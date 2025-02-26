using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using MessagePack;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009FF RID: 2559
	public class CvsA_Slot : CvsBase
	{
		// Token: 0x06004BAD RID: 19373 RVA: 0x001CF5E4 File Offset: 0x001CD9E4
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x001CF610 File Offset: 0x001CDA10
		public void UpdateAcsList(int ForceNo = -1)
		{
			ChaListDefine.CategoryNo[] array = new ChaListDefine.CategoryNo[]
			{
				ChaListDefine.CategoryNo.ao_none,
				ChaListDefine.CategoryNo.ao_head,
				ChaListDefine.CategoryNo.ao_ear,
				ChaListDefine.CategoryNo.ao_glasses,
				ChaListDefine.CategoryNo.ao_face,
				ChaListDefine.CategoryNo.ao_neck,
				ChaListDefine.CategoryNo.ao_shoulder,
				ChaListDefine.CategoryNo.ao_chest,
				ChaListDefine.CategoryNo.ao_waist,
				ChaListDefine.CategoryNo.ao_back,
				ChaListDefine.CategoryNo.ao_arm,
				ChaListDefine.CategoryNo.ao_hand,
				ChaListDefine.CategoryNo.ao_leg,
				ChaListDefine.CategoryNo.ao_kokan
			};
			int num = (ForceNo != -1) ? ForceNo : (base.nowAcs.parts[base.SNo].type - 350);
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(array[num], ChaListDefine.KeyType.Unknown);
			this.sscAcs.CreateList(lst);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x001CF674 File Offset: 0x001CDA74
		public void RestrictAcsMenu()
		{
			if (base.nowAcs.parts[base.SNo].type == 350)
			{
				if (this.objAcsSelect)
				{
					this.objAcsSelect.SetActiveIfDifferent(false);
				}
				base.ShowOrHideTab(false, new int[]
				{
					1,
					2,
					3,
					4
				});
				return;
			}
			CmpAccessory cmpAccessory = base.chaCtrl.cmpAccessory[base.SNo];
			if (null == cmpAccessory)
			{
				return;
			}
			if (null != this.objAcsSelect)
			{
				this.objAcsSelect.SetActiveIfDifferent(true);
			}
			base.ShowOrHideTab(true, new int[]
			{
				1,
				2,
				3,
				4
			});
			if (cmpAccessory.typeHair)
			{
				base.ShowOrHideTab(false, new int[]
				{
					1
				});
			}
			else
			{
				if (!cmpAccessory.useColor01 && !cmpAccessory.useColor02 && !cmpAccessory.useColor03 && (cmpAccessory.rendAlpha == null || cmpAccessory.rendAlpha.Length == 0))
				{
					base.ShowOrHideTab(false, new int[]
					{
						1
					});
				}
				base.ShowOrHideTab(false, new int[]
				{
					2
				});
			}
			if (!cmpAccessory.typeHair)
			{
				int num = 1;
				if (null != this.objColorGrp[0])
				{
					this.objColorGrp[0].SetActiveIfDifferent(cmpAccessory.useColor01);
				}
				if (null != this.objColorGrp[1])
				{
					this.objColorGrp[1].SetActiveIfDifferent(cmpAccessory.useColor02);
				}
				if (null != this.objColorGrp[2])
				{
					this.objColorGrp[2].SetActiveIfDifferent(cmpAccessory.useColor03);
				}
				if (null != this.objColorGrp[3])
				{
					this.objColorGrp[3].SetActiveIfDifferent(cmpAccessory.rendAlpha != null && 0 != cmpAccessory.rendAlpha.Length);
				}
				if (null != this.textColorTitle[0] && cmpAccessory.useColor01)
				{
					this.textColorTitle[0].text = string.Format("{0}{1}", "カラー", num++);
				}
				if (null != this.textColorTitle[1] && cmpAccessory.useColor02)
				{
					this.textColorTitle[1].text = string.Format("{0}{1}", "カラー", num++);
				}
				if (null != this.textColorTitle[2] && cmpAccessory.useColor03)
				{
					this.textColorTitle[2].text = string.Format("{0}{1}", "カラー", num++);
				}
				if (null != this.textColorTitle[3] && cmpAccessory.rendAlpha != null && cmpAccessory.rendAlpha.Length != 0)
				{
					this.textColorTitle[3].text = string.Format("{0}{1}", "カラー", num++);
				}
			}
			else
			{
				if (null != this.csHairTopColor)
				{
					this.csHairTopColor.gameObject.SetActiveIfDifferent(cmpAccessory.useColor02);
				}
				if (null != this.csHairUnderColor)
				{
					this.csHairUnderColor.gameObject.SetActiveIfDifferent(cmpAccessory.useColor03);
				}
			}
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x001CF9DC File Offset: 0x001CDDDC
		public void SetDefaultColor()
		{
			CmpAccessory cmpAccessory = base.chaCtrl.cmpAccessory[base.SNo];
			if (null == cmpAccessory)
			{
				return;
			}
			if (!cmpAccessory.typeHair)
			{
				base.chaCtrl.SetAccessoryDefaultColor(base.SNo);
				for (int i = 0; i < 4; i++)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileAccessory.PartsInfo.ColorInfo>(base.nowAcs.parts[base.SNo].colorInfo[i]);
					base.orgAcs.parts[base.SNo].colorInfo[i] = MessagePackSerializer.Deserialize<ChaFileAccessory.PartsInfo.ColorInfo>(bytes);
				}
			}
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x001CFA78 File Offset: 0x001CDE78
		public void ChangeAcsType(int idx)
		{
			if (base.nowAcs.parts[base.SNo].type - 350 == idx)
			{
				return;
			}
			base.nowAcs.parts[base.SNo].type = 350 + idx;
			base.orgAcs.parts[base.SNo].type = base.nowAcs.parts[base.SNo].type;
			base.nowAcs.parts[base.SNo].parentKey = string.Empty;
			for (int i = 0; i < 2; i++)
			{
				base.orgAcs.parts[base.SNo].addMove[i, 0] = (base.nowAcs.parts[base.SNo].addMove[i, 0] = Vector3.zero);
				base.orgAcs.parts[base.SNo].addMove[i, 1] = (base.nowAcs.parts[base.SNo].addMove[i, 1] = Vector3.zero);
				base.orgAcs.parts[base.SNo].addMove[i, 2] = (base.nowAcs.parts[base.SNo].addMove[i, 2] = Vector3.one);
			}
			base.chaCtrl.ChangeAccessory(base.SNo, base.nowAcs.parts[base.SNo].type, ChaAccessoryDefine.AccessoryDefaultIndex[idx], string.Empty, true);
			this.SetDefaultColor();
			base.chaCtrl.ChangeAccessoryColor(base.SNo);
			base.orgAcs.parts[base.SNo].id = base.nowAcs.parts[base.SNo].id;
			base.orgAcs.parts[base.SNo].parentKey = base.nowAcs.parts[base.SNo].parentKey;
			base.nowAcs.parts[base.SNo].noShake = false;
			base.orgAcs.parts[base.SNo].noShake = false;
			base.customBase.ChangeAcsSlotName(base.SNo);
			this.UpdateAcsList(-1);
			base.customBase.forceUpdateAcsList = true;
			this.UpdateCustomUI();
			base.customBase.showAcsControllerAll = base.customBase.chaCtrl.IsAccessory(base.SNo);
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x001CFD34 File Offset: 0x001CE134
		public void ChangeAcsId(int id)
		{
			bool flag = false;
			if (base.chaCtrl.cmpAccessory != null && null != base.chaCtrl.cmpAccessory[base.SNo])
			{
				flag = base.chaCtrl.cmpAccessory[base.SNo].typeHair;
			}
			base.chaCtrl.ChangeAccessory(base.SNo, base.nowAcs.parts[base.SNo].type, id, string.Empty, false);
			this.SetDefaultColor();
			base.chaCtrl.ChangeAccessoryColor(base.SNo);
			bool flag2 = false;
			if (base.chaCtrl.cmpAccessory != null && null != base.chaCtrl.cmpAccessory[base.SNo])
			{
				flag2 = base.chaCtrl.cmpAccessory[base.SNo].typeHair;
			}
			if (!flag && flag2)
			{
				this.ChangeHairTypeAccessoryColor(0);
			}
			base.orgAcs.parts[base.SNo].id = base.nowAcs.parts[base.SNo].id;
			base.orgAcs.parts[base.SNo].parentKey = base.nowAcs.parts[base.SNo].parentKey;
			base.nowAcs.parts[base.SNo].noShake = false;
			base.orgAcs.parts[base.SNo].noShake = false;
			base.customBase.ChangeAcsSlotName(base.SNo);
			this.UpdateCustomUI();
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x001CFECC File Offset: 0x001CE2CC
		public void ChangeAcsParent(int idx)
		{
			string[] array = (from key in Enum.GetNames(typeof(ChaAccessoryDefine.AccessoryParentKey))
			where key != "none"
			select key).ToArray<string>();
			string text = array[idx];
			if (base.nowAcs.parts[base.SNo].parentKey != text)
			{
				base.chaCtrl.ChangeAccessoryParent(base.SNo, text);
				base.orgAcs.parts[base.SNo].parentKey = base.nowAcs.parts[base.SNo].parentKey;
			}
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x001CFF78 File Offset: 0x001CE378
		private void CalculateUI()
		{
			CmpAccessory cmpAccessory = base.chaCtrl.cmpAccessory[base.SNo];
			if (null == cmpAccessory)
			{
				return;
			}
			if (!cmpAccessory.typeHair)
			{
				for (int i = 0; i < this.ssGloss.Length; i++)
				{
					this.ssGloss[i].SetSliderValue(base.nowAcs.parts[base.SNo].colorInfo[i].glossPower);
				}
				for (int j = 0; j < this.ssMetallic.Length; j++)
				{
					this.ssMetallic[j].SetSliderValue(base.nowAcs.parts[base.SNo].colorInfo[j].metallicPower);
				}
			}
			else
			{
				this.ssHairSmoothness.SetSliderValue(base.nowAcs.parts[base.SNo].colorInfo[0].smoothnessPower);
				this.ssHairMetallic.SetSliderValue(base.nowAcs.parts[base.SNo].colorInfo[0].metallicPower);
			}
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x001D0090 File Offset: 0x001CE490
		public override void UpdateCustomUI()
		{
			if (this.backSNo != base.SNo)
			{
				this.UpdateAcsList(-1);
				for (int i = 0; i < this.acCorrect.Length; i++)
				{
					this.acCorrect[i].Initialize(base.SNo, i);
				}
				this.backSNo = base.SNo;
			}
			else if (base.customBase.forceUpdateAcsList)
			{
				this.UpdateAcsList(-1);
				base.customBase.forceUpdateAcsList = false;
			}
			base.customBase.showAcsControllerAll = base.customBase.chaCtrl.IsAccessory(base.SNo);
			if (!this.mainMenu.IsSelectAccessory())
			{
				base.customBase.showAcsControllerAll = false;
			}
			base.UpdateCustomUI();
			this.CalculateUI();
			int num = base.nowAcs.parts[base.SNo].type - 350;
			for (int j = 0; j < this.tglType.Length; j++)
			{
				this.tglType[j].SetIsOnWithoutCallback(num == j);
			}
			CmpAccessory cmpAccessory = base.chaCtrl.cmpAccessory[base.SNo];
			bool flag = false;
			if (null != cmpAccessory)
			{
				flag = cmpAccessory.typeHair;
			}
			if (flag)
			{
				this.csHairBaseColor.SetColor(base.nowAcs.parts[base.SNo].colorInfo[0].color);
				this.csHairTopColor.SetColor(base.nowAcs.parts[base.SNo].colorInfo[1].color);
				this.csHairUnderColor.SetColor(base.nowAcs.parts[base.SNo].colorInfo[2].color);
				this.csHairSpecular.SetColor(base.nowAcs.parts[base.SNo].colorInfo[3].color);
			}
			else
			{
				for (int k = 0; k < this.csColor.Length; k++)
				{
					this.csColor[k].SetColor(base.nowAcs.parts[base.SNo].colorInfo[k].color);
				}
			}
			this.sscAcs.SetToggleID(base.nowAcs.parts[base.SNo].id);
			int num2 = ChaAccessoryDefine.GetAccessoryParentInt(base.nowAcs.parts[base.SNo].parentKey) - 1;
			if (0 <= num2)
			{
				for (int l = 0; l < this.tglParent.Length; l++)
				{
					this.tglParent[l].SetIsOnWithoutCallback(num2 == l);
				}
			}
			this.tglNoShake.SetIsOnWithoutCallback(base.nowAcs.parts[base.SNo].noShake);
			bool[] array = new bool[2];
			if (null != base.chaCtrl.cmpAccessory[base.SNo])
			{
				if (null != base.chaCtrl.cmpAccessory[base.SNo].trfMove01)
				{
					array[0] = true;
				}
				if (null != base.chaCtrl.cmpAccessory[base.SNo].trfMove02)
				{
					array[1] = true;
				}
			}
			for (int m = 0; m < this.acCorrect.Length; m++)
			{
				this.acCorrect[m].gameObject.SetActiveIfDifferent(array[m]);
				this.acCorrect[m].UpdateCustomUI();
				base.customBase.showAcsController[m] = array[m];
			}
			this.RestrictAcsMenu();
			if (null != this.titleText)
			{
				ListInfoBase listInfo = base.chaCtrl.lstCtrl.GetListInfo((ChaListDefine.CategoryNo)base.nowAcs.parts[base.SNo].type, base.nowAcs.parts[base.SNo].id);
				this.titleText.text = string.Format("{0:00} {1}", base.SNo + 1, listInfo.Name);
			}
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x001D04B4 File Offset: 0x001CE8B4
		public void ChangeHairTypeAccessoryColor(int hairPartsNo)
		{
			base.nowAcs.parts[base.SNo].colorInfo[0].color = base.hair.parts[hairPartsNo].baseColor;
			base.nowAcs.parts[base.SNo].colorInfo[1].color = base.hair.parts[hairPartsNo].topColor;
			base.nowAcs.parts[base.SNo].colorInfo[2].color = base.hair.parts[hairPartsNo].underColor;
			base.nowAcs.parts[base.SNo].colorInfo[3].color = base.hair.parts[hairPartsNo].specular;
			base.nowAcs.parts[base.SNo].colorInfo[0].smoothnessPower = base.hair.parts[hairPartsNo].smoothness;
			base.nowAcs.parts[base.SNo].colorInfo[0].metallicPower = base.hair.parts[hairPartsNo].metallic;
			base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
			for (int i = 0; i < 4; i++)
			{
				byte[] bytes = MessagePackSerializer.Serialize<ChaFileAccessory.PartsInfo.ColorInfo>(base.nowAcs.parts[base.SNo].colorInfo[i]);
				base.orgAcs.parts[base.SNo].colorInfo[i] = MessagePackSerializer.Deserialize<ChaFileAccessory.PartsInfo.ColorInfo>(bytes);
			}
			this.csHairBaseColor.SetColor(base.nowAcs.parts[base.SNo].colorInfo[0].color);
			this.csHairTopColor.SetColor(base.nowAcs.parts[base.SNo].colorInfo[1].color);
			this.csHairUnderColor.SetColor(base.nowAcs.parts[base.SNo].colorInfo[2].color);
			this.csHairSpecular.SetColor(base.nowAcs.parts[base.SNo].colorInfo[3].color);
			this.ssHairSmoothness.SetSliderValue(base.nowAcs.parts[base.SNo].colorInfo[0].smoothnessPower);
			this.ssHairMetallic.SetSliderValue(base.nowAcs.parts[base.SNo].colorInfo[0].metallicPower);
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x001D073C File Offset: 0x001CEB3C
		public void ShortcutChangeGuidType(int type)
		{
			bool flag = false;
			foreach (CustomAcsCorrectSet customAcsCorrectSet in this.acCorrect)
			{
				if (customAcsCorrectSet.IsDrag())
				{
					flag = true;
				}
			}
			if (!flag)
			{
				foreach (CustomAcsCorrectSet customAcsCorrectSet2 in this.acCorrect)
				{
					customAcsCorrectSet2.ShortcutChangeGuidType(type);
				}
			}
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x001D07B0 File Offset: 0x001CEBB0
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			CmpAccessory cmp = base.chaCtrl.cmpAccessory[base.SNo];
			if (null == cmp)
			{
				yield break;
			}
			if (!cmp.typeHair)
			{
				for (int i = 0; i < this.ssGloss.Length; i++)
				{
					this.ssGloss[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.nowAcs.parts[base.SNo].colorInfo[i].glossPower));
				}
				for (int j = 0; j < this.ssMetallic.Length; j++)
				{
					this.ssMetallic[j].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.nowAcs.parts[base.SNo].colorInfo[j].metallicPower));
				}
			}
			else
			{
				this.ssHairSmoothness.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.nowAcs.parts[base.SNo].colorInfo[0].smoothnessPower));
				this.ssHairMetallic.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.nowAcs.parts[base.SNo].colorInfo[0].metallicPower));
			}
			yield break;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x001D07CC File Offset: 0x001CEBCC
		protected override void Start()
		{
			base.Start();
			base.customBase.ChangeAcsSlotName(-1);
			base.customBase.actUpdateCvsAccessory += this.UpdateCustomUI;
			if (this.tglType.Any<Toggle>())
			{
				(from tgl in this.tglType.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where tgl.val != null
				select tgl).ToList().ForEach(delegate(tgl)
				{
					(from isOn in tgl.val.onValueChanged.AsObservable<bool>()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.ChangeAcsType(tgl.idx);
					});
				});
			}
			this.UpdateAcsList(-1);
			this.sscAcs.SetToggleID(base.nowAcs.parts[base.SNo].id);
			this.sscAcs.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.nowAcs.parts[base.SNo].id != info.id)
				{
					this.ChangeAcsId(info.id);
				}
			};
			(from item in this.csColor.Select((CustomColorSet val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null
			select item).ToList().ForEach(delegate(item)
			{
				item.val.actUpdateColor = delegate(Color color)
				{
					this.nowAcs.parts[this.SNo].colorInfo[item.idx].color = color;
					this.orgAcs.parts[this.SNo].colorInfo[item.idx].color = color;
					this.chaCtrl.ChangeAccessoryColor(this.SNo);
				};
			});
			(from item in this.ssGloss.Select((CustomSliderSet val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null
			select item).ToList().ForEach(delegate(item)
			{
				item.val.onChange = delegate(float value)
				{
					this.nowAcs.parts[this.SNo].colorInfo[item.idx].glossPower = value;
					this.orgAcs.parts[this.SNo].colorInfo[item.idx].glossPower = value;
					this.chaCtrl.ChangeAccessoryColor(this.SNo);
				};
				item.val.onSetDefaultValue = delegate()
				{
					if (null == this.chaCtrl.cmpAccessory[this.SNo])
					{
						return 0f;
					}
					switch (item.idx)
					{
					case 0:
						return this.chaCtrl.cmpAccessory[this.SNo].defGlossPower01;
					case 1:
						return this.chaCtrl.cmpAccessory[this.SNo].defGlossPower02;
					case 2:
						return this.chaCtrl.cmpAccessory[this.SNo].defGlossPower03;
					case 3:
						return this.chaCtrl.cmpAccessory[this.SNo].defGlossPower04;
					default:
						return 0f;
					}
				};
			});
			(from item in this.ssMetallic.Select((CustomSliderSet val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null
			select item).ToList().ForEach(delegate(item)
			{
				item.val.onChange = delegate(float value)
				{
					this.nowAcs.parts[this.SNo].colorInfo[item.idx].metallicPower = value;
					this.orgAcs.parts[this.SNo].colorInfo[item.idx].metallicPower = value;
					this.chaCtrl.ChangeAccessoryColor(this.SNo);
				};
				item.val.onSetDefaultValue = delegate()
				{
					if (null == this.chaCtrl.cmpAccessory[this.SNo])
					{
						return 0f;
					}
					switch (item.idx)
					{
					case 0:
						return this.chaCtrl.cmpAccessory[this.SNo].defMetallicPower01;
					case 1:
						return this.chaCtrl.cmpAccessory[this.SNo].defMetallicPower02;
					case 2:
						return this.chaCtrl.cmpAccessory[this.SNo].defMetallicPower03;
					case 3:
						return this.chaCtrl.cmpAccessory[this.SNo].defMetallicPower04;
					default:
						return 0f;
					}
				};
			});
			if (null != this.btnDefaultColor)
			{
				this.btnDefaultColor.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.SetDefaultColor();
					base.chaCtrl.ChangeAccessoryColor(base.SNo);
					this.UpdateCustomUI();
				});
			}
			if (this.csHairBaseColor)
			{
				this.csHairBaseColor.actUpdateColor = delegate(Color color)
				{
					base.nowAcs.parts[base.SNo].colorInfo[0].color = color;
					base.orgAcs.parts[base.SNo].colorInfo[0].color = color;
					base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
				};
			}
			if (this.csHairTopColor)
			{
				this.csHairTopColor.actUpdateColor = delegate(Color color)
				{
					base.nowAcs.parts[base.SNo].colorInfo[1].color = color;
					base.orgAcs.parts[base.SNo].colorInfo[1].color = color;
					base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
				};
			}
			if (this.csHairUnderColor)
			{
				this.csHairUnderColor.actUpdateColor = delegate(Color color)
				{
					base.nowAcs.parts[base.SNo].colorInfo[2].color = color;
					base.orgAcs.parts[base.SNo].colorInfo[2].color = color;
					base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
				};
			}
			if (this.csHairSpecular)
			{
				this.csHairSpecular.actUpdateColor = delegate(Color color)
				{
					base.nowAcs.parts[base.SNo].colorInfo[3].color = color;
					base.orgAcs.parts[base.SNo].colorInfo[3].color = color;
					base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
				};
			}
			if (this.ssHairMetallic)
			{
				this.ssHairMetallic.onChange = delegate(float value)
				{
					base.nowAcs.parts[base.SNo].colorInfo[0].metallicPower = value;
					base.orgAcs.parts[base.SNo].colorInfo[0].metallicPower = value;
					base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
				};
			}
			if (this.ssHairSmoothness)
			{
				this.ssHairSmoothness.onChange = delegate(float value)
				{
					base.nowAcs.parts[base.SNo].colorInfo[0].smoothnessPower = value;
					base.orgAcs.parts[base.SNo].colorInfo[0].smoothnessPower = value;
					base.chaCtrl.ChangeHairTypeAccessoryColor(base.SNo);
				};
			}
			(from item in this.btnGetHairColor.Select((Button val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null
			select item).ToList().ForEach(delegate(item)
			{
				item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.ChangeHairTypeAccessoryColor(item.idx);
				});
			});
			if (this.tglParent.Any<Toggle>())
			{
				(from tgl in this.tglParent.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where tgl.val != null
				select tgl).ToList().ForEach(delegate(tgl)
				{
					(from isOn in tgl.val.onValueChanged.AsObservable<bool>()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.ChangeAcsParent(tgl.idx);
					});
				});
			}
			if (null != this.btnDefaultParent)
			{
				this.btnDefaultParent.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					string accessoryDefaultParentStr = base.chaCtrl.GetAccessoryDefaultParentStr(base.SNo);
					base.chaCtrl.ChangeAccessoryParent(base.SNo, accessoryDefaultParentStr);
					base.orgAcs.parts[base.SNo].parentKey = base.nowAcs.parts[base.SNo].parentKey;
					this.UpdateCustomUI();
				});
			}
			this.tglNoShake.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				base.nowAcs.parts[base.SNo].noShake = isOn;
				base.orgAcs.parts[base.SNo].noShake = isOn;
			});
			GameObject[] array = new GameObject[]
			{
				base.customBase.objAcs01ControllerTop,
				base.customBase.objAcs02ControllerTop
			};
			for (int i = 0; i < this.acCorrect.Length; i++)
			{
				this.acCorrect[i].CreateGuid(array[i]);
				this.acCorrect[i].Initialize(base.SNo, i);
			}
			base.StartCoroutine(this.SetInputText());
			this.backSNo = base.SNo;
		}

		// Token: 0x0400458F RID: 17807
		[SerializeField]
		private CustomChangeMainMenu mainMenu;

		// Token: 0x04004590 RID: 17808
		[Header("【設定01】----------------------")]
		[SerializeField]
		private Toggle[] tglType;

		// Token: 0x04004591 RID: 17809
		[SerializeField]
		private GameObject objAcsSelect;

		// Token: 0x04004592 RID: 17810
		[SerializeField]
		private CustomSelectScrollController sscAcs;

		// Token: 0x04004593 RID: 17811
		[Header("【設定02】----------------------")]
		[SerializeField]
		private GameObject[] objColorGrp;

		// Token: 0x04004594 RID: 17812
		[SerializeField]
		private Text[] textColorTitle;

		// Token: 0x04004595 RID: 17813
		[SerializeField]
		private CustomColorSet[] csColor;

		// Token: 0x04004596 RID: 17814
		[SerializeField]
		private CustomSliderSet[] ssGloss;

		// Token: 0x04004597 RID: 17815
		[SerializeField]
		private CustomSliderSet[] ssMetallic;

		// Token: 0x04004598 RID: 17816
		[SerializeField]
		private Button btnDefaultColor;

		// Token: 0x04004599 RID: 17817
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomColorSet csHairBaseColor;

		// Token: 0x0400459A RID: 17818
		[SerializeField]
		private CustomColorSet csHairTopColor;

		// Token: 0x0400459B RID: 17819
		[SerializeField]
		private CustomColorSet csHairUnderColor;

		// Token: 0x0400459C RID: 17820
		[SerializeField]
		private CustomColorSet csHairSpecular;

		// Token: 0x0400459D RID: 17821
		[SerializeField]
		private CustomSliderSet ssHairMetallic;

		// Token: 0x0400459E RID: 17822
		[SerializeField]
		private CustomSliderSet ssHairSmoothness;

		// Token: 0x0400459F RID: 17823
		[SerializeField]
		private Button[] btnGetHairColor;

		// Token: 0x040045A0 RID: 17824
		[Header("【設定04】----------------------")]
		[SerializeField]
		private Toggle[] tglParent;

		// Token: 0x040045A1 RID: 17825
		[SerializeField]
		private Button btnDefaultParent;

		// Token: 0x040045A2 RID: 17826
		[Header("【設定05】----------------------")]
		[SerializeField]
		private Toggle tglNoShake;

		// Token: 0x040045A3 RID: 17827
		[SerializeField]
		private CustomAcsCorrectSet[] acCorrect;

		// Token: 0x040045A4 RID: 17828
		private int backSNo = -1;
	}
}
