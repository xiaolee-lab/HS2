using System;
using System.IO;
using System.Text;

namespace Studio
{
	// Token: 0x0200123A RID: 4666
	public class WorkInfo
	{
		// Token: 0x06009982 RID: 39298 RVA: 0x003F38F4 File Offset: 0x003F1CF4
		public void Init()
		{
			for (int i = 0; i < 6; i++)
			{
				this.visibleFlags[i] = true;
			}
			this.visibleCenter = true;
			this.visibleAxis = true;
			this.useAlt = false;
			this.visibleAxisTranslation = true;
			this.visibleAxisCenter = true;
			this.visibleGimmick = true;
		}

		// Token: 0x06009983 RID: 39299 RVA: 0x003F3948 File Offset: 0x003F1D48
		public void Save()
		{
			string path = UserData.Create("studio") + "work.dat";
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8))
				{
					try
					{
						binaryWriter.Write(this.version.ToString());
						for (int i = 0; i < 6; i++)
						{
							binaryWriter.Write(this.visibleFlags[i]);
						}
						binaryWriter.Write(this.visibleCenter);
						binaryWriter.Write(this.visibleAxis);
						binaryWriter.Write(this.useAlt);
						binaryWriter.Write(this.visibleAxisTranslation);
						binaryWriter.Write(this.visibleAxisCenter);
						binaryWriter.Write(this.visibleGimmick);
					}
					catch (Exception)
					{
						File.Delete(path);
					}
				}
			}
		}

		// Token: 0x06009984 RID: 39300 RVA: 0x003F3A58 File Offset: 0x003F1E58
		public void Load()
		{
			string path = UserData.Create("studio") + "work.dat";
			if (!File.Exists(path))
			{
				this.Init();
				return;
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.UTF8))
				{
					try
					{
						Version version = new Version(binaryReader.ReadString());
						for (int i = 0; i < 6; i++)
						{
							this.visibleFlags[i] = binaryReader.ReadBoolean();
						}
						this.visibleCenter = binaryReader.ReadBoolean();
						this.visibleAxis = binaryReader.ReadBoolean();
						this.useAlt = binaryReader.ReadBoolean();
						this.visibleAxisTranslation = binaryReader.ReadBoolean();
						this.visibleAxisCenter = binaryReader.ReadBoolean();
						if (version.CompareTo(new Version(1, 0, 1)) >= 0)
						{
							this.visibleGimmick = binaryReader.ReadBoolean();
						}
					}
					catch (Exception)
					{
						File.Delete(path);
						this.Init();
					}
				}
			}
		}

		// Token: 0x04007AD6 RID: 31446
		private const string userPath = "studio";

		// Token: 0x04007AD7 RID: 31447
		private const string fileName = "work.dat";

		// Token: 0x04007AD8 RID: 31448
		private readonly Version version = new Version(1, 0, 1);

		// Token: 0x04007AD9 RID: 31449
		public bool[] visibleFlags = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x04007ADA RID: 31450
		public bool visibleCenter = true;

		// Token: 0x04007ADB RID: 31451
		public bool visibleAxis = true;

		// Token: 0x04007ADC RID: 31452
		public bool useAlt;

		// Token: 0x04007ADD RID: 31453
		public bool visibleAxisTranslation = true;

		// Token: 0x04007ADE RID: 31454
		public bool visibleAxisCenter = true;

		// Token: 0x04007ADF RID: 31455
		public bool visibleGimmick = true;
	}
}
