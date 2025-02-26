using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D6 RID: 726
	[Serializable]
	public class SharedVector3Int : SharedVariable<Vector3Int>
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0002ED30 File Offset: 0x0002D130
		public static implicit operator SharedVector3Int(Vector3Int value)
		{
			return new SharedVector3Int
			{
				mValue = value
			};
		}
	}
}
