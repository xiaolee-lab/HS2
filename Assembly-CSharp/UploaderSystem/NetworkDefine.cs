using System;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x02001013 RID: 4115
	public static class NetworkDefine
	{
		// Token: 0x04007088 RID: 28808
		public const string CacheFileMark = "【CacheFile】";

		// Token: 0x04007089 RID: 28809
		public const int CacheFileVersion = 100;

		// Token: 0x0400708A RID: 28810
		public static readonly Version NetInfoVersion = new Version("1.0.0");

		// Token: 0x0400708B RID: 28811
		public const string CacheSettingPath = "cache/cachesetting.dat";

		// Token: 0x0400708C RID: 28812
		public const string CacheCharaDir = "cache/chara/";

		// Token: 0x0400708D RID: 28813
		public const string CacheHousingDir = "cache/housing/";

		// Token: 0x0400708E RID: 28814
		public const string cryptPW = "aisyoujyo";

		// Token: 0x0400708F RID: 28815
		public const string cryptSALT = "phpaddress";

		// Token: 0x04007090 RID: 28816
		public const string AIS_Check_URLFile = "ais_check_url.dat";

		// Token: 0x04007091 RID: 28817
		public const string AIS_Version_URLFile = "ais_version_url.dat";

		// Token: 0x04007092 RID: 28818
		public const string AIS_System_URLFile = "ais_system_url.dat";

		// Token: 0x04007093 RID: 28819
		public const string AIS_UploadChara_URLFile = "ais_uploadChara_url.dat";

		// Token: 0x04007094 RID: 28820
		public const string AIS_UploadHousing_URLFile = "ais_uploadHousing_url.dat";

		// Token: 0x04007095 RID: 28821
		public static readonly Color colorWhite = new Color(0.9215686f, 0.8862745f, 0.8431373f);
	}
}
