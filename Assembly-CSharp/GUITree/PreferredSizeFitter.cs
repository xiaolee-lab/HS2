using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUITree
{
	// Token: 0x02001243 RID: 4675
	[AddComponentMenu("GUITree/Preferred Size Fitter", 1001)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class PreferredSizeFitter : UIBehaviour, ITreeLayoutElement, ILayoutSelfController, ILayoutElement, ILayoutController
	{
		// Token: 0x060099A9 RID: 39337 RVA: 0x003F408D File Offset: 0x003F248D
		protected PreferredSizeFitter()
		{
		}

		// Token: 0x060099AA RID: 39338 RVA: 0x003F40AB File Offset: 0x003F24AB
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060099AB RID: 39339 RVA: 0x003F40AD File Offset: 0x003F24AD
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x060099AC RID: 39340 RVA: 0x003F40AF File Offset: 0x003F24AF
		public virtual float minWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x060099AD RID: 39341 RVA: 0x003F40B7 File Offset: 0x003F24B7
		public virtual float minHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x060099AE RID: 39342 RVA: 0x003F40BF File Offset: 0x003F24BF
		// (set) Token: 0x060099AF RID: 39343 RVA: 0x003F40C7 File Offset: 0x003F24C7
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x060099B0 RID: 39344 RVA: 0x003F40E0 File Offset: 0x003F24E0
		// (set) Token: 0x060099B1 RID: 39345 RVA: 0x003F40E8 File Offset: 0x003F24E8
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x060099B2 RID: 39346 RVA: 0x003F4101 File Offset: 0x003F2501
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x060099B3 RID: 39347 RVA: 0x003F4109 File Offset: 0x003F2509
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
		}

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x060099B4 RID: 39348 RVA: 0x003F4111 File Offset: 0x003F2511
		public virtual int layoutPriority
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x060099B5 RID: 39349 RVA: 0x003F4118 File Offset: 0x003F2518
		private RectTransform rectTransform
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

		// Token: 0x060099B6 RID: 39350 RVA: 0x003F413D File Offset: 0x003F253D
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060099B7 RID: 39351 RVA: 0x003F414B File Offset: 0x003F254B
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			if (this.rectTransform != null)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			}
			base.OnDisable();
		}

		// Token: 0x060099B8 RID: 39352 RVA: 0x003F417A File Offset: 0x003F257A
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x060099B9 RID: 39353 RVA: 0x003F4182 File Offset: 0x003F2582
		private void HandleSelfFittingAlongAxis(int axis)
		{
			this.m_Tracker.Add(this, this.rectTransform, (axis != 0) ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this, axis));
		}

		// Token: 0x060099BA RID: 39354 RVA: 0x003F41BE File Offset: 0x003F25BE
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x060099BB RID: 39355 RVA: 0x003F41D2 File Offset: 0x003F25D2
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x060099BC RID: 39356 RVA: 0x003F41DB File Offset: 0x003F25DB
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

		// Token: 0x04007AE8 RID: 31464
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x04007AE9 RID: 31465
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x04007AEA RID: 31466
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04007AEB RID: 31467
		private DrivenRectTransformTracker m_Tracker;
	}
}
