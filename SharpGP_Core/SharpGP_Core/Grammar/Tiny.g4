grammar Tiny;

program: (action)* | EOF ;

action: (assignment | ifStatement | loop | read | write);

// { }
scope: '{' (action)* '}' ;

// loop 10 { }
loop: 'loop' INT scope ;

read: 'read()'  ;
write: 'write()'  ;

// if (thing comp thing) { } ;
ifStatement: 'if (' condition ')' scope ;

// thing comp thing
condition: expression compareOp expression ; //this must be evalueable to boolean

// x_1 = (2+(3*4))
assignment: VAR '=' expression ;

expression: INT | VAR | '(' expression operand expression ')' ; // this must evaluate to value

operand: '*' | '/' | '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: 'and' | 'or';

INT: [0-9]+;

WS: [ \t\r\n]+ -> skip;
VAR: 'x_'[0-9][0-9]*;   