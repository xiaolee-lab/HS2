using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000511 RID: 1297
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchJoystickExample : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEventSystemHandler
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00099412 File Offset: 0x00097812
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0009941A File Offset: 0x0009781A
		public Vector2 position { get; private set; }

		// Token: 0x060018CB RID: 6347 RVA: 0x00099423 File Offset: 0x00097823
		private void Start()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
			this.StoreOrigValues();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00099440 File Offset: 0x00097840
		private void Update()
		{
			if ((float)Screen.width != this.origScreenResolution.x || (float)Screen.height != this.origScreenResolution.y || Screen.orientation != this.origScreenOrientation)
			{
				this.Restart();
				this.StoreOrigValues();
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00099495 File Offset: 0x00097895
		private void Restart()
		{
			this.hasFinger = false;
			(base.transform as RectTransform).anchoredPosition = this.origAnchoredPosition;
			this.position = Vector2.zero;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000994C0 File Offset: 0x000978C0
		private void StoreOrigValues()
		{
			this.origAnchoredPosition = (base.transform as RectTransform).anchoredPosition;
			this.origWorldPosition = base.transform.position;
			this.origScreenResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.origScreenOrientation = Screen.orientation;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00099518 File Offset: 0x00097918
		private void UpdateValue(Vector3 value)
		{
			Vector3 a = this.origWorldPosition - value;
			a.y = -a.y;
			a /= (float)this.radius;
			this.position = new Vector2(-a.x, a.y);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00099569 File Offset: 0x00097969
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.hasFinger)
			{
				return;
			}
			if (!this.allowMouseControl && TouchJoystickExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.hasFinger = true;
			this.lastFingerId = eventData.pointerId;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000995A6 File Offset: 0x000979A6
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			if (!this.allowMouseControl && TouchJoystickExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.Restart();
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000995DC File Offset: 0x000979DC
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.hasFinger || eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			Vector3 vector = new Vector3(eventData.position.x - this.origWorldPosition.x, eventData.position.y - this.origWorldPosition.y);
			vector = Vector3.ClampMagnitude(vector, (float)this.radius);
			Vector3 vector2 = this.origWorldPosition + vector;
			base.transform.position = vector2;
			this.UpdateValue(vector2);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0009966F File Offset: 0x00097A6F
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x04001BAF RID: 7087
		public bool allowMouseControl = true;

		// Token: 0x04001BB0 RID: 7088
		public int radius = 50;

		// Token: 0x04001BB1 RID: 7089
		private Vector2 origAnchoredPosition;

		// Token: 0x04001BB2 RID: 7090
		private Vector3 origWorldPosition;

		// Token: 0x04001BB3 RID: 7091
		private Vector2 origScreenResolution;

		// Token: 0x04001BB4 RID: 7092
		private ScreenOrientation origScreenOrientation;

		// Token: 0x04001BB5 RID: 7093
		[NonSerialized]
		private bool hasFinger;

		// Token: 0x04001BB6 RID: 7094
		[NonSerialized]
		private int lastFingerId;
	}
}
