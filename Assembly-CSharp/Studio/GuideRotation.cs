using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x02001251 RID: 4689
	public class GuideRotation : GuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x06009AB7 RID: 39607 RVA: 0x003F7B3C File Offset: 0x003F5F3C
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

		// Token: 0x06009AB8 RID: 39608 RVA: 0x003F7B60 File Offset: 0x003F5F60
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x06009AB9 RID: 39609 RVA: 0x003F7B64 File Offset: 0x003F5F64
		public void OnInitializePotentialDrag(PointerEventData _eventData)
		{
			this.prevScreenPos = _eventData.position;
			this.prevPlanePos = this.PlanePos(_eventData.position);
			this.dicChangeAmount = Singleton<GuideObjectManager>.Instance.selectObjectDictionary;
			this.dicOldRot = this.dicChangeAmount.ToDictionary((KeyValuePair<int, ChangeAmount> v) => v.Key, (KeyValuePair<int, ChangeAmount> v) => v.Value.rot);
		}

		// Token: 0x06009ABA RID: 39610 RVA: 0x003F7BEC File Offset: 0x003F5FEC
		public override void OnDrag(PointerEventData _eventData)
		{
			base.OnDrag(_eventData);
			GuideRotation.RotationAxis rotationAxis = this.axis;
			if (rotationAxis != GuideRotation.RotationAxis.XYZ)
			{
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
				foreach (KeyValuePair<int, ChangeAmount> keyValuePair in this.dicChangeAmount)
				{
					Vector3 eulerAngles = (Quaternion.Euler(keyValuePair.Value.rot) * Quaternion.Euler(zero)).eulerAngles;
					eulerAngles.x %= 360f;
					eulerAngles.y %= 360f;
					eulerAngles.z %= 360f;
					keyValuePair.Value.rot = eulerAngles;
				}
			}
			else
			{
				foreach (GuideObject guideObject in Singleton<GuideObjectManager>.Instance.selectObjects)
				{
					guideObject.Rotation(this.camera.transform.up, -_eventData.delta.x);
					guideObject.Rotation(this.camera.transform.right, _eventData.delta.y);
				}
			}
		}

		// Token: 0x06009ABB RID: 39611 RVA: 0x003F7F68 File Offset: 0x003F6368
		public void OnPointerUp(PointerEventData _eventData)
		{
		}

		// Token: 0x06009ABC RID: 39612 RVA: 0x003F7F6C File Offset: 0x003F636C
		public override void OnEndDrag(PointerEventData _eventData)
		{
			base.OnEndDrag(_eventData);
			GuideCommand.EqualsInfo[] changeAmountInfo = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select new GuideCommand.EqualsInfo
			{
				dicKey = v,
				oldValue = this.dicOldRot[v],
				newValue = this.dicChangeAmount[v].rot
			}).ToArray<GuideCommand.EqualsInfo>();
			Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.RotationEqualsCommand(changeAmountInfo));
		}

		// Token: 0x06009ABD RID: 39613 RVA: 0x003F7FB4 File Offset: 0x003F63B4
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

		// Token: 0x06009ABE RID: 39614 RVA: 0x003F805C File Offset: 0x003F645C
		private float VectorToAngle(Vector2 _v1, Vector2 _v2)
		{
			float current = Mathf.Atan2(_v1.x, _v1.y) * 57.29578f;
			float target = Mathf.Atan2(_v2.x, _v2.y) * 57.29578f;
			return Mathf.DeltaAngle(current, target);
		}

		// Token: 0x04007B5D RID: 31581
		public GuideRotation.RotationAxis axis;

		// Token: 0x04007B5E RID: 31582
		private Vector2 prevScreenPos = Vector2.zero;

		// Token: 0x04007B5F RID: 31583
		private Vector3 prevPlanePos = Vector3.zero;

		// Token: 0x04007B60 RID: 31584
		private Dictionary<int, Vector3> dicOldRot;

		// Token: 0x04007B61 RID: 31585
		private Dictionary<int, ChangeAmount> dicChangeAmount;

		// Token: 0x04007B62 RID: 31586
		private Camera m_Camera;

		// Token: 0x02001252 RID: 4690
		public enum RotationAxis
		{
			// Token: 0x04007B66 RID: 31590
			X,
			// Token: 0x04007B67 RID: 31591
			Y,
			// Token: 0x04007B68 RID: 31592
			Z,
			// Token: 0x04007B69 RID: 31593
			XYZ
		}
	}
}
