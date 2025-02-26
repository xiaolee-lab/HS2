using System;
using UnityEngine;

// Token: 0x020010BE RID: 4286
public class LookHit : MonoBehaviour
{
	// Token: 0x17001EFB RID: 7931
	// (get) Token: 0x06008EEF RID: 36591 RVA: 0x003B7440 File Offset: 0x003B5840
	public bool IsNowDragging
	{
		get
		{
			return this.isNowDragging;
		}
	}

	// Token: 0x06008EF0 RID: 36592 RVA: 0x003B7448 File Offset: 0x003B5848
	private void Start()
	{
	}

	// Token: 0x06008EF1 RID: 36593 RVA: 0x003B744A File Offset: 0x003B584A
	private void Update()
	{
	}

	// Token: 0x06008EF2 RID: 36594 RVA: 0x003B744C File Offset: 0x003B584C
	private void OnMouseDown()
	{
		this.isNowDragging = true;
	}

	// Token: 0x06008EF3 RID: 36595 RVA: 0x003B7455 File Offset: 0x003B5855
	private void OnMouseUp()
	{
		this.isNowDragging = false;
	}

	// Token: 0x06008EF4 RID: 36596 RVA: 0x003B745E File Offset: 0x003B585E
	private void OnCollisionEnter(Collision col)
	{
	}

	// Token: 0x06008EF5 RID: 36597 RVA: 0x003B7460 File Offset: 0x003B5860
	private void OnCollisionExit(Collision col)
	{
	}

	// Token: 0x06008EF6 RID: 36598 RVA: 0x003B7462 File Offset: 0x003B5862
	private void OnCollisionStay(Collision col)
	{
	}

	// Token: 0x06008EF7 RID: 36599 RVA: 0x003B7464 File Offset: 0x003B5864
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "CollDels")
		{
			col.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}

	// Token: 0x06008EF8 RID: 36600 RVA: 0x003B7491 File Offset: 0x003B5891
	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "CollDels")
		{
			col.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}

	// Token: 0x06008EF9 RID: 36601 RVA: 0x003B74BE File Offset: 0x003B58BE
	private void OnTriggerStay(Collider col)
	{
		if (col.gameObject.name == "Floor")
		{
		}
	}

	// Token: 0x0400737E RID: 29566
	private bool isNowDragging;
}
