using System;
using System.Linq;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x0200076E RID: 1902
	internal static class ObjectEx
	{
		// Token: 0x06002C98 RID: 11416 RVA: 0x000FFCDC File Offset: 0x000FE0DC
		public static Transform FindRoot(string findType, CommandController commandController)
		{
			Transform result = null;
			if (!findType.IsNullOrEmpty())
			{
				int index;
				if (int.TryParse(findType, out index))
				{
					result = commandController.CharaRoot.GetChild(index);
				}
				else
				{
					result = commandController.Objects[findType].transform;
				}
			}
			return result;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000FFD28 File Offset: 0x000FE128
		public static Transform FindChild(Transform root, string name)
		{
			return root.GetComponentsInChildren<Transform>(true).FirstOrDefault((Transform t) => t.name == name);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000FFD5C File Offset: 0x000FE15C
		public static Transform FindGet(string findType, string childName, string otherRootName, CommandController commandController)
		{
			Transform transform = ObjectEx.FindRoot(findType, commandController);
			if (transform == null)
			{
				GameObject gameObject = GameObject.Find(otherRootName);
				transform = gameObject.transform;
			}
			if (!childName.IsNullOrEmpty())
			{
				transform = ObjectEx.FindChild(transform, childName);
			}
			return transform;
		}
	}
}
