using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002C5 RID: 709
	[Serializable]
	public class SharedBool : SharedVariable<bool>
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x0002EACC File Offset: 0x0002CECC
		public static implicit operator SharedBool(bool value)
		{
			return new SharedBool
			{
				mValue = value
			};
		}
	}
}
