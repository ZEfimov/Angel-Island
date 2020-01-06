/*
 *	This program is the CONFIDENTIAL and PROPRIETARY property
 *	of Tomasello Software LLC. Any unauthorized use, reproduction or
 *	transfer of this computer program is strictly prohibited.
 *
 *      Copyright (c) 2004 Tomasello Software LLC.
 *	This is an unpublished work, and is subject to limited distribution and
 *	restricted disclosure only. ALL RIGHTS RESERVED.
 *
 *			RESTRICTED RIGHTS LEGEND
 *	Use, duplication, or disclosure by the Government is subject to
 *	restrictions set forth in subparagraph (c)(1)(ii) of the Rights in
 * 	Technical Data and Computer Software clause at DFARS 252.227-7013.
 *
 *	Angel Island UO Shard	Version 1.0
 *			Release A
 *			March 25, 2004
 */

/* Scripts\Engines\AI\Creature\SpeedInfo.cs
 * ChangeLog
 *	8/7/10, adam
 *		Add TaxCollector to very fast table
 *	7/22/10, adam
 *		add VladDracula to the very fast table with the other champs
 *	7/19/10, adam
 *		Move the Vampire to the fast table from the very fast table
 *	5/12/10, Adam
 *		Add Vampire to the very fast table
 *	4/29/10, adam
 *		moved the Nightmare and WhiteWyrm to the Medium table from the Fast table.
 *		these high-level tames are too fast on a shard without mounts .. they now match Dragons
 */

using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Engines.IOBSystem;
using Server.Factions;

namespace Server
{
	public class SpeedInfo
	{
		// Should we use the new method of speeds?
		private static bool Enabled = true;

		private double m_ActiveSpeed;
		private double m_PassiveSpeed;
		private Type[] m_Types;

		public double ActiveSpeed
		{
			get { return m_ActiveSpeed; }
			set { m_ActiveSpeed = value; }
		}

		public double PassiveSpeed
		{
			get { return m_PassiveSpeed; }
			set { m_PassiveSpeed = value; }
		}

		public Type[] Types
		{
			get { return m_Types; }
			set { m_Types = value; }
		}

		public SpeedInfo(double activeSpeed, double passiveSpeed, Type[] types)
		{
			m_ActiveSpeed = activeSpeed;
			m_PassiveSpeed = passiveSpeed;
			m_Types = types;
		}

		public static bool Contains(object obj)
		{
			if (!Enabled)
				return false;

			if (m_Table == null)
				LoadTable();

			SpeedInfo sp = (SpeedInfo)m_Table[obj.GetType()];

			return (sp != null);
		}

		public static bool GetSpeeds(object obj, ref double activeSpeed, ref double passiveSpeed)
		{
			return GetSpeeds(obj.GetType(), ref activeSpeed, ref passiveSpeed);
		}

		public static bool GetSpeeds(Type t, ref double activeSpeed, ref double passiveSpeed)
		{
			if (!Enabled)
				return false;

			if (m_Table == null)
				LoadTable();

			SpeedInfo sp = (SpeedInfo)m_Table[t];

			if (sp == null)
				return false;

			activeSpeed = sp.ActiveSpeed;
			passiveSpeed = sp.PassiveSpeed;

			return true;
		}

		private static void LoadTable()
		{
			m_Table = new Hashtable();

			for (int i = 0; i < m_Speeds.Length; ++i)
			{
				SpeedInfo info = m_Speeds[i];
				Type[] types = info.Types;

				for (int j = 0; j < types.Length; ++j)
					m_Table[types[j]] = info;
			}
		}

		private static Hashtable m_Table;

