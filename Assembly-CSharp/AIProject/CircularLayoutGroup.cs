using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000F94 RID: 3988
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	public class CircularLayoutGroup : UIBehaviour, ILayoutGroup, IScrollHandler, ILayoutController, IEventSystemHandler
	{
		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06008511 RID: 34065 RVA: 0x00374680 File Offset: 0x00372A80
		// (set) Token: 0x06008512 RID: 34066 RVA: 0x00374688 File Offset: 0x00372A88
		public CircularLayoutGroup.Anchor ChildAlignment
		{
			get
			{
				return this._childAlignment;
			}
			set
			{
				this._childAlignment = value;
			}
		}

		// Token: 0x06008513 RID: 34067 RVA: 0x00374691 File Offset: 0x00372A91
		public int GetRowCount()
		{
			return Mathf.CeilToInt((float)base.transform.childCount / (float)this._rowCellNum - 0.001f);
		}

		// Token: 0x06008514 RID: 34068 RVA: 0x003746B2 File Offset: 0x00372AB2
		private void Update()
		{
			this.Arrange();
		}

		// Token: 0x06008515 RID: 34069 RVA: 0x003746BA File Offset: 0x00372ABA
		public void SetLayoutHorizontal()
		{
		}

		// Token: 0x06008516 RID: 34070 RVA: 0x003746BC File Offset: 0x00372ABC
		public void SetLayoutVertical()
		{
			this.Arrange();
		}

		// Token: 0x06008517 RID: 34071 RVA: 0x003746C4 File Offset: 0x00372AC4
		private void Arrange()
		{
			int childCount = base.transform.childCount;
			int num = Mathf.CeilToInt((float)childCount / (float)this._rowCellNum - 0.001f);
			for (int i = 0; i < num; i++)
			{
				float radius = this._radius + (this._cellSize.y + this._rowMargin) * (float)i;
				int num2 = childCount - this._rowCellNum * i;
				if (num2 >= this._rowCellNum)
				{
					this.ArrangeRow(i, this._rowCellNum, radius);
				}
				else
				{
					this.ArrangeRow(i, num2, radius);
				}
			}
		}

		// Token: 0x06008518 RID: 34072 RVA: 0x00374758 File Offset: 0x00372B58
		private void ArrangeRow(int r, int cellNum, float radius)
		{
			int num = (this._childAlignment != CircularLayoutGroup.Anchor.Center) ? (this._rowCellNum - 1) : ((cellNum <= 1) ? cellNum : (cellNum - 1));
			float num2 = this._overallAngle / (float)num;
			float num3 = 90f + this._startAngle + this._scrollAngle;
			float num4 = this._overallAngle / 2f;
			int num5 = this._rowCellNum * r;
			for (int i = 0; i < cellNum; i++)
			{
				RectTransform rectTransform = base.transform.GetChild(num5 + i) as RectTransform;
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._cellSize.x);
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._cellSize.y);
				float num6 = num2 * (float)i;
				if (this._childAlignment == CircularLayoutGroup.Anchor.Right)
				{
					num6 = -num6;
				}
				num6 += num3;
				if (this._childAlignment == CircularLayoutGroup.Anchor.Left)
				{
					num6 -= num4;
				}
				else if (this._childAlignment == CircularLayoutGroup.Anchor.Right)
				{
					num6 += num4;
				}
				else if (cellNum > 1)
				{
					num6 -= num4;
				}
				rectTransform.anchoredPosition = this._offset + new Vector2(Mathf.Cos(num6 * 0.017453292f), Mathf.Sin(num6 * 0.017453292f)) * radius;
			}
		}

		// Token: 0x06008519 RID: 34073 RVA: 0x003748A8 File Offset: 0x00372CA8
		public void OnScroll(PointerEventData eventData)
		{
			this._scrollAngle += eventData.scrollDelta.y;
			this.Arrange();
		}

		// Token: 0x04006BAD RID: 27565
		[SerializeField]
		private Vector2 _offset = Vector3.zero;

		// Token: 0x04006BAE RID: 27566
		[SerializeField]
		private float _radius = 100f;

		// Token: 0x04006BAF RID: 27567
		[SerializeField]
		private float _startAngle;

		// Token: 0x04006BB0 RID: 27568
		[SerializeField]
		[Range(15f, 360f)]
		private float _overallAngle = 120f;

		// Token: 0x04006BB1 RID: 27569
		[SerializeField]
		private CircularLayoutGroup.Anchor _childAlignment;

		// Token: 0x04006BB2 RID: 27570
		[SerializeField]
		private Vector2 _cellSize = new Vector2(100f, 100f);

		// Token: 0x04006BB3 RID: 27571
		[SerializeField]
		private int _rowCellNum = 1;

		// Token: 0x04006BB4 RID: 27572
		[SerializeField]
		private float _rowMargin = 10f;

		// Token: 0x04006BB5 RID: 27573
		private float _scrollAngle;

		// Token: 0x02000F95 RID: 3989
		public enum Anchor
		{
			// Token: 0x04006BB7 RID: 27575
			Left,
			// Token: 0x04006BB8 RID: 27576
			Center,
			// Token: 0x04006BB9 RID: 27577
			Right
		}
	}
}
