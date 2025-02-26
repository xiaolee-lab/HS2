using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Player;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E69 RID: 3689
	public class CraftUI : MenuUIBehaviour
	{
		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060074F9 RID: 29945 RVA: 0x0031A5BF File Offset: 0x003189BF
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x060074FA RID: 29946 RVA: 0x0031A5C8 File Offset: 0x003189C8
		public static bool CreateCooking(CraftUI.CreateItem createItem, List<StuffItem> itemList, float pheromone, bool chef)
		{
			if (createItem == null)
			{
				return false;
			}
			int num = 1;
			StuffItem stuffItem = new StuffItem(createItem.item);
			List<StuffItem> itemListInPantry = Singleton<Game>.Instance.Environment.ItemListInPantry;
			int pantryCapacity = Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines.PantryCapacity;
			if (!itemListInPantry.CanAddItem(pantryCapacity, stuffItem, num))
			{
				return false;
			}
			RecipeDataInfo info = createItem.info;
			foreach (RecipeDataInfo.NeedData needData in info.NeedList)
			{
				StuffItem.RemoveStorages(new StuffItem(needData.CategoryID, needData.ID, needData.Sum * num), new List<StuffItem>[]
				{
					itemList,
					itemListInPantry
				});
			}
			float num2 = 50f;
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			float t = Mathf.InverseLerp(statusProfile.FlavorCookSuccessBoostMinMax.min, statusProfile.FlavorCookSuccessBoostMinMax.max, pheromone);
			float num3 = statusProfile.FlavorCookSuccessBoost.Lerp(t);
			float num4 = 0f;
			if (chef)
			{
				num4 += (float)statusProfile.ChefCookSuccessBoost;
			}
			float num5 = num2 + num3 + num4;
			float num6 = UnityEngine.Random.Range(0f, 100f);
			if (num6 <= num5)
			{
				itemListInPantry.AddItem(stuffItem, info.CreateSum * num);
			}
			else
			{
				stuffItem.CategoryID = 7;
				stuffItem.ID = 12;
				itemListInPantry.AddItem(stuffItem, info.CreateSum * num);
			}
			return true;
		}

		// Token: 0x060074FB RID: 29947 RVA: 0x0031A748 File Offset: 0x00318B48
		public static CraftUI.CreateItem CreateCheck(IReadOnlyDictionary<int, RecipeDataInfo[]> targetTable, IReadOnlyCollection<IReadOnlyCollection<StuffItem>> storages)
		{
			Dictionary<int, RecipeDataInfo[]> dictionary = (from v in targetTable
			select new
			{
				Key = v.Key,
				Value = CraftUI.Possible(storages, v.Value)
			} into v
			where v.Value.Any<RecipeDataInfo>()
			select v).ToDictionary(v => v.Key, v => v.Value);
			if (dictionary.Any<KeyValuePair<int, RecipeDataInfo[]>>())
			{
				KeyValuePair<int, RecipeDataInfo[]> keyValuePair = dictionary.Shuffle<KeyValuePair<int, RecipeDataInfo[]>>().First<KeyValuePair<int, RecipeDataInfo[]>>();
				StuffItemInfo stuffItemInfo = Singleton<Manager.Resources>.Instance.GameInfo.FindItemInfo(keyValuePair.Key);
				return new CraftUI.CreateItem
				{
					info = keyValuePair.Value.Shuffle<RecipeDataInfo>().First<RecipeDataInfo>(),
					item = new StuffItem(stuffItemInfo.CategoryID, stuffItemInfo.ID, 0)
				};
			}
			return null;
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x0031A840 File Offset: 0x00318C40
		private static RecipeDataInfo[] Possible(IReadOnlyCollection<IReadOnlyCollection<StuffItem>> storages, RecipeDataInfo[] info)
		{
			return (from x in info
			where x.NeedList.All((RecipeDataInfo.NeedData need) => storages.SelectMany((IReadOnlyCollection<StuffItem> storage) => storage).FindItems(new StuffItem(need.CategoryID, need.ID, 0)).Sum((StuffItem p) => p.Count) / need.Sum > 0)
			select x).ToArray<RecipeDataInfo>();
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060074FD RID: 29949 RVA: 0x0031A871 File Offset: 0x00318C71
		// (set) Token: 0x060074FE RID: 29950 RVA: 0x0031A879 File Offset: 0x00318C79
		public SystemMenuUI Observer { get; set; }

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060074FF RID: 29951 RVA: 0x0031A882 File Offset: 0x00318C82
		// (set) Token: 0x06007500 RID: 29952 RVA: 0x0031A88A File Offset: 0x00318C8A
		public Action OnClose { get; set; }

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06007501 RID: 29953 RVA: 0x0031A893 File Offset: 0x00318C93
		// (set) Token: 0x06007502 RID: 29954 RVA: 0x0031A89B File Offset: 0x00318C9B
		public Action OnClosedEvent { get; set; }

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06007503 RID: 29955 RVA: 0x0031A8A4 File Offset: 0x00318CA4
		public IReadOnlyCollection<IReadOnlyCollection<StuffItem>> checkStorages
		{
			[CompilerGenerated]
			get
			{
				return this._checkStorages;
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06007504 RID: 29956 RVA: 0x0031A8AC File Offset: 0x00318CAC
		// (set) Token: 0x06007505 RID: 29957 RVA: 0x0031A8B4 File Offset: 0x00318CB4
		private IReadOnlyCollection<List<StuffItem>> _checkStorages { get; set; }

		// Token: 0x06007506 RID: 29958 RVA: 0x0031A8C0 File Offset: 0x00318CC0
		public void SetID(int id)
		{
			this._id = id;
			List<List<StuffItem>> list = new List<List<StuffItem>>();
			list.Add(Singleton<Map>.Instance.Player.PlayerData.ItemList);
			list.Add(Singleton<Game>.Instance.Environment.ItemListInStorage);
			if (this._id == 2)
			{
				list.Add(Singleton<Game>.Instance.Environment.ItemListInPantry);
			}
			this._checkStorages = list;
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06007507 RID: 29959 RVA: 0x0031A931 File Offset: 0x00318D31
		// (set) Token: 0x06007508 RID: 29960 RVA: 0x0031A939 File Offset: 0x00318D39
		private int _id { get; set; }

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06007509 RID: 29961 RVA: 0x0031A942 File Offset: 0x00318D42
		// (set) Token: 0x0600750A RID: 29962 RVA: 0x0031A94A File Offset: 0x00318D4A
		private int _selectedIndexOf { get; set; }

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x0600750B RID: 29963 RVA: 0x0031A953 File Offset: 0x00318D53
		// (set) Token: 0x0600750C RID: 29964 RVA: 0x0031A95B File Offset: 0x00318D5B
		private MenuUIBehaviour[] MenuUIList { get; set; }

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x0600750D RID: 29965 RVA: 0x0031A964 File Offset: 0x00318D64
		// (set) Token: 0x0600750E RID: 29966 RVA: 0x0031A96C File Offset: 0x00318D6C
		private bool initialized { get; set; }

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x0600750F RID: 29967 RVA: 0x0031A975 File Offset: 0x00318D75
		// (set) Token: 0x06007510 RID: 29968 RVA: 0x0031A97D File Offset: 0x00318D7D
		private CraftViewer currentViewer { get; set; }

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06007511 RID: 29969 RVA: 0x0031A986 File Offset: 0x00318D86
		private CraftUI.CreateItem createItem { get; } = new CraftUI.CreateItem();

		// Token: 0x06007512 RID: 29970 RVA: 0x0031A990 File Offset: 0x00318D90
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			base.StartCoroutine(this.BindingUI());
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

		// Token: 0x06007513 RID: 29971 RVA: 0x0031AA64 File Offset: 0x00318E64
		private IEnumerator BindingUI()
		{
			this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			while (!Singleton<Manager.Resources>.IsInstance() || !Singleton<Manager.Resources>.Instance.GameInfo.recipe.initialized)
			{
				yield return null;
			}
			while (!Singleton<Map>.IsInstance() || Singleton<Map>.Instance.Player == null || Singleton<Map>.Instance.Player.PlayerData == null)
			{
				yield return null;
			}
			while (!Singleton<Game>.IsInstance() || Singleton<Game>.Instance.Environment == null)
			{
				yield return null;
			}
			if (this._recipeViewer == null)
			{
				yield return RecipeViewer.Load(this, this._recipeViewerLayout, delegate(RecipeViewer recipeViewer)
				{
					this._recipeViewer = recipeViewer;
				});
			}
			while (!this._recipeViewer.initialized)
			{
				yield return null;
			}
			if (this._itemInfoUI == null)
			{
				yield return CraftItemInfoUI.Load(this, this._itemInfoLayout, delegate(CraftItemInfoUI itemInfoUI)
				{
					this._itemInfoUI = itemInfoUI;
				});
			}
			UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
			this._recipeViewer.GetOrAddComponent<PointerEnterTrigger>().Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData x)
			{
				this.SetFocusLevel(this._recipeViewer.itemListUI.FocusLevel);
			});
			this._recipeViewer.itemListUI.CurrentChanged += delegate(int index, RecipeItemTitleNodeUI option)
			{
				if (option == null)
				{
					return;
				}
				this._itemInfoUI.createSum = option.data.CreateSum;
				StuffItem item = this.createItem.item;
				StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
				int num = option.MaxCount;
				if (!item2.isTrash)
				{
					if (Singleton<Map>.Instance.Player.PlayerData.FirstCreatedItemTable.Contains(item2.nameHash))
					{
						num = 0;
					}
					num = Mathf.Min(num, 1);
				}
				this._itemInfoUI.Refresh(num);
				this._itemInfoUI.isCountViewerVisible = (num > 0 && item2.isTrash);
				this.createItem.info = option.data;
			};
			CraftGroupUI[] array = this._craftTagSelection.group;
			this._craftVieweres = (from p in array
			select p.viewer).ToArray<CraftViewer>();
			CraftViewer[] craftVieweres = this._craftVieweres;
			for (int i = 0; i < craftVieweres.Length; i++)
			{
				CraftViewer item = craftVieweres[i];
				while (!item.initialized)
				{
					yield return null;
				}
				UITrigger.TriggerEvent onEnter = new UITrigger.TriggerEvent();
				item.GetOrAddComponent<PointerEnterTrigger>().Triggers.Add(onEnter);
				onEnter.AddListener(delegate(BaseEventData x)
				{
					this.SetFocusLevel(item.itemListUI.FocusLevel);
				});
				item.itemListUI.CurrentChanged += delegate(int index, CraftItemNodeUI option)
				{
					if (option == null)
					{
						return;
					}
					if (option.isUnknown)
					{
						this._itemInfoUI.Close();
					}
					else
					{
						this.createItem.item = new StuffItem(option.CategoryID, option.ID, 0);
						this._itemInfoUI.Open(this.createItem.item);
					}
					this._recipeViewer.itemListUI.ClearItems();
					this._recipeViewer.itemListUI.AddItemNode(index, option.data);
				};
			}
			this._craftTagSelection.Selection = delegate(int sel)
			{
				this._itemInfoUI.Close();
				this._recipeViewer.itemListUI.ClearItems();
				if (!this.IsActiveControl)
				{
					return;
				}
				for (int k = 0; k < array.Length; k++)
				{
					bool flag = k == sel;
					if (flag && this.currentViewer != array[k].viewer)
					{
						this.currentViewer = array[k].viewer;
						this.currentViewer.Refresh(this.$this);
					}
					array[k].SetActive(flag);
				}
				this.currentViewer.itemListUI.ForceSetNonSelect();
				this.playSE.Play(SoundPack.SystemSE.OK_S);
			};
			this._itemInfoUI.OnSubmit += delegate()
			{
				ConfirmScene.Sentence = this._itemInfoUI.ConfirmLabel;
				ConfirmScene.OnClickedYes = delegate()
				{
					int count = this._itemInfoUI.Count;
					foreach (RecipeDataInfo.NeedData needData in this.createItem.info.NeedList)
					{
						StuffItem.RemoveStorages(new StuffItem(needData.CategoryID, needData.ID, needData.Sum * count), this._checkStorages);
					}
					StuffItem item = this.createItem.item;
					foreach (List<StuffItem> list2 in this._checkStorages)
					{
						if (list2 == this._itemInfoUI.targetStorage)
						{
							list2.AddItem(new StuffItem(item), this.createItem.info.CreateSum * count);
							break;
						}
					}
					StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
					bool isTrash = item2.isTrash;
					if (!isTrash)
					{
						Singleton<Map>.Instance.Player.PlayerData.FirstCreatedItemTable.Add(item2.nameHash);
					}
					RecipeItemListUI itemListUI = this._recipeViewer.itemListUI;
					RecipeItemTitleNodeUI currentOption = itemListUI.CurrentOption;
					currentOption.Refresh();
					this._itemInfoUI.Refresh(isTrash ? currentOption.MaxCount : 0);
					if (!currentOption.isSuccess)
					{
						itemListUI.ForceSetNonSelect();
					}
					this.currentViewer.itemListUI.Refresh();
					this.playSE.Play(SoundPack.SystemSE.Craft);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					this.playSE.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			};
			foreach (CraftViewer craftViewer in this._craftVieweres)
			{
				craftViewer.itemListUI.OnCancel.AddListener(delegate()
				{
					this.OnInputCancel();
				});
			}
			this._recipeViewer.itemListUI.OnCancel.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._itemInfoUI.OnCancel += delegate()
			{
				this.OnInputCancel();
			};
			List<MenuUIBehaviour> list = new List<MenuUIBehaviour>();
			list.Add(this);
			list.Add(this._recipeViewer.itemListUI);
			list.Add(this._itemInfoUI);
			list.AddRange(from p in this._craftVieweres
			select p.itemListUI);
			this.MenuUIList = list.ToArray();
			this.initialized = true;
			yield break;
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x0031AA80 File Offset: 0x00318E80
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				MapUIContainer.SetVisibleHUD(false);
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.DoOpen();
			}
			else
			{
				MapUIContainer.SetVisibleHUD(true);
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.DoClose();
			}
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x0031AB58 File Offset: 0x00318F58
		private void Close()
		{
			if (Singleton<Map>.Instance.Player.Controller.State is Craft)
			{
				Time.timeScale = 1f;
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			this.IsActiveControl = false;
			if (this.Observer != null)
			{
				this.Observer.ChangeBackground(-1);
			}
			Action onClose = this.OnClose;
			if (onClose != null)
			{
				onClose();
			}
			this.playSE.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x06007516 RID: 29974 RVA: 0x0031ABD8 File Offset: 0x00318FD8
		private IEnumerator DoOpen()
		{
			if (this.Observer != null)
			{
				this.Observer.OnClose = delegate()
				{
					this.Close();
				};
			}
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			this._canvasGroup.blocksRaycasts = false;
			while (!this.initialized)
			{
				yield return null;
			}
			foreach (CraftViewer item in this._craftVieweres)
			{
				while (!item.initialized)
				{
					yield return null;
				}
			}
			foreach (Toggle toggle in this._craftTagSelection)
			{
				toggle.isOn = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIList)
			{
				menuUIBehaviour.EnabledInput = true;
			}
			if (this.Observer != null)
			{
				this.Observer.ActiveCloseButton = true;
			}
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x0031ABF4 File Offset: 0x00318FF4
		private IEnumerator DoClose()
		{
			if (this.Observer != null)
			{
				this.Observer.ActiveCloseButton = false;
			}
			foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIList)
			{
				menuUIBehaviour.EnabledInput = false;
			}
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			this._itemInfoUI.Close();
			foreach (CraftViewer craftViewer in this._craftVieweres)
			{
				craftViewer.itemListUI.ForceSetNonSelect();
			}
			this._recipeViewer.itemListUI.ClearItems();
			this._craftTagSelection[0].isOn = true;
			foreach (CraftViewer craftViewer2 in this._craftVieweres)
			{
				craftViewer2.itemListUI.ClearItems();
			}
			if (this.currentViewer != null)
			{
				this.currentViewer.Visible = false;
			}
			this.currentViewer = null;
			Singleton<Manager.Input>.Instance.SetupState();
			if (this.OnClosedEvent != null)
			{
				Action onClosedEvent = this.OnClosedEvent;
				this.OnClosedEvent = null;
				onClosedEvent();
			}
			this.playSE.use = false;
			yield break;
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x0031AC0F File Offset: 0x0031900F
		private new void SetFocusLevel(int level)
		{
			Singleton<Manager.Input>.Instance.FocusLevel = level;
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x0031AC1C File Offset: 0x0031901C
		private void OnInputSubmit()
		{
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x0031AC1E File Offset: 0x0031901E
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x04005F78 RID: 24440
		[Header("CraftUI Setting")]
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005F79 RID: 24441
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005F7A RID: 24442
		[Header("Craft Viewer")]
		[SerializeField]
		private CraftTagSelectionUI _craftTagSelection;

		// Token: 0x04005F7B RID: 24443
		private CraftViewer[] _craftVieweres;

		// Token: 0x04005F7C RID: 24444
		[Header("Recipe Viewer")]
		[SerializeField]
		private RectTransform _recipeViewerLayout;

		// Token: 0x04005F7D RID: 24445
		[SerializeField]
		private RecipeViewer _recipeViewer;

		// Token: 0x04005F7E RID: 24446
		[Header("InfoUI")]
		[SerializeField]
		private RectTransform _itemInfoLayout;

		// Token: 0x04005F7F RID: 24447
		[SerializeField]
		private CraftItemInfoUI _itemInfoUI;

		// Token: 0x04005F83 RID: 24451
		private IDisposable _fadeDisposable;

		// Token: 0x02000E6A RID: 3690
		public class CreateItem
		{
			// Token: 0x170016AE RID: 5806
			// (get) Token: 0x06007525 RID: 29989 RVA: 0x0031AC70 File Offset: 0x00319070
			// (set) Token: 0x06007526 RID: 29990 RVA: 0x0031AC78 File Offset: 0x00319078
			public StuffItem item { get; set; }

			// Token: 0x170016AF RID: 5807
			// (get) Token: 0x06007527 RID: 29991 RVA: 0x0031AC81 File Offset: 0x00319081
			// (set) Token: 0x06007528 RID: 29992 RVA: 0x0031AC89 File Offset: 0x00319089
			public RecipeDataInfo info { get; set; }
		}
	}
}
