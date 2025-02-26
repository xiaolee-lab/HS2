using System;
using UnityEngine;

namespace UnityEngineExtra
{
	// Token: 0x02001175 RID: 4469
	public class DontDestroyParent : MonoBehaviour
	{
		// Token: 0x060093A9 RID: 37801 RVA: 0x003D11E2 File Offset: 0x003CF5E2
		private void Awake()
		{
			if (DontDestroyParent.Instance == this)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060093AA RID: 37802 RVA: 0x003D120F File Offset: 0x003CF60F
		public static void Register(GameObject obj)
		{
			obj.transform.parent = DontDestroyParent.Instance.transform;
		}

		// Token: 0x060093AB RID: 37803 RVA: 0x003D1226 File Offset: 0x003CF626
		public static void Register(MonoBehaviour component)
		{
			DontDestroyParent.Register(component.gameObject);
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x060093AC RID: 37804 RVA: 0x003D1234 File Offset: 0x003CF634
		public static DontDestroyParent Instance
		{
			get
			{
				if (DontDestroyParent.instance == null)
				{
					DontDestroyParent.instance = UnityEngine.Object.FindObjectOfType<DontDestroyParent>();
					if (DontDestroyParent.instance == null)
					{
						GameObject gameObject = new GameObject("DontDestroyParent");
						DontDestroyParent.instance = gameObject.AddComponent<DontDestroyParent>();
					}
					DontDestroyParent.instance.gameObject.hideFlags = HideFlags.NotEditable;
				}
				return DontDestroyParent.instance;
			}
		}

		// Token: 0x04007713 RID: 30483
		private static DontDestroyParent instance;
	}
}
