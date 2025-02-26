using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D1 RID: 721
	[Serializable]
	public class SharedTransform : SharedVariable<Transform>
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0002EC7C File Offset: 0x0002D07C
		public static implicit operator SharedTransform(Transform value)
		{
			return new SharedTransform
			{
				mValue = value
			};
		}
	}
}
