using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200116E RID: 4462
public class DebugRenderLog : MonoBehaviour
{
	// Token: 0x0400770A RID: 30474
	public Color color = Color.white;

	// Token: 0x0400770B RID: 30475
	private Queue<string> logQueue = new Queue<string>();

	// Token: 0x0400770C RID: 30476
	private const uint LOG_MAX = 20U;
}
