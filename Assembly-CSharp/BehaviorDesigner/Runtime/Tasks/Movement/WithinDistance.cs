using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C9 RID: 201
	[TaskDescription("Check to see if the any object specified by the object list or tag is within the distance specified of the current agent.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=18")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}WithinDistanceIcon.png")]
	public class WithinDistance : Conditional
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0001D05C File Offset: 0x0001B45C
		public override void OnStart()
		{
			this.sqrMagnitude = this.magnitude.Value * this.magnitude.Value;
			if (this.objects != null)
			{
				this.objects.Clear();
			}
			else
			{
				this.objects = new List<GameObject>();
			}
			if (this.targetObject.Value == null)
			{
				if (!string.IsNullOrEmpty(this.targetTag.Value))
				{
					GameObject[] array = GameObject.FindGameObjectsWithTag(this.targetTag.Value);
					for (int i = 0; i < array.Length; i++)
					{
						this.objects.Add(array[i]);
					}
				}
				else
				{
					Collider[] array2 = Physics.OverlapSphere(this.transform.position, this.magnitude.Value, this.objectLayerMask.value);
					for (int j = 0; j < array2.Length; j++)
					{
						this.objects.Add(array2[j].gameObject);
					}
				}
			}
			else
			{
				this.objects.Add(this.targetObject.Value);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001D178 File Offset: 0x0001B578
		public override TaskStatus OnUpdate()
		{
			if (this.transform == null || this.objects == null)
			{
				return TaskStatus.Failure;
			}
			for (int i = 0; i < this.objects.Count; i++)
			{
				if (!(this.objects[i] == null))
				{
					Vector3 vector = this.objects[i].transform.position - (this.transform.position + this.offset.Value);
					if (Vector3.SqrMagnitude(vector) < this.sqrMagnitude)
					{
						if (!this.lineOfSight.Value)
						{
							this.returnedObject.Value = this.objects[i];
							return TaskStatus.Success;
						}
						if (MovementUtility.LineOfSight(this.transform, this.offset.Value, this.objects[i], this.targetOffset.Value, this.usePhysics2D, this.ignoreLayerMask.value))
						{
							this.returnedObject.Value = this.objects[i];
							return TaskStatus.Success;
						}
					}
				}
			}
			return TaskStatus.Failure;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001D2B4 File Offset: 0x0001B6B4
		public override void OnReset()
		{
			this.usePhysics2D = false;
			this.targetObject = null;
			this.targetTag = string.Empty;
			this.objectLayerMask = 0;
			this.magnitude = 5f;
			this.lineOfSight = true;
			this.ignoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
			this.offset = Vector3.zero;
			this.targetOffset = Vector3.zero;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001D341 File Offset: 0x0001B741
		public override void OnDrawGizmos()
		{
		}

		// Token: 0x040003F0 RID: 1008
		[Tooltip("Should the 2D version be used?")]
		public bool usePhysics2D;

		// Token: 0x040003F1 RID: 1009
		[Tooltip("The object that we are searching for")]
		public SharedGameObject targetObject;

		// Token: 0x040003F2 RID: 1010
		[Tooltip("The tag of the object that we are searching for")]
		public SharedString targetTag;

		// Token: 0x040003F3 RID: 1011
		[Tooltip("The LayerMask of the objects that we are searching for")]
		public LayerMask objectLayerMask;

		// Token: 0x040003F4 RID: 1012
		[Tooltip("The distance that the object needs to be within")]
		public SharedFloat magnitude = 5f;

		// Token: 0x040003F5 RID: 1013
		[Tooltip("If true, the object must be within line of sight to be within distance. For example, if this option is enabled then an object behind a wall will not be within distance even though it may be physically close to the other object")]
		public SharedBool lineOfSight;

		// Token: 0x040003F6 RID: 1014
		[Tooltip("The LayerMask of the objects to ignore when performing the line of sight check")]
		public LayerMask ignoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");

		// Token: 0x040003F7 RID: 1015
		[Tooltip("The raycast offset relative to the pivot position")]
		public SharedVector3 offset;

		// Token: 0x040003F8 RID: 1016
		[Tooltip("The target raycast offset relative to the pivot position")]
		public SharedVector3 targetOffset;

		// Token: 0x040003F9 RID: 1017
		[Tooltip("The object variable that will be set when a object is found what the object is")]
		public SharedGameObject returnedObject;

		// Token: 0x040003FA RID: 1018
		private List<GameObject> objects;

		// Token: 0x040003FB RID: 1019
		private float sqrMagnitude;
	}
}
