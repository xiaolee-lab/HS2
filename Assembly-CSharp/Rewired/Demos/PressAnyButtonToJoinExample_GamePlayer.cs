using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200051F RID: 1311
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressAnyButtonToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0009BBA5 File Offset: 0x00099FA5
		private Player player
		{
			get
			{
				return (!ReInput.isReady) ? null : ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0009BBC7 File Offset: 0x00099FC7
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0009BBD5 File Offset: 0x00099FD5
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (this.player == null)
			{
				return;
			}
			this.GetInput();
			this.ProcessInput();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0009BBFC File Offset: 0x00099FFC
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0009BC58 File Offset: 0x0009A058
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

		// Token: 0x04001C1E RID: 7198
		public int playerId;

		// Token: 0x04001C1F RID: 7199
		public float moveSpeed = 3f;

		// Token: 0x04001C20 RID: 7200
		public float bulletSpeed = 15f;

		// Token: 0x04001C21 RID: 7201
		public GameObject bulletPrefab;

		// Token: 0x04001C22 RID: 7202
		private CharacterController cc;

		// Token: 0x04001C23 RID: 7203
		private Vector3 moveVector;

		// Token: 0x04001C24 RID: 7204
		private bool fire;
	}
}
