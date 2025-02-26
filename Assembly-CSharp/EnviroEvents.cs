using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200032A RID: 810
public class EnviroEvents : MonoBehaviour
{
	// Token: 0x06000E3E RID: 3646 RVA: 0x00045840 File Offset: 0x00043C40
	private void Start()
	{
		EnviroSky.instance.OnHourPassed += delegate()
		{
			this.HourPassed();
		};
		EnviroSky.instance.OnDayPassed += delegate()
		{
			this.DayPassed();
		};
		EnviroSky.instance.OnYearPassed += delegate()
		{
			this.YearPassed();
		};
		EnviroSky.instance.OnWeatherChanged += delegate(EnviroWeatherPreset type)
		{
			this.WeatherChanged();
		};
		EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
		{
			this.SeasonsChanged();
		};
		EnviroSky.instance.OnNightTime += delegate()
		{
			this.NightTime();
		};
		EnviroSky.instance.OnDayTime += delegate()
		{
			this.DayTime();
		};
		EnviroSky.instance.OnZoneChanged += delegate(EnviroZone zone)
		{
			this.ZoneChanged();
		};
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000458FD File Offset: 0x00043CFD
	private void HourPassed()
	{
		this.onHourPassedActions.Invoke();
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0004590A File Offset: 0x00043D0A
	private void DayPassed()
	{
		this.onDayPassedActions.Invoke();
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00045917 File Offset: 0x00043D17
	private void YearPassed()
	{
		this.onYearPassedActions.Invoke();
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00045924 File Offset: 0x00043D24
	private void WeatherChanged()
	{
		this.onWeatherChangedActions.Invoke();
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00045931 File Offset: 0x00043D31
	private void SeasonsChanged()
	{
		this.onSeasonChangedActions.Invoke();
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0004593E File Offset: 0x00043D3E
	private void NightTime()
	{
		this.onNightActions.Invoke();
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0004594B File Offset: 0x00043D4B
	private void DayTime()
	{
		this.onDayActions.Invoke();
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00045958 File Offset: 0x00043D58
	private void ZoneChanged()
	{
		this.onZoneChangedActions.Invoke();
	}

	// Token: 0x04000E45 RID: 3653
	public EnviroEvents.EnviroActionEvent onHourPassedActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E46 RID: 3654
	public EnviroEvents.EnviroActionEvent onDayPassedActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E47 RID: 3655
	public EnviroEvents.EnviroActionEvent onYearPassedActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E48 RID: 3656
	public EnviroEvents.EnviroActionEvent onWeatherChangedActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E49 RID: 3657
	public EnviroEvents.EnviroActionEvent onSeasonChangedActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E4A RID: 3658
	public EnviroEvents.EnviroActionEvent onNightActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E4B RID: 3659
	public EnviroEvents.EnviroActionEvent onDayActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x04000E4C RID: 3660
	public EnviroEvents.EnviroActionEvent onZoneChangedActions = new EnviroEvents.EnviroActionEvent();

	// Token: 0x0200032B RID: 811
	[Serializable]
	public class EnviroActionEvent : UnityEvent
	{
	}
}
