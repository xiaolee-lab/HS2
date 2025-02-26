using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003B4 RID: 948
	internal class PostprocessCrack : Postprocess
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x00061C4E File Offset: 0x0006004E
		public PostprocessCrack(Core Core) : base(Core)
		{
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00061C57 File Offset: 0x00060057
		public override TaskType Type
		{
			get
			{
				return TaskType.PostprocessCrack;
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00061C5C File Offset: 0x0006005C
		public override void Init()
		{
			base.Init();
			FragmentPool.Instance.ResetTransform();
			FragmentPool.Instance.Reset(this.core.parameters);
			this.crackedObject = null;
			if (this.core.meshSet.Count > 0)
			{
				if (!this.core.splitMeshIslands)
				{
					this.core.postList = new List<MeshObject>(this.core.meshSet);
				}
				GameObject originalObject = (!this.core.postList[0].skinnedOriginal) ? this.core.postList[0].original : this.core.postList[0].skinnedOriginal;
				this.crackedObject = this.core.crackManager.Create(originalObject, this.core.parameters);
				this.crackedObject.pool = FragmentPool.Instance.GetAvailableFragments(this.core.postList.Count);
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00061D78 File Offset: 0x00060178
		public override bool Run(float frameBudget)
		{
			if (this.crackedObject == null)
			{
				return true;
			}
			int count = this.crackedObject.pool.Count;
			while (this.core.poolIdx < count)
			{
				Fragment fragment = this.crackedObject.pool[this.core.poolIdx];
				MeshObject meshObject = this.core.postList[this.core.poolIdx];
				this.core.poolIdx++;
				if (meshObject.original)
				{
					ExploderUtils.SetActiveRecursively(fragment.gameObject, false);
					Mesh mesh = meshObject.mesh.ToUnityMesh();
					fragment.AssignMesh(mesh);
					if (meshObject.option && meshObject.option.FragmentMaterial)
					{
						fragment.meshRenderer.sharedMaterial = meshObject.option.FragmentMaterial;
					}
					else if (this.core.parameters.FragmentOptions.FragmentMaterial != null)
					{
						fragment.meshRenderer.sharedMaterial = this.core.parameters.FragmentOptions.FragmentMaterial;
					}
					else
					{
						fragment.meshRenderer.sharedMaterial = meshObject.material;
					}
					mesh.RecalculateBounds();
					Transform parent = fragment.transform.parent;
					fragment.transform.parent = meshObject.parent;
					fragment.transform.position = meshObject.position;
					fragment.transform.rotation = meshObject.rotation;
					fragment.transform.localScale = meshObject.localScale;
					fragment.transform.parent = null;
					fragment.transform.parent = parent;
					fragment.Cracked = true;
					bool flag = meshObject.option && meshObject.option.Plane2D;
					bool use2DCollision = this.core.parameters.Use2DCollision;
					if (!this.core.parameters.FragmentOptions.DisableColliders)
					{
						if (this.core.parameters.FragmentOptions.MeshColliders && !use2DCollision)
						{
							if (!flag)
							{
								fragment.meshCollider.sharedMesh = mesh;
							}
						}
						else if (this.core.parameters.Use2DCollision)
						{
							MeshUtils.GeneratePolygonCollider(fragment.polygonCollider2D, mesh);
						}
						else
						{
							fragment.boxCollider.center = mesh.bounds.center;
							fragment.boxCollider.size = mesh.bounds.extents;
						}
					}
					float force = this.core.parameters.Force;
					if (meshObject.option && meshObject.option.UseLocalForce)
					{
						force = meshObject.option.Force;
					}
					fragment.ApplyExplosion(meshObject.transform, meshObject.mesh.centroid, force, meshObject.original, this.core.parameters);
					if ((float)base.Watch.ElapsedMilliseconds > frameBudget)
					{
						return false;
					}
				}
			}
			this.crackedObject.CalculateFractureGrid();
			base.Watch.Stop();
			return true;
		}

		// Token: 0x040012A6 RID: 4774
		private CrackedObject crackedObject;
	}
}
