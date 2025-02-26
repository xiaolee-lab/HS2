using System;
using System.Runtime.CompilerServices;
using ReMotion;
using RootMotion.FinalIK;
using UniRx;
using UnityEngine;

namespace AIProject.RootMotion
{
	// Token: 0x02000C71 RID: 3185
	public class HandsHolder : MonoBehaviour
	{
		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06006855 RID: 26709 RVA: 0x002C8796 File Offset: 0x002C6B96
		// (set) Token: 0x06006856 RID: 26710 RVA: 0x002C879E File Offset: 0x002C6B9E
		public Animator RightHandAnimator
		{
			get
			{
				return this._rightHandAnimator;
			}
			set
			{
				this._rightHandAnimator = value;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06006857 RID: 26711 RVA: 0x002C87A7 File Offset: 0x002C6BA7
		// (set) Token: 0x06006858 RID: 26712 RVA: 0x002C87AF File Offset: 0x002C6BAF
		public Animator LeftHandAnimator
		{
			get
			{
				return this._leftHandAnimator;
			}
			set
			{
				this._leftHandAnimator = value;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06006859 RID: 26713 RVA: 0x002C87B8 File Offset: 0x002C6BB8
		// (set) Token: 0x0600685A RID: 26714 RVA: 0x002C87C0 File Offset: 0x002C6BC0
		public FullBodyBipedIK RightHandIK
		{
			get
			{
				return this._rightHandIK;
			}
			set
			{
				this._rightHandIK = value;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x0600685B RID: 26715 RVA: 0x002C87C9 File Offset: 0x002C6BC9
		// (set) Token: 0x0600685C RID: 26716 RVA: 0x002C87D1 File Offset: 0x002C6BD1
		public FullBodyBipedIK LeftHandIK
		{
			get
			{
				return this._leftHandIK;
			}
			set
			{
				this._leftHandIK = value;
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x0600685D RID: 26717 RVA: 0x002C87DA File Offset: 0x002C6BDA
		// (set) Token: 0x0600685E RID: 26718 RVA: 0x002C87E2 File Offset: 0x002C6BE2
		public Transform RightHandTarget
		{
			get
			{
				return this._rightHandTarget;
			}
			set
			{
				this._rightHandTarget = value;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x0600685F RID: 26719 RVA: 0x002C87EB File Offset: 0x002C6BEB
		// (set) Token: 0x06006860 RID: 26720 RVA: 0x002C87F3 File Offset: 0x002C6BF3
		public Transform LeftHandTarget
		{
			get
			{
				return this._leftHandTarget;
			}
			set
			{
				this._leftHandTarget = value;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06006861 RID: 26721 RVA: 0x002C87FC File Offset: 0x002C6BFC
		// (set) Token: 0x06006862 RID: 26722 RVA: 0x002C8804 File Offset: 0x002C6C04
		public Transform RightElboTarget
		{
			get
			{
				return this._rightElboTarget;
			}
			set
			{
				this._rightElboTarget = value;
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06006863 RID: 26723 RVA: 0x002C880D File Offset: 0x002C6C0D
		// (set) Token: 0x06006864 RID: 26724 RVA: 0x002C8815 File Offset: 0x002C6C15
		public Transform RightLookTarget { get; set; }

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06006865 RID: 26725 RVA: 0x002C881E File Offset: 0x002C6C1E
		// (set) Token: 0x06006866 RID: 26726 RVA: 0x002C8826 File Offset: 0x002C6C26
		public Transform LeftLookTarget { get; set; }

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06006867 RID: 26727 RVA: 0x002C882F File Offset: 0x002C6C2F
		public Transform BaseTransform
		{
			[CompilerGenerated]
			get
			{
				return this._baseTransform;
			}
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06006868 RID: 26728 RVA: 0x002C8837 File Offset: 0x002C6C37
		public Transform TargetTransform
		{
			[CompilerGenerated]
			get
			{
				return this._targetTransform;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06006869 RID: 26729 RVA: 0x002C883F File Offset: 0x002C6C3F
		// (set) Token: 0x0600686A RID: 26730 RVA: 0x002C8847 File Offset: 0x002C6C47
		public float CrossFade
		{
			get
			{
				return this._crossFade;
			}
			set
			{
				this._crossFade = value;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x0600686B RID: 26731 RVA: 0x002C8850 File Offset: 0x002C6C50
		// (set) Token: 0x0600686C RID: 26732 RVA: 0x002C8858 File Offset: 0x002C6C58
		public float Speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this._speed = value;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x002C8861 File Offset: 0x002C6C61
		// (set) Token: 0x0600686E RID: 26734 RVA: 0x002C8869 File Offset: 0x002C6C69
		public float EffectiveDistance
		{
			get
			{
				return this._effectiveDistance;
			}
			set
			{
				this._effectiveDistance = value;
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x0600686F RID: 26735 RVA: 0x002C8872 File Offset: 0x002C6C72
		// (set) Token: 0x06006870 RID: 26736 RVA: 0x002C887A File Offset: 0x002C6C7A
		public float MinDistance { get; set; }

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06006871 RID: 26737 RVA: 0x002C8883 File Offset: 0x002C6C83
		// (set) Token: 0x06006872 RID: 26738 RVA: 0x002C888C File Offset: 0x002C6C8C
		public bool EnabledHolding
		{
			get
			{
				return this._enabledHolding;
			}
			set
			{
				if (this._enabledHolding == value)
				{
					return;
				}
				this._enabledHolding = value;
				if (this._disposable != null)
				{
					this._disposable.Dispose();
				}
				float startWeight = this._weight;
				float num = ((!value) ? startWeight : (1f - startWeight)) * 0.5f;
				if (num <= 0f)
				{
					return;
				}
				if (!value)
				{
					this._enabledTarget = false;
				}
				this._disposable = ObservableEasing.Linear(num, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
				{
					float num2 = (!value) ? Mathf.Lerp(startWeight, 0f, x.Value) : Mathf.Lerp(startWeight, 1f, x.Value);
					if (this._rightHandAnimator != null)
					{
						this._rightHandAnimator.SetLayerWeight(2, num2);
					}
					this._leftHandAnimator.SetLayerWeight(3, num2);
					this._weight = num2;
					if (this._rightHandIK != null)
					{
						this._rightHandIK.solver.rightHandEffector.positionWeight = num2;
						this._rightHandIK.solver.rightHandEffector.rotationWeight = num2;
						if (this._rightElboTarget != null)
						{
							this._rightHandIK.solver.rightArmChain.bendConstraint.weight = num2;
						}
					}
				}, delegate()
				{
					if (value)
					{
						this._enabledTarget = true;
					}
				});
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x002C8966 File Offset: 0x002C6D66
		// (set) Token: 0x06006874 RID: 26740 RVA: 0x002C896E File Offset: 0x002C6D6E
		public float Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x002C8978 File Offset: 0x002C6D78
		public IObservable<Unit> OnUpdateAsOsbervable()
		{
			Subject<Unit> result;
			if ((result = this._update) == null)
			{
				result = (this._update = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x002C89A0 File Offset: 0x002C6DA0
		public IObservable<Unit> OnBeforeLateUpdateAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._beforeLateUpdate) == null)
			{
				result = (this._beforeLateUpdate = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x002C89C8 File Offset: 0x002C6DC8
		public IObservable<Unit> OnAfterLateUpdateAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._afterLateUpdate) == null)
			{
				result = (this._afterLateUpdate = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06006878 RID: 26744 RVA: 0x002C89F0 File Offset: 0x002C6DF0
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x002C8A28 File Offset: 0x002C6E28
		private void OnUpdate()
		{
			if (this.RightLookTarget == null || this.LeftLookTarget == null)
			{
				this.EnabledHolding = false;
				return;
			}
			float num = Vector3.Distance(this._rightHandIK.transform.position, this._leftHandIK.transform.position);
			bool flag = num > this._effectiveDistance;
			Vector3 position = this._rightHandIK.transform.position;
			position.y = 0f;
			Vector3 position2 = this._leftHandIK.transform.position;
			position2.y = 0f;
			Vector3 fromDirection = Vector3.Normalize(position - position2);
			Quaternion quaternion = Quaternion.FromToRotation(fromDirection, this._rightHandIK.transform.forward);
			Quaternion quaternion2 = Quaternion.FromToRotation(fromDirection, this._leftHandIK.transform.forward);
			bool flag2 = quaternion.eulerAngles.y < 30f || quaternion.eulerAngles.y > 150f;
			bool flag3 = quaternion.eulerAngles.y < 30f || quaternion2.eulerAngles.y > 150f;
			Quaternion quaternion3 = Quaternion.FromToRotation(this._rightHandIK.transform.forward, this._leftHandIK.transform.forward);
			float num2 = quaternion3.eulerAngles.y;
			if (num2 > 180f)
			{
				num2 = 360f - quaternion3.eulerAngles.y;
			}
			bool flag4 = num2 > 60f;
			this.EnabledHolding = (!flag && !flag2 && !flag3 && !flag4);
			if (this._update != null)
			{
				this._update.OnNext(Unit.Default);
			}
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x002C8C20 File Offset: 0x002C7020
		private void LateUpdate()
		{
			if (this.RightLookTarget == null || this.LeftLookTarget == null)
			{
				return;
			}
			if (this._beforeLateUpdate != null)
			{
				this._beforeLateUpdate.OnNext(Unit.Default);
			}
			if (this._enabledTarget)
			{
				this._targetTransform.rotation = this._rightHandTarget.rotation;
			}
			if (this._weight > 0f)
			{
				this._rightHandIK.solver.rightHandEffector.target.position = this._rightHandTarget.position;
				this._rightHandIK.solver.rightHandEffector.target.rotation = this._targetTransform.rotation;
				if (this._rightElboTarget != null)
				{
					this._rightHandIK.solver.rightArmChain.bendConstraint.bendGoal.position = this._rightElboTarget.position;
				}
			}
			if (this._afterLateUpdate != null)
			{
				this._afterLateUpdate.OnNext(Unit.Default);
			}
		}

		// Token: 0x04005929 RID: 22825
		[SerializeField]
		private Animator _rightHandAnimator;

		// Token: 0x0400592A RID: 22826
		[SerializeField]
		private Animator _leftHandAnimator;

		// Token: 0x0400592B RID: 22827
		[SerializeField]
		private FullBodyBipedIK _rightHandIK;

		// Token: 0x0400592C RID: 22828
		[SerializeField]
		private FullBodyBipedIK _leftHandIK;

		// Token: 0x0400592D RID: 22829
		[SerializeField]
		private Transform _rightHandTarget;

		// Token: 0x0400592E RID: 22830
		[SerializeField]
		private Transform _leftHandTarget;

		// Token: 0x0400592F RID: 22831
		[SerializeField]
		private Transform _rightElboTarget;

		// Token: 0x04005932 RID: 22834
		[SerializeField]
		private Transform _baseTransform;

		// Token: 0x04005933 RID: 22835
		[SerializeField]
		private Transform _targetTransform;

		// Token: 0x04005934 RID: 22836
		private bool _enabledTarget;

		// Token: 0x04005935 RID: 22837
		[SerializeField]
		[Range(0f, 1f)]
		private float _crossFade;

		// Token: 0x04005936 RID: 22838
		[SerializeField]
		private float _speed = 10f;

		// Token: 0x04005937 RID: 22839
		[SerializeField]
		private float _effectiveDistance = 1f;

		// Token: 0x04005938 RID: 22840
		[SerializeField]
		private Vector3 _offset = Vector3.zero;

		// Token: 0x0400593A RID: 22842
		private float _weight;

		// Token: 0x0400593B RID: 22843
		private bool _enabledHolding;

		// Token: 0x0400593C RID: 22844
		private IDisposable _disposable;

		// Token: 0x0400593D RID: 22845
		private Subject<Unit> _update;

		// Token: 0x0400593E RID: 22846
		private Subject<Unit> _beforeLateUpdate;

		// Token: 0x0400593F RID: 22847
		private Subject<Unit> _afterLateUpdate;
	}
}
