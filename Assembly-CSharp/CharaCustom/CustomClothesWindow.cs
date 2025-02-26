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
	// Token: 0x020009B4 RID: 2484
	public class CustomClothesWindow : MonoBehaviour
	{
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x001B77DE File Offset: 0x001B5BDE
		// (set) Token: 0x06004766 RID: 18278 RVA: 0x001B77EB File Offset: 0x001B5BEB
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

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x001B77F9 File Offset: 0x001B5BF9
		// (set) Token: 0x06004768 RID: 18280 RVA: 0x001B7806 File Offset: 0x001B5C06
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

		// Token: 0x06004769 RID: 18281 RVA: 0x001B7814 File Offset: 0x001B5C14
		public void UpdateWindow(bool modeNew, int sex, bool save)
		{
			if (this.tglLoadOption != null && this.tglLoadOption.Length > 4 && this.tglLoadOption[4])
			{
				this.tglLoadOption[4].gameObject.SetActiveIfDifferent(modeNew);
			}
			this.lstClothes = CustomClothesFileInfoAssist.CreateClothesFileInfoList(0 == sex, 1 == sex, true, !save);
			this.Sort();
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x001B7880 File Offset: 0x001B5C80
		public void Sort()
		{
			if (this.lstClothes == null)
			{
				return;
			}
			if (this.lstClothes.Count == 0)
			{
				this.cscClothes.CreateList(this.lstClothes);
				return;
			}
			using (new GameSystem.CultureScope())
			{
				if (this.sortType == 0)
				{
					if (this.sortOrder == 0)
					{
						this.lstClothes = (from n in this.lstClothes
						orderby n.time, n.name
						select n).ToList<CustomClothesFileInfo>();
					}
					else
					{
						this.lstClothes = (from n in this.lstClothes
						orderby n.time descending, n.name descending
						select n).ToList<CustomClothesFileInfo>();
					}
				}
				else if (this.sortOrder == 0)
				{
					this.lstClothes = (from n in this.lstClothes
					orderby n.name, n.time
					select n).ToList<CustomClothesFileInfo>();
				}
				else
				{
					this.lstClothes = (from n in this.lstClothes
					orderby n.name descending, n.time descending
					select n).ToList<CustomClothesFileInfo>();
				}
				this.cscClothes.CreateList(this.lstClothes);
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x001B7A84 File Offset: 0x001B5E84
		public void SelectInfoClear()
		{
			if (null != this.cscClothes)
			{
				this.cscClothes.SelectInfoClear();
			}
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x001B7AA4 File Offset: 0x001B5EA4
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
				if (this.button[0])
				{
					this.button[0].OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.onClick01 != null)
						{
							Action<CustomClothesFileInfo> action = this.onClick01;
							CustomClothesScrollController.ScrollData selectInfo = this.cscClothes.selectInfo;
							action((selectInfo != null) ? selectInfo.info : null);
						}
					});
					this.button[0].UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						this.button[0].interactable = (!this.btnDisableNotSelect01 || null != this.cscClothes.selectInfo);
					});
				}
				if (this.button[1])
				{
					this.button[1].OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.onClick02 != null)
						{
							Action<CustomClothesFileInfo> action = this.onClick02;
							CustomClothesScrollController.ScrollData selectInfo = this.cscClothes.selectInfo;
							action((selectInfo != null) ? selectInfo.info : null);
						}
					});
					this.button[1].UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						this.button[1].interactable = (!this.btnDisableNotSelect02 || null != this.cscClothes.selectInfo);
					});
				}
				if (this.button[2])
				{
					this.button[2].OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.onClick03 != null)
						{
							Action<CustomClothesFileInfo> action = this.onClick03;
							CustomClothesScrollController.ScrollData selectInfo = this.cscClothes.selectInfo;
							action((selectInfo != null) ? selectInfo.info : null);
						}
					});
					this.button[2].UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						this.button[2].interactable = (!this.btnDisableNotSelect03 || null != this.cscClothes.selectInfo);
					});
				}
			}
		}

		// Token: 0x0400429F RID: 17055
		[SerializeField]
		private CustomClothesScrollController cscClothes;

		// Token: 0x040042A0 RID: 17056
		[SerializeField]
		private CustomClothesWindow.SortWindow winSort;

		// Token: 0x040042A1 RID: 17057
		[SerializeField]
		private Button btnShowWinSort;

		// Token: 0x040042A2 RID: 17058
		[SerializeField]
		private Toggle tglSortOrder;

		// Token: 0x040042A3 RID: 17059
		[SerializeField]
		private Toggle[] tglLoadOption;

		// Token: 0x040042A4 RID: 17060
		[SerializeField]
		private Button[] button;

		// Token: 0x040042A5 RID: 17061
		private IntReactiveProperty _sortType = new IntReactiveProperty(0);

		// Token: 0x040042A6 RID: 17062
		private IntReactiveProperty _sortOrder = new IntReactiveProperty(1);

		// Token: 0x040042A7 RID: 17063
		private List<CustomClothesFileInfo> lstClothes;

		// Token: 0x040042A8 RID: 17064
		public Action<CustomClothesFileInfo> onClick01;

		// Token: 0x040042A9 RID: 17065
		public Action<CustomClothesFileInfo> onClick02;

		// Token: 0x040042AA RID: 17066
		public Action<CustomClothesFileInfo> onClick03;

		// Token: 0x040042AB RID: 17067
		public bool btnDisableNotSelect01;

		// Token: 0x040042AC RID: 17068
		public bool btnDisableNotSelect02;

		// Token: 0x040042AD RID: 17069
		public bool btnDisableNotSelect03;

		// Token: 0x020009B5 RID: 2485
		[Serializable]
		public class SortWindow
		{
			// Token: 0x040042B9 RID: 17081
			public GameObject objWinSort;

			// Token: 0x040042BA RID: 17082
			public Button btnCloseWinSort;

			// Token: 0x040042BB RID: 17083
			public Toggle[] tglSort;
		}
	}
}
