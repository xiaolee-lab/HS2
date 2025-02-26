using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Illusion.Extensions;
using Microsoft.Win32;
using UnityEngine;

// Token: 0x020011B8 RID: 4536
public static class YS_Assist
{
	// Token: 0x060094CB RID: 38091 RVA: 0x003D5E44 File Offset: 0x003D4244
	public static T DeepCopyWithSerializationSurrogate<T>(T target) where T : ISerializationSurrogate
	{
		T result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			SurrogateSelector surrogateSelector = new SurrogateSelector();
			StreamingContext context = new StreamingContext(StreamingContextStates.All);
			surrogateSelector.AddSurrogate(typeof(T), context, target);
			binaryFormatter.SurrogateSelector = surrogateSelector;
			try
			{
				binaryFormatter.Serialize(memoryStream, target);
				memoryStream.Position = 0L;
				result = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			finally
			{
				memoryStream.Close();
			}
		}
		return result;
	}

	// Token: 0x060094CC RID: 38092 RVA: 0x003D5EEC File Offset: 0x003D42EC
	public static bool CheckFlagsList(List<bool> lstFlags)
	{
		int count = lstFlags.Count;
		for (int i = 0; i < count; i++)
		{
			if (!lstFlags[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060094CD RID: 38093 RVA: 0x003D5F24 File Offset: 0x003D4324
	public static bool SetActiveControl(GameObject obj, List<bool> lstFlags)
	{
		if (null == obj)
		{
			return false;
		}
		int count = lstFlags.Count;
		bool active = true;
		for (int i = 0; i < count; i++)
		{
			if (!lstFlags[i])
			{
				active = false;
				break;
			}
		}
		return obj.SetActiveIfDifferent(active);
	}

	// Token: 0x060094CE RID: 38094 RVA: 0x003D5F74 File Offset: 0x003D4374
	public static bool SetActiveControl(GameObject obj, bool flag)
	{
		return !(null == obj) && obj.SetActiveIfDifferent(flag);
	}

	// Token: 0x060094CF RID: 38095 RVA: 0x003D5F8C File Offset: 0x003D438C
	public static void GetListString(string text, out string[,] data)
	{
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		int num = array.Length;
		if (num != 0 && array[num - 1].Trim() == string.Empty)
		{
			num--;
		}
		string[] array2 = array[0].Split(new char[]
		{
			'\t'
		});
		int num2 = array2.Length;
		if (num2 != 0 && array2[num2 - 1].Trim() == string.Empty)
		{
			num2--;
		}
		data = new string[num, num2];
		for (int i = 0; i < num; i++)
		{
			string[] array3 = array[i].Split(new char[]
			{
				'\t'
			});
			for (int j = 0; j < array3.Length; j++)
			{
				if (j >= num2)
				{
					break;
				}
				data[i, j] = array3[j];
			}
			data[i, array3.Length - 1] = data[i, array3.Length - 1].Replace("\r", string.Empty).Replace("\n", string.Empty);
		}
	}

	// Token: 0x060094D0 RID: 38096 RVA: 0x003D60B4 File Offset: 0x003D44B4
	public static void BitRevBytes(byte[] data, int startPos)
	{
		int num = startPos % YS_Assist.tblRevCode.Length;
		for (int i = 0; i < data.Length; i++)
		{
			int num2 = i;
			data[num2] ^= YS_Assist.tblRevCode[num];
			num = (num + 1) % YS_Assist.tblRevCode.Length;
		}
	}

	// Token: 0x060094D1 RID: 38097 RVA: 0x003D6100 File Offset: 0x003D4500
	public static string Convert62StringFromInt32(int num)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (num < 0)
		{
			num *= -1;
			stringBuilder.Append("0");
		}
		while (num >= 62)
		{
			stringBuilder.Append(YS_Assist.tbl62[num % 62]);
			num /= 62;
		}
		stringBuilder.Append(YS_Assist.tbl62[num]);
		StringBuilder stringBuilder2 = new StringBuilder();
		for (int i = stringBuilder.Length - 1; i >= 0; i--)
		{
			stringBuilder2.Append(stringBuilder[i]);
		}
		return stringBuilder2.ToString();
	}

	// Token: 0x060094D2 RID: 38098 RVA: 0x003D6190 File Offset: 0x003D4590
	public static string Convert36StringFromInt32(int num)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (num < 0)
		{
			num *= -1;
			stringBuilder.Append("0");
		}
		while (num >= 36)
		{
			stringBuilder.Append(YS_Assist.tbl36[num % 36]);
			num /= 36;
		}
		stringBuilder.Append(YS_Assist.tbl36[num]);
		StringBuilder stringBuilder2 = new StringBuilder();
		for (int i = stringBuilder.Length - 1; i >= 0; i--)
		{
			stringBuilder2.Append(stringBuilder[i]);
		}
		return stringBuilder2.ToString();
	}

	// Token: 0x060094D3 RID: 38099 RVA: 0x003D6220 File Offset: 0x003D4620
	public static string GenerateRandomNumber(int length)
	{
		StringBuilder stringBuilder = new StringBuilder(length);
		byte[] array = new byte[length];
		RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
		rngcryptoServiceProvider.GetBytes(array);
		for (int i = 0; i < length; i++)
		{
			int value = (int)(array[i] % 10);
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060094D4 RID: 38100 RVA: 0x003D6274 File Offset: 0x003D4674
	public static string GeneratePassword36(int length)
	{
		StringBuilder stringBuilder = new StringBuilder(length);
		byte[] array = new byte[length];
		RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
		rngcryptoServiceProvider.GetBytes(array);
		for (int i = 0; i < length; i++)
		{
			int index = (int)array[i] % YS_Assist.passwordChars36.Length;
			char value = YS_Assist.passwordChars36[index];
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060094D5 RID: 38101 RVA: 0x003D62DC File Offset: 0x003D46DC
	public static string GeneratePassword62(int length)
	{
		StringBuilder stringBuilder = new StringBuilder(length);
		byte[] array = new byte[length];
		RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
		rngcryptoServiceProvider.GetBytes(array);
		for (int i = 0; i < length; i++)
		{
			int index = (int)array[i] % YS_Assist.passwordChars62.Length;
			char value = YS_Assist.passwordChars62[index];
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060094D6 RID: 38102 RVA: 0x003D6344 File Offset: 0x003D4744
	public static byte[] CreateSha256(string planeStr, string key)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(planeStr);
		byte[] bytes2 = Encoding.UTF8.GetBytes(key);
		HMACSHA256 hmacsha = new HMACSHA256(bytes2);
		return hmacsha.ComputeHash(bytes);
	}

	// Token: 0x060094D7 RID: 38103 RVA: 0x003D6378 File Offset: 0x003D4778
	public static byte[] CreateSha256(byte[] data, string key)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(key);
		HMACSHA256 hmacsha = new HMACSHA256(bytes);
		return hmacsha.ComputeHash(data);
	}

	// Token: 0x060094D8 RID: 38104 RVA: 0x003D63A0 File Offset: 0x003D47A0
	public static string ConvertStrX2FromBytes(byte[] data)
	{
		StringBuilder stringBuilder = new StringBuilder(256);
		foreach (byte b in data)
		{
			stringBuilder.Append(b.ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060094D9 RID: 38105 RVA: 0x003D63EC File Offset: 0x003D47EC
	public static string GetMacAddress()
	{
		string text = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		if (allNetworkInterfaces != null)
		{
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
				byte[] array2 = null;
				if (physicalAddress != null)
				{
					array2 = physicalAddress.GetAddressBytes();
				}
				if (array2 != null && array2.Length == 6)
				{
					text += physicalAddress.ToString();
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x060094DA RID: 38106 RVA: 0x003D646C File Offset: 0x003D486C
	public static string CreateIrregularString(string str, bool bitRand = false)
	{
		if (str.IsNullOrEmpty())
		{
			return str;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		int startPos = 7;
		if (bitRand)
		{
			startPos = UnityEngine.Random.Range(0, YS_Assist.tblRevCode.Length - 1);
		}
		YS_Assist.BitRevBytes(bytes, startPos);
		int num = bytes.Length / 4;
		int num2 = bytes.Length % 4;
		if (num2 != 0)
		{
			int num3 = 4 - num2;
			byte[] sourceArray = new byte[num3];
			int num4 = bytes.Length;
			Array.Resize<byte>(ref bytes, num4 + num3);
			Array.Copy(sourceArray, 0, bytes, num4, num3);
			num++;
		}
		StringBuilder stringBuilder = new StringBuilder(str.Length);
		for (int i = 0; i < num; i++)
		{
			stringBuilder.Append(YS_Assist.Convert62StringFromInt32(BitConverter.ToInt32(bytes, i * 4)));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060094DB RID: 38107 RVA: 0x003D6534 File Offset: 0x003D4934
	public static string CreateIrregularStringFromMacAddress()
	{
		StringBuilder stringBuilder = new StringBuilder(16);
		stringBuilder.Append(YS_Assist.GetMacAddress());
		if (string.Empty == stringBuilder.ToString())
		{
			return string.Empty;
		}
		return YS_Assist.CreateIrregularString(stringBuilder.ToString(), false);
	}

	// Token: 0x060094DC RID: 38108 RVA: 0x003D657C File Offset: 0x003D497C
	public static string CreateUUID()
	{
		return Guid.NewGuid().ToString();
	}

	// Token: 0x060094DD RID: 38109 RVA: 0x003D659C File Offset: 0x003D499C
	public static string GetStringRight(string str, int len)
	{
		if (str == null)
		{
			return string.Empty;
		}
		if (str.Length <= len)
		{
			return str;
		}
		return str.Substring(str.Length - len, len);
	}

	// Token: 0x060094DE RID: 38110 RVA: 0x003D65C7 File Offset: 0x003D49C7
	public static string GetRemoveStringRight(string str, int len)
	{
		if (str == null)
		{
			return string.Empty;
		}
		if (str.Length <= len)
		{
			return string.Empty;
		}
		return str.Substring(0, str.Length - len);
	}

	// Token: 0x060094DF RID: 38111 RVA: 0x003D65F8 File Offset: 0x003D49F8
	public static string GetRemoveStringLeft(string str, string find, bool removeFind = true)
	{
		if (str == null)
		{
			return string.Empty;
		}
		int num = str.IndexOf(find);
		if (0 >= num)
		{
			return str;
		}
		num += ((!removeFind) ? 0 : find.Length);
		return str.Substring(num);
	}

	// Token: 0x060094E0 RID: 38112 RVA: 0x003D6640 File Offset: 0x003D4A40
	public static string GetRemoveStringRight(string str, string find, bool removeFind = false)
	{
		if (str == null)
		{
			return string.Empty;
		}
		int num = str.LastIndexOf(find);
		if (0 >= num)
		{
			return str;
		}
		num += ((!removeFind) ? find.Length : 0);
		return str.Substring(0, num);
	}

	// Token: 0x060094E1 RID: 38113 RVA: 0x003D6688 File Offset: 0x003D4A88
	public static byte[] EncryptAES(byte[] srcData, string pw = "illusion", string salt = "unityunity")
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.KeySize = 128;
		rijndaelManaged.BlockSize = 128;
		byte[] bytes = Encoding.UTF8.GetBytes(salt);
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(pw, bytes);
		rfc2898DeriveBytes.IterationCount = 1000;
		rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
		rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
		byte[] result = cryptoTransform.TransformFinalBlock(srcData, 0, srcData.Length);
		cryptoTransform.Dispose();
		return result;
	}

	// Token: 0x060094E2 RID: 38114 RVA: 0x003D6714 File Offset: 0x003D4B14
	public static byte[] DecryptAES(byte[] srcData, string pw = "illusion", string salt = "unityunity")
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.KeySize = 128;
		rijndaelManaged.BlockSize = 128;
		byte[] bytes = Encoding.UTF8.GetBytes(salt);
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(pw, bytes);
		rfc2898DeriveBytes.IterationCount = 1000;
		rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
		rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
		byte[] result = cryptoTransform.TransformFinalBlock(srcData, 0, srcData.Length);
		cryptoTransform.Dispose();
		return result;
	}

	// Token: 0x060094E3 RID: 38115 RVA: 0x003D67A0 File Offset: 0x003D4BA0
	public static string GetRegistryInfoFrom(string keyName, string valueName, string baseKey = "HKEY_CURRENT_USER")
	{
		string result = string.Empty;
		try
		{
			RegistryKey registryKey;
			if (baseKey == "HKEY_CURRENT_USER")
			{
				registryKey = Registry.CurrentUser.OpenSubKey(keyName);
			}
			else
			{
				if (!(baseKey == "HKEY_LOCAL_MACHINE"))
				{
					return null;
				}
				registryKey = Registry.LocalMachine.OpenSubKey(keyName);
			}
			if (registryKey == null)
			{
				return null;
			}
			result = (string)registryKey.GetValue(valueName);
			registryKey.Close();
		}
		catch (Exception)
		{
			throw;
		}
		return result;
	}

	// Token: 0x060094E4 RID: 38116 RVA: 0x003D683C File Offset: 0x003D4C3C
	public static bool IsRegistryKey(string keyName, string baseKey = "HKEY_CURRENT_USER")
	{
		try
		{
			RegistryKey registryKey;
			if (baseKey == "HKEY_CURRENT_USER")
			{
				registryKey = Registry.CurrentUser.OpenSubKey(keyName);
			}
			else
			{
				if (!(baseKey == "HKEY_LOCAL_MACHINE"))
				{
					return false;
				}
				registryKey = Registry.LocalMachine.OpenSubKey(keyName);
			}
			if (registryKey == null)
			{
				return false;
			}
			registryKey.Close();
		}
		catch (Exception)
		{
			throw;
		}
		return true;
	}

	// Token: 0x060094E5 RID: 38117 RVA: 0x003D68C4 File Offset: 0x003D4CC4
	public static bool CompareFile(string file1, string file2)
	{
		FileStream fileStream = null;
		FileStream fileStream2 = null;
		if (file1 == file2)
		{
			return true;
		}
		try
		{
			fileStream = new FileStream(file1, FileMode.Open);
		}
		catch (FileNotFoundException)
		{
			UnityEngine.Debug.LogError(file1 + " がない");
			return false;
		}
		try
		{
			fileStream2 = new FileStream(file2, FileMode.Open);
		}
		catch (FileNotFoundException)
		{
			fileStream.Close();
			UnityEngine.Debug.LogError(file2 + " がない");
			return false;
		}
		if (fileStream.Length != fileStream2.Length)
		{
			fileStream.Close();
			fileStream2.Close();
			return false;
		}
		int num;
		int num2;
		do
		{
			num = fileStream.ReadByte();
			num2 = fileStream2.ReadByte();
		}
		while (num == num2 && num != -1);
		fileStream.Close();
		fileStream2.Close();
		return num - num2 == 0;
	}

	// Token: 0x040077A8 RID: 30632
	private static readonly string passwordChars36 = "0123456789abcdefghijklmnopqrstuvwxyz";

	// Token: 0x040077A9 RID: 30633
	private static readonly string passwordChars62 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	// Token: 0x040077AA RID: 30634
	private static readonly char[] tbl36 = new char[]
	{
		'0',
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'a',
		'b',
		'c',
		'd',
		'e',
		'f',
		'g',
		'h',
		'i',
		'j',
		'k',
		'l',
		'm',
		'n',
		'o',
		'p',
		'q',
		'r',
		's',
		't',
		'u',
		'v',
		'w',
		'x',
		'y',
		'z'
	};

	// Token: 0x040077AB RID: 30635
	private static readonly char[] tbl62 = new char[]
	{
		'0',
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'a',
		'b',
		'c',
		'd',
		'e',
		'f',
		'g',
		'h',
		'i',
		'j',
		'k',
		'l',
		'm',
		'n',
		'o',
		'p',
		'q',
		'r',
		's',
		't',
		'u',
		'v',
		'w',
		'x',
		'y',
		'z',
		'A',
		'B',
		'C',
		'D',
		'E',
		'F',
		'G',
		'H',
		'I',
		'J',
		'K',
		'L',
		'M',
		'N',
		'O',
		'P',
		'Q',
		'R',
		'S',
		'T',
		'U',
		'V',
		'W',
		'X',
		'Y',
		'Z'
	};

	// Token: 0x040077AC RID: 30636
	private static readonly byte[] tblRevCode = new byte[]
	{
		50,
		112,
		114,
		160,
		140,
		152,
		202,
		10,
		235,
		9,
		198,
		113,
		78,
		208,
		182,
		154,
		247,
		249,
		64,
		243,
		232,
		102,
		184,
		130,
		196,
		33,
		149,
		171,
		62,
		235,
		124,
		183,
		193,
		189,
		168,
		165,
		243,
		117,
		48,
		23,
		16,
		114,
		192,
		105,
		122,
		253,
		206,
		143,
		240,
		183,
		150,
		127,
		115,
		117,
		19,
		135,
		140,
		187,
		73,
		133,
		254,
		231,
		48,
		92,
		205,
		127,
		122,
		237,
		250,
		212,
		27,
		92,
		153,
		237,
		54,
		161,
		135,
		216,
		104,
		10,
		60,
		128,
		97,
		33,
		47,
		124,
		18,
		218,
		168,
		133,
		217,
		249,
		188,
		179,
		198,
		104,
		68,
		229,
		179,
		61,
		10,
		22,
		10,
		183,
		8,
		189,
		74,
		86,
		107,
		47,
		230,
		233,
		158,
		241,
		27,
		85,
		198,
		164,
		151,
		135,
		148,
		4,
		24,
		172,
		122,
		214,
		18,
		140
	};
}
