using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E6 RID: 230
	[TaskDescription("Returns success as soon as the event specified by eventName has been received.")]
	[TaskIcon("{SkinColor}HasReceivedEventIcon.png")]
	public class HasReceivedEvent : Conditional
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0001EBF0 File Offset: 0x0001CFF0
		public override void OnStart()
		{
			if (!this.registered)
			{
				base.Owner.RegisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
				base.Owner.RegisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
				base.Owner.RegisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
				base.Owner.RegisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
				this.registered = true;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001EC97 File Offset: 0x0001D097
		public override TaskStatus OnUpdate()
		{
			return (!this.eventReceived) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001ECAC File Offset: 0x0001D0AC
		public override void OnEnd()
		{
			if (this.eventReceived)
			{
				base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
				base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
				this.registered = false;
			}
			this.eventReceived = false;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001ED5A File Offset: 0x0001D15A
		private void ReceivedEvent()
		{
			this.eventReceived = true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001ED63 File Offset: 0x0001D163
		private void ReceivedEvent(object arg1)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001ED94 File Offset: 0x0001D194
		private void ReceivedEvent(object arg1, object arg2)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
			if (this.storedValue2 != null && !this.storedValue2.IsNone)
			{
				this.storedValue2.SetValue(arg2);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001EDF8 File Offset: 0x0001D1F8
		private void ReceivedEvent(object arg1, object arg2, object arg3)
		{
			this.ReceivedEvent();
			if (this.storedValue1 != null && !this.storedValue1.IsNone)
			{
				this.storedValue1.SetValue(arg1);
			}
			if (this.storedValue2 != null && !this.storedValue2.IsNone)
			{
				this.storedValue2.SetValue(arg2);
			}
			if (this.storedValue3 != null && !this.storedValue3.IsNone)
			{
				this.storedValue3.SetValue(arg3);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001EE80 File Offset: 0x0001D280
		public override void OnBehaviorComplete()
		{
			base.Owner.UnregisterEvent(this.eventName.Value, new Action(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object>(this.eventName.Value, new Action<object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object>(this.eventName.Value, new Action<object, object>(this.ReceivedEvent));
			base.Owner.UnregisterEvent<object, object, object>(this.eventName.Value, new Action<object, object, object>(this.ReceivedEvent));
			this.eventReceived = false;
			this.registered = false;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001EF23 File Offset: 0x0001D323
		public override void OnReset()
		{
			this.eventName = string.Empty;
		}

		// Token: 0x04000457 RID: 1111
		[Tooltip("The name of the event to receive")]
		public SharedString eventName = string.Empty;

		// Token: 0x04000458 RID: 1112
		[Tooltip("Optionally store the first sent argument")]
		[SharedRequired]
		public SharedVariable storedValue1;

		// Token: 0x04000459 RID: 1113
		[Tooltip("Optionally store the second sent argument")]
		[SharedRequired]
		public SharedVariable storedValue2;

		// Token: 0x0400045A RID: 1114
		[Tooltip("Optionally store the third sent argument")]
		[SharedRequired]
		public SharedVariable storedValue3;

		// Token: 0x0400045B RID: 1115
		private bool eventReceived;

		// Token: 0x0400045C RID: 1116
		private bool registered;
	}
}
