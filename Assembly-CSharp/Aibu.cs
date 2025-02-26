using System;
using AIProject.Definitions;
using Manager;
using UnityEngine;

// Token: 0x02000AFB RID: 2811
public class Aibu : ProcBase
{
	// Token: 0x060051EF RID: 20975 RVA: 0x0021DEAC File Offset: 0x0021C2AC
	public Aibu(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[1];
		this.animPar.m = new float[1];
		this.CatID = 0;
	}

	// Token: 0x060051F0 RID: 20976 RVA: 0x0021DEFB File Offset: 0x0021C2FB
	public override bool Init(int _modeCtrl)
	{
		base.Init(_modeCtrl);
		return true;
	}

	// Token: 0x060051F1 RID: 20977 RVA: 0x0021DF08 File Offset: 0x0021C308
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		if (_isIdle)
		{
			this.setPlay((!this.ctrlFlag.isFaintness) ? "Idle" : "D_Idle", false);
			this.ctrlFlag.loopType = -1;
			this.voice.HouchiTime = 0f;
			if (this.ctrlFlag.initiative == 0)
			{
			}
		}
		else
		{
			if (this.ctrlFlag.feel_f >= 0.7f)
			{
				this.setPlay((!this.ctrlFlag.isFaintness) ? "OLoop" : "D_OLoop", false);
				this.ctrlFlag.loopType = -1;
			}
			else
			{
				this.setPlay((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", false);
				if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
				{
					this.voice.PlaySoundETC((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", 1, this.chaFemales[0], 0, false);
				}
				else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
				{
					this.voice.PlaySoundETC((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", 2, this.chaFemales[0], 0, false);
				}
				this.ctrlFlag.loopType = 0;
			}
			this.ctrlFlag.speed = 0f;
			if (_infoAnimList.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainActionParam = true;
			}
			this.ctrlFlag.motions[0] = 0f;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.auto.SetSpeed(0f);
		}
		this.nextPlay = 0;
		this.oldHit = false;
		return true;
	}

	// Token: 0x060051F2 RID: 20978 RVA: 0x0021E10C File Offset: 0x0021C50C
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaFemales[0].objTop == null)
		{
			return false;
		}
		this.FemaleAi = this.chaFemales[0].getAnimatorStateInfo(0);
		if (this.ctrlFlag.initiative == 0)
		{
			this.Manual(this.FemaleAi, _modeCtrl, _infoAnimList);
		}
		else
		{
			this.Auto(this.FemaleAi, _modeCtrl, _infoAnimList);
		}
		if (this.enableMotion)
		{
			this.timeMotion = Mathf.Clamp(this.timeMotion + Time.deltaTime, 0f, this.lerpTime);
			float num = Mathf.Clamp01(this.timeMotion / this.lerpTime);
			num = this.ctrlFlag.changeMotionCurve.Evaluate(num);
			this.ctrlFlag.motions[0] = Mathf.Lerp(this.lerpMotion.x, this.lerpMotion.y, num);
			if (num >= 1f)
			{
				this.enableMotion = false;
			}
		}
		bool flag = this.FemaleAi.IsName("WLoop") || this.FemaleAi.IsName("SLoop") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop");
		flag &= Config.HData.FinishButton;
		this.sprite.categoryFinish.SetActive(flag, 3);
		this.ctrlMeta.Proc(this.FemaleAi, false);
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.RecoverFaintness)
		{
			if (this.FemaleAi.IsName("D_Idle") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop") || this.FemaleAi.IsName("D_OLoop") || this.FemaleAi.IsName("D_Orgasm_A"))
			{
				this.setPlay("Orgasm_A", true);
				this.ctrlFlag.isFaintness = false;
				this.sprite.SetVisibleLeaveItToYou(true, true);
				this.ctrlFlag.numOrgasm = 0;
				this.sprite.SetAnimationMenu();
				this.ctrlFlag.isGaugeHit = false;
				this.sprite.SetMotionListDraw(false, -1);
				this.ctrlFlag.nowOrgasm = false;
				this.Hitem.SetUse(6, false);
				if (this.FemaleAi.IsName("D_Orgasm_A") && this.voice.playAnimation != null)
				{
					this.voice.playAnimation.SetAllFlags(true);
				}
			}
			else
			{
				this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.None;
				this.Hitem.SetUse(6, false);
			}
		}
		this.setAnimationParamater();
		return true;
	}

	// Token: 0x060051F3 RID: 20979 RVA: 0x0021E3D0 File Offset: 0x0021C7D0
	private bool Manual(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(false);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("WLoop"))
		{
			this.LoopProc(0, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.LoopProc(1, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.OLoopProc(0, num, _infoAnimList);
		}
		else if (_ai.IsName("Orgasm"))
		{
			this.OrgasmProc(0, _ai.normalizedTime, _modeCtrl);
		}
		else if (_ai.IsName("Orgasm_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.FaintnessStartProcTrigger(num, true, _modeCtrl);
			this.FaintnessStartProc(true);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopProc(0, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopProc(1, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopProc(1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm"))
		{
			this.OrgasmProc(1, _ai.normalizedTime, _modeCtrl);
		}
		else if (_ai.IsName("D_Orgasm_A"))
		{
			this.FaintnessStartProcTrigger(num, false, _modeCtrl);
			this.FaintnessStartProc(false);
		}
		return true;
	}

	// Token: 0x060051F4 RID: 20980 RVA: 0x0021E614 File Offset: 0x0021CA14
	private bool Auto(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.AutoStartProcTrigger(false);
			this.AutoStartProc(false);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.AutoLoopProc(0, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.AutoLoopProc(1, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.AutoOLoopProc(0, num, _infoAnimList);
		}
		else if (_ai.IsName("Orgasm"))
		{
			this.OrgasmProc(0, _ai.normalizedTime, _modeCtrl);
		}
		else if (_ai.IsName("Orgasm_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartProc(true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.FaintnessStartProcTrigger(num, true, _modeCtrl);
			this.FaintnessStartProc(true);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopProc(0, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopProc(1, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopProc(1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm"))
		{
			this.OrgasmProc(1, _ai.normalizedTime, _modeCtrl);
		}
		else if (_ai.IsName("D_Orgasm_A"))
		{
			this.FaintnessStartProcTrigger(num, false, _modeCtrl);
			this.FaintnessStartProc(false);
		}
		return true;
	}

	// Token: 0x060051F5 RID: 20981 RVA: 0x0021E82C File Offset: 0x0021CC2C
	public override void setAnimationParamater()
	{
		this.animPar.breast = this.chaFemales[0].GetShapeBodyValue(1);
		this.animPar.speed = ((this.ctrlFlag.loopType == 1) ? this.ctrlFlag.speed : (this.ctrlFlag.speed + 1f));
		this.animPar.m[0] = this.ctrlFlag.motions[0];
		if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
		{
			this.animPar.heights[0] = this.chaFemales[0].GetShapeBodyValue(0);
			this.chaFemales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.chaFemales[0].setAnimatorParamFloat("speed", this.animPar.speed);
			this.chaFemales[0].setAnimatorParamFloat("motion", this.animPar.m[0]);
			this.chaFemales[0].setAnimatorParamFloat("breast", this.animPar.breast);
		}
		if (this.chaMales[0].objBodyBone != null)
		{
			this.chaMales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.chaMales[0].setAnimatorParamFloat("speed", this.animPar.speed);
			this.chaMales[0].setAnimatorParamFloat("motion", this.animPar.m[0]);
			this.chaMales[0].setAnimatorParamFloat("breast", this.animPar.breast);
		}
		if (this.item.GetItem() != null)
		{
			this.item.setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.item.setAnimatorParamFloat("speed", this.animPar.speed);
			this.item.setAnimatorParamFloat("motion", this.animPar.m[0]);
		}
	}

	// Token: 0x060051F6 RID: 20982 RVA: 0x0021EA64 File Offset: 0x0021CE64
	private void setPlay(string _playAnimation, bool _isFade = true)
	{
		this.chaFemales[0].setPlay(_playAnimation, 0);
		if (this.chaMales[0].objTop != null)
		{
			this.chaMales[0].setPlay(_playAnimation, 0);
		}
		for (int i = 0; i < this.lstMotionIK.Count; i++)
		{
			this.lstMotionIK[i].Item3.Calc(_playAnimation);
		}
		if (this.item != null)
		{
			this.item.setPlay(_playAnimation);
		}
		if (_isFade)
		{
			this.fade.FadeStart(1f);
		}
	}

	// Token: 0x060051F7 RID: 20983 RVA: 0x0021EB0C File Offset: 0x0021CF0C
	private bool StartProcTrigger(float _wheel)
	{
		if (_wheel == 0f || this.nextPlay != 0)
		{
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
			{
				return false;
			}
		}
		if (this.ctrlFlag.voice.playStart > 4)
		{
			return false;
		}
		this.nextPlay = 1;
		return true;
	}

	// Token: 0x060051F8 RID: 20984 RVA: 0x0021EB98 File Offset: 0x0021CF98
	private bool StartProc(bool _isReStart)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_isReStart)
			{
				this.ctrlFlag.voice.playStart = 2;
			}
			else
			{
				this.ctrlFlag.voice.playStart = 3;
			}
			return false;
		}
		if (this.nextPlay == 2)
		{
			for (int i = 0; i < 2; i++)
			{
				if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
				{
					return false;
				}
			}
			if (this.ctrlFlag.voice.playStart > 4)
			{
				return false;
			}
		}
		this.nextPlay = 0;
		this.setPlay("WLoop", true);
		if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
		{
			this.voice.PlaySoundETC("WLoop", 1, this.chaFemales[0], 0, false);
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[0], 0, false);
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.timeMotion = 0f;
		this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
		this.timeChangeMotionDeltaTimes[0] = 0f;
		this.ctrlFlag.isNotCtrl = false;
		this.oldHit = false;
		this.feelHit.InitTime();
		if (_isReStart)
		{
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x060051F9 RID: 20985 RVA: 0x0021ED88 File Offset: 0x0021D188
	private bool FaintnessStartProcTrigger(float _wheel, bool _start, int _modeCtrl)
	{
		if (_wheel == 0f || this.nextPlay != 0)
		{
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
			{
				return false;
			}
		}
		if (this.ctrlFlag.voice.playStart > 4)
		{
			return false;
		}
		this.nextPlay = 1;
		return true;
	}

	// Token: 0x060051FA RID: 20986 RVA: 0x0021EE14 File Offset: 0x0021D214
	private bool FaintnessStartProc(bool _start)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			if (_start)
			{
				this.nextPlay = 3;
			}
			else
			{
				this.nextPlay = 2;
				if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2 == 0)
				{
					this.ctrlFlag.voice.playStart = 3;
				}
			}
			return false;
		}
		if (this.nextPlay == 2)
		{
			for (int i = 0; i < 2; i++)
			{
				if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
				{
					return false;
				}
			}
			if (this.ctrlFlag.voice.playStart > 4)
			{
				return false;
			}
		}
		this.nextPlay = 0;
		this.setPlay("D_WLoop", true);
		if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
		{
			this.voice.PlaySoundETC("D_WLoop", 1, this.chaFemales[0], 0, false);
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			this.voice.PlaySoundETC("D_WLoop", 2, this.chaFemales[0], 0, false);
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.ctrlFlag.isNotCtrl = false;
		this.oldHit = false;
		this.feelHit.InitTime();
		this.voice.AfterFinish();
		return true;
	}

	// Token: 0x060051FB RID: 20987 RVA: 0x0021EFD4 File Offset: 0x0021D3D4
	private bool LoopProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.ctrlFlag.feel_f = 0.7f;
			this.oldHit = false;
			this.ctrlFlag.isGaugeHit = false;
		}
		else
		{
			this.ctrlFlag.speed += _wheel;
			if (_loop == 0)
			{
				this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
			}
			else
			{
				this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 1.5f);
			}
			if (_state == 0 && this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
			{
				this.timeChangeMotionDeltaTimes[0] += Time.deltaTime;
				if (this.timeChangeMotions[0] <= this.timeChangeMotionDeltaTimes[0] && !this.enableMotion)
				{
					this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
					this.timeChangeMotionDeltaTimes[0] = 0f;
					this.enableMotion = true;
					this.timeMotion = 0f;
					float num;
					if (this.allowMotion)
					{
						num = 1f - this.ctrlFlag.motions[0];
						if (num <= this.ctrlFlag.changeMotionMinRate)
						{
							num = 1f;
						}
						else
						{
							num = this.ctrlFlag.motions[0] + UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
						}
						if (num >= 1f)
						{
							this.allowMotion = false;
						}
					}
					else
					{
						num = this.ctrlFlag.motions[0];
						if (num <= this.ctrlFlag.changeMotionMinRate)
						{
							num = 0f;
						}
						else
						{
							num = this.ctrlFlag.motions[0] - UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
						}
						if (num <= 0f)
						{
							this.allowMotion = true;
						}
					}
					this.lerpMotion = new Vector2(this.ctrlFlag.motions[0], num);
					this.lerpTime = UnityEngine.Random.Range(this.ctrlFlag.changeMotionTimeMin, this.ctrlFlag.changeMotionTimeMax);
				}
			}
			if (_loop == 0)
			{
				if (this.ctrlFlag.speed > 1f && this.ctrlFlag.loopType == 0)
				{
					this.setPlay((_state != 0) ? "D_SLoop" : "SLoop", true);
					this.ctrlFlag.nowSpeedStateFast = false;
					this.feelHit.InitTime();
					this.ctrlFlag.loopType = 1;
				}
				this.ctrlFlag.speed = Mathf.Clamp(this.ctrlFlag.speed, 0f, 2f);
			}
			else
			{
				if (this.ctrlFlag.speed < 1f && this.ctrlFlag.loopType == 1)
				{
					this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
					this.ctrlFlag.nowSpeedStateFast = true;
					this.feelHit.InitTime();
					this.ctrlFlag.loopType = 0;
				}
				this.ctrlFlag.speed = Mathf.Clamp(this.ctrlFlag.speed, 0f, 2f);
			}
			this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, _loop, (_loop != 0) ? (this.ctrlFlag.speed - 1f) : this.ctrlFlag.speed);
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			if (this.ctrlFlag.isGaugeHit)
			{
				this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
				float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num2 *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant)
				{
					if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(0);
					}
					else if (ProcBase.hSceneManager.HSkil.ContainsValue(6) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(6);
					}
					else if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(15);
					}
					if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(3);
					}
				}
				this.ctrlFlag.feel_f += num2;
				this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
			}
			if (this.ctrlFlag.isGaugeHit != this.oldHit && this.ctrlFlag.isGaugeHit)
			{
				if (this.randVoicePlays[0].Get() == 0)
				{
					this.ctrlFlag.voice.playVoices[0] = true;
				}
				else if (this.randVoicePlays[1].Get() == 0)
				{
					this.ctrlFlag.voice.playVoices[1] = true;
				}
				if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0))
				{
					if (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 3)
					{
						this.ctrlFlag.voice.playShorts[0] = 0;
					}
					if (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 2)
					{
						this.ctrlFlag.voice.playShorts[1] = 0;
					}
				}
				this.ctrlFlag.voice.dialog = false;
			}
			this.oldHit = this.ctrlFlag.isGaugeHit;
			if (this.ctrlFlag.feel_f >= 0.7f)
			{
				this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = -1;
				this.ctrlFlag.nowSpeedStateFast = false;
				this.oldHit = false;
				this.feelHit.InitTime();
			}
		}
		return true;
	}

	// Token: 0x060051FC RID: 20988 RVA: 0x0021F6E4 File Offset: 0x0021DAE4
	private bool OLoopProc(int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		float num = 0f;
		this.ctrlFlag.speed = Mathf.Clamp01(this.ctrlFlag.speed + _wheel);
		this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, 2, this.ctrlFlag.speed);
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.isGaugeHit)
		{
			num += Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant)
			{
				if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(0);
				}
				else if (ProcBase.hSceneManager.HSkil.ContainsValue(6) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(6);
				}
				else if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(15);
				}
				if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(3);
				}
			}
			this.ctrlFlag.feel_f += num;
			this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
		}
		if (this.ctrlFlag.isGaugeHit != this.oldHit && this.ctrlFlag.isGaugeHit)
		{
			if (this.randVoicePlays[0].Get() == 0)
			{
				this.ctrlFlag.voice.playVoices[0] = true;
			}
			else if (this.randVoicePlays[1].Get() == 0)
			{
				this.ctrlFlag.voice.playVoices[1] = true;
			}
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0))
			{
				if (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 3)
				{
					this.ctrlFlag.voice.playShorts[0] = 0;
				}
				if (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 2)
				{
					this.ctrlFlag.voice.playShorts[1] = 0;
				}
			}
			this.ctrlFlag.voice.dialog = false;
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_Orgasm" : "Orgasm", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.voice.oldFinish = 0;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 3 && this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2 == 3)
			{
				this.ctrlFlag.AddParam(22, 1);
			}
			else if (item == 0 && id == 16)
			{
				this.ctrlFlag.AddParam(24, 1);
			}
			else if (item == 0 && id == 100)
			{
				this.ctrlFlag.AddParam(44, 1);
			}
			else if (item == 0 && id == 101)
			{
				this.ctrlFlag.AddParam(52, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
			}
			this.ctrlFlag.voice.dialog = false;
			this.ctrlFlag.rateNip = 1f;
			if (Config.HData.Gloss)
			{
				this.ctrlFlag.rateTuya = 1f;
			}
			bool sio = Config.HData.Sio;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				bool flag = this.Hitem.Effect(5);
				if (sio)
				{
					this.particle.Play(0);
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
				}
			}
			else if (sio)
			{
				this.particle.Play(0);
			}
			bool urine = Config.HData.Urine;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
				}
			}
			else if (urine)
			{
				this.particle.Play(1);
				this.ctrlFlag.numUrine++;
				this.ctrlFlag.voice.urines[0] = true;
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
		}
		return true;
	}

	// Token: 0x060051FD RID: 20989 RVA: 0x0021FE58 File Offset: 0x0021E258
	private bool GotoFaintness(int _state)
	{
		bool flag = this.Hitem.Effect(5);
		if (_state == 0 && (this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount || flag))
		{
			this.setPlay("D_Orgasm_A", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (flag)
			{
				this.Hitem.SetUse(5, false);
			}
			if (!ProcBase.hSceneManager.bMerchant && this.ctrlFlag.numFaintness == 0)
			{
				this.ctrlFlag.AddParam(14, 1);
			}
			this.ctrlFlag.isFaintness = true;
			this.ctrlFlag.numFaintness = Mathf.Clamp(this.ctrlFlag.numFaintness + 1, 0, 999999);
			this.sprite.SetVisibleLeaveItToYou(false, false);
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.GyakuYobai)
			{
				this.sprite.categoryMainButton.interactable = true;
				this.sprite.hPointButton.interactable = true;
			}
			this.sprite.SetToggleLeaveItToYou(false);
			if (this.ctrlFlag.initiative != 0)
			{
				this.ctrlFlag.initiative = 0;
				this.sprite.MainCategoryOfLeaveItToYou(false);
			}
		}
		else
		{
			this.setPlay((_state != 0) ? "D_Orgasm_A" : "Orgasm_A", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		this.ctrlFlag.nowOrgasm = false;
		return true;
	}

	// Token: 0x060051FE RID: 20990 RVA: 0x0021FFF0 File Offset: 0x0021E3F0
	private bool OrgasmProc(int _state, float _normalizedTime, int _modeCtrl)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		if ((this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 0 && _modeCtrl != 1) || this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3)
		{
			this.GotoFaintness(_state);
		}
		else
		{
			this.setPlay((_state != 0) ? "D_Orgasm_A" : "Orgasm_A", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		return true;
	}

	// Token: 0x060051FF RID: 20991 RVA: 0x0022008C File Offset: 0x0021E48C
	private bool AutoStartProcTrigger(bool _start)
	{
		if (this.nextPlay != 0)
		{
			return false;
		}
		if (!_start)
		{
			if (!this.auto.IsStart())
			{
				return false;
			}
		}
		else if (!this.auto.IsReStart())
		{
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
			{
				return false;
			}
		}
		this.nextPlay = 1;
		return true;
	}

	// Token: 0x06005200 RID: 20992 RVA: 0x00220124 File Offset: 0x0021E524
	private bool AutoStartProc(bool _isReStart)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			if (!_isReStart)
			{
				this.nextPlay = 3;
			}
			else
			{
				this.nextPlay = 2;
				this.ctrlFlag.voice.playStart = 3;
			}
			return false;
		}
		if (this.nextPlay == 2)
		{
			for (int i = 0; i < 2; i++)
			{
				if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
				{
					return false;
				}
			}
		}
		this.nextPlay = 0;
		if (!_isReStart || (_isReStart && !this.auto.IsChangeActionAtRestart()))
		{
			this.setPlay("WLoop", true);
			if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
			{
				this.voice.PlaySoundETC("WLoop", 1, this.chaFemales[0], 0, false);
			}
			else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
			{
				this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[0], 0, false);
			}
		}
		else
		{
			this.ctrlFlag.isAutoActionChange = true;
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.oldHit = false;
		this.timeMotion = 0f;
		this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
		this.timeChangeMotionDeltaTimes[0] = 0f;
		this.ctrlFlag.isNotCtrl = false;
		this.auto.Reset();
		this.feelHit.InitTime();
		if (_isReStart)
		{
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005201 RID: 20993 RVA: 0x00220340 File Offset: 0x0021E740
	private bool AutoLoopProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.feel_f = 0.7f;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.auto.SetSpeed(0f);
			this.oldHit = false;
			this.feelHit.InitTime();
			this.ctrlFlag.isGaugeHit = false;
		}
		else
		{
			if (_state == 0 && this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
			{
				this.timeChangeMotionDeltaTimes[0] += Time.deltaTime;
				if (this.timeChangeMotions[0] <= this.timeChangeMotionDeltaTimes[0] && !this.enableMotion)
				{
					this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
					this.timeChangeMotionDeltaTimes[0] = 0f;
					this.enableMotion = true;
					this.timeMotion = 0f;
					float num;
					if (this.allowMotion)
					{
						num = 1f - this.ctrlFlag.motions[0];
						if (num <= this.ctrlFlag.changeMotionMinRate)
						{
							num = 1f;
						}
						else
						{
							num = this.ctrlFlag.motions[0] + UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
						}
						if (num >= 1f)
						{
							this.allowMotion = false;
						}
					}
					else
					{
						num = this.ctrlFlag.motions[0];
						if (num <= this.ctrlFlag.changeMotionMinRate)
						{
							num = 0f;
						}
						else
						{
							num = this.ctrlFlag.motions[0] - UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
						}
						if (num <= 0f)
						{
							this.allowMotion = true;
						}
					}
					this.lerpMotion = new Vector2(this.ctrlFlag.motions[0], num);
					this.lerpTime = UnityEngine.Random.Range(this.ctrlFlag.changeMotionTimeMin, this.ctrlFlag.changeMotionTimeMax);
				}
			}
			this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
			Vector2 hitArea = this.feelHit.GetHitArea(_infoAnimList.nFeelHit, _loop);
			if (this.auto.ChangeLoopMotion(_loop == 1))
			{
				this.setPlay((_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop"), true);
				if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice))
				{
					Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
				}
				this.feelHit.InitTime();
			}
			else
			{
				this.auto.ChangeSpeed(_loop == 1, hitArea);
				if (this.auto.AddSpeed(_wheel, _loop))
				{
					this.setPlay((_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop"), true);
					if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice))
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
					}
					this.feelHit.InitTime();
				}
			}
			if (_loop == 0)
			{
				this.ctrlFlag.speed = this.auto.GetSpeed(false);
				this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
			}
			else
			{
				this.ctrlFlag.speed = this.auto.GetSpeed(true);
				this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
			}
			this.ctrlFlag.isGaugeHit = GlobalMethod.RangeOn<float>(this.ctrlFlag.speed, hitArea.x, hitArea.y);
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			if (_state == 1)
			{
				if (this.ctrlFlag.isGaugeHit)
				{
					float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
					num2 *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
					if (!ProcBase.hSceneManager.bMerchant)
					{
						if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
						{
							num2 *= this.ctrlFlag.SkilChangeSpeed(0);
						}
						else if (ProcBase.hSceneManager.HSkil.ContainsValue(6) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
						{
							num2 *= this.ctrlFlag.SkilChangeSpeed(6);
						}
						else if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
						{
							num2 *= this.ctrlFlag.SkilChangeSpeed(15);
						}
						if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
						{
							num2 *= this.ctrlFlag.SkilChangeSpeed(3);
						}
					}
					this.ctrlFlag.feel_f += num2;
					this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
				}
			}
			else
			{
				float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num2 *= ((!this.ctrlFlag.isGaugeHit) ? 0.3f : 1f) * (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant)
				{
					if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(0);
					}
					else if (ProcBase.hSceneManager.HSkil.ContainsValue(6) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(6);
					}
					else if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(15);
					}
					if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(3);
					}
				}
				this.ctrlFlag.feel_f += num2;
				this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
			}
			if (this.ctrlFlag.selectAnimationListInfo == null)
			{
				this.ctrlFlag.isAutoActionChange = this.auto.IsChangeActionAtLoop();
			}
			if (this.ctrlFlag.isGaugeHit != this.oldHit && this.ctrlFlag.isGaugeHit && !this.ctrlFlag.isAutoActionChange)
			{
				if (this.randVoicePlays[0].Get() == 0)
				{
					this.ctrlFlag.voice.playVoices[0] = true;
				}
				else if (this.randVoicePlays[1].Get() == 0)
				{
					this.ctrlFlag.voice.playVoices[1] = true;
				}
				if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0))
				{
					this.ctrlFlag.voice.playShorts[0] = 0;
					this.ctrlFlag.voice.playShorts[1] = 0;
				}
			}
			this.oldHit = this.ctrlFlag.isGaugeHit;
			if (this.ctrlFlag.feel_f >= 0.7f)
			{
				this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = -1;
				this.ctrlFlag.nowSpeedStateFast = false;
				this.oldHit = false;
				this.auto.SetSpeed(0f);
				this.feelHit.InitTime();
			}
		}
		return true;
	}

	// Token: 0x06005202 RID: 20994 RVA: 0x00220C98 File Offset: 0x0021F098
	private bool AutoOLoopProc(int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		Vector2 hitArea = this.feelHit.GetHitArea(_infoAnimList.nFeelHit, 2);
		this.auto.ChangeSpeed(false, hitArea);
		this.auto.AddSpeed(_wheel, 2);
		this.ctrlFlag.speed = this.auto.GetSpeed(false);
		this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
		this.ctrlFlag.isGaugeHit = GlobalMethod.RangeOn<float>(this.ctrlFlag.speed, hitArea.x, hitArea.y);
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
		if (_state == 1)
		{
			if (this.ctrlFlag.isGaugeHit)
			{
				float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant)
				{
					if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
					{
						num *= this.ctrlFlag.SkilChangeSpeed(0);
					}
					else if (ProcBase.hSceneManager.HSkil.ContainsValue(6) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
					{
						num *= this.ctrlFlag.SkilChangeSpeed(6);
					}
					else if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
					{
						num *= this.ctrlFlag.SkilChangeSpeed(15);
					}
					if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
					{
						num *= this.ctrlFlag.SkilChangeSpeed(3);
					}
				}
				this.ctrlFlag.feel_f += num;
				this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
			}
		}
		else
		{
			float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num *= ((!this.ctrlFlag.isGaugeHit) ? 0.3f : 1f) * (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant)
			{
				if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(0);
				}
				else if (ProcBase.hSceneManager.HSkil.ContainsValue(6) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(6);
				}
				else if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(15);
				}
				if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(3);
				}
			}
			this.ctrlFlag.feel_f += num;
			this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
		}
		if (this.ctrlFlag.isGaugeHit != this.oldHit && this.ctrlFlag.isGaugeHit && !this.ctrlFlag.isAutoActionChange)
		{
			if (this.randVoicePlays[0].Get() == 0)
			{
				this.ctrlFlag.voice.playVoices[0] = true;
			}
			else if (this.randVoicePlays[1].Get() == 0)
			{
				this.ctrlFlag.voice.playVoices[1] = true;
			}
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0))
			{
				this.ctrlFlag.voice.playShorts[0] = 0;
				this.ctrlFlag.voice.playShorts[1] = 0;
			}
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_Orgasm" : "Orgasm", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 3 && this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2 == 3)
			{
				this.ctrlFlag.AddParam(22, 1);
			}
			else if (item == 0 && id == 16)
			{
				this.ctrlFlag.AddParam(24, 1);
			}
			else if (item == 0 && id == 100)
			{
				this.ctrlFlag.AddParam(44, 1);
			}
			else if (item == 0 && id == 101)
			{
				this.ctrlFlag.AddParam(52, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
			}
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.voice.oldFinish = 0;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.ctrlFlag.rateNip = 1f;
			if (Config.HData.Gloss)
			{
				this.ctrlFlag.rateTuya = 1f;
			}
			bool sio = Config.HData.Sio;
			bool urine = Config.HData.Urine;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				bool flag = this.Hitem.Effect(5);
				if (sio)
				{
					this.particle.Play(0);
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
				}
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
				}
			}
			else
			{
				if (sio)
				{
					this.particle.Play(0);
				}
				if (urine)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
				}
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
		}
		return true;
	}

	// Token: 0x04004CA3 RID: 19619
	private float timeMotion;

	// Token: 0x04004CA4 RID: 19620
	private bool enableMotion;

	// Token: 0x04004CA5 RID: 19621
	private bool allowMotion = true;

	// Token: 0x04004CA6 RID: 19622
	private Vector2 lerpMotion = Vector2.zero;

	// Token: 0x04004CA7 RID: 19623
	private float lerpTime;

	// Token: 0x04004CA8 RID: 19624
	private int nextPlay;

	// Token: 0x04004CA9 RID: 19625
	private bool oldHit;

	// Token: 0x04004CAA RID: 19626
	private ProcBase.animParm animPar;
}
