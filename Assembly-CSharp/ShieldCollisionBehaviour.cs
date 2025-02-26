using System;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class ShieldCollisionBehaviour : MonoBehaviour
{
	// Token: 0x06001420 RID: 5152 RVA: 0x0007DF50 File Offset: 0x0007C350
	public void ShieldCollisionEnter(CollisionInfo e)
	{
		if (e.Hit.transform != null)
		{
			if (this.IsWaterInstance)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ExplosionOnHit);
				Transform transform = gameObject.transform;
				transform.parent = base.transform;
				float num = base.transform.localScale.x * this.ScaleWave;
				transform.localScale = new Vector3(num, num, num);
				transform.localPosition = new Vector3(0f, 0.001f, 0f);
				transform.LookAt(e.Hit.point);
			}
			else
			{
				if (this.EffectOnHit != null)
				{
					if (!this.CreateMechInstanceOnHit)
					{
						Transform transform2 = e.Hit.transform;
						Renderer componentInChildren = transform2.GetComponentInChildren<Renderer>();
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.EffectOnHit);
						gameObject2.transform.parent = componentInChildren.transform;
						gameObject2.transform.localPosition = Vector3.zero;
						AddMaterialOnHit component = gameObject2.GetComponent<AddMaterialOnHit>();
						component.SetMaterialQueue(this.currentQueue);
						component.UpdateMaterial(e.Hit);
					}
					else
					{
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.EffectOnHit);
						Transform transform3 = gameObject3.transform;
						transform3.parent = base.GetComponent<Renderer>().transform;
						transform3.localPosition = Vector3.zero;
						transform3.localScale = base.transform.localScale * this.ScaleWave;
						transform3.LookAt(e.Hit.point);
						transform3.Rotate(this.AngleFix);
						gameObject3.GetComponent<Renderer>().material.renderQueue = this.currentQueue - 1000;
					}
				}
				if (this.currentQueue > 4000)
				{
					this.currentQueue = 3001;
				}
				else
				{
					this.currentQueue++;
				}
				if (this.ExplosionOnHit != null)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.ExplosionOnHit, e.Hit.point, default(Quaternion));
					gameObject4.transform.parent = base.transform;
				}
			}
		}
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0007E181 File Offset: 0x0007C581
	private void Update()
	{
	}

	// Token: 0x040016DA RID: 5850
	public GameObject EffectOnHit;

	// Token: 0x040016DB RID: 5851
	public GameObject ExplosionOnHit;

	// Token: 0x040016DC RID: 5852
	public bool IsWaterInstance;

	// Token: 0x040016DD RID: 5853
	public float ScaleWave = 0.89f;

	// Token: 0x040016DE RID: 5854
	public bool CreateMechInstanceOnHit;

	// Token: 0x040016DF RID: 5855
	public Vector3 AngleFix;

	// Token: 0x040016E0 RID: 5856
	public int currentQueue = 3001;
}
