using System;
using CTS;
using UnityEngine;

// Token: 0x02000324 RID: 804
[AddComponentMenu("Enviro/Integration/CTS Integration")]
public class EnviroCTSIntegration : MonoBehaviour
{
	// Token: 0x06000E29 RID: 3625 RVA: 0x00044CA0 File Offset: 0x000430A0
	private void Start()
	{
		if (this.ctsWeatherManager == null)
		{
			this.ctsWeatherManager = UnityEngine.Object.FindObjectOfType<CTSWeatherManager>();
		}
		if (this.ctsWeatherManager == null)
		{
			return;
		}
		if (EnviroSky.instance == null)
		{
			return;
		}
		this.daysInYear = EnviroSky.instance.seasonsSettings.SpringInDays + EnviroSky.instance.seasonsSettings.SummerInDays + EnviroSky.instance.seasonsSettings.AutumnInDays + EnviroSky.instance.seasonsSettings.WinterInDays;
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x00044D34 File Offset: 0x00043134
	private void Update()
	{
		if (this.ctsWeatherManager == null || EnviroSky.instance == null)
		{
			return;
		}
		if (this.updateSnow)
		{
			this.ctsWeatherManager.SnowPower = EnviroSky.instance.Weather.curSnowStrength;
		}
		if (this.updateWetness)
		{
			this.ctsWeatherManager.RainPower = EnviroSky.instance.Weather.curWetness;
		}
		if (this.updateSeasons)
		{
			this.ctsWeatherManager.Season = Mathf.Lerp(0f, 4f, EnviroSky.instance.currentDay / this.daysInYear);
		}
	}

	// Token: 0x04000E27 RID: 3623
	public CTSWeatherManager ctsWeatherManager;

	// Token: 0x04000E28 RID: 3624
	public bool updateSnow;

	// Token: 0x04000E29 RID: 3625
	public bool updateWetness;

	// Token: 0x04000E2A RID: 3626
	public bool updateSeasons;

	// Token: 0x04000E2B RID: 3627
	private float daysInYear;
}
