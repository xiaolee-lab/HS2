using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002CF RID: 719
	[Serializable]
	public class SharedRect : SharedVariable<Rect>
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0002EC34 File Offset: 0x0002D034
		public static implicit operator SharedRect(Rect value)
		{
			return new SharedRect
			{
				mValue = value
			};
		}
	}
}
