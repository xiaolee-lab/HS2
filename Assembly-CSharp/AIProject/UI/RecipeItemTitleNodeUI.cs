using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.Definitions;
using AIProject.Scene;
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
	// Token: 0x02000E6F RID: 3695
	public class RecipeItemTitleNodeUI : SerializedMonoBehaviour
	{
		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06007582 RID: 30082 RVA: 0x0031D167 File Offset: 0x0031B567
		public Button.ButtonClickedEvent OnClick
		{
			[CompilerGenerated]
			get
			{
				return this._button.onClick;
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06007583 RID: 30083 RVA: 0x0031D174 File Offset: 0x0031B574
		public bool IsInteractable
		{
			[CompilerGenerated]
			get
			{
				return this._button.IsInteractable();
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06007584 RID: 30084 RVA: 0x0031D181 File Offset: 0x0031B581
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

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06007585 RID: 30085 RVA: 0x0031D19B File Offset: 0x0031B59B
		// (set) Token: 0x06007586 RID: 30086 RVA: 0x0031D1A3 File Offset: 0x0031B5A3
		public int MaxCount { get; private set; }

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06007587 RID: 30087 RVA: 0x0031D1AC File Offset: 0x0031B5AC
		public RecipeDataInfo data
		{
			[CompilerGenerated]
			get
			{
				return this._data;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06007588 RID: 30088 RVA: 0x0031D1B4 File Offset: 0x0031B5B4
		// (set) Token: 0x06007589 RID: 30089 RVA: 0x0031D1BC File Offset: 0x0031B5BC
		public IReadOnlyList<RecipeItemNodeUI> recipeItemNodeUIs { get; private set; }

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x0600758A RID: 30090 RVA: 0x0031D1C5 File Offset: 0x0031B5C5
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this._name.Value;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x0600758B RID: 30091 RVA: 0x0031D1D2 File Offset: 0x0031B5D2
		public bool isSuccess
		{
			[CompilerGenerated]
			get
			{
				return this._isSuccess.Value;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x0600758C RID: 30092 RVA: 0x0031D1DF File Offset: 0x0031B5DF
		// (set) Token: 0x0600758D RID: 30093 RVA: 0x0031D1EC File Offset: 0x0031B5EC
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

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x0600758E RID: 30094 RVA: 0x0031D1FB File Offset: 0x0031B5FB
		private GameObject cachedgameObject
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._cachedgameObject, new Func<GameObject>(base.get_gameObject));
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x0600758F RID: 30095 RVA: 0x0031D218 File Offset: 0x0031B618
		private static GameObject nodeBase
		{
			get
			{
				if (RecipeItemTitleNodeUI._nodeBase == null)
				{
					string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
					RecipeItemTitleNodeUI._nodeBase = CommonLib.LoadAsset<GameObject>(bundle, "RecipeItemNodeOption", false, string.Empty);
					if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
					{
						MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
					}
				}
				return RecipeItemTitleNodeUI._nodeBase;
			}
		}

		// Token: 0x06007590 RID: 30096 RVA: 0x0031D2AC File Offset: 0x0031B6AC
		public void Bind(CraftUI craftUI, int count, RecipeDataInfo data)
		{
			this._data = data;
			this._name.Value = "レシピ" + (count + 1);
			List<RecipeItemNodeUI> list = new List<RecipeItemNodeUI>();
			foreach (RecipeDataInfo.NeedData data2 in data.NeedList)
			{
				RecipeItemNodeUI recipeItemNodeUI = UnityEngine.Object.Instantiate<RecipeItemNodeUI>(RecipeItemTitleNodeUI.nodeBase.GetComponent<RecipeItemNodeUI>(), this._content, false);
				recipeItemNodeUI.Bind(craftUI, data2);
				list.Add(recipeItemNodeUI);
			}
			this.recipeItemNodeUIs = list;
			this.Refresh();
		}

		// Token: 0x06007591 RID: 30097 RVA: 0x0031D338 File Offset: 0x0031B738
		public void Refresh()
		{
			foreach (RecipeItemNodeUI recipeItemNodeUI in this.recipeItemNodeUIs)
			{
				recipeItemNodeUI.Refresh();
			}
			this.MaxCount = (from x in this.recipeItemNodeUIs
			select x.MaxCount into x
			orderby x
			select x).FirstOrDefault<int>();
			this._isSuccess.Value = (this.MaxCount > 0);
			float a = (!this._isSuccess.Value) ? 0.5f : 1f;
			foreach (RecipeItemNodeUI recipeItemNodeUI2 in this.recipeItemNodeUIs)
			{
				foreach (Graphic graphic in recipeItemNodeUI2.graphics)
				{
					Color color = graphic.color;
					color.a = a;
					graphic.color = color;
				}
			}
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x0031D4A4 File Offset: 0x0031B8A4
		protected virtual void Start()
		{
			if (this._nameLabel != null)
			{
				this._name.SubscribeToText(this._nameLabel);
			}
			if (this._successLabel != null)
			{
				this._isSuccess.SubscribeToInteractable(this._button);
				this._isSuccess.SubscribeToText(this._successLabel, (bool success) => (!success) ? string.Empty : "OK!");
				this._isSuccess.Subscribe(delegate(bool success)
				{
					this._successLabel.color = Define.Get((!success) ? Colors.White : Colors.Blue);
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

		// Token: 0x06007593 RID: 30099 RVA: 0x0031D59C File Offset: 0x0031B99C
		private void OnDestroy()
		{
			this.disposables.Clear();
		}

		// Token: 0x04005FC5 RID: 24517
		private UITrigger.TriggerEvent _onEnter;

		// Token: 0x04005FC6 RID: 24518
		[SerializeField]
		private Button _button;

		// Token: 0x04005FC7 RID: 24519
		[SerializeField]
		private Text _nameLabel;

		// Token: 0x04005FC8 RID: 24520
		[SerializeField]
		private Text _successLabel;

		// Token: 0x04005FCB RID: 24523
		[SerializeField]
		[Header("Animation")]
		private Transform _animationRoot;

		// Token: 0x04005FCC RID: 24524
		[SerializeField]
		protected CanvasGroup _canvasGroup;

		// Token: 0x04005FCD RID: 24525
		[SerializeField]
		private float _easingDuration;

		// Token: 0x04005FCE RID: 24526
		[SerializeField]
		private MotionType _motionType;

		// Token: 0x04005FCF RID: 24527
		[SerializeField]
		private Transform _from;

		// Token: 0x04005FD0 RID: 24528
		[SerializeField]
		private Transform _to;

		// Token: 0x04005FD1 RID: 24529
		[SerializeField]
		private MotionType _alphaMotionType;

		// Token: 0x04005FD2 RID: 24530
		[SerializeField]
		private float _fromAlpha;

		// Token: 0x04005FD3 RID: 24531
		[SerializeField]
		private float _toAlpha = 1f;

		// Token: 0x04005FD4 RID: 24532
		[SerializeField]
		private Image _line;

		// Token: 0x04005FD5 RID: 24533
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private StringReactiveProperty _name = new StringReactiveProperty();

		// Token: 0x04005FD6 RID: 24534
		private BoolReactiveProperty _isSuccess = new BoolReactiveProperty();

		// Token: 0x04005FD7 RID: 24535
		private GameObject _cachedgameObject;

		// Token: 0x04005FD8 RID: 24536
		private CompositeDisposable disposables = new CompositeDisposable();

		// Token: 0x04005FD9 RID: 24537
		private static GameObject _nodeBase;

		// Token: 0x04005FDA RID: 24538
		private RecipeDataInfo _data;

		// Token: 0x04005FDB RID: 24539
		[SerializeField]
		private RectTransform _content;
	}
}
