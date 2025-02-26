using System;
using System.Collections.Generic;
using UnityEngine;

namespace PicoGames.Utilities
{
	// Token: 0x02000A75 RID: 2677
	[AddComponentMenu("PicoGames/Utilities/Spline")]
	[Serializable]
	public class Spline : MonoBehaviour
	{
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x001E74E2 File Offset: 0x001E58E2
		public float CurveLength
		{
			get
			{
				if (this.curveLength < 0f)
				{
					this.UpdateCurveLength(1000);
				}
				return this.curveLength;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x001E7505 File Offset: 0x001E5905
		public int ControlCount
		{
			get
			{
				return (this.points.Count - 1) / 3;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x001E7516 File Offset: 0x001E5916
		public Vector3[] Points
		{
			get
			{
				return this.points.ToArray();
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x001E7523 File Offset: 0x001E5923
		// (set) Token: 0x06004F4E RID: 20302 RVA: 0x001E752C File Offset: 0x001E592C
		public bool IsLooped
		{
			get
			{
				return this.isLooped;
			}
			set
			{
				if (value != this.isLooped)
				{
					this.hasChanged = true;
					this.isLooped = value;
					if (value)
					{
						if (this.ControlCount < 2)
						{
							this.AddCurve(0, Spline.ControlPointMode.Mirrored);
							Vector3 vector = this.GetPoint(1, Space.Self) - this.GetPoint(0, Space.Self);
							this.SetControlPoint(1, this.GetControlPoint(0, Space.Self) - new Vector3(vector.y, -vector.x, 0f) * 1.5f, Space.Self);
							this.SetPoint(4, this.GetPoint(this.points.Count - 2, Space.Self) - new Vector3(vector.y, -vector.x, 0f) * 1.5f, Space.Self);
						}
						this.evenlyDistributPoints = true;
						this.modes[0] = this.modes[this.modes.Count - 1];
						this.SetPoint(this.points.Count - 1, this.points[0], Space.Self);
					}
				}
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x001E764A File Offset: 0x001E5A4A
		// (set) Token: 0x06004F50 RID: 20304 RVA: 0x001E7652 File Offset: 0x001E5A52
		public bool EvenPointDistribution
		{
			get
			{
				return this.evenlyDistributPoints;
			}
			set
			{
				this.evenlyDistributPoints = (this.isLooped || value);
			}
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x001E766C File Offset: 0x001E5A6C
		public void Reset()
		{
			this.points = new List<Vector3>(new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, -4f, 0f),
				new Vector3(0f, -5f, 0f)
			});
			this.modes = new List<Spline.ControlPointMode>(new Spline.ControlPointMode[]
			{
				Spline.ControlPointMode.Mirrored,
				Spline.ControlPointMode.Mirrored
			});
			this.UpdateCurveLength(1000);
			this.hasChanged = true;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x001E7738 File Offset: 0x001E5B38
		public void AddCurve(int _atIndex, Spline.ControlPointMode _defaultMode = Spline.ControlPointMode.Mirrored)
		{
			Vector3 vector = this.points[_atIndex * 3];
			Vector3 vector2 = this.points[(_atIndex + 1) * 3];
			Vector3 vector3 = (vector + vector2) * 0.5f;
			Vector3 normalized = (vector2 - vector).normalized;
			this.points.InsertRange(_atIndex * 3 + 2, new Vector3[]
			{
				vector3 - normalized,
				vector3,
				vector3 + normalized
			});
			this.modes.Insert(_atIndex, _defaultMode);
			this.EnforceMode(_atIndex);
			if (this.isLooped)
			{
				this.points[this.points.Count - 1] = this.points[0];
				this.modes[this.modes.Count - 1] = this.modes[0];
				this.EnforceMode(0);
			}
			this.hasChanged = true;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x001E7846 File Offset: 0x001E5C46
		public void RemoveCurve(int _index)
		{
			if (_index == 0 || _index == this.ControlCount)
			{
				return;
			}
			this.points.RemoveRange(_index * 3 - 1, 3);
			this.modes.RemoveAt(_index);
			this.hasChanged = true;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x001E787F File Offset: 0x001E5C7F
		public Spline.ControlPointMode GetMode(int _index)
		{
			return this.modes[(_index + 1) / 3];
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x001E7894 File Offset: 0x001E5C94
		public void SetMode(int _index, Spline.ControlPointMode _mode)
		{
			int num = (_index + 1) / 3;
			this.modes[num] = _mode;
			if (this.isLooped)
			{
				if (num == 0)
				{
					this.modes[this.modes.Count - 1] = _mode;
				}
				else if (num == this.modes.Count - 1)
				{
					this.modes[0] = _mode;
				}
			}
			this.EnforceMode(_index);
			this.hasChanged = true;
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x001E7914 File Offset: 0x001E5D14
		private void EnforceMode(int _index)
		{
			int num = (_index + 1) / 3;
			Spline.ControlPointMode controlPointMode = this.modes[num];
			if (controlPointMode == Spline.ControlPointMode.Free || (!this.isLooped && (num == 0 || num == this.modes.Count - 1)))
			{
				return;
			}
			int num2 = num * 3;
			int num3;
			int num4;
			if (_index <= num2)
			{
				num3 = num2 - 1;
				if (num3 < 0)
				{
					num3 = this.points.Count - 2;
				}
				num4 = num2 + 1;
				if (num4 >= this.points.Count - 2)
				{
					num4 = 1;
				}
			}
			else
			{
				num3 = num2 + 1;
				if (num3 >= this.points.Count)
				{
					num3 = 1;
				}
				num4 = num2 - 1;
				if (num4 < 0)
				{
					num4 = this.points.Count - 2;
				}
			}
			Vector3 a = this.points[num2];
			Vector3 b = a - this.points[num3];
			if (controlPointMode == Spline.ControlPointMode.Aligned)
			{
				b = b.normalized * Vector3.Distance(a, this.points[num4]);
			}
			this.points[num4] = a + b;
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x001E7A39 File Offset: 0x001E5E39
		public Vector3 GetControlPoint(int _index, Space _space = Space.Self)
		{
			return this.GetPoint(_index * 3, _space);
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x001E7A45 File Offset: 0x001E5E45
		public void SetControlPoint(int _index, Vector3 _position, Space _space = Space.Self)
		{
			this.SetPoint(_index * 3, _position, _space);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x001E7A54 File Offset: 0x001E5E54
		public void SetPoint(int _index, Vector3 _position, Space _space = Space.Self)
		{
			if (_space == Space.World)
			{
				_position = base.transform.InverseTransformPoint(_position);
			}
			if (_index % 3 == 0)
			{
				Vector3 b = _position - this.points[_index];
				if (this.isLooped)
				{
					if (_index == 0)
					{
						List<Vector3> list;
						(list = this.points)[1] = list[1] + b;
						int index;
						(list = this.points)[index = this.points.Count - 2] = list[index] + b;
						this.points[this.points.Count - 1] = _position;
					}
					else if (_index == this.points.Count - 1)
					{
						this.points[0] = _position;
						List<Vector3> list;
						(list = this.points)[1] = list[1] + b;
						int index2;
						(list = this.points)[index2 = _index - 1] = list[index2] + b;
					}
					else
					{
						List<Vector3> list;
						int index3;
						(list = this.points)[index3 = _index - 1] = list[index3] + b;
						int index4;
						(list = this.points)[index4 = _index + 1] = list[index4] + b;
					}
				}
				else
				{
					if (_index > 0)
					{
						List<Vector3> list;
						int index5;
						(list = this.points)[index5 = _index - 1] = list[index5] + b;
					}
					if (_index + 1 < this.points.Count)
					{
						List<Vector3> list;
						int index6;
						(list = this.points)[index6 = _index + 1] = list[index6] + b;
					}
				}
			}
			this.points[_index] = _position;
			this.EnforceMode(_index);
			this.UpdateCurveLength(1000);
			this.hasChanged = true;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x001E7C2B File Offset: 0x001E602B
		public Vector3 GetPoint(int _index, Space _space = Space.Self)
		{
			return (_space != Space.World) ? this.points[_index] : base.transform.TransformPoint(this.points[_index]);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x001E7C5C File Offset: 0x001E605C
		public Vector3 GetPointOnCurve(float _t)
		{
			int num;
			if (_t >= 1f)
			{
				_t = 1f;
				num = this.points.Count - 4;
			}
			else
			{
				_t = Mathf.Clamp01(_t) * (float)((this.points.Count - 1) / 3);
				num = (int)_t;
				_t -= (float)num;
				num *= 3;
			}
			return Spline.GetBezierPoint(this.points[num], this.points[num + 1], this.points[num + 2], this.points[num + 3], _t);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x001E7CF0 File Offset: 0x001E60F0
		public SplinePoint[] GetSpacedPointsReversed(float _spacing)
		{
			List<SplinePoint> list = new List<SplinePoint>();
			Vector3 vector = this.GetPointOnCurve(1f);
			float num = _spacing * _spacing;
			float num2 = 1f / (float)this.outputResolution;
			list.Add(new SplinePoint(vector, Quaternion.identity));
			for (float num3 = 1f; num3 >= 0f; num3 -= num2)
			{
				Vector3 pointOnCurve = this.GetPointOnCurve(num3);
				if (Vector3.SqrMagnitude(pointOnCurve - vector) >= num)
				{
					vector = pointOnCurve;
					list.Add(new SplinePoint(vector, Quaternion.identity));
				}
			}
			if (list.Count <= 1)
			{
				return new SplinePoint[0];
			}
			vector = this.GetPointOnCurve(0f);
			float num4 = Vector3.Distance(vector, list[list.Count - 1].position) / (float)list.Count;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Vector3 normalized = (vector - list[i].position).normalized;
				if (this.evenlyDistributPoints)
				{
					list[i].position += num4 * (float)i * normalized;
				}
				list[i].rotation = Quaternion.FromToRotation(Vector3.up, normalized);
				vector = list[i].position;
			}
			return list.ToArray();
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x001E7E5C File Offset: 0x001E625C
		private void UpdateCurveLength(int _resolution = 1000)
		{
			float num = 1f / (float)_resolution;
			Vector3 a = this.GetPointOnCurve(0f);
			this.curveLength = 0f;
			for (int i = 1; i <= _resolution; i++)
			{
				Vector3 pointOnCurve = this.GetPointOnCurve((float)i * num);
				this.curveLength += Vector3.Distance(a, pointOnCurve);
				a = pointOnCurve;
			}
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x001E7EBC File Offset: 0x001E62BC
		public static Vector3 GetBezierPoint(Vector3 _p0, Vector3 _p1, Vector3 _p2, Vector3 _p3, float _t)
		{
			_t = Mathf.Clamp01(_t);
			float num = 1f - _t;
			return num * num * num * _p0 + 3f * num * num * _t * _p1 + 3f * num * _t * _t * _p2 + _t * _t * _t * _p3;
		}

		// Token: 0x0400485A RID: 18522
		[SerializeField]
		public int outputResolution = 1000;

		// Token: 0x0400485B RID: 18523
		[SerializeField]
		[HideInInspector]
		public bool hasChanged;

		// Token: 0x0400485C RID: 18524
		[SerializeField]
		[HideInInspector]
		private List<Vector3> points = new List<Vector3>(new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, -1f, 0f),
			new Vector3(0f, -4f, 0f),
			new Vector3(0f, -5f, 0f)
		});

		// Token: 0x0400485D RID: 18525
		[SerializeField]
		[HideInInspector]
		private List<Spline.ControlPointMode> modes = new List<Spline.ControlPointMode>(new Spline.ControlPointMode[]
		{
			Spline.ControlPointMode.Mirrored,
			Spline.ControlPointMode.Mirrored
		});

		// Token: 0x0400485E RID: 18526
		[SerializeField]
		private bool isLooped;

		// Token: 0x0400485F RID: 18527
		[SerializeField]
		private bool evenlyDistributPoints = true;

		// Token: 0x04004860 RID: 18528
		[SerializeField]
		[HideInInspector]
		private float curveLength = -1f;

		// Token: 0x02000A76 RID: 2678
		public enum ControlPointMode
		{
			// Token: 0x04004862 RID: 18530
			Free,
			// Token: 0x04004863 RID: 18531
			Aligned,
			// Token: 0x04004864 RID: 18532
			Mirrored
		}
	}
}
