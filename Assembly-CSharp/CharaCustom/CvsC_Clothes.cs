using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using Illusion.Extensions;
using MessagePack;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009F8 RID: 2552
	public class CvsC_Clothes : CvsBase
	{
		// Token: 0x06004B61 RID: 19297 RVA: 0x001CC8FD File Offset: 0x001CACFD
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x001CC928 File Offset: 0x001CAD28
		public void UpdateClothesList()
		{
			ChaListDefine.CategoryNo[] array = new ChaListDefine.CategoryNo[]
			{
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_top : ChaListDefine.CategoryNo.mo_top,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_bot : ChaListDefine.CategoryNo.mo_bot,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_inner_t : ChaListDefine.CategoryNo.unknown,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_inner_b : ChaListDefine.CategoryNo.unknown,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_gloves : ChaListDefine.CategoryNo.mo_gloves,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_panst : ChaListDefine.CategoryNo.unknown,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_socks : ChaListDefine.CategoryNo.unknown,
				(base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_shoes : ChaListDefine.CategoryNo.mo_shoes
			};
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(array[base.SNo], ChaListDefine.KeyType.Unknown);
			this.sscClothesType.CreateList(lst);
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x001CCA58 File Offset: 0x001CAE58
		public void RestrictClothesMenu()
		{
			CmpClothes cmpClothes = base.chaCtrl.cmpClothes[base.SNo];
			if (null == cmpClothes)
			{
				base.ShowOrHideTab(false, new int[]
				{
					1,
					2,
					3,
					4
				});
				return;
			}
			base.ShowOrHideTab(true, new int[]
			{
				1,
				2,
				3,
				4
			});
			List<int> list = new List<int>();
			if (!cmpClothes.useColorN01 && !cmpClothes.useColorA01)
			{
				list.Add(1);
			}
			this.ccsColorSet[0].EnableColorAlpha(cmpClothes.useColorA01);
			if (!cmpClothes.useColorN02 && !cmpClothes.useColorA02)
			{
				list.Add(2);
			}
			this.ccsColorSet[1].EnableColorAlpha(cmpClothes.useColorA02);
			if (!cmpClothes.useColorN03 && !cmpClothes.useColorA03)
			{
				list.Add(3);
			}
			this.ccsColorSet[2].EnableColorAlpha(cmpClothes.useColorA03);
			base.ShowOrHideTab(false, list.ToArray());
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (this.ssBreak)
			{
				flag = cmpClothes.useBreak;
				this.ssBreak.gameObject.SetActiveIfDifferent(flag);
			}
			if (this.tglOption01)
			{
				flag2 = (cmpClothes.objOpt01 != null && 0 != cmpClothes.objOpt01.Length);
				this.tglOption01.gameObject.SetActiveIfDifferent(flag2);
			}
			if (this.tglOption02)
			{
				flag3 = (cmpClothes.objOpt02 != null && 0 != cmpClothes.objOpt02.Length);
				this.tglOption02.gameObject.SetActiveIfDifferent(flag3);
			}
			base.ShowOrHideTab(flag || flag2 || flag3, new int[]
			{
				4
			});
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x001CCC29 File Offset: 0x001CB029
		private void CalculateUI()
		{
			this.ssBreak.SetSliderValue(base.nowClothes.parts[base.SNo].breakRate);
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x001CCC50 File Offset: 0x001CB050
		public override void UpdateCustomUI()
		{
			if (this.backSNo != base.SNo)
			{
				int[] array = new int[]
				{
					0,
					0,
					1,
					1,
					1,
					1,
					1,
					0
				};
				base.customBase.ChangeClothesStateAuto(array[base.SNo]);
				this.UpdateClothesList();
				for (int i = 0; i < this.ccsColorSet.Length; i++)
				{
					this.ccsColorSet[i].Initialize(base.SNo, i);
				}
				this.backSNo = base.SNo;
			}
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscClothesType.SetToggleID(base.nowClothes.parts[base.SNo].id);
			if (this.tglOption01)
			{
				this.tglOption01.SetIsOnWithoutCallback(!base.nowClothes.parts[base.SNo].hideOpt[0]);
			}
			if (this.tglOption02)
			{
				this.tglOption02.SetIsOnWithoutCallback(!base.nowClothes.parts[base.SNo].hideOpt[1]);
			}
			for (int j = 0; j < this.ccsColorSet.Length; j++)
			{
				this.ccsColorSet[j].UpdateCustomUI();
			}
			base.customBase.RestrictSubMenu();
			this.RestrictClothesMenu();
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x001CCDA8 File Offset: 0x001CB1A8
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssBreak.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.nowClothes.parts[base.SNo].breakRate));
			yield break;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x001CCDC4 File Offset: 0x001CB1C4
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsClothes += this.UpdateCustomUI;
			base.customBase.RestrictSubMenu();
			this.UpdateClothesList();
			this.sscClothesType.SetToggleID(base.nowClothes.parts[base.SNo].id);
			this.sscClothesType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.nowClothes.parts[base.SNo].id != info.id)
				{
					base.chaCtrl.ChangeClothes(base.SNo, info.id, false);
					base.orgClothes.parts[base.SNo].id = base.nowClothes.parts[base.SNo].id;
					for (int i = 0; i < 3; i++)
					{
						base.orgClothes.parts[base.SNo].colorInfo[i].baseColor = base.nowClothes.parts[base.SNo].colorInfo[i].baseColor;
						this.ccsColorSet[i].UpdateCustomUI();
					}
					if (base.SNo == 0 || base.SNo == 2)
					{
						base.customBase.RestrictSubMenu();
					}
					this.RestrictClothesMenu();
				}
			};
			if (this.btnColorAllReset)
			{
				this.btnColorAllReset.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					base.chaCtrl.SetClothesDefaultSetting(base.SNo);
					for (int i = 0; i < 3; i++)
					{
						byte[] bytes = MessagePackSerializer.Serialize<ChaFileClothes.PartsInfo.ColorInfo>(base.nowClothes.parts[base.SNo].colorInfo[i]);
						base.orgClothes.parts[base.SNo].colorInfo[i] = MessagePackSerializer.Deserialize<ChaFileClothes.PartsInfo.ColorInfo>(bytes);
					}
					base.chaCtrl.ChangeCustomClothes(base.SNo, true, true, true, true);
					for (int j = 0; j < this.ccsColorSet.Length; j++)
					{
						this.ccsColorSet[j].UpdateCustomUI();
					}
				});
			}
			this.ccsColorSet[0].Initialize(base.SNo, 0);
			this.ccsColorSet[1].Initialize(base.SNo, 1);
			this.ccsColorSet[2].Initialize(base.SNo, 2);
			this.ssBreak.onChange = delegate(float value)
			{
				base.nowClothes.parts[base.SNo].breakRate = value;
				base.orgClothes.parts[base.SNo].breakRate = value;
				base.chaCtrl.ChangeBreakClothes(base.SNo);
			};
			this.ssBreak.onSetDefaultValue = (() => 0f);
			if (this.tglOption01)
			{
				this.tglOption01.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					base.nowClothes.parts[base.SNo].hideOpt[0] = !isOn;
					base.orgClothes.parts[base.SNo].hideOpt[0] = !isOn;
				});
			}
			if (this.tglOption02)
			{
				this.tglOption02.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					base.nowClothes.parts[base.SNo].hideOpt[1] = !isOn;
					base.orgClothes.parts[base.SNo].hideOpt[1] = !isOn;
				});
			}
			base.StartCoroutine(this.SetInputText());
			this.backSNo = base.SNo;
		}

		// Token: 0x04004558 RID: 17752
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscClothesType;

		// Token: 0x04004559 RID: 17753
		[SerializeField]
		private Button btnColorAllReset;

		// Token: 0x0400455A RID: 17754
		[Header("【設定02～04】-------------------")]
		[SerializeField]
		private CustomClothesColorSet[] ccsColorSet;

		// Token: 0x0400455B RID: 17755
		[Header("【設定05】----------------------")]
		[SerializeField]
		private CustomSliderSet ssBreak;

		// Token: 0x0400455C RID: 17756
		[SerializeField]
		private Toggle tglOption01;

		// Token: 0x0400455D RID: 17757
		[SerializeField]
		private Toggle tglOption02;

		// Token: 0x0400455E RID: 17758
		private int backSNo = -1;
	}
}
