using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

// Token: 0x02000AFC RID: 2812
public class Houshi : ProcBase
{
	// Token: 0x06005203 RID: 20995 RVA: 0x00221564 File Offset: 0x0021F964
	public Houshi(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[1];
		this.animPar.m = new float[1];
		this.CatID = 1;
	}

	// Token: 0x06005204 RID: 20996 RVA: 0x002215B3 File Offset: 0x0021F9B3
	public void SetAnimationList(List<HScene.AnimationListInfo> _list)
	{
		this.lstAnimation = _list;
	}

	// Token: 0x06005205 RID: 20997 RVA: 0x002215BC File Offset: 0x0021F9BC
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		if (_isIdle)
		{
			this.setPlay((!this.ctrlFlag.isFaintness) ? "Idle" : "D_Idle", false);
			this.ctrlFlag.loopType = -1;
			this.voice.HouchiTime = 0f;
		}
		else
		{
			if (this.ctrlFlag.feel_m >= 0.7f)
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
		this.ctrlMeta.SetParameterFromState((!this.ctrlFlag.isFaintness) ? 0 : 1);
		this.nextPlay = 0;
		this.oldHit = false;
		this.faintnessMouth = (_modeCtrl == 2);
		return true;
	}

	// Token: 0x06005206 RID: 20998 RVA: 0x002217DC File Offset: 0x0021FBDC
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaMales[0].objTop == null || this.chaFemales[0].objTop == null)
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
		this.ctrlMeta.Proc(this.FemaleAi, false);
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
		this.SetFinishCategoryEnable(this.FemaleAi.IsName("OLoop") || this.FemaleAi.IsName("D_OLoop"));
		bool flag = this.FemaleAi.IsName("WLoop") || this.FemaleAi.IsName("SLoop") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop");
		flag &= Config.HData.FinishButton;
		this.sprite.categoryFinish.SetActive(flag, 3);
		this.setAnimationParamater();
		return true;
	}

	// Token: 0x06005207 RID: 20999 RVA: 0x00221AEC File Offset: 0x0021FEEC
	private bool Manual(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(0, false);
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
			this.OLoopProc(0, num, _modeCtrl, _infoAnimList);
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
			this.StartProc(0, true);
		}
		else if (_ai.IsName("Drink_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(0, true);
		}
		else if (_ai.IsName("Vomit_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(0, true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(1, false);
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
			this.OLoopProc(1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm_OUT"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Orgasm_OUT_A", true, true);
		}
		else if (_ai.IsName("D_Orgasm_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(1, true);
		}
		else if (_ai.IsName("D_Orgasm_IN"))
		{
			this.SetAfterInsideFinishAnimation(1, _ai.normalizedTime);
		}
		else if (_ai.IsName("D_Drink_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Drink", false, false);
		}
		else if (_ai.IsName("D_Drink"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Drink_A", true, true);
		}
		else if (_ai.IsName("D_Vomit_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Vomit", false, false);
		}
		else if (_ai.IsName("D_Vomit"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Vomit_A", true, true);
		}
		else if (_ai.IsName("D_Drink_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(1, true);
		}
		else if (_ai.IsName("D_Vomit_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(1, true);
		}
		return true;
	}

	// Token: 0x06005208 RID: 21000 RVA: 0x00221F7C File Offset: 0x0022037C
	private bool Auto(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.AutoStartProcTrigger(false);
			this.AutoStartProc(0, false);
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
			this.AutoOLoopProc(0, num, _modeCtrl, _infoAnimList);
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
			this.AutoStartProc(0, true);
		}
		else if (_ai.IsName("Drink_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartProc(0, true);
		}
		else if (_ai.IsName("Vomit_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartProc(0, true);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(1, false);
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
			this.OLoopProc(1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_Orgasm_OUT"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Orgasm_OUT_A", true, true);
		}
		else if (_ai.IsName("D_Orgasm_OUT_A"))
		{
			this.StartProcTrigger(num);
			this.StartProc(1, true);
		}
		else if (_ai.IsName("D_Orgasm_IN"))
		{
			this.SetAfterInsideFinishAnimation(1, _ai.normalizedTime);
		}
		else if (_ai.IsName("D_Drink_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Drink", false, false);
		}
		else if (_ai.IsName("D_Drink"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Drink_A", true, true);
		}
		else if (_ai.IsName("D_Vomit_IN"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Vomit", false, false);
		}
		else if (_ai.IsName("D_Vomit"))
		{
			this.SetNextFinishAnimation(_ai.normalizedTime, "D_Vomit_A", true, true);
		}
		else if (_ai.IsName("D_Drink_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartProc(1, true);
		}
		else if (_ai.IsName("D_Vomit_A"))
		{
			this.AutoStartProcTrigger(true);
			this.AutoStartProc(1, true);
		}
		return true;
	}

	// Token: 0x06005209 RID: 21001 RVA: 0x002223DC File Offset: 0x002207DC
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

	// Token: 0x0600520A RID: 21002 RVA: 0x00222614 File Offset: 0x00220A14
	private void setPlay(string _playAnimation, bool _isFade = true)
	{
		this.chaFemales[0].setPlay(_playAnimation, 0);
		if (this.chaMales[0].objTop != null)
		{
			this.chaMales[0].setPlay(_playAnimation, 0);
		}
		if (this.item != null)
		{
			this.item.setPlay(_playAnimation);
		}
		for (int i = 0; i < this.lstMotionIK.Count; i++)
		{
			this.lstMotionIK[i].Item3.Calc(_playAnimation);
		}
		if (_isFade)
		{
			this.fade.FadeStart(1f);
		}
	}

	// Token: 0x0600520B RID: 21003 RVA: 0x002226BC File Offset: 0x00220ABC
	private bool LoopProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
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
			this.ctrlFlag.speed += _wheel;
			if (_loop == 0)
			{
				this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 0.5f);
			}
			else
			{
				this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed >= 1.5f);
			}
			if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
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

	// Token: 0x0600520C RID: 21004 RVA: 0x00222CA0 File Offset: 0x002210A0
	private bool OLoopProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
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
			if (item == 1 && id == 25)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 1 && id == 30)
			{
				this.ctrlFlag.AddParam(40, 1);
			}
			else if (item == 1 && id == 104)
			{
				this.ctrlFlag.AddParam(48, 1);
			}
			else if (item == 1 && id == 105)
			{
				this.ctrlFlag.AddParam(54, 1);
			}
			else if (item == 1 && id == 106)
			{
				this.ctrlFlag.AddParam(56, 1);
			}
			else if (item == 1 && id == 107)
			{
				this.ctrlFlag.AddParam(58, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.SetFinishCategoryEnable(false);
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
			this.ctrlFlag.voice.dialog = false;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishDrink && _modeCtrl != 0)
		{
			if (this.faintnessMouth)
			{
				this.setPlay((_state != 0) ? "D_Orgasm_IN" : "Orgasm_IN", true);
			}
			else
			{
				this.setPlay("Orgasm_IN", true);
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 1 && id2 == 25)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item2 == 1 && id2 == 30)
			{
				this.ctrlFlag.AddParam(40, 1);
			}
			else if (item2 == 1 && id2 == 104)
			{
				this.ctrlFlag.AddParam(48, 1);
			}
			else if (item2 == 1 && id2 == 107)
			{
				this.ctrlFlag.AddParam(58, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 0;
			this.SetFinishCategoryEnable(false);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishVomit && _modeCtrl != 0)
		{
			if (this.faintnessMouth)
			{
				this.setPlay((_state != 0) ? "D_Orgasm_IN" : "Orgasm_IN", true);
			}
			else
			{
				this.setPlay("Orgasm_IN", true);
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item3 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id3 = this.ctrlFlag.nowAnimationInfo.id;
			if (item3 == 1 && id3 == 25)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item3 == 1 && id3 == 30)
			{
				this.ctrlFlag.AddParam(40, 1);
			}
			else if (item3 == 1 && id3 == 104)
			{
				this.ctrlFlag.AddParam(48, 1);
			}
			else if (item3 == 1 && id3 == 107)
			{
				this.ctrlFlag.AddParam(58, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.finishMotion = 1;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.SetFinishCategoryEnable(false);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		return true;
	}

	// Token: 0x0600520D RID: 21005 RVA: 0x002234D0 File Offset: 0x002218D0
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

	// Token: 0x0600520E RID: 21006 RVA: 0x00223524 File Offset: 0x00221924
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
			if (this.faintnessMouth)
			{
				this.setPlay((_state != 0) ? "D_Drink_IN" : "Drink_IN", true);
			}
			else
			{
				this.setPlay("Drink_IN", true);
			}
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
			if (this.faintnessMouth)
			{
				this.setPlay((_state != 0) ? "D_Vomit_IN" : "Vomit_IN", true);
			}
			else
			{
				this.setPlay("Vomit_IN", true);
			}
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = -1;
		return true;
	}

	// Token: 0x0600520F RID: 21007 RVA: 0x0022370C File Offset: 0x00221B0C
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

	// Token: 0x06005210 RID: 21008 RVA: 0x00223798 File Offset: 0x00221B98
	private bool StartProc(int _state, bool _restart)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_restart && _state != 1)
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
			this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
		}
		else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
		{
			this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.isNotCtrl = false;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.oldHit = false;
		this.ctrlFlag.motions[0] = 0f;
		this.timeMotion = 0f;
		this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
		this.timeChangeMotionDeltaTimes[0] = 0f;
		this.feelHit.InitTime();
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		this.ctrlFlag.voice.playShorts[0] = 1;
		return true;
	}

	// Token: 0x06005211 RID: 21009 RVA: 0x002239DC File Offset: 0x00221DDC
	private void SetFinishCategoryEnable(bool _enable)
	{
		if (this.sprite.IsFinishVisible(0))
		{
			this.sprite.categoryFinish.SetActive(_enable, 0);
		}
		if (this.sprite.IsFinishVisible(4))
		{
			this.sprite.categoryFinish.SetActive(_enable, 4);
		}
		if (this.sprite.IsFinishVisible(5))
		{
			this.sprite.categoryFinish.SetActive(_enable, 5);
		}
	}

	// Token: 0x06005212 RID: 21010 RVA: 0x00223A54 File Offset: 0x00221E54
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

	// Token: 0x06005213 RID: 21011 RVA: 0x00223AEC File Offset: 0x00221EEC
	private bool AutoStartProc(int _state, bool _restart)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (!_restart && _state != 1)
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
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 1, this.chaFemales[0], 0, false);
			}
			else if (this.ctrlFlag.nowAnimationInfo.hasVoiceCategory.Contains(1))
			{
				this.voice.PlaySoundETC((_state != 0) ? "D_WLoop" : "WLoop", 2, this.chaFemales[0], 0, false);
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
		this.ctrlFlag.motions[0] = 0f;
		this.timeMotion = 0f;
		this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
		this.timeChangeMotionDeltaTimes[0] = 0f;
		this.feelHit.InitTime();
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		this.ctrlFlag.voice.playShorts[0] = 1;
		this.auto.Reset();
		return true;
	}

	// Token: 0x06005214 RID: 21012 RVA: 0x00223D50 File Offset: 0x00222150
	private bool AutoLoopProc(int _loop, int _state, float _wheel, HScene.AnimationListInfo _infoAnimList)
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
			if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
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

	// Token: 0x06005215 RID: 21013 RVA: 0x002242E0 File Offset: 0x002226E0
	private bool AutoOLoopProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
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
			if (item == 1 && id == 25)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 1 && id == 30)
			{
				this.ctrlFlag.AddParam(40, 1);
			}
			else if (item == 1 && id == 104)
			{
				this.ctrlFlag.AddParam(48, 1);
			}
			else if (item == 1 && id == 105)
			{
				this.ctrlFlag.AddParam(54, 1);
			}
			else if (item == 1 && id == 106)
			{
				this.ctrlFlag.AddParam(56, 1);
			}
			else if (item == 1 && id == 107)
			{
				this.ctrlFlag.AddParam(58, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.SetFinishCategoryEnable(false);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishDrink && _modeCtrl != 0)
		{
			if (this.faintnessMouth)
			{
				this.setPlay((_state != 0) ? "D_Orgasm_IN" : "Orgasm_IN", true);
			}
			else
			{
				this.setPlay("Orgasm_IN", true);
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id2 = this.ctrlFlag.nowAnimationInfo.id;
			if (item2 == 1 && id2 == 25)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item2 == 1 && id2 == 30)
			{
				this.ctrlFlag.AddParam(40, 1);
			}
			else if (item2 == 1 && id2 == 104)
			{
				this.ctrlFlag.AddParam(48, 1);
			}
			else if (item2 == 1 && id2 == 107)
			{
				this.ctrlFlag.AddParam(58, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 0;
			this.SetFinishCategoryEnable(false);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishVomit && _modeCtrl != 0)
		{
			if (this.faintnessMouth)
			{
				this.setPlay((_state != 0) ? "D_Orgasm_IN" : "Orgasm_IN", true);
			}
			else
			{
				this.setPlay("Orgasm_IN", true);
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			int item3 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int id3 = this.ctrlFlag.nowAnimationInfo.id;
			if (item3 == 1 && id3 == 25)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item3 == 1 && id3 == 30)
			{
				this.ctrlFlag.AddParam(40, 1);
			}
			else if (item3 == 1 && id3 == 104)
			{
				this.ctrlFlag.AddParam(48, 1);
			}
			else if (item3 == 1 && id3 == 107)
			{
				this.ctrlFlag.AddParam(58, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.isGaugeHit = false;
			this.ctrlFlag.isGaugeHit_M = this.ctrlFlag.isGaugeHit;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit_M;
			this.ctrlFlag.voice.dialog = false;
			this.finishMotion = 1;
			this.SetFinishCategoryEnable(false);
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.isHoushiFinish = true;
			this.ctrlFlag.nowOrgasm = true;
			this.ctrlFlag.voice.oldFinish = 1;
		}
		return true;
	}

	// Token: 0x06005216 RID: 21014 RVA: 0x00224B20 File Offset: 0x00222F20
	private HScene.AnimationListInfo RecoverFaintnessAi()
	{
		for (int i = 0; i < this.lstAnimation.Count; i++)
		{
			if (this.lstAnimation[i].nPositons.Contains(ProcBase.hSceneManager.height))
			{
				if (ProcBase.hSceneManager.bMerchant)
				{
					if (!this.lstAnimation[i].bMerchantMotion)
					{
						goto IL_125;
					}
					if (this.lstAnimation[i].nIyaAction == 2)
					{
						goto IL_125;
					}
				}
				else if (ProcBase.hSceneManager.isForce)
				{
					if (this.lstAnimation[i].nIyaAction == 0)
					{
						goto IL_125;
					}
				}
				else if (this.lstAnimation[i].nIyaAction == 2)
				{
					goto IL_125;
				}
				if (!this.lstAnimation[i].isNeedItem || ProcBase.hSceneManager.CheckHadItem(this.lstAnimation[i].ActionCtrl.Item1, this.lstAnimation[i].id))
				{
					return this.lstAnimation[i];
				}
			}
			IL_125:;
		}
		return this.lstAnimation[0];
	}

	// Token: 0x04004CAB RID: 19627
	private float timeMotion;

	// Token: 0x04004CAC RID: 19628
	private bool enableMotion;

	// Token: 0x04004CAD RID: 19629
	private bool allowMotion = true;

	// Token: 0x04004CAE RID: 19630
	private Vector2 lerpMotion = Vector2.zero;

	// Token: 0x04004CAF RID: 19631
	private float lerpTime;

	// Token: 0x04004CB0 RID: 19632
	private int finishMotion;

	// Token: 0x04004CB1 RID: 19633
	private List<HScene.AnimationListInfo> lstAnimation;

	// Token: 0x04004CB2 RID: 19634
	private int nextPlay;

	// Token: 0x04004CB3 RID: 19635
	private bool oldHit;

	// Token: 0x04004CB4 RID: 19636
	private ProcBase.animParm animPar;

	// Token: 0x04004CB5 RID: 19637
	private bool faintnessMouth;
}
