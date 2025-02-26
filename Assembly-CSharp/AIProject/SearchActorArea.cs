using System;
using System.Collections.Generic;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E21 RID: 3617
	public class SearchActorArea : MonoBehaviour
	{
		// Token: 0x06007047 RID: 28743 RVA: 0x002FF832 File Offset: 0x002FDC32
		private void Awake()
		{
			if (this._agent == null)
			{
				this._agent = base.GetComponentInChildren<AgentActor>();
			}
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x002FF851 File Offset: 0x002FDC51
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x002FF886 File Offset: 0x002FDC86
		private void SetCollisionState(int key, CollisionState state)
		{
			this._collisionStateTable[key] = state;
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x002FF898 File Offset: 0x002FDC98
		private void OnUpdate()
		{
			if (this._agent == null)
			{
				return;
			}
			float searchActorRadius = Singleton<Manager.Resources>.Instance.LocomotionProfile.SearchActorRadius;
			base.transform.position = this._agent.Position;
			Vector3 b = this._agent.Position + this._agent.Rotation * this._centerOffset;
			foreach (Actor actor in this._agent.TargetActors)
			{
				float num = Vector3.Distance(actor.Position, b);
				float num2 = this._radius + searchActorRadius;
				int instanceID = actor.InstanceID;
				CollisionState collisionState;
				if (!this._collisionStateTable.TryGetValue(instanceID, out collisionState))
				{
					CollisionState collisionState2 = CollisionState.None;
					this._collisionStateTable[instanceID] = collisionState2;
					collisionState = collisionState2;
				}
				if (num <= num2)
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(instanceID, CollisionState.Enter);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(instanceID, CollisionState.Stay);
						break;
					}
				}
				else
				{
					switch (collisionState)
					{
					case CollisionState.None:
					case CollisionState.Exit:
						this.SetCollisionState(instanceID, CollisionState.None);
						break;
					case CollisionState.Enter:
					case CollisionState.Stay:
						this.SetCollisionState(instanceID, CollisionState.Exit);
						break;
					}
				}
			}
			foreach (Actor actor2 in this._agent.TargetActors)
			{
				int instanceID2 = actor2.InstanceID;
				CollisionState collisionState3;
				if (this._collisionStateTable.TryGetValue(instanceID2, out collisionState3))
				{
					if (collisionState3 == CollisionState.Enter || collisionState3 == CollisionState.Exit)
					{
						if (collisionState3 != CollisionState.Enter)
						{
							if (collisionState3 != CollisionState.Exit)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x04005C51 RID: 23633
		[SerializeField]
		private AgentActor _agent;

		// Token: 0x04005C52 RID: 23634
		[SerializeField]
		private Vector3 _centerOffset = Vector3.zero;

		// Token: 0x04005C53 RID: 23635
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x04005C54 RID: 23636
		private Dictionary<int, CollisionState> _collisionStateTable = new Dictionary<int, CollisionState>();
	}
}
