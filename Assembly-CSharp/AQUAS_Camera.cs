using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
[AddComponentMenu("AQUAS/AQUAS Camera")]
[RequireComponent(typeof(Camera))]
public class AQUAS_Camera : MonoBehaviour
{
	// Token: 0x06000081 RID: 129 RVA: 0x00006D88 File Offset: 0x00005188
	private void Start()
	{
		this.Set();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00006D90 File Offset: 0x00005190
	private void Set()
	{
		if (base.GetComponent<Camera>().depthTextureMode == DepthTextureMode.None)
		{
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
		}
	}
}
