using System;
using UnityEngine;

// Token: 0x020010BA RID: 4282
public class CameraLookObject : MonoBehaviour
{
	// Token: 0x06008EDD RID: 36573 RVA: 0x003B6BC8 File Offset: 0x003B4FC8
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x06008EDE RID: 36574 RVA: 0x003B6BD1 File Offset: 0x003B4FD1
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x06008EDF RID: 36575 RVA: 0x003B6BDA File Offset: 0x003B4FDA
	private void OnWillRenderObject()
	{
	}

	// Token: 0x04007378 RID: 29560
	public string targetCamera = "Preview Camera";
}
