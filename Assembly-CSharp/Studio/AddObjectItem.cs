using System;
using System.Collections.Generic;
using System.Linq;
using Manager;
using Studio.Sound;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011EF RID: 4591
	public static class AddObjectItem
	{
		// Token: 0x060096DC RID: 38620 RVA: 0x003E5E3C File Offset: 0x003E423C
		public static OCIItem Add(int _group, int _category, int _no)
		{
			int newIndex = Studio.GetNewIndex();
			Singleton<UndoRedoManager>.Instance.Do(new AddObjectCommand.AddItemCommand(_group, _category, _no, newIndex, Studio.optionSystem.initialPosition));
			return Studio.GetCtrlInfo(newIndex) as OCIItem;
		}

		// Token: 0x060096DD RID: 38621 RVA: 0x003E5E78 File Offset: 0x003E4278
		public static OCIItem Load(OIItemInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			ChangeAmount source = _info.changeAmount.Clone();
			OCIItem ociitem = AddObjectItem.Load(_info, _parent, _parentNode, false, -1);
			_info.changeAmount.Copy(source, true, true, true);
			AddObjectAssist.LoadChild(_info.child, ociitem, null);
			return ociitem;
		}

		// Token: 0x060096DE RID: 38622 RVA: 0x003E5EBC File Offset: 0x003E42BC
		public static OCIItem Load(OIItemInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCIItem ociitem = new OCIItem();
			Info.ItemLoadInfo loadInfo = AddObjectItem.GetLoadInfo(_info.group, _info.category, _info.no);
			if (loadInfo == null)
			{
				loadInfo = AddObjectItem.GetLoadInfo(0, 0, 399);
			}
			ociitem.objectInfo = _info;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(loadInfo.bundlePath, loadInfo.fileName, true, loadInfo.manifest);
			if (gameObject == null)
			{
				Studio.DeleteIndex(_info.dicKey);
				return null;
			}
			gameObject.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			ociitem.objectItem = gameObject;
			ociitem.itemComponent = gameObject.GetComponent<ItemComponent>();
			ociitem.arrayRender = (from v in gameObject.GetComponentsInChildren<Renderer>()
			where v.enabled
			select v).ToArray<Renderer>();
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			if (!componentsInChildren.IsNullOrEmpty<ParticleSystem>())
			{
				ociitem.arrayParticle = (from v in componentsInChildren
				where v.isPlaying
				select v).ToArray<ParticleSystem>();
			}
			MeshCollider component = gameObject.GetComponent<MeshCollider>();
			if (component)
			{
				component.enabled = false;
			}
			ociitem.dynamicBones = gameObject.GetComponentsInChildren<DynamicBone>();
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, _info.dicKey);
			guideObject.isActive = false;
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			GuideObject guideObject2 = guideObject;
			guideObject2.isActiveFunc = (GuideObject.IsActiveFunc)Delegate.Combine(guideObject2.isActiveFunc, new GuideObject.IsActiveFunc(ociitem.OnSelect));
			guideObject.enableScale = (!(ociitem.itemComponent != null) || ociitem.itemComponent.isScale);
			guideObject.SetVisibleCenter(true);
			ociitem.guideObject = guideObject;
			if (ociitem.itemComponent != null && ociitem.itemComponent.childRoot != null)
			{
				ociitem.childRoot = ociitem.itemComponent.childRoot;
			}
			if (ociitem.childRoot == null)
			{
				ociitem.childRoot = gameObject.transform;
			}
			ociitem.animator = gameObject.GetComponentInChildren<Animator>();
			if (ociitem.animator)
			{
				ociitem.animator.enabled = (ociitem.itemComponent != null && ociitem.itemComponent.isAnime);
			}
			if (ociitem.itemComponent != null)
			{
				ociitem.itemComponent.SetGlass();
				ociitem.itemComponent.SetEmission();
				if (_addInfo && ociitem.itemComponent.check)
				{
					Color[] array = ociitem.itemComponent.defColorMain;
					for (int i = 0; i < 3; i++)
					{
						_info.colors[i].mainColor = array[i];
					}
					array = ociitem.itemComponent.defColorPattern;
					for (int j = 0; j < 3; j++)
					{
						_info.colors[j].pattern.color = array[j];
						_info.colors[j].metallic = ociitem.itemComponent.info[j].defMetallic;
						_info.colors[j].glossiness = ociitem.itemComponent.info[j].defGlossiness;
						_info.colors[j].pattern.clamp = ociitem.itemComponent.info[j].defClamp;
						_info.colors[j].pattern.uv = ociitem.itemComponent.info[j].defUV;
						_info.colors[j].pattern.rot = ociitem.itemComponent.info[j].defRot;
					}
					_info.colors[3].mainColor = ociitem.itemComponent.defGlass;
					_info.emissionColor = ociitem.itemComponent.DefEmissionColor;
					_info.emissionPower = ociitem.itemComponent.defEmissionStrength;
					_info.lightCancel = ociitem.itemComponent.defLightCancel;
				}
				ociitem.itemComponent.SetupSea();
			}
			ociitem.particleComponent = gameObject.GetComponent<ParticleComponent>();
			if (ociitem.particleComponent != null && _addInfo)
			{
				_info.colors[0].mainColor = ociitem.particleComponent.defColor01;
			}
			ociitem.iconComponent = gameObject.GetComponent<IconComponent>();
			if (ociitem.iconComponent != null)
			{
				ociitem.iconComponent.Layer = LayerMask.NameToLayer("Studio/Camera");
			}
			ociitem.VisibleIcon = Singleton<Studio>.Instance.workInfo.visibleGimmick;
			ociitem.panelComponent = gameObject.GetComponent<PanelComponent>();
			if (_addInfo && ociitem.panelComponent != null)
			{
				_info.colors[0].mainColor = ociitem.panelComponent.defColor;
				_info.colors[0].pattern.uv = ociitem.panelComponent.defUV;
				_info.colors[0].pattern.clamp = ociitem.panelComponent.defClamp;
				_info.colors[0].pattern.rot = ociitem.panelComponent.defRot;
			}
			ociitem.seComponent = gameObject.GetComponent<SEComponent>();
			if (_addInfo && ociitem.itemComponent != null && !ociitem.itemComponent.optionInfos.IsNullOrEmpty<ItemComponent.OptionInfo>())
			{
				_info.option = Enumerable.Repeat<bool>(true, ociitem.itemComponent.optionInfos.Length).ToList<bool>();
			}
			if (_addInfo)
			{
				Studio.AddInfo(_info, ociitem);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ociitem);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(loadInfo.name, parent);
			treeNodeObject.treeState = _info.treeState;
			TreeNodeObject treeNodeObject2 = treeNodeObject;
			treeNodeObject2.onVisible = (TreeNodeObject.OnVisibleFunc)Delegate.Combine(treeNodeObject2.onVisible, new TreeNodeObject.OnVisibleFunc(ociitem.OnVisible));
			treeNodeObject.enableVisible = true;
			treeNodeObject.visible = _info.visible;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ociitem.treeNodeObject = treeNodeObject;
			if (!loadInfo.bones.IsNullOrEmpty<string>())
			{
				ociitem.itemFKCtrl = gameObject.AddComponent<ItemFKCtrl>();
				ociitem.itemFKCtrl.InitBone(ociitem, loadInfo, _addInfo);
			}
			else
			{
				ociitem.itemFKCtrl = null;
			}
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			_info.changeAmount.OnChange();
			Studio.AddCtrlInfo(ociitem);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ociitem);
			}
			if (ociitem.animator)
			{
				if (_info.animePattern != 0)
				{
					ociitem.SetAnimePattern(_info.animePattern);
				}
				ociitem.animator.speed = _info.animeSpeed;
				if (_info.animeNormalizedTime != 0f && ociitem.animator.layerCount != 0)
				{
					ociitem.animator.Update(1f);
					AnimatorStateInfo currentAnimatorStateInfo = ociitem.animator.GetCurrentAnimatorStateInfo(0);
					ociitem.animator.Play(currentAnimatorStateInfo.shortNameHash, 0, _info.animeNormalizedTime);
				}
			}
			ociitem.SetupPatternTex();
			ociitem.SetMainTex();
			ociitem.UpdateColor();
			ociitem.ActiveFK(ociitem.itemInfo.enableFK);
			ociitem.UpdateFKColor();
			ociitem.ActiveDynamicBone(ociitem.itemInfo.enableDynamicBone);
			ociitem.UpdateOption();
			ParticleComponent particleComponent = ociitem.particleComponent;
			if (particleComponent != null)
			{
				particleComponent.PlayOnLoad();
			}
			return ociitem;
		}

		// Token: 0x060096DF RID: 38623 RVA: 0x003E6690 File Offset: 0x003E4A90
		private static Info.ItemLoadInfo GetLoadInfo(int _group, int _category, int _no)
		{
			Dictionary<int, Dictionary<int, Info.ItemLoadInfo>> dictionary = null;
			if (!Singleton<Info>.Instance.dicItemLoadInfo.TryGetValue(_group, out dictionary))
			{
				return null;
			}
			Dictionary<int, Info.ItemLoadInfo> dictionary2 = null;
			if (!dictionary.TryGetValue(_category, out dictionary2))
			{
				return null;
			}
			Info.ItemLoadInfo result = null;
			if (!dictionary2.TryGetValue(_no, out result))
			{
				return null;
			}
			return result;
		}
	}
}
