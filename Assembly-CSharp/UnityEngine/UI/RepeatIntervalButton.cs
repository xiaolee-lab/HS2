using System;

namespace UnityEngine.UI
{
	// Token: 0x0200115F RID: 4447
	public abstract class RepeatIntervalButton : RepeatButton
	{
		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x060092E3 RID: 37603 RVA: 0x003CE85F File Offset: 0x003CCC5F
		// (set) Token: 0x060092E4 RID: 37604 RVA: 0x003CE867 File Offset: 0x003CCC67
		private protected bool isOn { protected get; private set; }

		// Token: 0x060092E5 RID: 37605 RVA: 0x003CE870 File Offset: 0x003CCC70
		protected override void Process(bool push)
		{
			if (push)
			{
				this.isOn = (this.timer == 0f || this.timer == this.interval);
				this.timer += Time.deltaTime;
				this.timer = Mathf.Min(this.timer, this.interval);
			}
			else
			{
				this.isOn = false;
				this.timer = 0f;
			}
			if (!base.IsSelect)
			{
				this.isOn = false;
			}
		}

		// Token: 0x040076E2 RID: 30434
		[SerializeField]
		private float interval = 0.5f;

		// Token: 0x040076E3 RID: 30435
		private float timer;
	}
}
