using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD5 RID: 3029
	public class ActionPointItem : ActionPointComponentBase
	{
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x002749DE File Offset: 0x00272DDE
		public GameObject[] ItemObjects
		{
			[CompilerGenerated]
			get
			{
				return this._itemObjects;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x002749E6 File Offset: 0x00272DE6
		public MapItemKeyValuePair[] ItemData
		{
			[CompilerGenerated]
			get
			{
				return this._itemData;
			}
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x002749F0 File Offset: 0x00272DF0
		protected override void OnStart()
		{
			this._itemObjects = ActionPointItemData.Table[this._id];
			this._actionPoint.MapItemObjs = this._itemObjects;
			this._itemData = ActionPointItemData.Dic[this._id];
			this._actionPoint.MapItemData = this._itemData;
		}

		// Token: 0x0400535C RID: 21340
		[SerializeField]
		protected int _id;

		// Token: 0x0400535D RID: 21341
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private GameObject[] _itemObjects;

		// Token: 0x0400535E RID: 21342
		[SerializeField]
		private MapItemKeyValuePair[] _itemData;
	}
}
