using System;

namespace Battlehub.UIControls
{
	// Token: 0x020000AA RID: 170
	public class VirtualizingParentChangedEventArgs : EventArgs
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00016937 File Offset: 0x00014D37
		public VirtualizingParentChangedEventArgs(TreeViewItemContainerData oldParent, TreeViewItemContainerData newParent)
		{
			this.OldParent = oldParent;
			this.NewParent = newParent;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001694D File Offset: 0x00014D4D
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00016955 File Offset: 0x00014D55
		public TreeViewItemContainerData OldParent { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001695E File Offset: 0x00014D5E
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00016966 File Offset: 0x00014D66
		public TreeViewItemContainerData NewParent { get; private set; }
	}
}
