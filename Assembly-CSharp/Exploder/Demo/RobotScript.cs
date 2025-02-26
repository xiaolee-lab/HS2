using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000384 RID: 900
	internal class RobotScript : MonoBehaviour
	{
		// Token: 0x06000FE7 RID: 4071 RVA: 0x000596F4 File Offset: 0x00057AF4
		private void Start()
		{
			this.center = base.gameObject.transform.position;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0005970C File Offset: 0x00057B0C
		private void Update()
		{
			base.GetComponent<Animation>().Play();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0005971C File Offset: 0x00057B1C
		private void FixedUpdate()
		{
			Vector3 position = base.gameObject.transform.position;
			position.x = this.center.x + Mathf.Sin(this.angle) * this.radius;
			position.z = this.center.z + Mathf.Cos(this.angle) * this.radius;
			base.gameObject.transform.position = position;
			base.gameObject.transform.forward = position - this.lastPos;
			this.lastPos = position;
			this.angle += Time.deltaTime * this.velocity;
		}

		// Token: 0x040011A9 RID: 4521
		public float radius = 4f;

		// Token: 0x040011AA RID: 4522
		public float velocity = 1f;

		// Token: 0x040011AB RID: 4523
		private float angle;

		// Token: 0x040011AC RID: 4524
		private Vector3 center;

		// Token: 0x040011AD RID: 4525
		private Vector3 lastPos;
	}
}
