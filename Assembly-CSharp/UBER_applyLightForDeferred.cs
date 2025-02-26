using System;
using UnityEngine;

// Token: 0x02000610 RID: 1552
[AddComponentMenu("UBER/Apply Light for Deferred")]
[ExecuteInEditMode]
public class UBER_applyLightForDeferred : MonoBehaviour
{
	// Token: 0x0600250C RID: 9484 RVA: 0x000D25CD File Offset: 0x000D09CD
	private void Start()
	{
		this.Reset();
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x000D25D8 File Offset: 0x000D09D8
	private void Reset()
	{
		if (base.GetComponent<Light>() && this.lightForSelfShadowing == null)
		{
			this.lightForSelfShadowing = base.GetComponent<Light>();
		}
		if (base.GetComponent<Renderer>() && this._renderer == null)
		{
			this._renderer = base.GetComponent<Renderer>();
		}
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x000D2640 File Offset: 0x000D0A40
	private void Update()
	{
		if (this.lightForSelfShadowing)
		{
			if (this._renderer)
			{
				if (this.lightForSelfShadowing.type == LightType.Directional)
				{
					for (int i = 0; i < this._renderer.sharedMaterials.Length; i++)
					{
						this._renderer.sharedMaterials[i].SetVector("_WorldSpaceLightPosCustom", -this.lightForSelfShadowing.transform.forward);
					}
				}
				else
				{
					for (int j = 0; j < this._renderer.materials.Length; j++)
					{
						this._renderer.sharedMaterials[j].SetVector("_WorldSpaceLightPosCustom", new Vector4(this.lightForSelfShadowing.transform.position.x, this.lightForSelfShadowing.transform.position.y, this.lightForSelfShadowing.transform.position.z, 1f));
					}
				}
			}
			else if (this.lightForSelfShadowing.type == LightType.Directional)
			{
				Shader.SetGlobalVector("_WorldSpaceLightPosCustom", -this.lightForSelfShadowing.transform.forward);
			}
			else
			{
				Shader.SetGlobalVector("_WorldSpaceLightPosCustom", new Vector4(this.lightForSelfShadowing.transform.position.x, this.lightForSelfShadowing.transform.position.y, this.lightForSelfShadowing.transform.position.z, 1f));
			}
		}
	}

	// Token: 0x04002438 RID: 9272
	public Light lightForSelfShadowing;

	// Token: 0x04002439 RID: 9273
	private Renderer _renderer;
}
