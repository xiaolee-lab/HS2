using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD1 RID: 3025
	public class ActionPointAnimData : MonoBehaviour
	{
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06005CBD RID: 23741 RVA: 0x00274897 File Offset: 0x00272C97
		public static ReadOnlyDictionary<int, Animator> AnimationItemTable { get; } = new ReadOnlyDictionary<int, Animator>(ActionPointAnimData._animationItemTable);

		// Token: 0x06005CBE RID: 23742 RVA: 0x0027489E File Offset: 0x00272C9E
		protected virtual void Awake()
		{
			ActionPointAnimData._animationItemTable[this._id] = this._animator;
		}

		// Token: 0x04005355 RID: 21333
		private static Dictionary<int, Animator> _animationItemTable = new Dictionary<int, Animator>();

		// Token: 0x04005356 RID: 21334
		[SerializeField]
		protected int _id;

		// Token: 0x04005357 RID: 21335
		[SerializeField]
		protected Animator _animator;
	}
}
