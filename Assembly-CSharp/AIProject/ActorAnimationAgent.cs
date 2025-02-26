using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion.FinalIK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C5E RID: 3166
	public class ActorAnimationAgent : ActorAnimation
	{
		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x0600660B RID: 26123 RVA: 0x002B6483 File Offset: 0x002B4883
		// (set) Token: 0x0600660C RID: 26124 RVA: 0x002B648C File Offset: 0x002B488C
		public FullBodyBipedIK IK
		{
			get
			{
				return this._ik;
			}
			set
			{
				this._ik = value;
				if (this._ik)
				{
					IKSolverFullBodyBiped solver = this._ik.solver;
					solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
					this._ik.enabled = true;
				}
			}
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x002B64E8 File Offset: 0x002B48E8
		protected override void Start()
		{
			base.Start();
			if (this._ik == null)
			{
				this._ik = base.GetComponentInChildren<FullBodyBipedIK>(true);
				if (this._ik)
				{
					this._ik.enabled = true;
					IKSolverFullBodyBiped solver = this._ik.solver;
					solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
				}
			}
			if (this.Animator == null)
			{
				this.Animator = base.GetComponentInChildren<Animator>();
			}
			(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x002B65B4 File Offset: 0x002B49B4
		protected void OnAnimatorMove()
		{
			base.Character.transform.localPosition += this.Animator.deltaPosition;
			base.Character.transform.localRotation *= this.Animator.deltaRotation;
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x002B660D File Offset: 0x002B4A0D
		public override Vector3 GetPivotPoint()
		{
			return base.transform.position;
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x002B661C File Offset: 0x002B4A1C
		public IObservable<Unit> OnEndActionAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._endAction) == null)
			{
				result = (this._endAction = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x002B6644 File Offset: 0x002B4A44
		public IObservable<Unit> OnActionPlayAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._actionPlay) == null)
			{
				result = (this._actionPlay = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x002B666C File Offset: 0x002B4A6C
		public IObservable<Unit> OnCompleteActionAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._completeAction) == null)
			{
				result = (this._completeAction = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x002B6694 File Offset: 0x002B4A94
		public TaskStatus OnUpdateActionState()
		{
			if (base.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			ActorAnimInfo animInfo = base.AnimInfo;
			if (animInfo.isLoop)
			{
				if (base.PlayingActAnimation)
				{
					return TaskStatus.Running;
				}
				AgentActor agentActor = base.Actor as AgentActor;
				if (agentActor.Schedule.enabled)
				{
					AnimatorStateInfo currentAnimatorStateInfo = this.Animator.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.IsName(animInfo.loopStateName))
					{
						float num = currentAnimatorStateInfo.normalizedTime - animInfo.oldNormalizedTime;
						if (num > 1f)
						{
							animInfo.oldNormalizedTime = currentAnimatorStateInfo.normalizedTime;
							if (UnityEngine.Random.Range(0, animInfo.randomCount) == 0)
							{
								if (this._actionPlay != null)
								{
									this._actionPlay.OnNext(Unit.Default);
								}
								animInfo.oldNormalizedTime = 0f;
							}
						}
					}
					return TaskStatus.Running;
				}
				if (this._endAction != null)
				{
					this._endAction.OnNext(Unit.Default);
				}
				if (base.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				if (this._completeAction != null)
				{
					this._completeAction.OnNext(Unit.Default);
				}
				return TaskStatus.Success;
			}
			else
			{
				if (this._endAction != null)
				{
					this._endAction.OnNext(Unit.Default);
				}
				if (base.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				if (this._completeAction != null)
				{
					this._completeAction.OnNext(Unit.Default);
				}
				return TaskStatus.Success;
			}
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x002B6810 File Offset: 0x002B4C10
		protected override void LoadMatchTargetInfo(string stateName)
		{
			int key = Animator.StringToHash(stateName);
			List<AnimeMoveInfo> list;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentMoveInfoTable.TryGetValue(key, out list))
			{
				foreach (AnimeMoveInfo animeMoveInfo in list)
				{
					GameObject gameObject = base.Actor.CurrentPoint.transform.FindLoop(animeMoveInfo.movePoint);
					ProceduralTargetParameter proceduralTargetParameter = new ProceduralTargetParameter
					{
						Start = animeMoveInfo.start,
						End = animeMoveInfo.end
					};
					if (gameObject != null)
					{
						proceduralTargetParameter.Target = gameObject.transform;
					}
					base.Targets.Add(proceduralTargetParameter);
				}
			}
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x002B68F0 File Offset: 0x002B4CF0
		protected override void LoadStateLocomotionVoice(int stateHashName)
		{
			AgentActor x = base.Actor as AgentActor;
			if (x == null)
			{
				return;
			}
			Manager.Resources.ActionTable action = Singleton<Manager.Resources>.Instance.Action;
			int num;
			if (action.AgentLocomotionBreathTable.TryGetValue(stateHashName, out num))
			{
				if (this._loopActionVoice.Item1 == num && this._loopActionVoice.Item2 != null)
				{
					return;
				}
				int personality = base.Actor.ChaControl.fileParam.personality;
				AssetBundleInfo assetBundleInfo;
				if (!Singleton<Manager.Resources>.Instance.Sound.TryGetMapActionVoiceInfo(personality, num, out assetBundleInfo))
				{
					return;
				}
				Transform transform = base.Actor.Locomotor.transform;
				Voice instance = Singleton<Voice>.Instance;
				int no = personality;
				string assetbundle = assetBundleInfo.assetbundle;
				string asset = assetBundleInfo.asset;
				Transform voiceTrans = transform;
				Transform transform2 = instance.OnecePlayChara(no, assetbundle, asset, 1f, 0f, 0f, true, voiceTrans, Voice.Type.PCM, -1, false, true, false);
				base.Actor.ChaControl.SetVoiceTransform(transform2);
				this._loopActionVoice.Item1 = num;
				this._loopActionVoice.Item2 = transform2.GetComponent<AudioSource>();
				this._loopActionVoice.Item3 = transform;
				this._loopActionVoice.Item4 = personality;
			}
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x002B6A2C File Offset: 0x002B4E2C
		protected override void LoadStateExpression(int stateHashName)
		{
			Manager.Resources.ActionTable action = Singleton<Manager.Resources>.Instance.Action;
			base.ExpressionKeyframeList.Clear();
			int personality = base.Actor.ChaControl.fileParam.personality;
			Dictionary<int, List<ExpressionKeyframe>> dictionary;
			List<ExpressionKeyframe> list;
			Dictionary<int, string> dictionary2;
			string key;
			if (action.ActionExpressionKeyframeTable.TryGetValue(personality, out dictionary) && dictionary.TryGetValue(stateHashName, out list))
			{
				foreach (ExpressionKeyframe expressionKeyframe in list)
				{
					base.ExpressionKeyframeList.Add(new ActorAnimation.ExpressionKeyframeEvent
					{
						NormalizedTime = expressionKeyframe.normalizedTime,
						Name = expressionKeyframe.expressionName
					});
				}
			}
			else if (Singleton<Manager.Resources>.Instance.Action.ActionExpressionTable.TryGetValue(personality, out dictionary2) && dictionary2.TryGetValue(stateHashName, out key))
			{
				Game.Expression expression = Singleton<Game>.Instance.GetExpression(personality, key);
				if (expression != null)
				{
					expression.Change(base.Actor.ChaControl);
				}
			}
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x002B6B58 File Offset: 0x002B4F58
		protected override void UpdateExpressionEvent(float normalizedTime, bool isLoop)
		{
			if (isLoop)
			{
				foreach (ActorAnimation.ExpressionKeyframeEvent expressionKeyframeEvent in base.ExpressionKeyframeList)
				{
					expressionKeyframeEvent.Used = false;
				}
			}
			else
			{
				foreach (ActorAnimation.ExpressionKeyframeEvent expressionKeyframeEvent2 in base.ExpressionKeyframeList)
				{
					if (expressionKeyframeEvent2.NormalizedTime > normalizedTime && expressionKeyframeEvent2.Used)
					{
						expressionKeyframeEvent2.Used = false;
					}
				}
			}
			int personality = base.Actor.ChaControl.fileParam.personality;
			foreach (ActorAnimation.ExpressionKeyframeEvent expressionKeyframeEvent3 in base.ExpressionKeyframeList)
			{
				if (expressionKeyframeEvent3.NormalizedTime < normalizedTime && !expressionKeyframeEvent3.Used)
				{
					expressionKeyframeEvent3.Used = true;
					Game.Expression expression = Singleton<Game>.Instance.GetExpression(personality, expressionKeyframeEvent3.Name);
					if (expression != null)
					{
						expression.Change(base.Actor.ChaControl);
					}
				}
			}
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x002B6CD0 File Offset: 0x002B50D0
		public override void LoadEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary;
			Dictionary<int, List<AnimeEventInfo>> itemEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentItemEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out itemEventKeyTable))
			{
				base.ItemEventKeyTable = itemEventKeyTable;
			}
			Dictionary<int, List<AnimeEventInfo>> clothEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentChangeClothEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out clothEventKeyTable))
			{
				base.ClothEventKeyTable = clothEventKeyTable;
			}
			this.LoadSEEventKeyTable(eventID, poseID);
			this.LoadParticleEventKeyTable(eventID, poseID);
			this.LoadOnceVoiceEventKeyTable(eventID, poseID);
			this.LoadLoopVoiceEventKeyTable(eventID, poseID);
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x002B6D60 File Offset: 0x002B5160
		public override void LoadAnimalEventKeyTable(int animalTypeID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary;
			Dictionary<int, List<AnimeEventInfo>> itemEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentAnimalEventKeyTable.TryGetValue(animalTypeID, out dictionary) && dictionary.TryGetValue(poseID, out itemEventKeyTable))
			{
				base.ItemEventKeyTable = itemEventKeyTable;
			}
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x002B6DA0 File Offset: 0x002B51A0
		public override void LoadSEEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>> dictionary;
			Dictionary<int, List<AnimeSEEventInfo>> seeventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentActSEEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out seeventKeyTable))
			{
				base.SEEventKeyTable = seeventKeyTable;
				return;
			}
			base.SEEventKeyTable = null;
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x002B6DE8 File Offset: 0x002B51E8
		public override void LoadParticleEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary;
			Dictionary<int, List<AnimeParticleEventInfo>> particleEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentActParticleEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out particleEventKeyTable))
			{
				base.ParticleEventKeyTable = particleEventKeyTable;
				return;
			}
			base.ParticleEventKeyTable = null;
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x002B6E30 File Offset: 0x002B5230
		public override void LoadOnceVoiceEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>> dictionary;
			Dictionary<int, List<AnimeOnceVoiceEventInfo>> onceVoiceEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentActOnceVoiceEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out onceVoiceEventKeyTable))
			{
				base.OnceVoiceEventKeyTable = onceVoiceEventKeyTable;
				return;
			}
			base.OnceVoiceEventKeyTable = null;
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x002B6E78 File Offset: 0x002B5278
		public override void LoadLoopVoiceEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<int>>> dictionary;
			Dictionary<int, List<int>> loopVoiceEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.AgentActLoopVoiceEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out loopVoiceEventKeyTable))
			{
				base.LoopVoiceEventKeyTable = loopVoiceEventKeyTable;
				return;
			}
			base.LoopVoiceEventKeyTable = null;
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x002B6EC0 File Offset: 0x002B52C0
		protected override void PlayEventOnceVoice(int voiceID)
		{
			AgentActor agentActor = base.Actor as AgentActor;
			if (agentActor == null)
			{
				return;
			}
			int personality = agentActor.ChaControl.fileParam.personality;
			AssetBundleInfo assetBundleInfo;
			if (!Singleton<Manager.Resources>.Instance.Sound.TryGetMapActionVoiceInfo(personality, voiceID, out assetBundleInfo))
			{
				return;
			}
			Transform transform = base.Actor.Locomotor.transform;
			Voice instance = Singleton<Voice>.Instance;
			int no = personality;
			string assetbundle = assetBundleInfo.assetbundle;
			string asset = assetBundleInfo.asset;
			Transform voiceTrans = transform;
			Transform transform2 = instance.OnecePlayChara(no, assetbundle, asset, 1f, 0f, 0f, true, voiceTrans, Voice.Type.PCM, -1, true, true, false);
			base.Actor.ChaControl.SetVoiceTransform(transform2);
			base.OnceActionVoice = transform2.GetComponent<AudioSource>();
			if (base.OnceActionVoice != null)
			{
				base.OnceActionVoice.OnDestroyAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(Unit _)
				{
					if (!this.PlayEventLoopVoice())
					{
						this.StopLoopActionVoice();
					}
				});
			}
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x002B6FBC File Offset: 0x002B53BC
		protected override bool PlayEventLoopVoice()
		{
			if (!base.PlayEventLoopVoice())
			{
				return false;
			}
			AgentActor agentActor = base.Actor as AgentActor;
			if (agentActor == null)
			{
				return false;
			}
			int num = base.StateLoopVoiceEvents[UnityEngine.Random.Range(0, base.StateLoopVoiceEvents.Count)];
			if (this._loopActionVoice.Item1 == num && this._loopActionVoice.Item2 != null)
			{
				return false;
			}
			int personality = agentActor.ChaControl.fileParam.personality;
			AssetBundleInfo assetBundleInfo;
			if (!Singleton<Manager.Resources>.Instance.Sound.TryGetMapActionVoiceInfo(personality, num, out assetBundleInfo))
			{
				return false;
			}
			Transform transform = base.Actor.Locomotor.transform;
			Voice instance = Singleton<Voice>.Instance;
			int no = personality;
			string assetbundle = assetBundleInfo.assetbundle;
			string asset = assetBundleInfo.asset;
			Transform voiceTrans = transform;
			Transform transform2 = instance.OnecePlayChara(no, assetbundle, asset, 1f, 0f, 0f, true, voiceTrans, Voice.Type.PCM, -1, false, true, false);
			base.Actor.ChaControl.SetVoiceTransform(transform2);
			this._loopActionVoice.Item1 = num;
			this._loopActionVoice.Item2 = transform2.GetComponent<AudioSource>();
			this._loopActionVoice.Item3 = transform;
			this._loopActionVoice.Item4 = personality;
			return this._loopActionVoice.Item2 != null;
		}

		// Token: 0x06006620 RID: 26144 RVA: 0x002B7110 File Offset: 0x002B5510
		protected override void LoadFootStepEventKeyTable()
		{
			this._footStepEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.AgentFootStepEventKeyTable;
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x002B7128 File Offset: 0x002B5528
		public void UpdateState(ActorLocomotion.AnimationState state)
		{
			if (Mathf.Approximately(Time.deltaTime, 0f))
			{
				return;
			}
			if (this.Animator == null)
			{
				return;
			}
			base.IsLocomotionState = this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion");
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string directionParameterName = definePack.AnimatorParameter.DirectionParameterName;
			string forwardMove = definePack.AnimatorParameter.ForwardMove;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			float b2;
			if (state.setMediumOnWalk)
			{
				if (state.moveDirection.z < state.medVelocity || Mathf.Approximately(state.moveDirection.z, state.medVelocity))
				{
					float b = Mathf.InverseLerp(0f, state.maxVelocity, state.medVelocity);
					float value = Mathf.InverseLerp(0f, state.maxVelocity, state.moveDirection.z);
					float t = Mathf.InverseLerp(0f, b, value);
					b2 = Mathf.Lerp(0f, 0.5f, t);
				}
				else
				{
					b2 = Mathf.InverseLerp(0f, state.maxVelocity, state.moveDirection.z);
				}
			}
			else
			{
				b2 = Mathf.InverseLerp(0f, state.maxVelocity, state.moveDirection.z);
			}
			if (!base.Actor.IsSlave)
			{
				float @float = this.Animator.GetFloat(forwardMove);
				float value2 = Mathf.Lerp(@float, b2, Time.deltaTime * Singleton<Manager.Resources>.Instance.LocomotionProfile.LerpSpeed);
				this.Animator.SetFloat(forwardMove, value2);
			}
			float shapeBodyValue = base.Actor.ChaControl.GetShapeBodyValue(0);
			this.Animator.SetFloat(heightParameterName, shapeBodyValue);
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x002B7305 File Offset: 0x002B5705
		private void AfterFBBIK()
		{
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x002B7307 File Offset: 0x002B5707
		private void OnLateUpdate()
		{
			base.Follow();
			if (Vector3.Angle(base.transform.up, Vector3.up) <= 0.01f)
			{
				return;
			}
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x002B7330 File Offset: 0x002B5730
		private void RotateEffector(IKEffector effector, Quaternion rotation, float mlp)
		{
			Vector3 vector = effector.bone.position - base.transform.position;
			Vector3 a = rotation * vector;
			Vector3 a2 = a - vector;
			effector.positionOffset += a2 * mlp;
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x002B7384 File Offset: 0x002B5784
		public void ForceChangeAnime()
		{
			if (this.Animator && this.Animator.runtimeAnimatorController != null)
			{
				this.Animator.SetFloat(Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.ForwardMove, 0f);
			}
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x002B73DC File Offset: 0x002B57DC
		public ActorAnimationAgent CloneComponent(GameObject target)
		{
			ActorAnimationAgent actorAnimationAgent = target.AddComponent<ActorAnimationAgent>();
			actorAnimationAgent._character = this._character;
			actorAnimationAgent.Animator = this.Animator;
			actorAnimationAgent.EnabledPoser = base.EnabledPoser;
			actorAnimationAgent.ArmAnimator = base.ArmAnimator;
			actorAnimationAgent.Poser = base.Poser;
			actorAnimationAgent.IsLocomotionState = base.IsLocomotionState;
			actorAnimationAgent._ik = this._ik;
			return actorAnimationAgent;
		}

		// Token: 0x04005842 RID: 22594
		[SerializeField]
		private FullBodyBipedIK _ik;

		// Token: 0x04005843 RID: 22595
		private Subject<Unit> _endAction;

		// Token: 0x04005844 RID: 22596
		private Subject<Unit> _actionPlay;

		// Token: 0x04005845 RID: 22597
		private Subject<Unit> _completeAction;

		// Token: 0x04005846 RID: 22598
		private float _currentVelocity;
	}
}
