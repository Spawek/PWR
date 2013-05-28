function [ ] = RBF_predictor( real_data )

%% constants block
BACKWARD_DATA_USAGE = 4; %0 is no backward data usage (watcha see now)
ROAD_TO_PREDICT_ON = 5;
HOW_FAR_TO_PREDICT = 3;

%% prepare training data
training_data = prepare_reduced_training_data();%prepare_training_data();

%% calc weights matrix and A matrix (for PCA)
[weight_matrix, A_matrix, r, PCAd_probes] = ...
    calc_PCAd_RBF_weight_matrix(training_data, BACKWARD_DATA_USAGE);

%% geting probes out of real data
real_data = fix_input_data(real_data);
real_probes = generate_probes(real_data, BACKWARD_DATA_USAGE);

%% using PCA on input pattern
predicted_vals = zeros(1, size(real_probes,2));
for i = 1:size(real_probes, 2)
    predicted_vals(i) = ...  %omfg - so many parameters
        predict_speed_using_RBF(real_probes(:, i), weight_matrix, ...
        A_matrix, training_data, PCAd_probes, r, ...
        ROAD_TO_PREDICT_ON, HOW_FAR_TO_PREDICT, BACKWARD_DATA_USAGE);
end

%% plotting real val and predicted
clf
subplot(2,1,1)
plot(real_data(ROAD_TO_PREDICT_ON,:), 'b');
hold on
plot([zeros(1, BACKWARD_DATA_USAGE + HOW_FAR_TO_PREDICT), predicted_vals], 'r')
legend('real data', 'predicted values');
title('predicted values shown on real data');


%% plotting abs(error)
subplot(2,1,2)
error = abs(real_data(ROAD_TO_PREDICT_ON,:) - [zeros(1, BACKWARD_DATA_USAGE + HOW_FAR_TO_PREDICT),predicted_vals(1:end-HOW_FAR_TO_PREDICT)]);
error = error(BACKWARD_DATA_USAGE+HOW_FAR_TO_PREDICT+1:end);
error = [zeros(1, BACKWARD_DATA_USAGE+HOW_FAR_TO_PREDICT), error];
plot(error);
title('absolute error');


end

