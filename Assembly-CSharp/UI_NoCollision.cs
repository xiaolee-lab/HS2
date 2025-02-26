using System;
using UnityEngine;

// Token: 0x02001164 RID: 4452
public class UI_NoCollision : MonoBehaviour, ICanvasRaycastFilter
{
	// Token: 0x0600930A RID: 37642 RVA: 0x003CF8C0 File Offset: 0x003CDCC0
	public bool IsRaycastLocationValid(Vector2 sp, Camera cam)
	{
		return false;
	}
}
