using System;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A6C RID: 2668
	[Serializable]
	public struct TransformSettings
	{
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x001E516F File Offset: 0x001E356F
		// (set) Token: 0x06004F05 RID: 20229 RVA: 0x001E517C File Offset: 0x001E357C
		[SerializeField]
		public Quaternion rotation
		{
			get
			{
				return Quaternion.Euler(this.eulerRotation);
			}
			set
			{
				this.eulerRotation = value.eulerAngles;
			}
		}

		// Token: 0x04004811 RID: 18449
		[SerializeField]
		public Vector3 position;

		// Token: 0x04004812 RID: 18450
		[SerializeField]
		public Vector3 eulerRotation;

		// Token: 0x04004813 RID: 18451
		[SerializeField]
		public Vector3 scale;
	}
}
