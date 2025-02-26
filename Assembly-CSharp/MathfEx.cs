using System;
using UnityEngine;

// Token: 0x0200118D RID: 4493
public static class MathfEx
{
	// Token: 0x06009419 RID: 37913 RVA: 0x003D324F File Offset: 0x003D164F
	public static float LerpAccel(float from, float to, float t)
	{
		return Mathf.Lerp(from, to, Mathf.Sqrt(t));
	}

	// Token: 0x0600941A RID: 37914 RVA: 0x003D325E File Offset: 0x003D165E
	public static bool IsRange<T>(T min, T n, T max, bool isEqual) where T : IComparable
	{
		return (!isEqual) ? MathfEx.RangeEqualOff<T>(min, n, max) : MathfEx.RangeEqualOn<T>(min, n, max);
	}

	// Token: 0x0600941B RID: 37915 RVA: 0x003D327B File Offset: 0x003D167B
	public static bool RangeEqualOn<T>(T min, T n, T max) where T : IComparable
	{
		return n.CompareTo(max) <= 0 && n.CompareTo(min) >= 0;
	}

	// Token: 0x0600941C RID: 37916 RVA: 0x003D32B2 File Offset: 0x003D16B2
	public static bool RangeEqualOff<T>(T min, T n, T max) where T : IComparable
	{
		return n.CompareTo(max) < 0 && n.CompareTo(min) > 0;
	}

	// Token: 0x0600941D RID: 37917 RVA: 0x003D32E6 File Offset: 0x003D16E6
	public static float LerpBrake(float from, float to, float t)
	{
		return Mathf.Lerp(from, to, t * (2f - t));
	}

	// Token: 0x0600941E RID: 37918 RVA: 0x003D32F8 File Offset: 0x003D16F8
	public static int LoopValue(ref int value, int start, int end)
	{
		if (value > end)
		{
			value = start;
		}
		else if (value < start)
		{
			value = end;
		}
		return value;
	}

	// Token: 0x0600941F RID: 37919 RVA: 0x003D3317 File Offset: 0x003D1717
	public static int LoopValue(int value, int start, int end)
	{
		return MathfEx.LoopValue(ref value, start, end);
	}

	// Token: 0x06009420 RID: 37920 RVA: 0x003D3324 File Offset: 0x003D1724
	public static Rect AspectRect(float baseH = 1280f, float rate = 720f)
	{
		float y = ((float)Screen.height - (float)Screen.width / baseH * rate) * 0.5f / (float)Screen.height;
		float height = rate * (float)Screen.width / baseH / (float)Screen.height;
		return new Rect(0f, y, 1f, height);
	}

	// Token: 0x06009421 RID: 37921 RVA: 0x003D3373 File Offset: 0x003D1773
	public static long Min(long _a, long _b)
	{
		return (_a <= _b) ? _a : _b;
	}

	// Token: 0x06009422 RID: 37922 RVA: 0x003D3383 File Offset: 0x003D1783
	public static long Max(long _a, long _b)
	{
		return (_a <= _b) ? _b : _a;
	}

	// Token: 0x06009423 RID: 37923 RVA: 0x003D3393 File Offset: 0x003D1793
	public static long Clamp(long _value, long _min, long _max)
	{
		return MathfEx.Min(MathfEx.Max(_value, _min), _max);
	}

	// Token: 0x06009424 RID: 37924 RVA: 0x003D33A2 File Offset: 0x003D17A2
	public static float ToRadian(float degree)
	{
		return degree * 0.017453292f;
	}

	// Token: 0x06009425 RID: 37925 RVA: 0x003D33AB File Offset: 0x003D17AB
	public static float ToDegree(float radian)
	{
		return radian * 57.29578f;
	}

	// Token: 0x06009426 RID: 37926 RVA: 0x003D33B4 File Offset: 0x003D17B4
	public static Vector3 GetShapeLerpPositionValue(float shape, Vector3 min, Vector3 max)
	{
		return (shape < 0.5f) ? Vector3.Lerp(min, Vector3.zero, Mathf.InverseLerp(0f, 0.5f, shape)) : Vector3.Lerp(Vector3.zero, max, Mathf.InverseLerp(0.5f, 1f, shape));
	}

	// Token: 0x06009427 RID: 37927 RVA: 0x003D3408 File Offset: 0x003D1808
	public static Vector3 GetShapeLerpAngleValue(float shape, Vector3 min, Vector3 max)
	{
		Vector3 zero = Vector3.zero;
		if (shape >= 0.5f)
		{
			float t = Mathf.InverseLerp(0.5f, 1f, shape);
			for (int i = 0; i < 3; i++)
			{
				zero[i] = Mathf.LerpAngle(0f, max[i], t);
			}
		}
		else
		{
			float t2 = Mathf.InverseLerp(0f, 0.5f, shape);
			for (int j = 0; j < 3; j++)
			{
				zero[j] = Mathf.LerpAngle(min[j], 0f, t2);
			}
		}
		return zero;
	}
}
