using System;
using UnityEngine;

// Token: 0x0200112B RID: 4395
public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
{
	// Token: 0x06009171 RID: 37233 RVA: 0x003C7596 File Offset: 0x003C5996
	public AssetBundleLoadAssetOperationFull(string bundleName, string assetName, Type type, string manifestAssetBundleName)
	{
		this.m_AssetBundleName = bundleName;
		this.m_AssetName = assetName;
		this.m_Type = type;
		this.m_ManifestAssetBundleName = manifestAssetBundleName;
	}

	// Token: 0x06009172 RID: 37234 RVA: 0x003C75BB File Offset: 0x003C59BB
	public override bool IsEmpty()
	{
		return this.m_Request == null || !this.m_Request.isDone || this.m_Request.asset == null;
	}

	// Token: 0x06009173 RID: 37235 RVA: 0x003C75EF File Offset: 0x003C59EF
	public override T GetAsset<T>()
	{
		if (this.m_Request != null && this.m_Request.isDone)
		{
			return this.m_Request.asset as T;
		}
		return (T)((object)null);
	}

	// Token: 0x06009174 RID: 37236 RVA: 0x003C7628 File Offset: 0x003C5A28
	public override T[] GetAllAssets<T>()
	{
		if (this.m_Request != null && this.m_Request.isDone)
		{
			T[] array = new T[this.m_Request.allAssets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (this.m_Request.allAssets[i] as T);
			}
			return array;
		}
		return null;
	}

	// Token: 0x06009175 RID: 37237 RVA: 0x003C7698 File Offset: 0x003C5A98
	public override bool Update()
	{
		if (this.m_Request != null)
		{
			return false;
		}
		LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError, this.m_ManifestAssetBundleName);
		if (loadedAssetBundle != null)
		{
			if (loadedAssetBundle.m_AssetBundle)
			{
				if (this.m_AssetName.IsNullOrEmpty())
				{
					this.m_Request = loadedAssetBundle.m_AssetBundle.LoadAllAssetsAsync(this.m_Type);
				}
				else
				{
					this.m_Request = loadedAssetBundle.m_AssetBundle.LoadAssetAsync(this.m_AssetName, this.m_Type);
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x06009176 RID: 37238 RVA: 0x003C772C File Offset: 0x003C5B2C
	public override bool IsDone()
	{
		return (this.m_Request == null && this.m_DownloadingError != null) || (this.m_Request != null && this.m_Request.isDone);
	}

	// Token: 0x040075D6 RID: 30166
	protected string m_AssetBundleName;

	// Token: 0x040075D7 RID: 30167
	protected string m_AssetName;

	// Token: 0x040075D8 RID: 30168
	protected string m_ManifestAssetBundleName;

	// Token: 0x040075D9 RID: 30169
	protected Type m_Type;

	// Token: 0x040075DA RID: 30170
	protected string m_DownloadingError;

	// Token: 0x040075DB RID: 30171
	protected AssetBundleRequest m_Request;
}
