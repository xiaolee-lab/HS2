using System;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000FEA RID: 4074
	public class SwellButton : MonoBehaviour
	{
		// Token: 0x060088F3 RID: 35059 RVA: 0x0038FDF8 File Offset: 0x0038E1F8
		private void Start()
		{
			if (this._button != null)
			{
				(from _ in this._button.OnPointerEnterAsObservable()
				where this._button.IsInteractable()
				select _).Subscribe(delegate(PointerEventData _)
				{
					this._entered = true;
					this.Easing(true, this._focusDuration);
				});
				(from _ in this._button.OnPointerExitAsObservable()
				where this._button.IsInteractable()
				select _).Subscribe(delegate(PointerEventData _)
				{
					this._entered = false;
					this.Easing(false, this._focusDuration);
				});
				(from _ in this._button.OnPointerDownAsObservable()
				where this._button.IsInteractable() && this._entered
				select _).Subscribe(delegate(PointerEventData _)
				{
					this.Easing(false, this._clickDuration);
				});
				(from _ in this._button.OnPointerUpAsObservable()
				where this._button.IsInteractable() && this._entered
				select _).Subscribe(delegate(PointerEventData _)
				{
					this.Easing(true, this._clickDuration);
				});
			}
		}

		// Token: 0x060088F4 RID: 35060 RVA: 0x0038FED0 File Offset: 0x0038E2D0
		private void Easing(bool fadeIn, float duration)
		{
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
			Vector3 startScale = this._swellTarget.transform.localScale;
			Vector3 dest = (!fadeIn) ? this._defaultScale : this._destScale;
			this._disposable = ObservableEasing.EaseOutQuint(duration, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._swellTarget.transform.localScale = Vector3.Lerp(startScale, dest, x.Value);
			});
		}

		// Token: 0x04006ED6 RID: 28374
		[SerializeField]
		private Button _button;

		// Token: 0x04006ED7 RID: 28375
		[SerializeField]
		private Image _swellTarget;

		// Token: 0x04006ED8 RID: 28376
		[SerializeField]
		private Vector3 _destScale = Vector3.one;

		// Token: 0x04006ED9 RID: 28377
		[SerializeField]
		private float _focusDuration;

		// Token: 0x04006EDA RID: 28378
		[SerializeField]
		private float _clickDuration;

		// Token: 0x04006EDB RID: 28379
		private Vector3 _defaultScale = Vector3.one;

		// Token: 0x04006EDC RID: 28380
		private bool _entered;

		// Token: 0x04006EDD RID: 28381
		private IDisposable _disposable;
	}
}
