using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
[ExecuteInEditMode]
public class EffectsParticleSystemScaler : MonoBehaviour
{
	// Token: 0x06001467 RID: 5223 RVA: 0x0007FE12 File Offset: 0x0007E212
	private void Start()
	{
		this.oldScale = this.particlesScale;
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x0007FE20 File Offset: 0x0007E220
	private void Update()
	{
	}

	// Token: 0x04001756 RID: 5974
	public float particlesScale = 1f;

	// Token: 0x04001757 RID: 5975
	private float oldScale;
}
