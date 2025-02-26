using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Exploder
{
	// Token: 0x0200039D RID: 925
	internal class CrackedObject
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x0005BA64 File Offset: 0x00059E64
		public CrackedObject(GameObject originalObject, ExploderParams parameters)
		{
			this.originalObject = originalObject;
			this.parameters = parameters;
			this.fractureGrid = new FractureGrid(this);
			this.initPos = originalObject.transform.position;
			this.initRot = originalObject.transform.rotation;
			this.watch = new Stopwatch();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0005BABE File Offset: 0x00059EBE
		public void CalculateFractureGrid()
		{
			this.fractureGrid.CreateGrid();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0005BACC File Offset: 0x00059ECC
		public long Explode()
		{
			int count = this.pool.Count;
			int i = 0;
			if (count == 0)
			{
				return 0L;
			}
			this.watch.Start();
			if (this.parameters.Callback != null)
			{
				this.parameters.Callback(0f, ExploderObject.ExplosionState.ExplosionStarted);
			}
			Vector3 b = Vector3.zero;
			Quaternion rhs = Quaternion.identity;
			if (this.originalObject)
			{
				b = this.originalObject.transform.position - this.initPos;
				rhs = this.originalObject.transform.rotation * Quaternion.Inverse(this.initRot);
			}
			while (i < count)
			{
				Fragment fragment = this.pool[i];
				i++;
				if (this.originalObject != this.parameters.ExploderGameObject)
				{
					ExploderUtils.SetActiveRecursively(this.originalObject, false);
				}
				else
				{
					ExploderUtils.EnableCollider(this.originalObject, false);
					ExploderUtils.SetVisible(this.originalObject, false);
				}
				fragment.transform.position += b;
				fragment.transform.rotation *= rhs;
				fragment.Explode(this.parameters);
			}
			if (this.parameters.DestroyOriginalObject && this.originalObject && !this.originalObject.GetComponent<Fragment>())
			{
				UnityEngine.Object.Destroy(this.originalObject);
			}
			if (this.parameters.ExplodeSelf && !this.parameters.DestroyOriginalObject)
			{
				ExploderUtils.SetActiveRecursively(this.parameters.ExploderGameObject, false);
			}
			if (this.parameters.HideSelf)
			{
				ExploderUtils.SetActiveRecursively(this.parameters.ExploderGameObject, false);
			}
			this.watch.Stop();
			return this.watch.ElapsedMilliseconds;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0005BCC4 File Offset: 0x0005A0C4
		public long ExplodePartial(GameObject gameObject, Vector3 shotDir, Vector3 hitPosition, float bulletSize)
		{
			int count = this.pool.Count;
			int i = 0;
			if (count == 0)
			{
				return 0L;
			}
			this.watch.Start();
			if (this.parameters.Callback != null)
			{
				this.parameters.Callback(0f, ExploderObject.ExplosionState.ExplosionStarted);
			}
			Vector3 vector = Vector3.zero;
			Quaternion quaternion = Quaternion.identity;
			if (this.originalObject)
			{
				vector = this.originalObject.transform.position - this.initPos;
				quaternion = this.originalObject.transform.rotation * Quaternion.Inverse(this.initRot);
			}
			CombineInstance[] array = new CombineInstance[count];
			while (i < count)
			{
				Fragment fragment = this.pool[i];
				array[i].mesh = fragment.meshFilter.sharedMesh;
				array[i].transform = fragment.transform.localToWorldMatrix;
				i++;
			}
			Mesh mesh = new Mesh();
			mesh.CombineMeshes(array, true, false);
			this.originalObject.GetComponent<MeshFilter>().mesh = mesh;
			this.watch.Stop();
			return this.watch.ElapsedMilliseconds;
		}

		// Token: 0x04001216 RID: 4630
		public List<Fragment> pool;

		// Token: 0x04001217 RID: 4631
		private readonly Stopwatch watch;

		// Token: 0x04001218 RID: 4632
		private readonly Quaternion initRot;

		// Token: 0x04001219 RID: 4633
		private readonly Vector3 initPos;

		// Token: 0x0400121A RID: 4634
		private readonly GameObject originalObject;

		// Token: 0x0400121B RID: 4635
		private readonly ExploderParams parameters;

		// Token: 0x0400121C RID: 4636
		private readonly FractureGrid fractureGrid;
	}
}
