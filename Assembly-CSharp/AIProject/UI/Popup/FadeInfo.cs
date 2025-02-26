using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject.UI.Popup
{
	// Token: 0x02000FD5 RID: 4053
	[Serializable]
	public struct FadeInfo
	{
		// Token: 0x060086DF RID: 34527 RVA: 0x00384B40 File Offset: 0x00382F40
		public FadeInfo(float _alpha, float _scale, float _time)
		{
			this.alpha = _alpha;
			this.scale = _scale;
			this.time = _time;
		}

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x060086E0 RID: 34528 RVA: 0x00384B57 File Offset: 0x00382F57
		public float Alpha
		{
			[CompilerGenerated]
			get
			{
				return this.alpha;
			}
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x060086E1 RID: 34529 RVA: 0x00384B5F File Offset: 0x00382F5F
		public float Scale
		{
			[CompilerGenerated]
			get
			{
				return this.scale;
			}
		}

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x060086E2 RID: 34530 RVA: 0x00384B67 File Offset: 0x00382F67
		public float Time
		{
			[CompilerGenerated]
			get
			{
				return this.time;
			}
		}

		// Token: 0x04006D9D RID: 28061
		[SerializeField]
		private float alpha;

		// Token: 0x04006D9E RID: 28062
		[SerializeField]
		private float scale;

		// Token: 0x04006D9F RID: 28063
		[SerializeField]
		private float time;
	}
}
