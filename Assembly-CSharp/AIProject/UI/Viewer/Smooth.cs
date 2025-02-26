using System;
using UnityEngine;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EC1 RID: 3777
	public static class Smooth
	{
		// Token: 0x06007BC7 RID: 31687 RVA: 0x00344607 File Offset: 0x00342A07
		public static float Damp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x0034461C File Offset: 0x00342A1C
		public static Vector2 Damp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
		{
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x00344631 File Offset: 0x00342A31
		public static Vector3 Damp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, float.PositiveInfinity, Time.unscaledDeltaTime);
		}
	}
}
