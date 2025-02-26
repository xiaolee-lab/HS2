using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BDC RID: 3036
	public class HousingActionPointItem : ActionPointComponentBase
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x00275AE6 File Offset: 0x00273EE6
		public GameObject[] ItemObjects
		{
			[CompilerGenerated]
			get
			{
				return this._itemObjects;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x00275AEE File Offset: 0x00273EEE
		public MapItemKeyValuePair[] ItemData
		{
			[CompilerGenerated]
			get
			{
				return this._itemData;
			}
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x00275AF6 File Offset: 0x00273EF6
		protected override void OnStart()
		{
			this._actionPoint.MapItemObjs = this._itemObjects;
			this._actionPoint.MapItemData = this._itemData;
		}

		// Token: 0x04005371 RID: 21361
		[SerializeField]
		private GameObject[] _itemObjects;

		// Token: 0x04005372 RID: 21362
		[SerializeField]
		private MapItemKeyValuePair[] _itemData;
	}
}
