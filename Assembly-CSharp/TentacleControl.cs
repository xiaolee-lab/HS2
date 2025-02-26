using System;
using UnityEngine;

// Token: 0x02000A59 RID: 2649
public class TentacleControl : MonoBehaviour
{
	// Token: 0x06004E9F RID: 20127 RVA: 0x001E28FB File Offset: 0x001E0CFB
	private void Start()
	{
		this.SetupPhysicsBones();
	}

	// Token: 0x06004EA0 RID: 20128 RVA: 0x001E2903 File Offset: 0x001E0D03
	private void Update()
	{
	}

	// Token: 0x06004EA1 RID: 20129 RVA: 0x001E2908 File Offset: 0x001E0D08
	private void SetupPhysicsBones()
	{
		MetaballCellObject componentInChildren = this.seed.boneRoot.GetComponentInChildren<MetaballCellObject>();
		if (componentInChildren != null)
		{
			this.SetupPhysicsBonesRecursive(componentInChildren, true);
		}
	}

	// Token: 0x06004EA2 RID: 20130 RVA: 0x001E293C File Offset: 0x001E0D3C
	private void SetupPhysicsBonesRecursive(MetaballCellObject obj, bool bRoot = false)
	{
		Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
		if (rigidbody == null)
		{
			rigidbody = obj.gameObject.AddComponent<Rigidbody>();
		}
		rigidbody.useGravity = false;
		if (bRoot)
		{
			FixedJoint fixedJoint = obj.GetComponent<FixedJoint>();
			if (fixedJoint == null)
			{
				fixedJoint = obj.gameObject.AddComponent<FixedJoint>();
			}
			fixedJoint.connectedBody = this.seed.GetComponent<Rigidbody>();
		}
		else
		{
			HingeJoint hingeJoint = obj.GetComponent<HingeJoint>();
			if (hingeJoint == null)
			{
				hingeJoint = obj.gameObject.AddComponent<HingeJoint>();
			}
			hingeJoint.connectedBody = obj.transform.parent.GetComponent<Rigidbody>();
			hingeJoint.useLimits = true;
			hingeJoint.limits = new JointLimits
			{
				max = 30f,
				min = -30f
			};
		}
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			Transform child = obj.transform.GetChild(i);
			MetaballCellObject component = child.GetComponent<MetaballCellObject>();
			if (component != null)
			{
				this.SetupPhysicsBonesRecursive(component, false);
			}
		}
	}

	// Token: 0x04004790 RID: 18320
	public SkinnedMetaballSeed seed;
}
