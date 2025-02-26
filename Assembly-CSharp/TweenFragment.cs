using System;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class TweenFragment : MonoBehaviour
{
	// Token: 0x06001034 RID: 4148 RVA: 0x0005AB24 File Offset: 0x00058F24
	private void Start()
	{
		this.initPos = base.transform.position;
		this.time = 0f;
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0005AB44 File Offset: 0x00058F44
	private void Update()
	{
		this.time += Time.deltaTime * UnityEngine.Random.value * 2f;
		base.transform.position = Vector3.Lerp(this.initPos, this.TargetPos.position, this.time / this.LerpTime);
	}

	// Token: 0x040011EF RID: 4591
	public Transform TargetPos;

	// Token: 0x040011F0 RID: 4592
	public float LerpTime;

	// Token: 0x040011F1 RID: 4593
	private Vector3 initPos;

	// Token: 0x040011F2 RID: 4594
	private float time;
}
