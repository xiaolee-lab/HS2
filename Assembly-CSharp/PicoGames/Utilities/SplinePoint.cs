using System;
using UnityEngine;

namespace PicoGames.Utilities
{
	// Token: 0x02000A74 RID: 2676
	[Serializable]
	public class SplinePoint
	{
		// Token: 0x06004F48 RID: 20296 RVA: 0x001E73F1 File Offset: 0x001E57F1
		public SplinePoint(Vector3 _position, Quaternion _rotation)
		{
			this.position = _position;
			this.rotation = _rotation;
		}

		// Token: 0x04004858 RID: 18520
		[SerializeField]
		public Vector3 position;

		// Token: 0x04004859 RID: 18521
		[SerializeField]
		public Quaternion rotation;
	}
}
