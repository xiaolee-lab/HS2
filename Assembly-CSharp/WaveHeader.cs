using System;
using System.IO;
using System.Text;

// Token: 0x02001139 RID: 4409
public class WaveHeader
{
	// Token: 0x17001F58 RID: 8024
	// (get) Token: 0x060091EC RID: 37356 RVA: 0x003C9B35 File Offset: 0x003C7F35
	// (set) Token: 0x060091ED RID: 37357 RVA: 0x003C9B3D File Offset: 0x003C7F3D
	public int FileSize { get; private set; }

	// Token: 0x17001F59 RID: 8025
	// (get) Token: 0x060091EE RID: 37358 RVA: 0x003C9B46 File Offset: 0x003C7F46
	// (set) Token: 0x060091EF RID: 37359 RVA: 0x003C9B4E File Offset: 0x003C7F4E
	public string RIFF { get; private set; }

	// Token: 0x17001F5A RID: 8026
	// (get) Token: 0x060091F0 RID: 37360 RVA: 0x003C9B57 File Offset: 0x003C7F57
	// (set) Token: 0x060091F1 RID: 37361 RVA: 0x003C9B5F File Offset: 0x003C7F5F
	public int Size { get; private set; }

	// Token: 0x17001F5B RID: 8027
	// (get) Token: 0x060091F2 RID: 37362 RVA: 0x003C9B68 File Offset: 0x003C7F68
	// (set) Token: 0x060091F3 RID: 37363 RVA: 0x003C9B70 File Offset: 0x003C7F70
	public string WAVE { get; private set; }

	// Token: 0x17001F5C RID: 8028
	// (get) Token: 0x060091F4 RID: 37364 RVA: 0x003C9B79 File Offset: 0x003C7F79
	// (set) Token: 0x060091F5 RID: 37365 RVA: 0x003C9B81 File Offset: 0x003C7F81
	public string FMT { get; private set; }

	// Token: 0x17001F5D RID: 8029
	// (get) Token: 0x060091F6 RID: 37366 RVA: 0x003C9B8A File Offset: 0x003C7F8A
	// (set) Token: 0x060091F7 RID: 37367 RVA: 0x003C9B92 File Offset: 0x003C7F92
	public int FmtChunkSize { get; private set; }

	// Token: 0x17001F5E RID: 8030
	// (get) Token: 0x060091F8 RID: 37368 RVA: 0x003C9B9B File Offset: 0x003C7F9B
	// (set) Token: 0x060091F9 RID: 37369 RVA: 0x003C9BA3 File Offset: 0x003C7FA3
	public short FormatId { get; private set; }

	// Token: 0x17001F5F RID: 8031
	// (get) Token: 0x060091FA RID: 37370 RVA: 0x003C9BAC File Offset: 0x003C7FAC
	// (set) Token: 0x060091FB RID: 37371 RVA: 0x003C9BB4 File Offset: 0x003C7FB4
	public short Channels { get; private set; }

	// Token: 0x17001F60 RID: 8032
	// (get) Token: 0x060091FC RID: 37372 RVA: 0x003C9BBD File Offset: 0x003C7FBD
	// (set) Token: 0x060091FD RID: 37373 RVA: 0x003C9BC5 File Offset: 0x003C7FC5
	public int Frequency { get; private set; }

	// Token: 0x17001F61 RID: 8033
	// (get) Token: 0x060091FE RID: 37374 RVA: 0x003C9BCE File Offset: 0x003C7FCE
	// (set) Token: 0x060091FF RID: 37375 RVA: 0x003C9BD6 File Offset: 0x003C7FD6
	public int DataSpeed { get; private set; }

	// Token: 0x17001F62 RID: 8034
	// (get) Token: 0x06009200 RID: 37376 RVA: 0x003C9BDF File Offset: 0x003C7FDF
	// (set) Token: 0x06009201 RID: 37377 RVA: 0x003C9BE7 File Offset: 0x003C7FE7
	public short BlockSize { get; private set; }

	// Token: 0x17001F63 RID: 8035
	// (get) Token: 0x06009202 RID: 37378 RVA: 0x003C9BF0 File Offset: 0x003C7FF0
	// (set) Token: 0x06009203 RID: 37379 RVA: 0x003C9BF8 File Offset: 0x003C7FF8
	public short BitPerSample { get; private set; }

