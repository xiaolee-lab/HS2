using System;
using UnityEngine;

// Token: 0x0200063D RID: 1597
public class AssetStoreLink : MonoBehaviour
{
	// Token: 0x060025F5 RID: 9717 RVA: 0x000D847F File Offset: 0x000D687F
	public void VisitStore()
	{
		Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/" + this.assetStoreID);
	}

	// Token: 0x040025AF RID: 9647
	public string assetStoreID = "74356";
}
