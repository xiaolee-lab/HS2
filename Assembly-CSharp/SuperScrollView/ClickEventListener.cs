using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	// Token: 0x020005EE RID: 1518
	public class ClickEventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x06002331 RID: 9009 RVA: 0x000C2F94 File Offset: 0x000C1394
		public static ClickEventListener Get(GameObject obj)
		{
			ClickEventListener clickEventListener = obj.GetComponent<ClickEventListener>();
			if (clickEventListener == null)
			{
				clickEventListener = obj.AddComponent<ClickEventListener>();
			}
			return clickEventListener;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x000C2FBC File Offset: 0x000C13BC
		public bool IsPressd
		{
			get
			{
				return this.mIsPressed;
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000C2FC4 File Offset: 0x000C13C4
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				if (this.mDoubleClickedHandler != null)
				{
					this.mDoubleClickedHandler(base.gameObject);
				}
			}
			else if (this.mClickedHandler != null)
			{
				this.mClickedHandler(base.gameObject);
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000C301A File Offset: 0x000C141A
		public void SetClickEventHandler(Action<GameObject> handler)
		{
			this.mClickedHandler = handler;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000C3023 File Offset: 0x000C1423
		public void SetDoubleClickEventHandler(Action<GameObject> handler)
		{
			this.mDoubleClickedHandler = handler;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000C302C File Offset: 0x000C142C
		public void SetPointerDownHandler(Action<GameObject> handler)
		{
			this.mOnPointerDownHandler = handler;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000C3035 File Offset: 0x000C1435
		public void SetPointerUpHandler(Action<GameObject> handler)
		{
			this.mOnPointerUpHandler = handler;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000C303E File Offset: 0x000C143E
		public void OnPointerDown(PointerEventData eventData)
		{
			this.mIsPressed = true;
			if (this.mOnPointerDownHandler != null)
			{
				this.mOnPointerDownHandler(base.gameObject);
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000C3063 File Offset: 0x000C1463
		public void OnPointerUp(PointerEventData eventData)
		{
			this.mIsPressed = false;
			if (this.mOnPointerUpHandler != null)
			{
				this.mOnPointerUpHandler(base.gameObject);
			}
		}

		// Token: 0x040022BF RID: 8895
		private Action<GameObject> mClickedHandler;

		// Token: 0x040022C0 RID: 8896
		private Action<GameObject> mDoubleClickedHandler;

		// Token: 0x040022C1 RID: 8897
		private Action<GameObject> mOnPointerDownHandler;

		// Token: 0x040022C2 RID: 8898
		private Action<GameObject> mOnPointerUpHandler;

		// Token: 0x040022C3 RID: 8899
		private bool mIsPressed;
	}
}
