using System;
using System.Collections.Generic;
using Studio.Anime;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.Events;

namespace Studio.Item
{
	// Token: 0x020012DD RID: 4829
	public class ListNodePool : ObjectPool<ListNode>
	{
		// Token: 0x0600A10B RID: 41227 RVA: 0x0042256B File Offset: 0x0042096B
		public ListNodePool(Transform _parent, ListNode _prefab)
		{
			this.parent = _parent;
			this.prefab = _prefab;
			this.nodes = new List<ListNode>();
		}

		// Token: 0x0600A10C RID: 41228 RVA: 0x0042258C File Offset: 0x0042098C
		protected override ListNode CreateInstance()
		{
			return UnityEngine.Object.Instantiate<ListNode>(this.prefab, this.parent);
		}

		// Token: 0x0600A10D RID: 41229 RVA: 0x004225AC File Offset: 0x004209AC
		public ListNode Rent(string _text, UnityAction _action, bool _textSlide = true)
		{
			ListNode listNode = base.Rent();
			listNode.transform.SetAsLastSibling();
			listNode.Select = false;
			this.nodes.Add(listNode);
			listNode.UseSlide = _textSlide;
			listNode.Text = _text;
			listNode.SetButtonAction(_action);
			return listNode;
		}

		// Token: 0x0600A10E RID: 41230 RVA: 0x004225F4 File Offset: 0x004209F4
		public void Return()
		{
			foreach (ListNode instance in this.nodes)
			{
				base.Return(instance);
			}
			this.nodes.Clear();
		}

		// Token: 0x04007F3F RID: 32575
		private readonly Transform parent;

		// Token: 0x04007F40 RID: 32576
		private readonly ListNode prefab;

		// Token: 0x04007F41 RID: 32577
		private List<ListNode> nodes;
	}
}
