using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001263 RID: 4707
	public class PanelComponent : MonoBehaviour
	{
		// Token: 0x06009BCB RID: 39883 RVA: 0x003FB9A4 File Offset: 0x003F9DA4
		public void SetMainTex(Texture2D _texture)
		{
			foreach (Renderer renderer in this.renderer)
			{
				renderer.material.SetTexture(ItemShader._MainTex, _texture);
			}
		}

		// Token: 0x06009BCC RID: 39884 RVA: 0x003FB9E4 File Offset: 0x003F9DE4
		public void UpdateColor(OIItemInfo _info)
		{
			foreach (Renderer renderer in this.renderer)
			{
				renderer.material.SetColor(ItemShader._Color, _info.colors[0].mainColor);
				renderer.material.SetVector(ItemShader._patternuv1, _info.colors[0].pattern.uv);
				renderer.material.SetFloat(ItemShader._patternuv1Rotator, _info.colors[0].pattern.rot);
				renderer.material.SetFloat(ItemShader._patternclamp1, (!_info.colors[0].pattern.clamp) ? 0f : 1f);
			}
		}

		// Token: 0x06009BCD RID: 39885 RVA: 0x003FBAA8 File Offset: 0x003F9EA8
		public void Setup()
		{
			Renderer renderer = this.renderer.SafeGet(0);
			if (renderer == null)
			{
				return;
			}
			Material sharedMaterial = renderer.sharedMaterial;
			this.defColor = sharedMaterial.GetColor("_Color");
			this.defUV = sharedMaterial.GetVector("_patternuv1");
			this.defRot = sharedMaterial.GetFloat("_patternuv1Rotator");
			this.defClamp = !Mathf.Approximately(sharedMaterial.GetFloat("_patternclamp1"), 0f);
		}

		// Token: 0x04007C27 RID: 31783
		public Renderer[] renderer;

		// Token: 0x04007C28 RID: 31784
		public Color defColor = Color.white;

		// Token: 0x04007C29 RID: 31785
		public Vector4 defUV = Vector4.one;

		// Token: 0x04007C2A RID: 31786
		public float defRot;

		// Token: 0x04007C2B RID: 31787
		public bool defClamp = true;
	}
}
