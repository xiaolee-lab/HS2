using System;
using System.Collections.Generic;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E22 RID: 3618
	public class SearchArea : MonoBehaviour
	{
		// Token: 0x0600704E RID: 28750 RVA: 0x002FFAF5 File Offset: 0x002FDEF5
		private void Awake()
		{
			if (this._agent == null)
			{
				this._agent = base.GetComponentInChildren<AgentActor>();
			}
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x002FFB14 File Offset: 0x002FDF14
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x002FFB4C File Offset: 0x002FDF4C
		private void OnUpdate()
		{
			if (this._agent == null)
			{
				return;
			}
			foreach (ActionPoint actionPoint in this._searchPoints)
			{
				if (!(actionPoint == null))
				{
					int instanceID = actionPoint.InstanceID;
					CollisionState collisionState;
					if (!this._collisionStateTable.TryGetValue(instanceID, out collisionState))
					{
						CollisionState collisionState2 = CollisionState.None;
						this._collisionStateTable[instanceID] = collisionState2;
						collisionState = collisionState2;
					}
					float num = Vector3.Distance(base.transform.position, actionPoint.CommandCenter);
					if (num < this._radius + actionPoint.Radius)
					{
						switch (collisionState)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this._collisionStateTable[instanceID] = CollisionState.Enter;
							this.OnEnter(actionPoint);
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this._collisionStateTable[instanceID] = CollisionState.Stay;
							break;
						}
					}
					else
					{
						switch (collisionState)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this._collisionStateTable[instanceID] = CollisionState.None;
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this._collisionStateTable[instanceID] = CollisionState.Exit;
							this.OnExit(actionPoint);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06007051 RID: 28753 RVA: 0x002FFCBC File Offset: 0x002FE0BC
		private void OnEnter(ActionPoint point)
		{
			if (point == null)
			{
				return;
			}
			if (point.Actor != null)
			{
				return;
			}
			this._agent.AddActionPoint(point);
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x002FFCE9 File Offset: 0x002FE0E9
		private void OnExit(ActionPoint point)
		{
			if (((point != null) ? point.Actor : null) == null)
			{
				return;
			}
			this._agent.RemoveActionPoint(point);
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x002FFD14 File Offset: 0x002FE114
		public void RefreshQueryPoints()
		{
			ActionPoint[] actionPoints = Singleton<Map>.Instance.PointAgent.ActionPoints;
			this._searchPoints.Clear();
			this._searchPoints.AddRange(actionPoints);
		}

		// Token: 0x06007054 RID: 28756 RVA: 0x002FFD48 File Offset: 0x002FE148
		public void AddPoint(ActionPoint ap)
		{
			if (ap == null)
			{
				return;
			}
			this._searchPoints.Add(ap);
		}

		// Token: 0x06007055 RID: 28757 RVA: 0x002FFD63 File Offset: 0x002FE163
		public void AddPoints(ActionPoint[] ap)
		{
			this._searchPoints.AddRange(ap);
		}

		// Token: 0x06007056 RID: 28758 RVA: 0x002FFD71 File Offset: 0x002FE171
		public bool RemovePoint(ActionPoint ap)
		{
			return !(ap == null) && this._searchPoints.Remove(ap);
		}

		// Token: 0x04005C55 RID: 23637
		[SerializeField]
		private AgentActor _agent;

		// Token: 0x04005C56 RID: 23638
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x04005C57 RID: 23639
		private Dictionary<int, CollisionState> _collisionStateTable = new Dictionary<int, CollisionState>();

		// Token: 0x04005C58 RID: 23640
		private List<ActionPoint> _searchPoints = new List<ActionPoint>();
	}
}
