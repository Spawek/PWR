letters = load_letters_definitions();
certainity_level = 0.75;

[cut_letters, A_matrix] = cut_dimensions(load_letters_definitions(), certainity_level);

tries_per_letter = 10;
avg_tries_per_letter = zeros(size(letters,2),1);
%[weights,r] = calc_weights_matrix(load_letters_definitions);
[weights,r] = calc_weights_matrix(cut_letters);
cut_letters = (letters' * A_matrix)';
for letter_no = 1:size(letters,2)
    tries = 0;
    letter_no    
    for i = 1:tries_per_letter
        letter = letters(:, letter_no);
        random_changes_before_fail = 0;
        match_failed = 0;
        changed_indexes = zeros(100,1);
        while(match_failed == 0 && random_changes_before_fail ~= 100)
            if(RBF_recognizer(letter, weights, cut_letters, r, A_matrix) ~= mod((letter_no-1), 35)+1)
                match_failed = 1;
            else 
                % generate random index not changed yet
                rand_index = randi(100);
                while(max(ismember(changed_indexes, rand_index)) ~= 0)
                    rand_index = randi(100);
                end
                changed_indexes(length(changed_indexes)+1) = rand_index;

                letter(rand_index) = letter(rand_index) * -1;
                random_changes_before_fail = random_changes_before_fail + 1;
            end
        end
        tries = tries + random_changes_before_fail;
    end
    avg_tries_per_letter(letter_no) = tries / tries_per_letter;
end

stem(avg_tries_per_letter)
title('average random changes on picture needed for recogniser to match incorrectly')
xlabel('letter')
ylabel('tries needed')