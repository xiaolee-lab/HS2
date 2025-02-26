using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using AIProject.Definitions;
using AIProject.SaveData;
using GameLoadCharaFileSystem;
using Illusion.Component.UI;
using Illusion.Extensions;
using Manager;
using ReMotion;
using SceneAssist;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E42 RID: 3650
	public class CharaMigrateUI : MenuUIBehaviour
	{
		// Token: 0x06007279 RID: 29305 RVA: 0x0030B71D File Offset: 0x00309B1D
		public IObservable<int> OnSelectIDChangedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x0030B758 File Offset: 0x00309B58
		public IObservable<bool> OnActiveMapSelectChangedAsObservable()
		{
			if (this._activeChangeMapSelect == null)
			{
				this._activeChangeMapSelect = (from _ in this._isActiveMapSelect.TakeUntilDestroy(base.gameObject)
				where base.isActiveAndEnabled
				select _).Publish<bool>();
				this._activeChangeMapSelect.Connect();
			}
			return this._activeChangeMapSelect;
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x0600727B RID: 29307 RVA: 0x0030B7B0 File Offset: 0x00309BB0
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

		// Token: 0x0600727C RID: 29308 RVA: 0x0030B7E0 File Offset: 0x00309BE0
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
				});
			}
			for (int j = 0; j < this._charaArrowButtons.Length; j++)
			{
				Button button2 = this._charaArrowButtons[j];
				int id = j;
				button2.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this._selectedID.Value = id;
				});
			}
			this.OnSelectIDChangedAsObservable().Subscribe(delegate(int x)
			{
				this._selectedImageTransform.gameObject.SetActiveIfDifferent(x != -1);
				if (this._selectedImageTransform.gameObject.activeSelf)
				{
					this._selectedImageTransform.localPosition = this._elements[x].localPosition;
				}
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				AgentData agentData;
				if (worldData.AgentTable.TryGetValue(x, out agentData))
				{
					this._isActiveMapSelect.Value = (x != -1);
					AssetBundleInfo assetBundleInfo;
					if (Singleton<Manager.Resources>.Instance.Map.MapList.TryGetValue(agentData.MapID, out assetBundleInfo))
					{
						this._selectMapText.text = assetBundleInfo.name;
						this._selectedMapImageTransform.gameObject.SetActiveIfDifferent(agentData.MapID != -1);
						if (this._selectedMapImageTransform.gameObject.activeSelf)
						{
							Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
							{
								this._selectedMapImageTransform.localPosition = this._nodes[agentData.MapID].transform.localPosition;
							});
						}
					}
					else
					{
						this._selectMapText.text = "-----";
					}
				}
				else
				{
					this._selectMapText.text = "-----";
				}
				GameCharaFileInfo element = this._infos.GetElement(x);
				if (element != null)
				{
					if (!element.FullPath.IsNullOrEmpty())
					{
						bool activeSelf = this._objFemaleParameterWindow.activeSelf;
						this._objFemaleParameterWindow.SetActiveIfDifferent(true);
						if (!activeSelf)
						{
							this._femaleParameterSelectNum = 0;
							this._tglFemaleStateSelects[0].SetIsOnWithoutCallback(true);
							for (int num3 = 0; num3 < this._objFemaleParameterRoots.Length; num3++)
							{
								this._objFemaleParameterRoots[num3].SetActiveIfDifferent(num3 == 0);
							}
						}
						this._txtFemaleCharaName.text = element.name;
						this._riFemaleCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(element.FullPath), 0, 0, TextureFormat.ARGB32, false);
						for (int num4 = 0; num4 < this._sccStateOfProgress.Length; num4++)
						{
							this._sccStateOfProgress[num4].OnChangeValue((element.phase >= num4) ? 1 : 0);
						}
						string text = string.Empty;
						if (Lifestyle.LifestyleName.TryGetValue(element.lifeStyle, out text))
						{
							text = ((element.lifeStyle != 4) ? text : "E・シーカー");
						}
						else
						{
							text = "--------------------";
						}
						this._txtLifeStyle.text = text;
						this._txtGirlPower.text = element.flavorState[0].ToString();
						this._txtTrust.text = element.flavorState[1].ToString();
						this._txtHumanNature.text = element.flavorState[2].ToString();
						this._txtInstinct.text = element.flavorState[3].ToString();
						this._txtHentai.text = element.flavorState[4].ToString();
						this._txtVigilance.text = element.flavorState[5].ToString();
						this._txtSocial.text = element.flavorState[7].ToString();
						this._txtDarkness.text = element.flavorState[6].ToString();
						for (int num5 = 0; num5 < this._txtNormalSkillSlots.Length; num5++)
						{
							string text2 = "--------------------";
							if (element.normalSkill.ContainsKey(num5))
							{
								StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(16, element.normalSkill[num5]);
								text2 = (((item != null) ? item.Name : null) ?? "--------------------");
							}
							this._txtNormalSkillSlots[num5].text = text2;
						}
						for (int num6 = 0; num6 < this._txtHSkillSlots.Length; num6++)
						{
							string text3 = "--------------------";
							if (element.hSkill.ContainsKey(num6))
							{
								StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(17, element.hSkill[num6]);
								text3 = (((item2 != null) ? item2.Name : null) ?? "--------------------");
							}
							this._txtHSkillSlots[num6].text = text3;
						}
					}
					else
					{
						this._txtFemaleCharaName.text = string.Empty;
						for (int num7 = 0; num7 < this._sccStateOfProgress.Length; num7++)
						{
							this._sccStateOfProgress[num7].OnChangeValue((element.phase >= num7) ? 1 : 0);
						}
						this._txtLifeStyle.text = string.Empty;
						this._txtGirlPower.text = string.Empty;
						this._txtTrust.text = string.Empty;
						this._txtHumanNature.text = string.Empty;
						this._txtInstinct.text = string.Empty;
						this._txtHentai.text = string.Empty;
						this._txtVigilance.text = string.Empty;
						this._txtSocial.text = string.Empty;
						this._txtDarkness.text = string.Empty;
						for (int num8 = 0; num8 < this._txtNormalSkillSlots.Length; num8++)
						{
							string text4 = "--------------------";
							this._txtNormalSkillSlots[num8].text = text4;
						}
						for (int num9 = 0; num9 < this._txtHSkillSlots.Length; num9++)
						{
							string text5 = "--------------------";
							this._txtHSkillSlots[num9].text = text5;
						}
					}
				}
			});
			this.OnActiveMapSelectChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControlMapSelect(active);
			});
			this._objFemaleParameterWindow.SetActiveIfDifferent(false);
			this._txtFemaleCharaName.text = "NoName";
			this._riFemaleCard.texture = null;
			for (int k = 0; k < this._sccStateOfProgress.Length; k++)
			{
				this._sccStateOfProgress[k].OnChangeValue((k != 0) ? 0 : 1);
			}
			for (int l = 0; l < this._tglFemaleStateSelects.Length; l++)
			{
				int sel = l;
				(from _ in this._tglFemaleStateSelects[l].onValueChanged.AsObservable<bool>()
				where this._femaleParameterSelectNum != sel
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this._femaleParameterSelectNum = sel;
					for (int num3 = 0; num3 < this._objFemaleParameterRoots.Length; num3++)
					{
						this._objFemaleParameterRoots[num3].SetActiveIfDifferent(num3 == sel);
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
			for (int m = 0; m < this._actionStateSelects.Length; m++)
			{
				int sel = m;
				this._actionStateSelects[m].listActionEnter.Add(delegate
				{
					this._objFemaleStateSelectSels[sel].SetActiveIfDifferent(true);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
				this._actionStateSelects[m].listActionExit.Add(delegate
				{
					this._objFemaleStateSelectSels[sel].SetActiveIfDifferent(false);
				});
			}
			for (int n = 0; n < this._objFemaleStateSelectSels.Length; n++)
			{
				this._objFemaleStateSelectSels[n].SetActiveIfDifferent(false);
			}
			this._txtLifeStyle.text = string.Empty;
			this._txtGirlPower.text = "0";
			this._txtTrust.text = "0";
			this._txtHumanNature.text = "0";
			this._txtInstinct.text = "0";
			this._txtHentai.text = "0";
			this._txtVigilance.text = "0";
			this._txtSocial.text = "0";
			this._txtDarkness.text = "0";
			for (int num = 0; num < this._txtNormalSkillSlots.Length; num++)
			{
				this._txtNormalSkillSlots[num].text = "--------------------";
			}
			for (int num2 = 0; num2 < this._txtHSkillSlots.Length; num2++)
			{
				this._txtHSkillSlots[num2].text = "--------------------";
			}
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
			this._selectedImageTransform.gameObject.SetActive(false);
			this._selectedMapImageTransform.gameObject.SetActive(false);
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x0030BBB0 File Offset: 0x00309FB0
		private void Close()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x0030BBBC File Offset: 0x00309FBC
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				Time.timeScale = 0f;
				Dictionary<int, AgentData> agentTable = Singleton<Game>.Instance.WorldData.AgentTable;
				for (int i = 0; i < 4; i++)
				{
					AgentData agentData = agentTable[i];
					Dictionary<int, int> prevCharaMapIDs = this._prevCharaMapIDs;
					int key = i;
					int? prevMapID = agentData.PrevMapID;
					prevCharaMapIDs[key] = ((prevMapID == null) ? agentData.MapID : prevMapID.Value);
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
							lifeStyle = chaFileControl.gameinfo.lifestyle,
							data_uuid = chaFileControl.dataID
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
						this._charaButtons[i].interactable = (agentData.PlayEnterScene && !agentData.CharaFileName.IsNullOrEmpty());
					}
					this._charaArrowButtons[i].gameObject.SetActiveIfDifferent(agentData.OpenState && agentData.PlayEnterScene && !agentData.CharaFileName.IsNullOrEmpty());
					this._objFemaleParameterWindow.SetActiveIfDifferent(false);
					this._selectedID.Value = -1;
					this._isActiveMapSelect.Value = false;
				}
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.OpenCoroutine();
			}
			else
			{
				this._selectedID.Value = -1;
				this._isActiveMapSelect.Value = false;
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

		// Token: 0x0600727F RID: 29311 RVA: 0x0030C010 File Offset: 0x0030A410
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

		// Token: 0x06007280 RID: 29312 RVA: 0x0030C02C File Offset: 0x0030A42C
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

		// Token: 0x06007281 RID: 29313 RVA: 0x0030C048 File Offset: 0x0030A448
		private void SetActiveControlMapSelect(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				foreach (MigrateMapSelectNodeUI migrateMapSelectNodeUI in this._nodes)
				{
					if (!(migrateMapSelectNodeUI == null))
					{
						UnityEngine.Object.Destroy(migrateMapSelectNodeUI.gameObject);
					}
				}
				this._nodes.Clear();
				foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in Singleton<Manager.Resources>.Instance.Map.MapList)
				{
					MigrateMapSelectNodeUI node = UnityEngine.Object.Instantiate<GameObject>(this._mapSelectNode.gameObject, this._scrollRect.content, false).GetComponent<MigrateMapSelectNodeUI>();
					int id = keyValuePair.Key;
					string mapName = keyValuePair.Value.name;
					node.Text.text = mapName;
					node.Button.onClick.AddListener(delegate()
					{
						this._selectedMapImageTransform.gameObject.SetActiveIfDifferent(id != -1);
						if (this._selectedMapImageTransform.gameObject.activeSelf)
						{
							this._selectedMapImageTransform.localPosition = node.transform.localPosition;
						}
						this.OnSelectMap(id, mapName);
					});
					node.gameObject.SetActiveIfDifferent(true);
					this._nodes.Add(node);
				}
				coroutine = this.OpenMapSelectCoroutine();
			}
			else
			{
				coroutine = this.CloseMapSelectCoroutine();
			}
			if (this._fadeMapSelectDisposable != null)
			{
				this._fadeMapSelectDisposable.Dispose();
			}
			this._fadeMapSelectDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				if (!active)
				{
					this._objMapSelectWindow.SetActiveIfDifferent(false);
				}
			});
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x0030C28C File Offset: 0x0030A68C
		private void OnSelectMap(int id, string mapName)
		{
			int mapID = Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].MapID;
			if (mapID == id)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				return;
			}
			AgentActor agentActor;
			int num;
			if (Singleton<Manager.Map>.Instance.AgentTable.TryGetValue(this._selectedID.Value, out agentActor) && this._prevCharaMapIDs.TryGetValue(this._selectedID.Value, out num) && num != mapID)
			{
				agentActor.ChaControl.chaFile.SaveCharaFile(agentActor.ChaControl.chaFile.charaFileName, byte.MaxValue, false);
			}
			if (Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].PrevMapID != null)
			{
				if (Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].PrevMapID == id)
				{
					Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].PrevMapID = null;
				}
			}
			else
			{
				Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].PrevMapID = new int?(this._prevCharaMapIDs[this._selectedID.Value]);
			}
			Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].MapID = id;
			this._selectMapText.text = mapName;
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x0030C468 File Offset: 0x0030A868
		private IEnumerator OpenMapSelectCoroutine()
		{
			this._objMapSelectWindow.SetActiveIfDifferent(true);
			if (this._mapSelectCanvasGroup.blocksRaycasts)
			{
				this._mapSelectCanvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._mapSelectCanvasGroup.alpha;
			yield return ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._mapSelectCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			this._mapSelectCanvasGroup.blocksRaycasts = true;
			yield break;
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x0030C484 File Offset: 0x0030A884
		private IEnumerator CloseMapSelectCoroutine()
		{
			this._mapSelectCanvasGroup.blocksRaycasts = false;
			float startAlpha = this._mapSelectCanvasGroup.alpha;
			yield return ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._mapSelectCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).ToYieldInstruction<TimeInterval<float>>();
			this._objMapSelectWindow.SetActiveIfDifferent(false);
			yield break;
		}

		// Token: 0x04005DA0 RID: 23968
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005DA1 RID: 23969
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005DA2 RID: 23970
		[SerializeField]
		private Button[] _charaButtons;

		// Token: 0x04005DA3 RID: 23971
		[SerializeField]
		private Button[] _charaArrowButtons;

		// Token: 0x04005DA4 RID: 23972
		[SerializeField]
		private RectTransform[] _elements;

		// Token: 0x04005DA5 RID: 23973
		[SerializeField]
		private Text[] _charaTexts;

		// Token: 0x04005DA6 RID: 23974
		[SerializeField]
		private RectTransform _selectedImageTransform;

		// Token: 0x04005DA7 RID: 23975
		[SerializeField]
		private GameObject _objFemaleParameterWindow;

		// Token: 0x04005DA8 RID: 23976
		[SerializeField]
		private Text _txtFemaleCharaName;

		// Token: 0x04005DA9 RID: 23977
		[SerializeField]
		private RawImage _riFemaleCard;

		// Token: 0x04005DAA RID: 23978
		[SerializeField]
		private SpriteChangeCtrl[] _sccStateOfProgress;

		// Token: 0x04005DAB RID: 23979
		[SerializeField]
		private Toggle[] _tglFemaleStateSelects;

		// Token: 0x04005DAC RID: 23980
		[SerializeField]
		private PointerEnterExitAction[] _actionStateSelects;

		// Token: 0x04005DAD RID: 23981
		[SerializeField]
		private GameObject[] _objFemaleStateSelectSels;

		// Token: 0x04005DAE RID: 23982
		[SerializeField]
		private GameObject[] _objFemaleParameterRoots;

		// Token: 0x04005DAF RID: 23983
		[SerializeField]
		private Text _txtLifeStyle;

		// Token: 0x04005DB0 RID: 23984
		[SerializeField]
		private Text _txtGirlPower;

		// Token: 0x04005DB1 RID: 23985
		[SerializeField]
		private Text _txtTrust;

		// Token: 0x04005DB2 RID: 23986
		[SerializeField]
		private Text _txtHumanNature;

		// Token: 0x04005DB3 RID: 23987
		[SerializeField]
		private Text _txtInstinct;

		// Token: 0x04005DB4 RID: 23988
		[SerializeField]
		private Text _txtHentai;

		// Token: 0x04005DB5 RID: 23989
		[SerializeField]
		private Text _txtVigilance;

		// Token: 0x04005DB6 RID: 23990
		[SerializeField]
		private Text _txtSocial;

		// Token: 0x04005DB7 RID: 23991
		[SerializeField]
		private Text _txtDarkness;

		// Token: 0x04005DB8 RID: 23992
		[SerializeField]
		private Text[] _txtNormalSkillSlots;

		// Token: 0x04005DB9 RID: 23993
		[SerializeField]
		private Text[] _txtHSkillSlots;

		// Token: 0x04005DBA RID: 23994
		[SerializeField]
		private GameObject _objMapSelectWindow;

		// Token: 0x04005DBB RID: 23995
		[SerializeField]
		private CanvasGroup _mapSelectCanvasGroup;

		// Token: 0x04005DBC RID: 23996
		[SerializeField]
		private Text _selectMapText;

		// Token: 0x04005DBD RID: 23997
		[SerializeField]
		private ScrollRect _scrollRect;

		// Token: 0x04005DBE RID: 23998
		[SerializeField]
		private RectTransform _selectedMapImageTransform;

		// Token: 0x04005DBF RID: 23999
		[SerializeField]
		private MigrateMapSelectNodeUI _mapSelectNode;

		// Token: 0x04005DC0 RID: 24000
		private Dictionary<int, int> _prevCharaMapIDs = new Dictionary<int, int>();

		// Token: 0x04005DC1 RID: 24001
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005DC2 RID: 24002
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04005DC3 RID: 24003
		private BoolReactiveProperty _isActiveMapSelect = new BoolReactiveProperty(false);

		// Token: 0x04005DC4 RID: 24004
		private IConnectableObservable<bool> _activeChangeMapSelect;

		// Token: 0x04005DC5 RID: 24005
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005DC6 RID: 24006
		private List<MigrateMapSelectNodeUI> _nodes = new List<MigrateMapSelectNodeUI>();

		// Token: 0x04005DC7 RID: 24007
		private GameCharaFileInfo[] _infos = new GameCharaFileInfo[4];

		// Token: 0x04005DC8 RID: 24008
		private int _femaleParameterSelectNum;

		// Token: 0x04005DC9 RID: 24009
		private IDisposable _fadeDisposable;

		// Token: 0x04005DCA RID: 24010
		private IDisposable _fadeMapSelectDisposable;
	}
}
