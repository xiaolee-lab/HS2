using System;
using AIProject.Definitions;
using AIProject.Player;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C67 RID: 3175
	public class ActorLocomotionThirdPerson : ActorLocomotion
	{
		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06006693 RID: 26259 RVA: 0x002BA312 File Offset: 0x002B8712
		// (set) Token: 0x06006694 RID: 26260 RVA: 0x002BA31A File Offset: 0x002B871A
		public bool onGround { get; private set; }

		// Token: 0x06006695 RID: 26261 RVA: 0x002BA323 File Offset: 0x002B8723
		private void Awake()
		{
			this._rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x002BA334 File Offset: 0x002B8734
		private void OnEnable()
		{
			base.Start();
			this.lookPosSmooth = base.transform.position + base.transform.forward * 10f;
			this._rigidbody.isKinematic = false;
			this._rigidbody.velocity = Vector3.zero;
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x002BA38E File Offset: 0x002B878E
		private void OnDisable()
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			base.GetComponent<Rigidbody>().isKinematic = true;
			this.AnimState.Init();
			this._moveDirection = Vector3.zero;
			this.moveDirectionVelocity = Vector3.zero;
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x002BA3CD File Offset: 0x002B87CD
		protected override void Start()
		{
			base.Start();
			this.lookPosSmooth = base.transform.position + base.transform.forward * 10f;
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x002BA400 File Offset: 0x002B8800
		public override void Move(Vector3 deltaPosition)
		{
			if (this._actor.IsKinematic)
			{
				return;
			}
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			Vector3 a = new Vector3(this._actor.StateInfo.move.x, 0f, this._actor.StateInfo.move.z);
			Desire.ActionType mode = this._actor.Mode;
			if (mode != Desire.ActionType.Normal && mode != Desire.ActionType.Date)
			{
				a *= locomotionProfile.PlayerSpeed.walkSpeed;
			}
			else if (Singleton<Manager.Input>.Instance.IsDown(KeyCode.LeftShift) || Singleton<Manager.Input>.Instance.IsDown(KeyCode.RightShift))
			{
				a *= locomotionProfile.PlayerSpeed.walkSpeed;
			}
			else
			{
				a *= locomotionProfile.PlayerSpeed.normalSpeed;
			}
			a += new Vector3(this.platformVelocity.x, 0f, this.platformVelocity.z);
			PlayerActor playerActor = this._actor as PlayerActor;
			if (!(playerActor.PlayerController.State is Follow) && this._navMeshAgent.enabled)
			{
				this._navMeshAgent.Move(a * Time.deltaTime);
			}
			float b = this.onGround ? base.GetSlopeDamper(-deltaPosition / Time.deltaTime, this._actor.Normal) : 1f;
			this._actor.ForwardMLP = Mathf.Lerp(this._actor.ForwardMLP, b, Time.deltaTime * 5f);
			if (float.IsNaN(this._actor.ForwardMLP))
			{
				this._actor.ForwardMLP = 0f;
			}
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x002BA5F4 File Offset: 0x002B89F4
		public void UpdateState(Actor.InputInfo state, ActorLocomotion.UpdateType updateType)
		{
			this._actor.StateInfo = state;
			this.Look(updateType);
			this.GroundCheck();
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			PlayerActor playerActor = this._actor as PlayerActor;
			NavMeshAgent navMeshAgent = this._actor.NavMeshAgent;
			if (playerActor.PlayerController.State is Follow)
			{
				Actor.InputInfo stateInfo = this._actor.StateInfo;
				stateInfo.move = Vector3.Scale(this._actor.NavMeshAgent.velocity, new Vector3(1f, 0f, 1f)).normalized;
				this._actor.StateInfo = stateInfo;
				if (navMeshAgent.remainingDistance > agentProfile.RunDistance && !playerActor.IsRunning)
				{
					playerActor.IsRunning = true;
				}
				this.AnimState.setMediumOnWalk = true;
				this.AnimState.medVelocity = locomotionProfile.AgentSpeed.walkSpeed;
				this.AnimState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
				float b;
				if (playerActor.IsRunning)
				{
					this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
					b = locomotionProfile.AgentSpeed.followRunSpeed;
				}
				else
				{
					this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
					b = locomotionProfile.AgentSpeed.walkSpeed;
				}
				navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, b, locomotionProfile.LerpSpeed);
			}
			else
			{
				Desire.ActionType mode = this._actor.Mode;
				if (mode != Desire.ActionType.Normal && mode != Desire.ActionType.Date)
				{
					if (mode == Desire.ActionType.Onbu)
					{
						this.AnimState.setMediumOnWalk = false;
						this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.PlayerSpeed.walkSpeed;
						this.AnimState.maxVelocity = locomotionProfile.PlayerSpeed.walkSpeed;
					}
				}
				else
				{
					if (Singleton<Manager.Input>.Instance.IsDown(KeyCode.LeftShift) || Singleton<Manager.Input>.Instance.IsDown(KeyCode.RightShift))
					{
						this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.PlayerSpeed.walkSpeed;
					}
					else
					{
						this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.PlayerSpeed.normalSpeed;
					}
					this.AnimState.setMediumOnWalk = true;
					this.AnimState.medVelocity = locomotionProfile.PlayerSpeed.walkSpeed;
					this.AnimState.maxVelocity = locomotionProfile.PlayerSpeed.normalSpeed;
				}
			}
			this.AnimState.onGround = this.onGround;
			this.CharacterAnimation.UpdateState(this.AnimState);
			this.ProgressStepCycle(this._actor.StateInfo.move.magnitude);
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x002BA924 File Offset: 0x002B8D24
		private void ProgressStepCycle(float speed)
		{
			if (this.AnimState.onGround && this.AnimState.moveDirection.sqrMagnitude > 0f && this._actor.StateInfo.move.sqrMagnitude != 0f)
			{
				this._stepCycle += (this.AnimState.moveDirection.magnitude + speed * this._runStepLength) * Time.fixedDeltaTime;
			}
			if (this._stepCycle <= this._nextStep)
			{
				return;
			}
			this._nextStep = this._stepCycle + this._stepInterval;
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x002BA9D0 File Offset: 0x002B8DD0
		private Vector3 MoveDirection
		{
			get
			{
				Actor.InputInfo stateInfo = this._actor.StateInfo;
				this._moveDirection = Vector3.SmoothDamp(this._moveDirection, new Vector3(0f, 0f, stateInfo.move.magnitude), ref this.moveDirectionVelocity, this._accelerationTime);
				return this._moveDirection;
			}
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x002BAA28 File Offset: 0x002B8E28
		private void Look(ActorLocomotion.UpdateType updateType)
		{
			float num = (updateType != ActorLocomotion.UpdateType.Update || updateType != ActorLocomotion.UpdateType.LateUpdate) ? Time.fixedDeltaTime : Time.deltaTime;
			this.lookPosSmooth = Vector3.Lerp(this.lookPosSmooth, this._actor.StateInfo.lookPos, num * this._settings.lookResponseSpeed);
			float num2 = base.GetAngleFromForward(this.GetLookDirection());
			if (this._actor.StateInfo.move == Vector3.zero)
			{
				num2 *= (1.01f - Mathf.Abs(num2) / 180f) * this._settings.stationaryTurnSpeedMlp;
			}
			base.RigidbodyRotateAround(this.CharacterAnimation.GetPivotPoint(), base.transform.up, num2 * num * this._settings.movingTurnSpeed);
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x002BAB00 File Offset: 0x002B8F00
		private Vector3 GetLookDirection()
		{
			bool flag = this._actor.StateInfo.move != Vector3.zero;
			return (!flag) ? ((!this.lookInCameraDirection) ? base.transform.forward : (this._actor.StateInfo.lookPos - this._actor.Position)) : this._actor.StateInfo.move;
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x002BAB87 File Offset: 0x002B8F87
		private RaycastHit GetHit()
		{
			return this.GetSpherecastHit();
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x002BAB90 File Offset: 0x002B8F90
		private void GroundCheck()
		{
			Vector3 zero = Vector3.zero;
			float num = 0f;
			RaycastHit hit = this.GetHit();
			this._actor.Normal = hit.normal;
			bool onGround = this.onGround;
			this.onGround = false;
			this.platformVelocity = Vector3.Lerp(this.platformVelocity, zero, Time.deltaTime * this._settings.platformFriction);
			this.stickyForce = num;
		}

		// Token: 0x04005876 RID: 22646
		public ActorAnimationThirdPerson CharacterAnimation;

		// Token: 0x04005877 RID: 22647
		public PlayerController userControl;

		// Token: 0x04005878 RID: 22648
		[Range(1f, 4f)]
		private float _gravityMultipiler = 2f;

		// Token: 0x04005879 RID: 22649
		public float _accelerationTime = 0.2f;

		// Token: 0x0400587A RID: 22650
		[Tooltip("空中での移動補正値")]
		public float airSpeed = 6f;

		// Token: 0x0400587B RID: 22651
		[Tooltip("空中制御の補正値")]
		public float airControl = 2f;

		// Token: 0x0400587C RID: 22652
		public bool lookInCameraDirection;

		// Token: 0x0400587D RID: 22653
		[SerializeField]
		private ActorLocomotionThirdPerson.Settings _settings = new ActorLocomotionThirdPerson.Settings(1f, 5f, 2f, 0.25f, 5f, 7f, 3f, 0.6f, 1f);

		// Token: 0x0400587F RID: 22655
		public ActorLocomotion.AnimationState AnimState = default(ActorLocomotion.AnimationState);

		// Token: 0x04005880 RID: 22656
		protected Vector3 _moveDirection;

		// Token: 0x04005881 RID: 22657
		private Vector3 lookPosSmooth;

		// Token: 0x04005882 RID: 22658
		private Vector3 platformVelocity;

		// Token: 0x04005883 RID: 22659
		private float jumpEndTime;

		// Token: 0x04005884 RID: 22660
		private float groundDistance;

		// Token: 0x04005885 RID: 22661
		private float stickyForce;

		// Token: 0x04005886 RID: 22662
		private Vector3 moveDirectionVelocity;

		// Token: 0x04005887 RID: 22663
		private Vector3 fixedDeltaPosition;

		// Token: 0x04005888 RID: 22664
		private Quaternion fixedDeltaRotation;

		// Token: 0x04005889 RID: 22665
		private bool fixedFrame;

		// Token: 0x0400588A RID: 22666
		private Vector3 gravity;

		// Token: 0x0400588B RID: 22667
		[SerializeField]
		[Range(0f, 1f)]
		private float _runStepLength;

		// Token: 0x0400588C RID: 22668
		[SerializeField]
		private float _stepInterval;

		// Token: 0x0400588D RID: 22669
		private float _stepCycle;

		// Token: 0x0400588E RID: 22670
		private float _nextStep;

		// Token: 0x0400588F RID: 22671
		private float _lastPosition;

		// Token: 0x04005890 RID: 22672
		private Rigidbody _rigidbody;

		// Token: 0x04005891 RID: 22673
		[SerializeField]
		private NavMeshAgent _navMeshAgent;

		// Token: 0x02000C68 RID: 3176
		[Serializable]
		public struct Settings
		{
			// Token: 0x060066A1 RID: 26273 RVA: 0x002BABFC File Offset: 0x002B8FFC
			public Settings(float turnSpeedMlp, float turnSpeed, float resSpeed, float jumpRepeatDelay, float groundSticky, float friction, float maxVerticalVel, float capsuleScaleMlp, float weight)
			{
				this.stationaryTurnSpeedMlp = turnSpeedMlp;
				this.movingTurnSpeed = turnSpeed;
				this.lookResponseSpeed = resSpeed;
				this.jumpRepeatDelayTime = jumpRepeatDelay;
				this.groundStickyEffect = groundSticky;
				this.platformFriction = friction;
				this.maxVerticalVelocityOnGround = maxVerticalVel;
				this.crouchCapsuleScaleMlp = capsuleScaleMlp;
				this.velocityToGroundTangentWeight = weight;
			}

			// Token: 0x04005892 RID: 22674
			public float stationaryTurnSpeedMlp;

			// Token: 0x04005893 RID: 22675
			public float movingTurnSpeed;

			// Token: 0x04005894 RID: 22676
			public float lookResponseSpeed;

			// Token: 0x04005895 RID: 22677
			[Tooltip("着地してから再びジャンプするまでに経過しないといけない時間")]
			public float jumpRepeatDelayTime;

			// Token: 0x04005896 RID: 22678
			[Tooltip("強制的に地面にくっつける力")]
			public float groundStickyEffect;

			// Token: 0x04005897 RID: 22679
			[Tooltip("キャラが立つ場所にかかってる力の補間(摩擦力？)\n0にすると壁走りが出来なくなるっぽい？")]
			public float platformFriction;

			// Token: 0x04005898 RID: 22680
			[Tooltip("地面上に居る時にY軸にかかる力の最大値")]
			public float maxVerticalVelocityOnGround;

			// Token: 0x04005899 RID: 22681
			[Tooltip("しゃがみ中の当たり判定サイズ補正値")]
			public float crouchCapsuleScaleMlp;

			// Token: 0x0400589A RID: 22682
			[Tooltip("移動にかかるに地面の向きを反映させる割り合い")]
			public float velocityToGroundTangentWeight;
		}
	}
}
