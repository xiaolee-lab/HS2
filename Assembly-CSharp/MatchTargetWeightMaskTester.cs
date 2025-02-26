using System;
using UnityEngine;

// Token: 0x020011C4 RID: 4548
public class MatchTargetWeightMaskTester : MonoBehaviour
{
	// Token: 0x17001F95 RID: 8085
	// (get) Token: 0x06009522 RID: 38178 RVA: 0x003D83BD File Offset: 0x003D67BD
	// (set) Token: 0x06009523 RID: 38179 RVA: 0x003D83C5 File Offset: 0x003D67C5
	public Animator Animator
	{
		get
		{
			return this._animator;
		}
		set
		{
			this._animator = value;
		}
	}

	// Token: 0x17001F96 RID: 8086
	// (get) Token: 0x06009524 RID: 38180 RVA: 0x003D83CE File Offset: 0x003D67CE
	// (set) Token: 0x06009525 RID: 38181 RVA: 0x003D83D6 File Offset: 0x003D67D6
	public string StateName
	{
		get
		{
			return this._stateName;
		}
		set
		{
			this._stateName = value;
		}
	}

	// Token: 0x17001F97 RID: 8087
	// (get) Token: 0x06009526 RID: 38182 RVA: 0x003D83DF File Offset: 0x003D67DF
	// (set) Token: 0x06009527 RID: 38183 RVA: 0x003D83E7 File Offset: 0x003D67E7
	public AvatarTarget TargetBodyPart
	{
		get
		{
			return this._targetBodyPart;
		}
		set
		{
			this._targetBodyPart = value;
		}
	}

	// Token: 0x17001F98 RID: 8088
	// (get) Token: 0x06009528 RID: 38184 RVA: 0x003D83F0 File Offset: 0x003D67F0
	// (set) Token: 0x06009529 RID: 38185 RVA: 0x003D83F8 File Offset: 0x003D67F8
	public MatchTargetWeightMaskTester.TargetParameter[] Targets
	{
		get
		{
			return this._targets;
		}
		set
		{
			this._targets = value;
		}
	}

	// Token: 0x17001F99 RID: 8089
	// (get) Token: 0x0600952A RID: 38186 RVA: 0x003D8401 File Offset: 0x003D6801
	// (set) Token: 0x0600952B RID: 38187 RVA: 0x003D8409 File Offset: 0x003D6809
	public Vector3 PositionWeight
	{
		get
		{
			return this._positionWeight;
		}
		set
		{
			this._positionWeight = value;
		}
	}

	// Token: 0x17001F9A RID: 8090
	// (get) Token: 0x0600952C RID: 38188 RVA: 0x003D8412 File Offset: 0x003D6812
	// (set) Token: 0x0600952D RID: 38189 RVA: 0x003D841A File Offset: 0x003D681A
	public float RotationWeight
	{
		get
		{
			return this._rotationWeight;
		}
		set
		{
			this._rotationWeight = value;
		}
	}

	// Token: 0x17001F9B RID: 8091
	// (get) Token: 0x0600952E RID: 38190 RVA: 0x003D8423 File Offset: 0x003D6823
	// (set) Token: 0x0600952F RID: 38191 RVA: 0x003D842B File Offset: 0x003D682B
	public bool IsPlaying { get; set; }

	// Token: 0x06009530 RID: 38192 RVA: 0x003D8434 File Offset: 0x003D6834
	private void Update()
	{
		AnimatorStateInfo currentAnimatorStateInfo = this._animator.GetCurrentAnimatorStateInfo(0);
		bool flag = currentAnimatorStateInfo.IsName(this._stateName);
		bool flag2 = this._animator.IsInTransition(0);
		if (this.IsPlaying)
		{
			if (flag && currentAnimatorStateInfo.normalizedTime < 0.9f && !flag2)
			{
				MatchTargetWeightMask weightMask = new MatchTargetWeightMask(this._positionWeight, this._rotationWeight);
				foreach (MatchTargetWeightMaskTester.TargetParameter targetParameter in this._targets)
				{
					if (targetParameter.Target != null)
					{
						this._animator.MatchTarget(targetParameter.Target.position, targetParameter.Target.rotation, this._targetBodyPart, weightMask, targetParameter.Start, targetParameter.End);
					}
				}
			}
			if (!flag && this._started)
			{
				this.IsPlaying = false;
			}
			if (!this._started)
			{
				this._started = true;
			}
		}
		else
		{
			this._started = false;
		}
	}

	// Token: 0x06009531 RID: 38193 RVA: 0x003D854C File Offset: 0x003D694C
	public void PlayAnim()
	{
		base.transform.position = Vector3.zero;
		this.Animator.Play(this.StateName, 0, 0f);
		this.IsPlaying = true;
	}

	// Token: 0x06009532 RID: 38194 RVA: 0x003D857C File Offset: 0x003D697C
	private void Reset()
	{
		this._animator = base.GetComponent<Animator>();
	}

	// Token: 0x040077DA RID: 30682
	[SerializeField]
	private Animator _animator;

	// Token: 0x040077DB RID: 30683
	[SerializeField]
	private string _stateName = string.Empty;

	// Token: 0x040077DC RID: 30684
	[SerializeField]
	private AvatarTarget _targetBodyPart;

	// Token: 0x040077DD RID: 30685
	[SerializeField]
	private MatchTargetWeightMaskTester.TargetParameter[] _targets;

	// Token: 0x040077DE RID: 30686
	[Header("Weights")]
	[SerializeField]
	private Vector3 _positionWeight = Vector3.one;

	// Token: 0x040077DF RID: 30687
	[SerializeField]
	private float _rotationWeight;

	// Token: 0x040077E0 RID: 30688
	private MatchTargetWeightMask _weightMask;

	// Token: 0x040077E2 RID: 30690
	private bool _started;

	// Token: 0x020011C5 RID: 4549
	[Serializable]
	public class TargetParameter
	{
		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x06009534 RID: 38196 RVA: 0x003D859D File Offset: 0x003D699D
		// (set) Token: 0x06009535 RID: 38197 RVA: 0x003D85A5 File Offset: 0x003D69A5
		public float Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x06009536 RID: 38198 RVA: 0x003D85AE File Offset: 0x003D69AE
		// (set) Token: 0x06009537 RID: 38199 RVA: 0x003D85B6 File Offset: 0x003D69B6
		public float End
		{
			get
			{
				return this._end;
			}
			set
			{
				this._end = value;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x06009538 RID: 38200 RVA: 0x003D85BF File Offset: 0x003D69BF
		// (set) Token: 0x06009539 RID: 38201 RVA: 0x003D85C7 File Offset: 0x003D69C7
		public Transform Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		// Token: 0x040077E3 RID: 30691
		[SerializeField]
		[Range(0f, 1f)]
		private float _start;

		// Token: 0x040077E4 RID: 30692
		[SerializeField]
		[Range(0f, 1f)]
		private float _end = 1f;

		// Token: 0x040077E5 RID: 30693
		[SerializeField]
		private Transform _target;
	}
}
