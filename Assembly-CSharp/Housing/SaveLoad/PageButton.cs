using System;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace Housing.SaveLoad
{
	// Token: 0x020008AC RID: 2220
	public class PageButton : LoopListViewItem2
	{
		// Token: 0x060039D4 RID: 14804 RVA: 0x00153CAC File Offset: 0x001520AC
		public void SetData(int _index, bool _select, Action<int> _onClick)
		{
			this.index = _index;
			this.onClick = _onClick;
			this.text.text = string.Format("{0}", _index);
			this.toggle.isOn = _select;
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x00153CE3 File Offset: 0x001520E3
		public void Select()
		{
			this.toggle.isOn = true;
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x00153CF1 File Offset: 0x001520F1
		public void Deselect()
		{
			this.toggle.isOn = false;
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x00153CFF File Offset: 0x001520FF
		public void OnClick()
		{
			this.toggle.isOn = true;
			if (this.onClick != null)
			{
				this.onClick(this.index);
			}
		}

		// Token: 0x04003951 RID: 14673
		[SerializeField]
		private Text text;

		// Token: 0x04003952 RID: 14674
		[SerializeField]
		private Toggle toggle;

		// Token: 0x04003953 RID: 14675
		private Action<int> onClick;

		// Token: 0x04003954 RID: 14676
		private int index;
	}
}
