using System;
using System.Collections;
using System.Linq;
using AIProject.SaveData;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000FE8 RID: 4072
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class SpendMoneyUI : MonoBehaviour
	{
		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x060088E4 RID: 35044 RVA: 0x0038F759 File Offset: 0x0038DB59
		// (set) Token: 0x060088E5 RID: 35045 RVA: 0x0038F764 File Offset: 0x0038DB64
		public bool Visibled
		{
			get
			{
				return this._visibled;
			}
			set
			{
				if (this._visibled == value)
				{
					return;
				}
				this._visibled = value;
				this._fadeDisposable.Clear();
				IEnumerator coroutine = (!this._visibled) ? this.CloseCoroutine() : this.OpenCoroutine();
				Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
				{
				}, delegate(Exception ex)
				{
				}).AddTo(this._fadeDisposable);
			}
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x060088E6 RID: 35046 RVA: 0x0038F815 File Offset: 0x0038DC15
		// (set) Token: 0x060088E7 RID: 35047 RVA: 0x0038F83D File Offset: 0x0038DC3D
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x060088E8 RID: 35048 RVA: 0x0038F85C File Offset: 0x0038DC5C
		private IEnumerator OpenCoroutine()
		{
			this.SettingUI();
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._openFadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._fadeDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this._fadeDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._fadeDisposable);
			this.CanvasAlpha = 1f;
			yield break;
		}

		// Token: 0x060088E9 RID: 35049 RVA: 0x0038F878 File Offset: 0x0038DC78
		private IEnumerator CloseCoroutine()
		{
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._closeFadeTime, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._fadeDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).AddTo(this._fadeDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._fadeDisposable);
			this.CanvasAlpha = 0f;
			yield break;
		}

		// Token: 0x060088EA RID: 35050 RVA: 0x0038F894 File Offset: 0x0038DC94
		private void SettingUI()
		{
			PlayerData playerData;
			if (Singleton<Map>.IsInstance())
			{
				PlayerActor player = Singleton<Map>.Instance.Player;
				playerData = ((player != null) ? player.PlayerData : null);
			}
			else
			{
				playerData = null;
			}
			PlayerData playerData2 = playerData;
			MerchantProfile merchantProfile = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.MerchantProfile;
			int[] array = (merchantProfile != null) ? merchantProfile.SpendMoneyBorder : null;
			if (playerData2 == null || array.IsNullOrEmpty<int>())
			{
				return;
			}
			playerData2.SpendMoney = Mathf.Min(merchantProfile.SpendMoneyMax, playerData2.SpendMoney);
			int num = array.Last<int>();
			bool flag = false;
			if (!this._icons.IsNullOrEmpty<Image>())
			{
				bool flag2 = true;
				for (int i = 0; i < this._icons.Length; i++)
				{
					if (i == 0)
					{
						flag = true;
					}
					bool flag3 = i < array.Length && array[i] <= playerData2.SpendMoney;
					this._icons[i].sprite = ((!flag3) ? this._offIcon : this._onIcon);
					flag = (flag && flag3);
					if (!flag3 && flag2)
					{
						num = ((i >= array.Length) ? array.Last<int>() : array[i]);
					}
					flag2 = flag3;
				}
			}
			this._moneyText.text = string.Format("{0}/{1,4}", playerData2.SpendMoney, num);
			this.SetActive(this._moneyText, !flag);
			this.SetActive(this._maxText, flag);
		}

		// Token: 0x060088EB RID: 35051 RVA: 0x0038FA1F File Offset: 0x0038DE1F
		private void Awake()
		{
			if (this._canvasGroup != null)
			{
				this._canvasGroup.blocksRaycasts = false;
				this._canvasGroup.alpha = 0f;
			}
		}

		// Token: 0x060088EC RID: 35052 RVA: 0x0038FA4E File Offset: 0x0038DE4E
		private void OnDestroy()
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Clear();
			}
		}

		// Token: 0x060088ED RID: 35053 RVA: 0x0038FA68 File Offset: 0x0038DE68
		private void SetActive(Component com, bool active)
		{
			if (com != null && com.gameObject != null && com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
			}
		}

		// Token: 0x060088EE RID: 35054 RVA: 0x0038FAA4 File Offset: 0x0038DEA4
		private void SetActive(GameObject obj, bool active)
		{
			if (obj != null && obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}

		// Token: 0x04006EC9 RID: 28361
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006ECA RID: 28362
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006ECB RID: 28363
		[SerializeField]
		private Sprite _onIcon;

		// Token: 0x04006ECC RID: 28364
		[SerializeField]
		private Sprite _offIcon;

		// Token: 0x04006ECD RID: 28365
		[SerializeField]
		private Image[] _icons = new Image[0];

		// Token: 0x04006ECE RID: 28366
		[SerializeField]
		private Text _moneyText;

		// Token: 0x04006ECF RID: 28367
		[SerializeField]
		private Text _maxText;

		// Token: 0x04006ED0 RID: 28368
		[SerializeField]
		private float _openFadeTime = 1f;

		// Token: 0x04006ED1 RID: 28369
		[SerializeField]
		private float _closeFadeTime = 0.3f;

		// Token: 0x04006ED2 RID: 28370
		private CompositeDisposable _fadeDisposable = new CompositeDisposable();

		// Token: 0x04006ED3 RID: 28371
		private bool _visibled;
	}
}
