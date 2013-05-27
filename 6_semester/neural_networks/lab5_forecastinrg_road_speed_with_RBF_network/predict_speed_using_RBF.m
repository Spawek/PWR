function [ predicted_speed ] = predict_speed_using_RBF( input, weight_matrix, A_matrix, training_data, PCAd_probes, r, ROAD_TO_PREDICT_ON, HOW_FAR_TO_PREDICT, BACKWARD_DATA_USAGE)
    n = size(weight_matrix,1);

    PCAd_input = (input' * A_matrix)';

    pre = zeros(n,1);
    for i = 1:n
        pre(i) = exp(-1*(distance(PCAd_input, PCAd_probes(:,i))/r)^2);
    end

    %% get output vector
    y = weight_matrix * pre;

    %% find and return best match 
    recognised_point = find(ismember(y, max(y)));
    predicted_speed = training_data(ROAD_TO_PREDICT_ON, BACKWARD_DATA_USAGE + HOW_FAR_TO_PREDICT + recognised_point);

end

