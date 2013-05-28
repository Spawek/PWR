function [ distance ] = distance( letter1, letter2 )
if(size(letter1, 1) ~= size(letter2,1))
   letter1
   letter2
end
distance = sum(abs(letter1-letter2));

end

