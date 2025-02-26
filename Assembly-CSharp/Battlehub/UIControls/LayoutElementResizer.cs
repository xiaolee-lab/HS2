using System;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200008C RID: 140
	public class LayoutElementResizer : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x0000DED4 File Offset: 0x0000C2D4
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (this.Parent != null && this.SecondaryTarget != null)
			{
				if (this.XSign != 0f)
				{
					this.Target.flexibleWidth = Mathf.Clamp01(this.Target.flexibleWidth);
					this.SecondaryTarget.flexibleWidth = Mathf.Clamp01(this.SecondaryTarget.flexibleWidth);
				}
				if (this.YSign != 0f)
				{
					this.Target.flexibleHeight = Mathf.Clamp01(this.Target.flexibleHeight);
					this.SecondaryTarget.flexibleHeight = Mathf.Clamp01(this.SecondaryTarget.flexibleHeight);
				}
				this.m_midY = this.Target.flexibleHeight / (this.Target.flexibleHeight + this.SecondaryTarget.flexibleHeight);
				this.m_midY *= Math.Max(this.Parent.rect.height - this.Target.minHeight - this.SecondaryTarget.minHeight, 0f);
				this.m_midX = this.Target.flexibleWidth / (this.Target.flexibleWidth + this.SecondaryTarget.flexibleWidth);
				this.m_midX *= Math.Max(this.Parent.rect.width - this.Target.minWidth - this.SecondaryTarget.minWidth, 0f);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E068 File Offset: 0x0000C468
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (this.Parent != null && this.SecondaryTarget != null)
			{
				if (this.XSign != 0f)
				{
					float num = this.m_midX + eventData.delta.x * (float)Math.Sign(this.XSign);
					float num2 = num / (this.Parent.rect.width - this.Target.minWidth - this.SecondaryTarget.minWidth);
					this.Target.flexibleWidth = num2;
					this.SecondaryTarget.flexibleWidth = 1f - num2;
					this.m_midX = num;
				}
				if (this.YSign != 0f)
				{
					float num3 = this.m_midY + eventData.delta.y * (float)Math.Sign(this.YSign);
					float num4 = num3 / (this.Parent.rect.height - this.Target.minHeight - this.SecondaryTarget.minHeight);
					this.Target.flexibleHeight = num4;
					this.SecondaryTarget.flexibleHeight = 1f - num4;
					this.m_midY = num3;
				}
				if (this.XSign != 0f)
				{
					this.Target.flexibleWidth = Mathf.Clamp01(this.Target.flexibleWidth);
					this.SecondaryTarget.flexibleWidth = Mathf.Clamp01(this.SecondaryTarget.flexibleWidth);
				}
				if (this.YSign != 0f)
				{
					this.Target.flexibleHeight = Mathf.Clamp01(this.Target.flexibleHeight);
					this.SecondaryTarget.flexibleHeight = Mathf.Clamp01(this.SecondaryTarget.flexibleHeight);
				}
			}
			else
			{
				if (this.XSign != 0f)
				{
					this.Target.preferredWidth += eventData.delta.x * (float)Math.Sign(this.XSign);
					if (this.HasMaxSize && this.Target.preferredWidth > this.MaxSize)
					{
						this.Target.preferredWidth = this.MaxSize;
					}
				}
				if (this.YSign != 0f)
				{
					this.Target.preferredHeight += eventData.delta.y * (float)Math.Sign(this.YSign);
					if (this.HasMaxSize && this.Target.preferredHeight > this.MaxSize)
					{
						this.Target.preferredHeight = this.MaxSize;
					}
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E31C File Offset: 0x0000C71C
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E31E File Offset: 0x0000C71E
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E320 File Offset: 0x0000C720
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			this.m_pointerInside = true;
			this.m_cursorHelper.SetCursor(this, this.CursorTexture, new Vector2(0.5f, 0.5f), CursorMode.Auto);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E34B File Offset: 0x0000C74B
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			this.m_pointerInside = false;
			if (!this.m_pointerDown)
			{
				this.m_cursorHelper.ResetCursor(this);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E36C File Offset: 0x0000C76C
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			this.m_pointerDown = true;
			if (this.Target.preferredWidth < -1f)
			{
				this.Target.preferredWidth = this.Target.minWidth;
			}
			if (this.Target.preferredHeight < -1f)
			{
				this.Target.preferredHeight = this.Target.minHeight;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E3D6 File Offset: 0x0000C7D6
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			this.m_pointerDown = false;
			if (!this.m_pointerInside)
			{
				this.m_cursorHelper.ResetCursor(this);
			}
		}

		// Token: 0x04000275 RID: 629
		public LayoutElement Target;

		// Token: 0x04000276 RID: 630
		public RectTransform Parent;

		// Token: 0x04000277 RID: 631
		public LayoutElement SecondaryTarget;

		// Token: 0x04000278 RID: 632
		public Texture2D CursorTexture;

		// Token: 0x04000279 RID: 633
		public float XSign = 1f;

		// Token: 0x0400027A RID: 634
		public float YSign;

		// Token: 0x0400027B RID: 635
		public float MaxSize;

		// Token: 0x0400027C RID: 636
		public bool HasMaxSize;

		// Token: 0x0400027D RID: 637
		private bool m_pointerInside;

		// Token: 0x0400027E RID: 638
		private bool m_pointerDown;

		// Token: 0x0400027F RID: 639
		private float m_midX;

		// Token: 0x04000280 RID: 640
		private float m_midY;

		// Token: 0x04000281 RID: 641
		private CursorHelper m_cursorHelper = new CursorHelper();
	}
}
