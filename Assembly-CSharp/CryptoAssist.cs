using System;
using System.IO;
using System.Text;

// Token: 0x020010C6 RID: 4294
public class CryptoAssist
{
	// Token: 0x06008F1B RID: 36635 RVA: 0x003B7AB4 File Offset: 0x003B5EB4
	public CryptoAssist()
	{
		this.desKey = Encoding.UTF8.GetBytes("1234567890abcdefghujklmn");
		this.desIV = Encoding.UTF8.GetBytes("12345678");
		if (this.Load(UserData.Path + "crypt/data.dat"))
		{
		}
	}

	// Token: 0x17001F07 RID: 7943
	// (get) Token: 0x06008F1C RID: 36636 RVA: 0x003B7B10 File Offset: 0x003B5F10
	public byte[] Key
	{
		get
		{
			return this.desKey;
		}
	}

	// Token: 0x17001F08 RID: 7944
	// (get) Token: 0x06008F1D RID: 36637 RVA: 0x003B7B18 File Offset: 0x003B5F18
	public byte[] IV
	{
		get
		{
			return this.desIV;
		}
	}

	// Token: 0x06008F1E RID: 36638 RVA: 0x003B7B20 File Offset: 0x003B5F20
	public bool Load(string fileName)
	{
		bool result = false;
		if (File.Exists(fileName))
		{
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				byte[] array = new byte[0];
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				int i = 0;
				int num = 24;
				while (i < num)
				{
					byte[] array2 = this.desKey;
					int num2 = i;
					array2[num2] += array[i];
					i++;
				}
				num += 8;
				int num3 = 0;
				while (i < num)
				{
					byte[] array3 = this.desIV;
					int num4 = num3;
					array3[num4] += array[i];
					i++;
					num3++;
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x04007397 RID: 29591
	private byte[] desKey;

	// Token: 0x04007398 RID: 29592
	private byte[] desIV;
}
