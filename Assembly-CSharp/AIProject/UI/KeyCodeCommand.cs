using System;
using Manager;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000FA7 RID: 4007
	[Serializable]
	public class KeyCodeCommand : DownCommandDataBase
	{
		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06008580 RID: 34176 RVA: 0x00375863 File Offset: 0x00373C63
		// (set) Token: 0x06008581 RID: 34177 RVA: 0x0037586B File Offset: 0x00373C6B
		public KeyCode KeyCode
		{
			get
			{
				return this._keyCode;
			}
			set
			{
				this._keyCode = value;
			}
		}

		// Token: 0x06008582 RID: 34178 RVA: 0x00375874 File Offset: 0x00373C74
		protected override bool IsInput(Manager.Input input)
		{
			return input.IsDown(this._keyCode);
		}

		// Token: 0x04006BE8 RID: 27624
		[SerializeField]
		private KeyCode _keyCode = KeyCode.A;
	}
}
