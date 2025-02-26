using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009D6 RID: 2518
	public class CvsF_Eyebrow : CvsBase
	{
		// Token: 0x060049C1 RID: 18881 RVA: 0x001C1CBD File Offset: 0x001C00BD
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x001C1CE7 File Offset: 0x001C00E7
		private void CalculateUI()
		{
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x001C1CE9 File Offset: 0x001C00E9
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscEyebrowType.SetToggleID(base.face.eyebrowId);
			this.csEyebrowColor.SetColor(base.face.eyebrowColor);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x001C1D24 File Offset: 0x001C0124
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			yield break;
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x001C1D40 File Offset: 0x001C0140
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsEyebrow += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_eyebrow, ChaListDefine.KeyType.Unknown);
			this.sscEyebrowType.CreateList(lst);
			this.sscEyebrowType.SetToggleID(base.face.eyebrowId);
			this.sscEyebrowType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.eyebrowId != info.id)
				{
					base.face.eyebrowId = info.id;
					base.chaCtrl.ChangeEyebrowKind();
				}
			};
			this.csEyebrowColor.actUpdateColor = delegate(Color color)
			{
				base.face.eyebrowColor = color;
				base.chaCtrl.ChangeEyebrowColor();
			};
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004458 RID: 17496
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscEyebrowType;

		// Token: 0x04004459 RID: 17497
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csEyebrowColor;
	}
}
