using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B94 RID: 2964
	public class AnimalRootMotion : MonoBehaviour
	{
		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06005849 RID: 22601 RVA: 0x0025F642 File Offset: 0x0025DA42
		// (set) Token: 0x0600584A RID: 22602 RVA: 0x0025F64A File Offset: 0x0025DA4A
		public Animator Animator
		{
			get
			{
				return this.animator;
			}
			set
			{
				this.animator = value;
				this.ActiveAnimator = (this.animator != null);
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x0600584B RID: 22603 RVA: 0x0025F665 File Offset: 0x0025DA65
		// (set) Token: 0x0600584C RID: 22604 RVA: 0x0025F66D File Offset: 0x0025DA6D
		public bool ActiveAnimator { get; private set; }

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x0600584D RID: 22605 RVA: 0x0025F676 File Offset: 0x0025DA76
		// (set) Token: 0x0600584E RID: 22606 RVA: 0x0025F67E File Offset: 0x0025DA7E
		public bool UpdateRootMotion { get; set; } = true;

		// Token: 0x0600584F RID: 22607 RVA: 0x0025F687 File Offset: 0x0025DA87
		private void Awake()
		{
			if (this.Animator == null)
			{
				this.Animator = base.GetComponent<Animator>();
			}
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x0025F6A8 File Offset: 0x0025DAA8
		private void OnAnimatorMove()
		{
			if (!this.ActiveAnimator || !this.UpdateRootMotion)
			{
				return;
			}
			Vector3 rootPosition = this.Animator.rootPosition;
			Quaternion rootRotation = this.Animator.rootRotation;
			if (this.OnMove != null)
			{
				this.OnMove(rootPosition, rootRotation);
			}
		}

		// Token: 0x04005106 RID: 20742
		[SerializeField]
		private Animator animator;

		// Token: 0x04005109 RID: 20745
		public Action<Vector3, Quaternion> OnMove;
	}
}
