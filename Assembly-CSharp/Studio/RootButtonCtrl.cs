using System;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001339 RID: 4921
	public class RootButtonCtrl : MonoBehaviour
	{
		// Token: 0x17002282 RID: 8834
		// (set) Token: 0x0600A4CD RID: 42189 RVA: 0x0043375D File Offset: 0x00431B5D
		public ObjectCtrlInfo objectCtrlInfo
		{
			set
			{
				this.manipulate.manipulatePanelCtrl.objectCtrlInfo = value;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x0600A4CE RID: 42190 RVA: 0x00433770 File Offset: 0x00431B70
		// (set) Token: 0x0600A4CF RID: 42191 RVA: 0x00433778 File Offset: 0x00431B78
		public int select { get; private set; }

		// Token: 0x0600A4D0 RID: 42192 RVA: 0x00433784 File Offset: 0x00431B84
		public void OnClick(int _kind)
		{
			this.select = ((this.select != _kind) ? _kind : -1);
			for (int i = 0; i < this.ciArray.Length; i++)
			{
				this.ciArray[i].active = (i == this.select);
			}
			Singleton<Studio>.Instance.colorPalette.visible = false;
		}

		// Token: 0x0600A4D1 RID: 42193 RVA: 0x004337EC File Offset: 0x00431BEC
		private void Start()
		{
			this.select = -1;
			this.ciArray = new RootButtonCtrl.CommonInfo[]
			{
				this.add,
				this.manipulate,
				this.sound,
				this.system
			};
			for (int i = 0; i < this.ciArray.Length; i++)
			{
				this.ciArray[i].canvas = this.canvas;
				this.ciArray[i].active = false;
			}
		}

		// Token: 0x040081BB RID: 33211
		[SerializeField]
		private RootButtonCtrl.CommonInfo add = new RootButtonCtrl.CommonInfo();

		// Token: 0x040081BC RID: 33212
		[SerializeField]
		private RootButtonCtrl.ManipulateInfo manipulate = new RootButtonCtrl.ManipulateInfo();

		// Token: 0x040081BD RID: 33213
		[SerializeField]
		private RootButtonCtrl.CommonInfo sound = new RootButtonCtrl.CommonInfo();

		// Token: 0x040081BE RID: 33214
		[SerializeField]
		private RootButtonCtrl.CommonInfo system = new RootButtonCtrl.CommonInfo();

		// Token: 0x040081BF RID: 33215
		[SerializeField]
		private Canvas canvas;

		// Token: 0x040081C1 RID: 33217
		private RootButtonCtrl.CommonInfo[] ciArray;

		// Token: 0x0200133A RID: 4922
		[Serializable]
		private class CommonInfo
		{
			// Token: 0x17002284 RID: 8836
			// (get) Token: 0x0600A4D3 RID: 42195 RVA: 0x00433872 File Offset: 0x00431C72
			// (set) Token: 0x0600A4D4 RID: 42196 RVA: 0x0043387A File Offset: 0x00431C7A
			public Canvas canvas { get; set; }

			// Token: 0x17002285 RID: 8837
			// (set) Token: 0x0600A4D5 RID: 42197 RVA: 0x00433883 File Offset: 0x00431C83
			public virtual bool active
			{
				set
				{
					if (this.objRoot.activeSelf != value)
					{
						this.objRoot.SetActive(value);
						this.select = value;
					}
				}
			}

			// Token: 0x17002286 RID: 8838
			// (set) Token: 0x0600A4D6 RID: 42198 RVA: 0x004338A9 File Offset: 0x00431CA9
			public bool select
			{
				set
				{
					this.button.image.color = ((!value) ? Color.white : Color.green);
					SortCanvas.select = this.canvas;
				}
			}

			// Token: 0x040081C2 RID: 33218
			public GameObject objRoot;

			// Token: 0x040081C4 RID: 33220
			public Button button;
		}

		// Token: 0x0200133B RID: 4923
		[Serializable]
		private class ManipulateInfo : RootButtonCtrl.CommonInfo
		{
			// Token: 0x17002287 RID: 8839
			// (get) Token: 0x0600A4D8 RID: 42200 RVA: 0x004338E3 File Offset: 0x00431CE3
			public ManipulatePanelCtrl manipulatePanelCtrl
			{
				get
				{
					if (this.m_ManipulatePanelCtrl == null)
					{
						this.m_ManipulatePanelCtrl = this.objRoot.GetComponent<ManipulatePanelCtrl>();
					}
					return this.m_ManipulatePanelCtrl;
				}
			}

			// Token: 0x17002288 RID: 8840
			// (set) Token: 0x0600A4D9 RID: 42201 RVA: 0x0043390D File Offset: 0x00431D0D
			public override bool active
			{
				set
				{
					this.manipulatePanelCtrl.active = value;
					base.select = value;
				}
			}

			// Token: 0x040081C5 RID: 33221
			[SerializeField]
			private ManipulatePanelCtrl m_ManipulatePanelCtrl;
		}
	}
}
