using System;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

// Token: 0x02000ACD RID: 2765
public class DeliveryMember
{
	// Token: 0x04004A80 RID: 19072
	public HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004A81 RID: 19073
	public ChaControl[] chaFemales;

	// Token: 0x04004A82 RID: 19074
	public ChaControl[] chaMales;

	// Token: 0x04004A83 RID: 19075
	public CrossFade fade;

	// Token: 0x04004A84 RID: 19076
	public MetaballCtrl ctrlMeta;

	// Token: 0x04004A85 RID: 19077
	public HSceneSprite sprite;

	// Token: 0x04004A86 RID: 19078
	public HItemCtrl item;

	// Token: 0x04004A87 RID: 19079
	public FeelHit feelHit;

	// Token: 0x04004A88 RID: 19080
	public HAutoCtrl auto;

	// Token: 0x04004A89 RID: 19081
	public HVoiceCtrl voice;

	// Token: 0x04004A8A RID: 19082
	public HParticleCtrl particle;

	// Token: 0x04004A8B RID: 19083
	public ParticleSystem AtariEffect;

	// Token: 0x04004A8C RID: 19084
	public ParticleSystem FeelHitEffect3D;

	// Token: 0x04004A8D RID: 19085
	public HSeCtrl se;

	// Token: 0x04004A8E RID: 19086
	public List<Tuple<int, int, MotionIK>> lstMotionIK;
}
