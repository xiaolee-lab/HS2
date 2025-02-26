using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIProject;
using CharaUtils;
using FBSAssist;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using IllusionUtility.SetUtility;
using Manager;
using RootMotion.FinalIK;
using Studio;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007B2 RID: 1970
	public class ChaControl : ChaInfo
	{
		// Token: 0x06002E98 RID: 11928 RVA: 0x0010ACF4 File Offset: 0x001090F4
		public void Initialize(byte _sex, GameObject _objRoot, int _id, int _no, ChaFileControl _chaFile = null)
		{
			if (_chaFile == null || _sex != _chaFile.parameter.sex)
			{
			}
			base.MemberInitializeAll();
			this.InitializeControlLoadAll();
			this.InitializeControlFaceAll();
			this.InitializeControlBodyAll();
			this.InitializeControlCoordinateAll();
			this.InitializeControlAccessoryAll();
			this.InitializeControlCustomBodyAll();
			this.InitializeControlCustomFaceAll();
			this.InitializeControlCustomHairAll();
			this.InitializeControlHandAll();
			base.objRoot = _objRoot;
			base.chaID = _id;
			base.loadNo = _no;
			base.hideMoz = false;
			base.lstCtrl = Singleton<Character>.Instance.chaListCtrl;
			if (_chaFile == null)
			{
				base.chaFile = new ChaFileControl();
				this.LoadPreset((int)_sex, string.Empty);
			}
			else
			{
				base.chaFile = _chaFile;
			}
			base.chaFile.parameter.sex = _sex;
			this.InitBaseCustomTextureBody();
			this.ChangeNowCoordinate(false, true);
			if (_sex == 0)
			{
				base.chaFile.custom.body.shapeValueBody[0] = 0.75f;
			}
			if (_sex == 1)
			{
				base.chaFile.status.visibleSonAlways = false;
			}
			else
			{
				base.chaFile.status.visibleSon = false;
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0010AE1F File Offset: 0x0010921F
		public void ReleaseAll()
		{
			this.ReleaseControlLoadAll();
			this.ReleaseControlFaceAll();
			this.ReleaseControlBodyAll();
			this.ReleaseControlCoordinateAll();
			this.ReleaseControlAccessoryAll();
			this.ReleaseControlCustomBodyAll();
			this.ReleaseControlCustomFaceAll();
			this.ReleaseControlCustomHairAll();
			this.ReleaseControlHandAll();
			base.ReleaseInfoAll();
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0010AE60 File Offset: 0x00109260
		public void ReleaseObject()
		{
			this.ReleaseControlLoadObject(true);
			this.ReleaseControlFaceObject(true);
			this.ReleaseControlBodyObject(true);
			this.ReleaseControlCoordinateObject(true);
			this.ReleaseControlAccessoryObject(true);
			this.ReleaseControlCustomBodyObject(true);
			this.ReleaseControlCustomFaceObject(true);
			this.ReleaseControlCustomHairObject(true);
			this.ReleaseControlHandObject(true);
			base.ReleaseInfoObject(true);
			if (Singleton<Character>.Instance.enableCharaLoadGCClear)
			{
				UnityEngine.Resources.UnloadUnusedAssets();
				GC.Collect();
			}
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0010AED0 File Offset: 0x001092D0
		public void LoadPreset(int _sex, string presetName = "")
		{
			string text = string.Empty;
			if (presetName.IsNullOrEmpty())
			{
				text = ((_sex != 0) ? "ill_Default_Female" : "ill_Default_Male");
			}
			else
			{
				text = presetName;
			}
			Dictionary<int, ListInfoBase> categoryInfo;
			if (_sex == 0)
			{
				categoryInfo = base.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_m);
			}
			else
			{
				categoryInfo = base.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_f);
			}
			foreach (KeyValuePair<int, ListInfoBase> keyValuePair in categoryInfo)
			{
				if (keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainData) == text)
				{
					base.chaFile.LoadFromAssetBundle(keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainAB), text, false, true);
					break;
				}
			}
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x0010AFB0 File Offset: 0x001093B0
		public static ChaFileControl[] GetRandomFemaleCard(int num)
		{
			FolderAssist folderAssist = new FolderAssist();
			string[] searchPattern = new string[]
			{
				"*.png"
			};
			string folder = UserData.Path + "chara/female/";
			folderAssist.CreateFolderInfoEx(folder, searchPattern, true);
			List<string> list = (from n in folderAssist.lstFile.Shuffle<FolderAssist.FileInfo>()
			select n.FullPath).ToList<string>();
			int num2 = Mathf.Min(list.Count, num);
			if (num2 == 0)
			{
				return null;
			}
			List<ChaFileControl> list2 = new List<ChaFileControl>();
			for (int i = 0; i < num2; i++)
			{
				ChaFileControl chaFileControl = new ChaFileControl();
				if (chaFileControl.LoadCharaFile(list[i], 1, true, true))
				{
					if (chaFileControl.parameter.sex != 0)
					{
						list2.Add(chaFileControl);
					}
				}
			}
			return list2.ToArray();
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0010B09D File Offset: 0x0010949D
		public void SetActiveTop(bool active)
		{
			if (null != base.objTop)
			{
				base.objTop.SetActiveIfDifferent(active);
			}
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x0010B0BD File Offset: 0x001094BD
		public bool GetActiveTop()
		{
			return null != base.objTop && base.objTop.activeSelf;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x0010B0DD File Offset: 0x001094DD
		public void SetPosition(float x, float y, float z)
		{
			if (null != base.objTop)
			{
				base.objTop.transform.localPosition = new Vector3(x, y, z);
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x0010B108 File Offset: 0x00109508
		public void SetPosition(Vector3 pos)
		{
			if (null != base.objTop)
			{
				base.objTop.transform.localPosition = pos;
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x0010B12C File Offset: 0x0010952C
		public Vector3 GetPosition()
		{
			return (!(null == base.objTop)) ? base.objTop.transform.localPosition : Vector3.zero;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0010B159 File Offset: 0x00109559
		public void SetRotation(float x, float y, float z)
		{
			if (null != base.objTop)
			{
				base.objTop.transform.localRotation = Quaternion.Euler(x, y, z);
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0010B184 File Offset: 0x00109584
		public void SetRotation(Vector3 rot)
		{
			if (null != base.objTop)
			{
				base.objTop.transform.localRotation = Quaternion.Euler(rot);
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x0010B1AD File Offset: 0x001095AD
		public void SetRotation(Quaternion rot)
		{
			if (null != base.objTop)
			{
				base.objTop.transform.localRotation = rot;
			}
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x0010B1D4 File Offset: 0x001095D4
		public Vector3 GetRotation()
		{
			return (!(null == base.objTop)) ? base.objTop.transform.localRotation.eulerAngles : Vector3.zero;
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0010B214 File Offset: 0x00109614
		public void SetTransform(Transform trf)
		{
			if (null != base.objTop)
			{
				base.objTop.transform.localPosition = trf.localPosition;
				base.objTop.transform.localRotation = trf.localRotation;
				base.objTop.transform.localScale = trf.localScale;
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0010B274 File Offset: 0x00109674
		public void ChangeSettingMannequin(bool mannequin)
		{
			if (mannequin)
			{
				if (this.mannequinBackInfo.mannequin)
				{
					return;
				}
				this.mannequinBackInfo.mannequin = true;
				this.mannequinBackInfo.Backup(this);
				string assetBundleName = ChaABDefine.PresetAssetBundle((int)base.sex);
				string assetName = ChaABDefine.PresetAsset((int)base.sex);
				base.chaFile.LoadMannequinFile(assetBundleName, assetName, true, true, true, false, false);
				this.Reload(true, false, false, false, true);
				this.ChangeEyesPtn(0, false);
				this.ChangeEyesOpenMax(0f);
				this.ChangeMouthPtn(0, false);
				base.fileStatus.mouthFixed = true;
				this.ChangeMouthOpenMax(0f);
				base.neckLookCtrl.neckLookScript.skipCalc = true;
				base.neckLookCtrl.ForceLateUpdate();
				base.eyeLookCtrl.ForceLateUpdate();
				base.resetDynamicBoneAll = true;
				this.LateUpdateForce();
			}
			else
			{
				if (!this.mannequinBackInfo.mannequin)
				{
					return;
				}
				this.mannequinBackInfo.Restore(this);
				this.mannequinBackInfo.mannequin = false;
			}
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0010B37C File Offset: 0x0010977C
		public void RestoreMannequinHair()
		{
			ChaFileControl chaFileControl = new ChaFileControl();
			chaFileControl.SetCustomBytes(this.mannequinBackInfo.custom, ChaFileDefine.ChaFileCustomVersion);
			base.fileCustom.hair = chaFileControl.custom.hair;
			this.Reload(true, true, false, true, true);
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x0010B3C8 File Offset: 0x001097C8
		public bool IsVisibleInCamera
		{
			get
			{
				if (null != base.cmpBody && base.cmpBody.isVisible)
				{
					return true;
				}
				if (null != base.cmpFace && base.cmpFace.isVisible)
				{
					return true;
				}
				if (base.cmpHair != null)
				{
					for (int i = 0; i < base.cmpHair.Length; i++)
					{
						if (!(null == base.cmpHair[i]))
						{
							if (base.cmpHair[i].isVisible)
							{
								return true;
							}
						}
					}
				}
				if (base.cmpClothes != null)
				{
					for (int j = 0; j < base.cmpClothes.Length; j++)
					{
						if (!(null == base.cmpClothes[j]))
						{
							if (base.cmpClothes[j].isVisible)
							{
								return true;
							}
						}
					}
				}
				if (base.cmpAccessory != null)
				{
					for (int k = 0; k < base.cmpAccessory.Length; k++)
					{
						if (!(null == base.cmpAccessory[k]))
						{
							if (base.cmpAccessory[k].isVisible)
							{
								return true;
							}
						}
					}
				}
				if (base.cmpExtraAccessory != null)
				{
					for (int l = 0; l < base.cmpExtraAccessory.Length; l++)
					{
						if (!(null == base.cmpExtraAccessory[l]))
						{
							if (base.cmpExtraAccessory[l].isVisible)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0010B55C File Offset: 0x0010995C
		public void OnDestroy()
		{
			if (Singleton<Character>.IsInstance())
			{
				Singleton<Character>.Instance.DeleteChara(this, true);
			}
			this.ReleaseAll();
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0010B57B File Offset: 0x0010997B
		public void UpdateForce()
		{
			if (!base.loadEnd)
			{
				return;
			}
			this.UpdateBlendShapeVoice();
			this.UpdateSiru(false);
			if (base.updateWet)
			{
				this.UpdateWet();
				base.updateWet = false;
			}
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0010B5B0 File Offset: 0x001099B0
		public void LateUpdateForce()
		{
			if (!base.loadEnd)
			{
				return;
			}
			this.UpdateVisible();
			if (base.resetDynamicBoneAll)
			{
				this.ResetDynamicBoneAll(false);
				base.resetDynamicBoneAll = false;
			}
			if (base.updateShapeBody)
			{
				this.UpdateShapeBody();
				base.updateShapeBody = false;
			}
			if (base.updateShapeFace)
			{
				this.UpdateShapeFace();
				base.updateShapeFace = false;
			}
			this.UpdateAlwaysShapeBody();
			this.UpdateAlwaysShapeHand();
			if (null != base.cmpBoneBody && null != base.cmpBoneBody.targetEtc.trfAnaCorrect && null != base.cmpBoneBody.targetAccessory.acs_Ana)
			{
				base.cmpBoneBody.targetAccessory.acs_Ana.localScale = new Vector3(1f / base.cmpBoneBody.targetEtc.trfAnaCorrect.localScale.x, 1f / base.cmpBoneBody.targetEtc.trfAnaCorrect.localScale.y, 1f / base.cmpBoneBody.targetEtc.trfAnaCorrect.localScale.z);
			}
			if (base.reSetupDynamicBoneBust)
			{
				this.ReSetupDynamicBoneBust(0);
				this.UpdateBustSoftnessAndGravity();
				base.reSetupDynamicBoneBust = false;
			}
			if (base.updateBustSize && base.sex == 1)
			{
				int num = 1;
				float rate = 1f;
				if (0.5f > base.chaFile.custom.body.shapeValueBody[num])
				{
					rate = Mathf.InverseLerp(0f, 0.5f, base.chaFile.custom.body.shapeValueBody[num]);
				}
				if (this.bustNormal != null)
				{
					this.bustNormal.Blend(rate);
				}
				base.updateBustSize = false;
			}
			if (!this.IsVisibleInCamera)
			{
				if (null != base.cmpBoneBody)
				{
					for (int i = 0; i < base.enableDynamicBoneBustAndHip.Length; i++)
					{
						base.cmpBoneBody.EnableDynamicBonesBustAndHip(false, i);
					}
				}
				if (base.cmpHair != null)
				{
					for (int j = 0; j < base.cmpHair.Length; j++)
					{
						if (null != base.cmpHair[j])
						{
							base.cmpHair[j].EnableDynamicBonesHair(false, null);
						}
					}
				}
				if (base.cmpClothes != null)
				{
					for (int k = 0; k < base.cmpClothes.Length; k++)
					{
						if (null != base.cmpClothes[k])
						{
							base.cmpClothes[k].EnableDynamicBones(false);
						}
					}
				}
				if (base.cmpAccessory != null)
				{
					for (int l = 0; l < base.cmpAccessory.Length; l++)
					{
						if (null != base.cmpAccessory[l])
						{
							base.cmpAccessory[l].EnableDynamicBones(false);
						}
					}
				}
				if (base.cmpExtraAccessory != null)
				{
					for (int m = 0; m < base.cmpExtraAccessory.Length; m++)
					{
						if (null != base.cmpExtraAccessory[m])
						{
							base.cmpExtraAccessory[m].EnableDynamicBones(false);
						}
					}
				}
			}
			else
			{
				if (null != base.cmpBoneBody)
				{
					for (int n = 0; n < base.enableDynamicBoneBustAndHip.Length; n++)
					{
						base.cmpBoneBody.EnableDynamicBonesBustAndHip(base.cmpBody.isVisible && base.enableDynamicBoneBustAndHip[n], n);
					}
				}
				if (base.cmpHair != null)
				{
					for (int num2 = 0; num2 < base.cmpHair.Length; num2++)
					{
						if (null != base.cmpHair[num2])
						{
							base.cmpHair[num2].EnableDynamicBonesHair(base.cmpHair[num2].isVisible, base.fileHair.parts[num2]);
						}
					}
				}
				if (base.cmpClothes != null)
				{
					for (int num3 = 0; num3 < base.cmpClothes.Length; num3++)
					{
						if (null != base.cmpClothes[num3])
						{
							base.cmpClothes[num3].EnableDynamicBones(base.fileStatus.clothesState[num3] == 0 && base.cmpClothes[num3].isVisible);
						}
					}
				}
				if (base.cmpAccessory != null)
				{
					for (int num4 = 0; num4 < base.cmpAccessory.Length; num4++)
					{
						if (null != base.cmpAccessory[num4])
						{
							base.cmpAccessory[num4].EnableDynamicBones(!this.nowCoordinate.accessory.parts[num4].noShake && base.cmpAccessory[num4].isVisible);
						}
					}
				}
				if (base.cmpExtraAccessory != null)
				{
					for (int num5 = 0; num5 < base.cmpExtraAccessory.Length; num5++)
					{
						if (null != base.cmpExtraAccessory[num5])
						{
							base.cmpExtraAccessory[num5].EnableDynamicBones(base.cmpExtraAccessory[num5].isVisible);
						}
					}
				}
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x0010BB0A File Offset: 0x00109F0A
		// (set) Token: 0x06002EAE RID: 11950 RVA: 0x0010BB12 File Offset: 0x00109F12
		public bool[] hideHairAcs { get; private set; }

		// Token: 0x06002EAF RID: 11951 RVA: 0x0010BB1B File Offset: 0x00109F1B
		protected void InitializeControlAccessoryAll()
		{
			this.InitializeControlAccessoryObject();
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x0010BB23 File Offset: 0x00109F23
		protected void InitializeControlAccessoryObject()
		{
			this.hideHairAcs = new bool[20];
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x0010BB32 File Offset: 0x00109F32
		protected void ReleaseControlAccessoryAll()
		{
			this.ReleaseControlAccessoryObject(false);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x0010BB3B File Offset: 0x00109F3B
		protected void ReleaseControlAccessoryObject(bool init = true)
		{
			if (init)
			{
				this.InitializeControlAccessoryObject();
			}
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x0010BB49 File Offset: 0x00109F49
		public bool IsAccessory(int slotNo)
		{
			return MathfEx.RangeEqualOn<int>(0, slotNo, 19) && !(null == base.objAccessory[slotNo]);
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x0010BB75 File Offset: 0x00109F75
		public void SetAccessoryState(int slotNo, bool show)
		{
			if (base.fileStatus.showAccessory.Length <= slotNo)
			{
				return;
			}
			base.fileStatus.showAccessory[slotNo] = show;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x0010BB9C File Offset: 0x00109F9C
		public void SetAccessoryStateAll(bool show)
		{
			for (int i = 0; i < base.fileStatus.showAccessory.Length; i++)
			{
				base.fileStatus.showAccessory[i] = show;
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0010BBD8 File Offset: 0x00109FD8
		public string GetAccessoryDefaultParentStr(int type, int id)
		{
			int num = Enum.GetNames(typeof(ChaListDefine.CategoryNo)).Length;
			if (!MathfEx.RangeEqualOn<int>(0, type, num - 1))
			{
				return string.Empty;
			}
			ListInfoBase listInfoBase = null;
			Dictionary<int, ListInfoBase> categoryInfo = base.lstCtrl.GetCategoryInfo((ChaListDefine.CategoryNo)type);
			if (!categoryInfo.TryGetValue(id, out listInfoBase))
			{
				return string.Empty;
			}
			return listInfoBase.GetInfo(ChaListDefine.KeyType.Parent);
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0010BC3C File Offset: 0x0010A03C
		public string GetAccessoryDefaultParentStr(int slotNo)
		{
			GameObject gameObject = base.objAccessory[slotNo];
			if (null == gameObject)
			{
				return string.Empty;
			}
			ListInfoComponent component = gameObject.GetComponent<ListInfoComponent>();
			return component.data.GetInfo(ChaListDefine.KeyType.Parent);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0010BC78 File Offset: 0x0010A078
		public bool ChangeAccessoryParent(int slotNo, string parentStr)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			GameObject gameObject = base.objAccessory[slotNo];
			if (null == gameObject)
			{
				return false;
			}
			if ("none" == parentStr)
			{
				gameObject.transform.SetParent(null, false);
				return true;
			}
			ListInfoComponent component = gameObject.GetComponent<ListInfoComponent>();
			ListInfoBase data = component.data;
			if ("0" == data.GetInfo(ChaListDefine.KeyType.Parent))
			{
				return false;
			}
			try
			{
				Transform accessoryParentTransform = base.GetAccessoryParentTransform(parentStr);
				if (null == accessoryParentTransform)
				{
					return false;
				}
				gameObject.transform.SetParent(accessoryParentTransform, false);
				this.nowCoordinate.accessory.parts[slotNo].parentKey = parentStr;
				this.nowCoordinate.accessory.parts[slotNo].partsOfHead = ChaAccessoryDefine.CheckPartsOfHead(parentStr);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0010BD74 File Offset: 0x0010A174
		public bool SetAccessoryPos(int slotNo, int correctNo, float value, bool add, int flags = 7)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			Transform transform = base.trfAcsMove[slotNo, correctNo];
			if (null == transform)
			{
				return false;
			}
			ChaFileAccessory accessory = this.nowCoordinate.accessory;
			if ((flags & 1) != 0)
			{
				float value2 = float.Parse((((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 0].x) + value).ToString("f1"));
				accessory.parts[slotNo].addMove[correctNo, 0].x = Mathf.Clamp(value2, -100f, 100f);
			}
			if ((flags & 2) != 0)
			{
				float value3 = float.Parse((((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 0].y) + value).ToString("f1"));
				accessory.parts[slotNo].addMove[correctNo, 0].y = Mathf.Clamp(value3, -100f, 100f);
			}
			if ((flags & 4) != 0)
			{
				float value4 = float.Parse((((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 0].z) + value).ToString("f1"));
				accessory.parts[slotNo].addMove[correctNo, 0].z = Mathf.Clamp(value4, -100f, 100f);
			}
			transform.localPosition = new Vector3(accessory.parts[slotNo].addMove[correctNo, 0].x * 0.1f, accessory.parts[slotNo].addMove[correctNo, 0].y * 0.1f, accessory.parts[slotNo].addMove[correctNo, 0].z * 0.1f);
			return true;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0010BF78 File Offset: 0x0010A378
		public bool SetAccessoryRot(int slotNo, int correctNo, float value, bool add, int flags = 7)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			Transform transform = base.trfAcsMove[slotNo, correctNo];
			if (null == transform)
			{
				return false;
			}
			ChaFileAccessory accessory = this.nowCoordinate.accessory;
			if ((flags & 1) != 0)
			{
				float t = (float)((int)(((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 1].x) + value));
				accessory.parts[slotNo].addMove[correctNo, 1].x = Mathf.Repeat(t, 360f);
			}
			if ((flags & 2) != 0)
			{
				float t2 = (float)((int)(((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 1].y) + value));
				accessory.parts[slotNo].addMove[correctNo, 1].y = Mathf.Repeat(t2, 360f);
			}
			if ((flags & 4) != 0)
			{
				float t3 = (float)((int)(((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 1].z) + value));
				accessory.parts[slotNo].addMove[correctNo, 1].z = Mathf.Repeat(t3, 360f);
			}
			transform.localEulerAngles = new Vector3(accessory.parts[slotNo].addMove[correctNo, 1].x, accessory.parts[slotNo].addMove[correctNo, 1].y, accessory.parts[slotNo].addMove[correctNo, 1].z);
			return true;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x0010C12C File Offset: 0x0010A52C
		public bool SetAccessoryScl(int slotNo, int correctNo, float value, bool add, int flags = 7)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			Transform transform = base.trfAcsMove[slotNo, correctNo];
			if (null == transform)
			{
				return false;
			}
			ChaFileAccessory accessory = this.nowCoordinate.accessory;
			if ((flags & 1) != 0)
			{
				float value2 = float.Parse((((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 2].x) + value).ToString("f2"));
				accessory.parts[slotNo].addMove[correctNo, 2].x = Mathf.Clamp(value2, 0.01f, 100f);
			}
			if ((flags & 2) != 0)
			{
				float value3 = float.Parse((((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 2].y) + value).ToString("f2"));
				accessory.parts[slotNo].addMove[correctNo, 2].y = Mathf.Clamp(value3, 0.01f, 100f);
			}
			if ((flags & 4) != 0)
			{
				float value4 = float.Parse((((!add) ? 0f : accessory.parts[slotNo].addMove[correctNo, 2].z) + value).ToString("f2"));
				accessory.parts[slotNo].addMove[correctNo, 2].z = Mathf.Clamp(value4, 0.01f, 100f);
			}
			transform.localScale = new Vector3(accessory.parts[slotNo].addMove[correctNo, 2].x, accessory.parts[slotNo].addMove[correctNo, 2].y, accessory.parts[slotNo].addMove[correctNo, 2].z);
			return true;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0010C320 File Offset: 0x0010A720
		public bool ResetAccessoryMove(int slotNo, int correctNo, int type = 7)
		{
			bool flag = true;
			if ((type & 1) != 0)
			{
				flag &= this.SetAccessoryPos(slotNo, correctNo, 0f, false, 7);
			}
			if ((type & 2) != 0)
			{
				flag &= this.SetAccessoryRot(slotNo, correctNo, 0f, false, 7);
			}
			if ((type & 4) != 0)
			{
				flag &= this.SetAccessoryScl(slotNo, correctNo, 1f, false, 7);
			}
			return flag;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0010C380 File Offset: 0x0010A780
		public bool UpdateAccessoryMoveFromInfo(int slotNo)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			ChaFileAccessory accessory = this.nowCoordinate.accessory;
			for (int i = 0; i < 2; i++)
			{
				Transform transform = base.trfAcsMove[slotNo, i];
				if (!(null == transform))
				{
					transform.localPosition = new Vector3(accessory.parts[slotNo].addMove[i, 0].x * 0.1f, accessory.parts[slotNo].addMove[i, 0].y * 0.1f, accessory.parts[slotNo].addMove[i, 0].z * 0.1f);
					transform.localEulerAngles = new Vector3(accessory.parts[slotNo].addMove[i, 1].x, accessory.parts[slotNo].addMove[i, 1].y, accessory.parts[slotNo].addMove[i, 1].z);
					transform.localScale = new Vector3(accessory.parts[slotNo].addMove[i, 2].x, accessory.parts[slotNo].addMove[i, 2].y, accessory.parts[slotNo].addMove[i, 2].z);
				}
			}
			return true;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0010C4F0 File Offset: 0x0010A8F0
		public bool UpdateAccessoryMoveAllFromInfo()
		{
			for (int i = 0; i < 20; i++)
			{
				this.UpdateAccessoryMoveFromInfo(i);
			}
			return true;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0010C51C File Offset: 0x0010A91C
		public bool ChangeAccessoryColor(int slotNo)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			CmpAccessory cmpAccessory = base.cmpAccessory[slotNo];
			ChaFileAccessory.PartsInfo partsInfo = this.nowCoordinate.accessory.parts[slotNo];
			if (null == cmpAccessory)
			{
				return false;
			}
			if (cmpAccessory.rendNormal != null)
			{
				foreach (Renderer renderer in cmpAccessory.rendNormal)
				{
					if (cmpAccessory.useColor01)
					{
						renderer.material.SetColor(ChaShader.Color, partsInfo.colorInfo[0].color);
						renderer.material.SetFloat(ChaShader.ClothesGloss1, partsInfo.colorInfo[0].glossPower);
						renderer.material.SetFloat(ChaShader.Metallic, partsInfo.colorInfo[0].metallicPower);
					}
					if (cmpAccessory.useColor02)
					{
						renderer.material.SetColor(ChaShader.Color2, partsInfo.colorInfo[1].color);
						renderer.material.SetFloat(ChaShader.ClothesGloss2, partsInfo.colorInfo[1].glossPower);
						renderer.material.SetFloat(ChaShader.Metallic2, partsInfo.colorInfo[1].metallicPower);
					}
					if (cmpAccessory.useColor03)
					{
						renderer.material.SetColor(ChaShader.Color3, partsInfo.colorInfo[2].color);
						renderer.material.SetFloat(ChaShader.ClothesGloss3, partsInfo.colorInfo[2].glossPower);
						renderer.material.SetFloat(ChaShader.Metallic3, partsInfo.colorInfo[2].metallicPower);
					}
				}
			}
			if (cmpAccessory.rendAlpha != null)
			{
				foreach (Renderer renderer2 in cmpAccessory.rendAlpha)
				{
					renderer2.material.SetColor(ChaShader.Color, partsInfo.colorInfo[3].color);
					renderer2.material.SetFloat(ChaShader.ClothesGloss4, partsInfo.colorInfo[3].glossPower);
					renderer2.material.SetFloat(ChaShader.Metallic4, partsInfo.colorInfo[3].metallicPower);
					renderer2.gameObject.SetActiveIfDifferent(partsInfo.colorInfo[3].color.a != 0f);
				}
			}
			return true;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0010C77C File Offset: 0x0010AB7C
		public bool GetAccessoryDefaultColor(ref Color color, ref float gloss, ref float metallic, int slotNo, int no)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			CmpAccessory cmpAccessory = base.cmpAccessory[slotNo];
			if (null == cmpAccessory)
			{
				return false;
			}
			if (no == 0 && cmpAccessory.useColor01)
			{
				color = cmpAccessory.defColor01;
				gloss = cmpAccessory.defGlossPower01;
				metallic = cmpAccessory.defMetallicPower01;
				return true;
			}
			if (no == 1 && cmpAccessory.useColor02)
			{
				color = cmpAccessory.defColor02;
				gloss = cmpAccessory.defGlossPower02;
				metallic = cmpAccessory.defMetallicPower02;
				return true;
			}
			if (no == 2 && cmpAccessory.useColor03)
			{
				color = cmpAccessory.defColor03;
				gloss = cmpAccessory.defGlossPower03;
				metallic = cmpAccessory.defMetallicPower03;
				return true;
			}
			if (no == 3 && cmpAccessory.rendAlpha != null && cmpAccessory.rendAlpha.Length != 0)
			{
				color = cmpAccessory.defColor04;
				gloss = cmpAccessory.defGlossPower04;
				metallic = cmpAccessory.defMetallicPower04;
				return true;
			}
			return false;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0010C884 File Offset: 0x0010AC84
		public void SetAccessoryDefaultColor(int slotNo)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return;
			}
			CmpAccessory cmpAccessory = base.cmpAccessory[slotNo];
			if (null == cmpAccessory)
			{
				return;
			}
			if (cmpAccessory.useColor01)
			{
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[0].color = cmpAccessory.defColor01;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[0].glossPower = cmpAccessory.defGlossPower01;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[0].metallicPower = cmpAccessory.defMetallicPower01;
			}
			if (cmpAccessory.useColor02)
			{
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[1].color = cmpAccessory.defColor02;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[1].glossPower = cmpAccessory.defGlossPower02;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[1].metallicPower = cmpAccessory.defMetallicPower02;
			}
			if (cmpAccessory.useColor03)
			{
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[2].color = cmpAccessory.defColor03;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[2].glossPower = cmpAccessory.defGlossPower03;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[2].metallicPower = cmpAccessory.defMetallicPower03;
			}
			if (cmpAccessory.rendAlpha != null && cmpAccessory.rendAlpha.Length != 0)
			{
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[3].color = cmpAccessory.defColor04;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[3].glossPower = cmpAccessory.defGlossPower04;
				this.nowCoordinate.accessory.parts[slotNo].colorInfo[3].metallicPower = cmpAccessory.defMetallicPower04;
			}
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0010CAA0 File Offset: 0x0010AEA0
		public void ResetDynamicBoneAccessories(bool includeInactive = false)
		{
			if (base.cmpAccessory == null)
			{
				return;
			}
			for (int i = 0; i < base.cmpAccessory.Length; i++)
			{
				if (!(null == base.cmpAccessory[i]))
				{
					base.cmpAccessory[i].ResetDynamicBones(includeInactive);
				}
			}
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0010CAF8 File Offset: 0x0010AEF8
		public bool ChangeHairTypeAccessoryColor(int slotNo)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return false;
			}
			CmpAccessory cmpAccessory = base.cmpAccessory[slotNo];
			ChaFileAccessory.PartsInfo partsInfo = this.nowCoordinate.accessory.parts[slotNo];
			if (null == cmpAccessory)
			{
				return false;
			}
			if (cmpAccessory.rendNormal != null)
			{
				foreach (Renderer renderer in cmpAccessory.rendNormal)
				{
					if (cmpAccessory.useColor01)
					{
						renderer.material.SetColor(ChaShader.HairMainColor, partsInfo.colorInfo[0].color);
					}
					if (cmpAccessory.useColor02)
					{
						renderer.material.SetColor(ChaShader.HairTopColor, partsInfo.colorInfo[1].color);
					}
					if (cmpAccessory.useColor03)
					{
						renderer.material.SetColor(ChaShader.HairUnderColor, partsInfo.colorInfo[2].color);
					}
					renderer.material.SetColor(ChaShader.Specular, partsInfo.colorInfo[3].color);
					renderer.material.SetFloat(ChaShader.Smoothness, partsInfo.colorInfo[0].smoothnessPower);
					renderer.material.SetFloat(ChaShader.Metallic, partsInfo.colorInfo[0].metallicPower);
				}
			}
			return true;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0010CC40 File Offset: 0x0010B040
		public void ChangeSettingHairTypeAccessoryShaderAll()
		{
			for (int i = 0; i < 20; i++)
			{
				this.ChangeSettingHairTypeAccessoryShader(i);
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x0010CC68 File Offset: 0x0010B068
		public void ChangeSettingHairTypeAccessoryShader(int slotNo)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				return;
			}
			CmpAccessory cmpAccessory = base.cmpAccessory[slotNo];
			if (null == cmpAccessory)
			{
				return;
			}
			ChaFileAccessory.PartsInfo partsInfo = this.nowCoordinate.accessory.parts[slotNo];
			if (!cmpAccessory.typeHair)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			Shader shader = (hair.shaderType != 0) ? Singleton<Character>.Instance.shaderCutout : Singleton<Character>.Instance.shaderDithering;
			if (base.infoAccessory[slotNo] != null)
			{
				string info = base.infoAccessory[slotNo].GetInfo(ChaListDefine.KeyType.TexManifest);
				string info2 = base.infoAccessory[slotNo].GetInfo(ChaListDefine.KeyType.TexAB);
				string info3 = base.infoAccessory[slotNo].GetInfo((hair.shaderType != 0) ? ChaListDefine.KeyType.TexC : ChaListDefine.KeyType.TexD);
				Texture2D value = CommonLib.LoadAsset<Texture2D>(info2, info3, false, info);
				Singleton<Character>.Instance.AddLoadAssetBundle(info2, info);
				for (int i = 0; i < cmpAccessory.rendNormal.Length; i++)
				{
					for (int j = 0; j < cmpAccessory.rendNormal[i].materials.Length; j++)
					{
						int renderQueue = cmpAccessory.rendNormal[i].materials[j].renderQueue;
						cmpAccessory.rendNormal[i].materials[j].shader = shader;
						cmpAccessory.rendNormal[i].materials[j].SetTexture(ChaShader.MainTex, value);
						cmpAccessory.rendNormal[i].materials[j].renderQueue = renderQueue;
					}
				}
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0010CE06 File Offset: 0x0010B206
		public void ChangeExtraAccessory(ChaControlDefine.ExtraAccessoryParts parts, int id)
		{
			base.StartCoroutine(this.ChangeExtraAccessory(parts, id, false));
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0010CE18 File Offset: 0x0010B218
		public IEnumerator ChangeExtraAccessory(ChaControlDefine.ExtraAccessoryParts parts, int id, bool asyncFlags = true)
		{
			if (!MathfEx.RangeEqualOn<int>(0, (int)parts, Enum.GetNames(typeof(ChaControlDefine.ExtraAccessoryParts)).Length - 1))
			{
				yield break;
			}
			if (id == -1)
			{
				if (null != base.objExtraAccessory[(int)parts])
				{
					base.cmpExtraAccessory[(int)parts] = null;
					base.SafeDestroy(base.objExtraAccessory[(int)parts]);
					base.objExtraAccessory[(int)parts] = null;
				}
				yield break;
			}
			if (asyncFlags)
			{
				yield return null;
			}
			if (null != base.objExtraAccessory[(int)parts])
			{
				base.cmpExtraAccessory[(int)parts] = null;
				base.SafeDestroy(base.objExtraAccessory[(int)parts]);
				base.objExtraAccessory[(int)parts] = null;
			}
			Dictionary<int, AccessoryItemInfo> dic;
			AccessoryItemInfo item;
			if (!Singleton<Manager.Resources>.Instance.GameInfo.AccessoryItem.TryGetValue(10, out dic) || !dic.TryGetValue(id, out item))
			{
				yield break;
			}
			string manifestName = item.Manifest;
			string assetBundleName = item.AssetBundle;
			string assetName = item.Asset;
			GameObject newObj = null;
			if (asyncFlags)
			{
				yield return base.StartCoroutine(base.Load_Coroutine<GameObject>(assetBundleName, assetName, delegate(GameObject x)
				{
					newObj = x;
				}, true, manifestName));
			}
			else
			{
				newObj = CommonLib.LoadAsset<GameObject>(assetBundleName, assetName, true, manifestName);
			}
			Singleton<Character>.Instance.AddLoadAssetBundle(assetBundleName, manifestName);
			if (null == newObj)
			{
				yield break;
			}
			newObj.name = ChaControlDefine.extraAcsNames[(int)parts];
			string parentName = item.ParentName;
			Transform trfParent = base.GetAccessoryParentTransform(parentName);
			newObj.transform.SetParent(trfParent, false);
			DynamicBoneCollider[] dbc = base.objBodyBone.GetComponentsInChildren<DynamicBoneCollider>(true);
			Dictionary<string, GameObject> dictBone = this.aaWeightsBody.dictBone;
			DynamicBone[] db = newObj.GetComponentsInChildren<DynamicBone>(true);
			GameObject obj = null;
			foreach (DynamicBone dynamicBone in db)
			{
				if (null != dynamicBone.m_Root && dictBone.TryGetValue(dynamicBone.m_Root.name, out obj))
				{
					dynamicBone.m_Root = obj.transform;
				}
				if (dynamicBone.m_Exclusions != null && dynamicBone.m_Exclusions.Count != 0)
				{
					for (int j = 0; j < dynamicBone.m_Exclusions.Count; j++)
					{
						if (!(null == dynamicBone.m_Exclusions[j]))
						{
							if (dictBone.TryGetValue(dynamicBone.m_Exclusions[j].name, out obj))
							{
								dynamicBone.m_Exclusions[j] = obj.transform;
							}
						}
					}
				}
				if (dynamicBone.m_notRolls != null && dynamicBone.m_notRolls.Count != 0)
				{
					for (int k = 0; k < dynamicBone.m_notRolls.Count; k++)
					{
						if (!(null == dynamicBone.m_notRolls[k]))
						{
							if (dictBone.TryGetValue(dynamicBone.m_notRolls[k].name, out obj))
							{
								dynamicBone.m_notRolls[k] = obj.transform;
							}
						}
					}
				}
				if (dynamicBone.m_Colliders != null)
				{
					dynamicBone.m_Colliders.Clear();
					dynamicBone.m_Colliders.AddRange(dbc);
				}
			}
			Transform trfRootBone = base.cmpBoneBody.targetEtc.trfRoot;
			int copyWeights = item.Weight;
			if (copyWeights == 1)
			{
				this.aaWeightsBody.AssignedWeightsAndSetBounds(newObj, "cf_J_Root", ChaControlDefine.bounds, trfRootBone);
			}
			else if (copyWeights == 2)
			{
				this.aaWeightsHead.AssignedWeights(newObj, "cf_J_FaceRoot", trfRootBone);
			}
			base.objExtraAccessory[(int)parts] = newObj;
			if (null != base.objExtraAccessory[(int)parts])
			{
				base.cmpExtraAccessory[(int)parts] = base.objExtraAccessory[(int)parts].GetComponent<CmpAccessory>();
				if (null != base.cmpExtraAccessory[(int)parts])
				{
					base.cmpExtraAccessory[(int)parts].InitDynamicBones();
				}
			}
			yield break;
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0010CE48 File Offset: 0x0010B248
		public void ResetDynamicBoneExtraAccessories(bool includeInactive = false)
		{
			if (base.cmpExtraAccessory == null)
			{
				return;
			}
			for (int i = 0; i < base.cmpExtraAccessory.Length; i++)
			{
				if (!(null == base.cmpExtraAccessory[i]))
				{
					base.cmpExtraAccessory[i].ResetDynamicBones(includeInactive);
				}
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0010CEA0 File Offset: 0x0010B2A0
		public bool ChangeExtraAccessoryColor(ChaControlDefine.ExtraAccessoryParts parts, params Color[] color)
		{
			if (color == null)
			{
				return false;
			}
			if (!MathfEx.RangeEqualOn<int>(0, (int)parts, Enum.GetNames(typeof(ChaControlDefine.ExtraAccessoryParts)).Length - 1))
			{
				return false;
			}
			CmpAccessory cmpAccessory = base.cmpExtraAccessory[(int)parts];
			if (null == cmpAccessory)
			{
				return false;
			}
			if (cmpAccessory.rendNormal != null)
			{
				foreach (Renderer renderer in cmpAccessory.rendNormal)
				{
					if (cmpAccessory.useColor01 && 1 <= color.Length)
					{
						renderer.material.SetColor(ChaShader.Color, color[0]);
					}
					if (cmpAccessory.useColor02 && 2 <= color.Length)
					{
						renderer.material.SetColor(ChaShader.Color2, color[1]);
					}
					if (cmpAccessory.useColor03 && 3 <= color.Length)
					{
						renderer.material.SetColor(ChaShader.Color3, color[2]);
					}
				}
			}
			if (cmpAccessory.rendAlpha != null && 4 <= color.Length)
			{
				foreach (Renderer renderer2 in cmpAccessory.rendAlpha)
				{
					renderer2.material.SetColor(ChaShader.Color, color[3]);
				}
			}
			return true;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0010D008 File Offset: 0x0010B408
		public bool GetExtraAccessoryDefaultColor(ref Color color, ChaControlDefine.ExtraAccessoryParts parts, int no)
		{
			if (!MathfEx.RangeEqualOn<int>(0, (int)parts, Enum.GetNames(typeof(ChaControlDefine.ExtraAccessoryParts)).Length - 1))
			{
				return false;
			}
			CmpAccessory cmpAccessory = base.cmpExtraAccessory[(int)parts];
			if (null == cmpAccessory)
			{
				return false;
			}
			if (no == 0 && cmpAccessory.useColor01)
			{
				color = cmpAccessory.defColor01;
				return true;
			}
			if (no == 1 && cmpAccessory.useColor02)
			{
				color = cmpAccessory.defColor02;
				return true;
			}
			if (no == 2 && cmpAccessory.useColor03)
			{
				color = cmpAccessory.defColor03;
				return true;
			}
			if (no == 3 && cmpAccessory.rendAlpha != null && cmpAccessory.rendAlpha.Length != 0)
			{
				color = cmpAccessory.defColor04;
				return true;
			}
			return false;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0010D0DC File Offset: 0x0010B4DC
		public void ShowExtraAccessory(ChaControlDefine.ExtraAccessoryParts parts, bool show)
		{
			base.showExtraAccessory[(int)parts] = show;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0010D0E8 File Offset: 0x0010B4E8
		public RuntimeAnimatorController LoadAnimation(string assetBundleName, string assetName, string manifestName = "")
		{
			if (null == base.animBody)
			{
				return null;
			}
			RuntimeAnimatorController runtimeAnimatorController = CommonLib.LoadAsset<RuntimeAnimatorController>(assetBundleName, assetName, false, manifestName);
			if (null == runtimeAnimatorController)
			{
				return null;
			}
			base.animBody.runtimeAnimatorController = runtimeAnimatorController;
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			return runtimeAnimatorController;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0010D136 File Offset: 0x0010B536
		public void AnimPlay(string stateName)
		{
			if (null == base.animBody)
			{
				return;
			}
			base.animBody.Play(stateName);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0010D158 File Offset: 0x0010B558
		public AnimatorStateInfo getAnimatorStateInfo(int _nLayer)
		{
			if (null == base.animBody || null == base.animBody.runtimeAnimatorController)
			{
				return default(AnimatorStateInfo);
			}
			return base.animBody.GetCurrentAnimatorStateInfo(_nLayer);
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0010D1A2 File Offset: 0x0010B5A2
		public bool syncPlay(AnimatorStateInfo _syncState, int _nLayer)
		{
			if (null == base.animBody)
			{
				return false;
			}
			base.animBody.Play(_syncState.shortNameHash, _nLayer, _syncState.normalizedTime);
			return true;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x0010D1D2 File Offset: 0x0010B5D2
		public bool syncPlay(int _nameHash, int _nLayer, float _fnormalizedTime)
		{
			if (null == base.animBody)
			{
				return false;
			}
			base.animBody.Play(_nameHash, _nLayer, _fnormalizedTime);
			return true;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x0010D1F6 File Offset: 0x0010B5F6
		public bool syncPlay(string _strameHash, int _nLayer, float _fnormalizedTime)
		{
			if (null == base.animBody)
			{
				return false;
			}
			base.animBody.Play(_strameHash, _nLayer, _fnormalizedTime);
			return true;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x0010D21A File Offset: 0x0010B61A
		public bool setLayerWeight(float _fWeight, int _nLayer)
		{
			if (null == base.animBody)
			{
				return false;
			}
			base.animBody.SetLayerWeight(_nLayer, _fWeight);
			return true;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x0010D240 File Offset: 0x0010B640
		public bool setAllLayerWeight(float _fWeight)
		{
			if (null == base.animBody)
			{
				return false;
			}
			for (int i = 1; i < base.animBody.layerCount; i++)
			{
				base.animBody.SetLayerWeight(i, _fWeight);
			}
			return true;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x0010D28A File Offset: 0x0010B68A
		public float getLayerWeight(int _nLayer)
		{
			if (null == base.animBody)
			{
				return 0f;
			}
			return base.animBody.GetLayerWeight(_nLayer);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0010D2AF File Offset: 0x0010B6AF
		public bool setPlay(string _strAnmName, int _nLayer)
		{
			if (null == base.animBody)
			{
				return false;
			}
			base.animBody.Play(_strAnmName, _nLayer);
			return true;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x0010D2D2 File Offset: 0x0010B6D2
		public void setAnimatorParamTrigger(string _strAnmName)
		{
			if (null == base.animBody)
			{
				return;
			}
			base.animBody.SetTrigger(_strAnmName);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x0010D2F2 File Offset: 0x0010B6F2
		public void setAnimatorParamResetTrigger(string _strAnmName)
		{
			if (null == base.animBody)
			{
				return;
			}
			base.animBody.ResetTrigger(_strAnmName);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0010D312 File Offset: 0x0010B712
		public void setAnimatorParamBool(string _strAnmName, bool _bFlag)
		{
			if (null == base.animBody)
			{
				return;
			}
			base.animBody.SetBool(_strAnmName, _bFlag);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0010D333 File Offset: 0x0010B733
		public bool getAnimatorParamBool(string _strAnmName)
		{
			return !(null == base.animBody) && base.animBody.GetBool(_strAnmName);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x0010D354 File Offset: 0x0010B754
		public void setAnimatorParamFloat(string _strAnmName, float _fValue)
		{
			if (base.animBody != null)
			{
				base.animBody.SetFloat(_strAnmName, _fValue);
			}
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x0010D374 File Offset: 0x0010B774
		public void setAnimPtnCrossFade(string _strAnmName, float _fBlendTime, int _nLayer, float _fCrossStateTime)
		{
			if (null == base.animBody)
			{
				return;
			}
			base.animBody.CrossFade(_strAnmName, _fBlendTime, _nLayer, _fCrossStateTime);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x0010D398 File Offset: 0x0010B798
		public bool isBlend(int _nLayer)
		{
			return !(null == base.animBody) && base.animBody.IsInTransition(_nLayer);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x0010D3BC File Offset: 0x0010B7BC
		public bool IsNextHash(int _nLayer, string _nameHash)
		{
			return !(null == base.animBody) && base.animBody.GetNextAnimatorStateInfo(_nLayer).IsName(_nameHash);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x0010D3F4 File Offset: 0x0010B7F4
		public bool IsParameterInAnimator(string strParameter)
		{
			return !(base.animBody == null) && !(base.animBody.runtimeAnimatorController == null) && Array.FindIndex<AnimatorControllerParameter>(base.animBody.parameters, (AnimatorControllerParameter p) => p.name == strParameter) != -1;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x0010D459 File Offset: 0x0010B859
		// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x0010D461 File Offset: 0x0010B861
		private Texture texBodyAlphaMask { get; set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x0010D46A File Offset: 0x0010B86A
		// (set) Token: 0x06002EE2 RID: 12002 RVA: 0x0010D472 File Offset: 0x0010B872
		private Texture texBraAlphaMask { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x0010D47B File Offset: 0x0010B87B
		// (set) Token: 0x06002EE4 RID: 12004 RVA: 0x0010D483 File Offset: 0x0010B883
		private Texture texInnerTBAlphaMask { get; set; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x0010D48C File Offset: 0x0010B88C
		// (set) Token: 0x06002EE6 RID: 12006 RVA: 0x0010D494 File Offset: 0x0010B894
		private Texture texInnerBAlphaMask { get; set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x0010D49D File Offset: 0x0010B89D
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x0010D4A5 File Offset: 0x0010B8A5
		private Texture texPanstAlphaMask { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x0010D4AE File Offset: 0x0010B8AE
		// (set) Token: 0x06002EEA RID: 12010 RVA: 0x0010D4B6 File Offset: 0x0010B8B6
		private Texture texBodyBAlphaMask { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x0010D4BF File Offset: 0x0010B8BF
		// (set) Token: 0x06002EEC RID: 12012 RVA: 0x0010D4C7 File Offset: 0x0010B8C7
		private int underMaskReflectionType { get; set; } = -1;

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x0010D4D0 File Offset: 0x0010B8D0
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x0010D4D8 File Offset: 0x0010B8D8
		private bool underMaskBreakDisable { get; set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x0010D4E1 File Offset: 0x0010B8E1
		// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x0010D4E9 File Offset: 0x0010B8E9
		public bool hideInnerBWithBot { get; set; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x0010D4F2 File Offset: 0x0010B8F2
		// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x0010D4FA File Offset: 0x0010B8FA
		public BustNormal bustNormal { get; private set; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x0010D503 File Offset: 0x0010B903
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x0010D50B File Offset: 0x0010B90B
		private byte[] siruNewLv { get; set; }

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0010D514 File Offset: 0x0010B914
		protected void InitializeControlBodyAll()
		{
			this.siruNewLv = new byte[Enum.GetNames(typeof(ChaFileDefine.SiruParts)).Length];
			for (int i = 0; i < this.siruNewLv.Length; i++)
			{
				this.siruNewLv[i] = 0;
			}
			this.InitializeControlBodyObject();
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0010D565 File Offset: 0x0010B965
		protected void InitializeControlBodyObject()
		{
			this.texBodyAlphaMask = null;
			this.texBraAlphaMask = null;
			this.texInnerTBAlphaMask = null;
			this.texInnerBAlphaMask = null;
			this.texPanstAlphaMask = null;
			this.texBodyBAlphaMask = null;
			this.hideInnerBWithBot = false;
			this.bustNormal = null;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0010D59F File Offset: 0x0010B99F
		protected void ReleaseControlBodyAll()
		{
			this.ReleaseControlBodyObject(false);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0010D5A8 File Offset: 0x0010B9A8
		protected void ReleaseControlBodyObject(bool init = true)
		{
			if (base.sex == 1)
			{
				if (this.bustNormal != null)
				{
					this.bustNormal.Release();
				}
				this.bustNormal = null;
			}
			for (int i = 0; i < this.siruNewLv.Length; i++)
			{
				this.siruNewLv[i] = 0;
			}
			UnityEngine.Resources.UnloadUnusedAssets();
			if (init)
			{
				this.InitializeControlBodyObject();
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0010D612 File Offset: 0x0010BA12
		public void ResetDynamicBoneBustAndHip(bool includeInactive = false)
		{
			if (null != base.cmpBoneBody)
			{
				base.cmpBoneBody.ResetDynamicBonesBustAndHip(includeInactive);
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0010D631 File Offset: 0x0010BA31
		public void ResetDynamicBoneAll(bool includeInactive = false)
		{
			this.ResetDynamicBoneHair(includeInactive);
			this.ResetDynamicBoneBustAndHip(includeInactive);
			this.ResetDynamicBoneClothes(includeInactive);
			this.ResetDynamicBoneAccessories(includeInactive);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x0010D64F File Offset: 0x0010BA4F
		public DynamicBone_Ver02 GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind area)
		{
			if (null == base.cmpBoneBody)
			{
				return null;
			}
			return base.cmpBoneBody.GetDynamicBoneBustAndHip(area);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0010D670 File Offset: 0x0010BA70
		public bool ReSetupDynamicBoneBust(int cate = 0)
		{
			if (cate == 0)
			{
				DynamicBone_Ver02 dynamicBoneBustAndHip = this.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastL);
				if (null != dynamicBoneBustAndHip)
				{
					dynamicBoneBustAndHip.ResetPosition();
				}
				dynamicBoneBustAndHip = this.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastR);
				if (null != dynamicBoneBustAndHip)
				{
					dynamicBoneBustAndHip.ResetPosition();
				}
			}
			else
			{
				DynamicBone_Ver02 dynamicBoneBustAndHip = this.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.HipL);
				if (null != dynamicBoneBustAndHip)
				{
					dynamicBoneBustAndHip.ResetPosition();
				}
				dynamicBoneBustAndHip = this.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.HipR);
				if (null != dynamicBoneBustAndHip)
				{
					dynamicBoneBustAndHip.ResetPosition();
				}
			}
			return true;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x0010D6F3 File Offset: 0x0010BAF3
		public void playDynamicBoneBust(int area, bool play)
		{
			if (area >= base.enableDynamicBoneBustAndHip.Length)
			{
				return;
			}
			base.enableDynamicBoneBustAndHip[area] = (base.sex != 0 && play);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0010D71E File Offset: 0x0010BB1E
		public void playDynamicBoneBust(ChaControlDefine.DynamicBoneKind area, bool play)
		{
			this.playDynamicBoneBust((int)area, play);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0010D728 File Offset: 0x0010BB28
		public bool ChangeNipRate(float rate)
		{
			base.chaFile.status.nipStandRate = rate;
			this.changeShapeBodyMask = true;
			base.updateShapeBody = true;
			return true;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0010D74C File Offset: 0x0010BB4C
		public void ChangeBustInert(bool h)
		{
			if (base.sex != 1)
			{
				return;
			}
			float inert = 0.8f;
			if (h)
			{
				float num = base.fileBody.bustSoftness * base.fileBody.shapeValueBody[1] + 0.01f;
				num = Mathf.Clamp(num, 0f, 1f);
				inert = Mathf.Lerp(0.8f, 0.4f, num);
			}
			DynamicBone_Ver02 dynamicBoneBustAndHip = this.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastL);
			if (null != dynamicBoneBustAndHip)
			{
				dynamicBoneBustAndHip.setSoftParamsEx(0, -1, inert, true);
				dynamicBoneBustAndHip.ResetPosition();
			}
			dynamicBoneBustAndHip = this.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastR);
			if (null != dynamicBoneBustAndHip)
			{
				dynamicBoneBustAndHip.setSoftParamsEx(0, -1, inert, true);
				dynamicBoneBustAndHip.ResetPosition();
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0010D800 File Offset: 0x0010BC00
		public void SetSiruFlag(ChaFileDefine.SiruParts parts, byte lv)
		{
			this.siruNewLv[(int)parts] = lv;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x0010D80B File Offset: 0x0010BC0B
		public byte GetSiruFlag(ChaFileDefine.SiruParts parts)
		{
			return base.chaFile.status.siruLv[(int)parts];
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0010D820 File Offset: 0x0010BC20
		private bool UpdateSiru(bool forceChange = false)
		{
			if (base.sex == 0)
			{
				return false;
			}
			float[] array = new float[]
			{
				0f,
				0.5f,
				1f
			};
			if (!(null == base.customMatFace))
			{
				int num = 0;
				if (forceChange || base.fileStatus.siruLv[num] != this.siruNewLv[num])
				{
					base.fileStatus.siruLv[num] = this.siruNewLv[num];
					base.customMatFace.SetFloat(ChaShader.siruFace, array[(int)base.fileStatus.siruLv[num]]);
				}
			}
			ChaFileDefine.SiruParts[] array2 = new ChaFileDefine.SiruParts[]
			{
				ChaFileDefine.SiruParts.SiruFrontTop,
				ChaFileDefine.SiruParts.SiruFrontBot,
				ChaFileDefine.SiruParts.SiruBackTop,
				ChaFileDefine.SiruParts.SiruBackBot
			};
			List<string> list = new List<string>();
			bool flag = false;
			for (int i = 0; i < array2.Length; i++)
			{
				if (forceChange || base.fileStatus.siruLv[(int)array2[i]] != this.siruNewLv[(int)array2[i]])
				{
					flag = true;
				}
				if (this.siruNewLv[(int)array2[i]] != 0)
				{
					string item = array2[i].ToString() + this.siruNewLv[(int)array2[i]].ToString("00");
					list.Add(item);
				}
				base.fileStatus.siruLv[(int)array2[i]] = this.siruNewLv[(int)array2[i]];
			}
			if (flag)
			{
				byte[] array3 = new byte[]
				{
					base.fileStatus.siruLv[(int)array2[0]],
					base.fileStatus.siruLv[(int)array2[1]],
					base.fileStatus.siruLv[(int)array2[2]],
					base.fileStatus.siruLv[(int)array2[3]]
				};
				if (null != base.cmpBody.targetCustom.rendBody && 1 < base.cmpBody.targetCustom.rendBody.materials.Length)
				{
					base.cmpBody.targetCustom.rendBody.materials[1].SetFloat(ChaShader.siruFrontTop, array[(int)array3[0]]);
					base.cmpBody.targetCustom.rendBody.materials[1].SetFloat(ChaShader.siruFrontBot, array[(int)array3[1]]);
					base.cmpBody.targetCustom.rendBody.materials[1].SetFloat(ChaShader.siruBackTop, array[(int)array3[2]]);
					base.cmpBody.targetCustom.rendBody.materials[1].SetFloat(ChaShader.siruBackBot, array[(int)array3[3]]);
				}
				this.SetBodyBaseMaterial();
				int[] array4 = new int[]
				{
					0,
					1,
					2,
					3,
					5
				};
				for (int j = 0; j < array4.Length; j++)
				{
					this.UpdateClothesSiru(j, array[(int)array3[0]], array[(int)array3[1]], array[(int)array3[2]], array[(int)array3[3]]);
				}
			}
			return true;
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x0010DB09 File Offset: 0x0010BF09
		public float siriAkaRate
		{
			get
			{
				return base.fileStatus.siriAkaRate;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0010DB18 File Offset: 0x0010BF18
		public void ChangeSiriAkaRate(float value)
		{
			base.fileStatus.siriAkaRate = Mathf.Clamp(value, 0f, 1f);
			if (null != base.customMatBody)
			{
				base.customMatBody.SetFloat(ChaShader.SiriAka, base.fileStatus.siriAkaRate);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x0010DB6C File Offset: 0x0010BF6C
		// (set) Token: 0x06002F07 RID: 12039 RVA: 0x0010DB7C File Offset: 0x0010BF7C
		public float wetRate
		{
			get
			{
				return base.fileStatus.wetRate;
			}
			set
			{
				float num = Mathf.Clamp(value, 0f, 1f);
				if (base.fileStatus.wetRate != num)
				{
					base.updateWet = true;
				}
				base.fileStatus.wetRate = num;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x0010DBBE File Offset: 0x0010BFBE
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x0010DBCC File Offset: 0x0010BFCC
		public float skinGlossRate
		{
			get
			{
				return base.fileStatus.skinTuyaRate;
			}
			set
			{
				float num = Mathf.Clamp(value, 0f, 1f);
				if (base.fileStatus.skinTuyaRate != num)
				{
					base.fileStatus.skinTuyaRate = num;
					this.ChangeBodyGlossPower();
					this.ChangeFaceGlossPower();
				}
			}
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x0010DC18 File Offset: 0x0010C018
		public void UpdateWet()
		{
			float wetRate = base.fileStatus.wetRate;
			for (int i = 0; i < base.cmpHair.Length; i++)
			{
				if (!(null == base.cmpHair[i]))
				{
					foreach (Renderer renderer in base.cmpHair[i].rendHair)
					{
						foreach (Material material in renderer.materials)
						{
							if (material.HasProperty(ChaShader.wetRate))
							{
								material.SetFloat(ChaShader.wetRate, wetRate);
							}
						}
					}
					foreach (Renderer renderer2 in base.cmpHair[i].rendAccessory)
					{
						foreach (Material material2 in renderer2.materials)
						{
							if (material2.HasProperty(ChaShader.wetRate))
							{
								material2.SetFloat(ChaShader.wetRate, wetRate);
							}
						}
					}
				}
			}
			if (base.customMatFace)
			{
				base.customMatFace.SetFloat(ChaShader.wetRate, wetRate);
			}
			if (base.customMatBody)
			{
				base.customMatBody.SetFloat(ChaShader.wetRate, wetRate);
			}
			for (int n = 0; n < base.cmpClothes.Length; n++)
			{
				if (!(null == base.cmpClothes[n]))
				{
					foreach (Renderer renderer3 in base.cmpClothes[n].rendNormal01)
					{
						foreach (Material material3 in renderer3.materials)
						{
							if (material3.HasProperty(ChaShader.wetRate))
							{
								material3.SetFloat(ChaShader.wetRate, wetRate);
							}
						}
					}
					foreach (Renderer renderer4 in base.cmpClothes[n].rendNormal02)
					{
						foreach (Material material4 in renderer4.materials)
						{
							if (material4.HasProperty(ChaShader.wetRate))
							{
								material4.SetFloat(ChaShader.wetRate, wetRate);
							}
						}
					}
					foreach (Renderer renderer5 in base.cmpClothes[n].rendNormal03)
					{
						foreach (Material material5 in renderer5.materials)
						{
							if (material5.HasProperty(ChaShader.wetRate))
							{
								material5.SetFloat(ChaShader.wetRate, wetRate);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x0010DF28 File Offset: 0x0010C328
		public void ChangeAlphaMask(params byte[] state)
		{
			if (base.customMatBody)
			{
				if (base.customMatBody.HasProperty(ChaShader.alpha_a))
				{
					base.customMatBody.SetFloat(ChaShader.alpha_a, (float)state[0]);
				}
				if (base.customMatBody.HasProperty(ChaShader.alpha_b))
				{
					base.customMatBody.SetFloat(ChaShader.alpha_b, (float)state[1]);
				}
			}
			if (base.rendBra != null)
			{
				for (int i = 0; i < 1; i++)
				{
					if (base.rendBra[i])
					{
						Material material = base.rendBra[i].material;
						if (material)
						{
							if (material.HasProperty(ChaShader.alpha_a))
							{
								material.SetFloat(ChaShader.alpha_a, (float)state[0]);
							}
							if (material.HasProperty(ChaShader.alpha_b))
							{
								material.SetFloat(ChaShader.alpha_b, (float)state[1]);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x0010E020 File Offset: 0x0010C420
		public void ChangeAlphaMaskEx()
		{
			float value = (this.nowCoordinate.clothes.parts[0].breakRate != 0f) ? 1f : 0f;
			if (null != base.customMatBody && base.customMatBody.HasProperty(ChaShader.alpha_c))
			{
				base.customMatBody.SetFloat(ChaShader.alpha_c, value);
			}
			if (base.rendBra != null)
			{
				for (int i = 0; i < 1; i++)
				{
					if (base.rendBra[i])
					{
						Material material = base.rendBra[i].material;
						if (null != material && material.HasProperty(ChaShader.alpha_c))
						{
							material.SetFloat(ChaShader.alpha_c, value);
						}
					}
				}
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0010E0FC File Offset: 0x0010C4FC
		public void ChangeAlphaMask2()
		{
			float value = 0f;
			if (this.underMaskReflectionType == 0)
			{
				if (!this.underMaskBreakDisable || this.nowCoordinate.clothes.parts[0].breakRate == 0f)
				{
					if (base.fileStatus.clothesState[1] == 0)
					{
						value = 1f;
					}
				}
			}
			else if (this.underMaskReflectionType == 1)
			{
				if (!this.underMaskBreakDisable || this.nowCoordinate.clothes.parts[1].breakRate == 0f)
				{
					if (base.fileStatus.clothesState[1] == 0 && !this.notBot)
					{
						value = 1f;
					}
				}
			}
			if (null != base.customMatBody && base.customMatBody.HasProperty(ChaShader.alpha_d))
			{
				base.customMatBody.SetFloat(ChaShader.alpha_d, value);
			}
			if (null != base.rendInnerTB && null != base.rendInnerTB.material && base.rendInnerTB.material.HasProperty(ChaShader.alpha_d))
			{
				base.rendInnerTB.material.SetFloat(ChaShader.alpha_d, value);
			}
			if (null != base.rendInnerB && null != base.rendInnerB.material && base.rendInnerB.material.HasProperty(ChaShader.alpha_d))
			{
				base.rendInnerB.material.SetFloat(ChaShader.alpha_d, value);
			}
			if (null != base.rendPanst && null != base.rendPanst.material && base.rendPanst.material.HasProperty(ChaShader.alpha_d))
			{
				base.rendPanst.material.SetFloat(ChaShader.alpha_d, value);
			}
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0010E304 File Offset: 0x0010C704
		public void ChangeSimpleBodyDraw(bool drawSimple)
		{
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0010E308 File Offset: 0x0010C708
		public void ChangeSimpleBodyColor(Color color)
		{
			if (null == base.cmpSimpleBody)
			{
				return;
			}
			if (base.fileStatus.simpleColor == color)
			{
				return;
			}
			base.fileStatus.simpleColor = color;
			if (base.cmpSimpleBody.targetCustom.rendBody)
			{
				Material material = base.cmpSimpleBody.targetCustom.rendBody.material;
				if (material)
				{
					material.SetColor(ChaShader.Color, color);
				}
			}
			if (base.cmpSimpleBody.targetEtc.rendTongue)
			{
				Material material2 = base.cmpSimpleBody.targetEtc.rendTongue.material;
				if (material2)
				{
					material2.SetColor(ChaShader.Color, color);
				}
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0010E3D8 File Offset: 0x0010C7D8
		private void UpdateVisible()
		{
			this.confSon = true;
			this.confBody = true;
			this.drawSimple = false;
			if (Singleton<Config>.IsInstance() && Singleton<HSceneManager>.IsInstance() && HSceneManager.isHScene)
			{
				this.confSon = ((base.sex != 0 && (base.sex != 1 || !base.isPlayer)) || Config.HData.Son);
				this.confBody = ((base.sex != 0 && (base.sex != 1 || !base.isPlayer)) || Config.HData.Visible);
				this.drawSimple = ((base.sex == 0 || (base.sex == 1 && base.isPlayer)) && Config.GraphicData.SimpleBody);
				base.fileStatus.visibleSimple = this.drawSimple;
				this.ChangeSimpleBodyColor(Config.GraphicData.SilhouetteColor);
			}
			if (base.cmpBody)
			{
				if (base.cmpBody.targetEtc.objTongue)
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(base.fileStatus.tongueState == 1);
					this.lstActive.Add(this.confBody);
					this.lstActive.Add(!this.drawSimple);
					this.lstActive.Add(base.fileStatus.visibleHeadAlways);
					this.lstActive.Add(base.fileStatus.visibleBodyAlways);
					YS_Assist.SetActiveControl(base.cmpBody.targetEtc.objTongue, this.lstActive);
				}
				if (base.cmpBody.targetEtc.objMNPB)
				{
					YS_Assist.SetActiveControl(base.cmpBody.targetEtc.objMNPB, !base.hideMoz);
				}
				if (base.cmpBody.targetEtc.objBody)
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(this.confBody);
					this.lstActive.Add(!this.drawSimple);
					this.lstActive.Add(base.fileStatus.visibleBodyAlways);
					YS_Assist.SetActiveControl(base.cmpBody.targetEtc.objBody, this.lstActive);
				}
				if (base.cmpBody.targetEtc.objDanTop)
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					bool flag = false;
					if (this.notBot && base.fileStatus.clothesState[0] == 0)
					{
						flag = true;
					}
					this.lstActive.Add(this.drawSimple || ((!this.IsClothesStateKind(1) || base.fileStatus.clothesState[1] != 0) && !flag) || base.fileStatus.visibleSon);
					this.lstActive.Add(this.confSon);
					this.lstActive.Add(base.fileStatus.visibleSonAlways);
					YS_Assist.SetActiveControl(base.cmpBody.targetEtc.objDanTop, this.lstActive);
				}
			}
			if (base.cmpSimpleBody)
			{
				if (base.cmpSimpleBody.targetEtc.objTongue)
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(base.fileStatus.tongueState == 1);
					this.lstActive.Add(this.confBody);
					this.lstActive.Add(this.drawSimple);
					this.lstActive.Add(base.fileStatus.visibleHeadAlways);
					this.lstActive.Add(base.fileStatus.visibleBodyAlways);
					YS_Assist.SetActiveControl(base.cmpSimpleBody.targetEtc.objTongue, this.lstActive);
				}
				if (base.cmpSimpleBody.targetEtc.objMNPB)
				{
					YS_Assist.SetActiveControl(base.cmpSimpleBody.targetEtc.objMNPB, !base.hideMoz);
				}
				if (base.cmpSimpleBody.targetEtc.objBody)
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(this.confBody);
					this.lstActive.Add(this.drawSimple);
					this.lstActive.Add(base.fileStatus.visibleBodyAlways);
					YS_Assist.SetActiveControl(base.cmpSimpleBody.targetEtc.objBody, this.lstActive);
				}
				if (base.cmpSimpleBody.targetEtc.objDanTop)
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(this.drawSimple && base.fileStatus.visibleSon);
					this.lstActive.Add(this.confSon);
					this.lstActive.Add(base.fileStatus.visibleSonAlways);
					YS_Assist.SetActiveControl(base.cmpSimpleBody.targetEtc.objDanTop, this.lstActive);
				}
			}
			if (base.cmpFace)
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(base.fileStatus.visibleHeadAlways);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objHead, this.lstActive);
				if (base.cmpFace.targetEtc.objTongue)
				{
					YS_Assist.SetActiveControl(base.cmpFace.targetEtc.objTongue, base.fileStatus.tongueState == 0);
				}
			}
			if (base.cmpClothes[0])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(2 != base.fileStatus.clothesState[0]);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[0], this.lstActive);
			}
			bool flag2 = false;
			bool flag3 = false;
			if (base.cmpClothes[0])
			{
				if (YS_Assist.SetActiveControl(base.cmpClothes[0].objTopDef, base.fileStatus.clothesState[0] == 0))
				{
					flag2 = true;
				}
				if (YS_Assist.SetActiveControl(base.cmpClothes[0].objTopHalf, base.fileStatus.clothesState[0] == 1))
				{
					flag2 = true;
				}
				this.lstActive.Clear();
				this.lstActive.Add(base.fileStatus.clothesState[1] == 0);
				this.lstActive.Add(base.fileStatus.clothesState[0] != 2);
				if (YS_Assist.SetActiveControl(base.cmpClothes[0].objBotDef, this.lstActive))
				{
					flag3 = true;
				}
				this.lstActive.Clear();
				this.lstActive.Add(base.fileStatus.clothesState[1] != 0);
				this.lstActive.Add(base.fileStatus.clothesState[0] != 2);
				if (YS_Assist.SetActiveControl(base.cmpClothes[0].objBotHalf, this.lstActive))
				{
					flag3 = true;
				}
			}
			this.DrawOption(ChaFileDefine.ClothesKind.top);
			if (flag2 || this.updateAlphaMask)
			{
				byte b = base.fileStatus.clothesState[0];
				byte[,] array = new byte[,]
				{
					{
						1,
						1
					},
					{
						0,
						1
					},
					{
						0,
						0
					}
				};
				this.ChangeAlphaMask(new byte[]
				{
					array[(int)b, 0],
					array[(int)b, 1]
				});
				this.updateAlphaMask = false;
			}
			bool flag4 = false;
			if (base.cmpClothes[1])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(!this.notBot);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(2 != base.fileStatus.clothesState[1]);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				if (YS_Assist.SetActiveControl(base.objClothes[1], this.lstActive))
				{
					flag4 = true;
				}
			}
			if (base.cmpClothes[1])
			{
				if (YS_Assist.SetActiveControl(base.cmpClothes[1].objBotDef, base.fileStatus.clothesState[1] == 0))
				{
					flag4 = true;
				}
				if (YS_Assist.SetActiveControl(base.cmpClothes[1].objBotHalf, base.fileStatus.clothesState[1] == 1))
				{
					flag4 = true;
				}
				this.lstActive.Clear();
				this.lstActive.Add(base.fileStatus.clothesState[0] == 0);
				this.lstActive.Add(base.fileStatus.clothesState[1] != 2);
				YS_Assist.SetActiveControl(base.cmpClothes[1].objTopDef, this.lstActive);
				this.lstActive.Clear();
				this.lstActive.Add(base.fileStatus.clothesState[0] != 0);
				this.lstActive.Add(base.fileStatus.clothesState[1] != 2);
				YS_Assist.SetActiveControl(base.cmpClothes[1].objTopHalf, this.lstActive);
			}
			if (flag3 || flag4 || this.updateAlphaMask2)
			{
				this.ChangeAlphaMask2();
				this.updateAlphaMask2 = false;
			}
			this.DrawOption(ChaFileDefine.ClothesKind.bot);
			if (base.cmpClothes[2])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(!this.notInnerT);
				this.lstActive.Add(2 != base.fileStatus.clothesState[2]);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[2], this.lstActive);
			}
			if (base.cmpClothes[2])
			{
				YS_Assist.SetActiveControl(base.cmpClothes[2].objTopDef, base.fileStatus.clothesState[2] == 0);
				YS_Assist.SetActiveControl(base.cmpClothes[2].objTopHalf, base.fileStatus.clothesState[2] == 1);
				this.lstActive.Clear();
				this.lstActive.Add(base.fileStatus.clothesState[3] == 0);
				this.lstActive.Add(base.fileStatus.clothesState[2] != 2);
				YS_Assist.SetActiveControl(base.cmpClothes[2].objBotDef, this.lstActive);
				this.lstActive.Clear();
				this.lstActive.Add(base.fileStatus.clothesState[3] != 0);
				this.lstActive.Add(base.fileStatus.clothesState[2] != 2);
				YS_Assist.SetActiveControl(base.cmpClothes[2].objBotHalf, this.lstActive);
			}
			this.DrawOption(ChaFileDefine.ClothesKind.inner_t);
			bool item = true;
			if (base.cmpClothes[3])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(item);
				this.lstActive.Add(!this.notInnerB);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(2 != base.fileStatus.clothesState[3]);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[3], this.lstActive);
			}
			if (base.cmpClothes[3])
			{
				YS_Assist.SetActiveControl(base.cmpClothes[3].objBotDef, base.fileStatus.clothesState[3] == 0);
				YS_Assist.SetActiveControl(base.cmpClothes[3].objBotHalf, base.fileStatus.clothesState[3] == 1);
			}
			this.DrawOption(ChaFileDefine.ClothesKind.inner_b);
			if (base.cmpClothes[4])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(base.fileStatus.clothesState[4] == 0);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[4], this.lstActive);
			}
			this.DrawOption(ChaFileDefine.ClothesKind.gloves);
			if (base.cmpClothes[5])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(base.fileStatus.clothesState[5] != 2);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[5], this.lstActive);
			}
			if (base.cmpClothes[5])
			{
				YS_Assist.SetActiveControl(base.cmpClothes[5].objBotDef, base.fileStatus.clothesState[5] == 0);
				YS_Assist.SetActiveControl(base.cmpClothes[5].objBotHalf, base.fileStatus.clothesState[5] == 1);
			}
			this.DrawOption(ChaFileDefine.ClothesKind.panst);
			if (base.cmpClothes[6])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(base.fileStatus.clothesState[6] == 0);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[6], this.lstActive);
			}
			this.DrawOption(ChaFileDefine.ClothesKind.socks);
			if (base.cmpClothes[7])
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(base.fileStatus.clothesState[7] == 0);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(base.objClothes[7], this.lstActive);
			}
			this.DrawOption(ChaFileDefine.ClothesKind.shoes);
			for (int i = 0; i < base.objAccessory.Length; i++)
			{
				if (!(null == base.objAccessory[i]))
				{
					bool flag5 = false;
					if (!base.fileStatus.visibleHeadAlways && this.nowCoordinate.accessory.parts[i].partsOfHead)
					{
						flag5 = true;
					}
					if (!base.fileStatus.visibleBodyAlways || !this.confBody)
					{
						flag5 = true;
					}
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(base.fileStatus.showAccessory[i]);
					this.lstActive.Add(!this.drawSimple);
					this.lstActive.Add(!flag5);
					YS_Assist.SetActiveControl(base.objAccessory[i], this.lstActive);
				}
			}
			for (int j = 0; j < base.objExtraAccessory.Length; j++)
			{
				if (!(null == base.objExtraAccessory[j]))
				{
					this.lstActive.Clear();
					this.lstActive.Add(base.visibleAll);
					this.lstActive.Add(base.showExtraAccessory[j]);
					this.lstActive.Add(!this.drawSimple);
					YS_Assist.SetActiveControl(base.objExtraAccessory[j], this.lstActive);
				}
			}
			foreach (GameObject obj in base.objHair)
			{
				this.lstActive.Clear();
				this.lstActive.Add(base.visibleAll);
				this.lstActive.Add(this.confBody);
				this.lstActive.Add(!this.drawSimple);
				this.lstActive.Add(base.fileStatus.visibleHeadAlways);
				this.lstActive.Add(base.fileStatus.visibleBodyAlways);
				YS_Assist.SetActiveControl(obj, this.lstActive);
			}
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0010F768 File Offset: 0x0010DB68
		public void DrawOption(ChaFileDefine.ClothesKind kind)
		{
			CmpClothes cmpClothes = base.cmpClothes[(int)kind];
			if (null == cmpClothes)
			{
				return;
			}
			if (cmpClothes.objOpt01 != null)
			{
				foreach (GameObject obj in cmpClothes.objOpt01)
				{
					YS_Assist.SetActiveControl(obj, !this.nowCoordinate.clothes.parts[(int)kind].hideOpt[0]);
				}
			}
			if (cmpClothes.objOpt02 != null)
			{
				foreach (GameObject obj2 in cmpClothes.objOpt02)
				{
					YS_Assist.SetActiveControl(obj2, !this.nowCoordinate.clothes.parts[(int)kind].hideOpt[1]);
				}
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x0010F82F File Offset: 0x0010DC2F
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x0010F837 File Offset: 0x0010DC37
		public ChaFileCoordinate nowCoordinate { get; private set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x0010F840 File Offset: 0x0010DC40
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x0010F848 File Offset: 0x0010DC48
		public bool notInnerT { get; private set; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x0010F851 File Offset: 0x0010DC51
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x0010F859 File Offset: 0x0010DC59
		public bool notBot { get; private set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0010F862 File Offset: 0x0010DC62
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x0010F86A File Offset: 0x0010DC6A
		public bool notInnerB { get; private set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x0010F873 File Offset: 0x0010DC73
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x0010F87B File Offset: 0x0010DC7B
		public Dictionary<int, Dictionary<byte, string>> dictStateType { get; private set; }

		// Token: 0x06002F1C RID: 12060 RVA: 0x0010F884 File Offset: 0x0010DC84
		public bool AssignCoordinate(string path)
		{
			string path2 = ChaFileControl.ConvertCoordinateFilePath(path, base.sex);
			ChaFileCoordinate chaFileCoordinate = new ChaFileCoordinate();
			return chaFileCoordinate.LoadFile(path2) && this.AssignCoordinate(chaFileCoordinate);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x0010F8BC File Offset: 0x0010DCBC
		public bool AssignCoordinate(ChaFileCoordinate srcCoorde)
		{
			byte[] data = srcCoorde.SaveBytes();
			return base.chaFile.coordinate.LoadBytes(data, srcCoorde.loadVersion);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x0010F8E8 File Offset: 0x0010DCE8
		public bool AssignCoordinate()
		{
			byte[] data = this.nowCoordinate.SaveBytes();
			return base.chaFile.coordinate.LoadBytes(data, this.nowCoordinate.loadVersion);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x0010F91D File Offset: 0x0010DD1D
		public bool ChangeNowCoordinate(bool reload = false, bool forceChange = true)
		{
			return this.ChangeNowCoordinate(base.chaFile.coordinate, reload, forceChange);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x0010F934 File Offset: 0x0010DD34
		public bool ChangeNowCoordinate(string path, bool reload = false, bool forceChange = true)
		{
			string path2 = ChaFileControl.ConvertCoordinateFilePath(path, base.sex);
			ChaFileCoordinate chaFileCoordinate = new ChaFileCoordinate();
			return chaFileCoordinate.LoadFile(path2) && this.ChangeNowCoordinate(chaFileCoordinate, reload, forceChange);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0010F96C File Offset: 0x0010DD6C
		public bool ChangeNowCoordinate(ChaFileCoordinate srcCoorde, bool reload = false, bool forceChange = true)
		{
			byte[] data = srcCoorde.SaveBytes();
			return this.nowCoordinate.LoadBytes(data, srcCoorde.loadVersion) && (!reload || this.Reload(false, true, true, true, forceChange));
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x0010F9AC File Offset: 0x0010DDAC
		protected void InitializeControlCoordinateAll()
		{
			this.nowCoordinate = new ChaFileCoordinate();
			this.InitializeControlCoordinateObject();
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0010F9BF File Offset: 0x0010DDBF
		protected void InitializeControlCoordinateObject()
		{
			this.notInnerT = false;
			this.notBot = false;
			this.notInnerB = false;
			this.dictStateType = new Dictionary<int, Dictionary<byte, string>>();
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x0010F9E1 File Offset: 0x0010DDE1
		protected void ReleaseControlCoordinateAll()
		{
			this.ReleaseControlCoordinateObject(false);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0010F9EA File Offset: 0x0010DDEA
		protected void ReleaseControlCoordinateObject(bool init = true)
		{
			if (init)
			{
				this.InitializeControlCoordinateObject();
			}
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0010F9F8 File Offset: 0x0010DDF8
		protected void ReleaseBaseCustomTextureClothes(int parts, bool createTex = true)
		{
			for (int i = 0; i < 3; i++)
			{
				if (base.ctCreateClothes[parts, i] != null)
				{
					if (createTex)
					{
						base.ctCreateClothes[parts, i].Release();
					}
					else
					{
						base.ctCreateClothes[parts, i].ReleaseCreateMaterial();
					}
					base.ctCreateClothes[parts, i] = null;
				}
				if (base.ctCreateClothesGloss[parts, i] != null)
				{
					if (createTex)
					{
						base.ctCreateClothesGloss[parts, i].Release();
					}
					else
					{
						base.ctCreateClothesGloss[parts, i].ReleaseCreateMaterial();
					}
					base.ctCreateClothesGloss[parts, i] = null;
				}
			}
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x0010FAB8 File Offset: 0x0010DEB8
		protected bool InitBaseCustomTextureClothes(int parts)
		{
			if (base.infoClothes == null)
			{
				return false;
			}
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			ListInfoBase listInfoBase = base.infoClothes[parts];
			string info = listInfoBase.GetInfo(ChaListDefine.KeyType.MainManifest);
			string info2 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainAB);
			text = listInfoBase.GetInfo(ChaListDefine.KeyType.MainTex);
			text2 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainTex02);
			text3 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainTex03);
			text4 = listInfoBase.GetInfo(ChaListDefine.KeyType.ColorMaskTex);
			text5 = listInfoBase.GetInfo(ChaListDefine.KeyType.ColorMask02Tex);
			text6 = listInfoBase.GetInfo(ChaListDefine.KeyType.ColorMask03Tex);
			if ("0" == text)
			{
				return false;
			}
			Texture2D texture2D = CommonLib.LoadAsset<Texture2D>(info2, text, false, info);
			if (null == texture2D)
			{
				return false;
			}
			if ("0" == text4)
			{
				UnityEngine.Resources.UnloadAsset(texture2D);
				return false;
			}
			Texture2D texture2D2 = CommonLib.LoadAsset<Texture2D>(info2, text4, false, info);
			if (null == texture2D2)
			{
				UnityEngine.Resources.UnloadAsset(texture2D);
				return false;
			}
			Texture2D texture2D3 = null;
			if ("0" != text2)
			{
				texture2D3 = CommonLib.LoadAsset<Texture2D>(info2, text2, false, info);
			}
			Texture2D texture2D4 = null;
			if ("0" != text5)
			{
				texture2D4 = CommonLib.LoadAsset<Texture2D>(info2, text5, false, info);
			}
			Texture2D texture2D5 = null;
			if ("0" != text3)
			{
				texture2D5 = CommonLib.LoadAsset<Texture2D>(info2, text3, false, info);
			}
			Texture2D texture2D6 = null;
			if ("0" != text6)
			{
				texture2D6 = CommonLib.LoadAsset<Texture2D>(info2, text6, false, info);
			}
			Texture2D[] array = new Texture2D[]
			{
				texture2D,
				texture2D3,
				texture2D5
			};
			Texture2D[] array2 = new Texture2D[]
			{
				texture2D2,
				texture2D4,
				texture2D6
			};
			for (int i = 0; i < 3; i++)
			{
				base.ctCreateClothes[parts, i] = null;
				base.ctCreateClothesGloss[parts, i] = null;
				int num = 0;
				int num2 = 0;
				if (null != array[i])
				{
					CustomTextureCreate customTextureCreate = new CustomTextureCreate(base.objRoot.transform);
					num = array[i].width;
					num2 = array[i].height;
					customTextureCreate.Initialize("abdata", "chara/mm_base.unity3d", "create_clothes", num, num2, RenderTextureFormat.ARGB32);
					customTextureCreate.SetMainTexture(array[i]);
					customTextureCreate.SetTexture(ChaShader.ColorMask, array2[i]);
					base.ctCreateClothes[parts, i] = customTextureCreate;
				}
				if (null != array[i])
				{
					CustomTextureCreate customTextureCreate = new CustomTextureCreate(base.objRoot.transform);
					customTextureCreate.Initialize("abdata", "chara/mm_base.unity3d", "create_clothes detail", num, num2, RenderTextureFormat.ARGB32);
					RenderTexture active = RenderTexture.active;
					RenderTexture renderTexture = new RenderTexture(num, num2, 0, RenderTextureFormat.ARGB32);
					bool sRGBWrite = GL.sRGBWrite;
					GL.sRGBWrite = true;
					Graphics.SetRenderTarget(renderTexture);
					GL.Clear(false, true, new Color(0f, 0f, 0f, 1f));
					Graphics.SetRenderTarget(null);
					GL.sRGBWrite = sRGBWrite;
					Texture2D texture2D7 = new Texture2D(num, num2, TextureFormat.ARGB32, false);
					RenderTexture.active = renderTexture;
					texture2D7.ReadPixels(new Rect(0f, 0f, (float)num, (float)num2), 0, 0);
					texture2D7.Apply();
					RenderTexture.active = active;
					UnityEngine.Object.Destroy(renderTexture);
					customTextureCreate.SetMainTexture(texture2D7);
					customTextureCreate.SetTexture(ChaShader.ColorMask, array2[i]);
					base.ctCreateClothesGloss[parts, i] = customTextureCreate;
				}
			}
			return true;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0010FE40 File Offset: 0x0010E240
		public bool AddClothesStateKind(int clothesKind, string stateType)
		{
			ChaFileDefine.ClothesKind clothesKind2 = (ChaFileDefine.ClothesKind)Enum.ToObject(typeof(ChaFileDefine.ClothesKind), clothesKind);
			switch (clothesKind2)
			{
			case ChaFileDefine.ClothesKind.top:
			case ChaFileDefine.ClothesKind.bot:
			{
				this.dictStateType.Remove(0);
				this.dictStateType.Remove(1);
				ListInfoBase listInfoBase = base.infoClothes[0];
				byte b = 3;
				if (listInfoBase != null)
				{
					b = byte.Parse(listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
				}
				byte b2 = 3;
				ListInfoBase listInfoBase2 = base.infoClothes[1];
				if (listInfoBase2 != null)
				{
					b2 = byte.Parse(listInfoBase2.GetInfo(ChaListDefine.KeyType.StateType));
				}
				if (b2 != 0)
				{
					int num = 0;
					if (base.cmpClothes != null && null != base.cmpClothes[num])
					{
						GameObject objBotDef = base.cmpClothes[num].objBotDef;
						if (null != objBotDef)
						{
							if (b == 0)
							{
								b2 = 0;
							}
							else if (b == 1 && b2 == 2)
							{
								b2 = 1;
							}
						}
					}
				}
				if (b != 0)
				{
					int num2 = 1;
					if (base.cmpClothes != null && base.cmpClothes[num2])
					{
						GameObject objTopDef = base.cmpClothes[num2].objTopDef;
						if (null != objTopDef)
						{
							if (b2 == 0)
							{
								b = 0;
							}
							else if (b2 == 1 && b == 2)
							{
								b = 1;
							}
						}
					}
				}
				this.AddClothesStateKindSub(0, b);
				this.AddClothesStateKindSub(1, b2);
				if (clothesKind2 == ChaFileDefine.ClothesKind.top)
				{
					this.AddClothesStateKind(2, string.Empty);
				}
				break;
			}
			case ChaFileDefine.ClothesKind.inner_t:
			case ChaFileDefine.ClothesKind.inner_b:
			{
				this.dictStateType.Remove(2);
				this.dictStateType.Remove(3);
				byte b3 = 3;
				if (!this.notInnerT)
				{
					ListInfoBase listInfoBase3 = base.infoClothes[2];
					if (listInfoBase3 != null)
					{
						b3 = byte.Parse(listInfoBase3.GetInfo(ChaListDefine.KeyType.StateType));
					}
				}
				byte b4 = 3;
				if (!this.notInnerT || !this.notInnerB)
				{
					ListInfoBase listInfoBase4 = base.infoClothes[3];
					if (listInfoBase4 != null)
					{
						b4 = byte.Parse(listInfoBase4.GetInfo(ChaListDefine.KeyType.StateType));
					}
				}
				if (b4 != 0)
				{
					int num3 = 2;
					if (base.cmpClothes != null && null != base.cmpClothes[num3])
					{
						GameObject objBotDef2 = base.cmpClothes[num3].objBotDef;
						if (null != objBotDef2)
						{
							if (b3 == 0)
							{
								b4 = 0;
							}
							else if (b3 == 1 && b4 == 2)
							{
								b4 = 1;
							}
						}
					}
				}
				if (b3 != 0 && b3 != 2)
				{
					int num4 = 3;
					if (base.cmpClothes != null && null != base.cmpClothes[num4])
					{
						GameObject objBotDef3 = base.cmpClothes[num4].objBotDef;
						if (null != objBotDef3)
						{
							if (b4 == 0)
							{
								b3 = 0;
							}
							else if (b4 == 1 && b3 == 2)
							{
								b3 = 1;
							}
						}
					}
				}
				this.AddClothesStateKindSub(2, b3);
				this.AddClothesStateKindSub(3, b4);
				break;
			}
			case ChaFileDefine.ClothesKind.gloves:
				this.dictStateType.Remove(4);
				this.AddClothesStateKindSub(4, byte.Parse(stateType));
				break;
			case ChaFileDefine.ClothesKind.panst:
				this.dictStateType.Remove(5);
				this.AddClothesStateKindSub(5, byte.Parse(stateType));
				break;
			case ChaFileDefine.ClothesKind.socks:
				this.dictStateType.Remove(6);
				this.AddClothesStateKindSub(6, byte.Parse(stateType));
				break;
			case ChaFileDefine.ClothesKind.shoes:
				this.dictStateType.Remove(7);
				this.AddClothesStateKindSub(7, byte.Parse(stateType));
				break;
			}
			return true;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x001101D8 File Offset: 0x0010E5D8
		private bool AddClothesStateKindSub(int clothesKind, byte type)
		{
			if (!MathfEx.RangeEqualOn<int>(0, (int)type, 1))
			{
				return false;
			}
			Dictionary<byte, string> dictionary = new Dictionary<byte, string>();
			if (type == 0)
			{
				dictionary[0] = "着衣";
				dictionary[1] = "半脱";
				dictionary[2] = "脱衣";
			}
			else
			{
				dictionary[0] = "着衣";
				dictionary[2] = "脱衣";
			}
			this.dictStateType[clothesKind] = dictionary;
			return true;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x00110250 File Offset: 0x0010E650
		public void RemoveClothesStateKind(int clothesKind)
		{
			switch ((ChaFileDefine.ClothesKind)Enum.ToObject(typeof(ChaFileDefine.ClothesKind), clothesKind))
			{
			case ChaFileDefine.ClothesKind.top:
			case ChaFileDefine.ClothesKind.bot:
				this.AddClothesStateKind(0, string.Empty);
				break;
			case ChaFileDefine.ClothesKind.inner_t:
			case ChaFileDefine.ClothesKind.inner_b:
				this.AddClothesStateKind(2, string.Empty);
				break;
			case ChaFileDefine.ClothesKind.gloves:
				this.dictStateType.Remove(4);
				break;
			case ChaFileDefine.ClothesKind.panst:
				this.dictStateType.Remove(5);
				break;
			case ChaFileDefine.ClothesKind.socks:
				this.dictStateType.Remove(6);
				break;
			case ChaFileDefine.ClothesKind.shoes:
				this.dictStateType.Remove(7);
				break;
			}
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x0011030F File Offset: 0x0010E70F
		public bool IsClothes(int clothesKind)
		{
			return this.IsClothesStateKind(clothesKind) && !(null == base.objClothes[clothesKind]) && base.infoClothes[clothesKind] != null;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x00110344 File Offset: 0x0010E744
		public bool IsClothesStateKind(int clothesKind)
		{
			return this.dictStateType.ContainsKey(clothesKind);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x00110354 File Offset: 0x0010E754
		public Dictionary<byte, string> GetClothesStateKind(int clothesKind)
		{
			Dictionary<byte, string> result = null;
			this.dictStateType.TryGetValue(clothesKind, out result);
			return result;
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x00110374 File Offset: 0x0010E774
		public bool IsClothesStateType(int clothesKind, byte stateType)
		{
			Dictionary<byte, string> dictionary = null;
			this.dictStateType.TryGetValue(clothesKind, out dictionary);
			return dictionary != null && dictionary.ContainsKey(stateType);
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x001103A1 File Offset: 0x0010E7A1
		public bool IsBareFoot
		{
			get
			{
				return !this.IsClothes(7) || 0 != base.fileStatus.clothesState[7];
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x001103C4 File Offset: 0x0010E7C4
		public void SetClothesState(int clothesKind, byte state, bool next = true)
		{
			if (next)
			{
				byte b = base.fileStatus.clothesState[clothesKind];
				while (this.IsClothesStateKind(clothesKind))
				{
					if (this.IsClothesStateType(clothesKind, state))
					{
						base.fileStatus.clothesState[clothesKind] = state;
					}
					else
					{
						state = (state + 1) % 3;
						if (b != state)
						{
							continue;
						}
					}
					goto IL_D7;
				}
				base.fileStatus.clothesState[clothesKind] = state;
			}
			else
			{
				byte b2 = base.fileStatus.clothesState[clothesKind];
				while (this.IsClothesStateKind(clothesKind))
				{
					if (this.IsClothesStateType(clothesKind, state))
					{
						base.fileStatus.clothesState[clothesKind] = state;
						goto IL_D7;
					}
					state = (state + 2) % 3;
					if (b2 == state)
					{
						goto IL_D7;
					}
				}
				base.fileStatus.clothesState[clothesKind] = state;
			}
			IL_D7:
			switch (clothesKind)
			{
			case 0:
				if (this.notBot)
				{
					if (base.fileStatus.clothesState[clothesKind] == 2)
					{
						base.fileStatus.clothesState[1] = 2;
					}
					else if (base.fileStatus.clothesState[1] == 2)
					{
						base.fileStatus.clothesState[1] = state;
					}
				}
				break;
			case 1:
				if (this.notBot)
				{
					if (base.fileStatus.clothesState[clothesKind] == 2)
					{
						base.fileStatus.clothesState[0] = 2;
					}
					else if (base.fileStatus.clothesState[0] == 2)
					{
						base.fileStatus.clothesState[0] = state;
					}
				}
				break;
			case 2:
				if (this.notInnerB)
				{
					if (base.fileStatus.clothesState[clothesKind] == 2)
					{
						base.fileStatus.clothesState[3] = 2;
					}
					else if (base.fileStatus.clothesState[3] == 2)
					{
						base.fileStatus.clothesState[3] = state;
					}
				}
				break;
			case 3:
				if (this.notInnerB)
				{
					if (base.fileStatus.clothesState[clothesKind] == 2)
					{
						base.fileStatus.clothesState[2] = 2;
					}
					else if (base.fileStatus.clothesState[2] == 2)
					{
						base.fileStatus.clothesState[2] = state;
					}
				}
				break;
			}
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x00110624 File Offset: 0x0010EA24
		public void SetClothesStatePrev(int clothesKind)
		{
			byte b = base.fileStatus.clothesState[clothesKind];
			b = (b + 2) % 3;
			this.SetClothesState(clothesKind, b, false);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x00110650 File Offset: 0x0010EA50
		public void SetClothesStateNext(int clothesKind)
		{
			byte b = base.fileStatus.clothesState[clothesKind];
			b = (b + 1) % 3;
			this.SetClothesState(clothesKind, b, true);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0011067C File Offset: 0x0010EA7C
		public void SetClothesStateAll(byte state)
		{
			int num = Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length;
			for (int i = 0; i < num; i++)
			{
				this.SetClothesState(i, state, true);
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x001106B8 File Offset: 0x0010EAB8
		public void UpdateClothesStateAll()
		{
			int num = Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length;
			for (int i = 0; i < num; i++)
			{
				this.SetClothesState(i, base.fileStatus.clothesState[i], true);
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00110700 File Offset: 0x0010EB00
		public int GetNowClothesType()
		{
			int num = 0;
			int num2 = 1;
			int num3 = 2;
			int num4 = 3;
			int num5 = (int)((!this.IsClothesStateKind(num)) ? 2 : base.fileStatus.clothesState[num]);
			int num6 = (int)((!this.IsClothesStateKind(num2)) ? 2 : base.fileStatus.clothesState[num2]);
			int num7 = (int)((!this.IsClothesStateKind(num3)) ? 2 : base.fileStatus.clothesState[num3]);
			int num8 = (int)((!this.IsClothesStateKind(num4)) ? 2 : base.fileStatus.clothesState[num4]);
			bool flag = true;
			bool flag2 = true;
			if (base.infoClothes[num3] != null)
			{
				flag = (base.infoClothes[num3].Kind == 1);
			}
			if (base.infoClothes[num4] != null)
			{
				flag2 = (base.infoClothes[num4].Kind == 1);
			}
			if (this.notInnerB)
			{
				num8 = num7;
				flag2 = flag;
			}
			if (num5 == 0)
			{
				if (num6 == 0)
				{
					return 0;
				}
				if (num8 == 0)
				{
					return (!flag2) ? 2 : 1;
				}
				return 3;
			}
			else if (num6 == 0)
			{
				if (num7 == 0)
				{
					return (!flag) ? 2 : 1;
				}
				return 3;
			}
			else
			{
				if (num7 != 0)
				{
					return 3;
				}
				if (num8 != 0)
				{
					return 3;
				}
				if (flag)
				{
					return 1;
				}
				return (!flag2) ? 2 : 1;
			}
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00110874 File Offset: 0x0010EC74
		public bool IsKokanHide()
		{
			bool result = false;
			int[] array = new int[]
			{
				0,
				1,
				2,
				3
			};
			int[] array2 = new int[]
			{
				1,
				1,
				3,
				3
			};
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				if (this.IsClothes(num))
				{
					if ((i != 0 && i != 2) || !("1" != base.infoClothes[num].GetInfo(ChaListDefine.KeyType.Coordinate)))
					{
						if ("1" == base.infoClothes[num].GetInfo(ChaListDefine.KeyType.KokanHide) && base.fileStatus.clothesState[array2[i]] == 0)
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x0011093C File Offset: 0x0010ED3C
		public bool ChangeCustomClothes(int kind, bool updateColor, bool updateTex01, bool updateTex02, bool updateTex03)
		{
			CustomTextureCreate[] array = new CustomTextureCreate[]
			{
				base.ctCreateClothes[kind, 0],
				base.ctCreateClothes[kind, 1],
				base.ctCreateClothes[kind, 2]
			};
			CustomTextureCreate[] array2 = new CustomTextureCreate[]
			{
				base.ctCreateClothesGloss[kind, 0],
				base.ctCreateClothesGloss[kind, 1],
				base.ctCreateClothesGloss[kind, 2]
			};
			if (array[0] == null)
			{
				return false;
			}
			CmpClothes customClothesComponent = base.GetCustomClothesComponent(kind);
			if (null == customClothesComponent)
			{
				return false;
			}
			ChaFileClothes.PartsInfo partsInfo = this.nowCoordinate.clothes.parts[kind];
			if (!updateColor && !updateTex01 && !updateTex02 && !updateTex03)
			{
				return false;
			}
			bool result = true;
			int[] array3 = new int[]
			{
				ChaShader.PatternMask1,
				ChaShader.PatternMask2,
				ChaShader.PatternMask3
			};
			bool[] array4 = new bool[]
			{
				updateTex01,
				updateTex02,
				updateTex03
			};
			for (int i = 0; i < 3; i++)
			{
				if (array4[i])
				{
					Texture2D texture2D = null;
					ListInfoBase listInfo = base.lstCtrl.GetListInfo(ChaListDefine.CategoryNo.st_pattern, partsInfo.colorInfo[i].pattern);
					if (listInfo != null)
					{
						string info = listInfo.GetInfo(ChaListDefine.KeyType.MainTexAB);
						string info2 = listInfo.GetInfo(ChaListDefine.KeyType.MainTex);
						if ("0" != info && "0" != info2)
						{
							texture2D = CommonLib.LoadAsset<Texture2D>(info, info2, false, string.Empty);
							Singleton<Character>.Instance.AddLoadAssetBundle(info, "abdata");
						}
					}
					if (null != texture2D)
					{
						foreach (CustomTextureCreate customTextureCreate in array)
						{
							if (customTextureCreate != null)
							{
								customTextureCreate.SetTexture(array3[i], texture2D);
							}
						}
					}
					else
					{
						foreach (CustomTextureCreate customTextureCreate2 in array)
						{
							if (customTextureCreate2 != null)
							{
								customTextureCreate2.SetTexture(array3[i], null);
							}
						}
					}
				}
			}
			if (updateColor)
			{
				int[] array7 = new int[]
				{
					ChaShader.Color,
					ChaShader.Color2,
					ChaShader.Color3
				};
				int[] array8 = new int[]
				{
					ChaShader.Color1_2,
					ChaShader.Color2_2,
					ChaShader.Color3_2
				};
				int[] array9 = new int[]
				{
					ChaShader.patternuv1,
					ChaShader.patternuv2,
					ChaShader.patternuv3
				};
				int[] array10 = new int[]
				{
					ChaShader.patternuv1Rotator,
					ChaShader.patternuv2Rotator,
					ChaShader.patternuv3Rotator
				};
				int[] array11 = new int[]
				{
					ChaShader.ClothesGloss1,
					ChaShader.ClothesGloss2,
					ChaShader.ClothesGloss3
				};
				int[] array12 = new int[]
				{
					ChaShader.Metallic,
					ChaShader.Metallic2,
					ChaShader.Metallic3
				};
				bool[] array13 = new bool[]
				{
					customClothesComponent.useColorA01,
					customClothesComponent.useColorA02,
					customClothesComponent.useColorA03
				};
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l] != null)
					{
						for (int m = 0; m < 3; m++)
						{
							array[l].SetVector4(ChaShader.uvScalePattern, customClothesComponent.uvScalePattern);
							if (!array13[m] && partsInfo.colorInfo[m].baseColor.a != 1f)
							{
								partsInfo.colorInfo[m].baseColor = new Color(partsInfo.colorInfo[m].baseColor.r, partsInfo.colorInfo[m].baseColor.g, partsInfo.colorInfo[m].baseColor.b, 1f);
							}
							array[l].SetColor(array7[m], partsInfo.colorInfo[m].baseColor);
							array[l].SetColor(array8[m], partsInfo.colorInfo[m].patternColor);
							Vector4 value;
							value.x = Mathf.Lerp(20f, 1f, partsInfo.colorInfo[m].layout.x);
							value.y = Mathf.Lerp(20f, 1f, partsInfo.colorInfo[m].layout.y);
							value.z = Mathf.Lerp(-1f, 1f, partsInfo.colorInfo[m].layout.z);
							value.w = Mathf.Lerp(-1f, 1f, partsInfo.colorInfo[m].layout.w);
							array[l].SetVector4(array9[m], value);
							float value2 = Mathf.Lerp(-1f, 1f, partsInfo.colorInfo[m].rotation);
							array[l].SetFloat(array10[m], value2);
						}
					}
				}
				for (int n = 0; n < array2.Length; n++)
				{
					if (array2[n] != null)
					{
						for (int num = 0; num < 3; num++)
						{
							array2[n].SetFloat(array11[num], partsInfo.colorInfo[num].glossPower);
							array2[n].SetFloat(array12[num], partsInfo.colorInfo[num].metallicPower);
						}
					}
				}
			}
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				if (array[num2] != null)
				{
					array[num2].SetColor(ChaShader.Color4, customClothesComponent.defMainColor04);
				}
			}
			for (int num3 = 0; num3 < array2.Length; num3++)
			{
				if (array2[num3] != null)
				{
					array2[num3].SetFloat(ChaShader.ClothesGloss4, customClothesComponent.defGloss04);
					array2[num3].SetFloat(ChaShader.Metallic4, customClothesComponent.defMetallic04);
				}
			}
			bool[] array14 = new bool[]
			{
				customClothesComponent.rendNormal01 != null && 0 != customClothesComponent.rendNormal01.Length,
				customClothesComponent.rendNormal02 != null && 0 != customClothesComponent.rendNormal02.Length,
				customClothesComponent.rendNormal03 != null && 0 != customClothesComponent.rendNormal03.Length
			};
			Renderer[][] array15 = new Renderer[][]
			{
				customClothesComponent.rendNormal01,
				customClothesComponent.rendNormal02,
				customClothesComponent.rendNormal03
			};
			for (int num4 = 0; num4 < 3; num4++)
			{
				if (array14[num4] && array[num4] != null)
				{
					Texture texture = array[num4].RebuildTextureAndSetMaterial();
					Texture texture2 = null;
					if (array2[num4] != null)
					{
						texture2 = array2[num4].RebuildTextureAndSetMaterial();
					}
					if (null != texture)
					{
						if (array14[num4])
						{
							for (int num5 = 0; num5 < array15[num4].Length; num5++)
							{
								if (null != array15[num4][num5])
								{
									array15[num4][num5].material.SetTexture(ChaShader.MainTex, texture);
									if (null != texture2)
									{
										array15[num4][num5].material.SetTexture(ChaShader.DetailMainTex, texture2);
									}
								}
								else
								{
									result = false;
								}
							}
						}
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x001110C4 File Offset: 0x0010F4C4
		public void ChangeBreakClothes(int kind)
		{
			CmpClothes customClothesComponent = base.GetCustomClothesComponent(kind);
			if (null == customClothesComponent)
			{
				return;
			}
			if (!customClothesComponent.useBreak)
			{
				return;
			}
			ChaFileClothes.PartsInfo partsInfo = this.nowCoordinate.clothes.parts[kind];
			bool[] array = new bool[]
			{
				customClothesComponent.rendNormal01 != null && 0 != customClothesComponent.rendNormal01.Length,
				customClothesComponent.rendNormal02 != null && 0 != customClothesComponent.rendNormal02.Length,
				customClothesComponent.rendNormal03 != null && 0 != customClothesComponent.rendNormal03.Length
			};
			Renderer[][] array2 = new Renderer[][]
			{
				customClothesComponent.rendNormal01,
				customClothesComponent.rendNormal02,
				customClothesComponent.rendNormal03
			};
			for (int i = 0; i < 3; i++)
			{
				if (array[i] && array[i])
				{
					for (int j = 0; j < array2[i].Length; j++)
					{
						if (null != array2[i][j])
						{
							array2[i][j].material.SetFloat(ChaShader.ClothesBreak, 1f - partsInfo.breakRate);
						}
					}
				}
			}
			this.ChangeAlphaMaskEx();
			this.ChangeAlphaMask2();
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00111210 File Offset: 0x0010F610
		public bool UpdateClothesSiru(int kind, float frontTop, float frontBot, float downTop, float downBot)
		{
			if (base.sex == 0)
			{
				return false;
			}
			if (null == base.cmpClothes[kind])
			{
				return false;
			}
			if (!base.cmpClothes[kind].rendNormal01.IsNullOrEmpty<Renderer>())
			{
				foreach (Renderer renderer in base.cmpClothes[kind].rendNormal01)
				{
					if (!(null == renderer))
					{
						renderer.material.SetFloat(ChaShader.siruFrontTop, frontTop);
						renderer.material.SetFloat(ChaShader.siruFrontBot, frontBot);
						renderer.material.SetFloat(ChaShader.siruBackTop, downTop);
						renderer.material.SetFloat(ChaShader.siruBackBot, downBot);
					}
				}
			}
			if (!base.cmpClothes[kind].rendNormal02.IsNullOrEmpty<Renderer>())
			{
				foreach (Renderer renderer2 in base.cmpClothes[kind].rendNormal02)
				{
					if (!(null == renderer2))
					{
						renderer2.material.SetFloat(ChaShader.siruFrontTop, frontTop);
						renderer2.material.SetFloat(ChaShader.siruFrontBot, frontBot);
						renderer2.material.SetFloat(ChaShader.siruBackTop, downTop);
						renderer2.material.SetFloat(ChaShader.siruBackBot, downBot);
					}
				}
			}
			if (!base.cmpClothes[kind].rendNormal03.IsNullOrEmpty<Renderer>())
			{
				foreach (Renderer renderer3 in base.cmpClothes[kind].rendNormal03)
				{
					if (!(null == renderer3))
					{
						renderer3.material.SetFloat(ChaShader.siruFrontTop, frontTop);
						renderer3.material.SetFloat(ChaShader.siruFrontBot, frontBot);
						renderer3.material.SetFloat(ChaShader.siruBackTop, downTop);
						renderer3.material.SetFloat(ChaShader.siruBackBot, downBot);
					}
				}
			}
			return true;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00111414 File Offset: 0x0010F814
		public ChaFileClothes.PartsInfo.ColorInfo GetClothesDefaultSetting(int kind, int no)
		{
			ChaFileClothes.PartsInfo.ColorInfo colorInfo = new ChaFileClothes.PartsInfo.ColorInfo();
			CmpClothes customClothesComponent = base.GetCustomClothesComponent(kind);
			if (null != customClothesComponent)
			{
				if (no == 0)
				{
					colorInfo.baseColor = customClothesComponent.defMainColor01;
					colorInfo.patternColor = customClothesComponent.defPatternColor01;
					colorInfo.pattern = customClothesComponent.defPtnIndex01;
					colorInfo.glossPower = customClothesComponent.defGloss01;
					colorInfo.metallicPower = customClothesComponent.defMetallic01;
					Vector4 layout;
					layout.x = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout01.x);
					layout.y = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout01.y);
					layout.z = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout01.z);
					layout.w = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout01.w);
					colorInfo.layout = layout;
					colorInfo.rotation = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defRotation01);
				}
				else if (no == 1)
				{
					colorInfo.baseColor = customClothesComponent.defMainColor02;
					colorInfo.patternColor = customClothesComponent.defPatternColor02;
					colorInfo.pattern = customClothesComponent.defPtnIndex02;
					colorInfo.glossPower = customClothesComponent.defGloss02;
					colorInfo.metallicPower = customClothesComponent.defMetallic02;
					Vector4 layout2;
					layout2.x = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout02.x);
					layout2.y = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout02.y);
					layout2.z = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout02.z);
					layout2.w = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout02.w);
					colorInfo.layout = layout2;
					colorInfo.rotation = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defRotation02);
				}
				else if (no == 2)
				{
					colorInfo.baseColor = customClothesComponent.defMainColor03;
					colorInfo.patternColor = customClothesComponent.defPatternColor03;
					colorInfo.pattern = customClothesComponent.defPtnIndex03;
					colorInfo.glossPower = customClothesComponent.defGloss03;
					colorInfo.metallicPower = customClothesComponent.defMetallic03;
					Vector4 layout3;
					layout3.x = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout03.x);
					layout3.y = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout03.y);
					layout3.z = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout03.z);
					layout3.w = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout03.w);
					colorInfo.layout = layout3;
					colorInfo.rotation = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defRotation03);
				}
			}
			return colorInfo;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x00111704 File Offset: 0x0010FB04
		public void SetClothesDefaultSetting(int kind)
		{
			CmpClothes customClothesComponent = base.GetCustomClothesComponent(kind);
			if (null != customClothesComponent)
			{
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].baseColor = customClothesComponent.defMainColor01;
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].baseColor = customClothesComponent.defMainColor02;
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].baseColor = customClothesComponent.defMainColor03;
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].patternColor = customClothesComponent.defPatternColor01;
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].patternColor = customClothesComponent.defPatternColor02;
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].patternColor = customClothesComponent.defPatternColor03;
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].pattern = customClothesComponent.defPtnIndex01;
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].pattern = customClothesComponent.defPtnIndex02;
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].pattern = customClothesComponent.defPtnIndex03;
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].glossPower = customClothesComponent.defGloss01;
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].glossPower = customClothesComponent.defGloss02;
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].glossPower = customClothesComponent.defGloss03;
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].metallicPower = customClothesComponent.defMetallic01;
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].metallicPower = customClothesComponent.defMetallic02;
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].metallicPower = customClothesComponent.defMetallic03;
				Vector4 layout;
				layout.x = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout01.x);
				layout.y = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout01.y);
				layout.z = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout01.z);
				layout.w = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout01.w);
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].layout = layout;
				layout.x = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout02.x);
				layout.y = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout02.y);
				layout.z = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout02.z);
				layout.w = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout02.w);
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].layout = layout;
				layout.x = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout03.x);
				layout.y = Mathf.InverseLerp(20f, 1f, customClothesComponent.defLayout03.y);
				layout.z = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout03.z);
				layout.w = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defLayout03.w);
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].layout = layout;
				this.nowCoordinate.clothes.parts[kind].colorInfo[0].rotation = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defRotation01);
				this.nowCoordinate.clothes.parts[kind].colorInfo[1].rotation = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defRotation02);
				this.nowCoordinate.clothes.parts[kind].colorInfo[2].rotation = Mathf.InverseLerp(-1f, 1f, customClothesComponent.defRotation03);
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x00111BC4 File Offset: 0x0010FFC4
		public void ResetDynamicBoneClothes(bool includeInactive = false)
		{
			if (base.cmpClothes == null)
			{
				return;
			}
			for (int i = 0; i < base.cmpClothes.Length; i++)
			{
				if (!(null == base.cmpClothes[i]))
				{
					base.cmpClothes[i].ResetDynamicBones(includeInactive);
				}
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x00111C1C File Offset: 0x0011001C
		// (set) Token: 0x06002F3E RID: 12094 RVA: 0x00111C24 File Offset: 0x00110024
		private int ShapeBodyNum { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x00111C2D File Offset: 0x0011002D
		// (set) Token: 0x06002F40 RID: 12096 RVA: 0x00111C35 File Offset: 0x00110035
		public ShapeInfoBase sibBody { get; set; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x00111C3E File Offset: 0x0011003E
		// (set) Token: 0x06002F42 RID: 12098 RVA: 0x00111C46 File Offset: 0x00110046
		private bool changeShapeBodyMask { get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x00111C4F File Offset: 0x0011004F
		// (set) Token: 0x06002F44 RID: 12100 RVA: 0x00111C57 File Offset: 0x00110057
		public BustSoft bustSoft { get; private set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x00111C60 File Offset: 0x00110060
		// (set) Token: 0x06002F46 RID: 12102 RVA: 0x00111C68 File Offset: 0x00110068
		public BustGravity bustGravity { get; private set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x00111C71 File Offset: 0x00110071
		// (set) Token: 0x06002F48 RID: 12104 RVA: 0x00111C79 File Offset: 0x00110079
		public bool[] updateCMBodyTex { get; private set; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x00111C82 File Offset: 0x00110082
		// (set) Token: 0x06002F4A RID: 12106 RVA: 0x00111C8A File Offset: 0x0011008A
		public bool[] updateCMBodyColor { get; private set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x00111C93 File Offset: 0x00110093
		// (set) Token: 0x06002F4C RID: 12108 RVA: 0x00111C9B File Offset: 0x0011009B
		public bool[] updateCMBodyGloss { get; private set; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x00111CA4 File Offset: 0x001100A4
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x00111CAC File Offset: 0x001100AC
		public bool[] updateCMBodyLayout { get; private set; }

		// Token: 0x06002F4F RID: 12111 RVA: 0x00111CB5 File Offset: 0x001100B5
		protected void InitializeControlCustomBodyAll()
		{
			this.ShapeBodyNum = ChaFileDefine.cf_bodyshapename.Length;
			this.InitializeControlCustomBodyObject();
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x00111CCC File Offset: 0x001100CC
		protected void InitializeControlCustomBodyObject()
		{
			this.sibBody = new ShapeBodyInfoFemale();
			this.changeShapeBodyMask = false;
			this.bustSoft = new BustSoft(this);
			this.bustGravity = new BustGravity(this);
			int num = Enum.GetNames(typeof(ChaControl.BodyTexKind)).Length;
			this.updateCMBodyTex = new bool[num];
			this.updateCMBodyColor = new bool[num];
			this.updateCMBodyGloss = new bool[num];
			this.updateCMBodyLayout = new bool[num];
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x00111D45 File Offset: 0x00110145
		protected void ReleaseControlCustomBodyAll()
		{
			this.ReleaseControlCustomBodyObject(true);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x00111D4E File Offset: 0x0011014E
		protected void ReleaseControlCustomBodyObject(bool init = true)
		{
			if (this.sibBody != null)
			{
				this.sibBody.ReleaseShapeInfo();
			}
			if (init)
			{
				this.InitializeControlCustomBodyObject();
			}
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00111D72 File Offset: 0x00110172
		public void AddUpdateCMBodyTexFlags(bool inpBase, bool inpPaint01, bool inpPaint02, bool inpSunburn)
		{
			if (inpBase)
			{
				this.updateCMBodyTex[0] = inpBase;
			}
			if (inpPaint01)
			{
				this.updateCMBodyTex[1] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMBodyTex[2] = inpPaint02;
			}
			if (inpSunburn)
			{
				this.updateCMBodyTex[3] = inpSunburn;
			}
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x00111DB2 File Offset: 0x001101B2
		public void AddUpdateCMBodyColorFlags(bool inpBase, bool inpPaint01, bool inpPaint02, bool inpSunburn)
		{
			if (inpBase)
			{
				this.updateCMBodyColor[0] = inpBase;
			}
			if (inpPaint01)
			{
				this.updateCMBodyColor[1] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMBodyColor[2] = inpPaint02;
			}
			if (inpSunburn)
			{
				this.updateCMBodyColor[3] = inpSunburn;
			}
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x00111DF2 File Offset: 0x001101F2
		public void AddUpdateCMBodyGlossFlags(bool inpPaint01, bool inpPaint02)
		{
			if (inpPaint01)
			{
				this.updateCMBodyGloss[1] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMBodyGloss[2] = inpPaint02;
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x00111E12 File Offset: 0x00110212
		public void AddUpdateCMBodyLayoutFlags(bool inpPaint01, bool inpPaint02)
		{
			if (inpPaint01)
			{
				this.updateCMBodyLayout[1] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMBodyLayout[2] = inpPaint02;
			}
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x00111E34 File Offset: 0x00110234
		protected bool InitBaseCustomTextureBody()
		{
			if (base.customTexCtrlBody != null)
			{
				base.customTexCtrlBody.Release();
				base.customTexCtrlBody = null;
			}
			string drawMatName = ChaABDefine.BodyMaterialAsset((int)base.sex);
			base.customTexCtrlBody = new CustomTextureControl(2, "abdata", "chara/mm_base.unity3d", drawMatName, base.objRoot.transform);
			base.customTexCtrlBody.Initialize(0, "abdata", "chara/mm_base.unity3d", "create_skin_body", 4096, 4096, RenderTextureFormat.ARGB32);
			base.customTexCtrlBody.Initialize(1, "abdata", "chara/mm_base.unity3d", "create_skin detail_body", 4096, 4096, RenderTextureFormat.ARGB32);
			return true;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x00111EDC File Offset: 0x001102DC
		public bool CreateBodyTexture()
		{
			bool flag = false;
			bool flag2 = false;
			CustomTextureCreate customTextureCreate = base.customTexCtrlBody.createCustomTex[0];
			CustomTextureCreate customTextureCreate2 = base.customTexCtrlBody.createCustomTex[1];
			if (this.updateCMBodyTex[0])
			{
				if (this.SetCreateTexture(customTextureCreate, true, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_b : ChaListDefine.CategoryNo.mt_skin_b, base.fileBody.skinId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.MainTex, -1))
				{
					flag = true;
				}
				Texture2D texture2D = CommonLib.LoadAsset<Texture2D>("chara/etc.unity3d", "black4096", false, "abdata");
				Singleton<Character>.Instance.AddLoadAssetBundle("chara/etc.unity3d", "abdata");
				if (null != texture2D)
				{
					customTextureCreate2.SetMainTexture(texture2D);
					flag2 = true;
				}
				this.ChangeTexture(base.customTexCtrlBody.matDraw, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_detail_b : ChaListDefine.CategoryNo.mt_detail_b, base.fileBody.detailId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.OcclusionMapTex, ChaShader.SkinOcclusionMapTex, string.Empty);
				this.ChangeTexture(base.customTexCtrlBody.matDraw, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_detail_b : ChaListDefine.CategoryNo.mt_detail_b, base.fileBody.detailId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.NormalMapTex, ChaShader.SkinDetailTex, string.Empty);
				this.updateCMBodyTex[0] = false;
			}
			if (this.updateCMBodyColor[0])
			{
				customTextureCreate.SetColor(ChaShader.SkinColor, base.fileBody.skinColor);
				flag = true;
				this.updateCMBodyColor[0] = false;
			}
			if (this.updateCMBodyTex[3])
			{
				if (this.SetCreateTexture(customTextureCreate, false, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_sunburn : ChaListDefine.CategoryNo.mt_sunburn, base.fileBody.sunburnId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.SunburnTex))
				{
					flag = true;
				}
				this.updateCMBodyTex[3] = false;
			}
			if (this.updateCMBodyColor[3])
			{
				customTextureCreate.SetColor(ChaShader.SunburnColor, base.fileBody.sunburnColor);
				this.updateCMBodyColor[3] = false;
				flag = true;
			}
			int[] array = new int[]
			{
				1,
				2
			};
			int[] array2 = new int[]
			{
				ChaShader.Paint01Tex,
				ChaShader.Paint02Tex
			};
			int[] array3 = new int[]
			{
				ChaShader.Paint01Color,
				ChaShader.Paint02Color
			};
			int[] array4 = new int[]
			{
				ChaShader.Paint01Gloass,
				ChaShader.Paint02Gloass
			};
			int[] array5 = new int[]
			{
				ChaShader.Paint01Metallic,
				ChaShader.Paint02Metallic
			};
			int[] array6 = new int[]
			{
				ChaShader.Paint01Layout,
				ChaShader.Paint02Layout
			};
			int[] array7 = new int[]
			{
				ChaShader.Paint01Rot,
				ChaShader.Paint02Rot
			};
			for (int i = 0; i < 2; i++)
			{
				if (this.updateCMBodyTex[array[i]])
				{
					if (this.SetCreateTexture(customTextureCreate, false, ChaListDefine.CategoryNo.st_paint, base.fileBody.paintInfo[i].id, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, array2[i]))
					{
						flag = true;
					}
					if (this.SetCreateTexture(customTextureCreate2, false, ChaListDefine.CategoryNo.st_paint, base.fileBody.paintInfo[i].id, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.GlossTex, array2[i]))
					{
						flag2 = true;
					}
					this.updateCMBodyTex[array[i]] = false;
				}
				if (this.updateCMBodyColor[array[i]])
				{
					customTextureCreate.SetColor(array3[i], base.fileBody.paintInfo[i].color);
					this.updateCMBodyColor[array[i]] = false;
					flag = true;
				}
				if (this.updateCMBodyGloss[array[i]])
				{
					customTextureCreate2.SetFloat(array4[i], base.fileBody.paintInfo[i].glossPower);
					customTextureCreate2.SetFloat(array5[i], base.fileBody.paintInfo[i].metallicPower);
					this.updateCMBodyGloss[array[i]] = false;
					flag2 = true;
				}
				ListInfoBase listInfo = base.lstCtrl.GetListInfo(ChaListDefine.CategoryNo.bodypaint_layout, base.fileBody.paintInfo[i].layoutId);
				if (listInfo != null)
				{
					float a = listInfo.GetInfoFloat(ChaListDefine.KeyType.CenterX) + listInfo.GetInfoFloat(ChaListDefine.KeyType.MoveX);
					float b = listInfo.GetInfoFloat(ChaListDefine.KeyType.CenterX) - listInfo.GetInfoFloat(ChaListDefine.KeyType.MoveX);
					float a2 = listInfo.GetInfoFloat(ChaListDefine.KeyType.CenterY) + listInfo.GetInfoFloat(ChaListDefine.KeyType.MoveY);
					float b2 = listInfo.GetInfoFloat(ChaListDefine.KeyType.CenterY) - listInfo.GetInfoFloat(ChaListDefine.KeyType.MoveY);
					float a3 = listInfo.GetInfoFloat(ChaListDefine.KeyType.CenterScale) + listInfo.GetInfoFloat(ChaListDefine.KeyType.AddScale);
					float b3 = listInfo.GetInfoFloat(ChaListDefine.KeyType.CenterScale) - listInfo.GetInfoFloat(ChaListDefine.KeyType.AddScale);
					Vector4 value;
					value.x = Mathf.Lerp(a3, b3, base.fileBody.paintInfo[i].layout.x);
					value.y = Mathf.Lerp(a3, b3, base.fileBody.paintInfo[i].layout.y);
					value.z = Mathf.Lerp(a, b, base.fileBody.paintInfo[i].layout.z);
					value.w = Mathf.Lerp(a2, b2, base.fileBody.paintInfo[i].layout.w);
					float value2 = Mathf.Lerp(1f, -1f, base.fileBody.paintInfo[i].rotation);
					customTextureCreate.SetVector4(array6[i], value);
					customTextureCreate.SetFloat(array7[i], value2);
					customTextureCreate2.SetVector4(array6[i], value);
					customTextureCreate2.SetFloat(array7[i], value2);
					this.updateCMBodyLayout[array[i]] = false;
					flag = true;
					flag2 = true;
				}
			}
			if (flag)
			{
				base.customTexCtrlBody.SetNewCreateTexture(0, ChaShader.SkinTex);
			}
			if (flag2)
			{
				base.customTexCtrlBody.SetNewCreateTexture(1, ChaShader.SkinCreateDetailTex);
			}
			if (base.releaseCustomInputTexture)
			{
				this.ReleaseBodyCustomTexture();
			}
			return true;
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x001124AD File Offset: 0x001108AD
		public bool ChangeBodyDetailPower()
		{
			base.customTexCtrlBody.matDraw.SetFloat(ChaShader.SkinDetailPower, base.fileBody.detailPower);
			return true;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x001124D0 File Offset: 0x001108D0
		public bool ChangeBodyGlossPower()
		{
			float value = Mathf.Lerp(0f, 0.8f, base.fileBody.skinGlossPower) + 0.2f * base.fileStatus.skinTuyaRate;
			base.customTexCtrlBody.matDraw.SetFloat(ChaShader.Gloss, value);
			return true;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x00112521 File Offset: 0x00110921
		public bool ChangeBodyMetallicPower()
		{
			base.customTexCtrlBody.matDraw.SetFloat(ChaShader.Metallic, base.fileBody.skinMetallicPower);
			return true;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x00112544 File Offset: 0x00110944
		public bool ChangeNipKind()
		{
			this.ChangeTexture(base.customTexCtrlBody.matDraw, ChaListDefine.CategoryNo.st_nip, base.fileBody.nipId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.NipTex, string.Empty);
			return true;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x00112583 File Offset: 0x00110983
		public bool ChangeNipColor()
		{
			base.customTexCtrlBody.matDraw.SetColor(ChaShader.NipColor, base.fileBody.nipColor);
			return true;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x001125A6 File Offset: 0x001109A6
		public bool ChangeNipGloss()
		{
			base.customTexCtrlBody.matDraw.SetFloat(ChaShader.NipGloss, base.fileBody.nipGlossPower);
			return true;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x001125C9 File Offset: 0x001109C9
		public bool ChangeNipScale()
		{
			base.customTexCtrlBody.matDraw.SetFloat(ChaShader.NipScale, base.fileBody.areolaSize);
			return true;
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x001125EC File Offset: 0x001109EC
		public bool ChangeUnderHairKind()
		{
			this.ChangeTexture(base.customTexCtrlBody.matDraw, ChaListDefine.CategoryNo.st_underhair, base.fileBody.underhairId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.UnderhairTex, string.Empty);
			return true;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x0011262B File Offset: 0x00110A2B
		public bool ChangeUnderHairColor()
		{
			base.customTexCtrlBody.matDraw.SetColor(ChaShader.UnderhairColor, base.fileBody.underhairColor);
			return true;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0011264E File Offset: 0x00110A4E
		public bool ChangeNailColor()
		{
			base.customTexCtrlBody.matDraw.SetColor(ChaShader.NailColor, base.fileBody.nailColor);
			return true;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x00112671 File Offset: 0x00110A71
		public bool ChangeNailGloss()
		{
			base.customTexCtrlBody.matDraw.SetFloat(ChaShader.NailGloss, base.fileBody.nailGlossPower);
			return true;
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x00112694 File Offset: 0x00110A94
		public bool SetBodyBaseMaterial()
		{
			if (null == base.customMatBody)
			{
				return false;
			}
			if (null == base.cmpBody)
			{
				return false;
			}
			Renderer rendBody = base.cmpBody.targetCustom.rendBody;
			return !(null == rendBody) && this.SetBaseMaterial(rendBody, base.customMatBody);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x001126F4 File Offset: 0x00110AF4
		public bool ReleaseBodyCustomTexture()
		{
			if (base.customTexCtrlBody == null)
			{
				return false;
			}
			CustomTextureCreate customTextureCreate = base.customTexCtrlBody.createCustomTex[0];
			CustomTextureCreate customTextureCreate2 = base.customTexCtrlBody.createCustomTex[1];
			customTextureCreate.SetTexture(ChaShader.MainTex, null);
			customTextureCreate.SetTexture(ChaShader.SunburnTex, null);
			customTextureCreate.SetTexture(ChaShader.Paint01Tex, null);
			customTextureCreate.SetTexture(ChaShader.Paint02Tex, null);
			customTextureCreate2.SetTexture(ChaShader.MainTex, null);
			customTextureCreate2.SetTexture(ChaShader.SunburnTex, null);
			customTextureCreate2.SetTexture(ChaShader.Paint01Tex, null);
			customTextureCreate2.SetTexture(ChaShader.Paint02Tex, null);
			UnityEngine.Resources.UnloadUnusedAssets();
			return true;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x00112794 File Offset: 0x00110B94
		public void ChangeCustomBodyWithoutCustomTexture()
		{
			this.ChangeBodyGlossPower();
			this.ChangeBodyMetallicPower();
			this.ChangeBodyDetailPower();
			this.ChangeNipKind();
			this.ChangeNipColor();
			this.ChangeNipGloss();
			this.ChangeNipScale();
			this.ChangeUnderHairKind();
			this.ChangeUnderHairColor();
			this.ChangeNailColor();
			this.ChangeNailGloss();
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x001127F0 File Offset: 0x00110BF0
		public bool InitShapeBody(Transform trfBone)
		{
			if (this.sibBody == null)
			{
				return false;
			}
			if (null == trfBone)
			{
				return false;
			}
			this.sibBody.InitShapeInfo("abdata", "list/customshape.unity3d", "list/customshape.unity3d", "cf_anmShapeBody", "cf_custombody", trfBone);
			float[] array = new float[base.fileBody.shapeValueBody.Length];
			base.chaFile.custom.body.shapeValueBody.CopyTo(array, 0);
			array[32] = Mathf.Lerp(0f, 0.8f, array[32]) + 0.2f * base.fileStatus.nipStandRate;
			if (base.sex == 0 || base.isPlayer)
			{
				array[0] = 0.75f;
			}
			for (int i = 0; i < this.ShapeBodyNum; i++)
			{
				this.sibBody.ChangeValue(i, array[i]);
			}
			base.updateShapeBody = true;
			base.updateBustSize = true;
			base.reSetupDynamicBoneBust = true;
			return true;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x001128F0 File Offset: 0x00110CF0
		public void ReleaseShapeBody()
		{
			if (this.sibBody == null)
			{
				return;
			}
			this.sibBody.ReleaseShapeInfo();
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0011290C File Offset: 0x00110D0C
		public bool SetShapeBodyValue(int index, float value)
		{
			if (index >= this.ShapeBodyNum)
			{
				return false;
			}
			base.fileBody.shapeValueBody[index] = value;
			float value2 = value;
			if (index == 32)
			{
				value2 = Mathf.Lerp(0f, 0.8f, value) + 0.2f * base.fileStatus.nipStandRate;
			}
			if (index == 0 && (base.sex == 0 || base.isPlayer))
			{
				value2 = 0.75f;
			}
			if (this.sibBody != null && this.sibBody.InitEnd)
			{
				this.sibBody.ChangeValue(index, value2);
			}
			base.updateShapeBody = true;
			base.updateBustSize = true;
			base.reSetupDynamicBoneBust = true;
			return true;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x001129C8 File Offset: 0x00110DC8
		public bool UpdateShapeBodyValueFromCustomInfo()
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return false;
			}
			float[] array = new float[base.fileBody.shapeValueBody.Length];
			base.fileBody.shapeValueBody.CopyTo(array, 0);
			array[32] = Mathf.Lerp(0f, 0.8f, array[32]) + 0.2f * base.fileStatus.nipStandRate;
			if (base.sex == 0 || base.isPlayer)
			{
				array[0] = 0.75f;
			}
			for (int i = 0; i < base.fileBody.shapeValueBody.Length; i++)
			{
				this.sibBody.ChangeValue(i, array[i]);
			}
			base.updateShapeBody = true;
			base.updateBustSize = true;
			base.reSetupDynamicBoneBust = true;
			return true;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x00112AA2 File Offset: 0x00110EA2
		public float GetShapeBodyValue(int index)
		{
			if (index >= this.ShapeBodyNum)
			{
				return 0f;
			}
			return base.fileBody.shapeValueBody[index];
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x00112AC4 File Offset: 0x00110EC4
		public void UpdateShapeBody()
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return;
			}
			ShapeBodyInfoFemale shapeBodyInfoFemale = this.sibBody as ShapeBodyInfoFemale;
			if (shapeBodyInfoFemale == null)
			{
				return;
			}
			shapeBodyInfoFemale.updateMask = 31;
			this.sibBody.Update();
			if (!this.changeShapeBodyMask)
			{
				return;
			}
			float[] array = new float[ChaFileDefine.cf_BustShapeMaskID.Length];
			int[] array2 = new int[]
			{
				1,
				2
			};
			float[] array3 = new float[]
			{
				0.5f,
				0.5f,
				0.5f,
				0.5f,
				0.5f,
				0.5f,
				0.5f,
				0.5f
			};
			for (int i = 0; i < 2; i++)
			{
				int num;
				for (int j = 0; j < ChaFileDefine.cf_BustShapeMaskID.Length; j++)
				{
					num = ChaFileDefine.cf_BustShapeMaskID[j];
					array[j] = ((!base.fileStatus.disableBustShapeMask[i, j]) ? base.fileBody.shapeValueBody[num] : array3[j]);
				}
				int num2 = 7;
				num = ChaFileDefine.cf_BustShapeMaskID[num2];
				array[num2] = ((!base.fileStatus.disableBustShapeMask[i, num2]) ? (Mathf.Lerp(0f, 0.8f, base.fileBody.shapeValueBody[num]) + 0.2f * base.fileStatus.nipStandRate) : 0.5f);
				for (int k = 0; k < ChaFileDefine.cf_BustShapeMaskID.Length; k++)
				{
					this.sibBody.ChangeValue(ChaFileDefine.cf_BustShapeMaskID[k], array[k]);
				}
				shapeBodyInfoFemale.updateMask = array2[i];
				this.sibBody.Update();
			}
			this.changeShapeBodyMask = false;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x00112C70 File Offset: 0x00111070
		public void UpdateAlwaysShapeBody()
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return;
			}
			this.sibBody.UpdateAlways();
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x00112C99 File Offset: 0x00111099
		public void UpdateShapeBodyCalcForce()
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return;
			}
			this.sibBody.ForceUpdate();
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x00112CC4 File Offset: 0x001110C4
		public void DisableShapeBodyID(int LR, int id, bool disable)
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return;
			}
			if (id >= ChaFileDefine.cf_BustShapeMaskID.Length)
			{
				return;
			}
			this.changeShapeBodyMask = true;
			base.updateShapeBody = true;
			if (LR == 0)
			{
				base.fileStatus.disableBustShapeMask[0, id] = disable;
			}
			else if (LR == 1)
			{
				base.fileStatus.disableBustShapeMask[1, id] = disable;
			}
			else
			{
				base.fileStatus.disableBustShapeMask[0, id] = disable;
				base.fileStatus.disableBustShapeMask[1, id] = disable;
			}
			base.reSetupDynamicBoneBust = true;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x00112D74 File Offset: 0x00111174
		public void DisableShapeBust(int LR, bool disable)
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return;
			}
			this.changeShapeBodyMask = true;
			base.updateShapeBody = true;
			if (LR == 0)
			{
				for (int i = 0; i < ChaFileDefine.cf_ShapeMaskBust.Length; i++)
				{
					base.fileStatus.disableBustShapeMask[0, ChaFileDefine.cf_ShapeMaskBust[i]] = disable;
				}
			}
			else if (LR == 1)
			{
				for (int j = 0; j < ChaFileDefine.cf_ShapeMaskBust.Length; j++)
				{
					base.fileStatus.disableBustShapeMask[1, ChaFileDefine.cf_ShapeMaskBust[j]] = disable;
				}
			}
			else
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = 0; l < ChaFileDefine.cf_ShapeMaskBust.Length; l++)
					{
						base.fileStatus.disableBustShapeMask[k, ChaFileDefine.cf_ShapeMaskBust[l]] = disable;
					}
				}
			}
			base.reSetupDynamicBoneBust = true;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x00112E70 File Offset: 0x00111270
		public void DisableShapeNip(int LR, bool disable)
		{
			if (this.sibBody == null || !this.sibBody.InitEnd)
			{
				return;
			}
			this.changeShapeBodyMask = true;
			base.updateShapeBody = true;
			if (LR == 0)
			{
				for (int i = 0; i < ChaFileDefine.cf_ShapeMaskNip.Length; i++)
				{
					base.fileStatus.disableBustShapeMask[0, ChaFileDefine.cf_ShapeMaskNip[i]] = disable;
				}
			}
			else if (LR == 1)
			{
				for (int j = 0; j < ChaFileDefine.cf_ShapeMaskNip.Length; j++)
				{
					base.fileStatus.disableBustShapeMask[1, ChaFileDefine.cf_ShapeMaskNip[j]] = disable;
				}
			}
			else
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = 0; l < ChaFileDefine.cf_ShapeMaskNip.Length; l++)
					{
						base.fileStatus.disableBustShapeMask[k, ChaFileDefine.cf_ShapeMaskNip[l]] = disable;
					}
				}
			}
			base.reSetupDynamicBoneBust = true;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x00112F6A File Offset: 0x0011136A
		public void UpdateBustSoftnessAndGravity()
		{
			this.UpdateBustSoftness();
			this.UpdateBustGravity();
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x00112F7A File Offset: 0x0011137A
		public void ChangeBustSoftness(float soft)
		{
			if (this.bustSoft != null)
			{
				this.bustSoft.Change(soft, new int[1]);
				base.reSetupDynamicBoneBust = true;
			}
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x00112FA0 File Offset: 0x001113A0
		public bool UpdateBustSoftness()
		{
			if (this.bustSoft != null)
			{
				this.bustSoft.ReCalc(new int[1]);
				base.reSetupDynamicBoneBust = true;
				return true;
			}
			return false;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x00112FC8 File Offset: 0x001113C8
		public void ChangeBustGravity(float gravity)
		{
			if (this.bustGravity != null)
			{
				this.bustGravity.Change(gravity, new int[1]);
				base.reSetupDynamicBoneBust = true;
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x00112FEE File Offset: 0x001113EE
		public bool UpdateBustGravity()
		{
			if (this.bustGravity != null)
			{
				this.bustGravity.ReCalc(new int[1]);
				base.reSetupDynamicBoneBust = true;
				return true;
			}
			return false;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x00113016 File Offset: 0x00111416
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x0011301E File Offset: 0x0011141E
		private int ShapeFaceNum { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x00113027 File Offset: 0x00111427
		// (set) Token: 0x06002F7A RID: 12154 RVA: 0x0011302F File Offset: 0x0011142F
		private ShapeInfoBase sibFace { get; set; }

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x00113038 File Offset: 0x00111438
		// (set) Token: 0x06002F7C RID: 12156 RVA: 0x00113040 File Offset: 0x00111440
		public bool[] updateCMFaceTex { get; private set; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x00113049 File Offset: 0x00111449
		// (set) Token: 0x06002F7E RID: 12158 RVA: 0x00113051 File Offset: 0x00111451
		public bool[] updateCMFaceColor { get; private set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x0011305A File Offset: 0x0011145A
		// (set) Token: 0x06002F80 RID: 12160 RVA: 0x00113062 File Offset: 0x00111462
		public bool[] updateCMFaceGloss { get; private set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x0011306B File Offset: 0x0011146B
		// (set) Token: 0x06002F82 RID: 12162 RVA: 0x00113073 File Offset: 0x00111473
		public bool[] updateCMFaceLayout { get; private set; }

		// Token: 0x06002F83 RID: 12163 RVA: 0x0011307C File Offset: 0x0011147C
		protected void InitializeControlCustomFaceAll()
		{
			this.ShapeFaceNum = ChaFileDefine.cf_headshapename.Length;
			this.InitializeControlCustomFaceObject();
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x00113094 File Offset: 0x00111494
		protected void InitializeControlCustomFaceObject()
		{
			this.sibFace = new ShapeHeadInfoFemale();
			int num = Enum.GetNames(typeof(ChaControl.FaceTexKind)).Length;
			this.updateCMFaceTex = new bool[num];
			this.updateCMFaceColor = new bool[num];
			this.updateCMFaceGloss = new bool[num];
			this.updateCMFaceLayout = new bool[num];
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x001130EE File Offset: 0x001114EE
		protected void ReleaseControlCustomFaceAll()
		{
			this.ReleaseControlCustomFaceObject(true);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x001130F7 File Offset: 0x001114F7
		protected void ReleaseControlCustomFaceObject(bool init = true)
		{
			if (this.sibFace != null)
			{
				this.sibFace.ReleaseShapeInfo();
			}
			if (init)
			{
				this.InitializeControlCustomFaceObject();
			}
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0011311C File Offset: 0x0011151C
		private bool SetBaseMaterial(Renderer rend, Material mat)
		{
			if (null == mat || null == rend)
			{
				return false;
			}
			int num = rend.materials.Length;
			Material[] array;
			if (num == 0)
			{
				array = new Material[]
				{
					mat
				};
			}
			else
			{
				array = new Material[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = rend.materials[i];
				}
				Material material = array[0];
				array[0] = mat;
				if (material != mat)
				{
					UnityEngine.Object.Destroy(material);
				}
			}
			rend.materials = array;
			return true;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x001131AC File Offset: 0x001115AC
		private bool SetCreateTexture(CustomTextureCreate ctc, bool main, ChaListDefine.CategoryNo type, int id, ChaListDefine.KeyType manifestKey, ChaListDefine.KeyType assetBundleKey, ChaListDefine.KeyType assetKey, int propertyID)
		{
			ListInfoBase listInfo = base.lstCtrl.GetListInfo(type, id);
			if (listInfo != null)
			{
				string text = listInfo.GetInfo(manifestKey);
				if ("0" == text)
				{
					text = string.Empty;
				}
				string info = listInfo.GetInfo(assetBundleKey);
				string info2 = listInfo.GetInfo(assetKey);
				Texture2D texture2D = null;
				if ("0" != info && "0" != info2)
				{
					texture2D = CommonLib.LoadAsset<Texture2D>(info, info2, false, text);
					Singleton<Character>.Instance.AddLoadAssetBundle(info, text);
				}
				if (main)
				{
					ctc.SetMainTexture(texture2D);
				}
				else
				{
					ctc.SetTexture(propertyID, texture2D);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0011325C File Offset: 0x0011165C
		private void ChangeTexture(Renderer rend, ChaListDefine.CategoryNo type, int id, ChaListDefine.KeyType manifestKey, ChaListDefine.KeyType assetBundleKey, ChaListDefine.KeyType assetKey, int propertyID, string addStr = "")
		{
			if (null == rend)
			{
				return;
			}
			this.ChangeTexture(rend.material, type, id, manifestKey, assetBundleKey, assetKey, propertyID, addStr);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x00113290 File Offset: 0x00111690
		private void ChangeTexture(Material mat, ChaListDefine.CategoryNo type, int id, ChaListDefine.KeyType manifestKey, ChaListDefine.KeyType assetBundleKey, ChaListDefine.KeyType assetKey, int propertyID, string addStr = "")
		{
			if (null == mat)
			{
				return;
			}
			Texture2D texture = this.GetTexture(type, id, manifestKey, assetBundleKey, assetKey, addStr);
			mat.SetTexture(propertyID, texture);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x001132C4 File Offset: 0x001116C4
		private Texture2D GetTexture(ChaListDefine.CategoryNo type, int id, ChaListDefine.KeyType manifestKey, ChaListDefine.KeyType assetBundleKey, ChaListDefine.KeyType assetKey, string addStr = "")
		{
			ListInfoBase listInfo = base.lstCtrl.GetListInfo(type, id);
			if (listInfo != null)
			{
				string text = listInfo.GetInfo(manifestKey);
				if ("0" == text)
				{
					text = string.Empty;
				}
				string info = listInfo.GetInfo(assetBundleKey);
				string info2 = listInfo.GetInfo(assetKey);
				Texture2D texture2D = null;
				if ("0" != info && "0" != info2)
				{
					if (!addStr.IsNullOrEmpty())
					{
						string assetName = info2 + addStr;
						texture2D = CommonLib.LoadAsset<Texture2D>(info, assetName, false, text);
					}
					if (null == texture2D)
					{
						texture2D = CommonLib.LoadAsset<Texture2D>(info, info2, false, text);
					}
					Singleton<Character>.Instance.AddLoadAssetBundle(info, text);
				}
				return texture2D;
			}
			return null;
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x00113384 File Offset: 0x00111784
		public void AddUpdateCMFaceTexFlags(bool inpBase, bool inpEyeshadow, bool inpPaint01, bool inpPaint02, bool inpCheek, bool inpLip, bool inpMole)
		{
			if (inpBase)
			{
				this.updateCMFaceTex[0] = inpBase;
			}
			if (inpEyeshadow)
			{
				this.updateCMFaceTex[1] = inpEyeshadow;
			}
			if (inpPaint01)
			{
				this.updateCMFaceTex[2] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMFaceTex[3] = inpPaint02;
			}
			if (inpCheek)
			{
				this.updateCMFaceTex[4] = inpCheek;
			}
			if (inpLip)
			{
				this.updateCMFaceTex[5] = inpLip;
			}
			if (inpMole)
			{
				this.updateCMFaceTex[6] = inpMole;
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x00113404 File Offset: 0x00111804
		public void AddUpdateCMFaceColorFlags(bool inpBase, bool inpEyeshadow, bool inpPaint01, bool inpPaint02, bool inpCheek, bool inpLip, bool inpMole)
		{
			if (inpBase)
			{
				this.updateCMFaceColor[0] = inpBase;
			}
			if (inpEyeshadow)
			{
				this.updateCMFaceColor[1] = inpEyeshadow;
			}
			if (inpPaint01)
			{
				this.updateCMFaceColor[2] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMFaceColor[3] = inpPaint02;
			}
			if (inpCheek)
			{
				this.updateCMFaceColor[4] = inpCheek;
			}
			if (inpLip)
			{
				this.updateCMFaceColor[5] = inpLip;
			}
			if (inpMole)
			{
				this.updateCMFaceColor[6] = inpMole;
			}
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x00113484 File Offset: 0x00111884
		public void AddUpdateCMFaceGlossFlags(bool inpEyeshadow, bool inpPaint01, bool inpPaint02, bool inpCheek, bool inpLip)
		{
			if (inpEyeshadow)
			{
				this.updateCMFaceGloss[1] = inpEyeshadow;
			}
			if (inpPaint01)
			{
				this.updateCMFaceGloss[2] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMFaceGloss[3] = inpPaint02;
			}
			if (inpCheek)
			{
				this.updateCMFaceGloss[4] = inpCheek;
			}
			if (inpLip)
			{
				this.updateCMFaceGloss[5] = inpLip;
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x001134E0 File Offset: 0x001118E0
		public void AddUpdateCMFaceLayoutFlags(bool inpPaint01, bool inpPaint02, bool inpMole)
		{
			if (inpPaint01)
			{
				this.updateCMFaceLayout[2] = inpPaint01;
			}
			if (inpPaint02)
			{
				this.updateCMFaceLayout[3] = inpPaint02;
			}
			if (inpMole)
			{
				this.updateCMFaceLayout[6] = inpMole;
			}
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00113510 File Offset: 0x00111910
		private bool InitBaseCustomTextureFace(string drawManifest, string drawAssetBundleName, string drawAssetName)
		{
			if (base.customTexCtrlFace != null)
			{
				base.customTexCtrlFace.Release();
				base.customTexCtrlFace = null;
			}
			base.customTexCtrlFace = new CustomTextureControl(2, drawManifest, drawAssetBundleName, drawAssetName, base.objRoot.transform);
			base.customTexCtrlFace.Initialize(0, "abdata", "chara/mm_base.unity3d", "create_skin_face", 2048, 2048, RenderTextureFormat.ARGB32);
			base.customTexCtrlFace.Initialize(1, "abdata", "chara/mm_base.unity3d", "create_skin detail_face", 2048, 2048, RenderTextureFormat.ARGB32);
			return true;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x001135A4 File Offset: 0x001119A4
		public bool CreateFaceTexture()
		{
			ChaFileFace.MakeupInfo makeup = base.fileFace.makeup;
			bool flag = false;
			bool flag2 = false;
			CustomTextureCreate customTextureCreate = base.customTexCtrlFace.createCustomTex[0];
			CustomTextureCreate customTextureCreate2 = base.customTexCtrlFace.createCustomTex[1];
			if (this.updateCMFaceTex[0])
			{
				if (this.SetCreateTexture(customTextureCreate, true, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_f : ChaListDefine.CategoryNo.mt_skin_f, base.fileFace.skinId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.MainTex, -1))
				{
					flag = true;
				}
				Texture2D texture2D = CommonLib.LoadAsset<Texture2D>("chara/etc.unity3d", "black2048", false, "abdata");
				Singleton<Character>.Instance.AddLoadAssetBundle("chara/etc.unity3d", "abdata");
				if (null != texture2D)
				{
					customTextureCreate2.SetMainTexture(texture2D);
					flag2 = true;
				}
				this.ChangeTexture(base.customTexCtrlFace.matDraw, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_f : ChaListDefine.CategoryNo.mt_skin_f, base.fileFace.skinId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.OcclusionMapTex, ChaShader.SkinOcclusionMapTex, string.Empty);
				this.ChangeTexture(base.customTexCtrlFace.matDraw, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_f : ChaListDefine.CategoryNo.mt_skin_f, base.fileFace.skinId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.NormalMapTex, ChaShader.SkinNormalMapTex, string.Empty);
				this.updateCMFaceTex[0] = false;
			}
			if (this.updateCMFaceColor[0])
			{
				customTextureCreate.SetColor(ChaShader.SkinColor, base.fileBody.skinColor);
				flag = true;
				this.updateCMFaceColor[0] = false;
			}
			if (this.updateCMFaceTex[1])
			{
				if (this.SetCreateTexture(customTextureCreate, false, ChaListDefine.CategoryNo.st_eyeshadow, makeup.eyeshadowId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.EyeshadowTex))
				{
					flag = true;
				}
				if (this.SetCreateTexture(customTextureCreate2, false, ChaListDefine.CategoryNo.st_eyeshadow, makeup.eyeshadowId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.GlossTex, ChaShader.EyeshadowTex))
				{
					flag2 = true;
				}
				this.updateCMFaceTex[1] = false;
			}
			if (this.updateCMFaceColor[1])
			{
				customTextureCreate.SetColor(ChaShader.EyeshadowColor, makeup.eyeshadowColor);
				flag = true;
				this.updateCMFaceColor[1] = false;
			}
			if (this.updateCMFaceGloss[1])
			{
				customTextureCreate2.SetFloat(ChaShader.EyeshadowGloss, makeup.eyeshadowGloss);
				flag2 = true;
				this.updateCMFaceGloss[1] = false;
			}
			int[] array = new int[]
			{
				2,
				3
			};
			int[] array2 = new int[]
			{
				ChaShader.Paint01Tex,
				ChaShader.Paint02Tex
			};
			int[] array3 = new int[]
			{
				ChaShader.Paint01Color,
				ChaShader.Paint02Color
			};
			int[] array4 = new int[]
			{
				ChaShader.Paint01Gloass,
				ChaShader.Paint02Gloass
			};
			int[] array5 = new int[]
			{
				ChaShader.Paint01Metallic,
				ChaShader.Paint02Metallic
			};
			int[] array6 = new int[]
			{
				ChaShader.Paint01Layout,
				ChaShader.Paint02Layout
			};
			int[] array7 = new int[]
			{
				ChaShader.Paint01Rot,
				ChaShader.Paint02Rot
			};
			for (int i = 0; i < 2; i++)
			{
				if (this.updateCMFaceTex[array[i]])
				{
					if (this.SetCreateTexture(customTextureCreate, false, ChaListDefine.CategoryNo.st_paint, makeup.paintInfo[i].id, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, array2[i]))
					{
						flag = true;
					}
					if (this.SetCreateTexture(customTextureCreate2, false, ChaListDefine.CategoryNo.st_paint, makeup.paintInfo[i].id, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.GlossTex, array2[i]))
					{
						flag2 = true;
					}
					this.updateCMFaceTex[array[i]] = false;
				}
				if (this.updateCMFaceColor[array[i]])
				{
					customTextureCreate.SetColor(array3[i], makeup.paintInfo[i].color);
					this.updateCMFaceColor[array[i]] = false;
					flag = true;
				}
				if (this.updateCMFaceGloss[array[i]])
				{
					customTextureCreate2.SetFloat(array4[i], makeup.paintInfo[i].glossPower);
					customTextureCreate2.SetFloat(array5[i], makeup.paintInfo[i].metallicPower);
					this.updateCMFaceGloss[array[i]] = false;
					flag2 = true;
				}
				if (this.updateCMFaceLayout[array[i]])
				{
					Vector4 zero = Vector4.zero;
					zero.x = Mathf.Lerp(10f, 1f, makeup.paintInfo[i].layout.x);
					zero.y = Mathf.Lerp(10f, 1f, makeup.paintInfo[i].layout.y);
					zero.z = Mathf.Lerp(0.28f, -0.3f, makeup.paintInfo[i].layout.z);
					zero.w = Mathf.Lerp(0.28f, -0.3f, makeup.paintInfo[i].layout.w);
					float value = Mathf.Lerp(1f, -1f, makeup.paintInfo[i].rotation);
					customTextureCreate.SetVector4(array6[i], zero);
					customTextureCreate.SetFloat(array7[i], value);
					customTextureCreate2.SetVector4(array6[i], zero);
					customTextureCreate2.SetFloat(array7[i], value);
					this.updateCMFaceLayout[array[i]] = false;
					flag = true;
					flag2 = true;
				}
			}
			if (this.updateCMFaceTex[4])
			{
				if (this.SetCreateTexture(customTextureCreate, false, ChaListDefine.CategoryNo.st_cheek, makeup.cheekId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.CheekTex))
				{
					flag = true;
				}
				if (this.SetCreateTexture(customTextureCreate2, false, ChaListDefine.CategoryNo.st_cheek, makeup.cheekId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.GlossTex, ChaShader.CheekTex))
				{
					flag2 = true;
				}
				this.updateCMFaceTex[4] = false;
			}
			if (this.updateCMFaceColor[4])
			{
				customTextureCreate.SetColor(ChaShader.CheekColor, makeup.cheekColor);
				this.updateCMFaceColor[4] = false;
				flag = true;
			}
			if (this.updateCMFaceGloss[4])
			{
				customTextureCreate2.SetFloat(ChaShader.CheekGloss, makeup.cheekGloss);
				this.updateCMFaceGloss[4] = false;
				flag2 = true;
			}
			if (this.updateCMFaceTex[5])
			{
				if (this.SetCreateTexture(customTextureCreate, false, ChaListDefine.CategoryNo.st_lip, makeup.lipId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.LipTex))
				{
					flag = true;
				}
				if (this.SetCreateTexture(customTextureCreate2, false, ChaListDefine.CategoryNo.st_lip, makeup.lipId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.GlossTex, ChaShader.LipTex))
				{
					flag2 = true;
				}
				this.updateCMFaceTex[5] = false;
			}
			if (this.updateCMFaceColor[5])
			{
				customTextureCreate.SetColor(ChaShader.LipColor, makeup.lipColor);
				this.updateCMFaceColor[5] = false;
				flag = true;
			}
			if (this.updateCMFaceGloss[5])
			{
				customTextureCreate2.SetFloat(ChaShader.LipGloss, makeup.lipGloss);
				this.updateCMFaceGloss[5] = false;
				flag2 = true;
			}
			if (this.updateCMFaceTex[6])
			{
				if (this.SetCreateTexture(customTextureCreate, false, ChaListDefine.CategoryNo.st_mole, base.fileFace.moleId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.MoleTex))
				{
					flag = true;
				}
				this.updateCMFaceTex[6] = false;
			}
			if (this.updateCMFaceColor[6])
			{
				customTextureCreate.SetColor(ChaShader.MoleColor, base.fileFace.moleColor);
				this.updateCMFaceColor[6] = false;
				flag = true;
			}
			if (this.updateCMFaceLayout[6])
			{
				Vector4 vector = customTextureCreate.GetVector4(ChaShader.MoleLayout);
				vector.x = Mathf.Lerp(5f, 1f, base.fileFace.moleLayout.x);
				vector.y = Mathf.Lerp(5f, 1f, base.fileFace.moleLayout.y);
				vector.z = Mathf.Lerp(0.3f, -0.3f, base.fileFace.moleLayout.z);
				vector.w = Mathf.Lerp(0.3f, -0.3f, base.fileFace.moleLayout.w);
				customTextureCreate.SetVector4(ChaShader.MoleLayout, vector);
				this.updateCMFaceLayout[6] = false;
				flag = true;
			}
			if (flag)
			{
				base.customTexCtrlFace.SetNewCreateTexture(0, ChaShader.SkinTex);
			}
			if (flag2)
			{
				base.customTexCtrlFace.SetNewCreateTexture(1, ChaShader.SkinCreateDetailTex);
			}
			if (base.releaseCustomInputTexture)
			{
				this.ReleaseFaceCustomTexture();
			}
			return true;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x00113DB8 File Offset: 0x001121B8
		public bool ChangeFaceGlossPower()
		{
			float value = Mathf.Lerp(0f, 0.8f, base.fileBody.skinGlossPower) + 0.2f * base.fileStatus.skinTuyaRate;
			base.customTexCtrlFace.matDraw.SetFloat(ChaShader.Gloss, value);
			return true;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x00113E09 File Offset: 0x00112209
		public bool ChangeFaceMetallicPower()
		{
			base.customTexCtrlFace.matDraw.SetFloat(ChaShader.Metallic, base.fileBody.skinMetallicPower);
			return true;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x00113E2C File Offset: 0x0011222C
		public bool ChangeFaceDetailKind()
		{
			this.ChangeTexture(base.customTexCtrlFace.matDraw, (base.sex != 0) ? ChaListDefine.CategoryNo.ft_detail_f : ChaListDefine.CategoryNo.mt_detail_f, base.fileFace.detailId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.SkinDetailTex, string.Empty);
			return true;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x00113E7D File Offset: 0x0011227D
		public bool ChangeFaceDetailPower()
		{
			base.customTexCtrlFace.matDraw.SetFloat(ChaShader.SkinDetailPower, base.fileFace.detailPower);
			return true;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x00113EA0 File Offset: 0x001122A0
		public bool ChangeEyebrowKind()
		{
			this.ChangeTexture(base.customTexCtrlFace.matDraw, ChaListDefine.CategoryNo.st_eyebrow, base.fileFace.eyebrowId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.EyebrowTex, string.Empty);
			return true;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x00113EDF File Offset: 0x001122DF
		public bool ChangeEyebrowColor()
		{
			base.customTexCtrlFace.matDraw.SetColor(ChaShader.EyebrowColor, base.fileFace.eyebrowColor);
			return true;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x00113F04 File Offset: 0x00112304
		public bool ChangeEyebrowLayout()
		{
			Vector4 vector = base.customTexCtrlFace.matDraw.GetVector(ChaShader.EyebrowLayout);
			vector.x = Mathf.Lerp(-0.2f, 0.2f, base.fileFace.eyebrowLayout.x);
			vector.y = Mathf.Lerp(0.16f, 0f, base.fileFace.eyebrowLayout.y);
			vector.z = Mathf.Lerp(2f, 0.5f, base.fileFace.eyebrowLayout.z);
			vector.w = Mathf.Lerp(2f, 0.5f, base.fileFace.eyebrowLayout.w);
			base.customTexCtrlFace.matDraw.SetVector(ChaShader.EyebrowLayout, vector);
			return true;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x00113FE4 File Offset: 0x001123E4
		public bool ChangeEyebrowTilt()
		{
			float value = Mathf.Lerp(-0.15f, 0.15f, base.fileFace.eyebrowTilt);
			base.customTexCtrlFace.matDraw.SetFloat(ChaShader.EyebrowTilt, value);
			return true;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x00114024 File Offset: 0x00112424
		public bool ChangeWhiteEyesColor(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							rendEyes[i].material.SetColor(ChaShader.EyesWhiteColor, base.fileFace.pupil[i].whiteColor);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x001140D4 File Offset: 0x001124D4
		public bool ChangeEyesKind(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							this.ChangeTexture(rendEyes[i], ChaListDefine.CategoryNo.st_eye, base.fileFace.pupil[i].pupilId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.PupilTex, string.Empty);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x00114190 File Offset: 0x00112590
		public bool ChangeEyesWH(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							Vector4 vector = rendEyes[i].material.GetVector(ChaShader.PupilLayout);
							vector.x = Mathf.Lerp(2f, 0.5f, base.fileFace.pupil[i].pupilW);
							vector.y = Mathf.Lerp(2f, 0.5f, base.fileFace.pupil[i].pupilH);
							rendEyes[i].material.SetVector(ChaShader.PupilLayout, vector);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x00114290 File Offset: 0x00112690
		public bool ChangeEyesColor(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							rendEyes[i].material.SetColor(ChaShader.PupilColor, base.fileFace.pupil[i].pupilColor);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x00114340 File Offset: 0x00112740
		public bool ChangeEyesEmission(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							rendEyes[i].material.SetFloat(ChaShader.PupilEmission, base.fileFace.pupil[i].pupilEmission);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x001143F0 File Offset: 0x001127F0
		public bool ChangeBlackEyesKind(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							this.ChangeTexture(rendEyes[i], ChaListDefine.CategoryNo.st_eyeblack, base.fileFace.pupil[i].blackId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.PupilBlackTex, string.Empty);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x001144AC File Offset: 0x001128AC
		public bool ChangeBlackEyesColor(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							rendEyes[i].material.SetColor(ChaShader.PupilBlackColor, base.fileFace.pupil[i].blackColor);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0011455C File Offset: 0x0011295C
		public bool ChangeBlackEyesWH(int lr)
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (lr == 2 || lr == i)
				{
					if (!(null == rendEyes[i]))
					{
						if (!(null == rendEyes[i].material))
						{
							Vector4 vector = rendEyes[i].material.GetVector(ChaShader.PupilBlackLayout);
							vector.x = Mathf.Lerp(4f, 0.4f, base.fileFace.pupil[i].blackW);
							vector.y = Mathf.Lerp(4f, 0.4f, base.fileFace.pupil[i].blackH);
							rendEyes[i].material.SetVector(ChaShader.PupilBlackLayout, vector);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x0011465C File Offset: 0x00112A5C
		public bool ChangeEyesBasePosY()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (!(null == rendEyes[i]))
				{
					if (!(null == rendEyes[i].material))
					{
						Vector4 vector = rendEyes[i].material.GetVector(ChaShader.PupilLayout);
						vector.w = Mathf.Lerp(0.5f, -0.5f, base.fileFace.pupilY);
						rendEyes[i].material.SetVector(ChaShader.PupilLayout, vector);
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x0011471C File Offset: 0x00112B1C
		public bool ChangeEyesHighlightKind()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (!(null == rendEyes[i]))
				{
					if (!(null == rendEyes[i].material))
					{
						this.ChangeTexture(rendEyes[i], ChaListDefine.CategoryNo.st_eye_hl, base.fileFace.hlId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.EyesHighlightTex, string.Empty);
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x001147BC File Offset: 0x00112BBC
		public bool ChangeEyesHighlightColor()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			for (int i = 0; i < 2; i++)
			{
				if (!(null == rendEyes[i]))
				{
					if (!(null == rendEyes[i].material))
					{
						rendEyes[i].material.SetColor(ChaShader.EyesHighlightColor, base.fileFace.hlColor);
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x00114850 File Offset: 0x00112C50
		public bool ChangeEyesHighlighLayout()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			Vector4 value;
			value.x = Mathf.Lerp(1.8f, 0.2f, base.fileFace.hlLayout.x);
			value.y = Mathf.Lerp(1.8f, 0.2f, base.fileFace.hlLayout.y);
			value.z = Mathf.Lerp(-0.3f, 0.3f, base.fileFace.hlLayout.z);
			value.w = Mathf.Lerp(-0.3f, 0.3f, base.fileFace.hlLayout.w);
			for (int i = 0; i < 2; i++)
			{
				if (!(null == rendEyes[i]))
				{
					if (!(null == rendEyes[i].material))
					{
						rendEyes[i].material.SetVector(ChaShader.EyesHighlightLayout, value);
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00114988 File Offset: 0x00112D88
		public bool ChangeEyesHighlighTilt()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			if (rendEyes == null)
			{
				return false;
			}
			float value = Mathf.Lerp(-1f, 1f, base.fileFace.hlTilt);
			for (int i = 0; i < 2; i++)
			{
				if (!(null == rendEyes[i]))
				{
					if (!(null == rendEyes[i].material))
					{
						rendEyes[i].material.SetFloat(ChaShader.EyesHighlightTilt, value);
					}
				}
			}
			return true;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00114A30 File Offset: 0x00112E30
		public bool ChangeEyesShadowRange()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer rendShadow = base.cmpFace.targetCustom.rendShadow;
			if (null == rendShadow)
			{
				return false;
			}
			if (null == rendShadow.material)
			{
				return false;
			}
			float value = Mathf.Lerp(0.1f, 0.9f, base.fileFace.whiteShadowScale);
			rendShadow.material.SetFloat(ChaShader.EyesShadowRange, value);
			return true;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x00114AB0 File Offset: 0x00112EB0
		public bool ChangeEyelashesKind()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer rendEyelashes = base.cmpFace.targetCustom.rendEyelashes;
			if (null == rendEyelashes)
			{
				return false;
			}
			if (null == rendEyelashes.material)
			{
				return false;
			}
			this.ChangeTexture(rendEyelashes, ChaListDefine.CategoryNo.st_eyelash, base.fileFace.eyelashesId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.EyelashesTex, string.Empty);
			return true;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00114B2C File Offset: 0x00112F2C
		public bool ChangeEyelashesColor()
		{
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer rendEyelashes = base.cmpFace.targetCustom.rendEyelashes;
			if (null == rendEyelashes)
			{
				return false;
			}
			if (null == rendEyelashes.material)
			{
				return false;
			}
			rendEyelashes.material.SetColor(ChaShader.EyelashesColor, base.fileFace.eyelashesColor);
			return true;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x00114B9C File Offset: 0x00112F9C
		public bool ChangeBeardKind()
		{
			this.ChangeTexture(base.customTexCtrlFace.matDraw, ChaListDefine.CategoryNo.mt_beard, base.fileFace.beardId, ChaListDefine.KeyType.MainManifest, ChaListDefine.KeyType.MainAB, ChaListDefine.KeyType.AddTex, ChaShader.BeardTex, string.Empty);
			return true;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00114BD8 File Offset: 0x00112FD8
		public bool ChangeBeardColor()
		{
			base.customTexCtrlFace.matDraw.SetColor(ChaShader.BeardColor, base.fileFace.beardColor);
			return true;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00114BFC File Offset: 0x00112FFC
		public bool SetFaceBaseMaterial()
		{
			if (null == base.customMatFace)
			{
				return false;
			}
			if (null == base.cmpFace)
			{
				return false;
			}
			Renderer rendHead = base.cmpFace.targetCustom.rendHead;
			return !(null == rendHead) && this.SetBaseMaterial(rendHead, base.customMatFace);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00114C5C File Offset: 0x0011305C
		public bool ReleaseFaceCustomTexture()
		{
			if (base.customTexCtrlFace == null)
			{
				return false;
			}
			CustomTextureCreate customTextureCreate = base.customTexCtrlFace.createCustomTex[0];
			CustomTextureCreate customTextureCreate2 = base.customTexCtrlFace.createCustomTex[1];
			customTextureCreate.SetTexture(ChaShader.MainTex, null);
			customTextureCreate.SetTexture(ChaShader.EyeshadowTex, null);
			customTextureCreate.SetTexture(ChaShader.Paint01Tex, null);
			customTextureCreate.SetTexture(ChaShader.Paint02Tex, null);
			customTextureCreate.SetTexture(ChaShader.CheekTex, null);
			customTextureCreate.SetTexture(ChaShader.LipTex, null);
			customTextureCreate.SetTexture(ChaShader.MoleTex, null);
			customTextureCreate2.SetTexture(ChaShader.MainTex, null);
			customTextureCreate2.SetTexture(ChaShader.EyeshadowTex, null);
			customTextureCreate2.SetTexture(ChaShader.Paint01Tex, null);
			customTextureCreate2.SetTexture(ChaShader.Paint02Tex, null);
			customTextureCreate2.SetTexture(ChaShader.CheekTex, null);
			customTextureCreate2.SetTexture(ChaShader.LipTex, null);
			UnityEngine.Resources.UnloadUnusedAssets();
			return true;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00114D38 File Offset: 0x00113138
		public void ChangeCustomFaceWithoutCustomTexture()
		{
			this.ChangeFaceGlossPower();
			this.ChangeFaceMetallicPower();
			this.ChangeFaceDetailKind();
			this.ChangeFaceDetailPower();
			this.ChangeEyebrowKind();
			this.ChangeEyebrowColor();
			this.ChangeEyebrowLayout();
			this.ChangeEyebrowTilt();
			this.ChangeWhiteEyesColor(2);
			this.ChangeEyesKind(2);
			this.ChangeEyesWH(2);
			this.ChangeEyesColor(2);
			this.ChangeEyesEmission(2);
			this.ChangeBlackEyesKind(2);
			this.ChangeBlackEyesColor(2);
			this.ChangeBlackEyesWH(2);
			this.ChangeEyesBasePosY();
			this.ChangeEyesHighlightKind();
			this.ChangeEyesHighlightColor();
			this.ChangeEyesHighlighLayout();
			this.ChangeEyesHighlighTilt();
			this.ChangeEyesShadowRange();
			this.ChangeEyelashesKind();
			this.ChangeEyelashesColor();
			if (base.sex == 0)
			{
				this.ChangeBeardKind();
				this.ChangeBeardColor();
			}
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00114E10 File Offset: 0x00113210
		public bool InitShapeFace(Transform trfBone, string manifest, string assetBundleAnmShapeFace, string assetAnmShapeFace)
		{
			if (this.sibFace == null)
			{
				return false;
			}
			if (null == trfBone)
			{
				return false;
			}
			string cateInfoName = ChaABDefine.ShapeHeadListAsset((int)base.sex);
			this.sibFace.InitShapeInfo(manifest, assetBundleAnmShapeFace, "list/customshape.unity3d", assetAnmShapeFace, cateInfoName, trfBone);
			for (int i = 0; i < this.ShapeFaceNum; i++)
			{
				this.sibFace.ChangeValue(i, base.fileFace.shapeValueFace[i]);
			}
			base.updateShapeFace = true;
			return true;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00114E93 File Offset: 0x00113293
		public void ReleaseShapeFace()
		{
			if (this.sibFace == null)
			{
				return;
			}
			this.sibFace.ReleaseShapeInfo();
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x00114EAC File Offset: 0x001132AC
		public bool SetShapeFaceValue(int index, float value)
		{
			if (index >= this.ShapeFaceNum)
			{
				return false;
			}
			base.fileFace.shapeValueFace[index] = value;
			if (this.sibFace != null && this.sibFace.InitEnd)
			{
				this.sibFace.ChangeValue(index, value);
			}
			base.updateShapeFace = true;
			return true;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x00114F08 File Offset: 0x00113308
		public bool UpdateShapeFaceValueFromCustomInfo()
		{
			if (this.sibFace == null || !this.sibFace.InitEnd)
			{
				return false;
			}
			for (int i = 0; i < base.fileFace.shapeValueFace.Length; i++)
			{
				this.sibFace.ChangeValue(i, base.fileFace.shapeValueFace[i]);
			}
			base.updateShapeFace = true;
			return true;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x00114F72 File Offset: 0x00113372
		public float GetShapeFaceValue(int index)
		{
			if (index >= this.ShapeFaceNum)
			{
				return 0f;
			}
			return base.fileFace.shapeValueFace[index];
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x00114F94 File Offset: 0x00113394
		public void UpdateShapeFace()
		{
			if (this.sibFace == null || !this.sibFace.InitEnd)
			{
				return;
			}
			if (base.fileStatus.disableMouthShapeMask)
			{
				for (int i = 0; i < ChaFileDefine.cf_MouthShapeMaskID.Length; i++)
				{
					this.sibFace.ChangeValue(ChaFileDefine.cf_MouthShapeMaskID[i], ChaFileDefine.cf_MouthShapeDefault[i]);
				}
			}
			else
			{
				foreach (int num in ChaFileDefine.cf_MouthShapeMaskID)
				{
					this.sibFace.ChangeValue(num, base.fileFace.shapeValueFace[num]);
				}
			}
			this.sibFace.Update();
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x00115047 File Offset: 0x00113447
		public void DisableShapeMouth(bool disable)
		{
			base.updateShapeFace = true;
			base.fileStatus.disableMouthShapeMask = disable;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0011505C File Offset: 0x0011345C
		protected void InitializeControlCustomHairAll()
		{
			this.InitializeControlCustomHairObject();
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00115064 File Offset: 0x00113464
		protected void InitializeControlCustomHairObject()
		{
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00115066 File Offset: 0x00113466
		protected void ReleaseControlCustomHairAll()
		{
			this.ReleaseControlCustomHairObject(true);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0011506F File Offset: 0x0011346F
		protected void ReleaseControlCustomHairObject(bool init = true)
		{
			if (init)
			{
				this.InitializeControlCustomHairObject();
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x00115080 File Offset: 0x00113480
		public void ChangeSettingHairShader()
		{
			ChaFileHair hair = base.chaFile.custom.hair;
			Shader shader = (hair.shaderType != 0) ? Singleton<Character>.Instance.shaderCutout : Singleton<Character>.Instance.shaderDithering;
			for (int i = 0; i < base.cmpHair.Length; i++)
			{
				if (!(null == base.cmpHair[i]))
				{
					CmpHair customHairComponent = base.GetCustomHairComponent(i);
					if (!(null == customHairComponent) && customHairComponent.rendHair != null && customHairComponent.rendHair.Length != 0)
					{
						if (base.infoHair != null && base.infoHair[i] != null)
						{
							string info = base.infoHair[i].GetInfo(ChaListDefine.KeyType.TexManifest);
							string info2 = base.infoHair[i].GetInfo(ChaListDefine.KeyType.TexAB);
							string info3 = base.infoHair[i].GetInfo((hair.shaderType != 0) ? ChaListDefine.KeyType.TexC : ChaListDefine.KeyType.TexD);
							int value = (base.infoHair[i].GetInfoInt(ChaListDefine.KeyType.RingOff) != 1) ? 0 : 1;
							Texture2D value2 = CommonLib.LoadAsset<Texture2D>(info2, info3, false, info);
							Singleton<Character>.Instance.AddLoadAssetBundle(info2, info);
							for (int j = 0; j < customHairComponent.rendHair.Length; j++)
							{
								for (int k = 0; k < customHairComponent.rendHair[j].materials.Length; k++)
								{
									int renderQueue = customHairComponent.rendHair[j].materials[k].renderQueue;
									customHairComponent.rendHair[j].materials[k].shader = shader;
									customHairComponent.rendHair[j].materials[k].SetTexture(ChaShader.MainTex, value2);
									if (customHairComponent.rendHair[j].materials[k].HasProperty(ChaShader.HairRingoff))
									{
										customHairComponent.rendHair[j].materials[k].SetInt(ChaShader.HairRingoff, value);
									}
									customHairComponent.rendHair[j].materials[k].renderQueue = renderQueue;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x001152B0 File Offset: 0x001136B0
		public void ChangeSettingHairColor(int parts, bool _main, bool _top, bool _under)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				if (_main)
				{
					if (1f > hair.parts[parts].baseColor.a)
					{
						hair.parts[parts].baseColor = new Color(hair.parts[parts].baseColor.r, hair.parts[parts].baseColor.g, hair.parts[parts].baseColor.b, 1f);
					}
					for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
					{
						customHairComponent.rendHair[i].materials[j].SetColor(ChaShader.HairMainColor, hair.parts[parts].baseColor);
					}
				}
				if (customHairComponent.useTopColor && _top)
				{
					if (1f > hair.parts[parts].topColor.a)
					{
						hair.parts[parts].topColor = new Color(hair.parts[parts].topColor.r, hair.parts[parts].topColor.g, hair.parts[parts].topColor.b, 1f);
					}
					for (int k = 0; k < customHairComponent.rendHair[i].materials.Length; k++)
					{
						customHairComponent.rendHair[i].materials[k].SetColor(ChaShader.HairTopColor, hair.parts[parts].topColor);
					}
				}
				if (customHairComponent.useUnderColor && _under)
				{
					if (1f > hair.parts[parts].underColor.a)
					{
						hair.parts[parts].underColor = new Color(hair.parts[parts].underColor.r, hair.parts[parts].underColor.g, hair.parts[parts].underColor.b, 1f);
					}
					for (int l = 0; l < customHairComponent.rendHair[i].materials.Length; l++)
					{
						customHairComponent.rendHair[i].materials[l].SetColor(ChaShader.HairUnderColor, hair.parts[parts].underColor);
					}
				}
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x00115588 File Offset: 0x00113988
		public void CreateHairColor(int parts, Color color)
		{
			ChaFileHair hair = base.chaFile.custom.hair;
			hair.parts[parts].baseColor = new Color(color.r, color.g, color.b, 1f);
			Color topColor;
			Color underColor;
			Color specular;
			this.CreateHairColor(color, out topColor, out underColor, out specular);
			hair.parts[parts].topColor = topColor;
			hair.parts[parts].underColor = underColor;
			hair.parts[parts].specular = specular;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0011560C File Offset: 0x00113A0C
		public void CreateHairColor(Color baseColor, out Color topColor, out Color underColor, out Color specular)
		{
			float h;
			float num;
			float num2;
			Color.RGBToHSV(baseColor, out h, out num, out num2);
			topColor = Color.HSVToRGB(h, num, Mathf.Max(num2 - 0.15f, 0f));
			underColor = Color.HSVToRGB(h, Mathf.Max(num - 0.1f, 0f), Mathf.Min(num2 + 0.44f, 1f));
			specular = Color.HSVToRGB(h, num, Mathf.Min(num2 + 0.17f, 1f));
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x00115690 File Offset: 0x00113A90
		public void ChangeSettingHairSpecular(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				if (1f > hair.parts[parts].specular.a)
				{
					hair.parts[parts].specular = new Color(hair.parts[parts].specular.r, hair.parts[parts].specular.g, hair.parts[parts].specular.b, 1f);
				}
				for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
				{
					customHairComponent.rendHair[i].materials[j].SetColor(ChaShader.Specular, hair.parts[parts].specular);
				}
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x001157B8 File Offset: 0x00113BB8
		public void ChangeSettingHairMetallic(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
				{
					customHairComponent.rendHair[i].materials[j].SetFloat(ChaShader.Metallic, hair.parts[parts].metallic);
				}
			}
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00115864 File Offset: 0x00113C64
		public void ChangeSettingHairSmoothness(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
				{
					customHairComponent.rendHair[i].materials[j].SetFloat(ChaShader.Smoothness, hair.parts[parts].smoothness);
				}
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00115910 File Offset: 0x00113D10
		public int GetHairAcsColorNum(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.acsDefColor == null || customHairComponent.acsDefColor.Length == 0)
			{
				return 0;
			}
			return customHairComponent.acsDefColor.Length;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00115954 File Offset: 0x00113D54
		public void SetHairAcsDefaultColorParameterOnly(int parts)
		{
			ChaFileHair hair = base.chaFile.custom.hair;
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent)
			{
				return;
			}
			int num = customHairComponent.acsDefColor.Length;
			for (int i = 0; i < num; i++)
			{
				if (customHairComponent.acsDefColor != null)
				{
					hair.parts[parts].acsColorInfo[i].color = customHairComponent.acsDefColor[i];
				}
			}
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x001159D4 File Offset: 0x00113DD4
		public void ChangeSettingHairAcsColor(int parts)
		{
			if (base.cmpHair == null || null == base.cmpHair[parts])
			{
				return;
			}
			int hairAcsColorNum = this.GetHairAcsColorNum(parts);
			if (hairAcsColorNum == 0)
			{
				return;
			}
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent)
			{
				return;
			}
			int[] array = new int[]
			{
				ChaShader.Color,
				ChaShader.Color2,
				ChaShader.Color3
			};
			bool[] array2 = new bool[]
			{
				base.cmpHair[parts].useAcsColor01,
				base.cmpHair[parts].useAcsColor02,
				base.cmpHair[parts].useAcsColor03
			};
			for (int i = 0; i < customHairComponent.rendAccessory.Length; i++)
			{
				for (int j = 0; j < hairAcsColorNum; j++)
				{
					if (array2[j])
					{
						if (1f > base.fileHair.parts[parts].acsColorInfo[j].color.a)
						{
							base.fileHair.parts[parts].acsColorInfo[j].color = new Color(base.fileHair.parts[parts].acsColorInfo[j].color.r, base.fileHair.parts[parts].acsColorInfo[j].color.g, base.fileHair.parts[parts].acsColorInfo[j].color.b, 1f);
						}
						foreach (Material material in customHairComponent.rendAccessory[i].materials)
						{
							material.SetColor(array[j], base.fileHair.parts[parts].acsColorInfo[j].color);
						}
					}
				}
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00115BCC File Offset: 0x00113FCC
		public void ChangeSettingHairCorrectPos(int parts, int idx)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return;
			}
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length <= idx)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return;
			}
			base.cmpHair[parts].boneInfo[idx].moveRate = bundleInfo.moveRate;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00115C74 File Offset: 0x00114074
		public void ChangeSettingHairCorrectPosAll(int parts)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length != hair.parts[parts].dictBundle.Count)
			{
				return;
			}
			for (int i = 0; i < base.cmpHair[parts].boneInfo.Length; i++)
			{
				ChaFileHair.PartsInfo.BundleInfo bundleInfo;
				if (hair.parts[parts].dictBundle.TryGetValue(i, out bundleInfo))
				{
					base.cmpHair[parts].boneInfo[i].moveRate = bundleInfo.moveRate;
				}
			}
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00115D54 File Offset: 0x00114154
		public bool SetHairCorrectPosValue(int parts, int idx, Vector3 val, int _flag)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return false;
			}
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length <= idx)
			{
				return false;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return false;
			}
			val = base.cmpHair[parts].boneInfo[idx].trfCorrect.parent.InverseTransformPoint(val.x, val.y, val.z);
			Vector3 moveRate = bundleInfo.moveRate;
			if ((_flag & 1) != 0)
			{
				moveRate.x = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].posMin.x, base.cmpHair[parts].boneInfo[idx].posMax.x, val.x);
			}
			if ((_flag & 2) != 0)
			{
				moveRate.y = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].posMin.y, base.cmpHair[parts].boneInfo[idx].posMax.y, val.y);
			}
			if ((_flag & 4) != 0)
			{
				moveRate.z = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].posMin.z, base.cmpHair[parts].boneInfo[idx].posMax.z, val.z);
			}
			bundleInfo.moveRate = moveRate;
			return true;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00115F14 File Offset: 0x00114314
		public bool GetDefaultHairCorrectPosRate(int parts, int idx, out Vector3 v)
		{
			v = Vector3.zero;
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return false;
			}
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length <= idx)
			{
				return false;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return false;
			}
			v.x = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].posMin.x, base.cmpHair[parts].boneInfo[idx].posMax.x, base.cmpHair[parts].boneInfo[idx].basePos.x);
			v.y = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].posMin.y, base.cmpHair[parts].boneInfo[idx].posMax.y, base.cmpHair[parts].boneInfo[idx].basePos.y);
			v.z = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].posMin.z, base.cmpHair[parts].boneInfo[idx].posMax.z, base.cmpHair[parts].boneInfo[idx].basePos.z);
			return true;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x001160B4 File Offset: 0x001144B4
		public void SetDefaultHairCorrectPosRate(int parts, int idx)
		{
			Vector3 moveRate;
			if (!this.GetDefaultHairCorrectPosRate(parts, idx, out moveRate))
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return;
			}
			bundleInfo.moveRate = moveRate;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00116104 File Offset: 0x00114504
		public void SetDefaultHairCorrectPosRateAll(int parts)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length != hair.parts[parts].dictBundle.Count)
			{
				return;
			}
			for (int i = 0; i < base.cmpHair[parts].boneInfo.Length; i++)
			{
				ChaFileHair.PartsInfo.BundleInfo bundleInfo;
				if (hair.parts[parts].dictBundle.TryGetValue(i, out bundleInfo))
				{
					Vector3 moveRate;
					if (this.GetDefaultHairCorrectPosRate(parts, i, out moveRate))
					{
						bundleInfo.moveRate = moveRate;
					}
				}
			}
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x001161E0 File Offset: 0x001145E0
		public void ChangeSettingHairCorrectRot(int parts, int idx)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return;
			}
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length <= idx)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return;
			}
			base.cmpHair[parts].boneInfo[idx].rotRate = bundleInfo.rotRate;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00116288 File Offset: 0x00114688
		public void ChangeSettingHairCorrectRotAll(int parts)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length != hair.parts[parts].dictBundle.Count)
			{
				return;
			}
			for (int i = 0; i < base.cmpHair[parts].boneInfo.Length; i++)
			{
				ChaFileHair.PartsInfo.BundleInfo bundleInfo;
				if (hair.parts[parts].dictBundle.TryGetValue(i, out bundleInfo))
				{
					base.cmpHair[parts].boneInfo[i].rotRate = bundleInfo.rotRate;
				}
			}
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00116368 File Offset: 0x00114768
		public bool SetHairCorrectRotValue(int parts, int idx, Vector3 val, int _flag)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return false;
			}
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length <= idx)
			{
				return false;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return false;
			}
			val.x = ((val.x <= 180f) ? val.x : (val.x - 360f));
			val.y = ((val.y <= 180f) ? val.y : (val.y - 360f));
			val.z = ((val.z <= 180f) ? val.z : (val.z - 360f));
			Vector3 rotRate = bundleInfo.rotRate;
			if ((_flag & 1) != 0)
			{
				rotRate.x = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].rotMin.x, base.cmpHair[parts].boneInfo[idx].rotMax.x, val.x);
			}
			if ((_flag & 2) != 0)
			{
				rotRate.y = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].rotMin.y, base.cmpHair[parts].boneInfo[idx].rotMax.y, val.y);
			}
			if ((_flag & 4) != 0)
			{
				rotRate.z = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].rotMin.z, base.cmpHair[parts].boneInfo[idx].rotMax.z, val.z);
			}
			bundleInfo.rotRate = rotRate;
			return true;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00116588 File Offset: 0x00114988
		public bool GetDefaultHairCorrectRotRate(int parts, int idx, out Vector3 v)
		{
			v = Vector3.zero;
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return false;
			}
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length <= idx)
			{
				return false;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return false;
			}
			v.x = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].rotMin.x, base.cmpHair[parts].boneInfo[idx].rotMax.x, base.cmpHair[parts].boneInfo[idx].baseRot.x);
			v.y = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].rotMin.y, base.cmpHair[parts].boneInfo[idx].rotMax.y, base.cmpHair[parts].boneInfo[idx].baseRot.y);
			v.z = Mathf.InverseLerp(base.cmpHair[parts].boneInfo[idx].rotMin.z, base.cmpHair[parts].boneInfo[idx].rotMax.z, base.cmpHair[parts].boneInfo[idx].baseRot.z);
			return true;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00116728 File Offset: 0x00114B28
		public void SetDefaultHairCorrectRotRate(int parts, int idx)
		{
			Vector3 rotRate;
			if (!this.GetDefaultHairCorrectRotRate(parts, idx, out rotRate))
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!hair.parts[parts].dictBundle.TryGetValue(idx, out bundleInfo))
			{
				return;
			}
			bundleInfo.rotRate = rotRate;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x00116778 File Offset: 0x00114B78
		public void SetDefaultHairCorrectRotRateAll(int parts)
		{
			if (base.cmpHair == null || base.cmpHair.Length <= parts || null == base.cmpHair[parts])
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			if (base.cmpHair[parts].boneInfo == null || base.cmpHair[parts].boneInfo.Length != hair.parts[parts].dictBundle.Count)
			{
				return;
			}
			for (int i = 0; i < base.cmpHair[parts].boneInfo.Length; i++)
			{
				ChaFileHair.PartsInfo.BundleInfo bundleInfo;
				if (hair.parts[parts].dictBundle.TryGetValue(i, out bundleInfo))
				{
					Vector3 rotRate;
					if (this.GetDefaultHairCorrectRotRate(parts, i, out rotRate))
					{
						bundleInfo.rotRate = rotRate;
					}
				}
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00116854 File Offset: 0x00114C54
		public void ResetDynamicBoneHair(bool includeInactive = false)
		{
			if (base.cmpHair == null)
			{
				return;
			}
			for (int i = 0; i < base.cmpHair.Length; i++)
			{
				if (!(null == base.cmpHair[i]))
				{
					base.cmpHair[i].ResetDynamicBonesHair(includeInactive);
				}
			}
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x001168AC File Offset: 0x00114CAC
		public void ChangeSettingHairMeshType(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			if (!customHairComponent.useMesh)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			Texture2D value = null;
			ListInfoBase listInfo = base.lstCtrl.GetListInfo(ChaListDefine.CategoryNo.st_hairmeshptn, hair.parts[parts].meshType);
			if (listInfo != null)
			{
				string info = listInfo.GetInfo(ChaListDefine.KeyType.MainAB);
				string info2 = listInfo.GetInfo(ChaListDefine.KeyType.MainTex);
				if ("0" != info && "0" != info2)
				{
					value = CommonLib.LoadAsset<Texture2D>(info, info2, false, string.Empty);
					Singleton<Character>.Instance.AddLoadAssetBundle(info, "abdata");
				}
			}
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
				{
					customHairComponent.rendHair[i].materials[j].SetTexture(ChaShader.HairMeshColorMask, value);
				}
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x001169E0 File Offset: 0x00114DE0
		public void ChangeSettingHairMeshColor(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			if (!customHairComponent.useMesh)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			Color value = new Color(hair.parts[parts].meshColor.r, hair.parts[parts].meshColor.g, hair.parts[parts].meshColor.b, 1f);
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
				{
					customHairComponent.rendHair[i].materials[j].SetColor(ChaShader.HairMeshColor, value);
				}
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x00116AE4 File Offset: 0x00114EE4
		public void ChangeSettingHairMeshLayout(int parts)
		{
			CmpHair customHairComponent = base.GetCustomHairComponent(parts);
			if (null == customHairComponent || customHairComponent.rendHair == null || customHairComponent.rendHair.Length == 0)
			{
				return;
			}
			if (!customHairComponent.useMesh)
			{
				return;
			}
			ChaFileHair hair = base.chaFile.custom.hair;
			Vector2 value = new Vector2(Mathf.Lerp(0.5f, 1f, hair.parts[parts].meshLayout.x), Mathf.Lerp(0.5f, 1f, hair.parts[parts].meshLayout.y));
			Vector2 value2 = new Vector2(Mathf.Lerp(0.5f, 1.5f, hair.parts[parts].meshLayout.z), Mathf.Lerp(0.5f, 1.5f, hair.parts[parts].meshLayout.w));
			for (int i = 0; i < customHairComponent.rendHair.Length; i++)
			{
				for (int j = 0; j < customHairComponent.rendHair[i].materials.Length; j++)
				{
					customHairComponent.rendHair[i].materials[j].SetTextureScale(ChaShader.HairMeshColorMask, value);
					customHairComponent.rendHair[i].materials[j].SetTextureOffset(ChaShader.HairMeshColorMask, value2);
				}
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x00116C57 File Offset: 0x00115057
		// (set) Token: 0x06002FD5 RID: 12245 RVA: 0x00116C5F File Offset: 0x0011505F
		public AudioSource asVoice { get; private set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x00116C68 File Offset: 0x00115068
		// (set) Token: 0x06002FD7 RID: 12247 RVA: 0x00116C70 File Offset: 0x00115070
		private AudioAssist fbsaaVoice { get; set; }

		// Token: 0x06002FD8 RID: 12248 RVA: 0x00116C79 File Offset: 0x00115079
		protected void InitializeControlFaceAll()
		{
			this.fbsaaVoice = new AudioAssist();
			this.InitializeControlFaceObject();
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00116C8C File Offset: 0x0011508C
		protected void InitializeControlFaceObject()
		{
			this.asVoice = null;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x00116C95 File Offset: 0x00115095
		protected void ReleaseControlFaceAll()
		{
			this.ReleaseControlFaceObject(false);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x00116C9E File Offset: 0x0011509E
		protected void ReleaseControlFaceObject(bool init = true)
		{
			if (init)
			{
				this.InitializeControlFaceObject();
			}
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x00116CAC File Offset: 0x001150AC
		public void ChangeLookEyesTarget(int targetType, Transform trfTarg = null, float rate = 0.5f, float rotDeg = 0f, float range = 1f, float dis = 2f)
		{
			if (null == base.eyeLookCtrl)
			{
				return;
			}
			if (targetType == -1)
			{
				targetType = base.fileStatus.eyesTargetType;
			}
			else
			{
				base.fileStatus.eyesTargetType = targetType;
			}
			base.eyeLookCtrl.target = null;
			if (null != trfTarg)
			{
				base.eyeLookCtrl.target = trfTarg;
			}
			else
			{
				if (targetType == 0)
				{
					if (null != Camera.main)
					{
						base.eyeLookCtrl.target = Camera.main.transform;
					}
				}
				else if (base.objEyesLookTarget && base.objEyesLookTargetP)
				{
					switch (targetType)
					{
					case 1:
						rotDeg = 0f;
						range = 1f;
						break;
					case 2:
						rotDeg = 45f;
						range = 1f;
						break;
					case 3:
						rotDeg = 90f;
						range = 1f;
						break;
					case 4:
						rotDeg = 135f;
						range = 1f;
						break;
					case 5:
						rotDeg = 180f;
						range = 1f;
						break;
					case 6:
						rotDeg = 225f;
						range = 1f;
						break;
					case 7:
						rotDeg = 270f;
						range = 1f;
						break;
					case 8:
						rotDeg = 315f;
						range = 1f;
						break;
					case 9:
						rotDeg = 0f;
						range = 1f;
						break;
					}
					base.objEyesLookTargetP.transform.SetLocalPosition(0f, 0.7f, 0f);
					base.eyeLookCtrl.target = base.objEyesLookTarget.transform;
					float y = Mathf.Lerp(0f, range, (targetType != 9) ? rate : 0f);
					base.eyeLookCtrl.target.SetLocalPosition(0f, y, dis);
					base.objEyesLookTargetP.transform.localEulerAngles = new Vector3(0f, 0f, 360f - rotDeg);
				}
				base.fileStatus.eyesTargetAngle = rotDeg;
				base.fileStatus.eyesTargetRange = range;
				base.fileStatus.eyesTargetRate = rate;
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x00116F04 File Offset: 0x00115304
		public void ChangeLookEyesPtn(int ptn)
		{
			if (null == base.eyeLookCtrl)
			{
				return;
			}
			EyeLookController eyeLookCtrl = base.eyeLookCtrl;
			base.fileStatus.eyesLookPtn = ptn;
			eyeLookCtrl.ptnNo = ptn;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x00116F3D File Offset: 0x0011533D
		public int GetLookEyesPtn()
		{
			return base.fileStatus.eyesLookPtn;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x00116F4A File Offset: 0x0011534A
		public float GetLookEyesRate()
		{
			return base.fileStatus.eyesTargetRate;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x00116F58 File Offset: 0x00115358
		public void ChangeLookNeckTarget(int targetType, Transform trfTarg = null, float rate = 0.5f, float rotDeg = 0f, float range = 1f, float dis = 0.8f)
		{
			if (null == base.neckLookCtrl)
			{
				return;
			}
			if (targetType == -1)
			{
				targetType = base.fileStatus.neckTargetType;
			}
			else
			{
				base.fileStatus.neckTargetType = targetType;
			}
			base.neckLookCtrl.target = null;
			if (null != trfTarg)
			{
				base.neckLookCtrl.target = trfTarg;
			}
			else
			{
				if (targetType == 0)
				{
					if (null != Camera.main)
					{
						base.neckLookCtrl.target = Camera.main.transform;
					}
				}
				else if (null != base.objNeckLookTarget && null != base.objNeckLookTargetP)
				{
					switch (targetType)
					{
					case 1:
						rotDeg = 0f;
						range = 1f;
						break;
					case 2:
						rotDeg = 45f;
						range = 1f;
						break;
					case 3:
						rotDeg = 90f;
						range = 1f;
						break;
					case 4:
						rotDeg = 135f;
						range = 1f;
						break;
					case 5:
						rotDeg = 180f;
						range = 1f;
						break;
					case 6:
						rotDeg = 225f;
						range = 1f;
						break;
					case 7:
						rotDeg = 270f;
						range = 1f;
						break;
					case 8:
						rotDeg = 315f;
						range = 1f;
						break;
					}
					base.objNeckLookTargetP.transform.SetLocalPosition(0f, 2.7f, 0f);
					base.neckLookCtrl.target = base.objNeckLookTarget.transform;
					float y = Mathf.Lerp(0f, range, rate);
					base.neckLookCtrl.target.SetLocalPosition(0f, y, dis);
					base.objNeckLookTargetP.transform.localEulerAngles = new Vector3(0f, 0f, 360f - rotDeg);
				}
				base.fileStatus.neckTargetAngle = rotDeg;
				base.fileStatus.neckTargetRange = range;
				base.fileStatus.neckTargetRate = rate;
			}
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0011718C File Offset: 0x0011558C
		public void ChangeLookNeckPtn(int ptn, float rate = 1f)
		{
			if (null == base.neckLookCtrl)
			{
				return;
			}
			NeckLookControllerVer2 neckLookCtrl = base.neckLookCtrl;
			base.fileStatus.neckLookPtn = ptn;
			neckLookCtrl.ptnNo = ptn;
			base.neckLookCtrl.rate = rate;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x001171D1 File Offset: 0x001155D1
		public int GetLookNeckPtn()
		{
			return base.fileStatus.neckLookPtn;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x001171DE File Offset: 0x001155DE
		public float GetLookNeckRate()
		{
			return base.fileStatus.neckTargetRate;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x001171EC File Offset: 0x001155EC
		public void HideEyeHighlight(bool hide)
		{
			if (null == base.cmpFace)
			{
				return;
			}
			Renderer[] rendEyes = base.cmpFace.targetCustom.rendEyes;
			base.fileStatus.hideEyesHighlight = hide;
			float value = (!hide) ? 1f : 0f;
			if (rendEyes == null)
			{
				return;
			}
			foreach (Renderer renderer in rendEyes)
			{
				if (!(null == renderer))
				{
					Material material = renderer.material;
					if (null != material)
					{
						material.SetFloat(ChaShader.EyesHighlightOnOff, value);
					}
				}
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x0011729C File Offset: 0x0011569C
		public float tearsRate
		{
			get
			{
				return base.fileStatus.tearsRate;
			}
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x001172AC File Offset: 0x001156AC
		public void ChangeTearsRate(float value)
		{
			base.fileStatus.tearsRate = Mathf.Clamp(value, 0f, 1f);
			if (null != base.cmpFace && null != base.cmpFace.targetEtc.rendTears)
			{
				base.cmpFace.targetEtc.rendTears.material.SetFloat(ChaShader.tearsRate, base.fileStatus.tearsRate);
			}
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0011732A File Offset: 0x0011572A
		public int GetEyesPtnNum()
		{
			if (base.eyesCtrl == null)
			{
				return 0;
			}
			return base.eyesCtrl.GetMaxPtn();
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x00117344 File Offset: 0x00115744
		public void ChangeEyesPtn(int ptn, bool blend = true)
		{
			if (base.eyesCtrl == null)
			{
				return;
			}
			base.fileStatus.eyesPtn = ptn;
			base.eyesCtrl.ChangePtn(ptn, blend);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0011736B File Offset: 0x0011576B
		public int GetEyesPtn()
		{
			return base.fileStatus.eyesPtn;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x00117378 File Offset: 0x00115778
		public void ChangeEyesOpenMax(float maxValue)
		{
			if (base.eyesCtrl == null)
			{
				return;
			}
			float num = Mathf.Clamp(maxValue, 0f, 1f);
			base.fileStatus.eyesOpenMax = num;
			base.eyesCtrl.OpenMax = num;
			if (!base.fileStatus.eyesBlink)
			{
				base.eyesCtrl.SetOpenRateForce(num);
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x001173D6 File Offset: 0x001157D6
		public float GetEyesOpenMax()
		{
			return base.fileStatus.eyesOpenMax;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x001173E3 File Offset: 0x001157E3
		public void ChangeEyebrowPtn(int ptn, bool blend = true)
		{
			if (base.eyebrowCtrl == null)
			{
				return;
			}
			base.fileStatus.eyebrowPtn = ptn;
			base.eyebrowCtrl.ChangePtn(ptn, blend);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x0011740A File Offset: 0x0011580A
		public int GetEyebrowPtn()
		{
			return base.fileStatus.eyebrowPtn;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00117418 File Offset: 0x00115818
		public void ChangeEyebrowOpenMax(float maxValue)
		{
			if (base.eyebrowCtrl == null)
			{
				return;
			}
			float num = Mathf.Clamp(maxValue, 0f, 1f);
			base.fileStatus.eyebrowOpenMax = num;
			base.eyebrowCtrl.OpenMax = num;
			if (!base.fileStatus.eyesBlink)
			{
				base.eyebrowCtrl.SetOpenRateForce(1f);
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0011747A File Offset: 0x0011587A
		public float GetEyebrowOpenMax()
		{
			return base.fileStatus.eyebrowOpenMax;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00117488 File Offset: 0x00115888
		public void ChangeEyesBlinkFlag(bool blink)
		{
			if (null == base.fbsCtrl)
			{
				return;
			}
			if (base.fbsCtrl.BlinkCtrl == null)
			{
				return;
			}
			base.fileStatus.eyesBlink = blink;
			base.fbsCtrl.BlinkCtrl.SetFixedFlags((!blink) ? 1 : 0);
			if (!blink)
			{
				base.eyesCtrl.SetOpenRateForce(1f);
				base.eyebrowCtrl.SetOpenRateForce(1f);
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x00117508 File Offset: 0x00115908
		public bool GetEyesBlinkFlag()
		{
			return base.fileStatus.eyesBlink;
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00117518 File Offset: 0x00115918
		public void ChangeMouthPtn(int ptn, bool blend = true)
		{
			if (base.mouthCtrl == null)
			{
				return;
			}
			base.fileStatus.mouthPtn = ptn;
			base.mouthCtrl.ChangePtn(ptn, blend);
			this.ChangeTongueState((ptn != 10 && ptn != 13) ? 0 : 1);
			bool useFlags = true;
			if (9 <= ptn && ptn <= 16)
			{
				useFlags = false;
			}
			base.mouthCtrl.UseAdjustWidthScale(useFlags);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00117588 File Offset: 0x00115988
		public int GetMouthPtn()
		{
			return base.fileStatus.mouthPtn;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00117598 File Offset: 0x00115998
		public void ChangeMouthOpenMax(float maxValue)
		{
			if (base.mouthCtrl == null)
			{
				return;
			}
			float num = Mathf.Clamp(maxValue, 0f, 1f);
			base.fileStatus.mouthOpenMax = num;
			base.mouthCtrl.OpenMax = num;
			if (base.fileStatus.mouthFixed)
			{
				base.mouthCtrl.FixedRate = num;
			}
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x001175F6 File Offset: 0x001159F6
		public float GetMouthOpenMax()
		{
			return base.fileStatus.mouthOpenMax;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00117604 File Offset: 0x00115A04
		public void ChangeMouthOpenMin(float minValue)
		{
			if (base.mouthCtrl == null)
			{
				return;
			}
			float num = Mathf.Clamp(minValue, 0f, 1f);
			base.fileStatus.mouthOpenMin = num;
			base.mouthCtrl.OpenMin = num;
			if (base.fileStatus.mouthFixed)
			{
				base.mouthCtrl.FixedRate = num;
			}
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00117662 File Offset: 0x00115A62
		public float GetMouthOpenMin()
		{
			return base.fileStatus.mouthOpenMin;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00117670 File Offset: 0x00115A70
		public void ChangeMouthFixed(bool fix)
		{
			if (base.mouthCtrl == null)
			{
				return;
			}
			base.fileStatus.mouthFixed = fix;
			if (fix)
			{
				base.mouthCtrl.FixedRate = base.fileStatus.mouthOpenMax;
			}
			else
			{
				base.mouthCtrl.FixedRate = -1f;
			}
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x001176C6 File Offset: 0x00115AC6
		public bool GetMouthFixed()
		{
			return base.fileStatus.mouthFixed;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x001176D3 File Offset: 0x00115AD3
		public void ChangeTongueState(byte state)
		{
			base.fileStatus.tongueState = state;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x001176E1 File Offset: 0x00115AE1
		public byte GetTongueState()
		{
			return base.fileStatus.tongueState;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x001176EE File Offset: 0x00115AEE
		public bool SetVoiceTransform(Transform trfVoice)
		{
			if (null == trfVoice)
			{
				this.asVoice = null;
				return false;
			}
			this.asVoice = trfVoice.GetComponent<AudioSource>();
			return !(null == this.asVoice);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x00117728 File Offset: 0x00115B28
		private void UpdateBlendShapeVoice()
		{
			float voiceVaule = 0f;
			float correct = 3f;
			if (null != this.asVoice && this.asVoice.isPlaying)
			{
				voiceVaule = this.fbsaaVoice.GetAudioWaveValue(this.asVoice, correct);
				if (null != base.cmpBoneBody && null != base.cmpBoneBody.targetEtc.trfHeadParent && null != this.asVoice.transform)
				{
					this.asVoice.transform.position = base.cmpBoneBody.targetEtc.trfHeadParent.position;
				}
			}
			if (null != base.fbsCtrl)
			{
				base.fbsCtrl.SetVoiceVaule(voiceVaule);
			}
			if (base.fileStatus.mouthAdjustWidth && null != base.objHeadBone)
			{
				Transform trfMouthAdjustWidth = base.cmpBoneHead.targetEtc.trfMouthAdjustWidth;
				if (null != trfMouthAdjustWidth)
				{
					float x = 1f;
					if (base.mouthCtrl != null)
					{
						x = base.mouthCtrl.GetAdjustWidthScale();
					}
					trfMouthAdjustWidth.SetLocalScaleX(x);
				}
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x0011785B File Offset: 0x00115C5B
		public float hohoAkaRate
		{
			get
			{
				return base.fileStatus.hohoAkaRate;
			}
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x00117868 File Offset: 0x00115C68
		public void ChangeHohoAkaRate(float value)
		{
			base.fileStatus.hohoAkaRate = Mathf.Clamp(value, 0f, 1f);
			if (null != base.customMatFace)
			{
				base.customMatFace.SetFloat(ChaShader.HohoAka, base.fileStatus.hohoAkaRate);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x001178BC File Offset: 0x00115CBC
		// (set) Token: 0x06003001 RID: 12289 RVA: 0x001178C4 File Offset: 0x00115CC4
		public ShapeInfoBase sibHand { get; set; }

		// Token: 0x06003002 RID: 12290 RVA: 0x001178CD File Offset: 0x00115CCD
		protected void InitializeControlHandAll()
		{
			this.InitializeControlHandObject();
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x001178D5 File Offset: 0x00115CD5
		protected void InitializeControlHandObject()
		{
			this.sibHand = new ShapeHandInfo();
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x001178E2 File Offset: 0x00115CE2
		protected void ReleaseControlHandAll()
		{
			this.ReleaseControlHandObject(true);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x001178EB File Offset: 0x00115CEB
		protected void ReleaseControlHandObject(bool init = true)
		{
			if (this.sibHand != null)
			{
				this.sibHand.ReleaseShapeInfo();
			}
			if (init)
			{
				this.InitializeControlHandObject();
			}
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00117910 File Offset: 0x00115D10
		public bool InitShapeHand(Transform trfBone)
		{
			if (this.sibHand == null)
			{
				return false;
			}
			if (null == trfBone)
			{
				return false;
			}
			this.sibHand.InitShapeInfo("abdata", "list/customshape.unity3d", "list/customshape.unity3d", "cf_anmShapeHand", "cf_customhand", trfBone);
			for (int i = 0; i < 2; i++)
			{
				this.SetShapeHandValue(i, 0, 0, 0f);
			}
			return true;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x0011797F File Offset: 0x00115D7F
		public void ReleaseShapeHand()
		{
			if (this.sibHand == null)
			{
				return;
			}
			this.sibHand.ReleaseShapeInfo();
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00117998 File Offset: 0x00115D98
		public bool GetEnableShapeHand(int lr)
		{
			return base.fileStatus.enableShapeHand[lr];
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x001179A7 File Offset: 0x00115DA7
		public void SetEnableShapeHand(int lr, bool enable)
		{
			base.fileStatus.enableShapeHand[lr] = enable;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x001179B7 File Offset: 0x00115DB7
		public int GetShapeIndexHandCount()
		{
			return this.sibHand.GetKeyCount();
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x001179C4 File Offset: 0x00115DC4
		public int GetShapeHandIndex(int lr, int no)
		{
			if (2 <= no)
			{
				return 0;
			}
			return base.fileStatus.shapeHandPtn[lr, no];
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x001179E1 File Offset: 0x00115DE1
		public float GetShapeHandBlendValue(int lr)
		{
			return base.fileStatus.shapeHandBlendValue[lr];
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x001179F0 File Offset: 0x00115DF0
		public bool SetShapeHandValue(int lr, int idx01, int idx02, float blend)
		{
			base.fileStatus.shapeHandPtn[lr, 0] = idx01;
			base.fileStatus.shapeHandPtn[lr, 1] = idx02;
			base.fileStatus.shapeHandBlendValue[lr] = blend;
			if (this.sibHand != null && this.sibHand.InitEnd)
			{
				this.sibHand.ChangeValue(lr, idx01, idx02, blend);
			}
			return true;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00117A60 File Offset: 0x00115E60
		public bool SetShapeHandIndex(int lr, int idx01 = -1, int idx02 = -1)
		{
			if (idx01 != -1)
			{
				base.fileStatus.shapeHandPtn[lr, 0] = idx01;
			}
			if (idx02 != -1)
			{
				base.fileStatus.shapeHandPtn[lr, 1] = idx02;
			}
			if (this.sibHand != null && this.sibHand.InitEnd)
			{
				this.sibHand.ChangeValue(lr, base.fileStatus.shapeHandPtn[lr, 0], base.fileStatus.shapeHandPtn[lr, 1], base.fileStatus.shapeHandBlendValue[lr]);
			}
			return true;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00117AFC File Offset: 0x00115EFC
		public bool SetShapeHandBlend(int lr, float blend)
		{
			base.fileStatus.shapeHandBlendValue[lr] = blend;
			if (this.sibHand != null && this.sibHand.InitEnd)
			{
				this.sibHand.ChangeValue(lr, base.fileStatus.shapeHandPtn[lr, 0], base.fileStatus.shapeHandPtn[lr, 1], base.fileStatus.shapeHandBlendValue[lr]);
			}
			return true;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x00117B74 File Offset: 0x00115F74
		public void UpdateAlwaysShapeHand()
		{
			if (this.sibHand == null || !this.sibHand.InitEnd)
			{
				return;
			}
			ShapeHandInfo shapeHandInfo = this.sibHand as ShapeHandInfo;
			shapeHandInfo.updateMask = 0;
			if (base.fileStatus.enableShapeHand[0])
			{
				shapeHandInfo.updateMask |= 1;
			}
			if (base.fileStatus.enableShapeHand[1])
			{
				shapeHandInfo.updateMask |= 2;
			}
			this.sibHand.UpdateAlways();
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x00117BFB File Offset: 0x00115FFB
		// (set) Token: 0x06003012 RID: 12306 RVA: 0x00117C03 File Offset: 0x00116003
		private bool updateAlphaMask { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x00117C0C File Offset: 0x0011600C
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x00117C14 File Offset: 0x00116014
		private bool updateAlphaMask2 { get; set; }

		// Token: 0x06003015 RID: 12309 RVA: 0x00117C1D File Offset: 0x0011601D
		protected void InitializeControlLoadAll()
		{
			this.InitializeControlLoadObject();
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x00117C25 File Offset: 0x00116025
		protected void InitializeControlLoadObject()
		{
			this.aaWeightsHead = new AssignedAnotherWeights();
			this.aaWeightsBody = new AssignedAnotherWeights();
			this.updateAlphaMask = true;
			this.updateAlphaMask2 = true;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00117C4B File Offset: 0x0011604B
		protected void ReleaseControlLoadAll()
		{
			this.ReleaseControlLoadObject(false);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x00117C54 File Offset: 0x00116054
		protected void ReleaseControlLoadObject(bool init = true)
		{
			if (this.aaWeightsHead != null)
			{
				this.aaWeightsHead.Release();
			}
			if (this.aaWeightsBody != null)
			{
				this.aaWeightsBody.Release();
			}
			if (init)
			{
				this.InitializeControlLoadObject();
			}
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00117C8E File Offset: 0x0011608E
		public bool Load(bool reflectStatus = false)
		{
			base.StartCoroutine(this.LoadAsync(false, false));
			return true;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x00117CA0 File Offset: 0x001160A0
		public IEnumerator LoadAsync(bool reflectStatus = false, bool asyncFlags = true)
		{
			byte[] status = null;
			if (reflectStatus)
			{
				status = base.chaFile.GetStatusBytes();
			}
			this.ReleaseObject();
			if (asyncFlags)
			{
				yield return null;
			}
			base.objTop = new GameObject("BodyTop");
			if (null != base.objRoot)
			{
				base.objTop.transform.SetParent(base.objRoot.transform, false);
			}
			if (asyncFlags)
			{
				this.SetActiveTop(false);
			}
			this.AddUpdateCMBodyTexFlags(true, true, true, true);
			this.AddUpdateCMBodyColorFlags(true, true, true, true);
			this.AddUpdateCMBodyGlossFlags(true, true);
			this.AddUpdateCMBodyLayoutFlags(true, true);
			this.CreateBodyTexture();
			if (asyncFlags)
			{
				yield return base.StartCoroutine(base.Load_Coroutine<GameObject>("chara/oo_base.unity3d", "p_cf_anim", delegate(GameObject x)
				{
					base.objAnim = x;
				}, true, "abdata"));
			}
			else
			{
				base.objAnim = CommonLib.LoadAsset<GameObject>("chara/oo_base.unity3d", "p_cf_anim", true, "abdata");
			}
			Singleton<Character>.Instance.AddLoadAssetBundle("chara/oo_base.unity3d", "abdata");
			if (!(null != base.objAnim))
			{
				yield break;
			}
			base.cmpBoneBody = base.objAnim.GetComponent<CmpBoneBody>();
			if (null != base.cmpBoneBody)
			{
				base.cmpBoneBody.InitDynamicBonesBustAndHip();
			}
			base.animBody = base.objAnim.GetComponent<Animator>();
			base.objAnim.transform.SetParent(base.objTop.transform, false);
			if (base.sex == 0)
			{
				for (int i = 0; i < base.enableDynamicBoneBustAndHip.Length; i++)
				{
					base.enableDynamicBoneBustAndHip[i] = false;
				}
			}
			base.objBodyBone = base.objAnim.transform.FindLoop("cf_J_Root");
			if (null != base.objBodyBone)
			{
				this.aaWeightsBody.CreateBoneList(base.objBodyBone, string.Empty);
				NeckLookControllerVer2[] componentsInChildren = base.objBodyBone.GetComponentsInChildren<NeckLookControllerVer2>(true);
				if (componentsInChildren.Length != 0)
				{
					base.neckLookCtrl = componentsInChildren[0];
				}
				if (null != base.neckLookCtrl)
				{
					this.ChangeLookNeckTarget(base.fileStatus.neckTargetType, null, 0.5f, 0f, 1f, 0.8f);
					this.ChangeLookNeckPtn(0, 1f);
				}
				this.InitShapeBody(base.objBodyBone.transform);
				this.InitShapeHand(base.objBodyBone.transform);
				base.objNeckLookTargetP = new GameObject("N_NeckLookTargetP");
				Transform transform = base.cmpBoneBody.targetEtc.trfNeckLookTarget;
				if (null != base.objNeckLookTargetP && null != transform)
				{
					base.objNeckLookTargetP.transform.SetParent(transform, false);
					base.objNeckLookTarget = new GameObject("N_NeckLookTarget");
					if (null != base.objNeckLookTarget)
					{
						base.objNeckLookTarget.transform.SetParent(base.objNeckLookTargetP.transform, false);
					}
				}
				base.objEyesLookTargetP = new GameObject("N_EyesLookTargetP");
				transform = base.cmpBoneBody.targetEtc.trfHeadParent;
				if (null != base.objEyesLookTargetP && null != transform)
				{
					base.objEyesLookTargetP.transform.SetParent(transform, false);
					base.objEyesLookTarget = new GameObject("N_EyesLookTarget");
					if (null != base.objEyesLookTarget)
					{
						base.objEyesLookTarget.transform.SetParent(base.objEyesLookTargetP.transform, false);
					}
				}
				if (base.sex == 0)
				{
					base.cmpBoneBody.InactiveBustDynamicBoneCollider();
				}
			}
			base.fullBodyIK = base.objAnim.GetComponent<FullBodyBipedIK>();
			base.fullBodyIK.solver.Initiate(base.fullBodyIK.transform);
			if (asyncFlags)
			{
				yield return base.StartCoroutine(base.Load_Coroutine<GameObject>("chara/oo_base.unity3d", "p_cf_head_bone", delegate(GameObject x)
				{
					base.objHeadBone = x;
				}, true, "abdata"));
			}
			else
			{
				base.objHeadBone = CommonLib.LoadAsset<GameObject>("chara/oo_base.unity3d", "p_cf_head_bone", true, "abdata");
			}
			Singleton<Character>.Instance.AddLoadAssetBundle("chara/oo_base.unity3d", "abdata");
			if (null != base.objHeadBone)
			{
				base.cmpBoneHead = base.objHeadBone.GetComponent<CmpBoneHead>();
				base.objHeadBone.transform.SetParent(base.cmpBoneBody.targetEtc.trfHeadParent, false);
				EyeLookController[] componentsInChildren2 = base.objHeadBone.GetComponentsInChildren<EyeLookController>(true);
				if (componentsInChildren2.Length != 0)
				{
					base.eyeLookCtrl = componentsInChildren2[0];
				}
				if (null != base.eyeLookCtrl)
				{
					EyeLookCalc component = base.eyeLookCtrl.GetComponent<EyeLookCalc>();
					if (null != component)
					{
						this.ChangeLookEyesTarget(base.fileStatus.eyesTargetType, null, 0.5f, 0f, 1f, 2f);
						this.ChangeLookEyesPtn(0);
					}
				}
				this.aaWeightsHead.CreateBoneList(base.objHeadBone, string.Empty);
				string bodyAsset = ChaABDefine.BodyAsset((int)base.sex);
				if (asyncFlags)
				{
					yield return base.StartCoroutine(base.Load_Coroutine<GameObject>("chara/oo_base.unity3d", bodyAsset, delegate(GameObject x)
					{
						base.objBody = x;
					}, true, "abdata"));
				}
				else
				{
					base.objBody = CommonLib.LoadAsset<GameObject>("chara/oo_base.unity3d", bodyAsset, true, "abdata");
				}
				Singleton<Character>.Instance.AddLoadAssetBundle("chara/oo_base.unity3d", "abdata");
				if (null != base.objBody)
				{
					base.cmpBody = base.objBody.GetComponent<CmpBody>();
					this.SetBodyBaseMaterial();
					base.objBody.transform.SetParent(base.objTop.transform, false);
					this.aaWeightsBody.AssignedWeightsAndSetBounds(base.objBody, "cf_J_Root", ChaControlDefine.bounds, base.cmpBoneBody.targetEtc.trfRoot);
					if (base.sex == 1)
					{
						this.bustNormal = new BustNormal();
						this.bustNormal.Init(base.objBody, "chara/oo_base.unity3d", "p_cf_body_00_Nml", "abdata");
					}
					this.ChangeCustomBodyWithoutCustomTexture();
				}
				string simpleBodyAsset = ChaABDefine.SilhouetteAsset((int)base.sex);
				if (asyncFlags)
				{
					yield return base.StartCoroutine(base.Load_Coroutine<GameObject>("chara/oo_base.unity3d", simpleBodyAsset, delegate(GameObject x)
					{
						base.objSimpleBody = x;
					}, true, "abdata"));
				}
				else
				{
					base.objSimpleBody = CommonLib.LoadAsset<GameObject>("chara/oo_base.unity3d", simpleBodyAsset, true, "abdata");
				}
				Singleton<Character>.Instance.AddLoadAssetBundle("chara/oo_base.unity3d", "abdata");
				if (null != base.objSimpleBody)
				{
					base.cmpSimpleBody = base.objSimpleBody.GetComponent<CmpBody>();
					Color color = (base.sex != 0) ? new Color(230f, 115f, 115f) : new Color(115f, 175f, 230f);
					if (Singleton<Config>.IsInstance() && Config.GraphicData != null)
					{
						color = Config.GraphicData.SilhouetteColor;
					}
					this.ChangeSimpleBodyColor(color);
					base.objSimpleBody.transform.SetParent(base.objTop.transform, false);
					this.aaWeightsBody.AssignedWeightsAndSetBounds(base.objSimpleBody, "cf_J_Root", ChaControlDefine.bounds, base.cmpBoneBody.targetEtc.trfRoot);
				}
				base.InitializeAccessoryParent();
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeHeadAsync(true));
				}
				else
				{
					this.ChangeHead(true);
				}
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeHairAllAsync(true));
				}
				else
				{
					this.ChangeHairAll(true);
				}
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeClothesAsync(true));
				}
				else
				{
					this.ChangeClothes(true);
				}
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeAccessoryAsync(true));
				}
				else
				{
					this.ChangeAccessory(true);
				}
				base.updateBustSize = true;
				base.reSetupDynamicBoneBust = true;
				this.UpdateSiru(true);
				base.updateWet = true;
				this.ChangeHohoAkaRate(base.fileStatus.hohoAkaRate);
				if (reflectStatus)
				{
					base.chaFile.SetStatusBytes(status);
				}
				if (Singleton<Character>.Instance.enableCharaLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
				base.loadEnd = true;
				yield break;
			}
			yield break;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x00117CC9 File Offset: 0x001160C9
		public bool Reload(bool noChangeClothes = false, bool noChangeHead = false, bool noChangeHair = false, bool noChangeBody = false, bool forceChange = true)
		{
			base.StartCoroutine(this.ReloadAsync(noChangeClothes, noChangeHead, noChangeHair, noChangeBody, forceChange, false));
			return true;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x00117CE4 File Offset: 0x001160E4
		public IEnumerator ReloadAsync(bool noChangeClothes = false, bool noChangeHead = false, bool noChangeHair = false, bool noChangeBody = false, bool forceChange = true, bool asyncFlags = true)
		{
			if (asyncFlags)
			{
				this.SetActiveTop(false);
			}
			if (!noChangeBody)
			{
				this.AddUpdateCMBodyTexFlags(true, true, true, true);
				this.AddUpdateCMBodyGlossFlags(true, true);
				this.AddUpdateCMBodyColorFlags(true, true, true, true);
				this.AddUpdateCMBodyLayoutFlags(true, true);
				this.CreateBodyTexture();
				this.ChangeCustomBodyWithoutCustomTexture();
				if (noChangeHead)
				{
					this.AddUpdateCMFaceTexFlags(true, true, true, true, true, true, true);
					this.AddUpdateCMFaceGlossFlags(true, true, true, true, true);
					this.AddUpdateCMFaceColorFlags(true, true, true, true, true, true, true);
					this.AddUpdateCMFaceLayoutFlags(true, true, true);
					this.CreateFaceTexture();
					this.SetFaceBaseMaterial();
					this.ChangeCustomFaceWithoutCustomTexture();
				}
			}
			if (!noChangeHead)
			{
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeHeadAsync(forceChange));
				}
				else
				{
					this.ChangeHead(forceChange);
				}
			}
			if (!noChangeHair)
			{
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeHairAllAsync(forceChange));
				}
				else
				{
					this.ChangeHairAll(forceChange);
				}
			}
			if (!noChangeClothes)
			{
				this.underMaskReflectionType = -1;
				this.underMaskBreakDisable = false;
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeClothesAsync(forceChange));
				}
				else
				{
					this.ChangeClothes(forceChange);
				}
				if (asyncFlags)
				{
					yield return base.StartCoroutine(this.ChangeAccessoryAsync(forceChange));
				}
				else
				{
					this.ChangeAccessory(forceChange);
				}
			}
			if (asyncFlags)
			{
				yield return null;
			}
			this.UpdateShapeBodyValueFromCustomInfo();
			this.UpdateShapeFaceValueFromCustomInfo();
			base.updateBustSize = true;
			base.reSetupDynamicBoneBust = true;
			this.UpdateClothesStateAll();
			if (Singleton<Character>.Instance.enableCharaLoadGCClear)
			{
				UnityEngine.Resources.UnloadUnusedAssets();
				GC.Collect();
			}
			if (asyncFlags)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x00117D2C File Offset: 0x0011612C
		public void ChangeHead(bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeHeadAsync(base.fileFace.headId, forceChange, false));
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x00117D48 File Offset: 0x00116148
		public void ChangeHead(int _headId, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeHeadAsync(_headId, forceChange, false));
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00117D5C File Offset: 0x0011615C
		public IEnumerator ChangeHeadAsync(bool forceChange = false)
		{
			yield return base.StartCoroutine(this.ChangeHeadAsync(base.fileFace.headId, forceChange, true));
			yield break;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x00117D80 File Offset: 0x00116180
		public IEnumerator ChangeHeadAsync(int _headId, bool forceChange = false, bool asyncFlags = true)
		{
			if (_headId == -1)
			{
				yield break;
			}
			if (!forceChange && null != base.objHead && _headId == base.fileFace.headId)
			{
				yield break;
			}
			string objName = "ct_head";
			if (null != base.objHead)
			{
				base.SafeDestroy(base.objHead);
				base.objHead = null;
				base.infoHead = null;
				base.cmpFace = null;
				this.ReleaseShapeFace();
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			int category = (base.sex != 0) ? 210 : 110;
			int defId = (base.sex != 0) ? 0 : 0;
			if (asyncFlags)
			{
				IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
				{
					base.objHead = o;
				}, category, _headId, objName, false, 0, null, defId, true, false);
				yield return base.StartCoroutine(cor);
			}
			else
			{
				base.objHead = this.LoadCharaFbxData(category, _headId, objName, false, 0, null, defId, false);
			}
			if (null != base.objHead)
			{
				base.cmpFace = base.objHead.GetComponent<CmpFace>();
				CommonLib.CopySameNameTransform(base.objHeadBone.transform, base.objHead.transform);
				base.objHead.transform.SetParent(base.objHeadBone.transform, false);
				this.aaWeightsHead.AssignedWeightsAndSetBounds(base.objHead, "cf_J_FaceRoot", ChaControlDefine.bounds, null);
				if (asyncFlags)
				{
					yield return null;
				}
				ListInfoComponent libComponent = base.objHead.GetComponent<ListInfoComponent>();
				ListInfoBase data = libComponent.data;
				base.infoHead = data;
				ListInfoBase lib = data;
				base.fileFace.headId = lib.Id;
				Dictionary<int, ListInfoBase> dictSkin = base.lstCtrl.GetCategoryInfo((base.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_f : ChaListDefine.CategoryNo.mt_skin_f);
				List<int> lstSkin = (from x in dictSkin
				where x.Value.GetInfoInt(ChaListDefine.KeyType.HeadID) == base.fileFace.headId
				select x.Key).ToList<int>();
				if (!lstSkin.Contains(base.fileFace.skinId))
				{
					base.fileFace.skinId = lstSkin[0];
				}
				string customManifest = lib.GetInfo(ChaListDefine.KeyType.MainManifest);
				string customAssetBundle = lib.GetInfo(ChaListDefine.KeyType.MainAB);
				string customAsset = lib.GetInfo(ChaListDefine.KeyType.MatData);
				this.InitBaseCustomTextureFace(customManifest, customAssetBundle, customAsset);
				this.AddUpdateCMFaceTexFlags(true, true, true, true, true, true, true);
				this.AddUpdateCMFaceGlossFlags(true, true, true, true, true);
				this.AddUpdateCMFaceColorFlags(true, true, true, true, true, true, true);
				this.AddUpdateCMFaceLayoutFlags(true, true, true);
				this.CreateFaceTexture();
				this.SetFaceBaseMaterial();
				this.UpdateSiru(true);
				base.updateWet = true;
				this.ChangeHohoAkaRate(base.fileStatus.hohoAkaRate);
				this.InitShapeFace(base.objHeadBone.transform, lib.GetInfo(ChaListDefine.KeyType.MainManifest), lib.GetInfo(ChaListDefine.KeyType.MainAB), lib.GetInfo(ChaListDefine.KeyType.ShapeAnime));
				this.ChangeCustomFaceWithoutCustomTexture();
				this.HideEyeHighlight(false);
				base.fbsCtrl = base.objHead.GetComponent<FaceBlendShape>();
				if (null != base.fbsCtrl)
				{
					base.eyebrowCtrl = base.fbsCtrl.EyebrowCtrl;
					base.eyesCtrl = base.fbsCtrl.EyesCtrl;
					base.mouthCtrl = base.fbsCtrl.MouthCtrl;
					this.ChangeEyesBlinkFlag(base.fileStatus.eyesBlink);
					this.ChangeEyebrowPtn(base.fileStatus.eyebrowPtn, true);
					this.ChangeEyebrowOpenMax(base.fileStatus.eyebrowOpenMax);
					this.ChangeEyesPtn(base.fileStatus.eyesPtn, true);
					this.ChangeEyesOpenMax(base.fileStatus.eyesOpenMax);
					this.ChangeMouthPtn(base.fileStatus.mouthPtn, true);
					this.ChangeMouthOpenMax(base.fileStatus.mouthOpenMax);
					this.ChangeMouthOpenMin(base.fileStatus.mouthOpenMin);
				}
			}
			if (asyncFlags)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x00117DB0 File Offset: 0x001161B0
		public void ChangeHairAll(bool forceChange = false)
		{
			int[] array = (int[])Enum.GetValues(typeof(ChaFileDefine.HairKind));
			foreach (int num in array)
			{
				base.StartCoroutine(this.ChangeHairAsync(num, base.fileHair.parts[num].id, forceChange, false));
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00117E10 File Offset: 0x00116210
		public IEnumerator ChangeHairAllAsync(bool forceChange = false)
		{
			int[] hairKind = (int[])Enum.GetValues(typeof(ChaFileDefine.HairKind));
			foreach (int i in hairKind)
			{
				yield return base.StartCoroutine(this.ChangeHairAsync(i, base.fileHair.parts[i].id, forceChange, true));
			}
			yield break;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x00117E32 File Offset: 0x00116232
		public bool ChangeHair(int kind, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeHairAsync(kind, base.fileHair.parts[kind].id, forceChange, false));
			return true;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x00117E57 File Offset: 0x00116257
		public void ChangeHair(int kind, int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeHairAsync(kind, id, forceChange, false));
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x00117E6C File Offset: 0x0011626C
		public IEnumerator ChangeHairAsync(int kind, int id, bool forceChange = false, bool asyncFlags = true)
		{
			ChaListDefine.CategoryNo[] fbxType = new ChaListDefine.CategoryNo[]
			{
				ChaListDefine.CategoryNo.so_hair_b,
				ChaListDefine.CategoryNo.so_hair_f,
				ChaListDefine.CategoryNo.so_hair_s,
				ChaListDefine.CategoryNo.so_hair_o
			};
			string[] objName = new string[]
			{
				"ct_hairB",
				"ct_hairF",
				"ct_hairS",
				"ct_hairO"
			};
			int[,] array = new int[2, 4];
			array[0, 1] = 2;
			array[1, 1] = 1;
			int[,] defId = array;
			if (!forceChange && null != base.objHair[kind] && id == base.fileHair.parts[kind].id)
			{
				yield break;
			}
			if (null != base.objHair[kind])
			{
				base.SafeDestroy(base.objHair[kind]);
				base.objHair[kind] = null;
				base.infoHair[kind] = null;
				base.cmpHair[kind] = null;
				if (id != base.fileHair.parts[kind].bundleId)
				{
					base.fileHair.parts[kind].dictBundle.Clear();
					base.fileHair.parts[kind].bundleId = -1;
				}
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			Transform trfHairParent = base.cmpBoneHead.targetEtc.trfHairParent;
			if (asyncFlags)
			{
				IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
				{
					this.objHair[kind] = o;
				}, (int)fbxType[kind], id, objName[kind], false, 0, trfHairParent, defId[(int)base.sex, kind], true, false);
				yield return base.StartCoroutine(cor);
			}
			else
			{
				base.objHair[kind] = this.LoadCharaFbxData((int)fbxType[kind], id, objName[kind], false, 0, trfHairParent, defId[(int)base.sex, kind], false);
			}
			if (null != base.objHair[kind])
			{
				ListInfoComponent component = base.objHair[kind].GetComponent<ListInfoComponent>();
				ListInfoBase listInfoBase = base.infoHair[kind] = component.data;
				base.fileHair.parts[kind].id = listInfoBase.Id;
				if (kind == 0)
				{
					base.fileHair.kind = listInfoBase.Kind;
				}
				base.cmpHair[kind] = base.objHair[kind].GetComponent<CmpHair>();
				if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpHair[kind])
				{
				}
				if (null != base.cmpHair[kind] && (id != base.fileHair.parts[kind].bundleId || base.fileHair.parts[kind].dictBundle.Count != base.cmpHair[kind].boneInfo.Length))
				{
					base.fileHair.parts[kind].dictBundle.Clear();
					for (int i = 0; i < base.cmpHair[kind].boneInfo.Length; i++)
					{
						base.fileHair.parts[kind].dictBundle[i] = new ChaFileHair.PartsInfo.BundleInfo();
						this.SetDefaultHairCorrectPosRate(kind, i);
						this.SetDefaultHairCorrectRotRate(kind, i);
					}
				}
				base.fileHair.parts[kind].bundleId = id;
				this.ChangeSettingHairColor(kind, true, true, true);
				this.ChangeSettingHairSpecular(kind);
				this.ChangeSettingHairMetallic(kind);
				this.ChangeSettingHairSmoothness(kind);
				this.ChangeSettingHairAcsColor(kind);
				this.ChangeSettingHairCorrectPosAll(kind);
				this.ChangeSettingHairCorrectRotAll(kind);
				this.ChangeSettingHairShader();
				if (Game.isAdd30)
				{
					this.ChangeSettingHairMeshType(kind);
					this.ChangeSettingHairMeshColor(kind);
					this.ChangeSettingHairMeshLayout(kind);
				}
			}
			base.updateWet = true;
			if (asyncFlags)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00117EA4 File Offset: 0x001162A4
		public void ChangeClothes(bool forceChange = false)
		{
			int[] array = (int[])Enum.GetValues(typeof(ChaFileDefine.ClothesKind));
			foreach (int num in array)
			{
				base.StartCoroutine(this.ChangeClothesAsync(num, this.nowCoordinate.clothes.parts[num].id, forceChange, false));
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x00117F07 File Offset: 0x00116307
		public void ChangeClothes(int kind, int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesAsync(kind, id, forceChange, false));
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x00117F1C File Offset: 0x0011631C
		public IEnumerator ChangeClothesAsync(bool forceChange = false)
		{
			int[] clothesKind = (int[])Enum.GetValues(typeof(ChaFileDefine.ClothesKind));
			foreach (int i in clothesKind)
			{
				yield return base.StartCoroutine(this.ChangeClothesAsync(i, this.nowCoordinate.clothes.parts[i].id, forceChange, true));
			}
			yield break;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00117F40 File Offset: 0x00116340
		public IEnumerator ChangeClothesAsync(int kind, int id, bool forceChange = false, bool asyncFlags = true)
		{
			if (asyncFlags)
			{
				switch (kind)
				{
				case 0:
					yield return base.StartCoroutine(this.ChangeClothesTopAsync(id, forceChange, true));
					this.updateAlphaMask = true;
					this.updateAlphaMask2 = true;
					break;
				case 1:
					yield return base.StartCoroutine(this.ChangeClothesBotAsync(id, forceChange, true));
					this.updateAlphaMask2 = true;
					break;
				case 2:
					yield return base.StartCoroutine(this.ChangeClothesInnerTAsync(id, forceChange, true));
					break;
				case 3:
					yield return base.StartCoroutine(this.ChangeClothesInnerBAsync(id, forceChange, true));
					break;
				case 4:
					yield return base.StartCoroutine(this.ChangeClothesGlovesAsync(id, forceChange, true));
					break;
				case 5:
					yield return base.StartCoroutine(this.ChangeClothesPanstAsync(id, forceChange, true));
					break;
				case 6:
					yield return base.StartCoroutine(this.ChangeClothesSocksAsync(id, forceChange, true));
					break;
				case 7:
					yield return base.StartCoroutine(this.ChangeClothesShoesAsync(id, forceChange, true));
					break;
				}
			}
			else
			{
				switch (kind)
				{
				case 0:
					this.ChangeClothesTop(id, forceChange);
					this.updateAlphaMask = true;
					this.updateAlphaMask2 = true;
					break;
				case 1:
					this.ChangeClothesBot(id, forceChange);
					this.updateAlphaMask2 = true;
					break;
				case 2:
					this.ChangeClothesInnerT(id, forceChange);
					break;
				case 3:
					this.ChangeClothesInnerB(id, forceChange);
					break;
				case 4:
					this.ChangeClothesGloves(id, forceChange);
					break;
				case 5:
					this.ChangeClothesPanst(id, forceChange);
					break;
				case 6:
					this.ChangeClothesSocks(id, forceChange);
					break;
				case 7:
					this.ChangeClothesShoes(id, forceChange);
					break;
				}
			}
			yield break;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x00117F78 File Offset: 0x00116378
		public void ChangeClothesTop(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesTopAsync(id, forceChange, false));
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x00117F8C File Offset: 0x0011638C
		public IEnumerator ChangeClothesTopAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			this.updateAlphaMask = true;
			this.updateAlphaMask2 = true;
			bool load = true;
			bool release = true;
			int kindNo = 0;
			string objName = "ct_clothesTop";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				this.notInnerT = false;
				this.notBot = false;
				this.RemoveClothesStateKind(kindNo);
				this.ReleaseAlphaMaskTexture(0);
				this.ReleaseAlphaMaskTexture(1);
				if (this.underMaskReflectionType != 1)
				{
					this.ReleaseAlphaMaskTexture(2);
					this.ReleaseAlphaMaskTexture(3);
					this.ReleaseAlphaMaskTexture(4);
					this.ReleaseAlphaMaskTexture(5);
					this.underMaskReflectionType = -1;
				}
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, (base.sex != 0) ? 240 : 140, id, objName, true, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData((base.sex != 0) ? 240 : 140, id, objName, true, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase.Id;
					this.notInnerT = (listInfoBase.GetInfoInt(ChaListDefine.KeyType.NotBra) == 1);
					this.notBot = (listInfoBase.GetInfoInt(ChaListDefine.KeyType.Coordinate) == 1);
					this.AddClothesStateKind(kindNo, listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
					this.LoadAlphaMaskTexture(listInfoBase.GetInfo(ChaListDefine.KeyType.OverBodyMaskAB), listInfoBase.GetInfo(ChaListDefine.KeyType.OverBodyMask), 0);
					if (base.customMatBody)
					{
						base.customMatBody.SetTexture(ChaShader.AlphaMask, this.texBodyAlphaMask);
					}
					this.LoadAlphaMaskTexture(listInfoBase.GetInfo(ChaListDefine.KeyType.OverBraMaskAB), listInfoBase.GetInfo(ChaListDefine.KeyType.OverBraMask), 1);
					if (base.rendBra != null && base.rendBra[0])
					{
						base.rendBra[0].material.SetTexture(ChaShader.AlphaMask, this.texBraAlphaMask);
					}
					if (this.LoadAlphaMaskTexture(listInfoBase.GetInfo(ChaListDefine.KeyType.OverBodyBMaskAB), listInfoBase.GetInfo(ChaListDefine.KeyType.OverBodyBMask), 5))
					{
						if (base.customMatBody)
						{
							base.customMatBody.SetTexture(ChaShader.AlphaMask2, this.texBodyBAlphaMask);
						}
						this.LoadAlphaMaskTexture(listInfoBase.GetInfo(ChaListDefine.KeyType.OverInnerTBMaskAB), listInfoBase.GetInfo(ChaListDefine.KeyType.OverInnerTBMask), 2);
						if (null != base.rendInnerTB)
						{
							base.rendInnerTB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerTBAlphaMask);
						}
						this.LoadAlphaMaskTexture(listInfoBase.GetInfo(ChaListDefine.KeyType.OverInnerBMaskAB), listInfoBase.GetInfo(ChaListDefine.KeyType.OverInnerBMask), 3);
						if (null != base.rendInnerB)
						{
							base.rendInnerB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerBAlphaMask);
						}
						this.LoadAlphaMaskTexture(listInfoBase.GetInfo(ChaListDefine.KeyType.OverPanstMaskAB), listInfoBase.GetInfo(ChaListDefine.KeyType.OverPanstMask), 4);
						if (null != base.rendPanst)
						{
							base.rendPanst.material.SetTexture(ChaShader.AlphaMask2, this.texPanstAlphaMask);
						}
						this.underMaskReflectionType = 0;
						this.underMaskBreakDisable = ("1" == listInfoBase.GetInfo(ChaListDefine.KeyType.BreakDisableMask));
					}
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			this.UpdateSiru(true);
			base.updateWet = true;
			if ("0" == base.infoClothes[kindNo].GetInfo(ChaListDefine.KeyType.Coordinate))
			{
				int botKind = 1;
				if (null == base.objClothes[botKind])
				{
					if (asyncFlags)
					{
						yield return base.StartCoroutine(this.ChangeClothesBotAsync(this.nowCoordinate.clothes.parts[botKind].id, false, true));
					}
					else
					{
						this.ChangeClothesBot(this.nowCoordinate.clothes.parts[botKind].id, false);
					}
				}
			}
			else if (this.IsClothes(1))
			{
				ListInfoBase listInfoBase2 = base.infoClothes[1];
				if (listInfoBase2 != null && "0" != listInfoBase2.GetInfo(ChaListDefine.KeyType.OverBodyBMaskAB) && this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverBodyBMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverBodyBMask), 5))
				{
					if (base.customMatBody)
					{
						base.customMatBody.SetTexture(ChaShader.AlphaMask2, this.texBodyBAlphaMask);
					}
					this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerTBMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerTBMask), 2);
					if (null != base.rendInnerTB)
					{
						base.rendInnerTB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerTBAlphaMask);
					}
					this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerBMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerBMask), 3);
					if (null != base.rendInnerB)
					{
						base.rendInnerB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerBAlphaMask);
					}
					this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverPanstMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverPanstMask), 4);
					if (null != base.rendPanst)
					{
						base.rendPanst.material.SetTexture(ChaShader.AlphaMask2, this.texPanstAlphaMask);
					}
					this.underMaskReflectionType = 1;
					this.underMaskBreakDisable = ("1" == listInfoBase2.GetInfo(ChaListDefine.KeyType.BreakDisableMask));
				}
			}
			yield break;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x00117FBC File Offset: 0x001163BC
		public void ChangeClothesBot(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesBotAsync(id, forceChange, false));
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x00117FD0 File Offset: 0x001163D0
		public IEnumerator ChangeClothesBotAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			this.updateAlphaMask2 = true;
			bool load = true;
			bool release = true;
			int kindNo = 1;
			string objName = "ct_clothesBot";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			ChaListDefine.CategoryNo cateTop = (base.sex != 0) ? ChaListDefine.CategoryNo.fo_top : ChaListDefine.CategoryNo.mo_top;
			Dictionary<int, ListInfoBase> dictFbx = base.lstCtrl.GetCategoryInfo(cateTop);
			if (dictFbx.Count != 0)
			{
				ListInfoBase listInfoBase = null;
				if (dictFbx.TryGetValue(this.nowCoordinate.clothes.parts[0].id, out listInfoBase) && "1" == listInfoBase.GetInfo(ChaListDefine.KeyType.Coordinate))
				{
					load = false;
					release = true;
				}
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				if (this.underMaskReflectionType != 0)
				{
					this.ReleaseAlphaMaskTexture(2);
					this.ReleaseAlphaMaskTexture(3);
					this.ReleaseAlphaMaskTexture(4);
					this.ReleaseAlphaMaskTexture(5);
					this.underMaskReflectionType = -1;
				}
				this.RemoveClothesStateKind(kindNo);
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, (base.sex != 0) ? 241 : 141, id, objName, true, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData((base.sex != 0) ? 241 : 141, id, objName, true, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase2 = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase2.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase2.Id;
					this.AddClothesStateKind(kindNo, listInfoBase2.GetInfo(ChaListDefine.KeyType.StateType));
					if (this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverBodyBMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverBodyBMask), 5))
					{
						if (base.customMatBody)
						{
							base.customMatBody.SetTexture(ChaShader.AlphaMask2, this.texBodyBAlphaMask);
						}
						this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerTBMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerTBMask), 2);
						if (null != base.rendInnerTB)
						{
							base.rendInnerTB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerTBAlphaMask);
						}
						this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerBMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverInnerBMask), 3);
						if (null != base.rendInnerB)
						{
							base.rendInnerB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerBAlphaMask);
						}
						this.LoadAlphaMaskTexture(listInfoBase2.GetInfo(ChaListDefine.KeyType.OverPanstMaskAB), listInfoBase2.GetInfo(ChaListDefine.KeyType.OverPanstMask), 4);
						if (null != base.rendPanst)
						{
							base.rendPanst.material.SetTexture(ChaShader.AlphaMask2, this.texPanstAlphaMask);
						}
						this.underMaskReflectionType = 1;
						this.underMaskBreakDisable = ("1" == listInfoBase2.GetInfo(ChaListDefine.KeyType.BreakDisableMask));
					}
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			this.UpdateSiru(true);
			base.updateWet = true;
			yield break;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x00118000 File Offset: 0x00116400
		public void ChangeClothesInnerT(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesInnerTAsync(id, forceChange, false));
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x00118014 File Offset: 0x00116414
		public IEnumerator ChangeClothesInnerTAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			bool load = true;
			bool release = true;
			int kindNo = 2;
			string objName = "ct_inner_t";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				base.ReleaseRefObject(7UL);
				this.notInnerB = false;
				this.RemoveClothesStateKind(kindNo);
				base.rendBra = new Renderer[2];
				base.rendInnerTB = null;
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, 242, id, objName, false, 1, base.objTop.transform, 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData(242, id, objName, false, 1, base.objTop.transform, 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.notInnerB = ("1" == listInfoBase.GetInfo(ChaListDefine.KeyType.Coordinate));
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase.Id;
					base.CreateReferenceInfo(7UL, base.objClothes[kindNo]);
					this.AddClothesStateKind(kindNo, listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
					GameObject referenceInfo = base.GetReferenceInfo(ChaReference.RefObjKey.mask_braA);
					if (null != referenceInfo)
					{
						base.rendBra[0] = referenceInfo.GetComponent<Renderer>();
					}
					if (base.rendBra != null && base.rendBra[0])
					{
						base.rendBra[0].material.SetTexture(ChaShader.AlphaMask, this.texBraAlphaMask);
					}
					byte b = base.fileStatus.clothesState[0];
					byte[,] array = new byte[,]
					{
						{
							1,
							1
						},
						{
							0,
							1
						},
						{
							0,
							0
						}
					};
					this.ChangeAlphaMask(new byte[]
					{
						array[(int)b, 0],
						array[(int)b, 1]
					});
					this.ChangeAlphaMaskEx();
					GameObject referenceInfo2 = base.GetReferenceInfo(ChaReference.RefObjKey.mask_innerTB);
					if (null != referenceInfo2)
					{
						base.rendInnerTB = referenceInfo2.GetComponent<Renderer>();
					}
					if (null != base.rendInnerTB)
					{
						base.rendInnerTB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerTBAlphaMask);
					}
					this.ChangeAlphaMask2();
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			this.UpdateSiru(true);
			base.updateWet = true;
			if ("0" == base.infoClothes[kindNo].GetInfo(ChaListDefine.KeyType.Coordinate))
			{
				int kindInnerB = 3;
				if (null == base.objClothes[kindInnerB])
				{
					if (asyncFlags)
					{
						yield return base.StartCoroutine(this.ChangeClothesInnerBAsync(this.nowCoordinate.clothes.parts[kindInnerB].id, false, true));
					}
					else
					{
						this.ChangeClothesInnerB(this.nowCoordinate.clothes.parts[kindInnerB].id, false);
					}
				}
			}
			yield break;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x00118044 File Offset: 0x00116444
		public void ChangeClothesInnerB(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesInnerBAsync(id, forceChange, false));
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x00118058 File Offset: 0x00116458
		public IEnumerator ChangeClothesInnerBAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			bool load = true;
			bool release = true;
			int kindNo = 3;
			string objName = "ct_inner_b";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			Dictionary<int, ListInfoBase> dictFbx = base.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.fo_inner_t);
			if (dictFbx.Count != 0)
			{
				ListInfoBase listInfoBase = null;
				if (dictFbx.TryGetValue(this.nowCoordinate.clothes.parts[2].id, out listInfoBase) && "1" == listInfoBase.GetInfo(ChaListDefine.KeyType.Coordinate))
				{
					load = false;
					release = true;
				}
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				base.ReleaseRefObject(8UL);
				this.RemoveClothesStateKind(kindNo);
				base.rendInnerB = null;
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, 243, id, objName, false, 1, base.objTop.transform, 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData(243, id, objName, false, 1, base.objTop.transform, 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase2 = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase2.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase2.Id;
					base.CreateReferenceInfo(8UL, base.objClothes[kindNo]);
					this.AddClothesStateKind(kindNo, listInfoBase2.GetInfo(ChaListDefine.KeyType.StateType));
					GameObject referenceInfo = base.GetReferenceInfo(ChaReference.RefObjKey.mask_innerB);
					if (null != referenceInfo)
					{
						base.rendInnerB = referenceInfo.GetComponent<Renderer>();
					}
					if (null != base.rendInnerB)
					{
						base.rendInnerB.material.SetTexture(ChaShader.AlphaMask2, this.texInnerBAlphaMask);
					}
					this.ChangeAlphaMask2();
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			this.UpdateSiru(true);
			base.updateWet = true;
			yield break;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x00118088 File Offset: 0x00116488
		public void ChangeClothesGloves(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesGlovesAsync(id, forceChange, false));
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0011809C File Offset: 0x0011649C
		public IEnumerator ChangeClothesGlovesAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			bool load = true;
			bool release = true;
			int kindNo = 4;
			string objName = "ct_gloves";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				this.RemoveClothesStateKind(kindNo);
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, (base.sex != 0) ? 244 : 144, id, objName, false, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData((base.sex != 0) ? 244 : 144, id, objName, false, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase.Id;
					this.AddClothesStateKind(kindNo, listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			yield break;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x001180CC File Offset: 0x001164CC
		public void ChangeClothesPanst(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesPanstAsync(id, forceChange, false));
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x001180E0 File Offset: 0x001164E0
		public IEnumerator ChangeClothesPanstAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			bool load = true;
			bool release = true;
			int kindNo = 5;
			string objName = "ct_panst";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				base.ReleaseRefObject(10UL);
				this.RemoveClothesStateKind(kindNo);
				base.rendPanst = null;
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, 245, id, objName, false, 1, base.objTop.transform, 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData(245, id, objName, false, 1, base.objTop.transform, 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase.Id;
					base.CreateReferenceInfo(10UL, base.objClothes[kindNo]);
					this.AddClothesStateKind(kindNo, listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
					GameObject referenceInfo = base.GetReferenceInfo(ChaReference.RefObjKey.mask_panst);
					if (null != referenceInfo)
					{
						base.rendPanst = referenceInfo.GetComponent<Renderer>();
					}
					if (null != base.rendPanst)
					{
						base.rendPanst.material.SetTexture(ChaShader.AlphaMask2, this.texPanstAlphaMask);
					}
					this.ChangeAlphaMask2();
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			this.UpdateSiru(true);
			base.updateWet = true;
			yield break;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x00118110 File Offset: 0x00116510
		public void ChangeClothesSocks(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesSocksAsync(id, forceChange, false));
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x00118124 File Offset: 0x00116524
		public IEnumerator ChangeClothesSocksAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			bool load = true;
			bool release = true;
			int kindNo = 6;
			string objName = "ct_socks";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				this.RemoveClothesStateKind(kindNo);
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, 246, id, objName, false, 1, base.objTop.transform, 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData(246, id, objName, false, 1, base.objTop.transform, 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase.Id;
					this.AddClothesStateKind(kindNo, listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			yield break;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x00118154 File Offset: 0x00116554
		public void ChangeClothesShoes(int id, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeClothesShoesAsync(id, forceChange, false));
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x00118168 File Offset: 0x00116568
		public IEnumerator ChangeClothesShoesAsync(int id, bool forceChange = false, bool asyncFlags = true)
		{
			bool load = true;
			bool release = true;
			int kindNo = 7;
			string objName = "ct_shoes";
			int nowId = (base.infoClothes[kindNo] != null) ? base.infoClothes[kindNo].Id : -1;
			if (!forceChange && null != base.objClothes[kindNo] && id == nowId)
			{
				load = false;
				release = false;
			}
			this.ReleaseBaseCustomTextureClothes(kindNo, true);
			if (release && null != base.objClothes[kindNo])
			{
				base.SafeDestroy(base.objClothes[kindNo]);
				base.objClothes[kindNo] = null;
				base.infoClothes[kindNo] = null;
				base.cmpClothes[kindNo] = null;
				this.RemoveClothesStateKind(kindNo);
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objClothes[kindNo] = o;
					}, (base.sex != 0) ? 247 : 147, id, objName, false, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objClothes[kindNo] = this.LoadCharaFbxData((base.sex != 0) ? 247 : 147, id, objName, false, 1, base.objTop.transform, (base.sex != 0) ? 0 : 0, false);
				}
				if (asyncFlags)
				{
					yield return null;
				}
				if (null != base.objClothes[kindNo])
				{
					ListInfoComponent component = base.objClothes[kindNo].GetComponent<ListInfoComponent>();
					ListInfoBase listInfoBase = base.infoClothes[kindNo] = component.data;
					base.cmpClothes[kindNo] = base.objClothes[kindNo].GetComponent<CmpClothes>();
					if (null != base.cmpClothes[kindNo])
					{
						base.cmpClothes[kindNo].InitDynamicBones();
					}
					if (!(listInfoBase.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpClothes[kindNo])
					{
					}
					this.nowCoordinate.clothes.parts[kindNo].id = listInfoBase.Id;
					this.AddClothesStateKind(kindNo, listInfoBase.GetInfo(ChaListDefine.KeyType.StateType));
				}
			}
			if (null != base.objClothes[kindNo])
			{
				this.InitBaseCustomTextureClothes(kindNo);
				if (base.loadWithDefaultColorAndPtn)
				{
					this.SetClothesDefaultSetting(kindNo);
					for (int i = 0; i < 3; i++)
					{
						this.nowCoordinate.clothes.parts[kindNo].colorInfo[i].pattern = 0;
					}
				}
				this.ChangeCustomClothes(kindNo, true, true, true, true);
				if (base.releaseCustomInputTexture)
				{
					this.ReleaseBaseCustomTextureClothes(kindNo, false);
				}
				this.ChangeBreakClothes(kindNo);
			}
			yield break;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x00118198 File Offset: 0x00116598
		public void ChangeAccessory(bool forceChange = false)
		{
			for (int i = 0; i < 20; i++)
			{
				base.StartCoroutine(this.ChangeAccessoryAsync(i, this.nowCoordinate.accessory.parts[i].type, this.nowCoordinate.accessory.parts[i].id, this.nowCoordinate.accessory.parts[i].parentKey, forceChange, false));
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0011820D File Offset: 0x0011660D
		public void ChangeAccessory(int slotNo, int type, int id, string parentKey, bool forceChange = false)
		{
			base.StartCoroutine(this.ChangeAccessoryAsync(slotNo, type, id, parentKey, forceChange, false));
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00118224 File Offset: 0x00116624
		public IEnumerator ChangeAccessoryAsync(bool forceChange = false)
		{
			for (int i = 0; i < 20; i++)
			{
				yield return base.StartCoroutine(this.ChangeAccessoryAsync(i, this.nowCoordinate.accessory.parts[i].type, this.nowCoordinate.accessory.parts[i].id, this.nowCoordinate.accessory.parts[i].parentKey, forceChange, true));
			}
			yield break;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x00118248 File Offset: 0x00116648
		public IEnumerator ChangeAccessoryAsync(int slotNo, int type, int id, string parentKey, bool forceChange = false, bool asyncFlags = true)
		{
			if (!MathfEx.RangeEqualOn<int>(0, slotNo, 19))
			{
				yield break;
			}
			ListInfoBase lib = null;
			bool load = true;
			bool release = true;
			if (type == 350 || !MathfEx.RangeEqualOn<int>(351, type, 363))
			{
				release = true;
				load = false;
			}
			else
			{
				if (id == -1)
				{
					release = false;
					load = false;
				}
				int num = (base.infoAccessory[slotNo] != null) ? base.infoAccessory[slotNo].Category : -1;
				int num2 = (base.infoAccessory[slotNo] != null) ? base.infoAccessory[slotNo].Id : -1;
				if (!forceChange && null != base.objAccessory[slotNo] && type == num && id == num2)
				{
					load = false;
					release = false;
				}
				if (id != -1)
				{
					Dictionary<int, ListInfoBase> categoryInfo = base.lstCtrl.GetCategoryInfo((ChaListDefine.CategoryNo)type);
					if (categoryInfo == null)
					{
						release = true;
						load = false;
					}
					else if (!categoryInfo.TryGetValue(id, out lib))
					{
						release = true;
						load = false;
					}
				}
			}
			if (release)
			{
				if (!load)
				{
					this.nowCoordinate.accessory.parts[slotNo].MemberInit();
					this.nowCoordinate.accessory.parts[slotNo].type = 350;
				}
				if (null != base.objAccessory[slotNo])
				{
					base.SafeDestroy(base.objAccessory[slotNo]);
					base.objAccessory[slotNo] = null;
					base.infoAccessory[slotNo] = null;
					base.cmpAccessory[slotNo] = null;
					for (int i = 0; i < 2; i++)
					{
						base.trfAcsMove[slotNo, i] = null;
					}
				}
				if (Singleton<Character>.Instance.customLoadGCClear)
				{
					UnityEngine.Resources.UnloadUnusedAssets();
					GC.Collect();
				}
			}
			if (load)
			{
				if (asyncFlags)
				{
					yield return null;
				}
				byte weight = 0;
				Transform trfParent = null;
				if ("null" == lib.GetInfo(ChaListDefine.KeyType.Parent))
				{
					weight = 2;
					trfParent = base.objTop.transform;
				}
				if (asyncFlags)
				{
					IEnumerator cor = this.LoadCharaFbxDataAsync(delegate(GameObject o)
					{
						this.objAccessory[slotNo] = o;
					}, type, id, "ca_slot" + slotNo.ToString("00"), false, weight, trfParent, -1, true, false);
					yield return base.StartCoroutine(cor);
				}
				else
				{
					base.objAccessory[slotNo] = this.LoadCharaFbxData(type, id, "ca_slot" + slotNo.ToString("00"), false, weight, trfParent, -1, false);
				}
				if (null != base.objAccessory[slotNo])
				{
					ListInfoComponent component = base.objAccessory[slotNo].GetComponent<ListInfoComponent>();
					lib = (base.infoAccessory[slotNo] = component.data);
					base.cmpAccessory[slotNo] = base.objAccessory[slotNo].GetComponent<CmpAccessory>();
					if (null != base.cmpAccessory[slotNo])
					{
						base.cmpAccessory[slotNo].InitDynamicBones();
					}
					if (!(lib.GetInfo(ChaListDefine.KeyType.MainData) != "p_dummy") || null == base.cmpAccessory[slotNo])
					{
					}
					this.nowCoordinate.accessory.parts[slotNo].type = type;
					this.nowCoordinate.accessory.parts[slotNo].id = lib.Id;
					if (base.cmpAccessory != null && null != base.cmpAccessory[slotNo])
					{
						base.trfAcsMove[slotNo, 0] = base.cmpAccessory[slotNo].trfMove01;
						base.trfAcsMove[slotNo, 1] = base.cmpAccessory[slotNo].trfMove02;
					}
				}
			}
			if (null != base.objAccessory[slotNo])
			{
				if (string.Empty == parentKey)
				{
					parentKey = lib.GetInfo(ChaListDefine.KeyType.Parent);
				}
				ParticleSystem[] componentsInChildren = base.objAccessory[slotNo].GetComponentsInChildren<ParticleSystem>();
				if (!componentsInChildren.IsNullOrEmpty<ParticleSystem>())
				{
					base.objAccessory[slotNo].transform.SetParent(base.objRoot.transform, true);
					GameObject gameObject = new GameObject(string.Format("ca_slot{0:00}(dummy)", slotNo));
					ListInfoComponent listInfoComponent = gameObject.AddComponent<ListInfoComponent>();
					listInfoComponent.data = lib;
					ParticleParent particleParent = gameObject.AddComponent<ParticleParent>();
					particleParent.ObjOriginal = base.objAccessory[slotNo];
					base.objAccessory[slotNo] = gameObject;
				}
				this.ChangeAccessoryParent(slotNo, parentKey);
				this.UpdateAccessoryMoveFromInfo(slotNo);
				this.nowCoordinate.accessory.parts[slotNo].partsOfHead = ChaAccessoryDefine.CheckPartsOfHead(parentKey);
				if (base.cmpAccessory != null && base.cmpAccessory[slotNo].typeHair)
				{
					this.ChangeSettingHairTypeAccessoryShader(slotNo);
					this.ChangeHairTypeAccessoryColor(slotNo);
				}
				else
				{
					if (base.loadWithDefaultColorAndPtn)
					{
						this.SetAccessoryDefaultColor(slotNo);
					}
					this.ChangeAccessoryColor(slotNo);
				}
			}
			yield break;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x00118290 File Offset: 0x00116690
		private GameObject LoadCharaFbxData(int category, int id, string createName, bool copyDynamicBone, byte copyWeights, Transform trfParent, int defaultId, bool worldPositionStays = false)
		{
			GameObject actObj = null;
			base.StartCoroutine(this.LoadCharaFbxDataAsync(delegate(GameObject o)
			{
				actObj = o;
			}, category, id, createName, copyDynamicBone, copyWeights, trfParent, defaultId, false, worldPositionStays));
			return actObj;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x001182D8 File Offset: 0x001166D8
		private IEnumerator LoadCharaFbxDataAsync(Action<GameObject> actObj, int category, int id, string createName, bool copyDynamicBone, byte copyWeights, Transform trfParent, int defaultId, bool AsyncFlags = true, bool worldPositionStays = false)
		{
			Dictionary<int, ListInfoBase> work = null;
			work = base.lstCtrl.GetCategoryInfo((ChaListDefine.CategoryNo)category);
			if (work.Count == 0)
			{
				actObj(null);
				yield break;
			}
			ListInfoBase lib = null;
			if (!work.TryGetValue(id, out lib))
			{
				if (defaultId == -1)
				{
					actObj(null);
					yield break;
				}
				if (id != defaultId)
				{
					work.TryGetValue(defaultId, out lib);
				}
				if (lib == null)
				{
					lib = work.First<KeyValuePair<int, ListInfoBase>>().Value;
				}
			}
			string assetName = string.Empty;
			if (base.sex == 0)
			{
				string info = lib.GetInfo(ChaListDefine.KeyType.MainData02);
				if ("0" != info)
				{
					assetName = info;
				}
			}
			if (string.Empty == assetName)
			{
				assetName = lib.GetInfo(ChaListDefine.KeyType.MainData);
			}
			if (string.Empty == assetName)
			{
				yield break;
			}
			string manifestName = lib.GetInfo(ChaListDefine.KeyType.MainManifest);
			string assetBundleName = lib.GetInfo(ChaListDefine.KeyType.MainAB);
			GameObject newObj = null;
			if (AsyncFlags)
			{
				yield return base.StartCoroutine(base.Load_Coroutine<GameObject>(assetBundleName, assetName, delegate(GameObject x)
				{
					newObj = x;
				}, true, manifestName));
			}
			else
			{
				newObj = CommonLib.LoadAsset<GameObject>(assetBundleName, assetName, true, manifestName);
			}
			Singleton<Character>.Instance.AddLoadAssetBundle(assetBundleName, manifestName);
			if (null == newObj)
			{
				actObj(null);
				yield break;
			}
			newObj.name = createName;
			if (null != trfParent)
			{
				newObj.transform.SetParent(trfParent, worldPositionStays);
			}
			DynamicBoneCollider[] dbc = base.objBodyBone.GetComponentsInChildren<DynamicBoneCollider>(true);
			Dictionary<string, GameObject> dictBone = this.aaWeightsBody.dictBone;
			DynamicBone[] db = newObj.GetComponentsInChildren<DynamicBone>(true);
			GameObject obj = null;
			foreach (DynamicBone dynamicBone in db)
			{
				if (copyDynamicBone)
				{
					if (null != dynamicBone.m_Root && dictBone.TryGetValue(dynamicBone.m_Root.name, out obj))
					{
						dynamicBone.m_Root = obj.transform;
					}
					if (dynamicBone.m_Exclusions != null && dynamicBone.m_Exclusions.Count != 0)
					{
						for (int j = 0; j < dynamicBone.m_Exclusions.Count; j++)
						{
							if (!(null == dynamicBone.m_Exclusions[j]))
							{
								if (dictBone.TryGetValue(dynamicBone.m_Exclusions[j].name, out obj))
								{
									dynamicBone.m_Exclusions[j] = obj.transform;
								}
							}
						}
					}
					if (dynamicBone.m_notRolls != null && dynamicBone.m_notRolls.Count != 0)
					{
						for (int k = 0; k < dynamicBone.m_notRolls.Count; k++)
						{
							if (!(null == dynamicBone.m_notRolls[k]))
							{
								if (dictBone.TryGetValue(dynamicBone.m_notRolls[k].name, out obj))
								{
									dynamicBone.m_notRolls[k] = obj.transform;
								}
							}
						}
					}
				}
				if (dynamicBone.m_Colliders != null)
				{
					dynamicBone.m_Colliders.Clear();
					dynamicBone.m_Colliders.AddRange(dbc);
				}
			}
			Transform trfRootBone = base.cmpBoneBody.targetEtc.trfRoot;
			int copyWeightsEx = (int)copyWeights;
			if (copyWeightsEx == 0)
			{
				copyWeightsEx = lib.GetInfoInt(ChaListDefine.KeyType.Weights);
			}
			if (copyWeightsEx == 1)
			{
				this.aaWeightsBody.AssignedWeightsAndSetBounds(newObj, "cf_J_Root", ChaControlDefine.bounds, trfRootBone);
			}
			else if (copyWeightsEx == 2)
			{
				this.aaWeightsHead.AssignedWeightsAndSetBounds(newObj, "cf_J_FaceRoot", ChaControlDefine.bounds, trfRootBone);
			}
			ListInfoComponent libComponent = newObj.AddComponent<ListInfoComponent>();
			libComponent.data = lib;
			actObj(newObj);
			yield break;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00118340 File Offset: 0x00116740
		public void LoadHitObject()
		{
			this.ReleaseHitObject();
			string assetBundleName = "chara/oo_base.unity3d";
			string assetName = "p_cf_body_00_hit";
			base.objHitBody = CommonLib.LoadAsset<GameObject>(assetBundleName, assetName, true, "abdata");
			Singleton<Character>.Instance.AddLoadAssetBundle(assetBundleName, "abdata");
			if (null != base.objHitBody)
			{
				base.objHitBody.transform.SetParent(base.objTop.transform, false);
				this.aaWeightsBody.AssignedWeights(base.objHitBody, "cf_J_Root", null);
				SkinnedCollisionHelper[] componentsInChildren = base.objHitBody.GetComponentsInChildren<SkinnedCollisionHelper>(true);
				foreach (SkinnedCollisionHelper skinnedCollisionHelper in componentsInChildren)
				{
					skinnedCollisionHelper.Init();
				}
			}
			if (base.sex != 0 && null != base.objHead)
			{
				ListInfoComponent component = base.objHead.GetComponent<ListInfoComponent>();
				string manifestName = component.data.dictInfo[14];
				assetBundleName = component.data.dictInfo[15];
				assetName = component.data.dictInfo[16] + "_hit";
				base.objHitHead = CommonLib.LoadAsset<GameObject>(assetBundleName, assetName, true, manifestName);
				Singleton<Character>.Instance.AddLoadAssetBundle(assetBundleName, manifestName);
				if (null != base.objHitHead)
				{
					base.objHitHead.transform.SetParent(base.objTop.transform, false);
					this.aaWeightsHead.AssignedWeights(base.objHitHead, "cf_J_FaceRoot", null);
					SkinnedCollisionHelper[] componentsInChildren2 = base.objHitHead.GetComponentsInChildren<SkinnedCollisionHelper>(true);
					foreach (SkinnedCollisionHelper skinnedCollisionHelper2 in componentsInChildren2)
					{
						skinnedCollisionHelper2.Init();
					}
				}
			}
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x00118508 File Offset: 0x00116908
		public void ReleaseHitObject()
		{
			if (null != base.objHitBody)
			{
				SkinnedCollisionHelper[] componentsInChildren = base.objHitBody.GetComponentsInChildren<SkinnedCollisionHelper>(true);
				foreach (SkinnedCollisionHelper skinnedCollisionHelper in componentsInChildren)
				{
					skinnedCollisionHelper.Release();
				}
				base.SafeDestroy(base.objHitBody);
				base.objHitBody = null;
			}
			if (null != base.objHitHead)
			{
				SkinnedCollisionHelper[] componentsInChildren2 = base.objHitHead.GetComponentsInChildren<SkinnedCollisionHelper>(true);
				foreach (SkinnedCollisionHelper skinnedCollisionHelper2 in componentsInChildren2)
				{
					skinnedCollisionHelper2.Release();
				}
				base.SafeDestroy(base.objHitHead);
				base.objHitHead = null;
			}
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x001185C8 File Offset: 0x001169C8
		public bool LoadAlphaMaskTexture(string assetBundleName, string assetName, byte type)
		{
			if ("0" == assetBundleName || "0" == assetName)
			{
				return false;
			}
			Texture texture = CommonLib.LoadAsset<Texture>(assetBundleName, assetName, false, string.Empty);
			if (null == texture)
			{
				return false;
			}
			Singleton<Character>.Instance.AddLoadAssetBundle(assetBundleName, string.Empty);
			if (type == 0)
			{
				this.texBodyAlphaMask = texture;
			}
			else if (type == 1)
			{
				this.texBraAlphaMask = texture;
			}
			else if (type == 2)
			{
				this.texInnerTBAlphaMask = texture;
			}
			else if (type == 3)
			{
				this.texInnerBAlphaMask = texture;
			}
			else if (type == 4)
			{
				this.texPanstAlphaMask = texture;
			}
			else if (type == 5)
			{
				this.texBodyBAlphaMask = texture;
			}
			return true;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x00118690 File Offset: 0x00116A90
		public bool ReleaseAlphaMaskTexture(byte type)
		{
			if (type == 0)
			{
				if (null != base.customMatBody)
				{
					base.customMatBody.SetTexture(ChaShader.AlphaMask, null);
				}
				this.texBodyAlphaMask = null;
			}
			else if (type == 1)
			{
				if (base.rendBra != null && null != base.rendBra[0])
				{
					base.rendBra[0].material.SetTexture(ChaShader.AlphaMask, null);
				}
				this.texBraAlphaMask = null;
			}
			else if (type == 2)
			{
				if (null != base.rendInnerTB)
				{
					base.rendInnerTB.material.SetTexture(ChaShader.AlphaMask2, null);
				}
				this.texInnerTBAlphaMask = null;
			}
			else if (type == 3)
			{
				if (null != base.rendInnerB)
				{
					base.rendInnerB.material.SetTexture(ChaShader.AlphaMask2, null);
				}
				this.texInnerBAlphaMask = null;
			}
			else if (type == 4)
			{
				if (null != base.rendPanst)
				{
					base.rendPanst.material.SetTexture(ChaShader.AlphaMask2, null);
				}
				this.texPanstAlphaMask = null;
			}
			else if (type == 5)
			{
				if (null != base.customMatBody)
				{
					base.customMatBody.SetTexture(ChaShader.AlphaMask2, null);
				}
				this.texBodyBAlphaMask = null;
			}
			return true;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x001187FC File Offset: 0x00116BFC
		public bool InitializeExpression(int sex, bool _enable = true)
		{
			string text = "list/expression.unity3d";
			string text2 = (sex != 0) ? "cf_expression" : "cm_expression";
			if (!global::AssetBundleCheck.IsFile(text, text2))
			{
				string text3 = "読み込みエラー\r\nassetBundleName：" + text + "\tassetName：" + text2;
				return false;
			}
			base.expression = base.objRoot.AddComponent<Expression>();
			base.expression.LoadSetting(text, text2);
			int[] array = new int[]
			{
				0,
				0,
				4,
				0,
				0,
				0,
				0,
				1,
				1,
				5,
				1,
				1,
				1,
				1,
				6,
				6,
				6,
				2,
				2,
				6,
				7,
				7,
				7,
				3,
				3,
				7
			};
			for (int i = 0; i < base.expression.info.Length; i++)
			{
				base.expression.info[i].categoryNo = array[i];
			}
			base.expression.SetCharaTransform(base.objRoot.transform);
			base.expression.Initialize();
			base.expression.enable = _enable;
			return true;
		}

		// Token: 0x04002DC6 RID: 11718
		private ChaControl.MannequinBackInfo mannequinBackInfo = new ChaControl.MannequinBackInfo();

		// Token: 0x04002DD3 RID: 11731
		private bool confSon = true;

		// Token: 0x04002DD4 RID: 11732
		private bool confBody = true;

		// Token: 0x04002DD5 RID: 11733
		private bool drawSimple;

		// Token: 0x04002DD6 RID: 11734
		private List<bool> lstActive = new List<bool>();

		// Token: 0x04002DEE RID: 11758
		private AssignedAnotherWeights aaWeightsHead;

		// Token: 0x04002DEF RID: 11759
		private AssignedAnotherWeights aaWeightsBody;

		// Token: 0x020007B3 RID: 1971
		public class MannequinBackInfo
		{
			// Token: 0x06003047 RID: 12359 RVA: 0x001188F0 File Offset: 0x00116CF0
			public void Backup(ChaControl chaCtrl)
			{
				this.custom = chaCtrl.chaFile.GetCustomBytes();
				this.eyesPtn = chaCtrl.fileStatus.eyesPtn;
				this.eyesOpen = chaCtrl.fileStatus.eyesOpenMax;
				this.mouthPtn = chaCtrl.fileStatus.mouthPtn;
				this.mouthOpen = chaCtrl.fileStatus.mouthOpenMax;
				this.mouthFixed = chaCtrl.fileStatus.mouthFixed;
				this.neckLook = chaCtrl.fileStatus.neckLookPtn;
			}

			// Token: 0x06003048 RID: 12360 RVA: 0x00118974 File Offset: 0x00116D74
			public void Restore(ChaControl chaCtrl)
			{
				chaCtrl.chaFile.SetCustomBytes(this.custom, ChaFileDefine.ChaFileCustomVersion);
				chaCtrl.Reload(true, false, false, false, true);
				chaCtrl.ChangeEyesPtn(this.eyesPtn, false);
				chaCtrl.ChangeEyesOpenMax(this.eyesOpen);
				chaCtrl.ChangeMouthPtn(this.mouthPtn, false);
				chaCtrl.fileStatus.mouthFixed = this.mouthFixed;
				chaCtrl.ChangeMouthOpenMax(this.mouthOpen);
				chaCtrl.ChangeLookNeckPtn(this.neckLook, 1f);
				chaCtrl.neckLookCtrl.ForceLateUpdate();
				chaCtrl.eyeLookCtrl.ForceLateUpdate();
				chaCtrl.neckLookCtrl.neckLookScript.skipCalc = false;
				chaCtrl.resetDynamicBoneAll = true;
				chaCtrl.LateUpdateForce();
			}

			// Token: 0x04002DF3 RID: 11763
			public byte[] custom;

			// Token: 0x04002DF4 RID: 11764
			public int eyesPtn;

			// Token: 0x04002DF5 RID: 11765
			public float eyesOpen;

			// Token: 0x04002DF6 RID: 11766
			public int mouthPtn;

			// Token: 0x04002DF7 RID: 11767
			public float mouthOpen;

			// Token: 0x04002DF8 RID: 11768
			public bool mouthFixed;

			// Token: 0x04002DF9 RID: 11769
			public int neckLook;

			// Token: 0x04002DFA RID: 11770
			public byte[] eyesInfo;

			// Token: 0x04002DFB RID: 11771
			public bool mannequin;
		}

		// Token: 0x020007B9 RID: 1977
		public enum BodyTexKind
		{
			// Token: 0x04002E0A RID: 11786
			inpBase,
			// Token: 0x04002E0B RID: 11787
			inpPaint01,
			// Token: 0x04002E0C RID: 11788
			inpPaint02,
			// Token: 0x04002E0D RID: 11789
			inpSunburn
		}

		// Token: 0x020007BA RID: 1978
		public enum FaceTexKind
		{
			// Token: 0x04002E0F RID: 11791
			inpBase,
			// Token: 0x04002E10 RID: 11792
			inpEyeshadow,
			// Token: 0x04002E11 RID: 11793
			inpPaint01,
			// Token: 0x04002E12 RID: 11794
			inpPaint02,
			// Token: 0x04002E13 RID: 11795
			inpCheek,
			// Token: 0x04002E14 RID: 11796
			inpLip,
			// Token: 0x04002E15 RID: 11797
			inpMole
		}
	}
}
