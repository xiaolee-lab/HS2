using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200112F RID: 4399
public class AssetBundleManager : Singleton<AssetBundleManager>
{
	// Token: 0x17001F44 RID: 8004
	// (get) Token: 0x0600917B RID: 37243 RVA: 0x003C77BB File Offset: 0x003C5BBB
	public static string BaseDownloadingURL
	{
		get
		{
			return AssetBundleManager.m_BaseDownloadingURL;
		}
	}

	// Token: 0x17001F45 RID: 8005
	// (get) Token: 0x0600917C RID: 37244 RVA: 0x003C77C2 File Offset: 0x003C5BC2
	// (set) Token: 0x0600917D RID: 37245 RVA: 0x003C77CE File Offset: 0x003C5BCE
	public static string[] Variants
	{
		get
		{
			return AssetBundleManager.MainBundle.Variants;
		}
		set
		{
			AssetBundleManager.MainBundle.Variants = value;
		}
	}

	// Token: 0x0600917E RID: 37246 RVA: 0x003C77DC File Offset: 0x003C5BDC
	public static AssetBundleManager.BundlePack ManifestAdd(string manifestAssetBundleName)
	{
		if (AssetBundleManager.m_ManifestBundlePack.ContainsKey(manifestAssetBundleName))
		{
			return null;
		}
		AssetBundleManager.BundlePack bundlePack = new AssetBundleManager.BundlePack();
		AssetBundleManager.m_ManifestBundlePack.Add(manifestAssetBundleName, bundlePack);
		LoadedAssetBundle loadedAssetBundle = AssetBundleManager.LoadAssetBundle(manifestAssetBundleName, false, manifestAssetBundleName);
		if (loadedAssetBundle == null)
		{
			AssetBundleManager.m_ManifestBundlePack.Remove(manifestAssetBundleName);
			return null;
		}
		AssetBundleLoadAssetOperationSimulation assetBundleLoadAssetOperationSimulation = new AssetBundleLoadAssetOperationSimulation(loadedAssetBundle.m_AssetBundle.LoadAsset("AssetBundleManifest", typeof(AssetBundleManifest)));
		if (assetBundleLoadAssetOperationSimulation.IsEmpty())
		{
			AssetBundleManager.m_ManifestBundlePack.Remove(manifestAssetBundleName);
			return null;
		}
		bundlePack.AssetBundleManifest = assetBundleLoadAssetOperationSimulation.GetAsset<AssetBundleManifest>();
		return bundlePack;
	}

	// Token: 0x17001F46 RID: 8006
	// (get) Token: 0x0600917F RID: 37247 RVA: 0x003C7870 File Offset: 0x003C5C70
	public static HashSet<string> AllLoadedAssetBundleNames
	{
		get
		{
			return AssetBundleManager.m_AllLoadedAssetBundleNames;
		}
	}

	// Token: 0x17001F47 RID: 8007
	// (get) Token: 0x06009180 RID: 37248 RVA: 0x003C7877 File Offset: 0x003C5C77
	public static Dictionary<string, AssetBundleManager.BundlePack> ManifestBundlePack
	{
		get
		{
			return AssetBundleManager.m_ManifestBundlePack;
		}
	}

	// Token: 0x17001F48 RID: 8008
	// (get) Token: 0x06009181 RID: 37249 RVA: 0x003C7880 File Offset: 0x003C5C80
	public static float Progress
	{
		get
		{
			int num = AssetBundleManager.MainBundle.LoadedAssetBundles.Count;
			float num2 = (float)num;
			foreach (AssetBundleCreate assetBundleCreate in AssetBundleManager.MainBundle.CreateAssetBundles.Values)
			{
				num++;
				num2 += assetBundleCreate.m_CreateRequest.progress;
			}
			return (num != 0) ? (num2 / (float)num) : 1f;
		}
	}

