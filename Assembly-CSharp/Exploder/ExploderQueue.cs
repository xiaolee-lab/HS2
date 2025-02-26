using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A1 RID: 929
	internal class ExploderQueue
	{
		// Token: 0x0600106E RID: 4206 RVA: 0x0005C363 File Offset: 0x0005A763
		public ExploderQueue(Core core)
		{
			this.core = core;
			this.queue = new Queue<ExploderParams>();
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0005C380 File Offset: 0x0005A780
		public void Enqueue(ExploderObject exploderObject, ExploderObject.OnExplosion callback, bool crack, params GameObject[] target)
		{
			ExploderParams item = new ExploderParams(exploderObject)
			{
				Callback = callback,
				Targets = target,
				Crack = crack,
				processing = false
			};
			this.queue.Enqueue(item);
			this.ProcessQueue();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0005C3C8 File Offset: 0x0005A7C8
		private void ProcessQueue()
		{
			if (this.queue.Count > 0)
			{
				ExploderParams exploderParams = this.queue.Peek();
				if (!exploderParams.processing)
				{
					exploderParams.id = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
					exploderParams.processing = true;
					this.core.StartExplosionFromQueue(exploderParams);
				}
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0005C428 File Offset: 0x0005A828
		public void OnExplosionFinished(int id, long ellapsedMS)
		{
			ExploderParams exploderParams = this.queue.Dequeue();
			if (exploderParams.Callback != null)
			{
				exploderParams.Callback((float)ellapsedMS, (!exploderParams.Crack) ? ExploderObject.ExplosionState.ExplosionFinished : ExploderObject.ExplosionState.ObjectCracked);
			}
			this.ProcessQueue();
		}

		// Token: 0x04001243 RID: 4675
		private readonly Queue<ExploderParams> queue;

		// Token: 0x04001244 RID: 4676
		private readonly Core core;
	}
}
