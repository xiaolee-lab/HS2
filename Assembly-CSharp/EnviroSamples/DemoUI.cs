using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EnviroSamples
{
	// Token: 0x0200031F RID: 799
	public class DemoUI : MonoBehaviour
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x00043EAC File Offset: 0x000422AC
		private void Start()
		{
			EnviroSky.instance.OnWeatherChanged += delegate(EnviroWeatherPreset type)
			{
				this.UpdateWeatherSlider();
			};
			EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
			{
				this.UpdateSeasonSlider(season);
			};
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00043EDC File Offset: 0x000422DC
		private IEnumerator setupDrodown()
		{
			this.started = true;
			yield return new WaitForSeconds(0.1f);
			for (int i = 0; i < EnviroSky.instance.Weather.weatherPresets.Count; i++)
			{
				Dropdown.OptionData optionData = new Dropdown.OptionData();
				optionData.text = EnviroSky.instance.Weather.weatherPresets[i].Name;
				this.weatherDropdown.options.Add(optionData);
			}
			yield return new WaitForSeconds(0.1f);
			this.UpdateWeatherSlider();
			yield break;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00043EF8 File Offset: 0x000422F8
		public void ChangeTimeSlider()
		{
			if (this.sliderTime.value < 0f)
			{
				this.sliderTime.value = 0f;
			}
			EnviroSky.instance.SetInternalTimeOfDay(this.sliderTime.value * 24f);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00043F45 File Offset: 0x00042345
		public void ChangeTimeLenghtSlider(float value)
		{
			EnviroSky.instance.GameTime.DayLengthInMinutes = value;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00043F57 File Offset: 0x00042357
		public void ChangeCloudQuality(int value)
		{
			EnviroSky.instance.cloudsSettings.cloudsQuality = (EnviroCloudSettings.CloudQuality)value;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00043F69 File Offset: 0x00042369
		public void ChangeQualitySlider()
		{
			EnviroSky.instance.profile.qualitySettings.GlobalParticleEmissionRates = this.sliderQuality.value;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00043F8A File Offset: 0x0004238A
		public void ChangeAmbientVolume(float value)
		{
			EnviroSky.instance.Audio.ambientSFXVolume = value;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00043F9C File Offset: 0x0004239C
		public void ChangeWeatherVolume(float value)
		{
			EnviroSky.instance.Audio.weatherSFXVolume = value;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00043FAE File Offset: 0x000423AE
		public void SetWeatherID(int id)
		{
			EnviroSky.instance.ChangeWeather(id);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00043FBB File Offset: 0x000423BB
		public void SetClouds(int id)
		{
			EnviroSky.instance.cloudsMode = (EnviroSky.EnviroCloudsMode)id;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00043FC8 File Offset: 0x000423C8
		public void OverwriteSeason()
		{
			if (!this.seasonmode)
			{
				this.seasonmode = true;
				EnviroSky.instance.Seasons.calcSeasons = true;
			}
			else
			{
				this.seasonmode = false;
				EnviroSky.instance.Seasons.calcSeasons = false;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00044008 File Offset: 0x00042408
		public void FastDays()
		{
			if (!this.fastdays)
			{
				this.fastdays = true;
				EnviroSky.instance.GameTime.DayLengthInMinutes = 0.2f;
			}
			else
			{
				this.fastdays = false;
				EnviroSky.instance.GameTime.DayLengthInMinutes = 5f;
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0004405C File Offset: 0x0004245C
		public void SetSeason(int id)
		{
			switch (id)
			{
			case 0:
				EnviroSky.instance.ChangeSeason(EnviroSeasons.Seasons.Spring);
				break;
			case 1:
				EnviroSky.instance.ChangeSeason(EnviroSeasons.Seasons.Summer);
				break;
			case 2:
				EnviroSky.instance.ChangeSeason(EnviroSeasons.Seasons.Autumn);
				break;
			case 3:
				EnviroSky.instance.ChangeSeason(EnviroSeasons.Seasons.Winter);
				break;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000440C4 File Offset: 0x000424C4
		public void SetTimeProgress(int id)
		{
			if (id != 0)
			{
				if (id != 1)
				{
					if (id == 2)
					{
						EnviroSky.instance.GameTime.ProgressTime = EnviroTime.TimeProgressMode.SystemTime;
					}
				}
				else
				{
					EnviroSky.instance.GameTime.ProgressTime = EnviroTime.TimeProgressMode.Simulated;
				}
			}
			else
			{
				EnviroSky.instance.GameTime.ProgressTime = EnviroTime.TimeProgressMode.None;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0004412C File Offset: 0x0004252C
		private void UpdateWeatherSlider()
		{
			if (EnviroSky.instance.Weather.currentActiveWeatherPreset != null)
			{
				for (int i = 0; i < this.weatherDropdown.options.Count; i++)
				{
					if (this.weatherDropdown.options[i].text == EnviroSky.instance.Weather.currentActiveWeatherPreset.Name)
					{
						this.weatherDropdown.value = i;
					}
				}
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000441B4 File Offset: 0x000425B4
		private void UpdateSeasonSlider(EnviroSeasons.Seasons s)
		{
			switch (s)
			{
			case EnviroSeasons.Seasons.Spring:
				this.seasonDropdown.value = 0;
				break;
			case EnviroSeasons.Seasons.Summer:
				this.seasonDropdown.value = 1;
				break;
			case EnviroSeasons.Seasons.Autumn:
				this.seasonDropdown.value = 2;
				break;
			case EnviroSeasons.Seasons.Winter:
				this.seasonDropdown.value = 3;
				break;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00044220 File Offset: 0x00042620
		private void Update()
		{
			if (!EnviroSky.instance.started)
			{
				return;
			}
			if (!this.started)
			{
				base.StartCoroutine(this.setupDrodown());
			}
			this.timeText.text = EnviroSky.instance.GetTimeString();
			this.ChangeQualitySlider();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00044270 File Offset: 0x00042670
		private void LateUpdate()
		{
			this.sliderTime.value = EnviroSky.instance.internalHour / 24f;
		}

		// Token: 0x04000E01 RID: 3585
		public Slider sliderTime;

		// Token: 0x04000E02 RID: 3586
		public Slider sliderQuality;

		// Token: 0x04000E03 RID: 3587
		public Text timeText;

		// Token: 0x04000E04 RID: 3588
		public Dropdown weatherDropdown;

		// Token: 0x04000E05 RID: 3589
		public Dropdown seasonDropdown;

		// Token: 0x04000E06 RID: 3590
		private bool seasonmode = true;

		// Token: 0x04000E07 RID: 3591
		private bool fastdays;

		// Token: 0x04000E08 RID: 3592
		private bool started;
	}
}
