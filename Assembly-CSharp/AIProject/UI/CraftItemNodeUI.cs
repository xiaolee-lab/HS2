using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using Illusion.Extensions;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000E63 RID: 3683
	public class CraftItemNodeUI : SerializedMonoBehaviour
	{
		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x00319C61 File Offset: 0x00318061
		public Button.ButtonClickedEvent OnClick
		{
			[CompilerGenerated]
			get
			{
				return this._button.onClick;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x060074C3 RID: 29891 RVA: 0x00319C6E File Offset: 0x0031806E
		public bool IsInteractable
		{
			[CompilerGenerated]
			get
			{
				return this._button.IsInteractable();
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x060074C4 RID: 29892 RVA: 0x00319C7B File Offset: 0x0031807B
		public UITrigger.TriggerEvent onEnter
		{
			get
			{
				return this.GetCache(ref this._onEnter, delegate
				{
					UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
					this.GetOrAddComponent<PointerEnterTrigger>().Triggers.Add(triggerEvent);
					return triggerEvent;
				});
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x060074C5 RID: 29893 RVA: 0x00319C95 File Offset: 0x00318095
		// (set) Token: 0x060074C6 RID: 29894 RVA: 0x00319C9D File Offset: 0x0031809D
		public RecipeDataInfo[] data { get; private set; }

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060074C7 RID: 29895 RVA: 0x00319CA6 File Offset: 0x003180A6
		public bool isUnknown
		{
			[CompilerGenerated]
			get
			{
				return this._pack.isUnknown;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x060074C8 RID: 29896 RVA: 0x00319CB3 File Offset: 0x003180B3
		public int CategoryID
		{
			[CompilerGenerated]
			get
			{
				return this._pack.info.CategoryID;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x060074C9 RID: 29897 RVA: 0x00319CC5 File Offset: 0x003180C5
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._pack.info.ID;
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x060074CA RID: 29898 RVA: 0x00319CD7 File Offset: 0x003180D7
		// (set) Token: 0x060074CB RID: 29899 RVA: 0x00319CE4 File Offset: 0x003180E4
		public bool Visible
		{
			get
			{
				return this.cachedgameObject.activeSelf;
			}
			set
			{
				this.cachedgameObject.SetActiveIfDifferent(value);
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x060074CC RID: 29900 RVA: 0x00319CF3 File Offset: 0x003180F3
		private GameObject cachedgameObject
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._cachedgameObject, new Func<GameObject>(base.get_gameObject));
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x060074CD RID: 29901 RVA: 0x00319D10 File Offset: 0x00318110
		private int ItemCount
		{
			[CompilerGenerated]
			get
			{
				int result;
				if (this._pack.isUnknown)
				{
					result = -1;
				}
				else
				{
					result = this._craftUI.checkStorages.SelectMany((IReadOnlyCollection<StuffItem> x) => x).FindItems(new StuffItem(this._pack.info.CategoryID, this._pack.info.ID, 0)).Sum((StuffItem p) => p.Count);
				}
				return result;
			}
		}

		// Token: 0x060074CE RID: 29902 RVA: 0x00319DAD File Offset: 0x003181AD
		public void Bind(CraftUI craftUI, CraftItemNodeUI.StuffItemInfoPack pack, RecipeDataInfo[] data)
		{
			this._craftUI = craftUI;
			this._pack = pack;
			this.data = data;
			this.Refresh();
		}

		// Token: 0x060074CF RID: 29903 RVA: 0x00319DCC File Offset: 0x003181CC
		public void Refresh()
		{
			bool flag = this._pack.possible == null;
			bool isUnknown = this._pack.isUnknown;
			bool isSuccess = this._pack.isSuccess;
			this._pack.Refresh();
			this._stackCount.Value = this.ItemCount;
			bool flag2 = isUnknown != this._pack.isUnknown;
			if (flag || flag2)
			{
				if (this._iconImage != null)
				{
					Manager.Resources.ItemIconTables.SetIcon(Manager.Resources.ItemIconTables.IconCategory.Item, this._pack.IconID, this._iconImage, true);
				}
				this._name.Value = this._pack.Label;
			}
			flag2 |= (isSuccess != this._pack.isSuccess);
			if (flag || flag2)
			{
				float a = (!this._pack.isUnknown && this._pack.isSuccess) ? 1f : 0.5f;
				foreach (Graphic graphic in from p in new Text[]
				{
					this._nameLabel,
					this._stackCountText
				}
				where p != null
				select p)
				{
					Color color = graphic.color;
					color.a = a;
					graphic.color = color;
				}
			}
		}

		// Token: 0x060074D0 RID: 29904 RVA: 0x00319F64 File Offset: 0x00318364
		protected virtual void Start()
		{
			if (this._nameLabel != null)
			{
				this._name.SubscribeToText(this._nameLabel);
			}
			if (this._stackCountText != null)
			{
				int itemSlotMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
				this._stackCount.SubscribeToText(this._stackCountText, delegate(int i)
				{
					if (i < 0)
					{
						return string.Empty;
					}
					return (i > itemSlotMax) ? string.Format("{0}+", itemSlotMax) : string.Format("{0}", i);
				});
			}
			EasingFunction easing;
			if (Tween.MotionFunctionTable.TryGetValue(this._motionType, out easing))
			{
			}
			if (Tween.MotionFunctionTable.TryGetValue(this._alphaMotionType, out easing))
			{
				ObservableEasing.Create(easing, this._easingDuration, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
				{
					this._canvasGroup.alpha = Mathf.Lerp(this._fromAlpha, this._toAlpha, x.Value);
				}).AddTo(this.disposables);
			}
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x0031A041 File Offset: 0x00318441
		private void OnDestroy()
		{
			this.disposables.Clear();
		}

		// Token: 0x04005F4D RID: 24397
		private UITrigger.TriggerEvent _onEnter;

		// Token: 0x04005F4E RID: 24398
		[SerializeField]
		private Image _iconImage;

		// Token: 0x04005F4F RID: 24399
		[SerializeField]
		private Text _nameLabel;

		// Token: 0x04005F50 RID: 24400
		[SerializeField]
		private Text _stackCountText;

		// Token: 0x04005F51 RID: 24401
		[SerializeField]
		private Button _button;

		// Token: 0x04005F52 RID: 24402
		[SerializeField]
		[Header("Animation")]
		private Transform _animationRoot;

		// Token: 0x04005F53 RID: 24403
		[SerializeField]
		protected CanvasGroup _canvasGroup;

		// Token: 0x04005F54 RID: 24404
		[SerializeField]
		private float _easingDuration;

		// Token: 0x04005F55 RID: 24405
		[SerializeField]
		private MotionType _motionType;

		// Token: 0x04005F56 RID: 24406
		[SerializeField]
		private Transform _from;

		// Token: 0x04005F57 RID: 24407
		[SerializeField]
		private Transform _to;

		// Token: 0x04005F58 RID: 24408
		[SerializeField]
		private MotionType _alphaMotionType;

		// Token: 0x04005F59 RID: 24409
		[SerializeField]
		private float _fromAlpha;

		// Token: 0x04005F5A RID: 24410
		[SerializeField]
		private float _toAlpha = 1f;

		// Token: 0x04005F5B RID: 24411
		[SerializeField]
		private Image _line;

		// Token: 0x04005F5D RID: 24413
		private CraftItemNodeUI.StuffItemInfoPack _pack;

		// Token: 0x04005F5E RID: 24414
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private StringReactiveProperty _name = new StringReactiveProperty();

		// Token: 0x04005F5F RID: 24415
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		protected IntReactiveProperty _stackCount = new IntReactiveProperty(0);

		// Token: 0x04005F60 RID: 24416
		private GameObject _cachedgameObject;

		// Token: 0x04005F61 RID: 24417
		private CompositeDisposable disposables = new CompositeDisposable();

		// Token: 0x04005F62 RID: 24418
		private CraftUI _craftUI;

		// Token: 0x02000E64 RID: 3684
		public class Possible
		{
			// Token: 0x060074D7 RID: 29911 RVA: 0x0031A0AE File Offset: 0x003184AE
			public Possible(bool unknown, bool success)
			{
				this.unknown = unknown;
				this.success = success;
			}

			// Token: 0x1700168E RID: 5774
			// (get) Token: 0x060074D8 RID: 29912 RVA: 0x0031A0C4 File Offset: 0x003184C4
			public bool unknown { get; }

			// Token: 0x1700168F RID: 5775
			// (get) Token: 0x060074D9 RID: 29913 RVA: 0x0031A0CC File Offset: 0x003184CC
			public bool success { get; }
		}

		// Token: 0x02000E65 RID: 3685
		public class StuffItemInfoPack
		{
			// Token: 0x060074DA RID: 29914 RVA: 0x0031A0D4 File Offset: 0x003184D4
			public StuffItemInfoPack(StuffItemInfo info, Func<CraftItemNodeUI.Possible> PossibleFunc)
			{
				this._PossibleFunc = PossibleFunc;
				this.info = info;
			}

			// Token: 0x060074DB RID: 29915 RVA: 0x0031A0F5 File Offset: 0x003184F5
			public void Refresh()
			{
				this.possible = this._PossibleFunc();
			}

			// Token: 0x17001690 RID: 5776
			// (get) Token: 0x060074DC RID: 29916 RVA: 0x0031A108 File Offset: 0x00318508
			// (set) Token: 0x060074DD RID: 29917 RVA: 0x0031A110 File Offset: 0x00318510
			public CraftItemNodeUI.Possible possible { get; private set; }

			// Token: 0x17001691 RID: 5777
			// (get) Token: 0x060074DE RID: 29918 RVA: 0x0031A119 File Offset: 0x00318519
			private Func<CraftItemNodeUI.Possible> _PossibleFunc { get; }

			// Token: 0x17001692 RID: 5778
			// (get) Token: 0x060074DF RID: 29919 RVA: 0x0031A121 File Offset: 0x00318521
			public StuffItemInfo info { get; }

			// Token: 0x17001693 RID: 5779
			// (get) Token: 0x060074E0 RID: 29920 RVA: 0x0031A12C File Offset: 0x0031852C
			public bool isUnknown
			{
				[CompilerGenerated]
				get
				{
					CraftItemNodeUI.Possible possible = this.possible;
					bool? flag = (possible != null) ? new bool?(possible.unknown) : null;
					return flag == null || flag.Value;
				}
			}

			// Token: 0x17001694 RID: 5780
			// (get) Token: 0x060074E1 RID: 29921 RVA: 0x0031A178 File Offset: 0x00318578
			public bool isSuccess
			{
				[CompilerGenerated]
				get
				{
					CraftItemNodeUI.Possible possible = this.possible;
					bool? flag = (possible != null) ? new bool?(possible.success) : null;
					return flag != null && flag.Value;
				}
			}

			// Token: 0x17001695 RID: 5781
			// (get) Token: 0x060074E2 RID: 29922 RVA: 0x0031A1C1 File Offset: 0x003185C1
			public string Label
			{
				[CompilerGenerated]
				get
				{
					return (!this.isUnknown) ? this.info.Name : this.unknownInfo.Label;
				}
			}

			// Token: 0x17001696 RID: 5782
			// (get) Token: 0x060074E3 RID: 29923 RVA: 0x0031A1E9 File Offset: 0x003185E9
			public int IconID
			{
				[CompilerGenerated]
				get
				{
					return (!this.isUnknown) ? this.info.IconID : this.unknownInfo.IconID;
				}
			}

			// Token: 0x17001697 RID: 5783
			// (get) Token: 0x060074E4 RID: 29924 RVA: 0x0031A211 File Offset: 0x00318611
			private CraftItemNodeUI.StuffItemInfoPack.UnknownInfo unknownInfo { get; } = new CraftItemNodeUI.StuffItemInfoPack.UnknownInfo();

			// Token: 0x02000E66 RID: 3686
			private class UnknownInfo
			{
				// Token: 0x17001698 RID: 5784
				// (get) Token: 0x060074E6 RID: 29926 RVA: 0x0031A221 File Offset: 0x00318621
				public int IconID
				{
					[CompilerGenerated]
					get
					{
						return 122;
					}
				}

				// Token: 0x17001699 RID: 5785
				// (get) Token: 0x060074E7 RID: 29927 RVA: 0x0031A225 File Offset: 0x00318625
				public string Label
				{
					[CompilerGenerated]
					get
					{
						return "？？？？";
					}
				}
			}
		}
	}
}
