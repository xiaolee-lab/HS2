using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class ME_TrailRendererNoise : MonoBehaviour
{
	// Token: 0x060013A6 RID: 5030 RVA: 0x00079814 File Offset: 0x00077C14
	private void Start()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.lineRenderer.useWorldSpace = true;
		this.t = base.transform;
		this.prevPos = this.t.position;
		this.points.Insert(0, this.t.position);
		this.lifeTimes.Insert(0, this.VertexTime);
		this.velocities.Insert(0, Vector3.zero);
		this.randomOffset = (float)UnityEngine.Random.Range(0, 10000000) / 1000000f;
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000798A8 File Offset: 0x00077CA8
	private void OnEnable()
	{
		this.points.Clear();
		this.lifeTimes.Clear();
		this.velocities.Clear();
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000798CC File Offset: 0x00077CCC
	private void Update()
	{
		if (this.IsActive)
		{
			this.AddNewPoints();
		}
		this.UpdatetPoints();
		if (this.SmoothCurves && this.points.Count > 2)
		{
			this.UpdateLineRendererBezier();
		}
		else
		{
			this.UpdateLineRenderer();
		}
		if (this.AutodestructWhenNotActive && !this.IsActive && this.points.Count <= 1)
		{
			UnityEngine.Object.Destroy(base.gameObject, this.TotalLifeTime);
		}
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x00079958 File Offset: 0x00077D58
	private void AddNewPoints()
	{
		if ((this.t.position - this.prevPos).magnitude > this.MinVertexDistance || (this.IsRibbon && this.points.Count == 0) || (this.IsRibbon && this.points.Count > 0 && (this.t.position - this.points[0]).magnitude > this.MinVertexDistance))
		{
			this.prevPos = this.t.position;
			this.points.Insert(0, this.t.position);
			this.lifeTimes.Insert(0, this.VertexTime);
			this.velocities.Insert(0, Vector3.zero);
		}
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x00079A40 File Offset: 0x00077E40
	private void UpdatetPoints()
	{
		for (int i = 0; i < this.lifeTimes.Count; i++)
		{
			List<float> list;
			int index;
			(list = this.lifeTimes)[index = i] = list[index] - Time.deltaTime;
			if (this.lifeTimes[i] <= 0f)
			{
				int count = this.lifeTimes.Count - i;
				this.lifeTimes.RemoveRange(i, count);
				this.points.RemoveRange(i, count);
				this.velocities.RemoveRange(i, count);
				return;
			}
			this.CalculateTurbuelence(this.points[i], this.TimeScale, this.Frequency, this.Amplitude, this.Gravity, i);
		}
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x00079B00 File Offset: 0x00077F00
	private void UpdateLineRendererBezier()
	{
		if (this.SmoothCurves && this.points.Count > 2)
		{
			this.InterpolateBezier(this.points, 0.5f);
			List<Vector3> drawingPoints = this.GetDrawingPoints();
			this.lineRenderer.positionCount = drawingPoints.Count - 1;
			this.lineRenderer.SetPositions(drawingPoints.ToArray());
		}
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x00079B65 File Offset: 0x00077F65
	private void UpdateLineRenderer()
	{
		this.lineRenderer.positionCount = Mathf.Clamp(this.points.Count - 1, 0, int.MaxValue);
		this.lineRenderer.SetPositions(this.points.ToArray());
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x00079BA0 File Offset: 0x00077FA0
	private void CalculateTurbuelence(Vector3 position, float speed, float scale, float height, float gravity, int index)
	{
		float num = Time.timeSinceLevelLoad * speed + this.randomOffset;
		float x = position.x * scale + num;
		float num2 = position.y * scale + num + 10f;
		float y = position.z * scale + num + 25f;
		position.x = (Mathf.PerlinNoise(num2, y) - 0.5f) * height * Time.deltaTime;
		position.y = (Mathf.PerlinNoise(x, y) - 0.5f) * height * Time.deltaTime - gravity * Time.deltaTime;
		position.z = (Mathf.PerlinNoise(x, num2) - 0.5f) * height * Time.deltaTime;
		List<Vector3> list;
		(list = this.points)[index] = list[index] + position * this.TurbulenceStrength;
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x00079C7C File Offset: 0x0007807C
	public void InterpolateBezier(List<Vector3> segmentPoints, float scale)
	{
		this.controlPoints.Clear();
		if (segmentPoints.Count < 2)
		{
			return;
		}
		for (int i = 0; i < segmentPoints.Count; i++)
		{
			if (i == 0)
			{
				Vector3 vector = segmentPoints[i];
				Vector3 a = segmentPoints[i + 1];
				Vector3 a2 = a - vector;
				Vector3 item = vector + scale * a2;
				this.controlPoints.Add(vector);
				this.controlPoints.Add(item);
			}
			else if (i == segmentPoints.Count - 1)
			{
				Vector3 b = segmentPoints[i - 1];
				Vector3 vector2 = segmentPoints[i];
				Vector3 a3 = vector2 - b;
				Vector3 item2 = vector2 - scale * a3;
				this.controlPoints.Add(item2);
				this.controlPoints.Add(vector2);
			}
			else
			{
				Vector3 b2 = segmentPoints[i - 1];
				Vector3 vector3 = segmentPoints[i];
				Vector3 a4 = segmentPoints[i + 1];
				Vector3 normalized = (a4 - b2).normalized;
				Vector3 item3 = vector3 - scale * normalized * (vector3 - b2).magnitude;
				Vector3 item4 = vector3 + scale * normalized * (a4 - vector3).magnitude;
				this.controlPoints.Add(item3);
				this.controlPoints.Add(vector3);
				this.controlPoints.Add(item4);
			}
		}
		this.curveCount = (this.controlPoints.Count - 1) / 3;
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x00079E24 File Offset: 0x00078224
	public List<Vector3> GetDrawingPoints()
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.curveCount; i++)
		{
			List<Vector3> list2 = this.FindDrawingPoints(i);
			if (i != 0)
			{
				list2.RemoveAt(0);
			}
			list.AddRange(list2);
		}
		return list;
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x00079E6C File Offset: 0x0007826C
	private List<Vector3> FindDrawingPoints(int curveIndex)
	{
		List<Vector3> list = new List<Vector3>();
		Vector3 item = this.CalculateBezierPoint(curveIndex, 0f);
		Vector3 item2 = this.CalculateBezierPoint(curveIndex, 1f);
		list.Add(item);
		list.Add(item2);
		this.FindDrawingPoints(curveIndex, 0f, 1f, list, 1);
		return list;
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x00079EBC File Offset: 0x000782BC
	private int FindDrawingPoints(int curveIndex, float t0, float t1, List<Vector3> pointList, int insertionIndex)
	{
		Vector3 a = this.CalculateBezierPoint(curveIndex, t0);
		Vector3 vector = this.CalculateBezierPoint(curveIndex, t1);
		if ((a - vector).sqrMagnitude < 0.01f)
		{
			return 0;
		}
		float num = (t0 + t1) / 2f;
		Vector3 vector2 = this.CalculateBezierPoint(curveIndex, num);
		Vector3 normalized = (a - vector2).normalized;
		Vector3 normalized2 = (vector - vector2).normalized;
		if (Vector3.Dot(normalized, normalized2) > -0.99f || Mathf.Abs(num - 0.5f) < 0.0001f)
		{
			int num2 = 0;
			num2 += this.FindDrawingPoints(curveIndex, t0, num, pointList, insertionIndex);
			pointList.Insert(insertionIndex + num2, vector2);
			num2++;
			return num2 + this.FindDrawingPoints(curveIndex, num, t1, pointList, insertionIndex + num2);
		}
		return 0;
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x00079F9C File Offset: 0x0007839C
	public Vector3 CalculateBezierPoint(int curveIndex, float t)
	{
		int num = curveIndex * 3;
		Vector3 p = this.controlPoints[num];
		Vector3 p2 = this.controlPoints[num + 1];
		Vector3 p3 = this.controlPoints[num + 2];
		Vector3 p4 = this.controlPoints[num + 3];
		return this.CalculateBezierPoint(t, p, p2, p3, p4);
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x00079FF4 File Offset: 0x000783F4
	private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float d = num3 * num;
		float d2 = num2 * t;
		Vector3 a = d * p0;
		a += 3f * num3 * t * p1;
		a += 3f * num * num2 * p2;
		return a + d2 * p3;
	}

	// Token: 0x0400160A RID: 5642
	[Range(0.01f, 10f)]
	public float MinVertexDistance = 0.1f;

	// Token: 0x0400160B RID: 5643
	public float VertexTime = 1f;

	// Token: 0x0400160C RID: 5644
	public float TotalLifeTime = 3f;

	// Token: 0x0400160D RID: 5645
	public bool SmoothCurves;

	// Token: 0x0400160E RID: 5646
	public bool IsRibbon;

	// Token: 0x0400160F RID: 5647
	public bool IsActive = true;

	// Token: 0x04001610 RID: 5648
	[Range(0.001f, 10f)]
	public float Frequency = 1f;

	// Token: 0x04001611 RID: 5649
	[Range(0.001f, 10f)]
	public float TimeScale = 0.1f;

	// Token: 0x04001612 RID: 5650
	[Range(0.001f, 10f)]
	public float Amplitude = 1f;

	// Token: 0x04001613 RID: 5651
	public float Gravity = 1f;

	// Token: 0x04001614 RID: 5652
	public float TurbulenceStrength = 1f;

	// Token: 0x04001615 RID: 5653
	public bool AutodestructWhenNotActive;

	// Token: 0x04001616 RID: 5654
	private LineRenderer lineRenderer;

	// Token: 0x04001617 RID: 5655
	private Transform t;

	// Token: 0x04001618 RID: 5656
	private Vector3 prevPos;

	// Token: 0x04001619 RID: 5657
	private List<Vector3> points = new List<Vector3>(500);

	// Token: 0x0400161A RID: 5658
	private List<float> lifeTimes = new List<float>(500);

	// Token: 0x0400161B RID: 5659
	private List<Vector3> velocities = new List<Vector3>(500);

	// Token: 0x0400161C RID: 5660
	private float randomOffset;

	// Token: 0x0400161D RID: 5661
	private List<Vector3> controlPoints = new List<Vector3>();

	// Token: 0x0400161E RID: 5662
	private int curveCount;

	// Token: 0x0400161F RID: 5663
	private const float MinimumSqrDistance = 0.01f;

	// Token: 0x04001620 RID: 5664
	private const float DivisionThreshold = -0.99f;

	// Token: 0x04001621 RID: 5665
	private const float SmoothCurvesScale = 0.5f;
}
