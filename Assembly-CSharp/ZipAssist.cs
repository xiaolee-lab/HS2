using System;
using System.IO;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;

// Token: 0x0200100C RID: 4108
public class ZipAssist
{
	// Token: 0x06008A15 RID: 35349 RVA: 0x003A1E06 File Offset: 0x003A0206
	public ZipAssist()
	{
		this.progress = 0f;
		this.cntSaved = 0;
		this.cntTotal = 0;
	}

	// Token: 0x17001E28 RID: 7720
	// (get) Token: 0x06008A16 RID: 35350 RVA: 0x003A1E27 File Offset: 0x003A0227
	// (set) Token: 0x06008A17 RID: 35351 RVA: 0x003A1E2F File Offset: 0x003A022F
	public float progress { get; private set; }

	// Token: 0x17001E29 RID: 7721
	// (get) Token: 0x06008A18 RID: 35352 RVA: 0x003A1E38 File Offset: 0x003A0238
	// (set) Token: 0x06008A19 RID: 35353 RVA: 0x003A1E40 File Offset: 0x003A0240
	public int cntSaved { get; private set; }

	// Token: 0x17001E2A RID: 7722
	// (get) Token: 0x06008A1A RID: 35354 RVA: 0x003A1E49 File Offset: 0x003A0249
	// (set) Token: 0x06008A1B RID: 35355 RVA: 0x003A1E51 File Offset: 0x003A0251
	public int cntTotal { get; private set; }

	// Token: 0x06008A1C RID: 35356 RVA: 0x003A1E5A File Offset: 0x003A025A
	public void SaveProgress(object sender, SaveProgressEventArgs e)
	{
	}

	// Token: 0x06008A1D RID: 35357 RVA: 0x003A1E5C File Offset: 0x003A025C
	public byte[] SaveUnzipFile(byte[] srcBytes, EventHandler<SaveProgressEventArgs> callBack = null)
	{
		byte[] result = null;
		try
		{
			using (MemoryStream memoryStream = new MemoryStream(srcBytes))
			{
				ReadOptions options = new ReadOptions
				{
					Encoding = Encoding.GetEncoding("shift_jis")
				};
				using (ZipFile zipFile = ZipFile.Read(memoryStream, options))
				{
					ZipEntry zipEntry = zipFile[0];
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						zipEntry.Extract(memoryStream2);
						result = memoryStream2.ToArray();
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
		return result;
	}

	// Token: 0x06008A1E RID: 35358 RVA: 0x003A1F30 File Offset: 0x003A0330
	public byte[] SaveZipBytes(byte[] srcBytes, string entryName, EventHandler<SaveProgressEventArgs> callBack = null)
	{
		byte[] result = null;
		try
		{
			using (ZipFile zipFile = new ZipFile(Encoding.GetEncoding("shift_jis")))
			{
				if (callBack != null)
				{
					zipFile.SaveProgress += callBack;
				}
				else
				{
					zipFile.SaveProgress += this.SaveProgress;
				}
				zipFile.AlternateEncodingUsage = ZipOption.Always;
				zipFile.CompressionLevel = CompressionLevel.BestCompression;
				zipFile.AddEntry(entryName, srcBytes);
				long num = (long)srcBytes.Length;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					if (num % 65536L == 0L)
					{
						zipFile.ParallelDeflateThreshold = -1L;
					}
					zipFile.Save(memoryStream);
					result = memoryStream.ToArray();
				}
			}
		}
		catch (Exception ex)
		{
		}
		return result;
	}
}
