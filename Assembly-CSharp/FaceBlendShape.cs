using System;
using UniRx;
using UnityEngine;

// Token: 0x020010F8 RID: 4344
public class FaceBlendShape : MonoBehaviour
{
	// Token: 0x06009001 RID: 36865 RVA: 0x003BFFC3 File Offset: 0x003BE3C3
	private void Awake()
	{
		this.EyebrowCtrl.Init();
		this.EyesCtrl.Init();
		this.MouthCtrl.Init();
	}

	// Token: 0x06009002 RID: 36866 RVA: 0x003BFFE8 File Offset: 0x003BE3E8
	public void SetBlinkControlEx(FBSBlinkControl ctrl)
	{
		this.BlinkCtrlEx = ctrl;
	}

	// Token: 0x06009003 RID: 36867 RVA: 0x003BFFF1 File Offset: 0x003BE3F1
	private void Start()
	{
		(from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.OnLateUpdate();
		});
	}

	// Token: 0x06009004 RID: 36868 RVA: 0x003C0028 File Offset: 0x003BE428
	private void OnLateUpdate()
	{
		this.BlinkCtrl.CalcBlink();
		FBSBlinkControl fbsblinkControl = this.BlinkCtrl;
		if (this.BlinkCtrlEx != null)
		{
			fbsblinkControl = this.BlinkCtrlEx;
		}
		float blinkRate;
		if (fbsblinkControl.GetFixedFlags() == 0)
		{
			blinkRate = fbsblinkControl.GetOpenRate();
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
				if (angleHRate > 0f)
				{
					num2 -= MathfEx.LerpAccel(0f, this.EyeLookSideCorrect, angleHRate);
				}
				else
				{
					num2 -= MathfEx.LerpAccel(0f, this.EyeLookSideCorrect, -angleHRate);
				}
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

	// Token: 0x06009005 RID: 36869 RVA: 0x003C01D2 File Offset: 0x003BE5D2
	public void SetVoiceVaule(float value)
	{
		this.voiceValue = value;
	}

	// Token: 0x040074CF RID: 29903
	private FBSBlinkControl BlinkCtrlEx;

	// Token: 0x040074D0 RID: 29904
	public FBSBlinkControl BlinkCtrl;

	// Token: 0x040074D1 RID: 29905
	public FBSCtrlEyebrow EyebrowCtrl;

	// Token: 0x040074D2 RID: 29906
	public FBSCtrlEyes EyesCtrl;

	// Token: 0x040074D3 RID: 29907
	public FBSCtrlMouth MouthCtrl;

	// Token: 0x040074D4 RID: 29908
	private float voiceValue;

	// Token: 0x040074D5 RID: 29909
	public EyeLookController EyeLookController;

	// Token: 0x040074D6 RID: 29910
	[Range(0f, 1f)]
	public float EyeLookUpCorrect = 0.1f;

	// Token: 0x040074D7 RID: 29911
	[Range(0f, 1f)]
	public float EyeLookDownCorrect = 0.3f;

	// Token: 0x040074D8 RID: 29912
	[Range(0f, 1f)]
	public float EyeLookSideCorrect = 0.1f;
}
