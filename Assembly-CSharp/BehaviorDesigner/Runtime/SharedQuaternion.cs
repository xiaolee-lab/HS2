using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002CE RID: 718
	[Serializable]
	public class SharedQuaternion : SharedVariable<Quaternion>
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x0002EC10 File Offset: 0x0002D010
		public static implicit operator SharedQuaternion(Quaternion value)
		{
			return new SharedQuaternion
			{
				mValue = value
			};
		}
	}
}
