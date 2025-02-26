using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011F2 RID: 4594
	public static class AddObjectRoute
	{
		// Token: 0x060096EA RID: 38634 RVA: 0x003E7124 File Offset: 0x003E5524
		public static OCIRoute Add()
		{
			return AddObjectRoute.Load(new OIRouteInfo(Studio.GetNewIndex()), null, null, true, Studio.optionSystem.initialPosition);
		}

		// Token: 0x060096EB RID: 38635 RVA: 0x003E7144 File Offset: 0x003E5544
		public static OCIRoute Load(OIRouteInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			ChangeAmount source = _info.changeAmount.Clone();
			OCIRoute ociroute = AddObjectRoute.Load(_info, _parent, _parentNode, false, -1);
			_info.changeAmount.Copy(source, true, true, true);
			AddObjectAssist.LoadChild(_info.child, ociroute, null);
			return ociroute;
		}

		// Token: 0x060096EC RID: 38636 RVA: 0x003E7188 File Offset: 0x003E5588
		public static OCIRoute Load(OIRouteInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCIRoute ociroute = new OCIRoute();
			ociroute.objectInfo = _info;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>("studio/base/00.unity3d", "p_Route", true, string.Empty);
			if (gameObject == null)
			{
				Studio.DeleteIndex(_info.dicKey);
				return null;
			}
			gameObject.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			ociroute.objectItem = gameObject;
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, _info.dicKey);
			guideObject.isActive = false;
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			guideObject.enableScale = false;
			guideObject.SetVisibleCenter(true);
			ociroute.guideObject = guideObject;
			ociroute.childRoot = gameObject.transform.GetChild(0);
			if (_addInfo)
			{
				Studio.AddInfo(_info, ociroute);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ociroute);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(_info.name, parent);
			treeNodeObject.treeState = _info.treeState;
			treeNodeObject.enableVisible = true;
			treeNodeObject.enableChangeParent = false;
			treeNodeObject.visible = _info.visible;
			treeNodeObject.colorSelect = treeNodeObject.baseColor;
			TreeNodeObject treeNodeObject2 = treeNodeObject;
			treeNodeObject2.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject2.onVisible, new TreeNodeObject.OnVisibleFunc(ociroute.OnVisible));
			TreeNodeObject treeNodeObject3 = treeNodeObject;
			treeNodeObject3.onDelete = (Action)Delegate.Combine(treeNodeObject3.onDelete, new Action(ociroute.OnDeleteNode));
			TreeNodeObject treeNodeObject4 = treeNodeObject;
			treeNodeObject4.checkChild = (TreeNodeObject.CheckFunc)Delegate.Combine(treeNodeObject4.checkChild, new TreeNodeObject.CheckFunc(ociroute.CheckParentLoop));
			TreeNodeObject treeNodeObject5 = treeNodeObject;
			treeNodeObject5.checkParent = (TreeNodeObject.CheckFunc)Delegate.Combine(treeNodeObject5.checkParent, new TreeNodeObject.CheckFunc(ociroute.CheckParentLoop));
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ociroute.treeNodeObject = treeNodeObject;
			ociroute.routeComponent = gameObject.GetComponent<RouteComponent>();
			TreeNodeObject treeNodeObject6 = Studio.AddNode("子接続先", treeNodeObject);
			treeNodeObject6.enableChangeParent = false;
			treeNodeObject6.enableDelete = false;
			treeNodeObject6.enableCopy = false;
			treeNodeObject6.baseColor = Utility.ConvertColor(204, 128, 164);
			treeNodeObject6.colorSelect = treeNodeObject6.baseColor;
			treeNodeObject.childRoot = treeNodeObject6;
			ociroute.childNodeRoot = treeNodeObject6;
			if (_info.route.IsNullOrEmpty<OIRoutePointInfo>())
			{
				ociroute.routeInfo.route.Add(new OIRoutePointInfo(Studio.GetNewIndex()));
			}
			foreach (OIRoutePointInfo rpInfo in _info.route)
			{
				AddObjectRoute.LoadPoint(ociroute, rpInfo, -1);
			}
			Singleton<Studio>.Instance.treeNodeCtrl.RefreshHierachy();
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			_info.changeAmount.OnChange();
			Studio.AddCtrlInfo(ociroute);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ociroute);
			}
			ChangeAmount changeAmount = _info.changeAmount;
			changeAmount.onChangePos = (Action)Delegate.Combine(changeAmount.onChangePos, new Action(ociroute.UpdateLine));
			ChangeAmount changeAmount2 = _info.changeAmount;
			changeAmount2.onChangeRot = (Action)Delegate.Combine(changeAmount2.onChangeRot, new Action(ociroute.UpdateLine));
			ociroute.ForceUpdateLine();
			ociroute.visibleLine = ociroute.visibleLine;
			if (ociroute.isPlay)
			{
				ociroute.Play();
			}
			else
			{
				ociroute.Stop(true);
			}
			return ociroute;
		}

		// Token: 0x060096ED RID: 38637 RVA: 0x003E7554 File Offset: 0x003E5954
		public static OCIRoutePoint AddPoint(OCIRoute _ocir)
		{
			OIRoutePointInfo oiroutePointInfo = new OIRoutePointInfo(Studio.GetNewIndex());
			_ocir.routeInfo.route.Add(oiroutePointInfo);
			OCIRoutePoint result = AddObjectRoute.LoadPoint(_ocir, oiroutePointInfo, 1);
			_ocir.visibleLine = _ocir.visibleLine;
			Singleton<Studio>.Instance.treeNodeCtrl.RefreshHierachy();
			return result;
		}

		// Token: 0x060096EE RID: 38638 RVA: 0x003E75A4 File Offset: 0x003E59A4
		public static OCIRoutePoint LoadPoint(OCIRoute _ocir, OIRoutePointInfo _rpInfo, int _initialPosition)
		{
			int num = (!_ocir.listPoint.IsNullOrEmpty<OCIRoutePoint>()) ? (_ocir.listPoint.Count - 1) : -1;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>("studio/base/00.unity3d", "p_RoutePoint", true, string.Empty);
			if (gameObject == null)
			{
				Studio.DeleteIndex(_rpInfo.dicKey);
				return null;
			}
			gameObject.transform.SetParent(_ocir.objectItem.transform);
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, _rpInfo.dicKey);
			guideObject.isActive = false;
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			guideObject.enablePos = (num != -1);
			guideObject.enableRot = (num == -1);
			guideObject.enableScale = false;
			guideObject.mode = GuideObject.Mode.World;
			guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			TreeNodeObject childRoot = _ocir.treeNodeObject.childRoot;
			_ocir.treeNodeObject.childRoot = null;
			TreeNodeObject treeNodeObject = Studio.AddNode(_rpInfo.name, _ocir.treeNodeObject);
			treeNodeObject.treeState = _rpInfo.treeState;
			treeNodeObject.enableChangeParent = false;
			treeNodeObject.enableDelete = (num != -1);
			treeNodeObject.enableAddChild = false;
			treeNodeObject.enableCopy = false;
			treeNodeObject.enableVisible = false;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			_ocir.treeNodeObject.childRoot = childRoot;
			OCIRoutePoint ociroutePoint = new OCIRoutePoint(_ocir, _rpInfo, gameObject, guideObject, treeNodeObject);
			_ocir.listPoint.Add(ociroutePoint);
			_ocir.UpdateNumber();
			TreeNodeObject treeNodeObject2 = treeNodeObject;
			treeNodeObject2.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject2.onVisible, new TreeNodeObject.OnVisibleFunc(ociroutePoint.OnVisible));
			GuideObject guideObject2 = guideObject;
			guideObject2.isActiveFunc = (GuideObject.IsActiveFunc)Delegate.Combine(guideObject2.isActiveFunc, new GuideObject.IsActiveFunc(ociroutePoint.OnSelect));
			Studio.AddCtrlInfo(ociroutePoint);
			AddObjectRoute.InitAid(ociroutePoint);
			if (_initialPosition == 1)
			{
				if (num == -1)
				{
					_rpInfo.changeAmount.pos = _ocir.objectInfo.changeAmount.pos;
				}
				else
				{
					OCIRoutePoint ociroutePoint2 = _ocir.listPoint[num];
					_rpInfo.changeAmount.pos = _ocir.objectItem.transform.InverseTransformPoint(ociroutePoint2.position);
				}
			}
			_rpInfo.changeAmount.OnChange();
			ChangeAmount changeAmount = _rpInfo.changeAmount;
			changeAmount.onChangePosAfter = (Action)Delegate.Combine(changeAmount.onChangePosAfter, new Action(_ocir.UpdateLine));
			ChangeAmount changeAmount2 = _rpInfo.changeAmount;
			changeAmount2.onChangeRot = (Action)Delegate.Combine(changeAmount2.onChangeRot, new Action(_ocir.UpdateLine));
			ociroutePoint.connection = ociroutePoint.connection;
			return ociroutePoint;
		}

		// Token: 0x060096EF RID: 38639 RVA: 0x003E7848 File Offset: 0x003E5C48
		public static void InitAid(OCIRoutePoint _ocirp)
		{
			bool flag = _ocirp.routePointInfo.aidInfo == null;
			if (flag)
			{
				_ocirp.routePointInfo.aidInfo = new OIRoutePointAidInfo(Studio.GetNewIndex());
			}
			Transform transform = _ocirp.routePoint.objAid.transform;
			if (flag)
			{
				_ocirp.routePointInfo.aidInfo.changeAmount.pos = transform.localPosition;
			}
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(transform, _ocirp.routePointInfo.aidInfo.dicKey);
			guideObject.enableRot = false;
			guideObject.enableScale = false;
			guideObject.enableMaluti = false;
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			guideObject.parentGuide = _ocirp.guideObject;
			guideObject.changeAmount.OnChange();
			guideObject.mode = GuideObject.Mode.World;
			guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_ocirp.pointAidInfo = new OCIRoutePoint.PointAidInfo(guideObject, _ocirp.routePointInfo.aidInfo);
			_ocirp.pointAidInfo.active = false;
			ChangeAmount changeAmount = _ocirp.routePointInfo.aidInfo.changeAmount;
			changeAmount.onChangePosAfter = (Action)Delegate.Combine(changeAmount.onChangePosAfter, new Action(_ocirp.route.UpdateLine));
		}
	}
}
