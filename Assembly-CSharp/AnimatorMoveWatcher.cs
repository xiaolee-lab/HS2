using System;
using UnityEngine;

// Token: 0x02000C70 RID: 3184
public class AnimatorMoveWatcher : MonoBehaviour
{
	// Token: 0x06006852 RID: 26706 RVA: 0x002C8710 File Offset: 0x002C6B10
	private void OnAnimatorMove()
	{
		base.transform.position += this._animator.deltaPosition;
		base.transform.rotation *= this._animator.deltaRotation;
	}

	// Token: 0x06006853 RID: 26707 RVA: 0x002C875F File Offset: 0x002C6B5F
	private void Reset()
	{
		this._animator = base.GetComponent<Animator>();
	}

	// Token: 0x04005928 RID: 22824
	[SerializeField]
	private Animator _animator;
}
