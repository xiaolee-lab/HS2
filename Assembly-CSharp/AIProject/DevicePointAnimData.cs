using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BF7 RID: 3063
	public class DevicePointAnimData : MonoBehaviour
	{
		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x0027B1FC File Offset: 0x002795FC
		public static ReadOnlyDictionary<int, Animator> AnimatorItemTable { get; } = new ReadOnlyDictionary<int, Animator>(DevicePointAnimData._animatorItemTable);

		// Token: 0x06005DF8 RID: 24056 RVA: 0x0027B203 File Offset: 0x00279603
		protected virtual void Awake()
		{
			DevicePointAnimData._animatorItemTable[this._id] = this._animator;
		}

		// Token: 0x040053F7 RID: 21495
		private static Dictionary<int, Animator> _animatorItemTable = new Dictionary<int, Animator>();

		// Token: 0x040053F8 RID: 21496
		[SerializeField]
		private int _id;

		// Token: 0x040053F9 RID: 21497
		[SerializeField]
		private Animator _animator;
	}
}
