using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003E3 RID: 995
	[RequireComponent(typeof(Camera))]
	public class LuxWater_UnderWaterBlur : MonoBehaviour
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x0006813A File Offset: 0x0006653A
		private void OnEnable()
		{
			this.blurMaterial = new Material(Shader.Find("Hidden/Lux Water/BlurEffectConeTap"));
			this.blitMaterial = new Material(Shader.Find("Hidden/Lux Water/UnderWaterPost"));
			base.Invoke("GetWaterrendermanagerInstance", 0f);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00068176 File Offset: 0x00066576
		private void OnDisable()
		{
			if (this.blurMaterial)
			{
				UnityEngine.Object.DestroyImmediate(this.blurMaterial);
			}
			if (this.blitMaterial)
			{
				UnityEngine.Object.DestroyImmediate(this.blitMaterial);
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000681AE File Offset: 0x000665AE
		private void GetWaterrendermanagerInstance()
		{
			this.waterrendermanager = LuxWater_UnderWaterRendering.instance;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000681BC File Offset: 0x000665BC
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (this.waterrendermanager == null)
			{
				Graphics.Blit(src, dest);
				return;
			}
			this.doBlur = (this.waterrendermanager.activeWaterVolume > -1);
			if (this.doBlur || this.initBlur)
			{
				this.initBlur = false;
				int width = src.width / this.blurDownSample;
				int height = src.height / this.blurDownSample;
				RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.DefaultHDR);
				this.DownSample(src, renderTexture);
				for (int i = 0; i < this.blurIterations; i++)
				{
					RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.DefaultHDR);
					this.FourTapCone(renderTexture, temporary, i);
					RenderTexture.ReleaseTemporary(renderTexture);
					renderTexture = temporary;
				}
				Shader.SetGlobalTexture("_UnderWaterTex", renderTexture);
				Graphics.Blit(src, dest, this.blitMaterial, 1);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			else
			{
				Graphics.Blit(src, dest);
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000682B0 File Offset: 0x000666B0
		private void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
		{
			float num = 0.5f + (float)iteration * this.blurSpread;
			this.m_offsets[0].x = -num;
			this.m_offsets[0].y = -num;
			this.m_offsets[1].x = -num;
			this.m_offsets[1].y = num;
			this.m_offsets[2].x = num;
			this.m_offsets[2].y = num;
			this.m_offsets[3].x = num;
			this.m_offsets[3].y = -num;
			if (iteration == 0)
			{
				Graphics.BlitMultiTap(source, dest, this.blurMaterial, this.m_offsets);
			}
			else
			{
				Graphics.BlitMultiTap(source, dest, this.blurMaterial, this.m_offsets);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00068394 File Offset: 0x00066794
		private void DownSample(RenderTexture source, RenderTexture dest)
		{
			float num = 1f;
			this.m_offsets[0].x = -num;
			this.m_offsets[0].y = -num;
			this.m_offsets[1].x = -num;
			this.m_offsets[1].y = num;
			this.m_offsets[2].x = num;
			this.m_offsets[2].y = num;
			this.m_offsets[3].x = num;
			this.m_offsets[3].y = -num;
			Graphics.BlitMultiTap(source, dest, this.blurMaterial, this.m_offsets);
		}

		// Token: 0x04001391 RID: 5009
		[Space(6f)]
		[LuxWater_HelpBtn("h.3a2840a53u5j")]
		public float blurSpread = 0.6f;

		// Token: 0x04001392 RID: 5010
		public int blurDownSample = 4;

		// Token: 0x04001393 RID: 5011
		public int blurIterations = 4;

		// Token: 0x04001394 RID: 5012
		private Vector2[] m_offsets = new Vector2[4];

		// Token: 0x04001395 RID: 5013
		private Material blurMaterial;

		// Token: 0x04001396 RID: 5014
		private Material blitMaterial;

		// Token: 0x04001397 RID: 5015
		private LuxWater_UnderWaterRendering waterrendermanager;

		// Token: 0x04001398 RID: 5016
		private bool doBlur;

		// Token: 0x04001399 RID: 5017
		private bool initBlur = true;
	}
}
