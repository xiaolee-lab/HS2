using System;
using ADV.EventCG;
using UnityEngine;

namespace ADV.Commands.EventCG
{
	// Token: 0x0200070F RID: 1807
	internal static class Common
	{
		// Token: 0x06002B28 RID: 11048 RVA: 0x000FA198 File Offset: 0x000F8598
		public static bool Release(TextScenario scenario)
		{
			bool flag = scenario.commandController.EventCGRoot.childCount > 0;
			if (flag)
			{
				Transform child = scenario.commandController.EventCGRoot.GetChild(0);
				Data component = child.GetComponent<Data>();
				if (component != null)
				{
					component.ItemClear();
					component.Restore();
				}
				UnityEngine.Object.Destroy(child.gameObject);
				Transform transform = child;
				transform.name += "(Destroyed)";
				child.parent = null;
			}
			return flag;
		}
	}
}
