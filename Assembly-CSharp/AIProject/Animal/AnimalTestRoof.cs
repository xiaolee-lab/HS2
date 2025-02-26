using System;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000B9B RID: 2971
	public class AnimalTestRoof : MonoBehaviour
	{
		// Token: 0x060058C1 RID: 22721 RVA: 0x002611FC File Offset: 0x0025F5FC
		private void Start()
		{
			Vector3 position = base.transform.position;
			Vector3 lossyScale = base.transform.lossyScale;
			this.vertex[0].Item1 = Vector3.zero;
			this.vertex[1].Item1 = new Vector3(lossyScale.x * 0.5f, 0f, lossyScale.z * 0.5f);
			this.vertex[2].Item1 = new Vector3(lossyScale.x * 0.5f, 0f, lossyScale.z * -0.5f);
			this.vertex[3].Item1 = new Vector3(lossyScale.x * -0.5f, 0f, lossyScale.z * -0.5f);
			this.vertex[4].Item1 = new Vector3(lossyScale.x * -0.5f, 0f, lossyScale.z * 0.5f);
			int layerMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			for (int i = 0; i < this.vertex.Length; i++)
			{
				this.vertex[i].Item1 = base.transform.position + base.transform.rotation * this.vertex[i].Item1;
				RaycastHit raycastHit;
				this.vertex[i].Item2 = ((!Physics.Raycast(this.vertex[i].Item1, Vector3.down, out raycastHit, 1000f, layerMask)) ? this.vertex[i].Item1 : raycastHit.point);
			}
		}

		// Token: 0x04005142 RID: 20802
		private UnityEx.ValueTuple<Vector3, Vector3>[] vertex = new UnityEx.ValueTuple<Vector3, Vector3>[]
		{
			new UnityEx.ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.zero),
			new UnityEx.ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.zero),
			new UnityEx.ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.zero),
			new UnityEx.ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.zero),
			new UnityEx.ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.zero)
		};
	}
}
