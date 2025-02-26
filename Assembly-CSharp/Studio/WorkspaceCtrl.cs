using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200135F RID: 4959
	public class WorkspaceCtrl : MonoBehaviour
	{
		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x0600A633 RID: 42547 RVA: 0x004399C8 File Offset: 0x00437DC8
		// (set) Token: 0x0600A634 RID: 42548 RVA: 0x004399D0 File Offset: 0x00437DD0
		private Button[] buttons { get; set; }

		// Token: 0x0600A635 RID: 42549 RVA: 0x004399D9 File Offset: 0x00437DD9
		private void OnClickClose()
		{
			this.studioScene.OnClickDraw(1);
		}

		// Token: 0x0600A636 RID: 42550 RVA: 0x004399E8 File Offset: 0x00437DE8
		private void OnClickRemove()
		{
			this.treeNodeCtrl.RemoveNode();
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
		}

		// Token: 0x0600A637 RID: 42551 RVA: 0x00439A28 File Offset: 0x00437E28
		private void OnClickParent()
		{
			this.treeNodeCtrl.SetParent();
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
		}

		// Token: 0x0600A638 RID: 42552 RVA: 0x00439A68 File Offset: 0x00437E68
		public void OnClickDelete()
		{
			this.treeNodeCtrl.DeleteNode();
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
			Singleton<UndoRedoManager>.Instance.Clear();
		}

		// Token: 0x0600A639 RID: 42553 RVA: 0x00439AB4 File Offset: 0x00437EB4
		private void OnClickCopy()
		{
			this.treeNodeCtrl.CopyChangeAmount();
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
		}

		// Token: 0x0600A63A RID: 42554 RVA: 0x00439AF3 File Offset: 0x00437EF3
		private void OnClickDuplicate()
		{
			Singleton<Studio>.Instance.Duplicate();
		}

		// Token: 0x0600A63B RID: 42555 RVA: 0x00439AFF File Offset: 0x00437EFF
		private void OnClickFolder()
		{
			Singleton<Studio>.Instance.AddFolder();
		}

		// Token: 0x0600A63C RID: 42556 RVA: 0x00439B0B File Offset: 0x00437F0B
		private void OnClickCamera()
		{
			Singleton<Studio>.Instance.AddCamera();
		}

		// Token: 0x0600A63D RID: 42557 RVA: 0x00439B17 File Offset: 0x00437F17
		private void OnClickRoute()
		{
			Singleton<Studio>.Instance.AddRoute();
		}

		// Token: 0x0600A63E RID: 42558 RVA: 0x00439B24 File Offset: 0x00437F24
		public void UpdateUI()
		{
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
		}

		// Token: 0x0600A63F RID: 42559 RVA: 0x00439B58 File Offset: 0x00437F58
		public void OnParentage(TreeNodeObject _parent, TreeNodeObject _child)
		{
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
		}

		// Token: 0x0600A640 RID: 42560 RVA: 0x00439B8C File Offset: 0x00437F8C
		public void OnDeleteNode(TreeNodeObject _node)
		{
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i].interactable = false;
			}
		}

		// Token: 0x0600A641 RID: 42561 RVA: 0x00439BC0 File Offset: 0x00437FC0
		public void OnSelectSingle(TreeNodeObject _node)
		{
			this.buttonParent.interactable = false;
			this.buttonRemove.interactable = _node.isParent;
			this.buttonDelete.interactable = _node.enableDelete;
			this.buttonCopy.interactable = false;
			this.buttonDuplicate.interactable = _node.enableCopy;
		}

		// Token: 0x0600A642 RID: 42562 RVA: 0x00439C18 File Offset: 0x00438018
		public void OnSelectMultiple()
		{
			TreeNodeObject[] selectNodes = this.treeNodeCtrl.selectNodes;
			if (selectNodes.IsNullOrEmpty<TreeNodeObject>())
			{
				return;
			}
			this.buttonParent.interactable = selectNodes.Any((TreeNodeObject v) => v.enableChangeParent);
			this.buttonRemove.interactable = selectNodes.Any((TreeNodeObject v) => v.isParent);
			this.buttonDelete.interactable = selectNodes.Any((TreeNodeObject v) => v.enableDelete);
			Selectable selectable = this.buttonCopy;
			bool interactable;
			if (selectNodes[0].enableCopy)
			{
				interactable = (selectNodes.Count((TreeNodeObject v) => v.enableCopy) > 1);
			}
			else
			{
				interactable = false;
			}
			selectable.interactable = interactable;
			this.buttonDuplicate.interactable = selectNodes.Any((TreeNodeObject v) => v.enableCopy);
		}

		// Token: 0x0600A643 RID: 42563 RVA: 0x00439D38 File Offset: 0x00438138
		public void OnDeselectSingle(TreeNodeObject _node)
		{
			TreeNodeObject[] selectNodes = this.treeNodeCtrl.selectNodes;
			if (selectNodes.IsNullOrEmpty<TreeNodeObject>())
			{
				for (int i = 0; i < this.buttons.Length; i++)
				{
					this.buttons[i].interactable = false;
				}
			}
			else
			{
				Selectable selectable = this.buttonParent;
				bool interactable;
				if (selectNodes.Length > 1)
				{
					interactable = selectNodes.Any((TreeNodeObject v) => v.enableChangeParent);
				}
				else
				{
					interactable = false;
				}
				selectable.interactable = interactable;
				this.buttonRemove.interactable = selectNodes.Any((TreeNodeObject v) => v.isParent);
				this.buttonDelete.interactable = selectNodes.Any((TreeNodeObject v) => v.enableDelete);
				Selectable selectable2 = this.buttonCopy;
				bool interactable2;
				if (selectNodes.Length > 1)
				{
					if (selectNodes[0].enableCopy)
					{
						interactable2 = (selectNodes.Count((TreeNodeObject v) => v.enableCopy) > 1);
					}
					else
					{
						interactable2 = false;
					}
				}
				else
				{
					interactable2 = false;
				}
				selectable2.interactable = interactable2;
				this.buttonDuplicate.interactable = selectNodes.Any((TreeNodeObject v) => v.enableCopy);
			}
		}

		// Token: 0x0600A644 RID: 42564 RVA: 0x00439EA0 File Offset: 0x004382A0
		private void Awake()
		{
			this.buttonClose.onClick.AddListener(new UnityAction(this.OnClickClose));
			this.buttonRemove.onClick.AddListener(new UnityAction(this.OnClickRemove));
			this.buttonParent.onClick.AddListener(new UnityAction(this.OnClickParent));
			this.buttonDelete.onClick.AddListener(new UnityAction(this.OnClickDelete));
			this.buttonCopy.onClick.AddListener(new UnityAction(this.OnClickCopy));
			this.buttonDuplicate.onClick.AddListener(new UnityAction(this.OnClickDuplicate));
			this.buttonFolder.onClick.AddListener(new UnityAction(this.OnClickFolder));
			this.buttonCamera.onClick.AddListener(new UnityAction(this.OnClickCamera));
			this.buttonRoute.onClick.AddListener(new UnityAction(this.OnClickRoute));
			TreeNodeCtrl treeNodeCtrl = this.treeNodeCtrl;
			treeNodeCtrl.onParentage = (Action<TreeNodeObject, TreeNodeObject>)Delegate.Combine(treeNodeCtrl.onParentage, new Action<TreeNodeObject, TreeNodeObject>(this.OnParentage));
			TreeNodeCtrl treeNodeCtrl2 = this.treeNodeCtrl;
			treeNodeCtrl2.onDelete = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl2.onDelete, new Action<TreeNodeObject>(this.OnDeleteNode));
			TreeNodeCtrl treeNodeCtrl3 = this.treeNodeCtrl;
			treeNodeCtrl3.onSelect = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl3.onSelect, new Action<TreeNodeObject>(this.OnSelectSingle));
			TreeNodeCtrl treeNodeCtrl4 = this.treeNodeCtrl;
			treeNodeCtrl4.onSelectMultiple = (Action)Delegate.Combine(treeNodeCtrl4.onSelectMultiple, new Action(this.OnSelectMultiple));
			TreeNodeCtrl treeNodeCtrl5 = this.treeNodeCtrl;
			treeNodeCtrl5.onDeselect = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl5.onDeselect, new Action<TreeNodeObject>(this.OnDeselectSingle));
			this.buttons = new Button[]
			{
				this.buttonRemove,
				this.buttonParent,
				this.buttonDelete,
				this.buttonCopy,
				this.buttonDuplicate
			};
		}

		// Token: 0x0400828F RID: 33423
		[SerializeField]
		private Button buttonClose;

		// Token: 0x04008290 RID: 33424
		[SerializeField]
		private Button buttonRemove;

		// Token: 0x04008291 RID: 33425
		[SerializeField]
		private Button buttonParent;

		// Token: 0x04008292 RID: 33426
		[SerializeField]
		private TreeNodeCtrl treeNodeCtrl;

		// Token: 0x04008293 RID: 33427
		[SerializeField]
		private Button buttonDelete;

		// Token: 0x04008294 RID: 33428
		[SerializeField]
		private Button buttonCopy;

		// Token: 0x04008295 RID: 33429
		[SerializeField]
		private Button buttonDuplicate;

		// Token: 0x04008296 RID: 33430
		[SerializeField]
		private Button buttonFolder;

		// Token: 0x04008297 RID: 33431
		[SerializeField]
		private Button buttonCamera;

		// Token: 0x04008298 RID: 33432
		[SerializeField]
		private Button buttonRoute;

		// Token: 0x04008299 RID: 33433
		[SerializeField]
		private StudioScene studioScene;
	}
}
