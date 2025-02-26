using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000B8 RID: 184
	[TaskDescription("Check to see if the any objects are within sight of the agent.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=11")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}CanSeeObjectIcon.png")]
	public class CanSeeObject : Conditional
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0001A8E8 File Offset: 0x00018CE8
		public override TaskStatus OnUpdate()
		{
			if (this.usePhysics2D)
			{
				if (this.targetObjects.Value != null && this.targetObjects.Value.Count > 0)
				{
					GameObject value = null;
					float num = float.PositiveInfinity;
					for (int i = 0; i < this.targetObjects.Value.Count; i++)
					{
						float num2;
						GameObject gameObject;
						if ((gameObject = MovementUtility.WithinSight(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.targetObjects.Value[i], this.targetOffset.Value, true, this.angleOffset2D.Value, out num2, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone)) != null && num2 < num)
						{
							num = num2;
							value = gameObject;
						}
					}
					this.returnedObject.Value = value;
				}
				else if (this.targetObject.Value == null)
				{
					this.returnedObject.Value = MovementUtility.WithinSight2D(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.objectLayerMask, this.targetOffset.Value, this.angleOffset2D.Value, this.ignoreLayerMask);
				}
				else if (!string.IsNullOrEmpty(this.targetTag.Value))
				{
					this.returnedObject.Value = MovementUtility.WithinSight2D(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, GameObject.FindGameObjectWithTag(this.targetTag.Value), this.targetOffset.Value, this.angleOffset2D.Value, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone);
				}
				else
				{
					this.returnedObject.Value = MovementUtility.WithinSight2D(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.targetObject.Value, this.targetOffset.Value, this.angleOffset2D.Value, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone);
				}
			}
			else if (this.targetObjects.Value != null && this.targetObjects.Value.Count > 0)
			{
				GameObject value2 = null;
				float num3 = float.PositiveInfinity;
				for (int j = 0; j < this.targetObjects.Value.Count; j++)
				{
					float num4;
					GameObject gameObject2;
					if ((gameObject2 = MovementUtility.WithinSight(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.targetObjects.Value[j], this.targetOffset.Value, false, this.angleOffset2D.Value, out num4, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone)) != null && num4 < num3)
					{
						num3 = num4;
						value2 = gameObject2;
					}
				}
				this.returnedObject.Value = value2;
			}
			else if (this.targetObject.Value == null)
			{
				this.returnedObject.Value = MovementUtility.WithinSight(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.objectLayerMask, this.targetOffset.Value, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone);
			}
			else if (!string.IsNullOrEmpty(this.targetTag.Value))
			{
				this.returnedObject.Value = MovementUtility.WithinSight(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, GameObject.FindGameObjectWithTag(this.targetTag.Value), this.targetOffset.Value, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone);
			}
			else
			{
				this.returnedObject.Value = MovementUtility.WithinSight(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.targetObject.Value, this.targetOffset.Value, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone);
			}
			if (this.returnedObject.Value != null)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001ADD8 File Offset: 0x000191D8
		public override void OnReset()
		{
			this.fieldOfViewAngle = 90f;
			this.viewDistance = 1000f;
			this.offset = Vector3.zero;
			this.targetOffset = Vector3.zero;
			this.angleOffset2D = 0f;
			this.targetTag = string.Empty;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001AE48 File Offset: 0x00019248
		public override void OnDrawGizmos()
		{
			MovementUtility.DrawLineOfSight(base.Owner.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.angleOffset2D.Value, this.viewDistance.Value, this.usePhysics2D);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001AE97 File Offset: 0x00019297
		public override void OnBehaviorComplete()
		{
			MovementUtility.ClearCache();
		}

		// Token: 0x04000379 RID: 889
		[Tooltip("Should the 2D version be used?")]
		public bool usePhysics2D;

		// Token: 0x0400037A RID: 890
		[Tooltip("The object that we are searching for")]
		public SharedGameObject targetObject;

		// Token: 0x0400037B RID: 891
		[Tooltip("The objects that we are searching for")]
		public SharedGameObjectList targetObjects;

		// Token: 0x0400037C RID: 892
		[Tooltip("The tag of the object that we are searching for")]
		public SharedString targetTag;

		// Token: 0x0400037D RID: 893
		[Tooltip("The LayerMask of the objects that we are searching for")]
		public LayerMask objectLayerMask;

		// Token: 0x0400037E RID: 894
		[Tooltip("The LayerMask of the objects to ignore when performing the line of sight check")]
		public LayerMask ignoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");

		// Token: 0x0400037F RID: 895
		[Tooltip("The field of view angle of the agent (in degrees)")]
		public SharedFloat fieldOfViewAngle = 90f;

		// Token: 0x04000380 RID: 896
		[Tooltip("The distance that the agent can see")]
		public SharedFloat viewDistance = 1000f;

		// Token: 0x04000381 RID: 897
		[Tooltip("The raycast offset relative to the pivot position")]
		public SharedVector3 offset;

		// Token: 0x04000382 RID: 898
		[Tooltip("The target raycast offset relative to the pivot position")]
		public SharedVector3 targetOffset;

		// Token: 0x04000383 RID: 899
		[Tooltip("The offset to apply to 2D angles")]
		public SharedFloat angleOffset2D;

		// Token: 0x04000384 RID: 900
		[Tooltip("Should the target bone be used?")]
		public SharedBool useTargetBone;

		// Token: 0x04000385 RID: 901
		[Tooltip("The target's bone if the target is a humanoid")]
		public HumanBodyBones targetBone;

		// Token: 0x04000386 RID: 902
		[Tooltip("The object that is within sight")]
		public SharedGameObject returnedObject;
	}
}
