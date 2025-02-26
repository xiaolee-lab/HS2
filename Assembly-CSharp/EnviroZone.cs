using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000366 RID: 870
[AddComponentMenu("Enviro/Weather Zone")]
public class EnviroZone : MonoBehaviour
{
	// Token: 0x06000F64 RID: 3940 RVA: 0x00054270 File Offset: 0x00052670
	private void Start()
	{
		if (this.zoneWeatherPresets.Count > 0)
		{
			this.zoneCollider = base.gameObject.AddComponent<BoxCollider>();
			this.zoneCollider.isTrigger = true;
			if (!base.GetComponent<EnviroSky>())
			{
				EnviroSky.instance.RegisterZone(this);
			}
			else
			{
				this.isDefault = true;
			}
			this.UpdateZoneScale();
			this.nextUpdate = EnviroSky.instance.currentTimeInHours + (double)this.WeatherUpdateIntervall;
			this.nextUpdateRealtime = Time.time + this.WeatherUpdateIntervall * 60f;
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x00054310 File Offset: 0x00052710
	public void UpdateZoneScale()
	{
		if (!this.isDefault)
		{
			this.zoneCollider.size = this.zoneScale;
		}
		else
		{
			this.zoneCollider.size = Vector3.one * (1f / EnviroSky.instance.transform.localScale.y) * 0.25f;
		}
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0005437C File Offset: 0x0005277C
	public void CreateZoneWeatherTypeList()
	{
		for (int i = 0; i < this.zoneWeatherPresets.Count; i++)
		{
			if (this.zoneWeatherPresets[i] == null)
			{
				return;
			}
			bool flag = true;
			for (int j = 0; j < EnviroSky.instance.Weather.weatherPresets.Count; j++)
			{
				if (this.zoneWeatherPresets[i] == EnviroSky.instance.Weather.weatherPresets[j])
				{
					flag = false;
					this.zoneWeather.Add(EnviroSky.instance.Weather.WeatherPrefabs[j]);
				}
			}
			if (EnviroSky.instance.Weather.VFXHolder == null)
			{
				flag = false;
			}
			if (flag)
			{
				GameObject gameObject = new GameObject();
				EnviroWeatherPrefab enviroWeatherPrefab = gameObject.AddComponent<EnviroWeatherPrefab>();
				enviroWeatherPrefab.weatherPreset = this.zoneWeatherPresets[i];
				gameObject.name = enviroWeatherPrefab.weatherPreset.Name;
				enviroWeatherPrefab.effectEmmisionRates.Clear();
				gameObject.transform.parent = EnviroSky.instance.Weather.VFXHolder.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				this.zoneWeather.Add(enviroWeatherPrefab);
				EnviroSky.instance.Weather.WeatherPrefabs.Add(enviroWeatherPrefab);
				EnviroSky.instance.Weather.weatherPresets.Add(this.zoneWeatherPresets[i]);
			}
		}
		for (int k = 0; k < this.zoneWeather.Count; k++)
		{
			for (int l = 0; l < this.zoneWeather[k].effectSystems.Count; l++)
			{
				this.zoneWeather[k].effectEmmisionRates.Add(EnviroSky.GetEmissionRate(this.zoneWeather[k].effectSystems[l]));
				EnviroSky.SetEmissionRate(this.zoneWeather[k].effectSystems[l], 0f);
			}
		}
		if (this.isDefault && EnviroSky.instance.Weather.startWeatherPreset != null)
		{
			EnviroSky.instance.SetWeatherOverwrite(EnviroSky.instance.Weather.startWeatherPreset);
			for (int m = 0; m < this.zoneWeather.Count; m++)
			{
				if (this.zoneWeather[m].weatherPreset == EnviroSky.instance.Weather.startWeatherPreset)
				{
					this.currentActiveZoneWeatherPrefab = this.zoneWeather[m];
					this.lastActiveZoneWeatherPrefab = this.zoneWeather[m];
				}
			}
			this.currentActiveZoneWeatherPreset = EnviroSky.instance.Weather.startWeatherPreset;
			this.lastActiveZoneWeatherPreset = EnviroSky.instance.Weather.startWeatherPreset;
		}
		else
		{
			this.currentActiveZoneWeatherPrefab = this.zoneWeather[0];
			this.lastActiveZoneWeatherPrefab = this.zoneWeather[0];
			this.currentActiveZoneWeatherPreset = this.zoneWeatherPresets[0];
			this.lastActiveZoneWeatherPreset = this.zoneWeatherPresets[0];
		}
		this.nextUpdate = EnviroSky.instance.currentTimeInHours + (double)this.WeatherUpdateIntervall;
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x000546F8 File Offset: 0x00052AF8
	private void BuildNewWeatherList()
	{
		this.curPossibleZoneWeather = new List<EnviroWeatherPrefab>();
		for (int i = 0; i < this.zoneWeather.Count; i++)
		{
			switch (EnviroSky.instance.Seasons.currentSeasons)
			{
			case EnviroSeasons.Seasons.Spring:
				if (this.zoneWeather[i].weatherPreset.Spring)
				{
					this.curPossibleZoneWeather.Add(this.zoneWeather[i]);
				}
				break;
			case EnviroSeasons.Seasons.Summer:
				if (this.zoneWeather[i].weatherPreset.Summer)
				{
					this.curPossibleZoneWeather.Add(this.zoneWeather[i]);
				}
				break;
			case EnviroSeasons.Seasons.Autumn:
				if (this.zoneWeather[i].weatherPreset.Autumn)
				{
					this.curPossibleZoneWeather.Add(this.zoneWeather[i]);
				}
				break;
			case EnviroSeasons.Seasons.Winter:
				if (this.zoneWeather[i].weatherPreset.winter)
				{
					this.curPossibleZoneWeather.Add(this.zoneWeather[i]);
				}
				break;
			}
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x00054834 File Offset: 0x00052C34
	private EnviroWeatherPrefab PossibiltyCheck()
	{
		List<EnviroWeatherPrefab> list = new List<EnviroWeatherPrefab>();
		for (int i = 0; i < this.curPossibleZoneWeather.Count; i++)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (EnviroSky.instance.Seasons.currentSeasons == EnviroSeasons.Seasons.Spring)
			{
				if ((float)num <= this.curPossibleZoneWeather[i].weatherPreset.possibiltyInSpring)
				{
					list.Add(this.curPossibleZoneWeather[i]);
				}
			}
			else if (EnviroSky.instance.Seasons.currentSeasons == EnviroSeasons.Seasons.Summer)
			{
				if ((float)num <= this.curPossibleZoneWeather[i].weatherPreset.possibiltyInSummer)
				{
					list.Add(this.curPossibleZoneWeather[i]);
				}
			}
			else if (EnviroSky.instance.Seasons.currentSeasons == EnviroSeasons.Seasons.Autumn)
			{
				if ((float)num <= this.curPossibleZoneWeather[i].weatherPreset.possibiltyInAutumn)
				{
					list.Add(this.curPossibleZoneWeather[i]);
				}
			}
			else if (EnviroSky.instance.Seasons.currentSeasons == EnviroSeasons.Seasons.Winter && (float)num <= this.curPossibleZoneWeather[i].weatherPreset.possibiltyInWinter)
			{
				list.Add(this.curPossibleZoneWeather[i]);
			}
		}
		if (list.Count > 0)
		{
			EnviroSky.instance.NotifyZoneWeatherChanged(list[0].weatherPreset, this);
			return list[0];
		}
		return this.currentActiveZoneWeatherPrefab;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x000549BC File Offset: 0x00052DBC
	private void WeatherUpdate()
	{
		this.nextUpdate = EnviroSky.instance.currentTimeInHours + (double)this.WeatherUpdateIntervall;
		this.nextUpdateRealtime = Time.time + this.WeatherUpdateIntervall * 60f;
		this.BuildNewWeatherList();
		this.lastActiveZoneWeatherPrefab = this.currentActiveZoneWeatherPrefab;
		this.lastActiveZoneWeatherPreset = this.currentActiveZoneWeatherPreset;
		this.currentActiveZoneWeatherPrefab = this.PossibiltyCheck();
		this.currentActiveZoneWeatherPreset = this.currentActiveZoneWeatherPrefab.weatherPreset;
		EnviroSky.instance.NotifyZoneWeatherChanged(this.currentActiveZoneWeatherPreset, this);
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x00054A48 File Offset: 0x00052E48
	private IEnumerator CreateWeatherListLate()
	{
		yield return 0;
		this.CreateZoneWeatherTypeList();
		this.init = true;
		yield break;
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00054A64 File Offset: 0x00052E64
	private void LateUpdate()
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (EnviroSky.instance.started && !this.init)
		{
			if (this.isDefault)
			{
				this.CreateZoneWeatherTypeList();
				this.init = true;
			}
			else
			{
				base.StartCoroutine(this.CreateWeatherListLate());
			}
		}
		if (this.updateMode == EnviroZone.WeatherUpdateMode.GameTimeHours)
		{
			if (EnviroSky.instance.currentTimeInHours > this.nextUpdate && EnviroSky.instance.Weather.updateWeather && EnviroSky.instance.started)
			{
				this.WeatherUpdate();
			}
		}
		else if (Time.time > this.nextUpdateRealtime && EnviroSky.instance.Weather.updateWeather && EnviroSky.instance.started)
		{
			this.WeatherUpdate();
		}
		if (EnviroSky.instance.Player == null)
		{
			return;
		}
		if (this.isDefault && this.init)
		{
			this.zoneCollider.center = new Vector3(0f, (EnviroSky.instance.Player.transform.position.y - EnviroSky.instance.transform.position.y) / EnviroSky.instance.transform.lossyScale.y, 0f);
		}
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00054BE0 File Offset: 0x00052FE0
	private void OnTriggerEnter(Collider col)
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (EnviroSky.instance.profile.weatherSettings.useTag)
		{
			if (col.gameObject.tag == EnviroSky.instance.gameObject.tag)
			{
				EnviroSky.instance.Weather.currentActiveZone = this;
				EnviroSky.instance.NotifyZoneChanged(this);
			}
		}
		else if (col.gameObject.GetComponent<EnviroSky>())
		{
			EnviroSky.instance.Weather.currentActiveZone = this;
			EnviroSky.instance.NotifyZoneChanged(this);
		}
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00054C8C File Offset: 0x0005308C
	private void OnTriggerExit(Collider col)
	{
		if (!this.ExitToDefault || EnviroSky.instance == null)
		{
			return;
		}
		if (EnviroSky.instance.profile.weatherSettings.useTag)
		{
			if (col.gameObject.tag == EnviroSky.instance.gameObject.tag)
			{
				EnviroSky.instance.Weather.currentActiveZone = EnviroSky.instance.Weather.zones[0];
				EnviroSky.instance.NotifyZoneChanged(EnviroSky.instance.Weather.zones[0]);
			}
		}
		else if (col.gameObject.GetComponent<EnviroSky>())
		{
			EnviroSky.instance.Weather.currentActiveZone = EnviroSky.instance.Weather.zones[0];
			EnviroSky.instance.NotifyZoneChanged(EnviroSky.instance.Weather.zones[0]);
		}
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x00054D94 File Offset: 0x00053194
	private void OnDrawGizmos()
	{
		Gizmos.color = this.zoneGizmoColor;
		Gizmos.DrawCube(base.transform.position, new Vector3(this.zoneScale.x, this.zoneScale.y, this.zoneScale.z));
	}

	// Token: 0x040010C6 RID: 4294
	[Tooltip("Defines the zone name.")]
	public string zoneName;

	// Token: 0x040010C7 RID: 4295
	[Tooltip("Uncheck to remove OnTriggerExit call when using overlapping zone layout.")]
	public bool ExitToDefault = true;

	// Token: 0x040010C8 RID: 4296
	public List<EnviroWeatherPrefab> zoneWeather = new List<EnviroWeatherPrefab>();

	// Token: 0x040010C9 RID: 4297
	public List<EnviroWeatherPrefab> curPossibleZoneWeather;

	// Token: 0x040010CA RID: 4298
	[Header("Zone weather settings:")]
	[Tooltip("Add all weather prefabs for this zone here.")]
	public List<EnviroWeatherPreset> zoneWeatherPresets = new List<EnviroWeatherPreset>();

	// Token: 0x040010CB RID: 4299
	[Tooltip("Shall weather changes occure based on gametime or realtime?")]
	public EnviroZone.WeatherUpdateMode updateMode;

	// Token: 0x040010CC RID: 4300
	[Tooltip("Defines how often (gametime hours or realtime minutes) the system will heck to change the current weather conditions.")]
	public float WeatherUpdateIntervall = 6f;

	// Token: 0x040010CD RID: 4301
	[Header("Zone scaling and gizmo:")]
	[Tooltip("Defines the zone scale.")]
	public Vector3 zoneScale = new Vector3(100f, 100f, 100f);

	// Token: 0x040010CE RID: 4302
	[Tooltip("Defines the color of the zone's gizmo in editor mode.")]
	public Color zoneGizmoColor = Color.gray;

	// Token: 0x040010CF RID: 4303
	[Header("Current active weather:")]
	[Tooltip("The current active weather conditions.")]
	public EnviroWeatherPrefab currentActiveZoneWeatherPrefab;

	// Token: 0x040010D0 RID: 4304
	public EnviroWeatherPreset currentActiveZoneWeatherPreset;

	// Token: 0x040010D1 RID: 4305
	[HideInInspector]
	public EnviroWeatherPrefab lastActiveZoneWeatherPrefab;

	// Token: 0x040010D2 RID: 4306
	[HideInInspector]
	public EnviroWeatherPreset lastActiveZoneWeatherPreset;

	// Token: 0x040010D3 RID: 4307
	private BoxCollider zoneCollider;

	// Token: 0x040010D4 RID: 4308
	private double nextUpdate;

	// Token: 0x040010D5 RID: 4309
	private float nextUpdateRealtime;

	// Token: 0x040010D6 RID: 4310
	private bool init;

	// Token: 0x040010D7 RID: 4311
	private bool isDefault;

	// Token: 0x02000367 RID: 871
	public enum WeatherUpdateMode
	{
		// Token: 0x040010D9 RID: 4313
		GameTimeHours,
		// Token: 0x040010DA RID: 4314
		RealTimeMinutes
	}
}
