using System;
using System.Collections.Generic;
using System.Linq;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009B0 RID: 2480
	[Serializable]
	public class CustomClothesScrollController : MonoBehaviour
	{
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600474E RID: 18254 RVA: 0x001B707C File Offset: 0x001B547C
		// (set) Token: 0x0600474F RID: 18255 RVA: 0x001B7084 File Offset: 0x001B5484
		public CustomClothesScrollController.ScrollData selectInfo { get; private set; }

		// Token: 0x06004750 RID: 18256 RVA: 0x001B7090 File Offset: 0x001B5490
		public void CreateList(List<CustomClothesFileInfo> _lst)
		{
			this.scrollerDatas = _lst.Select((CustomClothesFileInfo value, int idx) => new CustomClothesScrollController.ScrollData
			{
				index = idx,
				info = value
			}).ToArray<CustomClothesScrollController.ScrollData>();
			int num = (!this.scrollerDatas.IsNullOrEmpty<CustomClothesScrollController.ScrollData>()) ? (this.scrollerDatas.Length / this.countPerRow) : 0;
			if (!this.scrollerDatas.IsNullOrEmpty<CustomClothesScrollController.ScrollData>() && this.scrollerDatas.Length % this.countPerRow > 0)
			{
				num++;
			}
			if (!this.view.IsInit)
			{
				this.view.InitListViewAndSize(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnUpdate));
			}
			else
			{
				this.view.ReSetListItemCount(num);
			}
			this.SelectName = string.Empty;
			if (this.text)
			{
				this.text.text = string.Empty;
			}
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x001B717E File Offset: 0x001B557E
		public void SelectInfoClear()
		{
			this.selectInfo = null;
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x001B7187 File Offset: 0x001B5587
		public void SetTopLine()
		{
			if (this.view.IsInit)
			{
				this.view.MovePanelToItemIndex(0, 0f);
			}
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x001B71AC File Offset: 0x001B55AC
		public void SetNowSelectToggle()
		{
			for (int i = 0; i < this.view.ShownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
				if (!(shownItemByIndex == null))
				{
					CustomClothesScrollViewInfo component = shownItemByIndex.GetComponent<CustomClothesScrollViewInfo>();
					for (int j = 0; j < this.countPerRow; j++)
					{
						component.SetToggleON(j, this.IsNowSelectInfo(component.GetListInfo(j)));
					}
				}
			}
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x001B7225 File Offset: 0x001B5625
		public void SetLine(int _line)
		{
			this.view.MovePanelToItemIndex(_line, 0f);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x001B7238 File Offset: 0x001B5638
		public void SetNowLine()
		{
			int itemIndex = 0;
			if (this.selectInfo != null)
			{
				itemIndex = this.selectInfo.index / this.countPerRow;
			}
			this.view.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x001B7278 File Offset: 0x001B5678
		private void OnValueChange(CustomClothesScrollController.ScrollData _data, bool _isOn)
		{
			if (_isOn)
			{
				bool flag = !this.IsNowSelectInfo((_data != null) ? _data.info : null);
				this.selectInfo = _data;
				if (flag)
				{
					for (int i = 0; i < this.view.ShownItemCount; i++)
					{
						LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
						if (!(shownItemByIndex == null))
						{
							CustomClothesScrollViewInfo component = shownItemByIndex.GetComponent<CustomClothesScrollViewInfo>();
							for (int j = 0; j < this.countPerRow; j++)
							{
								if (!this.IsNowSelectInfo(component.GetListInfo(j)))
								{
									component.SetToggleON(j, false);
								}
							}
						}
					}
					if (this.onSelect != null)
					{
						this.onSelect(this.selectInfo.info);
					}
					if (this.selectInfo != null)
					{
						this.SelectName = this.selectInfo.info.name;
					}
				}
			}
			else if (this.IsNowSelectInfo((_data != null) ? _data.info : null))
			{
				this.selectInfo = null;
				if (this.onDeSelect != null)
				{
					this.onDeSelect();
				}
			}
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x001B73AB File Offset: 0x001B57AB
		private void OnPointerEnter(string name)
		{
			if (this.text)
			{
				this.text.text = name;
			}
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x001B73C9 File Offset: 0x001B57C9
		private void OnPointerExit()
		{
			if (this.text)
			{
				this.text.text = this.SelectName;
			}
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x001B73EC File Offset: 0x001B57EC
		private bool IsNowSelectInfo(CustomClothesFileInfo _info)
		{
			return _info != null && this.selectInfo != null && this.selectInfo.info.FullPath == _info.FullPath;
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x001B7420 File Offset: 0x001B5820
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			CustomClothesScrollViewInfo component = loopListViewItem.GetComponent<CustomClothesScrollViewInfo>();
			for (int i = 0; i < this.countPerRow; i++)
			{
				int index = _index * this.countPerRow + i;
				CustomClothesScrollController.ScrollData data = this.scrollerDatas.SafeGet(index);
				component.SetData(i, data, delegate(bool _isOn)
				{
					this.OnValueChange(data, _isOn);
				}, new Action<string>(this.OnPointerEnter), new Action(this.OnPointerExit));
				CustomClothesScrollViewInfo customClothesScrollViewInfo = component;
				int index2 = i;
				CustomClothesScrollController.ScrollData data2 = data;
				customClothesScrollViewInfo.SetToggleON(index2, this.IsNowSelectInfo((data2 != null) ? data2.info : null));
			}
			return loopListViewItem;
		}

		// Token: 0x0400428D RID: 17037
		[SerializeField]
		private LoopListView2 view;

		// Token: 0x0400428E RID: 17038
		[SerializeField]
		private GameObject original;

		// Token: 0x0400428F RID: 17039
		[SerializeField]
		private int countPerRow = 3;

		// Token: 0x04004290 RID: 17040
		[SerializeField]
		private Text text;

		// Token: 0x04004291 RID: 17041
		private string SelectName = string.Empty;

		// Token: 0x04004292 RID: 17042
		private CustomClothesScrollController.ScrollData[] scrollerDatas;

		// Token: 0x04004294 RID: 17044
		public Action<CustomClothesFileInfo> onSelect;

		// Token: 0x04004295 RID: 17045
		public Action onDeSelect;

		// Token: 0x020009B1 RID: 2481
		public class ScrollData
		{
			// Token: 0x04004297 RID: 17047
			public int index;

			// Token: 0x04004298 RID: 17048
			public CustomClothesFileInfo info;

			// Token: 0x04004299 RID: 17049
			public Toggle toggle;
		}
	}
}
