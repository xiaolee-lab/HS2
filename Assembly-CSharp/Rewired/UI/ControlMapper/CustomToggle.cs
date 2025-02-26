using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000548 RID: 1352
	[AddComponentMenu("")]
	public class CustomToggle : Toggle, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x000A90C5 File Offset: 0x000A74C5
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x000A90CD File Offset: 0x000A74CD
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

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x000A90D6 File Offset: 0x000A74D6
		// (set) Token: 0x06001C11 RID: 7185 RVA: 0x000A90DE File Offset: 0x000A74DE
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x000A90E7 File Offset: 0x000A74E7
		// (set) Token: 0x06001C13 RID: 7187 RVA: 0x000A90EF File Offset: 0x000A74EF
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x000A90F8 File Offset: 0x000A74F8
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x000A9100 File Offset: 0x000A7500
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x000A9109 File Offset: 0x000A7509
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x000A9111 File Offset: 0x000A7511
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x000A911A File Offset: 0x000A751A
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x000A9122 File Offset: 0x000A7522
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

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x000A912B File Offset: 0x000A752B
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x000A9133 File Offset: 0x000A7533
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000A913C File Offset: 0x000A753C
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06001C1D RID: 7197 RVA: 0x000A9148 File Offset: 0x000A7548
		// (remove) Token: 0x06001C1E RID: 7198 RVA: 0x000A9180 File Offset: 0x000A7580
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event UnityAction _CancelEvent;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06001C1F RID: 7199 RVA: 0x000A91B6 File Offset: 0x000A75B6
		// (remove) Token: 0x06001C20 RID: 7200 RVA: 0x000A91BF File Offset: 0x000A75BF
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

		// Token: 0x06001C21 RID: 7201 RVA: 0x000A91C8 File Offset: 0x000A75C8
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000A9214 File Offset: 0x000A7614
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000A9260 File Offset: 0x000A7660
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000A92AC File Offset: 0x000A76AC
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000A92F6 File Offset: 0x000A76F6
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000A932C File Offset: 0x000A772C
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

		// Token: 0x06001C27 RID: 7207 RVA: 0x000A93D0 File Offset: 0x000A77D0
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, (!instant) ? base.colors.fadeDuration : 0f, true, true);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000A941B File Offset: 0x000A781B
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000A943C File Offset: 0x000A783C
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000A94B9 File Offset: 0x000A78B9
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000A94C9 File Offset: 0x000A78C9
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000A94DC File Offset: 0x000A78DC
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

		// Token: 0x06001C2D RID: 7213 RVA: 0x000A9541 File Offset: 0x000A7941
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent();
			}
		}

		// Token: 0x04001D75 RID: 7541
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x04001D76 RID: 7542
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x04001D77 RID: 7543
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x04001D78 RID: 7544
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x04001D79 RID: 7545
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x04001D7A RID: 7546
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001D7B RID: 7547
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001D7C RID: 7548
		private bool isHighlightDisabled;
	}
}
