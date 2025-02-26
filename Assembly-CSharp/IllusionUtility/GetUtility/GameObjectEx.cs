using System;
using System.Collections.Generic;
using UnityEngine;

namespace IllusionUtility.GetUtility
{
	// Token: 0x02001186 RID: 4486
	public static class GameObjectEx
	{
		// Token: 0x060093F2 RID: 37874 RVA: 0x003D27F4 File Offset: 0x003D0BF4
		public static T Get<T>(this GameObject obj) where T : MonoBehaviour
		{
			T component = obj.GetComponent<T>();
			if (!component)
			{
			}
			return component;
		}

		// Token: 0x060093F3 RID: 37875 RVA: 0x003D281C File Offset: 0x003D0C1C
		public static T SearchComponent<T>(this GameObject obj, string searchName) where T : MonoBehaviour
		{
			T[] componentsInChildren = obj.GetComponentsInChildren<T>(true);
			foreach (T result in componentsInChildren)
			{
				if (searchName == result.name)
				{
					return result;
				}
			}
			return (T)((object)null);
		}

		// Token: 0x060093F4 RID: 37876 RVA: 0x003D2870 File Offset: 0x003D0C70
		public static GameObject[] CreateChild(this GameObject topObj, string pathName)
		{
			GameObject gameObject = null;
			List<GameObject> list = new List<GameObject>();
			foreach (string name in pathName.Split(new char[]
			{
				'/'
			}))
			{
				GameObject gameObject2 = new GameObject(name);
				list.Add(gameObject2);
				if (gameObject != null)
				{
					gameObject2.transform.SetParent(gameObject.transform);
				}
				gameObject = gameObject2;
			}
			list[0].transform.SetParent(topObj.transform);
			return list.ToArray();
		}
	}
}
