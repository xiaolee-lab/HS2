using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CinematicEffects;

// Token: 0x0200060E RID: 1550
public class ExamplesController : MonoBehaviour
{
	// Token: 0x060024F9 RID: 9465 RVA: 0x000D18BC File Offset: 0x000CFCBC
	public void Start()
	{
		RenderSettings.skybox = this.skyboxMaterial1;
		this.realTimeLight1.SetActive(true);
		this.realTimeLight2.SetActive(false);
		this.realTimeLight3.SetActive(false);
		RenderSettings.customReflection = this.reflectionCubemap1;
		RenderSettings.reflectionIntensity = this.exposure1;
		DynamicGI.UpdateEnvironment();
		this.skyboxSphereActive = this.skyboxSphere1;
		this.currentTargetIndex = 0;
		this.PrepareCurrentObject();
		for (int i = 1; i < this.objectsParams.Length; i++)
		{
			this.objectsParams[i].target.SetActive(false);
		}
		this.hideTime = Time.time + this.hideTimeDelay;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x000D196A File Offset: 0x000CFD6A
	public void ClickedAutoRotation()
	{
		this.autoRotation = !this.autoRotation;
		this.autorotateButtonOn.SetActive(this.autoRotation);
		this.autorotateButtonOff.SetActive(!this.autoRotation);
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x000D19A0 File Offset: 0x000CFDA0
	public void ClickedArrow(bool rightFlag)
	{
		this.objectsParams[this.currentTargetIndex].target.transform.rotation = Quaternion.identity;
		this.objectsParams[this.currentTargetIndex].target.SetActive(false);
		if (this.currentRenderer != null && this.originalMaterial != null)
		{
			Material[] sharedMaterials = this.currentRenderer.sharedMaterials;
			sharedMaterials[this.objectsParams[this.currentTargetIndex].submeshIndex] = this.originalMaterial;
			this.currentRenderer.sharedMaterials = sharedMaterials;
			UnityEngine.Object.Destroy(this.currentMaterial);
		}
		if (rightFlag)
		{
			this.currentTargetIndex = (this.currentTargetIndex + 1) % this.objectsParams.Length;
		}
		else
		{
			this.currentTargetIndex = (this.currentTargetIndex + this.objectsParams.Length - 1) % this.objectsParams.Length;
		}
		this.PrepareCurrentObject();
		this.objectsParams[this.currentTargetIndex].target.SetActive(true);
		this.mouseOrbitController.target = this.objectsParams[this.currentTargetIndex].target;
		this.mouseOrbitController.targetFocus = this.objectsParams[this.currentTargetIndex].target.transform.Find("Focus");
		this.mouseOrbitController.Reset();
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x000D1AFC File Offset: 0x000CFEFC
	public void Update()
	{
		this.skyboxSphereActive.transform.Rotate(Vector3.up, Time.deltaTime * 200f, Space.World);
		if (this.objectsParams[this.currentTargetIndex].Title == "Ice block" && Input.GetKeyDown(KeyCode.L))
		{
			GameObject gameObject = this.objectsParams[this.currentTargetIndex].target.transform.Find("Amber").gameObject;
			gameObject.SetActive(!gameObject.activeSelf);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.ClickedArrow(true);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.ClickedArrow(false);
		}
		if (this.autoRotation)
		{
			this.objectsParams[this.currentTargetIndex].target.transform.Rotate(Vector3.up, Time.deltaTime * this.autoRotationSpeed, Space.World);
		}
		if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
		{
			this.hideTime = Time.time + this.hideTimeDelay;
			this.InteractiveUI.SetActive(true);
		}
		if (Time.time > this.hideTime)
		{
			this.InteractiveUI.SetActive(false);
		}
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x000D1C5C File Offset: 0x000D005C
	public void ButtonPressed(Button button)
	{
		RectTransform component = button.GetComponent<RectTransform>();
		Vector3 v = component.anchoredPosition;
		v.x += 2f;
		v.y -= 2f;
		component.anchoredPosition = v;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000D1CB0 File Offset: 0x000D00B0
	public void ButtonReleased(Button button)
	{
		RectTransform component = button.GetComponent<RectTransform>();
		Vector3 v = component.anchoredPosition;
		v.x -= 2f;
		v.y += 2f;
		component.anchoredPosition = v;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x000D1D04 File Offset: 0x000D0104
	public void ButtonEnterScale(Button button)
	{
		RectTransform component = button.GetComponent<RectTransform>();
		component.localScale = new Vector3(1.1f, 1.1f, 1.1f);
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x000D1D34 File Offset: 0x000D0134
	public void ButtonLeaveScale(Button button)
	{
		RectTransform component = button.GetComponent<RectTransform>();
		component.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x000D1D64 File Offset: 0x000D0164
	public void SliderChanged(Slider slider)
	{
		this.mouseOrbitController.disableSteering = true;
		if (this.objectsParams[this.currentTargetIndex].materialProperty == "fallIntensity")
		{
			UBER_GlobalParams component = this.mainCamera.GetComponent<UBER_GlobalParams>();
			component.fallIntensity = slider.value;
		}
		else if (this.objectsParams[this.currentTargetIndex].materialProperty == "_SnowColorAndCoverage")
		{
			Color color = this.currentMaterial.GetColor("_SnowColorAndCoverage");
			color.a = slider.value;
			this.currentMaterial.SetColor("_SnowColorAndCoverage", color);
			slider.wholeNumbers = false;
		}
		else if (this.objectsParams[this.currentTargetIndex].materialProperty == "SPECIAL_Tiling")
		{
			this.currentMaterial.SetTextureScale("_MainTex", new Vector2(slider.value, slider.value));
			slider.wholeNumbers = true;
		}
		else
		{
			this.currentMaterial.SetFloat(this.objectsParams[this.currentTargetIndex].materialProperty, slider.value);
			slider.wholeNumbers = false;
		}
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x000D1E90 File Offset: 0x000D0290
	public void ExposureChanged(Slider slider)
	{
		TonemappingColorGrading component = this.mainCamera.gameObject.GetComponent<TonemappingColorGrading>();
		TonemappingColorGrading.TonemappingSettings tonemapping = component.tonemapping;
		tonemapping.exposure = slider.value;
		component.tonemapping = tonemapping;
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x000D1ECC File Offset: 0x000D02CC
	public void ClickedSkybox1()
	{
		this.skyboxSphereActive.transform.rotation = Quaternion.identity;
		Renderer componentInChildren = this.skyboxSphereActive.GetComponentInChildren<Renderer>();
		componentInChildren.sharedMaterial = this.skyboxSphereMaterialInactive;
		this.skyboxSphereActive = this.skyboxSphere1;
		componentInChildren = this.skyboxSphereActive.GetComponentInChildren<Renderer>();
		componentInChildren.sharedMaterial = this.skyboxSphereMaterialActive;
		RenderSettings.customReflection = this.reflectionCubemap1;
		RenderSettings.reflectionIntensity = this.exposure1;
		RenderSettings.skybox = this.skyboxMaterial1;
		this.realTimeLight1.SetActive(true);
		this.realTimeLight2.SetActive(false);
		this.realTimeLight3.SetActive(false);
		DynamicGI.UpdateEnvironment();
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x000D1F74 File Offset: 0x000D0374
	public void ClickedSkybox2()
	{
		this.skyboxSphereActive.transform.rotation = Quaternion.identity;
		Renderer componentInChildren = this.skyboxSphereActive.GetComponentInChildren<Renderer>();
		componentInChildren.sharedMaterial = this.skyboxSphereMaterialInactive;
		this.skyboxSphereActive = this.skyboxSphere2;
		componentInChildren = this.skyboxSphereActive.GetComponentInChildren<Renderer>();
		componentInChildren.sharedMaterial = this.skyboxSphereMaterialActive;
		RenderSettings.customReflection = this.reflectionCubemap2;
		RenderSettings.reflectionIntensity = this.exposure2;
		RenderSettings.skybox = this.skyboxMaterial2;
		this.realTimeLight1.SetActive(false);
		this.realTimeLight2.SetActive(true);
		this.realTimeLight3.SetActive(false);
		DynamicGI.UpdateEnvironment();
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x000D201C File Offset: 0x000D041C
	public void ClickedSkybox3()
	{
		this.skyboxSphereActive.transform.rotation = Quaternion.identity;
		Renderer componentInChildren = this.skyboxSphereActive.GetComponentInChildren<Renderer>();
		componentInChildren.sharedMaterial = this.skyboxSphereMaterialInactive;
		this.skyboxSphereActive = this.skyboxSphere3;
		componentInChildren = this.skyboxSphereActive.GetComponentInChildren<Renderer>();
		componentInChildren.sharedMaterial = this.skyboxSphereMaterialActive;
		RenderSettings.customReflection = this.reflectionCubemap3;
		RenderSettings.reflectionIntensity = this.exposure3;
		RenderSettings.skybox = this.skyboxMaterial3;
		this.realTimeLight1.SetActive(false);
		this.realTimeLight2.SetActive(false);
		this.realTimeLight3.SetActive(true);
		DynamicGI.UpdateEnvironment();
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x000D20C4 File Offset: 0x000D04C4
	public void TogglePostFX()
	{
		TonemappingColorGrading component = this.mainCamera.gameObject.GetComponent<TonemappingColorGrading>();
		this.togglepostFXButtonOn.SetActive(!component.enabled);
		this.togglepostFXButtonOff.SetActive(component.enabled);
		this.exposureSlider.interactable = !component.enabled;
		component.enabled = !component.enabled;
		Bloom component2 = this.mainCamera.gameObject.GetComponent<Bloom>();
		component2.enabled = component.enabled;
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x000D2148 File Offset: 0x000D0548
	public void SetTemperatureSun()
	{
		ColorBlock colors = this.buttonSun.colors;
		colors.normalColor = new Color(1f, 1f, 1f, 0.7f);
		this.buttonSun.colors = colors;
		colors = this.buttonFrost.colors;
		colors.normalColor = new Color(1f, 1f, 1f, 0.2f);
		this.buttonFrost.colors = colors;
		UBER_GlobalParams component = this.mainCamera.GetComponent<UBER_GlobalParams>();
		component.temperature = 20f;
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x000D21DC File Offset: 0x000D05DC
	public void SetTemperatureFrost()
	{
		ColorBlock colors = this.buttonSun.colors;
		colors.normalColor = new Color(1f, 1f, 1f, 0.2f);
		this.buttonSun.colors = colors;
		colors = this.buttonFrost.colors;
		colors.normalColor = new Color(1f, 1f, 1f, 0.7f);
		this.buttonFrost.colors = colors;
		UBER_GlobalParams component = this.mainCamera.GetComponent<UBER_GlobalParams>();
		component.temperature = -20f;
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000D2270 File Offset: 0x000D0670
	private void PrepareCurrentObject()
	{
		this.currentRenderer = this.objectsParams[this.currentTargetIndex].renderer;
		if (this.currentRenderer)
		{
			this.originalMaterial = this.currentRenderer.sharedMaterials[this.objectsParams[this.currentTargetIndex].submeshIndex];
			this.currentMaterial = UnityEngine.Object.Instantiate<Material>(this.originalMaterial);
			Material[] sharedMaterials = this.currentRenderer.sharedMaterials;
			sharedMaterials[this.objectsParams[this.currentTargetIndex].submeshIndex] = this.currentMaterial;
			this.currentRenderer.sharedMaterials = sharedMaterials;
		}
		bool flag = this.objectsParams[this.currentTargetIndex].materialProperty == null || this.objectsParams[this.currentTargetIndex].materialProperty == string.Empty;
		if (flag)
		{
			this.materialSlider.gameObject.SetActive(false);
		}
		else
		{
			this.materialSlider.gameObject.SetActive(true);
			this.materialSlider.minValue = this.objectsParams[this.currentTargetIndex].SliderRange.x;
			this.materialSlider.maxValue = this.objectsParams[this.currentTargetIndex].SliderRange.y;
			if (this.objectsParams[this.currentTargetIndex].materialProperty == "fallIntensity")
			{
				UBER_GlobalParams component = this.mainCamera.GetComponent<UBER_GlobalParams>();
				this.materialSlider.value = component.fallIntensity;
				component.UseParticleSystem = true;
				this.buttonSun.gameObject.SetActive(true);
				this.buttonFrost.gameObject.SetActive(true);
			}
			else
			{
				UBER_GlobalParams component2 = this.mainCamera.GetComponent<UBER_GlobalParams>();
				component2.UseParticleSystem = false;
				this.buttonSun.gameObject.SetActive(false);
				this.buttonFrost.gameObject.SetActive(false);
				if (this.originalMaterial.HasProperty(this.objectsParams[this.currentTargetIndex].materialProperty))
				{
					if (this.objectsParams[this.currentTargetIndex].materialProperty == "_SnowColorAndCoverage")
					{
						Color color = this.originalMaterial.GetColor("_SnowColorAndCoverage");
						this.materialSlider.value = color.a;
					}
					else
					{
						this.materialSlider.value = this.originalMaterial.GetFloat(this.objectsParams[this.currentTargetIndex].materialProperty);
					}
				}
				else if (this.objectsParams[this.currentTargetIndex].materialProperty == "SPECIAL_Tiling")
				{
					this.materialSlider.value = 1f;
				}
			}
		}
		this.titleTextArea.text = this.objectsParams[this.currentTargetIndex].Title;
		this.descriptionTextArea.text = this.objectsParams[this.currentTargetIndex].Description;
		this.matParamTextArea.text = this.objectsParams[this.currentTargetIndex].MatParamName;
		Vector2 anchoredPosition = this.titleTextArea.rectTransform.anchoredPosition;
		anchoredPosition.y = (float)((!flag) ? 110 : 50) + this.descriptionTextArea.preferredHeight;
		this.titleTextArea.rectTransform.anchoredPosition = anchoredPosition;
	}

	// Token: 0x04002407 RID: 9223
	public UBER_ExampleObjectParams[] objectsParams;

	// Token: 0x04002408 RID: 9224
	public Camera mainCamera;

	// Token: 0x04002409 RID: 9225
	public UBER_MouseOrbit_DynamicDistance mouseOrbitController;

	// Token: 0x0400240A RID: 9226
	public GameObject InteractiveUI;

	// Token: 0x0400240B RID: 9227
	[Space(10f)]
	public GameObject autorotateButtonOn;

	// Token: 0x0400240C RID: 9228
	public GameObject autorotateButtonOff;

	// Token: 0x0400240D RID: 9229
	public GameObject togglepostFXButtonOn;

	// Token: 0x0400240E RID: 9230
	public GameObject togglepostFXButtonOff;

	// Token: 0x0400240F RID: 9231
	public float autoRotationSpeed = 30f;

	// Token: 0x04002410 RID: 9232
	public bool autoRotation = true;

	// Token: 0x04002411 RID: 9233
	[Space(10f)]
	public GameObject skyboxSphere1;

	// Token: 0x04002412 RID: 9234
	public Cubemap reflectionCubemap1;

	// Token: 0x04002413 RID: 9235
	[Range(0f, 1f)]
	public float exposure1 = 1f;

	// Token: 0x04002414 RID: 9236
	public GameObject realTimeLight1;

	// Token: 0x04002415 RID: 9237
	public Material skyboxMaterial1;

	// Token: 0x04002416 RID: 9238
	public GameObject skyboxSphere2;

	// Token: 0x04002417 RID: 9239
	public Cubemap reflectionCubemap2;

	// Token: 0x04002418 RID: 9240
	[Range(0f, 1f)]
	public float exposure2 = 1f;

	// Token: 0x04002419 RID: 9241
	public GameObject realTimeLight2;

	// Token: 0x0400241A RID: 9242
	public Material skyboxMaterial2;

	// Token: 0x0400241B RID: 9243
	public GameObject skyboxSphere3;

	// Token: 0x0400241C RID: 9244
	public Cubemap reflectionCubemap3;

	// Token: 0x0400241D RID: 9245
	[Range(0f, 1f)]
	public float exposure3 = 1f;

	// Token: 0x0400241E RID: 9246
	public GameObject realTimeLight3;

	// Token: 0x0400241F RID: 9247
	public Material skyboxMaterial3;

	// Token: 0x04002420 RID: 9248
	public Material skyboxSphereMaterialActive;

	// Token: 0x04002421 RID: 9249
	public Material skyboxSphereMaterialInactive;

	// Token: 0x04002422 RID: 9250
	[Space(10f)]
	public Slider materialSlider;

	// Token: 0x04002423 RID: 9251
	public Slider exposureSlider;

	// Token: 0x04002424 RID: 9252
	public Text titleTextArea;

	// Token: 0x04002425 RID: 9253
	public Text descriptionTextArea;

	// Token: 0x04002426 RID: 9254
	public Text matParamTextArea;

	// Token: 0x04002427 RID: 9255
	[Space(10f)]
	public Button buttonSun;

	// Token: 0x04002428 RID: 9256
	public Button buttonFrost;

	// Token: 0x04002429 RID: 9257
	[Space(10f)]
	public float hideTimeDelay = 10f;

	// Token: 0x0400242A RID: 9258
	private MeshRenderer currentRenderer;

	// Token: 0x0400242B RID: 9259
	private Material currentMaterial;

	// Token: 0x0400242C RID: 9260
	private Material originalMaterial;

	// Token: 0x0400242D RID: 9261
	private float hideTime;

	// Token: 0x0400242E RID: 9262
	private int currentTargetIndex;

	// Token: 0x0400242F RID: 9263
	private GameObject skyboxSphereActive;
}
