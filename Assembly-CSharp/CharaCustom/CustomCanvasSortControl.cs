using System;
using System.Linq;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009C8 RID: 2504
	public class CustomCanvasSortControl : MonoBehaviour
	{
		// Token: 0x0600493E RID: 18750 RVA: 0x001BD794 File Offset: 0x001BBB94
		private void Start()
		{
			if (this.sortCanvas != null && this.sortCanvas.Length != 0)
			{
				for (int i = 0; i < this.sortCanvas.Length; i++)
				{
					this.sortCanvas[i].sortingOrder = i + 1;
				}
			}
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x001BD7E4 File Offset: 0x001BBBE4
		public void SortCanvas(Canvas cvs)
		{
			if (1 >= this.sortCanvas.Length)
			{
				return;
			}
			if (cvs == this.sortCanvas.Last<Canvas>())
			{
				return;
			}
			Canvas[] array = new Canvas[this.sortCanvas.Length];
			int num = 0;
			for (int i = 0; i < this.sortCanvas.Length; i++)
			{
				if (!(cvs == this.sortCanvas[i]))
				{
					array[num++] = this.sortCanvas[i];
				}
			}
			array[array.Length - 1] = cvs;
			this.sortCanvas = array;
			if (this.sortCanvas != null && this.sortCanvas.Length != 0)
			{
				for (int j = 0; j < this.sortCanvas.Length; j++)
				{
					this.sortCanvas[j].sortingOrder = j + 1;
				}
			}
		}

		// Token: 0x040043D9 RID: 17369
		[SerializeField]
		private Canvas[] sortCanvas;
	}
}
