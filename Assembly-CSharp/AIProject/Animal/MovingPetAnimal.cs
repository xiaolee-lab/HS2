using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using AIProject.Animal.Resources;
using AIProject.Definitions;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BA7 RID: 2983
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(NavMeshObstacle))]
	public abstract class MovingPetAnimal : AnimalBase, IGroundPet, INicknameObject, INavMeshActor, IPetAnimal
	{
		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x002632A1 File Offset: 0x002616A1
		public NavMeshAgent Agent
		{
			[CompilerGenerated]
			get
			{
				return this._agent;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06005934 RID: 22836 RVA: 0x002632A9 File Offset: 0x002616A9
		public NavMeshObstacle Obstacle
		{
			[CompilerGenerated]
			get
			{
				return this._obstacle;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x002632B1 File Offset: 0x002616B1
		public float WalkSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._walkSpeed;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06005936 RID: 22838 RVA: 0x002632B9 File Offset: 0x002616B9
		public float RunSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._runSpeed;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06005937 RID: 22839 RVA: 0x002632C1 File Offset: 0x002616C1
		public int WaypointRetentionNum
		{
			[CompilerGenerated]
			get
			{
				return this._waypointRetentionNum;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06005938 RID: 22840 RVA: 0x002632C9 File Offset: 0x002616C9
		public float NormalStoppingDistance
		{
			[CompilerGenerated]
			get
			{
				return this._normalStoppingDistance;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06005939 RID: 22841 RVA: 0x002632D1 File Offset: 0x002616D1
		public float FollowTargetStoppingDistance
		{
			[CompilerGenerated]
			get
			{
				return this._followTargetStoppingDistance;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600593A RID: 22842 RVA: 0x002632D9 File Offset: 0x002616D9
		public float ReFollowTargetDistance
		{
			[CompilerGenerated]
			get
			{
				return this._reFollowTargetDistance;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x002632E1 File Offset: 0x002616E1
		public float RunningChangeDistance
		{
			[CompilerGenerated]
			get
			{
				return this._runningChangeDistance;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x002632E9 File Offset: 0x002616E9
		public Vector2 NarrowNextDistance
		{
			[CompilerGenerated]
			get
			{
				return this._narrowNextDistance;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600593D RID: 22845 RVA: 0x002632F1 File Offset: 0x002616F1
		public Vector2Int NarrowArrivalLimit
		{
			[CompilerGenerated]
			get
			{
				return this._narrowArrivalLimit;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x002632F9 File Offset: 0x002616F9
		public Vector2 LargeNextDistance
		{
			[CompilerGenerated]
			get
			{
				return this._largeNextDistance;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600593F RID: 22847 RVA: 0x00263301 File Offset: 0x00261701
		public Vector2Int LargeArrivalLimit
		{
			[CompilerGenerated]
			get
			{
				return this._largeArrivalLimit;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x00263309 File Offset: 0x00261709
		public float NicknameHeightOffset
		{
			[CompilerGenerated]
			get
			{
				return this._nicknameHeightOffset;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06005941 RID: 22849 RVA: 0x00263311 File Offset: 0x00261711
		// (set) Token: 0x06005942 RID: 22850 RVA: 0x00263319 File Offset: 0x00261719
		public AnimalData AnimalData { get; set; }

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06005943 RID: 22851 RVA: 0x00263322 File Offset: 0x00261722
		// (set) Token: 0x06005944 RID: 22852 RVA: 0x0026332A File Offset: 0x0026172A
		public bool IsChasePlayer { get; set; } = true;

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06005945 RID: 22853 RVA: 0x00263333 File Offset: 0x00261733
		// (set) Token: 0x06005946 RID: 22854 RVA: 0x00263357 File Offset: 0x00261757
		public string Nickname
		{
			get
			{
				AnimalData animalData = this.AnimalData;
				return ((animalData != null) ? animalData.Nickname : null) ?? base.Name;
			}
			set
			{
				if (this.AnimalData != null)
				{
					this.AnimalData.Nickname = value;
				}
				System.Action changeNickNameEvent = this.ChangeNickNameEvent;
				if (changeNickNameEvent != null)
				{
					changeNickNameEvent();
				}
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x00263384 File Offset: 0x00261784
		// (set) Token: 0x06005948 RID: 22856 RVA: 0x0026338C File Offset: 0x0026178C
		public bool ChaseActor
		{
			get
			{
				return this._chaseActor;
			}
			set
			{
				this.ChangeChaseActor(value);
			}
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00263395 File Offset: 0x00261795
		protected virtual void ChangeChaseActor(bool chaseFlag)
		{
			if (this._chaseActor == chaseFlag)
			{
				return;
			}
			this._chaseActor = chaseFlag;
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600594A RID: 22858 RVA: 0x002633B3 File Offset: 0x002617B3
		// (set) Token: 0x0600594B RID: 22859 RVA: 0x002633BB File Offset: 0x002617BB
		public PetHomePoint HomePoint { get; protected set; }

		// Token: 0x0600594C RID: 22860 RVA: 0x002633C4 File Offset: 0x002617C4
		public override bool IsWithAgentFree(AgentActor _actor)
		{
			if (!base.AgentInsight)
			{
				return false;
			}
			if (base.IsWithActor)
			{
				return false;
			}
			if (base.CommandPartner != null && base.CommandPartner is AgentActor && base.CommandPartner != _actor)
			{
				return false;
			}
			AnimalState currentState = base.CurrentState;
			switch (currentState)
			{
			case AnimalState.Idle:
			case AnimalState.Locomotion:
			case AnimalState.LovelyIdle:
			case AnimalState.LovelyFollow:
			case AnimalState.Sleep:
			case AnimalState.Eat:
				break;
			default:
				if (currentState != AnimalState.Action9)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x0026346E File Offset: 0x0026186E
		// (set) Token: 0x0600594E RID: 22862 RVA: 0x0026347B File Offset: 0x0026187B
		public bool AgentIsStopped
		{
			get
			{
				return this.Agent.isStopped;
			}
			set
			{
				if (!this.Agent.isOnNavMesh)
				{
					return;
				}
				this.Agent.isStopped = value;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600594F RID: 22863 RVA: 0x0026349A File Offset: 0x0026189A
		// (set) Token: 0x06005950 RID: 22864 RVA: 0x002634A2 File Offset: 0x002618A2
		public override Vector3 Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				if (this.Agent.isActiveAndEnabled)
				{
					this.Agent.Warp(value);
				}
				else
				{
					base.Position = value;
				}
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x002634CD File Offset: 0x002618CD
		// (set) Token: 0x06005952 RID: 22866 RVA: 0x002634D4 File Offset: 0x002618D4
		protected static List<int> PriorityList { get; set; } = new List<int>();

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x002634DC File Offset: 0x002618DC
		// (set) Token: 0x06005954 RID: 22868 RVA: 0x002634E4 File Offset: 0x002618E4
		public bool RunMode { get; protected set; }

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x002634ED File Offset: 0x002618ED
		public Transform NicknameRoot
		{
			[CompilerGenerated]
			get
			{
				return this._nicknameRoot;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005956 RID: 22870 RVA: 0x002634F5 File Offset: 0x002618F5
		// (set) Token: 0x06005957 RID: 22871 RVA: 0x0026350B File Offset: 0x0026190B
		public bool NicknameEnabled
		{
			get
			{
				return this._nicknameEnabled && this.BodyEnabled;
			}
			set
			{
				this._nicknameEnabled = value;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06005958 RID: 22872 RVA: 0x00263514 File Offset: 0x00261914
		// (set) Token: 0x06005959 RID: 22873 RVA: 0x0026351C File Offset: 0x0026191C
		public System.Action ChangeNickNameEvent { get; set; }

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600595A RID: 22874 RVA: 0x00263528 File Offset: 0x00261928
		public bool IsActiveNavMeshAgent
		{
			[CompilerGenerated]
			get
			{
				return this._agent != null && this._agent.isActiveAndEnabled && (this._agent.isOnNavMesh || this._agent.isOnOffMeshLink);
			}
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00263578 File Offset: 0x00261978
		public virtual void Initialize(PetHomePoint homePoint)
		{
			this.Clear();
			this.HomePoint = homePoint;
			if (homePoint == null)
			{
				return;
			}
			if (this.AnimalData == null)
			{
				return;
			}
			if (0 <= this.AnimalData.ModelID && Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalTable != null)
			{
				Dictionary<int, Dictionary<int, AnimalModelInfo>> modelInfoTable = Singleton<Manager.Resources>.Instance.AnimalTable.ModelInfoTable;
				Dictionary<int, AnimalModelInfo> dictionary;
				if (!modelInfoTable.IsNullOrEmpty<int, Dictionary<int, AnimalModelInfo>>() && modelInfoTable.TryGetValue(this.AnimalData.AnimalTypeID, out dictionary) && !dictionary.IsNullOrEmpty<int, AnimalModelInfo>())
				{
					AnimalModelInfo modelInfo;
					if (!dictionary.TryGetValue(this.AnimalData.ModelID, out modelInfo))
					{
						this.AnimalData.ModelID = dictionary.Keys.ToList<int>().Rand<int>();
						modelInfo = dictionary[this.AnimalData.ModelID];
					}
					base.SetModelInfo(modelInfo);
					base.LoadBody();
					if (this.bodyObject != null && this._nicknameRoot == null)
					{
						this._nicknameRoot = new GameObject("Nickname Root").transform;
						this._nicknameRoot.SetParent(this.bodyObject.transform, false);
						this._nicknameRoot.localPosition = new Vector3(0f, this._nicknameHeightOffset, 0f);
					}
				}
			}
			if (this._nicknameRoot == null)
			{
				this._nicknameRoot = new GameObject("Nickname Root").transform;
				this._nicknameRoot.SetParent(base.transform, false);
				this._nicknameRoot.localPosition = new Vector3(0f, this._nicknameHeightOffset, 0f);
			}
			base.SetStateData();
			this.InitializeCommandLabels();
			this._originPriority = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AgentInfo.GroundAnimalStartPriority;
			MovingPetAnimal.PriorityList.Sort();
			while (MovingPetAnimal.PriorityList.Contains(this._originPriority))
			{
				this._originPriority++;
				if (99 <= this._originPriority)
				{
					this._originPriority = 99;
					break;
				}
			}
			MovingPetAnimal.PriorityList.Add(this._originPriority);
			this.Agent.avoidancePriority = this._originPriority;
			this.Agent.stoppingDistance = 0.5f;
			this.Initialize();
			this.BodyEnabled = false;
			this.AnimalData.First = false;
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x002637F1 File Offset: 0x00261BF1
		public virtual void Initialize(AnimalData animalData)
		{
			if (animalData == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00263803 File Offset: 0x00261C03
		protected virtual void Initialize()
		{
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00263805 File Offset: 0x00261C05
		public virtual void ReturnHome()
		{
			this.SetState(AnimalState.Idle, null);
			if (this.HomePoint != null)
			{
				this.HomePoint.SetRootPoint(0, this);
			}
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x0026382D File Offset: 0x00261C2D
		public virtual void Release()
		{
			this.SetState(AnimalState.Destroyed, null);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x00263838 File Offset: 0x00261C38
		protected override void Awake()
		{
			base.Awake();
			if (this._agent == null)
			{
				this._agent = base.transform.GetOrAddComponent<NavMeshAgent>();
			}
			this._agent.enabled = false;
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x0026386E File Offset: 0x00261C6E
		protected override void Start()
		{
			base.Start();
			this._locomotionParamName = ((!Singleton<Manager.Resources>.IsInstance()) ? "Locomotion" : Singleton<Manager.Resources>.Instance.AnimalDefinePack.AnimatorInfo.LocomotionParamName);
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x002638A4 File Offset: 0x00261CA4
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.Agent != null && this.Agent.enabled != this._enableAgent)
			{
				this.Agent.enabled = this._enableAgent;
			}
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x002638E4 File Offset: 0x00261CE4
		protected override void OnDisable()
		{
			if (this.Agent != null)
			{
				this._enableAgent = this.Agent.enabled;
				if (this._enableAgent)
				{
					this.Agent.enabled = false;
				}
			}
			base.OnDisable();
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x00263930 File Offset: 0x00261D30
		protected override void OnDestroy()
		{
			this.Active = false;
			this.StopWaypointRetention();
			base.OnDestroy();
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x00263948 File Offset: 0x00261D48
		protected override void OnUpdate()
		{
			base.OnUpdate();
			if (base.IsLovely)
			{
				this._lovelyTimeCounter += Time.deltaTime;
			}
			else
			{
				this._lovelyTimeCounter = 0f;
			}
			if (this.AnimalData != null)
			{
				this.AnimalData.Position = this.Position;
				this.AnimalData.Rotation = base.Rotation;
			}
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x002639B8 File Offset: 0x00261DB8
		protected override void EnterStart()
		{
			AnimalState nextState;
			if (this.ChaseActor)
			{
				nextState = AnimalState.LovelyFollow;
				this.ActivateNavMeshAgent();
			}
			else
			{
				nextState = AnimalState.Action9;
				this.ActivateNavMeshObstacle();
			}
			this.SetState(nextState, null);
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x002639F1 File Offset: 0x00261DF1
		protected override void ExitStart()
		{
			this.BodyEnabled = true;
			this.Active = true;
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00263A01 File Offset: 0x00261E01
		protected override void EnterLovelyIdle()
		{
			this.ActivateNavMeshAgent();
			base.AnimationEndUpdate = false;
			base.PlayInAnim(AnimationCategoryID.Idle, 1, null);
			base.StateTimeLimit = 0.5f;
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x00263A24 File Offset: 0x00261E24
		protected override void OnLovelyIdle()
		{
			base.StateCounter += Time.deltaTime;
			if (base.StateCounter < base.StateTimeLimit)
			{
				return;
			}
			base.StateCounter = 0f;
			if (base.FollowActor != null)
			{
				if (this.CheckReachableActor(base.FollowActor))
				{
					float remainingDistance = this.GetRemainingDistance(base.FollowActor, this.CalcPath);
					if (this._reFollowTargetDistance < remainingDistance)
					{
						this.SetState(AnimalState.LovelyFollow, null);
						return;
					}
				}
				else
				{
					base.FollowActor = null;
				}
			}
			if (base.FollowActor == null)
			{
				base.FollowActor = this.GetChaseActor();
			}
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x00263AD4 File Offset: 0x00261ED4
		protected override void ExitLovelyIdle()
		{
			base.AnimationEndUpdate = true;
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00263AE0 File Offset: 0x00261EE0
		protected override void EnterLovelyFollow()
		{
			base.AnimationEndUpdate = false;
			if (base.FollowActor == null)
			{
				base.FollowActor = this.GetChaseActor();
			}
			this.ActivateNavMeshAgent();
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			this.Agent.avoidancePriority = this._originPriority;
			this.RunMode = false;
			this.Agent.speed = this.WalkSpeed;
			this.AgentIsStopped = false;
			base.StateTimeLimit = 0.5f;
			this._animParam = 0f;
			this._animLerpValue = Singleton<Manager.Resources>.Instance.AnimalDefinePack.GroundAnimalInfo.ForwardAnimationLerpValue;
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00263B80 File Offset: 0x00261F80
		protected override void OnLovelyFollow()
		{
			if (base.FollowActor == null || !this.IsActiveNavMeshAgent)
			{
				this.SetState(AnimalState.LovelyIdle, null);
				return;
			}
			if (!this.Agent.pathPending)
			{
				this.Agent.SetDestination(base.FollowActor.Position);
			}
			float num;
			if (!this.TryGetRemainingDistance(base.FollowActor, out num))
			{
				base.FollowActor = null;
				this.SetState(AnimalState.LovelyIdle, null);
				return;
			}
			if (num <= this._followTargetStoppingDistance)
			{
				this.SetState(AnimalState.LovelyIdle, null);
				return;
			}
			this.RunMode = (this._runningChangeDistance < num);
			bool flag = base.FollowActor is PlayerActor;
			if (this.RunMode)
			{
				this.Agent.speed = ((!flag) ? this._otherFollowRunSpeed : this._playerFollowRunSpeed);
			}
			else
			{
				this.Agent.speed = ((!flag) ? this._otherFollowWalkSpeed : this._playerFollowWalkSpeed);
			}
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00263C88 File Offset: 0x00262088
		protected override void ExitLovelyFollow()
		{
			this.ReleaseAgentPath();
			base.SetFloat(this._locomotionParamName, 0f);
			base.AnimationEndUpdate = true;
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x00263CA8 File Offset: 0x002620A8
		protected override void AnimationLovelyFollow()
		{
			float num = (!Mathf.Approximately(this.Agent.velocity.magnitude, 0f)) ? ((!this.RunMode) ? 0.5f : 1f) : 0f;
			if (!Mathf.Approximately(num, this._animParam))
			{
				float f = num - this._animParam;
				float num2 = Mathf.Sign(f);
				float num3 = this._animParam + this._animLerpValue * Time.deltaTime * num2;
				if (0f < num2)
				{
					if (num < num3)
					{
						num3 = num;
					}
				}
				else if (num3 < num)
				{
					num3 = num;
				}
				this._animParam = num3;
			}
			base.SetFloat(this._locomotionParamName, this._animParam);
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x00263D74 File Offset: 0x00262174
		protected override void EnterAction9()
		{
			base.AnimationEndUpdate = false;
			base.PlayInAnim(AnimationCategoryID.Etc, 999, null);
			this.ActivateNavMeshObstacle();
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x00263D90 File Offset: 0x00262190
		protected override void OnAction9()
		{
			Transform transform = (!(this.HomePoint != null)) ? null : this.HomePoint.GetRootPoint(0);
			if (transform != null)
			{
				this.Position = transform.position;
				base.Rotation = transform.rotation;
			}
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x00263DE5 File Offset: 0x002621E5
		protected override void ExitAction9()
		{
			base.AnimationEndUpdate = true;
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x00263DEE File Offset: 0x002621EE
		protected override void EnterWithAgent()
		{
			base.EnterWithAgent();
			this.ActivateNavMeshObstacle();
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00263DFC File Offset: 0x002621FC
		protected override void OnWithAgent()
		{
			AgentActor agentActor = base.CommandPartner as AgentActor;
			bool flag = agentActor == null;
			if (!flag)
			{
				flag = (agentActor.Mode != Desire.ActionType.EndTaskPetAnimal);
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

		// Token: 0x06005974 RID: 22900 RVA: 0x00263E76 File Offset: 0x00262276
		protected override void ExitWithAgent()
		{
			base.ExitWithAgent();
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x00263E80 File Offset: 0x00262280
		public Actor GetMostFavorabilityActor()
		{
			ReadOnlyDictionary<int, Actor> readOnlyDictionary = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.ActorTable;
			if (readOnlyDictionary.IsNullOrEmpty<int, Actor>())
			{
				return null;
			}
			List<KeyValuePair<int, float>> list = ListPool<KeyValuePair<int, float>>.Get();
			this.AnimalData.GetFavorabilityKeyPairs(ref list);
			if (list.IsNullOrEmpty<KeyValuePair<int, float>>())
			{
				ListPool<KeyValuePair<int, float>>.Release(list);
				return null;
			}
			Actor result = null;
			list.Sort((KeyValuePair<int, float> x1, KeyValuePair<int, float> x2) => (int)(x2.Value - x1.Value));
			foreach (KeyValuePair<int, float> keyValuePair in list)
			{
				Actor actor;
				if (readOnlyDictionary.TryGetValue(keyValuePair.Key, out actor) && actor != null)
				{
					result = actor;
					break;
				}
			}
			ListPool<KeyValuePair<int, float>>.Release(list);
			return result;
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x00263F74 File Offset: 0x00262374
		public Actor GetChaseActor()
		{
			Actor actor = this.GetMostFavorabilityActor();
			if (actor == null)
			{
				actor = ((!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player);
			}
			if (actor != null)
			{
				bool flag = NavMesh.CalculatePath(this.Position, actor.Position, this.Agent.areaMask, this.CalcPath);
				if (flag)
				{
					flag = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
				}
				if (flag)
				{
					return actor;
				}
			}
			ReadOnlyDictionary<int, Actor> readOnlyDictionary = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.ActorTable;
			actor = null;
			if (!readOnlyDictionary.IsNullOrEmpty<int, Actor>())
			{
				List<Actor> list = ListPool<Actor>.Get();
				list.AddRange(readOnlyDictionary.Values);
				while (!list.IsNullOrEmpty<Actor>())
				{
					Actor rand = list.GetRand<Actor>();
					if (!(rand == null) && !(rand is MerchantActor))
					{
						bool flag2 = NavMesh.CalculatePath(this.Position, rand.Position, this.Agent.areaMask, this.CalcPath);
						if (flag2)
						{
							flag2 = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
						}
						if (flag2)
						{
							actor = rand;
							break;
						}
					}
				}
				ListPool<Actor>.Release(list);
			}
			return actor;
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x002640C0 File Offset: 0x002624C0
		public bool CheckReachableActor(Actor actor)
		{
			if (actor == null)
			{
				return false;
			}
			bool flag = NavMesh.CalculatePath(this.Position, actor.Position, this.Agent.areaMask, this.CalcPath);
			if (flag)
			{
				flag = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
			}
			return flag;
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x00264114 File Offset: 0x00262514
		public float GetRemainingDistance(Actor actor)
		{
			float num = float.MaxValue;
			if (actor == null)
			{
				return num;
			}
			bool flag = NavMesh.CalculatePath(this.Position, actor.Position, this.Agent.areaMask, this.CalcPath);
			if (flag)
			{
				flag = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
			}
			if (flag)
			{
				num = 0f;
				for (int i = 0; i < this.CalcPath.corners.Length - 1; i++)
				{
					num += Vector3.Distance(this.CalcPath.corners[i], this.CalcPath.corners[i + 1]);
				}
			}
			return num;
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x002641D0 File Offset: 0x002625D0
		public bool TryGetRemainingDistance(NavMeshAgent agent, Actor actor, out float remainingDistance)
		{
			remainingDistance = float.MaxValue;
			if (agent == null || !agent.isActiveAndEnabled || !agent.isOnNavMesh)
			{
				return false;
			}
			if (agent.hasPath)
			{
				Vector3[] corners = agent.path.corners;
				remainingDistance = 0f;
				for (int i = 0; i < corners.Length - 1; i++)
				{
					remainingDistance += Vector3.Distance(corners[i], corners[i + 1]);
				}
				return true;
			}
			return agent.pathPending;
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x00264268 File Offset: 0x00262668
		public bool TryGetRemainingDistance(Actor actor, out float distance)
		{
			distance = float.MaxValue;
			if (actor == null)
			{
				return false;
			}
			bool flag = NavMesh.CalculatePath(this.Position, actor.Position, this.Agent.areaMask, this.CalcPath);
			if (flag)
			{
				flag = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
			}
			if (flag)
			{
				distance = 0f;
				for (int i = 0; i < this.CalcPath.corners.Length - 1; i++)
				{
					distance += Vector3.Distance(this.CalcPath.corners[i], this.CalcPath.corners[i + 1]);
				}
			}
			return flag;
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x00264328 File Offset: 0x00262728
		public float GetRemainingDistance(Actor actor, NavMeshPath path)
		{
			float num = float.MaxValue;
			if (actor == null || path == null)
			{
				return num;
			}
			if (path.status == NavMeshPathStatus.PathComplete)
			{
				num = 0f;
				for (int i = 0; i < this.CalcPath.corners.Length - 1; i++)
				{
					num += Vector3.Distance(this.CalcPath.corners[i], this.CalcPath.corners[i + 1]);
				}
			}
			return num;
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x002643B8 File Offset: 0x002627B8
		protected virtual void StartWaypointRetention()
		{
			IEnumerator coroutine = this.WaypointRetentionCoroutine();
			if (this._waypointRetentionDisposable != null)
			{
				this._waypointRetentionDisposable.Dispose();
			}
			this._waypointRetentionDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x00264414 File Offset: 0x00262814
		protected virtual Vector3 LastPosition()
		{
			if (this._movePointList.IsNullOrEmpty<Waypoint>())
			{
				return (!(this._destination != null)) ? this.Position : this._destination.transform.position;
			}
			Waypoint waypoint = this._movePointList.Last<Waypoint>();
			if (waypoint != null)
			{
				return waypoint.transform.position;
			}
			return (!(this._destination != null)) ? this.Position : this._destination.transform.position;
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x002644B0 File Offset: 0x002628B0
		protected virtual IEnumerator WaypointRetentionCoroutine()
		{
			for (;;)
			{
				if (this._movePointList.Count < this._waypointRetentionNum)
				{
					IEnumerator coroutine = (!this.ChaseActor) ? this.CalcWaypointRetentionCoroutine(this._restrictedPointList, this._narrowNextDistance) : this.CalcWaypointRetentionCoroutine(this._largeAreaPointList, this._largeNextDistance);
					yield return Observable.FromCoroutine((CancellationToken _) => coroutine, false).ToYieldInstruction<Unit>().AddTo(this);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600597F RID: 22911 RVA: 0x002644CC File Offset: 0x002628CC
		public NavMeshPath CalcPath
		{
			[CompilerGenerated]
			get
			{
				NavMeshPath result;
				if ((result = this._calcPath) == null)
				{
					result = (this._calcPath = new NavMeshPath());
				}
				return result;
			}
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x002644F4 File Offset: 0x002628F4
		protected virtual IEnumerator CalcWaypointRetentionCoroutine(IReadOnlyList<Waypoint> pointList, Vector2 limitDistance)
		{
			if (pointList.IsNullOrEmpty<Waypoint>())
			{
				yield break;
			}
			List<Waypoint> availableList = ListPool<Waypoint>.Get();
			foreach (Waypoint waypoint in pointList)
			{
				if (waypoint != null && waypoint.Available(this) && waypoint.Reserver == null)
				{
					availableList.Add(waypoint);
				}
			}
			if (!availableList.IsNullOrEmpty<Waypoint>())
			{
				while (!availableList.IsNullOrEmpty<Waypoint>())
				{
					Waypoint point = availableList.GetRand<Waypoint>();
					if (!(point == null))
					{
						bool condition = point.Available(this);
						if (condition)
						{
							condition = (point.Reserver == null);
						}
						if (condition)
						{
							condition = NavMesh.CalculatePath(this.Position, point.Position, this.Agent.areaMask, this.CalcPath);
						}
						if (condition)
						{
							condition = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
						}
						if (condition)
						{
							condition = NavMesh.CalculatePath(this.LastPosition(), point.Position, this.Agent.areaMask, this.CalcPath);
						}
						if (condition)
						{
							condition = (this.CalcPath.status == NavMeshPathStatus.PathComplete);
						}
						if (condition)
						{
							float num = 0f;
							Vector3[] corners = this.CalcPath.corners;
							for (int i = 0; i < corners.Length - 1; i++)
							{
								num += Vector3.Distance(corners[i], corners[i + 1]);
							}
							condition = limitDistance.Range(num);
						}
						if (condition)
						{
							point.Reserver = this;
							this._movePointList.Add(point);
							if (this._movePointList.Count >= this._waypointRetentionNum)
							{
								break;
							}
							yield return null;
						}
						else
						{
							yield return null;
						}
					}
				}
			}
			ListPool<Waypoint>.Release(availableList);
			yield break;
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x00264520 File Offset: 0x00262920
		protected bool SetNextMovePoint()
		{
			this.ReleaseDestination();
			Waypoint waypoint = null;
			while (!this._movePointList.IsNullOrEmpty<Waypoint>())
			{
				if ((waypoint = this._movePointList.PopFront<Waypoint>()) != null)
				{
					break;
				}
			}
			if (waypoint == null)
			{
				return false;
			}
			this._destination = waypoint;
			this.Agent.SetDestination(waypoint.Position);
			return true;
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x00264590 File Offset: 0x00262990
		protected bool ClosedDestination()
		{
			if (this._destination == null)
			{
				return false;
			}
			float num = Vector3.Distance(this._destination.Position, this.Position);
			return num <= this.Agent.stoppingDistance;
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x002645D8 File Offset: 0x002629D8
		protected virtual void StopWaypointRetention()
		{
			if (this._waypointRetentionDisposable != null)
			{
				this._waypointRetentionDisposable.Dispose();
				this._waypointRetentionDisposable = null;
			}
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x002645F7 File Offset: 0x002629F7
		protected virtual void ActivateNavMeshAgent()
		{
			this._obstacle.enabled = false;
			this._agent.enabled = false;
			this._agent.enabled = true;
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x0026461D File Offset: 0x00262A1D
		protected virtual void ActivateNavMeshObstacle()
		{
			this._agent.enabled = false;
			this._obstacle.enabled = false;
			this._obstacle.enabled = true;
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x00264643 File Offset: 0x00262A43
		protected virtual void ReleaseAgentPath()
		{
			if (!this.Agent.isActiveAndEnabled || !this.Agent.isOnNavMesh)
			{
				return;
			}
			this.Agent.ResetPath();
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x00264671 File Offset: 0x00262A71
		protected virtual void ReleaseDestination()
		{
			if (this._destination == null)
			{
				return;
			}
			if (this._destination.Reserver == this)
			{
				this._destination.Reserver = null;
			}
			this._destination = null;
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x002646AC File Offset: 0x00262AAC
		protected virtual void ReleaseMovePointList()
		{
			if (this._movePointList.IsNullOrEmpty<Waypoint>())
			{
				return;
			}
			foreach (Waypoint waypoint in this._movePointList)
			{
				if (!(waypoint == null))
				{
					if (waypoint.Reserver == this)
					{
						waypoint.Reserver = null;
					}
				}
			}
			this._movePointList.Clear();
		}

		// Token: 0x0400519F RID: 20895
		[SerializeField]
		private NavMeshAgent _agent;

		// Token: 0x040051A0 RID: 20896
		[SerializeField]
		private NavMeshObstacle _obstacle;

		// Token: 0x040051A1 RID: 20897
		[SerializeField]
		private float _walkSpeed = 5f;

		// Token: 0x040051A2 RID: 20898
		[SerializeField]
		private float _runSpeed = 20f;

		// Token: 0x040051A3 RID: 20899
		[SerializeField]
		private int _waypointRetentionNum = 5;

		// Token: 0x040051A4 RID: 20900
		[SerializeField]
		private float _normalStoppingDistance = 1f;

		// Token: 0x040051A5 RID: 20901
		[SerializeField]
		private float _followTargetStoppingDistance = 3f;

		// Token: 0x040051A6 RID: 20902
		[SerializeField]
		private float _reFollowTargetDistance = 10f;

		// Token: 0x040051A7 RID: 20903
		[SerializeField]
		private float _runningChangeDistance = 20f;

		// Token: 0x040051A8 RID: 20904
		[SerializeField]
		private Vector2 _narrowNextDistance = Vector2.zero;

		// Token: 0x040051A9 RID: 20905
		[SerializeField]
		private Vector2Int _narrowArrivalLimit = Vector2Int.zero;

		// Token: 0x040051AA RID: 20906
		[SerializeField]
		private Vector2 _largeNextDistance = Vector2.zero;

		// Token: 0x040051AB RID: 20907
		[SerializeField]
		private Vector2Int _largeArrivalLimit = Vector2Int.zero;

		// Token: 0x040051AC RID: 20908
		[SerializeField]
		private float _nicknameHeightOffset = 1f;

		// Token: 0x040051AD RID: 20909
		[SerializeField]
		private float _playerFollowWalkSpeed = 1f;

		// Token: 0x040051AE RID: 20910
		[SerializeField]
		private float _otherFollowWalkSpeed = 1f;

		// Token: 0x040051AF RID: 20911
		[SerializeField]
		private float _playerFollowRunSpeed;

		// Token: 0x040051B0 RID: 20912
		[SerializeField]
		private float _otherFollowRunSpeed;

		// Token: 0x040051B3 RID: 20915
		private bool _chaseActor;

		// Token: 0x040051B5 RID: 20917
		protected IReadOnlyList<Waypoint> _restrictedPointList;

		// Token: 0x040051B6 RID: 20918
		protected IReadOnlyList<Waypoint> _largeAreaPointList;

		// Token: 0x040051B7 RID: 20919
		protected List<Waypoint> _movePointList = new List<Waypoint>();

		// Token: 0x040051B8 RID: 20920
		protected Waypoint _destination;

		// Token: 0x040051B9 RID: 20921
		protected string _locomotionParamName = string.Empty;

		// Token: 0x040051BA RID: 20922
		protected int _arrivalCount;

		// Token: 0x040051BB RID: 20923
		protected int _arrivalLimit;

		// Token: 0x040051BD RID: 20925
		protected int _originPriority = 50;

		// Token: 0x040051BE RID: 20926
		protected float _lovelyTimeCounter;

		// Token: 0x040051C0 RID: 20928
		protected Transform _nicknameRoot;

		// Token: 0x040051C1 RID: 20929
		private bool _nicknameEnabled;

		// Token: 0x040051C3 RID: 20931
		private bool _enableAgent;

		// Token: 0x040051C4 RID: 20932
		protected float _animLerpValue = 1f;

		// Token: 0x040051C5 RID: 20933
		protected float _animParam;

		// Token: 0x040051C6 RID: 20934
		private IDisposable _waypointRetentionDisposable;

		// Token: 0x040051C7 RID: 20935
		private NavMeshPath _calcPath;
	}
}
