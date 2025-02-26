using System;
using PicoGames.Utilities;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A6D RID: 2669
	[AddComponentMenu("PicoGames/QuickRopes/QuickRope")]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Spline))]
	public class QuickRope : MonoBehaviour
	{
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x001E52F4 File Offset: 0x001E36F4
		public Spline Spline
		{
			get
			{
				if (this.spline == null)
				{
					this.spline = base.gameObject.GetComponent<Spline>();
					if (this.spline == null)
					{
						this.spline = base.gameObject.AddComponent<Spline>();
						this.spline.Reset();
					}
				}
				return this.spline;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x001E5356 File Offset: 0x001E3756
		public int ActiveLinkCount
		{
			get
			{
				return this.activeLinkCount;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x001E535E File Offset: 0x001E375E
		public Link[] Links
		{
			get
			{
				return this.links;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06004F0A RID: 20234 RVA: 0x001E5366 File Offset: 0x001E3766
		// (set) Token: 0x06004F0B RID: 20235 RVA: 0x001E536E File Offset: 0x001E376E
		public float Velocity
		{
			get
			{
				return this.velocity;
			}
			set
			{
				this.velocity = value;
			}
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x001E5377 File Offset: 0x001E3777
		private void Reset()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this.ClearLinks();
			this.Spline.Reset();
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x001E5398 File Offset: 0x001E3798
		public void Generate()
		{
			if (this.spline.IsLooped)
			{
				this.canResize = false;
			}
			SplinePoint[] array = this.ResizeLinkArray();
			if (array.Length == 0)
			{
				return;
			}
			Rigidbody connectedBody = null;
			for (int i = this.links.Length - 1; i >= 0; i--)
			{
				this.links[i].IsActive = (i < this.activeLinkCount || i == this.links.Length - 1);
				this.links[i].transform.localScale = Vector3.one * this.linkScale;
				this.links[i].gameObject.layer = base.gameObject.layer;
				this.links[i].gameObject.tag = base.gameObject.tag;
				if (i < array.Length - 1)
				{
					this.links[i].transform.rotation = base.transform.rotation * array[i].rotation;
					this.links[i].transform.position = base.transform.TransformPoint(array[i].position);
				}
				else if (i == this.links.Length - 1)
				{
					this.links[i].transform.rotation = base.transform.rotation * array[array.Length - 1].rotation;
					this.links[i].transform.position = base.transform.TransformPoint(array[array.Length - 1].position);
				}
				if (i != this.links.Length - 1)
				{
					if (!this.links[i].overridePrefab)
					{
						this.links[i].prefab = this.defaultPrefab;
					}
					if (!this.links[i].overrideOffsetSettings)
					{
						this.links[i].offsetSettings = this.defaultPrefabOffsets;
					}
					if (this.links[i].AttachedPrefab() != null)
					{
						this.links[i].AttachedPrefab().hideFlags = (HideFlags.HideInHierarchy | HideFlags.NotEditable);
					}
				}
				this.links[i].alternateJoints = this.alternateRotation;
				this.links[i].ApplyPrefabSettings();
				if (!this.links[i].overrideRigidbodySettings)
				{
					this.links[i].rigidbodySettings = this.defaultRigidbodySettings;
				}
				this.links[i].ApplyRigidbodySettings();
				if (i != this.links.Length - 1)
				{
					if (!this.links[i].overrideJointSettings)
					{
						this.links[i].jointSettings = this.defaultJointSettings;
					}
					this.links[i].ApplyJointSettings(this.linkSpacing * (1f / this.linkScale));
				}
				if (!this.links[i].overrideColliderSettings)
				{
					this.links[i].colliderSettings = this.defaultColliderSettings;
				}
				this.links[i].ApplyColliderSettings();
				if (this.links[i].TogglePhysicsEnabled(this.usePhysics))
				{
					this.links[i].ConnectedBody = connectedBody;
					connectedBody = this.links[i].Rigidbody;
				}
			}
			if (this.usePhysics)
			{
				if (this.spline.IsLooped)
				{
					this.links[this.links.Length - 1].IsActive = false;
					this.activeLinkCount--;
					this.links[this.links.Length - 2].ConnectedBody = this.links[0].Rigidbody;
				}
				else
				{
					this.links[this.links.Length - 1].RemoveJoint();
					this.links[this.links.Length - 1].IsPrefabActive = false;
					if (this.canResize && this.activeLinkCount != this.links.Length)
					{
						this.links[this.activeLinkCount - 1].ConnectedBody = this.links[this.links.Length - 1].Rigidbody;
					}
				}
			}
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x001E57A4 File Offset: 0x001E3BA4
		private SplinePoint[] ResizeLinkArray()
		{
			this.maxLinkCount = Mathf.Min(this.maxLinkCount, 128);
			SplinePoint[] array = this.Spline.GetSpacedPointsReversed(this.linkSpacing);
			this.activeLinkCount = Mathf.Min(this.maxLinkCount, array.Length) - 1;
			int num = (!this.canResize) ? array.Length : this.maxLinkCount;
			if (num <= 0 && this.links.Length > 0)
			{
				for (int i = 0; i < this.links.Length; i++)
				{
					this.links[i].Destroy();
				}
				this.links = new Link[0];
				array = new SplinePoint[0];
			}
			else if (this.links.Length != num)
			{
				if (num > this.links.Length)
				{
					int num2 = this.links.Length;
					Array.Resize<Link>(ref this.links, num);
					for (int j = num2; j < num; j++)
					{
						this.links[j] = new Link(new GameObject("Link_[" + j + "]"), j);
						this.links[j].Parent = base.transform;
					}
				}
				else if (num > 0)
				{
					for (int k = this.links.Length - 1; k >= num; k--)
					{
						this.links[k].Destroy();
					}
					Array.Resize<Link>(ref this.links, num);
				}
			}
			return array;
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x001E5928 File Offset: 0x001E3D28
		private void ClearLinks()
		{
			if (Application.isPlaying)
			{
				return;
			}
			while (base.transform.childCount > 0)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(base.transform.GetChild(0).gameObject);
				}
			}
			this.links = new Link[0];
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x001E59A0 File Offset: 0x001E3DA0
		private void FixedUpdate()
		{
			if (this.velocity != 0f)
			{
				this.kVelocity = Mathf.Lerp(this.kVelocity, this.velocity, Time.deltaTime * this.velocityAccel);
			}
			else
			{
				this.kVelocity = Mathf.Lerp(this.kVelocity, this.velocity, Time.deltaTime * this.velocityDampen);
			}
			if (this.kVelocity > 0f)
			{
				this.kVelocity = ((!this.Extend(this.kVelocity)) ? 0f : this.kVelocity);
			}
			if (this.kVelocity < 0f)
			{
				this.kVelocity = ((!this.Retract(this.kVelocity, this.minLinkCount)) ? 0f : this.kVelocity);
			}
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x001E5A7C File Offset: 0x001E3E7C
		public bool Extend(float _speed)
		{
			if (!this.canResize)
			{
				return false;
			}
			Link link = this.links[this.links.Length - 1];
			Link link2 = this.links[this.activeLinkCount - 1];
			Vector3 target = link.transform.position - link2.transform.up * this.linkSpacing * 2f;
			if (this.activeLinkCount < this.maxLinkCount - 1)
			{
				link2.ConnectedBody = null;
				link2.transform.position = Vector3.MoveTowards(link2.transform.position, target, Mathf.Abs(_speed) * Time.deltaTime);
				if (Vector3.SqrMagnitude(link2.transform.position - link.transform.position) > this.linkSpacing * this.linkSpacing)
				{
					Link link3 = this.links[this.activeLinkCount];
					link3.transform.position = link2.transform.position + (link.transform.position - link2.transform.position).normalized * this.linkSpacing;
					link3.transform.rotation = link2.transform.rotation;
					this.activeLinkCount++;
					link3.IsActive = true;
					link2.ConnectedBody = link3.Rigidbody;
					link2 = link3;
				}
				link2.ApplyJointSettings(Vector3.Distance(link.transform.position, link2.transform.position) * (1f / this.linkScale));
				link2.ConnectedBody = link.Rigidbody;
			}
			else
			{
				this.kVelocity = 0f;
			}
			return true;
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x001E5C3C File Offset: 0x001E403C
		public bool Retract(float _speed, int _minJointCount)
		{
			if (!this.canResize)
			{
				return false;
			}
			Link link = this.links[this.links.Length - 1];
			Link link2 = this.links[this.activeLinkCount - 1];
			link2.ConnectedBody = null;
			link2.transform.position = Vector3.MoveTowards(link2.transform.position, link.transform.position, Mathf.Abs(_speed) * Time.deltaTime);
			if (this.activeLinkCount > _minJointCount)
			{
				if (Vector3.SqrMagnitude(link.transform.position - link2.transform.position) <= 0.01f)
				{
					link2.IsActive = false;
					this.activeLinkCount--;
					link2 = this.links[this.activeLinkCount - 1];
				}
			}
			else
			{
				this.kVelocity = 0f;
				link2.transform.position = link.transform.position - link2.transform.up * this.linkSpacing;
			}
			link2.Joint.anchor = new Vector3(0f, Vector3.Distance(link.transform.position, link2.transform.position) * (1f / this.linkScale), 0f);
			link2.ConnectedBody = link.Rigidbody;
			return true;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x001E5D9C File Offset: 0x001E419C
		public static QuickRope Create(Vector3 _position, GameObject _prefab, float _linkSpacing = 1f, float _prefabScale = 0.5f)
		{
			return QuickRope.Create(_position, _position + new Vector3(0f, 5f, 0f), _prefab, _linkSpacing, _prefabScale);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x001E5DC4 File Offset: 0x001E41C4
		public static QuickRope Create(Vector3 _pointA, Vector3 _pointB, GameObject _prefab, float _linkSpacing = 1f, float _prefabScale = 0.5f)
		{
			QuickRope quickRope = new GameObject("Rope")
			{
				transform = 
				{
					position = _pointA
				}
			}.AddComponent<QuickRope>();
			quickRope.defaultPrefab = _prefab;
			Vector3 normalized = (_pointB - _pointA).normalized;
			quickRope.Spline.SetControlPoint(1, _pointB, Space.World);
			quickRope.Spline.SetPoint(1, _pointA + normalized, Space.World);
			quickRope.Spline.SetPoint(2, _pointB - normalized, Space.World);
			quickRope.Generate();
			return quickRope;
		}

		// Token: 0x04004814 RID: 18452
		private const int MAX_JOINT_COUNT = 128;

		// Token: 0x04004815 RID: 18453
		private const float MIN_JOINT_SCALE = 0.001f;

		// Token: 0x04004816 RID: 18454
		private const float MIN_JOINT_SPACING = 0.001f;

		// Token: 0x04004817 RID: 18455
		private const string VERSION = "3.1.6";

		// Token: 0x04004818 RID: 18456
		[SerializeField]
		[Min(0.001f)]
		public float linkScale = 1f;

		// Token: 0x04004819 RID: 18457
		[SerializeField]
		[Min(0.001f)]
		public float linkSpacing = 0.5f;

		// Token: 0x0400481A RID: 18458
		[SerializeField]
		[Min(3f)]
		public int maxLinkCount = 50;

		// Token: 0x0400481B RID: 18459
		[SerializeField]
		[Min(1f)]
		public int minLinkCount = 1;

		// Token: 0x0400481C RID: 18460
		[SerializeField]
		public bool alternateRotation = true;

		// Token: 0x0400481D RID: 18461
		[SerializeField]
		public bool usePhysics = true;

		// Token: 0x0400481E RID: 18462
		[SerializeField]
		public bool canResize;

		// Token: 0x0400481F RID: 18463
		[SerializeField]
		public GameObject defaultPrefab;

		// Token: 0x04004820 RID: 18464
		[SerializeField]
		public TransformSettings defaultPrefabOffsets = new TransformSettings
		{
			position = new Vector3(0f, 0.25f, 0f),
			scale = Vector3.one
		};

		// Token: 0x04004821 RID: 18465
		[SerializeField]
		public RigidbodySettings defaultRigidbodySettings = new RigidbodySettings
		{
			mass = 1f,
			drag = 0.1f,
			angularDrag = 0.05f,
			useGravity = true,
			isKinematic = false,
			solverCount = 6
		};

		// Token: 0x04004822 RID: 18466
		[SerializeField]
		public JointSettings defaultJointSettings = new JointSettings
		{
			breakForce = float.PositiveInfinity,
			breakTorque = float.PositiveInfinity,
			swingLimit = 90f,
			twistLimit = 40f
		};

		// Token: 0x04004823 RID: 18467
		[SerializeField]
		public ColliderSettings defaultColliderSettings = new ColliderSettings
		{
			size = Vector3.one,
			height = 2f,
			center = Vector3.zero,
			radius = 1f
		};

		// Token: 0x04004824 RID: 18468
		[SerializeField]
		public float velocityAccel = 1f;

		// Token: 0x04004825 RID: 18469
		[SerializeField]
		public float velocityDampen = 0.98f;

		// Token: 0x04004826 RID: 18470
		[SerializeField]
		[HideInInspector]
		private float velocity;

		// Token: 0x04004827 RID: 18471
		[SerializeField]
		[HideInInspector]
		private float kVelocity;

		// Token: 0x04004828 RID: 18472
		[SerializeField]
		[HideInInspector]
		private int activeLinkCount;

		// Token: 0x04004829 RID: 18473
		[SerializeField]
		private Link[] links = new Link[0];

		// Token: 0x0400482A RID: 18474
		[SerializeField]
		[HideInInspector]
		private Spline spline;

		// Token: 0x02000A6E RID: 2670
		public enum RenderType
		{
			// Token: 0x0400482C RID: 18476
			Prefab,
			// Token: 0x0400482D RID: 18477
			Rendered
		}

		// Token: 0x02000A6F RID: 2671
		public enum ColliderType
		{
			// Token: 0x0400482F RID: 18479
			None,
			// Token: 0x04004830 RID: 18480
			Sphere,
			// Token: 0x04004831 RID: 18481
			Box,
			// Token: 0x04004832 RID: 18482
			Capsule
		}
	}
}
