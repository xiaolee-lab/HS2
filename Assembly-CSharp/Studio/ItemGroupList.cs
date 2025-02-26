using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012DB RID: 4827
	public class ItemGroupList : MonoBehaviour
	{
		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x0600A100 RID: 41216 RVA: 0x00422078 File Offset: 0x00420478
		// (set) Token: 0x0600A101 RID: 41217 RVA: 0x00422085 File Offset: 0x00420485
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
						this.itemCategoryList.active = false;
					}
				}
			}
		}

		// Token: 0x0600A102 RID: 41218 RVA: 0x004220C0 File Offset: 0x004204C0
		public void InitList()
		{
			int childCount = this.transformRoot.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.dicNode = new Dictionary<int, Image>();
			foreach (KeyValuePair<int, Info.GroupInfo> keyValuePair in Singleton<Info>.Instance.dicItemGroupCategory)
			{
				if (Singleton<Info>.Instance.ExistItemGroup(keyValuePair.Key))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectPrefab);
					if (!gameObject.activeSelf)
					{
						gameObject.SetActive(true);
					}
					gameObject.transform.SetParent(this.transformRoot, false);
					ListNode component = gameObject.GetComponent<ListNode>();
					int no = keyValuePair.Key;
					component.AddActionToButton(delegate
					{
						this.OnSelect(no);
					});
					component.text = keyValuePair.Value.name;
					this.dicNode.Add(keyValuePair.Key, gameObject.GetComponent<Image>());
				}
			}
			this.select = -1;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.itemCategoryList.active = false;
			this.itemList.active = false;
		}

		// Token: 0x0600A103 RID: 41219 RVA: 0x00422264 File Offset: 0x00420664
		private void OnSelect(int _no)
		{
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
				this.itemCategoryList.InitList(_no);
				Image image = null;
				if (this.dicNode.TryGetValue(key, out image) && image != null)
				{
					image.color = Color.white;
				}
				image = null;
				if (this.dicNode.TryGetValue(this.select, out image) && image != null)
				{
					image.color = Color.green;
				}
			}
		}

		// Token: 0x0600A104 RID: 41220 RVA: 0x004222F2 File Offset: 0x004206F2
		private void Start()
		{
			this.InitList();
		}

		// Token: 0x04007F30 RID: 32560
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F31 RID: 32561
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007F32 RID: 32562
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007F33 RID: 32563
		[SerializeField]
		private ItemCategoryList itemCategoryList;

		// Token: 0x04007F34 RID: 32564
		[SerializeField]
		private ItemList itemList;

		// Token: 0x04007F35 RID: 32565
		private int select = -1;

		// Token: 0x04007F36 RID: 32566
		private Dictionary<int, Image> dicNode;
	}
}
