using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012D9 RID: 4825
	public class InputFieldToCamera : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IEventSystemHandler
	{
		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x0600A0F4 RID: 41204 RVA: 0x00421D5C File Offset: 0x0042015C
		private Canvas canvas
		{
			[CompilerGenerated]
			get
			{
				Canvas result;
				if ((result = this.m_Canvas) == null)
				{
					result = (this.m_Canvas = base.GetComponentInParent<Canvas>());
				}
				return result;
			}
		}

		// Token: 0x0600A0F5 RID: 41205 RVA: 0x00421D85 File Offset: 0x00420185
		public void OnDeselect(BaseEventData eventData)
		{
			Singleton<Studio>.Instance.DeselectInputField(this.inputField, this.inputFieldTMP);
		}

		// Token: 0x0600A0F6 RID: 41206 RVA: 0x00421D9D File Offset: 0x0042019D
		public void OnSelect(BaseEventData eventData)
		{
			Singleton<Studio>.Instance.SelectInputField(this.inputField, this.inputFieldTMP);
			SortCanvas.select = this.canvas;
		}

		// Token: 0x0600A0F7 RID: 41207 RVA: 0x00421DC0 File Offset: 0x004201C0
		public void OnSubmit(BaseEventData eventData)
		{
			Singleton<Studio>.Instance.SelectInputField(this.inputField, this.inputFieldTMP);
		}

		// Token: 0x04007F23 RID: 32547
		[SerializeField]
		private InputField inputField;

		// Token: 0x04007F24 RID: 32548
		[SerializeField]
		private TMP_InputField inputFieldTMP;

		// Token: 0x04007F25 RID: 32549
		[SerializeField]
		private Canvas m_Canvas;
	}
}
