using System;
using IllusionUtility.GetUtility;
using MorphAssist;
using UnityEngine;

// Token: 0x0200111F RID: 4383
public class MorphEditorCtrl : MonoBehaviour
{
	// Token: 0x06009120 RID: 37152 RVA: 0x003C6C40 File Offset: 0x003C5040
	private void Awake()
	{
		GameObject gameObject = base.transform.FindTop();
		GameObject gameObject2 = gameObject.transform.FindLoop("MorphCtrl");
		this.morphing = (Morphing)gameObject2.GetComponent("Morphing");
		this.EyebrowCtrl = this.morphing.EyebrowCtrl;
		this.EyesCtrl = this.morphing.EyesCtrl;
		this.MouthCtrl = this.morphing.MouthCtrl;
		this.audioAssist = new AudioAssist();
	}

	// Token: 0x06009121 RID: 37153 RVA: 0x003C6CBE File Offset: 0x003C50BE
	private void Start()
	{
	}

	// Token: 0x06009122 RID: 37154 RVA: 0x003C6CC0 File Offset: 0x003C50C0
	private void Update()
	{
		float audioWaveValue = this.audioAssist.GetAudioWaveValue(base.GetComponent<AudioSource>());
		if (this.clickButton[0, 0])
		{
			int ptn = Mathf.Max(0, this.EyebrowCtrl.NowPtn - 1);
			this.EyebrowCtrl.ChangePtn(ptn, true);
			this.clickButton[0, 0] = false;
		}
		else if (this.clickButton[0, 1])
		{
			int ptn2 = Mathf.Min(this.EyebrowCtrl.GetMaxPtn() - 1, this.EyebrowCtrl.NowPtn + 1);
			this.EyebrowCtrl.ChangePtn(ptn2, true);
			this.clickButton[0, 1] = false;
		}
		if (this.clickButton[1, 0])
		{
			int ptn3 = Mathf.Max(0, this.EyesCtrl.NowPtn - 1);
			this.EyesCtrl.ChangePtn(ptn3, true);
			this.clickButton[1, 0] = false;
		}
		else if (this.clickButton[1, 1])
		{
			int ptn4 = Mathf.Min(this.EyesCtrl.GetMaxPtn() - 1, this.EyesCtrl.NowPtn + 1);
			this.EyesCtrl.ChangePtn(ptn4, true);
			this.clickButton[1, 1] = false;
		}
		if (this.clickButton[2, 0])
		{
			int ptn5 = Mathf.Max(0, this.MouthCtrl.NowPtn - 1);
			this.MouthCtrl.ChangePtn(ptn5, true);
			this.clickButton[2, 0] = false;
		}
		else if (this.clickButton[2, 1])
		{
			int ptn6 = Mathf.Min(this.MouthCtrl.GetMaxPtn() - 1, this.MouthCtrl.NowPtn + 1);
			this.MouthCtrl.ChangePtn(ptn6, true);
			this.clickButton[2, 1] = false;
		}
		this.morphing.SetVoiceVaule(audioWaveValue);
		if (this.clickPlay)
		{
			if (this.playVoice)
			{
				this.playVoice = false;
				base.GetComponent<AudioSource>().Stop();
			}
			else
			{
				this.playVoice = true;
				base.GetComponent<AudioSource>().Play();
			}
			this.clickPlay = false;
		}
	}

	// Token: 0x06009123 RID: 37155 RVA: 0x003C6EF0 File Offset: 0x003C52F0
	private void PushEyebrowBackPtn()
	{
		this.clickButton[0, 0] = true;
	}

	// Token: 0x06009124 RID: 37156 RVA: 0x003C6F00 File Offset: 0x003C5300
	private void PushEyebrowNextPtn()
	{
		this.clickButton[0, 1] = true;
	}

	// Token: 0x06009125 RID: 37157 RVA: 0x003C6F10 File Offset: 0x003C5310
	private void PushEyesBackPtn()
	{
		this.clickButton[1, 0] = true;
	}

	// Token: 0x06009126 RID: 37158 RVA: 0x003C6F20 File Offset: 0x003C5320
	private void PushEyesNextPtn()
	{
		this.clickButton[1, 1] = true;
	}

	// Token: 0x06009127 RID: 37159 RVA: 0x003C6F30 File Offset: 0x003C5330
	private void PushMouthBackPtn()
	{
		this.clickButton[2, 0] = true;
	}

	// Token: 0x06009128 RID: 37160 RVA: 0x003C6F40 File Offset: 0x003C5340
	private void PushMouthNextPtn()
	{
		this.clickButton[2, 1] = true;
	}

	// Token: 0x06009129 RID: 37161 RVA: 0x003C6F50 File Offset: 0x003C5350
	private void PushPlayVoice()
	{
		this.clickPlay = true;
	}

	// Token: 0x040075B9 RID: 30137
	private AudioAssist audioAssist;

	// Token: 0x040075BA RID: 30138
	private bool[,] clickButton = new bool[3, 2];

	// Token: 0x040075BB RID: 30139
	private bool playVoice;

	// Token: 0x040075BC RID: 30140
	private bool clickPlay;

	// Token: 0x040075BD RID: 30141
	private Morphing morphing;

	// Token: 0x040075BE RID: 30142
	private MorphCtrlEyebrow EyebrowCtrl;

	// Token: 0x040075BF RID: 30143
	private MorphCtrlEyes EyesCtrl;

	// Token: 0x040075C0 RID: 30144
	private MorphCtrlMouth MouthCtrl;
}
