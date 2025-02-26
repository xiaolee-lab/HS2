using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class AQUAS_LensEffects : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000088 RID: 136 RVA: 0x00006F68 File Offset: 0x00005368
	// (set) Token: 0x06000089 RID: 137 RVA: 0x00006F70 File Offset: 0x00005370
	public bool underWater { get; private set; }

	// Token: 0x0600008A RID: 138 RVA: 0x00006F7C File Offset: 0x0000537C
	private void Start()
	{
		if (this.gameObjects.waterLens != null)
		{
			this.waterLensAudio = this.gameObjects.waterLens.GetComponent<AudioSource>();
		}
		if (this.gameObjects.airLens != null)
		{
			this.airLensAudio = this.gameObjects.airLens.GetComponent<AudioSource>();
		}
		this.audioComp = base.GetComponent<AudioSource>();
		this.cameraAudio = this.gameObjects.mainCamera.GetComponent<AudioSource>();
		this.bubbleBehaviour = this.gameObjects.bubble.GetComponent<AQUAS_BubbleBehaviour>();
		if (this.gameObjects.airLens != null)
		{
			this.gameObjects.airLens.SetActive(true);
		}
		if (this.gameObjects.waterLens != null)
		{
			this.gameObjects.waterLens.SetActive(false);
		}
		this.waterPlaneMaterial = this.gameObjects.waterPlanes[0].GetComponent<Renderer>().material;
		this.t = this.wetLens.wetTime + this.wetLens.dryingTime;
		this.t2 = 0f;
		this.bubbleSpawnTimer = 0f;
		this.defaultFog = RenderSettings.fog;
		this.defaultFogDensity = RenderSettings.fogDensity;
		this.defaultFogColor = RenderSettings.fogColor;
		this.defaultFoamContrast = this.waterPlaneMaterial.GetFloat("_FoamContrast");
		this.defaultSpecularity = this.waterPlaneMaterial.GetFloat("_Specular");
		if (this.waterPlaneMaterial.HasProperty("_Refraction"))
		{
			this.defaultRefraction = this.waterPlaneMaterial.GetFloat("_Refraction");
		}
		this.audioComp.clip = this.soundEffects.sounds[0];
		this.audioComp.loop = true;
		this.audioComp.Stop();
		if (this.airLensAudio != null)
		{
			this.airLensAudio.clip = this.soundEffects.sounds[1];
			this.airLensAudio.loop = false;
			this.airLensAudio.Stop();
		}
		if (this.waterLensAudio != null)
		{
			this.waterLensAudio.clip = this.soundEffects.sounds[2];
			this.waterLensAudio.loop = false;
			this.waterLensAudio.Stop();
		}
		if (GameObject.Find("Tenkoku DynamicSky") != null)
		{
			this.tenkokuObj = GameObject.Find("Tenkoku DynamicSky");
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00007208 File Offset: 0x00005608
	private void Update()
	{
		this.CheckIfStillUnderWater();
		if (this.underWater)
		{
			this.t = 0f;
			this.t2 += Time.deltaTime;
			if (this.gameObjects.airLens != null)
			{
				this.gameObjects.airLens.SetActive(false);
			}
			if (this.gameObjects.waterLens != null)
			{
				this.gameObjects.waterLens.SetActive(true);
			}
			this.sprayFrameIndex = 0;
			this.rundown = true;
			this.BubbleSpawner();
			if (this.playUnderwater)
			{
				this.audioComp.Play();
				this.playUnderwater = false;
			}
			if (this.playDiveSplash)
			{
				this.waterLensAudio.Play();
				this.playDiveSplash = false;
			}
			this.playSurfaceSplash = true;
			if (this.airLensAudio != null)
			{
				this.airLensAudio.Stop();
			}
			if (this.cameraAudio != null)
			{
				this.cameraAudio.enabled = false;
			}
			if (this.airLensAudio != null)
			{
				this.airLensAudio.volume = this.soundEffects.surfacingVolume;
			}
			this.audioComp.volume = this.soundEffects.diveVolume;
			if (this.waterLensAudio != null)
			{
				this.waterLensAudio.volume = this.soundEffects.underwaterVolume;
			}
			if (this.primaryCausticsProjector != null)
			{
				this.primaryCausticsProjector.material.SetTextureScale("_Texture", new Vector2(this.causticSettings.causticTiling.y, this.causticSettings.causticTiling.y));
				this.primaryCausticsProjector.material.SetFloat("_Intensity", this.causticSettings.causticIntensity.y);
				this.primaryAquasCaustics.maxCausticDepth = this.causticSettings.maxCausticDepth;
			}
			if (this.secondaryCausticsProjector != null)
			{
				this.secondaryCausticsProjector.material.SetTextureScale("_Texture", new Vector2(this.causticSettings.causticTiling.y, this.causticSettings.causticTiling.y));
				this.secondaryCausticsProjector.material.SetFloat("_Intensity", this.causticSettings.causticIntensity.y);
				this.secondaryAquasCaustics.maxCausticDepth = this.causticSettings.maxCausticDepth;
			}
			this.waterPlaneMaterial.SetFloat("_UnderwaterMode", 1f);
			this.waterPlaneMaterial.SetFloat("_FoamContrast", 0f);
			this.waterPlaneMaterial.SetFloat("_Specular", this.defaultSpecularity * 5f);
			this.waterPlaneMaterial.SetFloat("_Refraction", 0.7f);
			if (this.tenkokuObj != null)
			{
				Component component = this.tenkokuObj.GetComponent("TenkokuModule");
				FieldInfo field = component.GetType().GetField("enableFog", BindingFlags.Instance | BindingFlags.Public);
				if (field != null)
				{
					field.SetValue(component, false);
				}
			}
			RenderSettings.fog = true;
			RenderSettings.fogDensity = this.underWaterParameters.fogDensity;
			RenderSettings.fogColor = this.underWaterParameters.fogColor;
		}
		else
		{
			this.t2 = 0f;
			this.t += Time.deltaTime;
			if (this.gameObjects.airLens != null)
			{
				this.gameObjects.airLens.SetActive(true);
			}
			if (this.gameObjects.waterLens != null)
			{
				this.gameObjects.waterLens.SetActive(false);
			}
			if (this.rundown)
			{
				this.sprayFrameIndex = 0;
				this.NextFrame();
				base.InvokeRepeating("NextFrame", 1f / this.wetLens.rundownSpeed, 1f / this.wetLens.rundownSpeed);
				this.rundown = false;
			}
			this.bubbleCount = 0;
			this.maxBubbleCount = UnityEngine.Random.Range(this.bubbleSpawnCriteria.minBubbleCount, this.bubbleSpawnCriteria.maxBubbleCount);
			this.bubbleSpawnTimer = 0f;
			if (this.playSurfaceSplash)
			{
				this.airLensAudio.Play();
				this.playSurfaceSplash = false;
			}
			this.playUnderwater = true;
			this.playDiveSplash = true;
			this.audioComp.Stop();
			if (this.waterLensAudio != null)
			{
				this.waterLensAudio.Stop();
			}
			if (this.cameraAudio != null)
			{
				this.cameraAudio.enabled = true;
			}
			if (this.primaryCausticsProjector != null)
			{
				this.primaryCausticsProjector.material.SetTextureScale("_Texture", new Vector2(this.causticSettings.causticTiling.x, this.causticSettings.causticTiling.x));
				this.primaryCausticsProjector.material.SetFloat("_Intensity", this.causticSettings.causticIntensity.x);
			}
			if (this.secondaryCausticsProjector != null)
			{
				this.secondaryCausticsProjector.material.SetTextureScale("_Texture", new Vector2(this.causticSettings.causticTiling.x, this.causticSettings.causticTiling.x));
				this.secondaryCausticsProjector.material.SetFloat("_Intensity", this.causticSettings.causticIntensity.x);
			}
			if (this.t <= this.wetLens.wetTime)
			{
				if (this.airLensMaterial != null)
				{
					this.airLensMaterial.SetFloat("_Refraction", 1f);
					this.airLensMaterial.SetFloat("_Transparency", 0.01f);
				}
			}
			else if (this.airLensMaterial != null)
			{
				this.airLensMaterial.SetFloat("_Refraction", Mathf.Lerp(1f, 0f, (this.t - this.wetLens.wetTime) / this.wetLens.dryingTime));
				this.airLensMaterial.SetFloat("_Transparency", Mathf.Lerp(0.01f, 0f, (this.t - this.wetLens.wetTime) / this.wetLens.dryingTime));
			}
			this.waterPlaneMaterial.SetFloat("_FoamContrast", this.defaultFoamContrast);
			this.waterPlaneMaterial.SetFloat("_UnderwaterMode", 0f);
			this.waterPlaneMaterial.SetFloat("_Specular", this.defaultSpecularity);
			this.waterPlaneMaterial.SetFloat("_Refraction", this.defaultRefraction);
			if (this.tenkokuObj != null)
			{
				Component component2 = this.tenkokuObj.GetComponent("TenkokuModule");
				FieldInfo field2 = component2.GetType().GetField("enableFog", BindingFlags.Instance | BindingFlags.Public);
				if (field2 != null)
				{
					field2.SetValue(component2, true);
				}
			}
			RenderSettings.fog = this.defaultFog;
			if (this.setAfloatFog)
			{
				RenderSettings.fogColor = this.defaultFogColor;
				RenderSettings.fogDensity = this.defaultFogDensity;
			}
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00007950 File Offset: 0x00005D50
	private bool CheckIfUnderWater(int waterPlanesCount)
	{
		if (!this.gameObjects.useSquaredPlanes)
		{
			for (int i = 0; i < waterPlanesCount; i++)
			{
				if (Mathf.Pow(base.transform.position.x - this.gameObjects.waterPlanes[i].transform.position.x, 2f) + Mathf.Pow(base.transform.position.z - this.gameObjects.waterPlanes[i].transform.position.z, 2f) < Mathf.Pow(this.gameObjects.waterPlanes[i].GetComponent<Renderer>().bounds.extents.x, 2f))
				{
					if (this.activePlane != this.lastActivePlane)
					{
						if (this.gameObjects.waterPlanes[this.activePlane].transform.Find("PrimaryCausticsProjector") != null)
						{
							this.primaryCausticsProjector = this.gameObjects.waterPlanes[this.activePlane].transform.Find("PrimaryCausticsProjector").GetComponent<Projector>();
							this.primaryAquasCaustics = this.gameObjects.waterPlanes[this.activePlane].transform.Find("PrimaryCausticsProjector").GetComponent<AQUAS_Caustics>();
						}
						if (this.gameObjects.waterPlanes[this.activePlane].transform.Find("SecondaryCausticsProjector") != null)
						{
							this.secondaryCausticsProjector = this.gameObjects.waterPlanes[this.activePlane].transform.Find("SecondaryCausticsProjector").GetComponent<Projector>();
							this.secondaryAquasCaustics = this.gameObjects.waterPlanes[this.activePlane].transform.Find("SecondaryCausticsProjector").GetComponent<AQUAS_Caustics>();
						}
						this.lastActivePlane = this.activePlane;
					}
					this.activePlane = i;
					if (base.transform.position.y < this.gameObjects.waterPlanes[i].transform.position.y)
					{
						this.waterPlaneMaterial = this.gameObjects.waterPlanes[i].GetComponent<Renderer>().material;
						this.activePlane = i;
						return true;
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < waterPlanesCount; j++)
			{
				if (Mathf.Abs(base.transform.position.x - this.gameObjects.waterPlanes[j].transform.position.x) < this.gameObjects.waterPlanes[j].GetComponent<Renderer>().bounds.extents.x && Mathf.Abs(base.transform.position.z - this.gameObjects.waterPlanes[j].transform.position.z) < this.gameObjects.waterPlanes[j].GetComponent<Renderer>().bounds.extents.z)
				{
					if (this.activePlane != this.lastActivePlane)
					{
						if (this.gameObjects.waterPlanes[this.activePlane].transform.Find("PrimaryCausticsProjector") != null)
						{
							this.primaryCausticsProjector = this.gameObjects.waterPlanes[this.activePlane].transform.Find("PrimaryCausticsProjector").GetComponent<Projector>();
							this.primaryAquasCaustics = this.gameObjects.waterPlanes[this.activePlane].transform.Find("PrimaryCausticsProjector").GetComponent<AQUAS_Caustics>();
						}
						if (this.gameObjects.waterPlanes[this.activePlane].transform.Find("SecondaryCausticsProjector") != null)
						{
							this.secondaryCausticsProjector = this.gameObjects.waterPlanes[this.activePlane].transform.Find("SecondaryCausticsProjector").GetComponent<Projector>();
							this.secondaryAquasCaustics = this.gameObjects.waterPlanes[this.activePlane].transform.Find("SecondaryCausticsProjector").GetComponent<AQUAS_Caustics>();
						}
						this.lastActivePlane = this.activePlane;
					}
					this.activePlane = j;
					if (base.transform.position.y < this.gameObjects.waterPlanes[j].transform.position.y)
					{
						this.waterPlaneMaterial = this.gameObjects.waterPlanes[0].GetComponent<Renderer>().material;
						this.activePlane = j;
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00007E98 File Offset: 0x00006298
	private void CheckIfStillUnderWater()
	{
		if (!this.gameObjects.useSquaredPlanes)
		{
			if (this.underWater && Mathf.Pow(base.transform.position.x - this.gameObjects.waterPlanes[this.activePlane].transform.position.x, 2f) + Mathf.Pow(base.transform.position.z - this.gameObjects.waterPlanes[this.activePlane].transform.position.z, 2f) > Mathf.Pow(this.gameObjects.waterPlanes[this.activePlane].GetComponent<Renderer>().bounds.extents.x, 2f))
			{
				this.underWater = false;
			}
			else if (this.underWater && base.transform.position.y > this.gameObjects.waterPlanes[this.activePlane].transform.position.y)
			{
				this.underWater = false;
			}
			else if (!this.underWater)
			{
				this.underWater = this.CheckIfUnderWater(this.gameObjects.waterPlanes.Count);
			}
		}
		else if ((this.underWater && Mathf.Abs(base.transform.position.x - this.gameObjects.waterPlanes[this.activePlane].transform.position.x) > this.gameObjects.waterPlanes[this.activePlane].GetComponent<Renderer>().bounds.extents.x) || (this.underWater && Mathf.Abs(base.transform.position.z - this.gameObjects.waterPlanes[this.activePlane].transform.position.z) > this.gameObjects.waterPlanes[this.activePlane].GetComponent<Renderer>().bounds.extents.z))
		{
			this.underWater = false;
		}
		else if (this.underWater && base.transform.position.y > this.gameObjects.waterPlanes[this.activePlane].transform.position.y)
		{
			this.underWater = false;
		}
		else if (!this.underWater)
		{
			this.underWater = this.CheckIfUnderWater(this.gameObjects.waterPlanes.Count);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000081B8 File Offset: 0x000065B8
	private void NextFrame()
	{
		if (this.sprayFrameIndex >= this.wetLens.sprayFrames.Length - 1)
		{
			this.sprayFrameIndex = 0;
			base.CancelInvoke("NextFrame");
		}
		this.airLensMaterial.SetTexture("_CutoutReferenceTexture", this.wetLens.sprayFramesCutout[this.sprayFrameIndex]);
		this.airLensMaterial.SetTexture("_Normal", this.wetLens.sprayFrames[this.sprayFrameIndex]);
		this.sprayFrameIndex++;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00008244 File Offset: 0x00006644
	private void BubbleSpawner()
	{
		if (this.t2 > this.bubbleSpawnTimer && this.maxBubbleCount > this.bubbleCount)
		{
			float num = UnityEngine.Random.Range(0f, this.bubbleSpawnCriteria.avgScaleSummand * 2f);
			this.bubbleBehaviour.mainCamera = this.gameObjects.mainCamera;
			this.bubbleBehaviour.waterLevel = this.gameObjects.waterPlanes[this.activePlane].transform.position.y;
			this.bubbleBehaviour.averageUpdrift = this.bubbleSpawnCriteria.averageUpdrift + UnityEngine.Random.Range(-this.bubbleSpawnCriteria.averageUpdrift * 0.75f, this.bubbleSpawnCriteria.averageUpdrift * 0.75f);
			this.gameObjects.bubble.transform.localScale += new Vector3(num, num, num);
			UnityEngine.Object.Instantiate<GameObject>(this.gameObjects.bubble, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-this.bubbleSpawnCriteria.maxSpawnDistance, this.bubbleSpawnCriteria.maxSpawnDistance), base.transform.position.y - 0.4f, base.transform.position.z + UnityEngine.Random.Range(-this.bubbleSpawnCriteria.maxSpawnDistance, this.bubbleSpawnCriteria.maxSpawnDistance)), Quaternion.identity);
			this.bubbleSpawnTimer += UnityEngine.Random.Range(this.bubbleSpawnCriteria.minSpawnTimer, this.bubbleSpawnCriteria.maxSpawnTimer);
			this.bubbleCount++;
			this.gameObjects.bubble.transform.localScale = new Vector3(this.bubbleSpawnCriteria.baseScale, this.bubbleSpawnCriteria.baseScale, this.bubbleSpawnCriteria.baseScale);
		}
		else if (this.t2 > this.bubbleSpawnTimer && this.maxBubbleCount == this.bubbleCount)
		{
			float num2 = UnityEngine.Random.Range(0f, this.bubbleSpawnCriteria.avgScaleSummand * 2f);
			this.bubbleBehaviour.mainCamera = this.gameObjects.mainCamera;
			this.bubbleBehaviour.waterLevel = this.gameObjects.waterPlanes[this.activePlane].transform.position.y;
			this.bubbleBehaviour.averageUpdrift = this.bubbleSpawnCriteria.averageUpdrift + UnityEngine.Random.Range(-this.bubbleSpawnCriteria.averageUpdrift * 0.75f, this.bubbleSpawnCriteria.averageUpdrift * 0.75f);
			this.gameObjects.bubble.transform.localScale += new Vector3(num2, num2, num2);
			UnityEngine.Object.Instantiate<GameObject>(this.gameObjects.bubble, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-this.bubbleSpawnCriteria.maxSpawnDistance, this.bubbleSpawnCriteria.maxSpawnDistance), base.transform.position.y - 0.4f, base.transform.position.z + UnityEngine.Random.Range(-this.bubbleSpawnCriteria.maxSpawnDistance, this.bubbleSpawnCriteria.maxSpawnDistance)), Quaternion.identity);
			this.bubbleSpawnTimer += UnityEngine.Random.Range(this.bubbleSpawnCriteria.minSpawnTimerL, this.bubbleSpawnCriteria.maxSpawnTimerL);
			this.gameObjects.bubble.transform.localScale = new Vector3(this.bubbleSpawnCriteria.baseScale, this.bubbleSpawnCriteria.baseScale, this.bubbleSpawnCriteria.baseScale);
		}
	}

	// Token: 0x04000182 RID: 386
	public AQUAS_Parameters.UnderWaterParameters underWaterParameters = new AQUAS_Parameters.UnderWaterParameters();

	// Token: 0x04000183 RID: 387
	public AQUAS_Parameters.GameObjects gameObjects = new AQUAS_Parameters.GameObjects();

	// Token: 0x04000184 RID: 388
	public AQUAS_Parameters.BubbleSpawnCriteria bubbleSpawnCriteria = new AQUAS_Parameters.BubbleSpawnCriteria();

	// Token: 0x04000185 RID: 389
	public AQUAS_Parameters.WetLens wetLens = new AQUAS_Parameters.WetLens();

	// Token: 0x04000186 RID: 390
	public AQUAS_Parameters.CausticSettings causticSettings = new AQUAS_Parameters.CausticSettings();

	// Token: 0x04000187 RID: 391
	public AQUAS_Parameters.Audio soundEffects = new AQUAS_Parameters.Audio();

	// Token: 0x04000188 RID: 392
	private int sprayFrameIndex;

	// Token: 0x04000189 RID: 393
	private GameObject tenkokuObj;

	// Token: 0x0400018A RID: 394
	private Material airLensMaterial;

	// Token: 0x0400018B RID: 395
	private Material waterPlaneMaterial;

	// Token: 0x0400018C RID: 396
	[HideInInspector]
	public float t;

	// Token: 0x0400018D RID: 397
	private float t2;

	// Token: 0x0400018E RID: 398
	private float bubbleSpawnTimer;

	// Token: 0x0400018F RID: 399
	private float defaultFogDensity;

	// Token: 0x04000190 RID: 400
	private Color defaultFogColor;

	// Token: 0x04000191 RID: 401
	private float defaultFoamContrast;

	// Token: 0x04000192 RID: 402
	private float defaultBloomIntensity;

	// Token: 0x04000193 RID: 403
	private float defaultSpecularity;

	// Token: 0x04000194 RID: 404
	private float defaultRefraction;

	// Token: 0x04000195 RID: 405
	private bool defaultFog;

	// Token: 0x04000196 RID: 406
	private bool defaultSunShaftsEnabled;

	// Token: 0x04000197 RID: 407
	private bool defaultBloomEnabled;

	// Token: 0x04000198 RID: 408
	private bool defaultBlurEnabled;

	// Token: 0x04000199 RID: 409
	private bool defaultVignetteEnabled;

	// Token: 0x0400019A RID: 410
	private bool defaultNoiseEnabled;

	// Token: 0x0400019C RID: 412
	[HideInInspector]
	public bool setAfloatFog = true;

	// Token: 0x0400019D RID: 413
	[HideInInspector]
	public bool rundown;

	// Token: 0x0400019E RID: 414
	private bool playSurfaceSplash;

	// Token: 0x0400019F RID: 415
	private bool playDiveSplash;

	// Token: 0x040001A0 RID: 416
	private bool playUnderwater;

	// Token: 0x040001A1 RID: 417
	private int bubbleCount;

	// Token: 0x040001A2 RID: 418
	private int maxBubbleCount;

	// Token: 0x040001A3 RID: 419
	private int activePlane;

	// Token: 0x040001A4 RID: 420
	private int lastActivePlane = 100;

	// Token: 0x040001A5 RID: 421
	private FieldInfo fi;

	// Token: 0x040001A6 RID: 422
	private AudioSource waterLensAudio;

	// Token: 0x040001A7 RID: 423
	private AudioSource airLensAudio;

	// Token: 0x040001A8 RID: 424
	private AudioSource audioComp;

	// Token: 0x040001A9 RID: 425
	private AudioSource cameraAudio;

	// Token: 0x040001AA RID: 426
	private Projector primaryCausticsProjector;

	// Token: 0x040001AB RID: 427
	private Projector secondaryCausticsProjector;

	// Token: 0x040001AC RID: 428
	private AQUAS_Caustics primaryAquasCaustics;

	// Token: 0x040001AD RID: 429
	private AQUAS_Caustics secondaryAquasCaustics;

	// Token: 0x040001AE RID: 430
	private AQUAS_BubbleBehaviour bubbleBehaviour;
}
