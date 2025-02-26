using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Illusion.Extensions;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E6E RID: 3694
	public class RecipeItemNodeUI : SerializedMonoBehaviour
	{
		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06007570 RID: 30064 RVA: 0x0031CE80 File Offset: 0x0031B280
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this._name.Value;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06007571 RID: 30065 RVA: 0x0031CE8D File Offset: 0x0031B28D
		// (set) Token: 0x06007572 RID: 30066 RVA: 0x0031CE9A File Offset: 0x0031B29A
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

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06007573 RID: 30067 RVA: 0x0031CEA9 File Offset: 0x0031B2A9
		private GameObject cachedgameObject
		{
			[CompilerGenerated]
			get
			{
				return this.GetCacheObject(ref this._cachedgameObject, new Func<GameObject>(base.get_gameObject));
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06007574 RID: 30068 RVA: 0x0031CEC3 File Offset: 0x0031B2C3
		public Graphic[] graphics
		{
			get
			{
				return this.GetCache(ref this._graphics, delegate
				{
					List<Graphic> list = new List<Graphic>();
					if (this._nameLabel != null)
					{
						list.Add(this._nameLabel);
					}
					if (this._slotCounter.layout != null)
					{
						list.AddRange(this._slotCounter.layout.GetComponentsInChildren<Graphic>(true));
					}
					return list.ToArray();
				});
			}
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06007575 RID: 30069 RVA: 0x0031CEDD File Offset: 0x0031B2DD
		// (set) Token: 0x06007576 RID: 30070 RVA: 0x0031CEE5 File Offset: 0x0031B2E5
		public int MaxCount { get; private set; }

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06007577 RID: 30071 RVA: 0x0031CEF0 File Offset: 0x0031B2F0
		private int ItemCount
		{
			[CompilerGenerated]
			get
			{
				return this._craftUI.checkStorages.SelectMany((IReadOnlyCollection<StuffItem> x) => x).FindItems(new StuffItem(this._data.CategoryID, this._data.ID, 0)).Sum((StuffItem p) => p.Count);
			}
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x0031CF6D File Offset: 0x0031B36D
		public void Bind(CraftUI craftUI, RecipeDataInfo.NeedData data)
		{
			this._craftUI = craftUI;
			this._data = data;
			this._name.Value = data.Name;
			this._slotCounter.y = data.Sum;
			this.Refresh();
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x0031CFA5 File Offset: 0x0031B3A5
		public void Refresh()
		{
			this._slotCounter.x = this.ItemCount;
			this.MaxCount = this._slotCounter.x / this._slotCounter.y;
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x0031CFD8 File Offset: 0x0031B3D8
		protected virtual void Start()
		{
			if (this._nameLabel != null)
			{
				this._name.SubscribeToText(this._nameLabel);
			}
			this._slotCounter.Initialize();
			if (this._slotCounter.xText != null)
			{
				IObservable<Colors> source = from i in this._slotCounter.X
				select (i >= this._slotCounter.y) ? Colors.White : Colors.Red;
				if (RecipeItemNodeUI.<>f__mg$cache0 == null)
				{
					RecipeItemNodeUI.<>f__mg$cache0 = new Func<Colors, Color>(Define.Get);
				}
				source.Select(RecipeItemNodeUI.<>f__mg$cache0).Subscribe(delegate(Color color)
				{
					this._slotCounter.xText.color = color;
				}).AddTo(this._slotCounter.xText);
			}
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x0031D089 File Offset: 0x0031B489
		private void OnDestroy()
		{
			this.disposables.Clear();
		}

		// Token: 0x04005FB6 RID: 24502
		[SerializeField]
		private Text _nameLabel;

		// Token: 0x04005FB7 RID: 24503
		[SerializeField]
		private ConditionalTextXtoYViewer _slotCounter;

		// Token: 0x04005FB8 RID: 24504
		[SerializeField]
		[Header("Animation")]
		private Transform _animationRoot;

		// Token: 0x04005FB9 RID: 24505
		[SerializeField]
		protected CanvasGroup _canvasGroup;

		// Token: 0x04005FBA RID: 24506
		[SerializeField]
		private Image _line;

		// Token: 0x04005FBB RID: 24507
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private StringReactiveProperty _name = new StringReactiveProperty();

		// Token: 0x04005FBC RID: 24508
		private GameObject _cachedgameObject;

		// Token: 0x04005FBD RID: 24509
		private CompositeDisposable disposables = new CompositeDisposable();

		// Token: 0x04005FBE RID: 24510
		private RecipeDataInfo.NeedData _data;

		// Token: 0x04005FBF RID: 24511
		private Graphic[] _graphics;

		// Token: 0x04005FC1 RID: 24513
		private CraftUI _craftUI;

		// Token: 0x04005FC4 RID: 24516
		[CompilerGenerated]
		private static Func<Colors, Color> <>f__mg$cache0;
	}
}
