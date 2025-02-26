using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A9E RID: 2718
public class HitCollision : MonoBehaviour
{
	// Token: 0x06005001 RID: 20481 RVA: 0x001ECCB0 File Offset: 0x001EB0B0
	private void Reset()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.lstObj.Add(base.transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x04004901 RID: 18689
	public List<GameObject> lstObj = new List<GameObject>();
}
