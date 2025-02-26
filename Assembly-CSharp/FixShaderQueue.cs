using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class FixShaderQueue : MonoBehaviour
{
	// Token: 0x060014A6 RID: 5286 RVA: 0x000817D4 File Offset: 0x0007FBD4
	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
		if (this.rend != null)
		{
			this.rend.sharedMaterial.renderQueue += this.AddQueue;
		}
		else
		{
			base.Invoke("SetProjectorQueue", 0.1f);
		}
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x00081830 File Offset: 0x0007FC30
	private void SetProjectorQueue()
	{
		base.GetComponent<Projector>().material.renderQueue += this.AddQueue;
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x0008184F File Offset: 0x0007FC4F
	private void OnDisable()
	{
		if (this.rend != null)
		{
			this.rend.sharedMaterial.renderQueue = -1;
		}
	}

	// Token: 0x040017C7 RID: 6087
	public int AddQueue = 1;

	// Token: 0x040017C8 RID: 6088
	private Renderer rend;
}
