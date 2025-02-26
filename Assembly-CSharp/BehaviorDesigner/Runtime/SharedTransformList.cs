using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D2 RID: 722
	[Serializable]
	public class SharedTransformList : SharedVariable<List<Transform>>
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x0002ECA0 File Offset: 0x0002D0A0
		public static implicit operator SharedTransformList(List<Transform> value)
		{
			return new SharedTransformList
			{
				mValue = value
			};
		}
	}
}
