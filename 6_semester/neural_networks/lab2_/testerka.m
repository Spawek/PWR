letters = load_letters_definitions();


tries_per_letter = 1;
avg_tries_per_letter = zeros(size(letters,2),1);
weights = calc_weights_matrix(load_letters_definitions);
for letter_no = 1:size(letters,2)
    tries = 0;
    letter_no    
    for i = 1:tries_per_letter
        letter = letters(:, letter_no);
        random_changes_before_fail = 0;
        match_failed = 0;
        changed_indexes = zeros(200,1);
        while(match_failed == 0)
            %change 36 -> 1, 2 -> 2, 70 -> 35 etc...
            if(association_recogniser_optimized(letter, weights) ~= mod((letter_no-1), 35)+1)
                match_failed = 1;
            else 
                % generate random index not changed yet
                rand_index = randi(100);
                while(max(ismember(changed_indexes, rand_index)) ~= 0 & changed_indexes ~= 100)
                    rand_index = randi(100);
                end
                changed_indexes(length(changed_indexes)+1) = rand_index;

                letter(rand_index) = mod(letter(rand_index)+1,2);
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