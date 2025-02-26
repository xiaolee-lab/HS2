using System;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class ETFXProjectileScript : MonoBehaviour
{
	// Token: 0x06001335 RID: 4917 RVA: 0x00076154 File Offset: 0x00074554
	private void Start()
	{
		this.projectileParticle = UnityEngine.Object.Instantiate<GameObject>(this.projectileParticle, base.transform.position, base.transform.rotation);
		this.projectileParticle.transform.parent = base.transform;
		if (this.muzzleParticle)
		{
			this.muzzleParticle = UnityEngine.Object.Instantiate<GameObject>(this.muzzleParticle, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Destroy(this.muzzleParticle, 1.5f);
		}
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x000761E8 File Offset: 0x000745E8
	private void OnCollisionEnter(Collision hit)
	{
		if (!this.hasCollided)
		{
			this.hasCollided = true;
			this.impactParticle = UnityEngine.Object.Instantiate<GameObject>(this.impactParticle, base.transform.position, Quaternion.FromToRotation(Vector3.up, this.impactNormal));
			if (hit.gameObject.tag == "Destructible")
			{
				UnityEngine.Object.Destroy(hit.gameObject);
			}
			foreach (GameObject gameObject in this.trailParticles)
			{
				GameObject gameObject2 = base.transform.Find(this.projectileParticle.name + "/" + gameObject.name).gameObject;
				gameObject2.transform.parent = null;
				UnityEngine.Object.Destroy(gameObject2, 3f);
			}
			UnityEngine.Object.Destroy(this.projectileParticle, 3f);
			UnityEngine.Object.Destroy(this.impactParticle, 5f);
			UnityEngine.Object.Destroy(base.gameObject);
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			for (int j = 1; j < componentsInChildren.Length; j++)
			{
				ParticleSystem particleSystem = componentsInChildren[j];
				if (particleSystem.gameObject.name.Contains("Trail"))
				{
					particleSystem.transform.SetParent(null);
					UnityEngine.Object.Destroy(particleSystem.gameObject, 2f);
				}
			}
		}
	}

	// Token: 0x0400156C RID: 5484
	public GameObject impactParticle;

	// Token: 0x0400156D RID: 5485
	public GameObject projectileParticle;

	// Token: 0x0400156E RID: 5486
	public GameObject muzzleParticle;

	// Token: 0x0400156F RID: 5487
	public GameObject[] trailParticles;

	// Token: 0x04001570 RID: 5488
	[HideInInspector]
	public Vector3 impactNormal;

	// Token: 0x04001571 RID: 5489
	private bool hasCollided;
}
