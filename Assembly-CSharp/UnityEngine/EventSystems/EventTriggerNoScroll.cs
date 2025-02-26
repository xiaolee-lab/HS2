using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000A1D RID: 2589
	[AddComponentMenu("Event/Event Trigger NoScroll")]
	public class EventTriggerNoScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x06004D04 RID: 19716 RVA: 0x001D95CB File Offset: 0x001D79CB
		protected EventTriggerNoScroll()
		{
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x001D95D3 File Offset: 0x001D79D3
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x001D95F1 File Offset: 0x001D79F1
		public List<EventTriggerNoScroll.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTriggerNoScroll.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x001D95FC File Offset: 0x001D79FC
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			int i = 0;
			int count = this.triggers.Count;
			while (i < count)
			{
				EventTriggerNoScroll.Entry entry = this.triggers[i];
				if (entry.eventID == id)
				{
					EventTriggerNoScroll.TriggerEvent callback = entry.callback;
					if (callback != null)
					{
						callback.Invoke(eventData);
					}
				}
				i++;
			}
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x001D9655 File Offset: 0x001D7A55
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x001D965F File Offset: 0x001D7A5F
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x001D9669 File Offset: 0x001D7A69
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x001D9673 File Offset: 0x001D7A73
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x001D967D File Offset: 0x001D7A7D
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x001D9687 File Offset: 0x001D7A87
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x001D9691 File Offset: 0x001D7A91
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x001D969B File Offset: 0x001D7A9B
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x001D96A6 File Offset: 0x001D7AA6
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x001D96B1 File Offset: 0x001D7AB1
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x001D96BC File Offset: 0x001D7ABC
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x001D96C6 File Offset: 0x001D7AC6
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x001D96D1 File Offset: 0x001D7AD1
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x001D96DC File Offset: 0x001D7ADC
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x001D96E7 File Offset: 0x001D7AE7
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x001D96F2 File Offset: 0x001D7AF2
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x04004685 RID: 18053
		[SerializeField]
		private List<EventTriggerNoScroll.Entry> m_Delegates;

		// Token: 0x02000A1E RID: 2590
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x02000A1F RID: 2591
		[Serializable]
		public class Entry
		{
			// Token: 0x04004686 RID: 18054
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04004687 RID: 18055
			public EventTriggerNoScroll.TriggerEvent callback = new EventTriggerNoScroll.TriggerEvent();
		}
	}
}
