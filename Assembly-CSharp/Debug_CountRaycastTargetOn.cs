using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A1C RID: 2588
public class Debug_CountRaycastTargetOn : MonoBehaviour
{
	// Token: 0x06004D03 RID: 19715 RVA: 0x001D9570 File Offset: 0x001D7970
	public void CountRaycastTargetOn()
	{
		this.raycastTargetOnNum = 0;
		Image[] componentsInChildren = base.GetComponentsInChildren<Image>(true);
		foreach (Image image in componentsInChildren)
		{
			if (null != image && image.raycastTarget)
			{
				this.raycastTargetOnNum++;
			}
		}
	}

	// Token: 0x04004683 RID: 18051
	[Button("CountRaycastTargetOn", "RaycastTargetOn数取得", new object[]
	{

	})]
	public int countRaycastTargetOn;

	// Token: 0x04004684 RID: 18052
	public int raycastTargetOnNum;
}
