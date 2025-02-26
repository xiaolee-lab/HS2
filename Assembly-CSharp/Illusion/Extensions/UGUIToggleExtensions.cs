using System;
using UnityEngine.UI;

namespace Illusion.Extensions
{
	// Token: 0x0200087F RID: 2175
	public static class UGUIToggleExtensions
	{
		// Token: 0x060037AC RID: 14252 RVA: 0x0014953C File Offset: 0x0014793C
		public static void SetIsOnWithoutCallback(this Toggle self, bool isOn)
		{
			Toggle.ToggleEvent onValueChanged = self.onValueChanged;
			self.onValueChanged = new Toggle.ToggleEvent();
			self.isOn = isOn;
			self.onValueChanged = onValueChanged;
		}
	}
}
