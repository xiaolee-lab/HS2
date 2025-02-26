using System;
using UnityEngine;

// Token: 0x0200119F RID: 4511
public class SerialCaptureEx
{
	// Token: 0x06009473 RID: 38003 RVA: 0x003D44DC File Offset: 0x003D28DC
	public SerialCaptureEx()
	{
		this.captureDirectory = UserData.Create("SerialCapture");
	}

	// Token: 0x06009474 RID: 38004 RVA: 0x003D4538 File Offset: 0x003D2938
	public void StartRecording()
	{
		this.serialId = DateTime.Now.Minute.ToString("00");
		this.serialId += DateTime.Now.Second.ToString("00");
		this.serialId += "_";
		Time.captureFramerate = 1000 / this.FrameRate;
		this.frameCount = -1;
		this.recording = true;
	}

	// Token: 0x06009475 RID: 38005 RVA: 0x003D45C5 File Offset: 0x003D29C5
	public void EndRecording()
	{
		Time.captureFramerate = 0;
		this.recording = false;
	}

	// Token: 0x06009476 RID: 38006 RVA: 0x003D45D4 File Offset: 0x003D29D4
	public void Update()
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

	// Token: 0x04007785 RID: 30597
	public string Name = "1000/FrameRateして整数にならないとダメ";

	// Token: 0x04007786 RID: 30598
	public int FrameRate = 25;

	// Token: 0x04007787 RID: 30599
	public int SuperSize;

	// Token: 0x04007788 RID: 30600
	public int EndCount = -1;

	// Token: 0x04007789 RID: 30601
	private string captureDirectory = string.Empty;

	// Token: 0x0400778A RID: 30602
	private string serialId = string.Empty;

	// Token: 0x0400778B RID: 30603
	private int frameCount = -1;

	// Token: 0x0400778C RID: 30604
	private bool recording;
}
