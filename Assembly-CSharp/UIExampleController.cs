using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005A7 RID: 1447
public class UIExampleController : MonoBehaviour
{
	// Token: 0x06002188 RID: 8584 RVA: 0x000B953C File Offset: 0x000B793C
	private void Start()
	{
		this.maskImages = new List<Image>();
		Mask[] array = UnityEngine.Object.FindObjectsOfType<Mask>();
		foreach (Mask mask in array)
		{
			this.maskImages.Add(mask.GetComponent<Image>());
		}
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x000B9585 File Offset: 0x000B7985
	private void Update()
	{
	}

	// Token: 0x0600218A RID: 8586 RVA: 0x000B9588 File Offset: 0x000B7988
	public void ToggleMask()
	{
		foreach (Image image in this.maskImages)
		{
			image.enabled = this.maskToggle.isOn;
		}
		this.maskBorder.enabled = this.maskToggle.isOn;
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x000B9604 File Offset: 0x000B7A04
	public void ChangeWindowType(int i)
	{
		this.window.sprite = this.windowSprites[i];
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x000B961C File Offset: 0x000B7A1C
	public void OnSlidersChanged()
	{
		this.mana.rectTransform.sizeDelta = new Vector2(this.mana.rectTransform.sizeDelta.x, this.manaSlider.value * 240f);
		this.health.rectTransform.sizeDelta = new Vector2(this.health.rectTransform.sizeDelta.x, this.healthSlider.value * 240f);
	}

	// Token: 0x0400211A RID: 8474
	public Toggle maskToggle;

	// Token: 0x0400211B RID: 8475
	public Image maskBorder;

	// Token: 0x0400211C RID: 8476
	public List<Image> maskImages;

	// Token: 0x0400211D RID: 8477
	public Sprite[] windowSprites;

	// Token: 0x0400211E RID: 8478
	public Image window;

	// Token: 0x0400211F RID: 8479
	public Image health;

	// Token: 0x04002120 RID: 8480
	public Image mana;

	// Token: 0x04002121 RID: 8481
	public Slider healthSlider;

	// Token: 0x04002122 RID: 8482
	public Slider manaSlider;
}
