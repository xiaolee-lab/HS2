using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E8E RID: 3726
	public class ItemNodeShopUI : ItemNodeUI
	{
		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x00329844 File Offset: 0x00327C44
		public GameObject soldout
		{
			[CompilerGenerated]
			get
			{
				return this._soldout;
			}
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x0032984C File Offset: 0x00327C4C
		protected override void Start()
		{
			base.Start();
			if (this._soldout != null)
			{
				(from count in this._stackCount
				select count <= 0).Subscribe(delegate(bool active)
				{
					this._canvasGroup.blocksRaycasts = !active;
					this._soldout.SetActive(active);
				});
			}
		}

		// Token: 0x040060F4 RID: 24820
		[SerializeField]
		[Header("Soldout")]
		private GameObject _soldout;
	}
}
