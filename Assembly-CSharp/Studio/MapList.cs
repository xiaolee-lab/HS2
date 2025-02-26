using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200128A RID: 4746
	public class MapList : MonoBehaviour
	{
		// Token: 0x06009D1E RID: 40222 RVA: 0x004039A4 File Offset: 0x00401DA4
		public void UpdateInfo()
		{
			if (!this.isInit)
			{
				return;
			}
			foreach (KeyValuePair<int, ListNode> keyValuePair in this.dicNode)
			{
				keyValuePair.Value.select = false;
			}
			ListNode listNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.sceneInfo.mapInfo.no, out listNode))
			{
				listNode.select = true;
				this.select = Singleton<Studio>.Instance.sceneInfo.mapInfo.no;
			}
			else if (this.dicNode.TryGetValue(-1, out listNode))
			{
				listNode.select = true;
				this.select = -1;
			}
		}

		// Token: 0x06009D1F RID: 40223 RVA: 0x00403A84 File Offset: 0x00401E84
		public void OnClick(int _no)
		{
			Singleton<Studio>.Instance.AddMap(_no, Studio.optionSystem.autoHide, false, false);
			ListNode listNode = null;
			if (this.dicNode.TryGetValue(this.select, out listNode))
			{
				listNode.select = false;
			}
			if (this.dicNode.TryGetValue(_no, out listNode))
			{
				listNode.select = true;
			}
			this.select = _no;
		}

		// Token: 0x06009D20 RID: 40224 RVA: 0x00403AEC File Offset: 0x00401EEC
		public void Init()
		{
			this.AddNode(-1, "なし");
			foreach (KeyValuePair<int, Info.MapLoadInfo> keyValuePair in Singleton<Info>.Instance.dicMapLoadInfo)
			{
				this.AddNode(keyValuePair.Key, keyValuePair.Value.name);
			}
			ListNode listNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.sceneInfo.mapInfo.no, out listNode))
			{
				listNode.select = true;
			}
			this.isInit = true;
		}

		// Token: 0x06009D21 RID: 40225 RVA: 0x00403BA0 File Offset: 0x00401FA0
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
			this.dicNode.Add(key, component);
		}

		// Token: 0x04007CF5 RID: 31989
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007CF6 RID: 31990
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007CF7 RID: 31991
		private int select = -1;

		// Token: 0x04007CF8 RID: 31992
		private Dictionary<int, ListNode> dicNode = new Dictionary<int, ListNode>();

		// Token: 0x04007CF9 RID: 31993
		private bool isInit;
	}
}
