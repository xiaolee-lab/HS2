using System;
using PicoGames.QuickRopes;
using UnityEngine;

namespace PicoGames.Utilities
{
	// Token: 0x02000A72 RID: 2674
	[AddComponentMenu("PicoGames/QuickRopes/Extensions/Rope Renderer")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(QuickRope))]
	public class RopeRenderer : MonoBehaviour
	{
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x001E5F44 File Offset: 0x001E4344
		// (set) Token: 0x06004F1B RID: 20251 RVA: 0x001E5F4C File Offset: 0x001E434C
		public int EdgeCount
		{
			get
			{
				return this.leafs;
			}
			set
			{
				if (this.leafs == value)
				{
					return;
				}
				this.flagShapeUpdate = true;
				this.leafs = value;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x001E5F69 File Offset: 0x001E4369
		// (set) Token: 0x06004F1D RID: 20253 RVA: 0x001E5F71 File Offset: 0x001E4371
		public int EdgeDetail
		{
			get
			{
				return this.detail;
			}
			set
			{
				if (this.detail == value)
				{
					return;
				}
				this.flagShapeUpdate = true;
				this.detail = value;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x001E5F8E File Offset: 0x001E438E
		// (set) Token: 0x06004F1F RID: 20255 RVA: 0x001E5F96 File Offset: 0x001E4396
		public float EdgeIndent
		{
			get
			{
				return this.center;
			}
			set
			{
				if (this.center == value)
				{
					return;
				}
				this.flagShapeUpdate = true;
				this.center = value;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x001E5FB3 File Offset: 0x001E43B3
		// (set) Token: 0x06004F21 RID: 20257 RVA: 0x001E5FBB File Offset: 0x001E43BB
		public int StrandCount
		{
			get
			{
				return this.strandCount;
			}
			set
			{
				if (this.strandCount == value)
				{
					return;
				}
				this.strandCount = value;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x001E5FD1 File Offset: 0x001E43D1
		// (set) Token: 0x06004F23 RID: 20259 RVA: 0x001E5FD9 File Offset: 0x001E43D9
		public float StrandOffset
		{
			get
			{
				return this.strandOffset;
			}
			set
			{
				if (this.strandOffset == value)
				{
					return;
				}
				this.strandOffset = value;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x001E5FEF File Offset: 0x001E43EF
		// (set) Token: 0x06004F25 RID: 20261 RVA: 0x001E5FF7 File Offset: 0x001E43F7
		public float StrandTwist
		{
			get
			{
				return this.twistAngle;
			}
			set
			{
				if (this.twistAngle == value)
				{
					return;
				}
				this.twistAngle = value;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x001E600D File Offset: 0x001E440D
		// (set) Token: 0x06004F27 RID: 20263 RVA: 0x001E6015 File Offset: 0x001E4415
		public float Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				if (this.radius == value)
				{
					return;
				}
				this.radius = value;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x001E602B File Offset: 0x001E442B
		// (set) Token: 0x06004F29 RID: 20265 RVA: 0x001E6033 File Offset: 0x001E4433
		public AnimationCurve RadiusCurve
		{
			get
			{
				return this.radiusCurve;
			}
			set
			{
				this.radiusCurve = value;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06004F2A RID: 20266 RVA: 0x001E603C File Offset: 0x001E443C
		// (set) Token: 0x06004F2B RID: 20267 RVA: 0x001E6044 File Offset: 0x001E4444
		public Material Material
		{
			get
			{
				return this.material;
			}
			set
			{
				this.material = value;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x001E604D File Offset: 0x001E444D
		// (set) Token: 0x06004F2D RID: 20269 RVA: 0x001E6055 File Offset: 0x001E4455
		public Vector2 UVOffset
		{
			get
			{
				return this.uvOffset;
			}
			set
			{
				this.uvOffset = value;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x001E605E File Offset: 0x001E445E
		// (set) Token: 0x06004F2F RID: 20271 RVA: 0x001E6066 File Offset: 0x001E4466
		public Vector2 UVTile
		{
			get
			{
				return this.uvTile;
			}
			set
			{
				this.uvTile = value;
			}
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001E6070 File Offset: 0x001E4470
		private void OnDrawGizmos()
		{
			if (this.mesh != null && this.showBounds)
			{
				Gizmos.color = Color.gray;
				Gizmos.DrawWireCube(this.mesh.bounds.center, this.mesh.bounds.size);
			}
			if (this.vertices != null && (this.showEdges || this.showNormals))
			{
				for (int i = 0; i < this.vertices.Length; i++)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawWireCube(this.vertices[i], Vector3.one * 0.01f);
					if (this.showNormals)
					{
						Gizmos.color = Color.magenta;
						Gizmos.DrawRay(this.vertices[i], this.normals[i]);
					}
				}
			}
			if (this.showEdges)
			{
				Gizmos.color = Color.blue;
				for (int j = 0; j < this.strandCount; j++)
				{
					for (int k = 0; k < this.positions.Length; k++)
					{
						for (int l = 0; l < this.edgeCount + 1; l++)
						{
							Gizmos.DrawLine(this.vertices[l + k * (this.edgeCount + 1)], this.vertices[(l + 1) % this.edgeCount + k * (this.edgeCount + 1)]);
						}
					}
				}
			}
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x001E6224 File Offset: 0x001E4624
		private void OnDestroy()
		{
			if (this.meshObject != null)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(this.mesh);
					UnityEngine.Object.Destroy(this.meshObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.mesh);
					UnityEngine.Object.DestroyImmediate(this.meshObject);
				}
			}
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x001E6280 File Offset: 0x001E4680
		private void OnDisable()
		{
			if (this.meshObject != null)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(this.mesh);
					UnityEngine.Object.Destroy(this.meshObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.mesh);
					UnityEngine.Object.DestroyImmediate(this.meshObject);
				}
			}
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x001E62D9 File Offset: 0x001E46D9
		private void Start()
		{
			this.rope = base.gameObject.GetComponent<QuickRope>();
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x001E62EC File Offset: 0x001E46EC
		public void LateUpdate()
		{
			if (Application.isPlaying && this.dontRedraw)
			{
				return;
			}
			if (this.flagShapeUpdate)
			{
				this.UpdateShape();
			}
			this.UpdatePositions();
			this.UpdateRotations();
			this.RedrawMesh();
			if (base.gameObject.isStatic)
			{
				this.dontRedraw = true;
			}
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x001E634C File Offset: 0x001E474C
		private void VerifyMeshExists()
		{
			if (this.meshObject == null)
			{
				this.meshObject = GameObject.Find("Rope_Obj_" + base.gameObject.GetInstanceID());
				if (this.meshObject == null)
				{
					this.meshObject = new GameObject("Rope_Obj_" + base.gameObject.GetInstanceID(), new Type[]
					{
						typeof(MeshFilter),
						typeof(MeshRenderer)
					});
				}
				this.meshObject.hideFlags = HideFlags.HideInHierarchy;
			}
			if (this.mesh == null)
			{
				this.mesh = new Mesh();
				this.mesh.hideFlags = HideFlags.DontSave;
			}
			if (this.mFilter == null)
			{
				this.mFilter = this.meshObject.GetComponent<MeshFilter>();
				if (this.mFilter == null)
				{
					this.mFilter = this.meshObject.AddComponent<MeshFilter>();
				}
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = this.meshObject.GetComponent<MeshRenderer>();
				if (this.mRenderer == null)
				{
					this.mRenderer = this.meshObject.AddComponent<MeshRenderer>();
				}
			}
			if (this.material == null)
			{
				this.material = new Material(Shader.Find("Standard"));
			}
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x001E64C8 File Offset: 0x001E48C8
		private void RedrawMesh()
		{
			this.strandCount = Mathf.Max(1, this.strandCount);
			this.edgeCount = Mathf.Max(3, this.edgeCount);
			this.detail = Mathf.Max(1, this.detail);
			this.VerifyMeshExists();
			int num = (this.edgeCount + 1) * this.positions.Length * this.strandCount + this.shapeLookup.Length * this.strandCount * 2;
			int num2 = 6 * this.edgeCount * this.positions.Length * this.strandCount + this.shapeTriIndices.Length * this.strandCount * 2;
			if (this.vertices == null || this.vertices.Length != num)
			{
				this.vertices = new Vector3[num];
			}
			if (this.normals == null || this.normals.Length != num)
			{
				this.normals = new Vector3[num];
			}
			if (this.uvs == null || this.uvs.Length != num)
			{
				this.uvs = new Vector2[num];
			}
			if (this.triangles == null || this.triangles.Length != num2)
			{
				this.triangles = new int[num2];
			}
			Vector3 lhs = Vector3.one * float.MaxValue;
			Vector3 lhs2 = Vector3.one * float.MinValue;
			RopeRenderer.vertIndex = (RopeRenderer.triaIndex = 0);
			Matrix4x4 matrix4x = default(Matrix4x4);
			for (int i = 0; i < this.strandCount; i++)
			{
				float f = 360f / (float)this.strandCount * (float)i * 0.017453292f;
				Vector3 point = new Vector3(Mathf.Cos(f), Mathf.Sin(f), 0f);
				int num3 = RopeRenderer.vertIndex;
				for (int j = 0; j < this.positions.Length; j++)
				{
					float num4 = this.radiusCurve.Evaluate((float)j * (1f / (float)this.positions.Length)) * this.radius;
					matrix4x.SetTRS(this.positions[j] + ((this.strandCount <= 1) ? Vector3.zero : (this.rotations[j] * point * (num4 * this.strandOffset))), this.rotations[j], Vector3.one * num4);
					for (int k = 0; k < this.edgeCount + 1; k++)
					{
						int num5 = k % this.shapeLookup.Length;
						this.vertices[RopeRenderer.vertIndex] = matrix4x.MultiplyPoint3x4(this.shapeLookup[num5]);
						this.normals[RopeRenderer.vertIndex] = this.rotations[j] * this.shapeLookup[num5];
						this.uvs[RopeRenderer.vertIndex] = new Vector2((float)k / (float)this.edgeCount * (float)this.edgeCount * this.uvTile.x + this.uvOffset.x, (float)j / (float)(this.positions.Length - 1) * (float)this.positions.Length * this.uvTile.y + this.uvOffset.y);
						lhs = Vector3.Min(lhs, this.vertices[RopeRenderer.vertIndex]);
						lhs2 = Vector3.Max(lhs2, this.vertices[RopeRenderer.vertIndex]);
						if (j < this.positions.Length - 1 && k < this.edgeCount && this.isJoints[j])
						{
							this.triangles[RopeRenderer.triaIndex++] = RopeRenderer.vertIndex;
							this.triangles[RopeRenderer.triaIndex++] = RopeRenderer.vertIndex + 1;
							this.triangles[RopeRenderer.triaIndex++] = RopeRenderer.vertIndex + this.edgeCount + 1;
							this.triangles[RopeRenderer.triaIndex++] = RopeRenderer.vertIndex + this.edgeCount + 1;
							this.triangles[RopeRenderer.triaIndex++] = RopeRenderer.vertIndex + 1;
							this.triangles[RopeRenderer.triaIndex++] = RopeRenderer.vertIndex + 1 + this.edgeCount + 1;
						}
						RopeRenderer.vertIndex++;
					}
				}
				int num6 = RopeRenderer.vertIndex - 1;
				for (int l = 0; l < this.shapeLookup.Length; l++)
				{
					this.vertices[RopeRenderer.vertIndex] = this.vertices[num3 + l];
					this.vertices[RopeRenderer.vertIndex + this.shapeLookup.Length] = this.vertices[num6 - l];
					this.normals[RopeRenderer.vertIndex] = this.rotations[0] * Vector3.back;
					this.normals[RopeRenderer.vertIndex + this.shapeLookup.Length] = this.rotations[this.rotations.Length - 1] * Vector3.forward;
					this.uvs[RopeRenderer.vertIndex] = new Vector2(this.shapeLookup[l].x, this.shapeLookup[l].y);
					this.uvs[RopeRenderer.vertIndex + this.shapeLookup.Length] = new Vector2(this.shapeLookup[l].x, this.shapeLookup[l].y);
					RopeRenderer.vertIndex++;
				}
				RopeRenderer.vertIndex += this.shapeLookup.Length;
				for (int m = 0; m < this.shapeTriIndices.Length; m++)
				{
					this.triangles[RopeRenderer.triaIndex] = num6 + 1 + this.shapeTriIndices[m];
					this.triangles[RopeRenderer.triaIndex + this.shapeTriIndices.Length] = num6 + this.shapeLookup.Length + 1 + this.shapeTriIndices[m];
					RopeRenderer.triaIndex++;
				}
				RopeRenderer.triaIndex += this.shapeTriIndices.Length;
			}
			this.mesh.Clear();
			this.mesh.MarkDynamic();
			this.mesh.vertices = this.vertices;
			this.mesh.triangles = this.triangles;
			this.mesh.normals = this.normals;
			this.mesh.uv = this.uvs;
			Vector3 size = new Vector3(lhs2.x - lhs.x, lhs2.y - lhs.y, lhs2.z - lhs.z);
			Vector3 vector = new Vector3(lhs.x + size.x * 0.5f, lhs.y + size.y * 0.5f, lhs.z + size.z * 0.5f);
			this.mesh.bounds = new Bounds(vector, size);
			this.mFilter.sharedMesh = this.mesh;
			Material sharedMaterial = this.material;
			this.mRenderer.sharedMaterial = sharedMaterial;
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x001E6CA0 File Offset: 0x001E50A0
		private void UpdatePositions()
		{
			int num = this.rope.ActiveLinkCount + 1;
			if (this.positions == null || this.positions.Length != num)
			{
				Array.Resize<Vector3>(ref this.positions, num);
				Array.Resize<bool>(ref this.isJoints, num);
			}
			for (int i = 0; i <= this.rope.ActiveLinkCount; i++)
			{
				this.positions[i] = this.rope.Links[i].transform.position;
				this.isJoints[i] = ((!this.rope.Links[i].transform.GetComponent<ConfigurableJoint>()) ? false : true);
			}
			if (this.rope.Spline.IsLooped)
			{
				this.positions[this.positions.Length - 1] = this.positions[0];
			}
			else
			{
				this.positions[this.positions.Length - 1] = this.rope.Links[this.rope.Links.Length - 1].transform.position;
			}
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x001E6DE4 File Offset: 0x001E51E4
		private void UpdateShape()
		{
			this.shapeLookup = Shape.GetRoseCurve(this.leafs, this.detail, this.center, true);
			this.shapeTriIndices = Triangulate.Edge(this.shapeLookup);
			this.edgeCount = this.shapeLookup.Length;
			this.flagShapeUpdate = false;
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x001E6E38 File Offset: 0x001E5238
		private void UpdateRotations()
		{
			if (this.rotations == null || this.rotations.Length != this.positions.Length)
			{
				Array.Resize<Quaternion>(ref this.rotations, this.positions.Length);
			}
			if (this.directions == null || this.directions.Length != this.positions.Length)
			{
				Array.Resize<Vector3>(ref this.directions, this.positions.Length);
			}
			for (int i = 0; i < this.positions.Length - 1; i++)
			{
				this.directions[i].Set(this.positions[i + 1].x - this.positions[i].x, this.positions[i + 1].y - this.positions[i].y, this.positions[i + 1].z - this.positions[i].z);
			}
			this.directions[this.directions.Length - 1] = this.directions[this.directions.Length - 2];
			Vector3 zero = Vector3.zero;
			Vector3 vector = (!(this.kUp == Vector3.zero)) ? this.kUp : ((this.directions[0].x != 0f || this.directions[0].z != 0f) ? Vector3.up : Vector3.right);
			for (int j = 0; j < this.positions.Length; j++)
			{
				if (j != 0 && j != this.positions.Length - 1)
				{
					zero.Set(this.directions[j].x + this.directions[j - 1].x, this.directions[j].y + this.directions[j - 1].y, this.directions[j].z + this.directions[j - 1].z);
				}
				else if (this.positions[0] == this.positions[this.positions.Length - 1])
				{
					zero.Set(this.directions[this.positions.Length - 1].x + this.directions[0].x, this.directions[this.positions.Length - 1].y + this.directions[0].y, this.directions[this.positions.Length - 1].z + this.directions[0].z);
				}
				else
				{
					zero.Set(this.directions[j].x, this.directions[j].y, this.directions[j].z);
				}
				if (zero == Vector3.zero)
				{
					this.rotations[j] = Quaternion.identity;
				}
				else
				{
					zero.Normalize();
					Vector3 rhs = Vector3.Cross(vector, zero);
					vector = Vector3.Cross(zero, rhs);
					if (j == 0)
					{
						this.kUp = vector;
					}
					if (this.twistAngle != 0f)
					{
						vector = Quaternion.AngleAxis(this.twistAngle, zero) * vector;
					}
					this.rotations[j].SetLookRotation(zero, vector);
				}
			}
			if (this.rope.Spline.IsLooped)
			{
				this.rotations[this.rotations.Length - 1] = this.rotations[0];
			}
		}

		// Token: 0x04004834 RID: 18484
		public bool showBounds;

		// Token: 0x04004835 RID: 18485
		public bool showEdges;

		// Token: 0x04004836 RID: 18486
		public bool showNormals;

		// Token: 0x04004837 RID: 18487
		[SerializeField]
		private int leafs = 6;

		// Token: 0x04004838 RID: 18488
		[SerializeField]
		private int detail = 1;

		// Token: 0x04004839 RID: 18489
		[SerializeField]
		private float center = 4f;

		// Token: 0x0400483A RID: 18490
		[SerializeField]
		[Min(1f)]
		private int strandCount = 1;

		// Token: 0x0400483B RID: 18491
		[SerializeField]
		private float strandOffset = 0.75f;

		// Token: 0x0400483C RID: 18492
		[SerializeField]
		private float twistAngle;

		// Token: 0x0400483D RID: 18493
		[SerializeField]
		private float radius = 1f;

		// Token: 0x0400483E RID: 18494
		[SerializeField]
		private AnimationCurve radiusCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x0400483F RID: 18495
		[SerializeField]
		private Material material;

		// Token: 0x04004840 RID: 18496
		[SerializeField]
		private Vector2 uvTile = new Vector2(1f, 1f);

		// Token: 0x04004841 RID: 18497
		[SerializeField]
		private Vector2 uvOffset = new Vector2(0f, 0f);

		// Token: 0x04004842 RID: 18498
		[SerializeField]
		[HideInInspector]
		private QuickRope rope;

		// Token: 0x04004843 RID: 18499
		[SerializeField]
		[HideInInspector]
		private bool flagShapeUpdate = true;

		// Token: 0x04004844 RID: 18500
		[SerializeField]
		[HideInInspector]
		private Vector3[] shapeLookup;

		// Token: 0x04004845 RID: 18501
		[SerializeField]
		[HideInInspector]
		private int[] shapeTriIndices;

		// Token: 0x04004846 RID: 18502
		[SerializeField]
		[HideInInspector]
		private int edgeCount;

		// Token: 0x04004847 RID: 18503
		[SerializeField]
		[HideInInspector]
		private Vector3 kUp = Vector3.zero;

		// Token: 0x04004848 RID: 18504
		[SerializeField]
		[HideInInspector]
		private Vector3[] positions;

		// Token: 0x04004849 RID: 18505
		[SerializeField]
		[HideInInspector]
		private Quaternion[] rotations;

		// Token: 0x0400484A RID: 18506
		[SerializeField]
		[HideInInspector]
		private Vector3[] directions;

		// Token: 0x0400484B RID: 18507
		[SerializeField]
		[HideInInspector]
		private bool[] isJoints;

		// Token: 0x0400484C RID: 18508
		[SerializeField]
		[HideInInspector]
		private Vector3[] vertices;

		// Token: 0x0400484D RID: 18509
		[SerializeField]
		[HideInInspector]
		private Vector3[] normals;

		// Token: 0x0400484E RID: 18510
		[SerializeField]
		[HideInInspector]
		private int[] triangles;

		// Token: 0x0400484F RID: 18511
		[SerializeField]
		[HideInInspector]
		private Vector2[] uvs;

		// Token: 0x04004850 RID: 18512
		private GameObject meshObject;

		// Token: 0x04004851 RID: 18513
		private Mesh mesh;

		// Token: 0x04004852 RID: 18514
		private MeshRenderer mRenderer;

		// Token: 0x04004853 RID: 18515
		private MeshFilter mFilter;

		// Token: 0x04004854 RID: 18516
		private bool dontRedraw;

		// Token: 0x04004855 RID: 18517
		private static int vertIndex;

		// Token: 0x04004856 RID: 18518
		private static int triaIndex;
	}
}
