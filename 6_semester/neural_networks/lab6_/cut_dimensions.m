function [ cut_data, A_matrix ] = cut_dimensions( data, certainity_level)
if(certainity_level > 1 || certainity_level < 0)
    disp('invalid certainity level')
    return;
end

data = data';

R = size(data,2);
S = 1/R * (data' * data);
[wektory_wlasne,useless_variable] = eig(S);
lambda = eig(S);
%so (S - lambda(7)*eye(70))*wektory_wlasne(:,7) ~~~= 0 //numeric errors
%wektory_wlasne(:,7)'*wektory_wlasne(:,7) = 1

lambda_sum = sum(lambda);
sub_sum = 0;
beg_datadex = 0;
for i = R:-1:1
    sub_sum = sub_sum + lambda(i);
    if(sub_sum > certainity_level * lambda_sum)
        beg_datadex = i;
       break; 
    end
end

A_matrix = wektory_wlasne(:,beg_datadex:R);
cut_data = (data*A_matrix)';

end

