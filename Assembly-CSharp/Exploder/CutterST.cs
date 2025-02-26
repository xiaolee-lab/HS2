using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003AE RID: 942
	internal class CutterST : ExploderTask
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x00060A44 File Offset: 0x0005EE44
		public CutterST(Core Core) : base(Core)
		{
			this.cutter = new MeshCutter();
			this.cutter.Init(512, 512);
			this.newFragments = new HashSet<MeshObject>();
			this.meshToRemove = new HashSet<MeshObject>();
			this.cuttingPlane = new CuttingPlane(Core);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00060A9A File Offset: 0x0005EE9A
		public override TaskType Type
		{
			get
			{
				return TaskType.ProcessCutter;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00060A9D File Offset: 0x0005EE9D
		public override void Init()
		{
			base.Init();
			this.newFragments.Clear();
			this.meshToRemove.Clear();
			this.cutAttempt = 0;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00060AC2 File Offset: 0x0005EEC2
		public override bool Run(float frameBudget)
		{
			if (this.Cut(frameBudget))
			{
				base.Watch.Stop();
				return true;
			}
			return false;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00060AE0 File Offset: 0x0005EEE0
		protected virtual bool Cut(float frameBudget)
		{
			bool flag = true;
			int num = 0;
			bool flag2 = false;
			this.cutAttempt = 0;
			while (flag)
			{
				num++;
				if (num > this.core.parameters.TargetFragments)
				{
					return true;
				}
				this.newFragments.Clear();
				this.meshToRemove.Clear();
				flag = false;
				foreach (MeshObject meshObject in this.core.meshSet)
				{
					if (!this.core.targetFragments.ContainsKey(meshObject.id))
					{
					}
					if (this.core.targetFragments[meshObject.id] > 1)
					{
						List<ExploderMesh> list = this.CutSingleMesh(meshObject);
						flag = true;
						if (list != null)
						{
							foreach (ExploderMesh mesh in list)
							{
								this.newFragments.Add(new MeshObject
								{
									mesh = mesh,
									material = meshObject.material,
									transform = meshObject.transform,
									id = meshObject.id,
									original = meshObject.original,
									skinnedOriginal = meshObject.skinnedOriginal,
									bakeObject = meshObject.bakeObject,
									parent = meshObject.transform.parent,
									position = meshObject.transform.position,
									rotation = meshObject.transform.rotation,
									localScale = meshObject.transform.localScale,
									option = meshObject.option
								});
							}
							this.meshToRemove.Add(meshObject);
							Dictionary<int, int> targetFragments;
							int id;
							(targetFragments = this.core.targetFragments)[id = meshObject.id] = targetFragments[id] - 1;
							if ((float)base.Watch.ElapsedMilliseconds > frameBudget && num > 2)
							{
								flag2 = true;
								break;
							}
						}
					}
				}
				this.core.meshSet.ExceptWith(this.meshToRemove);
				this.core.meshSet.UnionWith(this.newFragments);
				if (flag2)
				{
					break;
				}
			}
			return !flag2;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00060D8C File Offset: 0x0005F18C
		protected List<ExploderMesh> CutSingleMesh(MeshObject mesh)
		{
			Plane plane = this.cuttingPlane.GetPlane(mesh.mesh, this.cutAttempt);
			bool flag = true;
			Color crossSectionVertexColor = Color.white;
			Vector4 crossSectionUV = new Vector4(0f, 0f, 1f, 1f);
			if (mesh.option)
			{
				flag = !mesh.option.Plane2D;
				crossSectionVertexColor = mesh.option.CrossSectionVertexColor;
				crossSectionUV = mesh.option.CrossSectionUV;
				this.core.splitMeshIslands |= mesh.option.SplitMeshIslands;
			}
			if (this.core.parameters.Use2DCollision)
			{
				flag = false;
			}
			flag &= !this.core.parameters.DisableTriangulation;
			List<ExploderMesh> list = null;
			this.cutter.Cut(mesh.mesh, mesh.transform, plane, flag, this.core.parameters.DisableTriangulation, ref list, crossSectionVertexColor, crossSectionUV);
			if (list == null)
			{
				this.cutAttempt++;
			}
			return list;
		}

		// Token: 0x0400128C RID: 4748
		protected readonly HashSet<MeshObject> newFragments;

		// Token: 0x0400128D RID: 4749
		protected readonly HashSet<MeshObject> meshToRemove;

		// Token: 0x0400128E RID: 4750
		protected readonly MeshCutter cutter;

		// Token: 0x0400128F RID: 4751
		protected readonly CuttingPlane cuttingPlane;

		// Token: 0x04001290 RID: 4752
		private int cutAttempt;
	}
}
