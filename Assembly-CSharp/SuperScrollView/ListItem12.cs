using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005B7 RID: 1463
	public class ListItem12 : MonoBehaviour
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000BA2A9 File Offset: 0x000B86A9
		public int TreeItemIndex
		{
			get
			{
				return this.mTreeItemIndex;
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000BA2B1 File Offset: 0x000B86B1
		public void Init()
		{
			this.mButton.onClick.AddListener(new UnityAction(this.OnButtonClicked));
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000BA2CF File Offset: 0x000B86CF
		public void SetClickCallBack(Action<int> clickHandler)
		{
			this.mClickHandler = clickHandler;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000BA2D8 File Offset: 0x000B86D8
		private void OnButtonClicked()
		{
			if (this.mClickHandler != null)
			{
				this.mClickHandler(this.mTreeItemIndex);
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000BA2F8 File Offset: 0x000B86F8
		public void SetExpand(bool expand)
		{
			if (expand)
			{
				this.mArrow.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			}
			else
			{
				this.mArrow.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000BA358 File Offset: 0x000B8758
		public void SetItemData(int treeItemIndex, bool expand)
		{
			this.mTreeItemIndex = treeItemIndex;
			this.SetExpand(expand);
		}

		// Token: 0x0400215F RID: 8543
		public Text mText;

		// Token: 0x04002160 RID: 8544
		public GameObject mArrow;

		// Token: 0x04002161 RID: 8545
		public Button mButton;

		// Token: 0x04002162 RID: 8546
		private int mTreeItemIndex = -1;

		// Token: 0x04002163 RID: 8547
		private Action<int> mClickHandler;
	}
}
