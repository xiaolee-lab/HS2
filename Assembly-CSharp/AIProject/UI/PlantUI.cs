using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E79 RID: 3705
	public class PlantUI : MenuUIBehaviour
	{
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06007628 RID: 30248 RVA: 0x003212C8 File Offset: 0x0031F6C8
		// (remove) Token: 0x06007629 RID: 30249 RVA: 0x00321300 File Offset: 0x0031F700
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PlantIcon> IconChanged;

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x0600762A RID: 30250 RVA: 0x00321336 File Offset: 0x0031F736
		// (set) Token: 0x0600762B RID: 30251 RVA: 0x0032133E File Offset: 0x0031F73E
		public UnityEvent OnSubmitRemove { get; private set; } = new UnityEvent();

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x0600762C RID: 30252 RVA: 0x00321347 File Offset: 0x0031F747
		// (set) Token: 0x0600762D RID: 30253 RVA: 0x0032134F File Offset: 0x0031F74F
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x0600762E RID: 30254 RVA: 0x00321358 File Offset: 0x0031F758
		public PlantIcon currentIcon
		{
			[CompilerGenerated]
			get
			{
				return this._plantIcons[this._currentIndex];
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x0600762F RID: 30255 RVA: 0x00321367 File Offset: 0x0031F767
		public int currentIndex
		{
			[CompilerGenerated]
			get
			{
				return this._currentIndex;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06007630 RID: 30256 RVA: 0x0032136F File Offset: 0x0031F76F
		public PlantIcon[] plantIcons
		{
			[CompilerGenerated]
			get
			{
				return this._plantIcons;
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06007631 RID: 30257 RVA: 0x00321377 File Offset: 0x0031F777
		public IObservable<Unit> AllGet
		{
			[CompilerGenerated]
			get
			{
				return this._allGetButton.OnClickAsObservable();
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06007632 RID: 30258 RVA: 0x00321384 File Offset: 0x0031F784
		// (set) Token: 0x06007633 RID: 30259 RVA: 0x0032138C File Offset: 0x0031F78C
		private PlantIcon[] _plantIcons { get; set; }

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06007634 RID: 30260 RVA: 0x00321395 File Offset: 0x0031F795
		// (set) Token: 0x06007635 RID: 30261 RVA: 0x0032139D File Offset: 0x0031F79D
		private int _currentIndex { get; set; } = -1;

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06007636 RID: 30262 RVA: 0x003213A6 File Offset: 0x0031F7A6
		// (set) Token: 0x06007637 RID: 30263 RVA: 0x003213AE File Offset: 0x0031F7AE
		private List<AIProject.SaveData.Environment.PlantInfo> _plantList { get; set; }

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06007638 RID: 30264 RVA: 0x003213B7 File Offset: 0x0031F7B7
		private CompositeDisposable disposable { get; } = new CompositeDisposable();

		// Token: 0x06007639 RID: 30265 RVA: 0x003213C0 File Offset: 0x0031F7C0
		public void SetPlantItem(StuffItem item)
		{
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			if (item2 == null)
			{
				return;
			}
			AIProject.SaveData.Environment.PlantInfo plantInfo = Singleton<Manager.Resources>.Instance.GameInfo.GetPlantInfo(item2.nameHash);
			this.currentIcon.info = plantInfo;
			this._plantList[this._currentIndex] = plantInfo;
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x00321424 File Offset: 0x0031F824
		public int GetEmptySum()
		{
			return this._plantList.Count((AIProject.SaveData.Environment.PlantInfo x) => x == null);
		}

		// Token: 0x0600763B RID: 30267 RVA: 0x00321450 File Offset: 0x0031F850
		public void SetPlantItemForAll(StuffItem item, int count)
		{
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			if (item2 == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this._plantList.Count; i++)
			{
				if (this._plantList[i] == null)
				{
					if (num++ >= count)
					{
						break;
					}
					AIProject.SaveData.Environment.PlantInfo plantInfo = Singleton<Manager.Resources>.Instance.GameInfo.GetPlantInfo(item2.nameHash);
					this._plantIcons[i].info = plantInfo;
					this._plantList[i] = plantInfo;
				}
			}
		}

		// Token: 0x0600763C RID: 30268 RVA: 0x003214F8 File Offset: 0x0031F8F8
		public void Open(List<AIProject.SaveData.Environment.PlantInfo> plantList)
		{
			this._plantList = plantList;
			int count = plantList.Count;
			for (int i = 0; i < this._plantIcons.Length; i++)
			{
				bool flag = i < count;
				this._plantIcons[i].visible = flag;
				if (flag)
				{
					this._plantIcons[i].info = plantList[i];
				}
				else
				{
					this._plantIcons[i].info = null;
				}
			}
			foreach (PlantIcon plantIcon in this._plantIcons)
			{
				plantIcon.toggle.isOn = false;
			}
			this._plantIcons[0].toggle.isOn = true;
			this._cursor.enabled = false;
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x003215BE File Offset: 0x0031F9BE
		public void Refresh()
		{
			if (this.IconChanged != null)
			{
				this.IconChanged(this.currentIcon);
			}
		}

		// Token: 0x0600763E RID: 30270 RVA: 0x003215E0 File Offset: 0x0031F9E0
		private IEnumerator BindingUI()
		{
			this._plantIcons = (from t in this._farmland.Children()
			select t.GetComponent<PlantIcon>()).ToArray<PlantIcon>();
			(from p in this._plantIcons
			select p.toggle).BindToEnter(true, this._cursor).AddTo(this.disposable);
			(from p in this._plantIcons
			select p.toggle).BindToGroup(delegate(int sel)
			{
				this._currentIndex = sel;
				if (this.IconChanged != null)
				{
					this.IconChanged(this.currentIcon);
				}
			}).AddTo(this.disposable);
			yield break;
		}

		// Token: 0x0600763F RID: 30271 RVA: 0x003215FC File Offset: 0x0031F9FC
		protected override void Start()
		{
			this._canvasGroup.alpha = 1f;
			base.StartCoroutine(this.BindingUI());
			this.IsActiveControl = true;
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand
			{
				ActionID = ActionID.MouseRight
			};
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			ActionIDDownCommand actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.SquareX
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.Submit
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			base.Start();
		}

		// Token: 0x06007640 RID: 30272 RVA: 0x003216FF File Offset: 0x0031FAFF
		private void OnDestroy()
		{
			this.disposable.Clear();
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x0032170C File Offset: 0x0031FB0C
		private void OnInputSubmit()
		{
			UnityEvent onSubmitRemove = this.OnSubmitRemove;
			if (onSubmitRemove != null)
			{
				onSubmitRemove.Invoke();
			}
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x00321722 File Offset: 0x0031FB22
		private void OnInputCancel()
		{
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x0400602C RID: 24620
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400602D RID: 24621
		[SerializeField]
		private RectTransform _farmland;

		// Token: 0x0400602E RID: 24622
		[SerializeField]
		private Image _cursor;

		// Token: 0x0400602F RID: 24623
		[SerializeField]
		private Button _allGetButton;
	}
}
