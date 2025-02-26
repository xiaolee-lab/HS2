using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000514 RID: 1300
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class EightPlayersExample_Player : MonoBehaviour
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x00099DD1 File Offset: 0x000981D1
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00099DDF File Offset: 0x000981DF
		private void Initialize()
		{
			this.player = ReInput.players.GetPlayer(this.playerId);
			this.initialized = true;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00099DFE File Offset: 0x000981FE
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.initialized)
			{
				this.Initialize();
			}
			this.GetInput();
			this.ProcessInput();
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00099E28 File Offset: 0x00098228
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00099E84 File Offset: 0x00098284
		private void ProcessInput()
		{
			if (this.moveVector.x != 0f || this.moveVector.y != 0f)
			{
				this.cc.Move(this.moveVector * this.moveSpeed * Time.deltaTime);
			}
			if (this.fire)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + base.transform.right, base.transform.rotation);
				gameObject.GetComponent<Rigidbody>().AddForce(base.transform.right * this.bulletSpeed, ForceMode.VelocityChange);
			}
		}

		// Token: 0x04001BC3 RID: 7107
		public int playerId;

		// Token: 0x04001BC4 RID: 7108
		public float moveSpeed = 3f;

		// Token: 0x04001BC5 RID: 7109
		public float bulletSpeed = 15f;

		// Token: 0x04001BC6 RID: 7110
		public GameObject bulletPrefab;

		// Token: 0x04001BC7 RID: 7111
		private Player player;

		// Token: 0x04001BC8 RID: 7112
		private CharacterController cc;

		// Token: 0x04001BC9 RID: 7113
		private Vector3 moveVector;

		// Token: 0x04001BCA RID: 7114
		private bool fire;

		// Token: 0x04001BCB RID: 7115
		[NonSerialized]
		private bool initialized;
	}
}
