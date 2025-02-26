using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D05 RID: 3333
	[TaskCategory("")]
	public class MoveTowardArea : AgentAction
	{
		// Token: 0x06006B06 RID: 27398 RVA: 0x002DBC14 File Offset: 0x002DA014
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.ActivateTransfer(true);
			Chunk chunk;
			if (!Singleton<Map>.Instance.ChunkTable.TryGetValue(0, out chunk))
			{
				return;
			}
			MapArea mapArea = null;
			foreach (MapArea mapArea2 in chunk.MapAreas)
			{
				if (mapArea2.Type == this._area)
				{
					mapArea = mapArea2;
					break;
				}
			}
			if (mapArea == null)
			{
				return;
			}
			int index = UnityEngine.Random.Range(0, mapArea.Waypoints.Count);
			Waypoint element = mapArea.Waypoints.GetElement(index);
			if (element == null)
			{
				return;
			}
			this._destination = element;
			base.Agent.NavMeshAgent.SetDestination(this._destination.transform.position);
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x002DBCF0 File Offset: 0x002DA0F0
		public override TaskStatus OnUpdate()
		{
			if (this._destination == null)
			{
				return TaskStatus.Failure;
			}
			float num = Vector3.Distance(this._destination.transform.position, base.Agent.Position);
			if (num > this._contactedRadius)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005A51 RID: 23121
		[SerializeField]
		private MapArea.AreaType _area = MapArea.AreaType.Indoor;

		// Token: 0x04005A52 RID: 23122
		[SerializeField]
		private float _contactedRadius = 1f;

		// Token: 0x04005A53 RID: 23123
		private Waypoint _destination;
	}
}
