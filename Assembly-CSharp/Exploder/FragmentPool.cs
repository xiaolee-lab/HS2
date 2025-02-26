using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003C4 RID: 964
	public class FragmentPool : MonoBehaviour
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0006440C File Offset: 0x0006280C
		public static FragmentPool Instance
		{
			get
			{
				if (FragmentPool.instance == null)
				{
					GameObject gameObject = new GameObject("FragmentRoot");
					FragmentPool.instance = gameObject.AddComponent<FragmentPool>();
				}
				return FragmentPool.instance;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00064444 File Offset: 0x00062844
		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			FragmentPool.instance = this;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00064457 File Offset: 0x00062857
		private void OnDestroy()
		{
			this.DestroyFragments();
			FragmentPool.instance = null;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00064465 File Offset: 0x00062865
		public int PoolSize
		{
			get
			{
				return this.pool.Length;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0006446F File Offset: 0x0006286F
		public Fragment[] Pool
		{
			get
			{
				return this.pool;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00064478 File Offset: 0x00062878
		public List<Fragment> GetAvailableFragments(int size)
		{
			if (size > this.pool.Length)
			{
				return null;
			}
			if (size == this.pool.Length)
			{
				return new List<Fragment>(this.pool);
			}
			List<Fragment> list = new List<Fragment>();
			int num = 0;
			foreach (Fragment fragment in this.pool)
			{
				if (!fragment.IsActive && !fragment.Cracked)
				{
					list.Add(fragment);
					num++;
				}
				if (num == size)
				{
					return list;
				}
			}
			foreach (Fragment fragment2 in this.pool)
			{
				if (!fragment2.Visible && !fragment2.Cracked)
				{
					list.Add(fragment2);
					num++;
				}
				if (num == size)
				{
					return list;
				}
			}
			if (num < size)
			{
				foreach (Fragment fragment3 in this.pool)
				{
					if (fragment3.IsSleeping() && fragment3.Visible && !fragment3.Cracked)
					{
						list.Add(fragment3);
						num++;
					}
					if (num == size)
					{
						return list;
					}
				}
			}
			if (num < size)
			{
				foreach (Fragment fragment4 in this.pool)
				{
					if (!fragment4.IsSleeping() && fragment4.Visible && !fragment4.Cracked)
					{
						list.Add(fragment4);
						num++;
					}
					if (num == size)
					{
						return list;
					}
				}
			}
			return list;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0006462C File Offset: 0x00062A2C
		public int GetAvailableCrackFragmentsCount()
		{
			int num = 0;
			foreach (Fragment fragment in this.pool)
			{
				if (!fragment.Cracked)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0006466C File Offset: 0x00062A6C
		public void Reset(ExploderParams parameters)
		{
			this.Allocate(parameters.FragmentPoolSize, parameters.FragmentOptions.MeshColliders, parameters.Use2DCollision);
			this.SetExplodableFragments(parameters.FragmentOptions.ExplodeFragments, parameters.DontUseTag);
			this.SetFragmentPhysicsOptions(parameters.FragmentOptions, parameters.Use2DCollision);
			this.SetSFXOptions(parameters.FragmentSFX);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000646CC File Offset: 0x00062ACC
		public void Allocate(int poolSize, bool useMeshColliders, bool use2dCollision)
		{
			if (this.pool == null || this.pool.Length < poolSize || useMeshColliders != this.meshColliders)
			{
				this.DestroyFragments();
				this.pool = new Fragment[poolSize];
				this.meshColliders = useMeshColliders;
				GameObject gameObject = null;
				Fragment fragment = UnityEngine.Object.FindObjectOfType<Fragment>();
				if (fragment)
				{
					gameObject = fragment.gameObject;
				}
				else
				{
					UnityEngine.Object @object = Resources.Load("ExploderFragment");
					if (@object)
					{
						gameObject = (UnityEngine.Object.Instantiate(@object) as GameObject);
					}
				}
				for (int i = 0; i < poolSize; i++)
				{
					GameObject gameObject2;
					if (gameObject)
					{
						gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						gameObject2.name = "fragment_" + i;
					}
					else
					{
						gameObject2 = new GameObject("fragment_" + i);
					}
					gameObject2.AddComponent<MeshFilter>();
					gameObject2.AddComponent<MeshRenderer>();
					if (use2dCollision)
					{
						gameObject2.AddComponent<PolygonCollider2D>();
						gameObject2.AddComponent<Rigidbody2D>();
					}
					else
					{
						if (useMeshColliders)
						{
							MeshCollider meshCollider = gameObject2.AddComponent<MeshCollider>();
							meshCollider.convex = true;
						}
						else
						{
							gameObject2.AddComponent<BoxCollider>();
						}
						gameObject2.AddComponent<Rigidbody>();
					}
					gameObject2.AddComponent<ExploderOption>();
					Fragment fragment2 = gameObject2.GetComponent<Fragment>();
					if (!fragment2)
					{
						fragment2 = gameObject2.AddComponent<Fragment>();
					}
					gameObject2.transform.parent = base.gameObject.transform;
					this.pool[i] = fragment2;
					ExploderUtils.SetActiveRecursively(gameObject2.gameObject, false);
					fragment2.RefreshComponentsCache();
					fragment2.Sleep();
				}
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0006486C File Offset: 0x00062C6C
		public void ResetTransform()
		{
			base.gameObject.transform.position = Vector3.zero;
			base.gameObject.transform.rotation = Quaternion.identity;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00064898 File Offset: 0x00062C98
		public void WakeUp()
		{
			foreach (Fragment fragment in this.pool)
			{
				fragment.WakeUp();
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000648CC File Offset: 0x00062CCC
		public void Sleep()
		{
			foreach (Fragment fragment in this.pool)
			{
				fragment.Sleep();
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00064900 File Offset: 0x00062D00
		public void DestroyFragments()
		{
			if (this.pool != null)
			{
				foreach (Fragment fragment in this.pool)
				{
					if (fragment)
					{
						UnityEngine.Object.Destroy(fragment.gameObject);
					}
				}
				this.pool = null;
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00064954 File Offset: 0x00062D54
		public void DeactivateFragments()
		{
			if (this.pool != null)
			{
				foreach (Fragment fragment in this.pool)
				{
					if (fragment)
					{
						fragment.Deactivate();
					}
				}
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0006499C File Offset: 0x00062D9C
		public void SetExplodableFragments(bool explodable, bool dontUseTag)
		{
			if (this.pool != null)
			{
				if (dontUseTag)
				{
					foreach (Fragment fragment in this.pool)
					{
						if (fragment.gameObject && !fragment.gameObject.GetComponent<Explodable>())
						{
							fragment.gameObject.AddComponent<Explodable>();
						}
					}
				}
				else if (explodable)
				{
					foreach (Fragment fragment2 in this.pool)
					{
						fragment2.tag = ExploderObject.Tag;
					}
				}
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00064A48 File Offset: 0x00062E48
		public void SetFragmentPhysicsOptions(FragmentOption options, bool physics2d)
		{
			if (this.pool != null)
			{
				RigidbodyConstraints rigidbodyConstraints = RigidbodyConstraints.None;
				if (options.FreezePositionX)
				{
					rigidbodyConstraints |= RigidbodyConstraints.FreezePositionX;
				}
				if (options.FreezePositionY)
				{
					rigidbodyConstraints |= RigidbodyConstraints.FreezePositionY;
				}
				if (options.FreezePositionZ)
				{
					rigidbodyConstraints |= RigidbodyConstraints.FreezePositionZ;
				}
				if (options.FreezeRotationX)
				{
					rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationX;
				}
				if (options.FreezeRotationY)
				{
					rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationY;
				}
				if (options.FreezeRotationZ)
				{
					rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationZ;
				}
				foreach (Fragment fragment in this.pool)
				{
					if (fragment.gameObject)
					{
						fragment.gameObject.layer = LayerMask.NameToLayer(options.Layer);
					}
					fragment.SetConstraints(rigidbodyConstraints);
					fragment.DisableColliders(options.DisableColliders, this.meshColliders, physics2d);
				}
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00064B20 File Offset: 0x00062F20
		public void SetSFXOptions(FragmentSFX sfx)
		{
			if (this.pool != null)
			{
				int num = 0;
				foreach (Fragment fragment in this.pool)
				{
					if (!fragment.IsActive && num++ <= sfx.EmitersMax)
					{
						fragment.InitSFX(sfx);
					}
				}
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00064B7C File Offset: 0x00062F7C
		public List<Fragment> GetActiveFragments()
		{
			if (this.pool != null)
			{
				List<Fragment> list = new List<Fragment>(this.pool.Length);
				foreach (Fragment fragment in this.pool)
				{
					if (ExploderUtils.IsActive(fragment.gameObject))
					{
						list.Add(fragment);
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x0400130E RID: 4878
		private static FragmentPool instance;

		// Token: 0x0400130F RID: 4879
		private Fragment[] pool;

		// Token: 0x04001310 RID: 4880
		private bool meshColliders;
	}
}
