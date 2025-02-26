using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008B2 RID: 2226
	[DisallowMultipleComponent]
	[RequireComponent(typeof(InputField))]
	public class InputFieldFocuse : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IEventSystemHandler
	{
		// Token: 0x06003A05 RID: 14853 RVA: 0x00154A1E File Offset: 0x00152E1E
		public void OnDeselect(BaseEventData eventData)
		{
			if (Singleton<CraftScene>.IsInstance())
			{
				Singleton<CraftScene>.Instance.DeselectInputField(this.inputField);
			}
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x00154A3A File Offset: 0x00152E3A
		public void OnSelect(BaseEventData eventData)
		{
			if (Singleton<CraftScene>.IsInstance())
			{
				Singleton<CraftScene>.Instance.SelectInputField(this.inputField);
			}
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x00154A56 File Offset: 0x00152E56
		public void OnSubmit(BaseEventData eventData)
		{
			if (Singleton<CraftScene>.IsInstance())
			{
				Singleton<CraftScene>.Instance.SelectInputField(this.inputField);
			}
		}

		// Token: 0x0400397C RID: 14716
		[SerializeField]
		private InputField inputField;
	}
}
