using System;

namespace Battlehub.UIControls
{
	// Token: 0x02000079 RID: 121
	public class Repeater
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00009ABD File Offset: 0x00007EBD
		public Repeater(float t, float initDelay, float firstDelay, float delay, Action callback)
		{
			this.m_nextT = t + initDelay;
			this.m_firstDelay = firstDelay;
			this.m_delay = delay;
			this.m_callback = callback;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009AE8 File Offset: 0x00007EE8
		public void Repeat(float t)
		{
			if (t >= this.m_nextT)
			{
				this.m_callback();
				if (this.m_firstDelay > 0f)
				{
					this.m_nextT += this.m_firstDelay;
					this.m_firstDelay = 0f;
				}
				else
				{
					this.m_nextT += this.m_delay;
				}
			}
		}

		// Token: 0x040001F8 RID: 504
		private float m_firstDelay;

		// Token: 0x040001F9 RID: 505
		private float m_delay;

		// Token: 0x040001FA RID: 506
		private float m_nextT;

		// Token: 0x040001FB RID: 507
		private Action m_callback;
	}
}
