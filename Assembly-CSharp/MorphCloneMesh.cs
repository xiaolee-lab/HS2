using System;
using UnityEngine;

// Token: 0x02001114 RID: 4372
public class MorphCloneMesh : MonoBehaviour
{
	// Token: 0x060090F3 RID: 37107 RVA: 0x003C56BA File Offset: 0x003C3ABA
	public static void Clone(out Mesh CloneData, Mesh SorceData)
	{
		CloneData = UnityEngine.Object.Instantiate<Mesh>(SorceData);
	}
}
