using System;
using System.Collections.Generic;
using UnityEx;

namespace AIProject.Definitions
{
	// Token: 0x0200094F RID: 2383
	public static class Map
	{
		// Token: 0x0600428D RID: 17037 RVA: 0x001A2C2C File Offset: 0x001A102C
		// Note: this type is marked as 'beforefieldinit'.
		static Map()
		{
			Dictionary<TimeZone, string> dictionary = new Dictionary<TimeZone, string>();
			dictionary[TimeZone.Morning] = "朝";
			dictionary[TimeZone.Day] = "昼";
			dictionary[TimeZone.Night] = "夜";
			Map.TimeZoneNameTable = dictionary;
			Dictionary<Weather, string> dictionary2 = new Dictionary<Weather, string>();
			dictionary2[Weather.Clear] = "快晴";
			dictionary2[Weather.Cloud1] = "晴れ（少量）";
			dictionary2[Weather.Cloud2] = "晴れ（大量）";
			dictionary2[Weather.Cloud3] = "曇り";
			dictionary2[Weather.Cloud4] = "曇天";
			dictionary2[Weather.Rain] = "雨";
			dictionary2[Weather.Storm] = "大雨";
			dictionary2[Weather.Fog] = "霧";
			Map.WeatherNameTable = dictionary2;
			Dictionary<Temperature, string> dictionary3 = new Dictionary<Temperature, string>();
			dictionary3[Temperature.Normal] = "普通";
			dictionary3[Temperature.Hot] = "暑い";
			dictionary3[Temperature.Cold] = "寒い";
			Map.TemperatureNameTable = dictionary3;
			Map.EndTimeOfDay = new DateTime(1, 1, 1, 23, 59, 59);
			Map.AreaList = new UnityEx.ValueTuple<int, string, MapArea.AreaType>[]
			{
				new UnityEx.ValueTuple<int, string, MapArea.AreaType>(0, "NormalArea", MapArea.AreaType.Normal),
				new UnityEx.ValueTuple<int, string, MapArea.AreaType>(1, "UnderRoofArea", MapArea.AreaType.Indoor),
				new UnityEx.ValueTuple<int, string, MapArea.AreaType>(2, "PrivateRoom", MapArea.AreaType.Private)
			};
		}

		// Token: 0x04003F24 RID: 16164
		public const int PlayerID = -99;

		// Token: 0x04003F25 RID: 16165
		public const int MerchantID = -90;

		// Token: 0x04003F26 RID: 16166
		public static Dictionary<TimeZone, string> TimeZoneNameTable;

		// Token: 0x04003F27 RID: 16167
		public static Dictionary<Weather, string> WeatherNameTable;

		// Token: 0x04003F28 RID: 16168
		public static Dictionary<Temperature, string> TemperatureNameTable;

		// Token: 0x04003F29 RID: 16169
		public static readonly DateTime EndTimeOfDay;

		// Token: 0x04003F2A RID: 16170
		public static readonly UnityEx.ValueTuple<int, string, MapArea.AreaType>[] AreaList;

		// Token: 0x02000950 RID: 2384
		public enum FootStepSE
		{
			// Token: 0x04003F2C RID: 16172
			Sand,
			// Token: 0x04003F2D RID: 16173
			Soil,
			// Token: 0x04003F2E RID: 16174
			Grass,
			// Token: 0x04003F2F RID: 16175
			Rock,
			// Token: 0x04003F30 RID: 16176
			Metal,
			// Token: 0x04003F31 RID: 16177
			Wood,
			// Token: 0x04003F32 RID: 16178
			WoodFlooring,
			// Token: 0x04003F33 RID: 16179
			Wet,
			// Token: 0x04003F34 RID: 16180
			Water,
			// Token: 0x04003F35 RID: 16181
			Snow
		}
	}
}
