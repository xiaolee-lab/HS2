using System;
using System.IO;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200122B RID: 4651
	public class ColorInfo
	{
		// Token: 0x06009926 RID: 39206 RVA: 0x003F1109 File Offset: 0x003EF509
		public virtual void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write(JsonUtility.ToJson(this.mainColor));
			_writer.Write(this.metallic);
			_writer.Write(this.glossiness);
			this.pattern.Save(_writer, _version);
		}

		// Token: 0x06009927 RID: 39207 RVA: 0x003F1146 File Offset: 0x003EF546
		public virtual void Load(BinaryReader _reader, Version _version)
		{
			this.mainColor = JsonUtility.FromJson<Color>(_reader.ReadString());
			this.metallic = _reader.ReadSingle();
			this.glossiness = _reader.ReadSingle();
			this.pattern.Load(_reader, _version);
		}

		// Token: 0x04007A28 RID: 31272
		public Color mainColor = Color.white;

		// Token: 0x04007A29 RID: 31273
		public float metallic;

		// Token: 0x04007A2A RID: 31274
		public float glossiness;

		// Token: 0x04007A2B RID: 31275
		public PatternInfo pattern = new PatternInfo();
	}
}
