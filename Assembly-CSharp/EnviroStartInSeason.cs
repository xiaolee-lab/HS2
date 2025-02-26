using System;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class EnviroStartInSeason : MonoBehaviour
{
	// Token: 0x06000FAD RID: 4013 RVA: 0x00057D34 File Offset: 0x00056134
	private void Start()
	{
		if (EnviroSky.instance != null)
		{
			switch (this.startInSeason)
			{
			case EnviroSeasons.Seasons.Spring:
				EnviroSky.instance.GameTime.Days = 1;
				break;
			case EnviroSeasons.Seasons.Summer:
				EnviroSky.instance.GameTime.Days = (int)EnviroSky.instance.seasonsSettings.SpringInDays;
				break;
			case EnviroSeasons.Seasons.Autumn:
				EnviroSky.instance.GameTime.Days = (int)EnviroSky.instance.seasonsSettings.SpringInDays + (int)EnviroSky.instance.seasonsSettings.SummerInDays;
				break;
			case EnviroSeasons.Seasons.Winter:
				EnviroSky.instance.GameTime.Days = (int)EnviroSky.instance.seasonsSettings.SpringInDays + (int)EnviroSky.instance.seasonsSettings.SummerInDays + (int)EnviroSky.instance.seasonsSettings.AutumnInDays;
				break;
			}
		}
	}

	// Token: 0x0400114F RID: 4431
	public EnviroSeasons.Seasons startInSeason;
}
