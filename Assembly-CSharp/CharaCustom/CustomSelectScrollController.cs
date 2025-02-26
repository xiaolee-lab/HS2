using System;
using System.Collections.Generic;
using System.Linq;
using Illusion.Extensions;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009BC RID: 2492
	[Serializable]
	public class CustomSelectScrollController : MonoBehaviour
	{
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x001B8498 File Offset: 0x001B6898
		// (set) Token: 0x06004798 RID: 18328 RVA: 0x001B84A0 File Offset: 0x001B68A0
		public CustomSelectScrollController.ScrollData selectInfo { get; private set; }

		// Token: 0x06004799 RID: 18329 RVA: 0x001B84A9 File Offset: 0x001B68A9
		public void Start()
		{
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x001B84AC File Offset: 0x001B68AC
		public void CreateList(List<CustomSelectInfo> _lst)
		{
			this.scrollerDatas = _lst.Select((CustomSelectInfo value, int idx) => new CustomSelectScrollController.ScrollData
			{
				index = idx,
				info = value
			}).ToArray<CustomSelectScrollController.ScrollData>();
			int num = (!this.scrollerDatas.IsNullOrEmpty<CustomSelectScrollController.ScrollData>()) ? (this.scrollerDatas.Length / this.countPerRow) : 0;
			if (!this.scrollerDatas.IsNullOrEmpty<CustomSelectScrollController.ScrollData>() && this.scrollerDatas.Length % this.countPerRow > 0)
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

		// Token: 0x0600479B RID: 18331 RVA: 0x001B859A File Offset: 0x001B699A
		public void SelectInfoClear()
		{
			this.selectInfo = null;
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x001B85A3 File Offset: 0x001B69A3
		public void SetTopLine()
		{
			if (this.view.IsInit)
			{
				this.view.MovePanelToItemIndex(0, 0f);
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x001B85C6 File Offset: 0x001B69C6
		public void SetToggle(int _index)
		{
			this.selectInfo = this.scrollerDatas[_index];
			this.view.RefreshAllShownItem();
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x001B85E4 File Offset: 0x001B69E4
		public void SetToggleID(int _id)
		{
			CustomSelectScrollController.ScrollData scrollData = this.scrollerDatas.FirstOrDefault((CustomSelectScrollController.ScrollData x) => x.info.id == _id);
			if (scrollData != null)
			{
				this.selectInfo = scrollData;
				this.view.RefreshAllShownItem();
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x001B8630 File Offset: 0x001B6A30
		private void SetNowSelectToggle()
		{
			for (int i = 0; i < this.view.ShownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
				if (!(shownItemByIndex == null))
				{
					CustomSelectScrollViewInfo component = shownItemByIndex.GetComponent<CustomSelectScrollViewInfo>();
					for (int j = 0; j < this.countPerRow; j++)
					{
						component.SetToggleON(j, this.IsNowSelectInfo(component.GetListInfo(j)));
					}
				}
			}
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x001B86A9 File Offset: 0x001B6AA9
		public void SetLine(int _line)
		{
			this.view.MovePanelToItemIndex(_line, 0f);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x001B86BC File Offset: 0x001B6ABC
		public void SetNowLine()
		{
			int itemIndex = 0;
			if (this.selectInfo != null)
			{
				itemIndex = this.selectInfo.index / this.countPerRow;
			}
			this.view.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x001B86FC File Offset: 0x001B6AFC
		private void OnValueChange(CustomSelectScrollController.ScrollData _data, bool _isOn)
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
							CustomSelectScrollViewInfo component = shownItemByIndex.GetComponent<CustomSelectScrollViewInfo>();
							for (int j = 0; j < this.countPerRow; j++)
							{
								if (!this.IsNowSelectInfo(component.GetListInfo(j)))
								{
									component.SetToggleON(j, false);
								}
								else
								{
									component.SetNewFlagOff(j);
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
				_data.toggle.SetIsOnWithoutCallback(true);
			}
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x001B8829 File Offset: 0x001B6C29
		private void OnPointerEnter(string name)
		{
			if (this.text)
			{
				this.text.text = name;
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x001B8847 File Offset: 0x001B6C47
		private void OnPointerExit()
		{
			if (this.text)
			{
				this.text.text = this.SelectName;
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x001B886A File Offset: 0x001B6C6A
		private bool IsNowSelectInfo(CustomSelectInfo _info)
		{
			return _info != null && this.selectInfo != null && this.selectInfo.info == _info;
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x001B8890 File Offset: 0x001B6C90
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			CustomSelectScrollViewInfo component = loopListViewItem.GetComponent<CustomSelectScrollViewInfo>();
			for (int i = 0; i < this.countPerRow; i++)
			{
				int index = _index * this.countPerRow + i;
				CustomSelectScrollController.ScrollData data = this.scrollerDatas.SafeGet(index);
				component.SetData(i, data, delegate(bool _isOn)
				{
					this.OnValueChange(data, _isOn);
				}, new Action<string>(this.OnPointerEnter), new Action(this.OnPointerExit));
				CustomSelectScrollViewInfo customSelectScrollViewInfo = component;
				int index2 = i;
				CustomSelectScrollController.ScrollData data2 = data;
				customSelectScrollViewInfo.SetToggleON(index2, this.IsNowSelectInfo((data2 != null) ? data2.info : null));
			}
			return loopListViewItem;
		}

		// Token: 0x040042D9 RID: 17113
		[SerializeField]
		private LoopListView2 view;

		// Token: 0x040042DA RID: 17114
		[SerializeField]
		private GameObject original;

		// Token: 0x040042DB RID: 17115
		[SerializeField]
		private int countPerRow = 5;

		// Token: 0x040042DC RID: 17116
		[SerializeField]
		private Text text;

		// Token: 0x040042DD RID: 17117
		private string SelectName = string.Empty;

		// Token: 0x040042DE RID: 17118
		private CustomSelectScrollController.ScrollData[] scrollerDatas;

		// Token: 0x040042E0 RID: 17120
		public Action<CustomSelectInfo> onSelect;

		// Token: 0x040042E1 RID: 17121
		public Action onDeSelect;

		// Token: 0x020009BD RID: 2493
		public class ScrollData
		{
			// Token: 0x040042E3 RID: 17123
			public int index;

			// Token: 0x040042E4 RID: 17124
			public CustomSelectInfo info = new CustomSelectInfo();

			// Token: 0x040042E5 RID: 17125
			public Toggle toggle;
		}
	}
}
