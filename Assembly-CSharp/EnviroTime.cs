using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
[Serializable]
public class EnviroTime
{
	// Token: 0x04000F5B RID: 3931
	[Tooltip("None = No time auto time progressing, Simulated = Time calculated with DayLenghtInMinutes, SystemTime = uses your systemTime.")]
	public EnviroTime.TimeProgressMode ProgressTime = EnviroTime.TimeProgressMode.Simulated;

	// Token: 0x04000F5C RID: 3932
	[Tooltip("Current Time: minutes")]
	[Range(0f, 60f)]
	public int Seconds;

	// Token: 0x04000F5D RID: 3933
	[Tooltip("Current Time: minutes")]
	[Range(0f, 60f)]
	public int Minutes;

	// Token: 0x04000F5E RID: 3934
	[Tooltip("Current Time: hours")]
	[Range(0f, 24f)]
	public int Hours = 12;

	// Token: 0x04000F5F RID: 3935
	[Tooltip("Current Time: Days")]
	public int Days = 1;

	// Token: 0x04000F60 RID: 3936
	[Tooltip("Current Time: Years")]
	public int Years = 1;

	// Token: 0x04000F61 RID: 3937
	[Space(20f)]
	[Tooltip("Day lenght in realtime minutes.")]
	public float DayLengthInMinutes = 5f;

	// Token: 0x04000F62 RID: 3938
	[Tooltip("Night lenght in realtime minutes.")]
	public float NightLengthInMinutes = 5f;

	// Token: 0x04000F63 RID: 3939
	[Range(-13f, 13f)]
	[Tooltip("Time offset for timezones")]
	public int utcOffset;

	// Token: 0x04000F64 RID: 3940
	[Range(-90f, 90f)]
	[Tooltip("-90,  90   Horizontal earth lines")]
	public float Latitude;

	// Token: 0x04000F65 RID: 3941
	[Range(-180f, 180f)]
	[Tooltip("-180, 180  Vertical earth line")]
	public float Longitude;

	// Token: 0x04000F66 RID: 3942
	[HideInInspector]
	public float solarTime;

	// Token: 0x04000F67 RID: 3943
	[HideInInspector]
	public float lunarTime;

	// Token: 0x02000349 RID: 841
	public enum TimeProgressMode
	{
		// Token: 0x04000F69 RID: 3945
		None,
		// Token: 0x04000F6A RID: 3946
		Simulated,
		// Token: 0x04000F6B RID: 3947
		OneDay,
		// Token: 0x04000F6C RID: 3948
		SystemTime
	}
}
