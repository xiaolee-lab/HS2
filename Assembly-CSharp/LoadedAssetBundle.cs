using System;
using UnityEngine;

// Token: 0x0200112C RID: 4396
public class LoadedAssetBundle
{
	// Token: 0x06009177 RID: 37239 RVA: 0x003C775F File Offset: 0x003C5B5F
	public LoadedAssetBundle(AssetBundle assetBundle)
	{
		this.m_AssetBundle = assetBundle;
		this.m_ReferencedCount = 1U;
	}

	// Token: 0x040075DC RID: 30172
	public AssetBundle m_AssetBundle;

	// Token: 0x040075DD RID: 30173
	public uint m_ReferencedCount;
}
