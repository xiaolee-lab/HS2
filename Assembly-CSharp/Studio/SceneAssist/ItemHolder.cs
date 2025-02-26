using System;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace Studio.SceneAssist
{
	// Token: 0x02001296 RID: 4758
	public class ItemHolder : MonoBehaviour
	{
		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06009D55 RID: 40277 RVA: 0x00404736 File Offset: 0x00402B36
		// (set) Token: 0x06009D56 RID: 40278 RVA: 0x0040473E File Offset: 0x00402B3E
		public ChaControl CharFemale { get; private set; }

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06009D57 RID: 40279 RVA: 0x00404747 File Offset: 0x00402B47
		public AnimatorStateInfo NowState
		{
			get
			{
				return this.CharFemale.getAnimatorStateInfo(0);
			}
		}

		// Token: 0x06009D58 RID: 40280 RVA: 0x00404755 File Offset: 0x00402B55
		public void PlayAnime(string _name, int _layer = 0)
		{
		}

		// Token: 0x06009D59 RID: 40281 RVA: 0x00404757 File Offset: 0x00402B57
		public bool LoadItem(string _asset, string _file, string _parent)
		{
			return true;
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x0040475C File Offset: 0x00402B5C
		public void ReleaseItem(string _name)
		{
			int num = this.listItem.FindIndex((GameObject o) => o.name == _name);
			if (num < 0)
			{
				return;
			}
			GameObject obj = this.listItem[num];
			UnityEngine.Object.Destroy(obj);
			this.listItem.RemoveAt(num);
		}

		// Token: 0x06009D5B RID: 40283 RVA: 0x004047B8 File Offset: 0x00402BB8
		public void ReleaseAllItem()
		{
			if (this.listItem == null)
			{
				return;
			}
			for (int i = 0; i < this.listItem.Count; i++)
			{
				if (this.listItem[i] != null)
				{
					UnityEngine.Object.Destroy(this.listItem[i]);
				}
			}
			this.listItem.Clear();
		}

		// Token: 0x06009D5C RID: 40284 RVA: 0x00404820 File Offset: 0x00402C20
		public void SetVisible(bool _visible)
		{
			for (int i = 0; i < this.listItem.Count; i++)
			{
				if (!(this.listItem[i] == null))
				{
					if (this.listItem[i].activeSelf != _visible)
					{
						this.listItem[i].SetActive(_visible);
					}
				}
			}
		}

		// Token: 0x06009D5D RID: 40285 RVA: 0x0040488E File Offset: 0x00402C8E
		private void Awake()
		{
			this.CharFemale = base.GetComponent<ChaControl>();
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x0040489C File Offset: 0x00402C9C
		private void OnDestroy()
		{
			this.ReleaseAllItem();
		}

		// Token: 0x04007D38 RID: 32056
		public List<GameObject> listItem = new List<GameObject>();
	}
}
