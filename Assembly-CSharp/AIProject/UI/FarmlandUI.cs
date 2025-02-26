using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
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
	// Token: 0x02000E75 RID: 3701
	public class FarmlandUI : MenuUIBehaviour
	{
		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060075C5 RID: 30149 RVA: 0x0031E489 File Offset: 0x0031C889
		public PlaySE playSE { get; } = new PlaySE(false);

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060075C6 RID: 30150 RVA: 0x0031E491 File Offset: 0x0031C891
		// (set) Token: 0x060075C7 RID: 30151 RVA: 0x0031E499 File Offset: 0x0031C899
		public List<AIProject.SaveData.Environment.PlantInfo> currentPlant
		{
			get
			{
				return this._currentPlant;
			}
			set
			{
				this._currentPlant = value;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060075C8 RID: 30152 RVA: 0x0031E4A2 File Offset: 0x0031C8A2
		// (set) Token: 0x060075C9 RID: 30153 RVA: 0x0031E4AA File Offset: 0x0031C8AA
		private List<AIProject.SaveData.Environment.PlantInfo> _currentPlant { get; set; }

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060075CA RID: 30154 RVA: 0x0031E4B3 File Offset: 0x0031C8B3
		private MenuUIBehaviour[] MenuUIList
		{
			get
			{
				return this.GetCache(ref this._menuUIList, () => (from p in new MenuUIBehaviour[]
				{
					this,
					this._plantUI,
					this._plantInfoUI,
					this._inventoryUI.categoryUI,
					this._inventoryUI.itemListUI,
					this._harvestListViewer.itemListUI
				}
				where p != null
				select p).ToArray<MenuUIBehaviour>());
			}
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x0031E4CD File Offset: 0x0031C8CD
		private void CursorOFF()
		{
			this._inventoryUI.cursor.enabled = false;
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x0031E4E0 File Offset: 0x0031C8E0
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

		// Token: 0x060075CD RID: 30157 RVA: 0x0031E5B4 File Offset: 0x0031C9B4
		private IEnumerator BindingUI()
		{
			yield return base.StartCoroutine(this._inventoryUI.Initialize());
			this._inventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData x)
			{
				this.Planting(x.ID, x.node);
			};
			Observable.FromEvent<PlantIcon>(delegate(Action<PlantIcon> h)
			{
				this._plantUI.IconChanged += h;
			}, delegate(Action<PlantIcon> h)
			{
				this._plantUI.IconChanged -= h;
			}).Subscribe(delegate(PlantIcon icon)
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				bool flag = icon.info != null;
				this._inventoryUI.Visible = !flag;
				this._harvestListViewer.ClearList();
				if (flag)
				{
					this._plantInfoUI.Open(icon);
					if (icon.info.isEnd)
					{
						this._plantInfoUI.ItemCancelInteractable(false);
					}
					else
					{
						StuffItemInfo itemInfo = icon.itemInfo;
						if (itemInfo != null)
						{
							StuffItem item = new StuffItem(itemInfo.CategoryID, itemInfo.ID, 1);
							bool interactable = this._inventoryUI.itemList.CanAddItem(this._inventoryUI.slotCounter.y, item);
							this._plantInfoUI.ItemCancelInteractable(interactable);
						}
					}
				}
				else
				{
					this._plantInfoUI.Close();
				}
			}).AddTo(this);
			this._plantUI.AllGet.Subscribe(delegate(Unit _)
			{
				foreach (AIProject.SaveData.Environment.PlantInfo plantInfo in this.currentPlant.Where(delegate(AIProject.SaveData.Environment.PlantInfo item)
				{
					bool? flag2 = (item != null) ? new bool?(item.isEnd) : null;
					return flag2 != null && flag2.Value;
				}))
				{
					foreach (StuffItem stuffItem in from item in plantInfo.result
					where item.Count > 0
					select item)
					{
						if (Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, stuffItem.ID) != null)
						{
							this._inventoryUI.AddItem(stuffItem);
						}
					}
				}
				for (int i = 0; i < this.currentPlant.Count; i++)
				{
					AIProject.SaveData.Environment.PlantInfo plantInfo2 = this.currentPlant[i];
					bool? flag = (plantInfo2 != null) ? new bool?(plantInfo2.isEnd) : null;
					if (flag != null && flag.Value)
					{
						if (!this.currentPlant[i].result.Any((StuffItem item) => item.Count > 0))
						{
							this._plantUI.plantIcons[i].info = null;
							this.currentPlant[i] = null;
						}
					}
					if (this.currentPlant[i] == null && i == this._plantUI.currentIndex)
					{
						this._inventoryUI.Visible = true;
						this._plantInfoUI.Close();
					}
				}
				this._inventoryUI.Refresh();
				this._plantUI.Refresh();
			});
			this._harvestListViewer.GetItemAction += delegate(IReadOnlyCollection<StuffItem> collection)
			{
				foreach (StuffItem stuffItem in collection)
				{
					if (Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, stuffItem.ID) != null)
					{
						if (!this._inventoryUI.AddItem(stuffItem))
						{
							this._harvestListViewer.AddFailedList.Add(stuffItem);
						}
					}
				}
				this._inventoryUI.Refresh();
			};
			this._plantInfoUI.OnComplete.Subscribe(delegate(bool _)
			{
				this._plantInfoUI.ItemCancelInteractable(false);
				bool flag = true;
				foreach (StuffItem item2 in from item in this._plantInfoUI.currentIcon.info.result
				where item.Count > 0
				select item)
				{
					flag = false;
					this._harvestListViewer.AddList(item2);
				}
				if (flag)
				{
					this._harvestListViewer.AddList(StuffItem.CreateSystemItem(1, 0, 1));
				}
				this._harvestListViewer.InStorageCheck(null);
			}).AddTo(this);
			this._harvestListViewer.AllGetListAction += delegate()
			{
				this._plantUI.currentIcon.info = null;
				this.currentPlant[this._plantUI.currentIndex] = null;
				this._inventoryUI.Visible = true;
				this._plantInfoUI.Close();
			};
			this._plantInfoUI.OnSubmit.AddListener(delegate()
			{
				this.playSE.Play(SoundPack.SystemSE.OK_S);
				ConfirmScene.Sentence = "栽培を中止しますか？";
				ConfirmScene.BackAlpha = new float?(0f);
				ConfirmScene.Offset = new Vector2?(new Vector2(0f, -420f));
				ConfirmScene.OnClickedYes = delegate()
				{
					PlantIcon currentIcon = this._plantInfoUI.currentIcon;
					AIProject.SaveData.Environment.PlantInfo info = currentIcon.info;
					bool? flag = (info != null) ? new bool?(info.isEnd) : null;
					if (flag == null || flag.Value)
					{
						this._plantInfoUI.Close();
						return;
					}
					StuffItemInfo itemInfo = currentIcon.itemInfo;
					if (itemInfo != null)
					{
						bool interactable = this._inventoryUI.AddItem(new StuffItem(itemInfo.CategoryID, itemInfo.ID, 1));
						this._plantInfoUI.ItemCancelInteractable(interactable);
					}
					this.currentPlant[this._plantUI.currentIndex] = null;
					currentIcon.info = null;
					this._plantInfoUI.Close();
					this._inventoryUI.Refresh();
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
				this._plantButton.interactable = (option != null);
			};
			this._plantButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Planting(this._inventoryUI.itemListUI.CurrentID, this._inventoryUI.itemListUI.CurrentOption);
			});
			this._closeButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnInputCancel();
			});
			this._inventoryUI.itemListUI.CurrentChanged += delegate(int _, ItemNodeUI option)
			{
				this._allPlantButton.interactable = (option != null);
			};
			this._allPlantButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.PlantingForAll(this._inventoryUI.itemListUI.CurrentID, this._inventoryUI.itemListUI.CurrentOption);
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
			yield break;
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x0031E5D0 File Offset: 0x0031C9D0
		private void SetActiveControl(bool isActive)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (isActive)
			{
				MapUIContainer.SetVisibleHUD(false);
				Time.timeScale = 0f;
				Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Map>.Instance.Player.ReleaseInteraction();
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

		// Token: 0x060075CF RID: 30159 RVA: 0x0031E6D0 File Offset: 0x0031CAD0
		private void Close()
		{
			Time.timeScale = 1f;
			this.IsActiveControl = false;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			this.playSE.Play(SoundPack.SystemSE.Cancel);
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x0031E6F8 File Offset: 0x0031CAF8
		private IEnumerator DoOpen()
		{
			yield return MapUIContainer.DrawOnceTutorialUI(15, null);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
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
			this._canvasGroup.blocksRaycasts = false;
			while (!this._inventoryUI.initialized)
			{
				yield return null;
			}
			this._inventoryUI.ItemListNodeCreate();
			this._inventoryUI.Refresh();
			this._harvestListViewer.Refresh();
			while (this._currentPlant == null)
			{
				yield return null;
			}
			this._plantUI.Open(this.currentPlant);
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			List<MenuUIBehaviour> onInputs = new List<MenuUIBehaviour>();
			List<MenuUIBehaviour> offInputs = new List<MenuUIBehaviour>();
			if (this._inventoryUI.itemListUI.ItemVisibles.Any<ItemNodeUI>())
			{
				onInputs.Add(this._inventoryUI.itemListUI);
				onInputs.Add(this._inventoryUI.categoryUI);
			}
			else
			{
				offInputs.Add(this._inventoryUI.itemListUI);
				onInputs.Add(this._inventoryUI.categoryUI);
			}
			onInputs.ForEach(delegate(MenuUIBehaviour p)
			{
				p.EnabledInput = true;
			});
			offInputs.ForEach(delegate(MenuUIBehaviour p)
			{
				p.EnabledInput = false;
			});
			base.EnabledInput = true;
			this.playSE.use = true;
			yield break;
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x0031E714 File Offset: 0x0031CB14
		private IEnumerator DoClose()
		{
			base.EnabledInput = false;
			this._inventoryUI.itemListUI.EnabledInput = false;
			PlayerActor player = Singleton<Map>.Instance.Player;
			player.CameraControl.Mode = CameraMode.Normal;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			this._plantInfoUI.Close();
			this._inventoryUI.cursor.enabled = false;
			this._inventoryUI.itemListUI.ClearItems();
			player.Controller.ChangeState("Normal");
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			Singleton<Manager.Input>.Instance.SetupState();
			this.playSE.use = false;
			yield break;
		}

		// Token: 0x060075D2 RID: 30162 RVA: 0x0031E730 File Offset: 0x0031CB30
		private new void SetFocusLevel(int level)
		{
			this._inventoryUI.viewer.SetFocusLevel(level);
			base.EnabledInput = (level == base.FocusLevel);
			this._plantUI.EnabledInput = (level == this._plantUI.FocusLevel);
			this._plantInfoUI.EnabledInput = (level == this._plantInfoUI.FocusLevel);
			this._harvestListViewer.itemListUI.EnabledInput = (level == this._harvestListViewer.itemListUI.FocusLevel);
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x0031E7B4 File Offset: 0x0031CBB4
		private void Planting(int currentID, ItemNodeUI currentOption)
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
			this._plantUI.SetPlantItem(item);
			this._plantUI.Refresh();
			bool interactable = this._inventoryUI.itemList.CanAddItem(this._inventoryUI.slotCounter.y, item);
			this._plantInfoUI.ItemCancelInteractable(interactable);
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x0031E8A0 File Offset: 0x0031CCA0
		private void PlantingForAll(int currentID, ItemNodeUI currentOption)
		{
			int emptySum = this._plantUI.GetEmptySum();
			if (emptySum == 0)
			{
				return;
			}
			StuffItem item = currentOption.Item;
			StuffItem stuffItem = this._inventoryUI.itemList.Find((StuffItem x) => x == item);
			int num = 0;
			while (stuffItem.Count > 0)
			{
				stuffItem.Count--;
				if (++num >= emptySum)
				{
					break;
				}
			}
			if (stuffItem.Count <= 0)
			{
				this._inventoryUI.itemList.Remove(stuffItem);
				this._inventoryUI.itemListUI.RemoveItemNode(currentID);
				this._inventoryUI.itemListUI.ForceSetNonSelect();
			}
			this._inventoryUI.itemListUI.Refresh();
			this._plantUI.SetPlantItemForAll(item, num);
			this._plantUI.Refresh();
			bool interactable = this._inventoryUI.itemList.CanAddItem(this._inventoryUI.slotCounter.y, item);
			this._plantInfoUI.ItemCancelInteractable(interactable);
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x0031E9C2 File Offset: 0x0031CDC2
		private void OnInputSubmit()
		{
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x0031E9C4 File Offset: 0x0031CDC4
		private void OnInputCancel()
		{
			this.Close();
		}

		// Token: 0x04005FFB RID: 24571
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005FFC RID: 24572
		[SerializeField]
		private InventoryFacadeViewer _inventoryUI;

		// Token: 0x04005FFD RID: 24573
		[SerializeField]
		private HarvestListViewer _harvestListViewer;

		// Token: 0x04005FFE RID: 24574
		[SerializeField]
		private PlantUI _plantUI;

		// Token: 0x04005FFF RID: 24575
		[SerializeField]
		private PlantInfoUI _plantInfoUI;

		// Token: 0x04006000 RID: 24576
		[SerializeField]
		private Button _plantButton;

		// Token: 0x04006001 RID: 24577
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006002 RID: 24578
		[SerializeField]
		private Button _allPlantButton;

		// Token: 0x04006003 RID: 24579
		private IDisposable _fadeDisposable;

		// Token: 0x04006004 RID: 24580
		private MenuUIBehaviour[] _menuUIList;
	}
}
