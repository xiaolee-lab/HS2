using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;
using UnityEx.Misc;

namespace AIProject
{
	// Token: 0x02000EFD RID: 3837
	public class EnvironmentSimulator : SerializedMonoBehaviour
	{
		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06007D41 RID: 32065 RVA: 0x003591CE File Offset: 0x003575CE
		// (set) Token: 0x06007D42 RID: 32066 RVA: 0x003591DB File Offset: 0x003575DB
		public bool EnabledSky
		{
			get
			{
				return this._enviroSky.enabled;
			}
			set
			{
				this._enviroSky.enabled = value;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06007D43 RID: 32067 RVA: 0x003591E9 File Offset: 0x003575E9
		// (set) Token: 0x06007D44 RID: 32068 RVA: 0x003591F1 File Offset: 0x003575F1
		public EnviroSky EnviroSky
		{
			get
			{
				return this._enviroSky;
			}
			set
			{
				this._enviroSky = value;
				value.Weather.updateWeather = false;
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06007D45 RID: 32069 RVA: 0x00359206 File Offset: 0x00357606
		// (set) Token: 0x06007D46 RID: 32070 RVA: 0x0035920E File Offset: 0x0035760E
		public EnviroZone DefaultEnviroZone
		{
			get
			{
				return this._defaultEnviroZone;
			}
			set
			{
				this._defaultEnviroZone = value;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06007D47 RID: 32071 RVA: 0x00359217 File Offset: 0x00357617
		// (set) Token: 0x06007D48 RID: 32072 RVA: 0x0035921F File Offset: 0x0035761F
		public EnviroEvents EnviroEvents
		{
			get
			{
				return this._enviroEvents;
			}
			set
			{
				this._enviroEvents = value;
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06007D49 RID: 32073 RVA: 0x00359228 File Offset: 0x00357628
		// (set) Token: 0x06007D4A RID: 32074 RVA: 0x00359230 File Offset: 0x00357630
		public bool IsActiveSimElement
		{
			get
			{
				return this._isActiveSimElement;
			}
			set
			{
				this._isActiveSimElement = value;
				this._skyDomeObject.SetActiveSafe(value);
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06007D4B RID: 32075 RVA: 0x00359245 File Offset: 0x00357645
		// (set) Token: 0x06007D4C RID: 32076 RVA: 0x00359252 File Offset: 0x00357652
		public bool EnabledTimeProgression
		{
			get
			{
				return this._enableTimeProgression.Value;
			}
			set
			{
				this._enableTimeProgression.Value = value;
			}
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x00359260 File Offset: 0x00357660
		public IObservable<bool> OnEnableChangeTimeAsObservable()
		{
			if (this._enableTimeChanged == null)
			{
				this._enableTimeChanged = this._enableTimeProgression.TakeUntilDestroy(base.gameObject).Publish<bool>();
				this._enableTimeChanged.Connect();
			}
			return this._enableTimeChanged;
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x0035929C File Offset: 0x0035769C
		public void SetTimeProgress(bool progress)
		{
			if (this._enviroSky == null)
			{
				return;
			}
			if (progress)
			{
				this._enviroSky.GameTime.ProgressTime = EnviroTime.TimeProgressMode.Simulated;
			}
			else
			{
				this._enviroSky.GameTime.ProgressTime = EnviroTime.TimeProgressMode.None;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06007D4F RID: 32079 RVA: 0x003592E8 File Offset: 0x003576E8
		// (set) Token: 0x06007D50 RID: 32080 RVA: 0x003592F0 File Offset: 0x003576F0
		public GameObject SkyDomeObject
		{
			get
			{
				return this._skyDomeObject;
			}
			set
			{
				this._skyDomeObject = value;
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06007D51 RID: 32081 RVA: 0x003592F9 File Offset: 0x003576F9
		public TimeZone TimeZone
		{
			[CompilerGenerated]
			get
			{
				return this._timeZone;
			}
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x00359304 File Offset: 0x00357704
		public IObservable<Weather> OnWeatherChangedAsObservable()
		{
			Subject<Weather> result;
			if ((result = this._weatherChanged) == null)
			{
				result = (this._weatherChanged = new Subject<Weather>());
			}
			return result;
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06007D53 RID: 32083 RVA: 0x0035932C File Offset: 0x0035772C
		public Weather Weather
		{
			[CompilerGenerated]
			get
			{
				return this._weather;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06007D54 RID: 32084 RVA: 0x00359334 File Offset: 0x00357734
		public Temperature Temperature
		{
			[CompilerGenerated]
			get
			{
				return this._temperature;
			}
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06007D55 RID: 32085 RVA: 0x0035933C File Offset: 0x0035773C
		// (set) Token: 0x06007D56 RID: 32086 RVA: 0x00359344 File Offset: 0x00357744
		public float TemperatureValue
		{
			get
			{
				return this._temperatureValue;
			}
			set
			{
				float num = value;
				if (this._environmentProfile != null)
				{
					float min = (float)this._environmentProfile.TemperatureBorder.MinDegree;
					float max = (float)this._environmentProfile.TemperatureBorder.MaxDegree;
					num = Mathf.Clamp(num, min, max);
				}
				if (this._temperatureValue == num)
				{
					return;
				}
				this._temperatureValue = num;
				if (this._environmentProfile == null)
				{
					return;
				}
				Temperature temperatureType = this._environmentProfile.TemperatureBorder.GetTemperatureType(this._temperatureValue);
				this.RefreshTemperature(temperatureType);
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06007D57 RID: 32087 RVA: 0x003593E2 File Offset: 0x003577E2
		public EnvironmentProfile EnvironmentProfile
		{
			[CompilerGenerated]
			get
			{
				return this._environmentProfile;
			}
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x003593EC File Offset: 0x003577EC
		public IObservable<Unit> OnEnvironmentChangedAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._onEnvironmentChange) == null)
			{
				result = (this._onEnvironmentChange = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x00359414 File Offset: 0x00357814
		public IObservable<TimeZone> OnTimeZoneChangedAsObservable()
		{
			Subject<TimeZone> result;
			if ((result = this._onTimeZoneChange) == null)
			{
				result = (this._onTimeZoneChange = new Subject<TimeZone>());
			}
			return result;
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x0035943C File Offset: 0x0035783C
		public IObservable<TimeZone> OnMapLightTimeZoneChangedAsObservable()
		{
			Subject<TimeZone> result;
			if ((result = this._onMapLightTimeZoneChange) == null)
			{
				result = (this._onMapLightTimeZoneChange = new Subject<TimeZone>());
			}
			return result;
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06007D5B RID: 32091 RVA: 0x00359464 File Offset: 0x00357864
		// (set) Token: 0x06007D5C RID: 32092 RVA: 0x003594CB File Offset: 0x003578CB
		public DateTime Now
		{
			get
			{
				EnviroTime gameTime = this._enviroSky.GameTime;
				DateTime result = new DateTime(gameTime.Years, 1, 1);
				result = result.AddDays((double)(gameTime.Days - 1));
				result = result.AddHours((double)gameTime.Hours);
				result = result.AddMinutes((double)gameTime.Minutes);
				result = result.AddSeconds((double)gameTime.Seconds);
				return result;
			}
			set
			{
				this._enviroSky.SetTime(value);
			}
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06007D5D RID: 32093 RVA: 0x003594D9 File Offset: 0x003578D9
		public int DeltaSeconds
		{
			[CompilerGenerated]
			get
			{
				return this._deltaSeconds;
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06007D5E RID: 32094 RVA: 0x003594E1 File Offset: 0x003578E1
		public ReadOnlyCollection<AIProject.SaveData.Environment.ScheduleData> Schedules
		{
			[CompilerGenerated]
			get
			{
				return this._schedules.AsReadOnly();
			}
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x003594EE File Offset: 0x003578EE
		private void Awake()
		{
			this.ResetTempDateTime();
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x003594F8 File Offset: 0x003578F8
		private void Start()
		{
			this._reactiveTimeZone.Subscribe(delegate(TimeZone timeZone)
			{
				this.SetTimeZone(timeZone);
			});
			this._reactiveTempTimeZone.Subscribe(delegate(TimeZone timeZone)
			{
				this.SetTempTimeZone(timeZone);
			});
			this._reactiveMapLightTimeZone.Subscribe(delegate(TimeZone timeZone)
			{
				this.SetMapLightTimeZone(timeZone);
			});
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x00359550 File Offset: 0x00357950
		private void Update()
		{
			float dayLengthInMinute = this._environmentProfile.DayLengthInMinute;
			this._enviroSky.GameTime.DayLengthInMinutes = dayLengthInMinute;
			this._enviroSky.GameTime.NightLengthInMinutes = dayLengthInMinute;
			this.OnTimeUpdate();
			TimeSpan timeOfDay = this.Now.TimeOfDay;
			if (timeOfDay >= this._environmentProfile.MorningTime.TimeOfDay && timeOfDay < this._environmentProfile.DayTime.TimeOfDay)
			{
				this._reactiveTimeZone.Value = TimeZone.Morning;
			}
			else if (timeOfDay >= this._environmentProfile.DayTime.TimeOfDay && timeOfDay < this._environmentProfile.NightTime.TimeOfDay)
			{
				this._reactiveTimeZone.Value = TimeZone.Day;
			}
			else
			{
				this._reactiveTimeZone.Value = TimeZone.Night;
			}
			this.OnTemperatureValueUpdate(timeOfDay);
			this.OnMapLightTimeUpdate(timeOfDay);
		}

		// Token: 0x06007D62 RID: 32098 RVA: 0x00359657 File Offset: 0x00357A57
		public void InitializeEvents()
		{
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x00359659 File Offset: 0x00357A59
		private void OnHourUpdate()
		{
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x0035965B File Offset: 0x00357A5B
		public void OnMinuteUpdate(TimeSpan deltaTime)
		{
		}

		// Token: 0x06007D65 RID: 32101 RVA: 0x00359660 File Offset: 0x00357A60
		private void OnSecondUpdate()
		{
			DateTime now = this.Now;
			this._deltaSeconds = (int)(now - this._prevDateTime).TotalSeconds;
			this._prevDateTime = now;
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x00359698 File Offset: 0x00357A98
		private void OnTimeUpdate()
		{
			EnviroTime oldTime = this.OldTime;
			EnviroTime gameTime = this._enviroSky.GameTime;
			DateTime newTime = new DateTime(1, 1, gameTime.Days, gameTime.Hours, gameTime.Minutes, gameTime.Seconds);
			bool flag = false;
			if (gameTime.Days > oldTime.Days)
			{
				TimeSpan value = this.DeltaTimeSpan(newTime, this.OldDayUpdatedTime);
				TimeSpan value2 = this.DeltaTimeSpan(newTime, this.OldHourUpdatedTime);
				TimeSpan value3 = this.DeltaTimeSpan(newTime, this.OldMinuteUpdatedTime);
				TimeSpan value4 = this.DeltaTimeSpan(newTime, this.OldSecondUpdatedTime);
				gameTime.Days = 1;
				if (this._onDay != null)
				{
					this._onDay.OnNext(value);
				}
				if (this._onHour != null)
				{
					this._onHour.OnNext(value2);
				}
				if (this._onMinute != null)
				{
					this._onMinute.OnNext(value3);
				}
				if (this._onSecond != null)
				{
					this._onSecond.OnNext(value4);
				}
				flag = true;
				this.SetTimeToEnviroTime(this.OldDayUpdatedTime, gameTime);
				this.SetTimeToEnviroTime(this.OldHourUpdatedTime, gameTime);
				this.SetTimeToEnviroTime(this.OldMinuteUpdatedTime, gameTime);
				this.SetTimeToEnviroTime(this.OldSecondUpdatedTime, gameTime);
			}
			else if (gameTime.Hours > oldTime.Hours)
			{
				TimeSpan value5 = this.DeltaTimeSpan(newTime, this.OldHourUpdatedTime);
				TimeSpan value6 = this.DeltaTimeSpan(newTime, this.OldMinuteUpdatedTime);
				TimeSpan value7 = this.DeltaTimeSpan(newTime, this.OldSecondUpdatedTime);
				if (this._onHour != null)
				{
					this._onHour.OnNext(value5);
				}
				if (this._onMinute != null)
				{
					this._onMinute.OnNext(value6);
				}
				if (this._onSecond != null)
				{
					this._onSecond.OnNext(value7);
				}
				flag = true;
				this.SetTimeToEnviroTime(this.OldHourUpdatedTime, gameTime);
				this.SetTimeToEnviroTime(this.OldMinuteUpdatedTime, gameTime);
				this.SetTimeToEnviroTime(this.OldSecondUpdatedTime, gameTime);
			}
			else if (gameTime.Minutes > oldTime.Minutes)
			{
				TimeSpan value8 = this.DeltaTimeSpan(newTime, this.OldMinuteUpdatedTime);
				TimeSpan value9 = this.DeltaTimeSpan(newTime, this.OldSecondUpdatedTime);
				if (this._onMinute != null)
				{
					this._onMinute.OnNext(value8);
				}
				if (this._onSecond != null)
				{
					this._onSecond.OnNext(value9);
				}
				flag = true;
				this.SetTimeToEnviroTime(this.OldMinuteUpdatedTime, gameTime);
				this.SetTimeToEnviroTime(this.OldSecondUpdatedTime, gameTime);
			}
			else if (gameTime.Seconds > oldTime.Seconds)
			{
				TimeSpan value10 = this.DeltaTimeSpan(newTime, this.OldSecondUpdatedTime);
				if (this._onSecond != null)
				{
					this._onSecond.OnNext(value10);
				}
				flag = true;
				this.SetTimeToEnviroTime(this.OldSecondUpdatedTime, gameTime);
			}
			if (flag)
			{
				this.OldTime.Days = gameTime.Days;
				this.OldTime.Hours = gameTime.Hours;
				this.OldTime.Minutes = gameTime.Minutes;
				this.OldTime.Seconds = gameTime.Seconds;
			}
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06007D67 RID: 32103 RVA: 0x003599A3 File Offset: 0x00357DA3
		// (set) Token: 0x06007D68 RID: 32104 RVA: 0x003599AB File Offset: 0x00357DAB
		public EnviroTime OldTime { get; private set; } = new EnviroTime
		{
			Hours = 1
		};

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06007D69 RID: 32105 RVA: 0x003599B4 File Offset: 0x00357DB4
		// (set) Token: 0x06007D6A RID: 32106 RVA: 0x003599BC File Offset: 0x00357DBC
		public EnviroTime OldDayUpdatedTime { get; private set; } = new EnviroTime
		{
			Hours = 1
		};

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06007D6B RID: 32107 RVA: 0x003599C5 File Offset: 0x00357DC5
		// (set) Token: 0x06007D6C RID: 32108 RVA: 0x003599CD File Offset: 0x00357DCD
		public EnviroTime OldHourUpdatedTime { get; private set; } = new EnviroTime
		{
			Hours = 1
		};

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06007D6D RID: 32109 RVA: 0x003599D6 File Offset: 0x00357DD6
		// (set) Token: 0x06007D6E RID: 32110 RVA: 0x003599DE File Offset: 0x00357DDE
		public EnviroTime OldMinuteUpdatedTime { get; private set; } = new EnviroTime
		{
			Hours = 1
		};

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06007D6F RID: 32111 RVA: 0x003599E7 File Offset: 0x00357DE7
		// (set) Token: 0x06007D70 RID: 32112 RVA: 0x003599EF File Offset: 0x00357DEF
		public EnviroTime OldSecondUpdatedTime { get; private set; } = new EnviroTime
		{
			Hours = 1
		};

		// Token: 0x06007D71 RID: 32113 RVA: 0x003599F8 File Offset: 0x00357DF8
		public void SetTimeToEnviroTime(EnviroTime time, EnviroTime newTime)
		{
			time.Days = newTime.Days;
			time.Hours = newTime.Hours;
			time.Minutes = newTime.Minutes;
			time.Seconds = newTime.Seconds;
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x00359A2C File Offset: 0x00357E2C
		public TimeSpan DeltaTimeSpan(DateTime newTime, EnviroTime oldEnviroTime)
		{
			DateTime d = new DateTime(1, 1, oldEnviroTime.Days, oldEnviroTime.Hours, oldEnviroTime.Minutes, oldEnviroTime.Seconds);
			return newTime - d;
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x00359A64 File Offset: 0x00357E64
		public IObservable<TimeSpan> OnDayAsObservable()
		{
			Subject<TimeSpan> result;
			if ((result = this._onDay) == null)
			{
				result = (this._onDay = new Subject<TimeSpan>());
			}
			return result;
		}

		// Token: 0x06007D74 RID: 32116 RVA: 0x00359A8C File Offset: 0x00357E8C
		public IObservable<TimeSpan> OnHourAsObservable()
		{
			Subject<TimeSpan> result;
			if ((result = this._onHour) == null)
			{
				result = (this._onHour = new Subject<TimeSpan>());
			}
			return result;
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x00359AB4 File Offset: 0x00357EB4
		public IObservable<TimeSpan> OnMinuteAsObservable()
		{
			Subject<TimeSpan> result;
			if ((result = this._onMinute) == null)
			{
				result = (this._onMinute = new Subject<TimeSpan>());
			}
			return result;
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x00359ADC File Offset: 0x00357EDC
		public IObservable<TimeSpan> OnSecondAsObservable()
		{
			Subject<TimeSpan> result;
			if ((result = this._onSecond) == null)
			{
				result = (this._onSecond = new Subject<TimeSpan>());
			}
			return result;
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x00359B04 File Offset: 0x00357F04
		public void SetTimeZone(TimeZone zone)
		{
			this._timeZone = zone;
			if (this._onTimeZoneChange != null)
			{
				this._onTimeZoneChange.OnNext(zone);
			}
			if (this._onEnvironmentChange != null)
			{
				this._onEnvironmentChange.OnNext(Unit.Default);
			}
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x00359B44 File Offset: 0x00357F44
		public void RefreshWeather(Weather weather, bool forced = false)
		{
			if (!forced && this._weather == weather)
			{
				return;
			}
			this._weather = weather;
			if (this._weatherChanged != null)
			{
				this._weatherChanged.OnNext(weather);
			}
			this._enviroSky.ChangeWeather((int)weather);
			this.RefreshEnviroParticles(weather);
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06007D79 RID: 32121 RVA: 0x00359B97 File Offset: 0x00357F97
		// (set) Token: 0x06007D7A RID: 32122 RVA: 0x00359B9F File Offset: 0x00357F9F
		public int MapID { get; set; }

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06007D7B RID: 32123 RVA: 0x00359BA8 File Offset: 0x00357FA8
		public TimeZone TempTimeZone
		{
			[CompilerGenerated]
			get
			{
				return this._tempTimeZone;
			}
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x00359BB0 File Offset: 0x00357FB0
		private void ResetTempDateTime()
		{
			if (this._environmentProfile != null)
			{
				this._tempDayTimeOfDay = this._environmentProfile.WeatherTemperatureRange.DayTime.Time.TimeOfDay;
				this._tempNightTimeOfDay = this._environmentProfile.WeatherTemperatureRange.NightTime.Time.TimeOfDay;
			}
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x00359C24 File Offset: 0x00358024
		private void SetTempTimeZone(TimeZone timeZone)
		{
			this._tempTimeZone = timeZone;
			TimeZone tempTimeZone = this._tempTimeZone;
			if (tempTimeZone != TimeZone.Day)
			{
				if (tempTimeZone != TimeZone.Night)
				{
				}
			}
			else
			{
				WeatherProbability probWeatherProfile = Singleton<Manager.Resources>.Instance.CommonDefine.ProbWeatherProfile;
				List<Weather> list = Enum.GetValues(typeof(Weather)).Cast<Weather>().Skip(0).ToList<Weather>();
				Weather weather = probWeatherProfile.Lottery(list);
				while (this._weather == weather)
				{
					list.Remove(weather);
					weather = probWeatherProfile.Lottery(list);
				}
				this.RefreshWeather(weather, false);
			}
			this.RefreshTemperatureValue();
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x00359CC4 File Offset: 0x003580C4
		public void SetTemperatureValue(float tempValue)
		{
			Observable.TimerFrame(2, FrameCountType.Update).TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				if (this._environmentProfile == null)
				{
					return;
				}
				Threshold range = this._environmentProfile.WeatherTemperatureRange.GetRange(this._tempTimeZone, this._weather);
				if (range.min <= tempValue && tempValue <= range.max)
				{
					this.TemperatureValue = tempValue;
				}
				else
				{
					this.TemperatureValue = range.RandomValue;
				}
			});
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x00359D04 File Offset: 0x00358104
		public void RefreshTemperatureValue()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>> tempRangeTable = Singleton<Manager.Resources>.Instance.Map.TempRangeTable;
			if (tempRangeTable.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>>())
			{
				return;
			}
			Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>> dictionary;
			if (!tempRangeTable.TryGetValue(this.MapID, out dictionary) || dictionary.IsNullOrEmpty<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>())
			{
				if (this.MapID != 0)
				{
					tempRangeTable.TryGetValue(0, out dictionary);
				}
				if (dictionary.IsNullOrEmpty<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>())
				{
					return;
				}
			}
			int key;
			if (!AIProject.Definitions.Environment.TimeZoneIDTable.TryGetValue(this._tempTimeZone, out key))
			{
				return;
			}
			Dictionary<int, List<UnityEx.ValueTuple<int, int>>> dictionary2;
			if (!dictionary.TryGetValue(key, out dictionary2) || dictionary2.IsNullOrEmpty<int, List<UnityEx.ValueTuple<int, int>>>())
			{
				return;
			}
			List<UnityEx.ValueTuple<int, int>> list;
			if (!dictionary2.TryGetValue((int)this._weather, out list) || list.IsNullOrEmpty<UnityEx.ValueTuple<int, int>>())
			{
				return;
			}
			List<UnityEx.ValueTuple<int, int>> list2 = ListPool<UnityEx.ValueTuple<int, int>>.Get();
			list2.AddRange(list);
			int num = 0;
			foreach (UnityEx.ValueTuple<int, int> valueTuple in list2)
			{
				num += valueTuple.Item2;
			}
			int num2 = UnityEngine.Random.Range(0, num);
			while (0 < list2.Count)
			{
				UnityEx.ValueTuple<int, int> valueTuple2 = list2.PopFront<UnityEx.ValueTuple<int, int>>();
				if (num2 < valueTuple2.Item2)
				{
					this.TemperatureValue = (float)valueTuple2.Item1;
					ListPool<UnityEx.ValueTuple<int, int>>.Release(list2);
					return;
				}
				num2 -= valueTuple2.Item2;
			}
			ListPool<UnityEx.ValueTuple<int, int>>.Release(list2);
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x00359E8C File Offset: 0x0035828C
		private void OnTemperatureValueUpdate(TimeSpan now)
		{
			if (this._tempNightTimeOfDay < this._tempDayTimeOfDay)
			{
				if (now < this._tempNightTimeOfDay || this._tempDayTimeOfDay <= now)
				{
					this._reactiveTempTimeZone.Value = TimeZone.Day;
				}
				else
				{
					this._reactiveTempTimeZone.Value = TimeZone.Night;
				}
			}
			else if (this._tempDayTimeOfDay <= now && now < this._tempNightTimeOfDay)
			{
				this._reactiveTempTimeZone.Value = TimeZone.Day;
			}
			else
			{
				this._reactiveTempTimeZone.Value = TimeZone.Night;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06007D81 RID: 32129 RVA: 0x00359F32 File Offset: 0x00358332
		public TimeZone MapLightTimeZone
		{
			[CompilerGenerated]
			get
			{
				return this._mapLightTimeZone;
			}
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x00359F3A File Offset: 0x0035833A
		public void SetMapLightTimeZone(TimeZone timeZone)
		{
			this._mapLightTimeZone = timeZone;
			if (this._onMapLightTimeZoneChange != null)
			{
				this._onMapLightTimeZoneChange.OnNext(timeZone);
			}
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x00359F5C File Offset: 0x0035835C
		private void OnMapLightTimeUpdate(TimeSpan now)
		{
			if (this._environmentProfile.LightMorningTime.TimeOfDay <= now && now < this._environmentProfile.LightDayTime.TimeOfDay)
			{
				this._reactiveMapLightTimeZone.Value = TimeZone.Morning;
			}
			else if (this._environmentProfile.LightDayTime.TimeOfDay <= now && now < this._environmentProfile.LightNightTime.TimeOfDay)
			{
				this._reactiveMapLightTimeZone.Value = TimeZone.Day;
			}
			else
			{
				this._reactiveMapLightTimeZone.Value = TimeZone.Night;
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06007D84 RID: 32132 RVA: 0x0035A00F File Offset: 0x0035840F
		// (set) Token: 0x06007D85 RID: 32133 RVA: 0x0035A04F File Offset: 0x0035844F
		public GameObject EnviroParticleRoot
		{
			get
			{
				if (this._enviroParticleRoot == null)
				{
					this._enviroParticleRoot = new GameObject("Enviro Particle Root");
					this._enviroParticleRoot.transform.SetParent(base.transform, false);
				}
				return this._enviroParticleRoot;
			}
			private set
			{
				this._enviroParticleRoot = value;
			}
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x0035A058 File Offset: 0x00358458
		public void EnviroParticleActivate(bool active)
		{
			GameObject enviroParticleRoot = this.EnviroParticleRoot;
			if (enviroParticleRoot == null || enviroParticleRoot.activeSelf == active)
			{
				return;
			}
			enviroParticleRoot.SetActive(active);
		}

		// Token: 0x06007D87 RID: 32135 RVA: 0x0035A08C File Offset: 0x0035848C
		public void InitializeEnviroParticle()
		{
			if (!this._enviroParticlePrefabTable.IsNullOrEmpty<Weather, List<ParticleSystem>>())
			{
				GameObject enviroParticleRoot = this.EnviroParticleRoot;
				foreach (KeyValuePair<Weather, List<ParticleSystem>> keyValuePair in this._enviroParticlePrefabTable)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<ParticleSystem>())
					{
						foreach (ParticleSystem particleSystem in keyValuePair.Value)
						{
							if (!(particleSystem == null))
							{
								int instanceID = particleSystem.GetInstanceID();
								EnviroParticleInfo enviroParticleInfo;
								if (!this._enviroParticleDataTable.TryGetValue(instanceID, out enviroParticleInfo))
								{
									ParticleSystem particleSystem2 = UnityEngine.Object.Instantiate<ParticleSystem>(particleSystem, enviroParticleRoot.transform);
									enviroParticleInfo = (this._enviroParticleDataTable[instanceID] = new EnviroParticleInfo(instanceID, particleSystem2));
									particleSystem2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
									enviroParticleInfo.Emission = 0f;
								}
								List<EnviroParticleInfo> list;
								if (!this._enviroParticleObjectTable.TryGetValue(keyValuePair.Key, out list))
								{
									list = (this._enviroParticleObjectTable[keyValuePair.Key] = new List<EnviroParticleInfo>());
								}
								list.Add(enviroParticleInfo);
							}
						}
					}
				}
			}
		}

		// Token: 0x06007D88 RID: 32136 RVA: 0x0035A214 File Offset: 0x00358614
		public void SetEnviroParticleTarget(Transform target)
		{
			if (target == null)
			{
				return;
			}
			GameObject envParRoot = this.EnviroParticleRoot;
			if (envParRoot == null)
			{
				return;
			}
			if (this._enviroParticleFollowDisposable != null)
			{
				this._enviroParticleFollowDisposable.Dispose();
			}
			this._enviroParticleFollowDisposable = envParRoot.transform.LateUpdateAsObservable().TakeUntilDestroy(target).Subscribe(delegate(Unit _)
			{
				Vector3 position = target.position;
				if (this._enviroParticleFollowTargetHeight)
				{
					position.y += this._enviroParticleHeight;
				}
				else
				{
					position.y = this._enviroParticleHeight;
				}
				envParRoot.transform.position = position;
			});
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x0035A2AF File Offset: 0x003586AF
		public void StopEnviroParticleTargetFollow()
		{
			if (this._enviroParticleFollowDisposable != null)
			{
				this._enviroParticleFollowDisposable.Dispose();
				this._enviroParticleFollowDisposable = null;
			}
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x0035A2D0 File Offset: 0x003586D0
		private void RefreshEnviroParticles(Weather weather)
		{
			List<EnviroParticleInfo> list;
			if (!this._enviroParticleObjectTable.TryGetValue(weather, out list) || list.IsNullOrEmpty<EnviroParticleInfo>())
			{
				if (!this._playingEnviroParticles.IsNullOrEmpty<EnviroParticleInfo>())
				{
					foreach (EnviroParticleInfo enviroParticleInfo in this._playingEnviroParticles)
					{
						enviroParticleInfo.PlayFadeOut(this._fadeTime);
					}
					this._playingEnviroParticles.Clear();
				}
				return;
			}
			List<EnviroParticleInfo> list2 = ListPool<EnviroParticleInfo>.Get();
			foreach (EnviroParticleInfo item in this._playingEnviroParticles)
			{
				if (!list.Contains(item) && !list2.Contains(item))
				{
					list2.Add(item);
				}
			}
			foreach (EnviroParticleInfo enviroParticleInfo2 in list2)
			{
				enviroParticleInfo2.PlayFadeOut(this._fadeTime);
			}
			ListPool<EnviroParticleInfo>.Release(list2);
			foreach (EnviroParticleInfo enviroParticleInfo3 in list)
			{
				enviroParticleInfo3.PlayFadeIn(this._fadeTime);
			}
			this._playingEnviroParticles.Clear();
			this._playingEnviroParticles.AddRange(list);
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x0035A494 File Offset: 0x00358894
		public void RefreshTemperature(Temperature temperature)
		{
			if (this._temperature == temperature)
			{
				return;
			}
			this._temperature = temperature;
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x0035A4AA File Offset: 0x003588AA
		public bool Elapsed(DateTime dateTime)
		{
			return this.Now > dateTime;
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x0035A4B8 File Offset: 0x003588B8
		public void ElapseDays(int days)
		{
			foreach (AIProject.SaveData.Environment.ScheduleData scheduleData in this.Schedules)
			{
				scheduleData.DaysToGo -= days;
			}
			List<AIProject.SaveData.Environment.ScheduleData> list = this._schedules.FindAll(delegate(AIProject.SaveData.Environment.ScheduleData x)
			{
				TimeSpan t = this.Now - x.StartTime.DateTime;
				return x.DaysToGo == 0 && x.StartTime.DateTime < this.Now && x.Duration.TimeSpan < t;
			});
			foreach (AIProject.SaveData.Environment.ScheduleData scheduleData2 in list)
			{
				AgentActor agentActor = Singleton<Manager.Map>.Instance.AgentTable[scheduleData2.Event.agentID];
				this._schedules.Remove(scheduleData2);
			}
		}

		// Token: 0x0400654D RID: 25933
		[SerializeField]
		private EnviroSky _enviroSky;

		// Token: 0x0400654E RID: 25934
		[SerializeField]
		private EnviroZone _defaultEnviroZone;

		// Token: 0x0400654F RID: 25935
		[SerializeField]
		private EnviroEvents _enviroEvents;

		// Token: 0x04006550 RID: 25936
		private bool _isActiveSimElement = true;

		// Token: 0x04006551 RID: 25937
		[SerializeField]
		private BoolReactiveProperty _enableTimeProgression = new BoolReactiveProperty();

		// Token: 0x04006552 RID: 25938
		private IConnectableObservable<bool> _enableTimeChanged;

		// Token: 0x04006553 RID: 25939
		[SerializeField]
		private GameObject _skyDomeObject;

		// Token: 0x04006554 RID: 25940
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private TimeZone _timeZone = TimeZone.Day;

		// Token: 0x04006555 RID: 25941
		private ReactiveProperty<TimeZone> _reactiveTimeZone = new ReactiveProperty<TimeZone>(TimeZone.Day);

		// Token: 0x04006556 RID: 25942
		private Subject<Weather> _weatherChanged;

		// Token: 0x04006557 RID: 25943
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private Weather _weather;

		// Token: 0x04006558 RID: 25944
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private Temperature _temperature = Temperature.Normal;

		// Token: 0x04006559 RID: 25945
		[ShowInInspector]
		[HideInEditorMode]
		[DisableInPlayMode]
		private float _temperatureValue = 25f;

		// Token: 0x0400655A RID: 25946
		[SerializeField]
		private float _fadeTime = 10f;

		// Token: 0x0400655B RID: 25947
		[SerializeField]
		private EnvironmentProfile _environmentProfile;

		// Token: 0x0400655C RID: 25948
		[SerializeField]
		private float _enviroParticleHeight;

		// Token: 0x0400655D RID: 25949
		[SerializeField]
		private bool _enviroParticleFollowTargetHeight;

		// Token: 0x0400655E RID: 25950
		[SerializeField]
		[DisableInPlayMode]
		private Dictionary<Weather, List<ParticleSystem>> _enviroParticlePrefabTable = new Dictionary<Weather, List<ParticleSystem>>();

		// Token: 0x0400655F RID: 25951
		[SerializeField]
		private Dictionary<Weather, float> _weatherTemperatureCorrectedValueTable = new Dictionary<Weather, float>();

		// Token: 0x04006560 RID: 25952
		private Subject<Unit> _onEnvironmentChange;

		// Token: 0x04006561 RID: 25953
		private Subject<TimeZone> _onTimeZoneChange;

		// Token: 0x04006562 RID: 25954
		private Subject<TimeZone> _onMapLightTimeZoneChange;

		// Token: 0x04006563 RID: 25955
		private DateTime _prevDateTime = DateTime.MinValue;

		// Token: 0x04006564 RID: 25956
		private int _deltaSeconds;

		// Token: 0x04006565 RID: 25957
		private List<AIProject.SaveData.Environment.ScheduleData> _schedules = new List<AIProject.SaveData.Environment.ScheduleData>();

		// Token: 0x0400656B RID: 25963
		private Subject<TimeSpan> _onDay;

		// Token: 0x0400656C RID: 25964
		private Subject<TimeSpan> _onHour;

		// Token: 0x0400656D RID: 25965
		private Subject<TimeSpan> _onMinute;

		// Token: 0x0400656E RID: 25966
		private Subject<TimeSpan> _onSecond;

		// Token: 0x04006570 RID: 25968
		private TimeSpan _tempDayTimeOfDay = TimeSpan.MinValue;

		// Token: 0x04006571 RID: 25969
		private TimeSpan _tempNightTimeOfDay = TimeSpan.MinValue;

		// Token: 0x04006572 RID: 25970
		private TimeZone _tempTimeZone = TimeZone.Morning;

		// Token: 0x04006573 RID: 25971
		private ReactiveProperty<TimeZone> _reactiveTempTimeZone = new ReactiveProperty<TimeZone>(TimeZone.Morning);

		// Token: 0x04006574 RID: 25972
		private TimeZone _mapLightTimeZone = TimeZone.Day;

		// Token: 0x04006575 RID: 25973
		private ReactiveProperty<TimeZone> _reactiveMapLightTimeZone = new ReactiveProperty<TimeZone>(TimeZone.Day);

		// Token: 0x04006576 RID: 25974
		private List<EnviroParticleInfo> _playingEnviroParticles = new List<EnviroParticleInfo>();

		// Token: 0x04006577 RID: 25975
		private Dictionary<int, EnviroParticleInfo> _enviroParticleDataTable = new Dictionary<int, EnviroParticleInfo>();

		// Token: 0x04006578 RID: 25976
		private Dictionary<Weather, List<EnviroParticleInfo>> _enviroParticleObjectTable = new Dictionary<Weather, List<EnviroParticleInfo>>();

		// Token: 0x04006579 RID: 25977
		private GameObject _enviroParticleRoot;

		// Token: 0x0400657A RID: 25978
		private IDisposable _enviroParticleFollowDisposable;

		// Token: 0x02000EFE RID: 3838
		[Serializable]
		public struct DateTimeThreshold
		{
			// Token: 0x0400657B RID: 25979
			public EnvironmentSimulator.DateTimeSerialization min;

			// Token: 0x0400657C RID: 25980
			public EnvironmentSimulator.DateTimeSerialization max;
		}

		// Token: 0x02000EFF RID: 3839
		[Serializable]
		public struct DateTimeSerialization
		{
			// Token: 0x06007D92 RID: 32146 RVA: 0x0035A623 File Offset: 0x00358A23
			public DateTimeSerialization(DateTime dateTime)
			{
				this._day = dateTime.Day;
				this._hour = dateTime.Hour;
				this._minute = dateTime.Minute;
			}

			// Token: 0x170018A8 RID: 6312
			// (get) Token: 0x06007D93 RID: 32147 RVA: 0x0035A64C File Offset: 0x00358A4C
			// (set) Token: 0x06007D94 RID: 32148 RVA: 0x0035A66A File Offset: 0x00358A6A
			public DateTime Time
			{
				get
				{
					return new DateTime(1, 1, 1 + this._day, this._hour, this._minute, 0);
				}
				set
				{
					this._day = value.Day - 1;
					this._hour = value.Hour;
					this._minute = value.Minute;
				}
			}

			// Token: 0x0400657D RID: 25981
			[SerializeField]
			private int _day;

			// Token: 0x0400657E RID: 25982
			[SerializeField]
			private int _hour;

			// Token: 0x0400657F RID: 25983
			[SerializeField]
			private int _minute;
		}
	}
}
