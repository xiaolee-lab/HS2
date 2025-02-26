using System;
using UnityEngine;

// Token: 0x0200119E RID: 4510
public class SerialCapture : MonoBehaviour
{
	// Token: 0x0600946C RID: 37996 RVA: 0x003D42CB File Offset: 0x003D26CB
	private void Awake()
	{
		this.captureDirectory = UserData.Create("SerialCapture");
	}

	// Token: 0x0600946D RID: 37997 RVA: 0x003D42DD File Offset: 0x003D26DD
	private void Start()
	{
		if (this.AutoRecord)
		{
			this.StartRecording();
		}
	}

	// Token: 0x0600946E RID: 37998 RVA: 0x003D42F0 File Offset: 0x003D26F0
	public bool GetRecording()
	{
		return this.recording;
	}

	// Token: 0x0600946F RID: 37999 RVA: 0x003D42F8 File Offset: 0x003D26F8
	private void StartRecording()
	{
		this.serialId = DateTime.Now.Minute.ToString("00");
		this.serialId += DateTime.Now.Second.ToString("00");
		this.serialId += "_";
		Time.captureFramerate = 1000 / this.FrameRate;
		this.frameCount = -1;
		this.recording = true;
	}

	// Token: 0x06009470 RID: 38000 RVA: 0x003D4385 File Offset: 0x003D2785
	private void EndRecording()
	{
		Time.captureFramerate = 0;
		this.recording = false;
	}

	// Token: 0x06009471 RID: 38001 RVA: 0x003D4394 File Offset: 0x003D2794
	private void Update()
	{
		if (this.recording)
		{
			if (0 < this.frameCount)
			{
				string filename = string.Concat(new string[]
				{
					this.captureDirectory,
					"/",
					this.serialId,
					this.frameCount.ToString("0000"),
					".png"
				});
				ScreenCapture.CaptureScreenshot(filename, this.SuperSize);
			}
			this.frameCount++;
			if (0 < this.frameCount && this.frameCount % this.FrameRate == 0)
			{
				int num = this.frameCount / this.FrameRate;
				int num2 = num / 60;
				int num3 = num % 60;
			}
			if (this.EndCount != -1 && this.frameCount > this.EndCount)
			{
				this.EndRecording();
			}
		}
	}

	// Token: 0x06009472 RID: 38002 RVA: 0x003D446C File Offset: 0x003D286C
	private void OnGUI()
	{
		if (this.recording)
		{
			if (Input.GetKeyDown(this.ExitKey))
			{
				this.EndRecording();
			}
		}
		else if (GUI.Button(new Rect(this.posCapBtn.x, this.posCapBtn.y, 200f, 30f), "Start SerialCapture"))
		{
			this.StartRecording();
		}
	}

	// Token: 0x0400777A RID: 30586
	public string Name = "1000/FrameRateして整数にならないとダメ";

	// Token: 0x0400777B RID: 30587
	public int FrameRate = 25;

	// Token: 0x0400777C RID: 30588
	public int SuperSize;

	// Token: 0x0400777D RID: 30589
	public int EndCount = -1;

	// Token: 0x0400777E RID: 30590
	public bool AutoRecord;

	// Token: 0x0400777F RID: 30591
	public KeyCode ExitKey = KeyCode.Q;

	// Token: 0x04007780 RID: 30592
	public Vector2 posCapBtn = new Vector2(0f, 0f);

	// Token: 0x04007781 RID: 30593
	private string captureDirectory = string.Empty;

	// Token: 0x04007782 RID: 30594
	private string serialId = string.Empty;

	// Token: 0x04007783 RID: 30595
	private int frameCount = -1;

	// Token: 0x04007784 RID: 30596
	private bool recording;
}
