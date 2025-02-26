using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using AIProject.Definitions;
using AIProject.SaveData;
using CharaCustom;
using GameLoadCharaFileSystem;
using Illusion.Component.UI;
using Illusion.Extensions;
using Manager;
using ReMotion;
using SceneAssist;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E41 RID: 3649
	public class CharaLookEditUI : MenuUIBehaviour
	{
		// Token: 0x0600726A RID: 29290 RVA: 0x0030A4F8 File Offset: 0x003088F8
		public IObservable<int> OnSelectIDChangedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x0600726B RID: 29291 RVA: 0x0030A534 File Offset: 0x00308934
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

		// Token: 0x0600726C RID: 29292 RVA: 0x0030A564 File Offset: 0x00308964
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
			this.OnSelectIDChangedAsObservable().Subscribe(delegate(int x)
			{
				this._charaCreateButton.interactable = (x != -1);
				this._selectedImageTransform.gameObject.SetActiveIfDifferent(x != -1);
				if (this._selectedImageTransform.gameObject.activeSelf)
				{
					this._selectedImageTransform.localPosition = this._elements[x].localPosition;
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
							for (int num2 = 0; num2 < this._objFemaleParameterRoots.Length; num2++)
							{
								this._objFemaleParameterRoots[num2].SetActiveIfDifferent(num2 == 0);
							}
						}
						this._txtFemaleCharaName.text = element.name;
						this._riFemaleCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(element.FullPath), 0, 0, TextureFormat.ARGB32, false);
						for (int num3 = 0; num3 < this._sccStateOfProgress.Length; num3++)
						{
							this._sccStateOfProgress[num3].OnChangeValue((element.phase >= num3) ? 1 : 0);
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
						for (int num4 = 0; num4 < this._txtNormalSkillSlots.Length; num4++)
						{
							string text2 = "--------------------";
							if (element.normalSkill.ContainsKey(num4))
							{
								StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(16, element.normalSkill[num4]);
								text2 = (((item != null) ? item.Name : null) ?? "--------------------");
							}
							this._txtNormalSkillSlots[num4].text = text2;
						}
						for (int num5 = 0; num5 < this._txtHSkillSlots.Length; num5++)
						{
							string text3 = "--------------------";
							if (element.hSkill.ContainsKey(num5))
							{
								StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(17, element.hSkill[num5]);
								text3 = (((item2 != null) ? item2.Name : null) ?? "--------------------");
							}
							this._txtHSkillSlots[num5].text = text3;
						}
					}
					else
					{
						this._txtFemaleCharaName.text = string.Empty;
						this._riFemaleCard.texture = this._texEmpty;
						for (int num6 = 0; num6 < this._sccStateOfProgress.Length; num6++)
						{
							this._sccStateOfProgress[num6].OnChangeValue((element.phase >= num6) ? 1 : 0);
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
						for (int num7 = 0; num7 < this._txtNormalSkillSlots.Length; num7++)
						{
							string text4 = "--------------------";
							this._txtNormalSkillSlots[num7].text = text4;
						}
						for (int num8 = 0; num8 < this._txtHSkillSlots.Length; num8++)
						{
							string text5 = "--------------------";
							this._txtHSkillSlots[num8].text = text5;
						}
					}
				}
			});
			this._objFemaleParameterWindow.SetActiveIfDifferent(false);
			this._txtFemaleCharaName.text = "NoName";
			this._riFemaleCard.texture = null;
			for (int j = 0; j < this._sccStateOfProgress.Length; j++)
			{
				this._sccStateOfProgress[j].OnChangeValue((j != 0) ? 0 : 1);
			}
			for (int k = 0; k < this._tglFemaleStateSelects.Length; k++)
			{
				int sel = k;
				(from _ in this._tglFemaleStateSelects[k].onValueChanged.AsObservable<bool>()
				where this._femaleParameterSelectNum != sel
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this._femaleParameterSelectNum = sel;
					for (int num2 = 0; num2 < this._objFemaleParameterRoots.Length; num2++)
					{
						this._objFemaleParameterRoots[num2].SetActiveIfDifferent(num2 == sel);
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
			for (int l = 0; l < this._actionStateSelects.Length; l++)
			{
				int sel = l;
				this._actionStateSelects[l].listActionEnter.Add(delegate
				{
					this._objFemaleStateSelectSels[sel].SetActiveIfDifferent(true);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
				this._actionStateSelects[l].listActionExit.Add(delegate
				{
					this._objFemaleStateSelectSels[sel].SetActiveIfDifferent(false);
				});
			}
			for (int m = 0; m < this._objFemaleStateSelectSels.Length; m++)
			{
				this._objFemaleStateSelectSels[m].SetActiveIfDifferent(false);
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
			for (int n = 0; n < this._txtNormalSkillSlots.Length; n++)
			{
				this._txtNormalSkillSlots[n].text = "--------------------";
			}
			for (int num = 0; num < this._txtHSkillSlots.Length; num++)
			{
				this._txtHSkillSlots[num].text = "--------------------";
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
			this._charaCreateButton.onClick.AddListener(delegate()
			{
				if (Singleton<Scene>.Instance.IsNowLoadingFade)
				{
					return;
				}
				CharaCustom.modeNew = false;
				CharaCustom.modeSex = 1;
				CharaCustom.editCharaFileName = Singleton<Game>.Instance.WorldData.AgentTable[this._selectedID.Value].CharaFileName;
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
			});
			this._charaCreateButton.interactable = false;
			this._selectedImageTransform.gameObject.SetActive(false);
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x0030A8DA File Offset: 0x00308CDA
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x0030A8F4 File Offset: 0x00308CF4
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				this._selectedID.Value = -1;
				Time.timeScale = 0f;
				Dictionary<int, AgentData> agentTable = Singleton<Game>.Instance.WorldData.AgentTable;
				for (int i = 0; i < 4; i++)
				{
					AgentData agentData = agentTable[i];
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
					this._objFemaleParameterWindow.SetActiveIfDifferent(false);
					this._charaButtons[i].gameObject.SetActiveIfDifferent(agentData.OpenState);
					if (this._charaButtons[i].gameObject.activeSelf)
					{
						this._charaButtons[i].interactable = (agentTable.ContainsKey(i) && !agentTable[i].CharaFileName.IsNullOrEmpty());
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

		// Token: 0x0600726F RID: 29295 RVA: 0x0030ACB8 File Offset: 0x003090B8
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

		// Token: 0x06007270 RID: 29296 RVA: 0x0030ACD4 File Offset: 0x003090D4
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

		// Token: 0x04005D7D RID: 23933
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005D7E RID: 23934
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005D7F RID: 23935
		[SerializeField]
		private Button[] _charaButtons;

		// Token: 0x04005D80 RID: 23936
		[SerializeField]
		private RectTransform[] _elements;

		// Token: 0x04005D81 RID: 23937
		[SerializeField]
		private Text[] _charaTexts;

		// Token: 0x04005D82 RID: 23938
		[SerializeField]
		private RectTransform _selectedImageTransform;

		// Token: 0x04005D83 RID: 23939
		[SerializeField]
		private Button _charaCreateButton;

		// Token: 0x04005D84 RID: 23940
		[SerializeField]
		private Texture2D _texEmpty;

		// Token: 0x04005D85 RID: 23941
		[SerializeField]
		private GameObject _objFemaleParameterWindow;

		// Token: 0x04005D86 RID: 23942
		[SerializeField]
		private Text _txtFemaleCharaName;

		// Token: 0x04005D87 RID: 23943
		[SerializeField]
		private RawImage _riFemaleCard;

		// Token: 0x04005D88 RID: 23944
		[SerializeField]
		private SpriteChangeCtrl[] _sccStateOfProgress;

		// Token: 0x04005D89 RID: 23945
		[SerializeField]
		private Toggle[] _tglFemaleStateSelects;

		// Token: 0x04005D8A RID: 23946
		[SerializeField]
		private PointerEnterExitAction[] _actionStateSelects;

		// Token: 0x04005D8B RID: 23947
		[SerializeField]
		private GameObject[] _objFemaleStateSelectSels;

		// Token: 0x04005D8C RID: 23948
		[SerializeField]
		private GameObject[] _objFemaleParameterRoots;

		// Token: 0x04005D8D RID: 23949
		[SerializeField]
		private Text _txtLifeStyle;

		// Token: 0x04005D8E RID: 23950
		[SerializeField]
		private Text _txtGirlPower;

		// Token: 0x04005D8F RID: 23951
		[SerializeField]
		private Text _txtTrust;

		// Token: 0x04005D90 RID: 23952
		[SerializeField]
		private Text _txtHumanNature;

		// Token: 0x04005D91 RID: 23953
		[SerializeField]
		private Text _txtInstinct;

		// Token: 0x04005D92 RID: 23954
		[SerializeField]
		private Text _txtHentai;

		// Token: 0x04005D93 RID: 23955
		[SerializeField]
		private Text _txtVigilance;

		// Token: 0x04005D94 RID: 23956
		[SerializeField]
		private Text _txtSocial;

		// Token: 0x04005D95 RID: 23957
		[SerializeField]
		private Text _txtDarkness;

		// Token: 0x04005D96 RID: 23958
		[SerializeField]
		private Text[] _txtNormalSkillSlots;

		// Token: 0x04005D97 RID: 23959
		[SerializeField]
		private Text[] _txtHSkillSlots;

		// Token: 0x04005D98 RID: 23960
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005D99 RID: 23961
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04005D9A RID: 23962
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005D9B RID: 23963
		private GameCharaFileInfo[] _infos = new GameCharaFileInfo[4];

		// Token: 0x04005D9C RID: 23964
		private int _femaleParameterSelectNum;

		// Token: 0x04005D9D RID: 23965
		private IDisposable _fadeDisposable;
	}
}
