using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000565 RID: 1381
	[AddComponentMenu("")]
	public class UIGroup : MonoBehaviour
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000AB43C File Offset: 0x000A983C
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x000AB464 File Offset: 0x000A9864
		public string labelText
		{
			get
			{
				return (!(this._label != null)) ? string.Empty : this._label.text;
			}
			set
			{
				if (this._label == null)
				{
					return;
				}
				this._label.text = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x000AB484 File Offset: 0x000A9884
		public Transform content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000AB48C File Offset: 0x000A988C
		public void SetLabelActive(bool state)
		{
			if (this._label == null)
			{
				return;
			}
			this._label.gameObject.SetActive(state);
		}

		// Token: 0x04001E2D RID: 7725
		[SerializeField]
		private Text _label;

		// Token: 0x04001E2E RID: 7726
		[SerializeField]
		private Transform _content;
	}
}
