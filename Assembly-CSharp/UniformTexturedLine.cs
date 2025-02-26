using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000659 RID: 1625
public class UniformTexturedLine : MonoBehaviour
{
	// Token: 0x06002676 RID: 9846 RVA: 0x000DC970 File Offset: 0x000DAD70
	private void Start()
	{
		new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height / 2)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, this.lineTexture, this.lineWidth)
		{
			textureScale = this.textureScale
		}.Draw();
	}

	// Token: 0x0400266D RID: 9837
	public Texture lineTexture;

	// Token: 0x0400266E RID: 9838
	public float lineWidth = 8f;

	// Token: 0x0400266F RID: 9839
	public float textureScale = 1f;
}
