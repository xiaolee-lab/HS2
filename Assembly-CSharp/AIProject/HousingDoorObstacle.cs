using System;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000BDF RID: 3039
	[RequireComponent(typeof(DoorPoint))]
	public class HousingDoorObstacle : DoorObstacle
	{
		// Token: 0x06005CFF RID: 23807 RVA: 0x00275D2C File Offset: 0x0027412C
		protected override void OnStart()
		{
			DoorPoint component = base.GetComponent<DoorPoint>();
			component.ObstacleInOpenRight = this._obstacleInOpenRight;
			component.ObstacleInOpenLeft = this._obstacleInOpenLeft;
			component.ObstacleInClose = this._obstacleInClose;
		}

		// Token: 0x04005374 RID: 21364
		[SerializeField]
		private NavMeshObstacle _obstacleInOpenRight;

		// Token: 0x04005375 RID: 21365
		[SerializeField]
		private NavMeshObstacle _obstacleInOpenLeft;

		// Token: 0x04005376 RID: 21366
		[SerializeField]
		private NavMeshObstacle _obstacleInClose;
	}
}
