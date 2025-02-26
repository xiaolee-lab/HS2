using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C31 RID: 3121
	public class TimeOpenLinkedObject : MonoBehaviour
	{
		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x00289C84 File Offset: 0x00288084
		public int TimeOpenID
		{
			[CompilerGenerated]
			get
			{
				return this._timeOpenID;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x060060AC RID: 24748 RVA: 0x00289C8C File Offset: 0x0028808C
		public bool EnableFlag
		{
			[CompilerGenerated]
			get
			{
				return this._enableFlag;
			}
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x00289C94 File Offset: 0x00288094
		private void Awake()
		{
			if (TimeOpenLinkedObject.Table == null)
			{
				TimeOpenLinkedObject.Table = new ReadOnlyDictionary<int, List<TimeOpenLinkedObject>>(TimeOpenLinkedObject._table);
			}
			List<TimeOpenLinkedObject> list;
			if (!TimeOpenLinkedObject._table.TryGetValue(this._timeOpenID, out list) || list == null)
			{
				list = (TimeOpenLinkedObject._table[this._timeOpenID] = new List<TimeOpenLinkedObject>());
			}
			if (!list.Contains(this))
			{
				list.Add(this);
			}
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x00289D04 File Offset: 0x00288104
		private void OnDestroy()
		{
			List<TimeOpenLinkedObject> list;
			if (TimeOpenLinkedObject._table.TryGetValue(this._timeOpenID, out list) && !list.IsNullOrEmpty<TimeOpenLinkedObject>() && list.Contains(this))
			{
				list.Remove(this);
			}
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x00289D47 File Offset: 0x00288147
		public void SetActive(bool active)
		{
			if (base.gameObject.activeSelf != active)
			{
				base.gameObject.SetActive(active);
			}
		}

		// Token: 0x040055BF RID: 21951
		[SerializeField]
		[DisableInPlayMode]
		private int _timeOpenID = -1;

		// Token: 0x040055C0 RID: 21952
		[SerializeField]
		[DisableInPlayMode]
		private bool _enableFlag = true;

		// Token: 0x040055C1 RID: 21953
		private static Dictionary<int, List<TimeOpenLinkedObject>> _table = new Dictionary<int, List<TimeOpenLinkedObject>>();

		// Token: 0x040055C2 RID: 21954
		public static ReadOnlyDictionary<int, List<TimeOpenLinkedObject>> Table = null;
	}
}
