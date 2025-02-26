using System;

// Token: 0x0200035C RID: 860
[Serializable]
public class EnviroVegetationAge
{
	// Token: 0x04001050 RID: 4176
	public float maxAgeHours = 24f;

	// Token: 0x04001051 RID: 4177
	public float maxAgeDays = 60f;

	// Token: 0x04001052 RID: 4178
	public float maxAgeYears;

	// Token: 0x04001053 RID: 4179
	public bool randomStartAge;

	// Token: 0x04001054 RID: 4180
	public float startAgeinHours;

	// Token: 0x04001055 RID: 4181
	public double birthdayInHours;

	// Token: 0x04001056 RID: 4182
	public bool Loop = true;

	// Token: 0x04001057 RID: 4183
	public int LoopFromGrowStage;
}
