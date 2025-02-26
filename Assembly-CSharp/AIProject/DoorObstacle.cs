using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD9 RID: 3033
	[RequireComponent(typeof(DoorPoint))]
	public class DoorObstacle : ActionPointComponentBase
	{
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x00275A2B File Offset: 0x00273E2B
		public DoorObstacleData.DoorObstaclePack Data
		{
			[CompilerGenerated]
			get
			{
				return this._data;
			}
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x00275A34 File Offset: 0x00273E34
		protected override void OnStart()
		{
			this._data = DoorObstacleData.Table[this._id];
			DoorPoint component = base.GetComponent<DoorPoint>();
			component.ObstacleInOpenRight = this._data.OpenRight;
			component.ObstacleInOpenLeft = this._data.OpenLeft;
			component.ObstacleInClose = this._data.Close;
		}

		// Token: 0x0400536D RID: 21357
		[SerializeField]
		private int _id;

		// Token: 0x0400536E RID: 21358
		[SerializeField]
		private DoorObstacleData.DoorObstaclePack _data;
	}
}
