using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using AIProject.SaveData;
using CharaCustom;
using GameLoadCharaFileSystem;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E44 RID: 3652
	public class PlayerChangeUI : MenuUIBehaviour
	{
		// Token: 0x06007294 RID: 29332 RVA: 0x0030D2F1 File Offset: 0x0030B6F1
		public IObservable<int> OnSelectIDChangedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x0030D32C File Offset: 0x0030B72C
		private IObservable<bool> OnActiveLCWChangedAsObservable()
		{
			if (this._activeChangeLCW == null)
			{
				this._activeChangeLCW = (from _ in this._isActiveLCW.TakeUntilDestroy(base.gameObject)
				where base.isActiveAndEnabled
				select _).Publish<bool>();
				this._activeChangeLCW.Connect();
			}
			return this._activeChangeLCW;
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06007296 RID: 29334 RVA: 0x0030D384 File Offset: 0x0030B784
		public MenuUIBehaviour[] MenuUIList
		{
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIList) == null)
				{
					result = (this._menuUIList = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x06007297 RID: 29335 RVA: 0x0030D3B4 File Offset: 0x0030B7B4
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControl(active);
			});
			this._playerButton.onClick.AddListener(delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				this._selectedID.Value = 0;
				this._isActiveLCW.Value = true;
			});
			this.OnSelectIDChangedAsObservable().Subscribe(delegate(int x)
			{
				this._selectedImageTransform.gameObject.SetActiveIfDifferent(x != 1);
				if (this._selectedImageTransform.gameObject.activeSelf)
				{
					this._selectedImageTransform.localPosition = this._element.localPosition;
				}
			});
			this.OnActiveLCWChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControlLCW(active);
			});
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.Close();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			this._closeButton.onClick.AddListener(delegate()
			{
				this.Close();
			});
			this._lcw.onLoadItemFunc = delegate(GameCharaFileInfo dat)
			{
				PlayerData playerData = Singleton<Game>.Instance.WorldData.PlayerData;
				playerData.CharaFileNames[dat.sex] = dat.FileName;
				playerData.Sex = (byte)dat.sex;
				this._info = dat;
				this._charaText.text = dat.name;
				this._isActiveLCW.Value = false;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			};
			this._lcw.onClickRightFunc = delegate()
			{
				if (!this._isActiveLCW.Value)
				{
					return;
				}
				this._isActiveLCW.Value = false;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			};
			this._lcw.onCloseWindowFunc = delegate()
			{
				if (!this._isActiveLCW.Value)
				{
					return;
				}
				this._isActiveLCW.Value = false;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			};
			this._lcw.onCharaCreateClickAction = delegate(int sex)
			{
				if (Singleton<Scene>.Instance.IsNowLoadingFade)
				{
					return;
				}
				CharaCustom.modeNew = true;
				CharaCustom.modeSex = (byte)sex;
				CharaCustom.actEixt = null;
				CharaCustom.nextScene = Singleton<Manager.Resources>.Instance.DefinePack.SceneNames.MapScene;
				Scene.Data data = new Scene.Data
				{
					levelName = "CharaCustom",
					isAdd = false,
					isFade = true
				};
				Singleton<Scene>.Instance.LoadReserve(data, false);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			};
			this._selectedImageTransform.gameObject.SetActive(false);
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x0030D4F5 File Offset: 0x0030B8F5
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x0030D510 File Offset: 0x0030B910
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				Time.timeScale = 0f;
				ChaFileControl chaFileControl = new ChaFileControl();
				PlayerData playerData = Singleton<Game>.Instance.WorldData.PlayerData;
				if (!playerData.CharaFileName.IsNullOrEmpty() && chaFileControl.LoadCharaFile(playerData.CharaFileName, playerData.Sex, false, true))
				{
					string personality = string.Empty;
					VoiceInfo.Param param;
					if (!Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(chaFileControl.parameter.personality, out param))
					{
						personality = "不明";
					}
					else
					{
						personality = param.Personality;
					}
					string fullPath = (playerData.Sex != 0) ? string.Format("{0}chara/female/{1}.png", UserData.Path, playerData.CharaFileName) : string.Format("{0}chara/male/{1}.png", UserData.Path, playerData.CharaFileName);
					this._info = new GameCharaFileInfo
					{
						name = chaFileControl.parameter.fullname,
						personality = personality,
						voice = chaFileControl.parameter.personality,
						hair = chaFileControl.custom.hair.kind,
						birthMonth = (int)chaFileControl.parameter.birthMonth,
						birthDay = (int)chaFileControl.parameter.birthDay,
						strBirthDay = chaFileControl.parameter.strBirthDay,
						sex = (int)chaFileControl.parameter.sex,
						FullPath = fullPath,
						FileName = playerData.CharaFileName,
						gameRegistration = chaFileControl.gameinfo.gameRegistration,
						flavorState = new Dictionary<int, int>(chaFileControl.gameinfo.flavorState),
						phase = chaFileControl.gameinfo.phase,
						normalSkill = new Dictionary<int, int>(chaFileControl.gameinfo.normalSkill),
						hSkill = new Dictionary<int, int>(chaFileControl.gameinfo.hSkill),
						favoritePlace = chaFileControl.gameinfo.favoritePlace,
						futanari = chaFileControl.parameter.futanari,
						data_uuid = chaFileControl.dataID
					};
				}
				else
				{
					this._info = null;
				}
				if (this._info != null)
				{
					this._charaText.text = this._info.name;
				}
				else
				{
					this._charaText.text = "-----";
				}
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.OpenCoroutine();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.CloseCoroutine();
			}
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x0030D82C File Offset: 0x0030BC2C
		private IEnumerator OpenCoroutine()
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x0030D848 File Offset: 0x0030BC48
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			Time.timeScale = 1f;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x0030D864 File Offset: 0x0030BC64
		private void SetActiveControlLCW(bool active)
		{
			Manager.Input input = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				input.FocusLevel = 1;
				this._lcw.gameObject.SetActiveIfDifferent(true);
				this._lcw.useDownload = true;
				this._lcw.ReCreateList(true, true);
				coroutine = this.OpenLCWCoroutine();
			}
			else
			{
				coroutine = this.CloseLCWCoroutine();
			}
			if (this._fadeLCWDisposable != null)
			{
				this._fadeLCWDisposable.Dispose();
			}
			this._fadeLCWDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				if (!active)
				{
					input.FocusLevel = 0;
					this._lcw.gameObject.SetActive(false);
				}
			});
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x0030D96C File Offset: 0x0030BD6C
		private IEnumerator OpenLCWCoroutine()
		{
			this._lcwObject.SetActiveIfDifferent(true);
			if (this._lcwCanvasGroup.blocksRaycasts)
			{
				this._lcwCanvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._lcwCanvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._lcwCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._lcwCanvasGroup.blocksRaycasts = true;
			yield break;
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x0030D988 File Offset: 0x0030BD88
		private IEnumerator CloseLCWCoroutine()
		{
			this._lcwCanvasGroup.blocksRaycasts = false;
			float startAlpha = this._lcwCanvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._lcwCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			this._lcwObject.SetActiveIfDifferent(false);
			yield break;
		}

		// Token: 0x04005DD1 RID: 24017
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005DD2 RID: 24018
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005DD3 RID: 24019
		[SerializeField]
		private Button _playerButton;

		// Token: 0x04005DD4 RID: 24020
		[SerializeField]
		private RectTransform _element;

		// Token: 0x04005DD5 RID: 24021
		[SerializeField]
		private Text _charaText;

		// Token: 0x04005DD6 RID: 24022
		[SerializeField]
		private RectTransform _selectedImageTransform;

		// Token: 0x04005DD7 RID: 24023
		[SerializeField]
		private GameLoadCharaWindow _lcw;

		// Token: 0x04005DD8 RID: 24024
		[SerializeField]
		private CanvasGroup _lcwCanvasGroup;

		// Token: 0x04005DD9 RID: 24025
		[SerializeField]
		private GameObject _lcwObject;

		// Token: 0x04005DDA RID: 24026
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005DDB RID: 24027
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04005DDC RID: 24028
		private BoolReactiveProperty _isActiveLCW = new BoolReactiveProperty(false);

		// Token: 0x04005DDD RID: 24029
		private IConnectableObservable<bool> _activeChangeLCW;

		// Token: 0x04005DDE RID: 24030
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005DDF RID: 24031
		private GameCharaFileInfo _info;

		// Token: 0x04005DE0 RID: 24032
		private IDisposable _fadeDisposable;

		// Token: 0x04005DE1 RID: 24033
		private IDisposable _fadeLCWDisposable;
	}
}
