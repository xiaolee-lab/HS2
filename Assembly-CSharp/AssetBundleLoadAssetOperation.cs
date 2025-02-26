using System;
using UnityEngine;

// Token: 0x02001129 RID: 4393
public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
{
	// Token: 0x06009167 RID: 37223
	public abstract bool IsEmpty();

	// Token: 0x06009168 RID: 37224
	public abstract T GetAsset<T>() where T : UnityEngine.Object;

	// Token: 0x06009169 RID: 37225
	public abstract T[] GetAllAssets<T>() where T : UnityEngine.Object;
}
