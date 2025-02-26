using System;
using System.Collections.Generic;
using AIProject;
using Manager;
using UnityEngine;

// Token: 0x02000AFE RID: 2814
public class Masturbation : ProcBase
{
	// Token: 0x0600522B RID: 21035 RVA: 0x002283E8 File Offset: 0x002267E8
	public Masturbation(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[1];
		this.aTimer[0] = new RandomTimer();
		this.aTimer[1] = new RandomTimer();
		this.Timers = new float[][]
		{
			new float[2],
			new float[2]
		};
		string strAssetHAutoListFolder = Singleton<HSceneManager>.Instance.strAssetHAutoListFolder;
		string text = "masturbation";
		if (!GlobalMethod.AssetFileExist(strAssetHAutoListFolder, text, string.Empty))
		{
			return;
		}
		ExcelData excelData = CommonLib.LoadAsset<ExcelData>(strAssetHAutoListFolder, text, false, string.Empty);
		Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(strAssetHAutoListFolder);
		if (excelData == null)
		{
			return;
		}
		int i = 2;
		while (i < excelData.MaxCell)
		{
			this.row.Clear();
			foreach (string item in excelData.list[i++].list)
			{
				this.row.Add(item);
			}
			int num = 0;
			this.Timers[0] = new float[]
			{
				float.Parse(this.row.GetElement(num++)),
				float.Parse(this.row.GetElement(num++))
			};
			this.Timers[1] = new float[]
			{
				float.Parse(this.row.GetElement(num++)),
				float.Parse(this.row.GetElement(num++))
			};
			this.aTimer[0].Init(this.Timers[0][0], this.Timers[0][0]);
			this.aTimer[1].Init(this.Timers[1][1], this.Timers[1][1]);
		}
		this.CatID = 4;
	}

