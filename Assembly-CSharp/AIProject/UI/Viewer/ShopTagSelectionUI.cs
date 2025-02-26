using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EB4 RID: 3764
	public class ShopTagSelectionUI : MonoBehaviour
	{
		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06007AFA RID: 31482 RVA: 0x0033AE47 File Offset: 0x00339247
		public int Index
		{
			[CompilerGenerated]
			get
			{
				return Array.FindIndex<ShopTagSelectionUI.Element>(this._elements, (ShopTagSelectionUI.Element x) => x.toggle.isOn);
			}
		}

		// Token: 0x17001835 RID: 6197
		public Toggle this[int index]
		{
			[CompilerGenerated]
			get
			{
				return this._elements[index].toggle;
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06007AFC RID: 31484 RVA: 0x0033AE80 File Offset: 0x00339280
		// (set) Token: 0x06007AFD RID: 31485 RVA: 0x0033AE88 File Offset: 0x00339288
		public Action<int> Selection { get; set; }

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06007AFE RID: 31486 RVA: 0x0033AE91 File Offset: 0x00339291
		private CompositeDisposable disposable { get; } = new CompositeDisposable();

		// Token: 0x06007AFF RID: 31487 RVA: 0x0033AE9C File Offset: 0x0033929C
		private IEnumerator Start()
		{
			while (this.Selection == null)
			{
				yield return null;
			}
			this._elements.BindToEnter(true).AddTo(this.disposable);
			(from x in this._elements
			select x.toggle).BindToGroup(this.Selection).AddTo(this.disposable);
			yield break;
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x0033AEB7 File Offset: 0x003392B7
		private void OnDestroy()
		{
			this.disposable.Clear();
			this.Selection = null;
		}

		// Token: 0x040062A3 RID: 25251
		[SerializeField]
		private ShopTagSelectionUI.Element[] _elements;

		// Token: 0x02000EB5 RID: 3765
		[Serializable]
		private class Element : TagSelection.ICursorTagElement
		{
			// Token: 0x17001838 RID: 6200
			// (get) Token: 0x06007B03 RID: 31491 RVA: 0x0033AEE0 File Offset: 0x003392E0
			public Image cursor
			{
				[CompilerGenerated]
				get
				{
					return this._cursor;
				}
			}

			// Token: 0x17001839 RID: 6201
			// (get) Token: 0x06007B04 RID: 31492 RVA: 0x0033AEE8 File Offset: 0x003392E8
			public Selectable selectable
			{
				[CompilerGenerated]
				get
				{
					return this._toggle;
				}
			}

			// Token: 0x1700183A RID: 6202
			// (get) Token: 0x06007B05 RID: 31493 RVA: 0x0033AEF0 File Offset: 0x003392F0
			public Toggle toggle
			{
				[CompilerGenerated]
				get
				{
					return this._toggle;
				}
			}

			// Token: 0x040062A6 RID: 25254
			[SerializeField]
			private Image _cursor;

			// Token: 0x040062A7 RID: 25255
			[SerializeField]
			private Toggle _toggle;
		}
	}
}
