using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000388 RID: 904
	public class ShotgunController : MonoBehaviour
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x00059CB6 File Offset: 0x000580B6
		public void OnActivate()
		{
			ExploderUtils.SetActive(this.MuzzleFlash, false);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00059CC4 File Offset: 0x000580C4
		private void Update()
		{
			GameObject gameObject = null;
			TargetType targetType = TargetManager.Instance.TargetType;
			if (targetType == TargetType.UseObject)
			{
				if (this.lastTarget != TargetType.UseObject)
				{
				}
				this.lastTarget = TargetType.UseObject;
			}
			if (this.lastTarget == TargetType.UseObject)
			{
			}
			this.lastTarget = targetType;
			Ray ray = this.MouseLookCamera.mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
			UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 0f);
			if (targetType == TargetType.DestroyableObject)
			{
				gameObject = TargetManager.Instance.TargetObject;
			}
			if (Input.GetMouseButtonDown(0) && this.nextShotTimeout < 0f && CursorLocking.IsLocked)
			{
				if (targetType != TargetType.UseObject)
				{
					this.Source.PlayOneShot(this.GunShot);
					this.MouseLookCamera.Kick();
					this.reloadTimeout = 0.3f;
					this.flashing = 5;
				}
				if (gameObject)
				{
					Vector3 centroid = ExploderUtils.GetCentroid(gameObject);
					this.exploder.transform.position = centroid;
					this.exploder.ExplodeSelf = false;
					this.exploder.ForceVector = ray.direction.normalized;
					this.exploder.Force = 10f;
					this.exploder.UseForceVector = true;
					this.exploder.TargetFragments = 30;
					this.exploder.Radius = 1f;
					this.exploder.ExplodeRadius();
				}
				this.nextShotTimeout = 0.6f;
			}
			this.nextShotTimeout -= Time.deltaTime;
			if (this.flashing > 0)
			{
				this.Flash.intensity = 1f;
				ExploderUtils.SetActive(this.MuzzleFlash, true);
				this.flashing--;
			}
			else
			{
				this.Flash.intensity = 0f;
				ExploderUtils.SetActive(this.MuzzleFlash, false);
			}
			this.reloadTimeout -= Time.deltaTime;
			if (this.reloadTimeout < 0f)
			{
				this.reloadTimeout = float.MaxValue;
				this.Source.PlayOneShot(this.Reload);
				this.ReloadAnim.Play();
			}
		}

		// Token: 0x040011BF RID: 4543
		public AudioClip GunShot;

		// Token: 0x040011C0 RID: 4544
		public AudioClip Reload;

		// Token: 0x040011C1 RID: 4545
		public AudioSource Source;

		// Token: 0x040011C2 RID: 4546
		public ExploderMouseLook MouseLookCamera;

		// Token: 0x040011C3 RID: 4547
		public Light Flash;

		// Token: 0x040011C4 RID: 4548
		public Animation ReloadAnim;

		// Token: 0x040011C5 RID: 4549
		public AnimationClip HideAnim;

		// Token: 0x040011C6 RID: 4550
		public GameObject MuzzleFlash;

		// Token: 0x040011C7 RID: 4551
		private int flashing;

		// Token: 0x040011C8 RID: 4552
		private float reloadTimeout = float.MaxValue;

		// Token: 0x040011C9 RID: 4553
		private float nextShotTimeout;

		// Token: 0x040011CA RID: 4554
		private TargetType lastTarget;

		// Token: 0x040011CB RID: 4555
		public ExploderObject exploder;
	}
}
