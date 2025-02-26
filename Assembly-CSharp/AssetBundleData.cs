using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02001124 RID: 4388
[Serializable]
public class AssetBundleData
{
	// Token: 0x06009138 RID: 37176 RVA: 0x001057A1 File Offset: 0x00103BA1
	public AssetBundleData()
	{
	}

	// Token: 0x06009139 RID: 37177 RVA: 0x001057BF File Offset: 0x00103BBF
	public AssetBundleData(string bundle, string asset)
	{
		this.bundle = bundle;
		this.asset = asset;
	}

	// Token: 0x17001F3A RID: 7994
	// (get) Token: 0x0600913A RID: 37178 RVA: 0x001057EB File Offset: 0x00103BEB
	public bool isEmpty
	{
		get
		{
			return this.bundle.IsNullOrEmpty() || this.asset.IsNullOrEmpty();
		}
	}

	// Token: 0x0600913B RID: 37179 RVA: 0x0010580B File Offset: 0x00103C0B
	public bool Check(string bundle, string asset)
	{
		return (!asset.IsNullOrEmpty() && this.asset != asset) || (!bundle.IsNullOrEmpty() && this.bundle != bundle);
	}

	// Token: 0x17001F3B RID: 7995
	// (get) Token: 0x0600913C RID: 37180 RVA: 0x0010584C File Offset: 0x00103C4C
	public virtual LoadedAssetBundle LoadedBundle
	{
		get
		{
			string text;
			return AssetBundleManager.GetLoadedAssetBundle(this.bundle, out text, null);
		}
	}

	// Token: 0x17001F3C RID: 7996
	// (get) Token: 0x0600913D RID: 37181 RVA: 0x00105867 File Offset: 0x00103C67
	public bool isFile
	{
		get
		{
			return this.LoadedBundle != null || File.Exists(AssetBundleManager.BaseDownloadingURL + this.bundle);
		}
	}

	// Token: 0x17001F3D RID: 7997
	// (get) Token: 0x0600913E RID: 37182 RVA: 0x00105894 File Offset: 0x00103C94
	public virtual string[] AllAssetNames
	{
		get
		{
			LoadedAssetBundle loadedBundle = this.LoadedBundle;
			AssetBundle assetBundle;
			if (loadedBundle != null)
			{
				assetBundle = loadedBundle.m_AssetBundle;
			}
			else
			{
				assetBundle = AssetBundle.LoadFromFile(AssetBundleManager.BaseDownloadingURL + this.bundle);
			}
			IEnumerable<string> allAssetNames = assetBundle.GetAllAssetNames();
			if (AssetBundleData.<>f__mg$cache0 == null)
			{
				AssetBundleData.<>f__mg$cache0 = new Func<string, string>(Path.GetFileNameWithoutExtension);
			}
			string[] result = allAssetNames.Select(AssetBundleData.<>f__mg$cache0).ToArray<string>();
			if (loadedBundle == null)
			{
				assetBundle.Unload(true);
			}
			return result;
		}
	}

	// Token: 0x0600913F RID: 37183 RVA: 0x00105910 File Offset: 0x00103D10
	public static List<string> GetAssetBundleNameListFromPath(string path, bool subdirCheck = false)
	{
		List<string> result = new List<string>();
		string basePath = AssetBundleManager.BaseDownloadingURL;
		string path2 = basePath + path;
		if (!Directory.Exists(path2))
		{
			return result;
		}
		string[] files;
		if (subdirCheck)
		{
			files = Directory.GetFiles(path2, "*.unity3d", SearchOption.AllDirectories);
		}
		else
		{
			files = Directory.GetFiles(path2, "*.unity3d");
		}
		return (from s in files
		select s.Replace(basePath, string.Empty)).ToList<string>();
	}

	// Token: 0x06009140 RID: 37184 RVA: 0x0010598B File Offset: 0x00103D8B
	public void ClearRequest()
	{
		this.request = null;
	}

