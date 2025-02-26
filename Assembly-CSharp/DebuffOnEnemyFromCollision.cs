using System;
using UnityEngine;

// Token: 0x02000449 RID: 1097
public class DebuffOnEnemyFromCollision : MonoBehaviour
{
	// Token: 0x0600141C RID: 5148 RVA: 0x0007DE58 File Offset: 0x0007C258
	private void Start()
	{
		this.EffectSettings.CollisionEnter += this.EffectSettings_CollisionEnter;
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0007DE74 File Offset: 0x0007C274
	private void EffectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		if (this.Effect == null)
		{
			return;
		}
		Collider[] array = Physics.OverlapSphere(base.transform.position, this.EffectSettings.EffectRadius, this.EffectSettings.LayerMask);
		foreach (Collider collider in array)
		{
			Transform transform = collider.transform;
			Renderer componentInChildren = transform.GetComponentInChildren<Renderer>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Effect);
			gameObject.transform.parent = componentInChildren.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.GetComponent<AddMaterialOnHit>().UpdateMaterial(collider.transform);
		}
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0007DF2D File Offset: 0x0007C32D
	private void Update()
	{
	}

	// Token: 0x040016D8 RID: 5848
	public EffectSettings EffectSettings;

	// Token: 0x040016D9 RID: 5849
	public GameObject Effect;
}
