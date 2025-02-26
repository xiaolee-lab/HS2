using System;
using System.Collections;
using UnityEngine;

// Token: 0x02001131 RID: 4401
public class AssetLoader : BaseLoader
{
	// Token: 0x17001F50 RID: 8016
	// (get) Token: 0x060091B3 RID: 37299 RVA: 0x003C8666 File Offset: 0x003C6A66
	// (set) Token: 0x060091B4 RID: 37300 RVA: 0x003C866E File Offset: 0x003C6A6E
	public UnityEngine.Object loadObject { get; protected set; }

	// Token: 0x17001F51 RID: 8017
	// (get) Token: 0x060091B5 RID: 37301 RVA: 0x003C8677 File Offset: 0x003C6A77
	// (set) Token: 0x060091B6 RID: 37302 RVA: 0x003C867F File Offset: 0x003C6A7F
	public bool isLoadEnd { get; private set; }

	// Token: 0x17001F52 RID: 8018
	// (get) Token: 0x060091B7 RID: 37303 RVA: 0x003C8688 File Offset: 0x003C6A88
	// (set) Token: 0x060091B8 RID: 37304 RVA: 0x003C8690 File Offset: 0x003C6A90
	public bool initialized { get; protected set; }

	// Token: 0x060091B9 RID: 37305 RVA: 0x003C8699 File Offset: 0x003C6A99
	public void Init()
	{
		base.StartCoroutine(this._Init());
	}

	// Token: 0x060091BA RID: 37306 RVA: 0x003C86A8 File Offset: 0x003C6AA8
	public virtual IEnumerator _Init()
	{
		if (this.initialized)
		{
			yield break;
		}
		this.initialized = true;
		if (this.isLoad)
		{
			if (this.isAsync)
			{
				yield return base.StartCoroutine(base.Load_Coroutine<UnityEngine.Object>(this.assetBundleName, this.assetName, delegate(UnityEngine.Object o)
				{
					this.loadObject = o;
				}, this.isClone, this.manifestFileName));
			}
			else
			{
				this.loadObject = base.Load<UnityEngine.Object>(this.assetBundleName, this.assetName, this.isClone, this.manifestFileName);
			}
		}
		this.isLoadEnd = true;
		yield break;
	}

	// Token: 0x060091BB RID: 37307 RVA: 0x003C86C4 File Offset: 0x003C6AC4
	protected virtual IEnumerator Start()
	{
		if (this.initialized)
		{
			yield break;
		}
		yield return base.StartCoroutine(this._Init());
		yield break;
	}

	// Token: 0x060091BC RID: 37308 RVA: 0x003C86DF File Offset: 0x003C6ADF
	protected virtual void OnDestroy()
	{
		if (!this.isLoadEnd)
		{
			return;
		}
		if (this.isBundleUnload && Singleton<AssetBundleManager>.IsInstance())
		{
			AssetBundleManager.UnloadAssetBundle(this.assetBundleName, this.isUnloadForceRefCount, this.manifestFileName, this.unloadAllLoadedObjects);
		}
	}

	// Token: 0x040075F8 RID: 30200
	public string assetBundleName;

	// Token: 0x040075F9 RID: 30201
	public string assetName;

	// Token: 0x040075FA RID: 30202
	public bool isAsync = true;

	// Token: 0x040075FB RID: 30203
	public bool isBundleUnload = true;

	// Token: 0x040075FC RID: 30204
	public bool isUnloadForceRefCount;

	// Token: 0x040075FD RID: 30205
	public bool unloadAllLoadedObjects;

	// Token: 0x040075FE RID: 30206
	public string manifestFileName;

	// Token: 0x040075FF RID: 30207
	[SerializeField]
	protected bool isLoad = true;

	// Token: 0x04007600 RID: 30208
	[SerializeField]
	protected bool isClone;
}
