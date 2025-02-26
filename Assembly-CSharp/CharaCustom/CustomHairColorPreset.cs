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
	// Token: 0x0200099D RID: 2461
	public class CustomHairColorPreset : MonoBehaviour
	{
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x001B4A33 File Offset: 0x001B2E33
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x001B4A3A File Offset: 0x001B2E3A
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x001B4A48 File Offset: 0x001B2E48
		public void Reset()
		{
			this.items = new CustomHairColorPreset.ItemInfo[7];
			for (int i = 0; i < 7; i++)
			{
				this.items[i] = new CustomHairColorPreset.ItemInfo();
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

		// Token: 0x060046EB RID: 18155 RVA: 0x001B4AE8 File Offset: 0x001B2EE8
		public void Start()
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.preset_hair_color);
			for (int i = 0; i < this.items.Length; i++)
			{
				ListInfoBase listInfoBase;
				if (categoryInfo.TryGetValue(i, out listInfoBase))
				{
					this.items[i].colorInfo.baseColor = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.BaseH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.BaseS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.BaseV));
					this.items[i].colorInfo.topColor = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.TopH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.TopS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.TopV));
					this.items[i].colorInfo.underColor = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.UnderH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.UnderS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.UnderV));
					this.items[i].colorInfo.specular = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SpecularH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SpecularS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SpecularV));
					this.items[i].colorInfo.metallic = listInfoBase.GetInfoFloat(ChaListDefine.KeyType.Metallic);
					this.items[i].colorInfo.smoothness = listInfoBase.GetInfoFloat(ChaListDefine.KeyType.Smoothness);
					this.items[i].image.color = Color.HSVToRGB(listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SampleH), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SampleS), listInfoBase.GetInfoFloat(ChaListDefine.KeyType.SampleV));
				}
			}
			if (this.items.Any<CustomHairColorPreset.ItemInfo>())
			{
				(from item in this.items.Select((CustomHairColorPreset.ItemInfo val, int idx) => new
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
							this.onClick(item.val.colorInfo);
						}
					});
				});
			}
		}

		// Token: 0x040041F3 RID: 16883
		public CustomHairColorPreset.ItemInfo[] items;

		// Token: 0x040041F4 RID: 16884
		public Action<CustomHairColorPreset.HairColorInfo> onClick;

		// Token: 0x0200099E RID: 2462
		public class HairColorInfo
		{
			// Token: 0x040041F7 RID: 16887
			public Color topColor = Color.white;

			// Token: 0x040041F8 RID: 16888
			public Color baseColor = Color.white;

			// Token: 0x040041F9 RID: 16889
			public Color underColor = Color.white;

			// Token: 0x040041FA RID: 16890
			public Color specular = Color.white;

			// Token: 0x040041FB RID: 16891
			public float metallic;

			// Token: 0x040041FC RID: 16892
			public float smoothness;
		}

		// Token: 0x0200099F RID: 2463
		[Serializable]
		public class ItemInfo
		{
			// Token: 0x040041FD RID: 16893
			public Button button;

			// Token: 0x040041FE RID: 16894
			public Image image;

			// Token: 0x040041FF RID: 16895
			public CustomHairColorPreset.HairColorInfo colorInfo = new CustomHairColorPreset.HairColorInfo();
		}
	}
}
