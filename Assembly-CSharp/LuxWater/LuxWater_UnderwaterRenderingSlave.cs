using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003E5 RID: 997
	[RequireComponent(typeof(Camera))]
	public class LuxWater_UnderwaterRenderingSlave : MonoBehaviour
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x00069E62 File Offset: 0x00068262
		private void OnEnable()
		{
			this.cam = base.GetComponent<Camera>();
			base.Invoke("GetWaterrendermanager", 0f);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00069E80 File Offset: 0x00068280
		private void GetWaterrendermanager()
		{
			LuxWater_UnderWaterRendering instance = LuxWater_UnderWaterRendering.instance;
			if (instance != null)
			{
				this.waterrendermanager = instance;
				this.readyToGo = true;
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00069EAD File Offset: 0x000682AD
		private void OnPreCull()
		{
			if (this.readyToGo)
			{
				this.waterrendermanager.RenderWaterMask(this.cam, true);
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00069ECC File Offset: 0x000682CC
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (this.readyToGo)
			{
				this.waterrendermanager.RenderUnderWater(src, dest, this.cam, true);
			}
			else
			{
				Graphics.Blit(src, dest);
			}
		}

		// Token: 0x040013F4 RID: 5108
		private LuxWater_UnderWaterRendering waterrendermanager;

		// Token: 0x040013F5 RID: 5109
		private bool readyToGo;

		// Token: 0x040013F6 RID: 5110
		public Camera cam;
	}
}
