using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Manager;
using RootMotion.FinalIK;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011F1 RID: 4593
	public static class AddObjectMale
	{
		// Token: 0x060096E5 RID: 38629 RVA: 0x003E69DC File Offset: 0x003E4DDC
		public static OCICharMale Add(string _path)
		{
			ChaControl chaControl = Singleton<Character>.Instance.CreateChara(0, Singleton<Scene>.Instance.commonSpace, -1, null);
			chaControl.chaFile.LoadCharaFile(_path, byte.MaxValue, true, true);
			chaControl.fileStatus.neckLookPtn = 3;
			OICharInfo info = new OICharInfo(chaControl.chaFile, Studio.GetNewIndex());
			return AddObjectMale.Add(chaControl, info, null, null, true, Studio.optionSystem.initialPosition);
		}

		// Token: 0x060096E6 RID: 38630 RVA: 0x003E6A48 File Offset: 0x003E4E48
		public static OCICharMale Load(OICharInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			OCICharMale ocicharMale = AddObjectMale.Add(Singleton<Character>.Instance.CreateChara(0, Singleton<Scene>.Instance.commonSpace, -1, _info.charFile), _info, _parent, _parentNode, false, -1);
			using (Dictionary<int, List<ObjectInfo>>.Enumerator enumerator = _info.child.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, List<ObjectInfo>> v = enumerator.Current;
					AddObjectAssist.LoadChild(v.Value, ocicharMale, ocicharMale.dicAccessoryPoint.First((KeyValuePair<TreeNodeObject, int> x) => x.Value == v.Key).Key);
				}
			}
			return ocicharMale;
		}

		// Token: 0x060096E7 RID: 38631 RVA: 0x003E6B00 File Offset: 0x003E4F00
		private static OCICharMale Add(ChaControl _male, OICharInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCICharMale ocicharMale = new OCICharMale();
			ChaFileStatus chaFileStatus = new ChaFileStatus();
			chaFileStatus.Copy(_male.fileStatus);
			_male.ChangeNowCoordinate(false, true);
			_male.Load(true);
			_male.InitializeExpression(1, true);
			ocicharMale.charInfo = _male;
			ocicharMale.charReference = _male;
			ocicharMale.preparation = _male.objAnim.GetComponent<Preparation>();
			ocicharMale.finalIK = _male.fullBodyIK;
			for (int m = 0; m < 2; m++)
			{
				GameObject gameObject = _male.objHair.SafeGet(m);
				if (gameObject != null)
				{
					AddObjectAssist.ArrangeNames(gameObject.transform);
				}
			}
			AddObjectAssist.SetupAccessoryDynamicBones(ocicharMale);
			AddObjectAssist.DisableComponent(ocicharMale);
			ocicharMale.objectInfo = _info;
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(_male.transform, _info.dicKey);
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			GuideObject guideObject2 = guideObject;
			guideObject2.isActiveFunc = (GuideObject.IsActiveFunc)Delegate.Combine(guideObject2.isActiveFunc, new GuideObject.IsActiveFunc(ocicharMale.OnSelect));
			guideObject.SetVisibleCenter(true);
			ocicharMale.guideObject = guideObject;
			ocicharMale.optionItemCtrl = _male.gameObject.AddComponent<OptionItemCtrl>();
			ocicharMale.optionItemCtrl.animator = _male.animBody;
			ocicharMale.optionItemCtrl.oiCharInfo = _info;
			ChangeAmount changeAmount = _info.changeAmount;
			changeAmount.onChangeScale = (Action<Vector3>)Delegate.Combine(changeAmount.onChangeScale, new Action<Vector3>(ocicharMale.optionItemCtrl.ChangeScale));
			ocicharMale.charAnimeCtrl = ocicharMale.preparation.CharAnimeCtrl;
			ocicharMale.charAnimeCtrl.oiCharInfo = _info;
			ocicharMale.yureCtrl = ocicharMale.preparation.YureCtrl;
			ocicharMale.yureCtrl.Init(ocicharMale);
			int group = _info.animeInfo.group;
			int category = _info.animeInfo.category;
			int no = _info.animeInfo.no;
			float animeNormalizedTime = _info.animeNormalizedTime;
			ocicharMale.LoadAnime(0, 0, 1, 0f);
			_male.animBody.Update(0f);
			_info.animeInfo.group = group;
			_info.animeInfo.category = category;
			_info.animeInfo.no = no;
			_info.animeNormalizedTime = animeNormalizedTime;
			IKSolver iksolver = ocicharMale.finalIK.GetIKSolver();
			if (!iksolver.initiated)
			{
				iksolver.Initiate(ocicharMale.finalIK.transform);
			}
			if (_addInfo)
			{
				Studio.AddInfo(_info, ocicharMale);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ocicharMale);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(_info.charFile.parameter.fullname, parent);
			treeNodeObject.enableChangeParent = true;
			treeNodeObject.treeState = _info.treeState;
			TreeNodeObject treeNodeObject2 = treeNodeObject;
			treeNodeObject2.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject2.onVisible, new TreeNodeObject.OnVisibleFunc(ocicharMale.OnVisible));
			treeNodeObject.enableVisible = true;
			treeNodeObject.visible = _info.visible;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ocicharMale.treeNodeObject = treeNodeObject;
			AddObjectAssist.InitBone(ocicharMale, _male.objBodyBone.transform, Singleton<Info>.Instance.dicBoneInfo);
			AddObjectAssist.InitIKTarget(ocicharMale, _addInfo);
			AddObjectAssist.InitLookAt(ocicharMale);
			AddObjectAssist.InitAccessoryPoint(ocicharMale);
			ocicharMale.voiceCtrl.ociChar = ocicharMale;
			_male.fileStatus.neckLookPtn = chaFileStatus.neckLookPtn;
			List<DynamicBone> list = new List<DynamicBone>();
			foreach (GameObject gameObject2 in _male.objHair)
			{
				list.AddRange(gameObject2.GetComponents<DynamicBone>());
			}
			ocicharMale.InitKinematic(_male.gameObject, ocicharMale.finalIK, _male.neckLookCtrl, (from v in list
			where v != null
			select v).ToArray<DynamicBone>(), null);
			treeNodeObject.enableAddChild = false;
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			_info.changeAmount.OnChange();
			treeNodeObject.treeState = TreeNodeObject.TreeState.Close;
			Studio.AddCtrlInfo(ocicharMale);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ocicharMale);
			}
			ocicharMale.LoadAnime(_info.animeInfo.group, _info.animeInfo.category, _info.animeInfo.no, _info.animeNormalizedTime);
			ocicharMale.ActiveKinematicMode(OICharInfo.KinematicMode.IK, _info.enableIK, true);
			for (int k = 0; k < 5; k++)
			{
				ocicharMale.ActiveIK((OIBoneInfo.BoneGroup)(1 << k), _info.activeIK[k], false);
			}
			foreach (var <>__AnonType in FKCtrl.parts.Select((OIBoneInfo.BoneGroup p, int i) => new
			{
				p,
				i
			}))
			{
				ocicharMale.ActiveFK(<>__AnonType.p, ocicharMale.oiCharInfo.activeFK[<>__AnonType.i], ocicharMale.oiCharInfo.activeFK[<>__AnonType.i]);
			}
			ocicharMale.ActiveKinematicMode(OICharInfo.KinematicMode.FK, _info.enableFK, true);
			for (int l = 0; l < _info.expression.Length; l++)
			{
				ocicharMale.charInfo.EnableExpressionCategory(l, _info.expression[l]);
			}
			ocicharMale.animeSpeed = ocicharMale.animeSpeed;
			ocicharMale.animeOptionParam1 = ocicharMale.animeOptionParam1;
			ocicharMale.animeOptionParam2 = ocicharMale.animeOptionParam2;
			chaFileStatus.visibleSonAlways = _info.visibleSon;
			ocicharMale.SetSonLength(_info.sonLength);
			ocicharMale.SetVisibleSimple(_info.visibleSimple);
			ocicharMale.SetSimpleColor(_info.simpleColor);
			AddObjectAssist.UpdateState(ocicharMale, chaFileStatus);
			return ocicharMale;
		}
	}
}
