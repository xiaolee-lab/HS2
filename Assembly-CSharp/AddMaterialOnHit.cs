using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public class AddMaterialOnHit : MonoBehaviour
{
	// Token: 0x060013EB RID: 5099 RVA: 0x0007C678 File Offset: 0x0007AA78
	private void Update()
	{
		if (this.EffectSettings == null)
		{
			return;
		}
		if (this.EffectSettings.IsVisible)
		{
			this.timeToDelete = 0f;
		}
		else
		{
			this.timeToDelete += Time.deltaTime;
			if (this.timeToDelete > this.RemoveAfterTime)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0007C6E8 File Offset: 0x0007AAE8
	public void UpdateMaterial(RaycastHit hit)
	{
		Transform transform = hit.transform;
		if (transform != null)
		{
			if (!this.RemoveWhenDisable)
			{
				UnityEngine.Object.Destroy(base.gameObject, this.RemoveAfterTime);
			}
			this.fadeInOutShaderColor = base.GetComponents<FadeInOutShaderColor>();
			this.fadeInOutShaderFloat = base.GetComponents<FadeInOutShaderFloat>();
			this.uvTextureAnimator = base.GetComponent<UVTextureAnimator>();
			this.renderParent = base.transform.parent.GetComponent<Renderer>();
			Material[] sharedMaterials = this.renderParent.sharedMaterials;
			int num = sharedMaterials.Length + 1;
			Material[] array = new Material[num];
			sharedMaterials.CopyTo(array, 0);
			this.renderParent.material = this.Material;
			this.instanceMat = this.renderParent.material;
			array[num - 1] = this.instanceMat;
			this.renderParent.sharedMaterials = array;
			if (this.UsePointMatrixTransform)
			{
				Matrix4x4 value = Matrix4x4.TRS(hit.transform.InverseTransformPoint(hit.point), Quaternion.Euler(180f, 180f, 0f), this.TransformScale);
				this.instanceMat.SetMatrix("_DecalMatr", value);
			}
			if (this.materialQueue != -1)
			{
				this.instanceMat.renderQueue = this.materialQueue;
			}
			if (this.fadeInOutShaderColor != null)
			{
				foreach (FadeInOutShaderColor fadeInOutShaderColor in this.fadeInOutShaderColor)
				{
					fadeInOutShaderColor.UpdateMaterial(this.instanceMat);
				}
			}
			if (this.fadeInOutShaderFloat != null)
			{
				foreach (FadeInOutShaderFloat fadeInOutShaderFloat in this.fadeInOutShaderFloat)
				{
					fadeInOutShaderFloat.UpdateMaterial(this.instanceMat);
				}
			}
			if (this.uvTextureAnimator != null)
			{
				this.uvTextureAnimator.SetInstanceMaterial(this.instanceMat, hit.textureCoord);
			}
		}
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x0007C8D0 File Offset: 0x0007ACD0
	public void UpdateMaterial(Transform transformTarget)
	{
		if (transformTarget != null)
		{
			if (!this.RemoveWhenDisable)
			{
				UnityEngine.Object.Destroy(base.gameObject, this.RemoveAfterTime);
			}
			this.fadeInOutShaderColor = base.GetComponents<FadeInOutShaderColor>();
			this.fadeInOutShaderFloat = base.GetComponents<FadeInOutShaderFloat>();
			this.uvTextureAnimator = base.GetComponent<UVTextureAnimator>();
			this.renderParent = base.transform.parent.GetComponent<Renderer>();
			Material[] sharedMaterials = this.renderParent.sharedMaterials;
			int num = sharedMaterials.Length + 1;
			Material[] array = new Material[num];
			sharedMaterials.CopyTo(array, 0);
			this.renderParent.material = this.Material;
			this.instanceMat = this.renderParent.material;
			array[num - 1] = this.instanceMat;
			this.renderParent.sharedMaterials = array;
			if (this.materialQueue != -1)
			{
				this.instanceMat.renderQueue = this.materialQueue;
			}
			if (this.fadeInOutShaderColor != null)
			{
				foreach (FadeInOutShaderColor fadeInOutShaderColor in this.fadeInOutShaderColor)
				{
					fadeInOutShaderColor.UpdateMaterial(this.instanceMat);
				}
			}
			if (this.fadeInOutShaderFloat != null)
			{
				foreach (FadeInOutShaderFloat fadeInOutShaderFloat in this.fadeInOutShaderFloat)
				{
					fadeInOutShaderFloat.UpdateMaterial(this.instanceMat);
				}
			}
			if (this.uvTextureAnimator != null)
			{
				this.uvTextureAnimator.SetInstanceMaterial(this.instanceMat, Vector2.zero);
			}
		}
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x0007CA5B File Offset: 0x0007AE5B
	public void SetMaterialQueue(int matlQueue)
	{
		this.materialQueue = matlQueue;
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0007CA64 File Offset: 0x0007AE64
	public int GetDefaultMaterialQueue()
	{
		return this.instanceMat.renderQueue;
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x0007CA74 File Offset: 0x0007AE74
	private void OnDestroy()
	{
		if (this.renderParent == null)
		{
			return;
		}
		List<Material> list = this.renderParent.sharedMaterials.ToList<Material>();
		list.Remove(this.instanceMat);
		this.renderParent.sharedMaterials = list.ToArray();
	}

	// Token: 0x0400167F RID: 5759
	public float RemoveAfterTime = 5f;

	// Token: 0x04001680 RID: 5760
	public bool RemoveWhenDisable;

	// Token: 0x04001681 RID: 5761
	public EffectSettings EffectSettings;

	// Token: 0x04001682 RID: 5762
	public Material Material;

	// Token: 0x04001683 RID: 5763
	public bool UsePointMatrixTransform;

	// Token: 0x04001684 RID: 5764
	public Vector3 TransformScale = Vector3.one;

	// Token: 0x04001685 RID: 5765
	private FadeInOutShaderColor[] fadeInOutShaderColor;

	// Token: 0x04001686 RID: 5766
	private FadeInOutShaderFloat[] fadeInOutShaderFloat;

	// Token: 0x04001687 RID: 5767
	private UVTextureAnimator uvTextureAnimator;

	// Token: 0x04001688 RID: 5768
	private Renderer renderParent;

	// Token: 0x04001689 RID: 5769
	private Material instanceMat;

	// Token: 0x0400168A RID: 5770
	private int materialQueue = -1;

	// Token: 0x0400168B RID: 5771
	private bool waitRemove;

	// Token: 0x0400168C RID: 5772
	private float timeToDelete;
}
