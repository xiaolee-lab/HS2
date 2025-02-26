using System;

namespace Studio
{
	// Token: 0x0200120C RID: 4620
	public abstract class ObjectCtrlInfo
	{
		// Token: 0x0600973E RID: 38718
		public abstract void OnDelete();

		// Token: 0x0600973F RID: 38719
		public abstract void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child);

		// Token: 0x06009740 RID: 38720
		public abstract void OnDetach();

		// Token: 0x06009741 RID: 38721
		public abstract void OnDetachChild(ObjectCtrlInfo _child);

		// Token: 0x06009742 RID: 38722
		public abstract void OnSelect(bool _select);

		// Token: 0x06009743 RID: 38723
		public abstract void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child);

		// Token: 0x06009744 RID: 38724
		public abstract void OnVisible(bool _visible);

		// Token: 0x06009745 RID: 38725 RVA: 0x003E8A0C File Offset: 0x003E6E0C
		public virtual void OnSavePreprocessing()
		{
			if (this.objectInfo != null && this.treeNodeObject != null)
			{
				this.objectInfo.treeState = this.treeNodeObject.treeState;
			}
			if (this.objectInfo != null && this.treeNodeObject != null)
			{
				this.objectInfo.visible = this.treeNodeObject.visible;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06009746 RID: 38726 RVA: 0x003E8A7D File Offset: 0x003E6E7D
		public int kind
		{
			get
			{
				return (this.objectInfo == null) ? -1 : this.objectInfo.kind;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06009747 RID: 38727 RVA: 0x003E8A9B File Offset: 0x003E6E9B
		public int[] kinds
		{
			get
			{
				int[] result;
				if (this.objectInfo != null)
				{
					result = this.objectInfo.kinds;
				}
				else
				{
					(result = new int[1])[0] = -1;
				}
				return result;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x06009748 RID: 38728 RVA: 0x003E8AC2 File Offset: 0x003E6EC2
		// (set) Token: 0x06009749 RID: 38729 RVA: 0x003E8ACA File Offset: 0x003E6ECA
		public virtual float animeSpeed { get; set; }

		// Token: 0x17002003 RID: 8195
		public virtual ObjectCtrlInfo this[int _idx]
		{
			get
			{
				return (_idx != 0) ? this.parentInfo : this;
			}
		}

		// Token: 0x04007964 RID: 31076
		public ObjectInfo objectInfo;

		// Token: 0x04007965 RID: 31077
		public TreeNodeObject treeNodeObject;

		// Token: 0x04007966 RID: 31078
		public GuideObject guideObject;

		// Token: 0x04007967 RID: 31079
		public ObjectCtrlInfo parentInfo;
	}
}
