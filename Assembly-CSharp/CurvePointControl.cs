using System;
using UnityEngine;

// Token: 0x0200065D RID: 1629
public class CurvePointControl : MonoBehaviour
{
	// Token: 0x0600267E RID: 9854 RVA: 0x000DCFAD File Offset: 0x000DB3AD
	private void OnMouseDrag()
	{
		base.transform.position = DrawCurve.cam.ScreenToViewportPoint(Input.mousePosition);
		DrawCurve.use.UpdateLine(this.objectNumber, Input.mousePosition, base.gameObject);
	}

	// Token: 0x04002671 RID: 9841
	public int objectNumber;

	// Token: 0x04002672 RID: 9842
	public GameObject controlObject;

	// Token: 0x04002673 RID: 9843
	public GameObject controlObject2;
}
