using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000264 RID: 612
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedGameObjectList values from the GameObjects. Returns Success.")]
	public class SharedGameObjectsToGameObjectList : Action
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002BBE4 File Offset: 0x00029FE4
		public override void OnAwake()
		{
			this.storedGameObjectList.Value = new List<GameObject>();
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002BBF8 File Offset: 0x00029FF8
		public override TaskStatus OnUpdate()
		{
			if (this.gameObjects == null || this.gameObjects.Length == 0)
			{
				return TaskStatus.Failure;
			}
			this.storedGameObjectList.Value.Clear();
			for (int i = 0; i < this.gameObjects.Length; i++)
			{
				this.storedGameObjectList.Value.Add(this.gameObjects[i].Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002BC66 File Offset: 0x0002A066
		public override void OnReset()
		{
			this.gameObjects = null;
			this.storedGameObjectList = null;
		}

		// Token: 0x040009A4 RID: 2468
		[Tooltip("The GameObjects value")]
		public SharedGameObject[] gameObjects;

		// Token: 0x040009A5 RID: 2469
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedGameObjectList storedGameObjectList;
	}
}
