using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E9E RID: 3742
	public class PresentItemListUI : MonoBehaviour
	{
		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x060078FB RID: 30971 RVA: 0x0032F8B9 File Offset: 0x0032DCB9
		// (set) Token: 0x060078FC RID: 30972 RVA: 0x0032F8C1 File Offset: 0x0032DCC1
		public AgentActor Target { get; set; }

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x060078FD RID: 30973 RVA: 0x0032F8CA File Offset: 0x0032DCCA
		// (set) Token: 0x060078FE RID: 30974 RVA: 0x0032F8D4 File Offset: 0x0032DCD4
		public bool IsActiveControl
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this._isActive == value)
				{
					return;
				}
				this._isActive = value;
				IEnumerator coroutine;
				if (value)
				{
					Time.timeScale = 0f;
					Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
					Singleton<Map>.Instance.Player.ReleaseInteraction();
					coroutine = this.DoOpen();
				}
				else
				{
					Singleton<Manager.Input>.Instance.ClearMenuElements();
					Singleton<Manager.Input>.Instance.FocusLevel = -1;
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
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x060078FF RID: 30975 RVA: 0x0032F9CC File Offset: 0x0032DDCC
		private MenuUIBehaviour[] MenuElements
		{
			[CompilerGenerated]
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuElements) == null)
				{
					result = (this._menuElements = new MenuUIBehaviour[]
					{
						this._itemList
					});
				}
				return result;
			}
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x0032FA00 File Offset: 0x0032DE00
		public IObservable<Unit> OnClosedAsObservable()
		{
			Subject<Unit> result;
			if ((result = this._close) == null)
			{
				result = (this._close = new Subject<Unit>());
			}
			return result;
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x0032FA28 File Offset: 0x0032DE28
		private void Start()
		{
			PointerClickTrigger pointerClickTrigger = this._canvasGroup.gameObject.AddComponent<PointerClickTrigger>();
			UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
			triggerEvent.AddListener(delegate(BaseEventData bed)
			{
				PointerEventData pointerEventData = bed as PointerEventData;
				if (pointerEventData.button != PointerEventData.InputButton.Right)
				{
					return;
				}
				this.Close();
			});
			pointerClickTrigger.Triggers.Add(triggerEvent);
			this._closeButton.onClick.AddListener(delegate()
			{
				this.Close();
			});
			this._itemList.OnSubmit.AddListener(delegate()
			{
				this.Select(this._itemList.SelectedOption);
			});
			this._itemList.OnCancel.AddListener(delegate()
			{
				this.Close();
			});
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x0032FABE File Offset: 0x0032DEBE
		private void Select(ItemNodeUI option)
		{
			if (option == null)
			{
				return;
			}
			if (!option.IsInteractable)
			{
				return;
			}
			Button.ButtonClickedEvent onClick = option.OnClick;
			if (onClick != null)
			{
				onClick.Invoke();
			}
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x0032FAED File Offset: 0x0032DEED
		private void Close()
		{
			Time.timeScale = 1f;
			this.IsActiveControl = false;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x0032FB08 File Offset: 0x0032DF08
		private IEnumerator DoOpen()
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			if (!this._itemList.isOptionNode)
			{
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, "ItemListOption", false, string.Empty);
				if (gameObject == null)
				{
					yield break;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, string.Empty));
				}
				this._itemList.SetOptionNode(gameObject);
			}
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			List<StuffItem> itemList = Singleton<Map>.Instance.Player.PlayerData.ItemList;
			int i;
			for (i = 0; i < itemList.Count; i++)
			{
				StuffItem item = itemList[i];
				ItemNodeUI itemNodeUI = this._itemList.AddItemNode(i, item);
				itemNodeUI.OnClick.AddListener(delegate()
				{
					this._selectedIndexOf = i;
					this._categoryID = item.CategoryID;
					this._itemID = item.ID;
					this._countSetting.Item = item;
					this._countSetting.Count = 1;
					ConfirmScene.OnClickedYes = delegate()
					{
						this.InvokePresent(1);
						this.Close();
					};
					ConfirmScene.OnClickedNo = delegate()
					{
					};
					Singleton<Game>.Instance.LoadDialog();
				});
			}
			this._itemList.SetupDefault();
			CanvasGroup canvasGroup = this._itemList.CursorFrame.GetComponent<CanvasGroup>();
			if (canvasGroup != null)
			{
				ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					canvasGroup.alpha = x.Value;
				});
			}
			IObservable<TimeInterval<float>> lerpFadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeInStream.Publish<TimeInterval<float>>();
			lerpConnStream.Connect();
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}, delegate(Exception ex)
			{
			});
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			if (this._countSetting != null)
			{
				this._countSetting.OnSubmit.RemoveAllListeners();
				this._countSetting.OnSubmit.AddListener(delegate()
				{
					this.InvokePresent(this._countSetting.Count);
					this.IsActiveControl = false;
					this._countSetting.Close();
				});
				this._countSetting.OnCancel.RemoveAllListeners();
				this._countSetting.OnCancel.AddListener(delegate()
				{
					this._countSetting.Close();
				});
			}
			this._canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.SetupState();
			Singleton<Manager.Input>.Instance.MenuElements = this.MenuElements;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			yield break;
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x0032FB24 File Offset: 0x0032DF24
		private IEnumerator DoClose()
		{
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> lerpFadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeOutStream.SubscribeOn(Scheduler.MainThreadIgnoreTimeScale).Publish<TimeInterval<float>>();
			lerpConnStream.Connect();
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			this._itemList.ClearItems();
			Singleton<Map>.Instance.Player.SetScheduledInteractionState(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			if (this._close != null)
			{
				this._close.OnNext(Unit.Default);
			}
			Singleton<Map>.Instance.Player.ReleaseInteraction();
			Singleton<Manager.Input>.Instance.SetupState();
			yield break;
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x0032FB40 File Offset: 0x0032DF40
		private void InvokePresent(int count)
		{
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(this._categoryID, this._itemID);
			PlayerActor player = Singleton<Map>.Instance.Player;
			for (int i = 0; i < count; i++)
			{
			}
			StuffItem item2 = new StuffItem(item.CategoryID, item.ID, count);
			this.Target.AgentData.ItemList.AddItem(item2);
			player.PlayerData.ItemList.RemoveItem(item2);
		}

		// Token: 0x040061B8 RID: 25016
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040061B9 RID: 25017
		[SerializeField]
		private ItemListUI _itemList;

		// Token: 0x040061BB RID: 25019
		[SerializeField]
		private Button _closeButton;

		// Token: 0x040061BC RID: 25020
		[Header("Dialog")]
		[SerializeField]
		private CountSettingUI _countSetting;

		// Token: 0x040061BD RID: 25021
		private int _selectedIndexOf;

		// Token: 0x040061BE RID: 25022
		private int _categoryID;

		// Token: 0x040061BF RID: 25023
		private int _itemID;

		// Token: 0x040061C0 RID: 25024
		private bool _isActive;

		// Token: 0x040061C1 RID: 25025
		private IDisposable _fadeDisposable;

		// Token: 0x040061C2 RID: 25026
		private MenuUIBehaviour[] _menuElements;

		// Token: 0x040061C3 RID: 25027
		private Subject<Unit> _close;

		// Token: 0x040061C4 RID: 25028
		private IDisposable _labelSubscriber;

		// Token: 0x040061C5 RID: 25029
		private bool _entered;
	}
}
