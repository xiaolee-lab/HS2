using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ConfigScene;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E30 RID: 3632
	public class CaptionSystem : MonoBehaviour
	{
		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x060071A2 RID: 29090 RVA: 0x003069D9 File Offset: 0x00304DD9
		// (set) Token: 0x060071A3 RID: 29091 RVA: 0x003069E6 File Offset: 0x00304DE6
		public int FontSpeed
		{
			get
			{
				return this._fontSpeed.Value;
			}
			set
			{
				this._fontSpeed.Value = Mathf.Clamp(value, 1, 100);
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x060071A4 RID: 29092 RVA: 0x003069FC File Offset: 0x00304DFC
		// (set) Token: 0x060071A5 RID: 29093 RVA: 0x00306A09 File Offset: 0x00304E09
		public bool Visible
		{
			get
			{
				return this._advWindowRootVisible.Value;
			}
			set
			{
				this._advWindowRootVisible.Value = value;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x060071A6 RID: 29094 RVA: 0x00306A17 File Offset: 0x00304E17
		private BoolReactiveProperty _advWindowRootVisible { get; } = new BoolReactiveProperty(true);

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x060071A7 RID: 29095 RVA: 0x00306A1F File Offset: 0x00304E1F
		private BoolReactiveProperty _advWindowVisible { get; } = new BoolReactiveProperty(true);

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x060071A8 RID: 29096 RVA: 0x00306A27 File Offset: 0x00304E27
		// (set) Token: 0x060071A9 RID: 29097 RVA: 0x00306A2F File Offset: 0x00304E2F
		public Image Background
		{
			get
			{
				return this._backgroundImage;
			}
			set
			{
				this._backgroundImage = value;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x060071AA RID: 29098 RVA: 0x00306A38 File Offset: 0x00304E38
		public Image RaycasterImage
		{
			[CompilerGenerated]
			get
			{
				return this._raycasterImage;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x060071AB RID: 29099 RVA: 0x00306A40 File Offset: 0x00304E40
		// (set) Token: 0x060071AC RID: 29100 RVA: 0x00306A48 File Offset: 0x00304E48
		public Action Action { get; set; }

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x060071AD RID: 29101 RVA: 0x00306A51 File Offset: 0x00304E51
		private StringReactiveProperty _name { get; } = new StringReactiveProperty();

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x060071AE RID: 29102 RVA: 0x00306A59 File Offset: 0x00304E59
		private StringReactiveProperty _message { get; } = new StringReactiveProperty();

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x060071AF RID: 29103 RVA: 0x00306A61 File Offset: 0x00304E61
		// (set) Token: 0x060071B0 RID: 29104 RVA: 0x00306A69 File Offset: 0x00304E69
		public CrossFade CrossFade { get; private set; }

		// Token: 0x060071B1 RID: 29105 RVA: 0x00306A74 File Offset: 0x00304E74
		private void Awake()
		{
			this._hypJpn = this._messageLabel.GetOrAddComponent<HyphenationJpn>();
			this._name.Value = string.Empty;
			this._name.SubscribeToText(this._nameLabel);
			this._message.Value = string.Empty;
			this._message.SubscribeToText(this._messageLabel);
			this._advWindowRootVisible.Subscribe(delegate(bool isOn)
			{
				this._advWindowRootCG.alpha = (float)((!isOn) ? 0 : 1);
				this._advWindowRootCG.blocksRaycasts = isOn;
			}).AddTo(this._advWindowRootCG);
			this._advWindowVisible.Subscribe(delegate(bool isOn)
			{
				this._advWindowCG.alpha = (float)((!isOn) ? 0 : 1);
				this._advWindowCG.blocksRaycasts = isOn;
			}).AddTo(this._advWindowCG);
			(from s in this._message
			select !s.IsNullOrEmpty()).Subscribe(delegate(bool isOn)
			{
				this._advWindowVisible.Value = isOn;
			});
			base.enabled = false;
			this._windowImage.color = Color.black - Color.black * 0.5f;
			if (this._nameFrameImage != null)
			{
				this._nameFrameImage.color = Color.black;
			}
			this._fontSpeed.TakeUntilDestroy(this).Subscribe(delegate(int value)
			{
				this._typefaceAnimator.isNoWait = (value == 100);
				if (!this._typefaceAnimator.isNoWait)
				{
					this._typefaceAnimator.timeMode = TypefaceAnimatorEx.TimeMode.Speed;
					this._typefaceAnimator.speed = (float)value;
				}
			});
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x00306BC0 File Offset: 0x00304FC0
		private void Start()
		{
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			if (canvasGroup != null)
			{
				(from _ in Observable.EveryGameObjectUpdate().TakeUntilDestroy(base.gameObject)
				where this.isActiveAndEnabled
				where Mathf.Approximately(canvasGroup.alpha, 1f)
				select _).Subscribe(delegate(long _)
				{
					this.OnUpdate();
				});
			}
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x00306C3C File Offset: 0x0030503C
		private void OnUpdate()
		{
			if (Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			if (Singleton<MapUIContainer>.IsInstance() && MapUIContainer.FadeCanvas.IsFading)
			{
				return;
			}
			if (!Singleton<Game>.IsInstance() || Singleton<Game>.Instance.Config != null || Singleton<Game>.Instance.Dialog != null || Singleton<Game>.Instance.ExitScene != null || Singleton<Game>.Instance.MapShortcutUI != null)
			{
				return;
			}
			if (this.Visible && this._advWindowVisible.Value && UnityEngine.Input.GetKeyDown(KeyCode.F3))
			{
				ConfigWindow.TitleChangeAction = delegate()
				{
					Singleton<Game>.Instance.Dialog.TimeScale = 1f;
				};
				Singleton<Game>.Instance.LoadConfig();
			}
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x00306D32 File Offset: 0x00305132
		public bool ChangeChara(int id)
		{
			return false;
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x00306D35 File Offset: 0x00305135
		public void Clear()
		{
			this._name.Value = string.Empty;
			this._message.Value = string.Empty;
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x00306D57 File Offset: 0x00305157
		public void SetName(string name)
		{
			this._nameLabel.enabled = false;
			this._name.Value = name;
			this._nameLabel.enabled = true;
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x00306D80 File Offset: 0x00305180
		public void SetText(string text, bool noWait = false)
		{
			base.enabled = true;
			this._message.Value = text;
			if (this._hypJpn != null)
			{
				this._hypJpn.SetText(text);
			}
			this.FontSpeed = Config.GameData.FontSpeed;
			if (noWait && !this._typefaceAnimator.isNoWait)
			{
				this.FontSpeed = 100;
			}
			this._typefaceAnimator.Play();
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x060071B8 RID: 29112 RVA: 0x00306DF2 File Offset: 0x003051F2
		public bool IsCompleteDisplayText
		{
			get
			{
				return !this._typefaceAnimator.isPlaying;
			}
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x00306E02 File Offset: 0x00305202
		public void ForceCompleteDisplayText()
		{
			this._typefaceAnimator.progress = 1f;
		}

		// Token: 0x060071BA RID: 29114 RVA: 0x00306E14 File Offset: 0x00305214
		private List<string> AnalysisText(string text)
		{
			List<string> list = new List<string>();
			int num = 0;
			text = text.Replace("\n", CaptionSystem._replaceStrings);
			StringBuilder stringBuilder = new StringBuilder();
			do
			{
				int num2 = 29;
				stringBuilder.Length = 0;
				for (int i = 0; i < 3; i++)
				{
					if (num + num2 > text.Length)
					{
						num2 += text.Length - (num + num2);
						if (num2 <= 0)
						{
							break;
						}
					}
					string text2 = text.Substring(num, num2);
					int num3 = text2.IndexOf("\n");
					if (num3 < 0)
					{
						stringBuilder.Append(text2);
						num += num2;
					}
					else
					{
						string text3 = text2.Substring(0, num3 + 1);
						stringBuilder.Append(text3);
						num += text3.Length;
					}
				}
				list.Add(stringBuilder.ToString());
			}
			while (num < text.Length);
			return list;
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x00306EFE File Offset: 0x003052FE
		private void OnStartText()
		{
			this._endAnimation = false;
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x00306F07 File Offset: 0x00305307
		private void OnEndText()
		{
			if (this._currentLine < this._textBuffer.Length)
			{
				this.NextLine(false);
			}
			else
			{
				this._endAnimation = true;
			}
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x00306F30 File Offset: 0x00305330
		private void NextLine(bool noWait = false)
		{
			StringBuilder stringBuilder = StringBuilderPool.Get();
			stringBuilder.Append(this._name.Value);
			stringBuilder.Append("： ");
			stringBuilder.Append(this._textBuffer[this._currentLine]);
			this._message.Value = stringBuilder.ToString();
			this._currentLine++;
			this.FontSpeed = Config.GameData.FontSpeed;
			if (noWait && !this._typefaceAnimator.isNoWait)
			{
				this.FontSpeed = 100;
			}
			this._typefaceAnimator.Play();
		}

		// Token: 0x04005D1A RID: 23834
		private const int _drawCount = 88;

		// Token: 0x04005D1B RID: 23835
		[SerializeField]
		private CanvasGroup _advWindowRootCG;

		// Token: 0x04005D1D RID: 23837
		[SerializeField]
		private CanvasGroup _advWindowCG;

		// Token: 0x04005D1F RID: 23839
		[SerializeField]
		private Image _backgroundImage;

		// Token: 0x04005D20 RID: 23840
		[SerializeField]
		private Image _windowImage;

		// Token: 0x04005D21 RID: 23841
		[SerializeField]
		private Image _nameFrameImage;

		// Token: 0x04005D22 RID: 23842
		[SerializeField]
		private Image _raycasterImage;

		// Token: 0x04005D24 RID: 23844
		[SerializeField]
		private Text _nameLabel;

		// Token: 0x04005D25 RID: 23845
		[SerializeField]
		private Text _messageLabel;

		// Token: 0x04005D28 RID: 23848
		[SerializeField]
		private TypefaceAnimatorEx _typefaceAnimator;

		// Token: 0x04005D29 RID: 23849
		private const int MAX_FONT_SPEED = 100;

		// Token: 0x04005D2A RID: 23850
		[SerializeField]
		[RangeReactiveProperty(1f, 100f)]
		private IntReactiveProperty _fontSpeed = new IntReactiveProperty(100);

		// Token: 0x04005D2B RID: 23851
		[SerializeField]
		private ColorReactiveProperty _fontColor = new ColorReactiveProperty(Color.white);

		// Token: 0x04005D2D RID: 23853
		private string[] _textBuffer;

		// Token: 0x04005D2E RID: 23854
		private int _currentLine = -1;

		// Token: 0x04005D2F RID: 23855
		private bool _endAnimation;

		// Token: 0x04005D30 RID: 23856
		private bool _clicked;

		// Token: 0x04005D31 RID: 23857
		private HyphenationJpn _hypJpn;

		// Token: 0x04005D32 RID: 23858
		private static readonly string[] _replaceStrings = new string[]
		{
			"\r\n",
			"\r"
		};
	}
}
