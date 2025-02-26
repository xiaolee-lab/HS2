using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class AnimationSpeedDebuff : MonoBehaviour
{
	// Token: 0x06001418 RID: 5144 RVA: 0x0007DD3C File Offset: 0x0007C13C
	private void GetAnimatorOnParent(Transform t)
	{
		Animator component = t.parent.GetComponent<Animator>();
		if (component == null)
		{
			if (this.root == t.parent)
			{
				return;
			}
			this.GetAnimatorOnParent(t.parent);
		}
		else
		{
			this.myAnimator = component;
		}
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x0007DD90 File Offset: 0x0007C190
	private void Start()
	{
		this.root = base.transform.root;
		this.GetAnimatorOnParent(base.transform);
		if (this.myAnimator == null)
		{
			return;
		}
		this.oldSpeed = this.myAnimator.speed;
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0007DDE0 File Offset: 0x0007C1E0
	private void Update()
	{
		if (this.myAnimator == null || this.AnimationSpeenOnTime.length == 0)
		{
			return;
		}
		this.time += Time.deltaTime;
		this.myAnimator.speed = Mathf.Clamp01(this.AnimationSpeenOnTime.Evaluate(this.time / this.MaxTime) * this.oldSpeed);
	}

	// Token: 0x040016D2 RID: 5842
	public AnimationCurve AnimationSpeenOnTime;

	// Token: 0x040016D3 RID: 5843
	public float MaxTime = 1f;

	// Token: 0x040016D4 RID: 5844
	private Animator myAnimator;

	// Token: 0x040016D5 RID: 5845
	private Transform root;

	// Token: 0x040016D6 RID: 5846
	private float oldSpeed;

	// Token: 0x040016D7 RID: 5847
	private float time;
}
