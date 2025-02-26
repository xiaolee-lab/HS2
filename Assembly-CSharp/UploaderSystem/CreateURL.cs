using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x02000FEF RID: 4079
	public class CreateURL : MonoBehaviour
	{
		// Token: 0x06008950 RID: 35152 RVA: 0x003926B0 File Offset: 0x00390AB0
		public void CreateAIS_URL(int kind)
		{
			string s = string.Empty;
			string str = string.Empty;
			switch (kind)
			{
			case 0:
				s = this.AIS_Check_URL;
				str = "ais_check_url.dat";
				break;
			case 1:
				s = this.AIS_System_URL;
				str = "ais_system_url.dat";
				break;
			case 2:
				s = this.AIS_UploadChara_URL;
				str = "ais_uploadChara_url.dat";
				break;
			case 3:
				s = this.AIS_UploadHousing_URL;
				str = "ais_uploadHousing_url.dat";
				break;
			case 4:
				s = this.AIS_Version_URL;
				str = "ais_version_url.dat";
				break;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] buffer = YS_Assist.EncryptAES(bytes, "aisyoujyo", "phpaddress");
			string path = Application.dataPath + "/../DefaultData/url/" + str;
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(buffer);
				}
			}
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x003927F4 File Offset: 0x00390BF4
		public static string LoadURL(string urlFile)
		{
			byte[] array = null;
			string path = Application.dataPath + "/../DefaultData/url/" + urlFile;
			if (!File.Exists(path))
			{
				return string.Empty;
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					array = binaryReader.ReadBytes((int)fileStream.Length);
				}
			}
			if (array == null)
			{
				return string.Empty;
			}
			byte[] bytes = YS_Assist.DecryptAES(array, "aisyoujyo", "phpaddress");
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x04006F33 RID: 28467
		[Space]
		public string AIS_Check_URL = string.Empty;

		// Token: 0x04006F34 RID: 28468
		[Button("CreateAIS_URL", "AIS_Check_URL作成", new object[]
		{
			0
		})]
		public int excuteCreateAIS_Check_URL;

		// Token: 0x04006F35 RID: 28469
		[Space]
		[Space]
		public string AIS_System_URL = string.Empty;

		// Token: 0x04006F36 RID: 28470
		[Button("CreateAIS_URL", "AIS_System_URL作成", new object[]
		{
			1
		})]
		public int excuteCreateAIS_System_URL;

		// Token: 0x04006F37 RID: 28471
		[Space]
		[Space]
		public string AIS_UploadChara_URL = string.Empty;

		// Token: 0x04006F38 RID: 28472
		[Button("CreateAIS_URL", "AIS_UploadChara_URL作成", new object[]
		{
			2
		})]
		public int excuteCreateAIS_UploadChara_URL;

		// Token: 0x04006F39 RID: 28473
		[Space]
		[Space]
		public string AIS_UploadHousing_URL = string.Empty;

		// Token: 0x04006F3A RID: 28474
		[Button("CreateAIS_URL", "AIS_UploadHousing_URL作成", new object[]
		{
			3
		})]
		public int excuteCreateAIS_UploadHousing_URL;

		// Token: 0x04006F3B RID: 28475
		[Space]
		[Space]
		public string AIS_Version_URL = string.Empty;

		// Token: 0x04006F3C RID: 28476
		[Button("CreateAIS_URL", "AIS_Version_URL作成", new object[]
		{
			4
		})]
		public int excuteCreateAIS_Version_URL;
	}
}
