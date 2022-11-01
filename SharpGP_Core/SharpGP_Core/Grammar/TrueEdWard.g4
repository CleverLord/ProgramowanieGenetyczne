grammar TrueEdWard;

program: (assignment | functionCallOut | ifStatement | forStatement | scope | COMMENT)* | EOF;

assignment: VAR '=' (expression | functionCallIn) ';';

functionCallIn: 'read()' | 'cin()' ';';

functionCallOut: 'write(' (VAR | constant) ')' | 'cout(' (VAR | constant) ')'  ';';

ifStatement: 'if' '(' logicExpression ')' block;

forStatement: 'for' forAssignment ',' forLogicExpression ',' ('+' | '-') forIncrement ')' block;

forAssignment: '('VAR '=' expression;

forLogicExpression: expression compareOp expression;

forIncrement: VAR | INT;

expression
    : constant                                   #constExpr
    | VAR                                        #varExpr
    | expression multOp expression               #multiplicativeExpr
    | expression addOp expression                #additiveExpr
    ;

logicExpression
    : expression compareOp expression             #comparisonExpr
    | logicExpression boolOp logicExpression      #booleanExpr
    ;

multOp: '*' | '/';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: 'and' | 'or';

block: '{' (assignment | functionCallOut | ifStatement | forStatement | scope | COMMENT)* '}';

scope: '[' (assignment | functionCallOut | ifStatement | forStatement | scope | COMMENT)* ']';

constant: INT | STR | BOOL | NULL;

INT: [0-9]+;
STR: ('"' ~'"'* '"') | ('\'' ~'\''* '\'');
BOOL: 'true' | 'false';
NULL: 'null';

COMMENT
    : '#' ~[+\-\r\n] ~[\r\n]* // a '*' must be followed by something other than '+', '-' or a line break
    | '#' ( [\r\n]+ | EOF );  // a '*' is a valid comment if directly followed by a line break, or the EOF ;

WS: [ \t\r\n]+ -> skip;
VAR: '@'? [a-zA-Z_][a-zA-Z0-9_]*;