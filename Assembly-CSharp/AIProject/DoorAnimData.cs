using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BFA RID: 3066
	public class DoorAnimData : ActionPointAnimData
	{
		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x0027B6B9 File Offset: 0x00279AB9
		public static ReadOnlyDictionary<int, Animator> Table { get; } = new ReadOnlyDictionary<int, Animator>(DoorAnimData._table);

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06005E0A RID: 24074 RVA: 0x0027B6C0 File Offset: 0x00279AC0
		public static ReadOnlyDictionary<int, DoorMatType> MatTable { get; } = new ReadOnlyDictionary<int, DoorMatType>(DoorAnimData._matTable);

		// Token: 0x06005E0B RID: 24075 RVA: 0x0027B6C7 File Offset: 0x00279AC7
		protected override void Awake()
		{
			DoorAnimData._table[this._id] = this._animator;
			DoorAnimData._matTable[this._id] = this._matType;
		}

		// Token: 0x04005406 RID: 21510
		private static Dictionary<int, Animator> _table = new Dictionary<int, Animator>();

		// Token: 0x04005407 RID: 21511
		[SerializeField]
		private DoorMatType _matType;

		// Token: 0x04005409 RID: 21513
		private static Dictionary<int, DoorMatType> _matTable = new Dictionary<int, DoorMatType>();
	}
}
