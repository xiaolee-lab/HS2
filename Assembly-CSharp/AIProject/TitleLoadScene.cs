using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.Scene;
using CharaCustom;
using GameLoadCharaFileSystem;
using Illusion.Component;
using Illusion.Extensions;
using Manager;
using SceneAssist;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02001033 RID: 4147
	public class TitleLoadScene : BaseLoader
	{
		// Token: 0x06008AD3 RID: 35539 RVA: 0x003A611C File Offset: 0x003A451C
		private IEnumerator Start()
		{
			base.enabled = false;
			this.language = Singleton<GameSystem>.Instance.languageInt;
			if (Singleton<Game>.Instance.Data == null)
			{
				string worldSaveDataFile = Path.WorldSaveDataFile;
				Singleton<Game>.Instance.LoadProfile(worldSaveDataFile);
			}
			List<int> list = (from v in Singleton<Game>.Instance.Data.WorldList.Select((KeyValuePair<int, WorldData> value, int idx) => new
			{
				value,
				idx
			})
			where v.value.Value != null
			where v.value.Value.PlayerData.CharaFileNames[0].IsNullOrEmpty() && v.value.Value.PlayerData.CharaFileNames[1].IsNullOrEmpty()
			select v.idx).ToList<int>();
			foreach (int id in list)
			{
				Singleton<Game>.Instance.RemoveWorldData(id);
			}
			this.objLoadRoot.SetActiveIfDifferent(true);
			this.objNewGameRoot.SetActiveIfDifferent(false);
			this.objModeSelectRoot.SetActiveIfDifferent(false);
			this.StartLoad();
			this.StartNewGame();
			this.StartStrongNewGameSelect();
			this.StartCharacterSelect();
			base.enabled = true;
			yield break;
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x003A6137 File Offset: 0x003A4537
		private void OnBack()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			if (this.actionClose != null)
			{
				this.actionClose();
			}
			Singleton<Scene>.Instance.UnLoad();
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x003A616C File Offset: 0x003A456C
		private void StartLoad()
		{
			bool isAutoFileExists = Singleton<Game>.Instance.Data.AutoData != null;
			this.infoAutoItem.objSave.SetActiveIfDifferent(isAutoFileExists);
			this.infoAutoItem.objInitialize.SetActiveIfDifferent(!isAutoFileExists);
			if (isAutoFileExists)
			{
				this.infoAutoItem.txtTitle.text = Singleton<Game>.Instance.Data.AutoData.Name;
				this.infoAutoItem.txtDay.text = Singleton<Game>.Instance.Data.AutoData.SaveTime.ToShortDateString();
				this.infoAutoItem.txtTime.text = Singleton<Game>.Instance.Data.AutoData.SaveTime.ToLongTimeString();
			}
			this.infoAutoItem.btnEntry.interactable = isAutoFileExists;
			this.infoAutoItem.isData = isAutoFileExists;
			this.infoAutoItem.num = 0;
			this.infoAutoItem.action.listActionEnter.Add(delegate
			{
				if (!isAutoFileExists)
				{
					return;
				}
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				this.infoAutoItem.objSelect.SetActiveIfDifferent(true);
			});
			this.infoAutoItem.action.listActionExit.Add(delegate
			{
				this.infoAutoItem.objSelect.SetActiveIfDifferent(false);
			});
			this.infoAutoItem.btnEntry.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.isDialogDraw = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				ConfirmScene.Sentence = this.localizeIsLoad[this.language];
				ConfirmScene.OnClickedYes = delegate()
				{
					this.SetWorldData(Singleton<Game>.Instance.Data.AutoData, true);
					this.GoToNextScene();
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
					this.isDialogDraw = false;
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					this.isDialogDraw = false;
				};
				Singleton<Game>.Instance.LoadDialog();
			});
			this.ObserveEveryValueChanged((TitleLoadScene _) => !Singleton<Scene>.Instance.IsNowLoadingFade && isAutoFileExists, FrameCountType.Update, false).SubscribeToInteractable(this.infoAutoItem.btnEntry);
			this.lstSaveInfo.Clear();
			this.lstSaveInfo.Add(this.infoAutoItem);
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < this.objSaveContentParent.transform.childCount; i++)
			{
				list.Add(this.objSaveContentParent.transform.GetChild(i));
			}
			this.objSaveContentParent.transform.DetachChildren();
			foreach (Transform obj in list)
			{
				UnityEngine.Object.Destroy(obj);
			}
			for (int j = 0; j < this.drawFileNum; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.nodeSaveButton);
				gameObject.transform.SetParent(this.objSaveContentParent.transform, false);
				TitleSaveItemInfo info = gameObject.GetComponent<TitleSaveItemInfo>();
				if (!info)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				else
				{
					bool isFileExists = Singleton<Game>.Instance.Data.WorldList.ContainsKey(j);
					info.objSave.SetActiveIfDifferent(isFileExists);
					info.objInitialize.SetActiveIfDifferent(!isFileExists);
					info.isData = isFileExists;
					info.num = j + 1;
					if (isFileExists)
					{
						WorldData worldData = Singleton<Game>.Instance.Data.WorldList[j];
						info.txtTitle.text = worldData.Name;
						info.txtDay.text = worldData.SaveTime.ToShortDateString();
						info.txtTime.text = worldData.SaveTime.ToLongTimeString();
					}
					info.action.listActionEnter.Add(delegate
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
						info.objSelect.SetActiveIfDifferent(true);
					});
					info.action.listActionExit.Add(delegate
					{
						info.objSelect.SetActiveIfDifferent(false);
					});
					info.btnEntry.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this.isDialogDraw = true;
						isFileExists = Singleton<Game>.Instance.Data.WorldList.ContainsKey(info.num - 1);
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
						ConfirmScene.Sentence = ((!isFileExists) ? this.localizeIsGameStart[this.language] : this.localizeIsLoad[this.language]);
						ConfirmScene.OnClickedYes = delegate()
						{
							if (isFileExists)
							{
								this.SetWorldData(Singleton<Game>.Instance.Data.WorldList[info.num - 1], false);
								this.GoToNextScene();
							}
							else
							{
								if (Singleton<Game>.Instance.GlobalData.Cleared)
								{
									this.objLoadRoot.SetActiveIfDifferent(false);
									this.objModeSelectRoot.SetActiveIfDifferent(true);
									this.ChangeSeletModeSelectUI(0);
								}
								else
								{
									this.objLoadRoot.SetActiveIfDifferent(false);
									this.objNewGameRoot.SetActiveIfDifferent(true);
								}
								this.worldNameInput.input.text = this.initWorldName;
								this.worldNameInput.textDummy.text = this.initWorldName;
								int num = info.num - 1;
								TitleLoadScene $this = this;
								WorldData value = Game.CreateInitData(num, false);
								Singleton<Game>.Instance.Data.WorldList[num] = value;
								$this.selectWorldData = value;
								this.selectWorldData.Name = "No Name";
							}
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
							this.isDialogDraw = false;
						};
						ConfirmScene.OnClickedNo = delegate()
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
							this.isDialogDraw = false;
						};
						Singleton<Game>.Instance.LoadDialog();
					});
					this.ObserveEveryValueChanged((TitleLoadScene _) => !Singleton<Scene>.Instance.IsNowLoadingFade, FrameCountType.Update, false).SubscribeToInteractable(info.btnEntry);
					this.lstSaveInfo.Add(info);
				}
			}
			this.btnLoadUIClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnBack();
			});
			this.btnLoadUIClose.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			IObservable<Unit> source = from _ in this.UpdateAsObservable()
			where Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Config == null
			where !Singleton<Scene>.Instance.IsNowLoadingFade
			select _;
			IObservable<Unit> source2 = from _ in source
			where UnityEngine.Input.GetMouseButtonDown(1)
			select _;
			(from _ in source2
			where this.objLoadRoot.activeSelf
			where !this.isDialogDraw
			select _).Subscribe(delegate(Unit _)
			{
				this.OnBack();
			});
			(from _ in source2
			where this.objNewGameRoot.activeSelf
			select _).Subscribe(delegate(Unit _)
			{
				this.BackToLoad();
			});
			(from _ in source2
			where this.objModeSelectRoot.activeSelf
			select _).Subscribe(delegate(Unit _)
			{
				this.objLoadRoot.SetActiveIfDifferent(true);
				this.objModeSelectRoot.SetActiveIfDifferent(false);
				Singleton<Game>.Instance.RemoveWorldData(this.selectWorldData.WorldID);
				this.selectWorldData = null;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			});
			(from _ in source
			where this.objLoadRoot.activeSelf
			where UnityEngine.Input.GetKeyDown(KeyCode.Delete)
			select _).Subscribe(delegate(Unit _)
			{
				TitleSaveItemInfo selectUI = (from info in this.lstSaveInfo
				where info.selectUI.IsSelect
				select info).FirstOrDefault<TitleSaveItemInfo>();
				if (selectUI)
				{
					if (!selectUI.isData)
					{
						return;
					}
					this.isDialogDraw = true;
					ConfirmScene.Sentence = this.localizeIsDelete[this.language];
					ConfirmScene.OnClickedYes = delegate()
					{
						if (selectUI.num == 0)
						{
							Singleton<Game>.Instance.Data.AutoData = null;
							selectUI.btnEntry.interactable = false;
							isAutoFileExists = false;
						}
						else
						{
							Singleton<Game>.Instance.RemoveWorldData(selectUI.num - 1);
						}
						selectUI.objSave.SetActiveIfDifferent(false);
						selectUI.objInitialize.SetActiveIfDifferent(true);
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
						<StartLoad>c__AnonStorey.isDialogDraw = false;
					};
					ConfirmScene.OnClickedNo = delegate()
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						<StartLoad>c__AnonStorey.isDialogDraw = false;
					};
					Singleton<Game>.Instance.LoadDialog();
				}
			});
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x003A6774 File Offset: 0x003A4B74
		private void StartNewGame()
		{
			this.worldNameInput.input.OnEndEditAsObservable().Subscribe(delegate(string str)
			{
				if (str.IsNullOrEmpty())
				{
					str = this.initWorldName;
					this.worldNameInput.input.text = str;
				}
				this.worldNameInput.textDummy.text = str;
				this.selectWorldData.Name = str;
			});
			this.btnNewGameBack.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.BackToLoad();
			});
			this.btnNewGameBack.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			(from _ in this.btnNewGameEntry.OnClickAsObservable()
			where !Singleton<Scene>.Instance.IsFadeNow
			select _).Subscribe(delegate(Unit _)
			{
				this.objNewGameRoot.SetActiveIfDifferent(false);
				this.lcwFemale.gameObject.SetActiveIfDifferent(true);
				this.lcwFemale.ReCreateList(true, true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			});
			this.btnNewGameEntry.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			(from _ in this.UpdateAsObservable()
			where this.objNewGameRoot.activeSelf
			select _).Subscribe(delegate(Unit _)
			{
				bool isFocused = this.worldNameInput.input.isFocused;
				this.worldNameInput.objDummy.SetActiveIfDifferent(!isFocused);
				this.worldNameInput.inputText.enabled = isFocused;
			});
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x003A6884 File Offset: 0x003A4C84
		private void BackToLoad()
		{
			this.objLoadRoot.SetActiveIfDifferent(true);
			this.objNewGameRoot.SetActiveIfDifferent(false);
			Singleton<Game>.Instance.RemoveWorldData(this.selectWorldData.WorldID);
			this.selectWorldData = null;
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x06008AD8 RID: 35544 RVA: 0x003A68D8 File Offset: 0x003A4CD8
		private void StartStrongNewGameSelect()
		{
			this.lstSelectUI[0].button.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.selectWorldData.FreeMode = false;
				this.objModeSelectRoot.SetActiveIfDifferent(false);
				this.objNewGameRoot.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			});
			this.lstSelectUI[0].action.listAction.Add(delegate
			{
				if (!this.lstSelectUI[0].objSelect.activeSelf)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				}
				this.ChangeSeletModeSelectUI(0);
			});
			this.lstSelectUI[1].button.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.selectWorldData.FreeMode = true;
				this.objModeSelectRoot.SetActiveIfDifferent(false);
				this.objNewGameRoot.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			});
			this.lstSelectUI[1].action.listAction.Add(delegate
			{
				if (!this.lstSelectUI[1].objSelect.activeSelf)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				}
				this.ChangeSeletModeSelectUI(1);
			});
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x003A6984 File Offset: 0x003A4D84
		private void StartCharacterSelect()
		{
			this.lcwPlayer.onLoadItemFunc = delegate(GameCharaFileInfo _data)
			{
				this.selectWorldData.PlayerData.CharaFileNames[_data.sex] = _data.FileName;
				this.selectWorldData.PlayerData.Sex = (byte)_data.sex;
				ChaFileControl.InitializeCharaFile(this.selectWorldData.AgentTable[0].CharaFileName, 1);
				this.SetWorldData(this.selectWorldData, false);
				this.GoToNextScene();
			};
			this.lcwPlayer.onCharaCreateClickAction = delegate(int sex)
			{
				CharaCustom.modeNew = true;
				CharaCustom.modeSex = (byte)sex;
				CharaCustom.actEixt = delegate()
				{
					if (Scene.isGameEnd)
					{
						return;
					}
					this.AllObjectVisible(true);
					if (this.objTitleMain)
					{
						this.lcwPlayer.ReCreateListOnly(true, true);
						if (this.titleScene)
						{
							this.titleScene.PlayBGM();
						}
					}
					Scene.ActiveScene = Scene.GetScene(TitleScene.mapFileName);
				};
				Scene.Data data = new Scene.Data
				{
					levelName = "CharaCustom",
					isAdd = true,
					isFade = true,
					onLoad = delegate
					{
						Scene.ActiveScene = Scene.GetScene("CharaCustom");
						this.AllObjectVisible(false);
					}
				};
				Singleton<Scene>.Instance.LoadReserve(data, true);
			};
			this.lcwPlayer.onClickRightFunc = delegate()
			{
				if (Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Config == null)
				{
					this.selectWorldData.AgentTable[0].CharaFileName = string.Empty;
					this.selectWorldData.PlayerData.Sex = 0;
					this.lcwPlayer.gameObject.SetActiveIfDifferent(false);
					this.lcwFemale.gameObject.SetActiveIfDifferent(true);
					this.lcwFemale.ReCreateList(true, true);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				}
			};
			this.lcwFemale.onLoadItemFunc = delegate(GameCharaFileInfo _data)
			{
				this.selectWorldData.AgentTable[0].CharaFileName = _data.FileName;
				this.lcwFemale.gameObject.SetActiveIfDifferent(false);
				this.lcwPlayer.gameObject.SetActiveIfDifferent(true);
				this.lcwPlayer.ReCreateList(true, true);
			};
			this.lcwFemale.onCharaCreateClickAction = delegate(int sex)
			{
				CharaCustom.modeNew = true;
				CharaCustom.modeSex = (byte)sex;
				CharaCustom.actEixt = delegate()
				{
					if (Scene.isGameEnd)
					{
						return;
					}
					this.AllObjectVisible(true);
					if (this.objTitleMain)
					{
						this.lcwFemale.ReCreateListOnly(true, true);
						if (this.titleScene)
						{
							this.titleScene.PlayBGM();
						}
					}
					Scene.ActiveScene = Scene.GetScene(TitleScene.mapFileName);
				};
				Scene.Data data = new Scene.Data
				{
					levelName = "CharaCustom",
					isAdd = true,
					isFade = true,
					onLoad = delegate
					{
						Scene.ActiveScene = Scene.GetScene("CharaCustom");
						this.AllObjectVisible(false);
					}
				};
				Singleton<Scene>.Instance.LoadReserve(data, true);
			};
			this.lcwFemale.onClickRightFunc = delegate()
			{
				if (Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Config == null)
				{
					this.lcwFemale.gameObject.SetActiveIfDifferent(false);
					this.objNewGameRoot.SetActiveIfDifferent(true);
					this.worldNameInput.input.text = this.selectWorldData.Name;
					this.worldNameInput.textDummy.text = this.selectWorldData.Name;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				}
			};
			this.lcwPlayer.gameObject.SetActiveIfDifferent(false);
			this.lcwFemale.gameObject.SetActiveIfDifferent(false);
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x003A6A40 File Offset: 0x003A4E40
		private void ChangeSeletModeSelectUI(int _drawIndex)
		{
			for (int i = 0; i < this.lstSelectUI.Count; i++)
			{
				this.lstSelectUI[i].objSelect.SetActiveIfDifferent(i == _drawIndex);
			}
		}

		// Token: 0x06008ADB RID: 35547 RVA: 0x003A6A84 File Offset: 0x003A4E84
		private void SetWorldData(WorldData _worldData, bool isAuto = false)
		{
			Singleton<Game>.Instance.WorldData = new WorldData();
			Singleton<Game>.Instance.WorldData.Copy(_worldData);
			Singleton<Game>.Instance.IsAuto = isAuto;
		}

		// Token: 0x06008ADC RID: 35548 RVA: 0x003A6AB0 File Offset: 0x003A4EB0
		private void GoToNextScene()
		{
			Scene.Data data = new Scene.Data
			{
				levelName = Singleton<Manager.Resources>.Instance.DefinePack.SceneNames.MapScene,
				isAdd = false,
				isFade = true,
				isAsync = true,
				isDrawProgressBar = false
			};
			Singleton<Scene>.Instance.LoadReserve(data, false);
			TitleScene.startmode = 0;
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x003A6B10 File Offset: 0x003A4F10
		private void AllObjectVisible(bool _visible)
		{
			if (this.objTitleMain)
			{
				if (this.objTitleMain != null)
				{
					this.objTitleMain.SetActiveIfDifferent(_visible);
				}
			}
			if (this.objMap)
			{
				if (this.objMap != null)
				{
					this.objMap.SetActiveIfDifferent(_visible);
				}
			}
			if (this.objTitleLoad)
			{
				if (this.objTitleLoad != null)
				{
					this.objTitleLoad.SetActiveIfDifferent(_visible);
				}
			}
		}

		// Token: 0x0400714B RID: 29003
		[Header("ロードUI")]
		[SerializeField]
		private GameObject objLoadRoot;

		// Token: 0x0400714C RID: 29004
		[SerializeField]
		private Button btnLoadUIClose;

		// Token: 0x0400714D RID: 29005
		[SerializeField]
		private TitleSaveItemInfo infoAutoItem;

		// Token: 0x0400714E RID: 29006
		[SerializeField]
		private GameObject nodeSaveButton;

		// Token: 0x0400714F RID: 29007
		[SerializeField]
		private GameObject objSaveContentParent;

		// Token: 0x04007150 RID: 29008
		[SerializeField]
		private int drawFileNum = 3;

		// Token: 0x04007151 RID: 29009
		[Header("NewGameUI")]
		[SerializeField]
		private GameObject objNewGameRoot;

		// Token: 0x04007152 RID: 29010
		[SerializeField]
		private InputFieldInfo worldNameInput = new InputFieldInfo();

		// Token: 0x04007153 RID: 29011
		[SerializeField]
		private Button btnNewGameBack;

		// Token: 0x04007154 RID: 29012
		[SerializeField]
		private Button btnNewGameEntry;

		// Token: 0x04007155 RID: 29013
		[SerializeField]
		private string initWorldName = "No Name";

		// Token: 0x04007156 RID: 29014
		[Header("モード選択UI")]
		[SerializeField]
		private GameObject objModeSelectRoot;

		// Token: 0x04007157 RID: 29015
		[SerializeField]
		private List<TitleLoadScene.ModeSelectUI> lstSelectUI = new List<TitleLoadScene.ModeSelectUI>();

		// Token: 0x04007158 RID: 29016
		[Header("Character選択UI")]
		[SerializeField]
		private GameLoadCharaWindow lcwPlayer;

		// Token: 0x04007159 RID: 29017
		[SerializeField]
		private GameLoadCharaWindow lcwFemale;

		// Token: 0x0400715A RID: 29018
		[Header("確認")]
		public GameObject objTitleMain;

		// Token: 0x0400715B RID: 29019
		public GameObject objMap;

		// Token: 0x0400715C RID: 29020
		[SerializeField]
		private GameObject objTitleLoad;

		// Token: 0x0400715D RID: 29021
		public TitleScene titleScene;

		// Token: 0x0400715E RID: 29022
		public System.Action actionClose;

		// Token: 0x0400715F RID: 29023
		private int proc;

		// Token: 0x04007160 RID: 29024
		private bool isCoroutine;

		// Token: 0x04007161 RID: 29025
		private List<TitleSaveItemInfo> lstSaveInfo = new List<TitleSaveItemInfo>();

		// Token: 0x04007162 RID: 29026
		private bool isDialogDraw;

		// Token: 0x04007163 RID: 29027
		private WorldData selectWorldData;

		// Token: 0x04007164 RID: 29028
		private string strD = string.Empty;

		// Token: 0x04007165 RID: 29029
		private readonly string[] localizeIsLoad = new string[]
		{
			"ロードしますか？",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04007166 RID: 29030
		private readonly string[] localizeIsGameStart = new string[]
		{
			"ゲームを開始しますか？",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04007167 RID: 29031
		private readonly string[] localizeIsDelete = new string[]
		{
			"削除してもいいですか？",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04007168 RID: 29032
		private int language;

		// Token: 0x02001034 RID: 4148
		[Serializable]
		public class ModeSelectUI
		{
			// Token: 0x04007172 RID: 29042
			public Button button;

			// Token: 0x04007173 RID: 29043
			public GameObject objSelect;

			// Token: 0x04007174 RID: 29044
			public PointerEnterAction action;
		}
	}
}
