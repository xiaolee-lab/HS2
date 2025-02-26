using System;
using System.Collections.Generic;
using GUITree;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012AE RID: 4782
	public class TreeNodeObject : MonoBehaviour
	{
		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x06009E2C RID: 40492 RVA: 0x00408243 File Offset: 0x00406643
		public TreeNode treeNode
		{
			get
			{
				return this.m_TreeNode;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x06009E2D RID: 40493 RVA: 0x0040824B File Offset: 0x0040664B
		public Button buttonState
		{
			get
			{
				return this.m_ButtonState;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06009E2E RID: 40494 RVA: 0x00408253 File Offset: 0x00406653
		public Button buttonSelect
		{
			get
			{
				return this.m_ButtonSelect;
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x06009E2F RID: 40495 RVA: 0x0040825B File Offset: 0x0040665B
		public Image imageSelect
		{
			get
			{
				return this.m_ImageSelect;
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (set) Token: 0x06009E30 RID: 40496 RVA: 0x00408263 File Offset: 0x00406663
		public Color colorSelect
		{
			set
			{
				this.imageSelect.color = value;
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x06009E31 RID: 40497 RVA: 0x00408271 File Offset: 0x00406671
		// (set) Token: 0x06009E32 RID: 40498 RVA: 0x0040827E File Offset: 0x0040667E
		public string textName
		{
			get
			{
				return this.m_TextName.text;
			}
			set
			{
				this.m_TextName.text = value;
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x06009E33 RID: 40499 RVA: 0x0040828C File Offset: 0x0040668C
		// (set) Token: 0x06009E34 RID: 40500 RVA: 0x00408294 File Offset: 0x00406694
		public TreeNodeObject.TreeState treeState
		{
			get
			{
				return this.m_TreeState;
			}
			set
			{
				if (Utility.SetStruct<TreeNodeObject.TreeState>(ref this.m_TreeState, value))
				{
					this.SetTreeState(this.m_TreeState);
				}
			}
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x06009E35 RID: 40501 RVA: 0x004082B3 File Offset: 0x004066B3
		public Image imageState
		{
			get
			{
				if (this.m_ImageState == null)
				{
					this.m_ImageState = this.m_ButtonState.GetComponent<Image>();
				}
				return this.m_ImageState;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06009E36 RID: 40502 RVA: 0x004082DD File Offset: 0x004066DD
		// (set) Token: 0x06009E37 RID: 40503 RVA: 0x004082E5 File Offset: 0x004066E5
		public bool visible
		{
			get
			{
				return this.m_Visible;
			}
			set
			{
				this.SetVisible(value);
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06009E38 RID: 40504 RVA: 0x004082EE File Offset: 0x004066EE
		public Button buttonVisible
		{
			get
			{
				return this.m_ButtonVisible;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06009E39 RID: 40505 RVA: 0x004082F6 File Offset: 0x004066F6
		public Image imageVisible
		{
			get
			{
				if (this.m_ImageVisible == null)
				{
					this.m_ImageVisible = this.m_ButtonVisible.image;
				}
				return this.m_ImageVisible;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x06009E3A RID: 40506 RVA: 0x00408320 File Offset: 0x00406720
		public float imageVisibleWidth
		{
			get
			{
				return this.imageVisible.rectTransform.sizeDelta.x;
			}
		}

		// Token: 0x170021D9 RID: 8665
		// (set) Token: 0x06009E3B RID: 40507 RVA: 0x00408345 File Offset: 0x00406745
		public bool enableVisible
		{
			set
			{
				this.m_ButtonVisible.gameObject.SetActive(value);
				this.RecalcSelectButtonPos();
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x06009E3C RID: 40508 RVA: 0x0040835E File Offset: 0x0040675E
		public RectTransform rectNode
		{
			get
			{
				return this._rectNode;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06009E3D RID: 40509 RVA: 0x00408366 File Offset: 0x00406766
		// (set) Token: 0x06009E3E RID: 40510 RVA: 0x0040836E File Offset: 0x0040676E
		public TreeNodeObject parent { get; set; }

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06009E3F RID: 40511 RVA: 0x00408377 File Offset: 0x00406777
		public bool isParent
		{
			get
			{
				return this.parent != null && this.enableChangeParent;
			}
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06009E40 RID: 40512 RVA: 0x00408393 File Offset: 0x00406793
		public int childCount
		{
			get
			{
				return this.m_child.Count;
			}
		}

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x06009E41 RID: 40513 RVA: 0x004083A0 File Offset: 0x004067A0
		public List<TreeNodeObject> child
		{
			get
			{
				return this.m_child;
			}
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x06009E42 RID: 40514 RVA: 0x004083A8 File Offset: 0x004067A8
		// (set) Token: 0x06009E43 RID: 40515 RVA: 0x004083B0 File Offset: 0x004067B0
		public bool enableChangeParent { get; set; }

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x06009E44 RID: 40516 RVA: 0x004083B9 File Offset: 0x004067B9
		// (set) Token: 0x06009E45 RID: 40517 RVA: 0x004083C1 File Offset: 0x004067C1
		public bool enableDelete { get; set; }

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x06009E46 RID: 40518 RVA: 0x004083CA File Offset: 0x004067CA
		// (set) Token: 0x06009E47 RID: 40519 RVA: 0x004083D2 File Offset: 0x004067D2
		public bool enableAddChild { get; set; }

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x06009E48 RID: 40520 RVA: 0x004083DB File Offset: 0x004067DB
		// (set) Token: 0x06009E49 RID: 40521 RVA: 0x004083E3 File Offset: 0x004067E3
		public bool enableCopy { get; set; }

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x06009E4A RID: 40522 RVA: 0x004083EC File Offset: 0x004067EC
		// (set) Token: 0x06009E4B RID: 40523 RVA: 0x004083F4 File Offset: 0x004067F4
		public Color baseColor { get; set; }

		// Token: 0x170021E4 RID: 8676
		// (set) Token: 0x06009E4C RID: 40524 RVA: 0x004083FD File Offset: 0x004067FD
		public float addPosX
		{
			set
			{
				this._addPosX = value;
				this.RecalcSelectButtonPos();
			}
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06009E4D RID: 40525 RVA: 0x0040840C File Offset: 0x0040680C
		// (set) Token: 0x06009E4E RID: 40526 RVA: 0x00408414 File Offset: 0x00406814
		public TreeNodeObject childRoot { get; set; }

		// Token: 0x06009E4F RID: 40527 RVA: 0x0040841D File Offset: 0x0040681D
		public void OnClickState()
		{
			SortCanvas.select = this.canvas;
			this.SetTreeState((this.m_TreeState != TreeNodeObject.TreeState.Open) ? TreeNodeObject.TreeState.Open : TreeNodeObject.TreeState.Close);
		}

		// Token: 0x06009E50 RID: 40528 RVA: 0x00408442 File Offset: 0x00406842
		public void OnClickSelect()
		{
			this.Select(true);
		}

		// Token: 0x06009E51 RID: 40529 RVA: 0x0040844B File Offset: 0x0040684B
		public void OnClickVisible()
		{
			SortCanvas.select = this.canvas;
			this.SetVisible(!this.m_Visible);
		}

		// Token: 0x06009E52 RID: 40530 RVA: 0x00408468 File Offset: 0x00406868
		public void OnDeselect()
		{
			this.colorSelect = this.baseColor;
			ObjectCtrlInfo objectCtrlInfo = null;
			if (Singleton<Studio>.Instance.dicInfo.TryGetValue(this, out objectCtrlInfo))
			{
				objectCtrlInfo.guideObject.SetActive(false, true);
			}
		}

		// Token: 0x06009E53 RID: 40531 RVA: 0x004084A8 File Offset: 0x004068A8
		public bool SetParent(TreeNodeObject _parent)
		{
			if (!this.enableChangeParent)
			{
				return false;
			}
			if (!this.CheckChildLoop(this, _parent))
			{
				return false;
			}
			if (_parent != null && _parent.childRoot != null)
			{
				_parent = _parent.childRoot;
			}
			if (this.CheckParentLoop(_parent, this))
			{
				return false;
			}
			if (_parent && (_parent.child.Contains(this) || !_parent.enableAddChild))
			{
				return false;
			}
			bool flag = true;
			if (this.checkParent != null)
			{
				flag &= this.checkParent(_parent);
			}
			if (!flag)
			{
				return false;
			}
			if (this.parent != null)
			{
				this.parent.RemoveChild(this, false);
			}
			if (_parent)
			{
				_parent.AddChild(this);
			}
			else
			{
				this.parent = null;
				this.m_TreeNodeCtrl.AddNode(this);
			}
			return true;
		}

		// Token: 0x06009E54 RID: 40532 RVA: 0x004085A0 File Offset: 0x004069A0
		public bool AddChild(TreeNodeObject _child)
		{
			if (!this.enableAddChild)
			{
				return false;
			}
			if (_child == null || _child == this)
			{
				return false;
			}
			if (this.m_child.Contains(_child))
			{
				return false;
			}
			this.m_child.Add(_child);
			_child.parent = this;
			this.m_TreeNodeCtrl.RemoveNode(_child);
			this.SetStateVisible(true);
			this.SetTreeState(this.m_TreeState);
			this.SetVisibleChild(_child, this.m_Visible);
			return true;
		}

		// Token: 0x06009E55 RID: 40533 RVA: 0x00408628 File Offset: 0x00406A28
		public void RemoveChild(TreeNodeObject _child, bool _removeOnly = false)
		{
			_child.transform.SetAsLastSibling();
			this.m_child.Remove(_child);
			if (_removeOnly)
			{
				return;
			}
			_child.parent = null;
			this.m_TreeNodeCtrl.AddNode(_child);
			this.SetStateVisible(this.childCount != 0);
		}

		// Token: 0x06009E56 RID: 40534 RVA: 0x0040867C File Offset: 0x00406A7C
		public void SetTreeState(TreeNodeObject.TreeState _state)
		{
			this.m_TreeState = _state;
			this.imageState.sprite = this.m_SpriteState[(int)_state];
			bool visible = _state == TreeNodeObject.TreeState.Open;
			foreach (TreeNodeObject source in this.m_child)
			{
				this.SetVisibleLoop(source, visible);
			}
		}

		// Token: 0x06009E57 RID: 40535 RVA: 0x004086F8 File Offset: 0x00406AF8
		public void SetVisible(bool _visible)
		{
			this.m_Visible = _visible;
			if (this.onVisible != null)
			{
				this.onVisible(_visible);
			}
			this.imageVisible.sprite = this.m_SpriteVisible[(!_visible) ? 0 : 1];
			foreach (TreeNodeObject source in this.m_child)
			{
				this.SetVisibleChild(source, _visible);
			}
		}

		// Token: 0x06009E58 RID: 40536 RVA: 0x00408794 File Offset: 0x00406B94
		public void ResetVisible()
		{
			if (this.onVisible != null)
			{
				this.onVisible(this.m_Visible);
			}
			this.imageVisible.sprite = this.m_SpriteVisible[(!this.m_Visible) ? 0 : 1];
			this.buttonVisible.interactable = true;
			foreach (TreeNodeObject source in this.child)
			{
				this.SetVisibleChild(source, this.m_Visible);
			}
		}

		// Token: 0x06009E59 RID: 40537 RVA: 0x00408844 File Offset: 0x00406C44
		public void Select(bool _button = false)
		{
			SortCanvas.select = this.canvas;
			TreeNodeObject selectNode = this.m_TreeNodeCtrl.selectNode;
			if (_button && selectNode != null && (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift)))
			{
				this.m_TreeNodeCtrl.SelectMultiple(selectNode, this);
			}
			else
			{
				this.m_TreeNodeCtrl.selectNode = this;
			}
		}

		// Token: 0x06009E5A RID: 40538 RVA: 0x004088B2 File Offset: 0x00406CB2
		protected void SetStateVisible(bool _visible)
		{
			if (this.m_ButtonState)
			{
				this.m_ButtonState.gameObject.SetActive(_visible);
			}
		}

		// Token: 0x06009E5B RID: 40539 RVA: 0x004088D8 File Offset: 0x00406CD8
		protected void SetVisibleLoop(TreeNodeObject _source, bool _visible)
		{
			if (_source.gameObject.activeSelf != _visible)
			{
				_source.gameObject.SetActive(_visible);
			}
			if (_visible && _source.treeState == TreeNodeObject.TreeState.Close)
			{
				_visible = false;
			}
			foreach (TreeNodeObject source in _source.child)
			{
				this.SetVisibleLoop(source, _visible);
			}
		}

		// Token: 0x06009E5C RID: 40540 RVA: 0x00408968 File Offset: 0x00406D68
		protected bool CheckParentLoop(TreeNodeObject _source, TreeNodeObject _target)
		{
			return !(_source == null) && (_source == _target || this.CheckParentLoop(_source.parent, _target));
		}

		// Token: 0x06009E5D RID: 40541 RVA: 0x00408994 File Offset: 0x00406D94
		protected void SetVisibleChild(TreeNodeObject _source, bool _visible)
		{
			bool flag = (!_visible || _source.visible) && _visible;
			if (_source.onVisible != null)
			{
				_source.onVisible(flag);
			}
			_source.buttonVisible.interactable = _visible;
			foreach (TreeNodeObject source in _source.child)
			{
				this.SetVisibleChild(source, flag);
			}
		}

		// Token: 0x06009E5E RID: 40542 RVA: 0x00408A30 File Offset: 0x00406E30
		protected void RecalcSelectButtonPos()
		{
			Vector2 anchoredPosition = this.m_TransSelect.anchoredPosition;
			anchoredPosition.x = this._addPosX + ((!this.m_ButtonVisible.gameObject.activeSelf) ? (this.textPosX * 0.5f) : this.textPosX);
			this.m_TransSelect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06009E5F RID: 40543 RVA: 0x00408A90 File Offset: 0x00406E90
		protected bool CheckChildLoop(TreeNodeObject _source, TreeNodeObject _parent)
		{
			if (_source == null || _parent == null)
			{
				return true;
			}
			bool flag = true;
			if (_source.checkChild != null)
			{
				flag &= _source.checkChild(_parent);
			}
			if (!flag)
			{
				return false;
			}
			foreach (TreeNodeObject source in _source.child)
			{
				if (!this.CheckChildLoop(source, _parent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06009E60 RID: 40544 RVA: 0x00408B3C File Offset: 0x00406F3C
		private void Awake()
		{
			this.enableChangeParent = true;
			this.enableDelete = true;
			this.enableAddChild = true;
			this.enableCopy = true;
			this.baseColor = Utility.ConvertColor(100, 99, 94);
			this.colorSelect = this.baseColor;
		}

		// Token: 0x06009E61 RID: 40545 RVA: 0x00408B77 File Offset: 0x00406F77
		private void Start()
		{
			this.SetStateVisible(this.childCount != 0);
		}

		// Token: 0x04007DC3 RID: 32195
		[SerializeField]
		protected TreeNode m_TreeNode;

		// Token: 0x04007DC4 RID: 32196
		[SerializeField]
		protected Sprite[] m_SpriteState;

		// Token: 0x04007DC5 RID: 32197
		[SerializeField]
		protected Button m_ButtonState;

		// Token: 0x04007DC6 RID: 32198
		[SerializeField]
		protected Button m_ButtonSelect;

		// Token: 0x04007DC7 RID: 32199
		[SerializeField]
		protected Image m_ImageSelect;

		// Token: 0x04007DC8 RID: 32200
		[SerializeField]
		protected RectTransform m_TransSelect;

		// Token: 0x04007DC9 RID: 32201
		[SerializeField]
		protected Text m_TextName;

		// Token: 0x04007DCA RID: 32202
		[SerializeField]
		protected Canvas canvas;

		// Token: 0x04007DCB RID: 32203
		protected TreeNodeObject.TreeState m_TreeState;

		// Token: 0x04007DCC RID: 32204
		protected Image m_ImageState;

		// Token: 0x04007DCD RID: 32205
		[SerializeField]
		protected TreeNodeCtrl m_TreeNodeCtrl;

		// Token: 0x04007DCE RID: 32206
		protected bool m_Visible = true;

		// Token: 0x04007DCF RID: 32207
		[SerializeField]
		protected Button m_ButtonVisible;

		// Token: 0x04007DD0 RID: 32208
		protected Image m_ImageVisible;

		// Token: 0x04007DD1 RID: 32209
		[SerializeField]
		protected Sprite[] m_SpriteVisible;

		// Token: 0x04007DD2 RID: 32210
		public TreeNodeObject.OnVisibleFunc onVisible;

		// Token: 0x04007DD3 RID: 32211
		[SerializeField]
		private float textPosX = 40f;

		// Token: 0x04007DD4 RID: 32212
		[Space(10f)]
		[SerializeField]
		private RectTransform _rectNode;

		// Token: 0x04007DD6 RID: 32214
		protected List<TreeNodeObject> m_child = new List<TreeNodeObject>();

		// Token: 0x04007DDC RID: 32220
		private float _addPosX;

		// Token: 0x04007DDE RID: 32222
		public Action onDelete;

		// Token: 0x04007DDF RID: 32223
		public TreeNodeObject.CheckFunc checkChild;

		// Token: 0x04007DE0 RID: 32224
		public TreeNodeObject.CheckFunc checkParent;

		// Token: 0x020012AF RID: 4783
		public enum TreeState
		{
			// Token: 0x04007DE2 RID: 32226
			Open,
			// Token: 0x04007DE3 RID: 32227
			Close
		}

		// Token: 0x020012B0 RID: 4784
		// (Invoke) Token: 0x06009E63 RID: 40547
		public delegate void OnVisibleFunc(bool _b);

		// Token: 0x020012B1 RID: 4785
		// (Invoke) Token: 0x06009E67 RID: 40551
		public delegate bool CheckFunc(TreeNodeObject _parent);
	}
}
