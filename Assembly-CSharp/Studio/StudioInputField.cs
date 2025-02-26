using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200123D RID: 4669
	[AddComponentMenu("Studio/GUI/Input Field", 1000)]
	public class StudioInputField : InputField
	{
		// Token: 0x0600998E RID: 39310 RVA: 0x003F3D40 File Offset: 0x003F2140
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
		}

		// Token: 0x0600998F RID: 39311 RVA: 0x003F3D49 File Offset: 0x003F2149
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
		}

		// Token: 0x06009990 RID: 39312 RVA: 0x003F3D52 File Offset: 0x003F2152
		public override void OnSubmit(BaseEventData eventData)
		{
			base.OnSubmit(eventData);
		}

		// Token: 0x06009991 RID: 39313 RVA: 0x003F3D5B File Offset: 0x003F215B
		protected override void Start()
		{
			base.Start();
		}
	}
}
