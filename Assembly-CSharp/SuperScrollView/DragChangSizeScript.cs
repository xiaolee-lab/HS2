using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	// Token: 0x020005AF RID: 1455
	public class DragChangSizeScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000B9F4D File Offset: 0x000B834D
		public RectTransform CachedRectTransform
		{
			get
			{
				if (this.mCachedRectTransform == null)
				{
					this.mCachedRectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this.mCachedRectTransform;
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000B9F77 File Offset: 0x000B8377
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.SetCursor(this.mCursorTexture, this.mCursorHotSpot, CursorMode.Auto);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000B9F8C File Offset: 0x000B838C
		public void OnPointerExit(PointerEventData eventData)
		{
			this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000B9F9C File Offset: 0x000B839C
		private void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			if (Input.mousePresent)
			{
				Cursor.SetCursor(texture, hotspot, cursorMode);
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000B9FB0 File Offset: 0x000B83B0
		private void LateUpdate()
		{
			if (this.mCursorTexture == null)
			{
				return;
			}
			if (this.mIsDraging)
			{
				this.SetCursor(this.mCursorTexture, this.mCursorHotSpot, CursorMode.Auto);
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.CachedRectTransform, Input.mousePosition, this.mCamera, out vector))
			{
				this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
				return;
			}
			float num = this.CachedRectTransform.rect.width - vector.x;
			if (num < 0f)
			{
				this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
			}
			else if (num <= this.mBorderSize)
			{
				this.SetCursor(this.mCursorTexture, this.mCursorHotSpot, CursorMode.Auto);
			}
			else
			{
				this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000BA08B File Offset: 0x000B848B
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.mIsDraging = true;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000BA094 File Offset: 0x000B8494
		public void OnEndDrag(PointerEventData eventData)
		{
			this.mIsDraging = false;
			if (this.mOnDragEndAction != null)
			{
				this.mOnDragEndAction();
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000BA0B4 File Offset: 0x000B84B4
		public void OnDrag(PointerEventData eventData)
		{
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.CachedRectTransform, eventData.position, this.mCamera, out vector);
			if (vector.x <= 0f)
			{
				return;
			}
			this.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector.x);
		}

		// Token: 0x04002147 RID: 8519
		private bool mIsDraging;

		// Token: 0x04002148 RID: 8520
		public Camera mCamera;

		// Token: 0x04002149 RID: 8521
		public float mBorderSize = 10f;

		// Token: 0x0400214A RID: 8522
		public Texture2D mCursorTexture;

		// Token: 0x0400214B RID: 8523
		public Vector2 mCursorHotSpot = new Vector2(16f, 16f);

		// Token: 0x0400214C RID: 8524
		private RectTransform mCachedRectTransform;

		// Token: 0x0400214D RID: 8525
		public Action mOnDragEndAction;
	}
}
