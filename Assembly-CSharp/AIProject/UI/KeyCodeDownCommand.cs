using System;
using Manager;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000FA8 RID: 4008
	[Serializable]
	public class KeyCodeDownCommand : PressedCommandDataBase
	{
		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06008584 RID: 34180 RVA: 0x00375892 File Offset: 0x00373C92
		// (set) Token: 0x06008585 RID: 34181 RVA: 0x0037589A File Offset: 0x00373C9A
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

		// Token: 0x06008586 RID: 34182 RVA: 0x003758A3 File Offset: 0x00373CA3
		protected override bool IsInput(Manager.Input input)
		{
			return input.IsPressedKey(this._keyCode);
		}

		// Token: 0x04006BE9 RID: 27625
		[SerializeField]
		private KeyCode _keyCode = KeyCode.A;
	}
}
