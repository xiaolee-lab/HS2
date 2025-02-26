using System;
using UnityEngine;

// Token: 0x02000669 RID: 1641
public class MakeSpheres : MonoBehaviour
{
	// Token: 0x060026B1 RID: 9905 RVA: 0x000DF850 File Offset: 0x000DDC50
	private void Start()
	{
		for (int i = 0; i < this.numberOfSpheres; i++)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.spherePrefab, new Vector3(UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area)), UnityEngine.Random.rotation);
		}
	}

	// Token: 0x040026D5 RID: 9941
	public GameObject spherePrefab;

	// Token: 0x040026D6 RID: 9942
	public int numberOfSpheres = 12;

	// Token: 0x040026D7 RID: 9943
	public float area = 4.5f;
}
