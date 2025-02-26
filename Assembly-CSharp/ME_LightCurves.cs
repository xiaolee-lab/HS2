using System;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class ME_LightCurves : MonoBehaviour
{
	// Token: 0x0600138C RID: 5004 RVA: 0x00078D1E File Offset: 0x0007711E
	private void Awake()
	{
		this.lightSource = base.GetComponent<Light>();
		this.lightSource.intensity = this.LightCurve.Evaluate(0f);
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x00078D47 File Offset: 0x00077147
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x00078D5C File Offset: 0x0007715C
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float intensity = this.LightCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.lightSource.intensity = intensity;
		}
		if (num >= this.GraphTimeMultiplier)
		{
			if (this.IsLoop)
			{
				this.startTime = Time.time;
			}
			else
			{
				this.canUpdate = false;
			}
		}
	}

	// Token: 0x040015E7 RID: 5607
	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x040015E8 RID: 5608
	public float GraphTimeMultiplier = 1f;

	// Token: 0x040015E9 RID: 5609
	public float GraphIntensityMultiplier = 1f;

	// Token: 0x040015EA RID: 5610
	public bool IsLoop;

	// Token: 0x040015EB RID: 5611
	private bool canUpdate;

	// Token: 0x040015EC RID: 5612
	private float startTime;

	// Token: 0x040015ED RID: 5613
	private Light lightSource;
}
