using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012ED RID: 4845
	public class VoiceList : MonoBehaviour
	{
		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x0600A1C1 RID: 41409 RVA: 0x00425FD1 File Offset: 0x004243D1
		// (set) Token: 0x0600A1C2 RID: 41410 RVA: 0x00425FDE File Offset: 0x004243DE
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
				if (!base.gameObject.activeSelf)
				{
					this.group = -1;
					this.category = -1;
				}
			}
		}

		// Token: 0x0600A1C3 RID: 41411 RVA: 0x0042601C File Offset: 0x0042441C
		public void InitList(int _group, int _category)
		{
			if (!Utility.SetStruct<int>(ref this.group, _group) && !Utility.SetStruct<int>(ref this.category, _category))
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
			foreach (KeyValuePair<int, Info.LoadCommonInfo> keyValuePair in Singleton<Info>.Instance.dicVoiceLoadInfo[_group][_category])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectPrefab);
				if (!gameObject.activeSelf)
				{
					gameObject.SetActive(true);
				}
				gameObject.transform.SetParent(this.transformRoot, false);
				VoiceNode component = gameObject.GetComponent<VoiceNode>();
				int no = keyValuePair.Key;
				component.addOnClick = delegate()
				{
					this.OnSelect(no);
				};
				component.text = keyValuePair.Value.name;
			}
			this.active = true;
			this.group = _group;
			this.category = _category;
		}

		// Token: 0x0600A1C4 RID: 41412 RVA: 0x0042618C File Offset: 0x0042458C
		private void OnSelect(int _no)
		{
			OCIChar[] array = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
			select Studio.GetCtrlInfo(v) as OCIChar into v
			where v != null
			select v).ToArray<OCIChar>();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i].AddVoice(this.group, this.category, _no);
			}
			this.voiceControl.InitList();
		}

		// Token: 0x04007FE3 RID: 32739
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007FE4 RID: 32740
		[SerializeField]
		private GameObject objectPrefab;

		// Token: 0x04007FE5 RID: 32741
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x04007FE6 RID: 32742
		[SerializeField]
		private VoiceControl voiceControl;

		// Token: 0x04007FE7 RID: 32743
		private int group = -1;

		// Token: 0x04007FE8 RID: 32744
		private int category = -1;
	}
}
