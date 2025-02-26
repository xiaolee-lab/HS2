using System;
using System.Reflection;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003E2 RID: 994
	public static class GeomUtil
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00068064 File Offset: 0x00066464
		public static void CalculateFrustumPlanes(Plane[] planes, Matrix4x4 worldToProjectMatrix)
		{
			if (GeomUtil._calculateFrustumPlanes_Imp == null)
			{
				MethodInfo method = typeof(GeometryUtility).GetMethod("Internal_ExtractPlanes", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[]
				{
					typeof(Plane[]),
					typeof(Matrix4x4)
				}, null);
				if (method == null)
				{
					throw new Exception("Failed to reflect internal method. Your Unity version may not contain the presumed named method in GeometryUtility.");
				}
				GeomUtil._calculateFrustumPlanes_Imp = (Delegate.CreateDelegate(typeof(Action<Plane[], Matrix4x4>), method) as Action<Plane[], Matrix4x4>);
				if (GeomUtil._calculateFrustumPlanes_Imp == null)
				{
					throw new Exception("Failed to reflect internal method. Your Unity version may not contain the presumed named method in GeometryUtility.");
				}
			}
			GeomUtil._calculateFrustumPlanes_Imp(planes, worldToProjectMatrix);
		}

		// Token: 0x04001390 RID: 5008
		private static Action<Plane[], Matrix4x4> _calculateFrustumPlanes_Imp;
	}
}
