function [ weights_matrix, r] = calc_weights_matrix( letters )

n = size(letters,2);

F = eye(n);

%find r - max distance betw 2 patterns
l = 13;
r_max = 0;
for i = 1:n
    for j = 1:n
        cur_r = distance(letters(:,i), letters(:, j));
        if(cur_r > r_max)
           r_max = cur_r; 
        end
    end
end
r = r_max/l;

%calc Phi matrix
Phi = zeros(n);
for i = 1:n
   for j = 1:n
       Phi(i,j) = exp(-1*(distance(letters(:,i), letters(:,j))/r)^2);
   end
end

%det(Phi) == 0 -> what now? //new method of calculating distance now ;)

weights_matrix = mat_reverse(Phi) * F; %F is just eye for now
end

