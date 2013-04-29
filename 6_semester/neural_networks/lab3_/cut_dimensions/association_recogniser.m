function [ recognised_letter] = association_recogniser( letter )

    %cutting definitions
    [cut_letters_definitions, A_matrix] = cut_dimensions(load_letters_definitions(), 0.95);

    % get output vector
    y = calc_weights_matrix(cut_letters_definitions) * (letter' * A_matrix)';
    
    % find and return best match (bigest val index in discrimination vector)
    recognised_letter = find(ismember(y, max(y)));

end

