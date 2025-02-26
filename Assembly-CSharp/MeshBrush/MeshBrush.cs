using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020003EE RID: 1006
	public class MeshBrush : MonoBehaviour
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0006B2FA File Offset: 0x000696FA
		public Transform CachedTransform
		{
			get
			{
				if (this.cachedTransform == null)
				{
					this.cachedTransform = base.transform;
				}
				return this.cachedTransform;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0006B31F File Offset: 0x0006971F
		public Collider CachedCollider
		{
			get
			{
				if (this.cachedCollider == null)
				{
					this.cachedCollider = base.GetComponent<Collider>();
				}
				return this.cachedCollider;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x0006B344 File Offset: 0x00069744
		public GameObject Brush
		{
			get
			{
				this.CheckBrush();
				return this.brush;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0006B352 File Offset: 0x00069752
		public Transform BrushTransform
		{
			get
			{
				this.CheckBrush();
				return this.brushTransform;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0006B360 File Offset: 0x00069760
		public Transform HolderObj
		{
			get
			{
				this.CheckHolder();
				return this.holderObj;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0006B370 File Offset: 0x00069770
		public void OnValidate()
		{
			this.ValidateKeyBindings();
			this.ValidateRangeLimits();
			if (this.meshes.Count == 0)
			{
				this.meshes.Add(null);
			}
			if (this.layerMask.Length != 32)
			{
				this.layerMask = new bool[32];
				for (int i = this.layerMask.Length - 1; i >= 0; i--)
				{
					this.layerMask[i] = true;
				}
			}
			if (this.layerMask[2])
			{
				this.layerMask[2] = false;
			}
			if (this.radius < 0.01f)
			{
				this.radius = 0.01f;
			}
			this.radius = (float)Math.Round((double)this.radius, 3);
			VectorClampingUtility.ClampVector(ref this.quantityRange, 1f, (float)this.maxQuantityLimit, 1f, (float)this.maxQuantityLimit);
			VectorClampingUtility.ClampVector(ref this.densityRange, 0.1f, this.maxDensityLimit, 0.1f, this.maxDensityLimit);
			this.delay = Mathf.Clamp(this.delay, 0.03f, this.maxDelayLimit);
			this.randomScaleCurveVariation = Mathf.Clamp(this.randomScaleCurveVariation, 0f, 3f);
			VectorClampingUtility.ClampVector(ref this.offsetRange, this.minOffsetLimit, this.maxOffsetLimit, this.minOffsetLimit, this.maxOffsetLimit);
			VectorClampingUtility.ClampVector(ref this.scatteringRange, 0f, 100f, 0f, 100f);
			VectorClampingUtility.ClampVector(ref this.slopeInfluenceRange, 0f, 100f, 0f, 100f);
			VectorClampingUtility.ClampVector(ref this.angleThresholdRange, 1f, 180f, 1f, 180f);
			VectorClampingUtility.ClampVector(ref this.minimumAbsoluteDistanceRange, 0f, this.maxMinimumAbsoluteDistanceLimit, 0f, this.maxMinimumAbsoluteDistanceLimit);
			VectorClampingUtility.ClampVector(ref this.randomScaleRange, 0.01f, this.maxRandomScaleLimit, 0f, this.maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref this.randomScaleRangeX, 0.01f, this.maxRandomScaleLimit, 0f, this.maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref this.randomScaleRangeY, 0.01f, this.maxRandomScaleLimit, 0f, this.maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref this.randomScaleRangeZ, 0.01f, this.maxRandomScaleLimit, 0f, this.maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref this.randomRotationRange, 0f, 100f, 0f, 100f);
			VectorClampingUtility.ClampVector(ref this.additiveScaleRange, -0.9f, this.maxAdditiveScaleLimit, -0.9f, this.maxAdditiveScaleLimit);
			VectorClampingUtility.ClampVector(ref this.additiveScaleNonUniform, -0.9f, this.maxAdditiveScaleLimit, -0.9f, this.maxAdditiveScaleLimit, -0.9f, this.maxAdditiveScaleLimit);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0006B638 File Offset: 0x00069A38
		private void ValidateRangeLimits()
		{
			this.maxQuantityLimit = Mathf.Clamp(this.maxQuantityLimit, 1, 1000);
			this.maxDensityLimit = Mathf.Clamp(this.maxDensityLimit, 1f, 1000f);
			this.maxDelayLimit = Mathf.Clamp(this.maxDelayLimit, 1f, 10f);
			this.minOffsetLimit = Mathf.Clamp(this.minOffsetLimit, -1000f, -1f);
			this.maxOffsetLimit = Mathf.Clamp(this.maxOffsetLimit, 1f, 1000f);
			this.maxMinimumAbsoluteDistanceLimit = Mathf.Clamp(this.maxMinimumAbsoluteDistanceLimit, 3f, 1000f);
			this.maxAdditiveScaleLimit = Mathf.Clamp(this.maxAdditiveScaleLimit, 3f, 1000f);
			this.maxRandomScaleLimit = Mathf.Clamp(this.maxRandomScaleLimit, 3f, 1000f);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0006B71C File Offset: 0x00069B1C
		private void ValidateKeyBindings()
		{
			if (this.paintKey == KeyCode.None)
			{
				this.paintKey = KeyCode.P;
			}
			if (this.deleteKey == KeyCode.None)
			{
				this.deleteKey = KeyCode.L;
			}
			if (this.randomizeKey == KeyCode.None)
			{
				this.randomizeKey = KeyCode.J;
			}
			if (this.combineKey == KeyCode.None)
			{
				this.combineKey = KeyCode.K;
			}
			if (this.increaseRadiusKey == KeyCode.None)
			{
				this.increaseRadiusKey = KeyCode.O;
			}
			if (this.decreaseRadiusKey == KeyCode.None)
			{
				this.decreaseRadiusKey = KeyCode.I;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0006B79B File Offset: 0x00069B9B
		public void GatherPaintedMeshes()
		{
			this.paintedMeshes = this.HolderObj.GetComponentsInChildren<Transform>().ToList<Transform>();
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0006B7B4 File Offset: 0x00069BB4
		public void CleanSetOfMeshesToPaint()
		{
			if (this.meshes.Count <= 1)
			{
				return;
			}
			for (int i = this.meshes.Count - 1; i >= 0; i--)
			{
				if (this.meshes[i] == null)
				{
					this.meshes.RemoveAt(i);
				}
			}
			if (this.meshes.Count == 0)
			{
				this.meshes.Add(null);
			}
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0006B830 File Offset: 0x00069C30
		private void GatherMeshesInsideBrushArea(RaycastHit brushLocation)
		{
			this.paintedMeshesInsideBrushArea.Clear();
			foreach (Transform transform in this.paintedMeshes)
			{
				if (transform != null && transform != this.BrushTransform && transform != this.HolderObj && Vector3.Distance(brushLocation.point, transform.position) < this.radius)
				{
					this.paintedMeshesInsideBrushArea.Add(transform);
				}
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0006B8E8 File Offset: 0x00069CE8
		public void PaintMeshes(RaycastHit brushLocation)
		{
			if (this.nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			this.nextFeasibleStrokeTime = Time.realtimeSinceStartup + this.delay;
			this.CheckBrush();
			this.brushStrokeDirection = brushLocation.point - this.lastPaintLocation;
			int num = (!this.useDensity) ? ((int)UnityEngine.Random.Range(this.quantityRange.x, this.quantityRange.y + 1f)) : ((int)(this.radius * this.radius * 3.1415927f * UnityEngine.Random.Range(this.densityRange.x, this.densityRange.y)));
			if (num <= 0)
			{
				num = 1;
			}
			if (this.useOverlapFilter)
			{
				this.GatherMeshesInsideBrushArea(brushLocation);
			}
			bool flag = false;
			for (int i = num; i > 0; i--)
			{
				float num2 = this.radius * 0.01f * UnityEngine.Random.Range(this.scatteringRange.x, this.scatteringRange.y);
				this.brushTransform.position = brushLocation.point + brushLocation.normal * 0.5f;
				this.brushTransform.rotation = Quaternion.LookRotation(brushLocation.normal);
				this.brushTransform.up = this.brushTransform.forward;
				if (num > 1)
				{
					this.brushTransform.Translate(UnityEngine.Random.Range(-UnityEngine.Random.insideUnitCircle.x * num2, UnityEngine.Random.insideUnitCircle.x * num2), 0f, UnityEngine.Random.Range(-UnityEngine.Random.insideUnitCircle.y * num2, UnityEngine.Random.insideUnitCircle.y * num2), Space.Self);
				}
				RaycastHit targetLocation;
				if ((!this.globalPaintingMode) ? this.CachedCollider.Raycast(new Ray(this.brushTransform.position, -brushLocation.normal), out targetLocation, 2.5f) : Physics.Raycast(new Ray(this.brushTransform.position, -brushLocation.normal), out targetLocation, 2.5f))
				{
					float num3 = (!this.useSlopeFilter) ? ((!this.inverseSlopeFilter) ? 0f : 180f) : Vector3.Angle(targetLocation.normal, (!this.manualReferenceVectorSampling) ? Vector3.up : this.slopeReferenceVector);
					if ((!this.inverseSlopeFilter) ? (num3 < UnityEngine.Random.Range(this.angleThresholdRange.x, this.angleThresholdRange.y)) : (num3 > UnityEngine.Random.Range(this.angleThresholdRange.x, this.angleThresholdRange.y)))
					{
						if (!this.useOverlapFilter || !this.CheckOverlap(targetLocation.point))
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.meshes[UnityEngine.Random.Range(0, this.meshes.Count)]);
							if (gameObject == null)
							{
								if (!flag)
								{
									flag = true;
								}
							}
							else
							{
								if (this.autoIgnoreRaycast)
								{
									gameObject.layer = 2;
								}
								Transform transform = gameObject.transform;
								this.OrientPaintedMesh(transform, targetLocation);
								if (Mathf.Abs(this.offsetRange.x) > 1E-45f || Mathf.Abs(this.offsetRange.y) > 1E-45f)
								{
									MeshTransformationUtility.ApplyMeshOffset(transform, UnityEngine.Random.Range(this.offsetRange.x, this.offsetRange.y), brushLocation.normal);
								}
								if (this.uniformRandomScale)
								{
									if (Mathf.Abs(this.randomScaleRange.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRange.y - 1f) > 1E-45f)
									{
										MeshTransformationUtility.ApplyRandomScale(transform, this.randomScaleRange);
									}
								}
								else if (Mathf.Abs(this.randomScaleRangeX.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeX.y - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeY.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeY.y - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeZ.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeZ.y - 1f) > 1E-45f)
								{
									MeshTransformationUtility.ApplyRandomScale(transform, this.randomScaleRangeX, this.randomScaleRangeY, this.randomScaleRangeZ);
								}
								transform.localScale *= Mathf.Abs(this.randomScaleCurve.Evaluate(Vector3.Distance(transform.position, brushLocation.point) / this.radius) + UnityEngine.Random.Range(-this.randomScaleCurveVariation, this.randomScaleCurveVariation));
								if (this.uniformAdditiveScale)
								{
									if (Mathf.Abs(this.additiveScaleRange.x) > 1E-45f || Mathf.Abs(this.additiveScaleRange.y) > 1E-45f)
									{
										MeshTransformationUtility.AddConstantScale(transform, this.additiveScaleRange);
									}
								}
								else if (Mathf.Abs(this.additiveScaleNonUniform.x) > 1E-45f || Mathf.Abs(this.additiveScaleNonUniform.y) > 1E-45f || Mathf.Abs(this.additiveScaleNonUniform.z) > 1E-45f)
								{
									MeshTransformationUtility.AddConstantScale(transform, this.additiveScaleNonUniform.x, this.additiveScaleNonUniform.y, this.additiveScaleNonUniform.z);
								}
								if (this.randomRotationRange.x > 0f || this.randomRotationRange.y > 0f)
								{
									MeshTransformationUtility.ApplyRandomRotation(transform, UnityEngine.Random.Range(this.randomRotationRange.x, this.randomRotationRange.y));
								}
								transform.parent = this.HolderObj;
								gameObject.isStatic |= this.autoStatic;
								this.paintedMeshes.Add(transform);
							}
						}
					}
				}
			}
			this.lastPaintLocation = brushLocation.point;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0006BF74 File Offset: 0x0006A374
		public void RandomizeMeshes(RaycastHit brushLocation)
		{
			if (this.nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			this.nextFeasibleStrokeTime = Time.realtimeSinceStartup + this.delay;
			this.GatherMeshesInsideBrushArea(brushLocation);
			for (int i = this.paintedMeshesInsideBrushArea.Count - 1; i >= 0; i--)
			{
				Transform transform = this.paintedMeshesInsideBrushArea[i];
				if (transform != null)
				{
					if (this.positionBrushRandomizer)
					{
						float num = this.radius * 0.01f * UnityEngine.Random.Range(this.scatteringRange.x, this.scatteringRange.y);
						this.brushTransform.position = brushLocation.point + brushLocation.normal * 0.5f;
						this.brushTransform.rotation = Quaternion.LookRotation(brushLocation.normal);
						this.brushTransform.up = this.brushTransform.forward;
						this.brushTransform.Translate(UnityEngine.Random.Range(-UnityEngine.Random.insideUnitCircle.x * num, UnityEngine.Random.insideUnitCircle.x * num), 0f, UnityEngine.Random.Range(-UnityEngine.Random.insideUnitCircle.y * num, UnityEngine.Random.insideUnitCircle.y * num), Space.Self);
						RaycastHit targetLocation;
						if ((!this.globalPaintingMode) ? this.CachedCollider.Raycast(new Ray(this.brushTransform.position, -brushLocation.normal), out targetLocation, 2.5f) : Physics.Raycast(new Ray(this.brushTransform.position, -brushLocation.normal), out targetLocation, 2.5f))
						{
							this.OrientPaintedMesh(transform, targetLocation);
						}
						if (Mathf.Abs(this.offsetRange.x) > 1E-45f || Mathf.Abs(this.offsetRange.y) > 1E-45f)
						{
							MeshTransformationUtility.ApplyMeshOffset(transform, UnityEngine.Random.Range(this.offsetRange.x, this.offsetRange.y), brushLocation.normal);
						}
					}
					if (this.rotationBrushRandomizer && (this.randomRotationRange.x > 0f || this.randomRotationRange.y > 0f))
					{
						MeshTransformationUtility.ApplyRandomRotation(transform, UnityEngine.Random.Range(this.randomRotationRange.x, this.randomRotationRange.y));
					}
					if (this.scaleBrushRandomizer)
					{
						if (this.uniformRandomScale)
						{
							if (Mathf.Abs(this.randomScaleRange.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRange.y - 1f) > 1E-45f)
							{
								MeshTransformationUtility.ApplyRandomScale(transform, this.randomScaleRange);
							}
						}
						else if (Mathf.Abs(this.randomScaleRangeX.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeX.y - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeY.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeY.y - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeZ.x - 1f) > 1E-45f || Mathf.Abs(this.randomScaleRangeZ.y - 1f) > 1E-45f)
						{
							MeshTransformationUtility.ApplyRandomScale(transform, this.randomScaleRangeX, this.randomScaleRangeY, this.randomScaleRangeZ);
						}
						transform.localScale *= Mathf.Abs(this.randomScaleCurve.Evaluate(Vector3.Distance(transform.position, brushLocation.point) / this.radius) + UnityEngine.Random.Range(-this.randomScaleCurveVariation, this.randomScaleCurveVariation));
					}
				}
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0006C37C File Offset: 0x0006A77C
		public void DeleteMeshes(RaycastHit brushLocation)
		{
			if (this.nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			this.nextFeasibleStrokeTime = Time.realtimeSinceStartup + this.delay;
			this.GatherMeshesInsideBrushArea(brushLocation);
			for (int i = this.paintedMeshesInsideBrushArea.Count - 1; i >= 0; i--)
			{
				this.paintedMeshes.Remove(this.paintedMeshesInsideBrushArea[i]);
				UnityEngine.Object.Destroy(this.paintedMeshesInsideBrushArea[i].gameObject);
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0006C400 File Offset: 0x0006A800
		public void CombineMeshes(RaycastHit brushLocation)
		{
			if (this.nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			this.nextFeasibleStrokeTime = Time.realtimeSinceStartup + this.delay;
			this.GatherMeshesInsideBrushArea(brushLocation);
			if (this.paintedMeshesInsideBrushArea.Count > 0)
			{
				this.HolderObj.GetComponent<MeshBrushParent>().CombinePaintedMeshes(this.autoSelectOnCombine, (from mesh in this.paintedMeshesInsideBrushArea
				select mesh.GetComponent<MeshFilter>()).ToArray<MeshFilter>());
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0006C48B File Offset: 0x0006A88B
		public void SampleReferenceVector(Vector3 referenceVector, Vector3 sampleLocation)
		{
			this.slopeReferenceVector = referenceVector;
			this.slopeReferenceVectorSampleLocation = sampleLocation;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0006C49C File Offset: 0x0006A89C
		private void OrientPaintedMesh(Transform targetTransform, RaycastHit targetLocation)
		{
			targetTransform.position = targetLocation.point;
			targetTransform.rotation = Quaternion.LookRotation(targetLocation.normal);
			Vector3 upwards = Vector3.Lerp((!this.yAxisTangent) ? Vector3.up : targetTransform.up, targetTransform.forward, UnityEngine.Random.Range(this.slopeInfluenceRange.x, this.slopeInfluenceRange.y) * 0.01f);
			Vector3 forward = (!this.strokeAlignment || !(this.brushStrokeDirection != Vector3.zero) || !(this.lastPaintLocation != Vector3.zero)) ? targetTransform.forward : this.brushStrokeDirection;
			Vector3.OrthoNormalize(ref upwards, ref forward);
			targetTransform.rotation = Quaternion.LookRotation(forward, upwards);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0006C570 File Offset: 0x0006A970
		private bool CheckOverlap(Vector3 objPos)
		{
			if (this.paintedMeshes == null || this.paintedMeshes.Count < 1)
			{
				return false;
			}
			foreach (Transform transform in this.paintedMeshes)
			{
				if (transform != null && transform != this.BrushTransform && transform != this.HolderObj && Vector3.Distance(objPos, transform.position) < UnityEngine.Random.Range(this.minimumAbsoluteDistanceRange.x, this.minimumAbsoluteDistanceRange.y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0006C648 File Offset: 0x0006AA48
		private void CheckHolder()
		{
			MeshBrushParent[] componentsInChildren = base.GetComponentsInChildren<MeshBrushParent>();
			if (componentsInChildren.Length > 0)
			{
				this.holderObj = null;
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] != null && string.CompareOrdinal(componentsInChildren[i].name, this.groupName) == 0)
					{
						this.holderObj = componentsInChildren[i].transform;
					}
				}
				if (this.holderObj == null)
				{
					this.CreateHolder();
				}
			}
			else
			{
				this.CreateHolder();
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0006C6D6 File Offset: 0x0006AAD6
		private void CheckBrush()
		{
			this.CheckHolder();
			this.brushTransform = this.holderObj.Find("Brush");
			if (this.brushTransform == null)
			{
				this.CreateBrush();
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0006C70C File Offset: 0x0006AB0C
		private void CreateHolder()
		{
			GameObject gameObject = new GameObject(this.groupName);
			gameObject.AddComponent<MeshBrushParent>();
			gameObject.transform.rotation = this.CachedTransform.rotation;
			gameObject.transform.parent = this.CachedTransform;
			gameObject.transform.localPosition = Vector3.zero;
			this.holderObj = gameObject.transform;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0006C770 File Offset: 0x0006AB70
		private void CreateBrush()
		{
			this.brush = new GameObject("Brush");
			this.brushTransform = this.brush.transform;
			this.brushTransform.position = this.CachedTransform.position;
			this.brushTransform.parent = this.holderObj;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0006C7C5 File Offset: 0x0006ABC5
		public void ResetKeyBindings()
		{
			this.paintKey = KeyCode.P;
			this.deleteKey = KeyCode.L;
			this.combineKey = KeyCode.K;
			this.randomizeKey = KeyCode.J;
			this.increaseRadiusKey = KeyCode.O;
			this.decreaseRadiusKey = KeyCode.I;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0006C7F8 File Offset: 0x0006ABF8
		public void ResetSlopeSettings()
		{
			this.slopeInfluenceRange = new Vector2(95f, 100f);
			this.angleThresholdRange = new Vector2(25f, 30f);
			this.useSlopeFilter = false;
			this.inverseSlopeFilter = false;
			this.manualReferenceVectorSampling = false;
			this.showReferenceVectorInSceneView = true;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0006C84C File Offset: 0x0006AC4C
		public void ResetRandomizers()
		{
			this.randomScaleRange = Vector2.one;
			this.randomScaleRangeX = (this.randomScaleRangeY = (this.randomScaleRangeZ = Vector2.one));
			this.randomScaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			this.randomScaleCurveVariation = 0f;
			this.randomRotationRange = new Vector2(0f, 5f);
			this.positionBrushRandomizer = false;
			this.rotationBrushRandomizer = true;
			this.scaleBrushRandomizer = true;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0006C8D5 File Offset: 0x0006ACD5
		public void ResetAdditiveScale()
		{
			this.uniformRandomScale = true;
			this.additiveScaleRange = Vector2.zero;
			this.additiveScaleNonUniform = Vector3.zero;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0006C8F4 File Offset: 0x0006ACF4
		public void ResetOverlapFilterSettings()
		{
			this.useOverlapFilter = false;
			this.minimumAbsoluteDistanceRange = new Vector2(0.25f, 0.5f);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0006C914 File Offset: 0x0006AD14
		public XDocument SaveTemplate(string filePath)
		{
			object[] array = new object[1];
			int num = 0;
			XName name = "meshBrushTemplate";
			object[] array2 = new object[13];
			array2[0] = new XAttribute("version", 1.9f);
			array2[1] = new XElement("instance", new object[]
			{
				new XElement("active", this.active),
				new XElement("name", this.groupName),
				new XElement("stats", this.stats),
				new XElement("lockSceneView", this.lockSceneView)
			});
			array2[2] = new XElement("meshes", new XElement("ui", new object[]
			{
				new XElement("style", (!this.classicUI) ? "modern" : "classic"),
				new XElement("iconSize", this.previewIconSize)
			}));
			array2[3] = new XElement("keyBindings", new object[]
			{
				new XElement("paint", this.paintKey),
				new XElement("delete", this.deleteKey),
				new XElement("combine", this.combineKey),
				new XElement("randomize", this.randomizeKey),
				new XElement("increaseRadius", this.increaseRadiusKey),
				new XElement("decreaseRadius", this.decreaseRadiusKey)
			});
			array2[4] = new XElement("brush", new object[]
			{
				new XElement("radius", this.radius),
				new XElement("color", new object[]
				{
					new XElement("r", this.color.r),
					new XElement("g", this.color.g),
					new XElement("b", this.color.b),
					new XElement("a", this.color.a)
				}),
				new XElement("quantity", new object[]
				{
					new XElement("min", this.quantityRange.x),
					new XElement("max", this.quantityRange.y)
				}),
				new XElement("useDensity", this.useDensity),
				new XElement("density", new object[]
				{
					new XElement("min", this.densityRange.x),
					new XElement("max", this.densityRange.y)
				}),
				new XElement("offset", new object[]
				{
					new XElement("min", this.offsetRange.x),
					new XElement("max", this.offsetRange.y)
				}),
				new XElement("scattering", new object[]
				{
					new XElement("min", this.scatteringRange.x),
					new XElement("max", this.scatteringRange.y)
				}),
				new XElement("delay", this.delay),
				new XElement("yAxisTangent", this.yAxisTangent),
				new XElement("strokeAlignment", this.strokeAlignment)
			});
			array2[5] = new XElement("slopes", new object[]
			{
				new XElement("slopeInfluence", new object[]
				{
					new XElement("min", this.slopeInfluenceRange.x),
					new XElement("max", this.slopeInfluenceRange.y)
				}),
				new XElement("slopeFilter", new object[]
				{
					new XElement("enabled", this.useSlopeFilter),
					new XElement("inverse", this.inverseSlopeFilter),
					new XElement("angleThreshold", new object[]
					{
						new XElement("min", this.angleThresholdRange.x),
						new XElement("max", this.angleThresholdRange.y)
					}),
					new XElement("manualReferenceVectorSampling", this.manualReferenceVectorSampling),
					new XElement("showReferenceVectorInSceneView", this.showReferenceVectorInSceneView),
					new XElement("referenceVector", new object[]
					{
						new XElement("x", this.slopeReferenceVector.x),
						new XElement("y", this.slopeReferenceVector.y),
						new XElement("z", this.slopeReferenceVector.z)
					}),
					new XElement("referenceVectorSampleLocation", new object[]
					{
						new XElement("x", this.slopeReferenceVectorSampleLocation.x),
						new XElement("y", this.slopeReferenceVectorSampleLocation.y),
						new XElement("z", this.slopeReferenceVectorSampleLocation.z)
					})
				})
			});
			int num2 = 6;
			XName name2 = "randomizers";
			object[] array3 = new object[3];
			int num3 = 0;
			XName name3 = "scale";
			object[] array4 = new object[4];
			array4[0] = new XElement("scaleUniformly", this.uniformRandomScale);
			array4[1] = new XElement("uniform", new object[]
			{
				new XElement("min", this.randomScaleRange.x),
				new XElement("max", this.randomScaleRange.y)
			});
			array4[2] = new XElement("nonUniform", new object[]
			{
				new XElement("x", new object[]
				{
					new XElement("min", this.randomScaleRangeX.x),
					new XElement("max", this.randomScaleRangeX.y)
				}),
				new XElement("y", new object[]
				{
					new XElement("min", this.randomScaleRangeY.x),
					new XElement("max", this.randomScaleRangeY.y)
				}),
				new XElement("z", new object[]
				{
					new XElement("min", this.randomScaleRangeZ.x),
					new XElement("max", this.randomScaleRangeZ.y)
				})
			});
			int num4 = 3;
			XName name4 = "curve";
			object[] array5 = new object[2];
			array5[0] = new XElement("variation", this.randomScaleCurveVariation);
			array5[1] = new XElement("keys", from key in this.randomScaleCurve.keys
			select new XElement("key", new object[]
			{
				new XElement("time", key.time),
				new XElement("value", key.value),
				new XElement("inTangent", key.inTangent),
				new XElement("outTangent", key.outTangent)
			}));
			array4[num4] = new XElement(name4, array5);
			array3[num3] = new XElement(name3, array4);
			array3[1] = new XElement("rotation", new object[]
			{
				new XElement("min", this.randomRotationRange.x),
				new XElement("max", this.randomRotationRange.y)
			});
			array3[2] = new XElement("randomizerBrush", new object[]
			{
				new XElement("position", this.positionBrushRandomizer),
				new XElement("rotation", this.rotationBrushRandomizer),
				new XElement("scale", this.scaleBrushRandomizer)
			});
			array2[num2] = new XElement(name2, array3);
			array2[7] = new XElement("overlapFilter", new object[]
			{
				new XElement("enabled", this.useOverlapFilter),
				new XElement("minimumAbsoluteDistance", new object[]
				{
					new XElement("min", this.minimumAbsoluteDistanceRange.x),
					new XElement("max", this.minimumAbsoluteDistanceRange.y)
				})
			});
			array2[8] = new XElement("additiveScale", new object[]
			{
				new XElement("scaleUniformly", this.uniformAdditiveScale),
				new XElement("uniform", new object[]
				{
					new XElement("min", this.additiveScaleRange.x),
					new XElement("max", this.additiveScaleRange.y)
				}),
				new XElement("nonUniform", new object[]
				{
					new XElement("x", this.additiveScaleNonUniform.x),
					new XElement("y", this.additiveScaleNonUniform.y),
					new XElement("z", this.additiveScaleNonUniform.z)
				})
			});
			array2[9] = new XElement("optimization", new object[]
			{
				new XElement("autoIgnoreRaycast", this.autoIgnoreRaycast),
				new XElement("autoSelectOnCombine", this.autoSelectOnCombine),
				new XElement("autoStatic", this.autoStatic)
			});
			array2[10] = new XElement("rangeLimits", new object[]
			{
				new XElement("quantity", new XElement("max", this.maxQuantityLimit)),
				new XElement("density", new XElement("max", this.maxDensityLimit)),
				new XElement("offset", new object[]
				{
					new XElement("min", this.minOffsetLimit),
					new XElement("max", this.maxOffsetLimit)
				}),
				new XElement("delay", new XElement("max", this.maxDelayLimit)),
				new XElement("minimumAbsoluteDistance", new XElement("max", this.maxMinimumAbsoluteDistanceLimit)),
				new XElement("randomScale", new XElement("max", this.maxRandomScaleLimit)),
				new XElement("additiveScale", new XElement("max", this.maxAdditiveScaleLimit))
			});
			array2[11] = new XElement("inspectorFoldouts", new object[]
			{
				new XElement("help", this.helpFoldout),
				new XElement("templatesHelp", this.helpTemplatesFoldout),
				new XElement("generalUsageHelp", this.helpGeneralUsageFoldout),
				new XElement("optimizationHelp", this.helpOptimizationFoldout),
				new XElement("meshes", this.meshesFoldout),
				new XElement("templates", this.templatesFoldout),
				new XElement("keyBindings", this.keyBindingsFoldout),
				new XElement("brush", this.brushFoldout),
				new XElement("slopes", this.slopesFoldout),
				new XElement("randomizers", this.randomizersFoldout),
				new XElement("overlapFilter", this.overlapFilterFoldout),
				new XElement("additiveScale", this.additiveScaleFoldout),
				new XElement("optimization", this.optimizationFoldout)
			});
			int num5 = 12;
			XName name5 = "globalPaintingMode";
			object[] array6 = new object[2];
			array6[0] = new XElement("enabled", this.globalPaintingMode);
			array6[1] = new XElement("layerMask", this.layerMask.Select((bool layer, int index) => new XElement("layer", new object[]
			{
				new XAttribute("index", index),
				layer
			})));
			array2[num5] = new XElement(name5, array6);
			array[num] = new XElement(name, array2);
			XDocument xdocument = new XDocument(array);
			xdocument.Save(filePath);
			return xdocument;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0006D8D8 File Offset: 0x0006BCD8
		public bool LoadTemplate(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				return false;
			}
			XDocument xdocument = XDocument.Load(filePath);
			if (xdocument == null)
			{
				return false;
			}
			foreach (XElement xelement in xdocument.Root.Elements())
			{
				string localName = xelement.Name.LocalName;
				switch (localName)
				{
				case "instance":
					foreach (XElement xelement2 in xelement.Elements())
					{
						string localName2 = xelement2.Name.LocalName;
						if (localName2 != null)
						{
							if (!(localName2 == "active"))
							{
								if (!(localName2 == "name"))
								{
									if (!(localName2 == "stats"))
									{
										if (localName2 == "lockSceneView")
										{
											this.lockSceneView = (string.CompareOrdinal(xelement2.Value, "true") == 0);
										}
									}
									else
									{
										this.stats = (string.CompareOrdinal(xelement2.Value, "true") == 0);
									}
								}
								else
								{
									this.groupName = xelement2.Value;
								}
							}
							else
							{
								this.active = (string.CompareOrdinal(xelement2.Value, "true") == 0);
							}
						}
					}
					break;
				case "meshes":
					foreach (XElement xelement3 in xelement.Elements())
					{
						string localName3 = xelement3.Name.LocalName;
						if (localName3 != null)
						{
							if (localName3 == "ui")
							{
								this.classicUI = (string.CompareOrdinal(xelement3.Element("style").Value, "classic") == 0);
								this.previewIconSize = float.Parse(xelement3.Element("iconSize").Value);
							}
						}
					}
					break;
				case "keyBindings":
					foreach (XElement xelement4 in xelement.Elements())
					{
						string localName4 = xelement4.Name.LocalName;
						if (localName4 != null)
						{
							if (!(localName4 == "paint"))
							{
								if (!(localName4 == "delete"))
								{
									if (!(localName4 == "combine"))
									{
										if (!(localName4 == "randomize"))
										{
											if (!(localName4 == "increaseRadius"))
											{
												if (localName4 == "decreaseRadius")
												{
													this.decreaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement4.Value);
												}
											}
											else
											{
												this.increaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement4.Value);
											}
										}
										else
										{
											this.randomizeKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement4.Value);
										}
									}
									else
									{
										this.combineKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement4.Value);
									}
								}
								else
								{
									this.deleteKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement4.Value);
								}
							}
							else
							{
								this.paintKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement4.Value);
							}
						}
					}
					break;
				case "brush":
					foreach (XElement xelement5 in xelement.Elements())
					{
						string localName5 = xelement5.Name.LocalName;
						switch (localName5)
						{
						case "radius":
							this.radius = float.Parse(xelement5.Value);
							break;
						case "color":
							this.color = new Color(float.Parse(xelement5.Element("r").Value), float.Parse(xelement5.Element("g").Value), float.Parse(xelement5.Element("b").Value), float.Parse(xelement5.Element("a").Value));
							break;
						case "quantity":
							this.quantityRange = new Vector2(float.Parse(xelement5.Element("min").Value), float.Parse(xelement5.Element("max").Value));
							break;
						case "useDensity":
							this.useDensity = (string.CompareOrdinal(xelement5.Value, "true") == 0);
							break;
						case "density":
							this.densityRange = new Vector2(float.Parse(xelement5.Element("min").Value), float.Parse(xelement5.Element("max").Value));
							break;
						case "offset":
							this.offsetRange = new Vector2(float.Parse(xelement5.Element("min").Value), float.Parse(xelement5.Element("max").Value));
							break;
						case "scattering":
							this.scatteringRange = new Vector2(float.Parse(xelement5.Element("min").Value), float.Parse(xelement5.Element("max").Value));
							break;
						case "delay":
							this.delay = float.Parse(xelement5.Value);
							break;
						case "yAxisTangent":
							this.yAxisTangent = (string.CompareOrdinal(xelement5.Value, "true") == 0);
							break;
						case "strokeAlignment":
							this.strokeAlignment = (string.CompareOrdinal(xelement5.Value, "true") == 0);
							break;
						}
					}
					break;
				case "slopes":
					foreach (XElement xelement6 in xelement.Descendants())
					{
						string localName6 = xelement6.Name.LocalName;
						switch (localName6)
						{
						case "slopeInfluence":
							this.slopeInfluenceRange = new Vector2(float.Parse(xelement6.Element("min").Value), float.Parse(xelement6.Element("max").Value));
							break;
						case "enabled":
							this.useSlopeFilter = (string.CompareOrdinal(xelement6.Value, "true") == 0);
							break;
						case "inverse":
							this.inverseSlopeFilter = (string.CompareOrdinal(xelement6.Value, "true") == 0);
							break;
						case "angleThreshold":
							this.angleThresholdRange = new Vector2(float.Parse(xelement6.Element("min").Value), float.Parse(xelement6.Element("max").Value));
							break;
						case "manualReferenceVectorSampling":
							this.manualReferenceVectorSampling = (string.CompareOrdinal(xelement6.Value, "true") == 0);
							break;
						case "showReferenceVectorInSceneView":
							this.showReferenceVectorInSceneView = (string.CompareOrdinal(xelement6.Value, "true") == 0);
							break;
						case "referenceVector":
							this.slopeReferenceVector = new Vector3(float.Parse(xelement6.Element("x").Value), float.Parse(xelement6.Element("y").Value), float.Parse(xelement6.Element("z").Value));
							break;
						case "referenceVectorSampleLocation":
							this.slopeReferenceVectorSampleLocation = new Vector3(float.Parse(xelement6.Element("x").Value), float.Parse(xelement6.Element("y").Value), float.Parse(xelement6.Element("z").Value));
							break;
						}
					}
					break;
				case "randomizers":
					foreach (XElement xelement7 in xelement.Elements())
					{
						string localName7 = xelement7.Name.LocalName;
						if (localName7 != null)
						{
							if (!(localName7 == "scale"))
							{
								if (!(localName7 == "rotation"))
								{
									if (localName7 == "randomizerBrush")
									{
										XElement xelement8 = xelement7.Element("position");
										if (xelement8 != null)
										{
											this.positionBrushRandomizer = (string.CompareOrdinal(xelement8.Value, "true") == 0);
										}
										xelement8 = xelement7.Element("rotation");
										if (xelement8 != null)
										{
											this.rotationBrushRandomizer = (string.CompareOrdinal(xelement8.Value, "true") == 0);
										}
										xelement8 = xelement7.Element("scale");
										if (xelement8 != null)
										{
											this.scaleBrushRandomizer = (string.CompareOrdinal(xelement8.Value, "true") == 0);
										}
									}
								}
								else if (string.CompareOrdinal(xelement7.Parent.Name.LocalName, "randomizerBrush") != 0)
								{
									this.randomRotationRange = new Vector2(float.Parse(xelement7.Element("min").Value), float.Parse(xelement7.Element("max").Value));
								}
							}
							else
							{
								foreach (XElement xelement9 in xelement7.Descendants())
								{
									string localName8 = xelement9.Name.LocalName;
									switch (localName8)
									{
									case "scaleUniformly":
										this.uniformRandomScale = (string.CompareOrdinal(xelement9.Value, "true") == 0);
										break;
									case "uniform":
										this.randomScaleRange = new Vector2(float.Parse(xelement9.Element("min").Value), float.Parse(xelement9.Element("max").Value));
										break;
									case "x":
										this.randomScaleRangeX = new Vector2(float.Parse(xelement9.Element("min").Value), float.Parse(xelement9.Element("max").Value));
										break;
									case "y":
										this.randomScaleRangeY = new Vector2(float.Parse(xelement9.Element("min").Value), float.Parse(xelement9.Element("max").Value));
										break;
									case "z":
										this.randomScaleRangeZ = new Vector2(float.Parse(xelement9.Element("min").Value), float.Parse(xelement9.Element("max").Value));
										break;
									case "variation":
										this.randomScaleCurveVariation = float.Parse(xelement9.Value);
										break;
									case "keys":
										this.randomScaleCurve = new AnimationCurve((from key in xelement9.Descendants("key")
										select new Keyframe(float.Parse(key.Element("time").Value), float.Parse(key.Element("value").Value), float.Parse(key.Element("inTangent").Value), float.Parse(key.Element("outTangent").Value))).ToArray<Keyframe>());
										break;
									}
								}
							}
						}
					}
					break;
				case "overlapFilter":
					foreach (XElement xelement10 in xelement.Elements())
					{
						string localName9 = xelement10.Name.LocalName;
						if (localName9 != null)
						{
							if (!(localName9 == "enabled"))
							{
								if (localName9 == "minimumAbsoluteDistance")
								{
									this.minimumAbsoluteDistanceRange = new Vector2(float.Parse(xelement10.Element("min").Value), float.Parse(xelement10.Element("max").Value));
								}
							}
							else
							{
								this.useOverlapFilter = (string.CompareOrdinal(xelement10.Value, "true") == 0);
							}
						}
					}
					break;
				case "additiveScale":
					foreach (XElement xelement11 in xelement.Elements())
					{
						string localName10 = xelement11.Name.LocalName;
						if (localName10 != null)
						{
							if (!(localName10 == "scaleUniformly"))
							{
								if (!(localName10 == "uniform"))
								{
									if (localName10 == "nonUniform")
									{
										this.additiveScaleNonUniform = new Vector3(float.Parse(xelement11.Element("x").Value), float.Parse(xelement11.Element("y").Value), float.Parse(xelement11.Element("z").Value));
									}
								}
								else
								{
									this.additiveScaleRange = new Vector2(float.Parse(xelement11.Element("min").Value), float.Parse(xelement11.Element("max").Value));
								}
							}
							else
							{
								this.uniformAdditiveScale = (string.CompareOrdinal(xelement11.Value, "true") == 0);
							}
						}
					}
					break;
				case "optimization":
					foreach (XElement xelement12 in xelement.Elements())
					{
						string localName11 = xelement12.Name.LocalName;
						if (localName11 != null)
						{
							if (!(localName11 == "autoIgnoreRaycast"))
							{
								if (!(localName11 == "autoSelectOnCombine"))
								{
									if (localName11 == "autoStatic")
									{
										this.autoStatic = (string.CompareOrdinal(xelement12.Value, "true") == 0);
									}
								}
								else
								{
									this.autoSelectOnCombine = (string.CompareOrdinal(xelement12.Value, "true") == 0);
								}
							}
							else
							{
								this.autoIgnoreRaycast = (string.CompareOrdinal(xelement12.Value, "true") == 0);
							}
						}
					}
					break;
				case "rangeLimits":
					foreach (XElement xelement13 in xelement.Elements())
					{
						string localName12 = xelement13.Name.LocalName;
						switch (localName12)
						{
						case "quantity":
							this.maxQuantityLimit = int.Parse(xelement13.Element("max").Value);
							break;
						case "density":
							this.maxDensityLimit = float.Parse(xelement13.Element("max").Value);
							break;
						case "offset":
							this.minOffsetLimit = float.Parse(xelement13.Element("min").Value);
							this.maxOffsetLimit = float.Parse(xelement13.Element("max").Value);
							break;
						case "delay":
							this.maxDelayLimit = float.Parse(xelement13.Element("max").Value);
							break;
						case "minimumAbsoluteDistance":
							this.maxMinimumAbsoluteDistanceLimit = float.Parse(xelement13.Element("max").Value);
							break;
						case "randomScale":
							this.maxRandomScaleLimit = float.Parse(xelement13.Element("max").Value);
							break;
						case "additiveScale":
							this.maxAdditiveScaleLimit = float.Parse(xelement13.Element("max").Value);
							break;
						}
					}
					break;
				case "inspectorFoldouts":
					foreach (XElement xelement14 in xelement.Elements())
					{
						string localName13 = xelement14.Name.LocalName;
						switch (localName13)
						{
						case "help":
							this.helpFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "templatesHelp":
							this.helpTemplatesFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "generalUsageHelp":
							this.helpGeneralUsageFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "optimizationHelp":
							this.helpOptimizationFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "meshes":
							this.meshesFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "templates":
							this.templatesFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "keyBindings":
							this.keyBindingsFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "brush":
							this.brushFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "slopes":
							this.slopesFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "randomizers":
							this.randomizersFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "overlapFilter":
							this.overlapFilterFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "additiveScale":
							this.additiveScaleFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						case "optimization":
							this.optimizationFoldout = (string.CompareOrdinal(xelement14.Value, "true") == 0);
							break;
						}
					}
					break;
				case "globalPaintingMode":
					this.globalPaintingMode = (string.CompareOrdinal(xelement.Element("enabled").Value, "true") == 0);
					this.layerMask = (from layerElement in xelement.Descendants("layer")
					select string.CompareOrdinal(layerElement.Value, "false") != 0).ToArray<bool>();
					break;
				}
			}
			return true;
		}

		// Token: 0x04001412 RID: 5138
		public const float version = 1.9f;

		// Token: 0x04001413 RID: 5139
		public bool active = true;

		// Token: 0x04001414 RID: 5140
		public string groupName = "<group name>";

		// Token: 0x04001415 RID: 5141
		public bool[] layerMask = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x04001416 RID: 5142
		public float radius = 0.3f;

		// Token: 0x04001417 RID: 5143
		public Color color = Color.white;

		// Token: 0x04001418 RID: 5144
		public Vector2 quantityRange = Vector2.one;

		// Token: 0x04001419 RID: 5145
		public bool useDensity;

		// Token: 0x0400141A RID: 5146
		public Vector2 densityRange = new Vector2(0.5f, 0.5f);

		// Token: 0x0400141B RID: 5147
		public float delay = 0.25f;

		// Token: 0x0400141C RID: 5148
		public Vector2 offsetRange;

		// Token: 0x0400141D RID: 5149
		public Vector2 slopeInfluenceRange = new Vector2(95f, 100f);

		// Token: 0x0400141E RID: 5150
		public bool useSlopeFilter;

		// Token: 0x0400141F RID: 5151
		public Vector2 angleThresholdRange = new Vector2(25f, 30f);

		// Token: 0x04001420 RID: 5152
		public bool inverseSlopeFilter;

		// Token: 0x04001421 RID: 5153
		public Vector3 slopeReferenceVector = Vector3.up;

		// Token: 0x04001422 RID: 5154
		public Vector3 slopeReferenceVectorSampleLocation = Vector3.zero;

		// Token: 0x04001423 RID: 5155
		public bool yAxisTangent;

		// Token: 0x04001424 RID: 5156
		public bool strokeAlignment;

		// Token: 0x04001425 RID: 5157
		public bool autoIgnoreRaycast;

		// Token: 0x04001426 RID: 5158
		public Vector2 scatteringRange = new Vector2(70f, 80f);

		// Token: 0x04001427 RID: 5159
		public bool useOverlapFilter;

		// Token: 0x04001428 RID: 5160
		public Vector2 minimumAbsoluteDistanceRange = new Vector2(0.25f, 0.5f);

		// Token: 0x04001429 RID: 5161
		public bool uniformRandomScale = true;

		// Token: 0x0400142A RID: 5162
		public bool uniformAdditiveScale = true;

		// Token: 0x0400142B RID: 5163
		public Vector2 randomScaleRange = Vector2.one;

		// Token: 0x0400142C RID: 5164
		public Vector2 randomScaleRangeX = Vector2.one;

		// Token: 0x0400142D RID: 5165
		public Vector2 randomScaleRangeY = Vector2.one;

		// Token: 0x0400142E RID: 5166
		public Vector2 randomScaleRangeZ = Vector2.one;

		// Token: 0x0400142F RID: 5167
		public Vector2 additiveScaleRange = Vector2.zero;

		// Token: 0x04001430 RID: 5168
		public Vector3 additiveScaleNonUniform = Vector3.zero;

		// Token: 0x04001431 RID: 5169
		public AnimationCurve randomScaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		// Token: 0x04001432 RID: 5170
		public float randomScaleCurveVariation;

		// Token: 0x04001433 RID: 5171
		public Vector2 randomRotationRange = new Vector2(0f, 5f);

		// Token: 0x04001434 RID: 5172
		public bool positionBrushRandomizer;

		// Token: 0x04001435 RID: 5173
		public bool rotationBrushRandomizer = true;

		// Token: 0x04001436 RID: 5174
		public bool scaleBrushRandomizer = true;

		// Token: 0x04001437 RID: 5175
		public KeyCode paintKey = KeyCode.P;

		// Token: 0x04001438 RID: 5176
		public KeyCode deleteKey = KeyCode.L;

		// Token: 0x04001439 RID: 5177
		public KeyCode combineKey = KeyCode.K;

		// Token: 0x0400143A RID: 5178
		public KeyCode randomizeKey = KeyCode.J;

		// Token: 0x0400143B RID: 5179
		public KeyCode increaseRadiusKey = KeyCode.O;

		// Token: 0x0400143C RID: 5180
		public KeyCode decreaseRadiusKey = KeyCode.I;

		// Token: 0x0400143D RID: 5181
		[SerializeField]
		private int maxQuantityLimit = 100;

		// Token: 0x0400143E RID: 5182
		[SerializeField]
		private float maxDelayLimit = 1f;

		// Token: 0x0400143F RID: 5183
		[SerializeField]
		private float maxDensityLimit = 10f;

		// Token: 0x04001440 RID: 5184
		[SerializeField]
		private float minOffsetLimit = -50f;

		// Token: 0x04001441 RID: 5185
		[SerializeField]
		private float maxOffsetLimit = 50f;

		// Token: 0x04001442 RID: 5186
		[SerializeField]
		private float maxMinimumAbsoluteDistanceLimit = 3f;

		// Token: 0x04001443 RID: 5187
		[SerializeField]
		private float maxAdditiveScaleLimit = 3f;

		// Token: 0x04001444 RID: 5188
		[SerializeField]
		private float maxRandomScaleLimit = 3f;

		// Token: 0x04001445 RID: 5189
		public bool helpFoldout;

		// Token: 0x04001446 RID: 5190
		public bool helpTemplatesFoldout;

		// Token: 0x04001447 RID: 5191
		public bool helpGeneralUsageFoldout;

		// Token: 0x04001448 RID: 5192
		public bool helpOptimizationFoldout;

		// Token: 0x04001449 RID: 5193
		public bool meshesFoldout = true;

		// Token: 0x0400144A RID: 5194
		public bool templatesFoldout = true;

		// Token: 0x0400144B RID: 5195
		public bool keyBindingsFoldout;

		// Token: 0x0400144C RID: 5196
		public bool brushFoldout = true;

		// Token: 0x0400144D RID: 5197
		public bool slopesFoldout = true;

		// Token: 0x0400144E RID: 5198
		public bool randomizersFoldout = true;

		// Token: 0x0400144F RID: 5199
		public bool overlapFilterFoldout = true;

		// Token: 0x04001450 RID: 5200
		public bool additiveScaleFoldout = true;

		// Token: 0x04001451 RID: 5201
		public bool optimizationFoldout = true;

		// Token: 0x04001452 RID: 5202
		[SerializeField]
		private bool globalPaintingMode;

		// Token: 0x04001453 RID: 5203
		public bool collapsed;

		// Token: 0x04001454 RID: 5204
		public bool stats;

		// Token: 0x04001455 RID: 5205
		public bool lockSceneView;

		// Token: 0x04001456 RID: 5206
		public bool classicUI;

		// Token: 0x04001457 RID: 5207
		public float previewIconSize = 60f;

		// Token: 0x04001458 RID: 5208
		public bool manualReferenceVectorSampling;

		// Token: 0x04001459 RID: 5209
		public bool showReferenceVectorInSceneView = true;

		// Token: 0x0400145A RID: 5210
		public bool autoStatic;

		// Token: 0x0400145B RID: 5211
		public bool autoSelectOnCombine = true;

		// Token: 0x0400145C RID: 5212
		private Transform cachedTransform;

		// Token: 0x0400145D RID: 5213
		private Collider cachedCollider;

		// Token: 0x0400145E RID: 5214
		private GameObject brush;

		// Token: 0x0400145F RID: 5215
		private Transform brushTransform;

		// Token: 0x04001460 RID: 5216
		private Transform holderObj;

		// Token: 0x04001461 RID: 5217
		private const string minString = "min";

		// Token: 0x04001462 RID: 5218
		private const string maxString = "max";

		// Token: 0x04001463 RID: 5219
		private const string trueString = "true";

		// Token: 0x04001464 RID: 5220
		private const string falseString = "false";

		// Token: 0x04001465 RID: 5221
		private const string enabledString = "enabled";

		// Token: 0x04001466 RID: 5222
		public Vector3 lastPaintLocation;

		// Token: 0x04001467 RID: 5223
		public Vector3 brushStrokeDirection;

		// Token: 0x04001468 RID: 5224
		[SerializeField]
		private List<GameObject> meshes = new List<GameObject>(5)
		{
			null
		};

		// Token: 0x04001469 RID: 5225
		private List<Transform> paintedMeshes = new List<Transform>(200);

		// Token: 0x0400146A RID: 5226
		private List<Transform> paintedMeshesInsideBrushArea = new List<Transform>(50);

		// Token: 0x0400146B RID: 5227
		private float nextFeasibleStrokeTime;
	}
}
