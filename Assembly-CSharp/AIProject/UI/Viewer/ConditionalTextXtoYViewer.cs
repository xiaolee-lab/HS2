using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EBF RID: 3775
	[Serializable]
	public class ConditionalTextXtoYViewer
	{
		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06007B9B RID: 31643 RVA: 0x00343DEA File Offset: 0x003421EA
		public bool isOver
		{
			[CompilerGenerated]
			get
			{
				return this._x.Value >= this._y.Value;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06007B9C RID: 31644 RVA: 0x00343E07 File Offset: 0x00342207
		public bool isUnder
		{
			[CompilerGenerated]
			get
			{
				return this._x.Value <= this._y.Value;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06007B9D RID: 31645 RVA: 0x00343E24 File Offset: 0x00342224
		public bool isGreater
		{
			[CompilerGenerated]
			get
			{
				return this._x.Value > this._y.Value;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06007B9E RID: 31646 RVA: 0x00343E3E File Offset: 0x0034223E
		public bool isLesser
		{
			[CompilerGenerated]
			get
			{
				return this._x.Value < this._y.Value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06007B9F RID: 31647 RVA: 0x00343E58 File Offset: 0x00342258
		public IObservable<int> X
		{
			[CompilerGenerated]
			get
			{
				return this._x;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06007BA0 RID: 31648 RVA: 0x00343E60 File Offset: 0x00342260
		public IObservable<int> Y
		{
			[CompilerGenerated]
			get
			{
				return this._y;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06007BA1 RID: 31649 RVA: 0x00343E68 File Offset: 0x00342268
		// (set) Token: 0x06007BA2 RID: 31650 RVA: 0x00343E75 File Offset: 0x00342275
		public int x
		{
			get
			{
				return this._x.Value;
			}
			set
			{
				this._x.Value = value;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06007BA3 RID: 31651 RVA: 0x00343E83 File Offset: 0x00342283
		// (set) Token: 0x06007BA4 RID: 31652 RVA: 0x00343E90 File Offset: 0x00342290
		public int y
		{
			get
			{
				return this._y.Value;
			}
			set
			{
				this._y.Value = value;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06007BA5 RID: 31653 RVA: 0x00343E9E File Offset: 0x0034229E
		// (set) Token: 0x06007BA6 RID: 31654 RVA: 0x00343EAB File Offset: 0x003422AB
		public bool visible
		{
			get
			{
				return this._layout.activeSelf;
			}
			set
			{
				this._layout.SetActive(value);
			}
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x00343EB9 File Offset: 0x003422B9
		public void VisibleUpdate()
		{
			if (this._layout != null && this.visible)
			{
				this.visible = false;
				this.visible = true;
			}
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x00343EE5 File Offset: 0x003422E5
		public void Refresh()
		{
			this._x.SetValueAndForceNotify(this._x.Value);
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06007BA9 RID: 31657 RVA: 0x00343EFD File Offset: 0x003422FD
		public GameObject layout
		{
			[CompilerGenerated]
			get
			{
				return this._layout;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x06007BAA RID: 31658 RVA: 0x00343F05 File Offset: 0x00342305
		public Text xText
		{
			[CompilerGenerated]
			get
			{
				return this._xText;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x00343F0D File Offset: 0x0034230D
		public Text yText
		{
			[CompilerGenerated]
			get
			{
				return this._yText;
			}
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06007BAC RID: 31660 RVA: 0x00343F15 File Offset: 0x00342315
		// (set) Token: 0x06007BAD RID: 31661 RVA: 0x00343F1D File Offset: 0x0034231D
		public bool initialized { get; private set; }

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06007BAE RID: 31662 RVA: 0x00343F26 File Offset: 0x00342326
		private IntReactiveProperty _x { get; } = new IntReactiveProperty();

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x00343F2E File Offset: 0x0034232E
		private IntReactiveProperty _y { get; } = new IntReactiveProperty();

		// Token: 0x06007BB0 RID: 31664 RVA: 0x00343F38 File Offset: 0x00342338
		public void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			if (this._yText != null)
			{
				this._y.SubscribeToText(this._yText).AddTo(this._yText);
				this._y.Subscribe(delegate(int _)
				{
					this.Refresh();
				}).AddTo(this._yText);
			}
			if (this._xText != null)
			{
				this._x.SubscribeToText(this._xText).AddTo(this._xText);
			}
			this.VisibleUpdate();
			this.initialized = true;
		}

		// Token: 0x04006354 RID: 25428
		[SerializeField]
		private GameObject _layout;

		// Token: 0x04006355 RID: 25429
		[SerializeField]
		private Text _xText;

		// Token: 0x04006356 RID: 25430
		[SerializeField]
		private Text _yText;
	}
}
