using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
	// Token: 0x02000E3F RID: 3647
	public class CharaChangeUI : MenuUIBehaviour
	{
		// Token: 0x06007242 RID: 29250 RVA: 0x00308AA9 File Offset: 0x00306EA9
		public IObservable<int> OnSelectIDChangedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x00308AE4 File Offset: 0x00306EE4
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

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06007244 RID: 29252 RVA: 0x00308B3C File Offset: 0x00306F3C
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

		// Token: 0x06007245 RID: 29253 RVA: 0x00308B6C File Offset: 0x00306F6C
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControl(active);
			});
			for (int i = 0; i < this._charaButtons.Length; i++)
			{
				Button button = this._charaButtons[i];
				int id = i;
				button.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this._selectedID.Value = id;
					this._isActiveLCW.Value = (id != -1);
				});
			}
			this.OnSelectIDChangedAsObservable().Subscribe(delegate(int x)
			{
				this._selectedImageTransform.gameObject.SetActiveIfDifferent(x != -1);
				if (this._selectedImageTransform.gameObject.activeSelf)
				{
					this._selectedImageTransform.localPosition = this._elements[x].localPosition;
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
				string charaFileName = Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].CharaFileName;
				AgentActor agentActor;
				string a;
				if (Singleton<Map>.Instance.AgentTable.TryGetValue(this._selectedID.Value, out agentActor) && this._prevCharaNames.TryGetValue(this._selectedID.Value, out a) && a == charaFileName && !charaFileName.IsNullOrEmpty())
				{
					agentActor.ChaControl.chaFile.SaveCharaFile(agentActor.ChaControl.chaFile.charaFileName, byte.MaxValue, false);
				}
				Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].CharaFileName = dat.FileName;
				if (this._prevCharaMapIDs[this._selectedID.Value] != Singleton<Map>.Instance.MapID)
				{
					if (!dat.FileName.IsNullOrEmpty())
					{
						Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].MapID = Singleton<Map>.Instance.MapID;
					}
					else
					{
						Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].MapID = this._prevCharaMapIDs[this._selectedID.Value];
					}
				}
				this._infos[this._selectedID.Value] = dat;
				this._charaTexts[this._selectedID.Value].text = (dat.name ?? "-----");
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
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				CharaCustom.modeNew = true;
				CharaCustom.modeSex = 1;
				CharaCustom.actEixt = null;
				CharaCustom.nextScene = Singleton<Manager.Resources>.Instance.DefinePack.SceneNames.MapScene;
				Scene.Data data = new Scene.Data
				{
					levelName = "CharaCustom",
					isAdd = false,
					isFade = true
				};
				Singleton<Scene>.Instance.LoadReserve(data, false);
			};
			this._selectedImageTransform.gameObject.SetActive(false);
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x00308CE1 File Offset: 0x003070E1
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x06007247 RID: 29255 RVA: 0x00308CFC File Offset: 0x003070FC
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				this._selectedID.Value = -1;
				Time.timeScale = 0f;
				List<string> list = ListPool<string>.Get();
				WorldData autoData = Singleton<Game>.Instance.Data.AutoData;
				if (autoData != null)
				{
					list.Add(autoData.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair in autoData.AgentTable)
					{
						list.Add(keyValuePair.Value.CharaFileName);
					}
				}
				foreach (KeyValuePair<int, WorldData> keyValuePair2 in Singleton<Game>.Instance.Data.WorldList)
				{
					list.Add(keyValuePair2.Value.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair3 in keyValuePair2.Value.AgentTable)
					{
						list.Add(keyValuePair3.Value.CharaFileName);
					}
				}
				Dictionary<int, AgentData> agentTable = Singleton<Game>.Instance.WorldData.AgentTable;
				for (int i = 0; i < 4; i++)
				{
					AgentData agentData = agentTable[i];
					this._prevCharaNames[i] = agentData.CharaFileName;
					this._prevCharaMapIDs[i] = agentData.MapID;
					ChaFileControl chaFileControl = new ChaFileControl();
					if (!agentData.CharaFileName.IsNullOrEmpty() && chaFileControl.LoadCharaFile(agentData.CharaFileName, 1, false, true))
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
						this._infos[i] = new GameCharaFileInfo
						{
							name = chaFileControl.parameter.fullname,
							personality = personality,
							voice = chaFileControl.parameter.personality,
							hair = chaFileControl.custom.hair.kind,
							birthMonth = (int)chaFileControl.parameter.birthMonth,
							birthDay = (int)chaFileControl.parameter.birthDay,
							strBirthDay = chaFileControl.parameter.strBirthDay,
							sex = (int)chaFileControl.parameter.sex,
							FullPath = string.Format("{0}chara/female/{1}.png", UserData.Path, agentData.CharaFileName),
							FileName = agentData.CharaFileName,
							gameRegistration = chaFileControl.gameinfo.gameRegistration,
							flavorState = new Dictionary<int, int>(chaFileControl.gameinfo.flavorState),
							phase = chaFileControl.gameinfo.phase,
							normalSkill = new Dictionary<int, int>(chaFileControl.gameinfo.normalSkill),
							hSkill = new Dictionary<int, int>(chaFileControl.gameinfo.hSkill),
							favoritePlace = chaFileControl.gameinfo.favoritePlace,
							futanari = chaFileControl.parameter.futanari,
							data_uuid = chaFileControl.dataID,
							isInSaveData = list.Contains(Path.GetFileNameWithoutExtension(chaFileControl.charaFileName))
						};
					}
					else
					{
						this._infos[i] = null;
					}
					if (this._infos[i] != null)
					{
						this._charaTexts[i].text = (this._infos[i].name ?? "-----");
					}
					else
					{
						this._charaTexts[i].text = "-----";
					}
					this._charaButtons[i].gameObject.SetActiveIfDifferent(agentData.OpenState);
					if (this._charaButtons[i].gameObject.activeSelf)
					{
						this._charaButtons[i].interactable = (agentData.PlayEnterScene || i == Singleton<Map>.Instance.AccessDeviceID);
					}
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

		// Token: 0x06007248 RID: 29256 RVA: 0x00309238 File Offset: 0x00307638
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

		// Token: 0x06007249 RID: 29257 RVA: 0x00309254 File Offset: 0x00307654
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

		// Token: 0x0600724A RID: 29258 RVA: 0x00309270 File Offset: 0x00307670
		private void SetActiveControlLCW(bool active)
		{
			Manager.Input input = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				input.FocusLevel = 1;
				this._lcw.gameObject.SetActiveIfDifferent(true);
				if (!this._lcw.IsStartUp)
				{
					this._lcw.Start();
				}
				this._lcw.useDownload = true;
				int num = 0;
				foreach (GameCharaFileInfo gameCharaFileInfo in this._infos)
				{
					if (gameCharaFileInfo != null)
					{
						if (!gameCharaFileInfo.FileName.IsNullOrEmpty())
						{
							num++;
						}
					}
				}
				bool flag = num <= 1;
				this._lcw.ReCreateList(true, !flag);
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

		// Token: 0x0600724B RID: 29259 RVA: 0x003093E4 File Offset: 0x003077E4
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

		// Token: 0x0600724C RID: 29260 RVA: 0x00309400 File Offset: 0x00307800
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

		// Token: 0x04005D5E RID: 23902
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005D5F RID: 23903
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005D60 RID: 23904
		[SerializeField]
		private Button[] _charaButtons;

		// Token: 0x04005D61 RID: 23905
		[SerializeField]
		private RectTransform[] _elements;

		// Token: 0x04005D62 RID: 23906
		[SerializeField]
		private Text[] _charaTexts;

		// Token: 0x04005D63 RID: 23907
		[SerializeField]
		private RectTransform _selectedImageTransform;

		// Token: 0x04005D64 RID: 23908
		[SerializeField]
		private GameLoadCharaWindow _lcw;

		// Token: 0x04005D65 RID: 23909
		[SerializeField]
		private CanvasGroup _lcwCanvasGroup;

		// Token: 0x04005D66 RID: 23910
		[SerializeField]
		private GameObject _lcwObject;

		// Token: 0x04005D67 RID: 23911
		private Dictionary<int, string> _prevCharaNames = new Dictionary<int, string>();

		// Token: 0x04005D68 RID: 23912
		private Dictionary<int, int> _prevCharaMapIDs = new Dictionary<int, int>();

		// Token: 0x04005D69 RID: 23913
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005D6A RID: 23914
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04005D6B RID: 23915
		private BoolReactiveProperty _isActiveLCW = new BoolReactiveProperty(false);

		// Token: 0x04005D6C RID: 23916
		private IConnectableObservable<bool> _activeChangeLCW;

		// Token: 0x04005D6D RID: 23917
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005D6E RID: 23918
		private GameCharaFileInfo[] _infos = new GameCharaFileInfo[4];

		// Token: 0x04005D6F RID: 23919
		private IDisposable _fadeDisposable;

		// Token: 0x04005D70 RID: 23920
		private IDisposable _fadeLCWDisposable;
	}
}
