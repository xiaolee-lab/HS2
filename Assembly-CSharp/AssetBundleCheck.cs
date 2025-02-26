using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02001123 RID: 4387
public static class AssetBundleCheck
{
	// Token: 0x17001F39 RID: 7993
	// (get) Token: 0x06009133 RID: 37171 RVA: 0x003C7055 File Offset: 0x003C5455
	public static bool IsSimulation
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06009134 RID: 37172 RVA: 0x003C7058 File Offset: 0x003C5458
	public static bool IsFile(string assetBundleName, string fileName = "")
	{
		return File.Exists(AssetBundleManager.BaseDownloadingURL + assetBundleName);
	}

	// Token: 0x06009135 RID: 37173 RVA: 0x003C7072 File Offset: 0x003C5472
	public static bool IsManifest(string manifest)
	{
		return AssetBundleManager.ManifestBundlePack.ContainsKey(manifest);
	}

	// Token: 0x06009136 RID: 37174 RVA: 0x003C707F File Offset: 0x003C547F
	public static bool IsManifestOrBundle(string bundle)
	{
		return AssetBundleManager.ManifestBundlePack.ContainsKey(bundle) || AssetBundleCheck.IsFile(bundle, string.Empty);
	}

	// Token: 0x06009137 RID: 37175 RVA: 0x003C70A0 File Offset: 0x003C54A0
	public static string[] GetAllAssetName(string assetBundleName, bool _WithExtension = true, string manifestAssetBundleName = null, bool isAllCheck = false)
	{
		if (manifestAssetBundleName == null && isAllCheck && AssetBundleManager.AllLoadedAssetBundleNames.Contains(assetBundleName))
		{
			foreach (KeyValuePair<string, AssetBundleManager.BundlePack> keyValuePair in AssetBundleManager.ManifestBundlePack)
			{
				LoadedAssetBundle loadedAssetBundle;
				if (keyValuePair.Value.LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle))
				{
					if (_WithExtension)
					{
						IEnumerable<string> allAssetNames = loadedAssetBundle.m_AssetBundle.GetAllAssetNames();
						if (AssetBundleCheck.<>f__mg$cache0 == null)
						{
							AssetBundleCheck.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
						}
						return allAssetNames.Select(AssetBundleCheck.<>f__mg$cache0).ToArray<string>();
					}
					IEnumerable<string> allAssetNames2 = loadedAssetBundle.m_AssetBundle.GetAllAssetNames();
					if (AssetBundleCheck.<>f__mg$cache1 == null)
					{
						AssetBundleCheck.<>f__mg$cache1 = new Func<string, string>(Path.GetFileNameWithoutExtension);
					}
					return allAssetNames2.Select(AssetBundleCheck.<>f__mg$cache1).ToArray<string>();
				}
			}
		}
		string text;
		LoadedAssetBundle loadedAssetBundle2 = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text, manifestAssetBundleName);
		AssetBundle assetBundle;
		if (loadedAssetBundle2 != null)
		{
			assetBundle = loadedAssetBundle2.m_AssetBundle;
		}
		else
		{
			assetBundle = AssetBundle.LoadFromFile(AssetBundleManager.BaseDownloadingURL + assetBundleName);
		}
		string[] result;
		if (_WithExtension)
		{
			IEnumerable<string> allAssetNames3 = assetBundle.GetAllAssetNames();
			if (AssetBundleCheck.<>f__mg$cache2 == null)
			{
				AssetBundleCheck.<>f__mg$cache2 = new Func<string, string>(Path.GetFileName);
			}
			result = allAssetNames3.Select(AssetBundleCheck.<>f__mg$cache2).ToArray<string>();
		}
		else
		{
			IEnumerable<string> allAssetNames4 = assetBundle.GetAllAssetNames();
			if (AssetBundleCheck.<>f__mg$cache3 == null)
			{
				AssetBundleCheck.<>f__mg$cache3 = new Func<string, string>(Path.GetFileNameWithoutExtension);
			}
			result = allAssetNames4.Select(AssetBundleCheck.<>f__mg$cache3).ToArray<string>();
		}
		if (loadedAssetBundle2 == null)
		{
			assetBundle.Unload(true);
		}
		return result;
	}

	// Token: 0x040075C6 RID: 30150
	[CompilerGenerated]
	private static Func<string, string> <>f__mg$cache0;

	// Token: 0x040075C7 RID: 30151
	[CompilerGenerated]
	private static Func<string, string> <>f__mg$cache1;

	// Token: 0x040075C8 RID: 30152
	[CompilerGenerated]
	private static Func<string, string> <>f__mg$cache2;

	// Token: 0x040075C9 RID: 30153
	[CompilerGenerated]
	private static Func<string, string> <>f__mg$cache3;
}
