using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD3 RID: 3027
	public abstract class ActionPointComponentBase : MonoBehaviour
	{
		// Token: 0x06005CC5 RID: 23749 RVA: 0x002748BE File Offset: 0x00272CBE
		protected void Start()
		{
			this._actionPoint = base.GetComponent<ActionPoint>();
			this.OnStart();
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x002748D2 File Offset: 0x00272CD2
		protected virtual void OnStart()
		{
		}

		// Token: 0x0400535A RID: 21338
		protected ActionPoint _actionPoint;
	}
}
