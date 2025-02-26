using System;
using System.Diagnostics;

namespace Exploder
{
	// Token: 0x020003B1 RID: 945
	internal abstract class ExploderTask
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x000609FF File Offset: 0x0005EDFF
		protected ExploderTask(Core Core)
		{
			this.core = Core;
			this.Watch = Stopwatch.StartNew();
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060010BC RID: 4284
		public abstract TaskType Type { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00060A19 File Offset: 0x0005EE19
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00060A21 File Offset: 0x0005EE21
		public Stopwatch Watch { get; private set; }

		// Token: 0x060010BF RID: 4287 RVA: 0x00060A2A File Offset: 0x0005EE2A
		public virtual void OnDestroy()
		{
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00060A2C File Offset: 0x0005EE2C
		public virtual void Init()
		{
			this.Watch.Reset();
			this.Watch.Start();
		}

		// Token: 0x060010C1 RID: 4289
		public abstract bool Run(float frameBudget);

		// Token: 0x040012A3 RID: 4771
		protected Core core;
	}
}
