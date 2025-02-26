using System;
using UnityEngine;

// Token: 0x02000A78 RID: 2680
public class ChangeHItem : MonoBehaviour
{
	// Token: 0x06004F65 RID: 20325 RVA: 0x001E832F File Offset: 0x001E672F
	public void ChangeActive(bool val)
	{
		if (this.VisibleObj == null)
		{
			return;
		}
		if (this.VisibleObj.activeSelf == val)
		{
			return;
		}
		this.VisibleObj.SetActive(val);
	}

	// Token: 0x04004866 RID: 18534
	[Tooltip("体位アイテムと入れ替わって表示を消すオブジェクト")]
	public GameObject VisibleObj;
}
