using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SceneAssist
{
	// Token: 0x02001023 RID: 4131
	public static class AssetBundleCheck
	{
		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x06008A88 RID: 35464 RVA: 0x003A416C File Offset: 0x003A256C
		public static bool IsSimulation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x003A4170 File Offset: 0x003A2570
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

		// Token: 0x06008A8A RID: 35466 RVA: 0x003A4248 File Offset: 0x003A2648
		private static bool CheckRegex(string _value, string _regex, RegexOptions _options)
		{
			Match match = Regex.Match(_value, _regex, _options);
			return match.Success;
		}

		// Token: 0x06008A8B RID: 35467 RVA: 0x003A4264 File Offset: 0x003A2664
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

		// Token: 0x04007107 RID: 28935
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;

		// Token: 0x04007108 RID: 28936
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache1;

		// Token: 0x04007109 RID: 28937
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache2;

		// Token: 0x0400710A RID: 28938
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache3;
	}
}
