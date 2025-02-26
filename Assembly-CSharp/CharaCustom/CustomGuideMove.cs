using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharaCustom
{
	// Token: 0x02000A13 RID: 2579
	public class CustomGuideMove : CustomGuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x001D7C3F File Offset: 0x001D603F
		private Camera camera
		{
			get
			{
				if (this.m_Camera == null)
				{
					this.m_Camera = Camera.main;
				}
				return this.m_Camera;
			}
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x001D7C63 File Offset: 0x001D6063
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(base.guideObject.ccv2))
			{
				return;
			}
			this.oldPos = eventData.pressPosition;
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x001D7C88 File Offset: 0x001D6088
		public override void OnDrag(PointerEventData eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(base.guideObject.ccv2))
			{
				return;
			}
			base.OnDrag(eventData);
			bool flag = false;
			Vector3 b = (this.axis != CustomGuideMove.MoveAxis.XYZ) ? this.AxisMove(eventData.delta, ref flag) : (this.WorldPos(eventData.position) - this.WorldPos(this.oldPos));
			Vector3 vector = base.guideObject.amount.position;
			vector += b;
			if (null != this.guidLimit && this.guidLimit.limited)
			{
				Vector3 position = this.guidLimit.trfParent.InverseTransformPoint(vector);
				float min = this.guidLimit.limitMin.x;
				float max = this.guidLimit.limitMax.x;
				if (this.guidLimit.limitMin.x > this.guidLimit.limitMax.x)
				{
					min = this.guidLimit.limitMax.x;
					max = this.guidLimit.limitMin.x;
				}
				position.x = Mathf.Clamp(position.x, min, max);
				min = this.guidLimit.limitMin.y;
				max = this.guidLimit.limitMax.y;
				if (this.guidLimit.limitMin.y > this.guidLimit.limitMax.y)
				{
					min = this.guidLimit.limitMax.y;
					max = this.guidLimit.limitMin.y;
				}
				position.y = Mathf.Clamp(position.y, min, max);
				min = this.guidLimit.limitMin.z;
				max = this.guidLimit.limitMax.z;
				if (this.guidLimit.limitMin.z > this.guidLimit.limitMax.z)
				{
					min = this.guidLimit.limitMax.z;
					max = this.guidLimit.limitMin.z;
				}
				position.z = Mathf.Clamp(position.z, min, max);
				Vector3 vector2 = this.guidLimit.trfParent.TransformPoint(position);
				if (this.axis == CustomGuideMove.MoveAxis.XYZ || this.axis == CustomGuideMove.MoveAxis.X)
				{
					vector.x = vector2.x;
				}
				if (this.axis == CustomGuideMove.MoveAxis.XYZ || this.axis == CustomGuideMove.MoveAxis.Y)
				{
					vector.y = vector2.y;
				}
				if (this.axis == CustomGuideMove.MoveAxis.XYZ || this.axis == CustomGuideMove.MoveAxis.Z)
				{
					vector.z = vector2.z;
				}
			}
			base.guideObject.amount.position = ((this.axis != CustomGuideMove.MoveAxis.XYZ) ? ((!flag) ? vector : this.Parse(vector)) : vector);
			base.guideObject.ctrlAxisType = (int)this.axis;
			this.oldPos = eventData.position;
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x001D7F9F File Offset: 0x001D639F
		public void OnPointerDown(PointerEventData eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(base.guideObject.ccv2))
			{
				return;
			}
			CustomGuideAssist.SetCameraMoveFlag(base.guideObject.ccv2, false);
			base.guideObject.isDrag = true;
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x001D7FD4 File Offset: 0x001D63D4
		public void OnPointerUp(PointerEventData eventData)
		{
			CustomGuideAssist.SetCameraMoveFlag(base.guideObject.ccv2, true);
			base.guideObject.isDrag = false;
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x001D7FF4 File Offset: 0x001D63F4
		private Vector3 WorldPos(Vector2 _screenPos)
		{
			Plane plane = new Plane(this.camera.transform.forward * -1f, base.transform.position);
			Ray ray = RectTransformUtility.ScreenPointToRay(this.camera, _screenPos);
			float distance = 0f;
			return (!plane.Raycast(ray, out distance)) ? base.transform.position : ray.GetPoint(distance);
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x001D8068 File Offset: 0x001D6468
		private Vector3 AxisPos(Vector2 _screenPos)
		{
			Vector3 position = base.transform.position;
			Plane plane = new Plane(base.transform.forward, position);
			if (!plane.GetSide(this.camera.transform.position))
			{
				plane = new Plane(base.transform.forward * -1f, position);
			}
			Vector3 up = base.transform.up;
			Ray ray = RectTransformUtility.ScreenPointToRay(this.camera, _screenPos);
			float distance = 0f;
			return (!plane.Raycast(ray, out distance)) ? Vector3.Project(position, up) : Vector3.Project(ray.GetPoint(distance), up);
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x001D8118 File Offset: 0x001D6518
		private Vector3 AxisMove(Vector2 _delta, ref bool _snap)
		{
			Vector3 vector = this.camera.transform.TransformVector(_delta.x * 0.01f, _delta.y * 0.01f, 0f);
			Vector3 up = base.transform.up;
			return up * vector.magnitude * base.guideObject.speedMove * Vector3.Dot(vector.normalized, up);
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x001D8190 File Offset: 0x001D6590
		private Vector3 Parse(Vector3 _src)
		{
			return _src;
		}

		// Token: 0x04004656 RID: 18006
		[SerializeField]
		private CustomGuideLimit guidLimit;

		// Token: 0x04004657 RID: 18007
		public CustomGuideMove.MoveAxis axis;

		// Token: 0x04004658 RID: 18008
		private Vector2 oldPos = Vector2.zero;

		// Token: 0x04004659 RID: 18009
		private Camera m_Camera;

		// Token: 0x02000A14 RID: 2580
		public enum MoveAxis
		{
			// Token: 0x0400465B RID: 18011
			X,
			// Token: 0x0400465C RID: 18012
			Y,
			// Token: 0x0400465D RID: 18013
			Z,
			// Token: 0x0400465E RID: 18014
			XYZ
		}
	}
}
