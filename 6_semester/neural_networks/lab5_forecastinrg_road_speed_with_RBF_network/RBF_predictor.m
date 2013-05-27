function [ ] = RBF_predictor( input )

%% constants block
BACKWARD_DATA_USAGE = 4; %0 is no backward data usage (watcha see now)
ROAD_TO_PREDICT_ON = 3;
HOW_FAR_TO_PREDICT = 3;

%% prepare training data
training_data = prepare_training_data();

%% calc weights matrix and A matrix (for PCA)
[weight_matrix, A_matrix, r, PCAd_probes] = ...
    calc_PCAd_RBF_weight_matrix(training_data, BACKWARD_DATA_USAGE);

%% using PCA on input pattern
predicted_val = ... %omfg - so many parameters
    predict_speed_using_RBF(input, weight_matrix, A_matrix, ...
    training_data, PCAd_probes, r, ...
    ROAD_TO_PREDICT_ON, HOW_FAR_TO_PREDICT, BACKWARD_DATA_USAGE)

end