	// Token: 0x06009141 RID: 37185 RVA: 0x00105994 File Offset: 0x00103D94
	public virtual AssetBundleLoadAssetOperation LoadBundle<T>() where T : UnityEngine.Object
	{
		if (!this.isFile)
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

	// Token: 0x06009142 RID: 37186 RVA: 0x001059D4 File Offset: 0x00103DD4
	public virtual AssetBundleLoadAssetOperation LoadBundleAsync<T>() where T : UnityEngine.Object
	{
		if (!this.isFile)
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

	// Token: 0x06009143 RID: 37187 RVA: 0x00105A14 File Offset: 0x00103E14
	public virtual AssetBundleLoadAssetOperation LoadAllBundle<T>() where T : UnityEngine.Object
	{
		if (!this.isFile)
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

	// Token: 0x06009144 RID: 37188 RVA: 0x00105A54 File Offset: 0x00103E54
	public virtual AssetBundleLoadAssetOperation LoadAllBundleAsync<T>() where T : UnityEngine.Object
	{
		if (!this.isFile)
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

	// Token: 0x06009145 RID: 37189 RVA: 0x00105A94 File Offset: 0x00103E94
	public virtual T GetAsset<T>() where T : UnityEngine.Object
	{
		if (this.request == null)
		{
			this.request = this.LoadBundle<T>();
		}
		if (this.request == null)
		{
			return (T)((object)null);
		}
		return this.request.GetAsset<T>();
	}

	// Token: 0x06009146 RID: 37190 RVA: 0x00105ACA File Offset: 0x00103ECA
	public virtual T[] GetAllAssets<T>() where T : UnityEngine.Object
	{
		if (this.request == null)
		{
			this.request = this.LoadAllBundle<T>();
		}
		if (this.request == null)
		{
			return null;
		}
		return this.request.GetAllAssets<T>();
	}

	// Token: 0x06009147 RID: 37191 RVA: 0x00105AFC File Offset: 0x00103EFC
	public IEnumerator GetAsset<T>(Action<T> act) where T : UnityEngine.Object
	{
		if (this.request == null)
		{
			this.request = this.LoadBundleAsync<T>();
		}
		if (this.request == null)
		{
			yield break;
		}
		yield return this.request;
		if (this.request.IsEmpty())
		{
			yield break;
		}
		act.Call(this.request.GetAsset<T>());
		yield break;
	}

	// Token: 0x06009148 RID: 37192 RVA: 0x00105B20 File Offset: 0x00103F20
	public IEnumerator GetAllAssets<T>(Action<T[]> act) where T : UnityEngine.Object
	{
		if (this.request == null)
		{
			this.request = this.LoadBundleAsync<T>();
		}
		if (this.request == null)
		{
			yield break;
		}
		yield return this.request;
		if (this.request.IsEmpty())
		{
			yield break;
		}
		act.Call(this.request.GetAllAssets<T>());
		yield break;
	}

	// Token: 0x06009149 RID: 37193 RVA: 0x00105B42 File Offset: 0x00103F42
	public virtual void UnloadBundle(bool isUnloadForceRefCount = false, bool unloadAllLoadedObjects = false)
	{
		if (this.request != null)
		{
			AssetBundleManager.UnloadAssetBundle(this, isUnloadForceRefCount, unloadAllLoadedObjects);
		}
		this.request = null;
	}

	// Token: 0x17001F3E RID: 7998
	// (get) Token: 0x0600914A RID: 37194 RVA: 0x00105B5E File Offset: 0x00103F5E
	protected static bool isSimulation
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600914B RID: 37195 RVA: 0x00105B61 File Offset: 0x00103F61
	[Conditional("BASE_LOADER_LOG")]
	private void LogError(string str)
	{
	}

	// Token: 0x040075CA RID: 30154
	public string bundle = string.Empty;

	// Token: 0x040075CB RID: 30155
	public string asset = string.Empty;

	// Token: 0x040075CC RID: 30156
	protected AssetBundleLoadAssetOperation request;

	// Token: 0x040075CD RID: 30157
	[CompilerGenerated]
	private static Func<string, string> <>f__mg$cache0;
}
