using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIProject;
using AIProject.Definitions;
using Illusion.Component.UI;
using Illusion.Extensions;
using Manager;
using SceneAssist;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLoadCharaFileSystem
{
	// Token: 0x0200087A RID: 2170
	public class GameLoadCharaListCtrl : MonoBehaviour
	{
		// Token: 0x0600373C RID: 14140 RVA: 0x00147352 File Offset: 0x00145752
		public List<GameCharaFileInfo> GetListCharaFileInfo()
		{
			return this.lstFileInfo;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x0014735C File Offset: 0x0014575C
		private void Start()
		{
			this.tglOrder.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool _isOn)
			{
				this.imgOrder.enabled = !_isOn;
				if (this.tglSortDay.isOn)
				{
					this.SortDate(_isOn);
				}
				else
				{
					this.SortName(_isOn);
				}
				this.charaSelectScrollView.Init(this.lstFileInfo);
				this.charaSelectScrollView.SetNowSelectToggle();
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.imgOrder.enabled = !this.tglOrder.isOn;
			this.objOrderSelect.SetActiveIfDifferent(false);
			this.actionOrderSelect.listActionEnter.Add(delegate
			{
				this.objOrderSelect.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this.actionOrderSelect.listActionExit.Add(delegate
			{
				this.objOrderSelect.SetActiveIfDifferent(false);
			});
			this.btnSortWindowOpen.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.objSortWindow.SetActiveIfDifferent(!this.objSortWindow.activeSelf);
				if (this.objSortWindow.activeSelf)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				}
				else
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				}
			});
			this.btnSortWindowOpen.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this.charaSelectScrollView.onSelect = delegate(GameCharaFileInfo _data)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				if (this.onChangeItemFunc != null)
				{
					this.onChangeItemFunc(_data);
				}
				if (this.onChangeItem != null)
				{
					this.onChangeItem(true);
				}
				this.SetParameter(_data);
			};
			this.charaSelectScrollView.onDeSelect = delegate()
			{
				if (this.onChangeItem != null)
				{
					this.onChangeItem(false);
				}
				this.objPlayerParameterWindow.SetActiveIfDifferent(false);
				this.objFemaleParameterWindow.SetActiveIfDifferent(false);
			};
			this.objPlayerParameterWindow.SetActiveIfDifferent(false);
			this.txtPlayerCharaName.text = "NoName";
			this.riPlayerCard.texture = null;
			this.txtPlayerSex.text = string.Empty;
			this.objFemaleParameterWindow.SetActiveIfDifferent(false);
			this.txtFemaleCharaName.text = "NoName";
			this.riFemaleCard.texture = null;
			for (int i = 0; i < this.sccStateOfProgress.Length; i++)
			{
				this.sccStateOfProgress[i].OnChangeValue((i != 0) ? 0 : 1);
			}
			for (int j = 0; j < this.tglFemaleStateSelects.Length; j++)
			{
				int sel = j;
				(from _ in this.tglFemaleStateSelects[j].onValueChanged.AsObservable<bool>()
				where this.femaleParameterSelectNum != sel
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this.femaleParameterSelectNum = sel;
					for (int num = 0; num < this.objFemalParameterRoots.Length; num++)
					{
						this.objFemalParameterRoots[num].SetActiveIfDifferent(num == sel);
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
			for (int k = 0; k < this.actionStateSelects.Length; k++)
			{
				int sel = k;
				this.actionStateSelects[k].listActionEnter.Add(delegate
				{
					this.objFemaleStateSelectSels[sel].SetActiveIfDifferent(true);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
				this.actionStateSelects[k].listActionExit.Add(delegate
				{
					this.objFemaleStateSelectSels[sel].SetActiveIfDifferent(false);
				});
			}
			for (int l = 0; l < this.objFemaleStateSelectSels.Length; l++)
			{
				this.objFemaleStateSelectSels[l].SetActiveIfDifferent(false);
			}
			this.txtLifeStyle.text = string.Empty;
			this.txtGirlPower.text = "0";
			this.txtTrust.text = "0";
			this.txtHumanNature.text = "0";
			this.txtInstinct.text = "0";
			this.txtHentai.text = "0";
			this.txtVigilance.text = "0";
			this.txtSocial.text = "0";
			this.txtDarkness.text = "0";
			for (int m = 0; m < this.txtNormalSkillSlots.Length; m++)
			{
				this.txtNormalSkillSlots[m].text = "--------------------";
			}
			for (int n = 0; n < this.txtHSkillSlots.Length; n++)
			{
				this.txtHSkillSlots[n].text = "--------------------";
			}
			this.objSortWindow.SetActiveIfDifferent(false);
			this.btnSortWindowClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				this.objSortWindow.SetActiveIfDifferent(false);
			});
			this.btnSortWindowClose.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this.actionSortDay.listActionEnter.Add(delegate
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				this.objSortDaySelect.SetActiveIfDifferent(true);
			});
			this.actionSortDay.listActionExit.Add(delegate
			{
				this.objSortDaySelect.SetActiveIfDifferent(false);
			});
			this.actionSortName.listActionEnter.Add(delegate
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				this.objSortNameSelect.SetActiveIfDifferent(true);
			});
			this.actionSortName.listActionExit.Add(delegate
			{
				this.objSortNameSelect.SetActiveIfDifferent(false);
			});
			this.objSortDaySelect.SetActiveIfDifferent(false);
			this.objSortNameSelect.SetActiveIfDifferent(false);
			if (this.tglSortDay)
			{
				(from _ in this.tglSortDay.onValueChanged.AsObservable<bool>()
				where this.sortSelectNum != 0
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this.sortSelectNum = 0;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.SortDate(this.tglOrder.isOn);
					this.charaSelectScrollView.Init(this.lstFileInfo);
					this.charaSelectScrollView.SetNowSelectToggle();
				});
			}
			if (this.tglSortName)
			{
				(from _ in this.tglSortName.onValueChanged.AsObservable<bool>()
				where this.sortSelectNum != 1
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this.sortSelectNum = 1;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.SortName(this.tglOrder.isOn);
					this.charaSelectScrollView.Init(this.lstFileInfo);
					this.charaSelectScrollView.SetNowSelectToggle();
				});
			}
			this.LoadSkillList();
			this.LoadHSkillList();
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x0014785C File Offset: 0x00145C5C
		public void SortDate(bool ascend)
		{
			if (this.lstFileInfo.Count == 0)
			{
				return;
			}
			GameCharaFileInfo gameCharaFileInfo = this.lstFileInfo.Find((GameCharaFileInfo i) => i.personality == "不明");
			if (gameCharaFileInfo != null)
			{
				this.lstFileInfo.Remove(gameCharaFileInfo);
			}
			using (new GameSystem.CultureScope())
			{
				if (ascend)
				{
					this.lstFileInfo = (from n in this.lstFileInfo
					orderby n.time, n.name, n.personality
					select n).ToList<GameCharaFileInfo>();
				}
				else
				{
					this.lstFileInfo = (from n in this.lstFileInfo
					orderby n.time descending, n.name descending, n.personality descending
					select n).ToList<GameCharaFileInfo>();
				}
			}
			if (gameCharaFileInfo != null)
			{
				this.lstFileInfo.Insert(0, gameCharaFileInfo);
			}
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x001479EC File Offset: 0x00145DEC
		public void SortName(bool ascend)
		{
			if (this.lstFileInfo.Count == 0)
			{
				return;
			}
			GameCharaFileInfo gameCharaFileInfo = this.lstFileInfo.Find((GameCharaFileInfo i) => i.personality == "不明");
			if (gameCharaFileInfo != null)
			{
				this.lstFileInfo.Remove(gameCharaFileInfo);
			}
			using (new GameSystem.CultureScope())
			{
				if (ascend)
				{
					this.lstFileInfo = (from n in this.lstFileInfo
					orderby n.name, n.time, n.personality
					select n).ToList<GameCharaFileInfo>();
				}
				else
				{
					this.lstFileInfo = (from n in this.lstFileInfo
					orderby n.name descending, n.time descending, n.personality descending
					select n).ToList<GameCharaFileInfo>();
				}
			}
			if (gameCharaFileInfo != null)
			{
				this.lstFileInfo.Insert(0, gameCharaFileInfo);
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x00147B7C File Offset: 0x00145F7C
		public void SetNowSelectToggle()
		{
			if (this.charaSelectScrollView != null)
			{
				this.charaSelectScrollView.SetNowSelectToggle();
			}
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x00147B96 File Offset: 0x00145F96
		public void ClearList()
		{
			this.lstFileInfo.Clear();
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x00147BA3 File Offset: 0x00145FA3
		public void AddList(List<GameCharaFileInfo> list)
		{
			this.lstFileInfo = new List<GameCharaFileInfo>(list);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x00147BB1 File Offset: 0x00145FB1
		public GameCharaFileInfo GetNowSelectCard()
		{
			GameCharaFileScrollController.ScrollData selectInfo = this.charaSelectScrollView.selectInfo;
			return (selectInfo != null) ? selectInfo.info : null;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x00147BCD File Offset: 0x00145FCD
		public void InitSort()
		{
			this.tglOrder.SetIsOnWithoutCallback(false);
			this.imgOrder.enabled = !this.tglOrder.isOn;
			this.sortSelectNum = 0;
			this.tglSortDay.SetIsOnWithoutCallback(true);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x00147C08 File Offset: 0x00146008
		public void Create(bool _isSelectInfoClear)
		{
			if (this.tglSortDay.isOn)
			{
				this.SortDate(this.tglOrder.isOn);
			}
			else
			{
				this.SortName(this.tglOrder.isOn);
			}
			if (_isSelectInfoClear)
			{
				this.charaSelectScrollView.SelectInfoClear();
			}
			this.charaSelectScrollView.Init(this.lstFileInfo);
			if (_isSelectInfoClear)
			{
				this.charaSelectScrollView.SetTopLine();
			}
			this.objPlayerParameterWindow.SetActiveIfDifferent(false);
			this.objFemaleParameterWindow.SetActiveIfDifferent(false);
			this.objSortWindow.SetActiveIfDifferent(false);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x00147CA8 File Offset: 0x001460A8
		public void CreateListView(bool _isSelectInfoClear)
		{
			if (this.tglSortDay.isOn)
			{
				this.SortDate(this.tglOrder.isOn);
			}
			else
			{
				this.SortName(this.tglOrder.isOn);
			}
			if (_isSelectInfoClear)
			{
				this.charaSelectScrollView.SelectInfoClear();
				this.charaSelectScrollView.SetTopLine();
				this.objPlayerParameterWindow.SetActiveIfDifferent(false);
				this.objFemaleParameterWindow.SetActiveIfDifferent(false);
			}
			this.charaSelectScrollView.Init(this.lstFileInfo);
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x00147D33 File Offset: 0x00146133
		private void SetParameter(GameCharaFileInfo _data)
		{
			if (this.cfWindow.windowType == 0)
			{
				this.SetPlayerParameter(_data);
			}
			else
			{
				this.SetFemaleParameter(_data);
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x00147D58 File Offset: 0x00146158
		public void SetParameterWindowVisible(bool _isVisible)
		{
			if (this.cfWindow.windowType == 0)
			{
				this.objPlayerParameterWindow.SetActiveIfDifferent(_isVisible);
			}
			else
			{
				this.objFemaleParameterWindow.SetActiveIfDifferent(_isVisible);
			}
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x00147D89 File Offset: 0x00146189
		public void ParameterWindowVisibleNoTypeJudge(bool _isVisible)
		{
			this.objPlayerParameterWindow.SetActiveIfDifferent(_isVisible);
			this.objFemaleParameterWindow.SetActiveIfDifferent(_isVisible);
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x00147DA8 File Offset: 0x001461A8
		private void SetPlayerParameter(GameCharaFileInfo _data)
		{
			this.objPlayerParameterWindow.SetActiveIfDifferent(true);
			this.txtPlayerCharaName.text = _data.name;
			this.riPlayerCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(_data.FullPath), 0, 0, TextureFormat.ARGB32, false);
			int languageInt = Singleton<GameSystem>.Instance.languageInt;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((_data.sex != 0) ? this.localizeFemale[languageInt] : this.localizeMale[languageInt]);
			if (_data.sex == 1 && _data.futanari)
			{
				stringBuilder.Append(this.localizeFutanari[languageInt]);
			}
			this.txtPlayerSex.text = stringBuilder.ToString();
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x00147E64 File Offset: 0x00146264
		private void SetFemaleParameter(GameCharaFileInfo _data)
		{
			if (!_data.FullPath.IsNullOrEmpty())
			{
				bool activeSelf = this.objFemaleParameterWindow.activeSelf;
				this.objFemaleParameterWindow.SetActiveIfDifferent(true);
				if (!activeSelf)
				{
					this.femaleParameterSelectNum = 0;
					this.tglFemaleStateSelects[0].SetIsOnWithoutCallback(true);
					for (int i = 0; i < this.objFemalParameterRoots.Length; i++)
					{
						this.objFemalParameterRoots[i].SetActiveIfDifferent(i == 0);
					}
				}
				this.txtFemaleCharaName.text = _data.name;
				this.riFemaleCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(_data.FullPath), 0, 0, TextureFormat.ARGB32, false);
				for (int j = 0; j < this.sccStateOfProgress.Length; j++)
				{
					this.sccStateOfProgress[j].OnChangeValue((_data.phase >= j) ? 1 : 0);
				}
				string text = string.Empty;
				if (Lifestyle.LifestyleName.TryGetValue(_data.lifeStyle, out text))
				{
					text = ((_data.lifeStyle != 4) ? text : "E・シーカー");
				}
				else
				{
					text = "--------------------";
				}
				this.txtLifeStyle.text = text;
				string text2 = "--------";
				this.txtGirlPower.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[0].ToString());
				this.txtTrust.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[1].ToString());
				this.txtHumanNature.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[2].ToString());
				this.txtInstinct.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[3].ToString());
				this.txtHentai.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[4].ToString());
				this.txtVigilance.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[5].ToString());
				this.txtSocial.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[7].ToString());
				this.txtDarkness.text = ((!_data.gameRegistration) ? text2 : _data.flavorState[6].ToString());
				int languageInt = Singleton<GameSystem>.Instance.languageInt;
				for (int k = 0; k < this.txtNormalSkillSlots.Length; k++)
				{
					string text3 = "--------------------";
					if (_data.normalSkill.ContainsKey(k))
					{
						List<string> list;
						text3 = ((!this.dicSkill.TryGetValue(_data.normalSkill[k], out list)) ? "--------------------" : list[languageInt]);
					}
					this.txtNormalSkillSlots[k].text = text3;
				}
				for (int l = 0; l < this.txtHSkillSlots.Length; l++)
				{
					string text4 = "--------------------";
					if (_data.hSkill.ContainsKey(l))
					{
						List<string> list;
						text4 = ((!this.dicHSkill.TryGetValue(_data.hSkill[l], out list)) ? "--------------------" : list[languageInt]);
					}
					this.txtHSkillSlots[l].text = text4;
				}
			}
			else
			{
				this.txtFemaleCharaName.text = string.Empty;
				this.riFemaleCard.texture = this.texEmpty;
				for (int m = 0; m < this.sccStateOfProgress.Length; m++)
				{
					this.sccStateOfProgress[m].OnChangeValue((_data.phase >= m) ? 1 : 0);
				}
				this.txtLifeStyle.text = string.Empty;
				this.txtGirlPower.text = string.Empty;
				this.txtTrust.text = string.Empty;
				this.txtHumanNature.text = string.Empty;
				this.txtInstinct.text = string.Empty;
				this.txtHentai.text = string.Empty;
				this.txtVigilance.text = string.Empty;
				this.txtSocial.text = string.Empty;
				this.txtDarkness.text = string.Empty;
				for (int n = 0; n < this.txtNormalSkillSlots.Length; n++)
				{
					string text5 = "--------------------";
					this.txtNormalSkillSlots[n].text = text5;
				}
				for (int num = 0; num < this.txtHSkillSlots.Length; num++)
				{
					string text6 = "--------------------";
					this.txtHSkillSlots[num].text = text6;
				}
			}
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x001483BC File Offset: 0x001467BC
		private bool LoadSkillList()
		{
			string assetBundleName = this.listAssetBundleName;
			TitleSkillName titleSkillName = CommonLib.LoadAsset<TitleSkillName>(assetBundleName, "title_skill", false, string.Empty);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			foreach (TitleSkillName.Param param in titleSkillName.param)
			{
				if (!this.dicSkill.ContainsKey(param.id))
				{
					this.dicSkill[param.id] = new List<string>();
				}
				List<string> list = this.dicSkill[param.id];
				list.Add(param.name0);
				list.Add(param.name1);
				list.Add(param.name2);
				list.Add(param.name3);
				list.Add(param.name4);
			}
			return true;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x001484B4 File Offset: 0x001468B4
		private bool LoadHSkillList()
		{
			string assetBundleName = this.listAssetBundleName;
			TitleSkillName titleSkillName = CommonLib.LoadAsset<TitleSkillName>(assetBundleName, "title_h_skill", false, string.Empty);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			foreach (TitleSkillName.Param param in titleSkillName.param)
			{
				if (!this.dicHSkill.ContainsKey(param.id))
				{
					this.dicHSkill[param.id] = new List<string>();
				}
				List<string> list = this.dicHSkill[param.id];
				list.Add(param.name0);
				list.Add(param.name1);
				list.Add(param.name2);
				list.Add(param.name3);
				list.Add(param.name4);
			}
			return true;
		}

		// Token: 0x040037D9 RID: 14297
		[SerializeField]
		private GameLoadCharaWindow cfWindow;

		// Token: 0x040037DA RID: 14298
		[Header("メインWindow")]
		[SerializeField]
		private Toggle tglOrder;

		// Token: 0x040037DB RID: 14299
		[SerializeField]
		private Image imgOrder;

		// Token: 0x040037DC RID: 14300
		[SerializeField]
		private PointerEnterExitAction actionOrderSelect;

		// Token: 0x040037DD RID: 14301
		[SerializeField]
		private GameObject objOrderSelect;

		// Token: 0x040037DE RID: 14302
		[SerializeField]
		private Button btnSortWindowOpen;

		// Token: 0x040037DF RID: 14303
		[SerializeField]
		private GameCharaFileScrollController charaSelectScrollView = new GameCharaFileScrollController();

		// Token: 0x040037E0 RID: 14304
		[Header("プレイヤーパラメーターWindow")]
		[SerializeField]
		private GameObject objPlayerParameterWindow;

		// Token: 0x040037E1 RID: 14305
		[SerializeField]
		private Text txtPlayerCharaName;

		// Token: 0x040037E2 RID: 14306
		[SerializeField]
		private RawImage riPlayerCard;

		// Token: 0x040037E3 RID: 14307
		[SerializeField]
		private Text txtPlayerSex;

		// Token: 0x040037E4 RID: 14308
		[SerializeField]
		private Texture2D texEmpty;

		// Token: 0x040037E5 RID: 14309
		[Header("女の子パラメーターWindow")]
		[SerializeField]
		private GameObject objFemaleParameterWindow;

		// Token: 0x040037E6 RID: 14310
		[SerializeField]
		private Text txtFemaleCharaName;

		// Token: 0x040037E7 RID: 14311
		[SerializeField]
		private RawImage riFemaleCard;

		// Token: 0x040037E8 RID: 14312
		[SerializeField]
		private SpriteChangeCtrl[] sccStateOfProgress;

		// Token: 0x040037E9 RID: 14313
		[SerializeField]
		private Toggle[] tglFemaleStateSelects;

		// Token: 0x040037EA RID: 14314
		[SerializeField]
		private PointerEnterExitAction[] actionStateSelects;

		// Token: 0x040037EB RID: 14315
		[SerializeField]
		private GameObject[] objFemaleStateSelectSels;

		// Token: 0x040037EC RID: 14316
		[SerializeField]
		private GameObject[] objFemalParameterRoots;

		// Token: 0x040037ED RID: 14317
		[Header("女の子パラメーター")]
		[SerializeField]
		private Text txtLifeStyle;

		// Token: 0x040037EE RID: 14318
		[SerializeField]
		private Text txtGirlPower;

		// Token: 0x040037EF RID: 14319
		[SerializeField]
		private Text txtTrust;

		// Token: 0x040037F0 RID: 14320
		[SerializeField]
		private Text txtHumanNature;

		// Token: 0x040037F1 RID: 14321
		[SerializeField]
		private Text txtInstinct;

		// Token: 0x040037F2 RID: 14322
		[SerializeField]
		private Text txtHentai;

		// Token: 0x040037F3 RID: 14323
		[SerializeField]
		private Text txtVigilance;

		// Token: 0x040037F4 RID: 14324
		[SerializeField]
		private Text txtSocial;

		// Token: 0x040037F5 RID: 14325
		[SerializeField]
		private Text txtDarkness;

		// Token: 0x040037F6 RID: 14326
		[SerializeField]
		private Text[] txtNormalSkillSlots;

		// Token: 0x040037F7 RID: 14327
		[SerializeField]
		private Text[] txtHSkillSlots;

		// Token: 0x040037F8 RID: 14328
		[Header("ソートWindow")]
		[SerializeField]
		private GameObject objSortWindow;

		// Token: 0x040037F9 RID: 14329
		[SerializeField]
		private Button btnSortWindowClose;

		// Token: 0x040037FA RID: 14330
		[SerializeField]
		private Toggle tglSortDay;

		// Token: 0x040037FB RID: 14331
		[SerializeField]
		private Toggle tglSortName;

		// Token: 0x040037FC RID: 14332
		[SerializeField]
		private PointerEnterExitAction actionSortDay;

		// Token: 0x040037FD RID: 14333
		[SerializeField]
		private PointerEnterExitAction actionSortName;

		// Token: 0x040037FE RID: 14334
		[SerializeField]
		private GameObject objSortDaySelect;

		// Token: 0x040037FF RID: 14335
		[SerializeField]
		private GameObject objSortNameSelect;

		// Token: 0x04003800 RID: 14336
		private readonly string[] localizeMale = new string[]
		{
			"男",
			"Male",
			"Male",
			"Male",
			string.Empty
		};

		// Token: 0x04003801 RID: 14337
		private readonly string[] localizeFemale = new string[]
		{
			"女",
			"Female",
			"Female",
			"Female",
			string.Empty
		};

		// Token: 0x04003802 RID: 14338
		private readonly string[] localizeFutanari = new string[]
		{
			"（フタナリ）",
			"(Futanari)",
			"(Futanari)",
			"(Futanari)",
			string.Empty
		};

		// Token: 0x04003803 RID: 14339
		private readonly string listAssetBundleName = "list/title.unity3d";

		// Token: 0x04003804 RID: 14340
		private List<GameCharaFileInfo> lstFileInfo = new List<GameCharaFileInfo>();

		// Token: 0x04003805 RID: 14341
		[HideInInspector]
		public bool updateCategory;

		// Token: 0x04003806 RID: 14342
		public GameLoadCharaListCtrl.OnChangeItemFunc onChangeItemFunc;

		// Token: 0x04003807 RID: 14343
		public Action<bool> onChangeItem;

		// Token: 0x04003808 RID: 14344
		private int sortSelectNum;

		// Token: 0x04003809 RID: 14345
		private int femaleParameterSelectNum;

		// Token: 0x0400380A RID: 14346
		private Dictionary<int, List<string>> dicSkill = new Dictionary<int, List<string>>();

		// Token: 0x0400380B RID: 14347
		private Dictionary<int, List<string>> dicHSkill = new Dictionary<int, List<string>>();

		// Token: 0x0200087B RID: 2171
		// (Invoke) Token: 0x0600376E RID: 14190
		public delegate void OnChangeItemFunc(GameCharaFileInfo info);
	}
}
