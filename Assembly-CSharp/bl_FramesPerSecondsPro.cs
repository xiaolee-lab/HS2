using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061D RID: 1565
public class bl_FramesPerSecondsPro : MonoBehaviour
{
	// Token: 0x0600255A RID: 9562 RVA: 0x000D6280 File Offset: 0x000D4680
	private void Update()
	{
		if (this.FPSText == null)
		{
			return;
		}
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		float num = this.deltaTime * 1000f;
		float num2 = 1f / this.deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} FPS)", num, num2);
		this.FPSText.text = text.ToUpper();
	}

	// Token: 0x04002525 RID: 9509
	[SerializeField]
	private Text FPSText;

	// Token: 0x04002526 RID: 9510
	private float deltaTime;
}
