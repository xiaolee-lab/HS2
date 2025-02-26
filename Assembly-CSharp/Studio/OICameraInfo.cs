using System;
using System.IO;

namespace Studio
{
	// Token: 0x02001220 RID: 4640
	public class OICameraInfo : ObjectInfo
	{
		// Token: 0x060098E1 RID: 39137 RVA: 0x003EFF70 File Offset: 0x003EE370
		public OICameraInfo(int _key) : base(_key)
		{
			this.name = string.Format("カメラ{0}", Singleton<Studio>.Instance.cameraCount);
			this.active = false;
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x060098E2 RID: 39138 RVA: 0x003EFFAA File Offset: 0x003EE3AA
		public override int kind
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060098E3 RID: 39139 RVA: 0x003EFFAD File Offset: 0x003EE3AD
		public override void Save(BinaryWriter _writer, Version _version)
		{
			base.Save(_writer, _version);
			_writer.Write(this.name);
			_writer.Write(this.active);
		}

		// Token: 0x060098E4 RID: 39140 RVA: 0x003EFFCF File Offset: 0x003EE3CF
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, true);
			this.name = _reader.ReadString();
			this.active = _reader.ReadBoolean();
		}

		// Token: 0x060098E5 RID: 39141 RVA: 0x003EFFF3 File Offset: 0x003EE3F3
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
		}

		// Token: 0x040079DA RID: 31194
		public string name = string.Empty;

		// Token: 0x040079DB RID: 31195
		public bool active;
	}
}
