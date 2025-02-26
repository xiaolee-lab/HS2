using System;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x020011EB RID: 4587
	public static class AddObjectCamera
	{
		// Token: 0x060096C0 RID: 38592 RVA: 0x003E4105 File Offset: 0x003E2505
		public static OCICamera Add()
		{
			return AddObjectCamera.Load(new OICameraInfo(Studio.GetNewIndex()), null, null, true, Studio.optionSystem.initialPosition);
		}

		// Token: 0x060096C1 RID: 38593 RVA: 0x003E4124 File Offset: 0x003E2524
		public static OCICamera Load(OICameraInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			ChangeAmount source = _info.changeAmount.Clone();
			OCICamera result = AddObjectCamera.Load(_info, _parent, _parentNode, false, -1);
			_info.changeAmount.Copy(source, true, true, true);
			return result;
		}

		// Token: 0x060096C2 RID: 38594 RVA: 0x003E4158 File Offset: 0x003E2558
		public static OCICamera Load(OICameraInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCICamera ocic = new OCICamera();
			ocic.objectInfo = _info;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>("studio/base/00.unity3d", "p_koi_stu_cameraicon00_00", true, string.Empty);
			if (gameObject == null)
			{
				Studio.DeleteIndex(_info.dicKey);
				return null;
			}
			gameObject.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			ocic.objectItem = gameObject;
			ocic.meshRenderer = gameObject.GetComponent<MeshRenderer>();
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, _info.dicKey);
			guideObject.isActive = false;
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			guideObject.enableScale = false;
			ocic.guideObject = guideObject;
			if (_addInfo)
			{
				Studio.AddInfo(_info, ocic);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ocic);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(_info.name, parent);
			TreeNodeObject treeNodeObject2 = treeNodeObject;
			treeNodeObject2.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject2.onVisible, new TreeNodeObject.OnVisibleFunc(ocic.OnVisible));
			treeNodeObject.treeState = _info.treeState;
			treeNodeObject.enableVisible = true;
			treeNodeObject.enableAddChild = false;
			treeNodeObject.visible = _info.visible;
			treeNodeObject.baseColor = ((!_info.active) ? AddObjectCamera.baseColor : AddObjectCamera.activeColor);
			treeNodeObject.colorSelect = treeNodeObject.baseColor;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ocic.treeNodeObject = treeNodeObject;
			treeNodeObject.buttonSelect.OnPointerClickAsObservable().Subscribe(delegate(PointerEventData _ped)
			{
				if (_ped.button != PointerEventData.InputButton.Right)
				{
					return;
				}
				Singleton<Studio>.Instance.ChangeCamera(ocic);
				Singleton<Studio>.Instance.manipulatePanelCtrl.UpdateInfo(5);
			});
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			_info.changeAmount.OnChange();
			Studio.AddCtrlInfo(ocic);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ocic);
			}
			Singleton<Studio>.Instance.ChangeCamera(ocic, _info.active, false);
			return ocic;
		}

		// Token: 0x0400791C RID: 31004
		public static Color baseColor = Utility.ConvertColor(0, 104, 183);

		// Token: 0x0400791D RID: 31005
		public static Color activeColor = Utility.ConvertColor(200, 0, 0);
	}
}
