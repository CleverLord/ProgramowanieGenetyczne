grammar Sharp;

program: (action)* | EOF ;

action: (assignment | ifStatement | loop | write );

// { }
scope: '{' (action)* '}' ;

// loop 10 { }
loop: 'loop' INT scope ;

read: 'read()'  ;
write: 'write' '(' expression ')' ';';

// if (thing comp thing) { } ;
ifStatement: 'if (' condition ')' scope ;

// thing comp thing
condition: expression COMPAREOP expression ; //this must be evalueable to boolean

// x_1 = (2+(3*4))
assignment: VAR '=' expression ';' ;

expression: INT | VAR | nestedExp | read ; // this must evaluate to value

nestedExp: '(' expression OPERAND expression ')' ; // this also must evaluate to value

OPERAND: '*' | '/' | '+' | '-';
COMPAREOP: '==' | '!=' | '>' | '<' | '>=' | '<=';

INT: [0-9]+;
WS: [ \t\r\n]+ -> skip;
VAR: 'x_'[0-9][0-9]*;   