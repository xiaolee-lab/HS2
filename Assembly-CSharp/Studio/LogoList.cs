using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012DF RID: 4831
	public class LogoList : MonoBehaviour
	{
		// Token: 0x0600A120 RID: 41248 RVA: 0x0042298C File Offset: 0x00420D8C
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
			int logo = Studio.optionSystem.logo;
			ListNode listNode = null;
			if (this.dicNode.TryGetValue(logo, out listNode))
			{
				listNode.select = true;
				this.select = logo;
			}
			else if (this.dicNode.TryGetValue(-1, out listNode))
			{
				listNode.select = true;
				this.select = -1;
			}
		}

		// Token: 0x0600A121 RID: 41249 RVA: 0x00422A50 File Offset: 0x00420E50
		public void OnClick(int _no)
		{
			Studio.optionSystem.logo = _no;
			this.UpdateLogo();
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

		// Token: 0x0600A122 RID: 41250 RVA: 0x00422AB0 File Offset: 0x00420EB0
		public void Init()
		{
			for (int i = 0; i < this.strName.Length; i++)
			{
				this.AddNode(i, this.strName[i]);
			}
			ListNode listNode = null;
			if (this.dicNode.TryGetValue(Studio.optionSystem.logo, out listNode))
			{
				listNode.select = true;
			}
			this.UpdateLogo();
			this.select = Studio.optionSystem.logo;
			this.isInit = true;
		}

		// Token: 0x0600A123 RID: 41251 RVA: 0x00422B28 File Offset: 0x00420F28
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

		// Token: 0x0600A124 RID: 41252 RVA: 0x00422BAC File Offset: 0x00420FAC
		private void UpdateLogo()
		{
			Sprite sprite = this.spriteLogo.SafeGet(Studio.optionSystem.logo);
			this.imageLogo.sprite = sprite;
			this.imageLogo.color = ((!(sprite == null)) ? Color.white : Color.clear);
		}

		// Token: 0x04007F46 RID: 32582
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007F47 RID: 32583
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F48 RID: 32584
		[SerializeField]
		private Image imageLogo;

		// Token: 0x04007F49 RID: 32585
		[SerializeField]
		private Sprite[] spriteLogo;

		// Token: 0x04007F4A RID: 32586
		[SerializeField]
		private string[] strName;

		// Token: 0x04007F4B RID: 32587
		private int select = -1;

		// Token: 0x04007F4C RID: 32588
		private Dictionary<int, ListNode> dicNode = new Dictionary<int, ListNode>();

		// Token: 0x04007F4D RID: 32589
		private bool isInit;
	}
}
