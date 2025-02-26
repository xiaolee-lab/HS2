using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E70 RID: 3696
	public class RecipeViewer : MonoBehaviour
	{
		// Token: 0x0600759C RID: 30108 RVA: 0x0031D65C File Offset: 0x0031BA5C
		public static IEnumerator Load(CraftUI craftUI, Transform viewerParent, Action<RecipeViewer> onComplete)
		{
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			GameObject asset = CommonLib.LoadAsset<GameObject>(bundle, "RecipeViewer", false, string.Empty);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
			}
			RecipeViewer viewer = UnityEngine.Object.Instantiate<GameObject>(asset, viewerParent, false).GetComponent<RecipeViewer>();
			viewer._craftUI = craftUI;
			onComplete.Call(viewer);
			yield break;
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x0600759D RID: 30109 RVA: 0x0031D685 File Offset: 0x0031BA85
		// (set) Token: 0x0600759E RID: 30110 RVA: 0x0031D68D File Offset: 0x0031BA8D
		public bool initialized { get; private set; }

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x0600759F RID: 30111 RVA: 0x0031D696 File Offset: 0x0031BA96
		public RecipeItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x0031D69E File Offset: 0x0031BA9E
		private void ResetScroll()
		{
			if (this._resetScrollTargetPos == null)
			{
				return;
			}
			this._resetScrollTarget.anchoredPosition = this._resetScrollTargetPos.Value;
		}

		// Token: 0x060075A1 RID: 30113 RVA: 0x0031D6C8 File Offset: 0x0031BAC8
		private void Awake()
		{
			this._resetScrollTargetPos = ((this._resetScrollTarget != null) ? new Vector2?(this._resetScrollTarget.anchoredPosition) : null);
		}

		// Token: 0x060075A2 RID: 30114 RVA: 0x0031D704 File Offset: 0x0031BB04
		private IEnumerator Start()
		{
			this.itemListUI.SetCraftUI(this._craftUI);
			if (!this.itemListUI.isOptionNode)
			{
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "RecipeItemTitleOption", false, string.Empty);
				if (gameObject == null)
				{
					yield break;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
				}
				this.itemListUI.SetOptionNode(gameObject);
			}
			this.initialized = true;
			yield break;
		}

		// Token: 0x04005FE0 RID: 24544
		[SerializeField]
		private CraftUI _craftUI;

		// Token: 0x04005FE1 RID: 24545
		[SerializeField]
		private RecipeItemListUI _itemListUI;

		// Token: 0x04005FE2 RID: 24546
		[SerializeField]
		private RectTransform _resetScrollTarget;

		// Token: 0x04005FE3 RID: 24547
		private Vector2? _resetScrollTargetPos;
	}
}
