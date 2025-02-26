using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001315 RID: 4885
	public class MPFolderCtrl : MonoBehaviour
	{
		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x0600A2D1 RID: 41681 RVA: 0x0042A1FB File Offset: 0x004285FB
		// (set) Token: 0x0600A2D2 RID: 41682 RVA: 0x0042A203 File Offset: 0x00428603
		public OCIFolder ociFolder
		{
			get
			{
				return this.m_OCIFolder;
			}
			set
			{
				this.m_OCIFolder = value;
				if (this.m_OCIFolder != null)
				{
					this.UpdateInfo();
				}
			}
		}

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x0600A2D3 RID: 41683 RVA: 0x0042A21D File Offset: 0x0042861D
		// (set) Token: 0x0600A2D4 RID: 41684 RVA: 0x0042A225 File Offset: 0x00428625
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
				if (this.m_Active)
				{
					base.gameObject.SetActive(this.m_OCIFolder != null);
				}
				else
				{
					base.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x0600A2D5 RID: 41685 RVA: 0x0042A261 File Offset: 0x00428661
		public bool Deselect(OCIFolder _ociFolder)
		{
			if (this.m_OCIFolder != _ociFolder)
			{
				return false;
			}
			this.ociFolder = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A2D6 RID: 41686 RVA: 0x0042A280 File Offset: 0x00428680
		private void UpdateInfo()
		{
			if (this.m_OCIFolder == null)
			{
				return;
			}
			this.isUpdateInfo = true;
			this.inputName.text = this.m_OCIFolder.name;
			this.isUpdateInfo = false;
		}

		// Token: 0x0600A2D7 RID: 41687 RVA: 0x0042A2B2 File Offset: 0x004286B2
		private void OnEndEditName(string _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIFolder.name = _value;
		}

		// Token: 0x0600A2D8 RID: 41688 RVA: 0x0042A2CC File Offset: 0x004286CC
		private void Start()
		{
			this.inputName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditName));
		}

		// Token: 0x0400808E RID: 32910
		[SerializeField]
		private InputField inputName;

		// Token: 0x0400808F RID: 32911
		private OCIFolder m_OCIFolder;

		// Token: 0x04008090 RID: 32912
		private bool m_Active;

		// Token: 0x04008091 RID: 32913
		private bool isUpdateInfo;
	}
}
