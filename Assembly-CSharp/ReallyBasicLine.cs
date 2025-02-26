using System;
using UnityEngine;
using Vectrosity;

// Token: 0x02000658 RID: 1624
public class ReallyBasicLine : MonoBehaviour
{
	// Token: 0x06002674 RID: 9844 RVA: 0x000DC8F8 File Offset: 0x000DACF8
	private void Start()
	{
		VectorLine.SetLine(Color.white, new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2((float)(Screen.width - 1), (float)(Screen.height - 1))
		});
	}
}
