using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUITree
{
	// Token: 0x02001246 RID: 4678
	[AddComponentMenu("GUITree/Tree Root", 1003)]
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class TreeRoot : UIBehaviour, ITreeLayoutElement, ILayoutGroup, ILayoutElement, ILayoutController
	{
		// Token: 0x060099FA RID: 39418 RVA: 0x003F4C7A File Offset: 0x003F307A
		protected TreeRoot()
		{
		}

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x060099FB RID: 39419 RVA: 0x003F4C98 File Offset: 0x003F3098
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

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x060099FC RID: 39420 RVA: 0x003F4CBD File Offset: 0x003F30BD
		protected List<TreeNode> treeChildren
		{
			get
			{
				return this.m_TreeLayoutElement;
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x060099FD RID: 39421 RVA: 0x003F4CC5 File Offset: 0x003F30C5
		// (set) Token: 0x060099FE RID: 39422 RVA: 0x003F4CCD File Offset: 0x003F30CD
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				this.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x060099FF RID: 39423 RVA: 0x003F4CDC File Offset: 0x003F30DC
		protected void CalcAlongAxis(int axis)
		{
			float num = 0f;
			bool flag = true ^ axis == 1;
			for (int i = 0; i < this.treeChildren.Count; i++)
			{
				TreeNode element = this.treeChildren[i];
				float preferredSize = LayoutUtility.GetPreferredSize(element, axis);
				if (flag)
				{
					num = Mathf.Max(preferredSize, num);
				}
				else
				{
					num += preferredSize + this.spacing;
				}
			}
			if (!flag && this.treeChildren.Count > 0)
			{
				num -= this.spacing;
			}
			this.m_TotalPreferredSize[axis] = num;
		}

		// Token: 0x06009A00 RID: 39424 RVA: 0x003F4D78 File Offset: 0x003F3178
		public virtual void CalculateLayoutInputHorizontal()
		{
			this.m_TreeLayoutElement.Clear();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				TreeNode component = this.rectTransform.GetChild(i).GetComponent<TreeNode>();
				if (!(component == null) && component.IsActive())
				{
					this.m_TreeLayoutElement.Add(component);
				}
			}
			this.CalcAlongAxis(0);
		}

		// Token: 0x06009A01 RID: 39425 RVA: 0x003F4DED File Offset: 0x003F31ED
		public void CalculateLayoutInputVertical()
		{
			this.CalcAlongAxis(1);
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06009A02 RID: 39426 RVA: 0x003F4DF6 File Offset: 0x003F31F6
		public virtual float minWidth
		{
			get
			{
				return this.preferredWidth;
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06009A03 RID: 39427 RVA: 0x003F4DFE File Offset: 0x003F31FE
		public virtual float preferredWidth
		{
			get
			{
				return this.m_TotalPreferredSize[0];
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06009A04 RID: 39428 RVA: 0x003F4E0C File Offset: 0x003F320C
		public virtual float flexibleWidth
		{
			get
			{
				return this.preferredWidth;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06009A05 RID: 39429 RVA: 0x003F4E14 File Offset: 0x003F3214
		public virtual float minHeight
		{
			get
			{
				return this.preferredHeight;
			}
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06009A06 RID: 39430 RVA: 0x003F4E1C File Offset: 0x003F321C
		public virtual float preferredHeight
		{
			get
			{
				return this.m_TotalPreferredSize[1];
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x06009A07 RID: 39431 RVA: 0x003F4E2A File Offset: 0x003F322A
		public virtual float flexibleHeight
		{
			get
			{
				return this.preferredHeight;
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06009A08 RID: 39432 RVA: 0x003F4E32 File Offset: 0x003F3232
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06009A09 RID: 39433 RVA: 0x003F4E35 File Offset: 0x003F3235
		public void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
		}

		// Token: 0x06009A0A RID: 39434 RVA: 0x003F4E44 File Offset: 0x003F3244
		public void SetLayoutVertical()
		{
			float num = 0f;
			for (int i = 0; i < this.treeChildren.Count; i++)
			{
				TreeNode treeNode = this.treeChildren[i];
				float preferredSize = LayoutUtility.GetPreferredSize(treeNode, 1);
				this.m_Tracker.Add(this, treeNode.rectTransform, DrivenTransformProperties.AnchoredPositionY);
				treeNode.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, num, preferredSize);
				num += preferredSize + this.spacing;
			}
		}

		// Token: 0x06009A0B RID: 39435 RVA: 0x003F4EB4 File Offset: 0x003F32B4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06009A0C RID: 39436 RVA: 0x003F4EC2 File Offset: 0x003F32C2
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			if (this.rectTransform != null)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			}
			base.OnDisable();
		}

		// Token: 0x06009A0D RID: 39437 RVA: 0x003F4EF1 File Offset: 0x003F32F1
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06009A0E RID: 39438 RVA: 0x003F4EFC File Offset: 0x003F32FC
		private bool isRootLayoutGroup
		{
			get
			{
				Transform parent = base.transform.parent;
				return parent == null || base.transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		// Token: 0x06009A0F RID: 39439 RVA: 0x003F4F43 File Offset: 0x003F3343
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.isRootLayoutGroup)
			{
				this.SetDirty();
			}
		}

		// Token: 0x06009A10 RID: 39440 RVA: 0x003F4F5C File Offset: 0x003F335C
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06009A11 RID: 39441 RVA: 0x003F4F64 File Offset: 0x003F3364
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x06009A12 RID: 39442 RVA: 0x003F4FC1 File Offset: 0x003F33C1
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

		// Token: 0x04007B01 RID: 31489
		private RectTransform m_Rect;

		// Token: 0x04007B02 RID: 31490
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x04007B03 RID: 31491
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x04007B04 RID: 31492
		private List<TreeNode> m_TreeLayoutElement = new List<TreeNode>();

		// Token: 0x04007B05 RID: 31493
		[SerializeField]
		protected float m_Spacing;
	}
}
