using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
[ExecuteInEditMode]
[AddComponentMenu("AQUAS/Render Queue Controller")]
public class AQUAS_RenderQueueEditor : MonoBehaviour
{
	// Token: 0x060000A7 RID: 167 RVA: 0x00009308 File Offset: 0x00007708
	private void Update()
	{
		base.gameObject.GetComponent<Renderer>().sharedMaterial.renderQueue = this.renderQueueIndex;
	}

	// Token: 0x040001E8 RID: 488
	public int renderQueueIndex = -1;
}
