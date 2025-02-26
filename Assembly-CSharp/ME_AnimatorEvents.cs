using System;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class ME_AnimatorEvents : MonoBehaviour
{
	// Token: 0x0600137B RID: 4987 RVA: 0x00078104 File Offset: 0x00076504
	private void Start()
	{
		if (this.SwordInstance != null)
		{
			UnityEngine.Object.Destroy(this.SwordInstance);
		}
		this.SwordInstance = UnityEngine.Object.Instantiate<GameObject>(this.SwordPrefab, this.StartSwordPosition.position, this.StartSwordPosition.rotation);
		this.SwordInstance.transform.parent = this.StartSwordPosition.transform;
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x00078170 File Offset: 0x00076570
	public void ActivateEffect()
	{
		if (this.EffectPrefab == null || this.SwordInstance == null)
		{
			return;
		}
		if (this.EffectInstance != null)
		{
			UnityEngine.Object.Destroy(this.EffectInstance);
		}
		this.EffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.EffectPrefab);
		this.EffectInstance.transform.parent = this.SwordInstance.transform;
		this.EffectInstance.transform.localPosition = Vector3.zero;
		this.EffectInstance.transform.localRotation = default(Quaternion);
		PSMeshRendererUpdater component = this.EffectInstance.GetComponent<PSMeshRendererUpdater>();
		component.UpdateMeshEffect(this.SwordInstance);
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x00078230 File Offset: 0x00076630
	public void ActivateSword()
	{
		this.SwordInstance.transform.parent = this.SwordPosition.transform;
		this.SwordInstance.transform.position = this.SwordPosition.position;
		this.SwordInstance.transform.rotation = this.SwordPosition.rotation;
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x00078290 File Offset: 0x00076690
	public void UpdateColor(float HUE)
	{
		if (this.EffectInstance == null)
		{
			return;
		}
		ME_EffectSettingColor me_EffectSettingColor = this.EffectInstance.GetComponent<ME_EffectSettingColor>();
		if (me_EffectSettingColor == null)
		{
			me_EffectSettingColor = this.EffectInstance.AddComponent<ME_EffectSettingColor>();
		}
		ME_ColorHelper.HSBColor hsbColor = ME_ColorHelper.ColorToHSV(me_EffectSettingColor.Color);
		hsbColor.H = HUE;
		me_EffectSettingColor.Color = ME_ColorHelper.HSVToColor(hsbColor);
	}

	// Token: 0x040015D9 RID: 5593
	public GameObject EffectPrefab;

	// Token: 0x040015DA RID: 5594
	public GameObject SwordPrefab;

	// Token: 0x040015DB RID: 5595
	public Transform SwordPosition;

	// Token: 0x040015DC RID: 5596
	public Transform StartSwordPosition;

	// Token: 0x040015DD RID: 5597
	private GameObject EffectInstance;

	// Token: 0x040015DE RID: 5598
	private GameObject SwordInstance;
}