	// Token: 0x17001F64 RID: 8036
	// (get) Token: 0x06009204 RID: 37380 RVA: 0x003C9C01 File Offset: 0x003C8001
	// (set) Token: 0x06009205 RID: 37381 RVA: 0x003C9C09 File Offset: 0x003C8009
	public string DATA { get; private set; }

	// Token: 0x17001F65 RID: 8037
	// (get) Token: 0x06009206 RID: 37382 RVA: 0x003C9C12 File Offset: 0x003C8012
	// (set) Token: 0x06009207 RID: 37383 RVA: 0x003C9C1A File Offset: 0x003C801A
	public int TrueWavBufSize { get; private set; }

	// Token: 0x17001F66 RID: 8038
	// (get) Token: 0x06009208 RID: 37384 RVA: 0x003C9C23 File Offset: 0x003C8023
	// (set) Token: 0x06009209 RID: 37385 RVA: 0x003C9C2B File Offset: 0x003C802B
	public int TrueWavBufIndex { get; private set; }

	// Token: 0x17001F67 RID: 8039
	// (get) Token: 0x0600920A RID: 37386 RVA: 0x003C9C34 File Offset: 0x003C8034
	// (set) Token: 0x0600920B RID: 37387 RVA: 0x003C9C3C File Offset: 0x003C803C
	public int TrueSamples { get; private set; }

	// Token: 0x0600920C RID: 37388 RVA: 0x003C9C48 File Offset: 0x003C8048
	public static WaveHeader ReadWaveHeader(string path)
	{
		WaveHeader result;
		using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			result = WaveHeader.ReadWaveHeader(fileStream);
		}
		return result;
	}

	// Token: 0x0600920D RID: 37389 RVA: 0x003C9C8C File Offset: 0x003C808C
	public static WaveHeader ReadWaveHeader(Stream stream)
	{
		if (stream == null)
		{
			return null;
		}
		WaveHeader result;
		using (BinaryReader binaryReader = new BinaryReader(stream))
		{
			result = WaveHeader.ReadWaveHeader(binaryReader);
		}
		return result;
	}

	// Token: 0x0600920E RID: 37390 RVA: 0x003C9CD4 File Offset: 0x003C80D4
	public static WaveHeader ReadWaveHeader(BinaryReader reader)
	{
		WaveHeader waveHeader = new WaveHeader();
		waveHeader.RIFF = Encoding.ASCII.GetString(reader.ReadBytes(4));
		waveHeader.Size = reader.ReadInt32();
		waveHeader.WAVE = Encoding.ASCII.GetString(reader.ReadBytes(4));
		if (waveHeader.RIFF.ToUpper() != "RIFF" || waveHeader.WAVE.ToUpper() != "WAVE")
		{
			return null;
		}
		while (waveHeader.FMT == null || waveHeader.FMT.ToLower() != "fmt")
		{
			waveHeader.FMT = Encoding.ASCII.GetString(reader.ReadBytes(4));
			if (waveHeader.FMT.ToLower().Trim() == "fmt")
			{
				break;
			}
			uint num = reader.ReadUInt32();
			reader.BaseStream.Seek((long)((ulong)num), SeekOrigin.Current);
		}
		waveHeader.FmtChunkSize = reader.ReadInt32();
		waveHeader.FormatId = reader.ReadInt16();
		waveHeader.Channels = reader.ReadInt16();
		waveHeader.Frequency = reader.ReadInt32();
		waveHeader.DataSpeed = reader.ReadInt32();
		waveHeader.BlockSize = reader.ReadInt16();
		waveHeader.BitPerSample = reader.ReadInt16();
		reader.BaseStream.Seek((long)(waveHeader.FmtChunkSize - 16), SeekOrigin.Current);
		while (waveHeader.DATA == null || waveHeader.DATA.ToLower() != "data")
		{
			waveHeader.DATA = Encoding.ASCII.GetString(reader.ReadBytes(4));
			if (waveHeader.DATA.ToLower() == "data")
			{
				break;
			}
			uint num2 = reader.ReadUInt32();
			reader.BaseStream.Seek((long)((ulong)num2), SeekOrigin.Current);
		}
		waveHeader.TrueWavBufSize = reader.ReadInt32();
		waveHeader.TrueSamples = waveHeader.TrueWavBufSize / (int)(waveHeader.BitPerSample / 8) / (int)waveHeader.Channels;
		waveHeader.TrueWavBufIndex = (int)reader.BaseStream.Position;
		reader.BaseStream.Seek(0L, SeekOrigin.Begin);
		return waveHeader;
	}
}
