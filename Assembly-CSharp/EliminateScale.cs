using System;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

// Token: 0x02000A7E RID: 2686
public class EliminateScale : MonoBehaviour
{
	// Token: 0x06004F78 RID: 20344 RVA: 0x001E9113 File Offset: 0x001E7513
	private void Start()
	{
		this.defScale = base.transform.lossyScale;
		this.defPositon = base.transform.localPosition;
		this.defRotation = base.transform.localRotation;
	}

	// Token: 0x06004F79 RID: 20345 RVA: 0x001E9148 File Offset: 0x001E7548
	private void LateUpdate()
	{
		Vector3 lossyScale = base.transform.lossyScale;
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3((this.kind != EliminateScale.EliminateScaleKind.ALL && this.kind != EliminateScale.EliminateScaleKind.X && this.kind != EliminateScale.EliminateScaleKind.XY && this.kind != EliminateScale.EliminateScaleKind.XZ) ? localScale.x : (localScale.x / lossyScale.x * this.defScale.x), (this.kind != EliminateScale.EliminateScaleKind.ALL && this.kind != EliminateScale.EliminateScaleKind.Y && this.kind != EliminateScale.EliminateScaleKind.XY && this.kind != EliminateScale.EliminateScaleKind.YZ) ? localScale.y : (localScale.y / lossyScale.y * this.defScale.y), (this.kind != EliminateScale.EliminateScaleKind.ALL && this.kind != EliminateScale.EliminateScaleKind.Z && this.kind != EliminateScale.EliminateScaleKind.XZ && this.kind != EliminateScale.EliminateScaleKind.YZ) ? localScale.z : (localScale.z / lossyScale.z * this.defScale.z));
		if (this.custom != null)
		{
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			for (int i = 0; i < this.lstShape.Count; i++)
			{
				float shapeBodyValue = this.custom.GetShapeBodyValue(Mathf.Max(this.lstShape[i].numShape, 0));
				vector += ((shapeBodyValue < 0.5f) ? Vector3.Lerp(this.lstShape[i].posMin, this.lstShape[i].posMid, Mathf.InverseLerp(0f, 0.5f, shapeBodyValue)) : Vector3.Lerp(this.lstShape[i].posMid, this.lstShape[i].posMax, Mathf.InverseLerp(0.5f, 1f, shapeBodyValue)));
				vector2 += ((shapeBodyValue < 0.5f) ? Vector3.Lerp(this.lstShape[i].rotMin, this.lstShape[i].rotMid, Mathf.InverseLerp(0f, 0.5f, shapeBodyValue)) : Vector3.Lerp(this.lstShape[i].rotMid, this.lstShape[i].rotMax, Mathf.InverseLerp(0.5f, 1f, shapeBodyValue)));
			}
			base.transform.localPosition = this.defPositon + vector;
			base.transform.localRotation = this.defRotation * Quaternion.Euler(vector2);
		}
	}

	// Token: 0x0400487A RID: 18554
	[Tooltip("省きたいスケール軸")]
	public EliminateScale.EliminateScaleKind kind;

	// Token: 0x0400487B RID: 18555
	public List<EliminateScale.ShapeMove> lstShape = new List<EliminateScale.ShapeMove>();

	// Token: 0x0400487C RID: 18556
	[Header("Debug表示")]
	public Vector3 defScale = Vector3.one;

	// Token: 0x0400487D RID: 18557
	public Vector3 defPositon = Vector3.zero;

	// Token: 0x0400487E RID: 18558
	public Quaternion defRotation = Quaternion.identity;

	// Token: 0x0400487F RID: 18559
	public ChaControl custom;

	// Token: 0x02000A7F RID: 2687
	public enum EliminateScaleKind
	{
		// Token: 0x04004881 RID: 18561
		ALL,
		// Token: 0x04004882 RID: 18562
		X,
		// Token: 0x04004883 RID: 18563
		Y,
		// Token: 0x04004884 RID: 18564
		Z,
		// Token: 0x04004885 RID: 18565
		XY,
		// Token: 0x04004886 RID: 18566
		XZ,
		// Token: 0x04004887 RID: 18567
		YZ,
		// Token: 0x04004888 RID: 18568
		NONE
	}

	// Token: 0x02000A80 RID: 2688
	[Serializable]
	public class ShapeMove
	{
		// Token: 0x04004889 RID: 18569
		public int numShape;

		// Token: 0x0400488A RID: 18570
		public Vector3 posMax;

		// Token: 0x0400488B RID: 18571
		public Vector3 posMid;

		// Token: 0x0400488C RID: 18572
		public Vector3 posMin;

		// Token: 0x0400488D RID: 18573
		public Vector3 rotMax;

		// Token: 0x0400488E RID: 18574
		public Vector3 rotMid;

		// Token: 0x0400488F RID: 18575
		public Vector3 rotMin;
	}
}
