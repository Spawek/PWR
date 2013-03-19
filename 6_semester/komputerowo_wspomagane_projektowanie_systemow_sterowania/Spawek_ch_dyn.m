function [ Twew, Tgsr ] = Spawek_ch_dyn(Tzew_add, Tgz_add, fg_mod, dTzew_, dTgz_, dFg_, dQt_)

global Vg
global Vp
global cpw
global row
global cpp
global rop
global Tzew_n
global Twew_n
global Tgz_n
global Tgsr_n
global Fg_n
global Q_n
global Tzew_0
global Twew_0
global Tgz_0
global Tgsr_0
global Fg_0
global Qt_0
global dTzew
global dTgz
global dFg
global dQt
global dt
global kw
global kg
global Cvg
global Cvw


%% parametry symulacji

dt = 0;          % [s] opoznienie startu symulacji

%% parametry pomieszczenia i grzejnika

Vg = 0.02;         % [m3] objetosc grzejnika
Vp = 20;           % [m3] objetosc pomieszczenia

%% parametry fizyczne

cpw = 4190;        % [J / kg*K] cieplo wlasciwe wody
row = 980;         % [kg / m3] gestosc wody
cpp = 1008;        % [J / kg*K] cieplo wlasciwe powietrza     
rop = 1.185;       % [kg / m3] gestosc powietrza

Cvg = cpw*row*Vg/4; % [W] pojemnosc cieplna grzejnika
Cvw = cpp*rop*Vp;   % [W] pojemnosc cieplna pomieszczenia

%% wartosci nominalne

Tzew_n = -20 + Tzew_add;      % [C] temperatura zewnetrzna
Twew_n = 20;       % [C] temperatura wewnetrzna
Tgz_n = 90 + Tgz_add;        % [C] temperatura zasilania grzejnika
Tgp_n = 70;        % [C] temperatura powrotu z grzejnika
Q_n = 20000;       % [W] cieplo wymagane
Qt_n = 0;          % [W] cieplo technologiczne

Tgsr_n = Tgp_n;                     % [C] temperatura srednia grzejnika
Fg_n = Q_n/(cpw*row*(Tgz_n-Tgp_n)) * fg_mod; % [m3 / s] przeplyw wody przez grzejnik
kg = Q_n/(Tgsr_n-Twew_n) * fg_mod;           % wspolczynnik przewodzenia grzejnika
kw = (Q_n-Qt_n)/(Twew_n-Tzew_n) * fg_mod;    % wspolczynnik przewodzenia scian

%% wartosci poczatkowe

Tzew_0 = Tzew_n;   % [C] temperatura zewnetrzna
Twew_0 = Twew_n;   % [C] temperatura wewnetrzna
Tgz_0 = Tgz_n;     % [C] temperatura zasilania grzejnika
Tgsr_0 = Tgsr_n;   % [C] temperatura powrotu z grzejnika
Fg_0 = Fg_n;       % [m3 / s] przeplyw wody przez grzejnik
Qt_0 = Qt_n;       % [W] straty/zyski technologiczne

%% zmiany wartosci

dTzew = dTzew_;         % [C] zmiana temperatury zewnetrznej
dTgz = dTgz_;          % [C] zmiana temperatury zasilania grzejnika
dFg = dFg_;           % [m3 / s] przeplyw wody przez grzejnik
dQt = dQt_;           % [W] straty/zyski technologiczne

%% symulacja

sim('lab1_bloczek');


end

