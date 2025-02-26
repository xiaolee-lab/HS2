using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Illusion.Extensions
{
	// Token: 0x0200107B RID: 4219
	public static class GameObjectExtensions
	{
		// Token: 0x06008D60 RID: 36192 RVA: 0x003B1160 File Offset: 0x003AF560
		public static List<GameObject> Children(this GameObject self)
		{
			List<GameObject> list = new List<GameObject>();
			Transform transform = self.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i).gameObject);
			}
			return list;
		}

		// Token: 0x06008D61 RID: 36193 RVA: 0x003B11A4 File Offset: 0x003AF5A4
		public static void ChildrenAction(this GameObject self, Action<GameObject> act)
		{
			Transform transform = self.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				act(transform.GetChild(i).gameObject);
			}
		}

		// Token: 0x06008D62 RID: 36194 RVA: 0x003B11E4 File Offset: 0x003AF5E4
		public static GameObject[] CreateChild(this GameObject self, string pathName, bool worldPositionStays = true)
		{
			GameObject[] array = (from s in pathName.Split(new char[]
			{
				'/'
			})
			select new GameObject(s)).ToArray<GameObject>();
			(from go in array
			select go.transform).Aggregate(delegate(Transform parent, Transform child)
			{
				child.SetParent(parent);
				return child;
			});
			array[0].transform.SetParent(self.transform, worldPositionStays);
			return array;
		}

		// Token: 0x06008D63 RID: 36195 RVA: 0x003B1285 File Offset: 0x003AF685
		public static bool SetActiveIfDifferent(this GameObject self, bool active)
		{
			if (self.activeSelf == active)
			{
				return false;
			}
			self.SetActive(active);
			return true;
		}
	}
}
