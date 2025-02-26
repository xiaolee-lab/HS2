using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001268 RID: 4712
	public class LightList : MonoBehaviour
	{
		// Token: 0x06009BF4 RID: 39924 RVA: 0x003FC4AC File Offset: 0x003FA8AC
		public void OnClick(int _no)
		{
			Singleton<Studio>.Instance.AddLight(_no);
		}

		// Token: 0x06009BF5 RID: 39925 RVA: 0x003FC4BC File Offset: 0x003FA8BC
		private void AddNode(int _key, string _name)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
			gameObject.transform.SetParent(this.transformRoot, false);
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
			ListNode component = gameObject.GetComponent<ListNode>();
			int key = _key;
			component.AddActionToButton(delegate
			{
				this.OnClick(key);
			});
			component.text = _name;
		}

		// Token: 0x06009BF6 RID: 39926 RVA: 0x003FC530 File Offset: 0x003FA930
		private void Awake()
		{
			foreach (KeyValuePair<int, Info.LightLoadInfo> keyValuePair in Singleton<Info>.Instance.dicLightLoadInfo)
			{
				this.AddNode(keyValuePair.Key, keyValuePair.Value.name);
			}
		}

		// Token: 0x04007C3A RID: 31802
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007C3B RID: 31803
		[SerializeField]
		private Transform transformRoot;
	}
}
