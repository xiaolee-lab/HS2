using System;
using UnityEngine;

// Token: 0x020006A2 RID: 1698
public class AnimatedFountainUVs : MonoBehaviour
{
	// Token: 0x06002840 RID: 10304 RVA: 0x000EEDA0 File Offset: 0x000ED1A0
	private void Start()
	{
		this._size = new Vector2(1f / (float)this._uvTieX, 1f / (float)this._uvTieY);
		this._myRenderer = base.GetComponent<Renderer>();
		if (this._myRenderer == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x000EEDF8 File Offset: 0x000ED1F8
	private void Update()
	{
		int num = (int)(Time.timeSinceLevelLoad * (float)this._fps) % (this._uvTieX * this._uvTieY);
		if (num != this._lastIndex)
		{
			int num2 = num % this._uvTieX;
			int num3 = num / this._uvTieY;
			Vector2 value = new Vector2((float)num2 * this._size.x, 1f - this._size.y - (float)num3 * this._size.y);
			this._myRenderer.material.SetTextureOffset("_MainTex", value);
			this._myRenderer.material.SetTextureScale("_MainTex", this._size);
			this._lastIndex = num;
		}
	}

	// Token: 0x04002945 RID: 10565
	public int _uvTieX = 1;

	// Token: 0x04002946 RID: 10566
	public int _uvTieY = 1;

	// Token: 0x04002947 RID: 10567
	public int _fps = 10;

	// Token: 0x04002948 RID: 10568
	private Vector2 _size;

	// Token: 0x04002949 RID: 10569
	private Renderer _myRenderer;

	// Token: 0x0400294A RID: 10570
	private int _lastIndex = -1;
}
