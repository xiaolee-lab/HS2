using System;
using UnityEngine;
using Vectrosity;

// Token: 0x0200067F RID: 1663
public class VectorObject : MonoBehaviour
{
	// Token: 0x060026FB RID: 9979 RVA: 0x000E1CE8 File Offset: 0x000E00E8
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("Shape", XrayLineData.use.shapePoints[(int)this.shape], XrayLineData.use.lineTexture, XrayLineData.use.lineWidth);
		vectorLine.color = Color.green;
		VectorManager.ObjectSetup(base.gameObject, vectorLine, Visibility.Always, Brightness.None);
	}

	// Token: 0x0400273E RID: 10046
	public VectorObject.Shape shape;

	// Token: 0x02000680 RID: 1664
	public enum Shape
	{
		// Token: 0x04002740 RID: 10048
		Cube,
		// Token: 0x04002741 RID: 10049
		Sphere
	}
}
