using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200069E RID: 1694
	public class CTSWeatherController : MonoBehaviour
	{
		// Token: 0x0600281E RID: 10270 RVA: 0x000EE563 File Offset: 0x000EC963
		private void OnEnable()
		{
			CTSSingleton<CTSTerrainManager>.Instance.RegisterWeatherController(this);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000EE570 File Offset: 0x000EC970
		private void OnDisable()
		{
			CTSSingleton<CTSTerrainManager>.Instance.UnregisterWeatherController(this);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000EE580 File Offset: 0x000EC980
		public void ProcessWeatherUpdate(CTSWeatherManager manager)
		{
			if (this.m_terrain == null)
			{
				this.m_terrain = base.GetComponent<Terrain>();
				if (this.m_terrain == null)
				{
					return;
				}
			}
			if (this.m_terrain.materialType != Terrain.MaterialType.Custom)
			{
				return;
			}
			Material materialTemplate = this.m_terrain.materialTemplate;
			if (materialTemplate == null)
			{
				return;
			}
			materialTemplate.SetFloat(CTSShaderID.Snow_Amount, manager.SnowPower * 2f);
			materialTemplate.SetFloat(CTSShaderID.Snow_Min_Height, manager.SnowMinHeight);
			float num = manager.RainPower * manager.MaxRainSmoothness;
			materialTemplate.SetFloat(CTSShaderID.Snow_Smoothness, num);
			Color color = Color.white;
			if (manager.Season < 1f)
			{
				color = Color.Lerp(manager.WinterTint, manager.SpringTint, manager.Season);
			}
			else if (manager.Season < 2f)
			{
				color = Color.Lerp(manager.SpringTint, manager.SummerTint, manager.Season - 1f);
			}
			else if (manager.Season < 3f)
			{
				color = Color.Lerp(manager.SummerTint, manager.AutumnTint, manager.Season - 2f);
			}
			else
			{
				color = Color.Lerp(manager.AutumnTint, manager.WinterTint, manager.Season - 3f);
			}
			for (int i = 0; i < 16; i++)
			{
				materialTemplate.SetVector(CTSShaderID.Texture_X_Color[i], new Vector4(color.r, color.g, color.b, num));
			}
		}

		// Token: 0x0400292B RID: 10539
		private Terrain m_terrain;
	}
}