		private static SpeedInfo[] m_Speeds = new SpeedInfo[]
			{
				/* Slow */
				new SpeedInfo( 0.3, 0.6, new Type[]
				{
					typeof( AntLion ),			typeof( ArcticOgreLord ),	typeof( BogThing ),
					typeof( Bogle ),			typeof( BoneKnight ),		typeof( EarthElemental ),
					typeof( Ettin ),			typeof( FrostOoze ),		typeof( FrostTroll ),
					typeof( GazerLarva ),		typeof( Ghoul ),			typeof( Golem ),
					typeof( HeadlessOne ),		typeof( Jwilson ),			typeof( Mummy ),
					typeof( Ogre ),				typeof( OgreLord ),			typeof( PlagueBeast ),
					typeof( Quagmire ),			typeof( Rat ),				typeof( RottingCorpse ),
					typeof( Sewerrat ),			typeof( Skeleton ),			typeof( Slime ),
					typeof( Zombie ),			typeof( Walrus ),			typeof( RestlessSoul ),
					typeof( CrystalElemental ),	typeof( DarknightCreeper ),	typeof( MoundOfMaggots ),
					//typeof( Juggernaut ),		typeof( Yamandon ),			typeof( Serado )
				} ),
				/* Fast */
				new SpeedInfo( 0.2, 0.4, new Type[]
				{
					typeof( Vampire ),
					typeof( LordOaks ),			typeof( Silvani ),			typeof( AirElemental ),
					typeof( AncientWyrm ),		typeof( Balron ),			typeof( BladeSpirits ),
					typeof( DreadSpider ),		typeof( Efreet ),			typeof( EtherealWarrior ),
					typeof( Lich ),				/*typeof( Nightmare ),*/		typeof( OphidianArchmage ),
					typeof( OphidianMage ),		typeof( OphidianWarrior ),	typeof( OphidianMatriarch ),
					typeof( OphidianKnight ),	typeof( PoisonElemental ),	typeof( Revenant ),
					typeof( SandVortex ),		typeof( SavageRider ),		typeof( SavageShaman ),
					typeof( SnowElemental ),	/*typeof( WhiteWyrm ),*/		typeof( Wisp ),
					typeof( DemonKnight ),		typeof( GiantBlackWidow ),	
					//typeof( LesserHiryu ),		typeof( Hiryu ),			typeof( LadyOfTheSnow ),
					//typeof( RaiJu ),			typeof( Ronin )
				} ),
				/* Very Fast */
				new SpeedInfo( 0.175, 0.350, new Type[]
				{
					typeof(VladDracula),		typeof ( TaxCollector),
					typeof( Barracoon ),		typeof( Mephitis ),			typeof( Neira ),
					typeof( Rikktor ),			typeof( Semidar ),			typeof( EnergyVortex ),
					typeof( Beetle ),			typeof( Pixie ),			typeof( SilverSerpent ),
					typeof( VorpalBunny ),		typeof( FleshRenderer ),	//typeof( KhaldunRevenant ),
					typeof( FactionDragoon ),	typeof( FactionKnight ),	typeof( FactionPaladin ),
					typeof( FactionHenchman ),	typeof( FactionMercenary ),	typeof( FactionNecromancer ),
					typeof( FactionSorceress ),	typeof( FactionWizard ),	typeof( FactionBerserker ),
					typeof( FactionPaladin ),	//typeof( Leviathan ),		typeof( FireBeetle ),
					//typeof( FanDancer ),		typeof( EliteNinja )
				} ),
				/* Medium */
				new SpeedInfo( 0.25, 0.5, new Type[]
				{
					typeof( ToxicElemental ),	typeof( AgapiteElemental ),	typeof( Alligator ),
					typeof( AncientLich ),		typeof( Betrayer ),			typeof( Bird ),
					typeof( BlackBear ),		typeof( BlackSolenInfiltratorQueen ), typeof( BlackSolenInfiltratorWarrior ),
					typeof( BlackSolenQueen ),	typeof( BlackSolenWarrior ), typeof( BlackSolenWorker ),
					typeof( BloodElemental ),	typeof( Boar ),				typeof( Bogling ),
					typeof( BoneMagi ),			typeof( Brigand ),			typeof( BronzeElemental ),
					typeof( BrownBear ),		typeof( Bull ),				typeof( BullFrog ),
					typeof( Cat ),				typeof( Centaur ),			typeof( ChaosDaemon ),
					typeof( Chicken ),			typeof( GolemController ),	typeof( CopperElemental ),
					typeof( CopperElemental ),	typeof( Cougar ),			typeof( Cow ),
					typeof( Cyclops ),			typeof( Daemon ),			typeof( DeepSeaSerpent ),
					typeof( DesertOstard ),		typeof( DireWolf ),			typeof( Dog ),
					typeof( Dolphin ),			typeof( Dragon ),			typeof( Drake ),
					typeof( DullCopperElemental ), typeof( Eagle ),			typeof( ElderGazer ),
					typeof( EvilMage ),			typeof( EvilMageLord ),		typeof( Executioner ),
					typeof( Savage ),			typeof( FireElemental ),	typeof( FireGargoyle ),
					typeof( FireSteed ),		typeof( ForestOstard ),		typeof( FrenziedOstard ),
					typeof( FrostSpider ),		typeof( Gargoyle ),			typeof( Gazer ),
					typeof( IceSerpent ),		typeof( GiantRat ),			typeof( GiantSerpent ),
					typeof( GiantSpider ),		typeof( GiantToad ),		typeof( Goat ),
					typeof( GoldenElemental ),	typeof( Gorilla ),			typeof( GreatHart ),
					typeof( GreyWolf ),			typeof( GrizzlyBear ),		typeof( Guardian ),
					typeof( Harpy ),			typeof( Harrower ),			typeof( HellHound ),
					typeof( Hind ),				typeof( HordeMinion ),		typeof( Horse ),
					typeof( Horse ),			typeof( IceElemental ),		typeof( IceFiend ),
					typeof( IceSnake ),			typeof( Imp ),				typeof( JackRabbit ),
					typeof( Kirin ),			typeof( Kraken ),			typeof( PredatorHellCat ),
					typeof( LavaLizard ),		typeof( LavaSerpent ),		typeof( LavaSnake ),
					typeof( Lizardman ),		typeof( Llama ),			typeof( Mongbat ),
					typeof( StrongMongbat ),	typeof( MountainGoat ),		typeof( Orc ),
					typeof( OrcBomber ),		typeof( OrcBrute ),			typeof( OrcCaptain ),
					typeof( OrcishLord ),		typeof( OrcishMage ),		typeof( PackHorse ),
					typeof( PackLlama ),		typeof( Panther ),			typeof( Pig ),
					typeof( PlagueSpawn ),		typeof( PolarBear ),		typeof( Rabbit ),
					typeof( Ratman ),			typeof( RatmanArcher ),		typeof( RatmanMage ),
					typeof( RedSolenInfiltratorQueen ), typeof( RedSolenInfiltratorWarrior ), typeof( RedSolenQueen ),
					typeof( RedSolenWarrior ),	typeof( RedSolenWorker ),	typeof( RidableLlama ),
					typeof( Ridgeback ),		typeof( Scorpion ),			typeof( SeaSerpent ),
					typeof( SerpentineDragon ),	typeof( Shade ),			typeof( ShadowIronElemental ),
					typeof( ShadowWisp ),		typeof( ShadowWyrm ),		typeof( Sheep ),
					typeof( SilverSteed ),		typeof( SkeletalDragon ),	typeof( SkeletalMage ),
					typeof( SkeletalMount ),	typeof( HellCat ),			typeof( Snake ),
					typeof( SnowLeopard ),		typeof( SpectralArmour ),	typeof( Spectre ),
					typeof( StoneGargoyle ),	typeof( StoneHarpy ),		typeof( SwampDragon ),
					typeof( ScaledSwampDragon ), typeof( SwampTentacle ),	typeof( TerathanAvenger ),
					typeof( TerathanDrone ),	typeof( TerathanMatriarch ), typeof( TerathanWarrior ),
					typeof( TimberWolf ),		typeof( Titan ),			typeof( Troll ),
					typeof( Unicorn ),			typeof( ValoriteElemental ), typeof( VeriteElemental ),
					typeof( CoMWarHorse ),		typeof( MinaxWarHorse ),	typeof( SLWarHorse ),
					typeof( TBWarHorse ),		typeof( WaterElemental ),	typeof( WhippingVine ),
					typeof( WhiteWolf ),		typeof( Wraith ),			typeof( Wyvern ),
					typeof( KhaldunZealot ),	typeof( KhaldunSummoner ),	typeof( SavageRidgeback ),
					typeof( LichLord ),			typeof( SkeletalKnight ),	
					typeof( MeerWarrior ),		typeof( MeerEternal ),		typeof( MeerMage ),
					typeof( MeerCaptain ),		typeof( JukaLord ),			typeof( JukaMage ),
					typeof( JukaWarrior ),		typeof( AbysmalHorror ),	typeof( BoneDemon ),
					typeof( Devourer ),			typeof( FleshGolem ),		typeof( Gibberling ),
					typeof( GoreFiend ),		typeof( Impaler ),			typeof( PatchworkSkeleton ),
					typeof( Ravager ),			typeof( ShadowKnight ),		typeof( SkitteringHopper ),
					typeof( Treefellow ),		typeof( VampireBat ),		typeof( WailingBanshee ),
					typeof( WandererOfTheVoid ),	//typeof( Cursed ),			typeof( GrimmochDrummel ),
					//typeof( LysanderGathenwale ),	typeof( MorgBergen ),	typeof( ShadowFiend ),
					typeof( SpectralArmour ),	//typeof( TavaraSewel ),		typeof( ArcaneDaemon ),
					//typeof( Doppleganger ),		typeof( EnslavedGargoyle ), typeof( ExodusMinion ),
					//typeof( ExodusOverseer ),	typeof( GargoyleDestroyer ),	typeof( GargoyleEnforcer ),
					//typeof( Moloch ),			typeof( BakeKitsune ),		typeof( DeathwatchBeetleHatchling ),
					//typeof( Kappa ),			typeof( KazeKemono ),		typeof( DeathwatchBeetle ),
					//typeof( TsukiWolf ),		typeof( YomotsuElder ),		typeof( YomotsuPriest ),
					//typeof( YomotsuWarrior ),	typeof( RevenantLion ),		typeof( Oni ),
					//typeof( RuneBeetle ),		typeof( Gaman ),			typeof( Crane )
					
					// moved from the FAST table to here.
					//	these high-level tames are too fast on a shard without mounts .. they now match Dragons
					typeof( WhiteWyrm ), typeof( Nightmare ),
				} )
			};
	}
}