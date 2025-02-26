using System;
using System.Collections.Generic;
using System.Linq;
using AIProject;
using Manager;
using SuperScrollView;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02001018 RID: 4120
	[Serializable]
	public class NetSelectHNScrollController : MonoBehaviour
	{
		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06008A64 RID: 35428 RVA: 0x003A348D File Offset: 0x003A188D
		private NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06008A65 RID: 35429 RVA: 0x003A3494 File Offset: 0x003A1894
		// (set) Token: 0x06008A66 RID: 35430 RVA: 0x003A349C File Offset: 0x003A189C
		public NetSelectHNScrollController.ScrollData selectInfo { get; private set; }

		// Token: 0x06008A67 RID: 35431 RVA: 0x003A34A5 File Offset: 0x003A18A5
		public void Start()
		{
			if (this.btnClose)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					this.ShowSelectHNWindow(false);
				});
			}
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x003A34D4 File Offset: 0x003A18D4
		public void Init()
		{
			List<NetSelectHNScrollController.ScrollData> list = new List<NetSelectHNScrollController.ScrollData>();
			using (Dictionary<int, NetworkInfo.UserInfo>.Enumerator enumerator = this.netInfo.dictUserInfo.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, NetworkInfo.UserInfo> n = enumerator.Current;
					int num = (from x in this.netInfo.lstCharaInfo
					where x.user_idx == n.Key
					select x).ToArray<NetworkInfo.CharaInfo>().Length;
					num += (from x in this.netInfo.lstHousingInfo
					where x.user_idx == n.Key
					select x).ToArray<NetworkInfo.HousingInfo>().Length;
					if (num != 0)
					{
						list.Add(new NetSelectHNScrollController.ScrollData
						{
							info = 
							{
								userIdx = n.Key,
								drawname = TextCorrectLimit.CorrectString(this.text, string.Format("({0}) {1}", num, n.Value.handleName), "…"),
								handlename = n.Value.handleName
							}
						});
					}
				}
			}
			using (new GameSystem.CultureScope())
			{
				list = (from n in list
				orderby n.info.handlename
				select n).ToList<NetSelectHNScrollController.ScrollData>();
			}
			this.scrollerDatas = list.ToArray();
			int num2 = (!this.scrollerDatas.IsNullOrEmpty<NetSelectHNScrollController.ScrollData>()) ? (this.scrollerDatas.Length / this.countPerRow) : 0;
			if (!this.scrollerDatas.IsNullOrEmpty<NetSelectHNScrollController.ScrollData>() && this.scrollerDatas.Length % this.countPerRow > 0)
			{
				num2++;
			}
			if (!this.view.IsInit)
			{
				this.view.InitListView(num2, new Func<LoopListView2, int, LoopListViewItem2>(this.OnUpdate), null);
			}
			else if (!this.view.SetListItemCount(num2, true))
			{
				this.view.RefreshAllShownItem();
			}
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x003A3710 File Offset: 0x003A1B10
		public void ShowSelectHNWindow(bool show)
		{
			if (show)
			{
				int hnidx = this.uiCtrl.searchSortHNIdx;
				this.noProc = true;
				this.selectInfo = ((hnidx != -1) ? this.scrollerDatas.FirstOrDefault((NetSelectHNScrollController.ScrollData d) => d.info.userIdx == hnidx) : null);
				this.SetNowSelectToggle();
				this.noProc = false;
				base.gameObject.SetActive(true);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x003A379C File Offset: 0x003A1B9C
		private void SetNowSelectToggle()
		{
			for (int i = 0; i < this.view.ShownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
				if (!(shownItemByIndex == null))
				{
					NetSelectHNScrollViewInfo component = shownItemByIndex.GetComponent<NetSelectHNScrollViewInfo>();
					for (int j = 0; j < this.countPerRow; j++)
					{
						component.SetToggleON(this.IsNowSelectInfo(component.GetListInfo()));
					}
				}
			}
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x003A3814 File Offset: 0x003A1C14
		private void OnValueChanged(bool _isOn, int _idx)
		{
			if (this.skip)
			{
				return;
			}
			this.skip = true;
			if (!this.noProc)
			{
				this.uiCtrl.searchSortHNIdx = ((!_isOn) ? -1 : this.scrollerDatas[_idx].info.userIdx);
				this.uiCtrl.changeSearchSetting = true;
			}
			if (_isOn)
			{
				bool flag = !this.IsNowSelectInfo(this.scrollerDatas[_idx].info);
				this.selectInfo = this.scrollerDatas[_idx];
				if (flag)
				{
					for (int i = 0; i < this.view.ShownItemCount; i++)
					{
						LoopListViewItem2 shownItemByIndex = this.view.GetShownItemByIndex(i);
						if (!(shownItemByIndex == null))
						{
							NetSelectHNScrollViewInfo component = shownItemByIndex.GetComponent<NetSelectHNScrollViewInfo>();
							for (int j = 0; j < this.countPerRow; j++)
							{
								if (!this.IsNowSelectInfo(component.GetListInfo()))
								{
									component.SetToggleON(false);
								}
							}
						}
					}
				}
			}
			else if (this.IsNowSelectInfo(this.scrollerDatas[_idx].info))
			{
				this.selectInfo = null;
			}
			this.skip = false;
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x003A3944 File Offset: 0x003A1D44
		private bool IsNowSelectInfo(NetworkInfo.SelectHNInfo _info)
		{
			return _info != null && this.selectInfo != null && this.selectInfo.info == _info;
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x003A3968 File Offset: 0x003A1D68
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			NetSelectHNScrollViewInfo component = loopListViewItem.GetComponent<NetSelectHNScrollViewInfo>();
			for (int i = 0; i < this.countPerRow; i++)
			{
				int index = _index * this.countPerRow + i;
				NetSelectHNScrollController.ScrollData scrollData = this.scrollerDatas.SafeGet(index);
				NetworkInfo.SelectHNInfo selectHNInfo = (scrollData != null) ? scrollData.info : null;
				component.SetData(selectHNInfo, delegate(bool _isOn)
				{
					this.OnValueChanged(_isOn, index);
				});
				this.noProc = true;
				component.SetToggleON(this.IsNowSelectInfo(selectHNInfo));
				this.noProc = false;
			}
			return loopListViewItem;
		}

		// Token: 0x040070AD RID: 28845
		public DownUIControl uiCtrl;

		// Token: 0x040070AE RID: 28846
		[SerializeField]
		private Button btnClose;

		// Token: 0x040070AF RID: 28847
		[SerializeField]
		private LoopListView2 view;

		// Token: 0x040070B0 RID: 28848
		[SerializeField]
		private GameObject original;

		// Token: 0x040070B1 RID: 28849
		[SerializeField]
		private int countPerRow = 3;

		// Token: 0x040070B2 RID: 28850
		[SerializeField]
		private Text text;

		// Token: 0x040070B3 RID: 28851
		private NetSelectHNScrollController.ScrollData[] scrollerDatas;

		// Token: 0x040070B5 RID: 28853
		private bool noProc;

		// Token: 0x040070B6 RID: 28854
		private bool skip;

		// Token: 0x02001019 RID: 4121
		public class ScrollData
		{
			// Token: 0x040070B8 RID: 28856
			public int index;

			// Token: 0x040070B9 RID: 28857
			public NetworkInfo.SelectHNInfo info = new NetworkInfo.SelectHNInfo();
		}
	}
}
