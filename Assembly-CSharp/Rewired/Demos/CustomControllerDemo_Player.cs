using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200050F RID: 1295
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class CustomControllerDemo_Player : MonoBehaviour
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x000991C0 File Offset: 0x000975C0
		private Player player
		{
			get
			{
				if (this._player == null)
				{
					this._player = ReInput.players.GetPlayer(this.playerId);
				}
				return this._player;
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000991E9 File Offset: 0x000975E9
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000991F8 File Offset: 0x000975F8
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Vector2 a = new Vector2(this.player.GetAxis("Move Horizontal"), this.player.GetAxis("Move Vertical"));
			this.cc.Move(a * this.speed * Time.deltaTime);
			if (this.player.GetButtonDown("Fire"))
			{
				Vector3 b = Vector3.Scale(new Vector3(1f, 0f, 0f), base.transform.right);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + b, Quaternion.identity);
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(this.bulletSpeed * base.transform.right.x, 0f, 0f);
			}
			if (this.player.GetButtonDown("Change Color"))
			{
				Renderer component = base.GetComponent<Renderer>();
				Material material = component.material;
				material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
				component.material = material;
			}
		}

		// Token: 0x04001BA7 RID: 7079
		public int playerId;

		// Token: 0x04001BA8 RID: 7080
		public float speed = 1f;

		// Token: 0x04001BA9 RID: 7081
		public float bulletSpeed = 20f;

		// Token: 0x04001BAA RID: 7082
		public GameObject bulletPrefab;

		// Token: 0x04001BAB RID: 7083
		private Player _player;

		// Token: 0x04001BAC RID: 7084
		private CharacterController cc;
	}
}
