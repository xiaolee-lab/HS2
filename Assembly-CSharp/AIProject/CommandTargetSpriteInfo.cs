using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F5F RID: 3935
	[Serializable]
	public class CommandTargetSpriteInfo
	{
		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x060082AE RID: 33454 RVA: 0x003701A0 File Offset: 0x0036E5A0
		public float FPS
		{
			[CompilerGenerated]
			get
			{
				return this._fps;
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x060082AF RID: 33455 RVA: 0x003701A8 File Offset: 0x0036E5A8
		public Sprite[] Sprites
		{
			[CompilerGenerated]
			get
			{
				return this._sprites;
			}
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x060082B0 RID: 33456 RVA: 0x003701B0 File Offset: 0x0036E5B0
		public Sprite[] SelectedSprites
		{
			[CompilerGenerated]
			get
			{
				return this._selectedSprites;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x060082B1 RID: 33457 RVA: 0x003701B8 File Offset: 0x0036E5B8
		public Sprite[] DisableSprites
		{
			[CompilerGenerated]
			get
			{
				return this._disableSprites;
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x060082B2 RID: 33458 RVA: 0x003701C0 File Offset: 0x0036E5C0
		public Sprite[] CoolTimeSprites
		{
			[CompilerGenerated]
			get
			{
				return this._coolTimeSprites;
			}
		}

		// Token: 0x04006922 RID: 26914
		[SerializeField]
		private float _fps = 1f;

		// Token: 0x04006923 RID: 26915
		[SerializeField]
		private Sprite[] _sprites;

		// Token: 0x04006924 RID: 26916
		[SerializeField]
		private Sprite[] _selectedSprites;

		// Token: 0x04006925 RID: 26917
		[SerializeField]
		private Sprite[] _disableSprites;

		// Token: 0x04006926 RID: 26918
		[SerializeField]
		private Sprite[] _coolTimeSprites;
	}
}
