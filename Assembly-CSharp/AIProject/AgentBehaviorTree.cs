using System;
using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C7C RID: 3196
	public class AgentBehaviorTree : Behavior
	{
		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x060068CA RID: 26826 RVA: 0x002C9CA5 File Offset: 0x002C80A5
		// (set) Token: 0x060068CB RID: 26827 RVA: 0x002C9CAD File Offset: 0x002C80AD
		public AgentActor SourceAgent
		{
			get
			{
				return this._sourceAgent;
			}
			set
			{
				this._sourceAgent = value;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x060068CC RID: 26828 RVA: 0x002C9CB6 File Offset: 0x002C80B6
		// (set) Token: 0x060068CD RID: 26829 RVA: 0x002C9CC0 File Offset: 0x002C80C0
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
				if (value != null && value.Initialized)
				{
					BehaviorSource behaviorSource = base.GetBehaviorSource();
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
					base.SetBehaviorSource(behaviorSource);
				}
				else
				{
					base.GetBehaviorSource().HasSerialized = false;
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

		// Token: 0x060068CE RID: 26830 RVA: 0x002C9DAD File Offset: 0x002C81AD
		private new void Start()
		{
			if (base.StartWhenEnabled)
			{
				base.StartWhenEnabled = false;
			}
			base.Start();
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x002C9DC8 File Offset: 0x002C81C8
		public new void OnEnable()
		{
			if (BehaviorManager.instance != null && this._isPause)
			{
				BehaviorManager.instance.EnableBehavior(this);
				this._isPause = false;
			}
			else if (base.StartWhenEnabled && this._initialized)
			{
				this.EnableBehavior();
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x002C9E24 File Offset: 0x002C8224
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

		// Token: 0x060068D1 RID: 26833 RVA: 0x002C9E64 File Offset: 0x002C8264
		public new void EnableBehavior()
		{
			AgentBehaviorTree.CreateAIBehaviorManager();
			if (BehaviorManager.instance != null)
			{
				BehaviorManager.instance.EnableBehavior(this);
			}
			BehaviorSource behaviorSource = base.GetBehaviorSource();
			if (!this._initialized)
			{
				for (int i = 0; i < 12; i++)
				{
					bool[] hasEvent = base.HasEvent;
					int num = i;
					Behavior.EventTypes eventTypes = (Behavior.EventTypes)i;
					hasEvent[num] = this.TaskContainsMethod(eventTypes.ToString(), behaviorSource.RootTask);
				}
			}
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x002C9EDA File Offset: 0x002C82DA
		public new void DisableBehavior()
		{
			if (BehaviorManager.instance != null)
			{
				BehaviorManager.instance.DisableBehavior(this, base.PauseWhenDisabled);
				this._isPause = base.PauseWhenDisabled;
			}
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x002C9F09 File Offset: 0x002C8309
		public new void DisableBehavior(bool pause)
		{
			if (BehaviorManager.instance != null)
			{
				BehaviorManager.instance.DisableBehavior(this, pause);
				this._isPause = pause;
			}
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x002C9F30 File Offset: 0x002C8330
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

		// Token: 0x0400596D RID: 22893
		[SerializeField]
		private AgentActor _sourceAgent;

		// Token: 0x0400596E RID: 22894
		private bool _initialized;

		// Token: 0x0400596F RID: 22895
		private bool _isPause;

		// Token: 0x04005970 RID: 22896
		private ExternalBehavior _externalBehavior;
	}
}
