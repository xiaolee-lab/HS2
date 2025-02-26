using System;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020003F7 RID: 1015
	public static class VectorClampingUtility
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x00071518 File Offset: 0x0006F918
		public static void ClampVector(ref Vector2 vector, float minX, float maxX, float minY, float maxY)
		{
			vector.x = Mathf.Clamp(vector.x, minX, maxX);
			vector.y = Mathf.Clamp(vector.y, minY, maxY);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00071541 File Offset: 0x0006F941
		public static void ClampVector(ref Vector3 vector, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
		{
			vector.x = Mathf.Clamp(vector.x, minX, maxX);
			vector.y = Mathf.Clamp(vector.y, minY, maxY);
			vector.z = Mathf.Clamp(vector.z, minZ, maxZ);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00071580 File Offset: 0x0006F980
		public static void ClampVector(ref Vector4 vector, float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float minW, float maxW)
		{
			vector.x = Mathf.Clamp(vector.x, minX, maxX);
			vector.y = Mathf.Clamp(vector.y, minY, maxY);
			vector.z = Mathf.Clamp(vector.z, minZ, maxZ);
			vector.w = Mathf.Clamp(vector.w, minW, maxW);
		}
	}
}
