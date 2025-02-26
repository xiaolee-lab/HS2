using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000436 RID: 1078
[ExecuteInEditMode]
public class PSMeshRendererUpdater : MonoBehaviour
{
	// Token: 0x060013B5 RID: 5045 RVA: 0x0007A0B9 File Offset: 0x000784B9
	private void Update()
	{
		if (Application.isPlaying)
		{
			this.CheckFading();
		}
		if (this.Color != this.oldColor)
		{
			this.oldColor = this.Color;
			this.UpdateColor(this.Color);
		}
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x0007A0FC File Offset: 0x000784FC
	public void CheckFading()
	{
		if (this.currentActiveStatus != this.IsActive)
		{
			this.currentActiveStatus = this.IsActive;
			this.needUpdateAlpha = true;
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in componentsInChildren)
			{
				if (this.currentActiveStatus)
				{
					particleSystem.Clear();
					particleSystem.Play();
				}
				else
				{
					particleSystem.Stop();
				}
			}
			ME_TrailRendererNoise[] componentsInChildren2 = base.GetComponentsInChildren<ME_TrailRendererNoise>();
			foreach (ME_TrailRendererNoise me_TrailRendererNoise in componentsInChildren2)
			{
				me_TrailRendererNoise.IsActive = this.currentActiveStatus;
			}
		}
		if (this.needUpdateAlpha)
		{
			if (this.currentActiveStatus)
			{
				this.currentAlphaTime += Time.deltaTime;
			}
			else
			{
				this.currentAlphaTime -= Time.deltaTime;
			}
			if (this.currentAlphaTime < 0f || this.currentAlphaTime > this.FadeTime)
			{
				this.needUpdateAlpha = false;
			}
			this.SetAlpha(Mathf.Clamp01(this.currentAlphaTime / this.FadeTime));
		}
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x0007A228 File Offset: 0x00078628
	public void SetAlpha(float alpha)
	{
		if (this.MeshObject == null)
		{
			return;
		}
		Light componentInChildren = this.MeshObject.GetComponentInChildren<Light>();
		if (componentInChildren != null)
		{
			componentInChildren.intensity = alpha;
		}
		MeshRenderer componentInChildren2 = this.MeshObject.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren2 != null)
		{
			Material[] materials = componentInChildren2.materials;
			foreach (Material material in materials)
			{
				if (material.name.Contains("MeshEffect"))
				{
					this.UpdateAlphaByPropertyName(material, "_TintColor", alpha);
					this.UpdateAlphaByPropertyName(material, "_MainColor", alpha);
				}
			}
		}
		SkinnedMeshRenderer componentInChildren3 = this.MeshObject.GetComponentInChildren<SkinnedMeshRenderer>();
		if (componentInChildren3 != null)
		{
			Material[] materials2 = componentInChildren3.materials;
			foreach (Material material2 in materials2)
			{
				if (material2.name.Contains("MeshEffect"))
				{
					this.UpdateAlphaByPropertyName(material2, "_TintColor", alpha);
					this.UpdateAlphaByPropertyName(material2, "_MainColor", alpha);
				}
			}
		}
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x0007A350 File Offset: 0x00078750
	private void UpdateAlphaByPropertyName(Material mat, string name, float alpha)
	{
		if (mat.HasProperty(name))
		{
			Color color = mat.GetColor(name);
			color.a = alpha;
			mat.SetColor(name, color);
		}
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x0007A384 File Offset: 0x00078784
	public void UpdateColor(Color color)
	{
		if (this.MeshObject == null)
		{
			return;
		}
		ME_ColorHelper.HSBColor hsbcolor = ME_ColorHelper.ColorToHSV(color);
		ME_ColorHelper.ChangeObjectColorByHUE(this.MeshObject, hsbcolor.H);
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x0007A3BC File Offset: 0x000787BC
	public void UpdateColor(float HUE)
	{
		if (this.MeshObject == null)
		{
			return;
		}
		ME_ColorHelper.ChangeObjectColorByHUE(this.MeshObject, HUE);
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x0007A3DC File Offset: 0x000787DC
	public void UpdateMeshEffect()
	{
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = default(Quaternion);
		this.rendererMaterials.Clear();
		this.skinnedMaterials.Clear();
		if (this.MeshObject == null)
		{
			return;
		}
		this.UpdatePSMesh(this.MeshObject);
		this.AddMaterialToMesh(this.MeshObject);
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x0007A44D File Offset: 0x0007884D
	private void CheckScaleIncludedParticles()
	{
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x0007A450 File Offset: 0x00078850
	public void UpdateMeshEffect(GameObject go)
	{
		this.rendererMaterials.Clear();
		this.skinnedMaterials.Clear();
		if (go == null)
		{
			return;
		}
		this.MeshObject = go;
		this.UpdatePSMesh(this.MeshObject);
		this.AddMaterialToMesh(this.MeshObject);
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x0007A4A0 File Offset: 0x000788A0
	private void UpdatePSMesh(GameObject go)
	{
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		MeshRenderer componentInChildren = go.GetComponentInChildren<MeshRenderer>();
		SkinnedMeshRenderer componentInChildren2 = go.GetComponentInChildren<SkinnedMeshRenderer>();
		Light[] componentsInChildren2 = base.GetComponentsInChildren<Light>();
		float num = 1f;
		if (componentInChildren != null)
		{
			num = componentInChildren.bounds.size.magnitude;
		}
		if (componentInChildren2 != null)
		{
			num = componentInChildren2.bounds.size.magnitude;
		}
		float magnitude = go.transform.lossyScale.magnitude;
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			particleSystem.transform.gameObject.SetActive(false);
			ParticleSystem.ShapeModule shape = particleSystem.shape;
			if (shape.enabled)
			{
				if (componentInChildren != null)
				{
					shape.shapeType = ParticleSystemShapeType.MeshRenderer;
					shape.meshRenderer = componentInChildren;
				}
				if (componentInChildren2 != null)
				{
					shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
					shape.skinnedMeshRenderer = componentInChildren2;
				}
			}
			ParticleSystem.MainModule mainModule;
			particleSystem.main.startSizeMultiplier = mainModule.startSizeMultiplier * (num / magnitude);
			particleSystem.transform.gameObject.SetActive(true);
		}
		if (componentInChildren != null)
		{
			foreach (Light light in componentsInChildren2)
			{
				light.transform.position = componentInChildren.bounds.center;
			}
		}
		if (componentInChildren2 != null)
		{
			foreach (Light light2 in componentsInChildren2)
			{
				light2.transform.position = componentInChildren2.bounds.center;
			}
		}
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0007A680 File Offset: 0x00078A80
	private void AddMaterialToMesh(GameObject go)
	{
		ME_MeshMaterialEffect componentInChildren = base.GetComponentInChildren<ME_MeshMaterialEffect>();
		if (componentInChildren == null)
		{
			return;
		}
		MeshRenderer componentInChildren2 = go.GetComponentInChildren<MeshRenderer>();
		SkinnedMeshRenderer componentInChildren3 = go.GetComponentInChildren<SkinnedMeshRenderer>();
		if (componentInChildren2 != null)
		{
			this.rendererMaterials.Add(componentInChildren2.sharedMaterials);
			componentInChildren2.sharedMaterials = this.AddToSharedMaterial(componentInChildren2.sharedMaterials, componentInChildren);
		}
		if (componentInChildren3 != null)
		{
			this.skinnedMaterials.Add(componentInChildren3.sharedMaterials);
			componentInChildren3.sharedMaterials = this.AddToSharedMaterial(componentInChildren3.sharedMaterials, componentInChildren);
		}
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x0007A710 File Offset: 0x00078B10
	private Material[] AddToSharedMaterial(Material[] sharedMaterials, ME_MeshMaterialEffect meshMatEffect)
	{
		if (meshMatEffect.IsFirstMaterial)
		{
			return new Material[]
			{
				meshMatEffect.Material
			};
		}
		List<Material> list = sharedMaterials.ToList<Material>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].name.Contains("MeshEffect"))
			{
				list.RemoveAt(i);
			}
		}
		list.Add(meshMatEffect.Material);
		return list.ToArray();
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x0007A78C File Offset: 0x00078B8C
	private void OnDestroy()
	{
		if (this.MeshObject == null)
		{
			return;
		}
		MeshRenderer[] componentsInChildren = this.MeshObject.GetComponentsInChildren<MeshRenderer>();
		SkinnedMeshRenderer[] componentsInChildren2 = this.MeshObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (this.rendererMaterials.Count == componentsInChildren.Length)
			{
				componentsInChildren[i].sharedMaterials = this.rendererMaterials[i];
			}
			List<Material> list = componentsInChildren[i].sharedMaterials.ToList<Material>();
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].name.Contains("MeshEffect"))
				{
					list.RemoveAt(j);
				}
			}
			componentsInChildren[i].sharedMaterials = list.ToArray();
		}
		for (int k = 0; k < componentsInChildren2.Length; k++)
		{
			if (this.skinnedMaterials.Count == componentsInChildren2.Length)
			{
				componentsInChildren2[k].sharedMaterials = this.skinnedMaterials[k];
			}
			List<Material> list2 = componentsInChildren2[k].sharedMaterials.ToList<Material>();
			for (int l = 0; l < list2.Count; l++)
			{
				if (list2[l].name.Contains("MeshEffect"))
				{
					list2.RemoveAt(l);
				}
			}
			componentsInChildren2[k].sharedMaterials = list2.ToArray();
		}
		this.rendererMaterials.Clear();
		this.skinnedMaterials.Clear();
	}

	// Token: 0x04001622 RID: 5666
	public GameObject MeshObject;

	// Token: 0x04001623 RID: 5667
	public Color Color = Color.black;

	// Token: 0x04001624 RID: 5668
	private const string materialName = "MeshEffect";

	// Token: 0x04001625 RID: 5669
	private List<Material[]> rendererMaterials = new List<Material[]>();

	// Token: 0x04001626 RID: 5670
	private List<Material[]> skinnedMaterials = new List<Material[]>();

	// Token: 0x04001627 RID: 5671
	public bool IsActive = true;

	// Token: 0x04001628 RID: 5672
	public float FadeTime = 1.5f;

	// Token: 0x04001629 RID: 5673
	private bool currentActiveStatus;

	// Token: 0x0400162A RID: 5674
	private bool needUpdateAlpha;

	// Token: 0x0400162B RID: 5675
	private Color oldColor = Color.black;

	// Token: 0x0400162C RID: 5676
	private float currentAlphaTime;
}
