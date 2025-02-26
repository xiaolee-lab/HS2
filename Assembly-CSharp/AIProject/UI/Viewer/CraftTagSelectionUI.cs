using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E67 RID: 3687
	public class CraftTagSelectionUI : MonoBehaviour
	{
		// Token: 0x1700169A RID: 5786
		public Toggle this[int index]
		{
			[CompilerGenerated]
			get
			{
				return this.elements[index].toggle;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x060074EA RID: 29930 RVA: 0x0031A2A6 File Offset: 0x003186A6
		// (set) Token: 0x060074EB RID: 29931 RVA: 0x0031A2AE File Offset: 0x003186AE
		public Action<int> Selection { get; set; }

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x060074EC RID: 29932 RVA: 0x0031A2B7 File Offset: 0x003186B7
		public CraftGroupUI[] group
		{
			[CompilerGenerated]
			get
			{
				return this._group;
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x060074ED RID: 29933 RVA: 0x0031A2BF File Offset: 0x003186BF
		private CompositeDisposable disposable { get; } = new CompositeDisposable();

		// Token: 0x060074EE RID: 29934 RVA: 0x0031A2C8 File Offset: 0x003186C8
		public IEnumerator<Toggle> GetEnumerator()
		{
			foreach (CraftTagSelectionUI.Element item in this.elements)
			{
				yield return item.toggle;
			}
			yield break;
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x0031A2E3 File Offset: 0x003186E3
		private CraftTagSelectionUI.Element[] elements
		{
			[CompilerGenerated]
			get
			{
				return this.GetCache(ref this._elements, () => (from x in this._group
				select new CraftTagSelectionUI.Element(x)).ToArray<CraftTagSelectionUI.Element>());
			}
		}

		// Token: 0x060074F0 RID: 29936 RVA: 0x0031A300 File Offset: 0x00318700
		private IEnumerator Start()
		{
			while (this.Selection == null)
			{
				yield return null;
			}
			this.elements.BindToEnter(true).AddTo(this.disposable);
			(from x in this.elements
			select x.toggle).BindToGroup(this.Selection).AddTo(this.disposable);
			yield break;
		}

		// Token: 0x060074F1 RID: 29937 RVA: 0x0031A31B File Offset: 0x0031871B
		private void OnDestroy()
		{
			this.disposable.Clear();
			this.Selection = null;
		}

		// Token: 0x04005F6D RID: 24429
		[SerializeField]
		private CraftGroupUI[] _group;

		// Token: 0x04005F6F RID: 24431
		private CraftTagSelectionUI.Element[] _elements;

		// Token: 0x02000E68 RID: 3688
		private class Element : TagSelection.ICursorTagElement
		{
			// Token: 0x060074F4 RID: 29940 RVA: 0x0031A366 File Offset: 0x00318766
			public Element(CraftGroupUI ui)
			{
				this.cursor = ui.cursor;
				this.selectable = ui.toggle;
				this.toggle = ui.toggle;
			}

			// Token: 0x1700169F RID: 5791
			// (get) Token: 0x060074F5 RID: 29941 RVA: 0x0031A392 File Offset: 0x00318792
			public Image cursor { get; }

			// Token: 0x170016A0 RID: 5792
			// (get) Token: 0x060074F6 RID: 29942 RVA: 0x0031A39A File Offset: 0x0031879A
			public Selectable selectable { get; }

			// Token: 0x170016A1 RID: 5793
			// (get) Token: 0x060074F7 RID: 29943 RVA: 0x0031A3A2 File Offset: 0x003187A2
			public Toggle toggle { get; }
		}
	}
}
