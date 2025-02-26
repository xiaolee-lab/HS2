using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class RandomMaterial : MonoBehaviour
{
	// Token: 0x06000C31 RID: 3121 RVA: 0x0002ED77 File Offset: 0x0002D177
	public void Start()
	{
		this.ChangeMaterial();
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0002ED7F File Offset: 0x0002D17F
	public void ChangeMaterial()
	{
		this.targetRenderer.sharedMaterial = this.materials[UnityEngine.Random.Range(0, this.materials.Length)];
	}

	// Token: 0x04000ADE RID: 2782
	public Renderer targetRenderer;

	// Token: 0x04000ADF RID: 2783
	public Material[] materials;
}
