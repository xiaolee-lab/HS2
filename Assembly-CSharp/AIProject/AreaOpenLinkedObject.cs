using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BED RID: 3053
	public class AreaOpenLinkedObject : MonoBehaviour
	{
		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06005D46 RID: 23878 RVA: 0x00276CA3 File Offset: 0x002750A3
		public int AreaOpenID
		{
			[CompilerGenerated]
			get
			{
				return this._areaOpenID;
			}
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x00276CAC File Offset: 0x002750AC
		private void Awake()
		{
			if (AreaOpenLinkedObject.Table == null)
			{
				AreaOpenLinkedObject.Table = new ReadOnlyDictionary<int, List<GameObject>>(AreaOpenLinkedObject._table);
			}
			List<GameObject> list;
			if (!AreaOpenLinkedObject._table.TryGetValue(this._areaOpenID, out list))
			{
				list = (AreaOpenLinkedObject._table[this._areaOpenID] = new List<GameObject>());
			}
			if (!list.Contains(base.gameObject))
			{
				list.Add(base.gameObject);
			}
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x00276D20 File Offset: 0x00275120
		private void OnDestroy()
		{
			List<GameObject> list;
			if (AreaOpenLinkedObject._table.TryGetValue(this._areaOpenID, out list) && !list.IsNullOrEmpty<GameObject>() && list.Contains(base.gameObject))
			{
				list.Remove(base.gameObject);
				if (list.Count == 0)
				{
					AreaOpenLinkedObject._table.Remove(this._areaOpenID);
				}
			}
		}

		// Token: 0x0400539A RID: 21402
		[SerializeField]
		private int _areaOpenID = -1;

		// Token: 0x0400539B RID: 21403
		private static Dictionary<int, List<GameObject>> _table = new Dictionary<int, List<GameObject>>();

		// Token: 0x0400539C RID: 21404
		public static ReadOnlyDictionary<int, List<GameObject>> Table = null;
	}
}
