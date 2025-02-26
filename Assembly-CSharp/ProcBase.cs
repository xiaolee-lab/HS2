using System;
using System.Collections.Generic;
using System.Text;
using AIChara;
using Manager;
using UniRx;
using UnityEngine;

// Token: 0x02000B01 RID: 2817
public class ProcBase
{
	// Token: 0x06005270 RID: 21104 RVA: 0x0021D96C File Offset: 0x0021BD6C
	public ProcBase(DeliveryMember _delivery)
	{
		this.ctrlFlag = _delivery.ctrlFlag;
		this.chaMales = _delivery.chaMales;
		this.chaFemales = _delivery.chaFemales;
		this.fade = _delivery.fade;
		this.ctrlMeta = _delivery.ctrlMeta;
		this.sprite = _delivery.sprite;
		this.item = _delivery.item;
		this.feelHit = _delivery.feelHit;
		this.auto = _delivery.auto;
		this.voice = _delivery.voice;
		this.particle = _delivery.particle;
		this.se = _delivery.se;
		this.lstMotionIK = _delivery.lstMotionIK;
		this.AtariEffect = _delivery.AtariEffect;
		this.FeelHitEffect3D = _delivery.FeelHitEffect3D;
		this.Hitem = this.sprite.objHItem.GetComponent<HSceneSpriteHitem>();
		if (ProcBase.hSceneManager == null)
		{
			ProcBase.hSceneManager = Singleton<HSceneManager>.Instance;
		}
		for (int i = 0; i < 2; i++)
		{
			this.randVoicePlays[i] = new ShuffleRand(-1);
			this.randVoicePlays[i].Init((i != 0) ? 2 : 3);
		}
		(from x in this.isAtariHit
		where this.isAtariHitOld != x && this.CatID != 1
		select x).Subscribe(delegate(bool x)
		{
			if ((this.CatID == 7 && Singleton<HSceneFlagCtrl>.Instance.nowAnimationInfo.ActionCtrl.Item2 == 1) || Singleton<HSceneFlagCtrl>.Instance.nowAnimationInfo.ActionCtrl.Item2 == 2)
			{
				return;
			}
			this.isAtariHitOld = x;
			if (x)
			{
				this.AtariEffect.Play();
				if (Singleton<HSceneManager>.Instance.isParticle)
				{
					this.FeelHitEffect3D.Play();
				}
			}
			else
			{
				this.AtariEffect.Stop();
				this.FeelHitEffect3D.Stop();
			}
		});
	}

	// Token: 0x06005271 RID: 21105 RVA: 0x0021DB14 File Offset: 0x0021BF14
	public virtual bool Init(int _modeCtrl)
	{
		ProcBase.endInit = true;
		this.ctrlFlag.lstSyncAnimLayers[0, 0].Clear();
		this.ctrlFlag.lstSyncAnimLayers[0, 1].Clear();
		this.ctrlFlag.lstSyncAnimLayers[1, 0].Clear();
		this.ctrlFlag.lstSyncAnimLayers[1, 1].Clear();
		Singleton<GameCursor>.Instance.setCursor(GameCursor.CursorKind.None, Vector2.zero);
		return true;
	}

	// Token: 0x06005272 RID: 21106 RVA: 0x0021DB95 File Offset: 0x0021BF95
	public virtual bool SetStartMotion(bool _isIdle, int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		return true;
	}

	// Token: 0x06005273 RID: 21107 RVA: 0x0021DB98 File Offset: 0x0021BF98
	public virtual bool Proc(int _modeCtrl, HScene.AnimationListInfo _infoAnimList)
	{
		return true;
	}

	// Token: 0x06005274 RID: 21108 RVA: 0x0021DB9B File Offset: 0x0021BF9B
	public virtual void setAnimationParamater()
	{
	}

	// Token: 0x06005275 RID: 21109 RVA: 0x0021DB9D File Offset: 0x0021BF9D
	public virtual bool ResetMotionSpeed()
	{
		this.auto.SetSpeed(this.ctrlFlag.speed);
		return true;
	}

