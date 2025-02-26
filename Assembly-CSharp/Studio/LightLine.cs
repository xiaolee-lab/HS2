using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001267 RID: 4711
	public static class LightLine
	{
		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06009BE0 RID: 39904 RVA: 0x003FBDD5 File Offset: 0x003FA1D5
		// (set) Token: 0x06009BE1 RID: 39905 RVA: 0x003FBDDC File Offset: 0x003FA1DC
		public static Shader shader { get; set; }

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06009BE2 RID: 39906 RVA: 0x003FBDE4 File Offset: 0x003FA1E4
		public static Material material
		{
			get
			{
				if (LightLine.m_Material == null)
				{
					LightLine.CreateMaterial();
				}
				return LightLine.m_Material;
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x06009BE3 RID: 39907 RVA: 0x003FBE00 File Offset: 0x003FA200
		// (set) Token: 0x06009BE4 RID: 39908 RVA: 0x003FBE07 File Offset: 0x003FA207
		public static Color color
		{
			get
			{
				return LightLine.m_Color;
			}
			set
			{
				LightLine.m_Color = value;
			}
		}

		// Token: 0x06009BE5 RID: 39909 RVA: 0x003FBE10 File Offset: 0x003FA210
		public static void DrawLine(Light _light)
		{
			LightType type = _light.type;
			if (type != LightType.Point)
			{
				if (type == LightType.Spot)
				{
					LightLine.m_Color = _light.color;
					LightLine.DrawSpotLight(_light.transform.rotation, _light.transform.position, _light.spotAngle, _light.range, 1f, 1f);
				}
			}
			else
			{
				LightLine.m_Color = _light.color;
				LightLine.DrawPointLight(Quaternion.identity, _light.transform.position, _light.range);
			}
		}

		// Token: 0x06009BE6 RID: 39910 RVA: 0x003FBEA4 File Offset: 0x003FA2A4
		private static void DrawPointLight(Quaternion _rotation, Vector3 _position, float _radius)
		{
			Vector3[] array = new Vector3[]
			{
				_rotation * Vector3.right,
				_rotation * Vector3.up,
				_rotation * Vector3.forward
			};
			if (Camera.current.orthographic)
			{
				Vector3 forward = Camera.current.transform.forward;
				LightLine.DrawWireDisc(_position, forward, _radius);
				for (int i = 0; i < 3; i++)
				{
					Vector3 normalized = Vector3.Cross(array[i], forward).normalized;
					LightLine.DrawTwoShadedWireDisc(_position, array[i], normalized, 180f, _radius);
				}
			}
			else
			{
				Vector3 vector = _position - Camera.current.transform.position;
				float sqrMagnitude = vector.sqrMagnitude;
				float num = _radius * _radius;
				float num2 = num * num / sqrMagnitude;
				float num3 = num2 / num;
				if (num3 < 1f)
				{
					LightLine.DrawWireDisc(_position - num * vector / sqrMagnitude, vector, Mathf.Sqrt(num - num2));
				}
				for (int j = 0; j < 3; j++)
				{
					if (num3 < 1f)
					{
						float num4 = Vector3.Angle(vector, array[j]);
						num4 = 90f - Mathf.Min(num4, 180f - num4);
						float num5 = Mathf.Tan(num4 * 0.017453292f);
						float num6 = Mathf.Sqrt(num2 + num5 * num5 * num2) / _radius;
						if (num6 < 1f)
						{
							float num7 = Mathf.Asin(num6) * 57.29578f;
							Vector3 vector2 = Vector3.Cross(array[j], vector).normalized;
							vector2 = Quaternion.AngleAxis(num7, array[j]) * vector2;
							LightLine.DrawTwoShadedWireDisc(_position, array[j], vector2, (90f - num7) * 2f, _radius);
						}
						else
						{
							LightLine.DrawTwoShadedWireDisc(_position, array[j], _radius);
						}
					}
					else
					{
						LightLine.DrawTwoShadedWireDisc(_position, array[j], _radius);
					}
				}
			}
		}

		// Token: 0x06009BE7 RID: 39911 RVA: 0x003FC0F8 File Offset: 0x003FA4F8
		private static void DrawSpotLight(Quaternion _rotation, Vector3 _position, float _angle, float _range, float _angleScale, float _rangeScale)
		{
			float num = _range * _rangeScale;
			float num2 = num * Mathf.Tan(0.017453292f * _angle / 2f) * _angleScale;
			Vector3 vector = _rotation * Vector3.forward;
			Vector3 a = _rotation * Vector3.up;
			Vector3 a2 = _rotation * Vector3.right;
			LightLine.DrawLine(_position, _position + vector * num + a * num2);
			LightLine.DrawLine(_position, _position + vector * num - a * num2);
			LightLine.DrawLine(_position, _position + vector * num + a2 * num2);
			LightLine.DrawLine(_position, _position + vector * num - a2 * num2);
			LightLine.DrawWireDisc(_position + num * vector, vector, num2);
		}

		// Token: 0x06009BE8 RID: 39912 RVA: 0x003FC1DC File Offset: 0x003FA5DC
		public static void DrawWireDisc(Vector3 _center, Vector3 _normal, float _radius)
		{
			Vector3 from = Vector3.Cross(_normal, Vector3.up);
			if (from.sqrMagnitude < 0.001f)
			{
				from = Vector3.Cross(_normal, Vector3.right);
			}
			LightLine.DrawWireArc(_center, _normal, from, 360f, _radius);
		}

		// Token: 0x06009BE9 RID: 39913 RVA: 0x003FC220 File Offset: 0x003FA620
		public static void DrawWireArc(Vector3 _center, Vector3 _normal, Vector3 _from, float _angle, float _radius)
		{
			Vector3[] array = new Vector3[60];
			LightLine.SetDiscSectionPoints(array, 60, _center, _normal, _from, _angle, _radius);
			LightLine.DrawPolyLine(array);
		}

		// Token: 0x06009BEA RID: 39914 RVA: 0x003FC24C File Offset: 0x003FA64C
		public static void DrawPolyLine(params Vector3[] _points)
		{
			if (!LightLine.BeginLineDrawing(Matrix4x4.identity))
			{
				return;
			}
			for (int i = 1; i < _points.Length; i++)
			{
				GL.Vertex(_points[i]);
				GL.Vertex(_points[i - 1]);
			}
			LightLine.EndLineDrawing();
		}

		// Token: 0x06009BEB RID: 39915 RVA: 0x003FC2A6 File Offset: 0x003FA6A6
		public static void DrawLine(Vector3 p1, Vector3 p2)
		{
			if (!LightLine.BeginLineDrawing(Matrix4x4.identity))
			{
				return;
			}
			GL.Vertex(p1);
			GL.Vertex(p2);
			LightLine.EndLineDrawing();
		}

		// Token: 0x06009BEC RID: 39916 RVA: 0x003FC2CC File Offset: 0x003FA6CC
		private static void DrawTwoShadedWireDisc(Vector3 _position, Vector3 _axis, Vector3 _from, float _degrees, float _radius)
		{
			LightLine.DrawWireArc(_position, _axis, _from, _degrees, _radius);
			Color color = LightLine.m_Color;
			Color color2 = color;
			color.a *= LightLine.backfaceAlphaMultiplier;
			LightLine.m_Color = color;
			LightLine.DrawWireArc(_position, _axis, _from, _degrees - 360f, _radius);
			LightLine.m_Color = color2;
		}

		// Token: 0x06009BED RID: 39917 RVA: 0x003FC31C File Offset: 0x003FA71C
		private static void DrawTwoShadedWireDisc(Vector3 position, Vector3 axis, float radius)
		{
			Color color = LightLine.m_Color;
			Color color2 = color;
			color.a *= LightLine.backfaceAlphaMultiplier;
			LightLine.m_Color = color;
			LightLine.DrawWireDisc(position, axis, radius);
			LightLine.m_Color = color2;
		}

		// Token: 0x06009BEE RID: 39918 RVA: 0x003FC358 File Offset: 0x003FA758
		private static void SetDiscSectionPoints(Vector3[] _dest, int _count, Vector3 _center, Vector3 _normal, Vector3 _from, float _angle, float _radius)
		{
			_from.Normalize();
			Quaternion rotation = Quaternion.AngleAxis(_angle / (float)(_count - 1), _normal);
			Vector3 vector = _from * _radius;
			for (int i = 0; i < _count; i++)
			{
				_dest[i] = _center + vector;
				vector = rotation * vector;
			}
		}

		// Token: 0x06009BEF RID: 39919 RVA: 0x003FC3B4 File Offset: 0x003FA7B4
		private static bool BeginLineDrawing(Matrix4x4 matrix)
		{
			if (Event.current.type != EventType.Repaint)
			{
				return false;
			}
			Color value = LightLine.m_Color * LightLine.lineTransparency;
			LightLine.material.SetPass(0);
			LightLine.material.SetColor("_Color", value);
			GL.PushMatrix();
			GL.MultMatrix(matrix);
			GL.Begin(1);
			return true;
		}

		// Token: 0x06009BF0 RID: 39920 RVA: 0x003FC411 File Offset: 0x003FA811
		private static void EndLineDrawing()
		{
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06009BF1 RID: 39921 RVA: 0x003FC420 File Offset: 0x003FA820
		private static void CreateMaterial()
		{
			Shader shader = (!(LightLine.shader == null)) ? LightLine.shader : Shader.Find("Custom/LightLine");
			if (shader == null)
			{
				return;
			}
			LightLine.m_Material = new Material(shader);
		}

		// Token: 0x04007C36 RID: 31798
		private static Material m_Material = null;

		// Token: 0x04007C37 RID: 31799
		private static Color m_Color = Color.white;

		// Token: 0x04007C38 RID: 31800
		private static float backfaceAlphaMultiplier = 0.2f;

		// Token: 0x04007C39 RID: 31801
		private static Color lineTransparency = new Color(1f, 1f, 1f, 0.75f);
	}
}
