using System;
using UnityEngine;

namespace IllusionUtility.SetUtility
{
	// Token: 0x020011A0 RID: 4512
	public static class TransformPositionEx
	{
		// Token: 0x06009477 RID: 38007 RVA: 0x003D46AC File Offset: 0x003D2AAC
		public static void SetPositionX(this Transform transform, float x)
		{
			Vector3 position = new Vector3(x, transform.position.y, transform.position.z);
			transform.position = position;
		}

		// Token: 0x06009478 RID: 38008 RVA: 0x003D46E4 File Offset: 0x003D2AE4
		public static void SetPositionY(this Transform transform, float y)
		{
			Vector3 position = new Vector3(transform.position.x, y, transform.position.z);
			transform.position = position;
		}

		// Token: 0x06009479 RID: 38009 RVA: 0x003D471C File Offset: 0x003D2B1C
		public static void SetPositionZ(this Transform transform, float z)
		{
			Vector3 position = new Vector3(transform.position.x, transform.position.y, z);
			transform.position = position;
		}

		// Token: 0x0600947A RID: 38010 RVA: 0x003D4754 File Offset: 0x003D2B54
		public static void SetPosition(this Transform transform, float x, float y, float z)
		{
			Vector3 position = new Vector3(x, y, z);
			transform.position = position;
		}

		// Token: 0x0600947B RID: 38011 RVA: 0x003D4774 File Offset: 0x003D2B74
		public static void SetLocalPositionX(this Transform transform, float x)
		{
			Vector3 localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
			transform.localPosition = localPosition;
		}

		// Token: 0x0600947C RID: 38012 RVA: 0x003D47AC File Offset: 0x003D2BAC
		public static void SetLocalPositionY(this Transform transform, float y)
		{
			Vector3 localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
			transform.localPosition = localPosition;
		}

		// Token: 0x0600947D RID: 38013 RVA: 0x003D47E4 File Offset: 0x003D2BE4
		public static void SetLocalPositionZ(this Transform transform, float z)
		{
			Vector3 localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
			transform.localPosition = localPosition;
		}

		// Token: 0x0600947E RID: 38014 RVA: 0x003D481C File Offset: 0x003D2C1C
		public static void SetLocalPosition(this Transform transform, float x, float y, float z)
		{
			Vector3 localPosition = new Vector3(x, y, z);
			transform.localPosition = localPosition;
		}
	}
}
