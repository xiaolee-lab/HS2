using System;
using UnityEngine;

namespace PicoGames.Utilities
{
	// Token: 0x02000A73 RID: 2675
	public class Shape
	{
		// Token: 0x06004F3C RID: 20284 RVA: 0x001E7260 File Offset: 0x001E5660
		public static Vector3[] GetSquare(float _centerScale = 2f)
		{
			return Shape.GetPolygon(4, _centerScale);
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x001E7269 File Offset: 0x001E5669
		public static Vector3[] GetPentagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(5, _centerScale);
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x001E7272 File Offset: 0x001E5672
		public static Vector3[] GetHexagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(6, _centerScale);
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x001E727B File Offset: 0x001E567B
		public static Vector3[] GetHeptagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(7, _centerScale);
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x001E7284 File Offset: 0x001E5684
		public static Vector3[] GetOctagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(8, _centerScale);
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x001E728D File Offset: 0x001E568D
		public static Vector3[] GetNonagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(9, _centerScale);
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x001E7297 File Offset: 0x001E5697
		public static Vector3[] GetDecagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(10, _centerScale);
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x001E72A1 File Offset: 0x001E56A1
		public static Vector3[] GetDodecagon(float _centerScale = 2f)
		{
			return Shape.GetPolygon(12, _centerScale);
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x001E72AB File Offset: 0x001E56AB
		public static Vector3[] GetPolygon(int _sides, float _centerScale = 2f)
		{
			return Shape.GetRoseCurve(_sides, 1, _centerScale, true);
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x001E72B6 File Offset: 0x001E56B6
		public static Vector3[] GetStar(float _centerScale = 2f)
		{
			return Shape.GetRoseCurve(5, 2, _centerScale, true);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x001E72C4 File Offset: 0x001E56C4
		public static Vector3[] GetRoseCurve(int _points, int _detail, float _centerScale, bool _unitize)
		{
			_points = Mathf.Max(3, _points);
			_detail = Mathf.Max(1, _detail);
			Vector3[] array = new Vector3[_points * _detail];
			int num = _points;
			Vector3 vector = Vector3.one * float.MaxValue;
			Vector3 vector2 = Vector3.one * float.MinValue;
			for (int i = 0; i < array.Length; i++)
			{
				float num2 = (float)i * (6.2831855f / (float)array.Length);
				float num3 = Mathf.Cos(num2 * (float)num) + _centerScale;
				array[i] = new Vector3(num3 * Mathf.Cos(num2), num3 * Mathf.Sin(num2), 0f);
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			if (_unitize)
			{
				Shape.Unitize(ref array, vector, vector2);
			}
			return array;
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x001E73A8 File Offset: 0x001E57A8
		public static void Unitize(ref Vector3[] _points, Vector3 _min, Vector3 _max)
		{
			float d = Vector3.Distance(_min, _max) / 1.4142135f;
			for (int i = 0; i < _points.Length; i++)
			{
				_points[i] /= d;
			}
		}

		// Token: 0x04004857 RID: 18519
		private const float SQR_TWO = 1.4142135f;
	}
}
