function [ recognised_letter ] = letter_recogniser( input )

%% load letters
letters = load_letters_definitions();

% make '-1' out of '0'
letters = letters *2 - 1;

%% match
matcher = letters' * input;

%% find and return best match
recognised_letter = find(ismember(matcher, max(matcher)));

end

