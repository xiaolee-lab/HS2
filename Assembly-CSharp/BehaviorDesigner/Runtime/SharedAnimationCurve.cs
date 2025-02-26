using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002C4 RID: 708
	[Serializable]
	public class SharedAnimationCurve : SharedVariable<AnimationCurve>
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x0002EAA8 File Offset: 0x0002CEA8
		public static implicit operator SharedAnimationCurve(AnimationCurve value)
		{
			return new SharedAnimationCurve
			{
				mValue = value
			};
		}
	}
}
