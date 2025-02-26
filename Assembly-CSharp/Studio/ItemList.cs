using System;
using System.Collections.Generic;
using Illusion.Extensions;
using Studio.Anime;
using Studio.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012DC RID: 4828
	public class ItemList : MonoBehaviour
	{
		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x0600A106 RID: 41222 RVA: 0x0042232B File Offset: 0x0042072B
		// (set) Token: 0x0600A107 RID: 41223 RVA: 0x00422338 File Offset: 0x00420738
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
					if (!value)
					{
						this.category = -1;
					}
				}
			}
		}

		// Token: 0x0600A108 RID: 41224 RVA: 0x00422364 File Offset: 0x00420764
		public void InitList(int _group, int _category)
		{
			this.Init();
			if (this.group == _group && this.category == _category)
			{
				return;
			}
			this.listNodePool.Return();
			this.scrollRect.verticalNormalizedPosition = 1f;
			foreach (KeyValuePair<int, Info.ItemLoadInfo> keyValuePair in Singleton<Info>.Instance.dicItemLoadInfo[_group][_category])
			{
				int no = keyValuePair.Key;
				ListNode listNode = this.listNodePool.Rent(keyValuePair.Value.name, delegate()
				{
					this.OnSelect(no);
				}, true);
				ItemColorData.ColorData colorData = Singleton<Info>.Instance.SafeGetItemColorData(_group, _category, keyValuePair.Key);
				switch ((colorData == null) ? 0 : colorData.Count)
				{
				case 1:
					listNode.TextColor = Color.red;
					break;
				case 2:
					listNode.TextColor = Color.cyan;
					break;
				case 3:
					listNode.TextColor = Color.green;
					break;
				case 4:
					listNode.TextColor = Color.yellow;
					break;
				default:
					listNode.TextColor = Color.white;
					break;
				}
			}
			base.gameObject.SetActiveIfDifferent(true);
			this.group = _group;
			this.category = _category;
		}

		// Token: 0x0600A109 RID: 41225 RVA: 0x004224F8 File Offset: 0x004208F8
		private void OnSelect(int _no)
		{
			Singleton<Studio>.Instance.AddItem(this.group, this.category, _no);
		}

		// Token: 0x0600A10A RID: 41226 RVA: 0x00422511 File Offset: 0x00420911
		private void Init()
		{
			if (this.isInit)
			{
				return;
			}
			this.listNodePool = new ListNodePool(this.transformRoot, this.objectNode.GetComponent<ListNode>());
			this.isInit = true;
			this.group = -1;
			this.category = -1;
		}

		// Token: 0x04007F37 RID: 32567
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F38 RID: 32568
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007F39 RID: 32569
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007F3A RID: 32570
		[SerializeField]
		private Material[] materialsTextMesh;

		// Token: 0x04007F3B RID: 32571
		private ListNodePool listNodePool;

		// Token: 0x04007F3C RID: 32572
		private bool isInit;

		// Token: 0x04007F3D RID: 32573
		private int group = -1;

		// Token: 0x04007F3E RID: 32574
		private int category = -1;
	}
}
