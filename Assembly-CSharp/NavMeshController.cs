using System;
using System.Runtime.CompilerServices;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000BA8 RID: 2984
[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshController : MonoBehaviour
{
	// Token: 0x170010AF RID: 4271
	// (get) Token: 0x0600598C RID: 22924 RVA: 0x00265B52 File Offset: 0x00263F52
	public NavMeshAgent Agent
	{
		[CompilerGenerated]
		get
		{
			return this._agent;
		}
	}

	// Token: 0x170010B0 RID: 4272
	// (get) Token: 0x0600598D RID: 22925 RVA: 0x00265B5A File Offset: 0x00263F5A
	public bool AgentActive
	{
		[CompilerGenerated]
		get
		{
			return this._agent != null && this._agent.isActiveAndEnabled;
		}
	}

	// Token: 0x170010B1 RID: 4273
	// (get) Token: 0x0600598E RID: 22926 RVA: 0x00265B7B File Offset: 0x00263F7B
	// (set) Token: 0x0600598F RID: 22927 RVA: 0x00265B83 File Offset: 0x00263F83
	public string AnimParamName
	{
		get
		{
			return this._animParamName;
		}
		set
		{
			this._animParamName = value;
			this._animParamNameHashCode = Animator.StringToHash(this._animParamName);
		}
	}

	// Token: 0x170010B2 RID: 4274
	// (get) Token: 0x06005990 RID: 22928 RVA: 0x00265B9D File Offset: 0x00263F9D
	public int AnimParamNameHashCode
	{
		[CompilerGenerated]
		get
		{
			return this._animParamNameHashCode;
		}
	}

	// Token: 0x170010B3 RID: 4275
	// (get) Token: 0x06005991 RID: 22929 RVA: 0x00265BA5 File Offset: 0x00263FA5
	// (set) Token: 0x06005992 RID: 22930 RVA: 0x00265BB2 File Offset: 0x00263FB2
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x170010B4 RID: 4276
	// (get) Token: 0x06005993 RID: 22931 RVA: 0x00265BC0 File Offset: 0x00263FC0
	// (set) Token: 0x06005994 RID: 22932 RVA: 0x00265BCD File Offset: 0x00263FCD
	public Vector3 EulerAngles
	{
		get
		{
			return base.transform.eulerAngles;
		}
		set
		{
			base.transform.eulerAngles = value;
		}
	}

	// Token: 0x170010B5 RID: 4277
	// (get) Token: 0x06005995 RID: 22933 RVA: 0x00265BDB File Offset: 0x00263FDB
	// (set) Token: 0x06005996 RID: 22934 RVA: 0x00265BE8 File Offset: 0x00263FE8
	public Quaternion Rotation
	{
		get
		{
			return base.transform.rotation;
		}
		set
		{
			base.transform.rotation = value;
		}
	}

	// Token: 0x170010B6 RID: 4278
	// (get) Token: 0x06005997 RID: 22935 RVA: 0x00265BF6 File Offset: 0x00263FF6
	// (set) Token: 0x06005998 RID: 22936 RVA: 0x00265BFE File Offset: 0x00263FFE
	public Vector3 Velocity { get; private set; } = Vector3.zero;

	// Token: 0x170010B7 RID: 4279
	// (get) Token: 0x06005999 RID: 22937 RVA: 0x00265C07 File Offset: 0x00264007
	public Vector3 Forward
	{
		[CompilerGenerated]
		get
		{
			return base.transform.forward;
		}
	}

	// Token: 0x170010B8 RID: 4280
	// (get) Token: 0x0600599A RID: 22938 RVA: 0x00265C14 File Offset: 0x00264014
	public Vector3 Up
	{
		[CompilerGenerated]
		get
		{
			return base.transform.up;
		}
	}

	// Token: 0x170010B9 RID: 4281
	// (get) Token: 0x0600599B RID: 22939 RVA: 0x00265C21 File Offset: 0x00264021
	public Vector3 Right
	{
		[CompilerGenerated]
		get
		{
			return base.transform.right;
		}
	}

	// Token: 0x170010BA RID: 4282
	// (get) Token: 0x0600599C RID: 22940 RVA: 0x00265C2E File Offset: 0x0026402E
	public Vector3 Destination
	{
		[CompilerGenerated]
		get
		{
			return this.Agent.destination;
		}
	}

	// Token: 0x170010BB RID: 4283
	// (get) Token: 0x0600599D RID: 22941 RVA: 0x00265C3B File Offset: 0x0026403B
	// (set) Token: 0x0600599E RID: 22942 RVA: 0x00265C48 File Offset: 0x00264048
	public int AreaMask
	{
		get
		{
			return this.Agent.areaMask;
		}
		set
		{
			this.Agent.areaMask = value;
		}
	}

	// Token: 0x170010BC RID: 4284
	// (get) Token: 0x0600599F RID: 22943 RVA: 0x00265C56 File Offset: 0x00264056
	// (set) Token: 0x060059A0 RID: 22944 RVA: 0x00265C5E File Offset: 0x0026405E
	public bool ConsiderYAxis { get; set; }

	// Token: 0x170010BD RID: 4285
	// (get) Token: 0x060059A1 RID: 22945 RVA: 0x00265C67 File Offset: 0x00264067
	// (set) Token: 0x060059A2 RID: 22946 RVA: 0x00265C6F File Offset: 0x0026406F
	public bool MoveUpdateEnabled { get; set; } = true;

	// Token: 0x170010BE RID: 4286
	// (get) Token: 0x060059A3 RID: 22947 RVA: 0x00265C78 File Offset: 0x00264078
	// (set) Token: 0x060059A4 RID: 22948 RVA: 0x00265C80 File Offset: 0x00264080
	public bool AnimationUpdateEnabled { get; set; } = true;

	// Token: 0x170010BF RID: 4287
	// (get) Token: 0x060059A5 RID: 22949 RVA: 0x00265C89 File Offset: 0x00264089
	public bool AnimatorEnabled
	{
		[CompilerGenerated]
		get
		{
			return this.Animator != null && this.Animator.isActiveAndEnabled;
		}
	}

	// Token: 0x060059A6 RID: 22950 RVA: 0x00265CAC File Offset: 0x002640AC
	private void Awake()
	{
		if (this._agent == null)
		{
			this._agent = this.GetOrAddComponent<NavMeshAgent>();
		}
		this._animParamNameHashCode = Animator.StringToHash(this._animParamName);
		NavMeshAgent agent = this.Agent;
		float num = 0f;
		this.Agent.acceleration = num;
		num = num;
		this.Agent.angularSpeed = num;
		agent.speed = num;
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		where this.MoveUpdateEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnUpdate();
		});
	}

	// Token: 0x170010C0 RID: 4288
	// (get) Token: 0x060059A7 RID: 22951 RVA: 0x00265D57 File Offset: 0x00264157
	public Vector3 SteeringTarget
	{
		[CompilerGenerated]
		get
		{
			return this.Agent.steeringTarget;
		}
	}

	// Token: 0x170010C1 RID: 4289
	// (get) Token: 0x060059A8 RID: 22952 RVA: 0x00265D64 File Offset: 0x00264164
	public bool IsReached
	{
		[CompilerGenerated]
		get
		{
			return this.RemainingDistance <= this.StopDistance;
		}
	}

	// Token: 0x170010C2 RID: 4290
	// (get) Token: 0x060059A9 RID: 22953 RVA: 0x00265D77 File Offset: 0x00264177
	public float RemainingDistance
	{
		[CompilerGenerated]
		get
		{
			return this.Agent.remainingDistance;
		}
	}

	// Token: 0x170010C3 RID: 4291
	// (get) Token: 0x060059AA RID: 22954 RVA: 0x00265D84 File Offset: 0x00264184
	public bool HasDestination
	{
		[CompilerGenerated]
		get
		{
			return this.Agent.hasPath || 0f < this.RemainingDistance;
		}
	}

	// Token: 0x170010C4 RID: 4292
	// (get) Token: 0x060059AB RID: 22955 RVA: 0x00265DA6 File Offset: 0x002641A6
	public bool IsMoving
	{
		[CompilerGenerated]
		get
		{
			return this.HasDestination && !this.Agent.isStopped;
		}
	}

	// Token: 0x060059AC RID: 22956 RVA: 0x00265DC4 File Offset: 0x002641C4
	public bool SetDestination(Vector3 _point, bool _isStopped = false)
	{
		if (!this.AgentActive)
		{
			return false;
		}
		this.Agent.isStopped = _isStopped;
		return this.Agent.SetDestination(_point);
	}

	// Token: 0x060059AD RID: 22957 RVA: 0x00265DEC File Offset: 0x002641EC
	public bool SetDestinationWithPathCheck(Vector3 _point, float _distance, bool _isStopped = false)
	{
		NavMeshPath navMeshPath = new NavMeshPath();
		NavMeshHit navMeshHit = default(NavMeshHit);
		return this.CalculatePath(_point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete && this.FindClosestEdge(_point, out navMeshHit) && navMeshHit.hit && _distance <= navMeshHit.distance && this.SetDestination(_point, _isStopped);
	}

	// Token: 0x060059AE RID: 22958 RVA: 0x00265E52 File Offset: 0x00264252
	public void ResetPath()
	{
		if (!this.AgentActive)
		{
			return;
		}
		if (this.Agent.hasPath)
		{
			this.Agent.ResetPath();
		}
	}

	// Token: 0x060059AF RID: 22959 RVA: 0x00265E7B File Offset: 0x0026427B
	public void Resume()
	{
		if (this.AgentActive && this.Agent.isStopped)
		{
			this.Agent.isStopped = false;
		}
	}

	// Token: 0x060059B0 RID: 22960 RVA: 0x00265EA4 File Offset: 0x002642A4
	public void Stop()
	{
		if (this.AgentActive && !this.Agent.isStopped)
		{
			this.Agent.isStopped = true;
		}
	}

	// Token: 0x060059B1 RID: 22961 RVA: 0x00265ECD File Offset: 0x002642CD
	public void Refresh()
	{
		if (this.AgentActive)
		{
			this.Agent.isStopped = true;
			this.Agent.ResetPath();
		}
	}

	// Token: 0x060059B2 RID: 22962 RVA: 0x00265EF1 File Offset: 0x002642F1
	public bool CalculatePath(Vector3 _targetPosition, NavMeshPath _path)
	{
		return !(this.Agent == null) && this.Agent.CalculatePath(_targetPosition, _path);
	}

	// Token: 0x060059B3 RID: 22963 RVA: 0x00265F13 File Offset: 0x00264313
	public bool FindClosestEdge(Vector3 _point, out NavMeshHit _hit)
	{
		return NavMesh.FindClosestEdge(_point, out _hit, this.Agent.areaMask);
	}

	// Token: 0x060059B4 RID: 22964 RVA: 0x00265F28 File Offset: 0x00264328
	public void ChangeSpeedLinear(float _speed, float _second)
	{
		if (this.changeSpeedLinearDisposable != null)
		{
			this.changeSpeedLinearDisposable.Dispose();
		}
		float _prevSpeed = this.Speed;
		float _startTime = Time.realtimeSinceStartup;
		this.changeSpeedLinearDisposable = ObservableEasing.Linear(_second, false).TakeUntilDestroy(base.gameObject).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
		{
			this.Speed = Mathf.Lerp(_prevSpeed, _speed, x.Value);
		}, delegate()
		{
		});
	}

	// Token: 0x060059B5 RID: 22965 RVA: 0x00265FB4 File Offset: 0x002643B4
	public void ChangeAccelerationLinear(float _acceleration, float _second)
	{
		if (this.changeAccelerationLinearDisposable != null)
		{
			this.changeAccelerationLinearDisposable.Dispose();
		}
		float _prevAcceleration = this.Acceleration;
		float _startTime = Time.realtimeSinceStartup;
		this.changeAccelerationLinearDisposable = ObservableEasing.Linear(_second, false).TakeUntilDestroy(base.gameObject).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
		{
			this.Acceleration = Mathf.Lerp(_prevAcceleration, _acceleration, x.Value);
		}, delegate()
		{
		});
	}

	// Token: 0x060059B6 RID: 22966 RVA: 0x00266040 File Offset: 0x00264440
	public void SetEnabled(bool _enabled)
	{
		if (_enabled)
		{
			if (base.enabled != _enabled)
			{
				base.enabled = _enabled;
			}
			if (this.Agent.enabled != _enabled)
			{
				this.Agent.enabled = _enabled;
			}
		}
		else
		{
			if (this.Agent.enabled != _enabled)
			{
				this.Agent.enabled = _enabled;
			}
			if (base.enabled != _enabled)
			{
				base.enabled = _enabled;
			}
		}
	}

	// Token: 0x060059B7 RID: 22967 RVA: 0x002660B8 File Offset: 0x002644B8
	public void Move(Vector3 _velocity)
	{
		if (this.AgentActive)
		{
			NavMeshAgent agent = this.Agent;
			this.Velocity = _velocity;
			agent.Move(_velocity);
		}
		else
		{
			Vector3 position = this.Position;
			this.Velocity = _velocity;
			this.Position = position + _velocity;
		}
	}

	// Token: 0x060059B8 RID: 22968 RVA: 0x00266108 File Offset: 0x00264508
	private void OnUpdate()
	{
		Vector3 zero = Vector3.zero;
		this.Velocity = zero;
		Vector3 vector = zero;
		float num = 0f;
		float deltaTime = Time.deltaTime;
		Vector3 destination = this.Destination;
		if (!this.AgentActive || this.Agent.pathPending || this.Agent.isStopped)
		{
			vector.x = (vector.y = (vector.z = 0f));
			this.speed = 0f;
		}
		else
		{
			this.speed = Mathf.Clamp(this.speed + this.Acceleration * deltaTime, 0f, this.Speed);
			num = this.speed * deltaTime;
			Vector3 steeringTarget = this.Agent.steeringTarget;
			vector = steeringTarget - this.Position;
			vector.y = 0f;
			float num2 = this.AngularSpeed * deltaTime;
			if (!this.IsReached)
			{
				float num3 = Vector3.SignedAngle(this.Forward, vector, Vector3.up);
				if (this.TurnAngle < Mathf.Abs(num3))
				{
					float b = this.TurnAngularSpeed * deltaTime;
					num2 = Mathf.Min(Mathf.Abs(num3), b);
					base.transform.Rotate(0f, num2 * Mathf.Sign(num3), 0f);
					vector = Vector3.zero;
					num = (this.speed = 0f);
				}
				else
				{
					if (this.RemainingDistance < this.SpeedDownDistance)
					{
						num *= 1f - Mathf.Abs(num3) / this.TurnAngle;
					}
					if (vector.sqrMagnitude < num * num)
					{
						num = vector.magnitude;
						base.transform.Rotate(0f, num3, 0f);
					}
					else
					{
						num2 = Mathf.Min(Mathf.Abs(num3), num2);
						base.transform.Rotate(0f, num2 * Mathf.Sign(num3), 0f);
					}
					vector = this.Forward * num;
				}
			}
			else
			{
				num = (this.speed = 0f);
				vector = Vector3.zero;
				this.Agent.ResetPath();
				this.Agent.isStopped = true;
			}
		}
		this.Velocity = vector;
		this.Agent.Move(vector);
		if (this.AnimatorEnabled && this.AnimationUpdateEnabled)
		{
			this.AnimationUpdate(num);
		}
	}

	// Token: 0x060059B9 RID: 22969 RVA: 0x00266380 File Offset: 0x00264780
	private void SetAnimator(Animator _animator)
	{
		this.Animator = _animator;
	}

	// Token: 0x060059BA RID: 22970 RVA: 0x0026638C File Offset: 0x0026478C
	private void AnimationUpdate(float _speed)
	{
		float num = this.Speed * Time.deltaTime;
		if (num == 0f)
		{
			this.Animator.SetFloat(this.AnimParamNameHashCode, 0f);
			return;
		}
		float num2 = _speed / num * this.Speed2Anim;
		if (num2 <= this.StopSpeed)
		{
			this.Animator.SetFloat(this.AnimParamNameHashCode, 0f);
			return;
		}
		this.Animator.SetFloat(this.AnimParamNameHashCode, num2);
	}

	// Token: 0x040051C9 RID: 20937
	[SerializeField]
	private NavMeshAgent _agent;

	// Token: 0x040051CA RID: 20938
	[Tooltip("最大移動速度")]
	[MinValue(0.10000000149011612)]
	public float Speed = 10f;

	// Token: 0x040051CB RID: 20939
	[Tooltip("加速度")]
	[MinValue(0.10000000149011612)]
	public float Acceleration = 200f;

	// Token: 0x040051CC RID: 20940
	[Tooltip("通常の旋回速度")]
	[MinValue(1.0)]
	public float AngularSpeed = 200f;

	// Token: 0x040051CD RID: 20941
	[Tooltip("ターンする時の角度差")]
	[MinValue(1.0)]
	[MaxValue(360.0)]
	public float TurnAngle = 45f;

	// Token: 0x040051CE RID: 20942
	[Tooltip("ターン時の旋回速度")]
	public float TurnAngularSpeed = 1000f;

	// Token: 0x040051CF RID: 20943
	[Tooltip("減速距離")]
	public float SpeedDownDistance = 2f;

	// Token: 0x040051D0 RID: 20944
	[Tooltip("停止距離")]
	public float StopDistance = 1f;

	// Token: 0x040051D1 RID: 20945
	[Header("アニメーション")]
	public Animator Animator;

	// Token: 0x040051D2 RID: 20946
	[SerializeField]
	[Tooltip("変動させるアニメーションのパラメーター名")]
	private string _animParamName = string.Empty;

	// Token: 0x040051D3 RID: 20947
	[SerializeField]
	[ReadOnly]
	private int _animParamNameHashCode;

	// Token: 0x040051D4 RID: 20948
	[Tooltip("移動速度とアニメーション速度の変換率")]
	public float Speed2Anim = 1f;

	// Token: 0x040051D5 RID: 20949
	[Tooltip("アニメを停止とみなす速度")]
	public float StopSpeed = 0.01f;

	// Token: 0x040051DA RID: 20954
	private IDisposable changeSpeedLinearDisposable;

	// Token: 0x040051DB RID: 20955
	private IDisposable changeAccelerationLinearDisposable;

	// Token: 0x040051DC RID: 20956
	private float speed;

	// Token: 0x02000BA9 RID: 2985
	public enum DistanceTypes
	{
		// Token: 0x040051DE RID: 20958
		XZ,
		// Token: 0x040051DF RID: 20959
		XYZ,
		// Token: 0x040051E0 RID: 20960
		CutHeight
	}
}
