using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035F RID: 863
[AddComponentMenu("Enviro/Vegetation Growth Object")]
public class EnviroVegetationInstance : MonoBehaviour
{
	// Token: 0x06000F38 RID: 3896 RVA: 0x000520E8 File Offset: 0x000504E8
	private void Start()
	{
		EnviroSky.instance.RegisterMe(this);
		this.currentSeason = EnviroSky.instance.Seasons.currentSeasons;
		this.maxAgeInHours = EnviroSky.instance.GetInHours(this.Age.maxAgeHours, this.Age.maxAgeDays, this.Age.maxAgeYears);
		EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
		{
			this.SetSeason();
		};
		if (this.Age.randomStartAge)
		{
			this.Age.startAgeinHours = UnityEngine.Random.Range(0f, (float)this.maxAgeInHours);
			this.Age.randomStartAge = false;
		}
		this.Birth(0, this.Age.startAgeinHours);
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000521A8 File Offset: 0x000505A8
	private void OnEnable()
	{
		if (this.GrowStages.Count < 1)
		{
			base.enabled = false;
		}
		for (int i = 0; i < this.GrowStages.Count; i++)
		{
			if (this.GrowStages[i].GrowGameobjectAutumn == null || this.GrowStages[i].GrowGameobjectSpring == null || this.GrowStages[i].GrowGameobjectSummer == null || this.GrowStages[i].GrowGameobjectWinter == null)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00052260 File Offset: 0x00050660
	private void SetSeason()
	{
		this.currentSeason = EnviroSky.instance.Seasons.currentSeasons;
		this.VegetationChange();
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00052280 File Offset: 0x00050680
	public void KeepVariablesClear()
	{
		this.GrowStages[0].minAgePercent = 0f;
		for (int i = 0; i < this.GrowStages.Count; i++)
		{
			if (this.GrowStages[i].minAgePercent > 100f)
			{
				this.GrowStages[i].minAgePercent = 100f;
			}
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000522F0 File Offset: 0x000506F0
	public void UpdateInstance()
	{
		if (this.reBirth)
		{
			this.Birth(0, 0f);
		}
		if (this.shrink)
		{
			this.ShrinkAndDeactivate();
		}
		if (this.canGrow)
		{
			this.UpdateGrowth();
		}
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0005232C File Offset: 0x0005072C
	public void UpdateGrowth()
	{
		this.ageInHours = EnviroSky.instance.currentTimeInHours - this.Age.birthdayInHours;
		this.KeepVariablesClear();
		if (!this.stay)
		{
			if (this.currentStage + 1 < this.GrowStages.Count)
			{
				if (this.maxAgeInHours * (double)(this.GrowStages[this.currentStage + 1].minAgePercent / 100f) <= this.ageInHours && this.ageInHours > 0.0)
				{
					this.currentStage++;
					this.VegetationChange();
				}
				else if (this.GrowStages[this.currentStage].growAction == EnviroVegetationStage.GrowState.Grow)
				{
					this.CalculateScale();
				}
			}
			else if (!this.stay)
			{
				if (this.ageInHours > this.maxAgeInHours)
				{
					if (this.Age.Loop)
					{
						this.currentVegetationObject.SetActive(false);
						if (this.DeadPrefab != null)
						{
							this.DeadPrefabLoop();
						}
						else
						{
							this.Birth(this.Age.LoopFromGrowStage, 0f);
						}
					}
					else
					{
						this.stay = true;
					}
				}
				else if (this.GrowStages[this.currentStage].growAction == EnviroVegetationStage.GrowState.Grow)
				{
					this.CalculateScale();
				}
			}
		}
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x000524A0 File Offset: 0x000508A0
	private void DeadPrefabLoop()
	{
		this.stay = true;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DeadPrefab, base.transform.position, base.transform.rotation);
		gameObject.transform.localScale = this.currentVegetationObject.transform.localScale;
		this.Birth(this.Age.LoopFromGrowStage, 0f);
		this.stay = false;
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00052510 File Offset: 0x00050910
	private IEnumerator BirthColliders()
	{
		Collider[] colliders = this.currentVegetationObject.GetComponentsInChildren<Collider>();
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].enabled = false;
		}
		yield return new WaitForSeconds(10f);
		for (int j = 0; j < colliders.Length; j++)
		{
			colliders[j].enabled = true;
		}
		yield break;
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x0005252C File Offset: 0x0005092C
	private void CalculateScale()
	{
		if (this.rescale)
		{
			this.currentVegetationObject.transform.localScale = this.minScale;
			this.rescale = false;
		}
		double num = this.ageInHours / this.maxAgeInHours * (double)this.GrowSpeedMod;
		this.currentVegetationObject.transform.localScale = this.minScale + new Vector3((float)num, (float)num, (float)num);
		if (this.currentVegetationObject.transform.localScale.y > this.maxScale.y)
		{
			this.currentVegetationObject.transform.localScale = this.maxScale;
		}
		if (this.currentVegetationObject.transform.localScale.y < this.minScale.y)
		{
			this.currentVegetationObject.transform.localScale = this.minScale;
		}
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x0005261C File Offset: 0x00050A1C
	public void Birth(int stage, float startAge)
	{
		this.Age.birthdayInHours = EnviroSky.instance.currentTimeInHours - (double)startAge;
		startAge = 0f;
		this.ageInHours = 0.0;
		this.currentStage = stage;
		this.rescale = true;
		this.reBirth = false;
		this.VegetationChange();
		base.StartCoroutine(this.BirthColliders());
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00052680 File Offset: 0x00050A80
	private void SeasonAction()
	{
		if (this.Seasons.seasonAction == EnviroVegetationSeasons.SeasonAction.SpawnDeadPrefab)
		{
			if (this.DeadPrefab != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DeadPrefab, base.transform.position, base.transform.rotation);
				gameObject.transform.localScale = this.currentVegetationObject.transform.localScale;
			}
			this.currentVegetationObject.SetActive(false);
		}
		else if (this.Seasons.seasonAction == EnviroVegetationSeasons.SeasonAction.Deactivate)
		{
			this.shrink = true;
		}
		else if (this.Seasons.seasonAction == EnviroVegetationSeasons.SeasonAction.Destroy)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00052738 File Offset: 0x00050B38
	private void CheckSeason(bool update)
	{
		if (!update && this.canGrow)
		{
			this.SeasonAction();
			this.canGrow = false;
		}
		else if (update && !this.canGrow)
		{
			this.canGrow = true;
			this.reBirth = true;
		}
		else if (!update && !this.canGrow)
		{
			this.SeasonAction();
		}
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x000527A4 File Offset: 0x00050BA4
	private void ShrinkAndDeactivate()
	{
		if (this.currentVegetationObject.transform.localScale.y > this.minScale.y)
		{
			this.currentVegetationObject.transform.localScale = new Vector3(this.currentVegetationObject.transform.localScale.x - 0.1f * Time.deltaTime, this.currentVegetationObject.transform.localScale.y - 0.1f * Time.deltaTime, this.currentVegetationObject.transform.localScale.z - 0.1f * Time.deltaTime);
		}
		else
		{
			this.shrink = false;
			this.currentVegetationObject.SetActive(false);
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00052874 File Offset: 0x00050C74
	public void VegetationChange()
	{
		this.canGrow = true;
		if (this.currentVegetationObject != null)
		{
			this.currentVegetationObject.SetActive(false);
		}
		switch (this.currentSeason)
		{
		case EnviroSeasons.Seasons.Spring:
			this.currentVegetationObject = this.GrowStages[this.currentStage].GrowGameobjectSpring;
			this.CalculateScale();
			this.currentVegetationObject.SetActive(true);
			if (!this.Seasons.GrowInSpring)
			{
				this.CheckSeason(false);
			}
			else if (this.Seasons.GrowInSpring)
			{
				this.CheckSeason(true);
			}
			break;
		case EnviroSeasons.Seasons.Summer:
			this.currentVegetationObject = this.GrowStages[this.currentStage].GrowGameobjectSummer;
			this.CalculateScale();
			this.currentVegetationObject.SetActive(true);
			if (!this.Seasons.GrowInSummer)
			{
				this.CheckSeason(false);
			}
			else if (this.Seasons.GrowInSummer)
			{
				this.CheckSeason(true);
			}
			break;
		case EnviroSeasons.Seasons.Autumn:
			this.currentVegetationObject = this.GrowStages[this.currentStage].GrowGameobjectAutumn;
			this.CalculateScale();
			this.currentVegetationObject.SetActive(true);
			if (!this.Seasons.GrowInAutumn)
			{
				this.CheckSeason(false);
			}
			else if (this.Seasons.GrowInAutumn)
			{
				this.CheckSeason(true);
			}
			break;
		case EnviroSeasons.Seasons.Winter:
			this.currentVegetationObject = this.GrowStages[this.currentStage].GrowGameobjectWinter;
			this.CalculateScale();
			this.currentVegetationObject.SetActive(true);
			if (!this.Seasons.GrowInWinter)
			{
				this.CheckSeason(false);
			}
			else if (this.Seasons.GrowInWinter)
			{
				this.CheckSeason(true);
			}
			break;
		}
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00052A60 File Offset: 0x00050E60
	private void LateUpdate()
	{
		if (this.GrowStages[this.currentStage].billboard && this.canGrow)
		{
			base.transform.rotation = Camera.main.transform.rotation;
		}
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00052AAD File Offset: 0x00050EAD
	private void OnDrawGizmos()
	{
		Gizmos.color = this.GizmoColor;
		Gizmos.DrawCube(base.transform.position, new Vector3(this.GizmoSize, this.GizmoSize, this.GizmoSize));
	}

	// Token: 0x04001061 RID: 4193
	[HideInInspector]
	public int id;

	// Token: 0x04001062 RID: 4194
	public EnviroVegetationAge Age;

	// Token: 0x04001063 RID: 4195
	public EnviroVegetationSeasons Seasons;

	// Token: 0x04001064 RID: 4196
	public List<EnviroVegetationStage> GrowStages = new List<EnviroVegetationStage>();

	// Token: 0x04001065 RID: 4197
	public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);

	// Token: 0x04001066 RID: 4198
	public Vector3 maxScale = new Vector3(1f, 1f, 1f);

	// Token: 0x04001067 RID: 4199
	public float GrowSpeedMod = 1f;

	// Token: 0x04001068 RID: 4200
	public GameObject DeadPrefab;

	// Token: 0x04001069 RID: 4201
	public Color GizmoColor = new Color(255f, 0f, 0f, 255f);

	// Token: 0x0400106A RID: 4202
	public float GizmoSize = 0.5f;

	// Token: 0x0400106B RID: 4203
	private EnviroSeasons.Seasons currentSeason;

	// Token: 0x0400106C RID: 4204
	private double ageInHours;

	// Token: 0x0400106D RID: 4205
	private double maxAgeInHours;

	// Token: 0x0400106E RID: 4206
	private int currentStage;

	// Token: 0x0400106F RID: 4207
	private GameObject currentVegetationObject;

	// Token: 0x04001070 RID: 4208
	private bool stay;

	// Token: 0x04001071 RID: 4209
	private bool reBirth;

	// Token: 0x04001072 RID: 4210
	private bool rescale = true;

	// Token: 0x04001073 RID: 4211
	private bool canGrow = true;

	// Token: 0x04001074 RID: 4212
	private bool shrink;
}
