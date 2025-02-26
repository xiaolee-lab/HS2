using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D5 RID: 725
	[Serializable]
	public class SharedVector3 : SharedVariable<Vector3>
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x0002ED0C File Offset: 0x0002D10C
		public static implicit operator SharedVector3(Vector3 value)
		{
			return new SharedVector3
			{
				mValue = value
			};
		}
	}
}
