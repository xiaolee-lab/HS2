using System;
using Manager;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000FA5 RID: 4005
	[Serializable]
	public abstract class DownCommandDataBase : CommandDataBase
	{
		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x06008578 RID: 34168 RVA: 0x003757F1 File Offset: 0x00373BF1
		// (set) Token: 0x06008579 RID: 34169 RVA: 0x003757F9 File Offset: 0x00373BF9
		public OnDownInputEvent TriggerEvent
		{
			get
			{
				return this._triggerEvent;
			}
			set
			{
				this._triggerEvent = value;
			}
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x00375802 File Offset: 0x00373C02
		protected override void OnInvoke(Manager.Input input)
		{
			this._triggerEvent.Invoke(this.IsInput(input));
		}

		// Token: 0x04006BE6 RID: 27622
		[SerializeField]
		private OnDownInputEvent _triggerEvent = new OnDownInputEvent();
	}
}
