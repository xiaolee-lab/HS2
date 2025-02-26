using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200132C RID: 4908
	public class MapDragButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IInitializePotentialDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x0600A435 RID: 42037 RVA: 0x00430EAC File Offset: 0x0042F2AC
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			Singleton<Studio>.Instance.cameraCtrl.isCursorLock = false;
			if (Singleton<GameCursor>.IsInstance())
			{
				Singleton<GameCursor>.Instance.SetCursorLock(true);
			}
			if (this.button)
			{
				this.button.transition = Selectable.Transition.None;
			}
		}

		// Token: 0x0600A436 RID: 42038 RVA: 0x00430EFA File Offset: 0x0042F2FA
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.onBeginDragFunc != null)
			{
				this.onBeginDragFunc();
			}
		}

		// Token: 0x0600A437 RID: 42039 RVA: 0x00430F12 File Offset: 0x0042F312
		public void OnDrag(PointerEventData eventData)
		{
			if (this.onDragFunc != null)
			{
				this.onDragFunc();
			}
		}

		// Token: 0x0600A438 RID: 42040 RVA: 0x00430F2C File Offset: 0x0042F32C
		public void OnEndDrag(PointerEventData eventData)
		{
			Singleton<Studio>.Instance.cameraCtrl.isCursorLock = true;
			if (Singleton<GameCursor>.IsInstance())
			{
				Singleton<GameCursor>.Instance.SetCursorLock(false);
			}
			if (this.button)
			{
				this.button.transition = Selectable.Transition.ColorTint;
			}
			if (this.onEndDragFunc != null)
			{
				this.onEndDragFunc();
			}
		}

		// Token: 0x0600A439 RID: 42041 RVA: 0x00430F90 File Offset: 0x0042F390
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x0600A43A RID: 42042 RVA: 0x00430F94 File Offset: 0x0042F394
		public void OnPointerUp(PointerEventData eventData)
		{
			Singleton<Studio>.Instance.cameraCtrl.isCursorLock = true;
			if (Singleton<GameCursor>.IsInstance())
			{
				Singleton<GameCursor>.Instance.SetCursorLock(false);
			}
			if (this.button)
			{
				this.button.transition = Selectable.Transition.ColorTint;
			}
		}

		// Token: 0x04008162 RID: 33122
		[SerializeField]
		private Button button;

		// Token: 0x04008163 RID: 33123
		public Action onBeginDragFunc;

		// Token: 0x04008164 RID: 33124
		public Action onDragFunc;

		// Token: 0x04008165 RID: 33125
		public Action onEndDragFunc;
	}
}
