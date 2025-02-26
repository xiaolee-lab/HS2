using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Illusion.Extensions;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E61 RID: 3681
	public class CraftItemListUI : MenuUIBehaviour
	{
		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x0600748F RID: 29839 RVA: 0x00319361 File Offset: 0x00317761
		public PlaySE playSE { get; } = new PlaySE();

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06007490 RID: 29840 RVA: 0x00319369 File Offset: 0x00317769
		private Transform itemParent
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._itemParent, () => this._layoutGroup.transform);
			}
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06007491 RID: 29841 RVA: 0x00319384 File Offset: 0x00317784
		// (remove) Token: 0x06007492 RID: 29842 RVA: 0x003193BC File Offset: 0x003177BC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, CraftItemNodeUI> SelectChanged;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06007493 RID: 29843 RVA: 0x003193F4 File Offset: 0x003177F4
		// (remove) Token: 0x06007494 RID: 29844 RVA: 0x0031942C File Offset: 0x0031782C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, CraftItemNodeUI> CurrentChanged;

		// Token: 0x06007495 RID: 29845 RVA: 0x00319462 File Offset: 0x00317862
		public void SetCraftUI(CraftUI craftUI)
		{
			this._craftUI = craftUI;
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06007496 RID: 29846 RVA: 0x0031946C File Offset: 0x0031786C
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

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06007497 RID: 29847 RVA: 0x0031949A File Offset: 0x0031789A
		// (set) Token: 0x06007498 RID: 29848 RVA: 0x003194A2 File Offset: 0x003178A2
		public CraftItemNodeUI SelectedOption { get; private set; }

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06007499 RID: 29849 RVA: 0x003194AB File Offset: 0x003178AB
		// (set) Token: 0x0600749A RID: 29850 RVA: 0x003194B3 File Offset: 0x003178B3
		public CraftItemNodeUI CurrentOption { get; private set; }

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x0600749B RID: 29851 RVA: 0x003194BC File Offset: 0x003178BC
		public bool isOptionNode
		{
			[CompilerGenerated]
			get
			{
				return this.OptionNode != null;
			}
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x003194CA File Offset: 0x003178CA
		public void SetOptionNode(GameObject node)
		{
			this.OptionNode = node;
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x0600749D RID: 29853 RVA: 0x003194D3 File Offset: 0x003178D3
		// (set) Token: 0x0600749E RID: 29854 RVA: 0x003194DB File Offset: 0x003178DB
		private GameObject OptionNode { get; set; }

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x0600749F RID: 29855 RVA: 0x003194E4 File Offset: 0x003178E4
		private ReactiveDictionary<int, CraftItemNodeUI> _optionTable { get; } = new ReactiveDictionary<int, CraftItemNodeUI>();

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x060074A0 RID: 29856 RVA: 0x003194EC File Offset: 0x003178EC
		// (set) Token: 0x060074A1 RID: 29857 RVA: 0x003194F4 File Offset: 0x003178F4
		public Action OnEntered { get; set; }

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x060074A2 RID: 29858 RVA: 0x003194FD File Offset: 0x003178FD
		// (set) Token: 0x060074A3 RID: 29859 RVA: 0x00319505 File Offset: 0x00317905
		public UnityEvent OnSubmit { get; private set; } = new UnityEvent();

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x060074A4 RID: 29860 RVA: 0x0031950E File Offset: 0x0031790E
		// (set) Token: 0x060074A5 RID: 29861 RVA: 0x00319516 File Offset: 0x00317916
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x060074A6 RID: 29862 RVA: 0x00319520 File Offset: 0x00317920
		public void ForceSetNonSelect()
		{
			int valueAndForceNotify = -1;
			this.playSE.use = false;
			this._selectedID.SetValueAndForceNotify(valueAndForceNotify);
			this._currentID.SetValueAndForceNotify(valueAndForceNotify);
			this.playSE.use = true;
		}

		// Token: 0x060074A7 RID: 29863 RVA: 0x00319560 File Offset: 0x00317960
		public CraftItemNodeUI AddItemNode(int id, CraftItemNodeUI.StuffItemInfoPack pack, RecipeDataInfo[] data)
		{
			GameObject optionNode = this.OptionNode;
			CraftItemNodeUI component = UnityEngine.Object.Instantiate<GameObject>(optionNode, this.itemParent).GetComponent<CraftItemNodeUI>();
			component.Bind(this._craftUI, pack, data);
			component.onEnter.AddListener(delegate(BaseEventData _)
			{
				this._selectedID.SetValueAndForceNotify(id);
			});
			component.OnClick.AddListener(delegate()
			{
				this._currentID.Value = id;
			});
			this._optionTable.Add(id, component);
			return component;
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x060074A8 RID: 29864 RVA: 0x003195E9 File Offset: 0x003179E9
		public int[] ItemVisiblesID
		{
			[CompilerGenerated]
			get
			{
				return this.ConvertID(this.ItemVisibles);
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x060074A9 RID: 29865 RVA: 0x003195F8 File Offset: 0x003179F8
		public CraftItemNodeUI[] ItemVisibles
		{
			[CompilerGenerated]
			get
			{
				return (from t in this.itemParent.Children()
				select t.GetComponent<CraftItemNodeUI>() into p
				where p.Visible
				select p).ToArray<CraftItemNodeUI>();
			}
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x0031965C File Offset: 0x00317A5C
		public void Refresh()
		{
			foreach (CraftItemNodeUI craftItemNodeUI in from item in this._optionTable.Values
			where item != null
			select item)
			{
				craftItemNodeUI.Refresh();
			}
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x003196DC File Offset: 0x00317ADC
		public void ClearItems()
		{
			foreach (CraftItemNodeUI craftItemNodeUI in from item in this._optionTable.Values
			where item != null
			select item)
			{
				UnityEngine.Object.Destroy(craftItemNodeUI.gameObject);
			}
			this._optionTable.Clear();
			this.ForceSetNonSelect();
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x00319774 File Offset: 0x00317B74
		private int[] ConvertID(CraftItemNodeUI[] nodeUIs)
		{
			return nodeUIs.Select(delegate(CraftItemNodeUI p)
			{
				foreach (KeyValuePair<int, CraftItemNodeUI> keyValuePair in this._optionTable)
				{
					if (p == keyValuePair.Value)
					{
						return keyValuePair.Key;
					}
				}
				return -1;
			}).ToArray<int>();
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x00319790 File Offset: 0x00317B90
		protected override void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			(from _ in this._selectedID
			where Singleton<Manager.Resources>.IsInstance()
			select _).Subscribe(delegate(int index)
			{
				Action onEntered = this.OnEntered;
				if (onEntered != null)
				{
					onEntered();
				}
				CraftItemNodeUI craftItemNodeUI;
				this._optionTable.TryGetValue(index, out craftItemNodeUI);
				this.SelectedOption = craftItemNodeUI;
				if (craftItemNodeUI == null)
				{
					return;
				}
				if (this.SelectChanged != null)
				{
					this.SelectChanged(index, craftItemNodeUI);
				}
			});
			this._currentID.Subscribe(delegate(int index)
			{
				CraftItemNodeUI craftItemNodeUI;
				this._optionTable.TryGetValue(index, out craftItemNodeUI);
				this.CurrentOption = craftItemNodeUI;
				if (this.CurrentChanged != null)
				{
					this.CurrentChanged(index, craftItemNodeUI);
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

		// Token: 0x060074AE RID: 29870 RVA: 0x003198D1 File Offset: 0x00317CD1
		private void OnDestroy()
		{
			if (this.disposable != null)
			{
				this.disposable.Dispose();
			}
			this.disposable = null;
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x003198F4 File Offset: 0x00317CF4
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
				CursorFrame.Set(rectTransform, rectTransform2, ref velocity, null, smoothSizeTime);
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
				CursorFrame.Set(rectTransform2, rectTransform, ref velocity, null, smoothSizeTime);
			}
		}

		// Token: 0x060074B0 RID: 29872 RVA: 0x00319A37 File Offset: 0x00317E37
		private void OnInputSubmit()
		{
			UnityEvent onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit.Invoke();
			}
		}

		// Token: 0x060074B1 RID: 29873 RVA: 0x00319A4D File Offset: 0x00317E4D
		private void OnInputCancel()
		{
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x04005F33 RID: 24371
		[SerializeField]
		private VerticalLayoutGroup _layoutGroup;

		// Token: 0x04005F34 RID: 24372
		private Transform _itemParent;

		// Token: 0x04005F35 RID: 24373
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005F38 RID: 24376
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _currentID = new IntReactiveProperty(-1);

		// Token: 0x04005F39 RID: 24377
		private CraftUI _craftUI;

		// Token: 0x04005F3A RID: 24378
		private CanvasGroup _cursorCanvasGroup;

		// Token: 0x04005F3B RID: 24379
		[SerializeField]
		private Image _cursorFrame;

		// Token: 0x04005F3C RID: 24380
		[SerializeField]
		private Image _selectedCursorFrame;

		// Token: 0x04005F44 RID: 24388
		private IDisposable disposable;

		// Token: 0x04005F45 RID: 24389
		private float _alphaVelocity;

		// Token: 0x04005F46 RID: 24390
		private Vector3 _velocity = Vector3.zero;

		// Token: 0x04005F47 RID: 24391
		private Vector3 _selectedvelocity = Vector3.zero;

		// Token: 0x02000E62 RID: 3682
		[Serializable]
		public class ValueChangeEvent : UnityEvent<int, StuffItem>
		{
		}
	}
}
