using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000F6D RID: 3949
	public class EnvironmentProfile : ScriptableObject
	{
		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06008378 RID: 33656 RVA: 0x00371014 File Offset: 0x0036F414
		// (set) Token: 0x06008379 RID: 33657 RVA: 0x0037101C File Offset: 0x0036F41C
		public float DayLengthInMinute
		{
			get
			{
				return this._dayLengthInMinute;
			}
			set
			{
				this._dayLengthInMinute = value;
			}
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x0600837A RID: 33658 RVA: 0x00371025 File Offset: 0x0036F425
		public DateTime MorningTime
		{
			[CompilerGenerated]
			get
			{
				return this._morningTime.Time;
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x0600837B RID: 33659 RVA: 0x00371032 File Offset: 0x0036F432
		public DateTime DayTime
		{
			[CompilerGenerated]
			get
			{
				return this._dayTime.Time;
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x0600837C RID: 33660 RVA: 0x0037103F File Offset: 0x0036F43F
		public DateTime NightTime
		{
			[CompilerGenerated]
			get
			{
				return this._nightTime.Time;
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x0600837D RID: 33661 RVA: 0x0037104C File Offset: 0x0036F44C
		public Threshold WeatherDurationMinThreshold
		{
			[CompilerGenerated]
			get
			{
				return this._weatherDurationMinThreshold;
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x0600837E RID: 33662 RVA: 0x00371054 File Offset: 0x0036F454
		public float WeatherDurationMax
		{
			[CompilerGenerated]
			get
			{
				return this._weatherDurationMax;
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x0600837F RID: 33663 RVA: 0x0037105C File Offset: 0x0036F45C
		public float RainIntensity
		{
			[CompilerGenerated]
			get
			{
				return this._rainIntensity;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06008380 RID: 33664 RVA: 0x00371064 File Offset: 0x0036F464
		public float RainMistThreshold
		{
			[CompilerGenerated]
			get
			{
				return this._rainMistThreshold;
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06008381 RID: 33665 RVA: 0x0037106C File Offset: 0x0036F46C
		public float WindSoundVolumeModifier
		{
			[CompilerGenerated]
			get
			{
				return this._windSoundVolumeModifier;
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06008382 RID: 33666 RVA: 0x00371074 File Offset: 0x0036F474
		public Threshold WindSpeedRange
		{
			[CompilerGenerated]
			get
			{
				return this._windSpeedRange;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06008383 RID: 33667 RVA: 0x0037107C File Offset: 0x0036F47C
		public float WindSoundMultiplier
		{
			[CompilerGenerated]
			get
			{
				return this._windSoundMultiplier;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06008384 RID: 33668 RVA: 0x00371084 File Offset: 0x0036F484
		public Threshold WindChangeIntervalThreshold
		{
			[CompilerGenerated]
			get
			{
				return this._windChangeIntervalThreshold;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x06008385 RID: 33669 RVA: 0x0037108C File Offset: 0x0036F48C
		public EnvironmentProfile.TemperatureBorderInfo TemperatureBorder
		{
			[CompilerGenerated]
			get
			{
				return this._temperatureBorder;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x06008386 RID: 33670 RVA: 0x00371094 File Offset: 0x0036F494
		public int SearchCount
		{
			[CompilerGenerated]
			get
			{
				return this._searchCount;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x06008387 RID: 33671 RVA: 0x0037109C File Offset: 0x0036F49C
		public float SearchCoolTimeDuration
		{
			[CompilerGenerated]
			get
			{
				return this._searchCoolTimeDuration;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06008388 RID: 33672 RVA: 0x003710A4 File Offset: 0x0036F4A4
		public float RuntimeMapItemLifeTime
		{
			[CompilerGenerated]
			get
			{
				return this._runtimeMapItemLifeTime;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06008389 RID: 33673 RVA: 0x003710AC File Offset: 0x0036F4AC
		public DateTime LightMorningTime
		{
			[CompilerGenerated]
			get
			{
				return this._lightMorningTime.Time;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x0600838A RID: 33674 RVA: 0x003710B9 File Offset: 0x0036F4B9
		public DateTime LightDayTime
		{
			[CompilerGenerated]
			get
			{
				return this._lightDayTime.Time;
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x0600838B RID: 33675 RVA: 0x003710C6 File Offset: 0x0036F4C6
		public DateTime LightNightTime
		{
			[CompilerGenerated]
			get
			{
				return this._lightNightTime.Time;
			}
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x0600838C RID: 33676 RVA: 0x003710D3 File Offset: 0x0036F4D3
		public EnvironmentProfile.WeatherTemperatureRangeDayInfo WeatherTemperatureRange
		{
			[CompilerGenerated]
			get
			{
				return this._weatherTemperatureRange;
			}
		}

		// Token: 0x040069E1 RID: 27105
		[SerializeField]
		[PropertyRange(1.0, 120.0)]
		private float _dayLengthInMinute = 50f;

		// Token: 0x040069E2 RID: 27106
		[SerializeField]
		[DisableInPlayMode]
		private EnvironmentSimulator.DateTimeSerialization _morningTime = new EnvironmentSimulator.DateTimeSerialization(new DateTime(1, 1, 1, 6, 0, 0));

		// Token: 0x040069E3 RID: 27107
		[SerializeField]
		[DisableInPlayMode]
		private EnvironmentSimulator.DateTimeSerialization _dayTime = new EnvironmentSimulator.DateTimeSerialization(new DateTime(1, 1, 1, 10, 0, 0));

		// Token: 0x040069E4 RID: 27108
		[SerializeField]
		[DisableInPlayMode]
		private EnvironmentSimulator.DateTimeSerialization _nightTime = new EnvironmentSimulator.DateTimeSerialization(new DateTime(1, 1, 1, 18, 0, 0));

		// Token: 0x040069E5 RID: 27109
		[SerializeField]
		private Threshold _weatherDurationMinThreshold = new Threshold(1f, 3f);

		// Token: 0x040069E6 RID: 27110
		[SerializeField]
		private float _weatherDurationMax = 4f;

		// Token: 0x040069E7 RID: 27111
		[SerializeField]
		[PropertyRange(0.0, 1.0)]
		private float _rainIntensity = 1f;

		// Token: 0x040069E8 RID: 27112
		[SerializeField]
		[PropertyRange(0.0, 1.0)]
		private float _rainMistThreshold = 1f;

		// Token: 0x040069E9 RID: 27113
		[SerializeField]
		private float _windSoundVolumeModifier = 0.5f;

		// Token: 0x040069EA RID: 27114
		[SerializeField]
		private Threshold _windSpeedRange = new Threshold(50f, 500f);

		// Token: 0x040069EB RID: 27115
		[SerializeField]
		[MinValue(1.0)]
		private float _windSoundMultiplier = 500f;

		// Token: 0x040069EC RID: 27116
		[SerializeField]
		private Threshold _windChangeIntervalThreshold = new Threshold(5f, 30f);

		// Token: 0x040069ED RID: 27117
		[SerializeField]
		private EnvironmentProfile.TemperatureBorderInfo _temperatureBorder = default(EnvironmentProfile.TemperatureBorderInfo);

		// Token: 0x040069EE RID: 27118
		[SerializeField]
		private int _searchCount;

		// Token: 0x040069EF RID: 27119
		[SerializeField]
		private float _searchCoolTimeDuration = 1f;

		// Token: 0x040069F0 RID: 27120
		[SerializeField]
		private float _runtimeMapItemLifeTime = 1f;

		// Token: 0x040069F1 RID: 27121
		[SerializeField]
		private EnvironmentSimulator.DateTimeSerialization _lightMorningTime = new EnvironmentSimulator.DateTimeSerialization(new DateTime(1, 1, 1, 7, 0, 0));

		// Token: 0x040069F2 RID: 27122
		[SerializeField]
		private EnvironmentSimulator.DateTimeSerialization _lightDayTime = new EnvironmentSimulator.DateTimeSerialization(new DateTime(1, 1, 1, 15, 0, 0));

		// Token: 0x040069F3 RID: 27123
		[SerializeField]
		private EnvironmentSimulator.DateTimeSerialization _lightNightTime = new EnvironmentSimulator.DateTimeSerialization(new DateTime(1, 1, 1, 18, 0, 0));

		// Token: 0x040069F4 RID: 27124
		[SerializeField]
		private EnvironmentProfile.WeatherTemperatureRangeDayInfo _weatherTemperatureRange = default(EnvironmentProfile.WeatherTemperatureRangeDayInfo);

		// Token: 0x02000F6E RID: 3950
		[Serializable]
		public struct TemperatureBorderInfo
		{
			// Token: 0x17001BEA RID: 7146
			// (get) Token: 0x0600838D RID: 33677 RVA: 0x003710DB File Offset: 0x0036F4DB
			public int MinDegree
			{
				[CompilerGenerated]
				get
				{
					return this._minDegree;
				}
			}

			// Token: 0x17001BEB RID: 7147
			// (get) Token: 0x0600838E RID: 33678 RVA: 0x003710E3 File Offset: 0x0036F4E3
			public int LowBorder
			{
				[CompilerGenerated]
				get
				{
					return this._lowBorder;
				}
			}

			// Token: 0x17001BEC RID: 7148
			// (get) Token: 0x0600838F RID: 33679 RVA: 0x003710EB File Offset: 0x0036F4EB
			public int HighBorder
			{
				[CompilerGenerated]
				get
				{
					return this._highBorder;
				}
			}

			// Token: 0x17001BED RID: 7149
			// (get) Token: 0x06008390 RID: 33680 RVA: 0x003710F3 File Offset: 0x0036F4F3
			public int MaxDegree
			{
				[CompilerGenerated]
				get
				{
					return this._maxDegree;
				}
			}

			// Token: 0x17001BEE RID: 7150
			// (get) Token: 0x06008391 RID: 33681 RVA: 0x003710FB File Offset: 0x0036F4FB
			public int TotalDegree
			{
				[CompilerGenerated]
				get
				{
					return this._maxDegree - this._minDegree;
				}
			}

			// Token: 0x06008392 RID: 33682 RVA: 0x0037110C File Offset: 0x0036F50C
			public Temperature GetTemperatureType(float degree)
			{
				degree = Mathf.Clamp(degree, (float)this._minDegree, (float)this._maxDegree);
				if (degree < (float)this._lowBorder)
				{
					return Temperature.Cold;
				}
				if ((float)this._lowBorder <= degree && degree < (float)this._highBorder)
				{
					return Temperature.Normal;
				}
				if ((float)this._highBorder <= degree)
				{
					return Temperature.Hot;
				}
				return Temperature.Normal;
			}

			// Token: 0x06008393 RID: 33683 RVA: 0x0037116A File Offset: 0x0036F56A
			public float GetRandValue()
			{
				return (float)UnityEngine.Random.Range(this._minDegree, this._maxDegree + 1);
			}

			// Token: 0x06008394 RID: 33684 RVA: 0x00371180 File Offset: 0x0036F580
			public UnityEx.ValueTuple<int, int> GetValueRange(Temperature temp)
			{
				switch (temp)
				{
				case Temperature.Normal:
					return new UnityEx.ValueTuple<int, int>(this._lowBorder, this._highBorder - this._lowBorder - 1);
				case Temperature.Hot:
					return new UnityEx.ValueTuple<int, int>(this._highBorder, this._maxDegree - this._highBorder);
				case Temperature.Cold:
					return new UnityEx.ValueTuple<int, int>(this._minDegree, this._lowBorder - this._minDegree - 1);
				}
				return new UnityEx.ValueTuple<int, int>(0, 0);
			}

			// Token: 0x06008395 RID: 33685 RVA: 0x00371200 File Offset: 0x0036F600
			public int GetRandValue(Temperature temp)
			{
				switch (temp)
				{
				case Temperature.Normal:
					return UnityEngine.Random.Range(this._lowBorder, this._highBorder);
				case Temperature.Hot:
					return UnityEngine.Random.Range(this._highBorder, this._maxDegree + 1);
				case Temperature.Cold:
					return UnityEngine.Random.Range(this._minDegree, this._lowBorder);
				}
				return 0;
			}

			// Token: 0x040069F5 RID: 27125
			[SerializeField]
			private int _minDegree;

			// Token: 0x040069F6 RID: 27126
			[SerializeField]
			private int _lowBorder;

			// Token: 0x040069F7 RID: 27127
			[SerializeField]
			private int _highBorder;

			// Token: 0x040069F8 RID: 27128
			[SerializeField]
			private int _maxDegree;
		}

		// Token: 0x02000F6F RID: 3951
		[Serializable]
		public struct WeatherTemperatureRangeDayInfo
		{
			// Token: 0x17001BEF RID: 7151
			// (get) Token: 0x06008396 RID: 33686 RVA: 0x00371263 File Offset: 0x0036F663
			public EnvironmentSimulator.DateTimeSerialization DayTime
			{
				[CompilerGenerated]
				get
				{
					return this._dayTime;
				}
			}

			// Token: 0x17001BF0 RID: 7152
			// (get) Token: 0x06008397 RID: 33687 RVA: 0x0037126B File Offset: 0x0036F66B
			public EnvironmentSimulator.DateTimeSerialization NightTime
			{
				[CompilerGenerated]
				get
				{
					return this._nightTime;
				}
			}

			// Token: 0x06008398 RID: 33688 RVA: 0x00371273 File Offset: 0x0036F673
			public Threshold GetRange(TimeZone timeZone, Weather weather)
			{
				if (timeZone == TimeZone.Day)
				{
					return this._dayTimeRange.GetRange(weather);
				}
				if (timeZone != TimeZone.Night)
				{
					return new Threshold(0f, 0f);
				}
				return this._nightTimeRange.GetRange(weather);
			}

			// Token: 0x040069F9 RID: 27129
			[SerializeField]
			private EnvironmentSimulator.DateTimeSerialization _dayTime;

			// Token: 0x040069FA RID: 27130
			[SerializeField]
			private EnvironmentSimulator.DateTimeSerialization _nightTime;

			// Token: 0x040069FB RID: 27131
			[SerializeField]
			private EnvironmentProfile.WeatherTemperatureRangeInfo _dayTimeRange;

			// Token: 0x040069FC RID: 27132
			[SerializeField]
			private EnvironmentProfile.WeatherTemperatureRangeInfo _nightTimeRange;
		}

		// Token: 0x02000F70 RID: 3952
		[Serializable]
		public struct WeatherTemperatureRangeInfo
		{
			// Token: 0x06008399 RID: 33689 RVA: 0x003712B4 File Offset: 0x0036F6B4
			public Threshold GetRange(Weather weather)
			{
				switch (weather)
				{
				case Weather.Clear:
					return this._clearRange;
				case Weather.Cloud1:
					return this._cloud1Range;
				case Weather.Cloud2:
					return this._cloud2Range;
				case Weather.Cloud3:
					return this._cloud3Range;
				case Weather.Cloud4:
					return this._cloud4Range;
				case Weather.Fog:
					return this._fogRange;
				case Weather.Rain:
					return this._rainRange;
				case Weather.Storm:
					return this._stormRange;
				default:
					return new Threshold(0f, 0f);
				}
			}

			// Token: 0x040069FD RID: 27133
			[SerializeField]
			private Threshold _clearRange;

			// Token: 0x040069FE RID: 27134
			[SerializeField]
			private Threshold _cloud1Range;

			// Token: 0x040069FF RID: 27135
			[SerializeField]
			private Threshold _cloud2Range;

			// Token: 0x04006A00 RID: 27136
			[SerializeField]
			private Threshold _cloud3Range;

			// Token: 0x04006A01 RID: 27137
			[SerializeField]
			private Threshold _cloud4Range;

			// Token: 0x04006A02 RID: 27138
			[SerializeField]
			private Threshold _fogRange;

			// Token: 0x04006A03 RID: 27139
			[SerializeField]
			private Threshold _rainRange;

			// Token: 0x04006A04 RID: 27140
			[SerializeField]
			private Threshold _stormRange;
		}
	}
}
