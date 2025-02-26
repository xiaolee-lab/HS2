using System;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000926 RID: 2342
	public static class AssetUtility
	{
		// Token: 0x06004260 RID: 16992 RVA: 0x001A1987 File Offset: 0x0019FD87
		public static T LoadAsset<T>(AssetBundleInfo info) where T : UnityEngine.Object
		{
			return AssetUtility.LoadAsset<T>(info.assetbundle, info.asset, info.manifest);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x001A19A4 File Offset: 0x0019FDA4
		public static T LoadAsset<T>(string assetbundleName, string assetName, string manifestName = "") where T : UnityEngine.Object
		{
			manifestName = ((!manifestName.IsNullOrEmpty()) ? manifestName : null);
			T result = CommonLib.LoadAsset<T>(assetbundleName, assetName, false, manifestName);
			AssetBundleManager.UnloadAssetBundle(assetbundleName, true, manifestName, false);
			return result;
		}
	}
}
