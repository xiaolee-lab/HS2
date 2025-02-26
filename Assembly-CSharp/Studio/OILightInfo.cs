using System;
using System.IO;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200122D RID: 4653
	public class OILightInfo : ObjectInfo
	{
		// Token: 0x06009938 RID: 39224 RVA: 0x003F17C0 File Offset: 0x003EFBC0
		public OILightInfo(int _no, int _key) : base(_key)
		{
			this.no = _no;
			this.color = Color.white;
			this.intensity = 1f;
			this.range = 10f;
			this.spotAngle = 30f;
			this.shadow = true;
			this.enable = true;
			this.drawTarget = true;
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x06009939 RID: 39225 RVA: 0x003F181C File Offset: 0x003EFC1C
		public override int kind
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x0600993A RID: 39226 RVA: 0x003F181F File Offset: 0x003EFC1F
		// (set) Token: 0x0600993B RID: 39227 RVA: 0x003F1827 File Offset: 0x003EFC27
		public int no { get; private set; }

		// Token: 0x0600993C RID: 39228 RVA: 0x003F1830 File Offset: 0x003EFC30
		public override void Save(BinaryWriter _writer, Version _version)
		{
			base.Save(_writer, _version);
			_writer.Write(this.no);
			Utility.SaveColor(_writer, this.color);
			_writer.Write(this.intensity);
			_writer.Write(this.range);
			_writer.Write(this.spotAngle);
			_writer.Write(this.shadow);
			_writer.Write(this.enable);
			_writer.Write(this.drawTarget);
		}

		// Token: 0x0600993D RID: 39229 RVA: 0x003F18A8 File Offset: 0x003EFCA8
		public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
		{
			base.Load(_reader, _version, _import, true);
			this.no = _reader.ReadInt32();
			this.color = Utility.LoadColor(_reader);
			this.intensity = _reader.ReadSingle();
			this.range = _reader.ReadSingle();
			this.spotAngle = _reader.ReadSingle();
			this.shadow = _reader.ReadBoolean();
			this.enable = _reader.ReadBoolean();
			this.drawTarget = _reader.ReadBoolean();
		}

		// Token: 0x0600993E RID: 39230 RVA: 0x003F191F File Offset: 0x003EFD1F
		public override void DeleteKey()
		{
			Studio.DeleteIndex(base.dicKey);
		}

		// Token: 0x04007A40 RID: 31296
		public Color color;

		// Token: 0x04007A41 RID: 31297
		public float intensity;

		// Token: 0x04007A42 RID: 31298
		public float range;

		// Token: 0x04007A43 RID: 31299
		public float spotAngle;

		// Token: 0x04007A44 RID: 31300
		public bool shadow;

		// Token: 0x04007A45 RID: 31301
		public bool enable;

		// Token: 0x04007A46 RID: 31302
		public bool drawTarget;
	}
}
