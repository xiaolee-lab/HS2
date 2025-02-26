using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class AQUAS_Caustics : MonoBehaviour
{
	// Token: 0x06000084 RID: 132 RVA: 0x00006DB8 File Offset: 0x000051B8
	private void Start()
	{
		this.projector = base.GetComponent<Projector>();
		this.projector.material = UnityEngine.Object.Instantiate<Material>(this.projector.material);
		this.NextFrame();
		base.InvokeRepeating("NextFrame", 1f / this.fps, 1f / this.fps);
		this.projector.material.SetFloat("_WaterLevel", base.transform.parent.transform.position.y);
		this.projector.material.SetFloat("_DepthFade", base.transform.parent.transform.position.y - this.maxCausticDepth);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00006E80 File Offset: 0x00005280
	private void Update()
	{
		this.projector.material.SetFloat("_DepthFade", base.transform.parent.transform.position.y - this.maxCausticDepth);
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00006EC6 File Offset: 0x000052C6
	private void NextFrame()
	{
		this.projector.material.SetTexture("_Texture", this.frames[this.frameIndex]);
		this.frameIndex = (this.frameIndex + 1) % this.frames.Length;
	}

	// Token: 0x0400017D RID: 381
	public float fps;

	// Token: 0x0400017E RID: 382
	public Texture2D[] frames;

	// Token: 0x0400017F RID: 383
	public float maxCausticDepth;

	// Token: 0x04000180 RID: 384
	private int frameIndex;

	// Token: 0x04000181 RID: 385
	private Projector projector;
}
