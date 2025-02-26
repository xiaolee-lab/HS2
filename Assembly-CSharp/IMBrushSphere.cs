using System;
using UnityEngine;

// Token: 0x02000A40 RID: 2624
public class IMBrushSphere : IMBrush
{
	// Token: 0x06004E18 RID: 19992 RVA: 0x001DE359 File Offset: 0x001DC759
	protected override void DoDraw()
	{
		this.im.AddSphere(base.transform, this.radius, base.PowerScale, this.fadeRadius);
	}

	// Token: 0x06004E19 RID: 19993 RVA: 0x001DE380 File Offset: 0x001DC780
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(Vector3.zero, this.radius);
		if (this.im != null)
		{
			Gizmos.color = ((!this.bSubtract) ? Color.white : Color.red);
			float powerThreshold = this.im.powerThreshold;
			float num = Mathf.Lerp(this.radius, this.radius - this.fadeRadius, powerThreshold);
			Gizmos.DrawWireSphere(Vector3.zero, num);
		}
	}

	// Token: 0x0400474D RID: 18253
	public float radius = 1f;
}
