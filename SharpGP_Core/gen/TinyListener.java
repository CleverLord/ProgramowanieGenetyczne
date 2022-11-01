// Generated from C:/Users/cl3v3/Documents/GitHub/ProgramowanieGenetyczne/SharpGP_Core/SharpGP_Core/Grammar\Tiny.g4 by ANTLR 4.10.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link TinyParser}.
 */
public interface TinyListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link TinyParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(TinyParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(TinyParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#action}.
	 * @param ctx the parse tree
	 */
	void enterAction(TinyParser.ActionContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#action}.
	 * @param ctx the parse tree
	 */
	void exitAction(TinyParser.ActionContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#scope}.
	 * @param ctx the parse tree
	 */
	void enterScope(TinyParser.ScopeContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#scope}.
	 * @param ctx the parse tree
	 */
	void exitScope(TinyParser.ScopeContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#loop}.
	 * @param ctx the parse tree
	 */
	void enterLoop(TinyParser.LoopContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#loop}.
	 * @param ctx the parse tree
	 */
	void exitLoop(TinyParser.LoopContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#read}.
	 * @param ctx the parse tree
	 */
	void enterRead(TinyParser.ReadContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#read}.
	 * @param ctx the parse tree
	 */
	void exitRead(TinyParser.ReadContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#write}.
	 * @param ctx the parse tree
	 */
	void enterWrite(TinyParser.WriteContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#write}.
	 * @param ctx the parse tree
	 */
	void exitWrite(TinyParser.WriteContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void enterIfStatement(TinyParser.IfStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void exitIfStatement(TinyParser.IfStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#condition}.
	 * @param ctx the parse tree
	 */
	void enterCondition(TinyParser.ConditionContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#condition}.
	 * @param ctx the parse tree
	 */
	void exitCondition(TinyParser.ConditionContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#assignment}.
	 * @param ctx the parse tree
	 */
	void enterAssignment(TinyParser.AssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#assignment}.
	 * @param ctx the parse tree
	 */
	void exitAssignment(TinyParser.AssignmentContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterExpression(TinyParser.ExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitExpression(TinyParser.ExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#operand}.
	 * @param ctx the parse tree
	 */
	void enterOperand(TinyParser.OperandContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#operand}.
	 * @param ctx the parse tree
	 */
	void exitOperand(TinyParser.OperandContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#compareOp}.
	 * @param ctx the parse tree
	 */
	void enterCompareOp(TinyParser.CompareOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#compareOp}.
	 * @param ctx the parse tree
	 */
	void exitCompareOp(TinyParser.CompareOpContext ctx);
	/**
	 * Enter a parse tree produced by {@link TinyParser#boolOp}.
	 * @param ctx the parse tree
	 */
	void enterBoolOp(TinyParser.BoolOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link TinyParser#boolOp}.
	 * @param ctx the parse tree
	 */
	void exitBoolOp(TinyParser.BoolOpContext ctx);
}