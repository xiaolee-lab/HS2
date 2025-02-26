using System;
using UnityEngine;

// Token: 0x020010B6 RID: 4278
public class BaseRenderCrossFade : MonoBehaviour
{
	// Token: 0x17001EF7 RID: 7927
	// (get) Token: 0x06008EBE RID: 36542 RVA: 0x003B64CF File Offset: 0x003B48CF
	// (set) Token: 0x06008EBF RID: 36543 RVA: 0x003B64D7 File Offset: 0x003B48D7
	public bool isDrawGUI { get; protected set; }

	// Token: 0x17001EF8 RID: 7928
	// (get) Token: 0x06008EC0 RID: 36544 RVA: 0x003B64E0 File Offset: 0x003B48E0
	// (set) Token: 0x06008EC1 RID: 36545 RVA: 0x003B64E8 File Offset: 0x003B48E8
	public float alpha { get; protected set; }

	// Token: 0x17001EF9 RID: 7929
	// (set) Token: 0x06008EC2 RID: 36546 RVA: 0x003B64F1 File Offset: 0x003B48F1
	public float MaxTime
	{
		set
		{
			this.maxTime = value;
			this.timer = 0f;
		}
	}

	// Token: 0x06008EC3 RID: 36547 RVA: 0x003B6508 File Offset: 0x003B4908
	protected void AlphaCalc()
	{
		this.alpha = Mathf.InverseLerp(0f, this.maxTime, this.timer);
		if (!this.isAlphaAdd)
		{
			this.alpha = Mathf.Lerp(1f, 0f, this.alpha);
		}
	}

	// Token: 0x06008EC4 RID: 36548 RVA: 0x003B6558 File Offset: 0x003B4958
	public void Capture()
	{
		if (this.texture != null && (this.texture.width != Screen.width || this.texture.height != Screen.height))
		{
			this.CreateRenderTexture();
		}
		if (!this.isInitRenderSetting)
		{
			this.RenderTargetSetting();
		}
		if (this.targetCamera != null)
		{
			RenderTexture targetTexture = this.targetCamera.targetTexture;
			Rect rect = this.targetCamera.rect;
			this.targetCamera.targetTexture = this.texture;
			this.targetCamera.Render();
			this.targetCamera.targetTexture = targetTexture;
			this.targetCamera.rect = rect;
		}
		if (this.uiCamera != null)
		{
			RenderTexture targetTexture = this.uiCamera.targetTexture;
			Rect rect = this.uiCamera.rect;
			this.uiCamera.targetTexture = this.texture;
			this.uiCamera.Render();
			this.uiCamera.targetTexture = targetTexture;
			this.uiCamera.rect = rect;
		}
		this.timer = 0f;
		this.isDrawGUI = false;
		this.AlphaCalc();
	}

	// Token: 0x06008EC5 RID: 36549 RVA: 0x003B668B File Offset: 0x003B4A8B
	public virtual void End()
	{
		this.timer = this.maxTime;
		this.AlphaCalc();
	}

	// Token: 0x06008EC6 RID: 36550 RVA: 0x003B669F File Offset: 0x003B4A9F
	public void Destroy()
	{
		this.ReleaseRenderTexture();
	}

	// Token: 0x06008EC7 RID: 36551 RVA: 0x003B66A7 File Offset: 0x003B4AA7
	protected virtual void Awake()
	{
		this.CreateRenderTexture();
		this.RenderTargetSetting();
		this.isDrawGUI = false;
	}

	// Token: 0x06008EC8 RID: 36552 RVA: 0x003B66BC File Offset: 0x003B4ABC
	protected virtual void Update()
	{
		this.timer += Time.deltaTime;
		this.timer = Mathf.Min(this.timer, this.maxTime);
		this.AlphaCalc();
	}

	// Token: 0x06008EC9 RID: 36553 RVA: 0x003B66F0 File Offset: 0x003B4AF0
	protected virtual void OnGUI()
	{
		GUI.depth = 10;
		GUI.color = new Color(1f, 1f, 1f, this.alpha);
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.texture);
		this.isDrawGUI = true;
	}

	// Token: 0x06008ECA RID: 36554 RVA: 0x003B6750 File Offset: 0x003B4B50
	private void CreateRenderTexture()
	{
		this.ReleaseRenderTexture();
		this.texture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.RGB565);
		this.texture.antiAliasing = ((QualitySettings.antiAliasing != 0) ? QualitySettings.antiAliasing : 1);
		this.texture.enableRandomWrite = false;
	}

	// Token: 0x06008ECB RID: 36555 RVA: 0x003B67A7 File Offset: 0x003B4BA7
	private void ReleaseRenderTexture()
	{
		if (this.texture != null)
		{
			this.texture.Release();
			UnityEngine.Object.Destroy(this.texture);
			this.texture = null;
		}
	}

	// Token: 0x06008ECC RID: 36556 RVA: 0x003B67D8 File Offset: 0x003B4BD8
	private void RenderTargetSetting()
	{
		if (this.uiCamera == null)
		{
			GameObject gameObject = GameObject.Find("SpDef");
			if (gameObject)
			{
				this.uiCamera = gameObject.GetComponent<Camera>();
			}
		}
		if (this.targetCamera == null)
		{
			this.targetCamera = Camera.main;
		}
	}

	// Token: 0x0400736A RID: 29546
	public Camera uiCamera;

	// Token: 0x0400736B RID: 29547
	public Camera targetCamera;

	// Token: 0x0400736E RID: 29550
	protected float maxTime = 1f;

	// Token: 0x0400736F RID: 29551
	protected float timer;

	// Token: 0x04007370 RID: 29552
	protected bool isAlphaAdd = true;

	// Token: 0x04007371 RID: 29553
	public RenderTexture texture;

	// Token: 0x04007372 RID: 29554
	protected bool isInitRenderSetting = true;
}
