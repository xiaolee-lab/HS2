using System;
using ConfigScene;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200128B RID: 4747
	public class OptionSystem : BaseSystem
	{
		// Token: 0x06009D22 RID: 40226 RVA: 0x00403C40 File Offset: 0x00402040
		public OptionSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06009D23 RID: 40227 RVA: 0x00403D03 File Offset: 0x00402103
		// (set) Token: 0x06009D24 RID: 40228 RVA: 0x00403D13 File Offset: 0x00402113
		public int logo
		{
			get
			{
				return Mathf.Clamp(this._logo, 0, 9);
			}
			set
			{
				this._logo = Mathf.Clamp(value, 0, 9);
			}
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06009D25 RID: 40229 RVA: 0x00403D24 File Offset: 0x00402124
		// (set) Token: 0x06009D26 RID: 40230 RVA: 0x00403D32 File Offset: 0x00402132
		public float routeLineWidth
		{
			get
			{
				return this._routeLineWidth * 16f;
			}
			set
			{
				this._routeLineWidth = value / 16f;
			}
		}

		// Token: 0x06009D27 RID: 40231 RVA: 0x00403D44 File Offset: 0x00402144
		public override void Init()
		{
			this.cameraSpeedX = 1f;
			this.cameraSpeedY = 1f;
			this.cameraSpeed = 1f;
			this.manipulateSize = 1f;
			this.manipuleteSpeed = 1f;
			this.initialPosition = 0;
			this.selectedState = 0;
			this.autoHide = true;
			this.autoSelect = false;
			this.snap = 0;
			this.colorFKHair = Color.white;
			this.colorFKNeck = Color.white;
			this.colorFKBreast = Color.white;
			this.colorFKBody = Color.white;
			this.colorFKRightHand = Color.white;
			this.colorFKLeftHand = Color.white;
			this.colorFKSkirt = Color.white;
			this.lineFK = true;
			this.colorFKItem = Color.white;
			this._logo = 0;
			this._routeLineWidth = 1f;
			this.routePointLimit = true;
			this.startupLoad = false;
		}

		// Token: 0x06009D28 RID: 40232 RVA: 0x00403E2C File Offset: 0x0040222C
		public Color GetFKColor(int _idx)
		{
			switch (_idx)
			{
			case 0:
				return this.colorFKHair;
			case 1:
				return this.colorFKNeck;
			case 2:
				return this.colorFKBreast;
			case 3:
				return this.colorFKBody;
			case 4:
				return this.colorFKRightHand;
			case 5:
				return this.colorFKLeftHand;
			case 6:
				return this.colorFKSkirt;
			default:
				return Color.white;
			}
		}

		// Token: 0x06009D29 RID: 40233 RVA: 0x00403E98 File Offset: 0x00402298
		public void SetFKColor(int _idx, Color _color)
		{
			switch (_idx)
			{
			case 0:
				this.colorFKHair = _color;
				break;
			case 1:
				this.colorFKNeck = _color;
				break;
			case 2:
				this.colorFKBreast = _color;
				break;
			case 3:
				this.colorFKBody = _color;
				break;
			case 4:
				this.colorFKRightHand = _color;
				break;
			case 5:
				this.colorFKLeftHand = _color;
				break;
			case 6:
				this.colorFKSkirt = _color;
				break;
			}
		}

		// Token: 0x04007CFA RID: 31994
		public float cameraSpeedX = 1f;

		// Token: 0x04007CFB RID: 31995
		public float cameraSpeedY = 1f;

		// Token: 0x04007CFC RID: 31996
		public float cameraSpeed = 1f;

		// Token: 0x04007CFD RID: 31997
		public float manipulateSize = 1f;

		// Token: 0x04007CFE RID: 31998
		public float manipuleteSpeed = 1f;

		// Token: 0x04007CFF RID: 31999
		public int initialPosition;

		// Token: 0x04007D00 RID: 32000
		public int selectedState;

		// Token: 0x04007D01 RID: 32001
		public bool autoHide = true;

		// Token: 0x04007D02 RID: 32002
		public bool autoSelect;

		// Token: 0x04007D03 RID: 32003
		public int snap;

		// Token: 0x04007D04 RID: 32004
		public Color colorFKHair = Color.white;

		// Token: 0x04007D05 RID: 32005
		public Color colorFKNeck = Color.white;

		// Token: 0x04007D06 RID: 32006
		public Color colorFKBreast = Color.white;

		// Token: 0x04007D07 RID: 32007
		public Color colorFKBody = Color.white;

		// Token: 0x04007D08 RID: 32008
		public Color colorFKRightHand = Color.white;

		// Token: 0x04007D09 RID: 32009
		public Color colorFKLeftHand = Color.white;

		// Token: 0x04007D0A RID: 32010
		public Color colorFKSkirt = Color.white;

		// Token: 0x04007D0B RID: 32011
		public bool lineFK = true;

		// Token: 0x04007D0C RID: 32012
		public Color colorFKItem = Color.white;

		// Token: 0x04007D0D RID: 32013
		public int _logo;

		// Token: 0x04007D0E RID: 32014
		public float _routeLineWidth = 1f;

		// Token: 0x04007D0F RID: 32015
		public bool routePointLimit = true;

		// Token: 0x04007D10 RID: 32016
		public bool startupLoad;
	}
}
