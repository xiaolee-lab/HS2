using System;
using UnityEngine;

namespace IllusionUtility.SetUtility
{
	// Token: 0x020011A2 RID: 4514
	public static class TransformScaleEx
	{
		// Token: 0x06009487 RID: 38023 RVA: 0x003D4A64 File Offset: 0x003D2E64
		public static void SetLocalScaleX(this Transform transform, float x)
		{
			Vector3 localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
			transform.localScale = localScale;
		}

		// Token: 0x06009488 RID: 38024 RVA: 0x003D4A9C File Offset: 0x003D2E9C
		public static void SetLocalScaleY(this Transform transform, float y)
		{
			Vector3 localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
			transform.localScale = localScale;
		}

		// Token: 0x06009489 RID: 38025 RVA: 0x003D4AD4 File Offset: 0x003D2ED4
		public static void SetLocalScaleZ(this Transform transform, float z)
		{
			Vector3 localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
			transform.localScale = localScale;
		}

		// Token: 0x0600948A RID: 38026 RVA: 0x003D4B0C File Offset: 0x003D2F0C
		public static void SetLocalScale(this Transform transform, float x, float y, float z)
		{
			Vector3 localScale = new Vector3(x, y, z);
			transform.localScale = localScale;
		}
	}
}
