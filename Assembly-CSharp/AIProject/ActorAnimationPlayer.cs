using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion.FinalIK;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C60 RID: 3168
	public class ActorAnimationPlayer : ActorAnimationThirdPerson
	{
		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06006650 RID: 26192 RVA: 0x002B7FB6 File Offset: 0x002B63B6
		// (set) Token: 0x06006651 RID: 26193 RVA: 0x002B7FBE File Offset: 0x002B63BE
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
					this._ik.enabled = true;
				}
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06006652 RID: 26194 RVA: 0x002B7FE3 File Offset: 0x002B63E3
		public float TurnSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._turnSpeed;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06006653 RID: 26195 RVA: 0x002B7FEB File Offset: 0x002B63EB
		public float RunCycleLegOffset
		{
			[CompilerGenerated]
			get
			{
				return this._runCycleLegOffset;
			}
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x002B7FF4 File Offset: 0x002B63F4
		protected override void Start()
		{
			base.Start();
			if (this._ik == null)
			{
				this._ik = base.GetComponentInChildren<FullBodyBipedIK>();
				if (this._ik)
				{
					this._ik.enabled = true;
				}
			}
			if (this.Animator == null)
			{
				this.Animator = base.GetComponentInChildren<Animator>();
			}
			this._lastForward = base.transform.forward;
			(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x002B80A4 File Offset: 0x002B64A4
		protected void OnAnimatorMove()
		{
			base.Character.transform.localPosition += this.Animator.deltaPosition;
			base.Character.transform.localRotation *= this.Animator.deltaRotation;
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x002B80FD File Offset: 0x002B64FD
		public override Vector3 GetPivotPoint()
		{
			return base.transform.position;
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x002B810C File Offset: 0x002B650C
		protected override void LoadMatchTargetInfo(string stateName)
		{
			int key = Animator.StringToHash(stateName);
			Dictionary<int, List<AnimeMoveInfo>> dictionary;
			List<AnimeMoveInfo> list;
			if (Singleton<Manager.Resources>.Instance.Animation.PlayerMoveInfoTable.TryGetValue((int)base.Actor.ChaControl.sex, out dictionary) && dictionary.TryGetValue(key, out list))
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

		// Token: 0x06006658 RID: 26200 RVA: 0x002B820C File Offset: 0x002B660C
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
			int personality = base.Actor.ID - (int)base.Actor.ChaControl.sex;
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

		// Token: 0x06006659 RID: 26201 RVA: 0x002B8388 File Offset: 0x002B6788
		protected override void LoadStateLocomotionVoice(int stateHashName)
		{
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x002B838C File Offset: 0x002B678C
		protected override void LoadStateExpression(int stateHashName)
		{
			Manager.Resources.ActionTable action = Singleton<Manager.Resources>.Instance.Action;
			base.ExpressionKeyframeList.Clear();
			int num = base.Actor.ID - (int)base.Actor.ChaControl.sex;
			Dictionary<int, List<ExpressionKeyframe>> dictionary;
			List<ExpressionKeyframe> list;
			Dictionary<int, string> dictionary2;
			string key;
			if (action.ActionExpressionKeyframeTable.TryGetValue(num, out dictionary) && dictionary.TryGetValue(stateHashName, out list))
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
			else if (action.ActionExpressionTable.TryGetValue(num, out dictionary2) && dictionary2.TryGetValue(stateHashName, out key))
			{
				Game.Expression expression = Singleton<Game>.Instance.GetExpression(num, key);
				if (expression != null)
				{
					expression.Change(base.Actor.ChaControl);
				}
			}
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x002B84B4 File Offset: 0x002B68B4
		public override void LoadEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> dictionary;
			Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary2;
			Dictionary<int, List<AnimeEventInfo>> itemEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.PlayerItemEventKeyTable.TryGetValue((int)base.Actor.ChaControl.sex, out dictionary) && dictionary.TryGetValue(eventID, out dictionary2) && dictionary2.TryGetValue(poseID, out itemEventKeyTable))
			{
				base.ItemEventKeyTable = itemEventKeyTable;
			}
			this.LoadSEEventKeyTable(eventID, poseID);
			this.LoadParticleEventKeyTable(eventID, poseID);
			this.LoadOnceVoiceEventKeyTable(eventID, poseID);
			this.LoadLoopVoiceEventKeyTable(eventID, poseID);
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x002B8530 File Offset: 0x002B6930
		public override void LoadSEEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> dictionary;
			Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>> dictionary2;
			Dictionary<int, List<AnimeSEEventInfo>> seeventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.PlayerActSEEventKeyTable.TryGetValue((int)base.Actor.ChaControl.sex, out dictionary) && dictionary.TryGetValue(eventID, out dictionary2) && dictionary2.TryGetValue(poseID, out seeventKeyTable))
			{
				base.SEEventKeyTable = seeventKeyTable;
				return;
			}
			base.SEEventKeyTable = null;
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x002B8594 File Offset: 0x002B6994
		public override void LoadParticleEventKeyTable(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> dictionary;
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary2;
			Dictionary<int, List<AnimeParticleEventInfo>> particleEventKeyTable;
			if (Singleton<Manager.Resources>.Instance.Animation.PlayerActParticleEventKeyTable.TryGetValue((int)base.Actor.ChaControl.sex, out dictionary) && dictionary.TryGetValue(eventID, out dictionary2) && dictionary2.TryGetValue(poseID, out particleEventKeyTable))
			{
				base.ParticleEventKeyTable = particleEventKeyTable;
				return;
			}
			base.ParticleEventKeyTable = null;
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x002B85F7 File Offset: 0x002B69F7
		public override void LoadOnceVoiceEventKeyTable(int eventID, int poseID)
		{
			base.OnceVoiceEventKeyTable = null;
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x002B8600 File Offset: 0x002B6A00
		public override void LoadLoopVoiceEventKeyTable(int eventID, int poseID)
		{
			base.LoopVoiceEventKeyTable = null;
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x002B860C File Offset: 0x002B6A0C
		protected override void PlayEventOnceVoice(int voiceID)
		{
			PlayerActor x = base.Actor as PlayerActor;
			if (x == null)
			{
				return;
			}
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x002B8634 File Offset: 0x002B6A34
		protected override bool PlayEventLoopVoice()
		{
			if (!base.PlayEventLoopVoice())
			{
				return false;
			}
			PlayerActor x = base.Actor as PlayerActor;
			return x == null && false;
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x002B866C File Offset: 0x002B6A6C
		protected override void LoadFootStepEventKeyTable()
		{
			PlayerActor playerActor = base.Actor as PlayerActor;
			if (playerActor == null)
			{
				this._footStepEventKeyTable = null;
				return;
			}
			Dictionary<int, Dictionary<int, FootStepInfo[]>> playerFootStepEventKeyTable = Singleton<Manager.Resources>.Instance.Animation.PlayerFootStepEventKeyTable;
			Dictionary<int, FootStepInfo[]> footStepEventKeyTable;
			playerFootStepEventKeyTable.TryGetValue((int)playerActor.ChaControl.sex, out footStepEventKeyTable);
			this._footStepEventKeyTable = footStepEventKeyTable;
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x002B86C4 File Offset: 0x002B6AC4
		public override void UpdateState(ActorLocomotion.AnimationState state)
		{
			if (Mathf.Approximately(Time.deltaTime, 0f))
			{
				return;
			}
			if (this.Animator == null)
			{
				return;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.Animator.GetCurrentAnimatorStateInfo(0);
			base.IsLocomotionState = (currentAnimatorStateInfo.IsName("Locomotion") || currentAnimatorStateInfo.IsName("Grounded Strafe"));
			float num = -base.GetAngleFromForward(this._lastForward);
			this._lastForward = base.transform.forward;
			num *= this._turnSensitivity * 0.01f;
			string forwardMove = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.ForwardMove;
			string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
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
			float value2 = Mathf.Lerp(this.Animator.GetFloat(forwardMove), b2, Time.deltaTime * Singleton<Manager.Resources>.Instance.LocomotionProfile.LerpSpeed);
			this.Animator.SetFloat(forwardMove, value2);
			PlayerActor playerActor = base.Actor as PlayerActor;
			if (playerActor != null && playerActor.Partner != null && playerActor.Partner.IsSlave)
			{
				playerActor.Partner.Animation.Animator.SetFloat(forwardMove, value2);
				float shapeBodyValue = playerActor.Partner.ChaControl.GetShapeBodyValue(0);
				this.Animator.SetFloat(heightParameterName, shapeBodyValue);
			}
			this.Animator.speed = ((!state.onGround || state.moveDirection.z <= 0f) ? 1f : this._animSpeedMultiplier);
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x002B895C File Offset: 0x002B6D5C
		protected override void OnLateUpdate()
		{
			base.Follow();
			if (Vector3.Angle(base.transform.up, Vector3.up) <= 0.01f)
			{
				return;
			}
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x002B8984 File Offset: 0x002B6D84
		private void RotateEffector(IKEffector effector, Quaternion rotation, float mlp)
		{
			Vector3 vector = effector.bone.position - base.transform.position;
			Vector3 a = rotation * vector;
			Vector3 a2 = a - vector;
			effector.positionOffset += a2 * mlp;
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x002B89D8 File Offset: 0x002B6DD8
		public void ForceChangeAnime()
		{
			if (this.Animator && this.Animator.runtimeAnimatorController != null)
			{
				DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
				this.Animator.SetFloat(definePack.AnimatorParameter.ForwardMove, 0f);
				this.Animator.Play(definePack.AnimatorState.IdleState, 0, 0f);
			}
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x002B8A50 File Offset: 0x002B6E50
		public ActorAnimationPlayer CloneComponent(GameObject target)
		{
			ActorAnimationPlayer actorAnimationPlayer = target.AddComponent<ActorAnimationPlayer>();
			actorAnimationPlayer._character = this._character;
			actorAnimationPlayer.Animator = this.Animator;
			actorAnimationPlayer.EnabledPoser = base.EnabledPoser;
			actorAnimationPlayer.ArmAnimator = base.ArmAnimator;
			actorAnimationPlayer.Poser = base.Poser;
			actorAnimationPlayer.IsLocomotionState = base.IsLocomotionState;
			actorAnimationPlayer._ik = this._ik;
			actorAnimationPlayer._turnSensitivity = this._turnSensitivity;
			return actorAnimationPlayer;
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x002B8AC8 File Offset: 0x002B6EC8
		public override void PlayAnimation(string stateName, int layer, float normalizedTime)
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
						float value = 0.75f;
						if (animator != null)
						{
							animator.SetFloat(heightParameterName, value);
						}
					}
				}
			}
			base.PlayAnimation(animator, stateName, layer, normalizedTime);
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x002B8B70 File Offset: 0x002B6F70
		public override void PlayAnimation(int stateNameHash, int layer, float normalizedTime)
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
						float value = 0.75f;
						if (animator != null)
						{
							animator.SetFloat(heightParameterName, value);
						}
					}
				}
			}
			if (animator != null)
			{
				animator.Play(stateNameHash, layer, normalizedTime);
			}
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x002B8C20 File Offset: 0x002B7020
		public override void CrossFadeAnimation(string stateName, float fadeTime, int layer, float fixedTimeOffset)
		{
			Animator animator = this.Animator;
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			foreach (AnimatorControllerParameter animatorControllerParameter in this._parameters)
			{
				if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float value = 0.75f;
					if (animator != null)
					{
						animator.SetFloat(heightParameterName, value);
					}
				}
			}
			base.CrossFadeAnimation(animator, stateName, fadeTime, layer, fixedTimeOffset);
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x002B8CB8 File Offset: 0x002B70B8
		public override void CrossFadeAnimation(int stateNameHash, float fadeTime, int layer, float fixedTimeOffset)
		{
			Animator animator = this.Animator;
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string heightParameterName = definePack.AnimatorParameter.HeightParameterName;
			foreach (AnimatorControllerParameter animatorControllerParameter in this._parameters)
			{
				if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					float value = 0.75f;
					if (animator != null)
					{
						animator.SetFloat(heightParameterName, value);
					}
				}
			}
			if (animator != null)
			{
				animator.CrossFadeInFixedTime(stateNameHash, fadeTime, layer, fixedTimeOffset, 0f);
			}
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x002B8D60 File Offset: 0x002B7160
		protected override void PlayItemAnimation(string stateName)
		{
			string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
			foreach (KeyValuePair<int, ItemAnimInfo> keyValuePair in base.ItemAnimatorTable)
			{
				Animator animator = keyValuePair.Value.Animator;
				if (keyValuePair.Value.Sync)
				{
					foreach (AnimatorControllerParameter animatorControllerParameter in keyValuePair.Value.Parameters)
					{
						if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
						{
							float shapeBodyValue = base.Actor.ChaControl.GetShapeBodyValue(0);
							if (animator != null)
							{
								animator.SetFloat(heightParameterName, shapeBodyValue);
							}
						}
					}
					base.PlayAnimation(animator, stateName, 0, 0f);
				}
			}
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x002B8E6C File Offset: 0x002B726C
		protected override void CrossFadeItemAnimation(string stateName, float fadeTime, int layer)
		{
			string heightParameterName = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.HeightParameterName;
			foreach (KeyValuePair<int, ItemAnimInfo> keyValuePair in base.ItemAnimatorTable)
			{
				Animator animator = keyValuePair.Value.Animator;
				if (keyValuePair.Value.Sync)
				{
					foreach (AnimatorControllerParameter animatorControllerParameter in keyValuePair.Value.Parameters)
					{
						if (animatorControllerParameter.name == heightParameterName && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
						{
							float shapeBodyValue = base.Actor.ChaControl.GetShapeBodyValue(0);
							if (animator != null)
							{
								animator.SetFloat(heightParameterName, shapeBodyValue);
							}
						}
					}
					base.CrossFadeAnimation(animator, stateName, fadeTime, layer, 0f);
				}
			}
		}

		// Token: 0x04005848 RID: 22600
		[SerializeField]
		private FullBodyBipedIK _ik;

		// Token: 0x04005849 RID: 22601
		[SerializeField]
		private float _turnSensitivity = 0.2f;

		// Token: 0x0400584A RID: 22602
		[SerializeField]
		private float _turnSpeed = 5f;

		// Token: 0x0400584B RID: 22603
		[SerializeField]
		private float _runCycleLegOffset = 0.2f;

		// Token: 0x0400584C RID: 22604
		[SerializeField]
		[Range(0.1f, 10f)]
		private float _animSpeedMultiplier = 1f;
	}
}
