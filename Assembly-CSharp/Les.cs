using System;
using AIProject.Definitions;
using Manager;
using UnityEngine;

// Token: 0x02000AFD RID: 2813
public class Les : ProcBase
{
	// Token: 0x06005217 RID: 21015 RVA: 0x00224C74 File Offset: 0x00223074
	public Les(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[2];
		this.animPar.m = new float[2];
		this.CatID = 6;
	}

	// Token: 0x06005218 RID: 21016 RVA: 0x00224D17 File Offset: 0x00223117
	public override bool Init(int _modeCtrl)
	{
		base.Init(_modeCtrl);
		return true;
	}

	// Token: 0x06005219 RID: 21017 RVA: 0x00224D24 File Offset: 0x00223124
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		if (_isIdle)
		{
			if (_infoAnimList.nDownPtn != 0)
			{
				this.setPlay((!this.ctrlFlag.isFaintness) ? "Idle" : "D_Idle", false);
			}
			else
			{
				this.setPlay("Idle", false);
			}
			this.voice.HouchiTime = 0f;
			this.ctrlFlag.loopType = -1;
			if (this.ctrlFlag.initiative == 0)
			{
			}
		}
		else
		{
			if (this.ctrlFlag.feel_f >= 0.7f)
			{
				if (_infoAnimList.nDownPtn != 0)
				{
					this.setPlay((!this.ctrlFlag.isFaintness) ? "OLoop" : "D_OLoop", false);
				}
				else
				{
					this.setPlay("OLoop", false);
				}
				this.ctrlFlag.loopType = -1;
			}
			else
			{
				if (_infoAnimList.nDownPtn != 0)
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
				}
				else
				{
					this.setPlay("WLoop", false);
					if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
					{
						this.voice.PlaySoundETC("WLoop", 1, this.chaFemales[0], 0, false);
					}
					else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
					{
						this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[0], 0, false);
					}
				}
				this.ctrlFlag.loopType = 0;
			}
			if (_infoAnimList.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainActionParam = true;
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.motions[0] = 0f;
			this.ctrlFlag.motions[1] = 0f;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.auto.SetSpeed(0f);
		}
		this.nextPlay = 0;
		this.oldHit = false;
		this.isHeight1Parameter = this.chaFemales[0].IsParameterInAnimator("height1");
		return true;
	}

	// Token: 0x0600521A RID: 21018 RVA: 0x00225018 File Offset: 0x00223418
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaFemales[0].objTop == null)
		{
			return false;
		}
		this.FemaleAi = this.chaFemales[0].getAnimatorStateInfo(0);
		if (this.ctrlFlag.initiative == 0)
		{
			this.Manual(this.FemaleAi, _infoAnimList);
		}
		else
		{
			this.Auto(this.FemaleAi, _infoAnimList);
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.enableMotions[i])
			{
				this.timeMotions[i] = Mathf.Clamp(this.timeMotions[i] + Time.deltaTime, 0f, this.lerpTimes[i]);
				float num = Mathf.Clamp01(this.timeMotions[i] / this.lerpTimes[i]);
				num = this.ctrlFlag.changeMotionCurve.Evaluate(num);
				this.ctrlFlag.motions[i] = Mathf.Lerp(this.lerpMotions[i].x, this.lerpMotions[i].y, num);
				if (num >= 1f)
				{
					this.enableMotions[i] = false;
				}
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

	// Token: 0x0600521B RID: 21019 RVA: 0x00225308 File Offset: 0x00223708
	private bool Manual(AnimatorStateInfo _ai, HScene.AnimationListInfo _infoAnimList)
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
			this.OrgasmProc(0, _ai.normalizedTime);
		}
		else if (_ai.IsName("Orgasm_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.FaintnessStartProcTrigger(num, true, _infoAnimList.nDownPtn != 0);
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
			this.OrgasmProc(1, _ai.normalizedTime);
		}
		else if (_ai.IsName("D_Orgasm_A"))
		{
			this.FaintnessStartProcTrigger(num, false, _infoAnimList.nDownPtn != 0);
			this.FaintnessStartProc(false);
		}
		return true;
	}

	// Token: 0x0600521C RID: 21020 RVA: 0x00225560 File Offset: 0x00223960
	private bool Auto(AnimatorStateInfo _ai, HScene.AnimationListInfo _infoAnimList)
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
			this.OrgasmProc(0, _ai.normalizedTime);
		}
		else if (_ai.IsName("Orgasm_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartProc(true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.FaintnessStartProcTrigger(num, true, _infoAnimList.nDownPtn != 0);
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
			this.OrgasmProc(1, _ai.normalizedTime);
		}
		else if (_ai.IsName("D_Orgasm_A"))
		{
			this.FaintnessStartProcTrigger(num, false, _infoAnimList.nDownPtn != 0);
			this.FaintnessStartProc(false);
		}
		return true;
	}

	// Token: 0x0600521D RID: 21021 RVA: 0x0022578C File Offset: 0x00223B8C
	public override void setAnimationParamater()
	{
		this.animPar.breast = this.chaFemales[0].GetShapeBodyValue(1);
		this.animPar.speed = ((this.ctrlFlag.loopType == 1) ? this.ctrlFlag.speed : (this.ctrlFlag.speed + 1f));
		this.animPar.m[0] = this.ctrlFlag.motions[0];
		this.animPar.m[1] = this.ctrlFlag.motions[1];
		for (int i = 0; i < this.chaFemales.Length; i++)
		{
			if (this.chaFemales[1].visibleAll && !(this.chaFemales[i].objBodyBone == null))
			{
				if (!this.chaFemales[i].isPlayer)
				{
					this.animPar.heights[i] = this.chaFemales[i].GetShapeBodyValue(0);
				}
				else
				{
					this.animPar.heights[i] = 0.75f;
				}
			}
		}
		for (int j = 0; j < this.chaFemales.Length; j++)
		{
			if (this.chaFemales[j].visibleAll && !(this.chaFemales[j].objTop == null))
			{
				this.chaFemales[j].setAnimatorParamFloat("height", this.animPar.heights[j]);
				this.chaFemales[j].setAnimatorParamFloat("speed", this.animPar.speed);
				this.chaFemales[j].setAnimatorParamFloat("motion", this.animPar.m[j]);
				this.chaFemales[j].setAnimatorParamFloat("breast", this.animPar.breast);
				if (this.isHeight1Parameter)
				{
					this.chaFemales[j].setAnimatorParamFloat("height1", this.animPar.heights[j ^ 1]);
				}
			}
		}
		if (this.item.GetItem() != null)
		{
			this.item.setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.item.setAnimatorParamFloat("speed", this.animPar.speed);
			this.item.setAnimatorParamFloat("motion", this.animPar.m[0]);
			if (this.isHeight1Parameter)
			{
				this.item.setAnimatorParamFloat("height1", this.animPar.heights[1]);
			}
		}
	}

	// Token: 0x0600521E RID: 21022 RVA: 0x00225A30 File Offset: 0x00223E30
	private void setPlay(string _playAnimation, bool _isFade = true)
	{
		this.chaFemales[0].setPlay(_playAnimation, 0);
		if (this.chaFemales[1].visibleAll && this.chaFemales[1].objTop != null)
		{
			this.chaFemales[1].setPlay(_playAnimation, 0);
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

	// Token: 0x0600521F RID: 21023 RVA: 0x00225AE8 File Offset: 0x00223EE8
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

	// Token: 0x06005220 RID: 21024 RVA: 0x00225B74 File Offset: 0x00223F74
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
		this.ctrlFlag.motions[1] = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		for (int j = 0; j < 2; j++)
		{
			this.timeMotions[j] = 0f;
			this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[j] = 0f;
		}
		this.ctrlFlag.isNotCtrl = false;
		this.oldHit = false;
		this.feelHit.InitTime();
		if (_isReStart)
		{
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005221 RID: 21025 RVA: 0x00225D88 File Offset: 0x00224188
	private bool FaintnessStartProcTrigger(float _wheel, bool _start, bool canFaintNess)
	{
		if (_wheel == 0f || this.nextPlay != 0)
		{
			return false;
		}
		if (!_start && !canFaintNess)
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

	// Token: 0x06005222 RID: 21026 RVA: 0x00225E20 File Offset: 0x00224220
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
		this.ctrlFlag.motions[1] = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.ctrlFlag.isNotCtrl = false;
		this.oldHit = false;
		this.feelHit.InitTime();
		this.voice.AfterFinish();
		return true;
	}

	// Token: 0x06005223 RID: 21027 RVA: 0x00225FF0 File Offset: 0x002243F0
	private bool LoopProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		float num = 0f;
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
			if (_state == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					if (this.chaFemales[i].visibleAll && !(this.chaFemales[i].objBodyBone == null))
					{
						this.timeChangeMotionDeltaTimes[i] += Time.deltaTime;
						if (this.timeChangeMotions[i] <= this.timeChangeMotionDeltaTimes[i] && !this.enableMotions[i])
						{
							this.timeChangeMotions[i] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
							this.timeChangeMotionDeltaTimes[i] = 0f;
							this.enableMotions[i] = true;
							this.timeMotions[i] = 0f;
							float num2;
							if (this.allowMotions[i])
							{
								num2 = 1f - this.ctrlFlag.motions[i];
								if (num2 <= this.ctrlFlag.changeMotionMinRate)
								{
									num2 = 1f;
								}
								else
								{
									num2 = this.ctrlFlag.motions[i] + UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num2);
								}
								if (num2 >= 1f)
								{
									this.allowMotions[i] = false;
								}
							}
							else
							{
								num2 = this.ctrlFlag.motions[i];
								if (num2 <= this.ctrlFlag.changeMotionMinRate)
								{
									num2 = 0f;
								}
								else
								{
									num2 = this.ctrlFlag.motions[i] - UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num2);
								}
								if (num2 <= 0f)
								{
									this.allowMotions[i] = true;
								}
							}
							this.lerpMotions[i] = new Vector2(this.ctrlFlag.motions[i], num2);
							this.lerpTimes[i] = UnityEngine.Random.Range(this.ctrlFlag.changeMotionTimeMin, this.ctrlFlag.changeMotionTimeMax);
						}
					}
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
				num += Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant)
				{
					if (ProcBase.hSceneManager.HSkil.ContainsValue(0) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(2))
					{
						num *= this.ctrlFlag.SkilChangeSpeed(0);
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
						this.ctrlFlag.voice.playShorts[0] = 1;
					}
					if (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 2)
					{
						this.ctrlFlag.voice.playShorts[1] = 0;
						this.ctrlFlag.voice.playShorts[1] = 1;
					}
				}
				this.ctrlFlag.voice.dialog = false;
				if (this.ctrlFlag.voice.playVoices[1] && ProcBase.hSceneManager.Agent[1] == null)
				{
					this.ctrlFlag.voice.dialog = true;
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
				this.feelHit.InitTime();
			}
		}
		return true;
	}

	// Token: 0x06005224 RID: 21028 RVA: 0x0022670C File Offset: 0x00224B0C
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
					this.ctrlFlag.voice.playShorts[0] = 1;
				}
				if (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 2)
				{
					this.ctrlFlag.voice.playShorts[1] = 0;
					this.ctrlFlag.voice.playShorts[1] = 1;
				}
			}
			this.ctrlFlag.voice.dialog = false;
			if (this.ctrlFlag.voice.playVoices[1] && ProcBase.hSceneManager.Agent[1] == null)
			{
				this.ctrlFlag.voice.dialog = true;
			}
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_Orgasm" : "Orgasm", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.voice.oldFinish = 0;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			if (ProcBase.hSceneManager.Player.ChaControl.sex == 1 && !ProcBase.hSceneManager.bFutanari)
			{
				this.ctrlFlag.numOrgasmFemalePlayer = Mathf.Clamp(this.ctrlFlag.numOrgasmFemalePlayer + 1, 0, 10);
			}
			this.ctrlFlag.nowOrgasm = true;
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
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.id == 0)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
				}
			}
			else if (sio)
			{
				this.particle.Play(0);
				if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.id == 0)
				{
					this.particle.Play(4);
				}
			}
			bool urine = Config.HData.Urine;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (urine && this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.id == 0)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					this.ctrlFlag.numUrine++;
				}
			}
			else if (urine)
			{
				this.particle.Play(1);
				this.ctrlFlag.voice.urines[0] = true;
				if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
				{
					this.particle.Play(5);
					this.ctrlFlag.voice.urines[1] = true;
				}
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
		}
		return true;
	}

	// Token: 0x06005225 RID: 21029 RVA: 0x00226F00 File Offset: 0x00225300
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

	// Token: 0x06005226 RID: 21030 RVA: 0x00227096 File Offset: 0x00225496
	private bool OrgasmProc(int _state, float _normalizedTime)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		this.GotoFaintness(_state);
		return true;
	}

	// Token: 0x06005227 RID: 21031 RVA: 0x002270B0 File Offset: 0x002254B0
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

	// Token: 0x06005228 RID: 21032 RVA: 0x00227148 File Offset: 0x00225548
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
				if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
				{
					return false;
				}
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
		this.ctrlFlag.motions[1] = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.oldHit = false;
		for (int j = 0; j < 2; j++)
		{
			this.timeMotions[j] = 0f;
			this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[j] = 0f;
		}
		this.ctrlFlag.isNotCtrl = false;
		this.auto.Reset();
		this.feelHit.InitTime();
		if (_isReStart)
		{
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005229 RID: 21033 RVA: 0x00227348 File Offset: 0x00225748
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
			if (_state == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					if (this.chaFemales[i].visibleAll && !(this.chaFemales[i].objBodyBone == null))
					{
						this.timeChangeMotionDeltaTimes[i] += Time.deltaTime;
						if (this.timeChangeMotions[i] <= this.timeChangeMotionDeltaTimes[i] && !this.enableMotions[i])
						{
							this.timeChangeMotions[i] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
							this.timeChangeMotionDeltaTimes[i] = 0f;
							this.enableMotions[i] = true;
							this.timeMotions[i] = 0f;
							float num;
							if (this.allowMotions[i])
							{
								num = 1f - this.ctrlFlag.motions[i];
								if (num <= this.ctrlFlag.changeMotionMinRate)
								{
									num = 1f;
								}
								else
								{
									num = this.ctrlFlag.motions[i] + UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
								}
								if (num >= 1f)
								{
									this.allowMotions[i] = false;
								}
							}
							else
							{
								num = this.ctrlFlag.motions[i];
								if (num <= this.ctrlFlag.changeMotionMinRate)
								{
									num = 0f;
								}
								else
								{
									num = this.ctrlFlag.motions[i] - UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
								}
								if (num <= 0f)
								{
									this.allowMotions[i] = true;
								}
							}
							this.lerpMotions[i] = new Vector2(this.ctrlFlag.motions[i], num);
							this.lerpTimes[i] = UnityEngine.Random.Range(this.ctrlFlag.changeMotionTimeMin, this.ctrlFlag.changeMotionTimeMax);
						}
					}
				}
			}
			this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
			Vector2 hitArea = this.feelHit.GetHitArea(_infoAnimList.nFeelHit, _loop);
			if (this.auto.ChangeLoopMotion(_loop == 1))
			{
				this.setPlay((_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop"), true);
				for (int j = 0; j < 2; j++)
				{
					if (this.chaFemales[j].visibleAll && !(this.chaFemales[j].objBodyBone == null))
					{
						if (this.voice.nowVoices[j].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[j].state == HVoiceCtrl.VoiceKind.startVoice)
						{
							Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[j]);
						}
					}
				}
				this.feelHit.InitTime();
			}
			else
			{
				this.auto.ChangeSpeed(_loop == 1, hitArea);
				if (this.auto.AddSpeed(_wheel, _loop))
				{
					this.setPlay((_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop"), true);
					for (int k = 0; k < 2; k++)
					{
						if (this.chaFemales[k].visibleAll && !(this.chaFemales[k].objBodyBone == null))
						{
							if (this.voice.nowVoices[k].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[k].state == HVoiceCtrl.VoiceKind.startVoice)
							{
								Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[k]);
							}
						}
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
					this.ctrlFlag.voice.playShorts[0] = 1;
					this.ctrlFlag.voice.playShorts[1] = 0;
					this.ctrlFlag.voice.playShorts[1] = 1;
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

	// Token: 0x0600522A RID: 21034 RVA: 0x00227BB4 File Offset: 0x00225FB4
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
				this.ctrlFlag.voice.playShorts[0] = 1;
				this.ctrlFlag.voice.playShorts[1] = 0;
				this.ctrlFlag.voice.playShorts[1] = 1;
			}
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_Orgasm" : "Orgasm", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.voice.oldFinish = 0;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			if (ProcBase.hSceneManager.Player.ChaControl.sex == 1)
			{
				this.ctrlFlag.numOrgasmFemalePlayer = Mathf.Clamp(this.ctrlFlag.numOrgasmFemalePlayer + 1, 0, 10);
			}
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
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
				}
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (urine && this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
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
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				if (urine)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
				}
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
		}
		return true;
	}

	// Token: 0x04004CB6 RID: 19638
	private float[] timeMotions = new float[2];

	// Token: 0x04004CB7 RID: 19639
	private bool[] enableMotions = new bool[2];

	// Token: 0x04004CB8 RID: 19640
	private bool[] allowMotions = new bool[]
	{
		true,
		true
	};

	// Token: 0x04004CB9 RID: 19641
	private Vector2[] lerpMotions = new Vector2[]
	{
		Vector2.zero,
		Vector2.zero
	};

	// Token: 0x04004CBA RID: 19642
	private float[] lerpTimes = new float[2];

	// Token: 0x04004CBB RID: 19643
	private int nextPlay;

	// Token: 0x04004CBC RID: 19644
	private bool oldHit;

	// Token: 0x04004CBD RID: 19645
	private ProcBase.animParm animPar;
}
