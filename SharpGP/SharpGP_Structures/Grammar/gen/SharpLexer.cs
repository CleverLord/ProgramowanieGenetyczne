//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.10.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/cl3v3/Documents/GitHub/ProgramowanieGenetyczne/SharpGP/SharpGP_Structures/Grammar\Sharp.g4 by ANTLR 4.10.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public partial class SharpLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, INT=21, WS=22, VAR=23;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "INT", "WS", "VAR"
	};


	public SharpLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public SharpLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'{'", "'}'", "'loop'", "'read()'", "';'", "'write'", "'('", "')'", 
		"'if ('", "'='", "'*'", "'/'", "'+'", "'-'", "'=='", "'!='", "'>'", "'<'", 
		"'>='", "'<='"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, "INT", "WS", "VAR"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Sharp.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static SharpLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,23,128,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,1,0,1,0,1,1,1,1,1,2,1,2,1,2,1,2,1,2,1,3,1,3,1,3,1,3,1,3,
		1,3,1,3,1,4,1,4,1,5,1,5,1,5,1,5,1,5,1,5,1,6,1,6,1,7,1,7,1,8,1,8,1,8,1,
		8,1,8,1,9,1,9,1,10,1,10,1,11,1,11,1,12,1,12,1,13,1,13,1,14,1,14,1,14,1,
		15,1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,18,1,18,1,19,1,19,1,19,1,20,4,
		20,108,8,20,11,20,12,20,109,1,21,4,21,113,8,21,11,21,12,21,114,1,21,1,
		21,1,22,1,22,1,22,1,22,1,22,5,22,124,8,22,10,22,12,22,127,9,22,0,0,23,
		1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,
		29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,45,23,1,0,2,1,0,48,57,
		3,0,9,10,13,13,32,32,130,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,
		0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,
		1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,
		0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,
		1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,1,47,1,0,0,0,3,49,1,0,0,0,5,51,1,0,0,
		0,7,56,1,0,0,0,9,63,1,0,0,0,11,65,1,0,0,0,13,71,1,0,0,0,15,73,1,0,0,0,
		17,75,1,0,0,0,19,80,1,0,0,0,21,82,1,0,0,0,23,84,1,0,0,0,25,86,1,0,0,0,
		27,88,1,0,0,0,29,90,1,0,0,0,31,93,1,0,0,0,33,96,1,0,0,0,35,98,1,0,0,0,
		37,100,1,0,0,0,39,103,1,0,0,0,41,107,1,0,0,0,43,112,1,0,0,0,45,118,1,0,
		0,0,47,48,5,123,0,0,48,2,1,0,0,0,49,50,5,125,0,0,50,4,1,0,0,0,51,52,5,
		108,0,0,52,53,5,111,0,0,53,54,5,111,0,0,54,55,5,112,0,0,55,6,1,0,0,0,56,
		57,5,114,0,0,57,58,5,101,0,0,58,59,5,97,0,0,59,60,5,100,0,0,60,61,5,40,
		0,0,61,62,5,41,0,0,62,8,1,0,0,0,63,64,5,59,0,0,64,10,1,0,0,0,65,66,5,119,
		0,0,66,67,5,114,0,0,67,68,5,105,0,0,68,69,5,116,0,0,69,70,5,101,0,0,70,
		12,1,0,0,0,71,72,5,40,0,0,72,14,1,0,0,0,73,74,5,41,0,0,74,16,1,0,0,0,75,
		76,5,105,0,0,76,77,5,102,0,0,77,78,5,32,0,0,78,79,5,40,0,0,79,18,1,0,0,
		0,80,81,5,61,0,0,81,20,1,0,0,0,82,83,5,42,0,0,83,22,1,0,0,0,84,85,5,47,
		0,0,85,24,1,0,0,0,86,87,5,43,0,0,87,26,1,0,0,0,88,89,5,45,0,0,89,28,1,
		0,0,0,90,91,5,61,0,0,91,92,5,61,0,0,92,30,1,0,0,0,93,94,5,33,0,0,94,95,
		5,61,0,0,95,32,1,0,0,0,96,97,5,62,0,0,97,34,1,0,0,0,98,99,5,60,0,0,99,
		36,1,0,0,0,100,101,5,62,0,0,101,102,5,61,0,0,102,38,1,0,0,0,103,104,5,
		60,0,0,104,105,5,61,0,0,105,40,1,0,0,0,106,108,7,0,0,0,107,106,1,0,0,0,
		108,109,1,0,0,0,109,107,1,0,0,0,109,110,1,0,0,0,110,42,1,0,0,0,111,113,
		7,1,0,0,112,111,1,0,0,0,113,114,1,0,0,0,114,112,1,0,0,0,114,115,1,0,0,
		0,115,116,1,0,0,0,116,117,6,21,0,0,117,44,1,0,0,0,118,119,5,120,0,0,119,
		120,5,95,0,0,120,121,1,0,0,0,121,125,7,0,0,0,122,124,7,0,0,0,123,122,1,
		0,0,0,124,127,1,0,0,0,125,123,1,0,0,0,125,126,1,0,0,0,126,46,1,0,0,0,127,
		125,1,0,0,0,4,0,109,114,125,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
