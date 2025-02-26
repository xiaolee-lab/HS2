using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02001031 RID: 4145
internal class InteractableAlphaChanger : MonoBehaviour
{
	// Token: 0x06008AC5 RID: 35525 RVA: 0x003A52F8 File Offset: 0x003A36F8
	private void Awake()
	{
		if (this.flagToggle == null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
	}

	// Token: 0x06008AC6 RID: 35526 RVA: 0x003A5314 File Offset: 0x003A3714
	private void Start()
	{
		List<Color> baseTextMeshColor = (from t in this.targetTextMesh
		select t.color).ToList<Color>();
		List<Color> baseTextColor = (from t in this.targetText
		select t.color).ToList<Color>();
		List<Color> baseImageColor = (from t in this.targetImage
		select t.color).ToList<Color>();
		Color[] baseRawImageColor = (from t in this.targetRawImage
		select t.color).ToArray<Color>();
		BoolReactiveProperty isInteract = new BoolReactiveProperty(this.flagToggle.interactable);
		isInteract.Subscribe(delegate(bool isOn)
		{
			ColorBlock colors = this.flagToggle.colors;
			List<Color> list = new List<Color>(baseTextMeshColor);
			List<Color> list2 = new List<Color>(baseTextColor);
			List<Color> list3 = new List<Color>(baseImageColor);
			List<Color> list4 = new List<Color>(baseRawImageColor);
			if (!isOn)
			{
				for (int i = 0; i < this.targetTextMesh.Count; i++)
				{
					list[i] = new Color(Mathf.Clamp01(list[i].r * colors.disabledColor.r), Mathf.Clamp01(list[i].g * colors.disabledColor.g), Mathf.Clamp01(list[i].b * colors.disabledColor.b), Mathf.Clamp01(list[i].a * colors.disabledColor.a));
				}
				for (int j = 0; j < this.targetText.Count; j++)
				{
					list2[j] = new Color(Mathf.Clamp01(list2[j].r * colors.disabledColor.r), Mathf.Clamp01(list2[j].g * colors.disabledColor.g), Mathf.Clamp01(list2[j].b * colors.disabledColor.b), Mathf.Clamp01(list2[j].a * colors.disabledColor.a));
				}
				for (int k = 0; k < this.targetImage.Count; k++)
				{
					list3[k] = new Color(Mathf.Clamp01(list3[k].r * colors.disabledColor.r), Mathf.Clamp01(list3[k].g * colors.disabledColor.g), Mathf.Clamp01(list3[k].b * colors.disabledColor.b), Mathf.Clamp01(list3[k].a * colors.disabledColor.a));
				}
				for (int l = 0; l < this.targetRawImage.Count; l++)
				{
					list4[l] = new Color(Mathf.Clamp01(list4[l].r * colors.disabledColor.r), Mathf.Clamp01(list4[l].g * colors.disabledColor.g), Mathf.Clamp01(list4[l].b * colors.disabledColor.b), Mathf.Clamp01(list4[l].a * colors.disabledColor.a));
				}
			}
			for (int m = 0; m < this.targetTextMesh.Count; m++)
			{
				this.targetTextMesh[m].color = list[m];
			}
			for (int n = 0; n < this.targetText.Count; n++)
			{
				this.targetText[n].color = list2[n];
			}
			for (int num = 0; num < this.targetImage.Count; num++)
			{
				this.targetImage[num].color = list3[num];
			}
			for (int num2 = 0; num2 < this.targetRawImage.Count; num2++)
			{
				this.targetRawImage[num2].color = list4[num2];
			}
		});
		this.OnEnableAsObservable().Subscribe(delegate(Unit _)
		{
			isInteract.Value = this.flagToggle.interactable;
		});
		(from _ in this.UpdateAsObservable()
		select this.flagToggle.interactable).DistinctUntilChanged<bool>().Subscribe(delegate(bool interactable)
		{
			isInteract.Value = interactable;
		});
	}

	// Token: 0x04007139 RID: 28985
	[Header("Interactable参照用トグル")]
	[SerializeField]
	private Toggle flagToggle;

	// Token: 0x0400713A RID: 28986
	[Header("カラー変更対象TextMesh")]
	[SerializeField]
	private List<TextMeshProUGUI> targetTextMesh = new List<TextMeshProUGUI>();

	// Token: 0x0400713B RID: 28987
	[Header("カラー変更対象Text")]
	[SerializeField]
	private List<Text> targetText = new List<Text>();

	// Token: 0x0400713C RID: 28988
	[Header("カラー変更対象Image")]
	[SerializeField]
	private List<Image> targetImage = new List<Image>();

	// Token: 0x0400713D RID: 28989
	[Header("カラー変更対象RawImage")]
	[SerializeField]
	private List<RawImage> targetRawImage = new List<RawImage>();
}
