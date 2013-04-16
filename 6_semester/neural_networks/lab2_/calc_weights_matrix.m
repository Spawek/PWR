function [ weights_matrix] = calc_weights_matrix( letters )

    % F matrix is eye matrix for now coz i have 
    % just 1 pattern for each letter
    F = eye(35);
    
    % addding small letters (from 36 to 70)
    % there is exactly 1 small letter for each capital letter
    F = [F;eye(35)];
    
    weights_matrix = F' * letters';
end

