using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AIProject.Animal;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Illusion;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E48 RID: 3656
	public class ChickenCoopUI : MenuUIBehaviour
	{
		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x060072F9 RID: 29433 RVA: 0x0030F9A1 File Offset: 0x0030DDA1
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x060072FA RID: 29434 RVA: 0x0030F9A9 File Offset: 0x0030DDA9
		// (set) Token: 0x060072FB RID: 29435 RVA: 0x0030F9B1 File Offset: 0x0030DDB1
		public List<AIProject.SaveData.Environment.ChickenInfo> currentChickens
		{
			get
			{
				return this._currentChickens;
			}
			set
			{
				this._currentChickens = value;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x060072FC RID: 29436 RVA: 0x0030F9BA File Offset: 0x0030DDBA
		// (set) Token: 0x060072FD RID: 29437 RVA: 0x0030F9C2 File Offset: 0x0030DDC2
		private List<AIProject.SaveData.Environment.ChickenInfo> _currentChickens { get; set; }

		// Token: 0x060072FE RID: 29438 RVA: 0x0030F9CB File Offset: 0x0030DDCB
		public void SetMode(ChickenCoopUI.Mode mode)
		{
			this._mode = mode;
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x0030F9D4 File Offset: 0x0030DDD4
		private void HarvestListOpen()
		{
			this._harvestListCG.alpha = 1f;
			this._harvestListCG.blocksRaycasts = true;
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x0030F9F2 File Offset: 0x0030DDF2
		private void HarvestListClose()
		{
			this._harvestListCG.alpha = 0f;
			this._harvestListCG.blocksRaycasts = false;
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06007301 RID: 29441 RVA: 0x0030FA10 File Offset: 0x0030DE10
		private MenuUIBehaviour[] MenuUIList
		{
			get
			{
				return this.GetCache(ref this._menuUIList, () => (from p in new MenuUIBehaviour[]
				{
					this,
					this._chickenCoopListUI,
					this._chickenNameChangeUI,
					this._inventoryUI.categoryUI,
					this._inventoryUI.itemListUI,
					this._harvestListViewer.itemListUI
				}
				where p != null
				select p).ToArray<MenuUIBehaviour>());
			}
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x0030FA2A File Offset: 0x0030DE2A
		private void CursorOFF()
		{
			this._inventoryUI.cursor.enabled = false;
		}

		// Token: 0x06007303 RID: 29443 RVA: 0x0030FA40 File Offset: 0x0030DE40
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

		// Token: 0x06007304 RID: 29444 RVA: 0x0030FB14 File Offset: 0x0030DF14
		private IEnumerator BindingUI()
		{
			yield return base.StartCoroutine(this._inventoryUI.Initialize());
			this._inventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData x)
			{
				this.Send(x.ID, x.node);
			};
			this._harvestListViewer.GetItemAction += delegate(IReadOnlyCollection<StuffItem> collection)
			{
				foreach (StuffItem item in collection)
				{
					if (!this._inventoryUI.AddItem(item))
					{
						this._harvestListViewer.AddFailedList.Add(item);
					}
				}
			};
			this._harvestListViewer.SyncListAction += delegate(IReadOnlyCollection<StuffItem> collection)
			{
				Singleton<Game>.Instance.Environment.ItemListInEggBox.Clear();
				foreach (StuffItem item in collection)
				{
					Singleton<Game>.Instance.Environment.ItemListInEggBox.AddItem(item);
				}
			};
			while (!Singleton<Map>.IsInstance() || Singleton<Map>.Instance.Simulator == null)
			{
				yield return null;
			}
			while (!Singleton<MapScene>.IsInstance() || !Singleton<MapScene>.Instance.isLoadEnd)
			{
				yield return null;
			}
			bool dayChanged = !Singleton<Map>.Instance.IsHour7After;
			Action resetAction = delegate()
			{
				dayChanged = false;
				int num = Singleton<Game>.Instance.Environment.ChickenTable.Values.SelectMany((List<AIProject.SaveData.Environment.ChickenInfo> x) => x).Count((AIProject.SaveData.Environment.ChickenInfo x) => x != null);
				for (int i = 0; i < num; i++)
				{
					StuffItem item;
					if (Illusion.Utils.ProbabilityCalclator.DetectFromPercent(95f))
					{
						item = new StuffItem(7, 0, 1);
					}
					else
					{
						item = new StuffItem(9, 5, 1);
					}
					Singleton<Game>.Instance.Environment.ItemListInEggBox.AddItem(item);
				}
			};
			Singleton<Map>.Instance.Simulator.OnDayAsObservable().TakeUntilDestroy(base.gameObject).Subscribe(delegate(TimeSpan _)
			{
				dayChanged = true;
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where dayChanged
			select _).Subscribe(delegate(long _)
			{
				if (Singleton<Map>.Instance.IsHour7After)
				{
					resetAction();
				}
			});
			this._chickenCoopListUI.Escape.Subscribe(delegate(Unit _)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				ConfirmScene.Sentence = "ニワトリを逃がしますか？\n" + "逃がすとアイテムとしては戻ってきません。".Coloring("#DE4529FF").Size(24);
				ConfirmScene.OnClickedYes = delegate()
				{
					int currentIndex = this._chickenCoopListUI.currentIndex;
					this.RemoveChicken(currentIndex, this._currentChickens[currentIndex]);
					this._currentChickens[currentIndex] = null;
					this._chickenCoopListUI.Refresh(currentIndex);
					this._inventoryUI.Visible = true;
					this.playSE.Play(SoundPack.SystemSE.OK_L);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					this.playSE.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			});
			this._inventoryUI.itemListUI.CurrentChanged += delegate(int _, ItemNodeUI option)
			{
				this._addButton.interactable = (option != null);
			};
			this._addButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this.Send(this._inventoryUI.itemListUI.CurrentID, this._inventoryUI.itemListUI.CurrentOption);
			});
			this._inventoryUI.categoryUI.OnSubmit.AddListener(delegate()
			{
				if (this._inventoryUI.categoryUI.useCursor)
				{
					this._inventoryUI.categoryUI.SelectedButton.onClick.Invoke();
				}
			});
			this._inventoryUI.categoryUI.OnCancel.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._inventoryUI.categoryUI.OnEntered = delegate()
			{
				this.CursorOFF();
				this.SetFocusLevel(this._inventoryUI.categoryUI.FocusLevel);
			};
			Observable.Merge<PointerEventData>(new IObservable<PointerEventData>[]
			{
				this._inventoryUI.categoryUI.leftButton.OnPointerEnterAsObservable(),
				this._inventoryUI.categoryUI.rightButton.OnPointerEnterAsObservable()
			}).Subscribe(delegate(PointerEventData _)
			{
				this._inventoryUI.categoryUI.OnEntered();
			}).AddTo(this);
			(from _ in this._inventoryUI.itemListUI.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				if (this._inventoryUI.itemListUI.ItemVisibles.Any<ItemNodeUI>())
				{
					this.CursorOFF();
					this.SetFocusLevel(this._inventoryUI.itemListUI.FocusLevel);
				}
			});
			this._inventoryUI.itemListUI.OnCancel.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			(from _ in this._harvestListViewer.itemListUI.OnEntered
			where this.IsActiveControl
			select _).Subscribe(delegate(int _)
			{
				if (this._harvestListViewer.itemListUI.ItemVisibles.Any<ItemNodeUI>())
				{
					this.CursorOFF();
					this.SetFocusLevel(this._harvestListViewer.itemListUI.FocusLevel);
				}
			});
			this._harvestListViewer.itemListUI.OnCancel.AddListener(delegate()
			{
				this.OnInputCancel();
			});
			this._harvestListCloseButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			Observable.FromEvent(delegate(Action h)
			{
				this._chickenCoopListUI.OnCancel += h;
			}, delegate(Action h)
			{
				this._chickenCoopListUI.OnCancel -= h;
			}).Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			}).AddTo(this);
			this._chickenCoopListUI.IconChanged += delegate(int _)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				this._inventoryUI.Visible = !this._chickenCoopListUI.currentActive;
			};
			yield break;
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x0030FB30 File Offset: 0x0030DF30
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
				if (playerActor != null)
				{
					this._currentFarmPoint = playerActor.CurrentFarmPoint;
				}
				Time.timeScale = 0f;
				Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Map>.Instance.Player.ReleaseInteraction();
				this._prevFocusLevel = instance.FocusLevel;
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.DoOpen();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = this._prevFocusLevel;
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

		// Token: 0x06007306 RID: 29446 RVA: 0x0030FC68 File Offset: 0x0030E068
		private void Close()
		{
			Time.timeScale = 1f;
			this.IsActiveControl = false;
			this.playSE.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x0030FC88 File Offset: 0x0030E088
		private IEnumerator DoOpen()
		{
			this._canvasGroup.blocksRaycasts = false;
			while (Singleton<Map>.Instance.Player == null)
			{
				yield return null;
			}
			this._inventoryUI.SetItemList(Singleton<Map>.Instance.Player.PlayerData.ItemList);
			this._inventoryUI.slotCounter.y = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
			if (!this._inventoryUI.itemListUI.isOptionNode)
			{
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "ItemOption_ItemBox", false, string.Empty);
				if (gameObject == null)
				{
					yield break;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
				}
				this._inventoryUI.itemListUI.SetOptionNode(gameObject);
			}
			while (!this._inventoryUI.initialized)
			{
				yield return null;
			}
			this._inventoryUI.ItemListNodeCreate();
			this._inventoryUI.Refresh();
			ChickenCoopUI.Mode mode = this._mode;
			if (mode != ChickenCoopUI.Mode.EggBox)
			{
				if (mode == ChickenCoopUI.Mode.Coop)
				{
					while (this._currentChickens == null)
					{
						yield return null;
					}
					this._chickenCoopListUI.SelectDefault();
					this._chickenCoopListUI.Open();
				}
			}
			else
			{
				this._harvestListViewer.ClearList();
				foreach (StuffItem item in Singleton<Game>.Instance.Environment.ItemListInEggBox)
				{
					this._harvestListViewer.AddList(item);
				}
				this.HarvestListOpen();
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			base.EnabledInput = true;
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x0030FCA4 File Offset: 0x0030E0A4
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			this._inventoryUI.itemListUI.EnabledInput = false;
			PlayerActor player = Singleton<Map>.Instance.Player;
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			ChickenCoopUI.Mode mode = this._mode;
			if (mode != ChickenCoopUI.Mode.EggBox)
			{
				if (mode == ChickenCoopUI.Mode.Coop)
				{
					this._chickenCoopListUI.SetToggle(0, true);
					this._chickenCoopListUI.Close();
					this._inventoryUI.Visible = false;
					this._inventoryUI.cursor.enabled = false;
					this._inventoryUI.itemListUI.ClearItems();
				}
			}
			else
			{
				this.HarvestListClose();
			}
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			Action closedEvent = this.ClosedEvent;
			if (closedEvent != null)
			{
				closedEvent();
			}
			this.playSE.use = false;
			yield break;
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06007309 RID: 29449 RVA: 0x0030FCBF File Offset: 0x0030E0BF
		// (set) Token: 0x0600730A RID: 29450 RVA: 0x0030FCC7 File Offset: 0x0030E0C7
		public Action ClosedEvent { get; set; }

		// Token: 0x0600730B RID: 29451 RVA: 0x0030FCD0 File Offset: 0x0030E0D0
		private new void SetFocusLevel(int level)
		{
			this._inventoryUI.viewer.SetFocusLevel(level);
			this._harvestListViewer.itemListUI.EnabledInput = (level == this._harvestListViewer.itemListUI.FocusLevel);
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x0030FD08 File Offset: 0x0030E108
		private void Send(int currentID, ItemNodeUI currentOption)
		{
			StuffItem item = currentOption.Item;
			StuffItem stuffItem = this._inventoryUI.itemList.Find((StuffItem x) => x == item);
			stuffItem.Count--;
			if (stuffItem.Count <= 0)
			{
				this._inventoryUI.itemList.Remove(stuffItem);
				this._inventoryUI.itemListUI.RemoveItemNode(currentID);
				this._inventoryUI.itemListUI.ForceSetNonSelect();
			}
			this._inventoryUI.itemListUI.Refresh();
			int currentIndex = this._chickenCoopListUI.currentIndex;
			while (this.currentChickens.Count <= currentIndex)
			{
				this.currentChickens.Add(null);
			}
			AIProject.SaveData.Environment.ChickenInfo chickenInfo = new AIProject.SaveData.Environment.ChickenInfo();
			chickenInfo.name = "ニワトリ";
			UnityEx.ValueTuple<AIProject.SaveData.AnimalData, PetChicken> valueTuple = this.CreateChicken(chickenInfo);
			chickenInfo.AnimalData = valueTuple.Item1;
			if (this._currentFarmPoint != null)
			{
				this._currentFarmPoint.AddChicken(currentIndex, valueTuple.Item2);
			}
			this.currentChickens[currentIndex] = chickenInfo;
			this._chickenCoopListUI.Refresh(currentIndex);
			this._inventoryUI.Visible = false;
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x0030FE40 File Offset: 0x0030E240
		private UnityEx.ValueTuple<AIProject.SaveData.AnimalData, PetChicken> CreateChicken(AIProject.SaveData.Environment.ChickenInfo info)
		{
			UnityEx.ValueTuple<AIProject.SaveData.AnimalData, PetChicken> result = new UnityEx.ValueTuple<AIProject.SaveData.AnimalData, PetChicken>(null, null);
			if (info == null)
			{
				return result;
			}
			if (!Singleton<AnimalManager>.IsInstance() || this._currentFarmPoint == null)
			{
				return result;
			}
			int animalTypeID = 1;
			AnimalBase animalBase = Singleton<AnimalManager>.Instance.CreateBase(animalTypeID, 1);
			IPetAnimal petAnimal = animalBase as IPetAnimal;
			if (animalBase == null)
			{
				return result;
			}
			animalBase.transform.SetParent(this._currentFarmPoint.AnimalRoot, true);
			AIProject.SaveData.AnimalData animalData = new AIProject.SaveData.AnimalData();
			animalData.AnimalID = animalBase.AnimalID;
			animalData.RegisterID = this._currentFarmPoint.RegisterID;
			animalData.AnimalType = AnimalTypes.Chicken;
			animalData.AnimalTypeID = animalTypeID;
			animalData.InitAnimalTypeID = true;
			animalData.BreedingType = BreedingTypes.Pet;
			animalData.Nickname = info.name;
			ItemIDKeyPair itemID = animalBase.ItemID;
			animalData.ItemCategoryID = itemID.categoryID;
			animalData.ItemID = itemID.itemID;
			if (petAnimal != null)
			{
				petAnimal.AnimalData = animalData;
				if (animalBase is PetChicken)
				{
					(animalBase as PetChicken).Initialize(this._currentFarmPoint);
				}
			}
			result.Item1 = animalData;
			result.Item2 = (animalBase as PetChicken);
			return result;
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x0030FF70 File Offset: 0x0030E370
		private void RemoveChicken(int index, AIProject.SaveData.Environment.ChickenInfo info)
		{
			if (info == null || info.AnimalData == null)
			{
				return;
			}
			if (!Singleton<AnimalManager>.IsInstance())
			{
				return;
			}
			ReadOnlyDictionary<int, AnimalBase> animalTable = Singleton<AnimalManager>.Instance.AnimalTable;
			AIProject.SaveData.AnimalData animalData = info.AnimalData;
			AnimalBase animalBase;
			if (animalTable.TryGetValue(animalData.AnimalID, out animalBase) && animalBase != null)
			{
				if (this._currentFarmPoint != null)
				{
					this._currentFarmPoint.RemoveChicken(index, animalBase as PetChicken);
				}
				if (animalBase is IPetAnimal)
				{
					(animalBase as IPetAnimal).Release();
				}
			}
		}

		// Token: 0x0600730F RID: 29455 RVA: 0x00310004 File Offset: 0x0030E404
		private void OnInputSubmit()
		{
		}

		// Token: 0x06007310 RID: 29456 RVA: 0x00310006 File Offset: 0x0030E406
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x04005E1A RID: 24090
		[Header("ChickenCoopUI")]
		[SerializeField]
		private ChickenCoopUI.Mode _mode;

		// Token: 0x04005E1B RID: 24091
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005E1C RID: 24092
		[SerializeField]
		private InventoryFacadeViewer _inventoryUI;

		// Token: 0x04005E1D RID: 24093
		[SerializeField]
		private HarvestListViewer _harvestListViewer;

		// Token: 0x04005E1E RID: 24094
		[SerializeField]
		private ChickenCoopListUI _chickenCoopListUI;

		// Token: 0x04005E1F RID: 24095
		[SerializeField]
		private ChickenNameChangeUI _chickenNameChangeUI;

		// Token: 0x04005E20 RID: 24096
		[SerializeField]
		private Button _addButton;

		// Token: 0x04005E21 RID: 24097
		[Header("HarvestList")]
		[SerializeField]
		private CanvasGroup _harvestListCG;

		// Token: 0x04005E22 RID: 24098
		[SerializeField]
		private Button _harvestListCloseButton;

		// Token: 0x04005E23 RID: 24099
		private IDisposable _fadeDisposable;

		// Token: 0x04005E24 RID: 24100
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005E25 RID: 24101
		private FarmPoint _currentFarmPoint;

		// Token: 0x04005E26 RID: 24102
		private int _prevFocusLevel = -1;

		// Token: 0x02000E49 RID: 3657
		public enum Mode
		{
			// Token: 0x04005E2C RID: 24108
			EggBox,
			// Token: 0x04005E2D RID: 24109
			Coop
		}
	}
}
