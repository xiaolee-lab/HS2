using System;
using System.IO;
using MessagePack;

namespace AIChara
{
	// Token: 0x020007F9 RID: 2041
	public class ChaFileCustom : ChaFileAssist
	{
		// Token: 0x0600334A RID: 13130 RVA: 0x00131B4F File Offset: 0x0012FF4F
		public ChaFileCustom()
		{
			this.MemberInit();
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x00131B5D File Offset: 0x0012FF5D
		public void MemberInit()
		{
			this.face = new ChaFileFace();
			this.body = new ChaFileBody();
			this.hair = new ChaFileHair();
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x00131B80 File Offset: 0x0012FF80
		public byte[] SaveBytes()
		{
			byte[] array = MessagePackSerializer.Serialize<ChaFileFace>(this.face);
			byte[] array2 = MessagePackSerializer.Serialize<ChaFileBody>(this.body);
			byte[] array3 = MessagePackSerializer.Serialize<ChaFileHair>(this.hair);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					binaryWriter.Write(array2.Length);
					binaryWriter.Write(array2);
					binaryWriter.Write(array3.Length);
					binaryWriter.Write(array3);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x00131C3C File Offset: 0x0013003C
		public bool LoadBytes(byte[] data, Version ver)
		{
			bool result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					int count = binaryReader.ReadInt32();
					byte[] bytes = binaryReader.ReadBytes(count);
					this.face = MessagePackSerializer.Deserialize<ChaFileFace>(bytes);
					count = binaryReader.ReadInt32();
					bytes = binaryReader.ReadBytes(count);
					this.body = MessagePackSerializer.Deserialize<ChaFileBody>(bytes);
					count = binaryReader.ReadInt32();
					bytes = binaryReader.ReadBytes(count);
					this.hair = MessagePackSerializer.Deserialize<ChaFileHair>(bytes);
					this.face.ComplementWithVersion();
					this.body.ComplementWithVersion();
					this.hair.ComplementWithVersion();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x00131D0C File Offset: 0x0013010C
		public void SaveFace(string path)
		{
			base.SaveFileAssist<ChaFileFace>(path, this.face);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x00131D1B File Offset: 0x0013011B
		public void LoadFace(string path)
		{
			base.LoadFileAssist<ChaFileFace>(path, out this.face);
			this.face.ComplementWithVersion();
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x00131D35 File Offset: 0x00130135
		public void LoadFace(byte[] bytes)
		{
			base.LoadFileAssist<ChaFileFace>(bytes, out this.face);
			this.face.ComplementWithVersion();
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x00131D4F File Offset: 0x0013014F
		public void SaveBody(string path)
		{
			base.SaveFileAssist<ChaFileBody>(path, this.body);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x00131D5E File Offset: 0x0013015E
		public void LoadBody(string path)
		{
			base.LoadFileAssist<ChaFileBody>(path, out this.body);
			this.body.ComplementWithVersion();
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x00131D78 File Offset: 0x00130178
		public void SaveHair(string path)
		{
			base.SaveFileAssist<ChaFileHair>(path, this.hair);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x00131D87 File Offset: 0x00130187
		public void LoadHair(string path)
		{
			base.LoadFileAssist<ChaFileHair>(path, out this.hair);
			this.hair.ComplementWithVersion();
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00131DA4 File Offset: 0x001301A4
		public int GetBustSizeKind()
		{
			int result = 1;
			float num = this.body.shapeValueBody[1];
			if (0.33f >= num)
			{
				result = 0;
			}
			else if (0.66f <= num)
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x00131DE4 File Offset: 0x001301E4
		public int GetHeightKind()
		{
			int result = 1;
			float num = this.body.shapeValueBody[0];
			if (0.33f >= num)
			{
				result = 0;
			}
			else if (0.66f <= num)
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x040032A4 RID: 12964
		public static readonly string BlockName = "Custom";

		// Token: 0x040032A5 RID: 12965
		public ChaFileFace face;

		// Token: 0x040032A6 RID: 12966
		public ChaFileBody body;

		// Token: 0x040032A7 RID: 12967
		public ChaFileHair hair;
	}
}
