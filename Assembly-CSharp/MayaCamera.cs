using System;
using UnityEngine;

// Token: 0x0200077A RID: 1914
public class MayaCamera : MonoBehaviour
{
	// Token: 0x06002CCE RID: 11470 RVA: 0x00100D20 File Offset: 0x000FF120
	private void LateUpdate()
	{
		if (this._camera != null)
		{
			this._camera.fieldOfView = base.transform.localScale.z;
		}
	}

	// Token: 0x04002B7F RID: 11135
	public Camera _camera;
}
