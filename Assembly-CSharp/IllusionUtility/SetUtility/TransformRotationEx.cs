using System;
using UnityEngine;

namespace IllusionUtility.SetUtility
{
	// Token: 0x020011A1 RID: 4513
	public static class TransformRotationEx
	{
		// Token: 0x0600947F RID: 38015 RVA: 0x003D483C File Offset: 0x003D2C3C
		public static void SetRotationX(this Transform transform, float x)
		{
			Vector3 euler = new Vector3(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009480 RID: 38016 RVA: 0x003D488C File Offset: 0x003D2C8C
		public static void SetRotationY(this Transform transform, float y)
		{
			Vector3 euler = new Vector3(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
			transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009481 RID: 38017 RVA: 0x003D48DC File Offset: 0x003D2CDC
		public static void SetRotationZ(this Transform transform, float z)
		{
			Vector3 euler = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, z);
			transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009482 RID: 38018 RVA: 0x003D492C File Offset: 0x003D2D2C
		public static void SetRotation(this Transform transform, float x, float y, float z)
		{
			Vector3 euler = new Vector3(x, y, z);
			transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009483 RID: 38019 RVA: 0x003D4950 File Offset: 0x003D2D50
		public static void SetLocalRotationX(this Transform transform, float x)
		{
			Vector3 euler = new Vector3(x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
			transform.localRotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009484 RID: 38020 RVA: 0x003D49A0 File Offset: 0x003D2DA0
		public static void SetLocalRotationY(this Transform transform, float y)
		{
			Vector3 euler = new Vector3(transform.localRotation.eulerAngles.x, y, transform.localRotation.eulerAngles.z);
			transform.localRotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009485 RID: 38021 RVA: 0x003D49F0 File Offset: 0x003D2DF0
		public static void SetLocalRotationZ(this Transform transform, float z)
		{
			Vector3 euler = new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, z);
			transform.localRotation = Quaternion.Euler(euler);
		}

		// Token: 0x06009486 RID: 38022 RVA: 0x003D4A40 File Offset: 0x003D2E40
		public static void SetLocalRotation(this Transform transform, float x, float y, float z)
		{
			Vector3 euler = new Vector3(x, y, z);
			transform.localRotation = Quaternion.Euler(euler);
		}
	}
}
