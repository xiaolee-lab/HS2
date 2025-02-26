using System;
using UnityEngine;
using UnityEngine.UI;

namespace CTS
{
	// Token: 0x0200068E RID: 1678
	public class CTSFps : MonoBehaviour
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000E73D6 File Offset: 0x000E57D6
		public int FPS
		{
			get
			{
				return (int)this.m_currentFps;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000E73E0 File Offset: 0x000E57E0
		private void Start()
		{
			this.m_fpsNextPeriod = Time.realtimeSinceStartup + 1f;
			try
			{
				this.m_CTSVersion = string.Concat(new object[]
				{
					"CTS v",
					CTSConstants.MajorVersion,
					".",
					CTSConstants.MinorVersion,
					", Unity v",
					Application.unityVersion
				});
				this.m_deviceName = SystemInfo.deviceName;
				this.m_deviceType = SystemInfo.deviceType.ToString();
				this.m_OS = SystemInfo.operatingSystem;
				this.m_platform = Application.platform.ToString();
				this.m_processor = string.Concat(new object[]
				{
					SystemInfo.processorType,
					" - ",
					SystemInfo.processorCount,
					" cores"
				});
				this.m_gpu = SystemInfo.graphicsDeviceName;
				this.m_gpuDevice = SystemInfo.graphicsDeviceType + " - " + SystemInfo.graphicsDeviceVersion;
				this.m_gpuVendor = SystemInfo.graphicsDeviceVendor;
				this.m_gpuRam = SystemInfo.graphicsMemorySize.ToString();
				this.m_gpuCapabilities = this.m_gpuCapabilities + "TA: " + SystemInfo.supports2DArrayTextures.ToString();
				this.m_gpuCapabilities = this.m_gpuCapabilities + ", MT: " + SystemInfo.maxTextureSize.ToString();
				this.m_gpuCapabilities = this.m_gpuCapabilities + ", NPOT: " + SystemInfo.npotSupport.ToString();
				this.m_gpuCapabilities = this.m_gpuCapabilities + ", RTC: " + SystemInfo.supportedRenderTargetCount.ToString();
				this.m_gpuCapabilities = this.m_gpuCapabilities + ", CT: " + SystemInfo.copyTextureSupport.ToString();
				int num = SystemInfo.graphicsShaderLevel;
				if (num >= 10 && num <= 99)
				{
					this.m_gpuSpec = string.Concat(new object[]
					{
						"SM: ",
						num /= 10,
						'.',
						num / 10
					});
				}
				else
				{
					this.m_gpuSpec = "SM: N/A";
				}
				int graphicsMemorySize = SystemInfo.graphicsMemorySize;
				if (graphicsMemorySize > 0)
				{
					string text = this.m_gpuSpec;
					this.m_gpuSpec = string.Concat(new object[]
					{
						text,
						", VRAM: ",
						graphicsMemorySize,
						" MB"
					});
				}
				else
				{
					string text = this.m_gpuSpec;
					this.m_gpuSpec = string.Concat(new object[]
					{
						text,
						", VRAM: ",
						graphicsMemorySize,
						" N/A"
					});
				}
				int systemMemorySize = SystemInfo.systemMemorySize;
				if (systemMemorySize > 0)
				{
					this.m_ram = systemMemorySize.ToString();
				}
				else
				{
					this.m_ram = "N/A";
				}
				Resolution currentResolution = Screen.currentResolution;
				this.m_screenInfo = string.Concat(new object[]
				{
					currentResolution.width,
					"x",
					currentResolution.height,
					" @",
					currentResolution.refreshRate,
					" Hz [window size: ",
					Screen.width,
					"x",
					Screen.height
				});
				float dpi = Screen.dpi;
				if (dpi > 0f)
				{
					string text = this.m_screenInfo;
					this.m_screenInfo = string.Concat(new object[]
					{
						text,
						", DPI: ",
						dpi,
						"]"
					});
				}
				else
				{
					this.m_screenInfo += "]";
				}
				this.m_deviceModel = SystemInfo.deviceModel;
				this.m_quality = QualitySettings.GetQualityLevel().ToString();
			}
			catch (Exception ex)
			{
			}
			if (this.m_CTSVersionText != null)
			{
				this.m_CTSVersionText.text = this.m_CTSVersion;
			}
			if (this.m_OSText != null)
			{
				this.m_OSText.text = this.m_OS;
			}
			if (this.m_deviceText != null)
			{
				this.m_deviceText.text = string.Concat(new string[]
				{
					this.m_deviceName,
					", ",
					this.m_platform,
					", ",
					this.m_deviceType
				});
			}
			if (this.m_systemText != null)
			{
				this.m_systemText.text = string.Concat(new string[]
				{
					this.m_deviceModel,
					", ",
					this.m_processor,
					", ",
					this.m_ram,
					" GB"
				});
			}
			if (this.m_gpuText != null)
			{
				this.m_gpuText.text = string.Concat(new string[]
				{
					this.m_gpu,
					", ",
					this.m_gpuSpec,
					", QUAL: ",
					this.m_quality
				});
			}
			if (this.m_gpuCapabilitiesText != null)
			{
				this.m_gpuCapabilitiesText.text = this.m_gpuDevice + ", " + this.m_gpuCapabilities;
			}
			if (this.m_screenInfoText != null)
			{
				this.m_screenInfoText.text = this.m_screenInfo;
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000E79D0 File Offset: 0x000E5DD0
		private void Update()
		{
			this.m_fpsAccumulator += 1f;
			if (Time.realtimeSinceStartup > this.m_fpsNextPeriod)
			{
				this.m_currentFps = this.m_fpsAccumulator / 1f;
				this.m_currentMs = 1000f / this.m_currentFps;
				this.m_fpsAccumulator = 0f;
				this.m_fpsNextPeriod = Time.realtimeSinceStartup + 1f;
				if (this.m_fpsText != null)
				{
					this.m_fpsText.text = string.Format("FPS {0}, MS {1:0.00}", this.m_currentFps, this.m_currentMs);
				}
			}
		}

		// Token: 0x040027AE RID: 10158
		private const string cFormat = "FPS {0}, MS {1:0.00}";

		// Token: 0x040027AF RID: 10159
		private const float cMeasurePeriod = 1f;

		// Token: 0x040027B0 RID: 10160
		private float m_currentFps;

		// Token: 0x040027B1 RID: 10161
		private float m_currentMs;

		// Token: 0x040027B2 RID: 10162
		private float m_fpsAccumulator;

		// Token: 0x040027B3 RID: 10163
		private float m_fpsNextPeriod;

		// Token: 0x040027B4 RID: 10164
		public string m_CTSVersion;

		// Token: 0x040027B5 RID: 10165
		public string m_OS;

		// Token: 0x040027B6 RID: 10166
		public string m_deviceName;

		// Token: 0x040027B7 RID: 10167
		public string m_deviceType;

		// Token: 0x040027B8 RID: 10168
		public string m_deviceModel;

		// Token: 0x040027B9 RID: 10169
		public string m_platform;

		// Token: 0x040027BA RID: 10170
		public string m_processor;

		// Token: 0x040027BB RID: 10171
		public string m_ram;

		// Token: 0x040027BC RID: 10172
		public string m_gpu;

		// Token: 0x040027BD RID: 10173
		public string m_gpuDevice;

		// Token: 0x040027BE RID: 10174
		public string m_gpuVendor;

		// Token: 0x040027BF RID: 10175
		public string m_gpuSpec;

		// Token: 0x040027C0 RID: 10176
		public string m_gpuRam;

		// Token: 0x040027C1 RID: 10177
		public string m_gpuCapabilities;

		// Token: 0x040027C2 RID: 10178
		public string m_screenInfo;

		// Token: 0x040027C3 RID: 10179
		public string m_quality;

		// Token: 0x040027C4 RID: 10180
		public Text m_fpsText;

		// Token: 0x040027C5 RID: 10181
		public Text m_CTSVersionText;

		// Token: 0x040027C6 RID: 10182
		public Text m_OSText;

		// Token: 0x040027C7 RID: 10183
		public Text m_deviceText;

		// Token: 0x040027C8 RID: 10184
		public Text m_systemText;

		// Token: 0x040027C9 RID: 10185
		public Text m_gpuText;

		// Token: 0x040027CA RID: 10186
		public Text m_gpuCapabilitiesText;

		// Token: 0x040027CB RID: 10187
		public Text m_screenInfoText;
	}
}
