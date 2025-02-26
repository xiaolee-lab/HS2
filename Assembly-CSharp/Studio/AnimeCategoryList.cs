using System;
using System.Collections.Generic;
using System.Linq;
using Studio.Anime;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012E0 RID: 4832
	public class AnimeCategoryList : MonoBehaviour
	{
		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x0600A126 RID: 41254 RVA: 0x00422C39 File Offset: 0x00421039
		// (set) Token: 0x0600A127 RID: 41255 RVA: 0x00422C46 File Offset: 0x00421046
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				if (base.gameObject.activeSelf != value)
				{
					base.gameObject.SetActive(value);
					if (!base.gameObject.activeSelf)
					{
						this.animeList.active = false;
					}
				}
			}
		}

		// Token: 0x0600A128 RID: 41256 RVA: 0x00422C84 File Offset: 0x00421084
		public void InitList(AnimeGroupList.SEX _sex, int _group)
		{
			this.Init();
			this.listNodePool.Return();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.dicNode.Clear();
			foreach (KeyValuePair<int, Info.CategoryInfo> keyValuePair in from v in Singleton<Info>.Instance.dicAGroupCategory[_group].dicCategory
			orderby v.Value.sort
			select v)
			{
				if (Singleton<Info>.Instance.ExistAnimeCategory(_group, keyValuePair.Key))
				{
					int no = keyValuePair.Key;
					ListNode value = this.listNodePool.Rent(keyValuePair.Value.name, delegate()
					{
						this.OnSelect(no);
					});
					this.dicNode.Add(keyValuePair.Key, value);
				}
			}
			this.select = -1;
			this.group = _group;
			this.sex = _sex;
			this.active = true;
			this.animeList.active = false;
		}

		// Token: 0x0600A129 RID: 41257 RVA: 0x00422DC8 File Offset: 0x004211C8
		private bool CheckCategory(int _group, int _category, Dictionary<int, Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>>> _dic)
		{
			Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>> dictionary = null;
			return _dic.TryGetValue(_group, out dictionary) && dictionary.ContainsKey(_category);
		}

		// Token: 0x0600A12A RID: 41258 RVA: 0x00422DF0 File Offset: 0x004211F0
		private void OnSelect(int _no)
		{
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
				this.animeList.InitList(this.sex, this.group, _no);
				ListNode listNode = null;
				if (this.dicNode.TryGetValue(key, out listNode) && listNode != null)
				{
					listNode.Select = false;
				}
				listNode = null;
				if (this.dicNode.TryGetValue(this.select, out listNode) && listNode != null)
				{
					listNode.Select = true;
				}
			}
		}

		// Token: 0x0600A12B RID: 41259 RVA: 0x00422E82 File Offset: 0x00421282
		private void Init()
		{
			if (this.isInit)
			{
				return;
			}
			this.listNodePool = new AnimeCategoryList.ListNodePool(this.transformRoot, this.objectPrefab.GetComponent<ListNode>());
			this.dicNode = new Dictionary<int, ListNode>();
			this.isInit = true;
		}

		// Token: 0x04007F4E RID: 32590
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F4F RID: 32591
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007F50 RID: 32592
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007F51 RID: 32593
		[SerializeField]
		private AnimeList animeList;

		// Token: 0x04007F52 RID: 32594
		private AnimeCategoryList.ListNodePool listNodePool;

		// Token: 0x04007F53 RID: 32595
		private bool isInit;

		// Token: 0x04007F54 RID: 32596
		private AnimeGroupList.SEX sex = AnimeGroupList.SEX.Unknown;

		// Token: 0x04007F55 RID: 32597
		private int group = -1;

		// Token: 0x04007F56 RID: 32598
		private int select = -1;

		// Token: 0x04007F57 RID: 32599
		private Dictionary<int, ListNode> dicNode;

		// Token: 0x020012E1 RID: 4833
		private class ListNodePool : ObjectPool<ListNode>
		{
			// Token: 0x0600A12D RID: 41261 RVA: 0x00422ECC File Offset: 0x004212CC
			public ListNodePool(Transform _parent, ListNode _prefab)
			{
				this.parent = _parent;
				this.prefab = _prefab;
				this.nodes = new List<ListNode>();
			}

			// Token: 0x0600A12E RID: 41262 RVA: 0x00422EF0 File Offset: 0x004212F0
			protected override ListNode CreateInstance()
			{
				return UnityEngine.Object.Instantiate<ListNode>(this.prefab, this.parent);
			}

			// Token: 0x0600A12F RID: 41263 RVA: 0x00422F10 File Offset: 0x00421310
			public ListNode Rent(string _text, UnityAction _action)
			{
				ListNode listNode = base.Rent();
				listNode.transform.SetAsLastSibling();
				listNode.Select = false;
				this.nodes.Add(listNode);
				listNode.Text = _text;
				listNode.SetButtonAction(_action);
				return listNode;
			}

			// Token: 0x0600A130 RID: 41264 RVA: 0x00422F54 File Offset: 0x00421354
			public void Return()
			{
				foreach (ListNode instance in this.nodes)
				{
					base.Return(instance);
				}
				this.nodes.Clear();
			}

			// Token: 0x04007F59 RID: 32601
			private readonly Transform parent;

			// Token: 0x04007F5A RID: 32602
			private readonly ListNode prefab;

			// Token: 0x04007F5B RID: 32603
			private List<ListNode> nodes;
		}
	}
}
