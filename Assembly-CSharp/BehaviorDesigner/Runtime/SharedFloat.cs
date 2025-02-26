using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x020002C7 RID: 711
	[Serializable]
	public class SharedFloat : SharedVariable<float>
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x0002EB14 File Offset: 0x0002CF14
		public static implicit operator SharedFloat(float value)
		{
			return new SharedFloat
			{
				Value = value
			};
		}
	}
}
