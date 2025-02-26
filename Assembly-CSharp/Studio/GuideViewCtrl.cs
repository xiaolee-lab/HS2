using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001256 RID: 4694
	public class GuideViewCtrl : MonoBehaviour
	{
		// Token: 0x06009AD6 RID: 39638 RVA: 0x003F85EC File Offset: 0x003F69EC
		public void OnClick()
		{
			this.isDefault = !this.isDefault;
			this.camera.cullingMask = ((!this.isDefault) ? this.layerMask : this.layerMaskDefault);
			this.drawLightLine.enabled = this.isDefault;
		}

		// Token: 0x06009AD7 RID: 39639 RVA: 0x003F8645 File Offset: 0x003F6A45
		private void Awake()
		{
			this.camera.enabled = true;
			this.layerMaskDefault = this.camera.cullingMask;
			this.drawLightLine.enabled = true;
			this.isDefault = true;
		}

		// Token: 0x04007B79 RID: 31609
		[SerializeField]
		private Camera camera;

		// Token: 0x04007B7A RID: 31610
		[SerializeField]
		private LayerMask layerMask = -1;

		// Token: 0x04007B7B RID: 31611
		private LayerMask layerMaskDefault = -1;

		// Token: 0x04007B7C RID: 31612
		[SerializeField]
		private DrawLightLine drawLightLine;

		// Token: 0x04007B7D RID: 31613
		private bool isDefault = true;
	}
}
