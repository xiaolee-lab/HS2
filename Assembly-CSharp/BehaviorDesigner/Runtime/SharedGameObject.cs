using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	public class SharedGameObject : SharedVariable<GameObject>
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x0002EB38 File Offset: 0x0002CF38
		public static implicit operator SharedGameObject(GameObject value)
		{
			return new SharedGameObject
			{
				mValue = value
			};
		}
	}
}
