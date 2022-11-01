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

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="SharpParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public interface ISharpListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] SharpParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] SharpParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAction([NotNull] SharpParser.ActionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAction([NotNull] SharpParser.ActionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.scope"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterScope([NotNull] SharpParser.ScopeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.scope"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitScope([NotNull] SharpParser.ScopeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoop([NotNull] SharpParser.LoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoop([NotNull] SharpParser.LoopContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.read"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRead([NotNull] SharpParser.ReadContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.read"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRead([NotNull] SharpParser.ReadContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.write"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWrite([NotNull] SharpParser.WriteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.write"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWrite([NotNull] SharpParser.WriteContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatement([NotNull] SharpParser.IfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatement([NotNull] SharpParser.IfStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondition([NotNull] SharpParser.ConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondition([NotNull] SharpParser.ConditionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment([NotNull] SharpParser.AssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment([NotNull] SharpParser.AssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] SharpParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] SharpParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SharpParser.nestedExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNestedExp([NotNull] SharpParser.NestedExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SharpParser.nestedExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNestedExp([NotNull] SharpParser.NestedExpContext context);
}
