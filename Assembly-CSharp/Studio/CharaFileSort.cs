using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020011D2 RID: 4562
	[Serializable]
	public class CharaFileSort
	{
		// Token: 0x060095B5 RID: 38325 RVA: 0x003DE061 File Offset: 0x003DC461
		public CharaFileSort()
		{
			this.m_Select = -1;
			this.sortKind = -1;
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x060095B6 RID: 38326 RVA: 0x003DE09D File Offset: 0x003DC49D
		// (set) Token: 0x060095B7 RID: 38327 RVA: 0x003DE0A8 File Offset: 0x003DC4A8
		public int select
		{
			get
			{
				return this.m_Select;
			}
			set
			{
				int select = this.m_Select;
				if (Utility.SetStruct<int>(ref this.m_Select, value))
				{
					if (MathfEx.RangeEqualOn<int>(0, select, this.cfiList.Count))
					{
						this.cfiList[select].select = false;
					}
					if (MathfEx.RangeEqualOn<int>(0, this.m_Select, this.cfiList.Count))
					{
						this.cfiList[this.m_Select].select = true;
					}
				}
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x060095B8 RID: 38328 RVA: 0x003DE129 File Offset: 0x003DC529
		// (set) Token: 0x060095B9 RID: 38329 RVA: 0x003DE131 File Offset: 0x003DC531
		public int sortKind { get; private set; }

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x060095BA RID: 38330 RVA: 0x003DE13C File Offset: 0x003DC53C
		public string selectPath
		{
			get
			{
				if (this.cfiList.Count == 0)
				{
					return string.Empty;
				}
				if (!MathfEx.RangeEqualOn<int>(0, this.select, this.cfiList.Count - 1))
				{
					return string.Empty;
				}
				return this.cfiList[this.select].file;
			}
		}

		// Token: 0x060095BB RID: 38331 RVA: 0x003DE19C File Offset: 0x003DC59C
		public void DeleteAllNode()
		{
			int childCount = this.root.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UnityEngine.Object.Destroy(this.root.GetChild(i).gameObject);
			}
			this.root.DetachChildren();
			this.m_Select = -1;
		}

		// Token: 0x060095BC RID: 38332 RVA: 0x003DE1F0 File Offset: 0x003DC5F0
		public void Sort(int _type, bool _ascend)
		{
			this.sortKind = _type;
			int sortKind = this.sortKind;
			if (sortKind != 0)
			{
				if (sortKind == 1)
				{
					this.SortTime(_ascend);
				}
			}
			else
			{
				this.SortName(_ascend);
			}
			for (int i = 0; i < this.imageSort.Length; i++)
			{
				this.imageSort[i].enabled = (i == this.sortKind);
			}
		}

		// Token: 0x060095BD RID: 38333 RVA: 0x003DE264 File Offset: 0x003DC664
		public void Sort(int _type)
		{
			this.Sort(_type, (this.sortKind != _type) ? this.sortType[_type] : (!this.sortType[_type]));
		}

		// Token: 0x060095BE RID: 38334 RVA: 0x003DE294 File Offset: 0x003DC694
		private void SortName(bool _ascend)
		{
			if (this.cfiList.IsNullOrEmpty<CharaFileInfo>())
			{
				return;
			}
			this.sortType[0] = _ascend;
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ja-JP");
			if (_ascend)
			{
				this.cfiList.Sort((CharaFileInfo a, CharaFileInfo b) => a.name.CompareTo(b.name));
			}
			else
			{
				this.cfiList.Sort((CharaFileInfo a, CharaFileInfo b) => b.name.CompareTo(a.name));
			}
			Thread.CurrentThread.CurrentCulture = currentCulture;
			for (int i = 0; i < this.cfiList.Count; i++)
			{
				this.cfiList[i].index = i;
				this.cfiList[i].siblingIndex = i;
			}
			this.select = this.cfiList.FindIndex((CharaFileInfo v) => v.select);
			this.imageSort[0].sprite = this.spriteSort[(!this.sortType[0]) ? 1 : 0];
		}

		// Token: 0x060095BF RID: 38335 RVA: 0x003DE3D8 File Offset: 0x003DC7D8
		private void SortTime(bool _ascend)
		{
			if (this.cfiList.IsNullOrEmpty<CharaFileInfo>())
			{
				return;
			}
			this.sortType[1] = _ascend;
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ja-JP");
			if (_ascend)
			{
				this.cfiList.Sort((CharaFileInfo a, CharaFileInfo b) => a.time.CompareTo(b.time));
			}
			else
			{
				this.cfiList.Sort((CharaFileInfo a, CharaFileInfo b) => b.time.CompareTo(a.time));
			}
			Thread.CurrentThread.CurrentCulture = currentCulture;
			for (int i = 0; i < this.cfiList.Count; i++)
			{
				this.cfiList[i].index = i;
				this.cfiList[i].siblingIndex = i;
			}
			this.select = this.cfiList.FindIndex((CharaFileInfo v) => v.select);
			this.imageSort[1].sprite = this.spriteSort[(!this.sortType[1]) ? 1 : 0];
		}

		// Token: 0x0400788D RID: 30861
		public Transform root;

		// Token: 0x0400788E RID: 30862
		public Image[] imageSort;

		// Token: 0x0400788F RID: 30863
		public Sprite[] spriteSort;

		// Token: 0x04007890 RID: 30864
		public List<CharaFileInfo> cfiList = new List<CharaFileInfo>();

		// Token: 0x04007891 RID: 30865
		private int m_Select = -1;

		// Token: 0x04007893 RID: 30867
		private bool[] sortType = new bool[]
		{
			true,
			true
		};
	}
}
