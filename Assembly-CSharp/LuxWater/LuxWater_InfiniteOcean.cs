using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003D9 RID: 985
	public class LuxWater_InfiniteOcean : MonoBehaviour
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x00066CAE File Offset: 0x000650AE
		private void OnEnable()
		{
			this.trans = base.GetComponent<Transform>();
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00066CBC File Offset: 0x000650BC
		private void LateUpdate()
		{
			if (this.MainCam == null)
			{
				Camera main = Camera.main;
				if (main == null)
				{
					return;
				}
				this.MainCam = main;
			}
			if (this.camTrans == null)
			{
				this.camTrans = this.MainCam.transform;
			}
			Vector3 position = this.camTrans.position;
			Vector3 position2 = this.trans.position;
			Vector3 lossyScale = this.trans.lossyScale;
			Vector2 vector = new Vector2(this.GridSize * lossyScale.x, this.GridSize * lossyScale.z);
			float num = (float)Math.Round((double)(position.x / vector.x));
			float num2 = vector.x * num;
			num = (float)Math.Round((double)(position.z / vector.y));
			float num3 = vector.y * num;
			position2.x = num2 + position2.x % vector.x;
			position2.z = num3 + position2.z % vector.y;
			this.trans.position = position2;
		}

		// Token: 0x0400134C RID: 4940
		[Space(6f)]
		[LuxWater_HelpBtn("h.c1utuz9up55r")]
		public Camera MainCam;

		// Token: 0x0400134D RID: 4941
		public float GridSize = 10f;

		// Token: 0x0400134E RID: 4942
		private Transform trans;

		// Token: 0x0400134F RID: 4943
		private Transform camTrans;
	}
}
