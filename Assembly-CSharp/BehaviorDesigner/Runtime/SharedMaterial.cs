using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002CB RID: 715
	[Serializable]
	public class SharedMaterial : SharedVariable<Material>
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0002EBA4 File Offset: 0x0002CFA4
		public static implicit operator SharedMaterial(Material value)
		{
			return new SharedMaterial
			{
				mValue = value
			};
		}
	}
}
