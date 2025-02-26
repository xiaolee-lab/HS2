using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012EC RID: 4844
	public class VoiceGroupList : MonoBehaviour
	{
		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x0600A1BB RID: 41403 RVA: 0x00425DB7 File Offset: 0x004241B7
		// (set) Token: 0x0600A1BC RID: 41404 RVA: 0x00425DC4 File Offset: 0x004241C4
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

		// Token: 0x0600A1BD RID: 41405 RVA: 0x00425DE4 File Offset: 0x004241E4
		private void InitList()
		{
			int childCount = this.transformRoot.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			foreach (KeyValuePair<int, Info.GroupInfo> keyValuePair in Singleton<Info>.Instance.dicVoiceGroupCategory)
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
			this.select = -1;
			this.active = true;
		}

		// Token: 0x0600A1BE RID: 41406 RVA: 0x00425F20 File Offset: 0x00424320
		private void OnSelect(int _no)
		{
			int key = this.select;
			if (Utility.SetStruct<int>(ref this.select, _no))
			{
				this.voiceCategoryList.InitList(_no);
				this.voiceList.active = false;
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

		// Token: 0x0600A1BF RID: 41407 RVA: 0x00425F98 File Offset: 0x00424398
		private void Start()
		{
			this.InitList();
		}

		// Token: 0x04007FDD RID: 32733
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007FDE RID: 32734
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007FDF RID: 32735
		[SerializeField]
		private VoiceCategoryList voiceCategoryList;

		// Token: 0x04007FE0 RID: 32736
		[SerializeField]
		private VoiceList voiceList;

		// Token: 0x04007FE1 RID: 32737
		private int select = -1;

		// Token: 0x04007FE2 RID: 32738
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();
	}
}
