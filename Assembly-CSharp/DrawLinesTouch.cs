using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000662 RID: 1634
public class DrawLinesTouch : MonoBehaviour
{
	// Token: 0x06002697 RID: 9879 RVA: 0x000DE62C File Offset: 0x000DCA2C
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
		this.line = new VectorLine("DrawnLine", new List<Vector2>(), texture, width, LineType.Continuous, Joins.Weld);
		this.line.endPointsUpdate = 2;
		if (this.useEndCap)
		{
			this.line.endCap = "RoundCap";
		}
		this.sqrMinPixelMove = this.minPixelMove * this.minPixelMove;
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x000DE6DC File Offset: 0x000DCADC
	private void Update()
	{
		if (Input.touchCount > 0)
		{
			this.touch = Input.GetTouch(0);
			if (this.touch.phase == TouchPhase.Began)
			{
				this.line.points2.Clear();
				this.line.Draw();
				this.previousPosition = this.touch.position;
				this.line.points2.Add(this.touch.position);
				this.canDraw = true;
			}
			else if (this.touch.phase == TouchPhase.Moved && (this.touch.position - this.previousPosition).sqrMagnitude > (float)this.sqrMinPixelMove && this.canDraw)
			{
				this.previousPosition = this.touch.position;
				this.line.points2.Add(this.touch.position);
				if (this.line.points2.Count >= this.maxPoints)
				{
					this.canDraw = false;
				}
				this.line.Draw();
			}
		}
	}

	// Token: 0x040026A3 RID: 9891
	public Texture2D lineTex;

	// Token: 0x040026A4 RID: 9892
	public int maxPoints = 5000;

	// Token: 0x040026A5 RID: 9893
	public float lineWidth = 4f;

	// Token: 0x040026A6 RID: 9894
	public int minPixelMove = 5;

	// Token: 0x040026A7 RID: 9895
	public bool useEndCap;

	// Token: 0x040026A8 RID: 9896
	public Texture2D capLineTex;

	// Token: 0x040026A9 RID: 9897
	public Texture2D capTex;

	// Token: 0x040026AA RID: 9898
	public float capLineWidth = 20f;

	// Token: 0x040026AB RID: 9899
	private VectorLine line;

	// Token: 0x040026AC RID: 9900
	private Vector2 previousPosition;

	// Token: 0x040026AD RID: 9901
	private int sqrMinPixelMove;

	// Token: 0x040026AE RID: 9902
	private bool canDraw;

	// Token: 0x040026AF RID: 9903
	private Touch touch;
}
