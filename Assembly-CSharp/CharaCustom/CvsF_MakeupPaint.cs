using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009DF RID: 2527
	public class CvsF_MakeupPaint : CvsBase
	{
		// Token: 0x06004A2F RID: 18991 RVA: 0x001C4589 File Offset: 0x001C2989
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x001C45B4 File Offset: 0x001C29B4
		private void CalculateUI()
		{
			this.ssPaintGloss.SetSliderValue(base.makeup.paintInfo[base.SNo].glossPower);
			this.ssPaintMetallic.SetSliderValue(base.makeup.paintInfo[base.SNo].metallicPower);
			this.ssPaintW.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.x);
			this.ssPaintH.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.y);
			this.ssPaintX.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.z);
			this.ssPaintY.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.w);
			this.ssPaintRot.SetSliderValue(base.makeup.paintInfo[base.SNo].rotation);
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x001C46D0 File Offset: 0x001C2AD0
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscPaintType.SetToggleID(base.makeup.paintInfo[base.SNo].id);
			this.csPaintColor.SetColor(base.makeup.paintInfo[base.SNo].color);
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x001C4730 File Offset: 0x001C2B30
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssPaintGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].glossPower));
			this.ssPaintMetallic.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].metallicPower));
			this.ssPaintW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].layout.x));
			this.ssPaintH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].layout.y));
			this.ssPaintX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].layout.z));
			this.ssPaintY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].layout.w));
			this.ssPaintRot.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.paintInfo[base.SNo].rotation));
			yield break;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x001C474C File Offset: 0x001C2B4C
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFacePaint += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_paint, ChaListDefine.KeyType.Unknown);
			this.sscPaintType.CreateList(lst);
			this.sscPaintType.SetToggleID(base.makeup.paintInfo[base.SNo].id);
			this.sscPaintType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.makeup.paintInfo[base.SNo].id != info.id)
				{
					base.makeup.paintInfo[base.SNo].id = info.id;
					base.chaCtrl.AddUpdateCMFaceTexFlags(false, false, 0 == base.SNo, 1 == base.SNo, false, false, false);
					base.chaCtrl.CreateFaceTexture();
				}
			};
			this.csPaintColor.actUpdateColor = delegate(Color color)
			{
				base.makeup.paintInfo[base.SNo].color = color;
				base.chaCtrl.AddUpdateCMFaceColorFlags(false, false, 0 == base.SNo, 1 == base.SNo, false, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintGloss.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].glossPower = value;
				base.chaCtrl.AddUpdateCMFaceGlossFlags(false, 0 == base.SNo, 1 == base.SNo, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].glossPower);
			this.ssPaintMetallic.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].metallicPower = value;
				base.chaCtrl.AddUpdateCMFaceGlossFlags(false, 0 == base.SNo, 1 == base.SNo, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintMetallic.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].metallicPower);
			Dictionary<int, ListInfoBase> categoryInfo = base.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.facepaint_layout);
			this.dictPaintLayout = categoryInfo.Select((KeyValuePair<int, ListInfoBase> val, int idx) => new
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
			List<CustomPushInfo> lst2 = CvsBase.CreatePushList(ChaListDefine.CategoryNo.facepaint_layout);
			this.pscPaintLayout.CreateList(lst2);
			this.pscPaintLayout.onPush = delegate(CustomPushInfo info)
			{
				Vector4 layout;
				if (info != null && this.dictPaintLayout.TryGetValue(info.id, out layout))
				{
					base.makeup.paintInfo[base.SNo].layout = layout;
					base.chaCtrl.AddUpdateCMFaceLayoutFlags(0 == base.SNo, 1 == base.SNo, false);
					base.chaCtrl.CreateFaceTexture();
					this.ssPaintW.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.x);
					this.ssPaintH.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.y);
					this.ssPaintX.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.z);
					this.ssPaintY.SetSliderValue(base.makeup.paintInfo[base.SNo].layout.w);
				}
			};
			this.ssPaintW.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].layout = new Vector4(value, base.makeup.paintInfo[base.SNo].layout.y, base.makeup.paintInfo[base.SNo].layout.z, base.makeup.paintInfo[base.SNo].layout.w);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(0 == base.SNo, 1 == base.SNo, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintW.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].layout.x);
			this.ssPaintH.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].layout = new Vector4(base.makeup.paintInfo[base.SNo].layout.x, value, base.makeup.paintInfo[base.SNo].layout.z, base.makeup.paintInfo[base.SNo].layout.w);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(0 == base.SNo, 1 == base.SNo, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintH.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].layout.y);
			this.ssPaintX.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].layout = new Vector4(base.makeup.paintInfo[base.SNo].layout.x, base.makeup.paintInfo[base.SNo].layout.y, value, base.makeup.paintInfo[base.SNo].layout.w);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(0 == base.SNo, 1 == base.SNo, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintX.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].layout.z);
			this.ssPaintY.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].layout = new Vector4(base.makeup.paintInfo[base.SNo].layout.x, base.makeup.paintInfo[base.SNo].layout.y, base.makeup.paintInfo[base.SNo].layout.z, value);
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(0 == base.SNo, 1 == base.SNo, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintY.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].layout.w);
			this.ssPaintRot.onChange = delegate(float value)
			{
				base.makeup.paintInfo[base.SNo].rotation = value;
				base.chaCtrl.AddUpdateCMFaceLayoutFlags(0 == base.SNo, 1 == base.SNo, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssPaintRot.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.paintInfo[base.SNo].rotation);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400447D RID: 17533
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscPaintType;

		// Token: 0x0400447E RID: 17534
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csPaintColor;

		// Token: 0x0400447F RID: 17535
		[SerializeField]
		private CustomSliderSet ssPaintGloss;

		// Token: 0x04004480 RID: 17536
		[SerializeField]
		private CustomSliderSet ssPaintMetallic;

		// Token: 0x04004481 RID: 17537
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomPushScrollController pscPaintLayout;

		// Token: 0x04004482 RID: 17538
		[SerializeField]
		private CustomSliderSet ssPaintW;

		// Token: 0x04004483 RID: 17539
		[SerializeField]
		private CustomSliderSet ssPaintH;

		// Token: 0x04004484 RID: 17540
		[SerializeField]
		private CustomSliderSet ssPaintX;

		// Token: 0x04004485 RID: 17541
		[SerializeField]
		private CustomSliderSet ssPaintY;

		// Token: 0x04004486 RID: 17542
		[SerializeField]
		private CustomSliderSet ssPaintRot;

		// Token: 0x04004487 RID: 17543
		private Dictionary<int, Vector4> dictPaintLayout;
	}
}
