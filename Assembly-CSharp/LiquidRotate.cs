using System;
using UnityEngine;

// Token: 0x02000AED RID: 2797
public class LiquidRotate : MonoBehaviour
{
	// Token: 0x060051B9 RID: 20921 RVA: 0x0021B361 File Offset: 0x00219761
	private void Start()
	{
		this.RandomRot();
		base.InvokeRepeating("RandomRot", 0f, this.RotateEverySecond);
	}

	// Token: 0x060051BA RID: 20922 RVA: 0x0021B37F File Offset: 0x0021977F
	private void Update()
	{
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.TargetRot, Time.time * Time.deltaTime);
	}

	// Token: 0x060051BB RID: 20923 RVA: 0x0021B3AD File Offset: 0x002197AD
	private void RandomRot()
	{
		this.TargetRot = UnityEngine.Random.rotation;
	}

	// Token: 0x04004C39 RID: 19513
	private Quaternion TargetRot;

	// Token: 0x04004C3A RID: 19514
	public float RotateEverySecond = 1f;
}
