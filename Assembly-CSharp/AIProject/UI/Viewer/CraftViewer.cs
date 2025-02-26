using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E6B RID: 3691
	public class CraftViewer : MonoBehaviour
	{
		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x0600752A RID: 29994 RVA: 0x0031BF3B File Offset: 0x0031A33B
		// (set) Token: 0x0600752B RID: 29995 RVA: 0x0031BF43 File Offset: 0x0031A343
		public bool initialized { get; private set; }

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x0600752C RID: 29996 RVA: 0x0031BF4C File Offset: 0x0031A34C
		// (set) Token: 0x0600752D RID: 29997 RVA: 0x0031BF59 File Offset: 0x0031A359
		public bool Visible
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				base.gameObject.SetActive(value);
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x0600752E RID: 29998 RVA: 0x0031BF67 File Offset: 0x0031A367
		public CraftItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x0600752F RID: 29999 RVA: 0x0031BF6F File Offset: 0x0031A36F
		public Image cursor
		{
			[CompilerGenerated]
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x06007530 RID: 30000 RVA: 0x0031BF77 File Offset: 0x0031A377
		public void SetID(int id)
		{
			this._id = id;
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x0031BF80 File Offset: 0x0031A380
		public void SetIcon(Sprite sprite)
		{
			this._icon.sprite = sprite;
		}

		// Token: 0x06007532 RID: 30002 RVA: 0x0031BF8E File Offset: 0x0031A38E
		public void SetIcon(string text)
		{
			this._text.text = text;
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x0031BF9C File Offset: 0x0031A39C
		private static CraftItemNodeUI.Possible Possible(IReadOnlyCollection<IReadOnlyCollection<StuffItem>> storages, int nameHash, RecipeDataInfo[] info)
		{
			PlayerData playerData = Singleton<Map>.Instance.Player.PlayerData;
			HashSet<int> craftPossibleTable = playerData.CraftPossibleTable;
			bool flag = info.Any((RecipeDataInfo x) => x.NeedList.All((RecipeDataInfo.NeedData need) => storages.SelectMany((IReadOnlyCollection<StuffItem> storage) => storage).FindItems(new StuffItem(need.CategoryID, need.ID, 0)).Sum((StuffItem p) => p.Count) / need.Sum > 0));
			if (flag)
			{
				craftPossibleTable.Add(nameHash);
			}
			if (playerData.FirstCreatedItemTable.Contains(nameHash))
			{
				flag = false;
			}
			return new CraftItemNodeUI.Possible(!craftPossibleTable.Contains(nameHash), flag);
		}

		// Token: 0x06007534 RID: 30004 RVA: 0x0031C014 File Offset: 0x0031A414
		public void Refresh(CraftUI craftUI)
		{
			this.itemListUI.ClearItems();
			this.ResetScroll();
			craftUI.SetID(this._id);
			foreach (var <>__AnonType in this._info.Select((KeyValuePair<int, RecipeDataInfo[]> p, int i) => new
			{
				p,
				i
			}))
			{
				int hash = <>__AnonType.p.Key;
				RecipeDataInfo[] recipeInfo = <>__AnonType.p.Value;
				StuffItemInfo info = Singleton<Manager.Resources>.Instance.GameInfo.FindItemInfo(hash);
				this.itemListUI.AddItemNode(<>__AnonType.i, new CraftItemNodeUI.StuffItemInfoPack(info, () => CraftViewer.Possible(craftUI.checkStorages, hash, recipeInfo)), recipeInfo);
			}
		}

		// Token: 0x06007535 RID: 30005 RVA: 0x0031C130 File Offset: 0x0031A530
		private void ResetScroll()
		{
			if (this._resetScrollTargetPos == null)
			{
				return;
			}
			this._resetScrollTarget.anchoredPosition = this._resetScrollTargetPos.Value;
		}

		// Token: 0x06007536 RID: 30006 RVA: 0x0031C15C File Offset: 0x0031A55C
		private void Awake()
		{
			this._resetScrollTargetPos = ((this._resetScrollTarget != null) ? new Vector2?(this._resetScrollTarget.anchoredPosition) : null);
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x0031C198 File Offset: 0x0031A598
		private IEnumerator Start()
		{
			while (this._id == -1)
			{
				yield return null;
			}
			if (!this.itemListUI.isOptionNode)
			{
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "CraftItemOption", false, string.Empty);
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
			this._info = Singleton<Manager.Resources>.Instance.GameInfo.recipe[this._id];
			this.initialized = true;
			yield break;
		}

		// Token: 0x06007538 RID: 30008 RVA: 0x0031C1B3 File Offset: 0x0031A5B3
		private void OnEnable()
		{
			this.ResetScroll();
		}

		// Token: 0x04005F90 RID: 24464
		[SerializeField]
		private CraftItemListUI _itemListUI;

		// Token: 0x04005F91 RID: 24465
		[SerializeField]
		private Image _cursor;

		// Token: 0x04005F92 RID: 24466
		[SerializeField]
		private RectTransform _resetScrollTarget;

		// Token: 0x04005F93 RID: 24467
		private Vector2? _resetScrollTargetPos;

		// Token: 0x04005F94 RID: 24468
		private int _id = -1;

		// Token: 0x04005F95 RID: 24469
		[SerializeField]
		private Image _icon;

		// Token: 0x04005F96 RID: 24470
		[SerializeField]
		private Text _text;

		// Token: 0x04005F97 RID: 24471
		private IReadOnlyDictionary<int, RecipeDataInfo[]> _info;
	}
}
