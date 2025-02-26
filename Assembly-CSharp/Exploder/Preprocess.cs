using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003B6 RID: 950
	internal class Preprocess : ExploderTask
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x000626C8 File Offset: 0x00060AC8
		public Preprocess(Core Core) : base(Core)
		{
			Core.targetFragments = new Dictionary<int, int>(4);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x000626DD File Offset: 0x00060ADD
		public override TaskType Type
		{
			get
			{
				return TaskType.Preprocess;
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000626E0 File Offset: 0x00060AE0
		public override void Init()
		{
			base.Init();
			this.core.targetFragments.Clear();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000626F8 File Offset: 0x00060AF8
		public override bool Run(float frameBudget)
		{
			List<MeshObject> meshList = this.GetMeshList();
			if (meshList.Count == 0)
			{
				base.Watch.Stop();
				this.core.meshSet.Clear();
				return true;
			}
			this.core.meshSet.Clear();
			foreach (MeshObject item in meshList)
			{
				if (this.core.targetFragments[item.id] > 0)
				{
					this.core.meshSet.Add(item);
				}
			}
			this.core.splitMeshIslands = this.core.parameters.SplitMeshIslands;
			base.Watch.Stop();
			return true;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000627E0 File Offset: 0x00060BE0
		private List<MeshObject> GetMeshList()
		{
			List<GameObject> list;
			if (this.core.parameters.Targets != null)
			{
				list = new List<GameObject>(this.core.parameters.Targets);
			}
			else if (this.core.parameters.DontUseTag)
			{
				UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(Explodable));
				list = new List<GameObject>(array.Length);
				foreach (UnityEngine.Object @object in array)
				{
					Explodable explodable = (Explodable)@object;
					if (explodable)
					{
						list.Add(explodable.gameObject);
					}
				}
			}
			else
			{
				list = new List<GameObject>(GameObject.FindGameObjectsWithTag(ExploderObject.Tag));
			}
			if (this.core.parameters.ExplodeSelf)
			{
				list.Add(this.core.parameters.ExploderGameObject);
			}
			List<MeshObject> list2 = new List<MeshObject>(list.Count);
			int num = 0;
			Vector3 vector = Vector3.zero;
			int num2 = 0;
			foreach (GameObject gameObject in list)
			{
				if (gameObject)
				{
					if (this.core.parameters.ExplodeSelf || !(gameObject == this.core.parameters.ExploderGameObject))
					{
						if (!(gameObject != this.core.parameters.ExploderGameObject) || !this.core.parameters.ExplodeSelf || !this.core.parameters.DisableRadiusScan)
						{
							if (this.core.parameters.Targets != null || this.IsInRadius(gameObject))
							{
								List<Preprocess.MeshData> meshData = this.GetMeshData(gameObject);
								int count = meshData.Count;
								for (int j = 0; j < count; j++)
								{
									Vector3 centroid = meshData[j].centroid;
									if (this.core.parameters.Targets != null)
									{
										this.core.parameters.Position = centroid;
										vector += centroid;
										num2++;
									}
									float magnitude = (centroid - this.core.parameters.Position).magnitude;
									list2.Add(new MeshObject
									{
										id = num++,
										mesh = new ExploderMesh(meshData[j].sharedMesh),
										material = meshData[j].sharedMaterial,
										transform = new ExploderTransform(meshData[j].gameObject.transform),
										parent = meshData[j].gameObject.transform.parent,
										position = meshData[j].gameObject.transform.position,
										rotation = meshData[j].gameObject.transform.rotation,
										localScale = meshData[j].gameObject.transform.localScale,
										bakeObject = meshData[j].gameObject,
										distanceRatio = this.GetDistanceRatio(magnitude, this.core.parameters.Radius),
										original = meshData[j].parentObject,
										skinnedOriginal = meshData[j].skinnedBakeOriginal,
										option = gameObject.GetComponent<ExploderOption>()
									});
								}
							}
						}
					}
				}
			}
			if (num2 > 0)
			{
				vector /= (float)num2;
				this.core.parameters.Position = vector;
			}
			if (list2.Count == 0)
			{
				return list2;
			}
			if (this.core.parameters.UniformFragmentDistribution || this.core.parameters.Targets != null)
			{
				int num3 = this.core.parameters.TargetFragments / list2.Count;
				int k = this.core.parameters.TargetFragments;
				foreach (MeshObject meshObject in list2)
				{
					this.core.targetFragments[meshObject.id] = num3;
					k -= num3;
				}
				while (k > 0)
				{
					k--;
					MeshObject meshObject2 = list2[UnityEngine.Random.Range(0, list2.Count - 1)];
					Dictionary<int, int> targetFragments;
					int id;
					(targetFragments = this.core.targetFragments)[id = meshObject2.id] = targetFragments[id] + 1;
				}
			}
			else
			{
				float num4 = 0f;
				int num5 = 0;
				foreach (MeshObject meshObject3 in list2)
				{
					num4 += meshObject3.distanceRatio;
				}
				foreach (MeshObject meshObject4 in list2)
				{
					this.core.targetFragments[meshObject4.id] = (int)(meshObject4.distanceRatio / num4 * (float)this.core.parameters.TargetFragments);
					num5 += this.core.targetFragments[meshObject4.id];
				}
				if (num5 < this.core.parameters.TargetFragments)
				{
					int l = this.core.parameters.TargetFragments - num5;
					while (l > 0)
					{
						foreach (MeshObject meshObject5 in list2)
						{
							Dictionary<int, int> targetFragments;
							int id2;
							(targetFragments = this.core.targetFragments)[id2 = meshObject5.id] = targetFragments[id2] + 1;
							l--;
							if (l == 0)
							{
								break;
							}
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00062F18 File Offset: 0x00061318
		private List<Preprocess.MeshData> GetMeshData(GameObject obj)
		{
			MeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>();
			MeshFilter[] componentsInChildren2 = obj.GetComponentsInChildren<MeshFilter>();
			if (componentsInChildren.Length != componentsInChildren2.Length)
			{
				return new List<Preprocess.MeshData>();
			}
			List<Preprocess.MeshData> list = new List<Preprocess.MeshData>(componentsInChildren.Length);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (!(componentsInChildren2[i].sharedMesh == null))
				{
					if (!componentsInChildren2[i].sharedMesh || !componentsInChildren2[i].sharedMesh.isReadable)
					{
						UnityEngine.Debug.LogWarning("Mesh is not readable: " + componentsInChildren2[i].name);
					}
					else
					{
						list.Add(new Preprocess.MeshData
						{
							sharedMesh = componentsInChildren2[i].sharedMesh,
							sharedMaterial = componentsInChildren[i].sharedMaterial,
							gameObject = componentsInChildren[i].gameObject,
							centroid = componentsInChildren[i].bounds.center,
							parentObject = obj
						});
					}
				}
			}
			SkinnedMeshRenderer[] componentsInChildren3 = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int j = 0; j < componentsInChildren3.Length; j++)
			{
				Mesh mesh = new Mesh();
				componentsInChildren3[j].BakeMesh(mesh);
				GameObject gameObject = this.core.bakeSkinManager.CreateBakeObject(obj.name);
				MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
				meshFilter.sharedMesh = mesh;
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				meshRenderer.sharedMaterial = componentsInChildren3[j].material;
				gameObject.transform.position = componentsInChildren3[j].gameObject.transform.position;
				gameObject.transform.rotation = componentsInChildren3[j].gameObject.transform.rotation;
				ExploderUtils.SetVisible(gameObject, false);
				list.Add(new Preprocess.MeshData
				{
					sharedMesh = mesh,
					sharedMaterial = meshRenderer.sharedMaterial,
					gameObject = gameObject,
					centroid = meshRenderer.bounds.center,
					parentObject = gameObject,
					skinnedBakeOriginal = obj
				});
			}
			return list;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0006312F File Offset: 0x0006152F
		private float GetDistanceRatio(float distance, float radius)
		{
			return 1f - Mathf.Clamp01(distance / radius);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00063140 File Offset: 0x00061540
		private bool IsInRadius(GameObject o)
		{
			Vector3 centroid = ExploderUtils.GetCentroid(o);
			if (this.core.parameters.UseCubeRadius)
			{
				Vector3 vector = this.core.parameters.ExploderGameObject.transform.InverseTransformPoint(centroid);
				Vector3 vector2 = this.core.parameters.ExploderGameObject.transform.InverseTransformPoint(this.core.parameters.Position);
				return Mathf.Abs(vector.x - vector2.x) < this.core.parameters.CubeRadius.x && Mathf.Abs(vector.y - vector2.y) < this.core.parameters.CubeRadius.y && Mathf.Abs(vector.z - vector2.z) < this.core.parameters.CubeRadius.z;
			}
			return this.core.parameters.Radius * this.core.parameters.Radius > (centroid - this.core.parameters.Position).sqrMagnitude;
		}

		// Token: 0x020003B7 RID: 951
		private struct MeshData
		{
			// Token: 0x040012A7 RID: 4775
			public Mesh sharedMesh;

			// Token: 0x040012A8 RID: 4776
			public Material sharedMaterial;

			// Token: 0x040012A9 RID: 4777
			public GameObject gameObject;

			// Token: 0x040012AA RID: 4778
			public GameObject parentObject;

			// Token: 0x040012AB RID: 4779
			public GameObject skinnedBakeOriginal;

			// Token: 0x040012AC RID: 4780
			public Vector3 centroid;
		}
	}
}
