using System;
using Manager;

namespace AIProject.UI
{
	// Token: 0x02000FA4 RID: 4004
	[Serializable]
	public abstract class CommandDataBase : ICommandData
	{
		// Token: 0x06008572 RID: 34162
		protected abstract bool IsInput(Input input);

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06008573 RID: 34163 RVA: 0x003757B9 File Offset: 0x00373BB9
		// (set) Token: 0x06008574 RID: 34164 RVA: 0x003757C1 File Offset: 0x00373BC1
		public bool IsActive { get; set; } = true;

		// Token: 0x06008575 RID: 34165 RVA: 0x003757CA File Offset: 0x00373BCA
		public void Invoke(Input input)
		{
			if (this.IsActive)
			{
				this.OnInvoke(input);
			}
		}

		// Token: 0x06008576 RID: 34166
		protected abstract void OnInvoke(Input input);
	}
}
