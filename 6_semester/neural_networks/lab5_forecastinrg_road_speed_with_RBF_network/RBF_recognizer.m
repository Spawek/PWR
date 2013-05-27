function [ recognised_input] = RBF_recognizer( input , weights_matrix, patterns, r, A_matrix)
    
    n = size(weights_matrix,1);
    
    input = (input' * A_matrix)';
    
    pre = zeros(n,1);
    for i = 1:n
        pre(i) = exp(-1*(distance(input, patterns(:,i))/r)^2);
    end

    % get output vector h
    y = weights_matrix * pre;
    
    % find and return best match 
    recognised_input = find(ismember(y, max(y)));

end

