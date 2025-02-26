using System;
using UnityEngine;

// Token: 0x020010BB RID: 4283
public class CollisionCamera : MonoBehaviour
{
	// Token: 0x06008EE1 RID: 36577 RVA: 0x003B6BEF File Offset: 0x003B4FEF
	public void SetCollision()
	{
		this.objDels = GameObject.FindGameObjectsWithTag(this.tagName);
	}

	// Token: 0x06008EE2 RID: 36578 RVA: 0x003B6C02 File Offset: 0x003B5002
	protected void Start()
	{
		this.camCtrl = base.gameObject.GetComponent<BaseCameraControl>();
	}

	// Token: 0x06008EE3 RID: 36579 RVA: 0x003B6C18 File Offset: 0x003B5018
	private void Update()
	{
		this.SetCollision();
		if (this.objDels != null)
		{
			foreach (GameObject gameObject in this.objDels)
			{
				if (gameObject.GetComponent<Renderer>())
				{
					gameObject.GetComponent<Renderer>().enabled = true;
				}
			}
			Vector3 position = base.transform.position;
			Vector3 a;
			if (this.camCtrl.targetObj)
			{
				a = this.camCtrl.targetObj.transform.position;
			}
			else
			{
				a = this.camCtrl.TargetPos;
			}
			Vector3 vector = a - position;
			RaycastHit[] array2 = Physics.RaycastAll(position, vector.normalized, vector.magnitude);
			foreach (RaycastHit raycastHit in array2)
			{
				if (raycastHit.collider.gameObject.tag == this.tagName)
				{
					float magnitude = vector.magnitude;
					float num = Vector3.Distance(raycastHit.collider.bounds.center, position);
					if (magnitude > num)
					{
						raycastHit.collider.gameObject.GetComponent<Renderer>().enabled = false;
					}
				}
			}
		}
	}

	// Token: 0x04007379 RID: 29561
	public string tagName = "CollDels";

	// Token: 0x0400737A RID: 29562
	protected GameObject[] objDels;

	// Token: 0x0400737B RID: 29563
	protected BaseCameraControl camCtrl;
}
