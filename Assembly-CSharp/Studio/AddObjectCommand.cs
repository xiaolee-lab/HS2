using System;

namespace Studio
{
	// Token: 0x020011F3 RID: 4595
	public static class AddObjectCommand
	{
		// Token: 0x020011F4 RID: 4596
		public class AddItemCommand : ICommand
		{
			// Token: 0x060096F0 RID: 38640 RVA: 0x003E7978 File Offset: 0x003E5D78
			public AddItemCommand(int _group, int _category, int _no, int _dicKey, int _initialPosition)
			{
				this.group = _group;
				this.category = _category;
				this.no = _no;
				this.dicKey = _dicKey;
				this.initialPosition = _initialPosition;
			}

			// Token: 0x060096F1 RID: 38641 RVA: 0x003E79CC File Offset: 0x003E5DCC
			public void Do()
			{
				OCIItem ociitem = AddObjectItem.Load(new OIItemInfo(this.group, this.category, this.no, this.dicKey), null, null, true, this.initialPosition);
				this.tno = ((ociitem == null) ? null : ociitem.treeNodeObject);
			}

			// Token: 0x060096F2 RID: 38642 RVA: 0x003E7A1D File Offset: 0x003E5E1D
			public void Undo()
			{
				Studio.DeleteNode(this.tno);
				this.tno = null;
			}

			// Token: 0x060096F3 RID: 38643 RVA: 0x003E7A31 File Offset: 0x003E5E31
			public void Redo()
			{
				this.Do();
				Studio.SetNewIndex(this.dicKey);
			}

			// Token: 0x0400792F RID: 31023
			private int group = -1;

			// Token: 0x04007930 RID: 31024
			private int category = -1;

			// Token: 0x04007931 RID: 31025
			private int no = -1;

			// Token: 0x04007932 RID: 31026
			private int dicKey = -1;

			// Token: 0x04007933 RID: 31027
			private int initialPosition;

			// Token: 0x04007934 RID: 31028
			private TreeNodeObject tno;
		}

		// Token: 0x020011F5 RID: 4597
		public class AddLightCommand : ICommand
		{
			// Token: 0x060096F4 RID: 38644 RVA: 0x003E7A45 File Offset: 0x003E5E45
			public AddLightCommand(int _no, int _dicKey, int _initialPosition)
			{
				this.no = _no;
				this.dicKey = _dicKey;
				this.initialPosition = _initialPosition;
			}

			// Token: 0x060096F5 RID: 38645 RVA: 0x003E7A70 File Offset: 0x003E5E70
			public void Do()
			{
				OCILight ocilight = AddObjectLight.Load(new OILightInfo(this.no, this.dicKey), null, null, true, this.initialPosition);
				this.tno = ((ocilight == null) ? null : ocilight.treeNodeObject);
			}

			// Token: 0x060096F6 RID: 38646 RVA: 0x003E7AB5 File Offset: 0x003E5EB5
			public void Undo()
			{
				Studio.DeleteNode(this.tno);
				this.tno = null;
			}

			// Token: 0x060096F7 RID: 38647 RVA: 0x003E7AC9 File Offset: 0x003E5EC9
			public void Redo()
			{
				this.Do();
				Studio.SetNewIndex(this.dicKey);
			}

			// Token: 0x04007935 RID: 31029
			private int no = -1;

			// Token: 0x04007936 RID: 31030
			private int dicKey = -1;

			// Token: 0x04007937 RID: 31031
			private int initialPosition;

			// Token: 0x04007938 RID: 31032
			private TreeNodeObject tno;
		}
	}
}
