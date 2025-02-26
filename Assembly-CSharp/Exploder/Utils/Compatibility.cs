using System;
using UnityEngine;

namespace Exploder.Utils
{
	// Token: 0x020003C6 RID: 966
	public static class Compatibility
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x00064C48 File Offset: 0x00063048
		public static void SetVisible(GameObject obj, bool status, bool includeInactive)
		{
			if (obj)
			{
				MeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>(includeInactive);
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					meshRenderer.enabled = status;
				}
				SkinnedMeshRenderer[] componentsInChildren2 = obj.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive);
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren2)
				{
					skinnedMeshRenderer.enabled = status;
				}
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00064CBE File Offset: 0x000630BE
		public static bool IsActive(GameObject obj)
		{
			return obj && obj.activeSelf;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00064CD4 File Offset: 0x000630D4
		public static void SetActive(GameObject obj, bool status)
		{
			if (obj)
			{
				obj.SetActive(status);
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00064CE8 File Offset: 0x000630E8
		public static void SetActiveRecursively(GameObject obj, bool status)
		{
			if (obj)
			{
				int childCount = obj.transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					Compatibility.SetActiveRecursively(obj.transform.GetChild(i).gameObject, status);
				}
				obj.SetActive(status);
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00064D3C File Offset: 0x0006313C
		public static void EnableCollider(GameObject obj, bool status)
		{
			if (obj)
			{
				Collider[] componentsInChildren = obj.GetComponentsInChildren<Collider>();
				foreach (Collider collider in componentsInChildren)
				{
					collider.enabled = status;
				}
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00064D7C File Offset: 0x0006317C
		public static void Destroy(UnityEngine.Object obj, bool allowDestroyingAssets)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj, allowDestroyingAssets);
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00064D9A File Offset: 0x0006319A
		public static void SetCursorVisible(bool status)
		{
			Cursor.visible = status;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00064DA2 File Offset: 0x000631A2
		public static void LockCursor(bool status)
		{
			Cursor.lockState = ((!status) ? CursorLockMode.Confined : CursorLockMode.Locked);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00064DB6 File Offset: 0x000631B6
		public static bool IsCursorLocked()
		{
			return Cursor.lockState == CursorLockMode.Locked;
		}
	}
}
