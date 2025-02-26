using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x02000987 RID: 2439
	[MessagePackObject(false)]
	public class SaveData
	{
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x001ACE7B File Offset: 0x001AB27B
		// (set) Token: 0x060045DF RID: 17887 RVA: 0x001ACE83 File Offset: 0x001AB283
		[Key(0)]
		public WorldData AutoData { get; set; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x001ACE8C File Offset: 0x001AB28C
		// (set) Token: 0x060045E1 RID: 17889 RVA: 0x001ACE94 File Offset: 0x001AB294
		[Key(1)]
		public Dictionary<int, WorldData> WorldList { get; set; } = new Dictionary<int, WorldData>();

		// Token: 0x060045E2 RID: 17890 RVA: 0x001ACEA0 File Offset: 0x001AB2A0
		public void SaveFile(byte[] buffer)
		{
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				this.SaveFile(memoryStream);
			}
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x001ACEE0 File Offset: 0x001AB2E0
		public void SaveFile(string path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				this.SaveFile(fileStream);
			}
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x001ACF20 File Offset: 0x001AB320
		public void SaveFile(Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				this.SaveFile(binaryWriter);
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x001ACF60 File Offset: 0x001AB360
		public void SaveFile(BinaryWriter writer)
		{
			byte[] buffer = MessagePackSerializer.Serialize<SaveData>(this);
			writer.Write(buffer);
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x001ACF7C File Offset: 0x001AB37C
		public static SaveData LoadFile(string fileName)
		{
			SaveData saveData = new SaveData();
			if (saveData.Load(fileName))
			{
				saveData.ComplementDiff();
				return saveData;
			}
			return null;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x001ACFA4 File Offset: 0x001AB3A4
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

		// Token: 0x060045E8 RID: 17896 RVA: 0x001AD020 File Offset: 0x001AB420
		public bool Load(Stream stream)
		{
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				result = this.Load(binaryReader);
			}
			return result;
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x001AD060 File Offset: 0x001AB460
		public bool Load(BinaryReader reader)
		{
			try
			{
				byte[] array = reader.ReadBytes((int)reader.BaseStream.Length);
				if (array.IsNullOrEmpty<byte>())
				{
					return false;
				}
				SaveData source = MessagePackSerializer.Deserialize<SaveData>(array);
				this.Copy(source);
				return true;
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x001AD0C0 File Offset: 0x001AB4C0
		public void Copy(SaveData source)
		{
			if (source.AutoData != null)
			{
				this.AutoData = new WorldData();
				this.AutoData.Copy(source.AutoData);
			}
			foreach (KeyValuePair<int, WorldData> keyValuePair in source.WorldList)
			{
				WorldData worldData = new WorldData();
				worldData.Copy(keyValuePair.Value);
				this.WorldList[keyValuePair.Key] = worldData;
			}
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x001AD164 File Offset: 0x001AB564
		public void ComplementDiff()
		{
			WorldData autoData = this.AutoData;
			if (autoData != null)
			{
				autoData.ComplementDiff();
			}
			foreach (KeyValuePair<int, WorldData> keyValuePair in this.WorldList)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.ComplementDiff();
				}
			}
		}
	}
}
