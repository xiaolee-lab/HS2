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
	// Token: 0x02000E6C RID: 3692
	public class RecipeItemListUI : MenuUIBehaviour
	{
		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x0600753B RID: 30011 RVA: 0x0031C4A9 File Offset: 0x0031A8A9
		public PlaySE playSE { get; } = new PlaySE();

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x0600753C RID: 30012 RVA: 0x0031C4B1 File Offset: 0x0031A8B1
		private Transform itemParent
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._itemParent, () => this._layoutGroup.transform);
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x0600753D RID: 30013 RVA: 0x0031C4CC File Offset: 0x0031A8CC
		// (remove) Token: 0x0600753E RID: 30014 RVA: 0x0031C504 File Offset: 0x0031A904
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, RecipeItemTitleNodeUI> SelectChanged;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x0600753F RID: 30015 RVA: 0x0031C53C File Offset: 0x0031A93C
		// (remove) Token: 0x06007540 RID: 30016 RVA: 0x0031C574 File Offset: 0x0031A974
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, RecipeItemTitleNodeUI> CurrentChanged;

		// Token: 0x06007541 RID: 30017 RVA: 0x0031C5AA File Offset: 0x0031A9AA
		public void SetCraftUI(CraftUI craftUI)
		{
			this._craftUI = craftUI;
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06007542 RID: 30018 RVA: 0x0031C5B4 File Offset: 0x0031A9B4
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

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06007543 RID: 30019 RVA: 0x0031C5E2 File Offset: 0x0031A9E2
		// (set) Token: 0x06007544 RID: 30020 RVA: 0x0031C5EA File Offset: 0x0031A9EA
		public RecipeItemTitleNodeUI SelectedOption { get; private set; }

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06007545 RID: 30021 RVA: 0x0031C5F3 File Offset: 0x0031A9F3
		// (set) Token: 0x06007546 RID: 30022 RVA: 0x0031C5FB File Offset: 0x0031A9FB
		public RecipeItemTitleNodeUI CurrentOption { get; private set; }

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06007547 RID: 30023 RVA: 0x0031C604 File Offset: 0x0031AA04
		public bool isOptionNode
		{
			[CompilerGenerated]
			get
			{
				return this.OptionNode != null;
			}
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x0031C612 File Offset: 0x0031AA12
		public void SetOptionNode(GameObject node)
		{
			this.OptionNode = node;
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06007549 RID: 30025 RVA: 0x0031C61B File Offset: 0x0031AA1B
		// (set) Token: 0x0600754A RID: 30026 RVA: 0x0031C623 File Offset: 0x0031AA23
		private GameObject OptionNode { get; set; }

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x0600754B RID: 30027 RVA: 0x0031C62C File Offset: 0x0031AA2C
		private ReactiveDictionary<int, RecipeItemTitleNodeUI> _optionTable { get; } = new ReactiveDictionary<int, RecipeItemTitleNodeUI>();

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x0600754C RID: 30028 RVA: 0x0031C634 File Offset: 0x0031AA34
		// (set) Token: 0x0600754D RID: 30029 RVA: 0x0031C63C File Offset: 0x0031AA3C
		public Action OnEntered { get; set; }

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x0600754E RID: 30030 RVA: 0x0031C645 File Offset: 0x0031AA45
		// (set) Token: 0x0600754F RID: 30031 RVA: 0x0031C64D File Offset: 0x0031AA4D
		public UnityEvent OnSubmit { get; private set; } = new UnityEvent();

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06007550 RID: 30032 RVA: 0x0031C656 File Offset: 0x0031AA56
		// (set) Token: 0x06007551 RID: 30033 RVA: 0x0031C65E File Offset: 0x0031AA5E
		public UnityEvent OnCancel { get; private set; } = new UnityEvent();

		// Token: 0x06007552 RID: 30034 RVA: 0x0031C668 File Offset: 0x0031AA68
		public void ForceSetNonSelect()
		{
			int valueAndForceNotify = -1;
			this.playSE.use = false;
			this._selectedID.SetValueAndForceNotify(valueAndForceNotify);
			this._currentID.SetValueAndForceNotify(valueAndForceNotify);
			this.playSE.use = true;
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x0031C6A8 File Offset: 0x0031AAA8
		public IReadOnlyList<RecipeItemTitleNodeUI> AddItemNode(int id, RecipeDataInfo[] data)
		{
			GameObject optionNode = this.OptionNode;
			List<RecipeItemTitleNodeUI> list = new List<RecipeItemTitleNodeUI>();
			using (var enumerator = data.Select((RecipeDataInfo p, int i) => new
			{
				p,
				i
			}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>__AnonType18<RecipeDataInfo, int> item = enumerator.Current;
					RecipeItemListUI $this = this;
					RecipeItemTitleNodeUI component = UnityEngine.Object.Instantiate<GameObject>(optionNode, this.itemParent).GetComponent<RecipeItemTitleNodeUI>();
					component.Bind(this._craftUI, item.i, item.p);
					component.onEnter.AddListener(delegate(BaseEventData _)
					{
						$this._selectedID.SetValueAndForceNotify(item.i);
					});
					component.OnClick.AddListener(delegate()
					{
						$this._currentID.Value = item.i;
					});
					this._optionTable.Add(item.i, component);
					list.Add(component);
				}
			}
			return list;
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06007554 RID: 30036 RVA: 0x0031C7C0 File Offset: 0x0031ABC0
		public int[] ItemVisiblesID
		{
			[CompilerGenerated]
			get
			{
				return this.ConvertID(this.ItemVisibles);
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06007555 RID: 30037 RVA: 0x0031C7D0 File Offset: 0x0031ABD0
		public RecipeItemTitleNodeUI[] ItemVisibles
		{
			[CompilerGenerated]
			get
			{
				return (from t in this.itemParent.Children()
				select t.GetComponent<RecipeItemTitleNodeUI>() into p
				where p.Visible
				select p).ToArray<RecipeItemTitleNodeUI>();
			}
		}

		// Token: 0x06007556 RID: 30038 RVA: 0x0031C834 File Offset: 0x0031AC34
		public void Refresh()
		{
			foreach (RecipeItemTitleNodeUI recipeItemTitleNodeUI in from item in this._optionTable.Values
			where item != null
			select item)
			{
				recipeItemTitleNodeUI.Refresh();
			}
		}

		// Token: 0x06007557 RID: 30039 RVA: 0x0031C8B4 File Offset: 0x0031ACB4
		public void ClearItems()
		{
			foreach (RecipeItemTitleNodeUI recipeItemTitleNodeUI in from item in this._optionTable.Values
			where item != null
			select item)
			{
				UnityEngine.Object.Destroy(recipeItemTitleNodeUI.gameObject);
			}
			this._optionTable.Clear();
			this.ForceSetNonSelect();
		}

		// Token: 0x06007558 RID: 30040 RVA: 0x0031C94C File Offset: 0x0031AD4C
		private int[] ConvertID(RecipeItemTitleNodeUI[] nodeUIs)
		{
			return nodeUIs.Select(delegate(RecipeItemTitleNodeUI p)
			{
				foreach (KeyValuePair<int, RecipeItemTitleNodeUI> keyValuePair in this._optionTable)
				{
					if (p == keyValuePair.Value)
					{
						return keyValuePair.Key;
					}
				}
				return -1;
			}).ToArray<int>();
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x0031C968 File Offset: 0x0031AD68
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
				Action onEntered = this.OnEntered;
				if (onEntered != null)
				{
					onEntered();
				}
				RecipeItemTitleNodeUI recipeItemTitleNodeUI;
				this._optionTable.TryGetValue(index, out recipeItemTitleNodeUI);
				if (recipeItemTitleNodeUI != null && !recipeItemTitleNodeUI.isSuccess)
				{
					recipeItemTitleNodeUI = null;
				}
				this.SelectedOption = recipeItemTitleNodeUI;
				if (recipeItemTitleNodeUI == null)
				{
					return;
				}
				if (this.SelectChanged != null)
				{
					this.SelectChanged(index, recipeItemTitleNodeUI);
				}
			});
			this._currentID.Subscribe(delegate(int index)
			{
				RecipeItemTitleNodeUI recipeItemTitleNodeUI;
				this._optionTable.TryGetValue(index, out recipeItemTitleNodeUI);
				this.CurrentOption = recipeItemTitleNodeUI;
				if (this.CurrentChanged != null)
				{
					this.CurrentChanged(index, recipeItemTitleNodeUI);
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

		// Token: 0x0600755A RID: 30042 RVA: 0x0031CADB File Offset: 0x0031AEDB
		private void OnDestroy()
		{
			if (this.disposable != null)
			{
				this.disposable.Dispose();
			}
		}

		// Token: 0x0600755B RID: 30043 RVA: 0x0031CAF8 File Offset: 0x0031AEF8
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

		// Token: 0x0600755C RID: 30044 RVA: 0x0031CC3B File Offset: 0x0031B03B
		private void OnInputSubmit()
		{
			UnityEvent onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit.Invoke();
			}
		}

		// Token: 0x0600755D RID: 30045 RVA: 0x0031CC51 File Offset: 0x0031B051
		private void OnInputCancel()
		{
			UnityEvent onCancel = this.OnCancel;
			if (onCancel != null)
			{
				onCancel.Invoke();
			}
		}

		// Token: 0x04005F9A RID: 24474
		[SerializeField]
		private VerticalLayoutGroup _layoutGroup;

		// Token: 0x04005F9B RID: 24475
		private Transform _itemParent;

		// Token: 0x04005F9C RID: 24476
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005F9F RID: 24479
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private IntReactiveProperty _currentID = new IntReactiveProperty(-1);

		// Token: 0x04005FA0 RID: 24480
		private CraftUI _craftUI;

		// Token: 0x04005FA1 RID: 24481
		private CanvasGroup _cursorCanvasGroup;

		// Token: 0x04005FA2 RID: 24482
		[SerializeField]
		private Image _cursorFrame;

		// Token: 0x04005FA3 RID: 24483
		[SerializeField]
		private Image _selectedCursorFrame;

		// Token: 0x04005FAB RID: 24491
		private IDisposable disposable;

		// Token: 0x04005FAC RID: 24492
		private float _alphaVelocity;

		// Token: 0x04005FAD RID: 24493
		private Vector3 _velocity = Vector3.zero;

		// Token: 0x04005FAE RID: 24494
		private Vector3 _selectedvelocity = Vector3.zero;

		// Token: 0x02000E6D RID: 3693
		[Serializable]
		public class ValueChangeEvent : UnityEvent<int, StuffItem>
		{
		}
	}
}
