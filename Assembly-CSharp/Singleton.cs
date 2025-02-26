using System;
using UnityEngine;

// Token: 0x020011A6 RID: 4518
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x17001F87 RID: 8071
	// (get) Token: 0x06009495 RID: 38037 RVA: 0x0012E938 File Offset: 0x0012CD38
	public static T Instance
	{
		get
		{
			if (!Singleton<T>.instance)
			{
				Singleton<T>.instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
				if (!Singleton<T>.instance)
				{
				}
			}
			return Singleton<T>.instance;
		}
	}

	// Token: 0x06009496 RID: 38038 RVA: 0x0012E98B File Offset: 0x0012CD8B
	public static bool IsInstance()
	{
		return null != Singleton<T>.instance;
	}

	// Token: 0x06009497 RID: 38039 RVA: 0x0012E9A9 File Offset: 0x0012CDA9
	protected virtual void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06009498 RID: 38040 RVA: 0x0012E9B2 File Offset: 0x0012CDB2
	protected virtual void OnDestroy()
	{
		if (!Singleton<T>.instance)
		{
			return;
		}
		if (this != Singleton<T>.Instance)
		{
			return;
		}
		Singleton<T>.instance = (T)((object)null);
	}

	// Token: 0x06009499 RID: 38041 RVA: 0x0012E9EA File Offset: 0x0012CDEA
	protected bool CheckInstance()
	{
		if (this == Singleton<T>.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(this);
		return false;
	}

	// Token: 0x0400778E RID: 30606
	private static T instance;
}
