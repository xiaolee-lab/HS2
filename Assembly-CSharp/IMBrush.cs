using System;
using UnityEngine;

// Token: 0x02000A3E RID: 2622
public abstract class IMBrush : MonoBehaviour
{
	// Token: 0x17000E94 RID: 3732
	// (get) Token: 0x06004E11 RID: 19985 RVA: 0x001DE1AE File Offset: 0x001DC5AE
	public float PowerScale
	{
		get
		{
			return (!this.bSubtract) ? 1f : -1f;
		}
	}

	// Token: 0x06004E12 RID: 19986 RVA: 0x001DE1CA File Offset: 0x001DC5CA
	[ContextMenu("Draw")]
	public void Draw()
	{
		if (this.im == null)
		{
			this.im = Utils.FindComponentInParents<IncrementalModeling>(base.transform);
		}
		if (this.im != null)
		{
			this.DoDraw();
		}
	}

	// Token: 0x06004E13 RID: 19987
	protected abstract void DoDraw();

	// Token: 0x04004749 RID: 18249
	public IncrementalModeling im;

	// Token: 0x0400474A RID: 18250
	public float fadeRadius = 0.2f;

	// Token: 0x0400474B RID: 18251
	public bool bSubtract;
}
