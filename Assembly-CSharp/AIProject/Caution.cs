using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB0 RID: 3248
	[TaskCategory("")]
	public class Caution : AgentAction
	{
		// Token: 0x0600697A RID: 27002 RVA: 0x002CE9E6 File Offset: 0x002CCDE6
		public override void OnStart()
		{
			this._currentTime = 0f;
			base.OnStart();
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x002CE9FC File Offset: 0x002CCDFC
		public override TaskStatus OnUpdate()
		{
			Dictionary<int, CollisionState> actorCollisionStateTable = base.Agent.ActorCollisionStateTable;
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
				ActorController capturedInSight = this.GetCapturedInSight(base.Agent, list2.ToArray());
				ListPool<Actor>.Release(list2);
				if (capturedInSight == null || !(capturedInSight.Actor is PlayerActor))
				{
					ListPool<Actor>.Release(list);
					return TaskStatus.Failure;
				}
				base.Agent.TargetInSightActor = capturedInSight.Actor;
			}
			if (base.Agent.TargetInSightActor != null)
			{
				return TaskStatus.Success;
			}
			if (this._currentTime > Singleton<Manager.Resources>.Instance.LocomotionProfile.TimeToBeware)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x002CEB8C File Offset: 0x002CCF8C
		private ActorController GetCapturedInSight(AgentActor agent, Actor[] actors)
		{
			Actor element = actors.GetElement(UnityEngine.Random.Range(0, actors.Length));
			if (element == null)
			{
				return null;
			}
			ActorController controller = element.Controller;
			if (controller == null)
			{
				return null;
			}
			Vector3 position = agent.FovTargetPointTable[Actor.FovBodyPart.Head].position;
			foreach (Transform transform in element.FovTargetPoints)
			{
				Vector3 direction = position - transform.position;
				Ray ray = new Ray(position, direction);
				if (Physics.Raycast(ray, direction.magnitude, Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.MapLayer))
				{
					return null;
				}
			}
			return controller;
		}

		// Token: 0x040059AE RID: 22958
		private float _currentTime;
	}
}
