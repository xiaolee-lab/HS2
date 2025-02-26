using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x0200051D RID: 1309
	[AddComponentMenu("")]
	[RequireComponent(typeof(RectTransform))]
	public sealed class UIPointer : UIBehaviour
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0009B88A File Offset: 0x00099C8A
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x0009B892 File Offset: 0x00099C92
		public bool autoSort
		{
			get
			{
				return this._autoSort;
			}
			set
			{
				if (value == this._autoSort)
				{
					return;
				}
				this._autoSort = value;
				if (value)
				{
					base.transform.SetAsLastSibling();
				}
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0009B8BC File Offset: 0x00099CBC
		protected override void Awake()
		{
			base.Awake();
			Graphic[] componentsInChildren = base.GetComponentsInChildren<Graphic>();
			foreach (Graphic graphic in componentsInChildren)
			{
				graphic.raycastTarget = false;
			}
			if (this._hideHardwarePointer)
			{
				Cursor.visible = false;
			}
			if (this._autoSort)
			{
				base.transform.SetAsLastSibling();
			}
			this.GetDependencies();
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0009B924 File Offset: 0x00099D24
		private void Update()
		{
			if (this._autoSort && base.transform.GetSiblingIndex() < base.transform.parent.childCount - 1)
			{
				base.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0009B95E File Offset: 0x00099D5E
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.GetDependencies();
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0009B96C File Offset: 0x00099D6C
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			this.GetDependencies();
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0009B97C File Offset: 0x00099D7C
		public void OnScreenPositionChanged(Vector2 screenPosition)
		{
			if (this._canvasRectTransform == null)
			{
				return;
			}
			Rect rect = this._canvasRectTransform.rect;
			Vector2 anchoredPosition = Camera.main.ScreenToViewportPoint(screenPosition);
			anchoredPosition.x = anchoredPosition.x * rect.width - this._canvasRectTransform.pivot.x * rect.width;
			anchoredPosition.y = anchoredPosition.y * rect.height - this._canvasRectTransform.pivot.y * rect.height;
			(base.transform as RectTransform).anchoredPosition = anchoredPosition;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0009BA34 File Offset: 0x00099E34
		private void GetDependencies()
		{
			Canvas componentInChildren = base.transform.root.GetComponentInChildren<Canvas>();
			this._canvasRectTransform = ((!(componentInChildren != null)) ? null : componentInChildren.GetComponent<RectTransform>());
		}

		// Token: 0x04001C1B RID: 7195
		[Tooltip("Should the hardware pointer be hidden?")]
		[SerializeField]
		private bool _hideHardwarePointer = true;

		// Token: 0x04001C1C RID: 7196
		[Tooltip("Sets the pointer to the last sibling in the parent hierarchy. Do not enable this on multiple UIPointers under the same parent transform or they will constantly fight each other for dominance.")]
		[SerializeField]
		private bool _autoSort = true;

		// Token: 0x04001C1D RID: 7197
		[NonSerialized]
		private RectTransform _canvasRectTransform;
	}
}
