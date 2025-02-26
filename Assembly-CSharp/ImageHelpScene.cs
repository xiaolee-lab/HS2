using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B40 RID: 2880
public class ImageHelpScene : MonoBehaviour
{
	// Token: 0x0600544F RID: 21583 RVA: 0x00252CE6 File Offset: 0x002510E6
	private void Start()
	{
	}

	// Token: 0x06005450 RID: 21584 RVA: 0x00252CE8 File Offset: 0x002510E8
	private void Update()
	{
	}

	// Token: 0x04004F1C RID: 20252
	[SerializeField]
	private GraphicRaycaster graphicRaycaster;

	// Token: 0x04004F1D RID: 20253
	[SerializeField]
	private Image imageHelp;

	// Token: 0x04004F1E RID: 20254
	[SerializeField]
	private Sprite[] sprite;

	// Token: 0x04004F1F RID: 20255
	private UnityAction action;

	// Token: 0x04004F20 RID: 20256
	public static ImageHelpScene.HelpKind kind;

	// Token: 0x04004F21 RID: 20257
	public static bool isAdd = true;

	// Token: 0x02000B41 RID: 2881
	public enum HelpKind
	{
		// Token: 0x04004F23 RID: 20259
		Custom,
		// Token: 0x04004F24 RID: 20260
		H,
		// Token: 0x04004F25 RID: 20261
		Game
	}
}
