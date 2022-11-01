// Generated from C:/Users/cl3v3/Documents/GitHub/ProgramowanieGenetyczne/SharpGP_Core/SharpGP_Core/Grammar\Tiny.g4 by ANTLR 4.10.1
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link TinyParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface TinyVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link TinyParser#program}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(TinyParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#action}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAction(TinyParser.ActionContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#scope}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitScope(TinyParser.ScopeContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#loop}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLoop(TinyParser.LoopContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#read}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRead(TinyParser.ReadContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#write}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitWrite(TinyParser.WriteContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#ifStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIfStatement(TinyParser.IfStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#condition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCondition(TinyParser.ConditionContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#assignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssignment(TinyParser.AssignmentContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpression(TinyParser.ExpressionContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#nestedExp}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNestedExp(TinyParser.NestedExpContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#operand}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitOperand(TinyParser.OperandContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#compareOp}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCompareOp(TinyParser.CompareOpContext ctx);
	/**
	 * Visit a parse tree produced by {@link TinyParser#boolOp}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBoolOp(TinyParser.BoolOpContext ctx);
}