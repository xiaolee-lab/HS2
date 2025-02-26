using System;
using Housing.Command;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Housing
{
	// Token: 0x020008C5 RID: 2245
	public class GuideRotation : GuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x001575B3 File Offset: 0x001559B3
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

		// Token: 0x06003AC0 RID: 15040 RVA: 0x001575D7 File Offset: 0x001559D7
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x001575EC File Offset: 0x001559EC
		public void OnInitializePotentialDrag(PointerEventData _eventData)
		{
			if (!this.isDragButton)
			{
				this.isDragButton = (_eventData.button == PointerEventData.InputButton.Left);
			}
			this.isDragButton = (_eventData.button == PointerEventData.InputButton.Left);
			if (!this.isDragButton)
			{
				return;
			}
			this.prevScreenPos = _eventData.position;
			this.prevPlanePos = this.PlanePos(_eventData.position);
			this.objectCtrl = base.GuideObject.ObjectCtrl;
			this.oldRot = this.objectCtrl.LocalEulerAngles;
			this.workRot = this.oldRot;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x0015767C File Offset: 0x00155A7C
		public override void OnDrag(PointerEventData _eventData)
		{
			if (!this.isDragButton)
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
				float value = this.VectorToAngle(new Vector2(vector.x, vector.y), new Vector2(vector2.x, vector2.y));
				zero[(int)this.axis] = value;
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
				float value2 = this.VectorToAngle(new Vector2(vector3.x, vector3.y), new Vector2(vector4.x, vector4.y));
				zero[(int)this.axis] = value2;
				this.prevPlanePos = position4;
			}
			this.prevScreenPos = _eventData.position;
			Vector3 eulerAngles = (Quaternion.Euler(this.workRot) * Quaternion.Euler(zero)).eulerAngles;
			eulerAngles.x %= 360f;
			eulerAngles.y %= 360f;
			eulerAngles.z %= 360f;
			this.workRot = eulerAngles;
			eulerAngles.y = this.Round(eulerAngles.y);
			Vector3 localEulerAngles = this.objectCtrl.LocalEulerAngles;
			this.objectCtrl.LocalEulerAngles = eulerAngles;
			if (Singleton<GuideManager>.IsInstance() && !Singleton<GuideManager>.Instance.CheckRot(this.objectCtrl))
			{
				this.objectCtrl.LocalEulerAngles = localEulerAngles;
			}
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x00157975 File Offset: 0x00155D75
		public override void OnPointerUp(PointerEventData _eventData)
		{
			if (this.isDragButton && _eventData.dragging)
			{
				return;
			}
			base.OnPointerUp(_eventData);
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x00157998 File Offset: 0x00155D98
		public override void OnEndDrag(PointerEventData _eventData)
		{
			if (!this.isDragButton)
			{
				_eventData.dragging = false;
				base.OnEndDrag(_eventData);
				return;
			}
			if (_eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			base.OnEndDrag(_eventData);
			if (Singleton<UndoRedoManager>.IsInstance())
			{
				Singleton<UndoRedoManager>.Instance.Push(new RotationCommand(this.objectCtrl, this.oldRot));
			}
			Singleton<Housing>.Instance.CheckOverlap(this.objectCtrl as OCItem);
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x00157A24 File Offset: 0x00155E24
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

		// Token: 0x06003AC6 RID: 15046 RVA: 0x00157ACC File Offset: 0x00155ECC
		private float VectorToAngle(Vector2 _v1, Vector2 _v2)
		{
			float current = Mathf.Atan2(_v1.x, _v1.y) * 57.29578f;
			float target = Mathf.Atan2(_v2.x, _v2.y) * 57.29578f;
			return Mathf.DeltaAngle(current, target);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x00157B14 File Offset: 0x00155F14
		private float Round(float _value)
		{
			bool flag = _value < 0f;
			return (float)Mathf.RoundToInt(Mathf.Abs(_value) / 90f) * 90f * (float)((!flag) ? 1 : -1);
		}

		// Token: 0x040039EF RID: 14831
		public GuideRotation.RotationAxis axis;

		// Token: 0x040039F0 RID: 14832
		private Vector2 prevScreenPos = Vector2.zero;

		// Token: 0x040039F1 RID: 14833
		private Vector3 prevPlanePos = Vector3.zero;

		// Token: 0x040039F2 RID: 14834
		private ObjectCtrl objectCtrl;

		// Token: 0x040039F3 RID: 14835
		private Vector3 oldRot = Vector3.zero;

		// Token: 0x040039F4 RID: 14836
		private Vector3 workRot = Vector3.zero;

		// Token: 0x040039F5 RID: 14837
		private Camera m_Camera;

		// Token: 0x040039F6 RID: 14838
		private bool isDragButton;

		// Token: 0x020008C6 RID: 2246
		public enum RotationAxis
		{
			// Token: 0x040039F8 RID: 14840
			X,
			// Token: 0x040039F9 RID: 14841
			Y,
			// Token: 0x040039FA RID: 14842
			Z,
			// Token: 0x040039FB RID: 14843
			XYZ
		}
	}
}
