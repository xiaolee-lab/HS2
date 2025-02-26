using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200115D RID: 4445
public class RawImageUVScrool : MonoBehaviour
{
	// Token: 0x060092D9 RID: 37593 RVA: 0x003CE77F File Offset: 0x003CCB7F
	private void Start()
	{
		this.uvRect = this.image.uvRect;
	}

	// Token: 0x060092DA RID: 37594 RVA: 0x003CE794 File Offset: 0x003CCB94
	private void Update()
	{
		this.uvRect = this.image.uvRect;
		this.uvRect.x = this.uvRect.x + Time.deltaTime * this.scrollSpeed;
		this.image.uvRect = this.uvRect;
	}

	// Token: 0x040076DD RID: 30429
	public RawImage image;

	// Token: 0x040076DE RID: 30430
	public float scrollSpeed = 0.5f;

	// Token: 0x040076DF RID: 30431
	private Rect uvRect;
}
