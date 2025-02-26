using System;
using System.Collections.Generic;
using AIChara;
using AIProject;
using Manager;
using UnityEngine;

// Token: 0x02000AAB RID: 2731
public class HMotionEyeNeckLesPlayer : MonoBehaviour
{
	// Token: 0x0600503F RID: 20543 RVA: 0x001F1584 File Offset: 0x001EF984
	public bool Init(ChaControl _female, int id)
	{
		this.NowEndADV = false;
		this.Release();
		this.ID = id;
		this.chaFemale = _female;
		this.NeckTrs = this.chaFemale.neckLookCtrl.neckLookScript.aBones[0].neckBone;
		this.HeadTrs = this.chaFemale.neckLookCtrl.neckLookScript.aBones[1].neckBone;
		this.EyeTrs = new Transform[]
		{
			this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[0].eyeTransform,
			this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[1].eyeTransform
		};
		this.BackUpNeck = this.NeckTrs.localRotation;
		this.BackUpHead = this.HeadTrs.localRotation;
		this.BackUpEye = new Quaternion[]
		{
			this.EyeTrs[0].localRotation,
			this.EyeTrs[1].localRotation
		};
		this.objGenitalSelf = null;
		if (this.chaFemale && this.chaFemale.objBodyBone)
		{
			this.LoopParent = this.chaFemale.objBodyBone.transform;
			if (this.strFemaleGenital != string.Empty)
			{
				this.objGenitalSelf = this.GetObjectName(this.LoopParent, this.strFemaleGenital);
			}
		}
		if (this.hscene == null)
		{
			this.hscene = Singleton<HSceneManager>.Instance.HSceneSet.GetComponentInChildren<HScene>();
		}
		return true;
	}

	// Token: 0x06005040 RID: 20544 RVA: 0x001F1730 File Offset: 0x001EFB30
	public void Release()
	{
		this.lstEyeNeck.Clear();
	}

	// Token: 0x06005041 RID: 20545 RVA: 0x001F1740 File Offset: 0x001EFB40
	private GameObject GetObjectName(Transform top, string name)
	{
		this.getChild = top.GetComponentsInChildren<Transform>();
		for (int i = 0; i < this.getChild.Length; i++)
		{
			if (!(this.getChild[i].name != name))
			{
				return this.getChild[i].gameObject;
			}
		}
		return null;
	}

