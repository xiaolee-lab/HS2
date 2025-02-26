using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BDA RID: 3034
	public class EventPivot : ActionPointComponentBase
	{
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x00275A99 File Offset: 0x00273E99
		public Transform PivotTransform
		{
			[CompilerGenerated]
			get
			{
				return this._pivotTransform;
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x00275AA1 File Offset: 0x00273EA1
		protected override void OnStart()
		{
		}

		// Token: 0x0400536F RID: 21359
		[SerializeField]
		private Transform _pivotTransform;
	}
}
