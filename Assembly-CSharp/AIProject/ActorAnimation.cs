using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIChara;
using IllusionUtility.GetUtility;
using Manager;
using ReMotion;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C53 RID: 3155
	public abstract class ActorAnimation : MonoBehaviour
	{
		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x002AFE9C File Offset: 0x002AE29C
		// (set) Token: 0x06006501 RID: 25857 RVA: 0x002AFEA4 File Offset: 0x002AE2A4
		public Actor Actor { get; set; }

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06006502 RID: 25858 RVA: 0x002AFEAD File Offset: 0x002AE2AD
		// (set) Token: 0x06006503 RID: 25859 RVA: 0x002AFEB5 File Offset: 0x002AE2B5
		public ActorLocomotion Character
		{
			get
			{
				return this._character;
			}
			set
			{
				this._character = value;
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x002AFEBE File Offset: 0x002AE2BE
		// (set) Token: 0x06006505 RID: 25861 RVA: 0x002AFEC6 File Offset: 0x002AE2C6
		public bool EnabledPoser { get; set; }

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06006506 RID: 25862 RVA: 0x002AFECF File Offset: 0x002AE2CF
		// (set) Token: 0x06006507 RID: 25863 RVA: 0x002AFED7 File Offset: 0x002AE2D7
		public Animator ArmAnimator { get; set; }

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06006508 RID: 25864 RVA: 0x002AFEE0 File Offset: 0x002AE2E0
		// (set) Token: 0x06006509 RID: 25865 RVA: 0x002AFEE8 File Offset: 0x002AE2E8
		public JointPoser Poser { get; protected set; }

		// Token: 0x0600650A RID: 25866
		public abstract Vector3 GetPivotPoint();

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x002AFEF1 File Offset: 0x002AE2F1
		// (set) Token: 0x0600650C RID: 25868 RVA: 0x002AFEF9 File Offset: 0x002AE2F9
		public bool IsLocomotionState { get; protected set; }

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x0600650D RID: 25869 RVA: 0x002AFF02 File Offset: 0x002AE302
		// (set) Token: 0x0600650E RID: 25870 RVA: 0x002AFF0A File Offset: 0x002AE30A
		public AssetBundleInfo AnimABInfo { get; set; }

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x0600650F RID: 25871 RVA: 0x002AFF13 File Offset: 0x002AE313
		public Queue<PlayState.Info> InStates { get; } = new Queue<PlayState.Info>();

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06006510 RID: 25872 RVA: 0x002AFF1B File Offset: 0x002AE31B
		public Queue<PlayState.Info> OutStates { get; } = new Queue<PlayState.Info>();

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x002AFF23 File Offset: 0x002AE323
		public List<PlayState.PlayStateInfo> ActionStates { get; } = new List<PlayState.PlayStateInfo>();

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x002AFF2B File Offset: 0x002AE32B
		public Queue<PlayState.Info> ActionInStates { get; } = new Queue<PlayState.Info>();

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x002AFF33 File Offset: 0x002AE333
		public Queue<PlayState.Info> ActionOutStates { get; } = new Queue<PlayState.Info>();

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06006514 RID: 25876 RVA: 0x002AFF3B File Offset: 0x002AE33B
		public List<PlayState.Info> OnceActionStates { get; } = new List<PlayState.Info>();

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06006515 RID: 25877 RVA: 0x002AFF43 File Offset: 0x002AE343
		// (set) Token: 0x06006516 RID: 25878 RVA: 0x002AFF4B File Offset: 0x002AE34B
		public ActionPointInfo ActionPointInfo { get; set; }

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06006517 RID: 25879 RVA: 0x002AFF54 File Offset: 0x002AE354
		// (set) Token: 0x06006518 RID: 25880 RVA: 0x002AFF5C File Offset: 0x002AE35C
		public DateActionPointInfo DateActionPointInfo { get; set; }

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06006519 RID: 25881 RVA: 0x002AFF65 File Offset: 0x002AE365
		// (set) Token: 0x0600651A RID: 25882 RVA: 0x002AFF6D File Offset: 0x002AE36D
		public bool RefsActAnimInfo { get; set; }

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x0600651B RID: 25883 RVA: 0x002AFF76 File Offset: 0x002AE376
		// (set) Token: 0x0600651C RID: 25884 RVA: 0x002AFF7E File Offset: 0x002AE37E
		public ActorAnimInfo AnimInfo { get; set; }

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x0600651D RID: 25885 RVA: 0x002AFF87 File Offset: 0x002AE387
		// (set) Token: 0x0600651E RID: 25886 RVA: 0x002AFF8F File Offset: 0x002AE38F
		public Transform RecoveryPoint { get; set; }

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x0600651F RID: 25887 RVA: 0x002AFF98 File Offset: 0x002AE398
		// (set) Token: 0x06006520 RID: 25888 RVA: 0x002AFFA0 File Offset: 0x002AE3A0
		public AudioSource OnceActionVoice { get; protected set; }

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06006521 RID: 25889 RVA: 0x002AFFA9 File Offset: 0x002AE3A9
		public UnityEx.ValueTuple<int, AudioSource, Transform, int> LoopActionVoice
		{
			get
			{
				return this._loopActionVoice;
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06006522 RID: 25890 RVA: 0x002AFFB1 File Offset: 0x002AE3B1
		public MotionIK MapIK
		{
			[CompilerGenerated]
			get
			{
				return this._motionIK;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06006523 RID: 25891 RVA: 0x002AFFB9 File Offset: 0x002AE3B9
		// (set) Token: 0x06006524 RID: 25892 RVA: 0x002AFFC1 File Offset: 0x002AE3C1
		public bool CanUseMapIK { get; set; } = true;

		// Token: 0x06006525 RID: 25893 RVA: 0x002AFFCC File Offset: 0x002AE3CC
		public float GetAngleFromForward(Vector3 worldDirection)
		{
			Vector3 vector = base.transform.InverseTransformDirection(worldDirection);
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x002AFFFF File Offset: 0x002AE3FF
		public AnimatorControllerParameter[] Parameters
		{
			[CompilerGenerated]
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x002B0007 File Offset: 0x002AE407
		public void SetDefaultAnimatorController(RuntimeAnimatorController rac)
		{
			this._defaultAnimController = rac;
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x002B0010 File Offset: 0x002AE410
		public void SetAnimatorController(RuntimeAnimatorController rac)
		{
			this.Animator.runtimeAnimatorController = rac;
			this._parameters = this.Animator.parameters;
			if (this._motionIK == null)
			{
				this._motionIK = new MotionIK(this.Actor.ChaControl, false, null);
				this._motionIK.MapIK = true;
			}
			if (this._ignoreAnimEvent)
			{
				return;
			}
			this._motionIK.SetMapIK(this.Animator.runtimeAnimatorController.name);
			this._nameHash = 0;
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x002B0097 File Offset: 0x002AE497
		public void ResetDefaultAnimatorController()
		{
			this.SetAnimatorController(this._defaultAnimController);
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x002B00A8 File Offset: 0x002AE4A8
		protected virtual void Start()
		{
			if (this._character != null)
			{
				base.transform.position = this._character.transform.position;
				base.transform.rotation = this._character.transform.rotation;
			}
			for (int i = 0; i < this._shapeInfo.Length; i++)
			{
				this._shapeInfo[i].MemberInit();
			}
			(from _ in this.OnAnimatorMoveAsObservable().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(Unit _)
			{
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
				this.Follow();
			});
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x002B01A0 File Offset: 0x002AE5A0
		protected virtual void OnDestroy()
		{
			this.StopAllAnimCoroutine();
			foreach (AudioSource audioSource in this._footStepAudioSources)
			{
				if (!(audioSource == null) && !(audioSource.gameObject == null) && audioSource.isPlaying)
				{
					audioSource.Stop();
				}
			}
			this.StopLoopActionVoice();
			this.StopAllActionLoopSE();
			if (this.OnceActionVoice != null && this.OnceActionVoice.gameObject != null)
			{
				Actor actor = this.Actor;
				ActorLocomotion actorLocomotion = (!(actor != null)) ? null : actor.Locomotor;
				Transform transform = (!(actorLocomotion != null)) ? null : actorLocomotion.transform;
				if (transform != null && Singleton<Voice>.IsInstance())
				{
					Singleton<Voice>.Instance.Stop(transform);
				}
				else
				{
					UnityEngine.Object.Destroy(this.OnceActionVoice.gameObject);
				}
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x002B02D4 File Offset: 0x002AE6D4
		// (set) Token: 0x0600652D RID: 25901 RVA: 0x002B02DC File Offset: 0x002AE6DC
		public string CurrentStateName { get; set; } = string.Empty;

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x0600652E RID: 25902 RVA: 0x002B02E5 File Offset: 0x002AE6E5
		public List<ProceduralTargetParameter> Targets
		{
			[CompilerGenerated]
			get
			{
				return this._targets;
			}
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x002B02F0 File Offset: 0x002AE6F0
		protected void OnUpdate()
		{
			if (this._targets != null)
			{
				AnimatorStateInfo currentAnimatorStateInfo = this.Animator.GetCurrentAnimatorStateInfo(0);
				bool flag = currentAnimatorStateInfo.IsName(this.CurrentStateName);
				bool flag2 = this.Animator.IsInTransition(0);
				if (flag && currentAnimatorStateInfo.normalizedTime < 0.9f && !flag2)
				{
					MatchTargetWeightMask weightMask = new MatchTargetWeightMask(Vector3.one, 0f);
					foreach (ProceduralTargetParameter proceduralTargetParameter in this._targets)
					{
						if (proceduralTargetParameter.Target != null)
						{
							this.Animator.MatchTarget(proceduralTargetParameter.Target.position, Quaternion.identity, AvatarTarget.Root, weightMask, proceduralTargetParameter.Start, proceduralTargetParameter.End);
						}
					}
				}
			}
			this.UpdateAnimationEvent();
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x002B03EC File Offset: 0x002AE7EC
		protected void Follow()
		{
			base.transform.position = this._character.transform.position;
			base.transform.rotation = this._character.transform.rotation;
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x002B0424 File Offset: 0x002AE824
		public float GetFloat(string parameterName)
		{
			return this.Animator.GetFloat(parameterName);
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x002B0432 File Offset: 0x002AE832
		public float GetFloat(int parameterHash)
		{
			return this.Animator.GetFloat(parameterHash);
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x002B0440 File Offset: 0x002AE840
		public void SetFloat(string parameterName, float value)
		{
			this.Animator.SetFloat(parameterName, value);
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x002B044F File Offset: 0x002AE84F
		public void SetFloat(int parameterHash, float value)
		{
			this.Animator.SetFloat(parameterHash, value);
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06006535 RID: 25909 RVA: 0x002B045E File Offset: 0x002AE85E
		// (set) Token: 0x06006536 RID: 25910 RVA: 0x002B0466 File Offset: 0x002AE866
		public Dictionary<int, List<AnimeEventInfo>> ItemEventKeyTable { get; set; }

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06006537 RID: 25911 RVA: 0x002B046F File Offset: 0x002AE86F
		// (set) Token: 0x06006538 RID: 25912 RVA: 0x002B0477 File Offset: 0x002AE877
		public Dictionary<int, List<AnimeEventInfo>> PartnerEventKeyTable { get; set; }

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06006539 RID: 25913 RVA: 0x002B0480 File Offset: 0x002AE880
		// (set) Token: 0x0600653A RID: 25914 RVA: 0x002B0488 File Offset: 0x002AE888
		public Dictionary<int, List<AnimeSEEventInfo>> SEEventKeyTable { get; set; }

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x0600653B RID: 25915 RVA: 0x002B0491 File Offset: 0x002AE891
		// (set) Token: 0x0600653C RID: 25916 RVA: 0x002B0499 File Offset: 0x002AE899
		public Dictionary<int, List<AnimeParticleEventInfo>> ParticleEventKeyTable { get; set; }

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x0600653D RID: 25917 RVA: 0x002B04A2 File Offset: 0x002AE8A2
		// (set) Token: 0x0600653E RID: 25918 RVA: 0x002B04AA File Offset: 0x002AE8AA
		public Dictionary<int, List<AnimeOnceVoiceEventInfo>> OnceVoiceEventKeyTable { get; set; }

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x0600653F RID: 25919 RVA: 0x002B04B3 File Offset: 0x002AE8B3
		// (set) Token: 0x06006540 RID: 25920 RVA: 0x002B04BB File Offset: 0x002AE8BB
		public Dictionary<int, List<int>> LoopVoiceEventKeyTable { get; set; }

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06006541 RID: 25921 RVA: 0x002B04C4 File Offset: 0x002AE8C4
		// (set) Token: 0x06006542 RID: 25922 RVA: 0x002B04CC File Offset: 0x002AE8CC
		public Dictionary<int, List<AnimeEventInfo>> ClothEventKeyTable { get; set; }

		// Token: 0x06006543 RID: 25923 RVA: 0x002B04D8 File Offset: 0x002AE8D8
		private void LoadStateEvents(Dictionary<int, List<AnimeEventInfo>> table, List<ActorAnimation.AnimatorStateEvent> list)
		{
			if (table == null)
			{
				return;
			}
			List<AnimeEventInfo> list2;
			if (!table.TryGetValue(this._nameHash, out list2))
			{
				return;
			}
			if (list2.IsNullOrEmpty<AnimeEventInfo>())
			{
				return;
			}
			foreach (AnimeEventInfo animeEventInfo in list2)
			{
				list.Add(new ActorAnimation.AnimatorStateEvent
				{
					NormalizedTime = animeEventInfo.normalizedTime,
					EventID = animeEventInfo.eventID
				});
			}
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x002B0578 File Offset: 0x002AE978
		private void LoadStateSEEvents(Dictionary<int, List<AnimeSEEventInfo>> table, List<ActorAnimation.AnimatorStateEvent_SE> list)
		{
			if (table.IsNullOrEmpty<int, List<AnimeSEEventInfo>>())
			{
				return;
			}
			List<AnimeSEEventInfo> list2;
			if (!table.TryGetValue(this._nameHash, out list2))
			{
				return;
			}
			if (list2.IsNullOrEmpty<AnimeSEEventInfo>())
			{
				return;
			}
			foreach (AnimeSEEventInfo animeSEEventInfo in list2)
			{
				ActorAnimation.AnimatorStateEvent_SE animatorStateEvent_SE = new ActorAnimation.AnimatorStateEvent_SE();
				animatorStateEvent_SE.NormalizedTime = animeSEEventInfo.NormalizedTime;
				animatorStateEvent_SE.ClipID = animeSEEventInfo.ClipID;
				animatorStateEvent_SE.EventID = animeSEEventInfo.EventID;
				ActorAnimation.AnimatorStateEvent_SE animatorStateEvent_SE2 = animatorStateEvent_SE;
				GameObject gameObject = this.Actor.ChaControl.transform.FindLoop(animeSEEventInfo.Root);
				animatorStateEvent_SE2.Root = (((gameObject != null) ? gameObject.transform : null) ?? this.Actor.Locomotor.transform);
				list.Add(animatorStateEvent_SE);
			}
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x002B0670 File Offset: 0x002AEA70
		private void LoadStateParticleEvents(Dictionary<int, List<AnimeParticleEventInfo>> table, List<ActorAnimation.AnimatorStateEvent_Particle> list)
		{
			if (table.IsNullOrEmpty<int, List<AnimeParticleEventInfo>>())
			{
				return;
			}
			List<AnimeParticleEventInfo> list2;
			if (!table.TryGetValue(this._nameHash, out list2))
			{
				return;
			}
			if (list2.IsNullOrEmpty<AnimeParticleEventInfo>())
			{
				return;
			}
			foreach (AnimeParticleEventInfo animeParticleEventInfo in list2)
			{
				list.Add(new ActorAnimation.AnimatorStateEvent_Particle
				{
					NormalizedTime = animeParticleEventInfo.NormalizedTime,
					ParticleID = animeParticleEventInfo.ParticleID,
					EventID = animeParticleEventInfo.EventID,
					Root = animeParticleEventInfo.Root,
					Used = false
				});
			}
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x002B0734 File Offset: 0x002AEB34
		private void LoadStateOnceVoiceEvents(Dictionary<int, List<AnimeOnceVoiceEventInfo>> table, List<ActorAnimation.AnimatorStateEvent_OnceVoice> list)
		{
			if (table.IsNullOrEmpty<int, List<AnimeOnceVoiceEventInfo>>())
			{
				return;
			}
			List<AnimeOnceVoiceEventInfo> list2;
			if (!table.TryGetValue(this._nameHash, out list2))
			{
				return;
			}
			if (list2.IsNullOrEmpty<AnimeOnceVoiceEventInfo>())
			{
				return;
			}
			foreach (AnimeOnceVoiceEventInfo animeOnceVoiceEventInfo in list2)
			{
				list.Add(new ActorAnimation.AnimatorStateEvent_OnceVoice
				{
					NormalizedTime = animeOnceVoiceEventInfo.NormalizedTime,
					IDs = animeOnceVoiceEventInfo.EventIDs,
					Used = false
				});
			}
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x002B07E0 File Offset: 0x002AEBE0
		private void LoadStateLoopVoiceEvents(Dictionary<int, List<int>> table, List<int> list)
		{
			if (table.IsNullOrEmpty<int, List<int>>())
			{
				return;
			}
			List<int> list2;
			if (!table.TryGetValue(this._nameHash, out list2))
			{
				return;
			}
			if (list2.IsNullOrEmpty<int>())
			{
				return;
			}
			list.AddRange(list2);
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x002B0820 File Offset: 0x002AEC20
		public void BeginIgnoreEvent()
		{
			this._ignoreAnimEvent = true;
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x002B0829 File Offset: 0x002AEC29
		public void EndIgnoreEvent()
		{
			this._ignoreAnimEvent = false;
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x002B0832 File Offset: 0x002AEC32
		public void BeginIgnoreExpression()
		{
			this._ignoreExpressionEvent = true;
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x002B083B File Offset: 0x002AEC3B
		public void EndIgnoreExpression()
		{
			this._ignoreExpressionEvent = false;
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x002B0844 File Offset: 0x002AEC44
		public void BeginIgnoreVoice()
		{
			this._ignoreVoiceEvent = true;
			this.StopLoopActionVoice();
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x002B0853 File Offset: 0x002AEC53
		public void EndIgnoreVoice()
		{
			this._ignoreVoiceEvent = false;
			if (!this.PlayEventLoopVoice())
			{
				this.StopLoopActionVoice();
			}
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x002B0870 File Offset: 0x002AEC70
		public void UpdateAnimationEvent()
		{
			if (this.Animator == null)
			{
				return;
			}
			if (this._ignoreAnimEvent)
			{
				return;
			}
			if (this.Animator.runtimeAnimatorController != null)
			{
				string name = this.Animator.runtimeAnimatorController.name;
				if (this._animatorName != name)
				{
					this._animatorName = name;
					this.LoadAnimatorYureInfo(name);
				}
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.Animator.GetCurrentAnimatorStateInfo(0);
			if (this._nameHash != currentAnimatorStateInfo.shortNameHash)
			{
				this._nameHash = currentAnimatorStateInfo.shortNameHash;
				this._prevNormalizedTime = 0f;
				this._loopCount = 0;
				this.StateItemEvents.Clear();
				this.LoadStateEvents(this.ItemEventKeyTable, this.StateItemEvents);
				this.StatePartnerEvents.Clear();
				this.LoadStateEvents(this.PartnerEventKeyTable, this.StatePartnerEvents);
				this.StateSEEvents.Clear();
				this.StopAllActionLoopSE();
				this.LoadStateSEEvents(this.SEEventKeyTable, this.StateSEEvents);
				this.LoadFootStepEventKeyTable();
				this.LoadFootStepEvents();
				this.StateParticleEvents.Clear();
				this.LoadStateParticleEvents(this.ParticleEventKeyTable, this.StateParticleEvents);
				this.StateOnceVoiceEvents.Clear();
				this.LoadStateOnceVoiceEvents(this.OnceVoiceEventKeyTable, this.StateOnceVoiceEvents);
				if (!this._ignoreExpressionEvent)
				{
					this.LoadStateExpression(this._nameHash);
				}
				this.LoadYureInfo(this._nameHash);
				this.StateLoopVoiceEvents.Clear();
				this.LoadStateLoopVoiceEvents(this.LoopVoiceEventKeyTable, this.StateLoopVoiceEvents);
				if (!this.PlayEventLoopVoice())
				{
					this.StopLoopActionVoice();
				}
				this.StateClothEvents.Clear();
				this.LoadStateEvents(this.ClothEventKeyTable, this.StateClothEvents);
				if (this._motionIK != null && this.CanUseMapIK)
				{
					this._motionIK.Calc(this._nameHash);
				}
			}
			float num = currentAnimatorStateInfo.normalizedTime;
			bool loop = currentAnimatorStateInfo.loop;
			int num2 = (int)num;
			if (loop)
			{
				num = Mathf.Repeat(num, 1f);
			}
			bool isLoop = loop && (this._loopCount < num2 || num < this._prevNormalizedTime);
			this.UpdateItemEvent(num, isLoop);
			if (!this._ignoreExpressionEvent)
			{
				this.UpdateExpressionEvent(num, isLoop);
			}
			this.UpdatePartnerEvent(num, isLoop);
			this.UpdateSEEvent(num, isLoop);
			this.UpdateFootStepEvent(currentAnimatorStateInfo, num, isLoop);
			this.UpdateParticleEvent(num, isLoop);
			if (!this._ignoreVoiceEvent)
			{
				this.UpdateVoiceEvent(num, isLoop);
			}
			this.UpdateClothEvent(num, isLoop);
			this._prevNormalizedTime = num;
			this._loopCount = num2;
			if (this._motionIK == null)
			{
				this._motionIK = new MotionIK(this.Actor.ChaControl, false, null);
				this._motionIK.MapIK = true;
			}
			else
			{
				if (this.Animator.runtimeAnimatorController != null && this._preAnimatorName != this.Animator.runtimeAnimatorController.name)
				{
					this._preAnimatorName = this.Animator.runtimeAnimatorController.name;
					this._motionIK.SetMapIK(this.Animator.runtimeAnimatorController.name);
				}
				this._motionIK.ChangeWeight(this._nameHash, currentAnimatorStateInfo);
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x0600654F RID: 25935 RVA: 0x002B0BBC File Offset: 0x002AEFBC
		// (set) Token: 0x06006550 RID: 25936 RVA: 0x002B0BC4 File Offset: 0x002AEFC4
		public Dictionary<int, YureCtrl.Info> YureTable { get; set; }

		// Token: 0x06006551 RID: 25937 RVA: 0x002B0BD0 File Offset: 0x002AEFD0
		private void LoadAnimatorYureInfo(string animatorName)
		{
			Dictionary<int, YureCtrl.Info> yureTable;
			if (Singleton<Manager.Resources>.Instance.Action.ActionYureTable.TryGetValue(animatorName, out yureTable))
			{
				this.YureTable = yureTable;
			}
			else
			{
				ChaControl chaControl = this.Actor.ChaControl;
				this.ResetYure(chaControl);
			}
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x002B0C18 File Offset: 0x002AF018
		private void LoadYureInfo(int stateHashName)
		{
			ChaControl chaControl = this.Actor.ChaControl;
			if (this.YureTable != null)
			{
				YureCtrl.Info info;
				if (this.YureTable.TryGetValue(stateHashName, out info))
				{
					bool[] aIsActive = info.aIsActive;
					for (int i = 0; i < aIsActive.Length; i++)
					{
						chaControl.playDynamicBoneBust(i, aIsActive[i]);
					}
					YureCtrl.BreastShapeInfo[] aBreastShape = info.aBreastShape;
					for (int j = 0; j < 2; j++)
					{
						YureCtrl.BreastShapeInfo breastShapeInfo = aBreastShape[j];
						YureCtrl.BreastShapeInfo breastShapeInfo2 = this._shapeInfo[j];
						for (int k = 0; k < breastShapeInfo.breast.Length; k++)
						{
							bool flag = breastShapeInfo.breast[k];
							bool flag2 = breastShapeInfo2.breast[k];
							if (flag != flag2)
							{
								if (flag)
								{
									chaControl.DisableShapeBodyID((j != 0) ? 1 : 0, k, false);
								}
								else
								{
									chaControl.DisableShapeBodyID((j != 0) ? 1 : 0, k, true);
								}
							}
							this._shapeInfo[j].breast[k] = flag;
						}
						if (breastShapeInfo.nip != breastShapeInfo2.nip)
						{
							if (breastShapeInfo.nip)
							{
								chaControl.DisableShapeBodyID((j != 0) ? 1 : 0, 7, false);
							}
							else
							{
								chaControl.DisableShapeBodyID((j != 0) ? 1 : 0, 7, false);
							}
							this._shapeInfo[j].nip = breastShapeInfo.nip;
						}
					}
				}
				return;
			}
			this.ResetYure(chaControl);
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x002B0DB8 File Offset: 0x002AF1B8
		private void ResetYure(ChaControl chara)
		{
			for (int i = 0; i < 4; i++)
			{
				chara.playDynamicBoneBust(i, true);
			}
			for (int j = 0; j < 2; j++)
			{
				YureCtrl.BreastShapeInfo breastShapeInfo = this._shapeInfo[j];
				for (int k = 0; k < ActorAnimation._defaultYure.Length; k++)
				{
					bool flag = ActorAnimation._defaultYure[k];
					bool flag2 = breastShapeInfo.breast[k];
					if (flag != flag2)
					{
						if (flag)
						{
							chara.DisableShapeBodyID((j != 0) ? 1 : 0, k, false);
						}
						else
						{
							chara.DisableShapeBodyID((j != 0) ? 1 : 0, k, true);
						}
					}
					this._shapeInfo[j].breast[k] = flag;
				}
				if (!breastShapeInfo.nip)
				{
					chara.DisableShapeBodyID((j != 0) ? 1 : 0, 7, false);
					this._shapeInfo[j].nip = true;
				}
			}
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x002B0EB8 File Offset: 0x002AF2B8
		private void UpdateItemEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent in this.StateItemEvents)
				{
					animatorStateEvent.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent2 in this.StateItemEvents)
				{
					if (animatorStateEvent2.NormalizedTime > normalizedTime && animatorStateEvent2.Used)
					{
						animatorStateEvent2.Used = false;
					}
				}
			}
			foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent3 in this.StateItemEvents)
			{
				if (animatorStateEvent3.NormalizedTime < normalizedTime && !animatorStateEvent3.Used)
				{
					animatorStateEvent3.Used = true;
					switch (animatorStateEvent3.EventID)
					{
					case 0:
					{
						Renderer[] item = this.ItemRenderers.GetElement(0).Item2;
						this.SetEnableItemRenderers(item, true);
						break;
					}
					case 1:
					{
						Renderer[] item2 = this.ItemRenderers.GetElement(0).Item2;
						this.SetEnableItemRenderers(item2, false);
						break;
					}
					case 2:
					{
						Renderer[] item3 = this.ItemRenderers.GetElement(1).Item2;
						this.SetEnableItemRenderers(item3, true);
						break;
					}
					case 3:
					{
						Renderer[] item4 = this.ItemRenderers.GetElement(1).Item2;
						this.SetEnableItemRenderers(item4, false);
						break;
					}
					}
				}
			}
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x002B10A4 File Offset: 0x002AF4A4
		private void SetEnableItemRenderers(Renderer[] renderers, bool enable)
		{
			if (renderers.IsNullOrEmpty<Renderer>())
			{
				return;
			}
			foreach (Renderer renderer in renderers)
			{
				if (!(renderer == null) && renderer.enabled != enable)
				{
					renderer.enabled = enable;
				}
			}
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x002B10FC File Offset: 0x002AF4FC
		private void UpdateClothEvent(float normalizedTime, bool isLoop)
		{
			AgentActor agentActor = this.Actor as AgentActor;
			if (agentActor == null)
			{
				return;
			}
			if (isLoop)
			{
				foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent in this.StateClothEvents)
				{
					animatorStateEvent.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent2 in this.StateClothEvents)
				{
					if (animatorStateEvent2.NormalizedTime > normalizedTime && animatorStateEvent2.Used)
					{
						animatorStateEvent2.Used = false;
					}
				}
			}
			foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent3 in this.StateClothEvents)
			{
				if (animatorStateEvent3.NormalizedTime < normalizedTime && !animatorStateEvent3.Used)
				{
					animatorStateEvent3.Used = true;
					int eventID = animatorStateEvent3.EventID;
					switch (eventID + 1)
					{
					case 0:
						agentActor.ChaControl.ChangeNowCoordinate(true, true);
						break;
					case 1:
						agentActor.ChaControl.SetClothesState(0, 0, true);
						agentActor.ChaControl.SetClothesState(1, 0, true);
						agentActor.ChaControl.SetClothesState(2, 0, true);
						agentActor.ChaControl.SetClothesState(3, 0, true);
						break;
					case 2:
						agentActor.ChaControl.SetClothesState(0, 1, true);
						agentActor.ChaControl.SetClothesState(1, 1, true);
						agentActor.ChaControl.SetClothesState(2, 1, true);
						agentActor.ChaControl.SetClothesState(3, 1, true);
						break;
					case 3:
						agentActor.ChaControl.SetClothesState(0, 2, true);
						agentActor.ChaControl.SetClothesState(1, 2, true);
						agentActor.ChaControl.SetClothesState(2, 2, true);
						agentActor.ChaControl.SetClothesState(3, 2, true);
						break;
					case 4:
						agentActor.ChaControl.SetClothesState(0, 0, true);
						agentActor.ChaControl.SetClothesState(2, 0, true);
						break;
					case 5:
						agentActor.ChaControl.SetClothesState(0, 1, true);
						agentActor.ChaControl.SetClothesState(2, 1, true);
						break;
					case 6:
						agentActor.ChaControl.SetClothesState(0, 2, true);
						agentActor.ChaControl.SetClothesState(2, 2, true);
						break;
					case 7:
						agentActor.ChaControl.SetClothesState(1, 0, true);
						agentActor.ChaControl.SetClothesState(3, 0, true);
						agentActor.ChaControl.SetClothesState(5, 0, true);
						break;
					case 8:
						agentActor.ChaControl.SetClothesState(1, 1, true);
						agentActor.ChaControl.SetClothesState(3, 1, true);
						agentActor.ChaControl.SetClothesState(5, 1, true);
						break;
					case 9:
						agentActor.ChaControl.SetClothesState(1, 2, true);
						agentActor.ChaControl.SetClothesState(3, 2, true);
						agentActor.ChaControl.SetClothesState(5, 2, true);
						break;
					case 10:
					{
						string bathCoordinateFileName = agentActor.AgentData.BathCoordinateFileName;
						if (!bathCoordinateFileName.IsNullOrEmpty())
						{
							agentActor.ChaControl.ChangeNowCoordinate(agentActor.AgentData.BathCoordinateFileName, true, true);
						}
						else
						{
							agentActor.ChaControl.ChangeNowCoordinate(Singleton<Manager.Resources>.Instance.BathDefaultCoord, true, true);
						}
						break;
					}
					case 11:
					{
						string nowCoordinateFileName = agentActor.AgentData.NowCoordinateFileName;
						if (!nowCoordinateFileName.IsNullOrEmpty())
						{
							agentActor.ChaControl.ChangeNowCoordinate(nowCoordinateFileName, true, true);
						}
						else
						{
							agentActor.ChaControl.ChangeNowCoordinate(true, true);
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x002B1500 File Offset: 0x002AF900
		protected virtual void UpdateExpressionEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.ExpressionKeyframeEvent expressionKeyframeEvent in this.ExpressionKeyframeList)
				{
					expressionKeyframeEvent.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.ExpressionKeyframeEvent expressionKeyframeEvent2 in this.ExpressionKeyframeList)
				{
					if (expressionKeyframeEvent2.NormalizedTime > normalizedTime && expressionKeyframeEvent2.Used)
					{
						expressionKeyframeEvent2.Used = false;
					}
				}
			}
			int id = this.Actor.ID;
			foreach (ActorAnimation.ExpressionKeyframeEvent expressionKeyframeEvent3 in this.ExpressionKeyframeList)
			{
				if (expressionKeyframeEvent3.NormalizedTime < normalizedTime && !expressionKeyframeEvent3.Used)
				{
					expressionKeyframeEvent3.Used = true;
					Game.Expression expression = Singleton<Game>.Instance.GetExpression(id, expressionKeyframeEvent3.Name);
					if (expression != null)
					{
						expression.Change(this.Actor.ChaControl);
					}
				}
			}
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x002B166C File Offset: 0x002AFA6C
		private void UpdatePartnerEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent in this.StatePartnerEvents)
				{
					animatorStateEvent.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent2 in this.StatePartnerEvents)
				{
					if (animatorStateEvent2.NormalizedTime < normalizedTime && animatorStateEvent2.Used)
					{
						animatorStateEvent2.Used = false;
					}
				}
			}
			foreach (ActorAnimation.AnimatorStateEvent animatorStateEvent3 in this.StatePartnerEvents)
			{
				if (animatorStateEvent3.NormalizedTime < normalizedTime && !animatorStateEvent3.Used)
				{
					animatorStateEvent3.Used = true;
					int eventID = animatorStateEvent3.EventID;
					if (eventID != 0)
					{
					}
				}
			}
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x002B17B8 File Offset: 0x002AFBB8
		private void UpdateSEEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.AnimatorStateEvent_SE animatorStateEvent_SE in this.StateSEEvents)
				{
					animatorStateEvent_SE.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.AnimatorStateEvent_SE animatorStateEvent_SE2 in this.StateSEEvents)
				{
					if (normalizedTime < animatorStateEvent_SE2.NormalizedTime && animatorStateEvent_SE2.Used)
					{
						animatorStateEvent_SE2.Used = false;
					}
				}
			}
			using (List<ActorAnimation.AnimatorStateEvent_SE>.Enumerator enumerator3 = this.StateSEEvents.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ActorAnimation.AnimatorStateEvent_SE e = enumerator3.Current;
					if (e.NormalizedTime < normalizedTime && !e.Used)
					{
						e.Used = true;
						int clipID = e.ClipID;
						if (clipID != -1)
						{
							int eventID = e.EventID;
							if (eventID < 0)
							{
								Observable.EveryLateUpdate().Take(1).Subscribe(delegate(long _)
								{
									if (e.Root == null)
									{
										return;
									}
									if (!Singleton<Map>.IsInstance())
									{
										return;
									}
									if (!Singleton<Manager.Resources>.IsInstance())
									{
										return;
									}
									PlayerActor player = Singleton<Map>.Instance.Player;
									Transform transform;
									if (player == null)
									{
										transform = null;
									}
									else
									{
										ActorCameraControl cameraControl = player.CameraControl;
										if (cameraControl == null)
										{
											transform = null;
										}
										else
										{
											Camera cameraComponent = cameraControl.CameraComponent;
											transform = ((cameraComponent != null) ? cameraComponent.transform : null);
										}
									}
									Transform transform2 = transform;
									if (transform2 == null)
									{
										return;
									}
									SoundPack soundPack2 = Singleton<Manager.Resources>.Instance.SoundPack;
									SoundPack.Data3D data3D2;
									if (!soundPack2.TryGetActionSEData(e.ClipID, out data3D2))
									{
										return;
									}
									float num = data3D2.MaxDistance + soundPack2.Game3DInfo.MarginMaxDistance;
									Vector3 vector = e.Root.position - transform2.position;
									if (num * num < vector.sqrMagnitude)
									{
										return;
									}
									AudioSource audioSource = soundPack2.Play(data3D2, Sound.Type.GameSE3D, 0f);
									if (audioSource != null)
									{
										audioSource.Stop();
										audioSource.transform.position = e.Root.position;
										audioSource.transform.rotation = e.Root.rotation;
										audioSource.Play();
									}
								});
							}
							else
							{
								int key = eventID / 2;
								int clipID2 = e.ClipID;
								Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> dictionary2;
								UnityEx.ValueTuple<AudioSource, FadePlayer> pair2;
								if (eventID % 2 == 0)
								{
									Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> dictionary;
									bool flag = this.ActionLoopSETable.TryGetValue(key, out dictionary) && dictionary != null;
									if (!flag)
									{
										dictionary = (this.ActionLoopSETable[key] = new Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>());
									}
									if (flag)
									{
										UnityEx.ValueTuple<AudioSource, FadePlayer> pair = default(UnityEx.ValueTuple<AudioSource, FadePlayer>);
										flag = dictionary.TryGetValue(clipID2, out pair);
										if (flag)
										{
											flag = (pair.Item1 != null && pair.Item1.isPlaying);
											if (!flag)
											{
												this.StopActionLoopSE(pair);
												dictionary.Remove(clipID2);
											}
										}
									}
									if (!flag)
									{
										if (e.Root == null)
										{
											break;
										}
										if (!Singleton<Map>.IsInstance())
										{
											break;
										}
										if (!Singleton<Manager.Resources>.IsInstance())
										{
											break;
										}
										SoundPack soundPack = Singleton<Manager.Resources>.Instance.SoundPack;
										SoundPack.Data3D data3D;
										if (!soundPack.TryGetActionSEData(clipID2, out data3D))
										{
											break;
										}
										AudioSource audio = soundPack.Play(data3D, Sound.Type.GameSE3D, 0f);
										if (audio != null)
										{
											FadePlayer component = audio.GetComponent<FadePlayer>();
											audio.Stop();
											Transform root = e.Root;
											audio.transform.position = root.position;
											audio.transform.rotation = root.rotation;
											audio.loop = true;
											audio.Play();
											dictionary[clipID2] = new UnityEx.ValueTuple<AudioSource, FadePlayer>(audio, component);
											audio.LateUpdateAsObservable().TakeUntilDestroy(root).Subscribe(delegate(Unit __)
											{
												audio.transform.SetPositionAndRotation(root.position, root.rotation);
											});
										}
									}
								}
								else if (this.ActionLoopSETable.TryGetValue(key, out dictionary2) && dictionary2.TryGetValue(clipID2, out pair2))
								{
									this.StopActionLoopSE(pair2);
									dictionary2.Remove(clipID2);
								}
							}
						}
						else
						{
							ActionPoint currentPoint = this.Actor.CurrentPoint;
							if (currentPoint != null)
							{
								DoorAnimation component2 = currentPoint.GetComponent<DoorAnimation>();
								if (component2 != null)
								{
									component2.PlayMoveSE(true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x002B1C0C File Offset: 0x002B000C
		private void UpdateParticleEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.AnimatorStateEvent_Particle animatorStateEvent_Particle in this.StateParticleEvents)
				{
					animatorStateEvent_Particle.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.AnimatorStateEvent_Particle animatorStateEvent_Particle2 in this.StateParticleEvents)
				{
					if (normalizedTime < animatorStateEvent_Particle2.NormalizedTime && animatorStateEvent_Particle2.Used)
					{
						animatorStateEvent_Particle2.Used = false;
					}
				}
			}
			foreach (ActorAnimation.AnimatorStateEvent_Particle animatorStateEvent_Particle3 in this.StateParticleEvents)
			{
				if (animatorStateEvent_Particle3.NormalizedTime <= normalizedTime && !animatorStateEvent_Particle3.Used)
				{
					animatorStateEvent_Particle3.Used = true;
					Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>> dictionary;
					if (this.Particles.TryGetValue(animatorStateEvent_Particle3.EventID / 2, out dictionary))
					{
						Tuple<GameObject, ParticleSystem, ParticleSystemRenderer> tuple;
						if (dictionary.TryGetValue(animatorStateEvent_Particle3.ParticleID, out tuple))
						{
							if (!(tuple.Item2 == null))
							{
								ParticleSystem item = tuple.Item2;
								bool flag = animatorStateEvent_Particle3.EventID % 2 == 0;
								if (item.isPlaying)
								{
									item.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
								}
								if (flag)
								{
									item.Play(true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x002B1DC8 File Offset: 0x002B01C8
		private void UpdateVoiceEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.AnimatorStateEvent_OnceVoice animatorStateEvent_OnceVoice in this.StateOnceVoiceEvents)
				{
					animatorStateEvent_OnceVoice.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.AnimatorStateEvent_OnceVoice animatorStateEvent_OnceVoice2 in this.StateOnceVoiceEvents)
				{
					if (normalizedTime < animatorStateEvent_OnceVoice2.NormalizedTime && animatorStateEvent_OnceVoice2.Used)
					{
						animatorStateEvent_OnceVoice2.Used = false;
					}
				}
			}
			foreach (ActorAnimation.AnimatorStateEvent_OnceVoice animatorStateEvent_OnceVoice3 in this.StateOnceVoiceEvents)
			{
				if (animatorStateEvent_OnceVoice3.NormalizedTime <= normalizedTime && !animatorStateEvent_OnceVoice3.Used)
				{
					animatorStateEvent_OnceVoice3.Used = true;
					if (!animatorStateEvent_OnceVoice3.IDs.IsNullOrEmpty<int>())
					{
						int element = animatorStateEvent_OnceVoice3.IDs.GetElement(UnityEngine.Random.Range(0, animatorStateEvent_OnceVoice3.IDs.Length));
						this.PlayEventOnceVoice(element);
					}
				}
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x002B1F30 File Offset: 0x002B0330
		// (set) Token: 0x0600655D RID: 25949 RVA: 0x002B1F38 File Offset: 0x002B0338
		public List<ActorAnimation.AnimatorStateEvent> StateItemEvents { get; set; } = new List<ActorAnimation.AnimatorStateEvent>();

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x002B1F41 File Offset: 0x002B0341
		// (set) Token: 0x0600655F RID: 25951 RVA: 0x002B1F49 File Offset: 0x002B0349
		public List<ActorAnimation.AnimatorStateEvent> StatePartnerEvents { get; set; } = new List<ActorAnimation.AnimatorStateEvent>();

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x002B1F52 File Offset: 0x002B0352
		// (set) Token: 0x06006561 RID: 25953 RVA: 0x002B1F5A File Offset: 0x002B035A
		public List<ActorAnimation.AnimatorStateEvent_SE> StateSEEvents { get; set; } = new List<ActorAnimation.AnimatorStateEvent_SE>();

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06006562 RID: 25954 RVA: 0x002B1F63 File Offset: 0x002B0363
		// (set) Token: 0x06006563 RID: 25955 RVA: 0x002B1F6B File Offset: 0x002B036B
		public List<ActorAnimation.AnimatorStateEvent_Particle> StateParticleEvents { get; set; } = new List<ActorAnimation.AnimatorStateEvent_Particle>();

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06006564 RID: 25956 RVA: 0x002B1F74 File Offset: 0x002B0374
		// (set) Token: 0x06006565 RID: 25957 RVA: 0x002B1F7C File Offset: 0x002B037C
		public List<ActorAnimation.AnimatorStateEvent_OnceVoice> StateOnceVoiceEvents { get; set; } = new List<ActorAnimation.AnimatorStateEvent_OnceVoice>();

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x002B1F85 File Offset: 0x002B0385
		// (set) Token: 0x06006567 RID: 25959 RVA: 0x002B1F8D File Offset: 0x002B038D
		public List<int> StateLoopVoiceEvents { get; set; } = new List<int>();

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06006568 RID: 25960 RVA: 0x002B1F96 File Offset: 0x002B0396
		// (set) Token: 0x06006569 RID: 25961 RVA: 0x002B1F9E File Offset: 0x002B039E
		public List<ActorAnimation.AnimatorStateEvent> StateClothEvents { get; set; } = new List<ActorAnimation.AnimatorStateEvent>();

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x0600656A RID: 25962 RVA: 0x002B1FA7 File Offset: 0x002B03A7
		// (set) Token: 0x0600656B RID: 25963 RVA: 0x002B1FAF File Offset: 0x002B03AF
		public List<ActorAnimation.ExpressionKeyframeEvent> ExpressionKeyframeList { get; set; } = new List<ActorAnimation.ExpressionKeyframeEvent>();

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x002B1FB8 File Offset: 0x002B03B8
		// (set) Token: 0x0600656D RID: 25965 RVA: 0x002B1FC0 File Offset: 0x002B03C0
		public List<UnityEx.ValueTuple<int, GameObject>> Items { get; private set; } = new List<UnityEx.ValueTuple<int, GameObject>>();

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x0600656E RID: 25966 RVA: 0x002B1FC9 File Offset: 0x002B03C9
		// (set) Token: 0x0600656F RID: 25967 RVA: 0x002B1FD1 File Offset: 0x002B03D1
		public List<UnityEx.ValueTuple<int, Renderer[]>> ItemRenderers { get; private set; } = new List<UnityEx.ValueTuple<int, Renderer[]>>();

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x002B1FDA File Offset: 0x002B03DA
		// (set) Token: 0x06006571 RID: 25969 RVA: 0x002B1FE2 File Offset: 0x002B03E2
		public Dictionary<int, ItemAnimInfo> ItemAnimatorTable { get; private set; } = new Dictionary<int, ItemAnimInfo>();

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x002B1FEB File Offset: 0x002B03EB
		// (set) Token: 0x06006573 RID: 25971 RVA: 0x002B1FF3 File Offset: 0x002B03F3
		public Dictionary<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>> ActionLoopSETable { get; private set; } = new Dictionary<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>>();

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x002B1FFC File Offset: 0x002B03FC
		// (set) Token: 0x06006575 RID: 25973 RVA: 0x002B2004 File Offset: 0x002B0404
		public Dictionary<int, Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>> Particles { get; private set; } = new Dictionary<int, Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>>();

		// Token: 0x06006576 RID: 25974 RVA: 0x002B200D File Offset: 0x002B040D
		public void LoadEventVarious(int eventID, int poseID, PlayState info)
		{
			this.LoadEventKeyTable(eventID, poseID);
			this.Actor.LoadEventItems(info);
			this.Actor.LoadEventParticles(eventID, poseID);
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x002B2030 File Offset: 0x002B0430
		public void LoadAnimatorIfNotEquals(PlayState info)
		{
			bool flag = false;
			foreach (PlayState.Info info2 in info.MainStateInfo.InStateInfo.StateInfos)
			{
				int stateID = Animator.StringToHash(info2.stateName);
				if (!this.Animator.HasState(info2.layer, stateID))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				AssetBundleInfo assetBundleInfo = info.MainStateInfo.AssetBundleInfo;
				if (assetBundleInfo.asset != this.Animator.runtimeAnimatorController.name)
				{
					RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo.assetbundle, assetBundleInfo.asset, string.Empty);
					if (runtimeAnimatorController != null)
					{
						this.SetAnimatorController(runtimeAnimatorController);
					}
				}
			}
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x002B2108 File Offset: 0x002B0508
		public ActorAnimInfo SetAnimInfo(PlayState info)
		{
			this.AnimInfo = new ActorAnimInfo
			{
				inEnableBlend = info.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = info.MainStateInfo.InStateInfo.FadeSecond,
				inFadeOutTime = info.MainStateInfo.FadeOutTime,
				outEnableBlend = info.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = info.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = info.DirectionType,
				endEnableBlend = info.EndEnableBlend,
				endBlendSec = info.EndBlendRate,
				isLoop = info.MainStateInfo.IsLoop,
				loopMinTime = info.MainStateInfo.LoopMin,
				loopMaxTime = info.MainStateInfo.LoopMax,
				hasAction = info.ActionInfo.hasAction,
				loopStateName = info.MainStateInfo.InStateInfo.StateInfos.GetElement(info.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName,
				randomCount = info.ActionInfo.randomCount,
				oldNormalizedTime = 0f,
				layer = info.MainStateInfo.InStateInfo.StateInfos[0].layer
			};
			return this.AnimInfo;
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x002B2285 File Offset: 0x002B0685
		public ActorAnimInfo LoadActionState(int eventID, int poseID, PlayState info)
		{
			this.LoadEventVarious(eventID, poseID, info);
			this.InitializeStates(info);
			this.LoadAnimatorIfNotEquals(info);
			return this.SetAnimInfo(info);
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x002B22A8 File Offset: 0x002B06A8
		public void EnableItems()
		{
			foreach (UnityEx.ValueTuple<int, GameObject> valueTuple in this.Items)
			{
				if (valueTuple.Item2 != null)
				{
					valueTuple.Item2.SetActive(true);
				}
			}
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x002B231C File Offset: 0x002B071C
		public void DisableItems()
		{
			foreach (UnityEx.ValueTuple<int, GameObject> valueTuple in this.Items)
			{
				if (valueTuple.Item2 != null)
				{
					valueTuple.Item2.SetActive(false);
				}
			}
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x002B2390 File Offset: 0x002B0790
		public void ClearItems()
		{
			this.ItemRenderers.Clear();
			this.ItemAnimatorTable.Clear();
			foreach (UnityEx.ValueTuple<int, GameObject> valueTuple in this.Items)
			{
				if (valueTuple.Item2 != null)
				{
					UnityEngine.Object.Destroy(valueTuple.Item2);
				}
			}
			this.Items.Clear();
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x002B2424 File Offset: 0x002B0824
		public void EnableParticleRenderer()
		{
			foreach (KeyValuePair<int, Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>> keyValuePair in this.Particles)
			{
				if (keyValuePair.Value != null)
				{
					foreach (KeyValuePair<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>> keyValuePair2 in keyValuePair.Value)
					{
						ParticleSystem item = keyValuePair2.Value.Item2;
						if (item != null && item.isPlaying)
						{
							item.Pause(true);
						}
						ParticleSystemRenderer item2 = keyValuePair2.Value.Item3;
						if (item2 != null && !item2.enabled)
						{
							item2.enabled = true;
						}
					}
				}
			}
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x002B2530 File Offset: 0x002B0930
		public void DisableParticleRenderer()
		{
			foreach (KeyValuePair<int, Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>> keyValuePair in this.Particles)
			{
				if (keyValuePair.Value != null)
				{
					foreach (KeyValuePair<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>> keyValuePair2 in keyValuePair.Value)
					{
						ParticleSystem item = keyValuePair2.Value.Item2;
						if (item != null && item.isPaused)
						{
							item.Play(true);
						}
						ParticleSystemRenderer item2 = keyValuePair2.Value.Item3;
						if (item2 != null && item2.enabled)
						{
							item2.enabled = false;
						}
					}
				}
			}
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x002B263C File Offset: 0x002B0A3C
		public void ClearParticles()
		{
			if (!this.Particles.IsNullOrEmpty<int, Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>>())
			{
				foreach (KeyValuePair<int, Dictionary<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>> keyValuePair in this.Particles)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>>())
					{
						foreach (KeyValuePair<int, Tuple<GameObject, ParticleSystem, ParticleSystemRenderer>> keyValuePair2 in keyValuePair.Value)
						{
							Tuple<GameObject, ParticleSystem, ParticleSystemRenderer> value = keyValuePair2.Value;
							if (value != null)
							{
								GameObject item = value.Item1;
								if (!(item == null))
								{
									UnityEngine.Object.Destroy(item);
								}
							}
						}
					}
				}
				this.Particles.Clear();
			}
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x002B2740 File Offset: 0x002B0B40
		public virtual void StopLoopActionVoice()
		{
			if (this._loopActionVoice.Item2 == null || this._loopActionVoice.Item2.gameObject == null)
			{
				return;
			}
			if (this._loopActionVoice.Item3 != null)
			{
				Singleton<Voice>.Instance.Stop(this._loopActionVoice.Item4, this._loopActionVoice.Item3);
			}
			else
			{
				UnityEngine.Object.Destroy(this._loopActionVoice.Item2.gameObject);
			}
			this._loopActionVoice.Item1 = -1;
			this._loopActionVoice.Item2 = null;
			this._loopActionVoice.Item3 = null;
			this._loopActionVoice.Item4 = -1;
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x002B2800 File Offset: 0x002B0C00
		private void StopActionLoopSE(UnityEx.ValueTuple<AudioSource, FadePlayer> pair)
		{
			if (pair.Item2 != null && pair.Item2.gameObject != null)
			{
				if (Singleton<Manager.Resources>.IsInstance())
				{
					pair.Item2.Stop(Singleton<Manager.Resources>.Instance.SoundPack.Game3DInfo.StopFadeTime);
				}
				else
				{
					pair.Item2.Stop(0.5f);
				}
			}
			else if (pair.Item1 != null && pair.Item1.gameObject != null)
			{
				if (Singleton<Sound>.IsInstance())
				{
					Singleton<Sound>.Instance.Stop(Sound.Type.GameSE3D, pair.Item2.transform);
				}
				else
				{
					UnityEngine.Object.Destroy(pair.Item1.gameObject);
				}
			}
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x002B28DC File Offset: 0x002B0CDC
		private void StopAllActionLoopSE()
		{
			if (this.ActionLoopSETable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>>())
			{
				return;
			}
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>> keyValuePair in this.ActionLoopSETable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>())
				{
					foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> keyValuePair2 in keyValuePair.Value)
					{
						this.StopActionLoopSE(keyValuePair2.Value);
					}
				}
			}
			this.ActionLoopSETable.Clear();
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x002B29B8 File Offset: 0x002B0DB8
		private void LoadFootStepEvents()
		{
			this._prevFootStepInfo = null;
			this._footStepEvents.Clear();
			if (this._footStepEventKeyTable == null)
			{
				return;
			}
			FootStepInfo[] array;
			if (!this._footStepEventKeyTable.TryGetValue(this._nameHash, out array) || array.IsNullOrEmpty<FootStepInfo>())
			{
				return;
			}
			foreach (FootStepInfo info in array)
			{
				this._footStepEvents.Add(new FootStepInfo(info));
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06006584 RID: 25988 RVA: 0x002B2A32 File Offset: 0x002B0E32
		// (set) Token: 0x06006585 RID: 25989 RVA: 0x002B2A3A File Offset: 0x002B0E3A
		public bool IsFootStepActive
		{
			get
			{
				return this._isFootStepActive;
			}
			set
			{
				this._prevFootStepInfo = null;
				this._isFootStepActive = value;
			}
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x002B2A4C File Offset: 0x002B0E4C
		private void UpdateFootStepEvent(AnimatorStateInfo stateInfo, float normalizedTime, bool isLoop)
		{
			if (!this._isFootStepActive)
			{
				return;
			}
			if (this._footStepEvents.IsNullOrEmpty<FootStepInfo>())
			{
				return;
			}
			string forwardMove = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.ForwardMove;
			float @float = this.GetFloat(forwardMove);
			FootStepInfo footStepInfo = null;
			foreach (FootStepInfo footStepInfo2 in this._footStepEvents)
			{
				if (footStepInfo2.Threshold.IsRange(@float))
				{
					footStepInfo = footStepInfo2;
					break;
				}
			}
			if (footStepInfo == null || footStepInfo.Keys.IsNullOrEmpty<FootStepInfo.Key>())
			{
				return;
			}
			if (footStepInfo != this._prevFootStepInfo)
			{
				for (int i = 0; i < footStepInfo.Keys.Length; i++)
				{
					footStepInfo.Keys[i].Execute = (footStepInfo.Keys[i].Time < normalizedTime);
				}
				this._prevFootStepInfo = footStepInfo;
			}
			else if (isLoop)
			{
				for (int j = 0; j < footStepInfo.Keys.Length; j++)
				{
					footStepInfo.Keys[j].Execute = false;
				}
			}
			else
			{
				for (int k = 0; k < footStepInfo.Keys.Length; k++)
				{
					FootStepInfo.Key key = footStepInfo.Keys[k];
					if (normalizedTime < key.Time && key.Execute)
					{
						footStepInfo.Keys[k].Execute = false;
					}
				}
			}
			bool flag = false;
			for (int l = 0; l < footStepInfo.Keys.Length; l++)
			{
				FootStepInfo.Key key2 = footStepInfo.Keys[l];
				if (key2.Time <= normalizedTime && !key2.Execute)
				{
					footStepInfo.Keys[l].Execute = true;
					flag = true;
				}
			}
			if (flag)
			{
				this._footStepAudioSources.RemoveAll((AudioSource x) => x == null || x.gameObject == null || !x.isPlaying);
				Vector3 position = this.Actor.Position;
				Quaternion rotation = this.Actor.Rotation;
				Map instance = Singleton<Map>.Instance;
				ActorCameraControl cameraControl = instance.Player.CameraControl;
				float num = Vector3.Distance(position, cameraControl.transform.position);
				SoundPack.FootStepInfoGroup footStepInfo3 = Singleton<Manager.Resources>.Instance.SoundPack.FootStepInfo;
				if (num <= footStepInfo3.PlayEnableDistance)
				{
					AudioSource audioSource = null;
					bool isBareFoot = this.Actor.ChaControl.IsBareFoot;
					byte sex = this.Actor.ChaControl.sex;
					Vector3 origin = position + Vector3.up * 15f;
					LayerMask selayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.SELayer;
					Ray ray = new Ray(origin, Vector3.down);
					int num2 = Physics.RaycastNonAlloc(ray, ActorAnimation._raycastHits, 50f, selayer);
					Weather weather = instance.Simulator.Weather;
					SoundPack.PlayAreaType areaType = SoundPack.PlayAreaType.Normal;
					MapArea mapArea;
					if (0 < num2)
					{
						float num3 = float.MaxValue;
						string text = null;
						for (int m = 0; m < num2; m++)
						{
							RaycastHit raycastHit = ActorAnimation._raycastHits[m];
							Collider collider = raycastHit.collider;
							GameObject gameObject = (collider != null) ? collider.gameObject : null;
							if (!(gameObject == null))
							{
								float distance = raycastHit.distance;
								if (distance < num3)
								{
									text = gameObject.tag;
									num3 = distance;
								}
							}
						}
						if (!text.IsNullOrEmpty())
						{
							audioSource = Singleton<Manager.Resources>.Instance.SoundPack.PlayFootStep(sex, isBareFoot, text, weather, areaType);
						}
					}
					else if ((mapArea = this.Actor.MapArea) != null)
					{
						audioSource = Singleton<Manager.Resources>.Instance.SoundPack.PlayFootStep(sex, isBareFoot, instance.MapID, mapArea.AreaID, weather, areaType);
					}
					if (audioSource != null)
					{
						audioSource.Stop();
						audioSource.transform.SetPositionAndRotation(position, rotation);
						FadePlayer component = audioSource.GetComponent<FadePlayer>();
						if (component != null)
						{
							component.currentVolume = audioSource.volume;
						}
						audioSource.rolloffMode = footStepInfo3.RolloffMode;
						audioSource.minDistance = footStepInfo3.MinDistance;
						audioSource.maxDistance = footStepInfo3.MaxDistance;
						this._footStepAudioSources.Add(audioSource);
						audioSource.Play();
					}
				}
			}
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x002B2F04 File Offset: 0x002B1304
		public void InitializeStates(PlayState info)
		{
			if (info == null)
			{
				this.InStates.Clear();
				this.OutStates.Clear();
				this.ActionStates.Clear();
				return;
			}
			this.InStates.Clear();
			if (!info.MainStateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in info.MainStateInfo.InStateInfo.StateInfos)
				{
					this.InStates.Enqueue(item);
				}
			}
			this.OutStates.Clear();
			if (!info.MainStateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item2 in info.MainStateInfo.OutStateInfo.StateInfos)
				{
					this.OutStates.Enqueue(item2);
				}
			}
			this.ActionStates.Clear();
			if (!info.SubStateInfos.IsNullOrEmpty<PlayState.PlayStateInfo>())
			{
				foreach (PlayState.PlayStateInfo item3 in info.SubStateInfos)
				{
					this.ActionStates.Add(item3);
				}
			}
			this.AnimABInfo = info.MainStateInfo.AssetBundleInfo;
			this._maskState = info.MaskStateInfo;
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x002B3098 File Offset: 0x002B1498
		public void InitializeStates(PlayState.Info[] inStateInfos, PlayState.Info[] outStateInfos, AssetBundleInfo animABInfo)
		{
			this.InStates.Clear();
			if (!inStateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in inStateInfos)
				{
					this.InStates.Enqueue(item);
				}
			}
			this.OutStates.Clear();
			if (!outStateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item2 in outStateInfos)
				{
					this.OutStates.Enqueue(item2);
				}
			}
			this.AnimABInfo = animABInfo;
			this.ActionStates.Clear();
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x002B314C File Offset: 0x002B154C
		public void EndStates()
		{
			ActorAnimInfo animInfo = this.AnimInfo;
			if (!animInfo.endEnableBlend)
			{
				this.CrossFadeScreen(-1f);
			}
			if (this.Animator.runtimeAnimatorController.name != this._defaultAnimController.name)
			{
				this.ResetDefaultAnimatorController();
			}
			this.Actor.SetStand(this.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			this.RecoveryPoint = null;
			this.RefsActAnimInfo = true;
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x002B31D6 File Offset: 0x002B15D6
		public void StopAllAnimCoroutine()
		{
			this.StopInLocoAnimCoroutine();
			this.StopInAnimCoroutine();
			this.StopOutAnimCoroutine();
			this.StopActionAnimCoroutine();
			this.StopOnceActionAnimCoroutine();
			this.StopTurnAnimCoroutine();
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x002B31FC File Offset: 0x002B15FC
		public void PlayInLocoAnimation(bool enableFade, float fadeTime, int layer)
		{
			IEnumerator enumerator = this._inLocoAnimEnumerator = this.StartInLocoAnimation(enableFade, fadeTime, layer);
			this._inLocoAnimDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x002B3244 File Offset: 0x002B1644
		public void StopInLocoAnimCoroutine()
		{
			if (this._inLocoAnimDisposable != null)
			{
				this._inLocoAnimDisposable.Dispose();
				this._inLocoAnimEnumerator = null;
			}
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x002B3264 File Offset: 0x002B1664
		private IEnumerator StartInLocoAnimation(bool enableFade, float fadeTime, int layer)
		{
			Animator animator = this.Animator;
			if (!enableFade)
			{
				this.CrossFadeScreen(-1f);
			}
			for (int i = 1; i < animator.layerCount; i++)
			{
				animator.SetLayerWeight(i, 0f);
			}
			if (this._maskState.layer > 0)
			{
				animator.SetLayerWeight(this._maskState.layer, 1f);
				if (enableFade)
				{
					this.CrossFadeAnimation(this._maskState.stateName, fadeTime, this._maskState.layer, 0f);
				}
				else
				{
					this.PlayAnimation(this._maskState.stateName, this._maskState.layer, 0f);
				}
			}
			Queue<PlayState.Info> queue = this.InStates;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				if (enableFade)
				{
					this.CrossFadeAnimation(state.stateName, fadeTime, layer, 0f);
					this.CrossFadeItemAnimation(state.stateName, fadeTime, layer);
					this.CrossFadeHousingActionPointAnimation(state.stateName, fadeTime, layer);
					IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)fadeTime)).Publish<long>();
					waiter.Connect();
					yield return waiter.ToYieldInstruction<long>();
				}
				else
				{
					this.PlayAnimation(state.stateName, layer, 0f);
					this.PlayItemAnimation(state.stateName);
					this.PlayHousingActionPointAnimation(state.stateName);
					yield return null;
				}
				if (queue.Count == 0)
				{
					break;
				}
				this.CurrentStateName = state.stateName;
				yield return null;
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				bool isInTransition = animator.IsInTransition(state.layer);
				while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
				{
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					yield return null;
				}
				this.CurrentStateName = string.Empty;
				yield return null;
			}
			this.CurrentStateName = string.Empty;
			this._inLocoAnimEnumerator = null;
			yield return null;
			yield break;
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x002B3294 File Offset: 0x002B1694
		public void PlayInAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			IEnumerator enumerator = this._inAnimEnumerator = this.StartInAnimation(enableFade, fadeTime, fadeOutTime, layer);
			this._inAnimDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x002B32DE File Offset: 0x002B16DE
		public void StopInAnimCoroutine()
		{
			if (this._inAnimDisposable != null)
			{
				this._inAnimDisposable.Dispose();
				this._inAnimEnumerator = null;
			}
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x002B3300 File Offset: 0x002B1700
		private IEnumerator StartInAnimation(bool enableFade, float fadeTime, float fadeOutTime, int layer)
		{
			if (!enableFade)
			{
				this.CrossFadeScreen(-1f);
			}
			Queue<PlayState.Info> queue = this.InStates;
			Animator animator = this.Animator;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				yield return this.PlayState(animator, state, enableFade, fadeTime, layer, fadeOutTime);
			}
			yield return null;
			this._inAnimEnumerator = null;
			yield break;
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x002B3338 File Offset: 0x002B1738
		public void PlayOutAnimation(bool enableFade, float fadeTime, int layer)
		{
			IEnumerator enumerator = this._outAnimEnumerator = this.StartOutAnimation(enableFade, fadeTime, layer);
			this._outAnimDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x002B3380 File Offset: 0x002B1780
		public void StopOutAnimCoroutine()
		{
			if (this._outAnimDisposable != null)
			{
				this._outAnimDisposable.Dispose();
				this._outAnimEnumerator = null;
			}
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x002B33A0 File Offset: 0x002B17A0
		private IEnumerator StartOutAnimation(bool enableFade, float fadeTime, int layer)
		{
			Queue<PlayState.Info> queue = this.OutStates;
			Animator animator = this.Animator;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				yield return this.PlayState(animator, state, enableFade, fadeTime, layer, 1f);
			}
			yield return null;
			this._outAnimEnumerator = null;
			yield break;
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x002B33D0 File Offset: 0x002B17D0
		public void PlayActionAnimation(int layer)
		{
			IEnumerator enumerator = this._actionAnimEnumerator = this.StartActionAnimation(layer);
			this._actionAnimDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x002B3418 File Offset: 0x002B1818
		private IEnumerator StartActionAnimation(int layer)
		{
			PlayState.PlayStateInfo stateInfo = this.ActionStates[UnityEngine.Random.Range(0, this.ActionStates.Count)];
			Animator animator = this.Animator;
			AnimatorStateInfo prevState = animator.GetCurrentAnimatorStateInfo(layer);
			this.ActionInStates.Clear();
			if (!stateInfo.InStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item in stateInfo.InStateInfo.StateInfos)
				{
					this.ActionInStates.Enqueue(item);
				}
			}
			this.ActionOutStates.Clear();
			if (!stateInfo.OutStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				foreach (PlayState.Info item2 in stateInfo.OutStateInfo.StateInfos)
				{
					this.ActionOutStates.Enqueue(item2);
				}
			}
			yield return this.StartActionInAnimation(stateInfo.InStateInfo.EnableFade, stateInfo.InStateInfo.FadeSecond, layer);
			float elapsedTime = 0f;
			int loopDuration = UnityEngine.Random.Range(stateInfo.LoopMin, stateInfo.LoopMax);
			while ((elapsedTime += Time.deltaTime) < (float)loopDuration)
			{
				yield return null;
			}
			yield return this.StartActionOutAnimation(stateInfo.OutStateInfo.EnableFade, stateInfo.OutStateInfo.FadeSecond, layer);
			if (stateInfo.OutStateInfo.EnableFade)
			{
				this.CrossFadeAnimation(prevState.shortNameHash, stateInfo.OutStateInfo.FadeSecond, layer, 0f);
			}
			else
			{
				this.PlayAnimation(prevState.shortNameHash, layer, 0f);
			}
			yield return null;
			this._actionAnimEnumerator = null;
			yield break;
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x002B343C File Offset: 0x002B183C
		private IEnumerator StartActionInAnimation(bool enableFade, float fadeTime, int layer)
		{
			Queue<PlayState.Info> queue = this.ActionInStates;
			Animator animator = this.Animator;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				yield return this.PlayState(animator, state, enableFade, fadeTime, layer, 1f);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x002B346C File Offset: 0x002B186C
		private IEnumerator StartActionOutAnimation(bool enableFade, float fadeTime, int layer)
		{
			Queue<PlayState.Info> queue = this.ActionOutStates;
			Animator animator = this.Animator;
			while (queue.Count > 0)
			{
				PlayState.Info state = queue.Dequeue();
				yield return this.PlayState(animator, state, enableFade, fadeTime, layer, 1f);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x002B349C File Offset: 0x002B189C
		public void StopActionAnimCoroutine()
		{
			if (this._actionAnimDisposable != null)
			{
				this._actionAnimDisposable.Dispose();
				this._actionAnimEnumerator = null;
			}
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x002B34BC File Offset: 0x002B18BC
		public void PlayOnceActionAnimation(bool enableFade, float fadeTime, int layer)
		{
			IEnumerator enumerator = this._onceActionAnimEnumerator = this.StartOnceActionAnimation(enableFade, fadeTime, layer);
			this._onceActionAnimDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x002B3504 File Offset: 0x002B1904
		private IEnumerator StartOnceActionAnimation(bool enableFade, float fadeTime, int layer)
		{
			PlayState.Info state = this.OnceActionStates[UnityEngine.Random.Range(0, this.OnceActionStates.Count)];
			Animator animator = this.Animator;
			AnimatorStateInfo prevState = animator.GetCurrentAnimatorStateInfo(state.layer);
			if (enableFade)
			{
				this.CrossFadeAnimation(state.stateName, fadeTime, layer, 0f);
				IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)fadeTime)).Publish<long>();
				waiter.Connect();
				yield return waiter.ToYieldInstruction<long>();
			}
			else
			{
				this.CrossFadeScreen(-1f);
				this.PlayAnimation(state.stateName, state.layer, 0f);
				yield return null;
			}
			bool isInTransition = animator.IsInTransition(layer);
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
			while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
			{
				isInTransition = animator.IsInTransition(layer);
				stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				yield return null;
			}
			animator.Play(prevState.shortNameHash, state.layer, 0f);
			yield return null;
			this._onceActionAnimEnumerator = null;
			yield break;
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x002B3534 File Offset: 0x002B1934
		public void StopOnceActionAnimCoroutine()
		{
			if (this._onceActionAnimDisposable != null)
			{
				this._onceActionAnimDisposable.Dispose();
				this._onceActionAnimEnumerator = null;
			}
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x002B3554 File Offset: 0x002B1954
		public void PlayTurnAnimation(Vector3 to, float fadeOutTime, PlayState.AnimStateInfo recoverStateInfo, bool isFast = false)
		{
			IEnumerator enumerator = this._turnAnimEnumerator = this.StartTurnAnimation(to, fadeOutTime, recoverStateInfo, false);
			this._turnAnimDisposable = Observable.FromCoroutine(() => enumerator, false).Subscribe<Unit>();
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x002B35A0 File Offset: 0x002B19A0
		private IEnumerator StartTurnAnimation(Vector3 to, float fadeOutTime, PlayState.AnimStateInfo recoverStateInfo, bool isFast = false)
		{
			yield return Observable.EveryLateUpdate().Skip(1).Take(1).ToYieldInstruction<long>();
			float currentAngle = -this.GetAngleFromForward(this.Actor.Controller.transform.forward);
			Vector3 from = this.Actor.Position;
			from.y = 0f;
			to.y = 0f;
			float toAngle = this.GetAngleFromForward(Vector3.Normalize(to - from));
			float angle = toAngle - currentAngle;
			Animator animator = this.Animator;
			string direction = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.DirectionParameterName;
			float minAngle = Singleton<Manager.Resources>.Instance.AgentProfile.TurnMinAngle;
			Quaternion destRotation = Quaternion.LookRotation(to - from);
			if (Mathf.Abs(angle) > minAngle)
			{
				float inverse = Mathf.InverseLerp(-180f, 180f, angle);
				inverse = Mathf.Lerp(-1f, 1f, inverse);
				float invAngle = Mathf.Clamp(inverse, -1f, 1f);
				animator.SetFloat(direction, invAngle);
				DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
				PlayState.AnimStateInfo turn = definePack.AnimatorState.TurnStateInfo;
				if (isFast)
				{
					animator.speed = 2f;
				}
				animator.CrossFadeInFixedTime(turn.StateInfos[0].stateName, turn.FadeSecond, turn.StateInfos[0].layer, 0f);
				string stateName = turn.StateInfos[0].stateName;
				IObservable<long> timer = Observable.Timer(TimeSpan.FromSeconds(0.10000000149011612));
				yield return timer.ToYieldInstruction<long>();
				bool isInTransition = animator.IsInTransition(0);
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
				bool isTurn = stateInfo.IsName(stateName);
				float normalizedTime = stateInfo.normalizedTime;
				while (isInTransition || (isTurn && normalizedTime < fadeOutTime))
				{
					isInTransition = animator.IsInTransition(0);
					stateInfo = animator.GetCurrentAnimatorStateInfo(0);
					isTurn = stateInfo.IsName(stateName);
					normalizedTime = stateInfo.normalizedTime;
					yield return null;
				}
			}
			Quaternion startRotation = this.Actor.Rotation;
			ObservableEasing.Linear(0.3f, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				Quaternion rotation = Quaternion.Lerp(startRotation, destRotation, x.Value);
				this.Actor.Rotation = rotation;
			});
			animator.speed = 1f;
			if (recoverStateInfo != null && !recoverStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				this._recoveryStateFromTurn.Clear();
				foreach (PlayState.Info item in recoverStateInfo.StateInfos)
				{
					this._recoveryStateFromTurn.Enqueue(item);
				}
				float blendTime = recoverStateInfo.FadeSecond;
				while (this._recoveryStateFromTurn.Count > 0)
				{
					PlayState.Info state = this._recoveryStateFromTurn.Dequeue();
					if (recoverStateInfo.EnableFade)
					{
						this.CrossFadeAnimation(state.stateName, blendTime, state.layer, 0f);
						IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)blendTime)).Publish<long>();
						waiter.Connect();
						yield return waiter.ToYieldInstruction<long>();
					}
					else
					{
						this.PlayAnimation(state.stateName, state.layer, 0f);
						yield return null;
					}
					if (this._recoveryStateFromTurn.Count == 0)
					{
						break;
					}
					yield return null;
					AnimatorStateInfo stateInfo2 = animator.GetCurrentAnimatorStateInfo(state.layer);
					bool isInTransition2 = animator.IsInTransition(state.layer);
					while (isInTransition2 || (stateInfo2.IsName(state.stateName) && stateInfo2.normalizedTime < 1f))
					{
						stateInfo2 = animator.GetCurrentAnimatorStateInfo(state.layer);
						isInTransition2 = animator.IsInTransition(state.layer);
						yield return null;
					}
				}
			}
			animator.SetFloat(direction, 0f);
			this._turnAnimEnumerator = null;
			yield break;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x002B35D8 File Offset: 0x002B19D8
		public void PlayTurnAnimation(float angle, float fadeOutTime, PlayState.AnimStateInfo recoverStateInfo)
		{
			this._turnAnimEnumerator = this.StartTurnAnimation(angle, fadeOutTime, recoverStateInfo);
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x002B35EC File Offset: 0x002B19EC
		private IEnumerator StartTurnAnimation(float angle, float fadeOutTime, PlayState.AnimStateInfo recoverStateInfo)
		{
			float inverse = Mathf.InverseLerp(-180f, 180f, angle);
			inverse = Mathf.Lerp(-1f, 1f, angle);
			float invAngle = Mathf.Clamp(inverse, -1f, 1f);
			string direction = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.DirectionParameterName;
			Animator animator = this.Animator;
			animator.SetFloat(direction, invAngle);
			PlayState.AnimStateInfo turn = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.TurnStateInfo;
			animator.CrossFadeInFixedTime(turn.StateInfos[0].stateName, turn.FadeSecond, turn.StateInfos[0].layer, 0f);
			string stateName = turn.StateInfos[0].stateName;
			IObservable<long> timer = Observable.Timer(TimeSpan.FromSeconds((double)turn.FadeSecond));
			yield return timer.ToYieldInstruction<long>();
			bool isInTransition = animator.IsInTransition(0);
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			bool isTurn = stateInfo.IsName(stateName);
			float normalizedTime = stateInfo.normalizedTime;
			while (isInTransition || (isTurn && normalizedTime < fadeOutTime))
			{
				isInTransition = animator.IsInTransition(0);
				stateInfo = animator.GetCurrentAnimatorStateInfo(0);
				isTurn = stateInfo.IsName(stateName);
				normalizedTime = stateInfo.normalizedTime;
				yield return null;
			}
			if (!recoverStateInfo.StateInfos.IsNullOrEmpty<PlayState.Info>())
			{
				Queue<PlayState.Info> queue = QueuePool<AIProject.PlayState.Info>.Get();
				foreach (PlayState.Info item in recoverStateInfo.StateInfos)
				{
					queue.Enqueue(item);
				}
				float blendTime = recoverStateInfo.FadeSecond;
				while (queue.Count > 0)
				{
					PlayState.Info state = queue.Dequeue();
					if (recoverStateInfo.EnableFade)
					{
						this.CrossFadeAnimation(state.stateName, blendTime, state.layer, 0f);
						IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)blendTime)).Publish<long>();
						waiter.Connect();
						yield return waiter.ToYieldInstruction<long>();
					}
					else
					{
						this.PlayAnimation(state.stateName, state.layer, 0f);
						yield return null;
					}
					if (queue.Count == 0)
					{
						break;
					}
					yield return null;
					stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
					isInTransition = animator.IsInTransition(state.layer);
					while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < 1f))
					{
						stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
						isInTransition = animator.IsInTransition(state.layer);
						yield return null;
					}
				}
				QueuePool<AIProject.PlayState.Info>.Release(queue);
			}
			animator.SetFloat(direction, 0f);
			this._turnAnimEnumerator = null;
			yield break;
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x002B361C File Offset: 0x002B1A1C
		public void StopTurnAnimCoroutine()
		{
			if (this._turnAnimDisposable != null)
			{
				this._turnAnimDisposable.Dispose();
				this._turnAnimEnumerator = null;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060065A1 RID: 26017 RVA: 0x002B363B File Offset: 0x002B1A3B
		public bool PlayingInLocoAnimation
		{
			get
			{
				return this._inLocoAnimEnumerator != null;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x002B3649 File Offset: 0x002B1A49
		public bool PlayingInAnimation
		{
			get
			{
				return this._inAnimEnumerator != null;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x002B3657 File Offset: 0x002B1A57
		public bool PlayingOutAnimation
		{
			get
			{
				return this._outAnimEnumerator != null;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060065A4 RID: 26020 RVA: 0x002B3665 File Offset: 0x002B1A65
		public bool PlayingActAnimation
		{
			get
			{
				return this._actionAnimEnumerator != null;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x002B3673 File Offset: 0x002B1A73
		public bool PlayingTurnAnimation
		{
			get
			{
				return this._turnAnimEnumerator != null;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060065A6 RID: 26022 RVA: 0x002B3681 File Offset: 0x002B1A81
		public bool PlayingOnceActionAnimation
		{
			get
			{
				return this._onceActionAnimEnumerator != null;
			}
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x002B3690 File Offset: 0x002B1A90
		public virtual void PlayAnimation(string stateName, int layer, float normalizedTime)
		{
			Animator animator = this.Animator;
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			if (!this._parameters.IsNullOrEmpty<AnimatorControllerParameter>())
			{
				foreach (AnimatorControllerParameter animatorControllerParameter in this._parameters)
				{
					if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
					{
						float shapeBodyValue = this.Actor.ChaControl.GetShapeBodyValue(0);
						if (animator != null)
						{
							animator.SetFloat(heightParameterName, shapeBodyValue);
						}
					}
				}
			}
			this.PlayAnimation(animator, stateName, layer, normalizedTime);
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x002B3744 File Offset: 0x002B1B44
		public virtual void PlayAnimation(int stateNameHash, int layer, float normalizedTime)
		{
			Animator animator = this.Animator;
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			foreach (AnimatorControllerParameter animatorControllerParameter in animator.parameters)
			{
				if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float shapeBodyValue = this.Actor.ChaControl.GetShapeBodyValue(0);
					if (animator != null)
					{
						animator.SetFloat(heightParameterName, shapeBodyValue);
					}
				}
			}
			if (animator != null)
			{
				animator.Play(stateNameHash, layer, normalizedTime);
			}
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x002B37F0 File Offset: 0x002B1BF0
		public void PlayAnimation(Animator animator, string stateName, int layer, float normalizedTime)
		{
			if (!(base.gameObject != null) || global::Debug.isDebugBuild)
			{
			}
			if (animator != null)
			{
				animator.Play(stateName, layer, normalizedTime);
			}
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x002B3824 File Offset: 0x002B1C24
		public virtual void CrossFadeAnimation(string stateName, float fadeTime, int layer, float fixedTimeOffset)
		{
			Animator animator = this.Animator;
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			foreach (AnimatorControllerParameter animatorControllerParameter in this._parameters)
			{
				if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float shapeBodyValue = this.Actor.ChaControl.GetShapeBodyValue(0);
					if (animator != null)
					{
						animator.SetFloat(heightParameterName, shapeBodyValue);
					}
				}
			}
			this.CrossFadeAnimation(animator, stateName, fadeTime, layer, fixedTimeOffset);
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x002B38C8 File Offset: 0x002B1CC8
		public virtual void CrossFadeAnimation(int stateNameHash, float fadeTime, int layer, float fixedTimeOffset)
		{
			Animator animator = this.Animator;
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			foreach (AnimatorControllerParameter animatorControllerParameter in this._parameters)
			{
				if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float shapeBodyValue = this.Actor.ChaControl.GetShapeBodyValue(0);
					if (animator != null)
					{
						animator.SetFloat(heightParameterName, shapeBodyValue);
					}
				}
			}
			if (animator != null)
			{
				animator.CrossFadeInFixedTime(stateNameHash, fadeTime, layer, fixedTimeOffset, 0f);
			}
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x002B397B File Offset: 0x002B1D7B
		public void CrossFadeAnimation(Animator animator, string stateName, float fadeTime, int layer, float fixedTimeOffset)
		{
			if (global::Debug.isDebugBuild)
			{
			}
			if (animator != null)
			{
				animator.CrossFadeInFixedTime(stateName, fadeTime, layer, fixedTimeOffset, 0f);
			}
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x002B39A4 File Offset: 0x002B1DA4
		private IEnumerator PlayState(Animator animator, PlayState.Info state, bool enableFade, float fadeTime, int layer, float fadeOutTime)
		{
			string heightName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
			if (enableFade)
			{
				this.CrossFadeAnimation(state.stateName, fadeTime, layer, 0f);
				this.CrossFadeItemAnimation(state.stateName, fadeTime, layer);
				this.CrossFadeHousingActionPointAnimation(state.stateName, fadeTime, layer);
				IConnectableObservable<long> waiter = Observable.Timer(TimeSpan.FromSeconds((double)fadeTime)).Publish<long>();
				waiter.Connect();
				yield return waiter.ToYieldInstruction<long>();
			}
			else
			{
				this.PlayAnimation(state.stateName, layer, 0f);
				this.PlayItemAnimation(state.stateName);
				this.PlayHousingActionPointAnimation(state.stateName);
				yield return null;
			}
			this.CurrentStateName = state.stateName;
			this.LoadMatchTargetInfo(state.stateName);
			yield return null;
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
			bool isInTransition = animator.IsInTransition(state.layer);
			while (isInTransition || (stateInfo.IsName(state.stateName) && stateInfo.normalizedTime < fadeOutTime))
			{
				if (animator == null)
				{
					yield break;
				}
				stateInfo = animator.GetCurrentAnimatorStateInfo(state.layer);
				isInTransition = animator.IsInTransition(state.layer);
				yield return null;
			}
			this.CurrentStateName = string.Empty;
			this.Targets.Clear();
			yield return null;
			yield break;
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x002B39EC File Offset: 0x002B1DEC
		protected virtual void CrossFadeItemAnimation(string stateName, float fadeTime, int layer)
		{
			string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
			foreach (KeyValuePair<int, ItemAnimInfo> keyValuePair in this.ItemAnimatorTable)
			{
				Animator animator = keyValuePair.Value.Animator;
				if (keyValuePair.Value.Sync)
				{
					foreach (AnimatorControllerParameter animatorControllerParameter in keyValuePair.Value.Parameters)
					{
						if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
						{
							float shapeBodyValue = this.Actor.ChaControl.GetShapeBodyValue(0);
							animator.SetFloat(heightParameterName, shapeBodyValue);
						}
					}
					this.CrossFadeAnimation(animator, stateName, fadeTime, layer, 0f);
				}
			}
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x002B3AEC File Offset: 0x002B1EEC
		private void CrossFadeHousingActionPointAnimation(string stateName, float fadeTime, int layer)
		{
			if (this.Actor.CurrentPoint == null)
			{
				return;
			}
			HousingActionPointAnimation[] componentsInChildren = this.Actor.CurrentPoint.GetComponentsInChildren<HousingActionPointAnimation>();
			if (!componentsInChildren.IsNullOrEmpty<HousingActionPointAnimation>())
			{
				foreach (HousingActionPointAnimation housingActionPointAnimation in componentsInChildren)
				{
					if (!(housingActionPointAnimation.Animator == null))
					{
						this.CrossFadeAnimation(housingActionPointAnimation.Animator, stateName, fadeTime, layer, 0f);
					}
				}
			}
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x002B3B70 File Offset: 0x002B1F70
		protected virtual void PlayItemAnimation(string stateName)
		{
			string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
			foreach (KeyValuePair<int, ItemAnimInfo> keyValuePair in this.ItemAnimatorTable)
			{
				Animator animator = keyValuePair.Value.Animator;
				if (keyValuePair.Value.Sync)
				{
					foreach (AnimatorControllerParameter animatorControllerParameter in keyValuePair.Value.Parameters)
					{
						if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
						{
							float shapeBodyValue = this.Actor.ChaControl.GetShapeBodyValue(0);
							animator.SetFloat(heightParameterName, shapeBodyValue);
						}
					}
					this.PlayAnimation(animator, stateName, 0, 0f);
				}
			}
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x002B3C70 File Offset: 0x002B2070
		private void PlayHousingActionPointAnimation(string stateName)
		{
			if (this.Actor.CurrentPoint == null)
			{
				return;
			}
			HousingActionPointAnimation[] componentsInChildren = this.Actor.CurrentPoint.GetComponentsInChildren<HousingActionPointAnimation>();
			if (!componentsInChildren.IsNullOrEmpty<HousingActionPointAnimation>())
			{
				foreach (HousingActionPointAnimation housingActionPointAnimation in componentsInChildren)
				{
					if (!(housingActionPointAnimation.Animator == null))
					{
						this.PlayAnimation(housingActionPointAnimation.Animator, stateName, 0, 0f);
					}
				}
			}
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x002B3CF3 File Offset: 0x002B20F3
		protected virtual void LoadMatchTargetInfo(string stateName)
		{
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x002B3CF5 File Offset: 0x002B20F5
		protected virtual void LoadStateLocomotionVoice(int stateHashName)
		{
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x002B3CF8 File Offset: 0x002B20F8
		protected virtual void LoadStateExpression(int stateHashName)
		{
			Manager.Resources.ActionTable action = Singleton<Manager.Resources>.Instance.Action;
			this.ExpressionKeyframeList.Clear();
			int id = this.Actor.ID;
			Dictionary<int, List<ExpressionKeyframe>> dictionary;
			List<ExpressionKeyframe> list;
			Dictionary<int, string> dictionary2;
			string key;
			if (Singleton<Manager.Resources>.Instance.Action.ActionExpressionKeyframeTable.TryGetValue(id, out dictionary) && dictionary.TryGetValue(stateHashName, out list))
			{
				foreach (ExpressionKeyframe expressionKeyframe in list)
				{
					this.ExpressionKeyframeList.Add(new ActorAnimation.ExpressionKeyframeEvent
					{
						NormalizedTime = expressionKeyframe.normalizedTime,
						Name = expressionKeyframe.expressionName
					});
				}
			}
			else if (Singleton<Manager.Resources>.Instance.Action.ActionExpressionTable.TryGetValue(id, out dictionary2) && dictionary2.TryGetValue(stateHashName, out key))
			{
				Game.Expression expression = Singleton<Game>.Instance.GetExpression(id, key);
				if (expression != null)
				{
					expression.Change(this.Actor.ChaControl);
				}
			}
		}

		// Token: 0x060065B5 RID: 26037
		public abstract void LoadEventKeyTable(int eventID, int poseID);

		// Token: 0x060065B6 RID: 26038
		public abstract void LoadSEEventKeyTable(int eventID, int poseID);

		// Token: 0x060065B7 RID: 26039 RVA: 0x002B3E20 File Offset: 0x002B2220
		public virtual void LoadAnimalEventKeyTable(int animalTypeID, int poseID)
		{
		}

		// Token: 0x060065B8 RID: 26040
		protected abstract void LoadFootStepEventKeyTable();

		// Token: 0x060065B9 RID: 26041
		public abstract void LoadParticleEventKeyTable(int eventID, int poseID);

		// Token: 0x060065BA RID: 26042 RVA: 0x002B3E22 File Offset: 0x002B2222
		public virtual void LoadOnceVoiceEventKeyTable(int eventID, int poseID)
		{
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x002B3E24 File Offset: 0x002B2224
		public virtual void LoadLoopVoiceEventKeyTable(int eventID, int poseID)
		{
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x002B3E26 File Offset: 0x002B2226
		protected virtual void PlayEventOnceVoice(int voiceID)
		{
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x002B3E28 File Offset: 0x002B2228
		protected virtual bool PlayEventLoopVoice()
		{
			return !this._ignoreVoiceEvent && !this._ignoreAnimEvent && (!(this.OnceActionVoice != null) || !this.OnceActionVoice.isPlaying) && !this.StateLoopVoiceEvents.IsNullOrEmpty<int>();
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x002B3E84 File Offset: 0x002B2284
		public void CrossFadeScreen(float time = -1f)
		{
			bool isVisibleInCamera = this.Actor.ChaControl.IsVisibleInCamera;
			if (isVisibleInCamera)
			{
				ActorCameraControl cameraControl = Singleton<Map>.Instance.Player.CameraControl;
				float num = Vector3.Distance(this.Actor.Position, cameraControl.transform.position);
				float crossFadeEnableDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.CrossFadeEnableDistance;
				if (num <= crossFadeEnableDistance)
				{
					cameraControl.CrossFade.FadeStart(time);
				}
			}
		}

		// Token: 0x040057C5 RID: 22469
		[SerializeField]
		protected ActorLocomotion _character;

		// Token: 0x040057C6 RID: 22470
		public Animator Animator;

		// Token: 0x040057C7 RID: 22471
		private RuntimeAnimatorController _defaultAnimController;

		// Token: 0x040057CF RID: 22479
		private PlayState.Info _maskState = default(PlayState.Info);

		// Token: 0x040057DA RID: 22490
		protected UnityEx.ValueTuple<int, AudioSource, Transform, int> _loopActionVoice = new UnityEx.ValueTuple<int, AudioSource, Transform, int>(-1, null, null, -1);

		// Token: 0x040057DB RID: 22491
		protected MotionIK _motionIK;

		// Token: 0x040057DD RID: 22493
		protected string _preAnimatorName = string.Empty;

		// Token: 0x040057DE RID: 22494
		protected AnimatorControllerParameter[] _parameters;

		// Token: 0x040057E0 RID: 22496
		protected List<ProceduralTargetParameter> _targets = new List<ProceduralTargetParameter>();

		// Token: 0x040057E8 RID: 22504
		private float _prevNormalizedTime;

		// Token: 0x040057E9 RID: 22505
		private int _loopCount;

		// Token: 0x040057EA RID: 22506
		private bool _ignoreAnimEvent;

		// Token: 0x040057EB RID: 22507
		private bool _ignoreExpressionEvent;

		// Token: 0x040057EC RID: 22508
		private bool _ignoreVoiceEvent;

		// Token: 0x040057EE RID: 22510
		private static readonly bool[] _defaultYure = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x040057EF RID: 22511
		private const bool _defaultNip = true;

		// Token: 0x040057F0 RID: 22512
		private YureCtrl.BreastShapeInfo[] _shapeInfo = new YureCtrl.BreastShapeInfo[2];

		// Token: 0x040057F1 RID: 22513
		private string _animatorName = string.Empty;

		// Token: 0x040057F2 RID: 22514
		private int _nameHash;

		// Token: 0x04005800 RID: 22528
		private bool _isFootStepActive = true;

		// Token: 0x04005801 RID: 22529
		private FootStepInfo _prevFootStepInfo;

		// Token: 0x04005802 RID: 22530
		private List<FootStepInfo> _footStepEvents = new List<FootStepInfo>();

		// Token: 0x04005803 RID: 22531
		protected Dictionary<int, FootStepInfo[]> _footStepEventKeyTable = new Dictionary<int, FootStepInfo[]>();

		// Token: 0x04005804 RID: 22532
		private List<AudioSource> _footStepAudioSources = new List<AudioSource>();

		// Token: 0x04005805 RID: 22533
		protected static RaycastHit[] _raycastHits = new RaycastHit[3];

		// Token: 0x04005806 RID: 22534
		private Queue<PlayState.Info> _recoveryStateFromTurn = new Queue<PlayState.Info>();

		// Token: 0x04005807 RID: 22535
		private IEnumerator _inLocoAnimEnumerator;

		// Token: 0x04005808 RID: 22536
		private IDisposable _inLocoAnimDisposable;

		// Token: 0x04005809 RID: 22537
		private IEnumerator _inAnimEnumerator;

		// Token: 0x0400580A RID: 22538
		private IDisposable _inAnimDisposable;

		// Token: 0x0400580B RID: 22539
		private IEnumerator _outAnimEnumerator;

		// Token: 0x0400580C RID: 22540
		private IDisposable _outAnimDisposable;

		// Token: 0x0400580D RID: 22541
		private IEnumerator _actionAnimEnumerator;

		// Token: 0x0400580E RID: 22542
		private IDisposable _actionAnimDisposable;

		// Token: 0x0400580F RID: 22543
		private IEnumerator _turnAnimEnumerator;

		// Token: 0x04005810 RID: 22544
		private IDisposable _turnAnimDisposable;

		// Token: 0x04005811 RID: 22545
		private IEnumerator _onceActionAnimEnumerator;

		// Token: 0x04005812 RID: 22546
		private IDisposable _onceActionAnimDisposable;

		// Token: 0x02000C54 RID: 3156
		public class AnimatorStateEvent
		{
			// Token: 0x1700145D RID: 5213
			// (get) Token: 0x060065C6 RID: 26054 RVA: 0x002B3F6D File Offset: 0x002B236D
			// (set) Token: 0x060065C7 RID: 26055 RVA: 0x002B3F75 File Offset: 0x002B2375
			public float NormalizedTime { get; set; }

			// Token: 0x1700145E RID: 5214
			// (get) Token: 0x060065C8 RID: 26056 RVA: 0x002B3F7E File Offset: 0x002B237E
			// (set) Token: 0x060065C9 RID: 26057 RVA: 0x002B3F86 File Offset: 0x002B2386
			public int EventID { get; set; }

			// Token: 0x1700145F RID: 5215
			// (get) Token: 0x060065CA RID: 26058 RVA: 0x002B3F8F File Offset: 0x002B238F
			// (set) Token: 0x060065CB RID: 26059 RVA: 0x002B3F97 File Offset: 0x002B2397
			public bool Used { get; set; }
		}

		// Token: 0x02000C55 RID: 3157
		public class ExpressionKeyframeEvent
		{
			// Token: 0x17001460 RID: 5216
			// (get) Token: 0x060065CD RID: 26061 RVA: 0x002B3FA8 File Offset: 0x002B23A8
			// (set) Token: 0x060065CE RID: 26062 RVA: 0x002B3FB0 File Offset: 0x002B23B0
			public float NormalizedTime { get; set; }

			// Token: 0x17001461 RID: 5217
			// (get) Token: 0x060065CF RID: 26063 RVA: 0x002B3FB9 File Offset: 0x002B23B9
			// (set) Token: 0x060065D0 RID: 26064 RVA: 0x002B3FC1 File Offset: 0x002B23C1
			public string Name { get; set; }

			// Token: 0x17001462 RID: 5218
			// (get) Token: 0x060065D1 RID: 26065 RVA: 0x002B3FCA File Offset: 0x002B23CA
			// (set) Token: 0x060065D2 RID: 26066 RVA: 0x002B3FD2 File Offset: 0x002B23D2
			public bool Used { get; set; }
		}

		// Token: 0x02000C56 RID: 3158
		public class AnimatorStateEvent_SE
		{
			// Token: 0x17001463 RID: 5219
			// (get) Token: 0x060065D4 RID: 26068 RVA: 0x002B3FF1 File Offset: 0x002B23F1
			// (set) Token: 0x060065D5 RID: 26069 RVA: 0x002B3FF9 File Offset: 0x002B23F9
			public float NormalizedTime { get; set; }

			// Token: 0x17001464 RID: 5220
			// (get) Token: 0x060065D6 RID: 26070 RVA: 0x002B4002 File Offset: 0x002B2402
			// (set) Token: 0x060065D7 RID: 26071 RVA: 0x002B400A File Offset: 0x002B240A
			public int ClipID { get; set; } = -1;

			// Token: 0x17001465 RID: 5221
			// (get) Token: 0x060065D8 RID: 26072 RVA: 0x002B4013 File Offset: 0x002B2413
			// (set) Token: 0x060065D9 RID: 26073 RVA: 0x002B401B File Offset: 0x002B241B
			public int EventID { get; set; } = -1;

			// Token: 0x17001466 RID: 5222
			// (get) Token: 0x060065DA RID: 26074 RVA: 0x002B4024 File Offset: 0x002B2424
			// (set) Token: 0x060065DB RID: 26075 RVA: 0x002B402C File Offset: 0x002B242C
			public Transform Root { get; set; }

			// Token: 0x17001467 RID: 5223
			// (get) Token: 0x060065DC RID: 26076 RVA: 0x002B4035 File Offset: 0x002B2435
			// (set) Token: 0x060065DD RID: 26077 RVA: 0x002B403D File Offset: 0x002B243D
			public bool Used { get; set; }
		}

		// Token: 0x02000C57 RID: 3159
		public class AnimatorStateEvent_Particle
		{
			// Token: 0x17001468 RID: 5224
			// (get) Token: 0x060065DF RID: 26079 RVA: 0x002B4067 File Offset: 0x002B2467
			// (set) Token: 0x060065E0 RID: 26080 RVA: 0x002B406F File Offset: 0x002B246F
			public float NormalizedTime { get; set; } = float.MaxValue;

			// Token: 0x17001469 RID: 5225
			// (get) Token: 0x060065E1 RID: 26081 RVA: 0x002B4078 File Offset: 0x002B2478
			// (set) Token: 0x060065E2 RID: 26082 RVA: 0x002B4080 File Offset: 0x002B2480
			public int ParticleID { get; set; } = -1;

			// Token: 0x1700146A RID: 5226
			// (get) Token: 0x060065E3 RID: 26083 RVA: 0x002B4089 File Offset: 0x002B2489
			// (set) Token: 0x060065E4 RID: 26084 RVA: 0x002B4091 File Offset: 0x002B2491
			public int EventID { get; set; } = -1;

			// Token: 0x1700146B RID: 5227
			// (get) Token: 0x060065E5 RID: 26085 RVA: 0x002B409A File Offset: 0x002B249A
			// (set) Token: 0x060065E6 RID: 26086 RVA: 0x002B40A2 File Offset: 0x002B24A2
			public string Root { get; set; }

			// Token: 0x1700146C RID: 5228
			// (get) Token: 0x060065E7 RID: 26087 RVA: 0x002B40AB File Offset: 0x002B24AB
			// (set) Token: 0x060065E8 RID: 26088 RVA: 0x002B40B3 File Offset: 0x002B24B3
			public bool Used { get; set; }
		}

		// Token: 0x02000C58 RID: 3160
		public class AnimatorStateEvent_OnceVoice
		{
			// Token: 0x1700146D RID: 5229
			// (get) Token: 0x060065EA RID: 26090 RVA: 0x002B40CF File Offset: 0x002B24CF
			// (set) Token: 0x060065EB RID: 26091 RVA: 0x002B40D7 File Offset: 0x002B24D7
			public float NormalizedTime { get; set; } = float.MaxValue;

			// Token: 0x1700146E RID: 5230
			// (get) Token: 0x060065EC RID: 26092 RVA: 0x002B40E0 File Offset: 0x002B24E0
			// (set) Token: 0x060065ED RID: 26093 RVA: 0x002B40E8 File Offset: 0x002B24E8
			public int[] IDs { get; set; }

			// Token: 0x1700146F RID: 5231
			// (get) Token: 0x060065EE RID: 26094 RVA: 0x002B40F1 File Offset: 0x002B24F1
			// (set) Token: 0x060065EF RID: 26095 RVA: 0x002B40F9 File Offset: 0x002B24F9
			public bool Used { get; set; }
		}
	}
}
