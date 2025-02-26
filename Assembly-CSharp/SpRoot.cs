using System;
using UnityEngine;

// Token: 0x02000848 RID: 2120
public class SpRoot : MonoBehaviour
{
	// Token: 0x06003629 RID: 13865 RVA: 0x0013FAB0 File Offset: 0x0013DEB0
	public float GetSpriteRate()
	{
		float num = this.baseScreenHeight / 2f * 0.01f;
		return 1f / (num * ((float)Screen.height / (this.baseScreenHeight * ((float)Screen.width / this.baseScreenWidth))));
	}

	// Token: 0x0600362A RID: 13866 RVA: 0x0013FAF3 File Offset: 0x0013DEF3
	public float GetSpriteCorrectY()
	{
		return ((float)Screen.height - (float)Screen.width / this.baseScreenWidth * this.baseScreenHeight) * (2f / (float)Screen.height) * 0.5f;
	}

	// Token: 0x04003678 RID: 13944
	public Camera renderCamera;

	// Token: 0x04003679 RID: 13945
	public float baseScreenWidth = 1280f;

	// Token: 0x0400367A RID: 13946
	public float baseScreenHeight = 720f;
}
