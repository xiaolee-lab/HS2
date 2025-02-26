using System;
using Manager;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000FAA RID: 4010
	[Serializable]
	public class ActionIDDownCommand : PressedCommandDataBase
	{
		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x0600858C RID: 34188 RVA: 0x003758F0 File Offset: 0x00373CF0
		// (set) Token: 0x0600858D RID: 34189 RVA: 0x003758F8 File Offset: 0x00373CF8
		public ActionID ActionID
		{
			get
			{
				return this._actionID;
			}
			set
			{
				this._actionID = value;
			}
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x00375901 File Offset: 0x00373D01
		protected override bool IsInput(Manager.Input input)
		{
			return input.IsPressedKey(this._actionID);
		}

		// Token: 0x04006BEB RID: 27627
		[SerializeField]
		private ActionID _actionID = ActionID.Submit;
	}
}
