using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EpicToonFX
{
	// Token: 0x0200041A RID: 1050
	public class ETFXFireProjectile : MonoBehaviour
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00075B3E File Offset: 0x00073F3E
		private void Start()
		{
			this.selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00075B58 File Offset: 0x00073F58
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				this.nextEffect();
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				this.nextEffect();
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				this.previousEffect();
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				this.previousEffect();
			}
			if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out this.hit, 100f))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.projectiles[this.currentProjectile], this.spawnPosition.position, Quaternion.identity);
				gameObject.transform.LookAt(this.hit.point);
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * this.speed);
				gameObject.GetComponent<ETFXProjectileScript>().impactNormal = this.hit.normal;
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00075C6A File Offset: 0x0007406A
		public void nextEffect()
		{
			if (this.currentProjectile < this.projectiles.Length - 1)
			{
				this.currentProjectile++;
			}
			else
			{
				this.currentProjectile = 0;
			}
			this.selectedProjectileButton.getProjectileNames();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00075CA6 File Offset: 0x000740A6
		public void previousEffect()
		{
			if (this.currentProjectile > 0)
			{
				this.currentProjectile--;
			}
			else
			{
				this.currentProjectile = this.projectiles.Length - 1;
			}
			this.selectedProjectileButton.getProjectileNames();
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00075CE2 File Offset: 0x000740E2
		public void AdjustSpeed(float newSpeed)
		{
			this.speed = newSpeed;
		}

		// Token: 0x04001555 RID: 5461
		private RaycastHit hit;

		// Token: 0x04001556 RID: 5462
		public GameObject[] projectiles;

		// Token: 0x04001557 RID: 5463
		public Transform spawnPosition;

		// Token: 0x04001558 RID: 5464
		[HideInInspector]
		public int currentProjectile;

		// Token: 0x04001559 RID: 5465
		public float speed = 1000f;

		// Token: 0x0400155A RID: 5466
		private ETFXButtonScript selectedProjectileButton;
	}
}
