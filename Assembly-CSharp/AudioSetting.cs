using System;
using Manager;
using UnityEngine;
using Utility;

// Token: 0x02001137 RID: 4407
[RequireComponent(typeof(AudioSource))]
public sealed class AudioSetting : BaseLoader
{
	// Token: 0x060091E1 RID: 37345 RVA: 0x003C97DE File Offset: 0x003C7BDE
	private void Start()
	{
		if (this.isInit)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x060091E2 RID: 37346 RVA: 0x003C97F4 File Offset: 0x003C7BF4
	public void Init()
	{
		this.isInit = true;
		if (this.no >= 0)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			float num = 0f;
			if (Singleton<Sound>.IsInstance())
			{
				Sound.OutputSettingData outputSettingData = Singleton<Sound>.Instance.AudioSettingData(component, this.no);
				num = outputSettingData.delayTime;
			}
			if (component.playOnAwake)
			{
				component.PlayDelayed(num);
				if (!component.loop && component.clip != null)
				{
					component.PlayEndDestroy(num);
				}
			}
			else
			{
				component.Stop();
			}
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x0400761F RID: 30239
	public int no = -1;

	// Token: 0x04007620 RID: 30240
	private bool isInit;
}
