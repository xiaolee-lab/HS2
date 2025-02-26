using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000F9C RID: 3996
	public class InfiniteTurnScroll : UIBehaviour
	{
		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x0600853F RID: 34111 RVA: 0x00374CD8 File Offset: 0x003730D8
		// (set) Token: 0x06008540 RID: 34112 RVA: 0x00374CE0 File Offset: 0x003730E0
		public InfiniteTurnScroll.DirectionType Direction
		{
			get
			{
				return this._direction;
			}
			set
			{
				this._direction = value;
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06008541 RID: 34113 RVA: 0x00374CE9 File Offset: 0x003730E9
		// (set) Token: 0x06008542 RID: 34114 RVA: 0x00374CF1 File Offset: 0x003730F1
		public OptionPositionChangeEvent OnUpdate
		{
			get
			{
				return this._onUpdate;
			}
			set
			{
				this._onUpdate = value;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06008543 RID: 34115 RVA: 0x00374CFA File Offset: 0x003730FA
		// (set) Token: 0x06008544 RID: 34116 RVA: 0x00374D02 File Offset: 0x00373102
		public LinkedList<RectTransform> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = new LinkedList<RectTransform>(value);
			}
		}

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06008545 RID: 34117 RVA: 0x00374D10 File Offset: 0x00373110
		public RectTransform RectTransform
		{
			[CompilerGenerated]
			get
			{
				RectTransform result;
				if ((result = this._rectTransform) == null)
				{
					result = (this._rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06008546 RID: 34118 RVA: 0x00374D3C File Offset: 0x0037313C
		private float AnchoredPosition
		{
			get
			{
				InfiniteTurnScroll.DirectionType direction = this._direction;
				if (direction == InfiniteTurnScroll.DirectionType.Vertical)
				{
					return -this.RectTransform.anchoredPosition.y;
				}
				if (direction != InfiniteTurnScroll.DirectionType.Horizontal)
				{
					return (this._direction != InfiniteTurnScroll.DirectionType.Vertical) ? this._rectTransform.anchoredPosition.x : (-this._rectTransform.anchoredPosition.y);
				}
				return this.RectTransform.anchoredPosition.x;
			}
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06008547 RID: 34119 RVA: 0x00374DC4 File Offset: 0x003731C4
		public float OptionSize
		{
			get
			{
				if (this._optionNode != null && this._optionSize == -1f)
				{
					this._optionSize = ((this._direction != InfiniteTurnScroll.DirectionType.Vertical) ? this._optionNode.sizeDelta.x : this._optionNode.sizeDelta.y);
				}
				return this._optionSize;
			}
		}

		// Token: 0x06008548 RID: 34120 RVA: 0x00374E34 File Offset: 0x00373234
		protected override void Start()
		{
			IInfiniteScrollControl[] components = base.GetComponents<IInfiniteScrollControl>();
			ScrollRect componentInParent = base.GetComponentInParent<ScrollRect>();
			componentInParent.horizontal = (this._direction == InfiniteTurnScroll.DirectionType.Horizontal);
			componentInParent.vertical = (this._direction == InfiniteTurnScroll.DirectionType.Vertical);
			componentInParent.content = this._rectTransform;
			this._optionNode.gameObject.SetActive(false);
			for (int i = 0; i < this._instantiateCount; i++)
			{
				RectTransform rectTransform = UnityEngine.Object.Instantiate<GameObject>(this._optionNode.gameObject).transform as RectTransform;
				rectTransform.SetParent(base.transform, false);
				rectTransform.name = string.Format("Item {0}", i.ToString("000"));
				rectTransform.anchoredPosition = ((this._direction != InfiniteTurnScroll.DirectionType.Vertical) ? new Vector2(this.OptionSize * (float)i, 0f) : new Vector2(0f, -this.OptionSize * (float)i));
				this._options.AddLast(rectTransform);
				rectTransform.gameObject.SetActive(true);
				foreach (IInfiniteScrollControl infiniteScrollControl in components)
				{
					infiniteScrollControl.OnUpdateOption(i, rectTransform.gameObject);
				}
			}
			foreach (IInfiniteScrollControl infiniteScrollControl2 in components)
			{
				infiniteScrollControl2.OnPostSetupOption();
			}
		}

		// Token: 0x04006BC6 RID: 27590
		[SerializeField]
		private RectTransform _optionNode;

		// Token: 0x04006BC7 RID: 27591
		[SerializeField]
		[Range(0f, 30f)]
		private int _instantiateCount = 10;

		// Token: 0x04006BC8 RID: 27592
		[SerializeField]
		private InfiniteTurnScroll.DirectionType _direction;

		// Token: 0x04006BC9 RID: 27593
		[SerializeField]
		private OptionPositionChangeEvent _onUpdate = new OptionPositionChangeEvent();

		// Token: 0x04006BCA RID: 27594
		private LinkedList<RectTransform> _options = new LinkedList<RectTransform>();

		// Token: 0x04006BCB RID: 27595
		protected float _diffPreFramePosition;

		// Token: 0x04006BCC RID: 27596
		protected int _currentItemNo;

		// Token: 0x04006BCD RID: 27597
		private RectTransform _rectTransform;

		// Token: 0x04006BCE RID: 27598
		private float _optionSize = -1f;

		// Token: 0x02000F9D RID: 3997
		public enum DirectionType
		{
			// Token: 0x04006BD0 RID: 27600
			Vertical,
			// Token: 0x04006BD1 RID: 27601
			Horizontal
		}
	}
}
