using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000BFB RID: 3067
	public class DoorObstacleData : MonoBehaviour
	{
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x0027B718 File Offset: 0x00279B18
		public static ReadOnlyDictionary<int, DoorObstacleData.DoorObstaclePack> Table { get; } = new ReadOnlyDictionary<int, DoorObstacleData.DoorObstaclePack>(DoorObstacleData._table);

		// Token: 0x06005E0F RID: 24079 RVA: 0x0027B720 File Offset: 0x00279B20
		private void Awake()
		{
			DoorObstacleData._table[this._id] = new DoorObstacleData.DoorObstaclePack
			{
				OpenRight = this._obstacleInOpenRight,
				OpenLeft = this._obstacleInOpenLeft,
				Close = this._obstacleInClose
			};
		}

		// Token: 0x0400540B RID: 21515
		private static Dictionary<int, DoorObstacleData.DoorObstaclePack> _table = new Dictionary<int, DoorObstacleData.DoorObstaclePack>();

		// Token: 0x0400540C RID: 21516
		[SerializeField]
		private int _id;

		// Token: 0x0400540D RID: 21517
		[SerializeField]
		private NavMeshObstacle _obstacleInOpenRight;

		// Token: 0x0400540E RID: 21518
		[SerializeField]
		private NavMeshObstacle _obstacleInOpenLeft;

		// Token: 0x0400540F RID: 21519
		[SerializeField]
		private NavMeshObstacle _obstacleInClose;

		// Token: 0x02000BFC RID: 3068
		[Serializable]
		public class DoorObstaclePack
		{
			// Token: 0x17001216 RID: 4630
			// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0027B770 File Offset: 0x00279B70
			// (set) Token: 0x06005E12 RID: 24082 RVA: 0x0027B778 File Offset: 0x00279B78
			public NavMeshObstacle OpenRight { get; set; }

			// Token: 0x17001217 RID: 4631
			// (get) Token: 0x06005E13 RID: 24083 RVA: 0x0027B781 File Offset: 0x00279B81
			// (set) Token: 0x06005E14 RID: 24084 RVA: 0x0027B789 File Offset: 0x00279B89
			public NavMeshObstacle OpenLeft { get; set; }

			// Token: 0x17001218 RID: 4632
			// (get) Token: 0x06005E15 RID: 24085 RVA: 0x0027B792 File Offset: 0x00279B92
			// (set) Token: 0x06005E16 RID: 24086 RVA: 0x0027B79A File Offset: 0x00279B9A
			public NavMeshObstacle Close { get; set; }
		}
	}
}
