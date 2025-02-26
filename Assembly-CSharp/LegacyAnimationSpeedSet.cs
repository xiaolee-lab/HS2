using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200118C RID: 4492
public class LegacyAnimationSpeedSet : MonoBehaviour
{
	// Token: 0x06009417 RID: 37911 RVA: 0x003D3150 File Offset: 0x003D1550
	private void Awake()
	{
		this.anim = base.transform.GetComponent<Animation>();
		this.AnmCount = this.anim.GetClipCount();
		this.name_a = new string[this.AnmCount];
		int num = 0;
		IEnumerator enumerator = this.anim.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AnimationState animationState = (AnimationState)obj;
				this.name_a[num++] = animationState.name;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06009418 RID: 37912 RVA: 0x003D31F8 File Offset: 0x003D15F8
	private void Update()
	{
		for (int i = 0; i < this.AnmCount; i++)
		{
			if (this.AnmSpeed.Length <= i)
			{
				break;
			}
			this.anim[this.name_a[i]].speed = this.AnmSpeed[i];
		}
	}

	// Token: 0x04007760 RID: 30560
	public float[] AnmSpeed;

	// Token: 0x04007761 RID: 30561
	private Animation anim;

	// Token: 0x04007762 RID: 30562
	private string[] name_a;

	// Token: 0x04007763 RID: 30563
	private int AnmCount;
}
