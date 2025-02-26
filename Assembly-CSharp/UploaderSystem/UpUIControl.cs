using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using AIProject;
using CharaCustom;
using Illusion.Extensions;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02000FF7 RID: 4087
	public class UpUIControl : MonoBehaviour
	{
		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06008984 RID: 35204 RVA: 0x0039760A File Offset: 0x00395A0A
		private NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06008985 RID: 35205 RVA: 0x00397611 File Offset: 0x00395A11
		private NetCacheControl cacheCtrl
		{
			get
			{
				return (!Singleton<NetworkInfo>.IsInstance()) ? null : this.netInfo.cacheCtrl;
			}
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06008986 RID: 35206 RVA: 0x0039762E File Offset: 0x00395A2E
		// (set) Token: 0x06008987 RID: 35207 RVA: 0x0039763B File Offset: 0x00395A3B
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

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06008988 RID: 35208 RVA: 0x00397649 File Offset: 0x00395A49
		// (set) Token: 0x06008989 RID: 35209 RVA: 0x00397656 File Offset: 0x00395A56
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

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x0600898A RID: 35210 RVA: 0x00397664 File Offset: 0x00395A64
		// (set) Token: 0x0600898B RID: 35211 RVA: 0x00397671 File Offset: 0x00395A71
		public bool updateFemale
		{
			get
			{
				return this._updateFemale.Value;
			}
			set
			{
				this._updateFemale.Value = value;
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x0600898C RID: 35212 RVA: 0x0039767F File Offset: 0x00395A7F
		// (set) Token: 0x0600898D RID: 35213 RVA: 0x0039768C File Offset: 0x00395A8C
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

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x0600898E RID: 35214 RVA: 0x0039769A File Offset: 0x00395A9A
		// (set) Token: 0x0600898F RID: 35215 RVA: 0x003976A7 File Offset: 0x00395AA7
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

		// Token: 0x06008990 RID: 35216 RVA: 0x003976B5 File Offset: 0x00395AB5
		public void ShowNewestVersion()
		{
			if (null != this.textNewestVersion)
			{
				this.textNewestVersion.gameObject.SetActiveIfDifferent(true);
			}
		}

		// Token: 0x06008991 RID: 35217 RVA: 0x003976DC File Offset: 0x00395ADC
		public void ChangeUploadData()
		{
			this.uploadItem.modeUpdate = false;
			int user_idx = this.netInfo.profile.userIdx;
			DataType dataType = (DataType)this.dataType;
			if (dataType != DataType.Chara)
			{
				if (dataType == DataType.Housing)
				{
					this.uploadItem.modeUpdate = false;
					this.updateHousingInfo = true;
				}
			}
			else
			{
				this.updateCharaInfo = true;
				CustomCharaScrollController.ScrollData info = null;
				if (this.updateFemale)
				{
					info = this.netInfo.selectCharaFWindow.GetSelectInfo();
				}
				else
				{
					info = this.netInfo.selectCharaMWindow.GetSelectInfo();
				}
				if (info != null)
				{
					NetworkInfo.CharaInfo[] array = (from x in this.netInfo.lstCharaInfo
					where x.data_uid == info.info.data_uuid && x.user_idx == user_idx
					select x).ToArray<NetworkInfo.CharaInfo>();
					if (array.Length != 0)
					{
						this.uploadItem.modeUpdate = true;
					}
					else if (this.netInfo.dictUploaded[0].ContainsKey(info.info.data_uuid))
					{
						this.uploadItem.modeUpdate = true;
					}
				}
			}
			if (this.uploadItem.btnUpload)
			{
				Text componentInChildren = this.uploadItem.btnUpload.GetComponentInChildren<Text>(true);
				if (componentInChildren)
				{
					componentInChildren.text = ((!this.uploadItem.modeUpdate) ? "アップロード" : "更新");
				}
			}
		}

		// Token: 0x06008992 RID: 35218 RVA: 0x00397878 File Offset: 0x00395C78
		private void UpdatePreview(DataType type, string path)
		{
			Image imgThumbnail;
			if (type != DataType.Chara)
			{
				if (type != DataType.Housing)
				{
					return;
				}
				imgThumbnail = this.selInfoHousing.imgThumbnail;
			}
			else
			{
				imgThumbnail = this.selInfoCha.imgThumbnail;
			}
			if (null == imgThumbnail)
			{
				return;
			}
			if (null != imgThumbnail.sprite)
			{
				if (null != imgThumbnail.sprite.texture)
				{
					UnityEngine.Object.Destroy(imgThumbnail.sprite.texture);
				}
				UnityEngine.Object.Destroy(imgThumbnail.sprite);
				imgThumbnail.sprite = null;
			}
			if (!path.IsNullOrEmpty())
			{
				Sprite sprite = PngAssist.LoadSpriteFromFile(path);
				if (null != sprite)
				{
					imgThumbnail.sprite = sprite;
				}
				imgThumbnail.enabled = true;
			}
			else
			{
				imgThumbnail.enabled = false;
			}
		}

		// Token: 0x06008993 RID: 35219 RVA: 0x0039794C File Offset: 0x00395D4C
		public void UpdateInfoChara()
		{
			string path = string.Empty;
			CustomCharaScrollController.ScrollData selectInfo;
			if (this.updateFemale)
			{
				selectInfo = this.netInfo.selectCharaFWindow.GetSelectInfo();
			}
			else
			{
				selectInfo = this.netInfo.selectCharaMWindow.GetSelectInfo();
			}
			if (selectInfo != null)
			{
				CustomCharaFileInfo info = selectInfo.info;
				if (null != this.selInfoCha.textName)
				{
					this.selInfoCha.textName.text = info.name;
				}
				if (null != this.selInfoCha.textType)
				{
					this.selInfoCha.textType.text = ((info.sex != 0) ? Singleton<Character>.Instance.GetCharaTypeName(info.type) : string.Empty);
				}
				if (null != this.selInfoCha.textBirthDay)
				{
					this.selInfoCha.textBirthDay.text = info.strBirthDay;
				}
				if (null != this.selInfoCha.objOnPhase01)
				{
					this.selInfoCha.objOnPhase01.SetActiveIfDifferent(this.updateFemale);
				}
				if (null != this.selInfoCha.objOnPhase02)
				{
					this.selInfoCha.objOnPhase02.SetActiveIfDifferent(info.phase >= 1 && this.updateFemale);
				}
				if (null != this.selInfoCha.objOnPhase03)
				{
					this.selInfoCha.objOnPhase03.SetActiveIfDifferent(info.phase >= 2 && this.updateFemale);
				}
				if (null != this.selInfoCha.objOnPhase04)
				{
					this.selInfoCha.objOnPhase04.SetActiveIfDifferent(info.phase == 3 && this.updateFemale);
				}
				if (null != this.selInfoCha.textLifeStyle)
				{
					this.selInfoCha.textLifeStyle.text = ((info.lifestyle != -1 && this.updateFemale) ? this.netInfo.GetLifeStyleName(info.lifestyle) : "---------------");
				}
				if (null != this.selInfoCha.textPheromone)
				{
					this.selInfoCha.textPheromone.text = ((info.gameRegistration && this.updateFemale) ? info.pheromone.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textReliability)
				{
					this.selInfoCha.textReliability.text = ((info.gameRegistration && this.updateFemale) ? info.reliability.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textReason)
				{
					this.selInfoCha.textReason.text = ((info.gameRegistration && this.updateFemale) ? info.reason.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textInstinct)
				{
					this.selInfoCha.textInstinct.text = ((info.gameRegistration && this.updateFemale) ? info.instinct.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textDirty)
				{
					this.selInfoCha.textDirty.text = ((info.gameRegistration && this.updateFemale) ? info.dirty.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textWariness)
				{
					this.selInfoCha.textWariness.text = ((info.gameRegistration && this.updateFemale) ? info.wariness.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textSociability)
				{
					this.selInfoCha.textSociability.text = ((info.gameRegistration && this.updateFemale) ? info.sociability.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textDarkness)
				{
					this.selInfoCha.textDarkness.text = ((info.gameRegistration && this.updateFemale) ? info.darkness.ToString() : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n01)
				{
					this.selInfoCha.textSkill_n01.text = ((info.skill_n01 != -1 && this.updateFemale) ? this.netInfo.GetNormalSkillName(info.skill_n01) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n02)
				{
					this.selInfoCha.textSkill_n02.text = ((info.skill_n02 != -1 && this.updateFemale) ? this.netInfo.GetNormalSkillName(info.skill_n02) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n03)
				{
					this.selInfoCha.textSkill_n03.text = ((info.skill_n03 != -1 && this.updateFemale) ? this.netInfo.GetNormalSkillName(info.skill_n03) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n04)
				{
					this.selInfoCha.textSkill_n04.text = ((info.skill_n04 != -1 && this.updateFemale) ? this.netInfo.GetNormalSkillName(info.skill_n04) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_n05)
				{
					this.selInfoCha.textSkill_n05.text = ((info.skill_n05 != -1 && this.updateFemale) ? this.netInfo.GetNormalSkillName(info.skill_n05) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h01)
				{
					this.selInfoCha.textSkill_h01.text = ((info.skill_h01 != -1 && this.updateFemale) ? this.netInfo.GetHSkillName(info.skill_h01) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h02)
				{
					this.selInfoCha.textSkill_h02.text = ((info.skill_h02 != -1 && this.updateFemale) ? this.netInfo.GetHSkillName(info.skill_h02) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h03)
				{
					this.selInfoCha.textSkill_h03.text = ((info.skill_h03 != -1 && this.updateFemale) ? this.netInfo.GetHSkillName(info.skill_h03) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h04)
				{
					this.selInfoCha.textSkill_h04.text = ((info.skill_h04 != -1 && this.updateFemale) ? this.netInfo.GetHSkillName(info.skill_h04) : "---------------");
				}
				if (null != this.selInfoCha.textSkill_h05)
				{
					this.selInfoCha.textSkill_h05.text = ((info.skill_h05 != -1 && this.updateFemale) ? this.netInfo.GetHSkillName(info.skill_h05) : "---------------");
				}
				Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.init_wish_param);
				if (null != this.selInfoCha.textWish_01)
				{
					string text = "---------------";
					ListInfoBase listInfoBase;
					if (info.wish_01 != -1 && this.updateFemale && categoryInfo.TryGetValue(info.wish_01, out listInfoBase))
					{
						text = listInfoBase.Name;
					}
					this.selInfoCha.textWish_01.text = text;
				}
				if (null != this.selInfoCha.textWish_02)
				{
					string text2 = "---------------";
					ListInfoBase listInfoBase2;
					if (info.wish_02 != -1 && this.updateFemale && categoryInfo.TryGetValue(info.wish_02, out listInfoBase2))
					{
						text2 = listInfoBase2.Name;
					}
					this.selInfoCha.textWish_02.text = text2;
				}
				if (null != this.selInfoCha.textWish_03)
				{
					string text3 = "---------------";
					ListInfoBase listInfoBase3;
					if (info.wish_03 != -1 && this.updateFemale && categoryInfo.TryGetValue(info.wish_03, out listInfoBase3))
					{
						text3 = listInfoBase3.Name;
					}
					this.selInfoCha.textWish_03.text = text3;
				}
				path = info.FullPath;
			}
			else
			{
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
			}
			this.UpdatePreview(DataType.Chara, path);
		}

		// Token: 0x06008994 RID: 35220 RVA: 0x003987C4 File Offset: 0x00396BC4
		public void UpdateInfoHousing()
		{
			string selectPath = this.netInfo.selectHousingWindow.GetSelectPath();
			this.UpdatePreview(DataType.Housing, selectPath);
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x003987EC File Offset: 0x00396BEC
		public string GetUploadFile(DataType type)
		{
			if (type != DataType.Chara)
			{
				if (type != DataType.Housing)
				{
					return string.Empty;
				}
				return this.netInfo.selectHousingWindow.GetSelectPath();
			}
			else
			{
				CustomCharaScrollController.ScrollData selectInfo;
				if (this.updateFemale)
				{
					selectInfo = this.netInfo.selectCharaFWindow.GetSelectInfo();
				}
				else
				{
					selectInfo = this.netInfo.selectCharaMWindow.GetSelectInfo();
				}
				if (selectInfo == null)
				{
					return string.Empty;
				}
				return selectInfo.info.FullPath;
			}
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x00398872 File Offset: 0x00396C72
		public string GetComment(DataType type)
		{
			return this.uploadItem.bkComment[(int)type];
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x00398884 File Offset: 0x00396C84
		public string GetTitle()
		{
			string result = "NO_TITLE";
			if (null != this.uploadItem.inpTitle)
			{
				string text = this.uploadItem.inpTitle.text;
				if (!text.IsNullOrEmpty())
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06008998 RID: 35224 RVA: 0x003988CC File Offset: 0x00396CCC
		public void UpdateProfile()
		{
			this.profileItem.textHandle.text = Singleton<GameSystem>.Instance.HandleName;
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x003988E8 File Offset: 0x00396CE8
		private void Awake()
		{
			HashSet<GameObject> hashSet = new HashSet<GameObject>(this.objMale);
			hashSet.UnionWith(this.objFemale);
			this.objSexAll = hashSet.ToArray<GameObject>();
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x0039891C File Offset: 0x00396D1C
		private IEnumerator Start()
		{
			yield return new WaitWhile(() => !Singleton<Housing>.IsInstance() || !Singleton<Housing>.Instance.IsLoadList);
			this.netInfo.selectCharaFWindow.UpdateWindowInUploader(true, 1, false, null);
			this.netInfo.selectCharaFWindow.cscChara.onSelect = delegate(CustomCharaFileInfo info)
			{
				this.ChangeUploadData();
			};
			this.netInfo.selectCharaFWindow.cscChara.onDeSelect = delegate()
			{
				this.ChangeUploadData();
			};
			this.netInfo.selectCharaMWindow.UpdateWindowInUploader(true, 0, false, null);
			this.netInfo.selectCharaMWindow.cscChara.onSelect = delegate(CustomCharaFileInfo info)
			{
				this.ChangeUploadData();
			};
			this.netInfo.selectCharaMWindow.cscChara.onDeSelect = delegate()
			{
				this.ChangeUploadData();
			};
			this.netInfo.selectHousingWindow.Init();
			this.netInfo.selectHousingWindow.onSelect = delegate(int idx)
			{
				this.ChangeUploadData();
			};
			this.personalities = (from x in Singleton<Voice>.Instance.voiceInfoDic.Values
			where 0 <= x.No
			select x).ToArray<VoiceInfo.Param>();
			if (null != this.profileItem.btnChangeHandle)
			{
				this.profileItem.btnChangeHandle.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					Singleton<Scene>.Instance.LoadReserve(new Scene.Data
					{
						levelName = "EntryHandleName",
						isAdd = true,
						isFade = false,
						onLoad = delegate
						{
							string backHandleName = Singleton<GameSystem>.Instance.HandleName;
							EntryHandleName rootComponent = Scene.GetRootComponent<EntryHandleName>("EntryHandleName");
							if (rootComponent != null)
							{
								rootComponent.backSceneName = "Uploader";
							}
							rootComponent.onEnd = delegate()
							{
								if (backHandleName != Singleton<GameSystem>.Instance.HandleName)
								{
									this.netInfo.BlockUI();
									Observable.FromCoroutine<string>((IObserver<string> res) => this.phpCtrl.UpdateHandleName(res)).Subscribe(delegate(string __)
									{
									}, delegate(Exception err)
									{
										Singleton<GameSystem>.Instance.SaveHandleName(backHandleName);
										this.netInfo.UnblockUI();
									}, delegate()
									{
										this.UpdateProfile();
										NetworkInfo.UserInfo userInfo;
										if (this.netInfo.dictUserInfo.TryGetValue(this.netInfo.profile.userIdx, out userInfo))
										{
											userInfo.handleName = Singleton<GameSystem>.Instance.HandleName;
										}
										this.netInfo.UnblockUI();
									});
								}
							};
						}
					}, false);
				});
			}
			if (null != this.uploadItem.inpComment)
			{
				this.uploadItem.inpComment.OnEndEditAsObservable().Subscribe(delegate(string val)
				{
					this.uploadItem.bkComment[this.dataType] = val;
				});
			}
			if (null != this.uploadItem.tglAgreePolicy)
			{
				this.uploadItem.tglAgreePolicy.isOn = Singleton<GameSystem>.Instance.agreePolicy;
				this.uploadItem.tglAgreePolicy.OnValueChangedAsObservable().Subscribe(delegate(bool on)
				{
					if (Singleton<Game>.IsInstance())
					{
						if (Singleton<GameSystem>.Instance.agreePolicy != on)
						{
							Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
						}
						Singleton<GameSystem>.Instance.agreePolicy = on;
						Singleton<GameSystem>.Instance.SaveNetworkSetting();
					}
				});
			}
			if (null != this.uploadItem.btnPolicy)
			{
				this.uploadItem.btnPolicy.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
					if (null != this.uploadItem.objPolicy)
					{
						this.uploadItem.objPolicy.SetActiveIfDifferent(true);
					}
				});
			}
			if (null != this.uploadItem.exitPolicy)
			{
				(from _ in this.uploadItem.exitPolicy.UpdateAsObservable()
				where UnityEngine.Input.anyKey
				select _).Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					if (null != this.uploadItem.objPolicy)
					{
						this.uploadItem.objPolicy.SetActiveIfDifferent(false);
					}
				});
			}
			if (null != this.uploadItem.btnUpload)
			{
				Text text = this.uploadItem.btnUpload.GetComponentInChildren<Text>(true);
				this.uploadItem.btnUpload.OnClickAsObservable().Subscribe(delegate(Unit pusth)
				{
					EventSystem.current.SetSelectedGameObject(null);
					DataType dataType = (DataType)this.dataType;
					if (dataType != DataType.Chara)
					{
						if (dataType == DataType.Housing)
						{
							Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.UploadHousing(res)).Subscribe(delegate(bool __)
							{
							}, delegate(Exception err)
							{
							}, delegate()
							{
							});
						}
					}
					else
					{
						Observable.FromCoroutine<bool>((IObserver<bool> res) => this.phpCtrl.UploadChara(res, this.uploadItem.modeUpdate)).Subscribe(delegate(bool __)
						{
						}, delegate(Exception err)
						{
						}, delegate()
						{
						});
					}
				});
				this.uploadItem.btnUpload.UpdateAsObservable().Subscribe(delegate(Unit _)
				{
					bool flag = string.Empty != this.GetUploadFile((DataType)this.dataType) && this.uploadItem.tglAgreePolicy.isOn;
					if (this.uploadItem.btnUpload.interactable != flag)
					{
						this.uploadItem.btnUpload.interactable = flag;
						text.color = new Color(text.color.r, text.color.g, text.color.b, (!flag) ? 0.5f : 1f);
					}
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
			this._dataType.Subscribe(delegate(int no)
			{
				if (no != 0)
				{
					if (no == 1)
					{
						if (null != this.objCharaTop)
						{
							this.objCharaTop.SetActiveIfDifferent(false);
						}
						if (null != this.objHousingTop)
						{
							this.objHousingTop.SetActiveIfDifferent(true);
						}
						if (null != this.uploadItem.inpComment)
						{
							this.uploadItem.inpComment.text = this.uploadItem.bkComment[1];
						}
					}
				}
				else
				{
					if (null != this.objCharaTop)
					{
						this.objCharaTop.SetActiveIfDifferent(true);
					}
					if (null != this.objHousingTop)
					{
						this.objHousingTop.SetActiveIfDifferent(false);
					}
					if (null != this.uploadItem.inpComment)
					{
						this.uploadItem.inpComment.text = this.uploadItem.bkComment[0];
					}
				}
				this.ChangeUploadData();
			});
			(from f in this._updateCharaInfo
			where f
			select f).Subscribe(delegate(bool f)
			{
				this.UpdateInfoChara();
				this.updateCharaInfo = false;
			});
			this._updateFemale.Subscribe(delegate(bool f)
			{
				foreach (GameObject self in this.objSexAll)
				{
					self.SetActiveIfDifferent(false);
				}
				foreach (GameObject self2 in this.objMale)
				{
					self2.SetActiveIfDifferent(!f);
				}
				foreach (GameObject self3 in this.objFemale)
				{
					self3.SetActiveIfDifferent(f);
				}
			});
			if (null != this.tglFemale)
			{
				this.tglFemale.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					this.updateFemale = isOn;
				});
			}
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
			if (null != this.btnTitle)
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
			if (null != this.btnDownloader)
			{
				this.btnDownloader.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					this.netInfo.BlockUI();
					EventSystem.current.SetSelectedGameObject(null);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					Singleton<GameSystem>.Instance.networkSceneName = "Downloader";
					Singleton<Scene>.Instance.LoadReserve(new Scene.Data
					{
						levelName = "NetworkCheckScene",
						isFade = true
					}, false);
				});
			}
			yield break;
		}

		// Token: 0x04006F70 RID: 28528
		public UpPhpControl phpCtrl;

		// Token: 0x04006F71 RID: 28529
		[Header("---< タイプ別表示OBJ >-----------------------")]
		[SerializeField]
		private GameObject objCharaTop;

		// Token: 0x04006F72 RID: 28530
		[SerializeField]
		private GameObject objHousingTop;

		// Token: 0x04006F73 RID: 28531
		private GameObject[] objSexAll;

		// Token: 0x04006F74 RID: 28532
		[SerializeField]
		private GameObject[] objMale;

		// Token: 0x04006F75 RID: 28533
		[SerializeField]
		private GameObject[] objFemale;

		// Token: 0x04006F76 RID: 28534
		[SerializeField]
		private GameObject[] objHideH;

		// Token: 0x04006F77 RID: 28535
		[SerializeField]
		private Toggle tglFemale;

		// Token: 0x04006F78 RID: 28536
		[Header("---< モード・タイプ切り替え >----------------")]
		[SerializeField]
		private Toggle[] tglDataType;

		// Token: 0x04006F79 RID: 28537
		[Header("---< 選択情報・キャラ >----------------------")]
		[SerializeField]
		private UpUIControl.SelectInfoChara selInfoCha;

		// Token: 0x04006F7A RID: 28538
		[Header("---< 選択情報・ハウジング >----------------------")]
		[SerializeField]
		private UpUIControl.SelectInfoHousing selInfoHousing;

		// Token: 0x04006F7B RID: 28539
		[Header("---< アップロード >--------------------------")]
		[SerializeField]
		private UpUIControl.UploadItem uploadItem;

		// Token: 0x04006F7C RID: 28540
		[Header("---< プロフィール >--------------------------")]
		[SerializeField]
		private UpUIControl.ProfileItem profileItem;

		// Token: 0x04006F7D RID: 28541
		[Header("---< その他 >--------------------------------")]
		[SerializeField]
		private Button btnTitle;

		// Token: 0x04006F7E RID: 28542
		[SerializeField]
		private Button btnDownloader;

		// Token: 0x04006F7F RID: 28543
		[SerializeField]
		private Text textNewestVersion;

		// Token: 0x04006F80 RID: 28544
		private VoiceInfo.Param[] personalities;

		// Token: 0x04006F81 RID: 28545
		private IntReactiveProperty _dataType = new IntReactiveProperty(0);

		// Token: 0x04006F82 RID: 28546
		private BoolReactiveProperty _updateCharaInfo = new BoolReactiveProperty(false);

		// Token: 0x04006F83 RID: 28547
		private BoolReactiveProperty _updateFemale = new BoolReactiveProperty(true);

		// Token: 0x04006F84 RID: 28548
		private BoolReactiveProperty _updateHousingInfo = new BoolReactiveProperty(false);

		// Token: 0x04006F85 RID: 28549
		private BoolReactiveProperty _updateAllInfo = new BoolReactiveProperty(false);

		// Token: 0x04006F86 RID: 28550
		private Dictionary<int, int> dictVoiceInfo = new Dictionary<int, int>();

		// Token: 0x02000FF8 RID: 4088
		[Serializable]
		public class SelectInfoChara
		{
			// Token: 0x04006F87 RID: 28551
			public Text textName;

			// Token: 0x04006F88 RID: 28552
			public Text textType;

			// Token: 0x04006F89 RID: 28553
			public Text textBirthDay;

			// Token: 0x04006F8A RID: 28554
			public GameObject objOnPhase01;

			// Token: 0x04006F8B RID: 28555
			public GameObject objOnPhase02;

			// Token: 0x04006F8C RID: 28556
			public GameObject objOnPhase03;

			// Token: 0x04006F8D RID: 28557
			public GameObject objOnPhase04;

			// Token: 0x04006F8E RID: 28558
			public Text textLifeStyle;

			// Token: 0x04006F8F RID: 28559
			public Text textPheromone;

			// Token: 0x04006F90 RID: 28560
			public Text textReliability;

			// Token: 0x04006F91 RID: 28561
			public Text textReason;

			// Token: 0x04006F92 RID: 28562
			public Text textInstinct;

			// Token: 0x04006F93 RID: 28563
			public Text textDirty;

			// Token: 0x04006F94 RID: 28564
			public Text textWariness;

			// Token: 0x04006F95 RID: 28565
			public Text textSociability;

			// Token: 0x04006F96 RID: 28566
			public Text textDarkness;

			// Token: 0x04006F97 RID: 28567
			public Text textSkill_n01;

			// Token: 0x04006F98 RID: 28568
			public Text textSkill_n02;

			// Token: 0x04006F99 RID: 28569
			public Text textSkill_n03;

			// Token: 0x04006F9A RID: 28570
			public Text textSkill_n04;

			// Token: 0x04006F9B RID: 28571
			public Text textSkill_n05;

			// Token: 0x04006F9C RID: 28572
			public Text textSkill_h01;

			// Token: 0x04006F9D RID: 28573
			public Text textSkill_h02;

			// Token: 0x04006F9E RID: 28574
			public Text textSkill_h03;

			// Token: 0x04006F9F RID: 28575
			public Text textSkill_h04;

			// Token: 0x04006FA0 RID: 28576
			public Text textSkill_h05;

			// Token: 0x04006FA1 RID: 28577
			public Text textWish_01;

			// Token: 0x04006FA2 RID: 28578
			public Text textWish_02;

			// Token: 0x04006FA3 RID: 28579
			public Text textWish_03;

			// Token: 0x04006FA4 RID: 28580
			public Image imgThumbnail;
		}

		// Token: 0x02000FF9 RID: 4089
		[Serializable]
		public class SelectInfoHousing
		{
			// Token: 0x04006FA5 RID: 28581
			public Image imgThumbnail;
		}

		// Token: 0x02000FFA RID: 4090
		[Serializable]
		public class UploadItem
		{
			// Token: 0x04006FA6 RID: 28582
			public InputField inpTitle;

			// Token: 0x04006FA7 RID: 28583
			public InputField inpComment;

			// Token: 0x04006FA8 RID: 28584
			[HideInInspector]
			public string[] bkComment = new string[Enum.GetNames(typeof(DataType)).Length];

			// Token: 0x04006FA9 RID: 28585
			public Toggle tglAgreePolicy;

			// Token: 0x04006FAA RID: 28586
			public Button btnPolicy;

			// Token: 0x04006FAB RID: 28587
			public Button btnUpload;

			// Token: 0x04006FAC RID: 28588
			public UIBehaviour exitPolicy;

			// Token: 0x04006FAD RID: 28589
			public GameObject objPolicy;

			// Token: 0x04006FAE RID: 28590
			[HideInInspector]
			public bool modeUpdate;
		}

		// Token: 0x02000FFB RID: 4091
		[Serializable]
		public class ProfileItem
		{
			// Token: 0x04006FAF RID: 28591
			public Text textHandle;

			// Token: 0x04006FB0 RID: 28592
			public Button btnChangeHandle;
		}
	}
}
