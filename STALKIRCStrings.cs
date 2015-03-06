﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STALK_IRC
{
    class STALKIRCStrings
    {
        static List<string> times = new List<string>();
        static List<string> observance = new List<string>();
        static List<string> suffixes = new List<string>();
        static List<string> genericDeaths = new List<string>();
        static List<string> fNames = new List<string>();
        static List<string> sNames = new List<string>();
        static Dictionary<string, string> levelNames = new Dictionary<string, string>();
        static Dictionary<string, string> deaths = new Dictionary<string, string>();
        static Random rand = new Random();

        static string RandomString(List<string> collection)
        {
            return collection[rand.Next(collection.Count - 1)];
        }

        public static string BuildSentence(string name, string classType, string levelName)
        {
            string levelText = (levelNames.ContainsKey(levelName) ? levelNames[levelName] : "");
            classType = classType.ToUpper();
            if (classType == "DARK_STALKER" && rand.Next(1000) == 666) // :^)
                return "Psssh... nothin personnel... kid...";
            string deathText = (deaths.ContainsKey(classType) ? deaths[classType] : (RandomString(genericDeaths) + "(" + classType + ") "));
            return RandomString(times) + levelText + RandomString(observance) + name.Replace('_', ' ') + deathText + RandomString(suffixes);
        }

        public static string GenerateName()
        {
            return RandomString(fNames) + "_" + RandomString(sNames);
        }

        public static void Populate()
        {
            times.AddRange(new string[]{
                "Recently ",
                "A few minutes ago ",
                "Just now ",
            });

            // Shadow of Chernobyl
            levelNames["l01_escape"] = "at the Cordon ";
            levelNames["l02_garbage"] = "at the Garbage ";
            levelNames["l03_agroprom"] = "at Agroprom ";
            levelNames["l03u_agr_underground"] = "in Agroprom Underground ";
            levelNames["l04_darkvalley"] = "in Dark Valley ";
            levelNames["l04u_labx18"] = "in Lab X-18 ";
            levelNames["l05_bar"] = "at the Bar ";
            levelNames["l06_rostok"] = "at Wild Territory ";
            levelNames["l07_military"] = "at the Army Warehouses ";
            levelNames["l08_yantar"] = "at Yantar ";
            levelNames["l08u_brainlab"] = "in Lab X-16 ";
            levelNames["l10_radar"] = "in Red Forest ";
            levelNames["l10u_bunker"] = "in the Brain Scorcher ";
            levelNames["l11_pripyat"] = "in Eastern Pripyat ";
            levelNames["l12_stancia"] = "outside the CNPP ";
            levelNames["l12_stancia_2"] = levelNames["l12_stancia"];
            levelNames["l12u_control_monolith"] = "inside the CNPP ";
            levelNames["l12u_sarcofag"] = levelNames["l12u_control_monolith"];
            // Clear Sky
            levelNames["agroprom"] = levelNames["l03_agroprom"];
            levelNames["agroprom_underground"] = levelNames["l03u_agr_underground"];
            levelNames["darkvalley"] = levelNames["l04_darkvalley"];
            levelNames["escape"] = levelNames["l01_escape"];
            levelNames["garbage"] = levelNames["l02_garbage"];
            levelNames["hospital"] = "in the abandoned Hospital ";
            levelNames["limansk"] = "in Limansk ";
            levelNames["marsh"] = "in the Swamps ";
            levelNames["military"] = levelNames["l07_military"];
            levelNames["red_forest"] = levelNames["l10_radar"];
            levelNames["stancia_2"] = levelNames["l12_stancia"];
            levelNames["yantar"] = levelNames["l08_yantar"];
            // Call of Pripyat
            levelNames["jupiter"] = "around Jupiter ";
            levelNames["jupiter_underground"] = "in the Pripyat Underpass ";
            levelNames["labx8"] = "in Lab X-8 ";
            levelNames["pripyat"] = "in Western Pripyat ";
            levelNames["zaton"] = "at Zaton ";

            observance.AddRange(new string[]{
                "I saw ",
                "I witnessed ",
                "I noticed ",
                "it seems ",
                "someone told me ",
                "word is ",
            });

            deaths["O_ACTOR"] = " committed suicide. ";
            deaths["S_ACTOR"] = deaths["O_ACTOR"];
            deaths["AI_STL_S"] = " was gunned down by a stalker. ";
            deaths["AI_STL"] = deaths["AI_STL_S"];
            deaths["SM_BLOOD"] = " was sucked dry by a bloodsucker. ";
            deaths["SM_BOARW"] = " was goared by a boar. ";
            deaths["SM_BURER"] = " met their fate at the hands of a burer. ";
            deaths["SM_CAT_S"] = " was scratched to death a cat. ";
            deaths["SM_CHIMS"] = " was leapt on by a chimera. ";
            deaths["SM_CONTR"] = " was zombified by a controller. ";
            deaths["SM_DOG_S"] = " was mauled by a blind dog. ";
            deaths["SM_FLESH"] = " somehow died to a pig. ";
            deaths["SM_IZLOM"] = " was smashed by an izlom. ";
            deaths["SM_GIANT"] = " was crushed by a pseudogiant. ";
            deaths["AI_PHANT"] = " succumbed to brain damage. ";
            deaths["SM_POLTR"] = " was killed by some invisible thing. ";
            deaths["SM_P_DOG"] = " was hunted down by a pseudodog. ";
            deaths["SM_DOG_P"] = " was overwhelmed by a psy dog. ";
            deaths["SM_DOG_F"] = deaths["SM_DOG_P"];
            deaths["AI_RAT"] = " was eaten alive by rats. ";
            deaths["SM_SNORK"] = " was kicked to death by a snork. ";
            deaths["SM_TUSHK"] = " was gnawed to the bone by a tushkano. ";
            deaths["SM_ZOMBI"] = " had their brains eaten by a zombie. ";
            deaths["C_HLCP_S"] = " was shot down by a helicopter. ";
            deaths["SCRPTCAR"] = " became roadkill. ";
            deaths["ZS_MBALD"] = " walked right into an anomaly. ";
            deaths["ZS_GALAN"] = " was sucked into a vortex. ";
            deaths["ZS_MINCE"] = deaths["ZS_GALAN"];
            deaths["ZS_RADIO"] = " stepped into an anomalous field. ";
            deaths["ZS_TORRD"] = " couldn't outrun a comet. ";
            deaths["ZS_BFUZZ"] = " got tangled up in a burnt fuzz. ";	
            deaths["Z_MBALD"] = " triggered a land mine. ";
            deaths["Z_RADIO"] = deaths["ZS_RADIO"];
            //deaths["Z_ZONE"] = "cse_alife_anomalous_zone";
            deaths["Z_CFIRE"] = " tripped and fell into a campfire. ";
            //deaths["Z_NOGRAV"] = "cse_alife_anomalous_zone";
            //deaths["Z_TORRID"] = "cse_alife_torrid_zone";
            //deaths["Z_RUSTYH"] = "cse_alife_zone_visual";
            //deaths["Z_AMEBA"] = "cse_alife_zone_visual";
            deaths["S_EXPLO"] = " was blown apart by an explosion. ";
            deaths["II_EXPLO"] = deaths["S_EXPLO"];

            deaths["STALKER"] = " was gunned down by a loner. ";
            deaths["BANDIT"] = " was looted by a bandit. ";
            deaths["DOLG"] = " was removed by a Dutyer. ";
            deaths["FREEDOM"] = " was blazed by a Freedomer. ";
            deaths["ECOLOG"] = " was blinded by science. ";
            deaths["KILLER"] = " was taken out by a mercenary. ";
            deaths["MILITARY"] = " was shot down by the military. ";
            deaths["MONOLITH"] = " was purged by a cultist. ";
            deaths["ZOMBIED"] = " zombie. ";
            deaths["ARENA_ENEMY"] = " lost in the Arena. ";
            deaths["STRANGER"] = deaths["STALKER"];
            deaths["CSKY"] = " was killed by the best faction. ";
            deaths["RENEGADE"] = " was killed by some bandit wannabe. ";
            deaths["ARMY"] = deaths["MILITARY"];
            deaths["DARK_STALKER"] = " was killed by a deformed stalker. ";

            // AMK
            deaths["TURRETMG"] = " went down in a hail of bullets. ";

            genericDeaths.AddRange(new string[]{
                " was killed. ",
                " was killed by something strange. ",
                " died of unknown causes. ",
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
            });

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
	            "Big_Fellow",
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
	            "Eagle_owl",
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
	            "Horned_owl",
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
	            "Little_Man",
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
	            "Polar_explorer",
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
	            "Sawn-off_gun",
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
	            "SWAT_officer",
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