using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000FB RID: 251
	[TaskIcon("{SkinColor}EntryIcon.png")]
	public class EntryTask : ParentTask
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x0001FB80 File Offset: 0x0001DF80
		public override int MaxChildren()
		{
			return 1;
		}
	}
}
