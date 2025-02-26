using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003B5 RID: 949
	internal class PostprocessExplode : Postprocess
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x000620CA File Offset: 0x000604CA
		public PostprocessExplode(Core Core) : base(Core)
		{
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x000620D3 File Offset: 0x000604D3
		public override TaskType Type
		{
			get
			{
				return TaskType.PostprocessExplode;
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000620D8 File Offset: 0x000604D8
		public override void Init()
		{
			base.Init();
			if (!this.core.splitMeshIslands)
			{
				this.core.postList = new List<MeshObject>(this.core.meshSet);
			}
			int count = this.core.postList.Count;
			if (count == 0)
			{
				return;
			}
			FragmentPool.Instance.Reset(this.core.parameters);
			this.core.pool = FragmentPool.Instance.GetAvailableFragments(count);
			if (this.core.parameters.Callback != null)
			{
				this.core.parameters.Callback((float)base.Watch.ElapsedMilliseconds, ExploderObject.ExplosionState.ExplosionStarted);
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00062190 File Offset: 0x00060590
		public override bool Run(float frameBudget)
		{
			int count = this.core.pool.Count;
			while (this.core.poolIdx < count)
			{
				Fragment fragment = this.core.pool[this.core.poolIdx];
				MeshObject meshObject = this.core.postList[this.core.poolIdx];
				this.core.poolIdx++;
				if (meshObject.original)
				{
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
					if (meshObject.original != this.core.parameters.ExploderGameObject)
					{
						ExploderUtils.SetActiveRecursively(meshObject.original, false);
					}
					else
					{
						ExploderUtils.EnableCollider(meshObject.original, false);
						ExploderUtils.SetVisible(meshObject.original, false);
					}
					if (meshObject.skinnedOriginal && meshObject.skinnedOriginal != this.core.parameters.ExploderGameObject)
					{
						ExploderUtils.SetActiveRecursively(meshObject.skinnedOriginal, false);
					}
					else
					{
						ExploderUtils.EnableCollider(meshObject.skinnedOriginal, false);
						ExploderUtils.SetVisible(meshObject.skinnedOriginal, false);
					}
					if (meshObject.skinnedOriginal && meshObject.bakeObject)
					{
						UnityEngine.Object.Destroy(meshObject.bakeObject, 1f);
					}
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
					fragment.Explode(this.core.parameters);
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
			if (this.core.parameters.DestroyOriginalObject)
			{
				foreach (MeshObject meshObject2 in this.core.postList)
				{
					if (meshObject2.original && !meshObject2.original.GetComponent<Fragment>())
					{
						UnityEngine.Object.Destroy(meshObject2.original);
					}
					if (meshObject2.skinnedOriginal)
					{
						UnityEngine.Object.Destroy(meshObject2.skinnedOriginal);
					}
				}
			}
			if (this.core.parameters.ExplodeSelf && !this.core.parameters.DestroyOriginalObject)
			{
				ExploderUtils.SetActiveRecursively(this.core.parameters.ExploderGameObject, false);
			}
			if (this.core.parameters.HideSelf)
			{
				ExploderUtils.SetActiveRecursively(this.core.parameters.ExploderGameObject, false);
			}
			base.Watch.Stop();
			return true;
		}
	}
}