	// Token: 0x0600522C RID: 21036 RVA: 0x00228638 File Offset: 0x00226A38
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		this.TimerReset();
		int id = _infoAnimList.id;
		this.bAuto = !this.notAutoID.Contains(id);
		if (_isIdle)
		{
			this.setPlay("Idle", false);
			this.ctrlFlag.loopType = -1;
			this.voice.HouchiTime = 0f;
		}
		else
		{
			if (this.ctrlFlag.feel_f >= 0.7f)
			{
				this.setPlay("OLoop", false);
				this.ctrlFlag.loopType = -1;
			}
			else if (this.ctrlFlag.feel_f >= 0.5f)
			{
				this.setPlay("SLoop", false);
				this.ctrlFlag.loopType = -1;
			}
			else if (this.ctrlFlag.feel_f >= 0.25f)
			{
				this.setPlay("MLoop", false);
				this.ctrlFlag.loopType = -1;
			}
			else
			{
				this.setPlay("WLoop", false);
				this.ctrlFlag.loopType = -1;
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
		this.ctrlFlag.speed = 1f;
		this.ctrlFlag.loopType = -1;
		this.oldHit = false;
		this.nextPlay = 0;
		return true;
	}

	// Token: 0x0600522D RID: 21037 RVA: 0x002287DC File Offset: 0x00226BDC
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaFemales[0].objTop == null)
		{
			return false;
		}
		AnimatorStateInfo animatorStateInfo = this.chaFemales[0].getAnimatorStateInfo(0);
		bool animatorParamBool = this.chaFemales[0].getAnimatorParamBool("change");
		if (!this.bAuto)
		{
			this.Manual(animatorStateInfo, animatorParamBool);
		}
		else
		{
			this.Auto(animatorStateInfo, animatorParamBool);
		}
		this.ctrlMeta.Proc(animatorStateInfo, false);
		int id = this.ctrlFlag.nowAnimationInfo.id;
		if (id == 3 || id == 13 || id == 14)
		{
			bool flag = animatorStateInfo.IsName("WLoop") || animatorStateInfo.IsName("MLoop") || animatorStateInfo.IsName("SLoop");
			flag &= Config.HData.FinishButton;
			this.sprite.categoryFinish.SetActive(flag, 3);
		}
		this.setAnimationParamater();
		return true;
	}

	// Token: 0x0600522E RID: 21038 RVA: 0x002288E0 File Offset: 0x00226CE0
	private void Manual(AnimatorStateInfo ai, bool isChangeTrigger)
	{
		float num = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)((!this.sprite.IsSpriteOver()) ? 1 : 0);
		num = ((num >= 0f) ? ((num <= 0f) ? 0f : this.ctrlFlag.wheelActionCount) : (-this.ctrlFlag.wheelActionCount));
		bool flag = false;
		int id = this.ctrlFlag.nowAnimationInfo.id;
		if (id == 3 || id == 13 || id == 14)
		{
			bool flag2 = ai.IsName("WLoop") || ai.IsName("MLoop") || ai.IsName("SLoop");
			if (flag2 && this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
			{
				this.ctrlFlag.speed = 0f;
				this.ctrlFlag.loopType = -1;
				this.nextAnimation = "OLoop";
				this.chaFemales[0].setAnimatorParamBool("change", true);
				this.item.setAnimatorParamBool("change", true);
				this.setPlay("OLoop", true);
				this.ctrlFlag.feel_f = 0.7f;
				flag = true;
				this.oldHit = false;
				this.ctrlFlag.isGaugeHit = false;
			}
		}
		if (!flag)
		{
			this.ctrlFlag.speed += num;
			this.ctrlFlag.speed = Mathf.Clamp(this.ctrlFlag.speed, 0f, 1f);
			this.ctrlFlag.nowSpeedStateFast = (this.ctrlFlag.speed > 0.5f);
			if (ai.IsName("Idle"))
			{
				this.StartProcTrigger(num);
				this.StartProcManual(0);
				this.voice.HouchiTime += Time.unscaledDeltaTime;
			}
			else if (ai.IsName("WLoop"))
			{
				this.GotoNextLoop(0.25f, isChangeTrigger, "MLoop", this.ctrlFlag.nowAnimationInfo, 0);
			}
			else if (ai.IsName("MLoop"))
			{
				this.GotoNextLoop(0.5f, isChangeTrigger, "SLoop", this.ctrlFlag.nowAnimationInfo, 0);
			}
			else if (ai.IsName("SLoop"))
			{
				this.GotoNextLoop(0.7f, isChangeTrigger, "OLoop", this.ctrlFlag.nowAnimationInfo, 1);
			}
			else if (ai.IsName("OLoop"))
			{
				this.ctrlFlag.isGaugeHit = this.feelHit.isHit(this.ctrlFlag.nowAnimationInfo.nFeelHit, 2, this.ctrlFlag.speed);
				this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
				if (this.ctrlFlag.isGaugeHit)
				{
					float num2 = this.ctrlFlag.speedGuageRate * Time.deltaTime;
					num2 *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
					if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(3))
					{
						num2 *= this.ctrlFlag.SkilChangeSpeed(3);
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
					if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && (this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 1 || this.ctrlFlag.nowAnimationInfo.nShortBreahtPlay == 3))
					{
						this.ctrlFlag.voice.playShorts[0] = 0;
					}
					this.ctrlFlag.voice.dialog = false;
				}
				this.oldHit = this.ctrlFlag.isGaugeHit;
				if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
				{
					this.setPlay("Orgasm", true);
					this.ctrlFlag.speed = 0f;
					this.ctrlFlag.loopType = -1;
					if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && this.ctrlFlag.nowAnimationInfo.id == 13)
					{
						this.ctrlFlag.AddParam(18, 1);
					}
					else if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && this.ctrlFlag.nowAnimationInfo.id == 14)
					{
						this.ctrlFlag.AddParam(36, 1);
					}
					this.ctrlFlag.feel_f = 0f;
					this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
					this.ctrlFlag.AddOrgasm();
					this.ctrlFlag.voice.oldFinish = 0;
					this.sprite.objMotionListPanel.SetActive(false);
					this.sprite.SetEnableCategoryMain(false);
					this.sprite.SetEnableHItem(false);
					this.ctrlFlag.nowOrgasm = true;
					this.ctrlFlag.isGaugeHit = false;
					this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
					this.ctrlFlag.rateNip = 1f;
					if (Config.HData.Gloss)
					{
						this.ctrlFlag.rateTuya = 1f;
					}
					bool sio = Config.HData.Sio;
					if (!ProcBase.hSceneManager.bMerchant)
					{
						bool flag3 = this.Hitem.Effect(5);
						if (sio)
						{
							this.particle.Play(0);
						}
						else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag3) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
						{
							this.particle.Play(0);
						}
					}
					else if (sio)
					{
						this.particle.Play(0);
					}
				}
			}
			else if (ai.IsName("Orgasm"))
			{
				bool flag4 = (this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.voice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && Singleton<Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[0], true);
				if (ai.normalizedTime >= 1f && !flag4)
				{
					this.setPlay("Orgasm_A", true);
					this.ctrlFlag.speed = 0f;
					this.ctrlFlag.loopType = -1;
					this.ctrlFlag.nowOrgasm = false;
				}
			}
			else if (ai.IsName("Orgasm_A"))
			{
				this.StartProcTrigger(num);
				this.StartProcManual(1);
			}
		}
		if (isChangeTrigger && ai.IsName(this.nextAnimation))
		{
			this.chaFemales[0].setAnimatorParamBool("change", false);
		}
	}

	// Token: 0x0600522F RID: 21039 RVA: 0x0022909C File Offset: 0x0022749C
	private void Auto(AnimatorStateInfo ai, bool isChangeTrigger)
	{
		bool flag = false;
		int id = this.ctrlFlag.nowAnimationInfo.id;
		if (id == 3 || id == 13 || id == 14)
		{
			bool flag2 = ai.IsName("WLoop") || ai.IsName("MLoop") || ai.IsName("SLoop");
			if (flag2 && this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.FinishBefore)
			{
				this.ctrlFlag.speed = 1f;
				this.ctrlFlag.loopType = -1;
				this.nextAnimation = "OLoop";
				this.chaFemales[0].setAnimatorParamBool("change", true);
				this.item.setAnimatorParamBool("change", true);
				this.setPlay("OLoop", true);
				this.ctrlFlag.feel_f = 0.75f;
				flag = true;
			}
		}
		if (!flag)
		{
			if (ai.IsName("Idle"))
			{
				this.StartProc(0);
			}
			else if (ai.IsName("WLoop"))
			{
				this.GotoNextLoop(0.25f, isChangeTrigger, "MLoop");
				this.ctrlFlag.speed = 1f;
				this.ctrlFlag.loopType = -1;
			}
			else if (ai.IsName("MLoop"))
			{
				this.GotoNextLoop(0.5f, isChangeTrigger, "SLoop");
				this.ctrlFlag.speed = 1f;
				this.ctrlFlag.loopType = -1;
			}
			else if (ai.IsName("SLoop"))
			{
				this.GotoNextLoop(0.75f, isChangeTrigger, "OLoop");
				this.ctrlFlag.speed = 1f;
				this.ctrlFlag.loopType = -1;
			}
			else if (ai.IsName("OLoop"))
			{
				float num = this.ctrlFlag.speedGuageRate * Time.deltaTime;
				num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
				if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(3))
				{
					num *= this.ctrlFlag.SkilChangeSpeed(3);
				}
				this.ctrlFlag.feel_f += num;
				this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
				if (this.ctrlFlag.selectAnimationListInfo == null && this.ctrlFlag.feel_f >= 1f)
				{
					this.setPlay("Orgasm", true);
					this.ctrlFlag.speed = 1f;
					this.ctrlFlag.loopType = -1;
					if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && this.ctrlFlag.nowAnimationInfo.id == 13)
					{
						this.ctrlFlag.AddParam(18, 1);
					}
					else if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && this.ctrlFlag.nowAnimationInfo.id == 14)
					{
						this.ctrlFlag.AddParam(36, 1);
					}
					this.ctrlFlag.feel_f = 0f;
					this.ctrlFlag.numOrgasm = Mathf.Clamp(this.ctrlFlag.numOrgasm + 1, 0, 10);
					this.ctrlFlag.AddOrgasm();
					this.ctrlFlag.voice.oldFinish = 0;
					this.sprite.objMotionListPanel.SetActive(false);
					this.sprite.SetEnableCategoryMain(false);
					this.sprite.SetEnableHItem(false);
					this.ctrlFlag.nowOrgasm = true;
					this.ctrlFlag.rateNip = 1f;
					if (Config.HData.Gloss)
					{
						this.ctrlFlag.rateTuya = 1f;
					}
					bool sio = Config.HData.Sio;
					if (!ProcBase.hSceneManager.bMerchant)
					{
						bool flag3 = this.Hitem.Effect(5);
						if (sio)
						{
							this.particle.Play(0);
						}
						else if (((this.ctrlFlag.numFaintness == 0 && this.ctrlFlag.numOrgasm >= this.ctrlFlag.gotoFaintnessCount) || flag3) && ProcBase.hSceneManager.GetFlaverSkillLevel(2) >= 100)
						{
							this.particle.Play(0);
						}
					}
					else if (sio)
					{
						this.particle.Play(0);
					}
				}
			}
			else if (ai.IsName("Orgasm"))
			{
				if (ai.normalizedTime >= 1f)
				{
					this.setPlay("Orgasm_A", true);
					this.ctrlFlag.speed = 1f;
					this.ctrlFlag.loopType = -1;
					this.ctrlFlag.nowOrgasm = false;
				}
			}
			else if (ai.IsName("Orgasm_A"))
			{
				this.StartProc(1);
			}
		}
		if (isChangeTrigger && ai.IsName(this.nextAnimation))
		{
			this.chaFemales[0].setAnimatorParamBool("change", false);
		}
	}

	// Token: 0x06005230 RID: 21040 RVA: 0x002295F8 File Offset: 0x002279F8
	public override void setAnimationParamater()
	{
		this.animPar.speed = ((!this.bAuto) ? (this.ctrlFlag.speed + 1f) : this.ctrlFlag.speed);
		if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null)
		{
			this.animPar.heights[0] = this.chaFemales[0].GetShapeBodyValue(0);
			this.chaFemales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.chaFemales[0].setAnimatorParamFloat("speed", this.animPar.speed);
		}
		if (this.item.GetItem() != null)
		{
			this.item.setAnimatorParamFloat("height", this.animPar.heights[0]);
			this.item.setAnimatorParamFloat("speed", this.animPar.speed);
		}
	}

	// Token: 0x06005231 RID: 21041 RVA: 0x0022970C File Offset: 0x00227B0C
	private void setPlay(string _playAnimation, bool _isFade = true)
	{
		this.chaFemales[0].setPlay(_playAnimation, 0);
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

	// Token: 0x06005232 RID: 21042 RVA: 0x0022978C File Offset: 0x00227B8C
	private bool StartProc(int _start)
	{
		if (!this.aTimer[_start].Check())
		{
			return false;
		}
		this.setPlay("WLoop", true);
		this.ctrlFlag.speed = 1f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.isNotCtrl = false;
		this.timeChangeSpeed = 0f;
		this.chaFemales[0].setAnimatorParamBool("change", false);
		this.item.setAnimatorParamBool("change", false);
		if (_start == 1)
		{
			this.voice.AfterFinish();
		}
		return true;
	}

	// Token: 0x06005233 RID: 21043 RVA: 0x00229824 File Offset: 0x00227C24
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

	// Token: 0x06005234 RID: 21044 RVA: 0x002298B0 File Offset: 0x00227CB0
	private bool StartProcManual(int _start)
	{
		if (this.nextPlay == 0)
		{
			return false;
		}
		if (this.nextPlay == 1)
		{
			this.nextPlay = 2;
			if (_start == 0)
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
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = 0;
		this.ctrlFlag.isNotCtrl = false;
		this.timeChangeSpeed = 0f;
		this.chaFemales[0].setAnimatorParamBool("change", false);
		this.item.setAnimatorParamBool("change", false);
		if (_start == 1)
		{
			this.voice.AfterFinish();
		}
		this.oldHit = false;
		return true;
	}

	// Token: 0x06005235 RID: 21045 RVA: 0x002299FC File Offset: 0x00227DFC
	private bool GotoNextLoop(float _range, bool _isChangeTrigger, string _nextAnimation)
	{
		float num = this.ctrlFlag.speedGuageRate * Time.deltaTime;
		num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
		if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(3))
		{
			num *= this.ctrlFlag.SkilChangeSpeed(3);
		}
		this.ctrlFlag.feel_f += num;
		this.ctrlFlag.feel_f = Mathf.Clamp01(this.ctrlFlag.feel_f);
		if (this.ctrlFlag.feel_f < _range || _isChangeTrigger)
		{
			return false;
		}
		this.ctrlFlag.speed = 1f;
		this.nextAnimation = _nextAnimation;
		this.chaFemales[0].setAnimatorParamBool("change", true);
		this.item.setAnimatorParamBool("change", true);
		this.setPlay(_nextAnimation, true);
		return true;
	}

	// Token: 0x06005236 RID: 21046 RVA: 0x00229AFC File Offset: 0x00227EFC
	private bool GotoNextLoop(float _range, bool _isChangeTrigger, string _nextAnimation, HScene.AnimationListInfo _infoAnimList, int _loop)
	{
		this.ctrlFlag.isGaugeHit = this.feelHit.isHit(_infoAnimList.nFeelHit, _loop, this.ctrlFlag.speed);
		this.isAtariHit.Value = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.isGaugeHit)
		{
			float num = this.ctrlFlag.speedGuageRate * Time.deltaTime;
			num *= (float)((!this.ctrlFlag.stopFeelFemal) ? 1 : 0);
			if (!ProcBase.hSceneManager.bMerchant && ProcBase.hSceneManager.HSkil.ContainsValue(3))
			{
				num *= this.ctrlFlag.SkilChangeSpeed(3);
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
			if (!this.ctrlFlag.nowAnimationInfo.lstSystem.Contains(0) && (_infoAnimList.nShortBreahtPlay == 1 || _infoAnimList.nShortBreahtPlay == 3))
			{
				this.ctrlFlag.voice.playShorts[0] = 0;
			}
			this.ctrlFlag.voice.dialog = false;
		}
		this.oldHit = this.ctrlFlag.isGaugeHit;
		if (this.ctrlFlag.feel_f < _range || _isChangeTrigger)
		{
			return false;
		}
		this.ctrlFlag.speed = 0f;
		this.ctrlFlag.loopType = -1;
		this.nextAnimation = _nextAnimation;
		this.chaFemales[0].setAnimatorParamBool("change", true);
		this.item.setAnimatorParamBool("change", true);
		this.setPlay(_nextAnimation, true);
		this.oldHit = false;
		return true;
	}

	// Token: 0x06005237 RID: 21047 RVA: 0x00229D14 File Offset: 0x00228114
	private void TimerReset()
	{
		this.aTimer[0] = new RandomTimer();
		this.aTimer[1] = new RandomTimer();
		this.aTimer[0].Init(this.Timers[0][0], this.Timers[0][0]);
		this.aTimer[1].Init(this.Timers[1][1], this.Timers[1][1]);
	}

	// Token: 0x04004CBE RID: 19646
	private string nextAnimation;

	// Token: 0x04004CBF RID: 19647
	private float timeChangeSpeed;

	// Token: 0x04004CC0 RID: 19648
	private RandomTimer[] aTimer = new RandomTimer[2];

	// Token: 0x04004CC1 RID: 19649
	private ProcBase.animParm animPar;

	// Token: 0x04004CC2 RID: 19650
	private List<string> row = new List<string>();

	// Token: 0x04004CC3 RID: 19651
	private float[][] Timers;

	// Token: 0x04004CC4 RID: 19652
	private bool bAuto = true;

	// Token: 0x04004CC5 RID: 19653
	private readonly List<int> notAutoID = new List<int>
	{
		3,
		13,
		14
	};

	// Token: 0x04004CC6 RID: 19654
	private bool oldHit;

	// Token: 0x04004CC7 RID: 19655
	private int nextPlay;
}
