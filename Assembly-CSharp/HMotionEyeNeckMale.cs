using System;
using System.Collections.Generic;
using AIChara;
using AIProject;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

// Token: 0x02000AAE RID: 2734
public class HMotionEyeNeckMale : MonoBehaviour
{
	// Token: 0x06005050 RID: 20560 RVA: 0x001F2FFC File Offset: 0x001F13FC
	public bool Init(ChaControl _male, int id)
	{
		this.NowEndADV = false;
		this.Release();
		this.chaMale = _male;
		this.NeckTrs = this.chaMale.neckLookCtrl.neckLookScript.aBones[0].neckBone;
		this.HeadTrs = this.chaMale.neckLookCtrl.neckLookScript.aBones[1].neckBone;
		this.EyeTrs = new Transform[]
		{
			this.chaMale.eyeLookCtrl.eyeLookScript.eyeObjs[0].eyeTransform,
			this.chaMale.eyeLookCtrl.eyeLookScript.eyeObjs[1].eyeTransform
		};
		this.BackUpNeck = this.NeckTrs.localRotation;
		this.BackUpHead = this.HeadTrs.localRotation;
		this.BackUpEye = new Quaternion[]
		{
			this.EyeTrs[0].localRotation,
			this.EyeTrs[1].localRotation
		};
		this.objGenitalSelf = null;
		if (this.chaMale && this.chaMale.objBodyBone)
		{
			this.LoopParent = this.chaMale.objBodyBone.transform;
			if (this.strFemaleGenital != string.Empty)
			{
				this.objGenitalSelf = this.GetObjectName(this.LoopParent, this.strFemaleGenital);
			}
		}
		return true;
	}

	// Token: 0x06005051 RID: 20561 RVA: 0x001F317B File Offset: 0x001F157B
	public void Release()
	{
		this.lstEyeNeck.Clear();
	}

	// Token: 0x06005052 RID: 20562 RVA: 0x001F3188 File Offset: 0x001F1588
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

