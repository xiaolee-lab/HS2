using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200050D RID: 1293
	[AddComponentMenu("")]
	public class CustomControllersTiltDemo : MonoBehaviour
	{
		// Token: 0x060018AE RID: 6318 RVA: 0x00098D58 File Offset: 0x00097158
		private void Awake()
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			this.player = ReInput.players.GetPlayer(0);
			ReInput.InputSourceUpdateEvent += this.OnInputUpdate;
			this.controller = (CustomController)this.player.controllers.GetControllerWithTag(ControllerType.Custom, "TiltController");
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00098DB0 File Offset: 0x000971B0
		private void Update()
		{
			if (this.target == null)
			{
				return;
			}
			Vector3 a = Vector3.zero;
			a.y = this.player.GetAxis("Tilt Vertical");
			a.x = this.player.GetAxis("Tilt Horizontal");
			if (a.sqrMagnitude > 1f)
			{
				a.Normalize();
			}
			a *= Time.deltaTime;
			this.target.Translate(a * this.speed);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00098E40 File Offset: 0x00097240
		private void OnInputUpdate()
		{
			Vector3 acceleration = Input.acceleration;
			this.controller.SetAxisValue(0, acceleration.x);
			this.controller.SetAxisValue(1, acceleration.y);
			this.controller.SetAxisValue(2, acceleration.z);
		}

		// Token: 0x04001B98 RID: 7064
		public Transform target;

		// Token: 0x04001B99 RID: 7065
		public float speed = 10f;

		// Token: 0x04001B9A RID: 7066
		private CustomController controller;

		// Token: 0x04001B9B RID: 7067
		private Player player;
	}
}
