using System;
using UnityEngine;

// Token: 0x020010BF RID: 4287
public class RayChara : CollisionCamera
{
	// Token: 0x06008EFB RID: 36603 RVA: 0x003B74E2 File Offset: 0x003B58E2
	private new void Start()
	{
		base.Start();
	}

	// Token: 0x06008EFC RID: 36604 RVA: 0x003B74EC File Offset: 0x003B58EC
	private void Update()
	{
		if (this.parts != null && this.objDels != null)
		{
			foreach (GameObject gameObject in this.objDels)
			{
				gameObject.GetComponent<Renderer>().enabled = true;
			}
			foreach (RayChara.Parts parts in this.parts)
			{
				parts.Update(base.transform.position, this.tagName);
			}
		}
	}

	// Token: 0x0400737F RID: 29567
	public RayChara.Parts[] parts;

	// Token: 0x020010C0 RID: 4288
	[Serializable]
	public class Parts
	{
		// Token: 0x06008EFE RID: 36606 RVA: 0x003B7580 File Offset: 0x003B5980
		public void Update(Vector3 pos, string tag)
		{
			Vector3 vector = this.target.position - pos;
			RaycastHit[] array = Physics.RaycastAll(pos, vector.normalized, vector.magnitude);
			foreach (RaycastHit raycastHit in array)
			{
				if (raycastHit.collider.gameObject.tag == tag)
				{
					raycastHit.collider.gameObject.GetComponent<Renderer>().enabled = false;
				}
			}
		}

		// Token: 0x04007380 RID: 29568
		public Transform target;
	}
}
