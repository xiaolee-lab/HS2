using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MessagePack;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000AC9 RID: 2761
	[MessagePackObject(false)]
	public class HPointDataSerializedValue
	{
		// Token: 0x060050DD RID: 20701 RVA: 0x001FD302 File Offset: 0x001FB702
		public HPointDataSerializedValue()
		{
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x001FD30C File Offset: 0x001FB70C
		public HPointDataSerializedValue(AutoHPointData data)
		{
			this.HPointDataAreaID = new Dictionary<string, List<int>>();
			this.HPointDataPos = new Dictionary<string, List<Vector3>>();
			foreach (KeyValuePair<string, List<UnityEx.ValueTuple<int, Vector3>>> keyValuePair in data.Points)
			{
				this.HPointDataAreaID.Add(keyValuePair.Key, new List<int>());
				this.HPointDataPos.Add(keyValuePair.Key, new List<Vector3>());
				foreach (UnityEx.ValueTuple<int, Vector3> valueTuple in keyValuePair.Value)
				{
					this.HPointDataAreaID[keyValuePair.Key].Add(valueTuple.Item1);
					this.HPointDataPos[keyValuePair.Key].Add(valueTuple.Item2);
				}
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x001FD42C File Offset: 0x001FB82C
		// (set) Token: 0x060050E0 RID: 20704 RVA: 0x001FD434 File Offset: 0x001FB834
		[Key(0)]
		public Dictionary<string, List<int>> HPointDataAreaID { get; set; }

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x001FD43D File Offset: 0x001FB83D
		// (set) Token: 0x060050E2 RID: 20706 RVA: 0x001FD445 File Offset: 0x001FB845
		[Key(1)]
		public Dictionary<string, List<Vector3>> HPointDataPos { get; set; }

		// Token: 0x060050E3 RID: 20707 RVA: 0x001FD450 File Offset: 0x001FB850
		public static async Task SaveAsync(string path, AutoHPointData data)
		{
			HPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD <SaveAsync>c__AnonStoreyD = new HPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD();
			<SaveAsync>c__AnonStoreyD.path = path;
			<SaveAsync>c__AnonStoreyD.serialized = new HPointDataSerializedValue(data);
			await SystemUtil.TryProcAsync(Task.Run(delegate()
			{
				HPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD.<SaveAsync>c__asyncC <SaveAsync>c__asyncC;
				<SaveAsync>c__asyncC.<>f__ref$13 = <SaveAsync>c__AnonStoreyD;
				<SaveAsync>c__asyncC.$builder = AsyncTaskMethodBuilder.Create();
				<SaveAsync>c__asyncC.$builder.Start<HPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD.<SaveAsync>c__asyncC>(ref <SaveAsync>c__asyncC);
				return <SaveAsync>c__asyncC.$builder.Task;
			}));
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x001FD490 File Offset: 0x001FB890
		public static void Save(string path, byte[] bytes)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				HPointDataSerializedValue.Save(fileStream, bytes);
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x001FD4D0 File Offset: 0x001FB8D0
		public static void Save(Stream stream, byte[] bytes)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				HPointDataSerializedValue.Save(binaryWriter, bytes);
			}
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x001FD510 File Offset: 0x001FB910
		public static void Save(BinaryWriter writer, byte[] bytes)
		{
			writer.Write(bytes);
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x001FD51C File Offset: 0x001FB91C
		public static void Load(string path, ref AutoHPointData data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				HPointDataSerializedValue.Load(fileStream, ref data);
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x001FD55C File Offset: 0x001FB95C
		public static void Load(Stream stream, ref AutoHPointData data)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				HPointDataSerializedValue.Load(binaryReader, ref data);
			}
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x001FD59C File Offset: 0x001FB99C
		public static void Load(BinaryReader reader, ref AutoHPointData data)
		{
			byte[] bytes = reader.ReadBytes((int)reader.BaseStream.Length);
			HPointDataSerializedValue hpointDataSerializedValue = MessagePackSerializer.Deserialize<HPointDataSerializedValue>(bytes);
			data.Allocation(hpointDataSerializedValue.HPointDataAreaID, hpointDataSerializedValue.HPointDataPos);
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x001FD5D8 File Offset: 0x001FB9D8
		public static async Task SaveAsync(string path, byte[] bytes)
		{
			using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				await HPointDataSerializedValue.SaveAsync(stream, bytes);
			}
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x001FD618 File Offset: 0x001FBA18
		public static async Task SaveAsync(Stream stream, byte[] bytes)
		{
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				await HPointDataSerializedValue.SaveAsync(writer, bytes);
			}
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x001FD658 File Offset: 0x001FBA58
		public static async Task SaveAsync(BinaryWriter writer, byte[] bytes)
		{
			await Task.Run(delegate()
			{
				writer.Write(bytes);
			});
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x001FD698 File Offset: 0x001FBA98
		public static async Task<HPointDataSerializedValue> LoadAsync(string path)
		{
			HPointDataSerializedValue result;
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				result = await HPointDataSerializedValue.LoadAsync(stream);
			}
			return result;
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x001FD6D0 File Offset: 0x001FBAD0
		public static async Task<HPointDataSerializedValue> LoadAsync(Stream stream)
		{
			HPointDataSerializedValue result;
			using (BinaryReader reader = new BinaryReader(stream))
			{
				result = await HPointDataSerializedValue.LoadAsync(reader);
			}
			return result;
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x001FD708 File Offset: 0x001FBB08
		public static async Task<HPointDataSerializedValue> LoadAsync(BinaryReader reader)
		{
			return await Task.Run<HPointDataSerializedValue>(delegate()
			{
				byte[] bytes = reader.ReadBytes((int)reader.BaseStream.Length);
				return MessagePackSerializer.Deserialize<HPointDataSerializedValue>(bytes);
			});
		}
	}
}
