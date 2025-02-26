using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007B4 RID: 1972
	public class BustSoft
	{
		// Token: 0x06003049 RID: 12361 RVA: 0x0012048C File Offset: 0x0011E88C
		public BustSoft(ChaControl _ctrl)
		{
			this.chaCtrl = _ctrl;
			this.info = this.chaCtrl;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x001204F7 File Offset: 0x0011E8F7
		public void Change(float soft, params int[] changePtn)
		{
			if (null == this.chaCtrl)
			{
				return;
			}
			if (null == this.info)
			{
				return;
			}
			this.info.fileBody.bustSoftness = soft;
			this.ReCalc(changePtn);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x00120538 File Offset: 0x0011E938
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
			float num = this.info.fileBody.bustSoftness * this.info.fileBody.shapeValueBody[1] + 0.01f;
			num = Mathf.Clamp(num, 0f, 1f);
			float stiffness = this.TreeLerp(this.bustStiffness, num);
			float elasticity = this.TreeLerp(this.bustElasticity, num);
			float damping = this.TreeLerp(this.bustDamping, num);
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
						dynamicBone_Ver.setSoftParams(ptn, -1, damping, elasticity, stiffness, true);
					}
				}
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x00120660 File Offset: 0x0011EA60
		private float TreeLerp(float[] vals, float rate)
		{
			if (rate < 0.5f)
			{
				return Mathf.Lerp(vals[0], vals[1], rate * 2f);
			}
			return Mathf.Lerp(vals[1], vals[2], (rate - 0.5f) * 2f);
		}

		// Token: 0x04002DFC RID: 11772
		private ChaControl chaCtrl;

		// Token: 0x04002DFD RID: 11773
		private ChaInfo info;

		// Token: 0x04002DFE RID: 11774
		private float[] bustDamping = new float[]
		{
			0.2f,
			0.1f,
			0.1f
		};

		// Token: 0x04002DFF RID: 11775
		private float[] bustElasticity = new float[]
		{
			0.2f,
			0.15f,
			0.05f
		};

		// Token: 0x04002E00 RID: 11776
		private float[] bustStiffness = new float[]
		{
			1f,
			0.1f,
			0.01f
		};
	}
}
