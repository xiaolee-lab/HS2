using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x0200124B RID: 4683
	public class GuideMove : GuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06009A60 RID: 39520 RVA: 0x003F64AB File Offset: 0x003F48AB
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

		// Token: 0x06009A61 RID: 39521 RVA: 0x003F64CF File Offset: 0x003F48CF
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x06009A62 RID: 39522 RVA: 0x003F64D4 File Offset: 0x003F48D4
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.oldPos = eventData.pressPosition;
			Dictionary<int, ChangeAmount> selectObjectDictionary = Singleton<GuideObjectManager>.Instance.selectObjectDictionary;
			this.dicOld = selectObjectDictionary.ToDictionary((KeyValuePair<int, ChangeAmount> v) => v.Key, (KeyValuePair<int, ChangeAmount> v) => v.Value.pos);
			this.isSnap = true;
		}

		// Token: 0x06009A63 RID: 39523 RVA: 0x003F6548 File Offset: 0x003F4948
		public override void OnDrag(PointerEventData eventData)
		{
			base.OnDrag(eventData);
			switch (this.axis)
			{
			case GuideMove.MoveAxis.X:
			case GuideMove.MoveAxis.Y:
			case GuideMove.MoveAxis.Z:
			{
				bool snap = false;
				Vector3 value = this.AxisMove(eventData.delta, ref snap);
				foreach (GuideObject guideObject in Singleton<GuideObjectManager>.Instance.selectObjects)
				{
					guideObject.MoveLocal(value, snap, this.axis);
				}
				break;
			}
			case GuideMove.MoveAxis.XYZ:
			{
				Vector3 value2 = this.WorldPos(eventData.position) - this.WorldPos(this.oldPos);
				foreach (GuideObject guideObject2 in Singleton<GuideObjectManager>.Instance.selectObjects)
				{
					guideObject2.MoveWorld(value2);
				}
				break;
			}
			case GuideMove.MoveAxis.XY:
			case GuideMove.MoveAxis.YZ:
			case GuideMove.MoveAxis.XZ:
			{
				Vector3 value3 = this.PlanePos(eventData.position) - this.PlanePos(this.oldPos);
				foreach (GuideObject guideObject3 in Singleton<GuideObjectManager>.Instance.selectObjects)
				{
					guideObject3.MoveWorld(value3);
				}
				break;
			}
			}
			this.oldPos = eventData.position;
			if (this.onDragAction != null)
			{
				this.onDragAction();
			}
		}

		// Token: 0x06009A64 RID: 39524 RVA: 0x003F66AC File Offset: 0x003F4AAC
		public void OnPointerUp(PointerEventData eventData)
		{
		}

		// Token: 0x06009A65 RID: 39525 RVA: 0x003F66B0 File Offset: 0x003F4AB0
		public override void OnEndDrag(PointerEventData eventData)
		{
			base.OnEndDrag(eventData);
			GuideCommand.EqualsInfo[] changeAmountInfo = (from v in Singleton<GuideObjectManager>.Instance.selectObjectDictionary
			select new GuideCommand.EqualsInfo
			{
				dicKey = v.Key,
				oldValue = this.dicOld[v.Key],
				newValue = v.Value.pos
			}).ToArray<GuideCommand.EqualsInfo>();
			Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.MoveEqualsCommand(changeAmountInfo));
			if (this.onEndDragAction != null)
			{
				this.onEndDragAction();
			}
		}

		// Token: 0x06009A66 RID: 39526 RVA: 0x003F670C File Offset: 0x003F4B0C
		private Vector3 WorldPos(Vector2 _screenPos)
		{
			Plane plane = new Plane(this.camera.transform.forward * -1f, base.transform.position);
			Ray ray = RectTransformUtility.ScreenPointToRay(this.camera, _screenPos);
			float distance = 0f;
			return (!plane.Raycast(ray, out distance)) ? base.transform.position : ray.GetPoint(distance);
		}

		// Token: 0x06009A67 RID: 39527 RVA: 0x003F6780 File Offset: 0x003F4B80
		private Vector3 PlanePos(Vector2 _screenPos)
		{
			Plane plane = new Plane(base.transform.up, base.transform.position);
			Ray ray = RectTransformUtility.ScreenPointToRay(Camera.main, _screenPos);
			float distance = 0f;
			return (!plane.Raycast(ray, out distance)) ? base.transform.position : ray.GetPoint(distance);
		}

		// Token: 0x06009A68 RID: 39528 RVA: 0x003F67E4 File Offset: 0x003F4BE4
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

		// Token: 0x06009A69 RID: 39529 RVA: 0x003F6894 File Offset: 0x003F4C94
		private Vector3 AxisMove(Vector2 _delta, ref bool _snap)
		{
			Vector3 vector = this.camera.transform.TransformVector(_delta.x * 0.005f, _delta.y * 0.005f, 0f);
			if (Input.GetKey(KeyCode.V) && vector[(int)this.axis] != 0f)
			{
				float num = (vector[(int)this.axis] >= 0f) ? 1f : -1f;
				vector = Vector3.zero;
				if (this.isSnap)
				{
					int num2 = Mathf.Clamp(Studio.optionSystem.snap, 0, 2);
					float[] array = new float[]
					{
						0.01f,
						0.1f,
						1f
					};
					vector[(int)this.axis] = array[num2] * num;
					this.isSnap = false;
					Observable.Timer(TimeSpan.FromMilliseconds(50.0)).Subscribe(delegate(long _)
					{
						this.isSnap = true;
					}).AddTo(this);
					_snap = true;
				}
			}
			else
			{
				vector *= Studio.optionSystem.manipuleteSpeed;
			}
			GuideMove.MoveAxis moveAxis = this.axis;
			if (moveAxis != GuideMove.MoveAxis.X)
			{
				if (moveAxis != GuideMove.MoveAxis.Y)
				{
					if (moveAxis == GuideMove.MoveAxis.Z)
					{
						if (this.moveCalc == GuideMove.MoveCalc.TYPE3)
						{
							vector = this.transformRoot.TransformVector(Vector3.Scale(Vector3.forward, this.transformRoot.InverseTransformVector(vector)));
						}
						else
						{
							vector = Vector3.Scale(this.transformRoot.forward, vector);
						}
					}
				}
				else if (this.moveCalc == GuideMove.MoveCalc.TYPE3)
				{
					vector = this.transformRoot.TransformVector(Vector3.Scale(Vector3.up, this.transformRoot.InverseTransformVector(vector)));
				}
				else
				{
					vector = Vector3.Scale(this.transformRoot.up, vector);
				}
			}
			else if (this.moveCalc == GuideMove.MoveCalc.TYPE3)
			{
				vector = this.transformRoot.TransformVector(Vector3.Scale(Vector3.right, this.transformRoot.InverseTransformVector(vector)));
			}
			else
			{
				vector = Vector3.Scale(this.transformRoot.right, vector);
			}
			return vector;
		}

		// Token: 0x06009A6A RID: 39530 RVA: 0x003F6AB0 File Offset: 0x003F4EB0
		private Vector3 Parse(Vector3 _src)
		{
			string format = string.Format("F{0}", 2 - Studio.optionSystem.snap);
			_src[(int)this.axis] = float.Parse(_src[(int)this.axis].ToString(format));
			return _src;
		}

		// Token: 0x06009A6B RID: 39531 RVA: 0x003F6B04 File Offset: 0x003F4F04
		private void CalcType1(KeyValuePair<ChangeAmount, Transform> _pair, Vector3 _move, bool _snap)
		{
			Vector3 vector = _pair.Key.pos;
			vector += _move;
			_pair.Key.pos = ((!_snap) ? vector : this.Parse(vector));
		}

		// Token: 0x06009A6C RID: 39532 RVA: 0x003F6B48 File Offset: 0x003F4F48
		private void CalcType2(KeyValuePair<ChangeAmount, Transform> _pair, Vector3 _move, bool _snap)
		{
			Vector3 vector = _pair.Key.pos;
			vector += _pair.Value.InverseTransformVector(_move);
			_pair.Key.pos = ((!_snap) ? vector : this.Parse(vector));
		}

		// Token: 0x04007B28 RID: 31528
		public GuideMove.MoveAxis axis;

		// Token: 0x04007B29 RID: 31529
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007B2A RID: 31530
		public GuideMove.MoveCalc moveCalc;

		// Token: 0x04007B2B RID: 31531
		public Action onDragAction;

		// Token: 0x04007B2C RID: 31532
		public Action onEndDragAction;

		// Token: 0x04007B2D RID: 31533
		private Vector2 oldPos = Vector2.zero;

		// Token: 0x04007B2E RID: 31534
		private Camera m_Camera;

		// Token: 0x04007B2F RID: 31535
		private Dictionary<int, Vector3> dicOld;

		// Token: 0x04007B30 RID: 31536
		private bool isSnap = true;

		// Token: 0x0200124C RID: 4684
		public enum MoveAxis
		{
			// Token: 0x04007B34 RID: 31540
			X,
			// Token: 0x04007B35 RID: 31541
			Y,
			// Token: 0x04007B36 RID: 31542
			Z,
			// Token: 0x04007B37 RID: 31543
			XYZ,
			// Token: 0x04007B38 RID: 31544
			XY,
			// Token: 0x04007B39 RID: 31545
			YZ,
			// Token: 0x04007B3A RID: 31546
			XZ
		}

		// Token: 0x0200124D RID: 4685
		public enum MoveCalc
		{
			// Token: 0x04007B3C RID: 31548
			TYPE1,
			// Token: 0x04007B3D RID: 31549
			TYPE2,
			// Token: 0x04007B3E RID: 31550
			TYPE3
		}
	}
}
