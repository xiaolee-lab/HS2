using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BE1 RID: 3041
	public class HPointLinker : MonoBehaviour
	{
		// Token: 0x06005D04 RID: 23812 RVA: 0x00275E48 File Offset: 0x00274248
		private void Start()
		{
			ActionPoint component = base.GetComponent<ActionPoint>();
			component.HPoint = this._hPoint;
		}

		// Token: 0x04005379 RID: 21369
		[SerializeField]
		private HPoint _hPoint;
	}
}
