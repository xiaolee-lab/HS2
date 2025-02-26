using System;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200008E RID: 142
	public class ListBoxItem : ItemContainer
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000E406 File Offset: 0x0000C806
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000E40E File Offset: 0x0000C80E
		public override bool IsSelected
		{
			get
			{
				return base.IsSelected;
			}
			set
			{
				if (base.IsSelected != value)
				{
					this.m_toggle.isOn = value;
					base.IsSelected = value;
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000E42F File Offset: 0x0000C82F
		protected override void AwakeOverride()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			this.m_toggle.interactable = false;
			this.m_toggle.isOn = this.IsSelected;
		}

		// Token: 0x04000282 RID: 642
		private Toggle m_toggle;
	}
}
