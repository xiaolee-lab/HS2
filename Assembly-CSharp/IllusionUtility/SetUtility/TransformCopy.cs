using System;
using UnityEngine;

namespace IllusionUtility.SetUtility
{
	// Token: 0x020011A3 RID: 4515
	public static class TransformCopy
	{
		// Token: 0x0600948B RID: 38027 RVA: 0x003D4B2A File Offset: 0x003D2F2A
		public static void CopyPosRotScl(this Transform dst, Transform src)
		{
			dst.localPosition = src.localPosition;
			dst.localRotation = src.localRotation;
			dst.localScale = src.localScale;
			dst.position = src.position;
			dst.rotation = src.rotation;
		}

		// Token: 0x0600948C RID: 38028 RVA: 0x003D4B68 File Offset: 0x003D2F68
		public static void Identity(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
		}
	}
}
