using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUITree
{
	// Token: 0x02001245 RID: 4677
	[AddComponentMenu("GUITree/Tree Node", 1000)]
	[RequireComponent(typeof(RectTransform))]
	public class TreeNode : UIBehaviour, ITreeLayoutElement, ILayoutSelfController, ILayoutElement, ILayoutController
	{
		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x060099E7 RID: 39399 RVA: 0x003F4A1C File Offset: 0x003F2E1C
		public virtual float minWidth
		{
			get
			{
				return this.preferredWidth;
			}
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x060099E8 RID: 39400 RVA: 0x003F4A24 File Offset: 0x003F2E24
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth + (float)this.indent * this.indentSize;
			}
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x060099E9 RID: 39401 RVA: 0x003F4A3B File Offset: 0x003F2E3B
		public virtual float flexibleWidth
		{
			get
			{
				return this.preferredWidth;
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x060099EA RID: 39402 RVA: 0x003F4A43 File Offset: 0x003F2E43
		public virtual float minHeight
		{
			get
			{
				return this.preferredHeight;
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x060099EB RID: 39403 RVA: 0x003F4A4B File Offset: 0x003F2E4B
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x060099EC RID: 39404 RVA: 0x003F4A53 File Offset: 0x003F2E53
		public virtual float flexibleHeight
		{
			get
			{
				return this.preferredHeight;
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x060099ED RID: 39405 RVA: 0x003F4A5B File Offset: 0x003F2E5B
		public int layoutPriority
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x060099EE RID: 39406 RVA: 0x003F4A62 File Offset: 0x003F2E62
		public RectTransform rectTransform
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

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x060099EF RID: 39407 RVA: 0x003F4A87 File Offset: 0x003F2E87
		// (set) Token: 0x060099F0 RID: 39408 RVA: 0x003F4A8F File Offset: 0x003F2E8F
		public int indent
		{
			get
			{
				return this.m_Indent;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_Indent, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x060099F1 RID: 39409 RVA: 0x003F4AA8 File Offset: 0x003F2EA8
		// (set) Token: 0x060099F2 RID: 39410 RVA: 0x003F4AB0 File Offset: 0x003F2EB0
		public float indentSize
		{
			get
			{
				return this.m_IndentSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_IndentSize, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x060099F3 RID: 39411 RVA: 0x003F4AC9 File Offset: 0x003F2EC9
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060099F4 RID: 39412 RVA: 0x003F4AD7 File Offset: 0x003F2ED7
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			if (this.rectTransform)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			}
			base.OnDisable();
		}

		// Token: 0x060099F5 RID: 39413 RVA: 0x003F4B08 File Offset: 0x003F2F08
		public void CalculateLayoutInputHorizontal()
		{
			int childCount = this.rectTransform.childCount;
			float num = 0f;
			for (int i = 0; i < childCount; i++)
			{
				ITreeLayoutElement component = this.rectTransform.GetChild(i).GetComponent<ITreeLayoutElement>();
				if (component != null)
				{
					num += LayoutUtility.GetPreferredSize(component, 0);
				}
			}
			this.m_PreferredWidth = num;
		}

		// Token: 0x060099F6 RID: 39414 RVA: 0x003F4B68 File Offset: 0x003F2F68
		public void CalculateLayoutInputVertical()
		{
			int childCount = this.rectTransform.childCount;
			float num = 0f;
			for (int i = 0; i < childCount; i++)
			{
				ITreeLayoutElement component = this.rectTransform.GetChild(i).GetComponent<ITreeLayoutElement>();
				if (component != null)
				{
					float preferredSize = LayoutUtility.GetPreferredSize(component, 1);
					if (num < preferredSize)
					{
						num = preferredSize;
					}
				}
			}
			this.m_PreferredHeight = num;
		}

		// Token: 0x060099F7 RID: 39415 RVA: 0x003F4BD4 File Offset: 0x003F2FD4
		public void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.SizeDeltaX);
			this.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (float)this.indent * this.indentSize, LayoutUtility.GetPreferredSize(this, 0));
		}

		// Token: 0x060099F8 RID: 39416 RVA: 0x003F4C24 File Offset: 0x003F3024
		public void SetLayoutVertical()
		{
			this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, LayoutUtility.GetPreferredSize(this, 1));
		}

		// Token: 0x060099F9 RID: 39417 RVA: 0x003F4C50 File Offset: 0x003F3050
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

		// Token: 0x04007AFB RID: 31483
		private float m_PreferredWidth = -1f;

		// Token: 0x04007AFC RID: 31484
		private float m_PreferredHeight = -1f;

		// Token: 0x04007AFD RID: 31485
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04007AFE RID: 31486
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04007AFF RID: 31487
		[SerializeField]
		private int m_Indent;

		// Token: 0x04007B00 RID: 31488
		[SerializeField]
		private float m_IndentSize = 32f;
	}
}
