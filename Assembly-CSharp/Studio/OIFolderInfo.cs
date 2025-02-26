using System;
using System.Collections.Generic;
using System.IO;

namespace Studio
{
	// Token: 0x02001229 RID: 4649
	public class OIFolderInfo : ObjectInfo
	{
		// Token: 0x0600990C RID: 39180 RVA: 0x003F0CFD File Offset: 0x003EF0FD
		public OIFolderInfo(int _key) : base(_key)
		{
			this.name = "フォルダー";
			this.child = new List<ObjectInfo>();
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x0600990D RID: 39181 RVA: 0x003F0D27 File Offset: 0x003EF127
		public override int kind
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x0600990E RID: 39182 RVA: 0x003F0D2A File Offset: 0x003EF12A
		// (set) Token: 0x0600990F RID: 39183 RVA: 0x003F0D32 File Offset: 0x003EF132
		public List<ObjectInfo> child { get; private set; }

		// Token: 0x06009910 RID: 39184 RVA: 0x003F0D3C File Offset: 0x003EF13C
		public override void Save(BinaryWriter _writer, Version _version)
		{
			base.Save(_writer, _version);
			_writer.Write(this.name);
			int count = this.child.Count;
			_writer.Write(count);
			for (int i = 0; i < count; i++)
			{
				this.child[i].Save(_writer, _version);
			}
		}

		// Token: 0x06009911 RID: 39185 RVA: 0x003F0D95 File Offset: 0x003EF195
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, true);
			this.name = _reader.ReadString();
			ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
		}

		// Token: 0x06009912 RID: 39186 RVA: 0x003F0DBC File Offset: 0x003EF1BC
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
			int count = this.child.Count;
			for (int i = 0; i < count; i++)
			{
				this.child[i].DeleteKey();
			}
		}

		// Token: 0x04007A20 RID: 31264
		public string name = string.Empty;
	}
}
