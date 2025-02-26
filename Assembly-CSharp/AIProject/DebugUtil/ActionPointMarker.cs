using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject.DebugUtil
{
	// Token: 0x02000E19 RID: 3609
	[AddComponentMenu("YK/Debug/ActionPointMarker")]
	public class ActionPointMarker : MonoBehaviour
	{
		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x002FEAE1 File Offset: 0x002FCEE1
		public static Dictionary<int, ActionPoint> ActionPointTable { get; } = new Dictionary<int, ActionPoint>();

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06006FF1 RID: 28657 RVA: 0x002FEAE8 File Offset: 0x002FCEE8
		public static List<ActionPoint> ActionPointList { get; } = new List<ActionPoint>();

		// Token: 0x06006FF2 RID: 28658 RVA: 0x002FEAEF File Offset: 0x002FCEEF
		private void Awake()
		{
			this._actionPoint = base.GetComponent<ActionPoint>();
		}

		// Token: 0x06006FF3 RID: 28659 RVA: 0x002FEAFD File Offset: 0x002FCEFD
		private void OnEnable()
		{
			if (this._actionPoint != null)
			{
				ActionPointMarker.ActionPointTable[this._actionPoint.GetInstanceID()] = this._actionPoint;
				ActionPointMarker.ActionPointList.Add(this._actionPoint);
			}
		}

		// Token: 0x06006FF4 RID: 28660 RVA: 0x002FEB3B File Offset: 0x002FCF3B
		private void OnDisable()
		{
			if (this._actionPoint != null)
			{
				ActionPointMarker.ActionPointTable.Remove(this._actionPoint.GetInstanceID());
				ActionPointMarker.ActionPointList.RemoveAll((ActionPoint x) => x == this._actionPoint);
			}
		}

		// Token: 0x04005C2C RID: 23596
		private ActionPoint _actionPoint;
	}
}
