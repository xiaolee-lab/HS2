using System;
using UnityEngine;

// Token: 0x02000378 RID: 888
public class EnviroTrigger : MonoBehaviour
{
	// Token: 0x06000FB9 RID: 4025 RVA: 0x00058569 File Offset: 0x00056969
	private void Start()
	{
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0005856B File Offset: 0x0005696B
	private void Update()
	{
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00058570 File Offset: 0x00056970
	private void OnTriggerEnter(Collider col)
	{
		if (EnviroSky.instance.weatherSettings.useTag)
		{
			if (col.gameObject.tag == EnviroSky.instance.gameObject.tag)
			{
				this.EnterExit();
			}
		}
		else if (col.gameObject.GetComponent<EnviroSky>())
		{
			this.EnterExit();
		}
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x000585DC File Offset: 0x000569DC
	private void OnTriggerExit(Collider col)
	{
		if (this.myZone.zoneTriggerType == EnviroInterior.ZoneTriggerType.Zone)
		{
			if (EnviroSky.instance.weatherSettings.useTag)
			{
				if (col.gameObject.tag == EnviroSky.instance.gameObject.tag)
				{
					this.EnterExit();
				}
			}
			else if (col.gameObject.GetComponent<EnviroSky>())
			{
				this.EnterExit();
			}
		}
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x00058658 File Offset: 0x00056A58
	private void EnterExit()
	{
		if (EnviroSky.instance.lastInteriorZone != this.myZone)
		{
			if (EnviroSky.instance.lastInteriorZone != null)
			{
				EnviroSky.instance.lastInteriorZone.StopAllFading();
			}
			this.myZone.Enter();
		}
		else if (!EnviroSky.instance.interiorMode)
		{
			this.myZone.Enter();
		}
		else
		{
			this.myZone.Exit();
		}
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000586DD File Offset: 0x00056ADD
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.transform.worldToLocalMatrix;
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
	}

	// Token: 0x04001167 RID: 4455
	public EnviroInterior myZone;

	// Token: 0x04001168 RID: 4456
	public string Name;
}
