using System;
using System.Collections;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020003EF RID: 1007
	public class MeshBrushParent : MonoBehaviour
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x0006F39E File Offset: 0x0006D79E
		private void Initialize()
		{
			this.paintedMeshes = base.GetComponentsInChildren<Transform>();
			this.meshFilters = base.GetComponentsInChildren<MeshFilter>();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0006F3B8 File Offset: 0x0006D7B8
		public void FlagMeshesAsStatic()
		{
			this.Initialize();
			for (int i = this.paintedMeshes.Length - 1; i >= 0; i--)
			{
				this.paintedMeshes[i].gameObject.isStatic = true;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0006F3FC File Offset: 0x0006D7FC
		public void UnflagMeshesAsStatic()
		{
			this.Initialize();
			for (int i = this.paintedMeshes.Length - 1; i >= 0; i--)
			{
				this.paintedMeshes[i].gameObject.isStatic = false;
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0006F43D File Offset: 0x0006D83D
		public int GetMeshCount()
		{
			this.Initialize();
			return this.meshFilters.Length;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0006F450 File Offset: 0x0006D850
		public int GetTrisCount()
		{
			this.Initialize();
			if (this.meshFilters.Length > 0)
			{
				int num = 0;
				for (int i = this.meshFilters.Length - 1; i >= 0; i--)
				{
					num += this.meshFilters[i].sharedMesh.triangles.Length;
				}
				return num / 3;
			}
			return 0;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0006F4AA File Offset: 0x0006D8AA
		public void DeleteAllMeshes()
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0006F4B8 File Offset: 0x0006D8B8
		public void CombinePaintedMeshes(bool autoSelect, MeshFilter[] meshFilters)
		{
			if (meshFilters == null || meshFilters.Length == 0)
			{
				return;
			}
			this.localTransformationMatrix = base.transform.worldToLocalMatrix;
			this.materialToMesh = new Hashtable();
			int num = 0;
			for (long num2 = 0L; num2 < meshFilters.LongLength; num2 += 1L)
			{
				this.currentMeshFilter = meshFilters[(int)(checked((IntPtr)num2))];
				num += this.currentMeshFilter.sharedMesh.vertexCount;
				if (num > 64000)
				{
					return;
				}
			}
			for (long num3 = 0L; num3 < meshFilters.LongLength; num3 += 1L)
			{
				checked
				{
					this.currentMeshFilter = meshFilters[(int)((IntPtr)num3)];
					this.currentRenderer = meshFilters[(int)((IntPtr)num3)].GetComponent<Renderer>();
					this.instance = default(CombineUtility.MeshInstance);
					this.instance.mesh = this.currentMeshFilter.sharedMesh;
				}
				if (this.currentRenderer != null && this.currentRenderer.enabled && this.instance.mesh != null)
				{
					this.instance.transform = this.localTransformationMatrix * this.currentMeshFilter.transform.localToWorldMatrix;
					this.materials = this.currentRenderer.sharedMaterials;
					for (int i = 0; i < this.materials.Length; i++)
					{
						this.instance.subMeshIndex = Math.Min(i, this.instance.mesh.subMeshCount - 1);
						this.objects = (ArrayList)this.materialToMesh[this.materials[i]];
						if (this.objects != null)
						{
							this.objects.Add(this.instance);
						}
						else
						{
							this.objects = new ArrayList();
							this.objects.Add(this.instance);
							this.materialToMesh.Add(this.materials[i], this.objects);
						}
					}
					UnityEngine.Object.DestroyImmediate(this.currentRenderer.gameObject);
				}
			}
			IDictionaryEnumerator enumerator = this.materialToMesh.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this.elements = (ArrayList)dictionaryEntry.Value;
					this.instances = (CombineUtility.MeshInstance[])this.elements.ToArray(typeof(CombineUtility.MeshInstance));
					GameObject gameObject = new GameObject("Combined mesh");
					gameObject.transform.parent = base.transform;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.identity;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.AddComponent<MeshFilter>();
					gameObject.AddComponent<MeshRenderer>();
					gameObject.AddComponent<SaveCombinedMesh>();
					gameObject.GetComponent<Renderer>().material = (Material)dictionaryEntry.Key;
					gameObject.isStatic = true;
					this.currentMeshFilter = gameObject.GetComponent<MeshFilter>();
					this.currentMeshFilter.mesh = CombineUtility.Combine(this.instances, false);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			base.gameObject.isStatic = true;
		}

		// Token: 0x04001477 RID: 5239
		private Transform[] paintedMeshes;

		// Token: 0x04001478 RID: 5240
		private MeshFilter[] meshFilters;

		// Token: 0x04001479 RID: 5241
		private Matrix4x4 localTransformationMatrix;

		// Token: 0x0400147A RID: 5242
		private Hashtable materialToMesh;

		// Token: 0x0400147B RID: 5243
		private MeshFilter currentMeshFilter;

		// Token: 0x0400147C RID: 5244
		private Renderer currentRenderer;

		// Token: 0x0400147D RID: 5245
		private Material[] materials;

		// Token: 0x0400147E RID: 5246
		private CombineUtility.MeshInstance instance;

		// Token: 0x0400147F RID: 5247
		private CombineUtility.MeshInstance[] instances;

		// Token: 0x04001480 RID: 5248
		private ArrayList objects;

		// Token: 0x04001481 RID: 5249
		private ArrayList elements;
	}
}
