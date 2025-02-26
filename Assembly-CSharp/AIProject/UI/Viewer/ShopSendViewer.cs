using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EB3 RID: 3763
	public class ShopSendViewer : MonoBehaviour
	{
		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06007AF2 RID: 31474 RVA: 0x0033ACE0 File Offset: 0x003390E0
		public Text infoText
		{
			[CompilerGenerated]
			get
			{
				return this._infoText;
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06007AF3 RID: 31475 RVA: 0x0033ACE8 File Offset: 0x003390E8
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06007AF4 RID: 31476 RVA: 0x0033ACF0 File Offset: 0x003390F0
		public ShopViewer.ItemListController controller { get; } = new ShopViewer.ItemListController();

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06007AF5 RID: 31477 RVA: 0x0033ACF8 File Offset: 0x003390F8
		// (set) Token: 0x06007AF6 RID: 31478 RVA: 0x0033AD00 File Offset: 0x00339100
		public bool initialized { get; private set; }

		// Token: 0x06007AF7 RID: 31479 RVA: 0x0033AD09 File Offset: 0x00339109
		private void Awake()
		{
			this.controller.Bind(this._itemListUI);
		}

		// Token: 0x06007AF8 RID: 31480 RVA: 0x0033AD1C File Offset: 0x0033911C
		private IEnumerator Start()
		{
			if (this._infoText != null)
			{
				GameObject infoObj = this._infoText.gameObject;
				(from x in this._itemListUI.optionTable.ObserveCountChanged(false)
				select x == 0).Subscribe(delegate(bool active)
				{
					infoObj.SetActive(active);
				}).AddTo(infoObj);
			}
			this.initialized = true;
			yield break;
		}

		// Token: 0x0400629F RID: 25247
		[SerializeField]
		private Text _infoText;

		// Token: 0x040062A0 RID: 25248
		[SerializeField]
		private ItemListUI _itemListUI;
	}
}
