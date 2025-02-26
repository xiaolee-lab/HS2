using System;

namespace Battlehub.UIControls
{
	// Token: 0x02000094 RID: 148
	public class ParentChangedEventArgs : EventArgs
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000F3B1 File Offset: 0x0000D7B1
		public ParentChangedEventArgs(TreeViewItem oldParent, TreeViewItem newParent)
		{
			this.OldParent = oldParent;
			this.NewParent = newParent;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000F3C7 File Offset: 0x0000D7C7
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000F3CF File Offset: 0x0000D7CF
		public TreeViewItem OldParent { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000F3D8 File Offset: 0x0000D7D8
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000F3E0 File Offset: 0x0000D7E0
		public TreeViewItem NewParent { get; private set; }
	}
}
