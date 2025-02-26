using System;
using AIProject.Definitions;
using Manager;
using UnityEngine;

// Token: 0x02000B04 RID: 2820
public class Spnking : ProcBase
{
	// Token: 0x06005291 RID: 21137 RVA: 0x0023E450 File Offset: 0x0023C850
	public Spnking(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[1];
		this.CatID = 3;
	}

	// Token: 0x06005292 RID: 21138 RVA: 0x0023E474 File Offset: 0x0023C874
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		if (_isIdle)
		{
			this.setPlay((!this.ctrlFlag.isFaintness) ? "WIdle" : "D_Orgasm_A", false);
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.voice.HouchiTime = 0f;
		}
		else
		{
			if (this.ctrlFlag.isFaintness)
			{
				this.setPlay("D_Orgasm_A", false);
			}
			else
			{
				this.setPlay((this.ctrlFlag.feel_f < 0.5f) ? "WIdle" : "SIdle", false);
			}
			this.ctrlFlag.speed = 0f;
			this.ctrlFlag.loopType = -1;
			this.voice.HouchiTime = 0f;
			this.ctrlFlag.motions[0] = 0f;
			this.ctrlFlag.motions[1] = 0f;
			if (_infoAnimList.lstSystem.Contains(4))
			{
				this.ctrlFlag.isPainActionParam = true;
			}
		}
		this.isHeight1Parameter = this.chaFemales[0].IsParameterInAnimator("height1");
		return true;
	}

	// Token: 0x06005293 RID: 21139 RVA: 0x0023E5BC File Offset: 0x0023C9BC
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaMales[0].objTop == null || this.chaFemales[0].objTop == null)
		{
			return false;
		}
		this.FemaleAi = this.chaFemales[0].getAnimatorStateInfo(0);
		if (this.FemaleAi.IsName("WIdle"))
		{
			this.SpankingProc(0);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (this.FemaleAi.IsName("WAction"))
		{
			this.ActionProc(this.FemaleAi.normalizedTime, 0, _infoAnimList);
		}
		if (this.FemaleAi.IsName("SIdle"))
		{
			this.SpankingProc(1);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (this.FemaleAi.IsName("SAction"))
		{
			this.ActionProc(this.FemaleAi.normalizedTime, 1, _infoAnimList);
		}
		else if (this.FemaleAi.IsName("Orgasm"))
		{
			this.AfterWaitingAnimation(this.FemaleAi.normalizedTime, 0);
		}
		else if (this.FemaleAi.IsName("D_Idle"))
		{
			this.SpankingProc(2);
			this.voice.HouchiTime += Time.unscaledDeltaTime;
		}
		else if (this.FemaleAi.IsName("D_Action"))
		{
			this.ActionProc(this.FemaleAi.normalizedTime, 2, _infoAnimList);
		}
		else if (this.FemaleAi.IsName("D_Orgasm"))
		{
			this.AfterWaitingAnimation(this.FemaleAi.normalizedTime, 1);
		}
		else if (this.FemaleAi.IsName("D_Orgasm_A"))
		{
			this.SpankingProc(2);
		}
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.RecoverFaintness)
		{
			if (this.FemaleAi.IsName("D_Orgasm_A"))
			{
				this.setPlay("WIdle", true);
				this.voice.HouchiTime = 0f;
				this.ctrlFlag.isFaintness = false;
				this.sprite.SetVisibleLeaveItToYou(true, true);
				this.ctrlFlag.numOrgasm = 0;
				this.sprite.SetAnimationMenu();
				this.sprite.SetMotionListDraw(false, -1);
				this.Hitem.SetUse(6, false);
			}
			else
			{
				this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.None;
				this.Hitem.SetUse(6, false);
			}
		}
		if (this.upFeel)
		{
			this.timeFeelUp = Mathf.Clamp(this.timeFeelUp + Time.deltaTime, 0f, 0.3f);
			float num = Mathf.Clamp01(this.timeFeelUp / 0.3f);
			if (!this.ctrlFlag.stopFeelFemal)
			{
				float num2 = Mathf.Lerp(this.backupFeel, this.backupFeel + this.ctrlFlag.feelSpnking, num);
				if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(3))
				{
					num2 *= this.ctrlFlag.SkilChangeSpeed(3);
				}
				this.ctrlFlag.feel_f += num2;
				this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
			}
			if (num >= 1f)
			{
				this.upFeel = false;
			}
		}
		this.ctrlMeta.Proc(this.FemaleAi, false);
		this.setAnimationParamater();
		return true;
	}

	// Token: 0x06005294 RID: 21140 RVA: 0x0023E964 File Offset: 0x0023CD64
	public override void setAnimationParamater()
	{
		if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop == null)
		{
			this.animPar.heights[0] = this.chaFemales[0].GetShapeBodyValue(0);
			this.chaFemales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
		}
		if (this.chaMales[0].objTop != null)
		{
			this.chaMales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
		}
		if (this.item.GetItem() != null)
		{
			this.item.setAnimatorParamFloat("height", this.animPar.heights[0]);
		}
	}

	// Token: 0x06005295 RID: 21141 RVA: 0x0023EA40 File Offset: 0x0023CE40
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

	// Token: 0x06005296 RID: 21142 RVA: 0x0023EAE8 File Offset: 0x0023CEE8
	private bool SpankingProc(int _state)
	{
		if (!this.ctrlFlag.stopFeelFemal)
		{
			this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f - this.ctrlFlag.guageDecreaseRate * Time.deltaTime);
		}
		if (_state == 1 && this.ctrlFlag.feel_f < 0.5f)
		{
			this.setPlay("WIdle", true);
			this.voice.HouchiTime = 0f;
			return true;
		}
		float num = Singleton<Manager.Input>.Instance.ScrollValue() * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		if (num == 0f)
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
		this.setPlay((_state != 0) ? ((_state != 1) ? "D_Action" : "SAction") : "WAction", false);
		this.upFeel = true;
		this.timeFeelUp = 0f;
		this.backupFeel = this.ctrlFlag.feel_f;
		this.ctrlFlag.isNotCtrl = false;
		if (this.randVoicePlays[0].Get() == 0)
		{
			this.ctrlFlag.voice.playVoices[0] = true;
		}
		else if (this.randVoicePlays[1].Get() == 0)
		{
			this.ctrlFlag.voice.playVoices[1] = true;
		}
		this.ctrlFlag.voice.playShorts[0] = 0;
		return true;
	}

	// Token: 0x06005297 RID: 21143 RVA: 0x0023ECA0 File Offset: 0x0023D0A0
	private bool ActionProc(float _normalizedTime, int _state, HScene.AnimationListInfo _infoAnimList)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		if (_state == 0)
		{
			this.setPlay(((double)this.ctrlFlag.feel_f < 0.5) ? "WIdle" : "SIdle", false);
			this.voice.HouchiTime = 0f;
		}
		else if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
		{
			this.setPlay((_state != 1) ? "D_Orgasm" : "Orgasm", true);
			this.ctrlFlag.feel_f = 0f;
			this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
			this.ctrlFlag.AddOrgasm();
			this.sprite.objMotionListPanel.SetActive(false);
			this.sprite.SetEnableCategoryMain(false);
			this.sprite.SetEnableHItem(false);
			this.ctrlFlag.voice.oldFinish = 0;
			this.ctrlFlag.nowOrgasm = true;
			if (_state != 0)
			{
				this.ctrlFlag.AddParam(15, 1);
			}
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
		}
		else
		{
			this.setPlay((_state != 1) ? "D_Orgasm_A" : "SIdle", false);
		}
		return true;
	}

	// Token: 0x06005298 RID: 21144 RVA: 0x0023F008 File Offset: 0x0023D408
	private bool AfterWaitingAnimation(float _normalizedTime, int _state)
	{
		if (_normalizedTime < 1f)
		{
			return false;
		}
		bool flag = this.Hitem.Effect(5);
		if (_state == 0 && (this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount || flag))
		{
			this.setPlay("D_Orgasm_A", true);
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
			this.sprite.SetAnimationMenu();
		}
		else
		{
			this.setPlay((_state != 0) ? "D_Orgasm_A" : "WIdle", false);
			this.voice.HouchiTime = 0f;
		}
		this.ctrlFlag.nowOrgasm = false;
		this.voice.AfterFinish();
		return true;
	}

	// Token: 0x04004D01 RID: 19713
	private bool upFeel;

	// Token: 0x04004D02 RID: 19714
	private float backupFeel;

	// Token: 0x04004D03 RID: 19715
	private float timeFeelUp;

	// Token: 0x04004D04 RID: 19716
	private ProcBase.animParm animPar;
}
