using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012EA RID: 4842
	public class VoiceCategoryList : MonoBehaviour
	{
		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x0600A19E RID: 41374 RVA: 0x00425370 File Offset: 0x00423770
		// (set) Token: 0x0600A19F RID: 41375 RVA: 0x0042537D File Offset: 0x0042377D
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

		// Token: 0x0600A1A0 RID: 41376 RVA: 0x0042539C File Offset: 0x0042379C
		public void InitList(int _group)
		{
			if (!Utility.SetStruct<int>(ref this.group, _group))
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
			this.dicNode = new Dictionary<int, StudioNode>();
			foreach (KeyValuePair<int, Info.CategoryInfo> keyValuePair in from v in Singleton<Info>.Instance.dicVoiceGroupCategory[_group].dicCategory
			orderby v.Value.sort
			select v)
			{
				if (this.CheckCategory(_group, keyValuePair.Key))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectPrefab);
					if (!gameObject.activeSelf)
					{
						gameObject.SetActive(true);
					}
					gameObject.transform.SetParent(this.transformRoot, false);
					StudioNode component = gameObject.GetComponent<StudioNode>();
					int no = keyValuePair.Key;
					component.addOnClick = delegate()
					{
						this.OnSelect(no);
					};
					component.text = keyValuePair.Value.name;
					this.dicNode.Add(keyValuePair.Key, component);
				}
			}
			this.select = -1;
			this.active = true;
			this.voiceList.active = false;
		}

		// Token: 0x0600A1A1 RID: 41377 RVA: 0x00425554 File Offset: 0x00423954
		private bool CheckCategory(int _group, int _category)
		{
			Dictionary<int, Dictionary<int, Info.LoadCommonInfo>> dictionary = null;
			return Singleton<Info>.Instance.dicVoiceLoadInfo.TryGetValue(_group, out dictionary) && dictionary.ContainsKey(_category);
		}

		// Token: 0x0600A1A2 RID: 41378 RVA: 0x00425584 File Offset: 0x00423984
		private void OnSelect(int _no)
		{
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
				this.voiceList.InitList(this.group, _no);
				StudioNode studioNode = null;
				if (this.dicNode.TryGetValue(key, out studioNode))
				{
					studioNode.select = false;
				}
				if (this.dicNode.TryGetValue(this.select, out studioNode))
				{
					studioNode.select = true;
				}
			}
		}

		// Token: 0x04007FBE RID: 32702
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007FBF RID: 32703
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007FC0 RID: 32704
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007FC1 RID: 32705
		[SerializeField]
		private VoiceList voiceList;

		// Token: 0x04007FC2 RID: 32706
		private int group = -1;

		// Token: 0x04007FC3 RID: 32707
		private int select = -1;

		// Token: 0x04007FC4 RID: 32708
		private Dictionary<int, StudioNode> dicNode;
	}
}
