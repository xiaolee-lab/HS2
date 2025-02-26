using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public class SharedVector2Int : SharedVariable<Vector2Int>
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0002ECE8 File Offset: 0x0002D0E8
		public static implicit operator SharedVector2Int(Vector2Int value)
		{
			return new SharedVector2Int
			{
				mValue = value
			};
		}
	}
}
