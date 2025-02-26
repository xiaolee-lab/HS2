using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D32 RID: 3378
	[TaskCategory("")]
	public class EnterActorInSight : AgentConditional
	{
		// Token: 0x06006BB7 RID: 27575 RVA: 0x002E3504 File Offset: 0x002E1904
		public override TaskStatus OnUpdate()
		{
			switch (this._targetType)
			{
			case EnterActorInSight.ActorType.Player:
				this.CheckPlayer();
				break;
			case EnterActorInSight.ActorType.Agent:
				this.CheckAgent();
				break;
			case EnterActorInSight.ActorType.Merchant:
				this.CheckMerchant();
				break;
			}
			if (base.Agent.TargetInSightActor != null)
			{
				switch (this._targetType)
				{
				case EnterActorInSight.ActorType.Player:
					if (base.Agent.TargetInSightActor is PlayerActor)
					{
						return TaskStatus.Success;
					}
					break;
				case EnterActorInSight.ActorType.Agent:
					if (base.Agent.TargetInSightActor is AgentActor)
					{
						return TaskStatus.Success;
					}
					break;
				case EnterActorInSight.ActorType.Merchant:
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

		// Token: 0x06006BB8 RID: 27576 RVA: 0x002E35E8 File Offset: 0x002E19E8
		private void CheckPlayer()
		{
			List<Actor> list = ListPool<Actor>.Get();
			foreach (Actor actor in base.Agent.TargetActors)
			{
				CollisionState collisionState;
				if (actor is PlayerActor && base.Agent.ActorCollisionStateTable.TryGetValue(actor.InstanceID, out collisionState) && collisionState == CollisionState.Enter)
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
				if (!capturedInSight.Actor.IsNeutralCommand)
				{
					return;
				}
				base.Agent.TargetInSightActor = capturedInSight.Actor;
			}
			ListPool<Actor>.Release(list);
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x002E3748 File Offset: 0x002E1B48
		private void CheckAgent()
		{
			List<Desire.ActionType> encounterWhitelist = Singleton<Manager.Resources>.Instance.AgentProfile.EncounterWhitelist;
			List<Actor> list = ListPool<Actor>.Get();
			foreach (Actor actor in base.Agent.TargetActors)
			{
				if (actor is AgentActor)
				{
					CollisionState collisionState;
					if (base.Agent.ActorCollisionStateTable.TryGetValue(actor.InstanceID, out collisionState))
					{
						if ((actor as AgentActor).IsEncounterable)
						{
							if (collisionState == CollisionState.Enter)
							{
								list.Add(actor);
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
				ListPool<Actor>.Release(list);
				if (capturedInSight == null || !(capturedInSight.Actor is AgentActor))
				{
					return;
				}
				AgentActor agentActor = capturedInSight.Actor as AgentActor;
				if (!capturedInSight.Actor.IsNeutralCommand)
				{
					return;
				}
				if (agentActor.CommandPartner != null)
				{
					return;
				}
				if (capturedInSight.Actor.Partner != null)
				{
					return;
				}
				if (!encounterWhitelist.Contains(capturedInSight.Actor.Mode))
				{
					return;
				}
				if (!encounterWhitelist.Contains(agentActor.BehaviorResources.Mode))
				{
					return;
				}
				base.Agent.TargetInSightActor = capturedInSight.Actor;
			}
			else
			{
				ListPool<Actor>.Release(list);
			}
		}

		// Token: 0x06006BBA RID: 27578 RVA: 0x002E3940 File Offset: 0x002E1D40
		private void CheckMerchant()
		{
			List<Actor> list = ListPool<Actor>.Get();
			foreach (Actor actor in base.Agent.TargetActors)
			{
				MerchantActor merchantActor = actor as MerchantActor;
				if (!(merchantActor == null))
				{
					CollisionState collisionState;
					if (base.Agent.ActorCollisionStateTable.TryGetValue(actor.InstanceID, out collisionState) && collisionState == CollisionState.Enter)
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

		// Token: 0x04005AAA RID: 23210
		[SerializeField]
		private EnterActorInSight.ActorType _targetType;

		// Token: 0x02000D33 RID: 3379
		private enum ActorType
		{
			// Token: 0x04005AAC RID: 23212
			None,
			// Token: 0x04005AAD RID: 23213
			Player,
			// Token: 0x04005AAE RID: 23214
			Agent,
			// Token: 0x04005AAF RID: 23215
			Merchant
		}
	}
}
