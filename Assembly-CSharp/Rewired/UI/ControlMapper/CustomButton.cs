using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000546 RID: 1350
	[AddComponentMenu("")]
	public class CustomButton : Button, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000A8582 File Offset: 0x000A6982
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x000A858A File Offset: 0x000A698A
		public Sprite disabledHighlightedSprite
		{
			get
			{
				return this._disabledHighlightedSprite;
			}
			set
			{
				this._disabledHighlightedSprite = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000A8593 File Offset: 0x000A6993
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x000A859B File Offset: 0x000A699B
		public Color disabledHighlightedColor
		{
			get
			{
				return this._disabledHighlightedColor;
			}
			set
			{
				this._disabledHighlightedColor = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x000A85A4 File Offset: 0x000A69A4
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x000A85AC File Offset: 0x000A69AC
		public string disabledHighlightedTrigger
		{
			get
			{
				return this._disabledHighlightedTrigger;
			}
			set
			{
				this._disabledHighlightedTrigger = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x000A85B5 File Offset: 0x000A69B5
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x000A85BD File Offset: 0x000A69BD
		public bool autoNavUp
		{
			get
			{
				return this._autoNavUp;
			}
			set
			{
				this._autoNavUp = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000A85C6 File Offset: 0x000A69C6
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x000A85CE File Offset: 0x000A69CE
		public bool autoNavDown
		{
			get
			{
				return this._autoNavDown;
			}
			set
			{
				this._autoNavDown = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000A85D7 File Offset: 0x000A69D7
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x000A85DF File Offset: 0x000A69DF
		public bool autoNavLeft
		{
			get
			{
				return this._autoNavLeft;
			}
			set
			{
				this._autoNavLeft = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000A85E8 File Offset: 0x000A69E8
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x000A85F0 File Offset: 0x000A69F0
		public bool autoNavRight
		{
			get
			{
				return this._autoNavRight;
			}
			set
			{
				this._autoNavRight = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x000A85F9 File Offset: 0x000A69F9
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06001BD7 RID: 7127 RVA: 0x000A8604 File Offset: 0x000A6A04
		// (remove) Token: 0x06001BD8 RID: 7128 RVA: 0x000A863C File Offset: 0x000A6A3C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event UnityAction _CancelEvent;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06001BD9 RID: 7129 RVA: 0x000A8672 File Offset: 0x000A6A72
		// (remove) Token: 0x06001BDA RID: 7130 RVA: 0x000A867B File Offset: 0x000A6A7B
		public event UnityAction CancelEvent
		{
			add
			{
				this._CancelEvent += value;
			}
			remove
			{
				this._CancelEvent -= value;
			}
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000A8684 File Offset: 0x000A6A84
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000A86D0 File Offset: 0x000A6AD0
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000A871C File Offset: 0x000A6B1C
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000A8768 File Offset: 0x000A6B68
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000A87B2 File Offset: 0x000A6BB2
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000A87E8 File Offset: 0x000A6BE8
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.isHighlightDisabled)
			{
				Color disabledHighlightedColor = this._disabledHighlightedColor;
				Sprite disabledHighlightedSprite = this._disabledHighlightedSprite;
				string disabledHighlightedTrigger = this._disabledHighlightedTrigger;
				if (base.gameObject.activeInHierarchy)
				{
					Selectable.Transition transition = base.transition;
					if (transition != Selectable.Transition.ColorTint)
					{
						if (transition != Selectable.Transition.SpriteSwap)
						{
							if (transition == Selectable.Transition.Animation)
							{
								this.TriggerAnimation(disabledHighlightedTrigger);
							}
						}
						else
						{
							this.DoSpriteSwap(disabledHighlightedSprite);
						}
					}
					else
					{
						this.StartColorTween(disabledHighlightedColor * base.colors.colorMultiplier, instant);
					}
				}
			}
			else
			{
				base.DoStateTransition(state, instant);
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000A888C File Offset: 0x000A6C8C
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, (!instant) ? base.colors.fadeDuration : 0f, true, true);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000A88D7 File Offset: 0x000A6CD7
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000A88F8 File Offset: 0x000A6CF8
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000A8975 File Offset: 0x000A6D75
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000A8985 File Offset: 0x000A6D85
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000A8995 File Offset: 0x000A6D95
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			base.onClick.Invoke();
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000A89BC File Offset: 0x000A6DBC
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				this.isHighlightDisabled = true;
				this.DoStateTransition(Selectable.SelectionState.Disabled, false);
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000A8A18 File Offset: 0x000A6E18
		public override void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				this.isHighlightDisabled = true;
				this.DoStateTransition(Selectable.SelectionState.Disabled, false);
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000A8A68 File Offset: 0x000A6E68
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000A8A84 File Offset: 0x000A6E84
		private void EvaluateHightlightDisabled(bool isSelected)
		{
			if (!isSelected)
			{
				if (this.isHighlightDisabled)
				{
					this.isHighlightDisabled = false;
					Selectable.SelectionState state = (!this.isDisabled) ? base.currentSelectionState : Selectable.SelectionState.Disabled;
					this.DoStateTransition(state, false);
				}
			}
			else
			{
				if (!this.isDisabled)
				{
					return;
				}
				this.isHighlightDisabled = true;
				this.DoStateTransition(Selectable.SelectionState.Disabled, false);
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000A8AE9 File Offset: 0x000A6EE9
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent();
			}
		}

		// Token: 0x04001D63 RID: 7523
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x04001D64 RID: 7524
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x04001D65 RID: 7525
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x04001D66 RID: 7526
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x04001D67 RID: 7527
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x04001D68 RID: 7528
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001D69 RID: 7529
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001D6A RID: 7530
		private bool isHighlightDisabled;
	}
}
