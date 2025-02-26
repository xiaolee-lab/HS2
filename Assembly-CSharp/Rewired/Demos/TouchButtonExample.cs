using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000510 RID: 1296
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchButtonExample : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0009936A File Offset: 0x0009776A
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x00099372 File Offset: 0x00097772
		public bool isPressed { get; private set; }

		// Token: 0x060018C3 RID: 6339 RVA: 0x0009937B File Offset: 0x0009777B
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0009938F File Offset: 0x0009778F
		private void Restart()
		{
			this.isPressed = false;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00099398 File Offset: 0x00097798
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = true;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000993BD File Offset: 0x000977BD
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = false;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000993E2 File Offset: 0x000977E2
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x04001BAD RID: 7085
		public bool allowMouseControl = true;
	}
}
