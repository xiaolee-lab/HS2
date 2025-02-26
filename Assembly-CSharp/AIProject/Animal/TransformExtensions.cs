using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B70 RID: 2928
	public static class TransformExtensions
	{
		// Token: 0x06005708 RID: 22280 RVA: 0x0025A9F4 File Offset: 0x00258DF4
		public static void FindMatchLoop(this Transform trans, string str, ref List<Transform> list)
		{
			if (trans == null || list == null)
			{
				return;
			}
			string name = trans.name;
			bool flag = Regex.IsMatch(name, str);
			if (flag)
			{
				list.Add(trans);
			}
			for (int i = 0; i < trans.childCount; i++)
			{
				trans.GetChild(i).FindMatchLoop(str, ref list);
			}
		}
	}
}
