%% czyszczenie

clear all;

%% parametry symulacji

dt = 5;              % [s] opoznienie startu symulacji

Vg = 0.1;          % [m3] objetosc grzejnika
Vp = 75;           % [m3] objetosc pomieszczenia

cpw = 4190;        % [J / kg*K] cieplo wlasciwe wody
row = 980;         % [kg / m3] gestosc wody
cpp = 1008;        % [J / kg*K] cieplo wlasciwe powietrza     
rop = 1.185;       % [kg / m3] gestosc powietrza

%% wartosci nominalne

Tzew_n = -20;      % [C] temperatura zewnetrzna
Twew_n = 20;       % [C] temperatura wewnetrzna
Tgz_n = 90;        % [C] temperatura zasilania grzejnika
Tgp_n = 70;        % [C] temperatura powrotu z grzejnika
Q_n = 20000;       % [W] cieplo wymagane

Tgsr_n = Tgp_n;                     % [C] temperatura srednia grzejnika
Fg_n = Q_n/(cpw*row*(Tgz_n-Tgp_n)); % [m3 / s] przeplyw wody przez grzejnik

%% wartosci poczatkowe

Tzew_0 = Tzew_n+2;   % [C] temperatura zewnetrzna
Twew_0 = Twew_n;   % [C] temperatura wewnetrzna
Tgz_0 = Tgz_n;     % [C] temperatura zasilania grzejnika
Tgsr_0 = Tgsr_n;   % [C] temperatura powrotu z grzejnika
Fg_0 = Fg_n;       % [m3 / s] przeplyw wody przez grzejnik
Qt_0 = 0;          % [W] straty/zyski technologiczne

%% zmiany wartosci

dTzew = 0;         % [C] zmiana temperatury zewnetrznej
dTgz = 0;          % [C] zmiana temperatury zasilania grzejnika
dFg = 0;           % [m3 / s] przeplyw wody przez grzejnik
dQt = 0;           % [W] straty/zyski technologiczne

%% wspolczynniki

kg = Q_n/(Tgsr_n-Twew_n);         % wspolczynnik przewodzenia grzejnika
kw = (Q_n-Qt_0)/(Twew_n-Tzew_n);  % wspolczynnik przewodzenia scian

%% symulacja

sim('lab2_model_bloczek');

% plot(t,Twew);

