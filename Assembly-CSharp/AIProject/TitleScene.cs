using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using CharaCustom;
using ConfigScene;
using FadeCtrl;
using Illusion.Component;
using Illusion.Extensions;
using Illusion.Game;
using Manager;
using SceneAssist;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02001036 RID: 4150
	public class TitleScene : BaseLoader
	{
		// Token: 0x06008AFE RID: 35582 RVA: 0x003A7BE8 File Offset: 0x003A5FE8
		public void OnStart()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this.Enter("Title_Load");
		}

		// Token: 0x06008AFF RID: 35583 RVA: 0x003A7C01 File Offset: 0x003A6001
		public void OnCustom()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this._buttonStack.Push(this._buttonDic[1]);
			this.InitImgSelectPostion(false);
			this.Enter("next");
		}

		// Token: 0x06008B00 RID: 35584 RVA: 0x003A7C38 File Offset: 0x003A6038
		public void OnCustomMale()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this.charaCreateSex = 0;
			this.Enter("CharaCustom");
		}

		// Token: 0x06008B01 RID: 35585 RVA: 0x003A7C58 File Offset: 0x003A6058
		public void OnCustomFemale()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this.charaCreateSex = 1;
			this.Enter("CharaCustom");
		}

		// Token: 0x06008B02 RID: 35586 RVA: 0x003A7C78 File Offset: 0x003A6078
		public void OnUploader()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this.Enter("Uploader");
		}

		// Token: 0x06008B03 RID: 35587 RVA: 0x003A7C91 File Offset: 0x003A6091
		public void OnDownloader()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this.Enter("Downloader");
		}

		// Token: 0x06008B04 RID: 35588 RVA: 0x003A7CAC File Offset: 0x003A60AC
		public void OnBack()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this._buttonStack.Pop();
			if (this._buttonDic[0].Count > 2 && !this.isUploader)
			{
				this._buttonDic[0][2].gameObject.SetActiveIfDifferent(false);
			}
			this.InitImgSelectPostion(false);
			this.Enter(string.Empty);
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x003A7D23 File Offset: 0x003A6123
		public void OnOther()
		{
			if (!this.IsEnter())
			{
				return;
			}
			this._buttonStack.Push(this._buttonDic[2]);
			this.InitImgSelectPostion(false);
			this.Enter("next");
		}

		// Token: 0x06008B06 RID: 35590 RVA: 0x003A7D5A File Offset: 0x003A615A
		public void OnOtherEvent()
		{
			if (!this.IsEnter())
			{
				return;
			}
		}

		// Token: 0x06008B07 RID: 35591 RVA: 0x003A7D68 File Offset: 0x003A6168
		public void OnConfig()
		{
			if (!this.IsEnter())
			{
				return;
			}
			if (Singleton<Game>.Instance.Config != null)
			{
				return;
			}
			this.Enter("Config");
		}

		// Token: 0x06008B08 RID: 35592 RVA: 0x003A7D97 File Offset: 0x003A6197
		public void OnEnd()
		{
			if (!this.IsEnter())
			{
				return;
			}
			if (Singleton<Game>.Instance.ExitScene != null)
			{
				return;
			}
			this.Enter("Exit");
		}

		// Token: 0x06008B09 RID: 35593 RVA: 0x003A7DC6 File Offset: 0x003A61C6
		private bool IsEnter()
		{
			return this.menuPlayableDirector.state != PlayState.Playing && !Singleton<Scene>.Instance.IsNowLoadingFade && !this.isTitleLoad;
		}

		// Token: 0x06008B0A RID: 35594 RVA: 0x003A7DF4 File Offset: 0x003A61F4
		private void InitImgSelectPostion(bool _isForce = false)
		{
			if ((from bt in this._buttonStack.Peek()
			where bt.interactable
			select bt).Count<Button>() > this.selectNum && !_isForce)
			{
				return;
			}
			this.selectNum = 0;
			Vector2 anchoredPosition = this.rtImgSelect.anchoredPosition;
			anchoredPosition.y = this.rtButtonRoot.anchoredPosition.y;
			this.rtImgSelect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06008B0B RID: 35595 RVA: 0x003A7E80 File Offset: 0x003A6280
		private void Enter(string next)
		{
			if (next != null)
			{
				if (next == string.Empty)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					goto IL_5A;
				}
			}
			this.PlaySE(1);
			IL_5A:
			if (next != null)
			{
				if (!(next == "Title_Load"))
				{
					if (!(next == "CharaCustom"))
					{
						if (!(next == "Uploader"))
						{
							if (!(next == "Downloader"))
							{
								if (!(next == "Config"))
								{
									if (next == "Exit")
									{
										Singleton<Game>.Instance.LoadExit();
									}
								}
								else
								{
									ConfigWindow.backGroundColor = new Color32(0, 0, 0, 200);
									Singleton<Game>.Instance.LoadConfig();
								}
							}
							else
							{
								bool flag = !Singleton<GameSystem>.Instance.HandleName.IsNullOrEmpty();
								Singleton<GameSystem>.Instance.networkSceneName = "Downloader";
								if (flag)
								{
									Scene.Data data = new Scene.Data
									{
										levelName = "NetworkCheckScene",
										isAdd = false,
										isFade = true,
										isAsync = true
									};
									Singleton<Scene>.Instance.LoadReserve(data, true);
								}
								else
								{
									Singleton<Scene>.Instance.LoadReserve(new Scene.Data
									{
										levelName = "EntryHandleName",
										isFade = true
									}, false);
								}
							}
						}
						else
						{
							bool flag2 = !Singleton<GameSystem>.Instance.HandleName.IsNullOrEmpty();
							Singleton<GameSystem>.Instance.networkSceneName = "Uploader";
							if (flag2)
							{
								Scene.Data data2 = new Scene.Data
								{
									levelName = "NetworkCheckScene",
									isAdd = false,
									isFade = true,
									isAsync = true
								};
								Singleton<Scene>.Instance.LoadReserve(data2, true);
							}
							else
							{
								Singleton<Scene>.Instance.LoadReserve(new Scene.Data
								{
									levelName = "EntryHandleName",
									isFade = true
								}, false);
							}
						}
					}
					else
					{
						CharaCustom.modeNew = true;
						CharaCustom.modeSex = (byte)this.charaCreateSex;
						Scene.Data data3 = new Scene.Data
						{
							levelName = next,
							isAdd = false,
							isFade = true
						};
						Singleton<Scene>.Instance.LoadReserve(data3, true);
					}
				}
				else
				{
					this.isTitleLoad = true;
					this.AddScene("title/scene/title_load.unity3d", next, false, delegate
					{
						this.objCanvas.SetActiveIfDifferent(false);
						TitleLoadScene rootComponent = Scene.GetRootComponent<TitleLoadScene>(next);
						if (rootComponent == null)
						{
							return;
						}
						rootComponent.titleScene = this;
						rootComponent.objMap = this.objMap;
						rootComponent.objTitleMain = this.objTitleMain;
						rootComponent.actionClose = delegate()
						{
							this.objCanvas.SetActiveIfDifferent(true);
							this.isTitleLoad = false;
						};
					});
				}
			}
		}

		// Token: 0x06008B0C RID: 35596 RVA: 0x003A8134 File Offset: 0x003A6534
		private IEnumerator Start()
		{
			base.enabled = false;
			if (this._version != null)
			{
				this._version.text = string.Format("Ver. {0}", Singleton<GameSystem>.Instance.GameVersion.ToString());
				this._version.gameObject.SetActiveIfDifferent(TitleScene.startmode == 0);
			}
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			Singleton<Game>.Instance.LoadGlobalData();
			this.animCaption.enabled = false;
			this.animCaption.speed = 0f;
			this.tmpCaption = this.animCaption.GetComponent<TextMeshProUGUI>();
			Color color = this.tmpCaption.color;
			color.a = 0f;
			this.tmpCaption.color = color;
			this.SetTileLine();
			IEnumerable<IGrouping<int, Button>> source = this._buttons.ToLookup((TitleScene.ButtonGroup p) => p.Group, (TitleScene.ButtonGroup p) => p.Button);
			Func<IGrouping<int, Button>, int> keySelector = (IGrouping<int, Button> p) => p.Key;
			if (TitleScene.<>f__mg$cache0 == null)
			{
				TitleScene.<>f__mg$cache0 = new Func<IGrouping<int, Button>, List<Button>>(Enumerable.ToList<Button>);
			}
			this._buttonDic = source.ToDictionary(keySelector, TitleScene.<>f__mg$cache0);
			foreach (List<Button> list in this._buttonDic.Values)
			{
				list.ForEach(delegate(Button p)
				{
					p.gameObject.SetActive(false);
				});
			}
			this._buttonStack.Push(this._buttonDic[0]);
			foreach (KeyValuePair<int, List<Button>> keyValuePair in this._buttonDic)
			{
				using (var enumerator3 = keyValuePair.Value.Select((Button value, int index) => new
				{
					value,
					index
				}).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						TitleScene.<Start>c__Iterator0.<Start>c__AnonStorey4 <Start>c__AnonStorey2 = new TitleScene.<Start>c__Iterator0.<Start>c__AnonStorey4();
						<Start>c__AnonStorey2.btn = enumerator3.Current;
						PointerEnterAction action = <Start>c__AnonStorey2.btn.value.GetComponent<PointerEnterAction>();
						int sel = <Start>c__AnonStorey2.btn.index;
						action.listAction.Add(delegate
						{
							if (this.menuPlayableDirector.state == PlayState.Playing)
							{
								return;
							}
							if (!<Start>c__AnonStorey2.btn.value.interactable)
							{
								return;
							}
							RectTransform rectTransform = action.transform as RectTransform;
							Vector2 anchoredPosition = this.rtImgSelect.anchoredPosition;
							anchoredPosition.y = this.rtButtonRoot.anchoredPosition.y + rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y;
							this.rtImgSelect.anchoredPosition = anchoredPosition;
							if (this.selectNum != sel)
							{
								Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
							}
							this.selectNum = sel;
						});
					}
				}
			}
			this.objectCategory.SetActiveToggle(0);
			this.ObserveEveryValueChanged((TitleScene _) => !Singleton<Scene>.Instance.IsNowLoadingFade, FrameCountType.Update, false).Subscribe(delegate(bool isOn)
			{
			});
			Singleton<Scene>.Instance.UnloadBaseScene();
			Singleton<Scene>.Instance.SetFadeColorDefault();
			Singleton<Scene>.Instance.UnloadAddScene();
			List<UnityEx.ValueTuple<string, string>> paths = MapScene.AssetBundlePaths;
			if (!paths.IsNullOrEmpty<UnityEx.ValueTuple<string, string>>())
			{
				foreach (UnityEx.ValueTuple<string, string> valueTuple in paths)
				{
					AssetBundleManager.UnloadAssetBundle(valueTuple.Item1, true, valueTuple.Item2, false);
				}
				UnityEngine.Resources.UnloadUnusedAssets();
				GC.Collect();
				MapScene.AssetBundlePaths.Clear();
			}
			Game.PrevPlayerStateFromCharaCreate = null;
			if (Singleton<Character>.IsInstance())
			{
				Singleton<Character>.Instance.EndLoadAssetBundle(false);
			}
			foreach (string assetBundleName in (from s in AssetBundleManager.AllLoadedAssetBundleNames
			where s.StartsWith("sound/data/pcm/")
			select s).ToArray<string>())
			{
				AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			}
			this.LoadMapList();
			Map mapManager = Singleton<Map>.Instance;
			int rand = UnityEngine.Random.Range(0, this.dicMapData.Count);
			if (this.dicMapData.Count != 0)
			{
				TitleScene.mapFileName = this.dicMapData[rand].fileName;
				yield return mapManager.LoadMap(this.dicMapData[rand].assetPath, TitleScene.mapFileName);
				this.objMap = Scene.GetRootGameObjects(TitleScene.mapFileName)[0];
			}
			if (Scene.isReturnTitle && Singleton<Scene>.Instance.sceneFade._Fade == SimpleFade.Fade.In)
			{
				while (!Singleton<Scene>.Instance.sceneFade.IsEnd)
				{
					yield return null;
				}
				yield return Singleton<Scene>.Instance.Fade(SimpleFade.Fade.Out, null);
			}
			Scene.isReturnTitle = false;
			this.LoadMapSoundList();
			this.LoadMapSeList();
			while (!Config.initialized)
			{
				yield return null;
			}
			this.PlayBGM();
			List<AudioClip> clipList = ListPool<AudioClip>.Get();
			new List<AudioClip>();
			foreach (string assetBundleName2 in AssetBundleData.GetAssetBundleNameListFromPath("sound/data/systemse/titlecall/", true))
			{
				clipList.AddRange(AssetBundleManager.LoadAllAsset(assetBundleName2, typeof(AudioClip), null).GetAllAssets<AudioClip>());
				AssetBundleManager.UnloadAssetBundle(assetBundleName2, true, null, false);
			}
			clipList.RemoveAll((AudioClip p) => p == null);
			AudioClip clip = clipList.GetElement(UnityEngine.Random.Range(0, clipList.Count));
			(from _ in (from _ in this.UpdateAsObservable()
			where !Singleton<Scene>.Instance.IsFadeNow
			select _).Take(1)
			where clip != null
			select _).Subscribe(delegate(Unit _)
			{
			});
			(from _ in (from _ in this.UpdateAsObservable()
			where !Singleton<Scene>.Instance.IsNowLoadingFade
			select _).Take(1)
			where this.playableDirector != null
			select _).Subscribe(delegate(Unit _)
			{
				if (TitleScene.startmode == 1)
				{
					this.ModeChange(0);
				}
				else
				{
					this.animCaption.enabled = true;
					this.playableDirector.Play();
				}
			});
			(from _ in this.UpdateAsObservable()
			where !Singleton<Scene>.Instance.IsFadeNow
			where !Singleton<Scene>.Instance.NowSceneNames[0].ContainsAny(new string[]
			{
				"Config",
				"Title_Load"
			})
			where Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Config == null
			where !this.isCoroutine
			select _).Subscribe(delegate(Unit _)
			{
				if (this.proc == 0)
				{
					if (this.playableDirector.state != PlayState.Playing)
					{
						if (this.animCaption.speed == 0f)
						{
							this.animCaption.speed = 1f;
							this.animCaption.Play("alpha pingpong", 0, 0f);
						}
						if (!UnityEngine.Input.GetKeyDown(KeyCode.Escape) && UnityEngine.Input.anyKeyDown && Singleton<Game>.Instance.ExitScene == null)
						{
							this.PlaySE(0);
							this.StartCoroutine(this.ModeChangeCoroutine(0));
						}
					}
					else if (UnityEngine.Input.anyKeyDown && this.playableDirector.state == PlayState.Playing)
					{
						this.playableDirector.time = 10.0;
						this.playableDirector.Evaluate();
					}
				}
				if (this.proc == 1 && UnityEngine.Input.GetMouseButtonDown(1) && this._buttonStack.Count == 1)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					this.StartCoroutine(this.ModeChangeCoroutine(1));
				}
			});
			(from _ in this.UpdateAsObservable()
			where !Singleton<Scene>.Instance.IsFadeNow
			where Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Dialog == null && Singleton<Game>.Instance.Config == null
			where !this.isCoroutine
			select _).Subscribe(delegate(Unit _)
			{
				if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
				{
					Singleton<Game>.Instance.LoadExit();
				}
			});
			(from _ in this.UpdateAsObservable()
			where !Singleton<Scene>.Instance.NowSceneNames[0].ContainsAny(new string[]
			{
				"Config",
				"Title_Load"
			})
			where Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Config == null
			where UnityEngine.Input.GetMouseButtonDown(1)
			where this._buttonStack.Count > 1
			select _).Subscribe(delegate(Unit _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				this._buttonStack.Pop();
				if (this._buttonDic[0].Count > 2 && !this.isUploader)
				{
					this._buttonDic[0][2].gameObject.SetActiveIfDifferent(false);
				}
				this.InitImgSelectPostion(false);
			});
			base.enabled = true;
			yield break;
		}

		// Token: 0x06008B0D RID: 35597 RVA: 0x003A8150 File Offset: 0x003A6550
		private bool LoadMapList()
		{
			string assetBundleName = this.titleListAssetBundleName;
			TitleData titleData = CommonLib.LoadAsset<TitleData>(assetBundleName, "title_map", false, string.Empty);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			foreach (TitleData.Param param in titleData.param)
			{
				if (!this.dicMapData.ContainsKey(param.id))
				{
					this.dicMapData[param.id] = new TitleScene.MapData();
				}
				TitleScene.MapData mapData = this.dicMapData[param.id];
				mapData.assetPath = param.assetPath;
				mapData.fileName = param.fileName;
				mapData.manifest = param.manifest;
			}
			return true;
		}

		// Token: 0x06008B0E RID: 35598 RVA: 0x003A8230 File Offset: 0x003A6630
		private bool LoadMapSoundList()
		{
			string assetBundleName = this.titleListAssetBundleName;
			TitleData titleData = CommonLib.LoadAsset<TitleData>(assetBundleName, "title_sound", false, string.Empty);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			foreach (TitleData.Param param in titleData.param)
			{
				if (!this.dicMapSound.ContainsKey(param.id))
				{
					this.dicMapSound[param.id] = new TitleScene.MapData();
				}
				TitleScene.MapData mapData = this.dicMapSound[param.id];
				mapData.assetPath = param.assetPath;
				mapData.fileName = param.fileName;
				mapData.manifest = param.manifest;
			}
			return true;
		}

		// Token: 0x06008B0F RID: 35599 RVA: 0x003A8310 File Offset: 0x003A6710
		private bool LoadMapSeList()
		{
			string assetBundleName = this.titleListAssetBundleName;
			TitleData titleData = CommonLib.LoadAsset<TitleData>(assetBundleName, "title_se", false, string.Empty);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			foreach (TitleData.Param param in titleData.param)
			{
				if (!this.dicMapSe.ContainsKey(param.id))
				{
					this.dicMapSe[param.id] = new TitleScene.MapData();
				}
				TitleScene.MapData mapData = this.dicMapSe[param.id];
				mapData.assetPath = param.assetPath;
				mapData.fileName = param.fileName;
				mapData.manifest = param.manifest;
			}
			return true;
		}

		// Token: 0x06008B10 RID: 35600 RVA: 0x003A83F0 File Offset: 0x003A67F0
		public void PlayBGM()
		{
			if (this.dicMapSound.Count != 0)
			{
				TitleScene.MapData mapData = this.dicMapSound[0];
				Transform transform = Singleton<Sound>.Instance.FindAsset(Sound.Type.BGM, mapData.fileName, mapData.assetPath);
				if (transform != null)
				{
					AudioSource component = transform.GetComponent<AudioSource>();
					bool? flag = (component != null) ? new bool?(component.isPlaying) : null;
					bool? flag2 = (flag == null) ? null : new bool?(!flag.Value);
					if (flag2 != null && flag2.Value)
					{
						Singleton<Sound>.Instance.PlayBGM(Singleton<Manager.Resources>.Instance.SoundPack.BGMInfo.MapBGMFadeTime);
					}
				}
				else
				{
					Illusion.Game.Utils.Sound.Play(new Illusion.Game.Utils.Sound.SettingBGM
					{
						assetBundleName = mapData.assetPath,
						assetName = mapData.fileName
					});
				}
			}
		}

		// Token: 0x06008B11 RID: 35601 RVA: 0x003A84F8 File Offset: 0x003A68F8
		private IEnumerator ModeChangeCoroutine(int _nextMode)
		{
			this.isCoroutine = true;
			this.spriteFadeCtrl.SetColor(Color.white);
			this.spriteFadeCtrl.FadeStart(SpriteFadeCtrl.FadeKind.Out, this.fadeTime);
			yield return base.StartCoroutine(this.spriteFadeCtrl.FadeProc());
			int[] array = new int[2];
			array[0] = 1;
			int[] nextObject = array;
			this.objectCategory.SetActiveToggle(nextObject[_nextMode]);
			if (_nextMode == 0)
			{
				this.InitImgSelectPostion(true);
				yield return base.StartCoroutine(this.PlayableDirectorInit(this.menuPlayableDirector));
			}
			else if (_nextMode == 1)
			{
				this.animCaption.speed = 0f;
				this.animCaption.enabled = false;
				Color color = this.tmpCaption.color;
				color.a = 0f;
				this.tmpCaption.color = color;
				yield return base.StartCoroutine(this.PlayableDirectorInit(this.playableDirector));
			}
			if (this._version != null)
			{
				this._version.gameObject.SetActiveIfDifferent(_nextMode == 1);
			}
			this.spriteFadeCtrl.FadeStart(SpriteFadeCtrl.FadeKind.In, this.fadeTime);
			yield return base.StartCoroutine(this.spriteFadeCtrl.FadeProc());
			int[] array2 = new int[2];
			array2[0] = 1;
			int[] nextProc = array2;
			this.proc = nextProc[_nextMode];
			TitleScene.startmode = this.proc;
			if (_nextMode == 0)
			{
				this.menuPlayableDirector.Play();
			}
			else if (_nextMode == 1)
			{
				this.animCaption.enabled = true;
				this.playableDirector.Play();
			}
			this.isCoroutine = false;
			yield break;
		}

		// Token: 0x06008B12 RID: 35602 RVA: 0x003A851C File Offset: 0x003A691C
		private IEnumerator PlayableDirectorInit(PlayableDirector _director)
		{
			_director.Play();
			yield return null;
			_director.Stop();
			yield break;
		}

		// Token: 0x06008B13 RID: 35603 RVA: 0x003A8538 File Offset: 0x003A6938
		private void ModeChange(int _nextMode)
		{
			int[] array = new int[2];
			array[0] = 1;
			int[] array2 = array;
			this.objectCategory.SetActiveToggle(array2[_nextMode]);
			this.playableDirector.time = 10.0;
			this.playableDirector.Evaluate();
			this.playableDirector.Play();
			if (_nextMode == 0)
			{
				this.InitImgSelectPostion(true);
				this.menuPlayableDirector.Stop();
			}
			else if (_nextMode == 1)
			{
				this.animCaption.speed = 0f;
				this.animCaption.enabled = false;
				Color color = this.tmpCaption.color;
				color.a = 0f;
				this.tmpCaption.color = color;
				this.playableDirector.Stop();
			}
			int[] array3 = new int[2];
			array3[0] = 1;
			int[] array4 = array3;
			this.proc = array4[_nextMode];
			if (_nextMode == 0)
			{
				this.menuPlayableDirector.Play();
			}
			else if (_nextMode == 1)
			{
				this.animCaption.enabled = true;
				this.playableDirector.Play();
			}
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x003A863F File Offset: 0x003A6A3F
		public AudioSource Get(int _seID)
		{
			return Illusion.Game.Utils.Sound.Get(Sound.Type.SystemSE, this.dicMapSe[_seID].assetPath, this.dicMapSe[_seID].fileName, null);
		}

		// Token: 0x06008B15 RID: 35605 RVA: 0x003A866C File Offset: 0x003A6A6C
		public void PlaySE(int _seID)
		{
			AudioSource audioSource = this.Get(_seID);
			if (audioSource == null)
			{
				return;
			}
			audioSource.Play();
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x003A8694 File Offset: 0x003A6A94
		private void AddScene(string assetBundleName, string levelName, bool _isFade, Action onLoad)
		{
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				assetBundleName = assetBundleName,
				levelName = levelName,
				isAdd = true,
				isFade = _isFade,
				onLoad = onLoad
			}, false);
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x003A86D8 File Offset: 0x003A6AD8
		private void SetTileLine()
		{
			this.menuPlayableDirector.playableAsset = ((!this.isUploader) ? this.menuTimeLine1 : this.menuTimeLine);
			int i;
			for (i = 0; i < this.lstTimeLlineInfos.Count; i++)
			{
				PlayableBinding playableBinding = this.menuPlayableDirector.playableAsset.outputs.First((PlayableBinding c) => c.streamName == this.lstTimeLlineInfos[i].trackName);
				if (this.lstTimeLlineInfos[i].kind == TitleScene.TimeLineKind.animation)
				{
					this.menuPlayableDirector.SetGenericBinding(playableBinding.sourceObject, this.lstTimeLlineInfos[i].animator);
				}
				else if (this.lstTimeLlineInfos[i].kind == TitleScene.TimeLineKind.activeObject)
				{
					this.menuPlayableDirector.SetGenericBinding(playableBinding.sourceObject, this.lstTimeLlineInfos[i].activeObject);
				}
			}
		}

		// Token: 0x04007183 RID: 29059
		public static int startmode;

		// Token: 0x04007184 RID: 29060
		[SerializeField]
		private GameObject objCanvas;

		// Token: 0x04007185 RID: 29061
		[SerializeField]
		private PlayableDirector playableDirector;

		// Token: 0x04007186 RID: 29062
		[SerializeField]
		private PlayableDirector menuPlayableDirector;

		// Token: 0x04007187 RID: 29063
		[SerializeField]
		private PlayableAsset menuTimeLine;

		// Token: 0x04007188 RID: 29064
		[SerializeField]
		private PlayableAsset menuTimeLine1;

		// Token: 0x04007189 RID: 29065
		[SerializeField]
		private List<TitleScene.TimeLineInfo> lstTimeLlineInfos = new List<TitleScene.TimeLineInfo>();

		// Token: 0x0400718A RID: 29066
		[SerializeField]
		private SpriteFadeCtrl spriteFadeCtrl;

		// Token: 0x0400718B RID: 29067
		[SerializeField]
		private float fadeTime = 0.5f;

		// Token: 0x0400718C RID: 29068
		[SerializeField]
		private Illusion.Component.ObjectCategoryBehaviour objectCategory;

		// Token: 0x0400718D RID: 29069
		[SerializeField]
		private TitleScene.ButtonGroup[] _buttons;

		// Token: 0x0400718E RID: 29070
		[SerializeField]
		private RectTransform rtButtonRoot;

		// Token: 0x0400718F RID: 29071
		[SerializeField]
		private RectTransform rtImgSelect;

		// Token: 0x04007190 RID: 29072
		[SerializeField]
		private Animator animCaption;

		// Token: 0x04007191 RID: 29073
		[SerializeField]
		private TextMeshProUGUI _version;

		// Token: 0x04007192 RID: 29074
		[SerializeField]
		private bool isUploader;

		// Token: 0x04007193 RID: 29075
		private Dictionary<int, TitleScene.MapData> dicMapData = new Dictionary<int, TitleScene.MapData>();

		// Token: 0x04007194 RID: 29076
		private Dictionary<int, TitleScene.MapData> dicMapSound = new Dictionary<int, TitleScene.MapData>();

		// Token: 0x04007195 RID: 29077
		private Dictionary<int, TitleScene.MapData> dicMapSe = new Dictionary<int, TitleScene.MapData>();

		// Token: 0x04007196 RID: 29078
		private int proc;

		// Token: 0x04007197 RID: 29079
		private bool isCoroutine;

		// Token: 0x04007198 RID: 29080
		private int selectNum;

		// Token: 0x04007199 RID: 29081
		private Dictionary<int, List<Button>> _buttonDic;

		// Token: 0x0400719A RID: 29082
		private TitleScene.ButtonStack<List<Button>> _buttonStack = new TitleScene.ButtonStack<List<Button>>();

		// Token: 0x0400719B RID: 29083
		private TextMeshProUGUI tmpCaption;

		// Token: 0x0400719C RID: 29084
		private bool isTitleLoad;

		// Token: 0x0400719D RID: 29085
		private int charaCreateSex;

		// Token: 0x0400719E RID: 29086
		public static string mapFileName = string.Empty;

		// Token: 0x0400719F RID: 29087
		[SerializeField]
		private GameObject objTitleMain;

		// Token: 0x040071A0 RID: 29088
		private GameObject objMap;

		// Token: 0x040071A1 RID: 29089
		private readonly string titleListAssetBundleName = "list/title.unity3d";

		// Token: 0x040071A3 RID: 29091
		[CompilerGenerated]
		private static Func<IGrouping<int, Button>, List<Button>> <>f__mg$cache0;

		// Token: 0x02001037 RID: 4151
		public enum TimeLineKind
		{
			// Token: 0x040071A5 RID: 29093
			animation,
			// Token: 0x040071A6 RID: 29094
			activeObject
		}

		// Token: 0x02001038 RID: 4152
		public class MapData
		{
			// Token: 0x040071A7 RID: 29095
			public string assetPath;

			// Token: 0x040071A8 RID: 29096
			public string fileName;

			// Token: 0x040071A9 RID: 29097
			public string manifest;
		}

		// Token: 0x02001039 RID: 4153
		[Serializable]
		public class TimeLineInfo
		{
			// Token: 0x040071AA RID: 29098
			public string trackName;

			// Token: 0x040071AB RID: 29099
			public TitleScene.TimeLineKind kind;

			// Token: 0x040071AC RID: 29100
			public Animator animator;

			// Token: 0x040071AD RID: 29101
			public GameObject activeObject;
		}

		// Token: 0x0200103A RID: 4154
		public enum Mode
		{
			// Token: 0x040071AF RID: 29103
			MainTitle,
			// Token: 0x040071B0 RID: 29104
			Menu
		}

		// Token: 0x0200103B RID: 4155
		public class ButtonStack<T> : Stack<T> where T : List<Button>
		{
			// Token: 0x06008B1D RID: 35613 RVA: 0x003A8824 File Offset: 0x003A6C24
			public new void Push(T item)
			{
				if (base.Count > 0)
				{
					T t = base.Peek();
					t.ForEach(delegate(Button b)
					{
						b.gameObject.SetActive(false);
					});
				}
				item.ForEach(delegate(Button b)
				{
					b.gameObject.SetActive(true);
				});
				base.Push(item);
			}

			// Token: 0x06008B1E RID: 35614 RVA: 0x003A88A0 File Offset: 0x003A6CA0
			public new T Pop()
			{
				T result = base.Pop();
				result.ForEach(delegate(Button b)
				{
					b.gameObject.SetActive(false);
				});
				if (base.Count > 0)
				{
					T t = base.Peek();
					t.ForEach(delegate(Button b)
					{
						b.gameObject.SetActive(true);
					});
				}
				return result;
			}
		}

		// Token: 0x0200103C RID: 4156
		[Serializable]
		public class ButtonGroup
		{
			// Token: 0x17001E44 RID: 7748
			// (get) Token: 0x06008B24 RID: 35620 RVA: 0x003A895C File Offset: 0x003A6D5C
			// (set) Token: 0x06008B25 RID: 35621 RVA: 0x003A8964 File Offset: 0x003A6D64
			public int Group
			{
				get
				{
					return this._group;
				}
				set
				{
					this._group = value;
				}
			}

			// Token: 0x17001E45 RID: 7749
			// (get) Token: 0x06008B26 RID: 35622 RVA: 0x003A896D File Offset: 0x003A6D6D
			// (set) Token: 0x06008B27 RID: 35623 RVA: 0x003A8975 File Offset: 0x003A6D75
			public Button Button
			{
				get
				{
					return this._button;
				}
				set
				{
					this._button = value;
				}
			}

			// Token: 0x040071B5 RID: 29109
			[SerializeField]
			private int _group;

			// Token: 0x040071B6 RID: 29110
			[SerializeField]
			private Button _button;
		}
	}
}
