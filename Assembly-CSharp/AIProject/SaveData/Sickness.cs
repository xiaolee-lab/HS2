using System;
using System.Collections.Generic;
using ADV;
using AIProject.Definitions;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x0200096F RID: 2415
	[MessagePackObject(false)]
	public class Sickness : ICommandData
	{
		// Token: 0x060043C7 RID: 17351 RVA: 0x001A7FBF File Offset: 0x001A63BF
		public Sickness()
		{
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x001A7FCE File Offset: 0x001A63CE
		public Sickness(Sickness source)
		{
			this._id = source._id;
			this.ElapsedTime = source.ElapsedTime;
			this.UsedMedicine = source.UsedMedicine;
			this.Enabled = source.Enabled;
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x001A800D File Offset: 0x001A640D
		// (set) Token: 0x060043CA RID: 17354 RVA: 0x001A8015 File Offset: 0x001A6415
		[Key(0)]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if (this._id == value)
				{
					return;
				}
				this.SetID(value);
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x001A802B File Offset: 0x001A642B
		// (set) Token: 0x060043CC RID: 17356 RVA: 0x001A8033 File Offset: 0x001A6433
		[IgnoreMember]
		public int OverwritableID
		{
			get
			{
				return this._id;
			}
			set
			{
				this.SetID(value);
			}
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x001A803C File Offset: 0x001A643C
		private void SetID(int idValue)
		{
			this._id = idValue;
			this.UsedMedicine = false;
			this.Enabled = false;
			this.ElapsedTime = TimeSpan.Zero;
			this.Duration = TimeSpan.Zero;
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x001A806C File Offset: 0x001A646C
		[IgnoreMember]
		public string Name
		{
			get
			{
				if (this._id == -1)
				{
					return "なし";
				}
				string result;
				if (!Sickness.NameTable.TryGetValue(this.ID, out result))
				{
					return string.Format("無効:{0}", this.ID.ToString());
				}
				return result;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x001A80C2 File Offset: 0x001A64C2
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x001A80CA File Offset: 0x001A64CA
		[Key(1)]
		public TimeSpan ElapsedTime { get; set; }

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x001A80D3 File Offset: 0x001A64D3
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x001A80DB File Offset: 0x001A64DB
		[Key(2)]
		public bool UsedMedicine { get; set; }

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x001A80E4 File Offset: 0x001A64E4
		// (set) Token: 0x060043D4 RID: 17364 RVA: 0x001A80EC File Offset: 0x001A64EC
		[Key(3)]
		public bool Enabled { get; set; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x001A80F5 File Offset: 0x001A64F5
		// (set) Token: 0x060043D6 RID: 17366 RVA: 0x001A80FD File Offset: 0x001A64FD
		[Key(4)]
		public TimeSpan Duration { get; set; }

		// Token: 0x060043D7 RID: 17367 RVA: 0x001A8108 File Offset: 0x001A6508
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			return new CommandData[]
			{
				new CommandData(CommandData.Command.String, head + string.Format(".{0}", "Name"), new Func<object>(this.get_Name), null),
				new CommandData(CommandData.Command.String, head + string.Format(".{0}", "ElapsedTime"), () => this.ElapsedTime.ToString(), null),
				new CommandData(CommandData.Command.BOOL, head + string.Format(".{0}", "UsedMedicine"), () => this.UsedMedicine, null)
			};
		}

		// Token: 0x0400401C RID: 16412
		private int _id = -1;
	}
}
