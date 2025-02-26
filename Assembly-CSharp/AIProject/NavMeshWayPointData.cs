using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F45 RID: 3909
	public class NavMeshWayPointData : SerializedScriptableObject
	{
		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x0600815D RID: 33117 RVA: 0x0036DBC1 File Offset: 0x0036BFC1
		// (set) Token: 0x0600815E RID: 33118 RVA: 0x0036DBC9 File Offset: 0x0036BFC9
		public int MapID
		{
			get
			{
				return this._mapID;
			}
			set
			{
				this._mapID = value;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x0600815F RID: 33119 RVA: 0x0036DBD2 File Offset: 0x0036BFD2
		public int AreaID
		{
			[CompilerGenerated]
			get
			{
				return this._areaID;
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06008160 RID: 33120 RVA: 0x0036DBDA File Offset: 0x0036BFDA
		public Vector3[] Points
		{
			[CompilerGenerated]
			get
			{
				return this._wayData;
			}
		}

		// Token: 0x06008161 RID: 33121 RVA: 0x0036DBE4 File Offset: 0x0036BFE4
		public void Allocation(Vector3[] positions)
		{
			this._wayData = new Vector3[positions.Length];
			for (int i = 0; i < positions.Length; i++)
			{
				this._wayData[i] = positions[i];
			}
		}

		// Token: 0x06008162 RID: 33122 RVA: 0x0036DC30 File Offset: 0x0036C030
		public void Release()
		{
			this._wayData = null;
			GC.Collect();
		}

		// Token: 0x040067E2 RID: 26594
		[SerializeField]
		[DisableInPlayMode]
		private int _mapID;

		// Token: 0x040067E3 RID: 26595
		[SerializeField]
		[DisableInPlayMode]
		private int _areaID;

		// Token: 0x040067E4 RID: 26596
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private Vector3[] _wayData;
	}
}
