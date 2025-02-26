using System;
using UnityEngine;

// Token: 0x02000371 RID: 881
[AddComponentMenu("Enviro/Utility/Seasons for Meshes")]
public class EnviroMeshSeasons : MonoBehaviour
{
	// Token: 0x06000F9D RID: 3997 RVA: 0x00057950 File Offset: 0x00055D50
	private void Start()
	{
		this.myRenderer = base.GetComponent<MeshRenderer>();
		if (this.myRenderer == null)
		{
			base.enabled = false;
		}
		this.UpdateSeasonMaterial();
		EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
		{
			this.UpdateSeasonMaterial();
		};
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x000579A0 File Offset: 0x00055DA0
	private void OnEnable()
	{
		if (this.SpringMaterial == null)
		{
			base.enabled = false;
		}
		if (this.SummerMaterial == null)
		{
			base.enabled = false;
		}
		if (this.AutumnMaterial == null)
		{
			base.enabled = false;
		}
		if (this.WinterMaterial == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00057A10 File Offset: 0x00055E10
	private void UpdateSeasonMaterial()
	{
		switch (EnviroSky.instance.Seasons.currentSeasons)
		{
		case EnviroSeasons.Seasons.Spring:
			this.myRenderer.sharedMaterial = this.SpringMaterial;
			break;
		case EnviroSeasons.Seasons.Summer:
			this.myRenderer.sharedMaterial = this.SummerMaterial;
			break;
		case EnviroSeasons.Seasons.Autumn:
			this.myRenderer.sharedMaterial = this.AutumnMaterial;
			break;
		case EnviroSeasons.Seasons.Winter:
			this.myRenderer.sharedMaterial = this.WinterMaterial;
			break;
		}
	}

	// Token: 0x04001143 RID: 4419
	public Material SpringMaterial;

	// Token: 0x04001144 RID: 4420
	public Material SummerMaterial;

	// Token: 0x04001145 RID: 4421
	public Material AutumnMaterial;

	// Token: 0x04001146 RID: 4422
	public Material WinterMaterial;

	// Token: 0x04001147 RID: 4423
	private MeshRenderer myRenderer;
}
