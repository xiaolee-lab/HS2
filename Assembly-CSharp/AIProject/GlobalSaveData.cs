using System;
using System.IO;
using MessagePack;

namespace AIProject
{
	// Token: 0x02000981 RID: 2433
	[MessagePackObject(false)]
	public class GlobalSaveData
	{
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x001AA9E7 File Offset: 0x001A8DE7
		// (set) Token: 0x06004517 RID: 17687 RVA: 0x001AA9EF File Offset: 0x001A8DEF
		[Key(0)]
		public bool Cleared { get; set; }

		// Token: 0x06004518 RID: 17688 RVA: 0x001AA9F8 File Offset: 0x001A8DF8
		public void Copy(GlobalSaveData source)
		{
			this.Cleared = source.Cleared;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x001AAA08 File Offset: 0x001A8E08
		public void SaveFile(byte[] buffer)
		{
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				this.SaveFile(memoryStream);
			}
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x001AAA48 File Offset: 0x001A8E48
		public void SaveFile(string path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				this.SaveFile(fileStream);
			}
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x001AAA88 File Offset: 0x001A8E88
		public void SaveFile(Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				this.SaveFile(binaryWriter);
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x001AAAC8 File Offset: 0x001A8EC8
		public void SaveFile(BinaryWriter writer)
		{
			byte[] buffer = MessagePackSerializer.Serialize<GlobalSaveData>(this);
			writer.Write(buffer);
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x001AAAE4 File Offset: 0x001A8EE4
		public static GlobalSaveData LoadFile(string fileName)
		{
			GlobalSaveData globalSaveData = new GlobalSaveData();
			if (globalSaveData.Load(fileName))
			{
				return globalSaveData;
			}
			return null;
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x001AAB08 File Offset: 0x001A8F08
		public bool Load(string fileName)
		{
			bool result;
			try
			{
				using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (fileStream.Length == 0L)
					{
						result = false;
					}
					else
					{
						result = this.Load(fileStream);
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is FileNotFoundException)
				{
					result = false;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x001AAB84 File Offset: 0x001A8F84
		public bool Load(Stream stream)
		{
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				result = this.Load(binaryReader);
			}
			return result;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001AABC4 File Offset: 0x001A8FC4
		public bool Load(BinaryReader reader)
		{
			try
			{
				byte[] array = reader.ReadBytes((int)reader.BaseStream.Length);
				if (array.IsNullOrEmpty<byte>())
				{
					return false;
				}
				GlobalSaveData source = MessagePackSerializer.Deserialize<GlobalSaveData>(array);
				this.Copy(source);
				return true;
			}
			catch (Exception ex)
			{
			}
			return false;
		}
	}
}
