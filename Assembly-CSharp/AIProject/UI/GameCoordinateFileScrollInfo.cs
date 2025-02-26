using System;
using System.Collections.Generic;
using System.Linq;
using SuperScrollView;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000E58 RID: 3672
	[Serializable]
	public class GameCoordinateFileScrollInfo
	{
		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06007430 RID: 29744 RVA: 0x00317143 File Offset: 0x00315543
		// (set) Token: 0x06007431 RID: 29745 RVA: 0x0031714B File Offset: 0x0031554B
		public GameCoordinateFileScrollInfo.ScrollData SelectData { get; private set; }

		// Token: 0x06007432 RID: 29746 RVA: 0x00317154 File Offset: 0x00315554
		public void Init(List<GameCoordinateFileInfo> lst)
		{
			this._scrollDatas = lst.Select((GameCoordinateFileInfo value, int idx) => new GameCoordinateFileScrollInfo.ScrollData
			{
				index = idx,
				info = value
			}).ToArray<GameCoordinateFileScrollInfo.ScrollData>();
			int num = (!this._scrollDatas.IsNullOrEmpty<GameCoordinateFileScrollInfo.ScrollData>()) ? (this._scrollDatas.Length / this._countPerRow) : 0;
			if (!this._scrollDatas.IsNullOrEmpty<GameCoordinateFileScrollInfo.ScrollData>() && this._scrollDatas.Length % this._countPerRow > 0)
			{
				num++;
			}
			if (!this._view.IsInit)
			{
				this._view.InitListViewAndSize(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnUpdate));
			}
			else
			{
				this._view.ReSetListItemCount(num);
			}
		}

		// Token: 0x06007433 RID: 29747 RVA: 0x00317217 File Offset: 0x00315617
		public void SelectDataClear()
		{
			this.SelectData = null;
		}

		// Token: 0x06007434 RID: 29748 RVA: 0x00317220 File Offset: 0x00315620
		public void SetTopLine()
		{
			if (this._view.IsInit)
			{
				this._view.MovePanelToItemIndex(0, 0f);
			}
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x00317243 File Offset: 0x00315643
		public void SetToggle(int index)
		{
			this.SelectData = this._scrollDatas[index];
			this._view.RefreshAllShownItem();
		}

		// Token: 0x06007436 RID: 29750 RVA: 0x00317260 File Offset: 0x00315660
		public void SetNowSelectToggle()
		{
			for (int i = 0; i < this._view.ShownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this._view.GetShownItemByIndex(i);
				if (!(shownItemByIndex == null))
				{
					GameCoordinateFileInfoComponent component = shownItemByIndex.GetComponent<GameCoordinateFileInfoComponent>();
					for (int j = 0; j < this._countPerRow; j++)
					{
						component.SetToggleOn(j, this.IsNowSelectInfo(component.GetListInfo(j)));
					}
				}
			}
		}

		// Token: 0x06007437 RID: 29751 RVA: 0x003172D9 File Offset: 0x003156D9
		public void SetLine(int line)
		{
			this._view.MovePanelToItemIndex(line, 0f);
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x003172EC File Offset: 0x003156EC
		public void SetNowLine()
		{
			int itemIndex = 0;
			if (this.SelectData != null)
			{
				itemIndex = this.SelectData.index / this._countPerRow;
			}
			this._view.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x06007439 RID: 29753 RVA: 0x0031732C File Offset: 0x0031572C
		private void OnValueChange(GameCoordinateFileScrollInfo.ScrollData data, bool isOn)
		{
			if (isOn)
			{
				bool flag = !this.IsNowSelectInfo((data != null) ? data.info : null);
				this.SelectData = data;
				if (flag)
				{
					for (int i = 0; i < this._view.ShownItemCount; i++)
					{
						LoopListViewItem2 shownItemByIndex = this._view.GetShownItemByIndex(i);
						if (!(shownItemByIndex == null))
						{
							GameCoordinateFileInfoComponent component = shownItemByIndex.GetComponent<GameCoordinateFileInfoComponent>();
							for (int j = 0; j < this._countPerRow; j++)
							{
								if (!this.IsNowSelectInfo(component.GetListInfo(j)))
								{
									component.SetToggleOn(j, false);
								}
							}
						}
					}
					if (this.OnSelect != null)
					{
						this.OnSelect(this.SelectData.info);
					}
				}
			}
			else if (this.IsNowSelectInfo((data != null) ? data.info : null))
			{
				this.SelectData = null;
				if (this.OnDeselect != null)
				{
					this.OnDeselect();
				}
			}
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x0031743E File Offset: 0x0031583E
		private bool IsNowSelectInfo(GameCoordinateFileInfo info)
		{
			return info != null && this.SelectData != null && this.SelectData.info.FullPath == info.FullPath;
		}

		// Token: 0x0600743B RID: 29755 RVA: 0x00317470 File Offset: 0x00315870
		private LoopListViewItem2 OnUpdate(LoopListView2 view, int index)
		{
			if (index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = view.NewListViewItem(this._original.name);
			GameCoordinateFileInfoComponent component = loopListViewItem.GetComponent<GameCoordinateFileInfoComponent>();
			for (int i = 0; i < this._countPerRow; i++)
			{
				int index2 = index * this._countPerRow + i;
				GameCoordinateFileScrollInfo.ScrollData data = this._scrollDatas.SafeGet(index2);
				GameCoordinateFileInfoComponent gameCoordinateFileInfoComponent = component;
				int index3 = i;
				GameCoordinateFileScrollInfo.ScrollData data3 = data;
				gameCoordinateFileInfoComponent.SetData(index3, (data3 != null) ? data3.info : null, delegate(bool isOn)
				{
					this.OnValueChange(data, isOn);
				});
				GameCoordinateFileInfoComponent gameCoordinateFileInfoComponent2 = component;
				int index4 = i;
				GameCoordinateFileScrollInfo.ScrollData data2 = data;
				gameCoordinateFileInfoComponent2.SetToggleOn(index4, this.IsNowSelectInfo((data2 != null) ? data2.info : null));
			}
			return loopListViewItem;
		}

		// Token: 0x04005EFC RID: 24316
		[SerializeField]
		private LoopListView2 _view;

		// Token: 0x04005EFD RID: 24317
		[SerializeField]
		private GameObject _original;

		// Token: 0x04005EFE RID: 24318
		[SerializeField]
		private int _countPerRow = 3;

		// Token: 0x04005EFF RID: 24319
		private GameCoordinateFileScrollInfo.ScrollData[] _scrollDatas;

		// Token: 0x04005F01 RID: 24321
		public Action<GameCoordinateFileInfo> OnSelect;

		// Token: 0x04005F02 RID: 24322
		public Action OnDeselect;

		// Token: 0x02000E59 RID: 3673
		public class ScrollData
		{
			// Token: 0x04005F04 RID: 24324
			public int index;

			// Token: 0x04005F05 RID: 24325
			public GameCoordinateFileInfo info;
		}
	}
}
