using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007B5 RID: 1973
	public class BustGravity
	{
		// Token: 0x0600304D RID: 12365 RVA: 0x00120698 File Offset: 0x0011EA98
		public BustGravity(ChaControl _ctrl)
		{
			this.chaCtrl = _ctrl;
			this.info = this.chaCtrl;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x001206C7 File Offset: 0x0011EAC7
		public void Change(float gravity, params int[] changePtn)
		{
			if (null == this.chaCtrl)
			{
				return;
			}
			if (null == this.info)
			{
				return;
			}
			this.info.fileBody.bustWeight = gravity;
			this.ReCalc(changePtn);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x00120708 File Offset: 0x0011EB08
		public void ReCalc(params int[] changePtn)
		{
			if (null == this.chaCtrl)
			{
				return;
			}
			if (null == this.info)
			{
				return;
			}
			if (changePtn.Length == 0)
			{
				return;
			}
			float num = this.info.fileBody.shapeValueBody[1] * this.info.fileBody.bustSoftness * 0.5f;
			float y = Mathf.Lerp(this.range[0], this.range[1], this.info.fileBody.bustWeight) * num;
			DynamicBone_Ver02[] array = new DynamicBone_Ver02[]
			{
				this.chaCtrl.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastL),
				this.chaCtrl.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastR)
			};
			foreach (int ptn in changePtn)
			{
				foreach (DynamicBone_Ver02 dynamicBone_Ver in array)
				{
					if (dynamicBone_Ver != null)
					{
						dynamicBone_Ver.setGravity(ptn, new Vector3(0f, y, 0f), true);
					}
				}
			}
		}

		// Token: 0x04002E01 RID: 11777
		private ChaControl chaCtrl;

		// Token: 0x04002E02 RID: 11778
		private ChaInfo info;

		// Token: 0x04002E03 RID: 11779
		private float[] range = new float[]
		{
			0f,
			-0.05f
		};
	}
}
