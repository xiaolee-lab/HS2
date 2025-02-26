using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020010C7 RID: 4295
public class AesCryptography
{
	// Token: 0x06008F20 RID: 36640 RVA: 0x003B7BF4 File Offset: 0x003B5FF4
	public byte[] Encrypt(byte[] binData)
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Padding = PaddingMode.Zeros;
		rijndaelManaged.Mode = CipherMode.CBC;
		rijndaelManaged.KeySize = 256;
		rijndaelManaged.BlockSize = 256;
		byte[] bytes = Encoding.UTF8.GetBytes("piyopiyopiyopiyopiyopiyopiyopiyo");
		byte[] bytes2 = Encoding.UTF8.GetBytes("1234567890abcdefghujklmnopqrstuv");
		ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes, bytes2);
		MemoryStream memoryStream = new MemoryStream();
		using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
		{
			cryptoStream.Write(binData, 0, binData.Length);
		}
		memoryStream.Close();
		return memoryStream.ToArray();
	}

	// Token: 0x06008F21 RID: 36641 RVA: 0x003B7CAC File Offset: 0x003B60AC
	public byte[] Decrypt(byte[] binData)
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Padding = PaddingMode.Zeros;
		rijndaelManaged.Mode = CipherMode.CBC;
		rijndaelManaged.KeySize = 256;
		rijndaelManaged.BlockSize = 256;
		byte[] bytes = Encoding.UTF8.GetBytes("piyopiyopiyopiyopiyopiyopiyopiyo");
		byte[] bytes2 = Encoding.UTF8.GetBytes("1234567890abcdefghujklmnopqrstuv");
		ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, bytes2);
		byte[] array = new byte[binData.Length];
		using (MemoryStream memoryStream = new MemoryStream(binData))
		{
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
			{
				cryptoStream.Read(array, 0, array.Length);
			}
		}
		return array;
	}

	// Token: 0x04007399 RID: 29593
	private const string AesInitVector = "1234567890abcdefghujklmnopqrstuv";

	// Token: 0x0400739A RID: 29594
	private const string AesKey = "piyopiyopiyopiyopiyopiyopiyopiyo";

	// Token: 0x0400739B RID: 29595
	private const int BlockSize = 256;

	// Token: 0x0400739C RID: 29596
	private const int KeySize = 256;
}
