using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000377 RID: 887
[AddComponentMenu("Enviro/Utility/Seasons for Terrains")]
public class EnviroTerrainSeasons : MonoBehaviour
{
	// Token: 0x06000FB0 RID: 4016 RVA: 0x00057EC8 File Offset: 0x000562C8
	private void Start()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
		this.texturesIn = this.terrain.terrainData.splatPrototypes;
		this.UpdateSeason();
		EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
		{
			this.UpdateSeason();
		};
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x00057F24 File Offset: 0x00056324
	private void OnEnable()
	{
		if (this.ChangeTextures)
		{
			for (int i = 0; i < this.TextureChanges.Count; i++)
			{
				if (this.TextureChanges[i].SpringTexture == null)
				{
					base.enabled = false;
				}
				if (this.TextureChanges[i].SummerTexture == null)
				{
					base.enabled = false;
				}
				if (this.TextureChanges[i].AutumnTexture == null)
				{
					base.enabled = false;
				}
				if (this.TextureChanges[i].WinterTexture == null)
				{
					base.enabled = false;
				}
				if (this.TextureChanges[i].terrainTextureID < 0)
				{
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00058002 File Offset: 0x00056402
	private void ChangeGrassColor(Color ChangeToColor)
	{
		this.terrain.terrainData.wavingGrassTint = ChangeToColor;
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00058015 File Offset: 0x00056415
	private void ChangeTexture(Texture2D inTexture, int id, Vector2 tiling)
	{
		this.textureInSplats = this.texturesIn;
		this.textureInSplats[id].texture = inTexture;
		this.textureInSplats[id].tileSize = tiling;
		this.terrain.terrainData.splatPrototypes = this.textureInSplats;
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00058058 File Offset: 0x00056458
	private void ChangeTexture(Texture2D inTexture, Texture2D inNormal, int id, Vector2 tiling)
	{
		this.textureInSplats = this.texturesIn;
		this.textureInSplats[id].texture = inTexture;
		this.textureInSplats[id].normalMap = inNormal;
		this.textureInSplats[id].tileSize = tiling;
		this.terrain.terrainData.splatPrototypes = this.textureInSplats;
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x000580B4 File Offset: 0x000564B4
	private void UpdateSeason()
	{
		switch (EnviroSky.instance.Seasons.currentSeasons)
		{
		case EnviroSeasons.Seasons.Spring:
			for (int i = 0; i < this.TextureChanges.Count; i++)
			{
				if (this.ChangeTextures)
				{
					if (this.TextureChanges[i].SpringNormal != null)
					{
						this.ChangeTexture(this.TextureChanges[i].SpringTexture, this.TextureChanges[i].SpringNormal, this.TextureChanges[i].terrainTextureID, this.TextureChanges[i].tiling);
					}
					else
					{
						this.ChangeTexture(this.TextureChanges[i].SpringTexture, this.TextureChanges[i].terrainTextureID, this.TextureChanges[i].tiling);
					}
					this.terrain.Flush();
				}
			}
			if (this.ChangeGrassTint)
			{
				this.ChangeGrassColor(this.SpringGrassColor);
			}
			break;
		case EnviroSeasons.Seasons.Summer:
			for (int j = 0; j < this.TextureChanges.Count; j++)
			{
				if (this.ChangeTextures)
				{
					if (this.TextureChanges[j].SummerNormal != null)
					{
						this.ChangeTexture(this.TextureChanges[j].SummerTexture, this.TextureChanges[j].SummerNormal, this.TextureChanges[j].terrainTextureID, this.TextureChanges[j].tiling);
					}
					else
					{
						this.ChangeTexture(this.TextureChanges[j].SummerTexture, this.TextureChanges[j].terrainTextureID, this.TextureChanges[j].tiling);
					}
					this.terrain.Flush();
				}
			}
			if (this.ChangeGrassTint)
			{
				this.ChangeGrassColor(this.SummerGrassColor);
			}
			break;
		case EnviroSeasons.Seasons.Autumn:
			for (int k = 0; k < this.TextureChanges.Count; k++)
			{
				if (this.ChangeTextures)
				{
					if (this.TextureChanges[k].AutumnNormal != null)
					{
						this.ChangeTexture(this.TextureChanges[k].AutumnTexture, this.TextureChanges[k].AutumnNormal, this.TextureChanges[k].terrainTextureID, this.TextureChanges[k].tiling);
					}
					else
					{
						this.ChangeTexture(this.TextureChanges[k].AutumnTexture, this.TextureChanges[k].terrainTextureID, this.TextureChanges[k].tiling);
					}
					this.terrain.Flush();
				}
			}
			if (this.ChangeGrassTint)
			{
				this.ChangeGrassColor(this.AutumnGrassColor);
			}
			break;
		case EnviroSeasons.Seasons.Winter:
			for (int l = 0; l < this.TextureChanges.Count; l++)
			{
				if (this.ChangeTextures)
				{
					if (this.TextureChanges[l].WinterNormal != null)
					{
						this.ChangeTexture(this.TextureChanges[l].WinterTexture, this.TextureChanges[l].WinterNormal, this.TextureChanges[l].terrainTextureID, this.TextureChanges[l].tiling);
					}
					else
					{
						this.ChangeTexture(this.TextureChanges[l].WinterTexture, this.TextureChanges[l].terrainTextureID, this.TextureChanges[l].tiling);
					}
					this.terrain.Flush();
				}
			}
			if (this.ChangeGrassTint)
			{
				this.ChangeGrassColor(this.WinterGrassColor);
			}
			break;
		}
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x000584C0 File Offset: 0x000568C0
	private void Update()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this.ChangeGrassWind && EnviroSky.instance.Weather.currentActiveWeatherPreset != null)
		{
			this.terrain.terrainData.wavingGrassStrength = EnviroSky.instance.Weather.currentActiveWeatherPreset.WindStrenght * this.windSpeedModificator;
			this.terrain.terrainData.wavingGrassSpeed = EnviroSky.instance.Weather.currentActiveWeatherPreset.WindStrenght * this.windSizeModificator;
		}
	}

	// Token: 0x0400115A RID: 4442
	public Terrain terrain;

	// Token: 0x0400115B RID: 4443
	[Header("Terrain Textures")]
	public bool ChangeTextures = true;

	// Token: 0x0400115C RID: 4444
	public List<EnviroTerrainSeasonsChangeOrder> TextureChanges = new List<EnviroTerrainSeasonsChangeOrder>();

	// Token: 0x0400115D RID: 4445
	[Header("Grass Tint")]
	public bool ChangeGrassTint = true;

	// Token: 0x0400115E RID: 4446
	public Color SpringGrassColor = Color.white;

	// Token: 0x0400115F RID: 4447
	public Color SummerGrassColor = Color.white;

	// Token: 0x04001160 RID: 4448
	public Color AutumnGrassColor = Color.white;

	// Token: 0x04001161 RID: 4449
	public Color WinterGrassColor = Color.white;

	// Token: 0x04001162 RID: 4450
	[Header("Grass Wind")]
	public bool ChangeGrassWind = true;

	// Token: 0x04001163 RID: 4451
	public float windSpeedModificator = 5f;

	// Token: 0x04001164 RID: 4452
	public float windSizeModificator = 5f;

	// Token: 0x04001165 RID: 4453
	private SplatPrototype[] textureInSplats = new SplatPrototype[1];

	// Token: 0x04001166 RID: 4454
	private SplatPrototype[] texturesIn;
}
