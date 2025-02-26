using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E0 RID: 2528
	public class CvsF_Mole : CvsBase
	{
		// Token: 0x06004A49 RID: 19017 RVA: 0x001C53DD File Offset: 0x001C37DD
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x001C5408 File Offset: 0x001C3808
		private void CalculateUI()
		{
			this.ssMoleW.SetSliderValue(base.face.moleLayout.x);
			this.ssMoleH.SetSliderValue(base.face.moleLayout.y);
			this.ssMoleX.SetSliderValue(base.face.moleLayout.z);
			this.ssMoleY.SetSliderValue(base.face.moleLayout.w);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x001C548D File Offset: 0x001C388D
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscMole.SetToggleID(base.face.moleId);
			this.csMole.SetColor(base.face.moleColor);
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x001C54C8 File Offset: 0x001C38C8
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssMoleW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.moleLayout.x));
			this.ssMoleH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.moleLayout.y));
			this.ssMoleX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.moleLayout.z));
			this.ssMoleY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.moleLayout.w));
			yield break;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x001C54E4 File Offset: 0x001C38E4
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsMole += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_mole, ChaListDefine.KeyType.Unknown);
			this.sscMole.CreateList(lst);
			this.sscMole.SetToggleID(base.face.moleId);
			this.sscMole.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.moleId != info.id)
				{
					base.face.moleId = info.id;
					base.chaCtrl.AddUpdateCMFaceTexFlags(false, false, false, false, false, false, true);
					base.chaCtrl.CreateFaceTexture();
				}
			};
			this.csMole.actUpdateColor = delegate(Color color)
			{
				base.face.moleColor = color;
				base.chaCtrl.AddUpdateCMFaceColorFlags(false, false, false, false, false, false, true);
				base.chaCtrl.CreateFaceTexture();
			};
			Dictionary<int, ListInfoBase> categoryInfo = base.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mole_layout);
			this.dictMoleLayout = categoryInfo.Select((KeyValuePair<int, ListInfoBase> val, int idx) => new
			{
				idx = idx,
				x = val.Value.GetInfoFloat(ChaListDefine.KeyType.Scale),
				y = val.Value.GetInfoFloat(ChaListDefine.KeyType.Scale),
				z = val.Value.GetInfoFloat(ChaListDefine.KeyType.PosX),
				w = val.Value.GetInfoFloat(ChaListDefine.KeyType.PosY)
			}).ToDictionary(v => v.idx, v => new Vector4
			{
				x = v.x,
				y = v.y,
				z = v.z,
				w = v.w
			});
			List<CustomPushInfo> lst2 = CvsBase.CreatePushList(ChaListDefine.CategoryNo.mole_layout);
			this.pscMoleLayout.CreateList(lst2);
			this.pscMoleLayout.onPush = delegate(CustomPushInfo info)
			{
				Vector4 moleLayout;
				if (info != null && this.dictMoleLayout.TryGetValue(info.id, out moleLayout))
				{
					base.face.moleLayout = moleLayout;
					base.chaCtrl.AddUpdateCMFaceLayoutFlags(false, false, true);
					base.chaCtrl.CreateFaceTexture();
					this.ssMoleW.SetSliderValue(base.face.moleLayout.x);
					this.ssMoleH.SetSliderValue(base.face.moleLayout.y);
					this.ssMoleX.SetSliderValue(base.face.moleLayout.z);
					this.ssMoleY.SetSliderValue(base.face.moleLayout.w);
				}
			};
			this.ssMoleW.onChange = delegate(float value)
			{
				base.face.moleLayout = new Vector4(value, base.face.moleLayout.y, base.face.moleLayout.z, base.face.moleLayout.w);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(false, false, true);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssMoleW.onSetDefaultValue = (() => base.defChaCtrl.custom.face.moleLayout.x);
			this.ssMoleH.onChange = delegate(float value)
			{
				base.face.moleLayout = new Vector4(base.face.moleLayout.x, value, base.face.moleLayout.z, base.face.moleLayout.w);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(false, false, true);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssMoleH.onSetDefaultValue = (() => base.defChaCtrl.custom.face.moleLayout.y);
			this.ssMoleX.onChange = delegate(float value)
			{
				base.face.moleLayout = new Vector4(base.face.moleLayout.x, base.face.moleLayout.y, value, base.face.moleLayout.w);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(false, false, true);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssMoleX.onSetDefaultValue = (() => base.defChaCtrl.custom.face.moleLayout.z);
			this.ssMoleY.onChange = delegate(float value)
			{
				base.face.moleLayout = new Vector4(base.face.moleLayout.x, base.face.moleLayout.y, base.face.moleLayout.z, value);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(false, false, true);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssMoleY.onSetDefaultValue = (() => base.defChaCtrl.custom.face.moleLayout.w);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400448B RID: 17547
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscMole;

		// Token: 0x0400448C RID: 17548
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csMole;

		// Token: 0x0400448D RID: 17549
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomPushScrollController pscMoleLayout;

		// Token: 0x0400448E RID: 17550
		[SerializeField]
		private CustomSliderSet ssMoleW;

		// Token: 0x0400448F RID: 17551
		[SerializeField]
		private CustomSliderSet ssMoleH;

		// Token: 0x04004490 RID: 17552
		[SerializeField]
		private CustomSliderSet ssMoleX;

		// Token: 0x04004491 RID: 17553
		[SerializeField]
		private CustomSliderSet ssMoleY;

		// Token: 0x04004492 RID: 17554
		private Dictionary<int, Vector4> dictMoleLayout;
	}
}
