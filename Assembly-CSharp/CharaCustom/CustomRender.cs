using System;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009D2 RID: 2514
	public class CustomRender : MonoBehaviour
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x001C167D File Offset: 0x001BFA7D
		// (set) Token: 0x060049A8 RID: 18856 RVA: 0x001C1685 File Offset: 0x001BFA85
		public RenderTexture rtCamera { get; private set; }

		// Token: 0x060049A9 RID: 18857 RVA: 0x001C168E File Offset: 0x001BFA8E
		private void Start()
		{
			this.rtCamera = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x001C16A9 File Offset: 0x001BFAA9
		public RenderTexture GetRenderTexture()
		{
			return this.rtCamera;
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x001C16B4 File Offset: 0x001BFAB4
		private void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (this.update)
			{
				if (this.rtCamera == null)
				{
					Graphics.Blit(src, dst);
				}
				else
				{
					Graphics.Blit(src, this.rtCamera);
					Graphics.Blit(src, dst);
				}
			}
			else
			{
				Graphics.Blit(this.rtCamera, dst);
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x001C170D File Offset: 0x001BFB0D
		private void OnDestroy()
		{
			if (this.rtCamera)
			{
				this.rtCamera.Release();
				this.rtCamera = null;
			}
		}

		// Token: 0x04004448 RID: 17480
		public bool update = true;
	}
}
