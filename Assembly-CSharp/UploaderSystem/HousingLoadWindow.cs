using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Housing;
using Housing.SaveLoad;
using Illusion.Extensions;
using Manager;
using SuperScrollView;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02000FF2 RID: 4082
	[Serializable]
	public class HousingLoadWindow : MonoBehaviour
	{
		// Token: 0x0600895C RID: 35164 RVA: 0x00393298 File Offset: 0x00391698
		public void Init()
		{
			foreach (KeyValuePair<int, Housing.AreaSizeInfo> keyValuePair in Singleton<Housing>.Instance.dicAreaSizeInfo)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tabOriginal, this.tabRoot);
				gameObject.SetActive(true);
				SizeTab component = gameObject.GetComponent<SizeTab>();
				int type = keyValuePair.Value.no;
				component.Text = string.Format("{0}X{1}X{2}", keyValuePair.Value.limitSize.x, keyValuePair.Value.limitSize.y, keyValuePair.Value.limitSize.z);
				(from _b in component.toggle.OnValueChangedAsObservable()
				where _b
				select _b).Subscribe(delegate(bool _)
				{
					this.SelectTab(type, false);
				});
				this.dicTabInfo[type] = new HousingLoadWindow.TabInfo
				{
					gameObject = gameObject,
					sizeTab = component
				};
			}
			for (int i = 0; i < this.buttonsThumbnail.Length; i++)
			{
				int idx = i;
				this.buttonsThumbnail[i].OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					int num = this.thumbnailLimit * this.page + idx;
					if (this.backSelect != num)
					{
						this.select = num;
					}
					else
					{
						this.select = -1;
					}
					this.UpdateSelectImage();
					if (this.onSelect != null)
					{
						this.onSelect(this.select);
					}
					this.backSelect = this.select;
				});
			}
			this.dicTabInfo[0].IsOn = true;
			this.SelectTab(0, false);
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x0039346C File Offset: 0x0039186C
		private void InitInfo(int _idx)
		{
			string path = UserData.Create(string.Format("{0}{1:00}/", "housing/", _idx + 1));
			List<KeyValuePair<DateTime, string>> list = (from s in Directory.EnumerateFiles(path, "*.png")
			select new HousingLoadWindow.FileInfo(s) into fi
			where fi.CraftInfo != null
			where fi.Check(_idx)
			select new KeyValuePair<DateTime, string>(File.GetLastWriteTime(fi.Path), fi.Path)).ToList<KeyValuePair<DateTime, string>>();
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ja-JP");
			list.Sort((KeyValuePair<DateTime, string> a, KeyValuePair<DateTime, string> b) => b.Key.CompareTo(a.Key));
			Thread.CurrentThread.CurrentCulture = currentCulture;
			this.listPath = (from v in list
			select v.Value).ToList<string>();
			this.thumbnailNum = ((!this.listPath.IsNullOrEmpty<string>()) ? this.listPath.Count : 0);
			this.pageNum = this.thumbnailNum / this.thumbnailLimit + ((this.thumbnailNum % this.thumbnailLimit == 0) ? 0 : 1);
			this.pageNum = Mathf.Max(this.pageNum, 1);
			if (!this.view.IsInit)
			{
				this.view.InitListView(this.pageNum, (LoopListView2 _view, int _index) => this.OnUpdate(_view, _index), null);
			}
			else if (!this.view.SetListItemCount(this.pageNum, true))
			{
				this.view.RefreshAllShownItem();
			}
		}

		// Token: 0x0600895E RID: 35166 RVA: 0x00393670 File Offset: 0x00391A70
		private void SelectTab(int _idx, bool _force = false)
		{
			if (!_force && this.tab == _idx)
			{
				return;
			}
			this.InitInfo(_idx);
			this.select = -1;
			this.backSelect = -99;
			if (this.onSelect != null)
			{
				this.onSelect(this.select);
			}
			this.tab = _idx;
			this.SetPage(0, true);
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x003936D4 File Offset: 0x00391AD4
		private void SetPage(int _page, bool _force = false)
		{
			if (!_force && this.page == _page)
			{
				return;
			}
			_page = Mathf.Clamp(_page, 0, this.pageNum - 1);
			int num = this.thumbnailLimit * _page;
			for (int i = 0; i < this.thumbnailLimit; i++)
			{
				int num2 = num + i;
				if (!MathfEx.RangeEqualOn<int>(0, num2, this.thumbnailNum - 1))
				{
					if (_page == 0 && i == 0)
					{
						this.rawsThumbnail[i].texture = this.textureNoData;
						this.rawsThumbnail[i].enabled = true;
					}
					else
					{
						this.rawsThumbnail[i].enabled = false;
					}
					this.buttonsThumbnail[i].enabled = false;
				}
				else
				{
					this.rawsThumbnail[i].texture = PngAssist.LoadTexture(this.listPath[num2]);
					this.rawsThumbnail[i].enabled = true;
					this.buttonsThumbnail[i].enabled = true;
					this.buttonsThumbnail[i].interactable = true;
				}
			}
			this.page = _page;
			this.UpdateSelectImage();
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x003937F0 File Offset: 0x00391BF0
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			PageButton pageButton = loopListViewItem as PageButton;
			if (pageButton != null)
			{
				pageButton.SetData(_index, this.page == _index, delegate(int _i)
				{
					this.SetPage(_i, false);
				});
			}
			return loopListViewItem;
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x00393844 File Offset: 0x00391C44
		public void UpdateSelectImage()
		{
			for (int i = 0; i < this.rawsSelect.Length; i++)
			{
				if (null != this.rawsSelect[i])
				{
					this.rawsSelect[i].enabled = false;
				}
			}
			int num = this.thumbnailLimit * this.page;
			if (this.select >= num && this.select <= num + this.thumbnailLimit)
			{
				int num2 = this.select - num;
				this.rawsSelect[num2].enabled = true;
			}
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x003938CF File Offset: 0x00391CCF
		public int GetSelectIndex()
		{
			return this.select;
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x003938D7 File Offset: 0x00391CD7
		public string GetSelectPath()
		{
			if (this.listPath == null)
			{
				return string.Empty;
			}
			if (this.select == -1)
			{
				return string.Empty;
			}
			return this.listPath[this.select];
		}

		// Token: 0x04006F4E RID: 28494
		[SerializeField]
		private Transform tabRoot;

		// Token: 0x04006F4F RID: 28495
		[SerializeField]
		private GameObject tabOriginal;

		// Token: 0x04006F50 RID: 28496
		[SerializeField]
		private RawImage[] rawsThumbnail;

		// Token: 0x04006F51 RID: 28497
		[SerializeField]
		private Image[] rawsSelect;

		// Token: 0x04006F52 RID: 28498
		[SerializeField]
		private Button[] buttonsThumbnail;

		// Token: 0x04006F53 RID: 28499
		[SerializeField]
		private Texture textureNoData;

		// Token: 0x04006F54 RID: 28500
		[SerializeField]
		[Header("一覧関係")]
		private LoopListView2 view;

		// Token: 0x04006F55 RID: 28501
		[SerializeField]
		private GameObject original;

		// Token: 0x04006F56 RID: 28502
		private List<string> listPath;

		// Token: 0x04006F57 RID: 28503
		private int thumbnailNum = -1;

		// Token: 0x04006F58 RID: 28504
		private int thumbnailLimit = 15;

		// Token: 0x04006F59 RID: 28505
		private int pageNum = -1;

		// Token: 0x04006F5A RID: 28506
		private int tab = -1;

		// Token: 0x04006F5B RID: 28507
		private int page;

		// Token: 0x04006F5C RID: 28508
		private int select = -1;

		// Token: 0x04006F5D RID: 28509
		private int backSelect = -99;

		// Token: 0x04006F5E RID: 28510
		private Dictionary<int, HousingLoadWindow.TabInfo> dicTabInfo = new Dictionary<int, HousingLoadWindow.TabInfo>();

		// Token: 0x04006F5F RID: 28511
		public Action<int> onSelect;

		// Token: 0x02000FF3 RID: 4083
		private class TabInfo
		{
			// Token: 0x17001E0D RID: 7693
			// (set) Token: 0x0600896C RID: 35180 RVA: 0x0039397E File Offset: 0x00391D7E
			public bool Interactable
			{
				set
				{
					this.sizeTab.toggle.interactable = value;
				}
			}

			// Token: 0x17001E0E RID: 7694
			// (set) Token: 0x0600896D RID: 35181 RVA: 0x00393991 File Offset: 0x00391D91
			public bool IsOn
			{
				set
				{
					this.sizeTab.toggle.isOn = value;
				}
			}

			// Token: 0x17001E0F RID: 7695
			// (set) Token: 0x0600896E RID: 35182 RVA: 0x003939A4 File Offset: 0x00391DA4
			public bool IsOnWithoutCallback
			{
				set
				{
					this.sizeTab.toggle.SetIsOnWithoutCallback(value);
				}
			}

			// Token: 0x04006F66 RID: 28518
			public GameObject gameObject;

			// Token: 0x04006F67 RID: 28519
			public SizeTab sizeTab;
		}

		// Token: 0x02000FF4 RID: 4084
		private class FileInfo
		{
			// Token: 0x0600896F RID: 35183 RVA: 0x003939B7 File Offset: 0x00391DB7
			public FileInfo(string _path)
			{
				this.Path = _path;
				this.CraftInfo = CraftInfo.LoadStatic(_path);
			}

			// Token: 0x17001E10 RID: 7696
			// (get) Token: 0x06008970 RID: 35184 RVA: 0x003939D2 File Offset: 0x00391DD2
			// (set) Token: 0x06008971 RID: 35185 RVA: 0x003939DA File Offset: 0x00391DDA
			public string Path { get; private set; }

			// Token: 0x17001E11 RID: 7697
			// (get) Token: 0x06008972 RID: 35186 RVA: 0x003939E3 File Offset: 0x00391DE3
			// (set) Token: 0x06008973 RID: 35187 RVA: 0x003939EB File Offset: 0x00391DEB
			public CraftInfo CraftInfo { get; private set; }

			// Token: 0x06008974 RID: 35188 RVA: 0x003939F4 File Offset: 0x00391DF4
			public bool Check(int _areaType)
			{
				return this.CraftInfo != null && _areaType == Singleton<Housing>.Instance.GetSizeType(this.CraftInfo.AreaNo);
			}
		}
	}
}
