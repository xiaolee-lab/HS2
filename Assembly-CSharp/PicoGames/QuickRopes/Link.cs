using System;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A69 RID: 2665
	[Serializable]
	public class Link
	{
		// Token: 0x06004ED6 RID: 20182 RVA: 0x001E416C File Offset: 0x001E256C
		public Link(GameObject _gameObject, int _index)
		{
			this.gameObject = _gameObject;
			this.linkIndex = _index;
			this.IsActive = false;
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x001E4197 File Offset: 0x001E2597
		[SerializeField]
		public Transform transform
		{
			get
			{
				return this.gameObject.transform;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x001E41A4 File Offset: 0x001E25A4
		// (set) Token: 0x06004ED9 RID: 20185 RVA: 0x001E41AC File Offset: 0x001E25AC
		public GameObject Prefab
		{
			get
			{
				return this.prefab;
			}
			set
			{
				this.prefab = value;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x001E41B5 File Offset: 0x001E25B5
		// (set) Token: 0x06004EDB RID: 20187 RVA: 0x001E41C2 File Offset: 0x001E25C2
		public Transform Parent
		{
			get
			{
				return this.transform.parent;
			}
			set
			{
				this.transform.parent = value;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004EDC RID: 20188 RVA: 0x001E41D0 File Offset: 0x001E25D0
		// (set) Token: 0x06004EDD RID: 20189 RVA: 0x001E41F0 File Offset: 0x001E25F0
		public Rigidbody ConnectedBody
		{
			get
			{
				if (this.joint == null)
				{
					return null;
				}
				return this.joint.connectedBody;
			}
			set
			{
				if (this.joint == null)
				{
					return;
				}
				this.joint.connectedBody = value;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x001E4210 File Offset: 0x001E2610
		// (set) Token: 0x06004EDF RID: 20191 RVA: 0x001E4230 File Offset: 0x001E2630
		public bool IsActive
		{
			get
			{
				return !(this.gameObject == null) && this.gameObject.activeSelf;
			}
			set
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.hideFlags = ((!value) ? HideFlags.HideInHierarchy : HideFlags.None);
				this.gameObject.SetActive(value);
				this.IsPrefabActive = true;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x001E426F File Offset: 0x001E266F
		// (set) Token: 0x06004EE1 RID: 20193 RVA: 0x001E4290 File Offset: 0x001E2690
		public bool IsPrefabActive
		{
			get
			{
				return this.attachedPrefab != null && this.attachedPrefab.activeSelf;
			}
			set
			{
				if (this.attachedPrefab == null)
				{
					return;
				}
				this.attachedPrefab.SetActive(value);
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x001E42B0 File Offset: 0x001E26B0
		public Rigidbody Rigidbody
		{
			get
			{
				return this.rigidbody;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x001E42B8 File Offset: 0x001E26B8
		public ConfigurableJoint Joint
		{
			get
			{
				return this.joint;
			}
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x001E42C0 File Offset: 0x001E26C0
		public bool TogglePhysicsEnabled(bool _enabled)
		{
			if (_enabled)
			{
				this.Rigidbody.isKinematic = this.prevIsKinematic;
			}
			else
			{
				this.prevIsKinematic = this.Rigidbody.isKinematic;
				this.Rigidbody.isKinematic = true;
			}
			return _enabled;
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x001E42FC File Offset: 0x001E26FC
		public void ApplyPrefabSettings()
		{
			if (this.transform.childCount > 0)
			{
				if (!Application.isPlaying)
				{
					while (this.transform.childCount > 0)
					{
						UnityEngine.Object.DestroyImmediate(this.transform.GetChild(0).gameObject, false);
					}
				}
				else
				{
					for (int i = 0; i < this.transform.childCount; i++)
					{
						UnityEngine.Object.Destroy(this.transform.GetChild(i).gameObject);
					}
				}
			}
			if (this.prefab != null)
			{
				if (this.prefab.activeInHierarchy)
				{
					this.attachedPrefab = this.prefab;
				}
				else
				{
					this.attachedPrefab = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
				}
				this.attachedPrefab.name = this.prefab.name;
				this.attachedPrefab.transform.parent = this.transform;
				this.attachedPrefab.transform.localPosition = this.offsetSettings.position;
				this.attachedPrefab.transform.localRotation = this.offsetSettings.rotation * (this.alternateJoints ? Quaternion.AngleAxis((float)((this.linkIndex % 2 != 0) ? 0 : 90), Vector3.up) : Quaternion.identity);
				this.attachedPrefab.transform.localScale = this.offsetSettings.scale;
			}
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x001E4484 File Offset: 0x001E2884
		public void ApplyColliderSettings()
		{
			Collider[] components = this.gameObject.GetComponents<Collider>();
			for (int i = 0; i < components.Length; i++)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(components[i]);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(components[i]);
				}
			}
			switch (this.colliderSettings.type)
			{
			case QuickRope.ColliderType.None:
				this.collider = null;
				break;
			case QuickRope.ColliderType.Sphere:
			{
				this.collider = this.gameObject.AddComponent<SphereCollider>();
				SphereCollider sphereCollider = this.collider as SphereCollider;
				sphereCollider.material = this.colliderSettings.physicsMaterial;
				sphereCollider.radius = this.colliderSettings.radius;
				sphereCollider.center = this.colliderSettings.center;
				break;
			}
			case QuickRope.ColliderType.Box:
			{
				this.collider = this.gameObject.AddComponent<BoxCollider>();
				BoxCollider boxCollider = this.collider as BoxCollider;
				boxCollider.material = this.colliderSettings.physicsMaterial;
				boxCollider.size = this.colliderSettings.size;
				boxCollider.center = this.colliderSettings.center;
				break;
			}
			case QuickRope.ColliderType.Capsule:
			{
				this.collider = this.gameObject.AddComponent<CapsuleCollider>();
				CapsuleCollider capsuleCollider = this.collider as CapsuleCollider;
				capsuleCollider.material = this.colliderSettings.physicsMaterial;
				capsuleCollider.radius = this.colliderSettings.radius;
				capsuleCollider.height = this.colliderSettings.height;
				capsuleCollider.direction = (int)this.colliderSettings.direction;
				capsuleCollider.center = this.colliderSettings.center;
				break;
			}
			}
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x001E4630 File Offset: 0x001E2A30
		public void ApplyRigidbodySettings()
		{
			if (this.rigidbody == null)
			{
				this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
				if (this.rigidbody == null)
				{
					this.rigidbody = this.gameObject.AddComponent<Rigidbody>();
				}
			}
			this.prevIsKinematic = this.rigidbodySettings.isKinematic;
			this.rigidbody.mass = this.rigidbodySettings.mass;
			this.rigidbody.drag = this.rigidbodySettings.drag;
			this.rigidbody.angularDrag = this.rigidbodySettings.angularDrag;
			this.rigidbody.useGravity = this.rigidbodySettings.useGravity;
			this.rigidbody.isKinematic = this.rigidbodySettings.isKinematic;
			this.rigidbody.interpolation = this.rigidbodySettings.interpolate;
			this.rigidbody.collisionDetectionMode = this.rigidbodySettings.collisionDetection;
			this.rigidbody.constraints = this.rigidbodySettings.constraints;
			this.rigidbody.solverIterations = this.rigidbodySettings.solverCount;
			if (!this.rigidbody.isKinematic)
			{
				this.rigidbody.velocity = Vector3.zero;
				this.rigidbody.angularVelocity = Vector3.zero;
				this.rigidbody.Sleep();
			}
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x001E4794 File Offset: 0x001E2B94
		public void ApplyJointSettings(float _anchorOffset)
		{
			this.joint = this.gameObject.GetComponent<ConfigurableJoint>();
			if (this.joint == null)
			{
				this.joint = this.gameObject.AddComponent<ConfigurableJoint>();
			}
			this.joint.xMotion = ConfigurableJointMotion.Limited;
			this.joint.yMotion = ConfigurableJointMotion.Limited;
			this.joint.zMotion = ConfigurableJointMotion.Limited;
			this.joint.linearLimit = new SoftJointLimit
			{
				limit = this.jointSettings.swingLimit
			};
			this.joint.linearLimitSpring = new SoftJointLimitSpring
			{
				spring = this.jointSettings.spring,
				damper = this.jointSettings.damper
			};
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x001E4856 File Offset: 0x001E2C56
		public void RemoveJoint()
		{
			if (this.joint == null)
			{
				return;
			}
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this.joint);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.joint);
			}
			this.joint = null;
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x001E4898 File Offset: 0x001E2C98
		public void RemoveRigidbody()
		{
			if (this.rigidbody == null)
			{
				return;
			}
			this.RemoveJoint();
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this.rigidbody);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.rigidbody);
			}
			this.rigidbody = null;
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x001E48E9 File Offset: 0x001E2CE9
		public GameObject AttachedPrefab()
		{
			return this.attachedPrefab;
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x001E48F1 File Offset: 0x001E2CF1
		public void Destroy()
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.gameObject);
			}
			this.gameObject = null;
		}

		// Token: 0x040047E4 RID: 18404
		[SerializeField]
		public bool overridePrefab;

		// Token: 0x040047E5 RID: 18405
		[SerializeField]
		public bool overrideOffsetSettings;

		// Token: 0x040047E6 RID: 18406
		[SerializeField]
		public bool overrideRigidbodySettings;

		// Token: 0x040047E7 RID: 18407
		[SerializeField]
		public bool overrideJointSettings;

		// Token: 0x040047E8 RID: 18408
		[SerializeField]
		public bool overrideColliderSettings;

		// Token: 0x040047E9 RID: 18409
		[SerializeField]
		public GameObject prefab;

		// Token: 0x040047EA RID: 18410
		[SerializeField]
		public TransformSettings offsetSettings;

		// Token: 0x040047EB RID: 18411
		[SerializeField]
		public RigidbodySettings rigidbodySettings;

		// Token: 0x040047EC RID: 18412
		[SerializeField]
		public JointSettings jointSettings;

		// Token: 0x040047ED RID: 18413
		[SerializeField]
		public ColliderSettings colliderSettings;

		// Token: 0x040047EE RID: 18414
		[SerializeField]
		public bool alternateJoints = true;

		// Token: 0x040047EF RID: 18415
		[SerializeField]
		public GameObject gameObject;

		// Token: 0x040047F0 RID: 18416
		[SerializeField]
		public Collider collider;

		// Token: 0x040047F1 RID: 18417
		[SerializeField]
		private GameObject attachedPrefab;

		// Token: 0x040047F2 RID: 18418
		[SerializeField]
		private Rigidbody rigidbody;

		// Token: 0x040047F3 RID: 18419
		[SerializeField]
		private ConfigurableJoint joint;

		// Token: 0x040047F4 RID: 18420
		[SerializeField]
		private int linkIndex = -1;

		// Token: 0x040047F5 RID: 18421
		[SerializeField]
		private bool prevIsKinematic;
	}
}
