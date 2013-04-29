function [ recognised_letter] = association_recogniser_optimized( letter , weights_matrix, A_matrix)

    % get output vector
    y = weights_matrix * (letter' * A_matrix)';
    
    % find and return best match 
    recognised_letter = find(ismember(y, max(y)));

end

