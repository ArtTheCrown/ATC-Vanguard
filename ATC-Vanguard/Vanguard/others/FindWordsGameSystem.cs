using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATC_Vanguard.Vanguard.others
{
    public class FindWordsGameSystem
    {
        public string Result { get; set; }
        public List<string> ValidWords { get; set; }

        private static readonly List<string> EnglishWords = new List<string>
        {
            "able", "about", "above", "accept", "accident", "account", "accurate", "across", "act",
            "action", "active", "actor", "actual", "add", "address", "admire", "admit", "adopt", "adult",
            "adventure", "advertise", "advise", "affair", "affect", "afford", "afraid", "after", "again", "against",
            "age", "agency", "agent", "agree", "ahead", "aim", "air", "airplane", "airport", "alarm",
            "album", "alcohol", "alive", "all", "allow", "almost", "alone", "along", "already", "also",
            "although", "always", "amazing", "among", "amount", "amuse", "analyst", "ancient", "anger", "angle",
            "angry", "animal", "announce", "annual", "another", "answer", "any", "anybody", "anyone", "anything",
            "anyway", "anywhere", "apart", "apartment", "apologize", "appeal", "appear", "apple", "application", "apply",
            "appoint", "approve", "area", "argue", "arm", "around", "arrange", "arrest", "arrive", "art",
            "article", "artist", "ashamed", "aside", "ask", "asleep", "aspect", "assistant", "associate",
            "assume", "athlete", "atmosphere", "attach", "attack", "attempt", "attend", "attention", "attitude",
            "attract", "audience", "author", "authority", "auto", "available", "average", "avoid", "award", "aware",
            "away", "awful", "baby", "back", "bad", "bag", "balance", "ball", "band", "bank",
            "bar", "base", "baseball", "basic", "basket", "basketball", "bath", "beach", "bean",
            "bear", "beat", "beautiful", "because", "become", "bed", "bedroom", "beer","bee","bees","bud", "before", "begin",
            "behavior", "behind", "believe", "bell", "belong", "below", "belt", "benefit", "beside", "best",
            "bet", "better", "between", "beyond", "big", "bill", "bind", "bird", "birth", "bit",
            "bite", "black", "blade", "blame", "blank", "blind", "block", "blood", "blow", "blue",
            "board", "boat", "body", "bone", "book", "boot", "border", "born", "borrow", "both",
            "bother", "bottle", "bottom", "bowl", "box", "boy", "brain", "branch", "brave", "bread",
            "break", "breakfast", "breath", "brick", "bridge", "brief", "bright", "bring", "broad", "brother",
            "brown", "brush", "build", "building", "bump", "burn", "bus", "business", "busy", "but",
            "butter", "button", "buy", "by", "cabinet", "cable", "cake", "calculate", "call", "camera",
            "camp", "campaign", "campus", "can", "cancel", "cancer", "candidate", "cap", "capital", "car",
            "card", "care", "career", "careful", "careless", "carry", "case", "cash", "cast", "cat",
            "catch", "category", "cause", "ceiling", "celebrate", "cell", "center", "century", "certain", "chair",
            "challenge", "chance", "change", "channel", "chapter", "character", "charge", "chart", "cheap", "check",
            "cheese", "chef", "chemical", "chest", "chicken", "chief", "child", "childhood", "chocolate", "choice",
            "choose", "church", "circle", "circumstance", "citizen", "city", "civil", "claim", "class", "classic",
            "classroom", "clean", "clear", "clerk", "click", "client", "climate", "climb", "clock", "close",
            "closet", "cloth", "clothes", "cloud", "club", "clue", "coach", "coal", "coast", "coat",
            "coffee", "coin", "cold", "collapse", "collar", "collect", "college", "color", "combine", "come",
            "comfort", "comfortable", "command", "comment", "commercial", "commission", "commit", "committee", "common", "communicate",
            "communication", "community", "company", "compare", "competition", "complain", "complete", "complex", "complicate", "component",
            "compose", "composition", "computer", "concern", "concert", "conclude", "conclusion", "condition", "conference", "confidence",
            "confident", "conflict", "confuse", "congress", "connect", "connection", "conscious", "consider", "constant", "construction","construct",
            "consultant", "consume", "contact", "contain", "content", "contest", "context", "continue", "contract", "contrast",
            "contribute", "control", "conversation", "convert", "cook", "cool", "cooperate", "copy", "corner", "correct",
            "cost", "cotton", "couch", "could", "council", "count", "counter", "country", "county", "couple",
            "courage", "course", "court", "cover", "cow", "crash", "crazy", "cream", "create", "creative",
            "creature", "credit", "crew", "crime", "criminal", "crisis", "cross", "cry", "culture", "cup",
            "curious", "current", "curve", "custom", "customer", "cut", "cycle", "dad", "damage", "dance",
            "danger", "dangerous", "dark", "data", "date", "daughter", "day", "dead", "deal", "dealer",
            "dear", "death", "debate", "debt", "decade", "decide", "decision", "deep", "defeat", "defend",
            "defense", "definitely", "degree", "delay", "deliver", "delivery", "demand", "department", "depend", "dependent",
            "deposit", "depth", "describe", "description", "design", "designer", "desire", "desk", "detail", "determine",
            "develop", "development", "device", "devil", "die", "diet", "difference", "different", "difficult", "difficulty",
            "dig", "dimension", "dinner", "direct", "direction", "director", "dirt", "dirty", "disagree", "disappear",
            "disaster", "discuss", "discussion", "disease", "dish", "dismiss", "distance", "distant", "distribute", "district",
            "divide", "division", "divorce", "do", "doctor", "document", "dog", "doll", "dollar", "door",
            "dot", "double", "doubt", "down", "dozen", "draft", "drag", "drama", "draw", "drawer",
            "dream", "dress", "drink", "drive", "driver", "drop", "drug", "dry", "due", "during",
            "dust", "duty", "each", "ear", "early", "earn", "earth", "ease", "east", "easy",
            "eat", "economic", "economy", "edge", "edition", "editor", "education", "educational", "effect", "effective",
            "effort", "egg", "either", "elderly", "elect", "election", "electric", "electricity", "electronic", "element",
            "elevator", "else", "elsewhere", "emergency", "emotion", "emotional", "emphasis", "employ", "employee", "employer",
            "employment", "empty", "enable", "encourage", "end", "enemy", "energy", "engine", "engineer", "engineering",
            "enjoy", "enormous", "enough", "ensure", "enter", "enterprise", "entertain", "entertainment", "entire", "entrance",
            "entry", "environment", "equal", "equipment", "equivalent", "error", "escape", "especially", "essay", "essential",
            "establish", "estate", "estimate", "even", "evening", "event",  "establish", "estate", "estimate", "even", "evening", "event", "eventually", "ever", "every", "everybody",
            "everyone", "everything", "everywhere", "evidence", "evil", "exact", "exactly", "example", "excellent", "except",
            "exchange", "excite", "excuse", "exercise", "exist", "existence", "exit", "expand", "expect", "expense",
            "expensive", "experience", "expert", "explain", "explanation", "explore", "explosion", "export", "express", "extend",
            "extension", "extensive", "extent", "extra", "extreme", "eye", "face", "facility", "fact", "factor",
            "factory", "fail", "failure", "fair", "fairly", "faith", "fall", "false", "familiar", "family",
            "famous", "fan", "fantasy", "far", "farm", "farmer", "fashion", "fast", "fat", "fate",
            "father", "fault", "favor", "favorite", "fear", "feature", "federal", "fee", "feed", "feel",
            "feeling", "fellow", "female", "fence", "few", "fiber", "fiction", "field", "fifteen", "fifth",
            "fifty", "fight", "figure", "file", "fill", "film", "final", "finally", "finance", "financial",
            "find", "finding", "fine", "finger", "finish", "fire", "firm", "first", "fish", "fit",
            "fix", "flag", "flame", "flat", "flavor", "flee", "flight", "float", "floor", "flow",
            "flower", "fly", "focus", "fold", "folk", "follow", "food", "foot", "football", "for",
            "force", "foreign", "forest", "forever", "forget", "forgive", "fork", "form", "formal", "formation",
            "former", "formula", "forth", "fortune", "forward", "found", "foundation", "four", "fourth", "frame",
            "free", "freedom", "freeze", "French", "frequency", "frequent", "fresh", "friend", "friendly", "friendship",
            "from", "front", "fruit", "fuel", "full", "fully", "fun", "function", "fund", "fundamental",
            "funeral", "funny", "furniture", "further", "future", "gain", "game", "gap", "garage", "garden",
            "gas", "gate", "gather", "gay", "gear", "gender", "gene", "general", "generally", "generate",
            "generation", "genius", "gentleman", "genuine", "German", "gesture", "get", "ghost", "giant", "gift",
            "gifted", "girl", "give", "glad", "glance", "glass", "global", "glove", "goal",
            "god", "gold", "golden", "golf", "good", "government", "governor", "grab", "grade", "gradually",
            "grain", "grand", "grandfather", "grandmother", "grant", "grass", "grave", "gray", "great", "greatest",
            "green", "grocery", "ground", "group", "grow", "growing", "growth","gum", "guarantee", "guard", "guess",
            "guest", "guide", "guideline", "guilty", "gun", "guy", "habit", "habitat", "hair", "half",
            "hall", "hand", "handle", "hang", "happen", "happy", "hard", "hardly", "hat", "hate",
            "have", "head", "headline", "headquarters", "health", "healthy", "hear", "hearing", "heart",
            "heat", "heaven", "heavy", "heel", "height", "hello", "help", "helpful", "her", "here",
            "heritage", "hero", "herself", "hey", "hide", "high", "highlight", "highway", "hill",
            "him", "himself", "hip", "hire", "his", "historian", "historical", "history", "hit", "hold",
            "hole", "holiday", "holy", "home", "homework", "honest", "honey", "honor", "hope", "hopefully",
            "horizon", "horror", "horse", "hospital", "host", "hot", "hotel", "hour", "house", "household",
            "housing", "how", "however", "huge", "human", "humor", "hundred", "hungry", "hunt", "hunter",
            "hunting", "hurt", "husband", "hypothesis", "ice", "idea", "ideal", "ignore",
            "ill", "illegal", "illness", "illustrate", "image", "imagination", "imagine", "immediate", "immediately", "immigrant",
            "impact", "implement", "implication", "imply", "importance", "important", "impose", "impossible", "impress", "impression",
            "impressive", "improve", "improvement", "incentive", "incident", "include", "including", "income", "incorporate",
            "increase", "increasingly", "incredible", "indeed", "independence", "independent", "index", "indicate", "indication", "individual",
            "industrial", "industry", "infant", "infection", "inflation", "influence", "inform", "information", "ingredient", "initial",
            "initially", "initiative", "injure", "injury", "inner", "innocent", "inquiry", "inside", "insight", "insist",
            "inspire", "install", "instance", "instead", "institution", "institutional", "instruction", "instructor", "instrument", "insurance",
            "intellectual", "intelligence", "intelligent", "intend", "intense", "intensity", "intention", "interaction", "interest", "interested",
            "interesting", "internal", "international", "internet", "ink","inking", "interpret", "interpretation", "intervention", "interview", "into", "introduce",
            "introduction", "invest", "investigate", "investigation", "investment", "investor", "invite", "involve", "involvement", "Iraqi",
            "Irish", "iron", "Islamic", "island", "Israeli", "issue", "Italian", "item", "its",
            "itself", "jacket", "jail", "Japanese", "jet", "Jew", "Jewish", "job", "join", "joint",
            "joke", "journal", "journalist", "journey", "joy", "judge", "judgment", "juice", "jump", "junior",
            "jury", "just", "justice", "justify", "keep", "key", "kick", "kid", "kill", "killer",
            "killing", "kind", "king", "kiss", "kitchen", "knee", "knife", "knock", "know", "knowledge",
            "lab", "label", "labor", "laboratory", "lack", "lady", "lake", "land", "landscape", "language",
            "lap", "large", "largely", "last", "late", "later", "Latin", "latter", "laugh", "launch",
            "law", "lawn", "lawsuit", "lawyer", "lay", "layer", "lead", "leader", "leadership", "leading",
            "leaf", "league", "lean", "learn", "learning", "least", "leather", "leave", "left", "leg",
            "legacy", "legal", "legend", "legislation", "legitimate", "lemon", "length", "less", "lesson", "let",
            "letter", "level", "liberal", "library", "lid", "license", "lie", "life", "lifestyle", "lifetime", "lift",
            "light", "like", "likely", "limit", "limitation", "limited", "line", "link", "lip", "list",
            "listen", "literally", "literary", "literature", "little", "live", "living", "load", "loan", "local",
            "locate", "location", "lock", "long", "look", "loose", "lose", "loss", "lost", "lot",
            "lots", "loud", "love", "lovely", "lover", "low", "lower", "luck",     "lots", "loud", "love", "lovely", "lover", "low", "lower", "luck", "lucky", "lunch",
            "lung", "machine", "mad", "magazine", "mail", "main", "mainly", "maintain", "maintenance", "major",
            "majority", "make", "maker", "makeup", "male", "mall", "man", "manage", "management", "manager",
            "manner", "manufacturer", "manufacturing", "many", "map", "margin", "mark", "market", "marketing", "marriage",
            "married", "marry", "mask", "mass", "massive", "master", "match", "mate", "material", "math",
            "matter", "maximum", "may", "maybe", "mayor", "meal", "mean", "meaning", "meanwhile",
            "measure", "measurement", "meat", "mechanism", "media", "medical", "medicine", "medium", "meet", "meeting",
            "member", "membership", "memory", "mental", "mention", "menu", "mere", "merely", "mess", "message",
            "metal", "meter", "method", "Mexican", "middle", "might", "mild", "mile", "military", "milk",
            "mill", "million", "mind", "mine", "minister", "minor", "minority", "minute", "mirror", "miss",
            "missile", "mission", "mistake", "mix", "mixture", "mm-hmm", "mode", "model", "moderate", "modern",
            "modest", "mom", "moment", "money", "monitor", "month", "mood", "moon", "moral", "more",
            "moreover", "morning", "mortgage", "most", "mostly", "mother", "motion", "motivation", "motor", "mountain",
            "mouse", "mouth", "move", "movement", "movie", "much", "multiple",
            "murder", "muscle", "museum", "music", "musical", "musician", "Muslim", "must", "my", "myself",
            "mystery", "myth", "naked", "name", "narrative", "narrow", "nation", "national", "native", "natural",
            "naturally", "nature", "near", "nearby", "nearly", "necessarily", "necessary", "neck", "need", "negative",
            "negotiate", "negotiation", "neighbor", "neighborhood", "neither", "nerve", "nervous", "net", "network", "never",
            "nevertheless", "new", "newly", "news", "newspaper", "next", "nice", "night", "nine", "no",
            "nobody", "nod", "noise", "nomination", "none", "nonetheless", "nor", "normal", "normally", "north",
            "northern", "nose", "not", "note", "nothing", "notice", "notion", "novel", "now", "nowhere",
            "nuclear", "number", "numerous", "nurse", "nut", "object", "objective", "obligation", "observation", "observe",
            "observer", "obtain", "obvious", "obviously", "occasion", "occasionally", "occupation", "occupy", "occur", "ocean",
            "odd", "odds", "of", "off", "offense", "offensive", "offer", "office", "officer", "official",
            "often", "oil", "okay", "old", "Olympic", "once", "one", "ongoing",
            "onion", "online", "only", "onto", "open", "opening", "operate", "operating", "operation", "operator",
            "opinion", "opponent", "opportunity", "oppose", "opposite", "opposition", "option", "orange", "order",
            "ordinary", "organic", "organization", "organize", "orientation", "origin", "original", "originally", "other", "others",
            "otherwise", "ought", "our", "ourselves", "out", "outcome", "outside", "oven", "over", "overall",
            "overcome", "overlook", "owe", "own", "owner", "pace","pen", "pack", "package", "page", "pain",
            "painful", "paint", "painter", "painting", "pair", "pale", "Palestinian", "palm", "pan", "panel",
            "pant", "paper", "parent", "park", "parking", "part", "participant", "participate", "participation", "particular",
            "particularly", "partly", "partner", "partnership", "party", "pass", "passage", "passenger", "passion", "past",
            "patch", "path", "patient", "pattern", "pause", "pay", "payment", "PC", "peace", "peak",
            "peer", "penalty", "people", "pepper", "per", "perceive", "percentage", "perception", "perfect", "perfectly",
            "perform", "performance", "perhaps", "period", "permanent", "permission", "permit", "person", "personal", "personality",
            "personally", "personnel", "perspective", "persuade", "pet", "phase", "phenomenon", "philosophy", "phone", "photo",
            "photograph", "photographer", "phrase", "physical", "physically", "physician", "piano", "pick", "picture", "pie",
            "piece", "pile", "pilot", "pine", "pink", "pipe", "pitch", "place", "plan", "plane",
            "planet", "planning", "plant", "plastic", "plate", "platform", "play", "player", "please", "pleasure",
            "plenty", "plot", "plus", "pocket", "poem", "poet", "poetry", "point", "pole",
            "police", "policy", "political", "politically", "politician", "politics", "poll", "pollution", "pool", "poor",
            "pop", "popular", "population", "porch", "port", "portion", "portrait", "portray", "pose", "position",
            "positive", "possess", "possibility", "possible", "possibly", "post", "pot", "potato", "potential", "potentially",
            "pound", "pour", "poverty", "powder", "power", "powerful", "practical", "practice", "pray", "prayer",
            "precisely", "predict", "prefer", "preference", "pregnancy", "pregnant", "preparation", "prepare", "prescription", "presence",
            "present", "presentation", "preserve", "president", "presidential", "press", "pressure", "pretend", "pretty", "prevent",
            "previous", "previously", "price", "pride", "priest", "primarily", "primary", "prime", "principal", "principle",
            "print", "prior", "priority", "prison", "prisoner", "privacy", "private", "probably", "problem", "procedure",
            "proceed", "process", "produce", "producer", "product", "production", "profession", "professional", "professor", "profile",
            "profit", "program", "progress", "project", "prominent", "promise", "promote", "prompt", "proof", "proper",
            "properly", "property", "proportion", "proposal", "propose", "proposed", "prosecutor", "prospect", "protect", "protection",
            "protest", "proud", "prove", "provide", "provider", "province", "provision", "psychological", "psychologist", "psychology",
            "public", "publication", "publicly", "publish", "publisher", "pull", "punishment", "purchase", "pure", "purpose",
            "pursue", "push", "put", "qualify", "quality", "quarter", "quarterback", "question", "quick", "quickly",
            "quiet", "quietly", "quit", "quite", "quote", "race", "racial", "radical", "radio", "rail",
            "rain", "raise", "range", "rank", "rapid", "rapidly", "rare", "rarely", "rate", "rather",
            "rating", "ratio", "raw", "reach", "react", "reaction", "read", "reader", "reading", "ready",
            "real", "reality", "realize", "really", "reason", "reasonable", "recall", "receive", "recent",
            "recently", "recipe", "recognition", "recognize", "recommend", "recommendation", "record", "recording", "recover", "recovery",
            "recruit", "red", "reduce", "reduction", "refer", "reference", "reflect", "reflection", "reform", "refugee",
            "refuse", "regard", "regarding", "regardless", "regime", "region", "regional", "register", "regular", "regularly",
            "regulate", "regulation", "reinforce", "reject", "relate", "relation", "relationship", "relative", "relatively", "relax",
            "release", "relevant", "relief", "religion", "religious", "rely", "remain", "remaining", "remarkable", "remember",
            "remind", "remote", "remove", "repeat", "repeatedly", "replace", "reply", "report", "reporter", "represent",
            "representation", "representative", "Republican", "reputation", "request", "require", "requirement", "research", "researcher", "resemble",
            "reservation", "resident", "resist", "resistance", "resolution", "resolve", "resort", "resource", "respect", "respond",
            "respondent", "response", "responsibility", "responsible", "rest", "restaurant", "restore", "restriction", "result", "retain",
            "retire", "retirement", "return", "reveal", "revenue", "review", "revolution", "rhythm", "rice", "rich",
            "rid", "ride", "rifle", "right", "rat","rights","rave","raves", "ring", "rise", "risk", "river", "road", "rock",
            "role", "roll", "romantic", "roof", "room", "root", "rope", "rose", "rough", "roughly",
            "round", "route", "routine", "row", "rub", "rule", "run","ran", "running", "rural", "rush",
            "Russian", "sacred", "sad", "safe", "safety", "sake", "salad", "salary", "sale", "sales",
            "salt", "same", "sample", "sanction", "sand", "satellite", "satisfaction", "satisfy", "sauce", "save",
            "saving", "say", "scale", "scandal", "scared", "scenario", "scene", "schedule", "scheme", "scholar",
            "scholarship", "school", "science", "scientific", "scientist", "scope", "score", "scream", "screen", "script",
            "sea", "search", "season", "seat", "second", "secret", "secretary", "section", "sector", "secure",
            "security", "see", "seed", "seek", "seem", "segment", "seize", "select", "selection", "self",
            "sell", "Senate", "senator", "send", "senior", "sense", "sensitive", "sentence", "separate", "sequence",
            "series", "serious", "seriously", "serve", "service", "session", "set", "setting", "settle", "settlement",
            "seven", "several", "severe", "sex", "sexual", "shade", "shadow", "shake", "shall", "shape",
            "share", "sharp", "she", "sheet", "shelf", "shell", "shelter", "shift", "shine", "ship",
            "shirt", "shit", "shock", "shoe", "shoot", "shooting", "shop", "shopping", "shore", "short",
            "shortly", "shot", "should", "shoulder", "shout", "show", "shower", "shrug", "shut", "sick",
            "side", "sigh", "sight", "sign", "signal", "significance", "significant", "significantly", "silence", "silent",
            "silver", "similar", "similarly", "simple", "simply", "sin", "since", "sing", "singer", "single",
            "sink", "sir", "sister", "sit", "sat", "site", "situation", "six", "size", "ski", "skill",
            "skin", "sky", "slave", "sleep", "slice", "slide", "slight", "slightly", "slip", "slow",
            "slowly", "small", "smart", "smell", "smile", "smoke", "smooth", "snap", "snow", "so",
            "so-called", "soccer", "social", "society", "soft", "software", "soil", "solar", "soldier", "solid",
            "solution", "solve", "some", "somebody", "somehow", "someone", "something", "sometimes", "somewhat", "somewhere",
            "son", "song", "soon", "sophisticated", "sorry", "sort", "soul", "sound", "soup", "source",
            "south", "southern", "Soviet", "space", "Spanish", "speak", "speaker", "special", "specialist", "species",
            "specific", "specifically", "speech", "speed", "spend", "spill", "spirit", "spiritual", "split", "spokesman",
            "sport", "spot", "spread", "spring", "square", "squeeze", "stability", "stable", "staff", "stage",
            "stair", "stake", "stand", "standard", "standing", "star", "stare", "start", "state", "statement",
            "station", "statistics", "status", "stay", "steady", "steal", "steel", "step", "stick", "still",
            "stir", "stock", "stomach", "stone", "stop", "storage", "store", "storm", "story", "straight",
            "strange", "stranger", "strategic", "strategy", "stream", "street", "strength", "strengthen", "stress", "stretch",
            "strike", "string", "strip", "stroke", "strong", "strongly", "structure", "struct", "struggle", "student", "studio",
            "study", "stuff", "stupid", "style", "subject", "submit", "subsequent", "substance", "substantial", "succeed",
            "success", "successful", "successfully", "such", "sudden", "suddenly", "sue", "suffer", "sufficient", "sugar",
            "suggest", "suggestion", "suicide", "suit", "summer", "summit", "sun", "super", "supply", "support",
            "supporter", "suppose", "supposed", "Supreme", "sure", "surely", "surface", "surgery", "surprise", "surprised",
            "surprising", "surprisingly", "surround", "survey", "survival", "survive", "survivor", "suspect", "sustain", "swear",
            "sweep", "sweet", "swim", "swing", "switch", "symbol", "symptom", "system", "table", "tablespoon",
            "tactic", "tail", "take", "tale", "talent", "talk", "tall", "tank", "tap", "tape",
            "target", "task", "taste", "tax", "taxpayer", "tea", "teach", "teacher", "teaching", "team",
            "tear", "teaspoon", "technical", "technique", "technology", "teen", "teenager", "telephone", "telescope", "television",
            "tell", "temperature", "temporary", "ten", "tend", "tendency", "tennis", "tension", "tent", "term",
            "terms", "terrible", "territory", "terror", "terrorism", "terrorist", "test", "testify", "testimony", "testing",
            "text", "than", "thank", "thanks", "that", "the", "theater", "their", "them", "theme",
            "themselves", "then", "theory", "therapy", "there", "therefore", "these", "they", "thick", "thin",
            "thing", "think", "thinking", "third", "thirty", "this", "those", "though", "thought", "thousand",
            "threat", "threaten", "three", "throat", "through", "throughout", "throw", "thus", "ticket", "tie",
            "tight", "time", "tiny", "tip", "tire", "tired", "tissue", "title", "tob",   "tight", "time", "tiny", "tip", "tire", "tired", "tissue", "title", "to", "tobacco",
            "today", "toe", "together", "tomato", "tomorrow", "tone", "tongue", "tonight", "tool", "tooth",
            "top", "topic", "toss", "total", "totally", "touch", "tough", "tour", "tourist", "tournament",
            "toward", "towards", "tower", "town", "toy", "trace", "track", "trade", "tradition", "traditional",
            "traffic", "tragedy", "trail", "train", "training", "transfer", "transform", "transformation", "transition", "translate",
            "transportation", "travel", "treat", "treatment", "treaty", "tree", "tremendous", "trend", "trial", "tribe",
            "trick", "trip", "troop", "trouble", "truck", "true", "truly", "trust", "truth", "try",
            "tube", "tunnel", "turn", "tv", "twelve", "twenty", "twice", "twin", "two", "type",
            "typical", "typically", "ugly", "ultimate", "ultimately", "unable", "uncle", "under","uno", "undergo", "understand",
            "understanding", "unfortunately", "uniform", "union", "unique", "unit", "United", "universal", "universe", "university",
            "unknown", "unless", "unlike", "unlikely", "until", "unusual", "upon", "upper", "urban",
            "urge", "us", "use", "used", "useful", "user", "usual", "usually", "utility", "vacation",
            "valley", "valuable", "value", "variable", "variation", "variety", "various", "vary", "vast", "vegetable",
            "vehicle", "venture", "version", "versus", "very", "vessel", "veteran", "via", "victim", "victory",
            "video", "view", "viewer", "village", "violate", "violation", "violence", "violent", "virtually", "virtue",
            "virus", "visible", "vision", "visit", "visitor", "visual", "vital", "voice", "volume", "volunteer",
            "vote", "voter", "vulnerable", "wage", "wait", "wake", "walk", "wall", "wander", "want",
            "war", "warm", "warn", "warning", "wash", "waste", "watch", "water", "wave", "way", 
            "weak", "wealth", "wealthy", "weapon", "wear", "weather", "wedding", "week", "weekend",
            "weekly", "weigh", "weight", "welcome", "welfare", "well", "west", "western", "wet", "what",
            "whatever", "wheel", "when", "whenever", "where", "whereas", "whether", "which", "while", "whisper",
            "white", "who", "whole", "whom", "whose", "why", "wide", "widely", "widespread", "wife",
            "wild", "will", "willing", "win", "wind", "window", "wine", "wing", "winner", "winter",
            "wipe", "wire", "wisdom", "wise", "wish", "with", "withdraw", "within", "without", "witness",
            "woman", "wonder", "wonderful", "wood", "wooden", "word", "work", "worker", "working", "works",
            "workshop", "world", "worried", "worry", "worth", "would", "wound", "wrap", "write", "writer",
            "writing", "wrong", "yard", "yeah", "year", "yell", "yellow", "yes", "yesterday", "yet",
            "yield", "you", "young", "your", "yours", "yourself", "youth", "zone"
        };


        public FindWordsGameSystem(int level)
        {
            // Generate gibberish based on the level
            Result = GenerateGibberish(level);
            // Find valid English words in the gibberish
            ValidWords = FindValidWords(Result);
        }

        private string GenerateGibberish(int level)
        {
            Random random = new Random();
            List<string> wordsToInclude = new List<string>();
            int numOfWords = level * 5; // Number of valid words increases with the level

            // Randomly select words to include and randomize their case
            for (int i = 0; i < numOfWords; i++)
            {
                string word = RandomizeCase(EnglishWords[random.Next(EnglishWords.Count)], random);
                wordsToInclude.Add(word);
            }

            // Determine the length of the gibberish based on the level
            int length = 100 + (level - 1) * 500; // Level 1: 100, Level 2: 600, ..., Level 7: 3000

            // Construct the gibberish string
            StringBuilder gibberish = new StringBuilder();
            while (gibberish.Length < length)
            {
                gibberish.Append(GetRandomCharacter(random));
            }

            // Insert valid words at random positions in the gibberish string
            foreach (var word in wordsToInclude)
            {
                int position = random.Next(gibberish.Length);
                gibberish.Insert(position, $"{word}");
            }

            return gibberish.ToString();
        }

        private string RandomizeCase(string word, Random random)
        {
            StringBuilder randomized = new StringBuilder(word.Length);
            foreach (char c in word)
            {
                if (random.Next(2) == 0)
                {
                    randomized.Append(char.ToLower(c));
                }
                else
                {
                    randomized.Append(char.ToUpper(c));
                }
            }
            return randomized.ToString();
        }

        private char GetRandomCharacter(Random random)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return chars[random.Next(chars.Length)];
        }

        private List<string> FindValidWords(string gibberish)
        {
            HashSet<string> foundWordsSet = new HashSet<string>();
            List<string> foundWords = new List<string>();

            foreach (var word in EnglishWords)
            {
                if (gibberish.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!foundWordsSet.Contains(word)) // Check if word is already in HashSet
                    {
                        foundWordsSet.Add(word); // Add to HashSet
                        foundWords.Add(word);   // Add to List
                    }
                }
            }

            return foundWords;
        }

    }
}
