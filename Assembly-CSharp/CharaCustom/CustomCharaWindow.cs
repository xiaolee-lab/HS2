using System;
using System.Collections.Generic;
using System.Linq;
using Illusion.Extensions;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009AA RID: 2474
	public class CustomCharaWindow : MonoBehaviour
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x001B6538 File Offset: 0x001B4938
		// (set) Token: 0x06004724 RID: 18212 RVA: 0x001B6545 File Offset: 0x001B4945
		public int sortType
		{
			get
			{
				return this._sortType.Value;
			}
			set
			{
				this._sortType.Value = value;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x001B6553 File Offset: 0x001B4953
		// (set) Token: 0x06004726 RID: 18214 RVA: 0x001B6560 File Offset: 0x001B4960
		public int sortOrder
		{
			get
			{
				return this._sortOrder.Value;
			}
			set
			{
				this._sortOrder.Value = value;
			}
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x001B6570 File Offset: 0x001B4970
		public void UpdateWindow(bool modeNew, int sex, bool save, List<CustomCharaFileInfo> _lst = null)
		{
			if (this.tglLoadOption != null && this.tglLoadOption.Length > 4 && null != this.tglLoadOption[4])
			{
				this.tglLoadOption[4].gameObject.SetActiveIfDifferent(modeNew);
			}
			if (_lst == null)
			{
				this.lstChara = CustomCharaFileInfoAssist.CreateCharaFileInfoList(0 == sex, 1 == sex, true, true, false, save);
			}
			else
			{
				this.lstChara = _lst;
			}
			this.Sort();
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x001B65F0 File Offset: 0x001B49F0
		public void UpdateWindowInUploader(bool modeNew, int sex, bool save, List<CustomCharaFileInfo> _lst = null)
		{
			if (this.tglLoadOption != null && this.tglLoadOption.Length > 4 && null != this.tglLoadOption[4])
			{
				this.tglLoadOption[4].gameObject.SetActiveIfDifferent(modeNew);
			}
			if (_lst == null)
			{
				this.lstChara = CustomCharaFileInfoAssist.CreateCharaFileInfoList(0 == sex, 1 == sex, true, false, false, save);
			}
			else
			{
				this.lstChara = _lst;
			}
			this.Sort();
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x001B6670 File Offset: 0x001B4A70
		public void Sort()
		{
			if (this.lstChara == null)
			{
				return;
			}
			if (this.lstChara.Count == 0)
			{
				this.cscChara.CreateList(this.lstChara);
				return;
			}
			using (new GameSystem.CultureScope())
			{
				if (this.sortType == 0)
				{
					if (this.sortOrder == 0)
					{
						this.lstChara = (from n in this.lstChara
						orderby n.time, n.name, n.personality
						select n).ToList<CustomCharaFileInfo>();
					}
					else
					{
						this.lstChara = (from n in this.lstChara
						orderby n.time descending, n.name descending, n.personality descending
						select n).ToList<CustomCharaFileInfo>();
					}
				}
				else if (this.sortOrder == 0)
				{
					this.lstChara = (from n in this.lstChara
					orderby n.name, n.time, n.personality
					select n).ToList<CustomCharaFileInfo>();
				}
				else
				{
					this.lstChara = (from n in this.lstChara
					orderby n.name descending, n.time descending, n.personality descending
					select n).ToList<CustomCharaFileInfo>();
				}
				this.cscChara.CreateList(this.lstChara);
			}
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x001B68FC File Offset: 0x001B4CFC
		public void SelectInfoClear()
		{
			if (null != this.cscChara)
			{
				this.cscChara.SelectInfoClear();
			}
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x001B691A File Offset: 0x001B4D1A
		public CustomCharaScrollController.ScrollData GetSelectInfo()
		{
			if (null != this.cscChara)
			{
				return this.cscChara.selectInfo;
			}
			return null;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x001B693C File Offset: 0x001B4D3C
		public void Start()
		{
			this.btnShowWinSort.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.winSort.objWinSort.SetActiveIfDifferent(!this.winSort.objWinSort.activeSelf);
			});
			this.winSort.btnCloseWinSort.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.winSort.objWinSort.SetActiveIfDifferent(false);
			});
			if (this.winSort.tglSort.Any<Toggle>())
			{
				(from tgl in this.winSort.tglSort.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where tgl.val != null
				select tgl).ToList().ForEach(delegate(tgl)
				{
					(from isOn in tgl.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.sortType = tgl.idx;
					});
				});
			}
			this.tglSortOrder.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
			{
				this.sortOrder = ((!isOn) ? 1 : 0);
			});
			this._sortType.Subscribe(delegate(int _)
			{
				this.Sort();
			});
			this._sortOrder.Subscribe(delegate(int _)
			{
				this.Sort();
			});
			if (this.button != null && this.button.Length == 3)
			{
				if (null != this.button[0])
				{
					this.button[0].OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.onClick01 != null)
						{
							Action<CustomCharaFileInfo> action = this.onClick01;
							CustomCharaScrollController.ScrollData selectInfo = this.cscChara.selectInfo;
							action((selectInfo != null) ? selectInfo.info : null);
						}
					});
					this.button[0].UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						this.button[0].interactable = (!this.btnDisableNotSelect01 || null != this.cscChara.selectInfo);
					});
				}
				if (null != this.button[1])
				{
					this.button[1].OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.onClick02 != null)
						{
							Action<CustomCharaFileInfo> action = this.onClick02;
							CustomCharaScrollController.ScrollData selectInfo = this.cscChara.selectInfo;
							action((selectInfo != null) ? selectInfo.info : null);
						}
					});
					this.button[1].UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						this.button[1].interactable = (!this.btnDisableNotSelect02 || null != this.cscChara.selectInfo);
					});
				}
				if (null != this.button[2])
				{
					this.button[2].OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						int num = 0;
						if (this.tglLoadOption[0].isOn)
						{
							num |= 1;
						}
						if (this.tglLoadOption[1].isOn)
						{
							num |= 2;
						}
						if (this.tglLoadOption[2].isOn)
						{
							num |= 4;
						}
						if (this.tglLoadOption[3].isOn)
						{
							num |= 8;
						}
						if (this.tglLoadOption[4].isOn)
						{
							num |= 16;
						}
						if (this.onClick03 != null)
						{
							Action<CustomCharaFileInfo, int> action = this.onClick03;
							CustomCharaScrollController.ScrollData selectInfo = this.cscChara.selectInfo;
							action((selectInfo != null) ? selectInfo.info : null, num);
						}
					});
					this.button[2].UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						this.button[2].interactable = (!this.btnDisableNotSelect03 || null != this.cscChara.selectInfo);
					});
				}
			}
		}

		// Token: 0x04004259 RID: 16985
		public CustomCharaScrollController cscChara;

		// Token: 0x0400425A RID: 16986
		[SerializeField]
		private CustomCharaWindow.SortWindow winSort;

		// Token: 0x0400425B RID: 16987
		[SerializeField]
		private Button btnShowWinSort;

		// Token: 0x0400425C RID: 16988
		[SerializeField]
		private Toggle tglSortOrder;

		// Token: 0x0400425D RID: 16989
		[SerializeField]
		private Toggle[] tglLoadOption;

		// Token: 0x0400425E RID: 16990
		[SerializeField]
		private Button[] button;

		// Token: 0x0400425F RID: 16991
		private IntReactiveProperty _sortType = new IntReactiveProperty(0);

		// Token: 0x04004260 RID: 16992
		private IntReactiveProperty _sortOrder = new IntReactiveProperty(1);

		// Token: 0x04004261 RID: 16993
		private List<CustomCharaFileInfo> lstChara;

		// Token: 0x04004262 RID: 16994
		public Action<CustomCharaFileInfo> onClick01;

		// Token: 0x04004263 RID: 16995
		public Action<CustomCharaFileInfo> onClick02;

		// Token: 0x04004264 RID: 16996
		public Action<CustomCharaFileInfo, int> onClick03;

		// Token: 0x04004265 RID: 16997
		public bool btnDisableNotSelect01;

		// Token: 0x04004266 RID: 16998
		public bool btnDisableNotSelect02;

		// Token: 0x04004267 RID: 16999
		public bool btnDisableNotSelect03;

		// Token: 0x020009AB RID: 2475
		public enum LoadOption
		{
			// Token: 0x04004278 RID: 17016
			Face = 1,
			// Token: 0x04004279 RID: 17017
			Body,
			// Token: 0x0400427A RID: 17018
			Hair = 4,
			// Token: 0x0400427B RID: 17019
			Coorde = 8,
			// Token: 0x0400427C RID: 17020
			Status = 16
		}

		// Token: 0x020009AC RID: 2476
		[Serializable]
		public class SortWindow
		{
			// Token: 0x0400427D RID: 17021
			public GameObject objWinSort;

			// Token: 0x0400427E RID: 17022
			public Button btnCloseWinSort;

			// Token: 0x0400427F RID: 17023
			public Toggle[] tglSort;
		}
	}
}
