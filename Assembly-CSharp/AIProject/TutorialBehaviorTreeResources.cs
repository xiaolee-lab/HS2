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
	// Token: 0x02000E25 RID: 3621
	public class TutorialBehaviorTreeResources : SerializedMonoBehaviour
	{
		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x00304EEF File Offset: 0x003032EF
		public AgentBehaviorTree Current
		{
			[CompilerGenerated]
			get
			{
				return this._current;
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x0600713A RID: 28986 RVA: 0x00304EF7 File Offset: 0x003032F7
		// (set) Token: 0x0600713B RID: 28987 RVA: 0x00304EFF File Offset: 0x003032FF
		public Tutorial.ActionType Mode { get; private set; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x0600713C RID: 28988 RVA: 0x00304F08 File Offset: 0x00303308
		// (set) Token: 0x0600713D RID: 28989 RVA: 0x00304F10 File Offset: 0x00303310
		public AgentActor SourceAgent { get; set; }

		// Token: 0x0600713E RID: 28990 RVA: 0x00304F1C File Offset: 0x0030331C
		public AgentBehaviorTree GetBehaviorTree(Tutorial.ActionType mode)
		{
			return null;
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x00304F2C File Offset: 0x0030332C
		public void Initialize(AgentActor actor)
		{
			this.SourceAgent = actor;
			this._behaviorTreeTable.Clear();
			IEnumerator enumerator = Enum.GetValues(typeof(Tutorial.ActionType)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Tutorial.ActionType actionType = (Tutorial.ActionType)obj;
					AgentBehaviorTree tutorialBehavior = Singleton<Manager.Resources>.Instance.BehaviorTree.GetTutorialBehavior(actionType);
					if (!(tutorialBehavior == null))
					{
						AgentBehaviorTree agentBehaviorTree = UnityEngine.Object.Instantiate<AgentBehaviorTree>(tutorialBehavior);
						agentBehaviorTree.transform.SetParent(base.transform, false);
						agentBehaviorTree.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
						this._behaviorTreeTable[actionType] = agentBehaviorTree;
						agentBehaviorTree.SourceAgent = actor;
						agentBehaviorTree.StartWhenEnabled = false;
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
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x00305014 File Offset: 0x00303414
		public void ChangeMode(Tutorial.ActionType mode)
		{
			AgentBehaviorTree agentBehaviorTree;
			if (!this._behaviorTreeTable.TryGetValue(mode, out agentBehaviorTree) || agentBehaviorTree == null || this._current == agentBehaviorTree)
			{
				return;
			}
			this.Mode = mode;
			if (this._current != null)
			{
				this._current.DisableBehavior(false);
			}
			this._current = agentBehaviorTree;
			if (this._nextDisposable != null)
			{
				this._nextDisposable.Dispose();
			}
			this._nextDisposable = Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				this.Current.EnableBehavior();
			});
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x003050AE File Offset: 0x003034AE
		private void OnEnable()
		{
			if (this._current != null)
			{
				this._current.EnableBehavior();
			}
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x003050C8 File Offset: 0x003034C8
		private void OnDisable()
		{
			if (this._current != null)
			{
				this._current.DisableBehavior(true);
			}
		}

		// Token: 0x04005CEF RID: 23791
		private AgentBehaviorTree _current;

		// Token: 0x04005CF2 RID: 23794
		private Dictionary<Tutorial.ActionType, AgentBehaviorTree> _behaviorTreeTable = new Dictionary<Tutorial.ActionType, AgentBehaviorTree>();

		// Token: 0x04005CF3 RID: 23795
		private IDisposable _nextDisposable;
	}
}
