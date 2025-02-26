using System;
using AIProject.Definitions;
using Manager;
using UnityEngine;

// Token: 0x02000B03 RID: 2819
public class Sonyu : ProcBase
{
	// Token: 0x0600527A RID: 21114 RVA: 0x00236E78 File Offset: 0x00235278
	public Sonyu(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[1];
		this.animPar.m = new float[1];
		this.CatID = 2;
	}

	// Token: 0x0600527B RID: 21115 RVA: 0x00236EC8 File Offset: 0x002352C8
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		int item = _infoAnimList.ActionCtrl.Item1;
		this.bShokushu = (item == 3 && (_modeCtrl == 1 || _modeCtrl == 7));
		this.canInside = ((_infoAnimList.ActionCtrl.Item1 == 2 && _modeCtrl == 0) || this.bShokushu);
		this.sixnine = (_infoAnimList.ActionCtrl.Item1 == 3 && _modeCtrl == 0);
		if (_isIdle)
		{
			if ((item == 2 && _modeCtrl == 1) || this.sixnine)
			{
				this.setPlay((!this.ctrlFlag.isFaintness) ? "Idle" : "D_OrgasmM_OUT_A", false);
			}
			else
			{
				this.setPlay((!this.ctrlFlag.isFaintness) ? "Idle" : "D_Idle", false);
			}
			this.voice.HouchiTime = 0f;
			this.ctrlFlag.loopType = -1;
			this.nowInsert = false;
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
				this.ctrlFlag.loopType = 0;
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.motions[0] = 0f;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.auto.SetSpeed(0f);
			if (_infoAnimList.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainActionParam = true;
			}
		}
		this.ctrlMeta.SetParameterFromState((!this.ctrlFlag.isFaintness) ? 0 : 1);
		this.nextPlay = 0;
		this.oldHit = false;
		return true;
	}

	// Token: 0x0600527C RID: 21116 RVA: 0x002370F4 File Offset: 0x002354F4
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
		this.ctrlMeta.Proc(this.FemaleAi, this.ctrlFlag.isInsert);
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.RecoverFaintness)
		{
			if (this.FemaleAi.IsName("D_Idle") || this.FemaleAi.IsName("D_WLoop") || this.FemaleAi.IsName("D_SLoop") || this.FemaleAi.IsName("D_OLoop") || this.FemaleAi.IsName("D_Orgasm_IN_A") || this.FemaleAi.IsName("D_OrgasmM_OUT_A"))
			{
				base.RecoverFaintnessTaii();
				if (this.ctrlFlag.nowAnimationInfo == this.ctrlFlag.selectAnimationListInfo)
				{
					if ((this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 2 && _modeCtrl == 0) || this.bShokushu)
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
				int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
				int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2;
				this.sprite.SetFinishSelect(2, _modeCtrl, item, item2);
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
		this.SetFinishCategoryEnable(this.FemaleAi);
		this.setAnimationParamater();
		return true;
	}

	// Token: 0x0600527D RID: 21117 RVA: 0x00237438 File Offset: 0x00235838
	private bool Manual(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(false, 0, _modeCtrl);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 0);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.LoopProc(0, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.LoopProc(1, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("OLoop"))
		{
			this.OLoopProc(0, num, _modeCtrl, _infoAnimList);
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
			this.AfterTheInsideWaitingProc(0, num);
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
			this.StartProc(true, 0, _modeCtrl);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(false, 1, _modeCtrl);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (_ai.IsName("D_Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 1);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopProc(0, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopProc(1, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopProc(1, num, _modeCtrl, _infoAnimList);
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
			this.AfterTheInsideWaitingProc(1, num);
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
			this.StartProc(true, 1, _modeCtrl);
		}
		else if (_ai.IsName("Vomit"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 3f, 0, _modeCtrl, (!this.finishMorS) ? 1 : 0);
		}
		else if (_ai.IsName("D_Vomit"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 3f, 1, _modeCtrl, (!this.finishMorS) ? 1 : 0);
		}
		return true;
	}

	// Token: 0x0600527E RID: 21118 RVA: 0x00237940 File Offset: 0x00235D40
	private bool Auto(AnimatorStateInfo _ai, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		if (_ai.IsName("Idle"))
		{
			this.AutoStartProcTrigger(false);
			this.AutoStartProc(false, 0, _modeCtrl);
		}
		else if (_ai.IsName("Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 0);
		}
		else if (_ai.IsName("WLoop"))
		{
			this.AutoLoopProc(0, 0, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("SLoop"))
		{
			this.AutoLoopProc(1, 0, num, _modeCtrl, _infoAnimList);
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
			this.AutoAfterTheInsideWaitingProc(0, num, _modeCtrl);
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
			this.AutoStartProc(true, 0, _modeCtrl);
		}
		else if (_ai.IsName("D_Idle"))
		{
			this.StartProcTrigger(num);
			this.StartProc(false, 1, _modeCtrl);
		}
		else if (_ai.IsName("D_Insert"))
		{
			this.InsertProc(_ai.normalizedTime, 1);
		}
		else if (_ai.IsName("D_WLoop"))
		{
			this.LoopProc(0, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_SLoop"))
		{
			this.LoopProc(1, 1, num, _modeCtrl, _infoAnimList);
		}
		else if (_ai.IsName("D_OLoop"))
		{
			this.OLoopProc(1, num, _modeCtrl, _infoAnimList);
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
			this.AfterTheInsideWaitingProc(1, num);
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
			this.StartProc(true, 1, _modeCtrl);
		}
		else if (_ai.IsName("Vomit"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 3f, 0, _modeCtrl, (!this.finishMorS) ? 1 : 0);
		}
		else if (_ai.IsName("D_Vomit"))
		{
			this.AfterTheNextWaitingAnimation(_ai.normalizedTime, 3f, 1, _modeCtrl, (!this.finishMorS) ? 1 : 0);
		}
		return true;
	}

	// Token: 0x0600527F RID: 21119 RVA: 0x00237E1C File Offset: 0x0023621C
	public override void setAnimationParamater()
	{
		this.animPar.breast = this.chaFemales[0].GetShapeBodyValue(1);
		this.animPar.speed = ((this.ctrlFlag.loopType == 1) ? this.ctrlFlag.speed : (this.ctrlFlag.speed + 1f));
		this.animPar.m[0] = this.ctrlFlag.motions[0];
		if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null)
		{
			this.animPar.heights[0] = this.chaFemales[0].GetShapeBodyValue(0);
			this.chaFemales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.chaFemales[0].setAnimatorParamFloat("speed", this.animPar.speed);
			this.chaFemales[0].setAnimatorParamFloat("motion", this.animPar.m[0]);
			this.chaFemales[0].setAnimatorParamFloat("breast", this.animPar.breast);
		}
		if (this.chaMales[0].visibleAll && this.chaMales[0].objTop != null)
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

	// Token: 0x06005280 RID: 21120 RVA: 0x00238064 File Offset: 0x00236464
	private void setPlay(string _playAnimation, bool _fade = true)
	{
		this.chaFemales[0].setPlay(_playAnimation, 0);
		if (this.chaMales[0].objTop != null && this.chaMales[0].visibleAll)
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
		if (_fade)
		{
			this.fade.FadeStart(1f);
		}
	}

	// Token: 0x06005281 RID: 21121 RVA: 0x0023811C File Offset: 0x0023651C
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

	// Token: 0x06005282 RID: 21122 RVA: 0x002381A8 File Offset: 0x002365A8
	private bool StartProc(bool _restart, int _state, int _modeCtrl)
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
				if (ProcBase.hSceneManager.EventKind != HSceneManager.HEvent.Yobai || _state != 1)
				{
					this.ctrlFlag.voice.playStart = 2;
				}
			}
			else if (ProcBase.hSceneManager.EventKind != HSceneManager.HEvent.Yobai)
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
		if ((this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 2 && _modeCtrl == 0) || this.bShokushu)
		{
			this.setPlay((_state != 0) ? "D_Insert" : "Insert", true);
			this.ctrlFlag.loopType = -1;
			this.nowInsert = true;
		}
		else
		{
			this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
			this.ctrlFlag.loopType = 0;
			if (this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainActionParam = true;
			}
		}
		this.nextPlay = 0;
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.motions[0] = 0f;
		this.ctrlFlag.isNotCtrl = false;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.timeMotion = 0f;
		this.oldHit = false;
		this.feelHit.InitTime();
		if (_state == 0)
		{
			this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[0] = 0f;
		}
		if (_restart)
		{
			this.ctrlMeta.Clear();
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005283 RID: 21123 RVA: 0x002383F8 File Offset: 0x002367F8
	private bool InsertProc(float _normalizedTime, int _state)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.nowInsert = false;
		return true;
	}

	// Token: 0x06005284 RID: 21124 RVA: 0x00238454 File Offset: 0x00236854
	private bool LoopProc(int _loop, int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		bool flag = !this.sixnine || _state != 1;
		int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		int id = this.ctrlFlag.nowAnimationInfo.id;
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f && ((this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 2 && _modeCtrl == 0) || this.bShokushu))
		{
			string[] array = new string[]
			{
				"OrgasmM_IN",
				"D_OrgasmM_IN"
			};
			this.setPlay(array[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && flag)
		{
			string[] array2 = new string[]
			{
				"OrgasmM_OUT",
				"D_OrgasmM_OUT"
			};
			this.setPlay(array2[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
			if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice))
			{
				Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
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
			if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
			{
				this.timeChangeMotionDeltaTimes[0] += Time.deltaTime;
				if (this.timeChangeMotions[0] <= this.timeChangeMotionDeltaTimes[0] && !this.enableMotion && _state == 0)
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
					if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && this.sixnine)
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
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
					if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && this.sixnine)
					{
						Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
					}
					this.feelHit.InitTime();
					this.ctrlFlag.loopType = 0;
				}
				this.ctrlFlag.speed = Mathf.Clamp(this.ctrlFlag.speed, 0f, 2f);
			}
			if (_state != 1 || !this.sixnine)
			{
				float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num2 *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
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
				if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && !this.sixnine && (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 3))
				{
					this.ctrlFlag.voice.playShorts[0] = 0;
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
				if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice))
				{
					Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
				}
			}
		}
		return true;
	}

	// Token: 0x06005285 RID: 21125 RVA: 0x002395C4 File Offset: 0x002379C4
	private bool OLoopProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
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
		bool flag = !this.sixnine || _state != 1;
		int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		int id = this.ctrlFlag.nowAnimationInfo.id;
		if (_state != 1 || !this.sixnine)
		{
			float num = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
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
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.isGaugeHit != this.oldHit && this.ctrlFlag.isGaugeHit)
		{
			if (this.randVoicePlays[0].Get() == 0)
			{
				this.ctrlFlag.voice.playVoices[0] = true;
			}
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && !this.sixnine && (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 3))
			{
				this.ctrlFlag.voice.playShorts[0] = 0;
			}
			this.ctrlFlag.voice.dialog = false;
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_OrgasmF_IN" : "OrgasmF_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
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
			this.ctrlFlag.voice.dialog = false;
			this.ctrlFlag.rateNip = 1f;
			if (Config.HData.Gloss)
			{
				this.ctrlFlag.rateTuya = 1f;
			}
			bool urine = Config.HData.Urine;
			bool sio = Config.HData.Sio;
			if (!ProcBase.hSceneManager.bMerchant)
			{
				bool flag2 = this.Hitem.Effect(5);
				if (sio)
				{
					this.particle.Play(0);
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag2) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
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
		}
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f && this.canInside)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_IN" : "OrgasmM_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && flag)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_OUT" : "OrgasmM_OUT", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishSame && this.ctrlFlag.feel_m >= 0.7f && flag)
		{
			this.setPlay((_state != 0) ? "D_OrgasmS_IN" : "OrgasmS_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
			}
			this.ctrlFlag.feel_m = 0f;
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.isInsert = true;
			this.ctrlFlag.isGaugeHit = false;
			this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.ctrlFlag.numSameOrgasm = Mathf.Clamp(this.ctrlFlag.numSameOrgasm + 1, 0, 999999);
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
			this.ctrlFlag.nowOrgasm = true;
			if (this.canInside)
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
				bool flag3 = this.Hitem.Effect(5);
				if (sio2)
				{
					this.particle.Play(0);
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag3) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
				}
				if (urine2 || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
					ProcBase.hSceneManager.Toilet = 0f;
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Toilet);
					ProcBase.hSceneManager.Agent[0].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
				}
			}
			else
			{
				if (sio2)
				{
					this.particle.Play(0);
				}
				if (urine2)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
				}
			}
		}
		return true;
	}

	// Token: 0x06005286 RID: 21126 RVA: 0x0023AD9C File Offset: 0x0023919C
	private bool GotoFaintness(int _state, int _modeCtrl, int _nextAfter)
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
			bool sio = Config.HData.Sio;
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
			int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
			int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2;
			this.sprite.SetFinishSelect(2, _modeCtrl, item, item2);
		}
		else
		{
			this.setPlay((_state != 0) ? ((_nextAfter != 0) ? "D_OrgasmM_OUT_A" : "D_Orgasm_IN_A") : ((_nextAfter != 0) ? "OrgasmM_OUT_A" : "Orgasm_IN_A"), true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		return true;
	}

	// Token: 0x06005287 RID: 21127 RVA: 0x0023AFB8 File Offset: 0x002393B8
	private bool AfterTheNextWaitingAnimation(float _normalizedTime, float _loopCount, int _state, int _modeCtrl, int _nextAfter)
	{
		if (_normalizedTime < _loopCount)
		{
			return false;
		}
		if (_nextAfter == 0)
		{
			this.GotoFaintness(_state, _modeCtrl, (!this.canInside) ? 1 : 0);
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

	// Token: 0x06005288 RID: 21128 RVA: 0x0023B094 File Offset: 0x00239494
	private bool FinishNextAnimationByMorS(float _normalizedTime, float _loopCount, int _state, int _modeCtrl, bool _finishMorS)
	{
		if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && _modeCtrl == 1)
		{
			if (_normalizedTime < _loopCount)
			{
				return false;
			}
			this.finishMorS = _finishMorS;
			this.setPlay((_state != 0) ? "D_Vomit" : "Vomit", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
		}
		else
		{
			this.AfterTheNextWaitingAnimation(_normalizedTime, _loopCount, _state, _modeCtrl, (!_finishMorS) ? 1 : 0);
		}
		return true;
	}

	// Token: 0x06005289 RID: 21129 RVA: 0x0023B130 File Offset: 0x00239530
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

	// Token: 0x0600528A RID: 21130 RVA: 0x0023B1DC File Offset: 0x002395DC
	private bool AfterTheInsideWaitingProc(int _state, float _wheel)
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
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = 0;
				this.ctrlFlag.nowSpeedStateFast = false;
				this.ctrlFlag.motions[0] = 0f;
				this.timeMotion = 0f;
				if (_state == 0)
				{
					this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
					this.timeChangeMotionDeltaTimes[0] = 0f;
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
			this.ctrlFlag.motions[0] = 0f;
			this.timeMotion = 0f;
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

	// Token: 0x0600528B RID: 21131 RVA: 0x0023B428 File Offset: 0x00239828
	private void SetFinishCategoryEnable(AnimatorStateInfo _ai)
	{
		bool flag = _ai.IsName("WLoop") || _ai.IsName("SLoop") || _ai.IsName("D_WLoop") || _ai.IsName("D_SLoop") || _ai.IsName("OLoop") || _ai.IsName("D_OLoop");
		bool flag2 = this.ctrlFlag.feel_m >= 0.7f && flag;
		bool flag3 = (this.ctrlFlag.feel_f < 0.7f || this.ctrlFlag.feel_m < 0.7f) && flag;
		flag3 &= Config.HData.FinishButton;
		this.sprite.categoryFinish.SetActive(flag3, 3);
		if (this.sprite.IsFinishVisible(0))
		{
			this.sprite.categoryFinish.SetActive(flag2, 0);
		}
		if (this.sprite.IsFinishVisible(1))
		{
			this.sprite.categoryFinish.SetActive(flag2 && this.ctrlFlag.feel_f >= 0.7f, 1);
		}
		if (this.sprite.IsFinishVisible(2))
		{
			this.sprite.categoryFinish.SetActive(flag2, 2);
		}
	}

	// Token: 0x0600528C RID: 21132 RVA: 0x0023B590 File Offset: 0x00239990
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

	// Token: 0x0600528D RID: 21133 RVA: 0x0023B628 File Offset: 0x00239A28
	private bool AutoStartProc(bool _restart, int _state, int _modeCtrl)
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
				if (_state != 1)
				{
					this.ctrlFlag.voice.playStart = 2;
				}
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
			if ((this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 2 && _modeCtrl == 0) || this.bShokushu)
			{
				this.setPlay((_state != 0) ? "D_Insert" : "Insert", true);
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = -1;
				this.nowInsert = true;
			}
			else
			{
				this.setPlay((_state != 0) ? "D_WLoop" : "WLoop", true);
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
		this.timeMotion = 0f;
		this.oldHit = false;
		if (_state == 0)
		{
			this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[0] = 0f;
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

	// Token: 0x0600528E RID: 21134 RVA: 0x0023B870 File Offset: 0x00239C70
	private bool AutoLoopProc(int _loop, int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		bool flag = !this.sixnine || _state != 1;
		int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		int id = this.ctrlFlag.nowAnimationInfo.id;
		bool flag2 = this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f;
		flag2 |= (this.ctrlFlag.initiative == 2 && this.ctrlFlag.feel_m >= 1f);
		flag2 &= ((this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 2 && _modeCtrl == 0) || this.bShokushu);
		if (flag2)
		{
			string[] array = new string[]
			{
				"OrgasmM_IN",
				"D_OrgasmM_IN"
			};
			this.setPlay(array[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && flag)
		{
			string[] array2 = new string[]
			{
				"OrgasmM_OUT",
				"D_OrgasmM_OUT"
			};
			this.setPlay(array2[_state], true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
			this.ctrlFlag.isGaugeHit = false;
		}
		else
		{
			if (this.chaFemales[0].visibleAll && this.chaFemales[0].objBodyBone != null)
			{
				this.timeChangeMotionDeltaTimes[0] += Time.deltaTime;
				if (this.timeChangeMotions[0] <= this.timeChangeMotionDeltaTimes[0] && !this.enableMotion && _state == 0)
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
				if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && this.sixnine)
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
					if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null && (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && this.sixnine)
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
			if (_state == 1)
			{
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
			if (_state != 1 || this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 != 3 || _modeCtrl != 0)
			{
				float num3 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
				num3 *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
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
				if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && !this.sixnine)
				{
					this.ctrlFlag.voice.playShorts[0] = 0;
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

	// Token: 0x0600528F RID: 21135 RVA: 0x0023C9B4 File Offset: 0x0023ADB4
	private bool AutoOLoopProc(int _state, float _wheel, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
		{
			if (this.ctrlFlag.feel_m <= 0.7f)
			{
				this.ctrlFlag.isGaugeHit = false;
				this.ctrlFlag.feel_m = 0.7f;
			}
			return true;
		}
		bool flag = !this.sixnine || _state != 1;
		int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		int id = this.ctrlFlag.nowAnimationInfo.id;
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
		if (_state != 1 || this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 != 3 || _modeCtrl != 0)
		{
			float num2 = Time.deltaTime * this.ctrlFlag.speedGuageRate;
			num2 *= (float)((!this.ctrlFlag.stopFeelMale) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(1) && this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(1))
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
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && !this.sixnine)
			{
				this.ctrlFlag.voice.playShorts[0] = 0;
			}
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 0) ? "D_OrgasmF_IN" : "OrgasmF_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
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
				bool flag2 = this.Hitem.Effect(5);
				if (sio)
				{
					this.particle.Play(0);
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag2) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
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
		}
		else if (((this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishInSide && this.ctrlFlag.feel_m >= 0.7f) || (this.ctrlFlag.initiative == 2 && this.ctrlFlag.feel_m >= 1f)) && this.canInside)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_IN" : "OrgasmM_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishOutSide && this.ctrlFlag.feel_m >= 0.7f && flag)
		{
			this.setPlay((_state != 0) ? "D_OrgasmM_OUT" : "OrgasmM_OUT", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
		else if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishSame && this.ctrlFlag.feel_m >= 0.7f && flag)
		{
			this.setPlay((_state != 0) ? "D_OrgasmS_IN" : "OrgasmS_IN", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
			if (item == 2 && id == 38)
			{
				this.ctrlFlag.AddParam(20, 1);
			}
			else if (item == 2 && id == 44)
			{
				this.ctrlFlag.AddParam(26, 1);
			}
			else if (item == 2 && id == 45)
			{
				this.ctrlFlag.AddParam(38, 1);
			}
			else if (item == 2 && id == 46)
			{
				this.ctrlFlag.AddParam(42, 1);
			}
			else if (item == 2 && id == 113)
			{
				this.ctrlFlag.AddParam(46, 1);
			}
			else if (item == 2 && id == 110)
			{
				this.ctrlFlag.AddParam(50, 1);
			}
			else if (item == 2 && id == 114)
			{
				this.ctrlFlag.AddParam(60, 1);
			}
			else if (item == 2 && id == 115)
			{
				this.ctrlFlag.AddParam(62, 1);
			}
			else if (item == 2 && id == 116)
			{
				this.ctrlFlag.AddParam(64, 1);
			}
			else if (item == 2 && id == 117)
			{
				this.ctrlFlag.AddParam(66, 1);
			}
			else if (item == 2 && id == 118)
			{
				this.ctrlFlag.AddParam(68, 1);
			}
			else if (item == 2 && id == 119)
			{
				this.ctrlFlag.AddParam(70, 1);
			}
			else if (item == 2 && id == 120)
			{
				this.ctrlFlag.AddParam(72, 1);
			}
			else if (item == 3 && id == 109)
			{
				this.ctrlFlag.AddParam(74, 1);
			}
			else if (item == 3 && id == 110)
			{
				this.ctrlFlag.AddParam(76, 1);
			}
			else if (item == 3 && id == 111)
			{
				this.ctrlFlag.AddParam(78, 1);
			}
			if (ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Bath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.MapBath && ProcBase.hSceneManager.EventKind == HSceneManager.HEvent.Tachi)
			{
				this.ctrlFlag.AddParam(28, 1);
			}
			if (this.ctrlFlag.isToilet)
			{
				this.ctrlFlag.AddParam(30, 1);
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
			if (this.canInside)
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
				bool flag3 = this.Hitem.Effect(5);
				if (sio2)
				{
					this.particle.Play(0);
				}
				else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag3) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
				{
					this.particle.Play(0);
				}
				if (urine2 || ProcBase.hSceneManager.Toilet >= 70f)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
					ProcBase.hSceneManager.Toilet = 30f;
					int desireKey2 = Desire.GetDesireKey(Desire.Type.Toilet);
					ProcBase.hSceneManager.Agent[0].SetDesire(desireKey2, ProcBase.hSceneManager.Toilet);
				}
			}
			else
			{
				if (sio2)
				{
					this.particle.Play(0);
				}
				if (urine2)
				{
					this.particle.Play(1);
					this.ctrlFlag.numUrine++;
					this.ctrlFlag.voice.urines[0] = true;
				}
			}
		}
		return true;
	}

	// Token: 0x06005290 RID: 21136 RVA: 0x0023E288 File Offset: 0x0023C688
	private bool AutoAfterTheInsideWaitingProc(int _state, float _wheel, int _modeCtrl)
	{
		if (this.auto.IsPull(this.ctrlFlag.isInsert))
		{
			this.setPlay((_state != 0) ? "D_Pull" : "Pull", true);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.ctrlFlag.nowSpeedStateFast = false;
			this.ctrlFlag.motions[0] = 0f;
			this.timeMotion = 0f;
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
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.nowSpeedStateFast = false;
		this.ctrlFlag.motions[0] = 0f;
		this.timeMotion = 0f;
		this.oldHit = false;
		if (_state == 0)
		{
			this.timeChangeMotions[0] = UnityEngine.Random.Range(this.ctrlFlag.changeAutoMotionTimeMin, this.ctrlFlag.changeAutoMotionTimeMax);
			this.timeChangeMotionDeltaTimes[0] = 0f;
		}
		this.voice.AfterFinish();
		this.auto.ReStartInit();
		this.auto.PullInit();
		this.ctrlMeta.Clear();
		return true;
	}

	// Token: 0x04004CF4 RID: 19700
	private float timeMotion;

	// Token: 0x04004CF5 RID: 19701
	private bool enableMotion;

	// Token: 0x04004CF6 RID: 19702
	private bool allowMotion = true;

	// Token: 0x04004CF7 RID: 19703
	private Vector2 lerpMotion = Vector2.zero;

	// Token: 0x04004CF8 RID: 19704
	private float lerpTime;

	// Token: 0x04004CF9 RID: 19705
	private int nextPlay;

	// Token: 0x04004CFA RID: 19706
	private bool oldHit;

	// Token: 0x04004CFB RID: 19707
	private bool finishMorS;

	// Token: 0x04004CFC RID: 19708
	private ProcBase.animParm animPar;

	// Token: 0x04004CFD RID: 19709
	public bool nowInsert;

	// Token: 0x04004CFE RID: 19710
	private bool canInside;

	// Token: 0x04004CFF RID: 19711
	private bool sixnine;

	// Token: 0x04004D00 RID: 19712
	public bool bShokushu;
}
