using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000569 RID: 1385
	[AddComponentMenu("")]
	public class UISliderControl : UIControl
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x000AB7C8 File Offset: 0x000A9BC8
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x000AB7D0 File Offset: 0x000A9BD0
		public bool showIcon
		{
			get
			{
				return this._showIcon;
			}
			set
			{
				if (this.iconImage == null)
				{
					return;
				}
				this.iconImage.gameObject.SetActive(value);
				this._showIcon = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x000AB7FC File Offset: 0x000A9BFC
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x000AB804 File Offset: 0x000A9C04
		public bool showSlider
		{
			get
			{
				return this._showSlider;
			}
			set
			{
				if (this.slider == null)
				{
					return;
				}
				this.slider.gameObject.SetActive(value);
				this._showSlider = value;
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000AB830 File Offset: 0x000A9C30
		public override void SetCancelCallback(Action cancelCallback)
		{
			base.SetCancelCallback(cancelCallback);
			if (cancelCallback == null || this.slider == null)
			{
				return;
			}
			if (this.slider is ICustomSelectable)
			{
				(this.slider as ICustomSelectable).CancelEvent += delegate()
				{
					cancelCallback();
				};
			}
			else
			{
				EventTrigger eventTrigger = this.slider.GetComponent<EventTrigger>();
				if (eventTrigger == null)
				{
					eventTrigger = this.slider.gameObject.AddComponent<EventTrigger>();
				}
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.callback = new EventTrigger.TriggerEvent();
				entry.eventID = EventTriggerType.Cancel;
				entry.callback.AddListener(delegate(BaseEventData data)
				{
					cancelCallback();
				});
				if (eventTrigger.triggers == null)
				{
					eventTrigger.triggers = new List<EventTrigger.Entry>();
				}
				eventTrigger.triggers.Add(entry);
			}
		}

		// Token: 0x04001E33 RID: 7731
		public Image iconImage;

		// Token: 0x04001E34 RID: 7732
		public Slider slider;

		// Token: 0x04001E35 RID: 7733
		private bool _showIcon;

		// Token: 0x04001E36 RID: 7734
		private bool _showSlider;
	}
}
