using System;
using UnityEngine;
using UnityEngineExtra;

// Token: 0x02001176 RID: 4470
internal static class DontDestroyParentEX
{
	// Token: 0x060093AE RID: 37806 RVA: 0x003D1298 File Offset: 0x003CF698
	public static void DontDestroyOnNextLoad(this GameObject self, GameObject target)
	{
		DontDestroyParent.Register(target);
	}

	// Token: 0x060093AF RID: 37807 RVA: 0x003D12A0 File Offset: 0x003CF6A0
	public static void DontDestroyOnNextLoad(this GameObject self, MonoBehaviour target)
	{
		DontDestroyParent.Register(target);
	}

	// Token: 0x060093B0 RID: 37808 RVA: 0x003D12A8 File Offset: 0x003CF6A8
	public static void DontDestroyOnNextLoad(this MonoBehaviour self, GameObject target)
	{
		DontDestroyParent.Register(target);
	}

	// Token: 0x060093B1 RID: 37809 RVA: 0x003D12B0 File Offset: 0x003CF6B0
	public static void DontDestroyOnNextLoad(this MonoBehaviour self, MonoBehaviour target)
	{
		DontDestroyParent.Register(target);
	}
}
