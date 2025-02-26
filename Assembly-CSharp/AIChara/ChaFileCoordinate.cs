using System;
using System.IO;
using Manager;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007EF RID: 2031
	public class ChaFileCoordinate : ChaFileAssist
	{
		// Token: 0x0600325E RID: 12894 RVA: 0x001300F1 File Offset: 0x0012E4F1
		public ChaFileCoordinate()
		{
			this.MemberInit();
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x0013011F File Offset: 0x0012E51F
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x00130127 File Offset: 0x0012E527
		public string coordinateFileName { get; private set; }

		// Token: 0x06003261 RID: 12897 RVA: 0x00130130 File Offset: 0x0012E530
		public int GetLastErrorCode()
		{
			return this.lastLoadErrorCode;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x00130138 File Offset: 0x0012E538
		public void MemberInit()
		{
			this.clothes = new ChaFileClothes();
			this.accessory = new ChaFileAccessory();
			this.coordinateFileName = string.Empty;
			this.coordinateName = string.Empty;
			this.pngData = null;
			this.lastLoadErrorCode = 0;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x00130174 File Offset: 0x0012E574
		public byte[] SaveBytes()
		{
			byte[] array = MessagePackSerializer.Serialize<ChaFileClothes>(this.clothes);
			byte[] array2 = MessagePackSerializer.Serialize<ChaFileAccessory>(this.accessory);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					binaryWriter.Write(array2.Length);
					binaryWriter.Write(array2);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x0013020C File Offset: 0x0012E60C
		public bool LoadBytes(byte[] data, Version ver)
		{
			bool result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					try
					{
						int count = binaryReader.ReadInt32();
						byte[] bytes = binaryReader.ReadBytes(count);
						this.clothes = MessagePackSerializer.Deserialize<ChaFileClothes>(bytes);
						count = binaryReader.ReadInt32();
						bytes = binaryReader.ReadBytes(count);
						this.accessory = MessagePackSerializer.Deserialize<ChaFileAccessory>(bytes);
					}
					catch (EndOfStreamException ex)
					{
						return false;
					}
					this.clothes.ComplementWithVersion();
					this.accessory.ComplementWithVersion();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x001302D0 File Offset: 0x0012E6D0
		public void SaveFile(string path, int lang)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			this.coordinateFileName = Path.GetFileName(path);
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					if (this.pngData != null)
					{
						binaryWriter.Write(this.pngData);
					}
					binaryWriter.Write(100);
					binaryWriter.Write("【AIS_Clothes】");
					binaryWriter.Write(ChaFileDefine.ChaFileClothesVersion.ToString());
					binaryWriter.Write(lang);
					binaryWriter.Write(this.coordinateName);
					byte[] array = this.SaveBytes();
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
				}
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x001303B8 File Offset: 0x0012E7B8
		public static int GetProductNo(string path)
		{
			if (!File.Exists(path))
			{
				return -1;
			}
			int result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					try
					{
						PngFile.SkipPng(binaryReader);
						if (binaryReader.BaseStream.Length - binaryReader.BaseStream.Position == 0L)
						{
							result = -1;
						}
						else
						{
							int num = binaryReader.ReadInt32();
							result = num;
						}
					}
					catch (EndOfStreamException ex)
					{
						result = -1;
					}
				}
			}
			return result;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x00130468 File Offset: 0x0012E868
		public bool LoadFile(TextAsset ta)
		{
			bool result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.Write(ta.bytes, 0, ta.bytes.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = this.LoadFile(memoryStream, (int)Singleton<GameSystem>.Instance.language);
			}
			return result;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x001304D0 File Offset: 0x0012E8D0
		public bool LoadFile(string path)
		{
			if (!File.Exists(path))
			{
				this.lastLoadErrorCode = -6;
				return false;
			}
			this.coordinateFileName = Path.GetFileName(path);
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				result = this.LoadFile(fileStream, (int)Singleton<GameSystem>.Instance.language);
			}
			return result;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x0013053C File Offset: 0x0012E93C
		public bool LoadFile(Stream st, int lang)
		{
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(st))
			{
				try
				{
					PngFile.SkipPng(binaryReader);
					if (binaryReader.BaseStream.Length - binaryReader.BaseStream.Position == 0L)
					{
						this.lastLoadErrorCode = -5;
						result = false;
					}
					else
					{
						this.loadProductNo = binaryReader.ReadInt32();
						if (this.loadProductNo > 100)
						{
							this.lastLoadErrorCode = -3;
							result = false;
						}
						else
						{
							string a = binaryReader.ReadString();
							if (a != "【AIS_Clothes】")
							{
								this.lastLoadErrorCode = -1;
								result = false;
							}
							else
							{
								this.loadVersion = new Version(binaryReader.ReadString());
								if (this.loadVersion > ChaFileDefine.ChaFileClothesVersion)
								{
									this.lastLoadErrorCode = -2;
									result = false;
								}
								else
								{
									this.language = binaryReader.ReadInt32();
									this.coordinateName = binaryReader.ReadString();
									int count = binaryReader.ReadInt32();
									byte[] data = binaryReader.ReadBytes(count);
									if (this.LoadBytes(data, this.loadVersion))
									{
										this.lastLoadErrorCode = 0;
										result = true;
									}
									else
									{
										this.lastLoadErrorCode = -999;
										result = false;
									}
								}
							}
						}
					}
				}
				catch (EndOfStreamException ex)
				{
					this.lastLoadErrorCode = -999;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x001306B8 File Offset: 0x0012EAB8
		protected void SaveClothes(string path)
		{
			base.SaveFileAssist<ChaFileClothes>(path, this.clothes);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x001306C7 File Offset: 0x0012EAC7
		protected void LoadClothes(string path)
		{
			base.LoadFileAssist<ChaFileClothes>(path, out this.clothes);
			this.clothes.ComplementWithVersion();
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x001306E1 File Offset: 0x0012EAE1
		protected void SaveAccessory(string path)
		{
			base.SaveFileAssist<ChaFileAccessory>(path, this.accessory);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x001306F0 File Offset: 0x0012EAF0
		protected void LoadAccessory(string path)
		{
			base.LoadFileAssist<ChaFileAccessory>(path, out this.accessory);
			this.accessory.ComplementWithVersion();
		}

		// Token: 0x04003238 RID: 12856
		public static readonly string BlockName = "Coordinate";

		// Token: 0x04003239 RID: 12857
		public int loadProductNo;

		// Token: 0x0400323A RID: 12858
		public Version loadVersion = new Version(ChaFileDefine.ChaFileCoordinateVersion.ToString());

		// Token: 0x0400323B RID: 12859
		public int language;

		// Token: 0x0400323C RID: 12860
		public ChaFileClothes clothes;

		// Token: 0x0400323D RID: 12861
		public ChaFileAccessory accessory;

		// Token: 0x0400323E RID: 12862
		public string coordinateName = string.Empty;

		// Token: 0x04003240 RID: 12864
		public byte[] pngData;

		// Token: 0x04003241 RID: 12865
		private int lastLoadErrorCode;
	}
}
