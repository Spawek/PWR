function [ recognised_letter ] = letter_recogniser( input )

%% load letters
letters = load_letters_definitions();

%% weight calculation
w_n_plus_1 = -1*sum(letters'.*letters',2)/2;

%% dicrimination foo vector calculation
discrimination = letters' * input + w_n_plus_1;

%% find and return best match (bigest val index in discrimination vector)
recognised_letter = find(ismember(discrimination, max(discrimination)));

end
