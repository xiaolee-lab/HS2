using System;
using System.Collections.Generic;
using UnityEngine;

namespace Illusion.Extensions
{
	// Token: 0x02001082 RID: 4226
	public static class TransformExtensions
	{
		// Token: 0x06008D9F RID: 36255 RVA: 0x003B1B28 File Offset: 0x003AFF28
		public static List<Transform> Children(this Transform self)
		{
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < self.childCount; i++)
			{
				list.Add(self.GetChild(i));
			}
			return list;
		}

		// Token: 0x06008DA0 RID: 36256 RVA: 0x003B1B60 File Offset: 0x003AFF60
		public static void ChildrenAction(this Transform self, Action<Transform> act)
		{
			for (int i = 0; i < self.childCount; i++)
			{
				act(self.GetChild(i));
			}
		}
	}
}
