using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001293 RID: 4755
	public static class AssetBundleCheck
	{
		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06009D4A RID: 40266 RVA: 0x004042C6 File Offset: 0x004026C6
		public static bool IsSimulation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009D4B RID: 40267 RVA: 0x004042C9 File Offset: 0x004026C9
		public static bool IsManifest(string _manifest)
		{
			return AssetBundleManager.ManifestBundlePack.ContainsKey(_manifest);
		}

		// Token: 0x06009D4C RID: 40268 RVA: 0x004042D8 File Offset: 0x004026D8
		public static string[] FindAllAssetName(string assetBundleName, string _regex, bool _WithExtension = true, RegexOptions _options = RegexOptions.None)
		{
			_regex = _regex.ToLower();
			AssetBundle assetBundle = AssetBundle.LoadFromFile(AssetBundleManager.BaseDownloadingURL + assetBundleName);
			string[] result;
			if (_WithExtension)
			{
				IEnumerable<string> allAssetNames = assetBundle.GetAllAssetNames();
				if (AssetBundleCheck.<>f__mg$cache0 == null)
				{
					AssetBundleCheck.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
				}
				result = (from v in allAssetNames.Select(AssetBundleCheck.<>f__mg$cache0)
				where AssetBundleCheck.CheckRegex(v, _regex, _options)
				select v).ToArray<string>();
			}
			else
			{
				IEnumerable<string> allAssetNames2 = assetBundle.GetAllAssetNames();
				if (AssetBundleCheck.<>f__mg$cache1 == null)
				{
					AssetBundleCheck.<>f__mg$cache1 = new Func<string, string>(Path.GetFileNameWithoutExtension);
				}
				result = (from v in allAssetNames2.Select(AssetBundleCheck.<>f__mg$cache1)
				where AssetBundleCheck.CheckRegex(v, _regex, _options)
				select v).ToArray<string>();
			}
			assetBundle.Unload(true);
			return result;
		}

		// Token: 0x06009D4D RID: 40269 RVA: 0x004043B0 File Offset: 0x004027B0
		private static bool CheckRegex(string _value, string _regex, RegexOptions _options)
		{
			Match match = Regex.Match(_value, _regex, _options);
			return match.Success;
		}

		// Token: 0x06009D4E RID: 40270 RVA: 0x004043CC File Offset: 0x004027CC
		public static bool FindFile(string _assetBundleName, string _fineName, bool _WithExtension = false)
		{
			_fineName = _fineName.ToLower();
			AssetBundle assetBundle = AssetBundle.LoadFromFile(AssetBundleManager.BaseDownloadingURL + _assetBundleName);
			if (assetBundle == null)
			{
				return false;
			}
			bool result;
			if (_WithExtension)
			{
				IEnumerable<string> allAssetNames = assetBundle.GetAllAssetNames();
				if (AssetBundleCheck.<>f__mg$cache2 == null)
				{
					AssetBundleCheck.<>f__mg$cache2 = new Func<string, string>(Path.GetFileName);
				}
				result = (allAssetNames.Select(AssetBundleCheck.<>f__mg$cache2).ToList<string>().FindIndex((string s) => s == _fineName) != -1);
			}
			else
			{
				IEnumerable<string> allAssetNames2 = assetBundle.GetAllAssetNames();
				if (AssetBundleCheck.<>f__mg$cache3 == null)
				{
					AssetBundleCheck.<>f__mg$cache3 = new Func<string, string>(Path.GetFileNameWithoutExtension);
				}
				result = (allAssetNames2.Select(AssetBundleCheck.<>f__mg$cache3).ToList<string>().FindIndex((string s) => s == _fineName) != -1);
			}
			assetBundle.Unload(true);
			return result;
		}

		// Token: 0x06009D4F RID: 40271 RVA: 0x004044B8 File Offset: 0x004028B8
		public static string[] GetAllFileName(string _assetBundleName, bool _WithExtension = false)
		{
			string text;
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(_assetBundleName, out text, null);
			AssetBundle assetBundle;
			if (loadedAssetBundle != null)
			{
				assetBundle = loadedAssetBundle.m_AssetBundle;
			}
			else
			{
				assetBundle = AssetBundle.LoadFromFile(AssetBundleManager.BaseDownloadingURL + _assetBundleName);
			}
			string[] result;
			if (_WithExtension)
			{
				IEnumerable<string> allAssetNames = assetBundle.GetAllAssetNames();
				if (AssetBundleCheck.<>f__mg$cache4 == null)
				{
					AssetBundleCheck.<>f__mg$cache4 = new Func<string, string>(Path.GetFileName);
				}
				result = allAssetNames.Select(AssetBundleCheck.<>f__mg$cache4).ToArray<string>();
			}
			else
			{
				IEnumerable<string> allAssetNames2 = assetBundle.GetAllAssetNames();
				if (AssetBundleCheck.<>f__mg$cache5 == null)
				{
					AssetBundleCheck.<>f__mg$cache5 = new Func<string, string>(Path.GetFileNameWithoutExtension);
				}
				result = allAssetNames2.Select(AssetBundleCheck.<>f__mg$cache5).ToArray<string>();
			}
			if (loadedAssetBundle == null)
			{
				assetBundle.Unload(true);
			}
			return result;
		}

		// Token: 0x04007D2B RID: 32043
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;

		// Token: 0x04007D2C RID: 32044
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache1;

		// Token: 0x04007D2D RID: 32045
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache2;

		// Token: 0x04007D2E RID: 32046
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache3;

		// Token: 0x04007D2F RID: 32047
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache4;

		// Token: 0x04007D30 RID: 32048
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache5;
	}
}
