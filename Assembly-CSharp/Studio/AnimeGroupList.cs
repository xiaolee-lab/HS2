using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012E3 RID: 4835
	public class AnimeGroupList : MonoBehaviour
	{
		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x0600A168 RID: 41320 RVA: 0x004242CE File Offset: 0x004226CE
		// (set) Token: 0x0600A169 RID: 41321 RVA: 0x004242DB File Offset: 0x004226DB
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
						this.animeCategoryList.active = false;
					}
				}
			}
		}

		// Token: 0x0600A16A RID: 41322 RVA: 0x00424318 File Offset: 0x00422718
		public void InitList(AnimeGroupList.SEX _sex)
		{
			if (this.isInit)
			{
				return;
			}
			int childCount = this.transformRoot.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.dicNode = new Dictionary<int, Image>();
			this.sex = _sex;
			foreach (KeyValuePair<int, Info.GroupInfo> keyValuePair in from _v in Singleton<Info>.Instance.dicAGroupCategory
			orderby _v.Value.sort
			select _v)
			{
				if (Singleton<Info>.Instance.ExistAnimeGroup(keyValuePair.Key))
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
					this.dicNode.Add(keyValuePair.Key, component.image);
				}
			}
			this.select = -1;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.animeCategoryList.active = false;
			this.animeList.active = false;
			this.isInit = true;
		}

		// Token: 0x0600A16B RID: 41323 RVA: 0x004244F4 File Offset: 0x004228F4
		private void OnSelect(int _no)
		{
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
				this.animeCategoryList.InitList(this.sex, _no);
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

		// Token: 0x04007F8B RID: 32651
		public AnimeGroupList.SEX sex = AnimeGroupList.SEX.Unknown;

		// Token: 0x04007F8C RID: 32652
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F8D RID: 32653
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007F8E RID: 32654
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007F8F RID: 32655
		[SerializeField]
		private AnimeCategoryList animeCategoryList;

		// Token: 0x04007F90 RID: 32656
		[SerializeField]
		private AnimeList animeList;

		// Token: 0x04007F91 RID: 32657
		private int select = -1;

		// Token: 0x04007F92 RID: 32658
		private Dictionary<int, Image> dicNode;

		// Token: 0x04007F93 RID: 32659
		private bool isInit;

		// Token: 0x020012E4 RID: 4836
		public enum SEX
		{
			// Token: 0x04007F96 RID: 32662
			Male,
			// Token: 0x04007F97 RID: 32663
			Female,
			// Token: 0x04007F98 RID: 32664
			Unknown
		}
	}
}
