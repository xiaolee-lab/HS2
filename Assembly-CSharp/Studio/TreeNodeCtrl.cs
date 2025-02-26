using System;
using System.Collections.Generic;
using System.Linq;
using GUITree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012AC RID: 4780
	public class TreeNodeCtrl : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x06009E0D RID: 40461 RVA: 0x00407580 File Offset: 0x00405980
		// (set) Token: 0x06009E0E RID: 40462 RVA: 0x004075A5 File Offset: 0x004059A5
		public TreeNodeObject selectNode
		{
			get
			{
				TreeNodeObject[] selectNodes = this.selectNodes;
				return (selectNodes.Length == 0) ? null : selectNodes[0];
			}
			set
			{
				this.SetSelectNode(value);
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x06009E0F RID: 40463 RVA: 0x004075AE File Offset: 0x004059AE
		public TreeNodeObject[] selectNodes
		{
			get
			{
				return this.hashSelectNode.ToArray<TreeNodeObject>();
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x06009E10 RID: 40464 RVA: 0x004075BC File Offset: 0x004059BC
		public ObjectCtrlInfo[] selectObjectCtrl
		{
			get
			{
				List<ObjectCtrlInfo> list = new List<ObjectCtrlInfo>();
				foreach (TreeNodeObject key in this.hashSelectNode)
				{
					ObjectCtrlInfo item = null;
					if (Singleton<Studio>.Instance.dicInfo.TryGetValue(key, out item))
					{
						list.Add(item);
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x06009E11 RID: 40465 RVA: 0x00407640 File Offset: 0x00405A40
		public TreeNodeObject AddNode(string _name, TreeNodeObject _parent = null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_ObjectNode);
			gameObject.SetActive(true);
			gameObject.transform.SetParent(this.m_ObjectRoot.transform, false);
			TreeNodeObject component = gameObject.GetComponent<TreeNodeObject>();
			component.textName = _name;
			if (_parent)
			{
				component.SetParent(_parent);
			}
			else
			{
				this.m_TreeNodeObject.Add(component);
			}
			return component;
		}

		// Token: 0x06009E12 RID: 40466 RVA: 0x004076AC File Offset: 0x00405AAC
		public bool AddNode(TreeNodeObject _node)
		{
			if (_node == null || _node.parent != null)
			{
				return false;
			}
			if (this.m_TreeNodeObject.Contains(_node))
			{
				return false;
			}
			this.m_TreeNodeObject.Add(_node);
			return true;
		}

		// Token: 0x06009E13 RID: 40467 RVA: 0x004076F8 File Offset: 0x00405AF8
		public void RemoveNode(TreeNodeObject _node)
		{
			if (_node == null || _node.parent == null)
			{
				return;
			}
			this.m_TreeNodeObject.Remove(_node);
		}

		// Token: 0x06009E14 RID: 40468 RVA: 0x00407725 File Offset: 0x00405B25
		public bool CheckNode(TreeNodeObject _node)
		{
			return !(_node == null) && this.m_TreeNodeObject.Contains(_node);
		}

		// Token: 0x06009E15 RID: 40469 RVA: 0x00407744 File Offset: 0x00405B44
		public void DeleteNode(TreeNodeObject _node)
		{
			if (!_node.enableDelete)
			{
				return;
			}
			_node.SetParent(null);
			this.m_TreeNodeObject.Remove(_node);
			if (_node.onDelete != null)
			{
				_node.onDelete();
			}
			this.DeleteNodeLoop(_node);
			if (this.m_TreeNodeObject.Count == 0)
			{
				this.scrollRect.verticalNormalizedPosition = 1f;
			}
		}

		// Token: 0x06009E16 RID: 40470 RVA: 0x004077B0 File Offset: 0x00405BB0
		public void DeleteAllNode()
		{
			int count = this.m_TreeNodeObject.Count;
			for (int i = 0; i < count; i++)
			{
				this.DeleteAllNodeLoop(this.m_TreeNodeObject[i]);
			}
			this.m_TreeNodeObject.Clear();
			this.hashSelectNode.Clear();
			this.scrollRect.verticalNormalizedPosition = 1f;
			this.scrollRect.horizontalNormalizedPosition = 0f;
		}

		// Token: 0x06009E17 RID: 40471 RVA: 0x00407824 File Offset: 0x00405C24
		public TreeNodeObject GetNode(int _index)
		{
			int count = this.m_TreeNodeObject.Count;
			if (count == 0)
			{
				return null;
			}
			return (_index < 0 || _index >= count) ? null : this.m_TreeNodeObject[_index];
		}

		// Token: 0x06009E18 RID: 40472 RVA: 0x00407868 File Offset: 0x00405C68
		public void SetParent(TreeNodeObject _node, TreeNodeObject _parent)
		{
			if (_node == null)
			{
				return;
			}
			if (!_node.enableChangeParent)
			{
				return;
			}
			if (this.CheckNode(_node) && _parent == null)
			{
				return;
			}
			if (!_node.SetParent(_parent))
			{
				return;
			}
			this.RefreshHierachy();
			this.m_TreeRoot.Invoke("SetDirty", 0f);
			if (this.onParentage != null)
			{
				this.onParentage(_parent, _node);
			}
		}

		// Token: 0x06009E19 RID: 40473 RVA: 0x004078E8 File Offset: 0x00405CE8
		public void RefreshHierachy()
		{
			int count = this.m_TreeNodeObject.Count;
			for (int i = 0; i < count; i++)
			{
				this.RefreshHierachyLoop(this.m_TreeNodeObject[i], 0, true);
				this.RefreshVisibleLoop(this.m_TreeNodeObject[i]);
			}
		}

		// Token: 0x06009E1A RID: 40474 RVA: 0x0040793C File Offset: 0x00405D3C
		public void SetParent()
		{
			TreeNodeObject[] selectNodes = this.selectNodes;
			for (int i = 1; i < selectNodes.Length; i++)
			{
				this.SetParent(selectNodes[i], selectNodes[0]);
			}
			this.SelectSingle(null, true);
			Singleton<GuideObjectManager>.Instance.selectObject = null;
		}

		// Token: 0x06009E1B RID: 40475 RVA: 0x00407984 File Offset: 0x00405D84
		public void RemoveNode()
		{
			TreeNodeObject[] selectNodes = this.selectNodes;
			for (int i = 0; i < selectNodes.Length; i++)
			{
				this.SetParent(selectNodes[i], null);
			}
			this.SelectSingle(null, true);
		}

		// Token: 0x06009E1C RID: 40476 RVA: 0x004079C0 File Offset: 0x00405DC0
		public void DeleteNode()
		{
			TreeNodeObject[] selectNodes = this.selectNodes;
			for (int i = 0; i < selectNodes.Length; i++)
			{
				this.DeleteNode(selectNodes[i]);
			}
			this.SelectSingle(null, true);
		}

		// Token: 0x06009E1D RID: 40477 RVA: 0x004079FC File Offset: 0x00405DFC
		public void CopyChangeAmount()
		{
			TreeNodeObject[] selectNodes = this.selectNodes;
			ObjectCtrlInfo objectCtrlInfo = null;
			if (!Singleton<Studio>.Instance.dicInfo.TryGetValue(selectNodes[0], out objectCtrlInfo))
			{
				return;
			}
			List<TreeNodeCommand.MoveCopyInfo> list = new List<TreeNodeCommand.MoveCopyInfo>();
			for (int i = 1; i < selectNodes.Length; i++)
			{
				ObjectCtrlInfo objectCtrlInfo2 = null;
				if (Singleton<Studio>.Instance.dicInfo.TryGetValue(selectNodes[i], out objectCtrlInfo2))
				{
					list.Add(new TreeNodeCommand.MoveCopyInfo(objectCtrlInfo2.objectInfo.dicKey, objectCtrlInfo2.objectInfo.changeAmount, objectCtrlInfo.objectInfo.changeAmount));
				}
			}
			Singleton<UndoRedoManager>.Instance.Do(new TreeNodeCommand.MoveCopyCommand(list.ToArray()));
			this.SelectSingle(null, true);
		}

		// Token: 0x06009E1E RID: 40478 RVA: 0x00407AB0 File Offset: 0x00405EB0
		public void SelectMultiple(TreeNodeObject _start, TreeNodeObject _end)
		{
			float y = _start.rectNode.position.y;
			float y2 = _end.rectNode.position.y;
			float min = Mathf.Min(y, y2);
			float max = Mathf.Max(y, y2);
			foreach (TreeNodeObject treeNodeObject in this.hashSelectNode)
			{
				treeNodeObject.OnDeselect();
			}
			this.hashSelectNode.Clear();
			this.AddSelectNode(_start, false);
			foreach (TreeNodeObject source in this.m_TreeNodeObject)
			{
				this.SelectMultipleLoop(source, min, max);
			}
			this.AddSelectNode(_end, true);
		}

		// Token: 0x06009E1F RID: 40479 RVA: 0x00407BB8 File Offset: 0x00405FB8
		private void SelectMultipleLoop(TreeNodeObject _source, float _min, float _max)
		{
			if (_source == null)
			{
				return;
			}
			if (MathfEx.RangeEqualOff<float>(_min, _source.rectNode.position.y, _max))
			{
				this.AddSelectNode(_source, true);
			}
			if (_source.treeState == TreeNodeObject.TreeState.Close)
			{
				return;
			}
			foreach (TreeNodeObject source in _source.child)
			{
				this.SelectMultipleLoop(source, _min, _max);
			}
		}

		// Token: 0x06009E20 RID: 40480 RVA: 0x00407C58 File Offset: 0x00406058
		private void RefreshHierachyLoop(TreeNodeObject _source, int _indent, bool _visible)
		{
			_source.transform.SetAsLastSibling();
			_source.treeNode.indent = _indent;
			if (_source.gameObject.activeSelf != _visible)
			{
				_source.gameObject.SetActive(_visible);
			}
			int childCount = _source.childCount;
			if (_visible)
			{
				_visible = (_source.treeState == TreeNodeObject.TreeState.Open);
			}
			for (int i = 0; i < childCount; i++)
			{
				this.RefreshHierachyLoop(_source.child[i], _indent + 1, _visible);
			}
		}

		// Token: 0x06009E21 RID: 40481 RVA: 0x00407CDC File Offset: 0x004060DC
		private void RefreshVisibleLoop(TreeNodeObject _source)
		{
			if (!_source.visible)
			{
				_source.visible = _source.visible;
				return;
			}
			int childCount = _source.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.RefreshVisibleLoop(_source.child[i]);
			}
		}

		// Token: 0x06009E22 RID: 40482 RVA: 0x00407D2C File Offset: 0x0040612C
		private void DeleteNodeLoop(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return;
			}
			if (_node.onDelete != null)
			{
				_node.onDelete();
			}
			int childCount = _node.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.DeleteNodeLoop(_node.child[i]);
			}
			UnityEngine.Object.Destroy(_node.gameObject);
			if (this.onDelete != null)
			{
				this.onDelete(_node);
			}
		}

		// Token: 0x06009E23 RID: 40483 RVA: 0x00407DAC File Offset: 0x004061AC
		private void DeleteAllNodeLoop(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return;
			}
			int childCount = _node.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.DeleteAllNodeLoop(_node.child[i]);
			}
			UnityEngine.Object.DestroyImmediate(_node.gameObject);
		}

		// Token: 0x06009E24 RID: 40484 RVA: 0x00407DFC File Offset: 0x004061FC
		private void SetSelectNode(TreeNodeObject _node)
		{
			bool flag = Input.GetKey(KeyCode.LeftControl) | Input.GetKey(KeyCode.RightControl);
			TreeNodeCtrl.Calc calc = TreeNodeCtrl.Calc.None;
			if (this.selectNode && Input.GetKey(KeyCode.P))
			{
				calc = TreeNodeCtrl.Calc.Attach;
			}
			else if (Input.GetKey(KeyCode.O))
			{
				calc = TreeNodeCtrl.Calc.Detach;
			}
			else if (Input.GetKey(KeyCode.X))
			{
				calc = TreeNodeCtrl.Calc.Copy;
			}
			switch (calc)
			{
			case TreeNodeCtrl.Calc.Attach:
				if (flag)
				{
					this.hashSelectNode.Add(_node);
					this.SetParent();
				}
				else
				{
					this.SetParent(this.selectNode, _node);
				}
				this.SelectSingle(_node, true);
				return;
			case TreeNodeCtrl.Calc.Detach:
				if (flag)
				{
					this.hashSelectNode.Add(_node);
					foreach (TreeNodeObject node in this.hashSelectNode)
					{
						this.SetParent(node, null);
					}
				}
				else
				{
					this.SetParent(_node, null);
				}
				this.SelectSingle(_node, true);
				return;
			case TreeNodeCtrl.Calc.Copy:
				if (flag)
				{
					this.hashSelectNode.Add(_node);
					if (this.hashSelectNode.Count > 1)
					{
						this.CopyChangeAmount();
					}
				}
				this.SelectSingle(_node, true);
				return;
			}
			if (flag)
			{
				this.AddSelectNode(_node, false);
			}
			else
			{
				this.SelectSingle(_node, true);
			}
		}

		// Token: 0x06009E25 RID: 40485 RVA: 0x00407F94 File Offset: 0x00406394
		public void SelectSingle(TreeNodeObject _node, bool _deselect = true)
		{
			ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(_node);
			bool flag = this.hashSelectNode.Count == 1 && (this.hashSelectNode.Contains(_node) & (ctrlInfo == null || ctrlInfo.guideObject.isActive));
			foreach (TreeNodeObject treeNodeObject in this.hashSelectNode)
			{
				treeNodeObject.OnDeselect();
			}
			this.hashSelectNode.Clear();
			if (_deselect && flag)
			{
				this.DeselectNode(_node);
			}
			else
			{
				this.AddSelectNode(_node, false);
			}
		}

		// Token: 0x06009E26 RID: 40486 RVA: 0x00408060 File Offset: 0x00406460
		private void AddSelectNode(TreeNodeObject _node, bool _multiple = false)
		{
			if (_node == null)
			{
				return;
			}
			if (this.hashSelectNode.Add(_node))
			{
				if (this.onSelect != null && this.hashSelectNode.Count == 1)
				{
					this.onSelect(_node);
				}
				else if (this.onSelectMultiple != null && this.hashSelectNode.Count > 1)
				{
					this.onSelectMultiple();
				}
				_node.colorSelect = ((this.hashSelectNode.Count != 1) ? Utility.ConvertColor(94, 139, 100) : Utility.ConvertColor(91, 164, 82));
				ObjectCtrlInfo objectCtrlInfo = null;
				if (_multiple)
				{
					Singleton<GuideObjectManager>.Instance.AddSelectMultiple((!Singleton<Studio>.Instance.dicInfo.TryGetValue(_node, out objectCtrlInfo)) ? null : objectCtrlInfo.guideObject);
				}
				else
				{
					Singleton<GuideObjectManager>.Instance.selectObject = ((!Singleton<Studio>.Instance.dicInfo.TryGetValue(_node, out objectCtrlInfo)) ? null : objectCtrlInfo.guideObject);
				}
			}
			else
			{
				this.DeselectNode(_node);
			}
		}

		// Token: 0x06009E27 RID: 40487 RVA: 0x0040818C File Offset: 0x0040658C
		private void DeselectNode(TreeNodeObject _node)
		{
			this.hashSelectNode.Remove(_node);
			ObjectCtrlInfo objectCtrlInfo = null;
			if (Singleton<Studio>.Instance.dicInfo.TryGetValue(_node, out objectCtrlInfo))
			{
				Singleton<GuideObjectManager>.Instance.deselectObject = objectCtrlInfo.guideObject;
			}
			_node.OnDeselect();
			if (this.onDeselect != null)
			{
				this.onDeselect(_node);
			}
		}

		// Token: 0x06009E28 RID: 40488 RVA: 0x004081EC File Offset: 0x004065EC
		public bool CheckSelect(TreeNodeObject _node)
		{
			return this.hashSelectNode.Contains(_node);
		}

		// Token: 0x06009E29 RID: 40489 RVA: 0x004081FA File Offset: 0x004065FA
		public void OnPointerDown(PointerEventData eventData)
		{
			SortCanvas.select = this.canvas;
		}

		// Token: 0x06009E2A RID: 40490 RVA: 0x00408207 File Offset: 0x00406607
		private void Start()
		{
			this.m_ObjectRoot.transform.localPosition = Vector3.zero;
		}

		// Token: 0x04007DB1 RID: 32177
		[SerializeField]
		protected GameObject m_ObjectNode;

		// Token: 0x04007DB2 RID: 32178
		[SerializeField]
		protected GameObject m_ObjectRoot;

		// Token: 0x04007DB3 RID: 32179
		[SerializeField]
		protected TreeRoot m_TreeRoot;

		// Token: 0x04007DB4 RID: 32180
		protected List<TreeNodeObject> m_TreeNodeObject = new List<TreeNodeObject>();

		// Token: 0x04007DB5 RID: 32181
		protected HashSet<TreeNodeObject> hashSelectNode = new HashSet<TreeNodeObject>();

		// Token: 0x04007DB6 RID: 32182
		public Action<TreeNodeObject, TreeNodeObject> onParentage;

		// Token: 0x04007DB7 RID: 32183
		public Action<TreeNodeObject> onDelete;

		// Token: 0x04007DB8 RID: 32184
		public Action<TreeNodeObject> onSelect;

		// Token: 0x04007DB9 RID: 32185
		public Action onSelectMultiple;

		// Token: 0x04007DBA RID: 32186
		public Action<TreeNodeObject> onDeselect;

		// Token: 0x04007DBB RID: 32187
		[SerializeField]
		private Canvas canvas;

		// Token: 0x04007DBC RID: 32188
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x020012AD RID: 4781
		private enum Calc
		{
			// Token: 0x04007DBE RID: 32190
			None,
			// Token: 0x04007DBF RID: 32191
			Attach,
			// Token: 0x04007DC0 RID: 32192
			Detach,
			// Token: 0x04007DC1 RID: 32193
			Delete,
			// Token: 0x04007DC2 RID: 32194
			Copy
		}
	}
}
