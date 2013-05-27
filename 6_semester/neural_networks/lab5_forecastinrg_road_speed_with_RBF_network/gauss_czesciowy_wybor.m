function [ out ] = gauss_czesciowy_wybor( macierz_glowna, wyrazy_wolne )

%% sprawdzenie czy zadanie jest dobrze uwarunkowane
if (det(macierz_glowna) == 0)
    disp('wyznacznik macierzy glownej jest rowny 0, nie ma rozwi¹zania tego rownania') 
end

%% laczenie w macierzy_glownej i macierzy wyrazow wolnych w macierz
%% powiekszona
A = [macierz_glowna, wyrazy_wolne];

%% wyliczenie mincierzy trojkatnej gornej
for i = 1:size(A,1) %rzad odejmujemy
    
    %% czesciowy wybor
    maxValIndex = i;
    maxVal = A(i,i);
    for j = i:size(A,1)
        if A(j, i) > maxVal
            maxValIndex = j;
            maxVal = A(j,i);
        end
    end
    A([i,maxValIndex], :) = A([maxValIndex,i], :); % zamiana

    %% eliminacja gaussa
    for j = i+1:size(A,1) %rzad od ktorego odejmujemy
        d = A(j,i)/A(i,i);
        for k = i:size(A,2) % iteracja po kolumnach
            A(j,k) = A(j,k) - d*A(i,k);
        end
    end
end

%% wyliczenie macierzy skosnej
for i = size(A, 1):-1:1 % od ostatniego do pierwszego wiersza
    for j = i-1:-1:1
        for k = size(A,2):-1:i
            A(j,k) = A(j,k) - A(j,i)/A(i,i)*A(i,k);
        end
    end
end

out = zeros(size(A,1), 1);
for i = 1:size(A,1)
    out(i) = A(i, size(A,2))/A(i,i);
end


end

