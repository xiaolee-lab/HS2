using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012C8 RID: 4808
	public class BackgroundList : MonoBehaviour
	{
		// Token: 0x0600A065 RID: 41061 RVA: 0x0041F04C File Offset: 0x0041D44C
		public void UpdateUI()
		{
			this.SetSelect(this.select, false);
			this.select = this.listPath.FindIndex((string v) => v == Singleton<Studio>.Instance.sceneInfo.background);
			this.SetSelect(this.select, true);
		}

		// Token: 0x0600A066 RID: 41062 RVA: 0x0041F0A4 File Offset: 0x0041D4A4
		private void OnClickSelect(int _idx)
		{
			this.SetSelect(this.select, false);
			this.select = _idx;
			this.SetSelect(this.select, true);
			this.backgroundCtrl.Load((this.select == -1) ? string.Empty : this.listPath[_idx]);
		}

		// Token: 0x0600A067 RID: 41063 RVA: 0x0041F100 File Offset: 0x0041D500
		private void SetSelect(int _idx, bool _flag)
		{
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(_idx, out studioNode))
			{
				studioNode.select = _flag;
			}
		}

		// Token: 0x0600A068 RID: 41064 RVA: 0x0041F12C File Offset: 0x0041D52C
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			IEnumerable<string> files = Directory.GetFiles(UserData.Create(BackgroundList.dirName), "*.png");
			if (BackgroundList.<>f__mg$cache0 == null)
			{
				BackgroundList.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
			}
			this.listPath = files.Select(BackgroundList.<>f__mg$cache0).ToList<string>();
			this.CreateNode(-1, "なし");
			int count = this.listPath.Count;
			for (int j = 0; j < count; j++)
			{
				this.CreateNode(j, Path.GetFileNameWithoutExtension(this.listPath[j]));
			}
			this.select = this.listPath.FindIndex((string v) => v == Singleton<Studio>.Instance.sceneInfo.background);
			this.SetSelect(this.select, true);
		}

		// Token: 0x0600A069 RID: 41065 RVA: 0x0041F234 File Offset: 0x0041D634
		private void CreateNode(int _idx, string _text)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
			gameObject.transform.SetParent(this.transformRoot, false);
			StudioNode component = gameObject.GetComponent<StudioNode>();
			component.active = true;
			component.addOnClick = delegate()
			{
				this.OnClickSelect(_idx);
			};
			component.text = _text;
			this.dicNode.Add(_idx, component);
		}

		// Token: 0x0600A06A RID: 41066 RVA: 0x0041F2AC File Offset: 0x0041D6AC
		private void Start()
		{
			this.InitList();
		}

		// Token: 0x04007EBB RID: 32443
		public static string dirName = "bg";

		// Token: 0x04007EBC RID: 32444
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007EBD RID: 32445
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007EBE RID: 32446
		[SerializeField]
		private BackgroundCtrl backgroundCtrl;

		// Token: 0x04007EBF RID: 32447
		private List<string> listPath = new List<string>();

		// Token: 0x04007EC0 RID: 32448
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();

		// Token: 0x04007EC1 RID: 32449
		private int select = -1;

		// Token: 0x04007EC3 RID: 32451
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;
	}
}
