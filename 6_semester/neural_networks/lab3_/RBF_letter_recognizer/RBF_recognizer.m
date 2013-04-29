function [ recognised_letter] = RBF_recognizer( letter , weights_matrix, letters, r)
    
    n = size(weights_matrix,1);
    
    pre = zeros(n,1);
    for i = 1:n
        pre(i) = exp(-1*(distance(letter, letters(:,i))/r)^2);
    end

    % get output vector
    y = weights_matrix * pre;
    
    % find and return best match 
    recognised_letter = find(ismember(y, max(y)));

end

