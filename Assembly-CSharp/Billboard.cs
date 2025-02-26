using System;
using UnityEngine;

// Token: 0x02000451 RID: 1105
public class Billboard : MonoBehaviour
{
	// Token: 0x0600143E RID: 5182 RVA: 0x0007ED10 File Offset: 0x0007D110
	private void Awake()
	{
		if (this.AutoInitCamera)
		{
			this.Camera = Camera.main;
			this.Active = true;
		}
		this.t = base.transform;
		this.camT = this.Camera.transform;
		Transform parent = this.t.parent;
		this.myContainer = new GameObject
		{
			name = "Billboard_" + this.t.gameObject.name
		};
		this.contT = this.myContainer.transform;
		this.contT.position = this.t.position;
		this.t.parent = this.myContainer.transform;
		this.contT.parent = parent;
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x0007EDDC File Offset: 0x0007D1DC
	private void Update()
	{
		if (this.Active)
		{
			this.contT.LookAt(this.contT.position + this.camT.rotation * Vector3.back, this.camT.rotation * Vector3.up);
		}
	}

	// Token: 0x0400170B RID: 5899
	public Camera Camera;

	// Token: 0x0400170C RID: 5900
	public bool Active = true;

	// Token: 0x0400170D RID: 5901
	public bool AutoInitCamera = true;

	// Token: 0x0400170E RID: 5902
	private GameObject myContainer;

	// Token: 0x0400170F RID: 5903
	private Transform t;

	// Token: 0x04001710 RID: 5904
	private Transform camT;

	// Token: 0x04001711 RID: 5905
	private Transform contT;
}
