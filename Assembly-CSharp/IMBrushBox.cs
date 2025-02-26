using System;
using UnityEngine;

// Token: 0x02000A3F RID: 2623
public class IMBrushBox : IMBrush
{
	// Token: 0x06004E15 RID: 19989 RVA: 0x001DE21D File Offset: 0x001DC61D
	protected override void DoDraw()
	{
		this.im.AddBox(base.transform, this.extents, base.PowerScale, this.fadeRadius);
	}

	// Token: 0x06004E16 RID: 19990 RVA: 0x001DE244 File Offset: 0x001DC644
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(Vector3.zero, this.extents * 2f);
		if (this.im != null)
		{
			Gizmos.color = ((!this.bSubtract) ? Color.white : Color.red);
			float powerThreshold = this.im.powerThreshold;
			Vector3 a = new Vector3(Mathf.Lerp(this.extents.x, this.extents.x - this.fadeRadius, powerThreshold), Mathf.Lerp(this.extents.y, this.extents.y - this.fadeRadius, powerThreshold), Mathf.Lerp(this.extents.z, this.extents.z - this.fadeRadius, powerThreshold));
			Gizmos.DrawWireCube(Vector3.zero, a * 2f);
		}
	}

	// Token: 0x0400474C RID: 18252
	public Vector3 extents = Vector3.one;
}
