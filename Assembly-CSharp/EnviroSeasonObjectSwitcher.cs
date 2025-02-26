using System;
using UnityEngine;

// Token: 0x02000373 RID: 883
[AddComponentMenu("Enviro/Utility/Seasons for GameObjects")]
public class EnviroSeasonObjectSwitcher : MonoBehaviour
{
	// Token: 0x06000FA6 RID: 4006 RVA: 0x00057B5E File Offset: 0x00055F5E
	private void Start()
	{
		this.SwitchSeasonObject();
		EnviroSky.instance.OnSeasonChanged += delegate(EnviroSeasons.Seasons season)
		{
			this.SwitchSeasonObject();
		};
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x00057B7C File Offset: 0x00055F7C
	private void OnEnable()
	{
		if (this.SpringObject == null)
		{
			base.enabled = false;
		}
		if (this.SummerObject == null)
		{
			base.enabled = false;
		}
		if (this.AutumnObject == null)
		{
			base.enabled = false;
		}
		if (this.WinterObject == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00057BEC File Offset: 0x00055FEC
	private void SwitchSeasonObject()
	{
		switch (EnviroSky.instance.Seasons.currentSeasons)
		{
		case EnviroSeasons.Seasons.Spring:
			this.SummerObject.SetActive(false);
			this.AutumnObject.SetActive(false);
			this.WinterObject.SetActive(false);
			this.SpringObject.SetActive(true);
			break;
		case EnviroSeasons.Seasons.Summer:
			this.SpringObject.SetActive(false);
			this.AutumnObject.SetActive(false);
			this.WinterObject.SetActive(false);
			this.SummerObject.SetActive(true);
			break;
		case EnviroSeasons.Seasons.Autumn:
			this.SpringObject.SetActive(false);
			this.SummerObject.SetActive(false);
			this.WinterObject.SetActive(false);
			this.AutumnObject.SetActive(true);
			break;
		case EnviroSeasons.Seasons.Winter:
			this.SpringObject.SetActive(false);
			this.SummerObject.SetActive(false);
			this.AutumnObject.SetActive(false);
			this.WinterObject.SetActive(true);
			break;
		}
	}

	// Token: 0x0400114B RID: 4427
	public GameObject SpringObject;

	// Token: 0x0400114C RID: 4428
	public GameObject SummerObject;

	// Token: 0x0400114D RID: 4429
	public GameObject AutumnObject;

	// Token: 0x0400114E RID: 4430
	public GameObject WinterObject;
}
