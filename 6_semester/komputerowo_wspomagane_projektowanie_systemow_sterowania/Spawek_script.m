
%% zmiany wartosci

dTzew = 0;         % [C] zmiana temperatury zewnetrznej
dTgz = 0;          % [C] zmiana temperatury zasilania grzejnika
dFg = 0.0001;           % [m3 / s] przeplyw wody przez grzejnik
dQt = 0;           % [W] straty/zyski technologiczne

%Tzew_add, Tgz_add, fg_mod,
clf

%mode 1 - normal
[Twew, Tgsr] = Spawek_ch_dyn(0,0,1, dTzew, dTgz, dFg, dQt);
% Temperatura wewnetrzna
subplot(211);
plot(Twew.time, Twew.signals.values,'r');
title('Temperatura wewnetrzna');
xlabel('czas[s]');
ylabel('Temp wew[C deg]')


hold on

%mode 2 - Tzew += 10
[Twew, Tgsr] = Spawek_ch_dyn(10,0,1, dTzew, dTgz, dFg, dQt);
% Temperatura wewnetrzna
subplot(211);
plot(Twew.time, Twew.signals.values,'b');

%mode 3 - Tgz -= 10
[Twew, Tgsr] = Spawek_ch_dyn(0,-10,1, dTzew, dTgz, dFg, dQt);
% Temperatura wewnetrzna
plot(Twew.time, Twew.signals.values,'g');

%mode 4 - fg *= 0.5
[Twew, Tgsr] = Spawek_ch_dyn(0,0,0.5, dTzew, dTgz, dFg, dQt);
% Temperatura wewnetrzna
plot(Twew.time, Twew.signals.values,'k');

legend('warnki nominalne', 'Tzew += 10', 'Tgz -= 10', 'Fg *= 0.5', 'Location', 'NorthWest')