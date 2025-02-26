using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIChara;
using AIProject;
using AIProject.Definitions;
using Manager;
using UniRx;
using UnityEngine;

// Token: 0x02000ADA RID: 2778
public class HVoiceCtrl : MonoBehaviour
{
	// Token: 0x06005163 RID: 20835 RVA: 0x00212AD0 File Offset: 0x00210ED0
	public bool SetVoiceList(int _mode, int _kind, int _id, List<int> _lstSystem)
	{
		this.lstSystem = _lstSystem;
		this.nowKind = _kind;
		this.nowId = _id;
		switch (_mode)
		{
		case 3:
			this.nowMode = 4;
			break;
		case 4:
			this.nowMode = 5;
			break;
		case 5:
			this.nowMode = 3;
			break;
		default:
			this.nowMode = _mode;
			break;
		}
		int key = 0;
		if (this.nowMode == 3)
		{
			foreach (KeyValuePair<int, List<int>> keyValuePair in this.EtcKind)
			{
				if (keyValuePair.Value.Contains(this.nowId))
				{
					key = keyValuePair.Key;
					break;
				}
			}
		}
		else if (this.nowMode == 5)
		{
			int item;
			if (this.ctrlFlag.selectAnimationListInfo != null)
			{
				item = this.ctrlFlag.selectAnimationListInfo.ActionCtrl.Item2;
			}
			else
			{
				item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2;
			}
			if (item == 1 || item == 2)
			{
				key = 1;
			}
		}
		if (this.lstLoadVoicePtn[0].ContainsKey(this.nowMode))
		{
			this.lstLoadVoicePtn[0][this.nowMode].TryGetValue(key, out this.lstVoicePtn[0]);
		}
		if (this.lstLoadVoicePtn[1] != null && this.lstLoadVoicePtn[1].ContainsKey(this.nowMode))
		{
			this.lstLoadVoicePtn[1][this.nowMode].TryGetValue(key, out this.lstVoicePtn[1]);
		}
		this.playAnimation = new HVoiceCtrl.VoiceAnimationPlay();
		if (this.dicdicVoicePlayAnimation.ContainsKey(this.nowMode))
		{
			this.dicdicVoicePlayAnimation[this.nowMode].TryGetValue(_id, out this.playAnimation);
		}
		else
		{
			this.dicdicVoicePlayAnimation.Add(this.nowMode, new Dictionary<int, HVoiceCtrl.VoiceAnimationPlay>());
		}
		if (this.playAnimation == null)
		{
			this.playAnimation = new HVoiceCtrl.VoiceAnimationPlay();
			this.dicdicVoicePlayAnimation[this.nowMode].Add(this.nowId, this.playAnimation);
		}
		for (int i = 0; i < 2; i++)
		{
			this.nowVoices[i].animBreath = string.Empty;
			this.nowVoices[i].animVoice = string.Empty;
			this.nowVoices[i].breathGroup = -1;
		}
		return true;
	}

	// Token: 0x06005164 RID: 20836 RVA: 0x00212D88 File Offset: 0x00211188
	public bool SetBreathVoiceList(ChaControl[] _charas, int _mode, int _kind, bool _isFemaleMain, bool _isIyagari, bool _isMainFirstPerson = true)
	{
		this.breathUseLists[0] = this.breathLists[0];
		this.breathUseLists[1] = this.breathLists[1];
		int[] array = new int[]
		{
			-1,
			-1
		};
		if (_isIyagari && this.hSceneManager.EventKind != HSceneManager.HEvent.Yobai)
		{
			array[0] = (array[1] = 2);
		}
		else if (_isFemaleMain && _mode != 4)
		{
			array[0] = (array[1] = 3);
		}
		if (_mode < 3 && _mode != 1)
		{
			_kind = 0;
		}
		else if (_mode == 1)
		{
			int id;
			if (this.ctrlFlag.selectAnimationListInfo != null)
			{
				id = this.ctrlFlag.selectAnimationListInfo.id;
			}
			else
			{
				id = this.ctrlFlag.nowAnimationInfo.id;
			}
			foreach (KeyValuePair<int, List<int>> keyValuePair in this.HoushiKind)
			{
				if (keyValuePair.Value.Contains(id))
				{
					_kind = keyValuePair.Key;
					break;
				}
				_kind = 0;
			}
		}
		else if (_mode == 3)
		{
			int id2;
			if (this.ctrlFlag.selectAnimationListInfo != null)
			{
				id2 = this.ctrlFlag.selectAnimationListInfo.id;
			}
			else
			{
				id2 = this.ctrlFlag.nowAnimationInfo.id;
			}
			if (this.EtcKind[3].Contains(id2))
			{
				_kind = 1;
			}
			else if (this.EtcKind[4].Contains(id2))
			{
				_kind = 7;
			}
		}
		for (int i = 0; i < 2; i++)
		{
			this.dicBreathUsePtns[i] = null;
			if (this.dicBreathPtns[i].ContainsKey(_mode))
			{
				if (this.dicBreathPtns[i][_mode].ContainsKey(_kind))
				{
					int key = (!_isMainFirstPerson) ? (i ^ 1) : i;
					if (this.dicBreathPtns[i][_mode][_kind].ContainsKey(key))
					{
						if (array[i] == -1)
						{
							array[i] = ((!_charas[i] || this.hSceneManager.PersonalPhase[i] <= 1) ? 0 : 1);
						}
						if (this.dicBreathPtns[i][_mode][_kind][key].ContainsKey(array[i]))
						{
							this.dicBreathUsePtns[i] = this.dicBreathPtns[i][_mode][_kind][key][array[i]];
						}
					}
				}
			}
		}
		for (int j = 0; j < 2; j++)
		{
			this.dicShortBreathUsePtns[j] = null;
			if (this.shortBreathPtns[j].dicInfo.ContainsKey(_mode))
			{
				if (this.shortBreathPtns[j].dicInfo[_mode].ContainsKey(_kind))
				{
					int key2 = (!_isMainFirstPerson) ? (j ^ 1) : j;
					ValueDictionary<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> valueDictionary = this.shortBreathPtns[j].dicInfo[_mode][_kind];
					if (this.shortBreathPtns[j].dicInfo[_mode][_kind].ContainsKey(key2))
					{
						this.dicShortBreathUsePtns[j] = this.shortBreathPtns[j].dicInfo[_mode][_kind][key2];
					}
				}
			}
		}
		for (int k = 0; k < 2; k++)
		{
			if (this.dicBreathUsePtns[k] == null)
			{
				this.dicBreathUsePtns[k] = this.dicBreathUsePtns.New<int, string, HVoiceCtrl.BreathPtn>();
			}
			if (this.dicShortBreathUsePtns[k] == null)
			{
				this.dicShortBreathUsePtns[k] = this.dicShortBreathUsePtns.New<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
			}
		}
		return true;
	}

	// Token: 0x06005165 RID: 20837 RVA: 0x002131E8 File Offset: 0x002115E8
	public bool AfterFinish()
	{
		if (this.playAnimation == null)
		{
			return false;
		}
		this.playAnimation.AfterFinish();
		return true;
	}

	// Token: 0x06005166 RID: 20838 RVA: 0x00213204 File Offset: 0x00211604
	public bool Proc(AnimatorStateInfo _ai, params ChaControl[] _females)
	{
		this.isPlays[0] = false;
		this.isPlays[1] = false;
		bool flag = this.ctrlFlag.nowAnimationInfo.nPromiscuity > 0;
		for (int i = 1; i > -1; i--)
		{
			if ((_females.Length != 1 || i != 1) && !(_females[i] == null))
			{
				if (i != 1 || flag)
				{
					this.isPlays[i] = this.StartVoiceProc(_females, i);
				}
			}
		}
		for (int j = 1; j > -1; j--)
		{
			if ((_females.Length != 1 || j != 1) && !(_females[j] == null))
			{
				if (j != 1 || flag)
				{
					if (!this.isPlays[j])
					{
						this.isPlays[j] = this.VoiceProc(_ai, _females[j], j);
					}
				}
			}
		}
		for (int k = 0; k < 2; k++)
		{
			if ((_females.Length != 1 || k != 1) && !(_females[k] == null))
			{
				if (k != 1 || flag)
				{
					if (!this.isPlays[k])
					{
						this.isPlays[k] = this.ShortBreathProc(_females[k], k);
					}
				}
			}
		}
		for (int l = 0; l < 2; l++)
		{
			if ((_females.Length != 1 || l != 1) && !(_females[l] == null))
			{
				if (l != 1 || flag)
				{
					if (!this.isPlays[l])
					{
						this.BreathProc(_ai, _females[l], l, false);
					}
				}
			}
		}
		for (int m = 0; m < 2; m++)
		{
			if (_females.Length != 1 && !(_females[m] == null))
			{
				if (m != 1 || flag)
				{
					this.OpenCtrl(_females[m], m);
				}
			}
		}
		this.nowVoices[0].speedStateFast = this.ctrlFlag.nowSpeedStateFast;
		if (_females.Length > 1 && _females[1] != null && _females[1].visibleAll && _females[1].objTop != null)
		{
			this.nowVoices[1].speedStateFast = this.ctrlFlag.nowSpeedStateFast;
		}
		for (int n = 0; n < 2; n++)
		{
			if (n != 1 || (_females.Length > 1 && !(_females[1] == null) && _females[1].visibleAll && !(_females[1].objTop == null)))
			{
				if (!this.isVoiceCheck(n) && (this.nowVoices[n].state == HVoiceCtrl.VoiceKind.startVoice || this.nowVoices[n].state == HVoiceCtrl.VoiceKind.voice))
				{
					this.nowVoices[n].state = HVoiceCtrl.VoiceKind.none;
				}
			}
		}
		if ((this.isVoiceCheck(0) && (this.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice)) || (this.isVoiceCheck(1) && (this.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice || this.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice)))
		{
			this.HouchiTime = 0f;
		}
		if (this.ctrlFlag.StartHouchiTime < this.HouchiTime)
		{
			if (this.MapHID < 0)
			{
				this.ctrlFlag.voice.playVoices[0] = true;
				this.ctrlFlag.voice.playVoices[1] = true;
			}
			else if (this.ctrlFlag.MapHvoices.ContainsKey(this.MapHID))
			{
				this.ctrlFlag.MapHvoices[this.MapHID].playVoices[0] = true;
				this.ctrlFlag.MapHvoices[this.MapHID].playVoices[1] = true;
			}
		}
		for (int num = 1; num > -1; num--)
		{
			if (((_females.Length == 1 && num == 1) || _females[num] == null || !_females[num].visibleAll) && (this.nowVoices[num].state == HVoiceCtrl.VoiceKind.voice || this.nowVoices[num].state == HVoiceCtrl.VoiceKind.startVoice))
			{
				this.nowVoices[num].state = HVoiceCtrl.VoiceKind.none;
			}
		}
		return true;
	}

	// Token: 0x06005167 RID: 20839 RVA: 0x002136B4 File Offset: 0x00211AB4
	public bool OpenCtrl(ChaControl _female, int _main)
	{
		if (!_female.visibleAll || _female.objBody == null)
		{
			return false;
		}
		float maxValue = 0f;
		if (this.blendEyes[0] == null)
		{
			this.blendEyes[0] = new GlobalMethod.FloatBlend();
			this.blendEyes[1] = new GlobalMethod.FloatBlend();
		}
		if (this.blendMouths[0] == null)
		{
			this.blendMouths[0] = new GlobalMethod.FloatBlend();
			this.blendMouths[1] = new GlobalMethod.FloatBlend();
		}
		if (this.blendMouthMaxs[0] == null)
		{
			this.blendMouthMaxs[0] = new GlobalMethod.FloatBlend();
			this.blendMouthMaxs[1] = new GlobalMethod.FloatBlend();
		}
		if (this.blendEyes[_main].Proc(ref maxValue))
		{
			_female.ChangeEyesOpenMax(maxValue);
		}
		FBSCtrlMouth mouthCtrl = _female.mouthCtrl;
		if (mouthCtrl != null)
		{
			float num = 0f;
			if (this.blendMouths[_main].Proc(ref num))
			{
				mouthCtrl.OpenMin = num;
			}
			num = 1f;
			if (this.blendMouthMaxs[_main].Proc(ref num))
			{
				mouthCtrl.OpenMax = num;
			}
		}
		return true;
	}

	// Token: 0x06005168 RID: 20840 RVA: 0x002137C8 File Offset: 0x00211BC8
	public bool FaceReset(ChaControl _female)
	{
		_female.ChangeEyesOpenMax(1f);
		FBSCtrlMouth mouthCtrl = _female.mouthCtrl;
		if (mouthCtrl != null)
		{
			mouthCtrl.OpenMin = 0f;
		}
		_female.DisableShapeMouth(false);
		return true;
	}

