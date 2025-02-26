using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000FAE RID: 4014
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AllAreaCameraEffector : MonoBehaviour
{
	// Token: 0x060085AC RID: 34220 RVA: 0x00379B54 File Offset: 0x00377F54
	private void Awake()
	{
	}

	// Token: 0x060085AD RID: 34221 RVA: 0x00379B56 File Offset: 0x00377F56
	private void Update()
	{
	}

	// Token: 0x060085AE RID: 34222 RVA: 0x00379B58 File Offset: 0x00377F58
	public void Initialize()
	{
		Camera component = base.GetComponent<Camera>();
		this.renderTexture = component.targetTexture;
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
		commandBuffer.Blit(this.renderTexture, nameID);
		commandBuffer.Blit(nameID, this.renderTexture, this._material);
		commandBuffer.ReleaseTemporaryRT(nameID);
		component.AddCommandBuffer(CameraEvent.AfterEverything, commandBuffer);
	}

	// Token: 0x060085AF RID: 34223 RVA: 0x00379C10 File Offset: 0x00378010
	private void SetMaterialProperties()
	{
		if (this._material != null)
		{
			this._material.SetTexture("_MainTex", this.renderTexture);
			this._material.SetFloat("_Alpha", this.alpha);
			this._material.SetFloat("_OutlineThreshold", this._outlineThreshold);
			this._material.SetFloat("_OutlineThick", this.depthTexture.outlineThick);
			this._material.SetColor("_Color", this.BGColor);
		}
	}

	// Token: 0x04006C5D RID: 27741
	[SerializeField]
	private Shader _shader;

	// Token: 0x04006C5E RID: 27742
	[SerializeField]
	[Range(0f, 1f)]
	private float alpha;

	// Token: 0x04006C5F RID: 27743
	[SerializeField]
	private Color BGColor;

	// Token: 0x04006C60 RID: 27744
	[SerializeField]
	private float _outlineThreshold = 0.01f;

	// Token: 0x04006C61 RID: 27745
	[SerializeField]
	private MiniMapDepthTexture depthTexture;

	// Token: 0x04006C62 RID: 27746
	private Material _material;

	// Token: 0x04006C63 RID: 27747
	private RenderTexture renderTexture;
}
