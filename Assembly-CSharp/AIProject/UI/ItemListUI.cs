using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Illusion.Extensions;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E8C RID: 3724
	public class ItemListUI : MenuUIBehaviour
	{
		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06007765 RID: 30565 RVA: 0x00328091 File Offset: 0x00326491
		public PlaySE playSE { get; } = new PlaySE();

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06007766 RID: 30566 RVA: 0x00328099 File Offset: 0x00326499
		public VerticalLayoutGroup LayoutGroup
		{
			[CompilerGenerated]
			get
			{
				return this._layoutGroup;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06007767 RID: 30567 RVA: 0x003280A1 File Offset: 0x003264A1
		private Transform itemParent
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._itemParent, () => this._layoutGroup.transform);
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06007768 RID: 30568 RVA: 0x003280BB File Offset: 0x003264BB
		// (set) Token: 0x06007769 RID: 30569 RVA: 0x003280C8 File Offset: 0x003264C8
		public int SelectedID
		{
			get
			{
				return this._selectedID.Value;
			}
			set
			{
				this._selectedID.Value = value;
			}
		}

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x0600776A RID: 30570 RVA: 0x003280D8 File Offset: 0x003264D8
		// (remove) Token: 0x0600776B RID: 30571 RVA: 0x00328110 File Offset: 0x00326510
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, ItemNodeUI> SelectChanged;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x0600776C RID: 30572 RVA: 0x00328148 File Offset: 0x00326548
		// (remove) Token: 0x0600776D RID: 30573 RVA: 0x00328180 File Offset: 0x00326580
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, ItemNodeUI> CurrentChanged;

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x0600776E RID: 30574 RVA: 0x003281B6 File Offset: 0x003265B6
		// (set) Token: 0x0600776F RID: 30575 RVA: 0x003281C3 File Offset: 0x003265C3
		public int CurrentID
		{
			get
			{
				return this._currentID.Value;
			}
			set
			{
				this._currentID.Value = value;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06007770 RID: 30576 RVA: 0x003281D4 File Offset: 0x003265D4
		public CanvasGroup CursorCanvasGroup
		{
			[CompilerGenerated]
			get
			{
				CanvasGroup result;
				if ((result = this._cursorCanvasGroup) == null)
				{
					result = (this._cursorCanvasGroup = this._cursorFrame.GetComponent<CanvasGroup>());
				}
				return result;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06007771 RID: 30577 RVA: 0x00328202 File Offset: 0x00326602
		public Image CursorFrame
		{
			[CompilerGenerated]
			get
			{
				return this._cursorFrame;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06007772 RID: 30578 RVA: 0x0032820A File Offset: 0x0032660A
		public Image SelectedCursorFrame
		{
			[CompilerGenerated]
			get
			{
				return this._selectedCursorFrame;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06007773 RID: 30579 RVA: 0x00328212 File Offset: 0x00326612
		// (set) Token: 0x06007774 RID: 30580 RVA: 0x0032821A File Offset: 0x0032661A
		public ItemNodeUI SelectedOption { get; private set; }

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06007775 RID: 30581 RVA: 0x00328223 File Offset: 0x00326623
		// (set) Token: 0x06007776 RID: 30582 RVA: 0x0032822B File Offset: 0x0032662B
		public ItemNodeUI CurrentOption { get; private set; }

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06007777 RID: 30583 RVA: 0x00328234 File Offset: 0x00326634
		public bool isOptionNode
		{
			[CompilerGenerated]
			get
			{
				return this.OptionNode != null;
			}
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x00328242 File Offset: 0x00326642
		public void SetOptionNode(GameObject node)
		{
			this.OptionNode = node;
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06007779 RID: 30585 RVA: 0x0032824B File Offset: 0x0032664B
		// (set) Token: 0x0600777A RID: 30586 RVA: 0x00328253 File Offset: 0x00326653
		private GameObject OptionNode { get; set; }

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x0600777B RID: 30587 RVA: 0x0032825C File Offset: 0x0032665C
		private static GameObject SystemNode
		{
			get
			{
				if (ItemListUI._systemNode != null)
				{
					return ItemListUI._systemNode;
				}
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "ItemOption_system", false, string.Empty);
				if (gameObject == null)
				{
					return null;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
				}
				GameObject gameObject2 = gameObject;
				ItemListUI._systemNode = gameObject2;
				return gameObject2;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x0600777C RID: 30588 RVA: 0x00328302 File Offset: 0x00326702
		// (set) Token: 0x0600777D RID: 30589 RVA: 0x00328309 File Offset: 0x00326709
		private static GameObject _systemNode { get; set; }

		// Token: 0x0600777E RID: 30590 RVA: 0x00328314 File Offset: 0x00326714
		public ItemNodeUI GetNode(int index)
		{
			ItemNodeUI result;
			this._optionTable.TryGetValue(index, out result);
			return result;
		}

		// Token: 0x0600777F RID: 30591 RVA: 0x00328334 File Offset: 0x00326734
		public IEnumerator<ItemNodeUI> GetEnumerator()
		{
			foreach (ItemNodeUI item in this._optionTable.Values)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06007780 RID: 30592 RVA: 0x0032834F File Offset: 0x0032674F
		public IReadOnlyReactiveDictionary<int, ItemNodeUI> optionTable
		{
			[CompilerGenerated]
			get
			{
				return this._optionTable;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06007781 RID: 30593 RVA: 0x00328357 File Offset: 0x00326757
		private ReactiveDictionary<int, ItemNodeUI> _optionTable { get; } = new ReactiveDictionary<int, ItemNodeUI>();

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06007782 RID: 30594 RVA: 0x0032835F File Offset: 0x0032675F
		public IObservable<int> OnEntered
		{
			[CompilerGenerated]
			get
			{
				return from id in this._selectedID.AsObservable<int>()
				where id != -1
				select id;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06007783 RID: 30595 RVA: 0x0032838E File Offset: 0x0032678E
		// (set) Token: 0x06007784 RID: 30596 RVA: 0x00328396 File Offset: 0x00326796
		public UnityEvent OnSubmit { get; private set; } = new UnityEvent();

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06007785 RID: 30597 RVA: 0x0032839F File Offset: 0x0032679F
		// (set) Token: 0x06007786 RID: 30598 RVA: 0x003283A7 File Offset: 0x003267A7
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x06007787 RID: 30599 RVA: 0x003283B0 File Offset: 0x003267B0
		public void SetupDefault()
		{
			this.ForceSetSelectedID(0);
		}

		// Token: 0x06007788 RID: 30600 RVA: 0x003283B9 File Offset: 0x003267B9
		public void ForceSetSelectedID(int id)
		{
			this._selectedID.SetValueAndForceNotify(id);
		}

		// Token: 0x06007789 RID: 30601 RVA: 0x003283C8 File Offset: 0x003267C8
		public void ForceSetNonSelect()
		{
			int valueAndForceNotify = -1;
			this.playSE.use = false;
			this._selectedID.SetValueAndForceNotify(valueAndForceNotify);
			this._currentID.SetValueAndForceNotify(valueAndForceNotify);
			this.playSE.use = true;
		}

		// Token: 0x0600778A RID: 30602 RVA: 0x00328408 File Offset: 0x00326808
		public ItemNodeUI AddItemNode(int id, StuffItem item)
		{
			StuffItemInfo itemInfo = ItemNodeUI.GetItemInfo(item);
			if (itemInfo == null)
			{
				return null;
			}
			GameObject original = itemInfo.isNone ? ItemListUI.SystemNode : this.OptionNode;
			ItemNodeUI opt = UnityEngine.Object.Instantiate<GameObject>(original, this.itemParent).GetComponent<ItemNodeUI>();
			opt.Bind(item, itemInfo);
			opt.onEnter.Subscribe(delegate(PointerEventData _)
			{
				if (opt.Item.Count > 0)
				{
					this._selectedID.SetValueAndForceNotify(id);
				}
			}).AddTo(this);
			opt.OnClick.AddListener(delegate()
			{
				this._currentID.Value = id;
			});
			this._optionTable.Add(id, opt);
			return opt;
		}

		// Token: 0x0600778B RID: 30603 RVA: 0x003284D4 File Offset: 0x003268D4
		public bool RemoveItemNode(int index)
		{
			ItemNodeUI itemNodeUI;
			if (!this._optionTable.TryGetValue(index, out itemNodeUI))
			{
				return false;
			}
			if (itemNodeUI != null)
			{
				UnityEngine.Object.Destroy(itemNodeUI.gameObject);
			}
			return this._optionTable.Remove(index);
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x0600778C RID: 30604 RVA: 0x00328519 File Offset: 0x00326919
		public int SearchNotUsedIndex
		{
			[CompilerGenerated]
			get
			{
				return (from i in Enumerable.Range(0, this._optionTable.Count)
				where !this._optionTable.ContainsKey(i)
				select i).DefaultIfEmpty(this._optionTable.Count).First<int>();
			}
		}

		// Token: 0x0600778D RID: 30605 RVA: 0x00328554 File Offset: 0x00326954
		public int SearchIndex(StuffItem item)
		{
			if (item != null)
			{
				foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in this._optionTable)
				{
					if (keyValuePair.Value.Item.CategoryID == item.CategoryID && keyValuePair.Value.Item.ID == item.ID)
					{
						return keyValuePair.Key;
					}
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x0600778E RID: 30606 RVA: 0x003285F8 File Offset: 0x003269F8
		public int SearchIndex(ItemNodeUI node)
		{
			if (node != null)
			{
				foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in this._optionTable)
				{
					if (keyValuePair.Value == node)
					{
						return keyValuePair.Key;
					}
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x0600778F RID: 30607 RVA: 0x0032867C File Offset: 0x00326A7C
		public int[] ItemVisiblesID
		{
			[CompilerGenerated]
			get
			{
				return this.ConvertID(this.ItemVisibles);
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06007790 RID: 30608 RVA: 0x0032868C File Offset: 0x00326A8C
		public ItemNodeUI[] ItemVisibles
		{
			[CompilerGenerated]
			get
			{
				return (from t in this.itemParent.Children()
				select t.GetComponent<ItemNodeUI>() into p
				where p.Visible
				select p).ToArray<ItemNodeUI>();
			}
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x003286F0 File Offset: 0x00326AF0
		public void Filter(int category)
		{
			foreach (ItemNodeUI itemNodeUI in from p in this._optionTable
			select p.Value)
			{
				itemNodeUI.Visible = (category == 0 || itemNodeUI.CategoryID == 0 || itemNodeUI.CategoryID == category || (itemNodeUI.isNone && Mathf.Abs(itemNodeUI.CategoryID) == category));
			}
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x003287A8 File Offset: 0x00326BA8
		public void Refresh()
		{
			foreach (ItemNodeUI itemNodeUI in from item in this._optionTable.Values
			where item != null
			select item)
			{
				itemNodeUI.Refresh();
			}
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x00328828 File Offset: 0x00326C28
		public void ClearItems()
		{
			foreach (ItemNodeUI itemNodeUI in from item in this._optionTable.Values
			where item != null
			select item)
			{
				UnityEngine.Object.Destroy(itemNodeUI.gameObject);
			}
			this._optionTable.Clear();
			this.ForceSetNonSelect();
		}

		// Token: 0x06007794 RID: 30612 RVA: 0x003288C0 File Offset: 0x00326CC0
		public void SortItems(int type, bool ascending)
		{
			ItemNodeUI.Sort(type, ascending, this._optionTable);
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x003288CF File Offset: 0x00326CCF
		private int[] ConvertID(ItemNodeUI[] nodeUIs)
		{
			return nodeUIs.Select(delegate(ItemNodeUI p)
			{
				foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in this._optionTable)
				{
					if (p == keyValuePair.Value)
					{
						return keyValuePair.Key;
					}
				}
				return -1;
			}).ToArray<int>();
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x003288E8 File Offset: 0x00326CE8
		protected override void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.itemParent.gameObject.Children().ForEach(delegate(GameObject go)
			{
				UnityEngine.Object.Destroy(go);
			});
			(from _ in this._selectedID
			where Singleton<Manager.Resources>.IsInstance()
			select _).Subscribe(delegate(int index)
			{
				ItemNodeUI itemNodeUI;
				this._optionTable.TryGetValue(index, out itemNodeUI);
				this.SelectedOption = itemNodeUI;
				if (itemNodeUI == null)
				{
					return;
				}
				if (this.SelectChanged != null)
				{
					this.SelectChanged(index, itemNodeUI);
				}
			});
			this._currentID.Subscribe(delegate(int index)
			{
				ItemNodeUI itemNodeUI;
				this._optionTable.TryGetValue(index, out itemNodeUI);
				this.CurrentOption = itemNodeUI;
				if (this.CurrentChanged != null)
				{
					this.CurrentChanged(index, itemNodeUI);
				}
				if (index >= 0)
				{
					this.playSE.Play(SoundPack.SystemSE.OK_S);
				}
			});
			this.disposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Submit
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.OnInputSubmit();
			});
			this._actionCommands.Add(actionIDDownCommand);
			ActionIDDownCommand actionIDDownCommand2 = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand2);
			ActionIDDownCommand actionIDDownCommand3 = new ActionIDDownCommand
			{
				ActionID = ActionID.MouseRight
			};
			actionIDDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._actionCommands.Add(actionIDDownCommand3);
			base.Start();
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x00328A5B File Offset: 0x00326E5B
		private void OnDestroy()
		{
			if (this.disposable != null)
			{
				this.disposable.Dispose();
			}
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x00328A78 File Offset: 0x00326E78
		private void OnUpdate()
		{
			bool flag = base.EnabledInput && base.FocusLevel == Singleton<Manager.Input>.Instance.FocusLevel;
			float target = (float)((!flag || !(this.SelectedOption != null)) ? 0 : 1);
			this.CursorCanvasGroup.alpha = Smooth.Damp(this.CursorCanvasGroup.alpha, target, ref this._alphaVelocity, this._alphaAccelerationTime);
			if (this.SelectedOption != null)
			{
				RectTransform rectTransform = this._cursorFrame.rectTransform;
				RectTransform rectTransform2 = this.SelectedOption.GetComponent<RectTransform>();
				ref Vector3 velocity = ref this._velocity;
				float? smoothSizeTime = new float?(this._followAccelerationTime);
				AIProject.UI.Viewer.CursorFrame.Set(rectTransform, rectTransform2, ref velocity, null, smoothSizeTime);
			}
			if (this.CurrentOption == null)
			{
				this._selectedCursorFrame.gameObject.SetActive(false);
			}
			else
			{
				this._selectedCursorFrame.gameObject.SetActive(true);
				RectTransform rectTransform2 = this._selectedCursorFrame.rectTransform;
				RectTransform rectTransform = this.CurrentOption.GetComponent<RectTransform>();
				ref Vector3 velocity = ref this._selectedvelocity;
				float? smoothSizeTime = new float?(this._followAccelerationTime);
				AIProject.UI.Viewer.CursorFrame.Set(rectTransform2, rectTransform, ref velocity, null, smoothSizeTime);
			}
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x00328BBB File Offset: 0x00326FBB
		private void OnInputSubmit()
		{
			UnityEvent onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit.Invoke();
			}
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x00328BD1 File Offset: 0x00326FD1
		private void OnInputCancel()
		{
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x040060D8 RID: 24792
		[SerializeField]
		private VerticalLayoutGroup _layoutGroup;

		// Token: 0x040060D9 RID: 24793
		private Transform _itemParent;

		// Token: 0x040060DA RID: 24794
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x040060DD RID: 24797
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _currentID = new IntReactiveProperty(-1);

		// Token: 0x040060DE RID: 24798
		private CanvasGroup _cursorCanvasGroup;

		// Token: 0x040060DF RID: 24799
		[SerializeField]
		private Image _cursorFrame;

		// Token: 0x040060E0 RID: 24800
		[SerializeField]
		private Image _selectedCursorFrame;

		// Token: 0x040060E8 RID: 24808
		private IDisposable disposable;

		// Token: 0x040060E9 RID: 24809
		private float _alphaVelocity;

		// Token: 0x040060EA RID: 24810
		private Vector3 _velocity = Vector3.zero;

		// Token: 0x040060EB RID: 24811
		private Vector3 _selectedvelocity = Vector3.zero;

		// Token: 0x02000E8D RID: 3725
		[Serializable]
		public class ValueChangeEvent : UnityEvent<int, StuffItem>
		{
		}
	}
}