	// Token: 0x06005169 RID: 20841 RVA: 0x00213800 File Offset: 0x00211C00
	public bool BreathProc(AnimatorStateInfo _ai, ChaControl _female, int _main, bool _forceSleepIdle = false)
	{
		if (!_female.visibleAll || _female.objBody == null)
		{
			return false;
		}
		if (this.breathUseLists[_main].lstVoiceList.Count == 0)
		{
			return false;
		}
		foreach (KeyValuePair<string, HVoiceCtrl.BreathPtn> keyValuePair in this.dicBreathUsePtns[_main])
		{
			if (_ai.IsName(keyValuePair.Key) || _forceSleepIdle)
			{
				HVoiceCtrl.BreathPtn value = keyValuePair.Value;
				if (!_forceSleepIdle || !(value.anim != "D_Idle"))
				{
					if (value.onlyOne && (value.anim == this.nowVoices[_main].animBreath || value.anim == this.nowVoices[_main].animVoice))
					{
						break;
					}
					if (this.nowVoices[_main].state != HVoiceCtrl.VoiceKind.breath)
					{
						if (!value.force && this.isVoiceCheck(_main))
						{
							break;
						}
					}
					else
					{
						if (this.ctrlFlag.isGaugeHit != this.nowVoices[_main].isGaugeHit)
						{
							if (this.nowVoices[_main].breathInfo != null)
							{
								this.SetBreathFace(value, _main);
								this.SetFace(this.nowVoices[_main].Face, _female, _main);
							}
						}
						else
						{
							this.nowVoices[_main].timeFaceDelta += Time.deltaTime;
							if (this.nowVoices[_main].timeFaceDelta >= this.nowVoices[_main].timeFace && this.nowVoices[_main].breathInfo != null)
							{
								this.SetBreathFace(value, _main);
								this.SetFace(this.nowVoices[_main].Face, _female, _main);
							}
						}
						this.nowVoices[_main].isGaugeHit = this.ctrlFlag.isGaugeHit;
					}
					List<int> list = new List<int>();
					for (int i = 0; i < value.lstInfo.Count; i++)
					{
						HVoiceCtrl.BreathVoicePtnInfo breathVoicePtnInfo = value.lstInfo[i];
						if (this.IsPlayBreathVoicePtn(_female, breathVoicePtnInfo, _main))
						{
							list.AddRange((from inf in breathVoicePtnInfo.lstVoice
							orderby Guid.NewGuid()
							select inf).ToList<int>());
						}
					}
					list = (from inf in list
					orderby Guid.NewGuid()
					select inf).ToList<int>();
					if (list.Count == 0)
					{
						break;
					}
					int num = list[0];
					HVoiceCtrl.VoiceListInfo voiceListInfo = this.breathUseLists[_main].lstVoiceList[num];
					if (this.nowVoices[_main].state == HVoiceCtrl.VoiceKind.breath && (this.nowVoices[_main].breathInfo == voiceListInfo || this.nowVoices[_main].breathGroup == voiceListInfo.group) && this.isVoiceCheck(_main))
					{
						break;
					}
					HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
					Manager.Voice instance = Singleton<Manager.Voice>.Instance;
					int no = this.personality[_main];
					string pathAsset = voiceListInfo.pathAsset;
					string nameFile = voiceListInfo.nameFile;
					float pitch = this.voicePitch[_main];
					Transform voiceTrans = voiceFlag.voiceTrs[_main];
					Transform transform = instance.OnecePlayChara(no, pathAsset, nameFile, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
					AudioSource component = transform.GetComponent<AudioSource>();
					if (this.masturbation || this.les)
					{
						Singleton<Sound>.Instance.AudioSettingData3DOnly(component, 2);
					}
					else
					{
						component.rolloffMode = AudioRolloffMode.Linear;
					}
					_female.SetVoiceTransform(transform);
					if (!voiceFlag.lstUseAsset.Contains(voiceListInfo.pathAsset))
					{
						voiceFlag.lstUseAsset.Add(voiceListInfo.pathAsset);
					}
					this.nowVoices[_main].breathInfo = voiceListInfo;
					this.nowVoices[_main].state = HVoiceCtrl.VoiceKind.breath;
					this.nowVoices[_main].animBreath = value.anim;
					this.nowVoices[_main].notOverWrite = this.nowVoices[_main].breathInfo.notOverWrite;
					this.nowVoices[_main].arrayBreath = num;
					this.nowVoices[_main].breathGroup = voiceListInfo.group;
					this.SetBreathFace(value, _main);
					if (voiceFlag.urines[_main] && voiceListInfo.urine)
					{
						voiceFlag.urines[_main] = false;
					}
					this.SetFace(this.nowVoices[_main].Face, _female, _main);
					break;
				}
			}
		}
		return true;
	}

	// Token: 0x0600516A RID: 20842 RVA: 0x00213CDC File Offset: 0x002120DC
	private bool IsPlayBreathVoicePtn(ChaControl _female, HVoiceCtrl.BreathVoicePtnInfo _lst, int _main)
	{
		return this.IsBreathPtnConditions(_female, _lst.lstConditions, _main) && this.IsBreathAnimationList(_lst.lstAnimeID, this.nowId);
	}

	// Token: 0x0600516B RID: 20843 RVA: 0x00213D0D File Offset: 0x0021210D
	private bool IsBreathAnimationList(List<int> _lstAnimList, int _idNow)
	{
		return _lstAnimList.Count == 0 || _lstAnimList.Contains(-1) || _lstAnimList.Contains(_idNow);
	}

	// Token: 0x0600516C RID: 20844 RVA: 0x00213D34 File Offset: 0x00212134
	private bool IsBreathPtnConditions(ChaControl _female, List<int> _lstConditions, int _main)
	{
		for (int i = 0; i < _lstConditions.Count; i++)
		{
			int num = _lstConditions[i];
			switch (num + 1)
			{
			case 1:
				if (this.hSceneManager.PersonalPhase[_main] > 1)
				{
					return false;
				}
				break;
			case 2:
				if (this.hSceneManager.PersonalPhase[_main] < 2)
				{
					return false;
				}
				break;
			case 3:
				if (this.hSceneManager.GetFlaverSkillLevel(2) >= this.IngoLimit)
				{
					return false;
				}
				break;
			case 4:
				if (this.hSceneManager.GetFlaverSkillLevel(2) < this.IngoLimit)
				{
					return false;
				}
				break;
			case 5:
				if (this.VoiceFlag().sleep)
				{
					return false;
				}
				break;
			case 6:
				if (!this.VoiceFlag().sleep)
				{
					return false;
				}
				break;
			case 7:
				if (this.ctrlFlag.nowSpeedStateFast)
				{
					return false;
				}
				break;
			case 8:
				if (!this.ctrlFlag.nowSpeedStateFast)
				{
					return false;
				}
				break;
			case 9:
				if (this.VoiceFlag().urines[_main])
				{
					return false;
				}
				break;
			case 10:
				if (!this.VoiceFlag().urines[_main])
				{
					return false;
				}
				break;
			}
		}
		return true;
	}

	// Token: 0x0600516D RID: 20845 RVA: 0x00213EAC File Offset: 0x002122AC
	private bool SetBreathFace(HVoiceCtrl.BreathPtn _ptn, int _main)
	{
		if (this.ctrlFlag.isGaugeHit)
		{
			if (this.nowVoices[_main].breathInfo.lstHitFace.Count > 0)
			{
				this.nowVoices[_main].Face = this.nowVoices[_main].breathInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].breathInfo.lstHitFace.Count)];
			}
		}
		else if (this.nowVoices[_main].breathInfo.lstNotHitFace.Count > 0)
		{
			this.nowVoices[_main].Face = this.nowVoices[_main].breathInfo.lstNotHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].breathInfo.lstNotHitFace.Count)];
		}
		this.nowVoices[_main].timeFaceDelta = 0f;
		this.nowVoices[_main].timeFace = UnityEngine.Random.Range(_ptn.timeChangeFaceMin, _ptn.timeChangeFaceMax);
		return true;
	}

	// Token: 0x0600516E RID: 20846 RVA: 0x00213FBC File Offset: 0x002123BC
	public bool VoiceProc(AnimatorStateInfo _ai, ChaControl _female, int _main)
	{
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		if (Singleton<HSceneManager>.Instance.EventKind == HSceneManager.HEvent.Yobai || !_female.visibleAll || _female.objBody == null)
		{
			voiceFlag.playVoices[_main] = false;
			return false;
		}
		int num = this.VoiceProcDetail(_ai, _female, true, _main);
		if (num == 3)
		{
			num = this.VoiceProcDetail(_ai, _female, false, _main);
		}
		if (num != 2)
		{
			voiceFlag.playVoices[_main] = false;
			if (num == 1)
			{
				voiceFlag.playShorts[_main] = -1;
			}
		}
		return num == 1;
	}

	// Token: 0x0600516F RID: 20847 RVA: 0x00214048 File Offset: 0x00212448
	public int VoiceProcDetail(AnimatorStateInfo _ai, ChaControl _female, bool _isFirst, int _main)
	{
		if (!_female.visibleAll || _female.objBody == null)
		{
			return 0;
		}
		int result = 0;
		HVoiceCtrl.VoiceAnimationPlayInfo voiceAnimationPlayInfo = this.playAnimation.GetAnimation(_ai.shortNameHash);
		if (voiceAnimationPlayInfo == null)
		{
			voiceAnimationPlayInfo = new HVoiceCtrl.VoiceAnimationPlayInfo();
			voiceAnimationPlayInfo.animationHash = _ai.shortNameHash;
			this.playAnimation.lstPlayInfo.Add(voiceAnimationPlayInfo);
		}
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		bool flag = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 4 && this.ctrlFlag.nowAnimationInfo.nInitiativeFemale != 0;
		if (this.lstVoicePtn == null || this.lstVoicePtn[_main] == null)
		{
			if (flag && this.nowVoices[0].state != HVoiceCtrl.VoiceKind.voice && this.nowVoices[0].state != HVoiceCtrl.VoiceKind.startVoice)
			{
				voiceFlag.dialog = false;
			}
			return 0;
		}
		if (this.nowVoices[_main].notOverWrite && this.isVoiceCheck(_main))
		{
			return 2;
		}
		if (this.dicdiclstVoiceList[_main] == null || this.dicdiclstVoiceList[_main].Count == 0)
		{
			return 0;
		}
		int id = this.ctrlFlag.nowAnimationInfo.id;
		bool flag2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3;
		flag2 &= (id == 3 || id == 13 || id == 14);
		for (int i = 0; i < this.lstVoicePtn[_main].Count; i++)
		{
			HVoiceCtrl.VoicePtn voicePtn = this.lstVoicePtn[_main][i];
			if (this.hSceneManager.bMerchant || this.VoicePtnCondition(voicePtn.condition, _main))
			{
				if (this.VoicePtnStartEvent(voicePtn.startKind, _female, _main))
				{
					if (voicePtn.howTalk == 0)
					{
						if (_ai.IsName(voicePtn.anim))
						{
							List<int> lstCategory = new List<int>();
							bool flag3 = voicePtn.anim == "WLoop" || voicePtn.anim == "MLoop" || voicePtn.anim == "SLoop" || voicePtn.anim == "OLoop";
							if (!flag2)
							{
								if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3)
								{
									bool flag4 = false;
									if (voicePtn.lstInfo.Count > 0)
									{
										for (int j = 0; j < voicePtn.lstInfo.Count; j++)
										{
											flag4 |= this.VoiceAnimationList(voicePtn.lstInfo[j].lstAnimList, this.nowId);
											if (flag4)
											{
												break;
											}
										}
									}
									if (!flag4)
									{
										goto IL_BFD;
									}
								}
								if (voicePtn.LookFlag)
								{
									if (!voiceFlag.playVoices[_main])
									{
										break;
									}
								}
								else if (voiceAnimationPlayInfo.isPlays[_main])
								{
									break;
								}
							}
							else if (flag3)
							{
								if (!voicePtn.LookFlag)
								{
									goto IL_BFD;
								}
								if (!voiceFlag.playVoices[_main])
								{
									break;
								}
							}
							else if (voicePtn.LookFlag)
							{
								if (!voiceFlag.playVoices[_main])
								{
									break;
								}
							}
							else if (voiceAnimationPlayInfo.isPlays[_main])
							{
								break;
							}
							List<HVoiceCtrl.PlayVoiceinfo> list = (from inf in this.GetPlayListNum(voicePtn.lstInfo, this.dicdiclstVoiceList[_main], ref lstCategory, _female, this.playAnimation, _main)
							orderby Guid.NewGuid()
							select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
							if (list.Count == 0)
							{
								this.InitListPlayFlag(voicePtn.lstInfo, this.dicdiclstVoiceList[_main], lstCategory);
								list = (from inf in this.GetPlayListNum(voicePtn.lstInfo, this.dicdiclstVoiceList[_main], ref lstCategory, _female, this.playAnimation, _main)
								orderby Guid.NewGuid()
								select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
								if (list.Count == 0)
								{
									result = 3;
									goto IL_BFD;
								}
							}
							int voiceID = list[0].voiceID;
							int mode = list[0].mode;
							int kind = list[0].kind;
							if (this.dicdiclstVoiceList[_main].Count == 0)
							{
								break;
							}
							if (!this.dicdiclstVoiceList[_main].ContainsKey(mode) || !this.dicdiclstVoiceList[_main][mode].ContainsKey(kind))
							{
								break;
							}
							if (this.dicdiclstVoiceList[_main][mode][kind].dicdicVoiceList.Count == 0 || !this.dicdiclstVoiceList[_main][mode][kind].dicdicVoiceList.ContainsKey(voiceID))
							{
								break;
							}
							HVoiceCtrl.VoiceListInfo voiceListInfo = this.dicdiclstVoiceList[_main][mode][kind].dicdicVoiceList[voiceID];
							Manager.Voice instance = Singleton<Manager.Voice>.Instance;
							int no = this.personality[_main];
							string text = voiceListInfo.pathAsset;
							string text2 = voiceListInfo.nameFile;
							float pitch = this.voicePitch[_main];
							Transform voiceTrans = voiceFlag.voiceTrs[_main];
							Transform transform = instance.OnecePlayChara(no, text, text2, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
							AudioSource component = transform.GetComponent<AudioSource>();
							if (this.masturbation || this.les)
							{
								Singleton<Sound>.Instance.AudioSettingData3DOnly(component, 2);
							}
							else
							{
								component.rolloffMode = AudioRolloffMode.Linear;
							}
							_female.SetVoiceTransform(transform);
							if (!voiceFlag.lstUseAsset.Contains(voiceListInfo.pathAsset))
							{
								voiceFlag.lstUseAsset.Add(voiceListInfo.pathAsset);
							}
							this.nowVoices[_main].voiceInfo = voiceListInfo;
							this.nowVoices[_main].state = HVoiceCtrl.VoiceKind.voice;
							this.nowVoices[_main].animVoice = voicePtn.anim;
							this.nowVoices[_main].notOverWrite = this.nowVoices[_main].voiceInfo.notOverWrite;
							this.nowVoices[_main].arrayVoice = voiceID;
							this.nowVoices[_main].VoiceListID = mode;
							this.nowVoices[_main].VoiceListSheetID = kind;
							this.nowVoices[_main].voiceInfo.isPlay = true;
							if (this.nowVoices[_main].voiceInfo.lstHitFace.Count > 0)
							{
								this.nowVoices[_main].Face = this.nowVoices[_main].voiceInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].voiceInfo.lstHitFace.Count)];
							}
							voiceAnimationPlayInfo.isPlays[_main] = true;
							result = 1;
							this.SetFace(this.nowVoices[_main].Face, _female, _main);
							voiceFlag.dialog = false;
							break;
						}
					}
					else
					{
						if (flag)
						{
							if (voicePtn.howTalk == 1 && (voiceFlag.dialog || _main == 1))
							{
								goto IL_BFD;
							}
							if (voicePtn.howTalk == 2 && (!voiceFlag.dialog || _main == 0))
							{
								goto IL_BFD;
							}
						}
						else
						{
							if (voicePtn.howTalk == 1 && (voiceFlag.dialog || _main == 0))
							{
								goto IL_BFD;
							}
							if (voicePtn.howTalk == 2 && (!voiceFlag.dialog || _main == 1))
							{
								goto IL_BFD;
							}
						}
						if (_ai.IsName(voicePtn.anim))
						{
							List<int> list2 = new List<int>();
							if (voicePtn.LookFlag)
							{
								if ((!flag && _main == 1) || (flag && _main == 0))
								{
									if (!voiceFlag.playVoices[_main])
									{
										break;
									}
									if (voiceAnimationPlayInfo.isPlays[_main])
									{
										goto IL_BFD;
									}
								}
							}
							else if (voiceAnimationPlayInfo.isPlays[_main])
							{
								break;
							}
							if (this.nowVoices[_main ^ 1].state == HVoiceCtrl.VoiceKind.voice || this.nowVoices[_main ^ 1].state == HVoiceCtrl.VoiceKind.startVoice)
							{
								break;
							}
							List<HVoiceCtrl.PlayVoiceinfo> list = (from inf in this.GetPlayListNum(voicePtn.lstInfo, this.dicdiclstVoiceList[_main], ref list2, _female, this.playAnimation, _main)
							orderby Guid.NewGuid()
							select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
							if (list.Count == 0 && list.Count == 0)
							{
								result = 3;
							}
							else
							{
								int voiceID2 = list[0].voiceID;
								int mode2 = list[0].mode;
								int kind2 = list[0].kind;
								if (this.dicdiclstVoiceList[_main].Count == 0)
								{
									break;
								}
								if (!this.dicdiclstVoiceList[_main].ContainsKey(mode2) || !this.dicdiclstVoiceList[_main][mode2].ContainsKey(kind2))
								{
									break;
								}
								if (this.dicdiclstVoiceList[_main][mode2][kind2].dicdicVoiceList.Count == 0 || !this.dicdiclstVoiceList[_main][mode2][kind2].dicdicVoiceList.ContainsKey(voiceID2))
								{
									break;
								}
								HVoiceCtrl.VoiceListInfo voiceListInfo2 = this.dicdiclstVoiceList[_main][mode2][kind2].dicdicVoiceList[voiceID2];
								Manager.Voice instance2 = Singleton<Manager.Voice>.Instance;
								int no = this.personality[_main];
								string text2 = voiceListInfo2.pathAsset;
								string text = voiceListInfo2.nameFile;
								float pitch = this.voicePitch[_main];
								Transform voiceTrans = voiceFlag.voiceTrs[_main];
								Transform transform2 = instance2.OnecePlayChara(no, text2, text, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
								AudioSource component2 = transform2.GetComponent<AudioSource>();
								if (this.masturbation || this.les)
								{
									Singleton<Sound>.Instance.AudioSettingData3DOnly(component2, 2);
								}
								else
								{
									component2.rolloffMode = AudioRolloffMode.Linear;
								}
								_female.SetVoiceTransform(transform2);
								if (!voiceFlag.lstUseAsset.Contains(voiceListInfo2.pathAsset))
								{
									voiceFlag.lstUseAsset.Add(voiceListInfo2.pathAsset);
								}
								this.nowVoices[_main].voiceInfo = voiceListInfo2;
								this.nowVoices[_main].state = HVoiceCtrl.VoiceKind.voice;
								this.nowVoices[_main].animVoice = voicePtn.anim;
								this.nowVoices[_main].notOverWrite = this.nowVoices[_main].voiceInfo.notOverWrite;
								this.nowVoices[_main].arrayVoice = voiceID2;
								this.nowVoices[_main].VoiceListID = mode2;
								this.nowVoices[_main].VoiceListSheetID = kind2;
								this.nowVoices[_main].voiceInfo.isPlay = true;
								if (this.nowVoices[_main].voiceInfo.lstHitFace.Count > 0)
								{
									this.nowVoices[_main].Face = this.nowVoices[_main].voiceInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].voiceInfo.lstHitFace.Count)];
								}
								voiceAnimationPlayInfo.isPlays[_main] = true;
								result = 1;
								this.SetFace(this.nowVoices[_main].Face, _female, _main);
								voiceFlag.dialog = (voicePtn.howTalk == 1);
								break;
							}
						}
					}
				}
			}
			IL_BFD:;
		}
		return result;
	}

	// Token: 0x06005170 RID: 20848 RVA: 0x00214C70 File Offset: 0x00213070
	private List<HVoiceCtrl.PlayVoiceinfo> GetPlayListNum(List<HVoiceCtrl.VoicePtnInfo> _lst, Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>> _lstVoice, ref List<int> _lstCategory, ChaControl female, HVoiceCtrl.VoiceAnimationPlay nowVoiceInfo, int _main)
	{
		List<HVoiceCtrl.PlayVoiceinfo> list = new List<HVoiceCtrl.PlayVoiceinfo>();
		for (int i = 0; i < _lst.Count; i++)
		{
			if (this.VoiceAnimationList(_lst[i].lstAnimList, this.nowId))
			{
				if (this.ctrlFlag.initiative == 0 || _lst[i].lstPlayConditions.Contains(13))
				{
					if (this.VoicePtnConditions(_lst[i].lstPlayConditions, female, nowVoiceInfo, _main))
					{
						_lstCategory.Add(i);
						list.AddRange(this.GetPlayNum(_lst[i], _lstVoice));
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06005171 RID: 20849 RVA: 0x00214D28 File Offset: 0x00213128
	private bool VoicePtnStartEvent(int EventPtn, ChaControl _female, int _main)
	{
		if (EventPtn != -1)
		{
			if (EventPtn != 0)
			{
				if (EventPtn == 1)
				{
					if (!(_female != null) || this.hSceneManager.Mood[_main] >= _female.fileGameInfo.moodBound.lower)
					{
						if (this.hSceneManager.EventKind == HSceneManager.HEvent.Normal || this.hSceneManager.EventKind == HSceneManager.HEvent.GyakuYobai || this.hSceneManager.EventKind == HSceneManager.HEvent.FromFemale)
						{
							return false;
						}
					}
				}
			}
			else
			{
				if (_female != null && this.hSceneManager.Mood[_main] < _female.fileGameInfo.moodBound.lower && this.hSceneManager.PersonalPhase[_main] >= 2)
				{
					return false;
				}
				if (this.hSceneManager.EventKind != HSceneManager.HEvent.Normal && this.hSceneManager.EventKind != HSceneManager.HEvent.GyakuYobai && this.hSceneManager.EventKind != HSceneManager.HEvent.FromFemale)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06005172 RID: 20850 RVA: 0x00214E40 File Offset: 0x00213240
	private bool VoicePtnCondition(int _condition, int _main)
	{
		if (this.ctrlFlag.isFaintness)
		{
			return _condition == 4;
		}
		switch (_condition)
		{
		case 0:
			if (this.hSceneManager.PersonalPhase[_main] >= 2)
			{
				return false;
			}
			break;
		case 1:
			if (this.hSceneManager.PersonalPhase[_main] < 2)
			{
				return false;
			}
			break;
		case 4:
			return false;
		}
		return true;
	}

	// Token: 0x06005173 RID: 20851 RVA: 0x00214EC8 File Offset: 0x002132C8
	private bool VoicePtnConditionAccurate(int _condition, int _main)
	{
		switch (_condition)
		{
		case 0:
			if (this.hSceneManager.PersonalPhase[_main] >= 2 || this.hSceneManager.isForce || this.ctrlFlag.isFaintness)
			{
				return false;
			}
			break;
		case 1:
			if (this.hSceneManager.PersonalPhase[_main] < 2 || this.hSceneManager.isForce || this.ctrlFlag.isFaintness)
			{
				return false;
			}
			break;
		case 2:
			if (!this.hSceneManager.isForce || this.ctrlFlag.isFaintness)
			{
				return false;
			}
			break;
		case 3:
			if (this.ctrlFlag.isFaintness)
			{
				return false;
			}
			break;
		case 4:
			if (!this.ctrlFlag.isFaintness)
			{
				return false;
			}
			break;
		}
		return true;
	}

	// Token: 0x06005174 RID: 20852 RVA: 0x00214FBE File Offset: 0x002133BE
	private bool VoiceAnimationList(List<int> _lstAnimList, int _idNow)
	{
		return _lstAnimList.Count != 0 && (_lstAnimList.Contains(-1) || _lstAnimList.Contains(_idNow));
	}

	// Token: 0x06005175 RID: 20853 RVA: 0x00214FE4 File Offset: 0x002133E4
	private bool VoicePtnConditions(List<int> _lstConditions, ChaControl female, HVoiceCtrl.VoiceAnimationPlay nowVoiceInfo, int _main)
	{
		if (_lstConditions.Contains(13) && this.ctrlFlag.initiative == 0)
		{
			return false;
		}
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		for (int i = 0; i < _lstConditions.Count; i++)
		{
			int num = _lstConditions[i];
			switch (num + 1)
			{
			case 1:
				if (this.ctrlFlag.StartHouchiTime > this.HouchiTime)
				{
					return false;
				}
				break;
			case 2:
				if (female.GetBustSizeKind() != 0)
				{
					return false;
				}
				break;
			case 3:
				if (female.GetBustSizeKind() != 2)
				{
					return false;
				}
				break;
			case 4:
				if (female.GetBustSizeKind() != 1)
				{
					return false;
				}
				break;
			case 5:
				if (nowVoiceInfo == null || nowVoiceInfo.Count == 0)
				{
					return false;
				}
				break;
			case 6:
				if (nowVoiceInfo == null || nowVoiceInfo.Count < 1)
				{
					return false;
				}
				break;
			case 7:
				if (this.hSceneManager.GetFlaverSkillLevel(2) < this.IngoLimit)
				{
					return false;
				}
				break;
			case 8:
				if (this.hSceneManager.GetFlaverSkillLevel(2) >= this.IngoLimit)
				{
					return false;
				}
				break;
			case 9:
				if (_main != 0)
				{
					return false;
				}
				break;
			case 10:
				if (_main != 1)
				{
					return false;
				}
				break;
			case 11:
				if (voiceFlag.oldFinish != 0)
				{
					return false;
				}
				break;
			case 12:
				if (voiceFlag.oldFinish != 2)
				{
					return false;
				}
				break;
			case 13:
				if (voiceFlag.oldFinish != 3)
				{
					return false;
				}
				break;
			case 15:
				if (voiceFlag.oldFinish != 1)
				{
					return false;
				}
				break;
			}
		}
		if (_lstConditions.Contains(0) && this.nowMode < 4)
		{
			this.HouchiTime = 0f;
		}
		return true;
	}

	// Token: 0x06005176 RID: 20854 RVA: 0x002151D8 File Offset: 0x002135D8
	private List<HVoiceCtrl.PlayVoiceinfo> GetPlayNum(HVoiceCtrl.VoicePtnInfo _lstPlay, Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>> _lstVoice)
	{
		List<HVoiceCtrl.PlayVoiceinfo> list = new List<HVoiceCtrl.PlayVoiceinfo>();
		if (_lstVoice.Count == 0)
		{
			return list;
		}
		int loadListmode = _lstPlay.loadListmode;
		int loadListKind = _lstPlay.loadListKind;
		if (!_lstVoice.ContainsKey(loadListmode) || !_lstVoice[loadListmode].ContainsKey(loadListKind))
		{
			return list;
		}
		Dictionary<int, HVoiceCtrl.VoiceListInfo> dicdicVoiceList = _lstVoice[loadListmode][loadListKind].dicdicVoiceList;
		for (int i = 0; i < _lstPlay.lstVoice.Count; i++)
		{
			if (!dicdicVoiceList.ContainsKey(_lstPlay.lstVoice[i]))
			{
				GlobalMethod.DebugLog("再生しようとしている番号がリストにない", 1);
			}
			else
			{
				HVoiceCtrl.VoiceListInfo voiceListInfo = dicdicVoiceList[_lstPlay.lstVoice[i]];
				if (voiceListInfo.pathAsset.IsNullOrEmpty() || voiceListInfo.nameFile.IsNullOrEmpty())
				{
					GlobalMethod.DebugLog("再生予定の番号のバンドルかファイル名がないので欠番?", 1);
				}
				else if (!dicdicVoiceList[_lstPlay.lstVoice[i]].isPlay)
				{
					HVoiceCtrl.PlayVoiceinfo item;
					item.mode = loadListmode;
					item.kind = loadListKind;
					item.voiceID = _lstPlay.lstVoice[i];
					list.Add(item);
				}
			}
		}
		return list;
	}

	// Token: 0x06005177 RID: 20855 RVA: 0x00215318 File Offset: 0x00213718
	private bool InitListPlayFlag(List<HVoiceCtrl.VoicePtnInfo> _lst, Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>> _lstVoice, List<int> _lstCategory)
	{
		for (int i = 0; i < _lstCategory.Count; i++)
		{
			if (_lst.Count > _lstCategory[i])
			{
				int loadListmode = _lst[_lstCategory[i]].loadListmode;
				int loadListKind = _lst[_lstCategory[i]].loadListKind;
				for (int j = 0; j < _lst[_lstCategory[i]].lstVoice.Count; j++)
				{
					int key = _lst[_lstCategory[i]].lstVoice[j];
					if (_lstVoice.ContainsKey(loadListmode) && _lstVoice[loadListmode].ContainsKey(loadListKind))
					{
						if (!_lstVoice[loadListmode][loadListKind].dicdicVoiceList.ContainsKey(key))
						{
							GlobalMethod.DebugLog("再生しようとしている番号がリストにない", 1);
						}
						else
						{
							_lstVoice[loadListmode][loadListKind].dicdicVoiceList[key].isPlay = false;
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005178 RID: 20856 RVA: 0x0021542C File Offset: 0x0021382C
	private bool StartVoiceProc(ChaControl[] _females, int _nFemale)
	{
		bool result = false;
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		if (_females[_nFemale] == null || _females[_nFemale].objBody == null)
		{
			if (voiceFlag.playStart != -1 && _nFemale == 0)
			{
				voiceFlag.playStart = -1;
			}
			return false;
		}
		List<HVoiceCtrl.StartVoicePtn> list = this.lstLoadStartVoicePtn[_nFemale];
		if (list == null)
		{
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.nowVoices[i].notOverWrite && this.isVoiceCheck(_nFemale))
			{
				return result;
			}
		}
		if (_nFemale == 0 && voiceFlag.playStart > 4 && (this.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice))
		{
			return result;
		}
		if (this.dicdiclstVoiceList[_nFemale] == null)
		{
			return false;
		}
		for (int j = 0; j < list.Count; j++)
		{
			HVoiceCtrl.StartVoicePtn startVoicePtn = list[j];
			if (startVoicePtn.nTaii == this.nowMode)
			{
				if (this.hSceneManager.bMerchant || this.VoicePtnCondition(startVoicePtn.condition, _nFemale))
				{
					if (startVoicePtn.timing == voiceFlag.playStart)
					{
						if (startVoicePtn.nForce != -1)
						{
							if (startVoicePtn.nForce == 0)
							{
								if (this.hSceneManager.isForce)
								{
									goto IL_759;
								}
								if (this.hSceneManager.EventKind != HSceneManager.HEvent.Normal && this.hSceneManager.EventKind != HSceneManager.HEvent.GyakuYobai && this.hSceneManager.EventKind != HSceneManager.HEvent.FromFemale)
								{
									goto IL_759;
								}
							}
							else
							{
								if (this.MapHID >= 0 || !this.hSceneManager.isForce)
								{
									goto IL_759;
								}
								if (startVoicePtn.nForce == 2 && Singleton<HSceneFlagCtrl>.Instance.nPlace != 15)
								{
									goto IL_759;
								}
								if (startVoicePtn.nForce == 3 && Singleton<HSceneFlagCtrl>.Instance.nPlace != 12)
								{
									goto IL_759;
								}
								if (startVoicePtn.nForce == 5 && this.hSceneManager.Mood[_nFemale] > _females[_nFemale].fileGameInfo.moodBound.lower)
								{
									goto IL_759;
								}
								if (startVoicePtn.nForce == 4 && this.hSceneManager.Agent[0].BehaviorResources.Mode != Desire.ActionType.EndTaskMasturbation)
								{
									goto IL_759;
								}
							}
						}
						List<int> lstCategory = new List<int>();
						List<HVoiceCtrl.PlayVoiceinfo> list2 = (from inf in this.GetPlayListNum(startVoicePtn.lstInfo, this.dicdiclstVoiceList[_nFemale], ref lstCategory, _nFemale, _females[_nFemale])
						orderby Guid.NewGuid()
						select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
						if (list2.Count == 0)
						{
							this.InitGetPlayListNum(startVoicePtn.lstInfo, this.dicdiclstVoiceList[_nFemale], lstCategory);
							list2 = (from inf in this.GetPlayListNum(startVoicePtn.lstInfo, this.dicdiclstVoiceList[_nFemale], ref lstCategory, _nFemale, _females[_nFemale])
							orderby Guid.NewGuid()
							select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
							if (list2.Count == 0)
							{
								if (j == list.Count - 1)
								{
									if (_nFemale == 0 && voiceFlag.playStart < 5)
									{
										voiceFlag.playStart = -1;
									}
									break;
								}
								goto IL_759;
							}
						}
						if (startVoicePtn.timing == 4 && this.ctrlFlag.nowAnimationInfo.nInitiativeFemale != 0)
						{
							voiceFlag.playStart = -1;
							break;
						}
						int mode = list2[0].mode;
						int kind = list2[0].kind;
						int voiceID = list2[0].voiceID;
						if (!this.dicdiclstVoiceList[_nFemale][mode][kind].dicdicVoiceList.ContainsKey(voiceID))
						{
							GlobalMethod.DebugLog("配列外してる[" + _nFemale + "]", 1);
							break;
						}
						if (Singleton<HSceneManager>.Instance.EventKind == HSceneManager.HEvent.Yobai)
						{
							break;
						}
						HVoiceCtrl.VoiceListInfo voiceListInfo = this.dicdiclstVoiceList[_nFemale][mode][kind].dicdicVoiceList[voiceID];
						Manager.Voice instance = Singleton<Manager.Voice>.Instance;
						int no = this.personality[_nFemale];
						string pathAsset = voiceListInfo.pathAsset;
						string nameFile = voiceListInfo.nameFile;
						float pitch = this.voicePitch[_nFemale];
						Transform voiceTrans = voiceFlag.voiceTrs[_nFemale];
						Transform transform = instance.OnecePlayChara(no, pathAsset, nameFile, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
						AudioSource component = transform.GetComponent<AudioSource>();
						if (this.masturbation || this.les)
						{
							Singleton<Sound>.Instance.AudioSettingData3DOnly(component, 2);
						}
						else
						{
							component.rolloffMode = AudioRolloffMode.Linear;
						}
						_females[_nFemale].SetVoiceTransform(transform);
						if (!voiceFlag.lstUseAsset.Contains(voiceListInfo.pathAsset))
						{
							voiceFlag.lstUseAsset.Add(voiceListInfo.pathAsset);
						}
						this.nowVoices[_nFemale].voiceInfo = voiceListInfo;
						if (startVoicePtn.timing != 0 && startVoicePtn.timing < 4)
						{
							HVoiceCtrl.VoiceKind[] array = new HVoiceCtrl.VoiceKind[]
							{
								HVoiceCtrl.VoiceKind.startVoice,
								HVoiceCtrl.VoiceKind.startVoice,
								HVoiceCtrl.VoiceKind.voice
							};
							this.nowVoices[_nFemale].state = array[startVoicePtn.timing - 1];
							if (_nFemale == 1)
							{
								voiceFlag.playStart += 4;
								if (this.nowVoices[0].state != HVoiceCtrl.VoiceKind.breath && this.nowVoices[0].state != HVoiceCtrl.VoiceKind.breathShort)
								{
									Singleton<Manager.Voice>.Instance.Stop(voiceFlag.voiceTrs[_nFemale ^ 1]);
								}
							}
							else
							{
								voiceFlag.playStart = -1;
							}
						}
						else if (startVoicePtn.timing == 0)
						{
							this.nowVoices[_nFemale].state = HVoiceCtrl.VoiceKind.startVoice;
							voiceFlag.playStart = -1;
						}
						else
						{
							if (startVoicePtn.timing == 5 || startVoicePtn.timing == 6)
							{
								this.nowVoices[_nFemale].state = HVoiceCtrl.VoiceKind.startVoice;
							}
							else
							{
								this.nowVoices[_nFemale].state = HVoiceCtrl.VoiceKind.voice;
							}
							if (_nFemale == 1 && this.nowVoices[0].state != HVoiceCtrl.VoiceKind.breath && this.nowVoices[0].state != HVoiceCtrl.VoiceKind.breathShort)
							{
								Singleton<Manager.Voice>.Instance.Stop(voiceFlag.voiceTrs[_nFemale ^ 1]);
							}
							voiceFlag.playStart = -1;
						}
						this.nowVoices[_nFemale].animVoice = startVoicePtn.anim;
						this.nowVoices[_nFemale].notOverWrite = this.nowVoices[_nFemale].voiceInfo.notOverWrite;
						this.nowVoices[_nFemale].arrayVoice = voiceID;
						this.nowVoices[_nFemale].VoiceListID = mode;
						this.nowVoices[_nFemale].VoiceListSheetID = kind;
						if (this.nowVoices[_nFemale].voiceInfo.lstHitFace.Count > 0)
						{
							this.nowVoices[_nFemale].Face = this.nowVoices[_nFemale].voiceInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_nFemale].voiceInfo.lstHitFace.Count)];
						}
						this.nowVoices[_nFemale].voiceInfo.isPlay = true;
						result = true;
						this.SetFace(this.nowVoices[_nFemale].Face, _females[_nFemale], _nFemale);
						break;
					}
				}
			}
			IL_759:;
		}
		return result;
	}

	// Token: 0x06005179 RID: 20857 RVA: 0x00215BA8 File Offset: 0x00213FA8
	private List<HVoiceCtrl.PlayVoiceinfo> GetPlayListNum(List<HVoiceCtrl.VoicePtnInfo> _lst, Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>> _lstVoice, ref List<int> _lstCategory, int _main, ChaControl female)
	{
		List<HVoiceCtrl.PlayVoiceinfo> list = new List<HVoiceCtrl.PlayVoiceinfo>();
		for (int i = 0; i < _lst.Count; i++)
		{
			if (_lst[i].lstAnimList[0] == -1 || _lst[i].lstAnimList.Contains(this.ctrlFlag.nowAnimationInfo.id))
			{
				if (this.StartVoicePtnConditions(_lst[i].lstPlayConditions, female, _main))
				{
					list.AddRange(this.GetPlayNum(_lst[i], _lstVoice));
					_lstCategory.Add(i);
				}
			}
		}
		return list;
	}

	// Token: 0x0600517A RID: 20858 RVA: 0x00215C54 File Offset: 0x00214054
	private bool StartVoicePtnConditions(List<int> conditions, ChaControl female, int _main)
	{
		if (conditions.Contains(7) && this.ctrlFlag.initiative == 0)
		{
			return false;
		}
		for (int i = 0; i < conditions.Count; i++)
		{
			int num = conditions[i];
			switch (num + 1)
			{
			case 1:
				if (female.GetBustSizeKind() != 0)
				{
					return false;
				}
				break;
			case 2:
				if (female.GetBustSizeKind() != 2)
				{
					return false;
				}
				break;
			case 3:
				if (female.GetBustSizeKind() != 1)
				{
					return false;
				}
				break;
			case 4:
				if (this.hSceneManager.GetFlaverSkillLevel(2) < this.IngoLimit)
				{
					return false;
				}
				break;
			case 5:
				if (this.hSceneManager.GetFlaverSkillLevel(2) >= this.IngoLimit)
				{
					return false;
				}
				break;
			case 6:
				if (_main != 0)
				{
					return false;
				}
				break;
			case 7:
				if (_main != 1)
				{
					return false;
				}
				break;
			}
		}
		return true;
	}

	// Token: 0x0600517B RID: 20859 RVA: 0x00215D60 File Offset: 0x00214160
	private void InitGetPlayListNum(List<HVoiceCtrl.VoicePtnInfo> _lst, Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>> _lstVoice, List<int> _lstCategory)
	{
		for (int i = 0; i < _lstCategory.Count; i++)
		{
			if (_lst.Count <= _lstCategory[i])
			{
				GlobalMethod.DebugLog("開始音声のカテゴリ取得に失敗(フラグ再設定時)", 1);
			}
			else
			{
				int loadListmode = _lst[_lstCategory[i]].loadListmode;
				int loadListKind = _lst[_lstCategory[i]].loadListKind;
				for (int j = 0; j < _lst[_lstCategory[i]].lstVoice.Count; j++)
				{
					if (!_lstVoice.ContainsKey(loadListmode) || !_lstVoice[loadListmode].ContainsKey(loadListKind))
					{
						GlobalMethod.DebugLog("開始音声の再生番号取得に失敗(フラグ再設定時)", 1);
					}
					else
					{
						int key = _lst[_lstCategory[i]].lstVoice[j];
						if (!_lstVoice[loadListmode][loadListKind].dicdicVoiceList.ContainsKey(key))
						{
							GlobalMethod.DebugLog("開始音声の再生番号取得に失敗(フラグ再設定時)", 1);
						}
						else
						{
							_lstVoice[loadListmode][loadListKind].dicdicVoiceList[key].isPlay = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600517C RID: 20860 RVA: 0x00215E8C File Offset: 0x0021428C
	private List<HVoiceCtrl.PlayVoiceinfo> GetHBeforePlayListNum(List<HVoiceCtrl.VoicePtnInfo> _lst, Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>> _lstVoice, ref List<int> _lstCategory, int _main, ChaControl female)
	{
		List<HVoiceCtrl.PlayVoiceinfo> list = new List<HVoiceCtrl.PlayVoiceinfo>();
		for (int i = 0; i < _lst.Count; i++)
		{
			if (this.StartVoicePtnConditions(_lst[i].lstPlayConditions, female, _main))
			{
				list.AddRange(this.GetPlayNum(_lst[i], _lstVoice));
				_lstCategory.Add(i);
			}
		}
		return list;
	}

	// Token: 0x0600517D RID: 20861 RVA: 0x00215EF4 File Offset: 0x002142F4
	public void HBeforeProc(ChaControl[] _females)
	{
		this.isPlays[0] = false;
		this.isPlays[1] = false;
		for (int i = 1; i > -1; i--)
		{
			int num = i;
			if (_females.Length != 1 && !(_females[num] == null))
			{
				this.isPlays[num] = this.HBeforeProc(_females, num);
			}
		}
	}

	// Token: 0x0600517E RID: 20862 RVA: 0x00215F54 File Offset: 0x00214354
	private bool HBeforeProc(ChaControl[] _females, int _nFemale)
	{
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		if (_females[_nFemale] == null || _females[_nFemale].objBody == null)
		{
			if (voiceFlag.playStart != -1 && _nFemale == 0)
			{
				voiceFlag.playStart = -1;
			}
			return false;
		}
		List<HVoiceCtrl.StartVoicePtn> list = this.lstLoadStartVoicePtn[_nFemale];
		if (list == null)
		{
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.nowVoices[i].notOverWrite && this.isVoiceCheck(_nFemale))
			{
				return false;
			}
		}
		if (_nFemale == 0 && voiceFlag.playStart > 4 && (this.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice))
		{
			return false;
		}
		if (this.dicdiclstVoiceList[_nFemale] == null)
		{
			return false;
		}
		for (int j = 0; j < list.Count; j++)
		{
			HVoiceCtrl.StartVoicePtn startVoicePtn = list[j];
			if (startVoicePtn.timing == 0)
			{
				if (startVoicePtn.nForce != -1)
				{
					if (startVoicePtn.nForce == 0)
					{
						if (this.hSceneManager.EventKind != HSceneManager.HEvent.Normal && this.hSceneManager.EventKind != HSceneManager.HEvent.GyakuYobai && this.hSceneManager.EventKind != HSceneManager.HEvent.FromFemale)
						{
							goto IL_5EE;
						}
					}
					else
					{
						if (!this.hSceneManager.isForce)
						{
							goto IL_5EE;
						}
						if (startVoicePtn.nForce == 2 && Singleton<HSceneFlagCtrl>.Instance.nPlace != 15)
						{
							goto IL_5EE;
						}
						if (startVoicePtn.nForce == 3 && Singleton<HSceneFlagCtrl>.Instance.nPlace != 12)
						{
							goto IL_5EE;
						}
						if (startVoicePtn.nForce == 4 && this.hSceneManager.Agent[0].BehaviorResources.Mode != Desire.ActionType.EndTaskMasturbation)
						{
							goto IL_5EE;
						}
					}
				}
				if (this.hSceneManager.bMerchant || this.VoicePtnCondition(startVoicePtn.condition, _nFemale))
				{
					if (startVoicePtn.timing == voiceFlag.playStart)
					{
						List<int> lstCategory = new List<int>();
						List<HVoiceCtrl.PlayVoiceinfo> list2 = (from inf in this.GetHBeforePlayListNum(startVoicePtn.lstInfo, this.dicdiclstVoiceList[_nFemale], ref lstCategory, _nFemale, _females[_nFemale])
						orderby Guid.NewGuid()
						select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
						if (list2.Count == 0)
						{
							this.InitGetPlayListNum(startVoicePtn.lstInfo, this.dicdiclstVoiceList[_nFemale], lstCategory);
							list2 = (from inf in this.GetHBeforePlayListNum(startVoicePtn.lstInfo, this.dicdiclstVoiceList[_nFemale], ref lstCategory, _nFemale, _females[_nFemale])
							orderby Guid.NewGuid()
							select inf).ToList<HVoiceCtrl.PlayVoiceinfo>();
							if (list2.Count == 0)
							{
								if (j == list.Count - 1)
								{
									if (_nFemale == 0 && voiceFlag.playStart < 5)
									{
										voiceFlag.playStart = -1;
									}
									break;
								}
								goto IL_5EE;
							}
						}
						int mode = list2[0].mode;
						int kind = list2[0].kind;
						int voiceID = list2[0].voiceID;
						if (!this.dicdiclstVoiceList[_nFemale][mode][kind].dicdicVoiceList.ContainsKey(voiceID))
						{
							GlobalMethod.DebugLog("配列外してる[" + _nFemale + "]", 1);
							break;
						}
						if (Singleton<HSceneManager>.Instance.EventKind == HSceneManager.HEvent.Yobai)
						{
							break;
						}
						HVoiceCtrl.VoiceListInfo voiceListInfo = this.dicdiclstVoiceList[_nFemale][mode][kind].dicdicVoiceList[voiceID];
						Manager.Voice instance = Singleton<Manager.Voice>.Instance;
						int no = this.personality[_nFemale];
						string pathAsset = voiceListInfo.pathAsset;
						string nameFile = voiceListInfo.nameFile;
						float pitch = this.voicePitch[_nFemale];
						Transform voiceTrans = voiceFlag.voiceTrs[_nFemale];
						Transform transform = instance.OnecePlayChara(no, pathAsset, nameFile, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
						AudioSource component = transform.GetComponent<AudioSource>();
						if (this.masturbation || this.les)
						{
							Singleton<Sound>.Instance.AudioSettingData3DOnly(component, 2);
						}
						else
						{
							component.rolloffMode = AudioRolloffMode.Linear;
						}
						_females[_nFemale].SetVoiceTransform(transform);
						if (!voiceFlag.lstUseAsset.Contains(voiceListInfo.pathAsset))
						{
							voiceFlag.lstUseAsset.Add(voiceListInfo.pathAsset);
						}
						this.nowVoices[_nFemale].voiceInfo = voiceListInfo;
						this.nowVoices[_nFemale].state = HVoiceCtrl.VoiceKind.startVoice;
						if (_nFemale == 1)
						{
							voiceFlag.playStart += 5;
							if (this.nowVoices[0].state != HVoiceCtrl.VoiceKind.breath && this.nowVoices[0].state != HVoiceCtrl.VoiceKind.breathShort)
							{
								Singleton<Manager.Voice>.Instance.Stop(voiceFlag.voiceTrs[_nFemale ^ 1]);
							}
						}
						else
						{
							voiceFlag.playStart = -1;
						}
						this.nowVoices[_nFemale].animVoice = startVoicePtn.anim;
						this.nowVoices[_nFemale].notOverWrite = this.nowVoices[_nFemale].voiceInfo.notOverWrite;
						this.nowVoices[_nFemale].arrayVoice = voiceID;
						this.nowVoices[_nFemale].VoiceListID = mode;
						this.nowVoices[_nFemale].VoiceListSheetID = kind;
						if (this.nowVoices[_nFemale].voiceInfo.lstHitFace.Count > 0)
						{
							this.nowVoices[_nFemale].Face = this.nowVoices[_nFemale].voiceInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_nFemale].voiceInfo.lstHitFace.Count)];
						}
						this.nowVoices[_nFemale].voiceInfo.isPlay = true;
						this.SetFace(this.nowVoices[_nFemale].Face, _females[_nFemale], _nFemale);
						this.HBeforeHouchiTime = 0f;
						break;
					}
				}
			}
			IL_5EE:;
		}
		return true;
	}

	// Token: 0x0600517F RID: 20863 RVA: 0x00216560 File Offset: 0x00214960
	public bool ShortBreathProc(ChaControl _female, int _main)
	{
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		if (!_female.visibleAll || _female.objBody == null)
		{
			voiceFlag.playShorts[_main] = -1;
		}
		if (this.nowVoices[_main].notOverWrite && this.isVoiceCheck(_main))
		{
			voiceFlag.playShorts[_main] = -1;
		}
		if (Singleton<HSceneManager>.Instance.EventKind == HSceneManager.HEvent.Yobai)
		{
			voiceFlag.playShorts[_main] = -1;
		}
		if (this.dicShortBreathUsePtns[_main] == null)
		{
			voiceFlag.playShorts[_main] = -1;
		}
		if (voiceFlag.playShorts[_main] == -1)
		{
			return false;
		}
		if (this.dicShortBreathUsePtns[_main] != null && !this.dicShortBreathUsePtns[_main].ContainsKey(voiceFlag.playShorts[_main]))
		{
			return false;
		}
		List<HVoiceCtrl.BreathVoicePtnInfo> list = this.dicShortBreathUsePtns[_main][voiceFlag.playShorts[_main]];
		if (list.Count == 0)
		{
			voiceFlag.playShorts[_main] = -1;
			return false;
		}
		List<int> list2 = new List<int>();
		for (int i = 0; i < list.Count; i++)
		{
			if (this.IsPlayShortBreathVoicePtn(_female, list[i], _main))
			{
				list2.AddRange(list[i].lstVoice);
			}
		}
		if (list2.Count == 0)
		{
			return false;
		}
		list2 = (from inf in list2
		orderby Guid.NewGuid()
		select inf).ToList<int>();
		int num = list2[0];
		if (!this.ShortBreathLists[_main].dicShortBreathLists.ContainsKey(num))
		{
			voiceFlag.playShorts[_main] = -1;
			return false;
		}
		Dictionary<int, HVoiceCtrl.VoiceListInfo> dicShortBreathLists = this.ShortBreathLists[_main].dicShortBreathLists;
		Manager.Voice instance = Singleton<Manager.Voice>.Instance;
		int no = this.personality[_main];
		string pathAsset = dicShortBreathLists[num].pathAsset;
		string nameFile = dicShortBreathLists[num].nameFile;
		float pitch = this.voicePitch[_main];
		Transform voiceTrans = voiceFlag.voiceTrs[_main];
		Transform transform = instance.OnecePlayChara(no, pathAsset, nameFile, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
		AudioSource component = transform.GetComponent<AudioSource>();
		if (this.masturbation || this.les)
		{
			Singleton<Sound>.Instance.AudioSettingData3DOnly(component, 2);
		}
		else
		{
			component.rolloffMode = AudioRolloffMode.Linear;
		}
		_female.SetVoiceTransform(transform);
		if (!voiceFlag.lstUseAsset.Contains(dicShortBreathLists[num].pathAsset))
		{
			voiceFlag.lstUseAsset.Add(dicShortBreathLists[num].pathAsset);
		}
		this.nowVoices[_main].shortInfo = dicShortBreathLists[num];
		this.nowVoices[_main].state = HVoiceCtrl.VoiceKind.breathShort;
		this.nowVoices[_main].notOverWrite = this.nowVoices[_main].shortInfo.notOverWrite;
		this.nowVoices[_main].arrayShort = num;
		this.nowVoices[_main].Face = this.nowVoices[_main].shortInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].shortInfo.lstHitFace.Count)];
		voiceFlag.playShorts[_main] = -1;
		this.SetFace(this.nowVoices[_main].Face, _female, _main);
		return true;
	}

	// Token: 0x06005180 RID: 20864 RVA: 0x002168AE File Offset: 0x00214CAE
	private bool IsPlayShortBreathVoicePtn(ChaControl _female, HVoiceCtrl.BreathVoicePtnInfo _lst, int _main)
	{
		return this.CheckShortVoiceCondition(_female, _lst.lstConditions, _main) && this.IsShortBreathAnimationList(_lst.lstAnimeID, this.nowId);
	}

	// Token: 0x06005181 RID: 20865 RVA: 0x002168DF File Offset: 0x00214CDF
	private bool IsShortBreathAnimationList(List<int> _lstAnimList, int _idNow)
	{
		return _lstAnimList.Count == 0 || _lstAnimList.Contains(-1) || _lstAnimList.Contains(_idNow);
	}

	// Token: 0x06005182 RID: 20866 RVA: 0x00216904 File Offset: 0x00214D04
	private bool CheckShortVoiceCondition(ChaControl _female, List<int> _lstConditions, int _main)
	{
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		for (int i = 0; i < _lstConditions.Count; i++)
		{
			int num = _lstConditions[i];
			switch (num + 1)
			{
			case 1:
				if (this.hSceneManager.isForce || this.ctrlFlag.nowAnimationInfo.nInitiativeFemale != 0 || this.ctrlFlag.isFaintness || this.hSceneManager.PersonalPhase[_main] > 1)
				{
					return false;
				}
				break;
			case 2:
				if (this.hSceneManager.isForce || this.ctrlFlag.nowAnimationInfo.nInitiativeFemale != 0 || this.ctrlFlag.isFaintness || this.hSceneManager.PersonalPhase[_main] < 2)
				{
					return false;
				}
				break;
			case 3:
				if (!this.hSceneManager.isForce || this.ctrlFlag.isFaintness)
				{
					return false;
				}
				break;
			case 4:
				if (this.ctrlFlag.isFaintness || this.ctrlFlag.nowAnimationInfo.nInitiativeFemale == 0)
				{
					return false;
				}
				break;
			case 5:
				if (!this.ctrlFlag.isFaintness || voiceFlag.sleep)
				{
					return false;
				}
				break;
			case 6:
				if (!this.ctrlFlag.isFaintness || !voiceFlag.sleep)
				{
					return false;
				}
				break;
			case 7:
				if (this.hSceneManager.GetFlaverSkillLevel(2) >= this.IngoLimit)
				{
					return false;
				}
				break;
			case 8:
				if (this.hSceneManager.GetFlaverSkillLevel(2) < this.IngoLimit)
				{
					return false;
				}
				break;
			}
		}
		return true;
	}

	// Token: 0x06005183 RID: 20867 RVA: 0x00216AE0 File Offset: 0x00214EE0
	public void PlaySoundETC(string _animName, int _mode, ChaControl _female, int _main, bool _iya = false)
	{
		this.isPlays[_main] = true;
		if (Singleton<HSceneManager>.Instance.EventKind == HSceneManager.HEvent.Yobai && this.ctrlFlag.isFaintness)
		{
			this.isPlays[_main] = false;
			return;
		}
		HVoiceCtrl.VoiceAnimationPlayInfo animation = this.playAnimation.GetAnimation(Animator.StringToHash(_animName));
		if (_mode > 2)
		{
			this.isPlays[_main] = false;
			return;
		}
		HSceneFlagCtrl.VoiceFlag voiceFlag = this.VoiceFlag();
		if (_mode == 0)
		{
			int num;
			if (this.ctrlFlag.isFaintness)
			{
				num = 2;
			}
			else if (this.hSceneManager.Agent[_main].ChaControl.fileGameInfo.phase < 2)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
			if (!this.dicdiclstVoiceList[_main].ContainsKey(this.personality[_main]) || !this.dicdiclstVoiceList[_main][3].ContainsKey(5) || !this.dicdiclstVoiceList[_main][3][5].dicdicVoiceList.ContainsKey(num))
			{
				this.isPlays[_main] = false;
				return;
			}
			HVoiceCtrl.VoiceListInfo voiceListInfo = this.dicdiclstVoiceList[_main][3][5].dicdicVoiceList[num];
			Manager.Voice instance = Singleton<Manager.Voice>.Instance;
			int no = this.personality[_main];
			string text = voiceListInfo.pathAsset;
			string text2 = voiceListInfo.nameFile;
			float pitch = this.voicePitch[_main];
			Transform voiceTrans = voiceFlag.voiceTrs[_main];
			instance.OnecePlayChara(no, text, text2, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
			if (!voiceFlag.lstUseAsset.Contains(voiceListInfo.pathAsset))
			{
				voiceFlag.lstUseAsset.Add(voiceListInfo.pathAsset);
			}
			this.nowVoices[_main].voiceInfo = voiceListInfo;
			this.nowVoices[_main].state = HVoiceCtrl.VoiceKind.voice;
			this.nowVoices[_main].animVoice = _animName;
			this.nowVoices[_main].notOverWrite = this.nowVoices[_main].voiceInfo.notOverWrite;
			this.nowVoices[_main].arrayVoice = num;
			this.nowVoices[_main].Face = this.nowVoices[_main].voiceInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].voiceInfo.lstHitFace.Count)];
			this.nowVoices[_main].voiceInfo.isPlay = true;
			animation.isPlays[_main] = true;
			this.SetFace(this.nowVoices[_main].Face, _female, _main);
			voiceFlag.dialog = false;
		}
		else
		{
			int num;
			if (_mode == 1)
			{
				if (_iya)
				{
					num = 13;
				}
				else
				{
					num = 15;
				}
			}
			else if (_iya)
			{
				num = 14;
			}
			else
			{
				num = 16;
			}
			Dictionary<int, HVoiceCtrl.VoiceListInfo> dicShortBreathLists = this.ShortBreathLists[_main].dicShortBreathLists;
			if (!dicShortBreathLists.ContainsKey(num))
			{
				this.isPlays[_main] = false;
				return;
			}
			Manager.Voice instance2 = Singleton<Manager.Voice>.Instance;
			int no = this.personality[_main];
			string text2 = dicShortBreathLists[num].pathAsset;
			string text = dicShortBreathLists[num].nameFile;
			float pitch = this.voicePitch[_main];
			Transform voiceTrans = voiceFlag.voiceTrs[_main];
			instance2.OnecePlayChara(no, text2, text, pitch, 0f, 0f, true, voiceTrans, Manager.Voice.Type.PCM, -1, true, true, false);
			if (!voiceFlag.lstUseAsset.Contains(dicShortBreathLists[num].pathAsset))
			{
				voiceFlag.lstUseAsset.Add(dicShortBreathLists[num].pathAsset);
			}
			this.nowVoices[_main].shortInfo = dicShortBreathLists[num];
			this.nowVoices[_main].state = HVoiceCtrl.VoiceKind.breathShort;
			this.nowVoices[_main].notOverWrite = this.nowVoices[_main].shortInfo.notOverWrite;
			this.nowVoices[_main].arrayShort = num;
			this.nowVoices[_main].Face = this.nowVoices[_main].shortInfo.lstHitFace[UnityEngine.Random.Range(0, this.nowVoices[_main].shortInfo.lstHitFace.Count)];
			voiceFlag.playShorts[_main] = -1;
			this.SetFace(this.nowVoices[_main].Face, _female, _main);
		}
	}

	// Token: 0x06005184 RID: 20868 RVA: 0x00216F2C File Offset: 0x0021532C
	private bool isVoiceCheck(int id)
	{
		if (this.MapHID < 0)
		{
			return this.ctrlFlag.voice.voiceTrs[id] != null && Singleton<Manager.Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[id], true);
		}
		return this.ctrlFlag.MapHvoices.ContainsKey(this.MapHID) && this.ctrlFlag.MapHvoices[this.MapHID].voiceTrs[id] != null && Singleton<Manager.Voice>.Instance.IsVoiceCheck(this.ctrlFlag.MapHvoices[this.MapHID].voiceTrs[id], true);
	}

	// Token: 0x06005185 RID: 20869 RVA: 0x00216FF0 File Offset: 0x002153F0
	private HSceneFlagCtrl.VoiceFlag VoiceFlag()
	{
		if (this.MapHID < 0)
		{
			return this.ctrlFlag.voice;
		}
		if (this.ctrlFlag.MapHvoices.ContainsKey(this.MapHID))
		{
			return this.ctrlFlag.MapHvoices[this.MapHID];
		}
		return null;
	}

	// Token: 0x06005186 RID: 20870 RVA: 0x00217048 File Offset: 0x00215448
	private bool SetFace(HVoiceCtrl.FaceInfo _face, ChaControl _female, int _main)
	{
		FBSCtrlEyes eyesCtrl = _female.eyesCtrl;
		if (eyesCtrl != null)
		{
			this.blendEyes[_main].Start(eyesCtrl.OpenMax, _face.openEye, 0.3f);
		}
		FBSCtrlMouth mouthCtrl = _female.mouthCtrl;
		if (mouthCtrl != null)
		{
			this.blendMouths[_main].Start(mouthCtrl.OpenMin, _face.openMouthMin, 0.3f);
			this.blendMouthMaxs[_main].Start(mouthCtrl.OpenMax, _face.openMouthMax, 0.3f);
		}
		_female.ChangeEyebrowPtn(_face.eyeBlow, true);
		_female.ChangeEyesPtn(_face.eye, true);
		_female.ChangeMouthPtn(_face.mouth, true);
		if (_face.mouth == 10 || _face.mouth == 13)
		{
			_female.DisableShapeMouth(true);
		}
		else
		{
			_female.DisableShapeMouth(false);
		}
		_female.ChangeTearsRate(_face.tear);
		_female.ChangeHohoAkaRate(_face.cheek);
		_female.HideEyeHighlight(!_face.highlight);
		_female.ChangeEyesBlinkFlag(_face.blink);
		return true;
	}

	// Token: 0x06005187 RID: 20871 RVA: 0x00217154 File Offset: 0x00215554
	public IEnumerator Init(int _personality, float _pitch, Actor _param, int _personality_sub = 0, float _pitch_sub = 0f, Actor _param_sub = null, int merchant = -1, bool musterbation = false, bool les = false)
	{
		this.personality[0] = _personality;
		this.voicePitch[0] = _pitch;
		this.personality[1] = _personality_sub;
		this.voicePitch[1] = _pitch_sub;
		this.param = _param;
		this.param_sub = _param_sub;
		this.nowVoices[0] = new HVoiceCtrl.Voice();
		this.nowVoices[1] = new HVoiceCtrl.Voice();
		this.dicdiclstVoiceList[0] = new Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>>();
		this.dicdiclstVoiceList[1] = new Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>>();
		this.ShortBreathLists[0] = new HVoiceCtrl.ShortVoiceList();
		this.ShortBreathLists[1] = new HVoiceCtrl.ShortVoiceList();
		this.shortBreathPtns[0] = new HVoiceCtrl.ShortBreathPtn();
		this.shortBreathPtns[1] = new HVoiceCtrl.ShortBreathPtn();
		this.shortBreathAddPtns[0] = new HVoiceCtrl.ShortBreathPtn();
		this.shortBreathAddPtns[1] = new HVoiceCtrl.ShortBreathPtn();
		this.breathUseLists[0] = new HVoiceCtrl.BreathList();
		this.breathUseLists[1] = new HVoiceCtrl.BreathList();
		this.lstLoadVoicePtn[0] = new Dictionary<int, Dictionary<int, List<HVoiceCtrl.VoicePtn>>>();
		this.lstLoadVoicePtn[1] = new Dictionary<int, Dictionary<int, List<HVoiceCtrl.VoicePtn>>>();
		this.lstLoadStartVoicePtn[0] = new List<HVoiceCtrl.StartVoicePtn>();
		this.lstLoadStartVoicePtn[1] = new List<HVoiceCtrl.StartVoicePtn>();
		this.blendEyes[0] = new GlobalMethod.FloatBlend();
		this.blendEyes[1] = new GlobalMethod.FloatBlend();
		this.blendMouths[0] = new GlobalMethod.FloatBlend();
		this.blendMouths[1] = new GlobalMethod.FloatBlend();
		this.blendMouthMaxs[0] = new GlobalMethod.FloatBlend();
		this.blendMouthMaxs[1] = new GlobalMethod.FloatBlend();
		this.dicdicVoicePlayAnimation = new Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceAnimationPlay>>();
		for (int i = 0; i < 2; i++)
		{
			this.dicBreathPtns[i] = this.dicBreathPtns.New<int, int, int, int, int, string, HVoiceCtrl.BreathPtn>();
			this.dicBreathUsePtns[i] = this.dicBreathUsePtns.New<int, string, HVoiceCtrl.BreathPtn>();
			this.dicShortBreathUsePtns[i] = this.dicShortBreathUsePtns.New<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
			this.dicBreathAddPtns[i] = this.dicBreathAddPtns.New<int, int, int, int, int, string, HVoiceCtrl.BreathPtn>();
		}
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		for (int j = 0; j < this.breathLists.Length; j++)
		{
			this.breathLists[j] = new HVoiceCtrl.BreathList();
		}
		this.lstBreathAbnames.Clear();
		this.lstBreathAbnames = CommonLib.GetAssetBundleNameListFromPath(this.hSceneManager.strAssetBreathListFolder, false);
		this.masturbation = musterbation;
		this.les = les;
		yield return null;
		yield return this.LoadBreathList(this.hSceneManager.strAssetBreathListFolder, musterbation, les);
		for (int k = 0; k < this.breathLists.Length; k++)
		{
			this.breathLists[k].DebugListSet();
		}
		yield return this.LoadVoiceList(this.hSceneManager.strAssetVoiceListFolder, musterbation, les);
		yield return this.LoadShortBreathList(this.hSceneManager.strAssetVoiceListFolder, musterbation);
		for (int nLoopCnt = 0; nLoopCnt < 2; nLoopCnt++)
		{
			if (nLoopCnt == 0 || !(this.param_sub == null))
			{
				bool tmpMerchant = this.personality[nLoopCnt] == -90;
				yield return this.LoadStartVoicePtnList(nLoopCnt, this.hSceneManager.strAssetVoiceListFolder, tmpMerchant);
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06005188 RID: 20872 RVA: 0x002171AC File Offset: 0x002155AC
	private IEnumerator LoadBreathList(string _pathAssetFolder, bool musterbation = false, bool les = false)
	{
		for (int i = 0; i < 2; i++)
		{
			if (i == 0 || !(this.param_sub == null))
			{
				if (!musterbation && !les)
				{
					this.LoadBreath(this.personality[i], i, _pathAssetFolder);
				}
				else
				{
					this.LoadMapHBreath(this.personality[i], i, _pathAssetFolder);
				}
			}
		}
		yield return null;
		for (int j = 0; j < 2; j++)
		{
			if (j == 0 || !(this.param_sub == null))
			{
				if (!musterbation && !les)
				{
					this.LoadBreathPtn(j, 0, 0);
					this.LoadBreathPtn(j, 1, 0);
					this.LoadBreathPtn(j, 2, 0);
					this.LoadBreathPtn(j, 3, 0);
					this.LoadBreathPtn(j, 3, 3);
					this.LoadBreathPtn(j, 3, 5);
					this.LoadBreathPtn(j, 4, 0);
					this.LoadBreathPtn(j, 5, 3);
					this.LoadBreathPtn(j, 1, 1);
					this.LoadBreathPtn(j, 3, 1);
					this.LoadBreathPtn(j, 3, 7);
					this.LoadBreathPtn(j, 5, 1);
					this.LoadBreathAddPtn(j, 0, 0);
					this.LoadBreathAddPtn(j, 1, 0);
					this.LoadBreathAddPtn(j, 2, 0);
					this.LoadBreathAddPtn(j, 3, 0);
					this.LoadBreathAddPtn(j, 3, 3);
					this.LoadBreathAddPtn(j, 3, 5);
					this.LoadBreathAddPtn(j, 4, 0);
					this.LoadBreathAddPtn(j, 5, 3);
					this.LoadBreathAddPtn(j, 1, 1);
					this.LoadBreathAddPtn(j, 3, 1);
					this.LoadBreathAddPtn(j, 3, 7);
					this.LoadBreathAddPtn(j, 5, 1);
				}
				else if (!les)
				{
					this.LoadBreathPtn(j, 3, 0);
					this.LoadBreathPtn(j, 3, 3);
					this.LoadBreathPtn(j, 3, 5);
					this.LoadBreathAddPtn(j, 3, 0);
					this.LoadBreathAddPtn(j, 3, 3);
					this.LoadBreathAddPtn(j, 3, 5);
				}
				else
				{
					this.LoadBreathPtn(j, 4, 0);
					this.LoadBreathAddPtn(j, 4, 0);
				}
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06005189 RID: 20873 RVA: 0x002171DC File Offset: 0x002155DC
	private bool LoadBreath(int _personality, int _main, string _pathAssetFolder)
	{
		this.sbLoadFile.Clear();
		this.sbLoadFile.AppendFormat("HBreath_{0:00}", (_personality == -90) ? 5 : _personality);
		base.StartCoroutine(this.LoadBreathBase(GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString()), this.breathLists[_main]));
		return true;
	}

	// Token: 0x0600518A RID: 20874 RVA: 0x00217248 File Offset: 0x00215648
	private bool LoadMapHBreath(int _personality, int _main, string _pathAssetFolder)
	{
		this.sbLoadFile.Clear();
		this.sbLoadFile.AppendFormat("HBreath_{0:00}", (_personality == -90) ? 5 : _personality);
		Observable.FromCoroutine(() => this.LoadBreathBase(GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString()), this.breathLists[_main]), false).Subscribe<Unit>();
		return true;
	}

	// Token: 0x0600518B RID: 20875 RVA: 0x002172B4 File Offset: 0x002156B4
	private IEnumerator LoadBreathBase(string _str, HVoiceCtrl.BreathList _breath)
	{
		if (_str.IsNullOrEmpty())
		{
			yield break;
		}
		string[][] aastr;
		GlobalMethod.GetListString(_str, out aastr);
		int nY = aastr.Length;
		Dictionary<int, HVoiceCtrl.VoiceListInfo> dicBreath = _breath.lstVoiceList;
		for (int i = 0; i < nY; i++)
		{
			int key = int.Parse(aastr[i][0]);
			if (!dicBreath.ContainsKey(key))
			{
				dicBreath.Add(key, new HVoiceCtrl.VoiceListInfo());
			}
			HVoiceCtrl.VoiceListInfo voiceListInfo = dicBreath[key];
			voiceListInfo.pathAsset = aastr[i][1];
			voiceListInfo.nameFile = aastr[i][2];
			int.TryParse(aastr[i][3], out voiceListInfo.group);
			voiceListInfo.notOverWrite = (aastr[i][4] == "1");
			voiceListInfo.urine = (aastr[i][5] == "1");
			voiceListInfo.lstNotHitFace.Clear();
			for (int j = 0; j < 3; j++)
			{
				this.LoadBreathFace(aastr, i, j * 10 + 6, voiceListInfo.lstNotHitFace);
			}
			voiceListInfo.lstHitFace.Clear();
			this.LoadBreathFace(aastr, i, 36, voiceListInfo.lstHitFace);
		}
		yield return null;
		yield break;
	}

	// Token: 0x0600518C RID: 20876 RVA: 0x002172E0 File Offset: 0x002156E0
	private void LoadBreathFace(string[][] _aastr, int _line, int _idx, List<HVoiceCtrl.FaceInfo> _lst)
	{
		HVoiceCtrl.FaceInfo faceInfo = new HVoiceCtrl.FaceInfo();
		float num;
		if (!float.TryParse(_aastr[_line][_idx++], out num) || num < 0f)
		{
			return;
		}
		faceInfo.openEye = num;
		faceInfo.openMouthMin = float.Parse(_aastr[_line][_idx++]);
		faceInfo.openMouthMax = float.Parse(_aastr[_line][_idx++]);
		faceInfo.eyeBlow = int.Parse(_aastr[_line][_idx++]);
		faceInfo.eye = int.Parse(_aastr[_line][_idx++]);
		faceInfo.mouth = int.Parse(_aastr[_line][_idx++]);
		faceInfo.tear = float.Parse(_aastr[_line][_idx++]);
		faceInfo.cheek = float.Parse(_aastr[_line][_idx++]);
		faceInfo.highlight = (_aastr[_line][_idx++] == "1");
		faceInfo.blink = (_aastr[_line][_idx++] == "1");
		_lst.Add(faceInfo);
	}

	// Token: 0x0600518D RID: 20877 RVA: 0x002173EC File Offset: 0x002157EC
	private bool LoadBreathPtn(int personal, int mode, int kind)
	{
		this.sbLoadFile.Clear();
		this.sbLoadFile.AppendFormat("HBreathPattern_{0:00}_{1:00}_{2:00}", personal, mode, kind);
		string text = GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString());
		if (text.IsNullOrEmpty())
		{
			return false;
		}
		string[][] array;
		GlobalMethod.GetListString(text, out array);
		int num = array.Length;
		int num2 = (num == 0) ? 0 : array[0].Length;
		if (!this.dicBreathPtns[personal].ContainsKey(mode))
		{
			this.dicBreathPtns[personal][mode] = this.dicBreathPtns[personal].New<int, int, int, int, string, HVoiceCtrl.BreathPtn>();
		}
		if (!this.dicBreathPtns[personal][mode].ContainsKey(kind))
		{
			this.dicBreathPtns[personal][mode][kind] = this.dicBreathPtns[personal][mode].New<int, int, int, string, HVoiceCtrl.BreathPtn>();
		}
		ValueDictionary<int, int, string, HVoiceCtrl.BreathPtn> valueDictionary = this.dicBreathPtns[personal][mode][kind];
		for (int i = 0; i < num; i++)
		{
			int key = int.Parse(array[i][0]);
			if (!valueDictionary.ContainsKey(key))
			{
				valueDictionary[key] = valueDictionary.New<int, int, string, HVoiceCtrl.BreathPtn>();
			}
			int num3 = int.Parse(array[i][1]);
			if (!valueDictionary[key].ContainsKey(num3))
			{
				valueDictionary[key][num3] = valueDictionary[key].New<int, string, HVoiceCtrl.BreathPtn>();
			}
			ValueDictionary<string, HVoiceCtrl.BreathPtn> valueDictionary2 = valueDictionary[key][num3];
			string text2 = array[i][2];
			if (!valueDictionary2.ContainsKey(text2))
			{
				valueDictionary2.Add(text2, new HVoiceCtrl.BreathPtn());
			}
			HVoiceCtrl.BreathPtn breathPtn = valueDictionary2[text2];
			breathPtn.level = num3;
			breathPtn.anim = text2;
			breathPtn.onlyOne = (array[i][3] == "1");
			breathPtn.isPlay = false;
			breathPtn.force = (array[i][4] == "1");
			breathPtn.timeChangeFaceMin = float.Parse(array[i][5]);
			breathPtn.timeChangeFaceMax = float.Parse(array[i][6]);
			breathPtn.lstInfo.Clear();
			for (int j = 7; j < num2; j += 3)
			{
				string text3 = array[i][j];
				if (text3.IsNullOrEmpty())
				{
					break;
				}
				HVoiceCtrl.BreathVoicePtnInfo breathVoicePtnInfo = new HVoiceCtrl.BreathVoicePtnInfo();
				string[] array2 = text3.Split(new char[]
				{
					','
				});
				int item = 0;
				for (int k = 0; k < array2.Length; k++)
				{
					if (int.TryParse(array2[k], out item))
					{
						breathVoicePtnInfo.lstConditions.Add(item);
					}
				}
				array2 = array[i][j + 1].Split(new char[]
				{
					','
				});
				for (int l = 0; l < array2.Length; l++)
				{
					if (int.TryParse(array2[l], out item))
					{
						breathVoicePtnInfo.lstAnimeID.Add(item);
					}
				}
				array2 = array[i][j + 2].Split(new char[]
				{
					','
				});
				for (int m = 0; m < array2.Length; m++)
				{
					if (int.TryParse(array2[m], out item))
					{
						breathVoicePtnInfo.lstVoice.Add(item);
					}
				}
				breathPtn.lstInfo.Add(breathVoicePtnInfo);
			}
		}
		return true;
	}

	// Token: 0x0600518E RID: 20878 RVA: 0x00217778 File Offset: 0x00215B78
	private bool LoadBreathAddPtn(int personal, int mode, int kind)
	{
		for (int i = 0; i < 5; i++)
		{
			this.sbLoadFile.Clear();
			this.sbLoadFile.AppendFormat("HBreathPattern_{0:00}_{1:00}_{2:00}_{3:00}", new object[]
			{
				personal,
				mode,
				kind,
				i
			});
			string text = GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString());
			if (!text.IsNullOrEmpty())
			{
				string[][] array;
				GlobalMethod.GetListString(text, out array);
				int num = array.Length;
				int num2 = (num == 0) ? 0 : array[0].Length;
				if (!this.dicBreathAddPtns[personal].ContainsKey(mode))
				{
					this.dicBreathAddPtns[personal][mode] = this.dicBreathAddPtns[personal].New<int, int, int, int, string, HVoiceCtrl.BreathPtn>();
				}
				if (!this.dicBreathAddPtns[personal][mode].ContainsKey(kind))
				{
					this.dicBreathAddPtns[personal][mode][kind] = this.dicBreathAddPtns[personal][mode].New<int, int, int, string, HVoiceCtrl.BreathPtn>();
				}
				ValueDictionary<int, int, string, HVoiceCtrl.BreathPtn> valueDictionary = this.dicBreathAddPtns[personal][mode][kind];
				for (int j = 0; j < num; j++)
				{
					int key = int.Parse(array[j][0]);
					if (!valueDictionary.ContainsKey(key))
					{
						valueDictionary[key] = valueDictionary.New<int, int, string, HVoiceCtrl.BreathPtn>();
					}
					int num3 = int.Parse(array[j][1]);
					if (!valueDictionary[key].ContainsKey(num3))
					{
						valueDictionary[key][num3] = valueDictionary[key].New<int, string, HVoiceCtrl.BreathPtn>();
					}
					ValueDictionary<string, HVoiceCtrl.BreathPtn> valueDictionary2 = valueDictionary[key][num3];
					string text2 = array[j][2];
					if (!valueDictionary2.ContainsKey(text2))
					{
						valueDictionary2.Add(text2, new HVoiceCtrl.BreathPtn());
					}
					HVoiceCtrl.BreathPtn breathPtn = valueDictionary2[text2];
					breathPtn.level = num3;
					breathPtn.anim = text2;
					breathPtn.onlyOne = (array[j][3] == "1");
					breathPtn.isPlay = false;
					breathPtn.force = (array[j][4] == "1");
					breathPtn.timeChangeFaceMin = float.Parse(array[j][5]);
					breathPtn.timeChangeFaceMax = float.Parse(array[j][6]);
					for (int k = 7; k < num2; k += 3)
					{
						string text3 = array[j][k];
						if (text3 == string.Empty)
						{
							break;
						}
						HVoiceCtrl.BreathVoicePtnInfo bvi = new HVoiceCtrl.BreathVoicePtnInfo();
						string[] array2 = text3.Split(new char[]
						{
							','
						});
						int item = 0;
						for (int l = 0; l < array2.Length; l++)
						{
							if (int.TryParse(array2[l], out item))
							{
								bvi.lstConditions.Add(item);
							}
						}
						array2 = array[j][k + 1].Split(new char[]
						{
							','
						});
						for (int m = 0; m < array2.Length; m++)
						{
							if (int.TryParse(array2[m], out item))
							{
								bvi.lstAnimeID.Add(item);
							}
						}
						array2 = array[j][k + 2].Split(new char[]
						{
							','
						});
						for (int n = 0; n < array2.Length; n++)
						{
							if (int.TryParse(array2[n], out item))
							{
								bvi.lstVoice.Add(item);
							}
						}
						HVoiceCtrl.BreathVoicePtnInfo breathVoicePtnInfo = breathPtn.lstInfo.Find((HVoiceCtrl.BreathVoicePtnInfo f) => f.lstConditions.SequenceEqual(bvi.lstConditions) && f.lstAnimeID.SequenceEqual(bvi.lstAnimeID));
						if (breathVoicePtnInfo == null)
						{
							breathPtn.lstInfo.Add(bvi);
						}
						else
						{
							breathVoicePtnInfo.lstVoice = new List<int>(bvi.lstVoice);
						}
					}
				}
			}
		}
		this.LoadBreathPtnComposition(personal, mode, kind);
		return true;
	}

	// Token: 0x0600518F RID: 20879 RVA: 0x00217B98 File Offset: 0x00215F98
	private bool LoadBreathPtnComposition(int personal, int mode, int kind)
	{
		if (!this.dicBreathPtns.ContainsKey(personal))
		{
			return false;
		}
		if (!this.dicBreathPtns[personal].ContainsKey(mode))
		{
			return false;
		}
		if (!this.dicBreathPtns[personal][mode].ContainsKey(kind))
		{
			return false;
		}
		ValueDictionary<int, int, string, HVoiceCtrl.BreathPtn> valueDictionary = this.dicBreathPtns[personal][mode][kind];
		if (!this.dicBreathAddPtns.ContainsKey(personal))
		{
			return false;
		}
		if (!this.dicBreathAddPtns[personal].ContainsKey(mode))
		{
			return false;
		}
		if (!this.dicBreathAddPtns[personal][mode].ContainsKey(kind))
		{
			return false;
		}
		ValueDictionary<int, int, string, HVoiceCtrl.BreathPtn> valueDictionary2 = this.dicBreathAddPtns[personal][mode][kind];
		foreach (KeyValuePair<int, ValueDictionary<int, string, HVoiceCtrl.BreathPtn>> keyValuePair in valueDictionary)
		{
			if (valueDictionary2.ContainsKey(keyValuePair.Key))
			{
				foreach (KeyValuePair<int, ValueDictionary<string, HVoiceCtrl.BreathPtn>> keyValuePair2 in keyValuePair.Value)
				{
					if (valueDictionary2[keyValuePair.Key].ContainsKey(keyValuePair2.Key))
					{
						foreach (KeyValuePair<string, HVoiceCtrl.BreathPtn> keyValuePair3 in keyValuePair2.Value)
						{
							if (valueDictionary2[keyValuePair.Key][keyValuePair2.Key].ContainsKey(keyValuePair3.Key))
							{
								keyValuePair3.Value.lstInfo.AddRange(valueDictionary2[keyValuePair.Key][keyValuePair2.Key][keyValuePair3.Key].lstInfo);
							}
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005190 RID: 20880 RVA: 0x00217E08 File Offset: 0x00216208
	private IEnumerator LoadVoiceList(string _pathAssetFolder, bool musterbation = false, bool les = false)
	{
		for (int nLoopCharCnt = 0; nLoopCharCnt < 2; nLoopCharCnt++)
		{
			if (nLoopCharCnt == 0 || !(this.param_sub == null))
			{
				if (!musterbation && !les)
				{
					for (int i = 0; i < 5; i++)
					{
						int kind = i;
						this.LoadVoice(this.personality[nLoopCharCnt], 0, kind, _pathAssetFolder, nLoopCharCnt);
					}
					for (int j = 0; j < 5; j++)
					{
						int kind2 = j;
						this.LoadVoice(this.personality[nLoopCharCnt], 1, kind2, _pathAssetFolder, nLoopCharCnt);
					}
					for (int k = 0; k < 6; k++)
					{
						int kind3 = k;
						this.LoadVoice(this.personality[nLoopCharCnt], 2, kind3, _pathAssetFolder, nLoopCharCnt);
					}
					yield return null;
					for (int l = 0; l < 5; l++)
					{
						int kind4 = l;
						this.LoadVoice(this.personality[nLoopCharCnt], 3, kind4, _pathAssetFolder, nLoopCharCnt);
					}
					this.LoadVoice(this.personality[nLoopCharCnt], 3, 5, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 3, 6, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 4, 0, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 5, 0, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 5, 1, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 6, 0, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 7, 0, _pathAssetFolder, nLoopCharCnt);
				}
				else if (!les)
				{
					for (int m = 0; m < 5; m++)
					{
						int kind5 = m;
						this.LoadVoice(this.personality[nLoopCharCnt], 3, kind5, _pathAssetFolder, nLoopCharCnt);
					}
					this.LoadVoice(this.personality[nLoopCharCnt], 6, 0, _pathAssetFolder, nLoopCharCnt);
				}
				else
				{
					this.LoadVoice(this.personality[nLoopCharCnt], 4, 0, _pathAssetFolder, nLoopCharCnt);
					this.LoadVoice(this.personality[nLoopCharCnt], 6, 0, _pathAssetFolder, nLoopCharCnt);
				}
			}
		}
		yield return null;
		for (int n = 0; n < 2; n++)
		{
			if (n == 0 || !(this.param_sub == null))
			{
				bool merchant = this.personality[n] == -90;
				if (!musterbation && !les)
				{
					this.LoadVoicePtn(0, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(1, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(2, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 1, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 2, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(4, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(5, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 3, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 4, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(5, 1, n, _pathAssetFolder, merchant);
				}
				else if (!les)
				{
					this.LoadVoicePtn(3, 0, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 1, n, _pathAssetFolder, merchant);
					this.LoadVoicePtn(3, 2, n, _pathAssetFolder, merchant);
				}
				else
				{
					this.LoadVoicePtn(4, 0, n, _pathAssetFolder, merchant);
				}
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06005191 RID: 20881 RVA: 0x00217E38 File Offset: 0x00216238
	private bool LoadVoice(int _personality, int _mode, int _kind, string _pathAssetFolder, int _famale)
	{
		this.sbLoadFile.Clear();
		this.sbLoadFile.AppendFormat("HVoiceFace_{0:00}_{1:00}_{2:00}", _personality, _mode, _kind);
		this.lst.Clear();
		GlobalMethod.LoadAllListTextFromList(_pathAssetFolder, this.sbLoadFile.ToString(), ref this.lst, null);
		foreach (string text in this.lst)
		{
			if (!(text == string.Empty))
			{
				string[][] array;
				GlobalMethod.GetListString(text, out array);
				int num = array.Length;
				int num2 = 0;
				int num3 = 0;
				if (int.TryParse(array[num2][0], out num3))
				{
					num2++;
					if (num3 != 0 && num2 + num3 == num)
					{
						if (!this.dicdiclstVoiceList[_famale].ContainsKey(_mode))
						{
							this.dicdiclstVoiceList[_famale].Add(_mode, new Dictionary<int, HVoiceCtrl.VoiceList>());
							this.dicdiclstVoiceList[_famale][_mode].Add(_kind, new HVoiceCtrl.VoiceList());
						}
						else if (!this.dicdiclstVoiceList[_famale][_mode].ContainsKey(_kind))
						{
							this.dicdiclstVoiceList[_famale][_mode].Add(_kind, new HVoiceCtrl.VoiceList());
						}
						HVoiceCtrl.VoiceList voiceList = this.dicdiclstVoiceList[_famale][_mode][_kind];
						this.LoadVoiceFace(array, num3, ref num2, voiceList.dicdicVoiceList);
						voiceList.total += num3;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005192 RID: 20882 RVA: 0x00218000 File Offset: 0x00216400
	private void LoadVoiceFace(string[][] aastr, int endY, ref int line, Dictionary<int, HVoiceCtrl.VoiceListInfo> dic)
	{
		int[] array = new int[]
		{
			-1,
			-1
		};
		while (line - 1 < endY)
		{
			int num = aastr[line].Length;
			if (!int.TryParse(aastr[line][0], out array[0]))
			{
				array[0] = array[1] + 1;
			}
			HVoiceCtrl.VoiceListInfo voiceListInfo = new HVoiceCtrl.VoiceListInfo();
			voiceListInfo.pathAsset = aastr[line][1];
			voiceListInfo.nameFile = aastr[line][2];
			if (num < 4)
			{
				line++;
				array[1]++;
			}
			else
			{
				voiceListInfo.notOverWrite = (aastr[line][3] == "1");
				if ((num - 4) % 32 != 0)
				{
					line++;
					array[1]++;
				}
				else
				{
					for (int i = 4; i < num; i += 32)
					{
						int num2 = 0;
						HVoiceCtrl.FaceInfo faceInfo = new HVoiceCtrl.FaceInfo();
						if (aastr[line][i + num2].IsNullOrEmpty())
						{
							break;
						}
						float num3 = float.Parse(aastr[line][i + num2++]);
						if (num3 < 0f)
						{
							break;
						}
						faceInfo.openEye = num3;
						faceInfo.openMouthMin = float.Parse(aastr[line][i + num2++]);
						faceInfo.openMouthMax = float.Parse(aastr[line][i + num2++]);
						faceInfo.eyeBlow = int.Parse(aastr[line][i + num2++]);
						faceInfo.eye = int.Parse(aastr[line][i + num2++]);
						faceInfo.mouth = int.Parse(aastr[line][i + num2++]);
						faceInfo.tear = float.Parse(aastr[line][i + num2++]);
						faceInfo.cheek = float.Parse(aastr[line][i + num2++]);
						faceInfo.highlight = (aastr[line][i + num2++] == "1");
						faceInfo.blink = (aastr[line][i + num2++] == "1");
						faceInfo.behaviorNeckLine = int.Parse(aastr[line][i + num2++]);
						faceInfo.behaviorEyeLine = int.Parse(aastr[line][i + num2++]);
						faceInfo.targetNeckLine = int.Parse(aastr[line][i + num2++]);
						Vector3 zero = Vector3.zero;
						if (faceInfo.targetNeckLine == 7)
						{
							for (int j = 0; j < 2; j++)
							{
								zero = Vector3.zero;
								if (!float.TryParse(aastr[line][i + num2++], out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(aastr[line][i + num2++], out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(aastr[line][i + num2++], out zero.z))
								{
									zero.z = 0f;
								}
								faceInfo.NeckRot[j] = zero;
							}
							for (int k = 0; k < 2; k++)
							{
								zero = Vector3.zero;
								if (!float.TryParse(aastr[line][i + num2++], out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(aastr[line][i + num2++], out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(aastr[line][i + num2++], out zero.z))
								{
									zero.z = 0f;
								}
								faceInfo.HeadRot[k] = zero;
							}
						}
						else
						{
							num2 += 12;
						}
						faceInfo.targetEyeLine = int.Parse(aastr[line][i + num2++]);
						if (faceInfo.targetEyeLine == 7)
						{
							for (int l = 0; l < 2; l++)
							{
								zero = Vector3.zero;
								if (!float.TryParse(aastr[line][i + num2++], out zero.x))
								{
									zero.x = 0f;
								}
								if (!float.TryParse(aastr[line][i + num2++], out zero.y))
								{
									zero.y = 0f;
								}
								if (!float.TryParse(aastr[line][i + num2++], out zero.z))
								{
									zero.z = 0f;
								}
								faceInfo.EyeRot[l] = zero;
							}
						}
						else
						{
							num2 += 6;
						}
						voiceListInfo.lstHitFace.Add(faceInfo);
					}
					if (!dic.ContainsKey(array[0]))
					{
						dic.Add(array[0], voiceListInfo);
					}
					else
					{
						dic[array[0]] = voiceListInfo;
					}
					line++;
					array[1]++;
				}
			}
		}
	}

	// Token: 0x06005193 RID: 20883 RVA: 0x00218520 File Offset: 0x00216920
	private bool LoadVoicePtn(int taii, int kind, int charNum, string _pathAssetFolder, bool merchant)
	{
		this.sbLoadFile.Clear();
		if (!merchant)
		{
			this.sbLoadFile.AppendFormat("HVoicePattern_{0:00}_{1:00}", taii, kind);
		}
		else
		{
			this.sbLoadFile.AppendFormat("HVoicePattern_-90_{0:00}_{1:00}", taii, kind);
		}
		if (!this.lstLoadVoicePtn[charNum].ContainsKey(taii))
		{
			this.lstLoadVoicePtn[charNum].Add(taii, new Dictionary<int, List<HVoiceCtrl.VoicePtn>>());
		}
		if (!this.lstLoadVoicePtn[charNum][taii].ContainsKey(kind))
		{
			this.lstLoadVoicePtn[charNum][taii].Add(kind, new List<HVoiceCtrl.VoicePtn>());
		}
		this.lst.Clear();
		GlobalMethod.LoadAllListTextFromList(_pathAssetFolder, this.sbLoadFile.ToString(), ref this.lst, null);
		for (int i = 0; i < this.lst.Count; i++)
		{
			string[][] array;
			GlobalMethod.GetListString(this.lst[i], out array);
			int j = 0;
			int num = array.Length;
			while (j < num)
			{
				int num2 = array[j].Length;
				int num3 = -1;
				if (!int.TryParse(array[j][0], out num3))
				{
					j++;
				}
				else
				{
					HVoiceCtrl.CheckVoicePtn ptn;
					ptn.condition = num3;
					if (!int.TryParse(array[j][1], out num3))
					{
						num3 = -1;
					}
					ptn.startKind = num3;
					if (!int.TryParse(array[j][2], out num3))
					{
						num3 = 0;
					}
					ptn.howTalk = num3;
					ptn.LookFlag = (array[j][3] == "0");
					ptn.anim = array[j][4];
					HVoiceCtrl.VoicePtn voicePtn = this.CheckPtn(this.lstLoadVoicePtn[charNum][taii][kind], ptn);
					if (voicePtn == null)
					{
						this.lstLoadVoicePtn[charNum][taii][kind].Add(new HVoiceCtrl.VoicePtn());
						voicePtn = this.lstLoadVoicePtn[charNum][taii][kind][this.lstLoadVoicePtn[charNum][taii][kind].Count - 1];
					}
					else
					{
						voicePtn.lstInfo.Clear();
					}
					voicePtn.condition = ptn.condition;
					voicePtn.startKind = ptn.startKind;
					voicePtn.howTalk = ptn.howTalk;
					voicePtn.LookFlag = ptn.LookFlag;
					voicePtn.anim = ptn.anim;
					int k = 5;
					while (k < num2)
					{
						HVoiceCtrl.VoicePtnInfo voicePtnInfo = new HVoiceCtrl.VoicePtnInfo();
						if (array[j][k].IsNullOrEmpty())
						{
							break;
						}
						string[] array2 = array[j][k++].Split(new char[]
						{
							','
						});
						for (int l = 0; l < array2.Length; l++)
						{
							voicePtnInfo.lstAnimList.Add(int.Parse(array2[l]));
						}
						if (array[j][k].IsNullOrEmpty())
						{
							break;
						}
						array2 = array[j][k++].Split(new char[]
						{
							','
						});
						for (int m = 0; m < array2.Length; m++)
						{
							voicePtnInfo.lstPlayConditions.Add(int.Parse(array2[m]));
						}
						if (array[j][k].IsNullOrEmpty())
						{
							break;
						}
						voicePtnInfo.loadListmode = int.Parse(array[j][k++]);
						if (array[j][k].IsNullOrEmpty())
						{
							break;
						}
						voicePtnInfo.loadListKind = int.Parse(array[j][k++]);
						if (array[j][k].IsNullOrEmpty())
						{
							break;
						}
						array2 = array[j][k++].Split(new char[]
						{
							','
						});
						for (int n = 0; n < array2.Length; n++)
						{
							voicePtnInfo.lstVoice.Add(int.Parse(array2[n]));
						}
						voicePtn.lstInfo.Add(voicePtnInfo);
					}
					j++;
				}
			}
		}
		return true;
	}

	// Token: 0x06005194 RID: 20884 RVA: 0x00218938 File Offset: 0x00216D38
	private HVoiceCtrl.VoicePtn CheckPtn(List<HVoiceCtrl.VoicePtn> ptns, HVoiceCtrl.CheckVoicePtn ptn)
	{
		HVoiceCtrl.VoicePtn result = null;
		for (int i = 0; i < ptns.Count; i++)
		{
			HVoiceCtrl.VoicePtn voicePtn = ptns[i];
			if (voicePtn != null)
			{
				if (voicePtn.condition == ptn.condition)
				{
					if (voicePtn.startKind == ptn.startKind)
					{
						if (voicePtn.howTalk == ptn.howTalk)
						{
							if (voicePtn.LookFlag == ptn.LookFlag)
							{
								if (!(voicePtn.anim != ptn.anim))
								{
									result = voicePtn;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06005195 RID: 20885 RVA: 0x002189F0 File Offset: 0x00216DF0
	private IEnumerator LoadShortBreathList(string _pathAssetFolder, bool musterbation = false)
	{
		for (int i = 0; i < 2; i++)
		{
			if (i == 0 || !(this.param_sub == null))
			{
				this.LoadShortBreath(this.personality[i], i);
				if (!musterbation)
				{
					this.LoadShortBreathPtn(i, 0, 0);
					this.LoadShortBreathPtn(i, 1, 0);
					this.LoadShortBreathPtn(i, 2, 0);
					this.LoadShortBreathPtn(i, 3, 0);
					this.LoadShortBreathPtn(i, 3, 3);
					this.LoadShortBreathPtn(i, 3, 5);
					this.LoadShortBreathPtn(i, 4, 0);
					this.LoadShortBreathPtn(i, 5, 3);
					this.LoadShortBreathPtn(i, 1, 1);
					this.LoadShortBreathPtn(i, 3, 1);
					this.LoadShortBreathPtn(i, 3, 7);
					this.LoadShortBreathPtn(i, 5, 1);
					this.LoadShortBreathAddPtn(i, 0, 0);
					this.LoadShortBreathAddPtn(i, 1, 0);
					this.LoadShortBreathAddPtn(i, 2, 0);
					this.LoadShortBreathAddPtn(i, 3, 0);
					this.LoadShortBreathAddPtn(i, 3, 3);
					this.LoadShortBreathAddPtn(i, 3, 5);
					this.LoadShortBreathAddPtn(i, 4, 0);
					this.LoadShortBreathAddPtn(i, 5, 3);
					this.LoadShortBreathAddPtn(i, 1, 1);
					this.LoadShortBreathAddPtn(i, 3, 1);
					this.LoadShortBreathAddPtn(i, 3, 7);
					this.LoadShortBreathAddPtn(i, 5, 1);
				}
				else
				{
					this.LoadShortBreathPtn(i, 3, 0);
					this.LoadShortBreathPtn(i, 3, 3);
					this.LoadShortBreathPtn(i, 3, 5);
					this.LoadShortBreathAddPtn(i, 3, 0);
					this.LoadShortBreathAddPtn(i, 3, 3);
					this.LoadShortBreathAddPtn(i, 3, 5);
				}
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06005196 RID: 20886 RVA: 0x00218A14 File Offset: 0x00216E14
	private bool LoadShortBreath(int _personality, int _main)
	{
		this.sbLoadFile.Clear();
		this.sbLoadFile.AppendFormat("HShort_breath_{0:00}", _personality);
		string text = GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString());
		if (text.IsNullOrEmpty())
		{
			return false;
		}
		string[][] array;
		GlobalMethod.GetListString(text, out array);
		int num = array.Length;
		int num2 = (num == 0) ? 0 : array[0].Length;
		for (int i = 0; i < num; i++)
		{
			int key = 0;
			int num3 = 0;
			string s = array[i][num3++];
			if (int.TryParse(s, out key))
			{
				if (!this.ShortBreathLists[_main].dicShortBreathLists.ContainsKey(key))
				{
					this.ShortBreathLists[_main].dicShortBreathLists.Add(key, new HVoiceCtrl.VoiceListInfo());
				}
				HVoiceCtrl.VoiceListInfo voiceListInfo = this.ShortBreathLists[_main].dicShortBreathLists[key];
				voiceListInfo.pathAsset = array[i][num3++];
				voiceListInfo.nameFile = array[i][num3++];
				voiceListInfo.notOverWrite = (array[i][num3++] == "1");
				voiceListInfo.lstHitFace.Clear();
				for (int j = num3; j < num2; j += 10)
				{
					int num4 = 0;
					HVoiceCtrl.FaceInfo faceInfo = new HVoiceCtrl.FaceInfo();
					float num5;
					if (!float.TryParse(array[i][j + num4++], out num5) || num5 < 0f)
					{
						break;
					}
					faceInfo.openEye = num5;
					faceInfo.openMouthMin = float.Parse(array[i][j + num4++]);
					faceInfo.openMouthMax = float.Parse(array[i][j + num4++]);
					faceInfo.eyeBlow = int.Parse(array[i][j + num4++]);
					faceInfo.eye = int.Parse(array[i][j + num4++]);
					faceInfo.mouth = int.Parse(array[i][j + num4++]);
					faceInfo.tear = float.Parse(array[i][j + num4++]);
					faceInfo.cheek = float.Parse(array[i][j + num4++]);
					faceInfo.highlight = (array[i][j + num4++] == "1");
					faceInfo.blink = (array[i][j + num4++] == "1");
					voiceListInfo.lstHitFace.Add(faceInfo);
				}
			}
		}
		return true;
	}

	// Token: 0x06005197 RID: 20887 RVA: 0x00218CC0 File Offset: 0x002170C0
	private bool LoadShortBreathPtn(int _main, int _mode, int _kind)
	{
		this.sbLoadFile.Clear();
		this.sbLoadFile.AppendFormat("HShortBreathPattern_{0:00}_{1:00}_{2:00}", _main, _mode, _kind);
		string text = GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString());
		if (text.IsNullOrEmpty())
		{
			return false;
		}
		string[][] array;
		GlobalMethod.GetListString(text, out array);
		int num = array.Length;
		int num2 = (num == 0) ? 0 : array[0].Length;
		ValueDictionary<int, int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> dicInfo = this.shortBreathPtns[_main].dicInfo;
		for (int i = 0; i < num; i++)
		{
			if (!dicInfo.ContainsKey(_mode))
			{
				dicInfo[_mode] = dicInfo.New<int, int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
			}
			if (!dicInfo[_mode].ContainsKey(_kind))
			{
				dicInfo[_mode][_kind] = dicInfo[_mode].New<int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
			}
			int num3 = 0;
			int key = int.Parse(array[i][num3++]);
			if (!dicInfo[_mode][_kind].ContainsKey(key))
			{
				dicInfo[_mode][_kind][key] = dicInfo[_mode][_kind].New<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
			}
			int key2 = int.Parse(array[i][num3++]);
			if (!dicInfo[_mode][_kind][key].ContainsKey(key2))
			{
				dicInfo[_mode][_kind][key][key2] = new List<HVoiceCtrl.BreathVoicePtnInfo>();
			}
			dicInfo[_mode][_kind][key][key2].Clear();
			for (int j = num3; j < num2; j += 3)
			{
				string text2 = array[i][j];
				if (text2 == string.Empty)
				{
					break;
				}
				HVoiceCtrl.BreathVoicePtnInfo breathVoicePtnInfo = new HVoiceCtrl.BreathVoicePtnInfo();
				string[] array2 = text2.Split(new char[]
				{
					','
				});
				int item = 0;
				for (int k = 0; k < array2.Length; k++)
				{
					if (int.TryParse(array2[k], out item))
					{
						breathVoicePtnInfo.lstConditions.Add(item);
					}
				}
				array2 = array[i][j + 1].Split(new char[]
				{
					','
				});
				for (int l = 0; l < array2.Length; l++)
				{
					if (int.TryParse(array2[l], out item))
					{
						breathVoicePtnInfo.lstAnimeID.Add(item);
					}
				}
				array2 = array[i][j + 2].Split(new char[]
				{
					','
				});
				for (int m = 0; m < array2.Length; m++)
				{
					if (int.TryParse(array2[m], out item))
					{
						breathVoicePtnInfo.lstVoice.Add(item);
					}
				}
				dicInfo[_mode][_kind][key][key2].Add(breathVoicePtnInfo);
			}
		}
		return true;
	}

	// Token: 0x06005198 RID: 20888 RVA: 0x00218FD4 File Offset: 0x002173D4
	private bool LoadShortBreathAddPtn(int _main, int _mode, int _kind)
	{
		for (int i = 0; i < 5; i++)
		{
			this.sbLoadFile.Clear();
			this.sbLoadFile.AppendFormat("HShortBreathPattern_{0:00}_{1:00}_{2:00}_{3:00}", new object[]
			{
				_main,
				_mode,
				_kind,
				i
			});
			string text = GlobalMethod.LoadAllListText(this.lstBreathAbnames, this.sbLoadFile.ToString());
			if (!text.IsNullOrEmpty())
			{
				string[][] array;
				GlobalMethod.GetListString(text, out array);
				int num = array.Length;
				int num2 = (num == 0) ? 0 : array[0].Length;
				ValueDictionary<int, int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> dicInfo = this.shortBreathAddPtns[_main].dicInfo;
				for (int j = 0; j < num; j++)
				{
					if (!dicInfo.ContainsKey(_mode))
					{
						dicInfo[_mode] = dicInfo.New<int, int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
					}
					if (!dicInfo[_mode].ContainsKey(_kind))
					{
						dicInfo[_mode][_kind] = dicInfo[_mode].New<int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
					}
					int num3 = 0;
					int key = int.Parse(array[j][num3++]);
					if (!dicInfo[_mode][_kind].ContainsKey(key))
					{
						dicInfo[_mode][_kind][key] = dicInfo[_mode][_kind].New<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
					}
					int key2 = int.Parse(array[j][num3++]);
					if (!dicInfo[_mode][_kind][key].ContainsKey(key2))
					{
						dicInfo[_mode][_kind][key][key2] = new List<HVoiceCtrl.BreathVoicePtnInfo>();
					}
					for (int k = num3; k < num2; k += 3)
					{
						string text2 = array[j][k];
						if (text2 == string.Empty)
						{
							break;
						}
						HVoiceCtrl.BreathVoicePtnInfo bvi = new HVoiceCtrl.BreathVoicePtnInfo();
						string[] array2 = text2.Split(new char[]
						{
							','
						});
						int item = 0;
						for (int l = 0; l < array2.Length; l++)
						{
							if (int.TryParse(array2[l], out item))
							{
								bvi.lstConditions.Add(item);
							}
						}
						array2 = array[j][k + 1].Split(new char[]
						{
							','
						});
						for (int m = 0; m < array2.Length; m++)
						{
							if (int.TryParse(array2[m], out item))
							{
								bvi.lstAnimeID.Add(item);
							}
						}
						array2 = array[j][k + 2].Split(new char[]
						{
							','
						});
						for (int n = 0; n < array2.Length; n++)
						{
							if (int.TryParse(array2[n], out item))
							{
								bvi.lstVoice.Add(item);
							}
						}
						HVoiceCtrl.BreathVoicePtnInfo breathVoicePtnInfo = dicInfo[_mode][_kind][key][key2].Find((HVoiceCtrl.BreathVoicePtnInfo f) => f.lstConditions.SequenceEqual(bvi.lstConditions) && f.lstAnimeID.SequenceEqual(bvi.lstAnimeID));
						if (breathVoicePtnInfo == null)
						{
							dicInfo[_mode][_kind][key][key2].Add(bvi);
						}
						else
						{
							breathVoicePtnInfo.lstVoice = new List<int>(bvi.lstVoice);
						}
					}
				}
			}
		}
		this.LoadShortBreathPtnComposition(_main, _mode, _kind);
		return true;
	}

	// Token: 0x06005199 RID: 20889 RVA: 0x00219374 File Offset: 0x00217774
	private bool LoadShortBreathPtnComposition(int _main, int _mode, int _kind)
	{
		if (!this.shortBreathPtns[_main].dicInfo.ContainsKey(_mode))
		{
			return false;
		}
		if (!this.shortBreathPtns[_main].dicInfo[_mode].ContainsKey(_kind))
		{
			return false;
		}
		ValueDictionary<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> valueDictionary = this.shortBreathPtns[_main].dicInfo[_mode][_kind];
		if (!this.shortBreathAddPtns[_main].dicInfo.ContainsKey(_mode))
		{
			return false;
		}
		if (!this.shortBreathAddPtns[_main].dicInfo[_mode].ContainsKey(_kind))
		{
			return false;
		}
		ValueDictionary<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> valueDictionary2 = this.shortBreathAddPtns[_main].dicInfo[_mode][_kind];
		foreach (KeyValuePair<int, ValueDictionary<int, List<HVoiceCtrl.BreathVoicePtnInfo>>> keyValuePair in valueDictionary)
		{
			if (valueDictionary2.ContainsKey(keyValuePair.Key))
			{
				foreach (KeyValuePair<int, List<HVoiceCtrl.BreathVoicePtnInfo>> keyValuePair2 in keyValuePair.Value)
				{
					if (valueDictionary2[keyValuePair.Key].ContainsKey(keyValuePair2.Key))
					{
						keyValuePair2.Value.AddRange(valueDictionary2[keyValuePair.Key][keyValuePair2.Key]);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x0600519A RID: 20890 RVA: 0x00219514 File Offset: 0x00217914
	private IEnumerator LoadStartVoicePtnList(int charNum, string _pathAssetFolder, bool merchant)
	{
		this.sbLoadFile.Clear();
		if (!merchant)
		{
			this.sbLoadFile.Append("HStartVoicePattern");
		}
		else
		{
			this.sbLoadFile.AppendFormat("HStartVoicePattern_-90", Array.Empty<object>());
		}
		if (this.lstLoadStartVoicePtn[charNum] == null)
		{
			this.lstLoadStartVoicePtn[charNum] = new List<HVoiceCtrl.StartVoicePtn>();
		}
		this.lst.Clear();
		GlobalMethod.LoadAllListTextFromList(_pathAssetFolder, this.sbLoadFile.ToString(), ref this.lst, null);
		string[] aStr = new string[]
		{
			"H前待機",
			"開始(H前待機から)",
			"開始(その他)",
			"再開始",
			"行為変更",
			"H前待機返し",
			"開始返し",
			"再開始返し"
		};
		for (int nLoopCnt = 0; nLoopCnt < this.lst.Count; nLoopCnt++)
		{
			string[][] aastr;
			GlobalMethod.GetListString(this.lst[nLoopCnt], out aastr);
			int line = 0;
			int limitY = aastr.Length;
			while (line < limitY)
			{
				HVoiceCtrl.StartVoicePtn startVoicePtn = new HVoiceCtrl.StartVoicePtn();
				int num = aastr[line].Length;
				int num2 = -1;
				if (!int.TryParse(aastr[line][0], out num2))
				{
					line++;
				}
				else
				{
					startVoicePtn.condition = num2;
					if (!int.TryParse(aastr[line][1], out num2))
					{
						num2 = 0;
					}
					startVoicePtn.nForce = num2;
					if (!int.TryParse(aastr[line][2], out num2))
					{
						num2 = 0;
					}
					startVoicePtn.nTaii = num2;
					if (!int.TryParse(aastr[line][3], out num2))
					{
						num2 = -1;
					}
					startVoicePtn.timing = num2;
					startVoicePtn.anim = aStr[num2];
					int i = 4;
					while (i < num)
					{
						HVoiceCtrl.VoicePtnInfo voicePtnInfo = new HVoiceCtrl.VoicePtnInfo();
						if (aastr[line][i].IsNullOrEmpty())
						{
							break;
						}
						string[] buf = aastr[line][i++].Split(new char[]
						{
							','
						});
						for (int j = 0; j < buf.Length; j++)
						{
							voicePtnInfo.lstAnimList.Add(int.Parse(buf[j]));
						}
						buf = aastr[line][i++].Split(new char[]
						{
							','
						});
						for (int k = 0; k < buf.Length; k++)
						{
							voicePtnInfo.lstPlayConditions.Add(int.Parse(buf[k]));
						}
						voicePtnInfo.loadListmode = int.Parse(aastr[line][i++]);
						voicePtnInfo.loadListKind = int.Parse(aastr[line][i++]);
						buf = aastr[line][i++].Split(new char[]
						{
							','
						});
						for (int l = 0; l < buf.Length; l++)
						{
							voicePtnInfo.lstVoice.Add(int.Parse(buf[l]));
						}
						startVoicePtn.lstInfo.Add(voicePtnInfo);
					}
					int num3 = this.AddStartVoicePtn(this.lstLoadStartVoicePtn[charNum], startVoicePtn);
					if (num3 >= 0)
					{
						this.lstLoadStartVoicePtn[charNum][num3] = startVoicePtn;
					}
					else
					{
						this.lstLoadStartVoicePtn[charNum].Add(startVoicePtn);
					}
					line++;
				}
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600519B RID: 20891 RVA: 0x00219544 File Offset: 0x00217944
	private int AddStartVoicePtn(List<HVoiceCtrl.StartVoicePtn> voicePtns, HVoiceCtrl.StartVoicePtn check)
	{
		for (int i = 0; i < voicePtns.Count; i++)
		{
			if (voicePtns[i].condition == check.condition)
			{
				if (voicePtns[i].nForce == check.nForce)
				{
					if (voicePtns[i].nTaii == check.nTaii)
					{
						if (voicePtns[i].timing == check.timing)
						{
							return i;
						}
					}
				}
			}
		}
		return -1;
	}

	// Token: 0x04004BA4 RID: 19364
	public HVoiceCtrl.BreathList[] breathLists = new HVoiceCtrl.BreathList[2];

	// Token: 0x04004BA5 RID: 19365
	private Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>>[] dicdiclstVoiceList = new Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceList>>[2];

	// Token: 0x04004BA6 RID: 19366
	private HVoiceCtrl.ShortVoiceList[] ShortBreathLists = new HVoiceCtrl.ShortVoiceList[2];

	// Token: 0x04004BA7 RID: 19367
	private ValueDictionary<int, int, int, int, int, string, HVoiceCtrl.BreathPtn> dicBreathPtns = new ValueDictionary<int, int, int, int, int, string, HVoiceCtrl.BreathPtn>();

	// Token: 0x04004BA8 RID: 19368
	private ValueDictionary<int, int, int, int, int, string, HVoiceCtrl.BreathPtn> dicBreathAddPtns = new ValueDictionary<int, int, int, int, int, string, HVoiceCtrl.BreathPtn>();

	// Token: 0x04004BA9 RID: 19369
	private Dictionary<int, Dictionary<int, List<HVoiceCtrl.VoicePtn>>>[] lstLoadVoicePtn = new Dictionary<int, Dictionary<int, List<HVoiceCtrl.VoicePtn>>>[2];

	// Token: 0x04004BAA RID: 19370
	private List<HVoiceCtrl.StartVoicePtn>[] lstLoadStartVoicePtn = new List<HVoiceCtrl.StartVoicePtn>[2];

	// Token: 0x04004BAB RID: 19371
	private HVoiceCtrl.ShortBreathPtn[] shortBreathPtns = new HVoiceCtrl.ShortBreathPtn[2];

	// Token: 0x04004BAC RID: 19372
	private HVoiceCtrl.ShortBreathPtn[] shortBreathAddPtns = new HVoiceCtrl.ShortBreathPtn[2];

	// Token: 0x04004BAD RID: 19373
	private Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceAnimationPlay>> dicdicVoicePlayAnimation = new Dictionary<int, Dictionary<int, HVoiceCtrl.VoiceAnimationPlay>>();

	// Token: 0x04004BAE RID: 19374
	public HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004BAF RID: 19375
	[SerializeField]
	private HVoiceCtrl.BreathList[] breathUseLists = new HVoiceCtrl.BreathList[2];

	// Token: 0x04004BB0 RID: 19376
	[SerializeField]
	private ValueDictionary<int, string, HVoiceCtrl.BreathPtn> dicBreathUsePtns = new ValueDictionary<int, string, HVoiceCtrl.BreathPtn>();

	// Token: 0x04004BB1 RID: 19377
	[SerializeField]
	private List<HVoiceCtrl.VoicePtn>[] lstVoicePtn = new List<HVoiceCtrl.VoicePtn>[2];

	// Token: 0x04004BB2 RID: 19378
	[SerializeField]
	private ValueDictionary<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> dicShortBreathUsePtns = new ValueDictionary<int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();

	// Token: 0x04004BB3 RID: 19379
	public HVoiceCtrl.VoiceAnimationPlay playAnimation = new HVoiceCtrl.VoiceAnimationPlay();

	// Token: 0x04004BB4 RID: 19380
	[SerializeField]
	private int[] personality = new int[2];

	// Token: 0x04004BB5 RID: 19381
	[SerializeField]
	private float[] voicePitch = new float[2];

	// Token: 0x04004BB6 RID: 19382
	[SerializeField]
	private Actor param;

	// Token: 0x04004BB7 RID: 19383
	[SerializeField]
	private Actor param_sub;

	// Token: 0x04004BB8 RID: 19384
	[SerializeField]
	private List<int> lstSystem;

	// Token: 0x04004BB9 RID: 19385
	public HVoiceCtrl.Voice[] nowVoices = new HVoiceCtrl.Voice[2];

	// Token: 0x04004BBA RID: 19386
	public int nowMode;

	// Token: 0x04004BBB RID: 19387
	public int nowKind;

	// Token: 0x04004BBC RID: 19388
	public int nowId;

	// Token: 0x04004BBD RID: 19389
	[SerializeField]
	private int merchantType = -1;

	// Token: 0x04004BBE RID: 19390
	private GlobalMethod.FloatBlend[] blendEyes = new GlobalMethod.FloatBlend[2];

	// Token: 0x04004BBF RID: 19391
	private GlobalMethod.FloatBlend[] blendMouths = new GlobalMethod.FloatBlend[2];

	// Token: 0x04004BC0 RID: 19392
	private GlobalMethod.FloatBlend[] blendMouthMaxs = new GlobalMethod.FloatBlend[2];

	// Token: 0x04004BC1 RID: 19393
	private HSceneManager hSceneManager;

	// Token: 0x04004BC2 RID: 19394
	public float HBeforeHouchiTime;

	// Token: 0x04004BC3 RID: 19395
	public float HouchiTime;

	// Token: 0x04004BC4 RID: 19396
	private bool[] isPlays = new bool[2];

	// Token: 0x04004BC5 RID: 19397
	private StringBuilder sbLoadFile = new StringBuilder();

	// Token: 0x04004BC6 RID: 19398
	[SerializeField]
	private int IngoLimit = 170;

	// Token: 0x04004BC7 RID: 19399
	private bool masturbation;

	// Token: 0x04004BC8 RID: 19400
	private bool les;

	// Token: 0x04004BC9 RID: 19401
	public int MapHID = -1;

	// Token: 0x04004BCA RID: 19402
	private readonly Dictionary<int, List<int>> EtcKind = new Dictionary<int, List<int>>
	{
		{
			0,
			new List<int>
			{
				1,
				2,
				3,
				4,
				5,
				6,
				8,
				9,
				13,
				14
			}
		},
		{
			1,
			new List<int>
			{
				0,
				7
			}
		},
		{
			2,
			new List<int>
			{
				10,
				11,
				12
			}
		},
		{
			3,
			new List<int>
			{
				109,
				110
			}
		},
		{
			4,
			new List<int>
			{
				111
			}
		}
	};

	// Token: 0x04004BCB RID: 19403
	private readonly Dictionary<int, List<int>> HoushiKind = new Dictionary<int, List<int>>
	{
		{
			1,
			new List<int>
			{
				107
			}
		}
	};

	// Token: 0x04004BCC RID: 19404
	private List<string> lst = new List<string>();

	// Token: 0x04004BCD RID: 19405
	private List<string> lstBreathAbnames = new List<string>();

	// Token: 0x02000ADB RID: 2779
	[Serializable]
	public class FaceInfo
	{
		// Token: 0x04004BD8 RID: 19416
		[RangeLabel("目の開き", 0f, 1f)]
		public float openEye = 10f;

		// Token: 0x04004BD9 RID: 19417
		[RangeLabel("口の開き最小", 0f, 1f)]
		public float openMouthMin;

		// Token: 0x04004BDA RID: 19418
		[RangeLabel("口の開き最大", 0f, 1f)]
		public float openMouthMax = 1f;

		// Token: 0x04004BDB RID: 19419
		[Label("眉の形")]
		public int eyeBlow;

		// Token: 0x04004BDC RID: 19420
		[Label("目の形")]
		public int eye;

		// Token: 0x04004BDD RID: 19421
		[Label("口の形")]
		public int mouth;

		// Token: 0x04004BDE RID: 19422
		[RangeLabel("涙", 0f, 1f)]
		public float tear;

		// Token: 0x04004BDF RID: 19423
		[RangeLabel("頬赤", 0f, 1f)]
		public float cheek;

		// Token: 0x04004BE0 RID: 19424
		[Label("ハイライト")]
		public bool highlight;

		// Token: 0x04004BE1 RID: 19425
		[Label("瞬き")]
		public bool blink;

		// Token: 0x04004BE2 RID: 19426
		[Label("首挙動")]
		public int behaviorNeckLine;

		// Token: 0x04004BE3 RID: 19427
		[Label("目挙動")]
		public int behaviorEyeLine;

		// Token: 0x04004BE4 RID: 19428
		[Label("首タゲ")]
		public int targetNeckLine;

		// Token: 0x04004BE5 RID: 19429
		[Label("目タゲ")]
		public int targetEyeLine;

		// Token: 0x04004BE6 RID: 19430
		[Label("視線角度")]
		public Vector3[] EyeRot = new Vector3[2];

		// Token: 0x04004BE7 RID: 19431
		[Label("首角度")]
		public Vector3[] NeckRot = new Vector3[2];

		// Token: 0x04004BE8 RID: 19432
		[Label("頭角度")]
		public Vector3[] HeadRot = new Vector3[2];
	}

	// Token: 0x02000ADC RID: 2780
	[Serializable]
	public class VoiceListInfo
	{
		// Token: 0x04004BE9 RID: 19433
		[Label("ファイル名")]
		public string nameFile = string.Empty;

		// Token: 0x04004BEA RID: 19434
		[Label("アセットバンドルパス")]
		public string pathAsset = string.Empty;

		// Token: 0x04004BEB RID: 19435
		[Label("上書き禁止フラグ")]
		public bool notOverWrite;

		// Token: 0x04004BEC RID: 19436
		[Label("喋った(セリフ時のみ)")]
		public bool isPlay;

		// Token: 0x04004BED RID: 19437
		[Label("呼吸グループ(呼吸時のみ)")]
		public int group = -1;

		// Token: 0x04004BEE RID: 19438
		[Label("おしっこセリフ(呼吸時のみ)")]
		public bool urine;

		// Token: 0x04004BEF RID: 19439
		[Label("アタリにあたっていない")]
		public List<HVoiceCtrl.FaceInfo> lstNotHitFace = new List<HVoiceCtrl.FaceInfo>();

		// Token: 0x04004BF0 RID: 19440
		[Label("アタリにあたっている")]
		public List<HVoiceCtrl.FaceInfo> lstHitFace = new List<HVoiceCtrl.FaceInfo>();
	}

	// Token: 0x02000ADD RID: 2781
	[Serializable]
	public class BreathList
	{
		// Token: 0x060051A9 RID: 20905 RVA: 0x002196CC File Offset: 0x00217ACC
		public void DebugListSet()
		{
			foreach (KeyValuePair<int, HVoiceCtrl.VoiceListInfo> keyValuePair in this.lstVoiceList)
			{
				this.debugList.Add(new HVoiceCtrl.BreathList.InspectorBreathList
				{
					key = keyValuePair.Key,
					value = keyValuePair.Value
				});
			}
		}

		// Token: 0x04004BF1 RID: 19441
		public Dictionary<int, HVoiceCtrl.VoiceListInfo> lstVoiceList = new Dictionary<int, HVoiceCtrl.VoiceListInfo>();

		// Token: 0x04004BF2 RID: 19442
		[SerializeField]
		private List<HVoiceCtrl.BreathList.InspectorBreathList> debugList = new List<HVoiceCtrl.BreathList.InspectorBreathList>();

		// Token: 0x02000ADE RID: 2782
		[Serializable]
		private struct InspectorBreathList
		{
			// Token: 0x04004BF3 RID: 19443
			public int key;

			// Token: 0x04004BF4 RID: 19444
			public HVoiceCtrl.VoiceListInfo value;
		}
	}

	// Token: 0x02000ADF RID: 2783
	[Serializable]
	public class VoiceList
	{
		// Token: 0x04004BF5 RID: 19445
		public Dictionary<int, HVoiceCtrl.VoiceListInfo> dicdicVoiceList = new Dictionary<int, HVoiceCtrl.VoiceListInfo>();

		// Token: 0x04004BF6 RID: 19446
		public int total;
	}

	// Token: 0x02000AE0 RID: 2784
	[Serializable]
	public struct PlayVoiceinfo
	{
		// Token: 0x04004BF7 RID: 19447
		public int mode;

		// Token: 0x04004BF8 RID: 19448
		public int kind;

		// Token: 0x04004BF9 RID: 19449
		public int voiceID;
	}

	// Token: 0x02000AE1 RID: 2785
	[Serializable]
	public class ShortVoiceList
	{
		// Token: 0x04004BFA RID: 19450
		public Dictionary<int, HVoiceCtrl.VoiceListInfo> dicShortBreathLists = new Dictionary<int, HVoiceCtrl.VoiceListInfo>();
	}

	// Token: 0x02000AE2 RID: 2786
	[Serializable]
	public class BreathVoicePtnInfo
	{
		// Token: 0x04004BFB RID: 19451
		public List<int> lstConditions = new List<int>();

		// Token: 0x04004BFC RID: 19452
		public List<int> lstVoice = new List<int>();

		// Token: 0x04004BFD RID: 19453
		public List<int> lstAnimeID = new List<int>();
	}

	// Token: 0x02000AE3 RID: 2787
	[Serializable]
	public class BreathPtn
	{
		// Token: 0x04004BFE RID: 19454
		[Label("段階")]
		public int level;

		// Token: 0x04004BFF RID: 19455
		[Label("アニメーション名")]
		public string anim = string.Empty;

		// Token: 0x04004C00 RID: 19456
		[Label("ループ中1回")]
		public bool onlyOne;

		// Token: 0x04004C01 RID: 19457
		[Label("ループ中1回がtrueのとき使用")]
		public bool isPlay;

		// Token: 0x04004C02 RID: 19458
		[Label("強制フラグ")]
		public bool force;

		// Token: 0x04004C03 RID: 19459
		[Label("表情変更時間最小")]
		public float timeChangeFaceMin = 5f;

		// Token: 0x04004C04 RID: 19460
		[Label("表情変更時間最題")]
		public float timeChangeFaceMax = 5f;

		// Token: 0x04004C05 RID: 19461
		public List<HVoiceCtrl.BreathVoicePtnInfo> lstInfo = new List<HVoiceCtrl.BreathVoicePtnInfo>();
	}

	// Token: 0x02000AE4 RID: 2788
	[Serializable]
	public class VoicePtnInfo
	{
		// Token: 0x04004C06 RID: 19462
		public List<int> lstAnimList = new List<int>();

		// Token: 0x04004C07 RID: 19463
		public List<int> lstPlayConditions = new List<int>();

		// Token: 0x04004C08 RID: 19464
		public int loadListmode = -1;

		// Token: 0x04004C09 RID: 19465
		public int loadListKind = -1;

		// Token: 0x04004C0A RID: 19466
		public List<int> lstVoice = new List<int>();
	}

	// Token: 0x02000AE5 RID: 2789
	[Serializable]
	public class VoicePtn
	{
		// Token: 0x04004C0B RID: 19467
		[Label("アニメーション名")]
		public string anim = string.Empty;

		// Token: 0x04004C0C RID: 19468
		[Label("キャラの状態")]
		public int condition = -1;

		// Token: 0x04004C0D RID: 19469
		[Label("開始パターン")]
		public int startKind = -1;

		// Token: 0x04004C0E RID: 19470
		[Label("掛け合いか")]
		public int howTalk;

		// Token: 0x04004C0F RID: 19471
		[Label("外部フラグ見る")]
		public bool LookFlag = true;

		// Token: 0x04004C10 RID: 19472
		public List<HVoiceCtrl.VoicePtnInfo> lstInfo = new List<HVoiceCtrl.VoicePtnInfo>();
	}

	// Token: 0x02000AE6 RID: 2790
	public struct CheckVoicePtn
	{
		// Token: 0x04004C11 RID: 19473
		[Label("アニメーション名")]
		public string anim;

		// Token: 0x04004C12 RID: 19474
		[Label("キャラの状態")]
		public int condition;

		// Token: 0x04004C13 RID: 19475
		[Label("開始パターン")]
		public int startKind;

		// Token: 0x04004C14 RID: 19476
		[Label("掛け合いか")]
		public int howTalk;

		// Token: 0x04004C15 RID: 19477
		[Label("外部フラグ見る")]
		public bool LookFlag;
	}

	// Token: 0x02000AE7 RID: 2791
	[Serializable]
	public class StartVoicePtn
	{
		// Token: 0x04004C16 RID: 19478
		[Label("キャラの状態")]
		public int condition = -1;

		// Token: 0x04004C17 RID: 19479
		[Label("襲う種類")]
		public int nForce;

		// Token: 0x04004C18 RID: 19480
		[Label("どの体位の開始か")]
		public int nTaii = -1;

		// Token: 0x04004C19 RID: 19481
		[Label("タイミング名")]
		public string anim = string.Empty;

		// Token: 0x04004C1A RID: 19482
		[Label("タイミング")]
		public int timing;

		// Token: 0x04004C1B RID: 19483
		public List<HVoiceCtrl.VoicePtnInfo> lstInfo = new List<HVoiceCtrl.VoicePtnInfo>();
	}

	// Token: 0x02000AE8 RID: 2792
	[Serializable]
	public class ShortBreathPtn
	{
		// Token: 0x04004C1C RID: 19484
		public ValueDictionary<int, int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>> dicInfo = new ValueDictionary<int, int, int, int, List<HVoiceCtrl.BreathVoicePtnInfo>>();
	}

	// Token: 0x02000AE9 RID: 2793
	[Serializable]
	public class VoiceAnimationPlayInfo
	{
		// Token: 0x04004C1D RID: 19485
		[Label("アニメーション名(ハッシュ値)")]
		public int animationHash;

		// Token: 0x04004C1E RID: 19486
		[Label("再生した")]
		public bool[] isPlays = new bool[2];
	}

	// Token: 0x02000AEA RID: 2794
	[Serializable]
	public class VoiceAnimationPlay
	{
		// Token: 0x060051B4 RID: 20916 RVA: 0x002198A8 File Offset: 0x00217CA8
		public void SetAllFlags(bool _play)
		{
			for (int i = 0; i < this.lstPlayInfo.Count; i++)
			{
				this.lstPlayInfo[i].isPlays[0] = _play;
				this.lstPlayInfo[i].isPlays[1] = _play;
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x002198FC File Offset: 0x00217CFC
		public void AfterFinish()
		{
			for (int i = 0; i < this.lstPlayInfo.Count; i++)
			{
				if (this.lstPlayInfo[i].isPlays[0] || this.lstPlayInfo[i].isPlays[1])
				{
					this.lstPlayInfo[i].isPlays[0] = false;
					this.lstPlayInfo[i].isPlays[1] = false;
				}
			}
			this.Count++;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00219990 File Offset: 0x00217D90
		public HVoiceCtrl.VoiceAnimationPlayInfo GetAnimation(int _animHash)
		{
			for (int i = 0; i < this.lstPlayInfo.Count; i++)
			{
				if (_animHash == this.lstPlayInfo[i].animationHash)
				{
					return this.lstPlayInfo[i];
				}
			}
			return null;
		}

		// Token: 0x04004C1F RID: 19487
		public List<HVoiceCtrl.VoiceAnimationPlayInfo> lstPlayInfo = new List<HVoiceCtrl.VoiceAnimationPlayInfo>();

		// Token: 0x04004C20 RID: 19488
		[Label("モーションの再生回数")]
		public int Count;
	}

	// Token: 0x02000AEB RID: 2795
	public enum VoiceKind
	{
		// Token: 0x04004C22 RID: 19490
		breath,
		// Token: 0x04004C23 RID: 19491
		breathShort,
		// Token: 0x04004C24 RID: 19492
		voice,
		// Token: 0x04004C25 RID: 19493
		startVoice,
		// Token: 0x04004C26 RID: 19494
		none
	}

	// Token: 0x02000AEC RID: 2796
	[Serializable]
	public class Voice
	{
		// Token: 0x04004C27 RID: 19495
		public HVoiceCtrl.VoiceKind state = HVoiceCtrl.VoiceKind.none;

		// Token: 0x04004C28 RID: 19496
		[Header("呼吸")]
		public HVoiceCtrl.VoiceListInfo breathInfo;

		// Token: 0x04004C29 RID: 19497
		[Label("呼吸リストの配列番号")]
		public int arrayBreath;

		// Token: 0x04004C2A RID: 19498
		[Label("呼吸アニメーションステート")]
		public string animBreath;

		// Token: 0x04004C2B RID: 19499
		[Label("呼吸グループ")]
		public int breathGroup = -1;

		// Token: 0x04004C2C RID: 19500
		[Label("速い？")]
		public bool speedStateFast;

		// Token: 0x04004C2D RID: 19501
		[Label("表情変化経過時間")]
		public float timeFaceDelta;

		// Token: 0x04004C2E RID: 19502
		[Label("表情変化時間")]
		public float timeFace;

		// Token: 0x04004C2F RID: 19503
		[Label("当たってる？")]
		public bool isGaugeHit;

		// Token: 0x04004C30 RID: 19504
		[Header("セリフ")]
		public HVoiceCtrl.VoiceListInfo voiceInfo;

		// Token: 0x04004C31 RID: 19505
		[Label("セリフリストの配列番号")]
		public int arrayVoice;

		// Token: 0x04004C32 RID: 19506
		[Label("セリフリストの番号")]
		public int VoiceListID;

		// Token: 0x04004C33 RID: 19507
		[Label("セリフリストのシート番号")]
		public int VoiceListSheetID;

		// Token: 0x04004C34 RID: 19508
		[Label("セリフアニメーションステート")]
		public string animVoice;

		// Token: 0x04004C35 RID: 19509
		[Header("短い喘ぎ")]
		public HVoiceCtrl.VoiceListInfo shortInfo;

		// Token: 0x04004C36 RID: 19510
		[Label("短い喘ぎの配列番号")]
		public int arrayShort;

		// Token: 0x04004C37 RID: 19511
		[Header("共通")]
		[Label("上書き禁止")]
		public bool notOverWrite;

		// Token: 0x04004C38 RID: 19512
		public HVoiceCtrl.FaceInfo Face = new HVoiceCtrl.FaceInfo();
	}
}
