using System;
using System.IO;

namespace Studio
{
	// Token: 0x0200121E RID: 4638
	public abstract class ObjectInfo
	{
		// Token: 0x060098D0 RID: 39120 RVA: 0x003EFA04 File Offset: 0x003EDE04
		public ObjectInfo(int _key)
		{
			this.dicKey = _key;
			this.changeAmount = new ChangeAmount();
			this.treeState = TreeNodeObject.TreeState.Close;
			this.visible = true;
			if (this.dicKey != -1)
			{
				Studio.AddChangeAmount(this.dicKey, this.changeAmount);
			}
		}

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x060098D1 RID: 39121 RVA: 0x003EFA54 File Offset: 0x003EDE54
		// (set) Token: 0x060098D2 RID: 39122 RVA: 0x003EFA5C File Offset: 0x003EDE5C
		public int dicKey { get; private set; }

		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x060098D3 RID: 39123
		public abstract int kind { get; }

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x060098D4 RID: 39124 RVA: 0x003EFA65 File Offset: 0x003EDE65
		// (set) Token: 0x060098D5 RID: 39125 RVA: 0x003EFA6D File Offset: 0x003EDE6D
		public ChangeAmount changeAmount { get; protected set; }

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x060098D6 RID: 39126 RVA: 0x003EFA76 File Offset: 0x003EDE76
		// (set) Token: 0x060098D7 RID: 39127 RVA: 0x003EFA7E File Offset: 0x003EDE7E
		public TreeNodeObject.TreeState treeState { get; set; }

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x060098D8 RID: 39128 RVA: 0x003EFA87 File Offset: 0x003EDE87
		// (set) Token: 0x060098D9 RID: 39129 RVA: 0x003EFA8F File Offset: 0x003EDE8F
		public bool visible { get; set; }

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x060098DA RID: 39130 RVA: 0x003EFA98 File Offset: 0x003EDE98
		public virtual int[] kinds
		{
			get
			{
				return new int[]
				{
					this.kind
				};
			}
		}

		// Token: 0x060098DB RID: 39131 RVA: 0x003EFAA9 File Offset: 0x003EDEA9
		public virtual void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(this.kind);
			_writer.Write(this.dicKey);
			this.changeAmount.Save(_writer);
			_writer.Write((int)this.treeState);
			_writer.Write(this.visible);
		}

		// Token: 0x060098DC RID: 39132 RVA: 0x003EFAE8 File Offset: 0x003EDEE8
		public virtual void Load(BinaryReader _reader, Version _version, bool _import, bool _other = true)
		{
			if (!_import)
			{
				this.dicKey = Studio.SetNewIndex(_reader.ReadInt32());
			}
			else
			{
				_reader.ReadInt32();
			}
			this.changeAmount.Load(_reader);
			if (this.dicKey != -1 && !_import)
			{
				Studio.AddChangeAmount(this.dicKey, this.changeAmount);
			}
			if (_other)
			{
				this.treeState = (TreeNodeObject.TreeState)_reader.ReadInt32();
			}
			if (_other)
			{
				this.visible = _reader.ReadBoolean();
			}
		}

		// Token: 0x060098DD RID: 39133
		public abstract void DeleteKey();
	}
}
