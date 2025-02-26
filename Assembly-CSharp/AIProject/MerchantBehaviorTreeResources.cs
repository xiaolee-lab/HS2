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
	// Token: 0x02000E20 RID: 3616
	public class MerchantBehaviorTreeResources : SerializedMonoBehaviour
	{
		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06007037 RID: 28727 RVA: 0x002FF497 File Offset: 0x002FD897
		public MerchantBehaviorTree CurrentTree
		{
			[CompilerGenerated]
			get
			{
				return this.currentTree;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06007038 RID: 28728 RVA: 0x002FF49F File Offset: 0x002FD89F
		// (set) Token: 0x06007039 RID: 28729 RVA: 0x002FF4A7 File Offset: 0x002FD8A7
		public Merchant.ActionType Mode { get; private set; }

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x0600703A RID: 28730 RVA: 0x002FF4B0 File Offset: 0x002FD8B0
		public MerchantActor SourceMerchant
		{
			[CompilerGenerated]
			get
			{
				return this.sourceMerchant;
			}
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x002FF4B8 File Offset: 0x002FD8B8
		public MerchantBehaviorTree GetBehaviorTree(Merchant.ActionType _mode)
		{
			MerchantBehaviorTree merchantBehaviorTree = null;
			return (!this.behaviorTreeTable.TryGetValue(_mode, out merchantBehaviorTree)) ? null : merchantBehaviorTree;
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x002FF4E4 File Offset: 0x002FD8E4
		public void Initialize()
		{
			if (this.behaviorTreeTable == null)
			{
				this.behaviorTreeTable = new Dictionary<Merchant.ActionType, MerchantBehaviorTree>();
			}
			IEnumerator enumerator = Enum.GetValues(typeof(Merchant.ActionType)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Merchant.ActionType actionType = (Merchant.ActionType)obj;
					MerchantBehaviorTree merchantBehavior = Singleton<Manager.Resources>.Instance.BehaviorTree.GetMerchantBehavior(actionType);
					if (!(merchantBehavior == null))
					{
						MerchantBehaviorTree merchantBehaviorTree = UnityEngine.Object.Instantiate<MerchantBehaviorTree>(merchantBehavior);
						merchantBehaviorTree.transform.SetParent(base.transform, false);
						merchantBehaviorTree.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
						this.behaviorTreeTable[actionType] = merchantBehaviorTree;
						merchantBehaviorTree.SourceMerchant = this.sourceMerchant;
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

		// Token: 0x0600703D RID: 28733 RVA: 0x002FF5CC File Offset: 0x002FD9CC
		public bool IsMatchCurrentTree(Merchant.ActionType _mode)
		{
			MerchantBehaviorTree y = null;
			this.behaviorTreeTable.TryGetValue(_mode, out y);
			return this.currentTree == y;
		}

		// Token: 0x0600703E RID: 28734 RVA: 0x002FF5F8 File Offset: 0x002FD9F8
		public void ChangeMode(Merchant.ActionType _mode)
		{
			MerchantBehaviorTree merchantBehaviorTree = null;
			if (!this.behaviorTreeTable.TryGetValue(_mode, out merchantBehaviorTree) || merchantBehaviorTree == null)
			{
				return;
			}
			this.Mode = _mode;
			if (this.currentTree != null)
			{
				this.currentTree.DisableBehavior(false);
			}
			merchantBehaviorTree.DisableBehavior(false);
			this.currentTree = merchantBehaviorTree;
			(from _ in Observable.NextFrame(FrameCountType.Update)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.currentTree.EnableBehavior();
			});
		}

		// Token: 0x0600703F RID: 28735 RVA: 0x002FF67E File Offset: 0x002FDA7E
		private void OnEnable()
		{
			if (this.currentTree != null)
			{
				this.currentTree.EnableBehavior();
			}
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x002FF698 File Offset: 0x002FDA98
		private void OnDisable()
		{
			if (this.currentTree != null)
			{
				this.currentTree.DisableBehavior(true);
			}
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x002FF6B3 File Offset: 0x002FDAB3
		public void StopBehaviorTree()
		{
			if (this.currentTree != null)
			{
				this.currentTree.DisableBehavior(false);
			}
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x002FF6D4 File Offset: 0x002FDAD4
		[Sirenix.OdinInspector.Button("TreeにSourceMerchantを反映")]
		private void AttachSourceMarchant()
		{
			if (this.behaviorTreeTable.IsNullOrEmpty<Merchant.ActionType, MerchantBehaviorTree>())
			{
				return;
			}
			foreach (MerchantBehaviorTree merchantBehaviorTree in this.behaviorTreeTable.Values)
			{
				if (!(merchantBehaviorTree == null))
				{
					merchantBehaviorTree.SourceMerchant = this.sourceMerchant;
				}
			}
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x002FF75C File Offset: 0x002FDB5C
		[Sirenix.OdinInspector.Button("TreeTableをリフレッシュ")]
		[HideInPlayMode]
		private void RefreshTreeTable()
		{
			if (this.behaviorTreeTable == null)
			{
				this.behaviorTreeTable = new Dictionary<Merchant.ActionType, MerchantBehaviorTree>();
			}
			this.behaviorTreeTable.Clear();
			IEnumerator enumerator = Enum.GetValues(typeof(Merchant.ActionType)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Merchant.ActionType key = (Merchant.ActionType)obj;
					this.behaviorTreeTable[key] = null;
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

		// Token: 0x04005C4D RID: 23629
		[SerializeField]
		[HideInInspector]
		private MerchantBehaviorTree currentTree;

		// Token: 0x04005C4F RID: 23631
		[SerializeField]
		private MerchantActor sourceMerchant;

		// Token: 0x04005C50 RID: 23632
		[SerializeField]
		private Dictionary<Merchant.ActionType, MerchantBehaviorTree> behaviorTreeTable = new Dictionary<Merchant.ActionType, MerchantBehaviorTree>();
	}
}
