using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003C5 RID: 965
	[Serializable]
	public class FragmentSFX
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x00064BEC File Offset: 0x00062FEC
		public FragmentSFX Clone()
		{
			return new FragmentSFX
			{
				FragmentEmitter = this.FragmentEmitter,
				ChanceToPlay = this.ChanceToPlay,
				PlayOnlyOnce = this.PlayOnlyOnce,
				MixMultipleSounds = this.MixMultipleSounds,
				EmitersMax = this.EmitersMax,
				ParticleTimeout = this.ParticleTimeout
			};
		}

		// Token: 0x04001311 RID: 4881
		public GameObject FragmentEmitter;

		// Token: 0x04001312 RID: 4882
		public int ChanceToPlay = 100;

		// Token: 0x04001313 RID: 4883
		public bool PlayOnlyOnce;

		// Token: 0x04001314 RID: 4884
		public bool MixMultipleSounds;

		// Token: 0x04001315 RID: 4885
		public int EmitersMax;

		// Token: 0x04001316 RID: 4886
		public float ParticleTimeout;
	}
}
