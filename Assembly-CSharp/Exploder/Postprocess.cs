using System;

namespace Exploder
{
	// Token: 0x020003B3 RID: 947
	internal abstract class Postprocess : ExploderTask
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x00061C0C File Offset: 0x0006000C
		protected Postprocess(Core Core) : base(Core)
		{
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00061C15 File Offset: 0x00060015
		public override void Init()
		{
			base.Init();
			this.core.poolIdx = 0;
			if (this.core.audioSource)
			{
				this.core.audioSource.Play();
			}
		}
	}
}
