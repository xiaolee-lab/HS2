using System;
using UnityEngine;

// Token: 0x02001120 RID: 4384
public class RepeatButton : MonoBehaviour
{
	// Token: 0x17001F38 RID: 7992
	// (get) Token: 0x0600912B RID: 37163 RVA: 0x003C6F6C File Offset: 0x003C536C
	public bool IsPress
	{
		get
		{
			return this.isPressed;
		}
	}

	// Token: 0x0600912C RID: 37164 RVA: 0x003C6F74 File Offset: 0x003C5374
	private void OnPress(bool isPress)
	{
		this.isPressed = isPress;
		this.nextClick = Time.realtimeSinceStartup + this.interval;
	}

	// Token: 0x0600912D RID: 37165 RVA: 0x003C6F8F File Offset: 0x003C538F
	private void Update()
	{
		if (this.isPressed && Time.realtimeSinceStartup < this.nextClick)
		{
			this.nextClick = Time.realtimeSinceStartup + this.interval;
		}
	}

	// Token: 0x040075C1 RID: 30145
	public float interval = 0.25f;

	// Token: 0x040075C2 RID: 30146
	private float nextClick;

	// Token: 0x040075C3 RID: 30147
	private bool isPressed;
}
