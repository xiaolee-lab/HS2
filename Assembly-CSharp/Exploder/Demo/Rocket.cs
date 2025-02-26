using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000385 RID: 901
	public class Rocket : MonoBehaviour
	{
		// Token: 0x06000FEB RID: 4075 RVA: 0x000597E4 File Offset: 0x00057BE4
		private void Start()
		{
			this.parent = base.transform.parent;
			this.launchTimeout = float.MaxValue;
			ExploderUtils.SetActive(this.RocketStatic.gameObject, false);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00059813 File Offset: 0x00057C13
		public void OnActivate()
		{
			ExploderUtils.SetActive(this.RocketStatic.gameObject, true);
			if (this.parent)
			{
				ExploderUtils.SetVisible(base.gameObject, false);
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00059842 File Offset: 0x00057C42
		public void Reset()
		{
			ExploderUtils.SetActive(this.RocketStatic.gameObject, true);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00059858 File Offset: 0x00057C58
		public void Launch(Ray ray)
		{
			this.direction = ray;
			this.Source.PlayOneShot(this.GunShot);
			this.launchTimeout = 0.3f;
			this.launch = false;
			ExploderUtils.SetActive(this.RocketStatic.gameObject, false);
			ExploderUtils.SetVisible(base.gameObject, true);
			base.gameObject.transform.parent = this.parent;
			base.gameObject.transform.localPosition = this.RocketStatic.gameObject.transform.localPosition;
			base.gameObject.transform.localRotation = this.RocketStatic.gameObject.transform.localRotation;
			base.gameObject.transform.localScale = this.RocketStatic.gameObject.transform.localScale;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00059934 File Offset: 0x00057D34
		private void Update()
		{
			if (this.launchTimeout < 0f)
			{
				if (!this.launch)
				{
					this.launch = true;
					base.transform.parent = null;
					this.RocketLight.intensity = 2f;
					this.direction.origin = this.direction.origin + this.direction.direction * 2f;
					RaycastHit raycastHit;
					if (Physics.Raycast(this.direction, out raycastHit, float.PositiveInfinity))
					{
						this.shotPos = base.gameObject.transform.position;
						this.targetDistance = (base.gameObject.transform.position - raycastHit.point).sqrMagnitude;
					}
					else
					{
						this.targetDistance = 10000f;
					}
				}
				base.gameObject.transform.position += this.direction.direction * this.RocketVelocity * Time.timeScale;
				this.RocketLight.transform.position = base.gameObject.transform.position;
				if ((this.shotPos - base.gameObject.transform.position).sqrMagnitude > this.targetDistance)
				{
					this.Source.PlayOneShot(this.Explosion);
					this.HitCallback(base.gameObject.transform.position);
					this.launchTimeout = float.MaxValue;
					this.launch = false;
					ExploderUtils.SetVisible(base.gameObject, false);
					this.RocketLight.intensity = 0f;
				}
			}
			this.launchTimeout -= Time.deltaTime;
			if (Input.GetKeyDown(KeyCode.H))
			{
				this.HitCallback(base.gameObject.transform.position);
			}
		}

		// Token: 0x040011AE RID: 4526
		public AudioClip GunShot;

		// Token: 0x040011AF RID: 4527
		public AudioClip Explosion;

		// Token: 0x040011B0 RID: 4528
		public AudioSource Source;

		// Token: 0x040011B1 RID: 4529
		public GameObject RocketStatic;

		// Token: 0x040011B2 RID: 4530
		public Light RocketLight;

		// Token: 0x040011B3 RID: 4531
		public float RocketVelocity = 1f;

		// Token: 0x040011B4 RID: 4532
		public Rocket.OnHit HitCallback;

		// Token: 0x040011B5 RID: 4533
		private Ray direction;

		// Token: 0x040011B6 RID: 4534
		private bool launch;

		// Token: 0x040011B7 RID: 4535
		private float launchTimeout;

		// Token: 0x040011B8 RID: 4536
		private Transform parent;

		// Token: 0x040011B9 RID: 4537
		private Vector3 shotPos;

		// Token: 0x040011BA RID: 4538
		private float targetDistance;

		// Token: 0x02000386 RID: 902
		// (Invoke) Token: 0x06000FF1 RID: 4081
		public delegate void OnHit(Vector3 pos);
	}
}
