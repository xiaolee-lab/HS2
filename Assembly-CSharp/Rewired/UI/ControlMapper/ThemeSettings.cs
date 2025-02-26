using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000556 RID: 1366
	[Serializable]
	public class ThemeSettings : ScriptableObject
	{
		// Token: 0x06001CB0 RID: 7344 RVA: 0x000AA69C File Offset: 0x000A8A9C
		public void Apply(ThemedElement.ElementInfo[] elementInfo)
		{
			if (elementInfo == null)
			{
				return;
			}
			for (int i = 0; i < elementInfo.Length; i++)
			{
				if (elementInfo[i] != null)
				{
					this.Apply(elementInfo[i].themeClass, elementInfo[i].component);
				}
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000AA6E8 File Offset: 0x000A8AE8
		private void Apply(string themeClass, Component component)
		{
			if (component as Selectable != null)
			{
				this.Apply(themeClass, (Selectable)component);
				return;
			}
			if (component as Image != null)
			{
				this.Apply(themeClass, (Image)component);
				return;
			}
			if (component as Text != null)
			{
				this.Apply(themeClass, (Text)component);
				return;
			}
			if (component as UIImageHelper != null)
			{
				this.Apply(themeClass, (UIImageHelper)component);
				return;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000AA774 File Offset: 0x000A8B74
		private void Apply(string themeClass, Selectable item)
		{
			if (item == null)
			{
				return;
			}
			ThemeSettings.SelectableSettings_Base selectableSettings_Base;
			if (item as Button != null)
			{
				if (themeClass != null)
				{
					if (themeClass == "inputGridField")
					{
						selectableSettings_Base = this._inputGridFieldSettings;
						goto IL_51;
					}
				}
				selectableSettings_Base = this._buttonSettings;
				IL_51:;
			}
			else if (item as Scrollbar != null)
			{
				selectableSettings_Base = this._scrollbarSettings;
			}
			else if (item as Slider != null)
			{
				selectableSettings_Base = this._sliderSettings;
			}
			else if (item as Toggle != null)
			{
				if (themeClass != null)
				{
					if (themeClass == "button")
					{
						selectableSettings_Base = this._buttonSettings;
						goto IL_D4;
					}
				}
				selectableSettings_Base = this._selectableSettings;
				IL_D4:;
			}
			else
			{
				selectableSettings_Base = this._selectableSettings;
			}
			selectableSettings_Base.Apply(item);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000AA868 File Offset: 0x000A8C68
		private void Apply(string themeClass, Image item)
		{
			if (item == null)
			{
				return;
			}
			switch (themeClass)
			{
			case "area":
				this._areaBackground.CopyTo(item);
				break;
			case "popupWindow":
				this._popupWindowBackground.CopyTo(item);
				break;
			case "mainWindow":
				this._mainWindowBackground.CopyTo(item);
				break;
			case "calibrationValueMarker":
				this._calibrationValueMarker.CopyTo(item);
				break;
			case "calibrationRawValueMarker":
				this._calibrationRawValueMarker.CopyTo(item);
				break;
			case "invertToggle":
				this._invertToggle.CopyTo(item);
				break;
			case "invertToggleBackground":
				this._inputGridFieldSettings.imageSettings.CopyTo(item);
				break;
			case "invertToggleButtonBackground":
				this._buttonSettings.imageSettings.CopyTo(item);
				break;
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000AA9D0 File Offset: 0x000A8DD0
		private void Apply(string themeClass, Text item)
		{
			if (item == null)
			{
				return;
			}
			ThemeSettings.TextSettings textSettings;
			if (themeClass != null)
			{
				if (themeClass == "button")
				{
					textSettings = this._buttonTextSettings;
					goto IL_5C;
				}
				if (themeClass == "inputGridField")
				{
					textSettings = this._inputGridFieldTextSettings;
					goto IL_5C;
				}
			}
			textSettings = this._textSettings;
			IL_5C:
			if (textSettings.font != null)
			{
				item.font = textSettings.font;
			}
			item.color = textSettings.color;
			item.lineSpacing = textSettings.lineSpacing;
			if (textSettings.sizeMultiplier != 1f)
			{
				item.fontSize = (int)((float)item.fontSize * textSettings.sizeMultiplier);
				item.resizeTextMaxSize = (int)((float)item.resizeTextMaxSize * textSettings.sizeMultiplier);
				item.resizeTextMinSize = (int)((float)item.resizeTextMinSize * textSettings.sizeMultiplier);
			}
			if (textSettings.style != ThemeSettings.FontStyleOverride.Default)
			{
				item.fontStyle = (FontStyle)(textSettings.style - 1);
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000AAAD6 File Offset: 0x000A8ED6
		private void Apply(string themeClass, UIImageHelper item)
		{
			if (item == null)
			{
				return;
			}
			item.SetEnabledStateColor(this._invertToggle.color);
			item.SetDisabledStateColor(this._invertToggleDisabledColor);
			item.Refresh();
		}

		// Token: 0x04001DE4 RID: 7652
		[SerializeField]
		private ThemeSettings.ImageSettings _mainWindowBackground;

		// Token: 0x04001DE5 RID: 7653
		[SerializeField]
		private ThemeSettings.ImageSettings _popupWindowBackground;

		// Token: 0x04001DE6 RID: 7654
		[SerializeField]
		private ThemeSettings.ImageSettings _areaBackground;

		// Token: 0x04001DE7 RID: 7655
		[SerializeField]
		private ThemeSettings.SelectableSettings _selectableSettings;

		// Token: 0x04001DE8 RID: 7656
		[SerializeField]
		private ThemeSettings.SelectableSettings _buttonSettings;

		// Token: 0x04001DE9 RID: 7657
		[SerializeField]
		private ThemeSettings.SelectableSettings _inputGridFieldSettings;

		// Token: 0x04001DEA RID: 7658
		[SerializeField]
		private ThemeSettings.ScrollbarSettings _scrollbarSettings;

		// Token: 0x04001DEB RID: 7659
		[SerializeField]
		private ThemeSettings.SliderSettings _sliderSettings;

		// Token: 0x04001DEC RID: 7660
		[SerializeField]
		private ThemeSettings.ImageSettings _invertToggle;

		// Token: 0x04001DED RID: 7661
		[SerializeField]
		private Color _invertToggleDisabledColor;

		// Token: 0x04001DEE RID: 7662
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationValueMarker;

		// Token: 0x04001DEF RID: 7663
		[SerializeField]
		private ThemeSettings.ImageSettings _calibrationRawValueMarker;

		// Token: 0x04001DF0 RID: 7664
		[SerializeField]
		private ThemeSettings.TextSettings _textSettings;

		// Token: 0x04001DF1 RID: 7665
		[SerializeField]
		private ThemeSettings.TextSettings _buttonTextSettings;

		// Token: 0x04001DF2 RID: 7666
		[SerializeField]
		private ThemeSettings.TextSettings _inputGridFieldTextSettings;

		// Token: 0x02000557 RID: 1367
		[Serializable]
		private abstract class SelectableSettings_Base
		{
			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x000AAB10 File Offset: 0x000A8F10
			public Selectable.Transition transition
			{
				get
				{
					return this._transition;
				}
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000AAB18 File Offset: 0x000A8F18
			public ThemeSettings.CustomColorBlock selectableColors
			{
				get
				{
					return this._colors;
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x000AAB20 File Offset: 0x000A8F20
			public ThemeSettings.CustomSpriteState spriteState
			{
				get
				{
					return this._spriteState;
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000AAB28 File Offset: 0x000A8F28
			public ThemeSettings.CustomAnimationTriggers animationTriggers
			{
				get
				{
					return this._animationTriggers;
				}
			}

			// Token: 0x06001CBB RID: 7355 RVA: 0x000AAB30 File Offset: 0x000A8F30
			public virtual void Apply(Selectable item)
			{
				Selectable.Transition transition = this._transition;
				bool flag = item.transition != transition;
				item.transition = transition;
				ICustomSelectable customSelectable = item as ICustomSelectable;
				if (transition == Selectable.Transition.ColorTint)
				{
					ThemeSettings.CustomColorBlock colors = this._colors;
					colors.fadeDuration = 0f;
					item.colors = colors;
					colors.fadeDuration = this._colors.fadeDuration;
					item.colors = colors;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedColor = colors.disabledHighlightedColor;
					}
				}
				else if (transition == Selectable.Transition.SpriteSwap)
				{
					item.spriteState = this._spriteState;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedSprite = this._spriteState.disabledHighlightedSprite;
					}
				}
				else if (transition == Selectable.Transition.Animation)
				{
					item.animationTriggers.disabledTrigger = this._animationTriggers.disabledTrigger;
					item.animationTriggers.highlightedTrigger = this._animationTriggers.highlightedTrigger;
					item.animationTriggers.normalTrigger = this._animationTriggers.normalTrigger;
					item.animationTriggers.pressedTrigger = this._animationTriggers.pressedTrigger;
					if (customSelectable != null)
					{
						customSelectable.disabledHighlightedTrigger = this._animationTriggers.disabledHighlightedTrigger;
					}
				}
				if (flag)
				{
					item.targetGraphic.CrossFadeColor(item.targetGraphic.color, 0f, true, true);
				}
			}

			// Token: 0x04001DF4 RID: 7668
			[SerializeField]
			protected Selectable.Transition _transition;

			// Token: 0x04001DF5 RID: 7669
			[SerializeField]
			protected ThemeSettings.CustomColorBlock _colors;

			// Token: 0x04001DF6 RID: 7670
			[SerializeField]
			protected ThemeSettings.CustomSpriteState _spriteState;

			// Token: 0x04001DF7 RID: 7671
			[SerializeField]
			protected ThemeSettings.CustomAnimationTriggers _animationTriggers;
		}

		// Token: 0x02000558 RID: 1368
		[Serializable]
		private class SelectableSettings : ThemeSettings.SelectableSettings_Base
		{
			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06001CBD RID: 7357 RVA: 0x000AAC90 File Offset: 0x000A9090
			public ThemeSettings.ImageSettings imageSettings
			{
				get
				{
					return this._imageSettings;
				}
			}

			// Token: 0x06001CBE RID: 7358 RVA: 0x000AAC98 File Offset: 0x000A9098
			public override void Apply(Selectable item)
			{
				if (item == null)
				{
					return;
				}
				base.Apply(item);
				if (this._imageSettings != null)
				{
					this._imageSettings.CopyTo(item.targetGraphic as Image);
				}
			}

			// Token: 0x04001DF8 RID: 7672
			[SerializeField]
			private ThemeSettings.ImageSettings _imageSettings;
		}

		// Token: 0x02000559 RID: 1369
		[Serializable]
		private class SliderSettings : ThemeSettings.SelectableSettings_Base
		{
			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x000AACD7 File Offset: 0x000A90D7
			public ThemeSettings.ImageSettings handleImageSettings
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x000AACDF File Offset: 0x000A90DF
			public ThemeSettings.ImageSettings fillImageSettings
			{
				get
				{
					return this._fillImageSettings;
				}
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x000AACE7 File Offset: 0x000A90E7
			public ThemeSettings.ImageSettings backgroundImageSettings
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001CC3 RID: 7363 RVA: 0x000AACF0 File Offset: 0x000A90F0
			private void Apply(Slider item)
			{
				if (item == null)
				{
					return;
				}
				if (this._handleImageSettings != null)
				{
					this._handleImageSettings.CopyTo(item.targetGraphic as Image);
				}
				if (this._fillImageSettings != null)
				{
					RectTransform fillRect = item.fillRect;
					if (fillRect != null)
					{
						this._fillImageSettings.CopyTo(fillRect.GetComponent<Image>());
					}
				}
				if (this._backgroundImageSettings != null)
				{
					Transform transform = item.transform.Find("Background");
					if (transform != null)
					{
						this._backgroundImageSettings.CopyTo(transform.GetComponent<Image>());
					}
				}
			}

			// Token: 0x06001CC4 RID: 7364 RVA: 0x000AAD93 File Offset: 0x000A9193
			public override void Apply(Selectable item)
			{
				base.Apply(item);
				this.Apply(item as Slider);
			}

			// Token: 0x04001DF9 RID: 7673
			[SerializeField]
			private ThemeSettings.ImageSettings _handleImageSettings;

			// Token: 0x04001DFA RID: 7674
			[SerializeField]
			private ThemeSettings.ImageSettings _fillImageSettings;

			// Token: 0x04001DFB RID: 7675
			[SerializeField]
			private ThemeSettings.ImageSettings _backgroundImageSettings;
		}

		// Token: 0x0200055A RID: 1370
		[Serializable]
		private class ScrollbarSettings : ThemeSettings.SelectableSettings_Base
		{
			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x000AADB0 File Offset: 0x000A91B0
			public ThemeSettings.ImageSettings handle
			{
				get
				{
					return this._handleImageSettings;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x000AADB8 File Offset: 0x000A91B8
			public ThemeSettings.ImageSettings background
			{
				get
				{
					return this._backgroundImageSettings;
				}
			}

			// Token: 0x06001CC8 RID: 7368 RVA: 0x000AADC0 File Offset: 0x000A91C0
			private void Apply(Scrollbar item)
			{
				if (item == null)
				{
					return;
				}
				if (this._handleImageSettings != null)
				{
					this._handleImageSettings.CopyTo(item.targetGraphic as Image);
				}
				if (this._backgroundImageSettings != null)
				{
					this._backgroundImageSettings.CopyTo(item.GetComponent<Image>());
				}
			}

			// Token: 0x06001CC9 RID: 7369 RVA: 0x000AAE17 File Offset: 0x000A9217
			public override void Apply(Selectable item)
			{
				base.Apply(item);
				this.Apply(item as Scrollbar);
			}

			// Token: 0x04001DFC RID: 7676
			[SerializeField]
			private ThemeSettings.ImageSettings _handleImageSettings;

			// Token: 0x04001DFD RID: 7677
			[SerializeField]
			private ThemeSettings.ImageSettings _backgroundImageSettings;
		}

		// Token: 0x0200055B RID: 1371
		[Serializable]
		private class ImageSettings
		{
			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06001CCB RID: 7371 RVA: 0x000AAE3F File Offset: 0x000A923F
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000AAE47 File Offset: 0x000A9247
			public Sprite sprite
			{
				get
				{
					return this._sprite;
				}
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06001CCD RID: 7373 RVA: 0x000AAE4F File Offset: 0x000A924F
			public Material materal
			{
				get
				{
					return this._materal;
				}
			}

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06001CCE RID: 7374 RVA: 0x000AAE57 File Offset: 0x000A9257
			public Image.Type type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000AAE5F File Offset: 0x000A925F
			public bool preserveAspect
			{
				get
				{
					return this._preserveAspect;
				}
			}

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x000AAE67 File Offset: 0x000A9267
			public bool fillCenter
			{
				get
				{
					return this._fillCenter;
				}
			}

			// Token: 0x17000293 RID: 659
			// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x000AAE6F File Offset: 0x000A926F
			public Image.FillMethod fillMethod
			{
				get
				{
					return this._fillMethod;
				}
			}

			// Token: 0x17000294 RID: 660
			// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x000AAE77 File Offset: 0x000A9277
			public float fillAmout
			{
				get
				{
					return this._fillAmout;
				}
			}

			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x000AAE7F File Offset: 0x000A927F
			public bool fillClockwise
			{
				get
				{
					return this._fillClockwise;
				}
			}

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x000AAE87 File Offset: 0x000A9287
			public int fillOrigin
			{
				get
				{
					return this._fillOrigin;
				}
			}

			// Token: 0x06001CD5 RID: 7381 RVA: 0x000AAE90 File Offset: 0x000A9290
			public virtual void CopyTo(Image image)
			{
				if (image == null)
				{
					return;
				}
				image.color = this._color;
				image.sprite = this._sprite;
				image.material = this._materal;
				image.type = this._type;
				image.preserveAspect = this._preserveAspect;
				image.fillCenter = this._fillCenter;
				image.fillMethod = this._fillMethod;
				image.fillAmount = this._fillAmout;
				image.fillClockwise = this._fillClockwise;
				image.fillOrigin = this._fillOrigin;
			}

			// Token: 0x04001DFE RID: 7678
			[SerializeField]
			private Color _color = Color.white;

			// Token: 0x04001DFF RID: 7679
			[SerializeField]
			private Sprite _sprite;

			// Token: 0x04001E00 RID: 7680
			[SerializeField]
			private Material _materal;

			// Token: 0x04001E01 RID: 7681
			[SerializeField]
			private Image.Type _type;

			// Token: 0x04001E02 RID: 7682
			[SerializeField]
			private bool _preserveAspect;

			// Token: 0x04001E03 RID: 7683
			[SerializeField]
			private bool _fillCenter;

			// Token: 0x04001E04 RID: 7684
			[SerializeField]
			private Image.FillMethod _fillMethod;

			// Token: 0x04001E05 RID: 7685
			[SerializeField]
			private float _fillAmout;

			// Token: 0x04001E06 RID: 7686
			[SerializeField]
			private bool _fillClockwise;

			// Token: 0x04001E07 RID: 7687
			[SerializeField]
			private int _fillOrigin;
		}

		// Token: 0x0200055C RID: 1372
		[Serializable]
		private struct CustomColorBlock
		{
			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x000AAF22 File Offset: 0x000A9322
			// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x000AAF2A File Offset: 0x000A932A
			public float colorMultiplier
			{
				get
				{
					return this.m_ColorMultiplier;
				}
				set
				{
					this.m_ColorMultiplier = value;
				}
			}

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x000AAF33 File Offset: 0x000A9333
			// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x000AAF3B File Offset: 0x000A933B
			public Color disabledColor
			{
				get
				{
					return this.m_DisabledColor;
				}
				set
				{
					this.m_DisabledColor = value;
				}
			}

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000AAF44 File Offset: 0x000A9344
			// (set) Token: 0x06001CDB RID: 7387 RVA: 0x000AAF4C File Offset: 0x000A934C
			public float fadeDuration
			{
				get
				{
					return this.m_FadeDuration;
				}
				set
				{
					this.m_FadeDuration = value;
				}
			}

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06001CDC RID: 7388 RVA: 0x000AAF55 File Offset: 0x000A9355
			// (set) Token: 0x06001CDD RID: 7389 RVA: 0x000AAF5D File Offset: 0x000A935D
			public Color highlightedColor
			{
				get
				{
					return this.m_HighlightedColor;
				}
				set
				{
					this.m_HighlightedColor = value;
				}
			}

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000AAF66 File Offset: 0x000A9366
			// (set) Token: 0x06001CDF RID: 7391 RVA: 0x000AAF6E File Offset: 0x000A936E
			public Color normalColor
			{
				get
				{
					return this.m_NormalColor;
				}
				set
				{
					this.m_NormalColor = value;
				}
			}

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x000AAF77 File Offset: 0x000A9377
			// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x000AAF7F File Offset: 0x000A937F
			public Color pressedColor
			{
				get
				{
					return this.m_PressedColor;
				}
				set
				{
					this.m_PressedColor = value;
				}
			}

			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x000AAF88 File Offset: 0x000A9388
			// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x000AAF90 File Offset: 0x000A9390
			public Color disabledHighlightedColor
			{
				get
				{
					return this.m_DisabledHighlightedColor;
				}
				set
				{
					this.m_DisabledHighlightedColor = value;
				}
			}

			// Token: 0x06001CE4 RID: 7396 RVA: 0x000AAF9C File Offset: 0x000A939C
			public static implicit operator ColorBlock(ThemeSettings.CustomColorBlock item)
			{
				return new ColorBlock
				{
					colorMultiplier = item.m_ColorMultiplier,
					disabledColor = item.m_DisabledColor,
					fadeDuration = item.m_FadeDuration,
					highlightedColor = item.m_HighlightedColor,
					normalColor = item.m_NormalColor,
					pressedColor = item.m_PressedColor
				};
			}

			// Token: 0x04001E08 RID: 7688
			[SerializeField]
			private float m_ColorMultiplier;

			// Token: 0x04001E09 RID: 7689
			[SerializeField]
			private Color m_DisabledColor;

			// Token: 0x04001E0A RID: 7690
			[SerializeField]
			private float m_FadeDuration;

			// Token: 0x04001E0B RID: 7691
			[SerializeField]
			private Color m_HighlightedColor;

			// Token: 0x04001E0C RID: 7692
			[SerializeField]
			private Color m_NormalColor;

			// Token: 0x04001E0D RID: 7693
			[SerializeField]
			private Color m_PressedColor;

			// Token: 0x04001E0E RID: 7694
			[SerializeField]
			private Color m_DisabledHighlightedColor;
		}

		// Token: 0x0200055D RID: 1373
		[Serializable]
		private struct CustomSpriteState
		{
			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x000AB006 File Offset: 0x000A9406
			// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x000AB00E File Offset: 0x000A940E
			public Sprite disabledSprite
			{
				get
				{
					return this.m_DisabledSprite;
				}
				set
				{
					this.m_DisabledSprite = value;
				}
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x000AB017 File Offset: 0x000A9417
			// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x000AB01F File Offset: 0x000A941F
			public Sprite highlightedSprite
			{
				get
				{
					return this.m_HighlightedSprite;
				}
				set
				{
					this.m_HighlightedSprite = value;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000AB028 File Offset: 0x000A9428
			// (set) Token: 0x06001CEA RID: 7402 RVA: 0x000AB030 File Offset: 0x000A9430
			public Sprite pressedSprite
			{
				get
				{
					return this.m_PressedSprite;
				}
				set
				{
					this.m_PressedSprite = value;
				}
			}

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000AB039 File Offset: 0x000A9439
			// (set) Token: 0x06001CEC RID: 7404 RVA: 0x000AB041 File Offset: 0x000A9441
			public Sprite disabledHighlightedSprite
			{
				get
				{
					return this.m_DisabledHighlightedSprite;
				}
				set
				{
					this.m_DisabledHighlightedSprite = value;
				}
			}

			// Token: 0x06001CED RID: 7405 RVA: 0x000AB04C File Offset: 0x000A944C
			public static implicit operator SpriteState(ThemeSettings.CustomSpriteState item)
			{
				return new SpriteState
				{
					disabledSprite = item.m_DisabledSprite,
					highlightedSprite = item.m_HighlightedSprite,
					pressedSprite = item.m_PressedSprite
				};
			}

			// Token: 0x04001E0F RID: 7695
			[SerializeField]
			private Sprite m_DisabledSprite;

			// Token: 0x04001E10 RID: 7696
			[SerializeField]
			private Sprite m_HighlightedSprite;

			// Token: 0x04001E11 RID: 7697
			[SerializeField]
			private Sprite m_PressedSprite;

			// Token: 0x04001E12 RID: 7698
			[SerializeField]
			private Sprite m_DisabledHighlightedSprite;
		}

		// Token: 0x0200055E RID: 1374
		[Serializable]
		private class CustomAnimationTriggers
		{
			// Token: 0x06001CEE RID: 7406 RVA: 0x000AB08C File Offset: 0x000A948C
			public CustomAnimationTriggers()
			{
				this.m_DisabledTrigger = string.Empty;
				this.m_HighlightedTrigger = string.Empty;
				this.m_NormalTrigger = string.Empty;
				this.m_PressedTrigger = string.Empty;
				this.m_DisabledHighlightedTrigger = string.Empty;
			}

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000AB0CB File Offset: 0x000A94CB
			// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x000AB0D3 File Offset: 0x000A94D3
			public string disabledTrigger
			{
				get
				{
					return this.m_DisabledTrigger;
				}
				set
				{
					this.m_DisabledTrigger = value;
				}
			}

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x000AB0DC File Offset: 0x000A94DC
			// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x000AB0E4 File Offset: 0x000A94E4
			public string highlightedTrigger
			{
				get
				{
					return this.m_HighlightedTrigger;
				}
				set
				{
					this.m_HighlightedTrigger = value;
				}
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x000AB0ED File Offset: 0x000A94ED
			// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x000AB0F5 File Offset: 0x000A94F5
			public string normalTrigger
			{
				get
				{
					return this.m_NormalTrigger;
				}
				set
				{
					this.m_NormalTrigger = value;
				}
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x000AB0FE File Offset: 0x000A94FE
			// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x000AB106 File Offset: 0x000A9506
			public string pressedTrigger
			{
				get
				{
					return this.m_PressedTrigger;
				}
				set
				{
					this.m_PressedTrigger = value;
				}
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x000AB10F File Offset: 0x000A950F
			// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x000AB117 File Offset: 0x000A9517
			public string disabledHighlightedTrigger
			{
				get
				{
					return this.m_DisabledHighlightedTrigger;
				}
				set
				{
					this.m_DisabledHighlightedTrigger = value;
				}
			}

			// Token: 0x06001CF9 RID: 7417 RVA: 0x000AB120 File Offset: 0x000A9520
			public static implicit operator AnimationTriggers(ThemeSettings.CustomAnimationTriggers item)
			{
				return new AnimationTriggers
				{
					disabledTrigger = item.m_DisabledTrigger,
					highlightedTrigger = item.m_HighlightedTrigger,
					normalTrigger = item.m_NormalTrigger,
					pressedTrigger = item.m_PressedTrigger
				};
			}

			// Token: 0x04001E13 RID: 7699
			[SerializeField]
			private string m_DisabledTrigger;

			// Token: 0x04001E14 RID: 7700
			[SerializeField]
			private string m_HighlightedTrigger;

			// Token: 0x04001E15 RID: 7701
			[SerializeField]
			private string m_NormalTrigger;

			// Token: 0x04001E16 RID: 7702
			[SerializeField]
			private string m_PressedTrigger;

			// Token: 0x04001E17 RID: 7703
			[SerializeField]
			private string m_DisabledHighlightedTrigger;
		}

		// Token: 0x0200055F RID: 1375
		[Serializable]
		private class TextSettings
		{
			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06001CFB RID: 7419 RVA: 0x000AB18D File Offset: 0x000A958D
			public Color color
			{
				get
				{
					return this._color;
				}
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06001CFC RID: 7420 RVA: 0x000AB195 File Offset: 0x000A9595
			public Font font
			{
				get
				{
					return this._font;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06001CFD RID: 7421 RVA: 0x000AB19D File Offset: 0x000A959D
			public ThemeSettings.FontStyleOverride style
			{
				get
				{
					return this._style;
				}
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06001CFE RID: 7422 RVA: 0x000AB1A5 File Offset: 0x000A95A5
			public float lineSpacing
			{
				get
				{
					return this._lineSpacing;
				}
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06001CFF RID: 7423 RVA: 0x000AB1AD File Offset: 0x000A95AD
			public float sizeMultiplier
			{
				get
				{
					return this._sizeMultiplier;
				}
			}

			// Token: 0x04001E18 RID: 7704
			[SerializeField]
			private Color _color = Color.white;

			// Token: 0x04001E19 RID: 7705
			[SerializeField]
			private Font _font;

			// Token: 0x04001E1A RID: 7706
			[SerializeField]
			private ThemeSettings.FontStyleOverride _style;

			// Token: 0x04001E1B RID: 7707
			[SerializeField]
			private float _lineSpacing = 1f;

			// Token: 0x04001E1C RID: 7708
			[SerializeField]
			private float _sizeMultiplier = 1f;
		}

		// Token: 0x02000560 RID: 1376
		private enum FontStyleOverride
		{
			// Token: 0x04001E1E RID: 7710
			Default,
			// Token: 0x04001E1F RID: 7711
			Normal,
			// Token: 0x04001E20 RID: 7712
			Bold,
			// Token: 0x04001E21 RID: 7713
			Italic,
			// Token: 0x04001E22 RID: 7714
			BoldAndItalic
		}
	}
}
