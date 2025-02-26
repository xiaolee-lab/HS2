using System;

namespace Exploder
{
	// Token: 0x020003C2 RID: 962
	[Serializable]
	public class FragmentDeactivation
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x000642C4 File Offset: 0x000626C4
		public FragmentDeactivation Clone()
		{
			return new FragmentDeactivation
			{
				DeactivateOptions = this.DeactivateOptions,
				DeactivateTimeout = this.DeactivateTimeout,
				FadeoutOptions = this.FadeoutOptions
			};
		}

		// Token: 0x040012F7 RID: 4855
		public DeactivateOptions DeactivateOptions;

		// Token: 0x040012F8 RID: 4856
		public float DeactivateTimeout = 10f;

		// Token: 0x040012F9 RID: 4857
		public FadeoutOptions FadeoutOptions;
	}
}
