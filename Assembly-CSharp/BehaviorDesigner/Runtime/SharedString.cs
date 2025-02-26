using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002D0 RID: 720
	[Serializable]
	public class SharedString : SharedVariable<string>
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0002EC58 File Offset: 0x0002D058
		public static implicit operator SharedString(string value)
		{
			return new SharedString
			{
				mValue = value
			};
		}
	}
}
