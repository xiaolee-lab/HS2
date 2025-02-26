using System;
using Manager;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000FA9 RID: 4009
	[Serializable]
	public class ActionIDCommand : DownCommandDataBase
	{
		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06008588 RID: 34184 RVA: 0x003758C1 File Offset: 0x00373CC1
		// (set) Token: 0x06008589 RID: 34185 RVA: 0x003758C9 File Offset: 0x00373CC9
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

		// Token: 0x0600858A RID: 34186 RVA: 0x003758D2 File Offset: 0x00373CD2
		protected override bool IsInput(Manager.Input input)
		{
			return input.IsDown(this._actionID);
		}

		// Token: 0x04006BEA RID: 27626
		[SerializeField]
		private ActionID _actionID = ActionID.Submit;
	}
}
