using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000673 RID: 1651
public class PowerBar : MonoBehaviour
{
	// Token: 0x060026CE RID: 9934 RVA: 0x000E0514 File Offset: 0x000DE914
	private void Start()
	{
		this.position = new Vector2(this.radius + 20f, (float)Screen.height - (this.radius + 20f));
		VectorLine vectorLine = new VectorLine("BarBackground", new List<Vector2>(50), null, (float)this.lineWidth, LineType.Continuous, Joins.Weld);
		vectorLine.MakeCircle(this.position, this.radius);
		vectorLine.Draw();
		this.bar = new VectorLine("TotalBar", new List<Vector2>(this.segmentCount + 1), null, (float)(this.lineWidth - 4), LineType.Continuous, Joins.Weld);
		this.bar.color = Color.black;
		this.bar.MakeArc(this.position, this.radius, this.radius, 0f, 270f);
		this.bar.Draw();
		this.currentPower = UnityEngine.Random.value;
		this.SetTargetPower();
		this.bar.SetColor(Color.red, 0, (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower));
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x000E0638 File Offset: 0x000DEA38
	private void SetTargetPower()
	{
		this.targetPower = UnityEngine.Random.value;
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x000E0648 File Offset: 0x000DEA48
	private void Update()
	{
		float t = this.currentPower;
		if (this.targetPower < this.currentPower)
		{
			this.currentPower -= this.speed * Time.deltaTime;
			if (this.currentPower < this.targetPower)
			{
				this.SetTargetPower();
			}
			this.bar.SetColor(Color.black, (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower), (int)Mathf.Lerp(0f, (float)this.segmentCount, t));
		}
		else
		{
			this.currentPower += this.speed * Time.deltaTime;
			if (this.currentPower > this.targetPower)
			{
				this.SetTargetPower();
			}
			this.bar.SetColor(Color.red, (int)Mathf.Lerp(0f, (float)this.segmentCount, t), (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower));
		}
	}

	// Token: 0x060026D1 RID: 9937 RVA: 0x000E0754 File Offset: 0x000DEB54
	private void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width / 2 - 40), (float)(Screen.height / 2 - 15), 80f, 30f), "Power: " + (this.currentPower * 100f).ToString("f0") + "%");
	}

	// Token: 0x04002703 RID: 9987
	public float speed = 0.25f;

	// Token: 0x04002704 RID: 9988
	public int lineWidth = 25;

	// Token: 0x04002705 RID: 9989
	public float radius = 60f;

	// Token: 0x04002706 RID: 9990
	public int segmentCount = 200;

	// Token: 0x04002707 RID: 9991
	private VectorLine bar;

	// Token: 0x04002708 RID: 9992
	private Vector2 position;

	// Token: 0x04002709 RID: 9993
	private float currentPower;

	// Token: 0x0400270A RID: 9994
	private float targetPower;
}
