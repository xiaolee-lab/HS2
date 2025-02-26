using System;
using UnityEngine;

// Token: 0x0200043C RID: 1084
public class MyGUI3_1 : MonoBehaviour
{
	// Token: 0x060013D8 RID: 5080 RVA: 0x0007B530 File Offset: 0x00079930
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
		this.oldAmbientColor = RenderSettings.ambientLight;
		this.oldLightIntensity = this.DirLight.intensity;
		this.guiStyleHeader.fontSize = (int)(15f * this.dpiScale);
		this.guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
		this.current = this.CurrentPrefabNomber;
		this.InstanceCurrent(this.GuiStats[this.CurrentPrefabNomber]);
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x0007B600 File Offset: 0x00079A00
	private void InstanceEffect(Vector3 pos)
	{
		this.currentGo = UnityEngine.Object.Instantiate<GameObject>(this.Prefabs[this.current], pos, this.Prefabs[this.current].transform.rotation);
		this.effectSettings = this.currentGo.GetComponent<EffectSettings>();
		this.effectSettings.Target = this.GetTargetObject(this.GuiStats[this.current]);
		this.effectSettings.EffectDeactivated += this.effectSettings_EffectDeactivated;
		if (this.GuiStats[this.current] == MyGUI3_1.GuiStat.Middle)
		{
			this.currentGo.transform.parent = this.GetTargetObject(MyGUI3_1.GuiStat.Middle).transform;
			this.currentGo.transform.position = this.GetInstancePosition(MyGUI3_1.GuiStat.Middle);
		}
		else
		{
			this.currentGo.transform.parent = base.transform;
		}
		this.effectSettings.CollisionEnter += delegate(object n, CollisionInfo e)
		{
			if (e.Hit.transform != null)
			{
			}
		};
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x0007B70C File Offset: 0x00079B0C
	private GameObject GetTargetObject(MyGUI3_1.GuiStat stat)
	{
		switch (stat)
		{
		case MyGUI3_1.GuiStat.Ball:
			return this.Target;
		case MyGUI3_1.GuiStat.BallRotate:
			return this.Target;
		case MyGUI3_1.GuiStat.Bottom:
			return this.BottomPosition;
		case MyGUI3_1.GuiStat.Middle:
			this.MiddlePosition.transform.localPosition = this.defaultRobotPos;
			this.MiddlePosition.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			return this.MiddlePosition;
		case MyGUI3_1.GuiStat.MiddleWithoutRobot:
			return this.MiddlePosition.transform.parent.gameObject;
		case MyGUI3_1.GuiStat.Top:
			return this.TopPosition;
		case MyGUI3_1.GuiStat.TopTarget:
			return this.BottomPosition;
		}
		return base.gameObject;
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x0007B7C4 File Offset: 0x00079BC4
	private void effectSettings_EffectDeactivated(object sender, EventArgs e)
	{
		if (this.GuiStats[this.current] != MyGUI3_1.GuiStat.Middle)
		{
			this.currentGo.transform.position = this.GetInstancePosition(this.GuiStats[this.current]);
		}
		this.isReadyEffect = true;
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x0007B804 File Offset: 0x00079C04
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f * this.dpiScale, 15f * this.dpiScale, 105f * this.dpiScale, 30f * this.dpiScale), "Previous Effect"))
		{
			this.ChangeCurrent(-1);
		}
		if (GUI.Button(new Rect(130f * this.dpiScale, 15f * this.dpiScale, 105f * this.dpiScale, 30f * this.dpiScale), "Next Effect"))
		{
			this.ChangeCurrent(1);
		}
		if (this.Prefabs[this.current] != null)
		{
			GUI.Label(new Rect(300f * this.dpiScale, 15f * this.dpiScale, 100f * this.dpiScale, 20f * this.dpiScale), "Prefab name is \"" + this.Prefabs[this.current].name + "\"  \r\nHold any mouse button that would move the camera", this.guiStyleHeader);
		}
		if (GUI.Button(new Rect(10f * this.dpiScale, 60f * this.dpiScale, 225f * this.dpiScale, 30f * this.dpiScale), "Day/Night"))
		{
			this.DirLight.intensity = (this.isDay ? this.oldLightIntensity : 0f);
			this.DirLight.transform.rotation = ((!this.isDay) ? Quaternion.Euler(350f, 30f, 90f) : Quaternion.Euler(400f, 30f, 90f));
			RenderSettings.ambientLight = (this.isDay ? this.oldAmbientColor : new Color(0.1f, 0.1f, 0.1f));
			RenderSettings.ambientIntensity = ((!this.isDay) ? 0.1f : 0.5f);
			RenderSettings.reflectionIntensity = ((!this.isDay) ? 0.1f : 1f);
			this.isDay = !this.isDay;
		}
		GUI.DrawTexture(new Rect(12f * this.dpiScale, 110f * this.dpiScale, 220f * this.dpiScale, 15f * this.dpiScale), this.HUETexture, ScaleMode.StretchToFill, false, 0f);
		float num = this.colorHUE;
		this.colorHUE = GUI.HorizontalSlider(new Rect(12f * this.dpiScale, 135f * this.dpiScale, 220f * this.dpiScale, 15f * this.dpiScale), this.colorHUE, 0f, 360f);
		if ((double)Mathf.Abs(num - this.colorHUE) > 0.001)
		{
			this.ChangeColor();
		}
		GUI.Label(new Rect(240f * this.dpiScale, 105f * this.dpiScale, 30f * this.dpiScale, 30f * this.dpiScale), "Effect color", this.guiStyleHeader);
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x0007BB54 File Offset: 0x00079F54
	private void Update()
	{
		if (this.isReadyEffect)
		{
			this.isReadyEffect = false;
			this.currentGo.SetActive(true);
		}
		if (this.GuiStats[this.current] == MyGUI3_1.GuiStat.BallRotate)
		{
			this.currentGo.transform.localRotation = Quaternion.Euler(0f, Mathf.PingPong(Time.time * 5f, 60f) - 50f, 0f);
		}
		if (this.GuiStats[this.current] == MyGUI3_1.GuiStat.BallRotatex4)
		{
			this.currentGo.transform.localRotation = Quaternion.Euler(0f, Mathf.PingPong(Time.time * 30f, 100f) - 70f, 0f);
		}
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x0007BC1C File Offset: 0x0007A01C
	private void InstanceCurrent(MyGUI3_1.GuiStat stat)
	{
		switch (stat)
		{
		case MyGUI3_1.GuiStat.Ball:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(base.transform.position);
			break;
		case MyGUI3_1.GuiStat.BallRotate:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(base.transform.position);
			break;
		case MyGUI3_1.GuiStat.BallRotatex4:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(base.transform.position);
			break;
		case MyGUI3_1.GuiStat.Bottom:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.BottomPosition.transform.position);
			break;
		case MyGUI3_1.GuiStat.Middle:
			this.MiddlePosition.SetActive(true);
			this.InstanceEffect(this.MiddlePosition.transform.parent.transform.position);
			break;
		case MyGUI3_1.GuiStat.MiddleWithoutRobot:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.MiddlePosition.transform.position);
			break;
		case MyGUI3_1.GuiStat.Top:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.TopPosition.transform.position);
			break;
		case MyGUI3_1.GuiStat.TopTarget:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.TopPosition.transform.position);
			break;
		}
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x0007BD88 File Offset: 0x0007A188
	private Vector3 GetInstancePosition(MyGUI3_1.GuiStat stat)
	{
		switch (stat)
		{
		case MyGUI3_1.GuiStat.Ball:
			return base.transform.position;
		case MyGUI3_1.GuiStat.BallRotate:
			return base.transform.position;
		case MyGUI3_1.GuiStat.BallRotatex4:
			return base.transform.position;
		case MyGUI3_1.GuiStat.Bottom:
			return this.BottomPosition.transform.position;
		case MyGUI3_1.GuiStat.Middle:
			return this.MiddlePosition.transform.parent.transform.position;
		case MyGUI3_1.GuiStat.MiddleWithoutRobot:
			return this.MiddlePosition.transform.parent.transform.position;
		case MyGUI3_1.GuiStat.Top:
			return this.TopPosition.transform.position;
		case MyGUI3_1.GuiStat.TopTarget:
			return this.TopPosition.transform.position;
		default:
			return base.transform.position;
		}
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x0007BE58 File Offset: 0x0007A258
	private void ChangeCurrent(int delta)
	{
		UnityEngine.Object.Destroy(this.currentGo);
		base.CancelInvoke("InstanceDefaulBall");
		this.current += delta;
		if (this.current > this.Prefabs.Length - 1)
		{
			this.current = 0;
		}
		else if (this.current < 0)
		{
			this.current = this.Prefabs.Length - 1;
		}
		if (this.effectSettings != null)
		{
			this.effectSettings.EffectDeactivated -= this.effectSettings_EffectDeactivated;
		}
		this.MiddlePosition.SetActive(this.GuiStats[this.current] == MyGUI3_1.GuiStat.Middle);
		this.InstanceEffect(this.GetInstancePosition(this.GuiStats[this.current]));
		if (this.TargetForRay != null)
		{
			if (this.current == 14 || this.current == 22)
			{
				this.TargetForRay.SetActive(true);
			}
			else
			{
				this.TargetForRay.SetActive(false);
			}
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x0007BF6C File Offset: 0x0007A36C
	private Color Hue(float H)
	{
		Color result = new Color(1f, 0f, 0f);
		if (H >= 0f && H < 1f)
		{
			result = new Color(1f, 0f, H);
		}
		if (H >= 1f && H < 2f)
		{
			result = new Color(2f - H, 0f, 1f);
		}
		if (H >= 2f && H < 3f)
		{
			result = new Color(0f, H - 2f, 1f);
		}
		if (H >= 3f && H < 4f)
		{
			result = new Color(0f, 1f, 4f - H);
		}
		if (H >= 4f && H < 5f)
		{
			result = new Color(H - 4f, 1f, 0f);
		}
		if (H >= 5f && H < 6f)
		{
			result = new Color(1f, 6f - H, 0f);
		}
		return result;
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x0007C0A0 File Offset: 0x0007A4A0
	public MyGUI3_1.HSBColor ColorToHSV(Color color)
	{
		MyGUI3_1.HSBColor result = new MyGUI3_1.HSBColor(0f, 0f, 0f, color.a);
		float r = color.r;
		float g = color.g;
		float b = color.b;
		float num = Mathf.Max(r, Mathf.Max(g, b));
		if (num <= 0f)
		{
			return result;
		}
		float num2 = Mathf.Min(r, Mathf.Min(g, b));
		float num3 = num - num2;
		if (num > num2)
		{
			if (g == num)
			{
				result.h = (b - r) / num3 * 60f + 120f;
			}
			else if (b == num)
			{
				result.h = (r - g) / num3 * 60f + 240f;
			}
			else if (b > g)
			{
				result.h = (g - b) / num3 * 60f + 360f;
			}
			else
			{
				result.h = (g - b) / num3 * 60f;
			}
			if (result.h < 0f)
			{
				result.h += 360f;
			}
		}
		else
		{
			result.h = 0f;
		}
		result.h *= 0.0027777778f;
		result.s = num3 / num * 1f;
		result.b = num;
		return result;
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x0007C208 File Offset: 0x0007A608
	public Color HSVToColor(MyGUI3_1.HSBColor hsbColor)
	{
		float value = hsbColor.b;
		float value2 = hsbColor.b;
		float value3 = hsbColor.b;
		if (hsbColor.s != 0f)
		{
			float b = hsbColor.b;
			float num = hsbColor.b * hsbColor.s;
			float num2 = hsbColor.b - num;
			float num3 = hsbColor.h * 360f;
			if (num3 < 60f)
			{
				value = b;
				value2 = num3 * num / 60f + num2;
				value3 = num2;
			}
			else if (num3 < 120f)
			{
				value = -(num3 - 120f) * num / 60f + num2;
				value2 = b;
				value3 = num2;
			}
			else if (num3 < 180f)
			{
				value = num2;
				value2 = b;
				value3 = (num3 - 120f) * num / 60f + num2;
			}
			else if (num3 < 240f)
			{
				value = num2;
				value2 = -(num3 - 240f) * num / 60f + num2;
				value3 = b;
			}
			else if (num3 < 300f)
			{
				value = (num3 - 240f) * num / 60f + num2;
				value2 = num2;
				value3 = b;
			}
			else if (num3 <= 360f)
			{
				value = b;
				value2 = num2;
				value3 = -(num3 - 360f) * num / 60f + num2;
			}
			else
			{
				value = 0f;
				value2 = 0f;
				value3 = 0f;
			}
		}
		return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.a);
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x0007C3A4 File Offset: 0x0007A7A4
	private void ChangeColor()
	{
		Color color = this.Hue(this.colorHUE / 255f);
		Renderer[] componentsInChildren = this.currentGo.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			Material material = renderer.material;
			if (!(material == null))
			{
				if (material.HasProperty("_TintColor"))
				{
					Color color2 = material.GetColor("_TintColor");
					MyGUI3_1.HSBColor hsbColor = this.ColorToHSV(color2);
					hsbColor.h = this.colorHUE / 360f;
					color = this.HSVToColor(hsbColor);
					color.a = color2.a;
					material.SetColor("_TintColor", color);
				}
				if (material.HasProperty("_CoreColor"))
				{
					Color color3 = material.GetColor("_CoreColor");
					MyGUI3_1.HSBColor hsbColor2 = this.ColorToHSV(color3);
					hsbColor2.h = this.colorHUE / 360f;
					color = this.HSVToColor(hsbColor2);
					color.a = color3.a;
					material.SetColor("_CoreColor", color);
				}
			}
		}
		Projector[] componentsInChildren2 = this.currentGo.GetComponentsInChildren<Projector>();
		foreach (Projector projector in componentsInChildren2)
		{
			Material material2 = projector.material;
			if (!(material2 == null) && material2.HasProperty("_TintColor"))
			{
				Color color4 = material2.GetColor("_TintColor");
				MyGUI3_1.HSBColor hsbColor3 = this.ColorToHSV(color4);
				hsbColor3.h = this.colorHUE / 360f;
				color = this.HSVToColor(hsbColor3);
				color.a = color4.a;
				projector.material.SetColor("_TintColor", color);
			}
		}
		Light componentInChildren = this.currentGo.GetComponentInChildren<Light>();
		if (componentInChildren != null)
		{
			componentInChildren.color = color;
		}
	}

	// Token: 0x04001654 RID: 5716
	public Texture HUETexture;

	// Token: 0x04001655 RID: 5717
	public int CurrentPrefabNomber;

	// Token: 0x04001656 RID: 5718
	public float UpdateInterval = 0.5f;

	// Token: 0x04001657 RID: 5719
	public Light DirLight;

	// Token: 0x04001658 RID: 5720
	public GameObject Target;

	// Token: 0x04001659 RID: 5721
	public GameObject TargetForRay;

	// Token: 0x0400165A RID: 5722
	public GameObject TopPosition;

	// Token: 0x0400165B RID: 5723
	public GameObject MiddlePosition;

	// Token: 0x0400165C RID: 5724
	public Vector3 defaultRobotPos;

	// Token: 0x0400165D RID: 5725
	public GameObject BottomPosition;

	// Token: 0x0400165E RID: 5726
	public GameObject Plane1;

	// Token: 0x0400165F RID: 5727
	public GameObject Plane2;

	// Token: 0x04001660 RID: 5728
	public Material[] PlaneMaterials;

	// Token: 0x04001661 RID: 5729
	public MyGUI3_1.GuiStat[] GuiStats;

	// Token: 0x04001662 RID: 5730
	public GameObject[] Prefabs;

	// Token: 0x04001663 RID: 5731
	private float oldLightIntensity;

	// Token: 0x04001664 RID: 5732
	private Color oldAmbientColor;

	// Token: 0x04001665 RID: 5733
	private GameObject currentGo;

	// Token: 0x04001666 RID: 5734
	private bool isDay;

	// Token: 0x04001667 RID: 5735
	private bool isDefaultPlaneTexture;

	// Token: 0x04001668 RID: 5736
	private int current;

	// Token: 0x04001669 RID: 5737
	private EffectSettings effectSettings;

	// Token: 0x0400166A RID: 5738
	private bool isReadyEffect;

	// Token: 0x0400166B RID: 5739
	private Quaternion defaultRobotRotation;

	// Token: 0x0400166C RID: 5740
	private float colorHUE;

	// Token: 0x0400166D RID: 5741
	private GUIStyle guiStyleHeader = new GUIStyle();

	// Token: 0x0400166E RID: 5742
	private float dpiScale;

	// Token: 0x0200043D RID: 1085
	public enum GuiStat
	{
		// Token: 0x04001671 RID: 5745
		Ball,
		// Token: 0x04001672 RID: 5746
		BallRotate,
		// Token: 0x04001673 RID: 5747
		BallRotatex4,
		// Token: 0x04001674 RID: 5748
		Bottom,
		// Token: 0x04001675 RID: 5749
		Middle,
		// Token: 0x04001676 RID: 5750
		MiddleWithoutRobot,
		// Token: 0x04001677 RID: 5751
		Top,
		// Token: 0x04001678 RID: 5752
		TopTarget
	}

	// Token: 0x0200043E RID: 1086
	public struct HSBColor
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x0007C5B4 File Offset: 0x0007A9B4
		public HSBColor(float h, float s, float b, float a)
		{
			this.h = h;
			this.s = s;
			this.b = b;
			this.a = a;
		}

		// Token: 0x04001679 RID: 5753
		public float h;

		// Token: 0x0400167A RID: 5754
		public float s;

		// Token: 0x0400167B RID: 5755
		public float b;

		// Token: 0x0400167C RID: 5756
		public float a;
	}
}
