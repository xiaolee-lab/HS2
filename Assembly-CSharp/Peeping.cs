using System;
using System.Collections;
using AIProject.Scene;
using UnityEngine;

// Token: 0x02000B00 RID: 2816
public class Peeping : ProcBase
{
	// Token: 0x06005268 RID: 21096 RVA: 0x00236A1B File Offset: 0x00234E1B
	public Peeping(DeliveryMember _delivery) : base(_delivery)
	{
		this.animPar.heights = new float[1];
		this.CatID = 5;
	}

	// Token: 0x06005269 RID: 21097 RVA: 0x00236A3C File Offset: 0x00234E3C
	public override bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		this.AtariEffect.Stop();
		this.ctrlFlag.isNotCtrl = false;
		this.oldFrame = 0f;
		return true;
	}

	// Token: 0x0600526A RID: 21098 RVA: 0x00236A64 File Offset: 0x00234E64
	public override bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		if (this.chaFemales[0].objTop == null)
		{
			return false;
		}
		this.FemaleAi = this.chaFemales[0].getAnimatorStateInfo(0);
		float num = this.FemaleAi.normalizedTime % 1f;
		if (this.ctrlFlag.nowAnimationInfo.id == 10 && !this.sprite.isFade)
		{
			HSceneSprite.FadeKindProc fadeKindProc = this.sprite.GetFadeKindProc();
			if (fadeKindProc != HSceneSprite.FadeKindProc.OutEnd)
			{
				if (num > 0.93f)
				{
					this.sprite.FadeState(HSceneSprite.FadeKind.Out, 1.5f);
				}
				if (this.oldFrame <= 0.05f && 0.05f < num)
				{
					GlobalMethod.SetAllClothState(this.chaFemales[0], false, 2, true);
				}
				else if (this.oldFrame <= 0.21f && 0.21f < num)
				{
					this.particle.Play(2);
				}
				else if (this.oldFrame <= 0.82f && 0.82f < num)
				{
					GlobalMethod.SetAllClothState(this.chaFemales[0], false, 0, true);
				}
			}
			else if (fadeKindProc == HSceneSprite.FadeKindProc.OutEnd)
			{
				GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, false);
				this.chaFemales[0].animBody.speed = 0f;
				ConfirmScene.Sentence = string.Empty;
				ConfirmScene.OnClickedYes = delegate()
				{
					this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.PeepingRestart;
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.SceneEnd;
				};
				if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.PeepingRestart)
				{
					this.setRePlay(this.FemaleAi, 0f, false);
					this.sprite.FadeState(HSceneSprite.FadeKind.In, 0.5f);
					this.chaFemales[0].animBody.speed = 1f;
					this.voice.StartCoroutine(this.InitOldMemberCoroutine());
				}
			}
		}
		this.oldFrame = num;
		this.ctrlMeta.Proc(this.FemaleAi, false);
		this.setAnimationParamater();
		return true;
	}

	// Token: 0x0600526B RID: 21099 RVA: 0x00236C70 File Offset: 0x00235070
	public override void setAnimationParamater()
	{
		if (this.chaFemales[0].visibleAll && this.chaFemales[0].objTop != null)
		{
			this.animPar.heights[0] = this.chaFemales[0].GetShapeBodyValue(0);
			this.chaFemales[0].setAnimatorParamFloat("height", this.animPar.heights[0]);
		}
		if (this.item.GetItem() != null)
		{
			this.item.setAnimatorParamFloat("height", this.animPar.heights[0]);
		}
	}

	// Token: 0x0600526C RID: 21100 RVA: 0x00236D14 File Offset: 0x00235114
	private void setRePlay(AnimatorStateInfo _ai, float _normalizetime, bool _isFade = true)
	{
		this.chaFemales[0].syncPlay(_ai.shortNameHash, 0, _normalizetime);
		if (this.item != null)
		{
			this.item.syncPlay(_ai.shortNameHash, _normalizetime);
		}
		if (_isFade)
		{
			this.fade.FadeStart(1f);
		}
	}

	// Token: 0x0600526D RID: 21101 RVA: 0x00236D6C File Offset: 0x0023516C
	private IEnumerator InitOldMemberCoroutine()
	{
		WaitForEndOfFrame tmp = new WaitForEndOfFrame();
		yield return tmp;
		this.se.InitOldMember(1);
		this.oldFrame = 0f;
		yield return null;
		yield break;
	}

	// Token: 0x04004CD3 RID: 19667
	private float oldFrame;

	// Token: 0x04004CD4 RID: 19668
	private ProcBase.animParm animPar;
}
