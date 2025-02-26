using System;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000E51 RID: 3665
	public class CommandLabelOption : MonoBehaviour
	{
		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x0600739F RID: 29599 RVA: 0x00313639 File Offset: 0x00311A39
		// (set) Token: 0x060073A0 RID: 29600 RVA: 0x00313641 File Offset: 0x00311A41
		public CommandLabel.LabelPool PoolSource { get; set; }

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x060073A1 RID: 29601 RVA: 0x0031364A File Offset: 0x00311A4A
		// (set) Token: 0x060073A2 RID: 29602 RVA: 0x00313654 File Offset: 0x00311A54
		public CommandLabel.CommandInfo CommandInfo
		{
			get
			{
				return this._commandInfo;
			}
			set
			{
				this._commandInfo = value;
				if (value == null)
				{
					return;
				}
				if (this._textComponent != null)
				{
					if (value.OnText != null)
					{
						this._textComponent.text = value.OnText();
					}
					else
					{
						this._textComponent.text = value.Text;
					}
				}
				CommandTargetSpriteInfo targetSpriteInfo = value.TargetSpriteInfo;
				if (targetSpriteInfo != null)
				{
					ImageSpriteSheet targetImageSS = this._targetImageSS;
					float fps = targetSpriteInfo.FPS;
					this._targetSelectedImageSS.FPS = fps;
					targetImageSS.FPS = fps;
					this._targetImageSS.Sprites = targetSpriteInfo.Sprites;
					this._targetSelectedImageSS.Sprites = targetSpriteInfo.SelectedSprites;
				}
				if (this._targetImage != null)
				{
					this._targetImage.gameObject.SetActive(targetSpriteInfo != null && !targetSpriteInfo.Sprites.IsNullOrEmpty<Sprite>());
				}
				if (this._targetSelectedImage != null)
				{
					this._targetSelectedImage.gameObject.SetActive(targetSpriteInfo != null && !targetSpriteInfo.SelectedSprites.IsNullOrEmpty<Sprite>());
				}
				if (this._progressBackground != null)
				{
					this._progressBackground.alpha = (float)((!value.IsHold) ? 0 : 1);
				}
				if (this._progressCanvasGroup != null)
				{
					this._progressCanvasGroup.alpha = (float)((!value.IsHold) ? 0 : 1);
				}
				if (this._icon != null)
				{
					this._icon.sprite = value.Icon;
					this._icon.gameObject.SetActive(value.Icon != null);
				}
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x060073A3 RID: 29603 RVA: 0x00313811 File Offset: 0x00311C11
		// (set) Token: 0x060073A4 RID: 29604 RVA: 0x0031381C File Offset: 0x00311C1C
		public int Visibility
		{
			get
			{
				return this._visibility;
			}
			set
			{
				if (this._visibility == value)
				{
					return;
				}
				this._visibility = value;
				if (this._disposable != null)
				{
					this._disposable.Dispose();
				}
				float startValueA = this._canvasGroup.alpha;
				float startValueB = (!(this._labelCanvasGroup != null)) ? 0f : this._labelCanvasGroup.alpha;
				float startValueC = (!(this._targetCanvasGroup != null)) ? 0f : this._targetCanvasGroup.alpha;
				float startValueD = (!(this._targetSelectedCanvasGroup != null)) ? 0f : this._targetSelectedCanvasGroup.alpha;
				IObservable<TimeInterval<float>> source = ObservableEasing.EaseOutQuint(0.3f, false).TakeUntilDestroy(this._canvasGroup.gameObject).FrameTimeInterval(false);
				if (value == 0)
				{
					this._disposable = source.Subscribe(delegate(TimeInterval<float> x)
					{
						this._canvasGroup.alpha = Mathf.Lerp(startValueA, 0f, x.Value);
						if (this._labelCanvasGroup != null)
						{
							this._labelCanvasGroup.alpha = Mathf.Lerp(startValueB, 0f, x.Value);
						}
						if (this._targetCanvasGroup != null)
						{
							this._targetCanvasGroup.alpha = Mathf.Lerp(startValueC, 0f, x.Value);
						}
						if (this._targetSelectedCanvasGroup != null)
						{
							this._targetSelectedCanvasGroup.alpha = Mathf.Lerp(startValueD, 0f, x.Value);
						}
						if (this._panelCanvasGroup != null)
						{
							this._panelCanvasGroup.alpha = Mathf.Lerp(startValueB, 0f, x.Value);
						}
						if (this._iconCanvasGroup != null)
						{
							this._iconCanvasGroup.alpha = Mathf.Lerp(startValueB, 0f, x.Value);
						}
					}, delegate(Exception ex)
					{
					}, delegate()
					{
						if (this._targetImage != null && this._targetImage.gameObject.activeSelf)
						{
							this._targetImage.gameObject.SetActive(false);
						}
						if (this.PoolSource != null)
						{
							this.PoolSource.Return(this);
						}
						else
						{
							this.gameObject.SetActive(false);
						}
						this.SetActiveText(false);
					});
				}
				else if (value == 1)
				{
					if (this._targetImage != null && !this._targetImage.gameObject.activeSelf && this._commandInfo.Transform != null)
					{
						this._targetImage.gameObject.SetActive(true);
					}
					if (this._targetImage != null)
					{
						this._targetImage.enabled = true;
					}
					this._disposable = source.Subscribe(delegate(TimeInterval<float> x)
					{
						this._canvasGroup.alpha = Mathf.Lerp(startValueA, 1f, x.Value);
						if (this._labelCanvasGroup != null)
						{
							this._labelCanvasGroup.alpha = Mathf.Lerp(startValueB, 0f, x.Value);
						}
						if (this._targetCanvasGroup != null)
						{
							this._targetCanvasGroup.alpha = Mathf.Lerp(startValueC, 1f, x.Value);
						}
						if (this._targetSelectedCanvasGroup != null)
						{
							this._targetSelectedCanvasGroup.alpha = Mathf.Lerp(startValueD, 0f, x.Value);
						}
						if (this._panelCanvasGroup != null)
						{
							this._panelCanvasGroup.alpha = Mathf.Lerp(startValueB, 0f, x.Value);
						}
						if (this._iconCanvasGroup != null)
						{
							this._iconCanvasGroup.alpha = Mathf.Lerp(startValueB, 0f, x.Value);
						}
					}, delegate(Exception ex)
					{
					}, delegate()
					{
						this.SetActiveText(false);
					});
				}
				else if (value == 2)
				{
					base.transform.SetAsLastSibling();
					this.SetActiveText(true);
					if (this._targetImage != null && !this._targetImage.gameObject.activeSelf && this._commandInfo.Transform != null)
					{
						this._targetImage.gameObject.SetActive(true);
					}
					if (this._targetImage != null)
					{
						this._targetImage.enabled = false;
					}
					this._disposable = source.Subscribe(delegate(TimeInterval<float> x)
					{
						this._canvasGroup.alpha = Mathf.Lerp(startValueA, 1f, x.Value);
						if (this._labelCanvasGroup != null)
						{
							this._labelCanvasGroup.alpha = Mathf.Lerp(startValueB, 1f, x.Value);
						}
						if (this._targetCanvasGroup != null)
						{
							this._targetCanvasGroup.alpha = Mathf.Lerp(startValueC, 1f, x.Value);
						}
						if (this._targetSelectedCanvasGroup != null)
						{
							this._targetSelectedCanvasGroup.alpha = Mathf.Lerp(startValueD, 1f, x.Value);
						}
						if (this._panelCanvasGroup != null)
						{
							this._panelCanvasGroup.alpha = Mathf.Lerp(startValueB, 1f, x.Value);
						}
						if (this._iconCanvasGroup != null)
						{
							this._iconCanvasGroup.alpha = Mathf.Lerp(startValueB, 1f, x.Value);
						}
					});
				}
				if (value > 0)
				{
					if (this._commandInfo.Transform != null)
					{
						if (Singleton<Map>.IsInstance())
						{
							Vector2 v = RectTransformUtility.WorldToScreenPoint(Singleton<Map>.Instance.Player.CameraControl.CameraComponent, this._commandInfo.Transform.position);
							base.transform.position = v;
						}
						else
						{
							base.transform.position = Vector3.zero;
						}
					}
					else
					{
						(base.transform as RectTransform).anchoredPosition = Vector2.zero;
					}
				}
			}
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x00313B46 File Offset: 0x00311F46
		private void Start()
		{
			this._canvasBaseRect = (this._icon.canvas.transform as RectTransform).rect;
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x00313B68 File Offset: 0x00311F68
		private void OnEnable()
		{
			if (this._panel != null)
			{
				this._panel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._rect.sizeDelta.x);
			}
		}

		// Token: 0x060073A7 RID: 29607 RVA: 0x00313BAC File Offset: 0x00311FAC
		private void LateUpdate()
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			if (this._panel != null)
			{
				float x = this._rect.sizeDelta.x;
				float x2 = this._panel.rectTransform.sizeDelta.x;
				float size = Mathf.SmoothDamp(x2, x, ref this._smoothVelocity, 0.3f, float.PositiveInfinity, unscaledDeltaTime);
				this._panel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			}
			if (this._icon != null && this._iconRect != null)
			{
				this._icon.transform.position = Vector2.SmoothDamp(this._icon.transform.position, this._iconRect.position, ref this._smoothVelocityVector, 0.3f, float.PositiveInfinity, unscaledDeltaTime);
			}
			if (this._commandInfo != null)
			{
				CommandTargetSpriteInfo targetSpriteInfo = this._commandInfo.TargetSpriteInfo;
				Func<float> coolTimeFillRate = this._commandInfo.CoolTimeFillRate;
				float? num = (coolTimeFillRate != null) ? new float?(coolTimeFillRate()) : null;
				float num2 = (num == null) ? 0f : num.Value;
				bool flag = num2 > 0f;
				Func<PlayerActor, bool> condition = this._commandInfo.Condition;
				bool? flag2 = (condition != null) ? new bool?(condition(Singleton<Map>.Instance.Player)) : null;
				if (flag2 == null || flag2.Value)
				{
					this._textComponent.color = new Color32(235, 225, 215, byte.MaxValue);
					if (targetSpriteInfo != null)
					{
						if (this._targetImageSS.Sprites != targetSpriteInfo.Sprites)
						{
							this._targetImageSS.Sprites = targetSpriteInfo.Sprites;
						}
						if (this._targetSelectedImageSS.Sprites != targetSpriteInfo.SelectedSprites)
						{
							this._targetSelectedImageSS.Sprites = targetSpriteInfo.SelectedSprites;
						}
					}
				}
				else
				{
					this._textComponent.color = new Color32(235, 225, 215, 127);
					if (targetSpriteInfo != null)
					{
						if (flag)
						{
							if (this._targetImageSS.Sprites != targetSpriteInfo.CoolTimeSprites)
							{
								this._targetImageSS.Sprites = targetSpriteInfo.CoolTimeSprites;
							}
							if (this._targetSelectedImageSS.Sprites != targetSpriteInfo.CoolTimeSprites)
							{
								this._targetSelectedImageSS.Sprites = targetSpriteInfo.CoolTimeSprites;
							}
						}
						else
						{
							if (this._targetImageSS.Sprites != targetSpriteInfo.DisableSprites)
							{
								this._targetImageSS.Sprites = targetSpriteInfo.DisableSprites;
							}
							if (this._targetSelectedImageSS.Sprites != targetSpriteInfo.DisableSprites)
							{
								this._targetSelectedImageSS.Sprites = targetSpriteInfo.DisableSprites;
							}
						}
					}
				}
				if (this._progressImage != null)
				{
					this._progressImage.fillAmount = this._commandInfo.FillRate;
				}
				if (this._coolTimeImage != null)
				{
					if (this._coolTimeImage.enabled != flag)
					{
						this._coolTimeImage.enabled = flag;
					}
					if (flag)
					{
						this._coolTimeImage.fillAmount = num2;
					}
				}
				if (this._commandInfo.Transform != null)
				{
					Camera cameraComponent = Singleton<Map>.Instance.Player.CameraControl.CameraComponent;
					Vector3 v = cameraComponent.WorldToScreenPoint(this._commandInfo.Transform.position);
					RectTransform rectTransform = this._targetImage.rectTransform;
					Rect canvasBaseRect = this._canvasBaseRect;
					canvasBaseRect.x = rectTransform.rect.width * 0.5f + CommandLabelOption._sizeOffset.x;
					canvasBaseRect.y = CommandLabelOption._sizeOffset.y;
					canvasBaseRect.width -= CommandLabelOption._sizeOffset.x + this._labelDisplayOffset.x + this._rect.rect.width;
					canvasBaseRect.height -= CommandLabelOption._sizeOffset.y + this._labelDisplayOffset.y + this._rect.rect.height;
					Vector3 position = cameraComponent.transform.position;
					position.y = 0f;
					Vector3 position2 = this._commandInfo.Transform.position;
					position2.y = 0f;
					Quaternion a = Quaternion.LookRotation(position2 - position);
					float num3 = Quaternion.Angle(a, cameraComponent.transform.rotation);
					if (num3 < 120f)
					{
						Vector2 vector = v;
						vector.x = Mathf.Clamp(vector.x, canvasBaseRect.x, canvasBaseRect.width);
						vector.y = Mathf.Clamp(vector.y, canvasBaseRect.y, canvasBaseRect.height);
						rectTransform.position = vector;
						this._labelRect.position = vector + this._labelDisplayOffset;
					}
				}
			}
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x00314124 File Offset: 0x00312524
		private void SetActiveText(bool active)
		{
			if (this._panel != null && this._panel.gameObject.activeSelf != active)
			{
				this._panel.gameObject.SetActive(active);
			}
			if (active)
			{
				bool flag = this._commandInfo.Icon != null;
				if (this._icon.gameObject.activeSelf != flag)
				{
					this._icon.gameObject.SetActive(flag);
				}
			}
			else if (this._icon.gameObject.activeSelf)
			{
				this._icon.gameObject.SetActive(false);
			}
			if (this._textComponent.gameObject.activeSelf != active)
			{
				this._textComponent.gameObject.SetActive(active);
			}
			foreach (GameObject gameObject in this._blankSpaceRects)
			{
				if (gameObject.gameObject.activeSelf != active)
				{
					gameObject.SetActive(active);
				}
			}
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x00314231 File Offset: 0x00312631
		private void Reset()
		{
			this._rect = (base.transform as RectTransform);
		}

		// Token: 0x04005E81 RID: 24193
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005E82 RID: 24194
		[SerializeField]
		private CanvasGroup _targetCanvasGroup;

		// Token: 0x04005E83 RID: 24195
		[SerializeField]
		private CanvasGroup _targetSelectedCanvasGroup;

		// Token: 0x04005E84 RID: 24196
		[SerializeField]
		private Image _targetImage;

		// Token: 0x04005E85 RID: 24197
		[SerializeField]
		private Image _targetSelectedImage;

		// Token: 0x04005E86 RID: 24198
		[SerializeField]
		private ImageSpriteSheet _targetImageSS;

		// Token: 0x04005E87 RID: 24199
		[SerializeField]
		private ImageSpriteSheet _targetSelectedImageSS;

		// Token: 0x04005E88 RID: 24200
		[SerializeField]
		private CanvasGroup _progressBackground;

		// Token: 0x04005E89 RID: 24201
		[SerializeField]
		private CanvasGroup _progressCanvasGroup;

		// Token: 0x04005E8A RID: 24202
		[SerializeField]
		private Image _progressImage;

		// Token: 0x04005E8B RID: 24203
		[SerializeField]
		private Image _coolTimeImage;

		// Token: 0x04005E8C RID: 24204
		[SerializeField]
		private RectTransform _labelRect;

		// Token: 0x04005E8D RID: 24205
		[SerializeField]
		private RectTransform _rect;

		// Token: 0x04005E8E RID: 24206
		[SerializeField]
		private Image _panel;

		// Token: 0x04005E8F RID: 24207
		[SerializeField]
		private Text _textComponent;

		// Token: 0x04005E90 RID: 24208
		[SerializeField]
		private GameObject[] _blankSpaceRects;

		// Token: 0x04005E91 RID: 24209
		[SerializeField]
		private RectTransform _iconRect;

		// Token: 0x04005E92 RID: 24210
		[SerializeField]
		private Image _icon;

		// Token: 0x04005E93 RID: 24211
		[SerializeField]
		private CanvasGroup _panelCanvasGroup;

		// Token: 0x04005E94 RID: 24212
		[SerializeField]
		private CanvasGroup _iconCanvasGroup;

		// Token: 0x04005E95 RID: 24213
		[SerializeField]
		private CanvasGroup _labelCanvasGroup;

		// Token: 0x04005E96 RID: 24214
		[SerializeField]
		private Vector2 _labelDisplayOffset = Vector3.zero;

		// Token: 0x04005E98 RID: 24216
		private CommandLabel.CommandInfo _commandInfo;

		// Token: 0x04005E99 RID: 24217
		private static readonly Vector2 _sizeOffset = new Vector2(20f, 20f);

		// Token: 0x04005E9A RID: 24218
		private const float _fadeTime = 0.3f;

		// Token: 0x04005E9B RID: 24219
		private Rect _canvasBaseRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04005E9C RID: 24220
		private int _visibility;

		// Token: 0x04005E9D RID: 24221
		private IDisposable _disposable;

		// Token: 0x04005E9E RID: 24222
		private Vector2 _smoothVelocityVector = Vector2.zero;

		// Token: 0x04005E9F RID: 24223
		private float _smoothVelocity;
	}
}
