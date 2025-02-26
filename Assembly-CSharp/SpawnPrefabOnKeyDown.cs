using System;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class SpawnPrefabOnKeyDown : MonoBehaviour
{
	// Token: 0x06001272 RID: 4722 RVA: 0x000731B8 File Offset: 0x000715B8
	private void Update()
	{
		if (Input.GetKeyDown(this.m_KeyCode) && this.m_Prefab != null)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.m_Prefab, base.transform.position, base.transform.rotation);
		}
	}

	// Token: 0x040014F6 RID: 5366
	public GameObject m_Prefab;

	// Token: 0x040014F7 RID: 5367
	public KeyCode m_KeyCode;
}
