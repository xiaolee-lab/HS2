using System;
using System.Collections.Generic;
using System.Linq;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009B7 RID: 2487
	[Serializable]
	public class CustomPushScrollController : MonoBehaviour
	{
		// Token: 0x06004787 RID: 18311 RVA: 0x001B7F80 File Offset: 0x001B6380
		public void Start()
		{
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x001B7F84 File Offset: 0x001B6384
		public void CreateList(List<CustomPushInfo> _lst)
		{
			this.scrollerDatas = _lst.Select((CustomPushInfo value, int idx) => new CustomPushScrollController.ScrollData
			{
				index = idx,
				info = value
			}).ToArray<CustomPushScrollController.ScrollData>();
			int num = (!this.scrollerDatas.IsNullOrEmpty<CustomPushScrollController.ScrollData>()) ? (this.scrollerDatas.Length / this.countPerRow) : 0;
			if (!this.scrollerDatas.IsNullOrEmpty<CustomPushScrollController.ScrollData>() && this.scrollerDatas.Length % this.countPerRow > 0)
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

		// Token: 0x06004789 RID: 18313 RVA: 0x001B8072 File Offset: 0x001B6472
		public void SetTopLine()
		{
			if (this.view.IsInit)
			{
				this.view.MovePanelToItemIndex(0, 0f);
			}
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x001B8095 File Offset: 0x001B6495
		public void SetLine(int _line)
		{
			this.view.MovePanelToItemIndex(_line, 0f);
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x001B80A8 File Offset: 0x001B64A8
		private void OnClick(CustomPushScrollController.ScrollData _data)
		{
			if (this.onPush != null)
			{
				this.onPush((_data != null) ? _data.info : null);
			}
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x001B80D1 File Offset: 0x001B64D1
		private void OnPointerEnter(string name)
		{
			if (this.text)
			{
				this.text.text = name;
			}
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x001B80EF File Offset: 0x001B64EF
		private void OnPointerExit()
		{
			if (this.text)
			{
				this.text.text = this.SelectName;
			}
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001B8114 File Offset: 0x001B6514
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			CustomPushScrollViewInfo component = loopListViewItem.GetComponent<CustomPushScrollViewInfo>();
			for (int i = 0; i < this.countPerRow; i++)
			{
				int index = _index * this.countPerRow + i;
				CustomPushScrollController.ScrollData data = this.scrollerDatas.SafeGet(index);
				CustomPushScrollViewInfo customPushScrollViewInfo = component;
				int index2 = i;
				CustomPushScrollController.ScrollData data2 = data;
				customPushScrollViewInfo.SetData(index2, (data2 != null) ? data2.info : null, delegate
				{
					this.OnClick(data);
				}, new Action<string>(this.OnPointerEnter), new Action(this.OnPointerExit));
			}
			return loopListViewItem;
		}

		// Token: 0x040042C1 RID: 17089
		[SerializeField]
		private LoopListView2 view;

		// Token: 0x040042C2 RID: 17090
		[SerializeField]
		private GameObject original;

		// Token: 0x040042C3 RID: 17091
		[SerializeField]
		private int countPerRow = 5;

		// Token: 0x040042C4 RID: 17092
		[SerializeField]
		private Text text;

		// Token: 0x040042C5 RID: 17093
		private string SelectName = string.Empty;

		// Token: 0x040042C6 RID: 17094
		private CustomPushScrollController.ScrollData[] scrollerDatas;

		// Token: 0x040042C7 RID: 17095
		public Action<CustomPushInfo> onPush;

		// Token: 0x020009B8 RID: 2488
		public class ScrollData
		{
			// Token: 0x040042C9 RID: 17097
			public int index;

			// Token: 0x040042CA RID: 17098
			public CustomPushInfo info = new CustomPushInfo();
		}
	}
}
