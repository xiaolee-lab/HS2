using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000522 RID: 1314
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressStartToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x0009BF1A File Offset: 0x0009A31A
		private Player player
		{
			get
			{
				return PressStartToJoinExample_Assigner.GetRewiredPlayer(this.gamePlayerId);
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0009BF27 File Offset: 0x0009A327
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0009BF35 File Offset: 0x0009A335
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

		// Token: 0x06001943 RID: 6467 RVA: 0x0009BF5C File Offset: 0x0009A35C
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0009BFB8 File Offset: 0x0009A3B8
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

		// Token: 0x04001C2B RID: 7211
		public int gamePlayerId;

		// Token: 0x04001C2C RID: 7212
		public float moveSpeed = 3f;

		// Token: 0x04001C2D RID: 7213
		public float bulletSpeed = 15f;

		// Token: 0x04001C2E RID: 7214
		public GameObject bulletPrefab;

		// Token: 0x04001C2F RID: 7215
		private CharacterController cc;

		// Token: 0x04001C30 RID: 7216
		private Vector3 moveVector;

		// Token: 0x04001C31 RID: 7217
		private bool fire;
	}
}