	// Token: 0x06005276 RID: 21110 RVA: 0x0021DBB8 File Offset: 0x0021BFB8
	protected void RecoverFaintnessTaii()
	{
		this.ctrlFlag.selectAnimationListInfo = this.ctrlFlag.nowAnimationInfo;
		List<HScene.AnimationListInfo>[] lstAnimInfo = Singleton<Manager.Resources>.Instance.HSceneTable.lstAnimInfo;
		if (lstAnimInfo == null)
		{
			return;
		}
		if (this.ctrlFlag.nowAnimationInfo.nFaintnessLimit == 0)
		{
			return;
		}
		bool flag = this.ctrlFlag.nowAnimationInfo.nInitiativeFemale != 0;
		int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2;
		int i = 0;
		while (i < lstAnimInfo[item].Count)
		{
			HScene.AnimationListInfo animationListInfo = lstAnimInfo[item][i];
			if (!ProcBase.hSceneManager.bMerchant)
			{
				if (animationListInfo.nHentai != 1 || ProcBase.hSceneManager.isHAddTaii[0])
				{
					if (animationListInfo.nHentai != 2 || ProcBase.hSceneManager.isHAddTaii[1])
					{
						if (ProcBase.hSceneManager.isForce)
						{
							if (animationListInfo.nIyaAction == 0)
							{
								goto IL_1F8;
							}
						}
						else if (animationListInfo.nIyaAction == 2)
						{
							goto IL_1F8;
						}
						goto IL_154;
					}
				}
			}
			else if (animationListInfo.bMerchantMotion)
			{
				if (animationListInfo.nIyaAction != 2)
				{
					goto IL_154;
				}
			}
			IL_1F8:
			i++;
			continue;
			IL_154:
			if (item != animationListInfo.ActionCtrl.Item1)
			{
				goto IL_1F8;
			}
			if (item >= 3 && item2 != animationListInfo.ActionCtrl.Item2)
			{
				goto IL_1F8;
			}
			if (!animationListInfo.nPositons.Contains(this.ctrlFlag.nPlace))
			{
				goto IL_1F8;
			}
			if (animationListInfo.nFaintnessLimit != 0)
			{
				goto IL_1F8;
			}
			if (flag && animationListInfo.nInitiativeFemale == 0)
			{
				goto IL_1F8;
			}
			if (!flag && animationListInfo.nInitiativeFemale != 0)
			{
				goto IL_1F8;
			}
			this.ctrlFlag.selectAnimationListInfo = animationListInfo;
			return;
		}
		this.sbWarning.Clear();
		this.sbWarning.Append("脱力からの回復先がない");
	}

	// Token: 0x04004CD5 RID: 19669
	protected HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004CD6 RID: 19670
	protected ChaControl[] chaFemales;

	// Token: 0x04004CD7 RID: 19671
	protected ChaControl[] chaMales;

	// Token: 0x04004CD8 RID: 19672
	protected CrossFade fade;

	// Token: 0x04004CD9 RID: 19673
	protected MetaballCtrl ctrlMeta;

	// Token: 0x04004CDA RID: 19674
	protected HSceneSprite sprite;

	// Token: 0x04004CDB RID: 19675
	protected HItemCtrl item;

	// Token: 0x04004CDC RID: 19676
	protected FeelHit feelHit;

	// Token: 0x04004CDD RID: 19677
	protected HAutoCtrl auto;

	// Token: 0x04004CDE RID: 19678
	protected HVoiceCtrl voice;

	// Token: 0x04004CDF RID: 19679
	protected HParticleCtrl particle;

	// Token: 0x04004CE0 RID: 19680
	protected HSeCtrl se;

	// Token: 0x04004CE1 RID: 19681
	protected float[] timeChangeMotions = new float[2];

	// Token: 0x04004CE2 RID: 19682
	protected float[] timeChangeMotionDeltaTimes = new float[2];

	// Token: 0x04004CE3 RID: 19683
	protected ShuffleRand[] randVoicePlays = new ShuffleRand[2];

	// Token: 0x04004CE4 RID: 19684
	protected bool isHeight1Parameter;

	// Token: 0x04004CE5 RID: 19685
	protected List<Tuple<int, int, MotionIK>> lstMotionIK = new List<Tuple<int, int, MotionIK>>();

	// Token: 0x04004CE6 RID: 19686
	protected ParticleSystem AtariEffect;

	// Token: 0x04004CE7 RID: 19687
	protected ParticleSystem FeelHitEffect3D;

	// Token: 0x04004CE8 RID: 19688
	public static bool endInit;

	// Token: 0x04004CE9 RID: 19689
	protected BoolReactiveProperty isAtariHit = new BoolReactiveProperty(false);

	// Token: 0x04004CEA RID: 19690
	private bool isAtariHitOld;

	// Token: 0x04004CEB RID: 19691
	protected static HSceneManager hSceneManager;

	// Token: 0x04004CEC RID: 19692
	protected int CatID = -1;

	// Token: 0x04004CED RID: 19693
	protected StringBuilder sbWarning = new StringBuilder();

	// Token: 0x04004CEE RID: 19694
	protected HSceneSpriteHitem Hitem;

	// Token: 0x04004CEF RID: 19695
	protected AnimatorStateInfo FemaleAi;

	// Token: 0x02000B02 RID: 2818
	protected struct animParm
	{
		// Token: 0x04004CF0 RID: 19696
		public float[] heights;

		// Token: 0x04004CF1 RID: 19697
		public float breast;

		// Token: 0x04004CF2 RID: 19698
		public float[] m;

		// Token: 0x04004CF3 RID: 19699
		public float speed;
	}
}
