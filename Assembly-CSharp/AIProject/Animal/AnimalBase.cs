using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AIProject.Animal.Resources;
using AIProject.Definitions;
using AIProject.Scene;
using IllusionUtility.GetUtility;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000B49 RID: 2889
	public abstract class AnimalBase : MonoBehaviour, IAnimalActionPointUser, ICommandable
	{
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x00253AE0 File Offset: 0x00251EE0
		public AnimalTypes AnimalType
		{
			[CompilerGenerated]
			get
			{
				return this._animalType;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x0600547A RID: 21626 RVA: 0x00253AE8 File Offset: 0x00251EE8
		public BreedingTypes BreedingType
		{
			[CompilerGenerated]
			get
			{
				return this._breedingType;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x00253AF0 File Offset: 0x00251EF0
		// (set) Token: 0x0600547C RID: 21628 RVA: 0x00253AF8 File Offset: 0x00251EF8
		public ActionTypes ActionType { get; protected set; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x00253B01 File Offset: 0x00251F01
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x0600547E RID: 21630 RVA: 0x00253B09 File Offset: 0x00251F09
		// (set) Token: 0x0600547F RID: 21631 RVA: 0x00253B11 File Offset: 0x00251F11
		public string IdentifierName { get; private set; } = string.Empty;

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005480 RID: 21632 RVA: 0x00253B1A File Offset: 0x00251F1A
		public virtual ItemIDKeyPair ItemID
		{
			[CompilerGenerated]
			get
			{
				return this._itemID;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x00253B24 File Offset: 0x00251F24
		public StuffItemInfo ItemInfo
		{
			get
			{
				if (!Singleton<Manager.Resources>.IsInstance())
				{
					return null;
				}
				return Singleton<Manager.Resources>.Instance.GameInfo.GetItem(this.ItemID.categoryID, this.ItemID.itemID);
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06005482 RID: 21634 RVA: 0x00253B68 File Offset: 0x00251F68
		public virtual int NavMeshAreaMask
		{
			[CompilerGenerated]
			get
			{
				return -1;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x00253B6B File Offset: 0x00251F6B
		// (set) Token: 0x06005484 RID: 21636 RVA: 0x00253B78 File Offset: 0x00251F78
		public string ObjName
		{
			get
			{
				return base.gameObject.name;
			}
			set
			{
				base.gameObject.name = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005485 RID: 21637 RVA: 0x00253B86 File Offset: 0x00251F86
		// (set) Token: 0x06005486 RID: 21638 RVA: 0x00253B8E File Offset: 0x00251F8E
		public int AnimalID { get; protected set; }

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06005487 RID: 21639 RVA: 0x00253B97 File Offset: 0x00251F97
		public int AnimalTypeID
		{
			[CompilerGenerated]
			get
			{
				return this._animalTypeID;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x00253B9F File Offset: 0x00251F9F
		// (set) Token: 0x06005489 RID: 21641 RVA: 0x00253BA7 File Offset: 0x00251FA7
		public int ChunkID { get; protected set; } = -1;

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x00253BB0 File Offset: 0x00251FB0
		// (set) Token: 0x0600548B RID: 21643 RVA: 0x00253BB8 File Offset: 0x00251FB8
		public int AreaID { get; protected set; } = -1;

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x0600548C RID: 21644 RVA: 0x00253BC1 File Offset: 0x00251FC1
		// (set) Token: 0x0600548D RID: 21645 RVA: 0x00253BC9 File Offset: 0x00251FC9
		public MapArea CurrentMapArea { get; protected set; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x0600548E RID: 21646 RVA: 0x00253BD2 File Offset: 0x00251FD2
		// (set) Token: 0x0600548F RID: 21647 RVA: 0x00253BDA File Offset: 0x00251FDA
		public MapArea TargetMapArea { get; protected set; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x00253BE3 File Offset: 0x00251FE3
		// (set) Token: 0x06005491 RID: 21649 RVA: 0x00253BEB File Offset: 0x00251FEB
		public int MapAreaID { get; protected set; } = -1;

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005492 RID: 21650 RVA: 0x00253BF4 File Offset: 0x00251FF4
		public static string DefaultLocomotionParamName { get; } = "locomotion";

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x00253BFB File Offset: 0x00251FFB
		// (set) Token: 0x06005494 RID: 21652 RVA: 0x00253C11 File Offset: 0x00252011
		public virtual bool Active
		{
			get
			{
				return this.active_ && base.isActiveAndEnabled;
			}
			set
			{
				this.active_ = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x00253C1C File Offset: 0x0025201C
		public int InstanceID
		{
			get
			{
				return ((this._instanceID == null) ? (this._instanceID = new int?(base.GetInstanceID())) : this._instanceID).Value;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x00253C60 File Offset: 0x00252060
		// (set) Token: 0x06005497 RID: 21655 RVA: 0x00253C68 File Offset: 0x00252068
		public Transform Target { get; set; }

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x00253C71 File Offset: 0x00252071
		public bool HasTarget
		{
			[CompilerGenerated]
			get
			{
				return this.Target != null;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005499 RID: 21657 RVA: 0x00253C7F File Offset: 0x0025207F
		// (set) Token: 0x0600549A RID: 21658 RVA: 0x00253C87 File Offset: 0x00252087
		public Transform FollowTarget { get; set; }

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x0600549B RID: 21659 RVA: 0x00253C90 File Offset: 0x00252090
		public bool HasFollowTarget
		{
			[CompilerGenerated]
			get
			{
				return this.FollowTarget != null;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x0600549C RID: 21660 RVA: 0x00253C9E File Offset: 0x0025209E
		// (set) Token: 0x0600549D RID: 21661 RVA: 0x00253CA6 File Offset: 0x002520A6
		public Actor TargetActor { get; set; }

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x0600549E RID: 21662 RVA: 0x00253CAF File Offset: 0x002520AF
		// (set) Token: 0x0600549F RID: 21663 RVA: 0x00253CB7 File Offset: 0x002520B7
		public Actor FollowActor { get; set; }

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060054A0 RID: 21664 RVA: 0x00253CC0 File Offset: 0x002520C0
		public bool HasFollowActor
		{
			[CompilerGenerated]
			get
			{
				return this.FollowActor != null;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (set) Token: 0x060054A1 RID: 21665 RVA: 0x00253CCE File Offset: 0x002520CE
		public Transform LookTarget
		{
			set
			{
				if (this.eyeController)
				{
					this.eyeController.target = value;
				}
				if (this.neckController)
				{
					this.neckController.target = value;
				}
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060054A2 RID: 21666 RVA: 0x00253D08 File Offset: 0x00252108
		// (set) Token: 0x060054A3 RID: 21667 RVA: 0x00253D10 File Offset: 0x00252110
		public bool LookWaitMode { get; protected set; }

		// Token: 0x060054A4 RID: 21668 RVA: 0x00253D19 File Offset: 0x00252119
		public void ClearTargetObject()
		{
			this.Target = null;
			this.FollowTarget = null;
			this.LookTarget = null;
			this.TargetActor = null;
			this.FollowActor = null;
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x00253D3E File Offset: 0x0025213E
		// (set) Token: 0x060054A6 RID: 21670 RVA: 0x00253D46 File Offset: 0x00252146
		public virtual bool BadMood { get; set; }

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x00253D4F File Offset: 0x0025214F
		// (set) Token: 0x060054A8 RID: 21672 RVA: 0x00253D57 File Offset: 0x00252157
		public virtual bool WaitMode { get; set; }

		// Token: 0x060054A9 RID: 21673 RVA: 0x00253D60 File Offset: 0x00252160
		public virtual bool Wait()
		{
			return this.WaitMode || this.LookWaitMode;
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x00253D76 File Offset: 0x00252176
		public bool HasDestination
		{
			[CompilerGenerated]
			get
			{
				return this.destination != null;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x00253D83 File Offset: 0x00252183
		// (set) Token: 0x060054AC RID: 21676 RVA: 0x00253D8C File Offset: 0x0025218C
		public AnimalState CurrentState
		{
			get
			{
				return this.currentState_;
			}
			set
			{
				bool flag = this.currentState_ != value;
				if (flag)
				{
					this.PrevState = this.currentState_;
					this.currentState_ = value;
					float num = 0f;
					this.StateTimeLimit = num;
					this.StateCounter = num;
					this.StateChanged();
					if (value == AnimalState.Destroyed)
					{
						this.Destroy();
					}
				}
			}
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00253DE7 File Offset: 0x002521E7
		protected void SetDestroyState()
		{
			this.currentState_ = AnimalState.Destroyed;
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x00253DF1 File Offset: 0x002521F1
		// (set) Token: 0x060054AF RID: 21679 RVA: 0x00253DF9 File Offset: 0x002521F9
		public AnimalState PrevState { get; private set; }

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060054B0 RID: 21680 RVA: 0x00253E02 File Offset: 0x00252202
		// (set) Token: 0x060054B1 RID: 21681 RVA: 0x00253E0A File Offset: 0x0025220A
		public AnimalState NextState { get; set; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060054B2 RID: 21682 RVA: 0x00253E13 File Offset: 0x00252213
		// (set) Token: 0x060054B3 RID: 21683 RVA: 0x00253E1B File Offset: 0x0025221B
		public AnimalState BackupState { get; set; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x00253E24 File Offset: 0x00252224
		public bool IsLovely
		{
			[CompilerGenerated]
			get
			{
				return this.CurrentState == AnimalState.LovelyFollow || this.CurrentState == AnimalState.LovelyIdle;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x00253E3F File Offset: 0x0025223F
		public bool IsPrevLovely
		{
			[CompilerGenerated]
			get
			{
				return this.PrevState == AnimalState.LovelyFollow || this.PrevState == AnimalState.LovelyIdle;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x060054B6 RID: 21686 RVA: 0x00253E5A File Offset: 0x0025225A
		// (set) Token: 0x060054B7 RID: 21687 RVA: 0x00253E62 File Offset: 0x00252262
		public bool EnabledStateUpdate { get; set; } = true;

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x060054B8 RID: 21688 RVA: 0x00253E6B File Offset: 0x0025226B
		// (set) Token: 0x060054B9 RID: 21689 RVA: 0x00253E73 File Offset: 0x00252273
		public bool AutoChangeAnimation { get; set; } = true;

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x00253E7C File Offset: 0x0025227C
		// (set) Token: 0x060054BB RID: 21691 RVA: 0x00253E84 File Offset: 0x00252284
		public bool AnimationEndUpdate { get; set; } = true;

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x00253E8D File Offset: 0x0025228D
		public bool StateUpdatePossible
		{
			[CompilerGenerated]
			get
			{
				return (this.AnimationEndUpdate && this.IsRunningAnimation == 1) || !this.AnimationEndUpdate;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x00253EB2 File Offset: 0x002522B2
		// (set) Token: 0x060054BE RID: 21694 RVA: 0x00253EBA File Offset: 0x002522BA
		public float StateCounter { get; protected set; }

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x00253EC3 File Offset: 0x002522C3
		// (set) Token: 0x060054C0 RID: 21696 RVA: 0x00253ECB File Offset: 0x002522CB
		public float StateTimeLimit { get; protected set; }

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x00253ED4 File Offset: 0x002522D4
		public bool HasActionPoint
		{
			[CompilerGenerated]
			get
			{
				return this.actionPoint != null;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060054C2 RID: 21698 RVA: 0x00253EE2 File Offset: 0x002522E2
		public bool AnimatorEmtpy
		{
			[CompilerGenerated]
			get
			{
				return this.animator == null;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x00253EF0 File Offset: 0x002522F0
		public bool AnimatorEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.animator != null && this.animator.isActiveAndEnabled;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060054C4 RID: 21700 RVA: 0x00253F14 File Offset: 0x00252314
		public bool AnimatorControllerEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.animator != null && this.animator.isActiveAndEnabled && this.animator.isInitialized && this.animator.runtimeAnimatorController != null;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x00253F66 File Offset: 0x00252366
		// (set) Token: 0x060054C6 RID: 21702 RVA: 0x00253F6E File Offset: 0x0025236E
		private protected Dictionary<int, Dictionary<int, AnimalPlayState>> AnimCommonTable { protected get; private set; }

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x00253F77 File Offset: 0x00252377
		// (set) Token: 0x060054C8 RID: 21704 RVA: 0x00253F7F File Offset: 0x0025237F
		private protected ReadOnlyDictionary<AnimalState, LookState> LookStateTable { protected get; private set; }

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x00253F88 File Offset: 0x00252388
		// (set) Token: 0x060054CA RID: 21706 RVA: 0x00253F90 File Offset: 0x00252390
		private protected Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>>> ExpressionTable { protected get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>>>();

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x00253F99 File Offset: 0x00252399
		// (set) Token: 0x060054CC RID: 21708 RVA: 0x00253FA1 File Offset: 0x002523A1
		private protected Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>> CurrentExpressionTable { protected get; private set; }

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x060054CD RID: 21709 RVA: 0x00253FAA File Offset: 0x002523AA
		public bool PlayingInAnimation
		{
			[CompilerGenerated]
			get
			{
				return this.inAnimDisposable != null;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060054CE RID: 21710 RVA: 0x00253FB8 File Offset: 0x002523B8
		public bool PlayingOutAnimation
		{
			[CompilerGenerated]
			get
			{
				return this.outAnimDisposable != null;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x00253FC6 File Offset: 0x002523C6
		public int IsRunningAnimation
		{
			get
			{
				if (this.PlayingInAnimation)
				{
					return 0;
				}
				if (this.PlayingOutAnimation)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x00253FE3 File Offset: 0x002523E3
		private Queue<AnimalPlayState.StateInfo> InAnimStates { get; } = new Queue<AnimalPlayState.StateInfo>();

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x060054D1 RID: 21713 RVA: 0x00253FEB File Offset: 0x002523EB
		private Queue<AnimalPlayState.StateInfo> OutAnimStates { get; } = new Queue<AnimalPlayState.StateInfo>();

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x060054D2 RID: 21714 RVA: 0x00253FF3 File Offset: 0x002523F3
		// (set) Token: 0x060054D3 RID: 21715 RVA: 0x00254000 File Offset: 0x00252400
		public virtual Vector3 Position
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

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x0025400E File Offset: 0x0025240E
		// (set) Token: 0x060054D5 RID: 21717 RVA: 0x0025401B File Offset: 0x0025241B
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

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x00254029 File Offset: 0x00252429
		// (set) Token: 0x060054D7 RID: 21719 RVA: 0x00254036 File Offset: 0x00252436
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

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x00254044 File Offset: 0x00252444
		// (set) Token: 0x060054D9 RID: 21721 RVA: 0x00254051 File Offset: 0x00252451
		public Vector3 LocalPosition
		{
			get
			{
				return base.transform.localPosition;
			}
			set
			{
				base.transform.localPosition = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x0025405F File Offset: 0x0025245F
		// (set) Token: 0x060054DB RID: 21723 RVA: 0x0025406C File Offset: 0x0025246C
		public Vector3 LocalEulerAngles
		{
			get
			{
				return base.transform.localEulerAngles;
			}
			set
			{
				base.transform.localEulerAngles = value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x0025407A File Offset: 0x0025247A
		// (set) Token: 0x060054DD RID: 21725 RVA: 0x00254087 File Offset: 0x00252487
		public Quaternion LocalRotation
		{
			get
			{
				return base.transform.localRotation;
			}
			set
			{
				base.transform.localRotation = value;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x060054DE RID: 21726 RVA: 0x00254095 File Offset: 0x00252495
		public Vector3 Forward
		{
			[CompilerGenerated]
			get
			{
				return base.transform.forward;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x002540A2 File Offset: 0x002524A2
		public Vector3 Right
		{
			[CompilerGenerated]
			get
			{
				return base.transform.right;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x060054E0 RID: 21728 RVA: 0x002540AF File Offset: 0x002524AF
		public Vector3 Up
		{
			[CompilerGenerated]
			get
			{
				return base.transform.up;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x002540BC File Offset: 0x002524BC
		// (set) Token: 0x060054E2 RID: 21730 RVA: 0x002540C3 File Offset: 0x002524C3
		public static bool CreateDisplay { get; set; } = true;

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x060054E3 RID: 21731 RVA: 0x002540CB File Offset: 0x002524CB
		// (set) Token: 0x060054E4 RID: 21732 RVA: 0x002540D3 File Offset: 0x002524D3
		public bool IsForcedBodyEnabled { get; private set; }

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x060054E5 RID: 21733 RVA: 0x002540DC File Offset: 0x002524DC
		// (set) Token: 0x060054E6 RID: 21734 RVA: 0x002540E4 File Offset: 0x002524E4
		public bool IsNormalBodyEnabled { get; private set; }

		// Token: 0x060054E7 RID: 21735 RVA: 0x002540ED File Offset: 0x002524ED
		public void SetForcedBodyEnabled(bool _enabled)
		{
			this.IsForcedBodyEnabled = _enabled;
			this.RefreshBodyEnabled();
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060054E8 RID: 21736 RVA: 0x002540FC File Offset: 0x002524FC
		// (set) Token: 0x060054E9 RID: 21737 RVA: 0x002541A9 File Offset: 0x002525A9
		public virtual bool BodyEnabled
		{
			get
			{
				bool flag = false;
				if (!this.bodyRenderers.IsNullOrEmpty<Renderer>())
				{
					foreach (Renderer renderer in this.bodyRenderers)
					{
						if (!(renderer == null))
						{
							flag |= renderer.enabled;
						}
					}
				}
				if (!this.bodyParticleRenderers.IsNullOrEmpty<ParticleSystemRenderer>())
				{
					foreach (ParticleSystemRenderer particleSystemRenderer in this.bodyParticleRenderers)
					{
						if (!(particleSystemRenderer == null))
						{
							flag |= particleSystemRenderer.enabled;
						}
					}
				}
				return flag;
			}
			set
			{
				this.IsNormalBodyEnabled = value;
				this.RefreshBodyEnabled();
			}
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x002541B8 File Offset: 0x002525B8
		private void RefreshBodyEnabled()
		{
			bool flag = this.IsNormalBodyEnabled && this.IsForcedBodyEnabled;
			if (!this.bodyRenderers.IsNullOrEmpty<Renderer>())
			{
				foreach (Renderer renderer in this.bodyRenderers)
				{
					if (renderer != null && renderer.enabled != flag)
					{
						renderer.enabled = flag;
					}
				}
			}
			if (!this.bodyParticleRenderers.IsNullOrEmpty<ParticleSystemRenderer>())
			{
				foreach (ParticleSystemRenderer particleSystemRenderer in this.bodyParticleRenderers)
				{
					if (particleSystemRenderer != null && particleSystemRenderer.enabled != flag)
					{
						particleSystemRenderer.enabled = flag;
					}
				}
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x00254284 File Offset: 0x00252684
		// (set) Token: 0x060054EC RID: 21740 RVA: 0x00254334 File Offset: 0x00252734
		public bool BodyActive
		{
			get
			{
				bool flag = true;
				if (!this.bodyRendererObjects.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject in this.bodyRendererObjects)
					{
						if (!(gameObject == null))
						{
							flag &= gameObject.activeSelf;
						}
					}
				}
				if (!this.bodyParticleObjects.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject2 in this.bodyParticleObjects)
					{
						if (!(gameObject2 == null))
						{
							flag &= gameObject2.activeSelf;
						}
					}
				}
				return flag;
			}
			set
			{
				if (!this.bodyRendererObjects.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject in this.bodyRendererObjects)
					{
						if (gameObject != null && gameObject.activeSelf != value)
						{
							gameObject.SetActive(value);
						}
					}
				}
				if (!this.bodyParticleObjects.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject2 in this.bodyParticleObjects)
					{
						if (gameObject2 != null && gameObject2.activeSelf != value)
						{
							gameObject2.SetActive(value);
						}
					}
				}
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x002543EB File Offset: 0x002527EB
		// (set) Token: 0x060054EE RID: 21742 RVA: 0x0025440F File Offset: 0x0025280F
		public bool MarkerEnabled
		{
			get
			{
				return !(this._marker == null) && this._marker.enabled;
			}
			set
			{
				if (this._marker != null && this._marker.enabled != value)
				{
					this._marker.enabled = value;
				}
			}
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0025443F File Offset: 0x0025283F
		public void AttachMarker()
		{
			this._marker = base.gameObject.GetOrAddComponent<AnimalMarker>();
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x00254452 File Offset: 0x00252852
		public void DetachMarker()
		{
			this._marker = base.GetComponentInChildren<AnimalMarker>(true);
			if (this._marker != null)
			{
				UnityEngine.Object.Destroy(this._marker);
				this._marker = null;
			}
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x00254484 File Offset: 0x00252884
		public virtual void DesireMax(DesireType _desireType)
		{
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060054F2 RID: 21746 RVA: 0x00254486 File Offset: 0x00252886
		// (set) Token: 0x060054F3 RID: 21747 RVA: 0x0025448E File Offset: 0x0025288E
		public LabelTypes PrevLabelType { get; private set; }

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x00254497 File Offset: 0x00252897
		// (set) Token: 0x060054F5 RID: 21749 RVA: 0x0025449F File Offset: 0x0025289F
		public LabelTypes LabelType
		{
			get
			{
				return this._labelType;
			}
			set
			{
				if (this._labelType != value)
				{
					this.PrevLabelType = this._labelType;
					this._labelType = value;
				}
				this.RefreshCommands(true);
			}
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x002544C8 File Offset: 0x002528C8
		public virtual bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (!this._isCommandable)
			{
				return false;
			}
			if (this.BadMood)
			{
				return false;
			}
			PlayerActor player = Manager.Map.GetPlayer();
			if (player != null && player.Mode == Desire.ActionType.Onbu)
			{
				return false;
			}
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = Vector3.Distance(basePosition, commandCenter);
			CommandType commandType = this._commandType;
			if (commandType != CommandType.Forward)
			{
				return distance <= radiusB;
			}
			if (radiusA < num)
			{
				return false;
			}
			Vector3 a = commandCenter;
			a.y = 0f;
			float num2 = angle / 2f;
			float num3 = Vector3.Angle(a - basePosition, forward);
			return num3 <= num2;
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0025458C File Offset: 0x0025298C
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this.pathForCalc == null)
			{
				this.pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				nmAgent.CalculatePath(this.Position, this.pathForCalc);
				flag &= (this.pathForCalc.status == NavMeshPathStatus.PathComplete);
				float num = 0f;
				Vector3[] corners = this.pathForCalc.corners;
				for (int i = 0; i < corners.Length - 1; i++)
				{
					float num2 = Vector3.Distance(corners[i], corners[i + 1]);
					num += num2;
					float num3 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
					if (num > num3)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x00254659 File Offset: 0x00252A59
		// (set) Token: 0x060054F9 RID: 21753 RVA: 0x00254661 File Offset: 0x00252A61
		public bool IsImpossible { get; protected set; }

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x0025466A File Offset: 0x00252A6A
		// (set) Token: 0x060054FB RID: 21755 RVA: 0x00254672 File Offset: 0x00252A72
		public Actor CommandPartner { get; set; }

		// Token: 0x060054FC RID: 21756 RVA: 0x0025467C File Offset: 0x00252A7C
		public virtual bool SetImpossible(bool _value, Actor _actor)
		{
			if (this.IsImpossible != _value)
			{
				if (_value)
				{
					if (this.CommandPartner != null && this.CommandPartner != _actor)
					{
						return false;
					}
				}
				else if (this.CommandPartner != _actor)
				{
					return false;
				}
				this.IsImpossible = _value;
				this.CommandPartner = ((!_value) ? null : _actor);
				this.RefreshCommands(true);
				return true;
			}
			return false;
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x002546FB File Offset: 0x00252AFB
		public virtual bool IsNeutralCommand
		{
			get
			{
				return this._isCommandable;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x00254703 File Offset: 0x00252B03
		public virtual CommandLabel.CommandInfo[] Labels
		{
			get
			{
				return AnimalBase.emptyLabels;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060054FF RID: 21759 RVA: 0x0025470A File Offset: 0x00252B0A
		public virtual CommandLabel.CommandInfo[] DateLabels
		{
			get
			{
				return AnimalBase.emptyLabels;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x00254711 File Offset: 0x00252B11
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return this._layer;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005501 RID: 21761 RVA: 0x00254719 File Offset: 0x00252B19
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return this._commandType;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x00254721 File Offset: 0x00252B21
		public virtual Vector3 CommandCenter
		{
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0025472E File Offset: 0x00252B2E
		protected virtual void InitializeCommandLabels()
		{
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x00254730 File Offset: 0x00252B30
		public AnimalSearchActor SearchActor
		{
			[CompilerGenerated]
			get
			{
				return this._searchActor;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x00254738 File Offset: 0x00252B38
		public bool AgentInsight
		{
			[CompilerGenerated]
			get
			{
				return this._agentInsight;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x00254740 File Offset: 0x00252B40
		public bool OnGroundCheck
		{
			[CompilerGenerated]
			get
			{
				return this._onGroundCheck;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x00254748 File Offset: 0x00252B48
		public float LabelOffsetY
		{
			[CompilerGenerated]
			get
			{
				return this._labelOffsetY;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x00254750 File Offset: 0x00252B50
		public bool IsCommandable
		{
			[CompilerGenerated]
			get
			{
				return this._isCommandable;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005509 RID: 21769 RVA: 0x00254758 File Offset: 0x00252B58
		public Transform LabelPoint
		{
			[CompilerGenerated]
			get
			{
				return (!(this._labelPoint != null)) ? base.transform : this._labelPoint;
			}
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0025477C File Offset: 0x00252B7C
		private void InitializeActorHitEvent()
		{
			if (this.SearchActor == null)
			{
				return;
			}
			this.SearchActor.OnFarPlayerActorEnterEvent = delegate(PlayerActor x)
			{
				this.OnFarPlayerActorEnter(x);
			};
			this.SearchActor.OnFarPlayerActorStayEvent = delegate(PlayerActor x)
			{
				this.OnFarPlayerActorStay(x);
			};
			this.SearchActor.OnFarPlayerActorExitEvent = delegate(PlayerActor x)
			{
				this.OnFarPlayerActorExit(x);
			};
			this.SearchActor.OnFarAgentActorEnterEvent = delegate(AgentActor x)
			{
				this.OnFarAgentActorEnter(x);
			};
			this.SearchActor.OnFarAgentActorStayEvent = delegate(AgentActor x)
			{
				this.OnFarAgentActorStay(x);
			};
			this.SearchActor.OnFarAgentActorExitEvent = delegate(AgentActor x)
			{
				this.OnFarAgentActorExit(x);
			};
			this.SearchActor.OnFarActorEnterEvent = delegate(Actor x)
			{
				this.OnFarActorEnter(x);
			};
			this.SearchActor.OnFarActorStayEvent = delegate(Actor x)
			{
				this.OnFarActorStay(x);
			};
			this.SearchActor.OnFarActorExitEvent = delegate(Actor x)
			{
				this.OnFarActorExit(x);
			};
			this.SearchActor.OnNearPlayerActorEnterEvent = delegate(PlayerActor x)
			{
				this.OnNearPlayerActorEnter(x);
			};
			this.SearchActor.OnNearPlayerActorStayEvent = delegate(PlayerActor x)
			{
				this.OnNearPlayerActorStay(x);
			};
			this.SearchActor.OnNearPlayerActorExitEvent = delegate(PlayerActor x)
			{
				this.OnNearPlayerActorExit(x);
			};
			this.SearchActor.OnNearAgentActorEnterEvent = delegate(AgentActor x)
			{
				this.OnNearAgentActorEnter(x);
			};
			this.SearchActor.OnNearAgentActorStayEvent = delegate(AgentActor x)
			{
				this.OnNearAgentActorStay(x);
			};
			this.SearchActor.OnNearAgentActorExitEvent = delegate(AgentActor x)
			{
				this.OnNearAgentActorExit(x);
			};
			this.SearchActor.OnNearActorEnterEvent = delegate(Actor x)
			{
				this.OnNearActorEnter(x);
			};
			this.SearchActor.OnNearActorStayEvent = delegate(Actor x)
			{
				this.OnNearActorStay(x);
			};
			this.SearchActor.OnNearActorExitEvent = delegate(Actor x)
			{
				this.OnNearActorExit(x);
			};
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x00254939 File Offset: 0x00252D39
		protected virtual void OnFarPlayerActorEnter(PlayerActor _player)
		{
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0025493B File Offset: 0x00252D3B
		protected virtual void OnFarPlayerActorStay(PlayerActor _player)
		{
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x0025493D File Offset: 0x00252D3D
		protected virtual void OnFarPlayerActorExit(PlayerActor _player)
		{
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0025493F File Offset: 0x00252D3F
		protected virtual void OnFarAgentActorEnter(AgentActor _agent)
		{
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00254941 File Offset: 0x00252D41
		protected virtual void OnFarAgentActorStay(AgentActor _agent)
		{
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00254943 File Offset: 0x00252D43
		protected virtual void OnFarAgentActorExit(AgentActor _agent)
		{
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x00254945 File Offset: 0x00252D45
		protected virtual void OnFarActorEnter(Actor _actor)
		{
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00254947 File Offset: 0x00252D47
		protected virtual void OnFarActorStay(Actor _actor)
		{
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x00254949 File Offset: 0x00252D49
		protected virtual void OnFarActorExit(Actor _actor)
		{
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0025494B File Offset: 0x00252D4B
		protected virtual void OnNearPlayerActorEnter(PlayerActor _player)
		{
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0025494D File Offset: 0x00252D4D
		protected virtual void OnNearPlayerActorStay(PlayerActor _player)
		{
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0025494F File Offset: 0x00252D4F
		protected virtual void OnNearPlayerActorExit(PlayerActor _player)
		{
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x00254951 File Offset: 0x00252D51
		protected virtual void OnNearAgentActorEnter(AgentActor _agent)
		{
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00254953 File Offset: 0x00252D53
		protected virtual void OnNearAgentActorStay(AgentActor _agent)
		{
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x00254955 File Offset: 0x00252D55
		protected virtual void OnNearAgentActorExit(AgentActor _agent)
		{
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x00254957 File Offset: 0x00252D57
		protected virtual void OnNearActorEnter(Actor _actor)
		{
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x00254959 File Offset: 0x00252D59
		protected virtual void OnNearActorStay(Actor _actor)
		{
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0025495B File Offset: 0x00252D5B
		protected virtual void OnNearActorExit(Actor _actor)
		{
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00254960 File Offset: 0x00252D60
		protected virtual void Awake()
		{
			this.IdentifierName = this.AnimalType.ToString();
			if (this._animalTypeID < 0)
			{
				this._animalTypeID = AnimalData.GetAnimalTypeID(this._animalType);
			}
			else if (this._animalType != (AnimalTypes)(1 << this._animalTypeID))
			{
				this._animalType = (AnimalTypes)(1 << this._animalTypeID);
			}
			this._marker = base.GetComponentInChildren<AnimalMarker>(true);
			this.MarkerEnabled = false;
			this.InitializeCommandLabels();
			this.InitializeActorHitEvent();
			StuffItemInfo itemInfo = this.ItemInfo;
			if (itemInfo != null)
			{
				this._name = itemInfo.Name;
			}
			EnvironmentSimulator environmentSimulator = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Simulator;
			this.timeZone = ((!(environmentSimulator != null)) ? TimeZone.Morning : environmentSimulator.TimeZone);
			this.weather = ((!(environmentSimulator != null)) ? Weather.Clear : environmentSimulator.Weather);
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x00254A60 File Offset: 0x00252E60
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x00254A62 File Offset: 0x00252E62
		protected virtual void Start()
		{
			this.SetUpdateObservable();
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00254A6C File Offset: 0x00252E6C
		private void SetUpdateObservable()
		{
			if (this.everyUpdateDisposable != null)
			{
				this.everyUpdateDisposable.Dispose();
			}
			if (this.everyLateUpdateDisposable != null)
			{
				this.everyLateUpdateDisposable.Dispose();
			}
			if (this.everyFixedUpdateDisposable != null)
			{
				this.everyFixedUpdateDisposable.Dispose();
			}
			this.everyUpdateDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdateFirst();
				this.OnUpdate();
				this.OnUpdateEnd();
			});
			this.everyLateUpdateDisposable = (from _ in Observable.EveryLateUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdateFirst();
				this.OnLateUpdate();
				this.OnLateUpdateEnd();
			});
			this.everyFixedUpdateDisposable = (from _ in Observable.EveryFixedUpdate().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnFixedUpdateFirst();
				this.OnFixedUpdate();
				this.OnFixedUpdateEnd();
			});
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x00254B5A File Offset: 0x00252F5A
		protected virtual void OnDisable()
		{
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x00254B5C File Offset: 0x00252F5C
		protected virtual void OnUpdate()
		{
			if (this.StateUpdatePossible)
			{
				this.StateUpdate();
			}
			if (this.AnimatorControllerEnabled)
			{
				this.StateAnimationUpdate();
			}
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00254B80 File Offset: 0x00252F80
		protected virtual void OnUpdateFirst()
		{
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x00254B82 File Offset: 0x00252F82
		protected virtual void OnUpdateEnd()
		{
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x00254B84 File Offset: 0x00252F84
		protected virtual void OnLateUpdateFirst()
		{
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x00254B86 File Offset: 0x00252F86
		protected virtual void OnLateUpdate()
		{
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x00254B88 File Offset: 0x00252F88
		protected virtual void OnLateUpdateEnd()
		{
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x00254B8A File Offset: 0x00252F8A
		protected virtual void OnFixedUpdateFirst()
		{
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x00254B8C File Offset: 0x00252F8C
		protected virtual void OnFixedUpdate()
		{
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00254B8E File Offset: 0x00252F8E
		protected virtual void OnFixedUpdateEnd()
		{
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x00254B90 File Offset: 0x00252F90
		protected virtual void ChangedStateEvent()
		{
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00254B92 File Offset: 0x00252F92
		private void StateChanged()
		{
			this.StateExit();
			if (this.CurrentState != AnimalState.Destroyed)
			{
				this.ChangedStateEvent();
			}
			this.StateEnter();
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x00254BB4 File Offset: 0x00252FB4
		private void StateEnter()
		{
			AnimalState animalState = this.currentState_;
			switch (animalState)
			{
			case AnimalState.Start:
				this.EnterStart();
				break;
			case AnimalState.Repop:
				this.EnterRepop();
				break;
			case AnimalState.Depop:
				this.EnterDepop();
				break;
			case AnimalState.Idle:
				this.EnterIdle();
				break;
			case AnimalState.Wait:
				this.EnterWait();
				break;
			case AnimalState.SitWait:
				this.EnterSitWait();
				break;
			case AnimalState.Locomotion:
				this.EnterLocomotion();
				break;
			case AnimalState.LovelyIdle:
				this.EnterLovelyIdle();
				break;
			case AnimalState.LovelyFollow:
				this.EnterLovelyFollow();
				break;
			case AnimalState.Escape:
				this.EnterEscape();
				break;
			case AnimalState.Swim:
				this.EnterSwim();
				break;
			case AnimalState.Sleep:
				this.EnterSleep();
				break;
			case AnimalState.Toilet:
				this.EnterToilet();
				break;
			case AnimalState.Rest:
				this.EnterRest();
				break;
			case AnimalState.Eat:
				this.EnterEat();
				break;
			case AnimalState.Drink:
				this.EnterDrink();
				break;
			case AnimalState.Actinidia:
				this.EnterActinidia();
				break;
			case AnimalState.Grooming:
				this.EnterGrooming();
				break;
			case AnimalState.MoveEars:
				this.EnterMoveEars();
				break;
			case AnimalState.Roar:
				this.EnterRoar();
				break;
			case AnimalState.Peck:
				this.EnterPeck();
				break;
			case AnimalState.ToDepop:
				this.EnterToDepop();
				break;
			case AnimalState.ToIndoor:
				this.EnterToIndoor();
				break;
			default:
				switch (animalState)
				{
				case AnimalState.Action0:
					this.EnterAction0();
					break;
				case AnimalState.Action1:
					this.EnterAction1();
					break;
				case AnimalState.Action2:
					this.EnterAction2();
					break;
				case AnimalState.Action3:
					this.EnterAction3();
					break;
				case AnimalState.Action4:
					this.EnterAction4();
					break;
				case AnimalState.Action5:
					this.EnterAction5();
					break;
				case AnimalState.Action6:
					this.EnterAction6();
					break;
				case AnimalState.Action7:
					this.EnterAction7();
					break;
				case AnimalState.Action8:
					this.EnterAction8();
					break;
				case AnimalState.Action9:
					this.EnterAction9();
					break;
				case AnimalState.WithPlayer:
					this.EnterWithPlayer();
					break;
				case AnimalState.WithAgent:
					this.EnterWithAgent();
					break;
				case AnimalState.WithMerchant:
					this.EnterWithMerchant();
					break;
				}
				break;
			}
			if (this.AutoChangeAnimation)
			{
			}
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00254E08 File Offset: 0x00253208
		private void StateUpdate()
		{
			if (!this.EnabledStateUpdate)
			{
				return;
			}
			AnimalState animalState = this.currentState_;
			switch (animalState)
			{
			case AnimalState.Start:
				this.OnStart();
				break;
			case AnimalState.Repop:
				this.OnRepop();
				break;
			case AnimalState.Depop:
				this.OnDepop();
				break;
			case AnimalState.Idle:
				this.OnIdle();
				break;
			case AnimalState.Wait:
				this.OnWait();
				break;
			case AnimalState.SitWait:
				this.OnSitWait();
				break;
			case AnimalState.Locomotion:
				this.OnLocomotion();
				break;
			case AnimalState.LovelyIdle:
				this.OnLovelyIdle();
				break;
			case AnimalState.LovelyFollow:
				this.OnLovelyFollow();
				break;
			case AnimalState.Escape:
				this.OnEscape();
				break;
			case AnimalState.Swim:
				this.OnSwim();
				break;
			case AnimalState.Sleep:
				this.OnSleep();
				break;
			case AnimalState.Toilet:
				this.OnToilet();
				break;
			case AnimalState.Rest:
				this.OnRest();
				break;
			case AnimalState.Eat:
				this.OnEat();
				break;
			case AnimalState.Drink:
				this.OnDrink();
				break;
			case AnimalState.Actinidia:
				this.OnActinidia();
				break;
			case AnimalState.Grooming:
				this.OnGrooming();
				break;
			case AnimalState.MoveEars:
				this.OnMoveEars();
				break;
			case AnimalState.Roar:
				this.OnRoar();
				break;
			case AnimalState.Peck:
				this.OnPeck();
				break;
			case AnimalState.ToDepop:
				this.OnToDepop();
				break;
			case AnimalState.ToIndoor:
				this.OnToIndoor();
				break;
			default:
				switch (animalState)
				{
				case AnimalState.Action0:
					this.OnAction0();
					break;
				case AnimalState.Action1:
					this.OnAction1();
					break;
				case AnimalState.Action2:
					this.OnAction2();
					break;
				case AnimalState.Action3:
					this.OnAction3();
					break;
				case AnimalState.Action4:
					this.OnAction4();
					break;
				case AnimalState.Action5:
					this.OnAction5();
					break;
				case AnimalState.Action6:
					this.OnAction6();
					break;
				case AnimalState.Action7:
					this.OnAction7();
					break;
				case AnimalState.Action8:
					this.OnAction8();
					break;
				case AnimalState.Action9:
					this.OnAction9();
					break;
				case AnimalState.WithPlayer:
					this.OnWithPlayer();
					break;
				case AnimalState.WithAgent:
					this.OnWithAgent();
					break;
				case AnimalState.WithMerchant:
					this.OnWithMerchant();
					break;
				}
				break;
			}
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0025505C File Offset: 0x0025345C
		private void StateExit()
		{
			AnimalState prevState = this.PrevState;
			switch (prevState)
			{
			case AnimalState.Start:
				this.ExitStart();
				break;
			case AnimalState.Repop:
				this.ExitRepop();
				break;
			case AnimalState.Depop:
				this.ExitDepop();
				break;
			case AnimalState.Idle:
				this.ExitIdle();
				break;
			case AnimalState.Wait:
				this.ExitWait();
				break;
			case AnimalState.SitWait:
				this.ExitSitWait();
				break;
			case AnimalState.Locomotion:
				this.ExitLocomotion();
				break;
			case AnimalState.LovelyIdle:
				this.ExitLovelyIdle();
				break;
			case AnimalState.LovelyFollow:
				this.ExitLovelyFollow();
				break;
			case AnimalState.Escape:
				this.ExitEscape();
				break;
			case AnimalState.Swim:
				this.ExitSwim();
				break;
			case AnimalState.Sleep:
				this.ExitSleep();
				break;
			case AnimalState.Toilet:
				this.ExitToilet();
				break;
			case AnimalState.Rest:
				this.ExitRest();
				break;
			case AnimalState.Eat:
				this.ExitEat();
				break;
			case AnimalState.Drink:
				this.ExitDrink();
				break;
			case AnimalState.Actinidia:
				this.ExitActinidia();
				break;
			case AnimalState.Grooming:
				this.ExitGrooming();
				break;
			case AnimalState.MoveEars:
				this.ExitMoveEars();
				break;
			case AnimalState.Roar:
				this.ExitRoar();
				break;
			case AnimalState.Peck:
				this.ExitPeck();
				break;
			case AnimalState.ToDepop:
				this.ExitToDepop();
				break;
			case AnimalState.ToIndoor:
				this.ExitToIndoor();
				break;
			default:
				switch (prevState)
				{
				case AnimalState.Action0:
					this.ExitAction0();
					break;
				case AnimalState.Action1:
					this.ExitAction1();
					break;
				case AnimalState.Action2:
					this.ExitAction2();
					break;
				case AnimalState.Action3:
					this.ExitAction3();
					break;
				case AnimalState.Action4:
					this.ExitAction4();
					break;
				case AnimalState.Action5:
					this.ExitAction5();
					break;
				case AnimalState.Action6:
					this.ExitAction6();
					break;
				case AnimalState.Action7:
					this.ExitAction7();
					break;
				case AnimalState.Action8:
					this.ExitAction8();
					break;
				case AnimalState.Action9:
					this.ExitAction9();
					break;
				case AnimalState.WithPlayer:
					this.ExitWithPlayer();
					break;
				case AnimalState.WithAgent:
					this.ExitWithAgent();
					break;
				case AnimalState.WithMerchant:
					this.ExitWithMerchant();
					break;
				}
				break;
			}
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x002552A4 File Offset: 0x002536A4
		private void StateAnimationUpdate()
		{
			AnimalState animalState = this.currentState_;
			switch (animalState)
			{
			case AnimalState.Start:
				this.AnimationStart();
				break;
			case AnimalState.Repop:
				this.AnimationRepop();
				break;
			case AnimalState.Depop:
				this.AnimationDepop();
				break;
			case AnimalState.Idle:
				this.AnimationIdle();
				break;
			case AnimalState.Wait:
				this.AnimationWait();
				break;
			case AnimalState.SitWait:
				this.AnimationSitWait();
				break;
			case AnimalState.Locomotion:
				this.AnimationLocomotion();
				break;
			case AnimalState.LovelyIdle:
				this.AnimationLovelyIdle();
				break;
			case AnimalState.LovelyFollow:
				this.AnimationLovelyFollow();
				break;
			case AnimalState.Escape:
				this.AnimationEscape();
				break;
			case AnimalState.Swim:
				this.AnimationSwim();
				break;
			case AnimalState.Sleep:
				this.AnimationSleep();
				break;
			case AnimalState.Toilet:
				this.AnimationToilet();
				break;
			case AnimalState.Rest:
				this.AnimationRest();
				break;
			case AnimalState.Eat:
				this.AnimationEat();
				break;
			case AnimalState.Drink:
				this.AnimationDrink();
				break;
			case AnimalState.Actinidia:
				this.AnimationActinidia();
				break;
			case AnimalState.Grooming:
				this.AnimationGrooming();
				break;
			case AnimalState.MoveEars:
				this.AnimationMoveEars();
				break;
			case AnimalState.Roar:
				this.AnimationRoar();
				break;
			case AnimalState.Peck:
				this.AnimationPeck();
				break;
			case AnimalState.ToDepop:
				this.AnimationToDepop();
				break;
			case AnimalState.ToIndoor:
				this.AnimationToIndoor();
				break;
			default:
				switch (animalState)
				{
				case AnimalState.Action0:
					this.AnimationAction0();
					break;
				case AnimalState.Action1:
					this.AnimationAction1();
					break;
				case AnimalState.Action2:
					this.AnimationAction2();
					break;
				case AnimalState.Action3:
					this.AnimationAction3();
					break;
				case AnimalState.Action4:
					this.AnimationAction4();
					break;
				case AnimalState.Action5:
					this.AnimationAction5();
					break;
				case AnimalState.Action6:
					this.AnimationAction6();
					break;
				case AnimalState.Action7:
					this.AnimationAction7();
					break;
				case AnimalState.Action8:
					this.AnimationAction8();
					break;
				case AnimalState.Action9:
					this.AnimationAction9();
					break;
				case AnimalState.WithPlayer:
					this.AnimationWithPlayer();
					break;
				case AnimalState.WithAgent:
					this.AnimationWithAgent();
					break;
				case AnimalState.WithMerchant:
					this.AnimationWithMerchant();
					break;
				}
				break;
			}
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x002554EC File Offset: 0x002538EC
		protected virtual IEnumerator PrepareStart()
		{
			yield break;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00255500 File Offset: 0x00253900
		protected virtual void EnterStart()
		{
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00255502 File Offset: 0x00253902
		protected virtual void EnterRepop()
		{
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x00255504 File Offset: 0x00253904
		protected virtual void EnterDepop()
		{
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00255506 File Offset: 0x00253906
		protected virtual void EnterIdle()
		{
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00255508 File Offset: 0x00253908
		protected virtual void EnterWait()
		{
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0025550A File Offset: 0x0025390A
		protected virtual void EnterSitWait()
		{
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0025550C File Offset: 0x0025390C
		protected virtual void EnterLocomotion()
		{
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0025550E File Offset: 0x0025390E
		protected virtual void EnterLovelyIdle()
		{
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00255510 File Offset: 0x00253910
		protected virtual void EnterLovelyFollow()
		{
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x00255512 File Offset: 0x00253912
		protected virtual void EnterEscape()
		{
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00255514 File Offset: 0x00253914
		protected virtual void EnterSwim()
		{
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00255516 File Offset: 0x00253916
		protected virtual void EnterSleep()
		{
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00255518 File Offset: 0x00253918
		protected virtual void EnterToilet()
		{
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0025551A File Offset: 0x0025391A
		protected virtual void EnterRest()
		{
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0025551C File Offset: 0x0025391C
		protected virtual void EnterEat()
		{
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x0025551E File Offset: 0x0025391E
		protected virtual void EnterDrink()
		{
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00255520 File Offset: 0x00253920
		protected virtual void EnterActinidia()
		{
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x00255522 File Offset: 0x00253922
		protected virtual void EnterGrooming()
		{
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00255524 File Offset: 0x00253924
		protected virtual void EnterMoveEars()
		{
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x00255526 File Offset: 0x00253926
		protected virtual void EnterRoar()
		{
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00255528 File Offset: 0x00253928
		protected virtual void EnterPeck()
		{
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x0025552A File Offset: 0x0025392A
		protected virtual void EnterToDepop()
		{
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0025552C File Offset: 0x0025392C
		protected virtual void EnterToIndoor()
		{
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0025552E File Offset: 0x0025392E
		protected virtual void EnterAction0()
		{
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00255530 File Offset: 0x00253930
		protected virtual void EnterAction1()
		{
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x00255532 File Offset: 0x00253932
		protected virtual void EnterAction2()
		{
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00255534 File Offset: 0x00253934
		protected virtual void EnterAction3()
		{
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x00255536 File Offset: 0x00253936
		protected virtual void EnterAction4()
		{
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x00255538 File Offset: 0x00253938
		protected virtual void EnterAction5()
		{
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0025553A File Offset: 0x0025393A
		protected virtual void EnterAction6()
		{
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0025553C File Offset: 0x0025393C
		protected virtual void EnterAction7()
		{
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0025553E File Offset: 0x0025393E
		protected virtual void EnterAction8()
		{
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x00255540 File Offset: 0x00253940
		protected virtual void EnterAction9()
		{
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x00255542 File Offset: 0x00253942
		protected virtual void EnterWithPlayer()
		{
			this.AutoChangeAnimation = false;
			this.AnimationEndUpdate = false;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x00255552 File Offset: 0x00253952
		protected virtual void EnterWithAgent()
		{
			this.AutoChangeAnimation = false;
			this.AnimationEndUpdate = false;
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x00255562 File Offset: 0x00253962
		protected virtual void EnterWithMerchant()
		{
			this.AutoChangeAnimation = false;
			this.AnimationEndUpdate = false;
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x00255572 File Offset: 0x00253972
		protected virtual void OnStart()
		{
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x00255574 File Offset: 0x00253974
		protected virtual void OnRepop()
		{
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x00255576 File Offset: 0x00253976
		protected virtual void OnDepop()
		{
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00255578 File Offset: 0x00253978
		protected virtual void OnIdle()
		{
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0025557A File Offset: 0x0025397A
		protected virtual void OnWait()
		{
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0025557C File Offset: 0x0025397C
		protected virtual void OnSitWait()
		{
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x0025557E File Offset: 0x0025397E
		protected virtual void OnLocomotion()
		{
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x00255580 File Offset: 0x00253980
		protected virtual void OnLovelyIdle()
		{
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x00255582 File Offset: 0x00253982
		protected virtual void OnLovelyFollow()
		{
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00255584 File Offset: 0x00253984
		protected virtual void OnEscape()
		{
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x00255586 File Offset: 0x00253986
		protected virtual void OnSwim()
		{
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x00255588 File Offset: 0x00253988
		protected virtual void OnSleep()
		{
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x0025558A File Offset: 0x0025398A
		protected virtual void OnToilet()
		{
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0025558C File Offset: 0x0025398C
		protected virtual void OnRest()
		{
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0025558E File Offset: 0x0025398E
		protected virtual void OnEat()
		{
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x00255590 File Offset: 0x00253990
		protected virtual void OnDrink()
		{
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x00255592 File Offset: 0x00253992
		protected virtual void OnActinidia()
		{
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x00255594 File Offset: 0x00253994
		protected virtual void OnGrooming()
		{
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x00255596 File Offset: 0x00253996
		protected virtual void OnMoveEars()
		{
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x00255598 File Offset: 0x00253998
		protected virtual void OnRoar()
		{
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x0025559A File Offset: 0x0025399A
		protected virtual void OnPeck()
		{
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x0025559C File Offset: 0x0025399C
		protected virtual void OnToDepop()
		{
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x0025559E File Offset: 0x0025399E
		protected virtual void OnToIndoor()
		{
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x002555A0 File Offset: 0x002539A0
		protected virtual void OnAction0()
		{
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x002555A2 File Offset: 0x002539A2
		protected virtual void OnAction1()
		{
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x002555A4 File Offset: 0x002539A4
		protected virtual void OnAction2()
		{
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x002555A6 File Offset: 0x002539A6
		protected virtual void OnAction3()
		{
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x002555A8 File Offset: 0x002539A8
		protected virtual void OnAction4()
		{
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x002555AA File Offset: 0x002539AA
		protected virtual void OnAction5()
		{
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x002555AC File Offset: 0x002539AC
		protected virtual void OnAction6()
		{
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x002555AE File Offset: 0x002539AE
		protected virtual void OnAction7()
		{
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x002555B0 File Offset: 0x002539B0
		protected virtual void OnAction8()
		{
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x002555B2 File Offset: 0x002539B2
		protected virtual void OnAction9()
		{
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x002555B4 File Offset: 0x002539B4
		protected virtual void OnWithPlayer()
		{
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x002555B6 File Offset: 0x002539B6
		protected virtual void OnWithAgent()
		{
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x002555B8 File Offset: 0x002539B8
		protected virtual void OnWithMerchant()
		{
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x002555BA File Offset: 0x002539BA
		protected virtual void ExitStart()
		{
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x002555BC File Offset: 0x002539BC
		protected virtual void ExitRepop()
		{
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x002555BE File Offset: 0x002539BE
		protected virtual void ExitDepop()
		{
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x002555C0 File Offset: 0x002539C0
		protected virtual void ExitIdle()
		{
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x002555C2 File Offset: 0x002539C2
		protected virtual void ExitWait()
		{
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x002555C4 File Offset: 0x002539C4
		protected virtual void ExitSitWait()
		{
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x002555C6 File Offset: 0x002539C6
		protected virtual void ExitLocomotion()
		{
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x002555C8 File Offset: 0x002539C8
		protected virtual void ExitLovelyIdle()
		{
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x002555CA File Offset: 0x002539CA
		protected virtual void ExitLovelyFollow()
		{
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x002555CC File Offset: 0x002539CC
		protected virtual void ExitEscape()
		{
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x002555CE File Offset: 0x002539CE
		protected virtual void ExitSwim()
		{
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x002555D0 File Offset: 0x002539D0
		protected virtual void ExitSleep()
		{
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x002555D2 File Offset: 0x002539D2
		protected virtual void ExitToilet()
		{
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x002555D4 File Offset: 0x002539D4
		protected virtual void ExitRest()
		{
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x002555D6 File Offset: 0x002539D6
		protected virtual void ExitEat()
		{
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x002555D8 File Offset: 0x002539D8
		protected virtual void ExitDrink()
		{
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x002555DA File Offset: 0x002539DA
		protected virtual void ExitActinidia()
		{
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x002555DC File Offset: 0x002539DC
		protected virtual void ExitGrooming()
		{
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x002555DE File Offset: 0x002539DE
		protected virtual void ExitMoveEars()
		{
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x002555E0 File Offset: 0x002539E0
		protected virtual void ExitRoar()
		{
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x002555E2 File Offset: 0x002539E2
		protected virtual void ExitPeck()
		{
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x002555E4 File Offset: 0x002539E4
		protected virtual void ExitToDepop()
		{
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x002555E6 File Offset: 0x002539E6
		protected virtual void ExitToIndoor()
		{
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x002555E8 File Offset: 0x002539E8
		protected virtual void ExitAction0()
		{
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x002555EA File Offset: 0x002539EA
		protected virtual void ExitAction1()
		{
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x002555EC File Offset: 0x002539EC
		protected virtual void ExitAction2()
		{
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x002555EE File Offset: 0x002539EE
		protected virtual void ExitAction3()
		{
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x002555F0 File Offset: 0x002539F0
		protected virtual void ExitAction4()
		{
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x002555F2 File Offset: 0x002539F2
		protected virtual void ExitAction5()
		{
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x002555F4 File Offset: 0x002539F4
		protected virtual void ExitAction6()
		{
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x002555F6 File Offset: 0x002539F6
		protected virtual void ExitAction7()
		{
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x002555F8 File Offset: 0x002539F8
		protected virtual void ExitAction8()
		{
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x002555FA File Offset: 0x002539FA
		protected virtual void ExitAction9()
		{
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x002555FC File Offset: 0x002539FC
		protected virtual void ExitWithPlayer()
		{
			this.AutoChangeAnimation = true;
			this.AnimationEndUpdate = true;
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x0025560C File Offset: 0x00253A0C
		protected virtual void ExitWithAgent()
		{
			this.AutoChangeAnimation = true;
			this.AnimationEndUpdate = true;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x0025561C File Offset: 0x00253A1C
		protected virtual void ExitWithMerchant()
		{
			this.AutoChangeAnimation = true;
			this.AnimationEndUpdate = true;
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x0025562C File Offset: 0x00253A2C
		protected virtual void AnimationStart()
		{
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x0025562E File Offset: 0x00253A2E
		protected virtual void AnimationRepop()
		{
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x00255630 File Offset: 0x00253A30
		protected virtual void AnimationDepop()
		{
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x00255632 File Offset: 0x00253A32
		protected virtual void AnimationIdle()
		{
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x00255634 File Offset: 0x00253A34
		protected virtual void AnimationWait()
		{
		}

		// Token: 0x060055A3 RID: 21923 RVA: 0x00255636 File Offset: 0x00253A36
		protected virtual void AnimationSitWait()
		{
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00255638 File Offset: 0x00253A38
		protected virtual void AnimationLocomotion()
		{
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0025563A File Offset: 0x00253A3A
		protected virtual void AnimationLovelyIdle()
		{
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0025563C File Offset: 0x00253A3C
		protected virtual void AnimationLovelyFollow()
		{
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x0025563E File Offset: 0x00253A3E
		protected virtual void AnimationEscape()
		{
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x00255640 File Offset: 0x00253A40
		protected virtual void AnimationSwim()
		{
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x00255642 File Offset: 0x00253A42
		protected virtual void AnimationSleep()
		{
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x00255644 File Offset: 0x00253A44
		protected virtual void AnimationToilet()
		{
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x00255646 File Offset: 0x00253A46
		protected virtual void AnimationRest()
		{
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00255648 File Offset: 0x00253A48
		protected virtual void AnimationEat()
		{
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x0025564A File Offset: 0x00253A4A
		protected virtual void AnimationDrink()
		{
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0025564C File Offset: 0x00253A4C
		protected virtual void AnimationActinidia()
		{
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0025564E File Offset: 0x00253A4E
		protected virtual void AnimationGrooming()
		{
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x00255650 File Offset: 0x00253A50
		protected virtual void AnimationMoveEars()
		{
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00255652 File Offset: 0x00253A52
		protected virtual void AnimationRoar()
		{
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00255654 File Offset: 0x00253A54
		protected virtual void AnimationPeck()
		{
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00255656 File Offset: 0x00253A56
		protected virtual void AnimationToDepop()
		{
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x00255658 File Offset: 0x00253A58
		protected virtual void AnimationToIndoor()
		{
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x0025565A File Offset: 0x00253A5A
		protected virtual void AnimationAction0()
		{
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x0025565C File Offset: 0x00253A5C
		protected virtual void AnimationAction1()
		{
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x0025565E File Offset: 0x00253A5E
		protected virtual void AnimationAction2()
		{
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x00255660 File Offset: 0x00253A60
		protected virtual void AnimationAction3()
		{
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x00255662 File Offset: 0x00253A62
		protected virtual void AnimationAction4()
		{
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00255664 File Offset: 0x00253A64
		protected virtual void AnimationAction5()
		{
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x00255666 File Offset: 0x00253A66
		protected virtual void AnimationAction6()
		{
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x00255668 File Offset: 0x00253A68
		protected virtual void AnimationAction7()
		{
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x0025566A File Offset: 0x00253A6A
		protected virtual void AnimationAction8()
		{
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x0025566C File Offset: 0x00253A6C
		protected virtual void AnimationAction9()
		{
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x0025566E File Offset: 0x00253A6E
		protected virtual void AnimationWithPlayer()
		{
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00255670 File Offset: 0x00253A70
		protected virtual void AnimationWithAgent()
		{
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00255672 File Offset: 0x00253A72
		protected virtual void AnimationWithMerchant()
		{
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x060055C2 RID: 21954 RVA: 0x00255674 File Offset: 0x00253A74
		public bool IsNight
		{
			[CompilerGenerated]
			get
			{
				return this.timeZone == TimeZone.Night;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x0025567F File Offset: 0x00253A7F
		public bool IsRain
		{
			[CompilerGenerated]
			get
			{
				return this.weather == Weather.Rain || this.weather == Weather.Storm;
			}
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00255699 File Offset: 0x00253A99
		public bool CheckNight(TimeZone _timeZone)
		{
			return _timeZone == TimeZone.Night;
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0025569F File Offset: 0x00253A9F
		public bool CheckRain(Weather _weather)
		{
			return _weather == Weather.Rain || _weather == Weather.Storm;
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060055C6 RID: 21958 RVA: 0x002556AF File Offset: 0x00253AAF
		public bool IndoorMode
		{
			[CompilerGenerated]
			get
			{
				return this.IsNight || this.IsRain;
			}
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x002556C5 File Offset: 0x00253AC5
		public virtual void OnSecondUpdate(TimeSpan _deltaTime)
		{
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x002556C7 File Offset: 0x00253AC7
		public virtual void OnMinuteUpdate(TimeSpan _deltaTime)
		{
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x002556C9 File Offset: 0x00253AC9
		public virtual void OnTimeZoneChanged(EnvironmentSimulator _simulator)
		{
			this.timeZone = _simulator.TimeZone;
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x002556D7 File Offset: 0x00253AD7
		public virtual void OnEnvironmentChanged(EnvironmentSimulator _simulator)
		{
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x002556D9 File Offset: 0x00253AD9
		public virtual void OnWeatherChanged(EnvironmentSimulator _simulator)
		{
			this.weather = _simulator.Weather;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x002556E8 File Offset: 0x00253AE8
		public virtual void Clear()
		{
			this.Active = false;
			this.SetLookPattern(0);
			this.ClearTargetObject();
			bool flag = false;
			this.BadMood = flag;
			flag = flag;
			this.WaitMode = flag;
			this.LookWaitMode = flag;
			this.destination = null;
			this.CurrentState = AnimalState.None;
			AnimalState animalState = AnimalState.None;
			this.BackupState = animalState;
			animalState = animalState;
			this.NextState = animalState;
			animalState = animalState;
			this.PrevState = animalState;
			this.currentState_ = animalState;
			flag = true;
			this.AnimationEndUpdate = flag;
			flag = flag;
			this.AutoChangeAnimation = flag;
			this.EnabledStateUpdate = flag;
			float num = 0f;
			this.StateTimeLimit = num;
			this.StateCounter = num;
			this.ReleaseActionPoint();
			this.StopPlayAnimChange();
			this.animator = null;
			this.AnimCommonTable = null;
			this.LookStateTable = null;
			this.ReleaseBody();
			this.InAnimStates.Clear();
			this.OutAnimStates.Clear();
			this.CommandPartner = null;
			this.IsImpossible = false;
			this.LabelType = LabelTypes.None;
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x002557D9 File Offset: 0x00253BD9
		public virtual void Destroy()
		{
			if (this.currentState_ != AnimalState.Destroyed)
			{
				this.currentState_ = AnimalState.Destroyed;
			}
			if (base.gameObject != null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x0025580C File Offset: 0x00253C0C
		protected virtual void OnDestroy()
		{
			this.Active = false;
			this.currentState_ = AnimalState.Destroyed;
			if (this.OnDestroyEvent != null)
			{
				this.OnDestroyEvent();
			}
			this.StopPlayAnimChange();
			if (this.everyUpdateDisposable != null)
			{
				this.everyUpdateDisposable.Dispose();
			}
			if (this.everyLateUpdateDisposable != null)
			{
				this.everyLateUpdateDisposable.Dispose();
			}
			if (this.everyFixedUpdateDisposable != null)
			{
				this.everyFixedUpdateDisposable.Dispose();
			}
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0025588E File Offset: 0x00253C8E
		public void SetID(int _animalID, int _chunkID)
		{
			this.AnimalID = _animalID;
			this.ChunkID = _chunkID;
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0025589E File Offset: 0x00253C9E
		public void SetAnimalName(string _name_)
		{
			this._name = _name_;
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x002558A7 File Offset: 0x00253CA7
		public AnimalInfo GetAnimalInfo()
		{
			return new AnimalInfo(this.AnimalType, this.BreedingType, this.Name, this.IdentifierName, this.AnimalID, this.ChunkID, this.ModelInfo);
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x002558D8 File Offset: 0x00253CD8
		public void SetModelInfo(AnimalModelInfo _modelInfo)
		{
			this.ModelInfo = _modelInfo;
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x002558E1 File Offset: 0x00253CE1
		protected void SetStateData()
		{
			this.SetAnimStateTable();
			this.SetLookStateTable();
			this.SetExpressionTable();
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x002558F8 File Offset: 0x00253CF8
		protected void SetAnimStateTable()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, Dictionary<int, Dictionary<int, AnimalPlayState>>> commonAnimeTable = Singleton<Manager.Resources>.Instance.AnimalTable.CommonAnimeTable;
			this.AnimCommonTable = ((!commonAnimeTable.ContainsKey(this.AnimalTypeID)) ? new Dictionary<int, Dictionary<int, AnimalPlayState>>() : new Dictionary<int, Dictionary<int, AnimalPlayState>>(commonAnimeTable[this.AnimalTypeID]));
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x00255954 File Offset: 0x00253D54
		protected void SetLookStateTable()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, LookState>>> lookStateTable = Singleton<Manager.Resources>.Instance.AnimalTable.LookStateTable;
			this.LookStateTable = ((!lookStateTable.ContainsKey(this.AnimalType) || !lookStateTable[this.AnimalType].ContainsKey(this.BreedingType)) ? null : new ReadOnlyDictionary<AnimalState, LookState>(lookStateTable[this.AnimalType][this.BreedingType]));
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x002559D4 File Offset: 0x00253DD4
		protected void SetExpressionTable()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			this._skinnedMeshRendererTable.Clear();
			this.ExpressionTable.Clear();
			this.CurrentExpressionTable = null;
			Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>>> expressionTable = Singleton<Manager.Resources>.Instance.AnimalTable.ExpressionTable;
			Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>> dictionary;
			if (expressionTable.TryGetValue(this.AnimalTypeID, out dictionary) && !dictionary.IsNullOrEmpty<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>>())
			{
				foreach (KeyValuePair<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>> keyValuePair in dictionary)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>())
					{
						int key = keyValuePair.Key;
						foreach (KeyValuePair<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>> keyValuePair2 in keyValuePair.Value)
						{
							if (!keyValuePair2.Value.IsNullOrEmpty<string, List<UnityEx.ValueTuple<string, int, int>>>())
							{
								int key2 = keyValuePair2.Key;
								foreach (KeyValuePair<string, List<UnityEx.ValueTuple<string, int, int>>> keyValuePair3 in keyValuePair2.Value)
								{
									if (!keyValuePair3.Value.IsNullOrEmpty<UnityEx.ValueTuple<string, int, int>>())
									{
										string key3 = keyValuePair3.Key;
										List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>> list = ListPool<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>.Get();
										foreach (UnityEx.ValueTuple<string, int, int> valueTuple in keyValuePair3.Value)
										{
											SkinnedMeshRenderer skinnedMeshRenderer;
											if (!this._skinnedMeshRendererTable.TryGetValue(valueTuple.Item1, out skinnedMeshRenderer))
											{
												Transform transform = (!(this.bodyObject != null)) ? base.transform : this.bodyObject.transform;
												GameObject gameObject = transform.FindLoop(valueTuple.Item1);
												Transform transform2 = (!(gameObject != null)) ? null : gameObject.transform;
												skinnedMeshRenderer = ((!(transform2 != null)) ? null : transform2.GetComponent<SkinnedMeshRenderer>());
												this._skinnedMeshRendererTable[valueTuple.Item1] = skinnedMeshRenderer;
											}
											if (!(skinnedMeshRenderer == null))
											{
												list.Add(new UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>(skinnedMeshRenderer, valueTuple.Item2, valueTuple.Item3));
											}
										}
										if (list.IsNullOrEmpty<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>())
										{
											ListPool<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>.Release(list);
										}
										else
										{
											Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>> dictionary2;
											if (!this.ExpressionTable.TryGetValue(key, out dictionary2) || dictionary2 == null)
											{
												dictionary2 = (this.ExpressionTable[key] = new Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>>());
											}
											Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>> dictionary3;
											if (!dictionary2.TryGetValue(key2, out dictionary3) || dictionary3 == null)
											{
												dictionary3 = (dictionary2[key2] = new Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>());
											}
											List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>> list2;
											if (dictionary3.TryGetValue(key3, out list2) && !list2.IsNullOrEmpty<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>())
											{
												ListPool<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>.Release(list2);
											}
											dictionary3[key3] = list;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00255D50 File Offset: 0x00254150
		public virtual void ChangeState(AnimalState _nextState, System.Action _changeEvent = null)
		{
			if (this.AutoChangeAnimation)
			{
				this.PlayOutAnim(delegate()
				{
					this.CurrentState = _nextState;
					System.Action changeEvent2 = _changeEvent;
					if (changeEvent2 != null)
					{
						changeEvent2();
					}
				});
			}
			else
			{
				this.CurrentState = _nextState;
				System.Action changeEvent = _changeEvent;
				if (changeEvent != null)
				{
					changeEvent();
				}
			}
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x00255DBC File Offset: 0x002541BC
		public virtual void SetState(AnimalState _nextState, System.Action _changeEvent = null)
		{
			if (this.AutoChangeAnimation)
			{
				this.PlayOutAnim(delegate()
				{
					this.CurrentState = _nextState;
					System.Action changeEvent2 = _changeEvent;
					if (changeEvent2 != null)
					{
						changeEvent2();
					}
				});
			}
			else
			{
				this.CurrentState = _nextState;
				System.Action changeEvent = _changeEvent;
				if (changeEvent != null)
				{
					changeEvent();
				}
			}
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x00255E26 File Offset: 0x00254226
		public virtual void ResetState()
		{
			this.ChangedStateEvent();
			this.StateEnter();
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x00255E34 File Offset: 0x00254234
		public bool SetNextState()
		{
			if (this.NextState == AnimalState.None)
			{
				return false;
			}
			this.CurrentState = this.NextState;
			this.NextState = AnimalState.None;
			return true;
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x00255E57 File Offset: 0x00254257
		public virtual bool ActiveState
		{
			get
			{
				return this.CurrentState != AnimalState.None && this.CurrentState != AnimalState.Destroyed;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x00255E74 File Offset: 0x00254274
		public virtual bool ParamRisePossible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x00255E77 File Offset: 0x00254277
		public virtual bool WaitPossible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x00255E7A File Offset: 0x0025427A
		public virtual bool DepopPossible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x00255E7D File Offset: 0x0025427D
		public virtual bool NormalState
		{
			[CompilerGenerated]
			get
			{
				return this.CurrentState == AnimalState.Idle || this.CurrentState == AnimalState.Locomotion;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060055E0 RID: 21984 RVA: 0x00255E97 File Offset: 0x00254297
		public bool IsWithActor
		{
			[CompilerGenerated]
			get
			{
				return this.CurrentState == AnimalState.WithPlayer || this.CurrentState == AnimalState.WithAgent || this.CurrentState == AnimalState.WithMerchant;
			}
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x00255EC0 File Offset: 0x002542C0
		public virtual bool IsWithAgentFree(AgentActor _actor)
		{
			return false;
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x00255EC3 File Offset: 0x002542C3
		public virtual bool PrioritizeState()
		{
			return this.PrioritizeState(this.CurrentState);
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x00255ED1 File Offset: 0x002542D1
		public virtual bool PrioritizeState(AnimalState _state)
		{
			return false;
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x00255ED4 File Offset: 0x002542D4
		public void SetFloat(int _id, float _value)
		{
			if (this.AnimatorControllerEnabled)
			{
				this.animator.SetFloat(_id, _value);
			}
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x00255EF0 File Offset: 0x002542F0
		public void SetFloat(string _paramName, float _value)
		{
			if (this.AnimatorControllerEnabled)
			{
				foreach (AnimatorControllerParameter animatorControllerParameter in this.animator.parameters)
				{
					if (animatorControllerParameter.name == _paramName)
					{
						this.animator.SetFloat(_paramName, _value);
						return;
					}
				}
			}
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00255F4C File Offset: 0x0025434C
		public Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>> GetExpressionTable(AnimationCategoryID _category, int _poseID)
		{
			Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>> dictionary;
			Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>> result;
			if (this.ExpressionTable.TryGetValue((int)_category, out dictionary) && !dictionary.IsNullOrEmpty<int, Dictionary<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>>() && dictionary.TryGetValue(_poseID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x00255F88 File Offset: 0x00254388
		private void ChangeExpression(string stateName)
		{
			if (this.CurrentExpressionTable.IsNullOrEmpty<string, List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>>() || stateName.IsNullOrEmpty())
			{
				return;
			}
			List<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>> list;
			if (this.CurrentExpressionTable.TryGetValue(stateName, out list) && !list.IsNullOrEmpty<UnityEx.ValueTuple<SkinnedMeshRenderer, int, int>>())
			{
				foreach (UnityEx.ValueTuple<SkinnedMeshRenderer, int, int> valueTuple in list)
				{
					if (!(valueTuple.Item1 == null))
					{
						valueTuple.Item1.SetBlendShapeWeight(valueTuple.Item2, (float)valueTuple.Item3);
					}
				}
			}
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00256044 File Offset: 0x00254444
		public bool SetPlayAnimState(AnimationCategoryID _category)
		{
			this.InAnimStates.Clear();
			this.OutAnimStates.Clear();
			this.CurrentExpressionTable = null;
			if (this.AnimatorEmtpy)
			{
				return false;
			}
			Dictionary<int, AnimalPlayState> source;
			if (!this.AnimCommonTable.TryGetValue((int)_category, out source))
			{
				return false;
			}
			KeyValuePair<int, AnimalPlayState> keyValuePair = source.Rand<int, AnimalPlayState>();
			if (keyValuePair.Value == null)
			{
				return false;
			}
			this.CurrentExpressionTable = this.GetExpressionTable(_category, keyValuePair.Key);
			if (this.CurrentAnimState == null || this.CurrentAnimState.MainStateInfo.Controller != keyValuePair.Value.MainStateInfo.Controller)
			{
				this.animator.runtimeAnimatorController = keyValuePair.Value.MainStateInfo.Controller;
			}
			this.CurrentAnimState = keyValuePair.Value;
			if (!keyValuePair.Value.MainStateInfo.InStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item in keyValuePair.Value.MainStateInfo.InStateInfos)
				{
					this.InAnimStates.Enqueue(item);
				}
			}
			if (!keyValuePair.Value.MainStateInfo.OutStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item2 in keyValuePair.Value.MainStateInfo.OutStateInfos)
				{
					this.OutAnimStates.Enqueue(item2);
				}
			}
			return true;
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x002561DC File Offset: 0x002545DC
		public bool SetPlayAnimState(AnimationCategoryID _category, int _poseID)
		{
			this.InAnimStates.Clear();
			this.OutAnimStates.Clear();
			this.CurrentExpressionTable = null;
			if (this.AnimatorEmtpy)
			{
				return false;
			}
			Dictionary<int, AnimalPlayState> dictionary;
			if (!this.AnimCommonTable.TryGetValue((int)_category, out dictionary))
			{
				return false;
			}
			AnimalPlayState animalPlayState;
			if (!dictionary.TryGetValue(_poseID, out animalPlayState))
			{
				return false;
			}
			this.CurrentExpressionTable = this.GetExpressionTable(_category, _poseID);
			if (this.CurrentAnimState == null || this.CurrentAnimState.MainStateInfo.Controller != animalPlayState.MainStateInfo.Controller)
			{
				this.animator.runtimeAnimatorController = animalPlayState.MainStateInfo.Controller;
			}
			this.CurrentAnimState = animalPlayState;
			if (!animalPlayState.MainStateInfo.InStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item in animalPlayState.MainStateInfo.InStateInfos)
				{
					this.InAnimStates.Enqueue(item);
				}
			}
			if (!animalPlayState.MainStateInfo.OutStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item2 in animalPlayState.MainStateInfo.OutStateInfos)
				{
					this.OutAnimStates.Enqueue(item2);
				}
			}
			return true;
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x00256340 File Offset: 0x00254740
		public void SetPlayAnimState(AnimalPlayState _playState)
		{
			this.InAnimStates.Clear();
			this.OutAnimStates.Clear();
			if (_playState == null)
			{
				return;
			}
			AnimalPlayState.PlayStateInfo mainStateInfo = _playState.MainStateInfo;
			if (!this.AnimatorEmtpy && (this.CurrentAnimState == null || this.CurrentAnimState.MainStateInfo.Controller != mainStateInfo.Controller))
			{
				this.animator.runtimeAnimatorController = mainStateInfo.Controller;
			}
			if (!mainStateInfo.InStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item in mainStateInfo.InStateInfos)
				{
					this.InAnimStates.Enqueue(item);
				}
			}
			if (!mainStateInfo.OutStateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item2 in mainStateInfo.OutStateInfos)
				{
					this.OutAnimStates.Enqueue(item2);
				}
			}
			this.CurrentAnimState = _playState;
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x00256451 File Offset: 0x00254851
		public void StopPlayAnimChange()
		{
			if (this.inAnimDisposable != null)
			{
				this.inAnimDisposable.Dispose();
			}
			if (this.outAnimDisposable != null)
			{
				this.outAnimDisposable.Dispose();
			}
			this.inAnimDisposable = null;
			this.outAnimDisposable = null;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00256494 File Offset: 0x00254894
		public void PlayInAnim(AnimationCategoryID _category, System.Action _endEvent = null)
		{
			this.StopPlayAnimChange();
			if (!this.SetPlayAnimState(_category))
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartInAnimation(_endEvent);
			this.inAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00256500 File Offset: 0x00254900
		public void PlayInAnim(AnimationCategoryID _category, int _poseID, System.Action _endEvent = null)
		{
			this.StopPlayAnimChange();
			if (!this.SetPlayAnimState(_category, _poseID))
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartInAnimation(_endEvent);
			this.inAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0025656C File Offset: 0x0025496C
		private IEnumerator StartInAnimation(System.Action _endEvent = null)
		{
			Queue<AnimalPlayState.StateInfo> _queue = this.InAnimStates;
			Animator _animator = this.animator;
			this.LastAnimState.Item1 = _animator.runtimeAnimatorController;
			bool _fadeEnable = this.CurrentAnimState.MainStateInfo.InFadeEnable;
			float _fadeTime = this.CurrentAnimState.MainStateInfo.InFadeSecond;
			while (0 < _queue.Count)
			{
				AnimalPlayState.StateInfo _state = _queue.Dequeue();
				this.ChangeExpression(_state.animName);
				this.LastAnimState.Item2 = _state.animCode;
				this.LastAnimState.Item3 = _state.layer;
				this.LastAnimState.Item4 = _state.animName;
				if (_fadeEnable)
				{
					_animator.CrossFadeInFixedTime(_state.animCode, _fadeTime, _state.layer, 0f);
					IConnectableObservable<long> _waiter = Observable.Timer(TimeSpan.FromSeconds((double)_fadeTime)).Publish<long>();
					_waiter.Connect();
					yield return _waiter.ToYieldInstruction<long>();
				}
				else
				{
					_animator.Play(_state.animCode, _state.layer, 0f);
					yield return null;
				}
				yield return null;
				AnimatorStateInfo _stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
				while (this.AnimatorControllerEnabled && (_animator.IsInTransition(_state.layer) || (_stateInfo.IsName(_state.animName) && _stateInfo.normalizedTime < 1f)))
				{
					_stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this.inAnimDisposable = null;
			if (_endEvent != null)
			{
				_endEvent();
			}
			yield break;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x00256590 File Offset: 0x00254990
		public IEnumerator StartInAnimation(bool _fadeEnable, float _fadeTime, System.Action _endEvent = null)
		{
			Queue<AnimalPlayState.StateInfo> _queue = this.InAnimStates;
			Animator _animator = this.animator;
			this.LastAnimState.Item1 = _animator.runtimeAnimatorController;
			while (0 < _queue.Count)
			{
				AnimalPlayState.StateInfo _state = _queue.Dequeue();
				this.ChangeExpression(_state.animName);
				this.LastAnimState.Item2 = _state.animCode;
				this.LastAnimState.Item3 = _state.layer;
				this.LastAnimState.Item4 = _state.animName;
				if (_fadeEnable)
				{
					_animator.CrossFadeInFixedTime(_state.animCode, _fadeTime, _state.layer, 0f);
					IConnectableObservable<long> _waiter = Observable.Timer(TimeSpan.FromSeconds((double)_fadeTime)).Publish<long>();
					_waiter.Connect();
					yield return _waiter.ToYieldInstruction<long>();
				}
				else
				{
					_animator.Play(_state.animCode, _state.layer, 0f);
					yield return null;
				}
				AnimatorStateInfo _stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
				while (this.AnimatorControllerEnabled && (_animator.IsInTransition(_state.layer) || (_stateInfo.IsName(_state.animName) && _stateInfo.normalizedTime < 1f)))
				{
					_stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this.inAnimDisposable = null;
			if (_endEvent != null)
			{
				_endEvent();
			}
			yield break;
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x002565C0 File Offset: 0x002549C0
		public void PlayOutAnim()
		{
			this.StopPlayAnimChange();
			if (!this.AnimatorControllerEnabled)
			{
				return;
			}
			IEnumerator _coroutine = this.StartOutAnimation(null);
			this.outAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x0025661C File Offset: 0x00254A1C
		public void PlayOutAnim(AnimalPlayState.StateInfo[] _stateInfos, System.Action _endEvent = null)
		{
			this.OutAnimStates.Clear();
			if (!_stateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item in _stateInfos)
				{
					this.OutAnimStates.Enqueue(item);
				}
			}
			this.StopPlayAnimChange();
			if (!this.AnimatorControllerEnabled)
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartInAnimation(_endEvent);
			this.inAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x002566CC File Offset: 0x00254ACC
		public void PlayOutAnim(AnimalPlayState.StateInfo[] _stateInfos, bool _fadeEnable, float _fadeSecond, System.Action _endEvent = null)
		{
			this.OutAnimStates.Clear();
			if (!_stateInfos.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				foreach (AnimalPlayState.StateInfo item in _stateInfos)
				{
					this.OutAnimStates.Enqueue(item);
				}
			}
			this.StopPlayAnimChange();
			if (!this.AnimatorControllerEnabled)
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartOutAnimation(_fadeEnable, _fadeSecond, _endEvent);
			this.outAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00256780 File Offset: 0x00254B80
		public void PlayOutAnim(System.Action _endEvent)
		{
			this.StopPlayAnimChange();
			if (!this.AnimatorControllerEnabled || this.OutAnimStates.IsNullOrEmpty<AnimalPlayState.StateInfo>())
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartOutAnimation(_endEvent);
			this.outAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x002567F8 File Offset: 0x00254BF8
		public void PlayOutAnim(AnimationCategoryID _category, System.Action _endEvent = null)
		{
			this.StopPlayAnimChange();
			if (!this.SetPlayAnimState(_category))
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartOutAnimation(_endEvent);
			this.outAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00256864 File Offset: 0x00254C64
		public void PlayOutAnim(AnimationCategoryID _category, int _poseID, System.Action _endEvent = null)
		{
			this.StopPlayAnimChange();
			if (!this.SetPlayAnimState(_category, _poseID))
			{
				if (_endEvent != null)
				{
					_endEvent();
				}
				return;
			}
			IEnumerator _coroutine = this.StartOutAnimation(_endEvent);
			this.outAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x002568D0 File Offset: 0x00254CD0
		private IEnumerator StartOutAnimation(System.Action _endEvent = null)
		{
			Queue<AnimalPlayState.StateInfo> _queue = this.OutAnimStates;
			Animator _animator = this.animator;
			this.LastAnimState.Item1 = _animator.runtimeAnimatorController;
			bool _fadeEnable = this.CurrentAnimState.MainStateInfo.OutFadeEnable;
			float _fadeTime = this.CurrentAnimState.MainStateInfo.OutFadeSecond;
			while (0 < _queue.Count)
			{
				AnimalPlayState.StateInfo _state = _queue.Dequeue();
				this.LastAnimState.Item2 = _state.animCode;
				this.LastAnimState.Item3 = _state.layer;
				this.LastAnimState.Item4 = _state.animName;
				if (_fadeEnable)
				{
					_animator.CrossFadeInFixedTime(_state.animCode, _fadeTime, _state.layer, 0f);
					IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)_fadeTime)).TakeUntilDestroy(base.gameObject).Publish<long>();
					waiter.Connect();
					yield return waiter.ToYieldInstruction<long>();
				}
				else
				{
					_animator.Play(_state.animCode, _state.layer, 0f);
					yield return null;
				}
				yield return null;
				AnimatorStateInfo _stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
				while (this.AnimatorControllerEnabled && (_animator.IsInTransition(_state.layer) || (_stateInfo.IsName(_state.animName) && _stateInfo.normalizedTime < 1f)))
				{
					_stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			this.outAnimDisposable = null;
			if (_endEvent != null)
			{
				_endEvent();
			}
			yield break;
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x002568F4 File Offset: 0x00254CF4
		public IEnumerator StartOutAnimation(bool _fadeEnable, float _fadeTime, System.Action _endEvent = null)
		{
			Queue<AnimalPlayState.StateInfo> _queue = this.OutAnimStates;
			Animator _animator = this.animator;
			this.LastAnimState.Item1 = _animator.runtimeAnimatorController;
			while (0 < _queue.Count)
			{
				AnimalPlayState.StateInfo _state = _queue.Dequeue();
				this.LastAnimState.Item2 = _state.animCode;
				this.LastAnimState.Item3 = _state.layer;
				this.LastAnimState.Item4 = _state.animName;
				if (_fadeEnable)
				{
					_animator.CrossFadeInFixedTime(_state.animCode, _fadeTime, _state.layer);
					IConnectableObservable<long> _waiter = Observable.Timer(TimeSpan.FromSeconds((double)_fadeTime)).Publish<long>();
					_waiter.Connect();
					yield return _waiter.ToYieldInstruction<long>();
				}
				else
				{
					_animator.Play(_state.animCode, _state.layer, 0f);
					yield return null;
				}
				AnimatorStateInfo _stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
				while (this.AnimatorControllerEnabled && (_animator.IsInTransition(_state.layer) || (_stateInfo.IsName(_state.animName) && _stateInfo.normalizedTime < 1f)))
				{
					_stateInfo = _animator.GetCurrentAnimatorStateInfo(_state.layer);
					yield return null;
				}
				yield return null;
			}
			yield return null;
			if (_endEvent != null)
			{
				_endEvent();
			}
			this.outAnimDisposable = null;
			yield break;
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x00256924 File Offset: 0x00254D24
		public void StartInAnimationWithActor(int _poseID)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, Dictionary<int, AnimalPlayState>> withAgentAnimeTable = Singleton<Manager.Resources>.Instance.AnimalTable.WithAgentAnimeTable;
			AnimalPlayState animalPlayState;
			if (!withAgentAnimeTable.ContainsKey(this.AnimalTypeID) || !withAgentAnimeTable[this.AnimalTypeID].TryGetValue(_poseID, out animalPlayState))
			{
				return;
			}
			this.StopPlayAnimChange();
			this.SetPlayAnimState(animalPlayState);
			this.CurrentAnimState = animalPlayState;
			IEnumerator _coroutine = this.StartInAnimation(animalPlayState.MainStateInfo.InFadeEnable, animalPlayState.MainStateInfo.InFadeSecond, null);
			this.inAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x002569DC File Offset: 0x00254DDC
		public void StartOutAnimationWithActor()
		{
			if (this.CurrentAnimState == null)
			{
				return;
			}
			this.StopPlayAnimChange();
			AnimalPlayState currentAnimState = this.CurrentAnimState;
			IEnumerator _coroutine = this.StartOutAnimation(currentAnimState.MainStateInfo.OutFadeEnable, currentAnimState.MainStateInfo.OutFadeSecond, null);
			this.outAnimDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00256A54 File Offset: 0x00254E54
		public bool IsCurrentAnimationCheck()
		{
			if (this.LastAnimState.Item1 == null || !this.AnimatorControllerEnabled)
			{
				return false;
			}
			int item = this.LastAnimState.Item3;
			return this.animator.IsInTransition(item) || this.animator.GetCurrentAnimatorStateInfo(item).IsName(this.LastAnimState.Item4);
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00256AC4 File Offset: 0x00254EC4
		public bool IsCurrentAnimationCheck(int _animHash, int _layer = 0)
		{
			return this.AnimatorControllerEnabled && _animHash == this.animator.GetCurrentAnimatorStateInfo(_layer).shortNameHash;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x00256AFC File Offset: 0x00254EFC
		public bool AnimationKeepWaiting()
		{
			if (this.LastAnimState.Item1 == null || !this.AnimatorControllerEnabled)
			{
				return false;
			}
			int item = this.LastAnimState.Item3;
			if (this.animator.IsInTransition(item))
			{
				return true;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(item);
			return currentAnimatorStateInfo.IsName(this.LastAnimState.Item4) && currentAnimatorStateInfo.normalizedTime < 1f;
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x00256B80 File Offset: 0x00254F80
		public bool IsCurrentAnimationEnd(int _layer = 0)
		{
			return this.AnimatorControllerEnabled && this.animator.GetCurrentAnimatorStateInfo(_layer).normalizedTime >= 1f;
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x00256BBC File Offset: 0x00254FBC
		public bool UseActionPoint
		{
			get
			{
				return !(this.actionPoint == null) && this.actionPoint.MyUse(this);
			}
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x00256BE1 File Offset: 0x00254FE1
		public virtual void CancelActionPoint()
		{
			if (this.HasActionPoint)
			{
				if (!this.actionPoint.MyUse(this))
				{
					this.actionPoint.RemoveBooking(this);
				}
				this.NextState = AnimalState.None;
				this.ActionType = ActionTypes.None;
				this.actionPoint = null;
			}
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x00256C20 File Offset: 0x00255020
		public virtual void RemoveActionPoint()
		{
			if (this.HasActionPoint)
			{
				if (this.actionPoint.MyUse(this))
				{
					this.actionPoint.StopUsing();
					this.NextState = AnimalState.None;
					this.ActionType = ActionTypes.None;
					this.actionPoint = null;
				}
				else
				{
					this.CancelActionPoint();
				}
			}
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00256C75 File Offset: 0x00255075
		public virtual void MissingActionPoint()
		{
			if (this.HasActionPoint && this.actionPoint.MyUse(this))
			{
				return;
			}
			this.NextState = AnimalState.None;
			this.ActionType = ActionTypes.None;
			this.actionPoint = null;
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00256CA9 File Offset: 0x002550A9
		public virtual void ReleaseActionPoint()
		{
			if (!this.HasActionPoint)
			{
				return;
			}
			this.actionPoint.StopUsing(this);
			this.actionPoint.RemoveBooking(this);
			this.actionPoint = null;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x00256CD8 File Offset: 0x002550D8
		public virtual bool SetActionPoint(AnimalActionPoint _actionPoint, AnimalState _nextState, ActionTypes _actionType)
		{
			if (_actionPoint == null || this.actionPoint == _actionPoint)
			{
				return false;
			}
			this.NextState = _nextState;
			if (this.actionPoint != null)
			{
				if (this.actionPoint != null)
				{
					this.actionPoint.RemoveBooking(this);
				}
			}
			this.actionPoint = _actionPoint;
			this.actionPoint.AddBooking(this);
			this.ActionType = _actionType;
			return true;
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x00256D50 File Offset: 0x00255150
		protected AudioSource Play3DSound(int id)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack == null)
			{
				return null;
			}
			return soundPack.Play(id, Sound.Type.GameSE3D, 0f);
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x00256D93 File Offset: 0x00255193
		public virtual void ReleaseBody()
		{
			if (this.bodyObject)
			{
				UnityEngine.Object.Destroy(this.bodyObject);
			}
			this.bodyRenderers = null;
			this.bodyRendererObjects = null;
			this.bodyParticleObjects = null;
			this.ModelInfo.ClearShapeState();
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x00256DD0 File Offset: 0x002551D0
		public virtual void CreateBody()
		{
			AssetBundleInfo assetInfo = this.ModelInfo.AssetInfo;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetInfo.assetbundle, assetInfo.asset, false, assetInfo.manifest);
			if (gameObject == null)
			{
				return;
			}
			MapScene.AddAssetBundlePath(assetInfo.assetbundle, assetInfo.manifest);
			this.bodyObject = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			if (this.bodyObject != null)
			{
				GameObject gameObject2 = new GameObject("BodyRoot");
				gameObject2.transform.SetParent(base.transform, false);
				this.bodyObject.transform.SetParent(gameObject2.transform, false);
				this.bodyObject = gameObject2;
			}
			this.ModelInfo.SetShapeState((!(this.bodyObject != null)) ? null : this.bodyObject.transform);
			this.bodyRenderers = this.bodyObject.GetComponentsInChildren<Renderer>(true);
			if (this.bodyRenderers != null)
			{
				this.bodyRendererObjects = new GameObject[this.bodyRenderers.Length];
				for (int i = 0; i < this.bodyRenderers.Length; i++)
				{
					this.bodyRendererObjects[i] = this.bodyRenderers[i].gameObject;
				}
			}
			this.bodyParticleRenderers = this.bodyObject.GetComponentsInChildren<ParticleSystemRenderer>(true);
			if (this.bodyParticleRenderers != null)
			{
				this.bodyParticleObjects = new GameObject[this.bodyParticleRenderers.Length];
				for (int j = 0; j < this.bodyParticleRenderers.Length; j++)
				{
					this.bodyParticleObjects[j] = this.bodyParticleRenderers[j].gameObject;
				}
			}
			this.animator = base.GetComponentInChildren<Animator>(true);
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x00256F7C File Offset: 0x0025537C
		public virtual void CreateBody(GameObject prefab)
		{
			if (prefab == null)
			{
				return;
			}
			this.bodyObject = UnityEngine.Object.Instantiate<GameObject>(prefab, base.transform, false);
			if (this.bodyObject != null)
			{
				GameObject gameObject = new GameObject("BodyRoot");
				gameObject.transform.SetParent(base.transform, false);
				this.bodyObject.transform.SetParent(gameObject.transform, false);
				this.bodyObject = gameObject;
			}
			this.ModelInfo.SetShapeState((!(this.bodyObject != null)) ? null : this.bodyObject.transform);
			this.bodyRenderers = this.bodyObject.GetComponentsInChildren<Renderer>(true);
			if (this.bodyRenderers != null)
			{
				this.bodyRendererObjects = new GameObject[this.bodyRenderers.Length];
				for (int i = 0; i < this.bodyRenderers.Length; i++)
				{
					this.bodyRendererObjects[i] = this.bodyRenderers[i].gameObject;
				}
			}
			this.bodyParticleRenderers = this.bodyObject.GetComponentsInChildren<ParticleSystemRenderer>(true);
			if (this.bodyParticleRenderers != null)
			{
				this.bodyParticleObjects = new GameObject[this.bodyParticleRenderers.Length];
				for (int j = 0; j < this.bodyParticleRenderers.Length; j++)
				{
					this.bodyParticleObjects[j] = this.bodyParticleRenderers[j].gameObject;
				}
			}
			this.animator = base.GetComponentInChildren<Animator>(true);
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x002570EB File Offset: 0x002554EB
		public void LoadBody()
		{
			this.ReleaseBody();
			this.CreateBody();
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x002570F9 File Offset: 0x002554F9
		public void LoadBody(GameObject prefab)
		{
			this.ReleaseBody();
			this.CreateBody(prefab);
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x00257108 File Offset: 0x00255508
		public void SetLookPattern(int _ptnNo)
		{
			if (this.neckController)
			{
				this.neckController.ptnNo = _ptnNo;
			}
			if (this.eyeController)
			{
				this.eyeController.ptnNo = _ptnNo;
			}
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x00257144 File Offset: 0x00255544
		protected virtual void LookTargetUpdate()
		{
			this.LookWaitMode = false;
			if (this.Target == null || this.LookStateTable == null || !this.LookStateTable.ContainsKey(this.CurrentState))
			{
				this.SetLookPattern(0);
				return;
			}
			LookState lookState = this.LookStateTable[this.CurrentState];
			int ptnNo = lookState.ptnNo;
			bool flag = false;
			this.LookWaitMode = (lookState.waitFlag && flag);
			if (ptnNo != 0)
			{
				if (ptnNo == 1 || ptnNo == 2)
				{
					this.SetLookPattern((!flag) ? 0 : ptnNo);
				}
			}
			else
			{
				this.SetLookPattern(ptnNo);
			}
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x00257204 File Offset: 0x00255604
		protected void RefreshCommands(bool _conditionCheck)
		{
			CommandArea commandArea = Manager.Map.GetCommandArea();
			if (commandArea == null)
			{
				return;
			}
			if (_conditionCheck)
			{
				if (commandArea.ContainsConsiderationObject(this))
				{
					commandArea.RefreshCommands();
				}
			}
			else
			{
				commandArea.RefreshCommands();
			}
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x00257247 File Offset: 0x00255647
		public virtual void SetSearchTargetEnabled(bool _enabled, bool _clearCollision)
		{
			if (this.SearchActor != null)
			{
				this.SearchActor.SetSearchEnabled(_enabled, _clearCollision);
			}
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x00257267 File Offset: 0x00255667
		public virtual void RefreshSearchTarget()
		{
			if (this.SearchActor != null)
			{
				AnimalSearchActor searchActor = this.SearchActor;
				if (searchActor != null)
				{
					searchActor.RefreshSearchActorTable();
				}
			}
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0025728E File Offset: 0x0025568E
		public virtual void Refresh()
		{
			this.RefreshCommands(true);
			this.RefreshSearchTarget();
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005610 RID: 22032 RVA: 0x0025729D File Offset: 0x0025569D
		private static float RaycastDistance { get; } = 1000f;

		// Token: 0x06005611 RID: 22033 RVA: 0x002572A4 File Offset: 0x002556A4
		public static Vector3 RaycastStartPoint(Vector3 _position)
		{
			return _position + Vector3.up * 5f;
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x002572BC File Offset: 0x002556BC
		public void UpdateCurrentMapArea(LayerMask layer)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				this.CurrentMapArea = null;
				return;
			}
			Dictionary<int, Chunk> chunkTable = Singleton<Manager.Map>.Instance.ChunkTable;
			if (chunkTable.IsNullOrEmpty<int, Chunk>())
			{
				this.CurrentMapArea = null;
				return;
			}
			RaycastHit raycastHit;
			if (Physics.Raycast(this.Position + Vector3.up * 5f, Vector3.down, out raycastHit, 1000f, layer))
			{
				bool flag = false;
				foreach (KeyValuePair<int, Chunk> keyValuePair in chunkTable)
				{
					foreach (MapArea mapArea in keyValuePair.Value.MapAreas)
					{
						if (flag = mapArea.ContainsCollider(raycastHit.collider))
						{
							this.CurrentMapArea = mapArea;
							this.ChunkID = mapArea.ChunkID;
							this.AreaID = mapArea.AreaID;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					this.CurrentMapArea = null;
				}
			}
			else
			{
				this.CurrentMapArea = null;
			}
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x00257404 File Offset: 0x00255804
		public bool GetCurrentMapAreaType(MapArea _mapArea, out MapArea.AreaType _type)
		{
			_type = MapArea.AreaType.Normal;
			if (!Singleton<Manager.Resources>.IsInstance() || _mapArea == null)
			{
				return false;
			}
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			int num = Physics.RaycastNonAlloc(AnimalBase.RaycastStartPoint(this.Position), Vector3.up, AnimalBase.raycastHits, AnimalBase.RaycastDistance, areaDetectionLayer);
			_type = ((0 >= num) ? MapArea.AreaType.Normal : MapArea.AreaType.Indoor);
			return true;
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00257478 File Offset: 0x00255878
		public static bool GetMapAreaType(Vector3 _position, MapArea _mapArea, out MapArea.AreaType _type)
		{
			_type = MapArea.AreaType.Normal;
			if (_mapArea == null || !Singleton<Manager.Resources>.IsInstance())
			{
				return false;
			}
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			int num = Physics.RaycastNonAlloc(AnimalBase.RaycastStartPoint(_position), Vector3.up, AnimalBase.raycastHits, AnimalBase.RaycastDistance, areaDetectionLayer);
			_type = ((0 >= num) ? MapArea.AreaType.Normal : MapArea.AreaType.Indoor);
			return true;
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x002574E8 File Offset: 0x002558E8
		public static bool GetMapArea(Vector3 _position, out MapArea _mapArea, out MapArea.AreaType _type)
		{
			_type = MapArea.AreaType.Normal;
			_mapArea = null;
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return false;
			}
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			int num = Physics.RaycastNonAlloc(AnimalBase.RaycastStartPoint(_position), Vector3.down, AnimalBase.raycastHits, AnimalBase.RaycastDistance, areaDetectionLayer);
			if (num <= 0)
			{
				return false;
			}
			List<RaycastHit> list = ListPool<RaycastHit>.Get();
			for (int i = 0; i < num; i++)
			{
				RaycastHit item = AnimalBase.raycastHits[i];
				if (item.collider != null)
				{
					list.Add(item);
				}
			}
			if (list.IsNullOrEmpty<RaycastHit>())
			{
				ListPool<RaycastHit>.Release(list);
				return false;
			}
			list.Sort((RaycastHit a, RaycastHit b) => (int)(a.point - _position).sqrMagnitude - (int)(b.point - _position).sqrMagnitude);
			int num2 = 0;
			while (num2 < list.Count && _mapArea == null)
			{
				_mapArea = list[num2].collider.gameObject.GetComponent<MapArea>();
				num2++;
			}
			int num3 = 0;
			while (num3 < list.Count && _mapArea == null)
			{
				Transform parent = list[num3].collider.transform.parent;
				if (!(parent == null))
				{
					_mapArea = parent.GetComponent<MapArea>();
				}
				num3++;
			}
			ListPool<RaycastHit>.Release(list);
			if (_mapArea == null)
			{
				return false;
			}
			num = Physics.RaycastNonAlloc(AnimalBase.RaycastStartPoint(_position), Vector3.up, AnimalBase.raycastHits, AnimalBase.RaycastDistance, areaDetectionLayer);
			_type = ((0 >= num) ? MapArea.AreaType.Normal : MapArea.AreaType.Indoor);
			return true;
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x002576B8 File Offset: 0x00255AB8
		public bool CheckTargetOnDistance(Vector3 _targetPosition, float _distance)
		{
			Vector2 vector = new Vector2(_targetPosition.x - this.Position.x, _targetPosition.z - this.Position.z);
			return vector.sqrMagnitude <= Mathf.Pow(_distance, 2f);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x00257710 File Offset: 0x00255B10
		public bool CheckTargetOnHeight(Vector3 _targetPosition, float _height)
		{
			return Mathf.Abs(_targetPosition.y - this.Position.y) <= _height;
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00257740 File Offset: 0x00255B40
		public bool CheckTargetOnAngle(Vector3 _targetPosition, float _angle)
		{
			Vector2 vector = new Vector2(_targetPosition.x - this.Position.x, _targetPosition.z - this.Position.z);
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(this.Forward.x, this.Forward.z);
			Vector2 normalized2 = vector2.normalized;
			float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
			float num = Mathf.Acos(f) * 57.29578f;
			return num * 2f <= _angle;
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x002577EC File Offset: 0x00255BEC
		public bool CheckTargetOnArea(Vector3 _targetPosition, float _distance, float _height, float _angle)
		{
			return this.CheckTargetOnDistance(_targetPosition, _distance) && this.CheckTargetOnHeight(_targetPosition, _height) && this.CheckTargetOnAngle(_targetPosition, _angle);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x00257814 File Offset: 0x00255C14
		protected AnimalState RandState(List<AnimalState> _stateList, AnimalState _prevState)
		{
			if (_stateList.IsNullOrEmpty<AnimalState>())
			{
				return AnimalState.None;
			}
			List<AnimalState> list = ListPool<AnimalState>.Get();
			list.AddRange(_stateList);
			list.RemoveAll((AnimalState x) => x == _prevState);
			if (list.IsNullOrEmpty<AnimalState>())
			{
				ListPool<AnimalState>.Release(list);
				return AnimalState.None;
			}
			AnimalState _state = list[UnityEngine.Random.Range(0, list.Count)];
			_stateList.RemoveAll((AnimalState x) => x == _state);
			return _state;
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x0025789F File Offset: 0x00255C9F
		protected float AngleAbs(float _angle)
		{
			if (_angle < 0f)
			{
				_angle = _angle % 360f + 360f;
			}
			else if (360f <= _angle)
			{
				_angle %= 360f;
			}
			return _angle;
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x002578D5 File Offset: 0x00255CD5
		protected float Angle360To180(float _angle)
		{
			_angle %= 360f;
			if (_angle <= -180f)
			{
				_angle += 360f;
			}
			else if (360f < _angle)
			{
				_angle -= 360f;
			}
			return _angle;
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0025790E File Offset: 0x00255D0E
		protected float Angle180To360(float _angle)
		{
			_angle %= 360f;
			if (_angle < 0f)
			{
				_angle += 360f;
			}
			else if (360f <= _angle)
			{
				_angle -= _angle;
			}
			return _angle;
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x00257944 File Offset: 0x00255D44
		protected Vector3 GetRandomPosOnCircle(float _radius)
		{
			float f = UnityEngine.Random.value * 3.1415927f * 2f;
			float num = _radius * Mathf.Sqrt(UnityEngine.Random.value);
			return new Vector3(num * Mathf.Cos(f), 0f, num * Mathf.Sin(f));
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x0025798C File Offset: 0x00255D8C
		public virtual Vector3 GetWithActorPoint(Actor _targetActor, int _poseID)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return this.Position;
			}
			Manager.Resources.AnimalTables animalTable = Singleton<Manager.Resources>.Instance.AnimalTable;
			if (animalTable == null)
			{
				return this.Position;
			}
			Dictionary<int, AnimalPlayState> dictionary = null;
			AnimalPlayState animalPlayState = null;
			if (animalTable.WithAgentAnimeTable.TryGetValue(this.AnimalTypeID, out dictionary) && dictionary.TryGetValue(_poseID, out animalPlayState))
			{
				float shapeBodyValue = _targetActor.ChaControl.GetShapeBodyValue(0);
				float z = 0f;
				float num = 0.5f;
				if (shapeBodyValue == num)
				{
					z = animalPlayState.FloatList.GetElement(1);
				}
				else if (shapeBodyValue < num)
				{
					float t = Mathf.InverseLerp(0f, 0.5f, shapeBodyValue);
					z = Mathf.Lerp(animalPlayState.FloatList.GetElement(0), animalPlayState.FloatList.GetElement(1), t);
				}
				else if (num < shapeBodyValue)
				{
					float t2 = Mathf.InverseLerp(0.5f, 1f, shapeBodyValue);
					z = Mathf.Lerp(animalPlayState.FloatList.GetElement(1), animalPlayState.FloatList.GetElement(2), t2);
				}
				Vector3 point = new Vector3(0f, 0f, z);
				return _targetActor.Position + _targetActor.Rotation * point;
			}
			return this.Position;
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x00257AD0 File Offset: 0x00255ED0
		public void SetWithActorPoint(Actor _targetActor, int _poseID)
		{
			this.Position = this.GetWithActorPoint(_targetActor, _poseID);
			Vector3 eulerAngles = Quaternion.LookRotation(_targetActor.Position - this.Position).eulerAngles;
			eulerAngles.x = (eulerAngles.z = 0f);
			this.EulerAngles = eulerAngles;
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x00257B28 File Offset: 0x00255F28
		public virtual Vector3 GetWithActorGetPoint(Actor _targetActor)
		{
			if (Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null)
			{
				AnimalDefinePack.WithActorInfoGroup withActorInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.WithActorInfo;
				if (withActorInfo != null)
				{
					Vector3 point = new Vector3(0f, 0f, withActorInfo.GetPointDistance);
					return _targetActor.Position + _targetActor.Rotation * point;
				}
			}
			return Vector3.zero;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x00257BA0 File Offset: 0x00255FA0
		public void SetWithActorGetPoint(Actor _targetActor)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			this.Position = this.GetWithActorGetPoint(_targetActor);
			Vector3 eulerAngles = Quaternion.LookRotation(_targetActor.Position - this.Position).eulerAngles;
			eulerAngles.x = (eulerAngles.z = 0f);
			this.EulerAngles = eulerAngles;
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x00257C04 File Offset: 0x00256004
		public void Relocate(LocateTypes _locateType = LocateTypes.NavMesh)
		{
			if (_locateType != LocateTypes.NavMesh)
			{
				if (_locateType == LocateTypes.Collider)
				{
					if (Singleton<Manager.Map>.IsInstance() && Singleton<Manager.Resources>.IsInstance())
					{
						Manager.Resources instance = Singleton<Manager.Resources>.Instance;
						Manager.Map instance2 = Singleton<Manager.Map>.Instance;
						RaycastHit raycastHit;
						if (Physics.Raycast(this.Position + Vector3.up * 5f, Vector3.down, out raycastHit, 100f, instance.DefinePack.MapDefines.AreaDetectionLayer))
						{
							bool flag = false;
							Dictionary<int, Chunk> chunkTable = instance2.ChunkTable;
							foreach (KeyValuePair<int, Chunk> keyValuePair in chunkTable)
							{
								foreach (MapArea mapArea in keyValuePair.Value.MapAreas)
								{
									if (flag = mapArea.ContainsCollider(raycastHit.collider))
									{
										this.Position = raycastHit.point;
										break;
									}
								}
								if (flag)
								{
									break;
								}
							}
						}
					}
				}
			}
			else
			{
				Vector3 position = this.Position;
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(position, out navMeshHit, 15f, this.NavMeshAreaMask))
				{
					this.Position = navMeshHit.position;
				}
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06005624 RID: 22052 RVA: 0x00257D78 File Offset: 0x00256178
		public static bool RandomBool
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(0, 100) < 50;
			}
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x00257D88 File Offset: 0x00256188
		public void CrossFade(float _fadeTime = -1f)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			ActorCameraControl cameraControl = Manager.Map.GetCameraControl();
			if (cameraControl == null)
			{
				return;
			}
			Camera cameraComponent = cameraControl.CameraComponent;
			if (cameraComponent == null)
			{
				return;
			}
			AnimalDefinePack.GroundWildInfoGroup groundWildInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.GroundWildInfo;
			Vector3 vector = this.Position - cameraComponent.transform.position;
			float num = Vector3.SqrMagnitude(vector);
			if (groundWildInfo.DepopCrossFadeDistance * groundWildInfo.DepopCrossFadeDistance < num)
			{
				return;
			}
			float num2 = Vector3.Angle(vector, cameraComponent.transform.forward);
			if (groundWildInfo.DepopCrossFadeAngle < num2)
			{
				return;
			}
			cameraControl.CrossFade.FadeStart(_fadeTime);
		}

		// Token: 0x04004F45 RID: 20293
		[SerializeField]
		[DisableInPlayMode]
		private AnimalTypes _animalType;

		// Token: 0x04004F46 RID: 20294
		[SerializeField]
		[DisableInPlayMode]
		private int _animalTypeID = -1;

		// Token: 0x04004F47 RID: 20295
		[SerializeField]
		[DisableInPlayMode]
		private BreedingTypes _breedingType;

		// Token: 0x04004F49 RID: 20297
		[SerializeField]
		private string _name = string.Empty;

		// Token: 0x04004F4B RID: 20299
		[SerializeField]
		private ItemIDKeyPair _itemID = default(ItemIDKeyPair);

		// Token: 0x04004F53 RID: 20307
		protected bool active_;

		// Token: 0x04004F54 RID: 20308
		[NonSerialized]
		public AnimalModelInfo ModelInfo = default(AnimalModelInfo);

		// Token: 0x04004F55 RID: 20309
		private int? _instanceID;

		// Token: 0x04004F5A RID: 20314
		protected EyeLookControllerVer2 eyeController;

		// Token: 0x04004F5B RID: 20315
		protected NeckLookControllerVer3 neckController;

		// Token: 0x04004F5F RID: 20319
		protected Vector3? destination;

		// Token: 0x04004F60 RID: 20320
		[ShowInInspector]
		[ReadOnly]
		private AnimalState currentState_;

		// Token: 0x04004F67 RID: 20327
		protected AnimalSchedule schedule = default(AnimalSchedule);

		// Token: 0x04004F6A RID: 20330
		[NonSerialized]
		public AnimalActionPoint actionPoint;

		// Token: 0x04004F6B RID: 20331
		protected Animator animator;

		// Token: 0x04004F70 RID: 20336
		private IDisposable inAnimDisposable;

		// Token: 0x04004F71 RID: 20337
		private IDisposable outAnimDisposable;

		// Token: 0x04004F74 RID: 20340
		protected AnimalPlayState CurrentAnimState;

		// Token: 0x04004F75 RID: 20341
		private UnityEx.ValueTuple<RuntimeAnimatorController, int, int, string> LastAnimState = default(UnityEx.ValueTuple<RuntimeAnimatorController, int, int, string>);

		// Token: 0x04004F76 RID: 20342
		protected GameObject bodyObject;

		// Token: 0x04004F77 RID: 20343
		protected Renderer[] bodyRenderers;

		// Token: 0x04004F78 RID: 20344
		protected GameObject[] bodyRendererObjects;

		// Token: 0x04004F79 RID: 20345
		protected ParticleSystemRenderer[] bodyParticleRenderers;

		// Token: 0x04004F7A RID: 20346
		protected GameObject[] bodyParticleObjects;

		// Token: 0x04004F7E RID: 20350
		protected static RaycastHit[] raycastHits = new RaycastHit[4];

		// Token: 0x04004F7F RID: 20351
		private AnimalMarker _marker;

		// Token: 0x04004F81 RID: 20353
		private LabelTypes _labelType;

		// Token: 0x04004F82 RID: 20354
		protected static CommandLabel.CommandInfo[] emptyLabels = new CommandLabel.CommandInfo[0];

		// Token: 0x04004F83 RID: 20355
		private NavMeshPath pathForCalc;

		// Token: 0x04004F86 RID: 20358
		[SerializeField]
		private ObjectLayer _layer = ObjectLayer.Command;

		// Token: 0x04004F87 RID: 20359
		[SerializeField]
		private CommandType _commandType;

		// Token: 0x04004F88 RID: 20360
		[SerializeField]
		private AnimalSearchActor _searchActor;

		// Token: 0x04004F89 RID: 20361
		[SerializeField]
		private bool _agentInsight;

		// Token: 0x04004F8A RID: 20362
		[SerializeField]
		private bool _onGroundCheck;

		// Token: 0x04004F8B RID: 20363
		[SerializeField]
		private float _labelOffsetY;

		// Token: 0x04004F8C RID: 20364
		[SerializeField]
		private bool _isCommandable;

		// Token: 0x04004F8D RID: 20365
		[SerializeField]
		protected Transform _labelPoint;

		// Token: 0x04004F8E RID: 20366
		private IDisposable everyUpdateDisposable;

		// Token: 0x04004F8F RID: 20367
		private IDisposable everyLateUpdateDisposable;

		// Token: 0x04004F90 RID: 20368
		private IDisposable everyFixedUpdateDisposable;

		// Token: 0x04004F91 RID: 20369
		[NonSerialized]
		public TimeZone timeZone = TimeZone.Morning;

		// Token: 0x04004F92 RID: 20370
		[NonSerialized]
		public Weather weather;

		// Token: 0x04004F93 RID: 20371
		public System.Action OnDestroyEvent;

		// Token: 0x04004F94 RID: 20372
		private Dictionary<string, SkinnedMeshRenderer> _skinnedMeshRendererTable = new Dictionary<string, SkinnedMeshRenderer>();
	}
}
