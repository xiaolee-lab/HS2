using System;

namespace ReMotion
{
	// Token: 0x020004F9 RID: 1273
	public interface ITween
	{
		// Token: 0x060017DC RID: 6108
		bool MoveNext(ref float deltaTime, ref float unscaledDeltaTime);
	}
}
