using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Housing.Command;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Housing
{
	// Token: 0x020008C0 RID: 2240
	public class GuideMove : GuideBase, IInitializePotentialDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003A8B RID: 14987 RVA: 0x001568C4 File Offset: 0x00154CC4
		private Camera camera
		{
			[CompilerGenerated]
			get
			{
				Camera result;
				if ((result = this.m_Camera) == null)
				{
					result = (this.m_Camera = Camera.main);
				}
				return result;
			}
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x001568EC File Offset: 0x00154CEC
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00156904 File Offset: 0x00154D04
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (!this.isDragButton)
			{
				this.isDragButton = (eventData.button == PointerEventData.InputButton.Left);
			}
			if (!this.isDragButton)
			{
				eventData.dragging = false;
				return;
			}
			this.oldPos = eventData.pressPosition;
			this.dicTargetAndOld = (from v in Singleton<Selection>.Instance.SelectObjects
			where v is OCItem
			select v).ToDictionary((ObjectCtrl v) => v, (ObjectCtrl v) => new GuideMove.PosInfo(v.Position));
			this.isLocal = (Singleton<GuideManager>.IsInstance() && Singleton<GuideManager>.Instance.TransformRoot);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x001569E0 File Offset: 0x00154DE0
		public override void OnDrag(PointerEventData eventData)
		{
			if (!this.isDragButton || eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			base.OnDrag(eventData);
			GuideMove.MoveAxis moveAxis = this.axis;
			if (moveAxis == GuideMove.MoveAxis.X || moveAxis == GuideMove.MoveAxis.Y || moveAxis == GuideMove.MoveAxis.Z)
			{
				Vector3 vector = this.WorldPos(eventData.position) - this.WorldPos(this.oldPos);
				vector = ((!this.isLocal) ? this.AxisMove(vector) : this.AxisLocalMove(vector));
				bool flag = false;
				foreach (KeyValuePair<ObjectCtrl, GuideMove.PosInfo> keyValuePair in this.dicTargetAndOld)
				{
					keyValuePair.Value.work = keyValuePair.Key.Position;
					Vector3 vector2 = keyValuePair.Value.old + vector;
					if (Singleton<GuideManager>.IsInstance())
					{
						if (Singleton<GuideManager>.Instance.CorrectPos(keyValuePair.Key, ref vector2))
						{
							keyValuePair.Value.sub = this.InverseTransformVector(keyValuePair.Value.old + vector - vector2);
							keyValuePair.Value.IsHit = true;
							flag = true;
						}
						else
						{
							keyValuePair.Value.IsHit = false;
						}
					}
					keyValuePair.Key.Position = vector2;
				}
				if (flag)
				{
					IEnumerable<Vector3> enumerable = from v in this.dicTargetAndOld
					where v.Value.IsHit
					select v.Value.sub;
					if (vector[(int)this.axis] > 0f)
					{
						Vector3 vector3 = enumerable.First<Vector3>();
						foreach (Vector3 rhs in enumerable)
						{
							vector3 = Vector3.Max(vector3, rhs);
						}
						vector -= this.TransformVector(vector3);
					}
					else
					{
						Vector3 vector4 = enumerable.First<Vector3>();
						foreach (Vector3 rhs2 in enumerable)
						{
							vector4 = Vector3.Min(vector4, rhs2);
						}
						vector -= this.TransformVector(vector4);
					}
					foreach (KeyValuePair<ObjectCtrl, GuideMove.PosInfo> keyValuePair2 in this.dicTargetAndOld)
					{
						Vector3 position = keyValuePair2.Value.old + vector;
						if (Singleton<GuideManager>.IsInstance())
						{
							Singleton<GuideManager>.Instance.CorrectPos(keyValuePair2.Key, ref position);
						}
						keyValuePair2.Key.Position = position;
					}
				}
			}
			if (this.onDragAction != null)
			{
				this.onDragAction();
			}
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x00156D38 File Offset: 0x00155138
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (this.isDragButton && eventData.dragging)
			{
				return;
			}
			base.OnPointerUp(eventData);
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x00156D58 File Offset: 0x00155158
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (!this.isDragButton)
			{
				eventData.dragging = false;
				base.OnEndDrag(eventData);
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			base.OnEndDrag(eventData);
			if (Singleton<UndoRedoManager>.IsInstance())
			{
				Singleton<UndoRedoManager>.Instance.Push(new MoveCommand((from v in this.dicTargetAndOld
				select new MoveCommand.Info(v.Key, v.Value.old)).ToArray<MoveCommand.Info>()));
			}
			foreach (KeyValuePair<ObjectCtrl, GuideMove.PosInfo> keyValuePair in this.dicTargetAndOld)
			{
				Singleton<Housing>.Instance.CheckOverlap(keyValuePair.Key as OCItem);
			}
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
			if (this.onEndDragAction != null)
			{
				this.onEndDragAction();
			}
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x00156E64 File Offset: 0x00155264
		private Vector3 WorldPos(Vector2 _screenPos)
		{
			Plane plane = new Plane(this.camera.transform.forward * -1f, base.transform.position);
			Ray ray = RectTransformUtility.ScreenPointToRay(this.camera, _screenPos);
			float distance = 0f;
			return (!plane.Raycast(ray, out distance)) ? base.transform.position : ray.GetPoint(distance);
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x00156ED7 File Offset: 0x001552D7
		private Vector3 AxisMove(Vector3 _move)
		{
			return this.Floor(Vector3.Scale(base.transform.up, _move));
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x00156EF0 File Offset: 0x001552F0
		private Vector3 AxisLocalMove(Vector3 _move)
		{
			if (!Singleton<GuideManager>.IsInstance() || !Singleton<GuideManager>.Instance.TransformRoot)
			{
				return Vector3.zero;
			}
			Transform transform = Singleton<GuideManager>.Instance.TransformRoot;
			Vector3 a = transform.InverseTransformDirection(base.transform.up);
			Vector3 b = transform.InverseTransformVector(_move);
			return transform.TransformVector(this.Floor(Vector3.Scale(a, b)));
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x00156F59 File Offset: 0x00155359
		private Vector3 InverseTransformVector(Vector3 _move)
		{
			if (!Singleton<GuideManager>.IsInstance() || !Singleton<GuideManager>.Instance.TransformRoot)
			{
				return _move;
			}
			return Singleton<GuideManager>.Instance.TransformRoot.InverseTransformVector(_move);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x00156F8B File Offset: 0x0015538B
		private Vector3 TransformVector(Vector3 _move)
		{
			if (!Singleton<GuideManager>.IsInstance() || !Singleton<GuideManager>.Instance.TransformRoot)
			{
				return _move;
			}
			return Singleton<GuideManager>.Instance.TransformRoot.TransformVector(_move);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x00156FC0 File Offset: 0x001553C0
		private float Floor(float _value)
		{
			bool flag = _value < 0f;
			return Mathf.Floor(Mathf.Abs(_value)) * (float)((!flag) ? 1 : -1);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x00156FF0 File Offset: 0x001553F0
		private Vector3 Floor(Vector3 _value)
		{
			return new Vector3(this.Floor(_value.x), this.Floor(_value.y), this.Floor(_value.z));
		}

		// Token: 0x040039C3 RID: 14787
		public GuideMove.MoveAxis axis;

		// Token: 0x040039C4 RID: 14788
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x040039C5 RID: 14789
		public GuideMove.MoveCalc moveCalc;

		// Token: 0x040039C6 RID: 14790
		public Action onDragAction;

		// Token: 0x040039C7 RID: 14791
		public Action onEndDragAction;

		// Token: 0x040039C8 RID: 14792
		private Vector2 oldPos = Vector2.zero;

		// Token: 0x040039C9 RID: 14793
		private Camera m_Camera;

		// Token: 0x040039CA RID: 14794
		private Dictionary<ObjectCtrl, GuideMove.PosInfo> dicTargetAndOld;

		// Token: 0x040039CB RID: 14795
		private bool isLocal;

		// Token: 0x040039CC RID: 14796
		private bool isDragButton;

		// Token: 0x020008C1 RID: 2241
		public enum MoveAxis
		{
			// Token: 0x040039D4 RID: 14804
			X,
			// Token: 0x040039D5 RID: 14805
			Y,
			// Token: 0x040039D6 RID: 14806
			Z
		}

		// Token: 0x020008C2 RID: 2242
		public enum MoveCalc
		{
			// Token: 0x040039D8 RID: 14808
			TYPE1,
			// Token: 0x040039D9 RID: 14809
			TYPE2
		}

		// Token: 0x020008C3 RID: 2243
		private class PosInfo
		{
			// Token: 0x06003A9E RID: 15006 RVA: 0x0015706F File Offset: 0x0015546F
			public PosInfo(Vector3 _old)
			{
				this.old = _old;
			}

			// Token: 0x17000A93 RID: 2707
			// (get) Token: 0x06003A9F RID: 15007 RVA: 0x0015709F File Offset: 0x0015549F
			// (set) Token: 0x06003AA0 RID: 15008 RVA: 0x001570A7 File Offset: 0x001554A7
			public bool IsHit { get; set; }

			// Token: 0x040039DA RID: 14810
			public Vector3 old = Vector3.zero;

			// Token: 0x040039DB RID: 14811
			public Vector3 work = Vector3.zero;

			// Token: 0x040039DC RID: 14812
			public Vector3 sub = Vector3.zero;
		}
	}
}
