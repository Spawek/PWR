function [letters] = load_letters_definitions ( )
% A  
p1 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 1 1 1 1 1 1 1 ...
	0 0 1 0 0 0 1 0 0 1 ...
	0 1 0 0 0 0 1 0 0 0 ...
	0 1 0 0 0 0 1 0 0 0 ...
	0 0 1 0 0 0 1 0 0 1 ...
	0 0 0 1 1 1 1 1 1 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% A,  
p2 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 1 0 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 1 0 0 1 0 ...
	1 0 0 0 0 1 0 0 0 0 ...
	1 0 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 1 0 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ]';
										
% B
p3 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 0 1 1 1 0 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';
 
% C
p4 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 1 0 0 0 0 0 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';
    
    % c'
p5 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 1 1 1 1 1 0 ...
	1 0 0 1 0 0 0 0 0 1 ...
	0 1 0 1 0 0 0 0 0 1 ...
	0 0 0 1 0 0 0 0 0 1 ...
	0 0 0 1 0 0 0 0 0 1 ...
	0 0 0 0 1 0 0 0 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% D  
p6 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 1 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 0 0 0 0 0 0 1 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% E  
p7 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 1 0 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% E,
p8 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	1 1 1 1 1 1 1 1 1 0 ...
	1 0 0 0 1 0 0 0 1 0 ...
	1 0 0 0 1 0 0 0 1 0 ...
	1 0 0 0 1 0 0 0 1 0 ...
	1 0 0 0 1 0 0 0 1 1 ...
	1 1 0 0 0 0 0 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% F  
p9 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 1 1 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% G  
p10 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 0 1 0 0 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% H  
p11 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 0 0 0 0 1 0 0 0 0 ...
	0 0 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% I
p12 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% J
p13 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 1 1 0 0 0 0 0 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% K
p14 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 0 0 0 1 0 1 0 0 0 ...
	0 1 0 1 0 0 0 1 0 1 ...
	0 1 1 0 0 0 0 0 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% L
p15 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% �
p16 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 1 0 0 0 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 0 0 0 1 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% M
p17 = [	0 0 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 1 0 0 0 0 0 0 0 ...
	0 0 0 1 1 0 0 0 0 0 ...
	0 0 0 1 1 0 0 0 0 0 ...
	0 0 1 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 0 0 0 0 0 0 0 0 1 ]';

% N
p18 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 0 1 1 0 0 0 0 0 1 ...
	0 0 0 0 1 0 0 0 0 0 ...
	0 0 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 0 1 1 0 0 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% �
p19 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 1 1 1 1 1 1 1 ...
	0 1 0 0 1 0 0 0 0 1 ...
	0 0 1 0 0 1 0 0 0 0 ...
	0 0 0 0 0 0 1 0 0 0 ...
	0 0 0 1 0 0 0 1 1 0 ...
	0 0 0 1 1 1 1 1 1 1 ...
	0 0 0 1 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% O
p20 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% �
p21 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 1 1 1 1 1 0 ...
	0 1 0 1 0 0 0 0 0 1 ...
	0 0 1 1 0 0 0 0 0 1 ...
	0 0 0 1 0 0 0 0 0 1 ...
	0 0 0 1 0 0 0 0 0 1 ...
	0 0 0 0 1 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% P
p22 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 1 0 0 0 1 0 0 0 0 ...
	0 0 1 1 1 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% Q
p23 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 1 0 1 ...
	0 1 0 0 0 0 0 0 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 1 1 1 1 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% R
p24 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 1 0 0 0 ...
	0 1 0 0 0 1 0 1 0 1 ...
	0 1 0 0 0 1 0 0 1 1 ...
	0 0 1 1 1 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% S
p25 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 1 1 1 0 0 0 1 0 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 1 0 0 0 1 0 0 0 1 ...
	0 0 1 0 0 0 1 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% �
p26 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 1 1 0 0 1 0 ...
	1 0 0 1 0 0 1 0 0 1 ...
	0 1 0 1 0 0 1 0 0 1 ...
	0 0 0 1 0 0 1 0 0 1 ...
	0 0 0 1 0 0 1 0 0 1 ...
	0 0 0 0 1 0 0 1 1 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% T
p27 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 1 1 0 0 0 0 0 0 ...
	0 1 1 0 0 0 0 0 0 0 ...
	0 1 1 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 1 1 1 1 1 1 1 1 ...
	0 1 1 0 0 0 0 0 0 1 ...
	0 1 1 0 0 0 0 0 0 0 ...
	0 1 1 1 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% U
p28 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 1 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% V
p29 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 1 1 1 1 0 0 0 0 0 ...
	0 1 0 0 1 1 1 0 0 0 ...
	0 0 0 0 0 0 0 1 1 1 ...
	0 0 0 0 0 0 0 1 1 1 ...
	0 1 0 0 1 1 1 0 0 0 ...
	0 1 1 1 1 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% W
p30 = [	0 1 0 0 0 0 0 0 0 0 ...
	0 1 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 1 0 ...
	0 0 0 0 0 0 1 1 0 0 ...
	0 0 0 0 0 0 1 1 0 0 ...
	0 0 0 0 0 0 0 0 1 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 1 1 1 1 1 0 ...
	0 1 0 0 0 0 0 0 0 0 ]';

% X
p31 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 1 1 1 0 0 0 1 1 1 ...
	0 1 0 0 1 0 1 0 0 1 ...
	0 0 0 0 0 1 0 0 0 0 ...
	0 0 0 0 0 1 0 0 0 0 ...
	0 1 0 0 1 0 1 0 0 1 ...
	0 1 1 1 0 0 0 1 1 1 ...
	0 1 0 0 0 0 0 0 0 1 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% Y
p32 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 1 1 1 0 0 0 0 0 0 ...
	0 1 0 0 1 0 0 0 0 1 ...
	0 0 0 0 0 1 1 1 1 1 ...
	0 0 0 0 0 1 1 1 1 1 ...
	0 1 0 0 1 0 0 0 0 1 ...
	0 1 1 1 0 0 0 0 0 0 ...
	0 1 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% Z
p33 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 1 1 0 0 0 0 1 1 1 ...
	0 1 0 0 0 0 1 1 0 1 ...
	0 1 0 0 0 1 1 0 0 1 ...
	0 1 0 0 1 1 0 0 0 1 ...
	0 1 0 1 1 0 0 0 0 1 ...
	0 1 1 1 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% �
p34 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 1 1 0 0 0 1 1 ...
	0 0 0 1 0 0 0 1 0 1 ...
	1 0 0 1 0 0 1 0 0 1 ...
	0 1 0 1 0 1 0 0 0 1 ...
	0 0 0 1 1 0 0 0 0 1 ...
	0 0 0 1 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';

% �
p35 = [	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 1 1 0 0 0 1 1 ...
	0 0 0 1 0 0 0 1 0 1 ...
	0 1 0 1 0 0 1 0 0 1 ...
	0 1 0 1 0 1 0 0 0 1 ...
	0 0 0 1 1 0 0 0 0 1 ...
	0 0 0 1 0 0 0 0 1 1 ...
	0 0 0 0 0 0 0 0 0 0 ...
	0 0 0 0 0 0 0 0 0 0 ]';
  
letters = [p1 p2 p3 p4 p5 p6 p7 p8 p9 p10 p11 p12 p13 p14 p15 p16 p17 p18 p19 p20 p21 p22 p23 p24 p25 p26 p27 p28 p29 p30 p31 p32 p33 p34 p35];
