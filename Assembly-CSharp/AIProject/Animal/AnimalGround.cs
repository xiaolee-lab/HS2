using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using AIProject.Animal.Resources;
using AIProject.Definitions;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000B83 RID: 2947
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class AnimalGround : AnimalBase, INavMeshActor
	{
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005730 RID: 22320 RVA: 0x0025B1E9 File Offset: 0x002595E9
		public float WalkSpeed
		{
			[CompilerGenerated]
			get
			{
				return this.walkSpeed;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005731 RID: 22321 RVA: 0x0025B1F1 File Offset: 0x002595F1
		public float RunSpeed
		{
			[CompilerGenerated]
			get
			{
				return this.runSpeed;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06005732 RID: 22322 RVA: 0x0025B1F9 File Offset: 0x002595F9
		protected AnimalState StateIorL
		{
			[CompilerGenerated]
			get
			{
				return (UnityEngine.Random.value >= 0.5f) ? AnimalState.Locomotion : AnimalState.Idle;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005733 RID: 22323 RVA: 0x0025B211 File Offset: 0x00259611
		public NavMeshAgent Agent
		{
			[CompilerGenerated]
			get
			{
				return this.agent_;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x0025B219 File Offset: 0x00259619
		public NavMeshObstacle Obstacle
		{
			[CompilerGenerated]
			get
			{
				return this.obstacle_;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x0025B221 File Offset: 0x00259621
		public bool IsActiveAgent
		{
			[CompilerGenerated]
			get
			{
				return this.agent_ != null && this.agent_.isActiveAndEnabled && this.agent_.isOnNavMesh;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x0025B252 File Offset: 0x00259652
		// (set) Token: 0x06005737 RID: 22327 RVA: 0x0025B25A File Offset: 0x0025965A
		private protected float FirstStoppingDistance { protected get; private set; } = 1f;

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x0025B263 File Offset: 0x00259663
		public List<Waypoint> WaypointList { get; } = new List<Waypoint>();

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x0025B26B File Offset: 0x0025966B
		public int WaypointCount
		{
			[CompilerGenerated]
			get
			{
				return this.WaypointList.Count;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x0025B278 File Offset: 0x00259678
		// (set) Token: 0x0600573B RID: 22331 RVA: 0x0025B280 File Offset: 0x00259680
		public int LocomotionCount
		{
			get
			{
				return this.locomotionCount_;
			}
			set
			{
				this.locomotionCount_ = Mathf.Max(0, value);
				this.SetAgentAutoBraking();
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x0025B295 File Offset: 0x00259695
		public override int NavMeshAreaMask
		{
			[CompilerGenerated]
			get
			{
				return (!(this.Agent != null)) ? 0 : this.Agent.areaMask;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x0025B2B9 File Offset: 0x002596B9
		// (set) Token: 0x0600573E RID: 22334 RVA: 0x0025B2C1 File Offset: 0x002596C1
		public int Priority { get; protected set; } = 51;

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x0025B2CA File Offset: 0x002596CA
		public virtual float NextPointDistanceMin
		{
			[CompilerGenerated]
			get
			{
				return Mathf.Min(this.nextPointDistanceMin, this.nextPointDistanceMax);
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x0025B2DD File Offset: 0x002596DD
		public virtual float NextPointDistanceMax
		{
			[CompilerGenerated]
			get
			{
				return Mathf.Max(this.nextPointDistanceMin, this.nextPointDistanceMax);
			}
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x0025B2F0 File Offset: 0x002596F0
		protected override void Awake()
		{
			base.Awake();
			if (this.agent_ == null)
			{
				this.agent_ = base.GetComponent<NavMeshAgent>();
			}
			if (this.agent_ == null)
			{
				this.Destroy();
				return;
			}
			if (this.obstacle_ == null)
			{
				this.obstacle_ = base.GetComponent<NavMeshObstacle>();
			}
			this.FirstStoppingDistance = this.agent_.stoppingDistance;
			this.prevAgentEnabled = this.agent_.enabled;
			if (this.stateController == null)
			{
				this.stateController = new AnimalStateController();
			}
			if (this.agentPriorityAutoSetting)
			{
				int priority = 51;
				int num = 5;
				if (Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null)
				{
					AnimalDefinePack.NavMeshAgentInfoGroup agentInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AgentInfo;
					if (agentInfo != null)
					{
						priority = agentInfo.GroundAnimalStartPriority;
						num = agentInfo.PriorityMargin;
					}
				}
				this.Priority = priority;
				for (int i = 0; i < AnimalGround.priorityList.Count; i++)
				{
					if (this.Priority < AnimalGround.priorityList[i])
					{
						break;
					}
					this.Priority += num;
				}
				AnimalGround.priorityList.Add(this.Priority);
				AnimalGround.priorityList.Sort();
				this.Agent.avoidancePriority = this.Priority;
			}
			this.Agent.enabled = false;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06005742 RID: 22338 RVA: 0x0025B462 File Offset: 0x00259862
		// (set) Token: 0x06005743 RID: 22339 RVA: 0x0025B46A File Offset: 0x0025986A
		private protected bool Off { protected get; private set; }

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x0025B473 File Offset: 0x00259873
		// (set) Token: 0x06005745 RID: 22341 RVA: 0x0025B47B File Offset: 0x0025987B
		private protected bool prevAgentStopped { protected get; private set; }

		// Token: 0x06005746 RID: 22342 RVA: 0x0025B484 File Offset: 0x00259884
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.Off && this.prevSetAgentState)
			{
				this.ResetState();
				if (this.Agent != null && this.Agent.isActiveAndEnabled && this.Agent.isOnNavMesh && this.Agent.isStopped != this.prevAgentStopped)
				{
					this.Agent.isStopped = this.prevAgentStopped;
				}
			}
			this.Off = false;
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0025B514 File Offset: 0x00259914
		protected override void OnDisable()
		{
			if (this.Agent != null && this.Agent.isActiveAndEnabled && this.Agent.isOnNavMesh)
			{
				this.prevAgentStopped = this.Agent.isStopped;
				if (!this.prevAgentStopped)
				{
					this.Agent.isStopped = true;
				}
				this.prevSetAgentState = true;
			}
			else
			{
				this.prevSetAgentState = false;
			}
			base.OnDisable();
			this.Off = true;
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x0025B59C File Offset: 0x0025999C
		protected void StartCheckCoroutine()
		{
			IEnumerator _coroutine = this.CheckWaypointList();
			if (this.checkWaypointListDisposable != null)
			{
				this.checkWaypointListDisposable.Dispose();
			}
			this.checkWaypointListDisposable = (from _ in Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(this).TakeUntilDestroy(this.Agent)
			where this.isActiveAndEnabled
			select _).Subscribe<Unit>();
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x0025B619 File Offset: 0x00259A19
		protected void StopCheckCoroutine()
		{
			if (this.checkWaypointListDisposable != null)
			{
				this.checkWaypointListDisposable.Dispose();
				this.checkWaypointListDisposable = null;
			}
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x0025B638 File Offset: 0x00259A38
		protected override void OnDestroy()
		{
			this.Active = false;
			base.SetDestroyState();
			this.StopCheckCoroutine();
			this.RemoveActionPoint();
			this.ClearWaypoint();
			if (this.agentPriorityAutoSetting && AnimalGround.priorityList.Contains(this.Priority))
			{
				AnimalGround.priorityList.Remove(this.Priority);
			}
			base.OnDestroy();
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x0025B69C File Offset: 0x00259A9C
		public void SetWaypoints(int _chunkID)
		{
			if (Singleton<Manager.Map>.IsInstance())
			{
				Dictionary<int, Chunk> chunkTable = Singleton<Manager.Map>.Instance.ChunkTable;
				Chunk chunk = null;
				if (chunkTable.TryGetValue(_chunkID, out chunk))
				{
					if (this.MoveArea == null)
					{
						this.MoveArea = new LocomotionArea();
					}
					this.MoveArea.SetWaypoint(chunk.Waypoints);
					return;
				}
			}
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x0025B6F6 File Offset: 0x00259AF6
		public override void Clear()
		{
			this.StopCheckCoroutine();
			this.MoveArea = null;
			this.ClearWaypoint();
			this.stateController.Clear();
			base.Clear();
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x0025B71C File Offset: 0x00259B1C
		public override void ReleaseBody()
		{
			this.eyeController = null;
			this.neckController = null;
			base.ReleaseBody();
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x0025B732 File Offset: 0x00259B32
		public override void CreateBody()
		{
			base.CreateBody();
			this.eyeController = base.transform.GetComponentInChildren<EyeLookControllerVer2>(true);
			this.neckController = base.GetComponentInChildren<NeckLookControllerVer3>(true);
			this.BodyEnabled = false;
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x0025B760 File Offset: 0x00259B60
		protected override void ChangedStateEvent()
		{
			this.ResetAgent();
			this.destination = null;
			this.LocomotionCount = 0;
			this.ClearSchedule();
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x0025B78F File Offset: 0x00259B8F
		protected void ClearSchedule()
		{
			this.schedule = new AnimalSchedule(false, DateTime.MinValue, TimeSpan.Zero, false);
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x0025B7A8 File Offset: 0x00259BA8
		protected bool SetSchedule(AnimalPlayState.PlayStateInfo _stateInfo)
		{
			if (_stateInfo == null)
			{
				this.schedule = new AnimalSchedule(false, DateTime.MinValue, TimeSpan.Zero, false);
				return false;
			}
			int num = Mathf.Min(_stateInfo.LoopMin, _stateInfo.LoopMax);
			int num2 = Mathf.Max(_stateInfo.LoopMin, _stateInfo.LoopMax);
			int num3 = (num == num2) ? num : UnityEngine.Random.Range(num, num2 + 1);
			this.schedule = new AnimalSchedule(_stateInfo.IsLoop, Singleton<Manager.Map>.Instance.Simulator.Now, TimeSpan.FromMinutes((double)num3), true);
			return true;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x0025B838 File Offset: 0x00259C38
		protected bool SetSchedule(AnimalPlayState _playState)
		{
			return this.SetSchedule((_playState != null) ? _playState.MainStateInfo : null);
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x0025B84F File Offset: 0x00259C4F
		public void ActivateNavMeshAgent()
		{
			this.obstacle_.SetEnableSelf(false);
			this.agent_.SetEnableSelf(true);
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x0025B869 File Offset: 0x00259C69
		public void ActivateNavMeshObstacle()
		{
			this.agent_.SetEnableSelf(false);
			this.obstacle_.SetEnableSelf(true);
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x0025B883 File Offset: 0x00259C83
		public void DeactivateNavMeshElements()
		{
			this.agent_.SetEnableSelf(false);
			this.obstacle_.SetEnableSelf(false);
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x0025B89D File Offset: 0x00259C9D
		protected override void OnUpdate()
		{
			if (this.schedule.managing && base.StateUpdatePossible)
			{
				this.ScheduleUpdate();
			}
			base.OnUpdate();
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x0025B8C8 File Offset: 0x00259CC8
		public bool CheckBothTheIndoor(Vector3 _position)
		{
			MapArea.AreaType areaType;
			MapArea mapArea;
			return base.GetCurrentMapAreaType(base.CurrentMapArea, out areaType) && areaType == MapArea.AreaType.Indoor && AnimalBase.GetMapArea(_position, out mapArea, out areaType) && areaType == MapArea.AreaType.Indoor;
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x0025B908 File Offset: 0x00259D08
		public bool CheckBothTheIndoor(Actor _actor)
		{
			MapArea.AreaType areaType;
			return base.GetCurrentMapAreaType(base.CurrentMapArea, out areaType) && areaType == MapArea.AreaType.Indoor && AnimalBase.GetMapAreaType(_actor.Position, _actor.MapArea, out areaType) && areaType == MapArea.AreaType.Indoor;
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x0025B950 File Offset: 0x00259D50
		protected override void OnUpdateFirst()
		{
			int num = (0f >= this.Agent.velocity.sqrMagnitude) ? 0 : this.Priority;
			if (this.Agent.avoidancePriority != num)
			{
				this.Agent.avoidancePriority = num;
			}
			this.LocomotionOnClosePosition();
			this.LookTargetUpdate();
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x0025B9B0 File Offset: 0x00259DB0
		public override void OnMinuteUpdate(TimeSpan _deltaTime)
		{
			if (this.schedule.managing && this.schedule.enable)
			{
				this.schedule.elapsedTime = this.schedule.elapsedTime + _deltaTime;
				if (this.schedule.duration < this.schedule.elapsedTime)
				{
					this.schedule.enable = false;
				}
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x0025BA25 File Offset: 0x00259E25
		private Vector3 ToTarget
		{
			get
			{
				return (this.destination == null) ? Vector3.zero : (this.destination.Value - this.Position);
			}
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x0025BA58 File Offset: 0x00259E58
		protected void LocomotionOnClosePosition()
		{
			if (this.LocomotionCount == 0 || !this.Agent.enabled)
			{
				return;
			}
			if (1 < this.LocomotionCount)
			{
				if (base.HasDestination)
				{
					this.destination = null;
				}
				if (!this.Agent.updateRotation)
				{
					this.Agent.updateRotation = true;
				}
				return;
			}
			if ((!this.AgentHasPath && !base.HasDestination) || this.HasTwoCorners || this.Wait() || this.Agent.isStopped)
			{
				return;
			}
			if (!base.HasDestination && this.IsNearPointHasPath(this.disableUpdateDistance))
			{
				this.destination = new Vector3?(this.Agent.destination);
				this.Agent.updateRotation = false;
				this.toTargetVelocity = new Vector3(0f, 0f, this.Agent.velocity.magnitude);
				this.Agent.ResetPath();
			}
			else if (base.HasDestination && !this.IsNearPoint(this.disableUpdateDistance))
			{
				this.Agent.SetDestination(this.destination.Value);
				this.Agent.updateRotation = true;
				this.destination = null;
			}
			if (base.HasDestination)
			{
				Vector3 value = this.destination.Value;
				Vector2 vector = new Vector2(base.Forward.x, base.Forward.z);
				Vector2 normalized = vector.normalized;
				Vector2 vector2 = new Vector2(value.x - this.Position.x, value.z - this.Position.z);
				Vector2 normalized2 = vector2.normalized;
				float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
				float num = Mathf.Acos(f) * 57.29578f;
				if (0f < num)
				{
					Vector3 vector3 = Vector3.Cross(new Vector3(normalized.x, 0f, normalized.y), new Vector3(normalized2.x, 0f, normalized2.y));
					Vector3 eulerAngles = base.EulerAngles;
					float num2 = this.addAngle * Time.deltaTime * Mathf.Sign(vector3.y);
					if (num <= Mathf.Abs(num2))
					{
						eulerAngles.y += num * Mathf.Sign(vector3.y);
					}
					else
					{
						eulerAngles.y += num2;
					}
					eulerAngles.y = base.AngleAbs(eulerAngles.y);
					base.EulerAngles = eulerAngles;
				}
				float num3 = 45f;
				if (num < num3)
				{
					this.toTargetVelocity.z = this.toTargetVelocity.z + this.Agent.acceleration * Time.deltaTime;
					this.toTargetVelocity.z = Mathf.Min(this.toTargetVelocity.z, this.Agent.speed);
					Vector3 vector4 = base.Rotation * this.toTargetVelocity * Time.timeScale;
					vector4 *= 1f - num / num3;
					this.Agent.velocity = vector4;
				}
				else
				{
					this.Agent.velocity = Vector3.zero;
					this.toTargetVelocity = Vector3.zero;
				}
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x0600575D RID: 22365 RVA: 0x0025BDEA File Offset: 0x0025A1EA
		public bool AgentPathPending
		{
			[CompilerGenerated]
			get
			{
				return this.Agent.pathPending;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x0600575E RID: 22366 RVA: 0x0025BDF7 File Offset: 0x0025A1F7
		public bool AgentHasPath
		{
			[CompilerGenerated]
			get
			{
				return !this.Agent.pathPending && this.Agent.hasPath;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600575F RID: 22367 RVA: 0x0025BE17 File Offset: 0x0025A217
		public bool AgentActive
		{
			[CompilerGenerated]
			get
			{
				return this.Agent.isActiveAndEnabled && this.Agent.isOnNavMesh;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06005760 RID: 22368 RVA: 0x0025BE37 File Offset: 0x0025A237
		public bool HasTwoCorners
		{
			[CompilerGenerated]
			get
			{
				return this.Agent.hasPath && 2 < this.Agent.path.corners.Length;
			}
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x0025BE64 File Offset: 0x0025A264
		protected void ResetAgent()
		{
			if (base.CurrentState == AnimalState.Destroyed)
			{
				return;
			}
			bool flag = this.AgentMove();
			bool flag2 = this.AgentStop();
			if (flag)
			{
				this.ActivateNavMeshAgent();
			}
			else if (flag2)
			{
				this.ActivateNavMeshObstacle();
			}
			if (!this.Agent.isActiveAndEnabled || !this.Agent.isOnNavMesh)
			{
				return;
			}
			this.Agent.ResetPath();
			this.Agent.autoBraking = true;
			if (!flag)
			{
				this.Agent.velocity = Vector3.zero;
			}
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x0025BEF8 File Offset: 0x0025A2F8
		public virtual bool AgentMove()
		{
			return base.CurrentState == AnimalState.Locomotion || base.CurrentState == AnimalState.LovelyFollow || base.CurrentState == AnimalState.Escape || base.CurrentState == AnimalState.ToDepop || base.CurrentState == AnimalState.ToIndoor;
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x0025BF45 File Offset: 0x0025A345
		public virtual bool AgentStop()
		{
			return base.CurrentState == AnimalState.WithPlayer || base.CurrentState == AnimalState.WithAgent;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x0025BF61 File Offset: 0x0025A361
		protected void SetAgentSpeed(AnimalGround.LocomotionTypes _type)
		{
			if (_type != AnimalGround.LocomotionTypes.Walk)
			{
				if (_type == AnimalGround.LocomotionTypes.Run)
				{
					this.Agent.speed = this.runSpeed;
				}
			}
			else
			{
				this.Agent.speed = this.walkSpeed;
			}
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x0025BFA1 File Offset: 0x0025A3A1
		protected void SetAutoBraking()
		{
			if (this.Agent)
			{
				this.Agent.autoBraking = (this.LocomotionCount <= 1);
			}
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x0025BFCA File Offset: 0x0025A3CA
		public void SetAgentAutoBraking()
		{
			if (this.Agent)
			{
				this.Agent.autoBraking = (this.LocomotionCount <= 1);
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x0025BFF3 File Offset: 0x0025A3F3
		public Vector3 CurrentPosition(Vector3 _position)
		{
			if (this.CutDistanceFlag && Mathf.Abs(_position.y) <= this.CutDistance)
			{
				_position.y = 0f;
			}
			return _position;
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06005768 RID: 22376 RVA: 0x0025C024 File Offset: 0x0025A424
		public bool HasAgentPath
		{
			[CompilerGenerated]
			get
			{
				return this.Agent.isActiveAndEnabled && this.Agent.hasPath;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x0025C044 File Offset: 0x0025A444
		public bool HasNotAgentPath
		{
			[CompilerGenerated]
			get
			{
				return !this.Agent.isActiveAndEnabled || (!this.Agent.pathPending && !this.Agent.hasPath);
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x0025C07A File Offset: 0x0025A47A
		// (set) Token: 0x0600576B RID: 22379 RVA: 0x0025C082 File Offset: 0x0025A482
		public bool isNeutralCommand { get; protected set; } = true;

		// Token: 0x0600576C RID: 22380 RVA: 0x0025C08C File Offset: 0x0025A48C
		public override void ChangeState(AnimalState _nextState, System.Action _changeEvent = null)
		{
			this.isNeutralCommand = false;
			if (base.AutoChangeAnimation)
			{
				base.AutoChangeAnimation = false;
				base.PlayOutAnim(delegate()
				{
					this.AutoChangeAnimation = true;
					this.stateController.ChangeState(_nextState);
					this.isNeutralCommand = true;
					System.Action changeEvent2 = _changeEvent;
					if (changeEvent2 != null)
					{
						changeEvent2();
					}
				});
			}
			else
			{
				this.stateController.ChangeState(_nextState);
				this.isNeutralCommand = true;
				System.Action changeEvent = _changeEvent;
				if (changeEvent != null)
				{
					changeEvent();
				}
			}
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x0025C114 File Offset: 0x0025A514
		public override void SetState(AnimalState _nextState, System.Action _changeEvent = null)
		{
			this.isNeutralCommand = false;
			if (base.AutoChangeAnimation)
			{
				base.AutoChangeAnimation = false;
				base.PlayOutAnim(delegate()
				{
					this.AutoChangeAnimation = true;
					this.stateController.SetState(_nextState);
					this.isNeutralCommand = true;
					System.Action changeEvent2 = _changeEvent;
					if (changeEvent2 != null)
					{
						changeEvent2();
					}
				});
			}
			else
			{
				this.stateController.SetState(_nextState);
				this.isNeutralCommand = true;
				System.Action changeEvent = _changeEvent;
				if (changeEvent != null)
				{
					changeEvent();
				}
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x0600576E RID: 22382 RVA: 0x0025C198 File Offset: 0x0025A598
		public bool TargetHasPath
		{
			[CompilerGenerated]
			get
			{
				return base.Target != null && this.Agent.hasPath;
			}
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x0025C1B9 File Offset: 0x0025A5B9
		protected Vector3 CorrectPosition(Vector3 _position)
		{
			if (this.correctFlag && Mathf.Abs(_position.y) <= this.correctHeight)
			{
				_position.y = 0f;
			}
			return _position;
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x0025C1EA File Offset: 0x0025A5EA
		protected Vector3 CorrectPosition(Vector3 _position, bool _correctFlag)
		{
			if (_correctFlag && Mathf.Abs(_position.y) <= this.correctHeight)
			{
				_position.y = 0f;
			}
			return _position;
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x0025C218 File Offset: 0x0025A618
		protected bool IsNearPoint(Vector3 _targetPoint, float _distance)
		{
			return this.CorrectPosition(_targetPoint - this.Position).sqrMagnitude <= Mathf.Pow(_distance, 2f);
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x0025C24F File Offset: 0x0025A64F
		protected bool IsNearPointHasPath()
		{
			return this.AgentHasPath && this.Agent.remainingDistance <= this.Agent.stoppingDistance;
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x0025C27D File Offset: 0x0025A67D
		protected bool IsNearPointHasPath(float _stoppingDistance)
		{
			return this.AgentHasPath && this.Agent.remainingDistance <= _stoppingDistance;
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x0025C2A4 File Offset: 0x0025A6A4
		protected bool IsNearPointNotHasPath(Vector3 _point, float _stoppingDistance)
		{
			return this.HasNotAgentPath && this.CorrectPosition(_point - this.Position).sqrMagnitude <= Mathf.Pow(_stoppingDistance, 2f);
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x0025C2EC File Offset: 0x0025A6EC
		protected bool IsNearPoint(Vector3 _point)
		{
			return this.CorrectPosition(_point - this.Position).sqrMagnitude <= Mathf.Pow(this.Agent.stoppingDistance, 2f);
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x0025C330 File Offset: 0x0025A730
		public bool IsNearPoint()
		{
			if (base.HasActionPoint)
			{
				return this.IsNearPoint(this.actionPoint.Destination);
			}
			if (this.AgentHasPath)
			{
				return this.IsNearPointHasPath();
			}
			if (base.HasDestination)
			{
				Vector3? destination = this.destination;
				return this.IsNearPoint(destination.Value);
			}
			return this.currentWaypoint != null && this.IsNearPoint(this.currentWaypoint.transform.position);
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x0025C3B8 File Offset: 0x0025A7B8
		public bool IsNearPoint(float _stoppingDistance)
		{
			if (this.AgentHasPath)
			{
				return this.IsNearPointHasPath(_stoppingDistance);
			}
			if (base.HasDestination)
			{
				return this.IsNearPointNotHasPath(this.destination.Value, _stoppingDistance);
			}
			return this.currentWaypoint != null && this.IsNearPoint(this.currentWaypoint.transform.position, _stoppingDistance);
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x0025C420 File Offset: 0x0025A820
		public bool IsNearTargetPoint()
		{
			return this.TargetHasPath && this.CorrectPosition(base.Target.position - this.Position).sqrMagnitude <= Mathf.Pow(this.Agent.stoppingDistance, 2f);
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x0025C47C File Offset: 0x0025A87C
		protected void StateEndEvent()
		{
			if (this.ScheduleEndEvent != null)
			{
				System.Action scheduleEndEvent = this.ScheduleEndEvent;
				this.ScheduleEndEvent = null;
				scheduleEndEvent();
			}
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x0025C4A8 File Offset: 0x0025A8A8
		protected bool ScheduleUpdate()
		{
			if (this.schedule.managing)
			{
				if (!this.schedule.enable)
				{
					this.StateEndEvent();
					return true;
				}
			}
			else if (!base.AnimationKeepWaiting())
			{
				this.StateEndEvent();
				return true;
			}
			return false;
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x0025C4F6 File Offset: 0x0025A8F6
		protected bool UseWaypoint(Waypoint _waypoint)
		{
			return !(_waypoint == null) && _waypoint.Reserver != null;
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x0025C514 File Offset: 0x0025A914
		protected void ClearWaypoint()
		{
			this.ClearCurrentWaypoint();
			if (this.WaypointList.IsNullOrEmpty<Waypoint>())
			{
				return;
			}
			foreach (Waypoint waypoint in this.WaypointList)
			{
				if (!(waypoint == null))
				{
					if (waypoint.Reserver == this)
					{
						waypoint.Reserver = null;
					}
				}
			}
			this.WaypointList.Clear();
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x0025C5B0 File Offset: 0x0025A9B0
		protected void ClearCurrentWaypoint()
		{
			if (this.currentWaypoint != null)
			{
				if (this.currentWaypoint.Reserver == this)
				{
					this.currentWaypoint.Reserver = null;
				}
				this.currentWaypoint = null;
			}
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x0025C5E8 File Offset: 0x0025A9E8
		protected bool SetNextWaypoint(Waypoint _waypoint)
		{
			if (_waypoint == null || (_waypoint.Reserver != null && _waypoint.Reserver != this))
			{
				return false;
			}
			bool enabled = this.Agent.enabled;
			this.ActivateNavMeshAgent();
			if (this.calculatePath == null)
			{
				this.calculatePath = new NavMeshPath();
			}
			if (this.Agent.pathStatus == NavMeshPathStatus.PathInvalid || !this.Agent.CalculatePath(_waypoint.transform.position, this.calculatePath) || this.calculatePath.status != NavMeshPathStatus.PathComplete || !this.Agent.SetPath(this.calculatePath))
			{
				if (enabled)
				{
					this.ActivateNavMeshAgent();
				}
				else
				{
					this.ActivateNavMeshObstacle();
				}
				return false;
			}
			this.calculatePath = null;
			this.ClearCurrentWaypoint();
			this.destination = null;
			_waypoint.Reserver = this;
			this.currentWaypoint = _waypoint;
			this.Agent.isStopped = false;
			return true;
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x0025C6EC File Offset: 0x0025AAEC
		protected bool ChangeNextWaypoint()
		{
			if (this.Agent.isActiveAndEnabled)
			{
				this.Agent.ResetPath();
			}
			this.destination = null;
			if (!this.Agent.isActiveAndEnabled)
			{
				this.ClearCurrentWaypoint();
				return false;
			}
			if (this.currentWaypoint != null)
			{
				if (this.IsNearPoint(this.currentWaypoint.transform.position, this.NextPointDistanceMin))
				{
					this.ClearCurrentWaypoint();
				}
				else
				{
					if (this.Agent.SetDestination(this.currentWaypoint.transform.position))
					{
						this.SetAutoBraking();
						return true;
					}
					this.ClearCurrentWaypoint();
				}
			}
			bool flag = this.WaypointCount <= 0;
			this.Agent.isStopped = flag;
			if (flag)
			{
				return false;
			}
			while (0 < this.WaypointCount)
			{
				this.currentWaypoint = this.WaypointList.PopFront<Waypoint>();
				if (this.currentWaypoint.Reserver == this && this.Agent.SetDestination(this.currentWaypoint.transform.position))
				{
					this.SetAutoBraking();
					return true;
				}
				this.ClearCurrentWaypoint();
			}
			return false;
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x0025C830 File Offset: 0x0025AC30
		protected virtual List<Waypoint> GetWaypointList(Vector3 _startPosition, float _minDistance, float _maxDistance)
		{
			LocomotionArea.AreaType areaType = LocomotionArea.AreaType.Normal;
			BreedingTypes breedingType = base.BreedingType;
			if (breedingType != BreedingTypes.Wild)
			{
				if (breedingType == BreedingTypes.Pet)
				{
					areaType = ((!base.IndoorMode) ? (LocomotionArea.AreaType.Normal | LocomotionArea.AreaType.Indoor) : LocomotionArea.AreaType.Indoor);
				}
			}
			else
			{
				areaType = LocomotionArea.AreaType.Normal;
			}
			return (!(base.TargetMapArea == null)) ? this.MoveArea.GetPointList(_startPosition, _minDistance, _maxDistance, base.TargetMapArea, areaType) : this.MoveArea.GetPointList(_startPosition, _minDistance, _maxDistance, areaType);
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x0025C8B4 File Offset: 0x0025ACB4
		protected IEnumerator IAddWaypoint(float _minDistance, float _maxDistance)
		{
			yield return null;
			if (base.gameObject == null || this.Agent == null)
			{
				yield break;
			}
			Vector3 _startPosition = this.WaypointList.IsNullOrEmpty<Waypoint>() ? ((!base.HasDestination) ? ((!this.Agent.hasPath) ? ((!(this.currentWaypoint != null)) ? base.transform.position : this.currentWaypoint.transform.position) : this.Agent.destination) : this.destination.Value) : this.WaypointList[this.WaypointCount - 1].transform.position;
			this.dummyWaypointList.Clear();
			this.dummyWaypointList.AddRange(this.GetWaypointList(_startPosition, _minDistance, _maxDistance));
			while (!this.dummyWaypointList.IsNullOrEmpty<Waypoint>())
			{
				Waypoint _point = this.dummyWaypointList.GetRand<Waypoint>();
				if (!(_point == null) && !(_point.gameObject == null) && _point.gameObject.activeSelf)
				{
					if (!this.UseWaypoint(_point))
					{
						if (this.calculatePath == null)
						{
							this.calculatePath = new NavMeshPath();
						}
						if (NavMesh.CalculatePath(_startPosition, _point.transform.position, this.Agent.areaMask, this.calculatePath) && this.calculatePath.status == NavMeshPathStatus.PathComplete)
						{
							_point.Reserver = this;
							this.WaypointList.Add(_point);
							break;
						}
						yield return null;
					}
				}
			}
			yield break;
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x0025C8E0 File Offset: 0x0025ACE0
		protected IEnumerator CheckWaypointList()
		{
			while (base.CurrentState != AnimalState.Destroyed)
			{
				if (this.WaypointCount < 3)
				{
					IEnumerator _coroutine = this.IAddWaypoint(this.NextPointDistanceMin, this.NextPointDistanceMax);
					yield return Observable.FromCoroutine((CancellationToken _) => _coroutine, false).ToYieldInstruction<Unit>().AddTo(this);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x0025C8FB File Offset: 0x0025ACFB
		protected override void EnterWait()
		{
			base.RefreshCommands(true);
			base.PlayInAnim(AnimationCategoryID.Idle, 0, null);
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x0025C90D File Offset: 0x0025AD0D
		protected override void ExitWait()
		{
			base.RefreshCommands(true);
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x0025C916 File Offset: 0x0025AD16
		protected override void EnterIdle()
		{
			this.ActivateNavMeshObstacle();
			base.PlayInAnim(AnimationCategoryID.Idle, 0, null);
			this.SetSchedule(this.CurrentAnimState);
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x0025C934 File Offset: 0x0025AD34
		protected override void OnIdle()
		{
			if (this.schedule.managing)
			{
				if (!this.schedule.enable)
				{
					this.ClearSchedule();
					this.SetState(AnimalState.Locomotion, null);
					return;
				}
			}
			else if (!base.AnimationKeepWaiting())
			{
				this.SetState(AnimalState.Locomotion, null);
				return;
			}
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x0025C989 File Offset: 0x0025AD89
		protected override void EnterLocomotion()
		{
			this.ActivateNavMeshAgent();
			this.SetAgentSpeed(AnimalGround.LocomotionTypes.Walk);
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			this.ChangeNextWaypoint();
			this.LocomotionCount = 999;
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x0025C9B4 File Offset: 0x0025ADB4
		protected override void OnLocomotion()
		{
			if (this.Wait())
			{
				return;
			}
			if (this.AgentPathPending)
			{
				return;
			}
			if (this.IsNearPoint())
			{
				if (!base.HasActionPoint)
				{
					this.ChangeNextWaypoint();
				}
				else if (!base.SetNextState())
				{
					this.StateEndEvent();
					return;
				}
			}
			else if (!this.AgentHasPath)
			{
				if (base.HasActionPoint)
				{
					this.Agent.SetDestination(this.actionPoint.Destination);
				}
				else
				{
					this.ChangeNextWaypoint();
				}
			}
			if (this.Agent.isActiveAndEnabled && this.Agent.hasPath)
			{
				this.Agent.SetDestination(this.Agent.destination);
			}
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x0025CA82 File Offset: 0x0025AE82
		protected override void AnimationLocomotion()
		{
			this.WalkAnimationUpdate();
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x0025CA8A File Offset: 0x0025AE8A
		protected override void ExitLocomotion()
		{
			this.ClearCurrentWaypoint();
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x0025CA92 File Offset: 0x0025AE92
		protected override void EnterWithAgent()
		{
			base.EnterWithAgent();
			this.ActivateNavMeshObstacle();
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x0025CAA0 File Offset: 0x0025AEA0
		protected override void OnWithAgent()
		{
			AgentActor agentActor = base.CommandPartner as AgentActor;
			bool flag = agentActor == null;
			if (!flag)
			{
				flag = (agentActor.Mode != Desire.ActionType.EndTaskWildAnimal);
			}
			if (!flag)
			{
				flag = (agentActor.TargetInSightAnimal != this);
			}
			if (!flag)
			{
				flag = !agentActor.LivesWithAnimalSequence;
			}
			if (flag)
			{
				base.IsImpossible = false;
				base.CommandPartner = null;
				this.SetState(base.BackupState, null);
				return;
			}
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x0025CB1A File Offset: 0x0025AF1A
		protected override void ExitWithAgent()
		{
			base.ExitWithAgent();
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x0025CB24 File Offset: 0x0025AF24
		protected void WalkAnimationUpdate()
		{
			float value = (this.Agent.speed == 0f) ? 0f : (this.Agent.velocity.magnitude / this.Agent.speed);
			value = Mathf.Clamp(value, 0f, 0.5f);
			string paramName = AnimalBase.DefaultLocomotionParamName;
			if (Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null)
			{
				AnimalDefinePack.AnimatorInfoGroup animatorInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AnimatorInfo;
				if (animatorInfo != null)
				{
					paramName = animatorInfo.LocomotionParamName;
				}
			}
			base.SetFloat(paramName, value);
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x0025CBCC File Offset: 0x0025AFCC
		protected void RunAnimationUpdate()
		{
			float value = (this.Agent.speed == 0f) ? 0f : (this.Agent.velocity.magnitude / this.Agent.speed * 1.25f);
			value = Mathf.Clamp(value, 0f, 1f);
			string paramName = AnimalBase.DefaultLocomotionParamName;
			if (Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null)
			{
				AnimalDefinePack.AnimatorInfoGroup animatorInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AnimatorInfo;
				if (animatorInfo != null)
				{
					paramName = animatorInfo.LocomotionParamName;
				}
			}
			base.SetFloat(paramName, value);
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x0025CC7C File Offset: 0x0025B07C
		public override bool IsWithAgentFree(AgentActor _actor)
		{
			if (base.IsWithActor)
			{
				return false;
			}
			if (base.CommandPartner != null && base.CommandPartner is AgentActor && base.CommandPartner != _actor)
			{
				return false;
			}
			bool flag = base.CurrentState == AnimalState.Repop || base.CurrentState == AnimalState.Depop || base.CurrentState == AnimalState.ToDepop;
			return !flag;
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x0025CCF4 File Offset: 0x0025B0F4
		protected bool AvailablePoint(Waypoint _point)
		{
			return !(_point == null) && _point.gameObject.activeSelf && !(_point.OwnerArea == null) && (_point.Reserver == null || _point.Reserver == this);
		}

		// Token: 0x04005090 RID: 20624
		[SerializeField]
		private NavMeshAgent agent_;

		// Token: 0x04005091 RID: 20625
		[SerializeField]
		private NavMeshObstacle obstacle_;

		// Token: 0x04005092 RID: 20626
		[SerializeField]
		[DisableInPlayMode]
		[Tooltip("Agentの優先度を自動で設定するか")]
		private bool agentPriorityAutoSetting = true;

		// Token: 0x04005093 RID: 20627
		[SerializeField]
		private float walkSpeed = 5f;

		// Token: 0x04005094 RID: 20628
		[SerializeField]
		private float runSpeed = 10f;

		// Token: 0x04005095 RID: 20629
		[SerializeField]
		[Tooltip("次のポイントへの最短距離")]
		protected float nextPointDistanceMin = 75f;

		// Token: 0x04005096 RID: 20630
		[SerializeField]
		[Tooltip("次のポイントへの最長距離")]
		protected float nextPointDistanceMax = 1000f;

		// Token: 0x04005097 RID: 20631
		[SerializeField]
		[Tooltip("自動移動から手動移動に切り替える距離")]
		protected float disableUpdateDistance = 15f;

		// Token: 0x04005098 RID: 20632
		[SerializeField]
		protected float addAngle = 120f;

		// Token: 0x04005099 RID: 20633
		protected bool prevAgentEnabled = true;

		// Token: 0x0400509B RID: 20635
		protected LocomotionArea MoveArea;

		// Token: 0x0400509D RID: 20637
		protected Waypoint currentWaypoint;

		// Token: 0x0400509E RID: 20638
		private int locomotionCount_;

		// Token: 0x0400509F RID: 20639
		protected AnimalStateController stateController;

		// Token: 0x040050A0 RID: 20640
		protected static List<int> priorityList = new List<int>();

		// Token: 0x040050A4 RID: 20644
		protected bool prevSetAgentState;

		// Token: 0x040050A5 RID: 20645
		protected IDisposable checkWaypointListDisposable;

		// Token: 0x040050A6 RID: 20646
		private Vector3 toTargetVelocity = Vector3.zero;

		// Token: 0x040050A7 RID: 20647
		protected float CutDistance = 5f;

		// Token: 0x040050A8 RID: 20648
		protected bool CutDistanceFlag = true;

		// Token: 0x040050AA RID: 20650
		protected bool correctFlag = true;

		// Token: 0x040050AB RID: 20651
		protected float correctHeight = 5f;

		// Token: 0x040050AC RID: 20652
		protected System.Action ScheduleEndEvent;

		// Token: 0x040050AD RID: 20653
		protected NavMeshPath calculatePath;

		// Token: 0x040050AE RID: 20654
		private List<Waypoint> dummyWaypointList = new List<Waypoint>();

		// Token: 0x040050AF RID: 20655
		[NonSerialized]
		public float breakTime = -1f;

		// Token: 0x02000B84 RID: 2948
		protected enum LocomotionTypes
		{
			// Token: 0x040050B1 RID: 20657
			Walk,
			// Token: 0x040050B2 RID: 20658
			Run
		}
	}
}
