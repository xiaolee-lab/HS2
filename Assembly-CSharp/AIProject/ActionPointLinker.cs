using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD6 RID: 3030
	[RequireComponent(typeof(ActionPoint))]
	public class ActionPointLinker : ActionPointComponentBase
	{
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06005CCF RID: 23759 RVA: 0x00274A53 File Offset: 0x00272E53
		public ActionPoint ConnectedPoint
		{
			[CompilerGenerated]
			get
			{
				return this._connectedPoint;
			}
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00274A5B File Offset: 0x00272E5B
		protected override void OnStart()
		{
			if (this._connectedPoint != null)
			{
				this._actionPoint.ConnectedActionPoints.Add(this._connectedPoint);
			}
		}

		// Token: 0x0400535F RID: 21343
		[SerializeField]
		private ActionPoint _connectedPoint;
	}
}
