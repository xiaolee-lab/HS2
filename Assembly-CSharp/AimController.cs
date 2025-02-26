using System;
using UnityEngine;

// Token: 0x020005A4 RID: 1444
public class AimController : MonoBehaviour
{
	// Token: 0x0600217B RID: 8571 RVA: 0x000B8FA3 File Offset: 0x000B73A3
	private void Start()
	{
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x000B8FA8 File Offset: 0x000B73A8
	private void Update()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
		base.transform.position = position;
	}
}
