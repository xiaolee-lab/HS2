using System;

// Token: 0x0200112E RID: 4398
public class LoadedAssetBundleDependencies
{
	// Token: 0x06009179 RID: 37241 RVA: 0x003C778B File Offset: 0x003C5B8B
	public LoadedAssetBundleDependencies(string key, string[] bundleNames)
	{
		this.m_Key = key;
		this.m_BundleNames = bundleNames;
		this.m_ReferencedCount = 1;
	}

	// Token: 0x040075E0 RID: 30176
	public string m_Key;

	// Token: 0x040075E1 RID: 30177
	public int m_ReferencedCount;

	// Token: 0x040075E2 RID: 30178
	public string[] m_BundleNames;
}
