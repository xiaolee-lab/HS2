using System;
using UnityEngine;

namespace PicoGames.QuickRopes
{
	// Token: 0x02000A6A RID: 2666
	[Serializable]
	public class Link1
	{
		// Token: 0x06004EED RID: 20205 RVA: 0x001E491F File Offset: 0x001E2D1F
		public Link1(GameObject _gameObject, int _index)
		{
			this.gameObject = _gameObject;
			this.linkIndex = _index;
			this.IsActive = false;
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x001E494A File Offset: 0x001E2D4A
		[SerializeField]
		public Transform transform
		{
			get
			{
				return this.gameObject.transform;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x001E4957 File Offset: 0x001E2D57
		// (set) Token: 0x06004EF0 RID: 20208 RVA: 0x001E495F File Offset: 0x001E2D5F
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

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x001E4968 File Offset: 0x001E2D68
		// (set) Token: 0x06004EF2 RID: 20210 RVA: 0x001E4975 File Offset: 0x001E2D75
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

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x001E4983 File Offset: 0x001E2D83
		// (set) Token: 0x06004EF4 RID: 20212 RVA: 0x001E49A3 File Offset: 0x001E2DA3
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

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x001E49C3 File Offset: 0x001E2DC3
		// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x001E49E3 File Offset: 0x001E2DE3
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

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x001E4A22 File Offset: 0x001E2E22
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x001E4A43 File Offset: 0x001E2E43
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

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x001E4A63 File Offset: 0x001E2E63
		public Rigidbody Rigidbody
		{
			get
			{
				return this.rigidbody;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x001E4A6B File Offset: 0x001E2E6B
		public CharacterJoint Joint
		{
			get
			{
				return this.joint;
			}
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x001E4A73 File Offset: 0x001E2E73
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

		// Token: 0x06004EFC RID: 20220 RVA: 0x001E4AB0 File Offset: 0x001E2EB0
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

		// Token: 0x06004EFD RID: 20221 RVA: 0x001E4C38 File Offset: 0x001E3038
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

		// Token: 0x06004EFE RID: 20222 RVA: 0x001E4DE4 File Offset: 0x001E31E4
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

		// Token: 0x06004EFF RID: 20223 RVA: 0x001E4F48 File Offset: 0x001E3348
		public void ApplyJointSettings(float _anchorOffset)
		{
			if (this.joint == null)
			{
				this.joint = this.gameObject.GetComponent<CharacterJoint>();
				if (this.joint == null)
				{
					this.joint = this.gameObject.AddComponent<CharacterJoint>();
				}
			}
			this.joint.anchor = new Vector3(0f, _anchorOffset, 0f);
			this.joint.autoConfigureConnectedAnchor = true;
			this.joint.axis = Vector3.up;
			this.joint.swingAxis = Vector3.right;
			this.joint.lowTwistLimit = new SoftJointLimit
			{
				limit = -this.jointSettings.twistLimit
			};
			this.joint.highTwistLimit = new SoftJointLimit
			{
				limit = this.jointSettings.twistLimit
			};
			this.joint.swing1Limit = new SoftJointLimit
			{
				limit = this.jointSettings.swingLimit
			};
			this.joint.swing2Limit = new SoftJointLimit
			{
				limit = this.jointSettings.swingLimit
			};
			this.joint.breakForce = this.jointSettings.breakForce;
			this.joint.breakTorque = this.jointSettings.breakTorque;
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x001E50A5 File Offset: 0x001E34A5
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

		// Token: 0x06004F01 RID: 20225 RVA: 0x001E50E8 File Offset: 0x001E34E8
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

		// Token: 0x06004F02 RID: 20226 RVA: 0x001E5139 File Offset: 0x001E3539
		public GameObject AttachedPrefab()
		{
			return this.attachedPrefab;
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x001E5141 File Offset: 0x001E3541
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

		// Token: 0x040047F6 RID: 18422
		[SerializeField]
		public bool overridePrefab;

		// Token: 0x040047F7 RID: 18423
		[SerializeField]
		public bool overrideOffsetSettings;

		// Token: 0x040047F8 RID: 18424
		[SerializeField]
		public bool overrideRigidbodySettings;

		// Token: 0x040047F9 RID: 18425
		[SerializeField]
		public bool overrideJointSettings;

		// Token: 0x040047FA RID: 18426
		[SerializeField]
		public bool overrideColliderSettings;

		// Token: 0x040047FB RID: 18427
		[SerializeField]
		public GameObject prefab;

		// Token: 0x040047FC RID: 18428
		[SerializeField]
		public TransformSettings offsetSettings;

		// Token: 0x040047FD RID: 18429
		[SerializeField]
		public RigidbodySettings rigidbodySettings;

		// Token: 0x040047FE RID: 18430
		[SerializeField]
		public JointSettings jointSettings;

		// Token: 0x040047FF RID: 18431
		[SerializeField]
		public ColliderSettings colliderSettings;

		// Token: 0x04004800 RID: 18432
		[SerializeField]
		public bool alternateJoints = true;

		// Token: 0x04004801 RID: 18433
		[SerializeField]
		public GameObject gameObject;

		// Token: 0x04004802 RID: 18434
		[SerializeField]
		public Collider collider;

		// Token: 0x04004803 RID: 18435
		[SerializeField]
		private GameObject attachedPrefab;

		// Token: 0x04004804 RID: 18436
		[SerializeField]
		private Rigidbody rigidbody;

		// Token: 0x04004805 RID: 18437
		[SerializeField]
		private CharacterJoint joint;

		// Token: 0x04004806 RID: 18438
		[SerializeField]
		private int linkIndex = -1;

		// Token: 0x04004807 RID: 18439
		[SerializeField]
		private bool prevIsKinematic;
	}
}
