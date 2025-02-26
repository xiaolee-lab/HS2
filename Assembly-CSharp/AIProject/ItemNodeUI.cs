using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using Illusion.Extensions;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E8F RID: 3727
	public class ItemNodeUI : SerializedMonoBehaviour
	{
		// Token: 0x060077B4 RID: 30644 RVA: 0x00328FC8 File Offset: 0x003273C8
		public static StuffItemInfo GetItemInfo(StuffItem item)
		{
			if (item == null)
			{
				return null;
			}
			StuffItemInfo stuffItemInfo = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID) ?? Singleton<Manager.Resources>.Instance.GameInfo.GetItem_System(item.CategoryID, item.ID);
			if (stuffItemInfo == null)
			{
			}
			return stuffItemInfo;
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x00329024 File Offset: 0x00327424
		public static void Sort(int sortID, bool ascending, IDictionary<int, ItemNodeUI> table)
		{
			List<KeyValuePair<int, ItemNodeUI>> list = new List<KeyValuePair<int, ItemNodeUI>>(table);
			list.Sort(new ItemNodeUI.DictionaryComparer((ItemNodeUI.DictionaryComparer.SortID)sortID, ascending));
			foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in list)
			{
				ItemNodeUI value = keyValuePair.Value;
				value.transform.SetAsLastSibling();
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x060077B6 RID: 30646 RVA: 0x003290A0 File Offset: 0x003274A0
		public Button.ButtonClickedEvent OnClick
		{
			[CompilerGenerated]
			get
			{
				return this._button.onClick;
			}
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x060077B7 RID: 30647 RVA: 0x003290AD File Offset: 0x003274AD
		public bool IsInteractable
		{
			[CompilerGenerated]
			get
			{
				return this._button.IsInteractable();
			}
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x003290BA File Offset: 0x003274BA
		public void Enabled()
		{
			this._button.enabled = true;
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x003290C8 File Offset: 0x003274C8
		public void Disabled()
		{
			this._button.enabled = false;
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x060077BA RID: 30650 RVA: 0x003290D6 File Offset: 0x003274D6
		public IObservable<PointerEventData> onEnter
		{
			[CompilerGenerated]
			get
			{
				return from _ in this._button.OnPointerEnterAsObservable()
				where this._button.enabled
				select _;
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x060077BB RID: 30651 RVA: 0x003290F4 File Offset: 0x003274F4
		public int CategoryID
		{
			[CompilerGenerated]
			get
			{
				return this._item.CategoryID;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x060077BC RID: 30652 RVA: 0x00329101 File Offset: 0x00327501
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this._name.Value;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x060077BD RID: 30653 RVA: 0x0032910E File Offset: 0x0032750E
		public int Rate
		{
			[CompilerGenerated]
			get
			{
				return this._rate.Value;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x060077BE RID: 30654 RVA: 0x0032911B File Offset: 0x0032751B
		public DateTime LatestDateTime
		{
			[CompilerGenerated]
			get
			{
				return this._item.LatestDateTime;
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x060077BF RID: 30655 RVA: 0x00329128 File Offset: 0x00327528
		public int IconID
		{
			[CompilerGenerated]
			get
			{
				return this._info.IconID;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x060077C0 RID: 30656 RVA: 0x00329135 File Offset: 0x00327535
		public StuffItem Item
		{
			[CompilerGenerated]
			get
			{
				return this._item;
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x060077C1 RID: 30657 RVA: 0x0032913D File Offset: 0x0032753D
		// (set) Token: 0x060077C2 RID: 30658 RVA: 0x00329145 File Offset: 0x00327545
		private StuffItem _item { get; set; }

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x060077C3 RID: 30659 RVA: 0x0032914E File Offset: 0x0032754E
		// (set) Token: 0x060077C4 RID: 30660 RVA: 0x0032915B File Offset: 0x0032755B
		public bool Visible
		{
			get
			{
				return this.cachedgameObject.activeSelf;
			}
			set
			{
				this.cachedgameObject.SetActiveIfDifferent(value);
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x060077C5 RID: 30661 RVA: 0x0032916A File Offset: 0x0032756A
		private GameObject cachedgameObject
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._cachedgameObject, new Func<GameObject>(base.get_gameObject));
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x060077C6 RID: 30662 RVA: 0x00329184 File Offset: 0x00327584
		// (set) Token: 0x060077C7 RID: 30663 RVA: 0x0032918C File Offset: 0x0032758C
		private StuffItemInfo _info { get; set; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x060077C8 RID: 30664 RVA: 0x00329195 File Offset: 0x00327595
		// (set) Token: 0x060077C9 RID: 30665 RVA: 0x0032919D File Offset: 0x0032759D
		public bool isTrash { get; private set; } = true;

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x060077CA RID: 30666 RVA: 0x003291A6 File Offset: 0x003275A6
		// (set) Token: 0x060077CB RID: 30667 RVA: 0x003291AE File Offset: 0x003275AE
		public bool isNone { get; private set; }

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x060077CC RID: 30668 RVA: 0x003291B7 File Offset: 0x003275B7
		// (set) Token: 0x060077CD RID: 30669 RVA: 0x003291BF File Offset: 0x003275BF
		public ItemNodeUI.ExtraData extraData { get; set; }

		// Token: 0x060077CE RID: 30670 RVA: 0x003291C8 File Offset: 0x003275C8
		public void Bind(StuffItem item, StuffItemInfo info = null)
		{
			this._info = (info ?? ItemNodeUI.GetItemInfo(item));
			this.isTrash = this._info.isTrash;
			this.isNone = this._info.isNone;
			if (this.isNone)
			{
				this.isTrash = false;
				if (this._stackCountText != null)
				{
					this._stackCountText.enabled = false;
				}
			}
			this._name.Value = this._info.Name;
			if (this._iconImage != null)
			{
				Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, this._info.IconID, this._iconImage, true);
				if (this._iconImage.sprite == null)
				{
					this._iconImage.enabled = false;
				}
			}
			this._rarelity.Value = this._info.Rarelity;
			ReactiveProperty<int> rate = this._rate;
			MerchantData.VendorItem vendorItem = item as MerchantData.VendorItem;
			int? num = (vendorItem != null) ? new int?(vendorItem.Rate) : null;
			rate.Value = ((num == null) ? this._info.Rate : num.Value);
			this._rarelitySprite.Value = this._rarelities.GetElement((int)this._info.Grade);
			this._item = item;
			this._stackCount.Value = this._item.Count;
		}

		// Token: 0x060077CF RID: 30671 RVA: 0x00329340 File Offset: 0x00327740
		public void Refresh()
		{
			this._stackCount.Value = this._item.Count;
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x00329358 File Offset: 0x00327758
		protected virtual void Start()
		{
			if (this._nameLabel != null)
			{
				this._name.SubscribeToText(this._nameLabel);
			}
			if (this._stackCountText != null)
			{
				this._stackCount.SubscribeToText(this._stackCountText);
			}
			if (this._rateText != null)
			{
				this._rate.SubscribeToText(this._rateText, (int x) => (x < 0) ? string.Empty : x.ToString());
			}
			if (this._iconText != null)
			{
				this._iconText.enabled = false;
			}
			if (this._rarelityText != null)
			{
				this._rarelity.SubscribeToText(this._rarelityText, (Rarelity x) => (!x.GetType().IsEnumDefined(x)) ? string.Empty : x.ToString());
				this._rarelitySprite.Subscribe(delegate(Sprite sprite)
				{
					this._rarelityText.enabled = (sprite == null);
				});
			}
			if (this._rarelityImage != null)
			{
				this._rarelitySprite.Subscribe(delegate(Sprite sprite)
				{
					bool flag = sprite == null;
					this._rarelityImage.enabled = !flag;
					this._rarelityImage.sprite = sprite;
				});
			}
			EasingFunction easing;
			if (Tween.MotionFunctionTable.TryGetValue(this._motionType, out easing))
			{
			}
			if (Tween.MotionFunctionTable.TryGetValue(this._alphaMotionType, out easing))
			{
				ObservableEasing.Create(easing, this._easingDuration, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					this._canvasGroup.alpha = Mathf.Lerp(this._fromAlpha, this._toAlpha, x.Value);
				}).AddTo(this.disposables);
			}
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x003294E7 File Offset: 0x003278E7
		private void OnDestroy()
		{
			this.disposables.Clear();
		}

		// Token: 0x040060F6 RID: 24822
		[SerializeField]
		private Image _iconImage;

		// Token: 0x040060F7 RID: 24823
		[SerializeField]
		private Text _iconText;

		// Token: 0x040060F8 RID: 24824
		[SerializeField]
		private Text _nameLabel;

		// Token: 0x040060F9 RID: 24825
		[SerializeField]
		private RectTransform _itemNameLabelViewport;

		// Token: 0x040060FA RID: 24826
		[SerializeField]
		private Image _rarelityImage;

		// Token: 0x040060FB RID: 24827
		[SerializeField]
		private Text _rarelityText;

		// Token: 0x040060FC RID: 24828
		[SerializeField]
		private Text _rateText;

		// Token: 0x040060FD RID: 24829
		[SerializeField]
		private Text _stackCountText;

		// Token: 0x040060FE RID: 24830
		[SerializeField]
		private Button _button;

		// Token: 0x040060FF RID: 24831
		[SerializeField]
		[Header("Animation")]
		private Transform _animationRoot;

		// Token: 0x04006100 RID: 24832
		[SerializeField]
		protected CanvasGroup _canvasGroup;

		// Token: 0x04006101 RID: 24833
		[SerializeField]
		private float _easingDuration;

		// Token: 0x04006102 RID: 24834
		[SerializeField]
		private MotionType _motionType;

		// Token: 0x04006103 RID: 24835
		[SerializeField]
		private Transform _from;

		// Token: 0x04006104 RID: 24836
		[SerializeField]
		private Transform _to;

		// Token: 0x04006105 RID: 24837
		[SerializeField]
		private MotionType _alphaMotionType;

		// Token: 0x04006106 RID: 24838
		[SerializeField]
		private float _fromAlpha;

		// Token: 0x04006107 RID: 24839
		[SerializeField]
		private float _toAlpha = 1f;

		// Token: 0x04006108 RID: 24840
		[SerializeField]
		private Image _line;

		// Token: 0x04006109 RID: 24841
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private StringReactiveProperty _name = new StringReactiveProperty();

		// Token: 0x0400610A RID: 24842
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		protected IntReactiveProperty _stackCount = new IntReactiveProperty(0);

		// Token: 0x0400610B RID: 24843
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _rate = new IntReactiveProperty(-1);

		// Token: 0x0400610C RID: 24844
		[SerializeField]
		private Sprite[] _rarelities = new Sprite[0];

		// Token: 0x0400610D RID: 24845
		private ReactiveProperty<Sprite> _rarelitySprite = new ReactiveProperty<Sprite>(null);

		// Token: 0x0400610E RID: 24846
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private ReactiveProperty<Rarelity> _rarelity = new ReactiveProperty<Rarelity>();

		// Token: 0x04006110 RID: 24848
		private GameObject _cachedgameObject;

		// Token: 0x04006111 RID: 24849
		private CompositeDisposable disposables = new CompositeDisposable();

		// Token: 0x02000E90 RID: 3728
		public interface ExtraData
		{
		}

		// Token: 0x02000E91 RID: 3729
		private class DictionaryComparer : IComparer<KeyValuePair<int, ItemNodeUI>>
		{
			// Token: 0x060077D8 RID: 30680 RVA: 0x003295C1 File Offset: 0x003279C1
			public DictionaryComparer(ItemNodeUI.DictionaryComparer.SortID sortID, bool ascending)
			{
				this.sortID = sortID;
				this.ascending = ascending;
			}

			// Token: 0x17001789 RID: 6025
			// (get) Token: 0x060077D9 RID: 30681 RVA: 0x003295D7 File Offset: 0x003279D7
			private ItemNodeUI.DictionaryComparer.SortID sortID { get; }

			// Token: 0x1700178A RID: 6026
			// (get) Token: 0x060077DA RID: 30682 RVA: 0x003295DF File Offset: 0x003279DF
			private bool ascending { get; }

			// Token: 0x060077DB RID: 30683 RVA: 0x003295E8 File Offset: 0x003279E8
			public int Compare(KeyValuePair<int, ItemNodeUI> x, KeyValuePair<int, ItemNodeUI> y)
			{
				ItemNodeUI value = x.Value;
				ItemNodeUI value2 = y.Value;
				if (value.isNone && value2.isNone)
				{
					int num = this.SortCompare<int>(value._item.CategoryID, value2._item.CategoryID);
					return (num == 0) ? this.SortCompare<int>(value._item.ID, value2._item.ID) : num;
				}
				if (value.isNone)
				{
					return -1;
				}
				if (value2.isNone)
				{
					return 1;
				}
				switch (this.sortID)
				{
				case ItemNodeUI.DictionaryComparer.SortID.Time:
				{
					int num2 = (!this.ascending) ? this.SortCompare<DateTime>(value2.LatestDateTime, value.LatestDateTime) : this.SortCompare<DateTime>(value.LatestDateTime, value2.LatestDateTime);
					return (num2 == 0) ? this.SortCompare<string>(value.Name, value2.Name) : num2;
				}
				case ItemNodeUI.DictionaryComparer.SortID.Name:
					return (!this.ascending) ? this.SortCompare<string>(value2.Name, value.Name) : this.SortCompare<string>(value.Name, value2.Name);
				case ItemNodeUI.DictionaryComparer.SortID.Gread:
				{
					int num3 = (!this.ascending) ? this.SortCompare<Grade>(value2._info.Grade, value._info.Grade) : this.SortCompare<Grade>(value._info.Grade, value2._info.Grade);
					return (num3 == 0) ? this.SortCompare<string>(value.Name, value2.Name) : num3;
				}
				case ItemNodeUI.DictionaryComparer.SortID.Category:
				{
					int num4 = (!this.ascending) ? this.SortCompare<int>(value2._item.CategoryID, value._item.CategoryID) : this.SortCompare<int>(value._item.CategoryID, value2._item.CategoryID);
					if (num4 != 0)
					{
						return num4;
					}
					num4 = this.SortCompare<int>(value.IconID, value2.IconID);
					return (num4 == 0) ? this.SortCompare<string>(value.Name, value2.Name) : num4;
				}
				default:
					return 0;
				}
			}

			// Token: 0x060077DC RID: 30684 RVA: 0x00329827 File Offset: 0x00327C27
			private int SortCompare<T>(T a, T b) where T : IComparable
			{
				return a.CompareTo(b);
			}

			// Token: 0x02000E92 RID: 3730
			public enum SortID
			{
				// Token: 0x0400611B RID: 24859
				Time,
				// Token: 0x0400611C RID: 24860
				Name,
				// Token: 0x0400611D RID: 24861
				Gread,
				// Token: 0x0400611E RID: 24862
				Category
			}
		}
	}
}
