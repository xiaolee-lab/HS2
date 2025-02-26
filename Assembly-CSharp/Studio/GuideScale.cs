using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x02001253 RID: 4691
	public class GuideScale : GuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x06009AC3 RID: 39619 RVA: 0x003F811E File Offset: 0x003F651E
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

		// Token: 0x06009AC4 RID: 39620 RVA: 0x003F8142 File Offset: 0x003F6542
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x06009AC5 RID: 39621 RVA: 0x003F8144 File Offset: 0x003F6544
		public void OnInitializePotentialDrag(PointerEventData _eventData)
		{
			this.prevPos = _eventData.position;
			this.dicChangeAmount = Singleton<GuideObjectManager>.Instance.selectObjectDictionary;
			this.dicOldScale = this.dicChangeAmount.ToDictionary((KeyValuePair<int, ChangeAmount> v) => v.Key, (KeyValuePair<int, ChangeAmount> v) => v.Value.scale);
		}

		// Token: 0x06009AC6 RID: 39622 RVA: 0x003F81B8 File Offset: 0x003F65B8
		public override void OnDrag(PointerEventData _eventData)
		{
			base.OnDrag(_eventData);
			Vector3 b = Vector3.zero;
			if (this.axis == GuideScale.ScaleAxis.XYZ)
			{
				Vector2 delta = _eventData.delta;
				float d = (delta.x + delta.y) * this.speed;
				b = Vector3.one * d;
			}
			else
			{
				b = this.AxisMove(_eventData.delta);
			}
			foreach (KeyValuePair<int, ChangeAmount> keyValuePair in this.dicChangeAmount)
			{
				Vector3 vector = keyValuePair.Value.scale;
				vector += b;
				vector.x = Mathf.Clamp(vector.x, 0.01f, 9999999f);
				vector.y = Mathf.Clamp(vector.y, 0.01f, 9999999f);
				vector.z = Mathf.Clamp(vector.z, 0.01f, 9999999f);
				keyValuePair.Value.scale = vector;
			}
		}

		// Token: 0x06009AC7 RID: 39623 RVA: 0x003F82E0 File Offset: 0x003F66E0
		public void OnPointerUp(PointerEventData _eventData)
		{
		}

		// Token: 0x06009AC8 RID: 39624 RVA: 0x003F82E4 File Offset: 0x003F66E4
		public override void OnEndDrag(PointerEventData _eventData)
		{
			base.OnEndDrag(_eventData);
			GuideCommand.EqualsInfo[] changeAmountInfo = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select new GuideCommand.EqualsInfo
			{
				dicKey = v,
				oldValue = this.dicOldScale[v],
				newValue = this.dicChangeAmount[v].scale
			}).ToArray<GuideCommand.EqualsInfo>();
			Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.ScaleEqualsCommand(changeAmountInfo));
		}

		// Token: 0x06009AC9 RID: 39625 RVA: 0x003F832C File Offset: 0x003F672C
		private Vector3 AxisPos(Vector2 _screenPos)
		{
			Vector3 position = base.transform.position;
			Plane plane = new Plane(this.camera.transform.forward * -1f, position);
			Ray ray = RectTransformUtility.ScreenPointToRay(this.camera, _screenPos);
			float distance = 0f;
			Vector3 a = (!plane.Raycast(ray, out distance)) ? position : ray.GetPoint(distance);
			Vector3 vector = a - position;
			Vector3 onNormal = base.transform.up;
			GuideScale.ScaleAxis scaleAxis = this.axis;
			if (scaleAxis != GuideScale.ScaleAxis.X)
			{
				if (scaleAxis != GuideScale.ScaleAxis.Y)
				{
					if (scaleAxis == GuideScale.ScaleAxis.Z)
					{
						onNormal = Vector3.forward;
					}
				}
				else
				{
					onNormal = Vector3.up;
				}
			}
			else
			{
				onNormal = Vector3.right;
			}
			return Vector3.Project(vector, onNormal);
		}

		// Token: 0x06009ACA RID: 39626 RVA: 0x003F8404 File Offset: 0x003F6804
		private Vector3 AxisMove(Vector2 _delta)
		{
			Vector3 vector = this.camera.transform.TransformVector(_delta.x * 0.005f, _delta.y * 0.005f, 0f);
			vector *= Studio.optionSystem.manipuleteSpeed;
			vector = base.transform.InverseTransformVector(vector);
			GuideScale.ScaleAxis scaleAxis = this.axis;
			if (scaleAxis != GuideScale.ScaleAxis.X)
			{
				if (scaleAxis != GuideScale.ScaleAxis.Y)
				{
					if (scaleAxis == GuideScale.ScaleAxis.Z)
					{
						vector = Vector3.Scale(vector, Vector3.forward);
					}
				}
				else
				{
					vector = Vector3.Scale(vector, Vector3.up);
				}
			}
			else
			{
				vector = Vector3.Scale(vector, Vector3.right);
			}
			return vector;
		}

		// Token: 0x04007B6A RID: 31594
		public GuideScale.ScaleAxis axis;

		// Token: 0x04007B6B RID: 31595
		private float speed = 0.001f;

		// Token: 0x04007B6C RID: 31596
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007B6D RID: 31597
		private Vector2 prevPos = Vector2.zero;

		// Token: 0x04007B6E RID: 31598
		private Camera m_Camera;

		// Token: 0x04007B6F RID: 31599
		private Dictionary<int, Vector3> dicOldScale;

		// Token: 0x04007B70 RID: 31600
		private Dictionary<int, ChangeAmount> dicChangeAmount;

		// Token: 0x02001254 RID: 4692
		public enum ScaleAxis
		{
			// Token: 0x04007B74 RID: 31604
			X,
			// Token: 0x04007B75 RID: 31605
			Y,
			// Token: 0x04007B76 RID: 31606
			Z,
			// Token: 0x04007B77 RID: 31607
			XYZ
		}
	}
}
