using System;
using System.Collections.Generic;
using AIChara;
using AIProject;
using Manager;
using UnityEngine;

// Token: 0x02000AA8 RID: 2728
public class HMotionEyeNeckFemale : MonoBehaviour
{
	// Token: 0x0600502E RID: 20526 RVA: 0x001EED08 File Offset: 0x001ED108
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

	// Token: 0x0600502F RID: 20527 RVA: 0x001EEEB4 File Offset: 0x001ED2B4
	public void Release()
	{
		this.lstEyeNeck.Clear();
	}

	// Token: 0x06005030 RID: 20528 RVA: 0x001EEEC4 File Offset: 0x001ED2C4
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

	// Token: 0x06005031 RID: 20529 RVA: 0x001EEF24 File Offset: 0x001ED324
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
						this.info = default(HMotionEyeNeckFemale.EyeNeck);
						this.info.Init();
						if (this.row.Count != 0)
						{
							string element = this.row.GetElement(num++);
							if (!element.IsNullOrEmpty())
							{
								this.info.anim = element;
								this.info.isConfigDisregardNeck = (this.row.GetElement(num++) == "1");
								this.info.isConfigDisregardEye = (this.row.GetElement(num++) == "1");
								element = this.row.GetElement(num++);
								this.info.faceinfo[0].isDisregardVoiceNeck = (!element.IsNullOrEmpty() && element == "1");
								element = this.row.GetElement(num++);
								this.info.faceinfo[0].isDisregardVoiceEye = (!element.IsNullOrEmpty() && element == "1");
								this.info.faceinfo[0].Neckbehaviour = int.Parse(this.row.GetElement(num++));
								this.info.faceinfo[0].Eyebehaviour = int.Parse(this.row.GetElement(num++));
								this.info.faceinfo[0].targetNeck = int.Parse(this.row.GetElement(num++));
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
								this.info.faceinfo[0].NeckRot[0] = zero;
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
								this.info.faceinfo[0].NeckRot[1] = zero;
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
								this.info.faceinfo[0].HeadRot[0] = zero;
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
								this.info.faceinfo[0].HeadRot[1] = zero;
								this.info.faceinfo[0].targetEye = int.Parse(this.row.GetElement(num++));
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
								this.info.faceinfo[0].EyeRot[0] = zero;
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
								this.info.faceinfo[0].EyeRot[1] = zero;
								if (num >= this.row.Count)
								{
									this.lstEyeNeck.Add(this.info);
								}
								else
								{
									element = this.row.GetElement(num++);
									if (element.IsNullOrEmpty())
									{
										this.lstEyeNeck.Add(this.info);
									}
									else
									{
										this.info.ExistFaceInfoIya = true;
										this.info.faceinfo[1].isDisregardVoiceNeck = (!element.IsNullOrEmpty() && element == "1");
										element = this.row.GetElement(num++);
										this.info.faceinfo[1].isDisregardVoiceEye = (!element.IsNullOrEmpty() && element == "1");
										this.info.faceinfo[1].Neckbehaviour = int.Parse(this.row.GetElement(num++));
										this.info.faceinfo[1].Eyebehaviour = int.Parse(this.row.GetElement(num++));
										this.info.faceinfo[1].targetNeck = int.Parse(this.row.GetElement(num++));
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
										this.info.faceinfo[1].NeckRot[0] = zero;
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
										this.info.faceinfo[1].NeckRot[1] = zero;
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
										this.info.faceinfo[1].HeadRot[0] = zero;
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
										this.info.faceinfo[1].HeadRot[1] = zero;
										this.info.faceinfo[1].targetEye = int.Parse(this.row.GetElement(num++));
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
										this.info.faceinfo[1].EyeRot[0] = zero;
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
										this.info.faceinfo[1].EyeRot[1] = zero;
										this.lstEyeNeck.Add(this.info);
									}
								}
							}
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005032 RID: 20530 RVA: 0x001EFCC4 File Offset: 0x001EE0C4
	public bool SetPartner(GameObject _objMale1Bone, GameObject _objMale2Bone, GameObject _objFemale1Bone)
	{
		this.objMale1Head = null;
		this.objMale2Head = null;
		this.objFemale1Head = null;
		this.objMale1Genital = null;
		this.objMale2Genital = null;
		this.objFemale1Genital = null;
		if (_objMale1Bone)
		{
			this.LoopParent = _objMale1Bone.transform;
			if (this.strMaleHead != string.Empty)
			{
				this.objMale1Head = this.GetObjectName(this.LoopParent, this.strMaleHead);
			}
			if (this.strMaleGenital != string.Empty)
			{
				this.objMale1Genital = this.GetObjectName(this.LoopParent, this.strMaleGenital);
			}
		}
		if (_objMale2Bone)
		{
			this.LoopParent = _objMale2Bone.transform;
			if (this.strMaleHead != string.Empty)
			{
				this.objMale2Head = this.GetObjectName(this.LoopParent, this.strMaleHead);
			}
			if (this.strMaleGenital != string.Empty)
			{
				this.objMale2Genital = this.GetObjectName(this.LoopParent, this.strMaleGenital);
			}
		}
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

	// Token: 0x06005033 RID: 20531 RVA: 0x001EFE50 File Offset: 0x001EE250
	public bool Proc(AnimatorStateInfo _ai, HVoiceCtrl.FaceInfo _faceVoice, int _main)
	{
		bool flag = (_main != 0) ? Config.HData.EyeDir1 : Config.HData.EyeDir0;
		bool flag2 = (_main != 0) ? Config.HData.NeckDir1 : Config.HData.NeckDir0;
		this.ai = _ai;
		this.faceInfo = _faceVoice;
		for (int i = 0; i < this.lstEyeNeck.Count; i++)
		{
			if (_ai.IsName(this.lstEyeNeck[i].anim))
			{
				this.en = this.lstEyeNeck[i];
				if (this.en.ExistFaceInfoIya)
				{
					this.enFace = this.en.faceinfo[Singleton<HSceneManager>.Instance.isForce ? 1 : 0];
				}
				else
				{
					this.enFace = this.en.faceinfo[0];
				}
			}
		}
		if (this.hscene.ctrlVoice.nowVoices[this.ID].state == HVoiceCtrl.VoiceKind.startVoice || this.hscene.ctrlVoice.nowVoices[this.ID].state == HVoiceCtrl.VoiceKind.voice)
		{
			if (!this.enFace.isDisregardVoiceEye)
			{
				this.bFaceInfo[0] = true;
				this.SetEyesTarget((!flag) ? ((_faceVoice == null) ? 0 : _faceVoice.targetEyeLine) : 0);
				this.SetBehaviourEyes((!flag) ? ((_faceVoice == null) ? 0 : _faceVoice.behaviorEyeLine) : 1);
			}
			else
			{
				this.SetEyesTarget((!this.en.isConfigDisregardEye) ? ((!flag) ? this.enFace.targetEye : 0) : this.enFace.targetEye);
				this.SetBehaviourEyes((!this.en.isConfigDisregardEye) ? ((!flag) ? this.enFace.Eyebehaviour : 1) : this.enFace.Eyebehaviour);
			}
			if (!this.enFace.isDisregardVoiceNeck)
			{
				this.bFaceInfo[1] = true;
				this.SetNeckTarget((!flag2) ? ((_faceVoice == null) ? 0 : _faceVoice.targetNeckLine) : 0);
				this.SetBehaviourNeck((!flag2) ? ((_faceVoice == null) ? 0 : _faceVoice.behaviorNeckLine) : 1);
			}
			else
			{
				this.SetNeckTarget((!this.en.isConfigDisregardNeck) ? ((!flag2) ? this.enFace.targetNeck : 0) : this.enFace.targetNeck);
				this.SetBehaviourNeck((!this.en.isConfigDisregardNeck) ? ((!flag2) ? this.enFace.Neckbehaviour : 1) : this.enFace.Neckbehaviour);
			}
		}
		else
		{
			this.SetNeckTarget((!this.en.isConfigDisregardNeck) ? ((!flag2) ? this.enFace.targetNeck : 0) : this.enFace.targetNeck);
			this.SetEyesTarget((!this.en.isConfigDisregardEye) ? ((!flag) ? this.enFace.targetEye : 0) : this.enFace.targetEye);
			this.SetBehaviourEyes((!this.en.isConfigDisregardEye) ? ((!flag) ? this.enFace.Eyebehaviour : 1) : this.enFace.Eyebehaviour);
			this.SetBehaviourNeck((!this.en.isConfigDisregardNeck) ? ((!flag2) ? this.enFace.Neckbehaviour : 1) : this.enFace.Neckbehaviour);
			this.bFaceInfo[0] = false;
			this.bFaceInfo[1] = false;
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
		}
		return true;
	}

	// Token: 0x06005034 RID: 20532 RVA: 0x001F0336 File Offset: 0x001EE736
	private void LateUpdate()
	{
		this.EyeNeckCalc();
	}

	// Token: 0x06005035 RID: 20533 RVA: 0x001F0340 File Offset: 0x001EE740
	public void EyeNeckCalc()
	{
		if (this.chaFemale == null || this.NowEndADV)
		{
			return;
		}
		bool flag = (this.ID != 0) ? Config.HData.NeckDir1 : Config.HData.NeckDir0;
		bool flag2 = (this.ID != 0) ? Config.HData.EyeDir1 : Config.HData.EyeDir0;
		float num = Singleton<HSceneFlagCtrl>.Instance.motions[this.ID];
		if (!this.bFaceInfo[0] || !this.bFaceInfo[1])
		{
			for (int i = 0; i < this.lstEyeNeck.Count; i++)
			{
				if (this.ai.IsName(this.lstEyeNeck[i].anim))
				{
					this.en = this.lstEyeNeck[i];
					if (this.en.ExistFaceInfoIya)
					{
						this.enFace = this.en.faceinfo[Singleton<HSceneManager>.Instance.isForce ? 1 : 0];
					}
					else
					{
						this.enFace = this.en.faceinfo[0];
					}
					if (!flag)
					{
						if (!this.bFaceInfo[1])
						{
							if (this.enFace.targetNeck == 7)
							{
								if (num < 0.5f)
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
						}
						else if (this.faceInfo != null && this.faceInfo.targetNeckLine == 7)
						{
							if (num < 0.5f)
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
							this.NeckCalc(this.faceInfo.NeckRot[this.nYuragiType], this.faceInfo.HeadRot[this.nYuragiType]);
						}
					}
					if (!flag2)
					{
						if (!this.bFaceInfo[0])
						{
							if (this.enFace.targetEye == 7)
							{
								if (num < 0.5f)
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
						}
						else if (this.faceInfo != null && this.faceInfo.targetEyeLine == 7)
						{
							if (num < 0.5f)
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
							this.EyeCalc(this.faceInfo.EyeRot[this.nYuragiType]);
						}
					}
					return;
				}
			}
		}
		else if (this.faceInfo != null)
		{
			if (!flag && this.faceInfo.targetNeckLine == 7)
			{
				if (num < 0.5f)
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
				this.NeckCalc(this.faceInfo.NeckRot[this.nYuragiType], this.faceInfo.HeadRot[this.nYuragiType]);
			}
			if (!flag2 && this.faceInfo.targetEyeLine == 7)
			{
				if (num < 0.5f)
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
				this.EyeCalc(this.faceInfo.EyeRot[this.nYuragiType]);
			}
			return;
		}
	}

	// Token: 0x06005036 RID: 20534 RVA: 0x001F08DC File Offset: 0x001EECDC
	private bool SetEyesTarget(int _tag)
	{
		switch (_tag)
		{
		case 1:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objMale1Head) ? null : this.objMale1Head.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 2:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objMale1Genital) ? null : this.objMale1Genital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 3:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objMale2Head) ? null : this.objMale2Head.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 4:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objMale2Genital) ? null : this.objMale2Genital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 5:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objFemale1Head) ? null : this.objFemale1Head.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 6:
			this.chaFemale.ChangeLookEyesTarget(1, (!this.objFemale1Genital) ? null : this.objFemale1Genital.transform, 0.5f, 0f, 1f, 2f);
			break;
		case 7:
			break;
		case 8:
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

	// Token: 0x06005037 RID: 20535 RVA: 0x001F0B94 File Offset: 0x001EEF94
	private bool SetNeckTarget(int _tag)
	{
		switch (_tag)
		{
		case 1:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objMale1Head) ? null : this.objMale1Head.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 2:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objMale1Genital) ? null : this.objMale1Genital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 3:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objMale2Head) ? null : this.objMale2Head.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 4:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objMale2Genital) ? null : this.objMale2Genital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 5:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objFemale1Head) ? null : this.objFemale1Head.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 6:
			this.chaFemale.ChangeLookNeckTarget(1, (!this.objFemale1Genital) ? null : this.objFemale1Genital.transform, 0.5f, 0f, 1f, 0.8f);
			break;
		case 7:
			break;
		case 8:
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

	// Token: 0x06005038 RID: 20536 RVA: 0x001F0E34 File Offset: 0x001EF234
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

	// Token: 0x06005039 RID: 20537 RVA: 0x001F0ED0 File Offset: 0x001EF2D0
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

	// Token: 0x0600503A RID: 20538 RVA: 0x001F0FB0 File Offset: 0x001EF3B0
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

	// Token: 0x0600503B RID: 20539 RVA: 0x001F1194 File Offset: 0x001EF594
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
		float num3 = this.EyeTrs[1].localRotation.eulerAngles.y;
		float num4 = this.EyeTrs[1].localRotation.eulerAngles.x;
		num3 = ((num3 <= 180f) ? num3 : (num3 - 360f));
		num4 = ((num4 <= 180f) ? num4 : (num4 - 360f));
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[0].angleH = num3;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[0].angleV = num4;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[1].angleH = num3;
		this.chaFemale.eyeLookCtrl.eyeLookScript.eyeObjs[1].angleV = num4;
	}

	// Token: 0x04004939 RID: 18745
	[Label("相手男顔オブジェクト名")]
	public string strMaleHead = string.Empty;

	// Token: 0x0400493A RID: 18746
	[Label("相手男性器オブジェクト名")]
	public string strMaleGenital = string.Empty;

	// Token: 0x0400493B RID: 18747
	[Label("相手女顔オブジェクト名")]
	public string strFemaleHead = string.Empty;

	// Token: 0x0400493C RID: 18748
	[Label("相手女性器オブジェクト名")]
	public string strFemaleGenital = string.Empty;

	// Token: 0x0400493D RID: 18749
	[SerializeField]
	private List<HMotionEyeNeckFemale.EyeNeck> lstEyeNeck = new List<HMotionEyeNeckFemale.EyeNeck>();

	// Token: 0x0400493E RID: 18750
	[DisabledGroup("女クラス")]
	[SerializeField]
	private ChaControl chaFemale;

	// Token: 0x0400493F RID: 18751
	[DisabledGroup("自分性器オブジェクト")]
	[SerializeField]
	private GameObject objGenitalSelf;

	// Token: 0x04004940 RID: 18752
	[DisabledGroup("男1顔オブジェクト")]
	[SerializeField]
	private GameObject objMale1Head;

	// Token: 0x04004941 RID: 18753
	[DisabledGroup("男1性器オブジェクト")]
	[SerializeField]
	private GameObject objMale1Genital;

	// Token: 0x04004942 RID: 18754
	[DisabledGroup("男2顔オブジェクト")]
	[SerializeField]
	private GameObject objMale2Head;

	// Token: 0x04004943 RID: 18755
	[DisabledGroup("男2性器オブジェクト")]
	[SerializeField]
	private GameObject objMale2Genital;

	// Token: 0x04004944 RID: 18756
	[DisabledGroup("女相手顔オブジェクト")]
	[SerializeField]
	private GameObject objFemale1Head;

	// Token: 0x04004945 RID: 18757
	[DisabledGroup("女相手性器オブジェクト")]
	[SerializeField]
	private GameObject objFemale1Genital;

	// Token: 0x04004946 RID: 18758
	private Transform LoopParent;

	// Token: 0x04004947 RID: 18759
	private HMotionEyeNeckFemale.EyeNeck en;

	// Token: 0x04004948 RID: 18760
	private HMotionEyeNeckFemale.EyeNeckFace enFace;

	// Token: 0x04004949 RID: 18761
	private int ID;

	// Token: 0x0400494A RID: 18762
	private AnimatorStateInfo ai;

	// Token: 0x0400494B RID: 18763
	private Transform NeckTrs;

	// Token: 0x0400494C RID: 18764
	private Transform HeadTrs;

	// Token: 0x0400494D RID: 18765
	private Transform[] EyeTrs;

	// Token: 0x0400494E RID: 18766
	private Quaternion BackUpNeck;

	// Token: 0x0400494F RID: 18767
	private Quaternion BackUpHead;

	// Token: 0x04004950 RID: 18768
	private Quaternion[] BackUpEye;

	// Token: 0x04004951 RID: 18769
	private HVoiceCtrl.FaceInfo faceInfo;

	// Token: 0x04004952 RID: 18770
	private float ChangeTimeNeck;

	// Token: 0x04004953 RID: 18771
	private int NeckType;

	// Token: 0x04004954 RID: 18772
	private float ChangeTimeEye;

	// Token: 0x04004955 RID: 18773
	private int EyeType;

	// Token: 0x04004956 RID: 18774
	private bool[] bFaceInfo = new bool[2];

	// Token: 0x04004957 RID: 18775
	private int nYuragiType;

	// Token: 0x04004958 RID: 18776
	private string OldAnimName;

	// Token: 0x04004959 RID: 18777
	private HScene hscene;

	// Token: 0x0400495A RID: 18778
	private ExcelData excelData;

	// Token: 0x0400495B RID: 18779
	private HMotionEyeNeckFemale.EyeNeck info;

	// Token: 0x0400495C RID: 18780
	private List<string> row = new List<string>();

	// Token: 0x0400495D RID: 18781
	private string abName = string.Empty;

	// Token: 0x0400495E RID: 18782
	private string assetName = string.Empty;

	// Token: 0x0400495F RID: 18783
	public bool NowEndADV;

	// Token: 0x04004960 RID: 18784
	private Transform[] getChild;

	// Token: 0x04004961 RID: 18785
	private float[] nowleapSpeed = new float[2];

	// Token: 0x02000AA9 RID: 2729
	[Serializable]
	public struct EyeNeck
	{
		// Token: 0x0600503C RID: 20540 RVA: 0x001F13F4 File Offset: 0x001EF7F4
		public void Init()
		{
			this.anim = string.Empty;
			this.isConfigDisregardNeck = false;
			this.isConfigDisregardEye = false;
			this.ExistFaceInfoIya = false;
			this.faceinfo = new HMotionEyeNeckFemale.EyeNeckFace[2];
			this.faceinfo[0].Init();
			this.faceinfo[1].Init();
		}

		// Token: 0x04004962 RID: 18786
		[Label("アニメーション名")]
		public string anim;

		// Token: 0x04004963 RID: 18787
		[Label("首コンフィグ無視")]
		public bool isConfigDisregardNeck;

		// Token: 0x04004964 RID: 18788
		[Label("目コンフィグ無視")]
		public bool isConfigDisregardEye;

		// Token: 0x04004965 RID: 18789
		public bool ExistFaceInfoIya;

		// Token: 0x04004966 RID: 18790
		public HMotionEyeNeckFemale.EyeNeckFace[] faceinfo;
	}

	// Token: 0x02000AAA RID: 2730
	[Serializable]
	public struct EyeNeckFace
	{
		// Token: 0x0600503D RID: 20541 RVA: 0x001F1450 File Offset: 0x001EF850
		public void Init()
		{
			this.isDisregardVoiceNeck = false;
			this.isDisregardVoiceEye = false;
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

		// Token: 0x04004967 RID: 18791
		[Label("セリフ無視(首)")]
		public bool isDisregardVoiceNeck;

		// Token: 0x04004968 RID: 18792
		[Label("セリフ無視(目)")]
		public bool isDisregardVoiceEye;

		// Token: 0x04004969 RID: 18793
		[Label("首挙動")]
		public int Neckbehaviour;

		// Token: 0x0400496A RID: 18794
		[Label("目挙動")]
		public int Eyebehaviour;

		// Token: 0x0400496B RID: 18795
		[Label("首ターゲット")]
		public int targetNeck;

		// Token: 0x0400496C RID: 18796
		[Label("首角度")]
		public Vector3[] NeckRot;

		// Token: 0x0400496D RID: 18797
		[Label("頭角度")]
		public Vector3[] HeadRot;

		// Token: 0x0400496E RID: 18798
		[Label("目ターゲット")]
		public int targetEye;

		// Token: 0x0400496F RID: 18799
		[Label("視線角度")]
		public Vector3[] EyeRot;
	}
}
