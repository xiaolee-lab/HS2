using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using AIProject.ColorDefine;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Viewer;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E60 RID: 3680
	public class CraftItemInfoUI : ItemInfoUI
	{
		// Token: 0x0600747B RID: 29819 RVA: 0x003189A8 File Offset: 0x00316DA8
		public static IEnumerator Load(CraftUI craftUI, Transform viewerParent, Action<CraftItemInfoUI> onComplete)
		{
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			GameObject asset = CommonLib.LoadAsset<GameObject>(bundle, "CraftItemInfoUI", false, string.Empty);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
			}
			CraftItemInfoUI infoUI = UnityEngine.Object.Instantiate<GameObject>(asset, viewerParent, false).GetComponent<CraftItemInfoUI>();
			infoUI._craftUI = craftUI;
			onComplete.Call(infoUI);
			yield break;
		}

		// Token: 0x17001671 RID: 5745
		// (set) Token: 0x0600747C RID: 29820 RVA: 0x003189D1 File Offset: 0x00316DD1
		public int createSum
		{
			set
			{
				this._createSum.SetValueAndForceNotify(value);
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x0600747D RID: 29821 RVA: 0x003189DF File Offset: 0x00316DDF
		// (set) Token: 0x0600747E RID: 29822 RVA: 0x003189E7 File Offset: 0x00316DE7
		private string itemName { get; set; } = string.Empty;

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x0600747F RID: 29823 RVA: 0x003189F0 File Offset: 0x00316DF0
		private IntReactiveProperty _createSum { get; } = new IntReactiveProperty(0);

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06007480 RID: 29824 RVA: 0x003189F8 File Offset: 0x00316DF8
		private BoolReactiveProperty _active { get; } = new BoolReactiveProperty(false);

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06007481 RID: 29825 RVA: 0x00318A00 File Offset: 0x00316E00
		// (set) Token: 0x06007482 RID: 29826 RVA: 0x00318A08 File Offset: 0x00316E08
		public IReadOnlyCollection<StuffItem> targetStorage { get; private set; }

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06007483 RID: 29827 RVA: 0x00318A11 File Offset: 0x00316E11
		private IReadOnlyCollection<IReadOnlyCollection<StuffItem>> checkStorages
		{
			[CompilerGenerated]
			get
			{
				return this._craftUI.checkStorages;
			}
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x00318A1E File Offset: 0x00316E1E
		public override void Refresh(int count)
		{
			base.Refresh(count);
			this._active.Value = (count > 0);
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x00318A38 File Offset: 0x00316E38
		public override void Refresh(StuffItem item)
		{
			this._item = item;
			StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			this.itemName = item2.Name;
			this._createSum.SetValueAndForceNotify(0);
			this._flavorText.text = item2.Explanation;
			this.Refresh(item.Count);
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x00318A9D File Offset: 0x00316E9D
		public override void Close()
		{
			this._item = null;
			this.Refresh(0);
			base.Close();
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x00318AB4 File Offset: 0x00316EB4
		protected override void Start()
		{
			base.Start();
			if (this._infoLayout != null)
			{
				this._infoLayout.SetActive(true);
			}
			this._active.Value = base.isOpen;
			this._active.Subscribe(delegate(bool active)
			{
				this.isCountViewerVisible = active;
			});
			this._active.Subscribe(delegate(bool active)
			{
				this._submitButton.gameObject.SetActive(active);
			});
			this._createSum.SubscribeToText(this._itemName, delegate(int i)
			{
				string arg = string.Empty;
				if (i > 0)
				{
					arg = string.Format(" x {0}", i);
				}
				string itemName = this.itemName;
				if (itemName.IsNullOrEmpty())
				{
					arg = string.Empty;
				}
				return string.Format("{0}{1}", itemName, arg);
			});
			this._storageDisposable = Observable.FromCoroutine((CancellationToken _) => this.LoadStorageType(), false).Subscribe<Unit>();
			Text text = this._submitButton.GetComponentInChildren<Text>();
			Color baseTextColor = text.color;
			this._submitButton.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				text.color = Define.Get(Colors.Orange);
			});
			this._submitButton.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
			{
				text.color = baseTextColor;
			});
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x00318BC9 File Offset: 0x00316FC9
		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this._storageDisposable != null)
			{
				this._storageDisposable.Dispose();
			}
			this._storageDisposable = null;
			this._active.Dispose();
		}

		// Token: 0x06007489 RID: 29833 RVA: 0x00318BFC File Offset: 0x00316FFC
		private IEnumerator LoadStorageType()
		{
			yield return this.LoadWarningViewer();
			while (!Singleton<Map>.IsInstance() || Singleton<Map>.Instance.Player == null || Singleton<Map>.Instance.Player.PlayerData == null)
			{
				yield return null;
			}
			while (!Singleton<Game>.IsInstance() || Singleton<Game>.Instance.Environment == null)
			{
				yield return null;
			}
			while (!Singleton<Manager.Resources>.IsInstance() || Singleton<Manager.Resources>.Instance.DefinePack == null || Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines == null)
			{
				yield return null;
			}
			this._warningViewer.msgID = 1;
			(from _ in this._countViewer.Counter
			where this._item != null
			select _).Subscribe(delegate(int count)
			{
				this.EmptySlotCheck(count);
			});
			yield break;
		}

		// Token: 0x0600748A RID: 29834 RVA: 0x00318C18 File Offset: 0x00317018
		private IEnumerator LoadWarningViewer()
		{
			if (this._warningViewer != null)
			{
				yield break;
			}
			yield return WarningViewer.Load(this._warningViewerLayout, delegate(WarningViewer warningViewer)
			{
				this._warningViewer = warningViewer;
			});
			yield break;
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x00318C34 File Offset: 0x00317034
		private void EmptySlotCheck(int count)
		{
			count *= this._createSum.Value;
			this.targetStorage = null;
			bool flag = false;
			if (count <= 0)
			{
				flag = true;
			}
			else
			{
				foreach (IReadOnlyCollection<StuffItem> readOnlyCollection in this.checkStorages)
				{
					int capacity;
					if (readOnlyCollection == Singleton<Map>.Instance.Player.PlayerData.ItemList)
					{
						capacity = Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
					}
					else if (readOnlyCollection == Singleton<Game>.Instance.Environment.ItemListInStorage)
					{
						capacity = Singleton<Manager.Resources>.Instance.DefinePack.ItemBoxCapacityDefines.StorageCapacity;
					}
					else
					{
						if (readOnlyCollection == Singleton<Game>.Instance.Environment.ItemListInPantry)
						{
							continue;
						}
						continue;
					}
					if (this.IsStrageIn(capacity, count, readOnlyCollection))
					{
						this.targetStorage = readOnlyCollection;
						flag = true;
						break;
					}
				}
			}
			this._warningViewer.visible = !flag;
			this._submitButton.interactable = flag;
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x00318D68 File Offset: 0x00317168
		private bool IsStrageIn(int capacity, int count, IReadOnlyCollection<StuffItem> storage)
		{
			int itemSlotMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			foreach (StuffItem stuffItem in storage.FindItems(this._item))
			{
				int num = itemSlotMax - stuffItem.Count;
				int a = count - num;
				count = Mathf.Max(a, 0);
				if (count <= 0)
				{
					break;
				}
			}
			int num2 = count / itemSlotMax;
			num2 += ((count % itemSlotMax <= 0) ? 0 : 1);
			bool flag = capacity - storage.Count - num2 < 0;
			return !flag;
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x00318E04 File Offset: 0x00317204
		private new void SetFocusLevel(int level)
		{
			Singleton<Manager.Input>.Instance.FocusLevel = level;
		}

		// Token: 0x04005F2B RID: 24363
		[SerializeField]
		private CraftUI _craftUI;

		// Token: 0x04005F2C RID: 24364
		[SerializeField]
		private RectTransform _warningViewerLayout;

		// Token: 0x04005F2D RID: 24365
		[SerializeField]
		private WarningViewer _warningViewer;

		// Token: 0x04005F2F RID: 24367
		private IDisposable _storageDisposable;

		// Token: 0x04005F31 RID: 24369
		private StuffItem _item;
	}
}
