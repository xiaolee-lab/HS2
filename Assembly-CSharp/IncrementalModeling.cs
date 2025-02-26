using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A41 RID: 2625
public class IncrementalModeling : ImplicitSurface
{
	// Token: 0x06004E1B RID: 19995 RVA: 0x001DE434 File Offset: 0x001DC834
	protected override void InitializePowerMap()
	{
		foreach (IncrementalModeling.Brush brush in this._brushHistory)
		{
			brush.Draw(this);
		}
	}

	// Token: 0x06004E1C RID: 19996 RVA: 0x001DE490 File Offset: 0x001DC890
	[ContextMenu("Rebuild")]
	public void Rebuild()
	{
		base.ResetMaps();
		foreach (IncrementalModeling.Brush brush in this._brushHistory)
		{
			brush.Draw(this);
		}
		this.CreateMesh();
	}

	// Token: 0x06004E1D RID: 19997 RVA: 0x001DE4F8 File Offset: 0x001DC8F8
	[ContextMenu("ClearHistory")]
	public void ClearHistory()
	{
		this._brushHistory.Clear();
	}

	// Token: 0x06004E1E RID: 19998 RVA: 0x001DE508 File Offset: 0x001DC908
	public void AddSphere(Transform brushTransform, float radius, float powerScale, float fadeRadius)
	{
		Matrix4x4 invTransformMtx_ = brushTransform.worldToLocalMatrix * base.transform.localToWorldMatrix;
		IncrementalModeling.Brush brush = new IncrementalModeling.Brush(IncrementalModeling.Brush.Shape.sphere, invTransformMtx_, fadeRadius, powerScale, radius, Vector3.one);
		brush.Draw(this);
		if (this.bSaveBrushHistory)
		{
			this._brushHistory.Add(brush);
		}
		this.CreateMesh();
	}

	// Token: 0x06004E1F RID: 19999 RVA: 0x001DE564 File Offset: 0x001DC964
	public void AddBox(Transform brushTransform, Vector3 extents, float powerScale, float fadeRadius)
	{
		Matrix4x4 invTransformMtx_ = brushTransform.worldToLocalMatrix * base.transform.localToWorldMatrix;
		IncrementalModeling.Brush brush = new IncrementalModeling.Brush(IncrementalModeling.Brush.Shape.box, invTransformMtx_, fadeRadius, powerScale, 1f, extents);
		brush.Draw(this);
		if (this.bSaveBrushHistory)
		{
			this._brushHistory.Add(brush);
		}
		this.CreateMesh();
	}

	// Token: 0x0400474E RID: 18254
	public bool bSaveBrushHistory = true;

	// Token: 0x0400474F RID: 18255
	[SerializeField]
	private List<IncrementalModeling.Brush> _brushHistory = new List<IncrementalModeling.Brush>();

	// Token: 0x02000A42 RID: 2626
	[Serializable]
	public class Brush
	{
		// Token: 0x06004E20 RID: 20000 RVA: 0x001DE5BD File Offset: 0x001DC9BD
		public Brush()
		{
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x001DE5FC File Offset: 0x001DC9FC
		public Brush(IncrementalModeling.Brush.Shape shape_, Matrix4x4 invTransformMtx_, float fadeRadius_, float powerScale_, float sphereRadius_, Vector3 boxExtents_)
		{
			this.shape = shape_;
			this.fadeRadius = fadeRadius_;
			this.powerScale = powerScale_;
			this.invTransform = invTransformMtx_;
			this.sphereRadius = sphereRadius_;
			this.boxExtents = boxExtents_;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x001DE674 File Offset: 0x001DCA74
		public void Draw(IncrementalModeling model)
		{
			IncrementalModeling.Brush.Shape shape = this.shape;
			if (shape != IncrementalModeling.Brush.Shape.sphere)
			{
				if (shape == IncrementalModeling.Brush.Shape.box)
				{
					this.DrawBox(model);
				}
			}
			else
			{
				this.DrawSphere(model);
			}
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x001DE6B4 File Offset: 0x001DCAB4
		private void DrawSphere(IncrementalModeling model)
		{
			int num = model._countX * model._countY * model._countZ;
			for (int i = 0; i < num; i++)
			{
				float magnitude = this.invTransform.MultiplyPoint(model._positionMap[i]).magnitude;
				if (magnitude < this.sphereRadius)
				{
					float num2 = 1f;
					if (this.fadeRadius > 0f)
					{
						num2 = Mathf.Clamp01((this.sphereRadius - magnitude) / this.fadeRadius);
					}
					model._powerMap[i] = Mathf.Clamp01(model._powerMap[i] + this.powerScale * num2);
					model._powerMap[i] *= model._powerMapMask[i];
				}
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x001DE780 File Offset: 0x001DCB80
		private void DrawBox(IncrementalModeling model)
		{
			int num = model._countX * model._countY * model._countZ;
			for (int i = 0; i < num; i++)
			{
				float num2 = 1f;
				Vector3 vector = this.invTransform.MultiplyPoint(model._positionMap[i]);
				for (int j = 0; j < 3; j++)
				{
					float num3 = Mathf.Abs(vector[j]);
					float num4 = this.boxExtents[j];
					if (num3 >= num4)
					{
						num2 = 0f;
						break;
					}
					if (this.fadeRadius > 0f)
					{
						num2 *= Mathf.Clamp01((num4 - num3) / this.fadeRadius);
					}
				}
				if (num2 > 0f)
				{
					model._powerMap[i] = Mathf.Clamp01(model._powerMap[i] + this.powerScale * num2);
					model._powerMap[i] *= model._powerMapMask[i];
				}
			}
		}

		// Token: 0x04004750 RID: 18256
		public float fadeRadius = 0.1f;

		// Token: 0x04004751 RID: 18257
		public float powerScale = 1f;

		// Token: 0x04004752 RID: 18258
		public Matrix4x4 invTransform;

		// Token: 0x04004753 RID: 18259
		public float sphereRadius = 0.5f;

		// Token: 0x04004754 RID: 18260
		public Vector3 boxExtents = Vector3.one * 0.5f;

		// Token: 0x04004755 RID: 18261
		public IncrementalModeling.Brush.Shape shape;

		// Token: 0x02000A43 RID: 2627
		public enum Shape
		{
			// Token: 0x04004757 RID: 18263
			sphere,
			// Token: 0x04004758 RID: 18264
			box
		}
	}
}
