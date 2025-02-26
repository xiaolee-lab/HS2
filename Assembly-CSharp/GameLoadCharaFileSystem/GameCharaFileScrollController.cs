using System;
using System.Collections.Generic;
using System.Linq;
using SuperScrollView;
using UnityEngine;

namespace GameLoadCharaFileSystem
{
	// Token: 0x02000878 RID: 2168
	[Serializable]
	public class GameCharaFileScrollController
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x00146E40 File Offset: 0x00145240
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x00146E48 File Offset: 0x00145248
		public GameCharaFileScrollController.ScrollData selectInfo { get; private set; }

		// Token: 0x0600372F RID: 14127 RVA: 0x00146E54 File Offset: 0x00145254
		public void Init(List<GameCharaFileInfo> _lst)
		{
			this.scrollerDatas = _lst.Select((GameCharaFileInfo value, int idx) => new GameCharaFileScrollController.ScrollData
			{
				index = idx,
				info = value
			}).ToArray<GameCharaFileScrollController.ScrollData>();
			int num = (!this.scrollerDatas.IsNullOrEmpty<GameCharaFileScrollController.ScrollData>()) ? (this.scrollerDatas.Length / this.countPerRow) : 0;
			if (!this.scrollerDatas.IsNullOrEmpty<GameCharaFileScrollController.ScrollData>() && this.scrollerDatas.Length % this.countPerRow > 0)
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
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x00146F17 File Offset: 0x00145317
		public void SelectInfoClear()
		{
			this.selectInfo = null;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x00146F20 File Offset: 0x00145320
		public void SetTopLine()
		{
			if (this.view.IsInit)
			{
				this.view.MovePanelToItemIndex(0, 0f);
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x00146F43 File Offset: 0x00145343
		public void SetToggle(int _index)
		{
			this.selectInfo = this.scrollerDatas[_index];
			this.view.RefreshAllShownItem();
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x00146F60 File Offset: 0x00145360
		public void SetNowSelectToggle()
		{
			for (int i = 0; i < this.view.ShownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
				if (!(shownItemByIndex == null))
				{
					GameCharaFileInfoComponent component = shownItemByIndex.GetComponent<GameCharaFileInfoComponent>();
					for (int j = 0; j < this.countPerRow; j++)
					{
						component.SetToggleON(j, this.IsNowSelectInfo(component.GetListInfo(j)));
					}
				}
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x00146FD9 File Offset: 0x001453D9
		public void SetLine(int _line)
		{
			this.view.MovePanelToItemIndex(_line, 0f);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x00146FEC File Offset: 0x001453EC
		public void SetNowLine()
		{
			int itemIndex = 0;
			if (this.selectInfo != null)
			{
				itemIndex = this.selectInfo.index / this.countPerRow;
			}
			this.view.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x0014702C File Offset: 0x0014542C
		private void OnValueChange(GameCharaFileScrollController.ScrollData _data, bool _isOn)
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
							GameCharaFileInfoComponent component = shownItemByIndex.GetComponent<GameCharaFileInfoComponent>();
							for (int j = 0; j < this.countPerRow; j++)
							{
								if (!this.IsNowSelectInfo(component.GetListInfo(j)))
								{
									component.SetToggleON(j, false);
								}
							}
						}
					}
					this.onSelect(this.selectInfo.info);
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

		// Token: 0x06003737 RID: 14135 RVA: 0x00147131 File Offset: 0x00145531
		private bool IsNowSelectInfo(GameCharaFileInfo _info)
		{
			return _info != null && this.selectInfo != null && this.selectInfo.info.FullPath == _info.FullPath;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x00147164 File Offset: 0x00145564
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			GameCharaFileInfoComponent component = loopListViewItem.GetComponent<GameCharaFileInfoComponent>();
			for (int i = 0; i < this.countPerRow; i++)
			{
				int index = _index * this.countPerRow + i;
				GameCharaFileScrollController.ScrollData data = this.scrollerDatas.SafeGet(index);
				GameCharaFileInfoComponent gameCharaFileInfoComponent = component;
				int index2 = i;
				GameCharaFileScrollController.ScrollData data3 = data;
				gameCharaFileInfoComponent.SetData(index2, (data3 != null) ? data3.info : null, delegate(bool _isOn)
				{
					this.OnValueChange(data, _isOn);
				});
				GameCharaFileInfoComponent gameCharaFileInfoComponent2 = component;
				int index3 = i;
				GameCharaFileScrollController.ScrollData data2 = data;
				gameCharaFileInfoComponent2.SetToggleON(index3, this.IsNowSelectInfo((data2 != null) ? data2.info : null));
			}
			return loopListViewItem;
		}

		// Token: 0x040037CF RID: 14287
		[SerializeField]
		private LoopListView2 view;

		// Token: 0x040037D0 RID: 14288
		[SerializeField]
		private GameObject original;

		// Token: 0x040037D1 RID: 14289
		[SerializeField]
		private int countPerRow = 3;

		// Token: 0x040037D2 RID: 14290
		private GameCharaFileScrollController.ScrollData[] scrollerDatas;

		// Token: 0x040037D4 RID: 14292
		public Action<GameCharaFileInfo> onSelect;

		// Token: 0x040037D5 RID: 14293
		public Action onDeSelect;

		// Token: 0x02000879 RID: 2169
		public class ScrollData
		{
			// Token: 0x040037D7 RID: 14295
			public int index;

			// Token: 0x040037D8 RID: 14296
			public GameCharaFileInfo info;
		}
	}
}