	// Token: 0x06005053 RID: 20563 RVA: 0x001F31E8 File Offset: 0x001F15E8
	public bool Load(string _assetpath, string _file)
	{
		this.lstEyeNeck.Clear();
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
						this.info = default(HMotionEyeNeckMale.EyeNeck);
						this.info.Init();
						if (this.row.Count != 0)
						{
							string element = this.row.GetElement(num++);
							if (!element.IsNullOrEmpty())
							{
								this.info.anim = element;
								this.info.openEye = int.Parse(this.row.GetElement(num++));
								this.info.openMouth = int.Parse(this.row.GetElement(num++));
								this.info.eyebrow = int.Parse(this.row.GetElement(num++));
								this.info.eye = int.Parse(this.row.GetElement(num++));
								this.info.mouth = int.Parse(this.row.GetElement(num++));
								this.info.Neckbehaviour = int.Parse(this.row.GetElement(num++));
								this.info.Eyebehaviour = int.Parse(this.row.GetElement(num++));
								this.info.targetNeck = int.Parse(this.row.GetElement(num++));
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
								this.info.NeckRot[0] = zero;
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
								this.info.NeckRot[1] = zero;
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
								this.info.HeadRot[0] = zero;
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
								this.info.HeadRot[1] = zero;
								this.info.targetEye = int.Parse(this.row.GetElement(num++));
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
								this.info.EyeRot[0] = zero;
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
								this.info.EyeRot[1] = zero;
								this.lstEyeNeck.Add(this.info);
							}
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005054 RID: 20564 RVA: 0x001F38A4 File Offset: 0x001F1CA4
	public bool SetPartner(GameObject _objFemale1Bone, GameObject _objFemale2Bone, GameObject _objMaleBone)
	{
		this.objFemale1Head = null;
		this.objFemale2Head = null;
		this.objMaleHead = null;
		this.objFemale1Genital = null;
		this.objFemale2Genital = null;
		this.objMaleGenital = null;
		if (_objFemale1Bone)
		{
			this.LoopParent = _objFemale1Bone.transform;
			if (this.strFemaleHead != string.Empty)
			{
				this.objFemale1Head = this.GetObjectName(this.LoopParent, this.strFemaleHead);
			}
			if (this.strFemaleGenital != string.Empty)
			{
				this.objFemale1Genital = this.LoopParent.FindLoop(this.strFemaleGenital);
			}
		}
		if (_objFemale2Bone)
		{
			this.LoopParent = _objFemale2Bone.transform;
			if (this.strFemaleHead != string.Empty)
			{
				this.objFemale2Head = this.GetObjectName(this.LoopParent, this.strFemaleHead);
			}
			if (this.strFemaleGenital != string.Empty)
			{
				this.objFemale2Genital = this.GetObjectName(this.LoopParent, this.strFemaleGenital);
			}
		}
		if (_objMaleBone)
		{
			this.LoopParent = _objMaleBone.transform;
			if (this.strMaleHead != string.Empty)
			{
				this.objMaleHead = this.GetObjectName(this.LoopParent, this.strMaleHead);
			}
			if (this.strMaleGenital != string.Empty)
			{
				this.objMaleGenital = this.GetObjectName(this.LoopParent, this.strMaleGenital);
			}
		}
		return true;
	}

	// Token: 0x06005055 RID: 20565 RVA: 0x001F3A30 File Offset: 0x001F1E30
	public bool Proc(AnimatorStateInfo _ai)
	{
		this.ai = _ai;
		for (int i = 0; i < this.lstEyeNeck.Count; i++)
		{
			this.en = this.lstEyeNeck[i];
			if (_ai.IsName(this.en.anim))
			{
				this.chaMale.ChangeEyesOpenMax((float)this.en.openEye * 0.1f);
				this.mouthCtrl = this.chaMale.mouthCtrl;
				if (this.mouthCtrl != null)
				{
					this.mouthCtrl.OpenMin = (float)this.en.openMouth * 0.1f;
				}
				this.chaMale.ChangeEyebrowPtn(this.en.eyebrow, true);
				this.chaMale.ChangeEyesPtn(this.en.eye, true);
				this.chaMale.ChangeMouthPtn(this.en.mouth, true);
				this.SetEyesTarget(this.en.targetEye);
				this.SetNeckTarget(this.en.targetNeck);
				this.SetEyeBehaviour(this.en.Eyebehaviour);
				this.SetNeckBehaviour(this.en.Neckbehaviour);
				this.bFaceInfo = false;
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
				}
				this.OldAnimName = this.en.anim;
				return true;
			}
		}
		this.bFaceInfo = true;
		return true;
	}

	// Token: 0x06005056 RID: 20566 RVA: 0x001F3C3C File Offset: 0x001F203C
	private void LateUpdate()
	{
		this.EyeNeckCalc();
	}

	// Token: 0x06005057 RID: 20567 RVA: 0x001F3C44 File Offset: 0x001F2044
	public void EyeNeckCalc()
	{
		if (this.chaMale == null || this.NowEndADV)
		{
			return;
		}
		if (this.bFaceInfo)
		{
			return;
		}
		for (int i = 0; i < this.lstEyeNeck.Count; i++)
		{
			this.en = this.lstEyeNeck[i];
			if (this.ai.IsName(this.en.anim))
			{
				if (this.en.targetNeck == 7)
				{
					if (Singleton<HSceneFlagCtrl>.Instance.motions[0] < 0.5f)
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
					this.NeckCalc(this.en.NeckRot[this.nYuragiType], this.en.HeadRot[this.nYuragiType]);
				}
				if (this.en.targetEye == 7)
				{
					if (Singleton<HSceneFlagCtrl>.Instance.motions[0] < 0.5f)
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
					this.EyeCalc(this.en.EyeRot[this.nYuragiType]);
				}
				return;
			}
		}
	}

	// Token: 0x06005058 RID: 20568 RVA: 0x001F3E28 File Offset: 0x001F2228
	private bool SetEyesTarget(int _tag)
	{
		switch (_tag)
		{
		case 1:
			this.chaMale.ChangeLookEyesTarget(1, (!this.objFemale1Head) ? null : this.objFemale1Head.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 2:
			this.chaMale.ChangeLookEyesTarget(1, (!this.objFemale1Genital) ? null : this.objFemale1Genital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 3:
			this.chaMale.ChangeLookEyesTarget(1, (!this.objFemale2Head) ? null : this.objFemale2Head.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 4:
			this.chaMale.ChangeLookEyesTarget(1, (!this.objFemale2Genital) ? null : this.objFemale2Genital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 5:
			this.chaMale.ChangeLookEyesTarget(1, (!this.objMaleHead) ? null : this.objMaleHead.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 6:
			this.chaMale.ChangeLookEyesTarget(1, (!this.objMaleGenital) ? null : this.objMaleGenital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 7:
			break;
		case 8:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objGenitalSelf) ? null : this.objGenitalSelf.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		default:
			this.chaMale.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
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

	// Token: 0x06005059 RID: 20569 RVA: 0x001F40E0 File Offset: 0x001F24E0
	private bool SetNeckTarget(int _tag)
	{
		switch (_tag)
		{
		case 1:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objFemale1Head) ? null : this.objFemale1Head.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 2:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objFemale1Genital) ? null : this.objFemale1Genital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 3:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objFemale2Head) ? null : this.objFemale2Head.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 4:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objFemale2Genital) ? null : this.objFemale2Genital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 5:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objMaleHead) ? null : this.objMaleHead.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 6:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objMaleGenital) ? null : this.objMaleGenital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 7:
			break;
		case 8:
			this.chaMale.ChangeLookNeckTarget(1, (!this.objGenitalSelf) ? null : this.objGenitalSelf.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		default:
			this.chaMale.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
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

	// Token: 0x0600505A RID: 20570 RVA: 0x001F4380 File Offset: 0x001F2780
	private bool SetNeckBehaviour(int _behaviour)
	{
		if (!this.chaMale.neckLookCtrl.enabled && _behaviour != 3)
		{
			this.chaMale.neckLookCtrl.neckLookScript.UpdateCall(0);
		}
		switch (_behaviour)
		{
		case 1:
			this.chaMale.ChangeLookNeckPtn(1, 1f);
			break;
		case 2:
			this.chaMale.ChangeLookNeckPtn(2, 1f);
			break;
		case 3:
			this.chaMale.ChangeLookNeckPtn(1, 1f);
			break;
		default:
			this.chaMale.ChangeLookNeckPtn(3, 1f);
			break;
		}
		if (_behaviour == 3)
		{
			this.chaMale.neckLookCtrl.enabled = false;
		}
		else
		{
			this.chaMale.neckLookCtrl.enabled = true;
		}
		return true;
	}

	// Token: 0x0600505B RID: 20571 RVA: 0x001F4460 File Offset: 0x001F2860
	private bool SetEyeBehaviour(int _behaviour)
	{
		switch (_behaviour)
		{
		case 1:
			this.chaMale.ChangeLookEyesPtn(1);
			break;
		case 2:
			this.chaMale.ChangeLookEyesPtn(2);
			break;
		case 3:
			this.chaMale.ChangeLookEyesPtn(1);
			break;
		default:
			this.chaMale.ChangeLookEyesPtn(0);
			break;
		}
		if (_behaviour == 3)
		{
			this.chaMale.eyeLookCtrl.enabled = false;
		}
		else
		{
			this.chaMale.eyeLookCtrl.enabled = true;
		}
		return true;
	}

	// Token: 0x0600505C RID: 20572 RVA: 0x001F44FC File Offset: 0x001F28FC
	private void NeckCalc(Vector3 targetNeckRot, Vector3 targetHeadRot)
	{
		float deltaTime = Time.deltaTime;
		this.ChangeTimeNeck = Mathf.Clamp(this.ChangeTimeNeck + deltaTime, 0f, this.chaMale.neckLookCtrl.neckLookScript.changeTypeLeapTime);
		float num = Mathf.InverseLerp(0f, this.chaMale.neckLookCtrl.neckLookScript.changeTypeLeapTime, this.ChangeTimeNeck);
		if (this.chaMale.neckLookCtrl.neckLookScript.changeTypeLerpCurve != null)
		{
			num = this.chaMale.neckLookCtrl.neckLookScript.changeTypeLerpCurve.Evaluate(num);
		}
		this.nowleapSpeed[0] = Mathf.Clamp01(this.chaMale.neckLookCtrl.neckLookScript.neckTypeStates[3].leapSpeed * deltaTime);
		Quaternion b = Quaternion.Slerp(this.BackUpNeck, Quaternion.Euler(targetNeckRot), this.nowleapSpeed[0]);
		Quaternion quaternion = Quaternion.Slerp(this.NeckTrs.localRotation, b, this.chaMale.neckLookCtrl.neckLookScript.calcLerp);
		quaternion = Quaternion.Slerp(this.BackUpNeck, quaternion, num);
		Quaternion quaternion2 = quaternion;
		this.NeckTrs.localRotation = quaternion2;
		this.BackUpNeck = quaternion2;
		b = Quaternion.Slerp(this.BackUpHead, Quaternion.Euler(targetHeadRot), this.nowleapSpeed[0]);
		quaternion = Quaternion.Slerp(this.HeadTrs.localRotation, b, this.chaMale.neckLookCtrl.neckLookScript.calcLerp);
		quaternion = Quaternion.Slerp(this.BackUpHead, quaternion, num);
		quaternion2 = quaternion;
		this.HeadTrs.localRotation = quaternion2;
		this.BackUpHead = quaternion2;
		this.chaMale.neckLookCtrl.neckLookScript.aBones[0].fixAngle = this.NeckTrs.localRotation;
		this.chaMale.neckLookCtrl.neckLookScript.aBones[1].fixAngle = this.HeadTrs.localRotation;
	}

	// Token: 0x0600505D RID: 20573 RVA: 0x001F46E0 File Offset: 0x001F2AE0
	private void EyeCalc(Vector3 targetEyeRot)
	{
		float deltaTime = Time.deltaTime;
		this.ChangeTimeEye = Mathf.Clamp(this.ChangeTimeEye + deltaTime, 0f, 1f);
		float t = Mathf.InverseLerp(0f, 1f, this.ChangeTimeEye);
		this.nowleapSpeed[1] = Mathf.Clamp01(this.chaMale.eyeLookCtrl.eyeLookScript.eyeTypeStates[0].leapSpeed * deltaTime);
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
		this.chaMale.eyeLookCtrl.eyeLookScript.eyeObjs[0].angleH = this.EyeTrs[1].localRotation.eulerAngles.y;
		this.chaMale.eyeLookCtrl.eyeLookScript.eyeObjs[0].angleV = this.EyeTrs[1].localRotation.eulerAngles.x;
		this.chaMale.eyeLookCtrl.eyeLookScript.eyeObjs[1].angleH = this.EyeTrs[1].localRotation.eulerAngles.y;
		this.chaMale.eyeLookCtrl.eyeLookScript.eyeObjs[1].angleV = this.EyeTrs[1].localRotation.eulerAngles.x;
	}

	// Token: 0x040049A6 RID: 18854
	[Label("相手女顔オブジェクト名")]
	public string strFemaleHead = string.Empty;

	// Token: 0x040049A7 RID: 18855
	[Label("相手女性器オブジェクト名")]
	public string strFemaleGenital = string.Empty;

	// Token: 0x040049A8 RID: 18856
	[Label("相手男顔オブジェクト名")]
	public string strMaleHead = string.Empty;

	// Token: 0x040049A9 RID: 18857
	[Label("相手男性器オブジェクト名")]
	public string strMaleGenital = string.Empty;

	// Token: 0x040049AA RID: 18858
	[SerializeField]
	private List<HMotionEyeNeckMale.EyeNeck> lstEyeNeck = new List<HMotionEyeNeckMale.EyeNeck>();

	// Token: 0x040049AB RID: 18859
	[DisabledGroup("男クラス")]
	[SerializeField]
	private ChaControl chaMale;

	// Token: 0x040049AC RID: 18860
	[DisabledGroup("女1顔オブジェクト")]
	[SerializeField]
	private GameObject objFemale1Head;

	// Token: 0x040049AD RID: 18861
	[DisabledGroup("女1性器オブジェクト")]
	[SerializeField]
	private GameObject objFemale1Genital;

	// Token: 0x040049AE RID: 18862
	[DisabledGroup("女2顔オブジェクト")]
	[SerializeField]
	private GameObject objFemale2Head;

	// Token: 0x040049AF RID: 18863
	[DisabledGroup("女2性器オブジェクト")]
	[SerializeField]
	private GameObject objFemale2Genital;

	// Token: 0x040049B0 RID: 18864
	[DisabledGroup("男顔オブジェクト")]
	[SerializeField]
	private GameObject objMaleHead;

	// Token: 0x040049B1 RID: 18865
	[DisabledGroup("男性器オブジェクト")]
	[SerializeField]
	private GameObject objMaleGenital;

	// Token: 0x040049B2 RID: 18866
	[DisabledGroup("自分性器オブジェクト")]
	[SerializeField]
	private GameObject objGenitalSelf;

	// Token: 0x040049B3 RID: 18867
	private Transform LoopParent;

	// Token: 0x040049B4 RID: 18868
	private HMotionEyeNeckMale.EyeNeck en;

	// Token: 0x040049B5 RID: 18869
	private FBSCtrlMouth mouthCtrl;

	// Token: 0x040049B6 RID: 18870
	private AnimatorStateInfo ai;

	// Token: 0x040049B7 RID: 18871
	private Transform NeckTrs;

	// Token: 0x040049B8 RID: 18872
	private Transform HeadTrs;

	// Token: 0x040049B9 RID: 18873
	private Transform[] EyeTrs;

	// Token: 0x040049BA RID: 18874
	private Quaternion BackUpNeck;

	// Token: 0x040049BB RID: 18875
	private Quaternion BackUpHead;

	// Token: 0x040049BC RID: 18876
	private Quaternion[] BackUpEye;

	// Token: 0x040049BD RID: 18877
	private float ChangeTimeNeck;

	// Token: 0x040049BE RID: 18878
	private int NeckType;

	// Token: 0x040049BF RID: 18879
	private float ChangeTimeEye;

	// Token: 0x040049C0 RID: 18880
	private int EyeType;

	// Token: 0x040049C1 RID: 18881
	private bool bFaceInfo;

	// Token: 0x040049C2 RID: 18882
	private int nYuragiType;

	// Token: 0x040049C3 RID: 18883
	private string OldAnimName;

	// Token: 0x040049C4 RID: 18884
	private ExcelData excelData;

	// Token: 0x040049C5 RID: 18885
	private HMotionEyeNeckMale.EyeNeck info;

	// Token: 0x040049C6 RID: 18886
	private List<string> row = new List<string>();

	// Token: 0x040049C7 RID: 18887
	private string abName = string.Empty;

	// Token: 0x040049C8 RID: 18888
	private string assetName = string.Empty;

	// Token: 0x040049C9 RID: 18889
	public bool NowEndADV;

	// Token: 0x040049CA RID: 18890
	private Transform[] getChild;

	// Token: 0x040049CB RID: 18891
	private float[] nowleapSpeed = new float[2];

	// Token: 0x02000AAF RID: 2735
	[Serializable]
	public struct EyeNeck
	{
		// Token: 0x0600505E RID: 20574 RVA: 0x001F4938 File Offset: 0x001F2D38
		public void Init()
		{
			this.anim = string.Empty;
			this.openEye = 0;
			this.openMouth = 0;
			this.eyebrow = 0;
			this.eye = 0;
			this.mouth = 0;
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

		// Token: 0x040049CC RID: 18892
		[Label("アニメーション名")]
		public string anim;

		// Token: 0x040049CD RID: 18893
		[Label("目の開き")]
		public int openEye;

		// Token: 0x040049CE RID: 18894
		[Label("口の開き")]
		public int openMouth;

		// Token: 0x040049CF RID: 18895
		[Label("眉")]
		public int eyebrow;

		// Token: 0x040049D0 RID: 18896
		[Label("目")]
		public int eye;

		// Token: 0x040049D1 RID: 18897
		[Label("口")]
		public int mouth;

		// Token: 0x040049D2 RID: 18898
		[Label("首挙動")]
		public int Neckbehaviour;

		// Token: 0x040049D3 RID: 18899
		[Label("目挙動")]
		public int Eyebehaviour;

		// Token: 0x040049D4 RID: 18900
		[Label("目ターゲット")]
		public int targetEye;

		// Token: 0x040049D5 RID: 18901
		[Label("視線角度")]
		public Vector3[] EyeRot;

		// Token: 0x040049D6 RID: 18902
		[Label("首ターゲット")]
		public int targetNeck;

		// Token: 0x040049D7 RID: 18903
		[Label("首角度")]
		public Vector3[] NeckRot;

		// Token: 0x040049D8 RID: 18904
		[Label("頭角度")]
		public Vector3[] HeadRot;
	}
}
