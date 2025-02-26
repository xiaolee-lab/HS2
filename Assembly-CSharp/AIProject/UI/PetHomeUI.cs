using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.Animal.Resources;
using AIProject.MiniGames.Fishing;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
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
	// Token: 0x02000E9C RID: 3740
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class PetHomeUI : MenuUIBehaviour
	{
		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x060078B1 RID: 30897 RVA: 0x0032D653 File Offset: 0x0032BA53
		public CanvasGroup CanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x060078B2 RID: 30898 RVA: 0x0032D65B File Offset: 0x0032BA5B
		public RectTransform RectTransform
		{
			[CompilerGenerated]
			get
			{
				return this._rectTransform;
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x060078B3 RID: 30899 RVA: 0x0032D664 File Offset: 0x0032BA64
		public MenuUIBehaviour[] MenuUIBehaviorList
		{
			get
			{
				if (this._menuUIBehaviorList == null)
				{
					this._menuUIBehaviorList = new MenuUIBehaviour[]
					{
						this,
						this._nameChangeUI,
						this._inventoryUI.categoryUI,
						this._inventoryUI.itemListUI
					};
				}
				return this._menuUIBehaviorList;
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x060078B4 RID: 30900 RVA: 0x0032D6B7 File Offset: 0x0032BAB7
		// (set) Token: 0x060078B5 RID: 30901 RVA: 0x0032D6DF File Offset: 0x0032BADF
		private float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x060078B6 RID: 30902 RVA: 0x0032D6FE File Offset: 0x0032BAFE
		private void CursorOFF()
		{
			this._inventoryUI.cursor.enabled = false;
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x0032D714 File Offset: 0x0032BB14
		protected override void Awake()
		{
			base.Awake();
			if (this._canvasGroup == null)
			{
				this._canvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x0032D764 File Offset: 0x0032BB64
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			this._lerpStream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this);
			if (this._closeButton != null)
			{
				this._closeButton.onClick.AddListener(delegate()
				{
					this.DoClose();
				});
			}
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			if (this._escapeButton != null)
			{
				this._escapeButton.onClick.AddListener(delegate()
				{
					this.RemoveAnimal();
				});
			}
			this._chaseToggle.OnValueChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.ChangeChaseActor(x);
			});
			if (this._nameChangeButton != null)
			{
				this._nameChangeButton.onClick.AddListener(delegate()
				{
					this.DoNameChange();
				});
			}
			PetNameChangeUI nameChangeUI = this._nameChangeUI;
			nameChangeUI.SubmitAction = (Action<string>)Delegate.Combine(nameChangeUI.SubmitAction, new Action<string>(delegate(string str)
			{
				this.NameChanged(str);
			}));
			base.StartCoroutine(this.UISettingCoroutine());
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x0032D8E4 File Offset: 0x0032BCE4
		private IEnumerator UISettingCoroutine()
		{
			yield return base.StartCoroutine(this._inventoryUI.Initialize());
			this._inventoryUI.ItemNodeOnDoubleClick = delegate(InventoryFacadeViewer.DoubleClickData x)
			{
				this.Send(x.ID, x.node);
			};
			this._inventoryUI.itemListUI.CurrentChanged += delegate(int _, ItemNodeUI option)
			{
				this._addButton.interactable = (option != null);
			};
			this._addButton.onClick.AddListener(delegate()
			{
				this.Send(this._inventoryUI.itemListUI.CurrentID, this._inventoryUI.itemListUI.CurrentOption);
			});
			this._inventoryUI.categoryUI.OnSubmit.AddListener(delegate()
			{
				if (this._inventoryUI.categoryUI.useCursor)
				{
					this._inventoryUI.categoryUI.SelectedButton.onClick.Invoke();
				}
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
				this.DoClose();
			});
			this._inventoryUI.categoryUI.OnEntered = delegate()
			{
				this.CursorOFF();
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
				}
			});
			this._inventoryUI.itemListUI.OnCancel.AddListener(delegate()
			{
				this.DoClose();
			});
			this.ForcedClose();
			this.SetActive(this, false);
			yield break;
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x0032D900 File Offset: 0x0032BD00
		private void SetActiveControl(bool active)
		{
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).TakeUntilDestroy(this).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x0032D9A5 File Offset: 0x0032BDA5
		private void DoClose()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x0032D9AE File Offset: 0x0032BDAE
		private void ForcedClose()
		{
			this.CanvasAlpha = 0f;
			this._nameChangeUI.QuickClose();
			this._inventoryUI.Visible = false;
			this.SetAllFocusLevel(0);
			this.SetAllEnableInput(false);
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x0032D9E0 File Offset: 0x0032BDE0
		private IEnumerator OpenCoroutine()
		{
			this.SetActive(base.gameObject, true);
			MapUIContainer.SetVisibleHUD(false);
			this._prevTimeScale = Time.timeScale;
			Time.timeScale = 0f;
			this.SetAllEnableInput(false);
			this.SetBlockRaycast(true);
			PlayerActor player = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			Manager.Resources res = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
			PlayerActor playerActor = player;
			this._currentPetHomePoint = ((playerActor != null) ? playerActor.CurrentPetHomePoint : null);
			if (player == null || this._currentPetHomePoint == null)
			{
				this.IsActiveControl = false;
				yield break;
			}
			if (res == null)
			{
				this.IsActiveControl = false;
				yield break;
			}
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this.SettingInventoryFilter();
			this._inventoryUI.SetItemList(player.PlayerData.ItemList);
			this._inventoryUI.slotCounter.y = player.PlayerData.InventorySlotMax;
			if (!this._inventoryUI.itemListUI.isOptionNode)
			{
				string mapScenePrefab = res.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(mapScenePrefab, "ItemOption_ItemBox", false, string.Empty);
				if (gameObject == null)
				{
					this.IsActiveControl = false;
					yield break;
				}
				MapScene.AddAssetBundlePath(mapScenePrefab);
				this._inventoryUI.itemListUI.SetOptionNode(gameObject);
			}
			while (!this._inventoryUI.initialized)
			{
				yield return null;
			}
			this._inventoryUI.ItemListNodeCreate();
			this._inventoryUI.Refresh();
			IPetAnimal petAnimal = this._currentPetHomePoint.User;
			PetHomePoint.HomeKind homeKind = this._currentPetHomePoint.Kind;
			Dictionary<int, UnityEx.ValueTuple<int, List<string>>> petHomeUIInfoTable = res.AnimalTable.PetHomeUIInfoTable;
			Dictionary<int, Sprite> actionIconTable = res.itemIconTables.ActionIconTable;
			Dictionary<int, List<UnityEx.ValueTuple<ItemIDKeyPair, int>>> petItemInfoTable = res.AnimalTable.PetItemInfoTable;
			Sprite mainIcon = null;
			UnityEx.ValueTuple<int, List<string>> layoutInfo;
			petHomeUIInfoTable.TryGetValue((int)homeKind, out layoutInfo);
			actionIconTable.TryGetValue(layoutInfo.Item1, out mainIcon);
			int langIdx = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			this._mainIconImage.sprite = mainIcon;
			this._mainTitleText.text = layoutInfo.Item2.GetElement(langIdx);
			this.SetActive(this._selectImage, false);
			this.SetActive(this._nameChangeButton, petAnimal != null);
			this.SetInteractable(this._escapeButton, petAnimal != null);
			AIProject.SaveData.Environment.PetHomeInfo pointData = this._currentPetHomePoint.SaveData;
			this._chaseToggle.isOn = pointData.ChaseActor;
			if (petAnimal == null)
			{
				this._elementText.text = this._noneNameStr;
			}
			else
			{
				this._elementText.text = petAnimal.Nickname;
			}
			this._inventoryUI.Visible = (petAnimal == null);
			this.SetActive(this._chaseToggle, homeKind == PetHomePoint.HomeKind.PetMat);
			base.EnabledInput = true;
			Manager.Input inputManager = Singleton<Manager.Input>.Instance;
			inputManager.MenuElements = this.MenuUIBehaviorList;
			this._prevFocusLevel = inputManager.FocusLevel;
			this.SetAllFocusLevel(++inputManager.FocusLevel);
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.SetInteractable(true);
			this.SetAllEnableInput(true);
			yield break;
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x0032D9FC File Offset: 0x0032BDFC
		private IEnumerator CloseCoroutine()
		{
			this.PlaySystemSE(SoundPack.SystemSE.Cancel);
			MapUIContainer.SetVisibleHUD(true);
			this.SetAllEnableInput(false);
			this.SetInteractable(false);
			Manager.Input inputManager = Singleton<Manager.Input>.Instance;
			inputManager.FocusLevel = this._prevFocusLevel;
			inputManager.ClearMenuElements();
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Connect();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this.SetBlockRaycast(false);
			this._nameChangeUI.QuickClose();
			Time.timeScale = this._prevTimeScale;
			inputManager.ReserveState(Manager.Input.ValidType.Action);
			inputManager.SetupState();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			PlayerActor player = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			if (player != null)
			{
				player.SetScheduledInteractionState(true);
				player.ReleaseInteraction();
				player.PlayerController.ChangeState("Normal");
			}
			if (((this._currentPetHomePoint != null) ? this._currentPetHomePoint.User : null) != null)
			{
				IPetAnimal user = this._currentPetHomePoint.User;
				if (user is IGroundPet)
				{
					(user as IGroundPet).ChaseActor = this._currentPetHomePoint.SaveData.ChaseActor;
				}
			}
			this.SetActive(base.gameObject, false);
			yield break;
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x0032DA18 File Offset: 0x0032BE18
		private void SettingInventoryFilter()
		{
			Manager.Resources resources = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
			PlayerActor x = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			if (resources == null || x == null)
			{
				return;
			}
			PetHomePoint.HomeKind kind = this._currentPetHomePoint.Kind;
			Dictionary<int, List<UnityEx.ValueTuple<ItemIDKeyPair, int>>> petItemInfoTable = resources.AnimalTable.PetItemInfoTable;
			int key = (int)kind;
			InventoryFacadeViewer.ItemFilter[] array = null;
			if (!this._itemFilterTable.TryGetValue(key, out array))
			{
				List<UnityEx.ValueTuple<ItemIDKeyPair, int>> list;
				petItemInfoTable.TryGetValue(key, out list);
				if (!list.IsNullOrEmpty<UnityEx.ValueTuple<ItemIDKeyPair, int>>())
				{
					Dictionary<int, List<int>> dictionary = DictionaryPool<int, List<int>>.Get();
					foreach (UnityEx.ValueTuple<ItemIDKeyPair, int> valueTuple in list)
					{
						ItemIDKeyPair item = valueTuple.Item1;
						List<int> list2;
						if (!dictionary.TryGetValue(item.categoryID, out list2) || list2 == null)
						{
							List<int> list3 = ListPool<int>.Get();
							dictionary[item.categoryID] = list3;
							list2 = list3;
						}
						list2.Add(item.itemID);
					}
					array = new InventoryFacadeViewer.ItemFilter[dictionary.Count];
					int num = 0;
					foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary)
					{
						int[] array2 = new int[keyValuePair.Value.Count];
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i] = keyValuePair.Value[i];
						}
						array[num] = new InventoryFacadeViewer.ItemFilter(keyValuePair.Key, array2);
						num++;
					}
					List<int> list4 = dictionary.Keys.ToList<int>();
					for (int j = 0; j < list4.Count; j++)
					{
						ListPool<int>.Release(dictionary[list4[j]]);
					}
					DictionaryPool<int, List<int>>.Release(dictionary);
					this._itemFilterTable[key] = array;
					this._inventoryUI.SetItemFilter(array);
				}
				else
				{
					this._itemFilterTable[key] = this._emptyFilter;
					this._inventoryUI.SetItemFilter(this._emptyFilter);
				}
			}
			else
			{
				this._inventoryUI.SetItemFilter(array);
			}
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x0032DC9C File Offset: 0x0032C09C
		private void Send(int currentID, ItemNodeUI currentOption)
		{
			if (this._currentPetHomePoint == null)
			{
				return;
			}
			Dictionary<int, List<UnityEx.ValueTuple<ItemIDKeyPair, int>>> dictionary = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.AnimalTable.PetItemInfoTable;
			if (dictionary.IsNullOrEmpty<int, List<UnityEx.ValueTuple<ItemIDKeyPair, int>>>())
			{
				return;
			}
			int kind = (int)this._currentPetHomePoint.Kind;
			List<UnityEx.ValueTuple<ItemIDKeyPair, int>> list;
			if (!dictionary.TryGetValue(kind, out list) || list.IsNullOrEmpty<UnityEx.ValueTuple<ItemIDKeyPair, int>>())
			{
				return;
			}
			StuffItem item = currentOption.Item;
			int index;
			if ((index = list.FindIndex((UnityEx.ValueTuple<ItemIDKeyPair, int> x) => x.Item1.categoryID == item.CategoryID && x.Item1.itemID == item.ID)) < 0)
			{
				return;
			}
			UnityEx.ValueTuple<ItemIDKeyPair, int> valueTuple = list[index];
			StuffItem stuffItem = this._inventoryUI.itemList.Find((StuffItem x) => x == item);
			stuffItem.Count--;
			if (stuffItem.Count <= 0)
			{
				this._inventoryUI.itemList.Remove(stuffItem);
				this._inventoryUI.itemListUI.RemoveItemNode(currentID);
				this._inventoryUI.itemListUI.ForceSetNonSelect();
			}
			this._inventoryUI.itemListUI.Refresh();
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			string name = item2.Name;
			IPetAnimal petAnimal = null;
			int pointID = 0;
			PetHomePoint.HomeKind kind2 = this._currentPetHomePoint.Kind;
			if (kind2 != PetHomePoint.HomeKind.PetMat)
			{
				if (kind2 == PetHomePoint.HomeKind.FishTank)
				{
					Dictionary<int, Dictionary<int, FishInfo>> dictionary2 = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.Fishing.FishInfoTable;
					Dictionary<int, FishInfo> dictionary3;
					FishInfo fishInfo;
					if (dictionary2 != null && dictionary2.TryGetValue(item.CategoryID, out dictionary3) && !dictionary3.IsNullOrEmpty<int, FishInfo>() && dictionary3.TryGetValue(item.ID, out fishInfo))
					{
						petAnimal = (Singleton<AnimalManager>.Instance.CreateBase(valueTuple.Item2, 1) as IPetAnimal);
						pointID = fishInfo.TankPointID;
					}
				}
			}
			else
			{
				petAnimal = (Singleton<AnimalManager>.Instance.CreateBase(valueTuple.Item2, 1) as IPetAnimal);
			}
			this._elementText.text = (name ?? this._noneNameStr);
			this.SetActive(this._nameChangeButton, petAnimal != null);
			if (petAnimal != null)
			{
				this._inventoryUI.Visible = false;
				this.SetInteractable(this._escapeButton, true);
				petAnimal.AnimalData = new AIProject.SaveData.AnimalData
				{
					First = true,
					AnimalID = petAnimal.AnimalID,
					RegisterID = this._currentPetHomePoint.RegisterID,
					AnimalType = (AnimalTypes)(1 << valueTuple.Item2),
					AnimalTypeID = valueTuple.Item2,
					InitAnimalTypeID = true,
					BreedingType = BreedingTypes.Pet,
					ItemCategoryID = item.CategoryID,
					ItemID = item.ID,
					ModelID = this.GetPetAnimalModelID(valueTuple.Item2),
					Position = this._currentPetHomePoint.Position,
					Rotation = this._currentPetHomePoint.Rotation
				};
				petAnimal.Nickname = name;
				if (petAnimal is IGroundPet)
				{
					(petAnimal as IGroundPet).ChaseActor = this._currentPetHomePoint.SaveData.ChaseActor;
				}
				this._currentPetHomePoint.SetUser(petAnimal);
				this._currentPetHomePoint.SetRootPoint(pointID, petAnimal);
			}
			this.PlaySystemSE(SoundPack.SystemSE.OK_L);
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x0032E030 File Offset: 0x0032C430
		private int GetPetAnimalModelID(int animalTypeID)
		{
			Dictionary<int, Dictionary<int, AnimalModelInfo>> dictionary;
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Manager.Resources.AnimalTables animalTable = Singleton<Manager.Resources>.Instance.AnimalTable;
				dictionary = ((animalTable != null) ? animalTable.ModelInfoTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, Dictionary<int, AnimalModelInfo>> dictionary2 = dictionary;
			if (dictionary2.IsNullOrEmpty<int, Dictionary<int, AnimalModelInfo>>())
			{
				return -1;
			}
			Dictionary<int, AnimalModelInfo> dictionary3;
			if (dictionary2.TryGetValue(animalTypeID, out dictionary3) && !dictionary3.IsNullOrEmpty<int, AnimalModelInfo>())
			{
				return dictionary3.Keys.ToList<int>().Rand<int>();
			}
			return -1;
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x0032E0A0 File Offset: 0x0032C4A0
		private void RemoveAnimal()
		{
			IPetAnimal petAnimal = (this._currentPetHomePoint != null) ? this._currentPetHomePoint.User : null;
			string arg = ((petAnimal != null) ? petAnimal.Nickname : null) ?? "ペット";
			ConfirmScene.Sentence = string.Format("{0}を逃しますか？\n", arg) + "逃がすとアイテムとして戻ってきません。".Coloring("#DE4529FF").Size(24);
			ConfirmScene.OnClickedYes = delegate()
			{
				this._inventoryUI.Visible = true;
				this.PlaySystemSE(SoundPack.SystemSE.OK_L);
				this._currentPetHomePoint.RemoveUser();
				this._elementText.text = this._noneNameStr;
				this.SetInteractable(this._escapeButton, false);
				this.SetActive(this._nameChangeButton, false);
				if (this._nameChangeUI.IsActiveControl)
				{
					this._nameChangeUI.IsActiveControl = false;
				}
			};
			ConfirmScene.OnClickedNo = delegate()
			{
				this.PlaySystemSE(SoundPack.SystemSE.Cancel);
			};
			Singleton<Game>.Instance.LoadDialog();
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x0032E13C File Offset: 0x0032C53C
		private void ChangeChaseActor(bool active)
		{
			AIProject.SaveData.Environment.PetHomeInfo petHomeInfo = (this._currentPetHomePoint != null) ? this._currentPetHomePoint.SaveData : null;
			if (petHomeInfo != null)
			{
				petHomeInfo.ChaseActor = active;
			}
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x0032E170 File Offset: 0x0032C570
		private void DoNameChange()
		{
			if (this._nameChangeUI.IsActiveControl)
			{
				return;
			}
			this._nameChangeUI.NameInputField.text = this._elementText.text;
			this._nameChangeUI.IsActiveControl = true;
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x0032E1AC File Offset: 0x0032C5AC
		private void NameChanged(string petName)
		{
			this._elementText.text = petName;
			IPetAnimal petAnimal = (this._currentPetHomePoint != null) ? this._currentPetHomePoint.User : null;
			if (petAnimal != null)
			{
				petAnimal.Nickname = petName;
			}
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x0032E1EC File Offset: 0x0032C5EC
		private void SetAllFocusLevel(int level)
		{
			foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIBehaviorList)
			{
				menuUIBehaviour.SetFocusLevel(level);
			}
		}

		// Token: 0x060078C7 RID: 30919 RVA: 0x0032E220 File Offset: 0x0032C620
		private void SetAllEnableInput(bool active)
		{
			foreach (MenuUIBehaviour menuUIBehaviour in this.MenuUIBehaviorList)
			{
				menuUIBehaviour.EnabledInput = active;
			}
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x0032E253 File Offset: 0x0032C653
		private void SetBlockRaycast(bool active)
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			if (this._canvasGroup.blocksRaycasts != active)
			{
				this._canvasGroup.blocksRaycasts = active;
			}
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x0032E284 File Offset: 0x0032C684
		private void SetInteractable(bool active)
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			if (this._canvasGroup.interactable != active)
			{
				this._canvasGroup.interactable = active;
			}
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x0032E2B5 File Offset: 0x0032C6B5
		private void SetActive(GameObject obj, bool active)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x0032E2D8 File Offset: 0x0032C6D8
		private void SetActive(Component com, bool active)
		{
			if (com == null)
			{
				return;
			}
			GameObject gameObject = com.gameObject;
			if (gameObject == null)
			{
				return;
			}
			if (gameObject.activeSelf != active)
			{
				gameObject.SetActive(active);
			}
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x0032E319 File Offset: 0x0032C719
		private void SetInteractable(Selectable sel, bool active)
		{
			if (sel == null)
			{
				return;
			}
			if (sel.interactable != active)
			{
				sel.interactable = active;
			}
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x0032E33C File Offset: 0x0032C73C
		private void PlaySystemSE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x04006195 RID: 24981
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006196 RID: 24982
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006197 RID: 24983
		[SerializeField]
		private PetNameChangeUI _nameChangeUI;

		// Token: 0x04006198 RID: 24984
		[SerializeField]
		private InventoryFacadeViewer _inventoryUI;

		// Token: 0x04006199 RID: 24985
		[SerializeField]
		private Image _mainIconImage;

		// Token: 0x0400619A RID: 24986
		[SerializeField]
		private Text _mainTitleText;

		// Token: 0x0400619B RID: 24987
		[SerializeField]
		private Button _closeButton;

		// Token: 0x0400619C RID: 24988
		[SerializeField]
		private Toggle _elementToggle;

		// Token: 0x0400619D RID: 24989
		[SerializeField]
		private Text _elementText;

		// Token: 0x0400619E RID: 24990
		[SerializeField]
		private Button _nameChangeButton;

		// Token: 0x0400619F RID: 24991
		[SerializeField]
		private Toggle _chaseToggle;

		// Token: 0x040061A0 RID: 24992
		[SerializeField]
		private Button _escapeButton;

		// Token: 0x040061A1 RID: 24993
		[SerializeField]
		private Button _addButton;

		// Token: 0x040061A2 RID: 24994
		[SerializeField]
		private Image _selectImage;

		// Token: 0x040061A3 RID: 24995
		[SerializeField]
		private string _noneNameStr = string.Empty;

		// Token: 0x040061A4 RID: 24996
		private int _prevFocusLevel = -1;

		// Token: 0x040061A5 RID: 24997
		private float _prevTimeScale = 1f;

		// Token: 0x040061A6 RID: 24998
		private InventoryFacadeViewer.ItemFilter[] _emptyFilter = new InventoryFacadeViewer.ItemFilter[0];

		// Token: 0x040061A7 RID: 24999
		private MenuUIBehaviour[] _menuUIBehaviorList;

		// Token: 0x040061A8 RID: 25000
		private IObservable<TimeInterval<float>> _lerpStream;

		// Token: 0x040061A9 RID: 25001
		private PetHomePoint _currentPetHomePoint;

		// Token: 0x040061AA RID: 25002
		private IDisposable _fadeDisposable;

		// Token: 0x040061AB RID: 25003
		private Dictionary<int, InventoryFacadeViewer.ItemFilter[]> _itemFilterTable = new Dictionary<int, InventoryFacadeViewer.ItemFilter[]>();
	}
}
