using System;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class ME_DemoGUI : MonoBehaviour
{
	// Token: 0x0600134F RID: 4943 RVA: 0x000766E8 File Offset: 0x00074AE8
	private void Start()
	{
		if (Screen.dpi < 1f)
		{
			this.dpiScale = 1f;
		}
		if (Screen.dpi < 200f)
		{
			this.dpiScale = 1f;
		}
		else
		{
			this.dpiScale = Screen.dpi / 200f;
		}
		this.guiStyleHeader.fontSize = (int)(15f * this.dpiScale);
		this.guiStyleHeader.normal.textColor = this.guiColor;
		this.guiStyleHeaderMobile.fontSize = (int)(17f * this.dpiScale);
		this.ChangeCurrent(this.Current);
		this.startSunIntensity = this.Sun.intensity;
		this.startSunRotation = this.Sun.transform.rotation;
		this.startAmbientLight = RenderSettings.ambientLight;
		this.startAmbientIntencity = RenderSettings.ambientIntensity;
		this.startReflectionIntencity = RenderSettings.reflectionIntensity;
		this.startLightShadows = this.Sun.shadows;
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x000767EC File Offset: 0x00074BEC
	private void OnGUI()
	{
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			this.isButtonPressed = false;
		}
		if (GUI.Button(new Rect(10f * this.dpiScale, 15f * this.dpiScale, 135f * this.dpiScale, 37f * this.dpiScale), "PREVIOUS EFFECT") || (!this.isButtonPressed && Input.GetKeyDown(KeyCode.LeftArrow)))
		{
			this.isButtonPressed = true;
			this.ChangeCurrent(-1);
		}
		if (GUI.Button(new Rect(160f * this.dpiScale, 15f * this.dpiScale, 135f * this.dpiScale, 37f * this.dpiScale), "NEXT EFFECT") || (!this.isButtonPressed && Input.GetKeyDown(KeyCode.RightArrow)))
		{
			this.isButtonPressed = true;
			this.ChangeCurrent(1);
		}
		float num = 0f;
		if (GUI.Button(new Rect(10f * this.dpiScale, 63f * this.dpiScale + num, 285f * this.dpiScale, 37f * this.dpiScale), "Day / Night") || (!this.isButtonPressed && Input.GetKeyDown(KeyCode.DownArrow)))
		{
			this.isButtonPressed = true;
			if (this.ReflectionProbe != null)
			{
				this.ReflectionProbe.RenderProbe();
			}
			this.Sun.intensity = (this.isDay ? this.startSunIntensity : 0.05f);
			this.Sun.shadows = ((!this.isDay) ? LightShadows.None : this.startLightShadows);
			foreach (Light light in this.NightLights)
			{
				light.shadows = (this.isDay ? LightShadows.None : this.startLightShadows);
			}
			this.Sun.transform.rotation = ((!this.isDay) ? Quaternion.Euler(350f, 30f, 90f) : this.startSunRotation);
			RenderSettings.ambientLight = (this.isDay ? this.startAmbientLight : new Color(0.2f, 0.2f, 0.2f));
			float num2 = this.UseMobileVersion ? 0.3f : 1f;
			RenderSettings.ambientIntensity = ((!this.isDay) ? num2 : this.startAmbientIntencity);
			RenderSettings.reflectionIntensity = ((!this.isDay) ? 0.2f : this.startReflectionIntencity);
			this.isDay = !this.isDay;
		}
		GUI.Label(new Rect(400f * this.dpiScale, 15f * this.dpiScale + num / 2f, 100f * this.dpiScale, 20f * this.dpiScale), "Prefab name is \"" + this.Prefabs[this.currentNomber].name + "\"  \r\nHold any mouse button that would move the camera", this.guiStyleHeader);
		GUI.DrawTexture(new Rect(12f * this.dpiScale, 140f * this.dpiScale + num, 285f * this.dpiScale, 15f * this.dpiScale), this.HUETexture, ScaleMode.StretchToFill, false, 0f);
		float num3 = this.colorHUE;
		this.colorHUE = GUI.HorizontalSlider(new Rect(12f * this.dpiScale, 147f * this.dpiScale + num, 285f * this.dpiScale, 15f * this.dpiScale), this.colorHUE, 0f, 360f);
		if ((double)Mathf.Abs(num3 - this.colorHUE) > 0.001)
		{
			PSMeshRendererUpdater componentInChildren = this.characterInstance.GetComponentInChildren<PSMeshRendererUpdater>();
			if (componentInChildren != null)
			{
				componentInChildren.UpdateColor(this.colorHUE / 360f);
			}
			componentInChildren = this.modelInstance.GetComponentInChildren<PSMeshRendererUpdater>();
			if (componentInChildren != null)
			{
				componentInChildren.UpdateColor(this.colorHUE / 360f);
			}
		}
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00076C68 File Offset: 0x00075068
	private void ChangeCurrent(int delta)
	{
		this.currentNomber += delta;
		if (this.currentNomber > this.Prefabs.Length - 1)
		{
			this.currentNomber = 0;
		}
		else if (this.currentNomber < 0)
		{
			this.currentNomber = this.Prefabs.Length - 1;
		}
		if (this.characterInstance != null)
		{
			UnityEngine.Object.Destroy(this.characterInstance);
			this.RemoveClones();
		}
		if (this.modelInstance != null)
		{
			UnityEngine.Object.Destroy(this.modelInstance);
			this.RemoveClones();
		}
		this.characterInstance = UnityEngine.Object.Instantiate<GameObject>(this.Character);
		this.characterInstance.GetComponent<ME_AnimatorEvents>().EffectPrefab = this.Prefabs[this.currentNomber];
		this.modelInstance = UnityEngine.Object.Instantiate<GameObject>(this.Model);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefabs[this.currentNomber]);
		gameObject.transform.parent = this.modelInstance.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = default(Quaternion);
		PSMeshRendererUpdater component = gameObject.GetComponent<PSMeshRendererUpdater>();
		component.UpdateMeshEffect(this.modelInstance);
		if (this.UseMobileVersion)
		{
			base.CancelInvoke("ReactivateEffect");
		}
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x00076DBC File Offset: 0x000751BC
	private void RemoveClones()
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name.Contains("(Clone)"))
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x00076E04 File Offset: 0x00075204
	private void ReactivateEffect()
	{
		this.characterInstance.SetActive(false);
		this.characterInstance.SetActive(true);
		this.modelInstance.SetActive(false);
		this.modelInstance.SetActive(true);
	}

	// Token: 0x0400157F RID: 5503
	public GameObject Character;

	// Token: 0x04001580 RID: 5504
	public GameObject Model;

	// Token: 0x04001581 RID: 5505
	public int Current;

	// Token: 0x04001582 RID: 5506
	public GameObject[] Prefabs;

	// Token: 0x04001583 RID: 5507
	public Light Sun;

	// Token: 0x04001584 RID: 5508
	public ReflectionProbe ReflectionProbe;

	// Token: 0x04001585 RID: 5509
	public Light[] NightLights = new Light[0];

	// Token: 0x04001586 RID: 5510
	public Texture HUETexture;

	// Token: 0x04001587 RID: 5511
	public bool UseMobileVersion;

	// Token: 0x04001588 RID: 5512
	public GameObject MobileCharacter;

	// Token: 0x04001589 RID: 5513
	public GameObject Target;

	// Token: 0x0400158A RID: 5514
	public Color guiColor = Color.red;

	// Token: 0x0400158B RID: 5515
	private int currentNomber;

	// Token: 0x0400158C RID: 5516
	private GameObject characterInstance;

	// Token: 0x0400158D RID: 5517
	private GameObject modelInstance;

	// Token: 0x0400158E RID: 5518
	private GUIStyle guiStyleHeader = new GUIStyle();

	// Token: 0x0400158F RID: 5519
	private GUIStyle guiStyleHeaderMobile = new GUIStyle();

	// Token: 0x04001590 RID: 5520
	private float dpiScale;

	// Token: 0x04001591 RID: 5521
	private bool isDay;

	// Token: 0x04001592 RID: 5522
	private float colorHUE;

	// Token: 0x04001593 RID: 5523
	private float startSunIntensity;

	// Token: 0x04001594 RID: 5524
	private Quaternion startSunRotation;

	// Token: 0x04001595 RID: 5525
	private Color startAmbientLight;

	// Token: 0x04001596 RID: 5526
	private float startAmbientIntencity;

	// Token: 0x04001597 RID: 5527
	private float startReflectionIntencity;

	// Token: 0x04001598 RID: 5528
	private LightShadows startLightShadows;

	// Token: 0x04001599 RID: 5529
	private bool isButtonPressed;

	// Token: 0x0400159A RID: 5530
	private GameObject instanceShieldProjectile;
}
