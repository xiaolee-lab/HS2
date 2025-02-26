using System;
using UnityEngine;

// Token: 0x0200112D RID: 4397
public class AssetBundleCreate
{
	// Token: 0x06009178 RID: 37240 RVA: 0x003C7775 File Offset: 0x003C5B75
	public AssetBundleCreate(AssetBundleCreateRequest request)
	{
		this.m_CreateRequest = request;
		this.m_ReferencedCount = 1U;
	}

	// Token: 0x040075DE RID: 30174
	public AssetBundleCreateRequest m_CreateRequest;

	// Token: 0x040075DF RID: 30175
	public uint m_ReferencedCount;
}
