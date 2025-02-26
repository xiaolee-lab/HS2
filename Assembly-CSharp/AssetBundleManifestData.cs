using System;
using UnityEngine;

// Token: 0x02001125 RID: 4389
[Serializable]
public class AssetBundleManifestData : AssetBundleData
{
	// Token: 0x0600914C RID: 37196 RVA: 0x003C7254 File Offset: 0x003C5654
	public AssetBundleManifestData()
	{
	}

	// Token: 0x0600914D RID: 37197 RVA: 0x003C7267 File Offset: 0x003C5667
	public AssetBundleManifestData(string bundle, string asset) : base(bundle, asset)
	{
	}

	// Token: 0x0600914E RID: 37198 RVA: 0x003C727C File Offset: 0x003C567C
	public AssetBundleManifestData(string bundle, string asset, string manifest) : base(bundle, asset)
	{
		this._manifest = manifest;
	}

	// Token: 0x17001F3F RID: 7999
	// (get) Token: 0x0600914F RID: 37199 RVA: 0x003C7298 File Offset: 0x003C5698
	// (set) Token: 0x06009150 RID: 37200 RVA: 0x003C72A0 File Offset: 0x003C56A0
	public string manifest
	{
		get
		{
			return this._manifest;
		}
		set
		{
			this._manifest = value;
		}
	}

	// Token: 0x17001F40 RID: 8000
	// (get) Token: 0x06009151 RID: 37201 RVA: 0x003C72A9 File Offset: 0x003C56A9
	public new bool isEmpty
	{
		get
		{
			return base.isEmpty || this.manifest.IsNullOrEmpty();
		}
	}

	// Token: 0x06009152 RID: 37202 RVA: 0x003C72C4 File Offset: 0x003C56C4
	public bool Check(string bundle, string asset, string manifest)
	{
		return (!manifest.IsNullOrEmpty() && this._manifest != manifest) || base.Check(bundle, asset);
	}

	// Token: 0x17001F41 RID: 8001
	// (get) Token: 0x06009153 RID: 37203 RVA: 0x003C72EC File Offset: 0x003C56EC
	public override LoadedAssetBundle LoadedBundle
	{
		get
		{
			string text;
			return AssetBundleManager.GetLoadedAssetBundle(this.bundle, out text, this._manifest);
		}
	}

	// Token: 0x06009154 RID: 37204 RVA: 0x003C730C File Offset: 0x003C570C
	public override AssetBundleLoadAssetOperation LoadBundle<T>()
	{
		if (!base.isFile)
		{
			return null;
		}
		AssetBundleLoadAssetOperation result;
		if ((result = this.request) == null)
		{
			result = (this.request = AssetBundleManager.LoadAsset(this, typeof(T)));
		}
		return result;
	}

	// Token: 0x06009155 RID: 37205 RVA: 0x003C734C File Offset: 0x003C574C
	public override AssetBundleLoadAssetOperation LoadBundleAsync<T>()
	{
		if (!base.isFile)
		{
			return null;
		}
		AssetBundleLoadAssetOperation result;
		if ((result = this.request) == null)
		{
			result = (this.request = AssetBundleManager.LoadAssetAsync(this, typeof(T)));
		}
		return result;
	}

	// Token: 0x06009156 RID: 37206 RVA: 0x003C738C File Offset: 0x003C578C
	public override AssetBundleLoadAssetOperation LoadAllBundle<T>()
	{
		if (!base.isFile)
		{
			return null;
		}
		AssetBundleLoadAssetOperation result;
		if ((result = this.request) == null)
		{
			result = (this.request = AssetBundleManager.LoadAllAsset(this, typeof(T)));
		}
		return result;
	}

	// Token: 0x06009157 RID: 37207 RVA: 0x003C73CC File Offset: 0x003C57CC
	public override AssetBundleLoadAssetOperation LoadAllBundleAsync<T>()
	{
		if (!base.isFile)
		{
			return null;
		}
		AssetBundleLoadAssetOperation result;
		if ((result = this.request) == null)
		{
			result = (this.request = AssetBundleManager.LoadAllAssetAsync(this, typeof(T)));
		}
		return result;
	}

	// Token: 0x06009158 RID: 37208 RVA: 0x003C740C File Offset: 0x003C580C
	public override void UnloadBundle(bool isUnloadForceRefCount = false, bool unloadAllLoadedObjects = false)
	{
		if (this.request != null)
		{
			AssetBundleManager.UnloadAssetBundle(this, isUnloadForceRefCount, unloadAllLoadedObjects);
		}
		this.request = null;
	}

	// Token: 0x040075CE RID: 30158
	[SerializeField]
	private string _manifest = string.Empty;
}
