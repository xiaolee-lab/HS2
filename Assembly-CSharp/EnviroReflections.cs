using System;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class EnviroReflections : MonoBehaviour
{
	// Token: 0x06000FA2 RID: 4002 RVA: 0x00057ABB File Offset: 0x00055EBB
	private void Start()
	{
		if (this.probe == null)
		{
			this.probe = base.GetComponent<ReflectionProbe>();
		}
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00057ADA File Offset: 0x00055EDA
	private void UpdateProbe()
	{
		this.probe.RenderProbe();
		this.lastUpdate = EnviroSky.instance.currentTimeInHours;
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x00057AF8 File Offset: 0x00055EF8
	private void Update()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (EnviroSky.instance.currentTimeInHours > this.lastUpdate + (double)this.ReflectionUpdateInGameHours || EnviroSky.instance.currentTimeInHours < this.lastUpdate - (double)this.ReflectionUpdateInGameHours)
		{
			this.UpdateProbe();
		}
	}

	// Token: 0x04001148 RID: 4424
	public ReflectionProbe probe;

	// Token: 0x04001149 RID: 4425
	public float ReflectionUpdateInGameHours = 1f;

	// Token: 0x0400114A RID: 4426
	private double lastUpdate;
}
