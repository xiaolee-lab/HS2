using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D3D RID: 3389
	[TaskCategory("")]
	public class ExistsInvader : AgentConditional
	{
		// Token: 0x06006BDB RID: 27611 RVA: 0x002E4B70 File Offset: 0x002E2F70
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			PlayerActor player = Singleton<Map>.Instance.Player;
			Map instance = Singleton<Map>.Instance;
			float num = Vector3.Distance(instance.Player.Position, base.Agent.Position);
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			CollisionState collisionState;
			if (num < locomotionProfile.AccessInvasionRange && agent.ActorCollisionStateTable.TryGetValue(player.InstanceID, out collisionState))
			{
				if (collisionState == CollisionState.Enter || collisionState == CollisionState.Stay)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}
	}
}
