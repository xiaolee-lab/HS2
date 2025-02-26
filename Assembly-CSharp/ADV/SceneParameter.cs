using System;

namespace ADV
{
	// Token: 0x020006C4 RID: 1732
	public abstract class SceneParameter
	{
		// Token: 0x06002907 RID: 10503 RVA: 0x000F1B38 File Offset: 0x000EFF38
		public SceneParameter(IData data)
		{
			this.data = data;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000F1B47 File Offset: 0x000EFF47
		// (set) Token: 0x06002909 RID: 10505 RVA: 0x000F1B4E File Offset: 0x000EFF4E
		public static ADVScene advScene { get; set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x000F1B56 File Offset: 0x000EFF56
		public IData data { get; }

		// Token: 0x0600290B RID: 10507 RVA: 0x000F1B5E File Offset: 0x000EFF5E
		public virtual void Init()
		{
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000F1B60 File Offset: 0x000EFF60
		public virtual void Release()
		{
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000F1B62 File Offset: 0x000EFF62
		public virtual void WaitEndProc()
		{
		}
	}
}
