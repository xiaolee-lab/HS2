using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C62 RID: 3170
	[RequireComponent(typeof(Rigidbody))]
	public abstract class ActorLocomotion : MonoBehaviour
	{
		// Token: 0x06006675 RID: 26229
		public abstract void Move(Vector3 deltaPosition);

		// Token: 0x06006676 RID: 26230 RVA: 0x002B8FE0 File Offset: 0x002B73E0
		protected virtual void Start()
		{
			Vector3 origin = base.transform.position + Vector3.up;
			RaycastHit raycastHit;
			Physics.Raycast(origin, Vector3.down, out raycastHit, (float)this._groundLayers);
			base.transform.position = raycastHit.point + Vector3.up;
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x002B903C File Offset: 0x002B743C
		protected virtual RaycastHit GetSpherecastHit()
		{
			Ray ray = new Ray(base.GetComponent<Rigidbody>().position + Vector3.up * this._airbornThreshold, Vector3.down);
			RaycastHit result;
			Physics.SphereCast(ray, this._spherecastRadius, out result, this._airbornThreshold * 2f, this._groundLayers);
			return result;
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x002B909C File Offset: 0x002B749C
		public float GetAngleFromForward(Vector3 worldDirection)
		{
			Vector3 vector = base.transform.InverseTransformDirection(worldDirection);
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x002B90D0 File Offset: 0x002B74D0
		protected void RigidbodyRotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Quaternion lhs = Quaternion.AngleAxis(angle, axis);
			this._actor.Rotation = lhs * this._actor.Rotation;
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x002B9104 File Offset: 0x002B7504
		protected float GetSlopeDamper(Vector3 velocity, Vector3 groundNormal)
		{
			float num = 90f - Vector3.Angle(velocity, groundNormal);
			num -= this._slopeStartAngle;
			float num2 = this._slopeEndAngle - this._slopeStartAngle;
			return 1f - Mathf.Clamp(num / num2, 0f, 1f);
		}

		// Token: 0x0400584E RID: 22606
		[SerializeField]
		protected float _airbornThreshold = 0.6f;

		// Token: 0x0400584F RID: 22607
		[SerializeField]
		private float _slopeStartAngle = 50f;

		// Token: 0x04005850 RID: 22608
		[SerializeField]
		private float _slopeEndAngle = 85f;

		// Token: 0x04005851 RID: 22609
		[SerializeField]
		private float _spherecastRadius = 0.1f;

		// Token: 0x04005852 RID: 22610
		[SerializeField]
		private LayerMask _groundLayers;

		// Token: 0x04005853 RID: 22611
		[SerializeField]
		protected Actor _actor;

		// Token: 0x04005854 RID: 22612
		[SerializeField]
		protected float _slopeLimit = 45f;

		// Token: 0x04005855 RID: 22613
		protected const float _half = 0.5f;

		// Token: 0x04005856 RID: 22614
		protected float _originalHeight;

		// Token: 0x04005857 RID: 22615
		protected Vector3 _originalCenter = Vector3.zero;

		// Token: 0x02000C63 RID: 3171
		public struct AnimationState
		{
			// Token: 0x0600667B RID: 26235 RVA: 0x002B914E File Offset: 0x002B754E
			public void Init()
			{
				this.moveDirection = Vector3.zero;
				this.onGround = true;
				this.yVelocity = 0f;
			}

			// Token: 0x04005858 RID: 22616
			public float medVelocity;

			// Token: 0x04005859 RID: 22617
			public float maxVelocity;

			// Token: 0x0400585A RID: 22618
			public bool setMediumOnWalk;

			// Token: 0x0400585B RID: 22619
			public Vector3 moveDirection;

			// Token: 0x0400585C RID: 22620
			public bool onGround;

			// Token: 0x0400585D RID: 22621
			public float yVelocity;
		}

		// Token: 0x02000C64 RID: 3172
		public enum UpdateType
		{
			// Token: 0x0400585F RID: 22623
			Update,
			// Token: 0x04005860 RID: 22624
			LateUpdate,
			// Token: 0x04005861 RID: 22625
			FixedUpdate
		}
	}
}
