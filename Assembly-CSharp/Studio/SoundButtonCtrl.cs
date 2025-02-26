using System;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001343 RID: 4931
	public class SoundButtonCtrl : MonoBehaviour
	{
		// Token: 0x0600A511 RID: 42257 RVA: 0x00434DA8 File Offset: 0x004331A8
		public void OnClickButton(int _idx)
		{
			this.select = ((this.select != _idx) ? _idx : -1);
			for (int i = 0; i < this.ciRoot.Length; i++)
			{
				this.ciRoot[i].active = (i == this.select);
			}
		}

		// Token: 0x0600A512 RID: 42258 RVA: 0x00434DFD File Offset: 0x004331FD
		private void Start()
		{
			this.select = -1;
		}

		// Token: 0x04008206 RID: 33286
		[SerializeField]
		private SoundButtonCtrl.CommonInfo[] ciRoot;

		// Token: 0x04008207 RID: 33287
		private int select = -1;

		// Token: 0x02001344 RID: 4932
		[Serializable]
		private class CommonInfo
		{
			// Token: 0x1700228C RID: 8844
			// (set) Token: 0x0600A514 RID: 42260 RVA: 0x00434E10 File Offset: 0x00433210
			public bool active
			{
				set
				{
					if (this.obj && this.obj.activeSelf != value)
					{
						this.obj.SetActive(value);
						this.button.image.color = ((!value) ? Color.white : Color.green);
					}
				}
			}

			// Token: 0x04008208 RID: 33288
			public GameObject obj;

			// Token: 0x04008209 RID: 33289
			public Button button;
		}
	}
}
