function [ out ] = mat_reverse( in )

%% dopisanie do maeierzy wejsciowej macierzy jednostkowej o identycznych
%% wymiarach
out = [in, eye(length(in))];

%% eliminacja gaussa
%%out = gauss_elimination(out);

%% wyliczenie mincierzy trojkatnej gornej
for i = 1:size(out,1) %rzad odejmujemy
    
    %% czesciowy wybor
    maxValIndex = i;
    maxVal = out(i,i);
    for j = i:size(out,1)
        if out(j, i) > maxVal
            maxValIndex = j;
            maxVal = out(j,i);
        end
    end
    out([i,maxValIndex], :) = out([maxValIndex,i], :); % zamiana

    %% eliminacja gaussa
    for j = i+1:size(out,1) %rzad od ktorego odejmujemy
        d = out(j,i)/out(i,i);
        for k = i:size(out,2) % iteracja po kolumnach
            out(j,k) = out(j,k) - d*out(i,k);
        end
    end
end

%% wyliczenie macierzy skosnej
for i = size(out, 1):-1:1 % od ostatniego do pierwszego wiersza
    for j = i-1:-1:1
        for k = size(out,2):-1:i
            out(j,k) = out(j,k) - out(j,i)/out(i,i)*out(i,k);
        end
    end
end

%% przemnozenie wierszy przez odwrotnosci elementow na glownej przekatnej
for i = 1:size(out,1)
    for k = size(out,2):-1:i
        out(i,k) = out(i,k)/(out(i,i));
    end
end


%% wyciecie tylko potrzebnej czesci
out = out(: , size(out,1)+1:size(out,2));


end

