using System;
using UnityEngine;

// Token: 0x0200035A RID: 858
[Serializable]
public class EnviroVegetationStage
{
	// Token: 0x04001046 RID: 4166
	[Range(0f, 100f)]
	public float minAgePercent;

	// Token: 0x04001047 RID: 4167
	public EnviroVegetationStage.GrowState growAction;

	// Token: 0x04001048 RID: 4168
	public GameObject GrowGameobjectSpring;

	// Token: 0x04001049 RID: 4169
	public GameObject GrowGameobjectSummer;

	// Token: 0x0400104A RID: 4170
	public GameObject GrowGameobjectAutumn;

	// Token: 0x0400104B RID: 4171
	public GameObject GrowGameobjectWinter;

	// Token: 0x0400104C RID: 4172
	public bool billboard;

	// Token: 0x0200035B RID: 859
	public enum GrowState
	{
		// Token: 0x0400104E RID: 4174
		Grow,
		// Token: 0x0400104F RID: 4175
		Stay
	}
}
