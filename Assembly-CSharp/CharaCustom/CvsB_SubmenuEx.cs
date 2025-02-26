using System;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009F3 RID: 2547
	public class CvsB_SubmenuEx : MonoBehaviour
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06004B08 RID: 19208 RVA: 0x001CA3BD File Offset: 0x001C87BD
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x001CA3C4 File Offset: 0x001C87C4
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06004B0A RID: 19210 RVA: 0x001CA3D1 File Offset: 0x001C87D1
		protected ChaFileParameter parameter
		{
			get
			{
				return this.chaCtrl.fileParam;
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x001CA3DE File Offset: 0x001C87DE
		public void UpdateCustomUI()
		{
			this.tglFutanari.SetIsOnWithoutCallback(this.parameter.futanari);
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x001CA3F6 File Offset: 0x001C87F6
		private void Start()
		{
			this.customBase.actUpdateCvsFutanari += this.UpdateCustomUI;
			this.tglFutanari.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				this.parameter.futanari = isOn;
			});
		}

		// Token: 0x0400452B RID: 17707
		[SerializeField]
		private Toggle tglFutanari;
	}
}
