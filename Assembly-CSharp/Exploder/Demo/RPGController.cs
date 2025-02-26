using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000387 RID: 903
	public class RPGController : MonoBehaviour
	{
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00059B3A File Offset: 0x00057F3A
		private void Start()
		{
			this.Rocket.HitCallback = new Rocket.OnHit(this.OnRocketHit);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00059B53 File Offset: 0x00057F53
		public void OnActivate()
		{
			this.Rocket.OnActivate();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00059B60 File Offset: 0x00057F60
		private void OnRocketHit(Vector3 position)
		{
			this.nextShotTimeout = 0.6f;
			this.exploder.transform.position = position;
			this.exploder.ExplodeSelf = false;
			this.exploder.Force = 20f;
			this.exploder.TargetFragments = 100;
			this.exploder.Radius = 10f;
			this.exploder.UseForceVector = false;
			this.exploder.ExplodeRadius();
			this.Rocket.Reset();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00059BE4 File Offset: 0x00057FE4
		private void Update()
		{
			TargetType targetType = TargetManager.Instance.TargetType;
			if (Input.GetMouseButtonDown(0) && this.nextShotTimeout < 0f && CursorLocking.IsLocked && targetType != TargetType.UseObject)
			{
				this.MouseLookCamera.Kick();
				Ray ray = this.MouseLookCamera.mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
				UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 0f);
				this.Rocket.Launch(ray);
				this.nextShotTimeout = float.MaxValue;
			}
			this.nextShotTimeout -= Time.deltaTime;
		}

		// Token: 0x040011BB RID: 4539
		public ExploderObject exploder;

		// Token: 0x040011BC RID: 4540
		public ExploderMouseLook MouseLookCamera;

		// Token: 0x040011BD RID: 4541
		public Rocket Rocket;

		// Token: 0x040011BE RID: 4542
		private float nextShotTimeout;
	}
}
