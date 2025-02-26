using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000390 RID: 912
	public class DemoSelfExplode : MonoBehaviour
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x0005A749 File Offset: 0x00058B49
		private void Start()
		{
			Application.targetFrameRate = 60;
			if (!this.Camera)
			{
				this.Camera = Camera.main;
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0005A76D File Offset: 0x00058B6D
		private bool IsExplodable(GameObject obj)
		{
			return obj.CompareTag(ExploderObject.Tag);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0005A77C File Offset: 0x00058B7C
		private void Update()
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				Ray ray;
				if (this.Camera)
				{
					ray = this.Camera.ScreenPointToRay(Input.mousePosition);
				}
				else
				{
					ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				}
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit))
				{
					GameObject gameObject = raycastHit.collider.gameObject;
					if (this.IsExplodable(gameObject) && Input.GetMouseButtonDown(0))
					{
						this.ExplodeObject(gameObject);
					}
				}
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0005A810 File Offset: 0x00058C10
		private void ExplodeObject(GameObject obj)
		{
			ExploderObject component = obj.GetComponent<ExploderObject>();
			if (component)
			{
				component.ExplodeObject(base.gameObject, new ExploderObject.OnExplosion(this.OnExplosion));
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0005A847 File Offset: 0x00058C47
		private void OnExplosion(float time, ExploderObject.ExplosionState state)
		{
			if (state == ExploderObject.ExplosionState.ExplosionFinished)
			{
			}
		}

		// Token: 0x040011E7 RID: 4583
		public Camera Camera;
	}
}
