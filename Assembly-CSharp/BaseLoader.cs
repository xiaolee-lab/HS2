using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using UnityEngine;

// Token: 0x02001135 RID: 4405
public class BaseLoader : MonoBehaviour
{
	// Token: 0x060091D1 RID: 37329 RVA: 0x000F0251 File Offset: 0x000EE651
	protected virtual void Awake()
	{
		if (!Singleton<AssetBundleManager>.IsInstance())
		{
		}
		this.Initialize();
		if (this.isErase)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060091D2 RID: 37330 RVA: 0x000F027C File Offset: 0x000EE67C
	protected void Initialize()
	{
		if (Singleton<AssetBundleManager>.IsInstance())
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(Application.dataPath);
		string value = "/../abdata" + '/';
		stringBuilder.Append(value);
		AssetBundleManager.Initialize(stringBuilder.ToString());
	}

	// Token: 0x060091D3 RID: 37331 RVA: 0x000F02CC File Offset: 0x000EE6CC
	public string GetRelativePath()
	{
		if (Application.isEditor)
		{
			return "file://" + Environment.CurrentDirectory.Replace("\\", "/");
		}
		if (Application.isMobilePlatform || Application.isConsolePlatform)
		{
			return Application.streamingAssetsPath;
		}
		return "file://" + Application.streamingAssetsPath;
	}

	// Token: 0x060091D4 RID: 37332 RVA: 0x000F032C File Offset: 0x000EE72C
	private static string GetPlatformFolderForAssetBundles(RuntimePlatform platform)
	{
		if (platform == RuntimePlatform.OSXPlayer)
		{
			return "OSX";
		}
		if (platform != RuntimePlatform.WindowsPlayer)
		{
			switch (platform)
			{
			case RuntimePlatform.IPhonePlayer:
				return "iOS";
			case RuntimePlatform.Android:
				return "Android";
			}
			return null;
		}
		return "Windows";
	}

	// Token: 0x060091D5 RID: 37333 RVA: 0x000F0380 File Offset: 0x000EE780
	protected T Load<T>(string assetBundleName, string assetName, bool isClone = false, string manifestAssetBundleName = null) where T : UnityEngine.Object
	{
		T t = new AssetBundleManifestData(assetBundleName, assetName, manifestAssetBundleName).GetAsset<T>();
		if (t != null && isClone)
		{
			T t2 = UnityEngine.Object.Instantiate<T>(t);
			t2.name = t.name;
			t = t2;
		}
		return t;
	}

	// Token: 0x060091D6 RID: 37334 RVA: 0x000F03D8 File Offset: 0x000EE7D8
	protected IEnumerator Load_Coroutine<T>(string assetBundleName, string assetName, Action<T> act = null, bool isClone = false, string manifestAssetBundleName = null) where T : UnityEngine.Object
	{
		T asset = (T)((object)null);
		yield return new AssetBundleManifestData(assetBundleName, assetName, manifestAssetBundleName).GetAsset<T>(delegate(T x)
		{
			asset = x;
		});
		if (asset == null)
		{
			yield break;
		}
		if (isClone)
		{
			T asset2 = UnityEngine.Object.Instantiate<T>(asset);
			asset2.name = asset.name;
			asset = asset2;
		}
		act.Call(asset);
		yield break;
	}

	// Token: 0x060091D7 RID: 37335 RVA: 0x000F0418 File Offset: 0x000EE818
	[Conditional("BASE_LOADER_LOG")]
	private void Log(string str)
	{
	}

	// Token: 0x0400760F RID: 30223
	[SerializeField]
	protected bool isErase;

	// Token: 0x04007610 RID: 30224
	public const string LocalPath = "file://";

	// Token: 0x04007611 RID: 30225
	public const string NetWorkPath = "http://";
}
