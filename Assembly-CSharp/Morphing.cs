using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200111E RID: 4382
public class Morphing : MonoBehaviour
{
	// Token: 0x0600911B RID: 37147 RVA: 0x003C6A6C File Offset: 0x003C4E6C
	private void Awake()
	{
		List<MorphingTargetInfo> list = new List<MorphingTargetInfo>();
		list.Clear();
		this.EyebrowCtrl.Init(list);
		this.EyesCtrl.Init(list);
		this.MouthCtrl.Init(list);
	}

	// Token: 0x0600911C RID: 37148 RVA: 0x003C6AAB File Offset: 0x003C4EAB
	private void Start()
	{
	}

	// Token: 0x0600911D RID: 37149 RVA: 0x003C6AB0 File Offset: 0x003C4EB0
	private void LateUpdate()
	{
		this.BlinkCtrl.CalcBlink();
		float blinkRate;
		if (this.BlinkCtrl.GetFixedFlags() == 0)
		{
			blinkRate = this.BlinkCtrl.GetOpenRate();
			if (this.EyeLookController)
			{
				float angleHRate = this.EyeLookController.eyeLookScript.GetAngleHRate(EYE_LR.EYE_L);
				float angleVRate = this.EyeLookController.eyeLookScript.GetAngleVRate();
				float min = -Mathf.Max(this.EyeLookDownCorrect, this.EyeLookSideCorrect);
				float num = 1f - this.EyeLookUpCorrect;
				if (num > this.EyesCtrl.OpenMax)
				{
					num = this.EyesCtrl.OpenMax;
				}
				float num2;
				if (angleVRate > 0f)
				{
					num2 = MathfEx.LerpAccel(0f, this.EyeLookUpCorrect, angleVRate);
				}
				else
				{
					num2 = -MathfEx.LerpAccel(0f, this.EyeLookDownCorrect, -angleVRate);
				}
				num2 -= MathfEx.LerpAccel(0f, this.EyeLookSideCorrect, angleHRate);
				num2 = Mathf.Clamp(num2, min, this.EyeLookUpCorrect);
				num2 *= 1f - (1f - this.EyesCtrl.OpenMax);
				this.EyesCtrl.SetCorrectOpenMax(num + num2);
			}
		}
		else
		{
			blinkRate = -1f;
		}
		this.EyebrowCtrl.CalcBlend(blinkRate);
		this.EyesCtrl.CalcBlend(blinkRate);
		this.MouthCtrl.CalcBlend(this.voiceValue);
	}

	// Token: 0x0600911E RID: 37150 RVA: 0x003C6C22 File Offset: 0x003C5022
	public void SetVoiceVaule(float value)
	{
		this.voiceValue = value;
	}

	// Token: 0x040075B0 RID: 30128
	public MorphBlinkControl BlinkCtrl;

	// Token: 0x040075B1 RID: 30129
	public MorphCtrlEyebrow EyebrowCtrl;

	// Token: 0x040075B2 RID: 30130
	public MorphCtrlEyes EyesCtrl;

	// Token: 0x040075B3 RID: 30131
	public MorphCtrlMouth MouthCtrl;

	// Token: 0x040075B4 RID: 30132
	private float voiceValue;

	// Token: 0x040075B5 RID: 30133
	public EyeLookController EyeLookController;

	// Token: 0x040075B6 RID: 30134
	[Range(0f, 1f)]
	public float EyeLookUpCorrect = 0.1f;

	// Token: 0x040075B7 RID: 30135
	[Range(0f, 1f)]
	public float EyeLookDownCorrect = 0.3f;

	// Token: 0x040075B8 RID: 30136
	[Range(0f, 1f)]
	public float EyeLookSideCorrect = 0.1f;
}
