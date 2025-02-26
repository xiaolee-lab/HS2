using System;
using UnityEngine;

// Token: 0x02000613 RID: 1555
[AddComponentMenu("UBER/Global Params")]
[ExecuteInEditMode]
public class UBER_GlobalParams : MonoBehaviour
{
	// Token: 0x0600251D RID: 9501 RVA: 0x000D32B3 File Offset: 0x000D16B3
	private void Update()
	{
		this.AdvanceTime(Time.deltaTime);
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x000D32C0 File Offset: 0x000D16C0
	private void Start()
	{
		this.SetupIt();
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x000D32C8 File Offset: 0x000D16C8
	public void SetupIt()
	{
		Shader.SetGlobalFloat("_UBER_GlobalDry", 1f - this.WaterLevel);
		Shader.SetGlobalFloat("_UBER_GlobalDryConst", 1f - this.WetnessAmount);
		Shader.SetGlobalFloat("_UBER_GlobalRainDamp", 1f - this.RainIntensity);
		Shader.SetGlobalFloat("_UBER_RippleStrength", this.FlowBumpStrength);
		Shader.SetGlobalFloat("_UBER_GlobalSnowDamp", 1f - this.SnowLevel);
		Shader.SetGlobalFloat("_UBER_Frost", 1f - this.Frost);
		Shader.SetGlobalFloat("_UBER_GlobalSnowDissolve", this.SnowDissolve);
		Shader.SetGlobalFloat("_UBER_GlobalSnowBumpMicro", this.SnowBumpMicro);
		Shader.SetGlobalColor("_UBER_GlobalSnowSpecGloss", this.SnowSpecGloss);
		Shader.SetGlobalColor("_UBER_GlobalSnowGlitterColor", this.SnowGlitterColor);
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x000D3394 File Offset: 0x000D1794
	public void AdvanceTime(float amount)
	{
		this.SimulateDynamicWeather(amount * this.weatherTimeScale);
		amount *= this.flowTimeScale;
		this.__Time.x = this.__Time.x + amount / 20f;
		this.__Time.y = this.__Time.y + amount;
		this.__Time.z = this.__Time.z + amount * 2f;
		this.__Time.w = this.__Time.w + amount * 3f;
		Shader.SetGlobalVector("UBER_Time", this.__Time);
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x000D3428 File Offset: 0x000D1828
	public void SimulateDynamicWeather(float dt)
	{
		if (dt == 0f || !this.Simulate)
		{
			return;
		}
		float rainIntensity = this.RainIntensity;
		float propA = this.temperature;
		float propA2 = this.flowTimeScale;
		float flowBumpStrength = this.FlowBumpStrength;
		float waterLevel = this.WaterLevel;
		float wetnessAmount = this.WetnessAmount;
		float snowLevel = this.SnowLevel;
		float snowDissolve = this.SnowDissolve;
		float snowBumpMicro = this.SnowBumpMicro;
		Color snowSpecGloss = this.SnowSpecGloss;
		Color snowGlitterColor = this.SnowGlitterColor;
		float num = this.wind * 4f + 1f;
		float num2 = (!this.FreezeWetWhenSnowPresent) ? 1f : Mathf.Clamp01((0.05f - this.SnowLevel) / 0.05f);
		if (this.temperature > 0f)
		{
			float num3 = this.temperature + 10f;
			this.RainIntensity = this.fallIntensity * num2;
			this.flowTimeScale += dt * num3 * 0.3f * num2;
			if (this.flowTimeScale > 1f)
			{
				this.flowTimeScale = 1f;
			}
			this.FlowBumpStrength += dt * num3 * 0.3f * num2;
			if (this.FlowBumpStrength > 1f)
			{
				this.FlowBumpStrength = 1f;
			}
			this.WaterLevel += this.RainIntensity * dt * 2f * num2;
			if (this.WaterLevel > 1f)
			{
				this.WaterLevel = 1f;
			}
			this.WetnessAmount += this.RainIntensity * dt * 3f * num2;
			if (this.WetnessAmount > 1f)
			{
				this.WetnessAmount = 1f;
			}
			float num4 = Mathf.Abs(dt * num3 * 0.03f + dt * this.RainIntensity * 0.05f);
			this.SnowDissolve = this.TargetValue(this.SnowDissolve, this.SnowDissolveMelt, num4 * 2f);
			this.SnowBumpMicro = this.TargetValue(this.SnowBumpMicro, this.SnowBumpMicroMelt, num4 * 0.1f);
			this.SnowSpecGloss.r = this.TargetValue(this.SnowSpecGloss.r, this.SnowSpecGlossMelt.r, num4);
			this.SnowSpecGloss.g = this.TargetValue(this.SnowSpecGloss.g, this.SnowSpecGlossMelt.g, num4);
			this.SnowSpecGloss.b = this.TargetValue(this.SnowSpecGloss.b, this.SnowSpecGlossMelt.b, num4);
			this.SnowSpecGloss.a = this.TargetValue(this.SnowSpecGloss.a, this.SnowSpecGlossMelt.a, num4);
			this.SnowGlitterColor.r = this.TargetValue(this.SnowGlitterColor.r, this.SnowGlitterColorMelt.r, num4);
			this.SnowGlitterColor.g = this.TargetValue(this.SnowGlitterColor.g, this.SnowGlitterColorMelt.g, num4);
			this.SnowGlitterColor.b = this.TargetValue(this.SnowGlitterColor.b, this.SnowGlitterColorMelt.b, num4);
			this.SnowGlitterColor.a = this.TargetValue(this.SnowGlitterColor.a, this.SnowGlitterColorMelt.a, num4);
			this.Frost -= dt * num3 * 0.3f * num2;
			if (this.Frost < 0f)
			{
				this.Frost = 0f;
			}
			this.SnowLevel -= dt * num3 * 0.01f;
			if (this.SnowLevel < 0f)
			{
				this.SnowLevel = 0f;
			}
		}
		else
		{
			float num5 = this.temperature - 10f;
			this.RainIntensity += dt * num5 * 0.2f;
			if (this.RainIntensity < 0f)
			{
				this.RainIntensity = 0f;
			}
			this.flowTimeScale += dt * num5 * 0.3f * num;
			if (this.flowTimeScale < 0f)
			{
				this.flowTimeScale = 0f;
			}
			if (this.FlowBumpStrength > 0.1f)
			{
				this.FlowBumpStrength += dt * num5 * 0.5f * this.flowTimeScale;
				if (this.FlowBumpStrength < 0.1f)
				{
					this.FlowBumpStrength = 0.1f;
				}
			}
			float num6 = Mathf.Abs(dt * num5 * 0.05f) * this.fallIntensity;
			this.SnowDissolve = this.TargetValue(this.SnowDissolve, this.SnowDissolveCover, num6 * 2f);
			this.SnowBumpMicro = this.TargetValue(this.SnowBumpMicro, this.SnowBumpMicroCover, num6 * 0.1f);
			this.SnowSpecGloss.r = this.TargetValue(this.SnowSpecGloss.r, this.SnowSpecGlossCover.r, num6);
			this.SnowSpecGloss.g = this.TargetValue(this.SnowSpecGloss.g, this.SnowSpecGlossCover.g, num6);
			this.SnowSpecGloss.b = this.TargetValue(this.SnowSpecGloss.b, this.SnowSpecGlossCover.b, num6);
			this.SnowSpecGloss.a = this.TargetValue(this.SnowSpecGloss.a, this.SnowSpecGlossCover.a, num6);
			this.SnowGlitterColor.r = this.TargetValue(this.SnowGlitterColor.r, this.SnowGlitterColorCover.r, num6);
			this.SnowGlitterColor.g = this.TargetValue(this.SnowGlitterColor.g, this.SnowGlitterColorCover.g, num6);
			this.SnowGlitterColor.b = this.TargetValue(this.SnowGlitterColor.b, this.SnowGlitterColorCover.b, num6);
			this.SnowGlitterColor.a = this.TargetValue(this.SnowGlitterColor.a, this.SnowGlitterColorCover.a, num6);
			this.Frost -= dt * num5 * 0.3f;
			if (this.Frost > 1f)
			{
				this.Frost = 1f;
			}
			this.SnowLevel -= this.fallIntensity * (dt * num5 * 0.01f);
			if (this.SnowLevel > 1f)
			{
				this.SnowLevel = 1f;
			}
			if (this.AddWetWhenSnowPresent && this.WaterLevel < this.SnowLevel)
			{
				this.WaterLevel = this.SnowLevel;
			}
		}
		this.WaterLevel -= num * (this.temperature + 273f) * 0.001f * this.flowTimeScale * dt * num2;
		if (this.WaterLevel < 0f)
		{
			this.WaterLevel = 0f;
		}
		this.WetnessAmount -= num * (this.temperature + 273f) * 0.0003f * this.flowTimeScale * dt * num2;
		if (this.WetnessAmount < 0f)
		{
			this.WetnessAmount = 0f;
		}
		this.RefreshParticleSystem();
		bool flag = false;
		if (this.compareDelta(rainIntensity, this.RainIntensity))
		{
			flag = true;
		}
		else if (this.compareDelta(propA, this.temperature))
		{
			flag = true;
		}
		else if (this.compareDelta(propA2, this.flowTimeScale))
		{
			flag = true;
		}
		else if (this.compareDelta(flowBumpStrength, this.FlowBumpStrength))
		{
			flag = true;
		}
		else if (this.compareDelta(waterLevel, this.WaterLevel))
		{
			flag = true;
		}
		else if (this.compareDelta(wetnessAmount, this.WetnessAmount))
		{
			flag = true;
		}
		else if (this.compareDelta(snowLevel, this.SnowLevel))
		{
			flag = true;
		}
		else if (this.compareDelta(snowDissolve, this.SnowDissolve))
		{
			flag = true;
		}
		else if (this.compareDelta(snowBumpMicro, this.SnowBumpMicro))
		{
			flag = true;
		}
		else if (this.compareDelta(snowSpecGloss, this.SnowSpecGloss))
		{
			flag = true;
		}
		else if (this.compareDelta(snowGlitterColor, this.SnowGlitterColor))
		{
			flag = true;
		}
		if (flag)
		{
			this.SetupIt();
		}
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x000D3CB8 File Offset: 0x000D20B8
	private bool compareDelta(float propA, float propB)
	{
		return Mathf.Abs(propA - propB) > 1E-06f;
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x000D3CCC File Offset: 0x000D20CC
	private bool compareDelta(Color propA, Color propB)
	{
		return Mathf.Abs(propA.r - propB.r) > 1E-06f || Mathf.Abs(propA.g - propB.g) > 1E-06f || Mathf.Abs(propA.b - propB.b) > 1E-06f || Mathf.Abs(propA.a - propB.a) > 1E-06f;
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x000D3D5A File Offset: 0x000D215A
	private float TargetValue(float val, float target_val, float delta)
	{
		if (val < target_val)
		{
			val += delta;
			if (val > target_val)
			{
				val = target_val;
			}
		}
		else if (val > target_val)
		{
			val -= delta;
			if (val < target_val)
			{
				val = target_val;
			}
		}
		return val;
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x000D3D90 File Offset: 0x000D2190
	public void RefreshParticleSystem()
	{
		if (this.paricleSystemActive != this.UseParticleSystem)
		{
			if (this.rainGameObject)
			{
				this.rainGameObject.SetActive(this.UseParticleSystem);
			}
			if (this.snowGameObject)
			{
				this.snowGameObject.SetActive(this.UseParticleSystem);
			}
			this.paricleSystemActive = this.UseParticleSystem;
		}
		if (this.UseParticleSystem)
		{
			if (this.rainGameObject != null)
			{
				this.rainGameObject.transform.position = base.transform.position + Vector3.up * 3f;
				if (this.psRain == null)
				{
					this.psRain = this.rainGameObject.GetComponent<ParticleSystem>();
				}
			}
			if (this.snowGameObject != null)
			{
				this.snowGameObject.transform.position = base.transform.position + Vector3.up * 3f;
				if (this.psSnow == null)
				{
					this.psSnow = this.snowGameObject.GetComponent<ParticleSystem>();
				}
			}
			if (this.psRain != null)
			{
				ParticleSystem.EmissionModule emission = this.psRain.emission;
				ParticleSystem.MinMaxCurve rateOverTime = new ParticleSystem.MinMaxCurve(this.fallIntensity * 3000f * Mathf.Clamp01(this.temperature + 1f));
				emission.rateOverTime = rateOverTime;
			}
			if (this.psSnow != null)
			{
				ParticleSystem.EmissionModule emission2 = this.psSnow.emission;
				ParticleSystem.MinMaxCurve rateOverTime2 = new ParticleSystem.MinMaxCurve(this.fallIntensity * 3000f * Mathf.Clamp01(1f - this.temperature));
				emission2.rateOverTime = rateOverTime2;
			}
		}
	}

	// Token: 0x04002476 RID: 9334
	public const float DEFROST_RATE = 0.3f;

	// Token: 0x04002477 RID: 9335
	public const float RAIN_DAMP_ON_FREEZE_RATE = 0.2f;

	// Token: 0x04002478 RID: 9336
	public const float FROZEN_FLOW_BUMP_STRENGTH = 0.1f;

	// Token: 0x04002479 RID: 9337
	public const float FROST_RATE = 0.3f;

	// Token: 0x0400247A RID: 9338
	public const float FROST_RATE_BUMP = 0.5f;

	// Token: 0x0400247B RID: 9339
	public const float RAIN_TO_WATER_LEVEL_RATE = 2f;

	// Token: 0x0400247C RID: 9340
	public const float RAIN_TO_WET_AMOUNT_RATE = 3f;

	// Token: 0x0400247D RID: 9341
	public const float WATER_EVAPORATION_RATE = 0.001f;

	// Token: 0x0400247E RID: 9342
	public const float WET_EVAPORATION_RATE = 0.0003f;

	// Token: 0x0400247F RID: 9343
	public const float SNOW_FREEZE_RATE = 0.05f;

	// Token: 0x04002480 RID: 9344
	public const float SNOW_INCREASE_RATE = 0.01f;

	// Token: 0x04002481 RID: 9345
	public const float SNOW_MELT_RATE = 0.03f;

	// Token: 0x04002482 RID: 9346
	public const float SNOW_MELT_RATE_BY_RAIN = 0.05f;

	// Token: 0x04002483 RID: 9347
	public const float SNOW_DECREASE_RATE = 0.01f;

	// Token: 0x04002484 RID: 9348
	[Header("Global Water & Rain")]
	[Tooltip("You can control global water level (multiplied by material value)")]
	[Range(0f, 1f)]
	public float WaterLevel = 1f;

	// Token: 0x04002485 RID: 9349
	[Tooltip("You can control global wetness (multiplied by material value)")]
	[Range(0f, 1f)]
	public float WetnessAmount = 1f;

	// Token: 0x04002486 RID: 9350
	[Tooltip("Time scale for flow animation")]
	public float flowTimeScale = 1f;

	// Token: 0x04002487 RID: 9351
	[Tooltip("Multiplier of water flow ripple normalmap")]
	[Range(0f, 1f)]
	public float FlowBumpStrength = 1f;

	// Token: 0x04002488 RID: 9352
	[Tooltip("You can control global rain intensity")]
	[Range(0f, 1f)]
	public float RainIntensity = 1f;

	// Token: 0x04002489 RID: 9353
	[Header("Global Snow")]
	[Tooltip("You can control global snow")]
	[Range(0f, 1f)]
	public float SnowLevel = 1f;

	// Token: 0x0400248A RID: 9354
	[Tooltip("You can control global frost")]
	[Range(0f, 1f)]
	public float Frost = 1f;

	// Token: 0x0400248B RID: 9355
	[Tooltip("Global snow dissolve value")]
	[Range(0f, 4f)]
	public float SnowDissolve = 2f;

	// Token: 0x0400248C RID: 9356
	[Tooltip("Global snow dissolve value")]
	[Range(0.001f, 0.2f)]
	public float SnowBumpMicro = 0.08f;

	// Token: 0x0400248D RID: 9357
	[Tooltip("Global snow spec (RGB) & Gloss (A)")]
	public Color SnowSpecGloss = new Color(0.1f, 0.1f, 0.1f, 0.15f);

	// Token: 0x0400248E RID: 9358
	[Tooltip("Global snow glitter color/spec boost")]
	public Color SnowGlitterColor = new Color(0.8f, 0.8f, 0.8f, 0.2f);

	// Token: 0x0400248F RID: 9359
	[Header("Global Snow - cover state")]
	[HideInInspector]
	[Range(0f, 4f)]
	public float SnowDissolveCover = 2f;

	// Token: 0x04002490 RID: 9360
	[Tooltip("Global snow dissolve value")]
	[HideInInspector]
	[Range(0.001f, 0.2f)]
	public float SnowBumpMicroCover = 0.08f;

	// Token: 0x04002491 RID: 9361
	[Tooltip("Global snow spec (RGB) & Gloss (A)")]
	[HideInInspector]
	public Color SnowSpecGlossCover = new Color(0.1f, 0.1f, 0.1f, 0.15f);

	// Token: 0x04002492 RID: 9362
	[Tooltip("Global snow glitter color/spec boost")]
	[HideInInspector]
	public Color SnowGlitterColorCover = new Color(0.8f, 0.8f, 0.8f, 0.2f);

	// Token: 0x04002493 RID: 9363
	[Header("Global Snow - melt state")]
	[HideInInspector]
	[Range(0f, 4f)]
	public float SnowDissolveMelt = 0.3f;

	// Token: 0x04002494 RID: 9364
	[Tooltip("Global snow dissolve value")]
	[HideInInspector]
	[Range(0.001f, 0.2f)]
	public float SnowBumpMicroMelt = 0.02f;

	// Token: 0x04002495 RID: 9365
	[Tooltip("Global snow spec (RGB) & Gloss (A)")]
	[HideInInspector]
	public Color SnowSpecGlossMelt = new Color(0.15f, 0.15f, 0.15f, 0.6f);

	// Token: 0x04002496 RID: 9366
	[Tooltip("Global snow glitter color/spec boost")]
	[HideInInspector]
	public Color SnowGlitterColorMelt = new Color(0.1f, 0.1f, 0.1f, 0.03f);

	// Token: 0x04002497 RID: 9367
	[Header("Rainfall/snowfall controller")]
	public bool Simulate;

	// Token: 0x04002498 RID: 9368
	[Range(0f, 1f)]
	public float fallIntensity;

	// Token: 0x04002499 RID: 9369
	[Tooltip("Temperature (influences melt/freeze/evaporation speed) - 0 means water freeze")]
	[Range(-50f, 50f)]
	public float temperature = 20f;

	// Token: 0x0400249A RID: 9370
	[Tooltip("Wind (1 means 4x faster evaporation and freeze rate)")]
	[Range(0f, 1f)]
	public float wind;

	// Token: 0x0400249B RID: 9371
	[Tooltip("Speed of surface state change due to the weather dynamics")]
	[Range(0f, 1f)]
	public float weatherTimeScale = 1f;

	// Token: 0x0400249C RID: 9372
	[Tooltip("We won't melt ice nor decrease water level while snow level is >5%")]
	public bool FreezeWetWhenSnowPresent = true;

	// Token: 0x0400249D RID: 9373
	[Tooltip("Increase global Water level when snow appears")]
	public bool AddWetWhenSnowPresent = true;

	// Token: 0x0400249E RID: 9374
	[Space(10f)]
	[Tooltip("Set to show and adjust below particle systems")]
	public bool UseParticleSystem = true;

	// Token: 0x0400249F RID: 9375
	[Tooltip("GameObject with particle system attached controlling rain")]
	public GameObject rainGameObject;

	// Token: 0x040024A0 RID: 9376
	[Tooltip("GameObject with particle system attached controlling snow")]
	public GameObject snowGameObject;

	// Token: 0x040024A1 RID: 9377
	private Vector4 __Time;

	// Token: 0x040024A2 RID: 9378
	private float lTime;

	// Token: 0x040024A3 RID: 9379
	private bool paricleSystemActive;

	// Token: 0x040024A4 RID: 9380
	private ParticleSystem psRain;

	// Token: 0x040024A5 RID: 9381
	private ParticleSystem psSnow;
}
