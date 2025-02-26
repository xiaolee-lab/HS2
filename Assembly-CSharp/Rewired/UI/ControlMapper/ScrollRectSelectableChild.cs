using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000553 RID: 1363
	[AddComponentMenu("")]
	[RequireComponent(typeof(Selectable))]
	public class ScrollRectSelectableChild : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x000AA48F File Offset: 0x000A888F
		private RectTransform parentScrollRectContentTransform
		{
			get
			{
				return this.parentScrollRect.content;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x000AA49C File Offset: 0x000A889C
		private Selectable selectable
		{
			get
			{
				Selectable result;
				if ((result = this._selectable) == null)
				{
					result = (this._selectable = base.GetComponent<Selectable>());
				}
				return result;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x000AA4C5 File Offset: 0x000A88C5
		private RectTransform rectTransform
		{
			get
			{
				return base.transform as RectTransform;
			}
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000AA4D2 File Offset: 0x000A88D2
		private void Start()
		{
			this.parentScrollRect = base.transform.GetComponentInParent<ScrollRect>();
			if (this.parentScrollRect == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: No ScrollRect found! This component must be a child of a ScrollRect!");
				return;
			}
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000AA504 File Offset: 0x000A8904
		public void OnSelect(BaseEventData eventData)
		{
			if (this.parentScrollRect == null)
			{
				return;
			}
			if (!(eventData is AxisEventData))
			{
				return;
			}
			RectTransform rectTransform = this.parentScrollRect.transform as RectTransform;
			Rect child = MathTools.TransformRect(this.rectTransform.rect, this.rectTransform, rectTransform);
			Rect rect = rectTransform.rect;
			Rect rect2 = rectTransform.rect;
			float height;
			if (this.useCustomEdgePadding)
			{
				height = this.customEdgePadding;
			}
			else
			{
				height = child.height;
			}
			rect2.yMax -= height;
			rect2.yMin += height;
			if (MathTools.RectContains(rect2, child))
			{
				return;
			}
			Vector2 vector;
			if (!MathTools.GetOffsetToContainRect(rect2, child, out vector))
			{
				return;
			}
			Vector2 anchoredPosition = this.parentScrollRectContentTransform.anchoredPosition;
			anchoredPosition.x = Mathf.Clamp(anchoredPosition.x + vector.x, 0f, Mathf.Abs(rect.width - this.parentScrollRectContentTransform.sizeDelta.x));
			anchoredPosition.y = Mathf.Clamp(anchoredPosition.y + vector.y, 0f, Mathf.Abs(rect.height - this.parentScrollRectContentTransform.sizeDelta.y));
			this.parentScrollRectContentTransform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x04001DDD RID: 7645
		public bool useCustomEdgePadding;

		// Token: 0x04001DDE RID: 7646
		public float customEdgePadding = 50f;

		// Token: 0x04001DDF RID: 7647
		private ScrollRect parentScrollRect;

		// Token: 0x04001DE0 RID: 7648
		private Selectable _selectable;
	}
}
