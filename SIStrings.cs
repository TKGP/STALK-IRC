using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STALK_IRC
{
    class SIStrings
    {
        static List<string> times = new List<string>();
        static List<string> observance = new List<string>();
        static List<string> suffixes = new List<string>();
        static List<string> fNames = new List<string>();
        static List<string> sNames = new List<string>();
        public static List<string> validFactions = new List<string>();
        public static List<string> validGames = new List<string>();
        static Dictionary<string, string> levelNames = new Dictionary<string, string>();
        static Dictionary<string, string> deathBySection = new Dictionary<string, string>();
        static Dictionary<string, string> deathByClass = new Dictionary<string, string>();
        static Random rand = new Random();

        static string[] formats = {
            "$when $level $saw $name $death.",
            "$level $when $saw $name $death.",
            "$level $name $death.",
            "$when $level $name $death.",
            "$saw $level $name $death.",
            "$when $saw $name $death.",
            "$name $death $when $level.",
            "$name $death $level $when.",
            "$when $name $death $level.",
            "$when $name $death.",
            "$when $saw $name $death $level.",
            "$saw $name $death.",
            "$saw $name $death $when $level.",
            "$saw $name $death $level.",
        };

        static string RandomString(List<string> collection)
        {
            return collection[rand.Next(collection.Count - 1)];
        }

        public static string BuildSentence(string name, string level, string section, string classType)
        {
            name = name.Replace('_', ' ');
            string levelText = (levelNames.ContainsKey(level) ? levelNames[level] : ("in the Zone (" + level + ")"));
            classType = classType.ToUpper();
            if (classType == "DARK_STALKER" && rand.Next(1000) == 666) // :^)
                return "Psssh... nothin personnel... kid...";
            string deathText;
            if (deathBySection.ContainsKey(section))
                deathText = deathBySection[section];
            else if (deathByClass.ContainsKey(classType))
                deathText = deathByClass[classType];
            else
                deathText = "died of unknown causes (" + classType + ")";
            string message = formats[rand.Next(formats.Length - 1)];
            message = message.Replace("$when", RandomString(times));
            message = message.Replace("$level", levelText);
            message = message.Replace("$saw", RandomString(observance));
            message = message.Replace("$name", name);
            message = message.Replace("$death", deathText);
            message = message[0].ToString().ToUpper() + message.Substring(1);
            if (rand.Next(2) >= 1)
                message += ' ' + RandomString(suffixes);
            return message;
        }

        public static string GenerateName()
        {
            return RandomString(fNames) + " " + RandomString(sNames);
        }

        public static void Populate()
        {
            validFactions.AddRange(new string[]{
                "Loners",
                "Bandits",
                "Duty",
                "Freedom",
                "Ecologists",
                "ClearSky",
                "Mercenaries",
                "Military",
                "Monolith",
            });

            validGames.AddRange(new string[]{
                "SOC",
                "CS",
                "COP",
                "LA",
            });

            times.AddRange(new string[]{
                "recently",
                "a few minutes ago",
                "just now",
                "a couple minutes ago",
                "a short time ago",
                "just recently",
            });

            observance.AddRange(new string[]{
                "I saw",
                "I noticed",
                "it seems",
                "someone told me",
                "word is",
                "I've seen",
                "I've noticed",
            });

            suffixes.AddRange(new string[]{
                "Good riddance!",
                "What a shame...",
                "He was a good stalker.",
                "Let's drink to him once more!",
                "Damn, he owed me money.",
                "Be careful, friends.",
                "Watch out if you're in the area.",
                "He got what he deserved.",
                "Should have been more careful.",
                "Nasty.",
                "Shit, that could have been me.",
                "I'm getting out of here.",
                "Too bad.",
                "Terrible.",
                "Funny.",
                "Oh well.",
                "Hah!",
                "Hope the info is useful.",
                "He had it coming.",
                "Guess he won't be needing that gear any more...",
                "That's what happens when you let your guard down.",
                "Heh.",
                "Oops!",
                "I never liked him anyways.",
            });

            // Shadow of Chernobyl
            levelNames["l01_escape"] = "at the Cordon";
            levelNames["l02_garbage"] = "at the Garbage";
            levelNames["l03_agroprom"] = "at Agroprom";
            levelNames["l03u_agr_underground"] = "in Agroprom Underground";
            levelNames["l04_darkvalley"] = "in Dark Valley";
            levelNames["l04u_labx18"] = "in Lab X-18";
            levelNames["l05_bar"] = "at the Bar";
            levelNames["l06_rostok"] = "at Wild Territory";
            levelNames["l07_military"] = "at the Army Warehouses";
            levelNames["l08_yantar"] = "at Lake Yantar";
            levelNames["l08u_brainlab"] = "in Lab X-16";
            levelNames["l10_radar"] = "in Red Forest";
            levelNames["l10u_bunker"] = "in the Brain Scorcher";
            levelNames["l11_pripyat"] = "in Eastern Pripyat";
            levelNames["l12_stancia"] = "outside the CNPP";
            levelNames["l12_stancia_2"] = levelNames["l12_stancia"];
            levelNames["l12u_control_monolith"] = "inside the CNPP";
            levelNames["l12u_sarcofag"] = levelNames["l12u_control_monolith"];
            // Clear Sky
            levelNames["agroprom"] = levelNames["l03_agroprom"];
            levelNames["agroprom_underground"] = levelNames["l03u_agr_underground"];
            levelNames["darkvalley"] = levelNames["l04_darkvalley"];
            levelNames["escape"] = levelNames["l01_escape"];
            levelNames["garbage"] = levelNames["l02_garbage"];
            levelNames["hospital"] = "in the abandoned Hospital";
            levelNames["limansk"] = "in Limansk";
            levelNames["marsh"] = "in the Swamps";
            levelNames["military"] = levelNames["l07_military"];
            levelNames["red_forest"] = levelNames["l10_radar"];
            levelNames["stancia_2"] = levelNames["l12_stancia"];
            levelNames["yantar"] = levelNames["l08_yantar"];
            // Call of Pripyat
            levelNames["jupiter"] = "around Jupiter";
            levelNames["jupiter_underground"] = "in the Pripyat Underpass";
            levelNames["labx8"] = "in Lab X-8";
            levelNames["pripyat"] = "in Western Pripyat";
            levelNames["zaton"] = "at Zaton";
            // Lost Alpha
            levelNames["la01_escape"] = levelNames["l01_escape"];
            levelNames["la02_garbage"] = levelNames["l02_garbage"];
            levelNames["la03_agroprom"] = levelNames["l03_agroprom"];
            levelNames["la04_darkdolina"] = levelNames["l04_darkvalley"];
            levelNames["la04u_darklab"] = levelNames["l04u_labx18"];
            levelNames["la05_bar_rostok"] = "at the Great Metal Factory";
            levelNames["la06_yantar"] = levelNames["l08_yantar"];
            levelNames["la07_military"] = levelNames["l07_military"];
            levelNames["la08_deadcity"] = "in Dead City";
            levelNames["la09_swamp"] = "in the Great Swamps";
            levelNames["la10_radar"] = "at Radar";
            levelNames["la10u_bunker"] = "in Lab X-10";
            levelNames["la11_pripyat"] = levelNames["l11_pripyat"];
            levelNames["la12_stancia"] = levelNames["l12_stancia"];
            levelNames["la12u_sarcofag"] = levelNames["l12u_control_monolith"];
            levelNames["la13_generators"] = "at the Generators";
            //levelNames["la13u_oso"] = "";
            levelNames["la13u_warlab"] = "in Lab X-2";
            levelNames["la14_rostok_factory"] = "at Rostok Factory";
            levelNames["la14u_secret_lab"] = "in Lab X-14";
            levelNames["la15_darkscape"] = "at Darkscape";
            levelNames["la15u_mines"] = "in the Mines";
            levelNames["la16_lost_factory"] = "at the Concrete Factory";
            levelNames["la16u_labx16"] = "in Lab X-16";
            levelNames["la17_outskirts"] = "at Pripyat Outskirts";
            levelNames["la17u_labx7"] = "in Lab X-7";
            levelNames["la18_damned"] = "in the Pripyat Sewers";
            levelNames["la19_country"] = "on the Countryside";
            levelNames["la20_forgotten"] = "at the Construction Site";
            //levelNames["la21_generators_2"] = "";
            levelNames["la22_forest"] = "in the Forest";

            deathByClass["O_ACTOR"] = "committed suicide";
            deathByClass["S_ACTOR"] = deathByClass["O_ACTOR"];
            deathByClass["AI_STL_S"] = "was gunned down by a stalker";
            deathByClass["AI_STL"] = deathByClass["AI_STL_S"];
            deathByClass["SM_BLOOD"] = "was sucked dry by a bloodsucker";
            deathByClass["SM_BOARW"] = "was goared by a boar";
            deathByClass["SM_BURER"] = "met their fate at the hands of a burer";
            deathByClass["SM_CAT_S"] = "was scratched to death by a cat";
            deathByClass["SM_CHIMS"] = "was leapt on by a chimera";
            deathByClass["SM_CONTR"] = "was zombified by a controller";
            deathByClass["SM_DOG_S"] = "was mauled by a blind dog";
            deathByClass["SM_FLESH"] = "somehow died to a pig";
            deathByClass["SM_IZLOM"] = "was smashed by an izlom";
            deathByClass["SM_GIANT"] = "was crushed by a pseudogiant";
            deathByClass["AI_PHANT"] = "succumbed to brain damage";
            deathByClass["SM_POLTR"] = "was killed by some invisible thing";
            deathByClass["SM_P_DOG"] = "was hunted down by a pseudodog";
            deathByClass["SM_DOG_P"] = "was overwhelmed by a psy dog";
            deathByClass["SM_DOG_F"] = deathByClass["SM_DOG_P"];
            deathByClass["AI_RAT"] = "was eaten alive by rats";
            deathByClass["SM_SNORK"] = "was kicked to death by a snork";
            deathByClass["SM_TUSHK"] = "was gnawed to the bone by a tushkano";
            deathByClass["SM_ZOMBI"] = "had their brains eaten by a zombie";
            deathByClass["C_HLCP_S"] = "was shot down by a helicopter";
            deathByClass["SCRPTCAR"] = "became roadkill";
            deathByClass["ZS_MBALD"] = "walked right into an anomaly";
            deathByClass["ZS_GALAN"] = "was sucked into a vortex";
            deathByClass["ZS_MINCE"] = deathByClass["ZS_GALAN"];
            deathByClass["ZS_RADIO"] = "stepped into an anomalous field";
            deathByClass["ZS_TORRD"] = "couldn't outrun a comet";
            deathByClass["ZS_BFUZZ"] = "got tangled up in a burnt fuzz";
            deathByClass["Z_MBALD"] = "triggered a land mine";
            deathByClass["Z_RADIO"] = deathByClass["ZS_RADIO"];
            //deaths["Z_ZONE"] = "cse_alife_anomalous_zone";
            deathByClass["Z_CFIRE"] = "tripped and fell into a campfire";
            //deaths["Z_TORRID"] = "cse_alife_torrid_zone";
            //deaths["Z_RUSTYH"] = "cse_alife_zone_visual";
            deathByClass["Z_AMEBA"] = deathByClass["ZS_BFUZZ"];
            deathByClass["S_EXPLO"] = "was blown apart by an explosion";
            deathByClass["II_EXPLO"] = deathByClass["S_EXPLO"];

            // NPC factions
            deathByClass["STALKER"] = "was gunned down by a loner";
            deathByClass["BANDIT"] = "was looted by a bandit";
            deathByClass["DOLG"] = "was removed by a Dutyer";
            deathByClass["FREEDOM"] = "was blazed by a Freedomer";
            deathByClass["ECOLOG"] = "was blind-sided by a scientist";
            deathByClass["KILLER"] = "was taken out by a mercenary";
            deathByClass["MILITARY"] = "was shot down by the military";
            deathByClass["MONOLITH"] = "was purged by a cultist";
            deathByClass["ZOMBIED"] = "was blown away by a zombified stalker";
            deathByClass["ARENA_ENEMY"] = "lost in the Arena";
            deathByClass["STRANGER"] = deathByClass["STALKER"];
            deathByClass["CSKY"] = "was killed by the best faction";
            deathByClass["RENEGADE"] = "was killed by some bandit wannabe";
            deathByClass["ARMY"] = deathByClass["MILITARY"];
            deathByClass["DARK_STALKER"] = "was killed by a deformed stalker";


            // Stuff from mods

            // The Faction War - technically Izloms
            deathBySection["bloodsucker_flesh"] = deathByClass["SM_BLOOD"];
            deathBySection["bloodsucker_weak"] = deathByClass["SM_BLOOD"];
            deathBySection["bloodsucker_normal"] = deathByClass["SM_BLOOD"];
            deathBySection["bloodsucker_strong"] = deathByClass["SM_BLOOD"];

            // AMK
            deathByClass["TURRETMG"] = "went down in a hail of bullets";

            // SGM
            deathByClass["BANDIT_ENEMY"] = deathByClass["BANDIT"];
            deathByClass["BANDIT_ALIES"] = deathByClass["BANDIT"];
            deathByClass["ALFA_FORCE"] = "was eliminated by an Alpha Squad";
            deathByClass["MERCENARY"] = deathByClass["KILLER"];

            // Way to Pripyat (?)
            deathByClass["KİLLER"] = deathByClass["KILLER"];

            // Lost World 3.0
            deathByClass["NEBO"] = deathByClass["CSKY"];
            //deathByClass["DRAGON"] = "";
            //deathByClass["TUSHKANO"] = ""; // Why is this a thing

            // Doomed City
            //levelNames["l01_krasivay"] = "";
            //levelNames["l02_dd"] = "";
            levelNames["l03_rinok"] = "at the Market";
            levelNames["l04_pogost"] = "in the Cemetary";
            levelNames["l05_vokzal"] = "at the Railway Station";

            // STALKERSOUP - aka Kostya's et al
            levelNames["atp_for_test22"] = "at the ATP";
            //levelNames["av_peshera"] = "";
            //levelNames["aver"] = "";
            levelNames["cs_agroprom_underground"] = levelNames["l03u_agr_underground"];
            //levelNames["dead_forest"] = "";
            levelNames["dead_city"] = levelNames["la08_deadcity"];
            levelNames["digger_stash"] = "in Black Digger's Stash";
            levelNames["generators"] = levelNames["la13_generators"];
            //levelNames["hiding_road"] = "";
            levelNames["k01_darkscape"] = levelNames["la15_darkscape"];
            levelNames["lab_x14"] = "in Lab X-14";
            //levelNames["level_f-1"] = "";
            //levelNames["lost_village"] = "";
            levelNames["peshera"] = "in the Cave";
            levelNames["predbannik"] = "at Predbannik";
            //levelNames["promzone"] = "";
            //levelNames["puzir"] = "";
            //levelNames["swamp_old"] = "";
            //levelNames["warlab"] = "";
            levelNames["yantar_old"] = levelNames["l08_yantar"];

            // Lost World Origin
            levelNames["agro_full_underground"] = levelNames["l03u_agr_underground"];
            levelNames["darkscape"] = levelNames["la15_darkscape"];
            levelNames["escape_1935"] = levelNames["l01_escape"];
            //levelNames["full_rostok"] = "";
            levelNames["lab_x18"] = levelNames["l04u_labx18"];
            levelNames["pripyat_full"] = levelNames["l11_pripyat"];
            levelNames["radar_1935"] = levelNames["l10_radar"];
            levelNames["yantar_1935"] = levelNames["l08_yantar"];

            // Lost World Requital
            levelNames["garbage_old"] = levelNames["l02_garbage"];

            // Massive Zone
            //levelNames["collector22"] = "";
            levelNames["deadcity"] = levelNames["la08_deadcity"];

            // Oblivion Lost Remake
            levelNames["lvl1_escape"] = levelNames["l01_escape"];
            levelNames["lvl2_garbage"] = levelNames["l02_garbage"];
            levelNames["lvl3_agroprom"] = levelNames["l03_agroprom"];
            levelNames["lvl4_darkdolina"] = levelNames["l04_darkvalley"];
            levelNames["lvl4u_darklab"] = levelNames["l04u_labx18"];
            levelNames["lvl5_bar"] = levelNames["l05_bar"];
            levelNames["lvl6_rostok"] = levelNames["l06_rostok"];
            levelNames["lvl7_yantar"] = levelNames["l08_yantar"];
            //levelNames["lvl8_swamp"] = "";
            levelNames["lvl9_military"] = levelNames["l07_military"];
            levelNames["lvl10_radar"] = levelNames["l10_radar"];
            levelNames["lvl11_deadcity"] = levelNames["la08_deadcity"];
            levelNames["lvl12_radar_bunker"] = levelNames["l10u_bunker"];
            levelNames["lvl13_prip"] = levelNames["l11_pripyat"];
            levelNames["lvl14_stancia"] = levelNames["l12_stancia"];
            levelNames["lvl15_sarcofag"] = levelNames["l12u_control_monolith"];
            levelNames["lvl16_generators"] = levelNames["la13_generators"];
            //levelNames["lvl17_warlab"] = "";

            // Secret of the Zone
            levelNames["gz_agroprom"] = levelNames["l03_agroprom"];
            levelNames["gz_agroprom_underground"] = levelNames["l03u_agr_underground"];
            levelNames["gz_darkscape"] = levelNames["la15_darkscape"];
            levelNames["gz_darkvalley"] = levelNames["l04_darkvalley"];
            levelNames["gz_garbage1935"] = levelNames["l02_garbage"];
            levelNames["gz_labx13"] = "in Lab X-13";
            //levelNames["gz_promzone"] = "";
            levelNames["gz_red_forest"] = levelNames["l10_radar"];
            levelNames["gz_yantar"] = levelNames["l08_yantar"];

            // SRP Mod 0.3 - no, not /that/ SRP
            //levelNames["l12u_sarcofag2"] = "";

            // HARDWARMOD
            //levelNames["arena"] = "";

            // Time for Change
            //levelNames["cartographer_place"] = "";
            levelNames["generator"] = levelNames["la13_generators"];

            // Miliyantar
            //levelNames["miliyan"] = "";

            // Path in the Mist
            levelNames["level_bar"] = levelNames["l05_bar"];
            //levelNames["level_city"] = "";
            levelNames["level_escape"] = levelNames["l01_escape"];
            levelNames["level_garbage"] = levelNames["l02_garbage"];
            levelNames["level_labx5"] = "in Lab X-5";

            // Reach + Slenderman
            //haha no

            // Valley of Whispers
            //levelNames["sad"] = "";

            // World of War
            //levelNames["zakordon"] = "";

            // Spatial Anomaly
            //levelNames["black_valley"] = "";
            levelNames["labx7"] = levelNames["la17u_labx7"];
            levelNames["prostranstvenniy_puzir"] = "in the Spatial Bubble";
            //levelNames["station_digger"] = "";
            

            // Random stalker names from vanilla SoC

            fNames.AddRange(new string[]{
	            "Alex",
	            "Anton",
	            "Arthur",
	            "Artyom",
	            "Bogdan",
	            "Borka",
	            "Borya",
	            "Danila",
	            "Danko",
	            "Danya",
	            "Denis",
	            "Dima",
	            "Dimka",
	            "Dmitro",
	            "Edik",
	            "Egor",
	            "Egorka",
	            "Fedka",
	            "Fedya",
	            "Filka",
	            "Filya",
	            "Fima",
	            "Fyodor",
	            "Gena",
	            "Genka",
	            "Georg",
	            "German",
	            "Gleb",
	            "Gosha",
	            "Grisha",
	            "Grishka",
	            "Grishko",
	            "Igoryok",
	            "Ilya",
	            "Kolya",
	            "Kostik",
	            "Kostya",
	            "Lenka",
	            "Lyonya",
	            "Lyonya",
	            "Lyoshka",
	            "Lyova",
	            "Matvey",
	            "Max",
	            "Misha",
	            "Mishka",
	            "Mitya",
	            "Nik",
	            "Nikita",
	            "Oleg",
	            "Pasha",
	            "Pashka",
	            "Petka",
	            "Petro",
	            "Petya",
	            "Roma",
	            "Romka",
	            "Rostik",
	            "Rus",
                "Sanya",
	            "Sanyok",
	            "Sava",
	            "Semyon",
	            "Senya",
	            "Seryoga",
	            "Seva",
	            "Sevka",
	            "Shurik",
	            "Slava",
	            "Slavik",
	            "Stepa",
	            "Stepan",
	            "Syoma",
	            "Tima",
	            "Timka",
	            "Tolik",
	            "Toshka",
	            "Vadik",
	            "Vadim",
	            "Vadya",
	            "Valera",
	            "Valik",
	            "Vanka",
	            "Vanya",
	            "Vaska",
	            "Vasko",
	            "Vasya",
	            "Venya",
	            "Vitalik",
	            "Vitka",
	            "Vitya",
	            "Vlad",
	            "Vova",
	            "Vovka",
	            "Yara",
	            "Yarik",
	            "Yasha",
	            "Yashka",
	            "Yurka",
	            "Yurko",
	            "Yury",
	            "Zhenka",
	            "Zhenya",
	            "Zhora",
            });

            sNames.AddRange(new string[]{
	            "AA-Gun",
	            "Abject",
	            "Absolute",
	            "Accordion",
	            "Accountant",
	            "Ace",
	            "Aesthete",
	            "Agent",
	            "Alligator",
	            "Ambassador",
	            "Anaconda",
	            "Anomaly",
	            "Ant",
	            "Apostle",
	            "Armorer",
	            "Arsonist",
	            "Artist",
	            "Ascetic",
	            "Athlete",
	            "Attorney",
	            "Auroch",
	            "Aviator",
	            "Awl",
	            "Axe",
	            "Babbler",
	            "Baboon",
	            "Backbone",
	            "Bagel",
	            "Baldy",
	            "Banana",
	            "Banker",
	            "Barabashka",
	            "Barbarian",
	            "Bard",
	            "Barmaley",
	            "Baron",
	            "Barrel",
	            "Batiy",
	            "Baton",
	            "Bayonet",
	            "Bear",
	            "Beast",
	            "Beaten",
	            "Bedbug",
	            "Beekeeper",
	            "Beetle",
	            "Big-eyed",
	            "Big Fellow",
	            "Bitter",
	            "Black",
	            "Blessed",
	            "Blighter",
	            "Blimp",
	            "Blind",
	            "Block",
	            "Boa",
	            "Boar",
	            "Boatswain",
	            "Bolt",
	            "Bone",
	            "Bony",
	            "Boot",
	            "Bore",
	            "Boss",
	            "Boulder",
	            "Bourbon",
	            "Bourgeois",
	            "Bowlegged",
	            "Brave",
	            "Break",
	            "Brick",
	            "Brigand",
	            "Bubble",
	            "Buddy",
	            "Buffalo",
	            "Buffer",
	            "Buffoon",
	            "Bug",
	            "Bulkin",
	            "Bull",
	            "Bulldog",
	            "Bulldozer",
	            "Bum",
	            "Bummer",
	            "Bumper",
	            "Bundle",
	            "Bureaucrat",
	            "Burnt",
	            "Burr",
	            "Bushwhacker",
	            "Bustard",
	            "Busybody",
	            "Butcher",
	            "Butt",
	            "Butters",
	            "Cabbage",
	            "Caesar",
	            "Canary",
	            "Capital",
	            "Captain",
	            "Carbide",
	            "Cardan",
	            "Carp",
	            "Carpenter",
	            "Carrier",
	            "Casanova",
	            "Cat",
	            "Caveman",
	            "Chance",
	            "Chapay",
	            "Chapayev",
	            "Cheat",
	            "Chebur",
	            "Cheburashka",
	            "Chemist",
	            "Chest",
	            "Chief",
	            "Chill",
	            "Chilly",
	            "Chimneysweep",
	            "Chingachuk",
	            "Chipmunk",
	            "Chisel",
	            "Choker",
	            "Chopper",
	            "Chronic",
	            "Chucker",
	            "Cider",
	            "Claw",
	            "Cleaner",
	            "Closet",
	            "Clown",
	            "Clumsy",
	            "Coachman",
	            "Cobra",
	            "Cockroach",
	            "Coffin",
	            "Cognac",
	            "Collar",
	            "Colonel",
	            "Comanche",
	            "Comatose",
	            "Comb",
	            "Commodore",
	            "Conman",
	            "Contact",
	            "Corner-cutter",
	            "Corpse",
	            "Cosmos",
	            "Cossack",
	            "Cottager",
	            "Courier",
	            "Cracker",
	            "Cranky",
	            "Cripple",
	            "Crocodile",
	            "Cross",
	            "Cross-eyed",
	            "Crowbar",
	            "Cursor",
	            "Cutter",
	            "Cynic",
	            "Dad",
	            "Dancer",
	            "Dandy",
	            "Dead",
	            "Dealer",
	            "Death",
	            "Decadent",
	            "Deputy",
	            "Deserter",
	            "Despot",
	            "Dexter",
	            "Dinosaur",
	            "Diplomat",
	            "Diver",
	            "Docent",
	            "Dock",
	            "Doctor",
	            "Dogman",
	            "Dolittle",
	            "Dolphin",
	            "Doughnut",
	            "Drill",
	            "Drip",
	            "Driver",
	            "Dry",
	            "Dude",
	            "Dummy",
	            "Dumpling",
	            "Duremar",
	            "Dynamite",
	            "Eagle owl",
	            "Eared",
	            "Electrician",
	            "Epaulet",
	            "Factor",
	            "Falcon",
	            "Fang",
	            "Fantasist",
	            "Fat",
	            "Fatty",
	            "Favorite",
	            "Feather",
	            "Ferret",
	            "Fierce",
	            "Fireball",
	            "Fireman",
	            "Fist",
	            "Flamen",
	            "Flint",
	            "Footballer",
	            "Forester",
	            "Formidable",
	            "Fragged",
	            "Frame",
	            "Frantic",
	            "Friday",
	            "Gadfly",
	            "Gaffer",
	            "Gagster",
	            "Gangrene",
	            "Garlic",
	            "Genghis",
	            "Gentile",
	            "Geologist",
	            "Ghoul",
	            "Gioconda",
	            "Gladiator",
	            "Glider",
	            "Glitch",
	            "Globe",
	            "Gloomy",
	            "Glue",
	            "Glutton",
	            "Gnat",
	            "Goblin",
	            "Godfather",
	            "Goose",
	            "Gopher",
	            "Gorynich",
	            "Grabber",
	            "Grad",
	            "Gramps",
	            "Graph",
	            "Grasshopper",
	            "Grater",
	            "Gravedigger",
	            "Gray",
	            "Green",
	            "Gremlin",
	            "Grenade",
	            "Grinder",
	            "Grouse",
	            "Grudge",
	            "Guest",
	            "Gunpowder",
	            "Haggler",
	            "Handicapped",
	            "Hangman",
	            "Hare",
	            "Hatrack",
	            "Head",
	            "Healer",
	            "Heartthrob",
	            "Hedgehog",
	            "Herdsman",
	            "Hero",
	            "Hero",
	            "Hipster",
	            "Hireling",
	            "Hoary",
	            "Hog",
	            "Holey",
	            "Homebrew",
	            "Homer",
	            "Hood",
	            "Hook",
	            "Horned owl",
	            "Horse",
	            "Hunchback",
	            "Hybrid",
	            "Icarus",
	            "Ice",
	            "Immortal",
	            "Important",
	            "Indian",
	            "Invincible",
	            "Iron",
	            "Ironstone",
	            "Isotope",
	            "Jack",
	            "Jackal",
	            "Jammy",
	            "Jerboa",
	            "Jiggers",
	            "Joint",
	            "Joker",
	            "Judge",
	            "Jumper",
	            "Kagor",
	            "Karma",
	            "Kaschey",
	            "Key",
	            "Killer",
	            "King",
	            "King",
	            "Knuckles",
	            "Kutuzov",
	            "Lamer",
	            "Lantern",
	            "Law",
	            "Lax",
	            "Leary",
	            "Legalist",
	            "Legionary",
	            "Lemming",
	            "Leshiy",
	            "Lifeguard",
	            "Little Man",
	            "Loader",
	            "Local",
	            "Lock",
	            "Locksmith",
	            "Long",
	            "Loony",
	            "Lord",
	            "Loser",
	            "Loyal",
	            "Luckster",
	            "Lucky",
	            "Machine",
	            "Madera",
	            "Maggots",
	            "Magnate",
	            "Major",
	            "Mammoth",
	            "Maniac",
	            "Marmot",
	            "Martian",
	            "Mason",
	            "Master",
	            "Mayhem",
	            "Mechanic",
	            "Merchant",
	            "Messenger",
	            "Microbe",
	            "Milkman",
	            "Minepicker",
	            "Miner",
	            "Moderator",
	            "Mohawk",
	            "Monster",
	            "Moor",
	            "Moose",
	            "Moped",
	            "Mosquito",
	            "Motor",
	            "Mountaineer",
	            "Muddy",
	            "Muscleman",
	            "Muskrat",
	            "Mutant",
	            "Nail",
	            "Napoleon",
	            "Narc",
	            "Narcissus",
	            "Native",
	            "Nelson",
	            "Neptune",
	            "Nerve",
	            "Nickel",
	            "Ninja",
	            "Nocturnal",
	            "Nothing",
	            "Nozzle",
	            "Oldtimer",
	            "One-eyed",
	            "Operator",
	            "Oscar",
	            "Outcast",
	            "Owl",
	            "Pacifist",
	            "Paddle",
	            "Padishah",
	            "Pagan",
	            "Painter",
	            "Panadol",
	            "Parachute",
	            "Paranoid",
	            "Parasite",
	            "Partisan",
	            "Pastor",
	            "Patron",
	            "Patsyuk",
	            "Pepper",
	            "Percher",
	            "Perp",
	            "Pert",
	            "Phantom",
	            "Pianist",
	            "Pietist",
	            "Pilgrim",
	            "Pilot",
	            "Pimp",
	            "Pinocchio",
	            "Pintle",
	            "Pioneer",
	            "Pipe",
	            "Piranha",
	            "Pirate",
	            "Piston",
	            "Pitman",
	            "Piton",
	            "Plasticine",
	            "Player",
	            "Plodder",
	            "Plowman",
	            "Poet",
	            "Polar explorer",
	            "Porter",
	            "Postman",
	            "Prick",
	            "Priest",
	            "Prince",
	            "Professor",
	            "Prometheus",
	            "Prophet",
	            "Proud",
	            "Pumpkin",
	            "Punk",
	            "Pusher",
	            "Pushkin",
	            "Quack",
	            "Quiet",
	            "Rabbi",
	            "Rabbit",
	            "Ragged",
	            "Rambo",
	            "Ranger",
	            "Rasta",
	            "Raven",
	            "Ravioli",
	            "Razor",
	            "Reactor",
	            "Red",
	            "Red-haired",
	            "Reverend",
	            "Rider",
	            "Righteous",
	            "Robin",
	            "Robot",
	            "Rocker",
	            "Rocket",
	            "Rogue",
	            "Romantic",
	            "Rook",
	            "Root",
	            "Roquefort",
	            "Rotten",
	            "Roundboy",
	            "Runner",
	            "Rusty",
	            "Ryazansky",
	            "Samurai",
	            "Sawn-off gun",
	            "Scholar",
	            "Scoop",
	            "Screw",
	            "Scythe",
	            "Secretary",
	            "Shaggy",
	            "Shaman",
	            "Sharp",
	            "Sharp-sighted",
	            "Sharptoothed",
	            "Shaved",
	            "Shell",
	            "Shepherd",
	            "Sheriff",
	            "Shipboy",
	            "Shrimp",
	            "Shtirlits",
	            "Sieve",
	            "Simpleton",
	            "Simulator",
	            "Siniy",
	            "Skeleton",
	            "Skiff",
	            "Skinny",
	            "Slasher",
	            "Smartass",
	            "Smoker",
	            "Snake",
	            "Sniper",
	            "Snot",
	            "Softy",
	            "Sorcerer",
	            "Spaghetti",
	            "Spam",
	            "Sparrow",
	            "Spider",
	            "Spinner",
	            "Spirit",
	            "Spliff",
	            "Spy",
	            "Squint-eyed",
	            "Stern",
	            "Stone",
	            "Stooped",
	            "Stout",
	            "Stranger",
	            "Student",
	            "Sultan",
	            "Surgeon",
	            "Susanin",
	            "SWAT officer",
	            "Tail",
	            "Tails",
	            "Tambourine",
	            "Tanker",
	            "Tarantula",
	            "Target",
	            "Teddy",
	            "Telegraphist",
	            "Terminator",
	            "Thief",
	            "Thorn",
	            "Thoughtful",
	            "Tie",
	            "Tightwad",
	            "Timesheet",
	            "Tiranas",
	            "Tobacco",
	            "Tourist",
	            "Trailblazer",
	            "Tramp",
	            "Transporter",
	            "Trap",
	            "Trasher",
	            "Traveler",
	            "Trick",
	            "Trigger",
	            "Truculent",
	            "Trump",
	            "Tuner",
	            "Turkey",
	            "Vacuum",
	            "Vagabond",
	            "Vandal",
	            "Ventilator",
	            "Viking",
	            "Visitor",
	            "Voland",
	            "Vulture",
	            "Warrior",
	            "Warrior",
	            "Whiner",
	            "Whip",
	            "Whirlpool",
	            "Whist",
                "White",
	            "Whiz",
	            "Wind",
	            "Witcher",
	            "Woodchuck",
	            "Woodpecker",
	            "Worm",
	            "Worm",
	            "Wrangler",
	            "Wrinkled",
	            "Yakker",
	            "Zmur",
            });
        }
    }
}
