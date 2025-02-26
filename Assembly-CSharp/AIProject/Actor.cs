using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AIChara;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.Scene;
using ConfigScene;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C39 RID: 3129
	[DisallowMultipleComponent]
	public abstract class Actor : SerializedMonoBehaviour, IVisible
	{
		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x060060E1 RID: 24801 RVA: 0x0028B12B File Offset: 0x0028952B
		// (set) Token: 0x060060E2 RID: 24802 RVA: 0x0028B133 File Offset: 0x00289533
		public EventType EventKey { get; set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x060060E3 RID: 24803 RVA: 0x0028B13C File Offset: 0x0028953C
		// (set) Token: 0x060060E4 RID: 24804 RVA: 0x0028B144 File Offset: 0x00289544
		public ChaControl ChaControl { get; set; }

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x0028B14D File Offset: 0x0028954D
		// (set) Token: 0x060060E6 RID: 24806 RVA: 0x0028B155 File Offset: 0x00289555
		public bool IsVisible { get; set; } = true;

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x060060E7 RID: 24807 RVA: 0x0028B15E File Offset: 0x0028955E
		// (set) Token: 0x060060E8 RID: 24808 RVA: 0x0028B166 File Offset: 0x00289566
		public int ActionID { get; set; }

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x060060E9 RID: 24809 RVA: 0x0028B16F File Offset: 0x0028956F
		// (set) Token: 0x060060EA RID: 24810 RVA: 0x0028B177 File Offset: 0x00289577
		public int PoseID { get; set; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x060060EB RID: 24811
		public abstract string CharaName { get; }

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x060060EC RID: 24812 RVA: 0x0028B180 File Offset: 0x00289580
		// (set) Token: 0x060060ED RID: 24813 RVA: 0x0028B188 File Offset: 0x00289588
		public ItemCache EquipedItem { get; protected set; }

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x060060EE RID: 24814 RVA: 0x0028B191 File Offset: 0x00289591
		// (set) Token: 0x060060EF RID: 24815 RVA: 0x0028B19E File Offset: 0x0028959E
		public bool IsKinematic
		{
			get
			{
				return this._rigidbody.isKinematic;
			}
			set
			{
				this._rigidbody.isKinematic = value;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x060060F0 RID: 24816 RVA: 0x0028B1AC File Offset: 0x002895AC
		public Rigidbody Rigidbody
		{
			[CompilerGenerated]
			get
			{
				return this._rigidbody;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x060060F1 RID: 24817 RVA: 0x0028B1B4 File Offset: 0x002895B4
		public NavMeshAgent NavMeshAgent
		{
			[CompilerGenerated]
			get
			{
				return this._navMeshAgent;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x060060F2 RID: 24818 RVA: 0x0028B1BC File Offset: 0x002895BC
		public NavMeshObstacle NavMeshObstacle
		{
			[CompilerGenerated]
			get
			{
				return this._navMeshObstacle;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x060060F3 RID: 24819 RVA: 0x0028B1C4 File Offset: 0x002895C4
		public float AccelerationTime
		{
			[CompilerGenerated]
			get
			{
				return this._accelerationTime;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x060060F4 RID: 24820 RVA: 0x0028B1CC File Offset: 0x002895CC
		public float MaxVerticalVelocityOnGround
		{
			[CompilerGenerated]
			get
			{
				return this._maxVerticalVelocityOnGround;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x060060F5 RID: 24821 RVA: 0x0028B1D4 File Offset: 0x002895D4
		// (set) Token: 0x060060F6 RID: 24822 RVA: 0x0028B1DC File Offset: 0x002895DC
		public Actor.InputInfo StateInfo { get; set; }

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x060060F7 RID: 24823 RVA: 0x0028B1E8 File Offset: 0x002895E8
		public ReadOnlyDictionary<Actor.FovBodyPart, Transform> FovTargetPointTable
		{
			get
			{
				ReadOnlyDictionary<Actor.FovBodyPart, Transform> result;
				if ((result = this._fovBodyPartReadOnlyTable) == null)
				{
					result = (this._fovBodyPartReadOnlyTable = new ReadOnlyDictionary<Actor.FovBodyPart, Transform>(this._fovTargetPointTable));
				}
				return result;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x0028B218 File Offset: 0x00289618
		public Transform[] FovTargetPoints
		{
			[CompilerGenerated]
			get
			{
				Transform[] result;
				if ((result = this._fovTargets) == null)
				{
					result = (this._fovTargets = this._fovTargetPointTable.Values.ToArray<Transform>());
				}
				return result;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x060060F9 RID: 24825 RVA: 0x0028B24C File Offset: 0x0028964C
		public ReadOnlyDictionary<Actor.BodyPart, Transform> ChaBodyParts
		{
			get
			{
				ReadOnlyDictionary<Actor.BodyPart, Transform> result;
				if ((result = this._chaBodyPartsReadonlyDic) == null)
				{
					result = (this._chaBodyPartsReadonlyDic = new ReadOnlyDictionary<Actor.BodyPart, Transform>(this._chaBodyParts));
				}
				return result;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x060060FA RID: 24826 RVA: 0x0028B27A File Offset: 0x0028967A
		public Transform[] ChaFovTargets
		{
			get
			{
				if (this._chaFovTargets.IsNullOrEmpty<Transform>())
				{
					this._chaFovTargets = this._chaBodyParts.Values.ToArray<Transform>();
				}
				return this._chaFovTargets;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x060060FB RID: 24827 RVA: 0x0028B2A8 File Offset: 0x002896A8
		public LocomotionProfile LocomotionProfile
		{
			get
			{
				if (this._locomotionProfile == null)
				{
					this._locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				}
				return this._locomotionProfile;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x060060FC RID: 24828 RVA: 0x0028B2D1 File Offset: 0x002896D1
		// (set) Token: 0x060060FD RID: 24829 RVA: 0x0028B2D9 File Offset: 0x002896D9
		public bool Visible { get; set; } = true;

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x0028B2E2 File Offset: 0x002896E2
		// (set) Token: 0x060060FF RID: 24831 RVA: 0x0028B2EA File Offset: 0x002896EA
		public Vector3 Normal { get; set; }

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06006100 RID: 24832 RVA: 0x0028B2F3 File Offset: 0x002896F3
		// (set) Token: 0x06006101 RID: 24833 RVA: 0x0028B2FB File Offset: 0x002896FB
		public float ForwardMLP { get; set; }

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06006102 RID: 24834
		public abstract ActorController Controller { get; }

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06006103 RID: 24835
		public abstract ICharacterInfo TiedInfo { get; }

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06006104 RID: 24836
		public abstract ActorAnimation Animation { get; }

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06006105 RID: 24837 RVA: 0x0028B304 File Offset: 0x00289704
		// (set) Token: 0x06006106 RID: 24838 RVA: 0x0028B30C File Offset: 0x0028970C
		public int MapID { get; set; }

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06006107 RID: 24839 RVA: 0x0028B315 File Offset: 0x00289715
		// (set) Token: 0x06006108 RID: 24840 RVA: 0x0028B31D File Offset: 0x0028971D
		public int ChunkID { get; set; }

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06006109 RID: 24841 RVA: 0x0028B326 File Offset: 0x00289726
		// (set) Token: 0x0600610A RID: 24842 RVA: 0x0028B32E File Offset: 0x0028972E
		public int AreaID { get; set; }

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x0600610B RID: 24843 RVA: 0x0028B337 File Offset: 0x00289737
		// (set) Token: 0x0600610C RID: 24844 RVA: 0x0028B33F File Offset: 0x0028973F
		public MapArea MapArea
		{
			get
			{
				return this._mapArea;
			}
			set
			{
				this._mapArea = value;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x0028B348 File Offset: 0x00289748
		// (set) Token: 0x0600610E RID: 24846 RVA: 0x0028B350 File Offset: 0x00289750
		public MapArea.AreaType AreaType { get; set; }

		// Token: 0x0600610F RID: 24847 RVA: 0x0028B35C File Offset: 0x0028975C
		public IObservable<int> OnMapAreaChangedAsObservable()
		{
			IntReactiveProperty result;
			if ((result = this._mapAreaID) == null)
			{
				result = (this._mapAreaID = new IntReactiveProperty(-1));
			}
			return result;
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06006110 RID: 24848 RVA: 0x0028B385 File Offset: 0x00289785
		public virtual bool IsNeutralCommand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x0028B388 File Offset: 0x00289788
		// (set) Token: 0x06006112 RID: 24850 RVA: 0x0028B390 File Offset: 0x00289790
		public virtual Desire.ActionType Mode { get; set; }

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x0028B399 File Offset: 0x00289799
		// (set) Token: 0x06006114 RID: 24852 RVA: 0x0028B3A1 File Offset: 0x002897A1
		public bool IsInit { get; private set; }

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06006115 RID: 24853
		public abstract ActorLocomotion Locomotor { get; }

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06006116 RID: 24854 RVA: 0x0028B3AA File Offset: 0x002897AA
		// (set) Token: 0x06006117 RID: 24855 RVA: 0x0028B3B2 File Offset: 0x002897B2
		public Actor Partner { get; set; }

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06006118 RID: 24856 RVA: 0x0028B3BB File Offset: 0x002897BB
		// (set) Token: 0x06006119 RID: 24857 RVA: 0x0028B3E4 File Offset: 0x002897E4
		public Vector3 Position
		{
			get
			{
				if (this.Locomotor != null)
				{
					return this.Locomotor.transform.position;
				}
				return Vector3.zero;
			}
			set
			{
				this.Locomotor.transform.position = value;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x0600611A RID: 24858 RVA: 0x0028B3F7 File Offset: 0x002897F7
		public Vector3 CommandCenter
		{
			get
			{
				return this.Locomotor.transform.position;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x0600611B RID: 24859 RVA: 0x0028B409 File Offset: 0x00289809
		public Vector3 Forward
		{
			[CompilerGenerated]
			get
			{
				return this.Locomotor.transform.forward;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x0600611C RID: 24860 RVA: 0x0028B41B File Offset: 0x0028981B
		public Vector3 Back
		{
			[CompilerGenerated]
			get
			{
				return this.Locomotor.transform.rotation * Vector3.back;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x0600611D RID: 24861 RVA: 0x0028B437 File Offset: 0x00289837
		// (set) Token: 0x0600611E RID: 24862 RVA: 0x0028B449 File Offset: 0x00289849
		public Quaternion Rotation
		{
			get
			{
				return this.Locomotor.transform.rotation;
			}
			set
			{
				this.Locomotor.transform.rotation = value;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x0600611F RID: 24863 RVA: 0x0028B45C File Offset: 0x0028985C
		// (set) Token: 0x06006120 RID: 24864 RVA: 0x0028B464 File Offset: 0x00289864
		public PoseType PoseType { get; set; }

		// Token: 0x06006121 RID: 24865 RVA: 0x0028B470 File Offset: 0x00289870
		public void Relocate()
		{
			Vector3 origin = this.Position + Vector3.up * 10f;
			RaycastHit[] array = Physics.RaycastAll(origin, Vector3.down, 100f, Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer);
			RaycastHit raycastHit = default(RaycastHit);
			foreach (RaycastHit raycastHit2 in array)
			{
				if (raycastHit2.collider.gameObject != this.Controller.gameObject)
				{
					raycastHit = raycastHit2;
					break;
				}
			}
			this.Position = raycastHit.point;
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x0028B52E File Offset: 0x0028992E
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x0028B536 File Offset: 0x00289936
		public bool IsOnGround { get; set; }

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x0028B53F File Offset: 0x0028993F
		// (set) Token: 0x06006125 RID: 24869 RVA: 0x0028B547 File Offset: 0x00289947
		public ActionPoint CurrentPoint { get; set; }

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x0028B550 File Offset: 0x00289950
		// (set) Token: 0x06006127 RID: 24871 RVA: 0x0028B558 File Offset: 0x00289958
		public ActionPoint NextPoint { get; set; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06006128 RID: 24872 RVA: 0x0028B561 File Offset: 0x00289961
		// (set) Token: 0x06006129 RID: 24873 RVA: 0x0028B569 File Offset: 0x00289969
		public EquipState EquipState { get; private set; } = new EquipState();

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x0028B574 File Offset: 0x00289974
		public ObservableStateMachineTrigger ObservableStateMachine
		{
			[CompilerGenerated]
			get
			{
				ObservableStateMachineTrigger result;
				if ((result = this._observableStateMachine) == null)
				{
					result = (this._observableStateMachine = this.Animation.Animator.GetBehaviour<ObservableStateMachineTrigger>());
				}
				return result;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x0028B5A7 File Offset: 0x002899A7
		public int InstanceID
		{
			get
			{
				if (this._instanceID == null)
				{
					this._instanceID = new int?(base.GetInstanceID());
				}
				return this._instanceID.Value;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x0028B5D5 File Offset: 0x002899D5
		// (set) Token: 0x0600612D RID: 24877 RVA: 0x0028B5DD File Offset: 0x002899DD
		public bool IsSlave { get; set; }

		// Token: 0x0600612E RID: 24878 RVA: 0x0028B5E8 File Offset: 0x002899E8
		public void SetCurrentSchedule(bool scheduled, string name_, int minMin, int maxMin, bool gestures, bool useGameTime)
		{
			this._scheduleThreshold = new UnityEx.ValueTuple<int, int>(minMin, maxMin);
			int num = (minMin == maxMin) ? minMin : UnityEngine.Random.Range(minMin, maxMin + 1);
			this._schedule = new Actor.BehaviorSchedule(scheduled, name_, (float)num);
			this._schedule.useGameTime = useGameTime;
			TimeSpan i = TimeSpan.FromMinutes((double)(num / 2));
			this._gestureTimeLimit = new UnityEx.ValueTuple<bool, TimeSpan>(gestures, i);
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x0028B650 File Offset: 0x00289A50
		// (set) Token: 0x06006130 RID: 24880 RVA: 0x0028B658 File Offset: 0x00289A58
		public Actor.BehaviorSchedule Schedule
		{
			get
			{
				return this._schedule;
			}
			set
			{
				this._schedule = value;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x0028B661 File Offset: 0x00289A61
		// (set) Token: 0x06006132 RID: 24882 RVA: 0x0028B66E File Offset: 0x00289A6E
		public bool EnabledSchedule
		{
			get
			{
				return this._schedule.enabled;
			}
			set
			{
				this._schedule.enabled = value;
			}
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x0028B67C File Offset: 0x00289A7C
		public virtual void EnableEntity()
		{
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0028B67E File Offset: 0x00289A7E
		public virtual void DisableEntity()
		{
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x0028B680 File Offset: 0x00289A80
		public void ActivateNavMeshAgent()
		{
			if (this._navMeshObstacle.enabled)
			{
				this._navMeshObstacle.enabled = false;
			}
			if (!this._navMeshAgent.enabled)
			{
				this._navMeshAgent.enabled = true;
			}
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x0028B6BA File Offset: 0x00289ABA
		public void DeactivateNavMeshAgent()
		{
			if (this._navMeshAgent.enabled)
			{
				this._navMeshAgent.enabled = false;
			}
			if (!this._navMeshObstacle.enabled)
			{
				this._navMeshObstacle.enabled = true;
			}
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x0028B6F4 File Offset: 0x00289AF4
		public void StopNavMeshAgent()
		{
			if (!this.NavMeshAgent.isActiveAndEnabled)
			{
				return;
			}
			if (!this.NavMeshAgent.isOnNavMesh)
			{
				return;
			}
			this.NavMeshAgent.velocity = Vector3.zero;
			if (!this.NavMeshAgent.isStopped)
			{
				this.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x0028B74F File Offset: 0x00289B4F
		public void ChangeStaticNavMeshAgentAvoidance()
		{
			if (Singleton<Manager.Resources>.IsInstance())
			{
				this._navMeshAgent.avoidancePriority = Singleton<Manager.Resources>.Instance.AgentProfile.AvoidancePriorityStationary;
			}
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x0028B775 File Offset: 0x00289B75
		public void ChangeDynamicNavMeshAgentAvoidance()
		{
			if (Singleton<Manager.Resources>.IsInstance())
			{
				this._navMeshAgent.avoidancePriority = Singleton<Manager.Resources>.Instance.AgentProfile.AvoidancePriorityDefault;
			}
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x0028B79B File Offset: 0x00289B9B
		public void RecoverNavMeshAgentAvoidance()
		{
			if (this._originAvoidancePriority < 0)
			{
				return;
			}
			this._navMeshAgent.avoidancePriority = this._originAvoidancePriority;
			this._originAvoidancePriority = -1;
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x0028B7C2 File Offset: 0x00289BC2
		public void StartTalkSequence(Actor listener)
		{
			this.StartTalkSequence(listener, 0, 0);
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0028B7D0 File Offset: 0x00289BD0
		public void StartTalkSequence(Actor listener, int speakID, int listenID)
		{
			if (listener is AgentActor)
			{
				IEnumerator enumerator = this._talkEnumerator = this.StartTalkSequenceCoroutine(listener, speakID, listenID);
				if (this._talkDisposable != null)
				{
					this._talkDisposable.Dispose();
				}
				this._talkDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
				AgentActor agentActor = listener as AgentActor;
				agentActor.CommandPartner = this;
				agentActor.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			}
			else if (listener is MerchantActor)
			{
				IEnumerator enumerator = this._talkEnumerator = this.StartTalkSequenceCoroutine(listener, speakID, listenID);
				if (this._talkDisposable != null)
				{
					this._talkDisposable.Dispose();
				}
				this._talkDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
				MerchantActor merchantActor = listener as MerchantActor;
				merchantActor.CommandPartner = this;
				merchantActor.ChangeBehavior(Merchant.ActionType.TalkWithAgent);
			}
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x0028B8D0 File Offset: 0x00289CD0
		private IEnumerator StartTalkSequenceCoroutine(Actor listener, int speakID, int listenID)
		{
			PlayState.AnimStateInfo idleState = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
			this.Animation.PlayTurnAnimation(listener.Position, 1f, idleState, false);
			listener.Animation.PlayTurnAnimation(this.Position, 1f, idleState, false);
			while (this.Animation.PlayingTurnAnimation || listener.Animation.PlayingTurnAnimation)
			{
				yield return null;
			}
			Manager.Resources.AnimationTables animData = Singleton<Manager.Resources>.Instance.Animation;
			PoseKeyPair speakerPose = animData.TalkSpeakerStateTable[speakID];
			PlayState speakerInfo = animData.AgentActionAnimTable[speakerPose.postureID][speakerPose.poseID];
			bool sInEnableFade = speakerInfo.MainStateInfo.InStateInfo.EnableFade;
			float sInFadeSecond = speakerInfo.MainStateInfo.InStateInfo.FadeSecond;
			int sLayer = speakerInfo.Layer;
			this.Animation.LoadEventKeyTable(speakerPose.postureID, speakerPose.poseID);
			PlayState listenerInfo = null;
			if (listener is AgentActor)
			{
				PoseKeyPair poseKeyPair = animData.TalkListenerStateTable[listenID];
				listenerInfo = animData.AgentActionAnimTable[poseKeyPair.postureID][poseKeyPair.poseID];
				listener.Animation.LoadEventKeyTable(poseKeyPair.postureID, poseKeyPair.poseID);
			}
			else
			{
				PoseKeyPair poseKeyPair2 = animData.MerchantListenerStateTable[listenID];
				listenerInfo = animData.MerchantCommonActionAnimStateTable[poseKeyPair2.postureID][poseKeyPair2.poseID];
				listener.Animation.LoadEventKeyTable(poseKeyPair2.postureID, poseKeyPair2.poseID);
			}
			bool lInEnableFade = listenerInfo.MainStateInfo.InStateInfo.EnableFade;
			float lInFadeSecond = listenerInfo.MainStateInfo.InStateInfo.FadeSecond;
			int lLayer = listenerInfo.Layer;
			this.Animation.InitializeStates(speakerInfo.MainStateInfo.InStateInfo.StateInfos, speakerInfo.MainStateInfo.OutStateInfo.StateInfos, speakerInfo.MainStateInfo.AssetBundleInfo);
			listener.Animation.InitializeStates(listenerInfo.MainStateInfo.InStateInfo.StateInfos, listenerInfo.MainStateInfo.OutStateInfo.StateInfos, listenerInfo.MainStateInfo.AssetBundleInfo);
			this.Animation.PlayInAnimation(sInEnableFade, sInFadeSecond, 1f, speakerInfo.Layer);
			listener.Animation.PlayInAnimation(lInEnableFade, lInFadeSecond, 1f, listenerInfo.Layer);
			while (this.Animation.PlayingInAnimation || listener.Animation.PlayingTurnAnimation || listener.Animation.PlayingInAnimation)
			{
				yield return null;
			}
			this.SetCurrentSchedule(true, "会話", speakerInfo.MainStateInfo.LoopMin, speakerInfo.MainStateInfo.LoopMax, speakerInfo.ActionInfo.hasAction, false);
			while (this._schedule.enabled)
			{
				yield return null;
			}
			bool sOutEnableFade = speakerInfo.MainStateInfo.OutStateInfo.EnableFade;
			float sOutFadeSecond = speakerInfo.MainStateInfo.OutStateInfo.FadeSecond;
			this.Animation.PlayOutAnimation(sOutEnableFade, sOutFadeSecond, sLayer);
			bool lOutEnableFade = listenerInfo.MainStateInfo.OutStateInfo.EnableFade;
			float lOutFadeSecond = listenerInfo.MainStateInfo.OutStateInfo.FadeSecond;
			listener.Animation.PlayOutAnimation(lOutEnableFade, lOutFadeSecond, lLayer);
			while (this.Animation.PlayingOutAnimation || listener.Animation.PlayingOutAnimation)
			{
				yield return null;
			}
			this._talkEnumerator = null;
			yield break;
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x0600613E RID: 24894 RVA: 0x0028B900 File Offset: 0x00289D00
		public bool LivesTalkSequence
		{
			get
			{
				return this._talkEnumerator != null;
			}
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0028B90E File Offset: 0x00289D0E
		public void StopTalkSequence()
		{
			if (this._talkDisposable != null)
			{
				this._talkDisposable.Dispose();
				this._talkDisposable = null;
			}
			if (this._talkEnumerator != null)
			{
				this._talkEnumerator = null;
			}
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x0028B93F File Offset: 0x00289D3F
		public void StartWithAnimalSequence(AnimalBase animal)
		{
			this.StartWithAnimalSequence(animal, 0, 0);
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x0028B94A File Offset: 0x00289D4A
		public void StartWithAnimalSequence(AnimalBase animal, int poseID)
		{
			this.StartWithAnimalSequence(animal, poseID, poseID);
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x0028B958 File Offset: 0x00289D58
		public void StartWithAnimalSequence(AnimalBase animal, int actorPoseID, int animalPoseID)
		{
			if (animal == null)
			{
				return;
			}
			if (!(this is AgentActor) && !(this is PlayerActor))
			{
				return;
			}
			this._withAnimalEnumerator = this.StartWithAnimalSequenceCoroutine(animal, actorPoseID, animalPoseID);
			animal.SetImpossible(true, this);
			animal.SetState((!(this is AgentActor)) ? AnimalState.WithPlayer : AnimalState.WithAgent, null);
			if (this._withAnimalDisposable != null)
			{
				this._withAnimalDisposable.Dispose();
			}
			this._withAnimalDisposable = Observable.FromCoroutine(() => this._withAnimalEnumerator, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				this._withAnimalEnumerator = null;
			}, delegate()
			{
				this._withAnimalEnumerator = null;
			}).AddTo(this);
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0028BA2C File Offset: 0x00289E2C
		private IEnumerator StartWithAnimalSequenceCoroutine(AnimalBase animal, int actorPoseID, int animalPoseID)
		{
			yield return null;
			if (!this.IsMatchAnimalWithActor(animal))
			{
				this._withAnimalEnumerator = null;
				yield break;
			}
			Renderer bodyRenderer = this.ChaControl.objBody.GetComponentInChildren<Renderer>();
			Renderer headRenderer = this.ChaControl.objHead.GetComponentInChildren<Renderer>();
			bool isVisible = bodyRenderer != null && bodyRenderer.isVisible;
			isVisible |= (headRenderer != null && headRenderer.isVisible);
			if (isVisible && Singleton<Manager.Map>.IsInstance() && Singleton<Manager.Resources>.IsInstance())
			{
				LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				ActorCameraControl cameraControl = Singleton<Manager.Map>.Instance.Player.CameraControl;
				float num = Vector3.Distance(this.Position, cameraControl.transform.position);
				float crossFadeEnableDistance = locomotionProfile.CrossFadeEnableDistance;
				if (num <= crossFadeEnableDistance)
				{
					cameraControl.CrossFade.FadeStart(-1f);
				}
			}
			Vector3 eulerAngle = Quaternion.LookRotation(animal.Position - this.Position).eulerAngles;
			eulerAngle.x = (eulerAngle.z = 0f);
			this.Rotation = Quaternion.Euler(eulerAngle);
			animal.SetWithActorPoint(this, animalPoseID);
			PlayState playState = null;
			PlayState.PlayStateInfo mainStateInfo = null;
			int layer = 0;
			int animalTypeID = animal.AnimalTypeID;
			if (Singleton<Manager.Resources>.Instance.Animation.WithAnimalStateTable.ContainsKey(animalTypeID) && Singleton<Manager.Resources>.Instance.Animation.WithAnimalStateTable[animalTypeID].TryGetValue(actorPoseID, out playState))
			{
				layer = playState.Layer;
				mainStateInfo = playState.MainStateInfo;
				bool enableFade = mainStateInfo.InStateInfo.EnableFade;
				float fadeSecond = mainStateInfo.InStateInfo.FadeSecond;
				this.Animation.InitializeStates(mainStateInfo.InStateInfo.StateInfos, mainStateInfo.OutStateInfo.StateInfos, mainStateInfo.AssetBundleInfo);
				this.Animation.LoadAnimalEventKeyTable(animal.AnimalTypeID, actorPoseID);
				this.LoadEventItems(playState);
				this.Animation.PlayInAnimation(enableFade, fadeSecond, 1f, layer);
			}
			animal.StartInAnimationWithActor(animalPoseID);
			while (this.Animation.PlayingInAnimation || (animal != null && animal.PlayingInAnimation))
			{
				yield return null;
				if (!this.IsMatchAnimalWithActor(animal))
				{
					this.ClearItems();
					this._withAnimalEnumerator = null;
					yield break;
				}
			}
			if (!this.IsMatchAnimalWithActor(animal))
			{
				this.ClearItems();
				this._withAnimalEnumerator = null;
				yield break;
			}
			if (mainStateInfo != null && mainStateInfo.IsLoop)
			{
				this.SetCurrentSchedule(true, "動物行動", mainStateInfo.LoopMin, mainStateInfo.LoopMax, playState.ActionInfo.hasAction, false);
				while (this._schedule.enabled)
				{
					yield return null;
					if (!this.IsMatchAnimalWithActor(animal))
					{
						this.ClearItems();
						this._withAnimalEnumerator = null;
						yield break;
					}
				}
			}
			if (mainStateInfo != null)
			{
				bool enableFade2 = mainStateInfo.OutStateInfo.EnableFade;
				float fadeSecond2 = mainStateInfo.OutStateInfo.FadeSecond;
				this.Animation.PlayOutAnimation(enableFade2, fadeSecond2, layer);
			}
			if (this.IsMatchAnimalWithActor(animal))
			{
				animal.StartOutAnimationWithActor();
			}
			else
			{
				animal = null;
			}
			bool outAnimal = animal == null;
			while (this.Animation.PlayingOutAnimation || (animal != null && animal.PlayingOutAnimation))
			{
				if (!outAnimal)
				{
					if (this.IsMatchAnimalWithActor(animal))
					{
						if (!animal.PlayingOutAnimation)
						{
							animal.SetImpossible(false, this);
							animal.SetState(animal.BackupState, null);
							animal = null;
							outAnimal = true;
						}
					}
					else
					{
						animal = null;
						outAnimal = true;
					}
				}
				yield return null;
			}
			if (this.IsMatchAnimalWithActor(animal))
			{
				animal.SetImpossible(false, this);
				animal.SetState(animal.BackupState, null);
			}
			this.ClearItems();
			this._withAnimalEnumerator = null;
			yield break;
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x0028BA5C File Offset: 0x00289E5C
		public void StopWithAnimalSequence()
		{
			if (this._withAnimalDisposable != null)
			{
				this._withAnimalDisposable.Dispose();
				this._withAnimalDisposable = null;
			}
			this._withAnimalEnumerator = null;
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x0028BA84 File Offset: 0x00289E84
		public bool IsMatchAnimalWithActor(AnimalBase animal)
		{
			if (animal == null || !animal.Active)
			{
				return false;
			}
			switch (animal.CurrentState)
			{
			case AnimalState.WithPlayer:
				return this is PlayerActor;
			case AnimalState.WithAgent:
				return this is AgentActor;
			case AnimalState.WithMerchant:
				return this is MerchantActor;
			default:
				return false;
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06006146 RID: 24902 RVA: 0x0028BAEA File Offset: 0x00289EEA
		public bool LivesWithAnimalSequence
		{
			[CompilerGenerated]
			get
			{
				return this._withAnimalEnumerator != null;
			}
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0028BAF8 File Offset: 0x00289EF8
		private void Start()
		{
			this._rigidbody.sleepThreshold = -1f;
			this.OnStart();
			Observable.EveryUpdate().TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				if (this._rigidbody != null && this._rigidbody.IsSleeping())
				{
					this._rigidbody.WakeUp();
				}
				GraphicSystem graphicData = Config.GraphicData;
				if (this.Controller == null || graphicData == null)
				{
					return;
				}
				this.Controller.FaceLightActive = graphicData.FaceLight;
			});
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0028BB30 File Offset: 0x00289F30
		public virtual IEnumerator LoadAsync()
		{
			yield break;
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0028BB44 File Offset: 0x00289F44
		public virtual void LoadEquipments()
		{
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0028BB48 File Offset: 0x00289F48
		protected void LoadEquipmentItem(StuffItem item, ChaControlDefine.ExtraAccessoryParts parts)
		{
			int id = (item.CategoryID == -1) ? -1 : item.ID;
			this.ChaControl.ChangeExtraAccessory(parts, id);
			this.ChaControl.ShowExtraAccessory(parts, true);
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0028BB88 File Offset: 0x00289F88
		protected virtual void InitializeIK()
		{
			this.IsInit = true;
		}

		// Token: 0x0600614C RID: 24908
		protected abstract IEnumerator LoadCharAsync(string fileName);

		// Token: 0x0600614D RID: 24909
		protected abstract void OnStart();

		// Token: 0x0600614E RID: 24910 RVA: 0x0028BB91 File Offset: 0x00289F91
		public virtual void OnDayUpdated(TimeSpan timeSpan)
		{
		}

		// Token: 0x0600614F RID: 24911
		public abstract void OnMinuteUpdated(TimeSpan timeSpan);

		// Token: 0x06006150 RID: 24912 RVA: 0x0028BB93 File Offset: 0x00289F93
		public virtual void OnSecondUpdated(TimeSpan timeSpan)
		{
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x0028BB95 File Offset: 0x00289F95
		private void InitializeRagdoll()
		{
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x0028BB98 File Offset: 0x00289F98
		protected float GetSlopeDamper(Vector3 velocity, Vector3 groundNormal)
		{
			float num = 90f - Vector3.Angle(velocity, groundNormal);
			num -= this._slopeStartAngle;
			float num2 = this._slopeEndAngle - this._slopeStartAngle;
			return 1f - Mathf.Clamp(num / num2, 0f, 1f);
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x0028BBE2 File Offset: 0x00289FE2
		public virtual bool CanUnlock(int category, int id)
		{
			return false;
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x0028BBE5 File Offset: 0x00289FE5
		public virtual bool CanAddItem(StuffItem item)
		{
			return true;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x0028BBE8 File Offset: 0x00289FE8
		public virtual bool CanAddItem(StuffItemInfo item)
		{
			return true;
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x0028BBEC File Offset: 0x00289FEC
		public int Lottery(Dictionary<int, ItemTableElement> table)
		{
			float num = 0f;
			foreach (KeyValuePair<int, ItemTableElement> keyValuePair in table)
			{
				num += keyValuePair.Value.Rate;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			int result = -1;
			foreach (KeyValuePair<int, ItemTableElement> keyValuePair2 in table)
			{
				float rate = keyValuePair2.Value.Rate;
				if (num2 <= rate)
				{
					result = keyValuePair2.Key;
					break;
				}
				num2 -= rate;
			}
			return result;
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x0028BCCC File Offset: 0x0028A0CC
		public ItemTableElement.GatherElement Lottery(List<ItemTableElement.GatherElement> list)
		{
			float num = 0f;
			foreach (ItemTableElement.GatherElement gatherElement in list)
			{
				num += gatherElement.prob;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			int index = -1;
			int num3 = 0;
			foreach (ItemTableElement.GatherElement gatherElement2 in list)
			{
				float prob = gatherElement2.prob;
				if (num2 <= prob)
				{
					index = num3;
					break;
				}
				num2 -= prob;
				num3++;
			}
			return list[index];
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0028BDB0 File Offset: 0x0028A1B0
		public virtual Actor.SearchInfo RandomAddItem(Dictionary<int, ItemTableElement> itemTable, bool containsNone)
		{
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			if (itemTable == null)
			{
				return new Actor.SearchInfo
				{
					IsSuccess = false
				};
			}
			Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;
			int key = this.Lottery(itemTable);
			ItemTableElement itemTableElement;
			if (itemTable.TryGetValue(key, out itemTableElement))
			{
				Actor.SearchInfo searchInfo = new Actor.SearchInfo
				{
					IsSuccess = true
				};
				foreach (ItemTableElement.GatherElement gatherElement in itemTableElement.Elements)
				{
					float value = UnityEngine.Random.value;
					if (value < gatherElement.prob)
					{
						StuffItemInfo item = gameInfo.GetItem(gatherElement.categoryID, gatherElement.itemID);
						if (item != null)
						{
							int num = UnityEngine.Random.Range(gatherElement.minCount, gatherElement.maxCount + 1);
							if (num > 0)
							{
								searchInfo.ItemList.Add(new Actor.ItemSearchInfo
								{
									name = item.Name,
									categoryID = gatherElement.categoryID,
									id = gatherElement.itemID,
									count = num
								});
							}
						}
					}
				}
				if (searchInfo.ItemList.IsNullOrEmpty<Actor.ItemSearchInfo>())
				{
					searchInfo.IsSuccess = false;
				}
				return searchInfo;
			}
			return new Actor.SearchInfo
			{
				IsSuccess = false
			};
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0028BF20 File Offset: 0x0028A320
		public void ResetEquipEventItem(EquipEventItemInfo itemInfo)
		{
			if (this.EquipedItem != null)
			{
				if (itemInfo != null)
				{
					if (this.EquipedItem.EventItemID == itemInfo.EventItemID)
					{
						if (!this.EquipedItem.AsGameObject.activeSelf)
						{
							this.EquipedItem.AsGameObject.SetActive(true);
						}
					}
					else
					{
						this.ReleaseEquipedEventItem();
						this.LoadEquipedEventItem(itemInfo);
					}
				}
				else
				{
					this.ReleaseEquipedEventItem();
				}
			}
			else if (itemInfo != null)
			{
				this.LoadEquipedEventItem(itemInfo);
			}
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x0028BFA9 File Offset: 0x0028A3A9
		public virtual void LoadEventItems(PlayState playState)
		{
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x0028BFAB File Offset: 0x0028A3AB
		public virtual void LoadEventParticles(int eventID, int poseID)
		{
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x0028BFB0 File Offset: 0x0028A3B0
		public void ReleaseEquipedEventItem()
		{
			if (this.EquipedItem == null)
			{
				return;
			}
			if (this.EquipedItem.AsGameObject != null)
			{
				UnityEngine.Object.Destroy(this.EquipedItem.AsGameObject);
				this.EquipedItem.AsGameObject = null;
			}
			this.EquipedItem = null;
		}

		// Token: 0x0600615D RID: 24925
		protected abstract void LoadEquipedEventItem(EquipEventItemInfo eventItemInfo);

		// Token: 0x0600615E RID: 24926 RVA: 0x0028C002 File Offset: 0x0028A402
		public void LoadEventItem(int itemID, PlayState.ItemInfo itemInfo, ActionItemInfo eventItemInfo)
		{
			this.LoadEventItem(itemID, itemInfo.parentName, itemInfo.isSync, eventItemInfo);
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0028C01C File Offset: 0x0028A41C
		public GameObject LoadEventItem(int itemID, string parentName, bool isSync, ActionItemInfo eventItemInfo)
		{
			GameObject gameObject = null;
			if (this.CurrentPoint != null)
			{
				gameObject = this.CurrentPoint.CreateEventItems(itemID, parentName, isSync);
			}
			if (gameObject == null)
			{
				gameObject = CommonLib.LoadAsset<GameObject>(eventItemInfo.assetbundleInfo.assetbundle, eventItemInfo.assetbundleInfo.asset, false, eventItemInfo.assetbundleInfo.manifest);
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == eventItemInfo.assetbundleInfo.assetbundle && x.Item2 == eventItemInfo.assetbundleInfo.manifest))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(eventItemInfo.assetbundleInfo.assetbundle, eventItemInfo.assetbundleInfo.manifest));
				}
			}
			if (gameObject != null)
			{
				GameObject gameObject2 = this.ChaControl.animBody.transform.FindLoop(parentName);
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject, gameObject2.transform, false);
				gameObject3.SetActiveIfDifferent(true);
				gameObject3.gameObject.name = gameObject.gameObject.name;
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.transform.localRotation = Quaternion.identity;
				gameObject3.transform.localScale = Vector3.one;
				this.Animation.Items.Add(new UnityEx.ValueTuple<int, GameObject>(itemID, gameObject3));
				Renderer[] componentsInChildren = gameObject3.GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer in componentsInChildren)
				{
					renderer.enabled = false;
				}
				this.Animation.ItemRenderers.Add(new UnityEx.ValueTuple<int, Renderer[]>(itemID, componentsInChildren));
				if (eventItemInfo.existsAnimation)
				{
					Animator component = gameObject3.GetComponent<Animator>();
					RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(eventItemInfo.animeAssetBundle);
					component.runtimeAnimatorController = runtimeAnimatorController;
					this.Animation.ItemAnimatorTable[gameObject3.GetInstanceID()] = new ItemAnimInfo
					{
						Animator = component,
						Parameters = component.parameters,
						Sync = isSync
					};
				}
				Manager.Resources.TriValues triValues;
				if (Singleton<Manager.Resources>.Instance.Animation.ItemScaleTable.TryGetValue(itemID, out triValues))
				{
					Actor.ItemScaleInfo item = new Actor.ItemScaleInfo
					{
						TargetItem = gameObject3,
						ScaleMode = triValues.ScaleType,
						SThreshold = triValues.SThreshold,
						MThreshold = triValues.MThreshold,
						LThreshold = triValues.LThreshold
					};
					this._scaleCtrlInfos.Add(item);
				}
				return gameObject3;
			}
			AssetBundleInfo assetbundleInfo = eventItemInfo.assetbundleInfo;
			return null;
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x0028C2C0 File Offset: 0x0028A6C0
		protected void LoadEventParticle(Dictionary<int, List<AnimeParticleEventInfo>> eventTable)
		{
			this.ClearParticles();
			foreach (KeyValuePair<int, List<AnimeParticleEventInfo>> keyValuePair in eventTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<AnimeParticleEventInfo>())
				{
					foreach (AnimeParticleEventInfo animeParticleEventInfo in keyValuePair.Value)
					{
						if (animeParticleEventInfo.EventID % 2 != 1)
						{
							int particleID = animeParticleEventInfo.ParticleID;
							string root = animeParticleEventInfo.Root;
							int key = animeParticleEventInfo.EventID / 2;
							Dictionary<int, System.Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>> dictionary;
							if (!this.Animation.Particles.TryGetValue(key, out dictionary))
							{
								dictionary = (this.Animation.Particles[key] = new Dictionary<int, System.Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>());
							}
							if (!dictionary.ContainsKey(particleID))
							{
								AssetBundleInfo assetInfo;
								if (Singleton<Manager.Resources>.Instance.Map.EventParticleTable.TryGetValue(particleID, out assetInfo))
								{
									GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetInfo.assetbundle, assetInfo.asset, false, assetInfo.manifest);
									if (!(gameObject == null))
									{
										if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == assetInfo.assetbundle && x.Item2 == assetInfo.manifest))
										{
											MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(assetInfo.assetbundle, assetInfo.manifest));
										}
										GameObject gameObject2 = this.ChaControl.animBody.transform.FindLoop(root);
										Transform parent = ((gameObject2 != null) ? gameObject2.transform : null) ?? this.Locomotor.transform;
										GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject, parent, false);
										ParticleSystem particleSystem = gameObject3.GetComponent<ParticleSystem>();
										if (particleSystem == null)
										{
											particleSystem = gameObject3.GetComponentInChildren<ParticleSystem>();
										}
										if (particleSystem == null)
										{
											if (gameObject3 != null)
											{
												UnityEngine.Object.Destroy(gameObject3);
											}
										}
										else
										{
											gameObject3.name = gameObject.name;
											gameObject3.transform.localPosition = Vector3.zero;
											gameObject3.transform.localRotation = Quaternion.identity;
											gameObject3.transform.localScale = Vector3.one;
											ParticleSystem.MainModule main = particleSystem.main;
											if (main.playOnAwake)
											{
												main.playOnAwake = false;
											}
											if (particleSystem.isPlaying)
											{
												particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
											}
											ParticleSystemRenderer component = particleSystem.GetComponent<ParticleSystemRenderer>();
											dictionary[particleID] = new System.Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>(gameObject3, particleSystem, component);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x0028C5CC File Offset: 0x0028A9CC
		public void SetDefaultStateHousingItem()
		{
			if (this.CurrentPoint != null)
			{
				string housingAnimationDefault = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.HousingAnimationDefault;
				HousingActionPointAnimation[] componentsInChildren = this.CurrentPoint.GetComponentsInChildren<HousingActionPointAnimation>(true);
				if (!componentsInChildren.IsNullOrEmpty<HousingActionPointAnimation>())
				{
					foreach (HousingActionPointAnimation housingActionPointAnimation in componentsInChildren)
					{
						housingActionPointAnimation.Animator.Play(housingAnimationDefault, 0);
					}
				}
			}
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x0028C644 File Offset: 0x0028AA44
		public void ChangeAnimator(string assetbundle, string asset)
		{
			RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetbundle, asset, string.Empty);
			if (runtimeAnimatorController != null)
			{
				this.Animation.SetAnimatorController(runtimeAnimatorController);
			}
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x0028C676 File Offset: 0x0028AA76
		public void ChangeAnimator(RuntimeAnimatorController rac)
		{
			if (rac != null)
			{
				this.Animation.SetAnimatorController(rac);
			}
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x0028C690 File Offset: 0x0028AA90
		public void ClearItems()
		{
			this.Animation.ClearItems();
			this._scaleCtrlInfos.Clear();
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0028C6A8 File Offset: 0x0028AAA8
		public void SetActiveOnEquipedItem(bool active)
		{
			if (this.EquipedItem == null)
			{
				return;
			}
			if (this.EquipedItem.AsGameObject == null)
			{
				return;
			}
			if (this.EquipedItem.AsGameObject.activeSelf == active)
			{
				return;
			}
			this.EquipedItem.AsGameObject.SetActive(active);
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0028C700 File Offset: 0x0028AB00
		public void ClearParticles()
		{
			this.Animation.ClearParticles();
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0028C710 File Offset: 0x0028AB10
		public void SetStand(Transform t, bool enableFade, float fadeTime, int dirType)
		{
			if (this._standDisposable != null)
			{
				this._standDisposable.Dispose();
			}
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(fadeTime, false).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			this._standDisposable = connectableObservable.Connect();
			Vector3 position = this.Position;
			Quaternion rotation = this.Rotation;
			if (t != null)
			{
				if (dirType != 0)
				{
					if (dirType == 1)
					{
						Vector3 position3 = base.transform.position;
						position3.y = 0f;
						Vector3 position2 = this.Position;
						position2.y = 0f;
						Quaternion lookRotation = Quaternion.LookRotation(Vector3.Normalize(position3 - position2));
						if (enableFade)
						{
							connectableObservable.Subscribe(delegate(TimeInterval<float> x)
							{
								this.Rotation = Quaternion.Slerp(rotation, lookRotation, x.Value);
							});
						}
						else
						{
							this.Rotation = lookRotation;
						}
					}
				}
				else if (enableFade)
				{
					connectableObservable.Subscribe(delegate(TimeInterval<float> x)
					{
						this.Position = Vector3.Lerp(position, t.position, x.Value);
						this.Rotation = Quaternion.Slerp(rotation, t.rotation, x.Value);
					});
				}
				else if (this._navMeshAgent.enabled)
				{
					this.NavMeshWarp(t, 0, 100f);
				}
				else
				{
					this.Position = t.position;
					this.Rotation = t.rotation;
				}
			}
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x0028C891 File Offset: 0x0028AC91
		public void StopStand()
		{
			if (this._standDisposable != null)
			{
				this._standDisposable.Dispose();
				this._standDisposable = null;
			}
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x0028C8B0 File Offset: 0x0028ACB0
		public void NavMeshWarp(Transform t, int idx, float limitDistance = 100f)
		{
			if (t == null)
			{
				return;
			}
			this.NavMeshWarp(t.position, t.rotation, idx, limitDistance);
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x0028C8D4 File Offset: 0x0028ACD4
		public void NavMeshWarp(Vector3 position, Quaternion rotation, int idx, float limitDistance = 100f)
		{
			if (!this._navMeshAgent.Warp(position))
			{
				List<Vector3> list = ListPool<Vector3>.Get();
				for (int i = 0; i < 3; i++)
				{
					Vector3 queryPos = new Vector3(0f, 0f, (float)(10 + i * 10));
					this.SearchAroundPosition(position, queryPos, list);
				}
				Vector3? vector = null;
				if (!list.IsNullOrEmpty<Vector3>())
				{
					vector = new Vector3?(this.Nearest(position, list));
				}
				ListPool<Vector3>.Release(list);
				NavMeshHit navMeshHit;
				if (vector != null)
				{
					this._navMeshAgent.Warp(vector.Value);
				}
				else if (NavMesh.SamplePosition(position, out navMeshHit, limitDistance, this._navMeshAgent.areaMask))
				{
					this._navMeshAgent.Warp(navMeshHit.position);
				}
				else
				{
					foreach (BasePoint basePoint in Singleton<Manager.Map>.Instance.PointAgent.BasePoints)
					{
						if (basePoint.AreaIDInHousing == this.AreaID)
						{
							Transform transform = basePoint.RecoverPoints[0];
							this._navMeshAgent.Warp(transform.position);
						}
					}
				}
			}
			this.Rotation = rotation;
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x0028CA1C File Offset: 0x0028AE1C
		public int TryNavMeshWarp(Vector3 position, Quaternion rotation, int idx, float limitDistance = 100f)
		{
			if (!this._navMeshAgent.Warp(position))
			{
				List<Vector3> list = ListPool<Vector3>.Get();
				for (int i = 0; i < 3; i++)
				{
					Vector3 queryPos = new Vector3(0f, 0f, (float)(10 + i * 10));
					this.SearchAroundPosition(position, queryPos, list);
				}
				Vector3? vector = null;
				if (!list.IsNullOrEmpty<Vector3>())
				{
					vector = new Vector3?(this.Nearest(position, list));
				}
				ListPool<Vector3>.Release(list);
				if (vector != null)
				{
					this._navMeshAgent.Warp(vector.Value);
				}
				else
				{
					NavMeshHit navMeshHit;
					if (!NavMesh.SamplePosition(position, out navMeshHit, limitDistance, this._navMeshAgent.areaMask))
					{
						return -1;
					}
					this._navMeshAgent.Warp(navMeshHit.position);
				}
			}
			this.Rotation = rotation;
			return 0;
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x0028CB00 File Offset: 0x0028AF00
		private void SearchAroundPosition(Vector3 center, Vector3 queryPos, List<Vector3> list)
		{
			for (int i = 0; i < 9; i++)
			{
				Quaternion rotation = Quaternion.Euler(0f, (float)(40 * i), 0f);
				Vector3 vector = center + rotation * queryPos;
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(vector, out navMeshHit, 10f, this._navMeshAgent.areaMask))
				{
					list.Add(vector);
				}
			}
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x0028CB6C File Offset: 0x0028AF6C
		private Vector3 Nearest(Vector3 center, List<Vector3> list)
		{
			if (list.IsNullOrEmpty<Vector3>())
			{
				return Vector3.zero;
			}
			float num = float.MaxValue;
			Vector3 result = Vector3.zero;
			foreach (Vector3 vector in list)
			{
				float num2 = Vector3.Distance(center, vector);
				if (num > num2)
				{
					num = num2;
					result = vector;
				}
			}
			return result;
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x0600616E RID: 24942 RVA: 0x0028CBF0 File Offset: 0x0028AFF0
		public List<Actor.ItemScaleInfo> ScaleCtrlInfo
		{
			[CompilerGenerated]
			get
			{
				return this._scaleCtrlInfos;
			}
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0028CBF8 File Offset: 0x0028AFF8
		public void SetLookPtn(int ptnEye, int ptnNeck)
		{
			this.ChaControl.ChangeLookEyesPtn(ptnEye);
			this.ChaControl.ChangeLookNeckPtn(ptnNeck, 1f);
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0028CC18 File Offset: 0x0028B018
		public void SetLookTarget(int targetEye, int targetNeck, Transform target = null)
		{
			this.ChaControl.ChangeLookEyesTarget(targetEye, target, 0.5f, 0f, 1f, 2f);
			this.ChaControl.ChangeLookNeckTarget(targetNeck, target, 0.5f, 0f, 1f, 0.8f);
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0028CC68 File Offset: 0x0028B068
		public bool IsVisibleDistanceAll(Transform cameraTransform)
		{
			float charaVisibleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.CharaVisibleDistance;
			foreach (Transform transform in this.ChaFovTargets)
			{
				float num = Vector3.Distance(transform.position, cameraTransform.position);
				if (num < charaVisibleDistance)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0028CCC4 File Offset: 0x0028B0C4
		public bool IsVisibleDistance(Transform cameraTransform, Actor.BodyPart parts, float limitDistance)
		{
			Transform transform;
			if (!this.ChaBodyParts.TryGetValue(parts, out transform))
			{
				return false;
			}
			float num = Vector3.Distance(transform.position, cameraTransform.position);
			return num >= limitDistance;
		}

		// Token: 0x040055E2 RID: 21986
		public int ID;

		// Token: 0x040055E9 RID: 21993
		[SerializeField]
		protected Rigidbody _rigidbody;

		// Token: 0x040055EA RID: 21994
		[SerializeField]
		protected NavMeshAgent _navMeshAgent;

		// Token: 0x040055EB RID: 21995
		[SerializeField]
		protected NavMeshObstacle _navMeshObstacle;

		// Token: 0x040055EC RID: 21996
		[SerializeField]
		private float _accelerationTime = 0.2f;

		// Token: 0x040055ED RID: 21997
		[SerializeField]
		private float _maxVerticalVelocityOnGround = 3f;

		// Token: 0x040055EF RID: 21999
		[SerializeField]
		private float _slopeStartAngle = 50f;

		// Token: 0x040055F0 RID: 22000
		[SerializeField]
		private float _slopeEndAngle = 85f;

		// Token: 0x040055F1 RID: 22001
		private ReadOnlyDictionary<Actor.FovBodyPart, Transform> _fovBodyPartReadOnlyTable;

		// Token: 0x040055F2 RID: 22002
		[SerializeField]
		protected Dictionary<Actor.FovBodyPart, Transform> _fovTargetPointTable = new Dictionary<Actor.FovBodyPart, Transform>();

		// Token: 0x040055F3 RID: 22003
		private Transform[] _fovTargets;

		// Token: 0x040055F4 RID: 22004
		private ReadOnlyDictionary<Actor.BodyPart, Transform> _chaBodyPartsReadonlyDic;

		// Token: 0x040055F5 RID: 22005
		[SerializeField]
		protected Dictionary<Actor.BodyPart, Transform> _chaBodyParts = new Dictionary<Actor.BodyPart, Transform>();

		// Token: 0x040055F6 RID: 22006
		protected Transform[] _chaFovTargets;

		// Token: 0x040055F7 RID: 22007
		private LocomotionProfile _locomotionProfile;

		// Token: 0x040055FE RID: 22014
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private MapArea _mapArea;

		// Token: 0x04005600 RID: 22016
		protected IntReactiveProperty _mapAreaID;

		// Token: 0x04005609 RID: 22025
		private ObservableStateMachineTrigger _observableStateMachine;

		// Token: 0x0400560A RID: 22026
		private int? _instanceID;

		// Token: 0x0400560C RID: 22028
		protected UnityEx.ValueTuple<int, int> _scheduleThreshold = default(UnityEx.ValueTuple<int, int>);

		// Token: 0x0400560D RID: 22029
		protected Actor.BehaviorSchedule _schedule = default(Actor.BehaviorSchedule);

		// Token: 0x0400560E RID: 22030
		protected UnityEx.ValueTuple<bool, TimeSpan> _gestureTimeLimit = new UnityEx.ValueTuple<bool, TimeSpan>(false, TimeSpan.MaxValue);

		// Token: 0x0400560F RID: 22031
		protected int _originAvoidancePriority = -1;

		// Token: 0x04005610 RID: 22032
		private IEnumerator _talkEnumerator;

		// Token: 0x04005611 RID: 22033
		private IDisposable _talkDisposable;

		// Token: 0x04005612 RID: 22034
		private IEnumerator _withAnimalEnumerator;

		// Token: 0x04005613 RID: 22035
		private IDisposable _withAnimalDisposable;

		// Token: 0x04005614 RID: 22036
		private IDisposable _standDisposable;

		// Token: 0x04005615 RID: 22037
		protected List<Actor.ItemScaleInfo> _scaleCtrlInfos = new List<Actor.ItemScaleInfo>();

		// Token: 0x02000C3A RID: 3130
		public struct BehaviorSchedule
		{
			// Token: 0x06006178 RID: 24952 RVA: 0x0028CD88 File Offset: 0x0028B188
			public BehaviorSchedule(bool enabled_, string name_, float duration_)
			{
				this.enabled = enabled_;
				this.name = name_;
				this.duration = duration_;
				this.elapsedTime = 0f;
				this.useGameTime = false;
				this.progress = true;
			}

			// Token: 0x04005617 RID: 22039
			public bool enabled;

			// Token: 0x04005618 RID: 22040
			public string name;

			// Token: 0x04005619 RID: 22041
			public float duration;

			// Token: 0x0400561A RID: 22042
			public float elapsedTime;

			// Token: 0x0400561B RID: 22043
			public bool useGameTime;

			// Token: 0x0400561C RID: 22044
			public bool progress;
		}

		// Token: 0x02000C3B RID: 3131
		public struct InputInfo
		{
			// Token: 0x06006179 RID: 24953 RVA: 0x0028CDB8 File Offset: 0x0028B1B8
			public void Init()
			{
				this.move = Vector3.zero;
				this.lookPos = Vector3.zero;
			}

			// Token: 0x0400561D RID: 22045
			public Vector3 move;

			// Token: 0x0400561E RID: 22046
			public Vector3 lookPos;
		}

		// Token: 0x02000C3C RID: 3132
		public enum FovBodyPart
		{
			// Token: 0x04005620 RID: 22048
			Body,
			// Token: 0x04005621 RID: 22049
			Head,
			// Token: 0x04005622 RID: 22050
			Foot
		}

		// Token: 0x02000C3D RID: 3133
		public enum BodyPart
		{
			// Token: 0x04005624 RID: 22052
			Body,
			// Token: 0x04005625 RID: 22053
			Bust,
			// Token: 0x04005626 RID: 22054
			Head,
			// Token: 0x04005627 RID: 22055
			LeftFoot,
			// Token: 0x04005628 RID: 22056
			RightFoot
		}

		// Token: 0x02000C3E RID: 3134
		public class SearchInfo
		{
			// Token: 0x17001353 RID: 4947
			// (get) Token: 0x0600617B RID: 24955 RVA: 0x0028CDE3 File Offset: 0x0028B1E3
			// (set) Token: 0x0600617C RID: 24956 RVA: 0x0028CDEB File Offset: 0x0028B1EB
			public bool IsSuccess { get; set; }

			// Token: 0x17001354 RID: 4948
			// (get) Token: 0x0600617D RID: 24957 RVA: 0x0028CDF4 File Offset: 0x0028B1F4
			// (set) Token: 0x0600617E RID: 24958 RVA: 0x0028CDFC File Offset: 0x0028B1FC
			public List<Actor.ItemSearchInfo> ItemList { get; set; } = new List<Actor.ItemSearchInfo>();
		}

		// Token: 0x02000C3F RID: 3135
		public struct ItemSearchInfo
		{
			// Token: 0x0400562B RID: 22059
			public string name;

			// Token: 0x0400562C RID: 22060
			public int categoryID;

			// Token: 0x0400562D RID: 22061
			public int id;

			// Token: 0x0400562E RID: 22062
			public int count;
		}

		// Token: 0x02000C40 RID: 3136
		public class ItemScaleInfo
		{
			// Token: 0x17001355 RID: 4949
			// (get) Token: 0x06006180 RID: 24960 RVA: 0x0028CE0D File Offset: 0x0028B20D
			// (set) Token: 0x06006181 RID: 24961 RVA: 0x0028CE15 File Offset: 0x0028B215
			public GameObject TargetItem { get; set; }

			// Token: 0x17001356 RID: 4950
			// (get) Token: 0x06006182 RID: 24962 RVA: 0x0028CE1E File Offset: 0x0028B21E
			// (set) Token: 0x06006183 RID: 24963 RVA: 0x0028CE26 File Offset: 0x0028B226
			public int ScaleMode { get; set; }

			// Token: 0x17001357 RID: 4951
			// (get) Token: 0x06006184 RID: 24964 RVA: 0x0028CE2F File Offset: 0x0028B22F
			// (set) Token: 0x06006185 RID: 24965 RVA: 0x0028CE37 File Offset: 0x0028B237
			public float SThreshold { get; set; }

			// Token: 0x17001358 RID: 4952
			// (get) Token: 0x06006186 RID: 24966 RVA: 0x0028CE40 File Offset: 0x0028B240
			// (set) Token: 0x06006187 RID: 24967 RVA: 0x0028CE48 File Offset: 0x0028B248
			public float MThreshold { get; set; }

			// Token: 0x17001359 RID: 4953
			// (get) Token: 0x06006188 RID: 24968 RVA: 0x0028CE51 File Offset: 0x0028B251
			// (set) Token: 0x06006189 RID: 24969 RVA: 0x0028CE59 File Offset: 0x0028B259
			public float LThreshold { get; set; }

			// Token: 0x0600618A RID: 24970 RVA: 0x0028CE64 File Offset: 0x0028B264
			public float Evaluate(float t)
			{
				t = Mathf.Clamp01(t);
				if (t < 0.5f)
				{
					float t2 = Mathf.InverseLerp(0f, 0.5f, t);
					return Mathf.Lerp(this.SThreshold, this.MThreshold, t2);
				}
				float t3 = Mathf.InverseLerp(0.5f, 1f, t);
				return Mathf.Lerp(this.MThreshold, this.LThreshold, t3);
			}
		}
	}
}
