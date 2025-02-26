using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Illusion.Component.UI.ColorPicker
{
	// Token: 0x0200104F RID: 4175
	public class Info : MouseButtonCheck
	{
		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06008C60 RID: 35936 RVA: 0x003AE6C4 File Offset: 0x003ACAC4
		// (set) Token: 0x06008C61 RID: 35937 RVA: 0x003AE6CC File Offset: 0x003ACACC
		public bool isOn { get; private set; }

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06008C62 RID: 35938 RVA: 0x003AE6D5 File Offset: 0x003ACAD5
		// (set) Token: 0x06008C63 RID: 35939 RVA: 0x003AE6DD File Offset: 0x003ACADD
		public Vector2 imagePos { get; private set; }

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06008C64 RID: 35940 RVA: 0x003AE6E6 File Offset: 0x003ACAE6
		// (set) Token: 0x06008C65 RID: 35941 RVA: 0x003AE6EE File Offset: 0x003ACAEE
		public Vector2 imageRate { get; private set; }

		// Token: 0x06008C66 RID: 35942 RVA: 0x003AE6F8 File Offset: 0x003ACAF8
		private void Start()
		{
			if (this.canvas == null)
			{
				this.canvas = Info.SearchCanvas(base.transform);
			}
			if (this.canvas == null)
			{
				return;
			}
			this.myRt = base.GetComponent<RectTransform>();
			this.onPointerDown.AddListener(delegate(PointerEventData data)
			{
				this.isOn = true;
				this.SetImagePosition(data.position);
			});
			this.onPointerUp.AddListener(delegate(PointerEventData data)
			{
				this.isOn = false;
				this.SetImagePosition(data.position);
			});
			this.onBeginDrag.AddListener(delegate(PointerEventData data)
			{
				this.SetImagePosition(data.position);
			});
			this.onDrag.AddListener(delegate(PointerEventData data)
			{
				this.SetImagePosition(data.position);
			});
			this.onEndDrag.AddListener(delegate(PointerEventData data)
			{
				this.SetImagePosition(data.position);
			});
		}

		// Token: 0x06008C67 RID: 35943 RVA: 0x003AE7B8 File Offset: 0x003ACBB8
		private static Canvas SearchCanvas(Transform transform)
		{
			Transform transform2 = transform;
			Canvas component;
			for (;;)
			{
				component = transform2.GetComponent<Canvas>();
				if (component != null)
				{
					break;
				}
				transform2 = transform2.parent;
				if (!(transform2 != null))
				{
					goto Block_2;
				}
			}
			return component;
			Block_2:
			return null;
		}

		// Token: 0x06008C68 RID: 35944 RVA: 0x003AE7F0 File Offset: 0x003ACBF0
		private void SetImagePosition(Vector2 cursorPos)
		{
			Vector2 zero = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.myRt, cursorPos, this.canvas.worldCamera, out zero);
			RectTransform rectTransform = this.myRt;
			Vector2 vector = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
			float value = zero.x / rectTransform.localScale.x;
			float value2 = zero.y / rectTransform.localScale.y;
			this.imagePos = new Vector2(Mathf.Clamp(value, 0f, vector.x), Mathf.Clamp(value2, 0f, vector.y));
			this.imageRate = new Vector2(Mathf.InverseLerp(0f, vector.x, value), Mathf.InverseLerp(0f, vector.y, value2));
		}

		// Token: 0x04007258 RID: 29272
		[SerializeField]
		private Canvas canvas;

		// Token: 0x04007259 RID: 29273
		private RectTransform myRt;
	}
}
