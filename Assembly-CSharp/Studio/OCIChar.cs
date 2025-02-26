using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AIChara;
using Illusion;
using Illusion.Extensions;
using Manager;
using RootMotion.FinalIK;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200120E RID: 4622
	public class OCIChar : ObjectCtrlInfo
	{
		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x0600975B RID: 38747 RVA: 0x003E8E8C File Offset: 0x003E728C
		public OICharInfo oiCharInfo
		{
			get
			{
				return this.objectInfo as OICharInfo;
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x0600975C RID: 38748 RVA: 0x003E8E99 File Offset: 0x003E7299
		// (set) Token: 0x0600975D RID: 38749 RVA: 0x003E8EBC File Offset: 0x003E72BC
		public Transform transSon
		{
			get
			{
				return (!this.charAnimeCtrl) ? null : this.charAnimeCtrl.transSon;
			}
			set
			{
				if (this.charAnimeCtrl)
				{
					this.charAnimeCtrl.transSon = value;
				}
			}
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x0600975E RID: 38750 RVA: 0x003E8EDA File Offset: 0x003E72DA
		public ChaFileStatus charFileStatus
		{
			get
			{
				return this.charInfo.fileStatus;
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x0600975F RID: 38751 RVA: 0x003E8EE7 File Offset: 0x003E72E7
		public int sex
		{
			get
			{
				return (int)this.charInfo.fileParam.sex;
			}
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x06009760 RID: 38752 RVA: 0x003E8EF9 File Offset: 0x003E72F9
		public int HandAnimeNum
		{
			[CompilerGenerated]
			get
			{
				return this.charInfo.GetShapeIndexHandCount();
			}
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06009761 RID: 38753 RVA: 0x003E8F06 File Offset: 0x003E7306
		// (set) Token: 0x06009762 RID: 38754 RVA: 0x003E8F10 File Offset: 0x003E7310
		public bool DynamicAnimeBustL
		{
			get
			{
				return this.dynamicBust[0];
			}
			set
			{
				this.dynamicBust[0] = value;
				this.UpdateDynamicBonesBust(0);
			}
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06009763 RID: 38755 RVA: 0x003E8F22 File Offset: 0x003E7322
		// (set) Token: 0x06009764 RID: 38756 RVA: 0x003E8F2C File Offset: 0x003E732C
		public bool DynamicAnimeBustR
		{
			get
			{
				return this.dynamicBust[1];
			}
			set
			{
				this.dynamicBust[1] = value;
				this.UpdateDynamicBonesBust(1);
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06009765 RID: 38757 RVA: 0x003E8F3E File Offset: 0x003E733E
		// (set) Token: 0x06009766 RID: 38758 RVA: 0x003E8F48 File Offset: 0x003E7348
		public bool DynamicFKBustL
		{
			get
			{
				return this.dynamicBust[2];
			}
			set
			{
				this.dynamicBust[2] = value;
				this.UpdateDynamicBonesBust(0);
			}
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x06009767 RID: 38759 RVA: 0x003E8F5A File Offset: 0x003E735A
		// (set) Token: 0x06009768 RID: 38760 RVA: 0x003E8F64 File Offset: 0x003E7364
		public bool DynamicFKBustR
		{
			get
			{
				return this.dynamicBust[3];
			}
			set
			{
				this.dynamicBust[3] = value;
				this.UpdateDynamicBonesBust(1);
			}
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x06009769 RID: 38761 RVA: 0x003E8F76 File Offset: 0x003E7376
		public VoiceCtrl voiceCtrl
		{
			get
			{
				return this.oiCharInfo.voiceCtrl;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x0600976A RID: 38762 RVA: 0x003E8F83 File Offset: 0x003E7383
		// (set) Token: 0x0600976B RID: 38763 RVA: 0x003E8F90 File Offset: 0x003E7390
		public VoiceCtrl.Repeat voiceRepeat
		{
			get
			{
				return this.voiceCtrl.repeat;
			}
			set
			{
				this.voiceCtrl.repeat = value;
			}
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x0600976C RID: 38764 RVA: 0x003E8F9E File Offset: 0x003E739E
		// (set) Token: 0x0600976D RID: 38765 RVA: 0x003E8FA6 File Offset: 0x003E73A6
		private int neckPtnOld { get; set; }

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x0600976E RID: 38766 RVA: 0x003E8FAF File Offset: 0x003E73AF
		// (set) Token: 0x0600976F RID: 38767 RVA: 0x003E8FB7 File Offset: 0x003E73B7
		protected int breastLayer { get; set; }

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06009770 RID: 38768 RVA: 0x003E8FC0 File Offset: 0x003E73C0
		protected OCIChar.LoadedAnimeInfo loadedAnimeInfo
		{
			get
			{
				return this._loadedAnimeInfo;
			}
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06009771 RID: 38769 RVA: 0x003E8FC8 File Offset: 0x003E73C8
		// (set) Token: 0x06009772 RID: 38770 RVA: 0x003E8FD0 File Offset: 0x003E73D0
		public Preparation preparation { get; set; }

		// Token: 0x06009773 RID: 38771 RVA: 0x003E8FDC File Offset: 0x003E73DC
		public override void OnDelete()
		{
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			this.voiceCtrl.Stop();
			for (int i = 0; i < this.listBones.Count; i++)
			{
				Singleton<GuideObjectManager>.Instance.Delete(this.listBones[i].guideObject, true);
			}
			for (int j = 0; j < this.listIKTarget.Count; j++)
			{
				Singleton<GuideObjectManager>.Instance.Delete(this.listIKTarget[j].guideObject, true);
			}
			Singleton<GuideObjectManager>.Instance.Delete(this.lookAtInfo.guideObject, true);
			if (this.parentInfo != null)
			{
				this.parentInfo.OnDetachChild(this);
			}
			Studio.DeleteInfo(this.objectInfo, true);
		}

		// Token: 0x06009774 RID: 38772 RVA: 0x003E90B0 File Offset: 0x003E74B0
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			int num = -1;
			if (!this.dicAccessoryPoint.TryGetValue(_parent, out num))
			{
				return;
			}
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.oiCharInfo.child[num].Contains(_child.objectInfo))
			{
				this.oiCharInfo.child[num].Add(_child.objectInfo);
			}
			bool flag = false;
			if (_child is OCIItem)
			{
				flag = (_child as OCIItem).IsParticleArray;
			}
			Transform accessoryParentTransform = this.charInfo.GetAccessoryParentTransform(num);
			if (!flag)
			{
				_child.guideObject.transformTarget.SetParent(accessoryParentTransform);
			}
			_child.guideObject.parent = accessoryParentTransform;
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

		// Token: 0x06009775 RID: 38773 RVA: 0x003E9308 File Offset: 0x003E7708
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			int num = -1;
			if (!this.dicAccessoryPoint.TryGetValue(_parent, out num))
			{
				return;
			}
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.oiCharInfo.child[num].Contains(_child.objectInfo))
			{
				this.oiCharInfo.child[num].Add(_child.objectInfo);
			}
			bool flag = false;
			if (_child is OCIItem)
			{
				flag = (_child as OCIItem).IsParticleArray;
			}
			Transform accessoryParentTransform = this.charInfo.GetAccessoryParentTransform(num);
			if (!flag)
			{
				_child.guideObject.transformTarget.SetParent(accessoryParentTransform);
			}
			_child.guideObject.parent = accessoryParentTransform;
			_child.guideObject.nonconnect = flag;
			_child.guideObject.calcScale = !flag;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_child.guideObject.changeAmount.OnChange();
			_child.parentInfo = this;
		}

		// Token: 0x06009776 RID: 38774 RVA: 0x003E9424 File Offset: 0x003E7824
		public override void OnDetach()
		{
			this.parentInfo.OnDetachChild(this);
			this.guideObject.parent = null;
			Studio.AddInfo(this.objectInfo, this);
			this.guideObject.transformTarget.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			this.objectInfo.changeAmount.pos = this.guideObject.transformTarget.localPosition;
			this.objectInfo.changeAmount.rot = this.guideObject.transformTarget.localEulerAngles;
			this.guideObject.mode = GuideObject.Mode.Local;
			this.guideObject.moveCalc = GuideMove.MoveCalc.TYPE1;
			this.treeNodeObject.ResetVisible();
		}

		// Token: 0x06009777 RID: 38775 RVA: 0x003E94D8 File Offset: 0x003E78D8
		public override void OnSelect(bool _select)
		{
			int layer = LayerMask.NameToLayer((!_select) ? "Studio/Select" : "Studio/Col");
			this.lookAtInfo.layer = layer;
			for (int i = 0; i < this.listBones.Count; i++)
			{
				this.listBones[i].layer = layer;
			}
			for (int j = 0; j < this.listIKTarget.Count; j++)
			{
				this.listIKTarget[j].layer = layer;
			}
		}

		// Token: 0x06009778 RID: 38776 RVA: 0x003E9568 File Offset: 0x003E7968
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
			foreach (KeyValuePair<int, List<ObjectInfo>> keyValuePair in this.oiCharInfo.child)
			{
				if (keyValuePair.Value.Remove(_child.objectInfo))
				{
					break;
				}
			}
			_child.parentInfo = null;
		}

		// Token: 0x06009779 RID: 38777 RVA: 0x003E95E8 File Offset: 0x003E79E8
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.neckLookCtrl.SaveNeckLookCtrl(binaryWriter);
					this.oiCharInfo.neckByteData = memoryStream.ToArray();
				}
			}
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2))
				{
					this.charInfo.eyeLookCtrl.eyeLookScript.SaveAngle(binaryWriter2);
					this.oiCharInfo.eyesByteData = memoryStream2.ToArray();
				}
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.charInfo.animBody.GetCurrentAnimatorStateInfo(0);
			this.oiCharInfo.animeNormalizedTime = currentAnimatorStateInfo.normalizedTime;
			this.oiCharInfo.dicAccessGroup = new Dictionary<int, TreeNodeObject.TreeState>();
			this.oiCharInfo.dicAccessNo = new Dictionary<int, TreeNodeObject.TreeState>();
			foreach (KeyValuePair<int, OCIChar.AccessPointInfo> keyValuePair in this.dicAccessPoint)
			{
				this.oiCharInfo.dicAccessGroup.Add(keyValuePair.Key, keyValuePair.Value.root.treeState);
				foreach (KeyValuePair<int, TreeNodeObject> keyValuePair2 in keyValuePair.Value.child)
				{
					this.oiCharInfo.dicAccessNo.Add(keyValuePair2.Key, keyValuePair2.Value.treeState);
				}
			}
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x0600977A RID: 38778 RVA: 0x003E97F8 File Offset: 0x003E7BF8
		// (set) Token: 0x0600977B RID: 38779 RVA: 0x003E9805 File Offset: 0x003E7C05
		public override float animeSpeed
		{
			get
			{
				return this.oiCharInfo.animeSpeed;
			}
			set
			{
				this.oiCharInfo.animeSpeed = value;
				if (this.charInfo.animBody)
				{
					this.charInfo.animBody.speed = value;
				}
			}
		}

		// Token: 0x0600977C RID: 38780 RVA: 0x003E983C File Offset: 0x003E7C3C
		public override void OnVisible(bool _visible)
		{
			this.charInfo.visibleAll = _visible;
			if (this.optionItemCtrl)
			{
				this.optionItemCtrl.outsideVisible = _visible;
			}
			foreach (OCIChar.BoneInfo boneInfo in this.listBones)
			{
				boneInfo.guideObject.visibleOutside = _visible;
			}
			foreach (OCIChar.IKInfo ikinfo in this.listIKTarget)
			{
				ikinfo.guideObject.visibleOutside = _visible;
			}
			if (this.lookAtInfo != null && this.lookAtInfo.guideObject)
			{
				this.lookAtInfo.guideObject.visibleOutside = _visible;
			}
		}

		// Token: 0x0600977D RID: 38781 RVA: 0x003E9948 File Offset: 0x003E7D48
		public void InitKinematic(GameObject _target, FullBodyBipedIK _finalIK, NeckLookControllerVer2 _neckLook, DynamicBone[] _hairDynamic, DynamicBone[] _skirtDynamic)
		{
			this.neckLookCtrl = _neckLook;
			this.neckPtnOld = this.charFileStatus.neckLookPtn;
			this.skirtDynamic = _skirtDynamic;
			this.InitFK(_target);
			for (int i = 0; i < this.listIKTarget.Count; i++)
			{
				this.listIKTarget[i].active = false;
			}
			this.finalIK = _finalIK;
			this.finalIK.enabled = false;
		}

		// Token: 0x0600977E RID: 38782 RVA: 0x003E99C0 File Offset: 0x003E7DC0
		public void InitFK(GameObject _target)
		{
			if (this.fkCtrl == null && _target != null)
			{
				this.fkCtrl = _target.AddComponent<FKCtrl>();
			}
			this.fkCtrl.InitBones(this, this.oiCharInfo, this.charInfo, this.charReference);
			this.fkCtrl.enabled = false;
			for (int i = 0; i < this.listBones.Count; i++)
			{
				this.listBones[i].active = false;
			}
		}

		// Token: 0x0600977F RID: 38783 RVA: 0x003E9A50 File Offset: 0x003E7E50
		public void ActiveKinematicMode(OICharInfo.KinematicMode _mode, bool _active, bool _force)
		{
			if (_mode != OICharInfo.KinematicMode.IK)
			{
				if (_mode == OICharInfo.KinematicMode.FK)
				{
					if (_force || this.fkCtrl.enabled != _active)
					{
						this.fkCtrl.enabled = _active;
						this.oiCharInfo.enableFK = _active;
						OIBoneInfo.BoneGroup[] parts = FKCtrl.parts;
						for (int i = 0; i < parts.Length; i++)
						{
							this.ActiveFK(parts[i], _active && this.oiCharInfo.activeFK[i], true);
						}
						if (this.oiCharInfo.enableFK)
						{
							this.ActiveKinematicMode(OICharInfo.KinematicMode.IK, false, _force);
						}
					}
				}
			}
			else if (_force || this.finalIK.enabled != _active)
			{
				this.finalIK.enabled = _active;
				this.oiCharInfo.enableIK = _active;
				for (int j = 0; j < 5; j++)
				{
					this.ActiveIK((OIBoneInfo.BoneGroup)(1 << j), _active && this.oiCharInfo.activeIK[j], true);
				}
				if (this.oiCharInfo.enableIK)
				{
					this.ActiveKinematicMode(OICharInfo.KinematicMode.FK, false, _force);
				}
			}
			for (int k = 0; k < 4; k++)
			{
				this.preparation.PvCopy[k] = (!this.oiCharInfo.enableFK && this.enablePV[k]);
			}
		}

		// Token: 0x06009780 RID: 38784 RVA: 0x003E9BCC File Offset: 0x003E7FCC
		public void ActiveFK(OIBoneInfo.BoneGroup _group, bool _active, bool _force = false)
		{
			OCIChar.<ActiveFK>c__AnonStorey0 <ActiveFK>c__AnonStorey = new OCIChar.<ActiveFK>c__AnonStorey0();
			<ActiveFK>c__AnonStorey.parts = FKCtrl.parts;
			int i;
			for (i = 0; i < <ActiveFK>c__AnonStorey.parts.Length; i++)
			{
				if ((_group & <ActiveFK>c__AnonStorey.parts[i]) != (OIBoneInfo.BoneGroup)0)
				{
					if (!_force)
					{
						if (!Utility.SetStruct<bool>(ref this.oiCharInfo.activeFK[i], _active))
						{
							goto IL_10E;
						}
						if (!this.oiCharInfo.enableFK)
						{
							goto IL_10E;
						}
					}
					this.ActiveFKGroup(<ActiveFK>c__AnonStorey.parts[i], _active);
					foreach (OCIChar.BoneInfo boneInfo in from v in this.listBones
					where (v.boneGroup & <ActiveFK>c__AnonStorey.parts[i]) != (OIBoneInfo.BoneGroup)0 & v.boneWeight
					select v)
					{
						boneInfo.active = (_force ? _active : (this.oiCharInfo.enableFK & this.oiCharInfo.activeFK[i]));
					}
				}
				IL_10E:;
			}
		}

		// Token: 0x06009781 RID: 38785 RVA: 0x003E9D18 File Offset: 0x003E8118
		public bool IsFKGroup(OIBoneInfo.BoneGroup _group)
		{
			return this.listBones.Any((OCIChar.BoneInfo v) => (v.boneGroup & _group) != (OIBoneInfo.BoneGroup)0);
		}

		// Token: 0x06009782 RID: 38786 RVA: 0x003E9D4C File Offset: 0x003E814C
		public void InitFKBone(OIBoneInfo.BoneGroup _group)
		{
			foreach (OCIChar.BoneInfo boneInfo in from v in this.listBones
			where (v.boneGroup & _group) != (OIBoneInfo.BoneGroup)0
			select v)
			{
				boneInfo.boneInfo.changeAmount.Reset();
			}
		}

		// Token: 0x06009783 RID: 38787 RVA: 0x003E9DCC File Offset: 0x003E81CC
		private void ActiveFKGroup(OIBoneInfo.BoneGroup _group, bool _active)
		{
			if (_group != OIBoneInfo.BoneGroup.Neck)
			{
				if (_group == OIBoneInfo.BoneGroup.Breast)
				{
					this.DynamicFKBustL = !_active;
					this.DynamicFKBustR = !_active;
				}
			}
			else if (_active)
			{
				this.neckPtnOld = this.charFileStatus.neckLookPtn;
				this.ChangeLookNeckPtn(4);
			}
			else
			{
				this.ChangeLookNeckPtn(this.neckPtnOld);
			}
			this.fkCtrl.SetEnable(_group, _active);
			if (_group != OIBoneInfo.BoneGroup.Hair)
			{
				if (_group == OIBoneInfo.BoneGroup.Skirt)
				{
					if (!this.skirtDynamic.IsNullOrEmpty<DynamicBone>())
					{
						for (int i = 0; i < this.skirtDynamic.Length; i++)
						{
							this.skirtDynamic[i].enabled = !_active;
						}
					}
				}
			}
			else
			{
				ChaFileHair.PartsInfo[] parts = this.charInfo.fileCustom.hair.parts;
				for (int j = 0; j < parts.Length; j++)
				{
					CmpHair cmpHair = this.charInfo.cmpHair.SafeGet(j);
					if (!(cmpHair == null))
					{
						using (Dictionary<int, ChaFileHair.PartsInfo.BundleInfo>.Enumerator enumerator = parts[j].dictBundle.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> v = enumerator.Current;
								cmpHair.boneInfo.SafeProc(v.Key, delegate(CmpHair.BoneInfo _info)
								{
									if (!_info.dynamicBone.IsNullOrEmpty<DynamicBone>())
									{
										foreach (DynamicBone dynamicBone in from _v in _info.dynamicBone
										where _v != null
										select _v)
										{
											dynamicBone.enabled = (!_active & !v.Value.noShake);
										}
									}
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06009784 RID: 38788 RVA: 0x003E9FB0 File Offset: 0x003E83B0
		public void ActiveIK(OIBoneInfo.BoneGroup _group, bool _active, bool _force = false)
		{
			for (int i = 0; i < 5; i++)
			{
				OIBoneInfo.BoneGroup target = (OIBoneInfo.BoneGroup)(1 << i);
				if ((_group & target) != (OIBoneInfo.BoneGroup)0)
				{
					if (_force || Utility.SetStruct<bool>(ref this.oiCharInfo.activeIK[i], _active))
					{
						this.ActiveIKGroup(target, _active);
						foreach (OCIChar.IKInfo ikinfo in from v in this.listIKTarget
						where (v.boneGroup & target) != (OIBoneInfo.BoneGroup)0
						select v)
						{
							ikinfo.active = (_force ? _active : (this.oiCharInfo.enableIK & this.oiCharInfo.activeIK[i]));
						}
					}
				}
			}
		}

		// Token: 0x06009785 RID: 38789 RVA: 0x003EA0A8 File Offset: 0x003E84A8
		private void ActiveIKGroup(OIBoneInfo.BoneGroup _group, bool _active)
		{
			IKSolverFullBodyBiped solver = this.finalIK.solver;
			float num = (!_active) ? 0f : 1f;
			switch (_group)
			{
			case OIBoneInfo.BoneGroup.Body:
				solver.spineMapping.twistWeight = num;
				solver.SetEffectorWeights(FullBodyBipedEffector.Body, num, num);
				break;
			case OIBoneInfo.BoneGroup.RightLeg:
				solver.rightLegMapping.weight = num;
				solver.SetEffectorWeights(FullBodyBipedEffector.RightThigh, num, num);
				solver.SetEffectorWeights(FullBodyBipedEffector.RightFoot, num, num);
				break;
			default:
				if (_group == OIBoneInfo.BoneGroup.LeftArm)
				{
					solver.leftArmMapping.weight = num;
					solver.SetEffectorWeights(FullBodyBipedEffector.LeftShoulder, num, num);
					solver.SetEffectorWeights(FullBodyBipedEffector.LeftHand, num, num);
				}
				break;
			case OIBoneInfo.BoneGroup.LeftLeg:
				solver.leftLegMapping.weight = num;
				solver.SetEffectorWeights(FullBodyBipedEffector.LeftThigh, num, num);
				solver.SetEffectorWeights(FullBodyBipedEffector.LeftFoot, num, num);
				break;
			case OIBoneInfo.BoneGroup.RightArm:
				solver.rightArmMapping.weight = num;
				solver.SetEffectorWeights(FullBodyBipedEffector.RightShoulder, num, num);
				solver.SetEffectorWeights(FullBodyBipedEffector.RightHand, num, num);
				break;
			}
		}

		// Token: 0x06009786 RID: 38790 RVA: 0x003EA1B4 File Offset: 0x003E85B4
		public void UpdateFKColor(params OIBoneInfo.BoneGroup[] _parts)
		{
			if (_parts.IsNullOrEmpty<OIBoneInfo.BoneGroup>())
			{
				return;
			}
			using (List<OCIChar.BoneInfo>.Enumerator enumerator = this.listBones.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIChar.BoneInfo v = enumerator.Current;
					int num = Array.FindIndex<OIBoneInfo.BoneGroup>(_parts, (OIBoneInfo.BoneGroup p) => (p & v.boneGroup) != (OIBoneInfo.BoneGroup)0);
					if (num != -1)
					{
						OIBoneInfo.BoneGroup boneGroup = _parts[num];
						if (boneGroup != OIBoneInfo.BoneGroup.Body)
						{
							if (boneGroup != OIBoneInfo.BoneGroup.RightHand)
							{
								if (boneGroup != OIBoneInfo.BoneGroup.LeftHand)
								{
									if (boneGroup != OIBoneInfo.BoneGroup.Hair)
									{
										if (boneGroup != OIBoneInfo.BoneGroup.Neck)
										{
											if (boneGroup != OIBoneInfo.BoneGroup.Breast)
											{
												if (boneGroup == OIBoneInfo.BoneGroup.Skirt)
												{
													v.color = Studio.optionSystem.colorFKSkirt;
												}
											}
											else
											{
												v.color = Studio.optionSystem.colorFKBreast;
											}
										}
										else
										{
											v.color = Studio.optionSystem.colorFKNeck;
										}
									}
									else
									{
										v.color = Studio.optionSystem.colorFKHair;
									}
								}
								else
								{
									v.color = Studio.optionSystem.colorFKLeftHand;
								}
							}
							else
							{
								v.color = Studio.optionSystem.colorFKRightHand;
							}
						}
						else
						{
							v.color = Studio.optionSystem.colorFKBody;
						}
					}
				}
			}
		}

		// Token: 0x06009787 RID: 38791 RVA: 0x003EA350 File Offset: 0x003E8750
		public void VisibleFKGuide(bool _visible)
		{
			foreach (OCIChar.BoneInfo boneInfo in this.listBones)
			{
				boneInfo.guideObject.visible = _visible;
			}
		}

		// Token: 0x06009788 RID: 38792 RVA: 0x003EA3B4 File Offset: 0x003E87B4
		public void VisibleIKGuide(bool _visible)
		{
			foreach (OCIChar.IKInfo ikinfo in this.listIKTarget)
			{
				ikinfo.guideObject.visible = _visible;
			}
		}

		// Token: 0x06009789 RID: 38793 RVA: 0x003EA418 File Offset: 0x003E8818
		public void EnableExpressionCategory(int _category, bool _value)
		{
			this.oiCharInfo.expression[_category] = _value;
			this.charInfo.EnableExpressionCategory(_category, _value);
		}

		// Token: 0x0600978A RID: 38794 RVA: 0x003EA438 File Offset: 0x003E8838
		public void UpdateDynamicBonesBust(int _type = 2)
		{
			if (_type == 0 || _type == 2)
			{
				this.EnableDynamicBonesBustAndHip(this.dynamicBust[0] & this.dynamicBust[2], 0);
			}
			if (_type == 1 || _type == 2)
			{
				this.EnableDynamicBonesBustAndHip(this.dynamicBust[1] & this.dynamicBust[3], 1);
			}
		}

		// Token: 0x0600978B RID: 38795 RVA: 0x003EA490 File Offset: 0x003E8890
		public void EnableDynamicBonesBustAndHip(bool _enable, int _kind)
		{
			this.charInfo.cmpBoneBody.EnableDynamicBonesBustAndHip(_enable, _kind);
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x0600978C RID: 38796 RVA: 0x003EA4A4 File Offset: 0x003E88A4
		// (set) Token: 0x0600978D RID: 38797 RVA: 0x003EA4B4 File Offset: 0x003E88B4
		public float animePattern
		{
			get
			{
				return this.oiCharInfo.animePattern;
			}
			set
			{
				this.oiCharInfo.animePattern = value;
				if (this.isAnimeMotion)
				{
					this.charInfo.setAnimatorParamFloat("motion", this.oiCharInfo.animePattern);
				}
				if (this.optionItemCtrl)
				{
					this.optionItemCtrl.SetMotion(this.oiCharInfo.animePattern);
				}
			}
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x0600978E RID: 38798 RVA: 0x003EA519 File Offset: 0x003E8919
		public float[] animeOptionParam
		{
			get
			{
				return this.oiCharInfo.animeOptionParam;
			}
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x0600978F RID: 38799 RVA: 0x003EA526 File Offset: 0x003E8926
		// (set) Token: 0x06009790 RID: 38800 RVA: 0x003EA538 File Offset: 0x003E8938
		public float animeOptionParam1
		{
			get
			{
				return this.oiCharInfo.animeOptionParam[0];
			}
			set
			{
				this.oiCharInfo.animeOptionParam[0] = value;
				if (this.isHAnime && !this.animeParam[0].IsNullOrEmpty())
				{
					this.charInfo.setAnimatorParamFloat(this.animeParam[0], value);
				}
			}
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x06009791 RID: 38801 RVA: 0x003EA584 File Offset: 0x003E8984
		// (set) Token: 0x06009792 RID: 38802 RVA: 0x003EA594 File Offset: 0x003E8994
		public float animeOptionParam2
		{
			get
			{
				return this.oiCharInfo.animeOptionParam[1];
			}
			set
			{
				this.oiCharInfo.animeOptionParam[1] = value;
				if (this.isHAnime && !this.animeParam[1].IsNullOrEmpty())
				{
					this.charInfo.setAnimatorParamFloat(this.animeParam[1], value);
				}
			}
		}

		// Token: 0x06009793 RID: 38803 RVA: 0x003EA5E0 File Offset: 0x003E89E0
		public virtual void LoadAnime(int _group, int _category, int _no, float _normalizedTime = 0f)
		{
			Dictionary<int, Dictionary<int, Info.AnimeLoadInfo>> dictionary = null;
			if (!Singleton<Info>.Instance.dicAnimeLoadInfo.TryGetValue(_group, out dictionary))
			{
				return;
			}
			Dictionary<int, Info.AnimeLoadInfo> dictionary2 = null;
			if (!dictionary.TryGetValue(_category, out dictionary2))
			{
				return;
			}
			Info.AnimeLoadInfo animeLoadInfo = null;
			if (!dictionary2.TryGetValue(_no, out animeLoadInfo))
			{
				return;
			}
			if (this.loadedAnimeInfo.BaseCheck(animeLoadInfo.bundlePath, animeLoadInfo.fileName))
			{
				this.charInfo.LoadAnimation(animeLoadInfo.bundlePath, animeLoadInfo.fileName, string.Empty);
				this.loadedAnimeInfo.baseFile.bundlePath = animeLoadInfo.bundlePath;
				this.loadedAnimeInfo.baseFile.fileName = animeLoadInfo.fileName;
			}
			if (animeLoadInfo is Info.HAnimeLoadInfo)
			{
				Info.HAnimeLoadInfo hanimeLoadInfo = animeLoadInfo as Info.HAnimeLoadInfo;
				if (hanimeLoadInfo.overrideFile.Check)
				{
					if (this.loadedAnimeInfo.OverrideCheck(hanimeLoadInfo.overrideFile.bundlePath, hanimeLoadInfo.overrideFile.fileName))
					{
						CommonLib.LoadAsset<RuntimeAnimatorController>(hanimeLoadInfo.overrideFile.bundlePath, hanimeLoadInfo.overrideFile.fileName, false, string.Empty).SafeProc(delegate(RuntimeAnimatorController rac)
						{
							this.charAnimeCtrl.animator.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.charAnimeCtrl.animator.runtimeAnimatorController, rac);
						});
						AssetBundleManager.UnloadAssetBundle(hanimeLoadInfo.overrideFile.bundlePath, true, null, false);
						this.loadedAnimeInfo.overrideFile.bundlePath = hanimeLoadInfo.overrideFile.bundlePath;
						this.loadedAnimeInfo.overrideFile.fileName = hanimeLoadInfo.overrideFile.fileName;
					}
				}
				else
				{
					this.loadedAnimeInfo.overrideFile.Clear();
				}
				this.isAnimeMotion = hanimeLoadInfo.isMotion;
				this.isHAnime = true;
				this.animeParam[1] = this.CheckAnimeParam(new string[]
				{
					"Breast1",
					"Breast",
					"breast"
				});
				if (!this.animeParam[1].IsNullOrEmpty())
				{
					this.charInfo.setAnimatorParamFloat(this.animeParam[1], this.charInfo.fileBody.shapeValueBody[1]);
				}
				if (this.breastLayer != -1)
				{
					this.charAnimeCtrl.animator.SetLayerWeight(this.breastLayer, 0f);
					this.breastLayer = -1;
				}
				if (hanimeLoadInfo.isBreastLayer)
				{
					this.charAnimeCtrl.animator.SetLayerWeight(hanimeLoadInfo.breastLayer, 1f);
					this.breastLayer = hanimeLoadInfo.breastLayer;
					this.charAnimeCtrl.Play(animeLoadInfo.clip, _normalizedTime, hanimeLoadInfo.breastLayer);
				}
				if (hanimeLoadInfo.isMotion)
				{
					this.charInfo.setAnimatorParamFloat("motion", this.oiCharInfo.animePattern);
				}
				for (int i = 0; i < 8; i++)
				{
					this.enablePV[i] = hanimeLoadInfo.pv[i];
					this.preparation.PvCopy[i] = (!this.oiCharInfo.enableFK && this.enablePV[i]);
				}
				if (!hanimeLoadInfo.yureFile.Check || !this.yureCtrl.Load(hanimeLoadInfo.yureFile.bundlePath, hanimeLoadInfo.yureFile.fileName, hanimeLoadInfo.motionID, hanimeLoadInfo.num))
				{
					this.yureCtrl.ResetShape(true);
				}
				this.charInfo.setAnimatorParamFloat("speed", 1f);
			}
			else
			{
				this.loadedAnimeInfo.overrideFile.Clear();
				for (int j = 0; j < 4; j++)
				{
					this.enablePV[j] = true;
					this.preparation.PvCopy[j] = (!this.oiCharInfo.enableFK && this.enablePV[j]);
				}
				this.isAnimeMotion = false;
				this.isHAnime = false;
			}
			this.optionItemCtrl.LoadAnimeItem(animeLoadInfo, animeLoadInfo.clip, this.charInfo.fileBody.shapeValueBody[0], this.oiCharInfo.animePattern);
			if (_normalizedTime != 0f)
			{
				this.charAnimeCtrl.Play(animeLoadInfo.clip, _normalizedTime);
			}
			else
			{
				this.charAnimeCtrl.Play(animeLoadInfo.clip);
			}
			this.animeParam[0] = this.CheckAnimeParam(new string[]
			{
				"height1",
				"height"
			});
			if (!this.animeParam[0].IsNullOrEmpty())
			{
				this.charInfo.setAnimatorParamFloat(this.animeParam[0], this.charInfo.fileBody.shapeValueBody[0]);
			}
			this.animeOptionParam1 = this.animeOptionParam1;
			this.animeOptionParam2 = this.animeOptionParam2;
			this.charAnimeCtrl.nameHadh = Animator.StringToHash(animeLoadInfo.clip);
			this.oiCharInfo.animeInfo.Set(_group, _category, _no);
			this.SetNipStand(this.oiCharInfo.nipple);
			this.SetSonLength(this.oiCharInfo.sonLength);
		}

		// Token: 0x06009794 RID: 38804 RVA: 0x003EAACE File Offset: 0x003E8ECE
		public virtual void ChangeHandAnime(int _type, int _ptn)
		{
			this.oiCharInfo.handPtn[_type] = _ptn;
			if (_ptn != 0)
			{
				this.charInfo.SetShapeHandValue(_type, _ptn - 1, 0, 0f);
			}
			this.charInfo.SetEnableShapeHand(_type, _ptn != 0);
		}

		// Token: 0x06009795 RID: 38805 RVA: 0x003EAB10 File Offset: 0x003E8F10
		public virtual void RestartAnime()
		{
			Animator animBody = this.charInfo.animBody;
			int layerCount = animBody.layerCount;
			for (int i = 0; i < layerCount; i++)
			{
				animBody.Play(animBody.GetCurrentAnimatorStateInfo(i).shortNameHash, i, 0f);
			}
			this.optionItemCtrl.PlayAnime();
		}

		// Token: 0x06009796 RID: 38806 RVA: 0x003EAB68 File Offset: 0x003E8F68
		private string CheckAnimeParam(params string[] _names)
		{
			OCIChar.<CheckAnimeParam>c__AnonStorey8 <CheckAnimeParam>c__AnonStorey = new OCIChar.<CheckAnimeParam>c__AnonStorey8();
			<CheckAnimeParam>c__AnonStorey._names = _names;
			AnimatorControllerParameter[] parameters = this.charInfo.animBody.parameters;
			if (parameters.IsNullOrEmpty<AnimatorControllerParameter>())
			{
				return string.Empty;
			}
			int i;
			for (i = 0; i < <CheckAnimeParam>c__AnonStorey._names.Length; i++)
			{
				AnimatorControllerParameter animatorControllerParameter = parameters.FirstOrDefault((AnimatorControllerParameter p) => string.CompareOrdinal(p.name, <CheckAnimeParam>c__AnonStorey._names[i]) == 0);
				if (animatorControllerParameter != null)
				{
					return <CheckAnimeParam>c__AnonStorey._names[i];
				}
			}
			return string.Empty;
		}

		// Token: 0x06009797 RID: 38807 RVA: 0x003EAC0C File Offset: 0x003E900C
		public virtual void ChangeChara(string _path)
		{
			foreach (OCIChar.BoneInfo boneInfo in (from v in this.listBones
			where v.boneGroup == OIBoneInfo.BoneGroup.Hair
			select v).ToList<OCIChar.BoneInfo>())
			{
				Singleton<GuideObjectManager>.Instance.Delete(boneInfo.guideObject, true);
			}
			this.listBones = (from v in this.listBones
			where v.boneGroup != OIBoneInfo.BoneGroup.Hair
			select v).ToList<OCIChar.BoneInfo>();
			int[] array = (from b in this.oiCharInfo.bones
			where b.Value.@group == OIBoneInfo.BoneGroup.Hair
			select b.Key).ToArray<int>();
			for (int k = 0; k < array.Length; k++)
			{
				this.oiCharInfo.bones.Remove(array[k]);
			}
			this.skirtDynamic = null;
			this.charInfo.chaFile.LoadCharaFile(_path, byte.MaxValue, true, true);
			this.charInfo.ChangeNowCoordinate(false, true);
			this.charInfo.Reload(false, false, false, false, true);
			for (int j = 0; j < 2; j++)
			{
				GameObject gameObject = this.charInfo.objHair.SafeGet(j);
				if (gameObject != null)
				{
					AddObjectAssist.ArrangeNames(gameObject.transform);
				}
			}
			this.treeNodeObject.textName = this.charInfo.chaFile.parameter.fullname;
			AddObjectAssist.InitHairBone(this, Singleton<Info>.Instance.dicBoneInfo);
			this.skirtDynamic = AddObjectFemale.GetSkirtDynamic(this.charInfo.objClothes);
			this.InitFK(null);
			foreach (var <>__AnonType in FKCtrl.parts.Select((OIBoneInfo.BoneGroup p, int i) => new
			{
				p,
				i
			}))
			{
				this.ActiveFK(<>__AnonType.p, this.oiCharInfo.activeFK[<>__AnonType.i], this.oiCharInfo.activeFK[<>__AnonType.i]);
			}
			this.ActiveKinematicMode(OICharInfo.KinematicMode.FK, this.oiCharInfo.enableFK, true);
			this.UpdateFKColor(new OIBoneInfo.BoneGroup[]
			{
				OIBoneInfo.BoneGroup.Hair
			});
			this.ChangeEyesOpen(this.charFileStatus.eyesOpenMax);
			this.ChangeBlink(this.charFileStatus.eyesBlink);
			this.ChangeMouthOpen(this.oiCharInfo.mouthOpen);
		}

		// Token: 0x06009798 RID: 38808 RVA: 0x003EAF10 File Offset: 0x003E9310
		public virtual void SetClothesStateAll(int _state)
		{
		}

		// Token: 0x06009799 RID: 38809 RVA: 0x003EAF12 File Offset: 0x003E9312
		public virtual void SetClothesState(int _id, byte _state)
		{
			this.charInfo.SetClothesState(_id, _state, true);
		}

		// Token: 0x0600979A RID: 38810 RVA: 0x003EAF22 File Offset: 0x003E9322
		public virtual void ShowAccessory(int _id, bool _flag)
		{
			this.charFileStatus.showAccessory[_id] = _flag;
		}

		// Token: 0x0600979B RID: 38811 RVA: 0x003EAF32 File Offset: 0x003E9332
		public virtual void LoadClothesFile(string _path)
		{
			this.charInfo.ChangeNowCoordinate(_path, true, true);
			this.charInfo.AssignCoordinate();
		}

		// Token: 0x0600979C RID: 38812 RVA: 0x003EAF4F File Offset: 0x003E934F
		public virtual void SetSiruFlags(ChaFileDefine.SiruParts _parts, byte _state)
		{
			this.oiCharInfo.siru[(int)_parts] = _state;
		}

		// Token: 0x0600979D RID: 38813 RVA: 0x003EAF5F File Offset: 0x003E935F
		public virtual byte GetSiruFlags(ChaFileDefine.SiruParts _parts)
		{
			return 0;
		}

		// Token: 0x0600979E RID: 38814 RVA: 0x003EAF62 File Offset: 0x003E9362
		public virtual void SetTuyaRate(float _value)
		{
			this.charInfo.skinGlossRate = _value;
		}

		// Token: 0x0600979F RID: 38815 RVA: 0x003EAF70 File Offset: 0x003E9370
		public virtual void SetWetRate(float _value)
		{
			this.charInfo.wetRate = _value;
		}

		// Token: 0x060097A0 RID: 38816 RVA: 0x003EAF7E File Offset: 0x003E937E
		public virtual void SetNipStand(float _value)
		{
		}

		// Token: 0x060097A1 RID: 38817 RVA: 0x003EAF80 File Offset: 0x003E9380
		public virtual void SetVisibleSimple(bool _flag)
		{
			this.oiCharInfo.visibleSimple = _flag;
			this.charInfo.ChangeSimpleBodyDraw(_flag);
		}

		// Token: 0x060097A2 RID: 38818 RVA: 0x003EAF9A File Offset: 0x003E939A
		public bool GetVisibleSimple()
		{
			return this.oiCharInfo.visibleSimple;
		}

		// Token: 0x060097A3 RID: 38819 RVA: 0x003EAFA7 File Offset: 0x003E93A7
		public virtual void SetSimpleColor(Color _color)
		{
			this.oiCharInfo.simpleColor = _color;
			this.charInfo.ChangeSimpleBodyColor(_color);
		}

		// Token: 0x060097A4 RID: 38820 RVA: 0x003EAFC1 File Offset: 0x003E93C1
		public virtual void SetVisibleSon(bool _flag)
		{
			this.oiCharInfo.visibleSon = _flag;
			this.charFileStatus.visibleSonAlways = _flag;
		}

		// Token: 0x060097A5 RID: 38821 RVA: 0x003EAFDB File Offset: 0x003E93DB
		public virtual float GetSonLength()
		{
			return this.oiCharInfo.sonLength;
		}

		// Token: 0x060097A6 RID: 38822 RVA: 0x003EAFE8 File Offset: 0x003E93E8
		public virtual void SetSonLength(float _value)
		{
			this.oiCharInfo.sonLength = _value;
		}

		// Token: 0x060097A7 RID: 38823 RVA: 0x003EAFF6 File Offset: 0x003E93F6
		public virtual void SetTears(float _state)
		{
			this.charInfo.ChangeTearsRate(_state);
		}

		// Token: 0x060097A8 RID: 38824 RVA: 0x003EB004 File Offset: 0x003E9404
		public virtual float GetTears()
		{
			return this.charFileStatus.tearsRate;
		}

		// Token: 0x060097A9 RID: 38825 RVA: 0x003EB011 File Offset: 0x003E9411
		public virtual void SetHohoAkaRate(float _value)
		{
			this.charInfo.ChangeHohoAkaRate(_value);
		}

		// Token: 0x060097AA RID: 38826 RVA: 0x003EB01F File Offset: 0x003E941F
		public virtual float GetHohoAkaRate()
		{
			return this.charInfo.fileStatus.hohoAkaRate;
		}

		// Token: 0x060097AB RID: 38827 RVA: 0x003EB034 File Offset: 0x003E9434
		public virtual void ChangeLookEyesPtn(int _ptn, bool _force = false)
		{
			int num = (!_force) ? this.charInfo.fileStatus.eyesLookPtn : -1;
			if (_ptn == 4 && num != 4)
			{
				this.charInfo.eyeLookCtrl.target = this.lookAtInfo.target;
				this.lookAtInfo.active = true;
			}
			else if (num == 4 && _ptn != 4)
			{
				this.charInfo.eyeLookCtrl.target = Camera.main.transform;
				this.lookAtInfo.active = false;
			}
			this.charInfo.ChangeLookEyesPtn(_ptn);
		}

		// Token: 0x060097AC RID: 38828 RVA: 0x003EB0D8 File Offset: 0x003E94D8
		public virtual void ChangeLookNeckPtn(int _ptn)
		{
			this.charInfo.ChangeLookNeckPtn(_ptn, 1f);
		}

		// Token: 0x060097AD RID: 38829 RVA: 0x003EB0EB File Offset: 0x003E94EB
		public virtual void ChangeEyesOpen(float _value)
		{
			this.charInfo.ChangeEyesOpenMax(_value);
		}

		// Token: 0x060097AE RID: 38830 RVA: 0x003EB0F9 File Offset: 0x003E94F9
		public virtual void ChangeBlink(bool _value)
		{
			this.charInfo.ChangeEyesBlinkFlag(_value);
		}

		// Token: 0x060097AF RID: 38831 RVA: 0x003EB108 File Offset: 0x003E9508
		public virtual void ChangeMouthOpen(float _value)
		{
			this.oiCharInfo.mouthOpen = _value;
			if (this.charInfo.mouthCtrl != null)
			{
				this.charInfo.mouthCtrl.FixedRate = ((!this.voiceCtrl.isPlay || !this.oiCharInfo.lipSync) ? _value : -1f);
			}
		}

		// Token: 0x060097B0 RID: 38832 RVA: 0x003EB16C File Offset: 0x003E956C
		public virtual void ChangeLipSync(bool _value)
		{
			this.oiCharInfo.lipSync = _value;
			this.charInfo.SetVoiceTransform((!_value) ? null : this.oiCharInfo.voiceCtrl.transVoice);
			this.ChangeMouthOpen(this.oiCharInfo.mouthOpen);
		}

		// Token: 0x060097B1 RID: 38833 RVA: 0x003EB1BE File Offset: 0x003E95BE
		public virtual void SetVoice()
		{
			this.ChangeLipSync(this.oiCharInfo.lipSync);
		}

		// Token: 0x060097B2 RID: 38834 RVA: 0x003EB1D1 File Offset: 0x003E95D1
		public virtual void AddVoice(int _group, int _category, int _no)
		{
			this.voiceCtrl.list.Add(new VoiceCtrl.VoiceInfo(_group, _category, _no));
		}

		// Token: 0x060097B3 RID: 38835 RVA: 0x003EB1EB File Offset: 0x003E95EB
		public virtual void DeleteVoice(int _index)
		{
			this.voiceCtrl.list.RemoveAt(_index);
			if (this.voiceCtrl.index == _index)
			{
				this.voiceCtrl.index = -1;
				this.voiceCtrl.Stop();
			}
		}

		// Token: 0x060097B4 RID: 38836 RVA: 0x003EB226 File Offset: 0x003E9626
		public virtual void DeleteAllVoice()
		{
			this.voiceCtrl.list.Clear();
			this.voiceCtrl.Stop();
		}

		// Token: 0x060097B5 RID: 38837 RVA: 0x003EB243 File Offset: 0x003E9643
		public virtual bool PlayVoice(int _index)
		{
			return this.voiceCtrl.Play((_index >= 0) ? _index : 0);
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x003EB25E File Offset: 0x003E965E
		public virtual void StopVoice()
		{
			this.voiceCtrl.Stop();
		}

		// Token: 0x0400796E RID: 31086
		public ChaReference charReference;

		// Token: 0x0400796F RID: 31087
		public Dictionary<int, OCIChar.AccessPointInfo> dicAccessPoint = new Dictionary<int, OCIChar.AccessPointInfo>();

		// Token: 0x04007970 RID: 31088
		public List<OCIChar.BoneInfo> listBones = new List<OCIChar.BoneInfo>();

		// Token: 0x04007971 RID: 31089
		public List<OCIChar.IKInfo> listIKTarget = new List<OCIChar.IKInfo>();

		// Token: 0x04007972 RID: 31090
		public OCIChar.LookAtInfo lookAtInfo;

		// Token: 0x04007973 RID: 31091
		public ChaControl charInfo;

		// Token: 0x04007974 RID: 31092
		public FKCtrl fkCtrl;

		// Token: 0x04007975 RID: 31093
		public IKCtrl ikCtrl;

		// Token: 0x04007976 RID: 31094
		public FullBodyBipedIK finalIK;

		// Token: 0x04007977 RID: 31095
		public NeckLookControllerVer2 neckLookCtrl;

		// Token: 0x04007978 RID: 31096
		public DynamicBone[] skirtDynamic;

		// Token: 0x04007979 RID: 31097
		private bool[] dynamicBust = new bool[]
		{
			true,
			true,
			true,
			true
		};

		// Token: 0x0400797A RID: 31098
		private bool[] enablePV = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x0400797B RID: 31099
		public OptionItemCtrl optionItemCtrl;

		// Token: 0x0400797C RID: 31100
		public bool isAnimeMotion;

		// Token: 0x0400797D RID: 31101
		public bool isHAnime;

		// Token: 0x0400797E RID: 31102
		public CharAnimeCtrl charAnimeCtrl;

		// Token: 0x0400797F RID: 31103
		public YureCtrl yureCtrl;

		// Token: 0x04007980 RID: 31104
		public string[] animeParam = new string[]
		{
			"height",
			"Breast"
		};

		// Token: 0x04007981 RID: 31105
		public Dictionary<TreeNodeObject, int> dicAccessoryPoint = new Dictionary<TreeNodeObject, int>();

		// Token: 0x04007984 RID: 31108
		private OCIChar.LoadedAnimeInfo _loadedAnimeInfo = new OCIChar.LoadedAnimeInfo();

		// Token: 0x0200120F RID: 4623
		public class SyncBoneInfo
		{
			// Token: 0x060097BD RID: 38845 RVA: 0x003EB2DB File Offset: 0x003E96DB
			public SyncBoneInfo(GameObject _gameObject)
			{
				this.GameObject = _gameObject;
			}

			// Token: 0x1700201A RID: 8218
			// (get) Token: 0x060097BE RID: 38846 RVA: 0x003EB2EA File Offset: 0x003E96EA
			// (set) Token: 0x060097BF RID: 38847 RVA: 0x003EB2F2 File Offset: 0x003E96F2
			public GameObject GameObject { get; private set; }

			// Token: 0x1700201B RID: 8219
			// (get) Token: 0x060097C0 RID: 38848 RVA: 0x003EB2FC File Offset: 0x003E96FC
			private Transform Transform
			{
				[CompilerGenerated]
				get
				{
					Transform result;
					if ((result = this._transform) == null)
					{
						result = (this._transform = this.GameObject.transform);
					}
					return result;
				}
			}

			// Token: 0x1700201C RID: 8220
			// (set) Token: 0x060097C1 RID: 38849 RVA: 0x003EB32A File Offset: 0x003E972A
			public Quaternion Rotation
			{
				set
				{
					this.Transform.rotation = value;
				}
			}

			// Token: 0x1700201D RID: 8221
			// (set) Token: 0x060097C2 RID: 38850 RVA: 0x003EB338 File Offset: 0x003E9738
			public Quaternion LocalRotation
			{
				set
				{
					this.Transform.localRotation = value;
				}
			}

			// Token: 0x0400798C RID: 31116
			private Transform _transform;
		}

		// Token: 0x02001210 RID: 4624
		public class BoneInfo
		{
			// Token: 0x060097C3 RID: 38851 RVA: 0x003EB346 File Offset: 0x003E9746
			public BoneInfo(GuideObject _guideObject, OIBoneInfo _boneInfo, int _boneID)
			{
				this.guideObject = _guideObject;
				this.boneInfo = _boneInfo;
				this.boneID = _boneID;
				this.boneWeight = true;
			}

			// Token: 0x1700201E RID: 8222
			// (get) Token: 0x060097C4 RID: 38852 RVA: 0x003EB36A File Offset: 0x003E976A
			// (set) Token: 0x060097C5 RID: 38853 RVA: 0x003EB372 File Offset: 0x003E9772
			public GuideObject guideObject { get; private set; }

			// Token: 0x1700201F RID: 8223
			// (get) Token: 0x060097C6 RID: 38854 RVA: 0x003EB37B File Offset: 0x003E977B
			// (set) Token: 0x060097C7 RID: 38855 RVA: 0x003EB383 File Offset: 0x003E9783
			public OIBoneInfo boneInfo { get; private set; }

			// Token: 0x17002020 RID: 8224
			// (get) Token: 0x060097C8 RID: 38856 RVA: 0x003EB38C File Offset: 0x003E978C
			public GameObject gameObject
			{
				[CompilerGenerated]
				get
				{
					GameObject result;
					if ((result = this.m_GameObject) == null)
					{
						result = (this.m_GameObject = this.guideObject.gameObject);
					}
					return result;
				}
			}

			// Token: 0x17002021 RID: 8225
			// (get) Token: 0x060097C9 RID: 38857 RVA: 0x003EB3BA File Offset: 0x003E97BA
			// (set) Token: 0x060097CA RID: 38858 RVA: 0x003EB3DE File Offset: 0x003E97DE
			public bool active
			{
				get
				{
					return this.gameObject != null && this.gameObject.activeSelf;
				}
				set
				{
					GameObject gameObject = this.gameObject;
					if (gameObject != null)
					{
						gameObject.SetActiveIfDifferent(value);
					}
				}
			}

			// Token: 0x17002022 RID: 8226
			// (get) Token: 0x060097CB RID: 38859 RVA: 0x003EB3F6 File Offset: 0x003E97F6
			public OIBoneInfo.BoneGroup boneGroup
			{
				get
				{
					return this.boneInfo.group;
				}
			}

			// Token: 0x17002023 RID: 8227
			// (get) Token: 0x060097CC RID: 38860 RVA: 0x003EB403 File Offset: 0x003E9803
			// (set) Token: 0x060097CD RID: 38861 RVA: 0x003EB410 File Offset: 0x003E9810
			public float scaleRate
			{
				get
				{
					return this.guideObject.scaleRate;
				}
				set
				{
					this.guideObject.scaleRate = value;
				}
			}

			// Token: 0x17002024 RID: 8228
			// (set) Token: 0x060097CE RID: 38862 RVA: 0x003EB41E File Offset: 0x003E981E
			public int layer
			{
				set
				{
					this.guideObject.SetLayer(this.gameObject, value);
				}
			}

			// Token: 0x17002025 RID: 8229
			// (set) Token: 0x060097CF RID: 38863 RVA: 0x003EB432 File Offset: 0x003E9832
			public Color color
			{
				set
				{
					this.guideObject.guideSelect.color = value;
				}
			}

			// Token: 0x17002026 RID: 8230
			// (get) Token: 0x060097D0 RID: 38864 RVA: 0x003EB445 File Offset: 0x003E9845
			// (set) Token: 0x060097D1 RID: 38865 RVA: 0x003EB44D File Offset: 0x003E984D
			public int boneID { get; private set; }

			// Token: 0x17002027 RID: 8231
			// (get) Token: 0x060097D2 RID: 38866 RVA: 0x003EB456 File Offset: 0x003E9856
			// (set) Token: 0x060097D3 RID: 38867 RVA: 0x003EB45E File Offset: 0x003E985E
			public bool boneWeight { get; set; }

			// Token: 0x17002028 RID: 8232
			// (get) Token: 0x060097D4 RID: 38868 RVA: 0x003EB467 File Offset: 0x003E9867
			public Vector3 posision
			{
				get
				{
					return this.guideObject.transformTarget.position;
				}
			}

			// Token: 0x060097D5 RID: 38869 RVA: 0x003EB479 File Offset: 0x003E9879
			public void AddSyncBone(GameObject _gameObject)
			{
				this.syncBoneInfo = new OCIChar.SyncBoneInfo(_gameObject);
				ChangeAmount changeAmount = this.guideObject.changeAmount;
				changeAmount.onChangeRot = (Action)Delegate.Combine(changeAmount.onChangeRot, new Action(delegate()
				{
					this.syncBoneInfo.LocalRotation = this.gameObject.transform.localRotation;
				}));
			}

			// Token: 0x0400798F RID: 31119
			private GameObject m_GameObject;

			// Token: 0x04007992 RID: 31122
			private OCIChar.SyncBoneInfo syncBoneInfo;
		}

		// Token: 0x02001211 RID: 4625
		public class IKInfo
		{
			// Token: 0x060097D7 RID: 38871 RVA: 0x003EB4D0 File Offset: 0x003E98D0
			public IKInfo(GuideObject _guideObject, OIIKTargetInfo _targetInfo, Transform _base, Transform _target, Transform _bone)
			{
				this.guideObject = _guideObject;
				this.targetInfo = _targetInfo;
				this.baseObject = _base;
				this.targetObject = _target;
				this.boneObject = _bone;
			}

			// Token: 0x17002029 RID: 8233
			// (get) Token: 0x060097D8 RID: 38872 RVA: 0x003EB4FD File Offset: 0x003E98FD
			// (set) Token: 0x060097D9 RID: 38873 RVA: 0x003EB505 File Offset: 0x003E9905
			public GuideObject guideObject { get; private set; }

			// Token: 0x1700202A RID: 8234
			// (get) Token: 0x060097DA RID: 38874 RVA: 0x003EB50E File Offset: 0x003E990E
			// (set) Token: 0x060097DB RID: 38875 RVA: 0x003EB516 File Offset: 0x003E9916
			public OIIKTargetInfo targetInfo { get; private set; }

			// Token: 0x1700202B RID: 8235
			// (get) Token: 0x060097DC RID: 38876 RVA: 0x003EB51F File Offset: 0x003E991F
			// (set) Token: 0x060097DD RID: 38877 RVA: 0x003EB527 File Offset: 0x003E9927
			public Transform baseObject { get; private set; }

			// Token: 0x1700202C RID: 8236
			// (get) Token: 0x060097DE RID: 38878 RVA: 0x003EB530 File Offset: 0x003E9930
			// (set) Token: 0x060097DF RID: 38879 RVA: 0x003EB538 File Offset: 0x003E9938
			public Transform targetObject { get; private set; }

			// Token: 0x1700202D RID: 8237
			// (get) Token: 0x060097E0 RID: 38880 RVA: 0x003EB541 File Offset: 0x003E9941
			// (set) Token: 0x060097E1 RID: 38881 RVA: 0x003EB549 File Offset: 0x003E9949
			public Transform boneObject { get; private set; }

			// Token: 0x1700202E RID: 8238
			// (get) Token: 0x060097E2 RID: 38882 RVA: 0x003EB554 File Offset: 0x003E9954
			public GameObject gameObject
			{
				[CompilerGenerated]
				get
				{
					GameObject result;
					if ((result = this.m_GameObject) == null)
					{
						result = (this.m_GameObject = this.guideObject.gameObject);
					}
					return result;
				}
			}

			// Token: 0x1700202F RID: 8239
			// (get) Token: 0x060097E3 RID: 38883 RVA: 0x003EB582 File Offset: 0x003E9982
			// (set) Token: 0x060097E4 RID: 38884 RVA: 0x003EB5A6 File Offset: 0x003E99A6
			public bool active
			{
				get
				{
					return this.gameObject != null && this.gameObject.activeSelf;
				}
				set
				{
					GameObject gameObject = this.gameObject;
					if (gameObject != null)
					{
						gameObject.SetActiveIfDifferent(value);
					}
				}
			}

			// Token: 0x17002030 RID: 8240
			// (get) Token: 0x060097E5 RID: 38885 RVA: 0x003EB5BE File Offset: 0x003E99BE
			public OIBoneInfo.BoneGroup boneGroup
			{
				get
				{
					return this.targetInfo.group;
				}
			}

			// Token: 0x17002031 RID: 8241
			// (get) Token: 0x060097E6 RID: 38886 RVA: 0x003EB5CB File Offset: 0x003E99CB
			// (set) Token: 0x060097E7 RID: 38887 RVA: 0x003EB5D8 File Offset: 0x003E99D8
			public float scaleRate
			{
				get
				{
					return this.guideObject.scaleRate;
				}
				set
				{
					this.guideObject.scaleRate = value;
				}
			}

			// Token: 0x17002032 RID: 8242
			// (set) Token: 0x060097E8 RID: 38888 RVA: 0x003EB5E6 File Offset: 0x003E99E6
			public int layer
			{
				set
				{
					this.guideObject.SetLayer(this.gameObject, value);
				}
			}

			// Token: 0x060097E9 RID: 38889 RVA: 0x003EB5FC File Offset: 0x003E99FC
			public void CopyBaseValue()
			{
				this.targetObject.position = this.baseObject.position;
				this.targetObject.eulerAngles = this.baseObject.eulerAngles;
				this.guideObject.changeAmount.pos = this.targetObject.localPosition;
				this.guideObject.changeAmount.rot = ((!this.guideObject.enableRot) ? Vector3.zero : this.targetObject.localEulerAngles);
			}

			// Token: 0x060097EA RID: 38890 RVA: 0x003EB688 File Offset: 0x003E9A88
			public void CopyBone()
			{
				this.targetObject.position = this.boneObject.position;
				this.targetObject.eulerAngles = this.boneObject.eulerAngles;
				this.guideObject.changeAmount.pos = this.targetObject.localPosition;
				this.guideObject.changeAmount.rot = ((!this.guideObject.enableRot) ? Vector3.zero : this.targetObject.localEulerAngles);
			}

			// Token: 0x060097EB RID: 38891 RVA: 0x003EB714 File Offset: 0x003E9B14
			public void CopyBoneRotation()
			{
				this.targetObject.eulerAngles = this.boneObject.eulerAngles;
				this.guideObject.changeAmount.rot = ((!this.guideObject.enableRot) ? Vector3.zero : this.targetObject.localEulerAngles);
			}

			// Token: 0x04007998 RID: 31128
			private GameObject m_GameObject;
		}

		// Token: 0x02001212 RID: 4626
		public class LookAtInfo
		{
			// Token: 0x060097EC RID: 38892 RVA: 0x003EB76C File Offset: 0x003E9B6C
			public LookAtInfo(GuideObject _guideObject, LookAtTargetInfo _targetInfo)
			{
				this.guideObject = _guideObject;
				this.targetInfo = _targetInfo;
			}

			// Token: 0x17002033 RID: 8243
			// (get) Token: 0x060097ED RID: 38893 RVA: 0x003EB782 File Offset: 0x003E9B82
			// (set) Token: 0x060097EE RID: 38894 RVA: 0x003EB78A File Offset: 0x003E9B8A
			public GuideObject guideObject { get; private set; }

			// Token: 0x17002034 RID: 8244
			// (get) Token: 0x060097EF RID: 38895 RVA: 0x003EB793 File Offset: 0x003E9B93
			// (set) Token: 0x060097F0 RID: 38896 RVA: 0x003EB79B File Offset: 0x003E9B9B
			public LookAtTargetInfo targetInfo { get; private set; }

			// Token: 0x17002035 RID: 8245
			// (get) Token: 0x060097F1 RID: 38897 RVA: 0x003EB7A4 File Offset: 0x003E9BA4
			public GameObject gameObject
			{
				[CompilerGenerated]
				get
				{
					GameObject result;
					if ((result = this.m_GameObject) == null)
					{
						result = (this.m_GameObject = this.guideObject.gameObject);
					}
					return result;
				}
			}

			// Token: 0x17002036 RID: 8246
			// (get) Token: 0x060097F2 RID: 38898 RVA: 0x003EB7D2 File Offset: 0x003E9BD2
			public Transform target
			{
				get
				{
					return this.guideObject.transformTarget;
				}
			}

			// Token: 0x17002037 RID: 8247
			// (get) Token: 0x060097F3 RID: 38899 RVA: 0x003EB7DF File Offset: 0x003E9BDF
			// (set) Token: 0x060097F4 RID: 38900 RVA: 0x003EB803 File Offset: 0x003E9C03
			public bool active
			{
				get
				{
					return this.gameObject != null && this.gameObject.activeSelf;
				}
				set
				{
					GameObject gameObject = this.gameObject;
					if (gameObject != null)
					{
						gameObject.SetActiveIfDifferent(value);
					}
				}
			}

			// Token: 0x17002038 RID: 8248
			// (set) Token: 0x060097F5 RID: 38901 RVA: 0x003EB81B File Offset: 0x003E9C1B
			public int layer
			{
				set
				{
					this.guideObject.SetLayer(this.gameObject, value);
				}
			}

			// Token: 0x0400799B RID: 31131
			private GameObject m_GameObject;
		}

		// Token: 0x02001213 RID: 4627
		public class LoadedAnimeInfo
		{
			// Token: 0x060097F7 RID: 38903 RVA: 0x003EB84D File Offset: 0x003E9C4D
			public bool BaseCheck(string _bundle, string _file)
			{
				return this.baseFile.bundlePath != _bundle | this.baseFile.fileName != _file;
			}

			// Token: 0x060097F8 RID: 38904 RVA: 0x003EB872 File Offset: 0x003E9C72
			public bool OverrideCheck(string _bundle, string _file)
			{
				return this.overrideFile.bundlePath != _bundle | this.overrideFile.fileName != _file;
			}

			// Token: 0x0400799C RID: 31132
			public Info.FileInfo baseFile = new Info.FileInfo();

			// Token: 0x0400799D RID: 31133
			public Info.FileInfo overrideFile = new Info.FileInfo();
		}

		// Token: 0x02001214 RID: 4628
		public class AccessPointInfo
		{
			// Token: 0x060097F9 RID: 38905 RVA: 0x003EB897 File Offset: 0x003E9C97
			public AccessPointInfo(TreeNodeObject _root)
			{
				this.root = _root;
				this.child = new Dictionary<int, TreeNodeObject>();
			}

			// Token: 0x0400799E RID: 31134
			public TreeNodeObject root;

			// Token: 0x0400799F RID: 31135
			public Dictionary<int, TreeNodeObject> child;
		}
	}
}
