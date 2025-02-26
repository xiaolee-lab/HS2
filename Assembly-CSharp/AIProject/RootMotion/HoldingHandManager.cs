using System;
using UnityEngine;

namespace AIProject.RootMotion
{
	// Token: 0x020011C0 RID: 4544
	public class HoldingHandManager : MonoBehaviour
	{
		// Token: 0x06009508 RID: 38152 RVA: 0x003D7044 File Offset: 0x003D5444
		private void OnEnable()
		{
			this._leftHandHolder.enabled = true;
			this._leftHandHolder.EnabledHolding = true;
		}

		// Token: 0x06009509 RID: 38153 RVA: 0x003D705E File Offset: 0x003D545E
		private void OnDisable()
		{
			this._leftHandHolder.enabled = false;
			this._leftHandHolder.EnabledHolding = false;
		}

		// Token: 0x040077BD RID: 30653
		[SerializeField]
		private HandsHolder _leftHandHolder;
	}
}
