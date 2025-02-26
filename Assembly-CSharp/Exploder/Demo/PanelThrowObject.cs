using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000383 RID: 899
	public class PanelThrowObject : UseObject
	{
		// Token: 0x06000FE4 RID: 4068 RVA: 0x000595AC File Offset: 0x000579AC
		private void Start()
		{
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000595B0 File Offset: 0x000579B0
		public override void Use()
		{
			base.Use();
			GameObject original = this.ThrowObjects[UnityEngine.Random.Range(0, this.ThrowObjects.Length)];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, this.ThrowBox.transform.position + Vector3.up * 2f, Quaternion.identity);
			gameObject.AddComponent<ThrowObject>();
			gameObject.tag = "Exploder";
			if (gameObject.GetComponent<Rigidbody>() == null)
			{
				gameObject.AddComponent<Rigidbody>();
			}
			if (gameObject.GetComponentsInChildren<BoxCollider>().Length == 0)
			{
				gameObject.AddComponent<BoxCollider>();
			}
			Vector3 up = Vector3.up;
			up.x = UnityEngine.Random.Range(-0.2f, 0.2f);
			up.y = UnityEngine.Random.Range(1f, 0.8f);
			up.z = UnityEngine.Random.Range(-0.2f, 0.2f);
			up.Normalize();
			gameObject.GetComponent<Rigidbody>().velocity = up * 20f;
			gameObject.GetComponent<Rigidbody>().angularVelocity = UnityEngine.Random.insideUnitSphere * 3f;
			gameObject.GetComponent<Rigidbody>().mass = 20f;
		}

		// Token: 0x040011A7 RID: 4519
		public GameObject ThrowBox;

		// Token: 0x040011A8 RID: 4520
		public GameObject[] ThrowObjects;
	}
}
