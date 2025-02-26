using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace LuxWater
{
	// Token: 0x020003D7 RID: 983
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class LuxWater_CameraDepthMode : MonoBehaviour
	{
		// Token: 0x06001171 RID: 4465 RVA: 0x000669D4 File Offset: 0x00064DD4
		private void OnEnable()
		{
			this.cam = base.GetComponent<Camera>();
			this.cam.depthTextureMode |= DepthTextureMode.Depth;
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal)
			{
				Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(this.OnPrecull));
				this.CamCallBackAdded = true;
				this.CopyDepthMat = new Material(Shader.Find("Hidden/Lux Water/CopyDepth"));
				this.format = RenderTextureFormat.RFloat;
				if (!SystemInfo.SupportsRenderTextureFormat(this.format))
				{
					this.format = RenderTextureFormat.RHalf;
				}
				if (!SystemInfo.SupportsRenderTextureFormat(this.format))
				{
					this.format = RenderTextureFormat.ARGBHalf;
				}
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00066A80 File Offset: 0x00064E80
		private void OnDisable()
		{
			if (this.CamCallBackAdded)
			{
				Camera.onPreCull = (Camera.CameraCallback)Delegate.Remove(Camera.onPreCull, new Camera.CameraCallback(this.OnPrecull));
				foreach (KeyValuePair<Camera, CommandBuffer> keyValuePair in this.m_cmdBuffer)
				{
					if (keyValuePair.Key != null)
					{
						keyValuePair.Key.RemoveCommandBuffer(CameraEvent.AfterLighting, keyValuePair.Value);
					}
				}
				this.m_cmdBuffer.Clear();
			}
			this.ShowShaderWarning = true;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00066B38 File Offset: 0x00064F38
		private void OnPrecull(Camera camera)
		{
			if (this.GrabDepthTexture)
			{
				RenderingPath actualRenderingPath = this.cam.actualRenderingPath;
				if (actualRenderingPath == RenderingPath.DeferredShading && SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal)
				{
					CommandBuffer commandBuffer;
					if (!this.m_cmdBuffer.TryGetValue(camera, out commandBuffer))
					{
						commandBuffer = new CommandBuffer();
						commandBuffer.name = "Lux Water Grab Depth";
						camera.AddCommandBuffer(CameraEvent.BeforeSkybox, commandBuffer);
						this.m_cmdBuffer[camera] = commandBuffer;
					}
					commandBuffer.Clear();
					int pixelWidth = camera.pixelWidth;
					int pixelHeight = camera.pixelHeight;
					int nameID = Shader.PropertyToID("_Lux_GrabbedDepth");
					commandBuffer.GetTemporaryRT(nameID, pixelWidth, pixelHeight, 0, FilterMode.Point, this.format, RenderTextureReadWrite.Linear);
					commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, nameID, this.CopyDepthMat, 0);
					commandBuffer.ReleaseTemporaryRT(nameID);
				}
				else
				{
					this.GrabDepthTexture = false;
					foreach (KeyValuePair<Camera, CommandBuffer> keyValuePair in this.m_cmdBuffer)
					{
						if (keyValuePair.Key != null)
						{
							keyValuePair.Key.RemoveCommandBuffer(CameraEvent.AfterLighting, keyValuePair.Value);
						}
					}
					this.m_cmdBuffer.Clear();
					this.ShowShaderWarning = true;
				}
			}
		}

		// Token: 0x04001344 RID: 4932
		public bool GrabDepthTexture;

		// Token: 0x04001345 RID: 4933
		private Camera cam;

		// Token: 0x04001346 RID: 4934
		private Material CopyDepthMat;

		// Token: 0x04001347 RID: 4935
		private RenderTextureFormat format;

		// Token: 0x04001348 RID: 4936
		private Dictionary<Camera, CommandBuffer> m_cmdBuffer = new Dictionary<Camera, CommandBuffer>();

		// Token: 0x04001349 RID: 4937
		private bool CamCallBackAdded;

		// Token: 0x0400134A RID: 4938
		[HideInInspector]
		public bool ShowShaderWarning = true;
	}
}
