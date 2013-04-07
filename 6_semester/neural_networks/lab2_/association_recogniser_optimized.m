function [ recognised_letter] = association_recogniser_optimized( letter , weights_matrix)

    % get output vector
    y = weights_matrix * letter;
    
    % find and return best match 
    recognised_letter = find(ismember(y, max(y)));

end

