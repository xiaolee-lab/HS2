using System;

// Token: 0x0200035D RID: 861
[Serializable]
public class EnviroVegetationSeasons
{
	// Token: 0x04001058 RID: 4184
	public EnviroVegetationSeasons.SeasonAction seasonAction;

	// Token: 0x04001059 RID: 4185
	public bool GrowInSpring = true;

	// Token: 0x0400105A RID: 4186
	public bool GrowInSummer = true;

	// Token: 0x0400105B RID: 4187
	public bool GrowInAutumn = true;

	// Token: 0x0400105C RID: 4188
	public bool GrowInWinter = true;

	// Token: 0x0200035E RID: 862
	public enum SeasonAction
	{
		// Token: 0x0400105E RID: 4190
		SpawnDeadPrefab,
		// Token: 0x0400105F RID: 4191
		Deactivate,
		// Token: 0x04001060 RID: 4192
		Destroy
	}
}
