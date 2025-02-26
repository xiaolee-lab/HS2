using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003AF RID: 943
	internal class CutterWorker
	{
		// Token: 0x060010B2 RID: 4274 RVA: 0x00061500 File Offset: 0x0005F900
		public CutterWorker(Core core, CuttingPlane cuttingPlane)
		{
			this.cutter = new MeshCutter();
			this.cutter.Init(512, 512);
			this.newFragments = new HashSet<MeshObject>();
			this.meshToRemove = new HashSet<MeshObject>();
			this.meshSet = new HashSet<MeshObject>();
			this.cuttingPlane = cuttingPlane;
			this.core = core;
			this.thread = new Thread(new ThreadStart(this.ThreadRun));
			this.thread.IsBackground = true;
			this.thread.Start();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0006159C File Offset: 0x0005F99C
		public void Init()
		{
			this.meshSet.Clear();
			this.running = false;
			this.cutAttempt = 0;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000615B9 File Offset: 0x0005F9B9
		public void AddMesh(MeshObject meshObject)
		{
			this.meshSet.Add(meshObject);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000615C8 File Offset: 0x0005F9C8
		public void Run()
		{
			this.running = true;
			Thread.MemoryBarrier();
			this.mre.Set();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000615E4 File Offset: 0x0005F9E4
		private void ThreadRun()
		{
			this.mre.WaitOne();
			try
			{
				this.Cut();
			}
			finally
			{
				this.running = false;
				Thread.MemoryBarrier();
				this.mre.Reset();
				this.thread = new Thread(new ThreadStart(this.ThreadRun));
				this.thread.IsBackground = true;
				this.thread.Start();
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00061664 File Offset: 0x0005FA64
		public bool IsFinished()
		{
			return !this.running;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00061671 File Offset: 0x0005FA71
		public HashSet<MeshObject> GetResults()
		{
			return this.meshSet;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00061679 File Offset: 0x0005FA79
		public void Terminate()
		{
			this.mre.Close();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00061688 File Offset: 0x0005FA88
		private void Cut()
		{
			bool flag = true;
			int num = 0;
			this.cutAttempt = 0;
			while (flag)
			{
				num++;
				if (num > this.core.parameters.TargetFragments)
				{
					break;
				}
				this.newFragments.Clear();
				this.meshToRemove.Clear();
				flag = false;
				foreach (MeshObject item in this.meshSet)
				{
					if (this.core.targetFragments[item.id] > 1)
					{
						Plane plane = this.cuttingPlane.GetPlane(item.mesh, this.cutAttempt);
						bool triangulateHoles = true;
						Color crossSectionVertexColor = Color.white;
						Vector4 crossSectionUV = new Vector4(0f, 0f, 1f, 1f);
						if (item.option)
						{
							triangulateHoles = !item.option.Plane2D;
							crossSectionVertexColor = item.option.CrossSectionVertexColor;
							crossSectionUV = item.option.CrossSectionUV;
							this.core.splitMeshIslands |= item.option.SplitMeshIslands;
						}
						if (this.core.parameters.Use2DCollision)
						{
							triangulateHoles = false;
						}
						List<ExploderMesh> list = null;
						this.cutter.Cut(item.mesh, item.transform, plane, triangulateHoles, this.core.parameters.DisableTriangulation, ref list, crossSectionVertexColor, crossSectionUV);
						flag = true;
						if (list != null)
						{
							foreach (ExploderMesh mesh in list)
							{
								this.newFragments.Add(new MeshObject
								{
									mesh = mesh,
									material = item.material,
									transform = item.transform,
									id = item.id,
									original = item.original,
									skinnedOriginal = item.skinnedOriginal,
									bakeObject = item.bakeObject,
									parent = item.transform.parent,
									position = item.transform.position,
									rotation = item.transform.rotation,
									localScale = item.transform.localScale,
									option = item.option
								});
							}
							this.meshToRemove.Add(item);
							Dictionary<int, int> targetFragments;
							int id;
							(targetFragments = this.core.targetFragments)[id = item.id] = targetFragments[id] - 1;
						}
						else
						{
							this.cutAttempt++;
						}
					}
				}
				this.meshSet.ExceptWith(this.meshToRemove);
				this.meshSet.UnionWith(this.newFragments);
			}
		}

		// Token: 0x04001291 RID: 4753
		private readonly HashSet<MeshObject> newFragments;

		// Token: 0x04001292 RID: 4754
		private readonly HashSet<MeshObject> meshToRemove;

		// Token: 0x04001293 RID: 4755
		private readonly HashSet<MeshObject> meshSet;

		// Token: 0x04001294 RID: 4756
		private readonly MeshCutter cutter;

		// Token: 0x04001295 RID: 4757
		private readonly CuttingPlane cuttingPlane;

		// Token: 0x04001296 RID: 4758
		private readonly Core core;

		// Token: 0x04001297 RID: 4759
		private volatile bool running;

		// Token: 0x04001298 RID: 4760
		private int cutAttempt;

		// Token: 0x04001299 RID: 4761
		private readonly ManualResetEvent mre = new ManualResetEvent(false);

		// Token: 0x0400129A RID: 4762
		private Thread thread;
	}
}
