using System;
using UnityEngine;

namespace EnviroSamples
{
	// Token: 0x02000320 RID: 800
	public class EventTest : MonoBehaviour
	{
		// Token: 0x06000E0E RID: 3598 RVA: 0x000443D4 File Offset: 0x000427D4
		private void Start()
		{
			EnviroSky.instance.OnWeatherChanged += delegate(EnviroWeatherPreset type)
			{
				this.DoOnWeatherChange(type);
			};
			EnviroSky.instance.OnZoneChanged += delegate(EnviroZone z)
			{
				this.DoOnZoneChange(z);
			};
			EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
			{
			};
			EnviroSky.instance.OnHourPassed += delegate()
			{
			};
			EnviroSky.instance.OnDayPassed += delegate()
			{
			};
			EnviroSky.instance.OnYearPassed += delegate()
			{
			};
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000444A9 File Offset: 0x000428A9
		private void DoOnWeatherChange(EnviroWeatherPreset type)
		{
			if (type.Name == "Light Rain")
			{
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000444C0 File Offset: 0x000428C0
		private void DoOnZoneChange(EnviroZone type)
		{
			if (type.zoneName == "Swamp")
			{
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000444D7 File Offset: 0x000428D7
		public void TestEventsWWeather()
		{
			MonoBehaviour.print("Weather Changed though interface!");
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000444E3 File Offset: 0x000428E3
		public void TestEventsNight()
		{
			MonoBehaviour.print("Night now!!");
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000444EF File Offset: 0x000428EF
		public void TestEventsDay()
		{
			MonoBehaviour.print("Day now!!");
		}
	}
}
