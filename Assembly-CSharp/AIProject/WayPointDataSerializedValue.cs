using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MessagePack;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F46 RID: 3910
	[MessagePackObject(false)]
	public class WayPointDataSerializedValue
	{
		// Token: 0x06008163 RID: 33123 RVA: 0x0036DC3E File Offset: 0x0036C03E
		public WayPointDataSerializedValue()
		{
		}

		// Token: 0x06008164 RID: 33124 RVA: 0x0036DC46 File Offset: 0x0036C046
		public WayPointDataSerializedValue(NavMeshWayPointData data)
		{
			this.MapID = data.MapID;
			this.WayData = data.Points.ToArray<Vector3>();
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06008165 RID: 33125 RVA: 0x0036DC6B File Offset: 0x0036C06B
		// (set) Token: 0x06008166 RID: 33126 RVA: 0x0036DC73 File Offset: 0x0036C073
		[Key(0)]
		public int MapID { get; set; }

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06008167 RID: 33127 RVA: 0x0036DC7C File Offset: 0x0036C07C
		// (set) Token: 0x06008168 RID: 33128 RVA: 0x0036DC84 File Offset: 0x0036C084
		[Key(1)]
		public Vector3[] WayData { get; set; }

		// Token: 0x06008169 RID: 33129 RVA: 0x0036DC90 File Offset: 0x0036C090
		public static async Task SaveAsync(string path, NavMeshWayPointData data)
		{
			WayPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD <SaveAsync>c__AnonStoreyD = new WayPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD();
			<SaveAsync>c__AnonStoreyD.path = path;
			<SaveAsync>c__AnonStoreyD.serialized = new WayPointDataSerializedValue(data);
			await SystemUtil.TryProcAsync(Task.Run(delegate()
			{
				WayPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD.<SaveAsync>c__asyncC <SaveAsync>c__asyncC;
				<SaveAsync>c__asyncC.<>f__ref$13 = <SaveAsync>c__AnonStoreyD;
				<SaveAsync>c__asyncC.$builder = AsyncTaskMethodBuilder.Create();
				<SaveAsync>c__asyncC.$builder.Start<WayPointDataSerializedValue.<SaveAsync>c__async0.<SaveAsync>c__AnonStoreyD.<SaveAsync>c__asyncC>(ref <SaveAsync>c__asyncC);
				return <SaveAsync>c__asyncC.$builder.Task;
			}));
		}

		// Token: 0x0600816A RID: 33130 RVA: 0x0036DCD0 File Offset: 0x0036C0D0
		public static void Save(string path, byte[] bytes)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				WayPointDataSerializedValue.Save(fileStream, bytes);
			}
		}

		// Token: 0x0600816B RID: 33131 RVA: 0x0036DD10 File Offset: 0x0036C110
		public static void Save(Stream stream, byte[] bytes)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				WayPointDataSerializedValue.Save(binaryWriter, bytes);
			}
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x0036DD50 File Offset: 0x0036C150
		public static void Save(BinaryWriter writer, byte[] bytes)
		{
			writer.Write(bytes);
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x0036DD5C File Offset: 0x0036C15C
		public static void Load(string path, ref NavMeshWayPointData data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				WayPointDataSerializedValue.Load(fileStream, ref data);
			}
		}

		// Token: 0x0600816E RID: 33134 RVA: 0x0036DD9C File Offset: 0x0036C19C
		public static void Load(Stream stream, ref NavMeshWayPointData data)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				WayPointDataSerializedValue.Load(binaryReader, ref data);
			}
		}

		// Token: 0x0600816F RID: 33135 RVA: 0x0036DDDC File Offset: 0x0036C1DC
		public static void Load(BinaryReader reader, ref NavMeshWayPointData data)
		{
			byte[] bytes = reader.ReadBytes((int)reader.BaseStream.Length);
			WayPointDataSerializedValue wayPointDataSerializedValue = MessagePackSerializer.Deserialize<WayPointDataSerializedValue>(bytes);
			data.MapID = wayPointDataSerializedValue.MapID;
			data.Allocation(wayPointDataSerializedValue.WayData);
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x0036DE20 File Offset: 0x0036C220
		public static async Task SaveAsync(string path, byte[] bytes)
		{
			using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				await WayPointDataSerializedValue.SaveAsync(stream, bytes);
			}
		}

		// Token: 0x06008171 RID: 33137 RVA: 0x0036DE60 File Offset: 0x0036C260
		public static async Task SaveAsync(Stream stream, byte[] bytes)
		{
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				await WayPointDataSerializedValue.SaveAsync(writer, bytes);
			}
		}

		// Token: 0x06008172 RID: 33138 RVA: 0x0036DEA0 File Offset: 0x0036C2A0
		public static async Task SaveAsync(BinaryWriter writer, byte[] bytes)
		{
			await Task.Run(delegate()
			{
				writer.Write(bytes);
			});
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x0036DEE0 File Offset: 0x0036C2E0
		public static async Task<WayPointDataSerializedValue> LoadAsync(string path)
		{
			WayPointDataSerializedValue result;
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				result = await WayPointDataSerializedValue.LoadAsync(stream);
			}
			return result;
		}

		// Token: 0x06008174 RID: 33140 RVA: 0x0036DF18 File Offset: 0x0036C318
		public static async Task<WayPointDataSerializedValue> LoadAsync(Stream stream)
		{
			WayPointDataSerializedValue result;
			using (BinaryReader reader = new BinaryReader(stream))
			{
				result = await WayPointDataSerializedValue.LoadAsync(reader);
			}
			return result;
		}

		// Token: 0x06008175 RID: 33141 RVA: 0x0036DF50 File Offset: 0x0036C350
		public static async Task<WayPointDataSerializedValue> LoadAsync(BinaryReader reader)
		{
			return await Task.Run<WayPointDataSerializedValue>(delegate()
			{
				byte[] bytes = reader.ReadBytes((int)reader.BaseStream.Length);
				return MessagePackSerializer.Deserialize<WayPointDataSerializedValue>(bytes);
			});
		}
	}
}
