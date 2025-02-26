using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIProject.Animal;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x0200098B RID: 2443
	[MessagePackObject(false)]
	public class WorldData
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x001AD897 File Offset: 0x001ABC97
		// (set) Token: 0x06004618 RID: 17944 RVA: 0x001AD89F File Offset: 0x001ABC9F
		[Key(0)]
		private Version Version { get; set; } = new Version();

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x001AD8A8 File Offset: 0x001ABCA8
		// (set) Token: 0x0600461A RID: 17946 RVA: 0x001AD8B0 File Offset: 0x001ABCB0
		[Key(1)]
		public int WorldID { get; set; }

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x001AD8B9 File Offset: 0x001ABCB9
		// (set) Token: 0x0600461C RID: 17948 RVA: 0x001AD8C1 File Offset: 0x001ABCC1
		[Key(2)]
		public bool FreeMode { get; set; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x001AD8CA File Offset: 0x001ABCCA
		// (set) Token: 0x0600461E RID: 17950 RVA: 0x001AD8D2 File Offset: 0x001ABCD2
		[IgnoreMember]
		public DateTime SaveTime { get; set; }

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x001AD8DB File Offset: 0x001ABCDB
		// (set) Token: 0x06004620 RID: 17952 RVA: 0x001AD8E3 File Offset: 0x001ABCE3
		[Key(4)]
		public string Name { get; set; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x001AD8EC File Offset: 0x001ABCEC
		// (set) Token: 0x06004622 RID: 17954 RVA: 0x001AD8F4 File Offset: 0x001ABCF4
		[Key(5)]
		public Environment Environment { get; set; } = new Environment();

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06004623 RID: 17955 RVA: 0x001AD8FD File Offset: 0x001ABCFD
		// (set) Token: 0x06004624 RID: 17956 RVA: 0x001AD905 File Offset: 0x001ABD05
		[Key(6)]
		public Dictionary<int, AgentData> AgentTable { get; set; } = new Dictionary<int, AgentData>();

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004625 RID: 17957 RVA: 0x001AD90E File Offset: 0x001ABD0E
		// (set) Token: 0x06004626 RID: 17958 RVA: 0x001AD916 File Offset: 0x001ABD16
		[Key(7)]
		public PlayerData PlayerData { get; set; } = new PlayerData();

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x001AD91F File Offset: 0x001ABD1F
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x001AD927 File Offset: 0x001ABD27
		[Key(8)]
		public MerchantData MerchantData { get; set; } = new MerchantData();

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x001AD930 File Offset: 0x001ABD30
		// (set) Token: 0x0600462A RID: 17962 RVA: 0x001AD938 File Offset: 0x001ABD38
		[Key(9)]
		public HousingData HousingData { get; set; } = new HousingData();

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x001AD941 File Offset: 0x001ABD41
		// (set) Token: 0x0600462C RID: 17964 RVA: 0x001AD949 File Offset: 0x001ABD49
		[Key(10)]
		public bool Cleared { get; set; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x001AD952 File Offset: 0x001ABD52
		// (set) Token: 0x0600462E RID: 17966 RVA: 0x001AD95A File Offset: 0x001ABD5A
		[Key(11)]
		public string SaveTimeString { get; set; }

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600462F RID: 17967 RVA: 0x001AD963 File Offset: 0x001ABD63
		// (set) Token: 0x06004630 RID: 17968 RVA: 0x001AD96B File Offset: 0x001ABD6B
		[Key(13)]
		public Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> WildAnimalTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>>();

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x001AD974 File Offset: 0x001ABD74
		// (set) Token: 0x06004632 RID: 17970 RVA: 0x001AD97C File Offset: 0x001ABD7C
		[Key(14)]
		public int MapID { get; set; }

		// Token: 0x06004633 RID: 17971 RVA: 0x001AD988 File Offset: 0x001ABD88
		public void Copy(WorldData source)
		{
			this.WorldID = source.WorldID;
			this.FreeMode = source.FreeMode;
			this.SaveTime = source.SaveTime;
			this.Name = source.Name;
			this.Environment.Copy(source.Environment);
			foreach (KeyValuePair<int, AgentData> keyValuePair in source.AgentTable)
			{
				AgentData agentData;
				if (!this.AgentTable.TryGetValue(keyValuePair.Key, out agentData))
				{
					AgentData agentData2 = new AgentData();
					this.AgentTable[keyValuePair.Key] = agentData2;
					agentData = agentData2;
				}
				agentData.Copy(keyValuePair.Value);
				agentData.param.Bind(keyValuePair.Value.param.actor);
			}
			if (this.PlayerData == null)
			{
				this.PlayerData = new PlayerData();
			}
			this.PlayerData.Copy(source.PlayerData);
			this.PlayerData.param.Bind(source.PlayerData.param.actor);
			if (this.MerchantData == null)
			{
				this.MerchantData = new MerchantData();
			}
			this.MerchantData.Copy(source.MerchantData);
			this.MerchantData.param.Bind(source.MerchantData.param.actor);
			this.HousingData = new HousingData(source.HousingData);
			this.HousingData.CopyInstances(source.HousingData);
			this.Cleared = source.Cleared;
			this.SaveTimeString = source.SaveTimeString;
			if (!source.TutorialOpenStateTable.IsNullOrEmpty<int, bool>())
			{
				this.TutorialOpenStateTable = source.TutorialOpenStateTable.ToDictionary((KeyValuePair<int, bool> x) => x.Key, (KeyValuePair<int, bool> x) => x.Value);
			}
			else
			{
				this.TutorialOpenStateTable.Clear();
			}
			if (!source.WildAnimalTable.IsNullOrEmpty<AnimalTypes, Dictionary<int, WildAnimalData>>())
			{
				this.WildAnimalTable = (from x in source.WildAnimalTable
				where !x.Value.IsNullOrEmpty<int, WildAnimalData>()
				select x).ToDictionary((KeyValuePair<AnimalTypes, Dictionary<int, WildAnimalData>> x) => x.Key, (KeyValuePair<AnimalTypes, Dictionary<int, WildAnimalData>> x) => (from y in x.Value
				where y.Value != null
				select y).ToDictionary((KeyValuePair<int, WildAnimalData> y) => y.Key, (KeyValuePair<int, WildAnimalData> y) => new WildAnimalData(y.Value)));
			}
			else
			{
				this.WildAnimalTable.Clear();
			}
			this.MapID = source.MapID;
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x001ADC48 File Offset: 0x001AC048
		public void ComplementDiff()
		{
			DateTime dateTime;
			this.SaveTime = ((!DateTime.TryParse(this.SaveTimeString, out dateTime)) ? DateTime.MinValue : dateTime);
			this.PlayerData.ComplementDiff();
			foreach (KeyValuePair<int, AgentData> keyValuePair in this.AgentTable)
			{
				keyValuePair.Value.ComplementDiff();
			}
			this.MerchantData.ComplementDiff();
			if (this.HousingData != null)
			{
				this.HousingData.UpdateDiff();
			}
			else
			{
				this.HousingData = new HousingData();
			}
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x001ADD08 File Offset: 0x001AC108
		public void SaveFile(byte[] buffer)
		{
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				this.SaveFile(memoryStream);
			}
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x001ADD48 File Offset: 0x001AC148
		public void SaveFile(string path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				this.SaveFile(fileStream);
			}
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x001ADD88 File Offset: 0x001AC188
		public void SaveFile(Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				this.SaveFile(binaryWriter);
			}
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x001ADDC8 File Offset: 0x001AC1C8
		public void SaveFile(BinaryWriter writer)
		{
			byte[] buffer = MessagePackSerializer.Serialize<WorldData>(this);
			writer.Write(buffer);
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x001ADDE4 File Offset: 0x001AC1E4
		public static WorldData LoadFile(string fileName)
		{
			WorldData worldData = new WorldData();
			if (worldData.Load(fileName))
			{
				return worldData;
			}
			return null;
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x001ADE08 File Offset: 0x001AC208
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

		// Token: 0x0600463B RID: 17979 RVA: 0x001ADE84 File Offset: 0x001AC284
		public bool Load(Stream stream)
		{
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				result = this.Load(binaryReader);
			}
			return result;
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x001ADEC4 File Offset: 0x001AC2C4
		public bool Load(BinaryReader reader)
		{
			try
			{
				byte[] array = reader.ReadBytes((int)reader.BaseStream.Length);
				if (array.IsNullOrEmpty<byte>())
				{
					return false;
				}
				WorldData worldData = MessagePackSerializer.Deserialize<WorldData>(array);
				worldData.CheckDiff();
				this.Copy(worldData);
				return true;
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x001ADF2C File Offset: 0x001AC32C
		private void CheckDiff()
		{
			this.Environment.UpdateDiff();
			foreach (KeyValuePair<int, AgentData> keyValuePair in this.AgentTable)
			{
				keyValuePair.Value.UpdateDiff();
			}
			if (this.PlayerData != null)
			{
				this.PlayerData.UpdateDiff();
			}
			else
			{
				this.PlayerData = new PlayerData();
			}
			if (this.MerchantData != null)
			{
				this.MerchantData.UpdateDiff();
			}
			else
			{
				this.MerchantData = new MerchantData();
			}
			if (this.HousingData != null)
			{
				this.HousingData.UpdateDiff();
			}
			else
			{
				this.HousingData = new HousingData();
			}
		}

		// Token: 0x04004148 RID: 16712
		[Key(12)]
		public Dictionary<int, bool> TutorialOpenStateTable = new Dictionary<int, bool>();
	}
}
