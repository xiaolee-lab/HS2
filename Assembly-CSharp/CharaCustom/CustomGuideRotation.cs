using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharaCustom
{
	// Token: 0x02000A17 RID: 2583
	public class CustomGuideRotation : CustomGuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x001D8471 File Offset: 0x001D6871
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

		// Token: 0x06004CDC RID: 19676 RVA: 0x001D8495 File Offset: 0x001D6895
		public void OnPointerDown(PointerEventData eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(base.guideObject.ccv2))
			{
				return;
			}
			CustomGuideAssist.SetCameraMoveFlag(base.guideObject.ccv2, false);
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x001D84C0 File Offset: 0x001D68C0
		public void OnInitializePotentialDrag(PointerEventData _eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(base.guideObject.ccv2))
			{
				return;
			}
			base.guideObject.isDrag = true;
			this.prevScreenPos = _eventData.position;
			this.prevPlanePos = this.PlanePos(_eventData.position);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x001D8510 File Offset: 0x001D6910
		private float LimitedValue(CustomGuideRotation.RotationAxis axis, float val)
		{
			float result = 0f;
			if (axis == CustomGuideRotation.RotationAxis.X)
			{
				float min = this.guidLimit.limitMin.x;
				float max = this.guidLimit.limitMax.x;
				if (this.guidLimit.limitMin.x > this.guidLimit.limitMax.x)
				{
					min = this.guidLimit.limitMax.x;
					max = this.guidLimit.limitMin.x;
				}
				val = ((val <= 180f) ? val : (val - 360f));
				result = Mathf.Clamp(val, min, max);
			}
			else if (axis == CustomGuideRotation.RotationAxis.Y)
			{
				float min = this.guidLimit.limitMin.y;
				float max = this.guidLimit.limitMax.y;
				if (this.guidLimit.limitMin.y > this.guidLimit.limitMax.y)
				{
					min = this.guidLimit.limitMax.y;
					max = this.guidLimit.limitMin.y;
				}
				val = ((val <= 180f) ? val : (val - 360f));
				result = Mathf.Clamp(val, min, max);
			}
			else if (axis == CustomGuideRotation.RotationAxis.Z)
			{
				float min = this.guidLimit.limitMin.z;
				float max = this.guidLimit.limitMax.z;
				if (this.guidLimit.limitMin.z > this.guidLimit.limitMax.z)
				{
					min = this.guidLimit.limitMax.z;
					max = this.guidLimit.limitMin.z;
				}
				val = ((val <= 180f) ? val : (val - 360f));
				result = Mathf.Clamp(val, min, max);
			}
			return result;
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x001D86F4 File Offset: 0x001D6AF4
		public override void OnDrag(PointerEventData _eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(base.guideObject.ccv2))
			{
				return;
			}
			base.OnDrag(_eventData);
			Vector3 zero = Vector3.zero;
			float f = Vector3.Dot(this.camera.transform.forward, base.transform.right);
			if (Mathf.Abs(f) > 0.1f)
			{
				Vector3 position = this.PlanePos(_eventData.position);
				Vector3 vector = Quaternion.Euler(0f, 90f, 0f) * base.transform.InverseTransformPoint(this.prevPlanePos);
				Vector3 vector2 = Quaternion.Euler(0f, 90f, 0f) * base.transform.InverseTransformPoint(position);
				float num = this.VectorToAngle(new Vector2(vector.x, vector.y), new Vector2(vector2.x, vector2.y));
				if (null != this.guidLimit && this.guidLimit.limited)
				{
					zero[(int)this.axis] = this.LimitedValue(this.axis, num);
				}
				else
				{
					zero[(int)this.axis] = num;
				}
				this.prevPlanePos = position;
			}
			else
			{
				Vector3 position2 = _eventData.position;
				position2.z = Vector3.Distance(this.prevPlanePos, this.camera.transform.position);
				Vector3 position3 = this.prevScreenPos;
				position3.z = Vector3.Distance(this.prevPlanePos, this.camera.transform.position);
				Vector3 b = this.camera.ScreenToWorldPoint(position2) - this.camera.ScreenToWorldPoint(position3);
				Vector3 position4 = this.prevPlanePos + b;
				Vector3 vector3 = Quaternion.Euler(0f, 90f, 0f) * base.transform.InverseTransformPoint(this.prevPlanePos);
				Vector3 vector4 = Quaternion.Euler(0f, 90f, 0f) * base.transform.InverseTransformPoint(position4);
				this.prevPlanePos = position4;
				float num2 = this.VectorToAngle(new Vector2(vector3.x, vector3.y), new Vector2(vector4.x, vector4.y));
				if (null != this.guidLimit && this.guidLimit.limited)
				{
					zero[(int)this.axis] = this.LimitedValue(this.axis, num2);
				}
				else
				{
					zero[(int)this.axis] = num2;
				}
				this.prevPlanePos = position4;
			}
			this.prevScreenPos = _eventData.position;
			Vector3 eulerAngles = (Quaternion.Euler(base.guideObject.amount.rotation) * Quaternion.Euler(zero)).eulerAngles;
			eulerAngles.x %= 360f;
			eulerAngles.y %= 360f;
			eulerAngles.z %= 360f;
			base.guideObject.amount.rotation = eulerAngles;
			base.guideObject.ctrlAxisType = (int)this.axis;
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x001D8A44 File Offset: 0x001D6E44
		public void OnPointerUp(PointerEventData _eventData)
		{
			CustomGuideAssist.SetCameraMoveFlag(base.guideObject.ccv2, true);
			base.guideObject.isDrag = false;
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x001D8A64 File Offset: 0x001D6E64
		private Vector3 PlanePos(Vector2 _screenPos)
		{
			Plane plane = new Plane(base.transform.right, base.transform.position);
			if (!plane.GetSide(this.camera.transform.position))
			{
				plane.SetNormalAndPosition(base.transform.right * -1f, base.transform.position);
			}
			Ray ray = RectTransformUtility.ScreenPointToRay(this.camera, _screenPos);
			float distance = 0f;
			return (!plane.Raycast(ray, out distance)) ? base.transform.position : ray.GetPoint(distance);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x001D8B0C File Offset: 0x001D6F0C
		private float VectorToAngle(Vector2 _v1, Vector2 _v2)
		{
			float current = Mathf.Atan2(_v1.x, _v1.y) * 57.29578f;
			float target = Mathf.Atan2(_v2.x, _v2.y) * 57.29578f;
			return Mathf.DeltaAngle(current, target);
		}

		// Token: 0x0400466B RID: 18027
		public CustomGuideRotation.RotationAxis axis;

		// Token: 0x0400466C RID: 18028
		[SerializeField]
		private CustomGuideLimit guidLimit;

		// Token: 0x0400466D RID: 18029
		private Vector2 prevScreenPos = Vector2.zero;

		// Token: 0x0400466E RID: 18030
		private Vector3 prevPlanePos = Vector3.zero;

		// Token: 0x0400466F RID: 18031
		private Camera m_Camera;

		// Token: 0x02000A18 RID: 2584
		public enum RotationAxis
		{
			// Token: 0x04004671 RID: 18033
			X,
			// Token: 0x04004672 RID: 18034
			Y,
			// Token: 0x04004673 RID: 18035
			Z
		}
	}
}
