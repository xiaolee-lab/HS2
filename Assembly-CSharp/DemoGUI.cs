using System;
using UnityEngine;

// Token: 0x0200043B RID: 1083
public class DemoGUI : MonoBehaviour
{
	// Token: 0x060013D2 RID: 5074 RVA: 0x0007AF60 File Offset: 0x00079360
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
		this.guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
		this.currentInstance = UnityEngine.Object.Instantiate<GameObject>(this.Prefabs[this.currentNomber], base.transform.position, default(Quaternion));
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x0007B020 File Offset: 0x00079420
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
		GUI.Label(new Rect(300f * this.dpiScale, 15f * this.dpiScale, 100f * this.dpiScale, 20f * this.dpiScale), "Prefab name is \"" + this.Prefabs[this.currentNomber].name + "\"  \r\nHold any mouse button that would move the camera", this.guiStyleHeader);
		GUI.DrawTexture(new Rect(12f * this.dpiScale, 80f * this.dpiScale, 220f * this.dpiScale, 15f * this.dpiScale), this.HUETexture, ScaleMode.StretchToFill, false, 0f);
		float num = this.colorHUE;
		this.colorHUE = GUI.HorizontalSlider(new Rect(12f * this.dpiScale, 105f * this.dpiScale, 220f * this.dpiScale, 15f * this.dpiScale), this.colorHUE, 0f, 1530f);
		if ((double)Mathf.Abs(num - this.colorHUE) > 0.001)
		{
			this.ChangeColor();
		}
		GUI.Label(new Rect(240f * this.dpiScale, 105f * this.dpiScale, 30f * this.dpiScale, 30f * this.dpiScale), "Effect color", this.guiStyleHeader);
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x0007B22C File Offset: 0x0007962C
	private void ChangeColor()
	{
		Color color = this.Hue(this.colorHUE / 255f);
		Renderer[] componentsInChildren = this.currentInstance.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			Material material = renderer.material;
			if (!(material == null) && material.HasProperty("_TintColor"))
			{
				color.a = material.GetColor("_TintColor").a;
				material.SetColor("_TintColor", color);
			}
		}
		Light componentInChildren = this.currentInstance.GetComponentInChildren<Light>();
		if (componentInChildren != null)
		{
			componentInChildren.color = color;
		}
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x0007B2F0 File Offset: 0x000796F0
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

	// Token: 0x060013D6 RID: 5078 RVA: 0x0007B424 File Offset: 0x00079824
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
		if (this.currentInstance != null)
		{
			UnityEngine.Object.Destroy(this.currentInstance);
		}
		Vector3 position = base.transform.position;
		if (this.Positions[this.currentNomber] == Position.Bottom)
		{
			position.y -= 1f;
		}
		if (this.Positions[this.currentNomber] == Position.Bottom02)
		{
			position.y -= 0.8f;
		}
		this.currentInstance = UnityEngine.Object.Instantiate<GameObject>(this.Prefabs[this.currentNomber], position, default(Quaternion));
	}

	// Token: 0x0400164B RID: 5707
	public Texture HUETexture;

	// Token: 0x0400164C RID: 5708
	public Material mat;

	// Token: 0x0400164D RID: 5709
	public Position[] Positions;

	// Token: 0x0400164E RID: 5710
	public GameObject[] Prefabs;

	// Token: 0x0400164F RID: 5711
	private int currentNomber;

	// Token: 0x04001650 RID: 5712
	private GameObject currentInstance;

	// Token: 0x04001651 RID: 5713
	private GUIStyle guiStyleHeader = new GUIStyle();

	// Token: 0x04001652 RID: 5714
	private float colorHUE;

	// Token: 0x04001653 RID: 5715
	private float dpiScale;
}
