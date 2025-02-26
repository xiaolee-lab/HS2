using System;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
public class UVScroll : MonoBehaviour
{
	// Token: 0x06002838 RID: 10296 RVA: 0x000EEA08 File Offset: 0x000ECE08
	private void Start()
	{
		base.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x000EEA24 File Offset: 0x000ECE24
	private void Update()
	{
		float x = Mathf.Repeat(Time.time * this.scrollSpeedX, 1f);
		float y = Mathf.Repeat(Time.time * this.scrollSpeedY, 1f);
		Vector2 value = new Vector2(x, y);
		base.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", value);
	}

	// Token: 0x04002936 RID: 10550
	[SerializeField]
	private float scrollSpeedX = 0.1f;

	// Token: 0x04002937 RID: 10551
	[SerializeField]
	private float scrollSpeedY = 0.1f;
}
