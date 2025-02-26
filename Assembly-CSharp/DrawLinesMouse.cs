using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000661 RID: 1633
public class DrawLinesMouse : MonoBehaviour
{
	// Token: 0x06002693 RID: 9875 RVA: 0x000DE370 File Offset: 0x000DC770
	private void Start()
	{
		Texture2D texture;
		float width;
		if (this.useEndCap)
		{
			VectorLine.SetEndCap("RoundCap", EndCap.Mirror, new Texture2D[]
			{
				this.capLineTex,
				this.capTex
			});
			texture = this.capLineTex;
			width = this.capLineWidth;
		}
		else
		{
			texture = this.lineTex;
			width = this.lineWidth;
		}
		if (this.line3D)
		{
			this.line = new VectorLine("DrawnLine3D", new List<Vector3>(), texture, width, LineType.Continuous, Joins.Weld);
		}
		else
		{
			this.line = new VectorLine("DrawnLine", new List<Vector2>(), texture, width, LineType.Continuous, Joins.Weld);
		}
		this.line.endPointsUpdate = 2;
		if (this.useEndCap)
		{
			this.line.endCap = "RoundCap";
		}
		this.sqrMinPixelMove = this.minPixelMove * this.minPixelMove;
	}

	// Token: 0x06002694 RID: 9876 RVA: 0x000DE448 File Offset: 0x000DC848
	private void Update()
	{
		Vector3 mousePos = this.GetMousePos();
		if (Input.GetMouseButtonDown(0))
		{
			if (this.line3D)
			{
				this.line.points3.Clear();
				this.line.Draw3D();
			}
			else
			{
				this.line.points2.Clear();
				this.line.Draw();
			}
			this.previousPosition = Input.mousePosition;
			if (this.line3D)
			{
				this.line.points3.Add(mousePos);
			}
			else
			{
				this.line.points2.Add(mousePos);
			}
			this.canDraw = true;
		}
		else if (Input.GetMouseButton(0) && (Input.mousePosition - this.previousPosition).sqrMagnitude > (float)this.sqrMinPixelMove && this.canDraw)
		{
			this.previousPosition = Input.mousePosition;
			int count;
			if (this.line3D)
			{
				this.line.points3.Add(mousePos);
				count = this.line.points3.Count;
				this.line.Draw3D();
			}
			else
			{
				this.line.points2.Add(mousePos);
				count = this.line.points2.Count;
				this.line.Draw();
			}
			if (count >= this.maxPoints)
			{
				this.canDraw = false;
			}
		}
	}

	// Token: 0x06002695 RID: 9877 RVA: 0x000DE5C4 File Offset: 0x000DC9C4
	private Vector3 GetMousePos()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.line3D)
		{
			mousePosition.z = this.distanceFromCamera;
			return Camera.main.ScreenToWorldPoint(mousePosition);
		}
		return mousePosition;
	}

	// Token: 0x04002695 RID: 9877
	public Texture2D lineTex;

	// Token: 0x04002696 RID: 9878
	public int maxPoints = 5000;

	// Token: 0x04002697 RID: 9879
	public float lineWidth = 4f;

	// Token: 0x04002698 RID: 9880
	public int minPixelMove = 5;

	// Token: 0x04002699 RID: 9881
	public bool useEndCap;

	// Token: 0x0400269A RID: 9882
	public Texture2D capLineTex;

	// Token: 0x0400269B RID: 9883
	public Texture2D capTex;

	// Token: 0x0400269C RID: 9884
	public float capLineWidth = 20f;

	// Token: 0x0400269D RID: 9885
	public bool line3D;

	// Token: 0x0400269E RID: 9886
	public float distanceFromCamera = 1f;

	// Token: 0x0400269F RID: 9887
	private VectorLine line;

	// Token: 0x040026A0 RID: 9888
	private Vector3 previousPosition;

	// Token: 0x040026A1 RID: 9889
	private int sqrMinPixelMove;

	// Token: 0x040026A2 RID: 9890
	private bool canDraw;
}
