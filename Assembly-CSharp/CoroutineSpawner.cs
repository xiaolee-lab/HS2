using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public class CoroutineSpawner : MonoBehaviour
{
	// Token: 0x0600160A RID: 5642 RVA: 0x000877A4 File Offset: 0x00085BA4
	public IEnumerator Co01_WaitForSeconds()
	{
		for (;;)
		{
			yield return new WaitForSeconds(0.3f);
		}
		yield break;
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x000877B8 File Offset: 0x00085BB8
	public IEnumerator Co02_PerFrame_NULL()
	{
		for (;;)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000877CC File Offset: 0x00085BCC
	public IEnumerator Co03_PerFrame_EOF()
	{
		for (;;)
		{
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x000877E0 File Offset: 0x00085BE0
	public IEnumerator Co04_PerFrame_ARG(float argFloat)
	{
		for (;;)
		{
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}
}
