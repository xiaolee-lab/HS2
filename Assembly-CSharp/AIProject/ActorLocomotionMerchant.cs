using System;
using AIProject.Definitions;
using Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C66 RID: 3174
	public class ActorLocomotionMerchant : ActorLocomotion
	{
		// Token: 0x0600668C RID: 26252 RVA: 0x002B9F30 File Offset: 0x002B8330
		private void OnDisable()
		{
			this.AnimState.Init();
			this._moveDirection = (this._moveDirectionVelocity = Vector3.zero);
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x002B9F5C File Offset: 0x002B835C
		public override void Move(Vector3 deltaPosition)
		{
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x002B9F60 File Offset: 0x002B8360
		public void UpdateState()
		{
			this.CalcAnimSpeed();
			MerchantActor merchantActor = this._actor as MerchantActor;
			NavMeshAgent navMeshAgent = merchantActor.NavMeshAgent;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			if (merchantActor.CurrentMode == Merchant.ActionType.GotoLesbianSpotFollow)
			{
				if (navMeshAgent.isActiveAndEnabled && !navMeshAgent.pathPending)
				{
					if (!merchantActor.IsRunning && agentProfile.RunDistance < navMeshAgent.remainingDistance)
					{
						merchantActor.IsRunning = true;
					}
					float b;
					if (merchantActor.IsRunning)
					{
						this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.MerchantSpeed.runSpeed;
						b = locomotionProfile.MerchantSpeed.runSpeed;
					}
					else
					{
						this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.MerchantSpeed.walkSpeed;
						b = locomotionProfile.MerchantSpeed.walkSpeed;
					}
					navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, b, locomotionProfile.LerpSpeed);
				}
				else
				{
					this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.MerchantSpeed.walkSpeed;
				}
			}
			else
			{
				int movePoseID = this.MovePoseID;
				if (movePoseID != 0)
				{
					this.AnimState.moveDirection = Vector3.zero;
					this.AnimState.setMediumOnWalk = false;
					this.AnimState.maxVelocity = locomotionProfile.MerchantSpeed.walkSpeed;
				}
				else
				{
					this.AnimState.moveDirection = this.MoveDirection * locomotionProfile.MerchantSpeed.walkSpeed;
					this.AnimState.setMediumOnWalk = true;
					this.AnimState.medVelocity = locomotionProfile.MerchantSpeed.walkSpeed;
					this.AnimState.maxVelocity = locomotionProfile.MerchantSpeed.runSpeed;
				}
			}
			this.CharacterAnimation.UpdateState(this.AnimState);
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600668F RID: 26255 RVA: 0x002BA178 File Offset: 0x002B8578
		private Vector3 MoveDirection
		{
			get
			{
				Vector3 moveDirection = this._moveDirection;
				Vector3 vector = new Vector3(0f, 0f, this._actor.StateInfo.move.magnitude);
				return this._moveDirection = Vector3.SmoothDamp(moveDirection, vector.normalized, ref this._moveDirectionVelocity, this.AccelerationTime);
			}
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x002BA1D8 File Offset: 0x002B85D8
		private void CalcAnimSpeed()
		{
			Actor.InputInfo stateInfo = default(Actor.InputInfo);
			stateInfo.move = Vector3.Scale(this._actor.NavMeshAgent.velocity, ActorLocomotionMerchant.ZXOne).normalized;
			float num = this._actor.NavMeshAgent.velocity.magnitude;
			MerchantActor merchantActor = this._actor as MerchantActor;
			num = Mathf.InverseLerp(0f, merchantActor.DestinationSpeed, num);
			stateInfo.move *= num;
			this._actor.StateInfo = stateInfo;
		}

		// Token: 0x0400586F RID: 22639
		public ActorAnimationMerchant CharacterAnimation;

		// Token: 0x04005870 RID: 22640
		public float AccelerationTime = 0.2f;

		// Token: 0x04005871 RID: 22641
		private ActorLocomotion.AnimationState AnimState = default(ActorLocomotion.AnimationState);

		// Token: 0x04005872 RID: 22642
		private Vector3 _moveDirection = Vector3.zero;

		// Token: 0x04005873 RID: 22643
		private Vector3 _moveDirectionVelocity = Vector3.zero;

		// Token: 0x04005874 RID: 22644
		[HideInEditorMode]
		[DisableInPlayMode]
		public int MovePoseID;

		// Token: 0x04005875 RID: 22645
		private static Vector3 ZXOne = new Vector3(1f, 0f, 1f);
	}
}