	// Token: 0x06005042 RID: 20546 RVA: 0x001F17A0 File Offset: 0x001EFBA0
	public bool Load(string _assetpath, string _file)
	{
		this.lstEyeNeck.Clear();
		this.ChangeTimeEye = 0f;
		this.nowleapSpeed[1] = 0f;
		this.BackUpEye[0] = this.EyeTrs[0].localRotation;
		this.BackUpEye[1] = this.EyeTrs[1].localRotation;
		this.ChangeTimeNeck = 0f;
		this.nowleapSpeed[0] = 0f;
		this.BackUpNeck = this.NeckTrs.localRotation;
		this.BackUpHead = this.HeadTrs.localRotation;
		if (_file == string.Empty)
		{
			return false;
		}
		List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(_assetpath, false);
		assetBundleNameListFromPath.Sort();
		this.abName = string.Empty;
		this.assetName = string.Empty;
		this.excelData = null;
		this.info.Init();
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
		{
			this.abName = assetBundleNameListFromPath[i];
			this.assetName = _file;
			if (GlobalMethod.AssetFileExist(this.abName, this.assetName, string.Empty))
			{
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.abName, this.assetName, false, string.Empty);
				Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(this.abName);
				if (!(this.excelData == null))
				{
					int j = 3;
					while (j < this.excelData.MaxCell)
					{
						this.row = this.excelData.list[j++].list;
						int num = 0;
						this.info = default(HMotionEyeNeckLesPlayer.EyeNeck);
						this.info.Init();
						if (this.row.Count != 0)
						{
							string element = this.row.GetElement(num++);
							if (!element.IsNullOrEmpty())
							{
								this.info.anim = element;
								this.info.isConfigDisregardNeck = (this.row.GetElement(num++) == "1");
								this.info.isConfigDisregardEye = (this.row.GetElement(num++) == "1");
								float openEye = 0f;
								if (!float.TryParse(this.row.GetElement(num++), out openEye))
								{
									openEye = 0f;
								}
								this.info.openEye = openEye;
								float openMouth = 0f;
								if (!float.TryParse(this.row.GetElement(num++), out openMouth))
								{
									openMouth = 0f;
								}
								this.info.openMouth = openMouth;
								int eyeBlow = 0;
								if (!int.TryParse(this.row.GetElement(num++), out eyeBlow))
								{
									eyeBlow = 0;
								}
								this.info.eyeBlow = eyeBlow;
								int eye = 0;
								if (!int.TryParse(this.row.GetElement(num++), out eye))
								{
									eye = 0;
								}
								this.info.eye = eye;
								int mouth = 0;
								if (!int.TryParse(this.row.GetElement(num++), out mouth))
								{
									mouth = 0;
								}
								this.info.mouth = mouth;
								float tear = 0f;
								if (!float.TryParse(this.row.GetElement(num++), out tear))
								{
									tear = 0f;
								}
								this.info.tear = tear;
								float cheek = 0f;
								if (!float.TryParse(this.row.GetElement(num++), out cheek))
								{
									cheek = 0f;
								}
								this.info.cheek = cheek;
								this.info.blink = (this.row.GetElement(num++) == "1");
								this.info.faceinfo.Neckbehaviour = int.Parse(this.row.GetElement(num++));
								this.info.faceinfo.Eyebehaviour = int.Parse(this.row.GetElement(num++));
								this.info.faceinfo.targetNeck = int.Parse(this.row.GetElement(num++));
								if (!float.TryParse(this.row.GetElement(num++), out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.z))
								{
									zero.z = 0f;
								}
								this.info.faceinfo.NeckRot[0] = zero;
								if (!float.TryParse(this.row.GetElement(num++), out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.z))
								{
									zero.z = 0f;
								}
								this.info.faceinfo.NeckRot[1] = zero;
								if (!float.TryParse(this.row.GetElement(num++), out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.z))
								{
									zero.z = 0f;
								}
								this.info.faceinfo.HeadRot[0] = zero;
								if (!float.TryParse(this.row.GetElement(num++), out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.z))
								{
									zero.z = 0f;
								}
								this.info.faceinfo.HeadRot[1] = zero;
								this.info.faceinfo.targetEye = int.Parse(this.row.GetElement(num++));
								if (!float.TryParse(this.row.GetElement(num++), out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.z))
								{
									zero.z = 0f;
								}
								this.info.faceinfo.EyeRot[0] = zero;
								if (!float.TryParse(this.row.GetElement(num++), out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(this.row.GetElement(num++), out zero.z))
								{
									zero.z = 0f;
								}
								this.info.faceinfo.EyeRot[1] = zero;
								this.lstEyeNeck.Add(this.info);
							}
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005043 RID: 20547 RVA: 0x001F2060 File Offset: 0x001F0460
	public bool SetPartner(GameObject _objFemale1Bone)
	{
		this.objFemale1Head = null;
		this.objFemale1Genital = null;
		if (_objFemale1Bone)
		{
			this.LoopParent = _objFemale1Bone.transform;
			if (this.strFemaleHead != string.Empty)
			{
				this.objFemale1Head = this.GetObjectName(this.LoopParent, this.strFemaleHead);
			}
			if (this.strFemaleGenital != string.Empty)
			{
				this.objFemale1Genital = this.GetObjectName(this.LoopParent, this.strFemaleGenital);
			}
		}
		return true;
	}

	// Token: 0x06005044 RID: 20548 RVA: 0x001F20F0 File Offset: 0x001F04F0
	public bool Proc(AnimatorStateInfo _ai, int _main)
	{
		this.ai = _ai;
		for (int i = 0; i < this.lstEyeNeck.Count; i++)
		{
			this.en = this.lstEyeNeck[i];
			if (_ai.IsName(this.en.anim))
			{
				this.enFace = this.en.faceinfo;
				this.eyeNeck = this.en;
			}
		}
		this.chaFemale.ChangeEyesOpenMax(this.eyeNeck.openEye);
		FBSCtrlMouth mouthCtrl = this.chaFemale.mouthCtrl;
		if (mouthCtrl != null)
		{
			mouthCtrl.OpenMin = this.en.openMouth;
		}
		this.chaFemale.ChangeEyebrowPtn(this.en.eyeBlow, true);
		this.chaFemale.ChangeEyesPtn(this.en.eye, true);
		this.chaFemale.ChangeMouthPtn(this.eyeNeck.mouth, true);
		if (this.eyeNeck.mouth == 10 || this.eyeNeck.mouth == 13)
		{
			this.chaFemale.DisableShapeMouth(true);
		}
		else
		{
			this.chaFemale.DisableShapeMouth(false);
		}
		this.chaFemale.ChangeTearsRate(this.eyeNeck.tear);
		this.chaFemale.ChangeHohoAkaRate(this.eyeNeck.cheek);
		this.chaFemale.ChangeEyesBlinkFlag(this.eyeNeck.blink);
		this.SetNeckTarget((!this.en.isConfigDisregardNeck) ? this.enFace.targetNeck : this.enFace.targetNeck);
		this.SetEyesTarget((!this.en.isConfigDisregardEye) ? this.enFace.targetEye : this.enFace.targetEye);
		this.SetBehaviourEyes((!this.en.isConfigDisregardEye) ? this.enFace.Eyebehaviour : this.enFace.Eyebehaviour);
		this.SetBehaviourNeck((!this.en.isConfigDisregardNeck) ? this.enFace.Neckbehaviour : this.enFace.Neckbehaviour);
		if (this.OldAnimName != this.en.anim)
		{
			this.ChangeTimeEye = 0f;
			this.nowleapSpeed[1] = 0f;
			this.BackUpEye[0] = this.EyeTrs[0].localRotation;
			this.BackUpEye[1] = this.EyeTrs[1].localRotation;
			this.ChangeTimeNeck = 0f;
			this.nowleapSpeed[0] = 0f;
			this.BackUpNeck = this.NeckTrs.localRotation;
			this.BackUpHead = this.HeadTrs.localRotation;
			this.OldAnimName = this.en.anim;
		}
		return true;
	}

	// Token: 0x06005045 RID: 20549 RVA: 0x001F23EF File Offset: 0x001F07EF
	private void LateUpdate()
	{
		this.EyeNeckCalc();
	}

	// Token: 0x06005046 RID: 20550 RVA: 0x001F23F8 File Offset: 0x001F07F8
	public void EyeNeckCalc()
	{
		if (this.chaFemale == null || this.NowEndADV)
		{
			return;
		}
		for (int i = 0; i < this.lstEyeNeck.Count; i++)
		{
			this.en = this.lstEyeNeck[i];
			if (this.ai.IsName(this.en.anim))
			{
				this.enFace = this.en.faceinfo;
				if (this.enFace.targetNeck == 3)
				{
					if (Singleton<HSceneFlagCtrl>.Instance.motions[this.ID] < 0.5f)
					{
						if (this.nYuragiType != 0)
						{
							this.nYuragiType = 0;
							this.ChangeTimeNeck = 0f;
							this.nowleapSpeed[0] = 0f;
						}
					}
					else if (this.nYuragiType != 1)
					{
						this.nYuragiType = 1;
						this.ChangeTimeNeck = 0f;
						this.nowleapSpeed[0] = 0f;
					}
					this.NeckCalc(this.enFace.NeckRot[this.nYuragiType], this.enFace.HeadRot[this.nYuragiType]);
				}
				if (this.enFace.targetEye == 3)
				{
					if (Singleton<HSceneFlagCtrl>.Instance.motions[this.ID] < 0.5f)
					{
						if (this.nYuragiType != 0)
						{
							this.nYuragiType = 0;
							this.ChangeTimeEye = 0f;
							this.nowleapSpeed[1] = 0f;
						}
					}
					else if (this.nYuragiType != 1)
					{
						this.nYuragiType = 1;
						this.ChangeTimeEye = 0f;
						this.nowleapSpeed[1] = 0f;
					}
					this.EyeCalc(this.enFace.EyeRot[this.nYuragiType]);
				}
				return;
			}
		}
	}

	// Token: 0x06005047 RID: 20551 RVA: 0x001F25EC File Offset: 0x001F09EC
	private bool SetEyesTarget(int _tag)
	{
		switch (_tag)
		{
		case 1:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objFemale1Head) ? null : this.objFemale1Head.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 2:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objFemale1Genital) ? null : this.objFemale1Genital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 3:
			break;
		case 4:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objGenitalSelf) ? null : this.objGenitalSelf.transform, 0.5f, 0f, 1f, 2f);
			break;
		default:
			this.chaFemale.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			break;
		}
		if (this.EyeType != _tag)
		{
			this.ChangeTimeEye = 0f;
			this.nowleapSpeed[1] = 0f;
			this.BackUpEye[0] = this.EyeTrs[0].localRotation;
			this.BackUpEye[1] = this.EyeTrs[1].localRotation;
		}
		this.EyeType = _tag;
		return true;
	}

	// Token: 0x06005048 RID: 20552 RVA: 0x001F277C File Offset: 0x001F0B7C
	private bool SetNeckTarget(int _tag)
	{
		switch (_tag)
		{
		case 1:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objFemale1Head) ? null : this.objFemale1Head.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 2:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objFemale1Genital) ? null : this.objFemale1Genital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 3:
			break;
		case 4:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objGenitalSelf) ? null : this.objGenitalSelf.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		default:
			this.chaFemale.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			break;
		}
		if (this.NeckType != _tag)
		{
			this.ChangeTimeNeck = 0f;
			this.nowleapSpeed[0] = 0f;
			this.BackUpNeck = this.NeckTrs.localRotation;
			this.BackUpHead = this.HeadTrs.localRotation;
		}
		this.NeckType = _tag;
		return true;
	}

	// Token: 0x06005049 RID: 20553 RVA: 0x001F28F4 File Offset: 0x001F0CF4
	private bool SetBehaviourEyes(int _behaviour)
	{
		switch (_behaviour)
		{
		case 1:
			this.chaFemale.ChangeLookEyesPtn(1);
			break;
		case 2:
			this.chaFemale.ChangeLookEyesPtn(2);
			break;
		case 3:
			this.chaFemale.ChangeLookEyesPtn(1);
			break;
		default:
			this.chaFemale.ChangeLookEyesPtn(0);
			break;
		}
		if (_behaviour == 3)
		{
			this.chaFemale.eyeLookCtrl.enabled = false;
		}
		else
		{
			this.chaFemale.eyeLookCtrl.enabled = true;
		}
		return true;
	}

	// Token: 0x0600504A RID: 20554 RVA: 0x001F2990 File Offset: 0x001F0D90
	private bool SetBehaviourNeck(int _behaviour)
	{
		if (!this.chaFemale.neckLookCtrl.enabled && _behaviour != 3)
		{
			this.chaFemale.neckLookCtrl.neckLookScript.UpdateCall(0);
		}
		switch (_behaviour)
		{
		case 1:
			this.chaFemale.ChangeLookNeckPtn(1, 1f);
			break;
		case 2:
			this.chaFemale.ChangeLookNeckPtn(2, 1f);
			break;
		case 3:
			this.chaFemale.ChangeLookNeckPtn(1, 1f);
			break;
		default:
			this.chaFemale.ChangeLookNeckPtn(3, 1f);
			break;
		}
		if (_behaviour == 3)
		{
			this.chaFemale.neckLookCtrl.enabled = false;
		}
		else
		{
			this.chaFemale.neckLookCtrl.enabled = true;
		}
		return true;
	}

	// Token: 0x0600504B RID: 20555 RVA: 0x001F2A70 File Offset: 0x001F0E70
	private void NeckCalc(Vector3 targetNeckRot, Vector3 targetHeadRot)
	{
		float deltaTime = Time.deltaTime;
		this.ChangeTimeNeck = Mathf.Clamp(this.ChangeTimeNeck + deltaTime, 0f, this.chaFemale.neckLookCtrl.neckLookScript.changeTypeLeapTime);
		float num = Mathf.InverseLerp(0f, this.chaFemale.neckLookCtrl.neckLookScript.changeTypeLeapTime, this.ChangeTimeNeck);
		if (this.chaFemale.neckLookCtrl.neckLookScript.changeTypeLerpCurve != null)
		{
			num = this.chaFemale.neckLookCtrl.neckLookScript.changeTypeLerpCurve.Evaluate(num);
		}
		this.nowleapSpeed[0] = Mathf.Clamp01(this.chaFemale.neckLookCtrl.neckLookScript.neckTypeStates[3].leapSpeed * deltaTime);
		Quaternion b = Quaternion.Slerp(this.BackUpNeck, Quaternion.Euler(targetNeckRot), this.nowleapSpeed[0]);
		Quaternion quaternion = Quaternion.Slerp(this.NeckTrs.localRotation, b, this.chaFemale.neckLookCtrl.neckLookScript.calcLerp);
		quaternion = Quaternion.Slerp(this.BackUpNeck, quaternion, num);
		Quaternion quaternion2 = quaternion;
		this.NeckTrs.localRotation = quaternion2;
		this.BackUpNeck = quaternion2;
		b = Quaternion.Slerp(this.BackUpHead, Quaternion.Euler(targetHeadRot), this.nowleapSpeed[0]);
		quaternion = Quaternion.Slerp(this.HeadTrs.localRotation, b, this.chaFemale.neckLookCtrl.neckLookScript.calcLerp);
		quaternion = Quaternion.Slerp(this.BackUpHead, quaternion, num);
		quaternion2 = quaternion;
		this.HeadTrs.localRotation = quaternion2;
		this.BackUpHead = quaternion2;
		this.chaFemale.neckLookCtrl.neckLookScript.aBones[0].fixAngle = this.NeckTrs.localRotation;
		this.chaFemale.neckLookCtrl.neckLookScript.aBones[1].fixAngle = this.HeadTrs.localRotation;
	}

	// Token: 0x0600504C RID: 20556 RVA: 0x001F2C54 File Offset: 0x001F1054
	private void EyeCalc(Vector3 targetEyeRot)
	{
		float deltaTime = Time.deltaTime;
		this.ChangeTimeEye = Mathf.Clamp(this.ChangeTimeEye + deltaTime, 0f, 1f);
		float t = Mathf.InverseLerp(0f, 1f, this.ChangeTimeEye);
		this.nowleapSpeed[1] = Mathf.Clamp01(this.chaFemale.eyeLookCtrl.eyeLookScript.eyeTypeStates[0].leapSpeed * deltaTime);
		Quaternion b = Quaternion.Slerp(this.BackUpEye[0], Quaternion.Euler(targetEyeRot), this.nowleapSpeed[1]);
		Quaternion quaternion = Quaternion.Slerp(this.EyeTrs[0].localRotation, b, 1f);
		quaternion = Quaternion.Slerp(this.BackUpEye[0], quaternion, t);
		Quaternion[] backUpEye = this.BackUpEye;
		int num = 0;
		Quaternion quaternion2 = quaternion;
		this.EyeTrs[0].localRotation = quaternion2;
		backUpEye[num] = quaternion2;
		b = Quaternion.Slerp(this.BackUpEye[1], Quaternion.Euler(targetEyeRot), this.nowleapSpeed[1]);
		quaternion = Quaternion.Slerp(this.EyeTrs[1].localRotation, b, 1f);
		quaternion = Quaternion.Slerp(this.BackUpEye[1], quaternion, t);
		Quaternion[] backUpEye2 = this.BackUpEye;
		int num2 = 1;
		quaternion2 = quaternion;
		this.EyeTrs[1].localRotation = quaternion2;
		backUpEye2[num2] = quaternion2;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[0].angleH = this.EyeTrs[1].localRotation.eulerAngles.y;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[0].angleV = this.EyeTrs[1].localRotation.eulerAngles.x;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[1].angleH = this.EyeTrs[1].localRotation.eulerAngles.y;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[1].angleV = this.EyeTrs[1].localRotation.eulerAngles.x;
	}

	// Token: 0x04004970 RID: 18800
	[Label("相手女顔オブジェクト名")]
	public string strFemaleHead = string.Empty;

	// Token: 0x04004971 RID: 18801
	[Label("相手女性器オブジェクト名")]
	public string strFemaleGenital = string.Empty;

	// Token: 0x04004972 RID: 18802
	[SerializeField]
	private List<HMotionEyeNeckLesPlayer.EyeNeck> lstEyeNeck = new List<HMotionEyeNeckLesPlayer.EyeNeck>();

	// Token: 0x04004973 RID: 18803
	[DisabledGroup("女クラス")]
	[SerializeField]
	private ChaControl chaFemale;

	// Token: 0x04004974 RID: 18804
	[DisabledGroup("自分性器オブジェクト")]
	[SerializeField]
	private GameObject objGenitalSelf;

	// Token: 0x04004975 RID: 18805
	[DisabledGroup("女相手顔オブジェクト")]
	[SerializeField]
	private GameObject objFemale1Head;

	// Token: 0x04004976 RID: 18806
	[DisabledGroup("女相手性器オブジェクト")]
	[SerializeField]
	private GameObject objFemale1Genital;

	// Token: 0x04004977 RID: 18807
	private Transform LoopParent;

	// Token: 0x04004978 RID: 18808
	private HMotionEyeNeckLesPlayer.EyeNeck en;

	// Token: 0x04004979 RID: 18809
	private HMotionEyeNeckLesPlayer.EyeNeck eyeNeck;

	// Token: 0x0400497A RID: 18810
	private HMotionEyeNeckLesPlayer.EyeNeckFace enFace;

	// Token: 0x0400497B RID: 18811
	private int ID;

	// Token: 0x0400497C RID: 18812
	private AnimatorStateInfo ai;

	// Token: 0x0400497D RID: 18813
	private Transform NeckTrs;

	// Token: 0x0400497E RID: 18814
	private Transform HeadTrs;

	// Token: 0x0400497F RID: 18815
	private Transform[] EyeTrs;

	// Token: 0x04004980 RID: 18816
	private Quaternion BackUpNeck;

	// Token: 0x04004981 RID: 18817
	private Quaternion BackUpHead;

	// Token: 0x04004982 RID: 18818
	private Quaternion[] BackUpEye;

	// Token: 0x04004983 RID: 18819
	private float ChangeTimeNeck;

	// Token: 0x04004984 RID: 18820
	private int NeckType;

	// Token: 0x04004985 RID: 18821
	private float ChangeTimeEye;

	// Token: 0x04004986 RID: 18822
	private int EyeType;

	// Token: 0x04004987 RID: 18823
	private bool[] bFaceInfo = new bool[2];

	// Token: 0x04004988 RID: 18824
	private int nYuragiType;

	// Token: 0x04004989 RID: 18825
	private string OldAnimName;

	// Token: 0x0400498A RID: 18826
	private HScene hscene;

	// Token: 0x0400498B RID: 18827
	private ExcelData excelData;

	// Token: 0x0400498C RID: 18828
	private HMotionEyeNeckLesPlayer.EyeNeck info;

	// Token: 0x0400498D RID: 18829
	private List<string> row = new List<string>();

	// Token: 0x0400498E RID: 18830
	private string abName = string.Empty;

	// Token: 0x0400498F RID: 18831
	private string assetName = string.Empty;

	// Token: 0x04004990 RID: 18832
	public bool NowEndADV;

	// Token: 0x04004991 RID: 18833
	private Transform[] getChild;

	// Token: 0x04004992 RID: 18834
	private float[] nowleapSpeed = new float[2];

	// Token: 0x02000AAC RID: 2732
	[Serializable]
	public struct EyeNeck
	{
		// Token: 0x0600504D RID: 20557 RVA: 0x001F2EA9 File Offset: 0x001F12A9
		public void Init()
		{
			this.anim = string.Empty;
			this.isConfigDisregardNeck = false;
			this.isConfigDisregardEye = false;
			this.faceinfo.Init();
		}

		// Token: 0x04004993 RID: 18835
		[Label("アニメーション名")]
		public string anim;

		// Token: 0x04004994 RID: 18836
		[Label("首コンフィグ無視")]
		public bool isConfigDisregardNeck;

		// Token: 0x04004995 RID: 18837
		[Label("目コンフィグ無視")]
		public bool isConfigDisregardEye;

		// Token: 0x04004996 RID: 18838
		[Label("目の開き")]
		public float openEye;

		// Token: 0x04004997 RID: 18839
		[Label("口の開き")]
		public float openMouth;

		// Token: 0x04004998 RID: 18840
		[Label("眉の形")]
		public int eyeBlow;

		// Token: 0x04004999 RID: 18841
		[Label("目の形")]
		public int eye;

		// Token: 0x0400499A RID: 18842
		[Label("口の形")]
		public int mouth;

		// Token: 0x0400499B RID: 18843
		[RangeLabel("涙", 0f, 1f)]
		public float tear;

		// Token: 0x0400499C RID: 18844
		[RangeLabel("頬赤", 0f, 1f)]
		public float cheek;

		// Token: 0x0400499D RID: 18845
		[Label("瞬き")]
		public bool blink;

		// Token: 0x0400499E RID: 18846
		public HMotionEyeNeckLesPlayer.EyeNeckFace faceinfo;
	}

	// Token: 0x02000AAD RID: 2733
	[Serializable]
	public struct EyeNeckFace
	{
		// Token: 0x0600504E RID: 20558 RVA: 0x001F2ED0 File Offset: 0x001F12D0
		public void Init()
		{
			this.Neckbehaviour = 0;
			this.Eyebehaviour = 0;
			this.targetEye = 0;
			this.EyeRot = new Vector3[]
			{
				Vector3.zero,
				Vector3.zero
			};
			this.targetNeck = 0;
			this.NeckRot = new Vector3[]
			{
				Vector3.zero,
				Vector3.zero
			};
			this.HeadRot = new Vector3[]
			{
				Vector3.zero,
				Vector3.zero
			};
		}

		// Token: 0x0400499F RID: 18847
		[Label("首挙動")]
		public int Neckbehaviour;

		// Token: 0x040049A0 RID: 18848
		[Label("目挙動")]
		public int Eyebehaviour;

		// Token: 0x040049A1 RID: 18849
		[Label("首ターゲット")]
		public int targetNeck;

		// Token: 0x040049A2 RID: 18850
		[Label("首角度")]
		public Vector3[] NeckRot;

		// Token: 0x040049A3 RID: 18851
		[Label("頭角度")]
		public Vector3[] HeadRot;

		// Token: 0x040049A4 RID: 18852
		[Label("目ターゲット")]
		public int targetEye;

		// Token: 0x040049A5 RID: 18853
		[Label("視線角度")]
		public Vector3[] EyeRot;
	}
}
