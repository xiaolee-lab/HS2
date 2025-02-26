using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEx.Misc;

namespace AIProject
{
	// Token: 0x02000C35 RID: 3125
	[RequireComponent(typeof(Collider))]
	public class WaterVolume : MonoBehaviour
	{
		// Token: 0x060060CD RID: 24781 RVA: 0x0028ADF4 File Offset: 0x002891F4
		private void Update()
		{
			foreach (Rigidbody rigidbody in this._rigidbodies)
			{
				if (!(rigidbody == null) && !(rigidbody.gameObject == null))
				{
					Vector3 velocity = rigidbody.velocity;
					velocity.y = 0f;
					rigidbody.velocity = velocity;
				}
			}
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0028AE88 File Offset: 0x00289288
		private void OnTriggerEnter(Collider other)
		{
			int layer = other.gameObject.layer;
			if (!this._waterLayer.Contains(layer))
			{
				return;
			}
			Rigidbody component = other.GetComponent<Rigidbody>();
			if (component == null)
			{
				return;
			}
			if (!this._rigidbodies.Contains(component))
			{
				this._rigidbodies.Add(component);
			}
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0028AEE4 File Offset: 0x002892E4
		private void OnTriggerExit(Collider other)
		{
			int layer = other.gameObject.layer;
			if (!this._waterLayer.Contains(layer))
			{
				return;
			}
			Rigidbody component = other.GetComponent<Rigidbody>();
			if (component == null)
			{
				return;
			}
			if (this._rigidbodies.Contains(component))
			{
				this._rigidbodies.Remove(component);
			}
		}

		// Token: 0x040055D3 RID: 21971
		[SerializeField]
		private LayerMask _waterLayer = default(LayerMask);

		// Token: 0x040055D4 RID: 21972
		private List<Rigidbody> _rigidbodies = new List<Rigidbody>();
	}
}
