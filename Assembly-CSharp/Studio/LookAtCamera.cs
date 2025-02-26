using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200128C RID: 4748
	public class LookAtCamera : MonoBehaviour
	{
		// Token: 0x06009D2B RID: 40235 RVA: 0x00403F28 File Offset: 0x00402328
		private void Start()
		{
			this.target = Camera.main.transform;
		}

		// Token: 0x06009D2C RID: 40236 RVA: 0x00403F3A File Offset: 0x0040233A
		private void Update()
		{
			base.transform.LookAt(this.target.position, this.target.up);
		}

		// Token: 0x04007D11 RID: 32017
		private Transform target;
	}
}
