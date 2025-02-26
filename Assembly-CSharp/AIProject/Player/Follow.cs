using System;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Player
{
	// Token: 0x02000DDC RID: 3548
	public class Follow : PlayerStateBase
	{
		// Token: 0x06006DBC RID: 28092 RVA: 0x002EB948 File Offset: 0x002E9D48
		protected override void OnAwake(PlayerActor player)
		{
			this._prevAcceptionState = MapUIContainer.CommandLabel.Acception;
			if (this._prevAcceptionState != CommandLabel.AcceptionState.None)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			}
			player.Animation.RefsActAnimInfo = true;
			player.ActivateTransfer();
			player.SetActiveOnEquipedItem(true);
			player.ResetCoolTime();
			if (player.CameraControl.Mode == CameraMode.ActionNotMove || player.CameraControl.Mode == CameraMode.ActionFreeLook)
			{
				player.CameraControl.Mode = CameraMode.Normal;
				player.CameraControl.RecoverShotType();
			}
			this._prevAvoidancePriority = player.NavMeshAgent.avoidancePriority;
			player.NavMeshAgent.avoidancePriority = 99;
			Vector3 destination = this.DesiredPosition(player.Partner);
			this.SetDestination(player, destination);
			player.Partner.NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x002EBA14 File Offset: 0x002E9E14
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x002EBA24 File Offset: 0x002E9E24
		protected override void OnRelease(PlayerActor player)
		{
			IState state = player.PlayerController.State;
			if (state is Normal || state is Onbu)
			{
				this._prevAcceptionState = CommandLabel.AcceptionState.InvokeAcception;
			}
			player.NavMeshAgent.avoidancePriority = this._prevAvoidancePriority;
			if (player.Partner != null)
			{
				player.Partner.NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
			}
			if (this._prevAcceptionState != MapUIContainer.CommandLabel.Acception)
			{
				MapUIContainer.SetCommandLabelAcception(this._prevAcceptionState);
			}
		}

		// Token: 0x06006DBF RID: 28095 RVA: 0x002EBAB0 File Offset: 0x002E9EB0
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			AgentActor agentPartner = player.AgentPartner;
			if (agentPartner == null)
			{
				return;
			}
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			NavMeshAgent navMeshAgent = player.NavMeshAgent;
			if (navMeshAgent.isOnOffMeshLink)
			{
				this.Stop(player);
				if (navMeshAgent.currentOffMeshLinkData.offMeshLink != null)
				{
					NavMeshAgent navMeshAgent2 = player.NavMeshAgent;
					ActionPoint actionPoint;
					if (navMeshAgent2 == null)
					{
						actionPoint = null;
					}
					else
					{
						OffMeshLink offMeshLink = navMeshAgent2.currentOffMeshLinkData.offMeshLink;
						actionPoint = ((offMeshLink != null) ? offMeshLink.GetComponent<ActionPoint>() : null);
					}
					ActionPoint actionPoint2 = actionPoint;
					if (actionPoint2 != null && actionPoint2.OffMeshAvailablePoint(player))
					{
						if (actionPoint2 is DoorPoint)
						{
							player.CurrentPoint = actionPoint2;
							player.PlayerController.ChangeState("DoorOpen", actionPoint2, null);
						}
						else
						{
							player.CurrentPoint = actionPoint2;
							player.PlayerController.ChangeState("Move", actionPoint2, null);
						}
					}
				}
			}
			else
			{
				Vector3 vector = this.DesiredPosition(agentPartner);
				if (Vector3.Distance(vector, player.Position) >= agentProfile.RestDistance)
				{
					this.SetDestination(player, vector);
					this._moved = true;
				}
				else
				{
					NavMeshPathStatus pathStatus = navMeshAgent.pathStatus;
					if (pathStatus != NavMeshPathStatus.PathPartial && pathStatus != NavMeshPathStatus.PathInvalid)
					{
						if (!navMeshAgent.pathPending)
						{
							if (navMeshAgent.remainingDistance < agentProfile.RestDistance && player.IsRunning)
							{
								player.IsRunning = false;
							}
							if (this._moved && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
							{
								this.Stop(player);
								this._moved = false;
							}
						}
					}
					else if (Vector3.Distance(player.Position, agentPartner.Position) < agentProfile.RestDistance)
					{
						this.Stop(player);
						if (player.IsRunning)
						{
							player.IsRunning = false;
						}
					}
				}
			}
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x002EBC9A File Offset: 0x002EA09A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x002EBCA9 File Offset: 0x002EA0A9
		private Vector3 DesiredPosition(Actor partner)
		{
			return partner.Position + partner.Rotation * Vector3.back;
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x002EBCC6 File Offset: 0x002EA0C6
		private bool SetDestination(PlayerActor player, Vector3 destination)
		{
			if (player.NavMeshAgent.isStopped)
			{
				player.NavMeshAgent.isStopped = false;
			}
			return player.NavMeshAgent.SetDestination(destination);
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x002EBCF0 File Offset: 0x002EA0F0
		private void Stop(PlayerActor player)
		{
			if (player.NavMeshAgent.hasPath)
			{
				player.NavMeshAgent.isStopped = true;
			}
		}

		// Token: 0x04005B4E RID: 23374
		private CommandLabel.AcceptionState _prevAcceptionState;

		// Token: 0x04005B4F RID: 23375
		private bool _moved;

		// Token: 0x04005B50 RID: 23376
		private int _prevAvoidancePriority;
	}
}
