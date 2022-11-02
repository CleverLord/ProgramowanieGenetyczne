grammar Sharp;

program: (action)* | EOF ;
action: (assignment | ifStatement | loop | write );
scope: '{' (action)* '}' ;
loop: 'loop' constant scope ; // loop 10 { }
read: 'read()';
write: 'write' '(' expression ')' ';';
ifStatement: 'if (' condition ')' scope ; // if (thing comp thing) { } ;
condition: expression compareOp expression ; // thing comp thing //this must be evalueable to boolean
assignment: variable '=' expression ';' ; // x_1 = (2+(3*4))
expression: constant | variable | nestedExp | read ; // this must evaluate to value
nestedExp: '(' expression operand expression ')' ; // this also must evaluate to value

variable : VAR;
constant : INT;

operand: '*' | '/' | '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';

INT: [0-9]+;
WS: [ \t\r\n]+ -> skip;
VAR: 'x_'[0-9][0-9]*;   

