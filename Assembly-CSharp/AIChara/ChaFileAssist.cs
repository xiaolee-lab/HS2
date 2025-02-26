using System;
using System.IO;
using MessagePack;

namespace AIChara
{
	// Token: 0x020007E6 RID: 2022
	public class ChaFileAssist
	{
		// Token: 0x06003208 RID: 12808 RVA: 0x0012FA3C File Offset: 0x0012DE3C
		public void SaveFileAssist<T>(string path, T info)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					byte[] buffer = MessagePackSerializer.Serialize<T>(info);
					binaryWriter.Write(buffer);
				}
			}
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x0012FAC0 File Offset: 0x0012DEC0
		public void LoadFileAssist<T>(string path, out T info)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					byte[] bytes = binaryReader.ReadBytes((int)fileStream.Length);
					info = MessagePackSerializer.Deserialize<T>(bytes);
				}
			}
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x0012FB38 File Offset: 0x0012DF38
		public void LoadFileAssist<T>(byte[] bytes, out T info)
		{
			info = MessagePackSerializer.Deserialize<T>(bytes);
		}
	}
}
