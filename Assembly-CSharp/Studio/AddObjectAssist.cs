using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AIChara;
using Correct.Process;
using IllusionUtility.GetUtility;
using Manager;
using RootMotion;
using RootMotion.FinalIK;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011EA RID: 4586
	public static class AddObjectAssist
	{
		// Token: 0x060096AD RID: 38573 RVA: 0x003E2C78 File Offset: 0x003E1078
		public static void InitBone(OCIChar _ociChar, Transform _transformRoot, Dictionary<int, Info.BoneInfo> _dicBoneInfo)
		{
			Dictionary<int, OCIChar.BoneInfo> dictionary = new Dictionary<int, OCIChar.BoneInfo>();
			foreach (KeyValuePair<int, Info.BoneInfo> keyValuePair in _dicBoneInfo)
			{
				if (_ociChar.sex != 1 || keyValuePair.Value.level != 2)
				{
					GameObject gameObject = null;
					switch (keyValuePair.Value.group)
					{
					case 7:
					case 8:
					case 9:
					{
						GameObject referenceInfo = _ociChar.charReference.GetReferenceInfo(ChaReference.RefObjKey.HeadParent);
						gameObject = referenceInfo.transform.FindLoop(keyValuePair.Value.bone);
						break;
					}
					default:
						gameObject = _transformRoot.FindLoop(keyValuePair.Value.bone);
						if (gameObject == null)
						{
						}
						break;
					}
					if (!(gameObject == null))
					{
						OCIChar.BoneInfo boneInfo = null;
						if (!dictionary.TryGetValue(keyValuePair.Value.sync, out boneInfo))
						{
							OIBoneInfo oiboneInfo = null;
							if (!_ociChar.oiCharInfo.bones.TryGetValue(keyValuePair.Key, out oiboneInfo))
							{
								oiboneInfo = new OIBoneInfo(Studio.GetNewIndex());
								_ociChar.oiCharInfo.bones.Add(keyValuePair.Key, oiboneInfo);
							}
							switch (keyValuePair.Value.group)
							{
							case 0:
							case 1:
							case 2:
							case 3:
							case 4:
								oiboneInfo.group = (OIBoneInfo.BoneGroup)(1 << keyValuePair.Value.group | 1);
								break;
							case 5:
							case 6:
								goto IL_1EC;
							case 7:
							case 8:
							case 9:
								oiboneInfo.group = OIBoneInfo.BoneGroup.Hair;
								break;
							case 10:
								oiboneInfo.group = OIBoneInfo.BoneGroup.Neck;
								break;
							case 11:
							case 12:
								oiboneInfo.group = OIBoneInfo.BoneGroup.Breast;
								break;
							case 13:
								oiboneInfo.group = OIBoneInfo.BoneGroup.Skirt;
								break;
							default:
								goto IL_1EC;
							}
							IL_209:
							oiboneInfo.level = keyValuePair.Value.level;
							GuideObject guideObject = AddObjectAssist.AddBoneGuide(gameObject.transform, oiboneInfo.dicKey, _ociChar.guideObject, keyValuePair.Value.name);
							OIBoneInfo.BoneGroup group = oiboneInfo.group;
							if (group == OIBoneInfo.BoneGroup.LeftHand || group == OIBoneInfo.BoneGroup.RightHand)
							{
								guideObject.scaleSelect = 0.025f;
							}
							OCIChar.BoneInfo boneInfo2 = new OCIChar.BoneInfo(guideObject, oiboneInfo, keyValuePair.Key);
							_ociChar.listBones.Add(boneInfo2);
							guideObject.SetActive(false, true);
							if (keyValuePair.Value.no == 65)
							{
								_ociChar.transSon = gameObject.transform;
							}
							if (keyValuePair.Value.sync != -1)
							{
								dictionary.Add(keyValuePair.Key, boneInfo2);
								continue;
							}
							continue;
							IL_1EC:
							oiboneInfo.group = (OIBoneInfo.BoneGroup)(1 << keyValuePair.Value.group);
							goto IL_209;
						}
						boneInfo.AddSyncBone(gameObject);
					}
				}
			}
			_ociChar.UpdateFKColor(FKCtrl.parts);
		}

		// Token: 0x060096AE RID: 38574 RVA: 0x003E2FA8 File Offset: 0x003E13A8
		private static void TransformLoop(Transform _src, List<Transform> _list)
		{
			if (_src == null)
			{
				return;
			}
			_list.Add(_src);
			for (int i = 0; i < _src.childCount; i++)
			{
				AddObjectAssist.TransformLoop(_src.GetChild(i), _list);
			}
		}

		// Token: 0x060096AF RID: 38575 RVA: 0x003E2FF0 File Offset: 0x003E13F0
		public static void ArrangeNames(Transform _target)
		{
			IEnumerator enumerator = _target.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform target = (Transform)obj;
					AddObjectAssist.ArrangeNamesLoop(target);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060096B0 RID: 38576 RVA: 0x003E3050 File Offset: 0x003E1450
		private static void ArrangeNamesLoop(Transform _target)
		{
			string name = _target.name;
			if (Regex.Match(name, "c_J_hairF[CLRU]+[a-b]_(\\d*)", RegexOptions.IgnoreCase).Success)
			{
				_target.name = name.Replace("c_J_hairF", "c_J_hair_F");
			}
			else if (Regex.Match(name, "c_J_hairB[CLRU]+[a-b]_(\\d*)", RegexOptions.IgnoreCase).Success)
			{
				_target.name = name.Replace("c_J_hairB", "c_J_hair_B");
			}
			if (_target.childCount == 0)
			{
				return;
			}
			IEnumerator enumerator = _target.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform target = (Transform)obj;
					AddObjectAssist.ArrangeNamesLoop(target);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060096B1 RID: 38577 RVA: 0x003E3120 File Offset: 0x003E1520
		public static void InitHairBone(OCIChar _ociChar, Dictionary<int, Info.BoneInfo> _dicBoneInfo)
		{
			GameObject referenceInfo = _ociChar.charReference.GetReferenceInfo(ChaReference.RefObjKey.HeadParent);
			Dictionary<int, OCIChar.BoneInfo> dictionary = new Dictionary<int, OCIChar.BoneInfo>();
			foreach (KeyValuePair<int, Info.BoneInfo> keyValuePair in from b in _dicBoneInfo
			where MathfEx.RangeEqualOn<int>(7, b.Value.@group, 9)
			select b)
			{
				GameObject gameObject = referenceInfo.transform.FindLoop(keyValuePair.Value.bone);
				if (!(gameObject == null))
				{
					OCIChar.BoneInfo boneInfo = null;
					if (dictionary.TryGetValue(keyValuePair.Value.sync, out boneInfo))
					{
						boneInfo.AddSyncBone(gameObject);
					}
					else
					{
						OIBoneInfo oiboneInfo = null;
						if (!_ociChar.oiCharInfo.bones.TryGetValue(keyValuePair.Key, out oiboneInfo))
						{
							oiboneInfo = new OIBoneInfo(Studio.GetNewIndex());
							_ociChar.oiCharInfo.bones.Add(keyValuePair.Key, oiboneInfo);
						}
						oiboneInfo.group = OIBoneInfo.BoneGroup.Hair;
						oiboneInfo.level = keyValuePair.Value.level;
						GuideObject guideObject = AddObjectAssist.AddBoneGuide(gameObject.transform, oiboneInfo.dicKey, _ociChar.guideObject, keyValuePair.Value.name);
						OCIChar.BoneInfo boneInfo2 = new OCIChar.BoneInfo(guideObject, oiboneInfo, keyValuePair.Key);
						_ociChar.listBones.Add(boneInfo2);
						guideObject.SetActive(false, true);
						if (keyValuePair.Value.sync != -1)
						{
							dictionary.Add(keyValuePair.Key, boneInfo2);
						}
					}
				}
			}
			_ociChar.UpdateFKColor(new OIBoneInfo.BoneGroup[]
			{
				OIBoneInfo.BoneGroup.Hair
			});
		}

		// Token: 0x060096B2 RID: 38578 RVA: 0x003E32EC File Offset: 0x003E16EC
		private static GuideObject AddBoneGuide(Transform _target, int _dicKey, GuideObject _parent, string _name)
		{
			GuideObject guideObject = global::Singleton<GuideObjectManager>.Instance.Add(_target, _dicKey);
			guideObject.enablePos = false;
			guideObject.enableScale = false;
			guideObject.enableMaluti = false;
			guideObject.calcScale = false;
			guideObject.scaleRate = 0.5f;
			guideObject.scaleRot = 0.025f;
			guideObject.scaleSelect = 0.05f;
			guideObject.parentGuide = _parent;
			return guideObject;
		}

		// Token: 0x060096B3 RID: 38579 RVA: 0x003E334C File Offset: 0x003E174C
		public static void InitIKTarget(OCIChar _ociChar, bool _addInfo)
		{
			IKSolverFullBodyBiped solver = _ociChar.finalIK.solver;
			BipedReferences references = _ociChar.finalIK.references;
			_ociChar.ikCtrl = _ociChar.preparation.IKCtrl;
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 0, solver.bodyEffector, false, references.pelvis);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 1, solver.leftShoulderEffector, false, references.leftUpperArm);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 2, solver.leftArmChain, false, references.leftForearm);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 3, solver.leftHandEffector, true, references.leftHand);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 4, solver.rightShoulderEffector, false, references.rightUpperArm);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 5, solver.rightArmChain, false, references.rightForearm);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 6, solver.rightHandEffector, true, references.rightHand);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 7, solver.leftThighEffector, false, references.leftThigh);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 8, solver.leftLegChain, false, references.leftCalf);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 9, solver.leftFootEffector, true, references.leftFoot);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 10, solver.rightThighEffector, false, references.rightThigh);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 11, solver.rightLegChain, false, references.rightCalf);
			AddObjectAssist.AddIKTarget(_ociChar, _ociChar.ikCtrl, 12, solver.rightFootEffector, true, references.rightFoot);
			if (_addInfo)
			{
				_ociChar.ikCtrl.InitTarget();
			}
		}

		// Token: 0x060096B4 RID: 38580 RVA: 0x003E34F8 File Offset: 0x003E18F8
		public static void InitAccessoryPoint(OCIChar _ociChar)
		{
			Dictionary<int, Tuple<int, int>> dictionary = new Dictionary<int, Tuple<int, int>>();
			ExcelData accessoryPointGroup = global::Singleton<Info>.Instance.accessoryPointGroup;
			int count = accessoryPointGroup.list.Count;
			Dictionary<int, TreeNodeObject> dictionary2 = new Dictionary<int, TreeNodeObject>();
			for (int i = 1; i < count; i++)
			{
				ExcelData.Param param = accessoryPointGroup.list[i];
				int key = int.Parse(param.list[0]);
				string arg = param.list[1];
				string[] array = param.list[2].Split(new char[]
				{
					'-'
				});
				dictionary.Add(key, new Tuple<int, int>(int.Parse(array[0]), int.Parse(array[1])));
				TreeNodeObject treeNodeObject = Studio.AddNode(string.Format("グループ : {0}", arg), _ociChar.treeNodeObject);
				treeNodeObject.treeState = ((!_ociChar.oiCharInfo.dicAccessGroup.ContainsKey(key)) ? TreeNodeObject.TreeState.Close : _ociChar.oiCharInfo.dicAccessGroup[key]);
				treeNodeObject.enableChangeParent = false;
				treeNodeObject.enableDelete = false;
				treeNodeObject.enableCopy = false;
				dictionary2.Add(key, treeNodeObject);
				_ociChar.dicAccessPoint.Add(key, new OCIChar.AccessPointInfo(treeNodeObject));
			}
			foreach (KeyValuePair<int, Tuple<int, int>> keyValuePair in dictionary)
			{
				for (int j = keyValuePair.Value.Item1; j <= keyValuePair.Value.Item2; j++)
				{
					TreeNodeObject parent = dictionary2[keyValuePair.Key];
					TreeNodeObject treeNodeObject2 = Studio.AddNode(string.Format("部位 : {0}", ChaAccessoryDefine.AccessoryParentName.SafeGet(j)), parent);
					treeNodeObject2.treeState = ((!_ociChar.oiCharInfo.dicAccessNo.ContainsKey(j)) ? TreeNodeObject.TreeState.Close : _ociChar.oiCharInfo.dicAccessNo[j]);
					treeNodeObject2.enableChangeParent = false;
					treeNodeObject2.enableDelete = false;
					treeNodeObject2.enableCopy = false;
					treeNodeObject2.baseColor = Utility.ConvertColor(204, 128, 164);
					treeNodeObject2.colorSelect = treeNodeObject2.baseColor;
					_ociChar.dicAccessoryPoint.Add(treeNodeObject2, j);
					OCIChar.AccessPointInfo accessPointInfo = null;
					if (_ociChar.dicAccessPoint.TryGetValue(keyValuePair.Key, out accessPointInfo))
					{
						accessPointInfo.child.Add(j, treeNodeObject2);
					}
				}
			}
			foreach (KeyValuePair<int, TreeNodeObject> keyValuePair2 in dictionary2)
			{
				keyValuePair2.Value.enableAddChild = false;
			}
			global::Singleton<Studio>.Instance.treeNodeCtrl.RefreshHierachy();
		}

		// Token: 0x060096B5 RID: 38581 RVA: 0x003E37FC File Offset: 0x003E1BFC
		public static void LoadChild(List<ObjectInfo> _child, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			foreach (ObjectInfo child in _child)
			{
				AddObjectAssist.LoadChild(child, _parent, _parentNode);
			}
		}

		// Token: 0x060096B6 RID: 38582 RVA: 0x003E3854 File Offset: 0x003E1C54
		public static void LoadChild(Dictionary<int, ObjectInfo> _child, ObjectCtrlInfo _parent = null, TreeNodeObject _parentNode = null)
		{
			foreach (KeyValuePair<int, ObjectInfo> keyValuePair in _child)
			{
				AddObjectAssist.LoadChild(keyValuePair.Value, _parent, _parentNode);
			}
		}

		// Token: 0x060096B7 RID: 38583 RVA: 0x003E38B4 File Offset: 0x003E1CB4
		public static void LoadChild(ObjectInfo _child, ObjectCtrlInfo _parent = null, TreeNodeObject _parentNode = null)
		{
			switch (_child.kind)
			{
			case 0:
			{
				OICharInfo oicharInfo = _child as OICharInfo;
				if (oicharInfo.sex == 1)
				{
					AddObjectFemale.Load(oicharInfo, _parent, _parentNode);
				}
				else
				{
					AddObjectMale.Load(oicharInfo, _parent, _parentNode);
				}
				break;
			}
			case 1:
				AddObjectItem.Load(_child as OIItemInfo, _parent, _parentNode);
				break;
			case 2:
				AddObjectLight.Load(_child as OILightInfo, _parent, _parentNode);
				break;
			case 3:
				AddObjectFolder.Load(_child as OIFolderInfo, _parent, _parentNode);
				break;
			case 4:
				AddObjectRoute.Load(_child as OIRouteInfo, _parent, _parentNode);
				break;
			case 5:
				AddObjectCamera.Load(_child as OICameraInfo, _parent, _parentNode);
				break;
			}
		}

		// Token: 0x060096B8 RID: 38584 RVA: 0x003E397C File Offset: 0x003E1D7C
		public static void InitLookAt(OCIChar _ociChar)
		{
			bool flag = _ociChar.oiCharInfo.lookAtTarget == null;
			if (flag)
			{
				_ociChar.oiCharInfo.lookAtTarget = new LookAtTargetInfo(Studio.GetNewIndex());
			}
			Transform lookAtTarget = _ociChar.preparation.LookAtTarget;
			if (flag)
			{
				_ociChar.oiCharInfo.lookAtTarget.changeAmount.pos = lookAtTarget.localPosition;
			}
			GuideObject guideObject = global::Singleton<GuideObjectManager>.Instance.Add(lookAtTarget, _ociChar.oiCharInfo.lookAtTarget.dicKey);
			guideObject.enableRot = false;
			guideObject.enableScale = false;
			guideObject.enableMaluti = false;
			guideObject.scaleRate = 0.5f;
			guideObject.scaleSelect = 0.25f;
			guideObject.parentGuide = _ociChar.guideObject;
			guideObject.changeAmount.OnChange();
			guideObject.mode = GuideObject.Mode.World;
			guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_ociChar.lookAtInfo = new OCIChar.LookAtInfo(guideObject, _ociChar.oiCharInfo.lookAtTarget);
			_ociChar.lookAtInfo.active = false;
		}

		// Token: 0x060096B9 RID: 38585 RVA: 0x003E3A70 File Offset: 0x003E1E70
		public static void SetupAccessoryDynamicBones(OCIChar _ociChar)
		{
			ChaControl charInfo = _ociChar.charInfo;
			CmpAccessory[] cmpAccessory = charInfo.cmpAccessory;
			if (cmpAccessory.IsNullOrEmpty<CmpAccessory>())
			{
				return;
			}
			ChaFileAccessory.PartsInfo[] parts = charInfo.nowCoordinate.accessory.parts;
			for (int i = 0; i < cmpAccessory.Length; i++)
			{
				if (!(cmpAccessory[i] == null))
				{
					cmpAccessory[i].EnableDynamicBones(!parts[i].noShake);
				}
			}
		}

		// Token: 0x060096BA RID: 38586 RVA: 0x003E3AE4 File Offset: 0x003E1EE4
		public static void DisableComponent(OCIChar _ociChar)
		{
			GameObject objAnim = _ociChar.charInfo.objAnim;
			BaseProcess[] componentsInChildren = objAnim.GetComponentsInChildren<BaseProcess>(true);
			if (!componentsInChildren.IsNullOrEmpty<BaseProcess>())
			{
				foreach (BaseProcess baseProcess in componentsInChildren)
				{
					baseProcess.enabled = false;
				}
			}
		}

		// Token: 0x060096BB RID: 38587 RVA: 0x003E3B38 File Offset: 0x003E1F38
		public static void UpdateState(OCIChar _ociChar, ChaFileStatus _status)
		{
			ChaFileStatus charFileStatus = _ociChar.charFileStatus;
			charFileStatus.Copy(_status);
			for (int i = 0; i < charFileStatus.clothesState.Length; i++)
			{
				_ociChar.SetClothesState(i, charFileStatus.clothesState[i]);
			}
			for (int j = 0; j < charFileStatus.showAccessory.Length; j++)
			{
				_ociChar.ShowAccessory(j, charFileStatus.showAccessory[j]);
			}
			Dictionary<int, ListInfoBase> categoryInfo = global::Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo((_ociChar.sex != 0) ? ChaListDefine.CategoryNo.custom_eyebrow_f : ChaListDefine.CategoryNo.custom_eyebrow_m);
			int[] source = categoryInfo.Keys.ToArray<int>();
			_ociChar.charInfo.ChangeEyebrowPtn((!source.Contains(charFileStatus.eyebrowPtn)) ? 0 : charFileStatus.eyebrowPtn, true);
			Dictionary<int, ListInfoBase> categoryInfo2 = global::Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo((_ociChar.sex != 0) ? ChaListDefine.CategoryNo.custom_eye_f : ChaListDefine.CategoryNo.custom_eye_m);
			int[] source2 = categoryInfo2.Keys.ToArray<int>();
			_ociChar.charInfo.ChangeEyesPtn((!source2.Contains(charFileStatus.eyesPtn)) ? 0 : charFileStatus.eyesPtn, true);
			_ociChar.ChangeBlink(charFileStatus.eyesBlink);
			_ociChar.ChangeEyesOpen(charFileStatus.eyesOpenMax);
			_ociChar.charInfo.ChangeMouthPtn(charFileStatus.mouthPtn, true);
			_ociChar.ChangeMouthOpen(_ociChar.oiCharInfo.mouthOpen);
			_ociChar.ChangeHandAnime(0, _ociChar.oiCharInfo.handPtn[0]);
			_ociChar.ChangeHandAnime(1, _ociChar.oiCharInfo.handPtn[1]);
			_ociChar.ChangeLookEyesPtn(charFileStatus.eyesLookPtn, true);
			if (_ociChar.oiCharInfo.eyesByteData != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(_ociChar.oiCharInfo.eyesByteData))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						_ociChar.charInfo.eyeLookCtrl.eyeLookScript.LoadAngle(binaryReader);
					}
				}
				_ociChar.oiCharInfo.eyesByteData = null;
			}
			if (_ociChar.oiCharInfo.neckByteData != null)
			{
				using (MemoryStream memoryStream2 = new MemoryStream(_ociChar.oiCharInfo.neckByteData))
				{
					using (BinaryReader binaryReader2 = new BinaryReader(memoryStream2))
					{
						_ociChar.neckLookCtrl.LoadNeckLookCtrl(binaryReader2);
					}
				}
				_ociChar.oiCharInfo.neckByteData = null;
			}
			_ociChar.ChangeLookNeckPtn(charFileStatus.neckLookPtn);
			for (int k = 0; k < 5; k++)
			{
				_ociChar.SetSiruFlags((ChaFileDefine.SiruParts)k, _ociChar.oiCharInfo.siru[k]);
			}
			if (_ociChar.sex == 1)
			{
				_ociChar.charInfo.ChangeHohoAkaRate(charFileStatus.hohoAkaRate);
			}
			_ociChar.SetVisibleSon(charFileStatus.visibleSonAlways);
			_ociChar.SetTears(_ociChar.GetTears());
			_ociChar.SetTuyaRate(_ociChar.charInfo.skinGlossRate);
			_ociChar.SetWetRate(_ociChar.charInfo.wetRate);
		}

		// Token: 0x060096BC RID: 38588 RVA: 0x003E3E7C File Offset: 0x003E227C
		private static OCIChar.IKInfo AddIKTarget(OCIChar _ociChar, IKCtrl _ikCtrl, int _no, IKEffector _effector, bool _usedRot, Transform _bone)
		{
			OCIChar.IKInfo ikinfo = AddObjectAssist.AddIKTarget(_ociChar, _ikCtrl, _no, _effector.target, _usedRot, _bone, true);
			_effector.positionWeight = 1f;
			_effector.rotationWeight = ((!_usedRot) ? 0f : 1f);
			_effector.target = ikinfo.targetObject;
			return ikinfo;
		}

		// Token: 0x060096BD RID: 38589 RVA: 0x003E3ED4 File Offset: 0x003E22D4
		private static OCIChar.IKInfo AddIKTarget(OCIChar _ociChar, IKCtrl _ikCtrl, int _no, FBIKChain _chain, bool _usedRot, Transform _bone)
		{
			OCIChar.IKInfo ikinfo = AddObjectAssist.AddIKTarget(_ociChar, _ikCtrl, _no, _chain.bendConstraint.bendGoal, _usedRot, _bone, false);
			_chain.bendConstraint.weight = 1f;
			_chain.bendConstraint.bendGoal = ikinfo.targetObject;
			return ikinfo;
		}

		// Token: 0x060096BE RID: 38590 RVA: 0x003E3F1C File Offset: 0x003E231C
		private static OCIChar.IKInfo AddIKTarget(OCIChar _ociChar, IKCtrl _ikCtrl, int _no, Transform _target, bool _usedRot, Transform _bone, bool _isRed)
		{
			OIIKTargetInfo oiiktargetInfo = null;
			bool flag = !_ociChar.oiCharInfo.ikTarget.TryGetValue(_no, out oiiktargetInfo);
			if (flag)
			{
				oiiktargetInfo = new OIIKTargetInfo(Studio.GetNewIndex());
				_ociChar.oiCharInfo.ikTarget.Add(_no, oiiktargetInfo);
			}
			switch (_no)
			{
			case 0:
				oiiktargetInfo.group = OIBoneInfo.BoneGroup.Body;
				break;
			case 1:
			case 2:
			case 3:
				oiiktargetInfo.group = OIBoneInfo.BoneGroup.LeftArm;
				break;
			case 4:
			case 5:
			case 6:
				oiiktargetInfo.group = OIBoneInfo.BoneGroup.RightArm;
				break;
			case 7:
			case 8:
			case 9:
				oiiktargetInfo.group = OIBoneInfo.BoneGroup.LeftLeg;
				break;
			case 10:
			case 11:
			case 12:
				oiiktargetInfo.group = OIBoneInfo.BoneGroup.RightLeg;
				break;
			}
			GameObject gameObject = new GameObject(_target.name + "(work)");
			gameObject.transform.SetParent(_ociChar.charInfo.transform);
			GuideObject guideObject = global::Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, oiiktargetInfo.dicKey);
			guideObject.mode = GuideObject.Mode.LocalIK;
			guideObject.enableRot = _usedRot;
			guideObject.enableScale = false;
			guideObject.enableMaluti = false;
			guideObject.calcScale = false;
			guideObject.scaleRate = 0.5f;
			guideObject.scaleRot = 0.05f;
			guideObject.scaleSelect = 0.1f;
			guideObject.parentGuide = _ociChar.guideObject;
			guideObject.guideSelect.color = ((!_isRed) ? Color.blue : Color.red);
			guideObject.moveCalc = GuideMove.MoveCalc.TYPE3;
			OCIChar.IKInfo ikinfo = new OCIChar.IKInfo(guideObject, oiiktargetInfo, _target, gameObject.transform, _bone);
			if (!flag)
			{
				oiiktargetInfo.changeAmount.OnChange();
			}
			_ikCtrl.addIKInfo = ikinfo;
			_ociChar.listIKTarget.Add(ikinfo);
			guideObject.SetActive(false, true);
			return ikinfo;
		}
	}
}
