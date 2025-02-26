using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D7 RID: 727
	[Serializable]
	public class SharedVector4 : SharedVariable<Vector4>
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x0002ED54 File Offset: 0x0002D154
		public static implicit operator SharedVector4(Vector4 value)
		{
			return new SharedVector4
			{
				mValue = value
			};
		}
	}
}
