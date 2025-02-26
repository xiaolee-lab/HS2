using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IllusionUtility.GetUtility
{
	// Token: 0x02001185 RID: 4485
	public static class TransformFindEx
	{
		// Token: 0x060093EB RID: 37867 RVA: 0x003D25B4 File Offset: 0x003D09B4
		public static GameObject FindLoop(this Transform transform, string name)
		{
			if (string.Compare(name, transform.gameObject.name) == 0)
			{
				return transform.gameObject;
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).FindLoop(name);
				if (null != gameObject)
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x060093EC RID: 37868 RVA: 0x003D2614 File Offset: 0x003D0A14
		public static void FindLoopPrefix(this Transform transform, List<GameObject> list, string name)
		{
			if (string.Compare(name, 0, transform.gameObject.name, 0, name.Length) == 0)
			{
				list.Add(transform.gameObject);
			}
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform2 = (Transform)obj;
					transform2.FindLoopPrefix(list, name);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060093ED RID: 37869 RVA: 0x003D26A0 File Offset: 0x003D0AA0
		public static void FindLoopTag(this Transform transform, List<GameObject> list, string tag)
		{
			if (transform.gameObject.CompareTag(tag))
			{
				list.Add(transform.gameObject);
			}
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform2 = (Transform)obj;
					transform2.FindLoopTag(list, tag);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060093EE RID: 37870 RVA: 0x003D2720 File Offset: 0x003D0B20
		public static void FindLoopAll(this Transform transform, List<GameObject> list)
		{
			list.Add(transform.gameObject);
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform2 = (Transform)obj;
					transform2.FindLoopAll(list);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060093EF RID: 37871 RVA: 0x003D278C File Offset: 0x003D0B8C
		public static GameObject FindTop(this Transform transform)
		{
			return (!(null == transform.parent)) ? transform.parent.FindTop() : transform.gameObject;
		}

		// Token: 0x060093F0 RID: 37872 RVA: 0x003D27B5 File Offset: 0x003D0BB5
		public static GameObject[] FindRootObject(this Transform transform)
		{
			return Array.FindAll<GameObject>(UnityEngine.Object.FindObjectsOfType<GameObject>(), (GameObject item) => item.transform.parent == null);
		}
	}
}
