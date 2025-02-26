using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C78 RID: 3192
	public class BehaviorTreeResources : SerializedMonoBehaviour
	{
		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06006892 RID: 26770 RVA: 0x002C9242 File Offset: 0x002C7642
		public AgentBehaviorTree Current
		{
			[CompilerGenerated]
			get
			{
				return this._current;
			}
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06006893 RID: 26771 RVA: 0x002C924A File Offset: 0x002C764A
		// (set) Token: 0x06006894 RID: 26772 RVA: 0x002C9252 File Offset: 0x002C7652
		public Desire.ActionType Mode { get; private set; }

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06006895 RID: 26773 RVA: 0x002C925B File Offset: 0x002C765B
		public AgentActor SourceAgent
		{
			[CompilerGenerated]
			get
			{
				return this._sourceAgent;
			}
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x002C9264 File Offset: 0x002C7664
		public AgentBehaviorTree GetBehaviorTree(Desire.ActionType mode)
		{
			AgentBehaviorTree result;
			if (this._behaviorTreeTable.TryGetValue(mode, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x002C9288 File Offset: 0x002C7688
		public void Initialize()
		{
			this._behaviorTreeTable.Clear();
			IEnumerator enumerator = Enum.GetValues(typeof(Desire.ActionType)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Desire.ActionType actionType = (Desire.ActionType)obj;
					AgentBehaviorTree behavior = Singleton<Manager.Resources>.Instance.BehaviorTree.GetBehavior(actionType);
					if (behavior != null)
					{
						AgentBehaviorTree agentBehaviorTree = UnityEngine.Object.Instantiate<AgentBehaviorTree>(behavior);
						agentBehaviorTree.transform.SetParent(base.transform, false);
						agentBehaviorTree.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
						this._behaviorTreeTable[actionType] = agentBehaviorTree;
						agentBehaviorTree.SourceAgent = this._sourceAgent;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			foreach (AgentBehaviorTree agentBehaviorTree2 in this._behaviorTreeTable.Values)
			{
				agentBehaviorTree2.StartWhenEnabled = false;
			}
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x002C93B4 File Offset: 0x002C77B4
		public void ChangeMode(Desire.ActionType mode)
		{
			AgentBehaviorTree agentBehaviorTree;
			if (!this._behaviorTreeTable.TryGetValue(mode, out agentBehaviorTree))
			{
				return;
			}
			if (this.Current == agentBehaviorTree)
			{
				return;
			}
			this.Mode = mode;
			AgentBehaviorTree agentBehaviorTree2 = this.Current;
			if (agentBehaviorTree2 != null)
			{
				agentBehaviorTree2.DisableBehavior(false);
			}
			this._current = agentBehaviorTree;
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				this.Current.EnableBehavior();
			});
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x002C9422 File Offset: 0x002C7822
		private void OnEnable()
		{
			AgentBehaviorTree agentBehaviorTree = this.Current;
			if (agentBehaviorTree != null)
			{
				agentBehaviorTree.EnableBehavior();
			}
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x002C9438 File Offset: 0x002C7838
		private void OnDisable()
		{
			AgentBehaviorTree agentBehaviorTree = this.Current;
			if (agentBehaviorTree != null)
			{
				agentBehaviorTree.DisableBehavior(true);
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x002C9450 File Offset: 0x002C7850
		public void DisableAllBehaviors()
		{
			foreach (KeyValuePair<Desire.ActionType, AgentBehaviorTree> keyValuePair in this._behaviorTreeTable)
			{
				if (!(keyValuePair.Value == null))
				{
					keyValuePair.Value.DisableBehavior();
				}
			}
		}

		// Token: 0x04005958 RID: 22872
		[SerializeField]
		[HideInInspector]
		private AgentBehaviorTree _current;

		// Token: 0x0400595A RID: 22874
		[SerializeField]
		private AgentActor _sourceAgent;

		// Token: 0x0400595B RID: 22875
		[SerializeField]
		private Dictionary<Desire.ActionType, AgentBehaviorTree> _behaviorTreeTable = new Dictionary<Desire.ActionType, AgentBehaviorTree>();
	}
}
