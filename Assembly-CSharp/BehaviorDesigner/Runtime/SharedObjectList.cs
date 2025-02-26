using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002CD RID: 717
	[Serializable]
	public class SharedObjectList : SharedVariable<List<UnityEngine.Object>>
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x0002EBEC File Offset: 0x0002CFEC
		public static implicit operator SharedObjectList(List<UnityEngine.Object> value)
		{
			return new SharedObjectList
			{
				mValue = value
			};
		}
	}
}
