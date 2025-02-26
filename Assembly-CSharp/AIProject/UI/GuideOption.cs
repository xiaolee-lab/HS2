using System;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000F97 RID: 3991
	public class GuideOption : MonoBehaviour
	{
		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06008527 RID: 34087 RVA: 0x003749F1 File Offset: 0x00372DF1
		// (set) Token: 0x06008528 RID: 34088 RVA: 0x003749FE File Offset: 0x00372DFE
		public Sprite Icon
		{
			get
			{
				return this._iconImage.sprite;
			}
			set
			{
				this._iconImage.sprite = value;
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06008529 RID: 34089 RVA: 0x00374A0C File Offset: 0x00372E0C
		// (set) Token: 0x0600852A RID: 34090 RVA: 0x00374A19 File Offset: 0x00372E19
		public string CaptionText
		{
			get
			{
				return this._captionText.text;
			}
			set
			{
				this._captionText.text = value;
			}
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x00374A27 File Offset: 0x00372E27
		private void Start()
		{
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x00374A29 File Offset: 0x00372E29
		private void OnEnable()
		{
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x00374A2B File Offset: 0x00372E2B
		private void OnDisable()
		{
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x00374A2D File Offset: 0x00372E2D
		private void Open()
		{
			ObservableEasing.Linear(1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = x.Value;
			});
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x00374A52 File Offset: 0x00372E52
		private void Close()
		{
			ObservableEasing.Linear(1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = 1f - x.Value;
			});
		}

		// Token: 0x04006BBD RID: 27581
		[SerializeField]
		private Image _iconImage;

		// Token: 0x04006BBE RID: 27582
		[SerializeField]
		private Text _captionText;

		// Token: 0x04006BBF RID: 27583
		[SerializeField]
		private CanvasGroup _canvasGroup;
	}
}
