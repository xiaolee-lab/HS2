using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x02001247 RID: 4679
	public class GuideBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06009A14 RID: 39444 RVA: 0x003F4FFF File Offset: 0x003F33FF
		public Material material
		{
			get
			{
				return (!this.renderer) ? null : this.renderer.material;
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06009A15 RID: 39445 RVA: 0x003F5022 File Offset: 0x003F3422
		// (set) Token: 0x06009A16 RID: 39446 RVA: 0x003F502F File Offset: 0x003F342F
		public bool draw
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

		// Token: 0x06009A17 RID: 39447 RVA: 0x003F5040 File Offset: 0x003F3440
		protected Color ConvertColor(Color _color)
		{
			_color.r *= 0.75f;
			_color.g *= 0.75f;
			_color.b *= 0.75f;
			_color.a = 0.25f;
			return _color;
		}

		// Token: 0x170020E6 RID: 8422
		// (set) Token: 0x06009A18 RID: 39448 RVA: 0x003F5094 File Offset: 0x003F3494
		protected Color colorNow
		{
			set
			{
				if (this.material)
				{
					this.material.color = value;
					if (this.material.HasProperty("_EmissionColor"))
					{
						this.material.SetColor("_EmissionColor", value);
					}
				}
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x06009A19 RID: 39449 RVA: 0x003F50E3 File Offset: 0x003F34E3
		// (set) Token: 0x06009A1A RID: 39450 RVA: 0x003F50EB File Offset: 0x003F34EB
		public bool isDrag { get; private set; }

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06009A1B RID: 39451 RVA: 0x003F50F4 File Offset: 0x003F34F4
		// (set) Token: 0x06009A1C RID: 39452 RVA: 0x003F50FC File Offset: 0x003F34FC
		public GuideObject guideObject { get; set; }

		// Token: 0x06009A1D RID: 39453 RVA: 0x003F5105 File Offset: 0x003F3505
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Singleton<GuideObjectManager>.Instance.isOperationTarget)
			{
				return;
			}
			this.colorNow = this.colorHighlighted;
			if (this.pointerEnterAction != null)
			{
				this.pointerEnterAction(base.transform);
			}
		}

		// Token: 0x06009A1E RID: 39454 RVA: 0x003F513F File Offset: 0x003F353F
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.isDrag)
			{
				this.colorNow = this.colorNormal;
			}
		}

		// Token: 0x06009A1F RID: 39455 RVA: 0x003F5158 File Offset: 0x003F3558
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isDrag = true;
			Singleton<GuideObjectManager>.Instance.operationTarget = this.guideObject;
		}

		// Token: 0x06009A20 RID: 39456 RVA: 0x003F5171 File Offset: 0x003F3571
		public virtual void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06009A21 RID: 39457 RVA: 0x003F5173 File Offset: 0x003F3573
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.isDrag = false;
			this.colorNow = this.colorNormal;
			Singleton<GuideObjectManager>.Instance.operationTarget = null;
		}

		// Token: 0x06009A22 RID: 39458 RVA: 0x003F5193 File Offset: 0x003F3593
		private void OnDisable()
		{
			this.colorNow = this.colorNormal;
		}

		// Token: 0x06009A23 RID: 39459 RVA: 0x003F51A4 File Offset: 0x003F35A4
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
				this.renderer.enabled = this.draw;
			}
			if (this.collider)
			{
				this.collider.enabled = this.draw;
			}
			this.colorNow = this.colorNormal;
			this.isDrag = false;
		}

		// Token: 0x04007B06 RID: 31494
		[SerializeField]
		protected Color colorNormal;

		// Token: 0x04007B07 RID: 31495
		[SerializeField]
		protected Color colorHighlighted;

		// Token: 0x04007B08 RID: 31496
		protected Renderer renderer;

		// Token: 0x04007B09 RID: 31497
		protected Collider collider;

		// Token: 0x04007B0A RID: 31498
		private BoolReactiveProperty _draw = new BoolReactiveProperty(true);

		// Token: 0x04007B0D RID: 31501
		public Action<Transform> pointerEnterAction;
	}
}
