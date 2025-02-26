using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIChara;
using AIProject;
using Illusion.Extensions;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02000FFE RID: 4094
	public class DownUIControl : MonoBehaviour
	{
		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x060089B1 RID: 35249 RVA: 0x0039B46A File Offset: 0x0039986A
		private NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x060089B2 RID: 35250 RVA: 0x0039B471 File Offset: 0x00399871
		private NetCacheControl cacheCtrl
		{
			get
			{
				return (!Singleton<NetworkInfo>.IsInstance()) ? null : this.netInfo.cacheCtrl;
			}
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x060089B3 RID: 35251 RVA: 0x0039B48E File Offset: 0x0039988E
		// (set) Token: 0x060089B4 RID: 35252 RVA: 0x0039B49B File Offset: 0x0039989B
		public int searchSortHNIdx
		{
			get
			{
				return this._searchSortHNIdx.Value;
			}
			set
			{
				this._searchSortHNIdx.Value = value;
			}
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x060089B5 RID: 35253 RVA: 0x0039B4A9 File Offset: 0x003998A9
		// (set) Token: 0x060089B6 RID: 35254 RVA: 0x0039B4B6 File Offset: 0x003998B6
		public int pageMax
		{
			get
			{
				return this._pageMax.Value;
			}
			set
			{
				this._pageMax.Value = value;
			}
		}

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x060089B7 RID: 35255 RVA: 0x0039B4C4 File Offset: 0x003998C4
		// (set) Token: 0x060089B8 RID: 35256 RVA: 0x0039B4D1 File Offset: 0x003998D1
		public int pageNow
		{
			get
			{
				return this._pageNow.Value;
			}
			set
			{
				this._pageNow.Value = value;
			}
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x060089B9 RID: 35257 RVA: 0x0039B4DF File Offset: 0x003998DF
		// (set) Token: 0x060089BA RID: 35258 RVA: 0x0039B4EC File Offset: 0x003998EC
		public int mainMode
		{
			get
			{
				return this._mainMode.Value;
			}
			set
			{
				this._mainMode.Value = value;
			}
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x060089BB RID: 35259 RVA: 0x0039B4FA File Offset: 0x003998FA
		// (set) Token: 0x060089BC RID: 35260 RVA: 0x0039B507 File Offset: 0x00399907
		public int dataType
		{
			get
			{
				return this._dataType.Value;
			}
			set
			{
				this._dataType.Value = value;
			}
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x060089BD RID: 35261 RVA: 0x0039B515 File Offset: 0x00399915
		// (set) Token: 0x060089BE RID: 35262 RVA: 0x0039B522 File Offset: 0x00399922
		public bool updateCharaInfo
		{
			get
			{
				return this._updateCharaInfo.Value;
			}
			set
			{
				this._updateCharaInfo.Value = value;
			}
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x060089BF RID: 35263 RVA: 0x0039B530 File Offset: 0x00399930
		// (set) Token: 0x060089C0 RID: 35264 RVA: 0x0039B53D File Offset: 0x0039993D
		public bool updateHousingInfo
		{
			get
			{
				return this._updateHousingInfo.Value;
			}
			set
			{
				this._updateHousingInfo.Value = value;
			}
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x060089C1 RID: 35265 RVA: 0x0039B54B File Offset: 0x0039994B
		// (set) Token: 0x060089C2 RID: 35266 RVA: 0x0039B558 File Offset: 0x00399958
		public bool updateAllInfo
		{
			get
			{
				return this._updateAllInfo.Value;
			}
			set
			{
				this._updateAllInfo.Value = value;
			}
		}

		// Token: 0x060089C3 RID: 35267 RVA: 0x0039B566 File Offset: 0x00399966
		public void ShowNewestVersion()
		{
			if (this.textNewestVersion)
			{
				this.textNewestVersion.gameObject.SetActiveIfDifferent(true);
			}
		}

		// Token: 0x060089C4 RID: 35268 RVA: 0x0039B58C File Offset: 0x0039998C
		public void UpdateInfoChara()
		{
			NetworkInfo.CharaInfo charaInfo = null;
			string text = string.Empty;
			DownUIControl.MainMode mainMode = (DownUIControl.MainMode)this.mainMode;
			if (mainMode == DownUIControl.MainMode.MM_Download || mainMode == DownUIControl.MainMode.MM_MyData)
			{
				this.selInfoCha.hnUserIndex = -1;
				if (this.downloadSel.selChara != -1)
				{
					if (this.lstSearchCharaInfo.Count > this.downloadSel.selChara)
					{
						charaInfo = this.lstSearchCharaInfo[this.downloadSel.selChara];
						text = this.GetHandleNameFromUserIndex(charaInfo.user_idx);
						this.selInfoCha.hnUserIndex = charaInfo.user_idx;
					}
				}
			}
			if (charaInfo == null)
			{
				if (this.selInfoCha.textHN)
				{
					this.selInfoCha.textHN.text = string.Empty;
				}
				if (null != this.selInfoCha.textName)
				{
					this.selInfoCha.textName.text = string.Empty;
				}
				if (null != this.selInfoCha.textType)
				{
					this.selInfoCha.textType.text = string.Empty;
				}
				if (null != this.selInfoCha.textBirthDay)
				{
					this.selInfoCha.textBirthDay.text = string.Empty;
				}
				if (null != this.selInfoCha.objOnPhase01)
				{
					this.selInfoCha.objOnPhase01.SetActiveIfDifferent(false);
				}
				if (null != this.selInfoCha.objOnPhase02)
				{
					this.selInfoCha.objOnPhase02.SetActiveIfDifferent(false);
				}
				if (null != this.selInfoCha.objOnPhase03)
				{
					this.selInfoCha.objOnPhase03.SetActiveIfDifferent(false);
				}
				if (null != this.selInfoCha.objOnPhase04)
				{
					this.selInfoCha.objOnPhase04.SetActiveIfDifferent(false);
				}
				if (null != this.selInfoCha.textLifeStyle)
				{
					this.selInfoCha.textLifeStyle.text = string.Empty;
				}
				if (null != this.selInfoCha.textPheromone)
				{
					this.selInfoCha.textPheromone.text = string.Empty;
				}
				if (null != this.selInfoCha.textReliability)
				{
					this.selInfoCha.textReliability.text = string.Empty;
				}
				if (null != this.selInfoCha.textReason)
				{
					this.selInfoCha.textReason.text = string.Empty;
				}
				if (null != this.selInfoCha.textInstinct)
				{
					this.selInfoCha.textInstinct.text = string.Empty;
				}
				if (null != this.selInfoCha.textDirty)
				{
					this.selInfoCha.textDirty.text = string.Empty;
				}
				if (null != this.selInfoCha.textWariness)
				{
					this.selInfoCha.textWariness.text = string.Empty;
				}
				if (null != this.selInfoCha.textSociability)
				{
					this.selInfoCha.textSociability.text = string.Empty;
				}
				if (null != this.selInfoCha.textDarkness)
				{
					this.selInfoCha.textDarkness.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_n01)
				{
					this.selInfoCha.textSkill_n01.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_n02)
				{
					this.selInfoCha.textSkill_n02.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_n03)
				{
					this.selInfoCha.textSkill_n03.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_n04)
				{
					this.selInfoCha.textSkill_n04.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_n05)
				{
					this.selInfoCha.textSkill_n05.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_h01)
				{
					this.selInfoCha.textSkill_h01.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_h02)
				{
					this.selInfoCha.textSkill_h02.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_h03)
				{
					this.selInfoCha.textSkill_h03.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_h04)
				{
					this.selInfoCha.textSkill_h04.text = string.Empty;
				}
				if (null != this.selInfoCha.textSkill_h05)
				{
					this.selInfoCha.textSkill_h05.text = string.Empty;
				}
				if (null != this.selInfoCha.textWish_01)
				{
					this.selInfoCha.textWish_01.text = string.Empty;
				}
				if (null != this.selInfoCha.textWish_02)
				{
					this.selInfoCha.textWish_02.text = string.Empty;
				}
				if (null != this.selInfoCha.textWish_03)
				{
					this.selInfoCha.textWish_03.text = string.Empty;
				}
				if (this.selInfoCha.textComment)
				{
					this.selInfoCha.textComment.text = string.Empty;
				}
			}
			else
			{
				bool flag = 1 == charaInfo.sex;
				if (this.selInfoCha.textHN)
				{
					this.selInfoCha.textHN.text = text;
				}
				if (null != this.selInfoCha.textName)
				{
					this.selInfoCha.textName.text = charaInfo.name;
				}
				if (null != this.selInfoCha.textType)
				{
					this.selInfoCha.textType.text = (flag ? Singleton<Character>.Instance.GetCharaTypeName(charaInfo.type) : string.Empty);
				}
				if (null != this.selInfoCha.textBirthDay)
				{
					this.selInfoCha.textBirthDay.text = charaInfo.strBirthDay;
				}
				if (null != this.selInfoCha.objOnPhase01)
				{
					this.selInfoCha.objOnPhase01.SetActiveIfDifferent(flag);
				}
				if (null != this.selInfoCha.objOnPhase02)
				{
					this.selInfoCha.objOnPhase02.SetActiveIfDifferent(charaInfo.phase >= 1 && flag);
				}
				if (null != this.selInfoCha.objOnPhase03)
				{
					this.selInfoCha.objOnPhase03.SetActiveIfDifferent(charaInfo.phase >= 2 && flag);
				}
				if (null != this.selInfoCha.objOnPhase04)
				{
					this.selInfoCha.objOnPhase04.SetActiveIfDifferent(charaInfo.phase == 3 && flag);
				}
				if (null != this.selInfoCha.textLifeStyle)
				{
					this.selInfoCha.textLifeStyle.text = ((charaInfo.lifestyle != -1 && flag) ? this.netInfo.GetLifeStyleName(charaInfo.lifestyle) : "---------------");
				}
				if (null != this.selInfoCha.textPheromone)
				{
					this.selInfoCha.textPheromone.text = ((charaInfo.registration != 0 && flag) ? charaInfo.pheromone.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textReliability)
				{
					this.selInfoCha.textReliability.text = ((charaInfo.registration != 0 && flag) ? charaInfo.reliability.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textReason)
				{
					this.selInfoCha.textReason.text = ((charaInfo.registration != 0 && flag) ? charaInfo.reason.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textInstinct)
				{
					this.selInfoCha.textInstinct.text = ((charaInfo.registration != 0 && flag) ? charaInfo.instinct.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textDirty)
				{
					this.selInfoCha.textDirty.text = ((charaInfo.registration != 0 && flag) ? charaInfo.dirty.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textWariness)
				{
					this.selInfoCha.textWariness.text = ((charaInfo.registration != 0 && flag) ? charaInfo.wariness.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textSociability)
				{
					this.selInfoCha.textSociability.text = ((charaInfo.registration != 0 && flag) ? charaInfo.sociability.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textDarkness)
				{
					this.selInfoCha.textDarkness.text = ((charaInfo.registration != 0 && flag) ? charaInfo.darkness.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n01)
				{
					this.selInfoCha.textSkill_n01.text = ((charaInfo.skill_n01 != -1 && flag) ? this.netInfo.GetNormalSkillName(charaInfo.skill_n01) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n02)
				{
					this.selInfoCha.textSkill_n02.text = ((charaInfo.skill_n02 != -1 && flag) ? this.netInfo.GetNormalSkillName(charaInfo.skill_n02) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n03)
				{
					this.selInfoCha.textSkill_n03.text = ((charaInfo.skill_n03 != -1 && flag) ? this.netInfo.GetNormalSkillName(charaInfo.skill_n03) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n04)
				{
					this.selInfoCha.textSkill_n04.text = ((charaInfo.skill_n04 != -1 && flag) ? this.netInfo.GetNormalSkillName(charaInfo.skill_n04) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n05)
				{
					this.selInfoCha.textSkill_n05.text = ((charaInfo.skill_n05 != -1 && flag) ? this.netInfo.GetNormalSkillName(charaInfo.skill_n05) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h01)
				{
					this.selInfoCha.textSkill_h01.text = ((charaInfo.skill_h01 != -1 && flag) ? this.netInfo.GetHSkillName(charaInfo.skill_h01) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h02)
				{
					this.selInfoCha.textSkill_h02.text = ((charaInfo.skill_h02 != -1 && flag) ? this.netInfo.GetHSkillName(charaInfo.skill_h02) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h03)
				{
					this.selInfoCha.textSkill_h03.text = ((charaInfo.skill_h03 != -1 && flag) ? this.netInfo.GetHSkillName(charaInfo.skill_h03) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h04)
				{
					this.selInfoCha.textSkill_h04.text = ((charaInfo.skill_h04 != -1 && flag) ? this.netInfo.GetHSkillName(charaInfo.skill_h04) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h05)
				{
					this.selInfoCha.textSkill_h05.text = ((charaInfo.skill_h05 != -1 && flag) ? this.netInfo.GetHSkillName(charaInfo.skill_h05) : "---------------");
				}
				Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.init_wish_param);
				if (null != this.selInfoCha.textWish_01)
				{
					string text2 = "---------------";
					ListInfoBase listInfoBase;
					if (charaInfo.wish_01 != -1 && flag && categoryInfo.TryGetValue(charaInfo.wish_01, out listInfoBase))
					{
						text2 = listInfoBase.Name;
					}
					this.selInfoCha.textWish_01.text = text2;
				}
				if (null != this.selInfoCha.textWish_02)
				{
					string text3 = "---------------";
					ListInfoBase listInfoBase2;
					if (charaInfo.wish_02 != -1 && flag && categoryInfo.TryGetValue(charaInfo.wish_02, out listInfoBase2))
					{
						text3 = listInfoBase2.Name;
					}
					this.selInfoCha.textWish_02.text = text3;
				}
				if (null != this.selInfoCha.textWish_03)
				{
					string text4 = "---------------";
					ListInfoBase listInfoBase3;
					if (charaInfo.wish_03 != -1 && flag && categoryInfo.TryGetValue(charaInfo.wish_03, out listInfoBase3))
					{
						text4 = listInfoBase3.Name;
					}
					this.selInfoCha.textWish_03.text = text4;
				}
				if (this.selInfoCha.textComment)
				{
					this.selInfoCha.textComment.text = charaInfo.comment;
				}
			}
		}

		// Token: 0x060089C5 RID: 35269 RVA: 0x0039C480 File Offset: 0x0039A880
		public void UpdateInfoHousing()
		{
			NetworkInfo.HousingInfo housinginfo = null;
			string text = string.Empty;
			DownUIControl.MainMode mainMode = (DownUIControl.MainMode)this.mainMode;
			if (mainMode == DownUIControl.MainMode.MM_Download || mainMode == DownUIControl.MainMode.MM_MyData)
			{
				this.selInfoHousing.hnUserIndex = -1;
				if (this.downloadSel.selHousing != -1)
				{
					if (this.lstSearchHousingInfo.Count > this.downloadSel.selHousing)
					{
						housinginfo = this.lstSearchHousingInfo[this.downloadSel.selHousing];
						text = this.GetHandleNameFromUserIndex(housinginfo.user_idx);
						this.selInfoHousing.hnUserIndex = housinginfo.user_idx;
					}
				}
			}
			if (housinginfo == null)
			{
				if (this.selInfoHousing.textHN)
				{
					this.selInfoHousing.textHN.text = string.Empty;
				}
				if (this.selInfoHousing.textName)
				{
					this.selInfoHousing.textName.text = string.Empty;
				}
				if (this.selInfoHousing.textMapSize)
				{
					this.selInfoHousing.textMapSize.text = string.Empty;
				}
				if (this.selInfoHousing.textComment)
				{
					this.selInfoHousing.textComment.text = string.Empty;
				}
			}
			else
			{
				if (this.selInfoHousing.textHN)
				{
					this.selInfoHousing.textHN.text = text;
				}
				if (this.selInfoHousing.textName)
				{
					this.selInfoHousing.textName.text = housinginfo.name;
				}
				if (this.selInfoHousing.textMapSize)
				{
					KeyValuePair<int, Tuple<int, string>> keyValuePair = this.dictMapSizeInfo.FirstOrDefault((KeyValuePair<int, Tuple<int, string>> x) => x.Value.Item1 == housinginfo.mapSize);
					if (keyValuePair.Equals(null))
					{
						this.selInfoHousing.textMapSize.text = "不明";
					}
					else
					{
						this.selInfoHousing.textMapSize.text = keyValuePair.Value.Item2;
					}
				}
				if (this.selInfoHousing.textComment)
				{
					this.selInfoHousing.textComment.text = housinginfo.comment;
				}
			}
		}

		// Token: 0x060089C6 RID: 35270 RVA: 0x0039C700 File Offset: 0x0039AB00
		public void UpdateCharaSearchList()
		{
			int num = -1;
			int selChara = this.downloadSel.selChara;
			if (this.downloadSel.selChara != -1)
			{
				num = this.lstSearchCharaInfo[this.downloadSel.selChara].idx;
			}
			this.lstSearchCharaInfo.Clear();
			bool[] array = new bool[2];
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				if (!(null == this.searchItem.tglSex[i]))
				{
					array[i] = this.searchItem.tglSex[i].isOn;
					if (array[i])
					{
						flag = true;
					}
				}
			}
			bool[] array2 = new bool[3];
			bool flag2 = false;
			for (int j = 0; j < 3; j++)
			{
				if (!(null == this.searchItem.tglHeight[j]))
				{
					array2[j] = this.searchItem.tglHeight[j].isOn;
					if (array2[j])
					{
						flag2 = true;
					}
				}
			}
			bool[] array3 = new bool[3];
			bool flag3 = false;
			for (int k = 0; k < 3; k++)
			{
				if (!(null == this.searchItem.tglBust[k]))
				{
					array3[k] = this.searchItem.tglBust[k].isOn;
					if (array3[k])
					{
						flag3 = true;
					}
				}
			}
			bool[] array4 = new bool[6];
			bool flag4 = false;
			for (int l = 0; l < 6; l++)
			{
				if (!(null == this.searchItem.tglHair[l]))
				{
					array4[l] = this.searchItem.tglHair[l].isOn;
					if (array4[l])
					{
						flag4 = true;
					}
				}
			}
			bool flag5 = false;
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, int> keyValuePair in this.dictVoiceInfo)
			{
				int key = keyValuePair.Key;
				if (!(null == this.searchItem.tglVoice[key]))
				{
					if (this.searchItem.tglVoice[key].isOn)
					{
						list.Add(keyValuePair.Value);
						flag5 = true;
					}
				}
			}
			int num2;
			if (this.mainMode == 1)
			{
				num2 = this.netInfo.profile.userIdx;
			}
			else
			{
				num2 = this.searchSortHNIdx;
			}
			int count = this.netInfo.lstCharaInfo.Count;
			for (int m = 0; m < count; m++)
			{
				NetworkInfo.CharaInfo charaInfo = this.netInfo.lstCharaInfo[m];
				if (MathfEx.IsRange<int>(0, charaInfo.sex, 1, true))
				{
					if (!flag || array[charaInfo.sex])
					{
						if (charaInfo.sex != 0 || (!flag5 && !flag3 && !flag2))
						{
							if (MathfEx.IsRange<int>(0, charaInfo.height, array2.Length - 1, true))
							{
								if (!flag2 || array2[charaInfo.height])
								{
									if (charaInfo.sex != 1 || MathfEx.IsRange<int>(0, charaInfo.bust, array3.Length - 1, true))
									{
										if (!flag3 || array3[charaInfo.bust])
										{
											if (MathfEx.IsRange<int>(0, charaInfo.hair, array4.Length - 1, true))
											{
												if (!flag4 || array4[charaInfo.hair])
												{
													if (!flag5 || list.Contains(charaInfo.type))
													{
														if (num2 == -1 || num2 == charaInfo.user_idx)
														{
															if (this.searchItem.textKeywordDummy.text != string.Empty)
															{
																string text = this.searchItem.textKeywordDummy.text;
																if (!charaInfo.name.Contains(text) && !charaInfo.comment.Contains(text))
																{
																	goto IL_474;
																}
															}
															this.lstSearchCharaInfo.Add(charaInfo);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				IL_474:;
			}
			this.UpdateSortChara();
			this.UpdatePageMax();
			this.downloadSel.selChara = -1;
			for (int n = 0; n < this.lstSearchCharaInfo.Count; n++)
			{
				if (this.lstSearchCharaInfo[n].idx == num)
				{
					this.downloadSel.selChara = n;
					break;
				}
			}
			int num3 = this.CheckSelectInPage(this.downloadSel.selChara);
			if (num3 == -1)
			{
				this.downloadSel.selChara = -1;
				if (selChara != -1)
				{
					foreach (NetFileComponent netFileComponent in this.pageDataInfo.nfcChara)
					{
						netFileComponent.tglItem.isOn = false;
					}
				}
			}
			else
			{
				this.pageDataInfo.nfcChara[num3].tglItem.isOn = true;
			}
			this.UpdateInfoChara();
			this.UpdatePage();
		}

		// Token: 0x060089C7 RID: 35271 RVA: 0x0039CC94 File Offset: 0x0039B094
		public void UpdateSortChara()
		{
			switch (this.searchSortSort)
			{
			case 0:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.createTime descending, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			case 1:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.createTime, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			case 2:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.updateTime descending, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			case 3:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.updateTime, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			case 4:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.rankingT, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			case 5:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.rankingW, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			case 6:
				this.lstSearchCharaInfo = (from n in this.lstSearchCharaInfo
				orderby n.applause descending, n.idx
				select n).ToList<NetworkInfo.CharaInfo>();
				break;
			}
		}

		// Token: 0x060089C8 RID: 35272 RVA: 0x0039CF4C File Offset: 0x0039B34C
		public void UpdateHousingSearchList()
		{
			int num = -1;
			int selHousing = this.downloadSel.selHousing;
			if (this.downloadSel.selHousing != -1)
			{
				num = this.lstSearchHousingInfo[this.downloadSel.selHousing].idx;
			}
			this.lstSearchHousingInfo.Clear();
			bool flag = false;
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<int> hashSet2 = new HashSet<int>();
			foreach (KeyValuePair<int, Tuple<int, string>> keyValuePair in this.dictMapSizeInfo)
			{
				int key = keyValuePair.Key;
				if (!(null == this.searchItem.tglMapSize[key]))
				{
					hashSet.Add(keyValuePair.Value.Item1);
					if (this.searchItem.tglMapSize[key].isOn)
					{
						hashSet2.Add(keyValuePair.Value.Item1);
						flag = true;
					}
				}
			}
			int num2;
			if (this.mainMode == 1)
			{
				num2 = this.netInfo.profile.userIdx;
			}
			else
			{
				num2 = this.searchSortHNIdx;
			}
			int count = this.netInfo.lstHousingInfo.Count;
			int i = 0;
			while (i < count)
			{
				NetworkInfo.HousingInfo housingInfo = this.netInfo.lstHousingInfo[i];
				if (flag)
				{
					if (hashSet2.Contains(housingInfo.mapSize))
					{
						goto IL_18D;
					}
				}
				else if (hashSet.Contains(housingInfo.mapSize))
				{
					goto IL_18D;
				}
				IL_211:
				i++;
				continue;
				IL_18D:
				if (num2 != -1 && num2 != housingInfo.user_idx)
				{
					goto IL_211;
				}
				if (this.searchItem.textKeywordDummy.text != string.Empty)
				{
					string text = this.searchItem.textKeywordDummy.text;
					if (!housingInfo.name.Contains(text) && !housingInfo.comment.Contains(text))
					{
						goto IL_211;
					}
				}
				this.lstSearchHousingInfo.Add(housingInfo);
				goto IL_211;
			}
			this.UpdateSortScene();
			this.UpdatePageMax();
			this.downloadSel.selHousing = -1;
			for (int j = 0; j < this.lstSearchHousingInfo.Count; j++)
			{
				if (this.lstSearchHousingInfo[j].idx == num)
				{
					this.downloadSel.selHousing = j;
					break;
				}
			}
			int num3 = this.CheckSelectInPage(this.downloadSel.selHousing);
			if (num3 == -1)
			{
				this.downloadSel.selHousing = -1;
				if (selHousing != -1)
				{
					foreach (NetFileComponent netFileComponent in this.pageDataInfo.nfcHousing)
					{
						netFileComponent.tglItem.isOn = false;
					}
				}
			}
			else
			{
				this.pageDataInfo.nfcHousing[num3].tglItem.isOn = true;
			}
			this.UpdateInfoHousing();
			this.UpdatePage();
		}

		// Token: 0x060089C9 RID: 35273 RVA: 0x0039D280 File Offset: 0x0039B680
		public void UpdateSortScene()
		{
			switch (this.searchSortSort)
			{
			case 0:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.createTime descending, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			case 1:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.createTime, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			case 2:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.updateTime descending, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			case 3:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.updateTime, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			case 4:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.rankingT, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			case 5:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.rankingW, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			case 6:
				this.lstSearchHousingInfo = (from n in this.lstSearchHousingInfo
				orderby n.applause descending, n.idx
				select n).ToList<NetworkInfo.HousingInfo>();
				break;
			}
		}

		// Token: 0x060089CA RID: 35274 RVA: 0x0039D534 File Offset: 0x0039B934
		private int GetPageDataCount()
		{
			int dataType = this.dataType;
			if (dataType == 0)
			{
				return this.lstSearchCharaInfo.Count;
			}
			if (dataType != 1)
			{
				return 0;
			}
			return this.lstSearchHousingInfo.Count;
		}

		// Token: 0x060089CB RID: 35275 RVA: 0x0039D574 File Offset: 0x0039B974
		private int GetPageDrawCount()
		{
			int dataType = this.dataType;
			if (dataType == 0)
			{
				return this.pageDataInfo.nfcChara.Length;
			}
			if (dataType != 1)
			{
				return 0;
			}
			return this.pageDataInfo.nfcHousing.Length;
		}

		// Token: 0x060089CC RID: 35276 RVA: 0x0039D5B8 File Offset: 0x0039B9B8
		private NetFileComponent[] GetPageFileComponents()
		{
			int dataType = this.dataType;
			if (dataType == 0)
			{
				return this.pageDataInfo.nfcChara;
			}
			if (dataType != 1)
			{
				return null;
			}
			return this.pageDataInfo.nfcHousing;
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x0039D5F8 File Offset: 0x0039B9F8
		private List<NetworkInfo.BaseIndex> GetBaseIndexListFromSearch()
		{
			int dataType = this.dataType;
			if (dataType != 0)
			{
				if (dataType != 1)
				{
					return null;
				}
				if (this.lstSearchHousingInfo.Count == 0)
				{
					return null;
				}
				return this.lstSearchHousingInfo.OfType<NetworkInfo.BaseIndex>().ToList<NetworkInfo.BaseIndex>();
			}
			else
			{
				if (this.lstSearchCharaInfo.Count == 0)
				{
					return null;
				}
				return this.lstSearchCharaInfo.OfType<NetworkInfo.BaseIndex>().ToList<NetworkInfo.BaseIndex>();
			}
		}

		// Token: 0x060089CE RID: 35278 RVA: 0x0039D668 File Offset: 0x0039BA68
		private void UpdatePageMax()
		{
			int pageDrawCount = this.GetPageDrawCount();
			int pageDataCount = this.GetPageDataCount();
			int num = pageDataCount / pageDrawCount;
			num += ((pageDataCount % pageDrawCount != 0) ? 1 : 0);
			this.pageMax = Mathf.Max(num, 1);
			if (this.pageNow >= this.pageMax)
			{
				this.pageNow = 0;
			}
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x0039D6C0 File Offset: 0x0039BAC0
		private int CheckSelectInPage(int _sel)
		{
			int pageDrawCount = this.GetPageDrawCount();
			int pageDataCount = this.GetPageDataCount();
			int num = this.pageNow * pageDrawCount;
			for (int i = 0; i < pageDrawCount; i++)
			{
				int num2 = num + i;
				if (pageDataCount <= num2)
				{
					break;
				}
				if (num2 == _sel)
				{
					return num2 - num;
				}
			}
			return -1;
		}

		// Token: 0x060089D0 RID: 35280 RVA: 0x0039D718 File Offset: 0x0039BB18
		private void UpdatePage()
		{
			NetFileComponent[] cmpNetFile = this.GetPageFileComponents();
			if (cmpNetFile == null)
			{
				return;
			}
			int num = cmpNetFile.Length;
			this.pageCtrlItem.drawIndex = new int[num];
			this.pageCtrlItem.updateIndex = new int[num];
			for (int i = 0; i < num; i++)
			{
				cmpNetFile[i].SetState(false, false);
				this.pageCtrlItem.drawIndex[i] = -1;
				this.pageCtrlItem.updateIndex[i] = 0;
			}
			int pageDataCount = this.GetPageDataCount();
			if (pageDataCount == 0)
			{
				return;
			}
			List<NetworkInfo.BaseIndex> lstBaseIdx = this.GetBaseIndexListFromSearch();
			int num2 = this.pageNow * num;
			for (int j = 0; j < num; j++)
			{
				int index = num2 + j;
				if (pageDataCount <= index)
				{
					break;
				}
				this.pageCtrlItem.drawIndex[j] = lstBaseIdx[index].idx;
				this.pageCtrlItem.updateIndex[j] = lstBaseIdx[index].update_idx;
				cmpNetFile[j].SetState(true, true);
				cmpNetFile[j].UpdateSortType(this.searchSortSort);
				switch (this.searchSortSort)
				{
				case 0:
					cmpNetFile[j].SetUpdateTime(lstBaseIdx[index].createTime, 0);
					break;
				case 1:
					cmpNetFile[j].SetUpdateTime(lstBaseIdx[index].createTime, 0);
					break;
				case 2:
					cmpNetFile[j].SetUpdateTime(lstBaseIdx[index].updateTime, 1);
					break;
				case 3:
					cmpNetFile[j].SetUpdateTime(lstBaseIdx[index].updateTime, 1);
					break;
				case 4:
					cmpNetFile[j].SetRanking(lstBaseIdx[index].rankingT + 1);
					break;
				case 5:
					cmpNetFile[j].SetRanking(lstBaseIdx[index].rankingW + 1);
					break;
				case 6:
					cmpNetFile[j].SetApplauseNum(lstBaseIdx[index].applause);
					break;
				}
				bool flag = Singleton<GameSystem>.Instance.IsApplause((DataType)this.dataType, lstBaseIdx[index].data_uid);
				if (flag)
				{
					cmpNetFile[j].actApplause = null;
				}
				else
				{
					int no = j;
					cmpNetFile[j].actApplause = delegate()
					{
						EventSystem.current.SetSelectedGameObject(null);
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
						this.netInfo.BlockUI();
						Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.AddApplauseCount(res, (DataType)this.dataType, lstBaseIdx[index])).Subscribe(delegate(bool res)
						{
							Singleton<GameSystem>.Instance.AddApplause((DataType)this.dataType, lstBaseIdx[index].data_uid);
							lstBaseIdx[index].applause++;
							this.changeSearchSetting = true;
							cmpNetFile[no].SetApplauseNum(lstBaseIdx[index].applause);
						}, delegate(Exception err)
						{
							this.netInfo.UnblockUI();
						}, delegate()
						{
							this.netInfo.UnblockUI();
						});
					};
				}
			}
			if (!this.pageCtrlItem.updatingThumb)
			{
				this.pageCtrlItem.updatingThumb = true;
				this.objUpdatingThumbnailCanvas.SetActiveIfDifferent(true);
				Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.GetThumbnail(res, (DataType)this.dataType)).Subscribe(delegate(bool __)
				{
				}, delegate(Exception err)
				{
					this.pageCtrlItem.updatingThumb = false;
					this.objUpdatingThumbnailCanvas.SetActiveIfDifferent(false);
					this.netInfo.popupMsg.EndMessage();
				}, delegate()
				{
					this.pageCtrlItem.updatingThumb = false;
					this.objUpdatingThumbnailCanvas.SetActiveIfDifferent(false);
					this.netInfo.popupMsg.EndMessage();
				});
			}
			else
			{
				this.pageCtrlItem.updateThumb = true;
			}
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x0039DAE8 File Offset: 0x0039BEE8
		public void ChangeThumbnail(byte[][] data)
		{
			NetFileComponent[] pageFileComponents = this.GetPageFileComponents();
			if (pageFileComponents == null)
			{
				return;
			}
			int length = data.GetLength(0);
			for (int i = 0; i < pageFileComponents.Length; i++)
			{
				Texture image;
				if (i < length)
				{
					if (data[i] == null)
					{
						image = null;
					}
					else
					{
						image = PngAssist.ChangeTextureFromByte(data[i], 0, 0, TextureFormat.ARGB32, false);
					}
				}
				else
				{
					image = null;
				}
				pageFileComponents[i].SetImage(image);
			}
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x0039DB54 File Offset: 0x0039BF54
		public void UpdateSearchSettingUI()
		{
			DownUIControl.SearchSetting searchSetting = this.searchSettings[this.dataType];
			int num = this.searchSortSort;
			for (int i = 0; i < this.searchItem.tglSortType.Length; i++)
			{
				if (this.searchItem.tglSortType[i])
				{
					this.searchItem.tglSortType[i].isOn = false;
				}
			}
			this.searchItem.tglSortType[num].isOn = true;
			for (int j = 0; j < this.searchItem.tglSex.Length; j++)
			{
				if (this.searchItem.tglSex[j])
				{
					this.searchItem.tglSex[j].isOn = searchSetting.sex[j];
				}
			}
			for (int k = 0; k < this.searchItem.tglHeight.Length; k++)
			{
				if (this.searchItem.tglHeight[k])
				{
					this.searchItem.tglHeight[k].isOn = searchSetting.height[k];
				}
			}
			for (int l = 0; l < this.searchItem.tglBust.Length; l++)
			{
				if (this.searchItem.tglBust[l])
				{
					this.searchItem.tglBust[l].isOn = searchSetting.bust[l];
				}
			}
			for (int m = 0; m < this.searchItem.tglHair.Length; m++)
			{
				if (this.searchItem.tglHair[m])
				{
					this.searchItem.tglHair[m].isOn = searchSetting.hair[m];
				}
			}
			for (int n = 0; n < this.searchItem.tglVoice.Length; n++)
			{
				if (this.searchItem.tglVoice[n])
				{
					this.searchItem.tglVoice[n].isOn = searchSetting.voice[n];
				}
			}
			for (int num2 = 0; num2 < this.searchItem.tglMapSize.Length; num2++)
			{
				if (this.searchItem.tglMapSize[num2])
				{
					this.searchItem.tglMapSize[num2].isOn = searchSetting.mapSize[num2];
				}
			}
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x0039DDD0 File Offset: 0x0039C1D0
		public Tuple<int, int>[] GetThumbnailIndex(DataType type)
		{
			int num = this.pageCtrlItem.drawIndex.Count((int x) => -1 != x);
			Tuple<int, int>[] array = new Tuple<int, int>[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Tuple<int, int>(this.pageCtrlItem.drawIndex[i], this.pageCtrlItem.updateIndex[i]);
			}
			return array;
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x0039DE4C File Offset: 0x0039C24C
		public NetworkInfo.BaseIndex GetSelectServerInfo(DataType type)
		{
			int[] array = new int[]
			{
				this.downloadSel.selChara,
				this.downloadSel.selHousing
			};
			int num = array[(int)type];
			if (num == -1)
			{
				return null;
			}
			List<NetworkInfo.BaseIndex> baseIndexListFromSearch = this.GetBaseIndexListFromSearch();
			if (baseIndexListFromSearch == null)
			{
				return null;
			}
			return baseIndexListFromSearch[num];
		}

		// Token: 0x060089D5 RID: 35285 RVA: 0x0039DEA0 File Offset: 0x0039C2A0
		public string GetHandleNameFromUserIndex(int index)
		{
			NetworkInfo.UserInfo userInfo;
			if (!this.netInfo.dictUserInfo.TryGetValue(index, out userInfo))
			{
				return "不明";
			}
			return userInfo.handleName;
		}

		// Token: 0x060089D6 RID: 35286 RVA: 0x0039DED4 File Offset: 0x0039C2D4
		public void UpdateDownloadList()
		{
			int dataType = this.dataType;
			if (dataType != 0)
			{
				if (dataType == 1)
				{
					this.UpdateHousingSearchList();
				}
			}
			else
			{
				this.UpdateCharaSearchList();
			}
		}

		// Token: 0x060089D7 RID: 35287 RVA: 0x0039DF10 File Offset: 0x0039C310
		private void SaveDownloadFile(byte[] bytes, NetworkInfo.BaseIndex info)
		{
			DataType dataType = (DataType)this.dataType;
			if (dataType != DataType.Chara)
			{
				if (dataType == DataType.Housing)
				{
					NetworkInfo.HousingInfo housingInfo = info as NetworkInfo.HousingInfo;
					int mapSize = housingInfo.mapSize;
					DateTime now = DateTime.Now;
					string str = string.Format("{0}_{1:00}{2:00}_{3:00}{4:00}_{5:00}_{6:000}.png", new object[]
					{
						now.Year,
						now.Month,
						now.Day,
						now.Hour,
						now.Minute,
						now.Second,
						now.Millisecond
					});
					string path = UserData.Create(string.Format("{0}{1:00}", "housing/", mapSize + 1)) + str;
					using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
						{
							binaryWriter.Write(bytes);
						}
					}
				}
			}
			else
			{
				NetworkInfo.CharaInfo charaInfo = info as NetworkInfo.CharaInfo;
				string[] array = new string[]
				{
					UserData.Path + "chara/male/",
					UserData.Path + "chara/female/"
				};
				string[] array2 = new string[]
				{
					"AISChaM_",
					"AISChaF_"
				};
				string str2 = array2[charaInfo.sex] + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
				string path2 = array[charaInfo.sex] + str2;
				using (FileStream fileStream2 = new FileStream(path2, FileMode.Create, FileAccess.Write))
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream2))
					{
						binaryWriter2.Write(bytes);
					}
				}
			}
		}

		// Token: 0x060089D8 RID: 35288 RVA: 0x0039E13C File Offset: 0x0039C53C
		private void Awake()
		{
			HashSet<GameObject> hashSet = new HashSet<GameObject>(this.objTypeChara);
			hashSet.UnionWith(this.objTypeHousing);
			this.objTypeAll = hashSet.ToArray<GameObject>();
			HashSet<GameObject> hashSet2 = new HashSet<GameObject>(this.objModeDownload);
			hashSet2.UnionWith(this.objModeMyData);
			this.objModeAll = hashSet2.ToArray<GameObject>();
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x0039E194 File Offset: 0x0039C594
		private IEnumerator Start()
		{
			yield return new WaitWhile(() => !Singleton<Housing>.IsInstance() || !Singleton<Housing>.Instance.IsLoadList);
			this.dictMapSizeInfo = new Dictionary<int, Tuple<int, string>>();
			foreach (var <>__AnonType in Singleton<Housing>.Instance.dicAreaSizeInfo.Select((KeyValuePair<int, Housing.AreaSizeInfo> val, int idx) => new
			{
				val,
				idx
			}))
			{
				int key = <>__AnonType.val.Key;
				string item2 = string.Format("{0} X {1} X {2}", <>__AnonType.val.Value.limitSize.x, <>__AnonType.val.Value.limitSize.y, <>__AnonType.val.Value.limitSize.z);
				this.dictMapSizeInfo[<>__AnonType.idx] = new Tuple<int, string>(key, item2);
			}
			this.personalities = (from x in Singleton<Voice>.Instance.voiceInfoDic.Values
			where 0 <= x.No
			select x).ToArray<VoiceInfo.Param>();
			for (int i = 0; i < this.searchSettings.Length; i++)
			{
				this.searchSettings[i] = new DownUIControl.SearchSetting
				{
					voice = new bool[this.personalities.Length],
					mapSize = new bool[this.dictMapSizeInfo.Count]
				};
			}
			this.searchItem.tglVoice = new Toggle[this.personalities.Length];
			if (this.searchItem.objTempVoice)
			{
				for (int j = 0; j < this.personalities.Length; j++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.searchItem.objTempVoice, this.searchItem.objTempVoice.transform.parent, false);
					Toggle component = gameObject.GetComponent<Toggle>();
					if (component)
					{
						this.searchItem.tglVoice[j] = component;
					}
					Text componentInChildren = gameObject.GetComponentInChildren<Text>();
					if (componentInChildren)
					{
						componentInChildren.text = this.personalities[j].Personality;
					}
					gameObject.SetActiveIfDifferent(true);
					this.dictVoiceInfo[j] = this.personalities[j].No;
				}
			}
			int[] arrMapSize = this.dictMapSizeInfo.Keys.ToArray<int>();
			this.searchItem.tglMapSize = new Toggle[arrMapSize.Length];
			if (this.searchItem.objTempMapSize)
			{
				for (int k = 0; k < arrMapSize.Length; k++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.searchItem.objTempMapSize, this.searchItem.objTempMapSize.transform.parent, false);
					Toggle component2 = gameObject2.GetComponent<Toggle>();
					if (component2)
					{
						this.searchItem.tglMapSize[k] = component2;
					}
					Text componentInChildren2 = gameObject2.GetComponentInChildren<Text>();
					if (componentInChildren2)
					{
						componentInChildren2.text = this.dictMapSizeInfo[k].Item2;
					}
					gameObject2.SetActiveIfDifferent(true);
				}
			}
			if (this.serverItem.btnDownload)
			{
				Text text = this.serverItem.btnDownload.GetComponentInChildren<Text>(true);
				this.serverItem.btnDownload.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					this.netInfo.popupMsg.StartMessage(0.2f, 1f, 0.2f, "データをダウンロードしています…", 2);
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					NetworkInfo.BaseIndex dlinfo = this.GetSelectServerInfo((DataType)this.dataType);
					if (dlinfo != null)
					{
						this.netInfo.BlockUI();
						Observable.FromCoroutine<byte[]>((IObserver<byte[]> res) => this.phpCtrl.DownloadPNG(res, (DataType)this.dataType)).Subscribe(delegate(byte[] res)
						{
							this.SaveDownloadFile(res, dlinfo);
							Singleton<GameSystem>.Instance.AddDownload((DataType)this.dataType, dlinfo.data_uid);
							this.netInfo.DrawMessage(NetworkDefine.colorWhite, "データをダウンロードしました。");
							this.netInfo.popupMsg.StartMessage(0.2f, 1f, 0.2f, "データのダウンロードが完了しました。", 0);
						}, delegate(Exception err)
						{
							this.netInfo.popupMsg.StartMessage(0.2f, 1f, 0.2f, "データのダウンロードに失敗しました。", 0);
							this.netInfo.UnblockUI();
						}, delegate()
						{
							this.netInfo.UnblockUI();
						});
					}
				});
				this.serverItem.btnDownload.UpdateAsObservable().Subscribe(delegate(Unit _)
				{
					bool flag = null != this.selBaseIndex;
					if (this.serverItem.btnDownload.interactable != flag)
					{
						this.serverItem.btnDownload.interactable = flag;
						text.color = new Color(text.color.r, text.color.g, text.color.b, (!flag) ? 0.5f : 1f);
					}
				});
			}
			if (this.serverItem.btnDeleteCache)
			{
				this.serverItem.btnDeleteCache.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.DeleteCache(res, (DataType)this.dataType)).Subscribe(delegate(bool res)
					{
						this.UpdatePage();
						this.netInfo.DrawMessage(NetworkDefine.colorWhite, "キャッシュを削除しました。");
					}, delegate(Exception err)
					{
					}, delegate()
					{
					});
				});
			}
			if (this.serverItem.btnDelete)
			{
				Text text = this.serverItem.btnDelete.GetComponentInChildren<Text>(true);
				this.serverItem.btnDelete.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					NetworkInfo.BaseIndex selectServerInfo = this.GetSelectServerInfo((DataType)this.dataType);
					if (selectServerInfo != null)
					{
						Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.DeleteMyData(res, (DataType)this.dataType)).Subscribe(delegate(bool res)
						{
							this.netInfo.DrawMessage(NetworkDefine.colorWhite, "データを削除しました。");
						}, delegate(Exception err)
						{
						}, delegate()
						{
						});
					}
				});
				this.serverItem.btnDelete.UpdateAsObservable().Subscribe(delegate(Unit _)
				{
					bool flag = null != this.selBaseIndex;
					if (this.serverItem.btnDelete.interactable != flag)
					{
						this.serverItem.btnDelete.interactable = flag;
						text.color = new Color(text.color.r, text.color.g, text.color.b, (!flag) ? 0.5f : 1f);
					}
				});
			}
			if (this.searchItem.btnResetSearchSetting)
			{
				this.searchItem.btnResetSearchSetting.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.searchSettings[this.dataType].Reset(true);
					this.searchSortSort = 0;
					this.searchSortHNIdx = -1;
					this.searchItem.inpKeyword.text = string.Empty;
					this.searchItem.textKeywordDummy.text = string.Empty;
					this.changeSearchSetting = true;
					this.UpdateSearchSettingUI();
				});
			}
			if (this.searchItem.btnHNOpen)
			{
				this.searchItem.btnHNOpen.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.netInfo.netSelectHN.ShowSelectHNWindow(true);
				});
			}
			if (this.searchItem.btnHNOpenEx)
			{
				this.searchItem.btnHNOpenEx.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.netInfo.netSelectHN.ShowSelectHNWindow(true);
				});
			}
			if (this.searchItem.btnHNReset)
			{
				this.searchItem.btnHNReset.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.searchSortHNIdx = -1;
					this.changeSearchSetting = true;
				});
			}
			this._searchSortHNIdx.Subscribe(delegate(int x)
			{
				this.searchItem.textHN.text = ((x != -1) ? this.GetHandleNameFromUserIndex(x) : "No Select");
			});
			if (this.searchItem.inpKeyword)
			{
				this.searchItem.inpKeyword.OnEndEditAsObservable().Subscribe(delegate(string text)
				{
					this.searchItem.textKeywordDummy.text = text;
					this.changeSearchSetting = true;
				});
				this.searchItem.inpKeyword.UpdateAsObservable().Subscribe(delegate(Unit _)
				{
					bool isFocused = this.searchItem.inpKeyword.isFocused;
					this.searchItem.objKeywordDummy.SetActiveIfDifferent(!isFocused);
				}).AddTo(this);
			}
			if (this.searchItem.btnKeywordReset)
			{
				this.searchItem.btnKeywordReset.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.searchItem.textKeywordDummy.text = string.Empty;
					this.searchItem.inpKeyword.text = string.Empty;
					this.changeSearchSetting = true;
				});
			}
			if (this.searchItem.tglSortType.Any<Toggle>())
			{
				(from item in this.searchItem.tglSortType.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.tgl.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						if (this.searchSortSort != item.idx)
						{
							this.searchSortSort = item.idx;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.selInfoCha.btnHN)
			{
				this.selInfoCha.btnHN.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.searchSortHNIdx = this.selInfoCha.hnUserIndex;
					this.changeSearchSetting = true;
				});
			}
			if (this.selInfoCha.btnHNReset)
			{
				this.selInfoCha.btnHNReset.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.searchSortHNIdx = -1;
					this.changeSearchSetting = true;
				});
			}
			if (this.selInfoHousing.btnHN)
			{
				this.selInfoHousing.btnHN.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.searchSortHNIdx = this.selInfoHousing.hnUserIndex;
					this.changeSearchSetting = true;
				});
			}
			if (this.selInfoHousing.btnHNReset)
			{
				this.selInfoHousing.btnHNReset.OnClickAsObservable().Subscribe(delegate(Unit click)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					this.searchSortHNIdx = -1;
					this.changeSearchSetting = true;
				});
			}
			if (this.searchItem.tglSex.Any<Toggle>())
			{
				(from item in this.searchItem.tglSex.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					item.tgl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (this.searchSettings[this.dataType].sex[item.idx] != isOn)
						{
							this.searchSettings[this.dataType].sex[item.idx] = isOn;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.searchItem.tglHeight.Any<Toggle>())
			{
				(from item in this.searchItem.tglHeight.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					item.tgl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (this.searchSettings[this.dataType].height[item.idx] != isOn)
						{
							this.searchSettings[this.dataType].height[item.idx] = isOn;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.searchItem.tglBust.Any<Toggle>())
			{
				(from item in this.searchItem.tglBust.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					item.tgl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (this.searchSettings[this.dataType].bust[item.idx] != isOn)
						{
							this.searchSettings[this.dataType].bust[item.idx] = isOn;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.searchItem.tglHair.Any<Toggle>())
			{
				(from item in this.searchItem.tglHair.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					item.tgl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (this.searchSettings[this.dataType].hair[item.idx] != isOn)
						{
							this.searchSettings[this.dataType].hair[item.idx] = isOn;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.searchItem.tglVoice.Any<Toggle>())
			{
				(from item in this.searchItem.tglVoice.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					item.tgl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (this.searchSettings[this.dataType].voice[item.idx] != isOn)
						{
							this.searchSettings[this.dataType].voice[item.idx] = isOn;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.searchItem.tglMapSize.Any<Toggle>())
			{
				(from item in this.searchItem.tglMapSize.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					item.tgl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (this.searchSettings[this.dataType].mapSize[item.idx] != isOn)
						{
							this.searchSettings[this.dataType].mapSize[item.idx] = isOn;
							this.changeSearchSetting = true;
						}
					});
				});
			}
			if (this.tglMainMode.Any<Toggle>())
			{
				(from item in this.tglMainMode.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.tgl.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						if (this.mainMode != item.idx)
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
						}
						this.mainMode = item.idx;
					});
				});
			}
			if (this.tglDataType.Any<Toggle>())
			{
				(from item in this.tglDataType.Select((Toggle tgl, int idx) => new
				{
					tgl,
					idx
				})
				where item.tgl != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.tgl.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						if (this.dataType != item.idx)
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
						}
						this.dataType = item.idx;
					});
				});
			}
			this._mainMode.Subscribe(delegate(int no)
			{
				if (no != 0)
				{
					if (no == 1)
					{
						foreach (GameObject self in this.objModeAll)
						{
							self.SetActiveIfDifferent(false);
						}
						foreach (GameObject self2 in this.objModeMyData)
						{
							self2.SetActiveIfDifferent(true);
						}
					}
				}
				else
				{
					foreach (GameObject self3 in this.objModeAll)
					{
						self3.SetActiveIfDifferent(false);
					}
					foreach (GameObject self4 in this.objModeDownload)
					{
						self4.SetActiveIfDifferent(true);
					}
				}
				this.searchSortHNIdx = -1;
				if (this.netInfo.changeCharaList || this.netInfo.changeHosingList)
				{
					Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.UpdateBaseInfo(res)).Subscribe(delegate(bool res)
					{
						this.updateAllInfo = true;
						this.UpdateSearchSettingUI();
						this.changeSearchSetting = true;
					}, delegate(Exception err)
					{
					}, delegate()
					{
					});
				}
				else
				{
					this.updateAllInfo = true;
					this.UpdateSearchSettingUI();
					this.changeSearchSetting = true;
				}
			});
			this._dataType.Subscribe(delegate(int no)
			{
				if (no != 0)
				{
					if (no == 1)
					{
						foreach (GameObject self in this.objTypeAll)
						{
							self.SetActiveIfDifferent(false);
						}
						foreach (GameObject self2 in this.objTypeHousing)
						{
							self2.SetActiveIfDifferent(true);
						}
					}
				}
				else
				{
					foreach (GameObject self3 in this.objTypeAll)
					{
						self3.SetActiveIfDifferent(false);
					}
					foreach (GameObject self4 in this.objTypeChara)
					{
						self4.SetActiveIfDifferent(true);
					}
				}
				this.UpdateSearchSettingUI();
				this.changeSearchSetting = true;
			});
			(from f in this._updateCharaInfo
			where f
			select f).Subscribe(delegate(bool f)
			{
				this.UpdateInfoChara();
				this.updateCharaInfo = false;
			});
			(from f in this._updateHousingInfo
			where f
			select f).Subscribe(delegate(bool f)
			{
				this.UpdateInfoHousing();
				this.updateHousingInfo = false;
			});
			(from f in this._updateAllInfo
			where f
			select f).Subscribe(delegate(bool f)
			{
				this.updateCharaInfo = true;
				this.updateHousingInfo = true;
				this.updateAllInfo = false;
			});
			this.pageDataInfo.nfcChara.Select((NetFileComponent val, int idx) => new
			{
				tgl = val.tglItem,
				idx = idx
			}).ToList().ForEach(delegate(item)
			{
				(from _ in item.tgl.OnValueChangedAsObservable()
				where !this.skipToggleChange
				select _).Subscribe(delegate(bool isOn)
				{
					if (isOn)
					{
						this.skipToggleChange = true;
						foreach (NetFileComponent netFileComponent in from x in this.pageDataInfo.nfcChara
						where x.tglItem != item.tgl
						select x)
						{
							netFileComponent.tglItem.isOn = false;
						}
						this.skipToggleChange = false;
					}
					int pageDrawCount = this.GetPageDrawCount();
					int selChara = isOn ? (this.pageNow * pageDrawCount + item.idx) : -1;
					this.downloadSel.selChara = selChara;
					this.UpdateInfoChara();
				});
			});
			this.pageDataInfo.nfcHousing.Select((NetFileComponent val, int idx) => new
			{
				tgl = val.tglItem,
				idx = idx
			}).ToList().ForEach(delegate(item)
			{
				(from _ in item.tgl.OnValueChangedAsObservable()
				where !this.skipToggleChange
				select _).Subscribe(delegate(bool isOn)
				{
					if (isOn)
					{
						this.skipToggleChange = true;
						foreach (NetFileComponent netFileComponent in from x in this.pageDataInfo.nfcHousing
						where x.tglItem != item.tgl
						select x)
						{
							netFileComponent.tglItem.isOn = false;
						}
						this.skipToggleChange = false;
					}
					int pageDrawCount = this.GetPageDrawCount();
					int selHousing = isOn ? (this.pageNow * pageDrawCount + item.idx) : -1;
					this.downloadSel.selHousing = selHousing;
					this.UpdateInfoHousing();
				});
			});
			this._pageMax.Subscribe(delegate(int no)
			{
				bool interactable = 0 != no;
				this.pageCtrlItem.textPageMax.text = no.ToString();
				this.pageCtrlItem.InpPage.interactable = interactable;
				foreach (Button button in from x in this.pageCtrlItem.btnCtrlPage
				where null != x
				select x)
				{
					button.interactable = interactable;
				}
			});
			this._pageNow.Subscribe(delegate(int no)
			{
				int num = this.pageNow + 1;
				this.pageCtrlItem.InpPage.text = num.ToString();
				DataType dataType = (DataType)this.dataType;
				if (dataType != DataType.Chara)
				{
					if (dataType == DataType.Housing)
					{
						this.downloadSel.selHousing = -1;
						this.UpdateInfoHousing();
						foreach (NetFileComponent netFileComponent in this.pageDataInfo.nfcHousing)
						{
							netFileComponent.tglItem.isOn = false;
						}
					}
				}
				else
				{
					this.downloadSel.selChara = -1;
					this.UpdateInfoChara();
					foreach (NetFileComponent netFileComponent2 in this.pageDataInfo.nfcChara)
					{
						netFileComponent2.tglItem.isOn = false;
					}
				}
				this.UpdatePage();
			});
			this.pageCtrlItem.InpPage.onEndEdit.AddListener(delegate(string s)
			{
				int num;
				int.TryParse(s, out num);
				num = Mathf.Clamp(num, 1, this.pageMax);
				if (num - 1 != this.pageNow)
				{
					this.pageNow = num - 1;
				}
				this.pageCtrlItem.InpPage.text = num.ToString();
			});
			this.pageCtrlItem.btnCtrlPage.Select((Button btn, int idx) => new
			{
				btn,
				idx
			}).ToList().ForEach(delegate(item)
			{
				item.btn.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					switch (item.idx)
					{
					case 0:
						this.pageNow = 0;
						break;
					case 1:
						this.pageNow = Mathf.Max(0, this.pageNow - 1);
						break;
					case 2:
						this.pageNow = Mathf.Min(this.pageMax - 1, this.pageNow + 1);
						break;
					case 3:
						this.pageNow = this.pageMax - 1;
						break;
					}
				});
			});
			if (this.btnReload)
			{
				this.btnReload.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					Observable.FromCoroutine(() => this.phpCtrl.GetBaseInfo(false), false).Subscribe(delegate(Unit __)
					{
					}, delegate(Exception err)
					{
					}, delegate()
					{
						this.changeSearchSetting = true;
					});
				});
			}
			if (this.btnTitle)
			{
				this.btnTitle.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					this.netInfo.BlockUI();
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					Singleton<Scene>.Instance.LoadReserve(new Scene.Data
					{
						levelName = "Title",
						isFade = true
					}, false);
				});
			}
			if (null != this.btnUploader)
			{
				this.btnUploader.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					this.netInfo.BlockUI();
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					Singleton<GameSystem>.Instance.networkSceneName = "Uploader";
					Singleton<Scene>.Instance.LoadReserve(new Scene.Data
					{
						levelName = "NetworkCheckScene",
						isFade = true
					}, false);
				});
			}
			this.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				this.selBaseIndex = this.GetSelectServerInfo((DataType)this.dataType);
				if (this.changeSearchSetting)
				{
					this.UpdateDownloadList();
					this.changeSearchSetting = false;
				}
				if (this.pageCtrlItem.updateThumb && !this.pageCtrlItem.updatingThumb)
				{
					this.pageCtrlItem.updateThumb = false;
					this.pageCtrlItem.updatingThumb = true;
					this.objUpdatingThumbnailCanvas.SetActiveIfDifferent(true);
					Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.GetThumbnail(res, (DataType)this.dataType)).Subscribe(delegate(bool __)
					{
					}, delegate(Exception err)
					{
						this.pageCtrlItem.updatingThumb = false;
						this.objUpdatingThumbnailCanvas.SetActiveIfDifferent(false);
						this.netInfo.popupMsg.EndMessage();
					}, delegate()
					{
						this.pageCtrlItem.updatingThumb = false;
						this.objUpdatingThumbnailCanvas.SetActiveIfDifferent(false);
						this.netInfo.popupMsg.EndMessage();
					});
				}
			});
			yield break;
		}

		// Token: 0x04006FB7 RID: 28599
		public DownloadScene downScene;

		// Token: 0x04006FB8 RID: 28600
		public DownPhpControl phpCtrl;

		// Token: 0x04006FB9 RID: 28601
		public DownUIControl.SearchSetting[] searchSettings = new DownUIControl.SearchSetting[Enum.GetNames(typeof(DataType)).Length];

		// Token: 0x04006FBA RID: 28602
		[HideInInspector]
		public int searchSortSort;

		// Token: 0x04006FBB RID: 28603
		private IntReactiveProperty _searchSortHNIdx = new IntReactiveProperty(-1);

		// Token: 0x04006FBC RID: 28604
		[Header("---< モード別表示OBJ >-----------------------")]
		private GameObject[] objModeAll;

		// Token: 0x04006FBD RID: 28605
		[SerializeField]
		private GameObject[] objModeDownload;

		// Token: 0x04006FBE RID: 28606
		[SerializeField]
		private GameObject[] objModeMyData;

		// Token: 0x04006FBF RID: 28607
		[Header("---< タイプ別表示OBJ >-----------------------")]
		private GameObject[] objTypeAll;

		// Token: 0x04006FC0 RID: 28608
		[SerializeField]
		private GameObject[] objTypeChara;

		// Token: 0x04006FC1 RID: 28609
		[SerializeField]
		private GameObject[] objTypeHousing;

		// Token: 0x04006FC2 RID: 28610
		[SerializeField]
		private GameObject[] objHideH;

		// Token: 0x04006FC3 RID: 28611
		[Header("---< モード・タイプ切り替え >----------------")]
		[SerializeField]
		private Toggle[] tglMainMode;

		// Token: 0x04006FC4 RID: 28612
		[SerializeField]
		private Toggle[] tglDataType;

		// Token: 0x04006FC5 RID: 28613
		[Header("---< 選択情報・キャラ >----------------------")]
		[SerializeField]
		private DownUIControl.SelectInfoChara selInfoCha;

		// Token: 0x04006FC6 RID: 28614
		[Header("---< 選択情報・ハウジング >----------------------")]
		[SerializeField]
		private DownUIControl.SelectInfoHousing selInfoHousing;

		// Token: 0x04006FC7 RID: 28615
		[Header("---< 検索関連 >------------------------------")]
		[SerializeField]
		private DownUIControl.SearchItem searchItem;

		// Token: 0x04006FC8 RID: 28616
		[Header("---< データ一覧情報 >------------------------")]
		[SerializeField]
		private DownUIControl.PageDataInfo pageDataInfo;

		// Token: 0x04006FC9 RID: 28617
		[Header("---< サーバーデータ >--------------------------")]
		[SerializeField]
		private DownUIControl.ServerItem serverItem;

		// Token: 0x04006FCA RID: 28618
		[Header("---< ページ制御 >----------------------------")]
		[SerializeField]
		private DownUIControl.PageControlItem pageCtrlItem;

		// Token: 0x04006FCB RID: 28619
		private IntReactiveProperty _pageMax = new IntReactiveProperty(1);

		// Token: 0x04006FCC RID: 28620
		private IntReactiveProperty _pageNow = new IntReactiveProperty(1);

		// Token: 0x04006FCD RID: 28621
		[Header("---< その他 >--------------------------------")]
		[SerializeField]
		private GameObject objUpdatingThumbnailCanvas;

		// Token: 0x04006FCE RID: 28622
		[SerializeField]
		private Button btnTitle;

		// Token: 0x04006FCF RID: 28623
		[SerializeField]
		private Button btnUploader;

		// Token: 0x04006FD0 RID: 28624
		[SerializeField]
		private Button btnReload;

		// Token: 0x04006FD1 RID: 28625
		[SerializeField]
		private Text textNewestVersion;

		// Token: 0x04006FD2 RID: 28626
		private VoiceInfo.Param[] personalities;

		// Token: 0x04006FD3 RID: 28627
		private IntReactiveProperty _mainMode = new IntReactiveProperty(0);

		// Token: 0x04006FD4 RID: 28628
		private IntReactiveProperty _dataType = new IntReactiveProperty(0);

		// Token: 0x04006FD5 RID: 28629
		[HideInInspector]
		public bool changeSearchSetting;

		// Token: 0x04006FD6 RID: 28630
		private BoolReactiveProperty _updateCharaInfo = new BoolReactiveProperty(false);

		// Token: 0x04006FD7 RID: 28631
		private BoolReactiveProperty _updateHousingInfo = new BoolReactiveProperty(false);

		// Token: 0x04006FD8 RID: 28632
		private BoolReactiveProperty _updateAllInfo = new BoolReactiveProperty(false);

		// Token: 0x04006FD9 RID: 28633
		public DownUIControl.PageSelectInfo downloadSel = new DownUIControl.PageSelectInfo();

		// Token: 0x04006FDA RID: 28634
		public List<NetworkInfo.CharaInfo> lstSearchCharaInfo = new List<NetworkInfo.CharaInfo>();

		// Token: 0x04006FDB RID: 28635
		public List<NetworkInfo.HousingInfo> lstSearchHousingInfo = new List<NetworkInfo.HousingInfo>();

		// Token: 0x04006FDC RID: 28636
		private Dictionary<int, int> dictVoiceInfo = new Dictionary<int, int>();

		// Token: 0x04006FDD RID: 28637
		private Dictionary<int, Tuple<int, string>> dictMapSizeInfo = new Dictionary<int, Tuple<int, string>>();

		// Token: 0x04006FDE RID: 28638
		private NetworkInfo.BaseIndex selBaseIndex;

		// Token: 0x04006FDF RID: 28639
		private bool skipToggleChange;

		// Token: 0x02000FFF RID: 4095
		public enum MainMode
		{
			// Token: 0x04006FFF RID: 28671
			MM_Download,
			// Token: 0x04007000 RID: 28672
			MM_MyData
		}

		// Token: 0x02001000 RID: 4096
		public enum SortType
		{
			// Token: 0x04007002 RID: 28674
			ST_NEW_C,
			// Token: 0x04007003 RID: 28675
			ST_OLD_C,
			// Token: 0x04007004 RID: 28676
			ST_NEW_U,
			// Token: 0x04007005 RID: 28677
			ST_OLD_U,
			// Token: 0x04007006 RID: 28678
			ST_DL_ALL,
			// Token: 0x04007007 RID: 28679
			ST_DL_WEEK,
			// Token: 0x04007008 RID: 28680
			ST_APPLAUSE
		}

		// Token: 0x02001001 RID: 4097
		public class SearchSetting
		{
			// Token: 0x060089F9 RID: 35321 RVA: 0x0039E2D4 File Offset: 0x0039C6D4
			public void Reset(bool excludeSort = true)
			{
				for (int i = 0; i < this.sex.Length; i++)
				{
					this.sex[i] = false;
				}
				for (int j = 0; j < this.height.Length; j++)
				{
					this.height[j] = false;
				}
				for (int k = 0; k < this.bust.Length; k++)
				{
					this.bust[k] = false;
				}
				for (int l = 0; l < this.hair.Length; l++)
				{
					this.hair[l] = false;
				}
				if (this.voice != null)
				{
					for (int m = 0; m < this.voice.Length; m++)
					{
						this.voice[m] = false;
					}
				}
				if (this.mapSize != null)
				{
					for (int n = 0; n < this.mapSize.Length; n++)
					{
						this.mapSize[n] = false;
					}
				}
			}

			// Token: 0x04007009 RID: 28681
			public bool[] sex = new bool[2];

			// Token: 0x0400700A RID: 28682
			public bool[] height = new bool[3];

			// Token: 0x0400700B RID: 28683
			public bool[] bust = new bool[3];

			// Token: 0x0400700C RID: 28684
			public bool[] hair = new bool[6];

			// Token: 0x0400700D RID: 28685
			public bool[] voice;

			// Token: 0x0400700E RID: 28686
			public bool[] mapSize;
		}

		// Token: 0x02001002 RID: 4098
		[Serializable]
		public class SelectInfoChara
		{
			// Token: 0x0400700F RID: 28687
			public Text textHN;

			// Token: 0x04007010 RID: 28688
			public Text textName;

			// Token: 0x04007011 RID: 28689
			public Text textType;

			// Token: 0x04007012 RID: 28690
			public Text textBirthDay;

			// Token: 0x04007013 RID: 28691
			public GameObject objOnPhase01;

			// Token: 0x04007014 RID: 28692
			public GameObject objOnPhase02;

			// Token: 0x04007015 RID: 28693
			public GameObject objOnPhase03;

			// Token: 0x04007016 RID: 28694
			public GameObject objOnPhase04;

			// Token: 0x04007017 RID: 28695
			public Text textLifeStyle;

			// Token: 0x04007018 RID: 28696
			public Text textPheromone;

			// Token: 0x04007019 RID: 28697
			public Text textReliability;

			// Token: 0x0400701A RID: 28698
			public Text textReason;

			// Token: 0x0400701B RID: 28699
			public Text textInstinct;

			// Token: 0x0400701C RID: 28700
			public Text textDirty;

			// Token: 0x0400701D RID: 28701
			public Text textWariness;

			// Token: 0x0400701E RID: 28702
			public Text textSociability;

			// Token: 0x0400701F RID: 28703
			public Text textDarkness;

			// Token: 0x04007020 RID: 28704
			public Text textSkill_n01;

			// Token: 0x04007021 RID: 28705
			public Text textSkill_n02;

			// Token: 0x04007022 RID: 28706
			public Text textSkill_n03;

			// Token: 0x04007023 RID: 28707
			public Text textSkill_n04;

			// Token: 0x04007024 RID: 28708
			public Text textSkill_n05;

			// Token: 0x04007025 RID: 28709
			public Text textSkill_h01;

			// Token: 0x04007026 RID: 28710
			public Text textSkill_h02;

			// Token: 0x04007027 RID: 28711
			public Text textSkill_h03;

			// Token: 0x04007028 RID: 28712
			public Text textSkill_h04;

			// Token: 0x04007029 RID: 28713
			public Text textSkill_h05;

			// Token: 0x0400702A RID: 28714
			public Text textWish_01;

			// Token: 0x0400702B RID: 28715
			public Text textWish_02;

			// Token: 0x0400702C RID: 28716
			public Text textWish_03;

			// Token: 0x0400702D RID: 28717
			public Text textComment;

			// Token: 0x0400702E RID: 28718
			public Button btnHN;

			// Token: 0x0400702F RID: 28719
			public Button btnHNReset;

			// Token: 0x04007030 RID: 28720
			[HideInInspector]
			public int hnUserIndex = -1;

			// Token: 0x04007031 RID: 28721
			public Image imgThumbnail;
		}

		// Token: 0x02001003 RID: 4099
		[Serializable]
		public class SelectInfoHousing
		{
			// Token: 0x04007032 RID: 28722
			public Text textHN;

			// Token: 0x04007033 RID: 28723
			public Text textName;

			// Token: 0x04007034 RID: 28724
			public Text textMapSize;

			// Token: 0x04007035 RID: 28725
			public Text textComment;

			// Token: 0x04007036 RID: 28726
			public Button btnHN;

			// Token: 0x04007037 RID: 28727
			public Button btnHNReset;

			// Token: 0x04007038 RID: 28728
			[HideInInspector]
			public int hnUserIndex = -1;

			// Token: 0x04007039 RID: 28729
			public Image imgThumbnail;
		}

		// Token: 0x02001004 RID: 4100
		[Serializable]
		public class SearchItem
		{
			// Token: 0x0400703A RID: 28730
			public Button btnResetSearchSetting;

			// Token: 0x0400703B RID: 28731
			public Toggle[] tglSortType;

			// Token: 0x0400703C RID: 28732
			public Button btnHNOpen;

			// Token: 0x0400703D RID: 28733
			public Button btnHNOpenEx;

			// Token: 0x0400703E RID: 28734
			public Button btnHNReset;

			// Token: 0x0400703F RID: 28735
			public Text textHN;

			// Token: 0x04007040 RID: 28736
			public Toggle[] tglSex;

			// Token: 0x04007041 RID: 28737
			public Toggle[] tglHeight;

			// Token: 0x04007042 RID: 28738
			public Toggle[] tglBust;

			// Token: 0x04007043 RID: 28739
			public Toggle[] tglHair;

			// Token: 0x04007044 RID: 28740
			[HideInInspector]
			public Toggle[] tglVoice;

			// Token: 0x04007045 RID: 28741
			public GameObject objTempVoice;

			// Token: 0x04007046 RID: 28742
			[HideInInspector]
			public Toggle[] tglMapSize;

			// Token: 0x04007047 RID: 28743
			public GameObject objTempMapSize;

			// Token: 0x04007048 RID: 28744
			public InputField inpKeyword;

			// Token: 0x04007049 RID: 28745
			public GameObject objKeywordDummy;

			// Token: 0x0400704A RID: 28746
			public Text textKeywordDummy;

			// Token: 0x0400704B RID: 28747
			public Button btnKeywordReset;
		}

		// Token: 0x02001005 RID: 4101
		[Serializable]
		public class PageDataInfo
		{
			// Token: 0x0400704C RID: 28748
			public NetFileComponent[] nfcChara;

			// Token: 0x0400704D RID: 28749
			public NetFileComponent[] nfcHousing;
		}

		// Token: 0x02001006 RID: 4102
		[Serializable]
		public class ServerItem
		{
			// Token: 0x0400704E RID: 28750
			public Button btnDownload;

			// Token: 0x0400704F RID: 28751
			public Button btnDeleteCache;

			// Token: 0x04007050 RID: 28752
			public Button btnDelete;
		}

		// Token: 0x02001007 RID: 4103
		[Serializable]
		public class PageControlItem
		{
			// Token: 0x04007051 RID: 28753
			public Button[] btnCtrlPage;

			// Token: 0x04007052 RID: 28754
			public Text textPageMax;

			// Token: 0x04007053 RID: 28755
			public InputField InpPage;

			// Token: 0x04007054 RID: 28756
			[HideInInspector]
			public bool updatingThumb;

			// Token: 0x04007055 RID: 28757
			[HideInInspector]
			public bool updateThumb;

			// Token: 0x04007056 RID: 28758
			[HideInInspector]
			public int[] drawIndex;

			// Token: 0x04007057 RID: 28759
			[HideInInspector]
			public int[] updateIndex;
		}

		// Token: 0x02001008 RID: 4104
		public class PageSelectInfo
		{
			// Token: 0x04007058 RID: 28760
			public int selChara = -1;

			// Token: 0x04007059 RID: 28761
			public int selHousing = -1;
		}
	}
}
