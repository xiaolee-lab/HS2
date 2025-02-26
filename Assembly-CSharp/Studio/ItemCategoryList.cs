using System;
using System.Collections.Generic;
using System.Linq;
using Studio.Anime;
using Studio.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012DA RID: 4826
	public class ItemCategoryList : MonoBehaviour
	{
		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x0600A0F9 RID: 41209 RVA: 0x00421DEE File Offset: 0x004201EE
		// (set) Token: 0x0600A0FA RID: 41210 RVA: 0x00421DFB File Offset: 0x004201FB
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
						this.itemList.active = false;
					}
				}
			}
		}

		// Token: 0x0600A0FB RID: 41211 RVA: 0x00421E38 File Offset: 0x00420238
		public void InitList(int _group)
		{
			this.Init();
			this.listNodePool.Return();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.dicNode.Clear();
			foreach (KeyValuePair<int, Info.CategoryInfo> keyValuePair in from v in Singleton<Info>.Instance.dicItemGroupCategory[_group].dicCategory
			orderby v.Value.sort
			select v)
			{
				if (Singleton<Info>.Instance.ExistItemCategory(_group, keyValuePair.Key))
				{
					int no = keyValuePair.Key;
					ListNode value = this.listNodePool.Rent(keyValuePair.Value.name, delegate()
					{
						this.OnSelect(no);
					}, false);
					this.dicNode.Add(keyValuePair.Key, value);
				}
			}
			this.select = -1;
			this.group = _group;
			this.active = true;
			this.itemList.active = false;
		}

		// Token: 0x0600A0FC RID: 41212 RVA: 0x00421F78 File Offset: 0x00420378
		private void OnSelect(int _no)
		{
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
				this.itemList.InitList(this.group, _no);
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

		// Token: 0x0600A0FD RID: 41213 RVA: 0x00422004 File Offset: 0x00420404
		private void Init()
		{
			if (this.isInit)
			{
				return;
			}
			this.listNodePool = new ListNodePool(this.transformRoot, this.objectPrefab.GetComponent<ListNode>());
			this.dicNode = new Dictionary<int, ListNode>();
			this.isInit = true;
		}

		// Token: 0x04007F26 RID: 32550
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F27 RID: 32551
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007F28 RID: 32552
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007F29 RID: 32553
		[SerializeField]
		private ItemList itemList;

		// Token: 0x04007F2A RID: 32554
		private ListNodePool listNodePool;

		// Token: 0x04007F2B RID: 32555
		private Dictionary<int, ListNode> dicNode;

		// Token: 0x04007F2C RID: 32556
		private bool isInit;

		// Token: 0x04007F2D RID: 32557
		private int group = -1;

		// Token: 0x04007F2E RID: 32558
		private int select = -1;
	}
}
