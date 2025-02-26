using System;
using System.IO;
using MessagePack;

namespace AIChara
{
	// Token: 0x020007E4 RID: 2020
	public class ChaFile
	{
		// Token: 0x060031DD RID: 12765 RVA: 0x00120E6C File Offset: 0x0011F26C
		public ChaFile()
		{
			this.custom = new ChaFileCustom();
			this.coordinate = new ChaFileCoordinate();
			this.parameter = new ChaFileParameter();
			this.gameinfo = new ChaFileGameInfo();
			this.parameter2 = new ChaFileParameter2();
			this.gameinfo2 = new ChaFileGameInfo2();
			this.status = new ChaFileStatus();
			this.lastLoadErrorCode = 0;
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x00120EFE File Offset: 0x0011F2FE
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x00120F06 File Offset: 0x0011F306
		public string charaFileName { get; protected set; }

		// Token: 0x060031E0 RID: 12768 RVA: 0x00120F0F File Offset: 0x0011F30F
		public int GetLastErrorCode()
		{
			return this.lastLoadErrorCode;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00120F18 File Offset: 0x0011F318
		protected bool SaveFile(string path, int lang)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			this.charaFileName = Path.GetFileName(path);
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				result = this.SaveFile(fileStream, true, lang);
			}
			return result;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00120F80 File Offset: 0x0011F380
		protected bool SaveFile(Stream st, bool savePng, int lang)
		{
			bool result;
			using (BinaryWriter binaryWriter = new BinaryWriter(st))
			{
				result = this.SaveFile(binaryWriter, savePng, lang);
			}
			return result;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00120FC4 File Offset: 0x0011F3C4
		protected bool SaveFile(BinaryWriter bw, bool savePng, int lang)
		{
			if (savePng && this.pngData != null)
			{
				bw.Write(this.pngData);
			}
			bw.Write(100);
			bw.Write("【AIS_Chara】");
			bw.Write(ChaFileDefine.ChaFileVersion.ToString());
			bw.Write(lang);
			bw.Write(this.userID);
			bw.Write(this.dataID);
			byte[] customBytes = this.GetCustomBytes();
			byte[] coordinateBytes = this.GetCoordinateBytes();
			byte[] parameterBytes = this.GetParameterBytes();
			byte[] gameInfoBytes = this.GetGameInfoBytes();
			byte[] statusBytes = this.GetStatusBytes();
			byte[] parameter2Bytes = this.GetParameter2Bytes();
			byte[] gameInfo2Bytes = this.GetGameInfo2Bytes();
			int num = 7;
			long num2 = 0L;
			string[] array = new string[]
			{
				ChaFileCustom.BlockName,
				ChaFileCoordinate.BlockName,
				ChaFileParameter.BlockName,
				ChaFileGameInfo.BlockName,
				ChaFileStatus.BlockName,
				ChaFileParameter2.BlockName,
				ChaFileGameInfo2.BlockName
			};
			string[] array2 = new string[]
			{
				ChaFileDefine.ChaFileCustomVersion.ToString(),
				ChaFileDefine.ChaFileCoordinateVersion.ToString(),
				ChaFileDefine.ChaFileParameterVersion.ToString(),
				ChaFileDefine.ChaFileGameInfoVersion.ToString(),
				ChaFileDefine.ChaFileStatusVersion.ToString(),
				ChaFileDefine.ChaFileParameterVersion2.ToString(),
				ChaFileDefine.ChaFileGameInfoVersion2.ToString()
			};
			long[] array3 = new long[num];
			array3[0] = (long)((customBytes != null) ? customBytes.Length : 0);
			array3[1] = (long)((coordinateBytes != null) ? coordinateBytes.Length : 0);
			array3[2] = (long)((parameterBytes != null) ? parameterBytes.Length : 0);
			array3[3] = (long)((gameInfoBytes != null) ? gameInfoBytes.Length : 0);
			array3[4] = (long)((statusBytes != null) ? statusBytes.Length : 0);
			array3[5] = (long)((parameter2Bytes != null) ? parameter2Bytes.Length : 0);
			array3[6] = (long)((gameInfo2Bytes != null) ? gameInfo2Bytes.Length : 0);
			long[] array4 = new long[]
			{
				num2,
				num2 + array3[0],
				num2 + array3[0] + array3[1],
				num2 + array3[0] + array3[1] + array3[2],
				num2 + array3[0] + array3[1] + array3[2] + array3[3],
				num2 + array3[0] + array3[1] + array3[2] + array3[3] + array3[4],
				num2 + array3[0] + array3[1] + array3[2] + array3[3] + array3[4] + array3[5]
			};
			BlockHeader blockHeader = new BlockHeader();
			for (int i = 0; i < num; i++)
			{
				BlockHeader.Info item = new BlockHeader.Info
				{
					name = array[i],
					version = array2[i],
					size = array3[i],
					pos = array4[i]
				};
				blockHeader.lstInfo.Add(item);
			}
			byte[] array5 = MessagePackSerializer.Serialize<BlockHeader>(blockHeader);
			bw.Write(array5.Length);
			bw.Write(array5);
			long num3 = 0L;
			foreach (long num4 in array3)
			{
				num3 += num4;
			}
			bw.Write(num3);
			bw.Write(customBytes);
			bw.Write(coordinateBytes);
			bw.Write(parameterBytes);
			bw.Write(gameInfoBytes);
			bw.Write(statusBytes);
			bw.Write(parameter2Bytes);
			bw.Write(gameInfo2Bytes);
			return true;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x0012132C File Offset: 0x0011F72C
		public static bool GetProductInfo(string path, out ChaFile.ProductInfo info)
		{
			info = new ChaFile.ProductInfo();
			if (!File.Exists(path))
			{
				return false;
			}
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					long pngSize = PngFile.GetPngSize(binaryReader);
					if (pngSize != 0L)
					{
						binaryReader.BaseStream.Seek(pngSize, SeekOrigin.Current);
						if (binaryReader.BaseStream.Length - binaryReader.BaseStream.Position == 0L)
						{
							return false;
						}
					}
					try
					{
						info.productNo = binaryReader.ReadInt32();
						info.tag = binaryReader.ReadString();
						if (info.tag != "【AIS_Chara】")
						{
							result = false;
						}
						else
						{
							info.version = new Version(binaryReader.ReadString());
							if (info.version > ChaFileDefine.ChaFileVersion)
							{
								result = false;
							}
							else
							{
								info.language = binaryReader.ReadInt32();
								info.userID = binaryReader.ReadString();
								info.dataID = binaryReader.ReadString();
								result = true;
							}
						}
					}
					catch (EndOfStreamException ex)
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00121480 File Offset: 0x0011F880
		protected bool LoadFile(string path, int lang, bool noLoadPNG = false, bool noLoadStatus = true)
		{
			if (!File.Exists(path))
			{
				this.lastLoadErrorCode = -6;
				return false;
			}
			this.charaFileName = Path.GetFileName(path);
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				result = this.LoadFile(fileStream, lang, noLoadPNG, noLoadStatus);
			}
			return result;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x001214E8 File Offset: 0x0011F8E8
		protected bool LoadFile(Stream st, int lang, bool noLoadPNG = false, bool noLoadStatus = true)
		{
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(st))
			{
				result = this.LoadFile(binaryReader, lang, noLoadPNG, noLoadStatus);
			}
			return result;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x0012152C File Offset: 0x0011F92C
		protected bool LoadFile(BinaryReader br, int lang, bool noLoadPNG = false, bool noLoadStatus = true)
		{
			long pngSize = PngFile.GetPngSize(br);
			if (pngSize != 0L)
			{
				if (noLoadPNG)
				{
					br.BaseStream.Seek(pngSize, SeekOrigin.Current);
				}
				else
				{
					this.pngData = br.ReadBytes((int)pngSize);
				}
				if (br.BaseStream.Length - br.BaseStream.Position == 0L)
				{
					this.lastLoadErrorCode = -5;
					return false;
				}
			}
			try
			{
				this.loadProductNo = br.ReadInt32();
				if (this.loadProductNo > 100)
				{
					this.lastLoadErrorCode = -3;
					return false;
				}
				string a = br.ReadString();
				if (a != "【AIS_Chara】")
				{
					this.lastLoadErrorCode = -1;
					return false;
				}
				this.loadVersion = new Version(br.ReadString());
				if (this.loadVersion > ChaFileDefine.ChaFileVersion)
				{
					this.lastLoadErrorCode = -2;
					return false;
				}
				this.language = br.ReadInt32();
				this.userID = br.ReadString();
				this.dataID = br.ReadString();
				int count = br.ReadInt32();
				byte[] bytes = br.ReadBytes(count);
				BlockHeader blockHeader = MessagePackSerializer.Deserialize<BlockHeader>(bytes);
				long num = br.ReadInt64();
				long position = br.BaseStream.Position;
				BlockHeader.Info info = blockHeader.SearchInfo(ChaFileCustom.BlockName);
				if (info != null)
				{
					Version version = new Version(info.version);
					if (version > ChaFileDefine.ChaFileCustomVersion)
					{
						this.lastLoadErrorCode = -2;
					}
					else
					{
						br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
						byte[] data = br.ReadBytes((int)info.size);
						this.SetCustomBytes(data, version);
					}
				}
				info = blockHeader.SearchInfo(ChaFileCoordinate.BlockName);
				if (info != null)
				{
					Version version2 = new Version(info.version);
					if (version2 > ChaFileDefine.ChaFileCoordinateVersion)
					{
						this.lastLoadErrorCode = -2;
					}
					else
					{
						br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
						byte[] data2 = br.ReadBytes((int)info.size);
						this.SetCoordinateBytes(data2, version2);
					}
				}
				info = blockHeader.SearchInfo(ChaFileParameter.BlockName);
				if (info != null)
				{
					Version v = new Version(info.version);
					if (v > ChaFileDefine.ChaFileParameterVersion)
					{
						this.lastLoadErrorCode = -2;
					}
					else
					{
						br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
						byte[] parameterBytes = br.ReadBytes((int)info.size);
						this.SetParameterBytes(parameterBytes);
					}
				}
				info = blockHeader.SearchInfo(ChaFileGameInfo.BlockName);
				if (info != null)
				{
					Version v2 = new Version(info.version);
					if (v2 > ChaFileDefine.ChaFileGameInfoVersion)
					{
						this.lastLoadErrorCode = -2;
					}
					else
					{
						br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
						byte[] gameInfoBytes = br.ReadBytes((int)info.size);
						this.SetGameInfoBytes(gameInfoBytes);
					}
				}
				if (!noLoadStatus)
				{
					info = blockHeader.SearchInfo(ChaFileStatus.BlockName);
					if (info != null)
					{
						Version v3 = new Version(info.version);
						if (v3 > ChaFileDefine.ChaFileStatusVersion)
						{
							this.lastLoadErrorCode = -2;
						}
						else
						{
							br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
							byte[] statusBytes = br.ReadBytes((int)info.size);
							this.SetStatusBytes(statusBytes);
						}
					}
				}
				info = blockHeader.SearchInfo(ChaFileParameter2.BlockName);
				if (info != null)
				{
					Version v4 = new Version(info.version);
					if (v4 > ChaFileDefine.ChaFileParameterVersion2)
					{
						this.lastLoadErrorCode = -2;
					}
					else
					{
						br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
						byte[] parameter2Bytes = br.ReadBytes((int)info.size);
						this.SetParameter2Bytes(parameter2Bytes);
					}
				}
				info = blockHeader.SearchInfo(ChaFileGameInfo2.BlockName);
				if (info != null)
				{
					Version v5 = new Version(info.version);
					if (v5 > ChaFileDefine.ChaFileGameInfoVersion2)
					{
						this.lastLoadErrorCode = -2;
					}
					else
					{
						br.BaseStream.Seek(position + info.pos, SeekOrigin.Begin);
						byte[] gameInfo2Bytes = br.ReadBytes((int)info.size);
						this.SetGameInfo2Bytes(gameInfo2Bytes);
					}
				}
				br.BaseStream.Seek(position + num, SeekOrigin.Begin);
			}
			catch (EndOfStreamException ex)
			{
				this.lastLoadErrorCode = -999;
				return false;
			}
			this.lastLoadErrorCode = 0;
			return true;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x001219D8 File Offset: 0x0011FDD8
		public byte[] GetCustomBytes()
		{
			return ChaFile.GetCustomBytes(this.custom);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x001219E5 File Offset: 0x0011FDE5
		public static byte[] GetCustomBytes(ChaFileCustom _custom)
		{
			return _custom.SaveBytes();
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x001219ED File Offset: 0x0011FDED
		public byte[] GetCoordinateBytes()
		{
			return ChaFile.GetCoordinateBytes(this.coordinate);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x001219FA File Offset: 0x0011FDFA
		public static byte[] GetCoordinateBytes(ChaFileCoordinate _coordinate)
		{
			return _coordinate.SaveBytes();
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x00121A02 File Offset: 0x0011FE02
		public byte[] GetParameterBytes()
		{
			return ChaFile.GetParameterBytes(this.parameter);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00121A0F File Offset: 0x0011FE0F
		public static byte[] GetParameterBytes(ChaFileParameter _parameter)
		{
			return MessagePackSerializer.Serialize<ChaFileParameter>(_parameter);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x00121A17 File Offset: 0x0011FE17
		public byte[] GetParameter2Bytes()
		{
			return ChaFile.GetParameter2Bytes(this.parameter2);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x00121A24 File Offset: 0x0011FE24
		public static byte[] GetParameter2Bytes(ChaFileParameter2 _parameter)
		{
			return MessagePackSerializer.Serialize<ChaFileParameter2>(_parameter);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00121A2C File Offset: 0x0011FE2C
		public byte[] GetGameInfoBytes()
		{
			return ChaFile.GetGameInfoBytes(this.gameinfo);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00121A39 File Offset: 0x0011FE39
		public static byte[] GetGameInfoBytes(ChaFileGameInfo _gameinfo)
		{
			return MessagePackSerializer.Serialize<ChaFileGameInfo>(_gameinfo);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00121A41 File Offset: 0x0011FE41
		public byte[] GetGameInfo2Bytes()
		{
			return ChaFile.GetGameInfo2Bytes(this.gameinfo2);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00121A4E File Offset: 0x0011FE4E
		public static byte[] GetGameInfo2Bytes(ChaFileGameInfo2 _gameinfo)
		{
			return MessagePackSerializer.Serialize<ChaFileGameInfo2>(_gameinfo);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x00121A56 File Offset: 0x0011FE56
		public byte[] GetStatusBytes()
		{
			return ChaFile.GetStatusBytes(this.status);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x00121A63 File Offset: 0x0011FE63
		public static byte[] GetStatusBytes(ChaFileStatus _status)
		{
			return MessagePackSerializer.Serialize<ChaFileStatus>(_status);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00121A6B File Offset: 0x0011FE6B
		public void SetCustomBytes(byte[] data, Version ver)
		{
			this.custom.LoadBytes(data, ver);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x00121A7B File Offset: 0x0011FE7B
		public void SetCoordinateBytes(byte[] data, Version ver)
		{
			this.coordinate.LoadBytes(data, ver);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00121A8C File Offset: 0x0011FE8C
		public void SetParameterBytes(byte[] data)
		{
			ChaFileParameter chaFileParameter = MessagePackSerializer.Deserialize<ChaFileParameter>(data);
			chaFileParameter.ComplementWithVersion();
			this.parameter.Copy(chaFileParameter);
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x00121AB4 File Offset: 0x0011FEB4
		public void SetParameter2Bytes(byte[] data)
		{
			ChaFileParameter2 chaFileParameter = MessagePackSerializer.Deserialize<ChaFileParameter2>(data);
			chaFileParameter.ComplementWithVersion();
			this.parameter2.Copy(chaFileParameter);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00121ADC File Offset: 0x0011FEDC
		public void SetGameInfoBytes(byte[] data)
		{
			ChaFileGameInfo chaFileGameInfo = MessagePackSerializer.Deserialize<ChaFileGameInfo>(data);
			chaFileGameInfo.ComplementWithVersion();
			this.gameinfo.Copy(chaFileGameInfo);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00121B04 File Offset: 0x0011FF04
		public void SetGameInfo2Bytes(byte[] data)
		{
			ChaFileGameInfo2 chaFileGameInfo = MessagePackSerializer.Deserialize<ChaFileGameInfo2>(data);
			chaFileGameInfo.ComplementWithVersion();
			this.gameinfo2.Copy(chaFileGameInfo);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00121B2C File Offset: 0x0011FF2C
		public void SetStatusBytes(byte[] data)
		{
			ChaFileStatus chaFileStatus = MessagePackSerializer.Deserialize<ChaFileStatus>(data);
			chaFileStatus.ComplementWithVersion();
			this.status.Copy(chaFileStatus);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00121B52 File Offset: 0x0011FF52
		public static void CopyChaFile(ChaFile dst, ChaFile src, bool _custom = true, bool _coordinate = true, bool _parameter = true, bool _gameinfo = true, bool _status = true)
		{
			dst.CopyAll(src, _custom, _coordinate, _parameter, _gameinfo, _status);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00121B64 File Offset: 0x0011FF64
		public void CopyAll(ChaFile _chafile, bool _custom = true, bool _coordinate = true, bool _parameter = true, bool _gameinfo = true, bool _status = true)
		{
			if (_custom)
			{
				this.CopyCustom(_chafile.custom);
			}
			if (_coordinate)
			{
				this.CopyCoordinate(_chafile.coordinate);
			}
			if (_status)
			{
				this.CopyStatus(_chafile.status);
			}
			if (_parameter)
			{
				this.CopyParameter(_chafile.parameter);
				this.CopyParameter2(_chafile.parameter2);
			}
			if (_gameinfo)
			{
				this.CopyGameInfo(_chafile.gameinfo);
				this.CopyGameInfo2(_chafile.gameinfo2);
			}
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x00121BE8 File Offset: 0x0011FFE8
		public void CopyCustom(ChaFileCustom _custom)
		{
			byte[] customBytes = ChaFile.GetCustomBytes(_custom);
			this.SetCustomBytes(customBytes, ChaFileDefine.ChaFileCustomVersion);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00121C08 File Offset: 0x00120008
		public void CopyCoordinate(ChaFileCoordinate _coordinate)
		{
			byte[] coordinateBytes = ChaFile.GetCoordinateBytes(_coordinate);
			this.SetCoordinateBytes(coordinateBytes, ChaFileDefine.ChaFileCoordinateVersion);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00121C28 File Offset: 0x00120028
		public void CopyParameter(ChaFileParameter _parameter)
		{
			byte[] parameterBytes = ChaFile.GetParameterBytes(_parameter);
			this.SetParameterBytes(parameterBytes);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00121C44 File Offset: 0x00120044
		public void CopyParameter2(ChaFileParameter2 _parameter)
		{
			byte[] parameter2Bytes = ChaFile.GetParameter2Bytes(_parameter);
			this.SetParameter2Bytes(parameter2Bytes);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00121C60 File Offset: 0x00120060
		public void CopyGameInfo(ChaFileGameInfo _gameinfo)
		{
			byte[] gameInfoBytes = ChaFile.GetGameInfoBytes(_gameinfo);
			this.SetGameInfoBytes(gameInfoBytes);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00121C7C File Offset: 0x0012007C
		public void CopyGameInfo2(ChaFileGameInfo2 _gameinfo)
		{
			byte[] gameInfo2Bytes = ChaFile.GetGameInfo2Bytes(_gameinfo);
			this.SetGameInfo2Bytes(gameInfo2Bytes);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00121C98 File Offset: 0x00120098
		public void CopyStatus(ChaFileStatus _status)
		{
			byte[] statusBytes = ChaFile.GetStatusBytes(_status);
			this.SetStatusBytes(statusBytes);
		}

		// Token: 0x04003202 RID: 12802
		public int loadProductNo;

		// Token: 0x04003203 RID: 12803
		public Version loadVersion = new Version(ChaFileDefine.ChaFileVersion.ToString());

		// Token: 0x04003204 RID: 12804
		public int language;

		// Token: 0x04003206 RID: 12806
		public byte[] pngData;

		// Token: 0x04003207 RID: 12807
		public string userID = string.Empty;

		// Token: 0x04003208 RID: 12808
		public string dataID = string.Empty;

		// Token: 0x04003209 RID: 12809
		public ChaFileCustom custom;

		// Token: 0x0400320A RID: 12810
		public ChaFileCoordinate coordinate;

		// Token: 0x0400320B RID: 12811
		public ChaFileParameter parameter;

		// Token: 0x0400320C RID: 12812
		public ChaFileGameInfo gameinfo;

		// Token: 0x0400320D RID: 12813
		public ChaFileParameter2 parameter2;

		// Token: 0x0400320E RID: 12814
		public ChaFileGameInfo2 gameinfo2;

		// Token: 0x0400320F RID: 12815
		public ChaFileStatus status;

		// Token: 0x04003210 RID: 12816
		private int lastLoadErrorCode;

		// Token: 0x020007E5 RID: 2021
		public class ProductInfo
		{
			// Token: 0x04003211 RID: 12817
			public int productNo = -1;

			// Token: 0x04003212 RID: 12818
			public string tag = string.Empty;

			// Token: 0x04003213 RID: 12819
			public Version version = new Version(0, 0, 0);

			// Token: 0x04003214 RID: 12820
			public int language;

			// Token: 0x04003215 RID: 12821
			public string userID = string.Empty;

			// Token: 0x04003216 RID: 12822
			public string dataID = string.Empty;
		}
	}
}
