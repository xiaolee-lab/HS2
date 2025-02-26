using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Manager;
using RootMotion.FinalIK;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011EC RID: 4588
	public static class AddObjectFemale
	{
		// Token: 0x060096C4 RID: 38596 RVA: 0x003E442C File Offset: 0x003E282C
		public static OCICharFemale Add(string _path)
		{
			ChaControl chaControl = Singleton<Character>.Instance.CreateChara(1, Singleton<Scene>.Instance.commonSpace, -1, null);
			chaControl.chaFile.LoadCharaFile(_path, byte.MaxValue, true, true);
			chaControl.fileStatus.neckLookPtn = 3;
			OICharInfo info = new OICharInfo(chaControl.chaFile, Studio.GetNewIndex());
			return AddObjectFemale.Add(chaControl, info, null, null, true, Studio.optionSystem.initialPosition);
		}

		// Token: 0x060096C5 RID: 38597 RVA: 0x003E4498 File Offset: 0x003E2898
		public static OCICharFemale Load(OICharInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			ChaControl female = Singleton<Character>.Instance.CreateChara(1, Singleton<Scene>.Instance.commonSpace, -1, _info.charFile);
			OCICharFemale ocicharFemale = AddObjectFemale.Add(female, _info, _parent, _parentNode, false, -1);
			using (Dictionary<int, List<ObjectInfo>>.Enumerator enumerator = _info.child.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, List<ObjectInfo>> v = enumerator.Current;
					AddObjectAssist.LoadChild(v.Value, ocicharFemale, ocicharFemale.dicAccessoryPoint.First((KeyValuePair<TreeNodeObject, int> x) => x.Value == v.Key).Key);
				}
			}
			return ocicharFemale;
		}

		// Token: 0x060096C6 RID: 38598 RVA: 0x003E4554 File Offset: 0x003E2954
		private static OCICharFemale Add(ChaControl _female, OICharInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCICharFemale ocicharFemale = new OCICharFemale();
			ChaFileStatus chaFileStatus = new ChaFileStatus();
			chaFileStatus.Copy(_female.fileStatus);
			_female.ChangeNowCoordinate(false, true);
			_female.Load(true);
			_female.InitializeExpression(1, true);
			ocicharFemale.charInfo = _female;
			ocicharFemale.charReference = _female;
			ocicharFemale.preparation = _female.objAnim.GetComponent<Preparation>();
			ocicharFemale.finalIK = _female.fullBodyIK;
			ocicharFemale.charInfo.hideMoz = false;
			for (int m = 0; m < 2; m++)
			{
				GameObject gameObject = _female.objHair.SafeGet(m);
				if (gameObject != null)
				{
					AddObjectAssist.ArrangeNames(gameObject.transform);
				}
			}
			AddObjectAssist.SetupAccessoryDynamicBones(ocicharFemale);
			AddObjectAssist.DisableComponent(ocicharFemale);
			ocicharFemale.objectInfo = _info;
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(_female.transform, _info.dicKey);
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			GuideObject guideObject2 = guideObject;
			guideObject2.isActiveFunc = (GuideObject.IsActiveFunc)Delegate.Combine(guideObject2.isActiveFunc, new GuideObject.IsActiveFunc(ocicharFemale.OnSelect));
			guideObject.SetVisibleCenter(true);
			ocicharFemale.guideObject = guideObject;
			ocicharFemale.optionItemCtrl = _female.gameObject.AddComponent<OptionItemCtrl>();
			ocicharFemale.optionItemCtrl.animator = _female.animBody;
			ocicharFemale.optionItemCtrl.oiCharInfo = _info;
			ChangeAmount changeAmount = _info.changeAmount;
			changeAmount.onChangeScale = (Action<Vector3>)Delegate.Combine(changeAmount.onChangeScale, new Action<Vector3>(ocicharFemale.optionItemCtrl.ChangeScale));
			OCIChar ocichar = ocicharFemale;
			Preparation preparation = ocicharFemale.preparation;
			ocichar.charAnimeCtrl = ((preparation != null) ? preparation.CharAnimeCtrl : null);
			if (ocicharFemale.charAnimeCtrl)
			{
				ocicharFemale.charAnimeCtrl.oiCharInfo = _info;
			}
			ocicharFemale.yureCtrl = ocicharFemale.preparation.YureCtrl;
			ocicharFemale.yureCtrl.Init(ocicharFemale);
			if (_info.animeInfo.group == 0 && _info.animeInfo.category == 2 && _info.animeInfo.no == 11)
			{
				int group = _info.animeInfo.group;
				int category = _info.animeInfo.category;
				int no = _info.animeInfo.no;
				float animeNormalizedTime = _info.animeNormalizedTime;
				ocicharFemale.LoadAnime(0, 1, 0, 0f);
				_female.animBody.Update(0f);
				_info.animeInfo.group = group;
				_info.animeInfo.category = category;
				_info.animeInfo.no = no;
				_info.animeNormalizedTime = animeNormalizedTime;
			}
			IKSolver iksolver = ocicharFemale.finalIK.GetIKSolver();
			if (!iksolver.initiated)
			{
				iksolver.Initiate(ocicharFemale.finalIK.transform);
			}
			if (_addInfo)
			{
				Studio.AddInfo(_info, ocicharFemale);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ocicharFemale);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(_info.charFile.parameter.fullname, parent);
			treeNodeObject.enableChangeParent = true;
			treeNodeObject.treeState = _info.treeState;
			TreeNodeObject treeNodeObject2 = treeNodeObject;
			treeNodeObject2.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject2.onVisible, new TreeNodeObject.OnVisibleFunc(ocicharFemale.OnVisible));
			treeNodeObject.enableVisible = true;
			treeNodeObject.visible = _info.visible;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ocicharFemale.treeNodeObject = treeNodeObject;
			_info.changeAmount.OnChange();
			AddObjectAssist.InitBone(ocicharFemale, _female.objBodyBone.transform, Singleton<Info>.Instance.dicBoneInfo);
			AddObjectAssist.InitIKTarget(ocicharFemale, _addInfo);
			AddObjectAssist.InitLookAt(ocicharFemale);
			AddObjectAssist.InitAccessoryPoint(ocicharFemale);
			ocicharFemale.voiceCtrl.ociChar = ocicharFemale;
			_female.fileStatus.neckLookPtn = chaFileStatus.neckLookPtn;
			ocicharFemale.InitKinematic(_female.gameObject, ocicharFemale.finalIK, _female.neckLookCtrl, null, AddObjectFemale.GetSkirtDynamic(_female.objClothes));
			treeNodeObject.enableAddChild = false;
			Studio.AddCtrlInfo(ocicharFemale);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ocicharFemale);
			}
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			ocicharFemale.LoadAnime(_info.animeInfo.group, _info.animeInfo.category, _info.animeInfo.no, _info.animeNormalizedTime);
			for (int j = 0; j < 5; j++)
			{
				ocicharFemale.ActiveIK((OIBoneInfo.BoneGroup)(1 << j), _info.activeIK[j], false);
			}
			ocicharFemale.ActiveKinematicMode(OICharInfo.KinematicMode.IK, _info.enableIK, true);
			foreach (var <>__AnonType in FKCtrl.parts.Select((OIBoneInfo.BoneGroup p, int i) => new
			{
				p,
				i
			}))
			{
				ocicharFemale.ActiveFK(<>__AnonType.p, ocicharFemale.oiCharInfo.activeFK[<>__AnonType.i], false);
			}
			ocicharFemale.ActiveKinematicMode(OICharInfo.KinematicMode.FK, _info.enableFK, true);
			for (int k = 0; k < _info.expression.Length; k++)
			{
				ocicharFemale.charInfo.EnableExpressionCategory(k, _info.expression[k]);
			}
			ocicharFemale.animeSpeed = ocicharFemale.animeSpeed;
			ocicharFemale.animeOptionParam1 = ocicharFemale.animeOptionParam1;
			ocicharFemale.animeOptionParam2 = ocicharFemale.animeOptionParam2;
			byte[] siruLv = _female.fileStatus.siruLv;
			for (int l = 0; l < siruLv.Length; l++)
			{
				siruLv[l] = 0;
			}
			chaFileStatus.visibleSonAlways = _info.visibleSon;
			ocicharFemale.SetSonLength(_info.sonLength);
			ocicharFemale.SetVisibleSimple(_info.visibleSimple);
			ocicharFemale.SetSimpleColor(_info.simpleColor);
			AddObjectAssist.UpdateState(ocicharFemale, chaFileStatus);
			return ocicharFemale;
		}

		// Token: 0x060096C7 RID: 38599 RVA: 0x003E4B58 File Offset: 0x003E2F58
		public static DynamicBone[] GetHairDynamic(GameObject[] _objHair)
		{
			if (_objHair.IsNullOrEmpty<GameObject>())
			{
				return null;
			}
			List<DynamicBone> list = new List<DynamicBone>();
			foreach (GameObject gameObject in from o in _objHair
			where o != null
			select o)
			{
				list.AddRange(gameObject.GetComponents<DynamicBone>());
			}
			return (from v in list
			where v != null
			select v).ToArray<DynamicBone>();
		}

		// Token: 0x060096C8 RID: 38600 RVA: 0x003E4C10 File Offset: 0x003E3010
		public static DynamicBone[] GetSkirtDynamic(GameObject[] _objClothes)
		{
			if (_objClothes.IsNullOrEmpty<GameObject>())
			{
				return null;
			}
			string[] target = (from v in Singleton<Info>.Instance.dicBoneInfo
			where v.Value.@group == 13
			select v.Value.bone).ToArray<string>();
			List<DynamicBone> list = new List<DynamicBone>();
			int[] array = new int[]
			{
				0,
				1
			};
			for (int i = 0; i < array.Length; i++)
			{
				DynamicBone[] skirtDynamic = AddObjectFemale.GetSkirtDynamic(_objClothes[array[i]], target);
				if (!skirtDynamic.IsNullOrEmpty<DynamicBone>())
				{
					list.AddRange(skirtDynamic);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060096C9 RID: 38601 RVA: 0x003E4CD0 File Offset: 0x003E30D0
		private static DynamicBone[] GetSkirtDynamic(GameObject _object, string[] _target)
		{
			if (_object == null)
			{
				return null;
			}
			DynamicBone[] componentsInChildren = _object.GetComponentsInChildren<DynamicBone>();
			return (from v in componentsInChildren
			where AddObjectFemale.CheckNameLoop(v.m_Root, _target)
			select v).ToArray<DynamicBone>();
		}

		// Token: 0x060096CA RID: 38602 RVA: 0x003E4D18 File Offset: 0x003E3118
		private static bool CheckNameLoop(Transform _transform, string[] _target)
		{
			if (_transform == null)
			{
				return false;
			}
			if (_target.Contains(_transform.name))
			{
				return true;
			}
			if (_transform.childCount == 0)
			{
				return false;
			}
			for (int i = 0; i < _transform.childCount; i++)
			{
				if (AddObjectFemale.CheckNameLoop(_transform.GetChild(i), _target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060096CB RID: 38603 RVA: 0x003E4D80 File Offset: 0x003E3180
		public static IEnumerator AddCoroutine(AddObjectFemale.NecessaryInfo _necessary)
		{
			ChaControl female = Singleton<Character>.Instance.CreateChara(1, Singleton<Scene>.Instance.commonSpace, -1, null);
			female.chaFile.LoadCharaFile(_necessary.path, byte.MaxValue, true, true);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			_necessary.oICharInfo = new OICharInfo(female.chaFile, Studio.GetNewIndex());
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			_necessary.chaControl = female;
			yield return AddObjectFemale.AddCoroutine(_necessary, null, null, true);
			yield break;
		}

		// Token: 0x060096CC RID: 38604 RVA: 0x003E4D9C File Offset: 0x003E319C
		private static IEnumerator AddCoroutine(AddObjectFemale.NecessaryInfo _necessary, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo)
		{
			OCICharFemale ocicf = new OCICharFemale();
			ChaFileStatus status = new ChaFileStatus();
			status.Copy(_necessary.chaControl.fileStatus);
			yield return _necessary.chaControl.LoadAsync(true, true);
			_necessary.chaControl.SetActiveTop(true);
			_necessary.chaControl.InitializeExpression(1, true);
			ocicf.charInfo = _necessary.chaControl;
			ocicf.charReference = _necessary.chaControl;
			ocicf.preparation = _necessary.chaControl.objAnim.GetComponent<Preparation>();
			ocicf.finalIK = _necessary.chaControl.fullBodyIK;
			for (int m = 0; m < 2; m++)
			{
				GameObject gameObject = ocicf.charInfo.objHair.SafeGet(m);
				if (gameObject != null)
				{
					AddObjectAssist.ArrangeNames(gameObject.transform);
				}
			}
			AddObjectAssist.DisableComponent(ocicf);
			ocicf.objectInfo = _necessary.oICharInfo;
			GuideObject go = Singleton<GuideObjectManager>.Instance.Add(_necessary.chaControl.transform, _necessary.oICharInfo.dicKey);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			go.scaleSelect = 0.1f;
			go.scaleRot = 0.05f;
			GuideObject guideObject = go;
			guideObject.isActiveFunc = (GuideObject.IsActiveFunc)Delegate.Combine(guideObject.isActiveFunc, new GuideObject.IsActiveFunc(ocicf.OnSelect));
			ocicf.guideObject = go;
			OCIChar ocichar = ocicf;
			Preparation preparation = ocicf.preparation;
			ocichar.charAnimeCtrl = ((preparation != null) ? preparation.CharAnimeCtrl : null);
			ocicf.charAnimeCtrl.oiCharInfo = _necessary.oICharInfo;
			if (_necessary.oICharInfo.animeInfo.group == 0 && _necessary.oICharInfo.animeInfo.category == 2 && _necessary.oICharInfo.animeInfo.no == 11)
			{
				int group = _necessary.oICharInfo.animeInfo.group;
				int category = _necessary.oICharInfo.animeInfo.category;
				int no = _necessary.oICharInfo.animeInfo.no;
				float animeNormalizedTime = _necessary.oICharInfo.animeNormalizedTime;
				ocicf.LoadAnime(0, 1, 0, 0f);
				_necessary.chaControl.animBody.Update(0f);
				_necessary.oICharInfo.animeInfo.group = group;
				_necessary.oICharInfo.animeInfo.category = category;
				_necessary.oICharInfo.animeInfo.no = no;
				_necessary.oICharInfo.animeNormalizedTime = animeNormalizedTime;
			}
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			if (_addInfo)
			{
				Studio.AddInfo(_necessary.oICharInfo, ocicf);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ocicf);
			}
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			TreeNodeObject tnoParent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject tno = Studio.AddNode(_necessary.oICharInfo.charFile.parameter.fullname, tnoParent);
			tno.enableChangeParent = true;
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			tno.treeState = _necessary.oICharInfo.treeState;
			TreeNodeObject treeNodeObject = tno;
			treeNodeObject.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject.onVisible, new TreeNodeObject.OnVisibleFunc(ocicf.OnVisible));
			tno.enableVisible = true;
			tno.visible = _necessary.oICharInfo.visible;
			go.guideSelect.treeNodeObject = tno;
			ocicf.treeNodeObject = tno;
			_necessary.oICharInfo.changeAmount.OnChange();
			AddObjectAssist.InitBone(ocicf, _necessary.chaControl.objBodyBone.transform, Singleton<Info>.Instance.dicBoneInfo);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			AddObjectAssist.InitIKTarget(ocicf, _addInfo);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			AddObjectAssist.InitLookAt(ocicf);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			AddObjectAssist.InitAccessoryPoint(ocicf);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			ocicf.voiceCtrl.ociChar = ocicf;
			ocicf.InitKinematic(_necessary.chaControl.gameObject, ocicf.finalIK, _necessary.chaControl.neckLookCtrl, null, AddObjectFemale.GetSkirtDynamic(_necessary.chaControl.objClothes));
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			tno.enableAddChild = false;
			Studio.AddCtrlInfo(ocicf);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ocicf);
			}
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			int initialPosition = Studio.optionSystem.initialPosition;
			if (initialPosition == 1)
			{
				_necessary.oICharInfo.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			ocicf.LoadAnime(_necessary.oICharInfo.animeInfo.group, _necessary.oICharInfo.animeInfo.category, _necessary.oICharInfo.animeInfo.no, _necessary.oICharInfo.animeNormalizedTime);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			for (int j = 0; j < 5; j++)
			{
				ocicf.ActiveIK((OIBoneInfo.BoneGroup)(1 << j), _necessary.oICharInfo.activeIK[j], false);
			}
			ocicf.ActiveKinematicMode(OICharInfo.KinematicMode.IK, _necessary.oICharInfo.enableIK, true);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			foreach (var <>__AnonType in FKCtrl.parts.Select((OIBoneInfo.BoneGroup p, int i) => new
			{
				p,
				i
			}))
			{
				ocicf.ActiveFK(<>__AnonType.p, ocicf.oiCharInfo.activeFK[<>__AnonType.i], ocicf.oiCharInfo.activeFK[<>__AnonType.i]);
			}
			ocicf.ActiveKinematicMode(OICharInfo.KinematicMode.FK, _necessary.oICharInfo.enableFK, true);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			for (int k = 0; k < _necessary.oICharInfo.expression.Length; k++)
			{
				ocicf.charInfo.EnableExpressionCategory(k, _necessary.oICharInfo.expression[k]);
			}
			ocicf.animeSpeed = ocicf.animeSpeed;
			byte[] siru = _necessary.chaControl.fileStatus.siruLv;
			for (int l = 0; l < siru.Length; l++)
			{
				siru[l] = 0;
			}
			status.visibleSonAlways = _necessary.oICharInfo.visibleSon;
			AddObjectAssist.UpdateState(ocicf, status);
			if (_necessary.isOver)
			{
				yield return null;
				_necessary.Next();
			}
			_necessary.ocicf = ocicf;
			yield break;
		}

		// Token: 0x020011ED RID: 4589
		public class NecessaryInfo
		{
			// Token: 0x060096D2 RID: 38610 RVA: 0x003E4E07 File Offset: 0x003E3207
			public NecessaryInfo(string _path)
			{
				this.path = _path;
				this.waitTime = new Info.WaitTime();
			}

			// Token: 0x17001FF7 RID: 8183
			// (get) Token: 0x060096D3 RID: 38611 RVA: 0x003E4E28 File Offset: 0x003E3228
			// (set) Token: 0x060096D4 RID: 38612 RVA: 0x003E4E30 File Offset: 0x003E3230
			public string path { get; private set; }

			// Token: 0x17001FF8 RID: 8184
			// (get) Token: 0x060096D5 RID: 38613 RVA: 0x003E4E39 File Offset: 0x003E3239
			// (set) Token: 0x060096D6 RID: 38614 RVA: 0x003E4E41 File Offset: 0x003E3241
			public Info.WaitTime waitTime { get; private set; }

			// Token: 0x17001FF9 RID: 8185
			// (get) Token: 0x060096D7 RID: 38615 RVA: 0x003E4E4A File Offset: 0x003E324A
			public bool isOver
			{
				get
				{
					return this.waitTime.isOver;
				}
			}

			// Token: 0x060096D8 RID: 38616 RVA: 0x003E4E57 File Offset: 0x003E3257
			public void Next()
			{
				this.waitTime.Next();
			}

			// Token: 0x04007925 RID: 31013
			public OCICharFemale ocicf;

			// Token: 0x04007926 RID: 31014
			public ChaControl chaControl;

			// Token: 0x04007927 RID: 31015
			public OICharInfo oICharInfo;

			// Token: 0x04007928 RID: 31016
			public ObjectCtrlInfo parent;

			// Token: 0x04007929 RID: 31017
			public TreeNodeObject parentNode;

			// Token: 0x0400792A RID: 31018
			public bool addInfo = true;
		}
	}
}
