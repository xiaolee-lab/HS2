using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BF2 RID: 3058
	public class ChestAnimData : ActionPointAnimData
	{
		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x00278006 File Offset: 0x00276406
		public static ReadOnlyDictionary<int, Animator> Table { get; } = new ReadOnlyDictionary<int, Animator>(ChestAnimData._table);

		// Token: 0x06005D83 RID: 23939 RVA: 0x0027800D File Offset: 0x0027640D
		protected override void Awake()
		{
			ChestAnimData._table[this._id] = this._animator;
		}

		// Token: 0x040053B6 RID: 21430
		private static Dictionary<int, Animator> _table = new Dictionary<int, Animator>();
	}
}
