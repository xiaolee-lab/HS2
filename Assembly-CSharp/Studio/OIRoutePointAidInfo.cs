using System;
using System.IO;

namespace Studio
{
	// Token: 0x0200122E RID: 4654
	public class OIRoutePointAidInfo : ObjectInfo
	{
		// Token: 0x0600993F RID: 39231 RVA: 0x003F192D File Offset: 0x003EFD2D
		public OIRoutePointAidInfo(int _key) : base(_key)
		{
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x06009940 RID: 39232 RVA: 0x003F1936 File Offset: 0x003EFD36
		public override int kind
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x003F1939 File Offset: 0x003EFD39
		public override void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(base.dicKey);
			base.changeAmount.Save(_writer);
			_writer.Write(this.isInit);
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x003F195F File Offset: 0x003EFD5F
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, false);
			this.isInit = _reader.ReadBoolean();
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x003F1977 File Offset: 0x003EFD77
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
		}

		// Token: 0x04007A47 RID: 31303
		public bool isInit;
	}
}
