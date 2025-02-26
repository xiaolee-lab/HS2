using System;
using UnityEngine;

// Token: 0x0200117E RID: 4478
public class SimpleFade : MonoBehaviour
{
	// Token: 0x17001F7E RID: 8062
	// (get) Token: 0x060093D0 RID: 37840 RVA: 0x003D1957 File Offset: 0x003CFD57
	public bool IsFadeNow
	{
		get
		{
			return this._Fade == SimpleFade.Fade.In || !this.IsEnd;
		}
	}

	// Token: 0x17001F7F RID: 8063
	// (get) Token: 0x060093D1 RID: 37841 RVA: 0x003D1970 File Offset: 0x003CFD70
	public bool IsEnd
	{
		get
		{
			return this.timer == this._Time && this.fadeInOut == null;
		}
	}

	// Token: 0x060093D2 RID: 37842 RVA: 0x003D198F File Offset: 0x003CFD8F
	public void FadeSet(SimpleFade.Fade fade, float time = -1f, Texture2D tex = null)
	{
		this._Fade = fade;
		if (time != -1f)
		{
			this._Time = time;
		}
		if (tex != null)
		{
			this._Texture = tex;
		}
		this.Init();
	}

	// Token: 0x060093D3 RID: 37843 RVA: 0x003D19C3 File Offset: 0x003CFDC3
	public void FadeInOutSet(SimpleFade.FadeInOut set, Texture2D tex = null)
	{
		if (tex != null)
		{
			this._Texture = tex;
		}
		this.FadeInOutStart(set);
		this.Init();
	}

	// Token: 0x060093D4 RID: 37844 RVA: 0x003D19E8 File Offset: 0x003CFDE8
	public void Init()
	{
		this.timer = 0f;
		this._Color.a = (float)((this._Fade != SimpleFade.Fade.In) ? 1 : 0);
		if (this._Texture == null)
		{
			this._Texture = Texture2D.whiteTexture;
		}
	}

	// Token: 0x060093D5 RID: 37845 RVA: 0x003D1A3A File Offset: 0x003CFE3A
	public void ForceEnd()
	{
		this.timer = this._Time;
		this._Color.a = (float)((this._Fade != SimpleFade.Fade.In) ? 0 : 1);
	}

	// Token: 0x060093D6 RID: 37846 RVA: 0x003D1A68 File Offset: 0x003CFE68
	private void FadeInOutStart(SimpleFade.FadeInOut set)
	{
		if (set != null)
		{
			this.fadeInOut = set;
		}
		if (this.fadeInOut == null)
		{
			return;
		}
		this._Fade = ((set == null) ? SimpleFade.Fade.Out : SimpleFade.Fade.In);
		this._Time = ((set == null) ? this.fadeInOut.outTime : this.fadeInOut.inTime);
		this._Color = ((set == null) ? this.fadeInOut.outColor : this.fadeInOut.inColor);
		this.fadeInOut = set;
		this.Init();
	}

	// Token: 0x060093D7 RID: 37847 RVA: 0x003D1AFC File Offset: 0x003CFEFC
	protected virtual void Awake()
	{
		this.ForceEnd();
	}

	// Token: 0x060093D8 RID: 37848 RVA: 0x003D1B04 File Offset: 0x003CFF04
	protected virtual void Update()
	{
		float a = (float)((this._Fade != SimpleFade.Fade.In) ? 1 : 0);
		float b = (float)((this._Fade != SimpleFade.Fade.In) ? 0 : 1);
		this.timer = Mathf.Min(this.timer + Time.unscaledDeltaTime, this._Time);
		if (this.fadeInOut != null && this.timer == this._Time && this.fadeInOut.Update())
		{
			this.FadeInOutStart(null);
		}
		else
		{
			this._Color.a = Mathf.Lerp(a, b, Mathf.InverseLerp(0f, this._Time, this.timer));
		}
	}

	// Token: 0x060093D9 RID: 37849 RVA: 0x003D1BB8 File Offset: 0x003CFFB8
	protected virtual void OnGUI()
	{
		if (this._Texture == null)
		{
			return;
		}
		GUI.color = this._Color;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this._Texture);
	}

	// Token: 0x04007732 RID: 30514
	public SimpleFade.Fade _Fade;

	// Token: 0x04007733 RID: 30515
	public float _Time = 1f;

	// Token: 0x04007734 RID: 30516
	public Color _Color = Color.white;

	// Token: 0x04007735 RID: 30517
	public Texture2D _Texture;

	// Token: 0x04007736 RID: 30518
	protected float timer;

	// Token: 0x04007737 RID: 30519
	private SimpleFade.FadeInOut fadeInOut;

	// Token: 0x0200117F RID: 4479
	public enum Fade
	{
		// Token: 0x04007739 RID: 30521
		In,
		// Token: 0x0400773A RID: 30522
		Out
	}

	// Token: 0x02001180 RID: 4480
	public class FadeInOut
	{
		// Token: 0x060093DA RID: 37850 RVA: 0x003D1C08 File Offset: 0x003D0008
		public FadeInOut()
		{
		}

		// Token: 0x060093DB RID: 37851 RVA: 0x003D1C48 File Offset: 0x003D0048
		public FadeInOut(SimpleFade fade)
		{
			this.inTime = (this.outTime = fade._Time);
			this.inColor = (this.outColor = fade._Color);
		}

		// Token: 0x060093DC RID: 37852 RVA: 0x003D1CBC File Offset: 0x003D00BC
		public bool Update()
		{
			this.timer = Mathf.Min(this.timer + Time.unscaledDeltaTime, this.waitTime);
			return this.timer == this.waitTime;
		}

		// Token: 0x0400773B RID: 30523
		public float inTime = 1f;

		// Token: 0x0400773C RID: 30524
		public float outTime = 1f;

		// Token: 0x0400773D RID: 30525
		public Color inColor = Color.white;

		// Token: 0x0400773E RID: 30526
		public Color outColor = Color.white;

		// Token: 0x0400773F RID: 30527
		public float waitTime = 1f;

		// Token: 0x04007740 RID: 30528
		private float timer;
	}
}
