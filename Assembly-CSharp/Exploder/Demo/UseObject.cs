using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200038C RID: 908
	public abstract class UseObject : MonoBehaviour
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x00059340 File Offset: 0x00057740
		public virtual void Use()
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (component && this.UseClip)
			{
				component.PlayOneShot(this.UseClip);
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0005937B File Offset: 0x0005777B
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(base.transform.position, this.UseRadius);
		}

		// Token: 0x040011DA RID: 4570
		public float UseRadius = 5f;

		// Token: 0x040011DB RID: 4571
		public string HelperText = string.Empty;

		// Token: 0x040011DC RID: 4572
		public AudioClip UseClip;
	}
}
