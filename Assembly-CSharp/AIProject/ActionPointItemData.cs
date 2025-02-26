using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BE4 RID: 3044
	public class ActionPointItemData : MonoBehaviour
	{
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06005D11 RID: 23825 RVA: 0x00276310 File Offset: 0x00274710
		public static ReadOnlyDictionary<int, GameObject[]> Table { get; } = new ReadOnlyDictionary<int, GameObject[]>(ActionPointItemData._table);

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x00276317 File Offset: 0x00274717
		public static ReadOnlyDictionary<int, MapItemKeyValuePair[]> Dic { get; } = new ReadOnlyDictionary<int, MapItemKeyValuePair[]>(ActionPointItemData._dic);

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x0027631E File Offset: 0x0027471E
		public GameObject[] Objects
		{
			[CompilerGenerated]
			get
			{
				return this._objects;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x00276326 File Offset: 0x00274726
		public MapItemKeyValuePair[] ObjectData
		{
			[CompilerGenerated]
			get
			{
				return this._objectData;
			}
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x0027632E File Offset: 0x0027472E
		protected virtual void Awake()
		{
			ActionPointItemData._table[this._id] = this._objects;
			ActionPointItemData._dic[this._id] = this._objectData;
		}

		// Token: 0x04005381 RID: 21377
		private static Dictionary<int, GameObject[]> _table = new Dictionary<int, GameObject[]>();

		// Token: 0x04005383 RID: 21379
		private static Dictionary<int, MapItemKeyValuePair[]> _dic = new Dictionary<int, MapItemKeyValuePair[]>();

		// Token: 0x04005384 RID: 21380
		[SerializeField]
		private int _id;

		// Token: 0x04005385 RID: 21381
		[SerializeField]
		private GameObject[] _objects;

		// Token: 0x04005386 RID: 21382
		[SerializeField]
		private MapItemKeyValuePair[] _objectData;
	}
}
