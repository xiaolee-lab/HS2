using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000457 RID: 1111
public class EffectSettings : MonoBehaviour
{
	// Token: 0x14000053 RID: 83
	// (add) Token: 0x06001457 RID: 5207 RVA: 0x0007FA9C File Offset: 0x0007DE9C
	// (remove) Token: 0x06001458 RID: 5208 RVA: 0x0007FAD4 File Offset: 0x0007DED4
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EventHandler<CollisionInfo> CollisionEnter;

	// Token: 0x14000054 RID: 84
	// (add) Token: 0x06001459 RID: 5209 RVA: 0x0007FB0C File Offset: 0x0007DF0C
	// (remove) Token: 0x0600145A RID: 5210 RVA: 0x0007FB44 File Offset: 0x0007DF44
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event EventHandler<EventArgs> EffectDeactivated;

	// Token: 0x0600145B RID: 5211 RVA: 0x0007FB7A File Offset: 0x0007DF7A
	private void Start()
	{
		if (this.InstanceBehaviour == EffectSettings.DeactivationEnum.DestroyAfterTime)
		{
			UnityEngine.Object.Destroy(base.gameObject, this.DestroyTimeDelay);
		}
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x0007FB9C File Offset: 0x0007DF9C
	public void OnCollisionHandler(CollisionInfo e)
	{
		for (int i = 0; i < this.lastActiveIndex; i++)
		{
			base.Invoke("SetGoActive", this.active_value[i]);
		}
		for (int j = 0; j < this.lastInactiveIndex; j++)
		{
			base.Invoke("SetGoInactive", this.inactive_value[j]);
		}
		EventHandler<CollisionInfo> collisionEnter = this.CollisionEnter;
		if (collisionEnter != null)
		{
			collisionEnter(this, e);
		}
		if (this.InstanceBehaviour == EffectSettings.DeactivationEnum.Deactivate && !this.deactivatedIsWait)
		{
			this.deactivatedIsWait = true;
			base.Invoke("Deactivate", this.DeactivateTimeDelay);
		}
		if (this.InstanceBehaviour == EffectSettings.DeactivationEnum.DestroyAfterCollision)
		{
			UnityEngine.Object.Destroy(base.gameObject, this.DestroyTimeDelay);
		}
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x0007FC60 File Offset: 0x0007E060
	public void OnEffectDeactivatedHandler()
	{
		EventHandler<EventArgs> effectDeactivated = this.EffectDeactivated;
		if (effectDeactivated != null)
		{
			effectDeactivated(this, EventArgs.Empty);
		}
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x0007FC86 File Offset: 0x0007E086
	public void Deactivate()
	{
		this.OnEffectDeactivatedHandler();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x0007FC9A File Offset: 0x0007E09A
	private void SetGoActive()
	{
		this.active_key[this.currentActiveGo].SetActive(false);
		this.currentActiveGo++;
		if (this.currentActiveGo >= this.lastActiveIndex)
		{
			this.currentActiveGo = 0;
		}
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x0007FCD5 File Offset: 0x0007E0D5
	private void SetGoInactive()
	{
		this.inactive_Key[this.currentInactiveGo].SetActive(true);
		this.currentInactiveGo++;
		if (this.currentInactiveGo >= this.lastInactiveIndex)
		{
			this.currentInactiveGo = 0;
		}
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x0007FD10 File Offset: 0x0007E110
	public void OnEnable()
	{
		for (int i = 0; i < this.lastActiveIndex; i++)
		{
			this.active_key[i].SetActive(true);
		}
		for (int j = 0; j < this.lastInactiveIndex; j++)
		{
			this.inactive_Key[j].SetActive(false);
		}
		this.deactivatedIsWait = false;
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0007FD6E File Offset: 0x0007E16E
	public void OnDisable()
	{
		base.CancelInvoke("SetGoActive");
		base.CancelInvoke("SetGoInactive");
		base.CancelInvoke("Deactivate");
		this.currentActiveGo = 0;
		this.currentInactiveGo = 0;
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0007FD9F File Offset: 0x0007E19F
	public void RegistreActiveElement(GameObject go, float time)
	{
		this.active_key[this.lastActiveIndex] = go;
		this.active_value[this.lastActiveIndex] = time;
		this.lastActiveIndex++;
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0007FDCB File Offset: 0x0007E1CB
	public void RegistreInactiveElement(GameObject go, float time)
	{
		this.inactive_Key[this.lastInactiveIndex] = go;
		this.inactive_value[this.lastInactiveIndex] = time;
		this.lastInactiveIndex++;
	}

	// Token: 0x04001733 RID: 5939
	[Tooltip("Type of the effect")]
	public EffectSettings.EffectTypeEnum EffectType;

	// Token: 0x04001734 RID: 5940
	[Tooltip("The radius of the collider is required to correctly calculate the collision point. For example, if the radius 0.5m, then the position of the collision is shifted on 0.5m relative motion vector.")]
	public float ColliderRadius = 0.2f;

	// Token: 0x04001735 RID: 5941
	[Tooltip("The radius of the \"Area Of Damage (AOE)\"")]
	public float EffectRadius;

	// Token: 0x04001736 RID: 5942
	[Tooltip("Get the position of the movement of the motion vector, and not to follow to the target.")]
	public bool UseMoveVector;

	// Token: 0x04001737 RID: 5943
	[Tooltip("A projectile will be moved to the target (any object)")]
	public GameObject Target;

	// Token: 0x04001738 RID: 5944
	[Tooltip("Motion vector for the projectile (eg Vector3.Forward)")]
	public Vector3 MoveVector = Vector3.forward;

	// Token: 0x04001739 RID: 5945
	[Tooltip("The speed of the projectile")]
	public float MoveSpeed = 1f;

	// Token: 0x0400173A RID: 5946
	[Tooltip("Should the projectile have move to the target, until the target not reaches?")]
	public bool IsHomingMove;

	// Token: 0x0400173B RID: 5947
	[Tooltip("Distance flight of the projectile, after which the projectile is deactivated and call a collision event with a null value \"RaycastHit\"")]
	public float MoveDistance = 20f;

	// Token: 0x0400173C RID: 5948
	[Tooltip("Allows you to smoothly activate / deactivate effects which have an indefinite lifetime")]
	public bool IsVisible = true;

	// Token: 0x0400173D RID: 5949
	[Tooltip("Whether to deactivate or destroy the effect after a collision. Deactivation allows you to reuse the effect without instantiating, using \"effect.SetActive (true)\"")]
	public EffectSettings.DeactivationEnum InstanceBehaviour = EffectSettings.DeactivationEnum.Nothing;

	// Token: 0x0400173E RID: 5950
	[Tooltip("Delay before deactivating effect. (For example, after effect, some particles must have time to disappear).")]
	public float DeactivateTimeDelay = 4f;

	// Token: 0x0400173F RID: 5951
	[Tooltip("Delay before deleting effect. (For example, after effect, some particles must have time to disappear).")]
	public float DestroyTimeDelay = 10f;

	// Token: 0x04001740 RID: 5952
	[Tooltip("Allows you to adjust the layers, which can interact with the projectile.")]
	public LayerMask LayerMask = -1;

	// Token: 0x04001743 RID: 5955
	private GameObject[] active_key = new GameObject[100];

	// Token: 0x04001744 RID: 5956
	private float[] active_value = new float[100];

	// Token: 0x04001745 RID: 5957
	private GameObject[] inactive_Key = new GameObject[100];

	// Token: 0x04001746 RID: 5958
	private float[] inactive_value = new float[100];

	// Token: 0x04001747 RID: 5959
	private int lastActiveIndex;

	// Token: 0x04001748 RID: 5960
	private int lastInactiveIndex;

	// Token: 0x04001749 RID: 5961
	private int currentActiveGo;

	// Token: 0x0400174A RID: 5962
	private int currentInactiveGo;

	// Token: 0x0400174B RID: 5963
	private bool deactivatedIsWait;

	// Token: 0x02000458 RID: 1112
	public enum EffectTypeEnum
	{
		// Token: 0x0400174D RID: 5965
		Projectile,
		// Token: 0x0400174E RID: 5966
		AOE,
		// Token: 0x0400174F RID: 5967
		Other
	}

	// Token: 0x02000459 RID: 1113
	public enum DeactivationEnum
	{
		// Token: 0x04001751 RID: 5969
		Deactivate,
		// Token: 0x04001752 RID: 5970
		DestroyAfterCollision,
		// Token: 0x04001753 RID: 5971
		DestroyAfterTime,
		// Token: 0x04001754 RID: 5972
		Nothing
	}
}
