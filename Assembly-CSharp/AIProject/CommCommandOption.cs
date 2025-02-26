using System;
using System.Runtime.CompilerServices;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E55 RID: 3669
	public class CommCommandOption : MonoBehaviour
	{
		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x060073E7 RID: 29671 RVA: 0x00316194 File Offset: 0x00314594
		public Text TimerText
		{
			[CompilerGenerated]
			get
			{
				return this._timerText;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x060073E8 RID: 29672 RVA: 0x0031619C File Offset: 0x0031459C
		public CanvasGroup TimerCanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._timerCanvasGroup;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x003161A4 File Offset: 0x003145A4
		// (set) Token: 0x060073EA RID: 29674 RVA: 0x003161AC File Offset: 0x003145AC
		public Action OnClick { get; set; }

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x060073EB RID: 29675 RVA: 0x003161B5 File Offset: 0x003145B5
		// (set) Token: 0x060073EC RID: 29676 RVA: 0x003161D0 File Offset: 0x003145D0
		public string LabelText
		{
			get
			{
				return (this._label != null) ? this._label.text : null;
			}
			set
			{
				if (this._label != null)
				{
					this._label.text = value;
				}
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x060073ED RID: 29677 RVA: 0x003161EF File Offset: 0x003145EF
		// (set) Token: 0x060073EE RID: 29678 RVA: 0x0031620A File Offset: 0x0031460A
		public Sprite Sprite
		{
			get
			{
				return (this._panel != null) ? this._panel.sprite : null;
			}
			set
			{
				if (this._panel != null)
				{
					this._panel.sprite = value;
				}
			}
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x0031622C File Offset: 0x0031462C
		public void Start()
		{
			if (this._button != null)
			{
				this._button.onClick.AddListener(delegate()
				{
					Action onClick = this.OnClick;
					if (onClick != null)
					{
						onClick();
					}
				});
			}
			if (this._timerCanvasGroup != null)
			{
				this._timerCanvasGroup.alpha = 0f;
			}
		}

		// Token: 0x060073F0 RID: 29680 RVA: 0x00316288 File Offset: 0x00314688
		public void SetActiveTimer(bool active)
		{
			if (this._timerText == null || this._timerCanvasGroup == null)
			{
				return;
			}
			if (this.ActiveTimer == active)
			{
				return;
			}
			this.ActiveTimer = active;
			if (this._timerDisposable != null)
			{
				this._timerDisposable.Dispose();
			}
			float startAlpha = this._timerCanvasGroup.alpha;
			int destAlpha = (!active) ? 0 : 1;
			ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._timerCanvasGroup.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
			}).DoOnError(delegate(Exception ex)
			{
			}).Subscribe<TimeInterval<float>>();
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x060073F1 RID: 29681 RVA: 0x0031635F File Offset: 0x0031475F
		// (set) Token: 0x060073F2 RID: 29682 RVA: 0x00316367 File Offset: 0x00314767
		public bool ActiveTimer { get; private set; }

		// Token: 0x060073F3 RID: 29683 RVA: 0x00316370 File Offset: 0x00314770
		public void SetTimeColor(Color color)
		{
			if (this._timerText == null)
			{
				return;
			}
			this._timerText.color = color;
		}

		// Token: 0x060073F4 RID: 29684 RVA: 0x00316390 File Offset: 0x00314790
		public void SetTime(float time)
		{
			if (this._timerText == null)
			{
				return;
			}
			this._timerText.text = this.FloatToString(time);
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x003163B8 File Offset: 0x003147B8
		private string FloatToString(float timer)
		{
			int num = (int)timer;
			int num2 = num / 3600;
			num %= 3600;
			int num3 = num / 60;
			num %= 60;
			int num4 = num;
			string str = string.Empty;
			if (num2 > 0)
			{
				str += string.Format("{0}", num2);
			}
			return str + string.Format("{0:00}:{1:00}", num3, num4);
		}

		// Token: 0x04005ECE RID: 24270
		[SerializeField]
		private Text _label;

		// Token: 0x04005ECF RID: 24271
		[SerializeField]
		private Image _panel;

		// Token: 0x04005ED0 RID: 24272
		[SerializeField]
		private Button _button;

		// Token: 0x04005ED1 RID: 24273
		[SerializeField]
		private Text _timerText;

		// Token: 0x04005ED2 RID: 24274
		[SerializeField]
		private CanvasGroup _timerCanvasGroup;

		// Token: 0x04005ED5 RID: 24277
		private IDisposable _timerDisposable;
	}
}
