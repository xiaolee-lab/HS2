using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001219 RID: 4633
	public class OCILight : ObjectCtrlInfo
	{
		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x06009856 RID: 38998 RVA: 0x003ED605 File Offset: 0x003EBA05
		public OILightInfo lightInfo
		{
			get
			{
				return this.objectInfo as OILightInfo;
			}
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x06009857 RID: 38999 RVA: 0x003ED612 File Offset: 0x003EBA12
		public Light light
		{
			get
			{
				if (this.m_Light == null)
				{
					this.m_Light = this.objectLight.GetComponentInChildren<Light>();
				}
				return this.m_Light;
			}
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x06009858 RID: 39000 RVA: 0x003ED63C File Offset: 0x003EBA3C
		public LightType lightType
		{
			get
			{
				return (!(this.light != null)) ? LightType.Directional : this.light.type;
			}
		}

		// Token: 0x06009859 RID: 39001 RVA: 0x003ED660 File Offset: 0x003EBA60
		public void SetColor(Color _color)
		{
			this.lightInfo.color = _color;
			this.light.color = this.lightInfo.color;
			if (this.lightColor)
			{
				this.lightColor.color = this.lightInfo.color;
			}
		}

		// Token: 0x0600985A RID: 39002 RVA: 0x003ED6B8 File Offset: 0x003EBAB8
		public bool SetIntensity(float _value, bool _force = false)
		{
			if (!Utility.SetStruct<float>(ref this.lightInfo.intensity, _value) && !_force)
			{
				return false;
			}
			if (this.light)
			{
				this.light.intensity = this.lightInfo.intensity;
			}
			return true;
		}

		// Token: 0x0600985B RID: 39003 RVA: 0x003ED70C File Offset: 0x003EBB0C
		public bool SetRange(float _value, bool _force = false)
		{
			if (!Utility.SetStruct<float>(ref this.lightInfo.range, _value) && !_force)
			{
				return false;
			}
			if (this.light)
			{
				this.light.range = this.lightInfo.range;
			}
			return true;
		}

		// Token: 0x0600985C RID: 39004 RVA: 0x003ED760 File Offset: 0x003EBB60
		public bool SetSpotAngle(float _value, bool _force = false)
		{
			if (!Utility.SetStruct<float>(ref this.lightInfo.spotAngle, _value) && !_force)
			{
				return false;
			}
			if (this.light)
			{
				this.light.spotAngle = this.lightInfo.spotAngle;
			}
			return true;
		}

		// Token: 0x0600985D RID: 39005 RVA: 0x003ED7B4 File Offset: 0x003EBBB4
		public bool SetEnable(bool _value, bool _force = false)
		{
			if (!Utility.SetStruct<bool>(ref this.lightInfo.enable, _value) && !_force)
			{
				return false;
			}
			if (this.light)
			{
				this.light.enabled = this.lightInfo.enable;
			}
			return true;
		}

		// Token: 0x0600985E RID: 39006 RVA: 0x003ED808 File Offset: 0x003EBC08
		public bool SetDrawTarget(bool _value, bool _force = false)
		{
			if (!Utility.SetStruct<bool>(ref this.lightInfo.drawTarget, _value) && !_force)
			{
				return false;
			}
			Singleton<GuideObjectManager>.Instance.drawLightLine.SetEnable(this.light, this.lightInfo.drawTarget);
			this.guideObject.visible = this.lightInfo.drawTarget;
			return true;
		}

		// Token: 0x0600985F RID: 39007 RVA: 0x003ED86C File Offset: 0x003EBC6C
		public bool SetShadow(bool _value, bool _force = false)
		{
			if (!Utility.SetStruct<bool>(ref this.lightInfo.shadow, _value) && !_force)
			{
				return false;
			}
			if (this.light)
			{
				this.light.shadows = ((!this.lightInfo.shadow) ? LightShadows.None : LightShadows.Soft);
			}
			return true;
		}

		// Token: 0x06009860 RID: 39008 RVA: 0x003ED8CC File Offset: 0x003EBCCC
		public void Update()
		{
			this.SetColor(this.lightInfo.color);
			this.SetIntensity(this.lightInfo.intensity, true);
			this.SetRange(this.lightInfo.range, true);
			this.SetSpotAngle(this.lightInfo.spotAngle, true);
			this.SetEnable(this.lightInfo.enable, true);
			this.SetDrawTarget(this.lightInfo.drawTarget, true);
			this.SetShadow(this.lightInfo.shadow, true);
		}

		// Token: 0x06009861 RID: 39009 RVA: 0x003ED95C File Offset: 0x003EBD5C
		public override void OnDelete()
		{
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			UnityEngine.Object.Destroy(this.objectLight);
			if (this.parentInfo != null)
			{
				this.parentInfo.OnDetachChild(this);
			}
			Studio.DeleteInfo(this.objectInfo, true);
		}

		// Token: 0x06009862 RID: 39010 RVA: 0x003ED9A8 File Offset: 0x003EBDA8
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009863 RID: 39011 RVA: 0x003ED9AA File Offset: 0x003EBDAA
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009864 RID: 39012 RVA: 0x003ED9AC File Offset: 0x003EBDAC
		public override void OnDetach()
		{
			this.parentInfo.OnDetachChild(this);
			this.guideObject.parent = null;
			Studio.AddInfo(this.objectInfo, this);
			this.objectLight.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			this.objectInfo.changeAmount.pos = this.objectLight.transform.localPosition;
			this.objectInfo.changeAmount.rot = this.objectLight.transform.localEulerAngles;
			this.treeNodeObject.ResetVisible();
		}

		// Token: 0x06009865 RID: 39013 RVA: 0x003EDA47 File Offset: 0x003EBE47
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
		}

		// Token: 0x06009866 RID: 39014 RVA: 0x003EDA49 File Offset: 0x003EBE49
		public override void OnSelect(bool _select)
		{
		}

		// Token: 0x06009867 RID: 39015 RVA: 0x003EDA4B File Offset: 0x003EBE4B
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
		}

		// Token: 0x06009868 RID: 39016 RVA: 0x003EDA53 File Offset: 0x003EBE53
		public override void OnVisible(bool _visible)
		{
		}

		// Token: 0x040079B2 RID: 31154
		public GameObject objectLight;

		// Token: 0x040079B3 RID: 31155
		protected Light m_Light;

		// Token: 0x040079B4 RID: 31156
		public Info.LightLoadInfo.Target lightTarget;

		// Token: 0x040079B5 RID: 31157
		public LightColor lightColor;
	}
}
