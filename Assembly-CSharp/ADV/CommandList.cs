using System;
using System.Collections.Generic;
using System.Linq;
using ADV.Commands.Base;
using ADV.Commands.Chara;
using ADV.Commands.Effect;
using ADV.Commands.EventCG;
using ADV.Commands.Game;
using ADV.Commands.H;
using ADV.Commands.MapScene;
using ADV.Commands.Object;
using ADV.Commands.Sound.BGM;
using ADV.Commands.Sound.ENV;
using ADV.Commands.Sound.SE2D;
using ADV.Commands.Sound.SE3D;

namespace ADV
{
	// Token: 0x020006D0 RID: 1744
	public class CommandList : List<CommandBase>
	{
		// Token: 0x0600299F RID: 10655 RVA: 0x000F3945 File Offset: 0x000F1D45
		public CommandList(TextScenario scenario)
		{
			this.scenario = scenario;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000F3954 File Offset: 0x000F1D54
		private TextScenario scenario { get; }

		// Token: 0x060029A1 RID: 10657 RVA: 0x000F395C File Offset: 0x000F1D5C
		public new void Add(CommandBase item)
		{
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000F3960 File Offset: 0x000F1D60
		public bool Add(ScenarioData.Param item, int currentLine)
		{
			CommandBase commandBase = CommandList.CommandGet(item.Command);
			if (commandBase == null)
			{
				return true;
			}
			commandBase.Initialize(this.scenario, item.Command, item.Args);
			commandBase.ConvertBeforeArgsProc();
			for (int i = 0; i < commandBase.args.Length; i++)
			{
				commandBase.args[i] = this.scenario.ReplaceVars(commandBase.args[i]);
			}
			commandBase.localLine = currentLine;
			commandBase.Do();
			base.Add(commandBase);
			return item.Multi;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000F39F0 File Offset: 0x000F1DF0
		public bool Process()
		{
			(from item in this
			where item.Process()
			select item).ToList<CommandBase>().ForEach(delegate(CommandBase item)
			{
				base.Remove(item);
				item.Result(true);
			});
			return this.Any((CommandBase p) => CommandList.IsWait(p.command));
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000F3A59 File Offset: 0x000F1E59
		public void ProcessEnd()
		{
			base.ForEach(delegate(CommandBase item)
			{
				item.Result(false);
			});
			base.Clear();
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000F3A84 File Offset: 0x000F1E84
		private static bool IsWait(Command command)
		{
			switch (command)
			{
			case Command.Fade:
			case Command.FadeWait:
				break;
			default:
				switch (command)
				{
				case Command.CharaKaraokePlay:
				case Command.TaskWait:
					break;
				default:
					if (command != Command.Choice)
					{
						return false;
					}
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000F3AD8 File Offset: 0x000F1ED8
		public static CommandBase CommandGet(Command command)
		{
			switch (command)
			{
			case Command.VAR:
				return new VAR();
			case Command.RandomVar:
				return new RandomVar();
			case Command.Calc:
				return new Calc();
			case Command.Clamp:
				return new Clamp();
			case Command.Min:
				return new Min();
			case Command.Max:
				return new Max();
			case Command.Lerp:
				return new Lerp();
			case Command.LerpAngle:
				return new LerpAngle();
			case Command.InverseLerp:
				return new InverseLerp();
			case Command.LerpV3:
				return new LerpV3();
			case Command.LerpAngleV3:
				return new LerpAngleV3();
			case Command.Tag:
				return new Tag();
			case Command.Format:
				return new Format();
			case Command.IF:
				return new IF();
			case Command.Switch:
				return new Switch();
			case Command.Text:
				return new Text();
			case Command.Voice:
				return new Voice();
			case Command.Motion:
				return new ADV.Commands.Base.Motion();
			case Command.Expression:
				return new ADV.Commands.Base.Expression();
			case Command.Open:
				return new Open();
			case Command.Close:
				return new Close();
			case Command.Jump:
				return new Jump();
			case Command.Choice:
				return new Choice();
			case Command.Wait:
				return new Wait();
			case Command.TextClear:
				return new Clear();
			case Command.FontColor:
				return new FontColor();
			case Command.Scene:
				return new Scene();
			case Command.Regulate:
				return new Regulate();
			case Command.Replace:
				return new Replace();
			case Command.Reset:
				return new Reset();
			case Command.Vector:
				return new Vector();
			case Command.NullLoad:
				return new NullLoad();
			case Command.NullRelease:
				return new NullRelease();
			case Command.NullSet:
				return new NullSet();
			case Command.InfoAudioEco:
				return new InfoAudioEco();
			case Command.InfoAnimePlay:
				return new InfoAnimePlay();
			case Command.Fade:
				return new ADV.Commands.Effect.Fade();
			case Command.SceneFade:
				return new ADV.Commands.Effect.SceneFade();
			case Command.SceneFadeRegulate:
				return new SceneFadeRegulate();
			case Command.FadeWait:
				return new FadeWait();
			case Command.FilterImageLoad:
				return new FilterImageLoad();
			case Command.FilterImageRelease:
				return new FilterImageRelease();
			case Command.FilterSet:
				return new FilterSet();
			case Command.Filter:
				return new Filter();
			case Command.BGMPlay:
				return new ADV.Commands.Sound.BGM.Play();
			case Command.BGMStop:
				return new ADV.Commands.Sound.BGM.Stop();
			case Command.EnvPlay:
				return new ADV.Commands.Sound.ENV.Play();
			case Command.EnvStop:
				return new ADV.Commands.Sound.ENV.Stop();
			case Command.SE2DPlay:
				return new ADV.Commands.Sound.SE2D.Play();
			case Command.SE2DStop:
				return new ADV.Commands.Sound.SE2D.Stop();
			case Command.SE3DPlay:
				return new ADV.Commands.Sound.SE3D.Play();
			case Command.SE3DStop:
				return new ADV.Commands.Sound.SE3D.Stop();
			case Command.CharaStand:
				return new StandPosition();
			case Command.CharaStandFind:
				return new StandFindPosition();
			case Command.CharaPositionAdd:
				return new AddPosition();
			case Command.CharaPositionSet:
				return new SetPosition();
			case Command.CharaPositionLocalAdd:
				return new AddPositionLocal();
			case Command.CharaPositionLocalSet:
				return new SetPositionLocal();
			case Command.CharaMotion:
				return new ADV.Commands.Chara.Motion();
			case Command.CharaMotionWait:
				return new MotionWait();
			case Command.CharaMotionLayerWeight:
				return new MotionLayerWeight();
			case Command.CharaMotionSetParam:
				return new MotionSetParam();
			case Command.CharaMotionIKSetPartner:
				return new MotionIKSetPartner();
			case Command.CharaExpression:
				return new ADV.Commands.Chara.Expression();
			case Command.CharaFixEyes:
				return new FixEyes();
			case Command.CharaFixMouth:
				return new FixMouth();
			case Command.CharaGetShape:
				return new GetShape();
			case Command.CharaClothState:
				return new ClothState();
			case Command.CharaSiruState:
				return new SiruState();
			case Command.CharaVoicePlay:
				return new VoicePlay();
			case Command.CharaVoiceStop:
				return new VoiceStop();
			case Command.CharaVoiceStopAll:
				return new VoiceStopAll();
			case Command.CharaVoiceWait:
				return new VoiceWait();
			case Command.CharaVoiceWaitAll:
				return new VoiceWaitAll();
			case Command.CharaLookEyes:
				return new LookEyes();
			case Command.CharaLookEyesTarget:
				return new LookEyesTarget();
			case Command.CharaLookEyesTargetChara:
				return new LookEyesTargetChara();
			case Command.CharaLookNeck:
				return new LookNeck();
			case Command.CharaLookNeckTarget:
				return new LookNeckTarget();
			case Command.CharaLookNeckTargetChara:
				return new LookNeckTargetChara();
			case Command.CharaLookNeckSkip:
				return new LookNeckSkip();
			case Command.CharaItemCreate:
				return new ItemCreate();
			case Command.CharaItemDelete:
				return new ItemDelete();
			case Command.CharaItemAnime:
				return new ItemAnime();
			case Command.CharaItemFind:
				return new ItemFind();
			case Command.EventCGSetting:
				return new Setting();
			case Command.EventCGRelease:
				return new Release();
			case Command.EventCGNext:
				return new Next();
			case Command.ObjectCreate:
				return new Create();
			case Command.ObjectLoad:
				return new Load();
			case Command.ObjectDelete:
				return new Delete();
			case Command.ObjectPosition:
				return new ADV.Commands.Object.Position();
			case Command.ObjectRotation:
				return new Rotation();
			case Command.ObjectScale:
				return new Scale();
			case Command.ObjectParent:
				return new Parent();
			case Command.ObjectComponent:
				return new Component();
			case Command.ObjectAnimeParam:
				return new AnimeParam();
			case Command.CharaActive:
				return new CharaActive();
			case Command.CharaVisible:
				return new CharaVisible();
			case Command.CharaColor:
				return new CharaColor();
			case Command.CameraLookAt:
				return new ADV.Commands.Game.CameraLookAt();
			case Command.MozVisible:
				return new MozVisible();
			case Command.AddCollider:
				return new AddCollider();
			case Command.ColliderSetActive:
				return new ColliderSetActive();
			case Command.AddNavMeshAgent:
				return new AddNavMeshAgent();
			case Command.NavMeshAgentSetActive:
				return new NavMeshAgentSetActive();
			case Command.BundleCheck:
				return new BundleCheck();
			case Command.Prob:
				return new Prob();
			case Command.Probs:
				return new Probs();
			case Command.FormatVAR:
				return new FormatVAR();
			case Command.Task:
				return new Task();
			case Command.TaskWait:
				return new TaskWait();
			case Command.TaskEnd:
				return new TaskEnd();
			case Command.Log:
				return new ADV.Commands.Base.Log();
			case Command.CharaSetShape:
				return new SetShape();
			case Command.CharaCoordinateChange:
				return new CoordinateChange();
			case Command.InfoAudio:
				return new InfoAudio();
			case Command.ReplaceLanguage:
				return new ReplaceLanguage();
			case Command.InfoText:
				return new InfoText();
			case Command.SendCommandData:
				return new SendCommandData();
			case Command.SendCommandDataList:
				return new SendCommandDataList();
			case Command.PlaySystemSE:
				return new PlaySystemSE();
			case Command.PlayActionSE:
				return new PlayActionSE();
			case Command.PlayEnviroSE:
				return new PlayEnviroSE();
			case Command.PlayFootStepSE:
				return new PlayFootStepSE();
			case Command.InventoryCheck:
				return new InventoryCheck();
			case Command.SetPresent:
				return new SetPresent();
			case Command.SetPresentBirthday:
				return new SetPresentBirthday();
			case Command.ClearItems:
				return new ClearItems();
			case Command.AddItem:
				return new AddItem();
			case Command.RemoveItem:
				return new RemoveItem();
			case Command.ChangeADVFixedAngleCamera:
				return new ChangeADVFixedAngleCamera();
			case Command.InventoryGiveItem:
				return new InventoryGiveItem();
			case Command.SetItemScrounge:
				return new SetItemScrounge();
			case Command.CharaSetting:
				return new CharaSetting();
			case Command.AddItemInPlayer:
				return new AddItemInPlayer();
			case Command.SetItemRandomEvent:
				return new SetItemRandomEvent();
			}
			return null;
		}
	}
}
