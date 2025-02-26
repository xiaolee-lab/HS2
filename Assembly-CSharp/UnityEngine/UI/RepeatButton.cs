using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200115E RID: 4446
	public abstract class RepeatButton : SelectUI, IPointerUpHandler, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x060092DC RID: 37596
		protected abstract void Process(bool push);

		// Token: 0x060092DD RID: 37597 RVA: 0x003CE814 File Offset: 0x003CCC14
		public void OnPointerDown(PointerEventData eventData)
		{
			this.push = true;
		}

		// Token: 0x060092DE RID: 37598 RVA: 0x003CE81D File Offset: 0x003CCC1D
		public void OnPointerUp(PointerEventData eventData)
		{
			this.push = false;
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x003CE826 File Offset: 0x003CCC26
		protected virtual void Awake()
		{
			this.push = false;
		}

		// Token: 0x060092E0 RID: 37600 RVA: 0x003CE82F File Offset: 0x003CCC2F
		private void Update()
		{
			this.Process(this.push);
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x003CE83D File Offset: 0x003CCC3D
		protected override void OnDisable()
		{
			base.OnDisable();
			this.push = false;
		}

		// Token: 0x040076E0 RID: 30432
		private bool push;
	}
}
