using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D30 RID: 3376
	[TaskCategory("")]
	public class EnterActorInFarSight : AgentConditional
	{
		// Token: 0x06006BB2 RID: 27570 RVA: 0x002E2F68 File Offset: 0x002E1368
		public override TaskStatus OnUpdate()
		{
			switch (this._targetType)
			{
			case EnterActorInFarSight.ActorType.Player:
				this.CheckPlayer();
				break;
			case EnterActorInFarSight.ActorType.Agent:
				this.CheckAgent();
				break;
			case EnterActorInFarSight.ActorType.Merchant:
				this.CheckMerchant();
				break;
			}
			if (base.Agent.TargetInSightActor != null)
			{
				switch (this._targetType)
				{
				case EnterActorInFarSight.ActorType.Player:
					if (base.Agent.TargetInSightActor is PlayerActor)
					{
						return TaskStatus.Success;
					}
					break;
				case EnterActorInFarSight.ActorType.Agent:
					if (base.Agent.TargetInSightActor is AgentActor)
					{
						return TaskStatus.Success;
					}
					break;
				case EnterActorInFarSight.ActorType.Merchant:
					if (base.Agent.TargetInSightActor is MerchantActor)
					{
						return TaskStatus.Success;
					}
					break;
				default:
					return TaskStatus.Failure;
				}
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006BB3 RID: 27571 RVA: 0x002E304C File Offset: 0x002E144C
		private void CheckPlayer()
		{
			List<Actor> list = ListPool<Actor>.Get();
			foreach (Actor actor in base.Agent.TargetActors)
			{
				CollisionState collisionState;
				if (actor is PlayerActor && base.Agent.ActorFarCollisionStateTable.TryGetValue(actor.InstanceID, out collisionState) && collisionState == CollisionState.Enter)
				{
					list.Add(actor);
				}
			}
			if (list.Count > 0)
			{
				List<Actor> list2 = ListPool<Actor>.Get();
				foreach (Actor item in list)
				{
					list2.Add(item);
				}
				ActorController capturedInSight = AgentActor.GetCapturedInSight(base.Agent, list2);
				ListPool<Actor>.Release(list2);
				if (capturedInSight == null || !(capturedInSight.Actor is PlayerActor))
				{
					ListPool<Actor>.Release(list);
					return;
				}
				base.Agent.TargetInSightActor = capturedInSight.Actor;
			}
			ListPool<Actor>.Release(list);
		}

		// Token: 0x06006BB4 RID: 27572 RVA: 0x002E3198 File Offset: 0x002E1598
		private void CheckAgent()
		{
			List<Actor> list = ListPool<Actor>.Get();
			foreach (Actor actor in base.Agent.TargetActors)
			{
				if (actor is AgentActor)
				{
					CollisionState collisionState;
					if (base.Agent.ActorFarCollisionStateTable.TryGetValue(actor.InstanceID, out collisionState))
					{
						if ((actor as AgentActor).IsEncounterable)
						{
							if (!actor.IsNeutralCommand)
							{
								if (collisionState == CollisionState.Enter)
								{
									list.Add(actor);
								}
							}
						}
					}
				}
			}
			if (list.Count > 0)
			{
				List<Actor> list2 = ListPool<Actor>.Get();
				foreach (Actor item in list)
				{
					list2.Add(item);
				}
				ActorController capturedInSight = AgentActor.GetCapturedInSight(base.Agent, list2);
				ListPool<Actor>.Release(list2);
				if (capturedInSight == null || !(capturedInSight.Actor is AgentActor))
				{
					ListPool<Actor>.Release(list);
					return;
				}
				AgentActor agentActor = capturedInSight.Actor as AgentActor;
				if (!agentActor.IsNeutralCommand)
				{
					return;
				}
				if (agentActor.CommandPartner != null)
				{
					return;
				}
				if (agentActor.Partner != null)
				{
					return;
				}
				List<Desire.ActionType> encounterWhitelist = Singleton<Manager.Resources>.Instance.AgentProfile.EncounterWhitelist;
				if (!encounterWhitelist.Contains(agentActor.Mode))
				{
					return;
				}
				if (!encounterWhitelist.Contains(agentActor.BehaviorResources.Mode))
				{
					return;
				}
				base.Agent.TargetInSightActor = capturedInSight.Actor;
			}
			ListPool<Actor>.Release(list);
		}

		// Token: 0x06006BB5 RID: 27573 RVA: 0x002E338C File Offset: 0x002E178C
		private void CheckMerchant()
		{
			List<Actor> list = ListPool<Actor>.Get();
			foreach (Actor actor in base.Agent.TargetActors)
			{
				MerchantActor merchantActor = actor as MerchantActor;
				if (!(merchantActor == null))
				{
					CollisionState collisionState;
					if (base.Agent.ActorFarCollisionStateTable.TryGetValue(actor.InstanceID, out collisionState) && collisionState == CollisionState.Enter)
					{
						if (merchantActor.IsNeutralCommand)
						{
							list.Add(actor);
						}
					}
				}
			}
			if (list.IsNullOrEmpty<Actor>())
			{
				ListPool<Actor>.Release(list);
				return;
			}
			List<Actor> list2 = ListPool<Actor>.Get();
			foreach (Actor item in list)
			{
				list2.Add(item);
			}
			ActorController capturedInSight = AgentActor.GetCapturedInSight(base.Agent, list2);
			ListPool<Actor>.Release(list2);
			if (capturedInSight == null || !(capturedInSight.Actor is MerchantActor))
			{
				ListPool<Actor>.Release(list);
				return;
			}
			base.Agent.TargetInSightActor = capturedInSight.Actor;
			ListPool<Actor>.Release(list);
		}

		// Token: 0x04005AA4 RID: 23204
		[SerializeField]
		private EnterActorInFarSight.ActorType _targetType;

		// Token: 0x02000D31 RID: 3377
		private enum ActorType
		{
			// Token: 0x04005AA6 RID: 23206
			None,
			// Token: 0x04005AA7 RID: 23207
			Player,
			// Token: 0x04005AA8 RID: 23208
			Agent,
			// Token: 0x04005AA9 RID: 23209
			Merchant
		}
	}
}
