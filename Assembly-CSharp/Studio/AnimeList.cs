using System;
using System.Collections.Generic;
using Studio.Anime;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012E5 RID: 4837
	public class AnimeList : MonoBehaviour
	{
		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x0600A16E RID: 41326 RVA: 0x004245D5 File Offset: 0x004229D5
		// (set) Token: 0x0600A16F RID: 41327 RVA: 0x004245E2 File Offset: 0x004229E2
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
				}
			}
		}

		// Token: 0x0600A170 RID: 41328 RVA: 0x00424604 File Offset: 0x00422A04
		public void InitList(AnimeGroupList.SEX _sex, int _group, int _category)
		{
			this.Init();
			this.listNodePool.Return();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.dicNode.Clear();
			foreach (KeyValuePair<int, Info.AnimeLoadInfo> keyValuePair in Singleton<Info>.Instance.dicAnimeLoadInfo[_group][_category])
			{
				int no = keyValuePair.Key;
				ListNode value = this.listNodePool.Rent(keyValuePair.Value.name, delegate()
				{
					this.OnSelect(no);
				});
				this.dicNode.Add(keyValuePair.Key, value);
			}
			this.sex = _sex;
			this.group = _group;
			this.category = _category;
			this.select = -1;
			this.active = true;
		}

		// Token: 0x0600A171 RID: 41329 RVA: 0x0042470C File Offset: 0x00422B0C
		private void OnSelect(int _no)
		{
			this.mpCharCtrl.LoadAnime(this.sex, this.group, this.category, _no);
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
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

		// Token: 0x0600A172 RID: 41330 RVA: 0x004247A4 File Offset: 0x00422BA4
		private void Init()
		{
			if (this.isInit)
			{
				return;
			}
			this.listNodePool = new AnimeList.ListNodePool(this.transformRoot, this.objectPrefab.GetComponent<ListNode>());
			this.dicNode = new Dictionary<int, ListNode>();
			this.isInit = true;
		}

		// Token: 0x04007F99 RID: 32665
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F9A RID: 32666
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007F9B RID: 32667
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007F9C RID: 32668
		[SerializeField]
		private MPCharCtrl mpCharCtrl;

		// Token: 0x04007F9D RID: 32669
		private AnimeList.ListNodePool listNodePool;

		// Token: 0x04007F9E RID: 32670
		private bool isInit;

		// Token: 0x04007F9F RID: 32671
		private AnimeGroupList.SEX sex = AnimeGroupList.SEX.Unknown;

		// Token: 0x04007FA0 RID: 32672
		private int group = -1;

		// Token: 0x04007FA1 RID: 32673
		private int category = -1;

		// Token: 0x04007FA2 RID: 32674
		private int select = -1;

		// Token: 0x04007FA3 RID: 32675
		private Dictionary<int, ListNode> dicNode;

		// Token: 0x020012E6 RID: 4838
		private class ListNodePool : ObjectPool<ListNode>
		{
			// Token: 0x0600A173 RID: 41331 RVA: 0x004247E0 File Offset: 0x00422BE0
			public ListNodePool(Transform _parent, ListNode _prefab)
			{
				this.parent = _parent;
				this.prefab = _prefab;
				this.nodes = new List<ListNode>();
			}

			// Token: 0x0600A174 RID: 41332 RVA: 0x00424804 File Offset: 0x00422C04
			protected override ListNode CreateInstance()
			{
				return UnityEngine.Object.Instantiate<ListNode>(this.prefab, this.parent);
			}

			// Token: 0x0600A175 RID: 41333 RVA: 0x00424824 File Offset: 0x00422C24
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

			// Token: 0x0600A176 RID: 41334 RVA: 0x00424868 File Offset: 0x00422C68
			public void Return()
			{
				foreach (ListNode instance in this.nodes)
				{
					base.Return(instance);
				}
				this.nodes.Clear();
			}

			// Token: 0x04007FA4 RID: 32676
			private readonly Transform parent;

			// Token: 0x04007FA5 RID: 32677
			private readonly ListNode prefab;

			// Token: 0x04007FA6 RID: 32678
			private List<ListNode> nodes;
		}
	}
}
