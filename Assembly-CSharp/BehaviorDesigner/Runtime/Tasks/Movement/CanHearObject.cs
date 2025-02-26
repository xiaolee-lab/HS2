using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000B7 RID: 183
	[TaskDescription("Check to see if the any objects are within hearing range of the current agent.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=12")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}CanHearObjectIcon.png")]
	public class CanHearObject : Conditional
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0001A628 File Offset: 0x00018A28
		public override TaskStatus OnUpdate()
		{
			if (this.targetObjects.Value != null && this.targetObjects.Value.Count > 0)
			{
				GameObject value = null;
				for (int i = 0; i < this.targetObjects.Value.Count; i++)
				{
					float num = 0f;
					GameObject gameObject;
					if (Vector3.Distance(this.targetObjects.Value[i].transform.position, this.transform.position) < this.hearingRadius.Value && (gameObject = MovementUtility.WithinHearingRange(this.transform, this.offset.Value, this.audibilityThreshold.Value, this.targetObjects.Value[i], ref num)) != null)
					{
						value = gameObject;
					}
				}
				this.returnedObject.Value = value;
			}
			else if (this.targetObject.Value == null)
			{
				if (this.usePhysics2D)
				{
					this.returnedObject.Value = MovementUtility.WithinHearingRange2D(this.transform, this.offset.Value, this.audibilityThreshold.Value, this.hearingRadius.Value, this.objectLayerMask);
				}
				else
				{
					this.returnedObject.Value = MovementUtility.WithinHearingRange(this.transform, this.offset.Value, this.audibilityThreshold.Value, this.hearingRadius.Value, this.objectLayerMask);
				}
			}
			else
			{
				GameObject gameObject2;
				if (!string.IsNullOrEmpty(this.targetTag.Value))
				{
					gameObject2 = GameObject.FindGameObjectWithTag(this.targetTag.Value);
				}
				else
				{
					gameObject2 = this.targetObject.Value;
				}
				if (Vector3.Distance(gameObject2.transform.position, this.transform.position) < this.hearingRadius.Value)
				{
					this.returnedObject.Value = MovementUtility.WithinHearingRange(this.transform, this.offset.Value, this.audibilityThreshold.Value, this.targetObject.Value);
				}
			}
			if (this.returnedObject.Value != null)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001A86D File Offset: 0x00018C6D
		public override void OnReset()
		{
			this.hearingRadius = 50f;
			this.audibilityThreshold = 0.05f;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001A88F File Offset: 0x00018C8F
		public override void OnDrawGizmos()
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001A891 File Offset: 0x00018C91
		public override void OnBehaviorComplete()
		{
			MovementUtility.ClearCache();
		}

		// Token: 0x04000370 RID: 880
		[Tooltip("Should the 2D version be used?")]
		public bool usePhysics2D;

		// Token: 0x04000371 RID: 881
		[Tooltip("The object that we are searching for")]
		public SharedGameObject targetObject;

		// Token: 0x04000372 RID: 882
		[Tooltip("The objects that we are searching for")]
		public SharedGameObjectList targetObjects;

		// Token: 0x04000373 RID: 883
		[Tooltip("The tag of the object that we are searching for")]
		public SharedString targetTag;

		// Token: 0x04000374 RID: 884
		[Tooltip("The LayerMask of the objects that we are searching for")]
		public LayerMask objectLayerMask;

		// Token: 0x04000375 RID: 885
		[Tooltip("How far away the unit can hear")]
		public SharedFloat hearingRadius = 50f;

		// Token: 0x04000376 RID: 886
		[Tooltip("The further away a sound source is the less likely the agent will be able to hear it. Set a threshold for the the minimum audibility level that the agent can hear")]
		public SharedFloat audibilityThreshold = 0.05f;

		// Token: 0x04000377 RID: 887
		[Tooltip("The hearing offset relative to the pivot position")]
		public SharedVector3 offset;

		// Token: 0x04000378 RID: 888
		[Tooltip("The returned object that is heard")]
		public SharedGameObject returnedObject;
	}
}
