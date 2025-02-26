using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Housing
{
	// Token: 0x020008BF RID: 2239
	public class GuideBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x001564E1 File Offset: 0x001548E1
		public Material Material
		{
			get
			{
				return (!this.renderer) ? null : this.renderer.material;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x00156504 File Offset: 0x00154904
		// (set) Token: 0x06003A77 RID: 14967 RVA: 0x00156511 File Offset: 0x00154911
		public bool Draw
		{
			get
			{
				return this._draw.Value;
			}
			set
			{
				this._draw.Value = value;
			}
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x00156520 File Offset: 0x00154920
		protected Color ConvertColor(Color _color)
		{
			_color.r *= 0.75f;
			_color.g *= 0.75f;
			_color.b *= 0.75f;
			_color.a = 0.25f;
			return _color;
		}

		// Token: 0x17000A8E RID: 2702
		// (set) Token: 0x06003A79 RID: 14969 RVA: 0x00156574 File Offset: 0x00154974
		protected Color ColorNow
		{
			set
			{
				if (this.Material)
				{
					this.Material.color = value;
					if (this.Material.HasProperty("_EmissionColor"))
					{
						this.Material.SetColor("_EmissionColor", value);
					}
				}
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x001565C3 File Offset: 0x001549C3
		// (set) Token: 0x06003A7B RID: 14971 RVA: 0x001565CB File Offset: 0x001549CB
		public bool IsInit { get; protected set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x001565D4 File Offset: 0x001549D4
		// (set) Token: 0x06003A7D RID: 14973 RVA: 0x001565DC File Offset: 0x001549DC
		public bool IsDrag { get; protected set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x001565E5 File Offset: 0x001549E5
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x001565ED File Offset: 0x001549ED
		public GuideObject GuideObject { get; protected set; }

		// Token: 0x06003A80 RID: 14976 RVA: 0x001565F8 File Offset: 0x001549F8
		public virtual void Init(GuideObject _guideObject)
		{
			if (this.IsInit)
			{
				return;
			}
			this.GuideObject = _guideObject;
			this.renderer = base.gameObject.GetComponent<Renderer>();
			if (this.renderer == null)
			{
				this.renderer = base.gameObject.GetComponentInChildren<Renderer>();
			}
			this.collider = this.renderer.GetComponent<Collider>();
			this.colorNormal = this.ConvertColor(this.Material.color);
			this.colorHighlighted = this.Material.color;
			this.colorHighlighted.a = 0.75f;
			this._draw.Subscribe(delegate(bool _b)
			{
				if (this.renderer)
				{
					this.renderer.enabled = _b;
				}
				if (this.collider)
				{
					this.collider.enabled = _b;
				}
			});
			if (this.renderer)
			{
				this.renderer.enabled = this.Draw;
			}
			if (this.collider)
			{
				this.collider.enabled = this.Draw;
			}
			this.ColorNow = this.colorNormal;
			this.IsDrag = false;
			this.IsInit = true;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x00156708 File Offset: 0x00154B08
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.ColorNow = this.colorHighlighted;
			if (this.pointerEnterAction != null)
			{
				this.pointerEnterAction(base.transform);
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x00156734 File Offset: 0x00154B34
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			if (!this.IsDrag)
			{
				this.ColorNow = this.colorNormal;
			}
			if (this.pointerExitAction != null)
			{
				this.pointerExitAction(base.transform);
			}
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0015676B File Offset: 0x00154B6B
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!eventData.dragging)
			{
				return;
			}
			this.IsDrag = true;
			if (this.beginDragAction != null)
			{
				this.beginDragAction(base.transform);
			}
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x0015679E File Offset: 0x00154B9E
		public virtual void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x001567A0 File Offset: 0x00154BA0
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!eventData.dragging || eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.IsDrag = false;
			this.ColorNow = this.colorNormal;
			if (this.endDragAction != null)
			{
				this.endDragAction(base.transform);
			}
			if (Singleton<GuideManager>.IsInstance())
			{
				Singleton<GuideManager>.Instance.IsGuide = false;
			}
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x0015680A File Offset: 0x00154C0A
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (this.pointerDownAction != null)
			{
				this.pointerDownAction();
			}
			if (Singleton<GuideManager>.IsInstance())
			{
				Singleton<GuideManager>.Instance.IsGuide = true;
			}
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x00156839 File Offset: 0x00154C39
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (this.pointerUpAction != null)
			{
				this.pointerUpAction();
			}
			if (Singleton<GuideManager>.IsInstance())
			{
				Singleton<GuideManager>.Instance.IsGuide = false;
			}
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x00156868 File Offset: 0x00154C68
		private void OnDisable()
		{
			this.ColorNow = this.colorNormal;
		}

		// Token: 0x040039B5 RID: 14773
		[SerializeField]
		protected Color colorNormal;

		// Token: 0x040039B6 RID: 14774
		[SerializeField]
		protected Color colorHighlighted;

		// Token: 0x040039B7 RID: 14775
		protected Renderer renderer;

		// Token: 0x040039B8 RID: 14776
		protected Collider collider;

		// Token: 0x040039B9 RID: 14777
		protected BoolReactiveProperty _draw = new BoolReactiveProperty(true);

		// Token: 0x040039BD RID: 14781
		public Action<Transform> pointerEnterAction;

		// Token: 0x040039BE RID: 14782
		public Action<Transform> pointerExitAction;

		// Token: 0x040039BF RID: 14783
		public Action<Transform> beginDragAction;

		// Token: 0x040039C0 RID: 14784
		public Action<Transform> endDragAction;

		// Token: 0x040039C1 RID: 14785
		public Action pointerDownAction;

		// Token: 0x040039C2 RID: 14786
		public Action pointerUpAction;
	}
}
