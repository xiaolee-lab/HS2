using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000FC0 RID: 4032
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MiniMapDepthTexture : MonoBehaviour
{
	// Token: 0x17001D44 RID: 7492
	// (get) Token: 0x0600861C RID: 34332 RVA: 0x00381872 File Offset: 0x0037FC72
	public float outlineThick
	{
		[CompilerGenerated]
		get
		{
			return this._outlineThick;
		}
	}

	// Token: 0x0600861D RID: 34333 RVA: 0x0038187A File Offset: 0x0037FC7A
	private void Awake()
	{
	}

	// Token: 0x0600861E RID: 34334 RVA: 0x0038187C File Offset: 0x0037FC7C
	private void Update()
	{
	}

	// Token: 0x0600861F RID: 34335 RVA: 0x00381880 File Offset: 0x0037FC80
	public void Initialize()
	{
		Camera component = base.GetComponent<Camera>();
		RenderTexture targetTexture = component.targetTexture;
		component.depthTextureMode |= DepthTextureMode.Depth;
		if (component.allowMSAA || component.allowHDR)
		{
			return;
		}
		this._material = new Material(this._shader);
		this.SetMaterialProperties();
		CommandBuffer commandBuffer = new CommandBuffer();
		int nameID = Shader.PropertyToID("_PostEffectTempTexture");
		commandBuffer.GetTemporaryRT(nameID, -1, -1);
		commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, nameID);
		commandBuffer.Blit(nameID, targetTexture, this._material);
		commandBuffer.ReleaseTemporaryRT(nameID);
		component.AddCommandBuffer(CameraEvent.AfterEverything, commandBuffer);
	}

	// Token: 0x06008620 RID: 34336 RVA: 0x0038192C File Offset: 0x0037FD2C
	private void SetMaterialProperties()
	{
		if (this._material != null)
		{
			this._material.SetFloat("_OutlineThreshold", this._outlineThreshold);
			this._material.SetColor("_OutlineColor", this._outlineColor);
			this._material.SetFloat("_OutlineThick", this._outlineThick);
		}
	}

	// Token: 0x04006D33 RID: 27955
	[SerializeField]
	private Shader _shader;

	// Token: 0x04006D34 RID: 27956
	[SerializeField]
	private float _outlineThreshold = 0.01f;

	// Token: 0x04006D35 RID: 27957
	[SerializeField]
	private Color _outlineColor = Color.white;

	// Token: 0x04006D36 RID: 27958
	[SerializeField]
	private float _outlineThick = 1f;

	// Token: 0x04006D37 RID: 27959
	private Material _material;
}
