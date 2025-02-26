using System;

namespace Housing
{
	// Token: 0x020008BD RID: 2237
	public abstract class UIDerived
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x00152345 File Offset: 0x00150745
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x0015234D File Offset: 0x0015074D
		private protected UICtrl UICtrl { protected get; private set; }

		// Token: 0x06003A6B RID: 14955 RVA: 0x00152356 File Offset: 0x00150756
		public virtual void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			this.UICtrl = _uiCtrl;
		}

		// Token: 0x06003A6C RID: 14956
		public abstract void UpdateUI();
	}
}
