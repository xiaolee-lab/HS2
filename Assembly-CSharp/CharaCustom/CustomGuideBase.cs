using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharaCustom
{
	// Token: 0x02000A11 RID: 2577
	public class CustomGuideBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06004CAF RID: 19631 RVA: 0x001D798D File Offset: 0x001D5D8D
		public Material material
		{
			get
			{
				return (!this.renderer) ? null : this.renderer.material;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06004CB0 RID: 19632 RVA: 0x001D79B0 File Offset: 0x001D5DB0
		// (set) Token: 0x06004CB1 RID: 19633 RVA: 0x001D79B8 File Offset: 0x001D5DB8
		public bool draw
		{
			get
			{
				return this.m_Draw;
			}
			set
			{
				if (this.m_Draw != value)
				{
					this.m_Draw = value;
					if (this.renderer)
					{
						this.renderer.enabled = this.m_Draw;
					}
					if (this.collider)
					{
						this.collider.enabled = this.m_Draw;
					}
				}
			}
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x001D7A1C File Offset: 0x001D5E1C
		protected Color ConvertColor(Color _color)
		{
			_color.r *= 0.75f;
			_color.g *= 0.75f;
			_color.b *= 0.75f;
			_color.a = 0.25f;
			return _color;
		}

		// Token: 0x17000E70 RID: 3696
		// (set) Token: 0x06004CB3 RID: 19635 RVA: 0x001D7A6F File Offset: 0x001D5E6F
		protected Color colorNow
		{
			set
			{
				if (this.material)
				{
					this.material.color = value;
				}
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x001D7A8D File Offset: 0x001D5E8D
		// (set) Token: 0x06004CB5 RID: 19637 RVA: 0x001D7A95 File Offset: 0x001D5E95
		public bool isDrag { get; private set; }

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x001D7A9E File Offset: 0x001D5E9E
		// (set) Token: 0x06004CB7 RID: 19639 RVA: 0x001D7AA6 File Offset: 0x001D5EA6
		public CustomGuideObject guideObject { get; set; }

		// Token: 0x06004CB8 RID: 19640 RVA: 0x001D7AAF File Offset: 0x001D5EAF
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(this.guideObject.ccv2))
			{
				return;
			}
			this.colorNow = this.colorHighlighted;
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x001D7AD3 File Offset: 0x001D5ED3
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.isDrag)
			{
				this.colorNow = this.colorNormal;
			}
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x001D7AEC File Offset: 0x001D5EEC
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (CustomGuideAssist.IsCameraActionFlag(this.guideObject.ccv2))
			{
				return;
			}
			this.isDrag = true;
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x001D7B0B File Offset: 0x001D5F0B
		public virtual void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x001D7B0D File Offset: 0x001D5F0D
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.isDrag = false;
			this.colorNow = this.colorNormal;
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x001D7B22 File Offset: 0x001D5F22
		private void OnDisable()
		{
			this.colorNow = this.colorNormal;
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x001D7B30 File Offset: 0x001D5F30
		public virtual void Start()
		{
			this.renderer = base.gameObject.GetComponent<Renderer>();
			if (this.renderer == null)
			{
				this.renderer = base.gameObject.GetComponentInChildren<Renderer>();
			}
			this.collider = this.renderer.GetComponent<Collider>();
			this.colorNormal = this.ConvertColor(this.material.color);
			this.colorHighlighted = this.material.color;
			this.colorHighlighted.a = 0.75f;
			if (this.renderer)
			{
				this.renderer.enabled = this.m_Draw;
			}
			if (this.collider)
			{
				this.collider.enabled = this.m_Draw;
			}
			this.colorNow = this.colorNormal;
			this.isDrag = false;
		}

		// Token: 0x0400464B RID: 17995
		[SerializeField]
		protected Color colorNormal;

		// Token: 0x0400464C RID: 17996
		[SerializeField]
		protected Color colorHighlighted;

		// Token: 0x0400464D RID: 17997
		protected Renderer renderer;

		// Token: 0x0400464E RID: 17998
		protected Collider collider;

		// Token: 0x0400464F RID: 17999
		private bool m_Draw = true;
	}
}
