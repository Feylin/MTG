using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public sealed class DAL_WordLists
    {
        private static readonly Lazy<DAL_WordLists> Lazy = new Lazy<DAL_WordLists>(() => new DAL_WordLists());

        public static DAL_WordLists Instance => Lazy.Value;

        private DAL_WordLists()
        {
        }

        public Dictionary<string, string> CardNotes()
        {
            Dictionary<string, string> cardNotes = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
            {
                { "ability", "Text on a permanent tells you its abilities. There are three kinds: activated, static, and triggered." },
                { "affinity", "A spell with affinity for something costs {1} less to play for each of that something you control. Affinity can't reduce colored mana costs, \nand it can't change a spell's mana cost or converted mana cost. It just changes how much mana you pay to play the spell." },
                { "attacks", "A creature attacks an opponent or a planeswalker controlled by an opponent during the declare attackers step." },
                { "aura-swap", "You may pay the aura-swap cost to exchange this Aura with an Aura card in your hand. The new Aura comes into play attached to the permanent this Aura is currently attached to." },
                { "battle-cry", "Whenever a creature with battle cry attacks, each other attacking creature geats +1/+0 until end of turn." },
                { "battlefield", "The play area shared by all players. All permanents exist on the battlefield" },
                { "blocks", "A creature blocks an opponent's attacking creature during the declare blockers step." },
                { "bushido", "A creature with bushido gets a bonus to power and toughness whenever it blocks or becomes blocked. The bushido number tells you how big this bonus is." },
                { "buyback", "Buyback is an additional optional cost. If you pay a spell's buyback cost, the spell is put into your hand instead of your graveyard as it resolves." },
                { "cascade", "When you play this spell, remove cards from the top of your library from the game until you remove a nonland card that costs less. \nYou may play it without paying its mana cost. Put the removed cards on the bottom in a random order." },
                { "champion", "When a permanent with champion comes into play under your control, sacrifice it unless you remove another permanent you control of the stated type or subtype from the game. \nWhen the permanent with champion leaves play, return the removed card to play under its owner's control." },
                { "changeling", "A permanent, spell or card with changeling is all creature types at the same time." },
                { "channel", "Channel is an ability word indicating that a card may be discarded for an effect." },
                { "clash", "To clash, each player involved in the clash reveals the top card of his or her library, then puts that card on the top or bottom of that library. \nIf a player revealed a card with higher converted mana cost than all other revealed cards, that player wins the clash." },
                { "conspire", "As you play this spell, you may tap two untapped creatures you control that share a color with it. When you do, copy it." },
                { "controller", "You control the creatures and other permanents that you have on your battlefield." },
                { "convoke", "Each creature you tap while playing a spell with convoke reduces its cost by 1 or by one mana of that creature's color." },
                { "cycling", "You can pay the cycling cost of a card in your hand and discard it to draw a card." },
                { "damage", "Damage dealt to a player causes him or her to lose that much life. Damage dealt to a creature heals at the end of the turn." },
                { "deathtouch", "Whenever a permanent with deathtouch deals damage to a creature, destroy that creature." },
                { "defender", "A creature with defender can't attack" },
                { "detain", "Until your next turn, a detained permanent can't attack or block and its activated abilities can't be activated." },
                { "devotion", "Your devotion to a color is the number of mana symbols of that color in the mana cost of permanents you control." },
                { "dies", "A creature 'dies' if it's put into the graveyard." },
                { "discard", "Cards that your discard are put into your graveyard from your hand." },
                { "domain", "It appears in italics at the beginning of an ability that counts the number basic land types among the lands you control." },
                { "double-strike", "A creature with double-strike, deals both first-strike and regular combat damage." },
                { "echo", "At the beginning of your upkeep, if a permanent with echo came under your control since the beginning of your last upkeep, sacrifice it unless you pay its echo cost." },
                { "draw", "To draw a card is to take the top card of your library and put it into your hand." },
                { "enchant", "An Aura's enchant ability tells you what the Aura can be attached to. When you cast an Aura, you must target something it can enchant and when it resolves it will enter the battlefield attached to the target." },
                { "entwine", "Normally, you can choose only one of a spell's abilities listed after 'Choose one ..'. But if you pay the entwine cost, you get all the abilities." },
                { "equip", "An equipment comes into play just like any other artifact.. It doesn't equip anything until you play its equip ability. \nYou can pay your equipment's equip ability to move it onto a target creature you control. Even if the equipment is already on a creature." },
                { "evoke", "You may play a card with evoke for its evoke instead of its mana cost. If you do, it's sacrificed when it comes into play." },
                { "evolve", "Whenever a creature enters the battlefield under your control, if that creature has greater power or toughness than this creature, put a +1/+1 counter on this creature." },
                { "exalted", "Whenever a creature you control attacks alone, that creature gets +1/+1 until end of turn." },
                { "exile", "If a card is exiled, that card is set aside from the rest of the game in the exile zone. Exiled cards are normally faced up." },
                { "fading", "This permanent comes into play with N fade counters on it and At the beginning of your upkeep, remove a fade counter from this permanent. If you can't, sacrifice the permanent." },
                { "fear", "A creature with fear can't be blocked except by artifact creatures and/or black creatures." },
                { "first-strike", "Creatures with first strike deal their damage before other creatures. Creatures without first strike get to deal their damage afterwards only if they survived the first strike damage." },
                { "flanking", "Whenever a creature without flanking blocks this creature, the blocking creature gets -1/-1 until end of turn." },
                { "flash", "You may play a card with flash any time you can play an instant." },
                { "flashback", "You can play a card with flashback once from your graveyard as though it was in your hand. Instead of paying its mana cost, you'll pay its flashback cost. \nWhen the spell resolves or is countered, it's removed from the game." },
                { "flying", "A creature with flying can be blocked only by other creatures with flying, but a creature with flying can block both creatures with flying and creatures without flying." },
                { "fuse", "You may cast one or both halves of this card from your hand." },
                { "graft", "A creature with graft comes into play with a certain +1/+1 counters on it. Whenever a new creature comes into play, \nyou may move a +1/+1 counter from the creature with graft onto the new creature." },
                { "graveyard", "Your discard pile. Permanents go to the graveyard when they are destroyed or sacrificed. Instants and sorceries go to the graveyard after they resolve." },
                { "hand", "Cards your draw are moved from the top of your library into your hand. Each player has his or her own hand." },
                { "haste", "A creature with haste can attack or use tap abilities even it it hasn't been under your control since the beginning of your turn." },
                { "haunt", "When a creature with haunt is put into a graveyard from play, or when an instant or sorcery with haunt resolves, the card is removed from the game haunting target creature." },
                { "hellbent", "An ability word that indicates that a card is better if its controller has no cards in his or her hand." },
                { "heroic", "A creature with heroic gives the controlling player powerful benefits every time he casts a spell that targets it." },
                { "hexproof", "A permanent or player with hexproof can't be target of a spell or ability controlled by an opponent." },
                { "indestructible", "Effects that should 'Destroy' don't destroy an indestructible permanent, A indestructible permanent can't be destroyed by damage." },
                { "infect", "Something with infect deals damage to creatures in the form of -1/-1 counters and to players in the form of poison counters" },
                { "inspired", "Whenever an inspired creature becomes untapped, it does something." },
                { "intimidate", "A creature with intimidate can't be blocked except by artifact creatures and/or creature that share a color with it." },
                { "kicker", "Kicker is an optional card ability. When you play a kicker spell from your hand, you may decide to pay the kicker cost by selecting the 'play with kicker' option." },
                { "land", "Only lands with the word 'basic' on the type line are basic lands. Other lands are nonbasic" },
                { "landcycling", "You can play the basic landcycling cost of a card in your hand and discard it to search your library for a basic land and put it into your hand." },
                { "landfall", "A landfall ability triggers whenever a land enters the battlefield under your control for any reason." },
                { "library", "Your deck becomes your library at the beginning of the game. It's kept face down. Each player has his or her own library." },
                { "lifelink", "Whenever a permanent with lifelink deals damage, its controller gains that much life." },
                { "metalcraft", "Metalcraft gives a bonus to a permanent or a spell if its controller also controls three or more artifacts." },
                { "madness", "Madness is a keyword ability on spells that allows a player to play that spell for an alternate cost if it is discarded." },
                { "modular", "A creature with modular comes into play with a number of +1/+1 counters on it. When the creature is put into a graveyard from play, \nyou may put its +1/+1 counters onto a target artifact creature." },
                { "monstrosity", "If a creature isn't monstrous, monstrosity X puts X +1/+1 counters on it and it becomes monstrous." },
                { "morph", "You may play a creature card with morph face down for three mana. A face-down creature is 2/2 with no text, no name, no color, no creature type, and a mana cost of 0. \nYou can turn it face up any time by paying its morph cost." },
                { "multikicker", "You may pay a spell's multikicker cost any number of times you can cast it." },
                { "nonblack", "Something that is black, isn't nonblack, even if it's other colors as well." },
                { "owner", "You are the owner of each card that started the game in your deck. A card's owner can't change during the game." },
                { "permanent", "A permanent is a card or token on the battlefield." },
                { "persist", "When this creature is put into a graveyard from play, if it had no -1/-1 counters on it, return it to play under its owner's control with a -1/-1 counter on it." },
                { "power", "The number found before the slash in the lower right, power is how much damage a creature deals in combat." },
                { "prevent", "Damage that is prevented is never dealt." },
                { "protection", "Protection comprises several rules. A creature with protection from red, for example, can't be blocked by red creatures. Any damage dealt to it by red sources is prevented. \nIt can't be targeted by red spells or abilities from red sources. Any red enchantments on the creature will go to their owner's graveyard." },
                { "prowl", "You may play this card by paying it's Prowl cost instead of it's mana cost if a player was dealt combat damage this turn by a source that, \nat the time it dealt that damage, was under your control and had any of this spell's creature types." },
                { "rampage", "Rampage N means 'Whenever this creature becomes blocked, it gets +N/+N until end of turn for each creature blocking it beyond the first.'" },
                { "reach", "A creature with reach can block both creatures with flying and creatures without flying." },
                { "regenerate", "When you regenerate a permanent, you keep it from getting destroyed. Insted of being destroyed, all damage from it is removed, it becomes tapped and (if it's in combat), it's removed from combat." },
                { "return", "A spell or ability can 'return' a card or other permanents to a different zone, even if it was never in that zone." },
                { "sacrifice", "When you sacrifice a permanent, it's put into its owner's graveyard. You can only sacrifice permanents you control." },
                { "scavenge", "Pay this card's scavenge cost and exile it from your graveyard: Put a number of +1/+1 counters equal to this card's power on target creature. \nScavenge only as a sorcery." },
                { "scry", "Scry N means to look at the top N cards of your library, put any number of them on the bottom of your library in any order, and put the rest on top of your library in any order." },
                { "search", "When searching a hidden zone for cards with a certain quality like ('basic land card'), you can choose not to find any card." },
                { "shadow", "A creature with shadow can block or be blocked only by creatures with shadow." },
                { "shroud", "A permanent or player with shroud can't be target of spells or abilities." },
                { "shuffle", "If your library is shuffled, its order will be randomly rearranged." },
                { "spell", "All types of cards except lands are spells when you cast them." },
                { "split-second", "As long as a spell with split second is on the stack, players can't play spells or activated abilities that aren't mana abilities." },
                { "storm", "When you play a spell with storm, you put another copy of it onto the stack before each spell played before it that turn. You may choose new targets for the copies." },
                { "sunburst", "An artifact with sunburst comes into play with a number of counters on it equals to the numbers of colors used to pay its cost. \nCreature spells get +1/+1 counters and noncreature spells get charge counters." },
                { "suspend", "Rather than play a card with suspend from your hand, you may pay its suspend cost and remove it from the game with the indicated number of time counters on it. At the beginning of each of your upkeeps, \nyou remove a timer counter from it. When you remove the last time counter, you play it without paying its mana cost if able. If it's a creature, it comes into play with haste." },
                { "swampwalk", "A creature with swampwalk can't be blocked if the defending player controls a Swamp." },
                { "tap", "A permanent taps (turns sideways) to show it has been used. Creatures tap when they attack or use an ability with 'T' in their cost." },
                { "target", "When you cast a spell that uses the word 'target', you choose what the spell will affect when you cast it. The same is true for abilities you activate. " },
                { "threshold", "Threshold abilities only work when you have seven or more cards in your graveyard." },
                { "toughness", "The number found after the slash in the lower right, toughness is how much damage must be dealt to a creature in a single turn to destroy it." },
                { "trample", "If a creature, with trample, would assign enough damage to its blockers to destroy them, you may have it assign the rest of its damage to defending player or planeswalker." },
                { "transform", "When a double-faced card transforms, it's turned to it's other face. Only double-faced cards can transform." },
                { "unearth", "You can pay the unearth cost of a card to return it from your graveyard to play. It gains haste. \nRemove it from the game at the end of your turn or it would leave play. Unearth only as a sorcery." },
                { "untap", "A permanent untaps (turns upright again) to show it's ready to being used again. Your permanents untap at the beginning of each of your turns." },
                { "untapped", "Each permanent is either tapped or untapped. Tapped permanents are turned sideways." },
                { "vigilance", "Attacking doen't cause a creature with vigilance to tap." }
            };

            return cardNotes;
        }

        public List<string> ColoredWordList()
        {
            var coloredWords = new List<string> { "ability", "affinity", "attacks", "auru-swap", "basic land", "battle-cry", "battlefield", "blocks", "bushido", "buyback", 
                "cascade", "champion", "changeling", "channel", "clash", "conspire", "controller", "convoke", "cycling", "damage", "deathtouch", "defender", "detain", "devotion", 
                "dies", "discard", "domain", "double-strike", "draw", "echo", "enchant", "entwine", "equip", "evoke", "evolve", "exalted", "exile", "fading", "fear", "first-strike", 
                "flanking", "flash", "flashback", "flying", "fuse", "graft", "graveyard", "hand", "haste", "haunt", "hellbent", "heroic", "hexproof", "indestructible", "infect", 
                "inspired", "intimidate", "kicker", "land", "landcycling", "landfall", "library", "lifelink", "madness", "metalcraft", "modular", "monstrosity", "morph", "multikicker", 
                "nonblack", "owner", "permanent", "persist", "power", "prevent", "protection", "prowl", "rampage", "reach", "regenerate", "return", "sacrifice", "scavenge", "scry", 
                "search", "shadow", "shroud", "shuffle", "spell", "split-second", "storm", "sunburst", "suspend", "swampwalk", "tap", "target", "threshold", "toughness", "trample", 
                "transform", "unearth", "untap", "untapped", "vigilance" };

            return coloredWords;
        }

        public Dictionary<string, Color> ManaList()
        {
            Dictionary<string, Color> manaDictionary = new Dictionary<string, Color>
            {
                {"{R}", Color.Red},
                {"{B}", Color.Black},
                {"{W}", Color.FromArgb(100, 100, 100)},
                {"{U}", Color.Blue},
                {"{G}", Color.Green}
            };

            return manaDictionary;
        }

        public List<string> ItalicWordList()
        {
            var italicWords = new List<string> { "limited edition alpha", "limited edition beta", "unlimited edition", "revised edition", "fourth edition", 
                "fifth edition", "sixth edition", "seventh edition", "eighth edition", "ninth edition", "tenth edition", "magic 2010", "magic 2011", "magic 2012", 
                "magic 2013", "magic 2014", "magic 2015", "arabian nights", "antiquities", "legends", "the dark", "fallen empires", "homelands", "ice age", "alliances", 
                "coldsnap", "mirage", "visions", "weatherlight", "tempest", "stronghold", "exodus", "urza's saga", "urza's destiny", "urza's legacy", "mercadian masques", 
                "nemesis", "prophecy", "invasion", "apocalypse", "odyssey", "torment", "judgment", "onslaught", "legions", "scourge", "mirrodin", "darksteel", "fifth dawn", 
                "champions of kamigawa", "betrayers of kamigawa", "saviors of kamigawa", "ravnica: city of guilds", "guildpact", "dissension", "time spiral", "planar chaos", 
                "future sight", "lorwyn", "morningtide", "shadowmoor", "eventide", "shards of alara", "conflux", "alara reborn", "zendikar", "worldwake", "rise of the eldrazi", 
                "scars of mirrodin", "mirrodin besieged", "new phyrexia", "innistrad", "dark ascension", "avacyn restored", "return to ravnica", "gatecrash", "dragon's maze", 
                "theros", "born of the gods", "journey into nyx", "planeshift", "champions of kamigawa", "commander" };

            return italicWords;
        }

        public List<string> BoldWordList()
        {
            var boldWords = new List<string> { "official faq", "card of the day", "magic", "mtg encyclopedia", "card rules", 
                "card notes", "draft notes", "seventh edition card catalog", "shandalar short name" };

            foreach (var str in ItalicWordList())
            {
                //boldWords.Add(str.Insert(str.Length, " faq"));

                StringBuilder strb = new StringBuilder(str);
                boldWords.Add(strb.Append(" faq").ToString());
            }

            return boldWords;
        }
    }
}
