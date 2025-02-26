using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AIProject.UI.Popup;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000F17 RID: 3863
	[RequireComponent(typeof(CanvasGroup))]
	public class NotifyMessageList : MonoBehaviour
	{
		// Token: 0x06007E98 RID: 32408 RVA: 0x0035D90F File Offset: 0x0035BD0F
		public OneColor PersonColor(int _id)
		{
			if (_id != -99)
			{
				return this.personColors.GetElement(_id);
			}
			return this.playerColor;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x0035D934 File Offset: 0x0035BD34
		public string PersonName(int _id)
		{
			if (!Singleton<Map>.IsInstance())
			{
				return "テストネーム";
			}
			if (_id == -99)
			{
				return (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player.CharaName;
			}
			if (!Singleton<Map>.IsInstance())
			{
				return null;
			}
			AgentActor agentActor;
			return (!Singleton<Map>.Instance.AgentTable.TryGetValue(_id, out agentActor)) ? null : agentActor.CharaName;
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x0035D9AD File Offset: 0x0035BDAD
		public void ClearStockMessage()
		{
			if (this.messageStock != null)
			{
				this.messageStock.Clear();
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06007E9B RID: 32411 RVA: 0x0035D9C8 File Offset: 0x0035BDC8
		private GameObject Prefab
		{
			get
			{
				if (this._prefab != null)
				{
					return this._prefab;
				}
				DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
				this._prefab = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefab, "NotifyMessageElement", false, definePack.ABManifests.Default);
				return this._prefab;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06007E9C RID: 32412 RVA: 0x0035DA25 File Offset: 0x0035BE25
		// (set) Token: 0x06007E9D RID: 32413 RVA: 0x0035DA30 File Offset: 0x0035BE30
		public bool Visibled
		{
			get
			{
				return this.visibled;
			}
			set
			{
				if (this.visibled == value)
				{
					return;
				}
				this.visibled = value;
				float _from = this.canvasGroup.alpha;
				float _to = (!value) ? 0f : 1f;
				if (this.fadeDisposable != null)
				{
					this.fadeDisposable.Dispose();
				}
				this.fadeDisposable = ObservableEasing.EaseOutQuint(0.3f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					this.canvasGroup.alpha = Mathf.Lerp(_from, _to, x.Value);
				}, delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x0035DAE7 File Offset: 0x0035BEE7
		private void Awake()
		{
			this.openElements = ListPool<NotifyMessageElement>.Get();
			this.closeElements = ListPool<NotifyMessageElement>.Get();
			this.messageStock = ListPool<string>.Get();
			this.StartNextLogCheker();
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x0035DB10 File Offset: 0x0035BF10
		private void StartNextLogCheker()
		{
			if (this.nextLogChekerDisposable != null)
			{
				return;
			}
			IEnumerator _coroutine = this.NextLogChecker();
			this.nextLogChekerDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.gameObject).Subscribe<Unit>();
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x0035DB64 File Offset: 0x0035BF64
		private IEnumerator NextLogChecker()
		{
			for (;;)
			{
				while (this.messageStock.IsNullOrEmpty<string>() || !this.visibled || !base.isActiveAndEnabled)
				{
					yield return null;
				}
				while (this.fullNotPopup)
				{
					int _activeCount = 0;
					foreach (NotifyMessageElement notifyMessageElement in this.openElements)
					{
						if (notifyMessageElement.PlayingFadeIn || notifyMessageElement.PlayingDisplay)
						{
							_activeCount++;
						}
					}
					if (this.displayMaxElement > _activeCount)
					{
						break;
					}
					yield return null;
				}
				this.PopupLog();
				IObservable<long> _timer = Observable.Timer(TimeSpan.FromSeconds((double)this.nextPopupTime), Scheduler.MainThreadIgnoreTimeScale).TakeUntilDestroy(base.gameObject);
				yield return _timer.ToYieldInstruction<long>();
			}
			yield break;
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x0035DB80 File Offset: 0x0035BF80
		private void OnDestroy()
		{
			ListPool<NotifyMessageElement>.Release(this.openElements);
			ListPool<NotifyMessageElement>.Release(this.closeElements);
			ListPool<string>.Release(this.messageStock);
			if (this.nextLogChekerDisposable != null)
			{
				this.nextLogChekerDisposable.Dispose();
			}
			this.openElements = null;
			this.closeElements = null;
			this.messageStock = null;
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x0035DBDC File Offset: 0x0035BFDC
		private NotifyMessageElement GetElement()
		{
			NotifyMessageElement notifyMessageElement = (!this.closeElements.IsNullOrEmpty<NotifyMessageElement>()) ? this.closeElements.Pop<NotifyMessageElement>() : null;
			if (notifyMessageElement == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab, base.transform, false);
				notifyMessageElement = ((gameObject != null) ? gameObject.GetComponent<NotifyMessageElement>() : null);
				if (notifyMessageElement == null)
				{
					return null;
				}
				notifyMessageElement.gameObject.name = string.Format("{0}_{1}", this.Prefab.gameObject.name, this.elmCount++);
				notifyMessageElement.Root = this;
			}
			notifyMessageElement.EndActionEvent = new Action<NotifyMessageElement>(this.EndAction);
			return notifyMessageElement;
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x0035DCA0 File Offset: 0x0035C0A0
		private void ReturnElement(NotifyMessageElement _elm)
		{
			if (_elm.gameObject.activeSelf)
			{
				_elm.gameObject.SetActive(false);
			}
			if (this.openElements.Contains(_elm))
			{
				this.openElements.Remove(_elm);
			}
			if (!this.closeElements.Contains(_elm))
			{
				this.closeElements.Add(_elm);
			}
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x0035DD04 File Offset: 0x0035C104
		private void EndAction(NotifyMessageElement _elm)
		{
			_elm.EndActionEvent = null;
			this.ReturnElement(_elm);
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x0035DD14 File Offset: 0x0035C114
		public void AddMessage(string _message)
		{
			this.AddLog(_message ?? string.Empty);
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x0035DD2C File Offset: 0x0035C12C
		public void AddMessage(int _actorID, string _message)
		{
			if (_message.IsNullOrEmpty() && _message == null)
			{
				_message = string.Empty;
			}
			OneColor oneColor = this.PersonColor(_actorID);
			string text = this.PersonName(_actorID);
			if (text == null)
			{
				text = string.Empty;
			}
			if (text.IsNullOrEmpty() && _message.IsNullOrEmpty())
			{
				return;
			}
			if (text.IsNullOrEmpty())
			{
				this.AddLog(_message);
				return;
			}
			if (oneColor != null)
			{
				StringBuilder stringBuilder = StringBuilderPool.Get();
				stringBuilder.AppendFormat("<color={0}>{1}</color>{2}", oneColor, text, _message);
				_message = stringBuilder.ToString();
				StringBuilderPool.Release(stringBuilder);
			}
			else
			{
				StringBuilder stringBuilder2 = StringBuilderPool.Get();
				stringBuilder2.AppendFormat("{0}{1}", text, _message);
				_message = stringBuilder2.ToString();
				StringBuilderPool.Release(stringBuilder2);
			}
			this.AddLog(_message);
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x0035DDFE File Offset: 0x0035C1FE
		private void AddLog(string _message)
		{
			if (this.messageStock != null)
			{
				this.messageStock.Add(_message);
			}
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x0035DE1C File Offset: 0x0035C21C
		private void PopupLog()
		{
			string messageText = this.messageStock.Pop<string>();
			NotifyMessageElement element = this.GetElement();
			if (element == null)
			{
				return;
			}
			element.SetTime(this.fadeInTime, this.displayTime, this.fadeOutTime);
			element.SetAlpha(this.startAlpha, this.endAlpha);
			element.LocalPosition = Vector3.zero;
			element.MessageText = messageText;
			element.MessageColor = this.whiteColor;
			element.SpeechBubbleIconActive = true;
			if (!element.gameObject.activeSelf)
			{
				element.gameObject.SetActive(true);
			}
			element.transform.SetAsLastSibling();
			element.StartFadeIn(this.popupWidth);
			float num = 0f;
			for (int i = 0; i < this.openElements.Count; i++)
			{
				NotifyMessageElement notifyMessageElement = this.openElements[i];
				if (notifyMessageElement == null)
				{
					this.openElements.RemoveAt(i);
					i--;
				}
				else
				{
					notifyMessageElement.SpeechBubbleIconActive = false;
					num += this.popupHeight;
					if (i <= this.displayMaxElement - 2)
					{
						notifyMessageElement.StartSlidUp(num);
					}
					else if (!notifyMessageElement.PlayingFadeOut)
					{
						notifyMessageElement.StartFadeOut();
					}
				}
			}
			this.openElements.Insert(0, element);
		}

		// Token: 0x04006610 RID: 26128
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04006611 RID: 26129
		[SerializeField]
		private int displayMaxElement = 3;

		// Token: 0x04006612 RID: 26130
		[SerializeField]
		private float popupWidth = 320f;

		// Token: 0x04006613 RID: 26131
		[SerializeField]
		private float popupHeight = 100f;

		// Token: 0x04006614 RID: 26132
		[SerializeField]
		private float nextPopupTime = 1f;

		// Token: 0x04006615 RID: 26133
		[SerializeField]
		private bool fullNotPopup;

		// Token: 0x04006616 RID: 26134
		[SerializeField]
		private float fadeInTime = 0.8f;

		// Token: 0x04006617 RID: 26135
		[SerializeField]
		private float displayTime = 2f;

		// Token: 0x04006618 RID: 26136
		[SerializeField]
		private float fadeOutTime = 0.8f;

		// Token: 0x04006619 RID: 26137
		[SerializeField]
		private float startAlpha = 0.5f;

		// Token: 0x0400661A RID: 26138
		[SerializeField]
		private float endAlpha = 0.5f;

		// Token: 0x0400661B RID: 26139
		[SerializeField]
		private OneColor whiteColor = new OneColor(235f, 226f, 215f, 255f);

		// Token: 0x0400661C RID: 26140
		[SerializeField]
		private OneColor playerColor = new OneColor(133f, 214f, 83f, 255f);

		// Token: 0x0400661D RID: 26141
		[SerializeField]
		private OneColor[] personColors = new OneColor[]
		{
			new OneColor(233f, 67f, 33f, 255f),
			new OneColor(0f, 183f, 238f, 255f),
			new OneColor(250f, 239f, 0f, 255f),
			new OneColor(255f, 0f, 255f, 255f)
		};

		// Token: 0x0400661E RID: 26142
		private List<NotifyMessageElement> openElements;

		// Token: 0x0400661F RID: 26143
		private List<NotifyMessageElement> closeElements;

		// Token: 0x04006620 RID: 26144
		private List<string> messageStock;

		// Token: 0x04006621 RID: 26145
		private GameObject _prefab;

		// Token: 0x04006622 RID: 26146
		private bool visibled = true;

		// Token: 0x04006623 RID: 26147
		private IDisposable fadeDisposable;

		// Token: 0x04006624 RID: 26148
		private IDisposable nextLogChekerDisposable;

		// Token: 0x04006625 RID: 26149
		private int elmCount;
	}
}
