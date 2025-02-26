using System;
using UnityEngine;

// Token: 0x020010E2 RID: 4322
public class EyeLookMaterialControll : MonoBehaviour
{
	// Token: 0x06008FA3 RID: 36771 RVA: 0x003BC7CC File Offset: 0x003BABCC
	private void Start()
	{
		this._material = base.GetComponent<Renderer>().material;
		Texture mainTexture = this._material.mainTexture;
		this.textureWidth = mainTexture.width;
		this.textureHeight = mainTexture.height;
		float num = 0f;
		this.rect.y = num;
		this.rect.x = num;
		this.rect.width = (float)this.textureWidth;
		this.rect.height = (float)this.textureHeight;
		this.offset = default(Vector2);
		this.scale = default(Vector2);
	}

	// Token: 0x06008FA4 RID: 36772 RVA: 0x003BC870 File Offset: 0x003BAC70
	private void Update()
	{
		this.rect.x = Mathf.Lerp((float)this.InsideWait, (float)this.OutsideWait, Mathf.InverseLerp(-1f, 1f, this.script.GetAngleHRate(this.eyeLR)));
		this.rect.y = Mathf.Lerp((float)this.DownWait, (float)this.UpWait, Mathf.InverseLerp(-1f, 1f, this.script.GetAngleVRate()));
		this.offset = new Vector2(Mathf.Clamp(this.rect.x / (float)this.textureWidth, this.InsideLimit, this.OutsideLimit), Mathf.Clamp(this.rect.y / (float)this.textureHeight, this.UpLimit, this.DownLimit));
		this.scale = new Vector2(this.rect.width / (float)this.textureWidth, this.rect.height / (float)this.textureHeight);
		this._material.SetTextureOffset("_MainTex", this.offset);
		this._material.SetTextureScale("_MainTex", this.scale);
	}

	// Token: 0x06008FA5 RID: 36773 RVA: 0x003BC9A3 File Offset: 0x003BADA3
	public Vector2 GetEyeTexOffset()
	{
		return this.offset;
	}

	// Token: 0x06008FA6 RID: 36774 RVA: 0x003BC9AB File Offset: 0x003BADAB
	public Vector2 GetEyeTexScale()
	{
		return this.scale;
	}

	// Token: 0x0400742E RID: 29742
	public EyeLookCalc script;

	// Token: 0x0400742F RID: 29743
	public int InsideWait;

	// Token: 0x04007430 RID: 29744
	public int OutsideWait;

	// Token: 0x04007431 RID: 29745
	public int UpWait;

	// Token: 0x04007432 RID: 29746
	public int DownWait;

	// Token: 0x04007433 RID: 29747
	public float InsideLimit;

	// Token: 0x04007434 RID: 29748
	public float OutsideLimit;

	// Token: 0x04007435 RID: 29749
	public float UpLimit;

	// Token: 0x04007436 RID: 29750
	public float DownLimit;

	// Token: 0x04007437 RID: 29751
	public EYE_LR eyeLR;

	// Token: 0x04007438 RID: 29752
	public Rect Limit;

	// Token: 0x04007439 RID: 29753
	public Rect rect;

	// Token: 0x0400743A RID: 29754
	private Material _material;

	// Token: 0x0400743B RID: 29755
	private int textureWidth;

	// Token: 0x0400743C RID: 29756
	private int textureHeight;

	// Token: 0x0400743D RID: 29757
	private Vector2 offset;

	// Token: 0x0400743E RID: 29758
	private Vector2 scale;
}
