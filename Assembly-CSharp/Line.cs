using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000657 RID: 1623
public class Line : MonoBehaviour
{
	// Token: 0x06002672 RID: 9842 RVA: 0x000DC888 File Offset: 0x000DAC88
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("Line", new List<Vector2>
		{
			new Vector2(0f, (float)UnityEngine.Random.Range(0, Screen.height)),
			new Vector2((float)(Screen.width - 1), (float)UnityEngine.Random.Range(0, Screen.height))
		}, 2f);
		vectorLine.Draw();
	}
}
