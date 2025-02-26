using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02001128 RID: 4392
public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
{
	// Token: 0x06009162 RID: 37218 RVA: 0x003C744E File Offset: 0x003C584E
	public AssetBundleLoadLevelOperation(string assetbundleName, string levelName, bool isAdditive, string manifestAssetBundleName)
	{
		this.m_AssetBundleName = assetbundleName;
		this.m_LevelName = levelName;
		this.m_IsAdditive = isAdditive;
		this.m_ManifestAssetBundleName = manifestAssetBundleName;
	}

	// Token: 0x17001F43 RID: 8003
	// (get) Token: 0x06009163 RID: 37219 RVA: 0x003C7473 File Offset: 0x003C5873
	public AsyncOperation Request
	{
		get
		{
			return this.m_Request;
		}
	}

	// Token: 0x06009164 RID: 37220 RVA: 0x003C747C File Offset: 0x003C587C
	public override bool Update()
	{
		if (this.m_Request != null)
		{
			return false;
		}
		LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError, this.m_ManifestAssetBundleName);
		if (loadedAssetBundle != null)
		{
			this.m_Request = SceneManager.LoadSceneAsync(this.m_LevelName, (!this.m_IsAdditive) ? LoadSceneMode.Single : LoadSceneMode.Additive);
			return false;
		}
		return true;
	}

	// Token: 0x06009165 RID: 37221 RVA: 0x003C74DA File Offset: 0x003C58DA
	public override bool IsDone()
	{
		return (this.m_Request == null && this.m_DownloadingError != null) || (this.m_Request != null && this.m_Request.isDone);
	}

	// Token: 0x040075CF RID: 30159
	protected string m_AssetBundleName;

	// Token: 0x040075D0 RID: 30160
	protected string m_LevelName;

	// Token: 0x040075D1 RID: 30161
	protected string m_ManifestAssetBundleName;

	// Token: 0x040075D2 RID: 30162
	protected bool m_IsAdditive;

	// Token: 0x040075D3 RID: 30163
	protected string m_DownloadingError;

	// Token: 0x040075D4 RID: 30164
	protected AsyncOperation m_Request;
}
