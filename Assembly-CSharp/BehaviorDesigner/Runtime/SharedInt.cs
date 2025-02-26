using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002CA RID: 714
	[Serializable]
	public class SharedInt : SharedVariable<int>
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0002EB80 File Offset: 0x0002CF80
		public static implicit operator SharedInt(int value)
		{
			return new SharedInt
			{
				mValue = value
			};
		}
	}
}
