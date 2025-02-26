using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02001160 RID: 4448
	public class SelectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x060092E7 RID: 37607 RVA: 0x003CE7E9 File Offset: 0x003CCBE9
		public bool IsSelect
		{
			get
			{
				return this.isSelect;
			}
		}

		// Token: 0x060092E8 RID: 37608 RVA: 0x003CE7F1 File Offset: 0x003CCBF1
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isSelect = true;
		}

		// Token: 0x060092E9 RID: 37609 RVA: 0x003CE7FA File Offset: 0x003CCBFA
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isSelect = false;
		}

		// Token: 0x060092EA RID: 37610 RVA: 0x003CE803 File Offset: 0x003CCC03
		protected virtual void OnDisable()
		{
			this.isSelect = false;
		}

		// Token: 0x040076E4 RID: 30436
		[SerializeField]
		private bool isSelect;
	}
}
