using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C07 RID: 3079
	public class EventTransform : MonoBehaviour
	{
		// Token: 0x06005EE7 RID: 24295 RVA: 0x00282780 File Offset: 0x00280B80
		private void Start()
		{
			if (Singleton<Map>.IsInstance())
			{
				Dictionary<int, Transform> dictionary;
				if (!Singleton<Map>.Instance.EventStartPointDic.TryGetValue(this._mapID, out dictionary))
				{
					Dictionary<int, Transform> dictionary2 = new Dictionary<int, Transform>();
					Singleton<Map>.Instance.EventStartPointDic[this._mapID] = dictionary2;
					dictionary = dictionary2;
				}
				dictionary[this._id] = base.transform;
			}
		}

		// Token: 0x04005476 RID: 21622
		public int _mapID;

		// Token: 0x04005477 RID: 21623
		public int _id;
	}
}
