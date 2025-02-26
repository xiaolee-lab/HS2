using System;
using System.IO;

namespace Studio
{
	// Token: 0x02001224 RID: 4644
	public class LookAtTargetInfo : ObjectInfo
	{
		// Token: 0x060098F0 RID: 39152 RVA: 0x003F007A File Offset: 0x003EE47A
		public LookAtTargetInfo(int _key) : base(_key)
		{
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x060098F1 RID: 39153 RVA: 0x003F0083 File Offset: 0x003EE483
		public override int kind
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060098F2 RID: 39154 RVA: 0x003F0086 File Offset: 0x003EE486
		public override void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(base.dicKey);
			base.changeAmount.Save(_writer);
		}

		// Token: 0x060098F3 RID: 39155 RVA: 0x003F00A0 File Offset: 0x003EE4A0
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, false);
		}

		// Token: 0x060098F4 RID: 39156 RVA: 0x003F00AC File Offset: 0x003EE4AC
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
		}
	}
}
