function [ distance ] = distance( letter1, letter2 )

%distance = sqrt(sum(letter1-letter2).^2);
distance = sum(abs(letter1-letter2));

end

