using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BE5 RID: 3045
	[Serializable]
	public class MapItemKeyValuePair
	{
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06005D17 RID: 23831 RVA: 0x00276364 File Offset: 0x00274764
		// (set) Token: 0x06005D18 RID: 23832 RVA: 0x0027636C File Offset: 0x0027476C
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x00276375 File Offset: 0x00274775
		// (set) Token: 0x06005D1A RID: 23834 RVA: 0x0027637D File Offset: 0x0027477D
		public GameObject ItemObj
		{
			get
			{
				return this._itemObj;
			}
			set
			{
				this._itemObj = value;
			}
		}

		// Token: 0x04005387 RID: 21383
		[SerializeField]
		private int _id;

		// Token: 0x04005388 RID: 21384
		[SerializeField]
		private GameObject _itemObj;
	}
}
