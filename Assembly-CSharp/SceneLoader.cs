using System;
using System.Collections;
using Manager;
using UnityEngine;

// Token: 0x02001136 RID: 4406
public class SceneLoader : BaseLoader
{
	// Token: 0x17001F55 RID: 8021
	// (get) Token: 0x060091D9 RID: 37337 RVA: 0x003C96C8 File Offset: 0x003C7AC8
	// (set) Token: 0x060091DA RID: 37338 RVA: 0x003C96D0 File Offset: 0x003C7AD0
	public Func<IEnumerator> onFadeIn { get; set; }

	// Token: 0x17001F56 RID: 8022
	// (get) Token: 0x060091DB RID: 37339 RVA: 0x003C96D9 File Offset: 0x003C7AD9
	// (set) Token: 0x060091DC RID: 37340 RVA: 0x003C96E1 File Offset: 0x003C7AE1
	public Func<IEnumerator> onFadeOut { get; set; }

	// Token: 0x17001F57 RID: 8023
	// (get) Token: 0x060091DD RID: 37341 RVA: 0x003C96EA File Offset: 0x003C7AEA
	// (set) Token: 0x060091DE RID: 37342 RVA: 0x003C96F2 File Offset: 0x003C7AF2
	public Scene.Data.FadeType fadeType { get; set; }

	// Token: 0x060091DF RID: 37343 RVA: 0x003C96FC File Offset: 0x003C7AFC
	protected virtual void Start()
	{
		Scene.Data data = new Scene.Data
		{
			assetBundleName = this.assetBundleName,
			levelName = this.levelName,
			isAdd = !this.isLoad,
			isAsync = this.isAsync,
			isOverlap = this.isOverlap,
			manifestFileName = this.manifestFileName,
			onLoad = this.onLoad,
			onFadeIn = this.onFadeIn,
			onFadeOut = this.onFadeOut
		};
		if (this.isFade)
		{
			data.isFade = this.isFade;
		}
		else
		{
			data.fadeType = this.fadeType;
		}
		Singleton<Scene>.Instance.LoadReserve(data, this.isLoadingImageDraw);
		if (this.isStartAfterErase)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04007612 RID: 30226
	public string assetBundleName;

	// Token: 0x04007613 RID: 30227
	public string levelName;

	// Token: 0x04007614 RID: 30228
	public bool isLoad = true;

	// Token: 0x04007615 RID: 30229
	public bool isAsync = true;

	// Token: 0x04007616 RID: 30230
	public bool isFade = true;

	// Token: 0x04007617 RID: 30231
	public bool isOverlap;

	// Token: 0x04007618 RID: 30232
	public bool isLoadingImageDraw = true;

	// Token: 0x04007619 RID: 30233
	public string manifestFileName;

	// Token: 0x0400761A RID: 30234
	public Action onLoad;

	// Token: 0x0400761E RID: 30238
	[SerializeField]
	protected bool isStartAfterErase = true;
}
