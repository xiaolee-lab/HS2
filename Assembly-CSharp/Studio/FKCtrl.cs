using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using IllusionUtility.GetUtility;
using UniRx;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011D5 RID: 4565
	public class FKCtrl : MonoBehaviour
	{
		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x060095F2 RID: 38386 RVA: 0x003DF110 File Offset: 0x003DD510
		private new Transform transform
		{
			get
			{
				if (this.m_Transform == null)
				{
					this.m_Transform = base.transform;
				}
				return this.m_Transform;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x060095F3 RID: 38387 RVA: 0x003DF135 File Offset: 0x003DD535
		// (set) Token: 0x060095F4 RID: 38388 RVA: 0x003DF13D File Offset: 0x003DD53D
		private int count { get; set; }

		// Token: 0x060095F5 RID: 38389 RVA: 0x003DF148 File Offset: 0x003DD548
		public void InitBones(OCIChar _ociChar, OICharInfo _info, ChaControl _chaControl, ChaReference _charReference)
		{
			if (_info == null)
			{
				return;
			}
			this.listBones.Clear();
			Dictionary<int, FKCtrl.TargetInfo> dictionary = new Dictionary<int, FKCtrl.TargetInfo>();
			using (Dictionary<int, Info.BoneInfo>.Enumerator enumerator = Singleton<Info>.Instance.dicBoneInfo.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FKCtrl.<InitBones>c__AnonStorey0 <InitBones>c__AnonStorey = new FKCtrl.<InitBones>c__AnonStorey0();
					<InitBones>c__AnonStorey.v = enumerator.Current;
					GameObject gameObject = null;
					bool boneWeight = true;
					switch (<InitBones>c__AnonStorey.v.Value.group)
					{
					case 7:
					case 8:
					case 9:
					{
						GameObject referenceInfo = _charReference.GetReferenceInfo(ChaReference.RefObjKey.HeadParent);
						gameObject = referenceInfo.transform.FindLoop(<InitBones>c__AnonStorey.v.Value.bone);
						break;
					}
					default:
						gameObject = this.transform.FindLoop(<InitBones>c__AnonStorey.v.Value.bone);
						if (gameObject == null)
						{
						}
						break;
					}
					if (!(gameObject == null))
					{
						FKCtrl.TargetInfo targetInfo = null;
						if (dictionary.TryGetValue(<InitBones>c__AnonStorey.v.Value.sync, out targetInfo))
						{
							targetInfo.AddSyncBone(gameObject);
						}
						else
						{
							OIBoneInfo oiboneInfo = null;
							if (_info.bones.TryGetValue(<InitBones>c__AnonStorey.v.Key, out oiboneInfo))
							{
								OIBoneInfo.BoneGroup group = OIBoneInfo.BoneGroup.Body;
								switch (<InitBones>c__AnonStorey.v.Value.group)
								{
								case 0:
								case 1:
								case 2:
								case 3:
								case 4:
									group = OIBoneInfo.BoneGroup.Body;
									break;
								case 5:
								case 6:
									goto IL_247;
								case 7:
								case 8:
								case 9:
									group = OIBoneInfo.BoneGroup.Hair;
									break;
								case 10:
									group = OIBoneInfo.BoneGroup.Neck;
									break;
								case 11:
								case 12:
									group = OIBoneInfo.BoneGroup.Breast;
									break;
								case 13:
									boneWeight = false;
									boneWeight |= this.UsedBone(_chaControl.GetCustomClothesComponent(0), gameObject.transform);
									boneWeight |= this.UsedBone(_chaControl.GetCustomClothesComponent(1), gameObject.transform);
									group = OIBoneInfo.BoneGroup.Skirt;
									_ociChar.listBones.Find((OCIChar.BoneInfo _v) => _v.boneID == <InitBones>c__AnonStorey.v.Key).SafeProc(delegate(OCIChar.BoneInfo _v)
									{
										_v.boneWeight = boneWeight;
									});
									break;
								default:
									goto IL_247;
								}
								IL_263:
								FKCtrl.TargetInfo targetInfo2 = new FKCtrl.TargetInfo(gameObject, oiboneInfo.changeAmount, group, <InitBones>c__AnonStorey.v.Value.level, boneWeight, <InitBones>c__AnonStorey.v.Key);
								this.listBones.Add(targetInfo2);
								if (<InitBones>c__AnonStorey.v.Value.sync != -1)
								{
									dictionary.Add(<InitBones>c__AnonStorey.v.Key, targetInfo2);
									continue;
								}
								continue;
								IL_247:
								group = (OIBoneInfo.BoneGroup)(1 << <InitBones>c__AnonStorey.v.Value.group);
								goto IL_263;
							}
						}
					}
				}
			}
			this.count = this.listBones.Count;
		}

		// Token: 0x060095F6 RID: 38390 RVA: 0x003DF470 File Offset: 0x003DD870
		public void CopyBone()
		{
			foreach (FKCtrl.TargetInfo targetInfo in this.listBones)
			{
				targetInfo.CopyBone();
			}
		}

		// Token: 0x060095F7 RID: 38391 RVA: 0x003DF4CC File Offset: 0x003DD8CC
		public void CopyBone(OIBoneInfo.BoneGroup _target)
		{
			foreach (FKCtrl.TargetInfo targetInfo in from l in this.listBones
			where (l.@group & _target) != (OIBoneInfo.BoneGroup)0
			select l)
			{
				targetInfo.CopyBone();
			}
		}

		// Token: 0x060095F8 RID: 38392 RVA: 0x003DF544 File Offset: 0x003DD944
		public void SetEnable(OIBoneInfo.BoneGroup _group, bool _enable)
		{
			foreach (FKCtrl.TargetInfo targetInfo in from l in this.listBones
			where (l.@group & _group) != (OIBoneInfo.BoneGroup)0
			select l)
			{
				targetInfo.enable = _enable;
			}
		}

		// Token: 0x060095F9 RID: 38393 RVA: 0x003DF5BC File Offset: 0x003DD9BC
		public void ResetUsedBone(OCIChar _ociChar)
		{
			ChaControl charInfo = _ociChar.charInfo;
			using (IEnumerator<FKCtrl.TargetInfo> enumerator = (from _v in this.listBones
			where _v.@group == OIBoneInfo.BoneGroup.Skirt
			select _v).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FKCtrl.<ResetUsedBone>c__AnonStorey4 <ResetUsedBone>c__AnonStorey = new FKCtrl.<ResetUsedBone>c__AnonStorey4();
					<ResetUsedBone>c__AnonStorey.v = enumerator.Current;
					bool boneWeight = false;
					boneWeight |= this.UsedBone(charInfo.GetCustomClothesComponent(0), <ResetUsedBone>c__AnonStorey.v.transform);
					boneWeight |= this.UsedBone(charInfo.GetCustomClothesComponent(1), <ResetUsedBone>c__AnonStorey.v.transform);
					_ociChar.listBones.Find((OCIChar.BoneInfo _v) => _v.boneID == <ResetUsedBone>c__AnonStorey.v.boneID).SafeProc(delegate(OCIChar.BoneInfo _v)
					{
						_v.boneWeight = boneWeight;
					});
					<ResetUsedBone>c__AnonStorey.v.boneWeight = boneWeight;
				}
			}
		}

		// Token: 0x060095FA RID: 38394 RVA: 0x003DF6DC File Offset: 0x003DDADC
		private bool UsedBone(CmpClothes _cmpClothes, Transform _transform)
		{
			bool flag = false;
			if (_cmpClothes == null)
			{
				return false;
			}
			flag |= this.UsedBone(_cmpClothes.rendNormal01, _transform);
			flag |= this.UsedBone(_cmpClothes.rendNormal02, _transform);
			return flag | this.UsedBone(_cmpClothes.rendNormal03, _transform);
		}

		// Token: 0x060095FB RID: 38395 RVA: 0x003DF72C File Offset: 0x003DDB2C
		private bool UsedBone(Renderer[] _renderers, Transform _transform)
		{
			if (_renderers.IsNullOrEmpty<Renderer>())
			{
				return false;
			}
			foreach (Renderer renderer in _renderers)
			{
				if (this.UsedBone(renderer as SkinnedMeshRenderer, _transform))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060095FC RID: 38396 RVA: 0x003DF778 File Offset: 0x003DDB78
		private bool UsedBone(SkinnedMeshRenderer _renderer, Transform _transform)
		{
			if (_renderer == null)
			{
				return false;
			}
			int num = Array.FindIndex<Transform>(_renderer.bones, (Transform _v) => _v == _transform);
			if (num >= 0)
			{
				Mesh sharedMesh = _renderer.sharedMesh;
				BoneWeight[] boneWeights = sharedMesh.boneWeights;
				foreach (BoneWeight boneWeight in boneWeights)
				{
					if (boneWeight.boneIndex0 == num)
					{
						return boneWeight.weight0 != 0f;
					}
					if (boneWeight.boneIndex1 == num)
					{
						return boneWeight.weight1 != 0f;
					}
					if (boneWeight.boneIndex2 == num)
					{
						return boneWeight.weight2 != 0f;
					}
					if (boneWeight.boneIndex3 == num)
					{
						return boneWeight.weight3 != 0f;
					}
				}
			}
			return false;
		}

		// Token: 0x060095FD RID: 38397 RVA: 0x003DF878 File Offset: 0x003DDC78
		private void LateUpdate()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.listBones[i].Update();
			}
		}

		// Token: 0x040078AE RID: 30894
		public static OIBoneInfo.BoneGroup[] parts = new OIBoneInfo.BoneGroup[]
		{
			OIBoneInfo.BoneGroup.Hair,
			OIBoneInfo.BoneGroup.Neck,
			OIBoneInfo.BoneGroup.Breast,
			OIBoneInfo.BoneGroup.Body,
			OIBoneInfo.BoneGroup.RightHand,
			OIBoneInfo.BoneGroup.LeftHand,
			OIBoneInfo.BoneGroup.Skirt
		};

		// Token: 0x040078AF RID: 30895
		private Transform m_Transform;

		// Token: 0x040078B0 RID: 30896
		private List<FKCtrl.TargetInfo> listBones = new List<FKCtrl.TargetInfo>();

		// Token: 0x020011D6 RID: 4566
		private class TargetInfo
		{
			// Token: 0x06009600 RID: 38400 RVA: 0x003DF8D4 File Offset: 0x003DDCD4
			public TargetInfo(GameObject _gameObject, ChangeAmount _changeAmount, OIBoneInfo.BoneGroup _group, int _level, bool _boneWeight, int _boneID)
			{
				this.gameObject = _gameObject;
				this.changeAmount = _changeAmount;
				this.group = _group;
				this.level = _level;
				this.boneWeight = _boneWeight;
				this.boneID = _boneID;
				if ((this.group & OIBoneInfo.BoneGroup.Hair) != (OIBoneInfo.BoneGroup)0 || (this.group & OIBoneInfo.BoneGroup.Skirt) != (OIBoneInfo.BoneGroup)0 || (this.group & OIBoneInfo.BoneGroup.Body) != (OIBoneInfo.BoneGroup)0)
				{
					this._enable.Subscribe(delegate(bool _b)
					{
						if (!_b)
						{
							this.transform.localRotation = Quaternion.identity;
							this.syncBoneInfo.SafeProc(delegate(OCIChar.SyncBoneInfo _sbi)
							{
								_sbi.LocalRotation = Quaternion.identity;
							});
						}
					});
				}
			}

			// Token: 0x17001FC1 RID: 8129
			// (get) Token: 0x06009601 RID: 38401 RVA: 0x003DF967 File Offset: 0x003DDD67
			public Transform transform
			{
				get
				{
					if (this.m_Transform == null)
					{
						this.m_Transform = this.gameObject.transform;
					}
					return this.m_Transform;
				}
			}

			// Token: 0x17001FC2 RID: 8130
			// (get) Token: 0x06009602 RID: 38402 RVA: 0x003DF991 File Offset: 0x003DDD91
			// (set) Token: 0x06009603 RID: 38403 RVA: 0x003DF999 File Offset: 0x003DDD99
			public OIBoneInfo.BoneGroup group { get; private set; }

			// Token: 0x17001FC3 RID: 8131
			// (get) Token: 0x06009604 RID: 38404 RVA: 0x003DF9A2 File Offset: 0x003DDDA2
			// (set) Token: 0x06009605 RID: 38405 RVA: 0x003DF9AA File Offset: 0x003DDDAA
			public int level { get; private set; }

			// Token: 0x17001FC4 RID: 8132
			// (get) Token: 0x06009606 RID: 38406 RVA: 0x003DF9B3 File Offset: 0x003DDDB3
			// (set) Token: 0x06009607 RID: 38407 RVA: 0x003DF9BB File Offset: 0x003DDDBB
			public bool boneWeight { get; set; }

			// Token: 0x17001FC5 RID: 8133
			// (get) Token: 0x06009608 RID: 38408 RVA: 0x003DF9C4 File Offset: 0x003DDDC4
			// (set) Token: 0x06009609 RID: 38409 RVA: 0x003DF9CC File Offset: 0x003DDDCC
			public int boneID { get; private set; }

			// Token: 0x17001FC6 RID: 8134
			// (get) Token: 0x0600960A RID: 38410 RVA: 0x003DF9D5 File Offset: 0x003DDDD5
			// (set) Token: 0x0600960B RID: 38411 RVA: 0x003DF9E2 File Offset: 0x003DDDE2
			public bool enable
			{
				get
				{
					return this._enable.Value;
				}
				set
				{
					this._enable.Value = value;
				}
			}

			// Token: 0x0600960C RID: 38412 RVA: 0x003DF9F0 File Offset: 0x003DDDF0
			public void CopyBone()
			{
				this.changeAmount.rot = this.transform.localEulerAngles;
			}

			// Token: 0x0600960D RID: 38413 RVA: 0x003DFA08 File Offset: 0x003DDE08
			public void AddSyncBone(GameObject _gameObject)
			{
				this.syncBoneInfo = new OCIChar.SyncBoneInfo(_gameObject);
			}

			// Token: 0x0600960E RID: 38414 RVA: 0x003DFA16 File Offset: 0x003DDE16
			public void Update()
			{
				if (this.enable)
				{
					this.transform.localRotation = Quaternion.Euler(this.changeAmount.rot);
					this.syncBoneInfo.SafeProc(delegate(OCIChar.SyncBoneInfo _sbi)
					{
						_sbi.LocalRotation = this.transform.localRotation;
					});
				}
			}

			// Token: 0x040078B3 RID: 30899
			public GameObject gameObject;

			// Token: 0x040078B4 RID: 30900
			private Transform m_Transform;

			// Token: 0x040078B5 RID: 30901
			public ChangeAmount changeAmount;

			// Token: 0x040078BA RID: 30906
			private BoolReactiveProperty _enable = new BoolReactiveProperty(true);

			// Token: 0x040078BB RID: 30907
			private OCIChar.SyncBoneInfo syncBoneInfo;
		}
	}
}
