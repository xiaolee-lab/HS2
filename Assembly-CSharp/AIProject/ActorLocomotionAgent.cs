using System;
using AIProject.Definitions;
using AIProject.SaveData;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C65 RID: 3173
	public class ActorLocomotionAgent : ActorLocomotion
	{
		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x0600667D RID: 26237 RVA: 0x002B91DB File Offset: 0x002B75DB
		// (set) Token: 0x0600667E RID: 26238 RVA: 0x002B91E3 File Offset: 0x002B75E3
		public ActorLocomotion.AnimationState AnimState { get; set; }

		// Token: 0x0600667F RID: 26239 RVA: 0x002B91EC File Offset: 0x002B75EC
		private void OnEnable()
		{
			base.Start();
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x002B91F4 File Offset: 0x002B75F4
		private void OnDisable()
		{
			this.AnimState.Init();
			this._moveDirection = Vector3.zero;
			this._moveDirectionVelocity = Vector3.zero;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x002B9228 File Offset: 0x002B7628
		public override void Move(Vector3 deltaPosition)
		{
			if (this._actor.IsKinematic)
			{
				return;
			}
			if (Mathf.Approximately(Time.timeScale, 0f))
			{
				return;
			}
			if (Mathf.Approximately(Time.deltaTime, 0f))
			{
				return;
			}
			float b = this._actor.IsOnGround ? base.GetSlopeDamper(-deltaPosition / Time.deltaTime, this._actor.Normal) : 1f;
			this._actor.ForwardMLP = Mathf.Lerp(this._actor.ForwardMLP, b, Time.deltaTime * 5f);
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x002B92D4 File Offset: 0x002B76D4
		public void UpdateState()
		{
			this.CalcAnimSpeed();
			this.Look();
			this.GroundCheck();
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			AgentActor agentActor = this._actor as AgentActor;
			StuffItem carryingItem = agentActor.AgentData.CarryingItem;
			int num;
			if (carryingItem != null && !agentProfile.CanStandEatItems.Exists((ItemIDKeyPair pair) => pair.categoryID == carryingItem.CategoryID && pair.itemID == carryingItem.ID))
			{
				num = 0;
			}
			else
			{
				int id = agentActor.AgentData.SickState.ID;
				Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
				if (id != 3)
				{
					if (id != 4)
					{
						StuffItem equipedUmbrellaItem = agentActor.AgentData.EquipedUmbrellaItem;
						CommonDefine.ItemIDDefines itemIDDefine = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine;
						if (equipedUmbrellaItem != null && equipedUmbrellaItem.CategoryID == itemIDDefine.UmbrellaID.categoryID && equipedUmbrellaItem.ID == itemIDDefine.UmbrellaID.itemID)
						{
							if (weather != Weather.Rain && weather != Weather.Storm)
							{
								this.SetLocomotionInfo(agentActor, weather, out num);
							}
							else
							{
								num = 0;
							}
						}
						else
						{
							this.SetLocomotionInfo(agentActor, weather, out num);
						}
					}
					else
					{
						num = 0;
					}
				}
				else
				{
					num = 1;
				}
			}
			ActorLocomotion.AnimationState animState = this.AnimState;
			NavMeshAgent navMeshAgent = this._actor.NavMeshAgent;
			if (agentActor.Mode == Desire.ActionType.Date)
			{
				if (navMeshAgent.enabled && !navMeshAgent.pathPending)
				{
					if (navMeshAgent.remainingDistance > agentProfile.RunDistance)
					{
						if (!agentActor.IsRunning)
						{
							agentActor.IsRunning = true;
						}
					}
					float b;
					if (agentActor.IsRunning)
					{
						animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.followRunSpeed;
						b = locomotionProfile.AgentSpeed.followRunSpeed;
					}
					else
					{
						animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
						b = locomotionProfile.AgentSpeed.walkSpeed;
					}
					animState.setMediumOnWalk = true;
					animState.medVelocity = locomotionProfile.AgentSpeed.walkSpeed;
					animState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
					navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, b, locomotionProfile.LerpSpeed);
				}
				else
				{
					animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
				}
			}
			else if (agentActor.Mode == Desire.ActionType.TakeHPoint || agentActor.Mode == Desire.ActionType.ChaseYobai || agentActor.Mode == Desire.ActionType.ComeSleepTogether)
			{
				if (navMeshAgent.enabled && !navMeshAgent.pathPending)
				{
					navMeshAgent.speed = locomotionProfile.AgentSpeed.runSpeed;
				}
				animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
				animState.setMediumOnWalk = true;
				animState.medVelocity = locomotionProfile.AgentSpeed.walkSpeed;
				animState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
			}
			else if (agentActor.Mode == Desire.ActionType.WalkWithAgentFollow || agentActor.BehaviorResources.Mode == Desire.ActionType.WalkWithAgentFollow)
			{
				if (navMeshAgent.enabled && !navMeshAgent.pathPending)
				{
					float b2;
					if (navMeshAgent.remainingDistance > agentProfile.RunDistance)
					{
						animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
						b2 = locomotionProfile.AgentSpeed.runSpeed;
					}
					else
					{
						animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
						b2 = locomotionProfile.AgentSpeed.walkSpeed;
					}
					animState.setMediumOnWalk = true;
					animState.medVelocity = locomotionProfile.AgentSpeed.walkSpeed;
					animState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
					navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, b2, locomotionProfile.LerpSpeed);
				}
				else
				{
					animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.followRunSpeed;
				}
			}
			else if (this._actor.Mode == Desire.ActionType.Escape)
			{
				if (navMeshAgent.enabled && !navMeshAgent.pathPending)
				{
					float escapeSpeed = locomotionProfile.AgentSpeed.escapeSpeed;
					animState.moveDirection = this.MoveDirection * escapeSpeed;
					navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, escapeSpeed, locomotionProfile.LerpSpeed);
				}
			}
			else
			{
				if (agentActor.TutorialMode)
				{
					int tutorialProgress = Manager.Map.GetTutorialProgress();
					if (tutorialProgress == 14 || tutorialProgress == 15)
					{
						num = agentActor.TutorialLocomoCaseID;
					}
				}
				switch (num)
				{
				case 0:
					animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
					animState.setMediumOnWalk = false;
					animState.maxVelocity = locomotionProfile.AgentSpeed.walkSpeed;
					break;
				case 1:
					animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
					animState.setMediumOnWalk = true;
					animState.medVelocity = locomotionProfile.AgentSpeed.walkSpeed;
					animState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
					break;
				case 2:
					animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
					animState.setMediumOnWalk = false;
					animState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
					break;
				default:
					if (num != 100)
					{
						if (num != 101)
						{
							float num2 = agentActor.AgentData.StatsTable[5];
							float num3 = num2 * agentProfile.MustRunMotivationPercent;
							int desireKey = Desire.GetDesireKey(agentActor.RequestedDesire);
							float? motivation = agentActor.GetMotivation(desireKey);
							if (motivation != null && motivation.Value < num3)
							{
								animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
							}
							else if (agentActor.MapArea != null)
							{
								int areaID = agentActor.MapArea.AreaID;
								if (agentActor.TargetInSightActionPoint != null)
								{
									if (agentActor.TargetInSightActionPoint.OwnerArea.AreaID == areaID)
									{
										animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
									}
									else
									{
										animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
									}
								}
								else if (agentActor.DestWaypoint != null)
								{
									if (agentActor.DestWaypoint.OwnerArea.AreaID == areaID)
									{
										animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
									}
									else
									{
										animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.runSpeed;
									}
								}
								else
								{
									animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
								}
							}
							else
							{
								animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.walkSpeed;
							}
							animState.setMediumOnWalk = true;
							animState.medVelocity = locomotionProfile.AgentSpeed.walkSpeed;
							animState.maxVelocity = locomotionProfile.AgentSpeed.runSpeed;
							animState.onGround = this._actor.IsOnGround;
						}
						else
						{
							animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.tutorialRunSpeed;
							animState.setMediumOnWalk = false;
							animState.maxVelocity = locomotionProfile.AgentSpeed.tutorialRunSpeed;
						}
					}
					else
					{
						animState.moveDirection = this.MoveDirection * locomotionProfile.AgentSpeed.tutorialWalkSpeed;
						animState.setMediumOnWalk = true;
						animState.medVelocity = locomotionProfile.AgentSpeed.tutorialWalkSpeed;
						animState.maxVelocity = locomotionProfile.AgentSpeed.tutorialRunSpeed;
					}
					break;
				}
				agentActor.UpdateLocomotionSpeed(agentActor.DestWaypoint);
			}
			this.AnimState = animState;
			this.CharacterAnimation.UpdateState(animState);
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x002B9BE0 File Offset: 0x002B7FE0
		private void SetLocomotionInfo(AgentActor agent, Weather weather, out int caseID)
		{
			Desire.ActionType mode = agent.Mode;
			if (mode != Desire.ActionType.EndTaskGift && mode != Desire.ActionType.EndTaskH)
			{
				if (weather != Weather.Rain && weather != Weather.Storm)
				{
					caseID = -1;
				}
				else
				{
					caseID = 2;
				}
			}
			else
			{
				caseID = 0;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x002B9C3C File Offset: 0x002B803C
		public Vector3 MoveDirection
		{
			get
			{
				Vector3 moveDirection = this._moveDirection;
				Vector3 vector = new Vector3(0f, 0f, this._actor.StateInfo.move.magnitude);
				this._moveDirection = Vector3.SmoothDamp(moveDirection, vector.normalized, ref this._moveDirectionVelocity, this.AccelerationTime);
				return this._moveDirection;
			}
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x002B9C9C File Offset: 0x002B809C
		private void CalcAnimSpeed()
		{
			Actor.InputInfo stateInfo = default(Actor.InputInfo);
			stateInfo.move = Vector3.Scale(this._actor.NavMeshAgent.velocity, ActorLocomotionAgent._zxOne).normalized;
			this._actor.StateInfo = stateInfo;
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x002B9CE8 File Offset: 0x002B80E8
		private void Look()
		{
			float num = base.GetAngleFromForward(this.LookDirection);
			if (this._actor.StateInfo.move == Vector3.zero)
			{
				num *= (1f - Mathf.Abs(num) / 180f) * this._stationaryTurnSpeedMlp;
			}
			base.RigidbodyRotateAround(this.CharacterAnimation.GetPivotPoint(), base.transform.up, num * Time.deltaTime * this._movingTurnSpeed);
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x002B9D6C File Offset: 0x002B816C
		private Vector3 LookDirection
		{
			get
			{
				bool flag = this._actor.StateInfo.move != Vector3.zero;
				return (!flag) ? base.transform.forward : this._actor.StateInfo.move;
			}
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x002B9DC0 File Offset: 0x002B81C0
		private RaycastHit GetHit()
		{
			return this.GetSpherecastHit();
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x002B9DC8 File Offset: 0x002B81C8
		private void GroundCheck()
		{
			Vector3 b = Vector3.zero;
			RaycastHit hit = this.GetHit();
			this._actor.Normal = hit.normal;
			bool isOnGround = this._actor.IsOnGround;
			this._actor.IsOnGround = false;
			float num = isOnGround ? this._airbornThreshold : (this._airbornThreshold * 0.5f);
			if (this._groundDistance < num && hit.collider != null)
			{
				if (hit.rigidbody != null)
				{
					b = hit.rigidbody.GetPointVelocity(hit.point);
				}
				this._actor.IsOnGround = true;
			}
			this._platformVelocity = Vector3.Lerp(this._platformVelocity, b, Time.deltaTime * this._platformFriction);
		}

		// Token: 0x04005862 RID: 22626
		[SerializeField]
		private float _stationaryTurnSpeedMlp = 1f;

		// Token: 0x04005863 RID: 22627
		[SerializeField]
		private float _movingTurnSpeed = 5f;

		// Token: 0x04005864 RID: 22628
		[SerializeField]
		private float _platformFriction = 7f;

		// Token: 0x04005865 RID: 22629
		public ActorAnimationAgent CharacterAnimation;

		// Token: 0x04005867 RID: 22631
		private float _groundDistance;

		// Token: 0x04005868 RID: 22632
		[SerializeField]
		[Range(1f, 4f)]
		private float _gravityMultiplier = 2f;

		// Token: 0x04005869 RID: 22633
		public float AccelerationTime = 0.2f;

		// Token: 0x0400586A RID: 22634
		private Vector3 _moveDirection = Vector3.zero;

		// Token: 0x0400586B RID: 22635
		private Vector3 _moveDirectionVelocity = Vector3.zero;

		// Token: 0x0400586C RID: 22636
		private Vector3 _platformVelocity = Vector3.zero;

		// Token: 0x0400586D RID: 22637
		private float _lastHeight;

		// Token: 0x0400586E RID: 22638
		private static readonly Vector3 _zxOne = new Vector3(1f, 0f, 1f);
	}
}
