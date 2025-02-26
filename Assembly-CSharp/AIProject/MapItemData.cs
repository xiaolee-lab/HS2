using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C20 RID: 3104
	public class MapItemData : MonoBehaviour
	{
		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x00285D43 File Offset: 0x00284143
		public static ReadOnlyDictionary<int, List<GameObject>> Table { get; } = new ReadOnlyDictionary<int, List<GameObject>>(MapItemData._table);

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06005FC7 RID: 24519 RVA: 0x00285D4A File Offset: 0x0028414A
		public GameObject[] Objects
		{
			[CompilerGenerated]
			get
			{
				return this._objects;
			}
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x00285D54 File Offset: 0x00284154
		protected virtual void Awake()
		{
			if (!this._objects.IsNullOrEmpty<GameObject>())
			{
				List<GameObject> list;
				if (!MapItemData._table.TryGetValue(this._id, out list) || list == null)
				{
					list = (MapItemData._table[this._id] = new List<GameObject>());
				}
				foreach (GameObject gameObject in this._objects)
				{
					if (!(gameObject == null))
					{
						if (!list.Contains(gameObject))
						{
							list.Add(gameObject);
						}
					}
				}
			}
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x00285DE8 File Offset: 0x002841E8
		private void OnDestroy()
		{
			if (MapItemData.Table.IsNullOrEmpty<int, List<GameObject>>() || this._objects.IsNullOrEmpty<GameObject>())
			{
				return;
			}
			List<GameObject> list;
			if (!MapItemData.Table.TryGetValue(this._id, out list) || list.IsNullOrEmpty<GameObject>())
			{
				return;
			}
			foreach (GameObject gameObject in this._objects)
			{
				if (gameObject != null && list.Contains(gameObject))
				{
					list.Remove(gameObject);
				}
			}
			list.RemoveAll((GameObject x) => x == null);
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x00285E9C File Offset: 0x0028429C
		public static List<GameObject> Get(int id)
		{
			if (MapItemData.Table.IsNullOrEmpty<int, List<GameObject>>())
			{
				return null;
			}
			List<GameObject> list;
			return (!MapItemData.Table.TryGetValue(id, out list)) ? null : list;
		}

		// Token: 0x0400554B RID: 21835
		private static Dictionary<int, List<GameObject>> _table = new Dictionary<int, List<GameObject>>();

		// Token: 0x0400554C RID: 21836
		[SerializeField]
		private int _id;

		// Token: 0x0400554D RID: 21837
		[SerializeField]
		private GameObject[] _objects;
	}
}
