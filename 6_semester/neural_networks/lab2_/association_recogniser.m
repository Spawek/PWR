function [ recognised_letter] = association_recogniser( letter )

    % get output vector
    y = calc_weights_matrix(load_letters_definitions) * letter;
    
    % find and return best match (bigest val index in discrimination vector)
    recognised_letter = find(ismember(y, max(y)));

end

