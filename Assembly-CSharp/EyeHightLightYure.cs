using System;
using UnityEngine;

// Token: 0x020010DB RID: 4315
public class EyeHightLightYure : MonoBehaviour
{
	// Token: 0x06008F89 RID: 36745 RVA: 0x003BB158 File Offset: 0x003B9558
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
		this.offset = new Vector2(0f, 0f);
		this.scale = new Vector2(1f, 1f);
	}

	// Token: 0x06008F8A RID: 36746 RVA: 0x003BB208 File Offset: 0x003B9608
	private void FixedUpdate()
	{
		if (this.eyeLookMaterialCtrl != null)
		{
			this.offset = this.eyeLookMaterialCtrl.GetEyeTexOffset();
		}
		this.rect.x = (float)UnityEngine.Random.Range(this.Inside, this.Outside);
		this.rect.y = (float)UnityEngine.Random.Range(this.Up, this.Down);
		this.offset += new Vector2(this.rect.x / (float)this.textureWidth, this.rect.y / (float)this.textureHeight);
		this.scale = new Vector2(this.rect.width / (float)this.textureWidth, this.rect.height / (float)this.textureHeight);
		this._material.SetTextureOffset("_MainTex", this.offset);
		this._material.SetTextureScale("_MainTex", this.scale);
	}

	// Token: 0x040073F1 RID: 29681
	public EyeLookMaterialControll eyeLookMaterialCtrl;

	// Token: 0x040073F2 RID: 29682
	public int Inside;

	// Token: 0x040073F3 RID: 29683
	public int Outside;

	// Token: 0x040073F4 RID: 29684
	public int Up;

	// Token: 0x040073F5 RID: 29685
	public int Down;

	// Token: 0x040073F6 RID: 29686
	public Rect rect;

	// Token: 0x040073F7 RID: 29687
	private Material _material;

	// Token: 0x040073F8 RID: 29688
	private int textureWidth;

	// Token: 0x040073F9 RID: 29689
	private int textureHeight;

	// Token: 0x040073FA RID: 29690
	private Vector2 offset;

	// Token: 0x040073FB RID: 29691
	private Vector2 scale;
}
