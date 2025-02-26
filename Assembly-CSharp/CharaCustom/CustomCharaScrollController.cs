using System;
using System.Collections.Generic;
using System.Linq;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009A6 RID: 2470
	[Serializable]
	public class CustomCharaScrollController : MonoBehaviour
	{
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x001B5CD9 File Offset: 0x001B40D9
		// (set) Token: 0x0600470D RID: 18189 RVA: 0x001B5CE1 File Offset: 0x001B40E1
		public CustomCharaScrollController.ScrollData selectInfo { get; private set; }

		// Token: 0x0600470E RID: 18190 RVA: 0x001B5CEC File Offset: 0x001B40EC
		public void CreateList(List<CustomCharaFileInfo> _lst)
		{
			this.scrollerDatas = _lst.Select((CustomCharaFileInfo value, int idx) => new CustomCharaScrollController.ScrollData
			{
				index = idx,
				info = value
			}).ToArray<CustomCharaScrollController.ScrollData>();
			int num = (!this.scrollerDatas.IsNullOrEmpty<CustomCharaScrollController.ScrollData>()) ? (this.scrollerDatas.Length / this.countPerRow) : 0;
			if (!this.scrollerDatas.IsNullOrEmpty<CustomCharaScrollController.ScrollData>() && this.scrollerDatas.Length % this.countPerRow > 0)
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

		// Token: 0x0600470F RID: 18191 RVA: 0x001B5DDA File Offset: 0x001B41DA
		public void SelectInfoClear()
		{
			this.selectInfo = null;
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x001B5DE3 File Offset: 0x001B41E3
		public void SetTopLine()
		{
			if (this.view.IsInit)
			{
				this.view.MovePanelToItemIndex(0, 0f);
			}
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x001B5E08 File Offset: 0x001B4208
		public void SetNowSelectToggle()
		{
			for (int i = 0; i < this.view.ShownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
				if (!(shownItemByIndex == null))
				{
					CustomCharaScrollViewInfo component = shownItemByIndex.GetComponent<CustomCharaScrollViewInfo>();
					for (int j = 0; j < this.countPerRow; j++)
					{
						component.SetToggleON(j, this.IsNowSelectInfo(component.GetListInfo(j)));
					}
				}
			}
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x001B5E81 File Offset: 0x001B4281
		public void SetLine(int _line)
		{
			this.view.MovePanelToItemIndex(_line, 0f);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x001B5E94 File Offset: 0x001B4294
		public void SetNowLine()
		{
			int itemIndex = 0;
			if (this.selectInfo != null)
			{
				itemIndex = this.selectInfo.index / this.countPerRow;
			}
			this.view.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x001B5ED4 File Offset: 0x001B42D4
		private void OnValueChange(CustomCharaScrollController.ScrollData _data, bool _isOn)
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
							CustomCharaScrollViewInfo component = shownItemByIndex.GetComponent<CustomCharaScrollViewInfo>();
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

		// Token: 0x06004715 RID: 18197 RVA: 0x001B6007 File Offset: 0x001B4407
		private void OnPointerEnter(string name)
		{
			if (this.text)
			{
				this.text.text = name;
			}
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x001B6025 File Offset: 0x001B4425
		private void OnPointerExit()
		{
			if (this.text)
			{
				this.text.text = this.SelectName;
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x001B6048 File Offset: 0x001B4448
		private bool IsNowSelectInfo(CustomCharaFileInfo _info)
		{
			return _info != null && this.selectInfo != null && this.selectInfo.info.FullPath == _info.FullPath;
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x001B607C File Offset: 0x001B447C
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			CustomCharaScrollViewInfo component = loopListViewItem.GetComponent<CustomCharaScrollViewInfo>();
			for (int i = 0; i < this.countPerRow; i++)
			{
				int index = _index * this.countPerRow + i;
				CustomCharaScrollController.ScrollData data = this.scrollerDatas.SafeGet(index);
				component.SetData(i, data, delegate(bool _isOn)
				{
					this.OnValueChange(data, _isOn);
				}, new Action<string>(this.OnPointerEnter), new Action(this.OnPointerExit));
				CustomCharaScrollViewInfo customCharaScrollViewInfo = component;
				int index2 = i;
				CustomCharaScrollController.ScrollData data2 = data;
				customCharaScrollViewInfo.SetToggleON(index2, this.IsNowSelectInfo((data2 != null) ? data2.info : null));
			}
			return loopListViewItem;
		}

		// Token: 0x04004245 RID: 16965
		[SerializeField]
		private LoopListView2 view;

		// Token: 0x04004246 RID: 16966
		[SerializeField]
		private GameObject original;

		// Token: 0x04004247 RID: 16967
		[SerializeField]
		private int countPerRow = 3;

		// Token: 0x04004248 RID: 16968
		[SerializeField]
		private Text text;

		// Token: 0x04004249 RID: 16969
		private string SelectName = string.Empty;

		// Token: 0x0400424A RID: 16970
		private CustomCharaScrollController.ScrollData[] scrollerDatas;

		// Token: 0x0400424C RID: 16972
		public Action<CustomCharaFileInfo> onSelect;

		// Token: 0x0400424D RID: 16973
		public Action onDeSelect;

		// Token: 0x020009A7 RID: 2471
		public class ScrollData
		{
			// Token: 0x0400424F RID: 16975
			public int index;

			// Token: 0x04004250 RID: 16976
			public CustomCharaFileInfo info;

			// Token: 0x04004251 RID: 16977
			public Toggle toggle;
		}
	}
}
