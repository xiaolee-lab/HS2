using System;
using System.Linq;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012F0 RID: 4848
	public class ManipulatePanelCtrl : MonoBehaviour
	{
		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x0600A1DE RID: 41438 RVA: 0x004268C8 File Offset: 0x00424CC8
		// (set) Token: 0x0600A1DF RID: 41439 RVA: 0x004268D5 File Offset: 0x00424CD5
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				base.gameObject.SetActive(value);
				if (base.gameObject.activeSelf)
				{
					this.SetActive();
				}
				else
				{
					this.Deactivate();
				}
			}
		}

		// Token: 0x17002219 RID: 8729
		// (get) Token: 0x0600A1E0 RID: 41440 RVA: 0x00426904 File Offset: 0x00424D04
		// (set) Token: 0x0600A1E1 RID: 41441 RVA: 0x0042690C File Offset: 0x00424D0C
		private ManipulatePanelCtrl.RootInfo[] rootPanel { get; set; }

		// Token: 0x1700221A RID: 8730
		// (set) Token: 0x0600A1E2 RID: 41442 RVA: 0x00426918 File Offset: 0x00424D18
		public ObjectCtrlInfo objectCtrlInfo
		{
			set
			{
				int[] array;
				if (value != null)
				{
					array = value.kinds;
				}
				else
				{
					(array = new int[1])[0] = -1;
				}
				this.kinds = array;
				this.charaPanelInfo.mpCharCtrl.ociChar = (value as OCIChar);
				this.itemPanelInfo.mpItemCtrl.ociItem = (value as OCIItem);
				this.lightPanelInfo.mpLightCtrl.ociLight = (value as OCILight);
				this.folderPanelInfo.mpFolderCtrl.ociFolder = (value as OCIFolder);
				this.routePanelInfo.mpRouteCtrl.ociRoute = (value as OCIRoute);
				this.cameraPanelInfo.mpCameraCtrl.ociCamera = (value as OCICamera);
				this.routePointPanelInfo.mpRoutePointCtrl.ociRoutePoint = (value as OCIRoutePoint);
			}
		}

		// Token: 0x0600A1E3 RID: 41443 RVA: 0x004269E0 File Offset: 0x00424DE0
		public void OnSelect(TreeNodeObject _node)
		{
			Singleton<Studio>.Instance.colorPalette.visible = false;
			ObjectCtrlInfo objectCtrlInfo = this.TryGetLoop(_node);
			int[] array;
			if (objectCtrlInfo != null)
			{
				array = objectCtrlInfo.kinds;
			}
			else
			{
				(array = new int[1])[0] = -1;
			}
			this.kinds = array;
			for (int i = 0; i < this.kinds.Length; i++)
			{
				switch (this.kinds[i])
				{
				case 0:
					this.charaPanelInfo.mpCharCtrl.ociChar = (objectCtrlInfo[i] as OCIChar);
					break;
				case 1:
					this.itemPanelInfo.mpItemCtrl.ociItem = (objectCtrlInfo[i] as OCIItem);
					break;
				case 2:
					this.lightPanelInfo.mpLightCtrl.ociLight = (objectCtrlInfo[i] as OCILight);
					break;
				case 3:
					this.folderPanelInfo.mpFolderCtrl.ociFolder = (objectCtrlInfo[i] as OCIFolder);
					break;
				case 4:
					this.routePanelInfo.mpRouteCtrl.ociRoute = (objectCtrlInfo[i] as OCIRoute);
					break;
				case 5:
					this.cameraPanelInfo.mpCameraCtrl.ociCamera = (objectCtrlInfo[i] as OCICamera);
					break;
				case 6:
					this.routePointPanelInfo.mpRoutePointCtrl.ociRoutePoint = (objectCtrlInfo[i] as OCIRoutePoint);
					break;
				}
			}
			if (this.active)
			{
				this.SetActive();
			}
		}

		// Token: 0x0600A1E4 RID: 41444 RVA: 0x00426B6C File Offset: 0x00424F6C
		public void OnDeselect(TreeNodeObject _node)
		{
			ObjectCtrlInfo objectCtrlInfo = this.TryGetLoop(_node);
			switch ((objectCtrlInfo != null) ? objectCtrlInfo.kind : -1)
			{
			case 0:
				this.charaPanelInfo.mpCharCtrl.Deselect(objectCtrlInfo as OCIChar);
				break;
			case 1:
				this.itemPanelInfo.mpItemCtrl.Deselect(objectCtrlInfo as OCIItem);
				break;
			case 2:
				this.lightPanelInfo.mpLightCtrl.Deselect(objectCtrlInfo as OCILight);
				break;
			case 3:
				this.folderPanelInfo.mpFolderCtrl.Deselect(objectCtrlInfo as OCIFolder);
				break;
			case 4:
				this.routePanelInfo.mpRouteCtrl.Deselect(objectCtrlInfo as OCIRoute);
				break;
			case 5:
				this.cameraPanelInfo.mpCameraCtrl.Deselect(objectCtrlInfo as OCICamera);
				break;
			case 6:
				this.routePointPanelInfo.mpRoutePointCtrl.Deselect(objectCtrlInfo as OCIRoutePoint);
				break;
			}
		}

		// Token: 0x0600A1E5 RID: 41445 RVA: 0x00426C84 File Offset: 0x00425084
		public void UpdateInfo(int _kind)
		{
			if (_kind != 1)
			{
				if (_kind == 5)
				{
					this.cameraPanelInfo.mpCameraCtrl.UpdateInfo();
				}
			}
			else
			{
				this.itemPanelInfo.mpItemCtrl.UpdateInfo();
			}
		}

		// Token: 0x0600A1E6 RID: 41446 RVA: 0x00426CC4 File Offset: 0x004250C4
		private void SetActive()
		{
			for (int i = 0; i < this.rootPanel.Length; i++)
			{
				this.rootPanel[i].active = this.kinds.Contains(i);
			}
		}

		// Token: 0x0600A1E7 RID: 41447 RVA: 0x00426D04 File Offset: 0x00425104
		private void Deactivate()
		{
			for (int i = 0; i < this.rootPanel.Length; i++)
			{
				this.rootPanel[i].active = false;
			}
		}

		// Token: 0x0600A1E8 RID: 41448 RVA: 0x00426D38 File Offset: 0x00425138
		public void OnDelete(TreeNodeObject _node)
		{
			this.kinds = new int[]
			{
				-1
			};
			this.SetActive();
		}

		// Token: 0x0600A1E9 RID: 41449 RVA: 0x00426D50 File Offset: 0x00425150
		private ObjectCtrlInfo TryGetLoop(TreeNodeObject _node)
		{
			if (_node == null)
			{
				return null;
			}
			ObjectCtrlInfo result = null;
			if (Singleton<Studio>.Instance.dicInfo.TryGetValue(_node, out result))
			{
				return result;
			}
			return this.TryGetLoop(_node.parent);
		}

		// Token: 0x0600A1EA RID: 41450 RVA: 0x00426D94 File Offset: 0x00425194
		private void Awake()
		{
			this.rootPanel = new ManipulatePanelCtrl.RootInfo[]
			{
				this.charaPanelInfo,
				this.itemPanelInfo,
				this.lightPanelInfo,
				this.folderPanelInfo,
				this.routePanelInfo,
				this.cameraPanelInfo,
				this.routePointPanelInfo
			};
			this.kinds = new int[]
			{
				-1
			};
			this.SetActive();
			TreeNodeCtrl treeNodeCtrl = Singleton<Studio>.Instance.treeNodeCtrl;
			treeNodeCtrl.onSelect = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl.onSelect, new Action<TreeNodeObject>(this.OnSelect));
			TreeNodeCtrl treeNodeCtrl2 = Singleton<Studio>.Instance.treeNodeCtrl;
			treeNodeCtrl2.onDelete = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl2.onDelete, new Action<TreeNodeObject>(this.OnDelete));
			TreeNodeCtrl treeNodeCtrl3 = Singleton<Studio>.Instance.treeNodeCtrl;
			treeNodeCtrl3.onDeselect = (Action<TreeNodeObject>)Delegate.Combine(treeNodeCtrl3.onDeselect, new Action<TreeNodeObject>(this.OnDeselect));
		}

		// Token: 0x04007FFD RID: 32765
		[SerializeField]
		private ManipulatePanelCtrl.CharaPanelInfo charaPanelInfo = new ManipulatePanelCtrl.CharaPanelInfo();

		// Token: 0x04007FFE RID: 32766
		[SerializeField]
		private ManipulatePanelCtrl.ItemPanelInfo itemPanelInfo = new ManipulatePanelCtrl.ItemPanelInfo();

		// Token: 0x04007FFF RID: 32767
		[SerializeField]
		private ManipulatePanelCtrl.LightPanelInfo lightPanelInfo = new ManipulatePanelCtrl.LightPanelInfo();

		// Token: 0x04008000 RID: 32768
		[SerializeField]
		private ManipulatePanelCtrl.FolderPanelInfo folderPanelInfo = new ManipulatePanelCtrl.FolderPanelInfo();

		// Token: 0x04008001 RID: 32769
		[SerializeField]
		private ManipulatePanelCtrl.RoutePanelInfo routePanelInfo = new ManipulatePanelCtrl.RoutePanelInfo();

		// Token: 0x04008002 RID: 32770
		[SerializeField]
		private ManipulatePanelCtrl.CameraPanelInfo cameraPanelInfo = new ManipulatePanelCtrl.CameraPanelInfo();

		// Token: 0x04008003 RID: 32771
		[SerializeField]
		private ManipulatePanelCtrl.RoutePointPanelInfo routePointPanelInfo = new ManipulatePanelCtrl.RoutePointPanelInfo();

		// Token: 0x04008005 RID: 32773
		private int[] kinds = new int[]
		{
			-1
		};

		// Token: 0x020012F1 RID: 4849
		[Serializable]
		private class RootInfo
		{
			// Token: 0x1700221B RID: 8731
			// (set) Token: 0x0600A1EC RID: 41452 RVA: 0x00426E8B File Offset: 0x0042528B
			public virtual bool active
			{
				set
				{
					if (this.root.activeSelf != value)
					{
						this.root.SetActive(value);
					}
				}
			}

			// Token: 0x04008006 RID: 32774
			public GameObject root;
		}

		// Token: 0x020012F2 RID: 4850
		[Serializable]
		private class CharaPanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x1700221C RID: 8732
			// (get) Token: 0x0600A1EE RID: 41454 RVA: 0x00426EB2 File Offset: 0x004252B2
			public MPCharCtrl mpCharCtrl
			{
				get
				{
					if (this.m_MPCharCtrl == null)
					{
						this.m_MPCharCtrl = this.root.GetComponent<MPCharCtrl>();
					}
					return this.m_MPCharCtrl;
				}
			}

			// Token: 0x1700221D RID: 8733
			// (set) Token: 0x0600A1EF RID: 41455 RVA: 0x00426EDC File Offset: 0x004252DC
			public override bool active
			{
				set
				{
					this.mpCharCtrl.active = value;
				}
			}

			// Token: 0x04008007 RID: 32775
			private MPCharCtrl m_MPCharCtrl;
		}

		// Token: 0x020012F3 RID: 4851
		[Serializable]
		private class ItemPanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x1700221E RID: 8734
			// (get) Token: 0x0600A1F1 RID: 41457 RVA: 0x00426EF2 File Offset: 0x004252F2
			public MPItemCtrl mpItemCtrl
			{
				get
				{
					if (this.m_MPItemCtrl == null)
					{
						this.m_MPItemCtrl = this.root.GetComponent<MPItemCtrl>();
					}
					return this.m_MPItemCtrl;
				}
			}

			// Token: 0x1700221F RID: 8735
			// (set) Token: 0x0600A1F2 RID: 41458 RVA: 0x00426F1C File Offset: 0x0042531C
			public override bool active
			{
				set
				{
					this.mpItemCtrl.active = value;
				}
			}

			// Token: 0x04008008 RID: 32776
			private MPItemCtrl m_MPItemCtrl;
		}

		// Token: 0x020012F4 RID: 4852
		[Serializable]
		private class LightPanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x17002220 RID: 8736
			// (get) Token: 0x0600A1F4 RID: 41460 RVA: 0x00426F32 File Offset: 0x00425332
			public MPLightCtrl mpLightCtrl
			{
				get
				{
					if (this.m_MPLightCtrl == null)
					{
						this.m_MPLightCtrl = this.root.GetComponent<MPLightCtrl>();
					}
					return this.m_MPLightCtrl;
				}
			}

			// Token: 0x17002221 RID: 8737
			// (set) Token: 0x0600A1F5 RID: 41461 RVA: 0x00426F5C File Offset: 0x0042535C
			public override bool active
			{
				set
				{
					this.mpLightCtrl.active = value;
				}
			}

			// Token: 0x04008009 RID: 32777
			private MPLightCtrl m_MPLightCtrl;
		}

		// Token: 0x020012F5 RID: 4853
		[Serializable]
		private class FolderPanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x17002222 RID: 8738
			// (get) Token: 0x0600A1F7 RID: 41463 RVA: 0x00426F72 File Offset: 0x00425372
			public MPFolderCtrl mpFolderCtrl
			{
				get
				{
					if (this.m_MPFolderCtrl == null)
					{
						this.m_MPFolderCtrl = this.root.GetComponent<MPFolderCtrl>();
					}
					return this.m_MPFolderCtrl;
				}
			}

			// Token: 0x17002223 RID: 8739
			// (set) Token: 0x0600A1F8 RID: 41464 RVA: 0x00426F9C File Offset: 0x0042539C
			public override bool active
			{
				set
				{
					this.mpFolderCtrl.active = value;
				}
			}

			// Token: 0x0400800A RID: 32778
			private MPFolderCtrl m_MPFolderCtrl;
		}

		// Token: 0x020012F6 RID: 4854
		[Serializable]
		private class RoutePanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x17002224 RID: 8740
			// (get) Token: 0x0600A1FA RID: 41466 RVA: 0x00426FB2 File Offset: 0x004253B2
			public MPRouteCtrl mpRouteCtrl
			{
				get
				{
					if (this.m_MPRouteCtrl == null)
					{
						this.m_MPRouteCtrl = this.root.GetComponent<MPRouteCtrl>();
					}
					return this.m_MPRouteCtrl;
				}
			}

			// Token: 0x17002225 RID: 8741
			// (set) Token: 0x0600A1FB RID: 41467 RVA: 0x00426FDC File Offset: 0x004253DC
			public override bool active
			{
				set
				{
					this.mpRouteCtrl.active = value;
				}
			}

			// Token: 0x0400800B RID: 32779
			private MPRouteCtrl m_MPRouteCtrl;
		}

		// Token: 0x020012F7 RID: 4855
		[Serializable]
		private class CameraPanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x17002226 RID: 8742
			// (get) Token: 0x0600A1FD RID: 41469 RVA: 0x00426FF2 File Offset: 0x004253F2
			public MPCameraCtrl mpCameraCtrl
			{
				get
				{
					if (this.m_MPCameraCtrl == null)
					{
						this.m_MPCameraCtrl = this.root.GetComponent<MPCameraCtrl>();
					}
					return this.m_MPCameraCtrl;
				}
			}

			// Token: 0x17002227 RID: 8743
			// (set) Token: 0x0600A1FE RID: 41470 RVA: 0x0042701C File Offset: 0x0042541C
			public override bool active
			{
				set
				{
					this.mpCameraCtrl.active = value;
				}
			}

			// Token: 0x0400800C RID: 32780
			private MPCameraCtrl m_MPCameraCtrl;
		}

		// Token: 0x020012F8 RID: 4856
		[Serializable]
		private class RoutePointPanelInfo : ManipulatePanelCtrl.RootInfo
		{
			// Token: 0x17002228 RID: 8744
			// (get) Token: 0x0600A200 RID: 41472 RVA: 0x00427032 File Offset: 0x00425432
			public MPRoutePointCtrl mpRoutePointCtrl
			{
				get
				{
					if (this.m_MPRoutePointCtrl == null)
					{
						this.m_MPRoutePointCtrl = this.root.GetComponent<MPRoutePointCtrl>();
					}
					return this.m_MPRoutePointCtrl;
				}
			}

			// Token: 0x17002229 RID: 8745
			// (set) Token: 0x0600A201 RID: 41473 RVA: 0x0042705C File Offset: 0x0042545C
			public override bool active
			{
				set
				{
					this.mpRoutePointCtrl.active = value;
				}
			}

			// Token: 0x0400800D RID: 32781
			private MPRoutePointCtrl m_MPRoutePointCtrl;
		}
	}
}
