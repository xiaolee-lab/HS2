using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F5D RID: 3933
	[Serializable]
	public class ItemIconIDDefine
	{
		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x060082A9 RID: 33449 RVA: 0x0037016D File Offset: 0x0036E56D
		public int Umbrella
		{
			[CompilerGenerated]
			get
			{
				return this._umbrella;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x060082AA RID: 33450 RVA: 0x00370175 File Offset: 0x0036E575
		public int Torch
		{
			[CompilerGenerated]
			get
			{
				return this._torch;
			}
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x060082AB RID: 33451 RVA: 0x0037017D File Offset: 0x0036E57D
		public int Lamp
		{
			[CompilerGenerated]
			get
			{
				return this._lamp;
			}
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x060082AC RID: 33452 RVA: 0x00370185 File Offset: 0x0036E585
		public int Flashlight
		{
			[CompilerGenerated]
			get
			{
				return this._flashlight;
			}
		}

		// Token: 0x0400691C RID: 26908
		[SerializeField]
		private int _umbrella = -1;

		// Token: 0x0400691D RID: 26909
		[SerializeField]
		private int _torch = -1;

		// Token: 0x0400691E RID: 26910
		[SerializeField]
		private int _lamp = -1;

		// Token: 0x0400691F RID: 26911
		[SerializeField]
		private int _flashlight = -1;
	}
}
