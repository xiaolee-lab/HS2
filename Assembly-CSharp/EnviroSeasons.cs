using System;
using UnityEngine;

// Token: 0x02000342 RID: 834
[Serializable]
public class EnviroSeasons
{
	// Token: 0x04000F2D RID: 3885
	[Tooltip("When enabled the system will change seasons automaticly when enough days passed.")]
	public bool calcSeasons;

	// Token: 0x04000F2E RID: 3886
	[Tooltip("The current season.")]
	public EnviroSeasons.Seasons currentSeasons;

	// Token: 0x04000F2F RID: 3887
	[HideInInspector]
	public EnviroSeasons.Seasons lastSeason;

	// Token: 0x02000343 RID: 835
	public enum Seasons
	{
		// Token: 0x04000F31 RID: 3889
		Spring,
		// Token: 0x04000F32 RID: 3890
		Summer,
		// Token: 0x04000F33 RID: 3891
		Autumn,
		// Token: 0x04000F34 RID: 3892
		Winter
	}
}
