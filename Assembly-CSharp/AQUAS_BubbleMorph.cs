using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class AQUAS_BubbleMorph : MonoBehaviour
{
	// Token: 0x06000076 RID: 118 RVA: 0x000063F7 File Offset: 0x000047F7
	private void Start()
	{
		this.skinnedMeshRenderer = base.GetComponent<SkinnedMeshRenderer>();
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00006408 File Offset: 0x00004808
	private void Update()
	{
		this.t += Time.deltaTime;
		this.t2 += Time.deltaTime;
		if (this.t < this.tTarget / 2f)
		{
			this.skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(0f, 50f, this.t / (this.tTarget / 2f)));
			this.skinnedMeshRenderer.SetBlendShapeWeight(1, Mathf.Lerp(50f, 0f, this.t / (this.tTarget / 2f)));
		}
		else if (this.t >= this.tTarget / 2f && this.t < this.tTarget)
		{
			this.skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(50f, 100f, this.t / this.tTarget));
			this.skinnedMeshRenderer.SetBlendShapeWeight(1, Mathf.Lerp(0f, 50f, this.t / this.tTarget));
		}
		else if (this.t >= this.tTarget && this.t < this.tTarget * 1.5f)
		{
			this.skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(100f, 50f, this.t / (this.tTarget * 1.5f)));
			this.skinnedMeshRenderer.SetBlendShapeWeight(1, Mathf.Lerp(50f, 100f, this.t / (this.tTarget * 1.5f)));
		}
		else if (this.t >= this.tTarget * 1.5f && this.t < this.tTarget * 2f)
		{
			this.skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(50f, 0f, this.t / (this.tTarget * 2f)));
			this.skinnedMeshRenderer.SetBlendShapeWeight(1, Mathf.Lerp(100f, 50f, this.t / (this.tTarget * 2f)));
		}
		else
		{
			this.t = 0f;
		}
	}

	// Token: 0x04000166 RID: 358
	private float t;

	// Token: 0x04000167 RID: 359
	private float t2;

	// Token: 0x04000168 RID: 360
	[Space(5f)]
	[Header("Duration of a full morphing cycle")]
	public float tTarget;

	// Token: 0x04000169 RID: 361
	private SkinnedMeshRenderer skinnedMeshRenderer;
}
