using System;
using Illusion.CustomAttributes;
using UnityEngine;

// Token: 0x0200086D RID: 2157
public class CrossFade : MonoBehaviour
{
	// Token: 0x170009CD RID: 2509
	// (get) Token: 0x060036FC RID: 14076 RVA: 0x00145DF8 File Offset: 0x001441F8
	// (set) Token: 0x060036FD RID: 14077 RVA: 0x00145E00 File Offset: 0x00144200
	public bool isEnd { get; private set; }

	// Token: 0x060036FE RID: 14078 RVA: 0x00145E0C File Offset: 0x0014420C
	public void FadeStart(float time = -1f)
	{
		if (this.texBase == null)
		{
			return;
		}
		this.timeCalc = ((time >= 0f) ? time : this.time);
		if (Mathf.Approximately(this.timeCalc, 0f))
		{
			return;
		}
		this.timer = 0f;
		this.alpha = 0f;
		this.isProcess = true;
		this.isEnd = false;
	}

	// Token: 0x060036FF RID: 14079 RVA: 0x00145E82 File Offset: 0x00144282
	public void End()
	{
		this.timer = this.timeCalc;
		this.isEnd = true;
		this.alpha = 1f;
	}

	// Token: 0x06003700 RID: 14080 RVA: 0x00145EA2 File Offset: 0x001442A2
	private void OnDestroy()
	{
		if (this.texBase != null)
		{
			this.texBase.Release();
		}
	}

	// Token: 0x06003701 RID: 14081 RVA: 0x00145EC0 File Offset: 0x001442C0
	private void Start()
	{
		this._FadeTex = Shader.PropertyToID("_FadeTex");
		this._Alpha = Shader.PropertyToID("_Alpha");
		this.isProcess = false;
		this.isEnd = true;
		this.texBase = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
	}

	// Token: 0x06003702 RID: 14082 RVA: 0x00145F14 File Offset: 0x00144314
	private void Update()
	{
		if (!this.isProcess)
		{
			return;
		}
		this.timer = Mathf.Clamp(this.timer + Time.smoothDeltaTime, 0f, this.timeCalc);
		this.isEnd = (this.timer >= this.timeCalc);
		if (this.isEnd)
		{
			this.alpha = 1f;
		}
		else
		{
			this.alpha = this.timer / this.timeCalc;
		}
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x00145F94 File Offset: 0x00144394
	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (this.texBase == null)
		{
			Graphics.Blit(src, dst);
			return;
		}
		if (!this.isProcess)
		{
			Graphics.Blit(src, this.texBase);
			Graphics.Blit(src, dst);
			return;
		}
		this.materiaEffect.SetTexture(this._FadeTex, this.texBase);
		this.materiaEffect.SetFloat(this._Alpha, this.alpha);
		Graphics.Blit(src, dst, this.materiaEffect);
		this.isProcess = !this.isEnd;
	}

	// Token: 0x04003782 RID: 14210
	[global::Label("CrossFadeマテリアル")]
	public Material materiaEffect;

	// Token: 0x04003783 RID: 14211
	[global::Label("フェード時間")]
	public float time = 0.3f;

	// Token: 0x04003784 RID: 14212
	[Header("Debug表示")]
	[SerializeField]
	private RenderTexture texBase;

	// Token: 0x04003785 RID: 14213
	[SerializeField]
	[Range(0f, 1f)]
	private float alpha;

	// Token: 0x04003786 RID: 14214
	[SerializeField]
	[NotEditable]
	private float timer;

	// Token: 0x04003787 RID: 14215
	private float timeCalc;

	// Token: 0x04003788 RID: 14216
	private int _FadeTex;

	// Token: 0x04003789 RID: 14217
	private int _Alpha;

	// Token: 0x0400378A RID: 14218
	private bool isProcess;

	// Token: 0x0400378B RID: 14219
	[global::Button("FadeStart", "FadeStart", new object[]
	{
		-1
	})]
	public int FadeStartButton;
}
