using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class EnviroSetSystemTime : MonoBehaviour
{
	// Token: 0x06000FAB RID: 4011 RVA: 0x00057D08 File Offset: 0x00056108
	private void Start()
	{
		if (EnviroSky.instance != null)
		{
			EnviroSky.instance.SetTime(DateTime.Now);
		}
	}
}
