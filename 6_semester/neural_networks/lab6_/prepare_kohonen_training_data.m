function [ training_data ] = prepare_kohonen_training_data( )

WANTED_VECTORS_NO = 30;
KOHONEN_ITERATIONS_NO = 100;
ALFA = 0.5;

data = prepare_training_data();
probe_size = size(data,1);
no_of_probes = size(data,2);
size(data);

weights_matrix = rand(probe_size, WANTED_VECTORS_NO);
weights_matrix = weights_matrix / norm(weights_matrix);

for n = 1:KOHONEN_ITERATIONS_NO
    for probe = 1:no_of_probes
        x = data(:, probe);
        
        best_weight = 1;
        best_weight_match = weights_matrix(:, 1)' * x;
        for weight_no = 2:WANTED_VECTORS_NO
            curr_weight_match = weights_matrix(:, weight_no)' * x;
            if(curr_weight_match > best_weight_match)
                best_weight_match = curr_weight_match;
                best_weight = weight_no;
            end
        end
        
        weights_matrix(:, best_weight) = weights_matrix(:, best_weight) + ALFA * (x - weights_matrix(:, best_weight));
    end
end

weights_matrix

end