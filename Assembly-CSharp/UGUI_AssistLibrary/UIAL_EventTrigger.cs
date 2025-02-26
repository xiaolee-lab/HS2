using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UGUI_AssistLibrary
{
	// Token: 0x0200084C RID: 2124
	public class UIAL_EventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x00140156 File Offset: 0x0013E556
		protected UIAL_EventTrigger()
		{
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x0014015E File Offset: 0x0013E55E
		// (set) Token: 0x06003640 RID: 13888 RVA: 0x0014017C File Offset: 0x0013E57C
		public List<UIAL_EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<UIAL_EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x00140188 File Offset: 0x0013E588
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			int i = 0;
			int count = this.triggers.Count;
			while (i < count)
			{
				UIAL_EventTrigger.Entry entry = this.triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					if (id != EventTriggerType.PointerClick || this.isClick(entry.buttonType))
					{
						entry.callback.Invoke(eventData);
					}
				}
				i++;
			}
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x00140200 File Offset: 0x0013E600
		private bool isClick(UIAL_EventTrigger.ButtonType type)
		{
			return ((type & UIAL_EventTrigger.ButtonType.Left) != (UIAL_EventTrigger.ButtonType)0 && Input.GetMouseButtonUp(0)) || ((type & UIAL_EventTrigger.ButtonType.Right) != (UIAL_EventTrigger.ButtonType)0 && Input.GetMouseButtonUp(1)) || ((type & UIAL_EventTrigger.ButtonType.Center) != (UIAL_EventTrigger.ButtonType)0 && Input.GetMouseButtonUp(2));
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0014024D File Offset: 0x0013E64D
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x00140257 File Offset: 0x0013E657
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x00140261 File Offset: 0x0013E661
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x0400368F RID: 13967
		private List<UIAL_EventTrigger.Entry> m_Delegates;

		// Token: 0x0200084D RID: 2125
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x0200084E RID: 2126
		[Flags]
		public enum ButtonType
		{
			// Token: 0x04003691 RID: 13969
			Left = 1,
			// Token: 0x04003692 RID: 13970
			Right = 2,
			// Token: 0x04003693 RID: 13971
			Center = 4
		}

		// Token: 0x0200084F RID: 2127
		public class Entry
		{
			// Token: 0x04003694 RID: 13972
			public UIAL_EventTrigger.ButtonType buttonType = UIAL_EventTrigger.ButtonType.Left | UIAL_EventTrigger.ButtonType.Right | UIAL_EventTrigger.ButtonType.Center;

			// Token: 0x04003695 RID: 13973
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04003696 RID: 13974
			public UIAL_EventTrigger.TriggerEvent callback = new UIAL_EventTrigger.TriggerEvent();
		}
	}
}
