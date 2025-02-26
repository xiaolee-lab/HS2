using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EB2 RID: 3762
	public class ShopRateViewer : MonoBehaviour
	{
		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06007AE7 RID: 31463 RVA: 0x0033AA4D File Offset: 0x00338E4D
		public Text infoText
		{
			[CompilerGenerated]
			get
			{
				return this._infoText;
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06007AE8 RID: 31464 RVA: 0x0033AA55 File Offset: 0x00338E55
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06007AE9 RID: 31465 RVA: 0x0033AA5D File Offset: 0x00338E5D
		public ConditionalTextXtoYViewer rateCounter
		{
			[CompilerGenerated]
			get
			{
				return this._rateCounter;
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x0033AA65 File Offset: 0x00338E65
		// (set) Token: 0x06007AEB RID: 31467 RVA: 0x0033AA6D File Offset: 0x00338E6D
		public bool isTrade { get; private set; }

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06007AEC RID: 31468 RVA: 0x0033AA76 File Offset: 0x00338E76
		public ShopViewer.ItemListController controller { get; } = new ShopViewer.ItemListController();

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06007AED RID: 31469 RVA: 0x0033AA7E File Offset: 0x00338E7E
		// (set) Token: 0x06007AEE RID: 31470 RVA: 0x0033AA86 File Offset: 0x00338E86
		public bool initialized { get; private set; }

		// Token: 0x06007AEF RID: 31471 RVA: 0x0033AA8F File Offset: 0x00338E8F
		private void Awake()
		{
			this.controller.Bind(this._itemListUI);
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x0033AAA4 File Offset: 0x00338EA4
		private IEnumerator Start()
		{
			this._rateCounter.Initialize();
			if (this._infoText != null)
			{
				GameObject infoObj = this._infoText.gameObject;
				(from x in this._itemListUI.optionTable.ObserveCountChanged(false)
				select x == 0).Subscribe(delegate(bool active)
				{
					infoObj.SetActive(active);
				}).AddTo(infoObj);
			}
			ReadOnlyReactiveProperty<bool> source = (from i in this._rateCounter.X
			select this._rateCounter.y <= 0 || i < this._rateCounter.y).ToReadOnlyReactiveProperty<bool>();
			source.Subscribe(delegate(bool failed)
			{
				this.isTrade = !failed;
			});
			Text xText = this._rateCounter.xText;
			if (xText != null)
			{
				source.Subscribe(delegate(bool failed)
				{
					xText.color = Define.Get((!failed) ? Colors.White : Colors.Red);
				}).AddTo(xText);
			}
			this.initialized = true;
			yield break;
		}

		// Token: 0x0400629A RID: 25242
		[SerializeField]
		private Text _infoText;

		// Token: 0x0400629B RID: 25243
		[SerializeField]
		private ItemListUI _itemListUI;

		// Token: 0x0400629C RID: 25244
		[SerializeField]
		private ConditionalTextXtoYViewer _rateCounter;
	}
}
