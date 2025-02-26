using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001217 RID: 4631
	public class OCIFolder : ObjectCtrlInfo
	{
		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x0600980A RID: 38922 RVA: 0x003EBC0B File Offset: 0x003EA00B
		public OIFolderInfo folderInfo
		{
			get
			{
				return this.objectInfo as OIFolderInfo;
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x0600980B RID: 38923 RVA: 0x003EBC18 File Offset: 0x003EA018
		// (set) Token: 0x0600980C RID: 38924 RVA: 0x003EBC25 File Offset: 0x003EA025
		public string name
		{
			get
			{
				return this.folderInfo.name;
			}
			set
			{
				this.folderInfo.name = value;
				this.treeNodeObject.textName = value;
			}
		}

		// Token: 0x0600980D RID: 38925 RVA: 0x003EBC40 File Offset: 0x003EA040
		public override void OnDelete()
		{
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			UnityEngine.Object.Destroy(this.objectItem);
			if (this.parentInfo != null)
			{
				this.parentInfo.OnDetachChild(this);
			}
			Studio.DeleteInfo(this.objectInfo, true);
		}

		// Token: 0x0600980E RID: 38926 RVA: 0x003EBC8C File Offset: 0x003EA08C
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.folderInfo.child.Contains(_child.objectInfo))
			{
				this.folderInfo.child.Add(_child.objectInfo);
			}
			bool flag = false;
			if (_child is OCIItem)
			{
				flag = (_child as OCIItem).IsParticleArray;
			}
			if (!flag)
			{
				_child.guideObject.transformTarget.SetParent(this.childRoot);
			}
			_child.guideObject.parent = this.childRoot;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			if (!flag)
			{
				_child.objectInfo.changeAmount.pos = _child.guideObject.transformTarget.localPosition;
				_child.objectInfo.changeAmount.rot = _child.guideObject.transformTarget.localEulerAngles;
			}
			else if (_child.guideObject.nonconnect)
			{
				_child.objectInfo.changeAmount.pos = _child.guideObject.parent.InverseTransformPoint(_child.guideObject.transformTarget.position);
				Quaternion quaternion = _child.guideObject.transformTarget.rotation * Quaternion.Inverse(_child.guideObject.parent.rotation);
				_child.objectInfo.changeAmount.rot = quaternion.eulerAngles;
			}
			else
			{
				_child.objectInfo.changeAmount.pos = _child.guideObject.parent.InverseTransformPoint(_child.objectInfo.changeAmount.pos);
				Quaternion quaternion2 = Quaternion.Euler(_child.objectInfo.changeAmount.rot) * Quaternion.Inverse(_child.guideObject.parent.rotation);
				_child.objectInfo.changeAmount.rot = quaternion2.eulerAngles;
			}
			_child.guideObject.nonconnect = flag;
			_child.guideObject.calcScale = !flag;
			_child.parentInfo = this;
		}

		// Token: 0x0600980F RID: 38927 RVA: 0x003EBEBC File Offset: 0x003EA2BC
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.folderInfo.child.Contains(_child.objectInfo))
			{
				this.folderInfo.child.Add(_child.objectInfo);
			}
			bool flag = false;
			if (_child is OCIItem)
			{
				flag = (_child as OCIItem).IsParticleArray;
			}
			if (!flag)
			{
				_child.guideObject.transformTarget.SetParent(this.childRoot, false);
			}
			_child.guideObject.parent = this.childRoot;
			_child.guideObject.nonconnect = flag;
			_child.guideObject.calcScale = !flag;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_child.objectInfo.changeAmount.OnChange();
			_child.parentInfo = this;
		}

		// Token: 0x06009810 RID: 38928 RVA: 0x003EBFB4 File Offset: 0x003EA3B4
		public override void OnDetach()
		{
			this.parentInfo.OnDetachChild(this);
			this.guideObject.parent = null;
			Studio.AddInfo(this.objectInfo, this);
			this.objectItem.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			this.objectInfo.changeAmount.pos = this.objectItem.transform.localPosition;
			this.objectInfo.changeAmount.rot = this.objectItem.transform.localEulerAngles;
			this.guideObject.mode = GuideObject.Mode.Local;
			this.guideObject.moveCalc = GuideMove.MoveCalc.TYPE1;
			this.treeNodeObject.ResetVisible();
		}

		// Token: 0x06009811 RID: 38929 RVA: 0x003EC067 File Offset: 0x003EA467
		public override void OnSelect(bool _select)
		{
		}

		// Token: 0x06009812 RID: 38930 RVA: 0x003EC069 File Offset: 0x003EA469
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
			if (!this.folderInfo.child.Remove(_child.objectInfo))
			{
			}
			_child.parentInfo = null;
		}

		// Token: 0x06009813 RID: 38931 RVA: 0x003EC08D File Offset: 0x003EA48D
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
		}

		// Token: 0x06009814 RID: 38932 RVA: 0x003EC095 File Offset: 0x003EA495
		public override void OnVisible(bool _visible)
		{
		}

		// Token: 0x040079A0 RID: 31136
		public GameObject objectItem;

		// Token: 0x040079A1 RID: 31137
		public Transform childRoot;
	}
}
