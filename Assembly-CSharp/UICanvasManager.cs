using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000418 RID: 1048
public class UICanvasManager : MonoBehaviour
{
	// Token: 0x06001318 RID: 4888 RVA: 0x000757F4 File Offset: 0x00073BF4
	private void Awake()
	{
		UICanvasManager.GlobalAccess = this;
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000757FC File Offset: 0x00073BFC
	private void Start()
	{
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x00075824 File Offset: 0x00073C24
	private void Update()
	{
		if (!this.MouseOverButton && Input.GetMouseButtonUp(0))
		{
			this.SpawnCurrentParticleEffect();
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			this.SelectPreviousPE();
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			this.SelectNextPE();
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x00075874 File Offset: 0x00073C74
	public void UpdateToolTip(ButtonTypes toolTipType)
	{
		if (this.ToolTipText != null)
		{
			if (toolTipType == ButtonTypes.Previous)
			{
				this.ToolTipText.text = "Select Previous Particle Effect";
			}
			else if (toolTipType == ButtonTypes.Next)
			{
				this.ToolTipText.text = "Select Next Particle Effect";
			}
		}
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x000758C5 File Offset: 0x00073CC5
	public void ClearToolTip()
	{
		if (this.ToolTipText != null)
		{
			this.ToolTipText.text = string.Empty;
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x000758E8 File Offset: 0x00073CE8
	private void SelectPreviousPE()
	{
		ParticleEffectsLibrary.GlobalAccess.PreviousParticleEffect();
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x0007591A File Offset: 0x00073D1A
	private void SelectNextPE()
	{
		ParticleEffectsLibrary.GlobalAccess.NextParticleEffect();
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x0007594C File Offset: 0x00073D4C
	private void SpawnCurrentParticleEffect()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out this.rayHit))
		{
			ParticleEffectsLibrary.GlobalAccess.SpawnParticleEffect(this.rayHit.point);
		}
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x0007598F File Offset: 0x00073D8F
	public void UIButtonClick(ButtonTypes buttonTypeClicked)
	{
		if (buttonTypeClicked != ButtonTypes.Previous)
		{
			if (buttonTypeClicked == ButtonTypes.Next)
			{
				this.SelectNextPE();
			}
		}
		else
		{
			this.SelectPreviousPE();
		}
	}

	// Token: 0x04001546 RID: 5446
	public static UICanvasManager GlobalAccess;

	// Token: 0x04001547 RID: 5447
	public bool MouseOverButton;

	// Token: 0x04001548 RID: 5448
	public Text PENameText;

	// Token: 0x04001549 RID: 5449
	public Text ToolTipText;

	// Token: 0x0400154A RID: 5450
	private RaycastHit rayHit;
}
