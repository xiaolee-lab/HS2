using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ADV;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000F15 RID: 3861
	public class GameLogElement : MonoBehaviour
	{
		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06007E67 RID: 32359 RVA: 0x0035C811 File Offset: 0x0035AC11
		public Text NameText
		{
			[CompilerGenerated]
			get
			{
				return this._nameText;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06007E68 RID: 32360 RVA: 0x0035C819 File Offset: 0x0035AC19
		public Text Text
		{
			[CompilerGenerated]
			get
			{
				return this._text;
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06007E69 RID: 32361 RVA: 0x0035C821 File Offset: 0x0035AC21
		public Button VoiceButton
		{
			[CompilerGenerated]
			get
			{
				return this._voiceButton;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06007E6A RID: 32362 RVA: 0x0035C829 File Offset: 0x0035AC29
		// (set) Token: 0x06007E6B RID: 32363 RVA: 0x0035C831 File Offset: 0x0035AC31
		private IReadOnlyCollection<TextScenario.IVoice[]> _voices { get; set; }

		// Token: 0x06007E6C RID: 32364 RVA: 0x0035C83A File Offset: 0x0035AC3A
		private void OnDisable()
		{
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
			this._disposable = null;
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x0035C85C File Offset: 0x0035AC5C
		public void Add(string name, string text, IReadOnlyCollection<TextScenario.IVoice[]> voices)
		{
			this._nameText.text = name;
			this._text.text = text;
			this._voices = voices;
			Behaviour image = this._voiceButton.image;
			bool flag = this._voices != null && this._voices.Any<TextScenario.IVoice[]>();
			this._voiceButton.image.raycastTarget = flag;
			image.enabled = flag;
			this._voiceButton.onClick.RemoveAllListeners();
			this._voiceButton.onClick.AddListener(delegate()
			{
				if (this._disposable != null)
				{
					this._disposable.Dispose();
				}
				this._disposable = Observable.FromCoroutine((CancellationToken _) => this.VoicePlay(this._voices), false).Subscribe<Unit>();
			});
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x0035C8F0 File Offset: 0x0035ACF0
		private IEnumerator VoicePlay(IReadOnlyCollection<TextScenario.IVoice[]> voices)
		{
			Singleton<Voice>.Instance.StopAll(false);
			foreach (TextScenario.IVoice[] voice in voices)
			{
				foreach (TextScenario.IVoice voice2 in voice)
				{
					voice2.Convert2D();
					voice2.Play();
				}
				for (;;)
				{
					if (!voice.Any((TextScenario.IVoice p) => p.Wait()))
					{
						break;
					}
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x040065FA RID: 26106
		[SerializeField]
		private Text _nameText;

		// Token: 0x040065FB RID: 26107
		[SerializeField]
		private Text _text;

		// Token: 0x040065FC RID: 26108
		[SerializeField]
		private Button _voiceButton;

		// Token: 0x040065FE RID: 26110
		private IDisposable _disposable;
	}
}
