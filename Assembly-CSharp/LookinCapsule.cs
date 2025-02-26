using System;
using System.Text;
using UnityEngine;

// Token: 0x020010BD RID: 4285
public class LookinCapsule : CollisionCamera
{
	// Token: 0x06008EEB RID: 36587 RVA: 0x003B71B0 File Offset: 0x003B55B0
	private new void Start()
	{
		base.Start();
		this.lookCap = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		this.lookCap.GetComponent<CapsuleCollider>().isTrigger = true;
		this.lookCap.GetComponent<Renderer>().enabled = false;
		this.lookCap.transform.position = (this.camCtrl.TargetPos + this.camCtrl.transform.position) * 0.5f;
		this.lookCap.transform.parent = this.camCtrl.transform;
		Vector3 vector = this.camCtrl.CameraAngle;
		vector.x += 90f;
		this.lookCap.transform.rotation = Quaternion.Euler(vector);
		vector = this.lookCap.transform.localScale;
		vector.y = (this.camCtrl.TargetPos - this.camCtrl.transform.position).magnitude;
		this.lookCap.transform.localScale = vector;
		this.lookCap.AddComponent<LookHit>();
		Rigidbody rigidbody = this.lookCap.AddComponent<Rigidbody>();
		rigidbody.useGravity = false;
	}

	// Token: 0x06008EEC RID: 36588 RVA: 0x003B72EC File Offset: 0x003B56EC
	private void Update()
	{
		this.lookCap.transform.position = (this.camCtrl.TargetPos + this.camCtrl.transform.position) * 0.5f;
		Vector3 localScale;
		localScale.y = Vector3.Distance(this.camCtrl.TargetPos, this.camCtrl.transform.position) * 0.5f;
		localScale.x = (localScale.z = this.scaleRate);
		this.lookCap.transform.localScale = localScale;
	}

	// Token: 0x06008EED RID: 36589 RVA: 0x003B7388 File Offset: 0x003B5788
	private void OnGUI()
	{
		StringBuilder stringBuilder = new StringBuilder();
		float height = 1000f;
		if (this.objDels != null)
		{
			foreach (GameObject gameObject in this.objDels)
			{
				if (!gameObject.GetComponent<Renderer>().enabled)
				{
					stringBuilder.Append(gameObject.name);
					stringBuilder.Append("\n");
				}
			}
		}
		GUI.Box(new Rect(5f, 5f, 300f, height), string.Empty);
		GUI.Label(new Rect(10f, 5f, 1000f, height), stringBuilder.ToString());
	}

	// Token: 0x0400737C RID: 29564
	public float scaleRate = 5f;

	// Token: 0x0400737D RID: 29565
	private GameObject lookCap;
}
