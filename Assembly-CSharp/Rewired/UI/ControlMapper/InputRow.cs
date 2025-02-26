using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200054F RID: 1359
	[AddComponentMenu("")]
	public class InputRow : MonoBehaviour
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x000A9E2A File Offset: 0x000A822A
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x000A9E32 File Offset: 0x000A8232
		public ButtonInfo[] buttons { get; private set; }

		// Token: 0x06001C64 RID: 7268 RVA: 0x000A9E3B File Offset: 0x000A823B
		public void Initialize(int rowIndex, string label, Action<int, ButtonInfo> inputFieldActivatedCallback)
		{
			this.rowIndex = rowIndex;
			this.label.text = label;
			this.inputFieldActivatedCallback = inputFieldActivatedCallback;
			this.buttons = base.transform.GetComponentsInChildren<ButtonInfo>(true);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000A9E69 File Offset: 0x000A8269
		public void OnButtonActivated(ButtonInfo buttonInfo)
		{
			if (this.inputFieldActivatedCallback == null)
			{
				return;
			}
			this.inputFieldActivatedCallback(this.rowIndex, buttonInfo);
		}

		// Token: 0x04001D9B RID: 7579
		public Text label;

		// Token: 0x04001D9D RID: 7581
		private int rowIndex;

		// Token: 0x04001D9E RID: 7582
		private Action<int, ButtonInfo> inputFieldActivatedCallback;
	}
}
