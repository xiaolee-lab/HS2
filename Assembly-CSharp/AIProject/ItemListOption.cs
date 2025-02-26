using System;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E8B RID: 3723
	public class ItemListOption : MonoBehaviour
	{
		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06007752 RID: 30546 RVA: 0x00327EE7 File Offset: 0x003262E7
		public Image IconImage
		{
			[CompilerGenerated]
			get
			{
				return this._iconImage;
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06007753 RID: 30547 RVA: 0x00327EEF File Offset: 0x003262EF
		public StringReactiveProperty Text
		{
			[CompilerGenerated]
			get
			{
				return this._text;
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06007754 RID: 30548 RVA: 0x00327EF7 File Offset: 0x003262F7
		// (set) Token: 0x06007755 RID: 30549 RVA: 0x00327F04 File Offset: 0x00326304
		public bool EnabledButton
		{
			get
			{
				return this._button.enabled;
			}
			set
			{
				this._button.enabled = value;
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06007756 RID: 30550 RVA: 0x00327F12 File Offset: 0x00326312
		public Button.ButtonClickedEvent OnClicked
		{
			[CompilerGenerated]
			get
			{
				return this._button.onClick;
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06007757 RID: 30551 RVA: 0x00327F1F File Offset: 0x0032631F
		// (set) Token: 0x06007758 RID: 30552 RVA: 0x00327F27 File Offset: 0x00326327
		public UnityEvent OnRemove { get; private set; } = new UnityEvent();

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06007759 RID: 30553 RVA: 0x00327F30 File Offset: 0x00326330
		public bool IsInteractable
		{
			[CompilerGenerated]
			get
			{
				return this._button.IsInteractable();
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x0600775A RID: 30554 RVA: 0x00327F3D File Offset: 0x0032633D
		// (set) Token: 0x0600775B RID: 30555 RVA: 0x00327F45 File Offset: 0x00326345
		public bool IsStackable { get; set; } = true;

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x0600775C RID: 30556 RVA: 0x00327F4E File Offset: 0x0032634E
		// (set) Token: 0x0600775D RID: 30557 RVA: 0x00327F56 File Offset: 0x00326356
		public StuffItem Item { get; set; }

		// Token: 0x0600775E RID: 30558 RVA: 0x00327F60 File Offset: 0x00326360
		private void Start()
		{
			(from x in this._text
			where x != this._stackCountLabel.text
			select x).Subscribe(delegate(string x)
			{
				this._stackCountLabel.text = x;
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x00327FC9 File Offset: 0x003263C9
		private void OnUpdate()
		{
			if (this.Item == null)
			{
				return;
			}
			if (this.IsStackable)
			{
				return;
			}
			this._fillImage.fillAmount = 1f;
		}

		// Token: 0x040060CF RID: 24783
		[SerializeField]
		private Image _iconImage;

		// Token: 0x040060D0 RID: 24784
		[SerializeField]
		private Image _fillImage;

		// Token: 0x040060D1 RID: 24785
		[SerializeField]
		private StringReactiveProperty _text;

		// Token: 0x040060D2 RID: 24786
		[SerializeField]
		private TextMeshProUGUI _stackCountLabel;

		// Token: 0x040060D4 RID: 24788
		[SerializeField]
		private Button _button;
	}
}