	// Token: 0x06009182 RID: 37250 RVA: 0x003C7918 File Offset: 0x003C5D18
	public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error, string manifestAssetBundleName = null)
	{
		assetBundleName = (assetBundleName ?? string.Empty);
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		if (bundlePack.DownloadingErrors.TryGetValue(assetBundleName, out error))
		{
			return null;
		}
		LoadedAssetBundle loadedAssetBundle = null;
		bundlePack.LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
		if (loadedAssetBundle == null)
		{
			return null;
		}
		LoadedAssetBundleDependencies loadedAssetBundleDependencies = bundlePack.Dependencies.Find((LoadedAssetBundleDependencies p) => p.m_Key == assetBundleName);
		if (loadedAssetBundleDependencies == null)
		{
			return loadedAssetBundle;
		}
		foreach (string key in loadedAssetBundleDependencies.m_BundleNames)
		{
			if (bundlePack.DownloadingErrors.TryGetValue(assetBundleName, out error))
			{
				return loadedAssetBundle;
			}
			LoadedAssetBundle loadedAssetBundle2;
			bundlePack.LoadedAssetBundles.TryGetValue(key, out loadedAssetBundle2);
			if (loadedAssetBundle2 == null)
			{
				return null;
			}
		}
		return loadedAssetBundle;
	}

	// Token: 0x06009183 RID: 37251 RVA: 0x003C7A1C File Offset: 0x003C5E1C
	public static void Initialize(string basePath)
	{
		if (AssetBundleManager.isInitialized)
		{
			return;
		}
		AssetBundleManager.m_BaseDownloadingURL = basePath;
		GameObject gameObject = new GameObject("AssetBundleManager", new Type[]
		{
			typeof(AssetBundleManager)
		});
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		if (AssetBundleManager.MainBundle == null)
		{
			AssetBundleManager.MainBundle = AssetBundleManager.ManifestAdd("abdata");
		}
		if (Directory.Exists(basePath))
		{
			IEnumerable<string> source = from s in Directory.GetFiles(basePath, "*.*", SearchOption.TopDirectoryOnly)
			where Path.GetExtension(s).IsNullOrEmpty()
			select s;
			if (AssetBundleManager.<>f__mg$cache0 == null)
			{
				AssetBundleManager.<>f__mg$cache0 = new Func<string, string>(Path.GetFileNameWithoutExtension);
			}
			foreach (string manifestAssetBundleName in from s in source.Select(AssetBundleManager.<>f__mg$cache0)
			where s != "abdata"
			select s)
			{
				AssetBundleManager.ManifestAdd(manifestAssetBundleName);
			}
		}
		AssetBundleManager.isInitialized = true;
		InitAddComponent.AddComponents(gameObject);
	}

	// Token: 0x06009184 RID: 37252 RVA: 0x003C7B48 File Offset: 0x003C5F48
	public static LoadedAssetBundle LoadAssetBundle(string assetBundleName, bool isAsync, string manifestAssetBundleName = null)
	{
		bool flag = assetBundleName == manifestAssetBundleName;
		if (!flag)
		{
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName, manifestAssetBundleName);
		}
		if (!AssetBundleManager.LoadAssetBundleInternal(assetBundleName, isAsync, manifestAssetBundleName) && !flag)
		{
			AssetBundleManager.LoadDependencies(assetBundleName, isAsync, manifestAssetBundleName);
		}
		string text;
		return AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text, manifestAssetBundleName);
	}

	// Token: 0x06009185 RID: 37253 RVA: 0x003C7B94 File Offset: 0x003C5F94
	protected static string RemapVariantName(string assetBundleName, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		string[] allAssetBundlesWithVariant = bundlePack.AssetBundleManifest.GetAllAssetBundlesWithVariant();
		if (Array.IndexOf<string>(allAssetBundlesWithVariant, assetBundleName) < 0)
		{
			return assetBundleName;
		}
		string[] array = assetBundleName.Split(new char[]
		{
			'.'
		});
		int num = int.MaxValue;
		int num2 = -1;
		for (int i = 0; i < allAssetBundlesWithVariant.Length; i++)
		{
			string[] array2 = allAssetBundlesWithVariant[i].Split(new char[]
			{
				'.'
			});
			if (!(array2[0] != array[0]))
			{
				int num3 = Array.IndexOf<string>(bundlePack.Variants, array2[1]);
				if (num3 != -1 && num3 < num)
				{
					num = num3;
					num2 = i;
				}
			}
		}
		if (num2 != -1)
		{
			return allAssetBundlesWithVariant[num2];
		}
		return assetBundleName;
	}

	// Token: 0x06009186 RID: 37254 RVA: 0x003C7C70 File Offset: 0x003C6070
	public static bool LoadAssetBundleInternal(string assetBundleName, bool isAsync, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		LoadedAssetBundle loadedAssetBundle = null;
		bundlePack.LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
		if (loadedAssetBundle != null)
		{
			loadedAssetBundle.m_ReferencedCount += 1U;
			return true;
		}
		AssetBundleCreate assetBundleCreate = null;
		bundlePack.CreateAssetBundles.TryGetValue(assetBundleName, out assetBundleCreate);
		if (assetBundleCreate != null)
		{
			assetBundleCreate.m_ReferencedCount += 1U;
			return true;
		}
		if (!AssetBundleManager.m_AllLoadedAssetBundleNames.Add(assetBundleName))
		{
			return true;
		}
		string path = AssetBundleManager.BaseDownloadingURL + assetBundleName;
		if (!isAsync)
		{
			bundlePack.LoadedAssetBundles.Add(assetBundleName, new LoadedAssetBundle(AssetBundle.LoadFromFile(path)));
		}
		else
		{
			bundlePack.CreateAssetBundles.Add(assetBundleName, new AssetBundleCreate(AssetBundle.LoadFromFileAsync(path)));
		}
		return false;
	}

	// Token: 0x06009187 RID: 37255 RVA: 0x003C7D44 File Offset: 0x003C6144
	protected static void LoadDependencies(string assetBundleName, bool isAsync, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		if (object.ReferenceEquals(bundlePack.AssetBundleManifest, null))
		{
			return;
		}
		string[] allDependencies = bundlePack.AssetBundleManifest.GetAllDependencies(assetBundleName);
		if (allDependencies.Length == 0)
		{
			return;
		}
		for (int i = 0; i < allDependencies.Length; i++)
		{
			allDependencies[i] = AssetBundleManager.RemapVariantName(allDependencies[i], manifestAssetBundleName);
		}
		LoadedAssetBundleDependencies loadedAssetBundleDependencies = bundlePack.Dependencies.Find((LoadedAssetBundleDependencies p) => p.m_Key == assetBundleName);
		if (loadedAssetBundleDependencies != null)
		{
			loadedAssetBundleDependencies.m_ReferencedCount++;
		}
		else
		{
			bundlePack.Dependencies.Add(new LoadedAssetBundleDependencies(assetBundleName, allDependencies));
		}
		for (int j = 0; j < allDependencies.Length; j++)
		{
			AssetBundleManager.LoadAssetBundleInternal(allDependencies[j], isAsync, manifestAssetBundleName);
		}
	}

	// Token: 0x06009188 RID: 37256 RVA: 0x003C7E43 File Offset: 0x003C6243
	public static void UnloadAssetBundle(AssetBundleData data, bool isUnloadForceRefCount, bool unloadAllLoadedObjects = false)
	{
		AssetBundleManager.UnloadAssetBundle(data.bundle, isUnloadForceRefCount, null, unloadAllLoadedObjects);
	}

	// Token: 0x06009189 RID: 37257 RVA: 0x003C7E53 File Offset: 0x003C6253
	public static void UnloadAssetBundle(AssetBundleManifestData data, bool isUnloadForceRefCount, bool unloadAllLoadedObjects = false)
	{
		AssetBundleManager.UnloadAssetBundle(data.bundle, isUnloadForceRefCount, data.manifest, unloadAllLoadedObjects);
	}

	// Token: 0x0600918A RID: 37258 RVA: 0x003C7E68 File Offset: 0x003C6268
	public static void UnloadAssetBundle(string assetBundleName, bool isUnloadForceRefCount, string manifestAssetBundleName = null, bool unloadAllLoadedObjects = false)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		while (AssetBundleManager.UnloadBundleAndDependencies(assetBundleName, manifestAssetBundleName, unloadAllLoadedObjects) && isUnloadForceRefCount)
		{
		}
	}

	// Token: 0x0600918B RID: 37259 RVA: 0x003C7E94 File Offset: 0x003C6294
	private static bool UnloadBundleAndDependencies(string assetBundleName, string manifestAssetBundleName, bool unloadAllLoadedObjects)
	{
		AssetBundleManager.BundlePack bundlePack;
		if (!false)
		{
			bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		}
		else if (!AssetBundleManager.m_ManifestBundlePack.TryGetValue(manifestAssetBundleName, out bundlePack))
		{
			bundlePack = AssetBundleManager.m_ManifestBundlePack["abdata"];
		}
		bool flag = AssetBundleManager.UnloadBundle(assetBundleName, bundlePack, unloadAllLoadedObjects);
		if (flag)
		{
			LoadedAssetBundleDependencies loadedAssetBundleDependencies = bundlePack.Dependencies.Find((LoadedAssetBundleDependencies p) => p.m_Key == assetBundleName);
			if (loadedAssetBundleDependencies != null && --loadedAssetBundleDependencies.m_ReferencedCount == 0)
			{
				foreach (string assetBundleName2 in loadedAssetBundleDependencies.m_BundleNames)
				{
					AssetBundleManager.UnloadBundle(assetBundleName2, bundlePack, unloadAllLoadedObjects);
				}
				bundlePack.Dependencies.Remove(loadedAssetBundleDependencies);
			}
		}
		return flag;
	}

	// Token: 0x0600918C RID: 37260 RVA: 0x003C7F78 File Offset: 0x003C6378
	private static bool UnloadBundle(string assetBundleName, AssetBundleManager.BundlePack targetPack, bool unloadAllLoadedObjects)
	{
		assetBundleName = (assetBundleName ?? string.Empty);
		string text;
		if (targetPack.DownloadingErrors.TryGetValue(assetBundleName, out text))
		{
			return false;
		}
		LoadedAssetBundle loadedAssetBundle = null;
		if (!targetPack.LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle))
		{
			return false;
		}
		if ((loadedAssetBundle.m_ReferencedCount -= 1U) == 0U)
		{
			if (loadedAssetBundle.m_AssetBundle)
			{
				loadedAssetBundle.m_AssetBundle.Unload(unloadAllLoadedObjects);
			}
			targetPack.LoadedAssetBundles.Remove(assetBundleName);
			AssetBundleManager.m_AllLoadedAssetBundleNames.Remove(assetBundleName);
		}
		return true;
	}

	// Token: 0x0600918D RID: 37261 RVA: 0x003C800C File Offset: 0x003C640C
	private void Update()
	{
		foreach (KeyValuePair<string, AssetBundleManager.BundlePack> keyValuePair in AssetBundleManager.m_ManifestBundlePack)
		{
			AssetBundleManager.BundlePack value = keyValuePair.Value;
			foreach (KeyValuePair<string, AssetBundleCreate> keyValuePair2 in value.CreateAssetBundles)
			{
				AssetBundleCreateRequest createRequest = keyValuePair2.Value.m_CreateRequest;
				if (createRequest.isDone)
				{
					LoadedAssetBundle loadedAssetBundle = new LoadedAssetBundle(createRequest.assetBundle);
					loadedAssetBundle.m_ReferencedCount = keyValuePair2.Value.m_ReferencedCount;
					value.LoadedAssetBundles.Add(keyValuePair2.Key, loadedAssetBundle);
					this.keysToRemove.Add(keyValuePair2.Key);
				}
			}
			foreach (string key in this.keysToRemove)
			{
				value.CreateAssetBundles.Remove(key);
			}
			int i = 0;
			while (i < value.InProgressOperations.Count)
			{
				if (!value.InProgressOperations[i].Update())
				{
					value.InProgressOperations.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			this.keysToRemove.Clear();
		}
	}

	// Token: 0x0600918E RID: 37262 RVA: 0x003C81DC File Offset: 0x003C65DC
	public static AssetBundleLoadAssetOperation LoadAsset(AssetBundleData data, Type type)
	{
		return AssetBundleManager.LoadAsset(data.bundle, data.asset, type, null);
	}

	// Token: 0x0600918F RID: 37263 RVA: 0x003C81F1 File Offset: 0x003C65F1
	public static AssetBundleLoadAssetOperation LoadAsset(AssetBundleManifestData data, Type type)
	{
		return AssetBundleManager.LoadAsset(data.bundle, data.asset, type, data.manifest);
	}

	// Token: 0x06009190 RID: 37264 RVA: 0x003C820C File Offset: 0x003C660C
	public static AssetBundleLoadAssetOperation LoadAsset(string assetBundleName, string assetName, Type type, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = null;
		if (assetBundleLoadAssetOperation == null)
		{
			AssetBundleManager.LoadAssetBundle(assetBundleName, false, manifestAssetBundleName);
			assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationSimulation(bundlePack.LoadedAssetBundles[assetBundleName].m_AssetBundle.LoadAsset(assetName, type));
		}
		return assetBundleLoadAssetOperation;
	}

	// Token: 0x06009191 RID: 37265 RVA: 0x003C8267 File Offset: 0x003C6667
	public static AssetBundleLoadAssetOperation LoadAssetAsync(AssetBundleData data, Type type)
	{
		return AssetBundleManager.LoadAssetAsync(data.bundle, data.asset, type, null);
	}

	// Token: 0x06009192 RID: 37266 RVA: 0x003C827C File Offset: 0x003C667C
	public static AssetBundleLoadAssetOperation LoadAssetAsync(AssetBundleManifestData data, Type type)
	{
		return AssetBundleManager.LoadAssetAsync(data.bundle, data.asset, type, data.manifest);
	}

	// Token: 0x06009193 RID: 37267 RVA: 0x003C8298 File Offset: 0x003C6698
	public static AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, Type type, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = null;
		if (assetBundleLoadAssetOperation == null)
		{
			AssetBundleManager.LoadAssetBundle(assetBundleName, true, manifestAssetBundleName);
			assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type, manifestAssetBundleName);
			bundlePack.InProgressOperations.Add(assetBundleLoadAssetOperation);
		}
		return assetBundleLoadAssetOperation;
	}

	// Token: 0x06009194 RID: 37268 RVA: 0x003C82EB File Offset: 0x003C66EB
	public static AssetBundleLoadAssetOperation LoadAllAsset(AssetBundleData data, Type type)
	{
		return AssetBundleManager.LoadAllAsset(data.bundle, type, null);
	}

	// Token: 0x06009195 RID: 37269 RVA: 0x003C82FA File Offset: 0x003C66FA
	public static AssetBundleLoadAssetOperation LoadAllAsset(AssetBundleManifestData data, Type type)
	{
		return AssetBundleManager.LoadAllAsset(data.bundle, type, data.manifest);
	}

	// Token: 0x06009196 RID: 37270 RVA: 0x003C8310 File Offset: 0x003C6710
	public static AssetBundleLoadAssetOperation LoadAllAsset(string assetBundleName, Type type, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = null;
		if (assetBundleLoadAssetOperation == null)
		{
			AssetBundleManager.LoadAssetBundle(assetBundleName, false, manifestAssetBundleName);
			assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationSimulation(bundlePack.LoadedAssetBundles[assetBundleName].m_AssetBundle.LoadAllAssets(type));
		}
		return assetBundleLoadAssetOperation;
	}

	// Token: 0x06009197 RID: 37271 RVA: 0x003C836A File Offset: 0x003C676A
	public static AssetBundleLoadAssetOperation LoadAllAssetAsync(AssetBundleData data, Type type)
	{
		return AssetBundleManager.LoadAllAssetAsync(data.bundle, type, null);
	}

	// Token: 0x06009198 RID: 37272 RVA: 0x003C8379 File Offset: 0x003C6779
	public static AssetBundleLoadAssetOperation LoadAllAssetAsync(AssetBundleManifestData data, Type type)
	{
		return AssetBundleManager.LoadAllAssetAsync(data.bundle, type, data.manifest);
	}

	// Token: 0x06009199 RID: 37273 RVA: 0x003C8390 File Offset: 0x003C6790
	public static AssetBundleLoadAssetOperation LoadAllAssetAsync(string assetBundleName, Type type, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = null;
		if (assetBundleLoadAssetOperation == null)
		{
			AssetBundleManager.LoadAssetBundle(assetBundleName, true, manifestAssetBundleName);
			assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationFull(assetBundleName, null, type, manifestAssetBundleName);
			bundlePack.InProgressOperations.Add(assetBundleLoadAssetOperation);
		}
		return assetBundleLoadAssetOperation;
	}

	// Token: 0x0600919A RID: 37274 RVA: 0x003C83E3 File Offset: 0x003C67E3
	public static AssetBundleLoadOperation LoadLevel(AssetBundleData data, bool isAdditive)
	{
		return AssetBundleManager.LoadLevel(data.bundle, data.asset, isAdditive, null);
	}

	// Token: 0x0600919B RID: 37275 RVA: 0x003C83F8 File Offset: 0x003C67F8
	public static AssetBundleLoadOperation LoadLevel(AssetBundleManifestData data, bool isAdditive)
	{
		return AssetBundleManager.LoadLevel(data.bundle, data.asset, isAdditive, data.manifest);
	}

	// Token: 0x0600919C RID: 37276 RVA: 0x003C8414 File Offset: 0x003C6814
	public static AssetBundleLoadOperation LoadLevel(string assetBundleName, string levelName, bool isAdditive, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleLoadOperation assetBundleLoadOperation = null;
		if (assetBundleLoadOperation == null)
		{
			AssetBundleManager.LoadAssetBundle(assetBundleName, false, manifestAssetBundleName);
			SceneManager.LoadScene(levelName, (!isAdditive) ? LoadSceneMode.Single : LoadSceneMode.Additive);
			assetBundleLoadOperation = new AssetBundleLoadLevelSimulationOperation();
		}
		return assetBundleLoadOperation;
	}

	// Token: 0x0600919D RID: 37277 RVA: 0x003C845E File Offset: 0x003C685E
	public static AssetBundleLoadOperation LoadLevelAsync(AssetBundleData data, bool isAdditive)
	{
		return AssetBundleManager.LoadLevelAsync(data.bundle, data.asset, isAdditive, null);
	}

	// Token: 0x0600919E RID: 37278 RVA: 0x003C8473 File Offset: 0x003C6873
	public static AssetBundleLoadOperation LoadLevelAsync(AssetBundleManifestData data, bool isAdditive)
	{
		return AssetBundleManager.LoadLevelAsync(data.bundle, data.asset, isAdditive, data.manifest);
	}

	// Token: 0x0600919F RID: 37279 RVA: 0x003C8490 File Offset: 0x003C6890
	public static AssetBundleLoadOperation LoadLevelAsync(string assetBundleName, string levelName, bool isAdditive, string manifestAssetBundleName = null)
	{
		if (manifestAssetBundleName.IsNullOrEmpty())
		{
			manifestAssetBundleName = "abdata";
		}
		AssetBundleManager.BundlePack bundlePack = AssetBundleManager.m_ManifestBundlePack[manifestAssetBundleName];
		AssetBundleLoadOperation assetBundleLoadOperation = null;
		if (assetBundleLoadOperation == null)
		{
			AssetBundleManager.LoadAssetBundle(assetBundleName, true, manifestAssetBundleName);
			assetBundleLoadOperation = new AssetBundleLoadLevelOperation(assetBundleName, levelName, isAdditive, manifestAssetBundleName);
			bundlePack.InProgressOperations.Add(assetBundleLoadOperation);
		}
		return assetBundleLoadOperation;
	}

	// Token: 0x040075E3 RID: 30179
	public const string MAIN_MANIFEST_NAME = "abdata";

	// Token: 0x040075E4 RID: 30180
	public const string Extension = ".unity3d";

	// Token: 0x040075E5 RID: 30181
	private static HashSet<string> m_AllLoadedAssetBundleNames = new HashSet<string>();

	// Token: 0x040075E6 RID: 30182
	private static AssetBundleManager.BundlePack MainBundle = null;

	// Token: 0x040075E7 RID: 30183
	private static Dictionary<string, AssetBundleManager.BundlePack> m_ManifestBundlePack = new Dictionary<string, AssetBundleManager.BundlePack>();

	// Token: 0x040075E8 RID: 30184
	private static string m_BaseDownloadingURL = string.Empty;

	// Token: 0x040075E9 RID: 30185
	private static bool isInitialized = false;

	// Token: 0x040075EA RID: 30186
	private List<string> keysToRemove = new List<string>();

	// Token: 0x040075EB RID: 30187
	[CompilerGenerated]
	private static Func<string, string> <>f__mg$cache0;

	// Token: 0x02001130 RID: 4400
	public class BundlePack
	{
		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x060091A4 RID: 37284 RVA: 0x003C8581 File Offset: 0x003C6981
		// (set) Token: 0x060091A5 RID: 37285 RVA: 0x003C8589 File Offset: 0x003C6989
		public string[] Variants
		{
			get
			{
				return this.m_Variants;
			}
			set
			{
				this.m_Variants = value;
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x060091A6 RID: 37286 RVA: 0x003C8592 File Offset: 0x003C6992
		// (set) Token: 0x060091A7 RID: 37287 RVA: 0x003C859A File Offset: 0x003C699A
		public AssetBundleManifest AssetBundleManifest
		{
			get
			{
				return this.m_AssetBundleManifest;
			}
			set
			{
				this.m_AssetBundleManifest = value;
			}
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x060091A8 RID: 37288 RVA: 0x003C85A3 File Offset: 0x003C69A3
		// (set) Token: 0x060091A9 RID: 37289 RVA: 0x003C85AB File Offset: 0x003C69AB
		public Dictionary<string, LoadedAssetBundle> LoadedAssetBundles
		{
			get
			{
				return this.m_LoadedAssetBundles;
			}
			set
			{
				this.m_LoadedAssetBundles = value;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x060091AA RID: 37290 RVA: 0x003C85B4 File Offset: 0x003C69B4
		// (set) Token: 0x060091AB RID: 37291 RVA: 0x003C85BC File Offset: 0x003C69BC
		public Dictionary<string, AssetBundleCreate> CreateAssetBundles
		{
			get
			{
				return this.m_CreateAssetBundles;
			}
			set
			{
				this.m_CreateAssetBundles = value;
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x060091AC RID: 37292 RVA: 0x003C85C5 File Offset: 0x003C69C5
		// (set) Token: 0x060091AD RID: 37293 RVA: 0x003C85CD File Offset: 0x003C69CD
		public Dictionary<string, string> DownloadingErrors
		{
			get
			{
				return this.m_DownloadingErrors;
			}
			set
			{
				this.m_DownloadingErrors = value;
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x060091AE RID: 37294 RVA: 0x003C85D6 File Offset: 0x003C69D6
		// (set) Token: 0x060091AF RID: 37295 RVA: 0x003C85DE File Offset: 0x003C69DE
		public List<AssetBundleLoadOperation> InProgressOperations
		{
			get
			{
				return this.m_InProgressOperations;
			}
			set
			{
				this.m_InProgressOperations = value;
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x060091B0 RID: 37296 RVA: 0x003C85E7 File Offset: 0x003C69E7
		// (set) Token: 0x060091B1 RID: 37297 RVA: 0x003C85EF File Offset: 0x003C69EF
		public List<LoadedAssetBundleDependencies> Dependencies
		{
			get
			{
				return this.m_Dependencies;
			}
			set
			{
				this.m_Dependencies = value;
			}
		}

		// Token: 0x040075EE RID: 30190
		private string[] m_Variants = Array.Empty<string>();

		// Token: 0x040075EF RID: 30191
		private AssetBundleManifest m_AssetBundleManifest;

		// Token: 0x040075F0 RID: 30192
		private Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();

		// Token: 0x040075F1 RID: 30193
		private Dictionary<string, AssetBundleCreate> m_CreateAssetBundles = new Dictionary<string, AssetBundleCreate>();

		// Token: 0x040075F2 RID: 30194
		private Dictionary<string, string> m_DownloadingErrors = new Dictionary<string, string>();

		// Token: 0x040075F3 RID: 30195
		private List<AssetBundleLoadOperation> m_InProgressOperations = new List<AssetBundleLoadOperation>();

		// Token: 0x040075F4 RID: 30196
		private List<LoadedAssetBundleDependencies> m_Dependencies = new List<LoadedAssetBundleDependencies>();
	}
}
