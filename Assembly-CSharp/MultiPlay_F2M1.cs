using System;
using System.Collections.Generic;
using AIProject.Definitions;
using Manager;
using UnityEngine;
using UnityEx;

// Token: 0x02000AFF RID: 2815
public class MultiPlay_F2M1 : ProcBase
{
	// Token: 0x06005238 RID: 21048 RVA: 0x00229D80 File Offset: 0x00228180
	public MultiPlay_F2M1(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[2];
		this.animPar.m = new float[2];
		this.CatID = 7;
	}

	// Token: 0x06005239 RID: 21049 RVA: 0x00229E23 File Offset: 0x00228223
	public override bool Init(int _modeCtrl)
	{
		base.Init(_modeCtrl);
		return true;
	}

	// Token: 0x0600523A RID: 21050 RVA: 0x00229E2E File Offset: 0x0022822E
	public void SetAnimationList(List<HScene.AnimationListInfo> _list)
	{
		this.lstAnimation = _list;
	}

	// Token: 0x0600523B RID: 21051 RVA: 0x00229E38 File Offset: 0x00228238
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
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
						{
							this.voice.PlaySoundETC((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", 1, this.chaFemales[0], 0, false);
						}
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
						{
							this.voice.PlaySoundETC((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", 1, this.chaFemales[1], 1, false);
						}
					}
					else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
					{
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
						{
							this.voice.PlaySoundETC((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", 2, this.chaFemales[0], 0, false);
						}
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
						{
							this.voice.PlaySoundETC((!this.ctrlFlag.isFaintness) ? "WLoop" : "D_WLoop", 2, this.chaFemales[1], 1, false);
						}
					}
				}
				else
				{
					this.setPlay("WLoop", false);
					if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
					{
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
						{
							this.voice.PlaySoundETC("WLoop", 1, this.chaFemales[0], 0, false);
						}
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
						{
							this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[1], 1, false);
						}
					}
					else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
					{
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
						{
							this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[0], 0, false);
						}
						if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
						{
							this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[1], 1, false);
						}
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

	// Token: 0x0600523C RID: 21052 RVA: 0x0022A2C4 File Offset: 0x002286C4
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaFemales[0].objTop == null)
		{
			return false;
		}
		this.FemaleAi = this.chaFemales[0].getAnimatorStateInfo(0);
		if (this.ctrlFlag.initiative == 0)
		{
			if (_modeCtrl == 0)
			{
				this.ManualAibu(this.FemaleAi, _infoAnimList);
			}
			else if (_modeCtrl == 1 || _modeCtrl == 2)
			{
				this.ManualHoushi(this.FemaleAi, _modeCtrl, _infoAnimList);
			}
			else if (_modeCtrl == 3 || _modeCtrl == 4)
			{
				this.ManualSonyu(this.FemaleAi, _modeCtrl, _infoAnimList);
			}
		}
		else if (_modeCtrl == 0)
		{
			this.AutoAibu(this.FemaleAi, _infoAnimList);
		}
		else if (_modeCtrl == 1 || _modeCtrl == 2)
		{
			this.AutoHoushi(this.FemaleAi, _modeCtrl, _infoAnimList);
		}
		else if (_modeCtrl == 3 || _modeCtrl == 4)
		{
			this.AutoSonyu(this.FemaleAi, _modeCtrl, _infoAnimList);
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
		this.SetFinishCategoryEnable(this.FemaleAi, _modeCtrl);
		this.ctrlMeta.Proc(this.FemaleAi, false);
		if (_modeCtrl == 0)
		{
			if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.RecoverFaintness)
			{
				if (this.FemaleAi.IsName("D_Idle") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop") || this.FemaleAi.IsName("D_OLoop") || this.FemaleAi.IsName("D_Orgasm_A"))
				{
					base.RecoverFaintnessTaii();
					if (this.ctrlFlag.nowAnimationInfo == this.ctrlFlag.selectAnimationListInfo)
					{
						this.setPlay("Orgasm_A", true);
					}
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
		}
		else if (_modeCtrl == 1 || _modeCtrl == 2)
		{
			if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.RecoverFaintness)
			{
				if (this.FemaleAi.IsName("D_Idle") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop") || this.FemaleAi.IsName("D_OLoop") || this.FemaleAi.IsName("D_Orgasm_OUT_A"))
				{
					base.RecoverFaintnessTaii();
					if (this.ctrlFlag.nowAnimationInfo == this.ctrlFlag.selectAnimationListInfo)
					{
						this.setPlay("Orgasm_OUT_A", true);
					}
					this.ctrlFlag.isFaintness = false;
					this.sprite.SetVisibleLeaveItToYou(true, true);
					this.ctrlFlag.numOrgasm = 0;
					this.ctrlMeta.Clear();
					this.sprite.SetAnimationMenu();
					this.ctrlFlag.isGaugeHit = false;
					this.ctrlFlag.isGaugeHit_M = false;
					this.sprite.SetMotionListDraw(false, -1);
					this.ctrlFlag.nowOrgasm = false;
					this.Hitem.SetUse(6, false);
				}
				else
				{
					this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.None;
					this.Hitem.SetUse(6, false);
				}
			}
		}
		else if ((_modeCtrl == 3 || _modeCtrl == 4) && this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.RecoverFaintness)
		{
			if (this.FemaleAi.IsName("D_Idle") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop") || this.FemaleAi.IsName("D_OLoop") || this.FemaleAi.IsName("D_Orgasm_IN_A") || this.FemaleAi.IsName("D_OrgasmM_OUT_A"))
			{
				base.RecoverFaintnessTaii();
				if (this.ctrlFlag.nowAnimationInfo == this.ctrlFlag.selectAnimationListInfo)
				{
					if (_modeCtrl == 3)
					{
						this.setPlay("Orgasm_IN_A", true);
					}
					else
					{
						this.setPlay("OrgasmM_OUT_A", true);
					}
				}
				this.ctrlMeta.Clear();
				this.ctrlFlag.isFaintness = false;
				this.ctrlFlag.numOrgasm = 0;
				this.sprite.SetFinishSelect(7, _modeCtrl, -1, -1);
				this.sprite.SetVisibleLeaveItToYou(true, true);
				this.ctrlMeta.SetParameterFromState(0);
				this.sprite.SetAnimationMenu();
				this.ctrlFlag.isGaugeHit = false;
				this.sprite.SetMotionListDraw(false, -1);
				this.ctrlFlag.nowOrgasm = false;
				this.Hitem.SetUse(6, false);
				if ((this.FemaleAi.IsName("D_Orgasm_IN_A") || this.FemaleAi.IsName("D_OrgasmM_OUT_A")) && this.voice.playAnimation != null)
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

	// Token: 0x0600523D RID: 21053 RVA: 0x0022A938 File Offset: 0x00228D38
	private bool ManualAibu(AnimatorStateInfo _ai, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.StartProcTrigger(num);
			this.StartAibuProc(false);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("WLoop"))
		{
			this.LoopAibuProc(0, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.LoopAibuProc(1, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.OLoopAibuProc(0, num, _infoAnimList);
		}
		else if (_ai.IsName("Orgasm"))
		{
			this.OrgasmAibuProc(0, _ai.normalizedTime);
		}
		else if (_ai.IsName("Orgasm_A"))
		{
			this.StartProcTrigger(num);
			this.StartAibuProc(true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.FaintnessStartProcTrigger(num);
			this.FaintnessStartAibuProc(true);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopAibuProc(0, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopAibuProc(1, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopAibuProc(1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm"))
		{
			this.OrgasmAibuProc(1, _ai.normalizedTime);
		}
		else if (_ai.IsName("D_Orgasm_A"))
		{
			this.FaintnessStartProcTrigger(num);
			this.FaintnessStartAibuProc(false);
		}
		return true;
	}

	// Token: 0x0600523E RID: 21054 RVA: 0x0022AB78 File Offset: 0x00228F78
	private bool ManualHoushi(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(0, false);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("WLoop"))
		{
			this.LoopHoushiProc(0, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.LoopHoushiProc(1, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.OLoopHoushiProc(0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("Orgasm_OUT"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Orgasm_OUT_A", true, true);
		}
		else if (_ai.IsName("Orgasm_IN"))
		{
			this.SetAfterInsideFinishAnimation(0, _ai.normalizedTime);
		}
		else if (_ai.IsName("Drink_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Drink", false, false);
		}
		else if (_ai.IsName("Drink"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Drink_A", true, true);
		}
		else if (_ai.IsName("Vomit_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Vomit", false, false);
		}
		else if (_ai.IsName("Vomit"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Vomit_A", true, true);
		}
		else if (_ai.IsName("Orgasm_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(0, true);
		}
		else if (_ai.IsName("Drink_A"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(0, true);
		}
		else if (_ai.IsName("Vomit_A"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(0, true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(1, false);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopHoushiProc(0, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopHoushiProc(1, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopHoushiProc(1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm_OUT"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Orgasm_OUT_A", true, true);
		}
		else if (_ai.IsName("D_Orgasm_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(1, true);
		}
		return true;
	}

	// Token: 0x0600523F RID: 21055 RVA: 0x0022AEE8 File Offset: 0x002292E8
	private bool ManualSonyu(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.StartProcTrigger(num);
			this.StartSonyuProc(false, 0);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 0);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.LoopSonyuProc(0, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.LoopSonyuProc(1, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.OLoopSonyuProc(0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("OrgasmF_IN"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 0, _modeCtrl, 0);
		}
		else if (_ai.IsName("OrgasmM_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 0, _modeCtrl, false);
		}
		else if (_ai.IsName("OrgasmS_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 0, _modeCtrl, true);
		}
		else if (_ai.IsName("Orgasm_IN_A"))
		{
			this.AfterTheInsideWaitingProc(0, num, _modeCtrl);
		}
		else if (_ai.IsName("Pull"))
		{
			this.PullProc(_ai.normalizedTime, 0);
		}
		else if (_ai.IsName("Drop"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 4f, 0, _modeCtrl, 2);
		}
		else if (_ai.IsName("OrgasmM_OUT"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 0, _modeCtrl, 2);
		}
		else if (_ai.IsName("OrgasmM_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartSonyuProc(true, 0);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartSonyuProc(false, 1);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("D_Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 1);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopSonyuProc(0, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopSonyuProc(1, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopSonyuProc(1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_OrgasmF_IN"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 1, _modeCtrl, 0);
		}
		else if (_ai.IsName("D_OrgasmM_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 1, _modeCtrl, false);
		}
		else if (_ai.IsName("D_OrgasmS_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 1, _modeCtrl, true);
		}
		else if (_ai.IsName("D_Orgasm_IN_A"))
		{
			this.AfterTheInsideWaitingProc(1, num, _modeCtrl);
		}
		else if (_ai.IsName("D_Pull"))
		{
			this.PullProc(_ai.normalizedTime, 1);
		}
		else if (_ai.IsName("D_Drop"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 4f, 1, _modeCtrl, 2);
		}
		else if (_ai.IsName("D_OrgasmM_OUT"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 1, _modeCtrl, 2);
		}
		else if (_ai.IsName("D_OrgasmM_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartSonyuProc(true, 1);
		}
		return true;
	}

	// Token: 0x06005240 RID: 21056 RVA: 0x0022B374 File Offset: 0x00229774
	private bool AutoAibu(AnimatorStateInfo _ai, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.AutoStartProcTrigger(false);
			this.AutoStartAibuProc(false);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.AutoLoopAibuProc(0, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.AutoLoopAibuProc(1, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.AutoOLoopAibuProc(0, num, _infoAnimList);
		}
		else if (_ai.IsName("Orgasm"))
		{
			this.OrgasmAibuProc(0, _ai.normalizedTime);
		}
		else if (_ai.IsName("Orgasm_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartAibuProc(true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.FaintnessStartProcTrigger(num);
			this.FaintnessStartAibuProc(true);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopAibuProc(0, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopAibuProc(1, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopAibuProc(1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm"))
		{
			this.OrgasmAibuProc(1, _ai.normalizedTime);
		}
		else if (_ai.IsName("D_Orgasm_A"))
		{
			this.FaintnessStartProcTrigger(num);
			this.FaintnessStartAibuProc(false);
		}
		return true;
	}

	// Token: 0x06005241 RID: 21057 RVA: 0x0022B584 File Offset: 0x00229984
	private bool AutoHoushi(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.AutoStartProcTrigger(false);
			this.AutoStartHoushiProc(0, false);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.AutoLoopHoushiProc(0, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.AutoLoopHoushiProc(1, 0, num, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.AutoOLoopHoushiProc(0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("Orgasm_OUT"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Orgasm_OUT_A", true, true);
		}
		else if (_ai.IsName("Orgasm_IN"))
		{
			this.SetAfterInsideFinishAnimation(0, _ai.normalizedTime);
		}
		else if (_ai.IsName("Drink_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Drink", false, false);
		}
		else if (_ai.IsName("Drink"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Drink_A", true, true);
		}
		else if (_ai.IsName("Vomit_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Vomit", false, false);
		}
		else if (_ai.IsName("Vomit"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "Vomit_A", true, true);
		}
		else if (_ai.IsName("Orgasm_OUT_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartHoushiProc(0, true);
		}
		else if (_ai.IsName("Drink_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartHoushiProc(0, true);
		}
		else if (_ai.IsName("Vomit_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartHoushiProc(0, true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(1, false);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopHoushiProc(0, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopHoushiProc(1, 1, num, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopHoushiProc(1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm_OUT"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Orgasm_OUT_A", true, true);
		}
		else if (_ai.IsName("D_Orgasm_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartHoushiProc(1, true);
		}
		return true;
	}

	// Token: 0x06005242 RID: 21058 RVA: 0x0022B8C8 File Offset: 0x00229CC8
	private bool AutoSonyu(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.AutoStartProcTrigger(false);
			this.AutoStartSonyuProc(false, 0, _modeCtrl);
		}
		else if (_ai.IsName("Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 0);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.AutoLoopSonyuProc(0, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.AutoLoopSonyuProc(1, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.AutoOLoopProc(0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("OrgasmF_IN"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 0, _modeCtrl, 0);
		}
		else if (_ai.IsName("OrgasmM_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 0, _modeCtrl, false);
		}
		else if (_ai.IsName("OrgasmS_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 0, _modeCtrl, true);
		}
		else if (_ai.IsName("Orgasm_IN_A"))
		{
			this.AutoAfterTheInsideWaitingProc(0, num);
		}
		else if (_ai.IsName("Pull"))
		{
			this.PullProc(_ai.normalizedTime, 0);
		}
		else if (_ai.IsName("Drop"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 4f, 0, _modeCtrl, 2);
		}
		else if (_ai.IsName("OrgasmM_OUT"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 0, _modeCtrl, 2);
		}
		else if (_ai.IsName("OrgasmM_OUT_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartSonyuProc(true, 0, _modeCtrl);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartSonyuProc(false, 1);
		}
		else if (_ai.IsName("D_Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 1);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopSonyuProc(0, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopSonyuProc(1, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopSonyuProc(1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_OrgasmF_IN"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 1, _modeCtrl, 0);
		}
		else if (_ai.IsName("D_OrgasmM_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 1, _modeCtrl, false);
		}
		else if (_ai.IsName("D_OrgasmS_IN"))
		{
			this.FinishNextAnimationByMorS(_ai.normalizedTime, 1f, 1, _modeCtrl, true);
		}
		else if (_ai.IsName("D_Orgasm_IN_A"))
		{
			this.AfterTheInsideWaitingProc(1, num, _modeCtrl);
		}
		else if (_ai.IsName("D_Pull"))
		{
			this.PullProc(_ai.normalizedTime, 1);
		}
		else if (_ai.IsName("D_Drop"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 4f, 1, _modeCtrl, 2);
		}
		else if (_ai.IsName("D_OrgasmM_OUT"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 1f, 1, _modeCtrl, 2);
		}
		else if (_ai.IsName("D_OrgasmM_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartSonyuProc(true, 1);
		}
		return true;
	}

	// Token: 0x06005243 RID: 21059 RVA: 0x0022BD28 File Offset: 0x0022A128
	public override void setAnimationParamater()
	{
		this.animPar.breast = this.chaFemales[0].GetShapeBodyValue(1);
		this.animPar.speed = ((this.ctrlFlag.loopType == 1) ? this.ctrlFlag.speed : (this.ctrlFlag.speed + 1f));
		this.animPar.m[0] = this.ctrlFlag.motions[0];
		this.animPar.m[1] = this.ctrlFlag.motions[1];
		for (int i = 0; i < this.chaFemales.Length; i++)
		{
			if (this.chaFemales[i].visibleAll && !(this.chaFemales[i].objBodyBone == null))
			{
				this.animPar.heights[i] = this.chaFemales[i].GetShapeBodyValue(0);
			}
		}
		for (int j = 0; j < this.chaFemales.Length; j++)
		{
			if (this.chaFemales[j].visibleAll && !(this.chaFemales[j].objTop == null))
			{
				this.chaFemales[j].setAnimatorParamFloat("height", this.animPar.heights[j]);
				this.chaFemales[j].setAnimatorParamFloat("speed", this.animPar.speed);
				this.chaFemales[j].setAnimatorParamFloat("motion", this.animPar.m[0]);
				this.chaFemales[j].setAnimatorParamFloat("motion1", this.animPar.m[1]);
				this.chaFemales[j].setAnimatorParamFloat("breast", this.animPar.breast);
				if (this.isHeight1Parameter)
				{
					this.chaFemales[j].setAnimatorParamFloat("height1", this.animPar.heights[j ^ 1]);
				}
			}
		}
		if (this.chaMales[0].objBodyBone != null)
		{
			this.chaMales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.chaMales[0].setAnimatorParamFloat("speed", this.animPar.speed);
			this.chaMales[0].setAnimatorParamFloat("motion", this.animPar.m[0]);
			this.chaMales[0].setAnimatorParamFloat("breast", this.animPar.breast);
			if (this.isHeight1Parameter)
			{
				this.chaMales[0].setAnimatorParamFloat("height1", this.animPar.heights[1]);
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

	// Token: 0x06005244 RID: 21060 RVA: 0x0022C07C File Offset: 0x0022A47C
	private void setPlay(string _playAnimation, bool _isFade = true)
	{
		this.chaFemales[0].animBody.Play(_playAnimation, 0, 0f);
		if (this.chaFemales[1].visibleAll && this.chaFemales[1].objTop != null)
		{
			this.chaFemales[1].animBody.Play(_playAnimation, 0, 0f);
		}
		if (this.chaMales[0].objTop != null)
		{
			this.chaMales[0].animBody.Play(_playAnimation, 0, 0f);
		}
		for (int i = 0; i < this.lstMotionIK.Count; i++)
		{
			this.lstMotionIK[i].Item3.Calc(_playAnimation);
		}
		if (this.item != null)
		{
			this.item.setPlay(_playAnimation, 0f);
		}
		if (_isFade)
		{
			this.fade.FadeStart(1f);
		}
	}

	// Token: 0x06005245 RID: 21061 RVA: 0x0022C17B File Offset: 0x0022A57B
	private bool StartProcTrigger(float _wheel)
	{
		if (_wheel == 0f || this.nextPlay != 0)
		{
			return false;
		}
		if (this.ctrlFlag.voice.playStart > 4)
		{
			return false;
		}
		this.nextPlay = 1;
		return true;
	}

	// Token: 0x06005246 RID: 21062 RVA: 0x0022C1B8 File Offset: 0x0022A5B8
	private bool StartAibuProc(bool _isReStart)
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
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC("WLoop", 1, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[1], 1, false);
			}
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

	// Token: 0x06005247 RID: 21063 RVA: 0x0022C488 File Offset: 0x0022A888
	private bool FaintnessStartProcTrigger(float _wheel)
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

	// Token: 0x06005248 RID: 21064 RVA: 0x0022C514 File Offset: 0x0022A914
	private bool FaintnessStartAibuProc(bool _start)
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
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC("D_WLoop", 1, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC("D_WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC("D_WLoop", 2, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC("D_WLoop", 2, this.chaFemales[1], 1, false);
			}
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

	// Token: 0x06005249 RID: 21065 RVA: 0x0022C7A0 File Offset: 0x0022ABA0
	private bool LoopAibuProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
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

	// Token: 0x0600524A RID: 21066 RVA: 0x0022CE9C File Offset: 0x0022B29C
	private bool OLoopAibuProc(int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		this.ctrlFlag.speed = Mathf.Clamp01(this.ctrlFlag.speed + _wheel);
		this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, 2, this.ctrlFlag.speed);
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
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
			this.ctrlFlag.voice.dialog = false;
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
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			else
			{
				if (sio)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
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
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.nowOrgasm = true;
		}
		return true;
	}

	// Token: 0x0600524B RID: 21067 RVA: 0x0022D714 File Offset: 0x0022BB14
	private bool GotoFaintnessAibu(int _state)
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

	// Token: 0x0600524C RID: 21068 RVA: 0x0022D8AA File Offset: 0x0022BCAA
	private bool OrgasmAibuProc(int _state, float _normalizedTime)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		this.GotoFaintnessAibu(_state);
		return true;
	}

	// Token: 0x0600524D RID: 21069 RVA: 0x0022D8C4 File Offset: 0x0022BCC4
	private bool StartHoushiProc(int _state, bool _restart)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_restart)
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
		this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
		if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.isNotCtrl = false;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.oldHit = false;
		for (int j = 0; j < 2; j++)
		{
			this.ctrlFlag.motions[j] = 0f;
			this.timeMotions[j] = 0f;
			this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[j] = 0f;
		}
		this.feelHit.InitTime();
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x0600524E RID: 21070 RVA: 0x0022DBDC File Offset: 0x0022BFDC
	private bool LoopHoushiProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.ctrlFlag.feel_m = 0.7f;
			this.oldHit = false;
			if (this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 4)
			{
				if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice))
				{
					Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
				}
				this.ctrlFlag.voice.dialog = false;
			}
			this.feelHit.InitTime();
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
			for (int i = 0; i < 2; i++)
			{
				if (this.chaFemales[i].visibleAll && !(this.chaFemales[i].objBodyBone == null))
				{
					this.timeChangeMotionDeltaTimes[i] += Time.deltaTime;
					if (this.timeChangeMotions[i] <= this.timeChangeMotionDeltaTimes[i] && !this.enableMotions[i])
					{
						this.timeChangeMotions[i] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
						this.timeChangeMotionDeltaTimes[0] = 0f;
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
			if (_loop == 0)
			{
				if (this.ctrlFlag.speed > 1f && this.ctrlFlag.loopType == 0)
				{
					this.setPlay((_state != 0) ? "D_SLoop" : "SLoop", true);
					this.ctrlFlag.nowSpeedStateFast = false;
					if (this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 4)
					{
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
						this.ctrlFlag.voice.dialog = false;
					}
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
					if (this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 4)
					{
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
						this.ctrlFlag.voice.dialog = false;
					}
					this.feelHit.InitTime();
					this.ctrlFlag.loopType = 0;
				}
				this.ctrlFlag.speed = Mathf.Clamp(this.ctrlFlag.speed, 0f, 2f);
			}
			this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, _loop, (_loop != 0) ? (this.ctrlFlag.speed - 1f) : this.ctrlFlag.speed);
			if (this.ctrlFlag.isGaugeHit)
			{
				this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
			}
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num2 *= ((!this.ctrlFlag.isGaugeHit) ? 1f : 2f) * (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(18))
			{
				num2 *= this.ctrlFlag.SkilChangeSpeed(18);
			}
			this.ctrlFlag.feel_m += num2;
			this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
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
				this.ctrlFlag.voice.dialog = false;
			}
			this.oldHit = this.ctrlFlag.isGaugeHit;
			if (this.ctrlFlag.feel_m >= 0.7f)
			{
				this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = -1;
				this.ctrlFlag.nowSpeedStateFast = false;
				if (this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 4)
				{
					for (int l = 0; l < 2; l++)
					{
						if (!this.chaFemales[l].visibleAll && !(this.chaFemales[l].objBodyBone == null))
						{
							if (this.voice.nowVoices[l].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[l].state == HVoiceCtrl.VoiceKind.startVoice)
							{
								Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[l]);
							}
						}
					}
					this.ctrlFlag.voice.dialog = false;
				}
				this.oldHit = false;
				this.feelHit.InitTime();
			}
		}
		return true;
	}

	// Token: 0x0600524F RID: 21071 RVA: 0x0022E4E4 File Offset: 0x0022C8E4
	private bool OLoopHoushiProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.ctrlFlag.speed = Mathf.Clamp01(this.ctrlFlag.speed + _wheel);
		this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, 2, this.ctrlFlag.speed);
		this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
		float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
		num *= ((!this.ctrlFlag.isGaugeHit) ? 1f : 2f) * (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
		if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(18))
		{
			num *= this.ctrlFlag.SkilChangeSpeed(18);
		}
		this.ctrlFlag.feel_m += num;
		this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
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
			this.ctrlFlag.voice.dialog = false;
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide)
		{
			this.setPlay((_state != 0) ? "D_Orgasm_OUT" : "Orgasm_OUT", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (item == 5 && id == 113)
			{
				this.ctrlFlag.AddParam(80, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 2;
			this.ctrlFlag.voice.dialog = false;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishDrink && _modeCtrl != 1)
		{
			this.setPlay("Orgasm_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 5 && id2 == 113)
			{
				this.ctrlFlag.AddParam(80, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 0;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishVomit && _modeCtrl != 1)
		{
			this.setPlay("Orgasm_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item3 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id3 = this.ctrlFlag.nowAnimationInfo.id;
			if (item3 == 5 && id3 == 113)
			{
				this.ctrlFlag.AddParam(80, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 1;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		return true;
	}

	// Token: 0x06005250 RID: 21072 RVA: 0x0022EB2C File Offset: 0x0022CF2C
	private bool SetNextFinishAnimation(float _normalizedTime, string _nextAnimation, bool _isSpriteSet = true, bool _isFade = true)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		this.setPlay(_nextAnimation, _isFade);
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = -1;
		if (_isSpriteSet)
		{
			this.ctrlFlag.nowOrgasm = false;
		}
		return true;
	}

	// Token: 0x06005251 RID: 21073 RVA: 0x0022EB80 File Offset: 0x0022CF80
	private bool SetAfterInsideFinishAnimation(int _state, float _normalizedTime)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		if (this.finishMotion == 0)
		{
			this.ctrlFlag.numDrink = Mathf.Clamp(this.ctrlFlag.numDrink + 1, 0, 999999);
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.setPlay("Drink_IN", true);
		}
		else if (this.finishMotion == 1)
		{
			this.ctrlFlag.numVomit = Mathf.Clamp(this.ctrlFlag.numVomit + 1, 0, 999999);
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.setPlay("Vomit_IN", true);
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = -1;
		return true;
	}

	// Token: 0x06005252 RID: 21074 RVA: 0x0022ED10 File Offset: 0x0022D110
	private bool StartSonyuProc(bool _restart, int _state)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_restart)
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
		this.setPlay((_state != 0) ? "D_Insert" : "Insert", true);
		this.ctrlFlag.loopType = -1;
		this.nextPlay = 0;
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.motions[1] = 0f;
		this.ctrlFlag.isNotCtrl = false;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.timeMotions[0] = 0f;
		this.timeMotions[1] = 0f;
		this.oldHit = false;
		this.feelHit.InitTime();
		if (_state == 0)
		{
			for (int j = 0; j < 2; j++)
			{
				this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
				this.timeChangeMotionDeltaTimes[j] = 0f;
			}
		}
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005253 RID: 21075 RVA: 0x0022EEE4 File Offset: 0x0022D2E4
	private bool InsertProc(float _normalizedTime, int _state)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
		if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		return true;
	}

	// Token: 0x06005254 RID: 21076 RVA: 0x0022F0A4 File Offset: 0x0022D4A4
	private bool LoopSonyuProc(int _loop, int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f && _modeCtrl == 3)
		{
			string[] array = new string[]
			{
				"OrgasmM_IN",
				"D_OrgasmM_IN"
			};
			this.setPlay(array[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (item == 5 && id == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numInside = Mathf.Clamp(this.ctrlFlag.numInside + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 1;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.dialog = false;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && (_modeCtrl != 4 || (_modeCtrl == 4 && _state == 0)))
		{
			string[] array2 = new string[]
			{
				"OrgasmM_OUT",
				"D_OrgasmM_OUT"
			};
			this.setPlay(array2[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 5 && id2 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 2;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.dialog = false;
			if (this.ctrlFlag.numOutSide < 4)
			{
				ProcBase.hSceneManager.Bath += 10f;
			}
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			string[] array3 = new string[]
			{
				"OLoop",
				"D_OLoop"
			};
			this.setPlay(array3[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.feel_f = 0.7f;
			if (this.ctrlFlag.feel_m <= 0.7f)
			{
				this.ctrlFlag.feel_m = 0.7f;
			}
			this.ctrlFlag.nowSpeedStateFast = false;
			this.oldHit = false;
			this.feelHit.InitTime();
			for (int i = 0; i < 2; i++)
			{
				if (this.chaFemales[i].visibleAll && this.chaFemales[i].objTop != null)
				{
					if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 3)
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[i]);
					}
					else if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 3)
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[i]);
					}
				}
			}
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
			for (int j = 0; j < 2; j++)
			{
				if (this.chaFemales[j].visibleAll && !(this.chaFemales[j].objBodyBone == null))
				{
					this.timeChangeMotionDeltaTimes[j] += Time.deltaTime;
					if (this.timeChangeMotions[j] <= this.timeChangeMotionDeltaTimes[j] && !this.enableMotions[j] && _state == 0)
					{
						this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
						this.timeChangeMotionDeltaTimes[j] = 0f;
						this.enableMotions[j] = true;
						this.timeMotions[j] = 0f;
						float num;
						if (this.allowMotions[j])
						{
							num = 1f - this.ctrlFlag.motions[j];
							if (num <= this.ctrlFlag.changeMotionMinRate)
							{
								num = 1f;
							}
							else
							{
								num = this.ctrlFlag.motions[j] + UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
							}
							if (num >= 1f)
							{
								this.allowMotions[j] = false;
							}
						}
						else
						{
							num = this.ctrlFlag.motions[j];
							if (num <= this.ctrlFlag.changeMotionMinRate)
							{
								num = 0f;
							}
							else
							{
								num = this.ctrlFlag.motions[j] - UnityEngine.Random.Range(this.ctrlFlag.changeMotionMinRate, num);
							}
							if (num <= 0f)
							{
								this.allowMotions[j] = true;
							}
						}
						this.lerpMotions[j] = new Vector2(this.ctrlFlag.motions[j], num);
						this.lerpTimes[j] = UnityEngine.Random.Range(this.ctrlFlag.changeMotionTimeMin, this.ctrlFlag.changeMotionTimeMax);
					}
				}
			}
			if (_loop == 0)
			{
				if (this.ctrlFlag.speed > 1f && this.ctrlFlag.loopType == 0)
				{
					this.setPlay((_state != 0) ? "D_SLoop" : "SLoop", true);
					this.ctrlFlag.nowSpeedStateFast = false;
					for (int k = 0; k < 2; k++)
					{
						if (this.chaFemales[k].visibleAll && !(this.chaFemales[k].objTop == null))
						{
							if (this.voice.nowVoices[k].state == HVoiceCtrl.VoiceKind.voice)
							{
								Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[k]);
							}
							else if (this.voice.nowVoices[k].state == HVoiceCtrl.VoiceKind.startVoice)
							{
								Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[k]);
							}
						}
					}
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
					for (int l = 0; l < 2; l++)
					{
						if (this.chaFemales[l].visibleAll && !(this.chaFemales[l].objTop == null))
						{
							if ((this.voice.nowVoices[l].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[l].state == HVoiceCtrl.VoiceKind.startVoice) && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 3 && this.ctrlFlag.nowAnimationInfo.id == 0)
							{
								Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[l]);
							}
						}
					}
					this.feelHit.InitTime();
					this.ctrlFlag.loopType = 0;
				}
				this.ctrlFlag.speed = Mathf.Clamp(this.ctrlFlag.speed, 0f, 2f);
			}
			if (_state != 1 || _modeCtrl != 4)
			{
				float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num2 *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1))
				{
					num2 *= this.ctrlFlag.SkilChangeSpeed(1);
				}
				this.ctrlFlag.feel_m += num2;
				this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
			}
			this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, _loop, (_loop != 0) ? (this.ctrlFlag.speed - 1f) : this.ctrlFlag.speed);
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			if (this.ctrlFlag.isGaugeHit)
			{
				this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
				float num3 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num3 *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant)
				{
					if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
					{
						num3 *= this.ctrlFlag.SkilChangeSpeed(15);
					}
					if (ProcBase.hSceneManager.HSkil.ContainsValue(3))
					{
						num3 *= this.ctrlFlag.SkilChangeSpeed(3);
					}
				}
				this.ctrlFlag.feel_f += num3;
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
				if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && _modeCtrl != 4)
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
				for (int m = 0; m < 2; m++)
				{
					if (this.chaFemales[m].visibleAll && !(this.chaFemales[m].objTop == null))
					{
						if ((this.voice.nowVoices[m].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[m].state == HVoiceCtrl.VoiceKind.startVoice) && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 3)
						{
							Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[m]);
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005255 RID: 21077 RVA: 0x0022FEC8 File Offset: 0x0022E2C8
	private bool OLoopSonyuProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			if (this.ctrlFlag.feel_m <= 0.7f)
			{
				this.ctrlFlag.feel_m = 0.7f;
				this.ctrlFlag.isGaugeHit = false;
			}
			return true;
		}
		if (_state != 1 || _modeCtrl != 4)
		{
			float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1))
			{
				num *= this.ctrlFlag.SkilChangeSpeed(1);
			}
			this.ctrlFlag.feel_m += num;
			this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
		}
		this.ctrlFlag.speed = Mathf.Clamp01(this.ctrlFlag.speed + _wheel);
		this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, 2, this.ctrlFlag.speed);
		if (this.ctrlFlag.isGaugeHit)
		{
			float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num2 *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant)
			{
				if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
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
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && _modeCtrl != 4)
			{
				if ((_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 3) && _infoAnimList.nAnimListInfoID != 3)
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
			this.setPlay((_state != 0) ? "D_OrgasmF_IN" : "OrgasmF_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (item == 5 && id == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 0;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.dialog = false;
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
					if (this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
					if (this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			else
			{
				if (sio)
				{
					this.particle.Play(0);
					if (this.chaFemales[1] && this.chaFemales[1].objBodyBone)
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
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
				}
			}
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f && _modeCtrl == 3)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_IN" : "OrgasmM_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 5 && id2 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numInside = Mathf.Clamp(this.ctrlFlag.numInside + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 1;
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.ctrlFlag.voice.dialog = false;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && (_modeCtrl != 4 || (_modeCtrl == 4 && _state == 0)))
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_OUT" : "OrgasmM_OUT", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item3 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id3 = this.ctrlFlag.nowAnimationInfo.id;
			if (item3 == 5 && id3 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 2;
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.ctrlFlag.voice.dialog = false;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishSame && this.ctrlFlag.feel_m >= 0.7f && (_modeCtrl != 4 || (_modeCtrl == 4 && _state == 0)))
		{
			this.setPlay((_state != 0) ? "D_OrgasmS_IN" : "OrgasmS_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item4 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id4 = this.ctrlFlag.nowAnimationInfo.id;
			if (item4 == 5 && id4 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.ctrlFlag.numSameOrgasm = Mathf.Clamp(this.ctrlFlag.numSameOrgasm + 1, 0, 999999);
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.ctrlFlag.voice.dialog = false;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			if (_modeCtrl == 3)
			{
				this.ctrlFlag.numInside = Mathf.Clamp(this.ctrlFlag.numInside + 1, 0, 999999);
			}
			else
			{
				this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			}
			this.ctrlFlag.voice.oldFinish = 3;
			bool urine2 = Config.HData.Urine;
			bool sio2 = Config.HData.Sio;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				bool flag2 = this.Hitem.Effect(5);
				if (sio2)
				{
					this.particle.Play(0);
					if (this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag2) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
					if (this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				if (urine2 || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey3 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey3, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey3, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			else
			{
				if (sio2)
				{
					this.particle.Play(0);
					if (this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(4);
					}
				}
				if (urine2)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey4 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey4, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey4, ProcBase.hSceneManager.Toilet);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005256 RID: 21078 RVA: 0x002311D0 File Offset: 0x0022F5D0
	private bool AfterTheNextWaitingAnimation(float _normalizedTime, float _loopCount, int _state, int _modeCtrl, int _nextAfter)
	{
		if (_normalizedTime < _loopCount)
		{
			return false;
		}
		if (_nextAfter == 0)
		{
			this.GotoFaintnessSonyu(_state, _modeCtrl, (_modeCtrl != 3) ? 1 : 0);
		}
		else if (_nextAfter == 1)
		{
			this.setPlay((_state != 0) ? "D_Orgasm_IN_A" : "Orgasm_IN_A", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		else if (_nextAfter == 2)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_OUT_A" : "OrgasmM_OUT_A", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.isInsert = false;
		}
		this.ctrlFlag.nowOrgasm = false;
		return true;
	}

	// Token: 0x06005257 RID: 21079 RVA: 0x002312AC File Offset: 0x0022F6AC
	private bool GotoFaintnessSonyu(int _state, int _modeCtrl, int _nextAfter)
	{
		bool flag = this.Hitem.Effect(5);
		if (_state == 0 && (this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount || flag))
		{
			this.setPlay((_nextAfter != 0) ? "D_OrgasmM_OUT_A" : "D_Orgasm_IN_A", true);
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
			this.ctrlMeta.SetParameterFromState(1);
			this.sprite.SetToggleLeaveItToYou(false);
			if (this.ctrlFlag.initiative != 0)
			{
				this.ctrlFlag.initiative = 0;
				this.sprite.MainCategoryOfLeaveItToYou(false);
			}
			this.sprite.SetAnimationMenu();
			this.sprite.SetFinishSelect(7, _modeCtrl, -1, -1);
		}
		else
		{
			this.setPlay((_state != 0) ? ((_nextAfter != 0) ? "D_OrgasmM_OUT_A" : "D_Orgasm_IN_A") : ((_nextAfter != 0) ? "OrgasmM_OUT_A" : "Orgasm_IN_A"), true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		return true;
	}

	// Token: 0x06005258 RID: 21080 RVA: 0x0023148E File Offset: 0x0022F88E
	private bool FinishNextAnimationByMorS(float _normalizedTime, float _loopCount, int _state, int _modeCtrl, bool _finishMorS)
	{
		this.AfterTheNextWaitingAnimation(_normalizedTime, _loopCount, _state, _modeCtrl, (!_finishMorS) ? 1 : 0);
		return true;
	}

	// Token: 0x06005259 RID: 21081 RVA: 0x002314AC File Offset: 0x0022F8AC
	private bool AfterTheInsideWaitingProc(int _state, float _wheel, int _modeCtrl)
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[i].state == HVoiceCtrl.VoiceKind.startVoice)
			{
				return false;
			}
		}
		if (this.ctrlFlag.voice.dialog || this.ctrlFlag.voice.playStart > 4)
		{
			return false;
		}
		int num = this.nextPlay;
		if (num != 0)
		{
			if (num == 1)
			{
				this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
				if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
				{
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
					}
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
					}
				}
				else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
				{
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
					}
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
					}
				}
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = 0;
				this.ctrlFlag.nowSpeedStateFast = false;
				for (int j = 0; j < 2; j++)
				{
					this.ctrlFlag.motions[j] = 0f;
					this.timeMotions[j] = 0f;
				}
				if (_state == 0)
				{
					for (int k = 0; k < 2; k++)
					{
						this.timeChangeMotions[k] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
						this.timeChangeMotionDeltaTimes[k] = 0f;
					}
				}
				this.voice.AfterFinish();
				this.oldHit = false;
				this.feelHit.InitTime();
				this.nextPlay = 0;
				this.ctrlMeta.Clear();
			}
		}
		else if (_wheel < 0f)
		{
			this.setPlay((_state != 0) ? "D_Pull" : "Pull", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			for (int l = 0; l < 2; l++)
			{
				this.ctrlFlag.motions[l] = 0f;
				this.timeMotions[l] = 0f;
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.nowOrgasm = true;
			this.voice.AfterFinish();
			this.oldHit = false;
			this.feelHit.InitTime();
		}
		else if (_wheel > 0f)
		{
			this.ctrlFlag.voice.playStart = 3;
			this.nextPlay = 1;
		}
		return true;
	}

	// Token: 0x0600525A RID: 21082 RVA: 0x002318A4 File Offset: 0x0022FCA4
	private bool PullProc(float _normalizedTime, int _state)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		if (this.ctrlFlag.isInsert)
		{
			this.setPlay((_state != 0) ? "D_Drop" : "Drop", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		else
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_OUT_A" : "OrgasmM_OUT_A", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowOrgasm = false;
		}
		return true;
	}

	// Token: 0x0600525B RID: 21083 RVA: 0x00231950 File Offset: 0x0022FD50
	private void SetFinishCategoryEnable(AnimatorStateInfo _ai, int _modeCtrl)
	{
		bool flag = _ai.IsName("WLoop") || _ai.IsName("SLoop") || _ai.IsName("D_WLoop") || _ai.IsName("D_SLoop") || _ai.IsName("OLoop") || _ai.IsName("D_OLoop");
		bool flag2 = this.ctrlFlag.feel_m >= 0.7f && flag;
		bool flag3;
		if (_modeCtrl != 3 && _modeCtrl != 4)
		{
			flag3 = (this.ctrlFlag.feel_f < 0.7f || this.ctrlFlag.feel_m < 0.7f);
		}
		else
		{
			flag3 = ((this.ctrlFlag.feel_f < 0.7f || this.ctrlFlag.feel_m < 0.7f) && flag);
		}
		flag3 &= Config.HData.FinishButton;
		this.sprite.categoryFinish.SetActive(flag3, 3);
		if (this.sprite.IsFinishVisible(0))
		{
			this.sprite.categoryFinish.SetActive(flag2 && _modeCtrl != 0, 0);
		}
		if (this.sprite.IsFinishVisible(1))
		{
			this.sprite.categoryFinish.SetActive(flag2 && this.ctrlFlag.feel_f >= 0.7f && _modeCtrl == 3, 1);
		}
		if (this.sprite.IsFinishVisible(2))
		{
			this.sprite.categoryFinish.SetActive(flag2 && _modeCtrl == 3, 2);
		}
		if (this.sprite.IsFinishVisible(4))
		{
			this.sprite.categoryFinish.SetActive(flag2 && _modeCtrl == 2, 4);
		}
		if (this.sprite.IsFinishVisible(5))
		{
			this.sprite.categoryFinish.SetActive(flag2 && _modeCtrl == 2, 5);
		}
	}

	// Token: 0x0600525C RID: 21084 RVA: 0x00231B7C File Offset: 0x0022FF7C
	private HScene.AnimationListInfo RecoverFaintnessAi()
	{
		List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> lstStartAnimInfo = Singleton<Manager.Resources>.Instance.HSceneTable.lstStartAnimInfo;
		foreach (UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion> valueTuple in lstStartAnimInfo)
		{
			if (valueTuple.Item1 == ProcBase.hSceneManager.EventKind)
			{
				if (valueTuple.Item2 == ProcBase.hSceneManager.height)
				{
					if (valueTuple.Item3.mode == 1)
					{
						return this.lstAnimation[valueTuple.Item3.id];
					}
				}
			}
		}
		this.sbWarning.Clear();
		if (this.lstAnimation.Count == 0)
		{
			this.sbWarning.Append("RecoverFaintnessAi：失敗\n");
			return null;
		}
		this.sbWarning.Append("RecoverFaintnessAi：失敗\n").Append("回復後の体位を").Append(this.lstAnimation[0].nameAnimation).Append("に設定");
		return this.lstAnimation[0];
	}

	// Token: 0x0600525D RID: 21085 RVA: 0x00231CC0 File Offset: 0x002300C0
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

	// Token: 0x0600525E RID: 21086 RVA: 0x00231D58 File Offset: 0x00230158
	private bool AutoStartAibuProc(bool _isReStart)
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
		}
		this.nextPlay = 0;
		if (!_isReStart || (_isReStart && !this.auto.IsChangeActionAtRestart()))
		{
			this.setPlay("WLoop", true);
			if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
			{
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
				{
					this.voice.PlaySoundETC("WLoop", 1, this.chaFemales[0], 0, false);
				}
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
				{
					this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[1], 1, false);
				}
			}
			else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
			{
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
				{
					this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[0], 0, false);
				}
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
				{
					this.voice.PlaySoundETC("WLoop", 2, this.chaFemales[1], 1, false);
				}
			}
		}
		else
		{
			this.ctrlFlag.isAutoActionChange = true;
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

	// Token: 0x0600525F RID: 21087 RVA: 0x00232048 File Offset: 0x00230448
	private bool AutoLoopAibuProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
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
					if (this.voice.nowVoices[j].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[j].state == HVoiceCtrl.VoiceKind.startVoice)
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[j]);
					}
				}
				this.feelHit.InitTime();
			}
			else if (this.auto.AddSpeed(_wheel, _loop))
			{
				this.setPlay((_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop"), true);
				for (int k = 0; k < 2; k++)
				{
					if (this.voice.nowVoices[k].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[k].state == HVoiceCtrl.VoiceKind.startVoice)
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[k]);
					}
				}
				this.feelHit.InitTime();
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

	// Token: 0x06005260 RID: 21088 RVA: 0x00232924 File Offset: 0x00230D24
	private bool AutoOLoopAibuProc(int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
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
					if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
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
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(3))
			{
				num *= this.ctrlFlag.SkilChangeSpeed(3);
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
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			else
			{
				if (sio)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
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
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.nowOrgasm = true;
		}
		return true;
	}

	// Token: 0x06005261 RID: 21089 RVA: 0x002331FC File Offset: 0x002315FC
	private bool AutoStartHoushiProc(int _state, bool _restart)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_restart)
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
		}
		if (!_restart || (_restart && !this.auto.IsChangeActionAtRestart()))
		{
			this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
			if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
			{
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
				{
					this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
				}
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
				{
					this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
				}
			}
			else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
			{
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
				{
					this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
				}
				if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
				{
					this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
				}
			}
		}
		else
		{
			this.ctrlFlag.isAutoActionChange = true;
		}
		this.nextPlay = 0;
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.isNotCtrl = false;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.oldHit = false;
		for (int j = 0; j < 2; j++)
		{
			this.ctrlFlag.motions[j] = 0f;
			this.timeMotions[j] = 0f;
			this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[j] = 0f;
		}
		this.feelHit.InitTime();
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		this.auto.Reset();
		return true;
	}

	// Token: 0x06005262 RID: 21090 RVA: 0x00233534 File Offset: 0x00231934
	private bool AutoLoopHoushiProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			this.setPlay((_state != 0) ? "D_OLoop" : "OLoop", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.ctrlFlag.feel_m = 0.7f;
			this.oldHit = false;
			this.feelHit.InitTime();
			this.ctrlFlag.isGaugeHit = false;
		}
		else
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
			string playAnimation = (_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop");
			if (this.auto.ChangeLoopMotion(_loop == 1))
			{
				this.setPlay(playAnimation, true);
				this.feelHit.InitTime();
			}
			else
			{
				this.auto.ChangeSpeed(_loop == 1, new Vector2(-1f, -1f));
				if (this.auto.AddSpeed(_wheel, _loop))
				{
					this.setPlay(playAnimation, true);
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
			this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
			this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, _loop, this.ctrlFlag.speed);
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num2 *= ((!this.ctrlFlag.isGaugeHit) ? 1f : 2f) * (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(18))
			{
				num2 *= this.ctrlFlag.SkilChangeSpeed(18);
			}
			this.ctrlFlag.feel_m += num2;
			this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
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
				this.ctrlFlag.voice.dialog = false;
			}
			this.oldHit = this.ctrlFlag.isGaugeHit;
			if (this.ctrlFlag.feel_m >= 0.7f)
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

	// Token: 0x06005263 RID: 21091 RVA: 0x00233B20 File Offset: 0x00231F20
	private bool AutoOLoopHoushiProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.auto.ChangeSpeed(false, new Vector2(-1f, -1f));
		this.auto.AddSpeed(_wheel, 2);
		this.ctrlFlag.speed = this.auto.GetSpeed(false);
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, 2, this.ctrlFlag.speed);
		this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
		float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
		num *= ((!this.ctrlFlag.isGaugeHit) ? 1f : 2f) * (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
		if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(18))
		{
			num *= this.ctrlFlag.SkilChangeSpeed(18);
		}
		this.ctrlFlag.feel_m += num;
		this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
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
			this.ctrlFlag.voice.dialog = false;
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide || (this.ctrlFlag.initiative == 2 && this.ctrlFlag.feel_m >= 1f))
		{
			this.setPlay((_state != 0) ? "D_Orgasm_OUT" : "Orgasm_OUT", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (item == 5 && id == 113)
			{
				this.ctrlFlag.AddParam(80, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			this.ctrlFlag.voice.oldFinish = 2;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishDrink && _modeCtrl != 1)
		{
			this.setPlay("Orgasm_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 5 && id2 == 113)
			{
				this.ctrlFlag.AddParam(80, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 0;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishVomit && _modeCtrl != 1)
		{
			this.setPlay("Orgasm_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item3 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id3 = this.ctrlFlag.nowAnimationInfo.id;
			if (item3 == 5 && id3 == 113)
			{
				this.ctrlFlag.AddParam(80, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 1;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		return true;
	}

	// Token: 0x06005264 RID: 21092 RVA: 0x002341A0 File Offset: 0x002325A0
	private bool AutoStartSonyuProc(bool _restart, int _state, int _modeCtrl)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_restart)
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
		}
		this.nextPlay = 0;
		if (!_restart || (_restart && !this.auto.IsChangeActionAtRestart()))
		{
			if (_modeCtrl == 3)
			{
				this.setPlay((_state != 0) ? "D_Insert" : "Insert", true);
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = -1;
			}
			else
			{
				this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
				if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
				{
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
					}
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
					}
				}
				else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
				{
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
					}
					if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
					{
						this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
					}
				}
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = 0;
			}
		}
		else
		{
			this.ctrlFlag.isAutoActionChange = true;
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.motions[1] = 0f;
		this.timeMotions[0] = 0f;
		this.timeMotions[1] = 0f;
		this.oldHit = false;
		if (_state == 0)
		{
			for (int j = 0; j < 2; j++)
			{
				this.timeChangeMotions[j] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
				this.timeChangeMotionDeltaTimes[j] = 0f;
			}
		}
		this.ctrlFlag.isNotCtrl = false;
		this.auto.Reset();
		this.feelHit.InitTime();
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005265 RID: 21093 RVA: 0x00234554 File Offset: 0x00232954
	private bool AutoLoopSonyuProc(int _loop, int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (((this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f) || (this.ctrlFlag.initiative == 2 && this.ctrlFlag.feel_m >= 1f)) && _modeCtrl == 3)
		{
			string[] array = new string[]
			{
				"OrgasmM_IN",
				"D_OrgasmM_IN"
			};
			this.setPlay(array[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (item == 5 && id == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numInside = Mathf.Clamp(this.ctrlFlag.numInside + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 1;
			this.ctrlFlag.nowOrgasm = true;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && (_modeCtrl != 4 || (_modeCtrl == 4 && _state == 0)))
		{
			string[] array2 = new string[]
			{
				"OrgasmM_OUT",
				"D_OrgasmM_OUT"
			};
			this.setPlay(array2[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 5 && id2 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 2;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.nowOrgasm = true;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			string[] array3 = new string[]
			{
				"OLoop",
				"D_OLoop"
			};
			this.setPlay(array3[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.ctrlFlag.feel_f = 0.7f;
			if (this.ctrlFlag.feel_m <= 0.7f)
			{
				this.ctrlFlag.feel_m = 0.7f;
			}
			this.oldHit = false;
			this.feelHit.InitTime();
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				if (this.chaFemales[i].visibleAll && !(this.chaFemales[i].objBodyBone == null))
				{
					this.timeChangeMotionDeltaTimes[i] += Time.deltaTime;
					if (this.timeChangeMotions[i] <= this.timeChangeMotionDeltaTimes[i] && !this.enableMotions[i] && _state == 0)
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
			this.feelHit.ChangeHit(_infoAnimList.nFeelHit, _loop);
			Vector2 hitArea = this.feelHit.GetHitArea(_infoAnimList.nFeelHit, _loop);
			if (this.auto.ChangeLoopMotion(_loop == 1))
			{
				this.setPlay((_loop != 0) ? ((_state != 0) ? "D_WLoop" : "WLoop") : ((_state != 0) ? "D_SLoop" : "SLoop"), true);
				for (int j = 0; j < 2; j++)
				{
					if (this.chaFemales[j].visibleAll && !(this.chaFemales[j].objTop == null))
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
						if (this.chaFemales[k].visibleAll && !(this.chaFemales[k].objTop == null))
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
			if (_state == 1)
			{
				if (this.ctrlFlag.isGaugeHit)
				{
					float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
					this.ctrlFlag.feel_f *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
					if (!ProcBase.hSceneManager.bMerchant)
					{
						if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
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
					if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
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
			if (_state != 1 || _modeCtrl != 4)
			{
				float num3 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num3 *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1))
				{
					num3 *= this.ctrlFlag.SkilChangeSpeed(1);
				}
				this.ctrlFlag.feel_m += num3;
				this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
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
				if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && _modeCtrl != 4)
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
				this.feelHit.InitTime();
			}
		}
		return true;
	}

	// Token: 0x06005266 RID: 21094 RVA: 0x0023521C File Offset: 0x0023361C
	private bool AutoOLoopProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			if (this.ctrlFlag.feel_m <= 0.7f)
			{
				this.ctrlFlag.feel_m = 0.7f;
				this.ctrlFlag.isGaugeHit = false;
			}
			return true;
		}
		this.feelHit.ChangeHit(_infoAnimList.nFeelHit, 2);
		Vector2 hitArea = this.feelHit.GetHitArea(_infoAnimList.nFeelHit, 2);
		this.auto.ChangeSpeed(false, hitArea);
		this.auto.AddSpeed(_wheel, 2);
		this.ctrlFlag.speed = this.auto.GetSpeed(false);
		this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
		this.ctrlFlag.isGaugeHit = GlobalMethod.RangeOn<float>(this.ctrlFlag.speed, hitArea.x, hitArea.y);
		if (_state == 1)
		{
			if (this.ctrlFlag.isGaugeHit)
			{
				float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant)
				{
					if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
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
				if (ProcBase.hSceneManager.HSkil.ContainsValue(15) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(3))
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
		if (_state != 1 || _modeCtrl != 4)
		{
			float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num2 *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1))
			{
				num2 *= this.ctrlFlag.SkilChangeSpeed(1);
			}
			this.ctrlFlag.feel_m += num2;
			this.ctrlFlag.feel_m = Mathf.Clamp01(this.ctrlFlag.feel_m);
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
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && _modeCtrl != 4)
			{
				this.ctrlFlag.voice.playShorts[0] = 0;
				this.ctrlFlag.voice.playShorts[1] = 0;
			}
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_OrgasmF_IN" : "OrgasmF_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id = this.ctrlFlag.nowAnimationInfo.id;
			if (item == 5 && id == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 0;
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
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				if (urine || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			else
			{
				if (sio)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
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
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
					}
				}
			}
		}
		else if (((this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f) || (this.ctrlFlag.initiative == 2 && this.ctrlFlag.feel_m >= 1f)) && _modeCtrl == 3)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_IN" : "OrgasmM_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 5 && id2 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numInside = Mathf.Clamp(this.ctrlFlag.numInside + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 1;
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && (_modeCtrl != 4 || (_modeCtrl == 4 && _state == 0)))
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_OUT" : "OrgasmM_OUT", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item3 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id3 = this.ctrlFlag.nowAnimationInfo.id;
			if (item3 == 5 && id3 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			if (this.ctrlFlag.feel_f > 0.5f)
			{
				this.ctrlFlag.feel_f = 0.5f;
			}
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 2;
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishSame && this.ctrlFlag.feel_m >= 0.7f && (_modeCtrl != 4 || (_modeCtrl == 4 && _state == 0)))
		{
			this.setPlay((_state != 0) ? "D_OrgasmS_IN" : "OrgasmS_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item4 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id4 = this.ctrlFlag.nowAnimationInfo.id;
			if (item4 == 5 && id4 == 115)
			{
				this.ctrlFlag.AddParam(82, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.ctrlFlag.numSameOrgasm = Mathf.Clamp(this.ctrlFlag.numSameOrgasm + 1, 0, 999999);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.nowOrgasm = true;
			if (!this.ctrlFlag.isPainAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainAction = true;
			}
			if (!this.ctrlFlag.isConstraintAction && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(5))
			{
				this.ctrlFlag.isConstraintAction = true;
			}
			if (_modeCtrl == 3)
			{
				this.ctrlFlag.numInside = Mathf.Clamp(this.ctrlFlag.numInside + 1, 0, 999999);
			}
			else
			{
				this.ctrlFlag.numOutSide = Mathf.Clamp(this.ctrlFlag.numOutSide + 1, 0, 999999);
			}
			this.ctrlFlag.voice.oldFinish = 3;
			bool sio2 = Config.HData.Sio;
			bool urine2 = Config.HData.Urine;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				bool flag2 = this.Hitem.Effect(5);
				if (sio2)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag2) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				if (urine2 || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					this.ctrlFlag.numUrine++;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey3 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey3, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey3, ProcBase.hSceneManager.Toilet);
					}
				}
			}
			else
			{
				if (sio2)
				{
					this.particle.Play(0);
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone && this.ctrlFlag.nowAnimationInfo.nAnimListInfoID != 4)
					{
						this.particle.Play(4);
					}
				}
				if (urine2)
				{
					this.particle.Play(1);
					this.ctrlFlag.voice.urines[0] = true;
					if (this.chaFemales[1].visibleAll && this.chaFemales[1] && this.chaFemales[1].objBodyBone)
					{
						this.particle.Play(5);
						this.ctrlFlag.voice.urines[1] = true;
					}
					int desireKey4 = Desire.GetDesireKey(Desire.Type.Toilet);
					if (ProcBase.hSceneManager.Agent[0] != null)
					{
						ProcBase.hSceneManager.Agent[0].SetDesire(desireKey4, ProcBase.hSceneManager.Toilet);
					}
					if (ProcBase.hSceneManager.Agent[1] != null)
					{
						ProcBase.hSceneManager.Agent[1].SetDesire(desireKey4, ProcBase.hSceneManager.Toilet);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005267 RID: 21095 RVA: 0x00236694 File Offset: 0x00234A94
	private bool AutoAfterTheInsideWaitingProc(int _state, float _wheel)
	{
		if (this.auto.IsPull(this.ctrlFlag.isInsert))
		{
			this.setPlay((_state != 0) ? "D_Pull" : "Pull", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.ctrlFlag.motions[0] = 0f;
			this.ctrlFlag.motions[1] = 0f;
			this.timeMotions[0] = 0f;
			this.timeMotions[1] = 0f;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.nowOrgasm = true;
			this.voice.AfterFinish();
			this.oldHit = false;
			this.feelHit.InitTime();
			this.auto.ReStartInit();
			return true;
		}
		if (!this.auto.IsReStart())
		{
			return false;
		}
		this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
		if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(0))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
			}
			if (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1)
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[1], 1, false);
			}
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.motions[1] = 0f;
		this.timeMotions[0] = 0f;
		this.timeMotions[1] = 0f;
		this.oldHit = false;
		if (_state == 0)
		{
			for (int i = 0; i < 2; i++)
			{
				this.timeChangeMotions[i] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
				this.timeChangeMotionDeltaTimes[i] = 0f;
			}
		}
		this.voice.AfterFinish();
		this.auto.ReStartInit();
		this.auto.PullInit();
		this.ctrlMeta.Clear();
		return true;
	}

	// Token: 0x04004CC8 RID: 19656
	private float[] timeMotions = new float[2];

	// Token: 0x04004CC9 RID: 19657
	private bool[] enableMotions = new bool[2];

	// Token: 0x04004CCA RID: 19658
	private bool[] allowMotions = new bool[]
	{
		true,
		true
	};

	// Token: 0x04004CCB RID: 19659
	private Vector2[] lerpMotions = new Vector2[]
	{
		Vector2.zero,
		Vector2.zero
	};

	// Token: 0x04004CCC RID: 19660
	private float[] lerpTimes = new float[2];

	// Token: 0x04004CCD RID: 19661
	private List<HScene.AnimationListInfo> lstAnimation;

	// Token: 0x04004CCE RID: 19662
	private int nextPlay;

	// Token: 0x04004CCF RID: 19663
	private bool oldHit;

	// Token: 0x04004CD0 RID: 19664
	private int finishMotion;

	// Token: 0x04004CD1 RID: 19665
	private bool finishMorS;

	// Token: 0x04004CD2 RID: 19666
	private ProcBase.animParm animPar;
}
