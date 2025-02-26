using System;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012C4 RID: 4804
	public class AddButtonCtrl : MonoBehaviour
	{
		// Token: 0x0600A05D RID: 41053 RVA: 0x0041EDD8 File Offset: 0x0041D1D8
		public void OnClick(int _kind)
		{
			this.select = ((this.select != _kind) ? _kind : -1);
			for (int i = 0; i < this.commonInfo.Length; i++)
			{
				this.commonInfo[i].active = (i == this.select);
			}
			SortCanvas.select = this.canvas;
		}

		// Token: 0x0600A05E RID: 41054 RVA: 0x0041EE38 File Offset: 0x0041D238
		private void Start()
		{
			this.select = -1;
		}

		// Token: 0x04007EB0 RID: 32432
		[SerializeField]
		private AddButtonCtrl.CommonInfo[] commonInfo;

		// Token: 0x04007EB1 RID: 32433
		[SerializeField]
		private Canvas canvas;

		// Token: 0x04007EB2 RID: 32434
		private int select = -1;

		// Token: 0x020012C5 RID: 4805
		[Serializable]
		private class CommonInfo
		{
			// Token: 0x170021E9 RID: 8681
			// (set) Token: 0x0600A060 RID: 41056 RVA: 0x0041EE4C File Offset: 0x0041D24C
			public bool active
			{
				set
				{
					if (this.obj.activeSelf != value)
					{
						this.obj.SetActive(value);
						this.button.image.color = ((!value) ? Color.white : Color.green);
					}
				}
			}

			// Token: 0x04007EB3 RID: 32435
			public GameObject obj;

			// Token: 0x04007EB4 RID: 32436
			public Button button;
		}
	}
}
