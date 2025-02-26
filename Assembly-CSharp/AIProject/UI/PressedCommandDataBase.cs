using System;
using Manager;
using UnityEngine;
using UnityEngine.Events;

namespace AIProject.UI
{
	// Token: 0x02000FA6 RID: 4006
	[Serializable]
	public abstract class PressedCommandDataBase : CommandDataBase
	{
		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x0600857C RID: 34172 RVA: 0x00375829 File Offset: 0x00373C29
		// (set) Token: 0x0600857D RID: 34173 RVA: 0x00375831 File Offset: 0x00373C31
		public UnityEvent TriggerEvent
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

		// Token: 0x0600857E RID: 34174 RVA: 0x0037583A File Offset: 0x00373C3A
		protected override void OnInvoke(Manager.Input input)
		{
			if (this.IsInput(input))
			{
				this._triggerEvent.Invoke();
			}
		}

		// Token: 0x04006BE7 RID: 27623
		[SerializeField]
		private UnityEvent _triggerEvent = new UnityEvent();
	}
}
