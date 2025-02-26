using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002C9 RID: 713
	[Serializable]
	public class SharedGameObjectList : SharedVariable<List<GameObject>>
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0002EB5C File Offset: 0x0002CF5C
		public static implicit operator SharedGameObjectList(List<GameObject> value)
		{
			return new SharedGameObjectList
			{
				mValue = value
			};
		}
	}
}
