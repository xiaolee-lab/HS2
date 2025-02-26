using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A0A RID: 2570
	public class CvsBase : MonoBehaviour
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x001BF397 File Offset: 0x001BD797
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x001BF39E File Offset: 0x001BD79E
		protected ChaListControl lstCtrl
		{
			get
			{
				return Singleton<Character>.Instance.chaListCtrl;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x001BF3AA File Offset: 0x001BD7AA
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x001BF3B7 File Offset: 0x001BD7B7
		protected ChaFileFace face
		{
			get
			{
				return this.chaCtrl.fileFace;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x001BF3C4 File Offset: 0x001BD7C4
		protected ChaFileBody body
		{
			get
			{
				return this.chaCtrl.fileBody;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x001BF3D1 File Offset: 0x001BD7D1
		protected ChaFileHair hair
		{
			get
			{
				return this.chaCtrl.fileHair;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x001BF3DE File Offset: 0x001BD7DE
		protected ChaFileFace.MakeupInfo makeup
		{
			get
			{
				return this.chaCtrl.fileFace.makeup;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x001BF3F0 File Offset: 0x001BD7F0
		protected ChaFileClothes orgClothes
		{
			get
			{
				return this.chaCtrl.chaFile.coordinate.clothes;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x001BF407 File Offset: 0x001BD807
		protected ChaFileClothes nowClothes
		{
			get
			{
				return this.chaCtrl.nowCoordinate.clothes;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x001BF419 File Offset: 0x001BD819
		protected ChaFileAccessory orgAcs
		{
			get
			{
				return this.chaCtrl.chaFile.coordinate.accessory;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x001BF430 File Offset: 0x001BD830
		protected ChaFileAccessory nowAcs
		{
			get
			{
				return this.chaCtrl.nowCoordinate.accessory;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x001BF442 File Offset: 0x001BD842
		protected ChaFileParameter parameter
		{
			get
			{
				return this.chaCtrl.chaFile.parameter;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x001BF454 File Offset: 0x001BD854
		protected ChaFileParameter2 parameter2
		{
			get
			{
				return this.chaCtrl.chaFile.parameter2;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x001BF466 File Offset: 0x001BD866
		protected ChaFileGameInfo gameinfo
		{
			get
			{
				return this.chaCtrl.chaFile.gameinfo;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x001BF478 File Offset: 0x001BD878
		protected ChaFileControl defChaCtrl
		{
			get
			{
				return this.customBase.defChaCtrl;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x001BF485 File Offset: 0x001BD885
		// (set) Token: 0x06004C89 RID: 19593 RVA: 0x001BF48D File Offset: 0x001BD88D
		public int SNo { get; set; }

		// Token: 0x06004C8A RID: 19594 RVA: 0x001BF498 File Offset: 0x001BD898
		public void ReacquireTab()
		{
			this.tglTab = null;
			List<UI_ToggleEx> list = new List<UI_ToggleEx>();
			GameObject gameObject = base.transform.FindLoop("SelectMenu");
			if (gameObject)
			{
				for (int i = 0; i < 5; i++)
				{
					Transform transform = gameObject.transform.Find(string.Format("tgl{0:00}", i + 1));
					if (transform)
					{
						UI_ToggleEx component = transform.GetComponent<UI_ToggleEx>();
						if (component)
						{
							list.Add(component);
						}
					}
				}
				if (list.Count != 0)
				{
					this.tglTab = list.ToArray();
				}
			}
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x001BF53C File Offset: 0x001BD93C
		public void ShowOrHideTab(bool show, params int[] no)
		{
			if (this.tglTab.Length == 0)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < no.Length; i++)
			{
				if (this.tglTab.Length > no[i])
				{
					if (!show)
					{
						flag |= this.tglTab[no[i]].isOn;
					}
					this.tglTab[no[i]].gameObject.SetActiveIfDifferent(show);
				}
			}
			if (!show)
			{
				if (flag)
				{
					this.tglTab[0].isOn = true;
				}
				for (int j = 0; j < no.Length; j++)
				{
					this.tglTab[no[j]].SetIsOnWithoutCallback(false);
				}
			}
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x001BF5EE File Offset: 0x001BD9EE
		public virtual void UpdateCustomUI()
		{
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x001BF5F0 File Offset: 0x001BD9F0
		public virtual void ChangeMenuFunc()
		{
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x001BF5F4 File Offset: 0x001BD9F4
		public static List<CustomSelectInfo> CreateSelectList(ChaListDefine.CategoryNo cateNo, ChaListDefine.KeyType limitKey = ChaListDefine.KeyType.Unknown)
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(cateNo);
			int[] array = categoryInfo.Keys.ToArray<int>();
			List<CustomSelectInfo> list = new List<CustomSelectInfo>();
			for (int i = 0; i < categoryInfo.Count; i++)
			{
				if (categoryInfo[array[i]].GetInfoInt(ChaListDefine.KeyType.Possess) != 99)
				{
					bool newItem = false;
					byte b = chaListCtrl.CheckItemID(categoryInfo[array[i]].Category, categoryInfo[array[i]].Id);
					if (b == 1)
					{
						newItem = true;
					}
					list.Add(new CustomSelectInfo
					{
						category = categoryInfo[array[i]].Category,
						id = categoryInfo[array[i]].Id,
						limitIndex = ((limitKey != ChaListDefine.KeyType.Unknown) ? categoryInfo[array[i]].GetInfoInt(limitKey) : -1),
						name = categoryInfo[array[i]].Name,
						assetBundle = categoryInfo[array[i]].GetInfo(ChaListDefine.KeyType.ThumbAB),
						assetName = categoryInfo[array[i]].GetInfo(ChaListDefine.KeyType.ThumbTex),
						newItem = newItem
					});
				}
			}
			return list;
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x001BF740 File Offset: 0x001BDB40
		public int GetSelectTab()
		{
			var <>__AnonType = this.items.Select((CvsBase.ItemInfo v, int i) => new
			{
				v,
				i
			}).FirstOrDefault(x => x.v.tglItem.isOn);
			if (<>__AnonType != null)
			{
				return <>__AnonType.i;
			}
			return -1;
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x001BF7A8 File Offset: 0x001BDBA8
		public static List<CustomPushInfo> CreatePushList(ChaListDefine.CategoryNo cateNo)
		{
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(cateNo);
			int[] array = categoryInfo.Keys.ToArray<int>();
			List<CustomPushInfo> list = new List<CustomPushInfo>();
			for (int i = 0; i < categoryInfo.Count; i++)
			{
				list.Add(new CustomPushInfo
				{
					category = categoryInfo[array[i]].Category,
					id = categoryInfo[array[i]].Id,
					name = categoryInfo[array[i]].Name,
					assetBundle = categoryInfo[array[i]].GetInfo(ChaListDefine.KeyType.ThumbAB),
					assetName = categoryInfo[array[i]].GetInfo(ChaListDefine.KeyType.ThumbTex)
				});
			}
			return list;
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x001BF86C File Offset: 0x001BDC6C
		protected virtual void Reset()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			GameObject objectFromName = findAssist.GetObjectFromName("textWinTitle");
			if (objectFromName)
			{
				this.titleText = objectFromName.GetComponent<Text>();
			}
			List<CvsBase.ItemInfo> list = new List<CvsBase.ItemInfo>();
			for (int i = 0; i < 5; i++)
			{
				GameObject objectFromName2 = findAssist.GetObjectFromName(string.Format("tgl{0:00}", i + 1));
				if (objectFromName2)
				{
					GameObject objectFromName3 = findAssist.GetObjectFromName(string.Format("Setting{0:00}", i + 1));
					if (objectFromName3)
					{
						UI_ToggleEx component = objectFromName2.GetComponent<UI_ToggleEx>();
						CanvasGroup component2 = objectFromName3.GetComponent<CanvasGroup>();
						list.Add(new CvsBase.ItemInfo
						{
							tglItem = component,
							cgItem = component2
						});
					}
				}
			}
			if (1 < list.Count<CvsBase.ItemInfo>())
			{
				this.items = list.ToArray();
			}
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x001BF960 File Offset: 0x001BDD60
		protected virtual void Start()
		{
			if (this.items.Any<CvsBase.ItemInfo>())
			{
				(from item in this.items.Select((CvsBase.ItemInfo val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null && item.val.tglItem != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.tglItem.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						foreach (var <>__AnonType in this.items.Select((CvsBase.ItemInfo v, int i) => new
						{
							v,
							i
						}))
						{
							if (<>__AnonType.i != item.idx && <>__AnonType.v != null)
							{
								CanvasGroup cgItem = <>__AnonType.v.cgItem;
								if (cgItem)
								{
									cgItem.Enable(false, false);
								}
							}
						}
						if (item.val.cgItem)
						{
							item.val.cgItem.Enable(true, false);
						}
						this.customBase.customCtrl.showColorCvs = false;
						this.customBase.customCtrl.showPattern = false;
					});
				});
			}
		}

		// Token: 0x0400462D RID: 17965
		[Button("ReacquireTab", "タブ再取得", new object[]
		{

		})]
		public int reacquireTab;

		// Token: 0x0400462E RID: 17966
		[SerializeField]
		private UI_ToggleEx[] tglTab;

		// Token: 0x04004630 RID: 17968
		public Text titleText;

		// Token: 0x04004631 RID: 17969
		public CvsBase.ItemInfo[] items;

		// Token: 0x02000A0B RID: 2571
		[Serializable]
		public class ItemInfo
		{
			// Token: 0x04004637 RID: 17975
			public UI_ToggleEx tglItem;

			// Token: 0x04004638 RID: 17976
			public CanvasGroup cgItem;
		}
	}
}
