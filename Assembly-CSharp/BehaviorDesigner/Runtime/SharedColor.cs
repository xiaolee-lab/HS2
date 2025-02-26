using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002C6 RID: 710
	[Serializable]
	public class SharedColor : SharedVariable<Color>
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0002EAF0 File Offset: 0x0002CEF0
		public static implicit operator SharedColor(Color value)
		{
			return new SharedColor
			{
				mValue = value
			};
		}
	}
}
