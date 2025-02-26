using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002CC RID: 716
	[Serializable]
	public class SharedObject : SharedVariable<UnityEngine.Object>
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x0002EBC8 File Offset: 0x0002CFC8
		public static explicit operator SharedObject(UnityEngine.Object value)
		{
			return new SharedObject
			{
				mValue = value
			};
		}
	}
}
