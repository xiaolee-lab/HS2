using System;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class EnviroDayNightSwitch : MonoBehaviour
{
	// Token: 0x06000F8B RID: 3979 RVA: 0x00056C68 File Offset: 0x00055068
	private void Start()
	{
		this.lightsArray = base.GetComponentsInChildren<Light>();
		EnviroSky.instance.OnDayTime += delegate()
		{
			this.Deactivate();
		};
		EnviroSky.instance.OnNightTime += delegate()
		{
			this.Activate();
		};
		if (EnviroSky.instance.isNight)
		{
			this.Activate();
		}
		else
		{
			this.Deactivate();
		}
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00056CD0 File Offset: 0x000550D0
	private void Activate()
	{
		for (int i = 0; i < this.lightsArray.Length; i++)
		{
			this.lightsArray[i].enabled = true;
		}
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x00056D04 File Offset: 0x00055104
	private void Deactivate()
	{
		for (int i = 0; i < this.lightsArray.Length; i++)
		{
			this.lightsArray[i].enabled = false;
		}
	}

	// Token: 0x04001111 RID: 4369
	private Light[] lightsArray;
}
