using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000699 RID: 1689
	public class CTSSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000ED79C File Offset: 0x000EBB9C
		public static T Instance
		{
			get
			{
				object @lock = CTSSingleton<T>._lock;
				T instance;
				lock (@lock)
				{
					if (CTSSingleton<T>._instance == null)
					{
						GameObject gameObject = new GameObject();
						CTSSingleton<T>._instance = gameObject.AddComponent<T>();
						gameObject.name = typeof(T).ToString();
						gameObject.hideFlags = HideFlags.HideAndDontSave;
						if (Application.isPlaying)
						{
							UnityEngine.Object.DontDestroyOnLoad(gameObject);
						}
					}
					instance = CTSSingleton<T>._instance;
				}
				return instance;
			}
		}

		// Token: 0x040028FB RID: 10491
		private static T _instance;

		// Token: 0x040028FC RID: 10492
		private static object _lock = new object();
	}
}
