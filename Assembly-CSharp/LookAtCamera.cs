using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06000C39 RID: 3129 RVA: 0x0002EF2F File Offset: 0x0002D32F
	public void Start()
	{
		if (this.lookAtCamera == null)
		{
			this.lookAtCamera = Camera.main;
		}
		if (this.lookOnlyOnAwake)
		{
			this.LookCam();
		}
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x0002EF5E File Offset: 0x0002D35E
	public void Update()
	{
		if (!this.lookOnlyOnAwake)
		{
			this.LookCam();
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x0002EF71 File Offset: 0x0002D371
	public void LookCam()
	{
		base.transform.LookAt(this.lookAtCamera.transform);
	}

	// Token: 0x04000AE6 RID: 2790
	public Camera lookAtCamera;

	// Token: 0x04000AE7 RID: 2791
	public bool lookOnlyOnAwake;
}
