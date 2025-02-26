using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharaCustom
{
	// Token: 0x020009D3 RID: 2515
	public class CustomUIDrag : CustomCanvasSort
	{
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x001C1740 File Offset: 0x001BFB40
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x001C1747 File Offset: 0x001BFB47
		private void Start()
		{
			if (this.camCtrl == null && Camera.main)
			{
				this.camCtrl = Camera.main.GetComponent<CameraControl_Ver2>();
			}
			this.UpdatePosition();
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x001C1780 File Offset: 0x001BFB80
		public void UpdatePosition()
		{
			switch (this.type)
			{
			case CustomUIDrag.CanvasType.SubWin:
				this.rtMove.anchoredPosition = this.customBase.customSettingSave.winSubLayout;
				break;
			case CustomUIDrag.CanvasType.DrawWin:
				this.rtMove.anchoredPosition = this.customBase.customSettingSave.winDrawLayout;
				break;
			case CustomUIDrag.CanvasType.PatternWin:
				this.rtMove.anchoredPosition = this.customBase.customSettingSave.winPatternLayout;
				break;
			case CustomUIDrag.CanvasType.ColorWin:
				this.rtMove.anchoredPosition = this.customBase.customSettingSave.winColorLayout;
				break;
			}
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x001C1834 File Offset: 0x001BFC34
		private void CalcDragPos(PointerEventData ped)
		{
			Vector2 vector = new Vector2(this.rtMove.anchoredPosition.x + ped.delta.x, this.rtMove.anchoredPosition.y + ped.delta.y);
			vector.x = Mathf.Clamp(vector.x, 0f, this.rtRange.sizeDelta.x - this.rtRect.sizeDelta.x);
			vector.y = -Mathf.Clamp(-vector.y, 0f, this.rtRange.sizeDelta.y - this.rtRect.sizeDelta.y);
			this.rtMove.anchoredPosition = vector;
			switch (this.type)
			{
			case CustomUIDrag.CanvasType.SubWin:
				this.customBase.customSettingSave.winSubLayout = vector;
				break;
			case CustomUIDrag.CanvasType.DrawWin:
				this.customBase.customSettingSave.winDrawLayout = vector;
				break;
			case CustomUIDrag.CanvasType.PatternWin:
				this.customBase.customSettingSave.winPatternLayout = vector;
				break;
			case CustomUIDrag.CanvasType.ColorWin:
				this.customBase.customSettingSave.winColorLayout = vector;
				break;
			}
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x001C199E File Offset: 0x001BFD9E
		public override void OnPointerDown(PointerEventData ped)
		{
			base.OnPointerDown(ped);
			if (!Input.GetMouseButton(0))
			{
				return;
			}
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x001C19B4 File Offset: 0x001BFDB4
		public override void OnBeginDrag(PointerEventData ped)
		{
			base.OnBeginDrag(ped);
			if (!Input.GetMouseButton(0))
			{
				return;
			}
			this.isDrag = true;
			if (this.camCtrl)
			{
				this.camCtrl.NoCtrlCondition = (() => true);
			}
			this.CalcDragPos(ped);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x001C1A1A File Offset: 0x001BFE1A
		public override void OnDrag(PointerEventData ped)
		{
			base.OnDrag(ped);
			if (!this.isDrag)
			{
				return;
			}
			this.CalcDragPos(ped);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x001C1A38 File Offset: 0x001BFE38
		public override void OnEndDrag(PointerEventData ped)
		{
			base.OnEndDrag(ped);
			if (!this.isDrag)
			{
				return;
			}
			this.isDrag = false;
			if (this.camCtrl)
			{
				this.camCtrl.NoCtrlCondition = (() => false);
			}
		}

		// Token: 0x04004449 RID: 17481
		[SerializeField]
		private CustomUIDrag.CanvasType type = CustomUIDrag.CanvasType.ColorWin;

		// Token: 0x0400444A RID: 17482
		[SerializeField]
		private RectTransform rtMove;

		// Token: 0x0400444B RID: 17483
		[SerializeField]
		private RectTransform rtRect;

		// Token: 0x0400444C RID: 17484
		[SerializeField]
		private RectTransform rtRange;

		// Token: 0x0400444D RID: 17485
		private CameraControl_Ver2 camCtrl;

		// Token: 0x0400444E RID: 17486
		private bool isDrag;

		// Token: 0x020009D4 RID: 2516
		public enum CanvasType
		{
			// Token: 0x04004452 RID: 17490
			SubWin,
			// Token: 0x04004453 RID: 17491
			DrawWin,
			// Token: 0x04004454 RID: 17492
			PatternWin,
			// Token: 0x04004455 RID: 17493
			ColorWin
		}
	}
}
