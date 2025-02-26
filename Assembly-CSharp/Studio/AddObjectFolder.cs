using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011EE RID: 4590
	public static class AddObjectFolder
	{
		// Token: 0x060096D9 RID: 38617 RVA: 0x003E5C10 File Offset: 0x003E4010
		public static OCIFolder Add()
		{
			return AddObjectFolder.Load(new OIFolderInfo(Studio.GetNewIndex()), null, null, true, Studio.optionSystem.initialPosition);
		}

		// Token: 0x060096DA RID: 38618 RVA: 0x003E5C30 File Offset: 0x003E4030
		public static OCIFolder Load(OIFolderInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			ChangeAmount source = _info.changeAmount.Clone();
			OCIFolder ocifolder = AddObjectFolder.Load(_info, _parent, _parentNode, false, -1);
			_info.changeAmount.Copy(source, true, true, true);
			AddObjectAssist.LoadChild(_info.child, ocifolder, null);
			return ocifolder;
		}

		// Token: 0x060096DB RID: 38619 RVA: 0x003E5C74 File Offset: 0x003E4074
		public static OCIFolder Load(OIFolderInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCIFolder ocifolder = new OCIFolder();
			ocifolder.objectInfo = _info;
			GameObject gameObject = new GameObject(_info.name);
			if (gameObject == null)
			{
				Studio.DeleteIndex(_info.dicKey);
				return null;
			}
			gameObject.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			ocifolder.objectItem = gameObject;
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, _info.dicKey);
			guideObject.isActive = false;
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			guideObject.enableScale = false;
			guideObject.SetVisibleCenter(true);
			ocifolder.guideObject = guideObject;
			ocifolder.childRoot = gameObject.transform;
			if (_addInfo)
			{
				Studio.AddInfo(_info, ocifolder);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ocifolder);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(_info.name, parent);
			treeNodeObject.treeState = _info.treeState;
			treeNodeObject.enableVisible = true;
			treeNodeObject.visible = _info.visible;
			treeNodeObject.baseColor = Utility.ConvertColor(180, 150, 5);
			treeNodeObject.colorSelect = treeNodeObject.baseColor;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ocifolder.treeNodeObject = treeNodeObject;
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			_info.changeAmount.OnChange();
			Studio.AddCtrlInfo(ocifolder);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ocifolder);
			}
			return ocifolder;
		}
	}
}
