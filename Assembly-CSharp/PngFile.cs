using System;
using System.IO;

// Token: 0x02001158 RID: 4440
public static class PngFile
{
	// Token: 0x060092C8 RID: 37576 RVA: 0x003CE0F2 File Offset: 0x003CC4F2
	public static long GetPngSize(BinaryReader br)
	{
		return PngFile.GetPngSize(br.BaseStream);
	}

	// Token: 0x060092C9 RID: 37577 RVA: 0x003CE100 File Offset: 0x003CC500
	public static long GetPngSize(Stream st)
	{
		if (st == null)
		{
			return 0L;
		}
		long position = st.Position;
		long result = 0L;
		try
		{
			byte[] array = new byte[8];
			byte[] array2 = new byte[]
			{
				137,
				80,
				78,
				71,
				13,
				10,
				26,
				10
			};
			st.Read(array, 0, 8);
			for (int i = 0; i < 8; i++)
			{
				if (array[i] != array2[i])
				{
					st.Seek(position, SeekOrigin.Begin);
					return 0L;
				}
			}
			bool flag = true;
			while (flag)
			{
				byte[] array3 = new byte[4];
				st.Read(array3, 0, 4);
				Array.Reverse(array3);
				int num = BitConverter.ToInt32(array3, 0);
				byte[] array4 = new byte[4];
				st.Read(array4, 0, 4);
				int num2 = BitConverter.ToInt32(array4, 0);
				if (num2 == 1145980233)
				{
					flag = false;
				}
				if ((long)(num + 4) > st.Length - st.Position)
				{
					st.Seek(position, SeekOrigin.Begin);
					return 0L;
				}
				st.Seek((long)(num + 4), SeekOrigin.Current);
			}
			result = st.Position - position;
			st.Seek(position, SeekOrigin.Begin);
		}
		catch (EndOfStreamException ex)
		{
			st.Seek(position, SeekOrigin.Begin);
			return 0L;
		}
		return result;
	}

	// Token: 0x060092CA RID: 37578 RVA: 0x003CE260 File Offset: 0x003CC660
	public static long SkipPng(Stream st)
	{
		long pngSize = PngFile.GetPngSize(st);
		st.Seek(pngSize, SeekOrigin.Current);
		return pngSize;
	}

	// Token: 0x060092CB RID: 37579 RVA: 0x003CE280 File Offset: 0x003CC680
	public static long SkipPng(BinaryReader br)
	{
		long pngSize = PngFile.GetPngSize(br);
		br.BaseStream.Seek(pngSize, SeekOrigin.Current);
		return pngSize;
	}

	// Token: 0x060092CC RID: 37580 RVA: 0x003CE2A4 File Offset: 0x003CC6A4
	public static byte[] LoadPngBytes(string path)
	{
		byte[] result;
		using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
		{
			result = PngFile.LoadPngBytes(fileStream);
		}
		return result;
	}

	// Token: 0x060092CD RID: 37581 RVA: 0x003CE2E4 File Offset: 0x003CC6E4
	public static byte[] LoadPngBytes(Stream st)
	{
		byte[] result;
		using (BinaryReader binaryReader = new BinaryReader(st))
		{
			result = PngFile.LoadPngBytes(binaryReader);
		}
		return result;
	}

	// Token: 0x060092CE RID: 37582 RVA: 0x003CE324 File Offset: 0x003CC724
	public static byte[] LoadPngBytes(BinaryReader br)
	{
		long pngSize = PngFile.GetPngSize(br);
		if (pngSize == 0L)
		{
			return null;
		}
		return br.ReadBytes((int)pngSize);
	}
}
