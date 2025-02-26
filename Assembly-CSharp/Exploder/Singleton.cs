using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003CD RID: 973
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005B498 File Offset: 0x00059898
		public static T Instance
		{
			get
			{
				if (Singleton<T>.isQuitting)
				{
					return (T)((object)null);
				}
				if (Singleton<T>.instance == null)
				{
					Singleton<T>.instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
					if (UnityEngine.Object.FindObjectsOfType(typeof(T)).Length > 1)
					{
						UnityEngine.Debug.LogWarning("More than 1 singleton opened!");
						return Singleton<T>.instance;
					}
					if (Singleton<T>.instance == null)
					{
						GameObject gameObject = new GameObject("ExploderCore");
						Singleton<T>.instance = gameObject.AddComponent<T>();
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
				}
				return Singleton<T>.instance;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0005B541 File Offset: 0x00059941
		public virtual void OnDestroy()
		{
			Singleton<T>.isQuitting = true;
		}

		// Token: 0x04001324 RID: 4900
		private static T instance;

		// Token: 0x04001325 RID: 4901
		private static bool isQuitting;
	}
}
