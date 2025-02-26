using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GUITree
{
	// Token: 0x02001244 RID: 4676
	[AddComponentMenu("GUITree/Text Size Fitter", 1002)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class TextSizeFitter : UIBehaviour, ITreeLayoutElement, ILayoutGroup, ILayoutElement, ILayoutController
	{
		// Token: 0x060099BD RID: 39357 RVA: 0x003F4208 File Offset: 0x003F2608
		protected TextSizeFitter()
		{
			if (this.m_Padding == null)
			{
				this.m_Padding = new RectOffset();
			}
		}

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x060099BE RID: 39358 RVA: 0x003F426B File Offset: 0x003F266B
		// (set) Token: 0x060099BF RID: 39359 RVA: 0x003F4273 File Offset: 0x003F2673
		public RectOffset padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.SetProperty<RectOffset>(ref this.m_Padding, value);
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x060099C0 RID: 39360 RVA: 0x003F4282 File Offset: 0x003F2682
		// (set) Token: 0x060099C1 RID: 39361 RVA: 0x003F428A File Offset: 0x003F268A
		public TextAnchor childAlignment
		{
			get
			{
				return this.m_ChildAlignment;
			}
			set
			{
				this.SetProperty<TextAnchor>(ref this.m_ChildAlignment, value);
			}
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x060099C2 RID: 39362 RVA: 0x003F4299 File Offset: 0x003F2699
		protected RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x060099C3 RID: 39363 RVA: 0x003F42BE File Offset: 0x003F26BE
		protected Text text
		{
			get
			{
				if (this.m_Text == null)
				{
					this.m_Text = base.GetComponentInChildren<Text>();
				}
				return this.m_Text;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x060099C4 RID: 39364 RVA: 0x003F42E3 File Offset: 0x003F26E3
		protected RectTransform rectText
		{
			get
			{
				if (this.m_RectText == null && this.text != null)
				{
					this.m_RectText = this.text.rectTransform;
				}
				return this.m_RectText;
			}
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x060099C5 RID: 39365 RVA: 0x003F431E File Offset: 0x003F271E
		protected ILayoutElement elementText
		{
			get
			{
				if (this.m_ElementText == null && this.text != null)
				{
					this.m_ElementText = this.text.GetComponent<ILayoutElement>();
				}
				return this.m_ElementText;
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x060099C6 RID: 39366 RVA: 0x003F4353 File Offset: 0x003F2753
		// (set) Token: 0x060099C7 RID: 39367 RVA: 0x003F435B File Offset: 0x003F275B
		private bool isContentSizeFitter { get; set; }

		// Token: 0x060099C8 RID: 39368 RVA: 0x003F4364 File Offset: 0x003F2764
		public void CalculateLayoutInputHorizontal()
		{
			this.m_Tracker.Clear();
			ContentSizeFitter component = this.rectTransform.GetComponent<ContentSizeFitter>();
			this.isContentSizeFitter = (component != null);
			if (this.isContentSizeFitter)
			{
				this.m_FitModeHorizontal = component.horizontalFit;
				this.m_FitModeVertical = component.verticalFit;
			}
			this.CalcAlongAxis(0);
		}

		// Token: 0x060099C9 RID: 39369 RVA: 0x003F43BF File Offset: 0x003F27BF
		public void CalculateLayoutInputVertical()
		{
			this.CalcAlongAxis(1);
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x060099CA RID: 39370 RVA: 0x003F43C8 File Offset: 0x003F27C8
		public virtual float minWidth
		{
			get
			{
				return this.GetTotalMinSize(0);
			}
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x060099CB RID: 39371 RVA: 0x003F43D1 File Offset: 0x003F27D1
		public virtual float preferredWidth
		{
			get
			{
				return this.GetTotalPreferredSize(0);
			}
		}

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x060099CC RID: 39372 RVA: 0x003F43DA File Offset: 0x003F27DA
		public virtual float flexibleWidth
		{
			get
			{
				return this.GetTotalFlexibleSize(0);
			}
		}

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x060099CD RID: 39373 RVA: 0x003F43E3 File Offset: 0x003F27E3
		public virtual float minHeight
		{
			get
			{
				return this.GetTotalMinSize(1);
			}
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x060099CE RID: 39374 RVA: 0x003F43EC File Offset: 0x003F27EC
		public virtual float preferredHeight
		{
			get
			{
				return this.GetTotalPreferredSize(1);
			}
		}

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x060099CF RID: 39375 RVA: 0x003F43F5 File Offset: 0x003F27F5
		public virtual float flexibleHeight
		{
			get
			{
				return this.GetTotalFlexibleSize(1);
			}
		}

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x060099D0 RID: 39376 RVA: 0x003F43FE File Offset: 0x003F27FE
		public virtual int layoutPriority
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x060099D1 RID: 39377 RVA: 0x003F4405 File Offset: 0x003F2805
		public void SetLayoutHorizontal()
		{
			this.SetChildrenAlongAxis(0);
		}

		// Token: 0x060099D2 RID: 39378 RVA: 0x003F440E File Offset: 0x003F280E
		public void SetLayoutVertical()
		{
			this.SetChildrenAlongAxis(1);
		}

		// Token: 0x060099D3 RID: 39379 RVA: 0x003F4417 File Offset: 0x003F2817
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060099D4 RID: 39380 RVA: 0x003F4425 File Offset: 0x003F2825
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			if (this.rectTransform != null)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			}
			base.OnDisable();
		}

		// Token: 0x060099D5 RID: 39381 RVA: 0x003F4454 File Offset: 0x003F2854
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x060099D6 RID: 39382 RVA: 0x003F445C File Offset: 0x003F285C
		// (set) Token: 0x060099D7 RID: 39383 RVA: 0x003F4464 File Offset: 0x003F2864
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				this.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x060099D8 RID: 39384 RVA: 0x003F4473 File Offset: 0x003F2873
		// (set) Token: 0x060099D9 RID: 39385 RVA: 0x003F447B File Offset: 0x003F287B
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				this.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x060099DA RID: 39386 RVA: 0x003F448C File Offset: 0x003F288C
		protected void CalcAlongAxis(int axis)
		{
			float num = (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal);
			float num2 = num;
			float num3 = num;
			float num4 = 0f;
			bool flag = axis == 1;
			float minSize = LayoutUtility.GetMinSize(this.elementText, axis);
			float preferredSize = LayoutUtility.GetPreferredSize(this.elementText, axis);
			float num5 = LayoutUtility.GetFlexibleSize(this.elementText, axis);
			if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
			{
				num5 = Mathf.Max(num5, 1f);
			}
			if (flag)
			{
				num2 = Mathf.Max(minSize + num, num2);
				num3 = Mathf.Max(preferredSize + num, num3);
				num4 = Mathf.Max(num5, num4);
			}
			else
			{
				num2 += minSize;
				num3 += preferredSize;
				num4 += num5;
			}
			num3 = Mathf.Max(num2, num3);
			this.SetLayoutInputForAxis(num2, num3, num4, axis);
		}

		// Token: 0x060099DB RID: 39387 RVA: 0x003F456C File Offset: 0x003F296C
		protected void SetChildrenAlongAxis(int axis)
		{
			float num = this.rectTransform.rect.size[axis];
			bool flag = axis == 1;
			if (flag)
			{
				float value = num - (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal);
				float minSize = LayoutUtility.GetMinSize(this.elementText, axis);
				float preferredSize = LayoutUtility.GetPreferredSize(this.elementText, axis);
				float num2 = LayoutUtility.GetFlexibleSize(this.elementText, axis);
				if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
				{
					num2 = Mathf.Max(num2, 1f);
				}
				float num3 = Mathf.Clamp(value, minSize, (num2 <= 0f) ? preferredSize : num);
				float startOffset = this.GetStartOffset(axis, num3);
				this.SetChildAlongAxis(this.rectText, axis, startOffset, num3);
			}
			else
			{
				float num4 = (float)((axis != 0) ? this.padding.top : this.padding.left);
				if (this.GetTotalFlexibleSize(axis) == 0f && this.GetTotalPreferredSize(axis) < num)
				{
					num4 = this.GetStartOffset(axis, this.GetTotalPreferredSize(axis) - (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal));
				}
				float t = 0f;
				if (this.GetTotalMinSize(axis) != this.GetTotalPreferredSize(axis))
				{
					t = Mathf.Clamp01((num - this.GetTotalMinSize(axis)) / (this.GetTotalPreferredSize(axis) - this.GetTotalMinSize(axis)));
				}
				float num5 = 0f;
				if (num > this.GetTotalPreferredSize(axis) && this.GetTotalFlexibleSize(axis) > 0f)
				{
					num5 = (num - this.GetTotalPreferredSize(axis)) / this.GetTotalFlexibleSize(axis);
				}
				float minSize2 = LayoutUtility.GetMinSize(this.elementText, axis);
				float preferredSize2 = LayoutUtility.GetPreferredSize(this.elementText, axis);
				float num6 = LayoutUtility.GetFlexibleSize(this.elementText, axis);
				if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
				{
					num6 = Mathf.Max(num6, 1f);
				}
				float num7 = Mathf.Lerp(minSize2, preferredSize2, t);
				num7 += num6 * num5;
				this.SetChildAlongAxis(this.rectText, axis, num4, num7);
				num4 += num7;
			}
		}

		// Token: 0x060099DC RID: 39388 RVA: 0x003F47C5 File Offset: 0x003F2BC5
		protected float GetTotalMinSize(int axis)
		{
			return this.m_TotalMinSize[axis];
		}

		// Token: 0x060099DD RID: 39389 RVA: 0x003F47D4 File Offset: 0x003F2BD4
		protected float GetTotalPreferredSize(int axis)
		{
			return (this.isContentSizeFitter && !(this.isContentSizeFitter & ((axis != 0) ? this.m_FitModeVertical : this.m_FitModeHorizontal) == ContentSizeFitter.FitMode.PreferredSize)) ? this.rectTransform.sizeDelta[axis] : this.m_TotalPreferredSize[axis];
		}

		// Token: 0x060099DE RID: 39390 RVA: 0x003F4837 File Offset: 0x003F2C37
		protected float GetTotalFlexibleSize(int axis)
		{
			return this.m_TotalFlexibleSize[axis];
		}

		// Token: 0x060099DF RID: 39391 RVA: 0x003F4848 File Offset: 0x003F2C48
		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float num = requiredSpaceWithoutPadding + (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal);
			float num2 = this.rectTransform.rect.size[axis];
			float num3 = num2 - num;
			float num4;
			if (axis == 0)
			{
				num4 = (float)(this.childAlignment % TextAnchor.MiddleLeft) * 0.5f;
			}
			else
			{
				num4 = (float)(this.childAlignment / TextAnchor.MiddleLeft) * 0.5f;
			}
			return (float)((axis != 0) ? this.padding.top : this.padding.left) + num3 * num4;
		}

		// Token: 0x060099E0 RID: 39392 RVA: 0x003F48F8 File Offset: 0x003F2CF8
		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			this.m_TotalMinSize[axis] = totalMin;
			this.m_TotalPreferredSize[axis] = totalPreferred;
			this.m_TotalFlexibleSize[axis] = totalFlexible;
		}

		// Token: 0x060099E1 RID: 39393 RVA: 0x003F4924 File Offset: 0x003F2D24
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
			rect.SetInsetAndSizeFromParentEdge((axis != 0) ? RectTransform.Edge.Top : RectTransform.Edge.Left, pos, size);
		}

		// Token: 0x060099E2 RID: 39394 RVA: 0x003F495B File Offset: 0x003F2D5B
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
		}

		// Token: 0x060099E3 RID: 39395 RVA: 0x003F4963 File Offset: 0x003F2D63
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x060099E4 RID: 39396 RVA: 0x003F496C File Offset: 0x003F2D6C
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x003F49C9 File Offset: 0x003F2DC9
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.rectTransform != null)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			}
		}

		// Token: 0x04007AEC RID: 31468
		[SerializeField]
		protected RectOffset m_Padding = new RectOffset();

		// Token: 0x04007AED RID: 31469
		[FormerlySerializedAs("m_Alignment")]
		[SerializeField]
		protected TextAnchor m_ChildAlignment;

		// Token: 0x04007AEE RID: 31470
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04007AEF RID: 31471
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x04007AF0 RID: 31472
		private Vector2 m_TotalMinSize = Vector2.zero;

		// Token: 0x04007AF1 RID: 31473
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x04007AF2 RID: 31474
		private Vector2 m_TotalFlexibleSize = Vector2.zero;

		// Token: 0x04007AF3 RID: 31475
		private Text m_Text;

		// Token: 0x04007AF4 RID: 31476
		private RectTransform m_RectText;

		// Token: 0x04007AF5 RID: 31477
		private ILayoutElement m_ElementText;

		// Token: 0x04007AF7 RID: 31479
		private ContentSizeFitter.FitMode m_FitModeHorizontal;

		// Token: 0x04007AF8 RID: 31480
		private ContentSizeFitter.FitMode m_FitModeVertical;

		// Token: 0x04007AF9 RID: 31481
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x04007AFA RID: 31482
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;
	}
}
