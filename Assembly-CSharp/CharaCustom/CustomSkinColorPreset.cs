using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009A0 RID: 2464
	public class CustomSkinColorPreset : MonoBehaviour
	{
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x001B4DF3 File Offset: 0x001B31F3
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x001B4DFA File Offset: 0x001B31FA
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x001B4E08 File Offset: 0x001B3208
		public void Reset()
		{
			this.items = new CustomSkinColorPreset.ItemInfo[7];
			for (int i = 0; i < 7; i++)
			{
				this.items[i] = new CustomSkinColorPreset.ItemInfo();
				Transform transform = base.transform.Find(string.Format("btnSample{0:00}", i + 1));
				if (transform)
				{
					this.items[i].button = transform.GetComponent<Button>();
					Transform transform2 = transform.Find("imgColor");
					if (transform2)
					{
						this.items[i].image = transform2.GetComponent<Image>();
					}
				}
			}
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x001B4EA8 File Offset: 0x001B32A8
		public void Start()
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.preset_skin_color);
			for (int i = 0; i < this.items.Length; i++)
			{
				ListInfoBase listInfoBase;
				if (categoryInfo.TryGetValue(i, out listInfoBase))
				{
					this.items[i].skinColor = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.BaseH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.BaseS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.BaseV));
					this.items[i].image.color = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SampleH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SampleS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SampleV));
				}
			}
			if (this.items.Any<CustomSkinColorPreset.ItemInfo>())
			{
				(from item in this.items.Select((CustomSkinColorPreset.ItemInfo val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null && item.val.button != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.button.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.onClick != null)
						{
							this.onClick(item.val.skinColor);
						}
					});
				});
			}
		}

		// Token: 0x04004200 RID: 16896
		public CustomSkinColorPreset.ItemInfo[] items;

		// Token: 0x04004201 RID: 16897
		public Action<Color> onClick;

		// Token: 0x020009A1 RID: 2465
		[Serializable]
		public class ItemInfo
		{
			// Token: 0x04004204 RID: 16900
			public Button button;

			// Token: 0x04004205 RID: 16901
			public Image image;

			// Token: 0x04004206 RID: 16902
			public Color skinColor = Color.white;
		}
	}
}
