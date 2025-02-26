using System;
using System.IO;

namespace Studio
{
	// Token: 0x02001221 RID: 4641
	public class OIBoneInfo : ObjectInfo
	{
		// Token: 0x060098E6 RID: 39142 RVA: 0x003F0001 File Offset: 0x003EE401
		public OIBoneInfo(int _key) : base(_key)
		{
			this.group = (OIBoneInfo.BoneGroup)0;
			this.level = 0;
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x060098E7 RID: 39143 RVA: 0x003F0018 File Offset: 0x003EE418
		public override int kind
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x060098E8 RID: 39144 RVA: 0x003F001B File Offset: 0x003EE41B
		// (set) Token: 0x060098E9 RID: 39145 RVA: 0x003F0023 File Offset: 0x003EE423
		public OIBoneInfo.BoneGroup group { get; set; }

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x060098EA RID: 39146 RVA: 0x003F002C File Offset: 0x003EE42C
		// (set) Token: 0x060098EB RID: 39147 RVA: 0x003F0034 File Offset: 0x003EE434
		public int level { get; set; }

		// Token: 0x060098EC RID: 39148 RVA: 0x003F003D File Offset: 0x003EE43D
		public override void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(base.dicKey);
			base.changeAmount.Save(_writer);
		}

		// Token: 0x060098ED RID: 39149 RVA: 0x003F0057 File Offset: 0x003EE457
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, false);
		}

		// Token: 0x060098EE RID: 39150 RVA: 0x003F0063 File Offset: 0x003EE463
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
		}

		// Token: 0x02001222 RID: 4642
		public enum BoneGroup
		{
			// Token: 0x040079DF RID: 31199
			Body = 1,
			// Token: 0x040079E0 RID: 31200
			RightLeg,
			// Token: 0x040079E1 RID: 31201
			LeftLeg = 4,
			// Token: 0x040079E2 RID: 31202
			RightArm = 8,
			// Token: 0x040079E3 RID: 31203
			LeftArm = 16,
			// Token: 0x040079E4 RID: 31204
			RightHand = 32,
			// Token: 0x040079E5 RID: 31205
			LeftHand = 64,
			// Token: 0x040079E6 RID: 31206
			Hair = 128,
			// Token: 0x040079E7 RID: 31207
			Neck = 256,
			// Token: 0x040079E8 RID: 31208
			Breast = 512,
			// Token: 0x040079E9 RID: 31209
			Skirt = 1024
		}
	}
}
