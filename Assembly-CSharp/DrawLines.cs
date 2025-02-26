using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000660 RID: 1632
public class DrawLines : MonoBehaviour
{
	// Token: 0x0600268C RID: 9868 RVA: 0x000DDE48 File Offset: 0x000DC248
	private void Start()
	{
		this.SetLine();
	}

	// Token: 0x0600268D RID: 9869 RVA: 0x000DDE50 File Offset: 0x000DC250
	private void SetLine()
	{
		VectorLine.Destroy(ref this.line);
		if (!this.continuous)
		{
			this.fillJoins = false;
		}
		LineType lineType = (!this.continuous) ? LineType.Discrete : LineType.Continuous;
		Joins joins = (!this.fillJoins) ? Joins.None : Joins.Fill;
		int num = (!this.thickLine) ? 2 : 24;
		this.line = new VectorLine("Line", new List<Vector2>(), (float)num, lineType, joins);
		this.line.drawTransform = base.transform;
		this.endReached = false;
	}

	// Token: 0x0600268E RID: 9870 RVA: 0x000DDEE8 File Offset: 0x000DC2E8
	private void Update()
	{
		Vector3 v = base.transform.InverseTransformPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0) && this.canClick && !this.endReached)
		{
			this.line.points2.Add(v);
			if (this.line.points2.Count == 1)
			{
				this.line.points2.Add(Vector2.zero);
			}
			if ((float)this.line.points2.Count == this.maxPoints)
			{
				this.endReached = true;
			}
		}
		if (this.line.points2.Count >= 2)
		{
			this.line.points2[this.line.points2.Count - 1] = v;
			this.line.Draw();
		}
		base.transform.RotateAround(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vector3.forward, Time.deltaTime * this.rotateSpeed * Input.GetAxis("Horizontal"));
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x000DE018 File Offset: 0x000DC418
	private void OnGUI()
	{
		Rect screenRect = new Rect(20f, 20f, 265f, 220f);
		this.canClick = !screenRect.Contains(Event.current.mousePosition);
		GUILayout.BeginArea(screenRect);
		GUI.contentColor = Color.black;
		GUILayout.Label("Click to add points to the line\nRotate with the right/left arrow keys", Array.Empty<GUILayoutOption>());
		GUILayout.Space(5f);
		this.continuous = GUILayout.Toggle(this.continuous, "Continuous line", Array.Empty<GUILayoutOption>());
		this.thickLine = GUILayout.Toggle(this.thickLine, "Thick line", Array.Empty<GUILayoutOption>());
		this.line.lineWidth = (float)((!this.thickLine) ? 2 : 24);
		this.fillJoins = GUILayout.Toggle(this.fillJoins, "Fill joins (only works with continuous line)", Array.Empty<GUILayoutOption>());
		if (this.line.lineType != LineType.Continuous)
		{
			this.fillJoins = false;
		}
		this.weldJoins = GUILayout.Toggle(this.weldJoins, "Weld joins", Array.Empty<GUILayoutOption>());
		if (this.oldContinuous != this.continuous)
		{
			this.oldContinuous = this.continuous;
			this.line.lineType = ((!this.continuous) ? LineType.Discrete : LineType.Continuous);
		}
		if (this.oldFillJoins != this.fillJoins)
		{
			if (this.fillJoins)
			{
				this.weldJoins = false;
			}
			this.oldFillJoins = this.fillJoins;
		}
		else if (this.oldWeldJoins != this.weldJoins)
		{
			if (this.weldJoins)
			{
				this.fillJoins = false;
			}
			this.oldWeldJoins = this.weldJoins;
		}
		if (this.fillJoins)
		{
			this.line.joins = Joins.Fill;
		}
		else if (this.weldJoins)
		{
			this.line.joins = Joins.Weld;
		}
		else
		{
			this.line.joins = Joins.None;
		}
		GUILayout.Space(10f);
		GUI.contentColor = Color.white;
		if (GUILayout.Button("Randomize Color", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			this.RandomizeColor();
		}
		if (GUILayout.Button("Randomize All Colors", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			this.RandomizeAllColors();
		}
		if (GUILayout.Button("Reset line", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		}))
		{
			this.SetLine();
		}
		if (this.endReached)
		{
			GUI.contentColor = Color.black;
			GUILayout.Label("No more points available. You must be bored!", Array.Empty<GUILayoutOption>());
		}
		GUILayout.EndArea();
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x000DE2BB File Offset: 0x000DC6BB
	private void RandomizeColor()
	{
		this.line.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x000DE2E4 File Offset: 0x000DC6E4
	private void RandomizeAllColors()
	{
		int segmentNumber = this.line.GetSegmentNumber();
		for (int i = 0; i < segmentNumber; i++)
		{
			this.line.SetColor(new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value), i);
		}
	}

	// Token: 0x04002689 RID: 9865
	public float rotateSpeed = 90f;

	// Token: 0x0400268A RID: 9866
	public float maxPoints = 500f;

	// Token: 0x0400268B RID: 9867
	private VectorLine line;

	// Token: 0x0400268C RID: 9868
	private bool endReached;

	// Token: 0x0400268D RID: 9869
	private bool continuous = true;

	// Token: 0x0400268E RID: 9870
	private bool oldContinuous = true;

	// Token: 0x0400268F RID: 9871
	private bool fillJoins;

	// Token: 0x04002690 RID: 9872
	private bool oldFillJoins;

	// Token: 0x04002691 RID: 9873
	private bool weldJoins;

	// Token: 0x04002692 RID: 9874
	private bool oldWeldJoins;

	// Token: 0x04002693 RID: 9875
	private bool thickLine;

	// Token: 0x04002694 RID: 9876
	private bool canClick = true;
}
