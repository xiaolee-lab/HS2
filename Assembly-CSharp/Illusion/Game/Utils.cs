using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Illusion.Extensions;
using Manager;
using RootMotion.FinalIK;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Game
{
	// Token: 0x020007AA RID: 1962
	public static class Utils
	{
		// Token: 0x020007AB RID: 1963
		public static class IKLoader
		{
			// Token: 0x06002E7E RID: 11902 RVA: 0x00106C8C File Offset: 0x0010508C
			public static void Execute(FullBodyBipedIK ik, List<List<string>> dataList)
			{
				Transform[] componentsInChildren = ik.GetComponentsInChildren<Transform>(true);
				int num = 0;
				List<string> list = dataList[num++];
				int num2 = 0;
				ik.solver.IKPositionWeight = float.Parse(list[num2++]);
				ik.solver.iterations = int.Parse(list[num2++]);
				num2 = 0;
				foreach (List<string> list2 in dataList.Skip(num))
				{
					if (ik.solver.effectors.Length <= num2)
					{
						break;
					}
					num2++;
					int num3 = 0;
					IKEffector eff = ik.solver.GetEffector(Utils.Enum<FullBodyBipedEffector>.Cast(list2[num3++]));
					if (eff != null)
					{
						eff.positionWeight = float.Parse(list2[num3++]);
						eff.rotationWeight = float.Parse(list2[num3++]);
						string findFrame = list2[num3++];
						if (findFrame == "null")
						{
							eff.target = null;
						}
						else
						{
							componentsInChildren.FirstOrDefault((Transform p) => p.name == findFrame).SafeProc(delegate(Transform frame)
							{
								eff.target = frame;
							});
						}
						if (eff.target != null)
						{
							eff.target.localPosition = list2[num3++].GetVector3();
							eff.target.localEulerAngles = list2[num3++].GetVector3();
						}
					}
				}
				num += num2;
				num2 = 0;
				foreach (List<string> list3 in dataList.Skip(num))
				{
					if (ik.solver.chain.Length <= num2)
					{
						break;
					}
					FBIKChain fbikchain = ik.solver.chain[num2++];
					int num4 = 0;
					fbikchain.pull = float.Parse(list3[num4++]);
					fbikchain.reach = float.Parse(list3[num4++]);
					fbikchain.push = float.Parse(list3[num4++]);
					fbikchain.pushParent = float.Parse(list3[num4++]);
					fbikchain.reachSmoothing = Utils.Enum<FBIKChain.Smoothing>.Cast(list3[num4++]);
					fbikchain.pushSmoothing = Utils.Enum<FBIKChain.Smoothing>.Cast(list3[num4++]);
					fbikchain.bendConstraint.weight = float.Parse(list3[num4++]);
					string findFrame = list3[num4++];
					if (findFrame == "null")
					{
						fbikchain.bendConstraint.bendGoal = null;
					}
					else
					{
						fbikchain.bendConstraint.bendGoal = componentsInChildren.FirstOrDefault((Transform p) => p.name == findFrame);
					}
					if (fbikchain.bendConstraint.bendGoal != null)
					{
						fbikchain.bendConstraint.bendGoal.localPosition = list3[num4++].GetVector3();
						fbikchain.bendConstraint.bendGoal.localEulerAngles = list3[num4++].GetVector3();
					}
				}
			}
		}

		// Token: 0x020007AC RID: 1964
		public static class UniRx
		{
			// Token: 0x06002E7F RID: 11903 RVA: 0x001070E8 File Offset: 0x001054E8
			public static void FixPerspectiveObject<T>(T o, Camera camera) where T : Component
			{
				Transform transform = o.transform;
				Func<float> distance = () => (transform.position - camera.transform.position).magnitude;
				Vector3 baseScale = transform.localScale / distance();
				(from _ in o.UpdateAsObservable().TakeWhile((Unit _) => camera != null)
				select baseScale * distance()).Subscribe(delegate(Vector3 scale)
				{
					transform.localScale = scale;
				});
			}

			// Token: 0x020007AD RID: 1965
			public static class FPSCounter
			{
				// Token: 0x170007F3 RID: 2035
				// (get) Token: 0x06002E81 RID: 11905 RVA: 0x001071C1 File Offset: 0x001055C1
				// (set) Token: 0x06002E82 RID: 11906 RVA: 0x001071C8 File Offset: 0x001055C8
				public static IReadOnlyReactiveProperty<float> Current { get; private set; } = (from x in (from _ in Observable.EveryUpdate()
				select Time.deltaTime).Buffer(5, 1)
				select 1f / x.Average()).ToReadOnlyReactiveProperty<float>();

				// Token: 0x04002D4B RID: 11595
				private const int BufferSize = 5;
			}
		}

		// Token: 0x02001085 RID: 4229
		public static class Bundle
		{
			// Token: 0x06008DBE RID: 36286 RVA: 0x0010725C File Offset: 0x0010565C
			public static void LoadSprite(string assetBundleName, string assetName, Image image, bool isTexSize, string spAnimeName = null, string manifest = null, string spAnimeManifest = null)
			{
				AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(Sprite), manifest);
				Sprite sprite = assetBundleLoadAssetOperation.GetAsset<Sprite>();
				if (sprite == null)
				{
					Texture2D asset = assetBundleLoadAssetOperation.GetAsset<Texture2D>();
					sprite = Sprite.Create(asset, new Rect(0f, 0f, (float)asset.width, (float)asset.height), Vector2.zero);
				}
				image.sprite = sprite;
				RectTransform rectTransform = image.rectTransform;
				Vector2 vector = (!isTexSize) ? rectTransform.sizeDelta : new Vector2(sprite.rect.width, sprite.rect.height);
				if (!spAnimeName.IsNullOrEmpty())
				{
					Animator component = image.GetComponent<Animator>();
					component.enabled = true;
					component.runtimeAnimatorController = AssetBundleManager.LoadAsset(assetBundleName, spAnimeName, typeof(RuntimeAnimatorController), null).GetAsset<RuntimeAnimatorController>();
					Func<float, float, float> func = (float x, float y) => x / y;
					Func<float, float, bool> func2 = (float a, float b) => a > b && Mathf.FloorToInt(a - 1f) > 0;
					float num = func(vector.x, vector.y);
					float num2 = func(vector.y, vector.x);
					if (func2(num, num2))
					{
						rectTransform.sizeDelta = new Vector2(vector.y, vector.y);
					}
					else if (func2(num2, num))
					{
						rectTransform.sizeDelta = new Vector2(vector.x, vector.x);
					}
					else
					{
						rectTransform.sizeDelta = new Vector2(vector.x, vector.y);
					}
					AssetBundleManager.UnloadAssetBundle(assetBundleName, false, spAnimeManifest, false);
				}
				else
				{
					rectTransform.sizeDelta = new Vector2(vector.x, vector.y);
				}
				AssetBundleManager.UnloadAssetBundle(assetBundleName, false, manifest, false);
			}
		}

		// Token: 0x02001087 RID: 4231
		public static class Layout
		{
			// Token: 0x02001088 RID: 4232
			public class EnabledScope : GUI.Scope
			{
				// Token: 0x06008DC1 RID: 36289 RVA: 0x00107472 File Offset: 0x00105872
				public EnabledScope()
				{
					this.enabled = GUI.enabled;
				}

				// Token: 0x06008DC2 RID: 36290 RVA: 0x00107485 File Offset: 0x00105885
				public EnabledScope(bool enabled)
				{
					this.enabled = GUI.enabled;
					GUI.enabled = enabled;
				}

				// Token: 0x06008DC3 RID: 36291 RVA: 0x0010749E File Offset: 0x0010589E
				protected override void CloseScope()
				{
					GUI.enabled = this.enabled;
				}

				// Token: 0x040072D3 RID: 29395
				private readonly bool enabled;
			}

			// Token: 0x02001089 RID: 4233
			public class ColorScope : GUI.Scope
			{
				// Token: 0x06008DC4 RID: 36292 RVA: 0x001074AC File Offset: 0x001058AC
				public ColorScope()
				{
					this.colors = new Color[]
					{
						GUI.color,
						GUI.backgroundColor,
						GUI.contentColor
					};
				}

				// Token: 0x06008DC5 RID: 36293 RVA: 0x00107500 File Offset: 0x00105900
				public ColorScope(params Color[] colors)
				{
					this.colors = new Color[]
					{
						GUI.color,
						GUI.backgroundColor,
						GUI.contentColor
					};
					foreach (var <>__AnonType in colors.Take(this.colors.Length).Select((Color color, int index) => new
					{
						color,
						index
					}))
					{
						int index2 = <>__AnonType.index;
						if (index2 != 0)
						{
							if (index2 != 1)
							{
								if (index2 == 2)
								{
									GUI.contentColor = <>__AnonType.color;
								}
							}
							else
							{
								GUI.backgroundColor = <>__AnonType.color;
							}
						}
						else
						{
							GUI.color = <>__AnonType.color;
						}
					}
				}

				// Token: 0x06008DC6 RID: 36294 RVA: 0x00107614 File Offset: 0x00105A14
				public ColorScope(Colors colors)
				{
					this.colors = new Color[]
					{
						GUI.color,
						GUI.backgroundColor,
						GUI.contentColor
					};
					if (colors.color != null)
					{
						GUI.color = colors.color.Value;
					}
					if (colors.backgroundColor != null)
					{
						GUI.backgroundColor = colors.backgroundColor.Value;
					}
					if (colors.contentColor != null)
					{
						GUI.contentColor = colors.contentColor.Value;
					}
				}

				// Token: 0x06008DC7 RID: 36295 RVA: 0x001076CC File Offset: 0x00105ACC
				protected override void CloseScope()
				{
					int num = 0;
					GUI.color = this.colors[num++];
					GUI.backgroundColor = this.colors[num++];
					GUI.contentColor = this.colors[num++];
				}

				// Token: 0x040072D4 RID: 29396
				private readonly Color[] colors;
			}
		}

		// Token: 0x0200108A RID: 4234
		public static class ScreenShot
		{
			// Token: 0x17001EDD RID: 7901
			// (get) Token: 0x06008DC9 RID: 36297 RVA: 0x00107734 File Offset: 0x00105B34
			public static string Path
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					stringBuilder.Append(UserData.Create("cap"));
					DateTime now = DateTime.Now;
					stringBuilder.Append(now.Year.ToString("0000"));
					stringBuilder.Append(now.Month.ToString("00"));
					stringBuilder.Append(now.Day.ToString("00"));
					stringBuilder.Append(now.Hour.ToString("00"));
					stringBuilder.Append(now.Minute.ToString("00"));
					stringBuilder.Append(now.Second.ToString("00"));
					stringBuilder.Append(now.Millisecond.ToString("000"));
					stringBuilder.Append(".png");
					return stringBuilder.ToString();
				}
			}

			// Token: 0x06008DCA RID: 36298 RVA: 0x00107838 File Offset: 0x00105C38
			public static IEnumerator CaptureGSS(List<ScreenShotCamera> ssCamList, string path, Texture capMark, int capRate = 1)
			{
				if (ssCamList.IsNullOrEmpty<ScreenShotCamera>() || path.IsNullOrEmpty())
				{
					yield break;
				}
				yield return new WaitForEndOfFrame();
				Action<RenderTexture> shotProc = delegate(RenderTexture rt)
				{
					Graphics.Blit(ssCamList[0].renderTexture, rt);
					foreach (ScreenShotCamera screenShotCamera in ssCamList.Skip(1))
					{
						Graphics.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), screenShotCamera.renderTexture);
					}
					if (capMark != null)
					{
						Utils.ScreenShot.DrawCapMark(capMark, null);
					}
				};
				Utils.ScreenShot.Capture(shotProc, path, capRate);
				yield return null;
				yield break;
			}

			// Token: 0x06008DCB RID: 36299 RVA: 0x00107868 File Offset: 0x00105C68
			public static IEnumerator CaptureCameras(List<Camera> cameraList, string path, Texture capMark, int capRate = 1)
			{
				yield return new WaitForEndOfFrame();
				Action<RenderTexture> shotProc = delegate(RenderTexture rt)
				{
					Graphics.SetRenderTarget(rt);
					GL.Clear(true, true, Color.black);
					Graphics.SetRenderTarget(null);
					foreach (Camera camera in from p in cameraList
					where p != null
					select p)
					{
						bool enabled = camera.enabled;
						RenderTexture targetTexture = camera.targetTexture;
						Rect rect = camera.rect;
						camera.enabled = true;
						camera.targetTexture = rt;
						camera.Render();
						camera.targetTexture = targetTexture;
						camera.rect = rect;
						camera.enabled = enabled;
					}
					if (capMark != null)
					{
						Graphics.SetRenderTarget(rt);
						Utils.ScreenShot.DrawCapMark(capMark, null);
						Graphics.SetRenderTarget(null);
					}
				};
				Utils.ScreenShot.Capture(shotProc, path, capRate);
				yield return null;
				yield break;
			}

			// Token: 0x06008DCC RID: 36300 RVA: 0x00107898 File Offset: 0x00105C98
			public static void Capture(Action<RenderTexture> proc, string path, int capRate = 1)
			{
				int num = (capRate != 0) ? capRate : 1;
				Texture2D texture2D = new Texture2D(Screen.width * num, Screen.height * num, TextureFormat.RGB24, false);
				RenderTexture temporary = RenderTexture.GetTemporary(texture2D.width, texture2D.height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, (QualitySettings.antiAliasing != 0) ? QualitySettings.antiAliasing : 1);
				proc(temporary);
				RenderTexture.active = temporary;
				texture2D.ReadPixels(new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), 0, 0);
				texture2D.Apply();
				RenderTexture.active = null;
				byte[] bytes = texture2D.EncodeToPNG();
				RenderTexture.ReleaseTemporary(temporary);
				UnityEngine.Object.Destroy(texture2D);
				File.WriteAllBytes(path, bytes);
			}

			// Token: 0x06008DCD RID: 36301 RVA: 0x00107950 File Offset: 0x00105D50
			public static void DrawCapMark(Texture tex, Vector2? pos)
			{
				float num = (float)Screen.width / 1280f;
				if (pos == null)
				{
					pos = new Vector2?(new Vector2(1152f, 688f));
				}
				Graphics.DrawTexture(new Rect(pos.Value.x * num, pos.Value.y * num, (float)tex.width * num, (float)tex.height * num), tex);
			}
		}

		// Token: 0x0200108E RID: 4238
		public static class Sound
		{
			// Token: 0x06008DCE RID: 36302 RVA: 0x00107DCD File Offset: 0x001061CD
			public static LoadSound GetBGM()
			{
				if (Singleton<Manager.Sound>.IsInstance() && Singleton<Manager.Sound>.Instance.currentBGM != null)
				{
					return Singleton<Manager.Sound>.Instance.currentBGM.GetComponentInChildren<LoadSound>();
				}
				return null;
			}

			// Token: 0x06008DCF RID: 36303 RVA: 0x00107DFF File Offset: 0x001061FF
			public static AudioSource Get(Manager.Sound.Type type, AssetBundleData data)
			{
				return Singleton<Manager.Sound>.IsInstance() ? Singleton<Manager.Sound>.Instance.CreateCache(type, data) : null;
			}

			// Token: 0x06008DD0 RID: 36304 RVA: 0x00107E1D File Offset: 0x0010621D
			public static AudioSource Get(Manager.Sound.Type type, AssetBundleManifestData data)
			{
				return Singleton<Manager.Sound>.IsInstance() ? Singleton<Manager.Sound>.Instance.CreateCache(type, data) : null;
			}

			// Token: 0x06008DD1 RID: 36305 RVA: 0x00107E3B File Offset: 0x0010623B
			public static AudioSource Get(Manager.Sound.Type type, string bundle, string asset, string manifest = null)
			{
				return Singleton<Manager.Sound>.IsInstance() ? Singleton<Manager.Sound>.Instance.CreateCache(type, bundle, asset, manifest) : null;
			}

			// Token: 0x06008DD2 RID: 36306 RVA: 0x00107E5B File Offset: 0x0010625B
			public static void Remove(Manager.Sound.Type type, string bundle, string asset, string manifest = null)
			{
				if (!Singleton<Manager.Sound>.IsInstance())
				{
					return;
				}
				Singleton<Manager.Sound>.Instance.ReleaseCache(type, bundle, asset, manifest);
			}

			// Token: 0x06008DD3 RID: 36307 RVA: 0x00107E76 File Offset: 0x00106276
			public static AudioSource Get(SystemSE se)
			{
				return Utils.Sound.Get(Manager.Sound.Type.SystemSE, Utils.Sound.SoundBasePath[Manager.Sound.Type.SystemSE], Utils.Sound.SystemSECast[se], null);
			}

			// Token: 0x06008DD4 RID: 36308 RVA: 0x00107E95 File Offset: 0x00106295
			public static void Remove(SystemSE se)
			{
				Utils.Sound.Remove(Manager.Sound.Type.SystemSE, Utils.Sound.SoundBasePath[Manager.Sound.Type.SystemSE], Utils.Sound.SystemSECast[se], null);
			}

			// Token: 0x06008DD5 RID: 36309 RVA: 0x00107EB4 File Offset: 0x001062B4
			public static void Play(SystemSE se)
			{
				AudioSource audioSource = Utils.Sound.Get(se);
				if (audioSource == null)
				{
					return;
				}
				audioSource.Play();
			}

			// Token: 0x06008DD6 RID: 36310 RVA: 0x00107EDC File Offset: 0x001062DC
			public static AudioSource Play(Manager.Sound.Type type, AudioClip clip, float fadeTime = 0f)
			{
				AudioSource audio = Singleton<Manager.Sound>.Instance.Play(type, clip, fadeTime);
				(from __ in audio.UpdateAsObservable()
				where !audio.isPlaying
				select __).Take(1).Subscribe(delegate(Unit __)
				{
					UnityEngine.Object.Destroy(audio.gameObject);
				});
				return audio;
			}

			// Token: 0x06008DD7 RID: 36311 RVA: 0x00107F3C File Offset: 0x0010633C
			public static IEnumerator GetBGMandVolume(Action<string, float> bgmAndVolume)
			{
				string bgm = string.Empty;
				float volume = 1f;
				LoadSound now = Utils.Sound.GetBGM();
				if (now != null && now.audioSource != null)
				{
					FadePlayer fadePlayer = now.audioSource.GetComponent<FadePlayer>();
					while (fadePlayer != null && !(fadePlayer.nowState is FadePlayer.Playing))
					{
						yield return null;
					}
					bgm = now.assetBundleName;
					volume = now.audioSource.volume;
				}
				bgmAndVolume(bgm, volume);
				yield break;
			}

			// Token: 0x06008DD8 RID: 36312 RVA: 0x00107F58 File Offset: 0x00106358
			public static IEnumerator GetFadePlayerWhileNull(string bgm, float volume)
			{
				Transform source = Utils.Sound.Play(new Utils.Sound.SettingBGM(bgm)
				{
					isAsync = false
				});
				FadePlayer player = null;
				while (!(source == null))
				{
					player = source.GetComponent<FadePlayer>();
					yield return null;
					if (!(player == null))
					{
						IL_A9:
						if (player != null)
						{
							player.currentVolume = volume;
						}
						else if (Singleton<Manager.Sound>.IsInstance() && Singleton<Manager.Sound>.Instance.currentBGM != null)
						{
							player = Singleton<Manager.Sound>.Instance.currentBGM.GetComponent<FadePlayer>();
							if (player != null)
							{
								player.currentVolume = volume;
							}
							else
							{
								AudioSource component = Singleton<Manager.Sound>.Instance.currentBGM.GetComponent<AudioSource>();
								if (component != null)
								{
									component.volume = volume;
								}
							}
						}
						yield break;
					}
				}
				goto IL_A9;
			}

			// Token: 0x06008DD9 RID: 36313 RVA: 0x00107F7A File Offset: 0x0010637A
			public static bool isPlay(SystemSE se)
			{
				return Singleton<Manager.Sound>.IsInstance() && Singleton<Manager.Sound>.Instance.IsPlay(Manager.Sound.Type.SystemSE, Utils.Sound.SystemSECast[se]);
			}

			// Token: 0x06008DDA RID: 36314 RVA: 0x00107FA0 File Offset: 0x001063A0
			public static Transform Play(Utils.Sound.Setting s)
			{
				return Singleton<Manager.Sound>.IsInstance() ? Singleton<Manager.Sound>.Instance.Play(s.type, s.assetBundleName, s.assetName, s.delayTime, s.fadeTime, s.isAssetEqualPlay, s.isAsync, s.settingNo, s.isBundleUnload) : null;
			}

			// Token: 0x04007307 RID: 29447
			public static readonly Dictionary<SystemSE, string> SystemSECast = new Dictionary<SystemSE, string>(new Utils.Sound.SystemSEComparer())
			{
				{
					SystemSE.sel,
					"sse_00_01"
				},
				{
					SystemSE.ok_s,
					"sse_00_02"
				},
				{
					SystemSE.ok_l,
					"sse_00_03"
				},
				{
					SystemSE.cancel,
					"sse_00_04"
				},
				{
					SystemSE.photo,
					"sse_00_05"
				},
				{
					SystemSE.title,
					"se_06_title"
				},
				{
					SystemSE.ok_s2,
					"se_07_button_A"
				},
				{
					SystemSE.window_o,
					"se_08_window_B"
				},
				{
					SystemSE.save,
					"se_09_save_A"
				},
				{
					SystemSE.result_single,
					"result_00"
				},
				{
					SystemSE.result_gauge,
					"result_01"
				},
				{
					SystemSE.result_end,
					"result_02"
				}
			};

			// Token: 0x04007308 RID: 29448
			public static readonly Dictionary<Manager.Sound.Type, string> SoundBasePath = new Dictionary<Manager.Sound.Type, string>(new Utils.Sound.SoundTypeComparer())
			{
				{
					Manager.Sound.Type.BGM,
					"sound/data/bgm/00.unity3d"
				},
				{
					Manager.Sound.Type.ENV,
					"sound/data/env/00.unity3d"
				},
				{
					Manager.Sound.Type.GameSE2D,
					"sound/data/se/00.unity3d"
				},
				{
					Manager.Sound.Type.GameSE3D,
					"sound/data/se/00.unity3d"
				},
				{
					Manager.Sound.Type.SystemSE,
					"sound/data/systemse/00.unity3d"
				}
			};

			// Token: 0x0200108F RID: 4239
			public class SettingBGM : Utils.Sound.Setting
			{
				// Token: 0x06008DDC RID: 36316 RVA: 0x001081B5 File Offset: 0x001065B5
				public SettingBGM()
				{
					this.Initialize();
				}

				// Token: 0x06008DDD RID: 36317 RVA: 0x001081C3 File Offset: 0x001065C3
				public SettingBGM(int bgmNo)
				{
					this.Setting(this.Convert(bgmNo));
				}

				// Token: 0x06008DDE RID: 36318 RVA: 0x001081D8 File Offset: 0x001065D8
				public SettingBGM(BGM bgm)
				{
					this.Setting(this.Convert((int)bgm));
				}

				// Token: 0x06008DDF RID: 36319 RVA: 0x001081ED File Offset: 0x001065ED
				public SettingBGM(string assetBundleName)
				{
					this.Setting(assetBundleName);
				}

				// Token: 0x17001EDE RID: 7902
				// (get) Token: 0x06008DE0 RID: 36320 RVA: 0x001081FC File Offset: 0x001065FC
				// (set) Token: 0x06008DE1 RID: 36321 RVA: 0x00108204 File Offset: 0x00106604
				public override string assetBundleName
				{
					get
					{
						return this._assetBundleName;
					}
					set
					{
						this._assetBundleName = value;
						this._assetName = Path.GetFileNameWithoutExtension(value);
					}
				}

				// Token: 0x17001EDF RID: 7903
				// (get) Token: 0x06008DE2 RID: 36322 RVA: 0x00108219 File Offset: 0x00106619
				// (set) Token: 0x06008DE3 RID: 36323 RVA: 0x00108221 File Offset: 0x00106621
				public override string assetName
				{
					get
					{
						return this._assetName;
					}
					set
					{
						this._assetName = value;
					}
				}

				// Token: 0x06008DE4 RID: 36324 RVA: 0x0010822A File Offset: 0x0010662A
				private void Setting(string assetBundleName)
				{
					this.assetBundleName = assetBundleName;
					this.Initialize();
				}

				// Token: 0x06008DE5 RID: 36325 RVA: 0x00108239 File Offset: 0x00106639
				private string Convert(int bgmNo)
				{
					return string.Format("sound/data/bgm/bgm_{0:00}{1}", bgmNo, ".unity3d");
				}

				// Token: 0x06008DE6 RID: 36326 RVA: 0x00108250 File Offset: 0x00106650
				private void Initialize()
				{
					this.type = Manager.Sound.Type.BGM;
					this.fadeTime = 0.8f;
					this.isAssetEqualPlay = false;
					this.isBundleUnload = true;
				}

				// Token: 0x04007309 RID: 29449
				private string _assetBundleName;

				// Token: 0x0400730A RID: 29450
				private string _assetName;
			}

			// Token: 0x02001090 RID: 4240
			public class Setting
			{
				// Token: 0x06008DE7 RID: 36327 RVA: 0x001080FE File Offset: 0x001064FE
				public Setting()
				{
				}

				// Token: 0x06008DE8 RID: 36328 RVA: 0x0010811B File Offset: 0x0010651B
				public Setting(SystemSE se)
				{
					this.Cast(Manager.Sound.Type.SystemSE);
					this.assetName = Utils.Sound.SystemSECast[se];
				}

				// Token: 0x06008DE9 RID: 36329 RVA: 0x00108150 File Offset: 0x00106550
				public Setting(Manager.Sound.Type type)
				{
					this.Cast(type);
				}

				// Token: 0x17001EE0 RID: 7904
				// (get) Token: 0x06008DEA RID: 36330 RVA: 0x00108174 File Offset: 0x00106574
				// (set) Token: 0x06008DEB RID: 36331 RVA: 0x0010817C File Offset: 0x0010657C
				public virtual string assetBundleName { get; set; }

				// Token: 0x17001EE1 RID: 7905
				// (get) Token: 0x06008DEC RID: 36332 RVA: 0x00108185 File Offset: 0x00106585
				// (set) Token: 0x06008DED RID: 36333 RVA: 0x0010818D File Offset: 0x0010658D
				public virtual string assetName { get; set; }

				// Token: 0x06008DEE RID: 36334 RVA: 0x00108196 File Offset: 0x00106596
				public void Cast(Manager.Sound.Type type)
				{
					this.type = type;
					this.assetBundleName = Utils.Sound.SoundBasePath[this.type];
				}

				// Token: 0x0400730B RID: 29451
				public Manager.Sound.Type type;

				// Token: 0x0400730E RID: 29454
				public float delayTime;

				// Token: 0x0400730F RID: 29455
				public float fadeTime;

				// Token: 0x04007310 RID: 29456
				public bool isAssetEqualPlay = true;

				// Token: 0x04007311 RID: 29457
				public bool isAsync = true;

				// Token: 0x04007312 RID: 29458
				public int settingNo = -1;

				// Token: 0x04007313 RID: 29459
				public bool isBundleUnload;
			}

			// Token: 0x02001091 RID: 4241
			private class SystemSEComparer : IEqualityComparer<SystemSE>
			{
				// Token: 0x06008DF0 RID: 36336 RVA: 0x0010827A File Offset: 0x0010667A
				public bool Equals(SystemSE x, SystemSE y)
				{
					return x == y;
				}

				// Token: 0x06008DF1 RID: 36337 RVA: 0x00108280 File Offset: 0x00106680
				public int GetHashCode(SystemSE obj)
				{
					return (int)obj;
				}
			}

			// Token: 0x02001092 RID: 4242
			private class SoundTypeComparer : IEqualityComparer<Manager.Sound.Type>
			{
				// Token: 0x06008DF3 RID: 36339 RVA: 0x0010828B File Offset: 0x0010668B
				public bool Equals(Manager.Sound.Type x, Manager.Sound.Type y)
				{
					return x == y;
				}

				// Token: 0x06008DF4 RID: 36340 RVA: 0x00108291 File Offset: 0x00106691
				public int GetHashCode(Manager.Sound.Type obj)
				{
					return (int)obj;
				}
			}
		}

		// Token: 0x02001093 RID: 4243
		public static class Voice
		{
			// Token: 0x06008DF5 RID: 36341 RVA: 0x001085AE File Offset: 0x001069AE
			public static AudioSource Get(int voiceNo, AssetBundleData data)
			{
				return Singleton<Manager.Voice>.IsInstance() ? Singleton<Manager.Voice>.Instance.CreateCache(voiceNo, data) : null;
			}

			// Token: 0x06008DF6 RID: 36342 RVA: 0x001085CC File Offset: 0x001069CC
			public static AudioSource Get(int voiceNo, AssetBundleManifestData data)
			{
				return Singleton<Manager.Voice>.IsInstance() ? Singleton<Manager.Voice>.Instance.CreateCache(voiceNo, data) : null;
			}

			// Token: 0x06008DF7 RID: 36343 RVA: 0x001085EA File Offset: 0x001069EA
			public static AudioSource Get(int voiceNo, string bundle, string asset, string manifest = null)
			{
				return Singleton<Manager.Voice>.IsInstance() ? Singleton<Manager.Voice>.Instance.CreateCache(voiceNo, bundle, asset, manifest) : null;
			}

			// Token: 0x06008DF8 RID: 36344 RVA: 0x0010860A File Offset: 0x00106A0A
			public static void Remove(int voiceNo, string bundle, string asset, string manifest = null)
			{
				if (!Singleton<Manager.Voice>.IsInstance())
				{
					return;
				}
				Singleton<Manager.Voice>.Instance.ReleaseCache(voiceNo, bundle, asset, manifest);
			}

			// Token: 0x06008DF9 RID: 36345 RVA: 0x00108628 File Offset: 0x00106A28
			public static Transform Play(Utils.Voice.Setting s)
			{
				return Singleton<Manager.Voice>.IsInstance() ? Singleton<Manager.Voice>.Instance.Play(s.no, s.assetBundleName, s.assetName, s.pitch, s.delayTime, s.fadeTime, s.isAsync, s.voiceTrans, s.type, s.settingNo, s.isPlayEndDelete, s.isBundleUnload, s.is2D) : null;
			}

			// Token: 0x06008DFA RID: 36346 RVA: 0x001086A0 File Offset: 0x00106AA0
			public static Transform OnecePlay(Utils.Voice.Setting s)
			{
				return Singleton<Manager.Voice>.IsInstance() ? Singleton<Manager.Voice>.Instance.OnecePlay(s.no, s.assetBundleName, s.assetName, s.pitch, s.delayTime, s.fadeTime, s.isAsync, s.voiceTrans, s.type, s.settingNo, s.isPlayEndDelete, s.isBundleUnload, s.is2D) : null;
			}

			// Token: 0x06008DFB RID: 36347 RVA: 0x00108718 File Offset: 0x00106B18
			public static Transform OnecePlayChara(Utils.Voice.Setting s)
			{
				return Singleton<Manager.Voice>.IsInstance() ? Singleton<Manager.Voice>.Instance.OnecePlayChara(s.no, s.assetBundleName, s.assetName, s.pitch, s.delayTime, s.fadeTime, s.isAsync, s.voiceTrans, s.type, s.settingNo, s.isPlayEndDelete, s.isBundleUnload, s.is2D) : null;
			}

			// Token: 0x02001094 RID: 4244
			public class Setting
			{
				// Token: 0x04007314 RID: 29460
				public string assetBundleName;

				// Token: 0x04007315 RID: 29461
				public string assetName;

				// Token: 0x04007316 RID: 29462
				public Manager.Voice.Type type;

				// Token: 0x04007317 RID: 29463
				public int no;

				// Token: 0x04007318 RID: 29464
				public float pitch = 1f;

				// Token: 0x04007319 RID: 29465
				public Transform voiceTrans;

				// Token: 0x0400731A RID: 29466
				public float delayTime;

				// Token: 0x0400731B RID: 29467
				public float fadeTime;

				// Token: 0x0400731C RID: 29468
				public bool isAsync = true;

				// Token: 0x0400731D RID: 29469
				public int settingNo = -1;

				// Token: 0x0400731E RID: 29470
				public bool isPlayEndDelete = true;

				// Token: 0x0400731F RID: 29471
				public bool isBundleUnload;

				// Token: 0x04007320 RID: 29472
				public bool is2D;
			}
		}
	}
}
