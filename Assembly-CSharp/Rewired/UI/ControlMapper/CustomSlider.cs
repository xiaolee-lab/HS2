using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000547 RID: 1351
	[AddComponentMenu("")]
	public class CustomSlider : Slider, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000A8C0D File Offset: 0x000A700D
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x000A8C15 File Offset: 0x000A7015
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000A8C1E File Offset: 0x000A701E
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x000A8C26 File Offset: 0x000A7026
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000A8C2F File Offset: 0x000A702F
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x000A8C37 File Offset: 0x000A7037
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000A8C40 File Offset: 0x000A7040
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x000A8C48 File Offset: 0x000A7048
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

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000A8C51 File Offset: 0x000A7051
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x000A8C59 File Offset: 0x000A7059
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000A8C62 File Offset: 0x000A7062
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x000A8C6A File Offset: 0x000A706A
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x000A8C73 File Offset: 0x000A7073
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x000A8C7B File Offset: 0x000A707B
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x000A8C84 File Offset: 0x000A7084
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06001BFC RID: 7164 RVA: 0x000A8C90 File Offset: 0x000A7090
		// (remove) Token: 0x06001BFD RID: 7165 RVA: 0x000A8CC8 File Offset: 0x000A70C8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event UnityAction _CancelEvent;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06001BFE RID: 7166 RVA: 0x000A8CFE File Offset: 0x000A70FE
		// (remove) Token: 0x06001BFF RID: 7167 RVA: 0x000A8D07 File Offset: 0x000A7107
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

		// Token: 0x06001C00 RID: 7168 RVA: 0x000A8D10 File Offset: 0x000A7110
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000A8D5C File Offset: 0x000A715C
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000A8DA8 File Offset: 0x000A71A8
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000A8DF4 File Offset: 0x000A71F4
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Selectable.allSelectables, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000A8E3E File Offset: 0x000A723E
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000A8E74 File Offset: 0x000A7274
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

		// Token: 0x06001C06 RID: 7174 RVA: 0x000A8F18 File Offset: 0x000A7318
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, (!instant) ? base.colors.fadeDuration : 0f, true, true);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000A8F63 File Offset: 0x000A7363
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000A8F84 File Offset: 0x000A7384
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000A9001 File Offset: 0x000A7401
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000A9011 File Offset: 0x000A7411
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000A9024 File Offset: 0x000A7424
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

		// Token: 0x06001C0C RID: 7180 RVA: 0x000A9089 File Offset: 0x000A7489
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent();
			}
		}

		// Token: 0x04001D6C RID: 7532
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x04001D6D RID: 7533
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x04001D6E RID: 7534
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x04001D6F RID: 7535
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x04001D70 RID: 7536
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x04001D71 RID: 7537
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001D72 RID: 7538
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001D73 RID: 7539
		private bool isHighlightDisabled;
	}
}
