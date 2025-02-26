using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012D8 RID: 4824
	public class FrameList : MonoBehaviour
	{
		// Token: 0x0600A0EB RID: 41195 RVA: 0x00421AA0 File Offset: 0x0041FEA0
		public void UpdateUI()
		{
			this.SetSelect(this.select, false);
			this.select = this.listPath.FindIndex((string v) => v == Singleton<Studio>.Instance.sceneInfo.frame);
			this.SetSelect(this.select, true);
		}

		// Token: 0x0600A0EC RID: 41196 RVA: 0x00421AF8 File Offset: 0x0041FEF8
		private void OnClickSelect(int _idx)
		{
			this.SetSelect(this.select, false);
			this.select = _idx;
			this.SetSelect(this.select, true);
			this.frameCtrl.Load((this.select == -1) ? string.Empty : this.listPath[_idx]);
		}

		// Token: 0x0600A0ED RID: 41197 RVA: 0x00421B54 File Offset: 0x0041FF54
		private void SetSelect(int _idx, bool _flag)
		{
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(_idx, out studioNode))
			{
				studioNode.select = _flag;
			}
		}

		// Token: 0x0600A0EE RID: 41198 RVA: 0x00421B80 File Offset: 0x0041FF80
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			IEnumerable<string> files = Directory.GetFiles(UserData.Create("frame"), "*.png");
			if (FrameList.<>f__mg$cache0 == null)
			{
				FrameList.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
			}
			this.listPath = files.Select(FrameList.<>f__mg$cache0).ToList<string>();
			this.CreateNode(-1, "なし");
			int count = this.listPath.Count;
			for (int j = 0; j < count; j++)
			{
				this.CreateNode(j, Path.GetFileNameWithoutExtension(this.listPath[j]));
			}
			this.select = this.listPath.FindIndex((string v) => v == Singleton<Studio>.Instance.sceneInfo.frame);
			this.SetSelect(this.select, true);
		}

		// Token: 0x0600A0EF RID: 41199 RVA: 0x00421C88 File Offset: 0x00420088
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

		// Token: 0x0600A0F0 RID: 41200 RVA: 0x00421D00 File Offset: 0x00420100
		private void Start()
		{
			this.InitList();
		}

		// Token: 0x04007F1A RID: 32538
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x04007F1B RID: 32539
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007F1C RID: 32540
		[SerializeField]
		private FrameCtrl frameCtrl;

		// Token: 0x04007F1D RID: 32541
		private List<string> listPath = new List<string>();

		// Token: 0x04007F1E RID: 32542
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();

		// Token: 0x04007F1F RID: 32543
		private int select = -1;

		// Token: 0x04007F21 RID: 32545
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;
	}
}
