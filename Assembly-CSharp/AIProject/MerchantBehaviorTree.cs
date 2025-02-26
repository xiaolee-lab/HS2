using System;
using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000DD9 RID: 3545
	public class MerchantBehaviorTree : Behavior
	{
		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06006DA3 RID: 28067 RVA: 0x002EAE61 File Offset: 0x002E9261
		// (set) Token: 0x06006DA4 RID: 28068 RVA: 0x002EAE69 File Offset: 0x002E9269
		public MerchantActor SourceMerchant
		{
			get
			{
				return this._sourceMerchant;
			}
			set
			{
				this._sourceMerchant = value;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06006DA5 RID: 28069 RVA: 0x002EAE72 File Offset: 0x002E9272
		// (set) Token: 0x06006DA6 RID: 28070 RVA: 0x002EAE7C File Offset: 0x002E927C
		public new ExternalBehavior ExternalBehavior
		{
			get
			{
				return this._externalBehavior;
			}
			set
			{
				if (this._externalBehavior == value)
				{
					return;
				}
				if (BehaviorManager.instance != null)
				{
					BehaviorManager.instance.DisableBehavior(this);
				}
				BehaviorSource behaviorSource = base.GetBehaviorSource();
				if (value != null && value.Initialized)
				{
					List<SharedVariable> allVariables = behaviorSource.GetAllVariables();
					behaviorSource = value.BehaviorSource;
					behaviorSource.HasSerialized = true;
					if (allVariables != null)
					{
						for (int i = 0; i < allVariables.Count; i++)
						{
							if (allVariables[i] != null)
							{
								behaviorSource.SetVariable(allVariables[i].Name, allVariables[i]);
							}
						}
					}
				}
				else
				{
					behaviorSource.HasSerialized = false;
					base.HasInheritedVariables = false;
				}
				this._initialized = false;
				this._externalBehavior = value;
				if (base.StartWhenEnabled)
				{
					this.EnableBehavior();
				}
			}
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x002EAF5D File Offset: 0x002E935D
		private new void Start()
		{
			if (base.StartWhenEnabled)
			{
				base.StartWhenEnabled = false;
			}
			base.Start();
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x002EAF78 File Offset: 0x002E9378
		public new void OnEnable()
		{
			if (BehaviorManager.instance != null && this._isPaused)
			{
				BehaviorManager.instance.EnableBehavior(this);
				this._isPaused = false;
			}
			else if (base.StartWhenEnabled && this._initialized)
			{
				this.EnableBehavior();
			}
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x002EAFD4 File Offset: 0x002E93D4
		public static AIBehaviorManager CreateAIBehaviorManager()
		{
			if (BehaviorManager.instance == null && Application.isPlaying)
			{
				return new GameObject
				{
					name = "Behavior Manager"
				}.AddComponent<AIBehaviorManager>();
			}
			return null;
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x002EB014 File Offset: 0x002E9414
		public new void EnableBehavior()
		{
			MerchantBehaviorTree.CreateAIBehaviorManager();
			if (BehaviorManager.instance != null)
			{
				BehaviorManager.instance.EnableBehavior(this);
			}
			if (!this._initialized)
			{
				BehaviorSource behaviorSource = base.GetBehaviorSource();
				for (int i = 0; i < 12; i++)
				{
					bool[] hasEvent = base.HasEvent;
					int num = i;
					Behavior.EventTypes eventTypes = (Behavior.EventTypes)i;
					hasEvent[num] = this.TaskContainsMethod(eventTypes.ToString(), behaviorSource.RootTask);
				}
				this._initialized = true;
			}
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x002EB094 File Offset: 0x002E9494
		private bool TaskContainsMethod(string methodName, Task task)
		{
			if (task == null)
			{
				return false;
			}
			MethodInfo method = task.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null && method.DeclaringType.IsAssignableFrom(task.GetType()))
			{
				return true;
			}
			if (task is ParentTask)
			{
				ParentTask parentTask = task as ParentTask;
				if (parentTask.Children != null)
				{
					for (int i = 0; i < parentTask.Children.Count; i++)
					{
						if (this.TaskContainsMethod(methodName, parentTask.Children[i]))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04005B49 RID: 23369
		[SerializeField]
		[UnityEngine.Tooltip("行動管理する商人")]
		private MerchantActor _sourceMerchant;

		// Token: 0x04005B4A RID: 23370
		private ExternalBehavior _externalBehavior;

		// Token: 0x04005B4B RID: 23371
		private bool _initialized;

		// Token: 0x04005B4C RID: 23372
		private bool _isPaused;
	}
}
