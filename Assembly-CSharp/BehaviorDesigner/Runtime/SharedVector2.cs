using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D3 RID: 723
	[Serializable]
	public class SharedVector2 : SharedVariable<Vector2>
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x0002ECC4 File Offset: 0x0002D0C4
		public static implicit operator SharedVector2(Vector2 value)
		{
			return new SharedVector2
			{
				mValue = value
			};
		}
	}
}
